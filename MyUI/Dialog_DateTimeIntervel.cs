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
    public partial class Dialog_DateTimeIntervel : MyDialog
    {
        public DateTime DateTime_st;
        public DateTime DateTime_end;

        public Dialog_DateTimeIntervel(DateTime dateTime_st ,DateTime dateTime_end)
        {
            InitializeComponent();
            Basic.Reflection.MakeDoubleBuffered(this, true);
            this.DateTime_st = dateTime_st;
            this.DateTime_end = dateTime_end;
            this.Load += Dialog_DateTimeIntervel_Load;
            this.LoadFinishedEvent += Dialog_DateTimeIntervel_LoadFinishedEvent;
            this.plC_RJ_Button_確認.MouseDownEvent += PlC_RJ_Button_確認_MouseDownEvent;
            this.plC_RJ_Button_返回.MouseDownEvent += PlC_RJ_Button_返回_MouseDownEvent;
            this.rJ_TextBox_起始時間_時.Click += RJ_TextBox_Click;
            this.rJ_TextBox_起始時間_分.Click += RJ_TextBox_Click;
            this.rJ_TextBox_起始時間_秒.Click += RJ_TextBox_Click;

            this.rJ_TextBox_結束時間_時.Click += RJ_TextBox_Click;
            this.rJ_TextBox_結束時間_分.Click += RJ_TextBox_Click;
            this.rJ_TextBox_結束時間_秒.Click += RJ_TextBox_Click;

  

        }


        #region Event
        private void RJ_TextBox_Click(object sender, EventArgs e)
        {
            RJ_TextBox rJ_TextBox = (RJ_TextBox)sender;
            double temp = rJ_TextBox.Text.StringToDouble();
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
            this.calendarStart.Focus();
        }
        private void Dialog_DateTimeIntervel_LoadFinishedEvent(EventArgs e)
        {
            this.calendarStart.Focus();
        }
        private void Dialog_DateTimeIntervel_Load(object sender, EventArgs e)
        {
            calendarStart.Date = this.DateTime_st;
            calendarEnd.Date = this.DateTime_end;

            this.rJ_TextBox_起始時間_時.Texts = this.DateTime_st.Hour.ToString("00");
            this.rJ_TextBox_起始時間_分.Texts = this.DateTime_st.Minute.ToString("00");
            this.rJ_TextBox_起始時間_秒.Texts = this.DateTime_st.Second.ToString("00");

            this.rJ_TextBox_結束時間_時.Texts = this.DateTime_end.Hour.ToString("00");
            this.rJ_TextBox_結束時間_分.Texts = this.DateTime_end.Minute.ToString("00");
            this.rJ_TextBox_結束時間_秒.Texts = this.DateTime_end.Second.ToString("00");
        }
        private void PlC_RJ_Button_返回_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }));
        }
        private void PlC_RJ_Button_確認_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                DateTime_st = new DateTime(calendarStart.Date.Year, calendarStart.Date.Month, calendarStart.Date.Day, this.rJ_TextBox_起始時間_時.Texts.StringToInt32(), this.rJ_TextBox_起始時間_分.Texts.StringToInt32(), this.rJ_TextBox_起始時間_秒.Texts.StringToInt32());
                DateTime_end = new DateTime(calendarEnd.Date.Year, calendarEnd.Date.Month, calendarEnd.Date.Day, this.rJ_TextBox_結束時間_時.Texts.StringToInt32(), this.rJ_TextBox_結束時間_分.Texts.StringToInt32(), this.rJ_TextBox_結束時間_秒.Texts.StringToInt32());

                this.DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }
        #endregion

    }
}
