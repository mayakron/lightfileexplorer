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

        private void DrivesListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.MyOkButton_Click(sender, null);
        }

        private void DrivesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PathTextBox.Text = this.DrivesListBox.SelectedItem as string;
        }

        private void GotoWindow_Load(object sender, EventArgs e)
        {
            foreach (var drive in FileUtility.GetLogicalDrives())
            {
                this.DrivesListBox.Items.Add(drive);
            }
        }

        private void MyCancelButton_Click(object sender, EventArgs e)
        {
            this.SelectedPath = null;

            this.DialogResult = DialogResult.Cancel;
        }

        private void MyOkButton_Click(object sender, EventArgs e)
        {
            this.SelectedPath = this.PathTextBox.Text;

            this.DialogResult = DialogResult.OK;
        }
    }
}