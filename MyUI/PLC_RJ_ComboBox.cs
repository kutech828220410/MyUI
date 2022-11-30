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
    [System.Drawing.ToolboxBitmap(typeof(ComboBox))]
    public class PLC_RJ_ComboBox : RJ_ComboBox
    {
        private LowerMachine PLC;
        private bool FLAG_DropDown = false;
        private bool _PLC_要讀取及寫入 = false;
        private bool PLC_要讀取及寫入
        {
            get
            {
                return (_寫入元件位置 != "" || _讀取元件位置 != "" && PLC != null);
            }
        }

        private bool Enable_buf = true;
        private object value;
        private object value_buf;

        #region 自訂屬性
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
        private string _數值更動旗標 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 數值更動旗標
        {
            get { return _數值更動旗標; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _數值更動旗標 = value;
                else _數值更動旗標 = "";
            }
        }
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
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z") divice_OK = true;
                }

                if (divice_OK) _寫入元件位置 = value;
                else _寫入元件位置 = "";
            }
        }

        private string _讀取元件位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 讀取元件位置
        {
            get { return _讀取元件位置; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z") divice_OK = true;
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

        public PLC_RJ_ComboBox()
        {
            this.OnDropDown += new System.EventHandler(this.PLC_ComboBox_DropDown);
            this.OnSelectedIndexChanged += new System.EventHandler(this.PLC_ComboBox_SelectedIndexChanged);
            this.OnDropDownClosed += new System.EventHandler(this.PLC_ComboBox_DropDownClosed);
        }

        #region Function
        public string Get_Text()
        {
            return this.Items[this.GetValue()].ToString();
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

                    this.寫入位置註解 = this.寫入位置註解;
                    this.讀取位置註解 = this.讀取位置註解;
                }
            }
        }
        public int GetValue()
        {
            int _value_temp = -1;
            this.GetValue(ref _value_temp);
            return _value_temp;
        }
        public void GetValue(ref int value)
        {
            object temp = new object();
            if (_讀取元件位置 != "" && _讀取元件位置 != null && PLC != null)
            {
                PLC.properties.Device.Get_Device(_讀取元件位置, out temp);
                value = (int)temp;
            }
            else
            {
                if (this.value is int)
                {
                    value = (int)this.value;
                }
            }
        }
        public void SetValue(int Value)
        {
            int i = 987654321;
            if (PLC_要讀取及寫入)
            {
                PLC.properties.Device.Set_Device(_寫入元件位置, Value);
                this.GetValue(ref i);
                if (i != Value)
                {
                    if (PLC != null && _數值更動旗標 != null && _數值更動旗標 != "")
                    {
                        if (LadderProperty.DEVICE.TestDevice(_數值更動旗標))
                        {
                            PLC.properties.Device.Set_Device(_數值更動旗標, true);
                        }
                    }
                }
            }
            else
            {
                this.value = Value;
            }
        }
        public void Run()
        {
            //value = new object();
            if (_寫入元件位置 != "" || _讀取元件位置 != "" && PLC != null)
            {
                if ((_寫入元件位置 == "" || _寫入元件位置 == null) && _讀取元件位置 != "") _寫入元件位置 = _讀取元件位置;
                if ((_讀取元件位置 == "" || _讀取元件位置 == null) && _寫入元件位置 != "") _讀取元件位置 = _寫入元件位置;
            }
            if (_讀取元件位置 != "" && _讀取元件位置 != null && PLC != null && !FLAG_DropDown)
            {
                PLC.properties.Device.Get_Device(_讀取元件位置, out value);

            }

            if (value is int && !FLAG_DropDown)
            {
                this.Invoke(new Action(delegate
                {
                    if (this.SelectedIndex != (int)value)
                    {
                        if ((int)value >= this.Items.Count)
                        {
                            this.SelectedIndex = this.Items.Count - 1;
                        }
                        else if ((int)value < 0)
                        {
                            this.SelectedIndex = 0;
                        }
                        else
                        {
                            this.SelectedIndex = (int)value;
                        }
                    }
                }));
            }

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

        public void Load_PLC_Device(PLC_Device pLC_Device)
        {
            this.讀取元件位置 = pLC_Device.GetAdress();
            this.寫入元件位置 = pLC_Device.GetAdress();
        }
        #endregion
        #region Event
        private void PLC_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PLC_要讀取及寫入)
            {
                PLC.properties.Device.Set_Device(_寫入元件位置, this.SelectedIndex);
                if (PLC != null && _數值更動旗標 != null && _數值更動旗標 != "")
                {
                    if (LadderProperty.DEVICE.TestDevice(_數值更動旗標))
                    {
                        PLC.properties.Device.Set_Device(_數值更動旗標, true);
                    }
                }
            }
            else
            {
                this.value = this.SelectedIndex;
            }
        }
        private void PLC_ComboBox_DropDown(object sender, EventArgs e)
        {
            this.FLAG_DropDown = true;
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
        private void PLC_ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            this.FLAG_DropDown = false;
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
        #endregion
    }
}
