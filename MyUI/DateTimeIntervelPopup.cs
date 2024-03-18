using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyUI
{
    public partial class DateTimeIntervelPopup : UserControl
    {
        public delegate void BtnClickHandle(object sender, EventArgs e, DateTime start, DateTime end);
        public event BtnClickHandle SureClick;

        private DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
        private DateTime endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
        }

        public DateTimeIntervelPopup()
        {   
            InitializeComponent();
            Application.SetCompatibleTextRenderingDefault(false);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.btnSure.Click += BtnSure_Click;
        }
        public void SetDateTime(DateTime start, DateTime end)
        {
            this.startTime = start;
            this.endTime = end;
            calendarStart.SelectionStart = start;
            calendarEnd.SelectionStart = end;
            timeStart.SetTime(start.Hour, start.Minute, start.Second);
            timeEnd.SetTime(end.Hour, end.Minute, end.Second);
        }
        private void BtnSure_Click(object sender, EventArgs e)
        {
            startTime = calendarStart.SelectionStart;
            String sStart = startTime.Year + "-" + startTime.Month + "-" + startTime.Day + " " + timeStart.MyTime;
            startTime = Convert.ToDateTime(sStart);

            endTime = calendarEnd.SelectionStart;
            String sEnd = endTime.Year + "-" + endTime.Month + "-" + endTime.Day + " " + timeEnd.MyTime;
            endTime = Convert.ToDateTime(sEnd);

            if (SureClick != null)
            {
                SureClick(sender, e, startTime, endTime);  //把日期时间传给DateTimeIntervelPicker
            }
        }
    }
}
