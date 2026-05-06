using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;
using System.Diagnostics;

namespace Magic
{


    internal class Player
    {
        private static string loginUserFileName = @"NextPlayer1.txt";
        private static string FilePathLastUser => Path.Combine(ConfigReader.Current.GetLogFilesPath(), "lastUser.txt");
        public static void WriteLastUser(int userId)
        {
            if (userId == -1)
                return;

            File.WriteAllText(FilePathLastUser, userId.ToString());
        }

        public static void TransferLastUserIDToUserlist(string _filePathToUserList = @"NextPlayer1.txt")
        {
            try
            {                
                int UserID = int.Parse(File.ReadAllLines(FilePathLastUser)[0]);
                Player.InsertPlayerInLoginList(UserID, _filePathToUserList);
            }
            catch
            {

            }


        }
        public static void InsertPlayerInLoginList(int userID, string filePathToUserList)
        {
            var playerIDLines = File.ReadAllLines(filePathToUserList);
            string[] insertedID = new string[playerIDLines.Length + 1];
            insertedID[0] = userID.ToString();

            for (int i = 0; i < playerIDLines.Length; i++)
            {
                insertedID[i + 1] = playerIDLines[i];

            }
            File.WriteAllLines(filePathToUserList, insertedID, Encoding.UTF8);
        }

        public static int GetNextPlayer(string fileName = @"NextPlayer1.txt")
        {
            var playerlines = File.ReadAllLines(fileName);
            int first = -1;
            try
            {
                first = int.Parse(playerlines[0]);
            }
            catch
            {
                return -1;
            }
            List<string> list = new List<string>();
            for (int i = 1; i < playerlines.Length; i++)
                list.Add(playerlines[i]);

            File.WriteAllLines(fileName, list.ToArray(), Encoding.UTF8);

            return first;

        }

      


        //string DateForQuest;
        private string name;
        public bool White;
        public bool Black;
        public bool Blue;
        public bool Green;
        public bool Red;
        public Json.QuestCollection QuestCollection;
        public Json.InventoryInfo InventoryInfo;
        


        public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public Player()
		{
		
		}

        public int ActiveQuests()
        {
            int activeQuests = 0;
            for (int i = 0; i < QuestCollection.Quests.Count; i++)
            {
                if (QuestCollection.Quests[i].Shift == false)
                    activeQuests++;
            }
            return activeQuests;
        }

        public int ColorValue(bool whitePar, bool bluePar, bool blackPar, bool redPar, bool greenPar)
        {
            int colorValue = 0;

            for (int i = 0; i < QuestCollection.Quests.Count; i++)
            {
                if (QuestCollection.Quests[i].White && whitePar)
                {
                    colorValue += (int)QuestCollection.Quests[i].ChestDescription.Quantity;
                    colorValue += QuestCollection.Quests[i].intEndingProgress;
                    continue;
                }
                if (QuestCollection.Quests[i].Black && blackPar)
                {
                    colorValue += (int)QuestCollection.Quests[i].ChestDescription.Quantity;
                    colorValue += QuestCollection.Quests[i].intEndingProgress;
                    continue;
                }
                if (QuestCollection.Quests[i].Blue&& bluePar)
                {
                    colorValue += (int)QuestCollection.Quests[i].ChestDescription.Quantity;
                    colorValue += QuestCollection.Quests[i].intEndingProgress;
                    continue;
                }
                if (QuestCollection.Quests[i].Red && redPar)
                {
                    colorValue += (int)QuestCollection.Quests[i].ChestDescription.Quantity;
                    colorValue += QuestCollection.Quests[i].intEndingProgress;
                    continue;
                }
                if (QuestCollection.Quests[i].Green && greenPar)
                {
                    colorValue += (int)QuestCollection.Quests[i].ChestDescription.Quantity;
                    colorValue += QuestCollection.Quests[i].intEndingProgress;
                    continue;
                }
            }
            return colorValue;
        }


        public string ReturnQuestCollectionString()
		{
			string result = "";
			if (QuestCollection == null)
				return "";
            string progressResult = "";
            string colorResult = "";

			for (int i = 0; i < QuestCollection.Quests.Count; i++)
			{
                string lockey = QuestCollection.Quests[i].LocKey.Substring(13);
                string goal = QuestCollection.Quests[i].Goal.ToString();

                string? progress = QuestCollection.Quests[i].intEndingProgress.ToString();

                string colors = "";

                if (QuestCollection.Quests[i].Shift)
                    colors += "(SHIFT)";
                else
                {
                    colors += "(";
                    if (QuestCollection.Quests[i].White)
                        colors += "W/";
                    if (QuestCollection.Quests[i].Blue)
                        colors += "U/";
                    if (QuestCollection.Quests[i].Black)
                        colors += "B/";
                    if (QuestCollection.Quests[i].Red)
                        colors += "R/";
                    if (QuestCollection.Quests[i].Green)
                        colors += "G/";
                    colors = colors.Substring(0,colors.Length-1) + ")";
                }

                progressResult += string.Format("({0}/{1})", progress, goal); ;
                colorResult += colors;
            }

            //result += string.Format("({0}/{1}) {2}-", progress, goal, colors);
            result += string.Format("{0} - {1}", progressResult, colorResult);

            return result;
		}

	}
}
