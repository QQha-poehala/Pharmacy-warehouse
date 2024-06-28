using System.Data;
using System.Data.OleDb;
using database;
using Md5;
namespace KurovayaBD
{
    public partial class Form1 : Form
    {
        // ������ ��� ���������� �������� ��������� �����.
        System.Windows.Forms.Timer formTimer = new System.Windows.Forms.Timer();

        DataB dataBase = new DataB();
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            ActiveControl = UserName;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Blue;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Black;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            formTimer.Interval = 500;
            formTimer.Start();
            formTimer.Tick += new EventHandler(FormTimer_Tick);
        }
        private void FormTimer_Tick(object sender, EventArgs e)
        {
            CapsLockFlagLabel.Text = (Console.CapsLock ? "������� CapsLock ������" : "");
            if (InputLanguage.CurrentInputLanguage.LayoutName == "���")
                CurrentLanguageLabel.Text = "���� ����� ����������";
            else if (InputLanguage.CurrentInputLanguage.LayoutName == "�������")
                CurrentLanguageLabel.Text = "���� ����� �������";
        }
        private void EnterButton_Click(object sender, EventArgs e)
        {
            // ������� ��� ����� �� ��������� ������� Rights
            dataBase.openConnection();
            string qwery = $"DELETE FROM Rights";
            OleDbCommand command5 = new OleDbCommand(qwery, dataBase.getConnection());
            command5.ExecuteNonQuery();
            

            // ���������� ���������� ������ � ������.
            var loginUser = UserName.Text;
            var passUser = md5.hashPassword(Password.Text);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataTable table = new DataTable();

            // �������� ������ �������.
            string qwerystring = $"select Id, �����, ������ from Users where ����� = '{loginUser}' and ������ = '{passUser}'";

            OleDbCommand command = new OleDbCommand(qwerystring, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count == 1)
            {
                // ��������� id ������������.
                int user_id = 0;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user_id = reader.GetInt32(0);
                }
                reader.Close();
                dataBase.closeConnection();
                Menu1 menu1 = new Menu1(this, user_id);
                menu1.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("�������� ����� ��� ������", "������ �����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Password.Text = "";
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}