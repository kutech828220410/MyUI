using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
namespace LadderProperty
{
    [Serializable]
    public class DEVICE
    {
        static Basic.MyConvert myConvert = new Basic.MyConvert();
        static private Stopwatch stopwatch = new Stopwatch();
    
        [Serializable]
        private class Bit
        {
            public bool val = false;
            public string[] comment = new string[2];
        }
        [Serializable]
        private class DATA
        {
            public Int32 val = 0;
            public string[] comment = new string[2];
        }
        [Serializable]
        private class Timer
        {
            private bool flag_init =false;
            public bool time_start = false;
            public bool val = false;
            public string[] comment = new string[2];
            public int time_now_value = 0;
            public int time_set_value = 1;
            public int time_caculate_value = 0;
            public int time_start_value = 0;
            public bool Enable;
            public Timer()
            {
              //  backgroundWorker_Timer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.time_caculate_thread);
            }
            public void TimerStop()
            {
                if (Enable)
                {
                    this.time_start = false;
                    flag_init = true;
                    time_caculate();
                }
                else
                {
                    val = false; 
                }
            }
            public void TimerStart()
            {
                if (Enable)
                {
                    this.time_start = true;
                    time_caculate();
                }
                else
                {
                    val = true; 
                }

            }
            public void SetTimer(int value , bool reset)
            {
                if (Enable)
                {
                    if(reset)
                    {
                        this.time_now_value = 0;
                        this.time_caculate_value = (int)stopwatch.Elapsed.TotalMilliseconds;
                        this.time_start_value = (int)stopwatch.Elapsed.TotalMilliseconds;
                    }     
                    this.time_set_value = value;
                }
                else
                {
                    time_now_value = value;
                }
            }
            public int GetTimer()
            {
                time_caculate();
                return this.time_now_value;
            }
            public bool IsTimeUp()
            {
                bool FLAG = false;
                if (Enable)
                {
                    FLAG = time_caculate();                 
                }
                else
                {
                    FLAG = val;
                }
                return FLAG;
            }
            private void time_caculate_thread(object sender, DoWorkEventArgs e)
            {
               // time_caculate();
            }
            private bool time_caculate()
            {
                if (Enable)
                {
                    if (this.time_start)
                    {
                        this.time_caculate_value = (int)stopwatch.Elapsed.TotalMilliseconds;
                        if(flag_init)
                        {
                            this.time_start_value = (int)stopwatch.Elapsed.TotalMilliseconds;
                            flag_init = false;
                        }
                    }
                    else
                    {
                        this.time_now_value = 0;
                        this.time_caculate_value = (int)stopwatch.Elapsed.TotalMilliseconds;
                        this.time_start_value = (int)stopwatch.Elapsed.TotalMilliseconds;
                    }

                    int temp = this.time_caculate_value - this.time_start_value;
                    if (temp <= time_set_value)
                    {
                        val = false;
                        this.time_now_value = temp;
                    }
                    else
                    {
                        val = true;
                        time_now_value = time_set_value;
                    }
                }
                return val;
            }
        }
   
        public DEVICE(int x_count, int y_count, int m_count, int s_count, int t_count, int d_count, int r_count,bool TimerEnable)
        {
            X_count = x_count;
            Y_count = y_count;
            M_count = m_count;
            S_count = s_count;
            T_count = t_count;
            D_count = d_count;
            R_count = r_count;
            init(TimerEnable);        
        }
        public DEVICE(bool TimerEnable)
        {
            init(TimerEnable);
        }
        private void backgroundWorker_thread_control_DoWork(object sender, DoWorkEventArgs e)
        {
           /* while(true)
            {
                if (!backgroundWorker_Timer.IsBusy) backgroundWorker_Timer.RunWorkerAsync();
                Thread.Sleep(10);
            }*/
        }
        private int X_count = 1000;
        private int Y_count = 1000;
        private int M_count = 40000;
        private int S_count = 40000;
        private int T_count = 1000;
        private int D_count = 40000;
        private int R_count = 40000;
        private int F_count = 40000;
        private int Z_count = 200;
        private Bit[] X;
        private Bit[] Y;
        private Bit[] M;
        private Bit[] S;
        private Timer[] T;
        private DATA[] D;
        private DATA[] R;
        private DATA[] Z;
        private DATA[] F;
        public void init(bool TimerEnable)
        {
            stopwatch.Start();
           // backgroundWorker_thread_control.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_thread_control_DoWork);
           // if (!backgroundWorker_thread_control.IsBusy) backgroundWorker_thread_control.RunWorkerAsync();       

            X = new Bit[X_count];
            Y = new Bit[Y_count];
            M = new Bit[M_count];
            S = new Bit[S_count];
            T = new Timer[T_count];
            D = new DATA[D_count];
            R = new DATA[R_count];
            Z = new DATA[Z_count];
            F = new DATA[F_count];
            int Device_length_max = 0;
            if (X.Length > Device_length_max) Device_length_max = X.Length;
            if (Y.Length > Device_length_max) Device_length_max = Y.Length;
            if (M.Length > Device_length_max) Device_length_max = M.Length;
            if (S.Length > Device_length_max) Device_length_max = S.Length;
            if (D.Length > Device_length_max) Device_length_max = D.Length;
            if (R.Length > Device_length_max) Device_length_max = R.Length;
            if (Z.Length > Device_length_max) Device_length_max = Z.Length;
            if (F.Length > Device_length_max) Device_length_max = F.Length;
            if (T.Length > Device_length_max) Device_length_max = T.Length;
            for (int i = 0; i < Device_length_max; i++)
            {
                if (i < X.Length) X[i] = new Bit();
                if (i < Y.Length) Y[i] = new Bit();
                if (i < M.Length) M[i] = new Bit();
                if (i < S.Length) S[i] = new Bit();
                if (i < D.Length) D[i] = new DATA();
                if (i < R.Length) R[i] = new DATA();
                if (i < F.Length) F[i] = new DATA();
                if (i < Z.Length) Z[i] = new DATA();
                if (i < T.Length)
                {
                    T[i] = new Timer();
                    T[i].Enable = TimerEnable;
                }
            }
        }

        public bool Set_Device(String device, bool value)
        {
            bool FLAG = false;
            FLAG = _Set_Device(device, (object)value, 0);
            return FLAG;
        }
        public bool Set_Device(String device, Int32 value)
        {
            bool FLAG = false;
            FLAG = _Set_Device(device, (object)value, 0);
            return FLAG;
        }
        public bool Set_Device(String device, String commemt)
        {
            bool FLAG = false;
            FLAG = _Set_Device(device, (object)commemt, 0);
            return FLAG;
        }
        public bool Set_Device(String device, String commemt, int commment_num)
        {
            bool FLAG = false;
            FLAG = _Set_Device(device, (object)commemt, commment_num);
            return FLAG;
        }
        private bool _Set_Device(String device, object value, int commment_num)
        {
            int 錯誤次數 = 0;
            device = DeviceIndex(device);
            if (device != null && value != null)
            {
                //Type type; = value.GetType();
                if (device.Length > 0)
                {
                    device = device.ToUpper();
                    String Device_類型 = device.Substring(0, 1);
                    String Device_編號 = device.Substring(1, device.Length - 1);
                    int Device_編號_val = 0;
                    if (錯誤次數 == 0)
                    {
                        if (!int.TryParse(Device_編號, out Device_編號_val))
                        {
                            錯誤次數++;
                        }
                    }

                    if (錯誤次數 == 0)
                    {

                        bool FLAG = false;
                        檢查是否為8進位(ref FLAG, Device_編號_val);
                        if (Device_類型 == "X" && FLAG)
                        {
                            if (Device_編號_val >= X.Length)
                            {
                                錯誤次數++;
                            }
                            else if (value is Boolean)
                            {
                                this.X[Device_編號_val].val = (bool)value;
                            }
                            else if (value is string)
                            {
                                this.X[Device_編號_val].comment[0] = (String)value;
                            }
                            else
                            {
                                錯誤次數++;
                            }
                        }
                        else if (Device_類型 == "Y" && FLAG)
                        {
                            if (Device_編號_val >= Y.Length)
                            {
                                錯誤次數++;
                            }
                            else if (value is Boolean)
                            {
                                this.Y[Device_編號_val].val = (bool)value;
                            }
                            else if (value is string)
                            {
                                this.Y[Device_編號_val].comment[0] = (String)value;
                            }
                            else
                            {
                                錯誤次數++;
                            }
                        }
                        else if (Device_類型 == "M")
                        {
                            if (Device_編號_val >= M.Length)
                            {
                                錯誤次數++;
                            }
                            else if (value is Boolean)
                            {
                                this.M[Device_編號_val].val = (bool)value;
                            }
                            else if (value is string)
                            {
                                this.M[Device_編號_val].comment[0] = (String)value;
                            }
                            else
                            {
                                錯誤次數++;
                            }
                        }
                        else if (Device_類型 == "S")
                        {
                            if (Device_編號_val >= S.Length)
                            {
                                錯誤次數++;
                            }
                            else if (value is Boolean)
                            {
                                this.S[Device_編號_val].val = (bool)value;
                            }
                            else if (value is string)
                            {
                                this.S[Device_編號_val].comment[0] = (String)value;
                            }
                            else
                            {
                                錯誤次數++;
                            }
                        }
                        else if (Device_類型 == "D")
                        {
                            if (Device_編號_val >= D.Length)
                            {
                                錯誤次數++;
                            }
                            else if (value is Int32)
                            {
                                this.D[Device_編號_val].val = (Int32)value;
                            }
                            else if (value is string)
                            {
                                this.D[Device_編號_val].comment[0] = (String)value;
                            }
                            else
                            {
                                錯誤次數++;
                            }
                        }
                        else if (Device_類型 == "R")
                        {
                            if (Device_編號_val >= R.Length)
                            {
                                錯誤次數++;
                            }
                            else if (value is Int32)
                            {
                                this.R[Device_編號_val].val = (Int32)value;
                            }
                            else if (value is string)
                            {
                                this.R[Device_編號_val].comment[0] = (String)value;
                            }
                            else
                            {
                                錯誤次數++;
                            }
                        }
                        else if (Device_類型 == "F")
                        {
                            if (Device_編號_val >= F.Length)
                            {
                                錯誤次數++;
                            }
                            else if (value is Int32)
                            {
                                this.F[Device_編號_val].val = (Int32)value;
                            }
                            else if (value is string)
                            {
                                this.F[Device_編號_val].comment[0] = (String)value;
                            }
                            else
                            {
                                錯誤次數++;
                            }
                        }
                        else if (Device_類型 == "Z")
                        {
                            if (Device_編號_val >= Z.Length)
                            {
                                錯誤次數++;
                            }
                            else if (value is Int32)
                            {
                                this.Z[Device_編號_val].val = (Int32)value;
                            }
                            else if (value is string)
                            {
                                this.Z[Device_編號_val].comment[0] = (String)value;
                            }
                            else
                            {
                                錯誤次數++;
                            }
                        }
                        else if (Device_類型 == "T")
                        {
                            if (Device_編號_val >= T.Length)
                            {
                                錯誤次數++;
                            }
                            else if (value is Boolean)
                            {
                                if((bool)value) this.T[Device_編號_val].TimerStart();
                                else this.T[Device_編號_val].TimerStop();
                            }
                            else if (value is Int32)
                            {
                                T[Device_編號_val].SetTimer((Int32)value,true);
                            }
                            else if (value is string)
                            {
                                if(commment_num != 2)this.T[Device_編號_val].comment[0] = (String)value;
                                else
                                {
                                    string str_device_temp = (String)value;
                                    object temp = new object();
                                    Get_Device(str_device_temp, out temp);
                                    T[Device_編號_val].SetTimer((int)temp, false);
                                }
                            }
                            else
                            {
                                錯誤次數++;
                            }
                        }
                        else
                        {
                            錯誤次數++;
                        }
                    }

                }
                else
                {
                    錯誤次數++;
                }
            }
            else 錯誤次數++;

            if (錯誤次數 == 0) return true;
            else return false;
        }

        public bool Get_Device(String device, out object value)
        {
            int 錯誤次數 = 0;
            value = new object();
            device = DeviceIndex(device);
            if (device != null)
            {
                if (device.Length > 0)
                {
                    device = device.ToUpper();
                    int Device_編號_val = 0;
                    if (錯誤次數 == 0)
                    {
                        if (!int.TryParse(device.Substring(1, device.Length - 1), out Device_編號_val))
                        {
                            錯誤次數++;
                        }
                    }
                    if (錯誤次數 == 0)
                    {
         
                        switch(device.Substring(0, 1))
                        {
                            
                            case "X":
                                bool FLAG = false;
                                檢查是否為8進位(ref FLAG, Device_編號_val);
                                if (Device_編號_val < X.Length && FLAG) value = this.X[Device_編號_val].val;
                                else 錯誤次數++;
                                break;
                            case "Y":
                                FLAG = false;
                                檢查是否為8進位(ref FLAG, Device_編號_val);
                                if (Device_編號_val < Y.Length && FLAG) value = this.Y[Device_編號_val].val;
                                else 錯誤次數++;
                                break;
                            case "M":
                                if (Device_編號_val < M.Length) value = this.M[Device_編號_val].val;
                                else 錯誤次數++;
                                break;
                            case "S":
                                if (Device_編號_val < S.Length) value = this.S[Device_編號_val].val;
                                else 錯誤次數++;
                                break;
                            case "D":
                                if (Device_編號_val < D.Length) value = this.D[Device_編號_val].val;
                                else 錯誤次數++;
                                break;
                            case "R":
                                if (Device_編號_val < R.Length) value = this.R[Device_編號_val].val;
                                else 錯誤次數++;
                                break;
                            case "F":
                                if (Device_編號_val < F.Length) value = this.F[Device_編號_val].val;
                                else 錯誤次數++;
                                break;
                            case "Z":
                                if (Device_編號_val < Z.Length) value = this.Z[Device_編號_val].val;
                                else 錯誤次數++;
                                break;
                            case "T":
                                if (Device_編號_val < T.Length) value = this.T[Device_編號_val].IsTimeUp();
                                else 錯誤次數++;
                                break;
                            case "K":
                                value = Device_編號_val;
                                break;
                            default:
                                錯誤次數++;
                                break;
                        }
                       
                       /* if (Device_類型 == "X")
                        {
                            bool FLAG = false;
                            檢查是否為8進位(ref FLAG, Device_編號_val);
                            if (Device_編號_val < X.Length && FLAG) value = this.X[Device_編號_val].val;
                            else 錯誤次數++;
                        }
                        else if (Device_類型 == "Y")
                        {
                            bool FLAG = false;
                            檢查是否為8進位(ref FLAG, Device_編號_val);
                            if (Device_編號_val < Y.Length && FLAG) value = this.Y[Device_編號_val].val;
                            else 錯誤次數++;
                        }
                        else if (Device_類型 == "M")
                        {
                            if (Device_編號_val < M.Length) value = this.M[Device_編號_val].val;
                            else 錯誤次數++;
                        }
                        else if (Device_類型 == "S")
                        {
                            if (Device_編號_val < S.Length) value = this.S[Device_編號_val].val;
                            else 錯誤次數++;
                        }
                        else if (Device_類型 == "D")
                        {
                            if (Device_編號_val < D.Length) value = this.D[Device_編號_val].val;
                            else 錯誤次數++;
                        }
                        else if (Device_類型 == "R")
                        {
                            if (Device_編號_val < R.Length) value = this.R[Device_編號_val].val;
                            else 錯誤次數++;
                        }
                        else if (Device_類型 == "Z")
                        {
                            if (Device_編號_val < Z.Length) value = this.Z[Device_編號_val].val;
                            else 錯誤次數++;
                        }
                        else if (Device_類型 == "T")
                        {
                            if (Device_編號_val < T.Length) value = this.T[Device_編號_val].IsTimeUp();
                            else 錯誤次數++;
                        }
                        else if (Device_類型 == "K")
                        {
                            value = Device_編號_val;
                        }
                        else
                        {
                            錯誤次數++;
                        }*/
                    }

                }
                else
                {
                    錯誤次數++;
                }
            }
            else
            {
                錯誤次數++;
            }
            if (錯誤次數 == 0) return true;
            else return false;
        }
        public bool Get_Device(String device, int commemt_numm, out object value)
        {
            int 錯誤次數 = 0;
            value = new object();
            device = DeviceIndex(device);
            if (device != null)
            {
                if (device.Length > 0)
                {
                    device = device.ToUpper();
                    String Device_類型 = device.Substring(0, 1);
                    String Device_編號 = device.Substring(1, device.Length - 1);
                    int Device_編號_val = 0;
                    if (錯誤次數 == 0)
                    {
                        if (!int.TryParse(Device_編號, out Device_編號_val))
                        {
                            錯誤次數++;
                        }
                    }
                    if (錯誤次數 == 0)
                    {
                  
                        if (Device_類型 == "X")
                        {
                            bool FLAG = false;
                            檢查是否為8進位(ref FLAG, Device_編號_val);
                            if (Device_編號_val < this.X.Length && FLAG) value = this.X[Device_編號_val].comment[commemt_numm];
                            else value = "#None";
                        }
                        else if (Device_類型 == "Y")
                        {
                            bool FLAG = false;
                            檢查是否為8進位(ref FLAG, Device_編號_val);
                            if (Device_編號_val < this.Y.Length && FLAG) value = this.Y[Device_編號_val].comment[commemt_numm];
                            else value = "#None";
                        }
                        else if (Device_類型 == "M")
                        {
                            if (Device_編號_val < this.M.Length) value = this.M[Device_編號_val].comment[commemt_numm];
                            else value = "#None";
                        }
                        else if (Device_類型 == "S")
                        {
                            if (Device_編號_val < this.S.Length) value = this.S[Device_編號_val].comment[commemt_numm];
                            else value = "#None";
                        }
                        else if (Device_類型 == "D")
                        {
                            if (Device_編號_val < this.D.Length) value = this.D[Device_編號_val].comment[commemt_numm];
                            else value = "#None";
                        }
                        else if (Device_類型 == "R")
                        {
                            if (Device_編號_val < this.R.Length) value = this.R[Device_編號_val].comment[commemt_numm];
                            else value = "#None";
                        }
                        else if (Device_類型 == "F")
                        {
                            if (Device_編號_val < this.F.Length) value = this.F[Device_編號_val].comment[commemt_numm];
                            else value = "#None";
                        }
                        else if (Device_類型 == "Z")
                        {
                            if (Device_編號_val < this.Z.Length) value = this.Z[Device_編號_val].comment[commemt_numm];
                            else value = "#None";
                        }
                        else if (Device_類型 == "T")
                        {
                            if (commemt_numm < 2)
                            {
                                if (Device_編號_val < this.T.Length) value = this.T[Device_編號_val].comment[commemt_numm];
                                else value = "#None";
                            }
                            else if (commemt_numm == 2)
                            {
                                if (Device_編號_val < this.T.Length) value = this.T[Device_編號_val].GetTimer();
                            }
                           
                        }
                        else
                        {
                            錯誤次數++;
                        }
                    }

                }
                else
                {
                    錯誤次數++;
                }
            }
            else
            {
                錯誤次數++;
            }
            if (錯誤次數 == 0) return true;
            else return false;
        }

      
        public bool Get_DeviceFast_Ex(String device)
        {
            if (device == null || device == "") return false;

            if (device.Contains("Z")) device = DeviceIndexFast(device);
            int int_index = int.Parse(device.Substring(1, device.Length - 1));

            switch (device.Substring(0, 1))
            {

                case "X":
                    return this.X[int_index].val;
                    break;
                case "Y":
                    return this.Y[int_index].val;
                    break;
                case "M":
                    return this.M[int_index].val;
                    break;
                case "S":
                    return this.S[int_index].val;
                    break;
                case "T":
                    return this.T[int_index].IsTimeUp();
                    break;

                default:
                    return false;
                    break;
            }

        }
        public bool Get_DeviceFast_Ex(String device, int int_index)
        {
            if (device == null || device == "") return false;

            switch (device.Substring(0, 1))
            {

                case "X":
                    return this.X[int_index].val;
                    break;
                case "Y":
                    return this.Y[int_index].val;
                    break;
                case "M":
                    return this.M[int_index].val;
                    break;
                case "S":
                    return this.S[int_index].val;
                    break;
                case "T":
                    return this.T[int_index].IsTimeUp();
                    break;

                default:
                    return false;
                    break;
            }

        }
        public void Set_DeviceFast_Ex(String device, bool value)
        {
            if (device == null || device == "") return;

            if (device.Contains("Z")) device = DeviceIndexFast(device);
            int int_index = int.Parse(device.Substring(1, device.Length - 1));

            switch (device.Substring(0, 1))
            {
                case "X":
                    this.X[int_index].val = value;
                    break;
                case "Y":
                    this.Y[int_index].val = value;
                    break;
                case "M":
                    this.M[int_index].val = value;
                    break;
                case "S":
                    this.S[int_index].val = value;
                    break;
                case "T":
                    if (value) this.T[int_index].TimerStart();
                    else this.T[int_index].TimerStop();
                    break;
                default:
                    break;
            }

        }
        public void Set_DeviceFast_Ex(String device, int int_index, bool value)
        {
            if (device == null || device == "") return;

            switch (device.Substring(0, 1))
            {
                case "X":
                    this.X[int_index].val = value;
                    break;
                case "Y":
                    this.Y[int_index].val = value;
                    break;
                case "M":
                    this.M[int_index].val = value;
                    break;
                case "S":
                    this.S[int_index].val = value;
                    break;
                case "T":
                    if (value) this.T[int_index].TimerStart();
                    else this.T[int_index].TimerStop();
                    break;
                default:
                    break;
            }

        }
        public int Get_DataFast_Ex(String device)
        {
            if (device == null || device == "") return 0 ;

            if (device.Contains("Z")) device = DeviceIndexFast(device);
            int int_index = int.Parse(device.Substring(1, device.Length - 1));

            switch (device.Substring(0, 1))
            {
                case "D":
                    return this.D[int_index].val;
                    break;
                case "R":
                    return this.R[int_index].val;
                    break;
                case "F":
                    return this.F[int_index].val;
                    break;
                case "Z":
                    return this.Z[int_index].val;
                    break;
                case "T":
                    return this.T[int_index].GetTimer();
                    break;
                case "K":
                    return int_index;
                    break;
                default:
                    return -1;
                    break;
            }

        }
        public int Get_DataFast_Ex(String device, int int_index)
        {
            if (device == null || device == "") return 0;

            switch (device.Substring(0, 1))
            {
                case "D":
                    return this.D[int_index].val;
                    break;
                case "R":
                    return this.R[int_index].val;
                    break;
                case "F":
                    return this.F[int_index].val;
                    break;
                case "Z":
                    return this.Z[int_index].val;
                    break;
                case "T":
                    return this.T[int_index].GetTimer();
                    break;
                case "K":
                    return int_index;
                    break;
                default:
                    return -1;
                    break;
            }

        }
        public void Set_DataFast_Ex(String device, int value)
        {
            if (device == null || device == "") return;
            
            if (device.Contains("Z")) device = DeviceIndexFast(device);
            int int_index = int.Parse(device.Substring(1, device.Length - 1));
            switch (device.Substring(0, 1))
            {

                case "D":
                    this.D[int_index].val = value;
                    break;
                case "R":
                    this.R[int_index].val = value;
                    break;
                case "F":
                    this.F[int_index].val = value;
                    break;
                case "Z":
                    this.Z[int_index].val = value;
                    break;

                default:
                    break;
            }

        }
        public void Set_DataFast_Ex(String device, int int_index, int value)
        {
            if (device == null || device == "") return;

            if (device.Contains("Z")) device = DeviceIndexFast(device);
            switch (device.Substring(0, 1))
            {

                case "D":
                    this.D[int_index].val = value;
                    break;
                case "R":
                    this.R[int_index].val = value;
                    break;
                case "F":
                    this.F[int_index].val = value;
                    break;
                case "Z":
                    this.Z[int_index].val = value;
                    break;

                default:
                    break;
            }

        }

        Int32 Int32_Fast_val;
        public bool Get_DeviceFast(String device ,int index)
        {
            if (device.Contains("Z")) device = DeviceIndexFast(device);
            switch (device)
            {

                case "X":
                    return this.X[index].val;
                    break;
                case "Y":
                    return this.Y[index].val;
                    break;
                case "M":
                    return this.M[index].val;
                    break;
                case "S":
                    return this.S[index].val;
                    break;
                case "T":
                    return this.T[index].IsTimeUp();
                    break;

                default:
                    return false;
                    break;
            }

        }
        public void Get_DeviceFast(String device , out bool value)
        {
            value = false;
            if (device.Contains("Z")) device = DeviceIndexFast(device);
        
            Int32_Fast_val = int.Parse(device.Substring(1, device.Length - 1));
            switch (device.Substring(0, 1))
            {

                case "X":
                    value = this.X[Int32_Fast_val].val;
                    break;
                case "Y":
                    value = this.Y[Int32_Fast_val].val;
                    break;
                case "M":
                    value = this.M[Int32_Fast_val].val;
                    break;
                case "S":
                    value = this.S[Int32_Fast_val].val;
                    break;
                case "T":
                    value = this.T[Int32_Fast_val].IsTimeUp();
                    break;

                default:
                    break;
            }      
                                   
        }

        public void Set_DeviceFast(String device ,int index, bool value)
        {
            if (device.Contains("Z")) device = DeviceIndexFast(device);
            switch (device)
            {
                case "X":
                    this.X[index].val = value;
                    break;
                case "Y":
                    this.Y[index].val = value;
                    break;
                case "M":
                    this.M[index].val = value;
                    break;
                case "S":
                    this.S[index].val = value;
                    break;
                case "T":
                    if (value) this.T[index].TimerStart();
                    else this.T[index].TimerStop();
                    break;
                default:
                    break;
            }
        }
        public void Set_DeviceFast(String device , bool value)
        {
            if (device.Contains("Z")) device = DeviceIndexFast(device);
            Int32_Fast_val = int.Parse(device.Substring(1, device.Length - 1));
            switch (device.Substring(0, 1))
            {

                case "X":
                    this.X[Int32_Fast_val].val = value;
                    break;
                case "Y":
                    this.Y[Int32_Fast_val].val = value;
                    break;
                case "M":
                    this.M[Int32_Fast_val].val = value;
                    break;
                case "S":
                    this.S[Int32_Fast_val].val = value;
                    break;
                case "T":
                    if (value) this.T[Int32_Fast_val].TimerStart();
                    else this.T[Int32_Fast_val].TimerStop();
                    break;
                default:
                    break;
            }      
        }

        public int Get_DataFast(String device, int index)
        {
            if (device.Contains("Z")) device = DeviceIndexFast(device);
            switch (device)
            {
                case "D":
                    return this.D[index].val;
                    break;
                case "R":
                    return this.R[index].val;
                    break;
                case "F":
                    return this.F[index].val;
                    break;
                case "Z":
                    return this.Z[index].val;
                    break;
                case "T":
                    return this.T[index].GetTimer();
                    break;
                case "K":
                    return index;
                    break;
                default:
                    return -1;
                    break;
            }

        }
        public void Get_DataFast(String device, out int value)
        {
            value = 0;
            if (device.Contains("Z")) device = DeviceIndexFast(device);
            Int32_Fast_val = int.Parse(device.Substring(1, device.Length - 1));
            switch (device.Substring(0, 1))
            {
                case "D":
                    value = this.D[Int32_Fast_val].val;
                    break;
                case "R":
                    value = this.R[Int32_Fast_val].val;
                    break;
                case "F":
                    value = this.F[Int32_Fast_val].val;
                    break;
                case "Z":
                    value = this.Z[Int32_Fast_val].val;
                    break;
                case "T":
                    value = this.T[Int32_Fast_val].GetTimer();
                    break;
                case "K":
                    value = Int32_Fast_val;
                    break;
                default:
                    break;
            }
        }

        public void Set_DataFast(String device, int index, int value)
        {
            if (device.Contains("Z")) device = DeviceIndexFast(device);
            switch (device)
            {

                case "D":
                    this.D[index].val = value;
                    break;
                case "R":
                    this.R[index].val = value;
                    break;
                case "F":
                    this.F[index].val = value;
                    break;
                case "Z":
                    this.Z[index].val = value;
                    break;

                default:
                    break;
            }
        }
        public void Set_DataFast(String device, int value)
        {
            if (device.Contains("Z")) device = DeviceIndexFast(device);
            Int32_Fast_val = int.Parse(device.Substring(1, device.Length - 1));
            switch (device.Substring(0, 1))
            {

                case "D":
                    this.D[Int32_Fast_val].val = value;
                    break;
                case "R":
                    this.R[Int32_Fast_val].val = value;
                    break;
                case "F":
                    this.F[Int32_Fast_val].val = value;
                    break;
                case "Z":
                    this.Z[Int32_Fast_val].val = value;
                    break;
  
                default:
                    break;
            }
        }
        /*Int32 Int32_Ztemp0 = 0;
        Int32 Int32_Ztemp1 = 0;
        Int32 Int32_ZIndex = 0;
        String str_Ztemp0;
        String str_Ztemp1;
        String str_Ztemp2;*/
        public string DeviceIndexFast(String device)
        {
            Int32 Int32_Ztemp0 = 0;
            Int32 Int32_Ztemp1 = 0;
            Int32 Int32_ZIndex = 0;
            String str_Ztemp0;
            String str_Ztemp1;
            String str_Ztemp2;

            str_Ztemp0 = device.Remove(1);
            Int32_ZIndex = device.IndexOf("Z");
            if (str_Ztemp0 == "Z" || Int32_ZIndex < 0)
            {

            }
            else
            {
                str_Ztemp1 = device.Substring(0, Int32_ZIndex);
                str_Ztemp2 = device.Substring(Int32_ZIndex);
                Int32_Ztemp0 = Get_DataFast_Ex(str_Ztemp2);
                Int32_Ztemp1 = Int32.Parse(str_Ztemp1.Substring(1));
                device = str_Ztemp1.Remove(1) + (Int32_Ztemp0 + Int32_Ztemp1).ToString();

            }
            return device;
        }


        public Int64 Get_DoubleWord(String device)
        {
            object temp0_low;
            object temp0_high;
            Int64 temp0 = 0;
            String Device1_low = device;
            String Device1_high = DEVICE.DeviceOffset(Device1_low, 1);
            if (Device1_low.Substring(0, 1) == "K")
            {
                temp0 = Convert.ToInt64(Device1_low.Substring(1, Device1_low.Length - 1));
            }
            else
            {
                this.Get_Device(Device1_low, out temp0_low);
                this.Get_Device(Device1_high, out temp0_high);
                myConvert.Int32轉Int64(ref temp0, (int)temp0_high, (int)temp0_low);
            }
            return temp0;
        }
        public void Set_DoubleWord(String device , Int64 value)
        {
            int temp0_low = 0;
            int temp0_high = 0;
            String Device1_low = device;
            String Device1_high = DEVICE.DeviceOffset(Device1_low, 1);
            myConvert.Int64轉Int32(value, ref temp0_high, ref temp0_low);
            this.Set_Device(Device1_low, temp0_low);
            this.Set_Device(Device1_high, temp0_high);
        }

        public List<string[]> Get_Comment()
        {
            string Device;
            object comment;
            List<string[]> str_Array_temp = new List<string[]>();
            for (int i = 0; i < this.X_count; i++)
            {
                Device = "X" + i.ToString();
                this.Get_Device(Device, 0, out comment);
                string str_comment = (string)comment;
                if (str_comment != "" && str_comment != null && str_comment != "#None")
                {
                    List<string> str_temp = new List<string>();
                    str_temp.Add(Device);
                    str_temp.Add(str_comment);
                    str_Array_temp.Add(str_temp.ToArray());
                }
            }
            for (int i = 0; i < this.Y_count; i++)
            {
                Device = "Y" + i.ToString();
                this.Get_Device(Device, 0, out comment);
                string str_comment = (string)comment;
                if (str_comment != "" && str_comment != null && str_comment != "#None")
                {
                    List<string> str_temp = new List<string>();
                    str_temp.Add(Device);
                    str_temp.Add(str_comment);
                    str_Array_temp.Add(str_temp.ToArray());
                }
            }
            for (int i = 0; i < this.M_count; i++)
            {
                Device = "M" + i.ToString();
                this.Get_Device(Device, 0, out comment);
                string str_comment = (string)comment;
                if (str_comment != "" && str_comment != null && str_comment != "#None")
                {
                    List<string> str_temp = new List<string>();
                    str_temp.Add(Device);
                    str_temp.Add(str_comment);
                    str_Array_temp.Add(str_temp.ToArray());
                }
            }
            for (int i = 0; i < this.S_count; i++)
            {
                Device = "S" + i.ToString();
                this.Get_Device(Device, 0, out comment);
                string str_comment = (string)comment;
                if (str_comment != "" && str_comment != null && str_comment != "#None")
                {
                    List<string> str_temp = new List<string>();
                    str_temp.Add(Device);
                    str_temp.Add(str_comment);
                    str_Array_temp.Add(str_temp.ToArray());
                }
            }
            for (int i = 0; i < this.T_count; i++)
            {
                Device = "T" + i.ToString();
                this.Get_Device(Device, 0, out comment);
                string str_comment = (string)comment;
                if (str_comment != "" && str_comment != null && str_comment != "#None")
                {
                    List<string> str_temp = new List<string>();
                    str_temp.Add(Device);
                    str_temp.Add(str_comment);
                    str_Array_temp.Add(str_temp.ToArray());
                }
            }
            for (int i = 0; i < this.D_count; i++)
            {
                Device = "D" + i.ToString();
                this.Get_Device(Device, 0, out comment);
                string str_comment = (string)comment;
                if (str_comment != "" && str_comment != null && str_comment != "#None")
                {
                    List<string> str_temp = new List<string>();
                    str_temp.Add(Device);
                    str_temp.Add(str_comment);
                    str_Array_temp.Add(str_temp.ToArray());
                }
            }
            for (int i = 0; i < this.R_count; i++)
            {
                Device = "R" + i.ToString();
                this.Get_Device(Device, 0, out comment);
                string str_comment = (string)comment;
                if (str_comment != "" && str_comment != null && str_comment != "#None")
                {
                    List<string> str_temp = new List<string>();
                    str_temp.Add(Device);
                    str_temp.Add(str_comment);
                    str_Array_temp.Add(str_temp.ToArray());
                }
            }
            for (int i = 0; i < this.F_count; i++)
            {
                Device = "F" + i.ToString();
                this.Get_Device(Device, 0, out comment);
                string str_comment = (string)comment;
                if (str_comment != "" && str_comment != null && str_comment != "#None")
                {
                    List<string> str_temp = new List<string>();
                    str_temp.Add(Device);
                    str_temp.Add(str_comment);
                    str_Array_temp.Add(str_temp.ToArray());
                }
            }
            for (int i = 0; i < this.Z_count; i++)
            {
                Device = "Z" + i.ToString();
                this.Get_Device(Device, 0, out comment);
                string str_comment = (string)comment;
                if (str_comment != "" && str_comment != null && str_comment != "#None")
                {
                    List<string> str_temp = new List<string>();
                    str_temp.Add(Device);
                    str_temp.Add(str_comment);
                    str_Array_temp.Add(str_temp.ToArray());
                }
            }
            return str_Array_temp;
        }
        public void Set_Comment(List<string[]> List_str)
        {
            foreach(string[] str_array in List_str)
            {
                if(str_array.Length == 2)
                {
                    Set_Device(str_array[0], str_array[1]);
                }
            }
        }
        public void Clear_Comment()
        {
            for (int i = 0; i < this.X_count; i++)
            {
                X[i].comment[0] = "";
            }
            for (int i = 0; i < this.Y_count; i++)
            {
                Y[i].comment[0] = "";
            }
            for (int i = 0; i < this.M_count; i++)
            {
                M[i].comment[0] = "";
            }
            for (int i = 0; i < this.S_count; i++)
            {
                S[i].comment[0] = "";
            }
            for (int i = 0; i < this.D_count; i++)
            {
                D[i].comment[0] = "";
            }
            for (int i = 0; i < this.R_count; i++)
            {
                R[i].comment[0] = "";
            }
            for (int i = 0; i < this.F_count; i++)
            {
                F[i].comment[0] = "";
            }
            for (int i = 0; i < this.T_count; i++)
            {
                T[i].comment[0] = "";
            }
        }

        public List<object[]> GetAllValue()
        {
            List<object[]> allValue = new List<object[]>();
            List<object> DeviceValue = new List<object>();
            string Device;
            object value;
            DeviceValue.Add("X");
            for (int i = 0; i < this.X_count; i++)
            {
                Device = "X" + i.ToString();
                this.Get_Device(Device, out value);
                DeviceValue.Add(value);
            }
            allValue.Add(DeviceValue.ToArray());
            DeviceValue.Clear();

            DeviceValue.Add("Y");
            for (int i = 0; i < this.Y_count; i++)
            {
                Device = "Y" + i.ToString();
                this.Get_Device(Device, out value);
                DeviceValue.Add(value);
            }
            allValue.Add(DeviceValue.ToArray());
            DeviceValue.Clear();

            DeviceValue.Add("M");
            for (int i = 0; i < this.M_count; i++)
            {
                Device = "M" + i.ToString();
                this.Get_Device(Device, out value);
                DeviceValue.Add(value);
            }
            allValue.Add(DeviceValue.ToArray());
            DeviceValue.Clear();

            DeviceValue.Add("S");
            for (int i = 0; i < this.S_count; i++)
            {
                Device = "S" + i.ToString();
                this.Get_Device(Device, out value);
                DeviceValue.Add(value);
            }
            allValue.Add(DeviceValue.ToArray());
            DeviceValue.Clear();

            DeviceValue.Add("D");
            for (int i = 0; i < this.D_count; i++)
            {
                Device = "D" + i.ToString();
                this.Get_Device(Device, out value);
                DeviceValue.Add(value);
            }
            allValue.Add(DeviceValue.ToArray());
            DeviceValue.Clear();

            DeviceValue.Add("R");
            for (int i = 0; i < this.R_count; i++)
            {
                Device = "R" + i.ToString();
                this.Get_Device(Device, out value);
                DeviceValue.Add(value);
            }
            allValue.Add(DeviceValue.ToArray());
            DeviceValue.Clear();

            DeviceValue.Add("F");
            for (int i = 0; i < this.F_count; i++)
            {
                Device = "F" + i.ToString();
                this.Get_Device(Device, out value);
                DeviceValue.Add(value);
            }
            allValue.Add(DeviceValue.ToArray());
            DeviceValue.Clear();

            DeviceValue.Add("Z");
            for (int i = 0; i < this.Z_count; i++)
            {
                Device = "Z" + i.ToString();
                this.Get_Device(Device, out value);
                DeviceValue.Add(value);
            }
            allValue.Add(DeviceValue.ToArray());
            DeviceValue.Clear();

            return allValue;
        }
        public void SetAllValue( List<object[]> allValue)
        {
            if (allValue != null)
            {
                foreach (object[] obj_array in allValue)
                {
                    if (obj_array[0] is string)
                    {
                        string Device = (string)obj_array[0];
                        if (Device == "X" || Device == "Y" || Device == "M" || Device == "S")
                        {
                            for (int i = 1; i < obj_array.Length; i++)
                            {
                                if (obj_array[i].GetType().Name == "Boolean")
                                {
                                    this.Set_Device(Device + (i - 1).ToString(), (bool)obj_array[i]);
                                }
                            }
                        }
                        if (Device == "D" || Device == "R" || Device == "F" || Device == "Z")
                        {
                            for (int i = 1; i < obj_array.Length; i++)
                            {
                                if (obj_array[i].GetType().Name == "Int32")
                                {
                                    this.Set_Device(Device + (i - 1).ToString(), (int)obj_array[i]);
                                }
                            }
                        }
                    }
                
                }
            }
  
        }

        public List<object[]> Get_F_Device_Value()
        {
            List<object[]> allValue = new List<object[]>();
            List<object> DeviceValue = new List<object>();
            string Device;
            object value;         
            DeviceValue.Add("F");
            for (int i = 0; i < this.F_count; i++)
            {
                Device = "F" + i.ToString();
                this.Get_Device(Device, out value);
                DeviceValue.Add(value);
            }
            allValue.Add(DeviceValue.ToArray());
            DeviceValue.Clear();       
            return allValue;
        }
        public void Set_F_Device_Value(List<object[]> F_Device_Value)
        {
            if (F_Device_Value != null)
            {
                foreach (object[] obj_array in F_Device_Value)
                {
                    if (obj_array[0] is string)
                    {
                        string Device = (string)obj_array[0];
                
                        if (Device == "F")
                        {
                            for (int i = 1; i < obj_array.Length; i++)
                            {
                                if (obj_array[i].GetType().Name == "Int32")
                                {
                                    this.Set_Device(Device + (i - 1).ToString(), (int)obj_array[i]);
                                }
                            }
                        }
                    }

                }
            }

        }
        public List<object[]> Get_D_Device_Value()
        {
            List<object[]> allValue = new List<object[]>();
            List<object> DeviceValue = new List<object>();
            string Device;
            object value;
            DeviceValue.Add("D");
            for (int i = 0; i < this.D_count; i++)
            {
                Device = "D" + i.ToString();
                this.Get_Device(Device, out value);
                DeviceValue.Add(value);
            }
            allValue.Add(DeviceValue.ToArray());
            DeviceValue.Clear();
            return allValue;
        }
        public void Set_D_Device_Value(List<object[]> D_Device_Value)
        {
            if (D_Device_Value != null)
            {
                foreach (object[] obj_array in D_Device_Value)
                {
                    if (obj_array[0] is string)
                    {
                        string Device = (string)obj_array[0];

                        if (Device == "D")
                        {
                            for (int i = 1; i < obj_array.Length; i++)
                            {
                                if (obj_array[i].GetType().Name == "Int32")
                                {
                                    this.Set_Device(Device + (i - 1).ToString(), (int)obj_array[i]);
                                }
                            }
                        }
                    }

                }
            }

        }
        public List<object[]> Get_R_Device_Value()
        {
            List<object[]> allValue = new List<object[]>();
            List<object> DeviceValue = new List<object>();
            string Device;
            object value;
            DeviceValue.Add("R");
            for (int i = 0; i < this.R_count; i++)
            {
                Device = "R" + i.ToString();
                this.Get_Device(Device, out value);
                DeviceValue.Add(value);
            }
            allValue.Add(DeviceValue.ToArray());
            DeviceValue.Clear();
            return allValue;
        }
        public void Set_R_Device_Value(List<object[]> R_Device_Value)
        {
            if (R_Device_Value != null)
            {
                foreach (object[] obj_array in R_Device_Value)
                {
                    if (obj_array[0] is string)
                    {
                        string Device = (string)obj_array[0];

                        if (Device == "R")
                        {
                            for (int i = 1; i < obj_array.Length; i++)
                            {
                                if (obj_array[i].GetType().Name == "Int32")
                                {
                                    this.Set_Device(Device + (i - 1).ToString(), (int)obj_array[i]);
                                }
                            }
                        }
                    }

                }
            }

        }
        public List<object[]> Get_M_Device_Value()
        {
            List<object[]> allValue = new List<object[]>();
            List<object> DeviceValue = new List<object>();
            string Device;
            object value;
            DeviceValue.Add("M");
            for (int i = 0; i < this.M_count; i++)
            {
                Device = "M" + i.ToString();
                this.Get_Device(Device, out value);
                DeviceValue.Add(value);
            }
            allValue.Add(DeviceValue.ToArray());
            DeviceValue.Clear();
            return allValue;
        }
        public void Set_M_Device_Value(List<object[]> M_Device_Value)
        {
            if (M_Device_Value != null)
            {
                foreach (object[] obj_array in M_Device_Value)
                {
                    if (obj_array[0] is string)
                    {
                        string Device = (string)obj_array[0];

                        if (Device == "M")
                        {
                            for (int i = 1; i < obj_array.Length; i++)
                            {
                                if (obj_array[i] is bool)
                                {
                                    this.Set_Device(Device + (i - 1).ToString(), (bool)obj_array[i]);
                                }
                            }
                        }
                    }

                }
            }

        }
        public List<object[]> Get_S_Device_Value()
        {
            List<object[]> allValue = new List<object[]>();
            List<object> DeviceValue = new List<object>();
            string Device;
            object value;
            DeviceValue.Add("S");
            for (int i = 0; i < this.S_count; i++)
            {
                Device = "S" + i.ToString();
                this.Get_Device(Device, out value);
                DeviceValue.Add(value);
            }
            allValue.Add(DeviceValue.ToArray());
            DeviceValue.Clear();
            return allValue;
        }
        public void Set_S_Device_Value(List<object[]> S_Device_Value)
        {
            if (S_Device_Value != null)
            {
                foreach (object[] obj_array in S_Device_Value)
                {
                    if (obj_array[0] is string)
                    {
                        string Device = (string)obj_array[0];

                        if (Device == "S")
                        {
                            for (int i = 1; i < obj_array.Length; i++)
                            {
                                if (obj_array[i] is bool)
                                {
                                    this.Set_Device(Device + (i - 1).ToString(), (bool)obj_array[i]);
                                }
                            }
                        }
                    }

                }
            }

        }
        public string DeviceIndex(String device)
        {
            string[] temp;
            if(TestDevice(device, out temp))
            {
                if (temp.Length == 2)
                {
                    string first_device = temp[0].Remove(1);
                    string first_num = temp[0].Substring(1);
                    string num;
                    object int_temp;
                    if(Get_Device(temp[1], out int_temp))
                    {
                        num = (Convert.ToInt32(first_num) + (int)int_temp).ToString();
                        device = first_device + num;
                    }            
                }
            }

            return device;
        }

        static public bool TestDevice(String device)
        {
            bool FLAG = false;
            string temp = "";
            FLAG = TestDevice(device, ref temp);
            return FLAG;
        }
        static public bool TestDevice(String device, ref String str_temp)
        {

            int 錯誤次數 = 0;
            if (device == null) return false;
            if (device.Length > 0 )
            {
                string str_接點類型 = "";
                string str_接點編號 = "";          
                str_temp = "";
                String[] Str_temp;
                Str_temp = new string[1];
                Str_temp[0] = device;
                if (device.Length > 1)
                {
                    if (device.Remove(1) != "Z") Str_temp = device.Split(new char[1] { 'Z' }, StringSplitOptions.None);              
                }
               
                if (Str_temp.Length == 1 || Str_temp.Length == 2)
                {
                    if (Str_temp[0].Length > 1)
                    {


                        int int_接點編號 = 0;
                        str_接點類型 = Str_temp[0].Remove(1);
                        str_接點編號 = Str_temp[0].Substring(1);
                        if (!int.TryParse(str_接點編號, out int_接點編號)) 錯誤次數++;

                        if (str_接點類型 == "X") ;
                        else if (str_接點類型 == "Y") ;
                        else if (str_接點類型 == "M") ;
                        else if (str_接點類型 == "S") ;
                        else if (str_接點類型 == "D") ;
                        else if (str_接點類型 == "R") ;
                        else if (str_接點類型 == "F") ;
                        else if (str_接點類型 == "T") ;
                        else if (str_接點類型 == "Z") ;
                        else if (str_接點類型 == "K" && Str_temp.Length == 1) ;
                        else 錯誤次數++;
                        if (錯誤次數 == 0) str_temp = int_接點編號.ToString();
                    }
                    else 錯誤次數++;

                    if (Str_temp.Length == 2  && device.Remove(1) != "T")
                    {
                        if (Str_temp[1].Length > 0)
                        {
                            int int_接點編號 = 0;
                            str_接點編號 = Str_temp[1].Substring(1);
                            if (!int.TryParse(Str_temp[1], out int_接點編號)) 錯誤次數++;
                            if (錯誤次數 == 0) str_temp += "Z" + int_接點編號.ToString();
                        }
                        else 錯誤次數++;
                    }
                }
                else 錯誤次數++;
            }
            else 錯誤次數++;


            return (錯誤次數 == 0);
        }
        static public bool TestDevice(string device, out string[] finnal_device)
        {
            bool flag = false;
            finnal_device = new string[0];
            string temp = "";
            flag = TestDevice(device, ref temp);
            if (flag)
            {
                string first_device = device.Remove(1);
                int Z_index = device.IndexOf("Z");
                if (first_device == "Z" || Z_index < 0)
                {
                    finnal_device = new string[1];
                    finnal_device[0] = device;
                }
                else
                {
                    finnal_device = new string[2];
                    finnal_device[0] = device.Substring(0, Z_index);
                    finnal_device[1] = device.Substring(Z_index);
                }
            }
            return flag;
        }




        static public string DeviceOffset(string device,int offset)
        {
            string[] str_array;
            if (TestDevice(device, out str_array))
            {
                string str_temp = str_array[0].Substring(0, 1);
                string str_num = str_array[0].Substring(1, str_array[0].Length - 1);
                int int_num = Convert.ToInt32(str_num) + offset;
                device = str_temp + int_num.ToString();
                if (str_array.Length == 2)
                {
                    device += str_array[1];
                }
            }

            return device;
        }
        static public void KtoDoubleWord(string K, ref string K_high, ref string K_low)
        {
            Int64 temp = Convert.ToInt64(K.Substring(1, K.Length - 1));
            Int32 temp_low = 0;
            Int32 temp_high = 0;
            myConvert.Int64轉Int32(temp, ref temp_low, ref temp_high);
            K_low = "K" + temp_low.ToString();
            K_high = "K" + temp_high.ToString();

        }

        private void 檢查是否為8進位(ref bool FLAG, int val)
        {
            FLAG = true;
            return;
            FLAG = false;
            int temp0 = val;
            int 位數 = 0;
            if (val >= 0)
            {
                while (true)
                {
                    if ((int)(val / (int)Math.Pow(10, 位數)) > 0)
                    {
                        int i = val / (int)Math.Pow(10, 位數);
                        位數++;
                    }
                    else break;
                }
                while (true)
                {
                    int temp1 = temp0 / (int)Math.Pow(10, 位數);
                    temp0 = temp0 - temp1 * (int)Math.Pow(10, 位數);

                    if (temp1 > 7) break;
                    if (位數 == 0)
                    {
                        FLAG = true;
                        break;
                    }
                    位數--;
                }
            }
        }
    }
}
