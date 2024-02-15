using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PopupControl;
using Basic;
namespace MyUI
{
    public partial class DateTimeIntervelPicker : UserControl
    {
        private Popup popup;
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
        public DateTimeIntervelPicker()
        {
            InitializeComponent();
            btnPopup.Click += BtnPopup_Click;
            DateTimeIntervelPopup content = new DateTimeIntervelPopup();
            popup = new Popup(content);
            popup.Dock = DockStyle.Bottom;
            //添加DateTimeIntervelPopup点击按钮事件
            content.SureClick += new DateTimeIntervelPopup.BtnClickHandle(content_SureClick);

            rJ_Lable_起始.Text = startTime.ToDateTimeString();
            rJ_Lable_結束.Text = endTime.ToDateTimeString();

            content.SetDateTime(startTime, endTime);
        }

        private void BtnPopup_Click(object sender, EventArgs e)
        {
            popup.Show(this);
        }

        private void content_SureClick(object sender, EventArgs e, DateTime start, DateTime end)
        {
            startTime = start;
            endTime = end;
            rJ_Lable_起始.Text = startTime.ToDateTimeString();
            rJ_Lable_結束.Text = endTime.ToDateTimeString();

            popup.Hide();
        }

  
    }
}
