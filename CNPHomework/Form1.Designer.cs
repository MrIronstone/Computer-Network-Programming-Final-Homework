namespace CNPHomework
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ListenButton = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.newText = new System.Windows.Forms.TextBox();
            this.results = new System.Windows.Forms.ListBox();
            this.pictureBoxOfMap = new System.Windows.Forms.PictureBox();
            this.FlagsLeftToSewLabel = new System.Windows.Forms.Label();
            this.FlagsSewedLabel = new System.Windows.Forms.Label();
            this.ListBoxOfSewedFlags = new System.Windows.Forms.ListBox();
            this.FlagsLeftToSewTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOfMap)).BeginInit();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            this.ConnectButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ConnectButton.Location = new System.Drawing.Point(1133, 616);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 0;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ListenButton
            // 
            this.ListenButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ListenButton.Location = new System.Drawing.Point(1052, 616);
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.Size = new System.Drawing.Size(75, 23);
            this.ListenButton.TabIndex = 1;
            this.ListenButton.Text = "Listen";
            this.ListenButton.UseVisualStyleBackColor = true;
            this.ListenButton.Click += new System.EventHandler(this.ListenButton_Click);
            // 
            // SendButton
            // 
            this.SendButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SendButton.Location = new System.Drawing.Point(873, 744);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(120, 23);
            this.SendButton.TabIndex = 2;
            this.SendButton.Text = "Send Flags To Enemy";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(453, 627);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Enter text string:";
            // 
            // newText
            // 
            this.newText.Location = new System.Drawing.Point(456, 647);
            this.newText.Name = "newText";
            this.newText.Size = new System.Drawing.Size(159, 20);
            this.newText.TabIndex = 4;
            // 
            // results
            // 
            this.results.BackColor = System.Drawing.SystemColors.Window;
            this.results.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.results.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.results.FormattingEnabled = true;
            this.results.ItemHeight = 16;
            this.results.Location = new System.Drawing.Point(12, 599);
            this.results.Name = "results";
            this.results.Size = new System.Drawing.Size(418, 160);
            this.results.TabIndex = 5;
            // 
            // pictureBoxOfMap
            // 
            this.pictureBoxOfMap.Image = global::CNPHomework.Properties.Resources.CNP_Final_Homework_Map1;
            this.pictureBoxOfMap.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBoxOfMap.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxOfMap.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxOfMap.Name = "pictureBoxOfMap";
            this.pictureBoxOfMap.Size = new System.Drawing.Size(1215, 593);
            this.pictureBoxOfMap.TabIndex = 6;
            this.pictureBoxOfMap.TabStop = false;
            this.pictureBoxOfMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxOfMap_MouseDown);
            // 
            // FlagsLeftToSewLabel
            // 
            this.FlagsLeftToSewLabel.AutoSize = true;
            this.FlagsLeftToSewLabel.Location = new System.Drawing.Point(721, 627);
            this.FlagsLeftToSewLabel.Name = "FlagsLeftToSewLabel";
            this.FlagsLeftToSewLabel.Size = new System.Drawing.Size(93, 13);
            this.FlagsLeftToSewLabel.TabIndex = 7;
            this.FlagsLeftToSewLabel.Text = "Flags Left To Sew";
            this.FlagsLeftToSewLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FlagsSewedLabel
            // 
            this.FlagsSewedLabel.AutoSize = true;
            this.FlagsSewedLabel.Location = new System.Drawing.Point(870, 627);
            this.FlagsSewedLabel.Name = "FlagsSewedLabel";
            this.FlagsSewedLabel.Size = new System.Drawing.Size(68, 13);
            this.FlagsSewedLabel.TabIndex = 8;
            this.FlagsSewedLabel.Text = "Flags Sewed";
            // 
            // ListBoxOfSewedFlags
            // 
            this.ListBoxOfSewedFlags.FormattingEnabled = true;
            this.ListBoxOfSewedFlags.Location = new System.Drawing.Point(873, 643);
            this.ListBoxOfSewedFlags.Name = "ListBoxOfSewedFlags";
            this.ListBoxOfSewedFlags.Size = new System.Drawing.Size(120, 95);
            this.ListBoxOfSewedFlags.TabIndex = 9;
            // 
            // FlagsLeftToSewTextBox
            // 
            this.FlagsLeftToSewTextBox.Location = new System.Drawing.Point(820, 624);
            this.FlagsLeftToSewTextBox.Name = "FlagsLeftToSewTextBox";
            this.FlagsLeftToSewTextBox.Size = new System.Drawing.Size(33, 20);
            this.FlagsLeftToSewTextBox.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1220, 798);
            this.Controls.Add(this.FlagsLeftToSewTextBox);
            this.Controls.Add(this.ListBoxOfSewedFlags);
            this.Controls.Add(this.FlagsSewedLabel);
            this.Controls.Add(this.FlagsLeftToSewLabel);
            this.Controls.Add(this.pictureBoxOfMap);
            this.Controls.Add(this.results);
            this.Controls.Add(this.newText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ListenButton);
            this.Controls.Add(this.ConnectButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Computer Network Programming Final Homework";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOfMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button ListenButton;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newText;
        private System.Windows.Forms.ListBox results;
        private System.Drawing.Bitmap mapBitMap;
        private System.Windows.Forms.PictureBox pictureBoxOfMap;
        private System.Windows.Forms.Label FlagsLeftToSewLabel;
        private System.Windows.Forms.Label FlagsSewedLabel;
        private System.Windows.Forms.ListBox ListBoxOfSewedFlags;
        private System.Windows.Forms.TextBox FlagsLeftToSewTextBox;
    }
}

