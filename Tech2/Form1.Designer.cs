namespace KurovayaBD
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            CloseButton = new Label();
            WhitePanel = new Panel();
            PanelText = new Label();
            panel1 = new Panel();
            label2 = new Label();
            panel2 = new Panel();
            label1 = new Label();
            panel3 = new Panel();
            label3 = new Label();
            Password = new TextBox();
            UserName = new TextBox();
            UserNameLabel = new Label();
            PasswordLabel = new Label();
            CancelButton = new Button();
            EnterButton = new Button();
            statusStrip1 = new StatusStrip();
            CurrentLanguageLabel = new ToolStripStatusLabel();
            toolClearLabel = new ToolStripStatusLabel();
            Nothing = new ToolStripStatusLabel();
            CapsLockFlagLabel = new ToolStripStatusLabel();
            pictureBox1 = new PictureBox();
            WhitePanel.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // CloseButton
            // 
            CloseButton.AutoSize = true;
            CloseButton.Cursor = Cursors.Hand;
            CloseButton.Font = new Font("Trebuchet MS", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            CloseButton.Location = new Point(500, -1);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(20, 23);
            CloseButton.TabIndex = 0;
            CloseButton.Text = "X";
            CloseButton.Click += CloseButton_Click;
            CloseButton.MouseEnter += CloseButton_MouseEnter;
            CloseButton.MouseLeave += CloseButton_MouseLeave;
            // 
            // WhitePanel
            // 
            WhitePanel.BackColor = SystemColors.Control;
            WhitePanel.BorderStyle = BorderStyle.FixedSingle;
            WhitePanel.Controls.Add(PanelText);
            WhitePanel.Controls.Add(CloseButton);
            WhitePanel.Location = new Point(0, 0);
            WhitePanel.Name = "WhitePanel";
            WhitePanel.Size = new Size(521, 30);
            WhitePanel.TabIndex = 1;
            // 
            // PanelText
            // 
            PanelText.AutoSize = true;
            PanelText.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            PanelText.Location = new Point(3, 1);
            PanelText.Name = "PanelText";
            PanelText.Size = new Size(48, 23);
            PanelText.TabIndex = 1;
            PanelText.Text = "Вход";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Bisque;
            panel1.Controls.Add(label2);
            panel1.Location = new Point(12, 36);
            panel1.Name = "panel1";
            panel1.Size = new Size(498, 44);
            panel1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(312, 10);
            label2.Name = "label2";
            label2.Size = new Size(183, 22);
            label2.TabIndex = 1;
            label2.Text = "ИС Аптечный склад";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Gold;
            panel2.Controls.Add(label1);
            panel2.Location = new Point(12, 86);
            panel2.Name = "panel2";
            panel2.Size = new Size(498, 44);
            panel2.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(364, 10);
            label1.Name = "label1";
            label1.Size = new Size(131, 22);
            label1.TabIndex = 2;
            label1.Text = "Версия 1.0.0.1";
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.Control;
            panel3.Controls.Add(label3);
            panel3.Location = new Point(12, 136);
            panel3.Name = "panel3";
            panel3.Size = new Size(498, 44);
            panel3.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(180, 13);
            label3.Name = "label3";
            label3.Size = new Size(315, 22);
            label3.TabIndex = 2;
            label3.Text = "Введите имя пользователя и пароль";
            // 
            // Password
            // 
            Password.Location = new Point(195, 283);
            Password.Name = "Password";
            Password.PasswordChar = '*';
            Password.Size = new Size(315, 27);
            Password.TabIndex = 5;
            // 
            // UserName
            // 
            UserName.Location = new Point(195, 215);
            UserName.Name = "UserName";
            UserName.Size = new Size(315, 27);
            UserName.TabIndex = 6;
            // 
            // UserNameLabel
            // 
            UserNameLabel.AutoSize = true;
            UserNameLabel.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            UserNameLabel.Location = new Point(23, 215);
            UserNameLabel.Name = "UserNameLabel";
            UserNameLabel.Size = new Size(148, 20);
            UserNameLabel.TabIndex = 7;
            UserNameLabel.Text = "Имя пользователя";
            // 
            // PasswordLabel
            // 
            PasswordLabel.AutoSize = true;
            PasswordLabel.Font = new Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            PasswordLabel.Location = new Point(23, 283);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new Size(65, 20);
            PasswordLabel.TabIndex = 8;
            PasswordLabel.Text = "Пароль";
            // 
            // CancelButton
            // 
            CancelButton.Cursor = Cursors.Hand;
            CancelButton.Location = new Point(427, 331);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(94, 29);
            CancelButton.TabIndex = 9;
            CancelButton.Text = "Отмена";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // EnterButton
            // 
            EnterButton.Cursor = Cursors.Hand;
            EnterButton.Location = new Point(306, 331);
            EnterButton.Name = "EnterButton";
            EnterButton.Size = new Size(94, 29);
            EnterButton.TabIndex = 10;
            EnterButton.Text = "Вход";
            EnterButton.UseVisualStyleBackColor = true;
            EnterButton.Click += EnterButton_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { CurrentLanguageLabel, toolClearLabel, Nothing, CapsLockFlagLabel });
            statusStrip1.Location = new Point(0, 371);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(521, 26);
            statusStrip1.TabIndex = 11;
            statusStrip1.Text = "statusStrip1";
            // 
            // CurrentLanguageLabel
            // 
            CurrentLanguageLabel.Name = "CurrentLanguageLabel";
            CurrentLanguageLabel.Size = new Size(151, 20);
            CurrentLanguageLabel.Text = "toolStripStatusLabel1";
            // 
            // toolClearLabel
            // 
            toolClearLabel.Name = "toolClearLabel";
            toolClearLabel.Size = new Size(0, 20);
            // 
            // Nothing
            // 
            Nothing.Name = "Nothing";
            Nothing.Size = new Size(125, 20);
            Nothing.Text = "                             ";
            // 
            // CapsLockFlagLabel
            // 
            CapsLockFlagLabel.Name = "CapsLockFlagLabel";
            CapsLockFlagLabel.Size = new Size(121, 20);
            CapsLockFlagLabel.Text = "                            ";
            // 
            // pictureBox1
            // 
            pictureBox1.ErrorImage = null;
            pictureBox1.Image = Tech2.Properties.Resources._2023_03_14_194907;
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(15, 36);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(115, 116);
            pictureBox1.TabIndex = 12;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(521, 397);
            Controls.Add(pictureBox1);
            Controls.Add(statusStrip1);
            Controls.Add(EnterButton);
            Controls.Add(CancelButton);
            Controls.Add(PasswordLabel);
            Controls.Add(UserNameLabel);
            Controls.Add(UserName);
            Controls.Add(Password);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(WhitePanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Вход";
            Load += Form1_Load;
            WhitePanel.ResumeLayout(false);
            WhitePanel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label CloseButton;
        private Panel WhitePanel;
        private Label PanelText;
        private Panel panel1;
        private Label label2;
        private Panel panel2;
        private Label label1;
        private Panel panel3;
        private Label label3;
        private TextBox Password;
        private TextBox UserName;
        private Label UserNameLabel;
        private Label PasswordLabel;
        private Button CancelButton;
        private Button EnterButton;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel CurrentLanguageLabel;
        private ToolStripStatusLabel toolClearLabel;
        private ToolStripStatusLabel Nothing;
        private ToolStripStatusLabel CapsLockFlagLabel;
        private PictureBox pictureBox1;
    }
}