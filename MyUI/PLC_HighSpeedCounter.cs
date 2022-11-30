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
    public partial class PLC_HighSpeedCounter : UserControl
    {
        public LowerMachine PLC;
        private Form Active_Form;
        Basic.MyThread Thread_PLC_HighSpeedCounter = new Basic.MyThread("PLC_HighSpeedCounter");
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
        private string _輸入位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 輸入位置
        {
            get { return _輸入位置; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X") divice_OK = true;
                }

                if (divice_OK) _輸入位置 = value;
                else _輸入位置 = "";
            }
        }
        private string _寫入元件位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 寫入元件位置
        {
            get { return _寫入元件位置; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D") divice_OK = true;
                }

                if (divice_OK) _寫入元件位置 = value;
                else _寫入元件位置 = "";
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


                    if (divice_OK) _致能讀取位置 = value;
                    else _致能讀取位置 = "";
                }
            }
        }
        #endregion
        public PLC_HighSpeedCounter()
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
        public virtual void Run(Form form, LowerMachine pLC)
        {
            if (PLC == null)
            {
                if (Set_PLC(pLC))
                {
                    this.Active_Form = form;

                    Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);

                    Thread_PLC_HighSpeedCounter.SetSleepTime(0);
                    Thread_PLC_HighSpeedCounter.AutoRun(true);
                    Thread_PLC_HighSpeedCounter.Add_Method(Run);
                    Thread_PLC_HighSpeedCounter.Trigger();
                }
            }
        }
        object value = new object();
        object value1 = new object();
        object enable = new object();
        bool bool_enable = false;
        bool bool_toggle = false;
        public virtual void Run()
        {
            if (_致能讀取位置 != "" && PLC != null)
            {
                if (LadderProperty.DEVICE.TestDevice(_致能讀取位置))
                {
                    PLC.properties.Device.Get_Device(_致能讀取位置, out enable);
                    if (enable is bool)
                    {
                        bool_enable = (bool)enable;                       
                    }
                }
            }
            else if (_致能讀取位置 == "")
            {
                bool_enable = true;
            }


            if (bool_enable)
            {
                if (_輸入位置 != "" && PLC != null)
                {
                    if (LadderProperty.DEVICE.TestDevice(_輸入位置))
                    {
                        PLC.properties.Device.Get_Device(_輸入位置, out value);
                        if (value is bool)
                        {
                            if ((bool)value)
                            {                               
                                if(!bool_toggle)
                                {
                                    if (寫入元件位置 != "" && 寫入元件位置 != null && PLC != null)
                                    {
                                        PLC.properties.Device.Get_Device(_寫入元件位置, out value1);
                                        PLC.properties.Device.Set_Device(寫入元件位置, (int)value1 + 1);

                                    }
                                    bool_toggle = true;
                                }                         
                            }
                            else
                            {
                                bool_toggle = false;
                            }
                        }
                    }
                }

            }
        
        }
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            Thread_PLC_HighSpeedCounter.Stop();
        }

    }

}