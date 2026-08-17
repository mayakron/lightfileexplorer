using System;
using System.Windows.Forms;

namespace LightFileExplorer
{
    internal partial class GotoWindow : Form
    {
        public GotoWindow(Form parentWindow, string path)
        {
            InitializeComponent();

            this.PathTextBox.Text = path;
        }

        public string SelectedPath { get; private set; }

        private void DrivesListBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.MyOkButtonClick(sender, null);
        }

        private void DrivesListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            this.PathTextBox.Text = this.DrivesListBox.SelectedItem as string;
        }

        private void GotoWindowLoad(object sender, EventArgs e)
        {
            foreach (var drive in FileUtility.GetLogicalDrives())
            {
                this.DrivesListBox.Items.Add(drive);
            }
        }

        private void MyCancelButtonClick(object sender, EventArgs e)
        {
            this.SelectedPath = null;

            this.DialogResult = DialogResult.Cancel;
        }

        private void MyOkButtonClick(object sender, EventArgs e)
        {
            this.SelectedPath = this.PathTextBox.Text.Trim();

            this.DialogResult = DialogResult.OK;
        }
    }
}