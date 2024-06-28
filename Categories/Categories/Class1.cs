using Categories;
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
        static EventHandler CategoriesF()
        {
            return CategoriesF_Click;
        }
        private static void CategoriesF_Click(object sender, EventArgs e)
        {
            CategoriesForm form = new CategoriesForm();
            form.ShowDialog();
        }

    }
}