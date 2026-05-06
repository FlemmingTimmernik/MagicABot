using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Magic.Json;

namespace Magic
{
  
    internal class ParseLogfile
    {
        string playerLogFilename = @"C:\Users\Tommy\AppData\LocalLow\Wizards Of The Coast\MTGA\Player.log";
        string tempLogfileName = @"LogFiles\temp.log";
        int currentPlayerStartLine = 0;
        public bool playWhite = false;
        public bool playGreen = false;
        public bool playRed = false;
        public bool playBlack = false;
        public bool playBlue = false;
        public int activeQuests = 0;
        public QuestInfo bestColors;
       
        public ParseLogfile()
        {
            //"<== Quest_GetQuests";

        }

        public int GetLastQuestLine()
        {
            var lines = File.ReadAllLines(tempLogfileName);

            for (int i = lines.Length - 1; i > 0; i--)
            {
                if (lines[i].IndexOf("\"quests\":[") != -1)
                {
                    return i;
                    break;
                }

            }
            return 0;
        }

        public void CopyLogfilesAndNumberThem()
        {
            CopyLocalPlayerLogFile();
            CopyRemoteLogFileIfExist();
            RenameLogfiles();


            //FileInfo fi = new FileInfo(playerLogFilename);
            //var tsse = fi.Length;



           
        }
        string logFilePath = "logfilesdump";
        public void RenameLogfiles()
        {
           
           var files = Directory.GetFiles(logFilePath);

            for (int i = 0; i < files.Length; i++)
            {
                if (i<10)
                    File.Move(files[i], logFilePath + "\\0" + i.ToString() + ".log");
                else
                    File.Move(files[i], logFilePath + "\\" + i.ToString() + ".log");
            }
        }

        public void CopyRemoteLogFileIfExist()
        {
            string externalDirectory = @"\\tommy-pc\delt\Magic\logfilesdump";
            try
            {
                var files = Directory.GetFiles(externalDirectory);
                if (Directory.Exists("Config"))
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string filesname = files[i];
                        string destName = logFilePath + @"\tommy-pc_" + files[i].Substring(files[i].LastIndexOf(@"\") + 1);
                        File.Copy(filesname, destName, false);
                        File.Delete(filesname);
                    }

                }
            }
            catch { }

        }

        private void CopyLocalPlayerLogFile()
        {
            File.Copy(playerLogFilename, @"LogfilesDump\temp.log", false);
        }

        private void CopyLogFileAndReadAllLines()
        {
            if (lastTimeLogFileWasCopied.ElapsedMilliseconds > 3000 || firstTimeLogFileCopied)
            {
                firstTimeLogFileCopied = false;
                lastTimeLogFileWasCopied.Restart();
                File.Copy(playerLogFilename, tempLogfileName, true);
                lines = File.ReadAllLines(tempLogfileName);
            }
          
        }

        private void UpdateStartLine()
        {
            CopyLogFileAndReadAllLines();
            var lines = File.ReadAllLines(tempLogfileName);

                for (int i = lines.Length - 1; i > 0; i--)
                {
                    if (lines[i].IndexOf("[Accounts - Login] Logged in successfully. Display Name:") != -1)
                    {
                        currentPlayerStartLine = i;
                        break;
                    }

                }
        }
        private void CopyFile(string source, string destination)
        {
            try
            {
                File.Copy(source,destination);
            }
            catch (IOException e)
            {
                if (e.Message.Contains("used"))
                {
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.Arguments = "/C copy \"" + source + "\" \"" + destination + "\"";
                    p.Start();
                    Console.WriteLine(p.StandardOutput.ReadToEnd());
                    p.WaitForExit();
                    p.Close();
                }
            }
        }

        private static string[] lines = null;
      
      

        public string GetNames()
        {
            UpdateStartLine();
                     
            
            string result = "";
            int counter = 26;
            string findLine = "[Accounts - Login] Logged in successfully. Display Name:";
            for (int i = lines.Length-1; i > 0; i--)
            {
                if (lines[i].IndexOf(findLine) != -1)
                {
                    result += lines[i].Substring(findLine.Length + 1) + Environment.NewLine;
                    counter--;
                }

            }
            return result;


        }



        public void SetUserName(Player player)
        {
            //player = new Player();
            UpdateStartLine();
            var lines = File.ReadAllLines(tempLogfileName);
            string findUsername = "[Accounts - Login] Logged in successfully. Display Name: ";
            if (currentPlayerStartLine == 0)
            {
                player = null;
                return;
            }

            for (int i = currentPlayerStartLine -1; i < lines.Length-1; i++)
            {
                if (lines[i].IndexOf(findUsername) != -1)
                {
                    player.Name = lines[i].Substring(findUsername.Length);
                    return;
                }

            }
            player.Name = "";
            return;
        }
        private string[] quest = new string[3];
        //public string[] parsedQuests = new string[3];
        public QuestInfo[] parsedQuests = new QuestInfo[3];
        public bool noQuests = false;
        private static Stopwatch lastTimeLogFileWasCopied = new Stopwatch();
        private static bool firstTimeLogFileCopied = true;
        public void GetQuestsForCurrentPlayerJsonStyle(Player player)
        {
            CopyLogFileAndReadAllLines();
            //var lines = File.ReadAllLines(tempLogfileName);

            FindAndParseQuestLine(player,lines, currentPlayerStartLine);
            


          

            // ImportDecks.CalculateWhatColorToPlay(player);



            if (player.QuestCollection.Quests.Count == 3)
            {
                QuestInfo q0 = ConvertQuestFromQuestCollectionToQuestInfo(player.QuestCollection.Quests[0]);
                QuestInfo q1 = ConvertQuestFromQuestCollectionToQuestInfo(player.QuestCollection.Quests[1]);
                QuestInfo q2 = ConvertQuestFromQuestCollectionToQuestInfo(player.QuestCollection.Quests[2]);
                var bestColors = FindBestSingleColor(q0, q1, q2);

                SetColorsOnPlayerFromQuestInfo(bestColors, player);
            }
            if (player.QuestCollection.Quests.Count == 2)
            {
                QuestInfo q0 = ConvertQuestFromQuestCollectionToQuestInfo(player.QuestCollection.Quests[0]);
                QuestInfo q1 = ConvertQuestFromQuestCollectionToQuestInfo(player.QuestCollection.Quests[1]);
                var bestColors = FindBestColors(q0, q1);
                SetColorsOnPlayerFromQuestInfo(bestColors, player);
            }
            if (player.QuestCollection.Quests.Count == 1)
            {
                QuestInfo q0 = ConvertQuestFromQuestCollectionToQuestInfo(player.QuestCollection.Quests[0]);
                var bestColors = FindBestColors(q0);
                SetColorsOnPlayerFromQuestInfo(bestColors, player);
            }

        }

        public void FindAndParseQuestLine(Player player, string[] linesPar, int earliestLineToRead = 0)
        {
            bool foundQuests = false;
            bool foundInventoryInfo = false;
            for (int i = linesPar.Length - 1; i >= earliestLineToRead; i--)
            {
                int lineIndexNumber = linesPar[i].IndexOf("<== Quest_GetQuests");
                if (lineIndexNumber == 0 && !foundQuests)
                {
                    player.QuestCollection = Magic.Json.JsonExample.ParseQuests(linesPar[i+ 1]);
                    noQuests = true;
                    foundQuests = true;
                }

                lineIndexNumber = linesPar[i].IndexOf("<== Quest_GetQuests");
                if (lineIndexNumber != -1 && !foundInventoryInfo)
                {
                    player.InventoryInfo = Magic.Json.JsonExample.ParseInventoryInfo(linesPar[i]);
                    noQuests = true;
                    foundInventoryInfo = true;
                }

                if (foundQuests && foundInventoryInfo)
                    break;
            }

           
            //for (int i = 0; i < player.QuestCollection.Quests.Count; i++)
            //{
            //    ParseQuestsNamesToColors(player.QuestCollection.Quests[i]);
            //}
        }

        private void SetColorsOnPlayerFromQuestInfo(QuestInfo qi, Player p)
        {
            p.Blue = qi.Blue;
            p.Green = qi.Green;
            p.Red = qi.Red;
            p.Black = qi.Black;
            p.White = qi.White;
        }

        private QuestInfo ConvertQuestFromQuestCollectionToQuestInfo(Quest quest)
        {
            return new QuestInfo() { Black = quest.Black, White = quest.White, Green = quest.Green, Red = quest.Red, Blue = quest.Blue };
        }

        public int swapQuestNumber = -1;
        public bool swapQuest = false;
        private void SwapQuest(int NumberOfQuests)
        {            
            swapQuest = false;
            for (int i = 3- NumberOfQuests; i < 3; i++)
            {
                if (parsedQuests[i].Shift)
                {
                    swapQuest = true;
                    swapQuestNumber = i + (3 - NumberOfQuests); 
                    return;
                }
            }


            for (int i = 0; i < 3; i++)
            {
                int moveSpaces = i+(NumberOfQuests-1);
                if (parsedQuests[i].Value == 500)
                {
                    swapQuest = true;
                    swapQuestNumber = i + (NumberOfQuests - 1);
                    return;
                }
            }
        }       

        public void ParseQuestsNamesToColors(Json.Quest newQuest)
        {
            newQuest.Shift = false;

            if (newQuest.LocKey.IndexOf("Azorius") != -1)
            {
                if (newQuest.LocKey.IndexOf("Justiciar") != -1)
                    newQuest.ChestDescription.Quantity = 500 + newQuest.EndingProgress;
                else
                    newQuest.ChestDescription.Quantity = 750 + newQuest.EndingProgress;

                newQuest.White = true;
                newQuest.Blue = true;
            }

            if (newQuest.LocKey.IndexOf("Boros") != -1)
            {
                if (newQuest.LocKey.IndexOf("Reckoner") != -1)
                    newQuest.ChestDescription.Quantity = 500 + newQuest.EndingProgress;
                else
                    newQuest.ChestDescription.Quantity = 750 + newQuest.EndingProgress;

                newQuest.White = true;
                newQuest.Red = true;
            }

            if (newQuest.LocKey.IndexOf("Dimir") != -1)
            {
                if (newQuest.LocKey.IndexOf("Cutpurse") != -1)
                    newQuest.ChestDescription.Quantity = 500 + newQuest.EndingProgress;
                else
                    newQuest.ChestDescription.Quantity = 750 + newQuest.EndingProgress;

                newQuest.Blue = true;
                newQuest.Black = true;
                return ;
            }

            if (newQuest.LocKey.IndexOf("Golgari") != -1)
            {
                if (newQuest.LocKey.IndexOf("Guildmage") != -1)
                    newQuest.ChestDescription.Quantity = 500 + newQuest.EndingProgress;
                else
                    newQuest.ChestDescription.Quantity = 750 + newQuest.EndingProgress;

                newQuest.Black = true;
                newQuest.Green = true;
                return ;
            }

            if (newQuest.LocKey.IndexOf("Gruul") != -1)
            {
                if (newQuest.LocKey.IndexOf("Scrapper") != -1)
                    newQuest.ChestDescription.Quantity = 500 + newQuest.EndingProgress;
                else
                    newQuest.ChestDescription.Quantity = 750 + newQuest.EndingProgress;

                newQuest.Red = true;
                newQuest.Green = true;
                return ;
            }

            if (newQuest.LocKey.IndexOf("Izzet") != -1)
            {
                if (newQuest.LocKey.IndexOf("Chronarch") != -1)
                    newQuest.ChestDescription.Quantity = 500 + newQuest.EndingProgress;
                else
                    newQuest.ChestDescription.Quantity = 750 + newQuest.EndingProgress;

                newQuest.Red = true;
                newQuest.Blue = true;
                return ;
            }

            if (newQuest.LocKey.IndexOf("Orzhov") != -1)
            {
                if (newQuest.LocKey.IndexOf("Advokist") != -1)
                    newQuest.ChestDescription.Quantity = 500 + newQuest.EndingProgress;
                else
                    newQuest.ChestDescription.Quantity = 750 + newQuest.EndingProgress;

                newQuest.White = true;
                newQuest.Black = true;
                return ;
            }

            if (newQuest.LocKey.IndexOf("Rakdos") != -1)
            {
                if (newQuest.LocKey.IndexOf("Destruction") != -1)
                    newQuest.ChestDescription.Quantity = 750 + newQuest.EndingProgress;
                else if (newQuest.LocKey.IndexOf("Cackler") != -1)
                    newQuest.ChestDescription.Quantity = 500 + newQuest.EndingProgress;

                newQuest.Red = true;
                newQuest.Black = true;
                return ;
            }

            if (newQuest.LocKey.IndexOf("Selesnya") != -1)
            {
                if (newQuest.LocKey.IndexOf("Sentry") != -1)
                    newQuest.ChestDescription.Quantity = 500 + newQuest.EndingProgress;
                else
                    newQuest.ChestDescription.Quantity = 750 + newQuest.EndingProgress;

                newQuest.White = true;
                newQuest.Green = true;
                return ;
            }

            if (newQuest.LocKey.IndexOf("Simic") != -1)
            {
                if (newQuest.LocKey.IndexOf("Manipulator") != -1)
                    newQuest.ChestDescription.Quantity = 500 + newQuest.EndingProgress;
                else if (newQuest.LocKey.IndexOf("Evolution") != -1)
                    newQuest.ChestDescription.Quantity = 750 + newQuest.EndingProgress; 
                
                
                newQuest.Blue = true;
                newQuest.Green = true;
                return ;
            }

            //Kill
            if (newQuest.LocKey.IndexOf("Fatal") != -1 || newQuest.LocKey.IndexOf("Tragic") != -1)
            {
                newQuest.Shift = true;
                newQuest.White = true;
                newQuest.Red = true;
                newQuest.Blue = true;
                newQuest.Black = true;
                newQuest.Green = true;

                return ;
            }

            //Play Land
            if (newQuest.LocKey.IndexOf("Nissas") != -1)
            {
                newQuest.White = true;
                newQuest.Red = true;
                newQuest.Blue = true;
                newQuest.Black = true;
                newQuest.Green = true;
                newQuest.ChestDescription.Quantity = 550;
                return ;
            }

            //Play Creatures Har begge to  goal":20,"locKey":"Quests/Quest_Creature_Comforts", Play creatures : goal":40,"locKey":"Quests / Quest_Creature_Commander", Play Creatures
            if (newQuest.LocKey.IndexOf("Creature") != -1)
            {
                newQuest.White = true;
                newQuest.Red = true;
                newQuest.Blue = true;
                newQuest.Black = true;
                newQuest.Green = true;
                newQuest.ChestDescription.Quantity = 550;
                return;
            }
            
            //Attack
            if (newQuest.LocKey.IndexOf("Raiding") != -1 || newQuest.LocKey.IndexOf("Almighty") != -1)
            {
                newQuest.Shift = true;
                newQuest.White = true;
                newQuest.Red = true;
                newQuest.Blue = true;
                newQuest.Black = true;
                newQuest.Green = true;
                return ;
            }
        }


        public List<string> GetAllQuests()
        {//[Accounts - Login] Logged {"quests
            Regex r = new Regex("{\"questId\".*?}}}");// "}}]}"
            //Regex r = new Regex("Accounts - Login.*");// "}}]}"
            //Regex r = new Regex("goal.*?locKey.*?,");// "}}]}"
            //Regex r = new Regex("questId.*?locKey.*}}]}");
            var lines = File.ReadAllText(tempLogfileName);
            var matches = r.Matches(lines).ToList();
            List<string> result = new List<string>();
            for (int i = 0; i < matches.Count; i++)
            {
                result.Add(matches[i].Value);
            }
            return result;

        }
        static QuestInfo FindBestColors(QuestInfo quest1)
        {
            return FindBestColors(quest1, new QuestInfo() { White = true, Black = true, Blue = true, Red = true, Green = true });
        }
        static QuestInfo FindBestColors(QuestInfo quest1, QuestInfo quest2)
        {
            return FindBestColors(quest1, quest2, new QuestInfo() { White = true, Black = true, Blue = true, Red = true, Green = true });
        }

        public static QuestInfo FindBestSingleColor(QuestInfo quest1, QuestInfo quest2, QuestInfo quest3)
        {
            // Get all available colors from each quest
            List<string> availableColors1 = quest1.GetAvailableColors();
            List<string> availableColors2 = quest2.GetAvailableColors();
            List<string> availableColors3 = quest3.GetAvailableColors();

            // Combine all colors into one list
            List<string> allColors = availableColors1.Concat(availableColors2).Concat(availableColors3).ToList();

            // Count occurrences of each color
            Dictionary<string, int> colorCounts = new Dictionary<string, int>();

            foreach (var color in allColors)
            {
                if (colorCounts.ContainsKey(color))
                {
                    colorCounts[color]++;
                }
                else
                {
                    colorCounts[color] = 1;
                }
            }

            // Find the color with the highest count
            string bestColor = colorCounts.OrderByDescending(c => c.Value).First().Key;

            QuestInfo newCurrentQuest = new QuestInfo(false, false, false, false, false);

            switch (bestColor)
            {
                case "White":
                    newCurrentQuest.White = true;
                    break;
                case "Blue":
                    newCurrentQuest.Blue = true;
                    break;
                case "Black":
                    newCurrentQuest.Black = true;
                    break;
                case "Red":
                    newCurrentQuest.Red = true;
                    break;
                case "Green":
                    newCurrentQuest.Green = true;
                    break;

                default:
                    break;
            }



            return newCurrentQuest;
        }

        static QuestInfo FindBestColors(QuestInfo quest1, QuestInfo quest2, QuestInfo quest3)
        {
            // Get all available colors from each quest
            List<string> availableColors1 = quest1.GetAvailableColors();
            List<string> availableColors2 = quest2.GetAvailableColors();
            List<string> availableColors3 = quest3.GetAvailableColors();

            // Track the best combination and the minimum number of unique colors
            QuestInfo bestCombination = new QuestInfo();
            int minUniqueColors = int.MaxValue;

            // Try all combinations of picking one color from each quest
            foreach (var color1 in availableColors1)
            {
                foreach (var color2 in availableColors2)
                {
                    foreach (var color3 in availableColors3)
                    {
                        // Create a set of unique colors
                        HashSet<string> uniqueColors = new HashSet<string> { color1, color2, color3 };

                        // If the current combination has fewer unique colors, update the best combination
                        if (uniqueColors.Count < minUniqueColors)
                        {
                            minUniqueColors = uniqueColors.Count;
                            bestCombination = CreateQuestFromColors(uniqueColors);
                        }
                    }
                }
            }

            return bestCombination;
        }

        static QuestInfo CreateQuestFromColors(HashSet<string> colors)
        {
            // Initialize a Quest with default values
            QuestInfo quest = new QuestInfo(false, false, false, false, false);

            // Set the appropriate fields in the Quest based on the selected colors
            foreach (var color in colors)
            {
                switch (color)
                {
                    case "White":
                        quest.White = true;
                        break;
                    case "Red":
                        quest.Red = true;
                        break;
                    case "Blue":
                        quest.Blue = true;
                        break;
                    case "Green":
                        quest.Green = true;
                        break;
                    case "Black":
                        quest.Black = true;
                        break;
                }
            }

            return quest;
        }

    }
    struct QuestInfo
    {
        public bool White;
        public bool Red;
        public bool Blue;
        public bool Green;
        public bool Black;
        public bool noQuest = true;
        public int Value;

        public string progress = "0";
        public string goal = "0";
        public bool Shift = false;

        public QuestInfo(bool white, bool red, bool blue, bool green, bool black, int value = 100)
        {
            White = white;
            Red = red;
            Blue = blue;
            Green = green;
            Black = black;
            Value = value;
        }


        // Return the list of colors set to true in the current quest
        public List<string> GetAvailableColors()
        {
            List<string> colors = new List<string>();
            if (White) colors.Add("White");
            if (Red) colors.Add("Red");
            if (Blue) colors.Add("Blue");
            if (Green) colors.Add("Green");
            if (Black) colors.Add("Black");
            return colors;
        }

       

        // Override ToString for easy display of Quest contents
        public override string ToString()
        {
            string result = string.Join(", ", GetAvailableColors()) + "(" + progress + "/" + goal + ")  value: " + Value;
            return result;
        }
    }

}
