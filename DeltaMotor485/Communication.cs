using System;
using System.Collections.Generic;
using System.Linq;
using MyUI;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using Basic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Drawing.Drawing2D;
using System.Text;

namespace DeltaMotor485
{
    public enum enum_DI
    {
        SON = 0x01 >> 0,
        CTRG = 0x01 >> 1,
        POS0 = 0x01 >> 2,
        POS1 = 0x01 >> 3,
        POS2 = 0x01 >> 4,
        ARST = 0x01 >> 5,
        PL = 0x01 >> 6,
        NL = 0x01 >> 7,
    }
    public enum enum_DO
    {
        SRDY = 0,
        SON =  1,
        ZSPD =  2,
        TSPD = 3,
        TOPS = 4,
        TQL =  5,
        ALRM =  6,
        BRKR =  7,
        HOME =  8,
        OLW =  9,
        WARN =  10,
    }
    public class Driver_DO
    {
        public bool SRDY { get; set; }
        public bool SON { get; set; }
        public bool ZSPD { get; set; }
        public bool TSPD { get; set; }
        public bool TOPS { get; set; }
        public bool TQL { get; set; }
        public bool ALRM { get; set; }
        public bool BRKR { get; set; }
        public bool HOME { get; set; }
        public bool OLW { get; set; }
        public bool WARN { get; set; }
    }
    public class Communication
    {
        public static int UART_RetryNum = 3;
        public static bool ConsoleWrite = true;
        public static int UART_TimeOut = 1000;

  
        public enum enum_Command
        {
            JOG = 0x040A,
            enable_driver_DI = 0x030C,
            set_driver_DI = 0x040E,
            get_driver_DO = 0x005C,
            set_position = 0x0604,
        }

        static public bool UART_Command_JOG(MySerialPort MySerialPort, byte station, int speed_rpm)
        {
            if (speed_rpm > 5000) speed_rpm = 5000;
            if (speed_rpm == 4999) speed_rpm = 5000;
            if (speed_rpm == 4998) speed_rpm = 5000;
            if (speed_rpm < -5000) speed_rpm = -5000;
            if (speed_rpm == -4999) speed_rpm = -5000;
            if (speed_rpm == -4998) speed_rpm = -5000;
            if (speed_rpm == 0)
            {
                if (UART_Command_JOG_Speed(MySerialPort, station, speed_rpm) == false) return false;
            }
            else if (speed_rpm > 0)
            {
                if (UART_Command_JOG_Speed(MySerialPort, station, speed_rpm) == false) return false;
                if (UART_Command_UJOG(MySerialPort, station) == false) return false;
            }
            else if(speed_rpm < 0)
            {
                if (UART_Command_JOG_Speed(MySerialPort, station, speed_rpm) == false) return false;
                if (UART_Command_DJOG(MySerialPort, station) == false) return false;
            }
            return false;
        }
        static public bool UART_Command_UJOG(MySerialPort MySerialPort, byte station)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = new List<byte>();
                int speed = 4999;
                int command = (int)enum_Command.JOG;
                list_byte.Add(station);
                list_byte.Add((byte)(0x10));
                list_byte.Add((byte)(command >> 8));
                list_byte.Add((byte)(command >> 0));
                list_byte.Add((byte)(0x00));
                list_byte.Add((byte)(0x01));
                list_byte.Add((byte)(0x02));
                list_byte.Add((byte)(speed >> 8));
                list_byte.Add((byte)(speed >> 0));
                ushort CRC = Basic.MyConvert.Get_CRC16(list_byte.ToArray());
                list_byte.Add((byte)(CRC >> 0));
                list_byte.Add((byte)(CRC >> 8));


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            if (ConsoleWrite) Console.Write($"Set data error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
                            flag_OK = false;
                            break;
                        }
                        MySerialPort.ClearReadByte();
                        MySerialPort.WriteByte(list_byte.ToArray());
                        MyTimer_UART_TimeOut.TickStop();
                        MyTimer_UART_TimeOut.StartTickTime(UART_TimeOut);
                        cnt++;
                    }
                    if (cnt == 1)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            flag_OK = false;
                            break;
                        }
                        if (MyTimer_UART_TimeOut.IsTimeOut())
                        {
                            retry++;
                            cnt = 0;
                        }
                        byte[] UART_RX = MySerialPort.ReadByteEx();
                        if (UART_RX != null)
                        {
                            if (ConsoleWrite) Console.Write($"Set Data sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_DJOG(MySerialPort MySerialPort, byte station)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = new List<byte>();
                int speed = 4998;
                int command = (int)enum_Command.JOG;
                list_byte.Add(station);
                list_byte.Add((byte)(0x10));
                list_byte.Add((byte)(command >> 8));
                list_byte.Add((byte)(command >> 0));
                list_byte.Add((byte)(0x00));
                list_byte.Add((byte)(0x01));
                list_byte.Add((byte)(0x02));
                list_byte.Add((byte)(speed >> 8));
                list_byte.Add((byte)(speed >> 0));
                ushort CRC = Basic.MyConvert.Get_CRC16(list_byte.ToArray());
                list_byte.Add((byte)(CRC >> 0));
                list_byte.Add((byte)(CRC >> 8));


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            if (ConsoleWrite) Console.Write($"Set data error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
                            flag_OK = false;
                            break;
                        }
                        MySerialPort.ClearReadByte();
                        MySerialPort.WriteByte(list_byte.ToArray());
                        MyTimer_UART_TimeOut.TickStop();
                        MyTimer_UART_TimeOut.StartTickTime(UART_TimeOut);
                        cnt++;
                    }
                    if (cnt == 1)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            flag_OK = false;
                            break;
                        }
                        if (MyTimer_UART_TimeOut.IsTimeOut())
                        {
                            retry++;
                            cnt = 0;
                        }
                        byte[] UART_RX = MySerialPort.ReadByteEx();
                        if (UART_RX != null)
                        {
                            if (ConsoleWrite) Console.Write($"Set Data sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_JOG_Speed(MySerialPort MySerialPort, byte station, int speed_rpm)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.JOG, speed_rpm);
               

                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            if (ConsoleWrite) Console.Write($"Set data error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
                            flag_OK = false;
                            break;
                        }
                        MySerialPort.ClearReadByte();
                        MySerialPort.WriteByte(list_byte.ToArray());
                        MyTimer_UART_TimeOut.TickStop();
                        MyTimer_UART_TimeOut.StartTickTime(UART_TimeOut);
                        cnt++;
                    }
                    if (cnt == 1)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            flag_OK = false;
                            break;
                        }
                        if (MyTimer_UART_TimeOut.IsTimeOut())
                        {
                            retry++;
                            cnt = 0;
                        }
                        byte[] UART_RX = MySerialPort.ReadByteEx();
                        if (UART_RX != null)
                        {
                            if (ConsoleWrite) Console.Write($"Set Data sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_enable_driver_DI(MySerialPort MySerialPort, byte station, params enum_DI[] enum_DIs)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int value = 0;
                for (int i = 0; i < enum_DIs.Length; i++)
                {
                    value |= (int)enum_DIs[i];
                }
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.enable_driver_DI, value);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            if (ConsoleWrite) Console.Write($"Set data error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
                            flag_OK = false;
                            break;
                        }
                        MySerialPort.ClearReadByte();
                        MySerialPort.WriteByte(list_byte.ToArray());
                        MyTimer_UART_TimeOut.TickStop();
                        MyTimer_UART_TimeOut.StartTickTime(UART_TimeOut);
                        cnt++;
                    }
                    if (cnt == 1)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            flag_OK = false;
                            break;
                        }
                        if (MyTimer_UART_TimeOut.IsTimeOut())
                        {
                            retry++;
                            cnt = 0;
                        }
                        byte[] UART_RX = MySerialPort.ReadByteEx();
                        if (UART_RX != null)
                        {
                            if (ConsoleWrite) Console.Write($"Set Data sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_Set_driver_DI(MySerialPort MySerialPort, byte station , params enum_DI[] enum_DIs)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int value = 0;
                for (int i = 0; i < enum_DIs.Length; i++)
                {
                    value |= (int)enum_DIs[i];
                }
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_driver_DI, value);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            if (ConsoleWrite) Console.Write($"Set data error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
                            flag_OK = false;
                            break;
                        }
                        MySerialPort.ClearReadByte();
                        MySerialPort.WriteByte(list_byte.ToArray());
                        MyTimer_UART_TimeOut.TickStop();
                        MyTimer_UART_TimeOut.StartTickTime(UART_TimeOut);
                        cnt++;
                    }
                    if (cnt == 1)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            flag_OK = false;
                            break;
                        }
                        if (MyTimer_UART_TimeOut.IsTimeOut())
                        {
                            retry++;
                            cnt = 0;
                        }
                        byte[] UART_RX = MySerialPort.ReadByteEx();
                        if (UART_RX != null)
                        {
                            if (ConsoleWrite) Console.Write($"Set Data sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_Get_driver_DO(MySerialPort MySerialPort, byte station, ref Driver_DO driver_DO)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_ReadCommad(station, (int)enum_Command.get_driver_DO , 1);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            if (ConsoleWrite) Console.Write($"Set data error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
                            flag_OK = false;
                            break;
                        }
                        MySerialPort.ClearReadByte();
                        MySerialPort.WriteByte(list_byte.ToArray());
                        MyTimer_UART_TimeOut.TickStop();
                        MyTimer_UART_TimeOut.StartTickTime(UART_TimeOut);
                        cnt++;
                    }
                    if (cnt == 1)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            flag_OK = false;
                            break;
                        }
                        if (MyTimer_UART_TimeOut.IsTimeOut())
                        {
                            retry++;
                            cnt = 0;
                        }
                        byte[] UART_RX = MySerialPort.ReadByteEx();
                        if (UART_RX != null)
                        {
                        
                            if (UART_RX.Length == 7)
                            {
                                int value = (UART_RX[3] << 8) | UART_RX[4];
                                driver_DO.SRDY = ((value >> (int)enum_DO.SRDY) % 2) == 1;
                                driver_DO.SON = ((value >> (int)enum_DO.SON) % 2) == 1;
                                driver_DO.ZSPD = ((value >> (int)enum_DO.ZSPD) % 2) == 1;
                                driver_DO.TSPD = ((value >> (int)enum_DO.TSPD) % 2) == 1;
                                driver_DO.TOPS = ((value >> (int)enum_DO.TOPS) % 2) == 1;
                                driver_DO.TQL = ((value >> (int)enum_DO.TQL) % 2) == 1;
                                driver_DO.ALRM = ((value >> (int)enum_DO.ALRM) % 2) == 1;
                                driver_DO.BRKR = ((value >> (int)enum_DO.BRKR) % 2) == 1;
                                driver_DO.HOME = ((value >> (int)enum_DO.HOME) % 2) == 1;
                                driver_DO.OLW = ((value >> (int)enum_DO.OLW) % 2) == 1;
                                driver_DO.WARN = ((value >> (int)enum_DO.WARN) % 2) == 1;
                                Console.WriteLine($"");
                                Console.WriteLine($"SRDY(伺服Ready) : {driver_DO.SRDY}");
                                Console.WriteLine($"SON(伺服激磁) : {driver_DO.SON}");
                                Console.WriteLine($"ZSPD(零速度檢出) : {driver_DO.ZSPD}");
                                Console.WriteLine($"TSPD(目標速度到達) : {driver_DO.TSPD}");
                                Console.WriteLine($"TOPS(目標位置到達) : {driver_DO.TOPS}");
                                Console.WriteLine($"TQL(扭矩限制中) : {driver_DO.TQL}");
                                Console.WriteLine($"ALRM(錯誤警報) : {driver_DO.ALRM}");
                                Console.WriteLine($"BRKR(剎車控制輸出) : {driver_DO.BRKR}");
                                Console.WriteLine($"HOME(原點復歸完成) : {driver_DO.HOME}");
                                Console.WriteLine($"OLW(馬達過載預警) : {driver_DO.OLW}");
                                Console.WriteLine($"WARN(伺服警告、CW、CCW、EMGS、低點壓等等) : {driver_DO.WARN}");
                                Console.Write($"Set Data sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");

                                Console.WriteLine($"");
                                flag_OK = true;
                                break;
                            }
                            else
                            {
                                retry++;
                                cnt = 0;
                            }
                       
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            MySerialPort.SerialPortClose();
            return flag_OK;
        }


        static public List<byte> Get_WriteCommad(byte station, int start_adress , int value)
        {
            List<int> values = new List<int>();
            values.Add(value);
            return Get_WriteCommad(station, start_adress, values);
        }
        static public List<byte> Get_WriteCommad(byte station ,int start_adress ,List<int> values)
        {
            List<byte> list_byte = new List<byte>();
            int len = values.Count;

            list_byte.Add(station);
            list_byte.Add((byte)(0x10));
            list_byte.Add((byte)(start_adress >> 8));
            list_byte.Add((byte)(start_adress >> 0));
            list_byte.Add((byte)(len >> 8));
            list_byte.Add((byte)(len >> 0));
            list_byte.Add((byte)(len * 2));
            for(int i = 0; i < values.Count; i++)
            {
                list_byte.Add((byte)(values[i] >> 8));
                list_byte.Add((byte)(values[i] >> 0));
            }    
            ushort CRC = Basic.MyConvert.Get_CRC16(list_byte.ToArray());
            list_byte.Add((byte)(CRC >> 0));
            list_byte.Add((byte)(CRC >> 8));

            return list_byte;
        }

        static public List<byte> Get_ReadCommad(byte station, int start_adress , int len)
        {
            if (len == 0) return new List<byte>();
            List<byte> list_byte = new List<byte>();
            list_byte.Add(station);
            list_byte.Add((byte)(0x03));
            list_byte.Add((byte)(start_adress >> 8));
            list_byte.Add((byte)(start_adress >> 0));
            list_byte.Add((byte)(len >> 8));
            list_byte.Add((byte)(len >> 0));      
            ushort CRC = Basic.MyConvert.Get_CRC16(list_byte.ToArray());
            list_byte.Add((byte)(CRC >> 0));
            list_byte.Add((byte)(CRC >> 8));

            return list_byte;
        }
  
    }
}
