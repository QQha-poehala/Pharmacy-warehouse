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

namespace Suppliers
{
    
    public partial class SuppliersForm : Form
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
        public SuppliersForm()
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
        // Авторизация прав пользователя на меню Покупатель.
        private void RightsFunction()
        {
            b.openConnection();
            string qwertystring = $"select * from Rights where ID_Menu = 9";
            OleDbCommand command = new OleDbCommand(qwertystring, b.getConnection());
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
            dataGridView1.Columns.Add("banks_id", "Банк_ID");
            dataGridView1.Columns.Add("rasch", "Расчётный счёт");
            dataGridView1.Columns.Add("streets_id", "Улица_ID");
            dataGridView1.Columns.Add("House", "Дом");
            dataGridView1.Columns.Add("stroen", "Строение");
            dataGridView1.Columns.Add("kvar", "Квартира");
            dataGridView1.Columns.Add("tel", "Телефон");
            dataGridView1.Columns.Add("inn", "ИНН");
            // Статус строки.
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns[10].Visible = false;
        }
        // Отчистка таблицы "Запись".
        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox1.Items.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox2.Items.Clear();
        }
        // Добавление Строки из базы данных в DataGridView.
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            int srtoen;
            try
            {
                srtoen = Convert.ToInt32(record.GetInt32(6));
            }
            catch (Exception ex) { srtoen = 0; }
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1).Trim(), record.GetInt32(2), record.GetString(3).Trim(), record.GetInt32(4), record.GetInt32(5), srtoen, record.GetInt32(7), record.GetString(8).Trim(), record.GetString(9).Trim(), RowState.ModifideNew);
        }
        // Поиск данных в базе данных.
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string qwertystring = $"select * from Поставщик";
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
        private void SuppliersForm_Load(object sender, EventArgs e)
        {
            CreateColums();
            RefreshDataGrid(dataGridView1);
        }
        // Запись данных в таблицу "Запись".
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox1.Text = row.Cells[0].Value.ToString().Trim();
                textBox2.Text = row.Cells[1].Value.ToString().Trim();
                textBox3.Text = row.Cells[3].Value.ToString().Trim();
                textBox4.Text = row.Cells[4].Value.ToString().Trim();
                textBox5.Text = row.Cells[7].Value.ToString().Trim();
                textBox7.Text = row.Cells[8].Value.ToString().Trim();
                textBox8.Text = row.Cells[9].Value.ToString().Trim();
                if (row.Cells[6].Value.ToString().Trim() == null)
                {
                    textBox6.Text = "0";
                }
                else
                {
                    textBox6.Text = row.Cells[6].Value.ToString().Trim();
                }
                // Поиск Улицы по id и добавление вкладок.
                var qwery1 = $"select * from Улица";
                var command = new OleDbCommand(qwery1, b.getConnection());
                b.openConnection();
                OleDbDataReader reader = command.ExecuteReader();
                int ID_spr = 0;
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader["Наименование"].ToString());
                    ID_spr = reader.GetInt32(0);
                    if (ID_spr == Convert.ToInt32(row.Cells[4].Value.ToString()))
                    {
                        comboBox2.Text = reader.GetString(1);
                    }
                }
                reader.Close();
                // Поиск Банка по id и добавление вкладок.
                var qwery2 = $"select * from Банк";
                var command2 = new OleDbCommand(qwery2, b.getConnection());
                OleDbDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    comboBox1.Items.Add(reader2["Наименование"].ToString());
                    ID_spr = reader2.GetInt32(0);
                    if (ID_spr == Convert.ToInt32(row.Cells[2].Value.ToString()))
                    {
                        comboBox1.Text = reader2.GetString(1);
                    }
                }
                reader2.Close();
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
            AddSuppliers addfrm = new AddSuppliers();
            addfrm.Show();
        }
        // Метод для поиска данных в БД.
        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string serchString = $"select * from Поставщик where Название like '%" + textBox_search.Text + "%'" + " or ID like '%" + textBox_search.Text+ "%'" + " or Банк_ID like '%" + textBox_search.Text + "%'" + " or РасчётныйСчёт like '%" + textBox_search.Text + "%'" + " or Улица_ID like '%" + textBox_search.Text + "%'" + " or Дом like '%" + textBox_search.Text + "%'" + " or Строение like '%" + textBox_search.Text + "%'" + " or Квартира like '%" + textBox_search.Text + "%'" + " or Телефон like '%" + textBox_search.Text + "%'" + " or ИНН like '%" + textBox_search.Text + "%'";

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
                var rowState = (RowState)dataGridView1.Rows[i].Cells[10].Value;
                if (rowState == RowState.Existed) continue;
                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    var deleteQwery = $"delete from Поставщик where ID = {id}";

                    var command = new OleDbCommand(deleteQwery, b.getConnection());
                    command.ExecuteNonQuery();

                }
                if (rowState == RowState.Modifide)
                {
                    var id = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                    var name = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();
                    var banks_id = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                    var rasch = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();
                    var streets_id = dataGridView1.Rows[i].Cells[4].Value.ToString().Trim();
                    var house = dataGridView1.Rows[i].Cells[5].Value.ToString().Trim();
                    var stroen = Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value.ToString().Trim());
                    var kvar = dataGridView1.Rows[i].Cells[7].Value.ToString().Trim();
                    var tel = dataGridView1.Rows[i].Cells[8].Value.ToString().Trim();
                    var inn = dataGridView1.Rows[i].Cells[9].Value.ToString().Trim();
                    string changeQwery;
                    if (stroen != 0)
                    {
                        changeQwery = $"update Поставщик set Название = '{name}', Банк_ID = {banks_id}, РасчётныйСчёт = '{rasch}', Улица_ID ={streets_id}, Дом = {house}, Строение= {stroen}, Квартира= {kvar}, Телефон= '{tel}', ИНН= '{inn}' where ID ={id}";
                    }
                    else
                    {
                        changeQwery = $"update Поставщик set Название = '{name}', Банк_ID = {banks_id}, РасчётныйСчёт = '{rasch}', Улица_ID ={streets_id}, Дом = {house}, Строение= NULL, Квартира= {kvar}, Телефон= '{tel}', ИНН= '{inn}' where ID ={id}";
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
            var banks_id = 0;
            var rasch = textBox3.Text;
            var streets_id = 0;
            int house;
            int kvar;
            var tel = textBox7.Text;
            var inn = textBox8.Text;
            bool isNumber1 = int.TryParse(textBox4.Text, out house);
            bool isNumber3 = int.TryParse(textBox6.Text, out kvar);
            // Обратный поиск ID по Наименованием и названиям виду упаковки, категории, производителю.
            b.openConnection();

            // Поиск Улица_ID.
            var qwery1 = $"select ID from Улица where Наименование = '{comboBox2.Text}'";
            var command = new OleDbCommand(qwery1, b.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                streets_id = reader.GetInt32(0);
            }
            reader.Close();
            // Поиск Банк_ID.
            var qwery2 = $"select ID from Банк where Наименование = '{comboBox1.Text}'";
            var command2 = new OleDbCommand(qwery2, b.getConnection());
            OleDbDataReader reader2 = command.ExecuteReader();
            while (reader2.Read())
            {
                banks_id = reader2.GetInt32(0);
            }
            reader2.Close();

            // Проверка ввода.
            if (isNumber1 == true && isNumber3 == true && rasch.Length <= 20 && tel.Length <= 12 && inn.Length <= 10 && name != "" && Convert.ToInt32(house) > 0 && Convert.ToInt32(kvar) > 0)
            {
                int stroen_int;
                bool isNumber2 = int.TryParse(textBox5.Text, out stroen_int);
                if (isNumber2 == true)
                {
                    dataGridView1.Rows[selectedRowindex].SetValues(id, name, banks_id, rasch, streets_id, house, stroen_int, kvar, tel, inn);
                }
                else
                {
                    dataGridView1.Rows[selectedRowindex].SetValues(id, name, banks_id, rasch, streets_id, house, 0, kvar, tel, inn);
                }
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
