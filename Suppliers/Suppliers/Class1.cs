using Suppliers;
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
        static EventHandler SuppliersF()
        {
            return SuppliersF_Click;
        }
        private static void SuppliersF_Click(object sender, EventArgs e)
        {
            SuppliersForm form = new SuppliersForm();
            form.ShowDialog();
        }
    }
}
