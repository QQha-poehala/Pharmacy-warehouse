using database;
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

namespace Setting
{
    public partial class SettingForm : Form
    {
        DataB b = new DataB();
        int id_user;
        bool W, E, D;
        public SettingForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            RightsFunction();
            if (W == false)
            {
                buttonNew.Enabled = false;
            }
            if (E == false)
            {
                buttonChange.Enabled = false;
            }
            if (D == false)
            {
                buttonDel.Enabled = false;
            }
            if (E == false || W == false)
            {
                buttonChoice.Enabled = false;
            }
            Get_id_user();
            Add_ComboboxItem();
            buttonChange.Enabled = false;
            buttonDel.Enabled = false;
            button3.Enabled = false;
        }
        // Авторизация прав пользователя на меню Улица.
        private void RightsFunction()
        {
            string qwertystring = $"select * from Rights where ID_Menu = 2";
            OleDbCommand command = new OleDbCommand(qwertystring, b.getConnection());
            b.openConnection();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                W = Convert.ToBoolean(reader.GetBoolean(2));
                E = Convert.ToBoolean(reader.GetBoolean(3));
                D = Convert.ToBoolean(reader.GetBoolean(4));
            }
            reader.Close();
            b.closeConnection();
        }
        private void Get_id_user()
        {
            string qwertystring = $"select * from Rights where ID_Menu = 2";
            OleDbCommand command = new OleDbCommand(qwertystring, b.getConnection());
            b.openConnection();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id_user = Convert.ToInt32(reader.GetInt32(5));
            }
            reader.Close();
            b.closeConnection();
        }
        private void Add_ComboboxItem()
        {
            // Поиск Логинов и добавление вкладок.
            var qwery1 = $"select * from Users";
            var command = new OleDbCommand(qwery1, b.getConnection());
            b.openConnection();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["Логин"].ToString().Trim());
            }
            reader.Close();
            b.closeConnection();
        }


        // Отчистка таблицы.
        private void ClearFields()
        {
            checkBox1_1.Checked = false; checkBox1_2.Checked = false; checkBox1_3.Checked = false; checkBox1_4.Checked = false;
            checkBox2_1.Checked = false; checkBox2_2.Checked = false; checkBox2_3.Checked = false; checkBox2_4.Checked = false;
            checkBox3_1.Checked = false; checkBox3_2.Checked = false; checkBox3_3.Checked = false; checkBox3_4.Checked = false;
            checkBox4_1.Checked = false; checkBox4_2.Checked = false; checkBox4_3.Checked = false; checkBox4_4.Checked = false;
            checkBox5_1.Checked = false; checkBox5_2.Checked = false; checkBox5_3.Checked = false; checkBox5_4.Checked = false;
            checkBox6_1.Checked = false; checkBox6_2.Checked = false; checkBox6_3.Checked = false; checkBox6_4.Checked = false;
            checkBox7_1.Checked = false; checkBox7_2.Checked = false; checkBox7_3.Checked = false; checkBox7_4.Checked = false;
            checkBox8_1.Checked = false; checkBox8_2.Checked = false; checkBox8_3.Checked = false; checkBox8_4.Checked = false;
            checkBox9_1.Checked = false; checkBox9_2.Checked = false; checkBox9_3.Checked = false; checkBox9_4.Checked = false;
            checkBox10_1.Checked = false; checkBox10_2.Checked = false; checkBox10_3.Checked = false; checkBox10_4.Checked = false;
            checkBox11_1.Checked = false; checkBox11_2.Checked = false; checkBox11_3.Checked = false; checkBox11_4.Checked = false;
            checkBox12_1.Checked = false; checkBox12_2.Checked = false; checkBox12_3.Checked = false; checkBox12_4.Checked = false;
            checkBox13_1.Checked = false; checkBox13_2.Checked = false; checkBox13_3.Checked = false; checkBox13_4.Checked = false;
            checkBox14_1.Checked = false; checkBox14_2.Checked = false; checkBox14_3.Checked = false; checkBox14_4.Checked = false;
            checkBox15_1.Checked = false; checkBox15_2.Checked = false; checkBox15_3.Checked = false; checkBox15_4.Checked = false;
        }
        // Нажатие на клавишу "Отчистка".
        private void button1_Click(object sender, EventArgs e)
        {
            ClearFields();
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            Add_ComboboxItem();
            comboBox1.Enabled = true;
            buttonChange.Enabled = false;
            buttonDel.Enabled = false;
            button3.Enabled = false;

        }
        // Нажатие на "Сохранить".
        private void button3_Click(object sender, EventArgs e)
        {
            b.openConnection();
            int id = 0;
            string qwertystring = $"select * from Users where Логин = '{comboBox1.Text.Trim()}'";
            OleDbCommand command = new OleDbCommand(qwertystring, b.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            b.closeConnection();
            reader.Close();
            if (id < 0)
            {
                MessageBox.Show("Пользователь не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                b.openConnection();
                string changeQwery1 = $"update AccessRights set R = {checkBox1_1.Checked}, W ={checkBox1_2.Checked}, E = {checkBox1_3.Checked}, D= {checkBox1_4.Checked} where ID_User = {id} and ID_Menu =2";
                string changeQwery2 = $"update AccessRights set R = {checkBox2_1.Checked}, W ={checkBox2_2.Checked}, E = {checkBox2_3.Checked}, D= {checkBox2_4.Checked} where ID_User = {id} and ID_Menu =3";
                string changeQwery3 = $"update AccessRights set R = {checkBox3_1.Checked}, W ={checkBox3_2.Checked}, E = {checkBox3_3.Checked}, D= {checkBox3_4.Checked} where ID_User = {id} and ID_Menu =5";
                string changeQwery4 = $"update AccessRights set R = {checkBox4_1.Checked}, W ={checkBox4_2.Checked}, E = {checkBox4_3.Checked}, D= {checkBox4_4.Checked} where ID_User = {id} and ID_Menu =6";
                string changeQwery5 = $"update AccessRights set R = {checkBox5_1.Checked}, W ={checkBox5_2.Checked}, E = {checkBox5_3.Checked}, D= {checkBox5_4.Checked} where ID_User = {id} and ID_Menu =7";
                string changeQwery6 = $"update AccessRights set R = {checkBox6_1.Checked}, W ={checkBox6_2.Checked}, E = {checkBox6_3.Checked}, D= {checkBox6_4.Checked} where ID_User = {id} and ID_Menu =8";
                string changeQwery7 = $"update AccessRights set R = {checkBox7_1.Checked}, W ={checkBox7_2.Checked}, E = {checkBox7_3.Checked}, D= {checkBox7_4.Checked} where ID_User = {id} and ID_Menu =9";
                string changeQwery8 = $"update AccessRights set R = {checkBox8_1.Checked}, W ={checkBox8_2.Checked}, E = {checkBox8_3.Checked}, D= {checkBox8_4.Checked} where ID_User = {id} and ID_Menu =10";
                string changeQwery9 = $"update AccessRights set R = {checkBox9_1.Checked}, W ={checkBox9_2.Checked}, E = {checkBox9_3.Checked}, D= {checkBox9_4.Checked} where ID_User = {id} and ID_Menu =14";
                string changeQwery10 = $"update AccessRights set R = {checkBox10_1.Checked}, W ={checkBox10_2.Checked}, E = {checkBox10_3.Checked}, D= {checkBox10_4.Checked} where ID_User = {id} and ID_Menu =16";
                string changeQwery11 = $"update AccessRights set R = {checkBox11_1.Checked}, W ={checkBox11_2.Checked}, E = {checkBox11_3.Checked}, D= {checkBox11_4.Checked} where ID_User = {id} and ID_Menu =17";
                string changeQwery12 = $"update AccessRights set R = {checkBox12_1.Checked}, W ={checkBox12_2.Checked}, E = {checkBox12_3.Checked}, D= {checkBox12_4.Checked} where ID_User = {id} and ID_Menu =18";
                string changeQwery13 = $"update AccessRights set R = {checkBox13_1.Checked}, W ={checkBox13_2.Checked}, E = {checkBox13_3.Checked}, D= {checkBox13_4.Checked} where ID_User = {id} and ID_Menu =19";
                string changeQwery14 = $"update AccessRights set R = {checkBox14_1.Checked}, W ={checkBox14_2.Checked}, E = {checkBox14_3.Checked}, D= {checkBox14_4.Checked} where ID_User = {id} and ID_Menu =22";
                string changeQwery15 = $"update AccessRights set R = {checkBox15_1.Checked}, W ={checkBox15_2.Checked}, E = {checkBox15_3.Checked}, D= {checkBox15_4.Checked} where ID_User = {id} and ID_Menu =23";
                var command1 = new OleDbCommand(changeQwery1, b.getConnection());
                var command2 = new OleDbCommand(changeQwery2, b.getConnection());
                var command3 = new OleDbCommand(changeQwery3, b.getConnection());
                var command4 = new OleDbCommand(changeQwery4, b.getConnection());
                var command5 = new OleDbCommand(changeQwery5, b.getConnection());
                var command6 = new OleDbCommand(changeQwery6, b.getConnection());
                var command7 = new OleDbCommand(changeQwery7, b.getConnection());
                var command8 = new OleDbCommand(changeQwery8, b.getConnection());
                var command9 = new OleDbCommand(changeQwery9, b.getConnection());
                var command10 = new OleDbCommand(changeQwery10, b.getConnection());
                var command11 = new OleDbCommand(changeQwery11, b.getConnection());
                var command12 = new OleDbCommand(changeQwery12, b.getConnection());
                var command13 = new OleDbCommand(changeQwery13, b.getConnection());
                var command14 = new OleDbCommand(changeQwery14, b.getConnection());
                var command15 = new OleDbCommand(changeQwery15, b.getConnection());
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
                b.closeConnection();
                MessageBox.Show("Изменения внесены!", "Ок", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            AddUserForm frm = new AddUserForm();
            frm.Show();
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            Add_ComboboxItem();
            comboBox1.Enabled = true;
            buttonChange.Enabled = false;
            buttonDel.Enabled = false;
            button3.Enabled = false;
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            b.openConnection();

            int id = 0;
            string qwertystring = $"select * from Users where Логин = '{comboBox1.Text.Trim()}'";
            OleDbCommand command = new OleDbCommand(qwertystring, b.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
               id = reader.GetInt32(0);
            }
            reader.Close();
            var deleteQwery = $"delete from Users where ID = {id}";
            var command2 = new OleDbCommand(deleteQwery, b.getConnection());
            command2.ExecuteNonQuery();
            b.closeConnection();
            MessageBox.Show("Пользователь удалён", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            Add_ComboboxItem();
            comboBox1.Enabled = true;
            buttonChange.Enabled = false;
            buttonDel.Enabled = false;
            button3.Enabled = false;
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            b.openConnection();

            int id = 0;
            string qwertystring = $"select * from Users where Логин = '{comboBox1.Text.Trim()}'";
            OleDbCommand command = new OleDbCommand(qwertystring, b.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            reader.Close();
            b.closeConnection();
            ChangeUserForm frm = new ChangeUserForm(id);
            frm.Show();
        }

        // Поиск данных в базе данных.
        private void RefreshCheckBox(int id)
        {
            
            ClearFields();
            string qwertystring = $"select * from AccessRights where ID_User = {id}";
            OleDbCommand command = new OleDbCommand(qwertystring, b.getConnection());
            b.openConnection();
            OleDbDataReader reader = command.ExecuteReader();
            bool[,] mass = new bool[15,4];
            int i = 0;
            while (reader.Read())
            {
                //1,4,15,21
                if (reader.GetInt32(1) != 1 && reader.GetInt32(1) != 4 && reader.GetInt32(1) != 15 && reader.GetInt32(1) != 21)
                {
                    mass[i, 0] = reader.GetBoolean(2);
                    mass[i, 1] = reader.GetBoolean(3);
                    mass[i, 2] = reader.GetBoolean(4);
                    mass[i, 3] = reader.GetBoolean(5);
                    i++;
                }
            }
            reader.Close();
            b.closeConnection();
            checkBox1_1.Checked = mass[0, 0]; checkBox1_2.Checked = mass[0, 1]; checkBox1_3.Checked = mass[0, 2]; checkBox1_4.Checked = mass[0, 3];
            checkBox2_1.Checked = mass[1, 0]; checkBox2_2.Checked = mass[1, 1]; checkBox2_3.Checked = mass[1, 2]; checkBox2_4.Checked = mass[1, 3];
            checkBox3_1.Checked = mass[2, 0]; checkBox3_2.Checked = mass[2, 1]; checkBox3_3.Checked = mass[2, 2]; checkBox3_4.Checked = mass[2, 3];
            checkBox4_1.Checked = mass[3, 0]; checkBox4_2.Checked = mass[3, 1]; checkBox4_3.Checked = mass[3, 2]; checkBox4_4.Checked = mass[3, 3];
            checkBox5_1.Checked = mass[4, 0]; checkBox5_2.Checked = mass[4, 1]; checkBox5_3.Checked = mass[4, 2]; checkBox5_4.Checked = mass[4, 3];
            checkBox6_1.Checked = mass[5, 0]; checkBox6_2.Checked = mass[5, 1]; checkBox6_3.Checked = mass[5, 2]; checkBox6_4.Checked = mass[5, 3];
            checkBox7_1.Checked = mass[6, 0]; checkBox7_2.Checked = mass[6, 1]; checkBox7_3.Checked = mass[6, 2]; checkBox7_4.Checked = mass[6, 3];
            checkBox8_1.Checked = mass[7, 0]; checkBox8_2.Checked = mass[7, 1]; checkBox8_3.Checked = mass[7, 2]; checkBox8_4.Checked = mass[7, 3];
            checkBox9_1.Checked = mass[8, 0]; checkBox9_2.Checked = mass[8, 1]; checkBox9_3.Checked = mass[8, 2]; checkBox9_4.Checked = mass[8, 3];
            checkBox10_1.Checked = mass[9, 0]; checkBox10_2.Checked = mass[9, 1]; checkBox10_3.Checked = mass[9, 2]; checkBox10_4.Checked = mass[9, 3];
            checkBox11_1.Checked = mass[10, 0]; checkBox11_2.Checked = mass[10, 1]; checkBox11_3.Checked = mass[10, 2]; checkBox11_4.Checked = mass[10, 3];
            checkBox12_1.Checked = mass[11, 0]; checkBox12_2.Checked = mass[11, 1]; checkBox12_3.Checked = mass[11, 2]; checkBox12_4.Checked = mass[11, 3];
            checkBox13_1.Checked = mass[12, 0]; checkBox13_2.Checked = mass[12, 1]; checkBox13_3.Checked = mass[12, 2]; checkBox13_4.Checked = mass[12, 3];
            checkBox14_1.Checked = mass[13, 0]; checkBox14_2.Checked = mass[13, 1]; checkBox14_3.Checked = mass[13, 2]; checkBox14_4.Checked = mass[13, 3];
            checkBox15_1.Checked = mass[14, 0]; checkBox15_2.Checked = mass[14, 1]; checkBox15_3.Checked = mass[14, 2]; checkBox15_4.Checked = mass[14, 3];
        }
        // Нажатие на клавишу "Выбрать".
        private void button2_Click(object sender, EventArgs e)
        {
            b.openConnection();
            int id = 0;
            string qwertystring = $"select * from Users where Логин = '{comboBox1.Text.Trim()}'";
            OleDbCommand command = new OleDbCommand(qwertystring, b.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }
            b.closeConnection();
            reader.Close();
            if (id < 0)
            {
                MessageBox.Show("Пользователь не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                buttonChange.Enabled = true;
                buttonDel.Enabled = true;
                button3.Enabled = true;
                comboBox1.Enabled = false;
                RefreshCheckBox(id);
            }
        }
    }
}
