using System;
using System.Threading;
using System.Windows.Forms;

namespace LightFileExplorer
{
    public partial class ProgressWindow : Form
    {
        private readonly Thread workerThread;

        public ProgressWindow(Form parentWindow, string description, Thread workerThread)
        {
            InitializeComponent();

            this.workerThread = workerThread;

            this.Text = description + " - LFE";
        }

        private void MyAbortButtonClick(object sender, EventArgs e)
        {
            // By allowing the user to abort the operation, we accept that some files and directories can end up in a partially consistent state.

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