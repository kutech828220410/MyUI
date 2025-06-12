using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using LadderConnection;
namespace LadderUI
{
    public partial class LowerMachine_Panel : UserControl
    {
        private int _掃描速度 = 0;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int 掃描速度
        {
            get { return _掃描速度; }
            set
            {
                _掃描速度 = value;
            }
        }

        public bool 背景執行緒_Enable = false;
        bool RefreshUI_Enable = true;
        static private System.IO.Ports.SerialPort serialPort = new System.IO.Ports.SerialPort();
        public LowerMachine lowerMachine;
        private string[] serialPorts = new string[1];

        private Form form;
        private bool FLAG_已建立表單 = false;
        private List<MyUI.ExButton> List_ExButton = new List<MyUI.ExButton>();

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

        public LowerMachine_Panel()
        {
            InitializeComponent();
            numWordTextBox_Baudrate.Text = (115200).ToString();
            List_ExButton.Add(exButton_Open);
        }
        public void Run()
        {
            背景執行緒_Enable = true;
            if (!backgroundWorker_主程序.IsBusy) backgroundWorker_主程序.RunWorkerAsync();
        }
        int tmr_CycleTime = 0;
        private void Refresh_UI()
        {
            if (RefreshUI_Enable)
            {
                foreach (MyUI.ExButton ExButton_temp in List_ExButton)
                {
                    ExButton_temp.Run();
                }

           
                if (FLAG_已建立表單)
                {
                    if (checkBox_ReadList_Refesh.Checked) sub_檢查Read_List_要更新();
                    if (checkBox_SendList_Refesh.Checked) sub_檢查Send_List_要更新();
                    exButton_Open.Set_LoadState(lowerMachine.GetConnectState());
                    sub_檢查comboBox_COM_要更新();

                    if (tmr_CycleTime >= 10)
                    {
                        CallBackUI.label.字串更換(lowerMachine.GetCycleTime().ToString(), label_CycleTime);
                        tmr_CycleTime = 0;
                    }
                }
                tmr_CycleTime++;
            }
    
        }
        private void backgroundWorker_主程序_DoWork(object sender, DoWorkEventArgs e)
        {
         
            while (背景執行緒_Enable)
            {
                if (form == null) form = this.FindForm();

                if (!FLAG_已建立表單 && form != null)
                {
                    udP_Cilent.Run(form);
                    if (lowerMachine == null) lowerMachine = new LowerMachine(serialPort, form);
                    lowerMachine.Set_SleepTime(this.掃描速度);
                    CallBackUI.checkbox.Checked(lowerMachine.GetSerialPortAutoConnet(), checkBox_SerialPort_自動連線);
                    this.Invoke(new Action(delegate
                    {
                        if (lowerMachine.通訊方式 == LadderConnection.Properties.Tx通訊方式.Enthernet) radioButton_Enthernet.Checked = true;
                        else if (lowerMachine.通訊方式 == LadderConnection.Properties.Tx通訊方式.SerialPort) radioButton_SerialPort.Checked = true;

                    }));
                    FLAG_已建立表單 = true;
                    break;
                }
               Thread.Sleep(1);
            }
            lowerMachine.Add_UI_Method(Refresh_UI);

        }
        void sub_檢查comboBox_COM_要更新()
        {
            bool FLAG_comboBox_COM_要更新 = false;
            string[] str_temp = TopMachine.GetAllPortname();
            if (str_temp.Length == serialPorts.Length)
            {
                for (int i = 0; i < str_temp.Length; i++)
                {
                    if (str_temp[i] != serialPorts[i])
                    {
                        FLAG_comboBox_COM_要更新 = true;
                        break;
                    }
                }
            }
            else FLAG_comboBox_COM_要更新 = true;
            if (FLAG_comboBox_COM_要更新)
            {
                serialPorts = lowerMachine.GetAllPortname();
                CallBackUI.comobox.寫入DataSoure(serialPorts, comboBox_COM);
            }
            String GetCOM = lowerMachine.GetCOM();
            if (GetCOM != "#None")
            {
                CallBackUI.comobox.字串更換(lowerMachine.GetCOM(), comboBox_COM);
            }

            
        }
        void sub_檢查Read_List_要更新()
        {
            if (lowerMachine.Read_List.Count > 200)
            {
                lowerMachine.Read_List.Clear();
            }
            if (lowerMachine.Read_List.Count > 0)
            {
                string str_temp = lowerMachine.Read_List[0];
                lowerMachine.Read_List.RemoveAt(0);
                if (str_temp != null)
                {
                    str_temp = str_temp.Replace(((char)7).ToString(), "(7)");
                    str_temp = str_temp.Replace(((char)6).ToString(), "(6)");
                    str_temp = str_temp.Replace(((char)5).ToString(), "(5)");
                    str_temp = str_temp.Replace(((char)4).ToString(), "(4)");
                    str_temp = str_temp.Replace(((char)3).ToString(), "(3)");
                    str_temp = str_temp.Replace(((char)2).ToString(), "(2)");
                    str_temp = str_temp.Replace(((char)1).ToString(), "(1)");
                }
                CallBackUI.listbox.新增項目(str_temp, listBox_Read);
                CallBackUI.listbox.自動捲軸(18, listBox_Read);
          
            }
            if (listBox_Read.Items.Count > 500)
            {
                CallBackUI.listbox.刪除第一列(listBox_Read);
                CallBackUI.listbox.自動捲軸(18, listBox_Read);
            }
        }
        void sub_檢查Send_List_要更新()
        {
            if (lowerMachine.Send_List.Count > 200)
            {
                lowerMachine.Send_List.Clear();
            }
            if (lowerMachine.Send_List.Count > 0)
            {
                string str_temp = lowerMachine.Send_List[0];
                lowerMachine.Send_List.RemoveAt(0);
                if (str_temp != null)
                {
                    str_temp = str_temp.Replace(((char)7).ToString(), "(7)");
                    str_temp = str_temp.Replace(((char)6).ToString(), "(6)");
                    str_temp = str_temp.Replace(((char)5).ToString(), "(5)");
                    str_temp = str_temp.Replace(((char)4).ToString(), "(4)");
                    str_temp = str_temp.Replace(((char)3).ToString(), "(3)");
                    str_temp = str_temp.Replace(((char)2).ToString(), "(2)");
                    str_temp = str_temp.Replace(((char)1).ToString(), "(1)");
                }

                CallBackUI.listbox.新增項目(str_temp, listBox_Send);
                CallBackUI.listbox.自動捲軸(18, listBox_Send);           
            }
            if (listBox_Send.Items.Count > 500)
            {
                CallBackUI.listbox.刪除第一列(listBox_Send);
                CallBackUI.listbox.自動捲軸(18, listBox_Send);
            }

        }
        private void exButton_Open_btnClick(object sender, EventArgs e)
        {
            if (lowerMachine.GetConnectState())
            {
                lowerMachine.CloseSerialPort();
            }
            else
            {
                lowerMachine.SetCOM(comboBox_COM.SelectedItem.ToString());
                lowerMachine.OpenSerialPort();
            }
        }

        public LowerMachine GetlowerMachine()
        {
            return lowerMachine;
        }

        public string GetDeviceBase64()
        {
            return lowerMachine.GetSaveDeviceBase64();
        }
        public string SaveDevice()
        {
            return lowerMachine.SaveDevice();
        }
        public void LoadDevice(string base64string)
        {
            List<string> devices = new List<string>();
            for (int i = 0; i < 200; i++)
            {
                devices.Add($"R{i}");
                devices.Add($"D{i}");
            }
            lowerMachine.LoadDevice(base64string , devices.ToArray());
        }
        private void checkBox_SerialPort_自動連線_CheckedChanged(object sender, EventArgs e)
        {
           if( lowerMachine!=null)
           {
               lowerMachine.SetSerialPortAutoConnet(checkBox_SerialPort_自動連線.Checked);                                
           }
        }

        private void radioButton_Enthernet_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton_Enthernet.Checked)lowerMachine.通訊方式 = LadderConnection.Properties.Tx通訊方式.Enthernet;
        }

        private void radioButton_SerialPort_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_SerialPort.Checked) lowerMachine.通訊方式 = LadderConnection.Properties.Tx通訊方式.SerialPort;          
        }

        private void udP_Cilent_Load(object sender, EventArgs e)
        {

        }
    }
}
