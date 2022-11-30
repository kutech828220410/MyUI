using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LadderConnection;
using Basic;
namespace LadderUI
{
    public partial class TopMachine_Panel : UserControl
    {
        const string Enthernet = "Enthernet";
        const string SerialProt = "SerialProt";
        private Keyboard Keys = new Keyboard();
        Basic.MyConvert myConvert = new Basic.MyConvert();
        string[] Str_serialPorts = new string[1];
        bool FLAG_COM初始化 = false;
        private List<MyUI.ExButton> List_ExButton = new List<MyUI.ExButton>();
        public TopMachine_Panel()
        {      
            InitializeComponent();
            numWordTextBox_Baudrate.Text = (115200).ToString();
            List_ExButton.Add(exButton_bool_Read);
            List_ExButton.Add(exButton_bool_Write);
            List_ExButton.Add(exButton_Close);
            List_ExButton.Add(exButton_Connection_Test);
            List_ExButton.Add(exButton_Data_Read);
            List_ExButton.Add(exButton_Data_Write);
            List_ExButton.Add(exButton_OK);
           // if (!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
        }

        private void timer_程序執行_Tick(object sender, EventArgs e)
        {
            foreach (MyUI.ExButton ExButton_temp in List_ExButton)
            {
                ExButton_temp.Run();
            }

            sub_檢查comboBox_COM_要更新();
            sub_檢查DataWrite按鈕();
            sub_檢查DataRead按鈕();
            sub_檢查DeviceWrite按鈕();
            sub_檢查DeviceRead按鈕();
            if (Keys.Key_Esc)
            {
                if (this.FindForm().ContainsFocus)
                {
                    timer_程序執行.Enabled = false;
                    this.FindForm().Hide();
                }
               
                Keys.Key_Esc = false;
            }     

        }
        void sub_檢查comboBox_COM_要更新()
        {
            bool FLAG_comboBox_COM_要更新 = false;
            string[] str_temp = TopMachine.GetAllPortname();
            if (str_temp.Length == Str_serialPorts.Length)
            {
                for (int i = 0; i < str_temp.Length; i++)
                {
                    if (str_temp[i] != Str_serialPorts[i])
                    {
                        FLAG_comboBox_COM_要更新 = true;
                        break;
                    }
                }
            }
            else FLAG_comboBox_COM_要更新 = true;
            if (FLAG_comboBox_COM_要更新)
            {
                Str_serialPorts = TopMachine.GetAllPortname();
                comboBox_COM.DataSource = Str_serialPorts;
            }
            if (!FLAG_comboBox_COM_要更新 && !FLAG_COM初始化)
            {
                comboBox_COM.Text = TopMachine.COMPort;
                FLAG_COM初始化 = true;
                TopMachine.COMPort = comboBox_COM.Text;
            }
        }
        #region 檢查DataWrite按鈕
        static string str_檢查DataWrite按鈕_視窗顯示文字 = "";
        byte cnt_檢查DataWrite按鈕 = 255;
        byte byte_檢查DataWrite按鈕_現在次數 = 0;
        String str_檢查DataWrite按鈕_Device = "";
        String str_檢查DataWrite按鈕_value = "";
        Int32 Int32_檢查DataWrite按鈕_value = 0;
        Int64 Int64_檢查DataWrite按鈕_value = 0;
        void sub_檢查DataWrite按鈕()
        {
            if (cnt_檢查DataWrite按鈕 == 255) cnt_檢查DataWrite按鈕 = 1;
            if (cnt_檢查DataWrite按鈕 == 1) cnt_檢查DataWrite按鈕_00_檢查按下(ref cnt_檢查DataWrite按鈕);
            if (cnt_檢查DataWrite按鈕 == 2) cnt_檢查DataWrite按鈕_00_初始化(ref cnt_檢查DataWrite按鈕);
            if (cnt_檢查DataWrite按鈕 == 3) cnt_檢查DataWrite按鈕 = 10;

            if (cnt_檢查DataWrite按鈕 == 10) cnt_檢查DataWrite按鈕_10_檢查並更新Device(ref cnt_檢查DataWrite按鈕);  
            if (cnt_檢查DataWrite按鈕 == 11) cnt_檢查DataWrite按鈕_10_檢查Single或Double(ref cnt_檢查DataWrite按鈕);
            if (cnt_檢查DataWrite按鈕 == 12) cnt_檢查DataWrite按鈕_10_開始寫入(ref cnt_檢查DataWrite按鈕);
            if (cnt_檢查DataWrite按鈕 == 13) cnt_檢查DataWrite按鈕_10_更新控件(ref cnt_檢查DataWrite按鈕);
            if (cnt_檢查DataWrite按鈕 == 14) cnt_檢查DataWrite按鈕 = 150;

            if (cnt_檢查DataWrite按鈕 == 150) cnt_檢查DataWrite按鈕_150_寫入成功(ref cnt_檢查DataWrite按鈕);
            if (cnt_檢查DataWrite按鈕 == 151) cnt_檢查DataWrite按鈕 = 250;

            if (cnt_檢查DataWrite按鈕 == 200) cnt_檢查DataWrite按鈕_200_寫入失敗(ref cnt_檢查DataWrite按鈕);
            if (cnt_檢查DataWrite按鈕 == 201) cnt_檢查DataWrite按鈕 = 250;

            if (cnt_檢查DataWrite按鈕 == 250) cnt_檢查DataWrite按鈕_250_檢查放開(ref cnt_檢查DataWrite按鈕);
            if (cnt_檢查DataWrite按鈕 == 251) cnt_檢查DataWrite按鈕_250_顯示彈出視窗(ref cnt_檢查DataWrite按鈕);
            if (cnt_檢查DataWrite按鈕 == 252) cnt_檢查DataWrite按鈕 = 255;
        }
        void cnt_檢查DataWrite按鈕_00_檢查按下(ref byte cnt)
        {
            if(exButton_Data_Write.Load_WriteState())
            {
                cnt++;
            }
          
        }
        void cnt_檢查DataWrite按鈕_00_初始化(ref byte cnt)
        {
            byte_檢查DataWrite按鈕_現在次數 = 0;
            str_檢查DataWrite按鈕_value = TextBox_Data_Value_Write.Text;
            Int32_檢查DataWrite按鈕_value = 0;
            Int64_檢查DataWrite按鈕_value = 0;
            str_檢查DataWrite按鈕_Device = TextBox_Data_Device_Write.Text;
            cnt++;
        }
        void cnt_檢查DataWrite按鈕_10_檢查並更新Device(ref byte cnt)
        {
            int Device_num = 0;
            String str_temp = str_檢查DataWrite按鈕_Device;
            if(str_temp.Length > 1)
            {
                str_temp = str_temp.Substring(1, str_temp.Length - 1);
                if (Int32.TryParse(str_temp,out Device_num))
                {
                    str_檢查DataWrite按鈕_Device = str_檢查DataWrite按鈕_Device.Substring(0, 1) + (Device_num + byte_檢查DataWrite按鈕_現在次數).ToString();
                    cnt++;
                }
                else
                {
                    cnt = 200;
                }
            }
            else
            {
                cnt = 200;
            }
        }
        void cnt_檢查DataWrite按鈕_10_檢查Single或Double(ref byte cnt)
        {
            if(radioButton_Data_Double_Write.Checked)
            {
                if (Int64.TryParse(str_檢查DataWrite按鈕_value, out Int64_檢查DataWrite按鈕_value))
                {

                    cnt++;         
                }
                else
                {
                    cnt = 200;
                }    
           
            }
            else if (radioButton_Data_Single_Write.Checked)
            {
                if (Int32.TryParse(str_檢查DataWrite按鈕_value, out Int32_檢查DataWrite按鈕_value))
                {
                    cnt++;   
                }
                else
                {
                    cnt = 200;
                }                                
            }     
        }
        void cnt_檢查DataWrite按鈕_10_開始寫入(ref byte cnt)
        {
            String Device = str_檢查DataWrite按鈕_Device;
            int temp = 0;
            if (radioButton_Data_Single_Write.Checked)
            {
                temp = TopMachine.DataWrite(Device, Int32_檢查DataWrite按鈕_value);
            }
            else if (radioButton_Data_Double_Write.Checked)
            {
                temp = TopMachine.DataWrite(Device, Int64_檢查DataWrite按鈕_value);
            }
            if (temp == -1)
            {
                cnt = 200;
                return;
            }
            else if (temp == 1)
            {
                return;
            }
            else if (temp == 255)
            {
                cnt++;
                return;
            }
         
        }
   
        void cnt_檢查DataWrite按鈕_10_更新控件(ref byte cnt)
        {
            cnt++;
        }
        void cnt_檢查DataWrite按鈕_150_寫入成功(ref byte cnt)
        {
            str_檢查DataWrite按鈕_視窗顯示文字 = "寫入成功!";
            cnt++;
        }
        void cnt_檢查DataWrite按鈕_200_寫入失敗(ref byte cnt)
        {
            str_檢查DataWrite按鈕_視窗顯示文字 = "寫入失敗!";
            cnt++;
        }

        void cnt_檢查DataWrite按鈕_250_檢查放開(ref byte cnt)
        {
            if (!exButton_Data_Write.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_檢查DataWrite按鈕_250_顯示彈出視窗(ref byte cnt)
        {
            cnt++;
            if (str_檢查DataWrite按鈕_視窗顯示文字 == "寫入成功!")
            {
                MessageBox.Show(str_檢查DataWrite按鈕_視窗顯示文字, "Asterisk", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            if (str_檢查DataWrite按鈕_視窗顯示文字 == "寫入失敗!")
            {
                MessageBox.Show(str_檢查DataWrite按鈕_視窗顯示文字, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }
    
        void cnt_檢查DataWrite按鈕_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        #region 檢查DataRead按鈕
        static string str_檢查DataRead按鈕_視窗顯示文字 = "";
        byte cnt_檢查DataRead按鈕 = 255;
        byte byte_檢查DataRead按鈕_現在次數 = 0;
        String str_檢查DataRead按鈕_Device = "";
        String str_檢查DataRead按鈕_value = "";
        Int32 Int32_檢查DataRead按鈕_value = 0;
        Int64 Int64_檢查DataRead按鈕_value = 0;
        void sub_檢查DataRead按鈕()
        {
            if (cnt_檢查DataRead按鈕 == 255) cnt_檢查DataRead按鈕 = 1;
            if (cnt_檢查DataRead按鈕 == 1) cnt_檢查DataRead按鈕_00_檢查按下(ref cnt_檢查DataRead按鈕);
            if (cnt_檢查DataRead按鈕 == 2) cnt_檢查DataRead按鈕_00_初始化(ref cnt_檢查DataRead按鈕);
            if (cnt_檢查DataRead按鈕 == 3) cnt_檢查DataRead按鈕 = 10;

            if (cnt_檢查DataRead按鈕 == 10) cnt_檢查DataRead按鈕_10_檢查並更新Device(ref cnt_檢查DataRead按鈕);
            if (cnt_檢查DataRead按鈕 == 11) cnt_檢查DataRead按鈕_10_開始讀取(ref cnt_檢查DataRead按鈕);
            if (cnt_檢查DataRead按鈕 == 12) cnt_檢查DataRead按鈕_10_更新控件(ref cnt_檢查DataRead按鈕);
            if (cnt_檢查DataRead按鈕 == 13) cnt_檢查DataRead按鈕 = 150;

            if (cnt_檢查DataRead按鈕 == 150) cnt_檢查DataRead按鈕_150_讀取成功(ref cnt_檢查DataRead按鈕);
            if (cnt_檢查DataRead按鈕 == 151) cnt_檢查DataRead按鈕 = 250;

            if (cnt_檢查DataRead按鈕 == 200) cnt_檢查DataRead按鈕_200_讀取失敗(ref cnt_檢查DataRead按鈕);
            if (cnt_檢查DataRead按鈕 == 201) cnt_檢查DataRead按鈕 = 250;

            if (cnt_檢查DataRead按鈕 == 250) cnt_檢查DataRead按鈕_250_檢查放開(ref cnt_檢查DataRead按鈕);
            if (cnt_檢查DataRead按鈕 == 251) cnt_檢查DataRead按鈕_250_顯示彈出視窗(ref cnt_檢查DataRead按鈕);
            if (cnt_檢查DataRead按鈕 == 252) cnt_檢查DataRead按鈕 = 255;
        }
        void cnt_檢查DataRead按鈕_00_檢查按下(ref byte cnt)
        {
            if (exButton_Data_Read.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_檢查DataRead按鈕_00_初始化(ref byte cnt)
        {
            byte_檢查DataRead按鈕_現在次數 = 0;
            str_檢查DataRead按鈕_value = TextBox_Data_Value_Read.Text;
            Int32_檢查DataRead按鈕_value = 0;
            Int64_檢查DataRead按鈕_value = 0;
            str_檢查DataRead按鈕_Device = TextBox_Data_Device_Read.Text;
            cnt++;
        }
        void cnt_檢查DataRead按鈕_10_檢查並更新Device(ref byte cnt)
        {
            int Device_num = 0;
            String str_temp = str_檢查DataRead按鈕_Device;
            if (str_temp.Length > 1)
            {
                str_temp = str_temp.Substring(1, str_temp.Length - 1);
                if (Int32.TryParse(str_temp, out Device_num))
                {
                    str_檢查DataRead按鈕_Device = str_檢查DataRead按鈕_Device.Substring(0, 1) + (Device_num + byte_檢查DataRead按鈕_現在次數).ToString();
                    cnt++;
                }
                else
                {
                    cnt = 200;
                }
            }
            else
            {
                cnt = 200;
            }
        }
        void cnt_檢查DataRead按鈕_10_開始讀取(ref byte cnt)
        {
            String Device = str_檢查DataRead按鈕_Device;
            int temp = 0;
            if (radioButton_Data_Single_Read.Checked)
            {
                temp = TopMachine.DataRead(Device, ref Int32_檢查DataRead按鈕_value);
            }
            else if (radioButton_Data_Double_Read.Checked)
            {
                temp = TopMachine.DataRead(Device, ref Int64_檢查DataRead按鈕_value);
            }
            if (temp == -1)
            {
                cnt = 200;
                return;
            }
            else if (temp == 1)
            {
                return;
            }
            else if (temp == 255)
            {
                cnt++;
                return;
            }

        }

        void cnt_檢查DataRead按鈕_10_更新控件(ref byte cnt)
        {
            if (radioButton_Data_Single_Read.Checked)
            {
                TextBox_Data_Value_Read.Text = Int32_檢查DataRead按鈕_value.ToString();
            }
            else if (radioButton_Data_Double_Read.Checked)
            {
                TextBox_Data_Value_Read.Text = Int64_檢查DataRead按鈕_value.ToString();
            }
            cnt++;
        }
        void cnt_檢查DataRead按鈕_150_讀取成功(ref byte cnt)
        {
            str_檢查DataRead按鈕_視窗顯示文字 = "讀取成功!";
            cnt++;
        }
        void cnt_檢查DataRead按鈕_200_讀取失敗(ref byte cnt)
        {
            str_檢查DataRead按鈕_視窗顯示文字 = "讀取失敗!";
            cnt++;
        }

        void cnt_檢查DataRead按鈕_250_檢查放開(ref byte cnt)
        {
            if (!exButton_Data_Read.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_檢查DataRead按鈕_250_顯示彈出視窗(ref byte cnt)
        {
            cnt++;
            if (str_檢查DataRead按鈕_視窗顯示文字 == "讀取成功!")
            {
                MessageBox.Show(str_檢查DataRead按鈕_視窗顯示文字, "Asterisk", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            if (str_檢查DataRead按鈕_視窗顯示文字 == "讀取失敗!")
            {
                MessageBox.Show(str_檢查DataRead按鈕_視窗顯示文字, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        void cnt_檢查DataRead按鈕_(ref byte cnt)
        {
            cnt++;
        }
        #endregion

        #region 檢查DeviceWrite按鈕
        static string str_檢查DeviceWrite按鈕_視窗顯示文字 = "";
        byte cnt_檢查DeviceWrite按鈕 = 255;
        byte byte_檢查DeviceWrite按鈕_現在次數 = 0;
        String str_檢查DeviceWrite按鈕_Device = "";
        bool FLAG_檢查DeviceWrite按鈕_value =false;
        void sub_檢查DeviceWrite按鈕()
        {
            if (cnt_檢查DeviceWrite按鈕 == 255) cnt_檢查DeviceWrite按鈕 = 1;
            if (cnt_檢查DeviceWrite按鈕 == 1) cnt_檢查DeviceWrite按鈕_00_檢查按下(ref cnt_檢查DeviceWrite按鈕);
            if (cnt_檢查DeviceWrite按鈕 == 2) cnt_檢查DeviceWrite按鈕_00_初始化(ref cnt_檢查DeviceWrite按鈕);
            if (cnt_檢查DeviceWrite按鈕 == 3) cnt_檢查DeviceWrite按鈕 = 10;

            if (cnt_檢查DeviceWrite按鈕 == 10) cnt_檢查DeviceWrite按鈕_10_檢查並更新Device(ref cnt_檢查DeviceWrite按鈕);
            if (cnt_檢查DeviceWrite按鈕 == 11) cnt_檢查DeviceWrite按鈕_10_開始寫入(ref cnt_檢查DeviceWrite按鈕);
            if (cnt_檢查DeviceWrite按鈕 == 12) cnt_檢查DeviceWrite按鈕_10_更新控件(ref cnt_檢查DeviceWrite按鈕);
            if (cnt_檢查DeviceWrite按鈕 == 13) cnt_檢查DeviceWrite按鈕 = 150;

            if (cnt_檢查DeviceWrite按鈕 == 150) cnt_檢查DeviceWrite按鈕_150_寫入成功(ref cnt_檢查DeviceWrite按鈕);
            if (cnt_檢查DeviceWrite按鈕 == 151) cnt_檢查DeviceWrite按鈕 = 250;

            if (cnt_檢查DeviceWrite按鈕 == 200) cnt_檢查DeviceWrite按鈕_200_寫入失敗(ref cnt_檢查DeviceWrite按鈕);
            if (cnt_檢查DeviceWrite按鈕 == 201) cnt_檢查DeviceWrite按鈕 = 250;

            if (cnt_檢查DeviceWrite按鈕 == 250) cnt_檢查DeviceWrite按鈕_250_檢查放開(ref cnt_檢查DeviceWrite按鈕);
            if (cnt_檢查DeviceWrite按鈕 == 251) cnt_檢查DeviceWrite按鈕_250_顯示彈出視窗(ref cnt_檢查DeviceWrite按鈕);
            if (cnt_檢查DeviceWrite按鈕 == 252) cnt_檢查DeviceWrite按鈕 = 255;
        }
        void cnt_檢查DeviceWrite按鈕_00_檢查按下(ref byte cnt)
        {
            if (exButton_bool_Write.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_檢查DeviceWrite按鈕_00_初始化(ref byte cnt)
        {
            byte_檢查DeviceWrite按鈕_現在次數 = 0;
            if (radioButton_bool_ON_Write.Checked) FLAG_檢查DeviceWrite按鈕_value = true;
            else FLAG_檢查DeviceWrite按鈕_value = false;
            str_檢查DeviceWrite按鈕_Device = TextBox_bool_Device_Write.Text;
            cnt++;
        }
        void cnt_檢查DeviceWrite按鈕_10_檢查並更新Device(ref byte cnt)
        {
            int Device_num = 0;
            String str_temp = str_檢查DeviceWrite按鈕_Device;
            if (str_temp.Length > 1)
            {
                str_temp = str_temp.Substring(1, str_temp.Length - 1);
                if (Int32.TryParse(str_temp, out Device_num))
                {
                    str_檢查DeviceWrite按鈕_Device = str_檢查DeviceWrite按鈕_Device.Substring(0, 1) + (Device_num + byte_檢查DeviceWrite按鈕_現在次數).ToString();
                    cnt++;
                }
                else
                {
                    cnt = 200;
                }
            }
            else
            {
                cnt = 200;
            }
        }
        void cnt_檢查DeviceWrite按鈕_10_開始寫入(ref byte cnt)
        {
            String Device = str_檢查DeviceWrite按鈕_Device;
            int temp = 0;

            temp = TopMachine.DeviceWrite(Device, FLAG_檢查DeviceWrite按鈕_value);
            
            if (temp == -1)
            {
                cnt = 200;
                return;
            }
            else if (temp == 1)
            {
                return;
            }
            else if (temp == 255)
            {
                cnt++;
                return;
            }

        }

        void cnt_檢查DeviceWrite按鈕_10_更新控件(ref byte cnt)
        {
            cnt++;
        }
        void cnt_檢查DeviceWrite按鈕_150_寫入成功(ref byte cnt)
        {
            str_檢查DeviceWrite按鈕_視窗顯示文字 = "寫入成功!";
            cnt++;
        }
        void cnt_檢查DeviceWrite按鈕_200_寫入失敗(ref byte cnt)
        {
            str_檢查DeviceWrite按鈕_視窗顯示文字 = "寫入失敗!";
            cnt++;
        }

        void cnt_檢查DeviceWrite按鈕_250_檢查放開(ref byte cnt)
        {
            if (!exButton_bool_Write.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_檢查DeviceWrite按鈕_250_顯示彈出視窗(ref byte cnt)
        {
            cnt++;
            if (str_檢查DeviceWrite按鈕_視窗顯示文字 == "寫入成功!")
            {
                MessageBox.Show(str_檢查DeviceWrite按鈕_視窗顯示文字, "Asterisk", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            if (str_檢查DeviceWrite按鈕_視窗顯示文字 == "寫入失敗!")
            {
                MessageBox.Show(str_檢查DeviceWrite按鈕_視窗顯示文字, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        void cnt_檢查DeviceWrite按鈕_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        #region 檢查DeviceRead按鈕
        static string str_檢查DeviceRead按鈕_視窗顯示文字 = "";
        byte cnt_檢查DeviceRead按鈕 = 255;
        byte byte_檢查DeviceRead按鈕_現在次數 = 0;
        String str_檢查DeviceRead按鈕_Device = "";
        bool FLAG_檢查DeviceRead按鈕_value = false;
        void sub_檢查DeviceRead按鈕()
        {
            if (cnt_檢查DeviceRead按鈕 == 255) cnt_檢查DeviceRead按鈕 = 1;
            if (cnt_檢查DeviceRead按鈕 == 1) cnt_檢查DeviceRead按鈕_00_檢查按下(ref cnt_檢查DeviceRead按鈕);
            if (cnt_檢查DeviceRead按鈕 == 2) cnt_檢查DeviceRead按鈕_00_初始化(ref cnt_檢查DeviceRead按鈕);
            if (cnt_檢查DeviceRead按鈕 == 3) cnt_檢查DeviceRead按鈕 = 10;

            if (cnt_檢查DeviceRead按鈕 == 10) cnt_檢查DeviceRead按鈕_10_檢查並更新Device(ref cnt_檢查DeviceRead按鈕);
            if (cnt_檢查DeviceRead按鈕 == 11) cnt_檢查DeviceRead按鈕_10_開始讀取(ref cnt_檢查DeviceRead按鈕);
            if (cnt_檢查DeviceRead按鈕 == 12) cnt_檢查DeviceRead按鈕_10_更新控件(ref cnt_檢查DeviceRead按鈕);
            if (cnt_檢查DeviceRead按鈕 == 13) cnt_檢查DeviceRead按鈕 = 150;

            if (cnt_檢查DeviceRead按鈕 == 150) cnt_檢查DeviceRead按鈕_150_讀取成功(ref cnt_檢查DeviceRead按鈕);
            if (cnt_檢查DeviceRead按鈕 == 151) cnt_檢查DeviceRead按鈕 = 250;

            if (cnt_檢查DeviceRead按鈕 == 200) cnt_檢查DeviceRead按鈕_200_讀取失敗(ref cnt_檢查DeviceRead按鈕);
            if (cnt_檢查DeviceRead按鈕 == 201) cnt_檢查DeviceRead按鈕 = 250;

            if (cnt_檢查DeviceRead按鈕 == 250) cnt_檢查DeviceRead按鈕_250_檢查放開(ref cnt_檢查DeviceRead按鈕);
            if (cnt_檢查DeviceRead按鈕 == 251) cnt_檢查DeviceRead按鈕_250_顯示彈出視窗(ref cnt_檢查DeviceRead按鈕);
            if (cnt_檢查DeviceRead按鈕 == 252) cnt_檢查DeviceRead按鈕 = 255;
        }
        void cnt_檢查DeviceRead按鈕_00_檢查按下(ref byte cnt)
        {
            if (exButton_bool_Read.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_檢查DeviceRead按鈕_00_初始化(ref byte cnt)
        {
            byte_檢查DeviceRead按鈕_現在次數 = 0;
            str_檢查DeviceRead按鈕_Device = TextBox_bool_Device_Read.Text;
            cnt++;
        }
        void cnt_檢查DeviceRead按鈕_10_檢查並更新Device(ref byte cnt)
        {
            int Device_num = 0;
            String str_temp = str_檢查DeviceRead按鈕_Device;
            if (str_temp.Length > 1)
            {
                str_temp = str_temp.Substring(1, str_temp.Length - 1);
                if (Int32.TryParse(str_temp, out Device_num))
                {
                    str_檢查DeviceRead按鈕_Device = str_檢查DeviceRead按鈕_Device.Substring(0, 1) + (Device_num + byte_檢查DeviceRead按鈕_現在次數).ToString();
                    cnt++;
                }
                else
                {
                    cnt = 200;
                }
            }
            else
            {
                cnt = 200;
            }
        }
        void cnt_檢查DeviceRead按鈕_10_開始讀取(ref byte cnt)
        {
            String Device = str_檢查DeviceRead按鈕_Device;
            int temp = 0;

            temp = TopMachine.DeviceRead(Device,ref FLAG_檢查DeviceRead按鈕_value);

            if (temp == -1)
            {
                cnt = 200;
                return;
            }
            else if (temp == 1)
            {
                return;
            }
            else if (temp == 255)
            {
                cnt++;
                return;
            }

        }

        void cnt_檢查DeviceRead按鈕_10_更新控件(ref byte cnt)
        {
            if (FLAG_檢查DeviceRead按鈕_value) radioButton_bool_ON_Read.Checked = true;
            else radioButton_bool_OFF_Read.Checked = true;
            cnt++;
        }
        void cnt_檢查DeviceRead按鈕_150_讀取成功(ref byte cnt)
        {
            str_檢查DeviceRead按鈕_視窗顯示文字 = "讀取成功!";
            cnt++;
        }
        void cnt_檢查DeviceRead按鈕_200_讀取失敗(ref byte cnt)
        {
            str_檢查DeviceRead按鈕_視窗顯示文字 = "讀取失敗!";
            cnt++;
        }

        void cnt_檢查DeviceRead按鈕_250_檢查放開(ref byte cnt)
        {
            if (!exButton_bool_Read.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_檢查DeviceRead按鈕_250_顯示彈出視窗(ref byte cnt)
        {
            cnt++;
            if (str_檢查DeviceRead按鈕_視窗顯示文字 == "讀取成功!")
            {
                MessageBox.Show(str_檢查DeviceRead按鈕_視窗顯示文字, "Asterisk", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            if (str_檢查DeviceRead按鈕_視窗顯示文字 == "讀取失敗!")
            {
                MessageBox.Show(str_檢查DeviceRead按鈕_視窗顯示文字, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        void cnt_檢查DeviceRead按鈕_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        private void exButton_Connection_Test_btnClick(object sender, EventArgs e)
        {
          if(comboBox_COM.SelectedItem!=null)  TopMachine.SetCOM(comboBox_COM.SelectedItem.ToString());
            TopMachine.通訊測試();
        }
        private void comboBox_COM_SelectedIndexChanged(object sender, EventArgs e)
        {
    
        }
        private void exButton_Close_btnClick(object sender, EventArgs e)
        {
            this.FindForm().Hide();
        }

        private void exButton_OK_btnClick(object sender, EventArgs e)
        {
            TopMachine.COMPort = comboBox_COM.Text;
            this.FindForm().Hide();
        }

        private void radioButton_SerialPort_CheckedChanged(object sender, EventArgs e)
        {
            LadderConnection.Properties.通訊方式 = LadderConnection.Properties.Tx通訊方式.SerialPort;
            groupBox_SerialProt.Enabled = true;
            udP_Cilent.Enabled = false;
        }

        private void radioButton_Enthernet_CheckedChanged(object sender, EventArgs e)
        {
            LadderConnection.Properties.通訊方式 = LadderConnection.Properties.Tx通訊方式.Enthernet;
            groupBox_SerialProt.Enabled = false;
            udP_Cilent.Enabled = true;
        }

        private void TopMachine_Panel_Load(object sender, EventArgs e)
        {
            udP_Cilent.Refresh();
        }   
    }
}
