using Setting;
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
        static EventHandler SettingF()
        {
            return SettingF_Click;
        }
        private static void SettingF_Click(object sender, EventArgs e)
        {
            SettingForm form = new SettingForm();
            form.ShowDialog();
        }
    }
}
