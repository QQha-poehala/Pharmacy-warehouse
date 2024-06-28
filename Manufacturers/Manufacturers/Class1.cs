using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manufacturers;
namespace MyProj
{
    public class Class1
    {
        static EventHandler ManufacturersF()
        {
            return ManufacturersF_Click;
        }
        private static void ManufacturersF_Click(object sender, EventArgs e)
        {
            ManufacturersForm form = new ManufacturersForm();
            form.ShowDialog();
        }

    }
}