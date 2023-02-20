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
namespace MyUI
{
      [System.Drawing.ToolboxBitmap(typeof(Button))]
    public partial class PLC_LoadDeviceButtom : PLC_Button
    {
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private Object LoadObj = new object();
        private LowerMachine.SaveDeviceFile _SaveDeviceFile = new LowerMachine.SaveDeviceFile();
        public string FileName
        {
            get
            {
                return this.openFileDialog.FileName;
            }
        }
        #region 隱藏屬性
        [ReadOnly(false), Browsable(false), Category("自訂屬性"), Description(""), DefaultValue("")]
        public override StatusEnum 按鈕型態
        {
            get
            {
                return base.按鈕型態;
            }
            set
            {
                base.按鈕型態 = value;
            }
        }
        [ReadOnly(false), Browsable(false), Category("自訂屬性"), Description(""), DefaultValue("")]
        public override string 寫入元件位置
        {
            get
            {
                return base.寫入元件位置;
            }
            set
            {
                base.寫入元件位置 = value;
            }
        }
        [ReadOnly(false), Browsable(false), Category("自訂屬性"), Description(""), DefaultValue("")]
        public override string 讀取元件位置
        {
            get
            {
                return base.讀取元件位置;
            }
            set
            {
                base.讀取元件位置 = string.Empty;
            }
        }
        [ReadOnly(false), Browsable(false), Category("自訂屬性"), Description(""), DefaultValue("")]
        public override bool 讀寫鎖住
        {
            get
            {
                return base.讀寫鎖住;
            }
            set
            {
                base.讀寫鎖住 = value;
            }
        }
        #endregion
        #region 顯示屬性
        public enum 讀取範圍Enum : int
        {
            F元件, R元件, D元件, M元件, S元件, 所有元件
        }
        讀取範圍Enum _讀取範圍 = new 讀取範圍Enum();
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public 讀取範圍Enum 讀取範圍
        {
            get
            {
                return _讀取範圍;
            }
            set
            {
                _讀取範圍 = value;
            }
        }
        private string _旗標位置_開始讀取 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 旗標位置_讀取完成
        {
            get { return _旗標位置_開始讀取; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "M" || temp == "S") divice_OK = true;
                }

                if (divice_OK) _旗標位置_開始讀取 = value;
                else _旗標位置_開始讀取 = "";
            }
        }
        #endregion
        public PLC_LoadDeviceButtom()
        {
            InitializeComponent();

        }
        public override void Run(LowerMachine pLC)
        {
            base.Run(pLC);
        }

        // bool but_press_buf = false;
        delegate void strHandles(string str);
        CaptureDelegate captureDelegate;
        object value;
        public override void Run()
        {
            captureDelegate = new CaptureDelegate(LabelCapture);
            Invoke(captureDelegate);
            Init();

            base.EnableCheck();

            if (_旗標位置_開始讀取 != null || _旗標位置_開始讀取 != "")
            {
                if (PLC.properties.Device.Get_Device(_旗標位置_開始讀取, out value))
                {
                    if (value is bool)
                    {
                        if ((bool)value)
                        {
                            if (openFileDialog != null)
                            {
                                BeginInvoke(showDialogDelegate);
                            }
                        }
                    }
                }
            }

            if (but_press != but_press_buf)
            {
                if (openFileDialog != null)
                {
                    BeginInvoke(showDialogDelegate);
                }
                but_press_buf = but_press;


            }
        }
        delegate void ShowDialogDelegate();
        ShowDialogDelegate showDialogDelegate;
        bool flag_init = false;
        bool IsDialogShow = false;
        private void Init()
        {
            if (!flag_init)
            {
                this.openFileDialog = new OpenFileDialog();
                this.openFileDialog.DefaultExt = "pval";
                this.openFileDialog.Filter = "pval File (*pval)|*pval;";
                showDialogDelegate = new ShowDialogDelegate(ShowDialog);
                flag_init = true;
            }
        }
        private void ShowDialog()
        {
            if (!IsDialogShow)
            {
                IsDialogShow = true;
                base.sub_按鈕狀態設為ON();
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    Basic.FileIO.LoadProperties(ref LoadObj, openFileDialog.FileName);
                    _SaveDeviceFile = (LowerMachine.SaveDeviceFile)LoadObj;
                    if (_讀取範圍 == 讀取範圍Enum.所有元件)
                    {
                        base.PLC.properties.Device.SetAllValue(_SaveDeviceFile.allValue);
                    }
                    else if (_讀取範圍 == 讀取範圍Enum.F元件)
                    {
                        base.PLC.properties.Device.Set_F_Device_Value(_SaveDeviceFile.allValue);
                    }
                    else if (_讀取範圍 == 讀取範圍Enum.R元件)
                    {
                        base.PLC.properties.Device.Set_R_Device_Value(_SaveDeviceFile.allValue);
                    }
                    else if (_讀取範圍 == 讀取範圍Enum.D元件)
                    {
                        base.PLC.properties.Device.Set_D_Device_Value(_SaveDeviceFile.allValue);
                    }
                    else if (_讀取範圍 == 讀取範圍Enum.S元件)
                    {
                        base.PLC.properties.Device.Set_S_Device_Value(_SaveDeviceFile.allValue);
                    }
                    else if (_讀取範圍 == 讀取範圍Enum.M元件)
                    {
                        base.PLC.properties.Device.Set_M_Device_Value(_SaveDeviceFile.allValue);
                    }
                }
                if (_旗標位置_開始讀取 != null || _旗標位置_開始讀取 != "") PLC.properties.Device.Set_Device(_旗標位置_開始讀取, false);
                IsDialogShow = false;
                base.sub_按鈕狀態設為OFF();
            }
        }
    }
}
