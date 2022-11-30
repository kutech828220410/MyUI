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
    public partial class C9016_Basic : UserControl
    {
        const int chanel_num = 2;
        const int Axis_num = 6;
        public C9016_Basic()
        {
            InitializeComponent();
        }
        public NumWordTextBox[,] Get_Input_NumWordTextBox()
        {
            NumWordTextBox[,] NumWordTextBox_Input = new NumWordTextBox[chanel_num, 8];

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

            return NumWordTextBox_Input;
        }
        public ExButton[,] Get_Input_ExButton()
        {
            ExButton[,] ExButton_Input = new ExButton[chanel_num, 8];

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

            return ExButton_Input;
        }
        public string[,] Get_Input_position()
        {
            string[,] Input_position = new string[chanel_num, 8];
            for (int i = 0; i < chanel_num; i++ )
            {
                for (int k = 0; k < 8; k++)
                {
                    Input_position[i, k] = "";
                }
            }
            return Input_position;
        }
        public bool[,] Get_Input()
        {
            bool[,] Input = new bool[chanel_num, 8];
            for (int i = 0; i < chanel_num; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    Input[i, k] = false;
                }
            }
            return Input;
        }

        public NumWordTextBox[,] Get_Output_NumWordTextBox()
        {
            NumWordTextBox[,] NumWordTextBox_Output = new NumWordTextBox[chanel_num, 8];

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

            return NumWordTextBox_Output;
        }
        public ExButton[,] Get_Output_ExButton()
        {
            ExButton[,] ExButton_Output = new ExButton[chanel_num, 8];

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

            return ExButton_Output;
        }
        public string[,] Get_Output_position()
        {
            string[,] Output_position = new string[chanel_num, 8];
            for (int i = 0; i < chanel_num; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    Output_position[i, k] = "";
                }
            }
            return Output_position;
        }
        public bool[,] Get_Output()
        {
            bool[,] Output = new bool[chanel_num, 8];
            for (int i = 0; i < chanel_num; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    Output[i, k] = false;
                }
            }
            return Output;
        }

        public NumTextBox[,] Get_Axis_對應軸號_NumWordTextBox()
        {
            NumTextBox[,] NumTextBox_Axis_對應軸號 = new NumTextBox[Axis_num, 1];

            NumTextBox_Axis_對應軸號[0, 0] = numTextBox_Axis1_對應軸號;
            NumTextBox_Axis_對應軸號[1, 0] = numTextBox_Axis2_對應軸號;
            NumTextBox_Axis_對應軸號[2, 0] = numTextBox_Axis3_對應軸號;
            NumTextBox_Axis_對應軸號[3, 0] = numTextBox_Axis4_對應軸號;
            NumTextBox_Axis_對應軸號[4, 0] = numTextBox_Axis5_對應軸號;
            NumTextBox_Axis_對應軸號[5, 0] = numTextBox_Axis6_對應軸號;

            return NumTextBox_Axis_對應軸號;
        }
        public int[,] Get_Axis_對應軸號()
        {
            int[,] Axis_對應軸號 = new int[Axis_num, 1];
            for (int i = 0; i < Axis_num; i++)
            {
                Axis_對應軸號[i, 0] = 0;   
            }
            return Axis_對應軸號;
        }

        public NumWordTextBox[,] Get_Axis_Input_NumWordTextBox()
        {
            NumWordTextBox[,] NumWordTextBox_Axis_Input = new NumWordTextBox[Axis_num, 3];

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
            NumWordTextBox_Axis_Input[4, 0] = numWordTextBox_Input_Axis5_0;
            NumWordTextBox_Axis_Input[4, 1] = numWordTextBox_Input_Axis5_1;
            NumWordTextBox_Axis_Input[4, 2] = numWordTextBox_Input_Axis5_2;
            NumWordTextBox_Axis_Input[5, 0] = numWordTextBox_Input_Axis6_0;
            NumWordTextBox_Axis_Input[5, 1] = numWordTextBox_Input_Axis6_1;
            NumWordTextBox_Axis_Input[5, 2] = numWordTextBox_Input_Axis6_2;

            return NumWordTextBox_Axis_Input;
        }
        public string[,] Get_Axis_Input_position()
        {
            string[,] Axis_Input_position = new string[Axis_num, 3];
            for (int i = 0; i < Axis_num; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    Axis_Input_position[i, k] = "";
                }
            }
            return Axis_Input_position;
        }
        public ExButton[,] Get_Axis_Input_ExButton()
        {
            ExButton[,] ExButton_Axis_Input = new ExButton[Axis_num, 3];

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
            ExButton_Axis_Input[4, 0] = exButton_Input_Axis5_0;
            ExButton_Axis_Input[4, 1] = exButton_Input_Axis5_1;
            ExButton_Axis_Input[4, 2] = exButton_Input_Axis5_2;
            ExButton_Axis_Input[5, 0] = exButton_Input_Axis6_0;
            ExButton_Axis_Input[5, 1] = exButton_Input_Axis6_1;
            ExButton_Axis_Input[5, 2] = exButton_Input_Axis6_2;
            return ExButton_Axis_Input;
        }
        public bool[,] Get_Axis_Input()
        {
            bool[,] Axis_Input = new bool[Axis_num, 8];
            for (int i = 0; i < Axis_num; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    Axis_Input[i, k] = false;
                }
            }
            return Axis_Input;
        }

        public ExButton[,] Get_Axis_status_ExButton()
        {
            ExButton[,] ExButton_Axis_status = new ExButton[Axis_num, 10];

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

            ExButton_Axis_status[4, 0] = exButton_Axis5_S0;
            ExButton_Axis_status[4, 1] = exButton_Axis5_S1;
            ExButton_Axis_status[4, 2] = exButton_Axis5_S2;
            ExButton_Axis_status[4, 3] = exButton_Axis5_S3;
            ExButton_Axis_status[4, 4] = exButton_Axis5_S4;
            ExButton_Axis_status[4, 5] = exButton_Axis5_S5;
            ExButton_Axis_status[4, 6] = exButton_Axis5_S6;
            ExButton_Axis_status[4, 7] = exButton_Axis5_S7;
            ExButton_Axis_status[4, 8] = exButton_Axis5_S8;
            ExButton_Axis_status[4, 9] = exButton_Axis5_S9;

            ExButton_Axis_status[5, 0] = exButton_Axis6_S0;
            ExButton_Axis_status[5, 1] = exButton_Axis6_S1;
            ExButton_Axis_status[5, 2] = exButton_Axis6_S2;
            ExButton_Axis_status[5, 3] = exButton_Axis6_S3;
            ExButton_Axis_status[5, 4] = exButton_Axis6_S4;
            ExButton_Axis_status[5, 5] = exButton_Axis6_S5;
            ExButton_Axis_status[5, 6] = exButton_Axis6_S6;
            ExButton_Axis_status[5, 7] = exButton_Axis6_S7;
            ExButton_Axis_status[5, 8] = exButton_Axis6_S8;
            ExButton_Axis_status[5, 9] = exButton_Axis6_S9;
            return ExButton_Axis_status;
        }
        public bool[,] Get_Axis_status()
        {
            bool[,] Axis_status = new bool[Axis_num, 10];
            for (int i = 0; i < Axis_num; i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    Axis_status[i, k] = false;
                }
            }
            return Axis_status;
        }

        public NumTextBox[,] Get_Axis_運動參數_NumWordTextBox()
        {
            NumTextBox[,] NumTextBox_Axis_運動參數 = new NumTextBox[Axis_num, 10];

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

            NumTextBox_Axis_運動參數[4, 0] = numTextBox_Axis5_D0;
            NumTextBox_Axis_運動參數[4, 1] = numTextBox_Axis5_D1;
            NumTextBox_Axis_運動參數[4, 2] = numTextBox_Axis5_D2;
            NumTextBox_Axis_運動參數[4, 3] = numTextBox_Axis5_D3;
            NumTextBox_Axis_運動參數[4, 4] = numTextBox_Axis5_D4;
            NumTextBox_Axis_運動參數[4, 5] = numTextBox_Axis5_D5;
            NumTextBox_Axis_運動參數[4, 6] = numTextBox_Axis5_D6;
            NumTextBox_Axis_運動參數[4, 7] = numTextBox_Axis5_D7;
            NumTextBox_Axis_運動參數[4, 8] = numTextBox_Axis5_D8;
            NumTextBox_Axis_運動參數[4, 9] = numTextBox_Axis5_D9;

            NumTextBox_Axis_運動參數[5, 0] = numTextBox_Axis6_D0;
            NumTextBox_Axis_運動參數[5, 1] = numTextBox_Axis6_D1;
            NumTextBox_Axis_運動參數[5, 2] = numTextBox_Axis6_D2;
            NumTextBox_Axis_運動參數[5, 3] = numTextBox_Axis6_D3;
            NumTextBox_Axis_運動參數[5, 4] = numTextBox_Axis6_D4;
            NumTextBox_Axis_運動參數[5, 5] = numTextBox_Axis6_D5;
            NumTextBox_Axis_運動參數[5, 6] = numTextBox_Axis6_D6;
            NumTextBox_Axis_運動參數[5, 7] = numTextBox_Axis6_D7;
            NumTextBox_Axis_運動參數[5, 8] = numTextBox_Axis6_D8;
            NumTextBox_Axis_運動參數[5, 9] = numTextBox_Axis6_D9;

            return NumTextBox_Axis_運動參數;
        }
        public int[,] Get_Axis_運動參數()
        {
            int[,] Axis_運動參數 = new int[Axis_num, 10];
            for (int i = 0; i < Axis_num; i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    Axis_運動參數[i, k] = 0;
                }
            }
            return Axis_運動參數;
        }

        public NumTextBox[,] Get_Axis_高速輸出入參數_NumWordTextBox()
        {
            NumTextBox[,] NumTextBox_Axis_高速輸出入參數 = new NumTextBox[Axis_num, 10];

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

            NumTextBox_Axis_高速輸出入參數[4, 0] = numTextBox_Axis5_R0;
            NumTextBox_Axis_高速輸出入參數[4, 1] = numTextBox_Axis5_R1;
            NumTextBox_Axis_高速輸出入參數[4, 2] = numTextBox_Axis5_R2;
            NumTextBox_Axis_高速輸出入參數[4, 3] = numTextBox_Axis5_R3;
            NumTextBox_Axis_高速輸出入參數[4, 4] = numTextBox_Axis5_R4;
            NumTextBox_Axis_高速輸出入參數[4, 5] = numTextBox_Axis5_R5;
            NumTextBox_Axis_高速輸出入參數[4, 6] = numTextBox_Axis5_R6;
            NumTextBox_Axis_高速輸出入參數[4, 7] = numTextBox_Axis5_R7;
            NumTextBox_Axis_高速輸出入參數[4, 8] = numTextBox_Axis5_R8;
            NumTextBox_Axis_高速輸出入參數[4, 9] = numTextBox_Axis5_R9;

            NumTextBox_Axis_高速輸出入參數[5, 0] = numTextBox_Axis6_R0;
            NumTextBox_Axis_高速輸出入參數[5, 1] = numTextBox_Axis6_R1;
            NumTextBox_Axis_高速輸出入參數[5, 2] = numTextBox_Axis6_R2;
            NumTextBox_Axis_高速輸出入參數[5, 3] = numTextBox_Axis6_R3;
            NumTextBox_Axis_高速輸出入參數[5, 4] = numTextBox_Axis6_R4;
            NumTextBox_Axis_高速輸出入參數[5, 5] = numTextBox_Axis6_R5;
            NumTextBox_Axis_高速輸出入參數[5, 6] = numTextBox_Axis6_R6;
            NumTextBox_Axis_高速輸出入參數[5, 7] = numTextBox_Axis6_R7;
            NumTextBox_Axis_高速輸出入參數[5, 8] = numTextBox_Axis6_R8;
            NumTextBox_Axis_高速輸出入參數[5, 9] = numTextBox_Axis6_R9;

            return NumTextBox_Axis_高速輸出入參數;
        }
        public int[,] Get_Axis_高速輸出入參數()
        {
            int[,] Axis_高速輸出入參數 = new int[Axis_num, 10];
            for (int i = 0; i < Axis_num; i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    Axis_高速輸出入參數[i, k] = 0;
                }
            }
            return Axis_高速輸出入參數;
        }
    }
}
