using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
namespace MyUI
{
    public partial class TimeSetting : UserControl
    {
        public TimeSetting()
        {
            InitializeComponent();
        }
        public String MyTime
        {
            get
            {
                return Hour + ":" + Min + ":" + Second;
            }
        }
        public string Hour
        {
            get
            {
                return numHour.Value.ToString("00");
            }
            set
            {
                if (value.StringIsInt32() == false) return;
                int temp = value.StringToInt32();
                if (temp <= 0 || temp >= 24) return;
                numHour.Value = temp;
            }
        }
        public string Min
        {
            get
            {
                return numMin.Value.ToString("00");
            }
            set
            {
                if (value.StringIsInt32() == false) return;
                int temp = value.StringToInt32();
                if (temp < 0 || temp >= 60) return;
                numMin.Value = temp;
            }
        }
        public string Second
        {
            get
            {
                return numSecond.Value.ToString("00");
            }
            set
            {
                if (value.StringIsInt32() == false) return;
                int temp = value.StringToInt32();
                if (temp < 0 || temp >= 60) return;
                numSecond.Value = temp;
            }
        }
        public void SetTime(int hour, int min, int second)
        {
            Hour = hour.ToString();
            Min = min.ToString();
            Second = second.ToString();
        }
        public string GetTime()
        {
            return MyTime;
        }
    }
}
