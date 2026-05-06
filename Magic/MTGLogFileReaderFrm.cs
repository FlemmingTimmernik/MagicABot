using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magic
{
    public partial class MTGLogFileReaderFrm : Form
    {
        public MTGLogFileReaderFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(ReadLogFile);
            t.IsBackground = true;
            t.Start();
        }

        private void ReadLogFile()
        {
            while (true)
            {
                WriteLabel(label1, MTGLogfileReader.Test2());
                WriteLabel(label2, MTGLogfileReader.strGameState);
                Thread.Sleep(50);
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

    }
}
