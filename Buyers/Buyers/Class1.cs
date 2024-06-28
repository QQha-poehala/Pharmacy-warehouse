using Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyProj
{
    public class Class1
    {
        static EventHandler BuyersF()
        {
            return BuyersF_Click;
        }
        private static void BuyersF_Click(object sender, EventArgs e)
        {
            BuyersForm form = new BuyersForm();
            form.ShowDialog();
        }
    }
}
