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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Manufacturers
{
    public partial class AddManufacturers : Form
    {
        DataB database = new DataB();
        public AddManufacturers()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            database.openConnection();
            // Поиск Улицы из бд.
            var qwery1 = $"select * from Улица";
            var command = new OleDbCommand(qwery1, database.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["Наименование"].ToString());
            }
            reader.Close();
           
            database.closeConnection();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            database.openConnection();

            var name = textBox1.Text;
            int streets_id = 0;
            int house;
            int stroen;
            int kvar;


            // Поиск Улица_ID.
            var qwery1 = $"select ID from Улица where Наименование = '{comboBox1.Text}'";
            var command = new OleDbCommand(qwery1, database.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                streets_id = reader.GetInt32(0);
            }
            reader.Close();
            // Проверка ввода.
            bool isNumber1 = int.TryParse(textBox2.Text, out house);
            bool isNumber2 = int.TryParse(textBox3.Text, out stroen);
            bool isNumber3 = int.TryParse(textBox4.Text, out kvar);

            string addQwery;
            // Проверка на не пустоту строк и запрос на добавление новой строки в бд.
            if (isNumber1 == true && isNumber3 == true && house > 0 && kvar > 0  && name!="" && streets_id >0)
            {
                if (isNumber2 == false)
                {
                    addQwery = $"insert into Производитель (Название, Улица_ID, Дом, Строение, Квартира) values ('{name}', {streets_id}, {house}, NULL, {kvar})";
                }
                else
                {
                    addQwery = $"insert into Производитель (Название, Улица_ID, Дом, Строение, Квартира) values ('{name}', {streets_id}, {house}, {stroen}, {kvar})";
                }
                var command4 = new OleDbCommand(addQwery, database.getConnection());
                command4.ExecuteNonQuery();

                MessageBox.Show("Запись успешно создана!", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";

            }
            else
            {
                MessageBox.Show("Неправильный ввод", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            database.closeConnection();
        }
    }
}
