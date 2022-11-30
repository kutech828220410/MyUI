using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO.Ports;
using System.IO;
using LadderUI;
using LadderConnection;
using MyUI;
using Basic;
namespace LightControlUI
{
    public partial class LD_NP24DV_4T : UserControl
    {
        private SerialPort serialPort;
        public readonly byte Start_Code = 36;
        public readonly byte End_Code_OK = 36;
        public readonly byte End_Code_NG = 38;

        MyThread MyThread_RS232;
        MyTimer MyTimer_ComTimeOut = new MyTimer();
        public LowerMachine PLC;
        PLC_UI_Init PLC_UI_Init;

        MyConvert myConvert = new MyConvert();
        private Form Active_Form;
        private string COMName = "";
        private string Baudrate = "";
        private string DataBits = "";
        private string Parity = "";
        private string StopBits = "";
        List<byte> byte_read_buf = new List<byte>();
        public bool IsOpen
        {
            get
            {
                return serialPort.IsOpen;
            }
        }
        public bool PortIsBusy = false;
        public byte CH01_Value = 0;
        public byte CH02_Value = 0;
        public byte CH03_Value = 0;
        public byte CH04_Value = 0;
        public enum enum_Chanle : int
        {
            CH01 = 1, CH02 = 2, CH03 = 3, CH04 = 4,
        }
        #region 一般參數
        private int _CycleTime = 10;
        [ReadOnly(false), Browsable(true), Category("一般參數"), Description(""), DefaultValue("")]
        public int CycleTime
        {
            get { return this._CycleTime; }
            set
            {
                this._CycleTime = value;
            }
        }
        #endregion

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
        #region 數值位置
        private PLC_Device PLC_00_CH01_光源亮度;
        private string __00_CH01_光源亮度 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _00_CH01_光源亮度
        {
            get { return __00_CH01_光源亮度; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __00_CH01_光源亮度 = value;
                else __00_CH01_光源亮度 = "";
            }
        }

        private PLC_Device PLC_01_CH02_光源亮度;
        private string __01_CH02_光源亮度 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _01_CH02_光源亮度
        {
            get { return __01_CH02_光源亮度; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __01_CH02_光源亮度 = value;
                else __01_CH02_光源亮度 = "";
            }
        }

        private PLC_Device PLC_02_CH03_光源亮度;
        private string __02_CH03_光源亮度 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _02_CH03_光源亮度
        {
            get { return __02_CH03_光源亮度; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __02_CH03_光源亮度 = value;
                else __02_CH03_光源亮度 = "";
            }
        }

        private PLC_Device PLC_03_CH04_光源亮度;
        private string __03_CH04_光源亮度 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _03_CH04_光源亮度
        {
            get { return __03_CH04_光源亮度; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __03_CH04_光源亮度 = value;
                else __03_CH04_光源亮度 = "";
            }
        }

        #endregion
        #region 旗標位置
        private PLC_Device PLC_00_COM開啟;
        private string __00_COM開啟 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _00_COM開啟
        {
            get { return __00_COM開啟; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __00_COM開啟 = value;
                else __00_COM開啟 = "";
            }
        }

        private PLC_Device PLC_01_COM已開啟;
        private string __01_COM已開啟 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _01_COM已開啟
        {
            get { return __01_COM已開啟; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __01_COM已開啟 = value;
                else __01_COM已開啟 = "";
            }
        }

        private PLC_Device PLC_10_CH01_亮度更新完成;
        private string __10_CH01_亮度更新完成 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _10_CH01_亮度更新完成
        {
            get { return __10_CH01_亮度更新完成; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __10_CH01_亮度更新完成 = value;
                else __10_CH01_亮度更新完成 = "";
            }
        }

        private PLC_Device PLC_11_CH02_亮度更新完成;
        private string __11_CH02_亮度更新完成 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _11_CH02_亮度更新完成
        {
            get { return __11_CH02_亮度更新完成; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __11_CH02_亮度更新完成 = value;
                else __11_CH02_亮度更新完成 = "";
            }
        }

        private PLC_Device PLC_12_CH03_亮度更新完成;
        private string __12_CH03_亮度更新完成 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _12_CH03_亮度更新完成
        {
            get { return __12_CH03_亮度更新完成; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __12_CH03_亮度更新完成 = value;
                else __12_CH03_亮度更新完成 = "";
            }
        }

        private PLC_Device PLC_13_CH04_亮度更新完成;
        private string __13_CH04_亮度更新完成 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _13_CH04_亮度更新完成
        {
            get { return __13_CH04_亮度更新完成; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __13_CH04_亮度更新完成 = value;
                else __13_CH04_亮度更新完成 = "";
            }
        }
        #endregion
        public LD_NP24DV_4T()
        {
            InitializeComponent();
        }
        public void Run(Form form, LowerMachine lowerMachine, PLC_UI_Init PLC_UI_Init)
        {
            this.Active_Form = form;
            this.PLC = lowerMachine;
            this.PLC_UI_Init = PLC_UI_Init;
            Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
            this.Init();
            // PLC_UI_Init.Add_Method(Run);
            this.PLC_00_CH01_光源亮度.SetComment("PLC_CH01_光源亮度");
            this.PLC_01_CH02_光源亮度.SetComment("PLC_CH02_光源亮度");
            this.PLC_02_CH03_光源亮度.SetComment("PLC_CH03_光源亮度");
            this.PLC_03_CH04_光源亮度.SetComment("PLC_CH04_光源亮度");

            this.PLC_10_CH01_亮度更新完成.SetComment("PLC_CH01_亮度更新完成");
            this.PLC_11_CH02_亮度更新完成.SetComment("PLC_CH02_亮度更新完成");
            this.PLC_12_CH03_亮度更新完成.SetComment("PLC_CH03_亮度更新完成");
            this.PLC_13_CH04_亮度更新完成.SetComment("PLC_CH04_亮度更新完成");

            this.PLC_00_COM開啟.SetComment("PLC_COM開啟");
            this.PLC_01_COM已開啟.SetComment("PLC_COM已開啟");
            this.MyThread_RS232 = new MyThread();
            this.MyThread_RS232.AutoRun(true);
            this.MyThread_RS232.SetSleepTime(this.CycleTime);
            this.MyThread_RS232.Add_Method(Run);
            this.MyThread_RS232.Trigger();
        }
        private void Run()
        {
            if (this.PLC_00_COM開啟.Bool)
            {
                if (this.IsOpen)
                {
                    this.PLC_01_COM已開啟.Bool = true;
                }
                else
                {
                    this.OpenCOMPort();
                }
            }
            else
            {
                if (!this.IsOpen)
                {
                    this.PLC_01_COM已開啟.Bool = false;
                }
                else
                {
                    this.CloseCOMPort();
                }
            }

            if (PLC_10_CH01_亮度更新完成.Bool)
            {
                if (this.Set_Chanel_LightValue(1, (byte)this.PLC_00_CH01_光源亮度.Value))
                {
                    PLC_10_CH01_亮度更新完成.Bool = false;
                }
            }
            if (PLC_11_CH02_亮度更新完成.Bool)
            {
                if (this.Set_Chanel_LightValue(2, (byte)this.PLC_01_CH02_光源亮度.Value))
                {
                    PLC_11_CH02_亮度更新完成.Bool = false;
                }
            }
            if (PLC_12_CH03_亮度更新完成.Bool)
            {
                if (this.Set_Chanel_LightValue(3, (byte)this.PLC_02_CH03_光源亮度.Value))
                {
                    PLC_12_CH03_亮度更新完成.Bool = false;
                }
            }
            if (PLC_13_CH04_亮度更新完成.Bool)
            {
                if (this.Set_Chanel_LightValue(4, (byte)this.PLC_03_CH04_光源亮度.Value))
                {
                    PLC_13_CH04_亮度更新完成.Bool = false;
                }
            }
            this.MyThread_RS232.GetCycleTime(100, this.label_CycleTime);
            
        }
        #region Function
        public void Init()
        {
            this.serialPort = new SerialPort();
            this.serialPort.DataReceived += serialPort_DataReceived;
            LoadProperties();
            comboBox_COM.DataSource = LadderConnection.TopMachine.GetAllPortname();
            SetComboBoxText(comboBox_COM, savePropertyFile.COMName);
            SetComboBoxText(comboBox_Baudrate, this.Baudrate);
            SetComboBoxText(comboBox_StopBits, this.StopBits);
            SetComboBoxText(comboBox_DataBits, this.DataBits);
            SetComboBoxText(comboBox_Parity, this.Parity);

            if (__00_COM開啟 != "" && __00_COM開啟 != null)
            {
                this.plC_Button_Open.寫入元件位置 = __00_COM開啟;
                this.PLC_00_COM開啟 = new PLC_Device(__00_COM開啟);
                this.PLC_00_COM開啟.Bool = false;
            }
            if (__01_COM已開啟 != "" && __01_COM已開啟 != null)
            {
                this.plC_Button_Open.讀取元件位置 = __01_COM已開啟;
                this.PLC_01_COM已開啟 = new PLC_Device(__01_COM已開啟);
                this.PLC_01_COM已開啟.Bool = false;
            }


            if (__00_CH01_光源亮度 != "" && __00_CH01_光源亮度 != null)
            {
                this.PLC_00_CH01_光源亮度 = new PLC_Device(__00_CH01_光源亮度);
                plC_TrackBarHorizontal_CH01_Lightness.讀取元件位置 = __00_CH01_光源亮度;
                plC_TrackBarHorizontal_CH01_Lightness.寫入元件位置 = __00_CH01_光源亮度;

            }
            if (__01_CH02_光源亮度 != "" && __01_CH02_光源亮度 != null)
            {
                this.PLC_01_CH02_光源亮度 = new PLC_Device(__01_CH02_光源亮度);
                plC_TrackBarHorizontal_CH02_Lightness.讀取元件位置 = __01_CH02_光源亮度;
                plC_TrackBarHorizontal_CH02_Lightness.寫入元件位置 = __01_CH02_光源亮度;

            }
            if (__02_CH03_光源亮度 != "" && __02_CH03_光源亮度 != null)
            {
                this.PLC_02_CH03_光源亮度 = new PLC_Device(__02_CH03_光源亮度);
                plC_TrackBarHorizontal_CH03_Lightness.讀取元件位置 = __02_CH03_光源亮度;
                plC_TrackBarHorizontal_CH03_Lightness.寫入元件位置 = __02_CH03_光源亮度;

            }
            if (__03_CH04_光源亮度 != "" && __03_CH04_光源亮度 != null)
            {
                this.PLC_03_CH04_光源亮度 = new PLC_Device(__03_CH04_光源亮度);
                plC_TrackBarHorizontal_CH04_Lightness.讀取元件位置 = __03_CH04_光源亮度;
                plC_TrackBarHorizontal_CH04_Lightness.寫入元件位置 = __03_CH04_光源亮度;

            }


            if (__10_CH01_亮度更新完成 != "" && __10_CH01_亮度更新完成 != null)
            {
                this.PLC_10_CH01_亮度更新完成 = new PLC_Device(__10_CH01_亮度更新完成);
                plC_Button_CH01_更新完成.讀取元件位置 = __10_CH01_亮度更新完成;
                plC_Button_CH01_更新完成.寫入元件位置 = __10_CH01_亮度更新完成;
                this.PLC_10_CH01_亮度更新完成.SetValue(false);
            }
            if (__11_CH02_亮度更新完成 != "" && __11_CH02_亮度更新完成 != null)
            {
                this.PLC_11_CH02_亮度更新完成 = new PLC_Device(__11_CH02_亮度更新完成);
                plC_Button_CH02_更新完成.讀取元件位置 = __11_CH02_亮度更新完成;
                plC_Button_CH02_更新完成.寫入元件位置 = __11_CH02_亮度更新完成;
                this.PLC_11_CH02_亮度更新完成.SetValue(false);
            }
            if (__12_CH03_亮度更新完成 != "" && __12_CH03_亮度更新完成 != null)
            {
                this.PLC_12_CH03_亮度更新完成 = new PLC_Device(__12_CH03_亮度更新完成);
                plC_Button_CH03_更新完成.讀取元件位置 = __12_CH03_亮度更新完成;
                plC_Button_CH03_更新完成.寫入元件位置 = __12_CH03_亮度更新完成;
                this.PLC_12_CH03_亮度更新完成.SetValue(false);
            }
            if (__13_CH04_亮度更新完成 != "" && __13_CH04_亮度更新完成 != null)
            {
                this.PLC_13_CH04_亮度更新完成 = new PLC_Device(__13_CH04_亮度更新完成);
                plC_Button_CH04_更新完成.讀取元件位置 = __13_CH04_亮度更新完成;
                plC_Button_CH04_更新完成.寫入元件位置 = __13_CH04_亮度更新完成;
                this.PLC_13_CH04_亮度更新完成.SetValue(false);
            }
        }


        public void CloseCOMPort()
        {
            serialPort.Close();
            if (!this.IsOpen)
            {
                this.Invoke(new Action(delegate
                {
                    comboBox_COM.Enabled = true;
                    comboBox_Baudrate.Enabled = true;
                    comboBox_StopBits.Enabled = true;
                    comboBox_DataBits.Enabled = true;
                    comboBox_Parity.Enabled = true;
                }));
                if (PLC_01_COM已開啟 != null) PLC_01_COM已開啟.Bool = false;
            }
        }
        public void OpenCOMPort()
        {
            serialPort.Close();
            for (int i = 0; i < comboBox_COM.Items.Count; i++)
            {
                if (comboBox_COM.Items[i].ToString() == this.COMName)
                {
                    serialPort.PortName = this.COMName;
                    break;
                }
            }
            for (int i = 0; i < comboBox_Baudrate.Items.Count; i++)
            {
                if (comboBox_Baudrate.Items[i].ToString() == this.Baudrate)
                {
                    int temp = 0;
                    if (int.TryParse(this.Baudrate, out temp))
                    {
                        serialPort.BaudRate = temp;
                        break;
                    }
                }
            }

            if (this.DataBits == "8") serialPort.DataBits = 8;
            else if (this.DataBits == "7") serialPort.DataBits = 7;
            else if (this.DataBits == "6") serialPort.DataBits = 6;
            else if (this.DataBits == "5") serialPort.DataBits = 5;

            if (this.Parity == "None") serialPort.Parity = System.IO.Ports.Parity.None;
            else if (this.Parity == "Even") serialPort.Parity = System.IO.Ports.Parity.Even;
            else if (this.Parity == "Odd") serialPort.Parity = System.IO.Ports.Parity.Odd;

            if (this.StopBits == "2") serialPort.StopBits = System.IO.Ports.StopBits.Two;
            else if (this.StopBits == "1.5") serialPort.StopBits = System.IO.Ports.StopBits.OnePointFive;
            else if (this.StopBits == "1") serialPort.StopBits = System.IO.Ports.StopBits.One;
            try
            {
                serialPort.Open();
            }
            catch
            {
            }
            if (this.IsOpen)
            {
                this.Invoke(new Action(delegate
                {
                    comboBox_COM.Enabled = false;
                    comboBox_Baudrate.Enabled = false;
                    comboBox_StopBits.Enabled = false;
                    comboBox_DataBits.Enabled = false;
                    comboBox_Parity.Enabled = false;
                }));
                if (PLC_01_COM已開啟 != null) PLC_01_COM已開啟.Bool = true;
            }
        }

        private void SetComboBoxText(ComboBox combobox, string str)
        {
            for (int i = 0; i < combobox.Items.Count; i++)
            {
                if (combobox.Items[i].ToString() == str)
                {
                    this.Invoke(new Action(delegate { combobox.SelectedIndex = i; }));
                    break;
                }
            }
        }
        public bool Set_Chanel_LightValue(enum_Chanle enum_Chanle, byte value)
        {
            return this.Set_Chanel_LightValue((int)enum_Chanle, value);
        }
        private bool Set_Chanel_LightValue(int Chanel, byte value)
        {
            if (!this.IsOpen) return false;
            while (true)
            {
                if (!PortIsBusy) break;
            }
            string str_CheckSum = "";
            char[] str_CheckSum_Array;
            string str_value;
            char[] str_value_Array;
            byte byte_StartCode = this.Start_Code;
            byte byte_CMDCode = 3 + 48;
            byte byte_Chanel;

            byte_Chanel = (byte)(Chanel + 48);

            str_value = myConvert.ByteToStringHex(new byte[] { value });
            if (str_value.Length < 3) str_value = "0" + str_value;
            if (str_value.Length < 3) str_value = "0" + str_value;
            if (str_value.Length < 3) str_value = "0" + str_value;
            str_value_Array = str_value.ToCharArray();

            str_CheckSum = this.GetCheckSum(new byte[] { byte_StartCode, byte_CMDCode, byte_Chanel, (byte)str_value_Array[0], (byte)str_value_Array[1], (byte)str_value_Array[2] });
            str_CheckSum_Array = str_CheckSum.ToCharArray();
            this.MyTimer_ComTimeOut.TickStop();
            this.MyTimer_ComTimeOut.StartTickTime(500);
            this.PortIsBusy = true;
            if (serialPort.BytesToWrite != 0) return false;
            serialPort.Write(new byte[] { byte_StartCode, byte_CMDCode, byte_Chanel, (byte)str_value_Array[0], (byte)str_value_Array[1], (byte)str_value_Array[2], (byte)str_CheckSum_Array[0], (byte)str_CheckSum_Array[1] }, 0, 8);
            while (true)
            {
                if (byte_read_buf.Count > 0)
                {
                    if (byte_read_buf[0] == End_Code_OK)
                    {
                        this.PortIsBusy = false;
                        if (Chanel == 1)
                        {
                            this.PLC_00_CH01_光源亮度.Value = value;
                            this.CH01_Value = value;
                        }
                        else if (Chanel == 2)
                        {
                            this.PLC_01_CH02_光源亮度.Value = value;
                            this.CH02_Value = value;
                        }
                        else if (Chanel == 3)
                        {
                            this.PLC_02_CH03_光源亮度.Value = value;
                            this.CH03_Value = value;
                        }
                        else if (Chanel == 4)
                        {
                            this.PLC_03_CH04_光源亮度.Value = value;
                            this.CH04_Value = value;
                        }
                        return true;
                    }
                    else if (byte_read_buf[0] == End_Code_NG)
                    {
                        this.PortIsBusy = false;
                        return false;
                    }
                }
                if (this.MyTimer_ComTimeOut.IsTimeOut())
                {
                    this.PortIsBusy = false;
                    return false;
                }
            }
            return true;
        }
        public string GetCheckSum(byte[] byte_Array)
        {
            string CheckSum = "";
            byte byte_result = 0;
            System.Collections.BitArray XOR_result = new System.Collections.BitArray(16, false);
            List<System.Collections.BitArray> List_BitArray = new List<System.Collections.BitArray>();
            for (int i = 0; i < byte_Array.Length; i++)
            {
                byte[] Bit_Array = BitConverter.GetBytes(byte_Array[i]);
                System.Collections.BitArray arr = new System.Collections.BitArray(Bit_Array);
                List_BitArray.Add(arr);

            }
            for (int i = 0; i < List_BitArray.Count; i++)
            {
                XOR_result = List_BitArray[i].Xor(XOR_result);
            }
            for (int i = 0; i < 8; i++)
            {
                if (XOR_result[i]) byte_result += (byte)(1 << i);
            }

            CheckSum = myConvert.ByteToStringHex(new byte[] { byte_result });
            return CheckSum;
        }
        #endregion
        #region StreamIO
        [Serializable]
        private class SavePropertyFile
        {
            public string COMName = "";
            public string Baudrate = "";
            public string DataBits = "";
            public string Parity = "";
            public string StopBits = "";
        }
        private SavePropertyFile savePropertyFile = new SavePropertyFile();
        private void SaveProperties()
        {
            IFormatter binFmt = new BinaryFormatter();
            Stream stream = null;
            savePropertyFile.COMName = COMName.DeepClone();
            savePropertyFile.Baudrate = Baudrate.DeepClone();
            savePropertyFile.DataBits = DataBits.DeepClone();
            savePropertyFile.Parity = Parity.DeepClone();
            savePropertyFile.StopBits = StopBits.DeepClone();
            try
            {
                stream = File.Open(this.Name + ".pro", FileMode.Create);
                binFmt.Serialize(stream, savePropertyFile);
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }
        private void LoadProperties()
        {
            IFormatter binFmt = new BinaryFormatter();
            Stream stream = null;
            try
            {
                if (File.Exists(".\\" + this.Name + ".pro"))
                {
                    stream = File.Open(".\\" + this.Name + ".pro", FileMode.Open);
                    try { savePropertyFile = (SavePropertyFile)binFmt.Deserialize(stream); }
                    catch { }

                }
                COMName = savePropertyFile.COMName.DeepClone();
                Baudrate = savePropertyFile.Baudrate.DeepClone();
                DataBits = savePropertyFile.DataBits.DeepClone();
                Parity = savePropertyFile.Parity.DeepClone();
                StopBits = savePropertyFile.StopBits.DeepClone();
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }
        #endregion
        #region Event
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            this.MyThread_RS232.Stop();
            CloseCOMPort();
            SaveProperties();
        }
        private void comboBox_COM_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.COMName = comboBox_COM.Text;
        }
        private void comboBox_Baudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Baudrate = comboBox_Baudrate.Text;
        }
        private void comboBox_DataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DataBits = comboBox_DataBits.Text;
        }
        private void comboBox_Parity_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Parity = comboBox_Parity.Text;
        }
        private void comboBox_StopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.StopBits = comboBox_StopBits.Text;
        }
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int BytesToRead = serialPort.BytesToRead;
            for (int i = 0; i < BytesToRead; i++)
            {
                byte_read_buf.Add((byte)serialPort.ReadByte());
            }
        }


        #endregion

    }
}
