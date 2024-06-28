using System;
using database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Diagnostics;

namespace Invoices
{
    public partial class InvoicesForm : Form
    {
        int selectedRow;
        public bool W, E, D;
        public int id_user;
        DataB b = new DataB();
        public InvoicesForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            RightsFunction();
            if (W == true && E == true)
            {
                button3.Enabled = true;
            }
        }
        // Авторизация прав пользователя на меню Производитель.
        private void RightsFunction()
        {
            string qwertystring = $"select * from Rights where ID_Menu = 5";
            OleDbCommand command = new OleDbCommand(qwertystring, b.getConnection());
            b.openConnection();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                W = Convert.ToBoolean(reader.GetBoolean(2));
                E = Convert.ToBoolean(reader.GetBoolean(3));
                id_user = reader.GetInt32(5);
            }
            reader.Close();
            b.closeConnection();
        }
        private void CreateColums()
        {
            dataGridView1.Columns.Add("ID", "ID_Заказа");
            dataGridView1.Columns.Add("pokup_id", "Покупатель_ID");
            dataGridView1.Columns.Add("data", "Дата Выписки");
            dataGridView1.Columns.Add("secondName", "Фамилия продавца");
            dataGridView1.Columns.Add("status", "Статус");
            dataGridView1.Columns[1].Visible = false;
        }
        // Отчистка таблицы "Запись".
        private void ClearFields()
        {
            dateTimePicker1.Text = "";
            textBox3.Text = "";
        }
        // Поиск данных в базе данных.
        private void RefreshDataGrid(DataGridView dgw)
        {
            b.openConnection();
            // Кол-во активных заказов.
            string qwerystring3 = $"select * from `Счёт-фактуры` order by Заказ_ID";
            OleDbCommand command3 = new OleDbCommand(qwerystring3, b.getConnection());
            OleDbDataReader reader3 = command3.ExecuteReader();
            List<int> zak_aktiv= new List<int>();
            List<int> zak_sform = new List<int>();
            int last_id = 0;
            while (reader3.Read())
            {
                if (last_id != reader3.GetInt32(3))
                {
                    if(reader3.GetString(4).Trim() == "Активен")
                    {
                        zak_aktiv.Add(reader3.GetInt32(3));
                    }
                    if(reader3.GetString(4).Trim() == "Сформирован")
                    {
                        zak_sform.Add(reader3.GetInt32(3));
                    }
                    
                }
                last_id = reader3.GetInt32(3);
            }
            reader3.Close();
            if (W == true && E == true)
            {
                for (int i = 0; i < zak_aktiv.Count; i++)
                {
                    // Поиск Покупателя_ID.
                    string qwerystring = $"select * from Заказ where ID = {zak_aktiv[i]}";
                    OleDbCommand command = new OleDbCommand(qwerystring, b.getConnection());
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        dgw.Rows.Add(zak_aktiv[i], reader.GetInt32(1), "-", "-", "Активен");
                    }
                    reader.Close();
                }
                for (int i = 0; i < zak_sform.Count; i++)
                {
                    // Поиск Покупателя_ID.
                    string qwerystring2 = $"select * from Заказ where ID = {zak_sform[i]}";
                    OleDbCommand command2 = new OleDbCommand(qwerystring2, b.getConnection());
                    OleDbDataReader reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        dgw.Rows.Add(zak_sform[i], reader2.GetInt32(1), reader2.GetDateTime(2), reader2.GetString(3).Trim(), "Сформирован");
                    }
                    reader2.Close();
                }
            }
            else
            {
                for (int i = 0; i < zak_sform.Count; i++)
                {
                    // Поиск Покупателя_ID.
                    string qwerystring2 = $"select * from Заказ where ID = {zak_sform[i]} and Покупатель_ID = {id_user}";
                    OleDbCommand command2 = new OleDbCommand(qwerystring2, b.getConnection());
                    OleDbDataReader reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        dgw.Rows.Add(zak_sform[i], reader2.GetInt32(1), reader2.GetDateTime(2), reader2.GetString(3).Trim(), "Сформирован");
                    }
                    reader2.Close();
                }   
            }
            b.closeConnection();
        }
        // Нажатие на клавишу "Обновить".
        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFields();
        }

        private void InvoicesForm_Load(object sender, EventArgs e)
        {
            CreateColums();
            RefreshDataGrid(dataGridView1);
        }
        // Запись данных в таблицу "Запись".
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                if(W == true && E == true)
                {
                    button3.Enabled = true;
                }
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox1.Text = row.Cells[0].Value.ToString().Trim();
                textBox2.Text = row.Cells[1].Value.ToString().Trim();
                if(row.Cells[2].Value.ToString().Trim() != "-")
                {
                    dateTimePicker1.Text = row.Cells[2].Value.ToString().Trim();
                }
                else
                {
                    dateTimePicker1.Text = "";
                }
                if (row.Cells[3].Value.ToString().Trim() != "-")
                {
                    textBox3.Text = row.Cells[3].Value.ToString().Trim();
                    button4.Enabled = true;
                }
                else
                {
                    textBox3.Text = "";
                }
                if(row.Cells[4].Value.ToString().Trim() == "Сформирован")
                {
                    button4.Enabled = true;
                }
                else
                {
                    button4.Enabled = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            int selectedRowindex = dataGridView1.CurrentCell.RowIndex;
           
            // Проверка ввода.
            if (textBox3.Text.Length >0 && textBox3.Text != "-")
            {
                dataGridView1.Rows[selectedRowindex].Cells[2].Value = dateTimePicker1.Value;
                dataGridView1.Rows[selectedRowindex].Cells[3].Value = textBox3.Text;
                dataGridView1.Rows[selectedRowindex].Cells[4].Value = "Сформирован";
                // Обновление данных в таблице Заказ.
                b.openConnection();
                string changeQwery = $"update Заказ set ДатаВыписки = '{dataGridView1.Rows[selectedRowindex].Cells[2].Value}', ФамилияПродавца = '{textBox3.Text}' where ID ={textBox1.Text.Trim()}";
                var command2 = new OleDbCommand(changeQwery, b.getConnection());
                command2.ExecuteNonQuery();
                b.closeConnection();
                // Обновление данных в таблице Счёт-фактуры.
                b.openConnection();
                string str = $"update `Счёт-фактуры` set Статус = 'Сформирован' where Заказ_ID ={textBox1.Text.Trim()}";
                var comm = new OleDbCommand(str, b.getConnection());
                comm.ExecuteNonQuery();
                b.closeConnection();

            }
            else
            {
                MessageBox.Show("Неправильный ввод", "Внесение данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            button3.Enabled = false;
            ClearFields();
        }
        // Экспорт файла.
        private void button4_Click(object sender, EventArgs e)
        {
            // Создание объекта для запуска Excel.
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            // Создаём рабочую книгу.
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Cells[1, 1] = "ID_Заказа";
            ExcelApp.Cells[2, 1] = "Дата Выписки";
            ExcelApp.Cells[3, 1] = "Фамилия Продавца";
            ExcelApp.Cells[4, 1] = "Сумма к уплате";
            ExcelApp.Cells[6, 1] = "Лекарство";
            ExcelApp.Cells[6, 2] = "Количество";
            ExcelApp.Cells[6, 3] = "Цена";
            ExcelApp.Cells[6, 4] = "Сумма";
            ExcelApp.Columns.ColumnWidth = 18;
            int selectedRowindex = dataGridView1.CurrentCell.RowIndex;

            ExcelApp.Cells[1, 2] = dataGridView1.Rows[selectedRowindex].Cells[0].Value;
            ExcelApp.Cells[2, 2] = dataGridView1.Rows[selectedRowindex].Cells[2].Value;
            ExcelApp.Cells[3, 2] = dataGridView1.Rows[selectedRowindex].Cells[3].Value;

            // Информация по заказу.
            List<decimal> sum = new List<decimal>();
            List<decimal> price = new List<decimal>();
            List<int> kol = new List<int>();
            List<int> lek_id = new List<int>();
            List<string> lek = new List<string>();
            string qwertystring3 = $"select * from `Счёт-фактуры` where Заказ_ID = {dataGridView1.Rows[selectedRowindex].Cells[0].Value}";
            b.openConnection();
            OleDbCommand command3 = new OleDbCommand(qwertystring3, b.getConnection());
            OleDbDataReader reader3 = command3.ExecuteReader();
            while (reader3.Read())
            {
                lek_id.Add(reader3.GetInt32(1));
                kol.Add(reader3.GetInt32(2));
            }
            reader3.Close();
            // Запрос на получение данных о Лекарстве.
            for(int i=0; i< lek_id.Count; i++)
            {
                string qwertystring = $"select * from Лекарство where ID = {lek_id[i]}";
                OleDbCommand command = new OleDbCommand(qwertystring, b.getConnection());
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lek.Add(reader.GetString(1));
                    price.Add(reader.GetDecimal(9));
                }
                reader.Close();
            }
            b.closeConnection();
            // Вывод данных в Excel
            for (int i = 0; i < lek.Count; i++)
            {
                ExcelApp.Cells[i + 7, 1] = lek[i];
                ExcelApp.Cells[i + 7, 2] = kol[i];
                ExcelApp.Cells[i + 7, 3] = price[i];
                sum.Add(kol[i] * price[i]);
                ExcelApp.Cells[i + 7, 4] = sum[i];
            }
            // Подсчёт суммы.
            ExcelApp.Cells[4, 2] = sum.ToArray().Sum();
            ExcelApp.Visible = true;
            b.closeConnection();
            MessageBox.Show("Таблица успешно создана", "Создание таблицы", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
