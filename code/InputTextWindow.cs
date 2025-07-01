using System;
using System.Windows.Forms;

namespace LightFileExplorer
{
    internal partial class InputTextWindow : Form
    {
        public InputTextWindow(string title, string input1Label, string input1Value = null)
        {
            InitializeComponent();

            this.Text = title;

            this.Input1Label.Text = input1Label;

            if (!string.IsNullOrEmpty(input1Value))
            {
                this.Input1TextBox.Text = input1Value;
            }
        }

        public string Input1 { get; private set; }

        private void MyCancelButton_Click(object sender, EventArgs e)
        {
            this.Input1 = null;

            this.DialogResult = DialogResult.Cancel;
        }

        private void MyOkButton_Click(object sender, EventArgs e)
        {
            this.Input1 = this.Input1TextBox.Text;

            this.DialogResult = DialogResult.OK;
        }
    }
}