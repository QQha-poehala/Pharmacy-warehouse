namespace Suppliers
{
    partial class SuppliersForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_search = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonChange = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.textBox_search);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(4, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1222, 65);
            this.panel1.TabIndex = 4;
            // 
            // textBox_search
            // 
            this.textBox_search.AcceptsReturn = true;
            this.textBox_search.ForeColor = System.Drawing.Color.Gray;
            this.textBox_search.Location = new System.Drawing.Point(880, 21);
            this.textBox_search.Name = "textBox_search";
            this.textBox_search.Size = new System.Drawing.Size(263, 22);
            this.textBox_search.TabIndex = 13;
            this.textBox_search.Text = "Поиск...";
            this.textBox_search.TextChanged += new System.EventHandler(this.textBox_search_TextChanged);
            this.textBox_search.GotFocus += new System.EventHandler(this.TextGotFocus);
            this.textBox_search.Leave += new System.EventHandler(this.TextLostFocus);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(718, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 59);
            this.button2.TabIndex = 12;
            this.button2.Text = "Обновить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(608, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 59);
            this.button1.TabIndex = 11;
            this.button1.Text = "Очистка";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(32, 15);
            this.label1.MinimumSize = new System.Drawing.Size(30, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 39);
            this.label1.TabIndex = 10;
            this.label1.Text = "Поставщики";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 94);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1182, 275);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.textBox8);
            this.panel2.Controls.Add(this.textBox7);
            this.panel2.Controls.Add(this.textBox6);
            this.panel2.Controls.Add(this.textBox5);
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.comboBox2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(43, 375);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(441, 404);
            this.panel2.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 156);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(115, 16);
            this.label12.TabIndex = 31;
            this.label12.Text = "Расчётный счёт:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(82, 122);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 16);
            this.label11.TabIndex = 30;
            this.label11.Text = "Банк:";
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Location = new System.Drawing.Point(144, 150);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(261, 22);
            this.textBox3.TabIndex = 29;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(144, 114);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(261, 24);
            this.comboBox1.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(84, 366);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 16);
            this.label10.TabIndex = 27;
            this.label10.Text = "ИНН:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(54, 330);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 16);
            this.label9.TabIndex = 26;
            this.label9.Text = "Телефон:";
            // 
            // textBox8
            // 
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox8.Location = new System.Drawing.Point(144, 360);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(261, 22);
            this.textBox8.TabIndex = 25;
            // 
            // textBox7
            // 
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Location = new System.Drawing.Point(144, 324);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(261, 22);
            this.textBox7.TabIndex = 24;
            // 
            // textBox6
            // 
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox6.Location = new System.Drawing.Point(144, 286);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(261, 22);
            this.textBox6.TabIndex = 23;
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.Location = new System.Drawing.Point(144, 252);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(261, 22);
            this.textBox5.TabIndex = 22;
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Location = new System.Drawing.Point(144, 218);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(261, 22);
            this.textBox4.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(51, 292);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 16);
            this.label8.TabIndex = 20;
            this.label8.Text = "Квартира:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(50, 258);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 16);
            this.label7.TabIndex = 19;
            this.label7.Text = "Строение:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(88, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 16);
            this.label6.TabIndex = 18;
            this.label6.Text = "Дом:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "Улица:";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(144, 183);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(261, 24);
            this.comboBox2.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(138, 0);
            this.label4.MinimumSize = new System.Drawing.Size(30, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 39);
            this.label4.TabIndex = 11;
            this.label4.Text = "Запись:";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(144, 75);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(261, 22);
            this.textBox2.TabIndex = 10;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(144, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(261, 22);
            this.textBox1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Название:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "ID:";
            // 
            // label13
            // 
            this.label13.AutoEllipsis = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(587, 394);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(330, 39);
            this.label13.TabIndex = 8;
            this.label13.Tag = "0";
            this.label13.Text = "Управление записями:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.buttonSave);
            this.panel3.Controls.Add(this.buttonChange);
            this.panel3.Controls.Add(this.buttonDel);
            this.panel3.Controls.Add(this.buttonNew);
            this.panel3.Location = new System.Drawing.Point(593, 456);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(368, 254);
            this.panel3.TabIndex = 9;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(99, 185);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(160, 43);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonChange
            // 
            this.buttonChange.Location = new System.Drawing.Point(99, 126);
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.Size = new System.Drawing.Size(160, 43);
            this.buttonChange.TabIndex = 2;
            this.buttonChange.Text = "Изменить";
            this.buttonChange.UseVisualStyleBackColor = true;
            this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(99, 68);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(160, 43);
            this.buttonDel.TabIndex = 1;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(99, 9);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(160, 43);
            this.buttonNew.TabIndex = 0;
            this.buttonNew.Text = "Новая запись";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // SuppliersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 791);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "SuppliersForm";
            this.Text = "Поставщики";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            this.Load += new System.EventHandler(this.SuppliersForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_search;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox2;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonChange;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonNew;
    }
}