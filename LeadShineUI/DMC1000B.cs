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
    [System.Drawing.ToolboxBitmap(typeof(DMC1000B), "PCI.bmp")]
    [Designer(typeof(ComponentSet.JLabelExDesigner))]  
    public partial class DMC1000B : UserControl
    {
        #region 自訂屬性
        private string _設備名稱 = "DMC1000B-001";
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
        private LowerMachine PLC;
        private MyConvert myConvert = new MyConvert();
        private TabControl tabControl = new TabControl();
        private TabPage[] tabPage;
        private LeadShineUI.DMC1000B_Basic[] DMC1000B_Basic;
        private List<bool> Axis_PC_Enable = new List<bool>();

        public DMC1000B()
        {
            InitializeComponent();
            this.plC_Button_Save.btnClick += PlC_Button_Save_btnClick;
        }

   

        private void SetLowerMachine(LowerMachine PLC)
        {
            this.PLC = PLC;
        }
        private void SetForm(Form form)
        {
            Active_Form = form;
        }
        public void Run(Form form, LowerMachine PLC)
        {
            SetForm(form);
            SetLowerMachine(PLC);
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
            MyThread_RefreshUI.SetSleepTime(30);
            MyThread_RefreshUI.Trigger();
        }
        private void Init()
        {
            this.BoardOpen();          
            Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
        }
        private void TabPage_Init(int Card_count)
        {
            this.Invoke(new Action(delegate
            {
                tabPage = new TabPage[Card_count];
                DMC1000B_Basic = new DMC1000B_Basic[Card_count];
                for (int i = 0; i < Card_count; i++)
                {
                    DMC1000B_Basic[i] = new DMC1000B_Basic();
                    DMC1000B_Basic[i].Dock = System.Windows.Forms.DockStyle.Fill;
                    DMC1000B_Basic[i].Location = new System.Drawing.Point(3, 3);
                    DMC1000B_Basic[i].Name = "DMC1000B_Basic" + i.ToString();
                    DMC1000B_Basic[i].Size = new System.Drawing.Size(569, 429);
                    DMC1000B_Basic[i].TabIndex = 0;
                    DMC1000B_Basic[i].SetPLC(this.PLC);
                    tabPage[i] = new TabPage();
                    tabPage[i].Controls.Add(DMC1000B_Basic[i]);
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
            if (IsOpen && PLC != null)
            {
                sub_GetInput();
                sub_WriteToPLC();

                sub_ReadFromPLC();
                sub_Run_Axis();
            }
         
        }
        private void RefreshUI()
        {
            if (PLC != null)
            {
                plC_Button_Open.SetValue(IsOpen);
                plC_Button_Open.Run(PLC);
            }
            if (IsOpen && PLC != null)
            {
                for (int i = 0; i < Card_count; i++)
                {
                    DMC1000B_Basic[i].RefreshUI();
                }
                MyThread_Program.GetCycleTime(100, label_CycleTime);
            }
        }

        #region GetInput
        void sub_GetInput()
        {
            int index;
            int status;
            for(int i = 0 ; i < Card_count ; i++)
            {
                for(int k = 0 ; k < 32 ; k++)
                {
                    DMC1000B_Basic[i].Set_Input(k, this.GetInput(i, k));
                }
            }
            for (int i = 0; i < Card_count; i++)
            {             
                for (int k = 0; k < 4; k++)
                {
                    index = (i * 4) + k;
                    status = Dmc1000.d1000_get_axis_status(index);
                    DMC1000B_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC1000B_Basic.enum_Axis_Input.N_EL, myConvert.Int32GetBit(status, 0));
                    DMC1000B_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC1000B_Basic.enum_Axis_Input.P_EL, myConvert.Int32GetBit(status, 1));
                    DMC1000B_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC1000B_Basic.enum_Axis_Input.ORG, myConvert.Int32GetBit(status, 2));
                    DMC1000B_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC1000B_Basic.enum_Axis_Input.STA, myConvert.Int32GetBit(status, 3));
                    DMC1000B_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC1000B_Basic.enum_Axis_Input.STP, myConvert.Int32GetBit(status, 4));
                    DMC1000B_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC1000B_Basic.enum_Axis_Input.N_SD, myConvert.Int32GetBit(status, 5));
                    DMC1000B_Basic[i].Set_Axis_Input(k, LeadShineUI.DMC1000B_Basic.enum_Axis_Input.P_SD, myConvert.Int32GetBit(status, 6));
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
                for (int k = 0; k < 32; k++)
                {
                    flag = DMC1000B_Basic[i].Get_Input(k);
                    adress = DMC1000B_Basic[i].Get_Input_Adress(k);
                    if (adress != string.Empty) PLC.properties.device_system.Set_DeviceFast_Ex(adress, flag);
                }
            }
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    for (int m = 0; m < 7; m++)
                    {
                        flag = DMC1000B_Basic[i].Get_Axis_Input(k, m);
                        adress = DMC1000B_Basic[i].Get_Axis_Input_Adress(k, m);
                        if (adress != string.Empty) PLC.properties.device_system.Set_DeviceFast_Ex(adress, flag);
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
                for (int k = 0; k < 27; k++)
                {
                    index = (i * 32) + (k + 1);
                    adress = DMC1000B_Basic[i].Get_Output_Adress(k);
                    if (adress != string.Empty)
                    {
                        flag = PLC.properties.device_system.Get_DeviceFast_Ex(adress);
                        DMC1000B_Basic[i].Set_Output(k, flag);
                        this.SetOutput(index, flag);
                        
                    }
                }
            }
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    axis_num = i * 4 + k;
                    temp0 = DMC1000B_Basic[i].Get_Axis_Index(k);
                    for (int m = 1; m < 10; m++)
                    {
                        adress = "D" + ((8340 + temp0 * 10) + m).ToString();
                        temp1 = PLC.properties.Device.Get_DataFast_Ex("D", ((8340 + temp0 * 10) + m));
                        DMC1000B_Basic[i].Set_Axis_Parameter(k, m, temp1);
                                        
                    }
                }
            }

            List_Active_Code.Clear();
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    axis_num = i * 4 + k;
                    temp0 = DMC1000B_Basic[i].Get_Axis_Index(k);
                    temp1 = PLC.properties.Device.Get_DataFast_Ex("D", ((8340 + temp0 * 10) + 6));
                    List_Active_Code.Add(temp1);                       
                }
            }
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC1000B_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10 + 1)).ToString();
                    axis_num = i * 4 + k;
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 0)));
                    DMC1000B_Basic[i].Set_Axis_State(k, LeadShineUI.DMC1000B_Basic.enum_Axis_State.Axis_Busy, flag);
                }
            }
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC1000B_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10 + 1)).ToString();
                    axis_num = i * 4 + k;
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 1)));
                    DMC1000B_Basic[i].Set_Axis_State(k, LeadShineUI.DMC1000B_Basic.enum_Axis_State.位置更改致能, flag);
                }
            }

            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC1000B_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10 + 1)).ToString();
                    axis_num = i * 4 + k;
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 2)));
                    DMC1000B_Basic[i].Set_Axis_State(k, LeadShineUI.DMC1000B_Basic.enum_Axis_State.JOG加減速致能, flag);
                }
            }

            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC1000B_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10 + 1)).ToString();
                    axis_num = i * 4 + k;
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 3)));
                    DMC1000B_Basic[i].Set_Axis_State(k, LeadShineUI.DMC1000B_Basic.enum_Axis_State._1P_2P_模式, flag);
                }
            }

            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC1000B_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10 + 1)).ToString();
                    axis_num = i * 4 + k;
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 4)));
                    DMC1000B_Basic[i].Set_Axis_State(k, LeadShineUI.DMC1000B_Basic.enum_Axis_State.SD減速致能, flag);
                }
            }
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC1000B_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10 + 5)).ToString();
                    axis_num = i * 4 + k;
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 5)));
                    DMC1000B_Basic[i].Set_Axis_State(k, LeadShineUI.DMC1000B_Basic.enum_Axis_State.T_S_速度模式, flag);
                }
            }


            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC1000B_Basic[i].Get_Axis_Index(k);
                    //adress = "D" + ((8340 + temp0 * 10)).ToString();
                    axis_num = i * 4 + k;
                    if (!DMC1000B_Basic[i].Get_Axis_State(k, LeadShineUI.DMC1000B_Basic.enum_Axis_State.位置更改致能))
                    {
                        temp1 = this.GetAxisCmdPos(axis_num);
                        DMC1000B_Basic[i].Set_Axis_Parameter(k, LeadShineUI.DMC1000B_Basic.enum_Axis_Parameter.現在位置, temp1);
                        PLC.properties.Device.Set_DataFast_Ex("D", ((8340 + temp0 * 10)), temp1);
                    }
                    else
                    {
                        this.SetAxisCmdPos(axis_num, PLC.properties.Device.Get_DataFast_Ex("D", ((8340 + temp0 * 10))));

                    }
                }
            }

            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    temp0 = DMC1000B_Basic[i].Get_Axis_Index(k);
                    //adress = "M" + ((8340 + temp0 * 10 + 3)).ToString();
                    axis_num = i * 4 + k;
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 3)));
                    if (DMC1000B_Basic[i].Get_Axis_State(k, LeadShineUI.DMC1000B_Basic.enum_Axis_State._1P_2P_模式) != flag)
                    {
                        if (!flag)
                        {
                            this.Set_PLS_Mode(axis_num, enum_Axis_PLSMode.pulse_dir);
                        }
                        else
                        {
                            this.Set_PLS_Mode(axis_num, enum_Axis_PLSMode.cw_ccw);
                        }
                        DMC1000B_Basic[i].Set_Axis_State(k, LeadShineUI.DMC1000B_Basic.enum_Axis_State._1P_2P_模式, flag);
                    }
                }
            }
 
        }
        #endregion
        #region Run_Axis
        int[] List_active_speed_buf = new int[40];
        List<int> List_Active_Code = new List<int>();
        int[] List_Active_Code_buf = new int[40];
        void sub_Run_Axis()
        {
            if (IsOpen && PLC != null)
            {
                int axis_num;              
                int basic_speed;
                int active_speed;
                int target_position;
                double Tacc;
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

                            active_speed = DMC1000B_Basic[i].Get_Axis_Parameter(k, LeadShineUI.DMC1000B_Basic.enum_Axis_Parameter.運轉速度);
                            basic_speed = DMC1000B_Basic[i].Get_Axis_Parameter(k, LeadShineUI.DMC1000B_Basic.enum_Axis_Parameter.基底速度);
                            Tacc = DMC1000B_Basic[i].Get_Axis_Parameter(k, LeadShineUI.DMC1000B_Basic.enum_Axis_Parameter.加減速度);
                            target_position = DMC1000B_Basic[i].Get_Axis_Parameter(k, LeadShineUI.DMC1000B_Basic.enum_Axis_Parameter.運轉目標位置);
                            S_Mode = DMC1000B_Basic[i].Get_Axis_State(k, LeadShineUI.DMC1000B_Basic.enum_Axis_State.T_S_速度模式);
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
                                    if (!S_Mode) this.DRVA(axis_num, target_position, active_speed, basic_speed, Tacc, enum_Axis_SpeedMode.T_Mode);
                                    else this.DRVA(axis_num, target_position, active_speed, basic_speed, Tacc, enum_Axis_SpeedMode.S_Mode);
                                }
                            }
                            else if (List_Active_Code[axis_num] == 2)// DRVI
                            {
                                if (paulse)
                                {
                                    if (!S_Mode) this.DRVI(axis_num, target_position, active_speed, basic_speed, Tacc, enum_Axis_SpeedMode.T_Mode);
                                    else this.DRVI(axis_num, target_position, active_speed, basic_speed, Tacc, enum_Axis_SpeedMode.S_Mode);
                                }
                            }
                            else if (List_Active_Code[axis_num] == 3)// PLSV
                            {
                                if (List_active_speed_buf[axis_num] != active_speed)
                                {
                                    this.AxisStopEmg(axis_num);
                                    Dmc1000.d1000_change_speed(axis_num, active_speed);
                                    List_active_speed_buf[axis_num] = active_speed;
                                }
                                this.PLSV(axis_num, active_speed, basic_speed, 0, enum_Axis_SpeedMode.T_Mode);
                            }
                            else if (List_Active_Code[axis_num] == 4)// PLSV(Tacc)
                            {
                                if (List_active_speed_buf[axis_num] != active_speed)
                                {
                                    //this.AxisStopEmg(axis_num);
                                    Dmc1000.d1000_change_speed(axis_num, active_speed);
                                    List_active_speed_buf[axis_num] = active_speed;
                                }
                                this.PLSV(axis_num, active_speed, basic_speed, Tacc, enum_Axis_SpeedMode.T_Mode);
                            }
                        }                                    
                    }
                }
            }
        }
        #endregion

        private void plC_Button_Open_btnClick(object sender, EventArgs e)
        {
           if(!IsOpen) BoardOpen();
           else BoardClose();
        }

        private void BoardOpen()
        {
            this.Card_count = Dmc1000.d1000_board_init();
            if (Card_count > 0)
            {
                if (First_Init)
                {
                    this.TabPage_Init(Card_count);
                    this.TabControl_Init();
                    for (int i = 0; i < Card_count; i++)
                    {
                        DMC1000B_Basic[i].Init();
                    }
                }
                if (!First_Init) this.SaveProperties();
                First_Init = false;
                this.IsOpen = true;
                for (int i = 0; i < Card_count; i++)
                {
                    DMC1000B_Basic[i].Set_UI_Enable(false);
                }

                this.LoadProperties();

                int axis_index = 0;
                int device_index = 0;
                string commemt = "";
                for (int i = 0; i < Card_count; i++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        axis_index = this.DMC1000B_Basic[i].Get_Axis_Index(k);
                        string[] str_enumNames = LeadShineUI.DMC1000B_Basic.enum_Axis_State._1P_2P_模式.GetEnumNames();
                        for (int m = 0; m < str_enumNames.Length; m++)
                        {
                            commemt = "(DMC1000)" + i.ToString("00") + "-" + k.ToString("00") + " " + str_enumNames[m];
                            PLC.properties.Device.Set_Device("M" + ((8340 + axis_index * 10 + m)).ToString(), commemt);
                        }
                        str_enumNames = LeadShineUI.DMC1000B_Basic.enum_Axis_Parameter.加減速度.GetEnumNames();
                        for (int m = 0; m < str_enumNames.Length; m++)
                        {
                            commemt = "(DMC1000)" + i.ToString("00") + "-" + k.ToString("00") + " " + str_enumNames[m];
                            PLC.properties.Device.Set_Device("D" + ((8340 + axis_index * 10 + m)).ToString(), commemt);
                        }
          
                    }
                }
                
            }  
        }
        private void BoardClose()
        {
            Dmc1000.d1000_board_close();
            this.IsOpen = false;
            for (int i = 0; i < Card_count; i++)
            {
                DMC1000B_Basic[i].Set_UI_Enable(true);
            } 
        }

        private List<DMC1000B_Basic.SaveClass> List_SaveClass = new List<DMC1000B_Basic.SaveClass>();
        public void SaveProperties()
        {
            this.List_SaveClass.Clear();
            this.StreamName = @".\\DMC1000B\\" + _設備名稱 +".pro";
            for (int i = 0; i < Card_count; i++)
            {
                this.List_SaveClass.Add(DMC1000B_Basic[i].GetSaveObject());
            }
            Basic.FileIO.SaveProperties(this.List_SaveClass, this.StreamName);
        }
        public void LoadProperties()
        {
            object temp = new object();
            this.List_SaveClass.Clear();
            this.StreamName = @".\\DMC1000B\\" + _設備名稱 + ".pro";
            Basic.FileIO.LoadProperties(ref temp, StreamName);
            if (temp is List<DMC1000B_Basic.SaveClass>)
            {
                this.List_SaveClass = (List<DMC1000B_Basic.SaveClass>)temp;
            }
            for (int i = 0; i < Card_count; i++)
            {
                if (i < this.List_SaveClass.Count)
                {
                    DMC1000B_Basic[i].LoadObject(this.List_SaveClass[i]);
                }        
            }
        }

        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsOpen)
            {
                this.BoardClose();
            }
        }
        private void PlC_Button_Save_btnClick(object sender, EventArgs e)
        {
            this.SaveProperties();
            MyMessageBox.ShowDialog("存檔完成!");
        }
        #region Fuinction
        public enum enum_Axis_SpeedMode : int
        {
            T_Mode = 0, S_Mode = 1
        }
        public enum enum_Axis_PLSMode
        {
            pulse_dir = 0 , cw_ccw = 1
        }
        public void PLSV(int axis_num, int active_speed, int basic_speed, double Tacc, enum_Axis_SpeedMode Axis_SpeedMode)
        {
            if (Axis_SpeedMode == DMC1000B.enum_Axis_SpeedMode.T_Mode) Dmc1000.d1000_start_tv_move(axis_num, basic_speed, active_speed, Tacc / 1000D);
            else if (Axis_SpeedMode == DMC1000B.enum_Axis_SpeedMode.S_Mode) Dmc1000.d1000_start_sv_move(axis_num, basic_speed, active_speed, Tacc / 1000D);
        }
        public void DRVA(int axis_num, int target_position, int active_speed, int basic_speed, double Tacc, enum_Axis_SpeedMode Axis_SpeedMode)
        {
            if (Axis_SpeedMode == DMC1000B.enum_Axis_SpeedMode.T_Mode) Dmc1000.d1000_start_ta_move(axis_num, target_position, basic_speed, active_speed, Tacc / 1000D);
            else if (Axis_SpeedMode == DMC1000B.enum_Axis_SpeedMode.S_Mode) Dmc1000.d1000_start_sa_move(axis_num, target_position, basic_speed, active_speed, Tacc / 1000D);
        }
        public void DRVI(int axis_num, int target_position, int active_speed, int basic_speed, double Tacc, enum_Axis_SpeedMode Axis_SpeedMode)
        {
            if (Axis_SpeedMode == DMC1000B.enum_Axis_SpeedMode.T_Mode) Dmc1000.d1000_start_t_move(axis_num, target_position, basic_speed, active_speed, Tacc / 1000D);
            else if (Axis_SpeedMode == DMC1000B.enum_Axis_SpeedMode.S_Mode) Dmc1000.d1000_start_s_move(axis_num, target_position, basic_speed, active_speed, Tacc / 1000D);
        }
        public void DRVA_Liner(int TotalAxis, ushort[] AxisArray, int[] PosArray, int active_speed, int basic_speed, double Tacc)
        {
            Dmc1000.d1000_start_ta_line(TotalAxis, AxisArray, PosArray, basic_speed, active_speed, Tacc / 1000D);
        }
        public void DRVI_Liner(int TotalAxis, ushort[] AxisArray, int[] PosArray, int active_speed, int basic_speed, double Tacc)
        {
            Dmc1000.d1000_start_t_line(TotalAxis, AxisArray, PosArray, basic_speed, active_speed, Tacc / 1000D);
        }

        public void AxisStop(int axis_num)
        {
            Dmc1000.d1000_decel_stop(axis_num);
        }
        public void AxisStopEmg(int axis_num)
        {
            Dmc1000.d1000_immediate_stop(axis_num);
        }
        public void Set_PLS_Mode(int axis_num , enum_Axis_PLSMode Axis_PLSMode)
        {
            if (Axis_PLSMode == enum_Axis_PLSMode.pulse_dir)
            {
                Dmc1000.d1000_set_pls_outmode(axis_num, 0);
            }
            else if (Axis_PLSMode == enum_Axis_PLSMode.cw_ccw)
            {
                Dmc1000.d1000_set_pls_outmode(axis_num, 3);
            }
        }
        public void Set_SDMode_Enable(int axis_num, bool enable)
        {
            Dmc1000.d1000_set_sd(axis_num, enable ? 1 : 0);
        }
        public void Set_PC_ControlEnable(int axis_num, bool Enable)
        {
            if (axis_num < Axis_PC_Enable.Count) Axis_PC_Enable[axis_num] = Enable;
        }

        public bool GetInput(int CardNum, int BitNum)
        {
            int index = (CardNum * 32) + (BitNum + 1);
            return (Dmc1000.d1000_in_bit(index) == 0);
        }
        public bool GetOutput(int CardNum, int BitNum)
        {
            int index = (CardNum * 32) + (BitNum + 1);
            return (Dmc1000.d1000_get_outbit(index) == 0);
        }
        public void SetOutput(int CardNum, int BitNum, bool Statu)
        {
            int index = (CardNum * 32) + (BitNum + 1);
            string adress = DMC1000B_Basic[CardNum].Get_Output_Adress(BitNum);
            PLC.properties.Device.Set_Device(adress, Statu);
            this.SetOutput(index, Statu);
        }
        private void SetOutput(int index, bool Statu)
        {
            Dmc1000.d1000_out_bit(index, Statu ? 0 : 1);
        }
        public bool Get_Axis_Input(int axis_num, DMC1000B_Basic.enum_Axis_Input enum_Axis_State)
        {
            int status = this.GetAxisStatus(axis_num);
            return myConvert.Int32GetBit(status, (int)enum_Axis_State);
        }
        private int GetAxisStatus(int axis_num)
        {
            return Dmc1000.d1000_get_axis_status(axis_num);
        }
        public int GetAxisCmdPos(int axis_num)
        {
            return Dmc1000.d1000_get_command_pos(axis_num); 
        }
        public void SetAxisCmdPos(int axis_num, int pos)
        {
            Dmc1000.d1000_set_command_pos(axis_num, pos);
        }
        public void PLSV_ChangeSpeed(int axis_num, int Speed)
        {
            Dmc1000.d1000_change_speed(axis_num, Speed);
        }
        #endregion

    }
}
