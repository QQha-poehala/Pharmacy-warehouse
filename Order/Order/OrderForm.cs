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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Order
{
    public partial class OrderForm : Form
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
        public int id_user;
        public int id_zak;
        public OrderForm()
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
            // Получение ID пользователя.
            string qwertystring = $"select * from Rights where ID_Menu = 8";
            OleDbCommand command = new OleDbCommand(qwertystring, b.getConnection());
            b.openConnection();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id_user = Convert.ToInt32(reader.GetInt32(5));
            }
            reader.Close();
            
            // Добавление заказа.
            string addQwery = $"insert into Заказ (Покупатель_ID) values ({id_user})";
            var command4 = new OleDbCommand(addQwery, b.getConnection());
            command4.ExecuteNonQuery();

            // Получение ID созданного заказа.
            string qwerty = $"SELECT * FROM Заказ WHERE Покупатель_ID = {id_user} AND ID = (SELECT MAX(ID) FROM Заказ)";
            OleDbCommand command2 = new OleDbCommand(qwerty, b.getConnection());
            OleDbDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                id_zak = Convert.ToInt32(reader2.GetInt32(0));
            }
            reader2.Close();
            b.closeConnection();
        }
        // Авторизация прав пользователя на меню Производитель.
        private void RightsFunction()
        {
            string qwertystring = $"select * from Rights where ID_Menu = 8";
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
            dataGridView1.Columns.Add("lekar", "Лекарство");
            dataGridView1.Columns.Add("kol", "Количество");
            dataGridView1.Columns.Add("zak_id", "Заказ_id");
            dataGridView1.Columns.Add("Status", "Статус");
            // Статус строки.
            dataGridView1.Columns.Add("IsNew", String.Empty);

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;

        }
        // Отчистка таблицы "Запись".
        private void ClearFields()
        {
            textBox1.Text = "";
            numericUpDown1.Value = 1;
            textBox2.Text = "";
            label3.Visible = false;
        }
        // Добавление Строки из базы данных в DataGridView.
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetInt32(3),  record.GetString(4), RowState.ModifideNew);
        }
        // Поиск данных в базе данных.
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string qwertystring = $"select * from `Счёт-фактуры` where Заказ_ID = {id_zak}";
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
        private void OrderForm_Load(object sender, EventArgs e)
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
                var id = row.Cells[1].Value;
                // Поиск Лекарства по id для получения названия и количества.
                var qwery1 = $"select * from Лекарство where ID = {id}";
                var command = new OleDbCommand(qwery1, b.getConnection());
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    textBox2.Text = reader["Название"].ToString();
                    label3.Visible = true;
                    textBox1.Text = reader["Цена"].ToString();
                    label3.Text = reader["Количество"].ToString();
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
            AddLekZak addfrm = new AddLekZak(id_user, id_zak, dataGridView1);
            RefreshDataGrid(dataGridView1);
            addfrm.Show();
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
                    var deleteQwery = $"delete from `Счёт-фактуры` where ID = {id}";

                    var command = new OleDbCommand(deleteQwery, b.getConnection());
                    command.ExecuteNonQuery();

                }
                if (rowState == RowState.Modifide)
                {
                    var id = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                    var kol = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                    // Обновление данных
                    string changeQwery = $"update `Счёт-фактуры` set Количество = {kol} where ID ={id}";
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
        // Событие "Сохраниение".
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Update();
        }
        private void Change()
        {
            int selectedRowindex = dataGridView1.CurrentCell.RowIndex;
            b.openConnection();
            int kol = Convert.ToInt32(numericUpDown1.Value);

            // Проверка ввода.
            if (kol <= Convert.ToInt32(label3.Text))
            { 
                dataGridView1.Rows[selectedRowindex].Cells[2].Value = kol;
                dataGridView1.Rows[selectedRowindex].Cells[5].Value = RowState.Modifide;
            }
            else
            {
                MessageBox.Show("Неправильный ввод", "Создание записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            b.closeConnection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            b.openConnection();
            // Обновление cтатусов у всех позиций заказа
            string changeQwery = $"update `Счёт-фактуры` set Статус = 'Активен' where Заказ_ID ={id_zak}";
            var command2 = new OleDbCommand(changeQwery, b.getConnection());
            command2.ExecuteNonQuery();
            b.closeConnection();
            MessageBox.Show("Заказ сформирован! Вам перезвонит менеджер для оплаты заказа.", "Заказ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
