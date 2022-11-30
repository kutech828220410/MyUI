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
using LadderUI;
using LadderConnection;
using MyUI;
using Basic;

namespace PortControl
{
    public partial class ParallelPortControlUI : UserControl
    {
        MyThread ThreadStart;
        private string _設備名稱 = "LPT-001";
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
        private string _板卡位址 = "E010";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 板卡位址
        {
            get { return _板卡位址; }
            set
            {
                _板卡位址 = value;
                int_板卡位址 = myConvert.StringHexToint(板卡位址);
                if (int_板卡位址 == -1) _板卡位址 = "";
                numWordTextBox_板卡位址.Text = _板卡位址;
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
        private ParallelPortControl _ParallelPortControl;
        private int int_板卡位址 = 0;
        private Form Active_Form;
        private MyConvert myConvert = new MyConvert();
        private String StreamName;
        private LowerMachine lowerMachine;
        private List<ExButton> List_ExButton = new List<ExButton>();
        private bool FLAG_Isopen = false;

        private NumWordTextBox[,] NumWordTextBox_Input = new NumWordTextBox[1, 5];
        private string[,] Input_position = new string[1, 5];
        private ExButton[,] ExButton_Input = new ExButton[1, 5];
        private bool[,] Input = new bool[1, 5];

        private NumWordTextBox[,] NumWordTextBox_Output = new NumWordTextBox[1, 8];
        private string[,] Output_position = new string[1, 8];
        private ExButton[,] ExButton_Output = new ExButton[1, 8];
        private bool[,] Output = new bool[1, 8];

        public ParallelPortControlUI()
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

            ExButton_Input[0, 0] = exButton_Input_I00;
            ExButton_Input[0, 1] = exButton_Input_I01;
            ExButton_Input[0, 2] = exButton_Input_I02;
            ExButton_Input[0, 3] = exButton_Input_I03;
            ExButton_Input[0, 4] = exButton_Input_I04;

            ExButton_Output[0, 0] = exButton_Output_O00;
            ExButton_Output[0, 1] = exButton_Output_O01;
            ExButton_Output[0, 2] = exButton_Output_O02;
            ExButton_Output[0, 3] = exButton_Output_O03;
            ExButton_Output[0, 4] = exButton_Output_O04;
            ExButton_Output[0, 5] = exButton_Output_O05;
            ExButton_Output[0, 6] = exButton_Output_O06;
            ExButton_Output[0, 7] = exButton_Output_O07;

            NumWordTextBox_Input[0, 0] = numWordTextBox_Input_I00;
            NumWordTextBox_Input[0, 1] = numWordTextBox_Input_I01;
            NumWordTextBox_Input[0, 2] = numWordTextBox_Input_I02;
            NumWordTextBox_Input[0, 3] = numWordTextBox_Input_I03;
            NumWordTextBox_Input[0, 4] = numWordTextBox_Input_I04;

            NumWordTextBox_Output[0, 0] = numWordTextBox_Output_O00;
            NumWordTextBox_Output[0, 1] = numWordTextBox_Output_O01;
            NumWordTextBox_Output[0, 2] = numWordTextBox_Output_O02;
            NumWordTextBox_Output[0, 3] = numWordTextBox_Output_O03;
            NumWordTextBox_Output[0, 4] = numWordTextBox_Output_O04;
            NumWordTextBox_Output[0, 5] = numWordTextBox_Output_O05;
            NumWordTextBox_Output[0, 6] = numWordTextBox_Output_O06;
            NumWordTextBox_Output[0, 7] = numWordTextBox_Output_O07;

            List_ExButton.Add(exButton_Open);
            init();
            foreach (MyUI.ExButton ExButton_temp in List_ExButton) ExButton_temp.Run(lowerMachine);
        }
        private void init()
        {
            _ParallelPortControl = new ParallelPortControl(int_板卡位址);
            LoadProperties();
            lowerMachine.Add_UI_Method(DoWork_UI);
            // lowerMachine.Add_Start_Method(DoWork_strat);
            // lowerMachine.Add_Complete_Method(DoWork_complete);
            ThreadStart = new MyThread("ParallelPortControlStart", Active_Form);
            ThreadStart.Add_Method(DoWork_strat);
            ThreadStart.Add_Method(DoWork_complete);
            ThreadStart.SetSleepTime(CycleTime);
            ThreadStart.AutoRun(true);
            ThreadStart.Trigger();
            Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
            this.exButton_Open.btnClick += new System.EventHandler(this.exButton_Open_btnClick);
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
            RefreshUI();
            sub_Open();
            sub_Close();
        }
        private void RefreshUI()
        {
            for (int k = 0; k < 1; k++)
            {
                for (int i = 0; i < 5; i++)
                {
                    ExButton_Input[k, i].Set_LoadState(Input[k, i]);
                }
            }
            for (int k = 0; k < 1; k++)
            {
                for (int i = 0; i < 8; i++)
                {
                    ExButton_Output[k, i].Set_LoadState(Output[k, i]);
                }
            }
            ThreadStart.GetCycleTime(300, label_CycleTime);
            exButton_Open.Set_LoadState(FLAG_Isopen);
        }

        #region Open
        byte cnt_Open = 255;
        string str_Open_ErrorMessage = "";
        void sub_Open()
        {
            if (cnt_Open == 1) cnt_Open_00_檢查控制器是否開啟(ref cnt_Open);
            if (cnt_Open == 2) cnt_Open_00_檢查連接狀態(ref cnt_Open);
            if (cnt_Open == 3) cnt_Open_00_開啟板卡(ref cnt_Open);
            if (cnt_Open == 4) cnt_Open = 240;

            if (cnt_Open == 240) cnt_Open_240_開啟成功(ref cnt_Open);
            if (cnt_Open == 241) cnt_Open = 255;

            if (cnt_Open == 250) cnt_Open_250_開啟失敗(ref cnt_Open);
            if (cnt_Open == 251) cnt_Open = 255;

        }
        void cnt_Open_00_檢查控制器是否開啟(ref byte cnt)
        {
            FLAG_Isopen = false;
            str_Open_ErrorMessage = "";
            cnt++;
        }
        void cnt_Open_00_檢查連接狀態(ref byte cnt)
        {
            cnt++;
        }
        void cnt_Open_00_開啟板卡(ref byte cnt)
        {
            bool flag = false;
            if (int_板卡位址 == -1)
            {
                str_Open_ErrorMessage += "板卡位址錯誤,請輸入合法16進制內容!/n/r";
                flag = true;
            }
            if(!_ParallelPortControl.PortTest())
            {
                str_Open_ErrorMessage += "Prot Driver Error!/n/r";
                flag = true;
            }
            if (flag) cnt = 250;
            else cnt++;
            
        }
        void cnt_Open_240_開啟成功(ref byte cnt)
        {
            string temp0 = "";
            string device_type0 = "";
            for (int k = 0; k < 1; k++)
            {
                for (int i = 0; i < 5; i++)
                {
                    temp0 = "";
                    device_type0 = "";
                    CallBackUI.numWordTextBox.取得字串(ref temp0, NumWordTextBox_Input[k, i]);
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
            for (int k = 0; k < 1; k++)
            {
                for (int i = 0; i < 8; i++)
                {
                    temp0 = "";
                    device_type0 = "";
                    CallBackUI.numWordTextBox.取得字串(ref temp0, NumWordTextBox_Output[k, i]);
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
            SaveProperties();
            FLAG_Isopen = true;
            cnt++;
        }
        void cnt_Open_250_開啟失敗(ref byte cnt)
        {
            cnt++;
        }
        void cnt_Open_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        #region Close
        byte cnt_Close = 255;
        void sub_Close()
        {
            if (cnt_Close == 1)
            {

                FLAG_Isopen = false;
                for (int k = 0; k < 1; k++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        CallBackUI.numWordTextBox.Enable(true, NumWordTextBox_Input[k, i]);
                    }
                }
                for (int k = 0; k < 1; k++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        CallBackUI.numWordTextBox.Enable(true, NumWordTextBox_Output[k, i]);
                    }
                }
                cnt_Close = 255;
            }
        }
        #endregion

        #region Get_Input
        void sub_Get_Input()
        {
            if (FLAG_Isopen)
            {
                _ParallelPortControl.ReadIN();
                Input[0, 0] = _ParallelPortControl.IPIN10;
                Input[0, 1] = _ParallelPortControl.IPIN11;
                Input[0, 2] = _ParallelPortControl.IPIN12;
                Input[0, 3] = _ParallelPortControl.IPIN13;
                Input[0, 4] = _ParallelPortControl.IPIN15;
            }
        }
        #endregion
        #region WriteToPLC
        void sub_WriteToPLC()
        {
            if (FLAG_Isopen && lowerMachine != null)
            {
                bool flag = false;
                string device = "";

                for (int k = 0; k < 1; k++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        flag = Input[k, i];
                        device = Input_position[k, i];
                        if (device != "" && device != null) lowerMachine.properties.device_system.Set_Device(device, flag);
                    }
                }
            }
        }
        #endregion

        #region Set_Output
        void sub_Set_Output()
        {
            if (FLAG_Isopen)
            {
                _ParallelPortControl.WritePIN(ParallelPortControl.OUT.PIN02, Output[0, 0]);
                _ParallelPortControl.WritePIN(ParallelPortControl.OUT.PIN03, Output[0, 1]);
                _ParallelPortControl.WritePIN(ParallelPortControl.OUT.PIN04, Output[0, 2]);
                _ParallelPortControl.WritePIN(ParallelPortControl.OUT.PIN05, Output[0, 3]);
                _ParallelPortControl.WritePIN(ParallelPortControl.OUT.PIN06, Output[0, 4]);
                _ParallelPortControl.WritePIN(ParallelPortControl.OUT.PIN07, Output[0, 5]);
                _ParallelPortControl.WritePIN(ParallelPortControl.OUT.PIN08, Output[0, 6]);
                _ParallelPortControl.WritePIN(ParallelPortControl.OUT.PIN09, Output[0, 7]);
            }
        }
        #endregion
        #region ReadFromPLC
        void sub_ReadFromPLC()
        {
            if (FLAG_Isopen && lowerMachine != null)
            {
                object flag;
                string device = "";
                for (int k = 0; k < 1; k++)
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
        private void exButton_Open_btnClick(object sender, EventArgs e)
        {
            if (exButton_Open.Load_WriteState())
            {
                if (cnt_Open == 255) cnt_Open = 1;
            }
            else
            {
                if (cnt_Close == 255) cnt_Close = 1;
            }
        }
        [Serializable]
        private class SavePropertyFile
        {
            public string[,] Input_position = new string[1, 5];
            public string[,] Output_position = new string[1, 8];
        }
        private SavePropertyFile savePropertyFile = new SavePropertyFile();
        private void SaveProperties()
        {
            IFormatter binFmt = new BinaryFormatter();
            Stream stream = null;
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
                Input_position = MyFileStream.DeepClone(savePropertyFile.Input_position);
                Output_position = MyFileStream.DeepClone(savePropertyFile.Output_position);
                if (Input_position == null) Input_position = new string[1, 5];
                if (Output_position == null) Output_position = new string[1, 8];

            }
            finally
            {
                if (stream != null) stream.Close();
            }
            for (int k = 0; k < 1; k++)
            {
                for (int i = 0; i < 5; i++)
                {
                    CallBackUI.numWordTextBox.字串更換(Input_position[k, i], NumWordTextBox_Input[k, i]);
                }
            }
            for (int k = 0; k < 1; k++)
            {
                for (int i = 0; i < 8; i++)
                {
                    CallBackUI.numWordTextBox.字串更換(Output_position[k, i], NumWordTextBox_Output[k, i]);
                }
            }
            cnt_Open = 1;
        }
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            ThreadStart.Stop();
        }


    }
}
