using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CallBackUI
{
    static public class checkbox
    {
        private delegate void Checked_CallBack(bool flag, CheckBox ctl);
        static public void Checked(bool flag, CheckBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                Checked_CallBack myUpdate = new Checked_CallBack(Checked);
                ctl.Invoke(myUpdate, flag, ctl);
            }
            else
            {
                ctl.Checked = flag;
            }
        }
    }
}
