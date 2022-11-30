using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Basic
{
    static public class Time
    {
        static public double GetTotalMilliseconds()
        {
            return DateTime.Now.TimeOfDay.TotalMilliseconds;
        }
    }
}
