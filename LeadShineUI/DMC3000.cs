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
    [System.Drawing.ToolboxBitmap(typeof(DMC3000), "PCI.bmp")]
    [Designer(typeof(ComponentSet.JLabelExDesigner))]
    public partial class DMC3000 : UserControl
    {
        #region 自訂屬性
        private string _設備名稱 = "DMC3000-001";
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

        private int[] Card_NumOfAxis;
        [ReadOnly(false), Browsable(false), Category(""), Description(""), DefaultValue("")]
        public int Card_count
        {
            get
            {
                return this.Card_NumOfAxis.Length;
            }
        }


        private bool _Board_First_Init = true;
        private bool _CAN_First_Init = true;
        private bool IsOpen = false;
        private MyThread MyThread_Program;
        private MyThread MyThread_RefreshUI;
        private Form Active_Form;
        private String StreamName;
        private LowerMachine PLC;
        private MyConvert myConvert = new MyConvert();
        private TabControl tabControl = new TabControl();
        private TabPage[] tabPage;

        private List<DMC3C00_Basic> List_DMC3000_Basic = new List<DMC3C00_Basic>();

        public DMC3000()
        {
            InitializeComponent();
        }

        public void Run(Form form, LowerMachine PLC)
        {
            this.Active_Form = form;
            this.PLC = PLC;
            this.StreamName = this.numWordTextBox_StreamName.Text + ".pro";

            this.Init();

            this.MyThread_Program = new MyThread(form);
            this.MyThread_Program.Add_Method(this.sub_Program);
            this.MyThread_Program.AutoRun(true);
            this.MyThread_Program.SetSleepTime(this.CycleTime);
            this.MyThread_Program.Trigger();

            this.PLC.Add_UI_Method(this.sub_RefreshUI);


            //this.MyThread_RefreshUI = new MyThread(form);
            //this.MyThread_RefreshUI.Add_Method(this.RefreshUI);
            //this.MyThread_RefreshUI.AutoRun(true);
            //this.MyThread_RefreshUI.SetSleepTime(30);
            //this.MyThread_RefreshUI.Trigger();
        }
        private void Init()
        {
            this.BoardOpen();
            Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
        }
        private void BoardInit()
        {

            this.TabPage_Init();
            for (int i = 0; i < Card_count; i++)
            {
                this.List_DMC3000_Basic[i].Init(this.Card_NumOfAxis[i]);
            }

            this.LoadProperties();

            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < this.Card_NumOfAxis[i]; k++)
                {
                    this.Set_SrvOn_PIN(i, k, false);
                    this.Set_INP_PIN_Mode(i, k, false, enum_INP_PIN_Mode.HIGH_ACTIVE);
                    this.Set_Counter_Mode(i, k, 0);
                    this.Set_HighSpeed_CMP_Clear(i, 0);
                    this.Set_HighSpeed_CMP_Mode(i, k, 0);
                    this.Set_ALM_PIN_Mode(i, k, true, enum_ALM_PIN_Mode.HIGH_ACTIVE);
                    this.Set_EL_PIN_Mode(i, k, enum_EL_PIN_Enable.P_EL_Enable__N_EL_Enable, enum_EL_PIN_Logic.P_EL_HIGH_ACTIVE__N_EL_HIGH_ACTIVE, enum_EL_PIN_Mode.P_EL_EMG_STOP__N_EL_EMG_STOP);
                }
            }


        }
        private void BoardOpen()
        {
            this.Card_NumOfAxis = new int[Dmc3000.dmc_board_init()];
            uint NumOdAxis = 0;
            for (int i = 0; i < this.Card_count; i++)
            {
                NumOdAxis = 0;
                Dmc3000.dmc_get_total_axes((ushort)i, ref NumOdAxis);
                this.Card_NumOfAxis[i] = (int)NumOdAxis;
            }
            if (this._Board_First_Init)
            {
                this.BoardInit();
            }
            if (!this._Board_First_Init) this.SaveProperties();
            for (int i = 0; i < Card_count; i++)
            {
                this.List_DMC3000_Basic[i].Set_UI_Enable(false);
            }
            int axis_index = 0;
            int device_index = 0;
            string commemt = "";
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < this.Card_NumOfAxis[i]; k++)
                {
                    axis_index = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                    string[] str_enumNames = DMC3C00_Basic.enum_Axis_State._1P_2P_模式.GetEnumNames();
                    for (int m = 0; m < str_enumNames.Length; m++)
                    {
                        commemt = "(DMC3000)" + i.ToString("00") + "-" + k.ToString("00") + " " + str_enumNames[m];
                        PLC.properties.Device.Set_Device("M" + ((8340 + axis_index * 10 + m)).ToString(), commemt);
                    }
                    str_enumNames = DMC3C00_Basic.enum_Axis_Parameter.S段時間.GetEnumNames();
                    for (int m = 0; m < str_enumNames.Length; m++)
                    {
                        commemt = "(DMC3000)" + i.ToString("00") + "-" + k.ToString("00") + " " + str_enumNames[m];
                        PLC.properties.Device.Set_Device("D" + ((8340 + axis_index * 10 + m)).ToString(), commemt);
                    }
                    str_enumNames = DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP比較位置_0.GetEnumNames();
                    for (int m = 0; m < str_enumNames.Length; m++)
                    {
                        commemt = "(DMC3000)" + i.ToString("00") + "-" + k.ToString("00") + " " + str_enumNames[m];
                        PLC.properties.Device.Set_Device("R" + ((8340 + axis_index * 10 + m)).ToString(), commemt);
                    }
                }
            }
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < this.Card_NumOfAxis[i]; k++)
                {
                    Dmc3000.dmc_set_axis_io_map((ushort)i, (ushort)k, 0, 0, (ushort)k, 0);
                    Dmc3000.dmc_set_axis_io_map((ushort)i, (ushort)k, 1, 1, (ushort)k, 0);
                    Dmc3000.dmc_set_axis_io_map((ushort)i, (ushort)k, 2, 2, (ushort)k, 0);
                    Dmc3000.dmc_set_axis_io_map((ushort)i, (ushort)k, 5, 3, (ushort)k, 0);
                    Dmc3000.dmc_set_axis_io_map((ushort)i, (ushort)k, 6, 4, (ushort)k, 0);
                    Dmc3000.dmc_set_axis_io_map((ushort)i, (ushort)k, 7, 5, (ushort)k, 0);
                }
            }
            this._Board_First_Init = false;
            this.IsOpen = true;
        }
        private void BoardClose()
        {
            this.IsOpen = false;
            Dmc3000.dmc_board_close();
            for (int i = 0; i < Card_count; i++)
            {
                this.List_DMC3000_Basic[i].Set_UI_Enable(true);
            }
        }
        private void TabPage_Init()
        {
            this.Invoke(new Action(delegate
            {
                tabPage = new TabPage[Card_count];
                for (int i = 0; i < Card_count; i++)
                {
                    DMC3C00_Basic DMC3C00_Basic = new LeadShineUI.DMC3C00_Basic();
                    DMC3C00_Basic = new DMC3C00_Basic();
                    DMC3C00_Basic.Dock = System.Windows.Forms.DockStyle.Fill;
                    DMC3C00_Basic.Location = new System.Drawing.Point(3, 3);
                    DMC3C00_Basic.Name = "DMC3000_Basic" + i.ToString();
                    DMC3C00_Basic.Size = new System.Drawing.Size(569, 429);
                    DMC3C00_Basic.TabIndex = 0;
                    DMC3C00_Basic.SetPLC(this.PLC);

                    this.List_DMC3000_Basic.Add(DMC3C00_Basic);
                    tabPage[i] = new TabPage();
                    tabPage[i].Controls.Add(DMC3C00_Basic);
                    tabPage[i].Location = new System.Drawing.Point(4, 22);
                    tabPage[i].Name = "tabPage_card_" + i.ToString();
                    tabPage[i].Padding = new System.Windows.Forms.Padding(3);
                    tabPage[i].Size = new System.Drawing.Size(192, 74);
                    tabPage[i].TabIndex = i;
                    tabPage[i].Text = "CARD-" + i.ToString();
                    tabPage[i].UseVisualStyleBackColor = true;



                }
                this.Invoke(new Action(delegate
                {
                    if (this.tabControl == null) this.tabControl = new TabControl();
                    tabControl.SuspendLayout();
                    for (int i = 0; i < tabPage.Length; i++)
                    {
                        if (this.tabPage[i] != null)
                        {
                            this.tabControl.Controls.Add(this.tabPage[i]);
                        }
                    }
                    this.tabControl.Location = new System.Drawing.Point(0, panel_TAB.Size.Height);
                    //this.tabControl.Name = "tabControl_C9016";
                    this.tabControl.SelectedIndex = 0;
                    this.tabControl.Dock = DockStyle.Fill;
                    this.panel_TAB.Controls.Add(this.tabControl);
                    tabControl.ResumeLayout(false);

                }));
            }));
        }
        private void CAN_Init()
        {
            int NodeNum = 0;
            bool state = false;
            for (int i = 0; i < Card_count; i++)
            {
                this.Get_CAN_State(i, ref NodeNum, ref state);
                for (int k = 7; k >= 0; k--)
                {
                    if (this.Set_CAN_State(i, k, true, DMC3C00_Basic.enum_Baudrate._1000Kbs) == 0)
                    {
                        break;
                    }
                    else
                    {
                        this.Set_CAN_State(i, k, false, DMC3C00_Basic.enum_Baudrate._1000Kbs);
                    }
                }

                this.Get_CAN_State(i, ref NodeNum, ref state);
                this.List_DMC3000_Basic[i].Set_CAN_State(state);
                this.List_DMC3000_Basic[i].Set_CAN_NodeNum(NodeNum);
            }
            System.Threading.Thread.Sleep(500);
            foreach (Control ctl in this.Active_Form.Controls)
            {
                this.FindSubControl(ctl);
            }
        }
        void sub_FindScreenPage(ref Control ctl)
        {
            if (ctl.Parent is Form)
            {
                ctl = ctl.Parent;
                return;
            }
            else if (ctl.Parent is PLC_ScreenPage)
            {
                ctl = ctl.Parent;
                return;
            }
            else
            {
                ctl = ctl.Parent;
                sub_FindScreenPage(ref ctl);
            }
        }
        void FindSubControl(Control ctl)
        {
            if (ctl is PLC_ScreenPage)
            {
                PLC_ScreenPage ctl_temp = (PLC_ScreenPage)ctl;
                foreach (Control temp in ctl_temp.Controls)
                {
                    FindSubControl(temp);
                }
                ctl_temp.Run(PLC, Active_Form);
            }
            else if (ctl.Controls.Count > 0)
            {
                foreach (Control sub_ctl in ctl.Controls)
                {
                    FindSubControl(sub_ctl);
                }
            }

            if (ctl is PLC_ScreenButton)
            {
                PLC_ScreenButton ctl_temp = (PLC_ScreenButton)ctl;
                ctl_temp.Run(PLC);
                sub_FindScreenPage(ref ctl);
                if (ctl is PLC_ScreenPage)
                {
                    ctl_temp.Set_PLC_ScreenPage((PLC_ScreenPage)ctl);
                }
            }
            else if (ctl is EM32DX_C1)
            {
                EM32DX_C1 ctl_temp = (EM32DX_C1)ctl;
                ctl_temp.Run(Active_Form, PLC);
            }
            else if (ctl is EM06AX_C1)
            {
                EM06AX_C1 ctl_temp = (EM06AX_C1)ctl;
                ctl_temp.Run(Active_Form, PLC);
            }
        }
        private void sub_Program()
        {
            if (IsOpen && PLC != null)
            {
                this.sub_GetInput();
                this.sub_ReadFromPLC();
                this.sub_Run_Axis();
            }
        }
        private void sub_RefreshUI()
        {
            if (PLC != null)
            {
                plC_Button_Open.SetValue(IsOpen);
                plC_Button_Open.Run(PLC);
            }
            if (IsOpen && PLC != null)
            {
                if (this._CAN_First_Init)
                {
                    this.CAN_Init();
                    this._CAN_First_Init = false;
                }
                for (int i = 0; i < Card_count; i++)
                {
                    this.List_DMC3000_Basic[i].RefreshUI();
                }
                MyThread_Program.GetCycleTime(100, label_CycleTime);
            }
        }
        #region GetInput
        private void sub_GetInput()
        {
            string adress = "";
            int index;
            ushort io_status = 0;
            uint rsts = 0;
            uint read_prot = 0;
            System.Collections.BitArray[] BitArray_potr0 = new System.Collections.BitArray[Card_count];
            System.Collections.BitArray BitArray_Axis_status;
            for (int i = 0; i < Card_count; i++)
            {
                BitArray_potr0[i] = this.Get_InputPort_BitArray(i, 0);
            }
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 16; k++)
                {
                    List_DMC3000_Basic[i].Set_Input(k, !BitArray_potr0[i][k]);
                    adress = List_DMC3000_Basic[i].Get_Input_Adress(k);
                    if (adress != string.Empty) PLC.properties.device_system.Set_DeviceFast_Ex(adress, !BitArray_potr0[i][k]);
                }
                for (int m = 0; m < this.Card_NumOfAxis[i]; m++)
                {
                    BitArray_Axis_status = this.Get_Axis_Input_BitArray(i, m);

                    List_DMC3000_Basic[i].Set_Axis_Input(m, DMC3C00_Basic.enum_Axis_Input.ALM, BitArray_Axis_status[(int)enum_Axis_io_status.ALM]);
                    List_DMC3000_Basic[i].Set_Axis_Input(m, DMC3C00_Basic.enum_Axis_Input.EMG, BitArray_Axis_status[(int)enum_Axis_io_status.EMG]);
                    List_DMC3000_Basic[i].Set_Axis_Input(m, DMC3C00_Basic.enum_Axis_Input.EZ, BitArray_Axis_status[(int)enum_Axis_io_status.EZ]);
                    List_DMC3000_Basic[i].Set_Axis_Input(m, DMC3C00_Basic.enum_Axis_Input.INP, BitArray_Axis_status[(int)enum_Axis_io_status.INP]);
                    List_DMC3000_Basic[i].Set_Axis_Input(m, DMC3C00_Basic.enum_Axis_Input.N_EL, BitArray_Axis_status[(int)enum_Axis_io_status.N_EL]);
                    List_DMC3000_Basic[i].Set_Axis_Input(m, DMC3C00_Basic.enum_Axis_Input.P_EL, BitArray_Axis_status[(int)enum_Axis_io_status.P_EL]);
                    List_DMC3000_Basic[i].Set_Axis_Input(m, DMC3C00_Basic.enum_Axis_Input.ORG, BitArray_Axis_status[(int)enum_Axis_io_status.ORG]);
                    List_DMC3000_Basic[i].Set_Axis_Input(m, DMC3C00_Basic.enum_Axis_Input.RDY, BitArray_Axis_status[(int)enum_Axis_io_status.RDY]);

                    adress = List_DMC3000_Basic[i].Get_Axis_Input_Adress(m, DMC3C00_Basic.enum_Axis_Input.ALM);
                    if (adress != string.Empty) PLC.properties.device_system.Set_DeviceFast_Ex(adress, BitArray_Axis_status[(int)enum_Axis_io_status.ALM]);
                    adress = List_DMC3000_Basic[i].Get_Axis_Input_Adress(m, DMC3C00_Basic.enum_Axis_Input.EMG);
                    if (adress != string.Empty) PLC.properties.device_system.Set_DeviceFast_Ex(adress, BitArray_Axis_status[(int)enum_Axis_io_status.EMG]);
                    adress = List_DMC3000_Basic[i].Get_Axis_Input_Adress(m, DMC3C00_Basic.enum_Axis_Input.EZ);
                    if (adress != string.Empty) PLC.properties.device_system.Set_DeviceFast_Ex(adress, BitArray_Axis_status[(int)enum_Axis_io_status.EZ]);
                    adress = List_DMC3000_Basic[i].Get_Axis_Input_Adress(m, DMC3C00_Basic.enum_Axis_Input.INP);
                    if (adress != string.Empty) PLC.properties.device_system.Set_DeviceFast_Ex(adress, BitArray_Axis_status[(int)enum_Axis_io_status.INP]);
                    adress = List_DMC3000_Basic[i].Get_Axis_Input_Adress(m, DMC3C00_Basic.enum_Axis_Input.N_EL);
                    if (adress != string.Empty) PLC.properties.device_system.Set_DeviceFast_Ex(adress, BitArray_Axis_status[(int)enum_Axis_io_status.N_EL]);
                    adress = List_DMC3000_Basic[i].Get_Axis_Input_Adress(m, DMC3C00_Basic.enum_Axis_Input.P_EL);
                    if (adress != string.Empty) PLC.properties.device_system.Set_DeviceFast_Ex(adress, BitArray_Axis_status[(int)enum_Axis_io_status.P_EL]);
                    adress = List_DMC3000_Basic[i].Get_Axis_Input_Adress(m, DMC3C00_Basic.enum_Axis_Input.ORG);
                    if (adress != string.Empty) PLC.properties.device_system.Set_DeviceFast_Ex(adress, BitArray_Axis_status[(int)enum_Axis_io_status.ORG]);
                    adress = List_DMC3000_Basic[i].Get_Axis_Input_Adress(m, DMC3C00_Basic.enum_Axis_Input.RDY);
                    if (adress != string.Empty) PLC.properties.device_system.Set_DeviceFast_Ex(adress, BitArray_Axis_status[(int)enum_Axis_io_status.RDY]);
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
            int temp2;
            int axis_num;
            int index;
            uint write_prot = 0;
            #region SetOutput
            for (int i = 0; i < Card_count; i++)
            {
                //write_prot = this.Get_Output_Port(i);
                for (int k = 0; k < 16; k++)
                {

                    adress = this.List_DMC3000_Basic[i].Get_Output_Adress(k);
                    if (adress != string.Empty)
                    {
                        if (!this.List_DMC3000_Basic[i].Get_Output_PCUse(k))
                        {

                            //if (adress != string.Empty)
                            //{
                            //    flag = PLC.properties.device_system.Get_DeviceFast_Ex(adress);
                            //    this.List_DMC3000_Basic[i].Set_Output(k, flag);
                            //    write_prot = myConvert.UInt32SetBit(!flag, write_prot, k);
                            //}
                            //else
                            //{
                            //    write_prot = myConvert.UInt32SetBit(!false, write_prot, k);
                            //}
                            flag = PLC.properties.device_system.Get_DeviceFast_Ex(adress);
                            this.List_DMC3000_Basic[i].Set_Output(k, flag);
                            Dmc3000.dmc_write_outbit((ushort)i, (ushort)k, !flag ? (ushort)1 : (ushort)0);
                        }

                    }



                }
                ////this.Set_Output_Port(i, write_prot);

                //System.Collections.BitArray BitArray_out_port = this.Get_OutputPort_BitArray(i);
                //for (int k = 0; k < 16; k++)
                //{
                //    adress = this.List_DMC3000_Basic[i].Get_Output_Adress(k);
                //    if (adress != string.Empty)
                //    {
                //        PLC.properties.device_system.Set_DeviceFast_Ex(adress, !BitArray_out_port[k]);
                //    }
                //    this.List_DMC3000_Basic[i].Set_Output(k, !BitArray_out_port[k]);
                //}
            }
            #endregion
            #region Set_Axis_State
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < this.Card_NumOfAxis[i]; k++)
                {
                    #region Axis_Busy
                    temp0 = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                    flag = this.Get_Axis_Ready(i, k);
                    this.List_DMC3000_Basic[i].Set_Axis_State(k, DMC3C00_Basic.enum_Axis_State.Axis_Busy, !flag);
                    PLC.properties.Device.Set_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 0)), !flag);
                    #endregion
                    #region 位置更改致能
                    temp0 = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 1)));
                    this.List_DMC3000_Basic[i].Set_Axis_State(k, LeadShineUI.DMC3C00_Basic.enum_Axis_State.位置更改致能, flag);
                    if (!this.List_DMC3000_Basic[i].Get_Axis_State(k, LeadShineUI.DMC3C00_Basic.enum_Axis_State.位置更改致能))
                    {
                        temp1 = this.Get_Command_Pos(i, k);
                        temp2 = this.Get_Counter(i, k);
                        this.List_DMC3000_Basic[i].Set_Axis_Parameter(k, LeadShineUI.DMC3C00_Basic.enum_Axis_Parameter.現在位置, temp1);
                        this.List_DMC3000_Basic[i].Set_Axis_Counter_Parameter(k, DMC3C00_Basic.enum_Axis_Counter_Parameter.現在位置, temp2);
                        PLC.properties.Device.Set_DataFast_Ex("D", ((8340 + temp0 * 10)), temp1);
                        PLC.properties.Device.Set_DataFast_Ex("R", ((8340 + temp0 * 10)), temp2);
                    }
                    else
                    {
                        this.Set_Command_Pos(i, k, PLC.properties.Device.Get_DataFast_Ex("D", ((8340 + temp0 * 10))));
                        this.Set_Counter(i, k, PLC.properties.Device.Get_DataFast_Ex("R", ((8340 + temp0 * 10))));
                    }
                    #endregion
                    #region JOG加減速致能
                    temp0 = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 2)));
                    this.List_DMC3000_Basic[i].Set_Axis_State(k, LeadShineUI.DMC3C00_Basic.enum_Axis_State.JOG加減速致能, flag);
                    #endregion
                    #region _1P_2P_模式
                    temp0 = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 3)));
                    this.List_DMC3000_Basic[i].Set_Axis_State(k, LeadShineUI.DMC3C00_Basic.enum_Axis_State._1P_2P_模式, flag);
                    if (this.List_DMC3000_Basic[i].buffer._1P_2P_模式[k] != flag)
                    {
                        this.Set_PLS_Mode(i, k, flag ? enum_Axis_PLSMode.pulse_dir : enum_Axis_PLSMode.cw_ccw);
                        this.List_DMC3000_Basic[i].buffer._1P_2P_模式[k] = flag;
                    }
                    #endregion
                    #region SD減速致能
                    temp0 = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 4)));
                    this.List_DMC3000_Basic[i].Set_Axis_State(k, LeadShineUI.DMC3C00_Basic.enum_Axis_State.SD減速致能, flag);
                    #endregion
                    #region T_S_速度模式
                    temp0 = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 5)));
                    this.List_DMC3000_Basic[i].Set_Axis_State(k, LeadShineUI.DMC3C00_Basic.enum_Axis_State.T_S_速度模式, flag);
                    #endregion
                    #region CMP致能
                    temp0 = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 6)));
                    this.List_DMC3000_Basic[i].Set_Axis_State(k, LeadShineUI.DMC3C00_Basic.enum_Axis_State.CMP致能, flag);

                    int CMP比較來源 = PLC.properties.Device.Get_DataFast_Ex("R", ((8340 + temp0 * 10 + (int)LeadShineUI.DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP比較來源)));
                    int CMP比較模式 = PLC.properties.Device.Get_DataFast_Ex("R", ((8340 + temp0 * 10 + (int)LeadShineUI.DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP比較模式)));
                    int CMP脈衝寬度 = PLC.properties.Device.Get_DataFast_Ex("R", ((8340 + temp0 * 10 + (int)LeadShineUI.DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP脈衝寬度)));
                    int CMP輸出電位 = PLC.properties.Device.Get_DataFast_Ex("R", ((8340 + temp0 * 10 + (int)LeadShineUI.DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP輸出電位)));
                    int CMP比較位置_0 = PLC.properties.Device.Get_DataFast_Ex("R", ((8340 + temp0 * 10 + (int)LeadShineUI.DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP比較位置_0)));
                    int CMP比較位置_1 = PLC.properties.Device.Get_DataFast_Ex("R", ((8340 + temp0 * 10 + (int)LeadShineUI.DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP比較位置_1)));
                    int CMP比較位置_2 = PLC.properties.Device.Get_DataFast_Ex("R", ((8340 + temp0 * 10 + (int)LeadShineUI.DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP比較位置_2)));
                    int CMP比較位置_3 = PLC.properties.Device.Get_DataFast_Ex("R", ((8340 + temp0 * 10 + (int)LeadShineUI.DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP比較位置_3)));

                    this.List_DMC3000_Basic[i].Set_Axis_Counter_Parameter(k, DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP比較來源, CMP比較來源);
                    this.List_DMC3000_Basic[i].Set_Axis_Counter_Parameter(k, DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP比較模式, CMP比較模式);
                    this.List_DMC3000_Basic[i].Set_Axis_Counter_Parameter(k, DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP脈衝寬度, CMP脈衝寬度);
                    this.List_DMC3000_Basic[i].Set_Axis_Counter_Parameter(k, DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP輸出電位, CMP輸出電位);
                    this.List_DMC3000_Basic[i].Set_Axis_Counter_Parameter(k, DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP比較位置_0, CMP比較位置_0);
                    this.List_DMC3000_Basic[i].Set_Axis_Counter_Parameter(k, DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP比較位置_1, CMP比較位置_1);
                    this.List_DMC3000_Basic[i].Set_Axis_Counter_Parameter(k, DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP比較位置_2, CMP比較位置_2);
                    this.List_DMC3000_Basic[i].Set_Axis_Counter_Parameter(k, DMC3C00_Basic.enum_Axis_Counter_Parameter.CMP比較位置_3, CMP比較位置_3);

                    if (this.List_DMC3000_Basic[i].buffer.CMP致能[k] != flag)
                    {
                        if (flag)
                        {
                            this.Set_HighSpeed_CMP_Config(i, 0, k, (enum_HighSpeed_CMP_Source)CMP比較來源, (enum_HighSpeed_CMP_Logic)CMP輸出電位, CMP脈衝寬度);
                            this.Set_HighSpeed_CMP_Mode(i, k, (enum_HighSpeed_CMP_Mode)CMP比較模式);
                            this.Set_HighSpeed_CMP_Clear(i, 0);
                            if ((enum_HighSpeed_CMP_Mode)CMP比較模式 == enum_HighSpeed_CMP_Mode.ARRAY)
                            {
                                this.Set_HighSpeed_CMP_add_point(i, 0, CMP比較位置_0);
                                this.Set_HighSpeed_CMP_add_point(i, 0, CMP比較位置_1);
                                this.Set_HighSpeed_CMP_add_point(i, 0, CMP比較位置_2);
                                this.Set_HighSpeed_CMP_add_point(i, 0, CMP比較位置_3);
                            }
                            else if ((enum_HighSpeed_CMP_Mode)CMP比較模式 == enum_HighSpeed_CMP_Mode.EQAL)
                            {
                                this.Set_HighSpeed_CMP_add_point(i, 0, CMP比較位置_0);
                            }
                            else if ((enum_HighSpeed_CMP_Mode)CMP比較模式 == enum_HighSpeed_CMP_Mode.EQAL_GREATER)
                            {
                                this.Set_HighSpeed_CMP_add_point(i, 0, CMP比較位置_0);
                            }
                            else if ((enum_HighSpeed_CMP_Mode)CMP比較模式 == enum_HighSpeed_CMP_Mode.EQAL_LESS)
                            {
                                this.Set_HighSpeed_CMP_add_point(i, 0, CMP比較位置_0);
                            }
                        }
                        else
                        {
                            this.Set_HighSpeed_CMP_Clear(i, 0);
                            this.Set_HighSpeed_CMP_Mode(i, k, 0);
                        }
                        this.List_DMC3000_Basic[i].buffer.CMP致能[k] = flag;
                    }
                    #endregion
                    #region ALM低電位有效
                    temp0 = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 7)));
                    this.List_DMC3000_Basic[i].Set_Axis_State(k, LeadShineUI.DMC3C00_Basic.enum_Axis_State.ALM低電位有效, flag);
                    if (this.List_DMC3000_Basic[i].buffer.ALM低電位有效[k] != flag)
                    {
                        if (flag) this.Set_ALM_PIN_Mode(i, k, true, enum_ALM_PIN_Mode.LOW_ACTIVE);
                        else this.Set_ALM_PIN_Mode(i, k, true, enum_ALM_PIN_Mode.HIGH_ACTIVE);
                        this.List_DMC3000_Basic[i].buffer.ALM低電位有效[k] = flag;
                    }
                    #endregion
                    #region EL低電位有效
                    temp0 = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 8)));
                    this.List_DMC3000_Basic[i].Set_Axis_State(k, LeadShineUI.DMC3C00_Basic.enum_Axis_State.EL低電位有效, flag);
                    if (this.List_DMC3000_Basic[i].buffer.EL低電位有效[k] != flag)
                    {
                        if (flag) this.Set_EL_PIN_Mode(i, k, enum_EL_PIN_Enable.P_EL_Enable__N_EL_Enable, enum_EL_PIN_Logic.P_EL_LOW_ACTIVE__N_EL_LOW_ACTIVE, enum_EL_PIN_Mode.P_EL_EMG_STOP__N_EL_EMG_STOP);
                        else this.Set_EL_PIN_Mode(i, k, enum_EL_PIN_Enable.P_EL_Enable__N_EL_Enable, enum_EL_PIN_Logic.P_EL_HIGH_ACTIVE__N_EL_HIGH_ACTIVE, enum_EL_PIN_Mode.P_EL_EMG_STOP__N_EL_EMG_STOP);
                        this.List_DMC3000_Basic[i].buffer.EL低電位有效[k] = flag;
                    }
                    #endregion
                    #region 減速停止
                    temp0 = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                    flag = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + temp0 * 10 + 9)));
                    this.List_DMC3000_Basic[i].Set_Axis_State(k, LeadShineUI.DMC3C00_Basic.enum_Axis_State.減速停止, flag);
                    #endregion

                    #region 高速計數方式
                    temp0 = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                    temp1 = PLC.properties.Device.Get_DataFast_Ex("R", ((8340 + temp0 * 10 + (int)DMC3C00_Basic.enum_Axis_Counter_Parameter.計數方式)));
                    if (this.List_DMC3000_Basic[i].buffer.計數方式[k] != temp1)
                    {
                        this.Set_Counter_Mode(i, k, (enum_Counter_Mode)temp1);
                        this.List_DMC3000_Basic[i].buffer.計數方式[k] = temp1;
                    }
                    this.List_DMC3000_Basic[i].Set_Axis_Counter_Parameter(k, DMC3C00_Basic.enum_Axis_Counter_Parameter.計數方式, temp1);
                    #endregion
                }
            }
            #endregion
            #region Set_Axis_Output
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < this.Card_NumOfAxis[i]; k++)
                {
                    #region SrvOn
                    temp0 = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                    adress = this.List_DMC3000_Basic[i].Get_Axis_Output_Adress(k, DMC3C00_Basic.enum_Axis_Output.SrvOn);
                    if (adress != string.Empty)
                    {
                        flag = PLC.properties.device_system.Get_DeviceFast_Ex(adress);
                        if (this.List_DMC3000_Basic[i].buffer.Srv_On[k] != flag)
                        {
                            this.Set_SrvOn_PIN(i, k, flag);
                            this.List_DMC3000_Basic[i].Set_Axis_Output(k, DMC3C00_Basic.enum_Axis_Output.SrvOn, flag);
                            this.List_DMC3000_Basic[i].buffer.Srv_On[k] = flag;
                        }

                    }
                    #endregion
                    #region ERC
                    temp0 = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                    adress = this.List_DMC3000_Basic[i].Get_Axis_Output_Adress(k, DMC3C00_Basic.enum_Axis_Output.ERC);
                    if (adress != string.Empty)
                    {
                        flag = PLC.properties.device_system.Get_DeviceFast_Ex(adress);
                        this.Set_ERC_PIN(i, k, flag);
                        this.List_DMC3000_Basic[i].Set_Axis_Output(k, DMC3C00_Basic.enum_Axis_Output.ERC, flag);
                    }
                    #endregion
                }
            }
            #endregion

        }
        #endregion
        #region Run_Axis
        void sub_Run_Axis()
        {
            int active_code;
            int axis_index;
            int basic_speed;
            int active_speed;
            int target_position;
            int stop_speed;
            double Tacc;
            double Tdec;
            double S_para;
            bool S_Mode;
            bool 減速停止;
            bool paulse = false;

            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < this.Card_NumOfAxis[i]; k++)
                {
                    paulse = false;
                    if (!this.List_DMC3000_Basic[i].buffer.PC_Enable[k])
                    {
                        axis_index = this.List_DMC3000_Basic[i].Get_Axis_Index(k);
                        active_code = PLC.properties.Device.Get_DataFast_Ex("D", ((8340 + axis_index * 10 + (int)DMC3C00_Basic.enum_Axis_Parameter.運動命令碼)));
                        basic_speed = PLC.properties.Device.Get_DataFast_Ex("D", ((8340 + axis_index * 10 + (int)DMC3C00_Basic.enum_Axis_Parameter.基底速度)));
                        active_speed = PLC.properties.Device.Get_DataFast_Ex("D", ((8340 + axis_index * 10 + (int)DMC3C00_Basic.enum_Axis_Parameter.運轉速度)));
                        target_position = PLC.properties.Device.Get_DataFast_Ex("D", ((8340 + axis_index * 10 + (int)DMC3C00_Basic.enum_Axis_Parameter.運轉目標位置)));
                        stop_speed = PLC.properties.Device.Get_DataFast_Ex("D", ((8340 + axis_index * 10 + (int)DMC3C00_Basic.enum_Axis_Parameter.停止速度)));
                        Tacc = PLC.properties.Device.Get_DataFast_Ex("D", ((8340 + axis_index * 10 + (int)DMC3C00_Basic.enum_Axis_Parameter.加速度)));
                        Tdec = PLC.properties.Device.Get_DataFast_Ex("D", ((8340 + axis_index * 10 + (int)DMC3C00_Basic.enum_Axis_Parameter.減速度)));
                        S_para = PLC.properties.Device.Get_DataFast_Ex("D", ((8340 + axis_index * 10 + (int)DMC3C00_Basic.enum_Axis_Parameter.S段時間)));

                        S_Mode = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + axis_index * 10 + (int)DMC3C00_Basic.enum_Axis_State.T_S_速度模式)));
                        減速停止 = PLC.properties.Device.Get_DeviceFast_Ex("M", ((8340 + axis_index * 10 + (int)DMC3C00_Basic.enum_Axis_State.減速停止)));


                        this.List_DMC3000_Basic[i].Set_Axis_Parameter(k, LeadShineUI.DMC3C00_Basic.enum_Axis_Parameter.運動命令碼, active_code);
                        this.List_DMC3000_Basic[i].Set_Axis_Parameter(k, LeadShineUI.DMC3C00_Basic.enum_Axis_Parameter.基底速度, basic_speed);
                        this.List_DMC3000_Basic[i].Set_Axis_Parameter(k, LeadShineUI.DMC3C00_Basic.enum_Axis_Parameter.運轉速度, active_speed);
                        this.List_DMC3000_Basic[i].Set_Axis_Parameter(k, LeadShineUI.DMC3C00_Basic.enum_Axis_Parameter.運轉目標位置, target_position);
                        this.List_DMC3000_Basic[i].Set_Axis_Parameter(k, LeadShineUI.DMC3C00_Basic.enum_Axis_Parameter.停止速度, stop_speed);
                        this.List_DMC3000_Basic[i].Set_Axis_Parameter(k, LeadShineUI.DMC3C00_Basic.enum_Axis_Parameter.加速度, (int)Tacc);
                        this.List_DMC3000_Basic[i].Set_Axis_Parameter(k, LeadShineUI.DMC3C00_Basic.enum_Axis_Parameter.減速度, (int)Tdec);
                        this.List_DMC3000_Basic[i].Set_Axis_Parameter(k, LeadShineUI.DMC3C00_Basic.enum_Axis_Parameter.S段時間, (int)S_para);
                        if (this.List_DMC3000_Basic[i].buffer.運動命令碼[k] != active_code)
                        {
                            paulse = true;
                            this.List_DMC3000_Basic[i].buffer.運動命令碼[k] = active_code;
                        }
                        if (active_code == 0)// 停止
                        {
                            if (paulse)
                            {
                                if (減速停止)
                                {
                                    this.AxisStop(i, k);
                                }
                                else
                                {
                                    this.AxisStopEmg(i, k);
                                }
                            }
                        }
                        else if (active_code == 1)// DRVA
                        {
                            if (paulse)
                            {
                                if (!S_Mode) this.DRVA(i, k, target_position, active_speed, basic_speed, stop_speed, Tacc, Tdec, S_para, enum_Axis_SpeedMode.T_Mode);
                                else { this.DRVA(i, k, target_position, active_speed, basic_speed, stop_speed, Tacc, Tdec, S_para, enum_Axis_SpeedMode.S_Mode); }
                            }
                        }
                        else if (active_code == 2)// DRVI
                        {
                            if (paulse)
                            {
                                if (!S_Mode) this.DRVI(i, k, target_position, active_speed, basic_speed, stop_speed, Tacc, Tdec, S_para, enum_Axis_SpeedMode.T_Mode);
                                else { this.DRVI(i, k, target_position, active_speed, basic_speed, stop_speed, Tacc, Tdec, S_para, enum_Axis_SpeedMode.S_Mode); }
                            }
                        }
                        else if (active_code == 3)// PLSV
                        {
                            if (this.List_DMC3000_Basic[i].buffer.JOG速度[k] != active_speed)
                            {
                                Dmc3000.dmc_change_speed((ushort)i, (ushort)k, active_speed, 0);
                                this.List_DMC3000_Basic[i].buffer.JOG速度[k] = active_speed;
                                paulse = true;
                            }
                            if (paulse)
                            {
                                this.PLSV(i, k, active_speed, basic_speed, 0, 0);
                            }
                        }
                        else if (active_code == 4)// PLSV(Tacc , Tdec)
                        {
                            if (this.List_DMC3000_Basic[i].buffer.JOG速度[k] != active_speed)
                            {
                                Dmc3000.dmc_change_speed((ushort)i, (ushort)k, active_speed, Tacc / 1000D);
                                this.List_DMC3000_Basic[i].buffer.JOG速度[k] = active_speed;
                                paulse = true;
                            }
                            if (paulse)
                            {
                                this.PLSV(i, k, active_speed, basic_speed, stop_speed, Tacc, Tdec);
                            }
                        }
                    }
                }
            }

        }
        #endregion

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
            HIGH_ACTIVE = 0, LOW_ACTIVE = 1
        }
        public enum enum_EZ_PIN_Mode : ushort
        {
            HIGH_ACTIVE = 0, LOW_ACTIVE = 1
        }

        public enum enum_EL_PIN_Enable : ushort
        {
            P_EL_DISABLE__N_EL_DISABLE = 0, P_EL_Enable__N_EL_Enable = 1, P_EL_DISABLE__N_EL_Enable = 2, P_EL_Enable_N_EL__DISABLE = 3,
        }
        public enum enum_EL_PIN_Logic : ushort
        {
            P_EL_LOW_ACTIVE__N_EL_LOW_ACTIVE = 1, P_EL_HIGH_ACTIVE__N_EL_HIGH_ACTIVE = 0, P_EL_LOW_ACTIVE__N_EL_HIGH_ACTIVE = 2, P_EL_HIGH_ACTIVE__N_EL_LOW_ACTIVE = 3
        }
        public enum enum_EL_PIN_Mode : ushort
        {
            P_EL_EMG_STOP__N_EL_EMG_STOP = 0, P_EL_TDec_STOP__N_EL_TDec_STOP = 1, P_EL_EMG_STOP__N_EL_TDec_STOP = 2, P_EL_TDec_STOP__N_EL_EMG_STOP = 3
        }

        public enum enum_LTC_PIN_Mode : ushort
        {
            HIGH_ACTIVE = 1, LOW_ACTIVE = 0, HIGH_AND_LOW_ACTIVE = 2
        }
        public enum enum_INP_PIN_Mode : ushort
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
            ALM = 0, P_EL = 1, N_EL = 2, EMG = 3, ORG = 4, PSL = 6, NSL = 7, INP = 8, EZ = 9, RDY = 10, DSTP = 11
        }
        public enum enum_Axis_rsts : int
        {
            EMG = 7, EZ = 9, PA = 11, PB = 12
        }

        public enum enum_Normal_CMP_Source : ushort
        {
            COMMAND = 0, ENCODER = 1
        }
        public enum enum_Normal_CMP_Dir : ushort
        {
            EQAL_GREATER = 0, EQAL_LESS = 1
        }
        public enum enum_Normal_CMP_Action : ushort
        {
            LOW = 1, HIGH = 2, TOGGLE = 3, _500us = 5, _1ms = 6, _10ms = 7, _100ms = 8
        }
        public enum enum_HighSpeed_CMP_Mode : ushort
        {
            DISABLE = 0, EQAL = 1, EQAL_LESS = 2, EQAL_GREATER = 3, ARRAY = 4, LINER = 5,
        }
        public enum enum_HighSpeed_CMP_Source : ushort
        {
            COMMAND = 0, ENCODER = 1
        }
        public enum enum_HighSpeed_CMP_Logic : ushort
        {
            LOW_ACTIVE = 0, HIFG_ACTIVE = 1
        }

        public enum enum_Arc_DIR : ushort
        {
            順時針 = 0, 逆時針 = 1
        }

        public void Get_CAN_State(int CardNum, ref int NodeNum, ref bool state)
        {
            ushort NodeNum_temp = 0;
            ushort state_temp = 0;
            Dmc3000.nmc_get_connect_state((ushort)CardNum, ref NodeNum_temp, ref state_temp);
            NodeNum = NodeNum_temp;
            state = (state_temp == 1);
        }
        public int Set_CAN_State(int CardNum, int NodeNum, bool state, DMC3C00_Basic.enum_Baudrate Baudrate)
        {
            return Dmc3000.nmc_set_connect_state((ushort)CardNum, (ushort)NodeNum, state ? (ushort)1 : (ushort)0, (ushort)Baudrate);
        }

        public void Set_PC_ControlEnable(int CardNum, int axis_num, bool enable)
        {
            if (CardNum < this.List_DMC3000_Basic.Count)
            {
                if (axis_num < this.List_DMC3000_Basic[CardNum].buffer.PC_Enable.Length)
                {
                    this.List_DMC3000_Basic[CardNum].buffer.PC_Enable[axis_num] = enable;
                }
            }

        }
        public bool Get_PC_ControlEnable(int CardNum, int axis_num)
        {
            if (CardNum < this.List_DMC3000_Basic.Count)
            {
                if (axis_num < this.List_DMC3000_Basic[CardNum].buffer.PC_Enable.Length)
                {
                    return this.List_DMC3000_Basic[CardNum].buffer.PC_Enable[axis_num];
                }
            }
            return false;
        }

        public void PLSV(int CardNum, int axis_num, int active_speed, int basic_speed, int stop_speed, double Tacc, double Tdec)
        {
            this.PLSV(CardNum, axis_num, active_speed, basic_speed, stop_speed, Tacc, Tdec, 0, enum_Axis_SpeedMode.T_Mode);
        }
        public void PLSV(int CardNum, int axis_num, int active_speed, int basic_speed, int stop_speed, double Tacc)
        {
            this.PLSV(CardNum, axis_num, active_speed, basic_speed, stop_speed, Tacc, Tacc, 0, enum_Axis_SpeedMode.T_Mode);
        }
        public void PLSV(int CardNum, int axis_num, int active_speed, int basic_speed, int stop_speed, double Tacc, double Tdec, double Tsdec)
        {
            this.PLSV(CardNum, axis_num, active_speed, basic_speed, stop_speed, Tacc, Tdec, Tsdec, enum_Axis_SpeedMode.S_Mode);
        }
        private void PLSV(int CardNum, int axis_num, int active_speed, int basic_speed, int stop_speed, double Tacc, double Tdec, double Tsdec, enum_Axis_SpeedMode Axis_SpeedMode)
        {
            if (Axis_SpeedMode == DMC3000.enum_Axis_SpeedMode.T_Mode)
            {
                ushort DIR = 1;
                if (active_speed < 0)
                {
                    active_speed *= -1;
                    DIR = 0;
                }
                Dmc3000.dmc_set_dec_stop_time((ushort)CardNum, (ushort)axis_num, Tdec / 1000D);
                Dmc3000.dmc_set_profile((ushort)CardNum, (ushort)axis_num, basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D, stop_speed);
                Dmc3000.dmc_set_s_profile((ushort)CardNum, (ushort)axis_num, 0, 0);
                Dmc3000.dmc_vmove((ushort)CardNum, (ushort)axis_num, DIR);
            }
            else if (Axis_SpeedMode == DMC3000.enum_Axis_SpeedMode.S_Mode)
            {
                ushort DIR = 1;
                if (active_speed < 0)
                {
                    active_speed *= -1;
                    DIR = 0;
                }
                Dmc3000.dmc_set_dec_stop_time((ushort)CardNum, (ushort)axis_num, Tdec / 1000D);
                Dmc3000.dmc_set_profile((ushort)CardNum, (ushort)axis_num, basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D, stop_speed);
                Dmc3000.dmc_set_s_profile((ushort)CardNum, (ushort)axis_num, 0, Tsdec / 1000D);
                Dmc3000.dmc_vmove((ushort)CardNum, (ushort)axis_num, DIR);
            }
        }

        public void DRVA(int CardNum, int axis_num, int target_position, int active_speed, int basic_speed, int stop_speed, double Tacc, double Tdec)
        {
            this.DRVA(CardNum, axis_num, target_position, active_speed, basic_speed, stop_speed, Tacc, Tdec, 0, enum_Axis_SpeedMode.T_Mode);
        }
        public void DRVA(int CardNum, int axis_num, int target_position, int active_speed, int basic_speed, int stop_speed, double Tacc)
        {
            this.DRVA(CardNum, axis_num, target_position, active_speed, basic_speed, stop_speed, Tacc, Tacc, 0, enum_Axis_SpeedMode.T_Mode);
        }
        public void DRVA(int CardNum, int axis_num, int target_position, int active_speed, int basic_speed, int stop_speed, double Tacc, double Tdec, double Tsdec)
        {
            this.DRVA(CardNum, axis_num, target_position, active_speed, basic_speed, stop_speed, Tacc, Tacc, Tsdec, enum_Axis_SpeedMode.S_Mode);
        }
        private void DRVA(int CardNum, int axis_num, int target_position, int active_speed, int basic_speed, int stop_speed, double Tacc, double Tdec, double Tsdec, enum_Axis_SpeedMode Axis_SpeedMode)
        {
            if (Axis_SpeedMode == DMC3000.enum_Axis_SpeedMode.T_Mode)
            {
                Dmc3000.dmc_set_profile((ushort)CardNum, (ushort)axis_num, basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D, stop_speed);
                Dmc3000.dmc_set_s_profile((ushort)CardNum, (ushort)axis_num, 0, 0);
                Dmc3000.dmc_reset_target_position((ushort)CardNum, (ushort)axis_num, target_position, 0);
                Dmc3000.dmc_update_target_position((ushort)CardNum, (ushort)axis_num, target_position, 0);
                //Dmc3000.dmc_pmove((ushort)CardNum, (ushort)axis_num, target_position, 1);

            }
            else if (Axis_SpeedMode == DMC3000.enum_Axis_SpeedMode.S_Mode)
            {
                Dmc3000.dmc_set_profile((ushort)CardNum, (ushort)axis_num, basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D, stop_speed);
                Dmc3000.dmc_set_s_profile((ushort)CardNum, (ushort)axis_num, 0, Tsdec / 1000D);
                Dmc3000.dmc_reset_target_position((ushort)CardNum, (ushort)axis_num, target_position, 0);
                Dmc3000.dmc_update_target_position((ushort)CardNum, (ushort)axis_num, target_position, 0);
                //Dmc3000.dmc_pmove((ushort)CardNum, (ushort)axis_num, target_position, 1);
            }
        }

        public void DRVI(int CardNum, int axis_num, int target_position, int active_speed, int basic_speed, int stop_speed, double Tacc, double Tdec)
        {
            this.DRVI(CardNum, axis_num, target_position, active_speed, basic_speed, stop_speed, Tacc, Tdec, 0, enum_Axis_SpeedMode.T_Mode);
        }
        public void DRVI(int CardNum, int axis_num, int target_position, int active_speed, int basic_speed, int stop_speed, double Tacc)
        {
            this.DRVI(CardNum, axis_num, target_position, active_speed, basic_speed, stop_speed, Tacc, Tacc, 0, enum_Axis_SpeedMode.T_Mode);
        }
        public void DRVI(int CardNum, int axis_num, int target_position, int active_speed, int basic_speed, int stop_speed, double Tacc, double Tdec, double Tsdec)
        {
            this.DRVI(CardNum, axis_num, target_position, active_speed, basic_speed, stop_speed, Tacc, Tacc, Tsdec, enum_Axis_SpeedMode.S_Mode);
        }
        private void DRVI(int CardNum, int axis_num, int target_position, int active_speed, int basic_speed, int stop_speed, double Tacc, double Tdec, double Tsdec, enum_Axis_SpeedMode Axis_SpeedMode)
        {
            if (Axis_SpeedMode == DMC3000.enum_Axis_SpeedMode.T_Mode)
            {
                Dmc3000.dmc_set_profile((ushort)CardNum, (ushort)axis_num, basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D, stop_speed);
                Dmc3000.dmc_set_s_profile((ushort)CardNum, (ushort)axis_num, 0, 0);

                Dmc3000.dmc_pmove((ushort)CardNum, (ushort)axis_num, target_position, 0);

            }
            else if (Axis_SpeedMode == DMC3000.enum_Axis_SpeedMode.S_Mode)
            {
                Dmc3000.dmc_set_profile((ushort)CardNum, (ushort)axis_num, basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D, stop_speed);
                Dmc3000.dmc_set_s_profile((ushort)CardNum, (ushort)axis_num, 0, Tsdec / 1000D);

                Dmc3000.dmc_pmove((ushort)CardNum, (ushort)axis_num, target_position, 0);
            }
        }

        public void DRVA_Liner(int CardNum, int[] axis_num, int[] pos, int active_speed, int basic_speed, int stop_speed, double Tacc, double Tdec)
        {
            if (axis_num.Length == pos.Length)
            {
                ushort[] ushort_axis_num = new ushort[axis_num.Length];
                Dmc3000.dmc_set_vector_profile_multicoor((ushort)CardNum, 0, basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D, stop_speed);
                for (int i = 0; i < ushort_axis_num.Length; i++)
                {
                    ushort_axis_num[i] = (ushort)axis_num[i];
                }
                Dmc3000.dmc_line_multicoor((ushort)CardNum, 0, (ushort)ushort_axis_num.Length, ushort_axis_num, pos, 1);
            }
        }
        public void DRVI_Liner(int CardNum, int[] axis_num, int[] pos, int active_speed, int basic_speed, int stop_speed, double Tacc, double Tdec)
        {
            if (axis_num.Length == pos.Length)
            {
                ushort[] ushort_axis_num = new ushort[axis_num.Length];
                Dmc3000.dmc_set_vector_profile_multicoor((ushort)CardNum, 0, basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D, stop_speed);
                for (int i = 0; i < ushort_axis_num.Length; i++)
                {
                    ushort_axis_num[i] = (ushort)axis_num[i];
                }
                Dmc3000.dmc_line_multicoor((ushort)CardNum, 0, (ushort)ushort_axis_num.Length, ushort_axis_num, pos, 0);
            }
        }

        public void DRVA_Arc(int CardNum, int[] axis_num, int[] pos, int[] center, int active_speed, int basic_speed, int stop_speed, double Tacc, double Tdec, enum_Arc_DIR enum_Arc_DIR)
        {
            ushort[] ushort_axis_num = new ushort[axis_num.Length];
            Dmc3000.dmc_set_vector_profile_multicoor((ushort)CardNum, 0, basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D, stop_speed);
            for (int i = 0; i < ushort_axis_num.Length; i++)
            {
                ushort_axis_num[i] = (ushort)axis_num[i];
            }
            Dmc3000.dmc_arc_move_multicoor((ushort)CardNum, 0, ushort_axis_num, pos, center, (ushort)enum_Arc_DIR, 1);
        }
        public void DRVI_Arc(int CardNum, int[] axis_num, int[] pos, int[] center, int active_speed, int basic_speed, int stop_speed, double Tacc, double Tdec, enum_Arc_DIR enum_Arc_DIR)
        {
            ushort[] ushort_axis_num = new ushort[axis_num.Length];
            Dmc3000.dmc_set_vector_profile_multicoor((ushort)CardNum, 0, basic_speed, active_speed, Tacc / 1000D, Tdec / 1000D, stop_speed);
            for (int i = 0; i < ushort_axis_num.Length; i++)
            {
                ushort_axis_num[i] = (ushort)axis_num[i];
            }
            Dmc3000.dmc_arc_move_multicoor((ushort)CardNum, 0, ushort_axis_num, pos, center, (ushort)enum_Arc_DIR, 1);
        }

        public void Set_Command_Pos(int CardNum, int axis_num, int pos)
        {
            Dmc3000.dmc_set_position((ushort)CardNum, (ushort)axis_num, pos);
        }
        public int Get_Command_Pos(int CardNum, int axis_num)
        {
            return Dmc3000.dmc_get_position((ushort)CardNum, (ushort)axis_num);
        }
        public bool Get_Axis_Ready(int CardNum, int axis_num)
        {
            return (Dmc3000.dmc_check_done((ushort)CardNum, (ushort)axis_num) == 1);
        }

        public void Reset_Target_Position(int CardNum, int axis_num, int pos)
        {
            Dmc3000.dmc_reset_target_position((ushort)CardNum, (ushort)axis_num, pos, 0);
        }

        public void AxisStop(int CardNum, int axis_num)
        {
            Dmc3000.dmc_stop((ushort)CardNum, (ushort)axis_num, 0);
        }
        public void AxisStopEmg(int CardNum, int axis_num)
        {
            Dmc3000.dmc_stop((ushort)CardNum, (ushort)axis_num, 1);
        }

        public void Set_PLS_Mode(int CardNum, int axis_num, enum_Axis_PLSMode Axis_PLSMode)
        {
            if (Axis_PLSMode == enum_Axis_PLSMode.pulse_dir)
            {
                Dmc3000.dmc_set_pulse_outmode((ushort)CardNum, (ushort)axis_num, 1);
            }
            else if (Axis_PLSMode == enum_Axis_PLSMode.cw_ccw)
            {
                Dmc3000.dmc_set_pulse_outmode((ushort)CardNum, (ushort)axis_num, 5);
            }
        }
        public void Set_SrvOn_PIN(int CardNum, int axis_num, bool statu)
        {
            Dmc3000.dmc_write_sevon_pin((ushort)CardNum, (ushort)axis_num, statu ? (ushort)0 : (ushort)1);
        }
        public bool Get_SrvOn_PIN(int CardNum, int axis_num)
        {
            return (Dmc3000.dmc_read_sevon_pin((ushort)CardNum, (ushort)axis_num) == 0);
        }
        public void Set_ERC_PIN(int CardNum, int axis_num, bool statu)
        {
            Dmc3000.dmc_write_erc_pin((ushort)CardNum, (ushort)axis_num, statu ? (ushort)0 : (ushort)1);
        }
        public bool Get_ERC_PIN(int CardNum, int axis_num)
        {
            return (Dmc3000.dmc_read_erc_pin((ushort)CardNum, (ushort)axis_num) == 0);
        }
        public bool Get_RDY_PIN(int CardNum, int axis_num)
        {
            return (Dmc3000.dmc_read_rdy_pin((ushort)CardNum, (ushort)axis_num) == 1);
        }

        public void Set_ALM_PIN_Mode(int CardNum, int axis_num, bool enable, enum_ALM_PIN_Mode enum_ALM_PIN_Mode)
        {
            this.Set_ALM_PIN_Mode(CardNum, axis_num, enable, (ushort)enum_ALM_PIN_Mode);
        }
        public void Set_ALM_PIN_Mode(int CardNum, int axis_num, bool enable, ushort logic)
        {
            Dmc3000.dmc_set_alm_mode((ushort)CardNum, (ushort)axis_num, enable ? (ushort)1 : (ushort)0, logic, 0);
        }

        public void Set_EZ_PIN_Mode(int CardNum, int axis_num, enum_EZ_PIN_Mode enum_EZ_PIN_Mode)
        {
            this.Set_EZ_PIN_Mode(CardNum, axis_num, (ushort)enum_EZ_PIN_Mode);
        }
        public void Set_EZ_PIN_Mode(int CardNum, int axis_num, ushort logic)
        {
            Dmc3000.dmc_set_ez_mode((ushort)CardNum, (ushort)axis_num, logic, 0, 0);
        }

        public void Set_EL_PIN_Mode(int CardNum, int axis_num, enum_EL_PIN_Enable enum_EL_PIN_Enable, enum_EL_PIN_Logic enum_EL_PIN_Logic, enum_EL_PIN_Mode enum_EL_PIN_Mode)
        {
            this.Set_EL_PIN_Mode(CardNum, axis_num, (ushort)enum_EL_PIN_Enable, (ushort)enum_EL_PIN_Logic, (ushort)enum_EL_PIN_Mode);
        }
        public void Set_EL_PIN_Mode(int CardNum, int axis_num, ushort enable, ushort logic, ushort mode)
        {
            Dmc3000.dmc_set_el_mode((ushort)CardNum, (ushort)axis_num, enable, logic, mode);
        }

        public void Set_LTC_PIN_Mode(int CardNum, int axis_num, enum_LTC_PIN_Mode enum_LTC_PIN_Mode)
        {
            this.Set_LTC_PIN_Mode(CardNum, axis_num, (ushort)enum_LTC_PIN_Mode);
        }
        public void Set_LTC_PIN_Mode(int CardNum, int axis_num, ushort logic)
        {
            Dmc3000.dmc_set_ltc_mode((ushort)CardNum, (ushort)axis_num, logic, 0, 0);
        }

        public void Set_EMG_PIN_Mode(int CardNum, int axis_num, bool enable, enum_EMG_PIN_Mode enum_EMG_PIN_Mode)
        {
            this.Set_EMG_PIN_Mode(CardNum, axis_num, enable, (ushort)enum_EMG_PIN_Mode);
        }
        public void Set_EMG_PIN_Mode(int CardNum, int axis_num, bool enable, ushort logic)
        {
            Dmc3000.dmc_set_emg_mode((ushort)CardNum, (ushort)axis_num, enable ? (ushort)1 : (ushort)0, logic);
        }

        public void Set_INP_PIN_Mode(int CardNum, int axis_num, bool enable, enum_INP_PIN_Mode enum_INP_PIN_Mode)
        {
            this.Set_INP_PIN_Mode(CardNum, axis_num, enable, (ushort)enum_INP_PIN_Mode);
        }
        public void Set_INP_PIN_Mode(int CardNum, int axis_num, bool enable, ushort logic)
        {
            Dmc3000.dmc_set_inp_mode((ushort)CardNum, (ushort)axis_num, enable ? (ushort)1 : (ushort)0, logic);
        }

        public UInt32 Get_Axis_Input(int CardNum, int axis_num)
        {
            return Dmc3000.dmc_axis_io_status((ushort)CardNum, (ushort)axis_num);
        }
        public bool Get_Axis_Input(int CardNum, int axis_num, enum_Axis_io_status enum_Axis_io_status)
        {
            return myConvert.UInt32GetBit(Dmc3000.dmc_axis_io_status((ushort)CardNum, (ushort)axis_num), (int)enum_Axis_io_status);
        }

        public System.Collections.BitArray Get_Axis_Input_BitArray(int CardNum, int axis_num)
        {
            uint status = Dmc3000.dmc_axis_io_status((ushort)CardNum, (ushort)axis_num);
            return new System.Collections.BitArray(BitConverter.GetBytes(status));
        }

        public bool Get_Input(int CardNum, int bit_num)
        {
            return (Dmc3000.dmc_read_inbit((ushort)CardNum, (ushort)bit_num) == 0);
        }
        public UInt32 Get_InputPort(int CardNum)
        {
            return this.Get_InputPort(CardNum, 0);
        }
        public UInt32 Get_InputPort(int CardNum, int portNo)
        {
            return Dmc3000.dmc_read_inport((ushort)CardNum, (ushort)portNo);
        }
        public System.Collections.BitArray Get_InputPort_BitArray(int CardNum, int portNo)
        {
            return new System.Collections.BitArray(BitConverter.GetBytes(Dmc3000.dmc_read_inport((ushort)CardNum, (ushort)portNo)));
        }

        public bool Get_Output(int CardNum, int bit_num)
        {
            return (Dmc3000.dmc_read_outbit((ushort)CardNum, (ushort)bit_num) == 0);
        }
        public UInt32 Get_Output_Port(int CardNum)
        {
            return Dmc3000.dmc_read_outport((ushort)CardNum, 0);
        }
        public System.Collections.BitArray Get_OutputPort_BitArray(int CardNum)
        {
            return new System.Collections.BitArray(BitConverter.GetBytes(Dmc3000.dmc_read_outport((ushort)CardNum, (ushort)0)));
        }


        public void Set_Output(int CardNum, int bit_num, bool statu)
        {
            string adress = List_DMC3000_Basic[CardNum].Get_Output_Adress(bit_num);
            PLC.properties.Device.Set_DeviceFast_Ex(adress, statu);
            this.List_DMC3000_Basic[CardNum].Set_Output(bit_num, statu);
            Dmc3000.dmc_write_outbit((ushort)CardNum, (ushort)bit_num, !statu ? (ushort)1 : (ushort)0);
        }
        public void Set_Output_Port(int CardNum, uint port_value)
        {
            Dmc3000.dmc_write_outport((ushort)CardNum, 0, port_value);
        }

        public void Set_Counter_Mode(int CardNum, int axis_num, enum_Counter_Mode enum_Counter_Mode)
        {
            Dmc3000.dmc_set_counter_inmode((ushort)CardNum, (ushort)axis_num, (ushort)enum_Counter_Mode);
        }
        public int Get_Counter(int CardNum, int axis_num)
        {
            return Dmc3000.dmc_get_encoder((ushort)CardNum, (ushort)axis_num);
        }
        public void Set_Counter(int CardNum, int axis_num, int value)
        {
            Dmc3000.dmc_set_encoder((ushort)CardNum, (ushort)axis_num, value);
        }

        public void Set_Normal_CMP_Mode(int CardNum, bool enable, int axis_num, enum_Normal_CMP_Source enum_Normal_CMP_Source)
        {
            Dmc3000.dmc_compare_set_config((ushort)CardNum, (ushort)axis_num, enable ? (ushort)1 : (ushort)0, (ushort)enum_Normal_CMP_Source);
        }
        public void Get_Normal_CMP_Mode(int CardNum, int axis_num, ref bool enable, ref enum_Normal_CMP_Source enum_Normal_CMP_Source)
        {
            ushort enable_temp = 0;
            ushort enum_Normal_CMP_Source_temp = 0;
            Dmc3000.dmc_compare_get_config((ushort)CardNum, (ushort)axis_num, ref enable_temp, ref enum_Normal_CMP_Source_temp);

            if (enable_temp == 0) enable = false;
            else if (enable_temp == 1) enable = true;


            if (enum_Normal_CMP_Source_temp == 0) enum_Normal_CMP_Source = DMC3000.enum_Normal_CMP_Source.COMMAND;
            else if (enum_Normal_CMP_Source_temp == 1) enum_Normal_CMP_Source = DMC3000.enum_Normal_CMP_Source.ENCODER;
        }

        public void Set_Normal_CMP_Clear(int CardNum, int axis_num)
        {
            Dmc3000.dmc_compare_clear_points((ushort)CardNum, (ushort)axis_num);
        }
        public void Set_Normal_CMP_add_point(int CardNum, int axis_num, int pos, enum_Normal_CMP_Dir enum_Normal_CMP_Dir, enum_Normal_CMP_Action enum_Normal_CMP_Action, uint actpara)
        {
            Dmc3000.dmc_compare_add_point((ushort)CardNum, (ushort)axis_num, pos, (ushort)enum_Normal_CMP_Dir, (ushort)enum_Normal_CMP_Action, actpara);
        }
        public void Get_Normal_CMP_Current_point(int CardNum, int axis_num, ref int pos)
        {
            Dmc3000.dmc_compare_get_current_point((ushort)CardNum, (ushort)axis_num, ref pos);
        }
        public void Get_Normal_CMP_points_runned(int CardNum, int axis_num, ref int PointNum)
        {
            Dmc3000.dmc_compare_get_points_runned((ushort)CardNum, (ushort)axis_num, ref PointNum);
        }
        public void Get_Normal_CMP_points_remained(int CardNum, int axis_num, ref int PointNum)
        {
            Dmc3000.dmc_compare_get_points_remained((ushort)CardNum, (ushort)axis_num, ref PointNum);
        }



        public void Set_HighSpeed_CMP_Mode(int CardNum, int axis_num, enum_HighSpeed_CMP_Mode enum_HighSpeed_CMP_Mode)
        {
            Dmc3000.dmc_hcmp_set_mode((ushort)CardNum, (ushort)axis_num, (ushort)enum_HighSpeed_CMP_Mode);
        }
        public void Get_HighSpeed_CMP_Mode(int CardNum, int axis_num, ref enum_HighSpeed_CMP_Mode enum_HighSpeed_CMP_Mode)
        {
            ushort ushort_enum_HighSpeed_CMP_Mode = 0;
            Dmc3000.dmc_hcmp_get_mode((ushort)CardNum, (ushort)axis_num, ref ushort_enum_HighSpeed_CMP_Mode);
            enum_HighSpeed_CMP_Mode = (enum_HighSpeed_CMP_Mode)ushort_enum_HighSpeed_CMP_Mode;
        }
        public void Set_HighSpeed_CMP_Config(int CardNum, int CMP_Num, int axis_num, enum_HighSpeed_CMP_Source enum_HighSpeed_CMP_Source, enum_HighSpeed_CMP_Logic enum_HighSpeed_CMP_Logic, int paulse_width)
        {
            Dmc3000.dmc_hcmp_set_config((ushort)CardNum, (ushort)CMP_Num, (ushort)axis_num, (ushort)enum_HighSpeed_CMP_Source, (ushort)enum_HighSpeed_CMP_Logic, paulse_width * 1000);
        }
        public void Get_HighSpeed_CMP_Config(int CardNum, int CMP_Num, ref int axis_num, ref enum_HighSpeed_CMP_Source enum_HighSpeed_CMP_Source, ref enum_HighSpeed_CMP_Logic enum_HighSpeed_CMP_Logic, ref int paulse_width)
        {
            ushort ushort_enum_HighSpeed_CMP_Source = 0;
            ushort ushort_enum_HighSpeed_CMP_Logic = 0;
            ushort ushort_axis_num = 0;
            Dmc3000.dmc_hcmp_get_config((ushort)CardNum, (ushort)CMP_Num, ref ushort_axis_num, ref ushort_enum_HighSpeed_CMP_Source, ref ushort_enum_HighSpeed_CMP_Logic, ref paulse_width);
            axis_num = ushort_axis_num;
            enum_HighSpeed_CMP_Source = (enum_HighSpeed_CMP_Source)ushort_enum_HighSpeed_CMP_Source;
            enum_HighSpeed_CMP_Logic = (enum_HighSpeed_CMP_Logic)ushort_enum_HighSpeed_CMP_Logic;
        }
        public void Set_HighSpeed_CMP_Clear(int CardNum, int CMP_Num)
        {
            Dmc3000.dmc_hcmp_clear_points((ushort)CardNum, (ushort)CMP_Num);
        }
        public void Set_HighSpeed_CMP_add_point(int CardNum, int CMP_Num, int pos)
        {
            Dmc3000.dmc_hcmp_add_point((ushort)CardNum, (ushort)CMP_Num, pos);
        }
        public void Set_HighSpeed_CMP_liner_config(int CardNum, int CMP_Num, int Inc_Value, int cnt)
        {
            Dmc3000.dmc_hcmp_set_liner((ushort)CardNum, (ushort)CMP_Num, Inc_Value, cnt);
        }
        public void Get_HighSpeed_CMP_liner_config(int CardNum, int CMP_Num, ref int Inc_Value, ref int cnt)
        {
            Dmc3000.dmc_hcmp_get_liner((ushort)CardNum, (ushort)CMP_Num, ref Inc_Value, ref cnt);
        }
        public void Get_HighSpeed_CMP_current_state(int CardNum, int CMP_Num, ref int remained_points, ref int current_point, ref int runned_points)
        {
            Dmc3000.dmc_hcmp_get_current_state((ushort)CardNum, (ushort)CMP_Num, ref remained_points, ref current_point, ref runned_points);
        }
        public void Set_CMP_PIN(int CardNum, int CMP_Num, bool statu)
        {
            Dmc3000.dmc_write_cmp_pin((ushort)CardNum, (ushort)CMP_Num, statu ? (ushort)1 : (ushort)0);
        }
        public bool Get_CMP_PIN(int CardNum, int CMP_Num, bool statu)
        {
            return (Dmc3000.dmc_read_cmp_pin((ushort)CardNum, (ushort)CMP_Num) == 1);
        }
        #endregion
        #region Event
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.IsOpen)
            {
                this.BoardClose();
                //  this.SaveProperties();
            }
        }
        private void plC_Button_Open_btnClick(object sender, EventArgs e)
        {
            if (!this.IsOpen) this.BoardOpen();
            else this.BoardClose();
        }
        #endregion

        #region StreamIO
        private List<object> List_SaveClass = new List<object>();
        public void SaveProperties()
        {
            this.List_SaveClass.Clear();
            this.StreamName = @".\\DMC3000\\" + _設備名稱 + ".pro";
            for (int i = 0; i < Card_count; i++)
            {
                this.List_SaveClass.Add(List_DMC3000_Basic[i].GetSaveObject());
            }
            Basic.FileIO.SaveProperties(this.List_SaveClass, this.StreamName);
        }
        public void LoadProperties()
        {
            object temp = new object();
            this.List_SaveClass.Clear();
            this.StreamName = @".\\DMC3000\\" + _設備名稱 + ".pro";
            Basic.FileIO.LoadProperties(ref temp, StreamName);
            if (temp is List<object>)
            {
                this.List_SaveClass = (List<object>)temp;
            }
            for (int i = 0; i < Card_count; i++)
            {
                if (i < this.List_SaveClass.Count)
                {
                    this.List_DMC3000_Basic[i].LoadObject((DMC3C00_Basic.SaveClass)this.List_SaveClass[i]);
                }
            }
        }
        #endregion


    }
}
