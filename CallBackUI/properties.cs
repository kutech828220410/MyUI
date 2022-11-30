using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace CallBackUI
{
    static public class properties
    {
        
        static public void DobleBuffer<control>(this control ctrl,bool flag)
        {
            if (ctrl != null)
            {
                ctrl.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(ctrl, flag, null);
             
            }
        }
    }
}
