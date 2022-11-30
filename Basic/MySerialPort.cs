using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
namespace Basic
{
    public class MySerialPort
    {
        public bool IsConnected
        {
            get
            {
                return serialPort.IsOpen;
            }
        }
        public bool ConsoleWrite = false;
        public string PortName
        {
            get
            {
                return this.serialPort.PortName;
            }
            set
            {
                this.SerialPortClose();
                this.serialPort.PortName = value;
            }
        }
        public int BaudRate
        {
            get
            {
                return this.serialPort.BaudRate;
            }
            set
            {
                this.serialPort.BaudRate = value;
            }

        }
        public int DataBits
        {
            get
            {
                return this.serialPort.DataBits;
            }
            set
            {
                this.serialPort.DataBits = value;
            }

        }
        public System.IO.Ports.Parity Parity
        {
            get
            {
                return this.serialPort.Parity;
            }
            set
            {
                this.serialPort.Parity = value;
            }
        }
        public System.IO.Ports.StopBits StopBits
        {
            get
            {
                return this.serialPort.StopBits;
            }
            set
            {
                this.serialPort.StopBits = value;
            }
        }
        private SerialPort serialPort = new SerialPort();
        private bool FLAG_UART_RX = false;
        private List<byte> UART_RX_BUF = new List<byte>();
        private MyUI.MyTimer MyTimer_RX_Timeout = new MyUI.MyTimer();
        public void Init(string PortName, int BaudRate, int DataBits, System.IO.Ports.Parity Parity, System.IO.Ports.StopBits StopBits)
        {
            this.Init(PortName, BaudRate, DataBits, Parity, StopBits, true);
        }
        public void Init(string PortName, int BaudRate, int DataBits, System.IO.Ports.Parity Parity, System.IO.Ports.StopBits StopBits, bool AutoConnect)
        {
            this.PortName = PortName;
            this.BaudRate = BaudRate;
            this.DataBits = DataBits;
            this.Parity = Parity;
            this.StopBits = StopBits;
            this.serialPort.ReadBufferSize = 4096;
            this.serialPort.WriteBufferSize = 4096;
            this.serialPort.DataReceived += SerialPort_DataReceived;

            this.RX_TickRST();

            if (AutoConnect) this.SerialPortOpen();
        }
        public bool SerialPortOpen()
        {
            this.serialPort.Close();
            try
            {
                this.serialPort.Open();
            }
            catch
            {
                if (ConsoleWrite) Console.Write($"{PortName} open failed!/n");
                return false;
            }
            return true;

        }
        public void SerialPortClose()
        {
            this.serialPort.Close();
        }
        public string ReadString()
        {
            return this.ReadString("UTF-8");
        }
        public string ReadString(string endcoding)
        {
            byte[] read_bytes = this.ReadByte();
            if (read_bytes != null)
            {
                return System.Text.Encoding.GetEncoding(endcoding).GetString(read_bytes);
            }
            return null;

        }
        public byte[] ReadByte()
        {
            if (FLAG_UART_RX && MyTimer_RX_Timeout.IsTimeOut())
            {
                return this.UART_RX_BUF.ToArray();
            }
            else
            {
                return null;
            }

        }
        public void WriteString(string value)
        {
            this.WriteString(value, "UTF-8");
        }
        public void WriteString(string value, string endcoding)
        {
            byte[] write_bytes = System.Text.Encoding.GetEncoding(endcoding).GetBytes(value);
            this.WriteByte(write_bytes);
        }
        public void WriteByte(byte[] bytes)
        {
            this.serialPort.Write(bytes, 0, bytes.Length);
        }
        public void ClearReadByte()
        {
            this.UART_RX_BUF.Clear();
            this.FLAG_UART_RX = false;
        }

        private void RX_TickRST()
        {
            this.MyTimer_RX_Timeout.TickStop();
            this.MyTimer_RX_Timeout.StartTickTime(10);

        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (ConsoleWrite) Console.Write($"{PortName} 讀取到{this.serialPort.BytesToRead}長度資料!\n");
            int byte2read = this.serialPort.BytesToRead;
            for (int i = 0; i < byte2read; i++)
            {
                this.UART_RX_BUF.Add((byte)this.serialPort.ReadByte());
            }
            this.RX_TickRST();
            this.FLAG_UART_RX = true;
        }
    }
}
