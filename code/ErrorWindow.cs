using System;
using System.Text;
using System.Windows.Forms;

namespace LightFileExplorer
{
    internal partial class ErrorWindow : Form
    {
        public ErrorWindow(Form parentWindow, Exception ex) : this(parentWindow, null, ex)
        {
        }

        public ErrorWindow(Form parentWindow, string operation, Exception ex)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(operation))
            {
                this.Text = operation;
            }

            var stringBuilder = new StringBuilder();

            if (ex is AggregateException aggregateException)
            {
                foreach (var innerException in aggregateException.InnerExceptions)
                {
                    AppendExceptionToStringBuilder(stringBuilder, innerException);
                }
            }
            else
            {
                AppendExceptionToStringBuilder(stringBuilder, ex);
            }

            this.ErrorTextBox.Text = stringBuilder.ToString();
        }

        private static void AppendExceptionToStringBuilder(StringBuilder stringBuilder, Exception ex)
        {
            const int StackTraceMaxLines = 5;
            const int StackTraceLineMaxLength = 150;

            stringBuilder.AppendLine($"{ex.Message} @ {ex.Source} [{ex.GetType()}]");

            if (ex.Data != null)
            {
                if (ex.Data.Keys.Count > 0)
                {
                    stringBuilder.AppendLine("  Data:");

                    foreach (var dataKey in ex.Data.Keys)
                    {
                        var dataValue = ex.Data[dataKey];

                        stringBuilder.AppendLine($"    {dataKey}: {dataValue}");
                    }
                }
            }

            if (!string.IsNullOrEmpty(ex.StackTrace))
            {
                stringBuilder.AppendLine("  Stack Trace:");

                int stackTraceLineNo = 0; foreach (var stackTraceLine in ex.StackTrace.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None))
                {
                    stackTraceLineNo++;

                    if (stackTraceLineNo > StackTraceMaxLines)
                    {
                        break;
                    }

                    stringBuilder.AppendLine($" {StringUtility.EllipsisInTheMiddle(stackTraceLine, StackTraceLineMaxLength)}");
                }
            }
        }

        private void MyCopyButtonClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ErrorTextBox.Text))
            {
                Clipboard.SetText(this.ErrorTextBox.Text);
            }
        }
    }
}