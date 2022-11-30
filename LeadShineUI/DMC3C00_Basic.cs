using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using LadderConnection;

namespace LeadShineUI
{
    public partial class DMC3C00_Basic : UserControl
    {
        public class Buffer
        {
            public int[] 計數方式 = new int[12];

            public int[] 運動命令碼 = new int[12];
            public int[] CMP有效電位 = new int[12];
            public int[] JOG速度 = new int[12];
            public bool[] _1P_2P_模式 = new bool[12];
            public bool[] CMP致能 = new bool[12];
            public bool[] ALM低電位有效 = new bool[12];
            public bool[] EL低電位有效 = new bool[12];

            public bool[] Srv_On = new bool[12];

            public bool[] PC_Enable = new bool[12];
        }
        public Buffer buffer = new Buffer();


        public enum enum_Baudrate : int
        {
            _1000Kbs, _800Kbs, _500Kbs, _250Kbs, _125Kbs, _100Kbs,
        }
        private enum_Baudrate _Baudrate = enum_Baudrate._1000Kbs;
        public enum_Baudrate Baudrate
        {
            get
            {
                return this._Baudrate;
            }
            set
            {
                _Baudrate = value;
            }
        }

        private LowerMachine PLC;
        private List<TabPage> List_TabPage = new List<TabPage>();
        private bool flag_Init_OK = false;
        public static readonly int InputNum = 16;
        public static readonly int OutputNum = 16;

        private int _Axis_num = 12;
        public int Axis_num
        {
            get
            {
                return this._Axis_num;
            }
            private set
            {
                this._Axis_num = value;
            }
        }


        private List<PLC_NumBox> List_PLC_NumBox_Axis_Index = new List<PLC_NumBox>();

        private List<PLC_Button> List_PLC_Button_Input = new List<PLC_Button>();
        private List<NumWordTextBox> List_NumWordTextBox_Input = new List<NumWordTextBox>();
        private List<PLC_Button> List_PLC_Button_Output = new List<PLC_Button>();
        private List<CheckBox> List_CheckBox_Output_PCUse = new List<CheckBox>();

        private List<NumWordTextBox> List_NumWordTextBox_Output = new List<NumWordTextBox>();
        private List<PLC_Button[]> List_PLC_Button_Axis_Input = new List<PLC_Button[]>();
        private List<PLC_Button[]> List_PLC_Button_Axis_Output = new List<PLC_Button[]>();


        private List<NumWordTextBox[]> List_NumWordTextBox_Axis_Input = new List<NumWordTextBox[]>();
        public enum enum_Axis_Input : int
        {
            N_EL = 0,
            P_EL = 1,
            ORG = 2,
            EMG = 3,
            EZ = 4,
            INP = 5,
            ALM = 6,
            RDY = 7,
        }

        private List<NumWordTextBox[]> List_NumWordTextBox_Axis_Output = new List<NumWordTextBox[]>();
        public enum enum_Axis_Output : int
        {
            SrvOn = 0,
            ERC = 1,
        }

        private List<PLC_NumBox[]> List_PLC_NumBox_Axis_Parameter = new List<PLC_NumBox[]>();
        public enum enum_Axis_Parameter : int
        {
            現在位置 = 0,
            停止速度 = 1,
            備用_02 = 2,
            S段時間 = 3,
            運轉目標位置 = 4,
            基底速度 = 5,
            運動命令碼 = 6,
            運轉速度 = 7,
            加速度 = 8,
            減速度 = 9,
        }

        private List<PLC_NumBox[]> List_PLC_NumBox_Axis_Counter_Parameter = new List<PLC_NumBox[]>();
        public enum enum_Axis_Counter_Parameter : int
        {
            現在位置 = 0,
            計數方式 = 1,
            CMP比較來源 = 2,
            CMP比較模式 = 3,
            CMP輸出電位 = 4,
            CMP脈衝寬度 = 5,
            CMP比較位置_0 = 6,
            CMP比較位置_1 = 7,
            CMP比較位置_2 = 8,
            CMP比較位置_3 = 9,
        }

        private List<PLC_Button[]> List_PLC_Button_Axis_State = new List<PLC_Button[]>();
        public enum enum_Axis_State : int
        {
            Axis_Busy = 0,
            位置更改致能 = 1,
            JOG加減速致能 = 2,
            _1P_2P_模式 = 3,
            SD減速致能 = 4,
            T_S_速度模式 = 5,
            CMP致能 = 6,
            ALM低電位有效 = 7,
            EL低電位有效 = 8,
            減速停止 = 9,
        }

 

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
        public bool Get_Input(int index)
        {
            return List_PLC_Button_Input[index].GetValue();
        }
        public void Set_Input(int index, bool value)
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
        public void Set_Input_Adress(int index, string Adress)
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
        public string Get_Input_Adress(int index)
        {
            return List_NumWordTextBox_Input[index].Text;
        }
        #endregion
        #region Output
        public void List_CheckBox_Output_PCUse_Init()
        {
            List_CheckBox_Output_PCUse.Add(checkBox_O00_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O01_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O02_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O03_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O04_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O05_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O06_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O07_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O08_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O09_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O10_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O11_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O12_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O13_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O14_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O15_PCUse);
        }
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
        public bool Get_Output(int index)
        {
            return List_PLC_Button_Output[index].GetValue();
        }
        public void Set_Output(int index, bool value)
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
        public void Set_Output_Adress(int index, string Adress)
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
        public string Get_Output_Adress(int index)
        {
            return List_NumWordTextBox_Output[index].Text;
        }
        public void Set_Output_PCUse(int index, bool value)
        {
            this.Invoke(new Action(delegate { this.List_CheckBox_Output_PCUse[index].Checked = value; }));
        }
        public bool Get_Output_PCUse(int index)
        {
            return this.List_CheckBox_Output_PCUse[index].Checked;
        }
        #endregion
        #region Axis_Input
        private void List_PLC_Button_Axis_Input_Init()
        {
            PLC_Button[] pLC_Button_Axis1 = new PLC_Button[8];
            PLC_Button[] pLC_Button_Axis2 = new PLC_Button[8];
            PLC_Button[] pLC_Button_Axis3 = new PLC_Button[8];
            PLC_Button[] pLC_Button_Axis4 = new PLC_Button[8];
            PLC_Button[] pLC_Button_Axis5 = new PLC_Button[8];
            PLC_Button[] pLC_Button_Axis6 = new PLC_Button[8];
            PLC_Button[] pLC_Button_Axis7 = new PLC_Button[8];
            PLC_Button[] pLC_Button_Axis8 = new PLC_Button[8];
            PLC_Button[] pLC_Button_Axis9 = new PLC_Button[8];
            PLC_Button[] pLC_Button_Axis10 = new PLC_Button[8];
            PLC_Button[] pLC_Button_Axis11 = new PLC_Button[8];
            PLC_Button[] pLC_Button_Axis12 = new PLC_Button[8];

            pLC_Button_Axis1[0] = plC_Button_Axis1_I00;
            pLC_Button_Axis1[1] = plC_Button_Axis1_I01;
            pLC_Button_Axis1[2] = plC_Button_Axis1_I02;
            pLC_Button_Axis1[3] = plC_Button_Axis1_I03;
            pLC_Button_Axis1[4] = plC_Button_Axis1_I04;
            pLC_Button_Axis1[5] = plC_Button_Axis1_I05;
            pLC_Button_Axis1[6] = plC_Button_Axis1_I06;
            pLC_Button_Axis1[7] = plC_Button_Axis1_I07;

            pLC_Button_Axis2[0] = plC_Button_Axis2_I00;
            pLC_Button_Axis2[1] = plC_Button_Axis2_I01;
            pLC_Button_Axis2[2] = plC_Button_Axis2_I02;
            pLC_Button_Axis2[3] = plC_Button_Axis2_I03;
            pLC_Button_Axis2[4] = plC_Button_Axis2_I04;
            pLC_Button_Axis2[5] = plC_Button_Axis2_I05;
            pLC_Button_Axis2[6] = plC_Button_Axis2_I06;
            pLC_Button_Axis2[6] = plC_Button_Axis2_I06;
            pLC_Button_Axis2[7] = plC_Button_Axis2_I07;

            pLC_Button_Axis3[0] = plC_Button_Axis3_I00;
            pLC_Button_Axis3[1] = plC_Button_Axis3_I01;
            pLC_Button_Axis3[2] = plC_Button_Axis3_I02;
            pLC_Button_Axis3[3] = plC_Button_Axis3_I03;
            pLC_Button_Axis3[4] = plC_Button_Axis3_I04;
            pLC_Button_Axis3[5] = plC_Button_Axis3_I05;
            pLC_Button_Axis3[6] = plC_Button_Axis3_I06;
            pLC_Button_Axis3[6] = plC_Button_Axis3_I06;
            pLC_Button_Axis3[7] = plC_Button_Axis3_I07;

            pLC_Button_Axis4[0] = plC_Button_Axis4_I00;
            pLC_Button_Axis4[1] = plC_Button_Axis4_I01;
            pLC_Button_Axis4[2] = plC_Button_Axis4_I02;
            pLC_Button_Axis4[3] = plC_Button_Axis4_I03;
            pLC_Button_Axis4[4] = plC_Button_Axis4_I04;
            pLC_Button_Axis4[5] = plC_Button_Axis4_I05;
            pLC_Button_Axis4[6] = plC_Button_Axis4_I06;
            pLC_Button_Axis4[6] = plC_Button_Axis4_I06;
            pLC_Button_Axis4[7] = plC_Button_Axis4_I07;

            pLC_Button_Axis5[0] = plC_Button_Axis5_I00;
            pLC_Button_Axis5[1] = plC_Button_Axis5_I01;
            pLC_Button_Axis5[2] = plC_Button_Axis5_I02;
            pLC_Button_Axis5[3] = plC_Button_Axis5_I03;
            pLC_Button_Axis5[4] = plC_Button_Axis5_I04;
            pLC_Button_Axis5[5] = plC_Button_Axis5_I05;
            pLC_Button_Axis5[6] = plC_Button_Axis5_I06;
            pLC_Button_Axis5[6] = plC_Button_Axis5_I06;
            pLC_Button_Axis5[7] = plC_Button_Axis5_I07;

            pLC_Button_Axis6[0] = plC_Button_Axis6_I00;
            pLC_Button_Axis6[1] = plC_Button_Axis6_I01;
            pLC_Button_Axis6[2] = plC_Button_Axis6_I02;
            pLC_Button_Axis6[3] = plC_Button_Axis6_I03;
            pLC_Button_Axis6[4] = plC_Button_Axis6_I04;
            pLC_Button_Axis6[5] = plC_Button_Axis6_I05;
            pLC_Button_Axis6[6] = plC_Button_Axis6_I06;
            pLC_Button_Axis6[6] = plC_Button_Axis6_I06;
            pLC_Button_Axis6[7] = plC_Button_Axis6_I07;

            pLC_Button_Axis7[0] = plC_Button_Axis7_I00;
            pLC_Button_Axis7[1] = plC_Button_Axis7_I01;
            pLC_Button_Axis7[2] = plC_Button_Axis7_I02;
            pLC_Button_Axis7[3] = plC_Button_Axis7_I03;
            pLC_Button_Axis7[4] = plC_Button_Axis7_I04;
            pLC_Button_Axis7[5] = plC_Button_Axis7_I05;
            pLC_Button_Axis7[6] = plC_Button_Axis7_I06;
            pLC_Button_Axis7[6] = plC_Button_Axis7_I06;
            pLC_Button_Axis7[7] = plC_Button_Axis7_I07;

            pLC_Button_Axis8[0] = plC_Button_Axis8_I00;
            pLC_Button_Axis8[1] = plC_Button_Axis8_I01;
            pLC_Button_Axis8[2] = plC_Button_Axis8_I02;
            pLC_Button_Axis8[3] = plC_Button_Axis8_I03;
            pLC_Button_Axis8[4] = plC_Button_Axis8_I04;
            pLC_Button_Axis8[5] = plC_Button_Axis8_I05;
            pLC_Button_Axis8[6] = plC_Button_Axis8_I06;
            pLC_Button_Axis8[6] = plC_Button_Axis8_I06;
            pLC_Button_Axis8[7] = plC_Button_Axis8_I07;

            pLC_Button_Axis9[0] = plC_Button_Axis9_I00;
            pLC_Button_Axis9[1] = plC_Button_Axis9_I01;
            pLC_Button_Axis9[2] = plC_Button_Axis9_I02;
            pLC_Button_Axis9[3] = plC_Button_Axis9_I03;
            pLC_Button_Axis9[4] = plC_Button_Axis9_I04;
            pLC_Button_Axis9[5] = plC_Button_Axis9_I05;
            pLC_Button_Axis9[6] = plC_Button_Axis9_I06;
            pLC_Button_Axis9[6] = plC_Button_Axis9_I06;
            pLC_Button_Axis9[7] = plC_Button_Axis9_I07;

            pLC_Button_Axis10[0] = plC_Button_Axis10_I00;
            pLC_Button_Axis10[1] = plC_Button_Axis10_I01;
            pLC_Button_Axis10[2] = plC_Button_Axis10_I02;
            pLC_Button_Axis10[3] = plC_Button_Axis10_I03;
            pLC_Button_Axis10[4] = plC_Button_Axis10_I04;
            pLC_Button_Axis10[5] = plC_Button_Axis10_I05;
            pLC_Button_Axis10[6] = plC_Button_Axis10_I06;
            pLC_Button_Axis10[6] = plC_Button_Axis10_I06;
            pLC_Button_Axis10[7] = plC_Button_Axis10_I07;

            pLC_Button_Axis11[0] = plC_Button_Axis11_I00;
            pLC_Button_Axis11[1] = plC_Button_Axis11_I01;
            pLC_Button_Axis11[2] = plC_Button_Axis11_I02;
            pLC_Button_Axis11[3] = plC_Button_Axis11_I03;
            pLC_Button_Axis11[4] = plC_Button_Axis11_I04;
            pLC_Button_Axis11[5] = plC_Button_Axis11_I05;
            pLC_Button_Axis11[6] = plC_Button_Axis11_I06;
            pLC_Button_Axis11[6] = plC_Button_Axis11_I06;
            pLC_Button_Axis11[7] = plC_Button_Axis11_I07;

            pLC_Button_Axis12[0] = plC_Button_Axis12_I00;
            pLC_Button_Axis12[1] = plC_Button_Axis12_I01;
            pLC_Button_Axis12[2] = plC_Button_Axis12_I02;
            pLC_Button_Axis12[3] = plC_Button_Axis12_I03;
            pLC_Button_Axis12[4] = plC_Button_Axis12_I04;
            pLC_Button_Axis12[5] = plC_Button_Axis12_I05;
            pLC_Button_Axis12[6] = plC_Button_Axis12_I06;
            pLC_Button_Axis12[6] = plC_Button_Axis12_I06;
            pLC_Button_Axis12[7] = plC_Button_Axis12_I07;

            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis1);
            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis2);
            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis3);
            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis4);
            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis5);
            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis6);
            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis7);
            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis8);
            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis9);
            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis10);
            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis11);
            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis12);

        }
        public bool Get_Axis_Input(int AxisNum, enum_Axis_Input enum_Axis_Input)
        {
            return this.Get_Axis_Input(AxisNum, (int)enum_Axis_Input);
        }
        public bool Get_Axis_Input(int AxisNum, int index)
        {
            return List_PLC_Button_Axis_Input[AxisNum][index].GetValue();
        }
        public void Set_Axis_Input(int AxisNum, enum_Axis_Input enum_Axis_Input, bool value)
        {
            this.Set_Axis_Input(AxisNum, (int)enum_Axis_Input, value);
        }
        public void Set_Axis_Input(int AxisNum, int index, bool value)
        {
            List_PLC_Button_Axis_Input[AxisNum][index].SetValue(value);
        }

        private void List_NumWordTextBox_Axis_Input_Init()
        {
            NumWordTextBox[] numWordTextBox_Axis1 = new NumWordTextBox[8];
            NumWordTextBox[] numWordTextBox_Axis2 = new NumWordTextBox[8];
            NumWordTextBox[] numWordTextBox_Axis3 = new NumWordTextBox[8];
            NumWordTextBox[] numWordTextBox_Axis4 = new NumWordTextBox[8];
            NumWordTextBox[] numWordTextBox_Axis5 = new NumWordTextBox[8];
            NumWordTextBox[] numWordTextBox_Axis6 = new NumWordTextBox[8];
            NumWordTextBox[] numWordTextBox_Axis7 = new NumWordTextBox[8];
            NumWordTextBox[] numWordTextBox_Axis8 = new NumWordTextBox[8];
            NumWordTextBox[] numWordTextBox_Axis9 = new NumWordTextBox[8];
            NumWordTextBox[] numWordTextBox_Axis10 = new NumWordTextBox[8];
            NumWordTextBox[] numWordTextBox_Axis11 = new NumWordTextBox[8];
            NumWordTextBox[] numWordTextBox_Axis12 = new NumWordTextBox[8];

            numWordTextBox_Axis1[0] = numWordTextBox_Axis1_I00;
            numWordTextBox_Axis1[1] = numWordTextBox_Axis1_I01;
            numWordTextBox_Axis1[2] = numWordTextBox_Axis1_I02;
            numWordTextBox_Axis1[3] = numWordTextBox_Axis1_I03;
            numWordTextBox_Axis1[4] = numWordTextBox_Axis1_I04;
            numWordTextBox_Axis1[5] = numWordTextBox_Axis1_I05;
            numWordTextBox_Axis1[6] = numWordTextBox_Axis1_I06;
            numWordTextBox_Axis1[7] = numWordTextBox_Axis1_I07;

            numWordTextBox_Axis2[0] = numWordTextBox_Axis2_I00;
            numWordTextBox_Axis2[1] = numWordTextBox_Axis2_I01;
            numWordTextBox_Axis2[2] = numWordTextBox_Axis2_I02;
            numWordTextBox_Axis2[3] = numWordTextBox_Axis2_I03;
            numWordTextBox_Axis2[4] = numWordTextBox_Axis2_I04;
            numWordTextBox_Axis2[5] = numWordTextBox_Axis2_I05;
            numWordTextBox_Axis2[6] = numWordTextBox_Axis2_I06;
            numWordTextBox_Axis2[7] = numWordTextBox_Axis2_I07;

            numWordTextBox_Axis3[0] = numWordTextBox_Axis3_I00;
            numWordTextBox_Axis3[1] = numWordTextBox_Axis3_I01;
            numWordTextBox_Axis3[2] = numWordTextBox_Axis3_I02;
            numWordTextBox_Axis3[3] = numWordTextBox_Axis3_I03;
            numWordTextBox_Axis3[4] = numWordTextBox_Axis3_I04;
            numWordTextBox_Axis3[5] = numWordTextBox_Axis3_I05;
            numWordTextBox_Axis3[6] = numWordTextBox_Axis3_I06;
            numWordTextBox_Axis3[7] = numWordTextBox_Axis3_I07;

            numWordTextBox_Axis4[0] = numWordTextBox_Axis4_I00;
            numWordTextBox_Axis4[1] = numWordTextBox_Axis4_I01;
            numWordTextBox_Axis4[2] = numWordTextBox_Axis4_I02;
            numWordTextBox_Axis4[3] = numWordTextBox_Axis4_I03;
            numWordTextBox_Axis4[4] = numWordTextBox_Axis4_I04;
            numWordTextBox_Axis4[5] = numWordTextBox_Axis4_I05;
            numWordTextBox_Axis4[6] = numWordTextBox_Axis4_I06;
            numWordTextBox_Axis4[7] = numWordTextBox_Axis4_I07;

            numWordTextBox_Axis5[0] = numWordTextBox_Axis5_I00;
            numWordTextBox_Axis5[1] = numWordTextBox_Axis5_I01;
            numWordTextBox_Axis5[2] = numWordTextBox_Axis5_I02;
            numWordTextBox_Axis5[3] = numWordTextBox_Axis5_I03;
            numWordTextBox_Axis5[4] = numWordTextBox_Axis5_I04;
            numWordTextBox_Axis5[5] = numWordTextBox_Axis5_I05;
            numWordTextBox_Axis5[6] = numWordTextBox_Axis5_I06;
            numWordTextBox_Axis5[7] = numWordTextBox_Axis5_I07;

            numWordTextBox_Axis6[0] = numWordTextBox_Axis6_I00;
            numWordTextBox_Axis6[1] = numWordTextBox_Axis6_I01;
            numWordTextBox_Axis6[2] = numWordTextBox_Axis6_I02;
            numWordTextBox_Axis6[3] = numWordTextBox_Axis6_I03;
            numWordTextBox_Axis6[4] = numWordTextBox_Axis6_I04;
            numWordTextBox_Axis6[5] = numWordTextBox_Axis6_I05;
            numWordTextBox_Axis6[6] = numWordTextBox_Axis6_I06;
            numWordTextBox_Axis6[7] = numWordTextBox_Axis6_I07;

            numWordTextBox_Axis7[0] = numWordTextBox_Axis7_I00;
            numWordTextBox_Axis7[1] = numWordTextBox_Axis7_I01;
            numWordTextBox_Axis7[2] = numWordTextBox_Axis7_I02;
            numWordTextBox_Axis7[3] = numWordTextBox_Axis7_I03;
            numWordTextBox_Axis7[4] = numWordTextBox_Axis7_I04;
            numWordTextBox_Axis7[5] = numWordTextBox_Axis7_I05;
            numWordTextBox_Axis7[6] = numWordTextBox_Axis7_I06;
            numWordTextBox_Axis7[7] = numWordTextBox_Axis7_I07;

            numWordTextBox_Axis8[0] = numWordTextBox_Axis8_I00;
            numWordTextBox_Axis8[1] = numWordTextBox_Axis8_I01;
            numWordTextBox_Axis8[2] = numWordTextBox_Axis8_I02;
            numWordTextBox_Axis8[3] = numWordTextBox_Axis8_I03;
            numWordTextBox_Axis8[4] = numWordTextBox_Axis8_I04;
            numWordTextBox_Axis8[5] = numWordTextBox_Axis8_I05;
            numWordTextBox_Axis8[6] = numWordTextBox_Axis8_I06;
            numWordTextBox_Axis8[7] = numWordTextBox_Axis8_I07;

            numWordTextBox_Axis9[0] = numWordTextBox_Axis9_I00;
            numWordTextBox_Axis9[1] = numWordTextBox_Axis9_I01;
            numWordTextBox_Axis9[2] = numWordTextBox_Axis9_I02;
            numWordTextBox_Axis9[3] = numWordTextBox_Axis9_I03;
            numWordTextBox_Axis9[4] = numWordTextBox_Axis9_I04;
            numWordTextBox_Axis9[5] = numWordTextBox_Axis9_I05;
            numWordTextBox_Axis9[6] = numWordTextBox_Axis9_I06;
            numWordTextBox_Axis9[7] = numWordTextBox_Axis9_I07;

            numWordTextBox_Axis10[0] = numWordTextBox_Axis10_I00;
            numWordTextBox_Axis10[1] = numWordTextBox_Axis10_I01;
            numWordTextBox_Axis10[2] = numWordTextBox_Axis10_I02;
            numWordTextBox_Axis10[3] = numWordTextBox_Axis10_I03;
            numWordTextBox_Axis10[4] = numWordTextBox_Axis10_I04;
            numWordTextBox_Axis10[5] = numWordTextBox_Axis10_I05;
            numWordTextBox_Axis10[6] = numWordTextBox_Axis10_I06;
            numWordTextBox_Axis10[7] = numWordTextBox_Axis10_I07;

            numWordTextBox_Axis11[0] = numWordTextBox_Axis11_I00;
            numWordTextBox_Axis11[1] = numWordTextBox_Axis11_I01;
            numWordTextBox_Axis11[2] = numWordTextBox_Axis11_I02;
            numWordTextBox_Axis11[3] = numWordTextBox_Axis11_I03;
            numWordTextBox_Axis11[4] = numWordTextBox_Axis11_I04;
            numWordTextBox_Axis11[5] = numWordTextBox_Axis11_I05;
            numWordTextBox_Axis11[6] = numWordTextBox_Axis11_I06;
            numWordTextBox_Axis11[7] = numWordTextBox_Axis11_I07;

            numWordTextBox_Axis12[0] = numWordTextBox_Axis12_I00;
            numWordTextBox_Axis12[1] = numWordTextBox_Axis12_I01;
            numWordTextBox_Axis12[2] = numWordTextBox_Axis12_I02;
            numWordTextBox_Axis12[3] = numWordTextBox_Axis12_I03;
            numWordTextBox_Axis12[4] = numWordTextBox_Axis12_I04;
            numWordTextBox_Axis12[5] = numWordTextBox_Axis12_I05;
            numWordTextBox_Axis12[6] = numWordTextBox_Axis12_I06;
            numWordTextBox_Axis12[7] = numWordTextBox_Axis12_I07;

            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis1);
            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis2);
            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis3);
            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis4);
            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis5);
            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis6);
            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis7);
            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis8);
            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis9);
            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis10);
            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis11);
            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis12);
        }
        public string Get_Axis_Input_Adress(int AxisNum, enum_Axis_Input enum_Axis_Input)
        {
            return this.Get_Axis_Input_Adress(AxisNum, (int)enum_Axis_Input);
        }
        public string Get_Axis_Input_Adress(int AxisNum, int index)
        {
            return List_NumWordTextBox_Axis_Input[AxisNum][index].Text;
        }
        public void Set_Axis_Input_Adress(int AxisNum, enum_Axis_Input enum_Axis_Input, string Adress)
        {
            this.Set_Axis_Input_Adress(AxisNum, (int)enum_Axis_Input, Adress);
        }
        public void Set_Axis_Input_Adress(int AxisNum, int index, string Adress)
        {
            if (LadderProperty.DEVICE.TestDevice(Adress))
            {
                string temp = Adress.Remove(1);
                if (temp == "X")
                {
                    this.Invoke(new Action(delegate { List_NumWordTextBox_Axis_Input[AxisNum][index].Text = Adress; }));
                }
            }
        }
        #endregion
        #region Axis_Output
        private void List_PLC_Button_Axis_Output_Init()
        {
            PLC_Button[] pLC_Button_Axis1 = new PLC_Button[2];
            PLC_Button[] pLC_Button_Axis2 = new PLC_Button[2];
            PLC_Button[] pLC_Button_Axis3 = new PLC_Button[2];
            PLC_Button[] pLC_Button_Axis4 = new PLC_Button[2];
            PLC_Button[] pLC_Button_Axis5 = new PLC_Button[2];
            PLC_Button[] pLC_Button_Axis6 = new PLC_Button[2];
            PLC_Button[] pLC_Button_Axis7 = new PLC_Button[2];
            PLC_Button[] pLC_Button_Axis8 = new PLC_Button[2];
            PLC_Button[] pLC_Button_Axis9 = new PLC_Button[2];
            PLC_Button[] pLC_Button_Axis10 = new PLC_Button[2];
            PLC_Button[] pLC_Button_Axis11 = new PLC_Button[2];
            PLC_Button[] pLC_Button_Axis12 = new PLC_Button[2];

            pLC_Button_Axis1[0] = plC_Button_Axis1_O00;
            pLC_Button_Axis1[1] = plC_Button_Axis1_O01;

            pLC_Button_Axis2[0] = plC_Button_Axis2_O00;
            pLC_Button_Axis2[1] = plC_Button_Axis2_O01;

            pLC_Button_Axis3[0] = plC_Button_Axis3_O00;
            pLC_Button_Axis3[1] = plC_Button_Axis3_O01;

            pLC_Button_Axis4[0] = plC_Button_Axis4_O00;
            pLC_Button_Axis4[1] = plC_Button_Axis4_O01;

            pLC_Button_Axis5[0] = plC_Button_Axis5_O00;
            pLC_Button_Axis5[1] = plC_Button_Axis5_O01;

            pLC_Button_Axis6[0] = plC_Button_Axis6_O00;
            pLC_Button_Axis6[1] = plC_Button_Axis6_O01;

            pLC_Button_Axis7[0] = plC_Button_Axis7_O00;
            pLC_Button_Axis7[1] = plC_Button_Axis7_O01;

            pLC_Button_Axis8[0] = plC_Button_Axis8_O00;
            pLC_Button_Axis8[1] = plC_Button_Axis8_O01;

            pLC_Button_Axis9[0] = plC_Button_Axis9_O00;
            pLC_Button_Axis9[1] = plC_Button_Axis9_O01;

            pLC_Button_Axis10[0] = plC_Button_Axis10_O00;
            pLC_Button_Axis10[1] = plC_Button_Axis10_O01;

            pLC_Button_Axis11[0] = plC_Button_Axis11_O00;
            pLC_Button_Axis11[1] = plC_Button_Axis11_O01;

            pLC_Button_Axis12[0] = plC_Button_Axis12_O00;
            pLC_Button_Axis12[1] = plC_Button_Axis12_O01;


            List_PLC_Button_Axis_Output.Add(pLC_Button_Axis1);
            List_PLC_Button_Axis_Output.Add(pLC_Button_Axis2);
            List_PLC_Button_Axis_Output.Add(pLC_Button_Axis3);
            List_PLC_Button_Axis_Output.Add(pLC_Button_Axis4);
            List_PLC_Button_Axis_Output.Add(pLC_Button_Axis5);
            List_PLC_Button_Axis_Output.Add(pLC_Button_Axis6);
            List_PLC_Button_Axis_Output.Add(pLC_Button_Axis7);
            List_PLC_Button_Axis_Output.Add(pLC_Button_Axis8);
            List_PLC_Button_Axis_Output.Add(pLC_Button_Axis9);
            List_PLC_Button_Axis_Output.Add(pLC_Button_Axis10);
            List_PLC_Button_Axis_Output.Add(pLC_Button_Axis11);
            List_PLC_Button_Axis_Output.Add(pLC_Button_Axis12);
        }
        public bool Get_Axis_Output(int AxisNum, enum_Axis_Output enum_Axis_Output)
        {
            return this.Get_Axis_Output(AxisNum, (int)enum_Axis_Output);
        }
        public bool Get_Axis_Output(int AxisNum, int index)
        {
            return List_PLC_Button_Axis_Output[AxisNum][index].GetValue();
        }
        public void Set_Axis_Output(int AxisNum, enum_Axis_Output enum_Axis_Output, bool value)
        {
            this.Set_Axis_Output(AxisNum, (int)enum_Axis_Output, value);
        }
        public void Set_Axis_Output(int AxisNum, int index, bool value)
        {
            List_PLC_Button_Axis_Output[AxisNum][index].SetValue(value);
        }

        private void List_NumWordTextBox_Axis_Output_Init()
        {
            NumWordTextBox[] numWordTextBox_Axis1 = new NumWordTextBox[3];
            NumWordTextBox[] numWordTextBox_Axis2 = new NumWordTextBox[3];
            NumWordTextBox[] numWordTextBox_Axis3 = new NumWordTextBox[3];
            NumWordTextBox[] numWordTextBox_Axis4 = new NumWordTextBox[3];
            NumWordTextBox[] numWordTextBox_Axis5 = new NumWordTextBox[3];
            NumWordTextBox[] numWordTextBox_Axis6 = new NumWordTextBox[3];
            NumWordTextBox[] numWordTextBox_Axis7 = new NumWordTextBox[3];
            NumWordTextBox[] numWordTextBox_Axis8 = new NumWordTextBox[3];
            NumWordTextBox[] numWordTextBox_Axis9 = new NumWordTextBox[3];
            NumWordTextBox[] numWordTextBox_Axis10 = new NumWordTextBox[3];
            NumWordTextBox[] numWordTextBox_Axis11 = new NumWordTextBox[3];
            NumWordTextBox[] numWordTextBox_Axis12 = new NumWordTextBox[3];

            numWordTextBox_Axis1[0] = numWordTextBox_Axis1_O00;
            numWordTextBox_Axis1[1] = numWordTextBox_Axis1_O01;
            numWordTextBox_Axis1[2] = numWordTextBox_Axis1_O01;

            numWordTextBox_Axis2[0] = numWordTextBox_Axis2_O00;
            numWordTextBox_Axis2[1] = numWordTextBox_Axis2_O01;
            numWordTextBox_Axis2[2] = numWordTextBox_Axis2_O01;

            numWordTextBox_Axis3[0] = numWordTextBox_Axis3_O00;
            numWordTextBox_Axis3[1] = numWordTextBox_Axis3_O01;
            numWordTextBox_Axis3[2] = numWordTextBox_Axis3_O01;

            numWordTextBox_Axis4[0] = numWordTextBox_Axis4_O00;
            numWordTextBox_Axis4[1] = numWordTextBox_Axis4_O01;
            numWordTextBox_Axis4[2] = numWordTextBox_Axis4_O01;

            numWordTextBox_Axis5[0] = numWordTextBox_Axis5_O00;
            numWordTextBox_Axis5[1] = numWordTextBox_Axis5_O01;
            numWordTextBox_Axis5[2] = numWordTextBox_Axis5_O01;

            numWordTextBox_Axis6[0] = numWordTextBox_Axis6_O00;
            numWordTextBox_Axis6[1] = numWordTextBox_Axis6_O01;
            numWordTextBox_Axis6[2] = numWordTextBox_Axis6_O01;

            numWordTextBox_Axis7[0] = numWordTextBox_Axis7_O00;
            numWordTextBox_Axis7[1] = numWordTextBox_Axis7_O01;
            numWordTextBox_Axis7[2] = numWordTextBox_Axis7_O01;

            numWordTextBox_Axis8[0] = numWordTextBox_Axis8_O00;
            numWordTextBox_Axis8[1] = numWordTextBox_Axis8_O01;
            numWordTextBox_Axis8[2] = numWordTextBox_Axis8_O01;

            numWordTextBox_Axis9[0] = numWordTextBox_Axis9_O00;
            numWordTextBox_Axis9[1] = numWordTextBox_Axis9_O01;
            numWordTextBox_Axis9[2] = numWordTextBox_Axis9_O01;

            numWordTextBox_Axis10[0] = numWordTextBox_Axis10_O00;
            numWordTextBox_Axis10[1] = numWordTextBox_Axis10_O01;
            numWordTextBox_Axis10[2] = numWordTextBox_Axis10_O01;

            numWordTextBox_Axis11[0] = numWordTextBox_Axis11_O00;
            numWordTextBox_Axis11[1] = numWordTextBox_Axis11_O01;
            numWordTextBox_Axis11[2] = numWordTextBox_Axis11_O01;

            numWordTextBox_Axis12[0] = numWordTextBox_Axis12_O00;
            numWordTextBox_Axis12[1] = numWordTextBox_Axis12_O01;
            numWordTextBox_Axis12[2] = numWordTextBox_Axis12_O01;


            List_NumWordTextBox_Axis_Output.Add(numWordTextBox_Axis1);
            List_NumWordTextBox_Axis_Output.Add(numWordTextBox_Axis2);
            List_NumWordTextBox_Axis_Output.Add(numWordTextBox_Axis3);
            List_NumWordTextBox_Axis_Output.Add(numWordTextBox_Axis4);
            List_NumWordTextBox_Axis_Output.Add(numWordTextBox_Axis5);
            List_NumWordTextBox_Axis_Output.Add(numWordTextBox_Axis6);
            List_NumWordTextBox_Axis_Output.Add(numWordTextBox_Axis7);
            List_NumWordTextBox_Axis_Output.Add(numWordTextBox_Axis8);
            List_NumWordTextBox_Axis_Output.Add(numWordTextBox_Axis9);
            List_NumWordTextBox_Axis_Output.Add(numWordTextBox_Axis10);
            List_NumWordTextBox_Axis_Output.Add(numWordTextBox_Axis11);
            List_NumWordTextBox_Axis_Output.Add(numWordTextBox_Axis12);

        }
        public string Get_Axis_Output_Adress(int AxisNum, enum_Axis_Output enum_Axis_Output)
        {
            return this.Get_Axis_Output_Adress(AxisNum, (int)enum_Axis_Output);
        }
        public string Get_Axis_Output_Adress(int AxisNum, int index)
        {
            return List_NumWordTextBox_Axis_Output[AxisNum][index].Text;
        }
        public void Set_Axis_Output_Adress(int AxisNum, enum_Axis_Output enum_Axis_Output, string Adress)
        {
            this.Set_Axis_Output_Adress(AxisNum, (int)enum_Axis_Output, Adress);
        }
        public void Set_Axis_Output_Adress(int AxisNum, int index, string Adress)
        {
            if (LadderProperty.DEVICE.TestDevice(Adress))
            {
                string temp = Adress.Remove(1);
                if (temp == "Y")
                {
                    this.Invoke(new Action(delegate { List_NumWordTextBox_Axis_Output[AxisNum][index].Text = Adress; }));
                }
            }
        }
        #endregion
        #region Axis_Parameter
        private void List_PLC_NumBox_Axis_Parameter_Init()
        {
            PLC_NumBox[] pLC_NumBox_Axis1 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis2 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis3 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis4 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis5 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis6 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis7 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis8 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis9 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis10 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis11 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis12 = new PLC_NumBox[10];

            pLC_NumBox_Axis1[0] = plC_NumBox_Axis1_運動參數_D00;
            pLC_NumBox_Axis1[1] = plC_NumBox_Axis1_運動參數_D01;
            pLC_NumBox_Axis1[2] = plC_NumBox_Axis1_運動參數_D02;
            pLC_NumBox_Axis1[3] = plC_NumBox_Axis1_運動參數_D03;
            pLC_NumBox_Axis1[4] = plC_NumBox_Axis1_運動參數_D04;
            pLC_NumBox_Axis1[5] = plC_NumBox_Axis1_運動參數_D05;
            pLC_NumBox_Axis1[6] = plC_NumBox_Axis1_運動參數_D06;
            pLC_NumBox_Axis1[7] = plC_NumBox_Axis1_運動參數_D07;
            pLC_NumBox_Axis1[8] = plC_NumBox_Axis1_運動參數_D08;
            pLC_NumBox_Axis1[9] = plC_NumBox_Axis1_運動參數_D09;

            pLC_NumBox_Axis2[0] = plC_NumBox_Axis2_運動參數_D00;
            pLC_NumBox_Axis2[1] = plC_NumBox_Axis2_運動參數_D01;
            pLC_NumBox_Axis2[2] = plC_NumBox_Axis2_運動參數_D02;
            pLC_NumBox_Axis2[3] = plC_NumBox_Axis2_運動參數_D03;
            pLC_NumBox_Axis2[4] = plC_NumBox_Axis2_運動參數_D04;
            pLC_NumBox_Axis2[5] = plC_NumBox_Axis2_運動參數_D05;
            pLC_NumBox_Axis2[6] = plC_NumBox_Axis2_運動參數_D06;
            pLC_NumBox_Axis2[7] = plC_NumBox_Axis2_運動參數_D07;
            pLC_NumBox_Axis2[8] = plC_NumBox_Axis2_運動參數_D08;
            pLC_NumBox_Axis2[9] = plC_NumBox_Axis2_運動參數_D09;

            pLC_NumBox_Axis3[0] = plC_NumBox_Axis3_運動參數_D00;
            pLC_NumBox_Axis3[1] = plC_NumBox_Axis3_運動參數_D01;
            pLC_NumBox_Axis3[2] = plC_NumBox_Axis3_運動參數_D02;
            pLC_NumBox_Axis3[3] = plC_NumBox_Axis3_運動參數_D03;
            pLC_NumBox_Axis3[4] = plC_NumBox_Axis3_運動參數_D04;
            pLC_NumBox_Axis3[5] = plC_NumBox_Axis3_運動參數_D05;
            pLC_NumBox_Axis3[6] = plC_NumBox_Axis3_運動參數_D06;
            pLC_NumBox_Axis3[7] = plC_NumBox_Axis3_運動參數_D07;
            pLC_NumBox_Axis3[8] = plC_NumBox_Axis3_運動參數_D08;
            pLC_NumBox_Axis3[9] = plC_NumBox_Axis3_運動參數_D09;

            pLC_NumBox_Axis4[0] = plC_NumBox_Axis4_運動參數_D00;
            pLC_NumBox_Axis4[1] = plC_NumBox_Axis4_運動參數_D01;
            pLC_NumBox_Axis4[2] = plC_NumBox_Axis4_運動參數_D02;
            pLC_NumBox_Axis4[3] = plC_NumBox_Axis4_運動參數_D03;
            pLC_NumBox_Axis4[4] = plC_NumBox_Axis4_運動參數_D04;
            pLC_NumBox_Axis4[5] = plC_NumBox_Axis4_運動參數_D05;
            pLC_NumBox_Axis4[6] = plC_NumBox_Axis4_運動參數_D06;
            pLC_NumBox_Axis4[7] = plC_NumBox_Axis4_運動參數_D07;
            pLC_NumBox_Axis4[8] = plC_NumBox_Axis4_運動參數_D08;
            pLC_NumBox_Axis4[9] = plC_NumBox_Axis4_運動參數_D09;

            pLC_NumBox_Axis5[0] = plC_NumBox_Axis5_運動參數_D00;
            pLC_NumBox_Axis5[1] = plC_NumBox_Axis5_運動參數_D01;
            pLC_NumBox_Axis5[2] = plC_NumBox_Axis5_運動參數_D02;
            pLC_NumBox_Axis5[3] = plC_NumBox_Axis5_運動參數_D03;
            pLC_NumBox_Axis5[4] = plC_NumBox_Axis5_運動參數_D04;
            pLC_NumBox_Axis5[5] = plC_NumBox_Axis5_運動參數_D05;
            pLC_NumBox_Axis5[6] = plC_NumBox_Axis5_運動參數_D06;
            pLC_NumBox_Axis5[7] = plC_NumBox_Axis5_運動參數_D07;
            pLC_NumBox_Axis5[8] = plC_NumBox_Axis5_運動參數_D08;
            pLC_NumBox_Axis5[9] = plC_NumBox_Axis5_運動參數_D09;

            pLC_NumBox_Axis6[0] = plC_NumBox_Axis6_運動參數_D00;
            pLC_NumBox_Axis6[1] = plC_NumBox_Axis6_運動參數_D01;
            pLC_NumBox_Axis6[2] = plC_NumBox_Axis6_運動參數_D02;
            pLC_NumBox_Axis6[3] = plC_NumBox_Axis6_運動參數_D03;
            pLC_NumBox_Axis6[4] = plC_NumBox_Axis6_運動參數_D04;
            pLC_NumBox_Axis6[5] = plC_NumBox_Axis6_運動參數_D05;
            pLC_NumBox_Axis6[6] = plC_NumBox_Axis6_運動參數_D06;
            pLC_NumBox_Axis6[7] = plC_NumBox_Axis6_運動參數_D07;
            pLC_NumBox_Axis6[8] = plC_NumBox_Axis6_運動參數_D08;
            pLC_NumBox_Axis6[9] = plC_NumBox_Axis6_運動參數_D09;

            pLC_NumBox_Axis7[0] = plC_NumBox_Axis7_運動參數_D00;
            pLC_NumBox_Axis7[1] = plC_NumBox_Axis7_運動參數_D01;
            pLC_NumBox_Axis7[2] = plC_NumBox_Axis7_運動參數_D02;
            pLC_NumBox_Axis7[3] = plC_NumBox_Axis7_運動參數_D03;
            pLC_NumBox_Axis7[4] = plC_NumBox_Axis7_運動參數_D04;
            pLC_NumBox_Axis7[5] = plC_NumBox_Axis7_運動參數_D05;
            pLC_NumBox_Axis7[6] = plC_NumBox_Axis7_運動參數_D06;
            pLC_NumBox_Axis7[7] = plC_NumBox_Axis7_運動參數_D07;
            pLC_NumBox_Axis7[8] = plC_NumBox_Axis7_運動參數_D08;
            pLC_NumBox_Axis7[9] = plC_NumBox_Axis7_運動參數_D09;

            pLC_NumBox_Axis8[0] = plC_NumBox_Axis8_運動參數_D00;
            pLC_NumBox_Axis8[1] = plC_NumBox_Axis8_運動參數_D01;
            pLC_NumBox_Axis8[2] = plC_NumBox_Axis8_運動參數_D02;
            pLC_NumBox_Axis8[3] = plC_NumBox_Axis8_運動參數_D03;
            pLC_NumBox_Axis8[4] = plC_NumBox_Axis8_運動參數_D04;
            pLC_NumBox_Axis8[5] = plC_NumBox_Axis8_運動參數_D05;
            pLC_NumBox_Axis8[6] = plC_NumBox_Axis8_運動參數_D06;
            pLC_NumBox_Axis8[7] = plC_NumBox_Axis8_運動參數_D07;
            pLC_NumBox_Axis8[8] = plC_NumBox_Axis8_運動參數_D08;
            pLC_NumBox_Axis8[9] = plC_NumBox_Axis8_運動參數_D09;

            pLC_NumBox_Axis9[0] = plC_NumBox_Axis9_運動參數_D00;
            pLC_NumBox_Axis9[1] = plC_NumBox_Axis9_運動參數_D01;
            pLC_NumBox_Axis9[2] = plC_NumBox_Axis9_運動參數_D02;
            pLC_NumBox_Axis9[3] = plC_NumBox_Axis9_運動參數_D03;
            pLC_NumBox_Axis9[4] = plC_NumBox_Axis9_運動參數_D04;
            pLC_NumBox_Axis9[5] = plC_NumBox_Axis9_運動參數_D05;
            pLC_NumBox_Axis9[6] = plC_NumBox_Axis9_運動參數_D06;
            pLC_NumBox_Axis9[7] = plC_NumBox_Axis9_運動參數_D07;
            pLC_NumBox_Axis9[8] = plC_NumBox_Axis9_運動參數_D08;
            pLC_NumBox_Axis9[9] = plC_NumBox_Axis9_運動參數_D09;

            pLC_NumBox_Axis10[0] = plC_NumBox_Axis10_運動參數_D00;
            pLC_NumBox_Axis10[1] = plC_NumBox_Axis10_運動參數_D01;
            pLC_NumBox_Axis10[2] = plC_NumBox_Axis10_運動參數_D02;
            pLC_NumBox_Axis10[3] = plC_NumBox_Axis10_運動參數_D03;
            pLC_NumBox_Axis10[4] = plC_NumBox_Axis10_運動參數_D04;
            pLC_NumBox_Axis10[5] = plC_NumBox_Axis10_運動參數_D05;
            pLC_NumBox_Axis10[6] = plC_NumBox_Axis10_運動參數_D06;
            pLC_NumBox_Axis10[7] = plC_NumBox_Axis10_運動參數_D07;
            pLC_NumBox_Axis10[8] = plC_NumBox_Axis10_運動參數_D08;
            pLC_NumBox_Axis10[9] = plC_NumBox_Axis10_運動參數_D09;

            pLC_NumBox_Axis11[0] = plC_NumBox_Axis11_運動參數_D00;
            pLC_NumBox_Axis11[1] = plC_NumBox_Axis11_運動參數_D01;
            pLC_NumBox_Axis11[2] = plC_NumBox_Axis11_運動參數_D02;
            pLC_NumBox_Axis11[3] = plC_NumBox_Axis11_運動參數_D03;
            pLC_NumBox_Axis11[4] = plC_NumBox_Axis11_運動參數_D04;
            pLC_NumBox_Axis11[5] = plC_NumBox_Axis11_運動參數_D05;
            pLC_NumBox_Axis11[6] = plC_NumBox_Axis11_運動參數_D06;
            pLC_NumBox_Axis11[7] = plC_NumBox_Axis11_運動參數_D07;
            pLC_NumBox_Axis11[8] = plC_NumBox_Axis11_運動參數_D08;
            pLC_NumBox_Axis11[9] = plC_NumBox_Axis11_運動參數_D09;

            pLC_NumBox_Axis12[0] = plC_NumBox_Axis12_運動參數_D00;
            pLC_NumBox_Axis12[1] = plC_NumBox_Axis12_運動參數_D01;
            pLC_NumBox_Axis12[2] = plC_NumBox_Axis12_運動參數_D02;
            pLC_NumBox_Axis12[3] = plC_NumBox_Axis12_運動參數_D03;
            pLC_NumBox_Axis12[4] = plC_NumBox_Axis12_運動參數_D04;
            pLC_NumBox_Axis12[5] = plC_NumBox_Axis12_運動參數_D05;
            pLC_NumBox_Axis12[6] = plC_NumBox_Axis12_運動參數_D06;
            pLC_NumBox_Axis12[7] = plC_NumBox_Axis12_運動參數_D07;
            pLC_NumBox_Axis12[8] = plC_NumBox_Axis12_運動參數_D08;
            pLC_NumBox_Axis12[9] = plC_NumBox_Axis12_運動參數_D09;


            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis1);
            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis2);
            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis3);
            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis4);
            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis5);
            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis6);
            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis7);
            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis8);
            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis9);
            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis10);
            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis11);
            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis12);
        }
        public void Set_Axis_Parameter(int AxisNum, enum_Axis_Parameter enum_Axis_Parameter, int value)
        {
            this.Set_Axis_Parameter(AxisNum, (int)enum_Axis_Parameter, value);
        }
        public void Set_Axis_Parameter(int AxisNum, int index, int value)
        {
            List_PLC_NumBox_Axis_Parameter[AxisNum][index].SetValue(value);
        }
        public int Get_Axis_Parameter(int AxisNum, enum_Axis_Parameter enum_Axis_Parameter)
        {
            return this.Get_Axis_Parameter(AxisNum, (int)enum_Axis_Parameter);
        }
        public int Get_Axis_Parameter(int AxisNum, int index)
        {
            return List_PLC_NumBox_Axis_Parameter[AxisNum][index].GetValue();
        }
        #endregion
        #region Axis_Counter_Parameter
        private void List_PLC_NumBox_Axis_Counter_Parameter_Init()
        {
            PLC_NumBox[] pLC_NumBox_Axis1 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis2 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis3 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis4 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis5 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis6 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis7 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis8 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis9 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis10 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis11 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis12 = new PLC_NumBox[10];

            pLC_NumBox_Axis1[0] = plC_NumBox_Axis1_高速計數參數_R00;
            pLC_NumBox_Axis1[1] = plC_NumBox_Axis1_高速計數參數_R01;
            pLC_NumBox_Axis1[2] = plC_NumBox_Axis1_高速計數參數_R02;
            pLC_NumBox_Axis1[3] = plC_NumBox_Axis1_高速計數參數_R03;
            pLC_NumBox_Axis1[4] = plC_NumBox_Axis1_高速計數參數_R04;
            pLC_NumBox_Axis1[5] = plC_NumBox_Axis1_高速計數參數_R05;
            pLC_NumBox_Axis1[6] = plC_NumBox_Axis1_高速計數參數_R06;
            pLC_NumBox_Axis1[7] = plC_NumBox_Axis1_高速計數參數_R07;
            pLC_NumBox_Axis1[8] = plC_NumBox_Axis1_高速計數參數_R08;
            pLC_NumBox_Axis1[9] = plC_NumBox_Axis1_高速計數參數_R09;

            pLC_NumBox_Axis2[0] = plC_NumBox_Axis2_高速計數參數_R00;
            pLC_NumBox_Axis2[1] = plC_NumBox_Axis2_高速計數參數_R01;
            pLC_NumBox_Axis2[2] = plC_NumBox_Axis2_高速計數參數_R02;
            pLC_NumBox_Axis2[3] = plC_NumBox_Axis2_高速計數參數_R03;
            pLC_NumBox_Axis2[4] = plC_NumBox_Axis2_高速計數參數_R04;
            pLC_NumBox_Axis2[5] = plC_NumBox_Axis2_高速計數參數_R05;
            pLC_NumBox_Axis2[6] = plC_NumBox_Axis2_高速計數參數_R06;
            pLC_NumBox_Axis2[7] = plC_NumBox_Axis2_高速計數參數_R07;
            pLC_NumBox_Axis2[8] = plC_NumBox_Axis2_高速計數參數_R08;
            pLC_NumBox_Axis2[9] = plC_NumBox_Axis2_高速計數參數_R09;

            pLC_NumBox_Axis3[0] = plC_NumBox_Axis3_高速計數參數_R00;
            pLC_NumBox_Axis3[1] = plC_NumBox_Axis3_高速計數參數_R01;
            pLC_NumBox_Axis3[2] = plC_NumBox_Axis3_高速計數參數_R02;
            pLC_NumBox_Axis3[3] = plC_NumBox_Axis3_高速計數參數_R03;
            pLC_NumBox_Axis3[4] = plC_NumBox_Axis3_高速計數參數_R04;
            pLC_NumBox_Axis3[5] = plC_NumBox_Axis3_高速計數參數_R05;
            pLC_NumBox_Axis3[6] = plC_NumBox_Axis3_高速計數參數_R06;
            pLC_NumBox_Axis3[7] = plC_NumBox_Axis3_高速計數參數_R07;
            pLC_NumBox_Axis3[8] = plC_NumBox_Axis3_高速計數參數_R08;
            pLC_NumBox_Axis3[9] = plC_NumBox_Axis3_高速計數參數_R09;

            pLC_NumBox_Axis4[0] = plC_NumBox_Axis4_高速計數參數_R00;
            pLC_NumBox_Axis4[1] = plC_NumBox_Axis4_高速計數參數_R01;
            pLC_NumBox_Axis4[2] = plC_NumBox_Axis4_高速計數參數_R02;
            pLC_NumBox_Axis4[3] = plC_NumBox_Axis4_高速計數參數_R03;
            pLC_NumBox_Axis4[4] = plC_NumBox_Axis4_高速計數參數_R04;
            pLC_NumBox_Axis4[5] = plC_NumBox_Axis4_高速計數參數_R05;
            pLC_NumBox_Axis4[6] = plC_NumBox_Axis4_高速計數參數_R06;
            pLC_NumBox_Axis4[7] = plC_NumBox_Axis4_高速計數參數_R07;
            pLC_NumBox_Axis4[8] = plC_NumBox_Axis4_高速計數參數_R08;
            pLC_NumBox_Axis4[9] = plC_NumBox_Axis4_高速計數參數_R09;

            pLC_NumBox_Axis5[0] = plC_NumBox_Axis5_高速計數參數_R00;
            pLC_NumBox_Axis5[1] = plC_NumBox_Axis5_高速計數參數_R01;
            pLC_NumBox_Axis5[2] = plC_NumBox_Axis5_高速計數參數_R02;
            pLC_NumBox_Axis5[3] = plC_NumBox_Axis5_高速計數參數_R03;
            pLC_NumBox_Axis5[4] = plC_NumBox_Axis5_高速計數參數_R04;
            pLC_NumBox_Axis5[5] = plC_NumBox_Axis5_高速計數參數_R05;
            pLC_NumBox_Axis5[6] = plC_NumBox_Axis5_高速計數參數_R06;
            pLC_NumBox_Axis5[7] = plC_NumBox_Axis5_高速計數參數_R07;
            pLC_NumBox_Axis5[8] = plC_NumBox_Axis5_高速計數參數_R08;
            pLC_NumBox_Axis5[9] = plC_NumBox_Axis5_高速計數參數_R09;

            pLC_NumBox_Axis6[0] = plC_NumBox_Axis6_高速計數參數_R00;
            pLC_NumBox_Axis6[1] = plC_NumBox_Axis6_高速計數參數_R01;
            pLC_NumBox_Axis6[2] = plC_NumBox_Axis6_高速計數參數_R02;
            pLC_NumBox_Axis6[3] = plC_NumBox_Axis6_高速計數參數_R03;
            pLC_NumBox_Axis6[4] = plC_NumBox_Axis6_高速計數參數_R04;
            pLC_NumBox_Axis6[5] = plC_NumBox_Axis6_高速計數參數_R05;
            pLC_NumBox_Axis6[6] = plC_NumBox_Axis6_高速計數參數_R06;
            pLC_NumBox_Axis6[7] = plC_NumBox_Axis6_高速計數參數_R07;
            pLC_NumBox_Axis6[8] = plC_NumBox_Axis6_高速計數參數_R08;
            pLC_NumBox_Axis6[9] = plC_NumBox_Axis6_高速計數參數_R09;

            pLC_NumBox_Axis7[0] = plC_NumBox_Axis7_高速計數參數_R00;
            pLC_NumBox_Axis7[1] = plC_NumBox_Axis7_高速計數參數_R01;
            pLC_NumBox_Axis7[2] = plC_NumBox_Axis7_高速計數參數_R02;
            pLC_NumBox_Axis7[3] = plC_NumBox_Axis7_高速計數參數_R03;
            pLC_NumBox_Axis7[4] = plC_NumBox_Axis7_高速計數參數_R04;
            pLC_NumBox_Axis7[5] = plC_NumBox_Axis7_高速計數參數_R05;
            pLC_NumBox_Axis7[6] = plC_NumBox_Axis7_高速計數參數_R06;
            pLC_NumBox_Axis7[7] = plC_NumBox_Axis7_高速計數參數_R07;
            pLC_NumBox_Axis7[8] = plC_NumBox_Axis7_高速計數參數_R08;
            pLC_NumBox_Axis7[9] = plC_NumBox_Axis7_高速計數參數_R09;

            pLC_NumBox_Axis8[0] = plC_NumBox_Axis8_高速計數參數_R00;
            pLC_NumBox_Axis8[1] = plC_NumBox_Axis8_高速計數參數_R01;
            pLC_NumBox_Axis8[2] = plC_NumBox_Axis8_高速計數參數_R02;
            pLC_NumBox_Axis8[3] = plC_NumBox_Axis8_高速計數參數_R03;
            pLC_NumBox_Axis8[4] = plC_NumBox_Axis8_高速計數參數_R04;
            pLC_NumBox_Axis8[5] = plC_NumBox_Axis8_高速計數參數_R05;
            pLC_NumBox_Axis8[6] = plC_NumBox_Axis8_高速計數參數_R06;
            pLC_NumBox_Axis8[7] = plC_NumBox_Axis8_高速計數參數_R07;
            pLC_NumBox_Axis8[8] = plC_NumBox_Axis8_高速計數參數_R08;
            pLC_NumBox_Axis8[9] = plC_NumBox_Axis8_高速計數參數_R09;

            pLC_NumBox_Axis9[0] = plC_NumBox_Axis9_高速計數參數_R00;
            pLC_NumBox_Axis9[1] = plC_NumBox_Axis9_高速計數參數_R01;
            pLC_NumBox_Axis9[2] = plC_NumBox_Axis9_高速計數參數_R02;
            pLC_NumBox_Axis9[3] = plC_NumBox_Axis9_高速計數參數_R03;
            pLC_NumBox_Axis9[4] = plC_NumBox_Axis9_高速計數參數_R04;
            pLC_NumBox_Axis9[5] = plC_NumBox_Axis9_高速計數參數_R05;
            pLC_NumBox_Axis9[6] = plC_NumBox_Axis9_高速計數參數_R06;
            pLC_NumBox_Axis9[7] = plC_NumBox_Axis9_高速計數參數_R07;
            pLC_NumBox_Axis9[8] = plC_NumBox_Axis9_高速計數參數_R08;
            pLC_NumBox_Axis9[9] = plC_NumBox_Axis9_高速計數參數_R09;

            pLC_NumBox_Axis10[0] = plC_NumBox_Axis10_高速計數參數_R00;
            pLC_NumBox_Axis10[1] = plC_NumBox_Axis10_高速計數參數_R01;
            pLC_NumBox_Axis10[2] = plC_NumBox_Axis10_高速計數參數_R02;
            pLC_NumBox_Axis10[3] = plC_NumBox_Axis10_高速計數參數_R03;
            pLC_NumBox_Axis10[4] = plC_NumBox_Axis10_高速計數參數_R04;
            pLC_NumBox_Axis10[5] = plC_NumBox_Axis10_高速計數參數_R05;
            pLC_NumBox_Axis10[6] = plC_NumBox_Axis10_高速計數參數_R06;
            pLC_NumBox_Axis10[7] = plC_NumBox_Axis10_高速計數參數_R07;
            pLC_NumBox_Axis10[8] = plC_NumBox_Axis10_高速計數參數_R08;
            pLC_NumBox_Axis10[9] = plC_NumBox_Axis10_高速計數參數_R09;

            pLC_NumBox_Axis11[0] = plC_NumBox_Axis11_高速計數參數_R00;
            pLC_NumBox_Axis11[1] = plC_NumBox_Axis11_高速計數參數_R01;
            pLC_NumBox_Axis11[2] = plC_NumBox_Axis11_高速計數參數_R02;
            pLC_NumBox_Axis11[3] = plC_NumBox_Axis11_高速計數參數_R03;
            pLC_NumBox_Axis11[4] = plC_NumBox_Axis11_高速計數參數_R04;
            pLC_NumBox_Axis11[5] = plC_NumBox_Axis11_高速計數參數_R05;
            pLC_NumBox_Axis11[6] = plC_NumBox_Axis11_高速計數參數_R06;
            pLC_NumBox_Axis11[7] = plC_NumBox_Axis11_高速計數參數_R07;
            pLC_NumBox_Axis11[8] = plC_NumBox_Axis11_高速計數參數_R08;
            pLC_NumBox_Axis11[9] = plC_NumBox_Axis11_高速計數參數_R09;

            pLC_NumBox_Axis12[0] = plC_NumBox_Axis12_高速計數參數_R00;
            pLC_NumBox_Axis12[1] = plC_NumBox_Axis12_高速計數參數_R01;
            pLC_NumBox_Axis12[2] = plC_NumBox_Axis12_高速計數參數_R02;
            pLC_NumBox_Axis12[3] = plC_NumBox_Axis12_高速計數參數_R03;
            pLC_NumBox_Axis12[4] = plC_NumBox_Axis12_高速計數參數_R04;
            pLC_NumBox_Axis12[5] = plC_NumBox_Axis12_高速計數參數_R05;
            pLC_NumBox_Axis12[6] = plC_NumBox_Axis12_高速計數參數_R06;
            pLC_NumBox_Axis12[7] = plC_NumBox_Axis12_高速計數參數_R07;
            pLC_NumBox_Axis12[8] = plC_NumBox_Axis12_高速計數參數_R08;
            pLC_NumBox_Axis12[9] = plC_NumBox_Axis12_高速計數參數_R09;


            List_PLC_NumBox_Axis_Counter_Parameter.Add(pLC_NumBox_Axis1);
            List_PLC_NumBox_Axis_Counter_Parameter.Add(pLC_NumBox_Axis2);
            List_PLC_NumBox_Axis_Counter_Parameter.Add(pLC_NumBox_Axis3);
            List_PLC_NumBox_Axis_Counter_Parameter.Add(pLC_NumBox_Axis4);
            List_PLC_NumBox_Axis_Counter_Parameter.Add(pLC_NumBox_Axis5);
            List_PLC_NumBox_Axis_Counter_Parameter.Add(pLC_NumBox_Axis6);
            List_PLC_NumBox_Axis_Counter_Parameter.Add(pLC_NumBox_Axis7);
            List_PLC_NumBox_Axis_Counter_Parameter.Add(pLC_NumBox_Axis8);
            List_PLC_NumBox_Axis_Counter_Parameter.Add(pLC_NumBox_Axis9);
            List_PLC_NumBox_Axis_Counter_Parameter.Add(pLC_NumBox_Axis10);
            List_PLC_NumBox_Axis_Counter_Parameter.Add(pLC_NumBox_Axis11);
            List_PLC_NumBox_Axis_Counter_Parameter.Add(pLC_NumBox_Axis12);

        }
        public void Set_Axis_Counter_Parameter(int AxisNum, enum_Axis_Counter_Parameter enum_Axis_Counter_Parameter, int value)
        {
            this.Set_Axis_Counter_Parameter(AxisNum, (int)enum_Axis_Counter_Parameter, value);
        }
        public void Set_Axis_Counter_Parameter(int AxisNum, int index, int value)
        {
            List_PLC_NumBox_Axis_Counter_Parameter[AxisNum][index].SetValue(value);
        }
        public int Get_Axis_Counter_Parameter(int AxisNum, enum_Axis_Counter_Parameter enum_Axis_Counter_Parameter)
        {
            return this.Get_Axis_Counter_Parameter(AxisNum, (int)enum_Axis_Counter_Parameter);
        }
        public int Get_Axis_Counter_Parameter(int AxisNum, int index)
        {
            return List_PLC_NumBox_Axis_Counter_Parameter[AxisNum][index].GetValue();
        }
        #endregion
        #region Axis_State
        private void List_PLC_Button_Axis_State_Init()
        {
            PLC_Button[] pLC_Button_Axis1 = new PLC_Button[10];
            PLC_Button[] pLC_Button_Axis2 = new PLC_Button[10];
            PLC_Button[] pLC_Button_Axis3 = new PLC_Button[10];
            PLC_Button[] pLC_Button_Axis4 = new PLC_Button[10];
            PLC_Button[] pLC_Button_Axis5 = new PLC_Button[10];
            PLC_Button[] pLC_Button_Axis6 = new PLC_Button[10];
            PLC_Button[] pLC_Button_Axis7 = new PLC_Button[10];
            PLC_Button[] pLC_Button_Axis8 = new PLC_Button[10];
            PLC_Button[] pLC_Button_Axis9 = new PLC_Button[10];
            PLC_Button[] pLC_Button_Axis10 = new PLC_Button[10];
            PLC_Button[] pLC_Button_Axis11 = new PLC_Button[10];
            PLC_Button[] pLC_Button_Axis12 = new PLC_Button[10];

            pLC_Button_Axis1[0] = plC_Button_Axis1_狀態_B00;
            pLC_Button_Axis1[1] = plC_Button_Axis1_狀態_B01;
            pLC_Button_Axis1[2] = plC_Button_Axis1_狀態_B02;
            pLC_Button_Axis1[3] = plC_Button_Axis1_狀態_B03;
            pLC_Button_Axis1[4] = plC_Button_Axis1_狀態_B04;
            pLC_Button_Axis1[5] = plC_Button_Axis1_狀態_B05;
            pLC_Button_Axis1[6] = plC_Button_Axis1_狀態_B06;
            pLC_Button_Axis1[7] = plC_Button_Axis1_狀態_B07;
            pLC_Button_Axis1[8] = plC_Button_Axis1_狀態_B08;
            pLC_Button_Axis1[9] = plC_Button_Axis1_狀態_B09;

            pLC_Button_Axis2[0] = plC_Button_Axis2_狀態_B00;
            pLC_Button_Axis2[1] = plC_Button_Axis2_狀態_B01;
            pLC_Button_Axis2[2] = plC_Button_Axis2_狀態_B02;
            pLC_Button_Axis2[3] = plC_Button_Axis2_狀態_B03;
            pLC_Button_Axis2[4] = plC_Button_Axis2_狀態_B04;
            pLC_Button_Axis2[5] = plC_Button_Axis2_狀態_B05;
            pLC_Button_Axis2[6] = plC_Button_Axis2_狀態_B06;
            pLC_Button_Axis2[7] = plC_Button_Axis2_狀態_B07;
            pLC_Button_Axis2[8] = plC_Button_Axis2_狀態_B08;
            pLC_Button_Axis2[9] = plC_Button_Axis2_狀態_B09;

            pLC_Button_Axis3[0] = plC_Button_Axis3_狀態_B00;
            pLC_Button_Axis3[1] = plC_Button_Axis3_狀態_B01;
            pLC_Button_Axis3[2] = plC_Button_Axis3_狀態_B02;
            pLC_Button_Axis3[3] = plC_Button_Axis3_狀態_B03;
            pLC_Button_Axis3[4] = plC_Button_Axis3_狀態_B04;
            pLC_Button_Axis3[5] = plC_Button_Axis3_狀態_B05;
            pLC_Button_Axis3[6] = plC_Button_Axis3_狀態_B06;
            pLC_Button_Axis3[7] = plC_Button_Axis3_狀態_B07;
            pLC_Button_Axis3[8] = plC_Button_Axis3_狀態_B08;
            pLC_Button_Axis3[9] = plC_Button_Axis3_狀態_B09;

            pLC_Button_Axis4[0] = plC_Button_Axis4_狀態_B00;
            pLC_Button_Axis4[1] = plC_Button_Axis4_狀態_B01;
            pLC_Button_Axis4[2] = plC_Button_Axis4_狀態_B02;
            pLC_Button_Axis4[3] = plC_Button_Axis4_狀態_B03;
            pLC_Button_Axis4[4] = plC_Button_Axis4_狀態_B04;
            pLC_Button_Axis4[5] = plC_Button_Axis4_狀態_B05;
            pLC_Button_Axis4[6] = plC_Button_Axis4_狀態_B06;
            pLC_Button_Axis4[7] = plC_Button_Axis4_狀態_B07;
            pLC_Button_Axis4[8] = plC_Button_Axis4_狀態_B08;
            pLC_Button_Axis4[9] = plC_Button_Axis4_狀態_B09;

            pLC_Button_Axis5[0] = plC_Button_Axis5_狀態_B00;
            pLC_Button_Axis5[1] = plC_Button_Axis5_狀態_B01;
            pLC_Button_Axis5[2] = plC_Button_Axis5_狀態_B02;
            pLC_Button_Axis5[3] = plC_Button_Axis5_狀態_B03;
            pLC_Button_Axis5[4] = plC_Button_Axis5_狀態_B04;
            pLC_Button_Axis5[5] = plC_Button_Axis5_狀態_B05;
            pLC_Button_Axis5[6] = plC_Button_Axis5_狀態_B06;
            pLC_Button_Axis5[7] = plC_Button_Axis5_狀態_B07;
            pLC_Button_Axis5[8] = plC_Button_Axis5_狀態_B08;
            pLC_Button_Axis5[9] = plC_Button_Axis5_狀態_B09;

            pLC_Button_Axis6[0] = plC_Button_Axis6_狀態_B00;
            pLC_Button_Axis6[1] = plC_Button_Axis6_狀態_B01;
            pLC_Button_Axis6[2] = plC_Button_Axis6_狀態_B02;
            pLC_Button_Axis6[3] = plC_Button_Axis6_狀態_B03;
            pLC_Button_Axis6[4] = plC_Button_Axis6_狀態_B04;
            pLC_Button_Axis6[5] = plC_Button_Axis6_狀態_B05;
            pLC_Button_Axis6[6] = plC_Button_Axis6_狀態_B06;
            pLC_Button_Axis6[7] = plC_Button_Axis6_狀態_B07;
            pLC_Button_Axis6[8] = plC_Button_Axis6_狀態_B08;
            pLC_Button_Axis6[9] = plC_Button_Axis6_狀態_B09;

            pLC_Button_Axis7[0] = plC_Button_Axis7_狀態_B00;
            pLC_Button_Axis7[1] = plC_Button_Axis7_狀態_B01;
            pLC_Button_Axis7[2] = plC_Button_Axis7_狀態_B02;
            pLC_Button_Axis7[3] = plC_Button_Axis7_狀態_B03;
            pLC_Button_Axis7[4] = plC_Button_Axis7_狀態_B04;
            pLC_Button_Axis7[5] = plC_Button_Axis7_狀態_B05;
            pLC_Button_Axis7[6] = plC_Button_Axis7_狀態_B06;
            pLC_Button_Axis7[7] = plC_Button_Axis7_狀態_B07;
            pLC_Button_Axis7[8] = plC_Button_Axis7_狀態_B08;
            pLC_Button_Axis7[9] = plC_Button_Axis7_狀態_B09;

            pLC_Button_Axis8[0] = plC_Button_Axis8_狀態_B00;
            pLC_Button_Axis8[1] = plC_Button_Axis8_狀態_B01;
            pLC_Button_Axis8[2] = plC_Button_Axis8_狀態_B02;
            pLC_Button_Axis8[3] = plC_Button_Axis8_狀態_B03;
            pLC_Button_Axis8[4] = plC_Button_Axis8_狀態_B04;
            pLC_Button_Axis8[5] = plC_Button_Axis8_狀態_B05;
            pLC_Button_Axis8[6] = plC_Button_Axis8_狀態_B06;
            pLC_Button_Axis8[7] = plC_Button_Axis8_狀態_B07;
            pLC_Button_Axis8[8] = plC_Button_Axis8_狀態_B08;
            pLC_Button_Axis8[9] = plC_Button_Axis8_狀態_B09;

            pLC_Button_Axis9[0] = plC_Button_Axis9_狀態_B00;
            pLC_Button_Axis9[1] = plC_Button_Axis9_狀態_B01;
            pLC_Button_Axis9[2] = plC_Button_Axis9_狀態_B02;
            pLC_Button_Axis9[3] = plC_Button_Axis9_狀態_B03;
            pLC_Button_Axis9[4] = plC_Button_Axis9_狀態_B04;
            pLC_Button_Axis9[5] = plC_Button_Axis9_狀態_B05;
            pLC_Button_Axis9[6] = plC_Button_Axis9_狀態_B06;
            pLC_Button_Axis9[7] = plC_Button_Axis9_狀態_B07;
            pLC_Button_Axis9[8] = plC_Button_Axis9_狀態_B08;
            pLC_Button_Axis9[9] = plC_Button_Axis9_狀態_B09;

            pLC_Button_Axis10[0] = plC_Button_Axis10_狀態_B00;
            pLC_Button_Axis10[1] = plC_Button_Axis10_狀態_B01;
            pLC_Button_Axis10[2] = plC_Button_Axis10_狀態_B02;
            pLC_Button_Axis10[3] = plC_Button_Axis10_狀態_B03;
            pLC_Button_Axis10[4] = plC_Button_Axis10_狀態_B04;
            pLC_Button_Axis10[5] = plC_Button_Axis10_狀態_B05;
            pLC_Button_Axis10[6] = plC_Button_Axis10_狀態_B06;
            pLC_Button_Axis10[7] = plC_Button_Axis10_狀態_B07;
            pLC_Button_Axis10[8] = plC_Button_Axis10_狀態_B08;
            pLC_Button_Axis10[9] = plC_Button_Axis10_狀態_B09;

            pLC_Button_Axis11[0] = plC_Button_Axis11_狀態_B00;
            pLC_Button_Axis11[1] = plC_Button_Axis11_狀態_B01;
            pLC_Button_Axis11[2] = plC_Button_Axis11_狀態_B02;
            pLC_Button_Axis11[3] = plC_Button_Axis11_狀態_B03;
            pLC_Button_Axis11[4] = plC_Button_Axis11_狀態_B04;
            pLC_Button_Axis11[5] = plC_Button_Axis11_狀態_B05;
            pLC_Button_Axis11[6] = plC_Button_Axis11_狀態_B06;
            pLC_Button_Axis11[7] = plC_Button_Axis11_狀態_B07;
            pLC_Button_Axis11[8] = plC_Button_Axis11_狀態_B08;
            pLC_Button_Axis11[9] = plC_Button_Axis11_狀態_B09;

            pLC_Button_Axis12[0] = plC_Button_Axis12_狀態_B00;
            pLC_Button_Axis12[1] = plC_Button_Axis12_狀態_B01;
            pLC_Button_Axis12[2] = plC_Button_Axis12_狀態_B02;
            pLC_Button_Axis12[3] = plC_Button_Axis12_狀態_B03;
            pLC_Button_Axis12[4] = plC_Button_Axis12_狀態_B04;
            pLC_Button_Axis12[5] = plC_Button_Axis12_狀態_B05;
            pLC_Button_Axis12[6] = plC_Button_Axis12_狀態_B06;
            pLC_Button_Axis12[7] = plC_Button_Axis12_狀態_B07;
            pLC_Button_Axis12[8] = plC_Button_Axis12_狀態_B08;
            pLC_Button_Axis12[9] = plC_Button_Axis12_狀態_B09;


            List_PLC_Button_Axis_State.Add(pLC_Button_Axis1);
            List_PLC_Button_Axis_State.Add(pLC_Button_Axis2);
            List_PLC_Button_Axis_State.Add(pLC_Button_Axis3);
            List_PLC_Button_Axis_State.Add(pLC_Button_Axis4);
            List_PLC_Button_Axis_State.Add(pLC_Button_Axis5);
            List_PLC_Button_Axis_State.Add(pLC_Button_Axis6);
            List_PLC_Button_Axis_State.Add(pLC_Button_Axis7);
            List_PLC_Button_Axis_State.Add(pLC_Button_Axis8);
            List_PLC_Button_Axis_State.Add(pLC_Button_Axis9);
            List_PLC_Button_Axis_State.Add(pLC_Button_Axis10);
            List_PLC_Button_Axis_State.Add(pLC_Button_Axis11);
            List_PLC_Button_Axis_State.Add(pLC_Button_Axis12);

        }
        public bool Get_Axis_State(int AxisNum, enum_Axis_State enum_Axis_State)
        {
            return this.Get_Axis_State(AxisNum, (int)enum_Axis_State);
        }
        public bool Get_Axis_State(int AxisNum, int index)
        {
            return List_PLC_Button_Axis_State[AxisNum][index].GetValue();
        }
        public void Set_Axis_State(int AxisNum, enum_Axis_State enum_Axis_State, bool value)
        {
            this.Set_Axis_State(AxisNum, (int)enum_Axis_State, value);
        }
        public void Set_Axis_State(int AxisNum, int index, bool value)
        {
            List_PLC_Button_Axis_State[AxisNum][index].SetValue(value);
        }
        #endregion
        #region Axis_Index
        private void List_PLC_NumBox_Axis_Index_Init()
        {
            List_PLC_NumBox_Axis_Index.Add(plC_NumBox_Axis1_對應軸號);
            List_PLC_NumBox_Axis_Index.Add(plC_NumBox_Axis2_對應軸號);
            List_PLC_NumBox_Axis_Index.Add(plC_NumBox_Axis3_對應軸號);
            List_PLC_NumBox_Axis_Index.Add(plC_NumBox_Axis4_對應軸號);
            List_PLC_NumBox_Axis_Index.Add(plC_NumBox_Axis5_對應軸號);
            List_PLC_NumBox_Axis_Index.Add(plC_NumBox_Axis6_對應軸號);
            List_PLC_NumBox_Axis_Index.Add(plC_NumBox_Axis7_對應軸號);
            List_PLC_NumBox_Axis_Index.Add(plC_NumBox_Axis8_對應軸號);
            List_PLC_NumBox_Axis_Index.Add(plC_NumBox_Axis9_對應軸號);
            List_PLC_NumBox_Axis_Index.Add(plC_NumBox_Axis10_對應軸號);
            List_PLC_NumBox_Axis_Index.Add(plC_NumBox_Axis11_對應軸號);
            List_PLC_NumBox_Axis_Index.Add(plC_NumBox_Axis12_對應軸號);
        }
        public void Set_Axis_Index(int AxisNum, int value)
        {
            List_PLC_NumBox_Axis_Index[AxisNum].SetValue(value);
        }
        public int Get_Axis_Index(int AxisNum)
        {
            return List_PLC_NumBox_Axis_Index[AxisNum].GetValue();
        }
        #endregion
        #region CAN
        public void Set_CAN_State(bool state)
        {
            this.plC_Button_CAN_state.SetValue(state);
        }
        public bool Get_CAN_State()
        {
            return this.plC_Button_CAN_state.GetValue();
        }
        public void Set_CAN_NodeNum(int Num)
        {
            this.plC_NumBox_CAN_NodeNum.SetValue(Num);
        }
        public int Get_CAN_NodeNum()
        {
            return this.plC_NumBox_CAN_NodeNum.GetValue();
        }

        public void Set_CAN_Baudrate(enum_Baudrate enum_Baudrate) 
        {
            this.Baudrate = enum_Baudrate;
        }
        public enum_Baudrate Get_CAN_Baudrate()
        {
            return this.Baudrate ;
        }
        #endregion
        public DMC3C00_Basic()
        {
            InitializeComponent();
        }

        public bool IsInitDone()
        {
            return flag_Init_OK;
        }
        public void Init(int AxisNum)
        {
            List_PLC_Button_Input_Init();
            List_PLC_Button_Output_Init();
            List_CheckBox_Output_PCUse_Init();
            List_NumWordTextBox_Input_Init();
            List_NumWordTextBox_Output_Init();
            List_PLC_Button_Axis_Input_Init();
            List_NumWordTextBox_Axis_Input_Init();
            List_PLC_Button_Axis_Output_Init();
            List_NumWordTextBox_Axis_Output_Init();
            List_PLC_NumBox_Axis_Parameter_Init();
            List_PLC_NumBox_Axis_Counter_Parameter_Init();
            List_PLC_Button_Axis_State_Init();
            List_PLC_NumBox_Axis_Index_Init();

            this.Axis_num = AxisNum;
            this.List_TabPage.Add(Axis1);
            this.List_TabPage.Add(Axis2);
            this.List_TabPage.Add(Axis3);
            this.List_TabPage.Add(Axis4);
            this.List_TabPage.Add(Axis5);
            this.List_TabPage.Add(Axis6);
            this.List_TabPage.Add(Axis7);
            this.List_TabPage.Add(Axis8);
            this.List_TabPage.Add(Axis9);
            this.List_TabPage.Add(Axis10);
            this.List_TabPage.Add(Axis11);
            this.List_TabPage.Add(Axis12);
            int index = 0;
            foreach (TabPage _tabpage in this.List_TabPage)
            {
                if (index >= this.Axis_num) _tabpage.Parent = null;
                index++;
            }
            

            flag_Init_OK = true;
        }
        public void SetPLC(LowerMachine PLC)
        {
            this.PLC = PLC;
        }
        public void RefreshUI()
        {
            foreach (PLC_Button pLC_Button in List_PLC_Button_Input)
            {
                pLC_Button.Run(this.PLC);
            }
            foreach (PLC_Button pLC_Button in List_PLC_Button_Output)
            {
                pLC_Button.Run(this.PLC);
            }
            foreach (PLC_Button[] Array_pLC_Button in List_PLC_Button_Axis_Input)
            {
                for (int i = 0; i < Array_pLC_Button.Length; i++)
                {
                    Array_pLC_Button[i].Run(this.PLC);
                }
            }
            foreach (PLC_Button[] Array_pLC_Button in List_PLC_Button_Axis_Output)
            {
                for (int i = 0; i < Array_pLC_Button.Length; i++)
                {
                    Array_pLC_Button[i].Run(this.PLC);
                }
            }
            foreach (PLC_NumBox[] Array_PLC_NumBox in List_PLC_NumBox_Axis_Parameter)
            {
                for (int i = 0; i < Array_PLC_NumBox.Length; i++)
                {
                    Array_PLC_NumBox[i].Run(this.PLC);
                }
            }
            foreach (PLC_NumBox[] Array_PLC_NumBox in List_PLC_NumBox_Axis_Counter_Parameter)
            {
                for (int i = 0; i < Array_PLC_NumBox.Length; i++)
                {
                    Array_PLC_NumBox[i].Run(this.PLC);
                }
            }
            foreach (PLC_Button[] Array_PLC_Button in List_PLC_Button_Axis_State)
            {
                for (int i = 0; i < Array_PLC_Button.Length; i++)
                {
                    Array_PLC_Button[i].Run(this.PLC);
                }
            }
            foreach (PLC_NumBox pLC_NumBox in List_PLC_NumBox_Axis_Index)
            {
                pLC_NumBox.Run(this.PLC);
            }
            this.plC_NumBox_CAN_NodeNum.Run(this.PLC);
            this.plC_Button_CAN_state.Run(this.PLC);
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
                foreach (NumWordTextBox[] Array_numWordTextBox in List_NumWordTextBox_Axis_Input)
                {
                    for (int i = 0; i < Array_numWordTextBox.Length; i++)
                    {
                        Array_numWordTextBox[i].Enabled = enable;

                        if (!(LadderProperty.DEVICE.TestDevice(Array_numWordTextBox[i].Text) && (Array_numWordTextBox[i].Text.Remove(1) == "X")))
                        {
                            Array_numWordTextBox[i].Text = "";
                        }
                    }
                }
                foreach (NumWordTextBox[] Array_numWordTextBox in List_NumWordTextBox_Axis_Output)
                {
                    for (int i = 0; i < Array_numWordTextBox.Length; i++)
                    {
                        Array_numWordTextBox[i].Enabled = enable;

                        if (!(LadderProperty.DEVICE.TestDevice(Array_numWordTextBox[i].Text) && (Array_numWordTextBox[i].Text.Remove(1) == "Y")))
                        {
                            Array_numWordTextBox[i].Text = "";
                        }
                    }
                }
                foreach (PLC_NumBox pLC_NumBox in List_PLC_NumBox_Axis_Index)
                {
                    pLC_NumBox.Enabled = enable;
                }
            }));
        }
        [Serializable]
        public class SaveClass
        {
            public List<int> Axis_Index = new List<int>();
            public List<string> Input_Adress = new List<string>();
            public List<string> Output_Adress = new List<string>();
            public List<string[]> Axis_Input_Adress = new List<string[]>();
            public List<string[]> Axis_Output_Adress = new List<string[]>();
            public List<bool> Output_PCUse = new List<bool>();
        }
        public SaveClass GetSaveObject()
        {
            SaveClass saveClass = new SaveClass();
            foreach (PLC_NumBox pLC_NumBox in List_PLC_NumBox_Axis_Index)
            {
                saveClass.Axis_Index.Add(pLC_NumBox.GetValue());
            }
            foreach (NumWordTextBox numWordTextBox in List_NumWordTextBox_Input)
            {
                saveClass.Input_Adress.Add(numWordTextBox.Text);
            }
            foreach (NumWordTextBox numWordTextBox in List_NumWordTextBox_Output)
            {
                saveClass.Output_Adress.Add(numWordTextBox.Text);
            }
            foreach (CheckBox checkBox in List_CheckBox_Output_PCUse)
            {
                saveClass.Output_PCUse.Add(checkBox.Checked);
            }
            foreach (NumWordTextBox[] Array_numWordTextBox in List_NumWordTextBox_Axis_Input)
            {
                List<string> axis_Input_Adress = new List<string>();
                for (int i = 0; i < Array_numWordTextBox.Length; i++)
                {
                    axis_Input_Adress.Add(Array_numWordTextBox[i].Text);
                }
                saveClass.Axis_Input_Adress.Add(axis_Input_Adress.ToArray());
            }
            foreach (NumWordTextBox[] Array_numWordTextBox in List_NumWordTextBox_Axis_Output)
            {
                List<string> axis_Output_Adress = new List<string>();
                for (int i = 0; i < Array_numWordTextBox.Length; i++)
                {
                    axis_Output_Adress.Add(Array_numWordTextBox[i].Text);
                }
                saveClass.Axis_Output_Adress.Add(axis_Output_Adress.ToArray());
            }
            return saveClass;
        }
        public void LoadObject(SaveClass saveClass)
        {
            this.Invoke(new Action(delegate
            {
                for (int i = 0; i < List_PLC_NumBox_Axis_Index.Count; i++)
                {
                    if (saveClass.Axis_Index != null) if (i < saveClass.Axis_Index.Count) List_PLC_NumBox_Axis_Index[i].SetValue(saveClass.Axis_Index[i]);
                }
                for (int i = 0; i < List_NumWordTextBox_Input.Count; i++)
                {
                    if (saveClass.Input_Adress != null) if (i < saveClass.Input_Adress.Count) List_NumWordTextBox_Input[i].Text = saveClass.Input_Adress[i];
                }
                for (int i = 0; i < List_CheckBox_Output_PCUse.Count; i++)
                {
                    if (saveClass.Output_PCUse != null) if (i < saveClass.Output_PCUse.Count) List_CheckBox_Output_PCUse[i].Checked = saveClass.Output_PCUse[i];
                }
                for (int i = 0; i < List_NumWordTextBox_Output.Count; i++)
                {
                    if (saveClass.Output_Adress != null) if (i < saveClass.Output_Adress.Count) List_NumWordTextBox_Output[i].Text = saveClass.Output_Adress[i];
                }
                if (saveClass.Axis_Input_Adress != null)
                {
                    for (int i = 0; i < List_NumWordTextBox_Axis_Input.Count; i++)
                    {
                        if (i < saveClass.Axis_Input_Adress.Count)
                        {
                            for (int k = 0; k < List_NumWordTextBox_Axis_Input[i].Length; k++)
                            {
                                if (k < saveClass.Axis_Input_Adress[i].Length)
                                {
                                    List_NumWordTextBox_Axis_Input[i][k].Text = saveClass.Axis_Input_Adress[i][k];
                                }

                            }
                        }
                    }
                }
                if (saveClass.Axis_Output_Adress != null)
                {
                    for (int i = 0; i < List_NumWordTextBox_Axis_Output.Count; i++)
                    {
                        if (i < saveClass.Axis_Output_Adress.Count)
                        {
                            for (int k = 0; k < List_NumWordTextBox_Axis_Output[i].Length; k++)
                            {
                                if (k < saveClass.Axis_Output_Adress[i].Length)
                                {
                                    List_NumWordTextBox_Axis_Output[i][k].Text = saveClass.Axis_Output_Adress[i][k];
                                }

                            }
                        }
                    }
                }
            }));

        }
    }
}
