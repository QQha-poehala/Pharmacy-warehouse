using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using database;
using System.Data.OleDb;

namespace KurovayaBD
{
    public partial class Menu1 : Form
    {
        Form1 form1 = new Form1();
        DataB dataBase = new DataB();
        public Menu1(Form1 form, int ID)
        {
            InitializeComponent();
            // Переменные для хранения информации о вкладках для каждого уровня иерархии.
            ToolStripMenuItem menu = new ToolStripMenuItem();
            ToolStripMenuItem soMenu = new ToolStripMenuItem();

            form1 = form;
            DataTable MenuBD = new DataTable();
            // добавление данных в основное меню
            UPD();
            // Создаём запрос на права пользователя.
            dataBase.openConnection();
            string qwerystring3 = $"select * from AccessRights where ID_User ={ID} order by ID_Menu";
            OleDbCommand command1 = new OleDbCommand(qwerystring3, dataBase.getConnection());
            OleDbDataReader reader = command1.ExecuteReader();

            // Коллекции типа ключ-значения, для хранения прав пользователя.
            List<int> MENU = new List<int>();
            Dictionary<int, bool> R = new Dictionary<int, bool>();
            Dictionary<int, bool> W = new Dictionary<int, bool>();
            Dictionary<int, bool> E = new Dictionary<int, bool>();
            Dictionary<int, bool> D = new Dictionary<int, bool>();
            while (reader.Read())
            {
                MENU.Add(reader.GetInt32(1));
                R.Add(reader.GetInt32(1), reader.GetBoolean(2));
                W.Add(reader.GetInt32(1), reader.GetBoolean(3));
                E.Add(reader.GetInt32(1), reader.GetBoolean(4));
                D.Add(reader.GetInt32(1), reader.GetBoolean(5));
            }
            reader.Close();

            // Добавление прав конкретного пользователя в таблицу Rights.
            for (int i = 0; i < MENU.Count; i++)
            {
                var addQwery = $"insert into Rights values ({MENU[i]},{R[MENU[i]]},{W[MENU[i]]}, {E[MENU[i]]}, {D[MENU[i]]}, {ID})";
                var command4 = new OleDbCommand(addQwery, dataBase.getConnection());
                command4.ExecuteNonQuery();
            }

            // Создание запроса на получение данных о меню.
            string qwerystring2 = $"select * from Menu order by ID, Порядок";
            OleDbCommand command = new OleDbCommand(qwerystring2, dataBase.getConnection());
            reader = command.ExecuteReader();
            // Данные из таблицы Menu.
            int ID_menu;
            int ID_parent;
            string name;
            string name_dll = "";
            string nameF = "";

            // Количество вкладок на панеле.
            int countMenus;
            // Количество подвкладок на панеле.
            int countSoMenus;
            while (reader.Read())
            {
                ID_menu = reader.GetInt32(0);
                ID_parent = reader.GetInt32(1);
                name = reader.GetString(2).Trim();
                try
                {
                    name_dll = reader.GetString(3).Trim() + ".dll";
                    nameF = reader.GetString(4).Trim();
                }
                catch (Exception) { name_dll = ""; nameF = ""; }
                // Добавление вкладки в панель если меню не имеет предков.
                if (ID_parent == 0)
                {
                    menu = new ToolStripMenuItem();
                    menu.Text += name;
                    if (R[ID_menu] == false)
                    {
                        menu.Enabled = false;
                    }
                    // Добавляем вкладку на панель.
                    TopMenu.Items.Add(menu);
                    countMenus = TopMenu.Items.Count - 1;
                    if (name_dll != "")
                    {
                        try
                        {
                            // Загружаем dll.
                            Assembly asm = Assembly.LoadFrom(name_dll);
                            // Получаем класс.
                            Type? t = asm.GetType("MyProj.Class1");
                            // Получаем метод обработки запросов.
                            MethodInfo? getMethod = t.GetMethod(nameF, BindingFlags.NonPublic | BindingFlags.Static);
                            // Вызываем метод из dll и передаём данные о правах пользователя.
                            EventHandler? result = getMethod?.Invoke(null, null) as EventHandler;
                            TopMenu.Items[countMenus].Click += result;
                        }
                        catch (Exception) { }
                    }
                }
                // Если меню - это подменю, то создаён подменю.
                if (ID_parent > 0)
                {
                    soMenu = new ToolStripMenuItem();
                    soMenu.Text += name;
                    if (R[ID_menu] == false)
                    {
                        soMenu.Enabled = false;
                    }
                    menu.DropDownItems.Add(soMenu);
                    countSoMenus = menu.DropDownItems.Count - 1;
                    try
                    {
                        Assembly asm = Assembly.LoadFrom(name_dll);
                        Type? t = asm.GetType("MyProj.Class1");
                        MethodInfo? getMethod = t.GetMethod(nameF, BindingFlags.Static | BindingFlags.NonPublic);
                        EventHandler? result = getMethod?.Invoke(null, null) as EventHandler;
                        menu.DropDownItems[countSoMenus].Click += result;
                    }
                    catch (Exception) { }
                }
            }
            reader.Close();
            dataBase.closeConnection();
        }
        private void Menu1_FormClosing(object sender, FormClosingEventArgs e)
        {
            form1.Close();
        }
        // Закрытие программы
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.Yes:
                    {
                        form1.Close();
                        break;
                    }
                case DialogResult.No:
                    break;
            }
        }
        // Кнопка для обновления основного меню.
        private void button2_Click(object sender, EventArgs e)
        {
            UPD();
        }
        // Обновление данных в основном меню.
        private void UPD()
        {
            dataBase.openConnection();
            // Кол-во поставщиков.
            string qwerystring = $"select count(*) from Поставщик";
            OleDbCommand command = new OleDbCommand(qwerystring, dataBase.getConnection());
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                label4.Text = reader.GetInt32(0).ToString();
            }
            reader.Close();
            // Кол-во покупателей.
            string qwerystring2 = $"select count(*) from Покупатель";
            OleDbCommand command2 = new OleDbCommand(qwerystring2, dataBase.getConnection());
            OleDbDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                label5.Text = reader2.GetInt32(0).ToString();
            }
            reader2.Close();
            // Кол-во активных заказов.
            string qwerystring3 = $"select * from `Счёт-фактуры` order by Заказ_ID";
            OleDbCommand command3 = new OleDbCommand(qwerystring3, dataBase.getConnection());
            OleDbDataReader reader3 = command3.ExecuteReader();
            int count = 0;
            int last_id = 0;
            while (reader3.Read())
            {
                if (last_id != reader3.GetInt32(3) && reader3.GetString(4).Trim() == "Активен")
                {

                    count++;
                }
                last_id = reader3.GetInt32(3);
            }
            reader2.Close();
            label6.Text = count.ToString();
            dataBase.closeConnection();
        }
    }
}
