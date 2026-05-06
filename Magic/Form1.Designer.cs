namespace Magic
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ButtonLogin = new Button();
            TxtLogin = new TextBox();
            TxtDelete = new TextBox();
            ButtonDelete = new Button();
            TxtStatus = new TextBox();
            BtnPlay = new Button();
            BtnCopyChosenDeckToClipboard = new Button();
            BtnLoginAndPlay = new Button();
            ChkWhite = new CheckBox();
            ChkBlue = new CheckBox();
            ChkBlack = new CheckBox();
            ChkRed = new CheckBox();
            ChkGreen = new CheckBox();
            BtnLoginScreenshot = new Button();
            TxtFirstPlayer = new TextBox();
            TxtLastPlayer = new TextBox();
            BtnTest = new Button();
            BtnTakeScreenShot = new Button();
            button1 = new Button();
            BtnCopyDeckAndStart = new Button();
            ChkStartPlaying = new CheckBox();
            BtnLoginAuto = new Button();
            BtnPixelTool = new Button();
            ChkReadLogfileFirst = new CheckBox();
            BtnMonitor = new Button();
            BtnSplitLoginFiles = new Button();
            BtnCopyThisIsTheWay = new Button();
            BtnCopyOriginalLogfiles = new Button();
            BtnConvert = new Button();
            ChkAutoConcede = new CheckBox();
            button2 = new Button();
            button3 = new Button();
            TxtMaxQuests = new TextBox();
            button4 = new Button();
            TxtNumberOfPages = new TextBox();
            BtnCopyFilesToSlave = new Button();
            Btn0 = new Button();
            Btn3 = new Button();
            button5 = new Button();
            ChkRanked = new CheckBox();
            SuspendLayout();
            // 
            // ButtonLogin
            // 
            ButtonLogin.Location = new Point(735, 179);
            ButtonLogin.Name = "ButtonLogin";
            ButtonLogin.Size = new Size(75, 23);
            ButtonLogin.TabIndex = 0;
            ButtonLogin.Text = "Login";
            ButtonLogin.UseVisualStyleBackColor = true;
            ButtonLogin.Click += ButtonLogin_Click;
            // 
            // TxtLogin
            // 
            TxtLogin.Location = new Point(629, 180);
            TxtLogin.Name = "TxtLogin";
            TxtLogin.Size = new Size(100, 23);
            TxtLogin.TabIndex = 1;
            TxtLogin.Text = "34";
            TxtLogin.TextChanged += TxtLogin_TextChanged;
            // 
            // TxtDelete
            // 
            TxtDelete.Location = new Point(806, 389);
            TxtDelete.Name = "TxtDelete";
            TxtDelete.Size = new Size(100, 23);
            TxtDelete.TabIndex = 2;
            TxtDelete.Text = "15";
            // 
            // ButtonDelete
            // 
            ButtonDelete.Location = new Point(912, 389);
            ButtonDelete.Name = "ButtonDelete";
            ButtonDelete.Size = new Size(75, 23);
            ButtonDelete.TabIndex = 3;
            ButtonDelete.Text = "Delete";
            ButtonDelete.UseVisualStyleBackColor = true;
            ButtonDelete.Click += ButtonDelete_Click;
            // 
            // TxtStatus
            // 
            TxtStatus.Location = new Point(12, 12);
            TxtStatus.Multiline = true;
            TxtStatus.Name = "TxtStatus";
            TxtStatus.ScrollBars = ScrollBars.Vertical;
            TxtStatus.Size = new Size(497, 425);
            TxtStatus.TabIndex = 4;
            TxtStatus.Text = "TxtStatus";
            // 
            // BtnPlay
            // 
            BtnPlay.Location = new Point(515, 151);
            BtnPlay.Name = "BtnPlay";
            BtnPlay.Size = new Size(75, 23);
            BtnPlay.TabIndex = 5;
            BtnPlay.Text = "Play";
            BtnPlay.UseVisualStyleBackColor = true;
            BtnPlay.Click += BtnTest_Click;
            // 
            // BtnCopyChosenDeckToClipboard
            // 
            BtnCopyChosenDeckToClipboard.Location = new Point(528, 258);
            BtnCopyChosenDeckToClipboard.Name = "BtnCopyChosenDeckToClipboard";
            BtnCopyChosenDeckToClipboard.Size = new Size(112, 47);
            BtnCopyChosenDeckToClipboard.TabIndex = 6;
            BtnCopyChosenDeckToClipboard.Text = "Copy chosen deck to clipboard";
            BtnCopyChosenDeckToClipboard.UseVisualStyleBackColor = true;
            BtnCopyChosenDeckToClipboard.Click += button1_Click;
            // 
            // BtnLoginAndPlay
            // 
            BtnLoginAndPlay.Location = new Point(515, 180);
            BtnLoginAndPlay.Name = "BtnLoginAndPlay";
            BtnLoginAndPlay.Size = new Size(108, 23);
            BtnLoginAndPlay.TabIndex = 7;
            BtnLoginAndPlay.Text = "Login and Play";
            BtnLoginAndPlay.UseVisualStyleBackColor = true;
            BtnLoginAndPlay.Click += BtnLoginAndPlay_Click;
            // 
            // ChkWhite
            // 
            ChkWhite.AutoSize = true;
            ChkWhite.Checked = true;
            ChkWhite.CheckState = CheckState.Checked;
            ChkWhite.Location = new Point(533, 311);
            ChkWhite.Name = "ChkWhite";
            ChkWhite.Size = new Size(57, 19);
            ChkWhite.TabIndex = 9;
            ChkWhite.Text = "White";
            ChkWhite.UseVisualStyleBackColor = true;
            // 
            // ChkBlue
            // 
            ChkBlue.AutoSize = true;
            ChkBlue.Location = new Point(533, 336);
            ChkBlue.Name = "ChkBlue";
            ChkBlue.Size = new Size(49, 19);
            ChkBlue.TabIndex = 10;
            ChkBlue.Text = "Blue";
            ChkBlue.UseVisualStyleBackColor = true;
            // 
            // ChkBlack
            // 
            ChkBlack.AutoSize = true;
            ChkBlack.Location = new Point(533, 361);
            ChkBlack.Name = "ChkBlack";
            ChkBlack.Size = new Size(54, 19);
            ChkBlack.TabIndex = 11;
            ChkBlack.Text = "Black";
            ChkBlack.UseVisualStyleBackColor = true;
            // 
            // ChkRed
            // 
            ChkRed.AutoSize = true;
            ChkRed.Location = new Point(533, 386);
            ChkRed.Name = "ChkRed";
            ChkRed.Size = new Size(46, 19);
            ChkRed.TabIndex = 12;
            ChkRed.Text = "Red";
            ChkRed.UseVisualStyleBackColor = true;
            // 
            // ChkGreen
            // 
            ChkGreen.AutoSize = true;
            ChkGreen.Location = new Point(533, 411);
            ChkGreen.Name = "ChkGreen";
            ChkGreen.Size = new Size(57, 19);
            ChkGreen.TabIndex = 13;
            ChkGreen.Text = "Green";
            ChkGreen.UseVisualStyleBackColor = true;
            // 
            // BtnLoginScreenshot
            // 
            BtnLoginScreenshot.Location = new Point(790, 15);
            BtnLoginScreenshot.Name = "BtnLoginScreenshot";
            BtnLoginScreenshot.Size = new Size(180, 43);
            BtnLoginScreenshot.TabIndex = 14;
            BtnLoginScreenshot.Text = "Login and Take Screenshot of all accounts";
            BtnLoginScreenshot.UseVisualStyleBackColor = true;
            BtnLoginScreenshot.Click += BtnLoginScreenshot_Click;
            // 
            // TxtFirstPlayer
            // 
            TxtFirstPlayer.Location = new Point(790, 64);
            TxtFirstPlayer.Name = "TxtFirstPlayer";
            TxtFirstPlayer.Size = new Size(60, 23);
            TxtFirstPlayer.TabIndex = 15;
            TxtFirstPlayer.Text = "1";
            // 
            // TxtLastPlayer
            // 
            TxtLastPlayer.Location = new Point(856, 64);
            TxtLastPlayer.Name = "TxtLastPlayer";
            TxtLastPlayer.Size = new Size(68, 23);
            TxtLastPlayer.TabIndex = 16;
            TxtLastPlayer.Text = "23";
            // 
            // BtnTest
            // 
            BtnTest.Location = new Point(515, 25);
            BtnTest.Name = "BtnTest";
            BtnTest.Size = new Size(75, 23);
            BtnTest.TabIndex = 17;
            BtnTest.Text = "Test";
            BtnTest.UseVisualStyleBackColor = true;
            BtnTest.Click += BtnTest_Click_1;
            // 
            // BtnTakeScreenShot
            // 
            BtnTakeScreenShot.Location = new Point(671, 15);
            BtnTakeScreenShot.Name = "BtnTakeScreenShot";
            BtnTakeScreenShot.Size = new Size(113, 43);
            BtnTakeScreenShot.TabIndex = 18;
            BtnTakeScreenShot.Text = "Take Screenshot";
            BtnTakeScreenShot.UseVisualStyleBackColor = true;
            BtnTakeScreenShot.Click += BtnTakeScreenShot_Click;
            // 
            // button1
            // 
            button1.Location = new Point(12, 472);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 25;
            button1.Text = "Test Logfile";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // BtnCopyDeckAndStart
            // 
            BtnCopyDeckAndStart.Location = new Point(648, 258);
            BtnCopyDeckAndStart.Name = "BtnCopyDeckAndStart";
            BtnCopyDeckAndStart.Size = new Size(112, 72);
            BtnCopyDeckAndStart.TabIndex = 29;
            BtnCopyDeckAndStart.Text = "Copy chosen deck to clipboard and start";
            BtnCopyDeckAndStart.UseVisualStyleBackColor = true;
            BtnCopyDeckAndStart.Click += BtnCopyDeckAndStart_Click;
            // 
            // ChkStartPlaying
            // 
            ChkStartPlaying.AutoSize = true;
            ChkStartPlaying.Location = new Point(515, 113);
            ChkStartPlaying.Name = "ChkStartPlaying";
            ChkStartPlaying.Size = new Size(92, 19);
            ChkStartPlaying.TabIndex = 30;
            ChkStartPlaying.Text = "Start Playing";
            ChkStartPlaying.UseVisualStyleBackColor = true;
            // 
            // BtnLoginAuto
            // 
            BtnLoginAuto.Location = new Point(274, 443);
            BtnLoginAuto.Name = "BtnLoginAuto";
            BtnLoginAuto.Size = new Size(171, 154);
            BtnLoginAuto.TabIndex = 31;
            BtnLoginAuto.Text = "Login Auto";
            BtnLoginAuto.UseVisualStyleBackColor = true;
            BtnLoginAuto.Click += BtnLoginAuto_Click;
            // 
            // BtnPixelTool
            // 
            BtnPixelTool.Location = new Point(12, 443);
            BtnPixelTool.Name = "BtnPixelTool";
            BtnPixelTool.Size = new Size(75, 23);
            BtnPixelTool.TabIndex = 39;
            BtnPixelTool.Text = "Pixel Tool";
            BtnPixelTool.UseVisualStyleBackColor = true;
            BtnPixelTool.Click += BtnPixelTool_Click;
            // 
            // ChkReadLogfileFirst
            // 
            ChkReadLogfileFirst.AutoSize = true;
            ChkReadLogfileFirst.Checked = true;
            ChkReadLogfileFirst.CheckState = CheckState.Checked;
            ChkReadLogfileFirst.Location = new Point(528, 233);
            ChkReadLogfileFirst.Name = "ChkReadLogfileFirst";
            ChkReadLogfileFirst.Size = new Size(126, 19);
            ChkReadLogfileFirst.TabIndex = 40;
            ChkReadLogfileFirst.Text = "Read Log File First?";
            ChkReadLogfileFirst.UseVisualStyleBackColor = true;
            // 
            // BtnMonitor
            // 
            BtnMonitor.Location = new Point(1234, 461);
            BtnMonitor.Name = "BtnMonitor";
            BtnMonitor.Size = new Size(75, 23);
            BtnMonitor.TabIndex = 41;
            BtnMonitor.Text = "Monitor";
            BtnMonitor.UseVisualStyleBackColor = true;
            BtnMonitor.Click += button2_Click;
            // 
            // BtnSplitLoginFiles
            // 
            BtnSplitLoginFiles.Location = new Point(12, 614);
            BtnSplitLoginFiles.Name = "BtnSplitLoginFiles";
            BtnSplitLoginFiles.Size = new Size(190, 23);
            BtnSplitLoginFiles.TabIndex = 42;
            BtnSplitLoginFiles.Text = "Split and clean Logfiles";
            BtnSplitLoginFiles.UseVisualStyleBackColor = true;
            BtnSplitLoginFiles.Click += BtnSplitLoginFiles_Click;
            // 
            // BtnCopyThisIsTheWay
            // 
            BtnCopyThisIsTheWay.Location = new Point(216, 643);
            BtnCopyThisIsTheWay.Name = "BtnCopyThisIsTheWay";
            BtnCopyThisIsTheWay.Size = new Size(159, 23);
            BtnCopyThisIsTheWay.TabIndex = 43;
            BtnCopyThisIsTheWay.Text = "Copy thisistheway";
            BtnCopyThisIsTheWay.UseVisualStyleBackColor = true;
            BtnCopyThisIsTheWay.Click += BtnCopyThisIsTheWay_Click;
            // 
            // BtnCopyOriginalLogfiles
            // 
            BtnCopyOriginalLogfiles.Location = new Point(12, 585);
            BtnCopyOriginalLogfiles.Name = "BtnCopyOriginalLogfiles";
            BtnCopyOriginalLogfiles.Size = new Size(126, 23);
            BtnCopyOriginalLogfiles.TabIndex = 45;
            BtnCopyOriginalLogfiles.Text = "Copy From other PC";
            BtnCopyOriginalLogfiles.UseVisualStyleBackColor = true;
            BtnCopyOriginalLogfiles.Click += BtnCopyOriginalLogfiles_Click;
            // 
            // BtnConvert
            // 
            BtnConvert.Location = new Point(658, 657);
            BtnConvert.Name = "BtnConvert";
            BtnConvert.Size = new Size(126, 23);
            BtnConvert.TabIndex = 46;
            BtnConvert.Text = "Make Deck";
            BtnConvert.UseVisualStyleBackColor = true;
            BtnConvert.Click += BtnConvert_Click;
            // 
            // ChkAutoConcede
            // 
            ChkAutoConcede.AutoSize = true;
            ChkAutoConcede.Checked = true;
            ChkAutoConcede.CheckState = CheckState.Checked;
            ChkAutoConcede.Location = new Point(658, 336);
            ChkAutoConcede.Name = "ChkAutoConcede";
            ChkAutoConcede.Size = new Size(102, 19);
            ChkAutoConcede.TabIndex = 47;
            ChkAutoConcede.Text = "Auto Concede";
            ChkAutoConcede.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(451, 443);
            button2.Name = "button2";
            button2.Size = new Size(136, 23);
            button2.TabIndex = 48;
            button2.Text = "Insert Last Player";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // button3
            // 
            button3.Location = new Point(841, 483);
            button3.Name = "button3";
            button3.Size = new Size(171, 154);
            button3.TabIndex = 49;
            button3.Text = "Login Auto";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // TxtMaxQuests
            // 
            TxtMaxQuests.Location = new Point(451, 574);
            TxtMaxQuests.Name = "TxtMaxQuests";
            TxtMaxQuests.Size = new Size(100, 23);
            TxtMaxQuests.TabIndex = 50;
            TxtMaxQuests.Text = "3";
            // 
            // button4
            // 
            button4.Location = new Point(1155, 193);
            button4.Name = "button4";
            button4.Size = new Size(141, 23);
            button4.TabIndex = 51;
            button4.Text = "Create 5 Decks";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // TxtNumberOfPages
            // 
            TxtNumberOfPages.Location = new Point(554, 658);
            TxtNumberOfPages.Name = "TxtNumberOfPages";
            TxtNumberOfPages.Size = new Size(100, 23);
            TxtNumberOfPages.TabIndex = 52;
            TxtNumberOfPages.Text = "2";
            // 
            // BtnCopyFilesToSlave
            // 
            BtnCopyFilesToSlave.Location = new Point(515, 53);
            BtnCopyFilesToSlave.Name = "BtnCopyFilesToSlave";
            BtnCopyFilesToSlave.Size = new Size(92, 23);
            BtnCopyFilesToSlave.TabIndex = 53;
            BtnCopyFilesToSlave.Text = "Copy All";
            BtnCopyFilesToSlave.UseVisualStyleBackColor = true;
            BtnCopyFilesToSlave.Click += BtnCopyFilesToSlave_Click;
            // 
            // Btn0
            // 
            Btn0.Location = new Point(451, 520);
            Btn0.Name = "Btn0";
            Btn0.Size = new Size(58, 23);
            Btn0.TabIndex = 54;
            Btn0.Text = "0";
            Btn0.UseVisualStyleBackColor = true;
            Btn0.Click += Btn0_Click;
            // 
            // Btn3
            // 
            Btn3.Location = new Point(451, 549);
            Btn3.Name = "Btn3";
            Btn3.Size = new Size(58, 23);
            Btn3.TabIndex = 55;
            Btn3.Text = "3";
            Btn3.UseVisualStyleBackColor = true;
            Btn3.Click += Btn3_Click;
            // 
            // button5
            // 
            button5.Location = new Point(515, 84);
            button5.Name = "button5";
            button5.Size = new Size(92, 23);
            button5.TabIndex = 56;
            button5.Text = "Copy 0";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // ChkRanked
            // 
            ChkRanked.AutoSize = true;
            ChkRanked.Location = new Point(559, 578);
            ChkRanked.Name = "ChkRanked";
            ChkRanked.Size = new Size(65, 19);
            ChkRanked.TabIndex = 57;
            ChkRanked.Text = "Ranked";
            ChkRanked.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1434, 692);
            Controls.Add(ChkRanked);
            Controls.Add(button5);
            Controls.Add(Btn3);
            Controls.Add(Btn0);
            Controls.Add(BtnCopyFilesToSlave);
            Controls.Add(TxtNumberOfPages);
            Controls.Add(button4);
            Controls.Add(TxtMaxQuests);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(ChkAutoConcede);
            Controls.Add(BtnConvert);
            Controls.Add(BtnCopyOriginalLogfiles);
            Controls.Add(BtnCopyThisIsTheWay);
            Controls.Add(BtnSplitLoginFiles);
            Controls.Add(BtnMonitor);
            Controls.Add(ChkReadLogfileFirst);
            Controls.Add(BtnPixelTool);
            Controls.Add(BtnLoginAuto);
            Controls.Add(ChkStartPlaying);
            Controls.Add(BtnCopyDeckAndStart);
            Controls.Add(button1);
            Controls.Add(BtnTakeScreenShot);
            Controls.Add(BtnTest);
            Controls.Add(TxtLastPlayer);
            Controls.Add(TxtFirstPlayer);
            Controls.Add(BtnLoginScreenshot);
            Controls.Add(ChkGreen);
            Controls.Add(ChkRed);
            Controls.Add(ChkBlack);
            Controls.Add(ChkBlue);
            Controls.Add(ChkWhite);
            Controls.Add(BtnLoginAndPlay);
            Controls.Add(BtnCopyChosenDeckToClipboard);
            Controls.Add(BtnPlay);
            Controls.Add(TxtStatus);
            Controls.Add(ButtonDelete);
            Controls.Add(TxtDelete);
            Controls.Add(TxtLogin);
            Controls.Add(ButtonLogin);
            Name = "Form1";
            Text = "Check and Change Quest";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ButtonLogin;
        private TextBox TxtLogin;
        private TextBox TxtDelete;
        private Button ButtonDelete;
        private TextBox TxtStatus;
        private Button BtnPlay;
        private Button BtnCopyChosenDeckToClipboard;
        private Button BtnLoginAndPlay;
        private CheckBox ChkWhite;
        private CheckBox ChkBlue;
        private CheckBox ChkBlack;
        private CheckBox ChkRed;
        private CheckBox ChkGreen;
        private Button BtnLoginScreenshot;
        private TextBox TxtFirstPlayer;
        private TextBox TxtLastPlayer;
        private Button BtnTest;
        private Button BtnTakeScreenShot;
        private Button button1;
        private Button BtnCopyDeckAndStart;
        private CheckBox ChkStartPlaying;
        private Button BtnLoginAuto;
        private Button BtnPixelTool;
        private CheckBox ChkReadLogfileFirst;
        private Button BtnMonitor;
        private Button BtnSplitLoginFiles;
        private Button BtnCopyThisIsTheWay;
        private Button BtnCopyOriginalLogfiles;
        private Button BtnConvert;
        private CheckBox ChkAutoConcede;
        private Button button2;
        private Button button3;
        private TextBox TxtMaxQuests;
        private Button button4;
        private TextBox TxtNumberOfPages;
        private Button BtnCopyFilesToSlave;
        private Button Btn0;
        private Button Btn3;
        private Button button5;
        private CheckBox ChkRanked;
    }
}