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
using MyUI;
namespace MyUI
{
    public partial class RJ_Calendar : UserControl
    {
        [Category("RJ Code Advance")]
        public DateTime Date
        {
            get
            {
                int Hour = rJ_TextBox_時.Text.StringToInt32();
                int Min = rJ_TextBox_分.Text.StringToInt32();
                int Sec = rJ_TextBox_秒.Text.StringToInt32();
                if (Hour > 23) Hour = 23;
                if (Hour < 00) Hour = 00;
                if (Min > 60) Min = 60;
                if (Min < 00) Min = 00;
                if (Sec > 60) Sec = 60;
                if (Sec < 00) Sec = 00;
                this.calendar.Date = new DateTime(this.calendar.Date.Year, this.calendar.Date.Month, this.calendar.Date.Day, Hour, Min, Sec);
                return this.calendar.Date;
            }
            set
            {
                rJ_TextBox_時.Text = value.Hour.ToString("00");
                rJ_TextBox_分.Text = value.Minute.ToString("00");
                rJ_TextBox_秒.Text = value.Second.ToString("00");

                this.calendar.Date = value;

            }
        }
        [Category("RJ Code Advance")]
        public Font CalendarFont
        {
            get
            {
                return this.calendar.Font;
            }
            set
            {
                this.calendar.Font = value;
            }
        }
        [Category("RJ Code Advance")]
        public Color CalendarForeColor
        {
            get
            {
                return this.calendar.ForeColor;
            }
            set
            {
                this.calendar.ForeColor = value;
            }
        }

        public RJ_Calendar()
        {
            InitializeComponent();
            Basic.Reflection.MakeDoubleBuffered(this, true);
            this.rJ_TextBox_時.Click += RJ_TextBox_Click;
            this.rJ_TextBox_分.Click += RJ_TextBox_Click;
            this.rJ_TextBox_秒.Click += RJ_TextBox_Click;

        }

        private void RJ_TextBox_Click(object sender, EventArgs e)
        {
            RJ_TextBox rJ_TextBox = (RJ_TextBox)sender;
            double temp = rJ_TextBox.Text.StringToInt32();
            if (temp < 0) temp = 0;

            Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel(0);
            if (dialog_NumPannel.ShowDialog() != DialogResult.Yes) return;
            temp = dialog_NumPannel.Value;
            if (rJ_TextBox.PlaceholderText == "時")
            {
                if (temp > 23) temp = 23;
            }
            if (rJ_TextBox.PlaceholderText == "分")
            {
                if (temp > 59) temp = 59;
            }
            if (rJ_TextBox.PlaceholderText == "秒")
            {
                if (temp > 59) temp = 59;
            }
            rJ_TextBox.Text = temp.ToString("00");
            this.calendar.Focus();
        }
    }
}
