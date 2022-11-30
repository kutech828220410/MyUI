using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
namespace CallBackUI
{
    static public class textbox
    {
        private delegate void 取得字串_CallBack(ref string str, TextBox ctl);
        static public void 取得字串(ref string str,TextBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                取得字串_CallBack myUpdate = new 取得字串_CallBack(取得字串);
                str = ctl.Text;
                if (ctl.IsHandleCreated) ctl.Invoke(myUpdate, str, ctl);
            }
            else
            {
                str = ctl.Text;
            }
        }
        private delegate void 字串更換_CallBack(string myStr, TextBox ctl);
        static public void 字串更換(string myStr, TextBox ctl)
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
