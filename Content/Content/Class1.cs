using Content;
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
        static EventHandler ContentF()
        {
            return ContentF_Click;
        }
        private static void ContentF_Click(object sender, EventArgs e)
        {
            ContenForm form = new ContenForm();
            form.ShowDialog();
        }
    }
}
