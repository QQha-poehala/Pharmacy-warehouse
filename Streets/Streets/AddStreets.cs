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

namespace Streets
{
    public partial class AddStreets : Form
    {
        DataB database = new DataB();
        public AddStreets()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            database.openConnection();
            var name = textBox1.Text;
            // Проверка на не пустоту строки и запрос на добавление новой строки в бд.
            if (name != "")
            {
                var addQwery = $"insert into Улица (Наименование) values ('{name}')";

                var command4 = new OleDbCommand(addQwery, database.getConnection());
                command4.ExecuteNonQuery();

                MessageBox.Show("Запись успешно создана!", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";

            }
            else
            {
                MessageBox.Show("Неправильный ввод наименования ", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            database.closeConnection();
        }
    }
}
