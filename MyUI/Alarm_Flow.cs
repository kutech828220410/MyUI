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
using CallBackUI;
using LadderConnection;
using LadderProperty;
using System.Media;
using Basic;
namespace MyUI
{
    public partial class Alarm_Flow : UserControl
    {       
        private LowerMachine PLC;
        private DEVICE device_system = new DEVICE(0, 0, 0, 0, 1, 0, 0, true);
        private MyConvert myConvert = new MyConvert();
        private object TimeUp = false;
        private bool Running = false;

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
        private int _捲動速度 = 100;
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
        private Font _文字字體 = new System.Drawing.Font("Courier New", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
                    Size = new Size(Size.Width, Str_Height);
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
                    Size = new Size(Size.Width, Str_Height);
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

        public Alarm_Flow()
        {
            InitializeComponent();
            文字字體 = 文字字體;
            this.Dock = DockStyle.Bottom;
            Running = true;
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
                    PLC.Run_UI.Add(Run);
                }
            }
        }
        public void Run()
        {
            DoWork_UI();
        }

        string[] str_value = new string[2];
        int current_num = 0;
        int width;
        int height;
        string str_temp;
        string str_Alarm_temp;
        PointF 文字繪製位置;
        object flag;
        bool 無警報 = true;
        bool 無警報_temp = false;
        Graphics g;
        SizeF SizeofStr;
        int Str_Height = 0;
        int Str_Width = 0;

        private delegate void VisibleDelegate(bool Visible);
        void 隱藏(bool Visible)
        {
            this.Visible = !Visible;
        }

        private void DoWork_UI()
        {
            width = this.Width;
            height = this.Height;
            str_value = new string[2];
            str_temp = "";
            if (current_num >= 警報編輯.Count)
            {
                if (無警報)
                {
                    Bitmap bmp = null;
                    try
                    {           
                        bmp = new Bitmap(this.Width, this.Height);
                        文字繪製位置 = new PointF(bmp.Width, 0);
                        g = Graphics.FromImage(bmp);
                        pictureBox1.Image = Basic.MyFileStream.DeepClone(bmp);

                        if (自動隱藏)
                        {
                            VisibleDelegate visibleDelegate = new VisibleDelegate(隱藏);
                            Invoke(visibleDelegate, true);
                        } 
               
                    }
                    finally
                    {
                        if (bmp != null) bmp.Dispose();
                    }
                }
                無警報 = true;
                current_num = 0;
            }
            bool flag_error = false;
            if (警報編輯[current_num].IndexOf("\t") < 0)
            {
                flag_error = true;
                current_num++;
                return;
            }
            else
            {
                str_value = myConvert.分解分隔號字串(警報編輯[current_num], "\t");
                if (str_value.Length != 2) 
                {
                    flag_error = true;
                    current_num++;
                    return;
                }
                if (!DEVICE.TestDevice(str_value[0]))
                {
                    flag_error = true;
                    current_num++;
                    return;
                }
                str_temp = str_value[0].Remove(1);
                if (str_temp != "Y" && str_temp != "M" && str_temp != "S")
                {
                    flag_error = true;
                    current_num++;
                    return;
                }
                if (str_value[1] == null)
                {
                    flag_error = true;
                    current_num++;
                    return;
                }
            }
    
            device_system.Set_Device("T0", true);
            device_system.Set_Device("T0", "K" + 捲動速度.ToString(), 2);
            device_system.Get_Device("T0", out TimeUp);

            if (!flag_error)
            {
                PLC.properties.Device.Get_Device(str_value[0], out flag);
                if ((bool)flag)
                {
                    無警報 = false;
                    if (顯示警報編號) str_value[1] = "#" + str_value[0] + " " + str_value[1];
                    if ((bool)TimeUp && height > 0 && width > 0)
                    {
                   
                        Bitmap bmp = null;
                        try
                        {
                            bmp = new Bitmap(this.Width, this.Height);
                            g = Graphics.FromImage(bmp);

                            if (str_Alarm_temp != str_value[0])
                            {
                                str_Alarm_temp = str_value[0];
                                文字繪製位置 = new PointF(bmp.Width, 0);
                            }

                            SizeofStr = g.MeasureString(str_value[1], 文字字體);//测量文字长度 
                            文字繪製位置 = new PointF(文字繪製位置.X - Str_Width, 0);//每次偏移Str_Width  
                            if (文字繪製位置.X <= -SizeofStr.Width)
                            {
                                文字繪製位置 = new PointF(bmp.Width, 0);
                                current_num++;
                            }
                            g.DrawString(str_value[1], 文字字體, new SolidBrush(文字顏色), 文字繪製位置);

                            pictureBox1.Image = Basic.MyFileStream.DeepClone(bmp);
                        }
                        finally
                        {
                            if (bmp != null) bmp.Dispose();
                        }
                        device_system.Set_Device("T0", false);
                    }
                }
                else
                {
                    current_num++;
                }
    
            }
            if (無警報_temp != 無警報 && 自動隱藏)
            {
                if(!無警報)
                {
                    VisibleDelegate visibleDelegate = new VisibleDelegate(隱藏);
                    Invoke(visibleDelegate, false);
                }
                無警報_temp = 無警報;
            }

          
        }

    }
}
