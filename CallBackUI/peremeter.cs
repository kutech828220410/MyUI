using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
namespace CallBackUI
{
    static public class peremeter
    {
        static public void 設定跨執行序存取UI(bool FLAG)
        {
            Form.CheckForIllegalCrossThreadCalls = !FLAG;
        }
        static public void MakeDoubleBuffered(Control control, bool setting)
        {
            Type controlType = control.GetType();
            PropertyInfo pi = controlType.GetProperty("DoubleBuffered",
            BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(control, setting, null);
        }
        private delegate void 開啟表單_CallBack(System.Windows.Forms.Form form);
        static public void 開啟表單(System.Windows.Forms.Form form)
        {
            if (form.InvokeRequired)
            {
                開啟表單_CallBack myUpdate = new 開啟表單_CallBack(開啟表單);
                form.Invoke(myUpdate);
            }
            else
            {
                form.Show();
            }
        }
    }
}
