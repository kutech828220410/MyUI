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
namespace MyUI
{
    [System.Drawing.ToolboxBitmap(typeof(SerialPort))]
    public partial class PLC_SerialPort : UserControl
    {
        public LowerMachine PLC;
        MyConvert myConvert = new MyConvert();
        private Form Active_Form;
        private string COMName = "";
        private string Baudrate = "";
        private string DataBits = "";
        private string Parity = "";
        private string StopBits = "";
        private MyThread ThreadRS232 = new MyThread("ThreadRS232");
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
        private string _發送旗標 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 發送旗標
        {
            get { return _發送旗標; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _發送旗標 = value;
                else _發送旗標 = "";
            }
        }

        private string _發送起始DATA = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 發送起始DATA
        {
            get { return _發送起始DATA; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z") divice_OK = true;
                }

                if (divice_OK) _發送起始DATA = value;
                else _發送起始DATA = "";
            }
        }

        private string _發送字節數DATA = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 發送字節數DATA
        {
            get { return _發送字節數DATA; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }

                if (divice_OK) _發送字節數DATA = value;
                else _發送字節數DATA = "";
            }
        }

        private string _接收旗標 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 接收旗標
        {
            get { return _接收旗標; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _接收旗標 = value;
                else _接收旗標 = "";
            }
        }
        private string _中文致能旗標 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 中文致能旗標
        {
            get { return _中文致能旗標; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _中文致能旗標 = value;
                else _中文致能旗標 = "";
            }
        }
        private string _Byte發送旗標 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string Byte發送旗標
        {
            get { return _Byte發送旗標; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _Byte發送旗標 = value;
                else _Byte發送旗標 = "";
            }
        }
        private string _接收起始DATA = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 接收起始DATA
        {
            get { return _接收起始DATA; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z") divice_OK = true;
                }

                if (divice_OK) _接收起始DATA = value;
                else _接收起始DATA = "";
            }
        }

        private string _接收字節數上限DATA = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 接收字節數上限DATA
        {
            get { return _接收字節數上限DATA; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }

                if (divice_OK) _接收字節數上限DATA = value;
                else _接收字節數上限DATA = "";
            }
        }
        private string _接收字節數DATA = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 接收字節數DATA
        {
            get { return _接收字節數DATA; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }

                if (divice_OK) _接收字節數DATA = value;
                else _接收字節數DATA = "";
            }
        }
        private string _COMPort開啟旗標 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string COMPort開啟旗標
        {
            get { return _COMPort開啟旗標; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _COMPort開啟旗標 = value;
                else _COMPort開啟旗標 = "";
            }
        }
        private string _COMPort已開啟旗標 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string COMPort已開啟旗標
        {
            get { return _COMPort已開啟旗標; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _COMPort已開啟旗標 = value;
                else _COMPort已開啟旗標 = "";
            }
        }
        #endregion
        public PLC_SerialPort()
        {
            InitializeComponent();
        }
        private void SetLowerMachine(LowerMachine lowerMachine)
        {
            this.PLC = lowerMachine;
        }
        private void SetForm(Form form)
        {
            Active_Form = form;
        }
       
        private void Init()
        {
            Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
            LoadProperties();
            comboBox_COM.DataSource = LadderConnection.TopMachine.GetAllPortname();
            SetComboBoxText(comboBox_COM, savePropertyFile.COMName);
            SetComboBoxText(comboBox_Baudrate, this.Baudrate);
            SetComboBoxText(comboBox_StopBits, this.StopBits);
            SetComboBoxText(comboBox_DataBits, this.DataBits);
            SetComboBoxText(comboBox_Parity, this.Parity);
            exButton_Open.Run();
            PLC.properties.Device.Set_Device(_COMPort開啟旗標, false);
            PLC.properties.Device.Set_Device(_COMPort已開啟旗標, false);
            PLC.properties.Device.Set_Device(_發送旗標, false);
            PLC.properties.Device.Set_Device(_接收旗標, false);

            if (this._發送旗標 != "") PLC.properties.Device.Set_Device(this._發送旗標, "*" + this.Name + "_發送旗標");
            if (this._接收旗標 != "") PLC.properties.Device.Set_Device(this._接收旗標, "*" + this.Name + "_接收旗標");
            if (this._發送起始DATA != "") PLC.properties.Device.Set_Device(this._發送起始DATA, "*" + this.Name + "_發送起始DATA");
            if (this._發送字節數DATA != "") PLC.properties.Device.Set_Device(this._發送字節數DATA, "*" + this.Name + "_發送字節數DATA");
            if (this._中文致能旗標 != "") PLC.properties.Device.Set_Device(this._中文致能旗標, "*" + this.Name + "_中文致能旗標");
            if (this._Byte發送旗標 != "") PLC.properties.Device.Set_Device(this._Byte發送旗標, "*" + this.Name + "_Byte發送旗標");
            if (this._接收起始DATA != "") PLC.properties.Device.Set_Device(this._接收起始DATA, "*" + this.Name + "_接收起始DATA");
            if (this._接收字節數上限DATA != "") PLC.properties.Device.Set_Device(this._接收字節數上限DATA, "*" + this.Name + "_接收字節數上限DATA");
            if (this._接收字節數DATA != "") PLC.properties.Device.Set_Device(this._接收字節數DATA, "*" + this.Name + "_接收字節數DATA");
            if (this._COMPort開啟旗標 != "") PLC.properties.Device.Set_Device(this._COMPort開啟旗標, "*" + this.Name + "_COMPort開啟旗標");
            if (this._COMPort已開啟旗標 != "") PLC.properties.Device.Set_Device(this._COMPort已開啟旗標, "*" + this.Name + "_COMPort已開啟旗標");


            ThreadRS232.Add_Method(Run_RS232);
            ThreadRS232.SetSleepTime(1);
            ThreadRS232.AutoRun(true);
            ThreadRS232.Trigger();
        }
        public void Run(Form form, LowerMachine lowerMachine)
        {
            SetForm(form);
            SetLowerMachine(lowerMachine);
            exButton_Open.Run(lowerMachine);
            Init();
        }
        bool flag_temp0 = false;
        bool bool_中文致能旗標 = false;
        bool bool_Byte發送旗標 = false;
        object object_COMPort開啟旗標 = false;
        object object_COMPort已開啟旗標 = false;
        object object_發送旗標 = false;
        object object_接收旗標 = false;
        object object_中文致能旗標 = false;
        object object_Byte發送旗標 = false;
        object object_發送字節數 = 0;
        object object_接收字節數上限 = 0;
        object object_接收字節數 = 0;
        object object_發送char_temp;
        object object_接收char_temp;
        bool flag_COMPort已開啟旗標_temp = false;
        string 發送str = "";
        int int_temp0 = 0;
        private byte[] buf;
        List<byte> byte_read_buf = new List<byte>();
        List<byte> byte_send_buf = new List<byte>();
        void Run_RS232()
        {
            #region COMPort開啟關閉檢查
            if (exButton_Open.Load_WriteState())
            {
                if (!flag_temp0)
                {
                    PLC.properties.Device.Set_Device(_COMPort開啟旗標, true);
                    flag_temp0 = true;
                }
            }
            else
            {
                flag_temp0 = false;
            }
            PLC_中文致能旗標_Refrsh();
            PLC_Byte發送旗標_Refrsh();
            if (PLC.properties.Device.Get_Device(_COMPort已開啟旗標, out object_COMPort已開啟旗標))
            {
                if (object_COMPort已開啟旗標 is bool)
                {
                    if(flag_COMPort已開啟旗標_temp != (bool)object_COMPort已開啟旗標)
                    {
                        exButton_Open.Set_LoadState((bool)object_COMPort已開啟旗標);
                        flag_COMPort已開啟旗標_temp = (bool)object_COMPort已開啟旗標;
                    }
                                                        
                }
            }
            if (PLC.properties.Device.Get_Device(_COMPort開啟旗標, out object_COMPort開啟旗標))
            {
                if (object_COMPort開啟旗標 is bool)
                {
                    if ((bool)object_COMPort開啟旗標)
                    {
                        if (object_COMPort已開啟旗標 is bool)
                        {
                            if ((bool)object_COMPort已開啟旗標)
                            {
                                CloseComPort();
                            }
                            else
                            {
                                OpenComPort();
                            }                     
                        }                   
                        PLC.properties.Device.Set_Device(_COMPort開啟旗標, false);
                    }
                }
            }
            #endregion
            #region 發送檢查
            if (PLC.properties.Device.Get_Device(_發送旗標, out object_發送旗標))
            {
                if (object_發送旗標 is bool && object_COMPort已開啟旗標 is bool)
                {
                    if((bool)object_發送旗標 && (bool)object_COMPort已開啟旗標)
                    {
                        PLC.properties.Device.Get_Device(_發送字節數DATA, out object_發送字節數);
                        if(object_發送字節數 is int)
                        {
                            發送str = "";
                            byte_send_buf.Clear();
                            for (int i = 0; i < (int)object_發送字節數; i++)
                            {
                                PLC.properties.Device.Get_Device(LadderProperty.DEVICE.DeviceOffset(_發送起始DATA, i), out object_發送char_temp);
                                if (object_發送char_temp is int)
                                {
                                    if (!bool_中文致能旗標 && !bool_Byte發送旗標) 發送str += Convert.ToChar((int)object_發送char_temp);
                                    else if (bool_Byte發送旗標) byte_send_buf.Add((byte)((int)object_發送char_temp));
                                    else if (bool_中文致能旗標) byte_send_buf.Add((byte)((int)object_發送char_temp));
                                  
                                }
                            }
                            if (bool_中文致能旗標) 發送str = System.Text.Encoding.UTF8.GetString(byte_send_buf.ToArray());
                            if (!bool_Byte發送旗標) serialPort.Write(發送str);
                            else
                            {
                                serialPort.Write(byte_send_buf.ToArray(), 0, byte_send_buf.Count);
                            }
                            
                        }
                    }
                    PLC.properties.Device.Set_Device(_發送旗標, false);
                }
            }
            #endregion
         
        }
        void PLC_中文致能旗標_Refrsh()
        {
            if (PLC.properties.Device.Get_Device(_中文致能旗標, out object_中文致能旗標))
            {
                if (object_中文致能旗標 is bool)
                {
                    bool_中文致能旗標 = (bool)object_中文致能旗標; ;
                }
            }
        }
        void PLC_Byte發送旗標_Refrsh()
        {
            if (PLC.properties.Device.Get_Device(_Byte發送旗標, out object_Byte發送旗標))
            {
                if (object_Byte發送旗標 is bool)
                {
                    bool_Byte發送旗標 = (bool)object_Byte發送旗標; ;
                }
            }
        }
        public void CloseComPort()
        {
            serialPort.Close();
            this.Invoke(new Action(delegate
            {
                comboBox_COM.Enabled = true;
                comboBox_Baudrate.Enabled = true;
                comboBox_StopBits.Enabled = true;
                comboBox_DataBits.Enabled = true;
                comboBox_Parity.Enabled = true;
            }));
            PLC.properties.Device.Set_Device(_COMPort已開啟旗標, false);
        }
        public void OpenComPort()
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
                    if(int.TryParse(this.Baudrate,out temp))
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
            PLC_中文致能旗標_Refrsh();
            if (bool_中文致能旗標) serialPort.Encoding = System.Text.Encoding.UTF8; 
            try
            {
                serialPort.Open();
                this.Invoke(new Action(delegate
                {
                    comboBox_COM.Enabled = false;
                    comboBox_Baudrate.Enabled = false;
                    comboBox_StopBits.Enabled = false;
                    comboBox_DataBits.Enabled = false;
                    comboBox_Parity.Enabled = false;
                }));
                PLC.properties.Device.Set_Device(_COMPort已開啟旗標, true);
            }
            catch
            {
                PLC.properties.Device.Set_Device(_COMPort已開啟旗標, false);
            }
        }
        public string ReadString()
        {
            string out_string = "";
            if (bool_中文致能旗標)
            {
                out_string = System.Text.Encoding.UTF8.GetString(byte_read_buf.ToArray());
            }
            else
            {
                for (int i = 0; i < byte_read_buf.Count; i++)
                {
                    out_string += myConvert.Int32ToASCII((int)byte_read_buf[i]);
                }

            }
            return out_string;
        }
        public void ReadBufferClear()
        {
            byte_read_buf.Clear();
            PLC.properties.Device.Set_Device(_接收旗標, false);
        }
        public void WriteString(string str)
        {
            PLC.properties.Device.Set_Device(_發送旗標, false);
            if (bool_中文致能旗標)
            {
                byte[] byte_temp = System.Text.Encoding.UTF8.GetBytes(str); 
                for(int i = 0; i < byte_temp.Length; i++)
                {
                    byte_send_buf.Add(byte_temp[i]);
                }
            }
            else
            {
                char[] char_temp = str.ToCharArray();
                for (int i = 0; i < char_temp.Length; i++)
                {
                    byte_send_buf.Add((byte)char_temp[i]);
                }

            }
            if (!bool_中文致能旗標 && !bool_Byte發送旗標)
            {
                foreach(byte _byte in byte_send_buf)
                {
                    發送str += Convert.ToChar((int)_byte);
                }
            }

            if (bool_中文致能旗標) 發送str = System.Text.Encoding.UTF8.GetString(byte_send_buf.ToArray());
            if (!bool_Byte發送旗標) serialPort.Write(發送str);
            else
            {
                serialPort.Write(byte_send_buf.ToArray(), 0, byte_send_buf.Count);
            }
        }
        private void SetComboBoxText(ComboBox combobox, string str)
        {
            for (int i = 0; i < combobox.Items.Count; i++)
            {
                if(combobox.Items[i].ToString() == str)
                {
                    this.Invoke(new Action(delegate
                    {
                        combobox.SelectedIndex = i;
                    }));
                    break;
                }
            }
        }
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
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseComPort();
            ThreadRS232.Stop();
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
            #region 接收檢查
            if (object_COMPort已開啟旗標 is bool)
            {
                if ((bool)object_COMPort已開啟旗標)
                {
                    PLC.properties.Device.Get_Device(_接收旗標, out object_接收旗標);
                    if (!(bool)object_接收旗標)
                    {
                        byte_read_buf.Clear();
                    }
                    else
                    {

                    }
                    int_temp0 = serialPort.BytesToRead;
                    if (int_temp0 > 0)
                    {
                        buf = new byte[int_temp0];
                        serialPort.Read(buf, 0, int_temp0);
                        for (int i = 0; i < buf.Length; i++)
                        {
                            byte_read_buf.Add(buf[i]);
                        }
         
                        PLC.properties.Device.Get_Device(_接收字節數上限DATA, out object_接收字節數上限);
                        if (object_接收字節數上限 is int)
                        {
                            for (int i = 0; i < (int)object_接收字節數上限; i++)
                            {
                                if (i < byte_read_buf.Count)
                                {
                                    PLC.properties.Device.Set_Device(LadderProperty.DEVICE.DeviceOffset(_接收起始DATA, i), (int)byte_read_buf[i]);

                                }
                            }
                            PLC.properties.Device.Set_Device(_接收字節數DATA, byte_read_buf.Count);
                            PLC.properties.Device.Set_Device(_接收旗標, true);
                        }
                    }
                }
            }

            #endregion
        }

    }
}
