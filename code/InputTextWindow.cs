using System;
using System.Windows.Forms;

namespace LightFileExplorer
{
    internal partial class InputTextWindow : Form
    {
        public InputTextWindow(string title, string input1Label, string input1Value = null, int input1SelectionStart = -1, int input1SelectionLength = -1)
        {
            InitializeComponent();

            this.Text = title;

            this.Input1Label.Text = input1Label;

            if (!string.IsNullOrEmpty(input1Value))
            {
                this.Input1TextBox.Text = input1Value;

                if (input1SelectionStart > -1)
                {
                    this.Input1TextBox.SelectionStart = input1SelectionStart;
                }

                if (input1SelectionLength > -1)
                {
                    this.Input1TextBox.SelectionLength = input1SelectionLength;
                }
            }
        }

        public string Input1 { get; private set; }

        private void MyCancelButtonClick(object sender, EventArgs e)
        {
            this.Input1 = null;

            this.DialogResult = DialogResult.Cancel;
        }

        private void MyOkButtonClick(object sender, EventArgs e)
        {
            this.Input1 = this.Input1TextBox.Text.Trim();

            this.DialogResult = DialogResult.OK;
        }
    }
}