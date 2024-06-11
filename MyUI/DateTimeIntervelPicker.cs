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
        private Size titleSize = new Size(33, 39);
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Size TiTleSize
        {
            get
            {
                return titleSize;
            }
            set
            {
                titleSize = value;
                label_起始.Size = value;
                label_結束.Size = value;
                this.Invalidate();
            }
        }
        private Size dateSize = new Size(217, 39);
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Size DateSize
        {
            get
            {
                return dateSize;
            }
            set
            {
                dateSize = value;
                rJ_Lable_起始.Size = value;
                rJ_Lable_結束.Size = value;
                this.Invalidate();
            }
        }
        private Font titleFont = new Font("新細明體", 9);
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Font TitleFont
        {
            get
            {
                return titleFont;
            }
            set
            {
                titleFont = value;
                label_起始.Font = value;
                label_結束.Font = value;
                this.Invalidate();
            }
        }
        private Font dateFont = new Font("微軟正黑體", 14);
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Font DateFont
        {
            get
            {
                return dateFont;
            }
            set
            {
                dateFont = value;
                rJ_Lable_起始.Font = value;
                rJ_Lable_結束.Font = value;
                this.Invalidate();
            }
        }
        public delegate void BtnClickHandle(object sender, EventArgs e, DateTime start, DateTime end);
        public event BtnClickHandle SureClick;

        private Popup popup;
        private DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
        private DateTime endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }
        public DateTimeIntervelPicker()
        {
            DateTime _startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
            DateTime _endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            init(_startTime, _endTime);

        
        }

   

        public DateTimeIntervelPicker(DateTime startTime, DateTime endTime)
        {
            init(startTime, endTime);
        }
        private void init(DateTime startTime, DateTime endTime)
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            btnPopup.Click += BtnPopup_Click;

            SetDateTime(startTime, endTime);


        }
        public void SetDateTime(DateTime startTime, DateTime endTime)
        {
            this.startTime = startTime;
            this.endTime = endTime;

            DateTimeIntervelPopup content = new DateTimeIntervelPopup();
            popup = new Popup(content);
            popup.Dock = DockStyle.Bottom;

            content.SureClick += new DateTimeIntervelPopup.BtnClickHandle(content_SureClick);

            rJ_Lable_起始.Text = startTime.ToDateTimeString();
            rJ_Lable_結束.Text = endTime.ToDateTimeString();
            startTime = startTime.GetStartDate();
            endTime = endTime.GetEndDate();
            content.SetDateTime(startTime, endTime);
        }
        private void BtnPopup_Click(object sender, EventArgs e)
        {
            Dialog_DateTimeIntervel dialog_DateTimeIntervel = new Dialog_DateTimeIntervel(startTime, endTime);
            if (dialog_DateTimeIntervel.ShowDialog() != DialogResult.Yes) return;
            startTime = dialog_DateTimeIntervel.DateTime_st;
            endTime = dialog_DateTimeIntervel.DateTime_end;
            rJ_Lable_起始.Text = startTime.ToDateTimeString();
            rJ_Lable_結束.Text = endTime.ToDateTimeString();
            if (SureClick != null)
            {
                SureClick(sender, e, startTime, endTime);
            }
        }
        public void OnSureClick()
        {
            content_SureClick(this, new EventArgs(), startTime, endTime);
        }
        private void content_SureClick(object sender, EventArgs e, DateTime start, DateTime end)
        {
            rJ_Lable_起始.Text = startTime.ToDateTimeString();
            rJ_Lable_結束.Text = endTime.ToDateTimeString();

            if (SureClick != null)
            {
                SureClick(sender, e, startTime, endTime); 
            }
        }


    }
}
