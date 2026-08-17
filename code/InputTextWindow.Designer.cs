namespace LightFileExplorer
{
    internal partial class InputTextWindow
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
            this.Input1Label = new System.Windows.Forms.Label();
            this.Input1TextBox = new System.Windows.Forms.TextBox();
            this.MyOkButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Input1Label
            // 
            this.Input1Label.AutoSize = true;
            this.Input1Label.Location = new System.Drawing.Point(15, 15);
            this.Input1Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Input1Label.Name = "Input1Label";
            this.Input1Label.Size = new System.Drawing.Size(47, 15);
            this.Input1Label.TabIndex = 0;
            this.Input1Label.Text = "Input 1:";
            // 
            // Input1TextBox
            // 
            this.Input1TextBox.Location = new System.Drawing.Point(15, 35);
            this.Input1TextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Input1TextBox.Name = "Input1TextBox";
            this.Input1TextBox.Size = new System.Drawing.Size(618, 23);
            this.Input1TextBox.TabIndex = 1;
            // 
            // MyOkButton
            // 
            this.MyOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.MyOkButton.Location = new System.Drawing.Point(221, 90);
            this.MyOkButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MyOkButton.Name = "MyOkButton";
            this.MyOkButton.Size = new System.Drawing.Size(100, 29);
            this.MyOkButton.TabIndex = 2;
            this.MyOkButton.Text = "&OK";
            this.MyOkButton.UseVisualStyleBackColor = true;
            this.MyOkButton.Click += new System.EventHandler(this.MyOkButtonClick);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(329, 90);
            this.MyCancelButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(100, 29);
            this.MyCancelButton.TabIndex = 3;
            this.MyCancelButton.Text = "&Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            this.MyCancelButton.Click += new System.EventHandler(this.MyCancelButtonClick);
            // 
            // InputTextWindow
            // 
            this.AcceptButton = this.MyOkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(650, 135);
            this.ControlBox = false;
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.MyOkButton);
            this.Controls.Add(this.Input1TextBox);
            this.Controls.Add(this.Input1Label);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputTextWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Input Text";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Input1Label;
        private System.Windows.Forms.Button MyOkButton;
        private System.Windows.Forms.Button MyCancelButton;
        private System.Windows.Forms.TextBox Input1TextBox;
    }
}