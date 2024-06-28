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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.OleDb;

namespace Banks
{
    public partial class BanksForm : Form
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
        public BanksForm()
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
        // Авторизация прав пользователя на меню Банк.
        private void RightsFunction()
        {
            b.openConnection();
            string qwertystring = $"select * from Rights where ID_Menu = 16";
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
            dataGridView1.Columns.Add("Name", "Наименование");
            // Статус строки.
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns[2].Visible = false;

        }
        private void BanksForm_Load(object sender, EventArgs e)
        {
            CreateColums();
            RefreshDataGrid(dataGridView1);
        }
        // Отчистка таблицы "Запись".
        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
        // Добавление Строки из базы данных в DataGridView.
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1).Trim(), RowState.ModifideNew);
        }
        // Поиск данных в базе данных.
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string qwertystring = $"select * from Банк";
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
        // Запись данных в таблицу "Запись".
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox1.Text = row.Cells[0].Value.ToString().Trim();
                textBox2.Text = row.Cells[1].Value.ToString().Trim();
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
            AddBanks addfrm = new AddBanks();
            addfrm.Show();
        }
        // Метод для поиска данных в БД.
        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string serchString = $"select * from Банк where Наименование like '%" + textBox_search.Text + "%'" + " or ID like '%" + textBox_search.Text+ "%'";
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
                var rowState = (RowState)dataGridView1.Rows[i].Cells[2].Value;
                if (rowState == RowState.Existed) continue;
                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    var deleteQwery = $"delete from Банк where ID = {id}";

                    var command = new OleDbCommand(deleteQwery, b.getConnection());
                    command.ExecuteNonQuery();

                }
                if (rowState == RowState.Modifide)
                {
                    var id = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                    var name = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();

                    var changeQwery = $"update Банк set Наименование = '{name}' where ID ={id}";
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
                dataGridView1.Rows[index].Cells[2].Value = RowState.Deleted;
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

            // Проверка на не пустоту строк и запрос на добавление новой строки в бд.
            if (name != "")
            {
                dataGridView1.Rows[selectedRowindex].SetValues(id, name);
                // установка статуса: Измененная строка.
                dataGridView1.Rows[selectedRowindex].Cells[2].Value = RowState.Modifide;
            }
            else
            {
                MessageBox.Show("Неправильный ввод наименования", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void buttonChange_Click(object sender, EventArgs e)
        {
            Change();
            ClearFields();
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

        // Нажатие на клавишу "Отчистка".
        private void button1_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}

