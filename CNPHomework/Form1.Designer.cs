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
            this.label1 = new System.Windows.Forms.Label();
            this.AttackText = new System.Windows.Forms.TextBox();
            this.results = new System.Windows.Forms.ListBox();
            this.pictureBoxOfMap = new System.Windows.Forms.PictureBox();
            this.FlagsLeftToSewLabel = new System.Windows.Forms.Label();
            this.FlagsSewedLabel = new System.Windows.Forms.Label();
            this.ListBoxOfSewedFlags = new System.Windows.Forms.ListBox();
            this.FlagsLeftToSewTextBox = new System.Windows.Forms.TextBox();
            this.AttackButton = new System.Windows.Forms.Button();
            this.ReadyButton = new System.Windows.Forms.Button();
            this.TurnLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DisconnectButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOfMap)).BeginInit();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            this.ConnectButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ConnectButton.Location = new System.Drawing.Point(697, 12);
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
            this.ListenButton.Location = new System.Drawing.Point(616, 12);
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.Size = new System.Drawing.Size(75, 23);
            this.ListenButton.TabIndex = 1;
            this.ListenButton.Text = "Listen";
            this.ListenButton.UseVisualStyleBackColor = true;
            this.ListenButton.Click += new System.EventHandler(this.ListenButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(631, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Type Attack Coordinates";
            // 
            // AttackText
            // 
            this.AttackText.Location = new System.Drawing.Point(634, 319);
            this.AttackText.Name = "AttackText";
            this.AttackText.ReadOnly = true;
            this.AttackText.Size = new System.Drawing.Size(129, 20);
            this.AttackText.TabIndex = 4;
            this.AttackText.TextChanged += new System.EventHandler(this.AttackText_TextChanged);
            // 
            // results
            // 
            this.results.BackColor = System.Drawing.SystemColors.Window;
            this.results.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.results.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.results.FormattingEnabled = true;
            this.results.ItemHeight = 16;
            this.results.Location = new System.Drawing.Point(0, 303);
            this.results.Name = "results";
            this.results.Size = new System.Drawing.Size(608, 96);
            this.results.TabIndex = 5;
            // 
            // pictureBoxOfMap
            // 
            this.pictureBoxOfMap.Image = global::CNPHomework.Properties.Resources.CNP_Homework_Map;
            this.pictureBoxOfMap.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBoxOfMap.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxOfMap.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxOfMap.Name = "pictureBoxOfMap";
            this.pictureBoxOfMap.Size = new System.Drawing.Size(608, 292);
            this.pictureBoxOfMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxOfMap.TabIndex = 6;
            this.pictureBoxOfMap.TabStop = false;
            this.pictureBoxOfMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxOfMap_MouseDown);
            // 
            // FlagsLeftToSewLabel
            // 
            this.FlagsLeftToSewLabel.AutoSize = true;
            this.FlagsLeftToSewLabel.Location = new System.Drawing.Point(631, 84);
            this.FlagsLeftToSewLabel.Name = "FlagsLeftToSewLabel";
            this.FlagsLeftToSewLabel.Size = new System.Drawing.Size(93, 13);
            this.FlagsLeftToSewLabel.TabIndex = 7;
            this.FlagsLeftToSewLabel.Text = "Flags Left To Sew";
            this.FlagsLeftToSewLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FlagsSewedLabel
            // 
            this.FlagsSewedLabel.AutoSize = true;
            this.FlagsSewedLabel.Location = new System.Drawing.Point(631, 116);
            this.FlagsSewedLabel.Name = "FlagsSewedLabel";
            this.FlagsSewedLabel.Size = new System.Drawing.Size(68, 13);
            this.FlagsSewedLabel.TabIndex = 8;
            this.FlagsSewedLabel.Text = "Flags Sewed";
            // 
            // ListBoxOfSewedFlags
            // 
            this.ListBoxOfSewedFlags.FormattingEnabled = true;
            this.ListBoxOfSewedFlags.Location = new System.Drawing.Point(634, 132);
            this.ListBoxOfSewedFlags.Name = "ListBoxOfSewedFlags";
            this.ListBoxOfSewedFlags.Size = new System.Drawing.Size(129, 95);
            this.ListBoxOfSewedFlags.TabIndex = 9;
            // 
            // FlagsLeftToSewTextBox
            // 
            this.FlagsLeftToSewTextBox.Location = new System.Drawing.Point(730, 81);
            this.FlagsLeftToSewTextBox.Name = "FlagsLeftToSewTextBox";
            this.FlagsLeftToSewTextBox.ReadOnly = true;
            this.FlagsLeftToSewTextBox.Size = new System.Drawing.Size(33, 20);
            this.FlagsLeftToSewTextBox.TabIndex = 10;
            // 
            // AttackButton
            // 
            this.AttackButton.Location = new System.Drawing.Point(634, 345);
            this.AttackButton.Name = "AttackButton";
            this.AttackButton.Size = new System.Drawing.Size(129, 23);
            this.AttackButton.TabIndex = 11;
            this.AttackButton.Text = "Attack!";
            this.AttackButton.UseVisualStyleBackColor = true;
            this.AttackButton.Click += new System.EventHandler(this.AttackButton_Click);
            // 
            // ReadyButton
            // 
            this.ReadyButton.Location = new System.Drawing.Point(634, 233);
            this.ReadyButton.Name = "ReadyButton";
            this.ReadyButton.Size = new System.Drawing.Size(129, 23);
            this.ReadyButton.TabIndex = 12;
            this.ReadyButton.Text = "Ready";
            this.ReadyButton.UseVisualStyleBackColor = true;
            this.ReadyButton.Click += new System.EventHandler(this.ReadyButton_Click);
            // 
            // TurnLabel
            // 
            this.TurnLabel.AutoSize = true;
            this.TurnLabel.Location = new System.Drawing.Point(631, 386);
            this.TurnLabel.Name = "TurnLabel";
            this.TurnLabel.Size = new System.Drawing.Size(57, 13);
            this.TurnLabel.TabIndex = 13;
            this.TurnLabel.Text = "Your Turn!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(631, 303);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "or Click on Map";
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Location = new System.Drawing.Point(660, 41);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(75, 23);
            this.DisconnectButton.TabIndex = 15;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(781, 411);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TurnLabel);
            this.Controls.Add(this.ReadyButton);
            this.Controls.Add(this.AttackButton);
            this.Controls.Add(this.FlagsLeftToSewTextBox);
            this.Controls.Add(this.ListBoxOfSewedFlags);
            this.Controls.Add(this.FlagsSewedLabel);
            this.Controls.Add(this.FlagsLeftToSewLabel);
            this.Controls.Add(this.pictureBoxOfMap);
            this.Controls.Add(this.results);
            this.Controls.Add(this.AttackText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListenButton);
            this.Controls.Add(this.ConnectButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Computer Network Programming Final Homework";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOfMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button ListenButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AttackText;
        private System.Windows.Forms.ListBox results;
        private System.Drawing.Bitmap mapBitMap;
        private System.Windows.Forms.PictureBox pictureBoxOfMap;
        private System.Windows.Forms.Label FlagsLeftToSewLabel;
        private System.Windows.Forms.Label FlagsSewedLabel;
        private System.Windows.Forms.ListBox ListBoxOfSewedFlags;
        private System.Windows.Forms.TextBox FlagsLeftToSewTextBox;
        private System.Windows.Forms.Button AttackButton;
        private System.Windows.Forms.Button ReadyButton;
        private System.Windows.Forms.Label TurnLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DisconnectButton;
    }
}

