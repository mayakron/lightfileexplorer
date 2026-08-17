using System;
using System.Windows.Forms;

namespace LightFileExplorer
{
    internal partial class InputTextTextWindow : Form
    {
        public InputTextTextWindow(Form parentWindow, string title, string input1Label, string input2Label, string input1Value = null, string input2Value = null)
        {
            InitializeComponent();

            this.Text = title;

            this.Input1Label.Text = input1Label;
            this.Input2Label.Text = input2Label;

            if (!string.IsNullOrEmpty(input1Value))
            {
                this.Input1TextBox.Text = input1Value;
            }

            if (!string.IsNullOrEmpty(input2Value))
            {
                this.Input2TextBox.Text = input2Value;
            }
        }

        public string Input1 { get; private set; }

        public string Input2 { get; private set; }

        private void MyCancelButtonClick(object sender, EventArgs e)
        {
            this.Input1 = null;
            this.Input2 = null;

            this.DialogResult = DialogResult.Cancel;
        }

        private void MyOkButtonClick(object sender, EventArgs e)
        {
            this.Input1 = this.Input1TextBox.Text.Trim();
            this.Input2 = this.Input2TextBox.Text.Trim();

            this.DialogResult = DialogResult.OK;
        }
    }
}