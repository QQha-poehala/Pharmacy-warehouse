using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices;
namespace MyProj
{
    public class Class1
    {
        static EventHandler InvoicesF()
        {
            return InvoicesF_Click;
        }
        private static void InvoicesF_Click(object sender, EventArgs e)
        {
            InvoicesForm form = new InvoicesForm();
            form.ShowDialog();
        }

    }
}
