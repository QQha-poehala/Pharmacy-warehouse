using System;
using database;
using Md5;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Xml.Linq;

namespace Setting
{
    public partial class ChangeUserForm : Form
    {
        DataB database = new DataB();
        public int ID;
        public ChangeUserForm(int id)
        {    
            ID = id;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            string qwertystring = $"select * from Users where ID = {id}";
            OleDbCommand command = new OleDbCommand(qwertystring, database.getConnection());
            database.openConnection();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader.GetString(1);
            }
            database.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.openConnection();
            
            var pass1 = textBox3.Text;
            var pass2 = textBox4.Text;
            // Проверка на не пустоту строк и запрос на изменение строки в бд.
            if (pass1.Length >= 0 && pass1 == pass2)
            {
                var changeQwery = $"update Users set Пароль = '{md5.hashPassword(pass1)}' where ID ={ID}";
                var command = new OleDbCommand(changeQwery, database.getConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Пароль изменён!", "Изменение записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();

            }
            else
            {
                MessageBox.Show("Неправильный ввод", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            database.closeConnection();
        }
    }
}
