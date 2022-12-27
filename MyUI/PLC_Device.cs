using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;
using Basic;
namespace MyUI
{
    public class MyTimer
    {
        private bool OnTick = false;
        public double TickTime = 0;
        static private Stopwatch stopwatch = new Stopwatch();
        PLC_Device pLC_Device;
        public MyTimer()
        {
            stopwatch.Start();
        }
        public MyTimer(double TickTime)
        {
            stopwatch.Start();
            this.StartTickTime(TickTime);
        }
        public MyTimer(string adress)
        {
            pLC_Device = new PLC_Device(adress);
            stopwatch.Start();
        }
        private double CycleTime_start;
        public void StartTickTime(double TickTime)
        {
            this.TickTime = TickTime;
            if (!OnTick)
            {
                CycleTime_start = stopwatch.Elapsed.TotalMilliseconds;
                OnTick = true;
            }
        }

        public void StartTickTime()
        {
            this.TickTime = pLC_Device.GetValue();
            if (!OnTick)
            {
                CycleTime_start = stopwatch.Elapsed.TotalMilliseconds;
                OnTick = true;
            }
        }
        public double GetTickTime()
        {
            return stopwatch.Elapsed.TotalMilliseconds - CycleTime_start;
        }
        public void TickStop()
        {
            this.OnTick = false;
        }
        public bool IsTimeOut()
        {
            //if (OnTick == false) return false;
            if ((stopwatch.Elapsed.TotalMilliseconds - CycleTime_start) >= TickTime)
            {
                OnTick = false;
                return true;
            }
            else return false;
        }
        public void SetComment(string comment)
        {
            pLC_Device.SetComment(comment);
        }

      
        public override string ToString()
        {
            return this.ToString(true);
        }
        public string ToString(bool retick)
        {
            string text = this.GetTickTime().ToString("0.000") + "ms";
            if (retick)
            {
                this.TickStop();
                this.StartTickTime(999999);
            }        
            return text;

        }

    }
    public class PLC_Device : UserControl
    {
        public delegate void ValueChangeEventHandler(object Value);
        public event ValueChangeEventHandler ValueChangeEvent;

        private static LadderProperty.DEVICE PLC;
        private static LadderProperty.DEVICE PLC_System;
        public static void InitPLC(LadderConnection.LowerMachine pLC)
        {
            PLC = pLC.properties.Device;
            PLC_System = pLC.properties.device_system;
        }
        public enum EnumValueType : int
        {
            Int32 = 0, Bollean, Others, Int64
        }
        private EnumValueType _EnumValueType;
        public EnumValueType enumValueType
        {
            get
            {
                return _EnumValueType;
            }
            private set
            {
                _EnumValueType = value;
            }
        }
        private string _adress = "";
        private string adress
        {
            get
            {
                return _adress;
            }
            set
            {
                enumValueType = EnumValueType.Others;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "D" || temp == "R" || temp == "F" || temp == "Z")
                    {
                        this.enumValueType = PLC_Device.EnumValueType.Int32;
                    }
                    else if (temp == "S" || temp == "M" || temp == "X" || temp == "Y")
                    {
                        this.enumValueType = PLC_Device.EnumValueType.Bollean;
                    }
                }

                _adress = value;
            }
        }
        private object _value = new object();
        private object value
        {
            get
            {
                return this._value;
            }
        }
        private bool flag_SetValueIsOK = false;
        private bool bool_buf = false;
        private int value_buf = -1;

        public bool Bool
        {
            get
            {
                bool value = GetBool();
                if(value != bool_buf)
                {
                    bool_buf = value;
                    ValueChangeEvent?.Invoke(value);
                }
                return value;
            }
            set
            {
                if (value is bool) SetValue(value);
                if (value != bool_buf)
                {
                    bool_buf = value;
                    ValueChangeEvent?.Invoke(value);
                }
            }
        }
        public int Value
        {
            get
            {
                int value = GetValue();
                if (value != value_buf)
                {
                    value_buf = value;
                    ValueChangeEvent?.Invoke(value);
                }
                return GetValue();
            }
            set
            {
                if (value is int) SetValue(value);
                if (value != value_buf)
                {
                    value_buf = value;
                    ValueChangeEvent?.Invoke(value);
                }
            }
        }
        public Int64 DoubleValue
        {
            get
            {
                return GetDoubleWord();
            }
            set
            {
                SetDoubleWord(value);
            }
        }
        public PLC_Device()
        {

        }
        public PLC_Device(string adress)
        {      
            this.adress = adress;            
        }
        public PLC_Device(string adress ,string comment)
        {
            this.adress = adress;
            this.SetComment(comment);
        }
        public int GetValue()
        {
            if (!this.adress.StringIsEmpty())
            {
                object _value = new object();
                if (PLC.Get_Device(this.adress, out _value))
                {
                    if (_value is int) return (int)_value;
                }
                return -66437068;
            }
            else
            {
                if (!(_value is int)) _value = 0;
                return (int)_value;
            }
        }
        public Int64 GetDoubleWord()
        {
            return PLC.Get_DoubleWord(this.adress);
        }
        public bool GetBool()
        {
            if(!this.adress.StringIsEmpty())
            {
                object _value = new object();
                if (PLC.Get_Device(this.adress, out _value))
                {
                    if (_value is bool) return (bool)_value;

                }
                return false;
            }
            else
            {
                if (!(_value is bool)) _value = false;
                return (bool)_value;
            }
         
        }
        public bool SetValue(object value)
        {
            flag_SetValueIsOK = false;
            if (!this.adress.StringIsEmpty())
            {
                if (enumValueType == EnumValueType.Int32 && (value is Int32))
                {
                    PLC.Set_Device(this.adress, (Int32)value);
                    _value = value;
                    flag_SetValueIsOK = true;
                }
                else if (enumValueType == EnumValueType.Bollean && (value is Boolean))
                {
                    if (this.adress.Substring(0, 1) == "X")
                    {
                        PLC_System.Set_Device(this.adress, (Boolean)value);
                    }
                    else
                    {
                        PLC.Set_Device(this.adress, (Boolean)value);
                    }
                    _value = value;
                    flag_SetValueIsOK = true;
                }
            }
            else
            {
                this._value = value;
            }
            return flag_SetValueIsOK;
        }
        public void SetDoubleWord(object value)
        {
            if (value is Int64) PLC.Set_DoubleWord(this.adress, (Int64)value);    
        }
        public string GetAdress()
        {
            return _adress;
        }
        public void SetAdress(string adress)
        {
            this.adress = adress; 
        }
        public void SetComment(string comment)
        {
            if (this.adress != "")
            {
                PLC.Set_Device(this.adress,"*" + comment);
            }       
        }


   

    }
    public class PLC_Method
    {
        private double CycleTime;
        private double CycleTime_start;
        private Stopwatch stopwatch = new Stopwatch();
        private bool Init = false;

        public delegate void Method();
        private Basic.MyThread.MethodDelegate _MainMethod;
        private Basic.MyThread.MethodDelegate _FinishMethod;
        private Basic.MyThread.MethodDelegate _InitMethod;
        private int StepCnt_buf = 0;
        private int StepCnt = 65535;

        private List<MethodStepClass> ListMethodStep = new List<MethodStepClass>();
        public class MethodStepClass
        {
            private int StepNum = 0;
            public MethodStepClass(int StepNum , MethodStep MethodStep)
            {
                this.StepNum = StepNum;
                this._MethodStep = MethodStep;
            }
            public bool CheckStep(int CheckStepNum)
            {
                return (CheckStepNum == this.StepNum);
            }
            public delegate void MethodStep(ref int cnt);
            public MethodStep _MethodStep;
        }
        public void AddStepMethod(int StepNum, MethodStepClass.MethodStep MethodStep)
        {
            this.ListMethodStep.Add(new MethodStepClass(StepNum, MethodStep));
        }

        private bool flag_Trigger = false;
        private bool flag_Ready = false;
        private bool flag_Busy = false;
        private PLC_Device PLC_Device_Trigger;
        private PLC_Device PLC_Device_Ready;
        private PLC_Device PLC_Device_Step;
        public PLC_Method(PLC_Device PLC_Device_Trigger, PLC_Device PLC_Device_Ready, PLC_Device PLC_Device_Step)
        {
            this._MainMethod = new Basic.MyThread.MethodDelegate(sub_Method);
            this.PLC_Device_Trigger = PLC_Device_Trigger;
            this.PLC_Device_Ready = PLC_Device_Ready;
            this.PLC_Device_Step = PLC_Device_Step;
            this.stopwatch.Start();
        }
        public PLC_Method(PLC_Device PLC_Device_Trigger, PLC_Device PLC_Device_Ready)
        {
            this._MainMethod = new Basic.MyThread.MethodDelegate(sub_Method);
            this.PLC_Device_Trigger = PLC_Device_Trigger;
            this.PLC_Device_Ready = PLC_Device_Ready;
            this.stopwatch.Start();
        }
        public PLC_Method(string TriggerAdress, string ReadyAdress)
        {
            this._MainMethod = new Basic.MyThread.MethodDelegate(sub_Method);
            this.PLC_Device_Trigger = new PLC_Device(TriggerAdress);
            this.PLC_Device_Ready = new PLC_Device(ReadyAdress);
            this.stopwatch.Start();
        }
        public PLC_Method(string TriggerAdress, string ReadyAdress, Basic.MyThread.MethodDelegate InitMethod, Basic.MyThread.MethodDelegate FinishMethod)
        {
            this._MainMethod = new Basic.MyThread.MethodDelegate(sub_Method);
            this._FinishMethod = FinishMethod;
            this._InitMethod = InitMethod;
            this.PLC_Device_Trigger = new PLC_Device(TriggerAdress);
            this.PLC_Device_Ready = new PLC_Device(ReadyAdress);

            this.stopwatch.Start();
        }
        public PLC_Method(string TriggerAdress, string ReadyAdress, string StepNumAdress)
        {
            this._MainMethod = new Basic.MyThread.MethodDelegate(sub_Method);
            this.PLC_Device_Trigger = new PLC_Device(TriggerAdress);
            this.PLC_Device_Ready = new PLC_Device(ReadyAdress);
            this.PLC_Device_Step = new PLC_Device(StepNumAdress);
            this.stopwatch.Start();
        }
        public PLC_Device GetTriggerDevice()
        {
            return this.PLC_Device_Trigger;
        }
        public PLC_Device GetReadyDevice()
        {
            return this.PLC_Device_Ready;
        }
        public PLC_Device GetStepDevice()
        {
            return this.PLC_Device_Step;
        }
        public void SetTrigger(bool state)
        {
            if (PLC_Device_Trigger != null) this.PLC_Device_Trigger.Bool = state;
            this.flag_Trigger = state;
        }
        public bool GetTrigger()
        {
            if (PLC_Device_Trigger != null) this.flag_Trigger = this.PLC_Device_Trigger.Bool;
            return this.flag_Trigger;
        }
        public void SetReady(bool state)
        {
            if (PLC_Device_Ready != null) this.PLC_Device_Ready.Bool = state;
            this.flag_Ready = state;
        }
        public bool GetReady()
        {
            if (PLC_Device_Ready != null) this.flag_Ready = this.PLC_Device_Ready.Bool;
            return this.flag_Ready;
        }
        public void SetBusy(bool state)
        {
            this.flag_Busy = state;
        }
        public bool GetBusy()
        {
            return this.flag_Busy;
        }
        public int GetStepCount()
        {
            return this.StepCnt;
        }
        public double GetCycleTime()
        {
            return this.CycleTime;
        }
        public Basic.MyThread.MethodDelegate GetMainMethod()
        {
            return this._MainMethod;
        }
        public void SetFinishMethod(Basic.MyThread.MethodDelegate Method)
        {
            this._FinishMethod = Method;
        }
        private void sub_Method()
        {
            if (this._InitMethod != null)
            {
                if (!this.Init)
                {
                    this._InitMethod();
                    this.Init = true;
                }
            }
            if (StepCnt == 65535)
            {
                this.SetReady(true);
                if (this.GetTrigger())
                {
                    CycleTime_start = stopwatch.Elapsed.TotalMilliseconds;   

                    this.SetReady(false);
                    this.SetBusy(true);
                    StepCnt = 1;
                }
            }
            for (int i = 0; i < ListMethodStep.Count; i++)
            {
                if (ListMethodStep[i] != null)
                {
                    if(this.ListMethodStep[i].CheckStep(StepCnt))
                    {
                        this.ListMethodStep[i]._MethodStep(ref StepCnt);
                    }
                }
            }
   
            if (StepCnt != 65535) if (this.GetReady()) StepCnt = 65500;
            if (StepCnt == 65500)
            {                    
                CycleTime = stopwatch.Elapsed.TotalMilliseconds - CycleTime_start;
                if (!this.GetTrigger())
                {
                    if (this._FinishMethod != null) this._FinishMethod(); 
                    StepCnt = 65535;
                    this.SetReady(true);
                }
            }
            if(StepCnt_buf != StepCnt)
            {
                StepCnt_buf = StepCnt;
                if (PLC_Device_Step != null) PLC_Device_Step.Value = StepCnt;
            }
        }
    }
}
