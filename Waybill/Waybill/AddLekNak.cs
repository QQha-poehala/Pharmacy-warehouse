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

namespace Waybill
{
    public partial class AddLekNak : Form
    {
        DataB database = new DataB();
        public int id_Post;
        public AddLekNak(int  id_post, DataGridView dgw)
        {
            id_Post = id_post;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            AddItems(dgw);
        }
        // Добавление лекарств в комбобокс.
        private void AddItems(DataGridView dgw)
        {
            List<int> Name_in_dgw = new List<int>();
            for (int i = 0; i < dgw.Rows.Count; i++)
            {
                Name_in_dgw.Add(Convert.ToInt32(dgw.Rows[i].Cells[1].Value));
            }
            database.openConnection();
            // Поиск Лекарств из бд.
            var qwery1 = $"select * from Лекарство";
            var command = new OleDbCommand(qwery1, database.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Если Лекарство уже есть в Заказе, то не добавляем его в combobox.
                if (Name_in_dgw.Contains(Convert.ToInt32(reader.GetInt32(0))))
                    continue;
                else
                    comboBox1.Items.Add(reader["Название"].ToString().Trim());
            }
            reader.Close();
            database.closeConnection();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            database.openConnection();

            var kol = numericUpDown1.Value;
            int lek_id = 0;
            decimal price;
            bool isNumber1 = decimal.TryParse(textBox1.Text, out price);
            // Поиск Лекарство_ID.
            var qwery1 = $"select * from Лекарство where Название = '{comboBox1.Text}'";
            var command = new OleDbCommand(qwery1, database.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lek_id = reader.GetInt32(0);
            }
            reader.Close();

            string addQwery;
            // Проверка на не пустоту строк и запрос на добавление новой строки в бд.
            if (lek_id >= 0)
            {
                if (isNumber1 == true)
                {
                    addQwery = $"insert into Накладная (Лекарство_ID, Количество, Цена, Поставка_ID) values ({lek_id}, {kol}, {price}, {id_Post})";
                    var command4 = new OleDbCommand(addQwery, database.getConnection());
                    command4.ExecuteNonQuery();

                    MessageBox.Show("Запись успешно создана!", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox1.Text = "";
                    numericUpDown1.Value = 1;
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверный ввод цены!", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Не выбран пункт!", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            database.closeConnection();
        }
    }
}
