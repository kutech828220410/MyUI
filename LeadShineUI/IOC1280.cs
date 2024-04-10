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
     [System.Drawing.ToolboxBitmap(typeof(IOC1280), "PCI.bmp")]
    public partial class IOC1280 : UserControl
    {
        public static string currentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        #region 自訂屬性
        private string _設備名稱 = "IOC1280-001";
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
        private MyTimer myTimer = new MyTimer();
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
        private LeadShineUI.IOC1280_Basic[] IOC1280_Basic;

        public IOC1280()
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
            MyThread_RefreshUI.SetSleepTime(10);
            MyThread_RefreshUI.Trigger();
            myTimer.StartTickTime(3000);
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
                IOC1280_Basic = new IOC1280_Basic[Card_count];
                for (int i = 0; i < Card_count; i++)
                {
                    IOC1280_Basic[i] = new IOC1280_Basic();
                    IOC1280_Basic[i].Dock = System.Windows.Forms.DockStyle.Fill;
                    IOC1280_Basic[i].Location = new System.Drawing.Point(3, 3);
                    IOC1280_Basic[i].Name = "IOC1280_Basic" + i.ToString();
                    IOC1280_Basic[i].Size = new System.Drawing.Size(569, 429);
                    IOC1280_Basic[i].TabIndex = 0;
                    IOC1280_Basic[i].SetPLC(this.PLC);
                    tabPage[i] = new TabPage();
                    tabPage[i].Controls.Add(IOC1280_Basic[i]);
                    tabPage[i].Location = new System.Drawing.Point(4, 22);
                    tabPage[i].Name = "tabPage_card_" + i.ToString();
                    tabPage[i].Padding = new System.Windows.Forms.Padding(3);
                    tabPage[i].Size = new System.Drawing.Size(192, 74);
                    tabPage[i].TabIndex = i;
                    tabPage[i].Text = "CARD-" + i.ToString();
                    tabPage[i].UseVisualStyleBackColor = true;
       
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
                this.tabControl.Name = "tabControl_IOC1280";
                this.tabControl.SelectedIndex = 0;
                this.tabControl.Dock = DockStyle.Fill;
                this.panel_TAB.Controls.Add(this.tabControl);
                tabControl.ResumeLayout(false);

            }));
        }
        private void Program()
        {
            if (!myTimer.IsTimeOut()) return;
            if (IsOpen && PLC != null)
            {
                sub_GetInput();
                sub_WriteToPLC();

                sub_ReadFromPLC();
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
                    IOC1280_Basic[i].RefreshUI();
                }
                MyThread_Program.GetCycleTime(100, label_CycleTime);
            }
        }

        #region GetInput
        void sub_GetInput()
        {
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 64; k++)
                {
                    IOC1280_Basic[i].Set_Input(k,(pci_ioc1280.ioc_read_inbit((ushort)i, (ushort)(k + 1)) == 0));
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
                    flag = IOC1280_Basic[i].Get_Input(k);
                    adress = IOC1280_Basic[i].Get_Input_Adress(k);
                    if (adress != "" && adress != null) PLC.properties.device_system.Set_Device(adress, flag);
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
            for (int i = 0; i < Card_count; i++)
            {
                for (int k = 0; k < 64; k++)
                {
                    adress = IOC1280_Basic[i].Get_Output_Adress(k);
                    if (adress != "" && adress != null)
                    {
                        if (!this.IOC1280_Basic[i].Get_Output_PCUse(k))
                        {
                            flag = PLC.properties.device_system.Get_DeviceFast_Ex(adress);
                            IOC1280_Basic[i].Set_Output(k, flag);
                            pci_ioc1280.ioc_write_outbit((ushort)i, (ushort)(k + 1), (int)(flag ? 0 : 1));
                        }
                    }
                }
            }
         
        }
        #endregion
        

        #region Function
        public bool GetInput(int CardNum, int BitNum)
        {
            if (BitNum < 1) return false;
            return (pci_ioc1280.ioc_read_inbit((ushort)CardNum, (ushort)(BitNum)) == 0);
        }
        public bool GetOutput(int CardNum, int BitNum)
        {
            if (BitNum < 1) return false;
            return (pci_ioc1280.ioc_read_outbit((ushort)CardNum, (ushort)(BitNum)) == 0);
        }
        public void SetOutput(int CardNum, int BitNum, bool Statu)
        {
            if (BitNum < 1) return;
            string adress = IOC1280_Basic[CardNum].Get_Output_Adress(BitNum - 1);
            PLC.properties.Device.Set_DeviceFast_Ex(adress, Statu);
            pci_ioc1280.ioc_write_outbit((ushort)CardNum, (ushort)(BitNum), (int)(Statu ? 0 : 1));
       
            return;
        }

        #endregion

        private void plC_Button_Open_btnClick(object sender, EventArgs e)
        {
            if (!IsOpen) BoardOpen();
            else BoardClose();
        }
        private void BoardOpen()
        {
            this.Card_count = pci_ioc1280.ioc_board_init();
            if (Card_count > 0)
            {
                this.IsOpen = true;
                if (First_Init)
                {
                    this.TabPage_Init(Card_count);
                    this.TabControl_Init();
                    for (int i = 0; i < Card_count; i++)
                    {
                        IOC1280_Basic[i].Init();
                    }
                }
                if (!First_Init)
                {
                    //this.SaveProperties();
                    this.LoadProperties();
                }
                First_Init = false;
              
                for (int i = 0; i < Card_count; i++)
                {
                    IOC1280_Basic[i].Set_UI_Enable(false);
                }

            }
        }
        private void BoardClose()
        {
            pci_ioc1280.ioc_board_close();
            this.IsOpen = false;
            for (int i = 0; i < Card_count; i++)
            {
                IOC1280_Basic[i].Set_UI_Enable(true);
            }
        }
        private List<IOC1280_Basic.SaveClass> List_SaveClass = new List<IOC1280_Basic.SaveClass>();
        public void SaveProperties()
        {
            if (!this.IsOpen) return;
            this.List_SaveClass.Clear();
            this.StreamName = $@"{currentDirectory}\\IOC1280\\" + _設備名稱 + ".pro";
            for (int i = 0; i < Card_count; i++)
            {
                this.List_SaveClass.Add(IOC1280_Basic[i].GetSaveObject());
            }
            Basic.FileIO.SaveProperties(this.List_SaveClass, this.StreamName);
        }
        public void LoadProperties()
        {
            object temp = new object();
            this.List_SaveClass.Clear();
            this.StreamName = $@"{currentDirectory}\\IOC1280\\" + _設備名稱 + ".pro";
            Console.WriteLine($"[LoadProperties] StreamName:{StreamName}");
            if(!Basic.FileIO.LoadProperties(ref temp, StreamName))
            {
                Console.WriteLine($"[LoadProperties] failed!");

            }
            if (temp is List<IOC1280_Basic.SaveClass>)
            {
                this.List_SaveClass = (List<IOC1280_Basic.SaveClass>)temp;
            }
            for (int i = 0; i < Card_count; i++)
            {
                if (i < this.List_SaveClass.Count)
                {
                    IOC1280_Basic[i].LoadObject(this.List_SaveClass[i]);
                }
            }
        }

        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsOpen)
            {
                BoardClose();
                //this.SaveProperties();
            }
        }
        private void PlC_Button_Save_btnClick(object sender, EventArgs e)
        {
            this.SaveProperties();
            MyMessageBox.ShowDialog("存檔完成!");
        }
    }
}
