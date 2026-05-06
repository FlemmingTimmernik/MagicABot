namespace ScreenshotHelper
{
    partial class PixelTool
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TxtUpperLeftCornerCoordinateX = new System.Windows.Forms.TextBox();
            this.TxtSizeHeight = new System.Windows.Forms.TextBox();
            this.TxtSizeWidth = new System.Windows.Forms.TextBox();
            this.TxtUpperLeftCornerCoordinateY = new System.Windows.Forms.TextBox();
            this.BtnTakeScreenshotsAndCompare = new System.Windows.Forms.Button();
            this.TxtOverview = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TxtUpperLeftCornerCoordinateX
            // 
            this.TxtUpperLeftCornerCoordinateX.Location = new System.Drawing.Point(93, 26);
            this.TxtUpperLeftCornerCoordinateX.Name = "TxtUpperLeftCornerCoordinateX";
            this.TxtUpperLeftCornerCoordinateX.Size = new System.Drawing.Size(44, 20);
            this.TxtUpperLeftCornerCoordinateX.TabIndex = 0;
            this.TxtUpperLeftCornerCoordinateX.Text = "0";
            // 
            // TxtSizeHeight
            // 
            this.TxtSizeHeight.Location = new System.Drawing.Point(143, 52);
            this.TxtSizeHeight.Name = "TxtSizeHeight";
            this.TxtSizeHeight.Size = new System.Drawing.Size(44, 20);
            this.TxtSizeHeight.TabIndex = 3;
            this.TxtSizeHeight.Text = "1";
            // 
            // TxtSizeWidth
            // 
            this.TxtSizeWidth.Location = new System.Drawing.Point(93, 52);
            this.TxtSizeWidth.Name = "TxtSizeWidth";
            this.TxtSizeWidth.Size = new System.Drawing.Size(44, 20);
            this.TxtSizeWidth.TabIndex = 2;
            this.TxtSizeWidth.Text = "1";
            // 
            // TxtUpperLeftCornerCoordinateY
            // 
            this.TxtUpperLeftCornerCoordinateY.Location = new System.Drawing.Point(143, 26);
            this.TxtUpperLeftCornerCoordinateY.Name = "TxtUpperLeftCornerCoordinateY";
            this.TxtUpperLeftCornerCoordinateY.Size = new System.Drawing.Size(44, 20);
            this.TxtUpperLeftCornerCoordinateY.TabIndex = 1;
            this.TxtUpperLeftCornerCoordinateY.Text = "0";
            // 
            // BtnTakeScreenshotsAndCompare
            // 
            this.BtnTakeScreenshotsAndCompare.Location = new System.Drawing.Point(12, 24);
            this.BtnTakeScreenshotsAndCompare.Name = "BtnTakeScreenshotsAndCompare";
            this.BtnTakeScreenshotsAndCompare.Size = new System.Drawing.Size(75, 86);
            this.BtnTakeScreenshotsAndCompare.TabIndex = 4;
            this.BtnTakeScreenshotsAndCompare.Text = "Take Screenshots and Compare";
            this.BtnTakeScreenshotsAndCompare.UseVisualStyleBackColor = true;
            this.BtnTakeScreenshotsAndCompare.Click += new System.EventHandler(this.BtnTakeScreenshotsAndCompare_Click);
            // 
            // TxtOverview
            // 
            this.TxtOverview.Location = new System.Drawing.Point(193, 24);
            this.TxtOverview.Multiline = true;
            this.TxtOverview.Name = "TxtOverview";
            this.TxtOverview.Size = new System.Drawing.Size(816, 385);
            this.TxtOverview.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 86);
            this.button1.TabIndex = 6;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // PixelTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TxtOverview);
            this.Controls.Add(this.BtnTakeScreenshotsAndCompare);
            this.Controls.Add(this.TxtUpperLeftCornerCoordinateY);
            this.Controls.Add(this.TxtSizeWidth);
            this.Controls.Add(this.TxtSizeHeight);
            this.Controls.Add(this.TxtUpperLeftCornerCoordinateX);
            this.Name = "PixelTool";
            this.Text = "PixelTool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtUpperLeftCornerCoordinateX;
        private System.Windows.Forms.TextBox TxtSizeHeight;
        private System.Windows.Forms.TextBox TxtSizeWidth;
        private System.Windows.Forms.TextBox TxtUpperLeftCornerCoordinateY;
        private System.Windows.Forms.Button BtnTakeScreenshotsAndCompare;
        private System.Windows.Forms.TextBox TxtOverview;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}