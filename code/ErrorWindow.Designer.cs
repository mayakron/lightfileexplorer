namespace LightFileExplorer
{
    internal partial class ErrorWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.ErrorTextBox = new System.Windows.Forms.TextBox();
            this.MyOkButton = new System.Windows.Forms.Button();
            this.MyCopyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Location = new System.Drawing.Point(15, 15);
            this.ErrorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(160, 15);
            this.ErrorLabel.TabIndex = 2;
            this.ErrorLabel.Text = "One or more errors occurred:";
            // 
            // ErrorTextBox
            // 
            this.ErrorTextBox.Location = new System.Drawing.Point(15, 35);
            this.ErrorTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ErrorTextBox.MaxLength = 1048576;
            this.ErrorTextBox.Multiline = true;
            this.ErrorTextBox.Name = "ErrorTextBox";
            this.ErrorTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ErrorTextBox.Size = new System.Drawing.Size(994, 232);
            this.ErrorTextBox.TabIndex = 3;
            // 
            // MyOkButton
            // 
            this.MyOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.MyOkButton.Location = new System.Drawing.Point(412, 290);
            this.MyOkButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MyOkButton.Name = "MyOkButton";
            this.MyOkButton.Size = new System.Drawing.Size(100, 29);
            this.MyOkButton.TabIndex = 0;
            this.MyOkButton.Text = "&OK";
            this.MyOkButton.UseVisualStyleBackColor = true;
            // 
            // MyCopyButton
            // 
            this.MyCopyButton.Location = new System.Drawing.Point(520, 290);
            this.MyCopyButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MyCopyButton.Name = "MyCopyButton";
            this.MyCopyButton.Size = new System.Drawing.Size(100, 29);
            this.MyCopyButton.TabIndex = 1;
            this.MyCopyButton.Text = "&Copy";
            this.MyCopyButton.UseVisualStyleBackColor = true;
            this.MyCopyButton.Click += new System.EventHandler(this.MyCopyButtonClick);
            // 
            // ErrorWindow
            // 
            this.AcceptButton = this.MyOkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyOkButton;
            this.ClientSize = new System.Drawing.Size(1026, 335);
            this.ControlBox = false;
            this.Controls.Add(this.MyCopyButton);
            this.Controls.Add(this.MyOkButton);
            this.Controls.Add(this.ErrorTextBox);
            this.Controls.Add(this.ErrorLabel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Error";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Button MyOkButton;
        private System.Windows.Forms.Button MyCopyButton;
        private System.Windows.Forms.TextBox ErrorTextBox;
    }
}