using Banks;
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
        static EventHandler BanksF()
        {
            return BanksF_Click;
        }
        private static void BanksF_Click(object sender, EventArgs e)
        {
            BanksForm form = new BanksForm();
            form.ShowDialog();
        }

    }
}