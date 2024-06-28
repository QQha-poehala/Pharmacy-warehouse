using database;
using Md5;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Setting
{
    public partial class AddUserForm : Form
    {
        DataB database = new DataB();
        public AddUserForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.openConnection();
            int id = 0;
            var name = textBox1.Text;
            var pass1 = textBox2.Text;
            var pass2 = textBox3.Text;
            // Проверка на не пустоту строк и запрос на добавление новой строки в бд.
            if (1<=name.Length && name.Length <= 20 && pass1.Length >=0 && pass1 == pass2)
            {
                string qwerty = $"select * from Users where Логин = '{name}'";
                OleDbCommand com = new OleDbCommand(qwerty, database.getConnection());
                OleDbDataReader rea = com.ExecuteReader();
                int count = 0;
                while (rea.Read())
                {
                    count++;
                }
                rea.Close();
                if (count == 0)
                {
                    string addQwery = $"insert into Users (Логин, Пароль) values ('{name}', '{md5.hashPassword(pass1)}')";
                    var comm = new OleDbCommand(addQwery, database.getConnection());
                    comm.ExecuteNonQuery();
                    string qwertystring = $"select ID from Users where Логин = '{name}'";
                    OleDbCommand command = new OleDbCommand(qwertystring, database.getConnection());
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }
                    reader.Close();
                    // Добавление стандартных прав пользователя.
                    string addRight1 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 1, 1, 0, 0, 0)";
                    string addRight2 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 2, 1, 0, 0, 0)";
                    string addRight3 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 3, 1, 1, 1, 1)";
                    string addRight4 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 4, 1, 0, 0, 0)";
                    string addRight5 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 5, 1, 0, 0, 0)";
                    string addRight6 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 6, 0, 0, 0, 0)";
                    string addRight7 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 7, 1, 0, 0, 0)";
                    string addRight8 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 8, 1, 1, 1, 1)";
                    string addRight9 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 9, 0, 0, 0, 0)";
                    string addRight10 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 10, 0, 0, 0, 0)";
                    string addRight11 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 14, 0, 0, 0, 0)";
                    string addRight12 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 15, 1, 0, 0, 0)";
                    string addRight13 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 16, 1, 0, 0, 0)";
                    string addRight14 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 17, 1, 0, 0, 0)";
                    string addRight15 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 18, 1, 0, 0, 0)";
                    string addRight16 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 19, 1, 0, 0, 0)";
                    string addRight17 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 21, 1, 0, 0, 0)";
                    string addRight18 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 22, 1, 0, 0, 0)";
                    string addRight19 = $"insert into AccessRights(ID_User, ID_Menu, R, W, E, D) values({id}, 23, 1, 0, 0, 0)";
                    var command1 = new OleDbCommand(addRight1, database.getConnection());
                    var command2 = new OleDbCommand(addRight2, database.getConnection());
                    var command3 = new OleDbCommand(addRight3, database.getConnection());
                    var command4 = new OleDbCommand(addRight4, database.getConnection());
                    var command5 = new OleDbCommand(addRight5, database.getConnection());
                    var command6 = new OleDbCommand(addRight6, database.getConnection());
                    var command7 = new OleDbCommand(addRight7, database.getConnection());
                    var command8 = new OleDbCommand(addRight8, database.getConnection());
                    var command9 = new OleDbCommand(addRight9, database.getConnection());
                    var command10 = new OleDbCommand(addRight10, database.getConnection());
                    var command11 = new OleDbCommand(addRight11, database.getConnection());
                    var command12 = new OleDbCommand(addRight12, database.getConnection());
                    var command13 = new OleDbCommand(addRight13, database.getConnection());
                    var command14 = new OleDbCommand(addRight14, database.getConnection());
                    var command15 = new OleDbCommand(addRight15, database.getConnection());
                    var command16 = new OleDbCommand(addRight16, database.getConnection());
                    var command17 = new OleDbCommand(addRight17, database.getConnection());
                    var command18 = new OleDbCommand(addRight18, database.getConnection());
                    var command19 = new OleDbCommand(addRight19, database.getConnection());
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    command3.ExecuteNonQuery();
                    command4.ExecuteNonQuery();
                    command5.ExecuteNonQuery();
                    command6.ExecuteNonQuery();
                    command7.ExecuteNonQuery();
                    command8.ExecuteNonQuery();
                    command9.ExecuteNonQuery();
                    command10.ExecuteNonQuery();
                    command11.ExecuteNonQuery();
                    command12.ExecuteNonQuery();
                    command13.ExecuteNonQuery();
                    command14.ExecuteNonQuery();
                    command15.ExecuteNonQuery();
                    command16.ExecuteNonQuery();
                    command17.ExecuteNonQuery();
                    command18.ExecuteNonQuery();
                    command19.ExecuteNonQuery();
                    MessageBox.Show("Пользователь создан!", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Неправильный ввод", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            database.closeConnection();
        }
    }
}
