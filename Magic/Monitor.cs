using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Numerics;

namespace Magic
{
    public partial class Monitor : Form
    {
        public Monitor()
        {
            InitializeComponent();
            Thread t = new Thread(new ThreadStart(UpdateLogFileInfo));
            t.IsBackground = true;
            t.Start();

        }


        private void UpdateLogFileInfo()
        {
            ParseLogfile plf = new ParseLogfile();
            Player player = new Player();

            while (true)
            {

                plf.GetQuestsForCurrentPlayerJsonStyle(player);   
                
                WriteLabel(label1, plf.GetLastQuestLine().ToString() + Environment.NewLine + player.ReturnQuestCollectionString());
                Thread.Sleep(1000);
            }


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
                tboxPar.Text += insertTextPar;
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

        private void BtnStart_Click(object sender, EventArgs e)
        {

        }
    }
}
