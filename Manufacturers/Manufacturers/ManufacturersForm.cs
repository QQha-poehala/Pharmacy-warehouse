using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using database;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Manufacturers
{
    public partial class ManufacturersForm : Form
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
        bool W, E, D;
        public ManufacturersForm()
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
        }
        // Авторизация прав пользователя на меню Производитель.
        private void RightsFunction()
        {
            string qwertystring = $"select * from Rights where ID_Menu = 14";
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
            dataGridView1.Columns.Add("streets_id", "Улица_ID");
            dataGridView1.Columns.Add("House", "Дом");
            dataGridView1.Columns.Add("stroen", "Строение");
            dataGridView1.Columns.Add("kvar", "Квартира");
            // Статус строки.
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns[6].Visible = false;

        }
        // Отчистка таблицы "Запись".
        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox1.Items.Clear();
        }
        // Добавление Строки из базы данных в DataGridView.
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            int srtoen;
            try
            {
                 srtoen = Convert.ToInt32(record.GetInt32(4));
            }
            catch (Exception ex) { srtoen = 0; }
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1).Trim(), record.GetInt32(2), record.GetInt32(3), srtoen, record.GetInt32(5), RowState.ModifideNew);
        }
        // Поиск данных в базе данных.
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string qwertystring = $"select * from Производитель";
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
        private void ManufacturersForm_Load(object sender, EventArgs e)
        {
            CreateColums();
            RefreshDataGrid(dataGridView1);
        }
        // Запись данных в таблицу "Запись".
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Items.Clear();
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox1.Text = row.Cells[0].Value.ToString().Trim();
                textBox2.Text = row.Cells[1].Value.ToString().Trim();
                textBox3.Text = row.Cells[3].Value.ToString().Trim();
                if (row.Cells[4].Value.ToString().Trim() == null)
                {
                    textBox4.Text = "0";
                }
                else
                {
                    textBox4.Text = row.Cells[4].Value.ToString().Trim();
                }
                textBox5.Text = row.Cells[5].Value.ToString().Trim();
                // Поиск Улицы по id и добавление вкладок.
                var qwery1 = $"select * from Улица";
                var command = new OleDbCommand(qwery1, b.getConnection());
                b.openConnection();
                OleDbDataReader reader = command.ExecuteReader();
                int ID_spr = 0;
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["Наименование"].ToString());
                    ID_spr = reader.GetInt32(0);
                    if (ID_spr == Convert.ToInt32(row.Cells[2].Value.ToString()))
                    {
                        comboBox1.Text = reader.GetString(1);
                    }
                }
                reader.Close();
                b.closeConnection();
            }
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
            AddManufacturers addfrm = new AddManufacturers();
            addfrm.Show();
        }
        // Метод для поиска данных в БД.
        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string serchString = $"select * from Производитель where ID like '%" + textBox_search.Text + "%'" + " or Название like '%" + textBox_search.Text + "%'" + " or Улица_ID like '%" + textBox_search.Text + "%'" + " or Дом like '%" + textBox_search.Text + "%'" + " or Строение like '%" + textBox_search.Text + "%'" + " or Квартира like '%" + textBox_search.Text + "%'";

            OleDbCommand com = new OleDbCommand(serchString, b.getConnection());
            b.openConnection();
            OleDbDataReader read = com.ExecuteReader();
            while (read.Read())
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
        // Метод обнавляет, удаляет или ничего не делает с данными в бд в соответствии со статусом каждой строки в DataGridView
        private void Update()
        {
            b.openConnection();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var rowState = (RowState)dataGridView1.Rows[i].Cells[6].Value;
                if (rowState == RowState.Existed) continue;
                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    var deleteQwery = $"delete from Производитель where ID = {id}";

                    var command = new OleDbCommand(deleteQwery, b.getConnection());
                    command.ExecuteNonQuery();

                }
                if (rowState == RowState.Modifide)
                {
                    var id = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                    var name = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();
                    var streets_id = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                    var house = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();
                    var stroen = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value.ToString().Trim());
                    var kvar = dataGridView1.Rows[i].Cells[5].Value.ToString().Trim();
                    string changeQwery;
                    if(stroen != 0 )
                    {
                        changeQwery = $"update Производитель set Название = '{name}', Улица_ID ={streets_id}, Дом = {house}, Строение= {stroen}, Квартира= {kvar} where ID ={id}";
                    }
                    else
                    {
                        changeQwery = $"update Производитель set Название = '{name}', Улица_ID ={streets_id}, Дом = {house}, Строение= NULL, Квартира= {kvar} where ID ={id}";
                    }
                    var command = new OleDbCommand(changeQwery, b.getConnection());
                    command.ExecuteNonQuery();

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
                dataGridView1.Rows[index].Cells[6].Value = RowState.Deleted;
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
            var streets_id = 0;
            int house;
            int kvar;
            bool isNumber1 = int.TryParse(textBox3.Text, out house);
            bool isNumber3 = int.TryParse(textBox5.Text, out kvar);
            // Обратный поиск ID по Наименованием и названиям виду упаковки, категории, производителю.
            b.openConnection();

            // Поиск Улица_ID.
            var qwery1 = $"select ID from Улица where Наименование = '{comboBox1.Text}'";
            var command = new OleDbCommand(qwery1, b.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                streets_id = reader.GetInt32(0);
            }
            reader.Close();

            // Проверка ввода.
            if (isNumber1 == true && isNumber3 == true && name != "" && Convert.ToInt32(house) > 0 && Convert.ToInt32(kvar) > 0 )
            {
                int strorn_int;
                bool isNumber2 = int.TryParse(textBox4.Text, out strorn_int);
                if (isNumber2 == true)
                {
                    dataGridView1.Rows[selectedRowindex].SetValues(id, name, streets_id, house, strorn_int, kvar);
                }
                else
                {
                    dataGridView1.Rows[selectedRowindex].SetValues(id, name, streets_id, house, 0, kvar);
                }
                // установка статуса: Измененная строка.
                dataGridView1.Rows[selectedRowindex].Cells[6].Value = RowState.Modifide;

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
