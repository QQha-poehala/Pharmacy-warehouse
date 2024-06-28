using TypePack;
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
        static EventHandler TypePackF()
        {
            return TypePackF_Click;
        }
        private static void TypePackF_Click(object sender, EventArgs e)
        {
            TypePackForm form = new TypePackForm();
            form.ShowDialog();
        }
    }
}