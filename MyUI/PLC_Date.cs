using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LadderConnection;
using Basic;

namespace MyUI
{
        [System.Drawing.ToolboxBitmap(typeof(DateTimePicker))]
    public partial class PLC_Date : UserControl
    {
        private MyConvert myConvert = new MyConvert();
        private LowerMachine PLC;
        private bool flag_init = false;
        #region 自訂屬性
        private Font _字體 = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Font 字體
        {
            get { return _字體; }
            set
            {
                _字體 = value;
                if(this.IsHandleCreated)
                {
                    this.Invoke(new Action(delegate { label_Time.Font = _字體; }));
                }
                else
                {
                    label_Time.Font = _字體; 
                }

            }
        }
        public Color foreColor = Color.Black;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public new Color ForeColor
        {
            get { return foreColor; }
            set
            {
                foreColor = value;
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(delegate { label_Time.ForeColor = foreColor; }));
                }
                else
                {
                    label_Time.ForeColor = foreColor;
                }
            }
        }
        #endregion
        #region 隱藏屬性
        [Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        [Browsable(false)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }
        [Browsable(false)]
        public override ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
                base.BackgroundImageLayout = value;
            }
        }
        [Browsable(false)]
        public override Cursor Cursor
        {
            get
            {
                return base.Cursor;
            }
            set
            {
                base.Cursor = value;
            }
        }
        [Browsable(false)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        [Browsable(false)]
        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                base.RightToLeft = value;
            }
        }

        #endregion
        public override System.Windows.Forms.Layout.LayoutEngine LayoutEngine
        {
            get
            {
                if (!flag_init)
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(delegate
                        {
                            GetTime();
                            SizeofStr = label_Time.CreateGraphics().MeasureString(str_Time, _字體);//测量文字长度 
                            SizeofOneStr = label_Time.CreateGraphics().MeasureString("0", _字體);//测量文字长度 
                            this.Size_temp.Width = (int)(SizeofStr.Width + SizeofOneStr.Width * 2);
                            this.Size_temp.Height = (int)(SizeofStr.Height * 2);
                            this.Size = Size_temp;
                        }));
        
                    }
                    else
                    {
                        GetTime();
                        SizeofStr = label_Time.CreateGraphics().MeasureString(str_Time, _字體);//测量文字长度 
                        SizeofOneStr = label_Time.CreateGraphics().MeasureString("0", _字體);//测量文字长度 
                        this.Size_temp.Width = (int)(SizeofStr.Width + SizeofOneStr.Width * 2);
                        this.Size_temp.Height = (int)(SizeofStr.Height * 2);
                        this.Size = Size_temp;
                    }
                }
                return base.LayoutEngine;
            }
        }
        public PLC_Date()
        {
            InitializeComponent();
        }
        public bool Set_PLC(LowerMachine pLC)
        {
            if (pLC != null)
            {
                this.PLC = pLC;
                return true;
            }
            return false;
        }
        public void Run(LowerMachine pLC)
        {
            if (PLC == null)
            {
                if (Set_PLC(pLC))
                {
                    PLC.Add_UI_Method(Run);
                }
            }
        }
        public string GetYear()
        {
            Date = DateTime.Now;
            return Date.Year.ToString("0000");
        }
        public string GetMonth()
        {
            Date = DateTime.Now;
            return Date.Month.ToString("00");
        }
        public string GetDay()
        {
            Date = DateTime.Now;
            return Date.Day.ToString("00");
        }
        public string GetHour()
        {
            Date = DateTime.Now;
            return Date.Hour.ToString("00");
        }
        public string GetMinute()
        {
            Date = DateTime.Now;
            return Date.Minute.ToString("00");
        }
        public string GetSecond()
        {
            Date = DateTime.Now;
            return Date.Second.ToString("00");
        }
        object value;
        DateTime Date = DateTime.Now;
        string Year = "YY";
        string Month = "MM";
        string Day = "DD";
        string Hour = "HH";
        string Minute = "MM";
        string Second = "SS";
        string DayOfWeek;
        string str_Time;
        SizeF SizeofStr;
        SizeF SizeofOneStr;
        Size Size_temp;
        public void Run()
        {
            flag_init = true;
            GetTime();
        }
        void GetTime()
        {
            _字體 = label_Time.Font;
            Date = DateTime.Now;
            Year = Date.Year.ToString("0000");
            Month = Date.Month.ToString("00");
            Day = Date.Day.ToString("00");
            Hour = Date.Hour.ToString("00");
            Minute = Date.Minute.ToString("00");
            Second = Date.Second.ToString("00");
            str_Time = Year + "/" + Month + "/" + Day + " " + Hour + ":" + Minute + ":" + Second;
            if (this.IsHandleCreated)
            {
                this.Invoke(new Action(delegate
                {
                    label_Time.Text = str_Time;
                }));
            }
        }

        private void PLC_Date_Load(object sender, EventArgs e)
        {
            Basic.Reflection.MakeDoubleBuffered(this.label_Time, true);
        }
    }
}
