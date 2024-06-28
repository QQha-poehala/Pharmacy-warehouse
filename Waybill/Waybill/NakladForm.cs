using database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
namespace Waybill
{
    public partial class NakladForm : Form
    {
        // Перечисление для состояния строки.
        enum RowState
        {
            Existed,
            New,
            Modifide,
            ModifideNew,
            Deleted
        }
        DataB b = new DataB();
        // Переменная для работы с DataGridView.
        int selectedRow;
        public bool W, E, D;
        public int id_postavki;
        public NakladForm(int id)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            id_postavki = id;
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
        }
        // Авторизация прав пользователя на меню Производитель.
        private void RightsFunction()
        {
            string qwertystring = $"select * from Rights where ID_Menu = 6";
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
        private void CreateColums()
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("lekar", "Лекарство");
            dataGridView1.Columns.Add("kol", "Количество");
            dataGridView1.Columns.Add("price", "Цена");
            dataGridView1.Columns.Add("post_id", "Поставка_ID");
            // Статус строки.
            dataGridView1.Columns.Add("IsNew", String.Empty);

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;

        }
        // Отчистка таблицы "Запись".
        private void ClearFields()
        {
            textBox1.Text = "";
            numericUpDown1.Value = 1;
            textBox2.Text = "";
        }
        // Добавление Строки из базы данных в DataGridView.
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetDecimal(3), record.GetInt32(4), RowState.ModifideNew);
        }
        // Поиск данных в базе данных.
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string qwertystring = $"select * from Накладная where Поставка_ID = {id_postavki}";
            OleDbCommand command = new OleDbCommand(qwertystring, b.getConnection());
            b.openConnection();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
            b.closeConnection();
        }
        // Нажатие на клавишу "Обновить"
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Сохранить изменения?", "Сохрание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.Yes:
                    {
                        Update();
                        RefreshDataGrid(dataGridView1);
                        ClearFields();
                        break;
                    }

                case DialogResult.No:
                    {
                        RefreshDataGrid(dataGridView1);
                        ClearFields();
                        break;
                    }
            }
        }
        // Событие "Добавление записи".
        private void buttonNew_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            AddLekNak addfrm = new AddLekNak(id_postavki, dataGridView1);
            addfrm.Show();
        }

        private void NakladForm_Load_1(object sender, EventArgs e)
        {
            CreateColums();
        }
        // Запись данных в таблицу "Запись".
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                b.openConnection();
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                numericUpDown1.Value = Convert.ToInt32(row.Cells[2].Value.ToString().Trim());
                textBox1.Text = row.Cells[3].Value.ToString().Trim();
                var id = row.Cells[1].Value;
                // Поиск Лекарства по id для получения названия и количества.
                var qwery1 = $"select * from Лекарство where ID = {id}";
                var command = new OleDbCommand(qwery1, b.getConnection());
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    textBox2.Text = reader["Название"].ToString();
                }
                reader.Close();
                b.closeConnection();
            }
        }
        // Метод обнавляет, удаляет или ничего не делает с данными в бд в соответствии со статусом каждой строки в DataGridView.
        private void Update()
        {
            b.openConnection();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var rowState = (RowState)dataGridView1.Rows[i].Cells[5].Value;
                if (rowState == RowState.Existed) continue;
                if (rowState == RowState.Deleted)
                {
                    var id = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                    var deleteQwery = $"delete from Накладная where ID = {id}";

                    var command = new OleDbCommand(deleteQwery, b.getConnection());
                    command.ExecuteNonQuery();

                }
                if (rowState == RowState.Modifide)
                {
                    var id = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                    var kol = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                    var price = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();
                    price = price.Replace(",", ".");
                    // Обновление данных
                    string changeQwery = $"update Накладная set Количество = {kol}, Цена = {price} where ID ={id}";
                    var command2 = new OleDbCommand(changeQwery, b.getConnection());
                    command2.ExecuteNonQuery();
                }
            }
            b.closeConnection();
        }
        // Метод делает строку невидимой и присваивает ей статус "Удален"
        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[index].Visible = false;

            if (Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value) >= 0)
            {
                dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;
            }
        }
        // Событие "Удаление записи".

        private void buttonDel_Click(object sender, EventArgs e)
        {
            deleteRow();
            ClearFields();
        }
        // Событие "Отчистка" .
        private void button1_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            Change();
            ClearFields();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            MessageBox.Show("Накладная заполнена! Теперь вы можете загрузить Excel-файл.", "Заказ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button4.Enabled = true;
        
        }
        // Экспорт файла
        private void button4_Click(object sender, EventArgs e)
        {
            // Создание объекта для запуска Excel.
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            // Создаём рабочую книгу.
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Cells[1, 1] = "ID накладной";
            ExcelApp.Cells[2, 1] = "Поставщик";
            ExcelApp.Cells[3, 1] = "Дата";
            ExcelApp.Cells[5, 1] = "Лекарство";
            ExcelApp.Cells[5, 2] = "Количество";
            ExcelApp.Cells[5, 3] = "Цена";
            ExcelApp.Columns.ColumnWidth = 15;

            int id_Postawschik = 0;
            string qwertystring = $"select * from Поставка where ID = {id_postavki}";
            OleDbCommand command = new OleDbCommand(qwertystring, b.getConnection());
            b.openConnection();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ExcelApp.Cells[1, 2] = reader.GetInt32(0).ToString();
                id_Postawschik = reader.GetInt32(1);
                ExcelApp.Cells[3, 2] = reader.GetDateTime(2).ToString();
            }
            reader.Close();
            // Название поставщика по ID
            string qwertystring2 = $"select Название from Поставщик where ID = {id_Postawschik}";
            OleDbCommand command2 = new OleDbCommand(qwertystring2, b.getConnection());
            OleDbDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                ExcelApp.Cells[2, 2] = reader2.GetString(0).ToString();
            }
            reader2.Close();
            b.closeConnection();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                // Название Лекарства по ID
                string qwertystring3 = $"select Название from Лекарство where ID = {dataGridView1[1, i].Value}";
                b.openConnection();
                OleDbCommand command3 = new OleDbCommand(qwertystring3, b.getConnection());
                OleDbDataReader reader3 = command3.ExecuteReader();
                while (reader3.Read())
                {
                    ExcelApp.Cells[i + 6, 1] = reader3.GetString(0).ToString();
                }
                reader3.Close();
                b.closeConnection();
           
                ExcelApp.Cells[i + 6, 2] = (dataGridView1[2, i].Value).ToString();
                ExcelApp.Cells[i + 6, 3] = (dataGridView1[3, i].Value).ToString();
            }
            ExcelApp.Visible = true;
            b.closeConnection();
            MessageBox.Show("Таблица успешно создана", "Создание таблицы", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Событие "Сохраниение".
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Update();
        }
        private void Change()
        {
            int selectedRowindex = dataGridView1.CurrentCell.RowIndex;
            int kol = Convert.ToInt32(numericUpDown1.Value);
            decimal price = Convert.ToDecimal(textBox1.Text);

            // Проверка ввода.
            if (price>=0)
            {
                dataGridView1.Rows[selectedRowindex].Cells[2].Value = kol;
                dataGridView1.Rows[selectedRowindex].Cells[3].Value = price;
                dataGridView1.Rows[selectedRowindex].Cells[5].Value = RowState.Modifide;
            }
            else
            {
                MessageBox.Show("Неправильный ввод", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
