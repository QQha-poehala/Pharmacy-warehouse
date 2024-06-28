using Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ActivationContext;

namespace MyProj
{
    public class Class1
    {
        static EventHandler OrderF()
        {
            return OrderF_Click;
        }
        private static void OrderF_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы хотите сформировать новый заказ?", "Заказ",  MessageBoxButtons.YesNo , MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.Yes:
                    {
                        OrderForm form = new OrderForm();
                        form.ShowDialog();
                        break;
                    }
                    
                case DialogResult.No:
                    break;
            }
        }
    }
}
