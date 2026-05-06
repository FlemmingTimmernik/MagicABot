using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Magic
{
    internal static class MTGLogfileReader
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
       string lpFileName,
       [MarshalAs(UnmanagedType.U4)] FileAccess dwDesiredAccess,
       [MarshalAs(UnmanagedType.U4)] FileShare dwShareMode,
       IntPtr lpSecurityAttributes,
       [MarshalAs(UnmanagedType.U4)] FileMode dwCreationDisposition,
       [MarshalAs(UnmanagedType.U4)] FileAttributes dwFlagsAndAttributes,
       IntPtr hTemplateFile);

        public static string[] Last50Lines()
        {
            string[] result = new string[50];
            StreamReader sr = new StreamReader(ConfigReader.Current.GetExpandedPlayerLogPath());
            Stopwatch sw = Stopwatch.StartNew();
            sw.Stop();
            var test2 = sw.ElapsedMilliseconds;
            for (int i = 0; i < result.Length; i++)
            {

            }

            return result;
        }
        public static string strTurn = "";
        public static string strGameState = "";
        public static string strPlayerName = "";
        
        public static string Test2()
        {
            string filePath = ConfigReader.Current.GetExpandedPlayerLogPath();
            
            // Open the file with read-only access and shared read/write permissions
            SafeFileHandle handle = CreateFile(
                filePath,
                FileAccess.Read,
                FileShare.ReadWrite,
                IntPtr.Zero,
                FileMode.Open,
                FileAttributes.Normal,
                IntPtr.Zero);


            string lineResult = "";

            bool gameStartedFound = false;
            bool turnInfoFound = false;

            if (!handle.IsInvalid)
            {
                using (FileStream fs = new FileStream(handle, FileAccess.Read))
                using (StreamReader reader = new StreamReader(fs))
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    string content = reader.ReadToEnd();
                    int start = Math.Max(content.Length - 2000000, 0);
                    string[] ending = content.Substring(start).Split(Environment.NewLine);
                    sw.Stop();
                    var test2 = sw.ElapsedMilliseconds;

                    for (int i = ending.Length - 1; i > 0; i--)
                    {
                        if (!turnInfoFound && ending[i].IndexOf("\"turnInfo\"") != -1)
                        {
                            string line = ending[i].Substring(ending[i].IndexOf("\"turnInfo\""));
                            lineResult = line.Substring(0, line.IndexOf('}')+1);
                            turnInfoFound = true;
                        }

                        if (!turnInfoFound && ending[i].IndexOf("OnSceneLoaded for PreGameScene") != -1)
                        {
                            strGameState = "In Game";
                            turnInfoFound = true;
                        }
                        if (!turnInfoFound && ending[i].IndexOf("OnSceneLoaded for MatchEndScene") != -1)
                        {
                            strGameState = "Endscreen";
                            turnInfoFound = true;
                        }
                        if (!turnInfoFound && ending[i].IndexOf("Client.SceneChange") != -1)
                        {
                            strGameState = ParseJson(ending[i], "toSceneName");
                            turnInfoFound = true;
                        }

                        if (gameStartedFound && turnInfoFound)
                            break;
                    }
                }
                //OnSceneLoaded for PreGameScene
            }
            else
            {

            }
            return lineResult;
        }

        private static string ParseJson(string json, string regexPar)
        {
            Regex regex = new Regex("\"" + regexPar + "\":(.*?),");
            Match match = regex.Match(json);

            if (match.Success)
            {
                string result = match.Groups[1].Value;
                return result;
            }
            return "";
        }


    }
}
