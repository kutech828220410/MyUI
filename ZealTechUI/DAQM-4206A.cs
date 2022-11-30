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
using Basic;
using LadderUI;
using LadderConnection;
using MyUI;
namespace ZealTechUI
{
    [Designer(typeof(ComponentSet.JLabelExDesigner))]  
    public partial class DAQM_4206A : UserControl
    {
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
        #region 旗標位置
        private PLC_Device PLC_00_通訊測試 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _00_通訊測試
        {
            get { return this.PLC_00_通訊測試.GetAdress(); }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK)
                {
                    this.PLC_00_通訊測試.SetAdress(value);
                }
            }
        }

        private PLC_Device PLC_01_通訊已建立 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _01_通訊已建立
        {
            get { return this.PLC_01_通訊已建立.GetAdress(); }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK)
                {
                    this.PLC_01_通訊已建立.SetAdress(value);
                }
            }
        }

        private PLC_Device PLC_10_更新數值完成 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _10_更新數值完成
        {
            get { return this.PLC_10_更新數值完成.GetAdress(); }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK)
                {
                    this.PLC_10_更新數值完成.SetAdress(value);
                }
            }
        }
        #endregion
        #region 數值位置
        private PLC_Device PLC_CH00_AD數值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public virtual string _CH00_AD數值
        {
            get { return this.PLC_CH00_AD數值.GetAdress(); }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK)
                {
                    this.PLC_CH00_AD數值.SetAdress(value);
                }
            }
        }

        private PLC_Device PLC_CH01_AD數值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public virtual string _CH01_AD數值
        {
            get { return this.PLC_CH01_AD數值.GetAdress(); }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK)
                {
                    this.PLC_CH01_AD數值.SetAdress(value);
                }
            }
        }

        private PLC_Device PLC_CH02_AD數值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public virtual string _CH02_AD數值
        {
            get { return this.PLC_CH02_AD數值.GetAdress(); }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK)
                {
                    this.PLC_CH02_AD數值.SetAdress(value);
                }
            }
        }

        private PLC_Device PLC_CH03_AD數值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public virtual string _CH03_AD數值
        {
            get { return this.PLC_CH03_AD數值.GetAdress(); }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK)
                {
                    this.PLC_CH03_AD數值.SetAdress(value);
                }
            }
        }

        private PLC_Device PLC_CH04_AD數值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public virtual string _CH04_AD數值
        {
            get { return this.PLC_CH04_AD數值.GetAdress(); }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK)
                {
                    this.PLC_CH04_AD數值.SetAdress(value);
                }
            }
        }

        private PLC_Device PLC_CH05_AD數值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public virtual string _CH05_AD數值
        {
            get { return this.PLC_CH05_AD數值.GetAdress(); }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK)
                {
                    this.PLC_CH05_AD數值.SetAdress(value);
                }
            }
        }

        private PLC_Device PLC_CH06_AD數值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public virtual string _CH06_AD數值
        {
            get { return this.PLC_CH06_AD數值.GetAdress(); }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK)
                {
                    this.PLC_CH06_AD數值.SetAdress(value);
                }
            }
        }

        private PLC_Device PLC_CH07_AD數值 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public virtual string _CH07_AD數值
        {
            get { return this.PLC_CH07_AD數值.GetAdress(); }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK)
                {
                    this.PLC_CH07_AD數值.SetAdress(value);
                }
            }
        }
        #endregion

        [Browsable(false)]
        public bool IsOpen
        {
            get
            {
                return this.serialPort.IsOpen;
            }
        }
        private LowerMachine PLC;
        private PLC_UI_Init PLC_UI_Init;
        private MyConvert myConvert = new MyConvert();
        private MyTimer MyTimer_ComTimeOut = new MyTimer();
        private MyThread MyThread_RS232;
        private Form Active_Form;
        public bool PortIsBusy = false;
        private List<byte> byte_read_buf = new List<byte>();
        private List<PLC_NumBox> List_PLC_NumBox_Value = new List<PLC_NumBox>();
        private int[] Value;

        [Browsable(false)]
        public string COMName
        {
            get
            {
                if (this.IsHandleCreated)
                {
                    string value = "";
                    this.Invoke(new Action(delegate
                    {
                        value = this.comboBox_COM.Text;
                    }));
                    return value;
                }
                else
                {
                    return this.comboBox_COM.Text;
                }
            }
            set
            {
                if(this.IsHandleCreated)
                {
                    this.Invoke(new Action(delegate
                    {
                        this.comboBox_COM.Text = value;
                    }));
                }
                else
                {
                    this.comboBox_COM.Text = value;
                }
            }
        }
        [Browsable(false)]
        public string Adress
        {
            get
            {
                if (this.IsHandleCreated)
                {
                    string value = "";
                    this.Invoke(new Action(delegate
                    {
                        value = this.comboBox_Adress.Text;
                    }));
                    return value;
                }
                else
                {
                    return this.comboBox_Adress.Text;
                }
            }
            set
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(delegate
                    {
                        this.comboBox_Adress.Text = value;
                    }));
                }
                else
                {
                    this.comboBox_Adress.Text = value;
                }
            }
        }
        [Browsable(false)]
        public string Baudrate
        {
            get
            {
                if (this.Enum_Baurate == enum_Baurate._9600bps)
                {
                    return "9600";
                }
                else if (this.Enum_Baurate == enum_Baurate._19200bps)
                {
                    return "19200";
                }
                else if (this.Enum_Baurate == enum_Baurate._38400bps)
                {
                    return "38400";
                }
                else if (this.Enum_Baurate == enum_Baurate._57600bps)
                {
                    return "57600";
                }
                else if (this.Enum_Baurate == enum_Baurate._115200bps)
                {
                    return "115200";
                }

                return "";
            }
        }
        [Browsable(false)]
        public string Parity
        {
            get
            {
                if (this.Enum_ComProperties == enum_ComProperties._8N1)
                {
                    return "None";
                }
                else if (this.Enum_ComProperties == enum_ComProperties._8E1)
                {
                    return "Even";
                }
                else if (this.Enum_ComProperties == enum_ComProperties._8O1)
                {
                    return "Odd";
                }
                return "";
            }
        }
        [Browsable(false)]
        public string DataBits
        {
            get
            {
                if (this.Enum_ComProperties == enum_ComProperties._8N1)
                {
                    return "8";
                }
                else if (this.Enum_ComProperties == enum_ComProperties._8E1)
                {
                    return "8";
                }
                else if (this.Enum_ComProperties == enum_ComProperties._8O1)
                {
                    return "8";
                }
                return "";
            }
        }
        [Browsable(false)]
        public string StopBits
        {
            get
            {
                if (this.Enum_ComProperties == enum_ComProperties._8N1)
                {
                    return "1";
                }
                else if (this.Enum_ComProperties == enum_ComProperties._8E1)
                {
                    return "1";
                }
                else if (this.Enum_ComProperties == enum_ComProperties._8O1)
                {
                    return "1";
                }
                return "";
            }
        }

        private enum_Baurate _Enum_Baurate = enum_Baurate._9600bps;
        [Browsable(false)]
        public enum_Baurate Enum_Baurate
        {
            get
            {
                return this._Enum_Baurate;
            }
            set
            {
                if(this.IsHandleCreated)
                {
                    this.Invoke(new Action(delegate
                    {
                        this._Enum_Baurate = value;
                        this.label_Baudrate.Text = this.Baudrate;
                    }));   
                }
                else
                {
                    this._Enum_Baurate = value;
                    this.label_Baudrate.Text = this.Baudrate;
                }
            }
        }
        public enum enum_Baurate : int
        {
             _9600bps = 3, _19200bps = 4, _38400bps = 5, _57600bps = 6, _115200bps = 7
        }

        private enum_ComProperties _Enum_ComProperties = enum_ComProperties._8N1;
        [Browsable(false)]
        public enum_ComProperties Enum_ComProperties
        {
            get
            {
                return this._Enum_ComProperties;
            }
            set
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(delegate
                    {
                        this._Enum_ComProperties = value;
                        //if (this.Enum_ComProperties == enum_ComProperties._8N1)
                        //{
                        //    this.label_ComProperty.Text = "8N1";
                        //}
                        //else if (this.Enum_ComProperties == enum_ComProperties._8E1)
                        //{
                        //    this.label_ComProperty.Text = "8E1";
                        //}
                        //else if (this.Enum_ComProperties == enum_ComProperties._8O1)
                        //{
                        //    this.label_ComProperty.Text = "8O1";
                        //}

                    }));
                }
                else
                {
                    this._Enum_ComProperties = value;
                    //if (this.Enum_ComProperties == enum_ComProperties._8N1)
                    //{
                    //    this.label_ComProperty.Text = "8N1";
                    //}
                    //else if (this.Enum_ComProperties == enum_ComProperties._8E1)
                    //{
                    //    this.label_ComProperty.Text = "8E1";
                    //}
                    //else if (this.Enum_ComProperties == enum_ComProperties._8O1)
                    //{
                    //    this.label_ComProperty.Text = "8O1";
                    //}
                }
            }
        }     
        public enum enum_ComProperties
        {
            _8N1, _8E1, _8O1
        }
        public DAQM_4206A()
        {
            InitializeComponent();
        }

        public void Run(Form form, LowerMachine lowerMachine, PLC_UI_Init PLC_UI_Init)
        {
            this.Active_Form = form;
            this.PLC = lowerMachine;
            this.PLC_UI_Init = PLC_UI_Init;
            this.Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
            this.Init();

            this.MyThread_RS232 = new MyThread();
            this.MyThread_RS232.AutoRun(true);
            this.MyThread_RS232.SetSleepTime(this.CycleTime);
            this.MyThread_RS232.Add_Method(Run);
        }
        public void Init()
        {
            this.plC_Button_通訊測試.Load_PLC_Device(this.PLC_00_通訊測試);

            this.plC_Button_通訊已建立.Load_PLC_Device(this.PLC_01_通訊已建立);
            this.plC_Button_通訊已建立.SetValue(false);
            this.plC_Button_更新數值完成.Load_PLC_Device(this.PLC_10_更新數值完成);
            this.plC_Button_更新數值完成.SetValue(false);

            this.List_PLC_NumBox_Value.Add(this.plC_NumBox_CH00_Value);
            this.List_PLC_NumBox_Value.Add(this.plC_NumBox_CH01_Value);
            this.List_PLC_NumBox_Value.Add(this.plC_NumBox_CH02_Value);
            this.List_PLC_NumBox_Value.Add(this.plC_NumBox_CH03_Value);
            this.List_PLC_NumBox_Value.Add(this.plC_NumBox_CH04_Value);
            this.List_PLC_NumBox_Value.Add(this.plC_NumBox_CH05_Value);
            this.List_PLC_NumBox_Value.Add(this.plC_NumBox_CH06_Value);
            this.List_PLC_NumBox_Value.Add(this.plC_NumBox_CH07_Value);

            this.plC_NumBox_CH00_Value.Load_PLC_Device(this.PLC_CH00_AD數值);
            this.plC_NumBox_CH01_Value.Load_PLC_Device(this.PLC_CH01_AD數值);
            this.plC_NumBox_CH02_Value.Load_PLC_Device(this.PLC_CH02_AD數值);
            this.plC_NumBox_CH03_Value.Load_PLC_Device(this.PLC_CH03_AD數值);
            this.plC_NumBox_CH04_Value.Load_PLC_Device(this.PLC_CH04_AD數值);
            this.plC_NumBox_CH05_Value.Load_PLC_Device(this.PLC_CH05_AD數值);
            this.plC_NumBox_CH06_Value.Load_PLC_Device(this.PLC_CH06_AD數值);
            this.plC_NumBox_CH07_Value.Load_PLC_Device(this.PLC_CH07_AD數值);

            this.PLC_00_通訊測試.SetComment("(DAQM-4206A)通訊測試");
            this.PLC_01_通訊已建立.SetComment("(DAQM-4206A)通訊已建立");
            this.PLC_10_更新數值完成.SetComment("(DAQM-4206A)更新數值完成");
            this.PLC_CH00_AD數值.SetComment("(DAQM-4206A)CH00_AD數值");
            this.PLC_CH01_AD數值.SetComment("(DAQM-4206A)CH01_AD數值");
            this.PLC_CH02_AD數值.SetComment("(DAQM-4206A)CH02_AD數值");
            this.PLC_CH03_AD數值.SetComment("(DAQM-4206A)CH03_AD數值");
            this.PLC_CH04_AD數值.SetComment("(DAQM-4206A)CH04_AD數值");
            this.PLC_CH05_AD數值.SetComment("(DAQM-4206A)CH05_AD數值");
            this.PLC_CH06_AD數值.SetComment("(DAQM-4206A)CH06_AD數值");
            this.PLC_CH07_AD數值.SetComment("(DAQM-4206A)CH07_AD數值");

            this.comboBox_COM.DataSource = LadderConnection.TopMachine.GetAllPortname();
            this.comboBox_參數設定_Baudrate.DataSource = this.Enum_Baurate.GetEnumNames();
            for(int i = 1 ; i < 255 ; i ++)
            {
                this.comboBox_參數設定_Adress.Items.Add(i.ToString());
                this.comboBox_Adress.Items.Add(i.ToString());
            }
            this.LoadProperties();
            if (this.comboBox_參數設定_Adress.Text == "") this.comboBox_參數設定_Adress.SelectedIndex = 0;
            if (this.comboBox_Adress.Text == "") this.comboBox_Adress.SelectedIndex = 0;
        }
        private void Run()
        {
            if (this.plC_Button_通訊測試.GetValue()) 
            {
                this.ConnectTest();
                this.plC_Button_通訊測試.SetValue(false);
            }
            if (this.plC_Button_通訊已建立.GetValue())
            {
                this.plC_Button_更新數值完成.SetValue(false);
                this.Value = this.Get_AD_Value();
                if (this.Value != null)
                {
                    for (int i = 0; i < this.List_PLC_NumBox_Value.Count; i++)
                    {
                        this.List_PLC_NumBox_Value[i].SetValue(this.Value[i]);
                    }
                    this.plC_Button_更新數值完成.SetValue(true);
                }
            }
        }
        #region Function
        public void CloseCOMPort()
        {
            serialPort.Close();
        }
        public void OpenCOMPort()
        {
            this.serialPort.Close();
            this.serialPort.PortName = this.COMName;
            this.serialPort.BaudRate = this.Baudrate.StringToInt32();
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
        }
        public bool ConnectTest()
        {          
            while (true)
            {
                if (!PortIsBusy) break;
            }
            this.plC_Button_通訊已建立.SetValue(false);
            this.PortIsBusy = true;
            byte[] write_value = new byte[] { (byte)this.Adress.StringToInt32(), 0x03, 0x00, 0x00, 0x00, 0x06 };

            foreach (int ComProperties in this.Enum_ComProperties.GetEnumValues())
            {
                foreach (int Baurate in this.Enum_Baurate.GetEnumValues())
                {
          
                    this.Enum_Baurate = (enum_Baurate)Baurate;
                    this.Enum_ComProperties = (enum_ComProperties)ComProperties;
                    this.CloseCOMPort();
                    this.OpenCOMPort();

                    this.MyTimer_ComTimeOut.TickStop();
                    this.MyTimer_ComTimeOut.StartTickTime(50);

                    this.SerialPort_Write(write_value);
                    while (true)
                    {
                        if (this.byte_read_buf.Count == 17)
                        {
                            if (this.byte_read_buf[0] == (byte)this.Adress.StringToInt32())
                            {
                                if (this.byte_read_buf[1] == 0x03)
                                {
                                    if (this.byte_read_buf[2] == 0x0C && this.byte_read_buf[3] == 0x42)
                                    {
                                        this.PortIsBusy = false;
                                        this.SaveProperties();
                                        this.plC_Button_通訊已建立.SetValue(true);
                                        return true;
                                    }
                                }
                            }
                        }
                        if (this.MyTimer_ComTimeOut.IsTimeOut())
                        {
                            break;
                        }
                    }

                }

            }
            this.CloseCOMPort();
            this.PortIsBusy = false;
            return false;
        }
        public bool SetBaudrate(enum_Baurate enum_Baurate)
        {
            if (!this.IsOpen) return false;
            while (true)
            {
                if (!this.PortIsBusy) break;
            }
            this.PortIsBusy = true;
            byte[] write_value = new byte[] { (byte)this.Adress.StringToInt32(), 0x06, 0x00, 0x04, 0x00, (byte)enum_Baurate };
            this.MyTimer_ComTimeOut.TickStop();
            this.MyTimer_ComTimeOut.StartTickTime(50);

            this.SerialPort_Write(write_value);
            while (true)
            {
                if (this.byte_read_buf.Count == 8)
                {

                    for (int i = 0; i < write_value.Length; i++)
                    {
                        if (this.byte_read_buf[i] != write_value[i])
                        {
                            this.PortIsBusy = false;
                            return false;
                        }
                    }
                    
                    break;
                }
                if (this.MyTimer_ComTimeOut.IsTimeOut())
                {
                    break;
                }
            }
            this.PortIsBusy = false;
            return true;
        }
        public bool SetAress(byte Adress)
        {
            if (!this.IsOpen) return false;
            while (true)
            {
                if (!this.PortIsBusy) break;
            }
            this.PortIsBusy = true;
            byte[] write_value = new byte[] { (byte)this.Adress.StringToInt32(), 0x06, 0x00, 0x03, 0x00, Adress };
            this.MyTimer_ComTimeOut.TickStop();
            this.MyTimer_ComTimeOut.StartTickTime(50);

            this.SerialPort_Write(write_value);
            while (true)
            {
                if (this.byte_read_buf.Count == 8)
                {

                    for (int i = 0; i < write_value.Length; i++)
                    {
                        if (this.byte_read_buf[i] != write_value[i])
                        {
                            this.PortIsBusy = false;
                            return false;
                        }
                    }

                    break;
                }
                if (this.MyTimer_ComTimeOut.IsTimeOut())
                {
                    break;
                }
            }
            this.PortIsBusy = false;
            return true;
        }
        public int[] Get_AD_Value()
        {
           if (!this.IsOpen) return null;
            while (true)
            {
                if (!this.PortIsBusy) break;
            }
            this.PortIsBusy = true;
            byte[] write_value = new byte[] { (byte)this.Adress.StringToInt32(), 0x04, 0x00, 0x00, 0x00, 0x08 };
            this.MyTimer_ComTimeOut.TickStop();
            this.MyTimer_ComTimeOut.StartTickTime(100);

            this.SerialPort_Write(write_value);
            while (true)
            {
                if (this.byte_read_buf.Count == 21)
                {
                    if(this.byte_read_buf[2] == 16)
                    {
                        int[] value = new int[8];
                        for (int i = 0; i < value.Length; i++)
                        {
                            value[i] = this.byte_read_buf[i * 2 + 4] | (this.byte_read_buf[i * 2 + 3] << 8);
                        }              
                        this.PortIsBusy = false;
                        return value;
                    }
                }

                if (this.MyTimer_ComTimeOut.IsTimeOut())
                {
                    break;
                }
            }
            this.PortIsBusy = false;
            return null;
        
        }
        private void SerialPort_Write(byte[] value)
        {
            this.byte_read_buf.Clear();
            List<byte> list_value = new List<byte>();
            byte L_byte = 0;
            byte H_byte = 0;
            Basic.MyConvert.Get_CRC16(value, ref L_byte, ref H_byte);
            foreach (byte temp in value) list_value.Add(temp);
            list_value.Add(L_byte);
            list_value.Add(H_byte);
            if (!this.IsOpen) return;
            try
            {
                this.serialPort.Write(list_value.ToArray(), 0, list_value.Count);
            }
            catch
            {

            }
      
        }
        private int Get_enum_Baurate_Num(string str_value)
        {
            int index = 0;
            int[] Enum_value = enum_Baurate._115200bps.GetEnumValues();
            foreach (string value in enum_Baurate._115200bps.GetEnumNames())
            {
                if (value == str_value) return Enum_value[index];
                index++;
            }
            return -1;
        }
        #endregion
        #region Event
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseCOMPort();
            SaveProperties();
        }
        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int BytesToRead = serialPort.BytesToRead;
            for (int i = 0; i < BytesToRead; i++)
            {
                if (!this.IsOpen) return;
                byte_read_buf.Add((byte)serialPort.ReadByte());
            }
        }
        private void plC_Button_通訊測試_btnClick(object sender, EventArgs e)
        {
            if (!this.PLC_00_通訊測試.Bool) this.PLC_00_通訊測試.Bool = true;           
        }
        private void button_參數設定_Baudrate_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("確認轉換'Baudrate'?", "Asterisk", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                enum_Baurate _enum_Baurate = (enum_Baurate)Get_enum_Baurate_Num(this.comboBox_參數設定_Baudrate.Text);
                if (this.SetBaudrate(_enum_Baurate))
                {
                    MessageBox.Show(string.Format("Baudrate '{0}' 設置成功,請重新上電!", _enum_Baurate.GetEnumName()));
                }
                else
                {
                    MessageBox.Show("設置失敗!");
                }
            }
        }
        private void button_參數設定_Adress_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("確認轉換'Adress'?", "Asterisk", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                byte Adress = (byte)this.comboBox_參數設定_Adress.Text.StringToInt32();
                if (this.SetAress(Adress))
                {
                    MessageBox.Show(string.Format("Adress :  '{0}' 設置成功,請重新上電!", Adress.ToString()));
                }
                else
                {
                    MessageBox.Show("設置失敗!");
                }
            }
        }
        #endregion
        #region StreamIO
        [Serializable]
        private class SavePropertyFile
        {
            public string COMName = "";
            public string Adress = "";
        }
        private SavePropertyFile savePropertyFile = new SavePropertyFile();
        private void SaveProperties()
        {
            IFormatter binFmt = new BinaryFormatter();
            Stream stream = null;
            savePropertyFile.COMName = COMName.DeepClone();
            savePropertyFile.Adress = Adress.DeepClone();

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
                Adress = savePropertyFile.Adress.DeepClone();
    
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }

        #endregion


    }
}
