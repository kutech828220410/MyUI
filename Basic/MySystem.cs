using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Basic
{
    public class MySystem
    {
        public static bool IsSystem_x64()
        {
            return (IntPtr.Size == 8);
        }
 
    }
}
