using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
namespace Basic
{
    public partial class Dialog_DateTime : Form
    {

        public static Form form;
        public DialogResult ShowDialog()
        {
            if (form == null)
            {
                base.ShowDialog();
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    base.ShowDialog();
                }));
            }

            return this.DialogResult;
        }
        public DateTime Value
        {
            get
            {
                return dateTimeComList.Value;
            }
        }
        public Dialog_DateTime()
        {
            InitializeComponent();
            this.Init("", dateTimeComList.Start_Year, dateTimeComList.End_Year, dateTimeComList.Value);
        }
        public Dialog_DateTime(DateTime dateTime)
        {
            InitializeComponent();
            this.Init("", dateTimeComList.Start_Year, dateTimeComList.End_Year, dateTime);
        }
        public Dialog_DateTime(int start_Year, int end_Year, DateTime dateTime)
        {
            InitializeComponent();
            this.Init("", start_Year, end_Year, dateTime);
        }
        public  Dialog_DateTime(int start_Year, int end_Year)
        {
            InitializeComponent();
            this.Init("", start_Year, end_Year, DateTime.Now);
        }
        public Dialog_DateTime(string FormText, int start_Year, int end_Year)
        {
            InitializeComponent();
            this.Init(FormText, start_Year, end_Year , DateTime.Now);
        }
        private void Init(string FormText, int start_Year, int end_Year , DateTime dateTime)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = FormText;
            dateTimeComList.Start_Year = start_Year;
            dateTimeComList.End_Year = end_Year;
            dateTimeComList.Value = dateTime;
            this.Load += Dialog_DateTime_Load;
     
        }

        private void Dialog_DateTime_Load(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            rJ_Button_OK.MouseDownEvent += RJ_Button_OK_MouseDownEvent;
            rJ_Button_Cancel.MouseDownEvent += RJ_Button_Cancel_MouseDownEvent;
        }

        private void RJ_Button_Cancel_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }));
        }

        private void RJ_Button_OK_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }

        private void rJ_Button_OK_Click(object sender, EventArgs e)
        {
        }

        private void rJ_Button_Cancel_Click(object sender, EventArgs e)
        {
        }

    }
}
