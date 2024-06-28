using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.OleDb;

namespace Catalog
{
    public partial class Add_Form : Form
    {
        DataB database = new DataB();
        public Add_Form()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            dateTimePicker1.Text = "";
            database.openConnection();
            // Поиск Категорий из бд.
            var qwery1 = $"select * from Категория";
            var command = new OleDbCommand(qwery1, database.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBoxCat.Items.Add(reader["Наименование"].ToString());
            }
            reader.Close();
            // Поиск Вида упаковки из бд.
            var qwery2 = $"select * from ВидУпаковки";
            var command2 = new OleDbCommand(qwery2, database.getConnection());
            OleDbDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                comboBoxVid.Items.Add(reader2["Наименование"].ToString());
            }
            reader2.Close();
            // Поиск Производителя из бд.
            var qwery3 = $"select * from Производитель";
            var command3 = new OleDbCommand(qwery3, database.getConnection());
            OleDbDataReader reader3 = command3.ExecuteReader();
            while (reader3.Read())
            {
                comboBoxPro.Items.Add(reader3["Название"].ToString());
            }
            reader3.Close();
            database.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.openConnection();
            var name = textBox1.Text;
            var dat = dateTimePicker1.Value.Date;
            string Srok = textBox3.Text;
            string num = textBox4.Text;
            int type_id = 0;
            int Vid_id = 0;
            int prod_id = 0;
            int count = Convert.ToInt32(numericUpDown1.Value);
            decimal price;
            bool isNumber1 = decimal.TryParse(textBox5.Text, out price);
            // Поиск Категории_ID.
            var qwery1 = $"select ID from Категория where Наименование = '{comboBoxCat.Text}'";
            var command = new OleDbCommand(qwery1, database.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                 type_id = reader.GetInt32(0);
            }
            reader.Close();

            // Поиск ВидУпаковки_ID.
            var qwery2 = $"select ID from ВидУпаковки where Наименование = '{comboBoxVid.Text}'";
            var command2 = new OleDbCommand(qwery2, database.getConnection());
            OleDbDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                Vid_id = reader2.GetInt32(0);
            }
            reader2.Close();

            // Поиск Производитель_ID.
            var qwery3 = $"select ID from Производитель where Название = '{comboBoxPro.Text}'";
            var command3 = new OleDbCommand(qwery3, database.getConnection());
            OleDbDataReader reader3 = command3.ExecuteReader();
            while (reader3.Read())
            {
                prod_id = reader3.GetInt32(0);
            }
            reader3.Close();

            // Проверка на не пустоту строк и запрос на добавление новой строки в бд.
            if (isNumber1 == true && name != "" && Srok != "" && num != "" && Srok.Length<11 && num.Length<45 && type_id>0 && Vid_id >0 && prod_id>0)
            {
                var addQwery = $"insert into Лекарство (Название, Дата, СрокГодности, РегНомер, Категория_ID, ВидУпаковки_ID, Производитель_ID, Количество, Цена ) values ('{name}', '{dat}', '{Srok}', '{num}', {type_id}, {Vid_id}, {prod_id}, {count}, {price})";

                var command4 =new OleDbCommand(addQwery, database.getConnection());
                command4.ExecuteNonQuery();

                MessageBox.Show("Запись успешно создана!", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                comboBoxCat.Text = "";
                dateTimePicker1.Text = "";
                comboBoxVid.Text = "";
                comboBoxPro.Text = "";
            }
            else
            {
                MessageBox.Show("Неправильный ввод", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            database.closeConnection();
        }

    }
}
