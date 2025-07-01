namespace LightFileExplorer
{
    internal partial class GotoWindow
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
            this.PathLabel = new System.Windows.Forms.Label();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.MyOkButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.DrivesLabel = new System.Windows.Forms.Label();
            this.DrivesListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // PathLabel
            // 
            this.PathLabel.AutoSize = true;
            this.PathLabel.Location = new System.Drawing.Point(15, 15);
            this.PathLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(34, 15);
            this.PathLabel.TabIndex = 0;
            this.PathLabel.Text = "&Path:";
            // 
            // PathTextBox
            // 
            this.PathTextBox.Location = new System.Drawing.Point(15, 35);
            this.PathTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(618, 23);
            this.PathTextBox.TabIndex = 1;
            // 
            // MyOkButton
            // 
            this.MyOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.MyOkButton.Location = new System.Drawing.Point(221, 272);
            this.MyOkButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MyOkButton.Name = "MyOkButton";
            this.MyOkButton.Size = new System.Drawing.Size(100, 29);
            this.MyOkButton.TabIndex = 4;
            this.MyOkButton.Text = "&OK";
            this.MyOkButton.UseVisualStyleBackColor = true;
            this.MyOkButton.Click += new System.EventHandler(this.MyOkButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(329, 272);
            this.MyCancelButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(100, 29);
            this.MyCancelButton.TabIndex = 5;
            this.MyCancelButton.Text = "&Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            this.MyCancelButton.Click += new System.EventHandler(this.MyCancelButton_Click);
            // 
            // DrivesLabel
            // 
            this.DrivesLabel.AutoSize = true;
            this.DrivesLabel.Location = new System.Drawing.Point(15, 72);
            this.DrivesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DrivesLabel.Name = "DrivesLabel";
            this.DrivesLabel.Size = new System.Drawing.Size(42, 15);
            this.DrivesLabel.TabIndex = 2;
            this.DrivesLabel.Text = "&Drives:";
            // 
            // DrivesListBox
            // 
            this.DrivesListBox.FormattingEnabled = true;
            this.DrivesListBox.ItemHeight = 15;
            this.DrivesListBox.Location = new System.Drawing.Point(15, 92);
            this.DrivesListBox.Name = "DrivesListBox";
            this.DrivesListBox.Size = new System.Drawing.Size(618, 154);
            this.DrivesListBox.TabIndex = 3;
            this.DrivesListBox.SelectedIndexChanged += new System.EventHandler(this.DrivesListBox_SelectedIndexChanged);
            this.DrivesListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DrivesListBox_MouseDoubleClick);
            // 
            // GotoWindow
            // 
            this.AcceptButton = this.MyOkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(650, 317);
            this.ControlBox = false;
            this.Controls.Add(this.DrivesListBox);
            this.Controls.Add(this.DrivesLabel);
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.MyOkButton);
            this.Controls.Add(this.PathTextBox);
            this.Controls.Add(this.PathLabel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GotoWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Goto";
            this.Load += new System.EventHandler(this.GotoWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PathLabel;
        private System.Windows.Forms.Button MyOkButton;
        private System.Windows.Forms.Button MyCancelButton;
        private System.Windows.Forms.Label DrivesLabel;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.ListBox DrivesListBox;
    }
}