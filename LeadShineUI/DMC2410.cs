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

namespace LeadShineUI
{
    [System.Drawing.ToolboxBitmap(typeof(DMC2410), "PCI.bmp")]
    public partial class DMC2410 : UserControl
    {
        #region 自訂屬性
        private string _設備名稱 = "DMC2410-001";
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
        #endregion
        private int Card_count = 0;
        private bool First_Init = true;
        private bool IsOpen = false;
        private MyThread MyThread_Program;
        private MyThread MyThread_RefreshUI;
        private Form Active_Form;
        private String StreamName;
        private LowerMachine lowerMachine;
        private MyConvert myConvert = new MyConvert();
        private TabControl tabControl = new TabControl();
        private TabPage[] tabPage;
        private LeadShineUI.DMC2410_Basic[] DMC2410_Basic;
        private List<bool> Axis_PC_Enable = new List<bool>();

        int[] List_active_speed_buf = new int[40];
        int[] List_target_position_buf = new int[40];
        List<int> List_Active_Code = new List<int>();
        int[] List_Active_Code_buf = new int[40];

        public DMC2410()
        {
            InitializeComponent();
        }

        private void SetLowerMachine(LowerMachine lowerMachine)
        {
            this.lowerMachine = lowerMachine;
        }
        private void SetForm(Form form)
        {
            Active_Form = form;
        }
        public void Run(Form form, LowerMachine lowerMachine)
        {
            SetForm(form);
            SetLowerMachine(lowerMachine);
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
        private void Init()
        {
            this.BoardOpen();
            this.LoadProperties();
            Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
        }
        private void TabPage_Init(int Card_count)
        {
            this.Invoke(new Action(delegate
            {
                tabPage = new TabPage[Card_count];
                DMC2410_Basic = new DMC2410_Basic[Card_count];
                for (int i = 0; i < Card_count; i++)
                {
                    DMC2410_Basic[i] = new DMC2410_Basic();
                    DMC2410_Basic[i].Dock = System.Windows.Forms.DockStyle.Fill;
                    DMC2410_Basic[i].Location = new System.Drawing.Point(3, 3);
                    DMC2410_Basic[i].Name = "DMC2410_Basic" + i.ToString();
                    DMC2410_Basic[i].Size = new System.Drawing.Size(569, 429);
                    DMC2410_Basic[i].TabIndex = 0;
                    DMC2410_Basic[i].SetPLC(this.lowerMachine);
                    tabPage[i] = new TabPage();
                    tabPage[i].Controls.Add(DMC2410_Basic[i]);
                    tabPage[i].Location = new System.Drawing.Point(4, 22);
                    tabPage[i].Name = "tabPage_card_" + i.ToString();
                    tabPage[i].Padding = new System.Windows.Forms.Padding(3);
                    tabPage[i].Size = new System.Drawing.Size(192, 74);
                    tabPage[i].TabIndex = i;
                    tabPage[i].Text = "CARD-" + i.ToString();
                    tabPage[i].UseVisualStyleBackColor = true;
                    for (int k = 0; k < 4; k++)
                    {
                        this.Axis_PC_Enable.Add(false);
                    }
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
                //this.tabControl.Name = "tabControl_C9016";
                this.tabControl.SelectedIndex = 0;
                this.tabControl.Dock = DockStyle.Fill;
                this.panel_TAB.Controls.Add(this.tabControl);
                tabControl.ResumeLayout(false);

            }));
        }
        private void Program()
        {
            if (IsOpen && lowerMachine != null)
            {
                sub_GetInput();
                sub_WriteToPLC();

                sub_ReadFromPLC();
                sub_Run_Axis();
            }

        }

        #region GetInput
        void sub_GetInput()
        {
            int index;
            ushort io_status = 0;
            uint rsts = 0;
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 28; k++)
                {
                    DMC2410_Basic[i].Set_Input(k, this.Get_Input(i, k));
                }
            }
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    index = (i * 4) + k;
                    Get_Axis_Input(index, ref io_status, ref rsts);


                    DMC2410_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC2410_Basic.enum_Axis_Input.ALM, myConvert.UInt16GetBit(io_status, (int)enum_Axis_io_status.ALM));
                    DMC2410_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC2410_Basic.enum_Axis_Input.N_EL, myConvert.UInt16GetBit(io_status, (int)enum_Axis_io_status.NEL));
                    DMC2410_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC2410_Basic.enum_Axis_Input.ORG, myConvert.UInt16GetBit(io_status, (int)enum_Axis_io_status.ORG));
                    DMC2410_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC2410_Basic.enum_Axis_Input.P_EL, myConvert.UInt16GetBit(io_status, (int)enum_Axis_io_status.PEL));


                    DMC2410_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC2410_Basic.enum_Axis_Input.EMG, myConvert.UInt32GetBit(rsts, (int)enum_Axis_rsts.EMG));
                    DMC2410_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC2410_Basic.enum_Axis_Input.EZ, myConvert.UInt32GetBit(rsts, (int)enum_Axis_rsts.EZ));
                    DMC2410_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC2410_Basic.enum_Axis_Input.P_DR, myConvert.UInt32GetBit(rsts, (int)enum_Axis_rsts.PA));
                    DMC2410_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC2410_Basic.enum_Axis_Input.N_DR, myConvert.UInt32GetBit(rsts, (int)enum_Axis_rsts.PB));


                    DMC2410_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC2410_Basic.enum_Axis_Input.RDY, this.Get_RDY_PIN(index));
                }
            }
        }
        #endregion
        #region WriteToPLC
        void sub_WriteToPLC()
        {
            string adress;
            bool flag;
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 28; k++)
                {
                    flag = DMC2410_Basic[i].Get_Input(k);
                    adress = DMC2410_Basic[i].Get_Input_Adress(k);
                    if (adress != string.Empty) lowerMachine.properties.device_system.Set_DeviceFast_Ex(adress, flag);
                }
            }
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    for (int m = 0; m < 9; m++)
                    {
                        flag = DMC2410_Basic[i].Get_Axis_Input(k, m);
                        adress = DMC2410_Basic[i].Get_Axis_Input_Adress(k, m);
                        if (adress != string.Empty) lowerMachine.properties.device_system.Set_DeviceFast_Ex(adress, flag);
                    }
                }
            }
        }
        #endregion

        #region ReadFromPLC
        void sub_ReadFromPLC()
        {
            string adress;
            bool flag;
            object obj;
            int temp0;
            int temp1;
            int axis_num;
            int index;
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 24; k++)
                {
                    adress = DMC2410_Basic[i].Get_Output_Adress(k);
                    if (adress != string.Empty)
                    {
                        flag = lowerMachine.properties.device_system.Get_DeviceFast_Ex(adress);
                        DMC2410_Basic[i].Set_Output(k, flag);
                        this.Set_Output(i, k, flag);
                        
                    }
                }
            }
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    axis_num = i * 4 + k;
                    temp0 = DMC2410_Basic[i].Get_Axis_Index(k);
                    for (int m = 1; m < 10; m++)
                    {
                        //adress = "D" + ((8340 + temp0 * 10) + m).ToString();
                        temp1 = lowerMachine.properties.Device.Get_DataFast_Ex("D", ((8340 + temp0 * 10) + m));
                        DMC2410_Basic[i].Set_Axis_Parameter(k, m, temp1);
                       
                    }
                }
            }

            List_Active_Code.Clear();
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    axis_num = i * 4 + k;
                    temp0 = DMC2410_Basic[i].Get_Axis_Index(k);
                    //adress = "D" + ((8340 + temp0 * 10) + 6).ToString();
                    temp1 = lowerMachine.properties.Device.Get_DataFast_Ex("D", ((8340 + temp0 * 10) + 6));
                    List_Active_Code.Add(temp1);
                
                }
            }

            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC2410_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10)).ToString();
                    axis_num = i * 4 + k;
                    flag = this.Get_Axis_Ready(axis_num);
                    DMC2410_Basic[i].Set_Axis_State(k, LeadShineUI.DMC2410_Basic.enum_Axis_State.Axis_Busy, flag);
                    lowerMachine.properties.Device.Set_DeviceFast_Ex("M", ((8340 + temp0 * 10)), flag);
                }
            }
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC2410_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10 + 1)).ToString();
                    axis_num = i * 4 + k;
                    flag = lowerMachine.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 1)));
                    DMC2410_Basic[i].Set_Axis_State(k, LeadShineUI.DMC2410_Basic.enum_Axis_State.位置更改致能, flag);
                }
            }

            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC2410_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10 + 1)).ToString();
                    axis_num = i * 4 + k;
                    flag = lowerMachine.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 2)));
                    DMC2410_Basic[i].Set_Axis_State(k, LeadShineUI.DMC2410_Basic.enum_Axis_State.JOG加減速致能, flag);
                }
            }

            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC2410_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10 + 1)).ToString();
                    axis_num = i * 4 + k;
                    flag = lowerMachine.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 3)));
                    DMC2410_Basic[i].Set_Axis_State(k, LeadShineUI.DMC2410_Basic.enum_Axis_State._1P_2P_模式, flag);
                }
            }

            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC2410_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10 + 1)).ToString();
                    axis_num = i * 4 + k;
                    flag = lowerMachine.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 4)));
                    DMC2410_Basic[i].Set_Axis_State(k, LeadShineUI.DMC2410_Basic.enum_Axis_State.SD減速致能, flag);
                }
            }
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC2410_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10 + 5)).ToString();
                    axis_num = i * 4 + k;
                    flag = lowerMachine.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 5)));
                    DMC2410_Basic[i].Set_Axis_State(k, LeadShineUI.DMC2410_Basic.enum_Axis_State.T_S_速度模式, flag);
                }
            }

            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC2410_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10 + 1)).ToString();
                    axis_num = i * 4 + k;
                    flag = lowerMachine.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 6)));
                    DMC2410_Basic[i].Set_Axis_State(k, LeadShineUI.DMC2410_Basic.enum_Axis_State.CMP致能, flag);
                }
            }

            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC2410_Basic[i].Get_Axis_Index(k);
                    //adress = "D" + ((8340 + temp0 * 10)).ToString();
                    axis_num = i * 4 + k;
                    if (!DMC2410_Basic[i].Get_Axis_State(k, LeadShineUI.DMC2410_Basic.enum_Axis_State.位置更改致能))
                    {
                        temp1 = this.Get_Command_Pos(axis_num);
                        DMC2410_Basic[i].Set_Axis_Parameter(k, LeadShineUI.DMC2410_Basic.enum_Axis_Parameter.現在位置, temp1);
                        lowerMachine.properties.Device.Set_DataFast_Ex("D", ((8340 + temp0 * 10)), temp1);
                    }
                    else
                    {
                        this.Set_Command_Pos(axis_num, lowerMachine.properties.Device.Get_DataFast_Ex("D", ((8340 + temp0 * 10))));

                    }
                }
            }

     
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC2410_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10 + 3)).ToString();
                    axis_num = i * 4 + k;
                    flag = lowerMachine.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 3)));
                    if (DMC2410_Basic[i].Get_Axis_State(k, LeadShineUI.DMC2410_Basic.enum_Axis_State._1P_2P_模式) != flag)
                    {
                        if (!flag)
                        {
                            this.Set_PLS_Mode(axis_num, enum_Axis_PLSMode.pulse_dir);
                        }
                        else
                        {
                            this.Set_PLS_Mode(axis_num, enum_Axis_PLSMode.cw_ccw);
                        }
                        DMC2410_Basic[i].Set_Axis_State(k, LeadShineUI.DMC2410_Basic.enum_Axis_State._1P_2P_模式, flag);
                    }
                }
            }


        }
        #endregion
        #region Run_Axis
        void sub_Run_Axis()
        {
            if (IsOpen && lowerMachine != null)
            {
                int axis_num;
                int basic_speed;
                int active_speed;
                int target_position;
                double Tacc;
                double Tdec;
                double Tsacc;
                bool S_Mode;
                bool paulse = false;
                for (int i = 0; i < Card_count; i++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        paulse = false;
                        axis_num = i * 4 + k;
                        if (!Axis_PC_Enable[axis_num])
                        {
                            if (List_Active_Code_buf[axis_num] != List_Active_Code[axis_num])
                            {
                                List_Active_Code_buf[axis_num] = List_Active_Code[axis_num];
                                paulse = true;
                            }

                            active_speed = DMC2410_Basic[i].Get_Axis_Parameter(k, LeadShineUI.DMC2410_Basic.enum_Axis_Parameter.運轉速度);
                            basic_speed = DMC2410_Basic[i].Get_Axis_Parameter(k, LeadShineUI.DMC2410_Basic.enum_Axis_Parameter.基底速度);
                            Tacc = DMC2410_Basic[i].Get_Axis_Parameter(k, LeadShineUI.DMC2410_Basic.enum_Axis_Parameter.加速度);
                            Tdec = DMC2410_Basic[i].Get_Axis_Parameter(k, LeadShineUI.DMC2410_Basic.enum_Axis_Parameter.減速度);
                            Tsacc = DMC2410_Basic[i].Get_Axis_Parameter(k, LeadShineUI.DMC2410_Basic.enum_Axis_Parameter.S段時間);
                            target_position = DMC2410_Basic[i].Get_Axis_Parameter(k, LeadShineUI.DMC2410_Basic.enum_Axis_Parameter.運轉目標位置);
                            S_Mode = DMC2410_Basic[i].Get_Axis_State(k, LeadShineUI.DMC2410_Basic.enum_Axis_State.T_S_速度模式);
                            if (List_Active_Code[axis_num] == 0)// 停止
                            {
                                if (paulse)
                                {
                                    this.AxisStopEmg(axis_num);
                                }
                            }
                            else if (List_Active_Code[axis_num] == 1)// DRVA
                            {
                                if (paulse)
                                {
                                    if (!S_Mode) this.DRVA(axis_num, target_position, active_speed, basic_speed, Tacc, Tdec);
                                    else this.DRVA(axis_num, target_position, active_speed, basic_speed, Tacc, Tdec, Tsacc);
                                    List_target_position_buf[axis_num] = target_position;
                                }
                                if(List_target_position_buf[axis_num] != target_position)
                                {
                                    this.Reset_Target_Position(axis_num, target_position);
                                    List_target_position_buf[axis_num] = target_position;
                                }
                            }
                            else if (List_Active_Code[axis_num] == 2)// DRVI
                            {
                                if (paulse)
                                {
                                    if (!S_Mode) this.DRVI(axis_num, target_position, active_speed, basic_speed, Tacc, Tdec);
                                    else this.DRVI(axis_num, target_position, active_speed, basic_speed, Tacc, Tdec, Tsacc);
                                }
                            }
                            else if (List_Active_Code[axis_num] == 3)// PLSV
                            {
                                if (paulse)
                                {
                                    if (List_active_speed_buf[axis_num] != active_speed)
                                    {
                                        if ((List_active_speed_buf[axis_num] > 0 && active_speed > 0) || (List_active_speed_buf[axis_num] < 0 && active_speed < 0))
                                        {

                                        }
                                        else this.AxisStopEmg(axis_num);
                                        //Dmc2410.d2410_change_speed((ushort)axis_num, active_speed);
                                        List_active_speed_buf[axis_num] = active_speed;
                                    }
                                    this.PLSV(axis_num, active_speed, basic_speed, 1, 1);
                                }
                            }
                            else if (List_Active_Code[axis_num] == 4)// PLSV(Tacc)
                            {
                                if (paulse)
                                {
                                    if (List_active_speed_buf[axis_num] != active_speed)
                                    {
                                        if ((List_active_speed_buf[axis_num] > 0 && active_speed > 0) || (List_active_speed_buf[axis_num] < 0 && active_speed < 0))
                                        {

                                        }
                                        else this.AxisStopEmg(axis_num);
                                        //Dmc2410.d2410_change_speed((ushort)axis_num, active_speed);
                                        List_active_speed_buf[axis_num] = active_speed;
                                    }
                                    this.PLSV(axis_num, active_speed, basic_speed, Tacc, Tdec);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
        private void RefreshUI()
        {
            if (lowerMachine != null)
            {
                plC_Button_Open.SetValue(IsOpen);
                plC_Button_Open.Run(lowerMachine);
            }
            if (IsOpen && lowerMachine != null)
            {
                for (int i = 0; i < Card_count; i++)
                {
                    DMC2410_Basic[i].RefreshUI();
                }
                MyThread_Program.GetCycleTime(100, label_CycleTime);
            }
        }

        private void plC_Button_Open_btnClick(object sender, EventArgs e)
        {
            if (!IsOpen) BoardOpen();
            else BoardClose();
        }
        private void BoardOpen()
        {
            this.Card_count = Dmc2410.d2410_board_init();
            if (Card_count > 0)
            {
                if (First_Init)
                {
                    this.TabPage_Init(Card_count);
                    this.TabControl_Init();
                    for (int i = 0; i < Card_count; i++)
                    {
                        DMC2410_Basic[i].Init();
                    }
                }
                if (!First_Init) this.SaveProperties();
                First_Init = false;
                this.IsOpen = true;
                for (int i = 0; i < Card_count; i++)
                {
                    DMC2410_Basic[i].Set_UI_Enable(false);
                }

            }
        }
        private void BoardClose()
        {
            Dmc2410.d2410_board_close();
            this.IsOpen = false;
            for (int i = 0; i < Card_count; i++)
            {
                DMC2410_Basic[i].Set_UI_Enable(true);
            }
        }

        private List<DMC2410_Basic.SaveClass> List_SaveClass = new List<DMC2410_Basic.SaveClass>();
        public void SaveProperties()
        {
            this.List_SaveClass.Clear();
            this.StreamName = @".\\DMC2410\\" + _設備名稱 + ".pro";
            for (int i = 0; i < Card_count; i++)
            {
                this.List_SaveClass.Add(DMC2410_Basic[i].GetSaveObject());
            }
            Basic.FileIO.SaveProperties(this.List_SaveClass, this.StreamName);
        }
        public void LoadProperties()
        {
            object temp = new object();
            this.List_SaveClass.Clear();
            this.StreamName = @".\\DMC2410\\" + _設備名稱 + ".pro";
            Basic.FileIO.LoadProperties(ref temp, StreamName);
            if (temp is List<DMC2410_Basic.SaveClass>)
            {
                this.List_SaveClass = (List<DMC2410_Basic.SaveClass>)temp;
            }
            for (int i = 0; i < Card_count; i++)
            {
                if (i < this.List_SaveClass.Count)
                {
                    DMC2410_Basic[i].LoadObject(this.List_SaveClass[i]);
                }
            }
        }
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsOpen)
            {
                BoardClose();
                this.SaveProperties();
            }
        }

        #region Fuinction
        public enum enum_Axis_SpeedMode : int
        {
            T_Mode = 0, S_Mode = 1
        }
        public enum enum_Axis_PLSMode : int
        {
            pulse_dir = 0, cw_ccw = 1
        }
        public enum enum_ALM_PIN_Mode : ushort
        {
            HIGH_ACTIVE = 1, LOW_ACTIVE = 0
        }
        public enum enum_EZ_PIN_Mode : ushort
        {
            HIGH_ACTIVE = 0, LOW_ACTIVE = 1
        }

        public enum enum_EL_PIN_Mode : ushort
        {
            HIGH_ACTIVE = 0, LOW_ACTIVE = 1
        }
        public enum enum_EL_PIN_Action : ushort
        {
            EMG_Stop = 0, TDec_Stop = 1
        }

        public enum enum_LTC_PIN_Mode : ushort
        {
            HIGH_ACTIVE = 1, LOW_ACTIVE = 0
        }

        public enum enum_EMG_PIN_Mode : ushort
        {
            HIGH_ACTIVE = 1, LOW_ACTIVE = 0
        }

        public enum enum_Counter_Mode : ushort
        {
            PLS_DIR = 0, _1xAB = 1, _2xAB = 2, _4xAB = 3,
        }

        public enum enum_Axis_io_status : int
        {
            ALM = 11, PEL = 12, NEL = 13, ORG = 14
        }
        public enum enum_Axis_rsts : int
        {
            EMG = 7, EZ = 9, PA = 11, PB = 12
        }

        public enum enum_Normal_CMP_Source : ushort
        {
            Command =0,Encoder = 1
        }
        public enum enum_Normal_CMP_Dir : ushort
        {
            eqal_greater = 0, eqal_less = 1
        }
        public enum enum_Normal_CMP_Action : ushort
        {
            LOW = 1, HIGH = 2, TOGGLE = 3, _100us = 5, _1ms = 6, _10ms = 7, _100ms = 8
        }
        public enum enum_HighSpeed_CMP_Mode : ushort
        {
            HIGH_ACTIVE = 1, LOW_ACTIVE = 0
        }

        public enum enum_Arc_DIR : ushort
        {
            順時針 = 0, 逆時針 = 1
        }
        public void PLSV(int axis_num, int active_speed, int basic_speed, double Tacc, double Tdec)
        {
            this.PLSV(axis_num, active_speed, basic_speed, Tacc, Tdec, 0, enum_Axis_SpeedMode.T_Mode);
        }
        public void PLSV(int axis_num, int active_speed, int basic_speed, double Tacc)
        {
            this.PLSV(axis_num, active_speed, basic_speed, Tacc, Tacc, 0, enum_Axis_SpeedMode.T_Mode);
        }
        public void PLSV(int axis_num, int active_speed, int basic_speed, double Tacc, double Tdec, double Tsdec)
        {
            this.PLSV(axis_num, active_speed, basic_speed, Tacc, Tdec, Tsdec, enum_Axis_SpeedMode.S_Mode);
        }
        private void PLSV(int axis_num, int active_speed, int basic_speed, double Tacc, double Tdec, double Tsdec, enum_Axis_SpeedMode Axis_SpeedMode)
        {
            if (Axis_SpeedMode == DMC2410.enum_Axis_SpeedMode.T_Mode)
            {
                ushort DIR = 1;
                if (active_speed < 0)
                {
                    active_speed *= -1;
                    DIR = 0;
                }
                Dmc2410.d2410_set_profile((ushort)axis_num, (int)basic_speed, (int)active_speed, Tacc / 1000D, Tdec / 1000D);
                Dmc2410.d2410_t_vmove((ushort)axis_num, DIR);
            }
            else if (Axis_SpeedMode == DMC2410.enum_Axis_SpeedMode.S_Mode)
            {
                ushort DIR = 1;
                if (active_speed < 0)
                {
                    active_speed *= -1;
                    DIR = 0;
                }
                Dmc2410.d2410_set_st_profile((ushort)axis_num, (int)basic_speed, (int)active_speed, Tacc / 1000D, Tdec / 1000D, Tsdec / 1000D, 0);
                Dmc2410.d2410_s_vmove((ushort)axis_num, DIR);
            }
        }

        public void DRVA(int axis_num, int target_position, int active_speed, int basic_speed, double Tacc, double Tdec)
        {
            this.DRVA(axis_num, target_position, active_speed, basic_speed, Tacc, Tdec, 0, enum_Axis_SpeedMode.T_Mode);
        }
        public void DRVA(int axis_num, int target_position, int active_speed, int basic_speed, double Tacc)
        {
            this.DRVA(axis_num, target_position, active_speed, basic_speed, Tacc, Tacc, 0, enum_Axis_SpeedMode.T_Mode);
        }
        public void DRVA(int axis_num, int target_position, int active_speed, int basic_speed, double Tacc, double Tdec, double Tsdec)
        {
            this.DRVA(axis_num, target_position, active_speed, basic_speed, Tacc, Tacc, Tsdec, enum_Axis_SpeedMode.S_Mode);
        }
        private void DRVA(int axis_num, int target_position, int active_speed, int basic_speed, double Tacc, double Tdec, double Tsdec, enum_Axis_SpeedMode Axis_SpeedMode)
        {
            if (Axis_SpeedMode == DMC2410.enum_Axis_SpeedMode.T_Mode)
            {
                Dmc2410.d2410_set_profile((ushort)axis_num, (int)basic_speed, (int)active_speed, Tacc / 1000D, Tdec / 1000D);
                Dmc2410.d2410_ex_t_pmove((ushort)axis_num, target_position, 1);

            }
            else if (Axis_SpeedMode == DMC2410.enum_Axis_SpeedMode.S_Mode)
            {
                Dmc2410.d2410_set_st_profile((ushort)axis_num, (int)basic_speed, (int)active_speed, Tacc / 1000D, Tdec / 1000D, Tsdec / 1000D, 0);
                Dmc2410.d2410_ex_s_pmove((ushort)axis_num, target_position, 1);
            }
        }

        public void DRVI(int axis_num, int target_position, int active_speed, int basic_speed, double Tacc, double Tdec)
        {
            this.DRVI(axis_num, target_position, active_speed, basic_speed, Tacc, Tdec, 0, enum_Axis_SpeedMode.T_Mode);
        }
        public void DRVI(int axis_num, int target_position, int active_speed, int basic_speed, double Tacc)
        {
            this.DRVI(axis_num, target_position, active_speed, basic_speed, Tacc, Tacc, 0, enum_Axis_SpeedMode.T_Mode);
        }
        public void DRVI(int axis_num, int target_position, int active_speed, int basic_speed, double Tacc, double Tdec, double Tsdec)
        {
            this.DRVI(axis_num, target_position, active_speed, basic_speed, Tacc, Tacc, Tsdec, enum_Axis_SpeedMode.S_Mode);
        }
        private void DRVI(int axis_num, int target_position, int active_speed, int basic_speed, double Tacc, double Tdec, double Tsdec, enum_Axis_SpeedMode Axis_SpeedMode)
        {
            if (Axis_SpeedMode == DMC2410.enum_Axis_SpeedMode.T_Mode)
            {
                Dmc2410.d2410_set_profile((ushort)axis_num, (int)basic_speed, (int)active_speed, Tacc / 1000D, Tdec / 1000D);
                Dmc2410.d2410_ex_t_pmove((ushort)axis_num, target_position, 0);

            }
            else if (Axis_SpeedMode == DMC2410.enum_Axis_SpeedMode.S_Mode)
            {
                Dmc2410.d2410_set_st_profile((ushort)axis_num, (int)basic_speed, (int)active_speed, Tacc / 1000D, Tdec / 1000D, Tsdec / 1000D, 0);
                Dmc2410.d2410_ex_s_pmove((ushort)axis_num, target_position, 0);
            }
        }

        public void DRVA_Liner(int[] axis_num, int[] pos, int active_speed, int basic_speed, double Tacc, double Tdec)
        {     
            if(axis_num.Length == pos.Length)
            {
                ushort[] ushort_axis_num = new ushort[axis_num.Length];
                Dmc2410.d2410_set_vector_profile(basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D);
                for (int i = 0; i < ushort_axis_num.Length; i++)
                {
                    ushort_axis_num[i] = (ushort)axis_num[i];
                }
                if (pos.Length == 2)
                {
                    Dmc2410.d2410_t_line2(ushort_axis_num[0], pos[0], ushort_axis_num[1], pos[1], 1);
                }
                else if (pos.Length == 3)
                {
                    Dmc2410.d2410_t_line3(ushort_axis_num, pos[0], pos[1], pos[2], 1);
                }
                else if (pos.Length == 4)
                {
                    //Dmc2410.d2410_t_line4(, pos[0], pos[1], pos[2], pos[3], 1);
                }
            }
        }
        public void DRVA_Liner(int card_num, int pos1, int pos2, int pos3, int pos4, int active_speed, int basic_speed, double Tacc, double Tdec)
        {
            Dmc2410.d2410_set_vector_profile(basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D);
            Dmc2410.d2410_t_line4((ushort)card_num, pos1, pos2, pos3, pos4, 1);
        }

        public void DRVI_Liner(int[] axis_num, int[] pos, int active_speed, int basic_speed, double Tacc, double Tdec)
        {
            if (axis_num.Length == pos.Length)
            {
                ushort[] ushort_axis_num = new ushort[axis_num.Length];
                Dmc2410.d2410_set_vector_profile(basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D);
                for (int i = 0; i < ushort_axis_num.Length; i++)
                {
                    ushort_axis_num[i] = (ushort)axis_num[i];
                }
                if (pos.Length == 2)
                {
                    Dmc2410.d2410_t_line2(ushort_axis_num[0], pos[0], ushort_axis_num[1], pos[1], 1);
                }
                else if (pos.Length == 3)
                {
                    Dmc2410.d2410_t_line3(ushort_axis_num, pos[0], pos[1], pos[2], 1);
                }
                else if (pos.Length == 4)
                {
                    //Dmc2410.d2410_t_line4(, pos[0], pos[1], pos[2], pos[3], 1);
                }
            }
        }
        public void DRVI_Liner(int card_num, int pos1, int pos2, int pos3, int pos4, int active_speed, int basic_speed, double Tacc, double Tdec)
        {
            Dmc2410.d2410_set_vector_profile(basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D);
            Dmc2410.d2410_t_line4((ushort)card_num, pos1, pos2, pos3, pos4, 1);
        }

        public void DRVA_Arc(int[] axis_num, int[] pos, int[] center, int active_speed, int basic_speed, double Tacc, double Tdec, enum_Arc_DIR enum_Arc_DIR)
        {
            ushort[] ushort_axis_num = new ushort[axis_num.Length];
            Dmc2410.d2410_set_vector_profile(basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D);
            for (int i = 0; i < ushort_axis_num.Length; i++)
            {
                ushort_axis_num[i] = (ushort)axis_num[i];
            }
            Dmc2410.d2410_arc_move(ushort_axis_num, pos, center, (ushort)enum_Arc_DIR);
        }
        public void DRVI_Arc(int[] axis_num, int[] pos, int[] center, int active_speed, int basic_speed, double Tacc, double Tdec, enum_Arc_DIR enum_Arc_DIR)
        {
            ushort[] ushort_axis_num = new ushort[axis_num.Length];
            Dmc2410.d2410_set_vector_profile(basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D);
            for (int i = 0; i < ushort_axis_num.Length; i++)
            {
                ushort_axis_num[i] = (ushort)axis_num[i];
            }
            Dmc2410.d2410_rel_arc_move(ushort_axis_num, pos, center, (ushort)enum_Arc_DIR);
        }

        public void Set_Command_Pos(int axis_num , int pos)
        {
            Dmc2410.d2410_set_position((ushort)axis_num, pos);
        }
        public int Get_Command_Pos(int axis_num)
        {
            return Dmc2410.d2410_get_position((ushort)axis_num);
        }
        public bool Get_Axis_Ready(int axis_num)
        {
            return (Dmc2410.d2410_check_done((ushort)axis_num) == 1);
        }

        public void Reset_Target_Position(int axis_num, int pos)
        {
            Dmc2410.d2410_reset_target_position((ushort)axis_num, pos);
        }

        public void AxisStop(int axis_num)
        {
            this.AxisStop(axis_num, 0);
        }
        public void AxisStop(int axis_num, double Tdec)
        {
            Dmc2410.d2410_decel_stop((ushort)axis_num, Tdec / 1000D);
        }
        public void AxisStopEmg(int axis_num)
        {
            Dmc2410.d2410_imd_stop((ushort)axis_num);
        }

        public void Set_PLS_Mode(int axis_num, enum_Axis_PLSMode Axis_PLSMode)
        {
            if (Axis_PLSMode == enum_Axis_PLSMode.pulse_dir)
            {
                Dmc2410.d2410_set_pulse_outmode((ushort)axis_num, 1);
            }
            else if (Axis_PLSMode == enum_Axis_PLSMode.cw_ccw)
            {
                Dmc2410.d2410_set_pulse_outmode((ushort)axis_num, 5);
            }
        }
        public void Set_SrvOn_PIN(int axis_num,bool statu)
        {
            Dmc2410.d2410_write_SEVON_PIN((ushort)axis_num, statu ? (ushort)1 : (ushort)0);
        }
        public bool Get_SrvOn_PIN(int axis_num)
        {
            return (Dmc2410.d2410_read_SEVON_PIN((ushort)axis_num) == 1);
        }
        public bool Get_RDY_PIN(int axis_num)
        {
            return (Dmc2410.d2410_read_RDY_PIN((ushort)axis_num) == 1);
        }

        public void Set_ALM_PIN_Mode(int axis_num, enum_ALM_PIN_Mode enum_ALM_PIN_Mode)
        {
            this.Set_ALM_PIN_Mode(axis_num, (ushort)enum_ALM_PIN_Mode, 0); 
        }
        public void Set_ALM_PIN_Mode(int axis_num, enum_ALM_PIN_Mode enum_ALM_PIN_Mode, ushort alm_action)
        {
            this.Set_ALM_PIN_Mode(axis_num, (ushort)enum_ALM_PIN_Mode, alm_action);     
        }
        public void Set_ALM_PIN_Mode(int axis_num, ushort alm_logic, ushort alm_action)
        {
            Dmc2410.d2410_config_ALM_PIN((ushort)axis_num, alm_logic, alm_action);
        }

        public void Set_EZ_PIN_Mode(int axis_num, enum_EZ_PIN_Mode enum_EZ_PIN_Mode)
        {
            this.Set_EZ_PIN_Mode(axis_num, (ushort)enum_EZ_PIN_Mode);
        }
        public void Set_EZ_PIN_Mode(int axis_num, ushort alm_logic)
        {
            Dmc2410.d2410_config_EZ_PIN((ushort)axis_num, alm_logic, 0);
        }

        public void Set_EL_PIN_Mode(int axis_num, enum_EL_PIN_Mode enum_EL_PIN_Mode, enum_EL_PIN_Action enum_EL_PIN_Action)
        {
            if(enum_EL_PIN_Mode == DMC2410.enum_EL_PIN_Mode.LOW_ACTIVE && enum_EL_PIN_Action == DMC2410.enum_EL_PIN_Action.EMG_Stop)
            {
                this.Set_EL_PIN_Mode(axis_num, 0);
            }
            else if (enum_EL_PIN_Mode == DMC2410.enum_EL_PIN_Mode.LOW_ACTIVE && enum_EL_PIN_Action == DMC2410.enum_EL_PIN_Action.TDec_Stop)
            {
                this.Set_EL_PIN_Mode(axis_num, 1);
            }
            else if (enum_EL_PIN_Mode == DMC2410.enum_EL_PIN_Mode.HIGH_ACTIVE && enum_EL_PIN_Action == DMC2410.enum_EL_PIN_Action.EMG_Stop)
            {
                this.Set_EL_PIN_Mode(axis_num, 2);
            }
            else if (enum_EL_PIN_Mode == DMC2410.enum_EL_PIN_Mode.HIGH_ACTIVE && enum_EL_PIN_Action == DMC2410.enum_EL_PIN_Action.TDec_Stop)
            {
                this.Set_EL_PIN_Mode(axis_num, 3);
            }
        }
        public void Set_EL_PIN_Mode(int axis_num, ushort alm_logic)
        {
            Dmc2410.d2410_config_EL_MODE((ushort)axis_num, alm_logic);
        }

        public void Set_LTC_PIN_Mode(int axis_num, enum_LTC_PIN_Mode enum_LTC_PIN_Mode)
        {
            this.Set_LTC_PIN_Mode(axis_num, (ushort)enum_LTC_PIN_Mode);
        }
        public void Set_LTC_PIN_Mode(int axis_num, ushort alm_logic)
        {
            Dmc2410.d2410_config_LTC_PIN((ushort)axis_num, alm_logic, 0);
        }

        public void Set_EMG_PIN_Mode(int axis_num, enum_EMG_PIN_Mode enum_EMG_PIN_Mode)
        {
            this.Set_EMG_PIN_Mode(axis_num, (ushort)enum_EMG_PIN_Mode);
        }
        public void Set_EMG_PIN_Mode(int axis_num, ushort alm_logic)
        {
            Dmc2410.d2410_config_EMG_PIN((ushort)axis_num, alm_logic, 0);
        }

        public void Get_Axis_Input(int axis_num ,ref ushort io_status,ref uint rsts)
        {
            io_status = Dmc2410.d2410_axis_io_status((ushort)axis_num);
            rsts = Dmc2410.d2410_get_rsts((ushort)axis_num);
        }
        public bool Get_Axis_Input(int axis_num, enum_Axis_io_status enum_Axis_io_status)
        {
            return myConvert.UInt16GetBit(Dmc2410.d2410_axis_io_status((ushort)axis_num), (int)enum_Axis_io_status);
        }
        public bool Get_Axis_Input(int axis_num, enum_Axis_rsts enum_Axis_rsts)
        {
            return myConvert.UInt32GetBit(Dmc2410.d2410_get_rsts((ushort)axis_num), (int)enum_Axis_rsts);
        }

        public bool Get_Input(int card_num, int bit_num)
        {
            bit_num++;
            return (Dmc2410.d2410_read_inbit((ushort)card_num, (ushort)bit_num) == 0);
        }
        public int Get_Input(int card_num)
        {
            return Dmc2410.d2410_read_inport((ushort)card_num);
        }

        public bool Get_Output(int card_num, int bit_num)
        {
            bit_num++;
            return (Dmc2410.d2410_read_outbit((ushort)card_num, (ushort)bit_num) == 0);
        }
        public int Get_Output(int card_num)
        {
            return Dmc2410.d2410_read_outport((ushort)card_num);
        }

        public void Set_Output(int card_num, int bit_num, bool statu)
        {
            bit_num++;
            Dmc2410.d2410_write_outbit((ushort)card_num, (ushort)bit_num, statu ? (ushort)1 : (ushort)0);
        }
        public void Set_Output(int card_num, uint port_value, bool statu)
        {
            Dmc2410.d2410_write_outport((ushort)card_num, port_value);
        }

        public void Set_Counter_Mode(int axis_num, enum_Counter_Mode enum_Counter_Mode)
        {
            Dmc2410.d2410_counter_config((ushort)axis_num, (ushort)enum_Counter_Mode);
        }
        public int Get_Counter(int axis_num)
        {
            return Dmc2410.d2410_get_encoder((ushort)axis_num);
        }
        public void Set_Counter(int axis_num , uint value)
        {
            Dmc2410.d2410_set_encoder((ushort)axis_num, value);
        }

        public void Set_Normal_CMP_Mode(int card_num, int queue, bool enable, int axis_num, enum_Normal_CMP_Source enum_Normal_CMP_Source)
        {
            Dmc2410.d2410_compare_config_Extern((ushort)card_num, (ushort)queue, enable ? (ushort)1 : (ushort)0, (ushort)axis_num, (ushort)enum_Normal_CMP_Source);
        }
        public void Get_Normal_CMP_Mode(int card_num, int queue,ref bool enable,ref int axis_num,ref enum_Normal_CMP_Source enum_Normal_CMP_Source)
        {
            ushort enable_temp = 0;
            ushort axis_num_temp = 0;
            ushort enum_Normal_CMP_Source_temp = 0;
            Dmc2410.d2410_compare_get_config_Extern((ushort)card_num, (ushort)queue, ref enable_temp, ref axis_num_temp, ref enum_Normal_CMP_Source_temp);

            if (enable_temp == 0) enable = false;
            else if (enable_temp == 1) enable = true;

            axis_num = axis_num_temp;

            if (enum_Normal_CMP_Source_temp == 0) enum_Normal_CMP_Source = DMC2410.enum_Normal_CMP_Source.Command;
            else if (enum_Normal_CMP_Source_temp == 1) enum_Normal_CMP_Source = DMC2410.enum_Normal_CMP_Source.Encoder;

        }
        public void Set_Normal_CMP_Clear(int card_num, int queue)
        {
            Dmc2410.d2410_compare_clear_points_Extern((ushort)card_num, (ushort)queue);
        }
        public void Set_Normal_CMP_add_point(int card_num, int queue, uint pos, enum_Normal_CMP_Dir enum_Normal_CMP_Dir, enum_Normal_CMP_Action enum_Normal_CMP_Action,uint actpara)
        {
            Dmc2410.d2410_compare_add_point_Extern((ushort)card_num, (ushort)queue, pos, (ushort)enum_Normal_CMP_Dir, (ushort)enum_Normal_CMP_Action, actpara);
        }
        public int Get_Normal_CMP_Current_point(int card_num, int queue)
        {
            return Dmc2410.d2410_compare_get_current_point_Extern((ushort)card_num, (ushort)queue);
        }
        public int Get_Normal_CMP_points_runned(int card_num, int queue)
        {
            return Dmc2410.d2410_compare_get_points_runned_Extern((ushort)card_num, (ushort)queue);
        }
        public int Get_Normal_CMP_points_remained(int card_num, int queue)
        {
            return Dmc2410.d2410_compare_get_points_remained_Extern((ushort)card_num, (ushort)queue);
        }
        public void Set_HighSpeed_CMP_PIN_Mode(int axis_num, bool enable, int pos, enum_HighSpeed_CMP_Mode enum_HighSpeed_CMP_Mode)
        {
            Dmc2410.d2410_config_CMP_PIN((ushort)axis_num, enable ? (ushort)1 : (ushort)0, pos, (ushort)enum_HighSpeed_CMP_Mode);
        }
        public void Get_HighSpeed_CMP_PIN_Mode(int axis_num,ref bool enable,ref int pos,ref enum_HighSpeed_CMP_Mode enum_HighSpeed_CMP_Mode)
        {
            ushort enable_temp = 0;
            ushort enum_HighSpeed_CMP_Mode_temp = 0;
            Dmc2410.d2410_get_config_CMP_PIN((ushort)axis_num, ref enable_temp, ref pos,ref enum_HighSpeed_CMP_Mode_temp);
            enable = (enable_temp == 1);
            if(enum_HighSpeed_CMP_Mode_temp == 0)
            {
                enum_HighSpeed_CMP_Mode = DMC2410.enum_HighSpeed_CMP_Mode.LOW_ACTIVE;
            }
            else if(enum_HighSpeed_CMP_Mode_temp == 1)
            {
                enum_HighSpeed_CMP_Mode = DMC2410.enum_HighSpeed_CMP_Mode.HIGH_ACTIVE;
            }
        }
        #endregion
    }
}
