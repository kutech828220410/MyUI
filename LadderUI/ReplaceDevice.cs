using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using Basic;
using LadderProperty;
using LadderUI;
namespace LadderForm
{
    public partial class ReplaceDevice : Form
    {
        private Keyboard Keys = new Keyboard();
        private MyConvert myConvert = new MyConvert();
        private static ReplaceDevice replaceDevice;
        private static readonly object synRoot = new object();
        public static DEVICE device = new DEVICE(false);
        public static bool 視窗已建立;
        public static string 起始視窗文字 = "";
        public bool FLAG_初始位置重新設置 = false;
        private Point 起始位置 = new Point();
        private Point 現在位置 = new Point();
        private int 起始位置畫面索引 = 0;
        public static int 起始鼠標列數 = 0;
        public static LADDER_Panel.LADDER LADDER_buf = new LADDER_Panel.LADDER();
        public static List<LADDER_Panel.LADDER[]> ladder_list_buf;
        public bool 背景執行緒_Enable = false;
        public static int 一列格數;
        public static int 一個畫面列數;

        public static int 搜索範圍上限_程式列數;
        public static int 搜索範圍下限_程式列數;

        public static int 搜索範圍上限;
        public static int 搜索範圍下限;

        private List<MyUI.ExButton> List_ExButton = new List<MyUI.ExButton>();

        public static ReplaceDevice GetForm(Point P0)
        {
            lock (synRoot)
            {
                if (replaceDevice == null)
                {
                    replaceDevice = new ReplaceDevice();
                }
                replaceDevice.StartPosition = FormStartPosition.Manual;
                P0.X -= replaceDevice.Size.Width / 2;
                P0.Y -= replaceDevice.Size.Height / 2;
                replaceDevice.Location = P0;
                視窗已建立 = true;
            }
            return replaceDevice;
        }
        private void ReplaceDevice_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < LADDER_buf.元素數量; i++)
            {
                if (DEVICE.TestDevice(LADDER_buf.ladderParam[i]))
                {
                    起始視窗文字 = LADDER_buf.ladderParam[i];
                    break;
                }
            }
            起始位置 = LADDER_Panel.操作方框索引;
            起始位置畫面索引 = LADDER_Panel.顯示畫面列數索引;
            replaceDevice.textBox_Earlier_device.Text = 起始視窗文字;
            nud_specified_range_lower.Value = (decimal)搜索範圍下限;
            nud_specified_range_upper.Value = (decimal)搜索範圍上限;

            搜索範圍下限 = (int)nud_specified_range_lower.Value;
            for (int Y = 0; Y < ladder_list_buf.Count; Y++)
            {
                if (ladder_list_buf[Y][0].ladderParam[0] == 搜索範圍下限.ToString())
                {
                    搜索範圍下限_程式列數 = Y;
                    break;
                }
                if (ladder_list_buf[Y][0].ladderParam[1] == 搜索範圍下限.ToString())
                {
                    搜索範圍下限_程式列數 = Y;
                    break;
                }
            }
            搜索範圍上限 = (int)nud_specified_range_upper.Value;
            for (int Y = 0; Y < ladder_list_buf.Count; Y++)
            {
                if (ladder_list_buf[Y][0].ladderParam[0] == 搜索範圍上限.ToString())
                {
                    搜索範圍上限_程式列數 = Y;
                    break;
                }
                if (ladder_list_buf[Y][0].ladderParam[1] == 搜索範圍上限.ToString())
                {
                    搜索範圍上限_程式列數 = Y;
                    break;
                }
            }
            現在位置 = new Point();
            timer_程序執行.Enabled = true;
            背景執行緒_Enable = true;

            List_ExButton.Add(exButton_Close);
            List_ExButton.Add(exButton_FindNext);
            List_ExButton.Add(exButton_Replace);
            List_ExButton.Add(exButton_Replace_all);

            if (!backgroundWorker.IsBusy) backgroundWorker.RunWorkerAsync();
        }
        private void ReplaceDevice_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer_程序執行.Dispose();
            視窗已建立 = false;
            replaceDevice = null;
        }
        private void ReplaceDevice_FormClosed(object sender, FormClosedEventArgs e)
        {
            視窗已建立 = false;
            replaceDevice = null;
        }
        public ReplaceDevice()
        {
            InitializeComponent();
        }

        private void timer_程序執行_Tick(object sender, EventArgs e)
        {
            if (replaceDevice != null)
            {
                foreach (MyUI.ExButton ExButton_temp in List_ExButton)
                {
                    ExButton_temp.Run();
                }

                if (搜索範圍下限 != (int)nud_specified_range_lower.Value)
                {
                    搜索範圍下限 = (int)nud_specified_range_lower.Value;
                    for (int Y = 0; Y < ladder_list_buf.Count; Y++)
                    {
                        if (ladder_list_buf[Y][0].ladderParam[0] == 搜索範圍下限.ToString())
                        {
                            搜索範圍下限_程式列數 = Y;
                            return;
                        }
                        if (ladder_list_buf[Y][0].ladderParam[1] == 搜索範圍下限.ToString())
                        {
                            搜索範圍下限_程式列數 = Y;
                            return;
                        }
                    }
                }
                if (搜索範圍上限 != (int)nud_specified_range_upper.Value)
                {
                    搜索範圍上限 = (int)nud_specified_range_upper.Value;
                    for (int Y = 0; Y < ladder_list_buf.Count; Y++)
                    {
                        if (ladder_list_buf[Y][0].ladderParam[0] == 搜索範圍上限.ToString())
                        {
                            搜索範圍上限_程式列數 = Y;
                            return;
                        }
                        if (ladder_list_buf[Y][0].ladderParam[1] == 搜索範圍上限.ToString())
                        {
                            搜索範圍上限_程式列數 = Y;
                            return;
                        }
                    }
                }

                if (replaceDevice.ContainsFocus) 視窗已建立 = true;
                else 視窗已建立 = false;

                if (!replaceDevice.ContainsFocus)
                {
                    SystemSounds.Beep.Play();
                    replaceDevice.Focus();
                }
                sub_檢查ENTER按下();
                sub_搜尋指定Device();
                sub_替換指定Device();
                sub_替換所有Device();
                if (Keys.Key_Esc)
                {
                    replaceDevice.Close();
                }
            }


        }
        String str_devicename = "";
        String str_Earlier_device = "";
        String str_New_device = "";
        String str_彈出視窗文字 = "";
        int 替換編號起始值 = 0;
        int 搜尋起點 = 0;
        int 搜尋終點 = 0;
        bool FLAG_已到搜索終點 = false;
        bool FLAG_註解已更換過 = false;
        bool FLAG_Messagebox已開啟 = false;
        bool FLAG_替換有錯誤 = false;
        bool FLAG_搜索有錯誤 = false;
        DialogResult Result;

        byte cnt_搜尋指定Device = 255;
        byte cnt_替換指定Device = 255;
        byte cnt_替換所有Device = 255;
        byte cnt_檢查ENTER按下 = 255;

        void sub_檢查ENTER按下()
        {
            if (cnt_檢查ENTER按下 == 255) cnt_檢查ENTER按下 = 1;
            if (cnt_檢查ENTER按下 == 1) cnt_檢查ENTER按下_檢查按下(ref cnt_檢查ENTER按下);
            if (cnt_檢查ENTER按下 == 2) cnt_檢查ENTER按下_檢查放開(ref cnt_檢查ENTER按下);
            if (cnt_檢查ENTER按下 == 3) cnt_檢查ENTER按下 = 255;
        }
        void cnt_檢查ENTER按下_檢查按下(ref byte cnt)
        {
            if (Keys.Key_Enter)
            {
                if (cnt_搜尋指定Device == 255)
                {
                    if (radioButton_from_top_to_buttom.Checked)
                    {
                        搜尋終點 = ladder_list_buf.Count;
                    }
                    else if (radioButton_from_cursor_to_buttom.Checked)
                    {
                        搜尋終點 = ladder_list_buf.Count;
                    }
                    else if (radioButton_specified_range.Checked)
                    {
                        搜尋終點 = 搜索範圍上限_程式列數 + 1;
                    }
                    cnt_搜尋指定Device = 1;
                }
                cnt++;
            }

        }
        void cnt_檢查ENTER按下_檢查放開(ref byte cnt)
        {
            if (!Keys.Key_Enter)
            {

                cnt++;
            }
        }
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
          
                System.Threading.Thread.Sleep(1);
            }

        }
        #region 搜尋指定Device
        void sub_搜尋指定Device()
        {
            exButton_FindNext.Set_LoadState(cnt_搜尋指定Device != 255);
            if (cnt_搜尋指定Device == 1) cnt_搜尋指定Device_初始化(ref cnt_搜尋指定Device);
            if (cnt_搜尋指定Device == 2) cnt_搜尋指定Device_檢查字串可搜尋(ref cnt_搜尋指定Device);
            if (cnt_搜尋指定Device == 3) cnt_搜尋指定Device_開始搜尋(ref cnt_搜尋指定Device);
            if (cnt_搜尋指定Device == 4) cnt_搜尋指定Device_更新索引及現在位置(ref cnt_搜尋指定Device);
            if (cnt_搜尋指定Device == 5) cnt_搜尋指定Device = 255;

            if (cnt_搜尋指定Device == 200) cnt_搜尋指定Device_200_顯示彈出視窗(ref cnt_搜尋指定Device);
            if (cnt_搜尋指定Device == 201) cnt_搜尋指定Device = 255;
        }
        void cnt_搜尋指定Device_初始化(ref byte cnt)
        {
            str_devicename = "";
            CallBackUI.textbox.取得字串(ref str_devicename, textBox_Earlier_device);
            str_devicename = str_devicename.ToUpper();
            FLAG_Messagebox已開啟 = false;
            FLAG_已到搜索終點 = false;
            FLAG_搜索有錯誤 = false;
            if (str_devicename == "")
            {
                str_彈出視窗文字 = "非法元件,查詢失敗!";
                FLAG_搜索有錯誤 = true;
                cnt = 200;
                return;
            }
            else cnt++;

        }
        void cnt_搜尋指定Device_檢查字串可搜尋(ref byte cnt)
        {
            if (DEVICE.TestDevice(str_devicename)) cnt++;
            else
            {
                str_彈出視窗文字 = "輸入字串不合法,查詢失敗!";
                FLAG_搜索有錯誤 = true;
                cnt = 200;
                return;
            }
        }
        void cnt_搜尋指定Device_開始搜尋(ref byte cnt)
        {
            int 要搜尋的點數 = (int)nud_num_point.Value;
            for (int Y = 現在位置.Y; Y < 搜尋終點; Y++)
            {
                for (int X = 0; X < 一列格數; X++)
                {
                    if (X >= 現在位置.X)
                    {
                        for (int i = 0; i < ladder_list_buf[Y][X].元素數量; i++)
                        {
                            for (int k = 0; k < 要搜尋的點數; k++)
                            {
                                String str_接點 = "";
                                String str_接點類型 = str_devicename.Substring(0, 1);
                                String str_接點編號 = str_devicename.Substring(1, str_devicename.Length - 1);
                                int int_接點編號 = 0;

                                if (!Int32.TryParse(str_接點編號, out int_接點編號))
                                {
                                    str_彈出視窗文字 = "輸入字串不合法,查詢失敗!";
                                    FLAG_搜索有錯誤 = true;
                                    cnt = 200;
                                    return;
                                }
                                if (str_接點類型 == "X" || str_接點類型 == "Y")
                                {
                                    int_接點編號 = myConvert.八進位轉十進位(int_接點編號);
                                }

                                int_接點編號 += k;
                                if (str_接點類型 == "X" || str_接點類型 == "Y")
                                {
                                    int_接點編號 = myConvert.十進位轉八進位(int_接點編號);
                                }

                                str_接點 = str_接點類型 + int_接點編號.ToString();
                                if (!DEVICE.TestDevice(str_接點))
                                {
                                    str_彈出視窗文字 = "輸入字串不合法,查詢失敗!";
                                    FLAG_搜索有錯誤 = true;
                                    cnt = 200;
                                    return;
                                }

                                if (ladder_list_buf[Y][X].ladderParam[i] != null)
                                {
                                    if (ladder_list_buf[Y][X].ladderParam[i] == str_接點)
                                    {
                                        現在位置.X = X;
                                        現在位置.Y = Y;
                                        cnt++;
                                        return;
                                    }
                                }
                            }
                        }
                    }

                }
                現在位置.X = 0;
            }
            cnt = 200;
            str_彈出視窗文字 = "已搜尋到終點!";
            FLAG_已到搜索終點 = true;
            FLAG_初始位置重新設置 = false;
        }
        void cnt_搜尋指定Device_更新索引及現在位置(ref byte cnt)
        {
            int temp_X = 現在位置.X;
            int temp_Y = 現在位置.Y;
            int temp_顯示畫面列數索引 = 0;
            while (true)
            {
                if (temp_Y > 一個畫面列數 - 1)
                {
                    temp_Y--;
                    temp_顯示畫面列數索引++;
                }
                else break;
            }

            LADDER_Panel.操作方框索引.X = temp_X;
            LADDER_Panel.操作方框索引.Y = temp_Y;
            LADDER_Panel.顯示畫面列數索引 = temp_顯示畫面列數索引;

            現在位置.X++;

            cnt++;
        }
        void cnt_搜尋指定Device_200_顯示彈出視窗(ref byte cnt)
        {
            現在位置 = new Point();
            cnt++;
            MessageBox.Show(str_彈出視窗文字, " ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }
        void cnt_搜尋指定Device_(ref byte cnt)
        {

        }
        #endregion
        #region 替換指定Device
        void sub_替換指定Device()
        {
            exButton_Replace.Set_LoadState(cnt_替換指定Device != 255);
            if (cnt_替換指定Device == 1) cnt_替換指定Device_00_初始化(ref  cnt_替換指定Device);
            if (cnt_替換指定Device == 2) cnt_替換指定Device = 10;

            if (cnt_替換指定Device == 10) cnt_替換指定Device_10_檢查字串可替換(ref  cnt_替換指定Device);
            if (cnt_替換指定Device == 11) cnt_替換指定Device_10_檢查接點對應合法(ref  cnt_替換指定Device);
            if (cnt_替換指定Device == 12) cnt_替換指定Device_10_計算替換範圍(ref  cnt_替換指定Device);
            if (cnt_替換指定Device == 13) cnt_替換指定Device_10_檢查註解是否要搬移(ref  cnt_替換指定Device);
            if (cnt_替換指定Device == 14) cnt_替換指定Device_10_開始搬移(ref  cnt_替換指定Device);
            if (cnt_替換指定Device == 15) cnt_替換指定Device = 255;

            if (cnt_替換指定Device == 20) cnt_替換指定Device_20_等待搜尋_READY(ref  cnt_替換指定Device);
            if (cnt_替換指定Device == 21) cnt_替換指定Device_20_等待搜尋_OVER(ref  cnt_替換指定Device);
            if (cnt_替換指定Device == 22) cnt_替換指定Device_20_開始替換(ref  cnt_替換指定Device);     
            if (cnt_替換指定Device == 23) cnt_替換指定Device = 255;

            if (cnt_替換指定Device == 200) cnt_替換指定Device_200_顯示彈出視窗(ref  cnt_替換指定Device);
            if (cnt_替換指定Device == 201) cnt_替換指定Device = 255;
        }
        void cnt_替換指定Device_00_初始化(ref byte cnt)
        {
            FLAG_Messagebox已開啟 = false;
            str_Earlier_device = "";
            str_New_device = "";
            CallBackUI.textbox.取得字串(ref str_Earlier_device, textBox_Earlier_device);
            CallBackUI.textbox.取得字串(ref str_New_device, textBox_New_device);
            str_Earlier_device = str_Earlier_device.ToUpper();
            str_New_device = str_New_device.ToUpper();
            FLAG_替換有錯誤 = false;
            替換編號起始值 = 0;
            cnt++;
        }

        void cnt_替換指定Device_10_檢查字串可替換(ref byte cnt)
        {
            if (DEVICE.TestDevice(str_Earlier_device) && DEVICE.TestDevice(str_New_device))
            {
                cnt++;
            }
            else
            {
                str_彈出視窗文字 = "輸入字串不合法!";
                FLAG_替換有錯誤 = true;
                cnt = 200;
                return;
            }
        }
        void cnt_替換指定Device_10_檢查接點對應合法(ref byte cnt)
        {
            String str_接點類型_Earlier_device = str_Earlier_device.Substring(0, 1);
            String str_接點類型_New_device = str_New_device.Substring(0, 1);
            if (str_接點類型_Earlier_device == "X" || str_接點類型_Earlier_device == "Y" || str_接點類型_Earlier_device == "M" || str_接點類型_Earlier_device == "S" || str_接點類型_Earlier_device == "T")
            {
                if(str_接點類型_New_device == "D" || str_接點類型_New_device == "R" )
                {
                    str_彈出視窗文字 = "輸入字串及替換字串無法對應!";
                    FLAG_替換有錯誤 = true;
                    cnt = 200;
                    return;
                }
            }
            if (str_接點類型_Earlier_device == "D" || str_接點類型_Earlier_device == "R")
            {
                if (str_接點類型_New_device == "X" || str_接點類型_New_device == "Y" || str_接點類型_New_device == "M" || str_接點類型_New_device == "S" || str_接點類型_New_device == "T")
                {
                    str_彈出視窗文字 = "輸入字串及替換字串無法對應!";
                    FLAG_替換有錯誤 = true;
                    cnt = 200;
                    return;
                }
            }
            if (str_接點類型_Earlier_device == "T" || str_接點類型_New_device == "T")
            {
                if (str_接點類型_Earlier_device != "T" || str_接點類型_New_device != "T")
                {
                    str_彈出視窗文字 = "輸入字串及替換字串無法對應!";
                    FLAG_替換有錯誤 = true;
                    cnt = 200;
                    return;
                }
            }
            cnt++;
        }
        void cnt_替換指定Device_10_計算替換範圍(ref byte cnt)
        {
            String str_Earlier_device_接點編號 = str_Earlier_device.Substring(1, str_Earlier_device.Length - 1);
            int in_Earlier_device_接點編號 = 0;
            String str_New_device_接點編號 = str_New_device.Substring(1, str_New_device.Length - 1);
            int in_New_device_接點編號 = 0;
            if (!Int32.TryParse(str_Earlier_device_接點編號, out in_Earlier_device_接點編號))
            {
                str_彈出視窗文字 = "接點編號轉換失敗!";
                FLAG_替換有錯誤 = true;
                cnt = 200;
                return;
            }
            if (!Int32.TryParse(str_New_device_接點編號, out in_New_device_接點編號))
            {
                str_彈出視窗文字 = "接點編號轉換失敗!";
                FLAG_替換有錯誤 = true;
                cnt = 200;
                return;
            }
            替換編號起始值 = in_New_device_接點編號 ;
            cnt++;
        }
        void cnt_替換指定Device_10_檢查註解是否要搬移(ref byte cnt)
        {
            if (checkBox_move_cooments.Checked && !FLAG_註解已更換過)
            {

                if (!FLAG_Messagebox已開啟)
                {
                    FLAG_Messagebox已開啟 = true;
                    Result = MessageBox.Show("確認要搬移所有註解?", "Asterisk", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                }              
                if (Result == DialogResult.Yes)
                {
                    cnt++;
                }
                else if (Result == DialogResult.No)
                {
                    FLAG_註解已更換過 = true;
                    cnt = 255;
                }
                else if (Result == DialogResult.Cancel)
                {
                    cnt = 255;
                }
                Result = new DialogResult();
        
            }
            else
            {
                cnt = 20;
            }
        
        }
        void cnt_替換指定Device_10_開始搬移(ref byte cnt)
        {
            int temp_X = LADDER_Panel.操作方框索引.X;
            int temp_Y = LADDER_Panel.操作方框索引.Y;
            int temp_顯示畫面列數索引 = LADDER_Panel.顯示畫面列數索引;
            LADDER_Panel.LADDER ladder_temp = ladder_list_buf[temp_Y + temp_顯示畫面列數索引][temp_X];
            int 要搜尋的點數 = (int)nud_num_point.Value;
            List<String> List_str_原本的註解 = new List<string>();
            for (int k = 0; k < 要搜尋的點數; k++)
            {
                String str_原本接點 = "";
         
                String str_原本接點類型 = str_Earlier_device.Substring(0, 1);
    
                String str_原本接點編號 = str_Earlier_device.Substring(1, textBox_Earlier_device.Text.Length - 1);
                String str_原本接點進制 = "10";

                int 原本接點偏移 = k;
            
                if (str_原本接點類型 == "X" || str_原本接點類型 == "Y") str_原本接點進制 = "8";
                else str_原本接點進制 = "10";
                int int_原本接點編號 = 0;
                if (!Int32.TryParse(str_原本接點編號, out int_原本接點編號))
                {
                    str_彈出視窗文字 = "輸入字串不合法,查詢失敗!";
                    FLAG_替換有錯誤 = true;
                    cnt = 200;
                    return;
                }
                if (str_原本接點進制 == "8")
                {
                    int_原本接點編號 = myConvert.八進位轉十進位(int_原本接點編號);
                }
                int_原本接點編號 += 原本接點偏移;

                if (str_原本接點進制 == "8")
                {
                    int_原本接點編號 = myConvert.十進位轉八進位(int_原本接點編號);
                }        
                str_原本接點 = str_原本接點類型 + int_原本接點編號.ToString();
 
                if (!DEVICE.TestDevice(str_原本接點) )
                {
                    str_彈出視窗文字 = "輸入字串不合法,查詢失敗!";
                    FLAG_替換有錯誤 = true;
                    cnt = 200;
                    return;
                }
                object comment = new object();
                device.Get_Device(str_原本接點, 0, out comment);
                List_str_原本的註解.Add((string)comment);
                device.Set_Device(str_原本接點, "");
               
            }
            for (int k = 0; k < 要搜尋的點數; k++)
            {
                String str_替換接點 = "";
                String str_替換接點類型 = str_New_device.Substring(0, 1);
                String str_替換接點進制 = "10";
                int 替換接點偏移 = k;
                if (str_替換接點類型 == "X" || str_替換接點類型 == "Y") str_替換接點進制 = "8";
                else str_替換接點進制 = "10";

                int int_替換接點編號 = 替換編號起始值;
                if (str_替換接點進制 == "8")
                {
                    int_替換接點編號 = myConvert.八進位轉十進位(int_替換接點編號);
                }

                int_替換接點編號 += 替換接點偏移;
                if (str_替換接點進制 == "8")
                {
                    int_替換接點編號 = myConvert.十進位轉八進位(int_替換接點編號);
                }
                str_替換接點 = str_替換接點類型 + int_替換接點編號.ToString();

                if (!DEVICE.TestDevice(str_替換接點))
                {
                    str_彈出視窗文字 = "輸入字串不合法,查詢失敗!";
                    FLAG_替換有錯誤 = true;
                    cnt = 200;
                    return;
                }
                device.Set_Device(str_替換接點, List_str_原本的註解[k]);           
            }
            FLAG_註解已更換過 = true;
            cnt++;
        }

        void cnt_替換指定Device_20_等待搜尋_READY(ref byte cnt)
        {
            if (cnt_搜尋指定Device == 255)
            {
                cnt_搜尋指定Device = 1;
                cnt++;
            }

        }
        void cnt_替換指定Device_20_等待搜尋_OVER(ref byte cnt)
        {
            if (cnt_搜尋指定Device == 255)
            {
                if (!FLAG_已到搜索終點) cnt++;
                else cnt = 255;
            }
        }
        void cnt_替換指定Device_20_開始替換(ref byte cnt)
        {
            int temp_X = LADDER_Panel.操作方框索引.X;
            int temp_Y = LADDER_Panel.操作方框索引.Y;
            int temp_顯示畫面列數索引 = LADDER_Panel.顯示畫面列數索引;
            int 要搜尋的點數 = (int)nud_num_point.Value;
            LADDER_Panel.LADDER ladder_temp = ladder_list_buf[temp_Y + temp_顯示畫面列數索引][temp_X];
            String str_接點類型_New_device = str_New_device.Substring(0, 1);
            for (int i = 0; i < ladder_temp.元素數量; i++)
            {
                for (int k = 0; k < 要搜尋的點數; k++)
                {
                    String str_原本接點 = "";
                    String str_替換接點 = "";
                    String str_原本接點類型 = str_Earlier_device.Substring(0, 1);
                    String str_替換接點類型 = str_New_device.Substring(0, 1);
                    String str_原本接點編號 = str_Earlier_device.Substring(1, textBox_Earlier_device.Text.Length - 1);
                    String str_原本接點進制 = "10";
                    String str_替換接點進制 = "10";
                    int 原本接點偏移 = k;
                    int 替換接點偏移 = k ;
                    if (str_原本接點類型 == "X" || str_原本接點類型 == "Y") str_原本接點進制 = "8";
                    else str_原本接點進制 = "10";
                    if (str_替換接點類型 == "X" || str_替換接點類型 == "Y") str_替換接點進制 = "8";
                    else str_替換接點進制 = "10";


                    int int_原本接點編號 = 0;
                    if (!Int32.TryParse(str_原本接點編號, out int_原本接點編號))
                    {
                        str_彈出視窗文字 = "輸入字串不合法,查詢失敗!";
                        FLAG_替換有錯誤 = true;
                        cnt = 200;
                        return;
                    }

                    int int_替換接點編號 = 替換編號起始值;
                    if (str_原本接點進制 == "8")
                    {
                        int_原本接點編號 = myConvert.八進位轉十進位(int_原本接點編號);
                    }
                    if (str_替換接點進制 == "8")
                    {
                        int_替換接點編號 = myConvert.八進位轉十進位(int_替換接點編號);
                    }

          
                    int_原本接點編號 += 原本接點偏移;
                    int_替換接點編號 += 替換接點偏移;


                    if (str_原本接點進制 == "8")
                    {
                        int_原本接點編號 = myConvert.十進位轉八進位(int_原本接點編號);
                    }
                    if (str_替換接點進制 == "8")
                    {
                        int_替換接點編號 = myConvert.十進位轉八進位(int_替換接點編號);
                    }

                    str_原本接點 = str_原本接點類型 + int_原本接點編號.ToString();
                    str_替換接點 = str_替換接點類型 + int_替換接點編號.ToString();
                    if (!DEVICE.TestDevice(str_原本接點) || !DEVICE.TestDevice(str_替換接點))
                    {
                        str_彈出視窗文字 = "輸入字串不合法,查詢失敗!";
                        FLAG_替換有錯誤 = true;
                        cnt = 200;
                        return;
                    }

                    if (ladder_temp.ladderParam[i] != null)
                    {
                        if (ladder_temp.ladderParam[i] == str_原本接點)
                        {
                            if (ladder_temp.ladderType == LADDER_Panel.partTypeEnum.OUT_Part)
                            {
                                if (str_替換接點類型 == "Y" || str_替換接點類型 == "M" || str_替換接點類型 == "S" || str_替換接點類型 == "T")
                                {
                                    ladder_list_buf[temp_Y + temp_顯示畫面列數索引][temp_X].ladderParam[i] = str_替換接點;
                                    ladder_list_buf[temp_Y + temp_顯示畫面列數索引][temp_X].未編譯 = true;
                                    LADDER_Panel.PasteTo_Ladderlist(ladder_list_buf[temp_Y + temp_顯示畫面列數索引][temp_X], temp_Y + temp_顯示畫面列數索引, temp_X);
                                    break;
                                }
                            }
                            else
                            {
                                ladder_list_buf[temp_Y + temp_顯示畫面列數索引][temp_X].ladderParam[i] = str_替換接點;
                                ladder_list_buf[temp_Y + temp_顯示畫面列數索引][temp_X].未編譯 = true;
                                LADDER_Panel.PasteTo_Ladderlist(ladder_list_buf[temp_Y + temp_顯示畫面列數索引][temp_X], temp_Y + temp_顯示畫面列數索引, temp_X);
                                break;
                            }
                        }
                    }
                }
            }
            cnt++;
        }

        void cnt_替換指定Device_200_顯示彈出視窗(ref byte cnt)
        {
            現在位置 = new Point();
            cnt++;
            MessageBox.Show(str_彈出視窗文字, " ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }
        void cnt_替換指定Device_(ref byte cnt)
        {

            cnt++;
        }
        #endregion
        #region 替換所有Device
        void sub_替換所有Device()
        {
            if (cnt_替換所有Device == 1) cnt_替換所有Device_00_初始化(ref cnt_替換所有Device);
            if (cnt_替換所有Device == 2) cnt_替換所有Device_00_提示是否更換(ref cnt_替換所有Device);
            if (cnt_替換所有Device == 3) cnt_替換所有Device = 10;

            if (cnt_替換所有Device == 10) cnt_替換所有Device_10_檢查是否到終點(ref cnt_替換所有Device);
            if (cnt_替換所有Device == 11) cnt_替換所有Device_10_等待替換指定Device_READY(ref cnt_替換所有Device);
            if (cnt_替換所有Device == 12) cnt_替換所有Device_10_等待替換指定Device_OVER(ref cnt_替換所有Device);
            if (cnt_替換所有Device == 13) cnt_替換所有Device = 10;
        }
        void cnt_替換所有Device_00_初始化(ref byte cnt)
        {
            FLAG_Messagebox已開啟 = false;
            FLAG_已到搜索終點 = false;
            FLAG_搜索有錯誤 = false;
            FLAG_替換有錯誤 = false;
            cnt++;
        }
        void cnt_替換所有Device_00_提示是否更換(ref byte cnt)
        {
            if (!FLAG_Messagebox已開啟)
            {
                FLAG_Messagebox已開啟 = true;
                Result = MessageBox.Show("是否要替換所有接點?", "Warring", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            if (Result == DialogResult.Yes)
            {
         
                cnt++;
            }
            else if (Result == DialogResult.No)
            {
                cnt = 255;
            }
            Result = new System.Windows.Forms.DialogResult();
        }
        void cnt_替換所有Device_10_檢查是否到終點(ref byte cnt)
        {
            if (FLAG_已到搜索終點 || FLAG_搜索有錯誤 || FLAG_替換有錯誤)
            {
                cnt = 255;
            }
            else
            {
                cnt++;
            }
           
        }
        void cnt_替換所有Device_10_等待替換指定Device_READY(ref byte cnt)
        {
            if (cnt_替換指定Device == 255)
            {
                cnt_替換指定Device = 1;
                cnt++;
            }
        }
        void cnt_替換所有Device_10_等待替換指定Device_OVER(ref byte cnt)
        {

            if (cnt_替換指定Device == 255)
            {
                cnt++;
            }
        }
        void cnt_替換所有Device_(ref byte cnt)
        {

            cnt++;
        }
        #endregion

        private void exButton_FindNext_btnClick(object sender, EventArgs e)
        {
            if (cnt_搜尋指定Device == 255)
            {
                if (radioButton_from_top_to_buttom.Checked)
                {
                    if (!FLAG_初始位置重新設置)
                    {
                        現在位置.Y = 0;
                        搜尋終點 = ladder_list_buf.Count;
                        FLAG_初始位置重新設置 = true;
                    }

                }
                else if (radioButton_from_cursor_to_buttom.Checked)
                {
                    if (!FLAG_初始位置重新設置)
                    {
                        現在位置.Y = 起始鼠標列數;
                        搜尋終點 = ladder_list_buf.Count;
                        FLAG_初始位置重新設置 = true;
                    }
                }
                else if (radioButton_specified_range.Checked)
                {
                    if (!FLAG_初始位置重新設置)
                    {
                        現在位置.Y = 搜索範圍下限_程式列數;
                        搜尋終點 = 搜索範圍上限_程式列數 + 1;
                        FLAG_初始位置重新設置 = true;
                    }
                }     
                cnt_搜尋指定Device = 1;
            }
        }
        private void exButton_Close_btnClick(object sender, EventArgs e)
        {
            replaceDevice.Close();
        }
        private void exButton_Replace_btnClick(object sender, EventArgs e)
        {
            if (cnt_替換指定Device == 255)
            {
                if (radioButton_from_top_to_buttom.Checked)
                {
                    if (!FLAG_初始位置重新設置)
                    {
                        現在位置.Y = 0;
                        搜尋終點 = ladder_list_buf.Count;
                        FLAG_初始位置重新設置 = true;
                    }

                }
                else if (radioButton_from_cursor_to_buttom.Checked)
                {
                    if (!FLAG_初始位置重新設置)
                    {
                        現在位置.Y = 起始鼠標列數;
                        搜尋終點 = ladder_list_buf.Count;
                        FLAG_初始位置重新設置 = true;
                    }
                }
                else if (radioButton_specified_range.Checked)
                {
                    if (!FLAG_初始位置重新設置)
                    {
                        現在位置.Y = 搜索範圍下限_程式列數;
                        搜尋終點 = 搜索範圍上限_程式列數 + 1;
                        FLAG_初始位置重新設置 = true;
                    }
                }  
                cnt_替換指定Device = 1;
            }
        }
        private void exButton_Replace_all_btnClick(object sender, EventArgs e)
        {
            if (cnt_替換所有Device == 255)
            {
                if (radioButton_from_top_to_buttom.Checked)
                {
                    if (!FLAG_初始位置重新設置)
                    {
                        現在位置.Y = 0;
                        搜尋終點 = ladder_list_buf.Count;
                        FLAG_初始位置重新設置 = true;
                    }

                }
                else if (radioButton_from_cursor_to_buttom.Checked)
                {
                    if (!FLAG_初始位置重新設置)
                    {
                        現在位置.Y = 起始鼠標列數;
                        搜尋終點 = ladder_list_buf.Count;
                        FLAG_初始位置重新設置 = true;
                    }
                }
                else if (radioButton_specified_range.Checked)
                {
                    if (!FLAG_初始位置重新設置)
                    {
                        現在位置.Y = 搜索範圍下限_程式列數;
                        搜尋終點 = 搜索範圍上限_程式列數 + 1;
                        FLAG_初始位置重新設置 = true;
                    }
                }  
                cnt_替換所有Device = 1;
            }
        }
        private void radioButton_from_top_to_buttom_CheckedChanged(object sender, EventArgs e)
        {
            nud_specified_range_upper.Enabled = false;
            nud_specified_range_lower.Enabled = false;
            FLAG_初始位置重新設置 = false;
        }
        private void radioButton_from_cursor_to_buttom_CheckedChanged(object sender, EventArgs e)
        {
            nud_specified_range_upper.Enabled = false;
            nud_specified_range_lower.Enabled = false;
            FLAG_初始位置重新設置 = false;
        }
        private void radioButton_specified_range_CheckedChanged(object sender, EventArgs e)
        {
            nud_specified_range_upper.Enabled = true;
            nud_specified_range_lower.Enabled = true;
            FLAG_初始位置重新設置 = false;
        }
        private void textBox_Earlier_device_TextChanged(object sender, EventArgs e)
        {
            FLAG_註解已更換過 = false;
            FLAG_初始位置重新設置 = false;
        }
        private void textBox_New_device_TextChanged(object sender, EventArgs e)
        {
            FLAG_註解已更換過 = false;
            FLAG_初始位置重新設置 = false;
        }










    }
}
