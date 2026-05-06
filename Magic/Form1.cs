using System.Threading;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using Microsoft.VisualBasic.ApplicationServices;
using System.Runtime.InteropServices;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;
using ScreenshotHelper;
using System.Text;
using System.Linq;
using Magic.Json;
using System.Collections.Generic;
using Microsoft.VisualBasic.Logging;
using System.Numerics;
using static System.Windows.Forms.LinkLabel;

namespace Magic
{
    public partial class Form1 : Form
    {
        ParseLogfile parseLogFile;
        Player player = new Player();
        LookAndClick.ScreenStates currentLocation = LookAndClick.ScreenStates.JUST_STARTED;
        LookAndClick.ScreenStates lastLocation = LookAndClick.ScreenStates.JUST_STARTED;
        string configLocation = @"config\config.txt";
        ConfigReader configReader;
        int maxGameTimeInSeconds = 85;
        int maxQuestsLeft = 2;

        public Form1()
        {

            InitializeComponent();
            player = new Player();
            parseLogFile = new ParseLogfile();
            configReader = new ConfigReader(configLocation);
            configReader.LoadConfigValues();
        }



        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            Thread.Sleep(200);
            MTGArena.GetMagicWindowInFocus();
            Thread.Sleep(200);

            for (int i = 0; i < int.Parse(TxtDelete.Text); i++)
            {
                DoLeftMouseClick(400, 200, 482, 433);
                DoLeftMouseClick(400, 200, 853, 1000);
                DoLeftMouseClick(300, 200, 1147, 625);
            }

            Create5Decks();

            Thread.Sleep(2000);
            ClickHomeButton();
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            int userNumber = int.Parse(TxtLogin.Text);
            CloseMagicOpenAndLogin(userNumber);
            TxtLogin.Text = ((int)userNumber + 1).ToString();
        }

        /// <summary>
        /// Login and Change Quest
        /// </summary>
        /// <param name="AccountNumber">The MTGX number</param>
        /// <param name="questNumber">The quest that needs change, 0 doesnt change anything, 3 2 1 in that order on the screen</param>
        private void LoginAndChangeQuest(int AccountNumber, int questNumber = 0)
        {
            LoginFromStartScreenToStartScreenAndLookForPlaybuttonOnHomescreen(AccountNumber);

            if (questNumber != 0)
            {
                TakeScreenShotOfScreen("BeforeQuestChange", AccountNumber);
                Thread.Sleep(300);
                ChangeQuest();
                Thread.Sleep(2000);
                TakeScreenShotOfScreen("AfterQuestChange", AccountNumber);
            }
            else
                TakeScreenShotOfScreen("BeforeQuestChange", AccountNumber);




        }

        private void TakeScreenShotOfScreen(string folder, int playerNumber)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            ScreenshotHelper.ScreenshotHelper.SaveFullScreenShot(folder + "\\" + playerNumber + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bmp");
            CopyLogFiles(playerNumber);
        }

        private bool ChangeQuest()
        {
            ReadLogFileAndSetColorsOnPlayer();

            int questNumber = FindWhatQuestToChange();

            int position = 0;
            if (questNumber == 0)
                position = player.QuestCollection.Quests.Count;

            if (questNumber == 1)
                position = player.QuestCollection.Quests.Count - 1;

            if (questNumber == 2)
                position = player.QuestCollection.Quests.Count - 2;

            int x = 0;

            switch (position)
            {
                case 0:
                    return false;
                case 1:
                    x = 830;
                    break;
                case 2:
                    x = 500;
                    break;
                case 3:
                    x = 210;
                    break;
                default:
                    break;
            }

            if (questNumber != -1)
            {
                //Click On the Quest to be changed
                DoLeftMouseClick(500, 1500, x, 880);

                //Click on Confirmation               
                DoLeftMouseClick(500, 1500, 1120, 630);

                //Click on profile
                DoLeftMouseClick(0, 5000, 220, 40);


                //Click back to Home
                DoLeftMouseClick(0, 1000, 100, 40);

                LookAndClick.WaitForPlayButtonOnHomeScreen(false);

                ReadLogFileAndSetColorsOnPlayer();
                return true;
            }
            return false;
        }

        private int FindWhatQuestToChange()
        {
            int questNumber = -1;

            for (int i = 0; i < player.QuestCollection.Quests.Count; i++)
            {
                var current = player.QuestCollection.Quests[i];
                if (current.Shift)
                {
                    return i;
                }

            }

            if (player.QuestCollection.Quests.Count == 1)
            {
                var current = player.QuestCollection.Quests[0];
                if (current.ChestDescription.Quantity < 510)
                {
                    questNumber = 0;
                }
            }

            // 2 quests
            if (player.QuestCollection.Quests.Count == 2)
            {
                Quest q0 = player.QuestCollection.Quests[0];
                Quest q1 = player.QuestCollection.Quests[1];

                if (q0.White && q1.White)
                    return questNumber;
                if (q0.Black && q1.Black)
                    return questNumber;
                if (q0.Green && q1.Green)
                    return questNumber;
                if (q0.Red && q1.Red)
                    return questNumber;
                if (q0.Blue && q1.Blue)
                    return questNumber;

                if (q0.ChestDescription.Quantity < 510)
                    return 0;
                if (q1.ChestDescription.Quantity < 510)
                    return 1;
            }



            // 3 quests
            if (player.QuestCollection.Quests.Count == 3)
            {
                Quest q0 = player.QuestCollection.Quests[0];
                Quest q1 = player.QuestCollection.Quests[1];
                Quest q2 = player.QuestCollection.Quests[2];


                int? w = q0.White ? q0.ChestDescription.Quantity : 0;
                w += q1.White ? q1.ChestDescription.Quantity : 0;
                w += q2.White ? q2.ChestDescription.Quantity : 0;

                int? b = q0.Black ? q0.ChestDescription.Quantity : 0;
                b += q1.Black ? q1.ChestDescription.Quantity : 0;
                b += q2.Black ? q2.ChestDescription.Quantity : 0;

                int? g = q0.Green ? q0.ChestDescription.Quantity : 0;
                g += q1.Green ? q1.ChestDescription.Quantity : 0;
                g += q2.Green ? q2.ChestDescription.Quantity : 0;

                int? r = q0.Red ? q0.ChestDescription.Quantity : 0;
                r += q1.Red ? q1.ChestDescription.Quantity : 0;
                r += q2.Red ? q2.ChestDescription.Quantity : 0;

                int? u = q0.Blue ? q0.ChestDescription.Quantity : 0;
                u += q1.Blue ? q1.ChestDescription.Quantity : 0;
                u += q2.Blue ? q2.ChestDescription.Quantity : 0;

                int? maxValue = w;
                string maxColor = "w";

                if (r > maxValue)
                {
                    maxValue = r;
                    maxColor = "r";
                }
                if (g > maxValue)
                {
                    maxValue = g;
                    maxColor = "g";
                }
                if (u > maxValue)
                {
                    maxValue = u;
                    maxColor = "u";
                }
                if (b > maxValue)
                {
                    maxValue = b;
                    maxColor = "b";
                }


                Quest[] questsCompare = new Quest[] { q0, q1, q2 };

                switch (maxColor)
                {
                    case "w":
                        for (int i = 0; i < 3; i++)
                        {
                            if (!questsCompare[i].White)
                                if (questsCompare[i].ChestDescription.Quantity < 510)
                                    return i;
                        }
                        break;
                    case "u":
                        for (int i = 0; i < 3; i++)
                        {
                            if (!questsCompare[i].Blue)
                                if (questsCompare[i].ChestDescription.Quantity < 510)
                                    return i;
                        }
                        break;
                    case "b":
                        for (int i = 0; i < 3; i++)
                        {
                            if (!questsCompare[i].Black)
                                if (questsCompare[i].ChestDescription.Quantity < 510)
                                    return i;
                        }
                        break;
                    case "g":
                        for (int i = 0; i < 3; i++)
                        {
                            if (!questsCompare[i].Green)
                                if (questsCompare[i].ChestDescription.Quantity < 510)
                                    return i;
                        }
                        break;
                    case "r":
                        for (int i = 0; i < 3; i++)
                        {
                            if (!questsCompare[i].Red)
                                if (questsCompare[i].ChestDescription.Quantity < 510)
                                    return i;
                        }
                        break;
                    default:
                        break;
                }
            }
            return questNumber;
        }

        private void DoLeftMouseClick(int waitBefore, int waitAfter, int x = -1, int y = -1)
        {
            var currentPoint = Cursor.Position;
            if (currentPoint.X > 1917 && currentPoint.Y > 1077)
            {
                MessageBox.Show("Paused");
                Thread.Sleep(1000);
                MTGArena.GetMagicWindowInFocus();
                Thread.Sleep(1000);
            }

            MTGArena.GetMagicWindowInFocus();
            if (y != -1)
            {
                Cursor.Position = new Point(x, y);

            }
            Thread.Sleep(waitBefore);
            MouseOperations.MouseOperations.DoLeftMouseClick();
            Thread.Sleep(waitAfter);
        }



        private bool LoginFromStartScreenToStartScreenAndLookForPlaybuttonOnHomescreen(int number, bool LogoutFirst = true)
        {
            MTGArena.GetMagicWindowInFocus();

            //Press Settings Button on Mainscreen (Mgt Arena and Log out)
            if (LogoutFirst)
            {
                //MakeSureCutScreenesArePassedOnHomeScreen();
                LogoutFromHomeScreen();
            }

            if (!Login(number))
                return false;

            int counter = 0;
            Stopwatch timeForLogin = new Stopwatch();
            timeForLogin.Start();

            //Check for everything that can happen on Startscreen before Playbutton is visible
            while (true)
            {
                if (LookAndClick.IsPlayButtonOnHomeScreen())
                    counter++;
                else
                    counter = 0;

                if (counter > 5)
                    break;

                if (LookAndClick.MonstrousRageBanned())
                { 
                    DoLeftMouseClick(500, 1000, 1000, 940);
                    for (int i = 0; i < 4; i++)
                        DoLeftMouseClick(200, 200, 1910, 500);
                    DoLeftMouseClick(500, 1000, 1740, 1010);
                    for (int i = 0; i < 4; i++)
                        DoLeftMouseClick(200, 200, 1910, 500);
                }

                if (LookAndClick.LeyLineBanned())
                {
                    for (int i = 0; i < 4; i++)
                        DoLeftMouseClick(500, 1000, 956, 956);
                }

                if (LookAndClick.DuskMourneFirstLogin())
                {
                    //Click Ok to banned cards 27 08 2024
                    DoLeftMouseClick(500, 1000, 1740, 1000);
                    Thread.Sleep(500);
                }

                if (LookAndClick.LoginScreenBannedCards_27082024())
                {
                    //Click Ok to banned cards 27 08 2024
                    DoLeftMouseClick(200, 2000, 960, 926);
                    Thread.Sleep(500);
                }

                //Timeout????
                if (timeForLogin.ElapsedMilliseconds > 60000)
                    return false;

                Thread.Sleep(500);
            }

            return true;
        }







        private bool Login(int number)
        {
            while (!LookAndClick.LookForLoginScreen())
                Thread.Sleep(100);

            string email = configReader.GetLoginEmail(number);
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(configReader.LoginPassword))
            {
                MessageBox.Show("Login mangler i config\\config.txt. Udfyld LoginEmailAccountZero, LoginEmailTemplate og LoginPassword.");
                return false;
            }

            //Thread.Sleep(500); if (Cursor.Position.Y < 5) return;
            DoLeftMouseClick(200, 200, 1212, 643);
            DoLeftMouseClick(200, 200, 1123, 648);

            SendKeys.Send(EscapeForSendKeys(email));
            SendKeys.Flush();
            DoLeftMouseClick(500, 500, 819, 713);
            SendKeys.Send(EscapeForSendKeys(configReader.LoginPassword));
            SendKeys.Flush();

            DoLeftMouseClick(500, 200, 955, 886);

            Thread.Sleep(200); if (Cursor.Position.Y < 5) return false;
            Cursor.Position = new Point(500, 500);
            return true;
        }

        private static string EscapeForSendKeys(string text)
        {
            StringBuilder result = new StringBuilder();

            foreach (char current in text)
            {
                switch (current)
                {
                    case '+':
                    case '^':
                    case '%':
                    case '~':
                    case '(':
                    case ')':
                    case '[':
                    case ']':
                        result.Append('{').Append(current).Append('}');
                        break;
                    case '{':
                        result.Append("{{}");
                        break;
                    case '}':
                        result.Append("{}}");
                        break;
                    default:
                        result.Append(current);
                        break;
                }
            }

            return result.ToString();
        }
        private void ClickSecureSpotOnMainScreen10TimesIn5Seconds()
        {
            for (int i = 0; i < 10; i++)
            {
                DoLeftMouseClick(900, 100, 1780, 537);
            }

        }

        private void LogoutFromHomeScreen()
        {


            //If there is a button
            DoLeftMouseClick(300, 100, 1722, 1005);

            //Press Settings Button
            DoLeftMouseClick(300, 100, 1780, 37);
            Thread.Sleep(500); if (Cursor.Position.Y < 5) return;

            //Click on Logout
            DoLeftMouseClick(300, 100, 959, 663);
            Thread.Sleep(500); if (Cursor.Position.Y < 5) return;

            //Confirm
            DoLeftMouseClick(300, 100, 1092, 622);
            Thread.Sleep(500); if (Cursor.Position.Y < 5) return;

        }

        private void MakeSureCutScreenesArePassedOnHomeScreen()
        {
            //Press Secure Place (non interactive) on main screen //But why
            ClickSecureSpotOnMainScreen10TimesIn5Seconds();
        }


        private void BtnTest_Click(object sender, EventArgs e)
        {
            maxGameTimeInSeconds = 100;
            Thread.Sleep(500);
            MTGArena.GetMagicWindowInFocus();
            StartGameLoop();

        }
        int questLeft = 0;
        private void StartGameLoop()
        {
            Thread t = new Thread(new ThreadStart(GameLoop));
            t.IsBackground = true;
            t.Start();
        }

        int NextLogin = 0;
        int GameLoopsPlayed = 0;

        private void GameLoop()
        {
            GameLoopsPlayed = 0;
            bool justBegun = true;
            Stopwatch lastTimeBotPlayedCard = new Stopwatch();
            Stopwatch lastActionByTheBot = new Stopwatch();
            Stopwatch gameLasted = new Stopwatch();
            gameLasted.Start();
            lastActionByTheBot.Start();
            lastTimeBotPlayedCard.Start();
            // NextLogin = int.Parse(TxtLogin.Text);


            int numberOfTimeDidntFindACard = 0;
            while (true)
            {
                if (Cursor.Position.Y < 5)
                    break;

                //if (IsQuestDone())
                //    return;

                if (justBegun)
                {
                    gameLasted.Restart();
                    lastActionByTheBot.Restart();
                    lastTimeBotPlayedCard.Restart();
                    justBegun = false;
                }
                try
                {
                    string result = "";

                    #region Search and Click Mulligan
                    if (CheckAreaForColor(new Point(1126, 875), new Size(3, 5), 252, 255, 252, 255, 252, 255) > 10)
                    {
                        DoLeftMouseClick(100, 1000, 1126, 875);
                        justBegun = true;
                    }



                    #endregion

                    #region Search for Top button in game
                    var croppedPictureTop = ScreenshotHelper.ScreenshotHelper.GetCroppedScreenShot2(new Point(1757, 882), new Size(50, 3));

                    int countWhiteTop = 0;
                    for (int x = 0; x < croppedPictureTop.Width; x++)
                        for (int y = 0; y < croppedPictureTop.Height; y++)
                        {
                            var c = croppedPictureTop.GetPixel(x, y);
                            if (c.R >= 253 && c.G >= 253 && c.B >= 253)
                                countWhiteTop++;
                        }
                    #endregion

                    #region Search for Bottom button in game
                    var croppedPictureBottom = ScreenshotHelper.ScreenshotHelper.GetCroppedScreenShot2(new Point(1757, 947), new Size(50, 3));
                    int countWhiteBottom = 0;
                    for (int x = 0; x < croppedPictureBottom.Width; x++)
                        for (int y = 0; y < croppedPictureBottom.Height; y++)
                        {
                            var c = croppedPictureBottom.GetPixel(x, y);
                            if (c.R >= 253 && c.G >= 253 && c.B >= 253)
                                countWhiteBottom++;
                        }
                    #endregion

                    #region Search for cards with blue border
                    bool noCardFound = true;

                    int checkLineHeightY = 1078;
                    int startLineHeightX = 220;
                    bool playedACard = false;
                    try
                    {
                        var croppedCardLine = ScreenshotHelper.ScreenshotHelper.GetCroppedScreenShot2(new Point(startLineHeightX, checkLineHeightY), new Size(1470, 1));
                        int counter = 0;
                        for (int x = 0; x < croppedCardLine.Width && noCardFound; x++)
                            for (int y = 0; y < croppedCardLine.Height && noCardFound; y++)
                            {
                                var c = croppedCardLine.GetPixel(x, y);
                                if (c.R > 2 && c.R < 40 && c.G > 250 && c.B > 250)
                                {
                                    counter++;
                                    Thread.Sleep(100);
                                    DoubleClickLeftMouseButton(startLineHeightX + x + 15, checkLineHeightY);
                                    Thread.Sleep(20);
                                    Cursor.Position = new Point(990, 500);
                                    lastTimeBotPlayedCard.Restart();
                                    lastActionByTheBot.Restart();
                                    noCardFound = false;
                                    numberOfTimeDidntFindACard = 0;
                                    playedACard = true;
                                    Thread.Sleep(500);
                                    break;
                                }

                                if (playedACard)
                                    break;
                            }
                        //result += "Line " + i + ": " + counter + Environment.NewLine;
                        GC.Collect();
                    }
                    catch { }
                    #endregion



                    if (countWhiteBottom > 15 && noCardFound)
                    {
                        numberOfTimeDidntFindACard++;
                        if (numberOfTimeDidntFindACard >= 2)
                        {
                            numberOfTimeDidntFindACard = 0;
                            DoLeftMouseClick(30, 50, 1850, 1030);

                            Cursor.Position = new Point(990, 500);
                            lastActionByTheBot.Restart();
                        }
                        else
                            Thread.Sleep(100);
                    }

                    #region Search for Defeat or Victory
                    //Concede if last action was more than 20 seconds ago, or last card was played more than 30 seconds ago
                    if (lastActionByTheBot.ElapsedMilliseconds > 20000 || lastTimeBotPlayedCard.ElapsedMilliseconds > 30000 || gameLasted.ElapsedMilliseconds > maxGameTimeInSeconds * 1000)
                    {
                        lastActionByTheBot.Restart();
                        lastTimeBotPlayedCard.Restart();
                        gameLasted.Restart();
                        ConcedeMatch();

                        if (ContinueSolvingQuests() == false)
                        {
                            ClickSecureSpotOnMainScreen10TimesIn5Seconds();
                            return;
                        }

                        justBegun = true;
                        if (StartNewGame() == false)
                        {
                            ClickSecureSpotOnMainScreen10TimesIn5Seconds();
                            return;
                        }
                        continue;
                    }


                    var croppedPictureDefeatOrVictory = ScreenshotHelper.ScreenshotHelper.GetCroppedScreenShot2(new Point(981, 533), new Size(7, 10));
                    int counterDefeatOrVictory = 0;
                    for (int x = 0; x < croppedPictureDefeatOrVictory.Width; x++)
                        for (int y = 0; y < croppedPictureDefeatOrVictory.Height; y++)
                        {
                            var c = croppedPictureDefeatOrVictory.GetPixel(x, y);
                            if (c.R >= 253 && c.G >= 253 && c.B >= 253)
                                counterDefeatOrVictory++;
                        }
                    if (counterDefeatOrVictory > 50 || IsQuestDone())
                    {

                        justBegun = true;
                        ClickVictoryOrDefeatScreen();
                        ConcedeMatch();

                        if (ContinueSolvingQuests() == false)
                        {
                            ClickSecureSpotOnMainScreen10TimesIn5Seconds();
                            return;
                        }
                        if (StartNewGame() == false)
                        {
                            ClickSecureSpotOnMainScreen10TimesIn5Seconds();
                            return;
                        }
                    }
                    #endregion






                    result = "Top: " + countWhiteTop + Environment.NewLine + "Bottom: " + countWhiteBottom + Environment.NewLine + result;

                    WriteTextBox(TxtStatus, result);
                    Thread.Sleep(500);
                }
                catch { }
            }

        }



        private void ConcedeMatch()
        {
            DoLeftMouseClick(500, 500, 1882, 46);
            DoLeftMouseClick(500, 500, 970, 634);
            Thread.Sleep(1000);
            DoLeftMouseClick(500, 500, 1882, 46);
            DoLeftMouseClick(500, 500, 970, 634);
            //press the end screen to end
            ClickSecureSpotOnMainScreen10TimesIn5Seconds();

        }

        private void ReadLogfileAndSetColors()
        {

            parseLogFile.SetUserName(player);
            parseLogFile.GetQuestsForCurrentPlayerJsonStyle(player);
            // parseLogFile.GetQuestsForCurrentPlayer(player);
        }

        private void ClickVictoryOrDefeatScreen()
        {
            for (int i = 0; i < 5; i++)
                DoLeftMouseClick(100, 100, 980, 500);
        }

        private bool IsQuestDone()
        {
            ReadLogfileAndSetColors();

            return player.QuestCollection.Quests.Count < maxQuestsLeft;
        }

        private bool ContinueSolvingQuests()
        {
            ReadLogfileAndSetColors();
            bool maxQuest = player.QuestCollection.Quests.Count == 3;
            int colorValue = player.ColorValue(deckColorWhite, deckColorBlue, deckColorBlack, deckColorRed, deckColorGreen);
            bool multipleQuests = colorValue > 900;

            if (maxQuestsLeft == 0 || maxQuestsLeft == 1)
                return colorValue > 400;


            return maxQuest || multipleQuests;
        }


        private bool StartNewGame()
        {
            LogFile.WriteLog("Form1.StartNewGame: Before GameLoop");

            //Count played games one up
            GameLoopsPlayed++;

            if (IsQuestDone())
                return false;


            //Leder efter playknappen på hovedskærmen
            LookAndClick.WaitForPlayButtonOnHomeScreen(true);


            for (int i = 0; i < 5; i++)
            {
                if (LookAndClick.running == false)
                    return false;
                DoLeftMouseClick(1000, 100);
            }

            //LogFile.WriteLog("Form1.Search and Click Mulligan: Before GameLoop");
            #region Search and Click Mulligan
            while (true)
            {
                if (CheckAreaForColor(new Point(1126, 875), new Size(3, 5), 252, 255, 252, 255, 252, 255) > 10)
                {
                    DoLeftMouseClick(100, 1000, 1126, 875);
                    break;
                }
                else
                    Thread.Sleep(500);
            }
            #endregion

            return true;


        }

        private int CheckAreaForColor(Point UpperRightCorner, Size size, int redMin, int redMax, int greenMin, int greenMax, int blueMin, int blueMax)
        {
            int countToleratedColor = 0;
            try
            {

                var croppedPicture = ScreenshotHelper.ScreenshotHelper.GetCroppedScreenShot2(UpperRightCorner, size);
                for (int x = 0; x < croppedPicture.Width; x++)
                    for (int y = 0; y < croppedPicture.Height; y++)
                    {
                        var c = croppedPicture.GetPixel(x, y);
                        if (c.R >= redMin && c.R <= redMax && c.G >= greenMin && c.G <= greenMax && c.B >= blueMin && c.B <= blueMax)
                            countToleratedColor++;
                    }
                return countToleratedColor;
            }
            catch
            {
                return 0;
            }
        }

        private void DoubleClickLeftMouseButton(int x, int y)
        {
            DoLeftMouseClick(50, 0, x, y);
            DoLeftMouseClick(50, 0, x, y);
        }

        private void WriteTextBox(TextBox tboxPar, string insertTextPar)
        {
            if (tboxPar.InvokeRequired)
            {
                tboxPar.BeginInvoke((MethodInvoker)delegate () { tboxPar.Text = insertTextPar; ; });
                //  label1.Invalidate();
            }
            else
            {
                tboxPar.Text = insertTextPar;
                // label1.Invalidate();
            }

        }

        private void WriteLabel(Label lblPar, string insertTextPar)
        {
            if (lblPar.InvokeRequired)
            {
                lblPar.BeginInvoke((MethodInvoker)delegate () { lblPar.Text = insertTextPar; ; });
                //  label1.Invalidate();
            }
            else
            {
                lblPar.Text += insertTextPar;
                // label1.Invalidate();
            }

        }

        private void SetCheckBox(CheckBox checkBoxPar, bool checkedOrNot)
        {
            if (checkBoxPar.InvokeRequired)
            {
                checkBoxPar.BeginInvoke((MethodInvoker)delegate () { checkBoxPar.Checked = checkedOrNot; ; });
                //  label1.Invalidate();
            }
            else
            {
                checkBoxPar.Checked = checkedOrNot;
                // label1.Invalidate();
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
            Thread.Sleep(500);
            MTGArena.GetMagicWindowInFocus();
            if (ChkReadLogfileFirst.Checked)
            {
                //ReadLogFileAndSetColorsOnPlayer();
                parseLogFile.GetQuestsForCurrentPlayerJsonStyle(player);


            }
            Thread.Sleep(500);


            //Remove this when all have this deck
            Clipboard.SetText(ImportDecks.AssembleDeck(player.White, player.Blue, player.Black, player.Red, player.Green));

            CreateDeck();

            Thread.Sleep(500);
            ClickHomeButton();
            LookAndClick.WaitForPlayButtonOnHomeScreen(false);
            Thread.Sleep(2000);

            StartFirstMatch(false);
        }

        private void StartFirstMatch(bool start = true)
        {
            //PressPlay
            LookAndClick.WaitForPlayButtonOnHomeScreen(true);

            //Click Find Match
            DoLeftMouseClick(1500, 500, 1733, 129);
            DoLeftMouseClick(1000, 500, 1733, 286);

            //Click Bot
            if (LookAndClick.IsBotMatchAvailable() == false)
                DoLeftMouseClick(500, 500, 1780, 400);

            DoLeftMouseClick(500, 500, 1692, 762);

            SelectFirstDeck();

            //Click Play
            if (start)
            {
                for (int i = 0; i < 5; i++)
                {
                    DoLeftMouseClick(500, 200, 1733, 1012);
                }
            }
        }

        private void SelectFirstDeck()
        {
            //Click My Decks
            if (LookAndClick.IsMyDecksUnfolded())
                DoLeftMouseClick(500, 500, 255, 350);

            //Click First Deck
            DoLeftMouseClick(500, 500, 450, 550);
        }

        private void ClickHomeButton()
        {
            //Press Home
            DoLeftMouseClick(100, 2000, 109, 40);
        }

        private void BtnLoginAndPlay_Click(object sender, EventArgs e)
        {
            Thread.Sleep(200);
            MTGArena.GetMagicWindowInFocus();
            Thread.Sleep(200);

            LoginFromStartScreenToStartScreenAndLookForPlaybuttonOnHomescreen(int.Parse(TxtLogin.Text));

            //TODO: Check if this got implemented and delete commented outted line
            //ImportDecks.ImportAllDecks(ChkWhite.Checked, ChkBlue.Checked, ChkBlack.Checked, ChkRed.Checked, ChkGreen.Checked);
            CreateDeckFromColors();

            Thread t = new Thread(new ThreadStart(GameLoop));
            t.IsBackground = true;
            t.Start();
        }

        private void BtnLoginScreenshot_Click(object sender, EventArgs e)
        {
            Thread.Sleep(500);
            MTGArena.GetMagicWindowInFocus();
            int start = int.Parse(TxtFirstPlayer.Text);
            int last = int.Parse(TxtLastPlayer.Text);

            for (int i = start; i <= last; i++)
            {
                ////delete the next two lines
                //LoginFromStartScreenToStartScreen(i);
                //continue;
                if (Cursor.Position.Y < 5) return;
                LoginAndChangeQuest(i);
                //CopyLogFiles(i);
            }
        }




        Dictionary<string, string> mtgDictionary;
        private void MakeDirectoryOfAllAccounts()
        {
            mtgDictionary = new Dictionary<string, string>()
{
                { "Dreagor#97477", "MTGMain" },
                { "Shiartu#95016", "MTG1" },
                { "Dreagor#39657", "MTG2" },
                { "Shiartu#76359", "MTG3" },
                { "Shiartu#93871", "MTG4" },
                { "Shiartu#29842", "MTG5" },
                { "Shiartu#70170", "MTG6" },
                { "Shiartu#42958", "MTG7" },
                { "Shiatu#65934", "MTG8" },
                { "Shiartu#15897", "MTG9" },
                { "Shiartu#95528", "MTG10" },
                { "Shiartu#19904", "MTG11" },
                { "Shiartu#69055", "MTG12" },
                { "Shiartu#02977", "MTG13" },
                { "Shiartu#62845", "MTG14" },
                { "Shiartu#55546", "MTG15" },
                { "Shiartu#37050", "MTG16" },
                { "Shiartu#67091", "MTG17" },
                { "Shiartu#46681", "MTG18" },
                { "Shiartu#99279", "MTG19" },
                { "Shiartu#98200", "MTG20" },
                { "Shiartu#48117", "MTG21" },
                { "Flying Brian#38268", "MTG22" },
                { "Broder Salsa#70833", "MTG23" },
                { "Brawler Brian#40587", "MTG24" },
                { "Franke#42761", "MTG25" },
                { "Trixia#10470", "MTG26" },
                { "Bingo Tina#72524", "MTG27" },
                { "Gilda the explorer#65036", "MTG28" },
                { "Frida the Barbarian#37152", "MTG29" },
                { "Musen#16370", "MTG30" },
                { "Server Disconnect#48065", "MTG31" },
                { "Ytma the Destroyer#45715", "MTG32" },
                { "Henry Danger#81326", "MTG33" },
                { "Vile Snacker#17077", "MTG34" },
                { "I Might Win#50020", "MTG35" },
                { "The Barrier of Grace#62943", "MTG36" },
                { "The Majestic Rhino#74605", "MTG37" },
                { "Captain Old Condor#98510", "MTG38" },
                { "Elron Harald the Wonder#52800", "MTG39" },
                { "Condor the Wicket#22922", "MTG40" },
                { "Elron Harald the Baloon#80746", "MTG41" },
                { "Flemse#79229", "MTG42" },
                { "The Wonder Electron#97126", "MTG43" },
                { "Card Splasher#93441", "MTG44" },
                { "Goblin Shredder#32594", "MTG45" },
                { "Bob#73397", "MTG46" },
                { "Heidi#76585", "MTG47" },
                { "Penflipper#87984", "MTG48" },
                { "Penspinner#80054", "MTG49" },
                { "Hackbard#58784", "MTG50" },
                { "Moose Gobler#06172", "MTG51" },
                { "Flashspinner#32899", "MTG52" },
                { "Funky Dawn#19886", "MTG53" },
                { "Hulk Smash#77777", "MTG54" },
                { "Fist of the North Star#36627", "MTG55" },
                { "Mostly Harmless#05803", "MTG56" },
                { "Don't Panic#51839", "MTG57" },
                { "Brass Brawler#47784", "MTG58" },
                { "Knitted Sweater#82868", "MTG59" },
                { "Dragon Lover#04007", "MTG60" },
                { "Totally Harmless#55555", "MTG61" },
                { "Carrington the Smirk#22755", "MTG62" },
                { "Terrell the Madlad#56511", "MTG63" },
                { "Dorsey Mad Eyes#69960", "MTG64" },
                { "Whitelaw Greed#84940", "MTG65" },
                { "Robbie the Silent#76377", "MTG66" },
                { "Three Fingered Mills#89390", "MTG67" },
                { "Smokey Welby#91856", "MTG68" },
                { "Viper Welby#52006", "MTG69" },
                { "Poison Antony#90265", "MTG70" },
                { "Silent Doyle#85424", "MTG71" },
                { "Alexis the Crackpot#35446", "MTG72" },
                { "Greedy Glenn#94117", "MTG73" },
                { "Aiden The Mad Dad#86077", "MTG74" },
                { "Kitt the Beast#99285", "MTG75" },
                { "Greedy Brett#69392", "MTG76" },
                { "Mad Tyler#99409", "MTG77" },
                { "Crazy Billy#19090", "MTG78" },
                { "Action Willy#64828", "MTG79" },
                { "Sharky#19146", "MTG80" }
            };


        }
        private void BtnTest_Click_1(object sender, EventArgs e)
        {
            MakeDirectoryOfAllAccounts();
            var files = Directory.GetFiles("cleanlogfiles");
            Player pl1 = new Player();
            string notPlayedLimited = "";
            string notPlayedConstructed = "";
            string result3 = "";
            string result2 = "";
            string result1 = "";
            string result0 = "";
            string loginOrder3 = "";
            string loginOrder2 = "";
            string loginOrder1 = "";
            string loginOrder0 = "";
            string strPremiereReady = "";
            string strReadyForBotDraft = "";
            string strNotReadyForAnything = "";
            string strWildCards = "";
            string strBLBOrbs = "";
            string QuestCountZeroLogin = "";

            int totalGold = 0;
            int primierReady = 0;
            int readyForBotDraft = 0;
            int int3Quests = 0;
            int int2Quests = 0;
            int int1Quests = 0;
            int int0Quests = 0;



            foreach (var file in files)
            {

                var lines = File.ReadAllLines(file);
                FindAndParseQuestLine(pl1, lines);



                if (pl1.QuestCollection != null)
                {
                    string name = file.Substring(file.IndexOf("\\") + 1).Replace(".log", "");
                    string mgtAndName = mtgDictionary[name] + ": " + name;

                    for (int i = 0; i < pl1.QuestCollection.Quests.Count; i++)
                    {
                        // parseLogFile.ParseQuestsNamesToColors(pl1.QuestCollection.Quests[i]);
                    }

                    string lineResult = mgtAndName + " : " + pl1.ReturnQuestCollectionString() + Environment.NewLine;
                    string loginOrderResult = mtgDictionary[name].Substring(3) + Environment.NewLine;
                    string goldLineResult = mgtAndName + "Gold: " + pl1.InventoryInfo.Gold + Environment.NewLine;

                    if (pl1.QuestCollection.Quests.Count > 1)
                        if (CheckIfTwoOfSameColorQuest(pl1.ReturnQuestCollectionString(), pl1.QuestCollection.Quests.Count))
                            if (loginOrderResult.IndexOf("Main") == -1)
                                QuestCountZeroLogin += mtgDictionary[name].Substring(3) + Environment.NewLine;


                    if (pl1.InventoryInfo.WildCardCommons > -1)
                    {
                        strWildCards += mgtAndName + "WildCards: (" + pl1.InventoryInfo.WildCardCommons + "/" + pl1.InventoryInfo.WildCardUnCommons + "/" + pl1.InventoryInfo.WildCardRares + "/" + pl1.InventoryInfo.WildCardMythics + ")" + Environment.NewLine;
                    }
                    if (pl1.InventoryInfo.BattlePass_BLB_Orb != 0)
                    {
                        strBLBOrbs += mgtAndName + "BLB Orbs: (" + pl1.InventoryInfo.BattlePass_BLB_Orb + ")" + Environment.NewLine;
                    }
                    totalGold += pl1.InventoryInfo.Gold;
                    if (pl1.InventoryInfo.Gold >= 10000)
                    {
                        primierReady += 1;
                        strPremiereReady += goldLineResult;
                    }
                    else if (pl1.InventoryInfo.Gold >= 5000)
                    {
                        readyForBotDraft += 1;
                        strReadyForBotDraft += goldLineResult;
                    }
                    else
                    {
                        strNotReadyForAnything += goldLineResult;
                    }


                    if (pl1.QuestCollection.Quests.Count == 3)
                    {
                        int3Quests++;
                        result3 += lineResult;
                        if (loginOrderResult.IndexOf("Main") == -1)
                            loginOrder3 += loginOrderResult;
                    }
                    if (pl1.QuestCollection.Quests.Count == 2)
                    {
                        int2Quests++;
                        result2 += lineResult;
                        if (loginOrderResult.IndexOf("Main") == -1)
                            loginOrder2 += loginOrderResult;
                    }
                    if (pl1.QuestCollection.Quests.Count == 1)
                    {
                        int1Quests++;
                        result1 += lineResult;
                        if (loginOrderResult.IndexOf("Main") == -1)
                            loginOrder1 += loginOrderResult;
                    }
                    if (pl1.QuestCollection.Quests.Count == 0)
                    {
                        int0Quests++;
                        result0 += lineResult;
                        if (loginOrderResult.IndexOf("Main") == -1)
                            loginOrder0 += loginOrderResult;
                    }

                    for (int i = lines.Length - 1; i > 0; i--)
                    {
                        if (lines[i].IndexOf("constructedSeasonOrdinal") != -1)
                        {
                            if (lines[i].IndexOf("constructedMatches") == -1)
                                notPlayedConstructed += mgtAndName + Environment.NewLine;
                            break;
                        }
                    }

                    for (int i = lines.Length - 1; i > 0; i--)
                    {
                        if (lines[i].IndexOf("constructedSeasonOrdinal") != -1)
                        {
                            if (lines[i].IndexOf("limitedMatches") != -1)
                                notPlayedLimited += mgtAndName + Environment.NewLine;
                            break;
                        }
                    }
                }


            }




            WriteTextBox(TxtStatus,
                "3 quests (" + int3Quests + "):" + Environment.NewLine + result3 + Environment.NewLine + Environment.NewLine +
                "2 quests (" + int2Quests + "):" + Environment.NewLine + result2 + Environment.NewLine + Environment.NewLine +
                "1 quests (" + int1Quests + "):" + Environment.NewLine + result1 + Environment.NewLine + Environment.NewLine +
                "0 quests (" + int0Quests + "):" + Environment.NewLine + result0 + Environment.NewLine + Environment.NewLine +
                "Total Gold:" + totalGold.ToString() + Environment.NewLine +
                "Primiere Draft Ready: " + primierReady.ToString() + Environment.NewLine +
                strPremiereReady + Environment.NewLine + Environment.NewLine +
                "Bot draft ready: " + readyForBotDraft.ToString() + Environment.NewLine +
                strReadyForBotDraft + Environment.NewLine + Environment.NewLine +
                "Not ready for draft: " + Environment.NewLine +
                strNotReadyForAnything + Environment.NewLine +
                "Not Played Ranked this season" + Environment.NewLine +
                notPlayedConstructed + Environment.NewLine + Environment.NewLine +
                "Played limited this season" + Environment.NewLine +
                notPlayedLimited + Environment.NewLine + Environment.NewLine +
                "Wildcards (Common/Uncommon/Rare/Mythic)" + Environment.NewLine +
                strWildCards + Environment.NewLine + Environment.NewLine +
                "Unspent BLB Orbs " + Environment.NewLine +
                strBLBOrbs

                );

            File.WriteAllText("loginOrder.txt", loginOrder3 + loginOrder2 + loginOrder1 + loginOrder0, Encoding.UTF8);
            File.WriteAllText("loginOrder0Quests.txt", QuestCountZeroLogin, Encoding.UTF8);

            return;

            //ReadLogFileAndSetColorsOnPlayer();
            //userNumber =  int.Parse(TxtFirstPlayer.Text);
            Thread threadCheckWhereWeAre = new Thread(new ThreadStart(WhereAreWe));
            threadCheckWhereWeAre.IsBackground = true;
            threadCheckWhereWeAre.Start();
            //WhereAreWe();
            //ScreenshotHelper.PixelTool f = new ScreenshotHelper.PixelTool();
            //f.Show();
        }

        private bool CheckIfTwoOfSameColorQuest(string questLinePar, int NumberOfQuests)
        {
            if (questLinePar.Count(f => f == 'W') == NumberOfQuests) return true;
            if (questLinePar.Count(f => f == 'B') == NumberOfQuests) return true;
            if (questLinePar.Count(f => f == 'U') == NumberOfQuests) return true;
            if (questLinePar.Count(f => f == 'R') == NumberOfQuests) return true;
            if (questLinePar.Count(f => f == 'G') == NumberOfQuests) return true;
            return false;
        }

        private void FindAndParseQuestLine(Player player, string[] linesPar, int earliestLineToRead = 0)
        {
            for (int i = linesPar.Length - 1; i >= earliestLineToRead; i--)
            {
                int lineIndexNumber = linesPar[i].IndexOf("quests\":[");
                if (lineIndexNumber != -1)
                {
                    player.QuestCollection = Magic.Json.JsonExample.ParseQuests(linesPar[i]);
                    //noQuests = true;
                    break;
                }
            }

            for (int i = linesPar.Length - 1; i >= earliestLineToRead; i--)
            {
                int lineIndexNumber = linesPar[i].IndexOf("{\"InventoryInfo\":");
                if (lineIndexNumber != -1)
                {
                    player.InventoryInfo = Magic.Json.JsonExample.ParseInventoryInfo(linesPar[i]);
                    //noQuests = true;
                    break;
                }
            }
        }

        private void WhereAreWe()
        {

            while (true)
            {

                Thread.Sleep(500);

                if (LookAndClick.BloomBurrowLoginFirstTime() &&
                    (currentLocation == LookAndClick.ScreenStates.JUST_STARTED || currentLocation == LookAndClick.ScreenStates.LOGIN_SCREEN))
                {
                    lastLocation = currentLocation;
                    currentLocation = LookAndClick.ScreenStates.BLOOMBURROW_FIRST_TIME;
                    WriteTextBox(TxtStatus, "On BloomBurrow Login");
                    ClickThrougToMainScreenFromBloomBurrowIntroScreen();
                    continue;
                }

                if (LookAndClick.LookForLoginScreen())
                {
                    lastLocation = currentLocation;
                    currentLocation = LookAndClick.ScreenStates.LOGIN_SCREEN;

                    WriteTextBox(TxtStatus, "Login screen");
                    //ClickThrougToMainScreenFromBloomBurrowIntroScreen();
                    continue;
                }


                if (LookAndClick.MainScreen())
                {
                    lastLocation = currentLocation;
                    currentLocation = LookAndClick.ScreenStates.MAIN_SCREEN;

                    WriteTextBox(TxtStatus, "On Mainscreen");
                    //Thread.Sleep(3500);
                    //ReadLogFileAndSetColorsOnPlayer();
                    //Thread.Sleep(3500);

                    //Thread.Sleep(500);
                    // MTGArena.GetMagicWindowInFocus();
                    //Thread.Sleep(500);
                    //LoginFromStartScreenToStartScreen(userNumber++);
                    //Thread.Sleep(500);
                    continue;
                }

                WriteTextBox(TxtStatus, "Could be anywhere");
            }
        }

        private void ClickThrougToMainScreenFromBloomBurrowIntroScreen()
        {
            DoLeftMouseClick(1000, 2000, 1735, 1020); //Get started
            DoLeftMouseClick(0, 1000, 256, 800); //Rotating cards
            DoLeftMouseClick(0, 5000, 968, 890); //Renewal Gift
            DoLeftMouseClick(0, 2000, 968, 890); //More
            DoLeftMouseClick(0, 4000, 968, 890); //More
            DoLeftMouseClick(0, 0, 100, 50); //Home

        }

        private void ReadLogFileAndSetColorsOnPlayer()
        {
            ReadLogfileAndSetColors();

            //Set Colors For the Deck to Play
            SetCheckBoxColors();


            string result = "";
            result = player.Name + Environment.NewLine;

            if (parseLogFile.noQuests)
            {
                result += "No Quests";
                return;
            }

            for (int i = 0; i < 3; i++)
            {
                result += parseLogFile.parsedQuests[i] + Environment.NewLine;
            }

            WriteTextBox(TxtStatus, result);

            if (parseLogFile.swapQuest)
            {
                parseLogFile.swapQuest = false;
                Thread.Sleep(100);
                MTGArena.GetMagicWindowInFocus();
                Thread.Sleep(100);
                //Click on the first Matching quest
                DoLeftMouseClick(100, 100, 280 + parseLogFile.swapQuestNumber * 300, 900);
                DoLeftMouseClick(500, 500, 1130, 630);

                if (ChkStartPlaying.Checked)
                {
                    DoLeftMouseClick(300, 5000, 220, 40);
                    DoLeftMouseClick(100, 5000, 100, 40);

                    Thread.Sleep(5000);
                    ReadLogfileAndSetColors();
                    Thread.Sleep(1000);

                    SetCheckBoxColors();
                    //Clipboard.SetText(ImportDecks.AssembleDeck(ChkWhite.Checked, ChkBlue.Checked, ChkBlack.Checked, ChkRed.Checked, ChkGreen.Checked));
                    Clipboard.SetText(ImportDecks.AssembleDeck(player.White, player.Blue, player.Black, player.Red, player.Green));

                }

            }
        }

        private void SetCheckBoxColors()
        {
            SetCheckBox(ChkBlack, player.Black);
            SetCheckBox(ChkWhite, player.White);
            SetCheckBox(ChkBlue, player.Blue);
            SetCheckBox(ChkRed, player.Red);
            SetCheckBox(ChkGreen, player.Green);

        }

        private void PromoCodeActivation(string promoCodePar)
        {

            //Click Promo Code
            DoLeftMouseClick(100, 100, 1734, 126);
            SendKeys.Send(promoCodePar + "{ENTER}");
            SendKeys.Flush();
            DoLeftMouseClick(7000, 500, 960, 638);
            DoLeftMouseClick(0, 1500, 1734, 1012);
        }

        private void BtnTakeScreenShot_Click(object sender, EventArgs e)
        {
            int userNumber = int.Parse(TxtLogin.Text);
            TxtLogin.Text = ((int)userNumber + 1).ToString();
            LoginFromStartScreenToStartScreenAndLookForPlaybuttonOnHomescreen(userNumber);
            //Press on Decks
            DoLeftMouseClick(500, 0, 322, 42);

        }

        private void BtnChangeQuests_Click(object sender, EventArgs e)
        {


        }



        private void BtnCreateAccount_Click(object sender, EventArgs e)
        {


        }

        private void CopyLogFiles(int AccountNumber)
        {
            string source = @"C:\Users\Tommy\AppData\LocalLow\Wizards Of The Coast\MTGA\Player.log";
            string logfileFolder = "LogFiles";
            if (!Directory.Exists(logfileFolder))
                Directory.CreateDirectory(logfileFolder);
            if (AccountNumber != -1)
                File.Copy(source, logfileFolder + "\\" + AccountNumber + ".txt", true);
            else
                File.Copy(source, logfileFolder + "\\temp.txt", true);



        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            QuestInfo q1 = new QuestInfo(false, false, false, false, true);
            QuestInfo q2 = new QuestInfo(true, true, false, false, false);
            QuestInfo q3 = new QuestInfo(true, false, true, false, true);


            var test = ParseLogfile.FindBestSingleColor(q1, q2, q3);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnCopyDeckAndStart_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(ImportDecks.AssembleDeck(ChkWhite.Checked, ChkBlue.Checked, ChkBlack.Checked, ChkRed.Checked, ChkGreen.Checked));
        }

        private void CreateDeckFromColors()
        {
            ImportDecks.CreateDeckFromColors(ChkWhite.Checked, ChkBlue.Checked, ChkBlack.Checked, ChkRed.Checked, ChkGreen.Checked);
        }

        Thread autoPlayThread;



        private void BtnLoginAuto_Click(object sender, EventArgs e)
        {
            maxQuestsLeft = int.Parse(TxtMaxQuests.Text.ToString());
            AutoLoginAndPlay();
        }
        bool startOver = false;
        private void AutoLoginAndPlay()
        {
            // Log the start of the method execution
            LogFile.WriteLog("Form1.AutoLoginAndPlay: Start");

            Thread.Sleep(500);

            MTGArena.GetMagicWindowInFocus();

            Thread.Sleep(500);

            int userNumber = int.Parse(TxtLogin.Text);

            while (true)
            {
                //Login
                {
                    Thread.Sleep(500);
                    if (Directory.Exists("config"))
                        userNumber = Player.GetNextPlayer(@"\\tommy-pc\delt\Magic\NextPlayer1.txt");
                    else
                        userNumber = Player.GetNextPlayer();

                    Player.WriteLastUser(userNumber);

                    if (userNumber == -1)
                    {
                        TxtStatus.Text = "No more users in file";
                        parseLogFile.CopyLogfilesAndNumberThem();
                        return;
                    }
                    LogFile.WriteLog("Form1.AutoLoginAndPlay: (User: " + userNumber + ") Before LoginFromStartScreenToStartScreen");


                    FileInfo logFileInfo = new FileInfo(@"C:\Users\Tommy\AppData\LocalLow\Wizards Of The Coast\MTGA\Player.log");
                    long logfileSize = logFileInfo.Length;

                    //TODO: Failed Login
                    if (logfileSize > 30000000)
                    {
                        CloseMagicOpenAndLogin(userNumber);
                    }
                    else
                    {
                        if (MTGArena.IsMTGArenaRunning() == false)
                            MTGArena.StartMTGArena();

                        bool succesfullLogin = LoginFromStartScreenToStartScreenAndLookForPlaybuttonOnHomescreen(userNumber);

                        if (succesfullLogin == false)
                        {
                            RestoreLastUser();
                            continue;
                        }
                    }

                    if (LookAndClick.WaitForPlayButtonOnHomeScreen(false) == false)
                    {
                        RestoreLastUser();
                        continue;
                    }
                    ClickSecureSpotOnMainScreen10TimesIn5Seconds();

                    //Activate with Season start
                    //ClickSecureSpotOnMainScreen10TimesIn5Seconds();
                    //continue;

                    //Play ranked
                    if (ChkRanked.Checked)
                    {
                        //Press play
                        DoLeftMouseClick(100, 500, 1750, 1005);

                        //Press Find Match
                        DoLeftMouseClick(100, 300, 1740, 135);
                        //Press Ranked
                        DoLeftMouseClick(100, 400, 1610, 275);

                        //Press Timeless
                        DoLeftMouseClick(100, 400, 1700, 705);

                        //Select Deck
                        SelectFirstDeck();

                        //Press Play
                        DoLeftMouseClick(200, 200, 1733, 1012);

                        Thread.Sleep(10000);

                        //Wait for Mulligan
                        while (CheckAreaForColor(new Point(1126, 875), new Size(3, 5), 252, 255, 252, 255, 252, 255) < 10)
                        {
                            Thread.Sleep(1000);
                        }
                        DoLeftMouseClick(100, 1000, 1126, 875);
                        Thread.Sleep(3000);
                        ConcedeMatch();
                        Thread.Sleep(1000);
                        MTGArena.CloseMTGArena();
                        Thread.Sleep(5000);
                        parseLogFile.CopyLogfilesAndNumberThem();
                        continue;
                    }




                    Cursor.Position = new Point(500, 500);

                    for (int i = 0; i < 300; i++)
                    {
                        if (Cursor.Position.X == 500 && Cursor.Position.Y == 500)
                            continue;

                        if (Cursor.Position.X < 1916 || Cursor.Position.Y < 1070)
                            Thread.Sleep(100);

                    }
                    Cursor.Position = new Point(500, 500);
                    Thread.Sleep(200);
                }

                //Check Quest
                {
                    CheckQuest();


                    if (player.QuestCollection.Quests.Count != 3)
                    {
                        //if more than one is true
                        if (new[] { player.White, player.Blue, player.Black, player.Red, player.Green }.Count(x => x) > 1)
                            continue;

                        if (player.QuestCollection.Quests.Count < maxQuestsLeft)
                            continue;
                    }


                }

                //Choose Deck
                {
                    //PutColorDeckFirst();
                    PutFavouriteOnDeckToPlay();
                    Thread.Sleep(2000);
                }

                if (ContinueSolvingQuests() == false)
                    continue;

                StartFirstMatch();

                // GameLoop until Quests are solved
                {
                    GameLoop();
                }
            }
            // Log the end of the method execution
        }

        private void CheckQuest()
        {
            ReadLogFileAndSetColorsOnPlayer();

            if (player.QuestCollection.CanSwap)
            {
                Thread.Sleep(100);

                ChangeQuest();
            }

            Thread.Sleep(300);

            LogFile.WriteLog($"Form1.AutoLoginAndPlay: Quests count: {player.QuestCollection.Quests.Count}");


        }

        private void PutFavouriteOnDeckToPlay()
        {
            //Press on Decks
            DoLeftMouseClick(500, 3000, 322, 42);

            if (LookAndClick.IsMyDecksUnfoldedOnDeckCreation() == false)
                DoLeftMouseClick(500, 500, 200, 200);

            //RemoveAllFavourites
            RemoveAllFavouriteDecks();

            //Click on textfield for search for decks
            DoLeftMouseClick(500, 500, 717, 121);

            if (player.White)
                SendKeys.Send("B-White{ENTER}");
            if (player.Blue)
                SendKeys.Send("B-Blue{ENTER}");
            if (player.Black)
                SendKeys.Send("B-Black{ENTER}");
            if (player.Red)
                SendKeys.Send("B-Red{ENTER}");
            if (player.Green)
                SendKeys.Send("B-Green{ENTER}");

            SendKeys.Flush();

            Thread.Sleep(1000);

            //Click on first Deck
            FavouriteFirstDeck();

            deckColorWhite = player.White;
            deckColorBlue = player.Blue;
            deckColorBlack = player.Black;
            deckColorGreen = player.Green;
            deckColorRed = player.Red;

            //PressOnHome
            DoLeftMouseClick(2000, 3000, 104, 42);
        }

        private void FavouriteFirstDeck()
        {
            //click first deck
            DoLeftMouseClick(500, 500, 500, 400);

            //click favourite icon
            DoLeftMouseClick(500, 500, 500, 1000);
        }

        private void RemoveAllFavouriteDecks()
        {
            while (LookAndClick.IsFirstDeckFavourite())
            {
                //click first deck
                DoLeftMouseClick(500, 500, 500, 400);

                //click favourite icon
                DoLeftMouseClick(500, 500, 500, 1000);
            }

        }

        bool deckColorWhite = false;
        bool deckColorBlue = false;
        bool deckColorBlack = false;
        bool deckColorGreen = false;
        bool deckColorRed = false;



        private void CreateDeck()
        {
            Create5Decks();
            PutColorDeckFirst();


            ////Press on Decks
            //DoLeftMouseClick(500, 3000, 322, 42);


            //string deck = ImportDecks.AssembleDeck(player.White, player.Blue, player.Black, player.Red, player.Green);
            //deckColorWhite = player.White;
            //deckColorBlue = player.Blue;
            //deckColorBlack = player.Black;
            //deckColorGreen = player.Green;
            //deckColorRed = player.Red;


            //Clipboard.SetText(deck);

            //ClickOnImportDeck();

            ////PressOnHome
            //DoLeftMouseClick(1000, 3000, 104, 42);
        }
        public void Create5Decks()
        {
            //Press on Decks
            DoLeftMouseClick(500, 3000, 322, 42);

            for (int i = 0; i < 5; i++)
            {
                string deck = ImportDecks.AssembleDeck(i == 0, i == 1, i == 2, i == 3, i == 4);
                Clipboard.SetText(deck);
                ClickOnImportDeck();
                ////PressOnHome
                //DoLeftMouseClick(1000, 3000, 104, 42);

            }
        }

        private void PutColorDeckFirst()
        {
            //Press on Decks
            DoLeftMouseClick(500, 3000, 322, 42);

            //Click on textfield for search for decks
            DoLeftMouseClick(500, 500, 717, 121);

            if (player.White)
                SendKeys.Send("B-White{ENTER}");
            if (player.Blue)
                SendKeys.Send("B-Blue{ENTER}");
            if (player.Black)
                SendKeys.Send("B-Black{ENTER}");
            if (player.Red)
                SendKeys.Send("B-Red{ENTER}");
            if (player.Green)
                SendKeys.Send("B-Green{ENTER}");

            SendKeys.Flush();

            Thread.Sleep(1000);

            if (LookAndClick.IsMyDecksUnfoldedOnDeckCreation() == false)
                DoLeftMouseClick(500, 500, 200, 200);

            DoubleClickOnFirstDeckAndClickDone();

            deckColorWhite = player.White;
            deckColorBlue = player.Blue;
            deckColorBlack = player.Black;
            deckColorGreen = player.Green;
            deckColorRed = player.Red;

            //PressOnHome
            DoLeftMouseClick(2000, 3000, 104, 42);
        }

        private void DoubleClickOnFirstDeckAndClickDone()
        {
            //Click On First Deck
            DoLeftMouseClick(50, 50, 500, 400);
            DoLeftMouseClick(50, 50, 500, 400);

            //Click Done
            DoLeftMouseClick(8000, 3000, 1750, 1000);
        }

        private void SelectDeck()
        {
            //Press Play
            DoLeftMouseClick(200, 2800, 1742, 1010);

            //Press on Find Match
            DoLeftMouseClick(200, 1000, 1736, 142);

            //Press on Bot
            DoLeftMouseClick(200, 200, 1686, 768);

            //Press on Deck
            DoLeftMouseClick(400, 4500, 280, 350);
            DoLeftMouseClick(500, 500, 450, 580);

            //Press Play
            DoLeftMouseClick(200, 200, 1738, 1012);
            Thread.Sleep(25000);
        }

        private static void ClickOnImportDeck()
        {
            Thread.Sleep(1500);
            //Press Import Deck
            MouseOperations.MouseOperations.DoLeftMouseClick(new Point(570, 1000));

            Thread.Sleep(1500);
            //Press OK
            MouseOperations.MouseOperations.DoLeftMouseClick(new Point(950, 630));
        }

        private void TxtLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnPixelTool_Click(object sender, EventArgs e)
        {
            PixelTool pxt = new PixelTool();
            pxt.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Monitor m = new Monitor();
            m.Show();
        }

        private void BtnSplitLoginFiles_Click(object sender, EventArgs e)
        {

        }

        private void SplitLogFilesToIndividualLogFiles()
        {
            var filenames = Directory.GetFiles("LogfilesDump");
            foreach (var filename in filenames)
            {
                if (filename.IndexOf(".log") != -1)
                {
                    ReadLogFile(filename);
                    File.Delete(filename);
                }
            }
        }

        private void ReadAllLogFiles()
        {
            var filenames = Directory.GetFiles("LogFilesTemp");
            foreach (var filename in filenames)
            {
                OnlySaveImportantLines(filename);
            }

        }

        private void OnlySaveImportantLines(string fileNamePar)
        {
            string cleanLogfilesDirectory = "cleanLogfiles\\";
            string[] lines = File.ReadAllLines(fileNamePar);
            bool inventoryLine = true;
            StringBuilder sb = new StringBuilder();
            string firstLineForDatetime = "";
            for (int i = lines.Length - 1; i >= 0; i--)
            {

                if (lines[i].IndexOf("{\"InventoryInfo\":{\"") != -1 && inventoryLine)
                {
                    firstLineForDatetime = lines[i - 2];
                    sb.Append(lines[i - 2] + Environment.NewLine);
                    sb.Append(lines[i]);//.Substring(0, lines[i].IndexOf("\"}]}}") + 5) + Environment.NewLine);
                    inventoryLine = false;
                    break;
                }

            }

            for (int i = 0; i < lines.Length; i++)
            {

                if (lines[i].IndexOf("{\"InventoryInfo\":{\"") != -1 && inventoryLine)
                {
                    sb.AppendLine(lines[i - 2]);
                    sb.AppendLine(lines[i].Substring(0, 2000));
                    inventoryLine = false;
                }

                if (lines[i].IndexOf("\"quests\":[") != -1) // || lines[i].IndexOf("") || lines[i].IndexOf(""))
                {
                    sb.AppendLine(lines[i - 2]);
                    sb.AppendLine(lines[i]);
                }


                if (lines[i].IndexOf("<== Rank_GetCombinedRankInfo") != -1) // || lines[i].IndexOf("") || lines[i].IndexOf(""))
                {
                    sb.AppendLine(lines[i]);
                    sb.AppendLine(lines[i + 1]);
                }
            }

            string cleanFileName = cleanLogfilesDirectory + fileNamePar.Substring(+fileNamePar.IndexOf("\\"));

            #region compareTime
            string firstLine = "[UnityCrossThreadLogger]";
            try
            {
                string oldFileTimeStamp = File.ReadAllLines(cleanFileName)[0].Substring(firstLine.Length);
                DateTime oldFileDatetime = ConvertLineToDatetime(oldFileTimeStamp);
                DateTime newFileDatetime = ConvertLineToDatetime(firstLineForDatetime.Substring(firstLine.Length));
                #endregion

                if (newFileDatetime.Ticks > oldFileDatetime.Ticks)
                {
                    File.WriteAllText(cleanFileName, sb.ToString(), Encoding.UTF8);
                }
                File.Delete(fileNamePar);
            }
            catch
            {
                File.WriteAllText(cleanFileName, sb.ToString(), Encoding.UTF8);
            }

        }

        private static DateTime ConvertLineToDatetime(string dateTimeString)
        {
            return DateTime.ParseExact(dateTimeString, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        }

        private void ReadLogFile(string fileNamePar)
        {

            string[] lines = File.ReadAllLines(fileNamePar);

            List<Tuple<int, string>> list = new List<Tuple<int, string>>(); ;
            string find = "[Accounts - Login] Logged in successfully. Display Name: ";
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].IndexOf(find) != -1)
                    list.Add(new Tuple<int, string>(i, lines[i].Substring(find.Length)));
            }
            list.Add(new Tuple<int, string>(lines.Length - 1, "Nobody"));
            for (int accountNumber = 0; accountNumber < list.Count - 1; accountNumber++)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int x = list[accountNumber].Item1 - 1; x < list[accountNumber + 1].Item1 - 2; x++)
                {
                    stringBuilder.AppendLine(lines[x]);
                }
                string filename = @"logfilestemp\" + list[accountNumber].Item2 + ".log";
                File.WriteAllText(filename, stringBuilder.ToString(), Encoding.UTF8);
            }

            int p = 0;
        }

        private void BtnCopyThisIsTheWay_Click(object sender, EventArgs e)
        {
            Thread.Sleep(5000);
            for (int i = 0; i < 50; i++)
                MouseOperations.MouseOperations.MouseWheelUp(-1);
            Thread.Sleep(500);
            for (int i = 0; i < 50; i++)
                MouseOperations.MouseOperations.MouseWheelUp(1);
            Thread.Sleep(500);
            for (int i = 0; i < 50; i++)
                MouseOperations.MouseOperations.MouseWheelUp(-1);
            Thread.Sleep(500);
            for (int i = 0; i < 50; i++)
                MouseOperations.MouseOperations.MouseWheelUp(1);
            Thread.Sleep(500);
        }

        private void BtnCleanUp_Click(object sender, EventArgs e)
        {

        }

        private void BtnCopyOriginalLogfiles_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(CopyAndParsePlayerFiles));
            t.IsBackground = false;
            t.Start();
        }

        private void CopyAndParsePlayerFiles()
        {
            parseLogFile.CopyLogfilesAndNumberThem();
            if (WhatComputer() == "Master")
            {
                SplitLogFilesToIndividualLogFiles();
                ReadAllLogFiles();
            }
        }

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            MTGArena.GetMagicWindowInFocus();
            Thread.Sleep(400);
            CardCollection cc = new CardCollection();
            cc.MakeFullCollection();
        }

        private void CleanLinesInventoryLines(string filename)
        {
            var allLines = File.ReadAllLines(filename);
            var result = new List<string>();
            string endOfJSONElement = ",\"CustomTokens\":"; ;
            string test;
            for (int i = 0; i < allLines.Length; i++)
            {
                if (allLines[i].IndexOf(endOfJSONElement) != -1)
                    result.Add(allLines[i].Substring(0, allLines[i].IndexOf(endOfJSONElement)) + "}}");
                else
                    result.Add(allLines[i]);
            }
            File.WriteAllLines(filename, result.ToArray());
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            RestoreLastUser();
        }

        private string WhatComputer()
        {
            if (Directory.Exists("config"))
                return "Master";
            else
                return "Slave";
        }

        private void RestoreLastUser()
        {
            if (WhatComputer() == "Master")
                Player.TransferLastUserIDToUserlist(@"\\tommy-pc\delt\Magic\NextPlayer1.txt");
            else
                Player.TransferLastUserIDToUserlist();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MTGLogFileReaderFrm frm = new MTGLogFileReaderFrm();
            frm.ShowDialog();
        }

        private void CloseMagicOpenAndLogin(int usernumberPar)
        {
            MTGArena.CloseMTGArena();
            Thread.Sleep(5000);
            parseLogFile.CopyLogfilesAndNumberThem();
            Thread.Sleep(3000);
            MTGArena.StartMTGArena();
            LoginFromStartScreenToStartScreenAndLookForPlaybuttonOnHomescreen(usernumberPar);
        }

        private void OnlyLoginToCheck()
        {

            // Log the start of the method execution
            LogFile.WriteLog("Form1.AutoLoginAndPlay: Start");

            Thread.Sleep(500);

            MTGArena.GetMagicWindowInFocus();

            Thread.Sleep(500);

            LogFile.WriteLog("Form1.AutoLoginAndPlay: Before ReadLogFileAndSetColorsOnPlayer");
            ReadLogFileAndSetColorsOnPlayer();
            LogFile.WriteLog("Form1.AutoLoginAndPlay: After ReadLogFileAndSetColorsOnPlayer");

            int userNumber = int.Parse(TxtLogin.Text);

            while (true)
            {
                //Login
                {
                    Thread.Sleep(500);
                    if (Directory.Exists("config"))
                        userNumber = Player.GetNextPlayer(@"\\tommy-pc\delt\Magic\NextPlayer1.txt");
                    else
                        userNumber = Player.GetNextPlayer();

                    if (userNumber == -1)
                    {
                        MessageBox.Show("No more users in file");
                        return;
                    }
                    LogFile.WriteLog("Form1.AutoLoginAndPlay: (User: " + userNumber + ") Before LoginFromStartScreenToStartScreen");
                    LoginFromStartScreenToStartScreenAndLookForPlaybuttonOnHomescreen(userNumber);

                    Thread.Sleep(500);


                    LookAndClick.WaitForPlayButtonOnHomeScreen(false);


                    LogoutFromHomeScreen();


                    continue;


                    for (int i = 0; i < 300; i++)
                    {
                        if (Cursor.Position.X == 500 && Cursor.Position.Y == 500)
                            continue;

                        if (Cursor.Position.X < 1916 || Cursor.Position.Y < 1070)
                            Thread.Sleep(100);

                    }
                    Cursor.Position = new Point(500, 500);
                    Thread.Sleep(500);
                }

                //Check Quest
                {
                    ReadLogFileAndSetColorsOnPlayer();

                    if (player.QuestCollection.CanSwap)
                    {
                        Thread.Sleep(500);

                        ChangeQuest();
                    }

                    Thread.Sleep(300);

                    LogFile.WriteLog($"Form1.AutoLoginAndPlay: Quests count: {player.QuestCollection.Quests.Count}");

                    if (player.ActiveQuests() != 3)
                    {
                        //if more than one is true
                        if (new[] { player.White, player.Blue, player.Black, player.Red, player.Green }.Count(x => x) > 1)
                            continue;

                        if (player.ActiveQuests() < maxQuestsLeft)
                            continue;
                    }
                }

                //Create Deck
                {
                    CreateDeck();

                    Thread.Sleep(5000);
                }

                StartFirstMatch();

                // GameLoop until Quests are solved
                {
                    GameLoop();
                }
            }
            // Log the end of the method execution
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MTGArena.GetMagicWindowInFocus();
            //Press on Decks
            DoLeftMouseClick(500, 3000, 322, 42);

            Create5Decks();
        }

        private void BtnCopyFilesToSlave_Click(object sender, EventArgs e)
        {
            File.Copy("loginOrder.txt", "\\\\tommy-pc\\delt\\Magic\\NextPlayer1.txt", true);
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            TxtMaxQuests.Text = "3";
        }

        private void Btn0_Click(object sender, EventArgs e)
        {
            TxtMaxQuests.Text = "0";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            File.Copy("loginOrder0Quests.txt", "\\\\tommy-pc\\delt\\Magic\\NextPlayer1.txt", true);
        }
    }
}


