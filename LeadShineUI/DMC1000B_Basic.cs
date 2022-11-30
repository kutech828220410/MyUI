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
    public partial class DMC1000B_Basic : UserControl
    {
        private LowerMachine PLC;

        private bool flag_Init_OK = false;
        public static readonly int InputNum = 32;
        public static readonly int OutputNum = 27;
        public static readonly int Axis_num = 4;

        private List<PLC_NumBox> List_PLC_NumBox_Axis_Index = new List<PLC_NumBox>();

        private List<PLC_Button> List_PLC_Button_Input = new List<PLC_Button>();
        private List<NumWordTextBox> List_NumWordTextBox_Input = new List<NumWordTextBox>();
        private List<PLC_Button> List_PLC_Button_Output = new List<PLC_Button>();
        private List<NumWordTextBox> List_NumWordTextBox_Output = new List<NumWordTextBox>();
        private List<PLC_Button[]> List_PLC_Button_Axis_Input = new List<PLC_Button[]>();

        private List<NumWordTextBox[]> List_NumWordTextBox_Axis_Input = new List<NumWordTextBox[]>();
        public enum enum_Axis_Input : int
        {
            N_EL = 0,
            P_EL = 1,
            ORG = 2,
            STP = 3,
            STA = 4,
            N_SD = 5,
            P_SD = 6,
        }

        private List<PLC_NumBox[]> List_PLC_NumBox_Axis_Parameter = new List<PLC_NumBox[]>();
        public enum enum_Axis_Parameter : int
        {
            現在位置 = 0,
            備用_01 = 1,
            備用_02 = 2,
            備用_03 = 3,
            運轉目標位置 = 4,
            基底速度 = 5,
            運動命令碼 = 6,
            運轉速度 = 7,
            加減速度 = 8,
            備用_04 = 9,
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
            備用_01 = 6,
            備用_02 = 7,
            備用_03 = 8,
            備用_04 = 9,
        }

        public DMC1000B_Basic()
        {
            InitializeComponent();
        }

        #region Input
        private void List_PLC_Button_Input_Init()
        {
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
            List_PLC_Button_Input.Add(plC_Button_I16);
            List_PLC_Button_Input.Add(plC_Button_I17);
            List_PLC_Button_Input.Add(plC_Button_I18);
            List_PLC_Button_Input.Add(plC_Button_I19);
            List_PLC_Button_Input.Add(plC_Button_I20);
            List_PLC_Button_Input.Add(plC_Button_I21);
            List_PLC_Button_Input.Add(plC_Button_I22);
            List_PLC_Button_Input.Add(plC_Button_I23);
            List_PLC_Button_Input.Add(plC_Button_I24);
            List_PLC_Button_Input.Add(plC_Button_I25);
            List_PLC_Button_Input.Add(plC_Button_I26);
            List_PLC_Button_Input.Add(plC_Button_I27);
            List_PLC_Button_Input.Add(plC_Button_I28);
            List_PLC_Button_Input.Add(plC_Button_I29);
            List_PLC_Button_Input.Add(plC_Button_I30);
            List_PLC_Button_Input.Add(plC_Button_I31);
            List_PLC_Button_Input.Add(plC_Button_I32);
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
            List_NumWordTextBox_Input.Add(numWordTextBox_I16);
            List_NumWordTextBox_Input.Add(numWordTextBox_I17);
            List_NumWordTextBox_Input.Add(numWordTextBox_I18);
            List_NumWordTextBox_Input.Add(numWordTextBox_I19);
            List_NumWordTextBox_Input.Add(numWordTextBox_I20);
            List_NumWordTextBox_Input.Add(numWordTextBox_I21);
            List_NumWordTextBox_Input.Add(numWordTextBox_I22);
            List_NumWordTextBox_Input.Add(numWordTextBox_I23);
            List_NumWordTextBox_Input.Add(numWordTextBox_I24);
            List_NumWordTextBox_Input.Add(numWordTextBox_I25);
            List_NumWordTextBox_Input.Add(numWordTextBox_I26);
            List_NumWordTextBox_Input.Add(numWordTextBox_I27);
            List_NumWordTextBox_Input.Add(numWordTextBox_I28);
            List_NumWordTextBox_Input.Add(numWordTextBox_I29);
            List_NumWordTextBox_Input.Add(numWordTextBox_I30);
            List_NumWordTextBox_Input.Add(numWordTextBox_I31);
            List_NumWordTextBox_Input.Add(numWordTextBox_I32);
          
        }
        public void Set_Input_Adress(int index , string Adress)
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
        public void List_PLC_Button_Output_Init()
        {
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
            List_PLC_Button_Output.Add(plC_Button_O16);
            List_PLC_Button_Output.Add(plC_Button_O17);
            List_PLC_Button_Output.Add(plC_Button_O18);
            List_PLC_Button_Output.Add(plC_Button_O19);
            List_PLC_Button_Output.Add(plC_Button_O20);
            List_PLC_Button_Output.Add(plC_Button_O21);
            List_PLC_Button_Output.Add(plC_Button_O22);
            List_PLC_Button_Output.Add(plC_Button_O23);
            List_PLC_Button_Output.Add(plC_Button_O24);
            List_PLC_Button_Output.Add(plC_Button_O25);
            List_PLC_Button_Output.Add(plC_Button_O26);
            List_PLC_Button_Output.Add(plC_Button_O27);
     
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
            List_NumWordTextBox_Output.Add(numWordTextBox_O16);
            List_NumWordTextBox_Output.Add(numWordTextBox_O17);
            List_NumWordTextBox_Output.Add(numWordTextBox_O18);
            List_NumWordTextBox_Output.Add(numWordTextBox_O19);
            List_NumWordTextBox_Output.Add(numWordTextBox_O20);
            List_NumWordTextBox_Output.Add(numWordTextBox_O21);
            List_NumWordTextBox_Output.Add(numWordTextBox_O22);
            List_NumWordTextBox_Output.Add(numWordTextBox_O23);
            List_NumWordTextBox_Output.Add(numWordTextBox_O24);
            List_NumWordTextBox_Output.Add(numWordTextBox_O25);
            List_NumWordTextBox_Output.Add(numWordTextBox_O26);
            List_NumWordTextBox_Output.Add(numWordTextBox_O27);

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
        #endregion
        #region Axis_Input
        private void List_PLC_Button_Axis_Input_Init()
        {
            PLC_Button[] pLC_Button_Axis1 = new PLC_Button[7];
            PLC_Button[] pLC_Button_Axis2 = new PLC_Button[7];
            PLC_Button[] pLC_Button_Axis3 = new PLC_Button[7];
            PLC_Button[] pLC_Button_Axis4 = new PLC_Button[7];

            pLC_Button_Axis1[0] = plC_Button_Axis1_I00;
            pLC_Button_Axis1[1] = plC_Button_Axis1_I01;
            pLC_Button_Axis1[2] = plC_Button_Axis1_I02;
            pLC_Button_Axis1[3] = plC_Button_Axis1_I03;
            pLC_Button_Axis1[4] = plC_Button_Axis1_I04;
            pLC_Button_Axis1[5] = plC_Button_Axis1_I05;
            pLC_Button_Axis1[6] = plC_Button_Axis1_I06;

            pLC_Button_Axis2[0] = plC_Button_Axis2_I00;
            pLC_Button_Axis2[1] = plC_Button_Axis2_I01;
            pLC_Button_Axis2[2] = plC_Button_Axis2_I02;
            pLC_Button_Axis2[3] = plC_Button_Axis2_I03;
            pLC_Button_Axis2[4] = plC_Button_Axis2_I04;
            pLC_Button_Axis2[5] = plC_Button_Axis2_I05;
            pLC_Button_Axis2[6] = plC_Button_Axis2_I06;

            pLC_Button_Axis3[0] = plC_Button_Axis3_I00;
            pLC_Button_Axis3[1] = plC_Button_Axis3_I01;
            pLC_Button_Axis3[2] = plC_Button_Axis3_I02;
            pLC_Button_Axis3[3] = plC_Button_Axis3_I03;
            pLC_Button_Axis3[4] = plC_Button_Axis3_I04;
            pLC_Button_Axis3[5] = plC_Button_Axis3_I05;
            pLC_Button_Axis3[6] = plC_Button_Axis3_I06;

            pLC_Button_Axis4[0] = plC_Button_Axis4_I00;
            pLC_Button_Axis4[1] = plC_Button_Axis4_I01;
            pLC_Button_Axis4[2] = plC_Button_Axis4_I02;
            pLC_Button_Axis4[3] = plC_Button_Axis4_I03;
            pLC_Button_Axis4[4] = plC_Button_Axis4_I04;
            pLC_Button_Axis4[5] = plC_Button_Axis4_I05;
            pLC_Button_Axis4[6] = plC_Button_Axis4_I06;

            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis1);
            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis2);
            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis3);
            List_PLC_Button_Axis_Input.Add(pLC_Button_Axis4);
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
        public void Set_Axis_Input(int AxisNum ,int index, bool value)
        {
            List_PLC_Button_Axis_Input[AxisNum][index].SetValue(value);
        }

        private void List_NumWordTextBox_Axis_Input_Init()
        {
            NumWordTextBox[] numWordTextBox_Axis1 = new NumWordTextBox[7];
            NumWordTextBox[] numWordTextBox_Axis2 = new NumWordTextBox[7];
            NumWordTextBox[] numWordTextBox_Axis3 = new NumWordTextBox[7];
            NumWordTextBox[] numWordTextBox_Axis4 = new NumWordTextBox[7];

            numWordTextBox_Axis1[0] = numWordTextBox_Axis1_I00;
            numWordTextBox_Axis1[1] = numWordTextBox_Axis1_I01;
            numWordTextBox_Axis1[2] = numWordTextBox_Axis1_I02;
            numWordTextBox_Axis1[3] = numWordTextBox_Axis1_I03;
            numWordTextBox_Axis1[4] = numWordTextBox_Axis1_I04;
            numWordTextBox_Axis1[5] = numWordTextBox_Axis1_I05;
            numWordTextBox_Axis1[6] = numWordTextBox_Axis1_I06;

            numWordTextBox_Axis2[0] = numWordTextBox_Axis2_I00;
            numWordTextBox_Axis2[1] = numWordTextBox_Axis2_I01;
            numWordTextBox_Axis2[2] = numWordTextBox_Axis2_I02;
            numWordTextBox_Axis2[3] = numWordTextBox_Axis2_I03;
            numWordTextBox_Axis2[4] = numWordTextBox_Axis2_I04;
            numWordTextBox_Axis2[5] = numWordTextBox_Axis2_I05;
            numWordTextBox_Axis2[6] = numWordTextBox_Axis2_I06;

            numWordTextBox_Axis3[0] = numWordTextBox_Axis3_I00;
            numWordTextBox_Axis3[1] = numWordTextBox_Axis3_I01;
            numWordTextBox_Axis3[2] = numWordTextBox_Axis3_I02;
            numWordTextBox_Axis3[3] = numWordTextBox_Axis3_I03;
            numWordTextBox_Axis3[4] = numWordTextBox_Axis3_I04;
            numWordTextBox_Axis3[5] = numWordTextBox_Axis3_I05;
            numWordTextBox_Axis3[6] = numWordTextBox_Axis3_I06;

            numWordTextBox_Axis4[0] = numWordTextBox_Axis4_I00;
            numWordTextBox_Axis4[1] = numWordTextBox_Axis4_I01;
            numWordTextBox_Axis4[2] = numWordTextBox_Axis4_I02;
            numWordTextBox_Axis4[3] = numWordTextBox_Axis4_I03;
            numWordTextBox_Axis4[4] = numWordTextBox_Axis4_I04;
            numWordTextBox_Axis4[5] = numWordTextBox_Axis4_I05;
            numWordTextBox_Axis4[6] = numWordTextBox_Axis4_I06;

            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis1);
            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis2);
            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis3);
            List_NumWordTextBox_Axis_Input.Add(numWordTextBox_Axis4);
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
        public void Set_Axis_Input_Adress(int AxisNum, int index , string Adress)
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
        #region Axis_Parameter
        private void List_PLC_NumBox_Axis_Parameter_Init()
        {
            PLC_NumBox[] pLC_NumBox_Axis1 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis2 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis3 = new PLC_NumBox[10];
            PLC_NumBox[] pLC_NumBox_Axis4 = new PLC_NumBox[10];

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

            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis1);
            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis2);
            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis3);
            List_PLC_NumBox_Axis_Parameter.Add(pLC_NumBox_Axis4);
        }
        public void Set_Axis_Parameter(int AxisNum ,enum_Axis_Parameter enum_Axis_Parameter , int value)
        {
            this.Set_Axis_Parameter(AxisNum, (int)enum_Axis_Parameter, value);
        }
        public void Set_Axis_Parameter(int AxisNum, int index, int value)
        {
            List_PLC_NumBox_Axis_Parameter[AxisNum][index].SetValue(value);
        }
        public int Get_Axis_Parameter(int AxisNum ,enum_Axis_Parameter enum_Axis_Parameter)
        {
            return this.Get_Axis_Parameter(AxisNum, (int)enum_Axis_Parameter);
        }
        public int Get_Axis_Parameter(int AxisNum, int index)
        {
            return List_PLC_NumBox_Axis_Parameter[AxisNum][index].GetValue();
        }
        #endregion
        #region Axis_State
        private void List_PLC_Button_Axis_State_Init()
        {
            PLC_Button[] pLC_Button_Axis1 = new PLC_Button[10];
            PLC_Button[] pLC_Button_Axis2 = new PLC_Button[10];
            PLC_Button[] pLC_Button_Axis3 = new PLC_Button[10];
            PLC_Button[] pLC_Button_Axis4 = new PLC_Button[10];

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

            List_PLC_Button_Axis_State.Add(pLC_Button_Axis1);
            List_PLC_Button_Axis_State.Add(pLC_Button_Axis2);
            List_PLC_Button_Axis_State.Add(pLC_Button_Axis3);
            List_PLC_Button_Axis_State.Add(pLC_Button_Axis4);
        }
        public bool Get_Axis_State(int AxisNum, enum_Axis_State enum_Axis_State)
        {
            return this.Get_Axis_State(AxisNum, (int)enum_Axis_State);
        }
        public bool Get_Axis_State(int AxisNum , int index)
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

        public bool IsInitDone()
        {
            return flag_Init_OK;
        }
        private void DMC1000B_Basic_Load(object sender, EventArgs e)
        {
 
        }
        public void Init()
        {
            List_PLC_Button_Input_Init();
            List_PLC_Button_Output_Init();
            List_NumWordTextBox_Input_Init();
            List_NumWordTextBox_Output_Init();
            List_PLC_Button_Axis_Input_Init();
            List_NumWordTextBox_Axis_Input_Init();
            List_PLC_NumBox_Axis_Parameter_Init();
            List_PLC_Button_Axis_State_Init();
            List_PLC_NumBox_Axis_Index_Init();
            flag_Init_OK = true;
        }
        public void SetPLC(LowerMachine PLC)
        {
            this.PLC = PLC;
        }
        public void RefreshUI()
        {
            foreach(PLC_Button pLC_Button in List_PLC_Button_Input)
            {
                pLC_Button.Run(this.PLC);
            }
            foreach (PLC_Button pLC_Button in List_PLC_Button_Output)
            {
                pLC_Button.Run(this.PLC);
            }
            foreach (PLC_Button[] Array_pLC_Button in List_PLC_Button_Axis_Input)
            {
                for (int i = 0; i < Array_pLC_Button.Length; i++ )
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
        }
        public SaveClass GetSaveObject()
        {
            SaveClass saveClass = new SaveClass();
            foreach(PLC_NumBox pLC_NumBox in List_PLC_NumBox_Axis_Index)
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
            foreach (NumWordTextBox[] Array_numWordTextBox in List_NumWordTextBox_Axis_Input)
            {
                List<string> axis_Input_Adress = new List<string>();
                for(int i = 0 ; i <Array_numWordTextBox.Length ; i++)
                {
                    axis_Input_Adress.Add(Array_numWordTextBox[i].Text);
                }
                saveClass.Axis_Input_Adress.Add(axis_Input_Adress.ToArray());    
            }
            return saveClass;
        }
        public void LoadObject(SaveClass saveClass)
        {
            this.Invoke(new Action(delegate
            {
                for (int i = 0; i < List_PLC_NumBox_Axis_Index.Count; i++)
                {
                    if (i < saveClass.Axis_Index.Count) List_PLC_NumBox_Axis_Index[i].SetValue(saveClass.Axis_Index[i]);
                }
                for (int i = 0; i < List_NumWordTextBox_Input.Count; i++)
                {
                    if (i < saveClass.Input_Adress.Count) List_NumWordTextBox_Input[i].Text = saveClass.Input_Adress[i];
                }
                for (int i = 0; i < List_NumWordTextBox_Output.Count; i++)
                {
                    if (i < saveClass.Output_Adress.Count) List_NumWordTextBox_Output[i].Text = saveClass.Output_Adress[i];
                }
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
            }));
        
        }
    }
}
