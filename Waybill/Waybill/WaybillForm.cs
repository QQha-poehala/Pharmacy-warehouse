using System;
using System.Data.OleDb;
using System.Windows.Forms;
using database;

namespace Waybill
{
    public partial class WaybillForm : Form
    {
        bool W;
        DataB b = new DataB();
        public WaybillForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            RightsFunction();
            AddItems();
            if (W == false)
            {
                button1.Enabled = false;
            }
        }
        // Авторизация прав пользователя на меню Накладная.
        private void RightsFunction()
        {
            string qwertystring = $"select * from Rights where ID_Menu = 7";
            OleDbCommand command = new OleDbCommand(qwertystring, b.getConnection());
            b.openConnection();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                W = Convert.ToBoolean(reader.GetBoolean(2));
            }
            reader.Close();
            b.closeConnection();
        }
        private void AddItems()
        {
            // Поиск Поставщиков и добавление вкладок.
            var qwery1 = $"select * from Поставщик";
            var command = new OleDbCommand(qwery1, b.getConnection());
            b.openConnection();
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["Название"].ToString().Trim());
            }
            reader.Close();
            b.closeConnection();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text.Length > 0)
            {
                b.openConnection();
                int post_id = 0;
                int postavstchik_id = 0;
                // Поиск Поставщик_ID.
                var qwery1 = $"select ID from Поставщик where Название = '{comboBox1.Text}'";
                var command = new OleDbCommand(qwery1, b.getConnection());
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    postavstchik_id = reader.GetInt32(0);
                }
                reader.Close();
                string addpost = $"insert into Поставка (Поставщик_ID, Дата ) values ({postavstchik_id},  '{dateTimePicker1.Value}')";
                var command1 = new OleDbCommand(addpost, b.getConnection());
                command1.ExecuteNonQuery();

                // Получение ID созданной поставки.
                string qwerty = $"SELECT * FROM Поставка WHERE Поставщик_ID = {postavstchik_id} AND ID = (SELECT MAX(ID) FROM Поставка)";
                OleDbCommand command2 = new OleDbCommand(qwerty, b.getConnection());
                OleDbDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    post_id = Convert.ToInt32(reader2.GetInt32(0));
                }
                reader2.Close();
                b.closeConnection();
                Close();
                NakladForm frm = new NakladForm(post_id);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не выбран поставщик", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
