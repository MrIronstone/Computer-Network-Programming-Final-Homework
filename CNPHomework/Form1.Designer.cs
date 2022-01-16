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
            this.Map = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Map)).BeginInit();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            resources.ApplyResources(this.ConnectButton, "ConnectButton");
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ListenButton
            // 
            resources.ApplyResources(this.ListenButton, "ListenButton");
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.UseVisualStyleBackColor = true;
            this.ListenButton.Click += new System.EventHandler(this.ListenButton_Click);
            // 
            // SendButton
            // 
            resources.ApplyResources(this.SendButton, "SendButton");
            this.SendButton.Name = "SendButton";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // newText
            // 
            resources.ApplyResources(this.newText, "newText");
            this.newText.Name = "newText";
            // 
            // results
            // 
            resources.ApplyResources(this.results, "results");
            this.results.BackColor = System.Drawing.SystemColors.Window;
            this.results.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.results.FormattingEnabled = true;
            this.results.Name = "results";
            // 
            // Map
            // 
            resources.ApplyResources(this.Map, "Map");
            this.Map.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Map.Image = global::CNPHomework.Properties.Resources.CNP_Final_Homework_Map;
            this.Map.Name = "Map";
            this.Map.TabStop = false;
            this.Map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Map_MouseDown);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.Map);
            this.Controls.Add(this.results);
            this.Controls.Add(this.newText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ListenButton);
            this.Controls.Add(this.ConnectButton);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Map)).EndInit();
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
        private System.Windows.Forms.PictureBox Map;
        private System.Drawing.Bitmap mapBitMap;
    }
}

