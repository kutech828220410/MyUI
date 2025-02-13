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
using Pcie1230;
using LadderUI;
using LadderConnection;
using MyUI;
using Basic;
namespace SLDUI
{
    [System.Drawing.ToolboxBitmap(typeof(C1230), "PCI.bmp")]
    public partial class C1230 : UserControl
    {
        private string _設備名稱 = "C1230-001";
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


        const int chanel_num = 4;
        private bool FLAG_C1230_Isopen = false;
        private short card_num = 0;

        private Form Active_Form;
        private MyThread ThreadStart = new MyThread("C1230Start");
        private bool RefreshUI_Enable = true;
        private String StreamName;
        private LowerMachine lowerMachine;
        private MyConvert myConvert = new MyConvert();
        private List<ExButton> List_ExButton = new List<ExButton>();

        private NumWordTextBox[,] NumWordTextBox_Input = new NumWordTextBox[chanel_num, 8];
        private string[,] Input_position = new string[chanel_num, 8];
        private ExButton[,] ExButton_Input = new ExButton[chanel_num, 8];
        private bool[,] Input = new bool[chanel_num, 8];

        private NumWordTextBox[,] NumWordTextBox_Output = new NumWordTextBox[chanel_num, 8];
        private string[,] Output_position = new string[chanel_num, 8];
        private ExButton[,] ExButton_Output = new ExButton[chanel_num, 8];
        private bool[,] Output = new bool[chanel_num, 8];

        public C1230()
        {
            InitializeComponent();
            button_Save.Click += Button_Save_Click;
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            SaveProperties();
            MyMessageBox.ShowDialog("Save Success");
        }

        public void UI_Visible(bool visble)
        {
            RefreshUI_Enable = visble;
            this.Visible = visble;
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

            ExButton_Input[2, 0] = exButton_Input_I20;
            ExButton_Input[2, 1] = exButton_Input_I21;
            ExButton_Input[2, 2] = exButton_Input_I22;
            ExButton_Input[2, 3] = exButton_Input_I23;
            ExButton_Input[2, 4] = exButton_Input_I24;
            ExButton_Input[2, 5] = exButton_Input_I25;
            ExButton_Input[2, 6] = exButton_Input_I26;
            ExButton_Input[2, 7] = exButton_Input_I27;

            ExButton_Input[3, 0] = exButton_Input_I30;
            ExButton_Input[3, 1] = exButton_Input_I31;
            ExButton_Input[3, 2] = exButton_Input_I32;
            ExButton_Input[3, 3] = exButton_Input_I33;
            ExButton_Input[3, 4] = exButton_Input_I34;
            ExButton_Input[3, 5] = exButton_Input_I35;
            ExButton_Input[3, 6] = exButton_Input_I36;
            ExButton_Input[3, 7] = exButton_Input_I37;

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

            ExButton_Output[2, 0] = exButton_Output_O20;
            ExButton_Output[2, 1] = exButton_Output_O21;
            ExButton_Output[2, 2] = exButton_Output_O22;
            ExButton_Output[2, 3] = exButton_Output_O23;
            ExButton_Output[2, 4] = exButton_Output_O24;
            ExButton_Output[2, 5] = exButton_Output_O25;
            ExButton_Output[2, 6] = exButton_Output_O26;
            ExButton_Output[2, 7] = exButton_Output_O27;

            ExButton_Output[3, 0] = exButton_Output_O30;
            ExButton_Output[3, 1] = exButton_Output_O31;
            ExButton_Output[3, 2] = exButton_Output_O32;
            ExButton_Output[3, 3] = exButton_Output_O33;
            ExButton_Output[3, 4] = exButton_Output_O34;
            ExButton_Output[3, 5] = exButton_Output_O35;
            ExButton_Output[3, 6] = exButton_Output_O36;
            ExButton_Output[3, 7] = exButton_Output_O37;

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

            NumWordTextBox_Input[2, 0] = numWordTextBox_Input_I20;
            NumWordTextBox_Input[2, 1] = numWordTextBox_Input_I21;
            NumWordTextBox_Input[2, 2] = numWordTextBox_Input_I22;
            NumWordTextBox_Input[2, 3] = numWordTextBox_Input_I23;
            NumWordTextBox_Input[2, 4] = numWordTextBox_Input_I24;
            NumWordTextBox_Input[2, 5] = numWordTextBox_Input_I25;
            NumWordTextBox_Input[2, 6] = numWordTextBox_Input_I26;
            NumWordTextBox_Input[2, 7] = numWordTextBox_Input_I27;

            NumWordTextBox_Input[3, 0] = numWordTextBox_Input_I30;
            NumWordTextBox_Input[3, 1] = numWordTextBox_Input_I31;
            NumWordTextBox_Input[3, 2] = numWordTextBox_Input_I32;
            NumWordTextBox_Input[3, 3] = numWordTextBox_Input_I33;
            NumWordTextBox_Input[3, 4] = numWordTextBox_Input_I34;
            NumWordTextBox_Input[3, 5] = numWordTextBox_Input_I35;
            NumWordTextBox_Input[3, 6] = numWordTextBox_Input_I36;
            NumWordTextBox_Input[3, 7] = numWordTextBox_Input_I37;

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

            NumWordTextBox_Output[2, 0] = numWordTextBox_Output_O20;
            NumWordTextBox_Output[2, 1] = numWordTextBox_Output_O21;
            NumWordTextBox_Output[2, 2] = numWordTextBox_Output_O22;
            NumWordTextBox_Output[2, 3] = numWordTextBox_Output_O23;
            NumWordTextBox_Output[2, 4] = numWordTextBox_Output_O24;
            NumWordTextBox_Output[2, 5] = numWordTextBox_Output_O25;
            NumWordTextBox_Output[2, 6] = numWordTextBox_Output_O26;
            NumWordTextBox_Output[2, 7] = numWordTextBox_Output_O27;

            NumWordTextBox_Output[3, 0] = numWordTextBox_Output_O30;
            NumWordTextBox_Output[3, 1] = numWordTextBox_Output_O31;
            NumWordTextBox_Output[3, 2] = numWordTextBox_Output_O32;
            NumWordTextBox_Output[3, 3] = numWordTextBox_Output_O33;
            NumWordTextBox_Output[3, 4] = numWordTextBox_Output_O34;
            NumWordTextBox_Output[3, 5] = numWordTextBox_Output_O35;
            NumWordTextBox_Output[3, 6] = numWordTextBox_Output_O36;
            NumWordTextBox_Output[3, 7] = numWordTextBox_Output_O37;

            List_ExButton.Add(exButton_Open);
            init();
            foreach (MyUI.ExButton ExButton_temp in List_ExButton) ExButton_temp.Run(lowerMachine);
        }
        private void init()
        {
            LoadProperties();
            lowerMachine.Add_UI_Method(DoWork_UI);
            ThreadStart.Add_Method(DoWork_strat);
            ThreadStart.Add_Method(DoWork_complete);
            ThreadStart.SetSleepTime(CycleTime);
            ThreadStart.AutoRun(true);
            ThreadStart.Trigger();
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
        }
        private void DoWork_UI()
        {
            if (RefreshUI_Enable) RefreshUI();
            sub_Open_C1230();
            sub_Close_C1230();
        }
        private void RefreshUI()
        {
            for (int k = 0; k < chanel_num; k++)
            {
                for (int i = 0; i < 8; i++)
                {
                    ExButton_Input[k, i].Set_LoadState(Input[k, i]);
                    ExButton_Output[k, i].Set_LoadState(Output[k, i]);
                }
            }
            ThreadStart.GetCycleTime(300, label_CycleTime);
            exButton_Open.Set_LoadState(FLAG_C1230_Isopen);
        }
        #region Open_C1230
        byte cnt_Open_C1230 = 255;
        void sub_Open_C1230()
        {
            if (cnt_Open_C1230 == 1) cnt_Open_C1230_00_檢查控制器是否開啟(ref cnt_Open_C1230);
            if (cnt_Open_C1230 == 2) cnt_Open_C1230_00_檢查連接狀態(ref cnt_Open_C1230);
            if (cnt_Open_C1230 == 3) cnt_Open_C1230_00_開啟板卡(ref cnt_Open_C1230);
            if (cnt_Open_C1230 == 4) cnt_Open_C1230 = 240;

            if (cnt_Open_C1230 == 240) cnt_Open_C1230_240_開啟成功(ref cnt_Open_C1230);
            if (cnt_Open_C1230 == 241) cnt_Open_C1230 = 255;

            if (cnt_Open_C1230 == 250) cnt_Open_C1230_250_開啟失敗(ref cnt_Open_C1230);
            if (cnt_Open_C1230 == 251) cnt_Open_C1230 = 255;

        }
        void cnt_Open_C1230_00_檢查控制器是否開啟(ref byte cnt)
        {
            FLAG_C1230_Isopen = false;
       
            cnt++;
        }
        void cnt_Open_C1230_00_檢查連接狀態(ref byte cnt)
        {
            cnt++;
        }
        void cnt_Open_C1230_00_開啟板卡(ref byte cnt)
        {
            int rc;
            rc = CPcie1230.Pci1230Open(card_num);
            if (rc != CPcie1230.PCI1230Success)
            {
                cnt = 250;
                return;
            }
            rc = CPcie1230.Pci1230Write(card_num, 0);
            cnt++;
        }
        void cnt_Open_C1230_240_開啟成功(ref byte cnt)
        {
            string temp0 = "";
            string device_type0 = "";
            for (int k = 0; k < chanel_num; k++)
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
            for (int k = 0; k < chanel_num; k++)
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
         
            FLAG_C1230_Isopen = true;
            cnt++;
        }
        void cnt_Open_C1230_250_開啟失敗(ref byte cnt)
        {
            MyMessageBox.ShowDialog("Open PCI-1230 fail" + "#" + card_num.ToString("00"));
            cnt++;
        }
        void cnt_Open_C1230_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        #region Close_C1230
        byte cnt_Close_C1230 = 255;
        void sub_Close_C1230()
        {
            if (cnt_Close_C1230 == 1)
            {
                CPcie1230.Pci1230Close(card_num);
                FLAG_C1230_Isopen = false;
                for (int k = 0; k < chanel_num; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        CallBackUI.numWordTextBox.Enable(true, NumWordTextBox_Input[k, i]);
                        CallBackUI.numWordTextBox.Enable(true, NumWordTextBox_Output[k, i]);
                    }
                }
              
                cnt_Close_C1230 = 255;
            }
        }
        #endregion

        #region Get_Input
        void sub_Get_Input()
        {
            if (FLAG_C1230_Isopen)
            {
                uint flag = 0;
                for (int k = 0; k < chanel_num; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        CPcie1230.Pci1230ReadDiBit(card_num, (k * 8) + i, ref flag);
                        Input[k, i] = flag == 1 ? true : false;
                    }
                }       
            }
        }
        #endregion
        #region Set_Output
        void sub_Set_Output()
        {
            if (FLAG_C1230_Isopen)
            {
                uint flag = 0;
                for (int k = 0; k < chanel_num; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (Output[k, i]) flag = 1;
                        else flag = 0;
                        CPcie1230.Pci1230WriteDoBit(card_num, (k * 8) + i, flag);
                      
                    }
                }   
         
            }
        }
        #endregion

        #region WriteToPLC
        void sub_WriteToPLC()
        {
            if (FLAG_C1230_Isopen && lowerMachine != null)
            {
                bool flag = false;
                string device = "";

                for (int k = 0; k < chanel_num; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        flag = Input[k, i];
                        device = Input_position[k, i];
                        if (device != "" && device != null) lowerMachine.properties.device_system.Set_Device(device, flag);
                    }
                }     
            }
        }
        #endregion
        #region ReadFromPLC
        void sub_ReadFromPLC()
        {
            if (FLAG_C1230_Isopen && lowerMachine != null)
            {
                object flag;
                string device = "";
                for (int k = 0; k < chanel_num; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        device = Output_position[k, i];
                        if (device != "" && device != null)
                        {
                            lowerMachine.properties.device_system.Get_Device(device, out flag);
                            bool temp = (bool)flag;
                            Output[k, i] = temp;
                        }
                    }
                }
       
            }
        }
        #endregion

        public bool GetInput(int BitNum)
        {
            uint flag = 0;
            CPcie1230.Pci1230ReadDiBit(card_num, BitNum, ref flag);
            return flag == 1 ? true : false;
        }
        public void SetOutput(int BitNum, bool Statu)
        {
            uint flag = 0;
            if (Statu) flag = 1;
            else flag = 0;
            int channel = BitNum / 8;
            int Bit = BitNum % 8;

            string device = Output_position[channel, Bit];
            if (device != "" && device != null)
            {
                lowerMachine.properties.Device.Set_Device(device, Statu);
                lowerMachine.properties.device_system.Set_Device(device, Statu);
            }
            CPcie1230.Pci1230WriteDoBit(card_num, BitNum, flag);
        }

        private void comboBox_板卡選擇_SelectedIndexChanged(object sender, EventArgs e)
        {
            card_num = Convert.ToInt16(comboBox_板卡選擇.Text);
        }
        private void exButton_Open_btnClick(object sender, EventArgs e)
        {
            if (exButton_Open.Load_WriteState())
            {
                if (cnt_Open_C1230 == 255) cnt_Open_C1230 = 1;
            }
            else
            {
                if (cnt_Close_C1230 == 255) cnt_Close_C1230 = 1;
            } 
        }
        [Serializable]
        private class SavePropertyFile
        {
            public short card_num;
            public string[,] Input_position = new string[chanel_num, 8];
            public string[,] Output_position = new string[chanel_num, 8];
        }
        private SavePropertyFile savePropertyFile = new SavePropertyFile();
        private void SaveProperties()
        {
            IFormatter binFmt = new BinaryFormatter();
            Stream stream = null;
            savePropertyFile.card_num = card_num.DeepClone();
            savePropertyFile.Input_position = MyFileStream.DeepClone(Input_position);
            savePropertyFile.Output_position = MyFileStream.DeepClone(Output_position);
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
                card_num = savePropertyFile.card_num.DeepClone();
                Input_position = MyFileStream.DeepClone(savePropertyFile.Input_position);
                Output_position = MyFileStream.DeepClone(savePropertyFile.Output_position);
                if (Input_position == null) Input_position = new string[2, chanel_num];
                if (Output_position == null) Output_position = new string[2, chanel_num];
      
            }
            finally
            {
                if (stream != null) stream.Close();
            }
            CallBackUI.comobox.字串更換(card_num.ToString(), comboBox_板卡選擇);
            for (int k = 0; k < chanel_num; k++)
            {
                for (int i = 0; i < 8; i++)
                {
                    CallBackUI.numWordTextBox.字串更換(Input_position[k, i], NumWordTextBox_Input[k, i]);
                    CallBackUI.numWordTextBox.字串更換(Output_position[k, i], NumWordTextBox_Output[k, i]);
                }
            }
            cnt_Open_C1230 = 1;
        }
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            ThreadStart.Stop();
        }
    }
}
