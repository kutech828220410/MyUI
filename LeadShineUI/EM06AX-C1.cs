using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
using LadderUI;
using LadderConnection;
using MyUI;

namespace LeadShineUI
{
    public partial class EM06AX_C1 : UserControl
    {
        #region 設備參數
        public enum enum_NodeNum : int
        {
            _1 = 1, _2, _3, _4, _5, _6, _7, _8
        }
        private enum_NodeNum _NodeNum = enum_NodeNum._1;
        [ReadOnly(false), Browsable(true), Category("設備參數"), Description(""), DefaultValue("")]
        public enum_NodeNum NodeNum
        {
            get
            {
                return this._NodeNum;
            }
            set
            {
                _NodeNum = value;
                this.設備名稱 = this.Get_StreamName();
            }
        }

        private int _CardNum = 0;
        [ReadOnly(false), Browsable(true), Category("設備參數"), Description(""), DefaultValue("")]
        public int CardNum
        {
            get
            {
                return this._CardNum;
            }
            set
            {
                this._CardNum = value;
                this.設備名稱 = this.Get_StreamName();
            }
        }

        [ReadOnly(true), Browsable(true), Category("設備參數"), Description(""), DefaultValue("")]
        public string 設備名稱
        {
            get
            {
                return this.numWordTextBox_StreamName.Text;
            }
            set
            {
                this.numWordTextBox_StreamName.Text = value;
            }
        }

        private int _CycleTime = 1;
        [ReadOnly(false), Browsable(true), Category("設備參數"), Description(""), DefaultValue("")]
        public int CycleTime
        {
            get { return _CycleTime; }
            set
            {
                _CycleTime = value;
            }
        }
        #endregion

        #region PLC-AD-數值位置

        private PLC_Device PLC_Device_CH00_AD_Mode = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-數值位置"), Description(""), DefaultValue("")]
        public string CH00_AD_Mode
        {
            get 
            {
                return this.PLC_Device_CH00_AD_Mode.GetAdress();        
            }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH00_AD_Mode.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH01_AD_Mode = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-數值位置"), Description(""), DefaultValue("")]
        public string CH01_AD_Mode
        {
            get
            {
                return this.PLC_Device_CH01_AD_Mode.GetAdress();
            }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH01_AD_Mode.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH02_AD_Mode = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-數值位置"), Description(""), DefaultValue("")]
        public string CH02_AD_Mode
        {
            get
            {
                return this.PLC_Device_CH02_AD_Mode.GetAdress();
            }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH02_AD_Mode.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH03_AD_Mode = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-數值位置"), Description(""), DefaultValue("")]
        public string CH03_AD_Mode
        {
            get
            {
                return this.PLC_Device_CH03_AD_Mode.GetAdress();
            }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH03_AD_Mode.SetAdress(value);
            }
        }


        private PLC_Device PLC_Device_CH00_AD_讀取值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-數值位置"), Description(""), DefaultValue("")]
        public string CH00_AD_讀取值
        {
            get
            {
                return this.PLC_Device_CH00_AD_讀取值.GetAdress();
            }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH00_AD_讀取值.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH01_AD_讀取值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-數值位置"), Description(""), DefaultValue("")]
        public string CH01_AD_讀取值
        {
            get
            {
                return this.PLC_Device_CH01_AD_讀取值.GetAdress();
            }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH01_AD_讀取值.SetAdress(value);
            }
        }


        private PLC_Device PLC_Device_CH02_AD_讀取值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-數值位置"), Description(""), DefaultValue("")]
        public string CH02_AD_讀取值
        {
            get
            {
                return this.PLC_Device_CH02_AD_讀取值.GetAdress();
            }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH02_AD_讀取值.SetAdress(value);
            }
        }


        private PLC_Device PLC_Device_CH03_AD_讀取值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-數值位置"), Description(""), DefaultValue("")]
        public string CH03_AD_讀取值
        {
            get
            {
                return this.PLC_Device_CH03_AD_讀取值.GetAdress();
            }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH03_AD_讀取值.SetAdress(value);
            }
        }
        #endregion
        #region PLC-AD-旗標位置

        private PLC_Device PLC_Device_CH00_AD_更新設定 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-旗標位置"), Description(""), DefaultValue("")]
        public virtual string CH00_AD_更新設定
        {
            get
            {
                return this.PLC_Device_CH00_AD_更新設定.GetAdress();
            }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH00_AD_更新設定.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH01_AD_更新設定 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-旗標位置"), Description(""), DefaultValue("")]
        public virtual string CH01_AD_更新設定
        {
            get
            {
                return this.PLC_Device_CH01_AD_更新設定.GetAdress();
            }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH01_AD_更新設定.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH02_AD_更新設定 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-旗標位置"), Description(""), DefaultValue("")]
        public virtual string CH02_AD_更新設定
        {
            get
            {
                return this.PLC_Device_CH02_AD_更新設定.GetAdress();
            }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH02_AD_更新設定.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH03_AD_更新設定 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-旗標位置"), Description(""), DefaultValue("")]
        public virtual string CH03_AD_更新設定
        {
            get
            {
                return this.PLC_Device_CH03_AD_更新設定.GetAdress();
            }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH03_AD_更新設定.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH00_AD_取值完成 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-旗標位置"), Description(""), DefaultValue("")]
        public virtual string CH00_AD_取值完成
        {
            get
            {
                return this.PLC_Device_CH00_AD_取值完成.GetAdress();
            }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH00_AD_取值完成.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH01_AD_取值完成 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-旗標位置"), Description(""), DefaultValue("")]
        public virtual string CH01_AD_取值完成
        {
            get
            {
                return this.PLC_Device_CH01_AD_取值完成.GetAdress();
            }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH01_AD_取值完成.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH02_AD_取值完成 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-旗標位置"), Description(""), DefaultValue("")]
        public virtual string CH02_AD_取值完成
        {
            get
            {
                return this.PLC_Device_CH02_AD_取值完成.GetAdress();
            }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH02_AD_取值完成.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH03_AD_取值完成 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-AD-旗標位置"), Description(""), DefaultValue("")]
        public virtual string CH03_AD_取值完成
        {
            get
            {
                return this.PLC_Device_CH03_AD_取值完成.GetAdress();
            }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH03_AD_取值完成.SetAdress(value);
            }
        }
        #endregion
        #region PLC-DA-數值位置

        private PLC_Device PLC_Device_CH00_DA_Mode = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-DA-數值位置"), Description(""), DefaultValue("")]
        public string CH00_DA_Mode
        {
            get
            {
                return this.PLC_Device_CH00_DA_Mode.GetAdress();
            }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH00_DA_Mode.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH01_DA_Mode = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-DA-數值位置"), Description(""), DefaultValue("")]
        public string CH01_DA_Mode
        {
            get
            {
                return this.PLC_Device_CH01_DA_Mode.GetAdress();
            }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH01_DA_Mode.SetAdress(value);
            }
        }


        private PLC_Device PLC_Device_CH00_DA_讀取值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-DA-數值位置"), Description(""), DefaultValue("")]
        public string CH00_DA_讀取值
        {
            get
            {
                return this.PLC_Device_CH00_DA_讀取值.GetAdress();
            }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH00_DA_讀取值.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH01_DA_讀取值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-DA-數值位置"), Description(""), DefaultValue("")]
        public string CH01_DA_讀取值
        {
            get
            {
                return this.PLC_Device_CH01_DA_讀取值.GetAdress();
            }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH01_DA_讀取值.SetAdress(value);
            }
        }


        private PLC_Device PLC_Device_CH00_DA_寫入值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-DA-數值位置"), Description(""), DefaultValue("")]
        public string CH00_DA_寫入值
        {
            get
            {
                return this.PLC_Device_CH00_DA_寫入值.GetAdress();
            }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH00_DA_寫入值.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH01_DA_寫入值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-DA-數值位置"), Description(""), DefaultValue("")]
        public string CH01_DA_寫入值
        {
            get
            {
                return this.PLC_Device_CH01_DA_寫入值.GetAdress();
            }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH01_DA_寫入值.SetAdress(value);
            }
        }

        #endregion
        #region PLC-DA-旗標位置

        private PLC_Device PLC_Device_CH00_DA_更新設定 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-DA-旗標位置"), Description(""), DefaultValue("")]
        public virtual string CH00_DA_更新設定
        {
            get
            {
                return this.PLC_Device_CH00_DA_更新設定.GetAdress();
            }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH00_DA_更新設定.SetAdress(value);
            }
        }

        private PLC_Device PLC_Device_CH01_DA_更新設定 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("PLC-DA-旗標位置"), Description(""), DefaultValue("")]
        public virtual string CH01_DA_更新設定
        {
            get
            {
                return this.PLC_Device_CH01_DA_更新設定.GetAdress();
            }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) PLC_Device_CH01_DA_更新設定.SetAdress(value);
            }
        }

        #endregion

        private String StreamName;

        public EM06AX_C1()
        {
            InitializeComponent();
        }

        #region UI

        private List<PLC_ComboBox> List_PLC_ComboBox_AD_Mode = new List<PLC_ComboBox>();
        private List<PLC_NumBox> List_PLC_NumBox_AD_讀取值 = new List<PLC_NumBox>();
        private List<PLC_Button> List_PLC_Button_AD_更新設定 = new List<PLC_Button>();

        private List<PLC_ComboBox> List_PLC_ComboBox_DA_Mode = new List<PLC_ComboBox>();
        private List<PLC_NumBox> List_PLC_NumBox_DA_讀取值 = new List<PLC_NumBox>();
        private List<PLC_NumBox> List_PLC_NumBox_DA_寫入值 = new List<PLC_NumBox>();
        private List<PLC_Button> List_PLC_Button_DA_更新設定 = new List<PLC_Button>();
        private List<PLC_Device> List_PLC_Device_AD_取值完成 = new List<PLC_Device>();
        private void Init_UI()
        {
            this.Invoke(new Action(delegate
            {
                this.plC_ComboBox_CH00_AD_Mode.Load_PLC_Device(this.PLC_Device_CH00_AD_Mode);
                this.plC_ComboBox_CH01_AD_Mode.Load_PLC_Device(this.PLC_Device_CH01_AD_Mode);
                this.plC_ComboBox_CH02_AD_Mode.Load_PLC_Device(this.PLC_Device_CH02_AD_Mode);
                this.plC_ComboBox_CH03_AD_Mode.Load_PLC_Device(this.PLC_Device_CH03_AD_Mode);

                this.plC_NumBox_CH00_AD_讀取值.Load_PLC_Device(this.PLC_Device_CH00_AD_讀取值);
                this.plC_NumBox_CH01_AD_讀取值.Load_PLC_Device(this.PLC_Device_CH01_AD_讀取值);
                this.plC_NumBox_CH02_AD_讀取值.Load_PLC_Device(this.PLC_Device_CH02_AD_讀取值);
                this.plC_NumBox_CH03_AD_讀取值.Load_PLC_Device(this.PLC_Device_CH03_AD_讀取值);

                this.plC_Button_CH00_AD_更新設定.Load_PLC_Device(this.PLC_Device_CH00_AD_更新設定);
                this.plC_Button_CH01_AD_更新設定.Load_PLC_Device(this.PLC_Device_CH01_AD_更新設定);
                this.plC_Button_CH02_AD_更新設定.Load_PLC_Device(this.PLC_Device_CH02_AD_更新設定);
                this.plC_Button_CH03_AD_更新設定.Load_PLC_Device(this.PLC_Device_CH03_AD_更新設定);

                this.plC_ComboBox_CH00_DA_Mode.Load_PLC_Device(this.PLC_Device_CH00_DA_Mode);
                this.plC_ComboBox_CH01_DA_Mode.Load_PLC_Device(this.PLC_Device_CH01_DA_Mode);

                this.plC_NumBox_CH00_DA_讀取值.Load_PLC_Device(this.PLC_Device_CH00_DA_讀取值);
                this.plC_NumBox_CH01_DA_讀取值.Load_PLC_Device(this.PLC_Device_CH01_DA_讀取值);

                this.plC_NumBox_CH00_DA_寫入值.Load_PLC_Device(this.PLC_Device_CH00_DA_寫入值);
                this.plC_NumBox_CH01_DA_寫入值.Load_PLC_Device(this.PLC_Device_CH01_DA_寫入值);

                this.plC_Button_CH00_DA_更新設定.Load_PLC_Device(this.PLC_Device_CH00_DA_更新設定);
                this.plC_Button_CH01_DA_更新設定.Load_PLC_Device(this.PLC_Device_CH01_DA_更新設定);

                this.List_PLC_ComboBox_AD_Mode.Add(this.plC_ComboBox_CH00_AD_Mode);
                this.List_PLC_ComboBox_AD_Mode.Add(this.plC_ComboBox_CH01_AD_Mode);
                this.List_PLC_ComboBox_AD_Mode.Add(this.plC_ComboBox_CH02_AD_Mode);
                this.List_PLC_ComboBox_AD_Mode.Add(this.plC_ComboBox_CH03_AD_Mode);
         
                this.List_PLC_NumBox_AD_讀取值.Add(this.plC_NumBox_CH00_AD_讀取值);
                this.List_PLC_NumBox_AD_讀取值.Add(this.plC_NumBox_CH01_AD_讀取值);
                this.List_PLC_NumBox_AD_讀取值.Add(this.plC_NumBox_CH02_AD_讀取值);
                this.List_PLC_NumBox_AD_讀取值.Add(this.plC_NumBox_CH03_AD_讀取值);

                this.List_PLC_Button_AD_更新設定.Add(this.plC_Button_CH00_AD_更新設定);
                this.List_PLC_Button_AD_更新設定.Add(this.plC_Button_CH01_AD_更新設定);
                this.List_PLC_Button_AD_更新設定.Add(this.plC_Button_CH02_AD_更新設定);
                this.List_PLC_Button_AD_更新設定.Add(this.plC_Button_CH03_AD_更新設定);

                this.List_PLC_Device_AD_取值完成.Add(this.PLC_Device_CH00_AD_取值完成);
                this.List_PLC_Device_AD_取值完成.Add(this.PLC_Device_CH01_AD_取值完成);
                this.List_PLC_Device_AD_取值完成.Add(this.PLC_Device_CH02_AD_取值完成);
                this.List_PLC_Device_AD_取值完成.Add(this.PLC_Device_CH03_AD_取值完成);


                this.List_PLC_ComboBox_DA_Mode.Add(this.plC_ComboBox_CH00_DA_Mode);
                this.List_PLC_ComboBox_DA_Mode.Add(this.plC_ComboBox_CH01_DA_Mode);

                this.List_PLC_NumBox_DA_讀取值.Add(this.plC_NumBox_CH00_DA_讀取值);
                this.List_PLC_NumBox_DA_讀取值.Add(this.plC_NumBox_CH01_DA_讀取值);

                this.List_PLC_NumBox_DA_寫入值.Add(this.plC_NumBox_CH00_DA_寫入值);
                this.List_PLC_NumBox_DA_寫入值.Add(this.plC_NumBox_CH01_DA_寫入值);

                this.List_PLC_Button_DA_更新設定.Add(this.plC_Button_CH00_DA_更新設定);
                this.List_PLC_Button_DA_更新設定.Add(this.plC_Button_CH01_DA_更新設定);

                string commemt = "";
                foreach (PLC_NumBox ctl in List_PLC_NumBox_DA_讀取值)
                {
                    commemt = ctl.Name;
                    PLC.properties.Device.Set_Device(ctl.寫入元件位置, commemt);
                }
                foreach (PLC_Button ctl in List_PLC_Button_DA_更新設定)
                {
                    commemt = ctl.Name;
                    PLC.properties.Device.Set_Device(ctl.寫入元件位置, commemt);
                }
                foreach (PLC_ComboBox ctl in List_PLC_ComboBox_DA_Mode)
                {
                    commemt = ctl.Name;
                    PLC.properties.Device.Set_Device(ctl.寫入元件位置, commemt);
                }
                foreach (PLC_NumBox ctl in List_PLC_NumBox_DA_寫入值)
                {
                    commemt = ctl.Name;
                    PLC.properties.Device.Set_Device(ctl.寫入元件位置, commemt);
                }
                foreach (PLC_NumBox ctl in List_PLC_NumBox_AD_讀取值)
                {
                    commemt = ctl.Name;
                    PLC.properties.Device.Set_Device(ctl.寫入元件位置, commemt);
                }
                foreach (PLC_Button ctl in List_PLC_Button_AD_更新設定)
                {
                    commemt = ctl.Name;
                    PLC.properties.Device.Set_Device(ctl.寫入元件位置, commemt);
                }
                foreach (PLC_ComboBox ctl in List_PLC_ComboBox_AD_Mode)
                {
                    commemt = ctl.Name;
                    PLC.properties.Device.Set_Device(ctl.寫入元件位置, commemt);
                }
                int index =0;
                foreach (PLC_Device ctl in List_PLC_Device_AD_取值完成)
                {
                    commemt = "PLC_Device_CH" + index.ToString("00") + "_DA_更新設定";
                    PLC.properties.Device.Set_Device(ctl.GetAdress(), commemt);
                    index++;
                }

                for (int i = 0; i < this.List_PLC_ComboBox_AD_Mode.Count; i++)
                {
                    this.List_PLC_ComboBox_AD_Mode[i].SetValue((int)this.Get_AD_mode(i));
                }
            }));
  
        }
        #endregion

        private bool _IsOpen = false;
        [ReadOnly(false), Browsable(false), Category(""), Description(""), DefaultValue("")]
        public bool IsOpen
        {
            get
            {
                return this._IsOpen;
            }
            set
            {

                this._IsOpen = value;
            }
        }
        private Form Active_Form;
        private LowerMachine PLC;
        private MyConvert myConvert = new MyConvert();
        private MyThread MyThread_Program;

        public void Run(Form form, LowerMachine lowerMachine)
        {
            this.PLC = lowerMachine;
            this.IsOpen = true;
            this.Active_Form = form;
            this.Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);

            this.Init();

            this.MyThread_Program = new MyThread(form);
            this.MyThread_Program.Add_Method(this.sub_Program);
            this.MyThread_Program.AutoRun(true);
            this.MyThread_Program.SetSleepTime(this.CycleTime);
            this.MyThread_Program.Trigger();

            this.PLC.Add_UI_Method(this.sub_RefreshUI);
        }

        public void Init()
        {
            this.Init_UI();
        }
        private void sub_Program()
        {
            if (this.PLC != null && this.IsOpen)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (this.List_PLC_Button_AD_更新設定[i].GetValue())
                    {
                        this.Set_AD_mode(i, (enum_Mode)this.List_PLC_ComboBox_AD_Mode[i].GetValue());
                        this.List_PLC_Button_AD_更新設定[i].SetValue(false);
                    }
                }
                for (int i = 0; i < 4; i++)
                {            
                    this.List_PLC_NumBox_AD_讀取值[i].SetValue((int)(this.Get_AD_Input(i) * 1000));
                    this.List_PLC_Device_AD_取值完成[i].SetValue(true);
                }
                for (int i = 0; i < 2; i++)
                {
                    if (this.List_PLC_Button_DA_更新設定[i].GetValue())
                    {
                        this.Set_DA_mode(i, (enum_Mode)this.List_PLC_ComboBox_DA_Mode[i].GetValue());              
                        this.Set_DA_Output(i, (this.List_PLC_NumBox_DA_寫入值[i].GetValue() / 1000D));
                        this.List_PLC_Button_DA_更新設定[i].SetValue(false);
                    }                 
                }
                for (int i = 0; i < 2; i++)
                {
                    this.List_PLC_NumBox_DA_讀取值[i].SetValue((int)(this.Get_DA_Output(i) * 1000));
                }
            }
        }
        private void sub_RefreshUI()
        {   
            if (PLC != null)
            {
                plC_Button_Open.SetValue(this.IsOpen);
                this.plC_Button_Open.Run(this.PLC);

                MyThread_Program.GetCycleTime(100, label_CycleTime);
            }
        }
        private string Get_StreamName()
        {
            return "EM06AX_C1-" + CardNum.ToString("00") + "-" + ((int)NodeNum).ToString("00");
        }

        #region Function
        public enum enum_Mode : ushort
        {
            Voltage, Current
        }
        public void Set_DA_Output(int chanel, double value)
        {
            if (value > 30) value = 30;
            if (value < -30) value = -30;
            Dmc3000.nmc_set_da_output((ushort)this._CardNum, (ushort)this._NodeNum, (ushort)chanel, value);
        }
        public double Get_DA_Output(int chanel)
        {
            double value = 0;
            Dmc3000.nmc_get_da_output((ushort)this._CardNum, (ushort)this._NodeNum, (ushort)chanel,ref value);
            return value;
        }
        public void Set_DA_mode(int chanel, enum_Mode enum_Mode)
        {
            Dmc3000.nmc_set_da_mode((ushort)this._CardNum, (ushort)this._NodeNum, (ushort)chanel, (ushort)enum_Mode, 0);
        }
        public enum_Mode Get_DA_mode(int chanel)
        {
            ushort enum_Mode_temp = 0;
            Dmc3000.nmc_get_da_mode((ushort)this._CardNum, (ushort)this._NodeNum, (ushort)chanel, ref enum_Mode_temp, 0);
            return (enum_Mode)enum_Mode_temp;
        }

        public double Get_AD_Input(int chanel)
        {
            double value = 0;
            Dmc3000.nmc_get_ad_input((ushort)this._CardNum, (ushort)this._NodeNum, (ushort)chanel, ref value);
            return value;
        }
        public void Set_AD_mode(int chanel, enum_Mode enum_Mode)
        {
            Dmc3000.nmc_set_ad_mode((ushort)this._CardNum, (ushort)this._NodeNum, (ushort)chanel, (ushort)enum_Mode, 0);
        }
        public enum_Mode Get_AD_mode(int chanel)
        {
            ushort enum_Mode_temp = 0;
            Dmc3000.nmc_get_ad_mode((ushort)this._CardNum, (ushort)this._NodeNum, (ushort)chanel, ref enum_Mode_temp, 0);
            return (enum_Mode)enum_Mode_temp;
        }
        #endregion
        #region Event
        private void plC_Button_Open_btnClick(object sender, EventArgs e)
        {
            this.IsOpen = !this.IsOpen;
        }
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            this.IsOpen = false;
        }
        #endregion
    }
}
