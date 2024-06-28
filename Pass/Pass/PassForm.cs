using database;
using Md5;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Pass
{
    public partial class PassForm : Form
    {
        // Таймер для постоянной проверки состояния формы.
        System.Windows.Forms.Timer formTimer = new System.Windows.Forms.Timer();
        bool W, E;
        DataB dataBase = new DataB();
        public PassForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            RightsFunction();
            if (W == false && E == false)
            {
                button1.Enabled = false;
            }
            ActiveControl = textBox1;
        }
        // Авторизация прав пользователя на меню Смена пароля.
        private void RightsFunction()
        {
            string qwertystring = $"select * from Rights where ID_Menu = 3";
            OleDbCommand command = new OleDbCommand(qwertystring, dataBase.getConnection());
            dataBase.openConnection();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                W = Convert.ToBoolean(reader.GetBoolean(2));
                E = Convert.ToBoolean(reader.GetBoolean(3));
            }
            reader.Close();
            dataBase.closeConnection();
        }
        private void PassForm_Load(object sender, EventArgs e)
        {
            formTimer.Interval = 500;
            formTimer.Start();
            formTimer.Tick += new EventHandler(FormTimer_Tick);
        }
        private void FormTimer_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = (Console.CapsLock ? "Клавиша CapsLock нажата" : "");
            if (InputLanguage.CurrentInputLanguage.LayoutName == "США")
                toolStripStatusLabel1.Text = "Язык ввода Английский";
            else if (InputLanguage.CurrentInputLanguage.LayoutName == "Русская")
                toolStripStatusLabel1.Text = "Язык ввода Русский";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Считывание введенного логина и пароля.
            var pass1 = md5.hashPassword(textBox1.Text);
            var pass2 = md5.hashPassword(textBox2.Text);
            var pass3 = md5.hashPassword(textBox3.Text);
            int id=0;
            string pass_bd="";
            // получение ID пользователя
            var qwery1 = $"select * from Rights where ID_Menu = 3";
            dataBase.openConnection();
            var command = new OleDbCommand(qwery1, dataBase.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = Convert.ToInt32(reader.GetInt32(5));
            }
            dataBase.closeConnection();
            reader.Close();

            // получение старого пароля по ID пользователя
            var qwery2 = $"select * from Users where ID = {id}";
            dataBase.openConnection();
            var command2 = new OleDbCommand(qwery2, dataBase.getConnection());
            OleDbDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                pass_bd = reader2.GetString(2).Trim();
            }
            dataBase.closeConnection();
            reader2.Close();
            // Проверка на правильность прежнего пароля.
            if ( pass1 == pass_bd)
            {
                // Проверка на равенство.
                if (pass2 == pass3)
                {
                    dataBase.openConnection();
                    string changeQwery = $"update Users set Пароль = '{pass3}' where ID ={id}";
                    var command3 = new OleDbCommand(changeQwery, dataBase.getConnection());
                    command3.ExecuteNonQuery();
                    MessageBox.Show("Пароль изменён!", "ОК", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Введены разные пароли", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Прежний пароль введен неправтльно!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
            
        }

    }
}
