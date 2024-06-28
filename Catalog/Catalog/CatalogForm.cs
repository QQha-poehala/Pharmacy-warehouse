using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using database;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using System.Data.OleDb;

namespace Catalog
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
    public partial class CatalogForm : Form
    {
        DataB b = new DataB();
        // Переменная для работы с DataGridView.
        int selectedRow;
        bool W, E, D;
        public CatalogForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            RightsFunction();
            if(W == false)
            {
                buttonNew.Enabled = false;
            }
            if (E == false)
            {
                buttonChange.Enabled = false;
                textBox3.Enabled = false;
            }
            if (D == false)
            {
                buttonDel.Enabled = false;
            }
        }
        // Авторизация прав пользователя на меню Каталог.
        private void RightsFunction()
        {
            string qwertystring = $"select * from Rights where ID_Menu = 7";
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
        // Создание столбцов в dataGridView.
        private void CreateColums()
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Name", "Название");
            dataGridView1.Columns.Add("data", "Дата");
            dataGridView1.Columns.Add("Srok", "Срок");
            dataGridView1.Columns.Add("Number", "Рег.номер");
            dataGridView1.Columns.Add("KategId", "Категория ID");
            dataGridView1.Columns.Add("Type", "Тип Упаковки ID");
            dataGridView1.Columns.Add("Prod", "Производитель ID");
            dataGridView1.Columns.Add("kol", "Количество");
            dataGridView1.Columns.Add("price", "Цена");
            // Статус строки.
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns[10].Visible = false;

        }
        // Отчистка таблицы "Запись".
        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            numericUpDown1.Text = "";
        }
        // Добавление Строки из базы данных в DataGridView.
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1).Trim(), record.GetDateTime(2), record.GetString(3).Trim(), record.GetString(4).Trim(), record.GetInt32(5), record.GetInt32(6), record.GetInt32(7), record.GetInt32(8),record.GetDecimal(9), RowState.ModifideNew);
        }
        // Поиск данных в базе данных.
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string qwertystring = $"select * from Лекарство";
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
        private void CatalogForm_Load(object sender, EventArgs e)
        {
            CreateColums();
            RefreshDataGrid(dataGridView1);
        }
        // Запись данных в таблицу "Запись".
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            selectedRow = e.RowIndex;
            if(e.RowIndex>=0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox1.Text = row.Cells[0].Value.ToString().Trim();
                textBox2.Text = row.Cells[1].Value.ToString().Trim();
                dateTimePicker1.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString().Trim();
                textBox5.Text = row.Cells[4].Value.ToString().Trim();
                textBox3.Text = row.Cells[9].Value.ToString().Trim();
                numericUpDown1.Text = row.Cells[8].Value.ToString();
                // Поиск Категории по id и добавление вкладок.
                var qwery1 = $"select * from Категория";
                var command = new OleDbCommand(qwery1, b.getConnection());
                b.openConnection();
                OleDbDataReader reader = command.ExecuteReader();
                int ID_spr = 0;
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["Наименование"].ToString());
                    ID_spr = reader.GetInt32(0);
                    if(ID_spr == Convert.ToInt32(row.Cells[5].Value.ToString()))
                    {
                        comboBox1.Text = reader.GetString(1);
                    }
                }
                reader.Close();

                // Поиск вида упаковки по id и добавление вкладок.
                var qwery2 = $"select * from ВидУпаковки";
                var command2 = new OleDbCommand(qwery2, b.getConnection());
                OleDbDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    comboBox2.Items.Add(reader2["Наименование"].ToString());
                    ID_spr = reader2.GetInt32(0);
                    if (ID_spr == Convert.ToInt32(row.Cells[6].Value.ToString()))
                    {
                        comboBox2.Text = reader2.GetString(1);
                    }
                }
                reader2.Close();

                // Поиск Производителя по ID и добавление вкладок.
                var qwery3 = $"select * from Производитель";
                var command3 = new OleDbCommand(qwery3, b.getConnection());
                OleDbDataReader reader3 = command3.ExecuteReader();
                while (reader3.Read())
                {
                    comboBox3.Items.Add(reader3["Название"].ToString());
                    ID_spr = reader3.GetInt32(0);
                    if (ID_spr == Convert.ToInt32(row.Cells[7].Value.ToString()))
                    {
                        comboBox3.Text = reader3.GetString(1);
                    }
                }
                reader3.Close();
                b.closeConnection();

            }
        }
        // Нажатие на клавишу "Обновить".
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
            Add_Form addfrm = new Add_Form();
            addfrm.Show();
        }
        // Метод для поиска данных в БД.
        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string serchString = $"select * from Лекарство where ID like '%" + textBox_search.Text + "%'" + " or Название like '%" + textBox_search.Text + "%'" + " or Дата like '%" + textBox_search.Text + "%'" + " or СрокГодности like '%" + textBox_search.Text + "%'" + " or РегНомер like '%" + textBox_search.Text + "%'" + " or Категория_ID like '%" + textBox_search.Text + "%'" + " or ВидУпаковки_ID like '%" + textBox_search.Text + "%'" + " or Производитель_ID like '%" + textBox_search.Text + "%'" + " or Количество like '%" + textBox_search.Text + "%'" + " or Цена like '%" + textBox_search.Text + "%'";

            OleDbCommand com = new OleDbCommand(serchString, b.getConnection());
            b.openConnection();
            OleDbDataReader read = com.ExecuteReader();
            while(read.Read())
            {
                ReadSingleRow(dgw, read);
            }
            read.Close();
            b.closeConnection();
        }
        // Событие когда поиск активен.
        private void TextGotFocus(object sender, EventArgs e)
        {
            // Удаление подсказки.
            if (textBox_search.Text == "Поиск...")
            {
                textBox_search.Text = "";
                textBox_search.ForeColor = Color.Black;
            }
        }
        // Cобытие когда поиск неактивен.
        private void TextLostFocus(object sender, EventArgs e)
        {
            // Возвращаем подсказку при неактивном состояни, если поле пустое.
            if (string.IsNullOrWhiteSpace(textBox_search.Text))
            {
                textBox_search.Text = "Поиск...";
                textBox_search.ForeColor = Color.Gray;
                RefreshDataGrid(dataGridView1);
            }
        }
        // Событие при изменении поиска.
        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }
        // Метод обнавляет, удаляет или ничего не делает с данными в бд в соответствии со статусом каждой строки в DataGridView.
        private void Update()
        {
            b.openConnection();
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var rowState = (RowState)dataGridView1.Rows[i].Cells[10].Value;
                if (rowState == RowState.Existed) continue;
                if(rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    var deleteQwery = $"delete from Лекарство where ID = {id}";

                    var command = new OleDbCommand(deleteQwery, b.getConnection());
                    command.ExecuteNonQuery();

                }
                if(rowState == RowState.Modifide) 
                {
                    var id = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                    var name = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();
                    var dat = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                    var Srok = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();
                    var num = dataGridView1.Rows[i].Cells[4].Value.ToString().Trim();
                    var type = dataGridView1.Rows[i].Cells[5].Value.ToString().Trim();
                    var Vid = dataGridView1.Rows[i].Cells[6].Value.ToString().Trim();
                    var prod = dataGridView1.Rows[i].Cells[7].Value.ToString().Trim();
                    var count = dataGridView1.Rows[i].Cells[8].Value.ToString().Trim();
                    string price = dataGridView1.Rows[i].Cells[9].Value.ToString().Trim();
                    price = price.Replace(",", ".");
                    var changeQwery = $"update Лекарство set Название = '{name}', Дата ='{dat}', СрокГодности = '{Srok}', РегНомер= '{num}', Категория_ID= {type}, ВидУпаковки_ID= {Vid}, Производитель_ID= {prod}, Количество= {count}, Цена = {price} where ID ={id}";

                    var command = new OleDbCommand(changeQwery, b.getConnection());
                    command.ExecuteNonQuery();

                }
            }
            b.closeConnection();
        }
        // Метод делает строку невидимой и присваивает ей статус "Удален".
        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[index].Visible = false;

            if (Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value) >= 0)
            {
                dataGridView1.Rows[index].Cells[10].Value = RowState.Deleted;
            }
        }
        // Событие "Удаление записи".
        private void buttonDel_Click(object sender, EventArgs e)
        {
            deleteRow();
            ClearFields();
        }
        // Событие "Сохраниение".
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Update();
        }
        private void Change()
        {
            var selectedRowindex = dataGridView1.CurrentCell.RowIndex;
            var id = textBox1.Text;
            var name = textBox2.Text;
            var dat = dateTimePicker1.Value.Date;
            var Srok = textBox4.Text;
            var num = textBox5.Text;
            var price = textBox3.Text;
            int type_id = 0;
            int Vid_id = 0;
            int prod_id = 0;
            int count = Convert.ToInt32(numericUpDown1.Value);
            //
            // Обратный поиск ID по Наименованием и названиям виду упаковки, категории, производителю.
            //
            b.openConnection();

            // Поиск Категории_ID.
            var qwery1 = $"select ID from Категория where Наименование = '{comboBox1.Text}'";
            var command = new OleDbCommand(qwery1, b.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                type_id = reader.GetInt32(0);
            }
            reader.Close();

            // Поиск ВидУпаковки_ID.
            var qwery2 = $"select ID from ВидУпаковки where Наименование = '{comboBox2.Text}'";
            var command2 = new OleDbCommand(qwery2, b.getConnection());
            OleDbDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                Vid_id = reader2.GetInt32(0);
            }
            reader2.Close();

            // Поиск Производитель_ID.
            var qwery3 = $"select ID from Производитель where Название = '{comboBox3.Text}'";
            var command3 = new OleDbCommand(qwery3, b.getConnection());
            OleDbDataReader reader3 = command3.ExecuteReader();
            while (reader3.Read())
            {
                prod_id = reader3.GetInt32(0);
            }
            reader3.Close();
            // Проверка на не пустоту строк и запрос на добавление новой строки в бд.
            if (name != "" && Srok != "" && num != "" && Srok.Length < 11 && num.Length < 46 && type_id > 0 && Vid_id > 0 && prod_id > 0)
            {
                dataGridView1.Rows[selectedRowindex].SetValues(id, name, dat, Srok, num, type_id, Vid_id, prod_id, count, price);
                // установка статуса: Измененная строка.
                dataGridView1.Rows[selectedRowindex].Cells[10].Value = RowState.Modifide;

            }
            else
            {
                MessageBox.Show("Неправильный ввод", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            b.closeConnection();
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
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

        private void buttonChange_Click(object sender, EventArgs e)
        {
            Change();
            ClearFields();
        }
        // Нажатие на клавишу "Отчистка"
        private void button1_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
