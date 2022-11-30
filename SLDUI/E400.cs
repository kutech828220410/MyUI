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
    [System.Drawing.ToolboxBitmap(typeof(E400), "ECAN.bmp")]
    public partial class E400 : UserControl
    {
        private string  _設備名稱 = "";
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

        private Form Active_Form;
        private bool RefreshUI_Enable = true;
        private MyThread ThreadStart = new MyThread("E400Start");
        private MyThread ThreadOutput = new MyThread("E400Output");
        private MyThread ThreadUI = new MyThread("E400UI");
        private String StreamName;
        private LowerMachine lowerMachine;
        private MyConvert myConvert = new MyConvert();
        private List<ExButton> List_ExButton = new List<ExButton>();

        private NumWordTextBox[,] NumWordTextBox_Input = new NumWordTextBox[2, 8];
        private string[,] Input_position = new string[2, 8];
        private ExButton[,] ExButton_Input = new ExButton[2, 8];
        private bool[,] Input = new bool[2, 8];

        private NumWordTextBox[,] NumWordTextBox_Output = new NumWordTextBox[2, 8];
        private string[,] Output_position = new string[2, 8];
        private ExButton[,] ExButton_Output = new ExButton[2, 8];   
        private bool[,] Output = new bool[2, 8];

        private NumWordTextBox[,] NumWordTextBox_Axis_Input = new NumWordTextBox[4, 3];
        private string[,] Axis_Input_position = new string[4, 3];
        private ExButton[,] ExButton_Axis_Input = new ExButton[4, 3];
        private bool[,] Axis_Input = new bool[4, 3];
        static private class Axis輸入
        {
            static public int 正極限 = 0;
            static public int 負極限 = 1;
            static public int 原點 = 2;
           
        }

        private NumWordTextBox[,] NumWordTextBox_Axis_Output = new NumWordTextBox[4, 1];
        private string[,] Axis_Output_position = new string[4, 1];
        private ExButton[,] ExButton_Axis_Output = new ExButton[4, 1];      
        private bool[,] Axis_Output = new bool[4, 1];
        static private class Axis輸出
        {
            static public int SrvON = 0;
        }

        private ExButton[,] ExButton_Axis_status = new ExButton[4, 10];
        private bool[,] Axis_status = new bool[4, 10];
        private bool[,] Axis_status_buf = new bool[4, 10];
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
        }

        private NumTextBox[,] NumTextBox_Axis_對應軸號 = new NumTextBox[4, 1];
        private int[,] Axis_對應軸號 = new int[4, 1];

   
        private NumTextBox[,] NumTextBox_Axis_運動參數 = new NumTextBox[4, 10];
        private int[,] Axis_運動參數 = new int[4, 10];
        private int[] Axis_運動參數_Buf = new int[4];
        static private class 運動參數
        {
            static public string device_type = "D";
            static public int device_num = 8340;
            static public int 現在位置 = 0;
            static public int 運轉目標位置 = 4;
            static public int 基底速度 = 5;
            static public int 運動命令碼 = 6;
            static public int 運轉速度 = 7;
            static public int 加速度 = 8;
            static public int 減速度 = 9;
        }

        private NumTextBox[,] NumTextBox_Axis_高速輸出入參數 = new NumTextBox[4, 10];
        private int[,] Axis_高速輸出入參數 = new int[4, 10];
        private bool[] Axis_高速輸出入致能_Buf = new bool[4];
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
            static public int 比較位置_8 = 9;
        }

        private bool FLAG_E400_Isopen = false;

        private short card_num = 0;
        private short m_nAddr = 0;

        delegate void UI_Visible_Delegate(bool visble);
        UI_Visible_Delegate Delegate;
        private void sub_UI_Visible(bool visble)
        {
            this.Visible = visble;
        }
        public void UI_Visible(bool visble)
        {
            if(Delegate == null) Delegate = new UI_Visible_Delegate(sub_UI_Visible);
            Invoke(Delegate, visble);
            RefreshUI_Enable = visble;
   
        }

        private void SetLowerMachine(LowerMachine lowerMachine)
        {
            this.lowerMachine = lowerMachine;
        }
        private void SetForm(Form form)
        {
            Active_Form = form;
        }
        private void Init()
        {
            Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
            LoadProperties();

           
            lowerMachine.Add_UI_Method(DoWork_UI);


            ThreadStart.Add_Method(DoWork_strat);
            ThreadStart.Add_Method(DoWork_complete);
            ThreadStart.Add_Method(DoWork_Output);
            ThreadStart.SetSleepTime(CycleTime);
            ThreadStart.AutoRun(true);       
        }
        public E400()
        {    
            InitializeComponent();           
        }
        public void Run(Form form, LowerMachine lowerMachine)
        {
            SetForm(form);
            SetLowerMachine(lowerMachine);
            StreamName = numWordTextBox_StreamName.Text + ".pro";
            //RefreshUI_Enable = this.Visible;
            List_ExButton.Add(exButton_Open);

            ExButton_Input[0, 0] = exButton_Input_I00;
            ExButton_Input[0, 1] = exButton_Input_I01;
            ExButton_Input[0, 2] = exButton_Input_I02;
            ExButton_Input[0, 3] = exButton_Input_I03;
            ExButton_Input[0, 4] = exButton_Input_I04;
            ExButton_Input[0, 5] = exButton_Input_I05;
            ExButton_Input[0, 6] = exButton_Input_I06;
            ExButton_Input[0, 7] = exButton_Input_I07;

            ExButton_Input[1, 0] = exButton_Input_I10;
            ExButton_Input[1, 1] = exButton_Input_I11;
            ExButton_Input[1, 2] = exButton_Input_I12;
            ExButton_Input[1, 3] = exButton_Input_I13;
            ExButton_Input[1, 4] = exButton_Input_I14;
            ExButton_Input[1, 5] = exButton_Input_I15;
            ExButton_Input[1, 6] = exButton_Input_I16;
            ExButton_Input[1, 7] = exButton_Input_I17;


            NumWordTextBox_Input[0, 0] = numWordTextBox_Input_I00;
            NumWordTextBox_Input[0, 1] = numWordTextBox_Input_I01;
            NumWordTextBox_Input[0, 2] = numWordTextBox_Input_I02;
            NumWordTextBox_Input[0, 3] = numWordTextBox_Input_I03;
            NumWordTextBox_Input[0, 4] = numWordTextBox_Input_I04;
            NumWordTextBox_Input[0, 5] = numWordTextBox_Input_I05;
            NumWordTextBox_Input[0, 6] = numWordTextBox_Input_I06;
            NumWordTextBox_Input[0, 7] = numWordTextBox_Input_I07;
            NumWordTextBox_Input[1, 0] = numWordTextBox_Input_I10;
            NumWordTextBox_Input[1, 1] = numWordTextBox_Input_I11;
            NumWordTextBox_Input[1, 2] = numWordTextBox_Input_I12;
            NumWordTextBox_Input[1, 3] = numWordTextBox_Input_I13;
            NumWordTextBox_Input[1, 4] = numWordTextBox_Input_I14;
            NumWordTextBox_Input[1, 5] = numWordTextBox_Input_I15;
            NumWordTextBox_Input[1, 6] = numWordTextBox_Input_I16;
            NumWordTextBox_Input[1, 7] = numWordTextBox_Input_I17;

            ExButton_Output[0, 0] = exButton_Output_O00;
            ExButton_Output[0, 1] = exButton_Output_O01;
            ExButton_Output[0, 2] = exButton_Output_O02;
            ExButton_Output[0, 3] = exButton_Output_O03;
            ExButton_Output[0, 4] = exButton_Output_O04;
            ExButton_Output[0, 5] = exButton_Output_O05;
            ExButton_Output[0, 6] = exButton_Output_O06;
            ExButton_Output[0, 7] = exButton_Output_O07;
            ExButton_Output[1, 0] = exButton_Output_O10;
            ExButton_Output[1, 1] = exButton_Output_O11;
            ExButton_Output[1, 2] = exButton_Output_O12;
            ExButton_Output[1, 3] = exButton_Output_O13;
            ExButton_Output[1, 4] = exButton_Output_O14;
            ExButton_Output[1, 5] = exButton_Output_O15;
            ExButton_Output[1, 6] = exButton_Output_O16;
            ExButton_Output[1, 7] = exButton_Output_O17;

            NumWordTextBox_Output[0, 0] = numWordTextBox_Output_O00;
            NumWordTextBox_Output[0, 1] = numWordTextBox_Output_O01;
            NumWordTextBox_Output[0, 2] = numWordTextBox_Output_O02;
            NumWordTextBox_Output[0, 3] = numWordTextBox_Output_O03;
            NumWordTextBox_Output[0, 4] = numWordTextBox_Output_O04;
            NumWordTextBox_Output[0, 5] = numWordTextBox_Output_O05;
            NumWordTextBox_Output[0, 6] = numWordTextBox_Output_O06;
            NumWordTextBox_Output[0, 7] = numWordTextBox_Output_O07;
            NumWordTextBox_Output[1, 0] = numWordTextBox_Output_O10;
            NumWordTextBox_Output[1, 1] = numWordTextBox_Output_O11;
            NumWordTextBox_Output[1, 2] = numWordTextBox_Output_O12;
            NumWordTextBox_Output[1, 3] = numWordTextBox_Output_O13;
            NumWordTextBox_Output[1, 4] = numWordTextBox_Output_O14;
            NumWordTextBox_Output[1, 5] = numWordTextBox_Output_O15;
            NumWordTextBox_Output[1, 6] = numWordTextBox_Output_O16;
            NumWordTextBox_Output[1, 7] = numWordTextBox_Output_O17;

            NumTextBox_Axis_對應軸號[0, 0] = numTextBox_Axis1_對應軸號;
            NumTextBox_Axis_對應軸號[1, 0] = numTextBox_Axis2_對應軸號;
            NumTextBox_Axis_對應軸號[2, 0] = numTextBox_Axis3_對應軸號;
            NumTextBox_Axis_對應軸號[3, 0] = numTextBox_Axis4_對應軸號;

            NumTextBox_Axis_運動參數[0, 0] = numTextBox_Axis1_D0;
            NumTextBox_Axis_運動參數[0, 1] = numTextBox_Axis1_D1;
            NumTextBox_Axis_運動參數[0, 2] = numTextBox_Axis1_D2;
            NumTextBox_Axis_運動參數[0, 3] = numTextBox_Axis1_D3;
            NumTextBox_Axis_運動參數[0, 4] = numTextBox_Axis1_D4;
            NumTextBox_Axis_運動參數[0, 5] = numTextBox_Axis1_D5;
            NumTextBox_Axis_運動參數[0, 6] = numTextBox_Axis1_D6;
            NumTextBox_Axis_運動參數[0, 7] = numTextBox_Axis1_D7;
            NumTextBox_Axis_運動參數[0, 8] = numTextBox_Axis1_D8;
            NumTextBox_Axis_運動參數[0, 9] = numTextBox_Axis1_D9;

            NumTextBox_Axis_運動參數[1, 0] = numTextBox_Axis2_D0;
            NumTextBox_Axis_運動參數[1, 1] = numTextBox_Axis2_D1;
            NumTextBox_Axis_運動參數[1, 2] = numTextBox_Axis2_D2;
            NumTextBox_Axis_運動參數[1, 3] = numTextBox_Axis2_D3;
            NumTextBox_Axis_運動參數[1, 4] = numTextBox_Axis2_D4;
            NumTextBox_Axis_運動參數[1, 5] = numTextBox_Axis2_D5;
            NumTextBox_Axis_運動參數[1, 6] = numTextBox_Axis2_D6;
            NumTextBox_Axis_運動參數[1, 7] = numTextBox_Axis2_D7;
            NumTextBox_Axis_運動參數[1, 8] = numTextBox_Axis2_D8;
            NumTextBox_Axis_運動參數[1, 9] = numTextBox_Axis2_D9;

            NumTextBox_Axis_運動參數[2, 0] = numTextBox_Axis3_D0;
            NumTextBox_Axis_運動參數[2, 1] = numTextBox_Axis3_D1;
            NumTextBox_Axis_運動參數[2, 2] = numTextBox_Axis3_D2;
            NumTextBox_Axis_運動參數[2, 3] = numTextBox_Axis3_D3;
            NumTextBox_Axis_運動參數[2, 4] = numTextBox_Axis3_D4;
            NumTextBox_Axis_運動參數[2, 5] = numTextBox_Axis3_D5;
            NumTextBox_Axis_運動參數[2, 6] = numTextBox_Axis3_D6;
            NumTextBox_Axis_運動參數[2, 7] = numTextBox_Axis3_D7;
            NumTextBox_Axis_運動參數[2, 8] = numTextBox_Axis3_D8;
            NumTextBox_Axis_運動參數[2, 9] = numTextBox_Axis3_D9;

            NumTextBox_Axis_運動參數[3, 0] = numTextBox_Axis4_D0;
            NumTextBox_Axis_運動參數[3, 1] = numTextBox_Axis4_D1;
            NumTextBox_Axis_運動參數[3, 2] = numTextBox_Axis4_D2;
            NumTextBox_Axis_運動參數[3, 3] = numTextBox_Axis4_D3;
            NumTextBox_Axis_運動參數[3, 4] = numTextBox_Axis4_D4;
            NumTextBox_Axis_運動參數[3, 5] = numTextBox_Axis4_D5;
            NumTextBox_Axis_運動參數[3, 6] = numTextBox_Axis4_D6;
            NumTextBox_Axis_運動參數[3, 7] = numTextBox_Axis4_D7;
            NumTextBox_Axis_運動參數[3, 8] = numTextBox_Axis4_D8;
            NumTextBox_Axis_運動參數[3, 9] = numTextBox_Axis4_D9;

            ExButton_Axis_status[0, 0] = exButton_Axis1_S0;
            ExButton_Axis_status[0, 1] = exButton_Axis1_S1;
            ExButton_Axis_status[0, 2] = exButton_Axis1_S2;
            ExButton_Axis_status[0, 3] = exButton_Axis1_S3;
            ExButton_Axis_status[0, 4] = exButton_Axis1_S4;
            ExButton_Axis_status[0, 5] = exButton_Axis1_S5;
            ExButton_Axis_status[0, 6] = exButton_Axis1_S6;
            ExButton_Axis_status[0, 7] = exButton_Axis1_S7;
            ExButton_Axis_status[0, 8] = exButton_Axis1_S8;
            ExButton_Axis_status[0, 9] = exButton_Axis1_S9;

            ExButton_Axis_status[1, 0] = exButton_Axis2_S0;
            ExButton_Axis_status[1, 1] = exButton_Axis2_S1;
            ExButton_Axis_status[1, 2] = exButton_Axis2_S2;
            ExButton_Axis_status[1, 3] = exButton_Axis2_S3;
            ExButton_Axis_status[1, 4] = exButton_Axis2_S4;
            ExButton_Axis_status[1, 5] = exButton_Axis2_S5;
            ExButton_Axis_status[1, 6] = exButton_Axis2_S6;
            ExButton_Axis_status[1, 7] = exButton_Axis2_S7;
            ExButton_Axis_status[1, 8] = exButton_Axis2_S8;
            ExButton_Axis_status[1, 9] = exButton_Axis2_S9;

            ExButton_Axis_status[2, 0] = exButton_Axis3_S0;
            ExButton_Axis_status[2, 1] = exButton_Axis3_S1;
            ExButton_Axis_status[2, 2] = exButton_Axis3_S2;
            ExButton_Axis_status[2, 3] = exButton_Axis3_S3;
            ExButton_Axis_status[2, 4] = exButton_Axis3_S4;
            ExButton_Axis_status[2, 5] = exButton_Axis3_S5;
            ExButton_Axis_status[2, 6] = exButton_Axis3_S6;
            ExButton_Axis_status[2, 7] = exButton_Axis3_S7;
            ExButton_Axis_status[2, 8] = exButton_Axis3_S8;
            ExButton_Axis_status[2, 9] = exButton_Axis3_S9;

            ExButton_Axis_status[3, 0] = exButton_Axis4_S0;
            ExButton_Axis_status[3, 1] = exButton_Axis4_S1;
            ExButton_Axis_status[3, 2] = exButton_Axis4_S2;
            ExButton_Axis_status[3, 3] = exButton_Axis4_S3;
            ExButton_Axis_status[3, 4] = exButton_Axis4_S4;
            ExButton_Axis_status[3, 5] = exButton_Axis4_S5;
            ExButton_Axis_status[3, 6] = exButton_Axis4_S6;
            ExButton_Axis_status[3, 7] = exButton_Axis4_S7;
            ExButton_Axis_status[3, 8] = exButton_Axis4_S8;
            ExButton_Axis_status[3, 9] = exButton_Axis4_S9;

            NumWordTextBox_Axis_Input[0, 0] = numWordTextBox_Input_Axis1_0;
            NumWordTextBox_Axis_Input[0, 1] = numWordTextBox_Input_Axis1_1;
            NumWordTextBox_Axis_Input[0, 2] = numWordTextBox_Input_Axis1_2;
            NumWordTextBox_Axis_Input[1, 0] = numWordTextBox_Input_Axis2_0;
            NumWordTextBox_Axis_Input[1, 1] = numWordTextBox_Input_Axis2_1;
            NumWordTextBox_Axis_Input[1, 2] = numWordTextBox_Input_Axis2_2;
            NumWordTextBox_Axis_Input[2, 0] = numWordTextBox_Input_Axis3_0;
            NumWordTextBox_Axis_Input[2, 1] = numWordTextBox_Input_Axis3_1;
            NumWordTextBox_Axis_Input[2, 2] = numWordTextBox_Input_Axis3_2;
            NumWordTextBox_Axis_Input[3, 0] = numWordTextBox_Input_Axis4_0;
            NumWordTextBox_Axis_Input[3, 1] = numWordTextBox_Input_Axis4_1;
            NumWordTextBox_Axis_Input[3, 2] = numWordTextBox_Input_Axis4_2;

            ExButton_Axis_Input[0, 0] = exButton_Input_Axis1_0;
            ExButton_Axis_Input[0, 1] = exButton_Input_Axis1_1;
            ExButton_Axis_Input[0, 2] = exButton_Input_Axis1_2;
            ExButton_Axis_Input[1, 0] = exButton_Input_Axis2_0;
            ExButton_Axis_Input[1, 1] = exButton_Input_Axis2_1;
            ExButton_Axis_Input[1, 2] = exButton_Input_Axis2_2;
            ExButton_Axis_Input[2, 0] = exButton_Input_Axis3_0;
            ExButton_Axis_Input[2, 1] = exButton_Input_Axis3_1;
            ExButton_Axis_Input[2, 2] = exButton_Input_Axis3_2;
            ExButton_Axis_Input[3, 0] = exButton_Input_Axis4_0;
            ExButton_Axis_Input[3, 1] = exButton_Input_Axis4_1;
            ExButton_Axis_Input[3, 2] = exButton_Input_Axis4_2;

            NumWordTextBox_Axis_Output[0, 0] = numWordTextBox_Output_Axis1_0;   
            NumWordTextBox_Axis_Output[1, 0] = numWordTextBox_Output_Axis2_0;
            NumWordTextBox_Axis_Output[2, 0] = numWordTextBox_Output_Axis3_0;     
            NumWordTextBox_Axis_Output[3, 0] = numWordTextBox_Output_Axis4_0;

            ExButton_Axis_Output[0, 0] = exButton_Output_Axis1_0;
            ExButton_Axis_Output[1, 0] = exButton_Output_Axis2_0;
            ExButton_Axis_Output[2, 0] = exButton_Output_Axis3_0;
            ExButton_Axis_Output[3, 0] = exButton_Output_Axis4_0;

            NumTextBox_Axis_高速輸出入參數[0, 0] = numTextBox_Axis1_R0;
            NumTextBox_Axis_高速輸出入參數[0, 1] = numTextBox_Axis1_R1;
            NumTextBox_Axis_高速輸出入參數[0, 2] = numTextBox_Axis1_R2;
            NumTextBox_Axis_高速輸出入參數[0, 3] = numTextBox_Axis1_R3;
            NumTextBox_Axis_高速輸出入參數[0, 4] = numTextBox_Axis1_R4;
            NumTextBox_Axis_高速輸出入參數[0, 5] = numTextBox_Axis1_R5;
            NumTextBox_Axis_高速輸出入參數[0, 6] = numTextBox_Axis1_R6;
            NumTextBox_Axis_高速輸出入參數[0, 7] = numTextBox_Axis1_R7;
            NumTextBox_Axis_高速輸出入參數[0, 8] = numTextBox_Axis1_R8;
            NumTextBox_Axis_高速輸出入參數[0, 9] = numTextBox_Axis1_R9;

            NumTextBox_Axis_高速輸出入參數[1, 0] = numTextBox_Axis2_R0;
            NumTextBox_Axis_高速輸出入參數[1, 1] = numTextBox_Axis2_R1;
            NumTextBox_Axis_高速輸出入參數[1, 2] = numTextBox_Axis2_R2;
            NumTextBox_Axis_高速輸出入參數[1, 3] = numTextBox_Axis2_R3;
            NumTextBox_Axis_高速輸出入參數[1, 4] = numTextBox_Axis2_R4;
            NumTextBox_Axis_高速輸出入參數[1, 5] = numTextBox_Axis2_R5;
            NumTextBox_Axis_高速輸出入參數[1, 6] = numTextBox_Axis2_R6;
            NumTextBox_Axis_高速輸出入參數[1, 7] = numTextBox_Axis2_R7;
            NumTextBox_Axis_高速輸出入參數[1, 8] = numTextBox_Axis2_R8;
            NumTextBox_Axis_高速輸出入參數[1, 9] = numTextBox_Axis2_R9;

            NumTextBox_Axis_高速輸出入參數[2, 0] = numTextBox_Axis3_R0;
            NumTextBox_Axis_高速輸出入參數[2, 1] = numTextBox_Axis3_R1;
            NumTextBox_Axis_高速輸出入參數[2, 2] = numTextBox_Axis3_R2;
            NumTextBox_Axis_高速輸出入參數[2, 3] = numTextBox_Axis3_R3;
            NumTextBox_Axis_高速輸出入參數[2, 4] = numTextBox_Axis3_R4;
            NumTextBox_Axis_高速輸出入參數[2, 5] = numTextBox_Axis3_R5;
            NumTextBox_Axis_高速輸出入參數[2, 6] = numTextBox_Axis3_R6;
            NumTextBox_Axis_高速輸出入參數[2, 7] = numTextBox_Axis3_R7;
            NumTextBox_Axis_高速輸出入參數[2, 8] = numTextBox_Axis3_R8;
            NumTextBox_Axis_高速輸出入參數[2, 9] = numTextBox_Axis3_R9;

            NumTextBox_Axis_高速輸出入參數[3, 0] = numTextBox_Axis4_R0;
            NumTextBox_Axis_高速輸出入參數[3, 1] = numTextBox_Axis4_R1;
            NumTextBox_Axis_高速輸出入參數[3, 2] = numTextBox_Axis4_R2;
            NumTextBox_Axis_高速輸出入參數[3, 3] = numTextBox_Axis4_R3;
            NumTextBox_Axis_高速輸出入參數[3, 4] = numTextBox_Axis4_R4;
            NumTextBox_Axis_高速輸出入參數[3, 5] = numTextBox_Axis4_R5;
            NumTextBox_Axis_高速輸出入參數[3, 6] = numTextBox_Axis4_R6;
            NumTextBox_Axis_高速輸出入參數[3, 7] = numTextBox_Axis4_R7;
            NumTextBox_Axis_高速輸出入參數[3, 8] = numTextBox_Axis4_R8;
            NumTextBox_Axis_高速輸出入參數[3, 9] = numTextBox_Axis4_R9;

            /*for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                CallBackUI.properties.DobleBuffer(tabControl1.TabPages[i], true);
            }*/
        
            foreach (MyUI.ExButton ExButton_temp in List_ExButton) ExButton_temp.Run(lowerMachine);
            Init();
        }
        private void DoWork_strat()
        {
            sub_Get_Input();
            sub_WriteToPLC();            
        }
        System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
        private void DoWork_complete()
        {       
            sub_Get_Axis_Status();
            sub_Run_HighSpeedCounter();
            sub_Run_CompareOut();        
            sub_Run_Axis();              
        }
        private void DoWork_Output()
        {
            sub_ReadFromPLC();
            sub_Set_Output();
        }
        private void DoWork_UI()
        {
            if (RefreshUI_Enable)
            {
                RefreshUI();
            }
            sub_Open_E400();
            sub_Close_E400();                
        }
        void RefreshUI()
        {          
            for (int i = 0; i < 8; i++)
            {
                ExButton_Input[0, i].Set_LoadState(Input[0, i]);
                ExButton_Input[1, i].Set_LoadState(Input[1, i]);

                ExButton_Output[0, i].Set_LoadState(Output[0, i]);
                ExButton_Output[1, i].Set_LoadState(Output[1, i]);
            }
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 3; i++)
                {
                    ExButton_Axis_Input[k, i].Set_LoadState(Axis_Input[k, i]);
                }
            }
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 1; i++)
                {
                    ExButton_Axis_Output[k, i].Set_LoadState(Axis_Output[k, i]);
                }
            }
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 10; i++)
                {
                    ExButton_Axis_status[k, i].Set_LoadState(Axis_status[k, i]);
                    CallBackUI.numTextBox.字串更換(Axis_運動參數[k, i].ToString(), NumTextBox_Axis_運動參數[k, i]);
                    CallBackUI.numTextBox.字串更換(Axis_高速輸出入參數[k, i].ToString(), NumTextBox_Axis_高速輸出入參數[k, i]);
                }
            }
            ThreadStart.GetCycleTime(300, label_CycleTime);
            exButton_Open.Set_LoadState(FLAG_E400_Isopen);
        }

        #region Open_E400
        byte cnt_Open_E400 = 255;
        void sub_Open_E400()
        {
            if (cnt_Open_E400 == 1) cnt_Open_E400_00_檢查控制器是否開啟(ref cnt_Open_E400);
            if (cnt_Open_E400 == 2) cnt_Open_E400_00_檢查連接狀態(ref cnt_Open_E400);
            if (cnt_Open_E400 == 3) cnt_Open_E400_00_開啟板卡(ref cnt_Open_E400);
            if (cnt_Open_E400 == 4) cnt_Open_E400 = 240;

            if (cnt_Open_E400 == 240) cnt_Open_E400_240_開啟成功(ref cnt_Open_E400);
            if (cnt_Open_E400 == 241) cnt_Open_E400 = 255;

            if (cnt_Open_E400 == 250) cnt_Open_E400_250_開啟失敗(ref cnt_Open_E400);
            if (cnt_Open_E400 == 251) cnt_Open_E400 = 255;
   
        }
        void cnt_Open_E400_00_檢查控制器是否開啟(ref byte cnt)
        {
            FLAG_E400_Isopen = false;
            if (SLD_Basic.ErrorCode(SLDMclass.SLDM_IsOpen(card_num), false))
            {
                {
                    SLD_Basic.ErrorCode(SLDMclass.SLDM_Close(card_num), false);
                }
            }     
            cnt++;
        }
        void cnt_Open_E400_00_檢查連接狀態(ref byte cnt)
        {
            cnt++;
            return;
            /*if (!SLD_Basic.ErrorCode(SLDMclass.SLDM_GetConnStatus(card_num), false))
            {
                cnt++;
            }
            else cnt = 250;*/
        }
        void cnt_Open_E400_00_開啟板卡(ref byte cnt)
        {
            if (SLD_Basic.ErrorCode(SLDMclass.SLDM_Open(card_num, 4, 1000), true) && SLD_Basic.ErrorCode(SLDMclass.SLDM_SetIOCfg(card_num, m_nAddr, 0, 1, 2, 2), true))
            {
                cnt++;
            }
            else cnt = 250;
        }
        void cnt_Open_E400_240_開啟成功(ref byte cnt)
        {
            string temp0 = "";
            string device_type0 = "";
            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 8; i++)
                {
                    temp0 = "";
                    device_type0 = "";                
                    CallBackUI.numWordTextBox.取得字串(ref temp0, NumWordTextBox_Input[k, i]);
                    if (temp0 != null)
                    {
                        temp0 = temp0.ToUpper();
                        if (temp0.Length > 1) device_type0 = temp0.Substring(0, 1);
                        if (!LadderProperty.DEVICE.TestDevice(temp0) || device_type0 != "X")
                        {
                            temp0 = "";
                            CallBackUI.numWordTextBox.字串更換(temp0, NumWordTextBox_Input[k, i]);
                        }
                        Input_position[k, i] = temp0;
                        CallBackUI.numWordTextBox.Enable(false, NumWordTextBox_Input[k, i]);
                    }
                 
                }
            }
            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 8; i++)
                {
                    temp0 = "";
                    device_type0 = "";
                    CallBackUI.numWordTextBox.取得字串(ref temp0, NumWordTextBox_Output[k, i]);
                    if (temp0 != null)
                    {
                        temp0 = temp0.ToUpper();
                        if (temp0.Length > 1) device_type0 = temp0.Substring(0, 1);
                        if (!LadderProperty.DEVICE.TestDevice(temp0) || device_type0 != "Y")
                        {
                            temp0 = "";
                            CallBackUI.numWordTextBox.字串更換(temp0, NumWordTextBox_Output[k, i]);

                        }
                        Output_position[k, i] = temp0;
                        CallBackUI.numWordTextBox.Enable(false, NumWordTextBox_Output[k, i]);
                    }
                }
            }
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 3; i++)
                {
                    temp0 = "";
                    device_type0 = "";
                    CallBackUI.numWordTextBox.取得字串(ref temp0, NumWordTextBox_Axis_Input[k, i]);
                    if (temp0 != null)
                    {
                        temp0 = temp0.ToUpper();
                        if (temp0.Length > 1) device_type0 = temp0.Substring(0, 1);
                        if (!LadderProperty.DEVICE.TestDevice(temp0) || device_type0 != "X")
                        {
                            CallBackUI.numWordTextBox.字串更換("", NumWordTextBox_Axis_Input[k, i]);
                        }
                        else Axis_Input_position[k, i] = temp0;
                        CallBackUI.numWordTextBox.Enable(false, NumWordTextBox_Axis_Input[k, i]);
                    }
                }
            }
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 1; i++)
                {
                    temp0 = "";
                    device_type0 = "";
                    CallBackUI.numWordTextBox.取得字串(ref temp0, NumWordTextBox_Axis_Output[k, i]);
                    if (temp0 != null)
                    {
                        temp0 = temp0.ToUpper();
                        if (temp0.Length > 1) device_type0 = temp0.Substring(0, 1);
                        if (!LadderProperty.DEVICE.TestDevice(temp0) || device_type0 != "Y")
                        {
                            CallBackUI.numWordTextBox.字串更換("", NumWordTextBox_Axis_Output[k, i]);
                        }
                        else Axis_Output_position[k, i] = temp0;
                        CallBackUI.numWordTextBox.Enable(false, NumWordTextBox_Axis_Output[k, i]);
                    }
                }
            }
            for (int k = 0; k < 4; k++)
            {
                CallBackUI.numTextBox.取得字串(ref temp0, NumTextBox_Axis_對應軸號[k, 0]);
                CallBackUI.numTextBox.Enable(false, NumTextBox_Axis_對應軸號[k, 0]);
                if (!Int32.TryParse(temp0, out Axis_對應軸號[k, 0]))
                {
                    Axis_對應軸號[k, 0] = 9999;
                }
                CallBackUI.numTextBox.字串更換(Axis_對應軸號[k, 0].ToString(), NumTextBox_Axis_對應軸號[k, 0]);
                for (int i = 0; i < 10; i++)
                {
                    CallBackUI.numTextBox.Enable(false, NumTextBox_Axis_運動參數[k, i]);
                    CallBackUI.numTextBox.Enable(false, NumTextBox_Axis_高速輸出入參數[k, i]);
                }
            }
            SaveProperties();
            Thread.Sleep(2000);
            Axis_init();
            ThreadStart.Trigger();
            ThreadOutput.Trigger();
            FLAG_E400_Isopen = true;
            cnt++;
        }
        void cnt_Open_E400_250_開啟失敗(ref byte cnt)
        {
            SLD_Basic.ErrorCode(SLDMclass.SLDM_Close(card_num), true);
            cnt++;
        }
        void cnt_Open_E400_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        #region Close_E400
        byte cnt_Close_E400 = 255;
        void sub_Close_E400()
        {
            if (cnt_Close_E400 == 1)
            {
                SLD_Basic.ErrorCode(SLDMclass.SLDM_Close(card_num), true);
                FLAG_E400_Isopen = false;
                for (int k = 0; k < 2; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        CallBackUI.numWordTextBox.Enable(true, NumWordTextBox_Input[k, i]);
                        CallBackUI.numWordTextBox.Enable(true, NumWordTextBox_Output[k, i]);
                    }
                }
                for (int k = 0; k < 4; k++)
                {
                    CallBackUI.numTextBox.Enable(true, NumTextBox_Axis_對應軸號[k, 0]);
                    for (int i = 0; i < 10; i++)
                    {
                        CallBackUI.numTextBox.Enable(true, NumTextBox_Axis_運動參數[k, i]);
                        CallBackUI.numTextBox.Enable(true, NumTextBox_Axis_高速輸出入參數[k, i]);
                    }
                }
                for (int k = 0; k < 4; k++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        CallBackUI.numWordTextBox.Enable(true, NumWordTextBox_Axis_Input[k, i]);
                    }
                }
                for (int k = 0; k < 4; k++)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        CallBackUI.numWordTextBox.Enable(true, NumWordTextBox_Axis_Output[k, i]);
                    }
                }
                cnt_Close_E400 = 255;
            }
        }
        #endregion
        #region Get_Input
        void sub_Get_Input()
        {
            if(FLAG_E400_Isopen)
            {
                ushort flag = 0;
                uint temp = 0xFF;
                for (int i = 0; i < 16; i++)
                {
                    if (i >= 0 && i < 8)
                    {                     
                        SLDMclass.SLDM_GetInBit(card_num, m_nAddr, (short)i, ref flag);
                        Input[0, i] = flag == 1 ? true : false;
                    }
                    else if (i >= 8 && i < 16)
                    {
                        SLDMclass.SLDM_GetInBit(card_num, m_nAddr, (short)i, ref flag);
                        Input[1, i - 8] = flag == 1 ? true : false;
                    }                                
                }

                SLDMclass.SLDM_GetPOT(card_num, ref temp);
                for (int k = 0; k < 4; k++)
                {
                    Axis_Input[k, Axis輸入.正極限] = myConvert.UInt32GetBit(temp, k);
                }
                SLDMclass.SLDM_GetNOT(card_num, ref temp);
                for (int k = 0; k < 4; k++)
                {
                    Axis_Input[k, Axis輸入.負極限] = myConvert.UInt32GetBit(temp, k);
                }
                SLDMclass.SLDM_GetHome(card_num, ref temp);
                for (int k = 0; k < 4; k++)
                {
                    Axis_Input[k, Axis輸入.原點] = myConvert.UInt32GetBit(temp, k);
                }
            }       
        }
        #endregion
        #region Set_Output
        void sub_Set_Output()
        {
            if (FLAG_E400_Isopen)
            {
                ushort flag = 0;
                //ushort write_byte = 0;
                for (int i = 0; i < 16; i++)
                {
                    if (i >= 0 && i < 8)
                    {
                        if (Output[0, i]) flag = 1;
                        else flag = 0;
                        //write_byte = myConvert.UInt16SetBit(Output[0, i], write_byte, i);
                        SLDMclass.SLDM_SetOutBit(card_num, m_nAddr, (short)i, flag);
                    }
                    else if (i >= 8 && i < 16)
                    {
                        if (Output[1, i - 8]) flag = 1;
                        else flag = 0;
                       // write_byte = myConvert.UInt16SetBit(Output[1, i - 8], write_byte, i);
                        SLDMclass.SLDM_SetOutBit(card_num, m_nAddr, (short)i, flag);
                    }
                   // SLDMclass.SLDM_SetOutWord(card_num, m_nAddr, 0, (ushort)write_byte);
                }

            }
        }
        #endregion

        #region WriteToPLC
        void sub_WriteToPLC()
        {
            if (FLAG_E400_Isopen && lowerMachine != null)
            {
                bool flag = false;
                string device = "";

                for (int k = 0; k < 4; k++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag = Axis_Input[k, i];
                        device = Axis_Input_position[k, i];
                        if (device != "" && device != null) lowerMachine.properties.device_system.Set_Device(device, flag);  
                    }
                }

                for (int k = 0; k < 2; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        flag = Input[k, i];
                        device = Input_position[k, i];
                        if (device != "" && device != null) lowerMachine.properties.device_system.Set_Device(device, flag);                    
                    }
                }
                for (int k = 0; k < 4; k++)
                {
                    if (Axis_對應軸號[k, 0] != 9999)
                    {
                        if (!Axis_status[k, Axis狀態.更改位置致能])
                        {
                            device = 運動參數.device_type + (運動參數.device_num + Axis_對應軸號[k, 0] * 10).ToString();
                            if (device != "" && device != null) lowerMachine.properties.Device.Set_Device(device, Axis_運動參數[k, 運動參數.現在位置]);
                            device = 高速輸出入參數.device_type + (高速輸出入參數.device_num + Axis_對應軸號[k, 0] * 10).ToString();
                            if (device != "" && device != null) lowerMachine.properties.Device.Set_Device(device, Axis_高速輸出入參數[k, 高速輸出入參數.現在位置]);   
                        }
                        device = Axis狀態.device_type + (Axis狀態.device_num + Axis_對應軸號[k, 0] * 10).ToString();
                        if (device != "" && device != null) lowerMachine.properties.Device.Set_Device(device, Axis_status[k, Axis狀態.Axis_Busy]);
                    }
                }
            }
        }
        #endregion
        #region ReadFromPLC
        void sub_ReadFromPLC()
        {
            if (FLAG_E400_Isopen && lowerMachine != null)
            {
                object flag;
                string device = "";
                for (int k = 0; k < 4; k++)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        device = Axis_Output_position[k, i];
                        if (device != "" && device != null)
                        {
                            lowerMachine.properties.device_system.Get_Device(device, out flag);
                            bool temp = (bool)flag;
                            Axis_Output[k, i] = temp;
                        }
                    }
                }
                for (int k = 0; k < 2; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        device = Output_position[k, i];
                        if (device != "" && device != null) 
                        {
                            lowerMachine.properties.device_system.Get_Device(device,out flag);
                            bool temp = (bool)flag;
                            Output[k, i] = temp;                     
                        }
                    }
                }
                object value;
                for (int k = 0; k < 4; k++)
                {
                    if (Axis_對應軸號[k, 0] != 9999)
                    {
                        for (int i = 9; i >= 0; i--)
                        {
                            if (i == Axis狀態.Axis_Busy)
                            {
                                continue;
                            }
                            device = Axis狀態.device_type + (Axis狀態.device_num + Axis_對應軸號[k, 0] * 10 + i).ToString();
                            lowerMachine.properties.Device.Get_Device(device, out value);
                            Axis_status[k, i] = (bool)value;

                        }
                        for (int i = 9; i >= 0; i--)
                        {
                            device = 運動參數.device_type + (運動參數.device_num + Axis_對應軸號[k, 0] * 10 + i).ToString();
                            if (i == 運動參數.現在位置)
                            {
                                if (Axis_status[k, Axis狀態.更改位置致能])
                                {
                                    uint nAxisMask = (uint)(1 << k);
                                    lowerMachine.properties.Device.Get_Device(device, out value);
                                    Axis_運動參數[k, i] = (int)value;
                                    SLDMclass.SLDM_SetPosC(card_num, nAxisMask, Axis_運動參數[k, i]);
                                }
                                continue;
                            }
                            lowerMachine.properties.Device.Get_Device(device, out value);                         
                            Axis_運動參數[k, i] = (int)value;                          
                        }
                        for (int i = 9; i >= 0; i--)
                        {
                            device = 高速輸出入參數.device_type + (高速輸出入參數.device_num + Axis_對應軸號[k, 0] * 10 + i).ToString();
                            if (i == 高速輸出入參數.現在位置)
                            {
                                if (Axis_status[k, Axis狀態.更改位置致能])
                                {
                                    uint nAxisMask = (uint)(1 << k);
                                    lowerMachine.properties.Device.Get_Device(device, out value);
                                    Axis_高速輸出入參數[k, i] = (int)value;
                                }
                                continue;
                            }
                            lowerMachine.properties.Device.Get_Device(device, out value);
                            Axis_高速輸出入參數[k, i] = (int)value;
                        }
                    }
                }
            }
        }
        #endregion

        #region Run_Axis
        bool flag_init = false;
        uint nAxisMask;
        delegate void MainThreadInvoe();
        void ESTOP()
        {
            SLDMclass.SLDM_EStop(card_num, nAxisMask);
        }
        void sub_Run_Axis()
        {
            if (FLAG_E400_Isopen && lowerMachine != null)
            {
                short temp0;
     
                float basic_speed;
                float active_speed;
                int target_position;
                float acc;
                float dec;
                float acc_temp = 0;
                float dec_temp = 0;
                for (int k = 0; k < 4; k++)
                {
                    nAxisMask = (uint)(1 << k);
                    for (int i = 0; i < 1; i++)
                    {
                        if (Axis_Output[0, i]) temp0 = 1;
                        else temp0 = 0;
                        SLDMclass.SLDM_ServoOn(card_num, nAxisMask, temp0);
                    }
                }
                for (int k = 0; k < 4; k++)
                {
                    nAxisMask = (uint)(1 << k);
                    for (int i = 0; i < 10; i++)
                    {
                        if (Axis_status_buf[k, i] != Axis_status[k, i] || (!flag_init))
                        {
                            Axis_status_buf[k, i] = Axis_status[k, i];
                            if (i == Axis狀態.脈衝輸出模式)
                            {
                                if (Axis_status[k, Axis狀態.脈衝輸出模式]) temp0 = 0;
                                else temp0 = 1;
                                SLDMclass.SLDM_SetPulseMode(card_num, nAxisMask, temp0); //設定脈波輸出形式  
                            }
                            else if (i == Axis狀態.正極限致能)
                            {
                                Axis_Set_PoT(k, Axis_status[k, Axis狀態.正極限致能]);
                            }
                            else if (i == Axis狀態.負極限致能)
                            {
                                Axis_Set_NoT(k, Axis_status[k, Axis狀態.負極限致能]);
                            }
                        }
                    }
                }

                for (int k = 0; k < 4; k++)
                {
                    bool paulse = false;
                    nAxisMask = (uint)(1 << k);
                    basic_speed = Math.Abs(Axis_運動參數[k, 運動參數.基底速度]);
                    if (basic_speed < 1) basic_speed = 1;
                    basic_speed = Math.Abs(Axis_運動參數[k, 運動參數.基底速度] / 1000F);
                    active_speed = Math.Abs(Axis_運動參數[k, 運動參數.運轉速度]);
                    if (active_speed < 1) active_speed = 1;
                    active_speed = Math.Abs(Axis_運動參數[k, 運動參數.運轉速度] / 1000F);
                    target_position = Axis_運動參數[k, 運動參數.運轉目標位置];
                    if (basic_speed > active_speed) basic_speed = active_speed;
                    acc_temp = (float)Axis_運動參數[k, 運動參數.加速度];
                    dec_temp = (float)Axis_運動參數[k, 運動參數.減速度];
                    if (acc_temp < 10) acc_temp = 10;
                    if (dec_temp < 10) dec_temp = 10;
                    acc = (active_speed - basic_speed) / (acc_temp);
                    dec = (active_speed - basic_speed) / (dec_temp);
                    if (Axis_運動參數_Buf[k] != Axis_運動參數[k, 運動參數.運動命令碼])
                    {
                        Axis_運動參數_Buf[k] = Axis_運動參數[k, 運動參數.運動命令碼];
                        paulse = true;
                    }

                    paulse = true;
                    if (Axis_運動參數[k, 運動參數.運動命令碼] == 0) // 停止
                    {
                        if (paulse)
                        {
                            SLD_Basic.ErrorCode(SLDMclass.SLDM_EStop(card_num, nAxisMask), true);
                        }                      
                    }
                    else if (Axis_運動參數[k, 運動參數.運動命令碼] == 1) //DDRVA           
                    {
                        if (paulse)
                        {
                            DRVA_init(card_num, k, active_speed, basic_speed, acc, dec, target_position);
                            DRVA(card_num, k);
                        }

                    }
                    else if (Axis_運動參數[k, 運動參數.運動命令碼] == 2) //DDRVI
                    {
                        if (paulse)
                        {
                            DRVI_init(card_num, k, active_speed, basic_speed, acc, dec, target_position);
                            DRVI(card_num, k);
                        }
                    }
                    else if (Axis_運動參數[k, 運動參數.運動命令碼] == 3) //PLSV
                    {
                        if (paulse)
                        {
                            active_speed = Axis_運動參數[k, 運動參數.運轉速度] / 1000F;
                            basic_speed = active_speed;
                            acc = (active_speed - basic_speed);
                            dec = (active_speed - basic_speed);
                            PLSV_init(card_num, k, Math.Abs(active_speed), basic_speed, acc, dec);
                            if (active_speed > 0) PLSV(card_num, k, true);
                            if (active_speed < 0) PLSV(card_num, k, false);
                        }
                    }
                    else if (Axis_運動參數[k, 運動參數.運動命令碼] == 4) //PLSV(加減速度)
                    {
                        if (paulse)
                        {
                            active_speed = Axis_運動參數[k, 運動參數.運轉速度] / 1000F;
                            PLSV_init(card_num, k, Math.Abs(active_speed), basic_speed, acc, dec);
                            if (active_speed > 0) PLSV(card_num, k, true);
                            if (active_speed < 0) PLSV(card_num, k, false);
                        }
                    }
                    
      

                }
                flag_init = true;
            }
        }
        #endregion
        #region Run_HighSpeedCounter
        void sub_Run_HighSpeedCounter()
        {
            if (FLAG_E400_Isopen && lowerMachine != null)
            {
 
            }
        }
        #endregion
        #region Run_CompareOut
        void sub_Run_CompareOut()
        {          
            if (FLAG_E400_Isopen && lowerMachine != null)
            {
                uint nAxisMask;
                for (int k = 0; k < 4; k++)
                {
                    if (Axis_status[k, Axis狀態.位置比較輸出致能] != Axis_高速輸出入致能_Buf[k])
                    {
                        nAxisMask = (uint)(1 << k);
                        if (Axis_status[k, Axis狀態.位置比較輸出致能])
                        {                      
                            short npos = (short)Axis_高速輸出入參數[k, 高速輸出入參數.比較位置數量];
                            int[] pos = new int[npos];
                            short src;
                            if (npos > 8) npos = 8;
                            if (npos < 0) npos = 0;
                            for (int i = 0; i < npos; i ++ )
                            {
                                pos[i] = Axis_高速輸出入參數[k , i + 2];
                            }
                            if (!Axis_status[k, Axis狀態.位置比較方式]) src = 0;
                            else src = 1;
                            SLDMclass.SLDM_CmprEnd(card_num, nAxisMask);//先结束上一次的位置比较设置
                            SLDMclass.SLDM_SetCmprSrc(card_num, nAxisMask, src); //0表示使用命令位置，1表示使用编码器位置
                            SLDMclass.SLDM_SetCmprQEnable(card_num, nAxisMask, 1);//0表示对应的输出端口不输出脉冲，1表示对应的输出端口输出脉冲
                            SLDMclass.SLDM_SetCmprQPol(card_num, nAxisMask, 1);//对应的输出端口低电平有效，1表示高电平有效
                            SLDMclass.SLDM_SetCmprQWidth(card_num, nAxisMask, 5);//表示对应IO输出为200ns,取值范围为2~15
                            SLDMclass.SLDM_SetCmprPos(card_num, nAxisMask, pos, npos);
                            SLDMclass.SLDM_CmprBegin(card_num, nAxisMask);//启动位置比较功能
                        }
      
                        Axis_高速輸出入致能_Buf[k] = Axis_status[k, Axis狀態.位置比較輸出致能];
                    }

                }
            }
        }
        #endregion
        #region Get_Axis_Status
        void sub_Get_Axis_Status()
        {
            uint nAxisMask;
            int[] pos = new int[8];
            uint status = 0xFF;
            bool status_flag = false;
            nAxisMask = (uint)0xFF;
            SLDMclass.SLDM_GetPosC(card_num, nAxisMask, pos);
            SLDMclass.SLDM_GetStopped(card_num, ref status);

            for (int k = 0; k < 4; k++)
            {
                Axis_運動參數[k, 0] = pos[k];
                if (!myConvert.UInt32GetBit(status, k)) status_flag = true;
                else status_flag = false;
                Axis_status[k, 0] = status_flag;             
            }
            
        }
        #endregion

        private void exButton_Open_btnClick(object sender, EventArgs e)
        {
           if(exButton_Open.Load_WriteState())
           {
               if (cnt_Open_E400 == 255) cnt_Open_E400 = 1;
           }
           else
           {
               if (cnt_Close_E400 == 255) cnt_Close_E400 = 1;
           }
        }
        private void comboBox_板卡選擇_SelectedIndexChanged(object sender, EventArgs e)
        {
            card_num = Convert.ToInt16(comboBox_板卡選擇.Text);
        }

        void DRVA_init(short mc, int axis_num, float active_speed, float basic_speed, float acc, float dec, int target_position)
        {
            uint nAxisMask = (uint)(1 << axis_num);
            SLDMclass.SLDM_RstAlm(card_num, nAxisMask);  
            SLDMclass.SLDM_SetVelS(card_num, nAxisMask, basic_speed);//設定基底速度
            SLDMclass.SLDM_SetVelT(card_num, nAxisMask, active_speed);//設定運轉速度
            SLDMclass.SLDM_SetAccT(card_num, nAxisMask, acc);//設定加速度
            SLDMclass.SLDM_SetDecT(card_num, nAxisMask, dec);//設定減速度
            SLDMclass.SLDM_SetPosT(card_num, nAxisMask, target_position);//設定目標位置
           
        }
        delegate void InvokeDelegate(short mc, int axis_num);
        InvokeDelegate InvokeDRVA;
        void DRVA(short mc, int axis_num)
        {
   
            uint nAxisMask = (uint)(1 << axis_num);
            SLDMclass.SLDM_JogA(card_num, nAxisMask);//開始絕對位置P2P動作
            SLDMclass.SLDM_JogA(card_num, nAxisMask);//開始絕對位置P2P動作
        }
        void DRVI_init(short mc, int axis_num, float active_speed, float basic_speed, float acc, float dec, int target_position)
        {
            uint nAxisMask = (uint)(1 << axis_num);
            SLDMclass.SLDM_RstAlm(card_num, nAxisMask); 
            SLDMclass.SLDM_SetVelS(card_num, nAxisMask, basic_speed);//設定基底速度
            SLDMclass.SLDM_SetVelT(card_num, nAxisMask, active_speed);//設定運轉速度
            SLDMclass.SLDM_SetAccT(card_num, nAxisMask, acc);//設定加速度
            SLDMclass.SLDM_SetDecT(card_num, nAxisMask, dec);//設定減速度
            SLDMclass.SLDM_SetPosT(card_num, nAxisMask, target_position);//設定目標位置

        }
        void DRVI(short mc, int axis_num)
        {
            uint nAxisMask = (uint)(1 << axis_num);
            SLDMclass.SLDM_JogI(card_num, nAxisMask);//開始絕對位置P2P動作
            SLDMclass.SLDM_JogI(card_num, nAxisMask);//開始絕對位置P2P動作
        }
        void PLSV_init(short mc, int axis_num, float active_speed, float basic_speed, float acc, float dec)
        {
            uint nAxisMask = (uint)(1 << axis_num);
            SLDMclass.SLDM_SetSmthT(card_num, nAxisMask, 10);
            SLDMclass.SLDM_SetVelS(card_num, nAxisMask, basic_speed);//設定基底速度
            SLDMclass.SLDM_SetVelT(card_num, nAxisMask, active_speed);//設定運轉速度
            SLDMclass.SLDM_SetAccT(card_num, nAxisMask, acc);//設定加速度
            SLDMclass.SLDM_SetDecT(card_num, nAxisMask, dec);//設定減速度
        }
        void PLSV(short mc, int axis_num ,bool dirrection)
        {
            uint nAxisMask = (uint)(1 << axis_num);
            if (dirrection)
            {
                SLDMclass.SLDM_JogP(card_num, nAxisMask);//開始JOG+
                SLDMclass.SLDM_JogP(card_num, nAxisMask);//開始JOG+
            }
            else if (!dirrection)
            {
                SLDMclass.SLDM_JogM(card_num, nAxisMask);//開始JOG-
                SLDMclass.SLDM_JogM(card_num, nAxisMask);//開始JOG-
            }
        }
        void Axis_init()
        {
          //  Thread.Sleep(3000);
            uint nAxisMask;
            for (int k = 0; k < 4; k++)
            {
                nAxisMask = (uint)(1 << k);
                SLDMclass.SLDM_SetPOTOn(card_num, nAxisMask, 0);
                SLDMclass.SLDM_SetPOTPol(card_num, nAxisMask, 1);
                SLDMclass.SLDM_SetNOTOn(card_num, nAxisMask, 0);
                SLDMclass.SLDM_SetNOTPol(card_num, nAxisMask, 1);
                SLDMclass.SLDM_SetHmPol(card_num, nAxisMask, 1);
            }           
        }
        void Axis_Set_PoT(int axis_num,bool Enable)
        {
            uint nAxisMask = (uint)(1 << axis_num);
            if (Enable) SLDMclass.SLDM_SetPOTOn(card_num, nAxisMask, 1);
            else if (!Enable) SLDMclass.SLDM_SetPOTOn(card_num, nAxisMask, 0);
        }
        void Axis_Set_NoT(int axis_num, bool Enable)
        {
            uint nAxisMask = (uint)(1 << axis_num);
            if (Enable) SLDMclass.SLDM_SetNOTOn(card_num, nAxisMask, 1);
            else if (!Enable) SLDMclass.SLDM_SetNOTOn(card_num, nAxisMask, 0);
        }
        void High_Speed_Counter_start(int axis_num)
        {
            uint nAxisMask = (uint)(1 << axis_num);
            SLDMclass.SLDM_SetLatchSrc(card_num, nAxisMask, 0);
            SLDMclass.SLDM_SetLatchTrigger(card_num, nAxisMask, 3);
            SLDMclass.SLDM_SetLatchCmprAxis(card_num, nAxisMask, 0);
            SLDMclass.SLDM_LatchBegin(card_num, nAxisMask);
        }
        void High_Speed_Counter_stop(int axis_num)
        {
            uint nAxisMask = (uint)(1 << axis_num);
            SLDMclass.SLDM_LatchEnd(card_num, nAxisMask);
        }
        short[] High_Speed_Counter_run(int axis_num)
        {
            int[] pos = new int[0];
            uint nAxisMask = (uint)(1 << axis_num);
            short[] numofcapt= new short[100];
            SLDMclass.SLDM_GetLatchPos(card_num, (short)axis_num, pos, numofcapt);
            return numofcapt;
        }
        [Serializable]
        private class SavePropertyFile
        {
            public short card_num;
            public string[,] Input_position = new string[2, 8];
            public string[,] Output_position = new string[2, 8];
            public string[,] Axis_Input_position = new string[4, 3];
            public string[,] Axis_Output_position = new string[4, 1];
            public int[,] Axis_對應軸號 = new int[4, 1];
        }
        private SavePropertyFile savePropertyFile = new SavePropertyFile();
        private void SaveProperties()
        {
            IFormatter binFmt = new BinaryFormatter();
            Stream stream = null;
            savePropertyFile.card_num = card_num.DeepClone();
            savePropertyFile.Input_position = MyFileStream.DeepClone(Input_position);
            savePropertyFile.Output_position = MyFileStream.DeepClone(Output_position);
            savePropertyFile.Axis_Input_position = MyFileStream.DeepClone(Axis_Input_position);
            savePropertyFile.Axis_Output_position = MyFileStream.DeepClone(Axis_Output_position);
            savePropertyFile.Axis_對應軸號 = MyFileStream.DeepClone(Axis_對應軸號);
            try
            {
                stream = File.Open(StreamName , FileMode.Create);
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
            bool flag = true;
            try
            {
                if (File.Exists(".\\" + StreamName))
                {
                    stream = File.Open(".\\" + StreamName, FileMode.Open);
                    try { savePropertyFile = (SavePropertyFile)binFmt.Deserialize(stream); }
                    catch { }

                }
                card_num = savePropertyFile.card_num.DeepClone();
                Input_position = MyFileStream.DeepClone(savePropertyFile.Input_position);
                Output_position = MyFileStream.DeepClone(savePropertyFile.Output_position);
                Axis_Input_position = MyFileStream.DeepClone(savePropertyFile.Axis_Input_position);
                Axis_Output_position = MyFileStream.DeepClone(savePropertyFile.Axis_Output_position);
                Axis_對應軸號 = MyFileStream.DeepClone(savePropertyFile.Axis_對應軸號);
                if (Axis_對應軸號 == null) Axis_對應軸號 = new int[4, 1];
                if (Axis_Input_position == null) Axis_Input_position = new string[4, 3];
                if (Axis_Output_position == null) Axis_Output_position = new string[4, 1];
                if (Input_position == null) Input_position = new string[2, 8];
                if (Output_position == null) Output_position = new string[2, 8];
                if(flag)
                {
                    CallBackUI.comobox.字串更換(card_num.ToString(), comboBox_板卡選擇);
                    for (int k = 0; k < 4; k++)
                    {
                        CallBackUI.numTextBox.字串更換(Axis_對應軸號[k, 0].ToString(), NumTextBox_Axis_對應軸號[k, 0]);
                    }
                    for (int k = 0; k < 4; k++)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            CallBackUI.numWordTextBox.字串更換(Axis_Input_position[k, i], NumWordTextBox_Axis_Input[k, i]);
                        }
                    }
                    for (int k = 0; k < 4; k++)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            CallBackUI.numWordTextBox.字串更換(Axis_Output_position[k, i], NumWordTextBox_Axis_Output[k, i]);
                        }
                    }
                    for (int k = 0; k < 2; k++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            CallBackUI.numWordTextBox.字串更換(Input_position[k, i], NumWordTextBox_Input[k, i]);
                            CallBackUI.numWordTextBox.字串更換(Output_position[k, i], NumWordTextBox_Output[k, i]);
                        }
                    }
                    cnt_Open_E400 = 1;
                }
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            //SaveProperties();
            ThreadUI.Stop();
            ThreadStart.Stop();
            ThreadOutput.Stop();
            for (int i = 0; i < 16; i++)
            {
                if (i >= 0 && i < 8)
                {
     
                    //write_byte = myConvert.UInt16SetBit(Output[0, i], write_byte, i);
                    SLDMclass.SLDM_SetOutBit(card_num, m_nAddr, (short)i, 0);
                }
                else if (i >= 8 && i < 16)
                {
   
                    // write_byte = myConvert.UInt16SetBit(Output[1, i - 8], write_byte, i);
                    SLDMclass.SLDM_SetOutBit(card_num, m_nAddr, (short)i, 0);
                }
                // SLDMclass.SLDM_SetOutWord(card_num, m_nAddr, 0, (ushort)write_byte);
            }
          
        }

    }
}
