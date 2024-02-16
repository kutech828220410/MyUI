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
    public partial class Dialog_AlarmForm : Form
    {
        public string Value = "";
        private MyThread MyThread_program;
        private string _title = "";
        private int _time_ms = 1000;
        private int _Y_offset = 0;
        private int _X_offset = 0;
        private Color backColor = Color.DarkRed;
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
        public Dialog_AlarmForm(string title, int time_ms)
        {
            InitializeComponent();
            _title = title;
            _time_ms = time_ms;

            this.Load += Dialog_AlarmForm_Load;
            this.FormClosing += Dialog_AlarmForm_FormClosing;
            this._Y_offset = 0;
            this._X_offset = 0;
        }
        public Dialog_AlarmForm(string title, int time_ms, Color bkColor)
        {
            InitializeComponent();
            _title = title;
            _time_ms = time_ms;

            this.Load += Dialog_AlarmForm_Load;
            this.FormClosing += Dialog_AlarmForm_FormClosing;
            this._Y_offset = 0;
            this._X_offset = 0;
            this.backColor = bkColor;
         
        }
        public Dialog_AlarmForm(string title, int time_ms, int x_offset, int y_offset,Color bkColor)
        {
            InitializeComponent();
            _title = title;
            _time_ms = time_ms;

            this.Load += Dialog_AlarmForm_Load;
            this.FormClosing += Dialog_AlarmForm_FormClosing;
            this._Y_offset = y_offset;
            this._X_offset = x_offset;
            this.backColor = bkColor;
        }
        public Dialog_AlarmForm(string title, int time_ms, int x_offset, int y_offset)
        {
            InitializeComponent();
            _title = title;
            _time_ms = time_ms;

            this.Load += Dialog_AlarmForm_Load;
            this.FormClosing += Dialog_AlarmForm_FormClosing;
            this._Y_offset = y_offset;
            this._X_offset = x_offset;


        }
        private void sub_program()
        {
            System.Threading.Thread.Sleep(_time_ms);
            this.Invoke(new Action(delegate
            {
                this.Close();
            }));

        }
        private void Dialog_AlarmForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MyThread_program != null)
            {
                MyThread_program.Abort();
                MyThread_program = null;
            }
        }

        private void Dialog_AlarmForm_Load(object sender, EventArgs e)
        {
            this.rJ_Lable1.BackgroundColor = this.backColor;
            this.rJ_Lable1.Text = _title;
       
            this.Location = new Point((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width) / 2 + _X_offset, (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height) / 2 + _Y_offset);

            MyThread_program = new MyThread();
            MyThread_program.Add_Method(sub_program);
            MyThread_program.AutoRun(true);
            MyThread_program.SetSleepTime(10);
            MyThread_program.Trigger();
        }
    }
}
