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
            this.DialogResult = DialogResult.None;
        }
        private void rJ_Button_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void rJ_Button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

    }
}
