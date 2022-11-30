using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CallBackUI
{
    static public class label
    {
        private delegate void 字串更換_CallBack(string myStr, Control ctl);
        static public void 字串更換(string myStr, Control ctl)
        {
            if (ctl.InvokeRequired)
            {
                字串更換_CallBack myUpdate = new 字串更換_CallBack(字串更換);
                if (ctl.IsHandleCreated) ctl.BeginInvoke(myUpdate, myStr, ctl);
            }
            else
            {
                ctl.Text = myStr;
            }
        }
    }
}
