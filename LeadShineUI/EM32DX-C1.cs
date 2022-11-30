using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
using LadderUI;
using LadderConnection;
using MyUI;
namespace LeadShineUI
{
    [System.Drawing.ToolboxBitmap(typeof(DMC3000), "ECAN.bmp")]
    [Designer(typeof(ComponentSet.JLabelExDesigner))]  
    public partial class EM32DX_C1 : UserControl
    {
        #region 設備參數
        public enum enum_NodeNum : int
        {
            _1 = 1, _2, _3, _4, _5, _6, _7, _8
        }
        private enum_NodeNum _NodeNum = enum_NodeNum._1;
        [ReadOnly(false), Browsable(true), Category("設備參數"), Description(""), DefaultValue("")]
        public enum_NodeNum NodeNum
        {
            get
            {
                return this._NodeNum;
            }
            set
            {
                _NodeNum = value;
                this.設備名稱 = this.Get_StreamName();
            }
        }

        private int _CardNum = 0;
        [ReadOnly(false), Browsable(true), Category("設備參數"), Description(""), DefaultValue("")]
        public int CardNum
        {
            get
            {
                return this._CardNum;
            }
            set
            {
                this._CardNum = value;
                this.設備名稱 = this.Get_StreamName();
            }
        }

        [ReadOnly(true), Browsable(true), Category("設備參數"), Description(""), DefaultValue("")]
        public string 設備名稱
        {
            get 
            { 
                return this.numWordTextBox_StreamName.Text; 
            }
            set
            {
                this.numWordTextBox_StreamName.Text = value;
            }
        }

        private int _CycleTime = 1;
        [ReadOnly(false), Browsable(true), Category("設備參數"), Description(""), DefaultValue("")]
        public int CycleTime
        {
            get { return _CycleTime; }
            set
            {
                _CycleTime = value;
            }
        }
        #endregion

        #region UI
        public static readonly int InputNum = 16;
        public static readonly int OutputNum = 16;
        private List<PLC_Button> List_PLC_Button_Input = new List<PLC_Button>();
        private List<NumWordTextBox> List_NumWordTextBox_Input = new List<NumWordTextBox>();
        private List<PLC_Button> List_PLC_Button_Output = new List<PLC_Button>();
        private List<NumWordTextBox> List_NumWordTextBox_Output = new List<NumWordTextBox>();
        private System.Collections.BitArray BitArray_Input;
        private System.Collections.BitArray BitArray_Output;
        private string _device_adress = "";
        private bool _flag_output = false;
        private uint _output_port_value = 0;
        #region Input
        private void List_PLC_Button_Input_Init()
        {
            List_PLC_Button_Input.Add(plC_Button_I00);
            List_PLC_Button_Input.Add(plC_Button_I01);
            List_PLC_Button_Input.Add(plC_Button_I02);
            List_PLC_Button_Input.Add(plC_Button_I03);
            List_PLC_Button_Input.Add(plC_Button_I04);
            List_PLC_Button_Input.Add(plC_Button_I05);
            List_PLC_Button_Input.Add(plC_Button_I06);
            List_PLC_Button_Input.Add(plC_Button_I07);
            List_PLC_Button_Input.Add(plC_Button_I08);
            List_PLC_Button_Input.Add(plC_Button_I09);
            List_PLC_Button_Input.Add(plC_Button_I10);
            List_PLC_Button_Input.Add(plC_Button_I11);
            List_PLC_Button_Input.Add(plC_Button_I12);
            List_PLC_Button_Input.Add(plC_Button_I13);
            List_PLC_Button_Input.Add(plC_Button_I14);
            List_PLC_Button_Input.Add(plC_Button_I15);

        }
        private bool Get_Input_UI(int index)
        {
            return List_PLC_Button_Input[index].GetValue();
        }
        private void Set_Input_UI(int index, bool value)
        {
            List_PLC_Button_Input[index].SetValue(value);
        }

        private void List_NumWordTextBox_Input_Init()
        {
            List_NumWordTextBox_Input.Add(numWordTextBox_I00);
            List_NumWordTextBox_Input.Add(numWordTextBox_I01);
            List_NumWordTextBox_Input.Add(numWordTextBox_I02);
            List_NumWordTextBox_Input.Add(numWordTextBox_I03);
            List_NumWordTextBox_Input.Add(numWordTextBox_I04);
            List_NumWordTextBox_Input.Add(numWordTextBox_I05);
            List_NumWordTextBox_Input.Add(numWordTextBox_I06);
            List_NumWordTextBox_Input.Add(numWordTextBox_I07);
            List_NumWordTextBox_Input.Add(numWordTextBox_I08);
            List_NumWordTextBox_Input.Add(numWordTextBox_I09);
            List_NumWordTextBox_Input.Add(numWordTextBox_I10);
            List_NumWordTextBox_Input.Add(numWordTextBox_I11);
            List_NumWordTextBox_Input.Add(numWordTextBox_I12);
            List_NumWordTextBox_Input.Add(numWordTextBox_I13);
            List_NumWordTextBox_Input.Add(numWordTextBox_I14);
            List_NumWordTextBox_Input.Add(numWordTextBox_I15);

        }
        private void Set_Input_UI_Adress(int index, string Adress)
        {
            if (LadderProperty.DEVICE.TestDevice(Adress))
            {
                string temp = Adress.Remove(1);
                if (temp == "X")
                {
                    this.Invoke(new Action(delegate { List_NumWordTextBox_Input[index].Text = Adress; }));
                }
            }
        }
        private string Get_Input_UI_Adress(int index)
        {
            return List_NumWordTextBox_Input[index].Text;
        }
        #endregion
        #region Output
        public void List_PLC_Button_Output_Init()
        {
            List_PLC_Button_Output.Add(plC_Button_O00);
            List_PLC_Button_Output.Add(plC_Button_O01);
            List_PLC_Button_Output.Add(plC_Button_O02);
            List_PLC_Button_Output.Add(plC_Button_O03);
            List_PLC_Button_Output.Add(plC_Button_O04);
            List_PLC_Button_Output.Add(plC_Button_O05);
            List_PLC_Button_Output.Add(plC_Button_O06);
            List_PLC_Button_Output.Add(plC_Button_O07);
            List_PLC_Button_Output.Add(plC_Button_O08);
            List_PLC_Button_Output.Add(plC_Button_O09);
            List_PLC_Button_Output.Add(plC_Button_O10);
            List_PLC_Button_Output.Add(plC_Button_O11);
            List_PLC_Button_Output.Add(plC_Button_O12);
            List_PLC_Button_Output.Add(plC_Button_O13);
            List_PLC_Button_Output.Add(plC_Button_O14);
            List_PLC_Button_Output.Add(plC_Button_O15);


        }
        public bool Get_Output_UI(int index)
        {
            return List_PLC_Button_Output[index].GetValue();
        }
        public void Set_Output_UI(int index, bool value)
        {
            List_PLC_Button_Output[index].SetValue(value);
        }

        private void List_NumWordTextBox_Output_Init()
        {
            List_NumWordTextBox_Output.Add(numWordTextBox_O00);
            List_NumWordTextBox_Output.Add(numWordTextBox_O01);
            List_NumWordTextBox_Output.Add(numWordTextBox_O02);
            List_NumWordTextBox_Output.Add(numWordTextBox_O03);
            List_NumWordTextBox_Output.Add(numWordTextBox_O04);
            List_NumWordTextBox_Output.Add(numWordTextBox_O05);
            List_NumWordTextBox_Output.Add(numWordTextBox_O06);
            List_NumWordTextBox_Output.Add(numWordTextBox_O07);
            List_NumWordTextBox_Output.Add(numWordTextBox_O08);
            List_NumWordTextBox_Output.Add(numWordTextBox_O09);
            List_NumWordTextBox_Output.Add(numWordTextBox_O10);
            List_NumWordTextBox_Output.Add(numWordTextBox_O11);
            List_NumWordTextBox_Output.Add(numWordTextBox_O12);
            List_NumWordTextBox_Output.Add(numWordTextBox_O13);
            List_NumWordTextBox_Output.Add(numWordTextBox_O14);
            List_NumWordTextBox_Output.Add(numWordTextBox_O15);


        }
        public void Set_Output_UI_Adress(int index, string Adress)
        {
            if (LadderProperty.DEVICE.TestDevice(Adress))
            {
                string temp = Adress.Remove(1);
                if (temp == "Y")
                {
                    this.Invoke(new Action(delegate { List_NumWordTextBox_Output[index].Text = Adress; }));
                }
            }
        }
        public string Get_Output_UI_Adress(int index)
        {
            return List_NumWordTextBox_Output[index].Text;
        }
        #endregion
        public void Init_UI()
        {
            List_PLC_Button_Input_Init();
            List_PLC_Button_Output_Init();
            List_NumWordTextBox_Input_Init();
            List_NumWordTextBox_Output_Init();
        }
        public void Set_UI_Enable(bool enable)
        {
            this.Invoke(new Action(delegate
            {
                foreach (NumWordTextBox numWordTextBox in List_NumWordTextBox_Input)
                {
                    numWordTextBox.Enabled = enable;

                    if (!(LadderProperty.DEVICE.TestDevice(numWordTextBox.Text) && (numWordTextBox.Text.Remove(1) == "X")))
                    {
                        numWordTextBox.Text = "";
                    }
                }
                foreach (NumWordTextBox numWordTextBox in List_NumWordTextBox_Output)
                {
                    numWordTextBox.Enabled = enable;

                    if (!(LadderProperty.DEVICE.TestDevice(numWordTextBox.Text) && (numWordTextBox.Text.Remove(1) == "Y")))
                    {
                        numWordTextBox.Text = "";
                    }
                }
                this.SaveProperties();
            }));
        }

        #region StreamIO
        [Serializable]
        public class SaveClass
        {
            public List<string> Input_Adress = new List<string>();
            public List<string> Output_Adress = new List<string>();
        }
        public SaveClass GetSaveObject()
        {
            SaveClass saveClass = new SaveClass();

            foreach (NumWordTextBox numWordTextBox in List_NumWordTextBox_Input)
            {
                saveClass.Input_Adress.Add(numWordTextBox.Text);
            }
            foreach (NumWordTextBox numWordTextBox in List_NumWordTextBox_Output)
            {
                saveClass.Output_Adress.Add(numWordTextBox.Text);
            }
         
            return saveClass;
        }
        public void LoadObject(SaveClass saveClass)
        {
            this.Invoke(new Action(delegate
            {
               
                for (int i = 0; i < List_NumWordTextBox_Input.Count; i++)
                {
                    if (i < saveClass.Input_Adress.Count) List_NumWordTextBox_Input[i].Text = saveClass.Input_Adress[i];
                }
                for (int i = 0; i < List_NumWordTextBox_Output.Count; i++)
                {
                    if (i < saveClass.Output_Adress.Count) List_NumWordTextBox_Output[i].Text = saveClass.Output_Adress[i];
                }
            
                
            }));

        }

        private List<object> List_SaveClass = new List<object>();
        public void SaveProperties()
        {
            this.List_SaveClass.Clear();
            this.StreamName = @".\\DMC3000\\" + 設備名稱 + ".pro";
        
            this.List_SaveClass.Add(this.GetSaveObject());
            
            Basic.FileIO.SaveProperties(this.List_SaveClass, this.StreamName);
        }
        public void LoadProperties()
        {
            object temp = new object();
            this.List_SaveClass.Clear();
            this.StreamName = @".\\DMC3000\\" + 設備名稱 + ".pro";
            Basic.FileIO.LoadProperties(ref temp, StreamName);
            if (temp is List<object>)
            {
                this.List_SaveClass = (List<object>)temp;
            }
            try
            {
                this.LoadObject((SaveClass)this.List_SaveClass[0]);  

            }
            catch
            {

            }
        }
        #endregion
        #endregion

        private bool _IsOpen = false;
        [ReadOnly(false), Browsable(false), Category(""), Description(""), DefaultValue("")]
        public bool IsOpen
        {
            get
            {
                return this._IsOpen;
            }
            set
            {
      
                this._IsOpen = value;
            }
        }
        private Form Active_Form;
        private String StreamName;
        private LowerMachine PLC;
        private MyConvert myConvert = new MyConvert();
        private MyThread MyThread_Program;

        public EM32DX_C1()
        {
            InitializeComponent();
        }

        public void Run(Form form, LowerMachine lowerMachine)
        {
            this.PLC = lowerMachine;
            this.Active_Form = form;
            this.Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);

            this.Init();

            this.MyThread_Program = new MyThread(form);
            this.MyThread_Program.Add_Method(this.sub_Program);
            this.MyThread_Program.AutoRun(true);
            this.MyThread_Program.SetSleepTime(this.CycleTime);
            this.MyThread_Program.Trigger();

            this.PLC.Add_UI_Method(this.sub_RefreshUI);
  
        }
        public void Init()
        {
            this.設備名稱 = this.Get_StreamName();
            this.Init_UI();
            this.LoadProperties();
            this.IsOpen = true;
            this.Set_UI_Enable(!this.IsOpen);

     
        }
        private string Get_StreamName()
        {
            return "EM32DX_C1-" + CardNum.ToString("00") + "-" + ((int)NodeNum).ToString("00");
        }

        private void sub_RefreshUI()
        {
            if (PLC != null)
            {
                plC_Button_Open.SetValue(this.IsOpen);
                this.plC_Button_Open.Run(this.PLC);
    
                MyThread_Program.GetCycleTime(100, label_CycleTime);
            }
        }
        private void sub_Program()
        {
            if (PLC != null && this.IsOpen)
            {
                this.BitArray_Input = this.Get_InputPort_BitArray();
                for (int i = 0; i < 16; i++)
                {
                    this.Set_Input_UI(i, !BitArray_Input[i]);
                    this._device_adress = this.Get_Input_UI_Adress(i);
                    if (this._device_adress != string.Empty)
                    {
                        this.PLC.properties.device_system.Set_DeviceFast_Ex(this._device_adress, !this.BitArray_Input[i]);
                    }
                }

                this._output_port_value = this.Get_OutputPort();
                for (int i = 0; i < 16; i++)
                {
                    this._device_adress = this.Get_Output_UI_Adress(i);
                    if (this._device_adress != string.Empty)
                    {
                        this._flag_output = this.PLC.properties.device_system.Get_DeviceFast_Ex(this._device_adress);
                        this._output_port_value = myConvert.UInt32SetBit(!this._flag_output, this._output_port_value, i);
                    }
                }
                this.Set_OutputPort(this._output_port_value);
                this.BitArray_Output = this.Get_OutputPort_BitArray();
                for (int i = 0; i < 16; i++)
                {
                    this.Set_Output_UI(i, !this.BitArray_Output[i]);
                }
            }
        }

        #region Function

        public bool Get_InputBit(int bitnum)
        {
            return this.Get_InputPort_BitArray()[bitnum];
        }
        public System.Collections.BitArray Get_InputPort_BitArray()
        {
            return new System.Collections.BitArray(BitConverter.GetBytes(this.Get_InputPort()));
        }
        public uint Get_InputPort()
        {
            uint port_00_value = 0;
            Dmc3000.nmc_read_inport((ushort)this.CardNum, (ushort)NodeNum, 0, ref port_00_value);
            return port_00_value;
        }

        public bool Get_OutputBit(int bitnum)
        {
            return this.Get_OutputPort_BitArray()[bitnum];
        }
        public System.Collections.BitArray Get_OutputPort_BitArray()
        {
            return new System.Collections.BitArray(BitConverter.GetBytes(this.Get_OutputPort()));
        }
        public uint Get_OutputPort()
        {
            uint port_00_value = 0;
            Dmc3000.nmc_read_outport((ushort)this.CardNum, (ushort)NodeNum, 0, ref port_00_value);
            return port_00_value;
        }

        public void Set_OutputBit(int bitnum , bool statu)
        {
            uint port_value = this.Get_OutputPort();
            port_value = myConvert.UInt32SetBit(statu, port_value, bitnum);
            this.Set_OutputPort(port_value);
        }
        public void Set_OutputPort(uint port_value)
        {
            Dmc3000.nmc_write_outport((ushort)this.CardNum, (ushort)NodeNum, 0, port_value);
        }
        #endregion
        #region Event
        private void plC_Button_Open_btnClick(object sender, EventArgs e)
        {
            this.IsOpen = !this.IsOpen;
            this.Set_UI_Enable(!this.IsOpen);
        }
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.IsOpen)
            {
                this.SaveProperties();
            }
            this.IsOpen = false;
        }
        #endregion
    }
}
