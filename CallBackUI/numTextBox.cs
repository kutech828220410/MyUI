using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
namespace CallBackUI
{
    static public class numTextBox
    {
        private delegate void 取得字串_CallBack(ref string str, NumTextBox ctl);
        static public void 取得字串(ref string str, NumTextBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                取得字串_CallBack myUpdate = new 取得字串_CallBack(取得字串);
                str = ctl.Text;
                ctl.Invoke(myUpdate, str, ctl);
            }
            else
            {
                str = ctl.Text;
            }
        }
        private delegate void 字串更換_CallBack(string myStr, NumTextBox ctl);
        static public void 字串更換(string myStr, NumTextBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                字串更換_CallBack myUpdate = new 字串更換_CallBack(字串更換);
                ctl.Invoke(myUpdate, myStr, ctl);
            }
            else
            {
                ctl.Text = myStr;
            }
        }

        private delegate void Enable_CallBack(bool flag, NumTextBox ctl);
        static public void Enable(bool flag, NumTextBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                Enable_CallBack myUpdate = new Enable_CallBack(Enable);
                ctl.Invoke(myUpdate, flag, ctl);
            }
            else
            {
                ctl.Enabled = flag;
            }
        }
    }
}
