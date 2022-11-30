using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using LadderConnection;
using LadderProperty;
using System.Media;
using Basic;

namespace MyUI
{
   [System.Drawing.ToolboxBitmap(typeof(PLC_AlarmFlow), "PLC_AlarmFlow.bmp")]
    public partial class PLC_AlarmFlow : UserControl
    {
        private bool flag_init = false;
        private LowerMachine PLC;
        private DEVICE device_system = new DEVICE(0, 0, 0, 0, 1, 0, 0, true);
        private MyConvert myConvert = new MyConvert();
        private object TimeUp = false;
        private bool Running = false;
        private MyTimer MyTimer_FlowTick = new MyTimer();
        private MyThread MyThread_Check_AlarmStatus;
        private MyThread MyThread_Show_AlarmString;
        private int Str_Height = 0;
        private int Str_Width = 0;
        #region 自訂屬性
        private List<string> _警報編輯 = new List<string>();
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public List<string> 警報編輯
        {
            get { return _警報編輯; }
            set
            {
                _警報編輯 = value;
            }
        }
        private int _捲動速度 = 200;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int 捲動速度
        {
            get { return _捲動速度; }
            set
            {
                _捲動速度 = value;
            }
        }
        private Color _文字顏色 = Color.White;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Color 文字顏色
        {
            get { return _文字顏色; }
            set
            {
                _文字顏色 = value;
            }
        }
        private Font _文字字體 = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Font 文字字體
        {
            get
            {
                if(!Running)
                {
                    Graphics temp = this.CreateGraphics();
                    SizeF SizeofStr = temp.MeasureString("TT", _文字字體);
                    Str_Height = (int)SizeofStr.Height;
                    Str_Width = (int)SizeofStr.Width;
                    this.Size = new Size(Size.Width, Str_Height);
                }
    
                return _文字字體;
            }
            set
            {
                if (!Running)
                {
                    Graphics temp = this.CreateGraphics();
                    SizeF SizeofStr = temp.MeasureString("TT", value);
                    Str_Height = (int)SizeofStr.Height;
                    Str_Width = (int)SizeofStr.Width;
                    this.Size = new Size(Size.Width, Str_Height);
                }
                _文字字體 = value;
            }
        }
        private bool _顯示警報編號 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue(1)]
        public bool 顯示警報編號
        {
            get { return _顯示警報編號; }
            set
            {
                _顯示警報編號 = value;
            }
        }
        private bool _自動隱藏 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue(1)]
        public bool 自動隱藏
        {
            get { return _自動隱藏; }
            set
            {
                _自動隱藏 = value;
            }
        }
        #endregion
        protected override Size DefaultSize
        {
            get
            {
                Size = new Size(Size.Width, Str_Height);
                return base.DefaultSize;
            }
        }

        public PLC_AlarmFlow()
        {
            InitializeComponent();
            文字字體 = 文字字體;
            this.Dock = DockStyle.Bottom;
           
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
        public void Run(Form form, LowerMachine pLC)
        {
            if (PLC == null)
            {
                if (Set_PLC(pLC))
                {
                    if (!flag_init)
                    {
                        Running = true;
                        this.MyThread_Check_AlarmStatus = new MyThread(form);
                        this.MyThread_Check_AlarmStatus.AutoRun(true);
                        this.MyThread_Check_AlarmStatus.Add_Method(Check_AlarmStatus);
                        this.MyThread_Check_AlarmStatus.SetSleepTime(100);
                        this.MyThread_Check_AlarmStatus.Trigger();

                        this.MyThread_Show_AlarmString = new MyThread(form);
                        this.MyThread_Show_AlarmString.AutoRun(true);
                        this.MyThread_Show_AlarmString.Add_Method(Show_AlarmString);
                        this.MyThread_Show_AlarmString.SetSleepTime(_捲動速度);
                        this.MyThread_Show_AlarmString.Trigger();

                        this.Graphics_label = this.label_Alarm.CreateGraphics();
                        this.StringCurrent_X = this.label_Alarm.Width;

                        this.NumOfWord_Width = (int)this.Graphics_label.MeasureString("X", this._文字字體).Width;
                      
                        flag_init = true;
                    }
                   // PLC.Add_UI_Method(Run);
                }
            }
        }

        #region Check_AlarmStatus
        string[] Check_AlarmStatus_temp;
        string Check_AlarmStatus_temp0;
        string AlarmString;
        string AlarmString_buf;
        object Alarm_flag = new object();
        private void Check_AlarmStatus()
        {
            this.AlarmString_buf = "";
            foreach(string str_Alarm in this._警報編輯)
            {
                Check_AlarmStatus_temp = myConvert.分解分隔號字串(str_Alarm, "\t");
                if (Check_AlarmStatus_temp.Length >= 2)
                {
                    if (DEVICE.TestDevice(Check_AlarmStatus_temp[0]))
                    {
                        Check_AlarmStatus_temp0 = Check_AlarmStatus_temp[0].Remove(1);
                        if (Check_AlarmStatus_temp0 == "Y" || Check_AlarmStatus_temp0 == "M" || Check_AlarmStatus_temp0 == "S")
                        {
                            PLC.properties.Device.Get_Device(Check_AlarmStatus_temp[0], out Alarm_flag);
                            if (Alarm_flag is bool)
                            {
                                if ((bool)Alarm_flag)
                                {
                                    if (this.顯示警報編號) AlarmString_buf += "#" + Check_AlarmStatus_temp[0] + "  ";
                                    AlarmString_buf += Check_AlarmStatus_temp[1] + "          ";
                                }
                            }
                        }
                    }
                }

            }
            if (this.AlarmString != this.AlarmString_buf) this.StringCurrent_X = this.label_Alarm.Width;
            this.AlarmString = this.AlarmString_buf;
        }
        #endregion
        #region Show_AlarmString
        Graphics Graphics_label;
        Graphics Graphics_Draw_Bitmap;
        Bitmap Draw_Bitmap;
        SizeF SizeOfString;
        int StringEnd_X;
        int StringCurrent_X;
        int NumOfWord_Width;
        private void Show_AlarmString()
        {
            if(this.CheckBitmap())
            {
                if (this.AlarmString != "")
                {

                    if (this.Visible == false)
                    {
                        this.Invoke(new Action(delegate
                        {
                            this.Visible = true;
                        }));
                    }
                    Graphics_Draw_Bitmap.FillRectangle(new SolidBrush(Color.Red), new RectangleF(new PointF(0, 0), label_Alarm.Size));
                    SizeOfString = Graphics_Draw_Bitmap.MeasureString(AlarmString, this.文字字體);
                    StringEnd_X = (int)(-SizeOfString.Width);
                    if (StringCurrent_X < StringEnd_X) this.StringCurrent_X = Draw_Bitmap.Width;
                    Graphics_Draw_Bitmap.DrawString(AlarmString, this.文字字體, new SolidBrush(this.文字顏色), new Point(StringCurrent_X, 0));
                    StringCurrent_X -= this.NumOfWord_Width;
                    Graphics_label.DrawImage(Draw_Bitmap, new Point(0, 0));
                }
                else
                {
                    Graphics_Draw_Bitmap.FillRectangle(new SolidBrush(System.Drawing.Color.Gray), new RectangleF(new PointF(0, 0), label_Alarm.Size));
                    Graphics_label.DrawImage(Draw_Bitmap, new Point(0, 0));
                    if (this.Visible == true)
                    {
                        this.Invoke(new Action(delegate
                        {
                            if(this.自動隱藏)this.Visible = false;
                        }));
                    }               
                }
            }
        }
        private bool CheckBitmap()
        {
            if (label_Alarm.Width == 0 || label_Alarm.Height == 0) return false;
            if (Draw_Bitmap == null)
            {
                Draw_Bitmap = new Bitmap(label_Alarm.Width, label_Alarm.Height);
                Graphics_Draw_Bitmap = Graphics.FromImage(Draw_Bitmap);
            }
            if (label_Alarm.Width != Draw_Bitmap.Width || label_Alarm.Height != Draw_Bitmap.Height)
            {
                Draw_Bitmap.Dispose();
                Graphics_Draw_Bitmap.Dispose();
                Draw_Bitmap = new Bitmap(label_Alarm.Width, label_Alarm.Height);
                Graphics_Draw_Bitmap = Graphics.FromImage(Draw_Bitmap);
                this.Graphics_label = this.label_Alarm.CreateGraphics();
            }
            return true;
        }
        #endregion

  
    }
}
