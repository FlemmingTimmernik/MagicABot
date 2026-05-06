using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Magic.Json
{
    public class Quest
    {
        [JsonPropertyName("questId")]
        public string QuestId { get; set; }

        [JsonPropertyName("goal")]
        public int Goal { get; set; }

        [JsonPropertyName("locKey")]
        public string LocKey { get; set; }

        [JsonPropertyName("tileResourceId")]
        public string TileResourceId { get; set; }

        [JsonPropertyName("treasureResourceId")]
        public string TreasureResourceId { get; set; }

        [JsonPropertyName("questTrack")]
        public string QuestTrack { get; set; }

        [JsonPropertyName("endingProgress")]
        public int? EndingProgress { get; set; } // Nullable in case the value is missing
        public int intEndingProgress 
        { 
            get 
            { 
                return EndingProgress != null ? (int)EndingProgress : 0; 
            }
        }

        [JsonPropertyName("startingProgress")]
        public int? StartingProgress { get; set; } // Nullable in case the value is missing

        [JsonPropertyName("chestDescription")]
        public ChestDescription ChestDescription { get; set; }

        public bool White;
        public bool Black;
        public bool Blue;
        public bool Green;
        public bool Red;
        public bool Shift;
    }

    public class ChestDescription
    {
        [JsonPropertyName("image1")]
        public string Image1 { get; set; }

        [JsonPropertyName("prefab")]
        public string Prefab { get; set; }

        [JsonPropertyName("headerLocKey")]
        public string HeaderLocKey { get; set; }

        [JsonPropertyName("quantity")]
        public string quantity { get; set; }

        public int? Quantity 
        {
            get { return int.Parse(quantity); }
            set { quantity = value != null ? value.ToString() : "0"; }
        }

        public ChestDescription(int? quantity)
        {
            Quantity = quantity;
        }

        [JsonPropertyName("locParams")]
        public LocParams LocParams { get; set; }
    }

    public class LocParams
    {
        [JsonPropertyName("number1")]
        public int Number1 { get; set; }

        [JsonPropertyName("number2")]
        public int Number2 { get; set; }
    }

    public class QuestCollection
    {
        [JsonPropertyName("canSwap")]
        public bool CanSwap { get; set; }

        [JsonPropertyName("quests")]
        public List<Quest> Quests { get; set; }
    }

    public class InventoryInfo
    {
        [JsonPropertyName("Gold")]
        public int Gold { get; set; }
        [JsonPropertyName("TotalVaultProgress")]
        public int TotalVaultProgress { get; set; }

        [JsonPropertyName("Gems")]
        public int Gems { get; set; }

        [JsonPropertyName("WildCardCommons")]
        public int WildCardCommons { get; set; }
        [JsonPropertyName("WildCardUnCommons")]
        public int WildCardUnCommons { get; set; }
        [JsonPropertyName("WildCardUnCommons")]
        public int WildCardRares { get; set; }
        [JsonPropertyName("WildCardMythics")]
        public int WildCardMythics { get; set; }

        [JsonPropertyName("BattlePass_BLB_Orb")]
        public int BattlePass_BLB_Orb { get; set; }
        [JsonPropertyName("Token_JumpIn")]
        public int Token_JumpIn { get; set; }
        [JsonPropertyName("BattlePass_DSK_Orb")]
        public int BattlePass_DSK_Orb { get; set; }


    }

    public class JsonExample
    {
        public static InventoryInfo ParseInventoryInfo(string json)
        {
            try
            {
                Regex regexGold = new Regex("\"Gold\":(\\d+)");
                Match matchGold = regexGold.Match(json);
                InventoryInfo info = new InventoryInfo();
                if (matchGold.Success)
                {
                    int gold = int.Parse(matchGold.Groups[1].Value);
                    info.Gold = gold;
                }

                info.WildCardCommons = ParseJson(json, "WildCardCommons");
                info.WildCardUnCommons = ParseJson(json, "WildCardUnCommons");
                info.WildCardRares = ParseJson(json, "WildCardRares");
                info.WildCardMythics = ParseJson(json, "WildCardMythics");
                info.Token_JumpIn = ParseJson(json, "Token_JumpIn");
                info.BattlePass_BLB_Orb = ParseJson(json, "BattlePass_BLB_Orb");
                info.BattlePass_DSK_Orb = ParseJson(json, "BattlePass_DSK_Orb");



                return info;
            }
            catch (JsonException e)
            {
                Console.WriteLine($"Error deserializing JSON: {e.Message}");
                return null;
            }
        }

        private static int ParseJson(string json, string regexPar)
        {
            Regex regex = new Regex("\"" + regexPar + "\":(\\d+)");
            Match match = regex.Match(json);

            if (match.Success)
            {
                int result = int.Parse(match.Groups[1].Value);
                return result;
            }
            return 0;
        }

        public static QuestCollection ParseQuests(string json)
        {
            try
            {
                // Deserialize the JSON string into a QuestCollection object
                QuestCollection? questCollection = JsonSerializer.Deserialize<QuestCollection>(json);
                for (int i = 0; i < questCollection.Quests.Count; i++)
                {
                    ParseQuestsNamesToColors(questCollection.Quests[i]);
                }
                return questCollection;
            }
            catch (JsonException e)
            {
                Console.WriteLine($"Error deserializing JSON: {e.Message}");
                return null;
            }
        }

        public static void ParseQuestsNamesToColors(Json.Quest newQuest)
        {
            newQuest.Shift = false;

            if (newQuest.LocKey.IndexOf("Azorius") != -1)
            {
                if (newQuest.LocKey.IndexOf("Justiciar") != -1)
                    newQuest.ChestDescription.Quantity = 500;
                else
                    newQuest.ChestDescription.Quantity = 750;

                newQuest.White = true;
                newQuest.Blue = true;
            }

            if (newQuest.LocKey.IndexOf("Boros") != -1)
            {
                if (newQuest.LocKey.IndexOf("Reckoner") != -1)
                    newQuest.ChestDescription.Quantity = 500;
                else
                    newQuest.ChestDescription.Quantity = 750;

                newQuest.White = true;
                newQuest.Red = true;
            }

            if (newQuest.LocKey.IndexOf("Dimir") != -1)
            {
                if (newQuest.LocKey.IndexOf("Cutpurse") != -1)
                    newQuest.ChestDescription.Quantity = 500;
                else
                    newQuest.ChestDescription.Quantity = 750;

                newQuest.Blue = true;
                newQuest.Black = true;
                return;
            }

            if (newQuest.LocKey.IndexOf("Golgari") != -1)
            {
                if (newQuest.LocKey.IndexOf("Guildmage") != -1)
                    newQuest.ChestDescription.Quantity = 500;
                else
                    newQuest.ChestDescription.Quantity = 750;

                newQuest.Black = true;
                newQuest.Green = true;
                return;
            }

            if (newQuest.LocKey.IndexOf("Gruul") != -1)
            {
                if (newQuest.LocKey.IndexOf("Scrapper") != -1)
                    newQuest.ChestDescription.Quantity = 500;
                else
                    newQuest.ChestDescription.Quantity = 750;

                newQuest.Red = true;
                newQuest.Green = true;
                return;
            }

            if (newQuest.LocKey.IndexOf("Izzet") != -1)
            {
                if (newQuest.LocKey.IndexOf("Chronarch") != -1)
                    newQuest.ChestDescription.Quantity = 500;
                else
                    newQuest.ChestDescription.Quantity = 750;

                newQuest.Red = true;
                newQuest.Blue = true;
                return;
            }

            if (newQuest.LocKey.IndexOf("Orzhov") != -1)
            {
                if (newQuest.LocKey.IndexOf("Advokist") != -1)
                    newQuest.ChestDescription.Quantity = 500;
                else
                    newQuest.ChestDescription.Quantity = 750;

                newQuest.White = true;
                newQuest.Black = true;
                return;
            }

            if (newQuest.LocKey.IndexOf("Rakdos") != -1)
            {
                if (newQuest.LocKey.IndexOf("Destruction") != -1)
                    newQuest.ChestDescription.Quantity = 750;
                else if (newQuest.LocKey.IndexOf("Cackler") != -1)
                    newQuest.ChestDescription.Quantity = 500;

                newQuest.Red = true;
                newQuest.Black = true;
                return;
            }

            if (newQuest.LocKey.IndexOf("Selesnya") != -1)
            {
                if (newQuest.LocKey.IndexOf("Sentry") != -1)
                    newQuest.ChestDescription.Quantity = 500;
                else
                    newQuest.ChestDescription.Quantity = 750;

                newQuest.White = true;
                newQuest.Green = true;
                return;
            }

            if (newQuest.LocKey.IndexOf("Simic") != -1)
            {
                if (newQuest.LocKey.IndexOf("Manipulator") != -1)
                    newQuest.ChestDescription.Quantity = 500;
                else if (newQuest.LocKey.IndexOf("Evolution") != -1)
                    newQuest.ChestDescription.Quantity = 750;

                newQuest.Blue = true;
                newQuest.Green = true;
                return;
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

                return;
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
                return;
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
                return;
            }
        }


        //private static void ParseQuestsNamesToColors(Json.Quest newQuest)
        //{
        //    newQuest.Shift = false;

        //    if (newQuest.LocKey.IndexOf("Azorius") != -1)
        //    {
        //        newQuest.White = true;
        //        newQuest.Blue = true;
        //    }

        //    if (newQuest.LocKey.IndexOf("Boros") != -1)
        //    {
        //        newQuest.White = true;
        //        newQuest.Red = true;
        //    }

        //    if (newQuest.LocKey.IndexOf("Dimir") != -1)
        //    {
        //        newQuest.Blue = true;
        //        newQuest.Black = true;
        //        return;
        //    }

        //    if (newQuest.LocKey.IndexOf("Golgari") != -1)
        //    {
        //        newQuest.Black = true;
        //        newQuest.Green = true;
        //        return;
        //    }

        //    if (newQuest.LocKey.IndexOf("Gruul") != -1)
        //    {
        //        newQuest.Red = true;
        //        newQuest.Green = true;
        //        return;
        //    }

        //    if (newQuest.LocKey.IndexOf("Izzet") != -1)
        //    {
        //        newQuest.Red = true;
        //        newQuest.Blue = true;
        //        return;
        //    }

        //    if (newQuest.LocKey.IndexOf("Orzhov") != -1)
        //    {
        //        newQuest.White = true;
        //        newQuest.Black = true;
        //        return;
        //    }

        //    if (newQuest.LocKey.IndexOf("Rakdos") != -1)
        //    {
        //        newQuest.Red = true;
        //        newQuest.Black = true;
        //        return;
        //    }

        //    if (newQuest.LocKey.IndexOf("Selesnya") != -1)
        //    {
        //        newQuest.White = true;
        //        newQuest.Green = true;
        //        return;
        //    }

        //    if (newQuest.LocKey.IndexOf("Simic") != -1)
        //    {
        //        newQuest.Blue = true;
        //        newQuest.Green = true;
        //        return;
        //    }

        //    //Kill
        //    if (newQuest.LocKey.IndexOf("Fatal") != -1 || newQuest.LocKey.IndexOf("Tragic") != -1)
        //    {
        //        newQuest.Shift = true;
        //        newQuest.White = true;
        //        newQuest.Red = true;
        //        newQuest.Blue = true;
        //        newQuest.Black = true;
        //        newQuest.Green = true;

        //        return;
        //    }

        //    //Play Land
        //    if (newQuest.LocKey.IndexOf("Nissas") != -1)
        //    {
        //        newQuest.White = true;
        //        newQuest.Red = true;
        //        newQuest.Blue = true;
        //        newQuest.Black = true;
        //        newQuest.Green = true;
        //        newQuest.ChestDescription.Quantity = "501";
        //        return;
        //    }

        //    //Play Creatures Har begge to  goal":20,"locKey":"Quests/Quest_Creature_Comforts", Play creatures : goal":40,"locKey":"Quests / Quest_Creature_Commander", Play Creatures
        //    if (newQuest.LocKey.IndexOf("Creature") != -1)
        //    {
        //        newQuest.White = true;
        //        newQuest.Red = true;
        //        newQuest.Blue = true;
        //        newQuest.Black = true;
        //        newQuest.Green = true;
        //        newQuest.ChestDescription.Quantity = "501";
        //        return;
        //    }

        //    //Attack
        //    if (newQuest.LocKey.IndexOf("Raiding") != -1 || newQuest.LocKey.IndexOf("Almighty") != -1)
        //    {
        //        newQuest.Shift = true;
        //        newQuest.White = true;
        //        newQuest.Red = true;
        //        newQuest.Blue = true;
        //        newQuest.Black = true;
        //        newQuest.Green = true;
        //        return;
        //    }
        //}

    }


}