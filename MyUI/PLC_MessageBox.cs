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

namespace MyUI
{
    [System.Drawing.ToolboxBitmap(typeof(PLC_UI_Init))]
    public partial class PLC_MessageBox : UserControl
    {
        public LowerMachine PLC;
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
        bool _顯示 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 顯示
        {
            get
            {
                return _顯示;
            }
            set
            {
                _顯示 = value;
            }
        }
        string _標題 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 標題
        {
            get
            {
                return _標題;
            }
            set
            {
                _標題 = value;
            }
        }
        string _內容 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 內容
        {
            get
            {
                return _內容;
            }
            set
            {
                _內容 = value;
            }
        }
        MessageBoxIcon _Icon = MessageBoxIcon.Warning;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public MessageBoxIcon Icon
        {
            get
            {
                return _Icon;
            }
            set
            {
                _Icon = value;
            }
        }
        MessageBoxButtons _Buttons = MessageBoxButtons.OKCancel;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public MessageBoxButtons Buttons
        {
            get
            {
                return _Buttons;
            }
            set
            {
                _Buttons = value;
            }
        }
        private string _旗標位置_Yes = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 旗標位置_Yes
        {
            get { return _旗標位置_Yes; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "M" || temp == "S") divice_OK = true;
                }

                if (divice_OK) _旗標位置_Yes = value;
                else _旗標位置_Yes = "";
            }
        }
        private string _旗標位置_No = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 旗標位置_No
        {
            get { return _旗標位置_No; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "M" || temp == "S") divice_OK = true;
                }

                if (divice_OK) _旗標位置_No = value;
                else _旗標位置_No = "";
            }
        }
        private string _旗標位置_OK = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 旗標位置_OK
        {
            get { return _旗標位置_OK; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "M" || temp == "S") divice_OK = true;
                }

                if (divice_OK) _旗標位置_OK = value;
                else _旗標位置_OK = "";
            }
        }
        private string _旗標位置_Abort = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 旗標位置_Abort
        {
            get { return _旗標位置_Abort; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "M" || temp == "S") divice_OK = true;
                }

                if (divice_OK) _旗標位置_Abort = value;
                else _旗標位置_Abort = "";
            }
        }
        private string _旗標位置_Ignore = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 旗標位置_Ignore
        {
            get { return _旗標位置_Ignore; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "M" || temp == "S") divice_OK = true;
                }

                if (divice_OK) _旗標位置_Ignore = value;
                else _旗標位置_Ignore = "";
            }
        }
        private string _旗標位置_Retry = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 旗標位置_Retry
        {
            get { return _旗標位置_Retry; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "M" || temp == "S") divice_OK = true;
                }

                if (divice_OK) _旗標位置_Retry = value;
                else _旗標位置_Retry = "";
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
        #endregion 
        public PLC_MessageBox()
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
        public virtual void Run(LowerMachine pLC)
        {
            if (PLC == null)
            {
                if (Set_PLC(pLC))
                {
                    PLC.Add_UI_Method(Run);
                }
            }
        }
        object value = new object();
        public virtual void Run()
        {
            if ( _讀取元件位置 != "" && PLC != null)
            {
                if (LadderProperty.DEVICE.TestDevice(_讀取元件位置))
                {
                    PLC.properties.Device.Get_Device(_讀取元件位置, out value);
                    if (value is bool)
                    {
                        if((bool)value)
                        {
                            PLC.properties.Device.Set_Device(_讀取元件位置, false);
                            DialogResult Result = MessageBox.Show(內容, 標題, Buttons, Icon, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            if (Result == DialogResult.Yes)
                            {
                                if (_旗標位置_Yes != null || _旗標位置_Yes != "") PLC.properties.Device.Set_Device(_旗標位置_Yes, true);
                            }
                            if (Result == DialogResult.No)
                            {
                                if (_旗標位置_No != null || _旗標位置_No != "") PLC.properties.Device.Set_Device(_旗標位置_No, true);
                            }
                            if (Result == DialogResult.Abort)
                            {
                                if (_旗標位置_Abort != null || _旗標位置_Abort != "") PLC.properties.Device.Set_Device(_旗標位置_Abort, true);
                            }
                            if (Result == DialogResult.Ignore)
                            {
                                if (_旗標位置_Ignore != null || _旗標位置_Ignore != "") PLC.properties.Device.Set_Device(_旗標位置_Ignore, true);
                            }
                            if (Result == DialogResult.OK)
                            {
                                if (_旗標位置_OK != null || _旗標位置_OK != "") PLC.properties.Device.Set_Device(_旗標位置_OK, true);
                            }
                            if (Result == DialogResult.Retry)
                            {
                                if (_旗標位置_Retry != null || _旗標位置_Retry != "") PLC.properties.Device.Set_Device(_旗標位置_Retry, true);
                            }
                        }
                        else
                        {

                        }
                        PLC.properties.Device.Set_Device(_讀取元件位置, false);
                    }
                }
            }
        }
    }
}
