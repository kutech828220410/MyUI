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

namespace HCGUI
{
    public partial class HCG_485_IO : UserControl
    {
        public string PortName = ""; 
        private Basic.MyConvert Myconvert = new Basic.MyConvert();
        private bool FLAG_UART_RX = false;
        private List<int> UART_RX_BUF = new List<int>();
        private bool First_Init = true;
        private bool IsOpen = false;
        private MyThread MyThread_Program;
        private MyThread MyThread_RefreshUI;
        private Form Active_Form;
        private String StreamName;
        private LowerMachine PLC;
        private MyConvert myConvert = new MyConvert();
        private TabControl tabControl = new TabControl();
        private TabPage[] tabPage;
        private HCG_485_IO_Basic[] HCG_485_IO_Basic;
        private List<int> List_Input = new List<int>();
        private List<int> List_Output = new List<int>();
        #region 自訂屬性
        private string _設備名稱 = "HCG_485_IO-001";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 設備名稱
        {
            get { return _設備名稱; }
            set
            {
                _設備名稱 = value;
                numWordTextBox_StreamName.Text = _設備名稱;
            }
        }
        private int _CycleTime = 1;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int CycleTime
        {
            get { return _CycleTime; }
            set
            {
                _CycleTime = value;
            }
        }
        private int _從站數量 = 1;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int 從站數量
        {
            get { return _從站數量; }
            set
            {
                _從站數量 = value;
            }
        }
        #endregion

        public HCG_485_IO()
        {
            InitializeComponent();
        }

   
        private void SetLowerMachine(LowerMachine PLC)
        {
            this.PLC = PLC;
        }
        private void SetForm(Form form)
        {
            Active_Form = form;
        }
        public void Run(Form form, LowerMachine PLC)
        {
            SetForm(form);
            SetLowerMachine(PLC);
            StreamName = numWordTextBox_StreamName.Text + ".pro";

            Init();

            MyThread_Program = new MyThread(form);
            MyThread_Program.Add_Method(Program);
            MyThread_Program.AutoRun(true);
            MyThread_Program.SetSleepTime(this.CycleTime);
            MyThread_Program.Trigger();

            MyThread_RefreshUI = new MyThread(form);
            MyThread_RefreshUI.Add_Method(RefreshUI);
            MyThread_RefreshUI.AutoRun(true);
            MyThread_RefreshUI.SetSleepTime(10);
            MyThread_RefreshUI.Trigger();
        }
        public void Init()
        {
            this.FindForm().FormClosing += this.FormClosing;
            this.IsOpen = this.BoardOpen();
            this.LoadProperties();
            this.IsOpen = this.SerialPortOpen();
        }

        private void TabPage_Init(int Card_count)
        {
            this.Invoke(new Action(delegate
            {
                tabPage = new TabPage[Card_count];
                HCG_485_IO_Basic = new HCG_485_IO_Basic[Card_count];
                for (int i = 0; i < Card_count; i++)
                {
                    HCG_485_IO_Basic[i] = new HCG_485_IO_Basic();
                    HCG_485_IO_Basic[i].Dock = System.Windows.Forms.DockStyle.Fill;
                    HCG_485_IO_Basic[i].Location = new System.Drawing.Point(3, 3);
                    HCG_485_IO_Basic[i].Name = "HCG_485_IO_Basic" + i.ToString();
                    HCG_485_IO_Basic[i].Size = new System.Drawing.Size(569, 429);
                    HCG_485_IO_Basic[i].TabIndex = 0;
                    HCG_485_IO_Basic[i].SetPLC(this.PLC);
                    tabPage[i] = new TabPage();
                    tabPage[i].Controls.Add(HCG_485_IO_Basic[i]);
                    tabPage[i].Location = new System.Drawing.Point(4, 22);
                    tabPage[i].Name = "tabPage_card_" + i.ToString();
                    tabPage[i].Padding = new System.Windows.Forms.Padding(3);
                    tabPage[i].Size = new System.Drawing.Size(192, 74);
                    tabPage[i].TabIndex = i;
                    tabPage[i].Text = "CARD-" + i.ToString();
                    tabPage[i].UseVisualStyleBackColor = true;

                }
            }));
        }
        private void TabControl_Init()
        {
            this.Invoke(new Action(delegate
            {
                if (this.tabControl == null) this.tabControl = new TabControl();
                tabControl.SuspendLayout();
                for (int i = 0; i < tabPage.Length; i++)
                {
                    this.tabControl.Controls.Add(this.tabPage[i]);
                }
                this.tabControl.Location = new System.Drawing.Point(0, panel_Open.Size.Height);
                this.tabControl.Name = "tabControl_HCG_485_IO";
                this.tabControl.SelectedIndex = 0;
                this.tabControl.Dock = DockStyle.Fill;
                this.panel_TAB.Controls.Add(this.tabControl);
                tabControl.ResumeLayout(false);

            }));
        }

        private void Program()
        {
            if(this.IsOpen)
            {
                this.sub_GetInput();
                this.sub_WriteToPLC();
                this.sub_ReadFromPLC();
            }
          
        }
        private void RefreshUI()
        {
            if (this.PLC != null)
            {
                this.plC_Button_Open.SetValue(this.IsOpen);
                this.plC_Button_Open.Run(this.PLC);
            }
            if (this.IsOpen && this.PLC != null)
            {
                for (int i = 0; i < this.從站數量; i++)
                {
                    this.HCG_485_IO_Basic[i].RefreshUI();
                }
                MyThread_Program.GetCycleTime(100, this.label_CycleTime);
            }
        }

        #region GetInput
        private int input = 0;
        private void sub_GetInput()
        {
            for (int i = 0; i < this.從站數量; i++)
            {
                this.Command_Read_Input(i,ref input);
                this.List_Input[i] = input;
                for (int k = 0; k < 32; k++)
                {
                    this.HCG_485_IO_Basic[i].Set_Input(k, (((input >> k) & 0x01) == 1) ? true : false);
                }
            }

        }
        #endregion
        #region WriteToPLC
        private void sub_WriteToPLC()
        {
            string adress;
            bool flag;
            for (int i = 0; i < this.從站數量; i++)
            {
                for (int k = 0; k < 32; k++)
                {
                    flag = this.HCG_485_IO_Basic[i].Get_Input(k);
                    adress = this.HCG_485_IO_Basic[i].Get_Input_Adress(k);
                    if (adress != "" && adress != null) PLC.properties.device_system.Set_Device(adress, flag);
                }
            }

        }
        #endregion
        #region ReadFromPLC
        private void sub_ReadFromPLC()
        {
            string adress;
            bool flag;
            int Output = 0;
            int Out_Buf = 0;
            for (int i = 0; i < this.從站數量; i++)
            {
                Output = 0;
                //this.Command_Read_Output(i, ref Out_Buf);
                for (int k = 0; k < 32; k++)
                {
                    adress = HCG_485_IO_Basic[i].Get_Output_Adress(k);
                    if (adress != "" && adress != null)
                    {
                        if (!this.HCG_485_IO_Basic[i].Get_Output_PCUse(k))
                        {
                            flag = PLC.properties.device_system.Get_DeviceFast_Ex(adress);
                            HCG_485_IO_Basic[i].Set_Output(k, flag);
                            Output = myConvert.Int32SetBit(flag, Output, k);
               
                        }
                    }
                }
                this.List_Output[i] = Output;
                this.Command_Write_Output(i, Output);
            }

        }
        #endregion

        private bool BoardOpen()
        {
            bool flag_OK = true;

            if (this.從站數量 <= 0) return false ;

            if (this.First_Init)
            {
                this.TabPage_Init(this.從站數量);
                this.TabControl_Init();
                for (int i = 0; i < this.從站數量; i++)
                {
                    HCG_485_IO_Basic[i].Init();
                }
            }
            if (!this.First_Init)
            {
                if (!this.SerialPortOpen())
                {
                    flag_OK = false;
                }
                this.SaveProperties();
            }
            this.First_Init = false;
            for (int i = 0; i < this.從站數量; i++)
            {
                HCG_485_IO_Basic[i].Set_UI_Enable(false);
            }

            int output = 0;
            this.List_Input.Clear();
            this.List_Output.Clear();
            for (int i = 0; i < this.從站數量; i++)
            {
                this.List_Input.Add(0);
                this.List_Output.Add(0);
                if (!this.Command_Read_Output(i , ref output))
                {
                    MyMessageBox.ShowDialog(string.Format("HCG485 ,站號 :{0} ,通訊失敗!", i.ToString("00")));
                    flag_OK = false;
                }
            }
         
            return flag_OK;
        }
        private void BoardClose()
        {
            this.SerialPortClose();
            this.IsOpen = false;
            for (int i = 0; i < this.從站數量; i++)
            {
                HCG_485_IO_Basic[i].Set_UI_Enable(true);
            }
        }

        public bool Get_Input(int station , int NumofBit)
        {
            int temp = this.Get_Input(station);
            return myConvert.Int32GetBit(temp, NumofBit);         
        }
        public int Get_Input(int station)
        {
            if (station >= this.List_Input.Count) return -1;

            return this.List_Input[station];
        }
        public bool Get_Output(int station, int NumofBit)
        {
            int temp = this.Get_Output(station);
            return myConvert.Int32GetBit(temp, NumofBit);
        }
        public int Get_Output(int station)
        {
            if (station >= this.List_Output.Count) return -1;

            return this.List_Output[station];
        }
        public void Set_Output(int station, int NumofBit, bool ON_OFF)
        {
            string adress = HCG_485_IO_Basic[station].Get_Output_Adress(NumofBit);
            if(adress != "")
            {
                PLC_Device pLC_Device = new PLC_Device(adress);
                pLC_Device.Bool = ON_OFF;
            }
           
        }

        public bool SerialPortOpen()
        {
            bool flag_OK = false;
            this.Invoke(new Action(delegate 
            {
                if (PortName.StringIsEmpty() == false) PortName = this.textBox_COM.Text;
                 flag_OK = this.SerialPortOpen(this.textBox_COM.Text, this.comboBox_Baudrate.Text.StringToInt32());
            }));
            return flag_OK;
        }
        public bool SerialPortOpen(string PortName,int BaudRate)
        {
            bool flag_OK = true;
            if (!this.serialPort.IsOpen)
            {
                try
                {
                    this.serialPort.PortName = PortName;
                    this.serialPort.BaudRate = BaudRate;
                    this.serialPort.DataBits = 8;
                    this.serialPort.Parity = System.IO.Ports.Parity.None;
                    this.serialPort.StopBits = System.IO.Ports.StopBits.One;
                    this.serialPort.Open();
                }
                catch
                {
                    flag_OK = false;
                }
            }
            return flag_OK;

        }
        public void SerialPortClose()
        {
            this.serialPort.Close();
        }
        public bool Command_Read_Output(int station, ref int Output)
        {
            bool flag_OK = true;
            if (this.SerialPortOpen())
            {
                byte CRC_L = 0;
                byte CRC_H = 0;
                int start_channel = 8 * 0;
                int channelNum = 8 * 4;
                List<byte> list_byte = new List<byte>();
                list_byte.Add((byte)station);
                list_byte.Add((byte)(0x01));

                list_byte.Add((byte)(start_channel >> 8));
                list_byte.Add((byte)(start_channel >> 0));

                list_byte.Add((byte)(channelNum >> 8));
                list_byte.Add((byte)(channelNum >> 0));
                Basic.MyConvert.Get_CRC16(list_byte.ToArray(), ref CRC_L, ref CRC_H);
                list_byte.Add(CRC_L);
                list_byte.Add(CRC_H);

                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry > 5)
                        {
                            flag_OK = false;
                            break;
                        }
                        this.UART_RX_BUF.Clear();
                        this.FLAG_UART_RX = false;
                        if (serialPort.IsOpen) serialPort.Write(list_byte.ToArray(), 0, list_byte.Count);
                        MyTimer_UART_TimeOut.TickStop();
                        MyTimer_UART_TimeOut.StartTickTime(200);
                        cnt++;
                    }
                    else if (cnt == 1)
                    {
                        if (MyTimer_UART_TimeOut.IsTimeOut())
                        {
                            retry++;
                            cnt = 0;
                        }
                        if (this.FLAG_UART_RX)
                        {
                            if (this.UART_RX_BUF.Count > 4)
                            {
                                if (this.UART_RX_BUF[0] == station && this.UART_RX_BUF[1] == 0x01)
                                {
                                    int numOfBytes = this.UART_RX_BUF[2] + 5;

                                    if (this.UART_RX_BUF.Count == numOfBytes)
                                    {
                                        if (this.CRC_Check(UART_RX_BUF.ToArray()))
                                        {
                                            Output = 0;
                                            Output |= (this.UART_RX_BUF[3] << (0 * 8));
                                            Output |= (this.UART_RX_BUF[4] << (1 * 8));
                                            Output |= (this.UART_RX_BUF[5] << (2 * 8));
                                            Output |= (this.UART_RX_BUF[6] << (3 * 8));
                                            break;
                                        }
                                    }
                                }
                            }


                        }
                    }
                    if (CycleTime != 0) System.Threading.Thread.Sleep(CycleTime);
                }
            }
            return flag_OK;
        }
        public bool Command_Write_Output(int station, int Output)
        {
            bool flag_OK = true;
            if (this.SerialPortOpen())
            {
                byte CRC_L = 0;
                byte CRC_H = 0;
                int start_channel = 8 * 0;
                int numOfoutputs = 4 * 8;
                List<byte> list_byte = new List<byte>();

                list_byte.Add((byte)station);
                list_byte.Add((byte)(0x0F));
                list_byte.Add((byte)(start_channel >> 8));
                list_byte.Add((byte)(start_channel >> 0));
                list_byte.Add((byte)(numOfoutputs >> 8));
                list_byte.Add((byte)(numOfoutputs >> 0));
                list_byte.Add((byte)(4));
                list_byte.Add((byte)(Output >> 8 * 0));
                list_byte.Add((byte)(Output >> 8 * 1));
                list_byte.Add((byte)(Output >> 8 * 2));
                list_byte.Add((byte)(Output >> 8 * 3));
                Basic.MyConvert.Get_CRC16(list_byte.ToArray(), ref CRC_L, ref CRC_H);
                list_byte.Add(CRC_L);
                list_byte.Add(CRC_H);

                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry > 5)
                        {
                            flag_OK = false;
                            break;
                        }
                        this.UART_RX_BUF.Clear();
                        this.FLAG_UART_RX = false;
                        if (serialPort.IsOpen) serialPort.Write(list_byte.ToArray(), 0, list_byte.Count);
                        MyTimer_UART_TimeOut.TickStop();
                        MyTimer_UART_TimeOut.StartTickTime(200);
                        cnt++;
                    }
                    else if (cnt == 1)
                    {
                        if (MyTimer_UART_TimeOut.IsTimeOut())
                        {
                            retry++;
                            cnt = 0;
                        }
                        if (this.FLAG_UART_RX)
                        {
                            if (this.UART_RX_BUF.Count > 4)
                            {
                                if (this.UART_RX_BUF.Count == 8)
                                {
                                    if (this.UART_RX_BUF[0] == station && this.UART_RX_BUF[1] == 0x0F)
                                    {
                                        if (this.CRC_Check(UART_RX_BUF.ToArray()))
                                        {

                                            break;
                                        }
                                    }
                                }
                            }


                        }
                    }
                    if (CycleTime != 0) System.Threading.Thread.Sleep(CycleTime);
                }
            }
            return flag_OK;
        }
        public bool Command_Read_Input(int station, ref int Input)
        {
            bool flag_OK = true;
            if (this.SerialPortOpen())
            {
                byte CRC_L = 0;
                byte CRC_H = 0;
                List<byte> list_byte = new List<byte>();
                list_byte.Add((byte)station);
                list_byte.Add((byte)(0x03));
                list_byte.Add((byte)(4 >> 8));
                list_byte.Add((byte)(4 >> 0));
                list_byte.Add((byte)(8 >> 8));
                list_byte.Add((byte)(8 >> 0));
                Basic.MyConvert.Get_CRC16(list_byte.ToArray(), ref CRC_L, ref CRC_H);
                list_byte.Add(CRC_L);
                list_byte.Add(CRC_H);

                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry > 5)
                        {
                            flag_OK = false;
                            break;
                        }
                        this.UART_RX_BUF.Clear();
                        this.FLAG_UART_RX = false;
                        if (serialPort.IsOpen) serialPort.Write(list_byte.ToArray(), 0, list_byte.Count);
                        MyTimer_UART_TimeOut.TickStop();
                        MyTimer_UART_TimeOut.StartTickTime(200);
                        cnt++;
                    }
                    else if (cnt == 1)
                    {
                        if (MyTimer_UART_TimeOut.IsTimeOut())
                        {
                            retry++;
                            cnt = 0;
                        }
                        if (this.FLAG_UART_RX)
                        {
                            if (this.UART_RX_BUF.Count > 4)
                            {
                                if (this.UART_RX_BUF[0] == station && this.UART_RX_BUF[1] == 0x03)
                                {
                                    int numOfBytes = this.UART_RX_BUF[2] + 5;

                                    if (this.UART_RX_BUF.Count == numOfBytes)
                                    {
                                        if (this.CRC_Check(UART_RX_BUF.ToArray()))
                                        {
                                            Input = 0;
                                            Input |= (this.UART_RX_BUF[4] << (0 * 8));
                                            Input |= (this.UART_RX_BUF[3] << (1 * 8));
                                            Input |= (this.UART_RX_BUF[6] << (2 * 8));
                                            Input |= (this.UART_RX_BUF[5] << (3 * 8));
                                            break;
                                        }
                                    }
                                }
                            }


                        }
                    }
                    if (CycleTime != 0) System.Threading.Thread.Sleep(CycleTime);
                }
            }
            return flag_OK;
        }
        private bool CRC_Check(int[] array)
        {
            if (array.Length <= 4) return false;
            byte crc_L = 0;
            byte crc_H = 0;
            byte[] bytes = new byte[array.Length - 2];
            for (int i = 0; i < array.Length - 2; i++)
            {
                bytes[i] = (byte)array[i];
            }


            Basic.MyConvert.Get_CRC16(bytes.ToArray(), ref crc_L, ref crc_H);
            if (array[array.Length - 2] == crc_L && array[array.Length - 1] == crc_H) return true;
            else return false;
        }
        #region StreamIO
        [Serializable]
        private class SavePropertyFile
        {
            public List<HCG_485_IO_Basic.SaveClass> List_HCG_485_IO_Basic = new List<HCG_485_IO_Basic.SaveClass>();
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
            this.savePropertyFile.COMName = this.textBox_COM.Text.DeepClone();
            this.savePropertyFile.Baudrate = this.comboBox_Baudrate.Text.DeepClone();
            this.savePropertyFile.List_HCG_485_IO_Basic.Clear();
            for (int i = 0; i < this.HCG_485_IO_Basic.Length; i++)
            {
                this.savePropertyFile.List_HCG_485_IO_Basic.Add(this.HCG_485_IO_Basic[i].GetSaveObject());
            }

            this.StreamName = @".\\HCG_485_IO\\" + _設備名稱 + ".pro";
            Basic.FileIO.SaveProperties(this.savePropertyFile, this.StreamName);
        }
        private void LoadProperties()
        {
            object temp = new object();
            this.StreamName = @".\\HCG_485_IO\\" + _設備名稱 + ".pro";
            if (Basic.FileIO.LoadProperties(ref temp, StreamName))
            {
                if(temp is SavePropertyFile)
                {
                    this.savePropertyFile = (SavePropertyFile)temp;
                    this.textBox_COM.Text = this.savePropertyFile.COMName;
                    this.comboBox_Baudrate.Text = this.savePropertyFile.Baudrate;
                    for (int i = 0; i < this.savePropertyFile.List_HCG_485_IO_Basic.Count; i++)
                    {
                        if(this.savePropertyFile.List_HCG_485_IO_Basic[i] != null)
                        {
                            if(i < this.HCG_485_IO_Basic.Length)
                            {
                                this.HCG_485_IO_Basic[i].LoadObject(this.savePropertyFile.List_HCG_485_IO_Basic[i]);
                            }
              
                        }
              
                    }
                }        
            }
      

        }
        #endregion


        #region Event
        private void plC_Button_Open_btnClick(object sender, EventArgs e)
        {
            if (!this.IsOpen)
            {
                this.IsOpen = this.BoardOpen();
            }
            else this.BoardClose();
        }
        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int byte2read = this.serialPort.BytesToRead;
            for (int i = 0; i < byte2read; i++)
            {
                if (serialPort.IsOpen)
                {
                    this.UART_RX_BUF.Add(this.serialPort.ReadByte());
                }
               
            }
            this.FLAG_UART_RX = true;
        }
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.IsOpen)
            {
                this.SaveProperties();
            }
        }


        #endregion

    }
}
