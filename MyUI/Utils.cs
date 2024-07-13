using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MyUI
{
    public static class Utils
    {
        public static void CenterControlInPanel(this Panel panel, Control control)
        {
            // 計算Control置中的位置
            int x = (panel.Width - control.Width) / 2;
            int y = (panel.Height - control.Height) / 2;
            // 設置Control的位置
            control.Location = new System.Drawing.Point(x, y);
        }
    }
}
