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
using System.Reflection;
namespace DeltaMotor485
{
    /// <summary>
    /// 驅動器輸入功能表索引
    /// </summary>
    public enum enum_DI_function_index
    {
        None = 0x00,
        SON = 0x01,
        ARST = 0x02,
        CCLR = 0x04,
        CTRG = 0x08,
        POS0 = 0x11,
        POS1 = 0x12,
        POS2 = 0x13,
        POS3 = 0x1A,
        POS4 = 0x1B,
        POS5 = 0x1C,
        POS6 = 0x1E,
        PL = 0x23,
        NL = 0x22,
        ORGP = 0x24,
        EMGS = 0x21,
        STP = 0x46,
    }
    /// <summary>
    /// 驅動器輸入
    /// </summary>
    public enum enum_DI
    {
        SON = 0,
        CTRG = 1,
        ORGP = 2,
        PL = 3,
        NL = 4,
    }

    /// <summary>
    /// 驅動器輸入
    /// </summary>
    public class Driver_DI
    {
        public bool SON { get; set; }
        public bool CTRG { get; set; }
        public bool POS0 { get; set; }
        public bool POS1 { get; set; }
        public bool POS2 { get; set; }
        public bool ARST { get; set; }
        public bool ORGP { get; set; }
        public bool PL { get; set; }
        public bool NL { get; set; }
        public bool EMGS { get; set; }
        public bool STP { get; set; }

    }
    /// <summary>
    /// 驅動器輸出
    /// </summary>
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
    /// <summary>
    /// 驅動器輸出
    /// </summary>
    public class Driver_DO
    {
        public Driver_DO()
        {

        }
        public Driver_DO(byte station)
        {
            this.station = station;
        }
        public byte station { get; set; }
        public bool Read485_Enable { get; set; }
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
        public bool DDRVA_Done
        {
            get
            {
                return (flag_DDRVA_BUSY == false);
            }
        }
        public int CurrentPath = 0;
        private Driver_DI driver_DI = new Driver_DI();
        public Driver_DI DI 
        { 
            get
            {
                return driver_DI;
            }
            set
            {
                driver_DI = value;
            }
        }
        public int CommandPosition { get; set; }
        public bool flag_Init { get; set; }
        public bool flag_Servo_on_off { get; set; }
        public bool flag_Servo_state { get; set; }
        public bool flag_DDRVA { get; set; }
        public bool flag_DDRVA_BUSY { get; set; }
        public bool flag_Stop { get; set; }
        public bool flag_Home { get; set; }
        public bool flag_Home_BUSY { get; set; }
        public bool flag_ZSPD_refresh { get; set; }
        public bool flag_positionToZero { get; set; }
        public bool flag_get_postion { get; set; }
        public bool flag_JOG { get; set; }
        public HomeConfigClass HomeConfig { get; set; }
        public DDRVAConfigClass DDRVAConfig { get; set; }
        public JOGConfigClass JOGConfig { get; set; }
        public class HomeConfigClass
        {
            public enum_Direction enum_Direction { get; set; }
            public bool Z_enable { get; set; }
            public int speed1 { get; set; }
            public int speed2 { get; set; }
            public int dec1 { get; set; }
            public int dec2 { get; set; }
            public int offset_postion { get; set; }
            public int offset_speed { get; set; }
            public int offset_acc { get; set; }
        }
        public class DDRVAConfigClass
        {
            public int postion { get; set; }
            public int speed_rpm { get; set; }
            public int acc_ms { get; set; }
        }
        public class JOGConfigClass
        {
            public int speed_rpm { get; set; }
        }

        public void positionToZero()
        {
            flag_positionToZero = true;
        }
        async public Task DDRVA_Async( int postion, int speed_rpm, int acc_ms)
        {
            if (DDRVAConfig == null) DDRVAConfig = new DDRVAConfigClass();
            DDRVAConfig.postion = postion;
            DDRVAConfig.speed_rpm = speed_rpm;
            DDRVAConfig.acc_ms = acc_ms;
            flag_DDRVA = true;
            flag_DDRVA_BUSY = true;
            while (true)
            {
                System.Threading.Thread.Sleep(10);
                if (flag_DDRVA == false && flag_DDRVA_BUSY == false)
                {
                    break;
                }
            }
        }
        public void DDRVA(int postion, int speed_rpm, int acc_ms)
        {
            if (DDRVAConfig == null) DDRVAConfig = new DDRVAConfigClass();
            DDRVAConfig.postion = postion;
            DDRVAConfig.speed_rpm = speed_rpm;
            DDRVAConfig.acc_ms = acc_ms;
            flag_DDRVA = true;
            flag_DDRVA_BUSY = true;
        
        }
        public void Home( enum_Direction enum_Direction, bool Z_enable, int speed1, int speed2, int dec1, int dec2, int offset_postion, int offset_speed, int offset_acc)
        {
            if (HomeConfig == null) HomeConfig = new HomeConfigClass();
            HomeConfig.enum_Direction = enum_Direction;
            HomeConfig.Z_enable = Z_enable;
            HomeConfig.speed1 = speed1;
            HomeConfig.speed2 = speed2;
            HomeConfig.dec1 = dec1;
            HomeConfig.offset_postion = offset_postion;
            HomeConfig.offset_speed = offset_speed;
            HomeConfig.offset_acc = offset_acc;
            flag_Home = true;
            flag_Home_BUSY = true;
        }
        public void Stop()
        {
            flag_Stop = true;
        }
        public void JOG(int speed_rpm)
        {
            if (JOGConfig == null) JOGConfig = new JOGConfigClass();
            JOGConfig.speed_rpm = speed_rpm;
            flag_JOG = true;
        }
        public void Servo_on_off(bool state)
        {
            flag_Servo_on_off = true;
            flag_Servo_state = state;
        }
    }
    public enum enum_PR_state
    {
        READY,
        BUSY,
    }
    public enum enum_Direction
    {
        CW,
        CCW
    }
    public enum enum_StateMode
    {
        馬達回授脈波數 = 3,
        脈波輸入數 = 4,
        脈波誤差數 = 5,
        脈波輸入頻率_Kpps = 6,
        馬達轉速_rpm = 7,
    }
    public enum enum_EncoderMode
    {
        絕對型 = 0x0001,
        增量型 = 0x0000,
    }
    public class Port
    {
        public List<Driver_DO> drivers_DO = new List<Driver_DO>();
        private MyThread myThread;
        public int Start_station = 1;
        private MySerialPort mySerialPort;
        private MyTimerBasic MyTimerBasic_driver_DO = new MyTimerBasic(100);
        private MyTimerBasic MyTimerBasic_driver_DI = new MyTimerBasic(100);
        private MyTimerBasic MyTimerBasic_CommandPosition = new MyTimerBasic(50);
        public Driver_DO this[byte station]
        {
            get
            {
                for(int i = 0; i < drivers_DO.Count; i++)
                {
                    if (drivers_DO[i].station == station) return drivers_DO[i];
                }
                return null;
            }
        }
        public void Init(MySerialPort MySerialPort,params byte[] stations)
        {
            mySerialPort = MySerialPort;
            for (int i = 0; i < stations.Length; i++)
            {
                drivers_DO.Add(new Driver_DO(stations[i]));
            }
            myThread = new MyThread();
            myThread.Add_Method(sub_program);
            myThread.AutoRun(true);
            myThread.SetSleepTime(1);
            myThread.Trigger();
        }
        
        private void sub_program()
        {
            for (int i = 0; i < drivers_DO.Count; i++)
            {
                Driver_DO driver_DO = drivers_DO[i];
                Driver_DI driver_DI = drivers_DO[i].DI;
                byte station = driver_DO.station;
                driver_DO.CommandPosition = Communication.Get_position(mySerialPort, station);
                Communication.UART_Command_get_driver_DO(mySerialPort, station, ref driver_DO);
                Communication.UART_Command_get_driver_DI(mySerialPort, station, ref driver_DI);

                //if (drivers_DO[i].Read485_Enable)
                //{
                //    if (this.MyTimerBasic_driver_DO.IsTimeOut())
                //    {
                //        Communication.UART_Command_get_driver_DO(mySerialPort, station, ref driver_DO);
                //        MyTimerBasic_driver_DO.TickStop();
                //        MyTimerBasic_driver_DO.StartTickTime(50);
                //    }
                //    if (this.MyTimerBasic_driver_DI.IsTimeOut())
                //    {
                //        Communication.UART_Command_get_driver_DI(mySerialPort, station, ref driver_DI);
                //        MyTimerBasic_driver_DI.TickStop();
                //        MyTimerBasic_driver_DI.StartTickTime(50);
                //    }


                //}

            }

            for (int i = 0; i < drivers_DO.Count; i++)
            {
                Driver_DO driver_DO = drivers_DO[i];
                byte station = driver_DO.station;
                driver_DO.flag_ZSPD_refresh = false;
                if (driver_DO.flag_Init)
                {
                    Communication.DriverInit(mySerialPort, station);
                    driver_DO.flag_Init = false;
                }
                if (driver_DO.flag_Home)
                {
                    driver_DO.flag_Home = false;
                    Communication.Home(mySerialPort, station, driver_DO);

                }
                if (driver_DO.flag_positionToZero)
                {
                    driver_DO.flag_positionToZero = false;
                    Communication.Set_position_to_zero(mySerialPort, station);
                }
                if (driver_DO.flag_DDRVA)
                {
                    driver_DO.flag_DDRVA = false;
                    driver_DO.flag_DDRVA_BUSY = true;
                    driver_DO = Communication.DDRVA(mySerialPort, station, driver_DO);
                    Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : driver_DO.flag_DDRVA_BUSY :{driver_DO.flag_DDRVA_BUSY }");
                }
                if (driver_DO.flag_Stop)
                {
                    driver_DO.flag_Stop = false;
                    Communication.Stop(mySerialPort, station);
                }
                if (driver_DO.flag_JOG)
                {
                    driver_DO.flag_JOG = false;
                    Communication.JOG(mySerialPort, station, driver_DO);
                }
                if (driver_DO.flag_Servo_on_off)
                {
                    driver_DO.flag_Servo_on_off = false;
                    if (driver_DO.flag_Servo_state)
                    {
                        Communication.Servo_On(mySerialPort, station);
                    }
                    else
                    {
                        Communication.Servo_Off(mySerialPort, station);
                    }
                }
                if (driver_DO.flag_DDRVA_BUSY == true)
                {
                    int value = -1;
                    Communication.UART_Command_get_position_state(mySerialPort, station, ref value);
                    System.Threading.Thread.Sleep(10);
                    if (value < 0)
                    {
                        Console.Write($"station : {station} , DDRVA 指令異常! value:{value}\n");
                        driver_DO.flag_DDRVA_BUSY = false;
                    }
                    else if (value >= 1 && value <100)
                    {
                        //Console.Write($"station : {station} , DDRVA 指令未傳輸完成!\n");

                    }
                    else if (value >= 10000 && value < 20000)
                    {
                        //Console.Write($"station : {station} , DDRVA 指令已傳輸完成,運轉中!\n");

                    }
                    else if (value >= 20000 && value < 30000)
                    {
                        if(value != 21000)
                        {
                            if(value -20000 == driver_DO.CurrentPath)
                            {
                                Console.Write($"station : {station} , DDRVA 指令已全部完成! value:{value}\n");
                                driver_DO.flag_DDRVA_BUSY = false;
                            }    
      
                        }
                  
                    }
                }
            }
        }
    }
    public class Communication
    {
        public static int UART_RetryNum = 3;
        public static bool ConsoleWrite = false;
        public static int UART_TimeOut = 50;

  
        public enum enum_Command
        {
            JOG = 0x040A,
            enable_rs485_DI = 0x030C,
            set_driver_DI0_function = 0x0214,
            set_driver_DI1_function = 0x0216,
            set_driver_DI2_function = 0x0218,
            set_driver_DI3_function = 0x021A,
            set_driver_DI4_function = 0x021C,
            set_driver_DI = 0x040E,
            get_driver_DI = 0x040E,
            get_driver_DO = 0x005C,
            set_drva_position0 = 0x0604,
            set_PR_trigger = 0x050E,
            set_home_mode = 0x0508,
            set_home_config = 0x0600,
            set_home_speed1 = 0x050A,
            set_home_speed2 = 0x050C,
            set_org_offset = 0x0602,

            get_position_command = 0x0524,
            get_position_encoder = 0x0520,
            set_read_potion_mode = 0x028C,
            set_read_state0_mode = 0x0022,
            set_read_state1_mode = 0x0024,
            set_read_state2_mode = 0x0026,
            get_read_state0 = 0x0012,
            get_read_state1 = 0x0014,
            get_read_state2 = 0x0016,
            set_encoder_mode = 0x028A,

            set_path1_config = 0x0604,
            set_target_position0 = 0x0606,
            set_position0_speed = 0x0578,
            set_position0_acc = 0x0528,

            set_position1_speed = 0x057A,
            set_position1_acc = 0x052A,

            set_position2_speed = 0x057C,
            set_position2_acc = 0x052C,
        }
   
      


        static public bool DriverInit(MySerialPort MySerialPort, byte station)
        {
            bool result = true;
            try
            {
                if (UART_Command_set_driver_DI0_function(MySerialPort, station, DeltaMotor485.enum_DI_function_index.SON) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_set_driver_DI1_function(MySerialPort, station, DeltaMotor485.enum_DI_function_index.CTRG) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_set_driver_DI2_function(MySerialPort, station, DeltaMotor485.enum_DI_function_index.ORGP) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_set_driver_DI3_function(MySerialPort, station, DeltaMotor485.enum_DI_function_index.PL) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_set_driver_DI4_function(MySerialPort, station, DeltaMotor485.enum_DI_function_index.NL) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_enable_rs485_DI(MySerialPort, station, DeltaMotor485.enum_DI.SON) == false)
                {
                    result = false;
                    return false;
                }
                return result;
            }
            catch
            {
                return false;
            }
            finally
            {
                Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : station : {station}  {DateTime.Now.ToDateTimeString()} {(result ? "OK":"NG")}");
            }
         
        }
        static public bool Servo_On(MySerialPort MySerialPort, byte station)
        {
            Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : station : {station}  {DateTime.Now.ToDateTimeString()}");
            int value = 0;
            if (UART_Command_get_driver_DI(MySerialPort, station, ref value) == false) return false;
            value = value.SetBit((int)enum_DI.SON, true);
            if (UART_Command_set_driver_DI(MySerialPort, station, value) == false) return false;
            return true;
        }
        static public bool Servo_Off(MySerialPort MySerialPort, byte station)
        {
            Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : station : {station}  {DateTime.Now.ToDateTimeString()}");
            int value = 0;
            if (UART_Command_get_driver_DI(MySerialPort, station, ref value) == false) return false;
            value = value.SetBit((int)enum_DI.SON, false);
            if (UART_Command_set_driver_DI(MySerialPort, station, value) == false) return false;
            return true;
        }
        static public bool Set_Driver_DI_on_off(MySerialPort MySerialPort, byte station, bool on_off, enum_DI enum_DI)
        {
            int value = 0;
            if (UART_Command_get_driver_DI(MySerialPort, station, ref value) == false) return false;
            value = value.SetBit((int)enum_DI, on_off);
            if (UART_Command_set_driver_DI(MySerialPort, station, value) == false) return false;
            return true;

        }
        static public bool DDRVA(MySerialPort MySerialPort, byte station, int postion, int speed_rpm, int acc_ms)
        {
            Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : station:{station} ,postion:{postion} ,speed_rpm:{speed_rpm} ,acc_ms:{acc_ms} , {DateTime.Now.ToDateTimeString()}");

            if (UART_Command_set_path1_config(MySerialPort, station) == false) return false;
            if (UART_Command_set_position0_speed(MySerialPort, station, speed_rpm) == false) return false;
            if (UART_Command_set_position0_acc(MySerialPort, station, acc_ms) == false) return false;
            if (UART_Command_set_target_position0(MySerialPort, station, postion) == false) return false;
            if (UART_Command_set_position0_trigger(MySerialPort, station) == false) return false;
            Driver_DO driver_DO = new Driver_DO();
            Task task = Task.Factory.StartNew(new Action(delegate
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(10);
                    UART_Command_get_driver_DO(MySerialPort, station, ref driver_DO);

                    if (driver_DO.ZSPD)
                    {
                        if (ConsoleWrite) Console.Write($"station : {station} , DDRVA 指令完成!");

                        break;
                    }
                }


            }));
            return true;
        }
        static public Driver_DO DDRVA(MySerialPort MySerialPort, byte station, Driver_DO driver_DO)
        {
           
            bool result = true;
            try
            {
                driver_DO.CurrentPath = 1;
                driver_DO.flag_DDRVA_BUSY = true;
                int speed_rpm = driver_DO.DDRVAConfig.speed_rpm;
                int acc_ms = driver_DO.DDRVAConfig.acc_ms;
                int postion = driver_DO.DDRVAConfig.postion;
                Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : station:{station} ,postion:{postion} ,speed_rpm:{speed_rpm} ,acc_ms:{acc_ms} , {DateTime.Now.ToDateTimeString()}");
                if (UART_Command_set_path1_config(MySerialPort, station) == false)
                {
                    Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : UART_Command_set_path1_config failed!");
                    result = false;
                    return driver_DO;
                }
                if (UART_Command_set_position0_speed(MySerialPort, station, speed_rpm) == false)
                {
                    Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : UART_Command_set_position0_speed failed!");
                    result = false;
                    return driver_DO;
                }
                if (UART_Command_set_position0_acc(MySerialPort, station, acc_ms) == false)
                {
                    Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : UART_Command_set_position0_acc failed!");
                    result = false;
                    return driver_DO;
                }
                if (UART_Command_set_target_position0(MySerialPort, station, postion) == false)
                {
                    Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : UART_Command_set_target_position0 failed!");
                    result = false;
                    return driver_DO;
                }
                driver_DO.flag_ZSPD_refresh = true;
                int retry = 0;
                while(true)
                {
                    if (retry >= 5)
                    {
                        result = false;
                        return driver_DO;

                    }
                    if (UART_Command_set_position0_trigger(MySerialPort, station) == false)
                    {
                        Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : UART_Command_set_position0_trigger failed!");
                        result = false;
                        return driver_DO;
                    }
                    int value = -1;
                    Communication.UART_Command_get_position_state(MySerialPort, station, ref value);
                    if (value == 1) break;
                    retry++;
                }


                //Task task = Task.Factory.StartNew(new Action(delegate
                //{

                //    while (true)
                //    {
                //        System.Threading.Thread.Sleep(10);
                //        if (driver_DO.ZSPD && driver_DO.TOPS && driver_DO.flag_ZSPD_refresh == false)
                //        {
                //            Console.Write($"station : {station} , DDRVA 指令完成!\n");
                //            driver_DO.flag_DDRVA_BUSY = false;
                //            break;
                //        }
                //    }


                //}));
                return driver_DO;
            }
            catch(Exception e)
            {
                Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] :{e.Message} ");

                return driver_DO;
            }
            finally
            {
                if(result == false)
                {
                    Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : 指令有異常! ");

                }
            }
        
        }
        static public bool Stop(MySerialPort MySerialPort, byte station)
        {
            if (UART_Command_set_PR_stop(MySerialPort, station) == false) return false;
            if (UART_Command_JOG_Speed(MySerialPort, station , 0) == false) return false;
            return true;
        }
        async static public Task<bool> Home(MySerialPort MySerialPort, byte station, DeltaMotor485.enum_Direction enum_Direction, bool Z_enable, int speed1, int speed2, int dec1, int dec2, int offset_postion, int offset_speed, int offset_acc)
        {
            Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : station : {station}  {DateTime.Now.ToDateTimeString()}");
            if (UART_Command_set_home_config(MySerialPort, station, 0x0021001) == false) return false;
            if (UART_Command_set_home_mode(MySerialPort, station, enum_Direction, Z_enable) == false) return false;
            if (UART_Command_set_home_speed1(MySerialPort, station, speed1) == false) return false;
            if (UART_Command_set_home_speed2(MySerialPort, station, speed2) == false) return false;
            if (UART_Command_set_path1_config(MySerialPort, station) == false) return false;
            if (UART_Command_set_target_position0(MySerialPort, station, offset_postion) == false) return false;
            if (UART_Command_set_position0_speed(MySerialPort, station, offset_speed) == false) return false;
            if (UART_Command_set_position0_acc(MySerialPort, station, offset_acc) == false) return false;
            if (UART_Command_set_position1_acc(MySerialPort, station, dec1) == false) return false;
            if (UART_Command_set_position2_acc(MySerialPort, station, dec2) == false) return false;
            if (UART_Command_set_serch_home(MySerialPort, station) == false) return false;
            Driver_DO driver_DO = new Driver_DO();
            Task task = Task.Factory.StartNew(new Action(delegate
            {
                int cnt = 0;
                while(true)
                {
                    System.Threading.Thread.Sleep(10);
                    UART_Command_get_driver_DO(MySerialPort, station, ref driver_DO);
                    if (cnt == 0)
                    {
                        if (driver_DO.HOME && driver_DO.ZSPD)
                        {
                            System.Threading.Thread.Sleep(500);
                            cnt++;
                        }
                    }
                    if(cnt == 1)
                    {
                        if (driver_DO.HOME && driver_DO.ZSPD)
                        {
                            Set_position_to_zero(MySerialPort, station);
                            cnt++;
                        }
                    }
                    if (cnt == 2)
                    {
                        if (ConsoleWrite) Console.Write($"station : {station} , 復歸完成!");
                        break;
                    }


                }
               

            }));
            await task;
            return true;
        }
        static public bool Home(MySerialPort MySerialPort, byte station, Driver_DO driver_DO)
        {
           
            bool result = true;
            try
            {
                driver_DO.flag_Home_BUSY = true;
                enum_Direction enum_Direction = driver_DO.HomeConfig.enum_Direction;
                bool Z_enable = driver_DO.HomeConfig.Z_enable;
                int speed1 = driver_DO.HomeConfig.speed1;
                int speed2 = driver_DO.HomeConfig.speed2;
                int offset_postion = driver_DO.HomeConfig.offset_postion;
                int offset_speed = driver_DO.HomeConfig.offset_speed;
                int offset_acc = driver_DO.HomeConfig.offset_acc;
                int dec1 = driver_DO.HomeConfig.dec1;
                int dec2 = driver_DO.HomeConfig.dec2;
                Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : station: {station} ,enum_Direction: {enum_Direction} ,Z_enable: {Z_enable} ,speed1: {speed1} ,speed2: {speed2} ," +
                    $"offset_postion: {offset_postion} ,offset_speed: {offset_speed} ,offset_acc: {offset_acc} ,dec1: {dec1} ,dec2: {dec2}  {DateTime.Now.ToDateTimeString()}");
                if (UART_Command_set_home_config(MySerialPort, station, 0x0021001) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_set_home_mode(MySerialPort, station, enum_Direction, Z_enable) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_set_home_speed1(MySerialPort, station, speed1) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_set_home_speed2(MySerialPort, station, speed2) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_set_path1_config(MySerialPort, station) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_set_target_position0(MySerialPort, station, offset_postion) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_set_position0_speed(MySerialPort, station, offset_speed) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_set_position0_acc(MySerialPort, station, offset_acc) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_set_position1_acc(MySerialPort, station, dec1) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_set_position2_acc(MySerialPort, station, dec2) == false)
                {
                    result = false;
                    return false;
                }
                if (UART_Command_set_serch_home(MySerialPort, station) == false)
                {
                    result = false;
                    return false;
                }
                driver_DO.flag_ZSPD_refresh = true;
                Task task = Task.Factory.StartNew(new Action(delegate
                {
                    int cnt = 0;
                    while (true)
                    {
                        System.Threading.Thread.Sleep(10);
                        if (cnt == 0)
                        {
                            if (driver_DO.HOME && driver_DO.ZSPD && driver_DO.flag_ZSPD_refresh == false)
                            {
                                System.Threading.Thread.Sleep(200);
                                cnt++;
                            }
                        }
                        if (cnt == 1)
                        {
                            if (driver_DO.HOME && driver_DO.ZSPD)
                            {
                                driver_DO.positionToZero();

                                cnt++;
                            }
                        }
                        if (cnt == 2)
                        {
                             Console.Write($"station : {station} , 復歸完成!");
                            break;
                        }


                    }

                }));

                return result;
            }
            catch
            {
                driver_DO.flag_Home_BUSY = false;
                return false;
            }
            finally
            {
                if (result == false)
                {
                    driver_DO.flag_Home_BUSY = false;
                }
            }
           
        }
        static public bool Set_position_to_zero(MySerialPort MySerialPort, byte station)
        {
            Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : station : {station}  {DateTime.Now.ToDateTimeString()}");
            if (UART_Command_set_org_offset(MySerialPort, station, 0) == false) return false;
            if (UART_Command_set_home_config(MySerialPort, station , 0x000000) == false) return false;
            if (UART_Command_set_org_postion_mode(MySerialPort, station) == false) return false;
            if (UART_Command_set_serch_home(MySerialPort, station) == false) return false;
            return true;
        }
        static public int Get_position(MySerialPort MySerialPort, byte station)
        {
            int position = 0;
            DeltaMotor485.Communication.UART_Command_get_position_encoder(MySerialPort, station, ref position);
            return position;
        }
        static public bool JOG(MySerialPort MySerialPort, byte station, Driver_DO driver_DO)
        {
            Console.WriteLine($"[{MethodBase.GetCurrentMethod().Name}] : station : {station}  {DateTime.Now.ToDateTimeString()}");
            return UART_Command_JOG(MySerialPort, station, driver_DO.JOGConfig.speed_rpm);
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
                speed_rpm *= -1;
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
                int speed = 4999;
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.JOG, speed);
                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
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
                int speed = 4998;
                 List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.JOG, speed);

                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
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
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_enable_rs485_DI(MySerialPort MySerialPort, byte station, params enum_DI[] enum_DIs)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int value = 0;
                for (int i = 0; i < enum_DIs.Length; i++)
                {
                    value |= (int)(1 >> (int)enum_DIs[i]);
                }
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.enable_rs485_DI, value);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_driver_DI0_function(MySerialPort MySerialPort, byte station, enum_DI_function_index enum_DI_Function_Index)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                List<int> list_value = new List<int>();
                for (int i = 0; i < 1; i++)
                {
                    int temp = 0;
                    if (i == 0)
                    {
                        temp |= (0x01 << 8);
                        temp |= (int)enum_DI_Function_Index;
                    }
                    list_value.Add(temp);
                }
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_driver_DI0_function, list_value);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                             Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_driver_DI1_function(MySerialPort MySerialPort, byte station, enum_DI_function_index enum_DI_Function_Index)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                List<int> list_value = new List<int>();
                for (int i = 0; i < 1; i++)
                {
                    int temp = 0;
                    if (i == 0)
                    {
                        temp |= (0x01 << 8);
                        temp |= (int)enum_DI_Function_Index;
                    }
                    list_value.Add(temp);
                }
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_driver_DI1_function, list_value);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_driver_DI2_function(MySerialPort MySerialPort, byte station, enum_DI_function_index enum_DI_Function_Index)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                List<int> list_value = new List<int>();
                for (int i = 0; i < 1; i++)
                {
                    int temp = 0;
                    if (i == 0)
                    {
                        temp |= (0x01 << 8);
                        temp |= (int)enum_DI_Function_Index;
                    }
                    list_value.Add(temp);
                }
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_driver_DI2_function, list_value);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                          Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_driver_DI3_function(MySerialPort MySerialPort, byte station, enum_DI_function_index enum_DI_Function_Index)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                List<int> list_value = new List<int>();
                for (int i = 0; i < 1; i++)
                {
                    int temp = 0;
                    if (i == 0)
                    {
                        temp |= (0x01 << 8);
                        temp |= (int)enum_DI_Function_Index;
                    }
                    list_value.Add(temp);
                }
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_driver_DI3_function, list_value);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_driver_DI4_function(MySerialPort MySerialPort, byte station, enum_DI_function_index enum_DI_Function_Index)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                List<int> list_value = new List<int>();
                for (int i = 0; i < 1; i++)
                {
                    int temp = 0;
                    if (i == 0)
                    {
                        temp |= (0x01 << 8);
                        temp |= (int)enum_DI_Function_Index;
                    }
                    list_value.Add(temp);
                }
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_driver_DI4_function, list_value);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_driver_DI(MySerialPort MySerialPort, byte station , params enum_DI[] enum_DIs)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int value = 0;
                for (int i = 0; i < enum_DIs.Length; i++)
                {
                    value |= (int) (1 >> (int)enum_DIs[i]);
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
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_driver_DI(MySerialPort MySerialPort, byte station, int value)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
               
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
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_get_driver_DI(MySerialPort MySerialPort, byte station, ref Driver_DI driver_DI)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_ReadCommad(station, (int)enum_Command.get_driver_DI, 1);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                                driver_DI.SON = ((value >> (int)enum_DI.SON) % 2) == 1;
                                driver_DI.CTRG = ((value >> (int)enum_DI.CTRG) % 2) == 1;
                                driver_DI.ORGP = ((value >> (int)enum_DI.ORGP) % 2) == 1;
                                driver_DI.PL = ((value >> (int)enum_DI.PL) % 2) == 1;
                                driver_DI.NL = ((value >> (int)enum_DI.NL) % 2) == 1;

                                if (ConsoleWrite)
                                {
                                    Console.WriteLine($"");
                                    Console.WriteLine($"SRDY : {driver_DI.SON}");
                                    Console.WriteLine($"CTRG : {driver_DI.CTRG}");
                                    Console.WriteLine($"ORGP : {driver_DI.ORGP}");
                                    Console.WriteLine($"PL : {driver_DI.EMGS}");
                                    Console.WriteLine($"NL : {driver_DI.STP}");

                                    Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");

                                    Console.WriteLine($"");
                                }
                         
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
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_get_driver_DI(MySerialPort MySerialPort, byte station, ref int value)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_ReadCommad(station, (int)enum_Command.get_driver_DI, 1);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                                value = (UART_RX[3] << 8) | UART_RX[4];

                                if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");

                                if (ConsoleWrite) Console.WriteLine($"");
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
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_get_driver_DO(MySerialPort MySerialPort, byte station, ref Driver_DO driver_DO)
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
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                                if (ConsoleWrite)
                                {
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
                                    Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");

                                    Console.WriteLine($"");
                                }
                       
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
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_home_mode(MySerialPort MySerialPort, byte station , enum_Direction enum_direction , bool Z_enable)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();

                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                byte[] bytes = new byte[2];
                bytes[1] = (byte)(0x01);
                if (enum_direction == enum_Direction.CW) bytes[0] |= 0x02;
                if (enum_direction == enum_Direction.CCW) bytes[0] |= 0x03;
                if (!Z_enable) bytes[0] |= (0x02 << 4);
               
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_home_mode, bytes[0] | (bytes[1] << 8));


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite)
                            {
                                Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed!{bytes.ByteToStringHex()} station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            }
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_org_postion_mode(MySerialPort MySerialPort, byte station)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();

                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                byte[] bytes = new byte[2];
                bytes[0] |= 0x08;
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_home_mode, bytes[0] | (bytes[1] << 8));


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite)
                            {
                                Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed!{bytes.ByteToStringHex()} station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            }
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_serch_home(MySerialPort MySerialPort, byte station)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();

                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_PR_trigger, 0);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_home_speed1(MySerialPort MySerialPort, byte station , int rpm)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                rpm *= 10;
                List<int> list_value = new List<int>();
           
                if (rpm > 2000) rpm = 2000;
                if (rpm < 1) rpm = 1;
                list_value.Add(rpm);
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_home_speed1, list_value);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_home_speed2(MySerialPort MySerialPort, byte station, int rpm)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                rpm *= 10;
                if (rpm > 2000) rpm = 2000;
                if (rpm < 1) rpm = 1;

                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_home_speed2, rpm);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_home_config(MySerialPort MySerialPort, byte station , int value)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_home_config, value);

                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                             Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_org_offset(MySerialPort MySerialPort, byte station, int offset)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
      
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_org_offset, offset);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }

        static public bool UART_Command_set_read_potion_mode(MySerialPort MySerialPort, byte station)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_read_potion_mode, 0);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_get_read_state0(MySerialPort MySerialPort, byte station, ref int value)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_ReadCommad(station, (int)enum_Command.get_read_state0, 2);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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

                            if (UART_RX.Length == 9)
                            {
                                value = 0;
                                value |= UART_RX[3] << 24;
                                value |= UART_RX[4] << 16;
                                value |= UART_RX[5] << 8;
                                value |= UART_RX[6] << 0;
                                if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} , value : {value}\n");
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
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_read_state0_mode(MySerialPort MySerialPort, byte station, enum_StateMode enum_StateMode)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                List<int> list_value = new List<int>();
                for (int i = 0; i < 1; i++)
                {
                    int temp = (int)enum_StateMode;
  
                    list_value.Add(temp);
                }
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_read_state0_mode, list_value);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_get_read_state1(MySerialPort MySerialPort, byte station, ref int value)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_ReadCommad(station, (int)enum_Command.get_read_state1, 2);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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

                            if (UART_RX.Length == 9)
                            {
                                value = 0;
                                value |= UART_RX[3] << 24;
                                value |= UART_RX[4] << 16;
                                value |= UART_RX[5] << 8;
                                value |= UART_RX[6] << 0;
                                if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} , value : {value}\n");
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
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_read_state1_mode(MySerialPort MySerialPort, byte station, enum_StateMode enum_StateMode)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                List<int> list_value = new List<int>();
                for (int i = 0; i < 1; i++)
                {
                    int temp = (int)enum_StateMode;

                    list_value.Add(temp);
                }
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_read_state1_mode, list_value);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_get_read_state2(MySerialPort MySerialPort, byte station, ref int value)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_ReadCommad(station, (int)enum_Command.get_read_state2, 2);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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

                            if (UART_RX.Length == 9)
                            {
                                value = 0;
                                value |= UART_RX[3] << 24;
                                value |= UART_RX[4] << 16;
                                value |= UART_RX[5] << 8;
                                value |= UART_RX[6] << 0;
                                if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} , value : {value}\n");
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
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_read_state2_mode(MySerialPort MySerialPort, byte station, enum_StateMode enum_StateMode)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                List<int> list_value = new List<int>();
                for (int i = 0; i < 1; i++)
                {
                    int temp = (int)enum_StateMode;

                    list_value.Add(temp);
                }
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_read_state2_mode, list_value);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_encoder_mode(MySerialPort MySerialPort, byte station, enum_EncoderMode  enum_EncoderMode)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                List<int> list_value = new List<int>();
                for (int i = 0; i < 1; i++)
                {
                    int temp = (int)enum_EncoderMode;

                    list_value.Add(temp);
                }
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_encoder_mode, list_value);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }

        static public bool UART_Command_get_position_command(MySerialPort MySerialPort, byte station, ref int value)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_ReadCommad(station, (int)enum_Command.get_position_command, 2);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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

                            if (UART_RX.Length == 9)
                            {
                                value = 0;
                                value |= UART_RX[5] << 24;
                                value |= UART_RX[6] << 16;
                                value |= UART_RX[3] << 8;
                                value |= UART_RX[4] << 0;
                                if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} , value : {value}\n");
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
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_get_position_encoder(MySerialPort MySerialPort, byte station, ref int value)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_ReadCommad(station, (int)enum_Command.get_position_encoder, 2);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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

                            if (UART_RX.Length == 9)
                            {
                                value = 0;
                                value |= UART_RX[5] << 24;
                                value |= UART_RX[6] << 16;
                                value |= UART_RX[3] << 8;
                                value |= UART_RX[4] << 0;
                                if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} , value : {value}\n");
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
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_get_position_state(MySerialPort MySerialPort, byte station, ref int value)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_ReadCommad(station, (int)enum_Command.set_PR_trigger, 2);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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

                            if (UART_RX.Length == 9)
                            {
                                value = 0;
                                value |= UART_RX[5] << 24;
                                value |= UART_RX[6] << 16;
                                value |= UART_RX[3] << 8;
                                value |= UART_RX[4] << 0;
                                if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} , value : {value}\n");
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
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }

        static public bool UART_Command_set_path1_config(MySerialPort MySerialPort, byte station)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int value = 0;
                value |= 0x02;
                value |= (0x03 << 4);
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_path1_config, value);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_target_position0(MySerialPort MySerialPort, byte station, int target_position)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_target_position0, target_position);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_position0_trigger(MySerialPort MySerialPort, byte station)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();

                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_PR_trigger, 1);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }

        static public bool UART_Command_set_position0_speed(MySerialPort MySerialPort, byte station, int rpm)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                rpm *= 10;
                if (rpm > 7500) rpm = 7500;
                if (rpm < 1) rpm = 1;

                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_position0_speed, rpm);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_position0_acc(MySerialPort MySerialPort, byte station, int ms)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                if (ms > 65500) ms = 65500;
                if (ms < 1) ms = 1;

                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_position0_acc, ms);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }

        static public bool UART_Command_set_position1_speed(MySerialPort MySerialPort, byte station, int rpm)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                rpm *= 10;
                if (rpm > 7500) rpm = 7500;
                if (rpm < 1) rpm = 1;

                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_position1_speed, rpm);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_position1_acc(MySerialPort MySerialPort, byte station, int ms)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                if (ms > 65500) ms = 65500;
                if (ms < 1) ms = 1;

                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_position1_acc, ms);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }

        static public bool UART_Command_set_position2_speed(MySerialPort MySerialPort, byte station, int rpm)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                rpm *= 10;
                if (rpm > 7500) rpm = 7500;
                if (rpm < 1) rpm = 1;

                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_position2_speed, rpm);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }
        static public bool UART_Command_set_position2_acc(MySerialPort MySerialPort, byte station, int ms)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();
                if (ms > 65500) ms = 65500;
                if (ms < 1) ms = 1;

                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_position2_acc, ms);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
            return flag_OK;
        }

        static public bool UART_Command_set_PR_stop(MySerialPort MySerialPort, byte station)
        {
            bool flag_OK = false;

            if (MySerialPort.SerialPortOpen())
            {
                MyTimer MyTimer_UART_TimeOut = new MyTimer();

                int retry = 0;
                int cnt = 0;
                MySerialPort.ClearReadByte();
                List<byte> list_byte = Get_WriteCommad(station, (int)enum_Command.set_PR_trigger, 1000);


                while (true)
                {
                    if (cnt == 0)
                    {
                        if (retry >= UART_RetryNum)
                        {
                            Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  error! station: {station}\n {list_byte.ToArray().ByteToStringHex()}");
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
                            if (ConsoleWrite) Console.Write($"[{MethodBase.GetCurrentMethod().Name}] Set data  sucessed! station : {station}\n {UART_RX.ByteToStringHex()} \n");
                            flag_OK = true;
                            break;
                        }

                    }

                    System.Threading.Thread.Sleep(1);
                }
            }
            //MySerialPort.SerialPortClose();
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
            int len = values.Count * 2;

            list_byte.Add(station);
            list_byte.Add((byte)(0x10));
            list_byte.Add((byte)(start_adress >> 8));
            list_byte.Add((byte)(start_adress >> 0));
            list_byte.Add((byte)((len ) >> 8));
            list_byte.Add((byte)((len ) >> 0));
            list_byte.Add((byte)(len * 2));
            for(int i = 0; i < values.Count; i++)
            {
                list_byte.Add((byte)(values[i] >> 8));
                list_byte.Add((byte)(values[i] >> 0));
                list_byte.Add((byte)(values[i] >> 24));
                list_byte.Add((byte)(values[i] >> 16));

            }    
            ushort CRC = Basic.MyConvert.Get_CRC16(list_byte.ToArray());
            list_byte.Add((byte)(CRC >> 0));
            list_byte.Add((byte)(CRC >> 8));
            if (ConsoleWrite) Console.WriteLine($"write command : {list_byte.ToArray().ByteToStringHex()}");
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
