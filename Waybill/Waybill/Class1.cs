using Waybill;
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
        static EventHandler WaybillF()
        {
            return WaybillF_Click;
        }
        private static void WaybillF_Click(object sender, EventArgs e)
        {
            WaybillForm form = new WaybillForm();
            form.ShowDialog();
        }
    }
}
