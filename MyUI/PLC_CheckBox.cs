using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LadderConnection;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Media;

namespace MyUI
{
    [System.Drawing.ToolboxBitmap(typeof(CheckBox))]
    public partial class PLC_CheckBox : CheckBox
    {
        public LowerMachine PLC;
        private bool flag_init = false;

        public bool Bool
        {
            get
            {
                if (flag_init) return this.GetValue();
                else return false;

            }
            set
            {
                if (flag_init) this.SetValue(value);
            }
        }
        private bool PLC_Can_Be_Write
        {
            get
            {
                return (_寫入元件位置 != "" && PLC != null);
            }
        }
        private bool PLC_Can_Be_Read
        {
            get
            {
                return (_讀取元件位置 != "" && PLC != null);
            }
        }
        private bool value = false;
        private bool value_buf = false;
        private bool Enable_buf = true;
        private bool Enable_value = false;
        public PLC_CheckBox()
        {
            InitializeComponent();
            this.Text = 文字內容;
            this.Font = 文字字體;
            this.ForeColor = 文字顏色;
        }
        #region 隱藏屬性
          [Browsable(false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }
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
        public override System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
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
        #region 自訂屬性
        private bool _音效 = true;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 音效
        {
            get { return _音效; }
            set
            {
                _音效 = value;
            }
        }
        private string _文字內容 = "CheckBox";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 文字內容
        {
            get { return _文字內容; }
            set
            {
                _文字內容 = value;
                this.Text = _文字內容;
            }
        }
        private Font _文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Font 文字字體
        {
            get { return _文字字體; }
            set
            {
                _文字字體 = value;
                this.Font = _文字字體;
            }
        }
        private Color _文字顏色 = Color.Black;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Color 文字顏色
        {
            get { return _文字顏色; }
            set
            {
                _文字顏色 = value;
                this.ForeColor = _文字顏色;
            }
        }
        private bool _讀寫鎖住 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue(1)]
        public virtual bool 讀寫鎖住
        {
            get { return _讀寫鎖住; }
            set
            {
                _讀寫鎖住 = value;
            }
        }
        private string _致能讀取位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
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

        private string _寫入元件位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
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
        private string _讀取元件位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
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
        string _寫入位置註解 = "";
        [ReadOnly(false), Browsable(true), Category("註解"), Description(""), DefaultValue("")]
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
        string _讀取位置註解 = "";
        [ReadOnly(false), Browsable(true), Category("註解"), Description(""), DefaultValue("")]
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
        #endregion 
        #region Function
        public bool GetValue()
        {
            bool flag = false;
            this.GetValue(ref flag);
            return flag;
        }
        public void GetValue(ref bool value)
        {
            if (this.PLC_Can_Be_Read)
            {
                this.value = PLC.properties.Device.Get_DeviceFast_Ex(_讀取元件位置);
            }
            value = this.value;
        }
        public void SetValue(bool value)
        {
            this.value = value;
            if (this.PLC_Can_Be_Write)
            {
                PLC.properties.Device.Set_DeviceFast_Ex(_寫入元件位置, this.value);
            }
        }
        #endregion

        public bool Set_PLC(LowerMachine pLC)
        {
            if (pLC != null)
            {
                this.PLC = pLC;
                return true;
            }
            return false;
        }
        public void Load_PLC_Device(PLC_Device pLC_Device)
        {
            this.讀取元件位置 = pLC_Device.GetAdress();
            this.寫入元件位置 = pLC_Device.GetAdress();
        }
        public virtual void Run(LowerMachine pLC)
        {
            if (PLC == null)
            {
                if (Set_PLC(pLC))
                {
                    this.寫入位置註解 = this.寫入位置註解;
                    this.讀取位置註解 = this.讀取位置註解;
                    flag_init = true;
                    PLC.Add_UI_Method(Run);
                }
            }
        }
        public virtual void Run()
        {
            按鈕狀態計算();
        }

        private void 按鈕狀態計算()
        {
            if (_寫入元件位置 != "" || _讀取元件位置 != "" && PLC != null)
            {
                if ((_寫入元件位置 == "" || _寫入元件位置 == null) && _讀取元件位置 != "") _寫入元件位置 = _讀取元件位置;
                if ((_讀取元件位置 == "" || _讀取元件位置 == null) && _寫入元件位置 != "") _讀取元件位置 = _寫入元件位置;
            }
            if (PLC_Can_Be_Read)
            {
                if (LadderProperty.DEVICE.TestDevice(_讀取元件位置))
                {
                    this.value = PLC.properties.Device.Get_DeviceFast_Ex(_讀取元件位置);
                }
            }
            if (PLC != null && _致能讀取位置 != null && _致能讀取位置 != "")
            {
                if (LadderProperty.DEVICE.TestDevice(_致能讀取位置))
                {
                    this.Enable_value = PLC.properties.Device.Get_DeviceFast_Ex(_致能讀取位置);
                    if (this.Enable_buf != this.Enable_value)
                    {
                        this.Invoke(new Action(delegate
                        {
                            this.Enabled = this.Enable_value;
                            Enable_buf = this.Enable_value;
                        }
                       ));
       
                    }

                }
            }

            if (this.Checked != this.value)
            {
                this.Invoke(new Action(delegate
                {
                    this.Checked = this.value;
                }));
            }
        }

        private void PLC_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.value = this.Checked;
            if (PLC_Can_Be_Write)
            {
                PLC.properties.Device.Set_Device(_寫入元件位置, this.value);
            }     
        }
        private void PLC_CheckBox_Click(object sender, EventArgs e)
        {
            if (音效)
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
            }
        }
    }
}
