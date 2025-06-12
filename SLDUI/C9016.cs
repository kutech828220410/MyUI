using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using ESLDM;
using LadderUI;
using LadderConnection;
using MyUI;
using Basic;

namespace SLDUI
{
    [System.Drawing.ToolboxBitmap(typeof(C9016), "PCI.bmp")]
    public partial class C9016 : UserControl
    {
        private string _設備名稱 = "C9016-001";
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
        const int chanel_num = 2;
        const int Axis_num = 6;
        const int HighSpeedCounter_num = 6;
        private bool FLAG_C9016_Isopen = false;
        private bool FLAG_first_init = true;
        private int Card_count = 0;
        private int[] card_id = new int[16];
        private TabControl tabControl = new TabControl();
        private TabPage[] tabPage;
        private SLDUI.C9016_Basic[] c9016_Basic;
        private MyThread Thread00;
        private MyThread Thread01;
        private Form Active_Form;
        private bool RefreshUI_Enable = true;
        private String StreamName;
        private LowerMachine PLC;
        private MyConvert myConvert = new MyConvert();
        private List<ExButton> List_ExButton = new List<ExButton>();

        private List<NumWordTextBox[,]> NumWordTextBox_Input = new List<NumWordTextBox[,]>();
        public List<string[,]> Input_position = new List<string[,]>();
        private List<ExButton[,]> ExButton_Input = new List<ExButton[,]>();
        private List<bool[,]> Input = new List<bool[,]>();

        private List<NumWordTextBox[,]> NumWordTextBox_Output = new List<NumWordTextBox[,]>();
        public List<string[,]> Output_position = new List<string[,]>();
        private List<ExButton[,]> ExButton_Output = new List<ExButton[,]>();
        private List<bool[,]> Output = new List<bool[,]>();


        private List<NumTextBox[,]> NumTextBox_Axis_對應軸號 = new List<NumTextBox[,]>();
        private List<int[,]> Axis_對應軸號 = new List<int[,]>();

        private List<NumWordTextBox[,]> NumWordTextBox_Axis_Input = new List<NumWordTextBox[,]>();
        private List<string[,]> Axis_Input_position = new List<string[,]>();
        private List<ExButton[,]> ExButton_Axis_Input = new List<ExButton[,]>();
        private List<bool[,]> Axis_Input = new List<bool[,]>();
        static private class Axis輸入
        {
            static public int 正極限 = 0;
            static public int 負極限 = 1;
            static public int 原點 = 2;

        }       
        private List<ExButton[,]> ExButton_Axis_status = new List<ExButton[,]>();
        private List<bool[,]> Axis_status = new List<bool[,]>();
        private List<bool[,]> Axis_status_buf = new List<bool[,]>();
        static private class Axis狀態
        {
            static public string device_type = "M";
            static public int device_num = 8340;
            static public int Axis_Busy = 0;
            static public int 更改位置致能 = 1;
            static public int JOG加減速致能 = 2;
            static public int 脈衝輸出模式 = 3;
            static public int 正極限致能 = 4;
            static public int 負極限致能 = 5;
            static public int 高速計數器致能 = 6;
            static public int 位置比較輸出致能 = 7;
            static public int 位置比較方式 = 8;
            static public int 電子齒輪比致能 = 9;
        }

        private List<NumTextBox[,]> NumTextBox_Axis_運動參數 = new List<NumTextBox[,]>();
        private List<int[,]> Axis_運動參數 = new List<int[,]>();
        private List<int[,]> Axis_運動參數_buf = new List<int[,]>();

        private List<bool> Axis_PC_Enable = new List<bool>();
        static private class 運動參數
        {
            static public string device_type = "D";
            static public int device_num = 8340;
            static public int 現在位置 = 0;
            static public int 齒輪比_分子 = 2;
            static public int 齒輪比_分母 = 3;
            static public int 運轉目標位置 = 4;
            static public int 基底速度 = 5;
            static public int 運動命令碼 = 6;
            static public int 運轉速度 = 7;
            static public int 加速度 = 8;
            static public int 減速度 = 9;
        }

        private List<NumTextBox[,]> NumTextBox_Axis_高速輸出入參數 = new List<NumTextBox[,]>();
        private List<int[,]> Axis_高速輸出入參數 = new List<int[,]>();
        static private class 高速輸出入參數
        {
            static public string device_type = "R";
            static public int device_num = 8340;
            static public int 現在位置 = 0;
            static public int 比較位置數量 = 1;
            static public int 比較位置_1 = 2;
            static public int 比較位置_2 = 3;
            static public int 比較位置_3 = 4;
            static public int 比較位置_4 = 5;
            static public int 比較位置_5 = 6;
            static public int 比較位置_6 = 7;
            static public int 比較位置_7 = 8;
            static public int 比較輸出位置 = 9;
        }

        public C9016()
        {
            InitializeComponent();
            button_Save.Click += Button_Save_Click;
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            SaveProperties();
            MyMessageBox.ShowDialog("Save sucessfully!");
        }

        delegate void UI_Visible_Delegate(bool visble);
        private void sub_UI_Visible(bool visble)
        {
            this.Visible = visble;
        }
        public void UI_Visible(bool visble)
        {
            UI_Visible_Delegate Delegate = new UI_Visible_Delegate(sub_UI_Visible);
            Invoke(Delegate, visble);
            RefreshUI_Enable = visble;

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
           // RefreshUI_Enable = this.Visible;
        
            List_ExButton.Add(exButton_Open);
     
            foreach (MyUI.ExButton ExButton_temp in List_ExButton) ExButton_temp.Run(PLC);
            if (cnt_Open_C9016 == 255)
            {
                FLAG_first_init = true;
                cnt_Open_C9016 = 1;
            }
            init();
        }

        private delegate void tabPageDelegate(int Card_count, int[] card_id);
        void TabPageDelegate(int Card_count, int[] card_id)
        {
            tabPage = new TabPage[Card_count];
            c9016_Basic = new C9016_Basic[Card_count];
            for (int i = 0; i < Card_count; i++)
            {
                c9016_Basic[i] = new C9016_Basic();
                c9016_Basic[i].Dock = System.Windows.Forms.DockStyle.Fill;
                c9016_Basic[i].Location = new System.Drawing.Point(3, 3);
                c9016_Basic[i].Name = "c9016_Basic_" + i.ToString();
                c9016_Basic[i].Size = new System.Drawing.Size(569, 429);
                c9016_Basic[i].TabIndex = 0;

                tabPage[i] = new TabPage();
                tabPage[i].Controls.Add(c9016_Basic[i]);
                tabPage[i].Location = new System.Drawing.Point(4, 22);
                tabPage[i].Name = "tabPage_card_" + i.ToString();
                tabPage[i].Padding = new System.Windows.Forms.Padding(3);
                tabPage[i].Size = new System.Drawing.Size(192, 74);
                tabPage[i].TabIndex = i;
                tabPage[i].Text = "CARD-" + card_id[i].ToString();
                tabPage[i].UseVisualStyleBackColor = true;
            }
      
        }
        void tabPage_init(int Card_count, int[] card_id)
        {
            tabPageDelegate tabPagedelegate = new tabPageDelegate(TabPageDelegate);
            Invoke(tabPagedelegate, Card_count, card_id);
        }
        private delegate void tabControlDelegate();
        void TabControlDelegate()
        {
            
            this.tabControl = new TabControl();
            tabControl.SuspendLayout();
            for (int i = 0; i < tabPage.Length; i++)
            {
                this.tabControl.Controls.Add(this.tabPage[i]);
            }
            this.tabControl.Location = new System.Drawing.Point(0, panel_Open.Size.Height);
            this.tabControl.Name = "tabControl_C9016";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(panel_Open.Width, this.Width - panel_Open.Size.Height);
            this.tabControl.TabIndex = 11;

            this.Controls.Add(this.tabControl);
            tabControl.ResumeLayout(false);
        }
        void tabControl_init()
        {
            tabControlDelegate tabControlelegate = new tabControlDelegate(TabControlDelegate);
            Invoke(tabControlelegate);
        }
        private void init()
        {
              LoadProperties();
              PLC.Add_UI_Method(DoWork_UI);
              Thread00 = new MyThread("", FindForm());
              Thread00.Add_Method(DoWork_complete);
              Thread00.AutoRun(true);
              Thread00.SetSleepTime(CycleTime);

              Thread01 = new MyThread("", FindForm());
              Thread01.Add_Method(DoWork_strat);
              Thread01.AutoRun(true);
              Thread01.SetSleepTime(CycleTime);

              Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
        }

        private void DoWork_strat()
        {
            sub_Get_Input();
            sub_WriteToPLC();
        }
        private void DoWork_complete()
        {
            sub_ReadFromPLC();
            sub_Set_Output();
            sub_Run_CompareOut();
            sub_Get_Axis_Status();
            sub_Run_Axis();
            sub_Get_HighSpeedCounter();
            sub_Run_HighSpeedCounter();    
        }
        private void DoWork_UI()
        {
            sub_Open_C9016();
            sub_Close_C9016();
            if (RefreshUI_Enable) RefreshUI();

        }
        private void RefreshUI()
        {
            exButton_Open.Set_LoadState(FLAG_C9016_Isopen);
            Thread01.GetCycleTime(300, label_CycleTime);

            if (FLAG_C9016_Isopen)
            {
                for (int N = 0; N < Card_count; N++)
                {
                    for (int k = 0; k < chanel_num; k++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            ExButton_Input[N][k, i].Set_LoadState(Input[N][k, i]);
                            ExButton_Output[N][k, i].Set_LoadState(Output[N][k, i]);
                        }
                    }
                    for (int k = 0; k < Axis_num; k++)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ExButton_Axis_Input[N][k, i].Set_LoadState(Axis_Input[N][k, i]);
                        }
                    }
                    for (int k = 0; k < Axis_num; k++)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            ExButton_Axis_status[N][k, i].Set_LoadState(Axis_status[N][k, i]);
                            CallBackUI.numTextBox.字串更換(Axis_運動參數[N][k, i].ToString(), NumTextBox_Axis_運動參數[N][k, i]);
                            CallBackUI.numTextBox.字串更換(Axis_高速輸出入參數[N][k, i].ToString(), NumTextBox_Axis_高速輸出入參數[N][k, i]);

                        }
                    }
                }
            }
        }

        #region Open_C9016
        byte cnt_Open_C9016 = 255;
        void sub_Open_C9016()
        {
            if (cnt_Open_C9016 == 1) cnt_Open_C9016_00_檢查控制器是否開啟(ref cnt_Open_C9016);
            if (cnt_Open_C9016 == 2) cnt_Open_C9016_00_檢查連接狀態(ref cnt_Open_C9016);
            if (cnt_Open_C9016 == 3) cnt_Open_C9016_00_開啟板卡(ref cnt_Open_C9016);
            if (cnt_Open_C9016 == 4) cnt_Open_C9016 = 240;

            if (cnt_Open_C9016 == 240) cnt_Open_C9016_240_開啟成功(ref cnt_Open_C9016);
            if (cnt_Open_C9016 == 241) cnt_Open_C9016 = 255;

            if (cnt_Open_C9016 == 250) cnt_Open_C9016_250_開啟失敗(ref cnt_Open_C9016);
            if (cnt_Open_C9016 == 251) cnt_Open_C9016 = 255;

        }
        void cnt_Open_C9016_00_檢查控制器是否開啟(ref byte cnt)
        {
            FLAG_C9016_Isopen = false;
            for (int i = 0; i < card_id.Length; i++)
            {
                card_id[i] = 999;
            }
            cnt++;
        }
        void cnt_Open_C9016_00_檢查連接狀態(ref byte cnt)
        {
            cnt++;
        }
        void cnt_Open_C9016_00_開啟板卡(ref byte cnt)
        {
            if(Pci9016.CPci9016.p9016_initial(ref Card_count, card_id) == 0)
            {
                cnt++;
            }
   
           
        }
        void cnt_Open_C9016_240_開啟成功(ref byte cnt)
        {       
            if(FLAG_first_init)
            {
                tabPage_init(Card_count, card_id);
                tabControl_init();
                for (int i = 0; i < Card_count; i++)
                {
                
                    NumWordTextBox_Input.Add(c9016_Basic[i].Get_Input_NumWordTextBox());
                    ExButton_Input.Add(c9016_Basic[i].Get_Input_ExButton());
                    if (Input_position.Count < Card_count) Input_position.Add(c9016_Basic[i].Get_Input_position());
                    Input.Add(c9016_Basic[i].Get_Input());

                    NumWordTextBox_Output.Add(c9016_Basic[i].Get_Output_NumWordTextBox());
                    ExButton_Output.Add(c9016_Basic[i].Get_Output_ExButton());
                    if (Output_position.Count < Card_count) Output_position.Add(c9016_Basic[i].Get_Output_position());
                    Output.Add(c9016_Basic[i].Get_Output());

                    NumTextBox_Axis_對應軸號.Add(c9016_Basic[i].Get_Axis_對應軸號_NumWordTextBox());
                    if(Axis_對應軸號.Count < Card_count)Axis_對應軸號.Add(c9016_Basic[i].Get_Axis_對應軸號());

                    NumWordTextBox_Axis_Input.Add(c9016_Basic[i].Get_Axis_Input_NumWordTextBox());
                    if (Axis_Input_position.Count < Card_count) Axis_Input_position.Add(c9016_Basic[i].Get_Axis_Input_position());
                    ExButton_Axis_Input.Add(c9016_Basic[i].Get_Axis_Input_ExButton());
                    Axis_Input.Add(c9016_Basic[i].Get_Axis_Input());

                    ExButton_Axis_status.Add(c9016_Basic[i].Get_Axis_status_ExButton());
                    Axis_status.Add(c9016_Basic[i].Get_Axis_status());
                    Axis_status_buf.Add(c9016_Basic[i].Get_Axis_status());

                    NumTextBox_Axis_運動參數.Add(c9016_Basic[i].Get_Axis_運動參數_NumWordTextBox());
                    Axis_運動參數.Add(c9016_Basic[i].Get_Axis_運動參數());
                    Axis_運動參數_buf.Add(c9016_Basic[i].Get_Axis_運動參數());

                    NumTextBox_Axis_高速輸出入參數.Add(c9016_Basic[i].Get_Axis_高速輸出入參數_NumWordTextBox());
                    Axis_高速輸出入參數.Add(c9016_Basic[i].Get_Axis_高速輸出入參數());
                }
                for (int N = 0; N < Card_count; N++)
                {

                    for (int k = 0; k < chanel_num; k++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            CallBackUI.numWordTextBox.字串更換(Input_position[N][k, i], NumWordTextBox_Input[N][k, i]);
                            CallBackUI.numWordTextBox.字串更換(Output_position[N][k, i], NumWordTextBox_Output[N][k, i]);
                        }
                    }
                    for (int k = 0; k < Axis_num; k++)
                    {
                        Axis_PC_Enable.Add(false);
                        CallBackUI.numTextBox.字串更換(Axis_對應軸號[N][k, 0].ToString(), NumTextBox_Axis_對應軸號[N][k, 0]);
                        for (int i = 0; i < 3; i++)
                        {
                            CallBackUI.numWordTextBox.字串更換(Axis_Input_position[N][k, i], NumWordTextBox_Axis_Input[N][k, i]);
                        }
                    }
                }
            }
            string temp0 = "";
            string device_type0 = "";
            for (int N = 0; N < Card_count; N++)
            {
                for (int k = 0; k < chanel_num; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        temp0 = "";
                        device_type0 = "";
                        CallBackUI.numWordTextBox.取得字串(ref temp0, NumWordTextBox_Input[N][k, i]);
                        temp0 = temp0.ToUpper();
                        if (temp0.Length > 1) device_type0 = temp0.Substring(0, 1);
                        if (!LadderProperty.DEVICE.TestDevice(temp0) || device_type0 != "X")
                        {
                            temp0 = "";
                            CallBackUI.numWordTextBox.字串更換(temp0, NumWordTextBox_Input[N][k, i]);
                      
                        }
                        Input_position[N][k, i] = temp0;
                        CallBackUI.numWordTextBox.Enable(false, NumWordTextBox_Input[N][k, i]);
                    }
                }
            }
            for (int N = 0; N < Card_count; N++)
            {
                for (int k = 0; k < chanel_num; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        temp0 = "";
                        device_type0 = "";
                        CallBackUI.numWordTextBox.取得字串(ref temp0, NumWordTextBox_Output[N][k, i]);
                        temp0 = temp0.ToUpper();
                        if (temp0.Length > 1) device_type0 = temp0.Substring(0, 1);
                        if (!LadderProperty.DEVICE.TestDevice(temp0) || device_type0 != "Y")
                        {
                            temp0 = "";
                            CallBackUI.numWordTextBox.字串更換( temp0, NumWordTextBox_Output[N][k, i]);
                           
                        }
                        Output_position[N][k, i] = temp0;
                        CallBackUI.numWordTextBox.Enable(false, NumWordTextBox_Output[N][k, i]);
                    }
                }
            }
            for (int N = 0; N < Card_count; N++)
            {
                for (int k = 0; k < Axis_num; k++)
                {
                    if (!FLAG_first_init)
                    {
                        CallBackUI.numTextBox.取得字串(ref temp0, NumTextBox_Axis_對應軸號[N][k, 0]);

                        if (!Int32.TryParse(temp0, out Axis_對應軸號[N][k, 0]))
                        {
                            Axis_對應軸號[N][k, 0] = 9999;
                        }
                        CallBackUI.numTextBox.字串更換(Axis_對應軸號[N][k, 0].ToString(), NumTextBox_Axis_對應軸號[N][k, 0]);
                    }
                    CallBackUI.numTextBox.Enable(false, NumTextBox_Axis_對應軸號[N][k, 0]);
                    for (int i = 0; i < 10; i++)
                    {
                        CallBackUI.numTextBox.Enable(false, NumTextBox_Axis_運動參數[N][k, i]);
                        CallBackUI.numTextBox.Enable(false, NumTextBox_Axis_高速輸出入參數[N][k, i]);
                    }
          
                }
            }
            for (int N = 0; N < Card_count; N++)
            {
                for (int k = 0; k < Axis_num; k++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        temp0 = "";
                        device_type0 = "";
                        CallBackUI.numWordTextBox.取得字串(ref temp0, NumWordTextBox_Axis_Input[N][k, i]);
                        temp0 = temp0.ToUpper();
                        if (temp0.Length > 1) device_type0 = temp0.Substring(0, 1);
                        if (!LadderProperty.DEVICE.TestDevice(temp0) || device_type0 != "X")
                        {
                            CallBackUI.numWordTextBox.字串更換("", NumWordTextBox_Axis_Input[N][k, i]);
                        }
                        else Axis_Input_position[N][k, i] = temp0;
                        CallBackUI.numWordTextBox.Enable(false, NumWordTextBox_Axis_Input[N][k, i]);
                    }
                }
            }
            Thread.Sleep(500);
            Thread00.Trigger();
            Thread01.Trigger();


            int axis_index = 0;
            int device_index = 0;
            string commemt = "";
            for (int N = 0; N < Card_count; N++)
            {
                for (int k = 0; k < Axis_num; k++)
                {
                    axis_index = Axis_對應軸號[N][k, 0];
                    if (axis_index == 9999) continue;
                    for (int m = 0; m < 10; m++)
                    {
                        if (m == 0) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "Axis_Busy";
                        if (m == 1) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "更改位置致能";
                        if (m == 2) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "JOG加減速致能";
                        if (m == 3) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "脈衝輸出模式";
                        if (m == 4) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "正極限致能";
                        if (m == 5) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "負極限致能";
                        if (m == 6) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "高速計數器致能";
                        if (m == 7) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "位置比較輸出致能";
                        if (m == 8) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "位置比較方式";
                        if (m == 9) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "電子齒輪比致能";

                        PLC.properties.Device.Set_Device("M" + ((8340 + axis_index * 10 + m)).ToString(), commemt);
                    }
                    for (int m = 0; m < 10; m++)
                    {
                        if (m == 0) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "現在位置";
                        if (m == 2) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "齒輪比_分子";
                        if (m == 3) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "齒輪比_分母";
                        if (m == 4) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "運轉目標位置";
                        if (m == 5) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "基底速度";
                        if (m == 6) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "運動命令碼";
                        if (m == 7) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "運轉速度";
                        if (m == 8) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "加速度";
                        if (m == 9) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "減速度";
                        PLC.properties.Device.Set_Device("D" + ((8340 + axis_index * 10 + m)).ToString(), commemt);
                    }
                    for (int m = 0; m < 10; m++)
                    {
                        if (m == 0) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "現在位置";
                        if (m == 1) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "比較位置數量";
                        if (m == 2) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "比較位置_1";
                        if (m == 3) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "比較位置_2";
                        if (m == 4) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "比較位置_3";
                        if (m == 5) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "比較位置_4";
                        if (m == 6) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "比較位置_5";
                        if (m == 7) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "比較位置_6";
                        if (m == 8) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "比較位置_7";
                        if (m == 9) commemt = "(C9016)" + N.ToString("00") + "-" + k.ToString("00") + " " + "比較輸出位置";

                        PLC.properties.Device.Set_Device("R" + ((8340 + axis_index * 10 + m)).ToString(), commemt);
                    }
                }
            }

            FLAG_C9016_Isopen = true;
            FLAG_first_init = false;
            cnt++;
        }
        void cnt_Open_C9016_250_開啟失敗(ref byte cnt)
        {
            FLAG_C9016_Isopen = false;
            cnt++;
        }
        void cnt_Open_C9016_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        #region Close_C9016
        byte cnt_Close_C9016 = 255;
        void sub_Close_C9016()
        {
            if (cnt_Close_C9016 == 1)
            {
                for (int N = 0; N < Card_count; N++)
                {
                    for (int k = 0; k < chanel_num; k++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            CallBackUI.numWordTextBox.Enable(true, NumWordTextBox_Input[N][k, i]);
                            CallBackUI.numWordTextBox.Enable(true, NumWordTextBox_Output[N][k, i]);
                        }
                    }
                    for (int k = 0; k < Axis_num; k++)
                    {
                        CallBackUI.numTextBox.Enable(true, NumTextBox_Axis_對應軸號[N][k, 0]);    
                        for (int i = 0; i < 3; i++)
                        {
                            CallBackUI.numWordTextBox.Enable(true, NumWordTextBox_Axis_Input[N][k, i]);                          
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            CallBackUI.numTextBox.Enable(true, NumTextBox_Axis_運動參數[N][k, i]);
                            CallBackUI.numTextBox.Enable(true, NumTextBox_Axis_高速輸出入參數[N][k, i]);
                        }
                    }
                }
                Pci9016.CPci9016.p9016_close();
                FLAG_C9016_Isopen = false;
                cnt_Close_C9016 = 255;
            }
        }
        #endregion

        #region Get_Input
        void sub_Get_Input()
        {
            if (FLAG_C9016_Isopen)
            {
                uint temp = 0;
                for (int N = 0; N < Card_count; N++)
                {
                    Pci9016.CPci9016.p9016_get_di(card_id[N], ref temp);
                    for (int k = 0; k < chanel_num; k++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            Input[N][k, i] = !myConvert.UInt32GetBit(temp, i + k * 8);
                        }
                    }
                    for (int k = 0; k < Axis_num; k++)
                    {
                        int M = card_id[N];
                        Pci9016.CPci9016.p9016_get_io_status(k + M * 6, ref temp);
                        Axis_Input[N][k, Axis輸入.正極限] = myConvert.UInt32GetBit(temp, Axis輸入.正極限);
                        Axis_Input[N][k, Axis輸入.負極限] = myConvert.UInt32GetBit(temp, Axis輸入.負極限);
                        Axis_Input[N][k, Axis輸入.原點] = myConvert.UInt32GetBit(temp, Axis輸入.原點);
                    }                   
                }       
            }
        }
        #endregion
        #region Set_Output
        void sub_Set_Output()
        {
            if (FLAG_C9016_Isopen)
            {
                uint temp = 0;
                for (int N = 0; N < Card_count; N++)
                {
                    temp = 0;
                    for (int k = 0; k < chanel_num; k++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            temp = myConvert.UInt32SetBit(!Output[N][k, i], temp, i + k * 8);
                        }
                    }
                    Pci9016.CPci9016.p9016_set_do(card_id[N], temp);
                }

            }
        }
        #endregion

        #region ReadFromPLC
        void sub_ReadFromPLC()
        {
            if (FLAG_C9016_Isopen && PLC != null)
            {
                object flag;
                string device = "";
                for (int N = 0; N < Card_count; N++)
                {
                    for (int k = 0; k < chanel_num; k++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            device = Output_position[N][k, i];
                            if (device != "" && device != null)
                            {
                                PLC.properties.device_system.Get_Device(device, out flag);
                                bool temp = (bool)flag;
                                Output[N][k, i] = temp;
                            }
                        }
                    }
                    object value;
                    for (int k = 0; k < Axis_num; k++)
                    {
                        if (Axis_對應軸號[N][k, 0] != 9999)
                        {
                            for (int i = 9; i >= 0; i--)
                            {
                                if (i == Axis狀態.Axis_Busy)
                                {
                                    continue;
                                }
                                device = Axis狀態.device_type + (Axis狀態.device_num + Axis_對應軸號[N][k, 0] * 10 + i).ToString();
                                PLC.properties.Device.Get_Device(device, out value);
                                Axis_status[N][k, i] = (bool)value;
                            }
                        }
                        for (int i = 9; i >= 0; i--)
                        {
                            device = 運動參數.device_type + (運動參數.device_num + Axis_對應軸號[N][k, 0] * 10 + i).ToString();
                            if (i == 運動參數.現在位置)
                            {
                                if (Axis_status[N][k, Axis狀態.更改位置致能])
                                {
                                    PLC.properties.Device.Get_Device(device, out value);
                                    Axis_運動參數[N][k, i] = (int)value;
                                    int M = card_id[N];
                                    Pci9016.CPci9016.p9016_set_pos(k + Axis_num * M, 0, Axis_運動參數[N][k, i]);                                       
                                }
                                continue;
                            }
                            PLC.properties.Device.Get_Device(device, out value);
                            if (value is int) Axis_運動參數[N][k, i] = (int)value;
                        }
                        for (int i = 9; i >= 0; i--)
                        {
                            device = 高速輸出入參數.device_type + (高速輸出入參數.device_num + Axis_對應軸號[N][k, 0] * 10 + i).ToString();
                            if (i == 高速輸出入參數.現在位置)
                            {
                                if (Axis_status[N][k, Axis狀態.更改位置致能])
                                {
                                    PLC.properties.Device.Get_Device(device, out value);
                                    Axis_高速輸出入參數[N][k, i] = (int)value;
                                    int M = card_id[N];
                        
                                    Pci9016.CPci9016.p9016_set_pos(k + Axis_num * M, 1, Axis_高速輸出入參數[N][k, i]);

                                }
                                continue;
                            }
                            PLC.properties.Device.Get_Device(device, out value);
                            if (value is int) Axis_高速輸出入參數[N][k, i] = (int)value;
                        }
                    }
                }
            }
        }
        #endregion
        #region WriteToPLC
        void sub_WriteToPLC()
        {
            if (FLAG_C9016_Isopen && PLC != null)
            {
                bool flag = false;
                string device = "";
                for (int N = 0; N < Card_count; N++)
                {
                    for (int k = 0; k < chanel_num; k++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            flag = Input[N][k, i];
                            device = Input_position[N][k, i];
                            if (device != "" && device != null) PLC.properties.device_system.Set_Device(device, flag);
                        }
                    }
                    for (int k = 0; k < Axis_num; k++)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag = Axis_Input[N][k, i];
                            device = Axis_Input_position[N][k, i];
                            if (device != "" && device != null) PLC.properties.device_system.Set_Device(device, flag);
                        }
                    }
                    for (int k = 0; k < Axis_num; k++)
                    {
                        if (Axis_對應軸號[N][k, 0] != 9999)
                        {
                            if (!Axis_status[N][k, Axis狀態.更改位置致能])
                            {
                                device = 運動參數.device_type + (運動參數.device_num + Axis_對應軸號[N][k, 0] * 10).ToString();
                                if (device != "" && device != null) PLC.properties.Device.Set_Device(device, Axis_運動參數[N][k, 運動參數.現在位置]);

                                device = 高速輸出入參數.device_type + (高速輸出入參數.device_num + Axis_對應軸號[N][k, 0] * 10).ToString();
                                if (device != "" && device != null) PLC.properties.Device.Set_Device(device, Axis_高速輸出入參數[N][k, 高速輸出入參數.現在位置]);
                            }
                            device = Axis狀態.device_type + (Axis狀態.device_num + Axis_對應軸號[N][k, 0] * 10).ToString();
                            if (device != "" && device != null) PLC.properties.Device.Set_Device(device, Axis_status[N][k, Axis狀態.Axis_Busy]);
                        }
                    }
                }
            }
        }
        #endregion

        #region Run_Axis
        void sub_Run_Axis()
        {
            if (FLAG_C9016_Isopen && PLC != null)
            {
                short temp0;
                int axis_num;
                double gear_ratio;
                double basic_speed;
                double active_speed;
                int target_position;
                double acc;
                double dec;
                double acc_temp = 0;
                double dec_temp = 0;
                for (int N = 0; N < Card_count; N++)
                {
                    for (int k = 0; k < Axis_num; k++)
                    {
                        int M = card_id[N];
                        axis_num = k + M * Axis_num;
                        if (!Axis_PC_Enable[axis_num])
                        {
                            if (Axis_status_buf[N][k, Axis狀態.脈衝輸出模式] != Axis_status[N][k, Axis狀態.脈衝輸出模式])
                            {
                                Axis_status_buf[N][k, Axis狀態.脈衝輸出模式] = Axis_status[N][k, Axis狀態.脈衝輸出模式];
                                if (Axis_status[N][k, Axis狀態.脈衝輸出模式]) temp0 = 0;
                                else temp0 = 1;
                                Pci9016.CPci9016.p9016_set_pls_outmode(axis_num, temp0); //設定脈波輸出形式                            
                            }
                            if (Axis_status_buf[N][k, Axis狀態.正極限致能] != Axis_status[N][k, Axis狀態.正極限致能])
                            {
                                Axis_status_buf[N][k, Axis狀態.正極限致能] = Axis_status[N][k, Axis狀態.正極限致能];
                                Axis_Set_PoT(axis_num, Axis_status[N][k, Axis狀態.正極限致能]);
                            }
                            if (Axis_status_buf[N][k, Axis狀態.負極限致能] != Axis_status[N][k, Axis狀態.負極限致能])
                            {
                                Axis_status_buf[N][k, Axis狀態.負極限致能] = Axis_status[N][k, Axis狀態.負極限致能];
                                Axis_Set_NoT(axis_num, Axis_status[N][k, Axis狀態.負極限致能]);
                            }
                        }
                      
                        
                    }
                    for (int k = 0; k < Axis_num; k++)
                    {

                        bool paulse = false;
                        if (Axis_運動參數[N][k, 運動參數.齒輪比_分子] > 0 && Axis_運動參數[N][k, 運動參數.齒輪比_分母] > 0)
                        {
                            gear_ratio = Axis_運動參數[N][k, 運動參數.齒輪比_分子] / Axis_運動參數[N][k, 運動參數.齒輪比_分母];
                        }
                        else gear_ratio = 1;
                        gear_ratio = 1;

                        int M = card_id[N];
                        axis_num = k + M * Axis_num;


                        if (!Axis_PC_Enable[axis_num])
                        {
                            basic_speed = Math.Abs(Axis_運動參數[N][k, 運動參數.基底速度]);
                            active_speed = Math.Abs(Axis_運動參數[N][k, 運動參數.運轉速度]);
                            target_position = (int)Math.Round(Axis_運動參數[N][k, 運動參數.運轉目標位置] * gear_ratio, 0);
                            if (basic_speed > active_speed) basic_speed = active_speed;
                            acc_temp = (float)Axis_運動參數[N][k, 運動參數.加速度];
                            dec_temp = (float)Axis_運動參數[N][k, 運動參數.減速度];
                            if (acc_temp < 10) acc_temp = 10;
                            if (dec_temp < 10) dec_temp = 10;
                            acc = (active_speed - basic_speed) / (acc_temp / 1000F);
                            dec = (active_speed - basic_speed) / (dec_temp / 1000F);

                            if (Axis_運動參數[N][k, 運動參數.運動命令碼] != Axis_運動參數_buf[N][k, 運動參數.運動命令碼])
                            {
                                paulse = true;
                                Axis_運動參數_buf[N][k, 運動參數.運動命令碼] = Axis_運動參數[N][k, 運動參數.運動命令碼];
                            }

                            if (Axis_運動參數[N][k, 運動參數.運動命令碼] == 0) // 停止
                            {
                                Pci9016.CPci9016.p9016_stop(axis_num, 1);
                            }
                            else if (Axis_運動參數[N][k, 運動參數.運動命令碼] == 1) //DDRVA           
                            {
                                DRVA_init(axis_num, active_speed, basic_speed, acc, dec);
                                DRVA(axis_num, target_position);
                            }
                            else if (Axis_運動參數[N][k, 運動參數.運動命令碼] == 2) //DDRVI
                            {
                                DRVI_init(axis_num, active_speed, basic_speed, acc, dec);
                                if (paulse)
                                {
                                    DRVI(axis_num, target_position);
                                }
                            }
                            else if (Axis_運動參數[N][k, 運動參數.運動命令碼] == 3) //PLSV
                            {
                                if (Axis_運動參數_buf[N][k, 運動參數.運轉速度] != Axis_運動參數[N][k, 運動參數.運轉速度])
                                {
                                    Pci9016.CPci9016.p9016_stop(axis_num, 1);
                                    Axis_運動參數_buf[N][k, 運動參數.運轉速度] = Axis_運動參數[N][k, 運動參數.運轉速度];
                                }
                                PLSV_init(axis_num, Math.Abs(active_speed), basic_speed, acc, dec);
                                if (Axis_運動參數[N][k, 運動參數.運轉速度] > 0) PLSV(axis_num, true, false);
                                if (Axis_運動參數[N][k, 運動參數.運轉速度] < 0) PLSV(axis_num, false, false);
                            }
                            else if (Axis_運動參數[N][k, 運動參數.運動命令碼] == 4) //PLSV(加減速度)
                            {
                                if (Axis_運動參數_buf[N][k, 運動參數.運轉速度] != Axis_運動參數[N][k, 運動參數.運轉速度])
                                {
                                    Pci9016.CPci9016.p9016_stop(axis_num, 1);
                                    Axis_運動參數_buf[N][k, 運動參數.運轉速度] = Axis_運動參數[N][k, 運動參數.運轉速度];
                                }
                                PLSV_init(axis_num, Math.Abs(active_speed), basic_speed, acc, dec);
                                if (Axis_運動參數[N][k, 運動參數.運轉速度] > 0) PLSV(axis_num, true, true);
                                if (Axis_運動參數[N][k, 運動參數.運轉速度] < 0) PLSV(axis_num, false, true);
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region Get_Axis_Status
        void sub_Get_Axis_Status()
        {
            if (FLAG_C9016_Isopen && PLC != null)
            {
                int axis_num;
                int pPos = 0;
                uint pStatus = 0;
                for (int N = 0; N < Card_count; N++)
                {
                    for (int k = 0; k < Axis_num; k++)
                    {
                        int M = card_id[N];
                        axis_num = k + M * Axis_num;
                        Pci9016.CPci9016.p9016_get_pos(axis_num, 0, ref pPos);
                        Axis_運動參數[N][k, 運動參數.現在位置] = pPos;
                        Pci9016.CPci9016.p9016_get_motion_status(axis_num, ref pStatus);
                        Axis_status[N][k, Axis狀態.Axis_Busy] = pStatus == 1 ? true : false;
                    }
                }
            }

        }
        #endregion
        #region Run_HighSpeedCounter
        void sub_Run_HighSpeedCounter()
        {
            short temp0;
            int axis_num;
            if (FLAG_C9016_Isopen && PLC != null)
            {
                for (int N = 0; N < Card_count; N++)
                {
                    for (int k = 0; k < HighSpeedCounter_num; k++)
                    {
                        int M = card_id[N];
                        axis_num = k + M * Axis_num;
                        if (Axis_status_buf[N][k, Axis狀態.高速計數器致能] != Axis_status[N][k, Axis狀態.高速計數器致能])
                        {
                            if (Axis_status[N][k, Axis狀態.高速計數器致能]) temp0 = 0;
                            else temp0 = 1;
                            Pci9016.CPci9016.p9016_set_pls_iptmode(axis_num, temp0);
                            Axis_status_buf[N][k, Axis狀態.高速計數器致能] = Axis_status[N][k, Axis狀態.高速計數器致能];
                        }
                  
                    }
                }
            }
        }
        #endregion
        #region Get_HighSpeedCounter
        void sub_Get_HighSpeedCounter()
        {
            if (FLAG_C9016_Isopen && PLC != null)
            {
                int axis_num;
                int pPos = 0;
                for (int N = 0; N < Card_count; N++)
                {
                    for (int k = 0; k < HighSpeedCounter_num; k++)
                    {
                        int M = card_id[N];
                        axis_num = k + M * Axis_num;
                        Pci9016.CPci9016.p9016_get_pos(axis_num, 1, ref pPos);
                        Axis_高速輸出入參數[N][k, 高速輸出入參數.現在位置] = pPos;
                    }
                }
            }
        }
        #endregion
        #region Run_CompareOut
        void sub_Run_CompareOut()
        {
            short temp0;
            int axis_num;
            if (FLAG_C9016_Isopen && PLC != null)
            {
                for (int N = 0; N < Card_count; N++)
                {
                    for (int k = 0; k < HighSpeedCounter_num; k++)
                    {
                        int M = card_id[N];
                        axis_num = k + M * Axis_num;
                        if (Axis_status_buf[N][k, Axis狀態.位置比較方式] != Axis_status[N][k, Axis狀態.位置比較方式])
                        {
                            
                            Axis_status_buf[N][k, Axis狀態.位置比較方式] = Axis_status[N][k, Axis狀態.位置比較方式];
                        }
                        if (Axis_status_buf[N][k, Axis狀態.位置比較輸出致能] != Axis_status[N][k, Axis狀態.位置比較輸出致能])
                        {
                            if(Axis_status[N][k, Axis狀態.位置比較輸出致能])
                            {
                                int numOfcmp = Axis_高速輸出入參數[N][k, 高速輸出入參數.比較位置數量];
                               // int OutLevel = 0;
                        
                                if (numOfcmp > 7) numOfcmp = 7;
                                if (numOfcmp < 0) numOfcmp = 0;

                                Pci9016.CPci9016.cmp_enable(card_id[N], k, 1, 0, Axis_高速輸出入參數[N][k, 高速輸出入參數.比較輸出位置], 100000);
                                for (int i = 0; i < numOfcmp;i++ )
                                {
                                    Pci9016.CPci9016.cmp_add_ref(card_id[N], k, Axis_高速輸出入參數[N][k,i + 2]);
                                }
                            }
                            else
                            {
                                Pci9016.CPci9016.cmp_enable(card_id[N], k, 1, 0, Axis_高速輸出入參數[N][k, 高速輸出入參數.比較輸出位置], 100000);
                                Pci9016.CPci9016.cmp_clr_fifo(card_id[N], k);
                                Pci9016.CPci9016.cmp_clr_matchCount(card_id[N], k);
                            }
                            Axis_status_buf[N][k, Axis狀態.位置比較輸出致能] = Axis_status[N][k, Axis狀態.位置比較輸出致能];
                        }
                    }
                }
            }
        }
        #endregion

        void Axis_Set_PoT(int axis_num, bool Enable)
        {
            int temp = 0;
            if (Enable) temp = 1;
            else temp = 0;
            Pci9016.CPci9016.p9016_set_el_level(axis_num, temp);
        }
        void Axis_Set_NoT(int axis_num, bool Enable)
        {
            int temp = 0;
            if (Enable) temp = 1;
            else temp = 0;
            Pci9016.CPci9016.p9016_set_el_level(axis_num, temp);
        }
        void DRVA_init(int axis_num, double active_speed, double basic_speed, double acc, double dec)
        {
            Pci9016.CPci9016.p9016_set_t_profile(axis_num, basic_speed, active_speed, acc, dec);

        }
        void DRVA(int axis_num, int target_position)
        {
            Pci9016.CPci9016.p9016_pmove(axis_num, target_position, 1, 2);
                          
        }
        void DRVI_init(int axis_num, double active_speed, double basic_speed, double acc, double dec)
        {
            Pci9016.CPci9016.p9016_set_t_profile(axis_num, basic_speed, active_speed, acc, dec);

        }
        void DRVI(int axis_num, int target_position)
        {
            Pci9016.CPci9016.p9016_pmove(axis_num, target_position, 0, 2);

        }
        void PLSV_init(int axis_num, double active_speed, double basic_speed, double acc, double dec)
        {
            Pci9016.CPci9016.p9016_set_t_profile(axis_num, basic_speed, active_speed, acc, dec);
        }
        void PLSV(int axis_num, bool dirrection , bool acc_enable)
        {
            int pus_dir, vel_mode;
            if (dirrection) pus_dir = 1;
            else pus_dir = 0;
            if (acc_enable) vel_mode = 2;
            else vel_mode = 1;
            double p = 0;
            Pci9016.CPci9016.p9016_get_current_speed(axis_num, ref p);
            Pci9016.CPci9016.p9016_vmove(axis_num, pus_dir, vel_mode);
        }

        public void DRVA(int axis_num, int target_position, double active_speed, double basic_speed, double acc, double dec)
        {
            if (axis_num < Axis_PC_Enable.Count)
            {
                if (Axis_PC_Enable[axis_num])
                {
                    double acc_temp = 0;
                    double dec_temp = 0;
                    if (acc < 10) acc = 10;
                    if (dec < 10) dec = 10;
                    acc_temp = (active_speed - basic_speed) / (acc / 1000F);
                    dec_temp = (active_speed - basic_speed) / (dec / 1000F);
                    Pci9016.CPci9016.p9016_set_t_profile(axis_num, basic_speed, active_speed, acc_temp, dec_temp);
                    Pci9016.CPci9016.p9016_pmove(axis_num, target_position, 1, 2);
                }
            }
       
        }
        public void DRVAS(int axis_num, int target_position, double active_speed, double basic_speed, double acc, double dec, double jerk_percent)
        {
            if (axis_num < Axis_PC_Enable.Count)
            {
                if (Axis_PC_Enable[axis_num])
                {
                    double acc_temp = 0;
                    double dec_temp = 0;
                    if (acc < 10) acc = 10;
                    if (dec < 10) dec = 10;
                    acc_temp = (active_speed - basic_speed) / (acc / 1000F);
                    dec_temp = (active_speed - basic_speed) / (dec / 1000F);
                    Pci9016.CPci9016.p9016_set_s_profile(axis_num, basic_speed, active_speed, acc_temp, dec_temp, jerk_percent);
                    Pci9016.CPci9016.p9016_pmove(axis_num, target_position, 1, 3);
                }
            }
    
        }
        public void PLSV(int axis_num, double active_speed, double basic_speed, double acc, double dec)
        {
            if (axis_num < Axis_PC_Enable.Count)
            {
                if (Axis_PC_Enable[axis_num])
                {
                    double acc_temp = 0;
                    double dec_temp = 0;
                    if (acc < 10) acc = 10;
                    if (dec < 10) dec = 10;
                    acc_temp = (active_speed - basic_speed) / (acc / 1000F);
                    dec_temp = (active_speed - basic_speed) / (dec / 1000F);
                    PLSV_init(axis_num, Math.Abs(active_speed), basic_speed, acc_temp, dec_temp);
                    if (active_speed > 0) PLSV(axis_num, true, true);
                    else if (active_speed < 0) PLSV(axis_num, false, true);
                }  
            }
               
        }
        public void PLSV(int axis_num, double active_speed)
        {
            if (axis_num < Axis_PC_Enable.Count)
            {
                if (Axis_PC_Enable[axis_num])
                {
                    PLSV_init(axis_num, Math.Abs(active_speed), 0, 0, 0);
                    if (active_speed > 0) PLSV(axis_num, true, false);
                    else if (active_speed < 0) PLSV(axis_num, false, false);
                }
            }
      
        }
        public void AxisStop(int axis_num)
        {
            Pci9016.CPci9016.p9016_stop(axis_num, 0);
        }
        public void AxisStopEmg(int axis_num)
        {
            Pci9016.CPci9016.p9016_stop(axis_num, 1);
        }
        public bool GetInput(int CardID ,int BitNum)
        {
            uint temp = 0;
            Pci9016.CPci9016.p9016_get_di(CardID, ref temp);
            return !myConvert.UInt32GetBit(temp, BitNum);
        }
        public bool GetOutput(int CardID, int BitNum)
        {
            uint temp = 0;
            Pci9016.CPci9016.p9016_get_do(CardID, ref temp);
            return !myConvert.UInt32GetBit(temp, BitNum);
        }
        public uint GetOutput(int CardID)
        {
            uint temp = 0;
            Pci9016.CPci9016.p9016_get_do(CardID, ref temp);
            return temp;
        }
        public void SetOutput(int CardID, uint pData)
        {
            Pci9016.CPci9016.p9016_set_do(CardID, pData);
        }
        public void SetOutput(int CardID, int BitNum, bool Statu)
        {
            uint temp = 0;
            int channel = BitNum / 8;
            int Bit = BitNum % 8;

            string device = Output_position[card_id[CardID]][channel, Bit];
            if (device != "" && device != null)
            {
                PLC.properties.Device.Set_Device(device, Statu);
                PLC.properties.device_system.Set_Device(device, Statu);
            }
            Pci9016.CPci9016.p9016_get_do(CardID, ref temp);
            temp = myConvert.UInt32SetBit(!Statu, temp, BitNum);
            Pci9016.CPci9016.p9016_set_do(CardID, temp);
        }
        public void Set_PC_ControlEnable(int AxisNum,bool Enable)
        {
            if (AxisNum < Axis_PC_Enable.Count) Axis_PC_Enable[AxisNum] = Enable;
        }
        public int GetAxisCmdPos(int axis_num)
        {
            int pPos = 0;
            Pci9016.CPci9016.p9016_get_pos(axis_num, 0, ref pPos);
            return pPos;
        }
        public void SetAxisCmdPos(int axis_num, int pos)
        {
            int pPos = 0;
            Pci9016.CPci9016.p9016_set_pos(axis_num, 0, pos);     
        }
        public int GetAxisEnCoderPos(int axis_num)
        {
            int pPos = 0;
            Pci9016.CPci9016.p9016_get_pos(axis_num, 1, ref pPos);
            return pPos;
        }
        public void SetAxisEnCoderPos(int axis_num, int pos)
        {
            int pPos = 0;
            Pci9016.CPci9016.p9016_set_pos(axis_num, 1, pos);
        }
        public bool GetAxisPEL(int axis_num)
        {
            uint temp = 0;
            Pci9016.CPci9016.p9016_get_io_status(axis_num, ref temp);
            return myConvert.UInt32GetBit(temp, 0);
        }
        public bool GetAxisMEL(int axis_num)
        {
            uint temp = 0;
            Pci9016.CPci9016.p9016_get_io_status(axis_num, ref temp);
            return myConvert.UInt32GetBit(temp, 1);
        }
        public bool GetAxisORG(int axis_num)
        {
            uint temp = 0;
            Pci9016.CPci9016.p9016_get_io_status(axis_num, ref temp);
            return myConvert.UInt32GetBit(temp, 2);
        }
        public void SetCmp(int CardID, int AxisNum, int BitNum, int[] Posref, int OutLenth)
        {
            Pci9016.CPci9016.cmp_enable(CardID, AxisNum, 1, 0, BitNum, OutLenth);
            for (int i = 0; i < Posref.Length; i++)
            {
                Pci9016.CPci9016.cmp_add_ref(CardID, AxisNum, Posref[i]);
            }
        }
        public void ClearCmp(int CardID, int AxisNum, int BitNum)
        {
            Pci9016.CPci9016.cmp_enable(CardID, AxisNum, 0, 0, BitNum, 10000);
            Pci9016.CPci9016.cmp_clr_fifo(CardID, AxisNum);
            Pci9016.CPci9016.cmp_clr_matchCount(CardID, AxisNum);
        }
        public bool IsOpen()
        {
            return this.FLAG_C9016_Isopen;
        }
        private void exButton_Open_btnClick(object sender, EventArgs e)
        {
            if (exButton_Open.Load_WriteState())
            {
                if (cnt_Open_C9016 == 255) cnt_Open_C9016 = 1;
            }
            else
            {
                if (cnt_Close_C9016 == 255) cnt_Close_C9016 = 1;
            }
        }
        [Serializable]
        private class SavePropertyFile
        {
            public int Card_count;
            public List<string[,]> Input_position = new List<string[,]>();
            public List<string[,]> Output_position = new List<string[,]>();

            public List<int[,]> Axis_對應軸號 = new List<int[,]>();
            public List<string[,]> Axis_Input_position = new List<string[,]>();
        }
        private SavePropertyFile savePropertyFile = new SavePropertyFile();
        private void SaveProperties()
        {
            IFormatter binFmt = new BinaryFormatter();
            Stream stream = null;
            savePropertyFile.Card_count = Card_count.DeepClone();
            savePropertyFile.Input_position = MyFileStream.DeepClone(Input_position);
            savePropertyFile.Output_position = MyFileStream.DeepClone(Output_position);
            savePropertyFile.Axis_對應軸號 = MyFileStream.DeepClone(Axis_對應軸號);
            savePropertyFile.Axis_Input_position = MyFileStream.DeepClone(Axis_Input_position);
            try
            {
                stream = File.Open(StreamName, FileMode.Create);
                binFmt.Serialize(stream, savePropertyFile);
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }
        private void LoadProperties()
        {
            IFormatter binFmt = new BinaryFormatter();
            Stream stream = null;
            try
            {
                if (File.Exists(".\\" + StreamName))
                {
                    stream = File.Open(".\\" + StreamName, FileMode.Open);
                    try { savePropertyFile = (SavePropertyFile)binFmt.Deserialize(stream); }
                    catch { }

                }
                Card_count = savePropertyFile.Card_count.DeepClone();
                Input_position = MyFileStream.DeepClone(savePropertyFile.Input_position);
                Output_position = MyFileStream.DeepClone(savePropertyFile.Output_position);
                Axis_對應軸號 = MyFileStream.DeepClone(savePropertyFile.Axis_對應軸號);
                Axis_Input_position = MyFileStream.DeepClone(savePropertyFile.Axis_Input_position);
                if (Input_position.Count > Card_count) Input_position.RemoveRange(Card_count, Input_position.Count - Card_count);
                if (Input_position == null) Input_position = new List<string[,]>();
                if (Output_position == null) Output_position = new List<string[,]>();
                if (Axis_Input_position == null) Axis_Input_position = new List<string[,]>();
                if (Axis_對應軸號 == null) Axis_對應軸號 = new List<int[,]>();
            }
            finally
            {
                if (stream != null) stream.Close();
            }

            cnt_Open_C9016 = 1;
        }
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            Thread00.Stop();
        }


    }
}
