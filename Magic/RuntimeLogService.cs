using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Magic
{
    public class RuntimeLogService
    {
        private readonly ConfigReader configReader;

        public RuntimeLogService(ConfigReader configReader)
        {
            this.configReader = configReader;
        }

        public void CopyLogFiles(int accountNumber)
        {
            string source = configReader.GetExpandedPlayerLogPath();
            string logfileFolder = configReader.GetLogFilesPath();
            Directory.CreateDirectory(logfileFolder);

            string destinationFileName = accountNumber != -1 ? accountNumber + ".txt" : "temp.txt";
            string destination = Path.Combine(logfileFolder, destinationFileName);

            if (configReader.DryRun)
            {
                LogFile.WriteLog($"DRY RUN: Would copy log file {source} to {destination}");
                return;
            }

            File.Copy(source, destination, true);
        }

        public void SplitLogFilesToIndividualLogFiles()
        {
            string dumpPath = configReader.GetLogfilesDumpPath();
            Directory.CreateDirectory(dumpPath);

            var filenames = Directory.GetFiles(dumpPath);
            foreach (var filename in filenames)
            {
                if (!Path.GetExtension(filename).Equals(".log", StringComparison.OrdinalIgnoreCase))
                    continue;

                ReadLogFile(filename);
                DeleteProcessedFile(filename);
            }
        }

        public void ReadAllLogFiles()
        {
            string tempPath = configReader.GetLogFilesTempPath();
            Directory.CreateDirectory(tempPath);

            var filenames = Directory.GetFiles(tempPath);
            foreach (var filename in filenames)
            {
                OnlySaveImportantLines(filename);
            }
        }

        public void OnlySaveImportantLines(string fileNamePar)
        {
            string cleanLogfilesDirectory = configReader.GetCleanLogfilesPath();
            Directory.CreateDirectory(cleanLogfilesDirectory);

            string[] lines = File.ReadAllLines(fileNamePar);
            bool inventoryLine = true;
            StringBuilder sb = new StringBuilder();
            string firstLineForDatetime = "";
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                if (lines[i].IndexOf("{\"InventoryInfo\":{\"") != -1 && inventoryLine)
                {
                    firstLineForDatetime = i >= 2 ? lines[i - 2] : "";
                    if (!string.IsNullOrWhiteSpace(firstLineForDatetime))
                        sb.Append(firstLineForDatetime + Environment.NewLine);

                    sb.Append(lines[i]);
                    inventoryLine = false;
                    break;
                }
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].IndexOf("{\"InventoryInfo\":{\"") != -1 && inventoryLine)
                {
                    if (i >= 2)
                        sb.AppendLine(lines[i - 2]);

                    sb.AppendLine(lines[i].Length > 2000 ? lines[i].Substring(0, 2000) : lines[i]);
                    inventoryLine = false;
                }

                if (lines[i].IndexOf("\"quests\":[") != -1)
                {
                    if (i >= 2)
                        sb.AppendLine(lines[i - 2]);

                    sb.AppendLine(lines[i]);
                }

                if (lines[i].IndexOf("<== Rank_GetCombinedRankInfo") != -1)
                {
                    sb.AppendLine(lines[i]);
                    if (i + 1 < lines.Length)
                        sb.AppendLine(lines[i + 1]);
                }
            }

            string cleanFileName = Path.Combine(cleanLogfilesDirectory, Path.GetFileName(fileNamePar));

            try
            {
                string firstLine = "[UnityCrossThreadLogger]";
                string oldFileTimeStamp = File.ReadAllLines(cleanFileName)[0].Substring(firstLine.Length);
                DateTime oldFileDatetime = ConvertLineToDatetime(oldFileTimeStamp);
                DateTime newFileDatetime = ConvertLineToDatetime(firstLineForDatetime.Substring(firstLine.Length));

                if (newFileDatetime.Ticks > oldFileDatetime.Ticks)
                {
                    File.WriteAllText(cleanFileName, sb.ToString(), Encoding.UTF8);
                }
            }
            catch
            {
                File.WriteAllText(cleanFileName, sb.ToString(), Encoding.UTF8);
            }

            DeleteProcessedFile(fileNamePar);
        }

        public void ReadLogFile(string fileNamePar)
        {
            string tempPath = configReader.GetLogFilesTempPath();
            Directory.CreateDirectory(tempPath);

            string[] lines = File.ReadAllLines(fileNamePar);

            List<Tuple<int, string>> list = new List<Tuple<int, string>>();
            string find = "[Accounts - Login] Logged in successfully. Display Name: ";
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].IndexOf(find) != -1)
                    list.Add(new Tuple<int, string>(i, lines[i].Substring(find.Length)));
            }

            if (lines.Length == 0)
                return;

            list.Add(new Tuple<int, string>(lines.Length - 1, "Nobody"));
            for (int accountNumber = 0; accountNumber < list.Count - 1; accountNumber++)
            {
                StringBuilder stringBuilder = new StringBuilder();
                int startLine = Math.Max(list[accountNumber].Item1 - 1, 0);
                int endLine = Math.Max(list[accountNumber + 1].Item1 - 2, startLine);

                for (int x = startLine; x < endLine; x++)
                {
                    stringBuilder.AppendLine(lines[x]);
                }

                string filename = Path.Combine(tempPath, list[accountNumber].Item2 + ".log");
                File.WriteAllText(filename, stringBuilder.ToString(), Encoding.UTF8);
            }
        }

        private static DateTime ConvertLineToDatetime(string dateTimeString)
        {
            return DateTime.ParseExact(dateTimeString, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        private void DeleteProcessedFile(string filename)
        {
            if (configReader.AllowDeleteProcessedLogFiles)
                File.Delete(filename);
        }
    }
}
