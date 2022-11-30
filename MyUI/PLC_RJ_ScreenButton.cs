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
using System.Media;
using System.Diagnostics;
using FontAwesome.Sharp;
namespace MyUI
{
    public partial class PLC_RJ_ScreenButton : UserControl
    {
        public LowerMachine PLC;
        public bool Is_ButPress
        {
            get
            {
                return this.but_press;
            }
        }
        public bool _but_press = false;
        public bool but_press
        {
            get
            {
                return this._but_press;
            }
            set
            {
                this._but_press = value;

            }
        }
        public bool but_press_buf = false;
        private bool flag_state = false;
        private bool IN = false;
        private bool M0 = false;
        private bool M1 = false;
        private bool M2 = false;
        private bool M3 = false;
        private bool M4 = false;
        private bool OUT = false;
        private bool _PLC_要讀取 = false;
        private bool PLC_要讀取
        {
            get
            {
                return (_讀取元件位置 != "");
            }
        }
        private bool _PLC_要寫入 = false;
        private bool PLC_要寫入
        {
            get
            {
                return (_寫入元件位置 != "");
            }
        }
        private object value = new object();
        private bool flag_init = false;
        private bool Value_Buf = false;
        private bool IsRunOnce = false;
        private bool _FLAG_寫入 = false;
        private bool FLAG_寫入
        {
            get
            {
                return _FLAG_寫入;
            }
            set
            {
                _FLAG_寫入 = value;
            }
        }
        private bool _FLAG_讀取 = false;
        private bool _FLAG_讀取_buf = false;
        private bool FLAG_讀取
        {
            get
            {
                //if (this._FLAG_讀取)
                //{
                //    if(!this.讀取位元反向)
                //    {
                //        this.sub_按鈕狀態設為ON();
                //    }
                //    else
                //    {
                //        this.sub_按鈕狀態設為OFF();
                //    }
                //}
                //if (!this._FLAG_讀取)
                //{
                //    if (!this.讀取位元反向)
                //    {
                //        this.sub_按鈕狀態設為OFF();
                //    }
                //    else
                //    {
                //        this.sub_按鈕狀態設為ON();                        
                //    }
                //}

                return _FLAG_讀取;
            }
            set
            {
                this._FLAG_讀取 = value;
                if (this._FLAG_讀取)
                {
                    if (!this.讀取位元反向)
                    {
                        this.sub_按鈕狀態設為ON();
                    }
                    else
                    {
                        this.sub_按鈕狀態設為OFF();
                    }
                }
                if (!this._FLAG_讀取)
                {
                    if (!this.讀取位元反向)
                    {
                        this.sub_按鈕狀態設為OFF();
                    }
                    else
                    {
                        this.sub_按鈕狀態設為ON();
                    }
                }
            }
        }
        public bool FLAG_buf = false;
        private bool Enable_buf = true;
        private bool Visible_buf = true;
        private bool _音效 = true;
        private delegate void strHandles(string str);


        public enum 換頁選擇方式Enum : int
        {
            名稱 = 0, 索引, 退出程式
        }
        public enum StatusEnum : int
        {
            保持型, 交替型
        }
        private PLC_ScreenPage pLC_ScreenPage;
        private Basic.MyThread MyThread_ExitApp;
        private StateEnum _顯示方式 = new StateEnum();
        private 換頁選擇方式Enum _換頁選擇方式 = new 換頁選擇方式Enum();
        private string _頁面名稱 = "";
        private int _頁面編號 = 0;
        private string _控制位址 = "D0";
        private string _寫入位置註解 = "";
        private string _讀取位置註解 = "";
        private string _讀取元件位置 = "";
        private string _寫入元件位置 = "";
        private string _致能讀取位置 = "";
        private string _顯示讀取位置 = "";
        private bool _讀取位元反向 = false;


        private Color offBackColor = Color.SkyBlue;
        private Color onBackColor = Color.LightBlue;
        private Color offForeColor = Color.White;
        private Color onForeColor = Color.White;
        private Color offIconColor = Color.Black;
        private Color onIconColor = Color.Black;
        private Font offFont = new Font("新細明體", 12);
        private Font onFont = new Font("新細明體", 12);
        private string offText = "iConText";
        private string onText = "iConText";
        private bool showIcon = true;
        private bool _顯示狀態 = false;

        [Category("RJ Code Appeareance")]
        public FontAwesome.Sharp.IconChar IconChar
        {
            get
            {
                return this.iconButton1.IconChar;
            }
            set
            {
                this.iconButton1.IconChar = value;
            }
        }
        [Category("RJ Code Appeareance")]
        public int IconSize
        {
            get
            {
                return this.iconButton1.IconSize;
            }
            set
            {
                this.iconButton1.IconSize = value;
            }
        }
        [Category("RJ Code Appeareance")]
        public new Font Font
        {
            get
            {
                return this.iconButton1.Font;
            }
            set
            {
                this.iconButton1.Font = value;
            }
        }
        [Category("RJ Code Appeareance")]
        public Color OffBackColor
        {
            get
            {
                return offBackColor;
            }
            set
            {
                offBackColor = value;
                this.iconButton1.BackColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Appeareance")]
        public Color OnBackColor
        {
            get
            {
                return onBackColor;
            }
            set
            {
                onBackColor = value;
                this.iconButton1.BackColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Appeareance")]
        public Color OffForeColor
        {
            get
            {
                return offForeColor;
            }
            set
            {
                offForeColor = value;
                this.iconButton1.ForeColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Appeareance")]
        public Color OnForeColor
        {
            get
            {
                return onForeColor;
            }
            set
            {
                onForeColor = value;
                this.iconButton1.ForeColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Appeareance")]
        public Color OffIconColor
        {
            get
            {
                return offIconColor;
            }
            set
            {
                offIconColor = value;
                this.iconButton1.IconColor = value;
            }
        }
        [Category("RJ Code Appeareance")]
        public Color OnIconColor
        {
            get
            {
                return onIconColor;
            }
            set
            {
                onIconColor = value;
                this.iconButton1.IconColor = value;
            }
        }
        [Category("RJ Code Appeareance")]
        public Font OffFont
        {
            get
            {
                return offFont;
            }
            set
            {
                offFont = value;
                this.iconButton1.Font = value;
            }
        }
        [Category("RJ Code Appeareance")]
        public Font OnFont
        {
            get
            {
                return onFont;
            }
            set
            {
                onFont = value;
                this.iconButton1.Font = value;
            }
        }
        [Category("RJ Code Appeareance")]
        public string OffText
        {
            get
            {
                return offText;
            }
            set
            {
                offText = value;
                this.iconButton1.Text = value;
            }
        }
        [Category("RJ Code Appeareance")]
        public string OnText
        {
            get
            {
                return onText;
            }
            set
            {
                onText = value;
                this.iconButton1.Text = value;
            }
        }
        [Category("RJ Code Appeareance")]
        public bool ShowIcon
        {
            get
            {
                return showIcon;
            }
            set
            {
                showIcon = value;
                if(!showIcon)
                {
                    this.iconButton1.TextImageRelation = TextImageRelation.Overlay;
                    this.iconButton1.TextAlign = ContentAlignment.MiddleCenter;
                    this.iconButton1.IconChar = IconChar.None;
                }
                else
                {

                }
            }
        }

        [Category("RJ Data Advance")]
        public 換頁選擇方式Enum 換頁選擇方式
        {
            get
            {
                return _換頁選擇方式;
            }
            set
            {
                _換頁選擇方式 = value;
            }
        }
        [Category("RJ Data Advance")]
        public string 頁面名稱
        {
            get
            {
                return _頁面名稱;
            }
            set
            {
                _頁面名稱 = value;
            }
        }
        [Category("RJ Data Advance")]
        public int 頁面編號
        {
            get
            {
                return _頁面編號;
            }
            set
            {
                _頁面編號 = value;
            }
        }
        [Category("RJ Data Advance")]
        public string 控制位址
        {
            get { return _控制位址; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "D" || temp == "R") divice_OK = true;
                }

                if (divice_OK) _控制位址 = value;
                else _控制位址 = "";
            }
        }
        public enum WordLengthEnum : int
        {
            單字元, 雙字元
        }
        [Category("RJ Data Advance")]
        public WordLengthEnum 字元長度 { get; set; }
        public enum StateEnum : int
        {
            正常顯示 = 0, 顯示為ON, 顯示為OFF
        }
        [Category("RJ Data Advance")]
        public StateEnum 顯示方式
        {
            get
            {
                return _顯示方式;
            }
            set
            {
                if (value == StateEnum.顯示為ON) 顯示狀態 = true;
                else 顯示狀態 = false;
                _顯示方式 = value;
            }
        }
        [Category("RJ Data Advance")]
        public bool 顯示狀態
        {
            get
            {
                if (_顯示狀態) sub_按鈕狀態設為ON();
                else sub_按鈕狀態設為OFF();
                return _顯示狀態;
            }
            set
            {
                _顯示狀態 = value;
                if (value) sub_按鈕狀態設為ON();
                else sub_按鈕狀態設為OFF();
            }
        }
        [Category("RJ Data Advance")]
        public string 寫入位置註解
        {
            get
            {
                return _寫入位置註解;
            }
            set
            {
                _寫入位置註解 = value;
                if (this.PLC != null && this.寫入元件位置 != "" && value != "")
                {
                    PLC.properties.Device.Set_Device(this.寫入元件位置, "*" + value);
                }
            }
        }
        [Category("RJ Data Advance")]
        public string 讀取位置註解
        {
            get
            {
                return _讀取位置註解;
            }
            set
            {
                _讀取位置註解 = value;
                if (this.PLC != null && this.讀取元件位置 != "" && value != "")
                {
                    PLC.properties.Device.Set_Device(this.讀取元件位置, "*" + value);
                }
            }
        }
        [Category("RJ Data Advance")]
        public virtual string 致能讀取位置
        {
            get { return _致能讀取位置; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _致能讀取位置 = value;
                else _致能讀取位置 = "";
            }
        }
        [Category("RJ Data Advance")]
        public virtual string 顯示讀取位置
        {
            get { return _顯示讀取位置; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _顯示讀取位置 = value;
                else _顯示讀取位置 = "";
            }
        }
        [Category("RJ Data Advance")]
        public virtual string 寫入元件位置
        {
            get { return _寫入元件位置; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _寫入元件位置 = value;
                else _寫入元件位置 = "";
            }
        }
        [Category("RJ Data Advance")]
        public virtual string 讀取元件位置
        {
            get { return _讀取元件位置; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _讀取元件位置 = value;
                else _讀取元件位置 = "";
            }
        }
        [Category("RJ Data Advance")]
        public bool 讀取位元反向
        {
            get
            {
                return _讀取位元反向;
            }
            set
            {
                _讀取位元反向 = value;
            }
        }
        [Category("RJ Data Advance")]
        public virtual StatusEnum 按鈕型態 { get; set; }

        [Category("RJ Data Advance")]
        public bool 音效
        {
            get { return _音效; }
            set
            {
                _音效 = value;
            }
        }


        #region Function
        public void sub_按鈕狀態設為ON()
        {
            if (this.IsHandleCreated == true)
            {
                this.Invoke(new Action(delegate
                {
                    this.iconButton1.BackColor = onBackColor;
                    this.iconButton1.ForeColor = onForeColor;            
                    this.iconButton1.IconColor = onIconColor;           
                    if (showIcon)
                    {
                        this.iconButton1.TextAlign = ContentAlignment.MiddleCenter;
                        this.iconButton1.ImageAlign = ContentAlignment.MiddleRight;
                        this.iconButton1.Font = onFont;
                        this.iconButton1.Text = "  " + onText;
                        this.iconButton1.TextImageRelation = TextImageRelation.TextBeforeImage;
                    }
                        
                }));
            }
            else
            {
                this.iconButton1.BackColor = onBackColor;
                this.iconButton1.ForeColor = onForeColor;
                this.iconButton1.IconColor = onIconColor;
                if (showIcon)
                {
                    this.iconButton1.Font = onFont;
                    this.iconButton1.TextAlign = ContentAlignment.MiddleCenter;
                    this.iconButton1.ImageAlign = ContentAlignment.MiddleRight;
                    this.iconButton1.Text = "  " + onText;
                    this.iconButton1.TextImageRelation = TextImageRelation.TextBeforeImage;
                }
                  
            }

        }
        public void sub_按鈕狀態設為OFF()
        {
            if (this.IsHandleCreated == true)
            {
                this.Invoke(new Action(delegate
                {
                    this.iconButton1.BackColor = offBackColor;
                    this.iconButton1.ForeColor = offForeColor;          
                    this.iconButton1.IconColor = offIconColor;
                    this.iconButton1.Font = offFont;
                    if (showIcon)
                    {
                        this.iconButton1.TextAlign = ContentAlignment.MiddleLeft;
                        this.iconButton1.ImageAlign = ContentAlignment.MiddleLeft;
                        this.iconButton1.Text = "  " + offText;
                        this.iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
                    }
                }));
            }
            else
            {
                this.iconButton1.BackColor = offBackColor;
                this.iconButton1.ForeColor = offForeColor;
                this.iconButton1.IconColor = offIconColor;
                if(showIcon)
                {
                    this.iconButton1.Font = offFont;
                    this.iconButton1.Text = "  " + offText;           
                    this.iconButton1.TextAlign = ContentAlignment.MiddleLeft;
                    this.iconButton1.ImageAlign = ContentAlignment.MiddleLeft;
                    this.iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
                }
           
            }

        }
        #endregion
        public PLC_RJ_ScreenButton()
        {
            InitializeComponent();
            this.iconButton1.MouseDown += PLC_RJ_ScreenButton_MouseDown;
            this.iconButton1.MouseUp += PLC_RJ_ScreenButton_MouseUp;
            this.iconButton1.MouseLeave += PLC_RJ_ScreenButton_MouseLeave;
        }
        private void PLC_RJ_ScreenButton_Load(object sender, EventArgs e)
        {

        }
        public void Run(LowerMachine pLC)
        {
            if (PLC == null)
            {
                if (Set_PLC(pLC))
                {
                    if (顯示方式 == StateEnum.顯示為ON) this.sub_按鈕狀態設為ON();
                    else if (顯示方式 == StateEnum.顯示為OFF) this.sub_按鈕狀態設為OFF();
                    PLC.Add_UI_Method(Run);
                    this.寫入位置註解 = this.寫入位置註解;
                    this.讀取位置註解 = this.讀取位置註解;
                }
            }
         
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
        public void EnableCheck()
        {
            if (PLC != null && _致能讀取位置 != null && _致能讀取位置 != "")
            {
                if (LadderProperty.DEVICE.TestDevice(_致能讀取位置))
                {
                    PLC.properties.Device.Get_Device(_致能讀取位置, out value);
                    if (value is bool)
                    {
                        if (Enable_buf != (bool)value)
                        {
                            this.Invoke(new Action(delegate
                            {
                                this.Enabled = (bool)value;
                            }));
                            Enable_buf = (bool)value;
                        }
                    }
                }
            }
        }
        public void VisibleCheck()
        {
            if (PLC != null && _顯示讀取位置 != null && _顯示讀取位置 != "")
            {
                if (LadderProperty.DEVICE.TestDevice(_顯示讀取位置))
                {
                    PLC.properties.Device.Get_Device(_顯示讀取位置, out value);
                    if (value is bool)
                    {
                        if (Visible_buf != (bool)value)
                        {
                            this.Invoke(new Action(delegate
                            {
                                this.Visible = (bool)value;
                            }));
                            Visible_buf = (bool)value;
                        }
                    }
                }
            }
        }
        public void Set_PLC_ScreenPage(PLC_ScreenPage pLC_ScreenPage)
        {
            this.pLC_ScreenPage = pLC_ScreenPage;
        }
        private void ExitApp()
        {
            if (this.InvokeRequired) this.Invoke(new Action(delegate { this.FindForm().Close(); }));
            //  Process.GetCurrentProcess().CloseMainWindow();
        }

        public virtual void Run()
        {           
            按鈕狀態計算();
        }
        private void Init()
        {
            if (!flag_init)
            {
                if (PLC != null)
                {
                    if ((_讀取元件位置 == "" || _讀取元件位置 == null) && _寫入元件位置 != "")
                    {
                        _讀取元件位置 = _寫入元件位置;
                    }
                }
                flag_init = true;
            }
        }
        private void 按鈕狀態計算()
        {
            Init();
            this.EnableCheck();
            this.VisibleCheck();
            if (pLC_ScreenPage != null)
            {
                if (this.pLC_ScreenPage.PageText == this._頁面名稱)
                {
                    if (!this.flag_state)
                    {
                        this.sub_按鈕狀態設為ON();
                    }
                    this.flag_state = true;
                }
                else
                {
                    if (this.flag_state)
                    {
                        this.sub_按鈕狀態設為OFF();
                    }
                    this.flag_state = false;
                }
            }

            if (but_press != but_press_buf)
            {
                if (but_press && 換頁選擇方式 == 換頁選擇方式Enum.索引)
                {
                    if (_控制位址 != "" && _控制位址 != null && PLC != null)
                    {
                        int value = _頁面編號;
                        if (字元長度 == WordLengthEnum.單字元)
                        {
                            PLC.properties.Device.Set_Device(_控制位址, value);
                        }
                        else if (字元長度 == WordLengthEnum.雙字元)
                        {
                            PLC.properties.Device.Set_DoubleWord(_控制位址, Convert.ToInt64(value));
                        }
                    }
                    if (顯示方式 == StateEnum.正常顯示) this.sub_按鈕狀態設為ON();
                }
                if (but_press && 換頁選擇方式 == 換頁選擇方式Enum.名稱)
                {
                    if (pLC_ScreenPage != null)
                    {
                        strHandles _strHandles = new strHandles(pLC_ScreenPage.SelecteTabText);
                        Invoke(_strHandles, _頁面名稱);
                    }
                }
                else
                {
                    if (顯示方式 == StateEnum.正常顯示) this.sub_按鈕狀態設為OFF();
                }
                if (but_press && 換頁選擇方式 == 換頁選擇方式Enum.退出程式)
                {
                    DialogResult Result = MessageBox.Show("是否退出?", "Warring", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (Result == DialogResult.Yes)
                    {
                        MyThread_ExitApp = new Basic.MyThread();
                        MyThread_ExitApp.Add_Method(ExitApp);
                        MyThread_ExitApp.AutoRun(true);
                        MyThread_ExitApp.IsBackGround = true;
                        MyThread_ExitApp.Trigger();
                    }
                    else if (Result == DialogResult.No)
                    {

                    }
                    else if (Result == DialogResult.Cancel)
                    {

                    }
                }

                but_press_buf = but_press;
            }
        }

        #region Event
        private void PLC_RJ_ScreenButton_MouseDown(object sender, MouseEventArgs e)
        {
            if(this.PLC == null)
            {
                this.pLC_ScreenPage.SelecteTabText(this._頁面名稱);
            }
            if (e.Button == MouseButtons.Left)
            {
                if (音效)
                {
                    this.BeginInvoke(new Action(delegate
                    {
                        System.Media.SoundPlayer sp = null;
                        try
                        {
                            sp = new System.Media.SoundPlayer();
                            sp.Stop();
                            sp.Stream = Resource1.BEEP;
                            sp.Play();
                        }
                        finally
                        {
                            if (sp != null) sp.Dispose();
                        }
                    }));

                }
                but_press = true;
                IsRunOnce = false;
            }
        }
        private void PLC_RJ_ScreenButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsRunOnce) but_press = false;
        }
        private void PLC_RJ_ScreenButton_MouseLeave(object sender, EventArgs e)
        {
            but_press = false;
        }
        #endregion


    }
}
