using Streets;
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
        static EventHandler StreetsF()
        {
            return StreetsF_Click;
        }
        private static void StreetsF_Click(object sender, EventArgs e)
        {
            StreetsForm form = new StreetsForm();
            form.ShowDialog();
        }
    }
}
