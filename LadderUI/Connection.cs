using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using System.Reflection;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using LadderProperty;
using LadderUI;
using TCP_Server;
using Basic;
namespace LadderConnection
{
    public class Properties
    {
        static public Tx通訊方式 通訊方式 = new Tx通訊方式();
        static public Tx通訊方式 通訊方式_buf = new Tx通訊方式();
        public enum Tx通訊方式 : int
        {
           Enthernet = 0, SerialPort,
        }
        public DEVICE Device = new DEVICE(true);
        public List<String[]> Program = new List<string[]>();
        public List<String[]> Program_緩衝區 = new List<string[]>();
        public bool Program_緩衝區寫入主程式 = false;
        public List<String[]> Comment = new List<string[]>();
        public List<String[]> Comment_緩衝區 = new List<string[]>();
        public static string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public DEVICE device_system = new DEVICE(500, 500, 100, 100, 20, 66, 0,true);
        public enum TxDataType
        {
            Single, Double
        }
        public TxDataType DataType = new TxDataType();
        public readonly int 通訊逾時時間 = 500;
        static public int BufferSize_Program = 500;
        static public int BufferSize_Comment = 150;
        public readonly string str_通訊測試命令 = "!ConnectionTest";
        public readonly string str_Read命令 = "!READ";
        public readonly string str_Write命令 = "!WRITE";
        public readonly string str_Upload_Program命令 = "!U-P";
        public readonly string str_Upload_Program_Confirm命令 = "!U-P-C";
        public readonly string str_Upload_Comment命令 = "!U-C";
        public readonly string str_Upload_Comment_Confirm命令 = "!U-C-C";
        public readonly string str_Upload_Finally命令 = "!U-Done";
        public readonly string str_Buffer_Program_Clear命令 = "!B-P-Clear";
        public readonly string str_Comment_Clear命令 = "!B-Clear";
        public readonly string str_Buffer_RemoveAt命令 = "!B-RemoveAt";
        public readonly string str_Buffer_Num命令 = "!B-Num";
        public readonly string str_Buffer_Read_Program命令 = "!R-P";
        public readonly string str_Buffer_Read_Comment命令 = "!R-C";
        public readonly string str_Buffer_Get_Comment命令 = "!B-R-Get_Comment";
        public readonly string str_Buffer_Get_Program命令 = "!B-R-Get_Program";
        public readonly string str_Buffer_Check_Program命令 = "!C-P";
        public readonly string str_Buffer_Check_Comment命令 = "!B-C-C";

        public readonly string str_Download_Program命令 = "!D-P";
        public readonly string str_Download_Comment命令 = "!D-C";
  
        public readonly string str_True命令 = "!T";
        public readonly string str_False命令 = "!F";
        public string str_命令空格 = "Ⓐ";
        public string str_起始命令 = "Ⓑ";
        public string str_結束命令 = "Ⓒ";
        public string str_完成命令 = "Ⓓ";
        public string str_程式空格 = "Ⓔ";
        public string str_每段程式空格 = "Ⓕ";
    }
    static public class TopMachine
    {
        static public string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        static public TCP.UDP_Cilent UDP_Cilent;
        static private BackgroundWorker backgroundWorker = new BackgroundWorker();
        static private SerialPort serialPort = new SerialPort();
        static public Properties properties = new Properties();
        
        static private Basic.MyConvert myConvert = new Basic.MyConvert();
        static private bool T0_接收逾時 = false;
        static private bool T1_RS232執行序Unbusy = false;
        static public List<String> str_read_list = new List<string>();
        static private string _str_read = "";
        static private string str_read
        {
            get
            {
                return _str_read;
            }
            set
            {
                _str_read = value;
            }
        }
        static private string str_read_temp0 = "";
        static List<byte> byte_read_buf = new List<byte>();
        static private string _str_send = "";
        static private string str_send
        {
            get
            {
                return _str_send;
            }
            set
            {
                _str_send = value;
            }
        }
        static public string COMPort = "COM1";

        static public void init(SerialPort serialPort)
        {
            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker_DoWork);
            if (!backgroundWorker.IsBusy) backgroundWorker.RunWorkerAsync();
        }
        static public bool OpenSerialPort()
        {
            if (Properties.通訊方式 == Properties.Tx通訊方式.SerialPort)
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Encoding = System.Text.Encoding.UTF8;
                    serialPort.DataBits = 8;
                    serialPort.Parity = Parity.None;
                    serialPort.StopBits = StopBits.One;
                    serialPort.ReadBufferSize = 409600;
                    serialPort.WriteBufferSize = 409600;
                    serialPort.BaudRate = 115200;
                    try
                    {
                        serialPort.Open();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else return true;
            }
            else
            {
                return true;
            }
        }
        static public bool CloseSerialPort()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                return true;
            }
            return false;
        }
        static public string[] GetAllPortname()
        {
            return SerialPort.GetPortNames();
        }
        static public void SetCOM(String COM)
        {
            if (!serialPort.IsOpen)
            {
                COMPort = COM;
                serialPort.PortName = COMPort;
            }
        }
        static public bool GetConnectState()
        {
            return serialPort.IsOpen;
        }
        static public bool 通訊測試()
        {
            if (cnt_通訊測試 == 255)
            {
                cnt_通訊測試 = 1;
                return true;
            }
            return false;
        }

        static public Int32 DataWrite(String Device, Int32 value)
        {
            String str_temp = Device.Substring(0, 1);
            if (DEVICE.TestDevice(Device) && (str_temp == "D" || str_temp == "R" || str_temp == "F" || str_temp == "Z"))
            {
                if (FLAG_Data寫入失敗)
                {
                    FLAG_Data寫入失敗 = false;
                    FLAG_Data寫入完成 = false;
                    return -1;
                }
                else if (cnt_Data寫入 == 255 && !FLAG_Data寫入完成)
                {
                    str_Data寫入_Device = Device;
                    Int32_Data寫入_Value = value;
                    properties.DataType = Properties.TxDataType.Single;
                    cnt_Data寫入 = 1;
                    return 1;
                }
                else if (cnt_Data寫入 == 255 && FLAG_Data寫入完成)
                {
                    FLAG_Data寫入完成 = false;
                    return 255;
                }
            }
            else return -1;                
            return 1;
        }
        static public Int32 DataWrite(String Device, Int64 value)
        {
            String str_temp = Device.Substring(0, 1);
            if (DEVICE.TestDevice(Device) && (str_temp == "D" || str_temp == "R" || str_temp == "F" || str_temp == "Z"))
            {
                if (FLAG_Data寫入失敗)
                {
                    FLAG_Data寫入失敗 = false;
                    FLAG_Data寫入完成 = false;
                    return -1;
                }
                else if (cnt_Data寫入 == 255 && !FLAG_Data寫入完成)
                {
                    str_Data寫入_Device = Device;
                    Int64_Data寫入_Value = value;
                    properties.DataType = Properties.TxDataType.Double;
                    cnt_Data寫入 = 1;
                    return 1;
                }
                else if (cnt_Data寫入 == 255 && FLAG_Data寫入完成)
                {
                    FLAG_Data寫入完成 = false;
                    return 255;
                }
            }
            else return -1;
            return 1;
        }

        static public Int32 DataRead(String Device, ref Int32 value)
        {
            String str_temp = Device.Substring(0, 1);
            if (DEVICE.TestDevice(Device) && (str_temp == "D" || str_temp == "R" || str_temp == "F" || str_temp == "T" || str_temp == "Z"))
            {
                if (FLAG_Data讀取失敗)
                {
                    FLAG_Data讀取失敗 = false;
                    FLAG_Data讀取完成 = false;
                    return -1;
                }
                else if (cnt_Data讀取 == 255 && !FLAG_Data讀取完成)
                {
                    str_Data讀取_Device = Device;          
                    properties.DataType = Properties.TxDataType.Single;
                    cnt_Data讀取 = 1;
                    return 1;
                }
                else if (cnt_Data讀取 == 255 && FLAG_Data讀取完成)
                {
                    FLAG_Data讀取完成 = false;
                    value = Int32_Data讀取_Value;
                    return 255;
                }
            }
            else return -1;
            return 1;
        }
        static public Int32 DataRead(String Device, ref Int64 value)
        {
            String str_temp = Device.Substring(0, 1);
            if (DEVICE.TestDevice(Device) && (str_temp == "D" || str_temp == "R" || str_temp == "F" || str_temp == "Z"))
            {
                if (FLAG_Data讀取失敗)
                {
                    FLAG_Data讀取失敗 = false;
                    FLAG_Data讀取完成 = false;
                    return -1;
                }
                else if (cnt_Data讀取 == 255 && !FLAG_Data讀取完成)
                {
                    str_Data讀取_Device = Device;
                    properties.DataType = Properties.TxDataType.Double;
                    cnt_Data讀取 = 1;
                    return 1;
                }
                else if (cnt_Data讀取 == 255 && FLAG_Data讀取完成)
                {
                    FLAG_Data讀取完成 = false;
                    value = Int64_Data讀取_Value;
                    return 255;
                }
            }
            else return -1;
            return 1;
        }

        static public Int32 DeviceWrite(String Device, bool value)
        {
            String str_temp = Device.Substring(0, 1);
            if (DEVICE.TestDevice(Device) && (str_temp == "X" || str_temp == "Y" || str_temp == "M" || str_temp == "S"))
            {
                if (FLAG_Device寫入失敗)
                {
                    FLAG_Device寫入失敗 = false;
                    FLAG_Device寫入完成 = false;
                    return -1;
                }
                else if (cnt_Device寫入 == 255 && !FLAG_Device寫入完成)
                {
                    str_Device寫入_Device = Device;
                    if (value) str_Device寫入_Value = properties.str_True命令;
                    else if (!value) str_Device寫入_Value = properties.str_False命令;
                    cnt_Device寫入 = 1;
                    return 1;
                }
                else if (cnt_Device寫入 == 255 && FLAG_Device寫入完成)
                {
                    FLAG_Device寫入完成 = false;
                    return 255;
                }
            }
            else return -1;
            return 1;
        }
        static public Int32 DeviceRead(String Device,ref bool value)
        {
            String str_temp = Device.Substring(0, 1);
            if (DEVICE.TestDevice(Device) && (str_temp == "X" || str_temp == "Y" || str_temp == "M" || str_temp == "S" || str_temp == "T"))
            {
                if (FLAG_Device讀取失敗)
                {
                    FLAG_Device讀取失敗 = false;
                    FLAG_Device讀取完成 = false;
                    return -1;
                }
                else if (cnt_Device讀取 == 255 && !FLAG_Device讀取完成)
                {
                    str_Device讀取_Device = Device;
                    cnt_Device讀取 = 1;
                    return 1;
                }
                else if (cnt_Device讀取 == 255 && FLAG_Device讀取完成)
                {
                    if (str_Device讀取_Value == properties.str_True命令) value = true;
                    else if (str_Device讀取_Value == properties.str_False命令) value = false;
                    FLAG_Device讀取完成 = false;
                    return 255;
                }
            }
            else return -1;
            return 1;
        }

        static public Int32 ProgramWrite(ref int Process , bool write_comment)
        {
            Process = 程式寫入_進度;
            if (FLAG_程式寫入失敗)
            {
                FLAG_程式寫入失敗 = false;
                FLAG_程式寫入完成 = false;
                return -1;
            }
            else if (cnt_程式寫入 == 255 && !FLAG_程式寫入完成)
            {
                if (write_comment) FLAG_程式寫入_要寫入註解 = true;
                else FLAG_程式寫入_要寫入註解 = false;
                cnt_程式寫入 = 1;
                return 1;
            }
            else if (cnt_程式寫入 == 255 && FLAG_程式寫入完成)
            {
                FLAG_程式寫入完成 = false;
                return 255;
            }
            return 1;
        }
        static public void ProgramStopWrite()
        {
            FLAG_程式寫入失敗 = false;
            FLAG_程式寫入完成 = false;
            cnt_程式寫入 = 255;
        }
        static public Int32 ProgramRead(ref int Process, bool read_comment)
        {
            Process = 程式讀取_進度;
            if (FLAG_程式讀取失敗)
            {
                FLAG_程式讀取失敗 = false;
                FLAG_程式讀取完成 = false;
                return -1;
            }
            else if (cnt_程式讀取 == 255 && !FLAG_程式讀取完成)
            {
                if (read_comment) FLAG_程式讀取_要讀取註解 = true;
                else FLAG_程式讀取_要讀取註解 = false;
                cnt_程式讀取 = 1;
                return 1;
            }
            else if (cnt_程式讀取 == 255 && FLAG_程式讀取完成)
            {
                FLAG_程式讀取完成 = false;
                return 255;
            }
            return 1;
        }
        static public void ProgramStopRead()
        {
            FLAG_程式讀取失敗 = false;
            FLAG_程式讀取完成 = false;
            cnt_程式讀取 = 255;
        }
        static public Int32 ProgramVerify(ref int Process, ref  List<String> Result, List<String[]> Program)
        {

            Process = 程式比較_進度;
            if (FLAG_程式比較失敗)
            {
                Result = 程式比較_Result.DeepClone();
                FLAG_程式比較失敗 = false;
                FLAG_程式讀取完成 = false;
                FLAG_程式比較完成 = false;
                return -1;
            }
            else if (cnt_程式比較 == 255 && !FLAG_程式比較完成)
            {
                程式比較_Program = Program.DeepClone();
                cnt_程式比較 = 1;
                return 1;
            }
            else if (cnt_程式比較 == 255 && FLAG_程式比較完成)
            {
                Result = 程式比較_Result.DeepClone();
                FLAG_程式比較完成 = false;
                FLAG_程式讀取完成 = false;
                return 255;
            }
            return 1;
        }
        static public void ProgramStopVerify()
        {
            FLAG_程式比較完成 = false;
            FLAG_程式讀取完成 = false;
            cnt_程式比較 = 255;
        }

        static private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int running = 0;
            while(true)
            {
                running = 0;
                SystemTimerRefresh();
                if (Properties.通訊方式 == Properties.Tx通訊方式.SerialPort)
                {
                    if (Properties.通訊方式_buf != Properties.通訊方式)
                    {
                        properties.str_命令空格 = "@@";
                        properties.str_起始命令 = "##";
                        properties.str_結束命令 = "$$";
                        properties.str_完成命令 = "%%";
                        properties.str_程式空格 = "^^";
                        properties.str_每段程式空格 = "~~";
                        Properties.BufferSize_Program = 50;
                        Properties.BufferSize_Comment = 1;
                        Properties.通訊方式_buf = Properties.通訊方式;
                    }
                    if (SerialPort_ReadData()) running++;
                    if (SerialPort_ReadDataProcess()) running++; 
                }
                else if (Properties.通訊方式 == Properties.Tx通訊方式.Enthernet)
                {
                    if (Properties.通訊方式_buf != Properties.通訊方式)
                    {
                        properties.str_命令空格 = "Ⓐ";
                        properties.str_起始命令 = "Ⓑ";
                        properties.str_結束命令 = "Ⓒ";
                        properties.str_完成命令 = "Ⓓ";
                        properties.str_程式空格 = "Ⓔ";
                        properties.str_每段程式空格 = "Ⓕ";
                        Properties.BufferSize_Program = 500;
                        Properties.BufferSize_Comment = 150;
                        Properties.通訊方式_buf = Properties.通訊方式;
                    }
              
                    if (Enthernet_ReadDataProcess()) running++; 
                }
              
                if (sub_通訊測試()) running++;
                if (sub_程式寫入()) running++;
                if (sub_程式讀取()) running++;
                if (sub_程式比較()) running++;           
                if (sub_Data寫入()) running++;
                if (sub_Data讀取()) running++;
                if (sub_Device寫入()) running++;
                if (sub_Device讀取()) running++;

                if (Properties.通訊方式 == Properties.Tx通訊方式.SerialPort)
                {
                    if (SerialPort_SendData()) running++;
                }
                else if (Properties.通訊方式 == Properties.Tx通訊方式.Enthernet)
                {
                    if (Enthernet_SendData()) running++;
                }

                if (running != 0)
                {
                    properties.device_system.Set_Device("T1", false);
                    properties.device_system.Set_Device("T1", "K1000", 2);
                    T1_RS232執行序Unbusy = false;
                }
                else
                {
                    properties.device_system.Set_Device("T1", true);
                    properties.device_system.Set_Device("T1", "K1000", 2);
                }
                if (T1_RS232執行序Unbusy)
                {
                    Thread.Sleep(100);
                }
                else
                {
                   // Thread.Sleep(0);
                }
            }
        }
        static private bool SerialPort_ReadData()
        {
            bool flag = false;
            if (GetConnectState())
            {
                int num = serialPort.BytesToRead;
                if (num > 0)
                {
                    byte[] buf = new byte[num];
   
                    serialPort.Read(buf, 0, num);
                    for (int i = 0; i < buf.Length; i++)
                    {
                        byte_read_buf.Add(buf[i]);
                    }
                    
                    flag = true;
                }
            }
      
            return flag;
        }
        static private bool SerialPort_ReadDataProcess()
        {
            bool flag = false;
            str_read_temp0 = "";
            str_read_temp0 = System.Text.Encoding.UTF8.GetString(byte_read_buf.ToArray());
            int index = str_read_temp0.IndexOf(properties.str_完成命令);   
            if (index >= 0)
            {
                index += properties.str_完成命令.Length;
                str_read_temp0 = str_read_temp0.Substring(0, index);
                str_read = str_read_temp0;
                byte_read_buf.RemoveRange(0, index);
                flag = true;
            } 
            return flag;
        }
        static private bool Enthernet_ReadDataProcess()
        {
            bool flag = false;
            str_read_temp0 = "";
            if (TCP.UDP_Cilent.client != null)
            {

                if (TCP.UDP_Cilent.client.Readline(ref str_read_temp0))
                {
                    if (str_read_temp0 == null) str_read_temp0 = "";
                    else
                    {
                        str_read = str_read_temp0;                       
                    }
                    if (str_read_temp0 != "") flag = true;
                }
            }        
            return flag;
        }
        static private bool SerialPort_SendData()
        {
            bool flag = false;
            if (str_send.Length > 0)
            {
                if (GetConnectState())
                {
                    serialPort.Write(str_send);
                    flag = true;

                }            
                str_send = "";
            }
            return flag;
        }
        static private bool Enthernet_SendData()
        {
            bool flag = false;
            if (str_send.Length > 0)
            {
                if (TCP.UDP_Cilent.client != null)
                {
                    TCP.UDP_Cilent.client.Writeline(str_send);
                    flag = true;
                }                                     
                str_send = "";
            }
            return flag;
        }
        static private bool IsSendDataBusy()
        {
            if (str_send.Length >0) return true;
            else return false;
        }
        static object object_temp = new object();
        static private void SystemTimerRefresh()
        {
            properties.device_system.Set_Device("T0", true);
            properties.device_system.Get_Device("T0", out object_temp);
            TopMachine.T0_接收逾時 = (bool)object_temp;

            properties.device_system.Set_Device("T1", true);
            properties.device_system.Get_Device("T1", out object_temp);
            T1_RS232執行序Unbusy = (bool)object_temp;
        }

        #region 通訊測試
        static byte cnt_通訊測試 = 255;
        static byte cnt_通訊測試_重新發送現在值 = 1;
        static byte cnt_通訊測試_重新發送設定值 = 5;
        static string str_通訊測試_視窗顯示文字 = "";
        static bool sub_通訊測試()
        {
            if (cnt_通訊測試 == 1) cnt_通訊測試_00_初始化(ref cnt_通訊測試);
            if (cnt_通訊測試 == 2) cnt_通訊測試_00_通訊PORT開啟(ref cnt_通訊測試);
            if (cnt_通訊測試 == 3) cnt_通訊測試 = 10;

            if (cnt_通訊測試 == 10) cnt_通訊測試_10_檢查次數到達(ref cnt_通訊測試);
            if (cnt_通訊測試 == 11) cnt_通訊測試 = 20;

            if (cnt_通訊測試 == 20) cnt_通訊測試_20_等待發送(ref cnt_通訊測試);
            if (cnt_通訊測試 == 21) cnt_通訊測試_20_等待接收完成命令(ref cnt_通訊測試);
            if (cnt_通訊測試 == 22) cnt_通訊測試 = 150;

            if (cnt_通訊測試 == 150) cnt_通訊測試_150_OK(ref cnt_通訊測試);
            if (cnt_通訊測試 == 151) cnt_通訊測試 = 240;

            if (cnt_通訊測試 == 200) cnt_通訊測試_200_NG(ref cnt_通訊測試);
            if (cnt_通訊測試 == 201) cnt_通訊測試 = 240;

            if (cnt_通訊測試 == 240) cnt_通訊測試_240_關閉通訊PORT(ref cnt_通訊測試);
            if (cnt_通訊測試 == 241) cnt_通訊測試_240_顯示彈出視窗(ref cnt_通訊測試);
            if (cnt_通訊測試 == 242) cnt_通訊測試 = 255;

            if (cnt_通訊測試 == 255) return false;
            else return true;

        }
        static void cnt_通訊測試_00_初始化(ref byte cnt)
        {
            str_通訊測試_視窗顯示文字 = "";
            cnt_通訊測試_重新發送現在值 = 0;
            cnt++;
        }
        static void cnt_通訊測試_00_通訊PORT開啟(ref byte cnt)
        {
            if (Properties.通訊方式 == Properties.Tx通訊方式.SerialPort)
            {
                if (OpenSerialPort())
                {
                    cnt++;
                }
                else
                {
                    cnt = 200;
                }
            }
            else if (Properties.通訊方式 == Properties.Tx通訊方式.Enthernet)
            {
                cnt++;
            }
        }
        static void cnt_通訊測試_10_檢查次數到達(ref byte cnt)
        {
            if (cnt_通訊測試_重新發送現在值 > cnt_通訊測試_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }

        }
        static void cnt_通訊測試_20_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_通訊測試命令;
                str_send += properties.str_結束命令;
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
            }      
        }
        static void cnt_通訊測試_20_等待接收完成命令(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                cnt_通訊測試_重新發送現在值++;
                cnt = 10;
            }
            else if (str_read == properties.str_完成命令.ToString())
            {
                str_read = "";
                cnt++;
            }

        }
        static void cnt_通訊測試_150_OK(ref byte cnt)
        {
            str_通訊測試_視窗顯示文字 = "通訊測試成功!";
            cnt++;
        }
        static void cnt_通訊測試_200_NG(ref byte cnt)
        {
            str_通訊測試_視窗顯示文字 = "通訊測試失敗!";
            cnt++;
        }

        static void cnt_通訊測試_240_關閉通訊PORT(ref byte cnt)
        {
            CloseSerialPort();
            cnt++;
        }
        static void cnt_通訊測試_240_顯示彈出視窗(ref byte cnt)
        {
            cnt++;
            if (str_通訊測試_視窗顯示文字 == "通訊測試成功!")
            {
                MessageBox.Show(str_通訊測試_視窗顯示文字, "Asterisk", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            if (str_通訊測試_視窗顯示文字 == "通訊測試失敗!")
            {
                MessageBox.Show(str_通訊測試_視窗顯示文字, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
       
        }
        static void cnt_通訊測試_(ref byte cnt)
        {
            cnt++;
        }
        #endregion

        #region Data寫入
        static bool FLAG_Data寫入失敗 = false;
        static bool FLAG_Data寫入完成 = false;
        static string str_Data寫入_Device = "";
        static Int32 Int32_Data寫入_Value = 0;
        static Int64 Int64_Data寫入_Value = 0;
        static byte byte_Data寫入_重新發送現在值 = 0;
        static byte byte_Data寫入_重新發送設定值 = 4;
        static string str_Data寫入_視窗顯示文字 = "";
        static byte cnt_Data寫入 = 255;
        static bool sub_Data寫入()
        {
            if (cnt_Data寫入 == 1) cnt_Data寫入_00_初始化(ref cnt_Data寫入);
            if (cnt_Data寫入 == 2) cnt_Data寫入_00_通訊PORT開啟(ref cnt_Data寫入);
            if (cnt_Data寫入 == 3) cnt_Data寫入 = 10;

            if (cnt_Data寫入 == 10) cnt_Data寫入_10_檢查次數到達(ref cnt_Data寫入);
            if (cnt_Data寫入 == 11) cnt_Data寫入_10_檢查DataType(ref cnt_Data寫入);

            if (cnt_Data寫入 == 20) cnt_Data寫入_20_Single_等待發送(ref cnt_Data寫入);
            if (cnt_Data寫入 == 21) cnt_Data寫入_20_Single_等待接收完成命令(ref cnt_Data寫入);
            if (cnt_Data寫入 == 22) cnt_Data寫入 = 150;

            if (cnt_Data寫入 == 30) cnt_Data寫入_30_Double_等待發送(ref cnt_Data寫入);
            if (cnt_Data寫入 == 31) cnt_Data寫入_30_Double_等待接收完成命令(ref cnt_Data寫入);
            if (cnt_Data寫入 == 32) cnt_Data寫入 = 150;

            if (cnt_Data寫入 == 150) cnt_Data寫入_150_OK(ref cnt_Data寫入);
            if (cnt_Data寫入 == 151) cnt_Data寫入 = 240;

            if (cnt_Data寫入 == 200) cnt_Data寫入_200_NG(ref cnt_Data寫入);
            if (cnt_Data寫入 == 201) cnt_Data寫入 = 240;

            if (cnt_Data寫入 == 240) cnt_Data寫入_240_顯示彈出視窗(ref cnt_Data寫入);
            if (cnt_Data寫入 == 241) cnt_Data寫入 = 255;

            if (cnt_Data寫入 == 255) return false;
            else return true;
        }
        static void cnt_Data寫入_00_初始化(ref byte cnt)
        {
            FLAG_Data寫入失敗 = false;
            byte_Data寫入_重新發送現在值 = 0;
            cnt++;
        }

        static void cnt_Data寫入_00_通訊PORT開啟(ref byte cnt)
        {
            if (OpenSerialPort())
            {
                cnt++;
            }
            else
            {
                cnt = 200;
            }
        }
        static void cnt_Data寫入_10_檢查次數到達(ref byte cnt)
        {
            if (byte_Data寫入_重新發送現在值 > byte_Data寫入_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }
        }
        static void cnt_Data寫入_10_檢查DataType(ref byte cnt)
        {
            if(properties.DataType == Properties.TxDataType.Single)
            {
                cnt = 20;
            }
            else if (properties.DataType == Properties.TxDataType.Double)
            {
                cnt = 30;
            }
        }

        static void cnt_Data寫入_20_Single_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_Write命令;
                str_send += properties.str_命令空格 + str_Data寫入_Device;
                str_send += properties.str_命令空格 + Int32_Data寫入_Value.ToString();
                str_send += properties.str_命令空格 + "S";
                str_send += properties.str_結束命令;    
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
            }      
        }
        static void cnt_Data寫入_20_Single_等待接收完成命令(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                byte_Data寫入_重新發送現在值++;
                str_read = "";
                cnt = 10;
            }
            else if (str_read == Int32_Data寫入_Value.ToString()+ properties.str_完成命令)
            {
                str_read = "";
                cnt++;
            }
        }

        static void cnt_Data寫入_30_Double_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_Write命令;
                str_send += properties.str_命令空格 + str_Data寫入_Device;
                str_send += properties.str_命令空格 + Int64_Data寫入_Value.ToString();
                str_send += properties.str_命令空格 + "D";
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
            }
        }
        static void cnt_Data寫入_30_Double_等待接收完成命令(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                byte_Data寫入_重新發送現在值++;
                str_read = "";
                cnt = 10;
            }
            else if (str_read == Int64_Data寫入_Value.ToString() + properties.str_完成命令)
            {
                str_read = "";
                cnt++;
            }
        }

        static void cnt_Data寫入_150_OK(ref byte cnt)
        {
            cnt++;
        }
        static void cnt_Data寫入_200_NG(ref byte cnt)
        {
            FLAG_Data寫入失敗 = true;
            cnt++;
        }
        static void cnt_Data寫入_240_顯示彈出視窗(ref byte cnt)
        {
            FLAG_Data寫入完成 = true;
            cnt++;
        }
        static void cnt_Data寫入_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        #region Data讀取
        static bool FLAG_Data讀取失敗 = false;
        static bool FLAG_Data讀取完成 = false;
        static string str_Data讀取_Device = "";
        static Int32 Int32_Data讀取_Value = 0;
        static Int64 Int64_Data讀取_Value = 0;
        static byte byte_Data讀取_重新發送現在值 = 0;
        static byte byte_Data讀取_重新發送設定值 = 50;
        static string str_Data讀取_視窗顯示文字 = "";
        static byte cnt_Data讀取 = 255;
        static bool sub_Data讀取()
        {
            if (cnt_Data讀取 == 1) cnt_Data讀取_00_初始化(ref cnt_Data讀取);
            if (cnt_Data讀取 == 2) cnt_Data讀取_00_通訊PORT開啟(ref cnt_Data讀取);
            if (cnt_Data讀取 == 3) cnt_Data讀取 = 10;

            if (cnt_Data讀取 == 10) cnt_Data讀取_10_檢查次數到達(ref cnt_Data讀取);
            if (cnt_Data讀取 == 11) cnt_Data讀取_10_檢查DataType(ref cnt_Data讀取);

            if (cnt_Data讀取 == 20) cnt_Data讀取_20_Single_等待發送(ref cnt_Data讀取);
            if (cnt_Data讀取 == 21) cnt_Data讀取_20_Single_等待接收完成命令(ref cnt_Data讀取);
            if (cnt_Data讀取 == 22) cnt_Data讀取 = 150;

            if (cnt_Data讀取 == 30) cnt_Data讀取_30_Double_等待發送(ref cnt_Data讀取);
            if (cnt_Data讀取 == 31) cnt_Data讀取_30_Double_等待接收完成命令(ref cnt_Data讀取);
            if (cnt_Data讀取 == 32) cnt_Data讀取 = 150;

            if (cnt_Data讀取 == 150) cnt_Data讀取_150_OK(ref cnt_Data讀取);
            if (cnt_Data讀取 == 151) cnt_Data讀取 = 240;

            if (cnt_Data讀取 == 200) cnt_Data讀取_200_NG(ref cnt_Data讀取);
            if (cnt_Data讀取 == 201) cnt_Data讀取 = 240;

            if (cnt_Data讀取 == 240) cnt_Data讀取_240_顯示彈出視窗(ref cnt_Data讀取);
            if (cnt_Data讀取 == 241) cnt_Data讀取 = 255;

            if (cnt_Data讀取 == 255) return false;
            else return true;
        }
        static void cnt_Data讀取_00_初始化(ref byte cnt)
        {
            FLAG_Data讀取失敗 = false;
            byte_Data讀取_重新發送現在值 = 0;
            cnt++;
        }

        static void cnt_Data讀取_00_通訊PORT開啟(ref byte cnt)
        {
            if (OpenSerialPort())
            {
                cnt++;
            }
            else
            {
                cnt = 200;
            }
        }
        static void cnt_Data讀取_10_檢查次數到達(ref byte cnt)
        {
            if (byte_Data讀取_重新發送現在值 > byte_Data讀取_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }
        }
        static void cnt_Data讀取_10_檢查DataType(ref byte cnt)
        {
            if (properties.DataType == Properties.TxDataType.Single)
            {
                cnt = 20;
            }
            else if (properties.DataType == Properties.TxDataType.Double)
            {
                cnt = 30;
            }
        }

        static void cnt_Data讀取_20_Single_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_Read命令;
                str_send += properties.str_命令空格 + str_Data讀取_Device;
                str_send += properties.str_命令空格 + "S";
                str_send += properties.str_結束命令;    
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
            }
        }
        static void cnt_Data讀取_20_Single_等待接收完成命令(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                byte_Data讀取_重新發送現在值++;
                str_read = "";
                cnt = 10;
            }
            else if (str_read.IndexOf(properties.str_完成命令) > 0)
            {
                string str_temp = str_read.Replace(properties.str_完成命令.ToString(), "");
                str_read = "";
                if (!Int32.TryParse(str_temp, out Int32_Data讀取_Value))
                {
                    cnt = 200;
                    return;
                }
                cnt++;
                return;
            }
        }

        static void cnt_Data讀取_30_Double_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_Read命令;
                str_send += properties.str_命令空格 + str_Data讀取_Device;
                str_send += properties.str_命令空格 + "D";
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
            }
        }
        static void cnt_Data讀取_30_Double_等待接收完成命令(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                byte_Data讀取_重新發送現在值++;
                str_read = "";
                cnt = 10;
            }
            else if (str_read.IndexOf(properties.str_完成命令) > 0)
            {
                string str_temp = str_read.Replace(properties.str_完成命令.ToString(),"");
                str_read = "";
                if (!Int64.TryParse(str_temp, out Int64_Data讀取_Value))
                {
                    cnt = 200;
                    return;
                }       
                cnt++;
                return;
            }
        }

        static void cnt_Data讀取_150_OK(ref byte cnt)
        {
            cnt++;
        }
        static void cnt_Data讀取_200_NG(ref byte cnt)
        {
            FLAG_Data讀取失敗 = true;
            cnt++;
        }
        static void cnt_Data讀取_240_顯示彈出視窗(ref byte cnt)
        {
            FLAG_Data讀取完成 = true;
            cnt++;
        }
        static void cnt_Data讀取_(ref byte cnt)
        {
            cnt++;
        }
        #endregion

        #region Device寫入
        static bool FLAG_Device寫入失敗 = false;
        static bool FLAG_Device寫入完成 = false;
        static string str_Device寫入_Device = "";
        static String str_Device寫入_Value = "";
        static byte byte_Device寫入_重新發送現在值 = 0;
        static byte byte_Device寫入_重新發送設定值 = 4;
        static string str_Device寫入_視窗顯示文字 = "";
        static byte cnt_Device寫入 = 255;
        static bool sub_Device寫入()
        {
            if (cnt_Device寫入 == 1) cnt_Device寫入_00_初始化(ref cnt_Device寫入);
            if (cnt_Device寫入 == 2) cnt_Device寫入_00_通訊PORT開啟(ref cnt_Device寫入);
            if (cnt_Device寫入 == 3) cnt_Device寫入 = 10;

            if (cnt_Device寫入 == 10) cnt_Device寫入_10_檢查次數到達(ref cnt_Device寫入);
            if (cnt_Device寫入 == 11) cnt_Device寫入 = 20;

            if (cnt_Device寫入 == 20) cnt_Device寫入_20_等待發送(ref cnt_Device寫入);
            if (cnt_Device寫入 == 21) cnt_Device寫入_20_等待接收完成命令(ref cnt_Device寫入);
            if (cnt_Device寫入 == 22) cnt_Device寫入 = 150;

            if (cnt_Device寫入 == 150) cnt_Device寫入_150_OK(ref cnt_Device寫入);
            if (cnt_Device寫入 == 151) cnt_Device寫入 = 240;

            if (cnt_Device寫入 == 200) cnt_Device寫入_200_NG(ref cnt_Device寫入);
            if (cnt_Device寫入 == 201) cnt_Device寫入 = 240;

            if (cnt_Device寫入 == 240) cnt_Device寫入_240_顯示彈出視窗(ref cnt_Device寫入);
            if (cnt_Device寫入 == 241) cnt_Device寫入 = 255;

            if (cnt_Device寫入 == 255) return false;
            else return true;
        }
        static void cnt_Device寫入_00_初始化(ref byte cnt)
        {
            FLAG_Device寫入失敗 = false;
            byte_Device寫入_重新發送現在值 = 0;
            cnt++;
        }

        static void cnt_Device寫入_00_通訊PORT開啟(ref byte cnt)
        {
            if (OpenSerialPort())
            {
                cnt++;
            }
            else
            {
                cnt = 200;
            }
        }
        static void cnt_Device寫入_10_檢查次數到達(ref byte cnt)
        {
            if (byte_Device寫入_重新發送現在值 > byte_Device寫入_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }
        }
   
        static void cnt_Device寫入_20_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_Write命令;
                str_send += properties.str_命令空格 + str_Device寫入_Device;
                str_send += properties.str_命令空格 + str_Device寫入_Value.ToString();
                str_send += properties.str_結束命令;    
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
            }
        }
        static void cnt_Device寫入_20_等待接收完成命令(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                byte_Device寫入_重新發送現在值++;
                str_read = "";
                cnt = 10;
            }
            else if (str_read == str_Device寫入_Value.ToString() + properties.str_完成命令)
            {
                str_read = "";
                cnt++;
            }
        }

        static void cnt_Device寫入_150_OK(ref byte cnt)
        {
            cnt++;
        }
        static void cnt_Device寫入_200_NG(ref byte cnt)
        {
            FLAG_Device寫入失敗 = true;
            cnt++;
        }
        static void cnt_Device寫入_240_顯示彈出視窗(ref byte cnt)
        {
            FLAG_Device寫入完成 = true;
            cnt++;
        }
        static void cnt_Device寫入_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        #region Device讀取
        static bool FLAG_Device讀取失敗 = false;
        static bool FLAG_Device讀取完成 = false;
        static string str_Device讀取_Device = "";
        static String str_Device讀取_Value = "";
        static byte byte_Device讀取_重新發送現在值 = 0;
        static byte byte_Device讀取_重新發送設定值 = 4;
        static string str_Device讀取_視窗顯示文字 = "";
        static byte cnt_Device讀取 = 255;
        static bool sub_Device讀取()
        {
            if (cnt_Device讀取 == 1) cnt_Device讀取_00_初始化(ref cnt_Device讀取);
            if (cnt_Device讀取 == 2) cnt_Device讀取_00_通訊PORT開啟(ref cnt_Device讀取);
            if (cnt_Device讀取 == 3) cnt_Device讀取 = 10;

            if (cnt_Device讀取 == 10) cnt_Device讀取_10_檢查次數到達(ref cnt_Device讀取);
            if (cnt_Device讀取 == 11) cnt_Device讀取 = 20;

            if (cnt_Device讀取 == 20) cnt_Device讀取_20_等待發送(ref cnt_Device讀取);
            if (cnt_Device讀取 == 21) cnt_Device讀取_20_等待接收完成命令(ref cnt_Device讀取);
            if (cnt_Device讀取 == 22) cnt_Device讀取 = 150;

            if (cnt_Device讀取 == 150) cnt_Device讀取_150_OK(ref cnt_Device讀取);
            if (cnt_Device讀取 == 151) cnt_Device讀取 = 240;

            if (cnt_Device讀取 == 200) cnt_Device讀取_200_NG(ref cnt_Device讀取);
            if (cnt_Device讀取 == 201) cnt_Device讀取 = 240;

            if (cnt_Device讀取 == 240) cnt_Device讀取_240_顯示彈出視窗(ref cnt_Device讀取);
            if (cnt_Device讀取 == 241) cnt_Device讀取 = 255;

            if (cnt_Device讀取 == 255) return false;
            else return true;
        }
        static void cnt_Device讀取_00_初始化(ref byte cnt)
        {
            FLAG_Device讀取失敗 = false;
            byte_Device讀取_重新發送現在值 = 0;
            cnt++;
        }

        static void cnt_Device讀取_00_通訊PORT開啟(ref byte cnt)
        {
            if (OpenSerialPort())
            {
                cnt++;
            }
            else
            {
                cnt = 200;
            }
        }
        static void cnt_Device讀取_10_檢查次數到達(ref byte cnt)
        {
            if (byte_Device讀取_重新發送現在值 > byte_Device讀取_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }
        }

        static void cnt_Device讀取_20_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_Read命令;
                str_send += properties.str_命令空格 + str_Device讀取_Device;
                str_send += properties.str_結束命令;    
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
            }
        }
        static void cnt_Device讀取_20_等待接收完成命令(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                byte_Device讀取_重新發送現在值++;
                str_read = "";
                cnt = 10;
            }
            else if ((str_read == properties.str_True命令 + properties.str_完成命令)||(str_read == properties.str_False命令 + properties.str_完成命令))
            {
                str_Device讀取_Value = str_read.Replace(properties.str_完成命令.ToString(), "");
                str_read = "";
                cnt++;
            }
        }

        static void cnt_Device讀取_150_OK(ref byte cnt)
        {
            cnt++;
        }
        static void cnt_Device讀取_200_NG(ref byte cnt)
        {
            FLAG_Device讀取失敗 = true;
            cnt++;
        }
        static void cnt_Device讀取_240_顯示彈出視窗(ref byte cnt)
        {
            FLAG_Device讀取完成 = true;
            cnt++;
        }
        static void cnt_Device讀取_(ref byte cnt)
        {
            cnt++;
        }
        #endregion

        #region 程式寫入
        static bool FLAG_程式寫入失敗 = false;
        static bool FLAG_程式寫入完成 = false;
        static bool FLAG_程式寫入_要寫入註解 = false;

        static byte byte_程式寫入_重新發送現在值 = 0;
        static int byte_程式寫入_重新發送設定值 = 1000;

        static int int_程式寫入_寫入程式現在行數 = 0;
        static int int_程式寫入_寫入程式設定行數 = 0;
        static int int_程式寫入_讀取寫入程式設定行數 = 0;
        static int int_程式寫入_讀取寫入程式現在行數 = 0;
        static int 程式寫入_寫入末段位置 = 0;
        static int int_程式寫入_寫入註解寫入程式設定行數 = 0;
        static int int_程式寫入_寫入註解寫入程式現在行數 = 0;
        static string[][] str_Array_程式寫入;
        static string str_程式寫入 = "";
        static string str_註解寫入 = "";
        static string str_程式寫入_視窗顯示文字 = "";
        static int 程式寫入_進度 = 0;

        static byte cnt_程式寫入 = 255;
        static bool sub_程式寫入()
        {
            if (cnt_程式寫入 == 1) cnt_程式寫入_00_初始化(ref cnt_程式寫入);
            if (cnt_程式寫入 == 2) cnt_程式寫入_00_通訊PORT開啟(ref cnt_程式寫入);
            if (cnt_程式寫入 == 3) cnt_程式寫入 = 5;

            if (cnt_程式寫入 == 5) cnt_程式寫入_05_清除緩衝區_檢查次數到達(ref cnt_程式寫入);
            if (cnt_程式寫入 == 6) cnt_程式寫入_05_清除緩衝區_等待發送(ref cnt_程式寫入);
            if (cnt_程式寫入 == 7) cnt_程式寫入_05_清除緩衝區_等待接收完成命令(ref cnt_程式寫入);
            if (cnt_程式寫入 == 8) cnt_程式寫入 = 10;

            if (cnt_程式寫入 == 10) cnt_程式寫入_10_檢查寫入行數到達(ref cnt_程式寫入);
            if (cnt_程式寫入 == 11) cnt_程式寫入_10_檢查次數到達(ref cnt_程式寫入);
            if (cnt_程式寫入 == 12) cnt_程式寫入 = 20;

            if (cnt_程式寫入 == 20) cnt_程式寫入_20_等待發送(ref cnt_程式寫入);
            if (cnt_程式寫入 == 21) cnt_程式寫入_20_等待接收完成命令(ref cnt_程式寫入);
            if (cnt_程式寫入 == 22) cnt_程式寫入_20_等待發送程式確認命令(ref cnt_程式寫入);
            if (cnt_程式寫入 == 23) cnt_程式寫入_20_等待接收程式確認完成命令(ref cnt_程式寫入);
            if (cnt_程式寫入 == 24) cnt_程式寫入 = 10;

            if (cnt_程式寫入 == 100) cnt_程式寫入_100_讀取緩衝區數量_初始化(ref cnt_程式寫入);
            if (cnt_程式寫入 == 101) cnt_程式寫入_100_讀取緩衝區數量_檢查次數到達(ref cnt_程式寫入);
            if (cnt_程式寫入 == 102) cnt_程式寫入_100_讀取緩衝區數量_等待發送(ref cnt_程式寫入);
            if (cnt_程式寫入 == 103) cnt_程式寫入_100_讀取緩衝區數量_等待接收(ref cnt_程式寫入);
            if (cnt_程式寫入 == 104) cnt_程式寫入_100_讀取緩衝區_初始化(ref cnt_程式寫入);
            if (cnt_程式寫入 == 105) cnt_程式寫入 = 120;

            if (cnt_程式寫入 == 110) cnt_程式寫入_110_讀取緩衝區_檢查讀取行數到達(ref cnt_程式寫入);
            if (cnt_程式寫入 == 111) cnt_程式寫入_110_讀取緩衝區_檢查重新發送次數到達(ref cnt_程式寫入);
            if (cnt_程式寫入 == 112) cnt_程式寫入_110_讀取緩衝區_等待發送(ref cnt_程式寫入);
            if (cnt_程式寫入 == 113) cnt_程式寫入_110_讀取緩衝區_等待接收(ref cnt_程式寫入);

            if (cnt_程式寫入 == 120) cnt_程式寫入_120_程式比對(ref cnt_程式寫入);     
            if (cnt_程式寫入 == 121) cnt_程式寫入 = 160;
      
            if (cnt_程式寫入 == 160) cnt_程式寫入_160_檢查註解要寫入(ref cnt_程式寫入);
            if (cnt_程式寫入 == 161) cnt_程式寫入_160_清除註解_檢查重新發送次數到達(ref cnt_程式寫入);
            if (cnt_程式寫入 == 162) cnt_程式寫入_160_清除註解_等待發送(ref cnt_程式寫入);
            if (cnt_程式寫入 == 163) cnt_程式寫入_160_清除註解_等待接收(ref cnt_程式寫入);
            if (cnt_程式寫入 == 164) cnt_程式寫入_160_註解寫入_初始化(ref cnt_程式寫入);
            if (cnt_程式寫入 == 165) cnt_程式寫入_160_註解寫入_蒐集註解(ref cnt_程式寫入);   
            if (cnt_程式寫入 == 166) cnt_程式寫入 = 130;

            if (cnt_程式寫入 == 130) cnt_程式寫入_130_註解寫入_檢查註解行數(ref cnt_程式寫入);
            if (cnt_程式寫入 == 131) cnt_程式寫入_130_註解寫入_檢查重新發送次數到達(ref cnt_程式寫入);
            if (cnt_程式寫入 == 132) cnt_程式寫入_130_註解寫入_等待發送(ref cnt_程式寫入);
            if (cnt_程式寫入 == 133) cnt_程式寫入_130_註解寫入_等待接收(ref cnt_程式寫入);
            if (cnt_程式寫入 == 134) cnt_程式寫入_130_註解寫入_註解確認_等待發送(ref cnt_程式寫入);
            if (cnt_程式寫入 == 135) cnt_程式寫入_130_註解寫入_註解確認_等待接收(ref cnt_程式寫入);
            if (cnt_程式寫入 == 136) cnt_程式寫入 = 130;

            if (cnt_程式寫入 == 140) cnt_程式寫入_140_緩衝區寫入主程式_初始化(ref cnt_程式寫入);
            if (cnt_程式寫入 == 141) cnt_程式寫入 = 145;

            if (cnt_程式寫入 == 145) cnt_程式寫入_145_緩衝區寫入主程式_檢查次數到達(ref cnt_程式寫入);
            if (cnt_程式寫入 == 146) cnt_程式寫入_145_緩衝區寫入主程式_等待發送(ref cnt_程式寫入);
            if (cnt_程式寫入 == 147) cnt_程式寫入_145_緩衝區寫入主程式_等待接收(ref cnt_程式寫入);
            if (cnt_程式寫入 == 148) cnt_程式寫入 = 150;

     

            if (cnt_程式寫入 == 150) cnt_程式寫入_150_OK(ref cnt_程式寫入);
            if (cnt_程式寫入 == 151) cnt_程式寫入 = 240;

            if (cnt_程式寫入 == 200) cnt_程式寫入_200_NG(ref cnt_程式寫入);
            if (cnt_程式寫入 == 201) cnt_程式寫入 = 240;

            if (cnt_程式寫入 == 240) cnt_程式寫入_240_顯示彈出視窗(ref cnt_程式寫入);
            if (cnt_程式寫入 == 241) cnt_程式寫入 = 255;

            if (cnt_程式寫入 == 255) return false;
            else return true;
        }
        static void cnt_程式寫入_00_初始化(ref byte cnt)
        {
            FLAG_程式寫入失敗 = false;
            byte_程式寫入_重新發送現在值 = 0;
            str_程式寫入 = "";
            str_Array_程式寫入 = properties.Program.ToArray();
            int_程式寫入_寫入程式現在行數 = 0;
            int_程式寫入_寫入程式設定行數 = str_Array_程式寫入.Length;
            程式寫入_進度 = 0;
            if (int_程式寫入_寫入程式設定行數==0)
            {
                cnt = 150;
                return;
            }
            cnt++;
        }
        static void cnt_程式寫入_00_通訊PORT開啟(ref byte cnt)
        {
            if (Properties.通訊方式 == Properties.Tx通訊方式.SerialPort)
            {
                if (OpenSerialPort())
                {
                    cnt++;
                    byte_程式寫入_重新發送現在值 = 0;
                }
                else
                {
                    cnt = 200;
                }
            }
            else if (Properties.通訊方式 == Properties.Tx通訊方式.Enthernet)
            {
                cnt++;
            }
          
        }

        static void cnt_程式寫入_05_清除緩衝區_檢查次數到達(ref byte cnt)
        {
            if (byte_程式寫入_重新發送現在值 > byte_程式寫入_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }
        }
        static void cnt_程式寫入_05_清除緩衝區_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_Buffer_Program_Clear命令;
                str_send += properties.str_結束命令;      
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;
      
            }
        }
        static void cnt_程式寫入_05_清除緩衝區_等待接收完成命令(ref byte cnt)
        {

            if (T0_接收逾時)
            {
                str_read = "";
                cnt = 200;
                return;
            }
            else if (str_read ==  properties.str_完成命令.ToString())
            {
                str_read = "";
                cnt++;
                return;
            }
     
        }

        static void cnt_程式寫入_10_檢查寫入行數到達(ref byte cnt)
        {
            程式寫入_進度 = (int)(((double)int_程式寫入_寫入程式現在行數 / (double)int_程式寫入_寫入程式設定行數) * 100);
            if (程式寫入_進度 < 0) 程式寫入_進度 = 0;
            if (程式寫入_進度 > 100) 程式寫入_進度 = 100;

           if(int_程式寫入_寫入程式現在行數==int_程式寫入_寫入程式設定行數)
           {
               cnt = 100;
               return;
           }
           else if (int_程式寫入_寫入程式現在行數 > int_程式寫入_寫入程式設定行數)
           {
               cnt = 100;
               return;
           }
           else
           {
               cnt++;
               return;
           }
        }
        static void cnt_程式寫入_10_檢查次數到達(ref byte cnt)
        {
            if (byte_程式寫入_重新發送現在值 > byte_程式寫入_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                str_程式寫入 = "";
    
                cnt++;
            }
        }

        static void cnt_程式寫入_20_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                string[] str_current_program;
                string str_temp0;
                str_程式寫入 = "";
                程式寫入_寫入末段位置 = ((int_程式寫入_寫入程式現在行數 / Properties.BufferSize_Program) + 1) * Properties.BufferSize_Program;
                if (程式寫入_寫入末段位置 > int_程式寫入_寫入程式設定行數) 程式寫入_寫入末段位置 = int_程式寫入_寫入程式設定行數;
                for (int i = int_程式寫入_寫入程式現在行數; i < 程式寫入_寫入末段位置; i++)
                {
                    str_current_program = str_Array_程式寫入[i];
                    str_temp0 = "";
                    for (int k = 0; k < str_current_program.Length; k++)
                    {
                        if (k == 0) str_temp0 += str_current_program[k];
                        else str_temp0 += properties.str_程式空格 + str_current_program[k];
                    }
                    if (i != 0) str_程式寫入 = str_程式寫入 + properties.str_每段程式空格 + str_temp0;
                    else str_程式寫入 = str_程式寫入 + str_temp0;
                }

                str_send = properties.str_Upload_Program命令;
                str_send += properties.str_命令空格.ToString() + str_程式寫入;
                str_send += properties.str_命令空格.ToString() + properties.str_結束命令;              
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;                          
            }
            
        }
        static void cnt_程式寫入_20_等待接收完成命令(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                byte_程式寫入_重新發送現在值++;
                str_read = "";
                cnt = 10;
                return;
            }
            else if (str_read != "")
            {
                if (str_read == str_程式寫入 + properties.str_完成命令.ToString())
                {
                    str_read = "";
                    cnt++;
                    return;
                }
                else
                {
                    str_read = "";
                    byte_程式寫入_重新發送現在值++;
                    cnt = 10;
                    return;
                }
            }              
        }
        static void cnt_程式寫入_20_等待發送程式確認命令(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_Upload_Program_Confirm命令;
                str_send += properties.str_結束命令;
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;
            }
        }
        static void cnt_程式寫入_20_等待接收程式確認完成命令(ref byte cnt)
        {

            if (T0_接收逾時)
            {
                str_read = "";
                cnt = 10;
                return;
            }
            else if (str_read != "")
            {
                if (str_read == properties.str_完成命令.ToString())
                {
                    str_read = "";
                    byte_程式寫入_重新發送現在值 = 0;
                    int_程式寫入_寫入程式現在行數 = ((int_程式寫入_寫入程式現在行數 / Properties.BufferSize_Program) + 1) * Properties.BufferSize_Program;
                    cnt = 10;
                    return;
                }      
            }
        }

        static void cnt_程式寫入_100_讀取緩衝區數量_初始化(ref byte cnt)
        {
            byte_程式寫入_重新發送現在值 = 0;
            cnt++;
        }
        static void cnt_程式寫入_100_讀取緩衝區數量_檢查次數到達(ref byte cnt)
        {
            if (byte_程式寫入_重新發送現在值 > byte_程式寫入_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }
        }
        static void cnt_程式寫入_100_讀取緩衝區數量_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_Buffer_Num命令;
                str_send += properties.str_結束命令; 
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;
            }
            cnt++;
        }
        static void cnt_程式寫入_100_讀取緩衝區數量_等待接收(ref byte cnt)
        {
            
            if (T0_接收逾時)
            {
                byte_程式寫入_重新發送現在值++;
                str_read = "";
                cnt = 100;
                return;
            }
            if (str_read != "")
            {
                if (str_read.IndexOf(properties.str_完成命令.ToString()) > 0)
                {
                    string str_temp = str_read.Replace(properties.str_完成命令.ToString(), "");
                    str_read = "";
                    if(!Int32.TryParse(str_temp, out int_程式寫入_讀取寫入程式設定行數))
                    {
                        byte_程式寫入_重新發送現在值++;
                        cnt = 100;
                        return;
                    }
                  
                  
                    cnt ++;
                    return;
                }
            }
    
        }
        static void cnt_程式寫入_100_讀取緩衝區_初始化(ref byte cnt)
        {
            properties.Program_緩衝區.Clear();
            byte_程式寫入_重新發送現在值 = 0;
            int_程式寫入_讀取寫入程式現在行數 = 0;
            cnt++;
        }

        static void cnt_程式寫入_110_讀取緩衝區_檢查讀取行數到達(ref byte cnt)
        {
            程式寫入_進度 = (int)(((double)int_程式寫入_讀取寫入程式現在行數 / (double)int_程式寫入_讀取寫入程式設定行數) * 100);
            if (程式寫入_進度 < 0) 程式寫入_進度 = 0;
            if (程式寫入_進度 > 100) 程式寫入_進度 = 100;
            if (int_程式寫入_讀取寫入程式設定行數 == 0)
            {
                cnt = 120;
                return;
            }
            if (int_程式寫入_讀取寫入程式設定行數 != int_程式寫入_寫入程式設定行數)
            {
                cnt = 200;
                return;
            }
            if (int_程式寫入_讀取寫入程式現在行數 >= int_程式寫入_讀取寫入程式設定行數)
            {
                cnt = 120;
                return;
            }
            else
            {
                cnt++;
                return;
            }
        }
        static void cnt_程式寫入_110_讀取緩衝區_檢查重新發送次數到達(ref byte cnt)
        {
            if (byte_程式寫入_重新發送現在值 > byte_程式寫入_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }
        }
        static void cnt_程式寫入_110_讀取緩衝區_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_Buffer_Read_Program命令;
                str_send += properties.str_命令空格.ToString() + int_程式寫入_讀取寫入程式現在行數.ToString();
                str_send += properties.str_結束命令; 
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;             
            }
        }
        static void cnt_程式寫入_110_讀取緩衝區_等待接收(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                byte_程式寫入_重新發送現在值++;
                str_read = "";
                cnt = 110;
                return;
            }
            if (str_read != "")
            {
                if (str_read.IndexOf(properties.str_完成命令) > 0)
                {
                    str_read = str_read.Replace(properties.str_完成命令.ToString(), "");
                    string[] str_receive_array = myConvert.分解分隔號字串(str_read, properties.str_命令空格);
                    properties.Program_緩衝區.Add(str_receive_array);
                    str_read = "";
                    byte_程式寫入_重新發送現在值 = 0;
                    int_程式寫入_讀取寫入程式現在行數++;
                    cnt = 110;
                    return;
                }
            }
        }

        static void cnt_程式寫入_120_程式比對(ref byte cnt)
        {
            if (properties.Program.Count != int_程式寫入_讀取寫入程式設定行數)
            {
                cnt = 200;
                return;
            }
           /* for (int i = 0; i < properties.Program.Count;i++ )
            {
                string[] str0 = properties.Program[i];
                string[] str1 = properties.Program_緩衝區[i];
                if(str0.Length != str1.Length)
                {
                    cnt = 200;
                    return;
                }
                for (int k = 0; k < str0.Length; k++)
                {
                    string str_temp0 = str0[k];
                    string str_temp1 = str1[k];
                    if(str_temp0 != str_temp1)
                    {
                        cnt = 200;
                        return;
                    }
                }
            }*/
            cnt++; 
        }

        static void cnt_程式寫入_160_檢查註解要寫入(ref byte cnt)
        {
            if (FLAG_程式寫入_要寫入註解)
            {
                cnt++;
                byte_程式寫入_重新發送現在值 = 0;
            }
            else
            {
                cnt = 140;
            }
        }
        static void cnt_程式寫入_160_清除註解_檢查重新發送次數到達(ref byte cnt)
        {
            if (byte_程式寫入_重新發送現在值 > byte_程式寫入_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }
        }
        static void cnt_程式寫入_160_清除註解_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_Comment_Clear命令;
                str_send += properties.str_結束命令;
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;
            }
        }
        static void cnt_程式寫入_160_清除註解_等待接收(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                byte_程式寫入_重新發送現在值++;
                str_read = "";
                cnt = 160;
                return;
            }
            if (str_read != "")
            {
                if (str_read == properties.str_完成命令)
                {
                    str_read = "";
                    byte_程式寫入_重新發送現在值 = 0;
                    cnt++;
                    return;
                }
            }
        }
        static void cnt_程式寫入_160_註解寫入_初始化(ref byte cnt)
        {
            properties.Device = LADDER_Panel.device;
            properties.Comment.Clear();
            int_程式寫入_寫入註解寫入程式現在行數 = 0;
            cnt++;
        }
        static void cnt_程式寫入_160_註解寫入_蒐集註解(ref byte cnt)
        {

            List<string[]> List_str_temp = new List<string[]>();
            List_str_temp = properties.Device.Get_Comment();
            foreach (string[] str_array_temp in List_str_temp)
            {
                if (str_array_temp.Length == 2)
                {
                    string str_temp = str_array_temp[0];
                    str_temp += properties.str_命令空格 + str_array_temp[1];
                    properties.Comment.Add(str_array_temp);
                }
            }
            int_程式寫入_寫入註解寫入程式設定行數 = properties.Comment.Count;
            byte_程式寫入_重新發送現在值 = 0;
            cnt++;
        }

        static void cnt_程式寫入_130_註解寫入_檢查註解行數(ref byte cnt)
        {
            程式寫入_進度 = (int)(((double)int_程式寫入_寫入註解寫入程式現在行數 / (double)int_程式寫入_寫入註解寫入程式設定行數) * 100);
            if (程式寫入_進度 < 0) 程式寫入_進度 = 0;
            if (程式寫入_進度 > 100) 程式寫入_進度 = 100;
            if (int_程式寫入_寫入註解寫入程式現在行數 >= int_程式寫入_寫入註解寫入程式設定行數)
            {
                cnt = 140;
            }
            else
            {
                cnt++;
            }
        }
        static void cnt_程式寫入_130_註解寫入_檢查重新發送次數到達(ref byte cnt)
        {
            if (byte_程式寫入_重新發送現在值 > byte_程式寫入_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }
        }
        static void cnt_程式寫入_130_註解寫入_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                string[] str_current_comment;
                string str_temp0;
                str_註解寫入 = "";
                程式寫入_寫入末段位置 = ((int_程式寫入_寫入註解寫入程式現在行數 / Properties.BufferSize_Comment) + 1) * Properties.BufferSize_Comment;
                if (程式寫入_寫入末段位置 > int_程式寫入_寫入註解寫入程式設定行數) 程式寫入_寫入末段位置 = int_程式寫入_寫入註解寫入程式設定行數;
                for (int i = int_程式寫入_寫入註解寫入程式現在行數; i < 程式寫入_寫入末段位置; i++)
                {
                    str_current_comment = properties.Comment[i];
                    str_temp0 = "";
                    for (int k = 0; k < str_current_comment.Length; k++)
                    {
                        if (k == 0) str_temp0 += str_current_comment[k];
                        else str_temp0 += properties.str_程式空格 + str_current_comment[k];
                    }
                    if (i != 0) str_註解寫入 = str_註解寫入 + properties.str_每段程式空格 + str_temp0;
                    else str_註解寫入 = str_註解寫入 + str_temp0;
                }

                str_send = properties.str_Upload_Comment命令;
                str_send += properties.str_命令空格.ToString() + str_註解寫入;
                str_send += properties.str_命令空格.ToString() + properties.str_結束命令;   

                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;
            }
        }
        static void cnt_程式寫入_130_註解寫入_等待接收(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                str_read = "";
                byte_程式寫入_重新發送現在值++;
                cnt = 130;
                return;
            }
            else if (str_read != "")
            {
                if (str_read == str_註解寫入 + properties.str_完成命令.ToString())
                {
                    str_read = "";
                    cnt++;
                    return;
                }
                else
                {
                    str_read = "";
                    byte_程式寫入_重新發送現在值++;
                    cnt = 130;
                    return;
                }
            }
       
        }
        static void cnt_程式寫入_130_註解寫入_註解確認_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_Upload_Comment_Confirm命令;
                str_send += properties.str_結束命令;
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;
            }
        }
        static void cnt_程式寫入_130_註解寫入_註解確認_等待接收(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                str_read = "";
                cnt = 130;
                return;
            }
            else if (str_read != "")
            {
                if (str_read == properties.str_完成命令.ToString())
                {
                    str_read = "";
                    byte_程式寫入_重新發送現在值 = 0;
                    int_程式寫入_寫入註解寫入程式現在行數 = ((int_程式寫入_寫入註解寫入程式現在行數 / Properties.BufferSize_Comment) + 1) * Properties.BufferSize_Comment;
                    cnt = 130;
                    return;
                }
            } 
        }

        static void cnt_程式寫入_140_緩衝區寫入主程式_初始化(ref byte cnt)
        {
            byte_程式寫入_重新發送現在值 = 0;
            cnt++;
        }

        static void cnt_程式寫入_145_緩衝區寫入主程式_檢查次數到達(ref byte cnt)
        {
            if (byte_程式寫入_重新發送現在值 > byte_程式寫入_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }
        }
        static void cnt_程式寫入_145_緩衝區寫入主程式_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {           
                str_send = properties.str_Upload_Finally命令;
                str_send += properties.str_命令空格;
                if (FLAG_程式寫入_要寫入註解) str_send += properties.str_True命令;
                else str_send += properties.str_False命令;
                str_send += properties.str_結束命令;
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;
            }
        }
        static void cnt_程式寫入_145_緩衝區寫入主程式_等待接收(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                byte_程式寫入_重新發送現在值++;
                str_read = "";
                cnt = 145;
                return;
            }
            else if (str_read == properties.str_完成命令.ToString())
            {
                str_read = "";
                byte_程式寫入_重新發送現在值 = 0;
                int_程式寫入_寫入程式現在行數++;
                cnt++;
                return;
            }
        }

        static void cnt_程式寫入_150_OK(ref byte cnt)
        {
            cnt++;
        }
        static void cnt_程式寫入_200_NG(ref byte cnt)
        {
            FLAG_程式寫入失敗 = true;
            cnt++;
        }
        static void cnt_程式寫入_240_顯示彈出視窗(ref byte cnt)
        {
            FLAG_程式寫入完成 = true;
            cnt++;
        }
        static void cnt_程式寫入_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        #region 程式讀取
        static bool FLAG_程式讀取失敗 = false;
        static bool FLAG_程式讀取完成 = false;
        static bool FLAG_程式讀取_要讀取註解 = false;

        static byte byte_程式讀取_重新發送現在值 = 0;
        static byte byte_程式讀取_重新發送設定值 = 10;
        static int 程式讀取_讀取末段位置 = 0;
        static int int_程式讀取_讀取程式現在行數 = 0;
        static int int_程式讀取_讀取程式設定行數 = 0;
        static int int_程式讀取_讀取註解設定行數 = 0;
        static int int_程式讀取_讀取註解現在行數 = 0;
        static string str_程式讀取 = "";
        static string str_註解讀取 = "";
        static int 程式讀取_進度 = 0;

        static byte cnt_程式讀取 = 255;
        static bool sub_程式讀取()
        {
            if (cnt_程式讀取 == 1) cnt_程式讀取_00_初始化(ref cnt_程式讀取);
            if (cnt_程式讀取 == 2) cnt_程式讀取_00_通訊PORT開啟(ref cnt_程式讀取);
            if (cnt_程式讀取 == 3) cnt_程式讀取 = 10;

            if (cnt_程式讀取 == 10) cnt_程式讀取_10_Buffer區取得程式命令_初始化(ref cnt_程式讀取);
            if (cnt_程式讀取 == 11) cnt_程式讀取 = 15;

            if (cnt_程式讀取 == 15) cnt_程式讀取_15_Buffer區取得程式命令_檢查重新發送次數到達(ref cnt_程式讀取);
            if (cnt_程式讀取 == 16) cnt_程式讀取_15_Buffer區取得程式命令_等待發送(ref cnt_程式讀取);
            if (cnt_程式讀取 == 17) cnt_程式讀取_15_Buffer區取得程式命令_等待接收(ref cnt_程式讀取);
            if (cnt_程式讀取 == 18) cnt_程式讀取 = 20;

            if (cnt_程式讀取 == 20) cnt_程式讀取_20_從緩衝區讀取程式_初始化(ref cnt_程式讀取);
            if (cnt_程式讀取 == 21) cnt_程式讀取 = 25;

            if (cnt_程式讀取 == 25) cnt_程式讀取_25_從緩衝區讀取程式_檢查程式行數到達(ref cnt_程式讀取);
            if (cnt_程式讀取 == 26) cnt_程式讀取_25_從緩衝區讀取程式_檢查重新發送次數到達(ref cnt_程式讀取);
            if (cnt_程式讀取 == 27) cnt_程式讀取_25_從緩衝區讀取程式_讀取_等待發送(ref cnt_程式讀取);
            if (cnt_程式讀取 == 28) cnt_程式讀取_25_從緩衝區讀取程式_讀取_等待接收(ref cnt_程式讀取);
            if (cnt_程式讀取 == 29) cnt_程式讀取_25_從緩衝區讀取程式_重新確認_等待發送(ref cnt_程式讀取);
            if (cnt_程式讀取 == 30) cnt_程式讀取_25_從緩衝區讀取程式_重新確認_等待接收(ref cnt_程式讀取);
            if (cnt_程式讀取 == 31) cnt_程式讀取 = 25;

            if (cnt_程式讀取 == 35) cnt_程式讀取_35_比較程式行數(ref cnt_程式讀取);
            if (cnt_程式讀取 == 36) cnt_程式讀取_35_檢查是否讀取註解(ref cnt_程式讀取);
            if (cnt_程式讀取 == 37) cnt_程式讀取 = 40;

            if (cnt_程式讀取 == 40) cnt_程式讀取_40_Buffer區取得註解_初始化(ref cnt_程式讀取);
            if (cnt_程式讀取 == 41) cnt_程式讀取 = 45;

            if (cnt_程式讀取 == 45) cnt_程式讀取_45_Buffer區取得註解_檢查重新發送次數到達(ref cnt_程式讀取);
            if (cnt_程式讀取 == 46) cnt_程式讀取_45_Buffer區取得註解_等待發送(ref cnt_程式讀取);
            if (cnt_程式讀取 == 47) cnt_程式讀取_45_Buffer區取得註解_等待接收(ref cnt_程式讀取);
            if (cnt_程式讀取 == 48) cnt_程式讀取 = 50;

            if (cnt_程式讀取 == 50) cnt_註解讀取_50_從讀取註解_檢查註解行數到達(ref cnt_程式讀取);
            if (cnt_程式讀取 == 51) cnt_註解讀取_50_從讀取註解_檢查重新發送次數到達(ref cnt_程式讀取);
            if (cnt_程式讀取 == 52) cnt_註解讀取_50_從讀取註解_讀取_等待發送(ref cnt_程式讀取);
            if (cnt_程式讀取 == 53) cnt_註解讀取_50_從讀取註解_讀取_等待接收(ref cnt_程式讀取);
            if (cnt_程式讀取 == 54) cnt_註解讀取_50_從讀取註解_重新確認_等待發送(ref cnt_程式讀取);
            if (cnt_程式讀取 == 55) cnt_註解讀取_50_從讀取註解_重新確認_等待接收(ref cnt_程式讀取);
            if (cnt_程式讀取 == 56) cnt_程式讀取 = 50;

            if (cnt_程式讀取 == 60) cnt_程式讀取 = 150;

            if (cnt_程式讀取 == 150) cnt_程式讀取_150_OK(ref cnt_程式讀取);
            if (cnt_程式讀取 == 151) cnt_程式讀取 = 240;

            if (cnt_程式讀取 == 200) cnt_程式讀取_200_NG(ref cnt_程式讀取);
            if (cnt_程式讀取 == 201) cnt_程式讀取 = 240;

            if (cnt_程式讀取 == 240) cnt_程式讀取_240_顯示彈出視窗(ref cnt_程式讀取);
            if (cnt_程式讀取 == 241) cnt_程式讀取 = 255;

            if (cnt_程式讀取 == 255) return false;
            else return true;
        }
        static void cnt_程式讀取_00_初始化(ref byte cnt)
        {
            FLAG_程式讀取失敗 = false;
            byte_程式讀取_重新發送現在值 = 0;
            str_程式讀取 = "";
            int_程式讀取_讀取程式現在行數 = 0;
            int_程式讀取_讀取程式設定行數 = 0;
            properties.Program.Clear();
            properties.Comment.Clear();
            cnt++;
        }
        static void cnt_程式讀取_00_通訊PORT開啟(ref byte cnt)
        {
            if (Properties.通訊方式 == Properties.Tx通訊方式.SerialPort)
            {
                if (OpenSerialPort())
                {
                    cnt++;
                }
                else
                {
                    cnt = 200;
                }
            }
            else if (Properties.通訊方式 == Properties.Tx通訊方式.Enthernet)
            {
                cnt++;
            }
        }

        static void cnt_程式讀取_10_Buffer區取得程式命令_初始化(ref byte cnt)
        {
            byte_程式讀取_重新發送現在值 = 0;
            cnt++;
        }

        static void cnt_程式讀取_15_Buffer區取得程式命令_檢查重新發送次數到達(ref byte cnt)
        {
            if (byte_程式讀取_重新發送現在值 > byte_程式讀取_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }
        }
        static void cnt_程式讀取_15_Buffer區取得程式命令_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_Buffer_Get_Program命令;
                str_send += properties.str_結束命令;
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;
            }
        }
        static void cnt_程式讀取_15_Buffer區取得程式命令_等待接收(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                byte_程式讀取_重新發送現在值++;
                str_read = "";
                cnt = 15;
                return;
            }
            else if (str_read.IndexOf(properties.str_完成命令.ToString()) > 0)
            {
                string str_temp = str_read.Replace(properties.str_完成命令.ToString(), "");
                str_read = "";
                if (!Int32.TryParse(str_temp, out int_程式讀取_讀取程式設定行數))
                {
                    byte_程式讀取_重新發送現在值++;
                    cnt = 15;
                    return;
                }


                cnt++;
                return;
            }
        }

        static void cnt_程式讀取_20_從緩衝區讀取程式_初始化(ref byte cnt)
        {
            byte_程式讀取_重新發送現在值 = 0;
            int_程式讀取_讀取程式現在行數 = 0;
            cnt++;
        }

        static void cnt_程式讀取_25_從緩衝區讀取程式_檢查程式行數到達(ref byte cnt)
        {
            程式讀取_進度 = (int)(((double)int_程式讀取_讀取程式現在行數 / (double)int_程式讀取_讀取程式設定行數) * 100);
            if (程式讀取_進度 < 0) 程式讀取_進度 = 0;
            if (程式讀取_進度 > 100) 程式讀取_進度 = 100;
            if (int_程式讀取_讀取程式設定行數 == 0)
            {
                cnt = 35;
                return;
            }
      
            if (int_程式讀取_讀取程式現在行數 >= int_程式讀取_讀取程式設定行數)
            {
                cnt = 35;
                return;
            }
            else
            {
                cnt++;
                return;
            }
        }
        static void cnt_程式讀取_25_從緩衝區讀取程式_檢查重新發送次數到達(ref byte cnt)
        {
            if (byte_程式讀取_重新發送現在值 > byte_程式讀取_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }
        }
        static void cnt_程式讀取_25_從緩衝區讀取程式_讀取_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                程式讀取_讀取末段位置 = ((int_程式讀取_讀取程式現在行數 / Properties.BufferSize_Program) + 1) * Properties.BufferSize_Program;
                if (程式讀取_讀取末段位置 > int_程式讀取_讀取程式設定行數) 程式讀取_讀取末段位置 = int_程式讀取_讀取程式設定行數;
                str_send = properties.str_Buffer_Read_Program命令;
                str_send += properties.str_命令空格.ToString() + int_程式讀取_讀取程式現在行數.ToString();
                str_send += properties.str_命令空格.ToString() + 程式讀取_讀取末段位置.ToString();
                str_send += properties.str_結束命令;
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;
            }
        }
        static void cnt_程式讀取_25_從緩衝區讀取程式_讀取_等待接收(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                byte_程式讀取_重新發送現在值++;
                str_read = "";
                cnt = 25;
                return;
            }
            if (str_read != "")
            {
                if (str_read.IndexOf(properties.str_完成命令) > 0)
                {
                    str_程式讀取 = str_read.Replace(properties.str_完成命令.ToString(), "");
                    str_read = "";
                    cnt++;
                    return;
                }
            }
        }
        static void cnt_程式讀取_25_從緩衝區讀取程式_重新確認_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                string str_send_temp = properties.str_Buffer_Check_Program命令;
                str_send_temp += properties.str_命令空格 + int_程式讀取_讀取程式現在行數.ToString();
                str_send_temp += properties.str_命令空格 + 程式讀取_讀取末段位置.ToString();
                str_send_temp += properties.str_命令空格 + str_程式讀取;
                str_send_temp += properties.str_結束命令;
                str_send = str_send_temp;
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;
            }
        }
        static void cnt_程式讀取_25_從緩衝區讀取程式_重新確認_等待接收(ref byte cnt)
        {
           // int_程式讀取_讀取程式現在行數++;
           // cnt++;
           // return;
            if (T0_接收逾時)
            {
                byte_程式讀取_重新發送現在值++;
                str_read = "";
                cnt = 25;
                return;
            }
            if (str_read != "")
            {
                if (str_read == properties.str_True命令 + properties.str_完成命令)
                {
                    string[] str_current_program;
                    string[] str_receive_array = myConvert.分解分隔號字串(str_程式讀取, properties.str_每段程式空格);
                    for (int i = 0; i < str_receive_array.Length; i++)
                    {
                        str_current_program = myConvert.分解分隔號字串(str_receive_array[i], properties.str_程式空格);
                        properties.Program.Add(str_current_program);
                    }
                    int_程式讀取_讀取程式現在行數 = ((int_程式讀取_讀取程式現在行數 / Properties.BufferSize_Program) + 1) * Properties.BufferSize_Program;
                    byte_程式讀取_重新發送現在值 = 0;
                    str_read = "";
                    cnt++;
                }
                else if(str_read == properties.str_False命令 + properties.str_完成命令)
                {        
                    byte_程式讀取_重新發送現在值 = 0;
                    str_read = "";
                    cnt = 25;
                    return;
                }
 
            }
    
        }

        static void cnt_程式讀取_35_比較程式行數(ref byte cnt)
        {
            if (properties.Program.Count != int_程式讀取_讀取程式設定行數)
            {
                cnt = 200;
                return;
            }
            else
            {
                cnt++;
                return;
            }
            
        }
        static void cnt_程式讀取_35_檢查是否讀取註解(ref byte cnt)
        {
            if (FLAG_程式讀取_要讀取註解)
            {
                cnt++;
                return;
            }
            else
            {
                cnt = 150;
                return;
            }
        }

        static void cnt_程式讀取_40_Buffer區取得註解_初始化(ref byte cnt)
        {
            properties.Comment.Clear();
            byte_程式讀取_重新發送現在值 = 0;
            int_程式讀取_讀取註解現在行數 = 0;
            int_程式讀取_讀取註解設定行數 = 0;
            cnt++;
        }

        static void cnt_程式讀取_45_Buffer區取得註解_檢查重新發送次數到達(ref byte cnt)
        {
            if (byte_程式讀取_重新發送現在值 > byte_程式讀取_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }
        }
        static void cnt_程式讀取_45_Buffer區取得註解_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_Buffer_Get_Comment命令;
                str_send += properties.str_結束命令;
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;
            }
        }
        static void cnt_程式讀取_45_Buffer區取得註解_等待接收(ref byte cnt)
        {

            if (T0_接收逾時)
            {
                byte_程式讀取_重新發送現在值++;
                str_read = "";
                cnt = 45;
                return;
            }
            else if (str_read.IndexOf(properties.str_完成命令.ToString()) > 0)
            {
                string str_temp = str_read.Replace(properties.str_完成命令.ToString(), "");
                str_read = "";
                if (!Int32.TryParse(str_temp, out int_程式讀取_讀取註解設定行數))
                {
                    byte_程式讀取_重新發送現在值++;
                    cnt = 45;
                    return;
                }

                Thread.Sleep(10);
                str_read = "";
                cnt++;
                return;
            }
        }

        static void cnt_註解讀取_50_從讀取註解_檢查註解行數到達(ref byte cnt)
        {
            程式讀取_進度 = (int)(((double)int_程式讀取_讀取註解現在行數 / (double)int_程式讀取_讀取註解設定行數) * 100);
            if (程式讀取_進度 < 0) 程式讀取_進度 = 0;
            if (程式讀取_進度 > 100) 程式讀取_進度 = 100;
            if (int_程式讀取_讀取註解設定行數 == 0)
            {
                cnt = 60;
                return;
            }
     
            if (int_程式讀取_讀取註解現在行數 >= int_程式讀取_讀取註解設定行數)
            {
                cnt = 60;
                return;
            }
            else
            {
                cnt++;
                return;
            }
        }
        static void cnt_註解讀取_50_從讀取註解_檢查重新發送次數到達(ref byte cnt)
        {
            if (byte_程式讀取_重新發送現在值 > byte_程式讀取_重新發送設定值)
            {
                cnt = 200;
            }
            else
            {
                cnt++;
            }
        }
        static void cnt_註解讀取_50_從讀取註解_讀取_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                程式讀取_讀取末段位置 = ((int_程式讀取_讀取註解現在行數 / Properties.BufferSize_Comment) + 1) * Properties.BufferSize_Comment;
                if (程式讀取_讀取末段位置 > int_程式讀取_讀取註解設定行數) 程式讀取_讀取末段位置 = int_程式讀取_讀取註解設定行數;
                str_send = properties.str_Buffer_Read_Comment命令;
                str_send += properties.str_命令空格.ToString() + int_程式讀取_讀取註解現在行數.ToString();
                str_send += properties.str_命令空格.ToString() + 程式讀取_讀取末段位置.ToString();
                str_send += properties.str_結束命令;  
                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;
            }
        }
        static string[] str_receive_array;
        static void cnt_註解讀取_50_從讀取註解_讀取_等待接收(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                byte_程式讀取_重新發送現在值++;
                str_read = "";
                cnt = 50;
                return;
            }
            if (str_read != "")
            {
                if (str_read.IndexOf(properties.str_完成命令) > 0)
                {
                    str_註解讀取 = str_read.Replace(properties.str_完成命令.ToString(), "");
                    str_read = "";
                    cnt++;
                    return;
                }
            }
        }
        static void cnt_註解讀取_50_從讀取註解_重新確認_等待發送(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                string str_send_temp = properties.str_Buffer_Check_Comment命令;
                str_send_temp += properties.str_命令空格 + int_程式讀取_讀取註解現在行數.ToString();
                str_send_temp += properties.str_命令空格 + 程式讀取_讀取末段位置.ToString();
                str_send_temp += properties.str_命令空格 + str_註解讀取;
                str_send_temp += properties.str_結束命令;
                str_send = str_send_temp;

                properties.device_system.Set_Device("T0", properties.通訊逾時時間);
                T0_接收逾時 = false;
                cnt++;
                return;
            }
        }
        static void cnt_註解讀取_50_從讀取註解_重新確認_等待接收(ref byte cnt)
        {
            if (T0_接收逾時)
            {
                byte_程式讀取_重新發送現在值++;
                str_read = "";
                cnt = 50;
                return;
            }
            if (str_read != "")
            {
                if (str_read == properties.str_True命令 + properties.str_完成命令)
                {
                    string[] str_current_comment;
                    string[] str_receive_array = myConvert.分解分隔號字串(str_註解讀取, properties.str_每段程式空格);
                    for (int i = 0; i < str_receive_array.Length; i++)
                    {
                        str_current_comment = myConvert.分解分隔號字串(str_receive_array[i], properties.str_程式空格);
                        properties.Comment.Add(str_current_comment);
                    }
                    int_程式讀取_讀取註解現在行數 = ((int_程式讀取_讀取註解現在行數 / Properties.BufferSize_Comment) + 1) * Properties.BufferSize_Comment;
                    str_read = "";
                    cnt++;
                }
                else if (str_read == properties.str_False命令 + properties.str_完成命令)
                {
                    byte_程式讀取_重新發送現在值 = 0;
                    str_read = "";
                    cnt = 50;
                    return;
                }
 
            }

        }

        static void cnt_程式讀取_150_OK(ref byte cnt)
        {
            cnt++;
        }
        static void cnt_程式讀取_200_NG(ref byte cnt)
        {
            FLAG_程式讀取失敗 = true;
            cnt++;
        }
        static void cnt_程式讀取_240_顯示彈出視窗(ref byte cnt)
        {
            FLAG_程式讀取完成 = true;
            cnt++;
        }
        static void cnt_程式讀取_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        #region 程式比較
        static bool FLAG_程式比較失敗 = false;
        static bool FLAG_程式比較完成 = false;
        static List<string[]> 程式比較_Program = new List<string[]>();
        static List<string> 程式比較_Result = new List<string>();
        static int 程式比較_進度 = 0;
        static byte cnt_程式比較 = 255;
        static bool sub_程式比較()
        {
            if (cnt_程式比較 == 1) cnt_程式比較_00_初始化(ref cnt_程式比較);
            if (cnt_程式比較 == 2) cnt_程式比較 = 10;

            if (cnt_程式比較 == 10) cnt_程式比較_10_等待程式讀取_READY(ref cnt_程式比較);
            if (cnt_程式比較 == 11) cnt_程式比較_10_等待程式讀取_OVER(ref cnt_程式比較);
            if (cnt_程式比較 == 12) cnt_程式比較_10_檢查讀取結果(ref cnt_程式比較);
            if (cnt_程式比較 == 13) cnt_程式比較 = 20;

            if (cnt_程式比較 == 20) cnt_程式比較_20_比較程式長度(ref cnt_程式比較);
            if (cnt_程式比較 == 21) cnt_程式比較_20_開始比較(ref cnt_程式比較);
            if (cnt_程式比較 == 22) cnt_程式比較 = 150;

            if (cnt_程式比較 == 150) cnt_程式比較_150_OK(ref cnt_程式比較);
            if (cnt_程式比較 == 151) cnt_程式比較 = 240;

            if (cnt_程式比較 == 200) cnt_程式比較_200_NG(ref cnt_程式比較);
            if (cnt_程式比較 == 201) cnt_程式比較 = 240;

            if (cnt_程式比較 == 240) cnt_程式比較_240_顯示彈出視窗(ref cnt_程式比較);
            if (cnt_程式比較 == 241) cnt_程式比較 = 255;

            if (cnt_程式比較 == 255) return false;
            else return true;
        }
        static void cnt_程式比較_00_初始化(ref byte cnt)
        {
            程式比較_Result.Clear();
            FLAG_程式比較失敗 = false;
            FLAG_程式比較完成 = false;
            程式比較_進度 = 0;
            cnt++;
        }
        static void cnt_程式比較_10_等待程式讀取_READY(ref byte cnt)
        {
            if (cnt_程式讀取 == 255)
            {
                FLAG_程式讀取_要讀取註解 = false;
                cnt_程式讀取 = 1;
                cnt++;
            }    
        }
        static void cnt_程式比較_10_等待程式讀取_OVER(ref byte cnt)
        {
            程式比較_進度 = 程式讀取_進度;
            if (cnt_程式讀取 == 255)
            {
                cnt++;
            }  
        }
        static void cnt_程式比較_10_檢查讀取結果(ref byte cnt)
        {
            if (FLAG_程式讀取失敗)
            {
                程式比較_Result.Add("Load program fail!");
                cnt = 200;
            }
            else
            {
                程式比較_Result.Add("Load program success!");
                cnt++;
            }
        }
        static void cnt_程式比較_20_比較程式長度(ref byte cnt)
        {
            程式比較_Result.Add("Origin program length :" + 程式比較_Program.Count.ToString());
            程式比較_Result.Add("Load program length   :" + properties.Program.Count.ToString());
            if (程式比較_Program.Count != properties.Program.Count)
            {
              
                cnt = 200;
            }
            else cnt++;
        }
        static void cnt_程式比較_20_開始比較(ref byte cnt)
        {
            bool verify_difference = false;
            for (int i = 0; i < 程式比較_Program.Count; i++)
            {
                string[] str_temp_org = 程式比較_Program[i];
                string[] str_temp_load = properties.Program[i];
                if(str_temp_org.Length != str_temp_load.Length)
                {
                    程式比較_Result.Add("'" + i.ToString() + "' " + "line is diffrent");
                    verify_difference = true;
                    continue;
                }
                for(int k = 0 ; k < str_temp_org.Length ; k++)
                {
                    if (str_temp_org[k] != str_temp_load[k])
                    {
                        程式比較_Result.Add("'" + i.ToString() + "' " + "line is diffrent");
                        verify_difference = true;
                        break;
                    }
                 
                }
            }
            if (!verify_difference) cnt++;
            else cnt = 200;
        }
        static void cnt_程式比較_150_OK(ref byte cnt)
        {
            程式比較_Result.Add("Verify program seccess!");
            cnt++;
        }
        static void cnt_程式比較_200_NG(ref byte cnt)
        {
            程式比較_Result.Add("Verify program fail!");
            FLAG_程式比較失敗 = true;
            cnt++;
        }
        static void cnt_程式比較_240_顯示彈出視窗(ref byte cnt)
        {
            FLAG_程式比較完成 = true;
            cnt++;
        }
        static void cnt_程式比較_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
    }
    public class LowerMachine
    {
        static public string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public delegate void MethodDelegate();
        public Properties.Tx通訊方式 通訊方式 = new Properties.Tx通訊方式();
        private List<MethodDelegate> Run_start = new List<MethodDelegate>();
        private List<MethodDelegate> Run_complete = new List<MethodDelegate>();
        private List<MethodDelegate> Run_UI = new List<MethodDelegate>();

        private BackgroundWorker backgroundWorker_Others = new BackgroundWorker();
        private BackgroundWorker backgroundWorker_Program = new BackgroundWorker();
        private BackgroundWorker backgroundWorker_UI = new BackgroundWorker();
        private BackgroundWorker backgroundWorker_RS232 = new BackgroundWorker();
        private bool backgroundWorker_Others_Alive = true;
        private bool backgroundWorker_Program_Alive = true;
        private bool backgroundWorker_UI_Alive = true;
        private bool backgroundWorker_RS232_Alive = true;
        public bool FLAG_UI_init = false;
        private SerialPort serialPort = new SerialPort();
        private TCP_Server.SoketFile.Receiver SoketFileReceiver;
        private Stopwatch stopwatch = new Stopwatch();
        private Basic.MyConvert myConvert = new Basic.MyConvert();
        private bool T1_RS232執行序Unbusy = false;
        public Properties properties = new Properties();
        public List<String> str_read = new List<string>();
        List<byte> byte_read_buf = new List<byte>();
        private int 掃描速度 = 0;

        private double CycleTime = 0;
        private double CycleTime_start = 0;
        private string _str_send = "";
        private string str_send
        {
            get
            {
                return _str_send;
            }
            set
            {
                _str_send = value;
            }
        }
        private string str_read_temp0 = "";
        private string str_read_current_program = "";
        private string str_read_current_comment = "";
        private byte[] buf;
        public List<String> Read_List = new List<string>();
        public List<String> Send_List = new List<string>();
        private String axis_speed_data = "D8347";
        private String axis_position_data = "D8344";
        private String axis_jog_acc_enable = "M8342";

        public void Add_Start_Method(MethodDelegate method)
        {
            lock (this) Run_start.Add(method);
        }
        public void Add_Complete_Method(MethodDelegate method)
        {
            lock (this) Run_complete.Add(method);
        }
        public void Add_UI_Method(MethodDelegate method)
        {
            lock (this) Run_UI.Add(method);
        }

        public void Set_SleepTime(int SleepTime)
        {
            this.掃描速度 = SleepTime;
        }
        public LowerMachine(SerialPort serialPort, Form form)
        {
            this.serialPort = serialPort;

            SoketFileReceiver = new SoketFile.Receiver(TCP.UDP_Cilent.client);

            LoadProgram();
            LoadDevice();
            backgroundWorker_Others.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_Others_DoWork);
            backgroundWorker_Program.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_Program_DoWork);
            backgroundWorker_UI.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_UI_DoWork);
            backgroundWorker_RS232.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_RS232_DoWork);
            if (!backgroundWorker_Others.IsBusy) backgroundWorker_Others.RunWorkerAsync();
            if (!backgroundWorker_Program.IsBusy) backgroundWorker_Program.RunWorkerAsync();
            if (!backgroundWorker_UI.IsBusy) backgroundWorker_UI.RunWorkerAsync();
            if (!backgroundWorker_RS232.IsBusy) backgroundWorker_RS232.RunWorkerAsync();
            backgroundWorker_Others.WorkerSupportsCancellation = true;
            backgroundWorker_Program.WorkerSupportsCancellation = true;
            backgroundWorker_UI.WorkerSupportsCancellation = true;
            backgroundWorker_RS232.WorkerSupportsCancellation = true;
            form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
        }
        #region Stream IO
        [Serializable]
        private class SaveProgramFile
        {
            public List<String[]> Program = new List<string[]>();
            public List<String[]> Comment = new List<string[]>();
            public String COMProt = "";
            public bool SerialPortAutoConnet = false;
            public Properties.Tx通訊方式 通訊方式;
        }
        [Serializable]
        public class SaveDeviceFile
        {
            public List<object[]> allValue = new List<object[]>();
        }
        private SaveProgramFile saveProgramFile = new SaveProgramFile();
        private SaveDeviceFile saveDeviceFile = new SaveDeviceFile();
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveProgram(false);
            SaveDevice();
            Stop();
        }
        private void SaveProgram(bool changeFilename)
        {
            try
            {
                IFormatter binFmt = new BinaryFormatter();
                Stream stream = null;
                if (changeFilename)
                {

                    MyFileStream.檔案更名($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\", "program.stp", "program.bak.stp");
                }

                saveProgramFile.Program = MyFileStream.DeepClone(properties.Program);
                saveProgramFile.Comment = MyFileStream.DeepClone(properties.Comment);
                saveProgramFile.COMProt = this.serialPort.PortName;
                saveProgramFile.通訊方式 = this.通訊方式;
                try
                {
                    stream = File.Open($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\program.stp", FileMode.Create);
                    binFmt.Serialize(stream, saveProgramFile);
                }
                finally
                {
                    if (stream != null) stream.Close();
                }
            }
            catch
            {
                MyMessageBox.ShowDialog("儲存prgram.stp失敗!");
            }
                         
        }
        private void LoadProgram()
        {
            properties.Program.Clear();
            properties.Comment.Clear();
            IFormatter binFmt = new BinaryFormatter();
            Stream stream = null;
            try
            {
                if (File.Exists($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\program.stp"))
                {
                    stream = File.Open($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\program.stp", FileMode.Open);
                    try { saveProgramFile = (SaveProgramFile)binFmt.Deserialize(stream); }
                    catch { }

                }

                properties.Program = MyFileStream.DeepClone(saveProgramFile.Program);
                properties.Comment = MyFileStream.DeepClone(saveProgramFile.Comment);
                properties.Device.Set_Comment(properties.Comment);
                this.通訊方式 = saveProgramFile.通訊方式.DeepClone();
                if (saveProgramFile.COMProt != null && saveProgramFile.COMProt != "")
                {
                    this.serialPort.PortName = saveProgramFile.COMProt;
                    if (saveProgramFile.SerialPortAutoConnet) OpenSerialPort();
                }
                Program_JumpPoint = new int[20000];
                int index = 0;
                foreach (string[] str_array in properties.Program)
                {
                    if (str_array[0] == "TAB")
                    {
                        int temp = 0;
                        if (int.TryParse(str_array[1], out temp))
                        {
                            if (temp < Program_JumpPoint.Length)
                            {
                                Program_JumpPoint[temp] = index;
                            }
                        }
                    }
                    index++;
                }
            }
            finally
            {
                if (stream != null) stream.Close();
                FLAG_Program_pause_enable = new bool[properties.Program.Count];
            }
        }
        //private void SaveDevice()
        //{
        //    try
        //    {
        //        IFormatter binFmt = new BinaryFormatter();
        //        Stream stream = null;

        //        saveDeviceFile.allValue = properties.Device.GetAllValue();

        //        try
        //        {
        //            stream = File.Open($@"{currentDirectory}\Device.val", FileMode.Create);
        //            binFmt.Serialize(stream, saveDeviceFile);
        //        }
        //        finally
        //        {
        //            if (stream != null) stream.Close();
        //        }
        //    }
        //    catch
        //    {
        //        MyMessageBox.ShowDialog("儲存Device.val失敗!");
        //    }
        
        //}
        //private void LoadDevice()
        //{
        //    IFormatter binFmt = new BinaryFormatter();
        //    Stream stream = null;
        //    MemoryStream memoryStream = null;
        //    try
        //    {
               
        //        if (File.Exists($@"{currentDirectory}\Device.val"))
        //        {
        //            stream = File.Open($@"{currentDirectory}\Device.val", FileMode.Open);
        //            try { saveDeviceFile = (SaveDeviceFile)binFmt.Deserialize(stream); }
        //            catch { }
        //        }
        //        properties.Device.SetAllValue(saveDeviceFile.allValue);
        
        //    }
        //    finally
        //    {
        //        if (stream != null) stream.Close();
        //        if (memoryStream != null) memoryStream.Close();
        //    }
        //}
        public string SaveDevice()
        {
            string base64String = "";
            try
            {
                IFormatter binFmt = new BinaryFormatter();
                saveDeviceFile.allValue = properties.Device.GetAllValue();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    binFmt.Serialize(memoryStream, saveDeviceFile);
                    base64String = Convert.ToBase64String(memoryStream.ToArray());

                    File.WriteAllText($@"{currentDirectory}\Device.txt", base64String, Encoding.UTF8);
                }
                return base64String;
            }
            catch
            {
                MyMessageBox.ShowDialog("儲存Device.txt失敗!");
            }
            return base64String;
        }
        public void LoadDevice()
        {
            string filePath = $@"{currentDirectory}\Device.txt";
            if (!File.Exists(filePath))
                return;

            string base64String = File.ReadAllText(filePath, Encoding.UTF8);
            LoadDevice(base64String);
        }
        public void LoadDevice(string base64String)
        {
            try
            {
       
                byte[] binaryData = Convert.FromBase64String(base64String);

                using (MemoryStream memoryStream = new MemoryStream(binaryData))
                {
                    IFormatter binFmt = new BinaryFormatter();
                    saveDeviceFile = (SaveDeviceFile)binFmt.Deserialize(memoryStream);
                }

                properties.Device.SetAllValue(saveDeviceFile.allValue);
            }
            catch
            {
                MyMessageBox.ShowDialog("讀取Device.txt失敗!");
            }
        }

        #endregion

        public bool OpenSerialPort()
        {
            if (this.通訊方式 == Properties.Tx通訊方式.SerialPort)
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Encoding = System.Text.Encoding.UTF8;
                    serialPort.DataBits = 8;
                    serialPort.Parity = Parity.None;
                    serialPort.StopBits = StopBits.One;
                    serialPort.ReadBufferSize = 409600;
                    serialPort.WriteBufferSize = 409600;
                    serialPort.BaudRate = 115200;
                    try
                    {
                        serialPort.Open();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else return true;
            }
            else
            {
                return true;
            }
        }
        public bool CloseSerialPort()
        {
            if (this.serialPort.IsOpen)
            {
                this.serialPort.Close();
                return true;
            }
            return false;
        }
        public bool GetConnectState()
        {
            return this.serialPort.IsOpen;
        }
        public double GetCycleTime()
        {
            return Math.Round( CycleTime,3);
        }
        public bool GetSerialPortAutoConnet()
        {
            return saveProgramFile.SerialPortAutoConnet;
        }
        public void SetSerialPortAutoConnet(bool flag)
        {
            saveProgramFile.SerialPortAutoConnet = flag;
        }
        public void SetCOM(String COM)
        {
            serialPort.PortName = COM;
        }
        public String GetCOM()
        {
            if (serialPort.IsOpen) return serialPort.PortName;
            else return "#None";
        
        }
        public string[] GetAllPortname()
        {
            return SerialPort.GetPortNames();
        }
        object T1_RS232執行序Unbusy_temp;
        private void SystemTimerRefresh()
        {
            T1_RS232執行序Unbusy_temp = new object();
            properties.device_system.Get_Device("T1", out T1_RS232執行序Unbusy_temp);
            this.T1_RS232執行序Unbusy = (bool)T1_RS232執行序Unbusy_temp;
        }
        public void Stop()
        {
            backgroundWorker_Others_Alive = false;
            backgroundWorker_Program_Alive = false;
            backgroundWorker_UI_Alive = false;
            backgroundWorker_RS232_Alive = false;
            UI_Thread.Abort();
            //while(true)
            //{
            //    bool flag = true;
            //    if (backgroundWorker_Others.IsBusy) flag = false;
            //    //if (backgroundWorker_Program.IsBusy) flag = false;
            //    //if (backgroundWorker_UI.IsBusy) flag = false;
            //    if (backgroundWorker_RS232.IsBusy) flag = false;
            //    if (flag) break;
            //}
        }
        private void backgroundWorker_Others_DoWork(object sender, DoWorkEventArgs e)
        {   
 
           /*while (backgroundWorker_Others_Alive)
            {
                if (Run_start.Count > 0)
                {
                    DelegateArrayStart = Run_start.ToArray();
                    for (int i = 0; i < DelegateArrayStart.Length; i++)
                    {
                        DelegateArrayStart[i]();
                    }
                }
                if (Run_complete.Count > 0)
                {
                    DelegateArrayComplete = Run_complete.ToArray();
                    for (int i = 0; i < DelegateArrayComplete.Length; i++)
                    {
                        DelegateArrayComplete[i]();
                    }
                }
               // Thread.Sleep(1); 
            }*/
        }
        private void backgroundWorker_Program_DoWork(object sender, DoWorkEventArgs e)
        {
            stopwatch.Start();
            while (backgroundWorker_Program_Alive)
            {        
                sub_緩衝區寫入主程式();
                Program_Run(); //Thread.Sleep(0);
            }
        }
        Thread UI_Thread;
        private void backgroundWorker_UI_DoWork(object sender, DoWorkEventArgs e)
        {
            MethodDelegate[] DelegateArrayUI;
            object obj_divece_save_flag;
            if (UI_Thread == null) UI_Thread = Thread.CurrentThread;
            while (backgroundWorker_UI_Alive)
            {
                if (FLAG_UI_init)
                {
                    if (Run_UI.Count > 0)
                    {
                        DelegateArrayUI = Run_UI.ToArray();
                        for (int i = 0; i < DelegateArrayUI.Length; i++)
                        {
                            DelegateArrayUI[i]();
                        }
                    }
                    properties.Device.Get_Device("S8", out obj_divece_save_flag);
                    if ((bool)obj_divece_save_flag)
                    {
                        SaveDevice();
                        properties.Device.Set_Device("S8", false);
                    }
                }
     
           
                Thread.Sleep(10);         
            }
        }
        private void backgroundWorker_RS232_DoWork(object sender, DoWorkEventArgs e)
        {
            int running = 0;
            while (backgroundWorker_RS232_Alive)
            {
                running = 0;
                SystemTimerRefresh();
                if (通訊方式 == Properties.Tx通訊方式.SerialPort)
                {
                    if (Properties.通訊方式_buf != 通訊方式)
                    {
                        properties.str_命令空格 = "@@";
                        properties.str_起始命令 = "##";
                        properties.str_結束命令 = "$$";
                        properties.str_完成命令 = "%%";
                        properties.str_程式空格 = "^^";
                        properties.str_每段程式空格 = "~~";
                        Properties.通訊方式_buf = 通訊方式;
                    }

                    if (SerialPort_ReadData()) running++;
                    if (SerialPort_ReadDataProcess()) running++;
                    if (SerialPort_SendData()) running++;
                }
                else if (通訊方式 == Properties.Tx通訊方式.Enthernet)
                {
                    if (Properties.通訊方式_buf != 通訊方式)
                    {
                        properties.str_命令空格 = "Ⓐ";
                        properties.str_起始命令 = "Ⓑ";
                        properties.str_結束命令 = "Ⓒ";
                        properties.str_完成命令 = "Ⓓ";
                        properties.str_程式空格 = "Ⓔ";
                        properties.str_每段程式空格 = "Ⓕ";
                        Properties.通訊方式_buf = 通訊方式;
                    }

                    if (Enthernet_ReadDataProcess()) running++;
                    if (Enthernet_SendData()) running++;
                }                    
                if (sub_解析接收到訊息()) running++;

                if (running != 0)
                {
                    properties.device_system.Set_Device("T1", false);
                    properties.device_system.Set_Device("T1", "K3000", 2);
                    T1_RS232執行序Unbusy = false;
                }
                else
                {
                    properties.device_system.Set_Device("T1", true);
                    properties.device_system.Set_Device("T1", "K3000", 2);
                }
                if (T1_RS232執行序Unbusy)
                {

                    Thread.Sleep(100);
                }
                else
                {
                    Thread.Sleep(0);
                }
            }
        }

        private bool SerialPort_ReadData()
        {
            bool flag = false;
            if (GetConnectState())
            {
                int num = serialPort.BytesToRead;
                if (num > 0)
                {
                    buf = new byte[num];

                    serialPort.Read(buf, 0, num);
                    for (int i = 0; i < buf.Length; i++)
                    {
                        byte_read_buf.Add(buf[i]);
                    }
                    flag = true;
                }
            }
            return flag;
        }
        private bool SerialPort_ReadDataProcess()
        {
            bool flag = false;
            str_read_temp0 = "";
            str_read_temp0 = System.Text.Encoding.UTF8.GetString(byte_read_buf.ToArray());
            int index = str_read_temp0.IndexOf(properties.str_結束命令);
            if (index >= 0)
            {
                index += properties.str_結束命令.Length;
                string[] str_read_temp;
                str_read_temp0 = str_read_temp0.Substring(0, index);
                str_read_temp = myConvert.分解分隔號字串(str_read_temp0, properties.str_結束命令);
                for (int k = 0; k < str_read_temp.Length; k++)
                {
                    str_read.Add(str_read_temp[k]);

                }
                byte_read_buf.RemoveRange(0, index);
                flag = true;
            }           
            return flag;
        }
        private bool Enthernet_ReadDataProcess()
        {
            bool flag = false;
            str_read_temp0 = "";
            if (TCP.UDP_Cilent.client != null)
            {
                if (TCP.UDP_Cilent.client.Readline(ref str_read_temp0))
                {
                    if (str_read_temp0 == null) str_read_temp0 = "";
                    else if (str_read_temp0.IndexOf("**") >= 0 || SoketFileReceiver.PaketWait)
                    {
                        SoketFileReceiver.AddReceiveData(str_read_temp0);
                    }
                    else str_read.Add(str_read_temp0.Replace(properties.str_結束命令, ""));
                    if (str_read_temp0 != "") flag = true;
                }
                SoketFileReceiver.Run();
            }
        
            return flag;
        }
        private bool SerialPort_SendData()
        {
            if (str_send.Length > 0)
            {
                if (GetConnectState())
                {
                    serialPort.Write(str_send);
                    Send_List.Add(str_send);
                }
                str_send = "";
                return true;
            }
            return false;
        }
        private bool Enthernet_SendData()
        {
            if (str_send.Length > 0)
            {
                if (TCP.UDP_Cilent.client != null)
                {
                    TCP.UDP_Cilent.client.Writeline(str_send);
                }
                str_send = "";
                return true; 
            }       
            return false;
        }
        private bool IsSendDataBusy()
        {
            if (str_send.Length > 0) return true;
            else return false;
        }

        #region 解析接收到訊息
        byte cnt_解析接收到訊息 = 255;
        String[] str_receive_array;
        String str_解析接收到訊息_緩衝區 = "";
        string str_解析接收到訊息_device_low = "";
        string str_解析接收到訊息_device_high = "";
        bool sub_解析接收到訊息()
        {
            if (cnt_解析接收到訊息 == 255) cnt_解析接收到訊息 = 1;
            if (cnt_解析接收到訊息 == 1) cnt_解析接收到訊息_00_檢查有無訊息接收到(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 2) cnt_解析接收到訊息 = 10;

            if (cnt_解析接收到訊息 == 10) cnt_解析接收到訊息_10_解析訊息(ref cnt_解析接收到訊息);

            if (cnt_解析接收到訊息 == 15) cnt_解析接收到訊息_15_通訊測試命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 16) cnt_解析接收到訊息 = 250;

            if (cnt_解析接收到訊息 == 20) cnt_解析接收到訊息_20_Write命令_檢查元件種類(ref cnt_解析接收到訊息); 

            if (cnt_解析接收到訊息 == 25) cnt_解析接收到訊息_25_Write命令_寫入Data(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 26) cnt_解析接收到訊息_25_Write命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 27) cnt_解析接收到訊息 = 250;

            if (cnt_解析接收到訊息 == 30) cnt_解析接收到訊息_30_Write命令_寫入Device(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 31) cnt_解析接收到訊息_30_Write命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 32) cnt_解析接收到訊息 = 250;

            if (cnt_解析接收到訊息 == 50) cnt_解析接收到訊息_50_Read命令_檢查元件種類(ref cnt_解析接收到訊息);

            if (cnt_解析接收到訊息 == 55) cnt_解析接收到訊息_55_Read命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 56) cnt_解析接收到訊息 = 250;

            if (cnt_解析接收到訊息 == 60) cnt_解析接收到訊息_60_Read命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 61) cnt_解析接收到訊息 = 250;

            if (cnt_解析接收到訊息 == 65) cnt_解析接收到訊息_65_Read命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 66) cnt_解析接收到訊息 = 250;

            if (cnt_解析接收到訊息 == 70) cnt_解析接收到訊息_70_Buffer_Program_Clear命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 71) cnt_解析接收到訊息_71_Buffer_RemoveAt命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 72) cnt_解析接收到訊息_72_Buffer_Num命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 73) cnt_解析接收到訊息_73_Buffer_Read_Program命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 74) cnt_解析接收到訊息_74_Buffer_Get_Program命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 75) cnt_解析接收到訊息_75_Buffer_Check_Program命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 76) cnt_解析接收到訊息_76_Buffer_Get_Comment命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 77) cnt_解析接收到訊息_77_Buffer_Read_Comment命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 78) cnt_解析接收到訊息_78_Buffer_Check_Comment命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 79) cnt_解析接收到訊息_79_Comment_Clear命令_回覆TopMachine(ref cnt_解析接收到訊息);

            if (cnt_解析接收到訊息 == 80) cnt_解析接收到訊息_80_Upload_Finally命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 81) cnt_解析接收到訊息_81_Upload_Comment命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 82) cnt_解析接收到訊息_82_Upload_Program命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 83) cnt_解析接收到訊息_83_Upload_Program_Confirm命令_回覆TopMachine(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 84) cnt_解析接收到訊息_84_Upload_Comment_Confirm命令_回覆TopMachine(ref cnt_解析接收到訊息);
            
            if (cnt_解析接收到訊息 == 250) cnt_解析接收到訊息_250_解析結束(ref cnt_解析接收到訊息);
            if (cnt_解析接收到訊息 == 251) cnt_解析接收到訊息 = 255;

            if (cnt_解析接收到訊息 == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        void cnt_解析接收到訊息_00_檢查有無訊息接收到(ref byte cnt)
        {
            str_解析接收到訊息_緩衝區 = "";
            if (str_read.Count > 0)
            {
                str_解析接收到訊息_緩衝區 = str_read[0];
                if (通訊方式 == Properties.Tx通訊方式.SerialPort) Read_List.Add(str_解析接收到訊息_緩衝區);
                str_read.RemoveAt(0);
                cnt++;
            }
        }

        void cnt_解析接收到訊息_10_解析訊息(ref byte cnt)
        {
            String str_temp = str_解析接收到訊息_緩衝區;
            str_解析接收到訊息_緩衝區 = "";
            str_receive_array = myConvert.分解分隔號字串(str_temp, properties.str_命令空格);
            if (str_receive_array.Length > 0)
            {
                if (str_receive_array[0] == properties.str_通訊測試命令)
                {
                    cnt = 15;
                }
                else if (str_receive_array[0] == properties.str_Write命令)
                {
                    cnt = 20;
                }
                else if (str_receive_array[0] == properties.str_Read命令)
                {
                    cnt = 50;
                }
                else if (str_receive_array[0] == properties.str_Buffer_Program_Clear命令)
                {
                    cnt = 70;
                }
                else if (str_receive_array[0] == properties.str_Buffer_RemoveAt命令)
                {
                    cnt = 71;
                }
                else if (str_receive_array[0] == properties.str_Buffer_Num命令)
                {
                    cnt = 72;
                }
                else if (str_receive_array[0] == properties.str_Buffer_Read_Program命令)
                {
                    cnt = 73;
                }
                else if (str_receive_array[0] == properties.str_Buffer_Get_Program命令)
                {
                    cnt = 74;
                }
                else if (str_receive_array[0] == properties.str_Buffer_Check_Program命令)
                {
                    cnt = 75;
                }
                else if (str_receive_array[0] == properties.str_Buffer_Get_Comment命令)
                {
                    cnt = 76;
                }
                else if (str_receive_array[0] == properties.str_Buffer_Read_Comment命令)
                {
                    cnt = 77;
                }
                else if (str_receive_array[0] == properties.str_Buffer_Check_Comment命令)
                {
                    cnt = 78;
                }
                else if (str_receive_array[0] == properties.str_Comment_Clear命令)
                {
                    cnt = 79;
                } 
                else if (str_receive_array[0] == properties.str_Upload_Finally命令)
                {
                    cnt = 80;
                }
                else if (str_receive_array[0] == properties.str_Upload_Comment命令)
                {
                    cnt = 81;
                }
                else if (str_receive_array[0] == properties.str_Upload_Program命令)
                {
                    cnt = 82;
                }
                else if (str_receive_array[0] == properties.str_Upload_Program_Confirm命令)
                {
                    cnt = 83;
                }
                else if (str_receive_array[0] == properties.str_Upload_Comment_Confirm命令)
                {
                    cnt = 84;
                }      
                else
                {
                    cnt = 250;
                }
            }
            else
            {
                cnt = 250;
            }
      
        }

        void cnt_解析接收到訊息_15_通訊測試命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.str_完成命令.ToString();
                cnt++;
            }

        }

        void cnt_解析接收到訊息_20_Write命令_檢查元件種類(ref byte cnt)
        {

            if (str_receive_array.Length > 1)
            {
                string str_device = str_receive_array[1].Substring(0, 1);
                if (str_receive_array.Length == 4 && (str_device == "D" || str_device == "F" || str_device == "R"))
                {
                    cnt = 25;
                }
                else if (str_receive_array.Length == 3 && (str_device == "X" || str_device == "Y" || str_device == "M" || str_device == "S"))
                {
                    cnt = 30;
                }
                else
                {
                    cnt = 250;
                    return;
                }
            }
            else
            {
                cnt = 250;
                return;
            }
            
        }
        void cnt_解析接收到訊息_25_Write命令_寫入Data(ref byte cnt)
        {
            if(str_receive_array.Length == 4)
            {
                string str_device = str_receive_array[1];
                string str_value = str_receive_array[2];
                string str_datatype = str_receive_array[3];

                int int_low_value = 0;
                int int_high_value = 0;
                if (str_datatype == "S")
                {
                    if (Int32.TryParse(str_value, out int_low_value))
                    {
                        if (properties.Device.Set_Device(str_device, int_low_value))
                        {
                            cnt++;
                            return;
                        }
                        else
                        {
                            cnt = 250;
                            return;
                        }
                    }
                    else
                    {
                        cnt = 250;
                        return;
                    }
                }
                else if (str_datatype == "D")
                {
                    Int64 Int64_value = 0;
                    if (Int64.TryParse(str_value, out Int64_value))
                    {
                        myConvert.Int64轉Int32(Int64_value, ref int_high_value, ref int_low_value);
                        str_解析接收到訊息_device_low = str_device;
                        str_解析接收到訊息_device_high = str_device.Substring(0, 1);
                        string str_device_num_high = str_device.Substring(1, str_device.Length - 1);
                        int int_device_num_high = 0;
                        if (Int32.TryParse(str_device_num_high, out int_device_num_high))
                        {
                            str_解析接收到訊息_device_high = str_解析接收到訊息_device_high + (str_device_num_high + 1).ToString();
                            if (!properties.Device.Set_Device(str_解析接收到訊息_device_low, int_low_value))
                            {
                                cnt = 250;
                                return;
                            }
                            if (!properties.Device.Set_Device(str_解析接收到訊息_device_high, int_high_value))
                            {
                                cnt = 250;
                                return;
                            }
                            cnt++;
                            return;

                        }
                        else
                        {
                            cnt = 250;
                            return;
                        }

                    }
                    else
                    {
                        cnt = 250;
                        return;
                    }
                }
                else
                {
                    cnt = 250;
                    return;
                }
            }
        
         
        }
        void cnt_解析接收到訊息_25_Write命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                string str_datatype = str_receive_array[3];
                if (str_datatype == "S")
                {
                    string str_device = str_receive_array[1];
                    object value = new object();
                    if (properties.Device.Get_Device(str_device, out value))
                    {
                        str_send = ((int)value).ToString() + properties.str_完成命令.ToString();
                        cnt++;
                        return;
                    }
                    else
                    {
                        cnt = 250;
                        return;
                    }
                }
                else if (str_datatype == "D")
                {
                    object value_low = new object();
                    object value_high = new object();
                    Int64 Int64_value = 0;
                    if (!properties.Device.Get_Device(str_解析接收到訊息_device_low, out value_low))
                    {
                        cnt = 250;
                        return;
                    }
                    if (!properties.Device.Get_Device(str_解析接收到訊息_device_high, out value_high))
                    {
                        cnt = 250;
                        return;
                    }
                    myConvert.Int32轉Int64(ref Int64_value, (int)value_high, (int)value_low);
                    str_send = (Int64_value).ToString() + properties.str_完成命令.ToString();
                    cnt++;
                    return;
                }
                else
                {
                    cnt = 250;
                    return;
                }
            }

        }
        void cnt_解析接收到訊息_30_Write命令_寫入Device(ref byte cnt)
        {
            if (str_receive_array.Length == 3)
            {
                string str_device = str_receive_array[1];
                string str_value = str_receive_array[2];
                bool FLAG_value = false;
                if (str_value == properties.str_True命令) FLAG_value = true;
                else if (str_value == properties.str_False命令) FLAG_value = false;

                if (properties.Device.Set_Device(str_device, FLAG_value))
                {
                    cnt++;
                    return;
                }
                else
                {
                    cnt = 250;
                    return;
                }

            }


        }
        void cnt_解析接收到訊息_30_Write命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                string str_device = str_receive_array[1];
                string str_value = str_receive_array[2];
                object FLAG_value = new object();
                if(!properties.Device.Get_Device(str_device,out FLAG_value))
                {
                    cnt = 250;
                    return;
                }
                if ((bool)FLAG_value) str_send = properties.str_True命令 + properties.str_完成命令.ToString();
                else str_send = properties.str_False命令 + properties.str_完成命令.ToString();               
                cnt++;
            }

        }

        void cnt_解析接收到訊息_50_Read命令_檢查元件種類(ref byte cnt)
        {
            if (str_receive_array.Length > 1)
            {
                string str_device = str_receive_array[1].Substring(0, 1);
                if (str_receive_array.Length == 3 && (str_device == "D" || str_device == "F" || str_device == "R" || str_device == "Z"))
                {
                    cnt = 55;
                }
                else if (str_receive_array.Length == 2 && (str_device == "X" || str_device == "Y" || str_device == "M" || str_device == "S" || str_device == "T"))
                {
                    cnt = 60;
                }
                else if (str_receive_array.Length == 3 && (str_device == "T"))
                {
                    cnt = 65;
                }
                else
                {
                    cnt = 250;
                    return;
                }
            }
            else
            {
                cnt = 250;
                return;
            }
               
        }
        void cnt_解析接收到訊息_55_Read命令_回覆TopMachine(ref byte cnt)
        {
            string str_device = str_receive_array[1];
            if (!IsSendDataBusy())
            {
                string str_datatype = str_receive_array[2];
                if (str_datatype == "S")
                {                 
                    object value = new object();
                    if (properties.Device.Get_Device(str_device, out value))
                    {
                        str_send = ((int)value).ToString() + properties.str_完成命令.ToString();
                        cnt++;
                        return;
                    }
                    else
                    {
                        cnt = 250;
                        return;
                    }
                }
                else if (str_datatype == "D")
                {

                    str_解析接收到訊息_device_low = str_device;
                    str_解析接收到訊息_device_high = str_device.Substring(0, 1);
                    string str_device_num_high = str_device.Substring(1,str_device.Length - 1);
                    int int_device_num_high = 0;
                    if (!Int32.TryParse(str_device_num_high, out int_device_num_high))
                    {
                        cnt = 250;
                        return;
                    }
                    str_解析接收到訊息_device_high = str_解析接收到訊息_device_high + (str_device_num_high + 1).ToString();

                    object value_low = new object();
                    object value_high = new object();
                    Int64 Int64_value = 0;
                    if (!properties.Device.Get_Device(str_解析接收到訊息_device_low, out value_low))
                    {
                        cnt = 250;
                        return;
                    }
                    if (!properties.Device.Get_Device(str_解析接收到訊息_device_high, out value_high))
                    {
                        cnt = 250;
                        return;
                    }
                    myConvert.Int32轉Int64(ref Int64_value, (int)value_high, (int)value_low);
                    str_send = (Int64_value).ToString() + properties.str_完成命令.ToString();
                    cnt++;
                    return;
                }
                else
                {
                    cnt = 250;
                    return;
                }
            }

        }
        void cnt_解析接收到訊息_60_Read命令_回覆TopMachine(ref byte cnt)
        {
            string str_device = str_receive_array[1];
            if (!IsSendDataBusy())
            {
                object value = new object();
                if (properties.Device.Get_Device(str_device, out value))
                {
                    String str_temp = "";
                    if ((bool)value) str_temp = properties.str_True命令;
                    else str_temp = properties.str_False命令;
                    str_send = str_temp + properties.str_完成命令.ToString();
                    cnt++;
                    return;
                }
                else
                {
                    cnt = 250;
                    return;
                }                      
            }

        }
        void cnt_解析接收到訊息_65_Read命令_回覆TopMachine(ref byte cnt)
        {
            string str_device = str_receive_array[1];
            if (!IsSendDataBusy())
            {
                object value = new object();
                if (properties.Device.Get_Device(str_device, 2,out value))
                {
                    str_send = value.ToString() +  properties.str_完成命令.ToString();
                    cnt++;
                    return;
                }
                else
                {
                    cnt = 250;
                    return;
                }
            }

        }

        void cnt_解析接收到訊息_70_Buffer_Program_Clear命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                properties.Program_緩衝區.Clear();
               // properties.Comment.Clear();
                str_send = properties.str_完成命令.ToString();
                cnt = 250;
                return;
         
            }

        }
        void cnt_解析接收到訊息_71_Buffer_RemoveAt命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                if (properties.Program_緩衝區.Count > 0)
                {
                    properties.Program_緩衝區.RemoveAt(properties.Program_緩衝區.Count - 1);
                }
                str_send = properties.str_完成命令.ToString();
                cnt = 250;
                return;

            }

        }
        void cnt_解析接收到訊息_72_Buffer_Num命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                str_send = properties.Program_緩衝區.Count.ToString() + properties.str_完成命令.ToString();
                cnt = 250;
                return;

            }

        }
        void cnt_解析接收到訊息_73_Buffer_Read_Program命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                if (str_receive_array.Length == 3)
                {
                    string str_temp0 = "";
                    string str_temp1 = "";
                    string[] str_current_program;
                    string str_start_num = str_receive_array[1];
                    string str_finally_num = str_receive_array[2];
                    int int_start_num = 0;
                    int int_finally_num = 0;
                    if (!Int32.TryParse(str_start_num, out int_start_num))
                    {
                        cnt = 250;
                        return;
                    }
                    if (!Int32.TryParse(str_finally_num, out int_finally_num))
                    {
                        cnt = 250;
                        return;
                    }
                    if (int_start_num >= properties.Program_緩衝區.Count)
                    {
                        cnt = 250;
                        return;
                    }
                    for (int i = int_start_num; i < int_finally_num; i++)
                    {
                        str_current_program = properties.Program_緩衝區[i];
                        str_temp0 = "";
                        for (int k = 0; k < str_current_program.Length; k++)
                        {
                            if (k == 0) str_temp0 += str_current_program[k];
                            else str_temp0 += properties.str_程式空格 + str_current_program[k];
                        }
                        if (i != 0) str_temp1 = str_temp1 + properties.str_每段程式空格 + str_temp0;
                        else str_temp1 = str_temp1 +  str_temp0;
                    }
                    str_send = str_temp1 + properties.str_完成命令.ToString();
                    cnt = 250;
                    return;
                }
                else
                {
                    cnt = 250;
                    return;
                }
            

            }

        }
        void cnt_解析接收到訊息_74_Buffer_Get_Program命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                if (str_receive_array.Length != 1)
                {
                    cnt = 250;
                    return;
                }
                properties.Program_緩衝區.Clear();
                foreach (string[] str_array in properties.Program)
                {
                    string[] str_aarray_temp = new string[str_array.Length];
                    for (int i = 0; i < str_array.Length; i++)
                    {
                        str_aarray_temp[i] = str_array[i];
                    }
                    properties.Program_緩衝區.Add(str_aarray_temp);
                }
                str_send = properties.Program_緩衝區.Count.ToString() + properties.str_完成命令;
                cnt = 250;
                return;
            }
        }
        void cnt_解析接收到訊息_75_Buffer_Check_Program命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                if (str_receive_array.Length != 4)
                {
                    str_send = properties.str_False命令 + properties.str_完成命令;
                    cnt = 250;
                    return;
                }
                string str_temp0 = "";
                string str_temp1 = "";
                string[] str_current_program;     
                string str_start_num = str_receive_array[1];
                string str_finally_num = str_receive_array[2];
                string str_check = str_receive_array[3];
                int int_start_num = 0;
                int int_finally_num = 0;

                if (!Int32.TryParse(str_start_num, out int_start_num))
                {
                    str_send = properties.str_False命令 + properties.str_完成命令;
                    cnt = 250;
                    return;
                }
                if (!Int32.TryParse(str_finally_num, out int_finally_num))
                {
                    str_send = properties.str_False命令 + properties.str_完成命令;
                    cnt = 250;
                    return;
                }
                for (int i = int_start_num; i < int_finally_num; i++)
                {
                    str_current_program = properties.Program_緩衝區[i];
                    str_temp0 = "";
                    for (int k = 0; k < str_current_program.Length; k++)
                    {
                        if (k == 0) str_temp0 += str_current_program[k];
                        else str_temp0 += properties.str_程式空格 + str_current_program[k];
                    }
                    if (i != 0) str_temp1 = str_temp1 + properties.str_每段程式空格 + str_temp0;
                    else str_temp1 = str_temp1 + str_temp0;
                }
                if (str_temp1 == str_check)
                {
                    str_send = properties.str_True命令 + properties.str_完成命令;
                    cnt = 250;
                    return;
                }
                else
                {
                    str_send = properties.str_False命令 + properties.str_完成命令;
                    cnt = 250;
                    return;
                }
            }
        }
        void cnt_解析接收到訊息_76_Buffer_Get_Comment命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                if (str_receive_array.Length != 1)
                {
                    cnt = 250;
                    return;
                }
                properties.Comment.Clear();
                properties.Comment = properties.Device.Get_Comment();
                str_send = properties.Comment.Count.ToString() + properties.str_完成命令;
                cnt = 250;
                return;
            }
        }
        void cnt_解析接收到訊息_77_Buffer_Read_Comment命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                if (str_receive_array.Length == 3)
                {
                    string str_temp0 = "";
                    string str_temp1 = "";
                    string[] str_current_comment;
                    string str_start_num = str_receive_array[1];
                    string str_finally_num = str_receive_array[2];
                    int int_start_num = 0;
                    int int_finally_num = 0;
                    if (!Int32.TryParse(str_start_num, out int_start_num))
                    {
                        cnt = 250;
                        return;
                    }
                    if (!Int32.TryParse(str_finally_num, out int_finally_num))
                    {
                        cnt = 250;
                        return;
                    }

                    for (int i = int_start_num; i < int_finally_num; i++)
                    {
                        str_current_comment = properties.Comment[i];
                        str_temp0 = "";
                        for (int k = 0; k < str_current_comment.Length; k++)
                        {
                            if (k == 0) str_temp0 += str_current_comment[k];
                            else str_temp0 += properties.str_程式空格 + str_current_comment[k];
                        }
                        if (i != 0) str_temp1 = str_temp1 + properties.str_每段程式空格 + str_temp0;
                        else str_temp1 = str_temp1 + str_temp0;
                    }
                    str_send = str_temp1 + properties.str_完成命令.ToString();
                    cnt = 250;
                    return;
                }
                else
                {
                    cnt = 250;
                    return;
                }


            }

        }
        void cnt_解析接收到訊息_78_Buffer_Check_Comment命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                if (str_receive_array.Length != 4)
                {
                    str_send = properties.str_False命令 + properties.str_完成命令;
                    cnt = 250;
                    return;
                }
                string str_temp0 = "";
                string str_temp1 = "";
                string[] str_current_comment;
                string str_start_num = str_receive_array[1];
                string str_finally_num = str_receive_array[2];
                string str_check = str_receive_array[3];
                int int_start_num = 0;
                int int_finally_num = 0;

                if (!Int32.TryParse(str_start_num, out int_start_num))
                {
                    str_send = properties.str_False命令 + properties.str_完成命令;
                    cnt = 250;
                    return;
                }
                if (!Int32.TryParse(str_finally_num, out int_finally_num))
                {
                    str_send = properties.str_False命令 + properties.str_完成命令;
                    cnt = 250;
                    return;
                }
                for (int i = int_start_num; i < int_finally_num; i++)
                {
                    str_current_comment = properties.Comment[i];
                    str_temp0 = "";
                    for (int k = 0; k < str_current_comment.Length; k++)
                    {
                        if (k == 0) str_temp0 += str_current_comment[k];
                        else str_temp0 += properties.str_程式空格 + str_current_comment[k];
                    }
                    if (i != 0) str_temp1 = str_temp1 + properties.str_每段程式空格 + str_temp0;
                    else str_temp1 = str_temp1 + str_temp0;
                }
                if (str_temp1 == str_check)
                {
                    str_send = properties.str_True命令 + properties.str_完成命令;
                    cnt = 250;
                    return;
                }
                else
                {
                    str_send = properties.str_False命令 + properties.str_完成命令;
                    cnt = 250;
                    return;
                }
            }
        }
        void cnt_解析接收到訊息_79_Comment_Clear命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
               // properties.Program_緩衝區.Clear();
                properties.Comment_緩衝區.Clear();
                str_send = properties.str_完成命令.ToString();
                cnt = 250;
                return;

            }

        }

        void cnt_解析接收到訊息_80_Upload_Finally命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                if (str_receive_array.Length == 2)
                {
                    properties.Program_緩衝區寫入主程式 = true;
                    if (str_receive_array[1] == properties.str_True命令) FLAG_註解要寫入 = true;
                    else if (str_receive_array[1] == properties.str_False命令) FLAG_註解要寫入 = false;
                    str_send = properties.str_完成命令.ToString();
                }
               cnt = 250;
                return;
            }
        }
        void cnt_解析接收到訊息_81_Upload_Comment命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                if (str_receive_array.Length == 2)
                {
                    str_read_current_comment = str_receive_array[1];
                    str_send = str_read_current_comment;
                    str_send += properties.str_完成命令.ToString();
                    cnt = 250;
                    return;
                }
                else
                {
                    cnt = 250;
                    return;
                }
            }
        }
        void cnt_解析接收到訊息_82_Upload_Program命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                if (str_receive_array.Length == 2)
                {
                    str_read_current_program = str_receive_array[1];
                    str_send = str_read_current_program;
                    str_send += properties.str_完成命令.ToString();
                    cnt = 250;
                    return;
                }
                else
                {
                    cnt = 250;
                    return;
                }
            }

        }
        void cnt_解析接收到訊息_83_Upload_Program_Confirm命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                if (str_receive_array.Length == 1)
                {
                    string[] str_current_program;
                    string[] str_program = myConvert.分解分隔號字串(str_read_current_program, properties.str_每段程式空格);
                    for (int i = 0; i < str_program.Length; i++)
                    {
                        str_current_program = myConvert.分解分隔號字串(str_program[i], properties.str_程式空格);
                        properties.Program_緩衝區.Add(str_current_program);
                    }
                    str_send = properties.str_完成命令;
                    cnt = 250;
                    return;
                }
                else
                {
                    cnt = 250;
                    return;
                }
            }

        }
        void cnt_解析接收到訊息_84_Upload_Comment_Confirm命令_回覆TopMachine(ref byte cnt)
        {
            if (!IsSendDataBusy())
            {
                if (str_receive_array.Length == 1)
                {
                    string[] str_current_comment;
                    string[] str_comment = myConvert.分解分隔號字串(str_read_current_comment, properties.str_每段程式空格);
                    for (int i = 0; i < str_comment.Length; i++)
                    {
                        str_current_comment = myConvert.分解分隔號字串(str_comment[i], properties.str_程式空格);
                        properties.Comment_緩衝區.Add(str_current_comment);
                    }
                    str_send = properties.str_完成命令;
                    cnt = 250;
                    return;
                }
                else
                {
                    cnt = 250;
                    return;
                }
            }

        }

        void cnt_解析接收到訊息_250_解析結束(ref byte cnt)
        {
            cnt++;
        }
        void cnt_解析接收到訊息_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        #region 緩衝區寫入主程式
        bool FLAG_註解要寫入 = false;
        byte cnt_緩衝區寫入主程式 = 255;
        bool sub_緩衝區寫入主程式()
        {
            if (cnt_緩衝區寫入主程式 == 255) cnt_緩衝區寫入主程式 = 1;
            if (cnt_緩衝區寫入主程式 == 1) cnt_緩衝區寫入主程式_檢查是否需要寫入(ref cnt_緩衝區寫入主程式);
            if (cnt_緩衝區寫入主程式 == 2) cnt_緩衝區寫入主程式_開始寫入Program(ref cnt_緩衝區寫入主程式);
            if (cnt_緩衝區寫入主程式 == 3) cnt_緩衝區寫入主程式_寫入註解(ref cnt_緩衝區寫入主程式);
            if (cnt_緩衝區寫入主程式 == 4) cnt_緩衝區寫入主程式 = 255;

            if (cnt_緩衝區寫入主程式 == 255) return false;
            else return true;
        }
        void cnt_緩衝區寫入主程式_檢查是否需要寫入(ref byte cnt)
        {
            if (properties.Program_緩衝區寫入主程式 && Program_Run_程式現在行數 ==0) cnt++;
            else cnt = 255;
        }
        void cnt_緩衝區寫入主程式_開始寫入Program(ref byte cnt)
        {
            properties.Program.Clear();
            Program_JumpPoint = new int[20000];
            int index = 0;
            foreach(string[] str_array in properties.Program_緩衝區)
            {
                string[] str_array_temp = new string[str_array.Length];
                for (int i = 0; i < str_array.Length; i++)
                {
                     str_array_temp[i] = str_array[i];                   
                }
                if (str_array_temp[0] == "TAB")
                {
                    int temp = 0;
                    if(int.TryParse(str_array_temp[1],out temp))
                    {
                        if(temp < Program_JumpPoint.Length)
                        {
                            Program_JumpPoint[temp] = index;
                        }
                    }
                }
                properties.Program.Add(str_array_temp);
                index++;
            }
            FLAG_Program_pause_enable = new bool[properties.Program.Count];
            cnt++;
        }
        void cnt_緩衝區寫入主程式_寫入註解(ref byte cnt)
        {
            if (FLAG_註解要寫入)
            {
                properties.Comment = properties.Comment_緩衝區.DeepClone();
                if (properties.Comment.Count > 0)
                {
                    properties.Device.Clear_Comment();
                    properties.Device.Set_Comment(properties.Comment);
                }   
            }
            properties.Program_緩衝區寫入主程式 = false;
            FLAG_註解要寫入 = false;
            SaveProgram(true);
            cnt++;
        }
        void cnt_緩衝區寫入主程式_(ref byte cnt)
        {

        }
        #endregion
        private byte cnt_Program_Run = 255;
        private List<bool> FLAG_暫存堆疊區 = new List<bool>();
        private int Program_Run_程式設定行數 = 0;
        private int Program_Run_程式現在行數 = 0;

        private int[] Program_JumpPoint = new int[20000];
        private bool FLAG_Program_init = false;

        private string[] str_Program_Run;
        private bool[] FLAG_Program_pause_enable;
        private bool FLAG_paulse_command = false;
        DateTime Date = DateTime.Now;
        private bool FLAG_M8002 = false;
        private object FLAG_M8011;
        private object FLAG_M8011_Timer;
        private object FLAG_M8012;
        private object FLAG_M8012_Timer;
        private object FLAG_M8013;
        private object FLAG_M8013_Timer;
        private object FLAG_M8014;
        private object FLAG_M8014_Timer;
        private void SyStemDevice()
        {
            Date = DateTime.Now;
            properties.Device.Set_DataFast("D8013",Date.Second);
            properties.Device.Set_DataFast("D8014", Date.Minute);
            properties.Device.Set_DataFast("D8015", Date.Hour);
            properties.Device.Set_DataFast("D8016", Date.Day);
            properties.Device.Set_DataFast("D8017", Date.Month);
            properties.Device.Set_DataFast("D8018", Date.Year);
            properties.Device.Set_DataFast("D8019", (int)Date.DayOfWeek);
            properties.Device.Set_Device("M8000", true);
            properties.Device.Set_Device("M8001", false);
            if (!FLAG_M8002)
            {
                properties.Device.Set_Device("M8002", true);
                FLAG_M8002 = true;
            }
            else
            {
                properties.Device.Set_Device("M8002", false);
            }

      
            properties.Device.Get_Device("M8011", out FLAG_M8011);
            if ((bool)FLAG_M8011)
            {
                properties.device_system.Set_Device("T5", true);
                properties.device_system.Set_Device("T5", "K10", 2);
                properties.device_system.Get_Device("T5", out FLAG_M8011_Timer);
                if ((bool)FLAG_M8011_Timer)
                {
                    properties.device_system.Set_Device("T5", false);
                    properties.Device.Set_Device("M8011", false);
                }
            }
            else
            {
                properties.device_system.Set_Device("T6", true);
                properties.device_system.Set_Device("T6", "K10", 2);
                properties.device_system.Get_Device("T6", out FLAG_M8011_Timer);
                if ((bool)FLAG_M8011_Timer)
                {
                    properties.device_system.Set_Device("T6", false);
                    properties.Device.Set_Device("M8011", true);
                }
            }

            properties.Device.Get_Device("M8012", out FLAG_M8012);
            if ((bool)FLAG_M8012)
            {
                properties.device_system.Set_Device("T7", true);
                properties.device_system.Set_Device("T7", "K100", 2);
                properties.device_system.Get_Device("T7", out FLAG_M8012_Timer);
                if ((bool)FLAG_M8012_Timer)
                {
                    properties.device_system.Set_Device("T7", false);
                    properties.Device.Set_Device("M8012", false);
                }
            }
            else
            {
                properties.device_system.Set_Device("T8", true);
                properties.device_system.Set_Device("T8", "K100", 2);
                properties.device_system.Get_Device("T8", out FLAG_M8012_Timer);
                if ((bool)FLAG_M8012_Timer)
                {
                    properties.device_system.Set_Device("T8", false);
                    properties.Device.Set_Device("M8012", true);
                }
            }

 
            properties.Device.Get_Device("M8013", out FLAG_M8013);
            if ((bool)FLAG_M8013)
            {
                properties.device_system.Set_Device("T9", true);
                properties.device_system.Set_Device("T9", "K1000", 2);
                properties.device_system.Get_Device("T9", out FLAG_M8013_Timer);
                if ((bool)FLAG_M8013_Timer)
                {
                    properties.device_system.Set_Device("T9", false);
                    properties.Device.Set_Device("M8013", false);
                }
            }
            else
            {
                properties.device_system.Set_Device("T10", true);
                properties.device_system.Set_Device("T10", "K1000", 2);
                properties.device_system.Get_Device("T10", out FLAG_M8013_Timer);
                if ((bool)FLAG_M8013_Timer)
                {
                    properties.device_system.Set_Device("T10", false);
                    properties.Device.Set_Device("M8013", true);
                }
            }

  
            properties.Device.Get_Device("M8014", out FLAG_M8014);
            if ((bool)FLAG_M8014)
            {
                properties.device_system.Set_Device("T11", true);
                properties.device_system.Set_Device("T11", "K60000", 2);
                properties.device_system.Get_Device("T11", out FLAG_M8014_Timer);
                if ((bool)FLAG_M8014_Timer)
                {
                    properties.device_system.Set_Device("T11", false);
                    properties.Device.Set_Device("M8014", false);
                }
            }
            else
            {
                properties.device_system.Set_Device("T12", true);
                properties.device_system.Set_Device("T12", "K60000", 2);
                properties.device_system.Get_Device("T12", out FLAG_M8014_Timer);
                if ((bool)FLAG_M8014_Timer)
                {
                    properties.device_system.Set_Device("T12", false);
                    properties.Device.Set_Device("M8014", true);
                }
            }
        }
        private void GetInput()
        {
            bool flag = true;
            int value = 0;
            bool temp;
            for(int i = 0 ; i < 300 ; i ++)
            {
                flag = true;
                value = 0;
                char[] buf = i.ToString().ToCharArray();
                for (int k = 0; k < buf.Length; k ++ )
                {
                    if(!Int32.TryParse(buf[k].ToString(), out value))
                    {
                        flag = false; 
                        break;
                    }
                    if(value >= 8)
                    {
                        flag = false;
                        break;
                    }
                    
                }
                if (flag)
                {
                    properties.Device.Set_DeviceFast("X" , i, properties.device_system.Get_DeviceFast("X", i));
                }                      
            }
        }
        private void SetOutput()
        {
            for (int i = 0; i < 300; i++)
            {
                properties.device_system.Set_DeviceFast("Y", i, properties.Device.Get_DeviceFast("Y", i));
            }
        }
        private void ClearAxisData()
        {
            for (int i = 0; i < 30; i++)
            {
                properties.device_system.Set_DeviceFast("S" , i, false);
            }
        }
        private void SetAxisData()
        {
            for (int i = 0; i < 30; i++)
            {
               /* properties.device_system.Get_DeviceFast("S" + i.ToString(), out FLAG_temp0);
                if (!FLAG_temp0) properties.device_system.Set_DataFast("D" + i.ToString(), 0);

                properties.device_system.Get_DataFast("D" + i.ToString(),out Int32_temp0);
                properties.Device.Set_DataFast("D" + (i * 10 + 8346).ToString(), Int32_temp0);*/

                if (!properties.device_system.Get_DeviceFast("S", i)) properties.device_system.Set_DataFast("D" ,i , 0);
                properties.Device.Set_DataFast("D", (i * 10 + 8346), properties.device_system.Get_DataFast("D", i));
            }
        }
        string Device0;
        string Device1;
        string Device2;
        object object0;
        object object1;
        object object2;
        bool FLAG_temp0;
        bool FLAG_temp1;
        bool FLAG_temp2;
        Int64 Int64_temp0;
        Int64 Int64_temp1;
        Int64 Int64_temp2;
        Int32 Int32_temp0;
        Int32 Int32_temp1;
        Int32 Int32_temp2;
        private bool Program_Run()
        { 
            if (cnt_Program_Run == 1)
            {               
                ClearAxisData();
                SyStemDevice();
                GetInput();
                cnt_Program_Run++;
            }
            if (cnt_Program_Run == 255) cnt_Program_Run = 2;
            if (cnt_Program_Run == 2) cnt_Program_Run_00_提取指令(ref cnt_Program_Run);
            if (cnt_Program_Run == 3) cnt_Program_Run_00_解析指令(ref cnt_Program_Run);
            if (cnt_Program_Run == 4) cnt_Program_Run = 255;

            if (cnt_Program_Run == 10) cnt_Program_Run_10_END(ref cnt_Program_Run);
            else if (cnt_Program_Run == 11) cnt_Program_Run_11_LD(ref cnt_Program_Run);
            else if (cnt_Program_Run == 12) cnt_Program_Run_12_LDI(ref cnt_Program_Run);
            else if (cnt_Program_Run == 13) cnt_Program_Run_13_AND(ref cnt_Program_Run);
            else if (cnt_Program_Run == 14) cnt_Program_Run_14_ANI(ref cnt_Program_Run);
            else if (cnt_Program_Run == 15) cnt_Program_Run_15_OR(ref cnt_Program_Run);
            else if (cnt_Program_Run == 16) cnt_Program_Run_16_ORI(ref cnt_Program_Run);
            else if (cnt_Program_Run == 17) cnt_Program_Run_17_OUT(ref cnt_Program_Run);
            else if (cnt_Program_Run == 18) cnt_Program_Run_18_ORB(ref cnt_Program_Run);
            else if (cnt_Program_Run == 19) cnt_Program_Run_19_ANB(ref cnt_Program_Run);
            else if (cnt_Program_Run == 20) cnt_Program_Run_20_LD_Equal(ref cnt_Program_Run);
            else if (cnt_Program_Run == 21) cnt_Program_Run_21_LDD_Equal(ref cnt_Program_Run);
            else if (cnt_Program_Run == 22) cnt_Program_Run_22_AND_Equal(ref cnt_Program_Run);
            else if (cnt_Program_Run == 23) cnt_Program_Run_23_ANDD_Equal(ref cnt_Program_Run);
            else if (cnt_Program_Run == 24) cnt_Program_Run_24_OR_Equal(ref cnt_Program_Run);
            else if (cnt_Program_Run == 25) cnt_Program_Run_25_ORD_Equal(ref cnt_Program_Run);

            else if (cnt_Program_Run == 32) cnt_Program_Run_32_MOV(ref cnt_Program_Run);
            else if (cnt_Program_Run == 33) cnt_Program_Run_33_DMOV(ref cnt_Program_Run);
            else if (cnt_Program_Run == 34) cnt_Program_Run_34_ADD(ref cnt_Program_Run);
            else if (cnt_Program_Run == 35) cnt_Program_Run_35_DADD(ref cnt_Program_Run);
            else if (cnt_Program_Run == 36) cnt_Program_Run_36_SUB(ref cnt_Program_Run);
            else if (cnt_Program_Run == 37) cnt_Program_Run_37_DSUB(ref cnt_Program_Run);
            else if (cnt_Program_Run == 38) cnt_Program_Run_38_MUL(ref cnt_Program_Run);
            else if (cnt_Program_Run == 39) cnt_Program_Run_39_DMUL(ref cnt_Program_Run);
            else if (cnt_Program_Run == 40) cnt_Program_Run_40_DIV(ref cnt_Program_Run);
            else if (cnt_Program_Run == 41) cnt_Program_Run_41_DDIV(ref cnt_Program_Run);
            else if (cnt_Program_Run == 42) cnt_Program_Run_42_INC(ref cnt_Program_Run);
            else if (cnt_Program_Run == 43) cnt_Program_Run_43_DINC(ref cnt_Program_Run);
            else if (cnt_Program_Run == 44) cnt_Program_Run_44_SET(ref cnt_Program_Run);
            else if (cnt_Program_Run == 45) cnt_Program_Run_45_RST(ref cnt_Program_Run);
            else if (cnt_Program_Run == 46) cnt_Program_Run_46_ZRST(ref cnt_Program_Run);
            else if (cnt_Program_Run == 47) cnt_Program_Run_47_BMOV(ref cnt_Program_Run);
            else if (cnt_Program_Run == 48) cnt_Program_Run_48_WTB(ref cnt_Program_Run);
            else if (cnt_Program_Run == 49) cnt_Program_Run_49_DWTB(ref cnt_Program_Run);
            else if (cnt_Program_Run == 50) cnt_Program_Run_50_BTW(ref cnt_Program_Run);
            else if (cnt_Program_Run == 51) cnt_Program_Run_51_DBTW(ref cnt_Program_Run);

            else if (cnt_Program_Run == 60) cnt_Program_Run_60_DRVA(ref cnt_Program_Run);
            else if (cnt_Program_Run == 61) cnt_Program_Run_61_DDRVA(ref cnt_Program_Run);
            else if (cnt_Program_Run == 62) cnt_Program_Run_62_DRVI(ref cnt_Program_Run);
            else if (cnt_Program_Run == 63) cnt_Program_Run_63_DDRVI(ref cnt_Program_Run);
            else if (cnt_Program_Run == 64) cnt_Program_Run_64_PLSV(ref cnt_Program_Run);
            else if (cnt_Program_Run == 65) cnt_Program_Run_65_DPLSV(ref cnt_Program_Run);

            else if (cnt_Program_Run == 70) cnt_Program_Run_70_JUMP(ref cnt_Program_Run);
            else if (cnt_Program_Run == 71) cnt_Program_Run_71_REF(ref cnt_Program_Run);
            
            if (cnt_Program_Run == 254)
            {
                SetOutput();
                SetAxisData();               
                cnt_Program_Run = 1;
                if (this.掃描速度 >= 0) System.Threading.Thread.Sleep(this.掃描速度);
            }
            return (cnt_Program_Run == 254);



        } 
        void cnt_Program_Run_00_提取指令(ref byte cnt)
        {
            FLAG_paulse_command = false;
            if (properties.Program_緩衝區寫入主程式 && Program_Run_程式現在行數 == 0)
            {
                cnt = 1;
                return;
            }
            if (Program_Run_程式現在行數 < properties.Program.Count)
            {
                str_Program_Run = properties.Program[Program_Run_程式現在行數];
                cnt++;
            }
            else
            {
                Program_Run_程式現在行數 = 0;

            }
           
        }
        void cnt_Program_Run_00_解析指令(ref byte cnt)
        {
            if (str_Program_Run[0] == "END")
            {
                cnt = 10;
                return;
            }
            else if (str_Program_Run[0] == "LD")
            {
                cnt = 11;
                return;
            }
            else if (str_Program_Run[0] == "LDI")
            {
                cnt = 12;
                return;
            }
            else if (str_Program_Run[0] == "AND")
            {
                cnt = 13;
                return;
            }
            else if (str_Program_Run[0] == "ANI")
            {
                cnt = 14;
                return;
            }
            else if (str_Program_Run[0] == "OR")
            {
                cnt = 15;
                return;
            }
            else if (str_Program_Run[0] == "ORI")
            {
                cnt = 16;
                return;
            }
            else if (str_Program_Run[0] == "OUT")
            {
                cnt = 17;
                return;
            }
            else if (str_Program_Run[0] == "ORB")
            {
                cnt = 18;
                return;
            }
            else if (str_Program_Run[0] == "ANB")
            {
                cnt = 19;
                return;
            }
            else if (str_Program_Run[0] == "LD=" || str_Program_Run[0] == "LD>=" || str_Program_Run[0] == "LD<=" || str_Program_Run[0] == "LD>" || str_Program_Run[0] == "LD<" || str_Program_Run[0] == "LD<>")
            {
                cnt = 20;
                return;
            }
            else if (str_Program_Run[0] == "LDD=" || str_Program_Run[0] == "LDD>=" || str_Program_Run[0] == "LDD<=" || str_Program_Run[0] == "LDD>" || str_Program_Run[0] == "LDD<" || str_Program_Run[0] == "LDD<>")
            {
                cnt = 21;
                return;
            }
            else if (str_Program_Run[0] == "AND=" || str_Program_Run[0] == "AND>=" || str_Program_Run[0] == "AND<=" || str_Program_Run[0] == "AND>" || str_Program_Run[0] == "AND<")
            {
                cnt = 22;
                return;
            }
            else if (str_Program_Run[0] == "ANDD=" || str_Program_Run[0] == "ANDD>=" || str_Program_Run[0] == "ANDD<=" || str_Program_Run[0] == "ANDD>" || str_Program_Run[0] == "ANDD<")
            {
                cnt = 23;
                return;
            }
            else if (str_Program_Run[0] == "OR=" || str_Program_Run[0] == "OR>=" || str_Program_Run[0] == "OR<=" || str_Program_Run[0] == "OR>" || str_Program_Run[0] == "OR<")
            {
                cnt = 24;
                return;
            }
            else if (str_Program_Run[0] == "ORD=" || str_Program_Run[0] == "ORD>=" || str_Program_Run[0] == "ORD<=" || str_Program_Run[0] == "ORD>" || str_Program_Run[0] == "ORD<")
            {
                cnt = 25;
                return;
            }
            else if (str_Program_Run[0] == "MOV")
            {
                cnt = 32;
                return;
            }
            else if (str_Program_Run[0] == "MOVP")
            {
                FLAG_paulse_command = true;
                cnt = 32;
                return;
            }
            else if (str_Program_Run[0] == "DMOV")
            {
                cnt = 33;
                return;
            }
            else if (str_Program_Run[0] == "DMOVP")
            {
                FLAG_paulse_command = true;
                cnt = 33;
                return;
            }
            else if (str_Program_Run[0] == "ADD")
            {
                cnt = 34;
                return;
            }
            else if (str_Program_Run[0] == "ADDP")
            {
                FLAG_paulse_command = true;
                cnt = 34;
                return;
            }
            else if (str_Program_Run[0] == "DADD")
            {
                cnt = 35;
                return;
            }
            else if (str_Program_Run[0] == "DADDP")
            {
                FLAG_paulse_command = true;
                cnt = 35;
                return;
            }
            else if (str_Program_Run[0] == "SUB")
            {
                cnt = 36;
                return;
            }
            else if (str_Program_Run[0] == "SUBP")
            {
                FLAG_paulse_command = true;
                cnt = 36;
                return;
            }
            else if (str_Program_Run[0] == "DSUB")
            {
                cnt = 37;
                return;
            }
            else if (str_Program_Run[0] == "DSUBP")
            {
                FLAG_paulse_command = true;
                cnt = 37;
                return;
            }
            else if (str_Program_Run[0] == "MUL")
            {
                cnt = 38;
                return;
            }
            else if (str_Program_Run[0] == "MULP")
            {
                FLAG_paulse_command = true;
                cnt = 38;
                return;
            }
            else if (str_Program_Run[0] == "DMUL")
            {
                cnt = 39;
                return;
            }
            else if (str_Program_Run[0] == "DMULP")
            {
                FLAG_paulse_command = true;
                cnt = 39;
                return;
            }
            else if (str_Program_Run[0] == "DIV")
            {
                cnt = 40;
                return;
            }
            else if (str_Program_Run[0] == "DIVP")
            {
                FLAG_paulse_command = true;
                cnt = 40;
                return;
            }
            else if (str_Program_Run[0] == "DDIV")
            {
                cnt = 41;
                return;
            }
            else if (str_Program_Run[0] == "DDIVP")
            {
                FLAG_paulse_command = true;
                cnt = 41;
                return;
            }
            else if (str_Program_Run[0] == "INC")
            {
                cnt = 42;
                return;
            }
            else if (str_Program_Run[0] == "INCP")
            {
                FLAG_paulse_command = true;
                cnt = 42;
                return;
            }
            else if (str_Program_Run[0] == "DINC")
            {
                cnt = 43;
                return;
            }
            else if (str_Program_Run[0] == "DINCP")
            {
                FLAG_paulse_command = true;
                cnt = 43;
                return;
            }
            else if (str_Program_Run[0] == "SET")
            {
                cnt = 44;
                return;
            }
            else if (str_Program_Run[0] == "RST")
            {
                cnt = 45;
                return;
            }
            else if (str_Program_Run[0] == "ZRST")
            {
                cnt = 46;
                return;
            }
            else if (str_Program_Run[0] == "ZRSTP")
            {
                FLAG_paulse_command = true;
                cnt = 46;
                return;
            }
            else if (str_Program_Run[0] == "BMOV")
            {
                cnt = 47;
                return;
            }
            else if (str_Program_Run[0] == "BMOVP")
            {
                FLAG_paulse_command = true;
                cnt = 47;
                return;
            }
            else if (str_Program_Run[0] == "WTB")
            {
                cnt = 48;
                return;
            }
            else if (str_Program_Run[0] == "WTBP")
            {
                FLAG_paulse_command = true;
                cnt = 48;
                return;
            }
            else if (str_Program_Run[0] == "DWTB")
            {
                cnt = 49;
                return;
            }
            else if (str_Program_Run[0] == "DWTBP")
            {
                FLAG_paulse_command = true;
                cnt = 49;
                return;
            }
            else if (str_Program_Run[0] == "BTW")
            {
                cnt = 50;
                return;
            }
            else if (str_Program_Run[0] == "BTWP")
            {
                FLAG_paulse_command = true;
                cnt = 50;
                return;
            }
            else if (str_Program_Run[0] == "DBTW")
            {
                cnt = 51;
                return;
            }
            else if (str_Program_Run[0] == "DBTWP")
            {
                FLAG_paulse_command = true;
                cnt = 51;
                return;
            }
            else if (str_Program_Run[0] == "DRVA")
            {
                cnt = 60;
                return;
            }
            else if (str_Program_Run[0] == "DDRVA")
            {
                cnt = 61;
                return;
            }
            else if (str_Program_Run[0] == "DRVI")
            {
                cnt = 62;
                return;
            }
            else if (str_Program_Run[0] == "DDRVI")
            {
                cnt = 63;
                return;
            }
            else if (str_Program_Run[0] == "PLSV")
            {
                cnt = 64;
                return;
            }
            else if (str_Program_Run[0] == "DPLSV")
            {
                cnt = 65;
                return;
            }
            else if (str_Program_Run[0] == "JUMP")
            {
                cnt = 70;
                return;
            }
            else if (str_Program_Run[0] == "REF")
            {
                cnt = 71;
                return;
            }
            else
            {
                Program_Run_程式現在行數++;
                cnt ++;
                return;
            }                      
        }

        void cnt_Program_Run_10_END(ref byte cnt)
        {
            Program_Run_程式現在行數 = 0;
            CycleTime = stopwatch.Elapsed.TotalMilliseconds - CycleTime_start;
            CycleTime_start = stopwatch.Elapsed.TotalMilliseconds;        
            cnt = 254;
        }
        void cnt_Program_Run_11_LD(ref byte cnt)
        {
            properties.Device.Get_DeviceFast(str_Program_Run[1], out FLAG_temp0);
            FLAG_暫存堆疊區.Add(FLAG_temp0);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_12_LDI(ref byte cnt)
        {
            properties.Device.Get_DeviceFast(str_Program_Run[1], out FLAG_temp0);
            FLAG_暫存堆疊區.Add(!FLAG_temp0);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_13_AND(ref byte cnt)
        {
            FLAG_temp0 = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
            properties.Device.Get_DeviceFast(str_Program_Run[1], out FLAG_temp1);
            FLAG_temp0 = FLAG_temp0 && FLAG_temp1;
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            FLAG_暫存堆疊區.Add(FLAG_temp0);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_14_ANI(ref byte cnt)
        {
            FLAG_temp0 = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
            properties.Device.Get_DeviceFast(str_Program_Run[1], out FLAG_temp1);
            FLAG_temp0 = FLAG_temp0 && !FLAG_temp1;
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            FLAG_暫存堆疊區.Add(FLAG_temp0);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_15_OR(ref byte cnt)
        {
            FLAG_temp0 = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
            properties.Device.Get_DeviceFast(str_Program_Run[1], out FLAG_temp1);
            FLAG_temp0 = FLAG_temp0 || FLAG_temp1;
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            FLAG_暫存堆疊區.Add(FLAG_temp0);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_16_ORI(ref byte cnt)
        {
            FLAG_temp0 = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
            properties.Device.Get_DeviceFast(str_Program_Run[1], out FLAG_temp1);
            FLAG_temp0 = FLAG_temp0 || !FLAG_temp1;
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            FLAG_暫存堆疊區.Add(FLAG_temp0);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_17_OUT(ref byte cnt)
        {
            if (str_Program_Run.Length == 2)
            {
                FLAG_temp0 = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
                properties.Device.Set_DeviceFast(str_Program_Run[1], FLAG_temp0);
                FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
                Program_Run_程式現在行數++;
                cnt = 255;
            }
            else if (str_Program_Run.Length == 3)
            {
                FLAG_temp0 = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
                string TimeValue = str_Program_Run[2];
                properties.Device.Set_DeviceFast(str_Program_Run[1], FLAG_temp0);
                properties.Device.Set_Device(str_Program_Run[1], TimeValue, 2);
                FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
                Program_Run_程式現在行數++;
                cnt = 255;
            }
        }
        void cnt_Program_Run_18_ORB(ref byte cnt)
        {
            FLAG_temp0 = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
            FLAG_temp1 = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 2];
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            FLAG_暫存堆疊區.Add(FLAG_temp0 || FLAG_temp1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_19_ANB(ref byte cnt)
        {
            FLAG_temp0 = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
            FLAG_temp1 = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 2];
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            FLAG_暫存堆疊區.Add(FLAG_temp0 && FLAG_temp1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_20_LD_Equal(ref byte cnt)
        {
            FLAG_temp0 = false;

            Device0 = str_Program_Run[1];
            Device1 = str_Program_Run[2];
            properties.Device.Get_DataFast(Device0, out Int32_temp0);
            properties.Device.Get_DataFast(Device1, out Int32_temp1);

            if (str_Program_Run[0] == "LD=")
            {
                if (Int32_temp0 == Int32_temp1) FLAG_temp0 = true;
                else FLAG_temp0 = false;
            }
            else if (str_Program_Run[0] == "LD>=")
            {
                if (Int32_temp0 >= Int32_temp1) FLAG_temp0 = true;
                else FLAG_temp0 = false;
            }
            else if (str_Program_Run[0] == "LD<=")
            {
                if (Int32_temp0 <= Int32_temp1) FLAG_temp0 = true;
                else FLAG_temp0 = false;
            }
            else if (str_Program_Run[0] == "LD>")
            {
                if (Int32_temp0 > Int32_temp1) FLAG_temp0 = true;
                else FLAG_temp0 = false;
            }
            else if (str_Program_Run[0] == "LD<")
            {
                if (Int32_temp0 < Int32_temp1) FLAG_temp0 = true;
                else FLAG_temp0 = false;
            }
            else if (str_Program_Run[0] == "LD<>")
            {
                if (Int32_temp0 != Int32_temp1) FLAG_temp0 = true;
                else FLAG_temp0 = false;
            }
            FLAG_暫存堆疊區.Add(FLAG_temp0);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_21_LDD_Equal(ref byte cnt)
        {
            FLAG_temp0 = false;
            Int64_temp0 = properties.Device.Get_DoubleWord(str_Program_Run[1]);
            Int64_temp1 = properties.Device.Get_DoubleWord(str_Program_Run[2]);
            if (str_Program_Run[0] == "LDD=")
            {
                if (Int64_temp0 == Int64_temp1) FLAG_temp0 = true;
                else FLAG_temp0 = false;
            }
            else if (str_Program_Run[0] == "LDD>=")
            {
                if (Int64_temp0 >= Int64_temp1) FLAG_temp0 = true;
                else FLAG_temp0 = false;
            }
            else if (str_Program_Run[0] == "LDD<=")
            {
                if (Int64_temp0 <= Int64_temp1) FLAG_temp0 = true;
                else FLAG_temp0 = false;
            }
            else if (str_Program_Run[0] == "LDD>")
            {
                if (Int64_temp0 > Int64_temp1) FLAG_temp0 = true;
                else FLAG_temp0 = false;
            }
            else if (str_Program_Run[0] == "LDD<")
            {
                if (Int64_temp0 < Int64_temp1) FLAG_temp0 = true;
                else FLAG_temp0 = false;
            }
            else if (str_Program_Run[0] == "LDD<>")
            {
                if (Int64_temp0 != Int64_temp1) FLAG_temp0 = true;
                else FLAG_temp0 = false;
            }
            FLAG_暫存堆疊區.Add(FLAG_temp0);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_22_AND_Equal(ref byte cnt)
        {
            FLAG_temp0 = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
            FLAG_temp1 = false;
            Device0 = str_Program_Run[1];
            Device1 = str_Program_Run[2];
            properties.Device.Get_DataFast(Device0, out Int32_temp0);
            properties.Device.Get_DataFast(Device1, out Int32_temp1);

            if (str_Program_Run[0] == "AND=")
            {
                if (Int32_temp0 == Int32_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "AND>=")
            {
                if (Int32_temp0 >= Int32_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "AND<=")
            {
                if (Int32_temp0 <= Int32_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "AND>")
            {
                if (Int32_temp0 > Int32_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "AND<")
            {
                if (Int32_temp0 < Int32_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            FLAG_暫存堆疊區.Add(FLAG_temp0 && FLAG_temp1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_23_ANDD_Equal(ref byte cnt)
        {
            FLAG_temp0 = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
            FLAG_temp1 = false;
            Int64_temp0 = properties.Device.Get_DoubleWord(str_Program_Run[1]);
            Int64_temp1 = properties.Device.Get_DoubleWord(str_Program_Run[2]);
            if (str_Program_Run[0] == "ANDD=")
            {
                if (Int64_temp0 == Int64_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "ANDD>=")
            {
                if (Int64_temp0 >= Int64_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "ANDD<=")
            {
                if (Int64_temp0 <= Int64_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "ANDD>")
            {
                if (Int64_temp0 > Int64_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "ANDD<")
            {
                if (Int64_temp0 < Int64_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            FLAG_暫存堆疊區.Add(FLAG_temp0 && FLAG_temp1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_24_OR_Equal(ref byte cnt)
        {
            FLAG_temp0 = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
            FLAG_temp1 = false;
            Device0 = str_Program_Run[1];
            Device1 = str_Program_Run[2];
            properties.Device.Get_DataFast(Device0, out Int32_temp0);
            properties.Device.Get_DataFast(Device1, out Int32_temp1);

            if (str_Program_Run[0] == "OR=")
            {
                if (Int32_temp0 == Int32_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "OR>=")
            {
                if (Int32_temp0 >= Int32_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "OR<=")
            {
                if (Int32_temp0 <= Int32_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "OR>")
            {
                if (Int32_temp0 > Int32_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "OR<")
            {
                if (Int32_temp0 < Int32_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            FLAG_暫存堆疊區.Add(FLAG_temp0 || FLAG_temp1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_25_ORD_Equal(ref byte cnt)
        {
            FLAG_temp0 = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
            FLAG_temp1 = false;
            Int64_temp0 = properties.Device.Get_DoubleWord(str_Program_Run[1]);
            Int64_temp1 = properties.Device.Get_DoubleWord(str_Program_Run[2]);
            if (str_Program_Run[0] == "ORD=")
            {
                if (Int64_temp0 == Int64_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "ORD>=")
            {
                if (Int64_temp0 >= Int64_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "ORD<=")
            {
                if (Int64_temp0 <= Int64_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "ORD>")
            {
                if (Int64_temp0 > Int64_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            else if (str_Program_Run[0] == "ORD<")
            {
                if (Int64_temp0 < Int64_temp1) FLAG_temp1 = true;
                else FLAG_temp1 = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            FLAG_暫存堆疊區.Add(FLAG_temp0 || FLAG_temp1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }

        void cnt_Program_Run_32_MOV(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                if ((!FLAG_paulse_command)||(FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    Device0 = str_Program_Run[1];
                    Device1 = str_Program_Run[2];
                    if (Device0.Substring(0, 1) == "T")
                    {
                        properties.Device.Get_Device(Device0, 2, out object0);
                        Int32_temp0 = (int)object0;
                    }
                    else
                    {
                        properties.Device.Get_DataFast(Device0, out Int32_temp0);
                    }
                    properties.Device.Set_DataFast(Device1, Int32_temp0);
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
     
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_33_DMOV(ref byte cnt)
        {          
          
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    Device0 = str_Program_Run[1];
                    Device1 = str_Program_Run[2];
                    Int64_temp0 = properties.Device.Get_DoubleWord(str_Program_Run[1]);
                    properties.Device.Set_DoubleWord(Device1, Int64_temp0);
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_34_ADD(ref byte cnt)
        {    
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    Device0 = str_Program_Run[1];
                    Device1 = str_Program_Run[2];
                    Device2 = str_Program_Run[3];
                    properties.Device.Get_DataFast(Device0, out Int32_temp0);
                    properties.Device.Get_DataFast(Device1, out Int32_temp1);
                    properties.Device.Set_DataFast(Device2, Int32_temp0 + Int32_temp1);
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_35_DADD(ref byte cnt)
        {   
            bool FLAG_temp = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
            if (FLAG_temp)
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    Device0 = str_Program_Run[1];
                    Device1 = str_Program_Run[2];
                    Device2 = str_Program_Run[3];
                    Int64_temp0 = properties.Device.Get_DoubleWord(Device0);
                    Int64_temp1 = properties.Device.Get_DoubleWord(Device1);
                    properties.Device.Set_DoubleWord(Device2, Int64_temp0 + Int64_temp1);
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_36_SUB(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    Device0 = str_Program_Run[1];
                    Device1 = str_Program_Run[2];
                    Device2 = str_Program_Run[3];
                    properties.Device.Get_DataFast(Device0, out Int32_temp0);
                    properties.Device.Get_DataFast(Device1, out Int32_temp1);
                    properties.Device.Set_DataFast(Device2, Int32_temp0 - Int32_temp1);
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_37_DSUB(ref byte cnt)
        {
            bool FLAG_temp = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
            if (FLAG_temp)
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    Device0 = str_Program_Run[1];
                    Device1 = str_Program_Run[2];
                    Device2 = str_Program_Run[3];
                    properties.Device.Get_DataFast(Device0, out Int32_temp0);
                    properties.Device.Get_DataFast(Device1, out Int32_temp1);
                    properties.Device.Set_DataFast(Device2, Int32_temp0 * Int32_temp1);
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_38_MUL(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    Device0 = str_Program_Run[1];
                    Device1 = str_Program_Run[2];
                    Device2 = str_Program_Run[3];
                    properties.Device.Get_Device(Device0, out object0);
                    properties.Device.Get_Device(Device1, out object1);
                    properties.Device.Set_Device(Device2, (int)object0 * (int)object1);
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_39_DMUL(ref byte cnt)
        {
            bool FLAG_temp = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
            if (FLAG_temp)
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    Device0 = str_Program_Run[1];
                    Device1 = str_Program_Run[2];
                    Device2 = str_Program_Run[3];
                    Int64_temp0 = properties.Device.Get_DoubleWord(Device0);
                    Int64_temp1 = properties.Device.Get_DoubleWord(Device1);
                    properties.Device.Set_DoubleWord(Device2, Int64_temp0 * Int64_temp1);
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_40_DIV(ref byte cnt)
        {
            if ( FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
 
                    Device0 = str_Program_Run[1];
                    Device1 = str_Program_Run[2];
                    Device2 = str_Program_Run[3];
                    properties.Device.Get_DataFast(Device0, out Int32_temp0);
                    properties.Device.Get_DataFast(Device1, out Int32_temp1);

                    if (Int32_temp0 != 0 && Int32_temp1 != 0)
                    {
                        int temp = Int32_temp0 / Int32_temp1;
                        properties.Device.Set_Device(Device2, temp);
                        temp = Int32_temp0 % Int32_temp1;
                        properties.Device.Set_Device(DEVICE.DeviceOffset(Device2, 1), temp);
                    }
                    else
                    {
                        properties.Device.Set_Device(Device2, 0);
                        properties.Device.Set_Device(DEVICE.DeviceOffset(Device2, 1), 0);
                    }
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_41_DDIV(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    Device0 = str_Program_Run[1];
                    Device1 = str_Program_Run[2];
                    Device2 = str_Program_Run[3];
                    Int64_temp0 = properties.Device.Get_DoubleWord(Device0);
                    Int64_temp1 = properties.Device.Get_DoubleWord(Device1);
                    if (Int64_temp0 != 0 && Int64_temp1 != 0)
                    {
                        properties.Device.Set_DoubleWord(Device2, Int64_temp0 / Int64_temp1);
                        properties.Device.Set_DoubleWord(DEVICE.DeviceOffset(Device2, 2), Int64_temp0 % Int64_temp1);
                    }
                    else
                    {
                        properties.Device.Set_DoubleWord(Device2, 0);
                        properties.Device.Set_DoubleWord(DEVICE.DeviceOffset(Device2, 2),0);
                    }
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_42_INC(ref byte cnt)
        {
           
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    Device0 = str_Program_Run[1];
                    properties.Device.Get_DataFast(Device0, out Int32_temp0);
                    properties.Device.Set_DataFast(Device0, Int32_temp0 + 1);
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_43_DINC(ref byte cnt)
        {
            bool FLAG_temp = FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1];
            if (FLAG_temp)
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    Device0 = str_Program_Run[1];
                    Int64_temp0 = properties.Device.Get_DoubleWord(str_Program_Run[1]);
                    properties.Device.Set_DoubleWord(Device0, Int64_temp0 + 1); ;
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_44_SET(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                Device0 = str_Program_Run[1];
                properties.Device.Set_Device(Device0, true);
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_45_RST(ref byte cnt)
        {        
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                Device0 = str_Program_Run[1];
                properties.Device.Set_Device(Device0, false);
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_46_ZRST(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    string DeviceType = str_Program_Run[1].Substring(0, 1);
                    int num_start = Convert.ToInt32(str_Program_Run[1].Substring(1, str_Program_Run[1].Length - 1));
                    int num_over = Convert.ToInt32(str_Program_Run[2].Substring(1, str_Program_Run[2].Length - 1));
                    if (DeviceType == "X" || DeviceType == "Y" || DeviceType == "M" || DeviceType == "S")
                    {
                        for (int i = num_start; i <= num_over; i++)
                        {
                            properties.Device.Set_Device(DeviceType + i.ToString(), false);
                        }
                    }
                    else if (DeviceType == "D" || DeviceType == "R")
                    {
                        for (int i = num_start; i <= num_over; i++)
                        {
                            properties.Device.Set_Device(DeviceType + i.ToString(), 0);
                        }
                    }
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_47_BMOV(ref byte cnt)
        {
          
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    object temp;
                    Device0 = properties.Device.DeviceIndex(str_Program_Run[1]);
                    Device1 = properties.Device.DeviceIndex(str_Program_Run[3]);
                    string DeviceType0 = Device0.Substring(0, 1);
                    int num0_start = Convert.ToInt32(Device0.Substring(1, Device0.Length - 1));
                    string DeviceType1 = Device1.Substring(0, 1);
                    int num1_start = Convert.ToInt32(Device1.Substring(1, Device1.Length - 1));
                    object num = new object();
                    properties.Device.Get_Device(properties.Device.DeviceIndex(str_Program_Run[2]), out num);
                    for (int i = 0; i < (int)num; i++)
                    {
                        temp = new object();
                        properties.Device.Get_Device(DeviceType0 + (num0_start + i).ToString(), out temp);
                        properties.Device.Set_Device(DeviceType1 + (num1_start + i).ToString(), (int)temp);
                    }
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_48_WTB(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    str_Program_Run[3] = properties.Device.DeviceIndex(str_Program_Run[3]);
                    string DeviceType1 = str_Program_Run[3].Substring(0, 1);
                    int num1_start = Convert.ToInt32(str_Program_Run[3].Substring(1, str_Program_Run[3].Length - 1));
                    object num = new object();
                    properties.Device.Get_Device(str_Program_Run[2], out num);
                    object value = new object();

                    if ((int)num > 32) num = 32;
                    properties.Device.Get_Device(str_Program_Run[1], out value);
                    for (int i = 0; i < (int)num; i++)
                    {
                        bool flag = myConvert.Int32GetBit((int)value, i);
                        properties.Device.Set_Device(DeviceType1 + (num1_start + i).ToString(), flag);
                    }
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }
            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_49_DWTB(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    str_Program_Run[3] = properties.Device.DeviceIndex(str_Program_Run[3]);
                    string DeviceType1 = str_Program_Run[3].Substring(0, 1);
                    int num1_start = Convert.ToInt32(str_Program_Run[3].Substring(1, str_Program_Run[3].Length - 1));
                    object num = new object();
                    properties.Device.Get_Device(str_Program_Run[2], out num);
                    if ((int)num > 64) num = 64;
                    Int64 value = properties.Device.Get_DoubleWord(str_Program_Run[1].ToString());
                    for (int i = 0; i < (int)num; i++)
                    {
                        bool flag = myConvert.Int64GetBit(value, i);
                        properties.Device.Set_Device(DeviceType1 + (num1_start + i).ToString(), flag);
                    }
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }

            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_50_BTW(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    str_Program_Run[1] = properties.Device.DeviceIndex(str_Program_Run[1]);
                    string DeviceType0 = str_Program_Run[1].Substring(0, 1);
                    int num0_start = Convert.ToInt32(str_Program_Run[1].Substring(1, str_Program_Run[1].Length - 1));
                    object num = new object();
                    properties.Device.Get_Device(str_Program_Run[2], out num);

                    Int32 value = 0;
                    if ((int)num > 32) num = 32;
                    for (int i = 0; i < (int)num; i++)
                    {
                        object flag = new object();
                        properties.Device.Get_Device(DeviceType0 + (i + num0_start).ToString(), out flag);
                        value = myConvert.Int32SetBit((bool)flag, value, i);
                    }

                    properties.Device.Set_Device(str_Program_Run[3], (int)value);
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }

            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_51_DBTW(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                if ((!FLAG_paulse_command) || (FLAG_paulse_command && !FLAG_Program_pause_enable[Program_Run_程式現在行數]))
                {
                    str_Program_Run[1] = properties.Device.DeviceIndex(str_Program_Run[1]);
                    string DeviceType0 = str_Program_Run[1].Substring(0, 1);
                    int num0_start = Convert.ToInt32(str_Program_Run[1].Substring(1, str_Program_Run[1].Length - 1));
                    object num = new object();
                    properties.Device.Get_Device(str_Program_Run[2], out num);

                    Int64 value = 0;
                    if ((int)num > 32) num = 32;
                    for (int i = 0; i < (int)num; i++)
                    {
                        object flag = new object();
                        properties.Device.Get_Device(DeviceType0 + (i + num0_start).ToString(), out flag);
                        value = myConvert.Int64SetBit((bool)flag, value, i);
                    }

                    properties.Device.Set_DoubleWord(str_Program_Run[3], value);
                    FLAG_Program_pause_enable[Program_Run_程式現在行數] = true;
                }

            }
            else
            {
                FLAG_Program_pause_enable[Program_Run_程式現在行數] = false;
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }

        void cnt_Program_Run_60_DRVA(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                String str_temp ="";
                Device0 = str_Program_Run[1];
                Device1 = str_Program_Run[2];
                Device2 = str_Program_Run[3];
                properties.Device.Get_DataFast(Device0, out Int32_temp0);
                properties.Device.Get_DataFast(Device1, out Int32_temp1);
                properties.Device.Get_DataFast(Device2, out Int32_temp2);
                str_temp = DEVICE.DeviceOffset(axis_speed_data, Int32_temp2 * 10);
                properties.Device.Set_DataFast(str_temp, Int32_temp0);
                str_temp = DEVICE.DeviceOffset(axis_position_data, Int32_temp2 * 10);
                properties.Device.Set_DataFast(str_temp, Int32_temp1);
                properties.device_system.Set_DataFast("D" + Int32_temp2.ToString(), 1);
                properties.device_system.Set_DeviceFast("S" + Int32_temp2.ToString(), true);
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_61_DDRVA(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                String str_temp = "";
                Device0 = str_Program_Run[1];
                Device1 = str_Program_Run[2];
                Device2 = str_Program_Run[3];
                properties.Device.Get_Device(Device0, out object0);
                properties.Device.Get_Device(Device1, out object1);
                properties.Device.Get_Device(Device2, out object2);
                str_temp = DEVICE.DeviceOffset(axis_speed_data, (int)object2 * 10);
                properties.Device.Set_Device(str_temp, (int)object0);
                str_temp = DEVICE.DeviceOffset(axis_position_data, (int)object2 * 10);
                properties.Device.Set_Device(str_temp, (int)object1);
                properties.device_system.Set_Device("D" + object2.ToString(), 1);
                properties.device_system.Set_DeviceFast("S" + object2.ToString(), true);

            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_62_DRVI(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                String str_temp = "";
                Device0 = str_Program_Run[1];
                Device1 = str_Program_Run[2];
                Device2 = str_Program_Run[3];
                properties.Device.Get_DataFast(Device0, out Int32_temp0);
                properties.Device.Get_DataFast(Device1, out Int32_temp1);
                properties.Device.Get_DataFast(Device2, out Int32_temp2);
                str_temp = DEVICE.DeviceOffset(axis_speed_data, Int32_temp2 * 10);
                properties.Device.Set_DataFast(str_temp, Int32_temp0);
                str_temp = DEVICE.DeviceOffset(axis_position_data, Int32_temp2 * 10);
                properties.Device.Set_DataFast(str_temp, Int32_temp1);
                properties.device_system.Set_DataFast("D" + Int32_temp2.ToString(), 2);
                properties.device_system.Set_DeviceFast("S" + Int32_temp2.ToString(), true);
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_63_DDRVI(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                String str_temp = "";
    
                Device0 = str_Program_Run[1];
                Device1 = str_Program_Run[2];
                Device2 = str_Program_Run[3];
                properties.Device.Get_Device(Device0, out object0);
                properties.Device.Get_Device(Device1, out object1);
                properties.Device.Get_Device(Device2, out object2);
                str_temp = DEVICE.DeviceOffset(axis_speed_data, (int)object2 * 10);
                properties.Device.Set_Device(str_temp, (int)object0);
                str_temp = DEVICE.DeviceOffset(axis_position_data, (int)object2 * 10);
                properties.Device.Set_Device(str_temp, (int)object1);
                properties.device_system.Set_Device("D" + object2.ToString(), 2);
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_64_PLSV(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                String str_temp = "";
                Device0 = str_Program_Run[1];
                Device1 = str_Program_Run[2];
                properties.Device.Get_DataFast(Device0, out Int32_temp0);//速度
                properties.Device.Get_DataFast(Device1, out Int32_temp1);//軸號

                str_temp = DEVICE.DeviceOffset(axis_speed_data, Int32_temp1 * 10);
                properties.Device.Set_DataFast(str_temp, Int32_temp0);

                str_temp = DEVICE.DeviceOffset(axis_jog_acc_enable, Int32_temp1 * 10);
                properties.Device.Get_DeviceFast(str_temp, out FLAG_temp0);

                if (FLAG_temp0) properties.device_system.Set_DataFast("D" + Int32_temp1.ToString(), 4);
                else properties.device_system.Set_DataFast("D" + Int32_temp1.ToString(), 3);
                properties.device_system.Set_DeviceFast("S" + Int32_temp1.ToString(), true);
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_65_DPLSV(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                String str_temp = "";
                Device0 = str_Program_Run[1];
                Device1 = str_Program_Run[2];
                properties.Device.Get_Device(Device0, out object0);
                properties.Device.Get_Device(Device1, out object1);

                str_temp = DEVICE.DeviceOffset(axis_speed_data, (int)object1 * 10);
                properties.Device.Set_Device(str_temp, (int)object0);

                str_temp = DEVICE.DeviceOffset(axis_jog_acc_enable, (int)object1 * 10);
                properties.Device.Get_Device(str_temp, out object0);

                if ((bool)object0) properties.device_system.Set_Device("D" + object1.ToString(), 4);
                else properties.device_system.Set_Device("D" + object1.ToString(), 3);
            }
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }

        void cnt_Program_Run_70_JUMP(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                Device0 = str_Program_Run[1];
                properties.Device.Get_DataFast(Device0, out Int32_temp0);
                if (Int32_temp0 < Program_JumpPoint.Length && Int32_temp0 >= 0)
                {
                    Program_Run_程式現在行數 = Program_JumpPoint[Int32_temp0];
                }     
            }
    
            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }
        void cnt_Program_Run_71_REF(ref byte cnt)
        {
            if (FLAG_暫存堆疊區[FLAG_暫存堆疊區.Count - 1])
            {
                string device;
                string device_type;
                int device_num;
                Device0 = str_Program_Run[1];
                Device1 = str_Program_Run[2];
                device_type = Device0.Remove(1);
                properties.Device.Get_DataFast(Device1, out Int32_temp0);
                if (int.TryParse(Device0.Substring(1), out device_num))
                {
                    int index = 0;
                    if (device_type == "X")
                    {
                        for (int i = device_num; i < device_num + Int32_temp0; i ++ )
                        {
                            device = "X" + myConvert.十進位轉八進位(index + myConvert.八進位轉十進位(device_num)).ToString();
                            properties.device_system.Get_DeviceFast(device, out FLAG_temp0);
                            properties.Device.Set_DeviceFast(device, FLAG_temp0);
                            index++;
                        }
                     
                    }
                    else if (device_type == "Y")
                    {
                        for (int i = device_num; i < device_num + Int32_temp0; i++)
                        {
                            device = "Y" + myConvert.十進位轉八進位(index + myConvert.八進位轉十進位(device_num)).ToString();
                            properties.Device.Get_DeviceFast(device, out FLAG_temp0);
                            properties.device_system.Set_DeviceFast(device, FLAG_temp0);
                            index++;
                        }
                    }
                }
    
            }

            FLAG_暫存堆疊區.RemoveAt(FLAG_暫存堆疊區.Count - 1);
            Program_Run_程式現在行數++;
            cnt = 255;
        }


        void cnt_Program_Run_(ref byte cnt)
        {
            cnt++;
        }
    }
}
