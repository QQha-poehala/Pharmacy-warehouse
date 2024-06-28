using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using aboutPr;
namespace MyProj
{
    public class Class1
    {
        static EventHandler aboutPrF()
        {
            return aboutPrF_Click;
        }
        private static void aboutPrF_Click(object sender, EventArgs e)
        {
            aboutPrForm form = new aboutPrForm();
            form.ShowDialog();
        }

    }
}