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
    public partial class PLC_RJ_GroupBox : RJ_GroupBox
    {
        private LowerMachine PLC;
        private bool Enable_buf = true;
        private object value;
        #region 自訂屬性
        private string _致能讀取位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 致能讀取位置
        {
            get { return _致能讀取位置; }
            set
            {
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
        #endregion

        public PLC_RJ_GroupBox()
        {
            InitializeComponent();
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
        public bool Set_PLC(LowerMachine pLC)
        {
            if (pLC != null)
            {
                this.PLC = pLC;
                return true;
            }
            return false;
        }
        public void Run(LowerMachine pLC, Basic.MyThread myThread)
        {
            if (PLC == null)
            {
                if (Set_PLC(pLC))
                {
                    myThread.Add_Method(Run);
                }
            }
        }
        public void Run()
        {
            if (PLC != null)
            {
                if (_致能讀取位置 != string.Empty)
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

        }
    }
}
