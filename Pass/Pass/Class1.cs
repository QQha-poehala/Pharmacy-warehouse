using Pass;
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
        static EventHandler PassF()
        {
            return PassF_Click;
        }
        private static void PassF_Click(object sender, EventArgs e)
        {
            PassForm form = new PassForm();
            form.ShowDialog();
        }

    }
}