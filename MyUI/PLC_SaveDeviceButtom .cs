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
    public partial class PLC_SaveDeviceButtom : PLC_Button
    {
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private LowerMachine.SaveDeviceFile _SaveDeviceFile = new LowerMachine.SaveDeviceFile();
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
        public enum 存檔範圍Enum : int
        {
          F元件,所有元件
        }
        存檔範圍Enum _存檔範圍 = new 存檔範圍Enum();
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public 存檔範圍Enum 存檔範圍
        {
            get
            {
                return _存檔範圍;
            }
            set
            {
                _存檔範圍 = value;
            }
        }
        private string _旗標位置_開始存檔 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 旗標位置_開始存檔
        {
            get { return _旗標位置_開始存檔; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "M" || temp == "S") divice_OK = true;
                }

                if (divice_OK) _旗標位置_開始存檔 = value;
                else _旗標位置_開始存檔 = "";
            }
        }
        #endregion
        public PLC_SaveDeviceButtom()
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

            base.EnableCheck();

            Init();     
            if (_旗標位置_開始存檔 != null || _旗標位置_開始存檔 != "")
            {
                if (PLC.properties.Device.Get_Device(_旗標位置_開始存檔, out value))
                {
                    if (value is bool)
                    {
                        if ((bool)value)
                        {
                            if (saveFileDialog != null)
                            {
                                BeginInvoke(showDialogDelegate);
                            }
                        }
                    }
                }
            }

            if (but_press != but_press_buf)
            {
                if (saveFileDialog != null)
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
            if(!flag_init)
            {
                this.saveFileDialog = new SaveFileDialog();
                this.saveFileDialog.DefaultExt = "pval";
                this.saveFileDialog.Filter = "pval File (*pval)|*pval;";
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
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {

                    if (_存檔範圍 == 存檔範圍Enum.所有元件)
                    {
                        _SaveDeviceFile.allValue = base.PLC.properties.Device.GetAllValue();
                    }
                    else if (_存檔範圍 == 存檔範圍Enum.F元件)
                    {
                        _SaveDeviceFile.allValue = base.PLC.properties.Device.Get_F_Device_Value();
                    }
                    Basic.FileIO.SaveProperties(_SaveDeviceFile, saveFileDialog.FileName);
                   
                }
                if (_旗標位置_開始存檔 != null || _旗標位置_開始存檔 != "") PLC.properties.Device.Set_Device(_旗標位置_開始存檔, false);
                IsDialogShow = false;
                base.sub_按鈕狀態設為OFF();
            }    
        }
 
    }

}
