using System;
using System.Threading;
using System.Windows.Forms;

namespace LightFileExplorer
{
    public partial class ProgressWindow : Form
    {
        private Thread workerThread;

        public ProgressWindow(Form parentWindow, string description, Thread workerThread)
        {
            InitializeComponent();

            this.workerThread = workerThread;

            this.Text = description + " - LFE";
        }

        private void MyAbortButton_Click(object sender, EventArgs e)
        {
            if (this.workerThread != null)
            {
                if (this.workerThread.IsAlive)
                {
                    try
                    {
                        this.workerThread.Abort();
                    }
                    catch
                    {
                    }
                }
            }

            this.Close();
        }
    }
}