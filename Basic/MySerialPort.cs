﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
namespace Basic
{
    public class MySerialPort
    {
        public bool keyBoardMode = false;
        private MyThread myThread;
        public bool RecvFinished
        {
            get
            {
                return (serialPort.BytesToRead == 0);
            }
        }
        public bool IsConnected
        {
            get
            {
                if (keyBoardMode) return true;
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
        private bool FLAG_SET_READBYTE_RT_UART_RX = false;
        public int BufferSize = 20480;
        private List<byte> UART_RX_BUF = new List<byte>();
        private byte[] UART_RX_bytes;
        public int BytesToRead = 0;
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
            this.serialPort.ReadBufferSize = BufferSize;
            this.serialPort.WriteBufferSize = BufferSize;
            this.serialPort.DataReceived += SerialPort_DataReceived;
            UART_RX_bytes = new byte[BufferSize];
            this.RX_TickRST();
            //myThread = new MyThread();
            //myThread.AutoRun(true);
            //myThread.SetSleepTime(1);
            //myThread.Add_Method(DataReceived);
            //myThread.Trigger();

            if (AutoConnect) this.SerialPortOpen();
        }
        public bool SerialPortOpen()
        {
            if (IsConnected) return true;
            this.serialPort.Close();
            try
            {
                this.serialPort.Open();
            }
            catch
            {
                if (ConsoleWrite) Console.Write($"{PortName} open failed!\n");
                return false;
            }
            return true;

        }
        public void SerialPortClose()
        {
            this.serialPort.Close();
        }

        public void SetReadByte(byte[] bytes)
        {
            this.UART_RX_bytes = bytes;
            keyBoardMode = true;
            FLAG_SET_READBYTE_RT_UART_RX = true;
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
            if ((FLAG_UART_RX && MyTimer_RX_Timeout.IsTimeOut())|| FLAG_SET_READBYTE_RT_UART_RX)
            {
                return UART_RX_bytes;
            }
            else
            {
                return null;
            }

        }

        public byte[] ReadByteEx()
        {
            if (FLAG_UART_RX && MyTimer_RX_Timeout.IsTimeOut())
            {
                List<byte> bytes = new List<byte>();
                for(int i = 0; i < BytesToRead; i++)
                {
                    bytes.Add(UART_RX_bytes[i]);
                }
                return bytes.ToArray();
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
            //this.UART_RX_BUF.Clear();
            //this.FLAG_UART_RX = false;
            try
            {
                lock (UART_RX_bytes)
                {
                    for (int i = 0; i < UART_RX_bytes.Length; i++)
                    {
                        UART_RX_bytes[i] = 0;
                    }
                    BytesToRead = 0;
                    this.FLAG_UART_RX = false;
                    FLAG_SET_READBYTE_RT_UART_RX = false;
                }
            }
            catch
            {

            }
      
              
        }

        private void RX_TickRST()
        {
            this.MyTimer_RX_Timeout.TickStop();
            this.MyTimer_RX_Timeout.StartTickTime(10);

        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            DataReceived();
        }
        private void DataReceived()
        {
            if (!IsConnected) return;
            int byte2read = this.serialPort.BytesToRead;
            if (ConsoleWrite) Console.Write($"{PortName} 讀取到{byte2read}長度資料!\n");
            if (BytesToRead + byte2read >= BufferSize)
            {
                ClearReadByte();
            }
            lock (UART_RX_bytes)
            {
                for (int i = 0; i < byte2read; i++)
                {
                    if (this.serialPort.IsOpen == false) return;
                    UART_RX_bytes[i + BytesToRead] = (byte)this.serialPort.ReadByte();
                }
                BytesToRead = BytesToRead + byte2read;
                this.RX_TickRST();
                this.FLAG_UART_RX = true;
            }
        }
        public static string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }
    }
}
