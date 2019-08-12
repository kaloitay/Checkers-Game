namespace CheckersWindowsUI
{
    public partial class GameSettingsForm
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
            this.BoardSize = new System.Windows.Forms.Label();
            this.RadioButton6x6 = new System.Windows.Forms.RadioButton();
            this.RadioButton8x8 = new System.Windows.Forms.RadioButton();
            this.RadioButton10x10 = new System.Windows.Forms.RadioButton();
            this.Players = new System.Windows.Forms.Label();
            this.Player1 = new System.Windows.Forms.Label();
            this.Player2 = new System.Windows.Forms.Label();
            this.Player2CheckBox = new System.Windows.Forms.CheckBox();
            this.Player2TextBox = new System.Windows.Forms.TextBox();
            this.Player1TextBox = new System.Windows.Forms.TextBox();
            this.Done = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BoardSize
            // 
            this.BoardSize.AutoSize = true;
            this.BoardSize.Location = new System.Drawing.Point(16, 19);
            this.BoardSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BoardSize.Name = "BoardSize";
            this.BoardSize.Size = new System.Drawing.Size(58, 13);
            this.BoardSize.TabIndex = 0;
            this.BoardSize.Text = "Board Size";
            // 
            // RadioButton6x6
            // 
            this.RadioButton6x6.AutoSize = true;
            this.RadioButton6x6.Location = new System.Drawing.Point(32, 43);
            this.RadioButton6x6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RadioButton6x6.Name = "RadioButton6x6";
            this.RadioButton6x6.Size = new System.Drawing.Size(48, 17);
            this.RadioButton6x6.TabIndex = 1;
            this.RadioButton6x6.Text = "6 x 6";
            this.RadioButton6x6.UseVisualStyleBackColor = true;
            this.RadioButton6x6.Click += new System.EventHandler(this.RadioButton6x6_Click);
            // 
            // RadioButton8x8
            // 
            this.RadioButton8x8.AutoSize = true;
            this.RadioButton8x8.Checked = true;
            this.RadioButton8x8.Location = new System.Drawing.Point(92, 43);
            this.RadioButton8x8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RadioButton8x8.Name = "RadioButton8x8";
            this.RadioButton8x8.Size = new System.Drawing.Size(48, 17);
            this.RadioButton8x8.TabIndex = 2;
            this.RadioButton8x8.TabStop = true;
            this.RadioButton8x8.Text = "8 x 8";
            this.RadioButton8x8.UseVisualStyleBackColor = true;
            this.RadioButton8x8.Click += new System.EventHandler(this.RadioButton8x8_Click);
            // 
            // RadioButton10x10
            // 
            this.RadioButton10x10.AutoSize = true;
            this.RadioButton10x10.Location = new System.Drawing.Point(152, 43);
            this.RadioButton10x10.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RadioButton10x10.Name = "RadioButton10x10";
            this.RadioButton10x10.Size = new System.Drawing.Size(60, 17);
            this.RadioButton10x10.TabIndex = 3;
            this.RadioButton10x10.Text = "10 x 10";
            this.RadioButton10x10.UseVisualStyleBackColor = true;
            this.RadioButton10x10.Click += new System.EventHandler(this.RadioButton10x10_Click);
            // 
            // Players
            // 
            this.Players.AutoSize = true;
            this.Players.Location = new System.Drawing.Point(16, 76);
            this.Players.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Players.Name = "Players";
            this.Players.Size = new System.Drawing.Size(44, 13);
            this.Players.TabIndex = 4;
            this.Players.Text = "Players:";
            // 
            // Player1
            // 
            this.Player1.AutoSize = true;
            this.Player1.Location = new System.Drawing.Point(30, 104);
            this.Player1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Player1.Name = "Player1";
            this.Player1.Size = new System.Drawing.Size(48, 13);
            this.Player1.TabIndex = 5;
            this.Player1.Text = "Player 1:";
            // 
            // Player2
            // 
            this.Player2.AutoSize = true;
            this.Player2.Location = new System.Drawing.Point(49, 130);
            this.Player2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Player2.Name = "Player2";
            this.Player2.Size = new System.Drawing.Size(48, 13);
            this.Player2.TabIndex = 8;
            this.Player2.Text = "Player 2:";
            // 
            // Player2CheckBox
            // 
            this.Player2CheckBox.AutoSize = true;
            this.Player2CheckBox.Location = new System.Drawing.Point(32, 130);
            this.Player2CheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Player2CheckBox.Name = "Player2CheckBox";
            this.Player2CheckBox.Size = new System.Drawing.Size(15, 14);
            this.Player2CheckBox.TabIndex = 10;
            this.Player2CheckBox.UseVisualStyleBackColor = true;
            this.Player2CheckBox.CheckedChanged += new System.EventHandler(this.Player2CheckBox_CheckedChanged);
            // 
            // Player2TextBox
            // 
            this.Player2TextBox.Enabled = false;
            this.Player2TextBox.Location = new System.Drawing.Point(116, 127);
            this.Player2TextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Player2TextBox.Name = "Player2TextBox";
            this.Player2TextBox.Size = new System.Drawing.Size(94, 20);
            this.Player2TextBox.TabIndex = 11;
            this.Player2TextBox.Text = "[Computer]";
            // 
            // Player1TextBox
            // 
            this.Player1TextBox.Location = new System.Drawing.Point(116, 102);
            this.Player1TextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Player1TextBox.Name = "Player1TextBox";
            this.Player1TextBox.Size = new System.Drawing.Size(94, 20);
            this.Player1TextBox.TabIndex = 12;
            // 
            // Done
            // 
            this.Done.Location = new System.Drawing.Point(128, 158);
            this.Done.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Done.Name = "Done";
            this.Done.Size = new System.Drawing.Size(80, 28);
            this.Done.TabIndex = 13;
            this.Done.Text = "Done";
            this.Done.UseVisualStyleBackColor = true;
            this.Done.Click += new System.EventHandler(this.Done_Click);
            // 
            // GameSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 203);
            this.Controls.Add(this.Done);
            this.Controls.Add(this.Player1TextBox);
            this.Controls.Add(this.Player2TextBox);
            this.Controls.Add(this.Player2CheckBox);
            this.Controls.Add(this.Player2);
            this.Controls.Add(this.Player1);
            this.Controls.Add(this.Players);
            this.Controls.Add(this.RadioButton10x10);
            this.Controls.Add(this.RadioButton8x8);
            this.Controls.Add(this.RadioButton6x6);
            this.Controls.Add(this.BoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "GameSettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BoardSize;
        private System.Windows.Forms.RadioButton RadioButton6x6;
        private System.Windows.Forms.RadioButton RadioButton8x8;
        private System.Windows.Forms.RadioButton RadioButton10x10;
        private System.Windows.Forms.Label Players;
        private System.Windows.Forms.Label Player1;
        private System.Windows.Forms.Label Player2;
        private System.Windows.Forms.CheckBox Player2CheckBox;
        private System.Windows.Forms.TextBox Player2TextBox;
        private System.Windows.Forms.TextBox Player1TextBox;
        private System.Windows.Forms.Button Done;
    }
}