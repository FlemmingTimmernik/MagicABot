using System;
using System.IO;
//using System.Xml;
using Newtonsoft.Json;

namespace Magic
{
    public class ConfigReader
    {
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

        private readonly string configFilePath;

        public ConfigReader()
        {
            configFilePath = @"config\config.txt";
            SetDefaultValues();
        }

        // Constructor to specify the config file path and load configuration values
        public ConfigReader(string configFilePathPar)// = @"config\config.txt")
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
                }
            }
            else
            {
                SaveConfigValues(); // Save default values to file
            }
        }

        // Method to save the current properties to the JSON file
        public void SaveConfigValues()
        {
            string? directoryName = Path.GetDirectoryName(configFilePath);
            if (!string.IsNullOrWhiteSpace(directoryName))
                Directory.CreateDirectory(directoryName);

            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            if (configFilePath == null)
                File.WriteAllText(@"config\config.txt", json);
            else            
                File.WriteAllText(configFilePath, json);
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
        }
    }    
}
