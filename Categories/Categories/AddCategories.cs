﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using database;
using System.Data.OleDb;

namespace Categories
{
    public partial class AddCategories : Form
    {
        DataB database = new DataB();
        public AddCategories()
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
                var addQwery = $"insert into Категория (Наименование) values ('{name}')";

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
