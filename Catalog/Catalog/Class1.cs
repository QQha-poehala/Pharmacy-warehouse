using Catalog;
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
        static EventHandler catalogF()
        {
            return catalogF_Click;
        }
        private static void catalogF_Click(object sender, EventArgs e)
        {
            CatalogForm form = new CatalogForm();
            form.ShowDialog();
        }

    }
}
