using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CallBackUI
{
    static public class vScrollBar
    {
        private delegate void 設定最大值_CallBack(int val, System.Windows.Forms.VScrollBar ctl);
        static public void 設定最大值(int val, System.Windows.Forms.VScrollBar ctl)
        {
            if (ctl.InvokeRequired)
            {
                設定最大值_CallBack myUpdate = new 設定最大值_CallBack(設定最大值);
                ctl.Invoke(myUpdate, val, ctl);
            }
            else
            {
                ctl.Maximum = val;
            }
        }
        private delegate void 設定最小值_CallBack(int val, System.Windows.Forms.VScrollBar ctl);
        static public void 設定最小值(int val, System.Windows.Forms.VScrollBar ctl)
        {
            if (ctl.InvokeRequired)
            {
                設定最小值_CallBack myUpdate = new 設定最小值_CallBack(設定最小值);
                ctl.Invoke(myUpdate, val, ctl);
            }
            else
            {
                ctl.Minimum = val;
            }
        }
        private delegate void 設定現在值_CallBack(int val, System.Windows.Forms.VScrollBar ctl);
        static public void 設定現在值(int val, System.Windows.Forms.VScrollBar ctl)
        {
            if (ctl.InvokeRequired)
            {
                設定現在值_CallBack myUpdate = new 設定現在值_CallBack(設定現在值);
                ctl.Invoke(myUpdate, val, ctl);
            }
            else
            {
                ctl.Value = val;
            }
        }
        private delegate void 設定換頁一次值_CallBack(int val, System.Windows.Forms.VScrollBar ctl);
        static public void 設定換頁一次值(int val, System.Windows.Forms.VScrollBar ctl)
        {
            if (ctl.InvokeRequired)
            {
                設定換頁一次值_CallBack myUpdate = new 設定換頁一次值_CallBack(設定換頁一次值);
                ctl.Invoke(myUpdate, val, ctl);
            }
            else
            {
                ctl.LargeChange = val;
            }
        }
        private delegate void 設定換格一次值_CallBack(int val, System.Windows.Forms.VScrollBar ctl);
        static public void 設定換格一次值(int val, System.Windows.Forms.VScrollBar ctl)
        {
            if (ctl.InvokeRequired)
            {
                設定換格一次值_CallBack myUpdate = new 設定換格一次值_CallBack(設定換格一次值);
                ctl.Invoke(myUpdate, val, ctl);
            }
            else
            {
                ctl.SmallChange = val;
            }
        }
        private delegate void 是否隱藏_CallBack(bool val, System.Windows.Forms.VScrollBar ctl);
        static public void 是否隱藏(bool val, System.Windows.Forms.VScrollBar ctl)
        {
            if (ctl.InvokeRequired)
            {
                是否隱藏_CallBack myUpdate = new 是否隱藏_CallBack(是否隱藏);
                ctl.Invoke(myUpdate, val, ctl);
            }
            else
            {
                ctl.Visible = val;
            }
        }
        private delegate void 設定是否致能_CallBack(bool val, System.Windows.Forms.VScrollBar ctl);
        static public void 設定是否致能(bool val, System.Windows.Forms.VScrollBar ctl)
        {
            if (ctl.InvokeRequired)
            {
                設定是否致能_CallBack myUpdate = new 設定是否致能_CallBack(設定是否致能);
                ctl.Invoke(myUpdate, val, ctl);
            }
            else
            {
                ctl.Enabled = val;
            }
        }
    }
}
