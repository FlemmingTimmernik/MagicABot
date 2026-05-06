using System;
using System.IO;
//using System.Xml;
using Newtonsoft.Json;

namespace Magic
{
    public class ConfigReader
    {
        public const string DefaultConfigFilePath = @"config\config.json";
        public static ConfigReader Current { get; private set; } = new ConfigReader();

        // Properties to hold configuration values
        public int TimeOutWhite { get; set; }
        public int TimeOutBlack { get; set; }
        public int TimeOutBlue { get; set; }
        public int TimeOutRed { get; set; }
        public int TimeOutGreen { get; set; }
        private int timeOutGreen;
        public int TimeOutNoAction { get; set; }
        public int TimeOutNoCardPlayed { get; set; }       
        public int MaxActiveQuests { get; set; }       
        public bool MainComputer { get; set; }
        public string LoginEmailAccountZero { get; set; } = "";
        public string LoginEmailTemplate { get; set; } = "";
        public string LoginPassword { get; set; } = "";
        public string PlayerLogPath { get; set; } = "";
        public string SteamPath { get; set; } = "";
        public string SteamAppId { get; set; } = "";
        public string MasterSharePath { get; set; } = "";
        public string NextPlayerFileName { get; set; } = "";
        public string LoginOrderFileName { get; set; } = "";
        public string LoginOrderZeroQuestsFileName { get; set; } = "";
        public string LogFilesPath { get; set; } = "";
        public string LogFilesTempPath { get; set; } = "";
        public string LogfilesDumpPath { get; set; } = "";
        public string CleanLogfilesPath { get; set; } = "";
        public string MagicLogFilePath { get; set; } = "";
        public bool DryRun { get; set; }
        public bool DebugClickLogging { get; set; }
        public bool AllowForceKillArena { get; set; }
        public bool AllowNetworkFileWrites { get; set; }
        public bool AllowDeleteProcessedLogFiles { get; set; }

        private readonly string configFilePath;

        public ConfigReader()
        {
            configFilePath = DefaultConfigFilePath;
            SetDefaultValues();
        }

        // Constructor to specify the config file path and load configuration values
        public ConfigReader(string configFilePathPar)
        {
            this.configFilePath = configFilePathPar;
            SetDefaultValues();
            //LoadConfigValues();
        }

        // Method to load the config values from the JSON file
        public void LoadConfigValues()
        {
            if (File.Exists(configFilePath))
            {
                string json = File.ReadAllText(configFilePath);
                var config = JsonConvert.DeserializeObject<ConfigReader>(json);

                if (config != null)
                {
                    TimeOutWhite = config.TimeOutWhite;
                    TimeOutBlack = config.TimeOutBlack;
                    TimeOutBlue = config.TimeOutBlue;
                    TimeOutRed = config.TimeOutRed;
                    TimeOutGreen = config.TimeOutGreen;
                    TimeOutNoAction = config.TimeOutNoAction;
                    TimeOutNoCardPlayed = config.TimeOutNoCardPlayed;
                    MaxActiveQuests = config.MaxActiveQuests;
                    MainComputer = config.MainComputer;
                    LoginEmailAccountZero = config.LoginEmailAccountZero;
                    LoginEmailTemplate = config.LoginEmailTemplate;
                    LoginPassword = config.LoginPassword;
                    PlayerLogPath = config.PlayerLogPath;
                    SteamPath = config.SteamPath;
                    SteamAppId = config.SteamAppId;
                    MasterSharePath = config.MasterSharePath;
                    NextPlayerFileName = config.NextPlayerFileName;
                    LoginOrderFileName = config.LoginOrderFileName;
                    LoginOrderZeroQuestsFileName = config.LoginOrderZeroQuestsFileName;
                    LogFilesPath = config.LogFilesPath;
                    LogFilesTempPath = config.LogFilesTempPath;
                    LogfilesDumpPath = config.LogfilesDumpPath;
                    CleanLogfilesPath = config.CleanLogfilesPath;
                    MagicLogFilePath = config.MagicLogFilePath;
                    DryRun = config.DryRun;
                    DebugClickLogging = config.DebugClickLogging;
                    AllowForceKillArena = config.AllowForceKillArena;
                    AllowNetworkFileWrites = config.AllowNetworkFileWrites;
                    AllowDeleteProcessedLogFiles = config.AllowDeleteProcessedLogFiles;
                }
            }
            else
            {
                SaveConfigValues(); // Save default values to file
            }

            EnsureRuntimeDirectories();
            Current = this;
        }

        // Method to save the current properties to the JSON file
        public void SaveConfigValues()
        {
            string? directoryName = Path.GetDirectoryName(configFilePath);
            if (!string.IsNullOrWhiteSpace(directoryName))
                Directory.CreateDirectory(directoryName);

            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            if (configFilePath == null)
                File.WriteAllText(DefaultConfigFilePath, json);
            else            
                File.WriteAllText(configFilePath, json);
        }

        public void EnsureRuntimeDirectories()
        {
            CreateDirectoryForPath(LogFilesPath);
            CreateDirectoryForPath(LogFilesTempPath);
            CreateDirectoryForPath(LogfilesDumpPath);
            CreateDirectoryForPath(CleanLogfilesPath);
            CreateDirectoryForPath(Path.GetDirectoryName(MagicLogFilePath));
        }

        public string GetExpandedPlayerLogPath()
        {
            return ExpandPath(PlayerLogPath);
        }

        public string GetExpandedSteamPath()
        {
            return ExpandPath(SteamPath);
        }

        public string GetLogFilesPath()
        {
            return ExpandPath(LogFilesPath);
        }

        public string GetLogFilesTempPath()
        {
            return ExpandPath(LogFilesTempPath);
        }

        public string GetLogfilesDumpPath()
        {
            return ExpandPath(LogfilesDumpPath);
        }

        public string GetCleanLogfilesPath()
        {
            return ExpandPath(CleanLogfilesPath);
        }

        public string GetMagicLogFilePath()
        {
            return ExpandPath(MagicLogFilePath);
        }

        public string GetMasterShareFilePath(string fileName)
        {
            if (string.IsNullOrWhiteSpace(MasterSharePath))
                return fileName;

            return Path.Combine(ExpandPath(MasterSharePath), fileName);
        }

        public string GetRemoteLogfilesDumpPath()
        {
            return GetMasterShareFilePath(LogfilesDumpPath);
        }

        public string GetLoginEmail(int accountNumber)
        {
            if (accountNumber == 0)
                return LoginEmailAccountZero;

            if (string.IsNullOrWhiteSpace(LoginEmailTemplate))
                return string.Empty;

            return string.Format(LoginEmailTemplate, accountNumber);
        }

        // Optional method to display current configuration values (for debugging)
        public string DisplayConfigValues()
        {
            string result = string.Empty;
            result += ($"TimeOutWhite: {TimeOutWhite}") + Environment.NewLine;
            result += ($"TimeOutBlack: {TimeOutBlack}") + Environment.NewLine;
            result += ($"TimeOutBlue: {TimeOutBlue}") + Environment.NewLine;
            result += ($"TimeOutRed: {TimeOutRed}") + Environment.NewLine;
            result += ($"TimeOutGreen: {TimeOutGreen}") + Environment.NewLine;
            result += ($"TimeOutNoAction: {TimeOutNoAction}") + Environment.NewLine;
            result += ($"TimeOutNoCardPlayed: {TimeOutNoCardPlayed}") + Environment.NewLine;
            result += ($"MaxActiveQuests: {MaxActiveQuests}") + Environment.NewLine;
            result += ($"MainComputer: {MainComputer}") + Environment.NewLine;
            result += ($"LoginEmailAccountZero: {LoginEmailAccountZero}") + Environment.NewLine;
            result += ($"LoginEmailTemplate: {LoginEmailTemplate}") + Environment.NewLine;
            result += ($"LoginPassword configured: {!string.IsNullOrWhiteSpace(LoginPassword)}") + Environment.NewLine;
            result += ($"PlayerLogPath: {PlayerLogPath}") + Environment.NewLine;
            result += ($"SteamPath: {SteamPath}") + Environment.NewLine;
            result += ($"MasterSharePath: {MasterSharePath}") + Environment.NewLine;
            result += ($"DryRun: {DryRun}") + Environment.NewLine;
            result += ($"DebugClickLogging: {DebugClickLogging}") + Environment.NewLine;

            return result;
        }

        private void SetDefaultValues()
        {
            TimeOutWhite = 90;
            TimeOutBlack = 100;
            TimeOutBlue = 100;
            TimeOutRed = 90;
            TimeOutGreen = 100;
            TimeOutNoAction = 20;
            TimeOutNoCardPlayed = 30;
            MaxActiveQuests = 2;
            MainComputer = false;
            LoginEmailAccountZero = "";
            LoginEmailTemplate = "";
            LoginPassword = "";
            PlayerLogPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                @"AppData\LocalLow\Wizards Of The Coast\MTGA\Player.log"
            );
            SteamPath = @"%ProgramFiles(x86)%\Steam\steam.exe";
            SteamAppId = "2141910";
            MasterSharePath = "";
            NextPlayerFileName = "NextPlayer1.txt";
            LoginOrderFileName = "loginOrder.txt";
            LoginOrderZeroQuestsFileName = "loginOrder0Quests.txt";
            LogFilesPath = "LogFiles";
            LogFilesTempPath = "LogFilesTemp";
            LogfilesDumpPath = "LogfilesDump";
            CleanLogfilesPath = "cleanLogfiles";
            MagicLogFilePath = @"magicLogfile\Logfile.txt";
            DryRun = false;
            DebugClickLogging = false;
            AllowForceKillArena = false;
            AllowNetworkFileWrites = false;
            AllowDeleteProcessedLogFiles = false;
        }

        private static string ExpandPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return path;

            return Environment.ExpandEnvironmentVariables(path);
        }

        private static void CreateDirectoryForPath(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return;

            Directory.CreateDirectory(ExpandPath(path));
        }
    }    
}
