using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CallBackUI
{
    static public class panel
    {    
        private delegate void 是否隱藏_CallBack(bool 隱藏, Panel ctl);
        static public void 是否隱藏(bool 隱藏, Panel ctl)
        {
            if (ctl.InvokeRequired)
            {
                是否隱藏_CallBack myUpdate = new 是否隱藏_CallBack(是否隱藏);
                ctl.Invoke(myUpdate, 隱藏, ctl);
            }
            else
            {
                ctl.Visible = !隱藏;
            }
        }
        private delegate void 取得焦點_CallBack(Panel ctl);
        static public void 取得焦點(Panel ctl)
        {
            if (ctl.InvokeRequired)
            {
                取得焦點_CallBack myUpdate = new 取得焦點_CallBack(取得焦點);
                ctl.Invoke(myUpdate,  ctl);
            }
            else
            {
                ctl.Focus();
            }
        }
    }
}
