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
using MyUI;
using Basic;

namespace MeasureSystemUI
{
    public partial class H_ImageCopier : UserControl
    {
        private AxOvkImage.AxImageCopier AxImageCopier;
        private bool flag_Init = false;
        private bool flag_Thread_Init = false;
        private Form Activeform;
        private LowerMachine PLC;
        PLC_UI_Init PLC_UI_Init;
        private List<H_Thread> List_H_Thread = new List<H_Thread>();

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
        #region 參數設定
        private string _UIName = "UIName";
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public string UIName
        {
            get
            {
                return _UIName;
            }
            set
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(delegate { this.label_UIName.Text = value; }));
                }
                _UIName = value;
            }
        }

        private string _關聯ThreadName = "";
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public string 關聯ThreadName
        {
            get
            {
                return _關聯ThreadName;
            }
            set
            {

                _關聯ThreadName = value;
            }
        }
        #endregion
        #region 數值位置
        private PLC_Device PLC_00_VegaHandle;
        private string __00_VegaHandle = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _00_VegaHandle
        {
            get { return __00_VegaHandle; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __00_VegaHandle = value;
                else __00_VegaHandle = "";
            }
        }
        private PLC_Device PLC_01_SrcImageHandle;
        private string __01_SrcImageHandle = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _01_SrcImageHandle
        {
            get { return __01_SrcImageHandle; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __01_SrcImageHandle = value;
                else __01_SrcImageHandle = "";
            }
        }
        private PLC_Device PLC_02_DstImageHandle;
        private string __02_DstImageHandle = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _02_DstImageHandle
        {
            get { return __02_DstImageHandle; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __02_DstImageHandle = value;
                else __02_DstImageHandle = "";
            }
        }
        #endregion
        #region 旗標位置
        private PLC_Device PLC_00_Copy;
        private string __00_Copy = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _00_Copy
        {
            get { return __00_Copy; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __00_Copy = value;
                else __00_Copy = "";
            }
        }
        #endregion
        public void Run(Form Activeform, LowerMachine pLC, PLC_UI_Init PLC_UI_Init)
        {
            this.Activeform = Activeform;
            this.PLC = pLC;
            this.PLC_UI_Init = PLC_UI_Init;

            #region 數值位置初始化
            if (this._00_VegaHandle != string.Empty)
            {
                this.PLC_00_VegaHandle = new PLC_Device(this._00_VegaHandle);
                plC_NumBox_VagaHandle.寫入元件位置 = this._00_VegaHandle;
                plC_NumBox_VagaHandle.讀取元件位置 = this._00_VegaHandle;
                plC_NumBox_VagaHandle.Run(this.PLC);
                this.PLC_00_VegaHandle.DoubleValue = 0;
            }
            if (this._01_SrcImageHandle != string.Empty)
            {
                this.PLC_01_SrcImageHandle = new PLC_Device(this._01_SrcImageHandle);
                plC_NumBox_SrcImageHandle.寫入元件位置 = this._01_SrcImageHandle;
                plC_NumBox_SrcImageHandle.讀取元件位置 = this._01_SrcImageHandle;
                plC_NumBox_SrcImageHandle.Run(this.PLC);
                this.PLC_01_SrcImageHandle.Value = 0;
            }
            if (this._02_DstImageHandle != string.Empty)
            {
                this.PLC_02_DstImageHandle = new PLC_Device(this._02_DstImageHandle);
                plC_NumBox_DstImageHandle.寫入元件位置 = this._02_DstImageHandle;
                plC_NumBox_DstImageHandle.讀取元件位置 = this._02_DstImageHandle;
                plC_NumBox_DstImageHandle.Run(this.PLC);
                this.PLC_02_DstImageHandle.Value = 0;
            }
            #endregion
            #region 旗標位置初始化
            if (this._00_Copy != string.Empty)
            {
                this.PLC_00_Copy = new PLC_Device(this._00_Copy);
                plC_Button_Copy.寫入元件位置 = this._00_Copy;
                plC_Button_Copy.讀取元件位置 = this._00_Copy;
                plC_Button_Copy.Run(this.PLC);
                this.PLC_00_Copy.Bool = false;
            }

            #endregion

            this.PLC_UI_Init.Add_Method(Thread_Init);
        }
        private void Thread_Init()
        {
            if (!this.flag_Thread_Init)
            {
                H_Thread.FindThread(this.關聯ThreadName, Activeform, ref this.List_H_Thread);
                for (int i = 0; i < this.List_H_Thread.Count; i++)
                {
                    this.List_H_Thread[i].AddMethod(Run);
                }
                this.flag_Thread_Init = true;
            }
        }
        private void Run()
        {
            this.Init();
            if (this.PLC_00_Copy.Bool)
            {
                this.Copy(this.PLC_01_SrcImageHandle.DoubleValue, this.PLC_02_DstImageHandle.DoubleValue);
                this.PLC_00_Copy.Bool = false;
            }
        }
        private void Init()
        {
            if (this.IsHandleCreated && this.PLC != null && this.Activeform != null && !flag_Init)
            {
                if (this.AxImageCopier == null) this.AxImageCopier = new AxOvkImage.AxImageCopier();
                this.PLC_00_VegaHandle.DoubleValue = AxImageCopier.VegaHandle;
                this.flag_Init = true;
            }
        }

        #region Function
        public void Copy(long SrcImageHandle, long DstImageHandle)
        {
            this.AxImageCopier.SrcImageHandle = SrcImageHandle;
            this.AxImageCopier.DstImageHandle = DstImageHandle;
            this.AxImageCopier.Copy();
        }
        #endregion
        public H_ImageCopier()
        {
            InitializeComponent();
        }
    }
}
