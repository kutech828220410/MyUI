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
using DrawingClass;
using System.Reflection;
using System.Diagnostics;
using LadderProperty;
using LadderForm;
using LadderConnection;
using Basic;

namespace LadderUI
{
    public partial class LADDER_Panel : UserControl
    {
        Basic.MyThread MyThread_Online讀取;
        private void backgroundWorker_LADDER_主程式_DoWork(object sender, DoWorkEventArgs e)
        {
            EnterSymbol enterSymbol = new EnterSymbol();
            while (true)
            {
                SystemTimer();

                FLAG_有功能鍵按下 = false;
                if (exButton_窗選模式切換.Load_WriteState()) 窗選模式 = Tx_窗選模式.鼠線模式;
                else 窗選模式 = Tx_窗選模式.複製模式;

                if (!FLAG_有程式未編譯)
                {
                    未編譯範圍最大值 = 0;
                    未編譯範圍最小值 = 99999999;
                }

                sub_初始化(ref FLAG_初始化);
                sub_Shif_上下左右組合鍵();

                if (!FLAG_編譯視窗獲取焦點)
                {
                    device_system.Set_Device("T9", 100);
                    T9_編譯視窗獲取焦點 = false;
                    device_system.Set_Device("T10", 500);
                    T10_編譯視窗ESC按鈕致能 = false;
                }

                if (T9_編譯視窗獲取焦點)
                {
                    sub_鍵盤按鈕檢查();
                    sub_操作方框移動();
                    sub_檢查操作框位置是否超出範圍();
                }
                sub_ExButton程式();
                sub_檢查輸入指令();
 
                sub_註解列表更新至Device();
                sub_Download_程式反編譯及註解寫入();


                Thread.Sleep(10);
            }
        }
        private void backgroundWorker_畫面更新_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if(cnt_程式編譯 == 255)sub_畫面更新();
                sub_程式編譯();
                sub_未編譯檢查();
                sub_備份已編譯程式();
                Thread.Sleep(10);              
            }
        }
        private void backgroundWorker_計時器_DoWork(object sender, DoWorkEventArgs e)
        {

           /* while (true)
            {
                object T0_編譯畫面更新 = new object();
                object T1_操作方框移動_向上 = new object();
                object T2_操作方框移動_向下 = new object();
                object T3_操作方框移動_向左 = new object();
                object T4_操作方框移動_向右 = new object();
                object T5_拖曳操作框上下捲動間隔 = new object();
                object T6_未編譯檢查 = new object();
                object T7_編譯視窗未獲取焦點 = new object();
                object T8_指令輸入視窗致能 = new object();
                object T9_編譯視窗獲取焦點 = new object();
                object T10_編譯視窗ESC按鈕致能 = new object();

                device_system.Set_Device("T0", true);
                device_system.Set_Device("T1", true);
                device_system.Set_Device("T2", true);
                device_system.Set_Device("T3", true);
                device_system.Set_Device("T4", true);
                device_system.Set_Device("T5", true);
                device_system.Set_Device("T6", true);
                device_system.Set_Device("T7", true);
                device_system.Set_Device("T8", true);
                device_system.Set_Device("T9", true);
                device_system.Set_Device("T10", true);

                device_system.Get_Device("T0", out T0_編譯畫面更新);
                device_system.Get_Device("T1", out T1_操作方框移動_向上);
                device_system.Get_Device("T2", out T2_操作方框移動_向下);
                device_system.Get_Device("T3", out T3_操作方框移動_向左);
                device_system.Get_Device("T4", out T4_操作方框移動_向右);
                device_system.Get_Device("T5", out T5_拖曳操作框上下捲動間隔);
                device_system.Get_Device("T6", out T6_未編譯檢查);
                device_system.Get_Device("T7", out T7_編譯視窗未獲取焦點);
                device_system.Get_Device("T8", out T8_指令輸入視窗致能);
                device_system.Get_Device("T9", out T9_編譯視窗獲取焦點);
                device_system.Get_Device("T10", out T10_編譯視窗ESC按鈕致能);

                this.T0_編譯畫面更新 = (bool)T0_編譯畫面更新;
                this.T1_操作方框移動_向上 = (bool)T1_操作方框移動_向上;
                this.T2_操作方框移動_向下 = (bool)T2_操作方框移動_向下;
                this.T3_操作方框移動_向左 = (bool)T3_操作方框移動_向左;
                this.T4_操作方框移動_向右 = (bool)T4_操作方框移動_向右;
                this.T5_拖曳操作框上下捲動間隔 = (bool)T5_拖曳操作框上下捲動間隔;
                this.T6_未編譯檢查 = (bool)T6_未編譯檢查;
                this.T7_編譯視窗未獲取焦點 = (bool)T7_編譯視窗未獲取焦點;
                this.T8_指令輸入視窗致能 = (bool)T8_指令輸入視窗致能;
                this.T9_編譯視窗獲取焦點 = (bool)T9_編譯視窗獲取焦點;
                this.T10_編譯視窗ESC按鈕致能 = (bool)T10_編譯視窗ESC按鈕致能;

                System.Threading.Thread.Sleep(10);
            }*/
        }
        public LADDER_Panel()
        {
            InitializeComponent();

            this.MyThread_Online讀取 = new MyThread();
            this.MyThread_Online讀取.AutoRun(true);
            this.MyThread_Online讀取.Add_Method(this.sub_Online讀取);
            this.MyThread_Online讀取.SetSleepTime(0);
            this.MyThread_Online讀取.Trigger();

            LoadSystemProperties();
           

            this.pictureBox_LADDER.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox_LADDER_MouseWheel);
            this.panel_Listbox_IL指令集.Dock = DockStyle.None;
            this.panel_Listbox_IL指令集.Location = new Point(0, 0);
            this.panel_Listbox_IL指令集.Size = new System.Drawing.Size(panel_LADDER.Size.Width, panel_LADDER.Size.Height);

            操作方框大小.Width = (int)(panel_LADDER.Width / (float)一列格數);
            操作方框大小.Height = (int)(panel_LADDER.Height / (float)一個畫面列數);

            操作方框索引 = new Point(0, 0);
            CallBackUI.peremeter.設定跨執行序存取UI(true);
            CallBackUI.peremeter.MakeDoubleBuffered(dataGridView_註解列表, true);
            Basic.Reflection.MakeDoubleBuffered(pictureBox_LADDER, true);
            CallBackUI.peremeter.MakeDoubleBuffered(vScrollBar_picture_滾動條, true);
            mainThreadId = Thread.CurrentThread.ManagedThreadId;
            TopMachine.init(this.serialPort);

            List_ExButton.Add(exButton_Online);
            List_ExButton.Add(exButton_上一步);
            List_ExButton.Add(exButton_刪除豎線);
            List_ExButton.Add(exButton_刪除橫線);
            List_ExButton.Add(exButton_剪下);
            List_ExButton.Add(exButton_復原回未修正);
            List_ExButton.Add(exButton_畫豎線);
            List_ExButton.Add(exButton_畫橫線);
            List_ExButton.Add(exButton_程式_註解模式選擇);
            List_ExButton.Add(exButton_窗選模式切換);
            List_ExButton.Add(exButton_註解查詢);
            List_ExButton.Add(exButton_貼上);
            List_ExButton.Add(exButton_開新專案);
            List_ExButton.Add(exButton_語法切換);
            List_ExButton.Add(exButton_寫入A接點);
            List_ExButton.Add(exButton_寫入B接點);
            List_ExButton.Add(exButton_複製);
            List_ExButton.Add(exButton_儲存檔案);
            List_ExButton.Add(exButton_讀取檔案);
            List_ExButton.Add(exButton15);

 
        }
        #region LADDER
        #region 系統參數
        private Form Form_作用中表單;
        private string Form_作用中表單_Text;
        private static String Str_儲存位置 = ".\\Setting.stp";
        private static String Str_讀取位置 = ".\\Setting.stp";

        private DEVICE device_system = new DEVICE(0, 0, 0, 0, 20, 0, 0,true);
        private Keyboard Keys = new Keyboard();
        private MyConvert myConvert = new MyConvert();

        private int 程式行數 = 99999;
        private bool FLAG_有程式未編譯_buf = true;
        private int 未編譯範圍最大值 = 0;
        private int 未編譯範圍最小值 = 99999999;
        private int 編譯區塊首列位置 = 0;
        private int vScrollBar_picture_滾動條_value_buf = 99999;
        private bool FLAG_編譯視窗獲取焦點 = true;
        private bool FLAG_主視窗獲取焦點 = true;
        private bool FLAG_專案已儲存 = true;
        private static Bitmap bitmap_temp;
        private bool FLAG_有功能鍵按下 = false;
        private bool FLAG_複製到剪貼簿 = false;
        private bool FLAG_取得剪貼簿 = false;
        private int mainThreadId;
        private List<MyUI.ExButton> List_ExButton = new List<MyUI.ExButton>();
        private List<string[]> TAB目錄 = new List<string[]>();//索引 ,程式行數 ,內容
        #region LADDER屬性
        [Serializable]
        public struct LADDER  //梯形图数据结构
        {
            public LADDER(int i)
            {
                _元素數量 = i;
                Vline_左上 = false;
                Vline_左下 = false;
                Vline_右上 = false;
                Vline_右下 = false;
                未編譯 = false;
                comment = new string[2];
                ladderParam = new string[2];
                _ladderType = partTypeEnum.noPart;
            }
            private int _元素數量;
            public int 元素數量
            {
                get
                {
                    return _元素數量; 
                }
                set
                {
                    _元素數量 = value;
                    ladderParam = new string[元素數量 + 1];
                    for (int i = 0; i < 元素數量 + 1; i++)
                    {
                        ladderParam[i] = "";
                    }
                }
            }
            private partTypeEnum _ladderType;
            public partTypeEnum ladderType //梯型图类型
            {
                get
                {
                    return _ladderType;
                }
                set
                {
                    _ladderType = value;
                    if (_ladderType == partTypeEnum.leftParenthesis)
                    {
                        Vline_右上 = true;
                        Vline_右下 = true;
                        元素數量 = 1;
                    }
                    else if (_ladderType == partTypeEnum.rightParenthesis)
                    {
                        Vline_左上 = true;
                        Vline_左下 = true;
                        元素數量 = 1;
                    }
                    else if(_ladderType == partTypeEnum.LD_Equal_Part)
                    {
                        元素數量 = 3;
                    }
                    else if (_ladderType == partTypeEnum.MOV_Part)
                    {
                        元素數量 = 3;
                    }
                    else if (_ladderType == partTypeEnum.ADD_PART)
                    {
                        元素數量 = 4;
                    }
                    else if (_ladderType == partTypeEnum.SUB_PART)
                    {
                        元素數量 = 4;
                    }
                    else if (_ladderType == partTypeEnum.MUL_PART)
                    {
                        元素數量 = 4;
                    }
                    else if (_ladderType == partTypeEnum.DIV_PART)
                    {
                        元素數量 = 4;
                    }
                    else if (_ladderType == partTypeEnum.INC_PART)
                    {
                        元素數量 = 2;
                    }
                    else if (_ladderType == partTypeEnum.DRVA_PART)
                    {
                        元素數量 = 4;
                    }
                    else if (_ladderType == partTypeEnum.DRVI_PART)
                    {
                        元素數量 = 4;
                    }
                    else if (_ladderType == partTypeEnum.PLSV_PART)
                    {
                        元素數量 = 3;
                    }
                    else if (_ladderType == partTypeEnum.OUT_TIMER_PART)
                    {
                        元素數量 = 2;
                    }
                    else if (_ladderType == partTypeEnum.SET_PART)
                    {
                        元素數量 = 2;
                    }
                    else if (_ladderType == partTypeEnum.RST_PART)
                    {
                        元素數量 = 2;
                    }
                    else if (_ladderType == partTypeEnum.ZRST_PART)
                    {
                        元素數量 = 3;
                    }
                    else if (_ladderType == partTypeEnum.BMOV_PART)
                    {
                        元素數量 = 4;
                    }
                    else if (_ladderType == partTypeEnum.BTW_PART)
                    {
                        元素數量 = 4;
                    }
                    else if (_ladderType == partTypeEnum.WTB_PART)
                    {
                        元素數量 = 4;
                    }
                    else if (_ladderType == partTypeEnum.TAB_PART)
                    {
                        元素數量 = 10;
                    }
                    else if (_ladderType == partTypeEnum.JUMP_PART)
                    {
                        元素數量 = 2;
                    }
                    else if (_ladderType == partTypeEnum.REF_PART)
                    {
                        元素數量 = 3;
                    }
                    else
                    {
                        元素數量 = 1;
                    }
                }
            }
            public string[] ladderParam;      //指令参数
            public bool Vline_左上, Vline_左下, Vline_右上, Vline_右下;        //有竖线 (所有元件都允许有竖线)       
            public string[] comment;
            public bool 未編譯;
        };
        public enum partTypeEnum
        {
            noPart = 0, Data_no_Part, A_Part, B_Part, OUT_Part, H_Line_Part, leftParenthesis, rightParenthesis,
            EndPart, LD_Equal_Part, MOV_Part, ADD_PART, SUB_PART, MUL_PART, DIV_PART, INC_PART, DRVA_PART, DRVI_PART,
            PLSV_PART, OUT_TIMER_PART, SET_PART, RST_PART, ZRST_PART, BMOV_PART, WTB_PART, BTW_PART,TAB_PART,JUMP_PART,REF_PART
        }
        private static List<LADDER[]> ladder_list = new List<LADDER[]>();
        private List<LADDER[]> ladder_list_備份 = new List<LADDER[]>();
        private List<LADDER[]> ladder_list_上一步 = new List<LADDER[]>();
        private List<LADDER[]> ladder_list_buf = new List<LADDER[]>();
        private List<LADDER[]> ladder_list_Copy = new List<LADDER[]>();
        private List<LADDER[]> ladder_list_編譯區塊 = new List<LADDER[]>();
        #endregion
        #region IL屬性
        public class IL節點
        {
            public int 母線首行位置;
            public int 母線末行位置;
            public int 母線列位置;
            public bool BLOCK已完成 = false;
        }

        #endregion
        #endregion
        #region 動態參數
        public void Run(Form form)
        {
            Form_作用中表單 = form;
            Form_作用中表單_Text = Form_作用中表單.Text;
            Form_作用中表單.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
            TransferSetup.GetForm().topMachine_Panel.udP_Cilent.Run(form);
            TransferSetup.GetForm().topMachine_Panel.udP_Cilent.Name = "TT";
            TopMachine.UDP_Cilent = TransferSetup.GetForm().topMachine_Panel.udP_Cilent;
            timer_主執行序監控.Enabled = true;
        }
        public void Stop()
        {
            timer_主執行序監控.Enabled = false;
        }
        static public Point 操作方框索引 = new Point();
        static public Size 操作方框大小 = new Size();
        static public bool FLAG_初始化 = false;
        static public bool FLAG_Online = false;
        static public bool FLAG_有程式未編譯 = false;
        static public int 顯示畫面列數索引 = 0;

        private int 一列格數 = 12;
        private int 一個畫面列數 = 8;
        private int 接點偏移位置_Y = 40;
        private int 註解一列半形字母數 = 8;
        private int 註解列數 = 2;
        private int 註解偏移位置_X = 5;
        private int 註解偏移位置_Y = 10;
        private int 接點寬度 = 15;
        private int 接點高度 = 20;
        private bool 註解不顯示 = false;
        private Font 註解字體 = new Font("微軟正黑體", 14);
        private Color 註解文字顏色 = Color.Red;
        private Color Online顏色 = Color.FromArgb(255, 0, 80, 255);
        private Font 視窗字體 = new Font("Courier New", 14);
        private Font Online字體 = new Font("標楷體", 12);

        static public List<String[]> IL指令程式 = new List<string[]>();
        static public DEVICE device = new DEVICE(false);
        #endregion
        #region System_Timer
        private bool T0_編譯畫面更新 = false;
        private bool T1_操作方框移動_向上 = false;
        private bool T2_操作方框移動_向下 = false;
        private bool T3_操作方框移動_向左 = false;
        private bool T4_操作方框移動_向右 = false;
        private bool T5_拖曳操作框上下捲動間隔 = false;
        private bool T6_未編譯檢查 = false;
        private bool T7_編譯視窗未獲取焦點 = false;
        private bool T8_指令輸入視窗致能 = false;
        private bool T9_編譯視窗獲取焦點 = false;
        private bool T10_編譯視窗ESC按鈕致能 = false;
 
        private void SystemTimer()
        {
            object T0_編譯畫面更新 = new object();
            object T1_操作方框移動_向上 = new object();
            object T2_操作方框移動_向下 = new object();
            object T3_操作方框移動_向左 = new object();
            object T4_操作方框移動_向右 = new object();
            object T5_拖曳操作框上下捲動間隔 = new object();
            object T6_未編譯檢查 = new object();
            object T7_編譯視窗未獲取焦點 = new object();
            object T8_指令輸入視窗致能 = new object();
            object T9_編譯視窗獲取焦點 = new object();
            object T10_編譯視窗ESC按鈕致能 = new object();

            device_system.Set_Device("T0", true);
            device_system.Set_Device("T1", true);
            device_system.Set_Device("T2", true);
            device_system.Set_Device("T3", true);
            device_system.Set_Device("T4", true);
            device_system.Set_Device("T5", true);
            device_system.Set_Device("T6", true);
            device_system.Set_Device("T7", true);
            device_system.Set_Device("T8", true);
            device_system.Set_Device("T9", true);
            device_system.Set_Device("T10", true);

            device_system.Get_Device("T0", out T0_編譯畫面更新);
            device_system.Get_Device("T1", out T1_操作方框移動_向上);
            device_system.Get_Device("T2", out T2_操作方框移動_向下);
            device_system.Get_Device("T3", out T3_操作方框移動_向左);
            device_system.Get_Device("T4", out T4_操作方框移動_向右);
            device_system.Get_Device("T5", out T5_拖曳操作框上下捲動間隔);
            device_system.Get_Device("T6", out T6_未編譯檢查);
            device_system.Get_Device("T7", out T7_編譯視窗未獲取焦點);
            device_system.Get_Device("T8", out T8_指令輸入視窗致能);
            device_system.Get_Device("T9", out T9_編譯視窗獲取焦點);
            device_system.Get_Device("T10", out T10_編譯視窗ESC按鈕致能);

            this.T0_編譯畫面更新 = (bool)T0_編譯畫面更新;
            this.T1_操作方框移動_向上 = (bool)T1_操作方框移動_向上;
            this.T2_操作方框移動_向下 = (bool)T2_操作方框移動_向下;
            this.T3_操作方框移動_向左 = (bool)T3_操作方框移動_向左;
            this.T4_操作方框移動_向右 = (bool)T4_操作方框移動_向右;
            this.T5_拖曳操作框上下捲動間隔 = (bool)T5_拖曳操作框上下捲動間隔;
            this.T6_未編譯檢查 = (bool)T6_未編譯檢查;
            this.T7_編譯視窗未獲取焦點 = (bool)T7_編譯視窗未獲取焦點;
            this.T8_指令輸入視窗致能 = (bool)T8_指令輸入視窗致能;
            this.T9_編譯視窗獲取焦點 = (bool)T9_編譯視窗獲取焦點;
            this.T10_編譯視窗ESC按鈕致能 = (bool)T10_編譯視窗ESC按鈕致能;
        }
        #endregion
        #region Online讀取
   
        byte cnt_Online讀取 = 255;
        void sub_Online讀取()
        {     
            if (cnt_Online讀取 == 255) cnt_Online讀取 = 1;
            if (Upload.視窗已建立) cnt_Online讀取 = 255;
            if (Download.視窗已建立) cnt_Online讀取 = 255;
            if (Verify.視窗已建立) cnt_Online讀取 = 255;
            if (!FLAG_Online) cnt_Online讀取 = 255;
            if (cnt_鍵盤按鈕檢查_Shift_Enter != 1) cnt_Online讀取 = 255;      
            if (cnt_Online讀取 == 1) cnt_Online讀取_00_檢查是否在Online(ref cnt_Online讀取);
            if (cnt_Online讀取 == 2) cnt_Online讀取 = 10;

            if (cnt_Online讀取 == 10) cnt_Online讀取_10_檢查XY是否到達最大值(ref cnt_Online讀取);
            if (cnt_Online讀取 == 11) cnt_Online讀取_10_檢查可讀取(ref cnt_Online讀取);
            if (cnt_Online讀取 == 12) cnt_Online讀取_10_檢查是否要讀取(ref cnt_Online讀取);
            if (cnt_Online讀取 == 13) cnt_Online讀取 = 20;

            if (cnt_Online讀取 == 20) cnt_Online讀取_20_檢查元素最大值(ref cnt_Online讀取);
            if (cnt_Online讀取 == 21) cnt_Online讀取_20_檢查Z索引_分離字串(ref cnt_Online讀取);
            if (cnt_Online讀取 == 22) cnt_Online讀取_20_讀取Z值(ref cnt_Online讀取);
            if (cnt_Online讀取 == 23) cnt_Online讀取_20_設定Str_Device(ref cnt_Online讀取);
            if (cnt_Online讀取 == 24) cnt_Online讀取_20_檢查DeviceType(ref cnt_Online讀取);

            if (cnt_Online讀取 == 25) cnt_Online讀取_25_一般元件讀取(ref cnt_Online讀取);

            if (cnt_Online讀取 == 30) cnt_Online讀取_30_特殊元件讀取_LOW(ref cnt_Online讀取);
            if (cnt_Online讀取 == 31) cnt_Online讀取_30_特殊元件讀取_HIGH(ref cnt_Online讀取);

            if (cnt_Online讀取 == 35) cnt_Online讀取_35_Timer_bool讀取(ref cnt_Online讀取);
            if (cnt_Online讀取 == 36) cnt_Online讀取_35_Timer_Value讀取(ref cnt_Online讀取);

            if (cnt_Online讀取 == 150) cnt_Online讀取_150_元件讀取完成(ref cnt_Online讀取);

            if (cnt_Online讀取 == 250) cnt_Online讀取_250_畫面讀取完成(ref cnt_Online讀取);
            if (cnt_Online讀取 == 251) cnt_Online讀取 = 255;

            if (!this.checkBox_Online_High_Speed.Checked && Online讀取_讀取完成) Thread.Sleep(10);
        }

        int Online讀取_Xtemp = 0;
        int Online讀取_Ytemp = 0;
        LADDER Online讀取_ladder_temp = new LADDER();
        int Online讀取_元素現在值 = 0;
        bool Online讀取_要讀取 = false;
        string[] Online讀取_分離字串;
        String Online讀取_DeviceType = "";
        String Online讀取_Device = "";
        bool Online讀取_讀取完成 = false;
        void cnt_Online讀取_00_檢查是否在Online(ref byte cnt)
        {
            if (FLAG_Online ) cnt++;
            else cnt = 255;
        }
        void cnt_Online讀取_10_檢查XY是否到達最大值(ref byte cnt)
        {
            this.Online讀取_讀取完成 = false;
            Online讀取_要讀取 = false;
            bool X_已到達 = false;
            bool Y_已到達 = false;
            if (Online讀取_Ytemp >= 一個畫面列數) Y_已到達 = true;
            if (Online讀取_Xtemp >= 一列格數) X_已到達 = true;
            if (X_已到達 && !Y_已到達)
            {
                Online讀取_Xtemp = 0;
                Online讀取_Ytemp++;
                return;
            }
            else if (Y_已到達 && !X_已到達)
            {
                cnt++;
                return;
            }
            else if (!Y_已到達 && !X_已到達)
            {
                cnt++;
                return;
            }
            else if (Y_已到達 && X_已到達)
            {
                cnt = 250;
                return;
            }
      
        }
        void cnt_Online讀取_10_檢查可讀取(ref byte cnt)
        {
            if (Online讀取_Ytemp + 顯示畫面列數索引 < ladder_list.Count)
            {
                cnt++;
            }
            else
            {
                cnt = 150;
            }        
        }
        void cnt_Online讀取_10_檢查是否要讀取(ref byte cnt)
        {
            if (Online讀取_Ytemp + 顯示畫面列數索引 < ladder_list.Count)
            {
                Online讀取_要讀取 = true;
                Online讀取_ladder_temp = ladder_list[Online讀取_Ytemp + 顯示畫面列數索引][Online讀取_Xtemp];
                if (Online讀取_ladder_temp.ladderType == partTypeEnum.noPart) Online讀取_要讀取 = false;
                if (Online讀取_ladder_temp.ladderType == partTypeEnum.Data_no_Part) Online讀取_要讀取 = false;
                if (Online讀取_ladder_temp.ladderType == partTypeEnum.H_Line_Part) Online讀取_要讀取 = false;
                if (Online讀取_ladder_temp.ladderType == partTypeEnum.EndPart) Online讀取_要讀取 = false;
                if (Online讀取_ladder_temp.ladderType == partTypeEnum.leftParenthesis) Online讀取_要讀取 = false;
                if (Online讀取_ladder_temp.ladderType == partTypeEnum.rightParenthesis) Online讀取_要讀取 = false;
                if (Online讀取_要讀取)
                {
                    cnt++;
                    Online讀取_元素現在值 = 0;
                    return;
                }
                else
                {
                    cnt = 150;
                    return;
                }
            }
            else
            {
                cnt = 150;
                return;
            }    
         
        }
        void cnt_Online讀取_20_檢查元素最大值(ref byte cnt)
        {
            if (Online讀取_元素現在值 >= Online讀取_ladder_temp.元素數量)
            {
                cnt = 150;
                return;
            }
            else
            {
                cnt++;
            }
      
        }
        void cnt_Online讀取_20_檢查Z索引_分離字串(ref byte cnt)
        {
            string device = Online讀取_ladder_temp.ladderParam[Online讀取_元素現在值];
            DEVICE.TestDevice(device, out Online讀取_分離字串);

            cnt++;
        }
        void cnt_Online讀取_20_讀取Z值(ref byte cnt)
        {
            if (Online讀取_分離字串.Length == 2)
            {
                int temp = 0;
                int int_Read = 0;
                temp = TopMachine.DataRead(Online讀取_分離字串[1], ref int_Read);

                if (temp == -1)
                {
                    Online讀取_元素現在值++;
                    cnt = 20;
                    return;
                }
                else if (temp == 1)
                {
                    return;
                }
                else if (temp == 255)
                {
                    device.Set_Device(Online讀取_分離字串[1], int_Read);
                    cnt++;
                    return;
                }
            }
            else cnt++;
    
        }
        void cnt_Online讀取_20_設定Str_Device(ref byte cnt)
        {
            if (Online讀取_ladder_temp.ladderParam[Online讀取_元素現在值].Length > 1)
            {
                Online讀取_DeviceType = Online讀取_ladder_temp.ladderParam[Online讀取_元素現在值].Substring(0, 1);
                Online讀取_Device = Online讀取_ladder_temp.ladderParam[Online讀取_元素現在值];
                Online讀取_Device = device.DeviceIndex(Online讀取_Device);
                cnt++;
            }
            else
            {
                Online讀取_元素現在值++;
                cnt = 20;
            }
         
        }
        void cnt_Online讀取_20_檢查DeviceType(ref byte cnt)
        {
            if (Online讀取_DeviceType == "X" || Online讀取_DeviceType == "Y" || Online讀取_DeviceType == "M" || Online讀取_DeviceType == "S")
            {
                cnt = 25;
            }
            else if (Online讀取_DeviceType == "D" || Online讀取_DeviceType == "R" || Online讀取_DeviceType == "F" || Online讀取_DeviceType == "Z")
            {
                cnt = 30;
            }
            else if (Online讀取_DeviceType == "T")
            {
                cnt = 35;
            }
            else
            {
                Online讀取_元素現在值++;
                cnt = 20;
                return;
            }
         
        }
        void cnt_Online讀取_25_一般元件讀取(ref byte cnt)
        {
            int temp = 0;
            bool FLAG_Read = false;

            temp = TopMachine.DeviceRead(Online讀取_Device, ref FLAG_Read);

            if (temp == -1)
            {
                Online讀取_元素現在值++;
                cnt = 20;
                return;
            }
            else if (temp == 1)
            {
                return;
            }
            else if (temp == 255)
            {
                device.Set_Device(Online讀取_Device, FLAG_Read);
                Online讀取_元素現在值++;
                cnt = 20;
                return;
            }              
        }
        void cnt_Online讀取_30_特殊元件讀取_LOW(ref byte cnt)
        {
            int temp = 0;
            int int_Read = 0;
            temp = TopMachine.DataRead(Online讀取_Device, ref int_Read);

            if (temp == -1)
            {
                Online讀取_元素現在值++;
                cnt = 20;
                return;
            }
            else if (temp == 1)
            {
                return;
            }
            else if (temp == 255)
            {
                device.Set_Device(Online讀取_Device, int_Read);
                cnt ++;
                return;
            }  
        }
        void cnt_Online讀取_30_特殊元件讀取_HIGH(ref byte cnt)
        {
            if (Online讀取_ladder_temp.ladderParam[0].Substring(0, 1) == "D")
            {
                int temp = 0;
                int int_Read = 0;
                string str_temp = DEVICE.DeviceOffset(Online讀取_Device, 1);
                temp = TopMachine.DataRead(str_temp, ref int_Read);

                if (temp == -1)
                {
                    Online讀取_元素現在值++;
                    cnt = 20;
                    return;
                }
                else if (temp == 1)
                {
                    return;
                }
                else if (temp == 255)
                {
                    device.Set_Device(str_temp, int_Read);
                    Online讀取_元素現在值++;
                    cnt = 20;
                    return;
                }
            }
            else
            {
                Online讀取_元素現在值++;
                cnt = 20;
            }

        }
        void cnt_Online讀取_35_Timer_bool讀取(ref byte cnt)
        {
            int temp = 0;
            bool FLAG_Read = false;

            temp = TopMachine.DeviceRead(Online讀取_Device, ref FLAG_Read);

            if (temp == -1)
            {
                cnt++;
                return;
            }
            else if (temp == 1)
            {
                return;
            }
            else if (temp == 255)
            {
                device.Set_Device(Online讀取_Device, FLAG_Read);
                cnt++;
                return;
            }   
        }
        void cnt_Online讀取_35_Timer_Value讀取(ref byte cnt)
        {

            int temp = 0;
            int int_Read = 0;
            temp = TopMachine.DataRead(Online讀取_Device, ref int_Read);

            if (temp == -1)
            {
                Online讀取_元素現在值++;
                cnt = 20;
                return;
            }
            else if (temp == 1)
            {
                return;
            }
            else if (temp == 255)
            {
                device.Set_Device(Online讀取_Device, int_Read);
                Online讀取_元素現在值++;
                cnt = 20;
                return;
            }  
        }
        void cnt_Online讀取_150_元件讀取完成(ref byte cnt)
        {
            Online讀取_Xtemp++;
            cnt = 10;
        }
        void cnt_Online讀取_250_畫面讀取完成(ref byte cnt)
        {
            Online讀取_Xtemp = 0;
            Online讀取_Ytemp = 0;
            Online讀取_讀取完成 = true;
            cnt++;
        }
        void cnt_Online讀取_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        #region 畫面更新
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        private Graphics pic_ladder_g;
        Graphics Graphics_PictureBox;
        private static object objlock = new object();
        bool graphics_init = false;
        void sub_畫面更新()
        {
            if (FLAG_初始化 && T0_編譯畫面更新)
            {

                if (!graphics_init)
                {
                    bitmap_temp = new Bitmap(pictureBox_LADDER.Width, pictureBox_LADDER.Height);
                    pic_ladder_g = Graphics.FromImage(bitmap_temp);
                    graphics_init = true;
                }
                lock(this)
                {
                    try
                    {
                        if ((FLAG_編譯視窗獲取焦點 || true) || FindDevice.視窗已建立 || ReplaceDevice.視窗已建立 || TransferSetup.視窗已建立 || Download.視窗已建立)
                        {
                            sub_編譯畫面繪製(pic_ladder_g);
                            sub_複製窗選畫面繪製(pic_ladder_g);
                            sub_鼠線窗選畫面繪製(pic_ladder_g);
                            sub_註解繪製(pic_ladder_g);
                            sub_操作框繪製(pic_ladder_g);
                            sub_VscrollBar_檢查();
                            sub_ListBox_IL指令集更新();
                        }
                        pictureBox_LADDER.CreateGraphics().DrawImage(bitmap_temp,0,0);
                    }
                    finally
                    {

                    }

                }
      
                if (exButton_語法切換.Load_WriteState() != exButton_語法切換.FLAG_buf)
                {
                    if (exButton_語法切換.Load_WriteState())
                    {
                        CallBackUI.panel.是否隱藏(false, panel_Listbox_IL指令集);
                        FLAG_編譯視窗獲取焦點 = true;
                    }
                    else
                    {
                        CallBackUI.panel.是否隱藏(true, panel_Listbox_IL指令集);
                        FLAG_編譯視窗獲取焦點 = false;
                    }
                    exButton_語法切換.FLAG_buf = exButton_語法切換.Load_WriteState();
                }

            }
            if (FLAG_初始化) if ((bool)T0_編譯畫面更新) device_system.Set_Device("T0", 10);

        }
        void sub_編譯畫面繪製(Graphics pic_ladder_g)
        {
            for (int Y = 0; Y < 一個畫面列數; Y++)
            {
                for (int X = 0; X < 一列格數; X++)
                {
                    Point p = new Point(X * 操作方框大小.Width, Y * 操作方框大小.Height);
                    if (Y + 顯示畫面列數索引 < ladder_list.Count)
                    {
                        Color color;
                        if (ladder_list[Y + 顯示畫面列數索引][X].未編譯) color = Color.Gray;
                        else color = Color.White;
                        drawPart(pic_ladder_g, p, 操作方框大小, ladder_list[Y + 顯示畫面列數索引][X], FLAG_Online, Color.Black, color);
                    }
                    else
                    {
                        LADDER ladder_temp = new LADDER(1);
                        if (X == 0) ladder_temp.ladderType = partTypeEnum.leftParenthesis;
                        else if (X == 一列格數 - 1) ladder_temp.ladderType = partTypeEnum.rightParenthesis;
                        drawPart(pic_ladder_g, p, 操作方框大小, ladder_temp, FLAG_Online, Color.Black, Color.White);
                    }
                }
            }
        }
        void sub_註解繪製(Graphics pic_ladder_g)
        {
            if (!註解不顯示)
            {
                for (int Y = 0; Y < 一個畫面列數; Y++)
                {
                    for (int X = 0; X < 一列格數; X++)
                    {

                        if (Y + 顯示畫面列數索引 < ladder_list.Count)
                        {
                            bool 要繪製註解 = true;
                            if (ladder_list[Y + 顯示畫面列數索引][X].ladderType == partTypeEnum.H_Line_Part) 要繪製註解 = false;
                            if (ladder_list[Y + 顯示畫面列數索引][X].ladderType == partTypeEnum.leftParenthesis) 要繪製註解 = false;
                            if (ladder_list[Y + 顯示畫面列數索引][X].ladderType == partTypeEnum.rightParenthesis) 要繪製註解 = false;
                            if (ladder_list[Y + 顯示畫面列數索引][X].ladderType == partTypeEnum.noPart) 要繪製註解 = false;
                            if (ladder_list[Y + 顯示畫面列數索引][X].ladderType == partTypeEnum.Data_no_Part) 要繪製註解 = false;
                            if (ladder_list[Y + 顯示畫面列數索引][X].ladderType == partTypeEnum.EndPart) 要繪製註解 = false;
                            if (要繪製註解)
                            {
                                String 註解文字 = "";
                                object comment = new object();
                                if (X > 0 && (Y + 顯示畫面列數索引) < ladder_list.Count)
                                {
                                    for (int i = 0; i < ladder_list[Y + 顯示畫面列數索引][X].元素數量; i++)
                                    {
                                        Point p = new Point((X + i) * 操作方框大小.Width, Y * 操作方框大小.Height);
                                        if (!device.Get_Device(ladder_list[Y + 顯示畫面列數索引][X].ladderParam[i], 0, out comment))
                                        {
                                            comment = "                                      ";
                                            //comment = "ERROR";
                                        }
                                        註解文字 = (String)comment;
                                        draw_comment(pic_ladder_g, p, 操作方框大小, 註解文字);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        
        }
        void sub_複製窗選畫面繪製(Graphics pic_ladder_g)
        {
            Point 初始位 = 操作框窗選_初始位;
            if (操作框窗選_位移量.X != 0 || 操作框窗選_位移量.Y != 0)
            {
                if (窗選模式 == Tx_窗選模式.複製模式)
                {
                    while (true)
                    {
                        bool FLAG_走訪完成 = false;
                        if (初始位.X >= 0 && 初始位.Y >= 0 && 初始位.X < 一列格數 && 初始位.Y < ladder_list.Count)
                        {                      
                            Tx_操作框走訪方向 操作框走訪方向 = new Tx_操作框走訪方向();
                            sub_複製模式_檢查操作框走訪方向(操作框窗選_初始位, 操作框窗選_位移量, ref 初始位, ref 操作框走訪方向);
                            if (初始位.Y >= 顯示畫面列數索引 && 初始位.Y < ladder_list.Count)
                            {
                                Point p = new Point(初始位.X * 操作方框大小.Width, (初始位.Y - 顯示畫面列數索引) * 操作方框大小.Height);
                                drawPart(pic_ladder_g, p, 操作方框大小, ladder_list[初始位.Y][初始位.X], FLAG_Online, Color.LightGray, Color.FromArgb(255, 0, 0, 240));
                            }
                            if (操作框走訪方向 == Tx_操作框走訪方向.無) FLAG_走訪完成 = true;
                            sub_複製模式_操作框走訪(操作框窗選_初始位, ref 初始位, 操作框走訪方向);
                        }
                        else FLAG_走訪完成 = true;

                        if (FLAG_走訪完成) break;
                    }
                }
            }

        }
        void sub_鼠線窗選畫面繪製(Graphics pic_ladder_g)
        {
            if (操作框窗選_位移量.X != 0 || 操作框窗選_位移量.Y != 0)
            {
                if (窗選模式 == Tx_窗選模式.鼠線模式)
                {
                    int 起始_X = 0;
                    int 起始_Y = 0;
                    int 結束_X = 0;
                    int 結束_Y = 0;
                    int 要畫橫線列數 = 0;
                    int 要畫豎線行豎數 = 0;
                    #region 檢查起始與結束位
                    if (操作框窗選_位移量.Y > 0)//往下畫鼠線
                    {
                        起始_Y = 操作框窗選_初始位.Y;
                        結束_Y = 操作框窗選_初始位.Y + 操作框窗選_位移量.Y;
                        要畫橫線列數 = 結束_Y;

                        if (操作框窗選_位移量.X < 0)
                        {
                            起始_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            結束_X = 操作框窗選_初始位.X;
                            要畫豎線行豎數 = 結束_X;
                        }
                        if (操作框窗選_位移量.X >= 0)
                        {
                            起始_X = 操作框窗選_初始位.X;
                            結束_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            要畫豎線行豎數 = 起始_X;
                        }
                    }
                    if (操作框窗選_位移量.Y < 0)//往上畫鼠線
                    {

                        if (操作框窗選_位移量.X < 0)
                        {
                            起始_Y = 操作框窗選_初始位.Y + 操作框窗選_位移量.Y;
                            結束_Y = 操作框窗選_初始位.Y;
                            要畫橫線列數 = 結束_Y;

                            起始_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            結束_X = 操作框窗選_初始位.X;
                            要畫豎線行豎數 = 起始_X;
                        }
                        if (操作框窗選_位移量.X > 0)
                        {
                            起始_Y = 操作框窗選_初始位.Y + 操作框窗選_位移量.Y;
                            結束_Y = 操作框窗選_初始位.Y;
                            要畫橫線列數 = 結束_Y;

                            起始_X = 操作框窗選_初始位.X;
                            結束_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            要畫豎線行豎數 = 結束_X;
                        }
                    }
                    if (操作框窗選_位移量.Y == 0)//畫水平線
                    {
                        起始_Y = 操作框窗選_初始位.Y + 操作框窗選_位移量.Y;
                        結束_Y = 操作框窗選_初始位.Y;
                        要畫橫線列數 = 結束_Y;

                        if (操作框窗選_位移量.X < 0)
                        {
                            起始_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            結束_X = 操作框窗選_初始位.X;
                            要畫豎線行豎數 = 結束_X;
                        }
                        if (操作框窗選_位移量.X > 0)
                        {
                            起始_X = 操作框窗選_初始位.X;
                            結束_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            要畫豎線行豎數 = 結束_X;
                        }
                    }
                    if (操作框窗選_位移量.X == 0)//畫豎線
                    {

                        if (操作框窗選_位移量.Y < 0)
                        {
                            起始_Y = 操作框窗選_初始位.Y + 操作框窗選_位移量.Y;
                            結束_Y = 操作框窗選_初始位.Y;

                            起始_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            結束_X = 操作框窗選_初始位.X;
                            要畫豎線行豎數 = 結束_X;
                            要畫橫線列數 = 結束_Y;
                        }
                        if (操作框窗選_位移量.Y > 0)
                        {
                            起始_Y = 操作框窗選_初始位.Y;
                            結束_Y = 操作框窗選_初始位.Y + 操作框窗選_位移量.Y;

                            起始_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            結束_X = 操作框窗選_初始位.X;
                            要畫豎線行豎數 = 起始_X;
                            要畫橫線列數 = 起始_Y;
                        }
                    }
                    #endregion
                    for (int Y = 起始_Y; Y <= 結束_Y; Y++)
                    {
                        for (int X = 起始_X; X <= 結束_X; X++)
                        {
                            #region 線判斷
                            LADDER ladder_temp = new LADDER(1);

                            bool Vline_左上 = false;
                            bool Vline_左下 = false;
                            Point 繪製位置 = new Point(X, Y);
                            if (操作框窗選_位移量.X == 0)//只有Y移動
                            {
                                Vline_左上 = true;
                                Vline_左下 = true;
                                if (操作框窗選_位移量.Y > 0)
                                {
                                    if (Y == 起始_Y)
                                    {
                                        Vline_左上 = false;
                                        Vline_左下 = true;
                                    }
                                    if (Y == 結束_Y)
                                    {
                                        Vline_左上 = true;
                                        Vline_左下 = false;
                                    }
                                }
                                if (操作框窗選_位移量.Y < 0)
                                {
                                    if (Y == 結束_Y)
                                    {
                                        Vline_左上 = true;
                                        Vline_左下 = false;
                                    }
                                    if (Y == 起始_Y)
                                    {
                                        Vline_左上 = false;
                                        Vline_左下 = true;
                                    }
                                }

                            }
                            else if (操作框窗選_位移量.Y == 0)//只有X移動
                            {
                                if (X != 結束_X) ladder_temp.ladderType = partTypeEnum.H_Line_Part;
                            }
                            else if (操作框窗選_位移量.X < 0 && 操作框窗選_位移量.Y > 0)//左下
                            {
                                if (X == 要畫豎線行豎數)
                                {
                                    Vline_左上 = true;
                                    Vline_左下 = true;
                                    if (Y == 起始_Y)
                                    {
                                        Vline_左上 = false;
                                        Vline_左下 = true;
                                    }
                                    if (Y == 結束_Y)
                                    {
                                        Vline_左上 = true;
                                        Vline_左下 = false;
                                    }
                                }
                                else if (Y == 要畫橫線列數)
                                {
                                    ladder_temp.ladderType = partTypeEnum.H_Line_Part;
                                }
                            }
                            else if (操作框窗選_位移量.X > 0 && 操作框窗選_位移量.Y > 0)//右下
                            {
                                if (X == 要畫豎線行豎數)
                                {
                                    Vline_左上 = true;
                                    Vline_左下 = true; ;
                                    if (Y == 起始_Y)
                                    {
                                        Vline_左上 = false;
                                        Vline_左下 = true;
                                    }
                                    if (Y == 結束_Y)
                                    {
                                        Vline_左上 = true;
                                        Vline_左下 = false;
                                    }
                                }
                                if (Y == 要畫橫線列數)
                                {
                                    ladder_temp.ladderType = partTypeEnum.H_Line_Part;
                                }
                            }
                            else if (操作框窗選_位移量.X < 0 && 操作框窗選_位移量.Y < 0)//左上
                            {

                                if (Y == 要畫橫線列數)
                                {
                                    if (X != 結束_X) ladder_temp.ladderType = partTypeEnum.H_Line_Part;
                                }
                                if (X == 要畫豎線行豎數)
                                {
                                    Vline_左上 = true;
                                    Vline_左下 = true;
                                    if (Y == 結束_Y)
                                    {
                                        Vline_左上 = true;
                                        Vline_左下 = false;
                                    }
                                    if (Y == 起始_Y)
                                    {
                                        Vline_左上 = false;
                                        Vline_左下 = true;
                                    }
                                }
                            }
                            else if (操作框窗選_位移量.X > 0 && 操作框窗選_位移量.Y < 0)//右上
                            {

                                if (Y == 要畫橫線列數)
                                {
                                    if (X != 結束_X) ladder_temp.ladderType = partTypeEnum.H_Line_Part;
                                }
                                if (X == 要畫豎線行豎數)
                                {
                                    Vline_左上 = true;
                                    Vline_左下 = true;
                                    if (Y == 起始_Y)
                                    {
                                        Vline_左上 = false;
                                        Vline_左下 = true;
                                    }
                                    if (Y == 結束_Y)
                                    {
                                        Vline_左上 = true;
                                        Vline_左下 = false;
                                    }
                                }
                            }
                            #endregion
                            Point p = new Point(繪製位置.X * 操作方框大小.Width, (繪製位置.Y - 顯示畫面列數索引) * 操作方框大小.Height);
                            if (繪製位置.X > 0) drawPart(pic_ladder_g, p, 操作方框大小, ladder_temp, FLAG_Online, Color.Yellow, Color.Transparent);
                            if (Vline_左上 || Vline_左下)
                            {
                                p = new Point((繪製位置.X - 1) * 操作方框大小.Width, (繪製位置.Y - 顯示畫面列數索引) * 操作方框大小.Height);
                                ladder_temp = new LADDER(1);
                                ladder_temp.Vline_右上 = Vline_左上;
                                ladder_temp.Vline_右下 = Vline_左下;
                                if (繪製位置.X - 1 > 0) drawPart(pic_ladder_g, p, 操作方框大小, ladder_temp, FLAG_Online, Color.Yellow, Color.Transparent);
                            }
                        }
                    }

                }
            }
        }
        void sub_操作框繪製(Graphics pic_ladder_g)
        {
            if ((操作方框索引.Y + 顯示畫面列數索引) < ladder_list.Count && 操作方框索引.X < 一列格數 && (操作方框索引.Y + 顯示畫面列數索引) >= 0)
            {
                int Width = ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].元素數量 * 操作方框大小.Width;
                int Height = 操作方框大小.Height;
                Point p0 = new Point(操作方框索引.X * 操作方框大小.Width, 操作方框索引.Y * 操作方框大小.Height);
                draw_control_rect(pic_ladder_g, p0, new Size(Width, Height), (FLAG_編譯視窗獲取焦點 || FindDevice.視窗已建立 || ReplaceDevice.視窗已建立));
            }
  

        }
        void sub_VscrollBar_檢查()
        {
            if (ladder_list.Count != 程式行數)
            {
                程式行數 = ladder_list.Count;
                if (程式行數 > 一個畫面列數)
                {
                    CallBackUI.vScrollBar.設定最大值(程式行數 - 1, vScrollBar_picture_滾動條);
                    CallBackUI.vScrollBar.是否隱藏(true, vScrollBar_picture_滾動條);
                }
                else
                {
                    CallBackUI.vScrollBar.是否隱藏(false, vScrollBar_picture_滾動條);
                }

            }
            if (!FLAG_vScrollBar_picture_Scroll)
            {
                if (顯示畫面列數索引 != vScrollBar_picture_滾動條_value_buf)
                {
                    vScrollBar_picture_滾動條_value_buf = 顯示畫面列數索引;
                    
                    CallBackUI.vScrollBar.設定現在值(顯示畫面列數索引, vScrollBar_picture_滾動條);
                }
            }
            else
            {

            }
        }
     
        #region ListBox_IL指令集更新
        byte cnt_ListBox_IL指令集更新 = 255;
        void sub_ListBox_IL指令集更新()
        {
            if (cnt_ListBox_IL指令集更新 == 1) ListBox_IL指令集更新_清除列表(ref cnt_ListBox_IL指令集更新);
            if (cnt_ListBox_IL指令集更新 == 2) ListBox_IL指令集更新_開始更新(ref cnt_ListBox_IL指令集更新);
            if (cnt_ListBox_IL指令集更新 == 3) cnt_ListBox_IL指令集更新 = 255;
        }
        void ListBox_IL指令集更新_清除列表(ref byte cnt)
        {
            CallBackUI.listbox.清除所有資料(listBox_IL指令集);
            cnt++;
        }
        void ListBox_IL指令集更新_開始更新(ref byte cnt)
        {
            for (int i = 0; i < IL指令程式.Count; i++)
            {
                string str_temp = "";
                for (int j = 0; j < IL指令程式[i].Length; j++)
                {
                    string str_temp0 = "";
                    str_temp0 = IL指令程式[i][j];
                    if (str_temp0.Length < 10) str_temp0 += " ";
                    if (str_temp0.Length < 10) str_temp0 += " ";
                    if (str_temp0.Length < 10) str_temp0 += " ";
                    if (str_temp0.Length < 10) str_temp0 += " ";
                    if (str_temp0.Length < 10) str_temp0 += " ";
                    if (str_temp0.Length < 10) str_temp0 += " ";
                    if (str_temp0.Length < 10) str_temp0 += " ";
                    if (str_temp0.Length < 10) str_temp0 += " ";
                    if (str_temp0.Length < 10) str_temp0 += " ";
                    if (str_temp0.Length < 10) str_temp0 += " ";
                    if (str_temp0.Length < 10) str_temp0 += " ";
                    if (str_temp0.Length < 10) str_temp0 += " ";
                    str_temp += str_temp0;

                    string str_temp1 = "";
                    if (j == 0)
                    {
                        str_temp1 = i.ToString();
                        if (str_temp1.Length < 9) str_temp1 += " ";
                        if (str_temp1.Length < 9) str_temp1 += " ";
                        if (str_temp1.Length < 9) str_temp1 += " ";
                        if (str_temp1.Length < 9) str_temp1 += " ";
                        if (str_temp1.Length < 9) str_temp1 += " ";
                        if (str_temp1.Length < 9) str_temp1 += " ";
                        if (str_temp1.Length < 9) str_temp1 += " ";
                        if (str_temp1.Length < 9) str_temp1 += " ";
                        if (str_temp1.Length < 9) str_temp1 += " ";
                        if (str_temp1.Length < 9) str_temp1 += " ";
                        str_temp = str_temp1 + str_temp;
                    }
                }
                CallBackUI.listbox.新增項目(str_temp, listBox_IL指令集);
            }
            cnt++;
        }
        void ListBox_IL指令集更新_(ref byte cnt)
        {
            cnt++;
        }
        #endregion

        private void drawPart(Graphics g, Point start_po, Size size, LADDER ladderNode,bool Online ,Color 線段顏色, Color 背景顏色)
        {
            switch (ladderNode.ladderType)
            {
                case partTypeEnum.leftParenthesis:
                    draw_leftParenthesis(g, start_po, size, ladderNode, 線段顏色, 背景顏色);
                    break;

                case partTypeEnum.rightParenthesis:
                    draw_rightParenthesis(g, start_po, size, ladderNode, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.Data_no_Part: //DATA空元件
                    draw_Data_no_Part(g, start_po, size, 背景顏色);
                    break;
                case partTypeEnum.noPart: //空元件
                    draw_noPart(g, start_po, size, 背景顏色);
                    break;

                case partTypeEnum.A_Part:
                    draw_A_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;

                case partTypeEnum.B_Part:
                    draw_B_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;

                case partTypeEnum.OUT_Part:
                    draw_OUT_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;

                case partTypeEnum.H_Line_Part:
                    draw_H_Line_Part(g, start_po, size, ladderNode, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.EndPart:
                    draw_End_Part(g, start_po, size, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.TAB_PART:
                    draw_TAB_Part(g, start_po, size, ladderNode,線段顏色, 背景顏色);
                    break;
                case partTypeEnum.LD_Equal_Part:
                    draw_LD_Equal_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.MOV_Part:
                    draw_MOV_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.ADD_PART:
                    draw_ADD_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.SUB_PART:
                    draw_SUB_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.MUL_PART:
                    draw_MUL_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.DIV_PART:
                    draw_DIV_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.INC_PART:
                    draw_INC_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.DRVA_PART:
                    draw_DRVA_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.DRVI_PART:
                    draw_DRVI_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.PLSV_PART:
                    draw_PLSV_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.OUT_TIMER_PART:
                    draw_OUT_TIMER_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.SET_PART:
                    draw_SET_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.RST_PART:
                    draw_RST_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.ZRST_PART:
                    draw_ZRST_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.BMOV_PART:
                    draw_BMOV_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.WTB_PART:
                    draw_WTB_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.BTW_PART:
                    draw_BTW_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.JUMP_PART:
                    draw_JUMP_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;
                case partTypeEnum.REF_PART:
                    draw_REF_Part(g, start_po, size, ladderNode, Online, 線段顏色, 背景顏色);
                    break;   
            }
            if (ladderNode.Vline_右上) draw_Vline_右上(g, start_po, size, 接點偏移位置_Y, 線段顏色);
            if (ladderNode.Vline_右下) draw_Vline_右下(g, start_po, size, 接點偏移位置_Y, 線段顏色);
            if (ladderNode.Vline_左上) draw_Vline_左上(g, start_po, size, 接點偏移位置_Y, 線段顏色);
            if (ladderNode.Vline_左下) draw_Vline_左下(g, start_po, size, 接點偏移位置_Y, 線段顏色);
        }
        private void draw_Vline_右上(Graphics g, Point start_po, Size size, int offset, Color 線段顏色)
        {
            //   p0
            //   ｜
            //   p1
            //   ｜
            //   p2
            PointF p0 = new PointF(start_po.X + size.Width - 1, start_po.Y);
            PointF p1 = new PointF(p0.X, p0.Y + offset);
            PointF p2 = new PointF(p0.X, p0.Y + (int)(size.Height));
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
        }
        private void draw_Vline_右下(Graphics g, Point start_po, Size size, int offset, Color 線段顏色)
        {
            //   p0
            //   ｜
            //   p1
            //   ｜
            //   p2
            PointF p0 = new PointF(start_po.X + size.Width - 1, start_po.Y);
            PointF p1 = new PointF(p0.X, p0.Y + offset);
            PointF p2 = new PointF(p0.X, p0.Y + (int)(size.Height));
            Draw.線段繪製(p1, p2, 線段顏色, 1, g, 1, 1);
        }
        private void draw_Vline_左上(Graphics g, Point start_po, Size size, int offset, Color 線段顏色)
        {
            //   p0
            //   ｜
            //   p1
            //   ｜
            //   p2
            PointF p0 = new PointF(start_po.X - 1, start_po.Y);
            PointF p1 = new PointF(p0.X, p0.Y + offset);
            PointF p2 = new PointF(p0.X, p0.Y + (int)(size.Height));
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
        }
        private void draw_Vline_左下(Graphics g, Point start_po, Size size, int offset, Color 線段顏色)
        {
            //   p0
            //   ｜
            //   p1
            //   ｜
            //   p2
            PointF p0 = new PointF(start_po.X - 1, start_po.Y);
            PointF p1 = new PointF(p0.X, p0.Y + offset);
            PointF p2 = new PointF(p0.X, p0.Y + (int)(size.Height));
            Draw.線段繪製(p1, p2, 線段顏色, 1, g, 1, 1);
        }
        private void draw_leftParenthesis(Graphics g, Point start_po, Size size, LADDER ladderNode, Color 線段顏色, Color 背景顏色)
        {
            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            PointF p0 = new PointF(start_po.X + size.Width - 1, start_po.Y);
            Draw.文字右中繪製(ladderNode.ladderParam[0], new PointF(p0.X, p0.Y + 接點偏移位置_Y), 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
        }
        private void draw_rightParenthesis(Graphics g, Point start_po, Size size, LADDER ladderNode, Color 線段顏色, Color 背景顏色)
        {
            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
        }
        private void draw_Data_no_Part(Graphics g, Point start_po, Size size, Color 背景顏色)
        {
     

        }
        private void draw_noPart(Graphics g, Point start_po, Size size, Color 背景顏色)
        {
            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1); ;
        }
        private void draw_A_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;

            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);

            
            //          p2 p4
            //  p0 — p1｜ ｜p6—p7
            //          p3 p5
            start_po.X += 0;
            PointF p0, p1, p2, p3, p4, p5, p6, p7;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (size.Width / 2.0F - 接點寬度 / 2.0F), p0.Y);
            p2 = new PointF(p1.X, p1.Y - (接點高度 / 2.0F));
            p3 = new PointF(p1.X, p1.Y + (接點高度 / 2.0F));
            p4 = new PointF(p2.X + 接點寬度, p2.Y);
            p5 = new PointF(p3.X + 接點寬度, p3.Y);
            p6 = new PointF(p1.X + 接點寬度, p1.Y);
            p7 = new PointF(p0.X + size.Width, p0.Y);

            if(Online)
            {
                string str_接點訊息 = ladderNode.ladderParam[0];
                object value = new object();
                PointF p0_online = p2;
                Size size_online = new System.Drawing.Size((int)(p4.X - p2.X), (int)(p5.Y - p4.Y));
                if(device.Get_Device(str_接點訊息, out value))
                {
                    if ((bool)value) Draw.方框繪製(p0_online, size_online, Online顏色, 0, true, g, 1, 1);   
                }
           
            }
            PointF str_po = new PointF((p2.X + p4.X) / 2, (p2.Y + p4.Y) / 2 - 接點偏移位置_Y / 2);
            Draw.文字中心繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);

            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p4, p5, 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p6, p7, 線段顏色, 1, g, 1, 1);


        }
        private void draw_B_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;

            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);

            //          p2 p4
            //  p0 — p1｜ ｜p6—p7
            //          p3 p5
            //            p8
            //            /
            //            p9
            start_po.X += 0;
            PointF p0, p1, p2, p3, p4, p5, p6, p7, p8, p9;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (size.Width / 2.0F - 接點寬度 / 2.0F), p0.Y);
            p2 = new PointF(p1.X, p1.Y - (接點高度 / 2.0F));
            p3 = new PointF(p1.X, p1.Y + (接點高度 / 2.0F));
            p4 = new PointF(p2.X + 接點寬度, p2.Y);
            p5 = new PointF(p3.X + 接點寬度, p3.Y);
            p6 = new PointF(p1.X + 接點寬度, p1.Y);
            p7 = new PointF(p0.X + size.Width, p0.Y);
            p8 = new PointF(p4.X + (接點寬度 / 2.0F), p4.Y);
            p9 = new PointF(p3.X - (接點寬度 / 2.0F), p3.Y);

            if (Online)
            {
                string str_接點訊息 = ladderNode.ladderParam[0];
                object value = new object();
                PointF p0_online = p2;
                Size size_online = new System.Drawing.Size((int)(p4.X - p2.X), (int)(p5.Y - p4.Y));
                device.Get_Device(str_接點訊息, out value);
                if (!(bool)value) Draw.方框繪製(p0_online, size_online, Online顏色, 0, true, g, 1, 1);
            }

            PointF str_po = new PointF((p2.X + p4.X) / 2, (p2.Y + p4.Y) / 2 - 接點偏移位置_Y / 2);
            Draw.文字中心繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);

            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p4, p5, 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p6, p7, 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p8, p9, 線段顏色, 1, g, 1, 1);


        }
        private void draw_OUT_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            start_po.X += 0;//  p0—p1( )p2—p3

            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            bool 接點狀態 = false;
            if (Online)
            {
                string str_接點訊息 = ladderNode.ladderParam[0];
                object value = new object();
                PointF p0_online = p1;
                p0_online.X += 接點寬度 / 2.0F;
                p0_online.Y -= 接點高度 / 2.0F;
                Size size_online = new System.Drawing.Size((int)(p2.X - p1.X - 接點寬度), (int)(接點高度));
                if(device.Get_Device(str_接點訊息, out value))
                {
                    if ((bool)value)
                    {
                        Draw.方框繪製(p0_online, size_online, Online顏色, 0, true, g, 1, 1);
                        Draw.左括號填滿(p1, new Size(接點寬度, 接點高度), Online顏色, g, 1, 1);
                        Draw.右括號填滿(p2, new Size(接點寬度, 接點高度), Online顏色, g, 1, 1);
                        接點狀態 = true;
                    }
                }
         
            }

            PointF str_po = new PointF((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            if (!接點狀態) Draw.文字中心繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            else Draw.文字中心繪製(ladderNode.ladderParam[0], str_po, 視窗字體, Color.White, Color.Transparent, g, 1, 1);

            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.右括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);


        }
        private void draw_H_Line_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, Color 線段顏色, Color 背景顏色)
        {
            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            start_po.X += 0;//  p0—p1( )p2—p3
            //          
            start_po.X += 0;//  p0 — p1
            //          
            PointF p0, p1;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + size.Width, p0.Y);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);

        }
        private void draw_LD_Equal_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
            if (ladderNode.ladderParam[0].Substring(0, 1) == "D") double_word = true;
            Int64 value0 = 1;
            Int64 value1 = 2;
            PointF 左括號連結點 = new PointF();
            PointF 右括號連結點 = new PointF();
            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);

            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);     
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
         
            左括號連結點 = p1;
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[1];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);                           
                }
   
                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
                value0 = value;
            }
            //----------------------------------------------------------------------------------------
            //第三區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[2], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[2];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
                value1 = value;
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            右括號連結點 = p2;
            if (Online)
            {
                string type = "";
                if (double_word) type = ladderNode.ladderParam[0].Substring(1);
                else type = ladderNode.ladderParam[0];
                bool OK = false;
                if(type == "=")
                {
                    if (value0 == value1) OK = true;
                }
                else if (type == ">")
                {
                    if (value0 > value1) OK = true;
                }
                else if (type == "<")
                {
                    if (value0 < value1) OK = true;
                }
                else if (type == ">=")
                {
                    if (value0 >= value1) OK = true;
                }
                else if (type == "<=")
                {
                    if (value0 <= value1) OK = true;
                }
                else if (type == "<>")
                {
                    if (value0 != value1) OK = true;
                }
                if(OK)
                {
                    Draw.左方形括號填滿(左括號連結點, new Size(接點寬度, 接點高度 * 2), Online顏色, 1, g, 1, 1);
                    Draw.右方形括號填滿(右括號連結點, new Size(接點寬度, 接點高度 * 2), Online顏色, 1, g, 1, 1);
                }
            }
            Draw.左方形括號繪製(左括號連結點, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.右方形括號繪製(右括號連結點, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);

     
        }
        private void draw_MOV_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
            if (ladderNode.ladderParam[0].Substring(0, 1) == "D") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[1];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);

                if (str_接點訊息_low.Substring(0, 1) == "T")
                {
                    device.Get_Device(str_接點訊息_low, 2, out value_low);
                }
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第三區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[2], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[2];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_ADD_Part(Graphics g, Point start_po, Size size, LADDER ladderNode,bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
            if (ladderNode.ladderParam[0].Substring(0, 1) == "D") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[1];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第三區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[2], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[2];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第四區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[3], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[3];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_SUB_Part(Graphics g, Point start_po, Size size, LADDER ladderNode,bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
            if (ladderNode.ladderParam[0].Substring(0, 1) == "D") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[1];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第三區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[2], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[2];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第四區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[3], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[3];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_MUL_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
            if (ladderNode.ladderParam[0].Substring(0, 1) == "D") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[1];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第三區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[2], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[2];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第四區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[3], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[3];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_DIV_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
            if (ladderNode.ladderParam[0].Substring(0, 2) == "DD") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[1];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第三區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[2], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[2];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第四區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[3], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[3];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_INC_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
            if (ladderNode.ladderParam[0].Substring(0, 1) == "D") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);

            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[1];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_DRVA_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
          //  if (ladderNode.ladderParam[0].Substring(0, 2) == "DD") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[1];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第三區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[2], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[2];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第四區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[3], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[3];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_DRVI_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
           // if (ladderNode.ladderParam[0].Substring(0, 2) == "DD") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[1];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第三區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[2], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[2];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第四區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[3], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[3];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
         
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_PLSV_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
          //  if (ladderNode.ladderParam[0].Substring(0, 1) == "D") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
 
            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[1];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第三區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[2], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[2];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_SET_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            object value = new object();
            PointF 左括號連結點 = new PointF();
            PointF 右括號連結點 = new PointF();
            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);

            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            左括號連結點 = p1;
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息 = ladderNode.ladderParam[1];
                device.Get_Device(str_接點訊息, out value);

            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            右括號連結點 = p2;
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
            if (Online)
            {
                if (value.GetType().Name == "Boolean")
                {
                    if ((bool)value)
                    {
                        Draw.左方形括號填滿(左括號連結點, new Size(接點寬度, 接點高度 * 2), Online顏色, 1, g, 1, 1);
                        Draw.右方形括號填滿(右括號連結點, new Size(接點寬度, 接點高度 * 2), Online顏色, 1, g, 1, 1);
                    }
                }
            }
        }
        private void draw_RST_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            object value = new object();
            PointF 左括號連結點 = new PointF();
            PointF 右括號連結點 = new PointF();
            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);

            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            左括號連結點 = p1;
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息 = ladderNode.ladderParam[1];    
                device.Get_Device(str_接點訊息, out value);

            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            右括號連結點 = p2;
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
            if (Online)
            {
                if (value.GetType().Name == "Boolean")
                {
                    if (!(bool)value)
                    {
                        Draw.左方形括號填滿(左括號連結點, new Size(接點寬度, 接點高度 * 2), Online顏色, 1, g, 1, 1);
                        Draw.右方形括號填滿(右括號連結點, new Size(接點寬度, 接點高度 * 2), Online顏色, 1, g, 1, 1);
                    }
                }
            }
        }
        private void draw_OUT_TIMER_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);

            bool 接點狀態 = false;
            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);

            if (Online)
            {
                string str_接點訊息 = ladderNode.ladderParam[0];
                object value = new object();
                PointF p0_online = new PointF();

                if (device.Get_Device(str_接點訊息, out value))
                {
                    p0_online = p1;
                    p1.X -= 接點寬度 / 2;
                    p0_online.Y -= 接點高度 / 2;
                    PointF 左括號點 = p1;
                    PointF 右括號點 = new PointF(p0.X + size.Width * 2 - 接點寬度, p1.Y);
                    Size size_online = new System.Drawing.Size((int)(右括號點.X - 左括號點.X - 接點寬度), (int)(接點高度));
                    if ((bool)value)
                    {
                        接點狀態 = true;
                    }
                    if ((bool)value)
                    {
                        Draw.方框繪製(p0_online, size_online, Online顏色, 0, true, g, 1, 1);
                        Draw.左括號填滿(左括號點, new Size(接點寬度, 接點高度), Online顏色, g, 1, 1);
                        Draw.右括號填滿(右括號點, new Size(接點寬度, 接點高度), Online顏色, g, 1, 1);
                    }
                }

                if (device.Get_Device(str_接點訊息, 2, out value))
                {
                    p0_online = p1;
                    p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                    p0_online.Y -= 接點偏移位置_Y / 2;
                    Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
                }
            }

            if(!接點狀態)Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            else Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, Color.White, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
  

            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            if (!接點狀態) Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            else Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, Color.White, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息 = ladderNode.ladderParam[1];
                object value = new object();
                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;
                if (device.Get_Device(str_接點訊息, out value))
                {
                    Draw.文字右中繪製(value.ToString(), p0_online, 視窗字體, Online顏色, Color.Transparent, g, 1, 1);
                }
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_ZRST_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
           // bool double_word = false;
           // if (ladderNode.ladderParam[0].Substring(0, 1) == "D") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
      
            }
            //----------------------------------------------------------------------------------------
            //第三區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[2], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {

            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_BMOV_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
            if (ladderNode.ladderParam[0].Substring(0, 1) == "D") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[1];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第三區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[2], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[2];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第四區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[3], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[3];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_WTB_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
            if (ladderNode.ladderParam[0].Substring(0, 1) == "D") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[1];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第三區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[2], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[2];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第四區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[3], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息 = ladderNode.ladderParam[3];
                object value = new object();
                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;
                if (device.Get_Device(str_接點訊息, out value))
                {
                    string str_temp = "";
                    if ((bool)value) str_temp = "ON";
                    else str_temp = "OFF";
                    Draw.文字右中繪製(str_temp, p0_online, 視窗字體, Online顏色, Color.Transparent, g, 1, 1);
                }
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_BTW_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
            if (ladderNode.ladderParam[0].Substring(0, 1) == "D") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息 = ladderNode.ladderParam[1];
                object value = new object();
                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;
                if (device.Get_Device(str_接點訊息, out value))
                {
                    string str_temp = "";
                    if ((bool)value) str_temp = "ON";
                    else str_temp = "OFF";
                    Draw.文字右中繪製(str_temp, p0_online, 視窗字體, Online顏色, Color.Transparent, g, 1, 1);
                }
            }
            //----------------------------------------------------------------------------------------
            //第三區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[2], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[2];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            //第四區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[3], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[3];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_End_Part(Graphics g, Point start_po, Size size, Color 線段顏色, Color 背景顏色)
        {
            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            start_po.X += 0;//  p0—p1( )p2—p3

            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);

            PointF str_po = new PointF((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            Draw.文字中心繪製("END", str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);

            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_TAB_Part(Graphics g, Point start_po, Size size, LADDER ladderNode,Color 線段顏色, Color 背景顏色)
        {
            Point clear_po = start_po;
            Point p0 = new Point();
            Point p1 = new Point();
            Point p2 = new Point();
            Point p3 = new Point();
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width * 11, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            start_po.X += 0;//  p0—p1
                            //  p2—p3
            p0.X = start_po.X;
            p0.Y = start_po.Y + (size.Height / 3) - (size.Height / 40);
            p1.X = start_po.X * 11;
            p1.Y = p0.Y;
            p2.X = start_po.X;
            p2.Y = start_po.Y + (size.Height / 3) + (size.Height / 40);
            p3.X = start_po.X * 11;
            p3.Y = p2.Y;
            Draw.線段繪製(p0, p1, 線段顏色, 3, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);

            float 文字高度 = g.MeasureString(ladderNode.ladderParam[1], 註解字體).Height;
            Draw.文字左上繪製("<" + ladderNode.ladderParam[1] + ">" + ladderNode.ladderParam[2], new PointF(p2.X, p2.Y + size.Height / 30), 註解字體, Color.ForestGreen, Color.Transparent, g, 1, 1);
        }
        private void draw_JUMP_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
            //if (ladderNode.ladderParam[0].Substring(0, 1) == "D") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);

            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[1];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }
        private void draw_REF_Part(Graphics g, Point start_po, Size size, LADDER ladderNode, bool Online, Color 線段顏色, Color 背景顏色)
        {
            bool double_word = false;
            //  if (ladderNode.ladderParam[0].Substring(0, 1) == "D") double_word = true;

            Point clear_po = start_po;
            clear_po.X += 0;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);
            clear_po.X += size.Width;
            clear_po.Y += 0;
            Draw.方框繪製(clear_po, new Size(size.Width - 0, size.Height - 0), 背景顏色, 0, true, g, 1, 1);

            //----------------------------------------------------------------------------------------
            //第一區域繪製
            start_po.X += 0;
            PointF p0, p1, p2, p3;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            PointF str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[0], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            Draw.線段繪製(p0, p1, 線段顏色, 1, g, 1, 1);
            Draw.左方形括號繪製(p1, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            //----------------------------------------------------------------------------------------
            //第二區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[1], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {

            }
            //----------------------------------------------------------------------------------------
            //第三區域繪製
            start_po.X += size.Width;
            p0 = new PointF(start_po.X, start_po.Y + 接點偏移位置_Y);
            p1 = new PointF(p0.X + (接點寬度 / 2.0F), p0.Y);
            str_po = new PointF(p1.X + 接點寬度, p1.Y);
            Draw.文字左中繪製(ladderNode.ladderParam[2], str_po, 視窗字體, 線段顏色, Color.Transparent, g, 1, 1);
            if (Online)
            {
                string str_接點訊息_low = ladderNode.ladderParam[2];
                string str_接點訊息_high = "";
                object value_low = new object();
                object value_high = new object();
                if (str_接點訊息_low.Substring(0, 1) != "K") str_接點訊息_high = DEVICE.DeviceOffset(str_接點訊息_low, 1);
                else
                {
                    if (!double_word) str_接點訊息_high = "K0";
                    else DEVICE.KtoDoubleWord(str_接點訊息_low, ref str_接點訊息_low, ref str_接點訊息_high);
                }

                PointF p0_online = p1;
                p0_online.X = start_po.X + size.Width - 接點寬度 * 2;
                p0_online.Y -= 接點偏移位置_Y / 2;

                device.Get_Device(str_接點訊息_low, out value_low);
                device.Get_Device(str_接點訊息_high, out value_high);
                Int64 value = 0; ;
                if (double_word) myConvert.Int32轉Int64(ref value, (int)value_high, (int)value_low);
                else value = Convert.ToInt64(value_low);
                Draw.文字右中繪製(value.ToString(), p0_online, Online字體, Online顏色, Color.Transparent, g, 1, 1);
            }
            //----------------------------------------------------------------------------------------
            p3 = new PointF(p0.X + size.Width, p0.Y);
            p2 = new PointF(p3.X - 接點寬度, p1.Y);
            Draw.右方形括號繪製(p2, new Size(接點寬度, 接點高度), 線段顏色, 1, g, 1, 1);
            Draw.線段繪製(p2, p3, 線段顏色, 1, g, 1, 1);
        }

        private void draw_control_rect(Graphics g, Point start_po, Size size, bool 獲取焦點)
        {
            Color color = Color.FromArgb(255, 0, 0, 170);
            if (!獲取焦點) color = Color.LightGray;

            Point clear_po = start_po;
            Draw.方框繪製(new Point(clear_po.X + 2, clear_po.Y + 2), new Size(size.Width - 4, size.Height - 4), color, 2, false, g, 1, 1);
        }
        private void draw_comment(Graphics g, Point start_po, Size size, string str)
        {
            if (str != null && str != "")
            {
                char[] str_temp = str.ToCharArray();
                int index0 = 0;
                int index1 = 0;
                int index2 = 0;
                List<string> str_comment = new List<string>();
                //string[] str_comment = new string[註解列數];
                int str_len = 0;
                while (true)
                {
                    byte[] len = System.Text.Encoding.Default.GetBytes(str_temp[index0].ToString());
                    str_len = str_len + len.Length;
                    index0++;
                    index1++;
                    if (str_len > 註解一列半形字母數)
                    {
                        char[] _char = new char[index1 - 1];
                        int j = 0;
                        for (int i = index0 - index1; i < index0 - 1; i++)
                        {

                            _char[j] = str_temp[i];
                            j++;
                        }
                        string str_trmp = new string(_char);
                        str_comment.Add(str_trmp);
                        index2++;
                        index0--;
                        str_len = 0;
                        index1 = 0;
                    }

                    if (index0 >= str_temp.Length)
                    {
                        char[] _char = new char[index1];
                        int j = 0;
                        for (int i = index0 - index1; i < index0; i++)
                        {
                            _char[j] = str_temp[i];
                            j++;
                        }
                        string str_trmp = new string(_char);
                        str_comment.Add(str_trmp);
                        break;
                    }
                }
                PointF p0 = new PointF(start_po.X + 註解偏移位置_X, start_po.Y + 接點偏移位置_Y + 註解偏移位置_Y);
                if (str_comment != null)
                {
                    for (int i = 0; i < str_comment.Count; i++)
                    {
                        if (i < 註解列數)
                        {
                            float 文字高度 = g.MeasureString(str_comment[i], 註解字體).Height;
                            文字高度 -= 0;
                            Draw.文字左上繪製(str_comment[i], new PointF(p0.X, p0.Y + (i * 文字高度)), 註解字體, 註解文字顏色, Color.Transparent, g, 1, 1);
                        }
                    }
                }
            }
        }
        #endregion
        #region 操作功能函數
        void sub_插入一列(int 插入位置,bool 未編譯 ,bool 要記憶)
        {
            LADDER[] ladder_temp = new LADDER[一列格數];
            bool[] FLAG_豎線要補連接線 =new bool[一列格數];
            if (要記憶) sub_記憶上一步();
            for (int X = 0; X < 一列格數; X++)
            {
                if (ladder_list.Count > 1)
                {
                    if (ladder_list[插入位置][X].Vline_右上) FLAG_豎線要補連接線[X] = true;
                }
                if (X == 0) ladder_temp[X].ladderType = partTypeEnum.leftParenthesis;
                else if (X == (一列格數 - 1)) ladder_temp[X].ladderType = partTypeEnum.rightParenthesis;
                else ladder_temp[X].ladderType = partTypeEnum.noPart;             
            }
            
            ladder_list.Insert(插入位置, ladder_temp);
            for (int X = 0; X < 一列格數; X++)
            {
                if(FLAG_豎線要補連接線[X] )
                {
                    ladder_list[插入位置][X].Vline_右上 = true;
                    ladder_list[插入位置][X].Vline_右下 = true;
                }
            }
            Point p0 = new Point();
            Point p1 = new Point();
            p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, 插入位置), ladder_list);
            p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, 插入位置), ladder_list);
            for (int Y = p0.Y; Y <= p1.Y; Y++)
            {
                ladder_list[Y][1].未編譯 = 未編譯;
            }
   
        }
        void sub_插入一列(int 插入位置, LADDER[] ladder_temp, bool 要記憶)
        {
            if (要記憶) sub_記憶上一步();
            ladder_list.Insert(插入位置, ladder_temp);
   
        }
        void sub_刪除一列(int 刪除位置, bool 要記憶)
        {

            if (ladder_list.Count > 1 )
            {
                Point p0 = new Point();
                Point p1 = new Point();
                p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, 刪除位置), ladder_list);
                p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, 刪除位置), ladder_list);

                if (要記憶) sub_記憶上一步();

                for (int Y0 = p0.Y; Y0 <= p1.Y; Y0++)
                {
                    ladder_list[Y0][1].未編譯 = true;
                }

                if (刪除位置 - 1 >= 0)
                {
                    for (int X = 1; X < 一列格數 - 1; X++)
                    {
                        if (!ladder_list[刪除位置][X].Vline_右下)
                        {
                            ladder_list[刪除位置 - 1][X].Vline_右下 = false;
                        }
                        if ((刪除位置 + 1)<ladder_list.Count)
                        {
                            if (!ladder_list[刪除位置][X].Vline_右上)
                            {
                                ladder_list[刪除位置 + 1][X].Vline_右上 = false;
                            }
                        }
              

                    }
                }
                try
                {
                    if (ladder_list[刪除位置][一列格數 - 2].ladderType != partTypeEnum.EndPart)
                    {
                        ladder_list.RemoveAt(刪除位置);
                        p0 = new Point();
                        p1 = new Point();
                        p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, 刪除位置), ladder_list);
                        p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, 刪除位置), ladder_list);
                        if ((刪除位置) < (ladder_list.Count - 1))
                        {
                            for (int Y0 = p0.Y; Y0 <= p1.Y; Y0++)
                            {
                                ladder_list[Y0][1].未編譯 = true;
                            }
                        }

                        if (刪除位置 - 1 > 0)
                        {
                            p0 = new Point();
                            p1 = new Point();
                            p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, 刪除位置 - 1), ladder_list);
                            p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, 刪除位置 - 1), ladder_list);
                            for (int Y0 = p0.Y; Y0 <= p1.Y; Y0++)
                            {
                                ladder_list[Y0][1].未編譯 = true;
                            }
                        }
                    }
         
              
                }
                finally { }

            }
        
        }
        void sub_插入一元件()
        {
            sub_記憶上一步();
            Point index = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            LADDER[] ladder_temp = new LADDER[一列格數];

            Point p0 = new Point();
            Point p1 = new Point();
            p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, index.Y), ladder_list);
            p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, index.Y), ladder_list);
            int last_elelment = 0;
            for (int Y = p0.Y; Y <= p1.Y; Y++ )
            {
                ladder_list[Y][1].未編譯 = true;
                for (int X = 一列格數 - 2; X >= 0; X--)
                {
                    if (ladder_list[Y][X].ladderType != partTypeEnum.Data_no_Part)
                    {
                        last_elelment = X;
                        break;
                    }
                }
                for (int X = 0; X < 一列格數; X++)
                {
                    ladder_temp[X] = new LADDER(1);
                    if (X == 0) ladder_temp[X].ladderType = partTypeEnum.leftParenthesis;
                    else if (X == 一列格數 - 1) ladder_temp[X].ladderType = partTypeEnum.rightParenthesis;
                    else if (X >= last_elelment && X <= 一列格數 - 2) ladder_temp[X] = ladder_list[Y][X].DeepClone();

                }
                for (int X = index.X; X < last_elelment - 1; X++)
                {
                    ladder_temp[X + 1] = ladder_list[Y][X].DeepClone();
                    if (ladder_temp[X].ladderType != partTypeEnum.A_Part && ladder_temp[X].ladderType != partTypeEnum.B_Part)
                    {
                        if (X == 操作方框索引.X)
                        {
                            ladder_temp[X] = new LADDER(1);
                            if (ladder_list[Y][X].ladderType != partTypeEnum.noPart) ladder_temp[X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }

                }
                for (int X = index.X; X < 一列格數 - 2; X++)
                {
                    ladder_list[Y][X] = ladder_temp[X].DeepClone();                   
                }
            }

        

        }
        void sub_刪除一元件()
        {
            sub_記憶上一步();
            Point index = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            if (index.X != 0 && index.X != 一列格數 - 1 && index.Y != ladder_list.Count -1)
            {
                ladder_list[index.Y][index.X] = new LADDER(1);
                ladder_list[index.Y][index.X].未編譯 = true;         
            }         
        }
        void sub_上一步()
        {
            ladder_list = List_LADDER_All_Copy(ladder_list_上一步);
            ladder_list_buf = List_LADDER_All_Copy(ladder_list);                    
        }
        bool sub_複製()
        {
            bool FLAG = true;
            bool FLAG_整列窗選 = false;
            if (操作框窗選_初始位.X == 0 && 操作框窗選_位移量.X == 11) FLAG_整列窗選 = true;
            Point 現在位置 = 操作框窗選_初始位;
            if (ladder_list_Copy == null) ladder_list_Copy = new List<LADDER[]>();
            ladder_list_Copy.Clear();
            List<LADDER[]> ladder_list_Copy_buf = new List<LADDER[]>();
            List<LADDER> ladder_list_row = new List<LADDER>();
            Tx_操作框走訪方向 操作框走訪方向 = new Tx_操作框走訪方向();
            string 上一個左右執行指令 = "Add";
            string 上一個上下執行指令 = "Add";

            while (true)
            {
                if ((現在位置.X == 0 || 現在位置.X == 一列格數) && !FLAG_整列窗選) FLAG = false; //首列及末列不得複製
                if (現在位置.Y == ladder_list.Count - 1) FLAG = false; //END列不得複製
                if (!FLAG) break;

                bool FLAG_走訪完成 = false;
                sub_複製模式_檢查操作框走訪方向(操作框窗選_初始位, 操作框窗選_位移量, ref 現在位置, ref 操作框走訪方向);
                if (操作框走訪方向 == Tx_操作框走訪方向.左)
                {
                    ladder_list_row.Insert(0, ladder_list[現在位置.Y ][現在位置.X]);
                    上一個左右執行指令 = "Insert";
                }
                if (操作框走訪方向 == Tx_操作框走訪方向.右)
                {
                    ladder_list_row.Add(ladder_list[現在位置.Y ][現在位置.X]);
                    上一個左右執行指令 = "Add";
                }
                if (操作框走訪方向 == Tx_操作框走訪方向.下)
                {
                    if (上一個左右執行指令 == "Insert") ladder_list_row.Insert(0, ladder_list[現在位置.Y][現在位置.X]);
                    if (上一個左右執行指令 == "Add") ladder_list_row.Add(ladder_list[現在位置.Y  ][現在位置.X]);
                    ladder_list_Copy_buf.Add(ladder_list_row.ToArray());
                    ladder_list_row.Clear();
                    上一個上下執行指令 = "Add";
                }
                if (操作框走訪方向 == Tx_操作框走訪方向.上)
                {
                    if (上一個左右執行指令 == "Insert") ladder_list_row.Insert(0, ladder_list[現在位置.Y ][現在位置.X]);
                    if (上一個左右執行指令 == "Add") ladder_list_row.Add(ladder_list[現在位置.Y  ][現在位置.X]);
                    ladder_list_Copy_buf.Insert(0, ladder_list_row.ToArray());
                    ladder_list_row.Clear();
                    上一個上下執行指令 = "Insert";
                }
                if (操作框走訪方向 == Tx_操作框走訪方向.無)
                {
                    if (上一個左右執行指令 == "Insert") ladder_list_row.Insert(0, ladder_list[現在位置.Y ][現在位置.X]);
                    if (上一個左右執行指令 == "Add") ladder_list_row.Add(ladder_list[現在位置.Y ][現在位置.X]);
                    if (上一個上下執行指令 == "Insert") ladder_list_Copy_buf.Insert(0, ladder_list_row.ToArray());
                    if (上一個上下執行指令 == "Add") ladder_list_Copy_buf.Add(ladder_list_row.ToArray());
                    FLAG_走訪完成 = true;
                }

                sub_複製模式_操作框走訪(操作框窗選_初始位,ref 現在位置, 操作框走訪方向);

  
                if (FLAG_走訪完成) break;
            }
            ladder_list_Copy = List_LADDER_All_Copy(ladder_list_Copy_buf);

            foreach (LADDER[] list_array in ladder_list_Copy)
            {
                for (int i = 0; i < list_array.Length; i++)
                {
                    list_array[i].未編譯 = true;
                    if(ladder_list_Copy.Count ==1)
                    {
                        if(list_array.Length == 1)
                        {
                            list_array[i].Vline_右上 = false;
                            list_array[i].Vline_右下 = false;
                        }
                    }
                }     
            }

            if (!FLAG) ladder_list_Copy.Clear();
            else
            {
                FLAG_複製到剪貼簿 = true;
                while(true)
                {
                    if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                    {
                        Clipboard.SetData(DataFormats.Serializable, ladder_list_Copy);
                        FLAG_複製到剪貼簿 = false;
                    }
                    if (!FLAG_複製到剪貼簿) break;
                }
            }
            return FLAG;
        }
        bool sub_刪除()
        {
            sub_記憶上一步();
            bool FLAG = true;
            Point 現在位置 = 操作框窗選_初始位;
            List<LADDER> ladder_list_row = new List<LADDER>();
            Tx_操作框走訪方向 操作框走訪方向 = new Tx_操作框走訪方向();
            int 刪除個數 = 0;
            bool FLAG_整列窗選 = false;
            if (Math.Abs(操作框窗選_位移量.X) == 11) FLAG_整列窗選 = true;
            if (FLAG_整列窗選)
            {
                int Y_temp = 操作框窗選_初始位.Y;
                if ((操作框窗選_初始位.Y + 操作框窗選_位移量.Y) >= ladder_list.Count - 1)
                {
                    FLAG = false; //END列不得複製刪除
                    return FLAG;
                }
                for (int i = 0; i <= Math.Abs(操作框窗選_位移量.Y); i++)
                {
                    ladder_list.RemoveAt(Y_temp);
                }
                操作框窗選_初始位 = new Point();
                操作框窗選_位移量 = new Point();
                FLAG_有程式未編譯 = true;
            }
            else
            {
                while (true)
                {
                    if ((現在位置.X == 0 || 現在位置.X == 一列格數)) FLAG = false; //首列及末列不得刪除
                    if (現在位置.Y == ladder_list.Count - 1) FLAG = false; //END列不得複製刪除
                    if (!FLAG) break;

                    bool FLAG_走訪完成 = false;
                    sub_複製模式_檢查操作框走訪方向(操作框窗選_初始位, 操作框窗選_位移量, ref 現在位置, ref 操作框走訪方向);
                    if (操作框走訪方向 == Tx_操作框走訪方向.無) FLAG_走訪完成 = true;
                    int 連續刪除數量 = ladder_list[現在位置.Y][現在位置.X].元素數量;
                    for (int i = 0; i < 連續刪除數量; i++)
                    {
                        ladder_list[現在位置.Y][現在位置.X + i] = new LADDER(1);
                    }

                    Point p0 = new Point();
                    Point p1 = new Point();
                    p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, 現在位置.Y), ladder_list);
                    p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, 現在位置.Y), ladder_list);
                    for (int Y0 = p0.Y; Y0 <= p1.Y; Y0++)
                    {
                        ladder_list[Y0][1].未編譯 = true;
                    }

                    sub_複製模式_操作框走訪(操作框窗選_初始位, ref 現在位置, 操作框走訪方向);
                    刪除個數++;
                    FLAG_有程式未編譯 = true;
                    if (FLAG_走訪完成) break;
                }
            }       
            return FLAG;
        }
        bool sub_貼上()
        {
            sub_記憶上一步();

            FLAG_取得剪貼簿 = true;
            while (true)
            {
                if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                {
                    ladder_list_Copy = (List<LADDER[]>)Clipboard.GetData(DataFormats.Serializable);
                    FLAG_取得剪貼簿 = false;
                }
                if (!FLAG_取得剪貼簿) break;
            }

            bool FLAG = true;
            bool FLAG_整列窗選 = true;
            FLAG_有程式未編譯 = true;
            if (ladder_list_Copy == null) ladder_list_Copy = new List<LADDER[]>();
            for (int i = 0; i < ladder_list_Copy.Count; i++)
            {
                if (ladder_list_Copy[i].Length != 一列格數) FLAG_整列窗選 = false;
            }
            Point 起始位置 = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);

            if (FLAG_整列窗選)
            {
                起始位置.X = 0;
                for (int i = 0; i < ladder_list_Copy.Count; i++)
                {
                    sub_插入一列(起始位置.Y, true, false);
                }
            }
            else
            {
                if ((起始位置.X == 0 || 起始位置.X == 一列格數)) FLAG = false; //首列及末列不得貼上          
                if ((起始位置.Y + ladder_list_Copy.Count) > (ladder_list.Count - 1)) FLAG = false;
            }
            if (起始位置.Y == ladder_list.Count - 1) FLAG = false; //END列不得貼上
            if (FLAG)
            {
                for (int Y = 0; Y < ladder_list_Copy.Count; Y++)
                {
                    int 當列元素數量 = 0;
                    for (int i = 0; i < ladder_list_Copy[Y].Length; i++)
                    {
                        if (ladder_list_Copy[Y][i].ladderType != partTypeEnum.Data_no_Part) 當列元素數量 += ladder_list_Copy[Y][i].元素數量;

                    }
                    if ((起始位置.X + 當列元素數量 > 一列格數 - 1) && !FLAG_整列窗選)
                    {
                        FLAG = false;
                        return FLAG;
                    }
                    當列元素數量--;
                    bool 位置元素可貼上 = false;
                    if (ladder_list[起始位置.Y + Y][起始位置.X + 當列元素數量].ladderType == partTypeEnum.A_Part)
                    {
                        位置元素可貼上 = true;
                    }
                    if (ladder_list[起始位置.Y + Y][起始位置.X + 當列元素數量].ladderType == partTypeEnum.B_Part)
                    {
                        位置元素可貼上 = true;
                    }
                    if (ladder_list[起始位置.Y + Y][起始位置.X + 當列元素數量].ladderType == partTypeEnum.noPart)
                    {
                        位置元素可貼上 = true;
                    }
                    if (ladder_list[起始位置.Y + Y][起始位置.X + 當列元素數量].ladderType == partTypeEnum.H_Line_Part)
                    {
                        位置元素可貼上 = true;
                    }
                    if (ladder_list[起始位置.Y + Y][起始位置.X + 當列元素數量].ladderType == partTypeEnum.OUT_Part)
                    {
                        位置元素可貼上 = true;
                    }
                    if (ladder_list[起始位置.Y + Y][起始位置.X + 當列元素數量].ladderType == partTypeEnum.Data_no_Part)
                    {
                        if (ladder_list[起始位置.Y + Y][起始位置.X + 當列元素數量 + 1].ladderType != partTypeEnum.Data_no_Part)
                        {
                            位置元素可貼上 = true;
                        }

                    }
                    if (!FLAG_整列窗選) FLAG = 位置元素可貼上;
                    if (!FLAG)
                    {
                        return FLAG;
                    }

                }
            }

            if (FLAG || FLAG_整列窗選)
            {
                for (int Y = 0; Y < ladder_list_Copy.Count; Y++)
                {
                    for (int X = 0; X < ladder_list_Copy[Y].Length; X++)
                    {
                        if ((起始位置.X + ladder_list_Copy[Y].Length > 一列格數 - 1) && !FLAG_整列窗選)
                        {
                            FLAG = false;
                            return FLAG;
                        }
                        if (FLAG)
                        {
                            if (ladder_list_Copy.Count == 1)
                            {
                                if ((起始位置.X + ladder_list_Copy[Y][X].元素數量 > 一列格數 - 1) && !FLAG_整列窗選)
                                {
                                    FLAG = false;
                                    return FLAG;
                                }
                                else
                                {
                                    ladder_list[起始位置.Y + Y][起始位置.X + X].ladderType = ladder_list_Copy[Y][X].ladderType;
                                    for (int i = 0; i < ladder_list_Copy[Y][X].元素數量; i++)
                                    {
                                        if (ladder_list_Copy[Y][X].元素數量 <= 1) ladder_list[起始位置.Y + Y][起始位置.X + X].ladderParam[i] = ladder_list_Copy[Y][X].ladderParam[i];
                                        else ladder_list[起始位置.Y + Y][起始位置.X + X] = ladder_list_Copy[Y][X];
                                        if (i != 0)
                                        {
                                            ladder_list[起始位置.Y + Y][起始位置.X + X + i] = new LADDER(1);
                                            ladder_list[起始位置.Y + Y][起始位置.X + X + i].ladderType = partTypeEnum.Data_no_Part;
                                        }
                                    }

                                }

                            }
                            else
                            {
                                if ((起始位置.X + ladder_list_Copy[Y][X].元素數量 > 一列格數 - 1) && !FLAG_整列窗選)
                                {
                                    FLAG = false;
                                    return FLAG;
                                }
                                else
                                {
                                    for (int i = 0; i < ladder_list_Copy[Y][X].元素數量; i++)
                                    {
                                        if (i == 0) ladder_list[起始位置.Y + Y + i][起始位置.X + X + i] = ladder_list_Copy[Y][X + i];
                                        else
                                        {
                                            ladder_list[起始位置.Y + Y][起始位置.X + X + i] = new LADDER(1);
                                            ladder_list[起始位置.Y + Y][起始位置.X + X + i].ladderType = partTypeEnum.Data_no_Part;
                                        }
                                    }
                                }
                            }
                        }
                        Point p0 = new Point();
                        Point p1 = new Point();
                        p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, 起始位置.Y + Y), ladder_list);
                        p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, 起始位置.Y + Y), ladder_list);
                        for (int Y0 = p0.Y; Y0 <= p1.Y; Y0++)
                        {
                            ladder_list[Y0][1].未編譯 = true;
                        }
                    }

                }
            }

     
           
            return FLAG;
        }
        void sub_畫橫線(Point index)
        {
            
            if (index.X != 0)
            {
                sub_記憶上一步();
                ladder_list[index.Y][index.X].ladderType = partTypeEnum.H_Line_Part;
                ladder_list[index.Y][index.X].未編譯 = true;
            }
            Point p0 = new Point();
            Point p1 = new Point();
            p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, index.Y), ladder_list);
            p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, index.Y), ladder_list);
            for (int Y = p0.Y; Y <= p1.Y; Y++)
            {
                ladder_list[Y][1].未編譯 = true;
            }

        }
        void sub_畫豎線(Point index)
        {
        
            if (index.X != 0 && index.X != 1)
            {
                sub_記憶上一步();
                if ((index.Y + 1) < ladder_list.Count)
                {
                    
                    ladder_list[index.Y][index.X - 1].Vline_右下 = true;
                    ladder_list[index.Y + 1][index.X - 1].Vline_右上 = true;
                    ladder_list[index.Y][index.X - 1].未編譯 = true;
                    ladder_list[index.Y + 1][index.X - 1].未編譯 = true;
                }  
            }
            Point p0 = new Point();
            Point p1 = new Point();
            p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, index.Y), ladder_list);
            p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, index.Y), ladder_list);
            for (int Y = p0.Y; Y <= p1.Y; Y++)
            {
                ladder_list[Y][1].未編譯 = true;
            }
      
        }
        void sub_刪除豎線(Point index)
        {
            if (index.X != 0 && index.X != 1)
            {
                sub_記憶上一步();
                if ((index.Y + 1) < ladder_list.Count)
                {
                    ladder_list[index.Y][index.X - 1].Vline_右下 = false;
                    ladder_list[index.Y + 1][index.X - 1].Vline_右上 = false;
                    ladder_list[index.Y][index.X - 1].未編譯 = true;
                    ladder_list[index.Y + 1][index.X - 1].未編譯 = true;
                }
            }
            Point p0 = new Point();
            Point p1 = new Point();
            p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, index.Y), ladder_list);
            p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, index.Y), ladder_list);
            for (int Y = p0.Y; Y <= p1.Y; Y++)
            {
                ladder_list[Y][1].未編譯 = true;
            }
            
        }
        void sub_刪除橫線(Point index)
        {
            if (index.X != 0 && index.X != 1)
            {
                sub_記憶上一步();
                if ((index.Y + 1) < ladder_list.Count)
                {
                    ladder_list[index.Y][index.X].ladderType = partTypeEnum.noPart;
                    ladder_list[index.Y][index.X].未編譯 = true;
                }
            }
            Point p0 = new Point();
            Point p1 = new Point();
            p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, index.Y), ladder_list);
            p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, index.Y), ladder_list);
            for (int Y = p0.Y; Y <= p1.Y; Y++)
            {
                ladder_list[Y][1].未編譯 = true;
            }
      
        }
        void sub_記憶上一步()
        {
            ladder_list_上一步 = List_LADDER_All_Copy(ladder_list);
            FLAG_專案已儲存 = false;
        }
        void sub_初始化(ref bool FLAG)
        {
            if (!FLAG)
            {
                ladder_list.Clear();
      
                for (int Y = 0; Y < 1; Y++)
                {
                    sub_插入一列(0, false, false);
                    for (int X = 0; X < 一列格數; X++)
                    {
                        if (Y == 0)
                        {
                            if (X == 0)
                            {
                                ladder_list[Y][X].ladderParam[0] = "0";
                                string[] str_temp = new string[1];
                                str_temp[0] = "END";
                                IL指令程式.Add(str_temp);
                            }
                            if (X > 0)
                            {
                                if (X < (一列格數 - 2))
                                {
                                    ladder_list[Y][X].ladderType = partTypeEnum.H_Line_Part;
                                }
                                if (X == (一列格數 - 2)) ladder_list[Y][X].ladderType = partTypeEnum.EndPart;
                            }
                        }
                    }

                }
                ladder_list_上一步 = List_LADDER_All_Copy(ladder_list);
                ladder_list_buf = List_LADDER_All_Copy(ladder_list);
                IL指令程式.Clear();
                device.Clear_Comment();
                TAB目錄.Clear();
                CallBackUI.treeview.清空節點(treeView_程式分頁);
                string[] str_temp0 = new string[1];
                str_temp0[0] = "END";
                IL指令程式.Add(str_temp0);
                cnt_ListBox_IL指令集更新 = 1;
                cnt_DataTable_初始化 = 1;
                CallBackUI.panel.取得焦點(panel_LADDER);
            }

            FLAG = true;
        }
        #endregion
        #region 系統功能函數
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //FLAG_編譯視窗獲取焦點
            switch (keyData)
            {
                case System.Windows.Forms.Keys.Up:
                    //UpKey();
                    if (FLAG_編譯視窗獲取焦點) return true;
                    break;
                case System.Windows.Forms.Keys.Down:
                    //DownKey();
                    if (FLAG_編譯視窗獲取焦點) return true;
                    break;
                case System.Windows.Forms.Keys.Left:
                    //LeftKey();
                    if (FLAG_編譯視窗獲取焦點) return true;
                    break;
                case System.Windows.Forms.Keys.Right:
                    //RightKey();
                    if (FLAG_編譯視窗獲取焦點) return true;
                    break;
                case System.Windows.Forms.Keys.LShiftKey:
                    return true;
                case System.Windows.Forms.Keys.ShiftKey:
                    return true;
                case System.Windows.Forms.Keys.Shift:
                    return true;
                case System.Windows.Forms.Keys.F10:
                    return true;
              /*  case System.Windows.Forms.Keys.Enter:
                    return true;*/
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        static public void PasteTo_Ladderlist(LADDER ladder , int Y , int X)
        {
            ladder_list[Y][X].ladderType = ladder.ladderType;
            for (int k = 0; k <= ladder.元素數量; k++)
            {
                if (k <= ladder.元素數量) ladder_list[Y][X].ladderParam[k] = ladder.ladderParam[k];
            }
            ladder_list[Y][X].Vline_右上 = ladder.Vline_右上;
            ladder_list[Y][X].Vline_右下 = ladder.Vline_右下;
            ladder_list[Y][X].Vline_左上 = ladder.Vline_左上;
            ladder_list[Y][X].Vline_左下 = ladder.Vline_左下;

            ladder_list[Y][X].未編譯 = ladder.未編譯;
        }
        List<LADDER[]> List_LADDER_All_Copy(List<LADDER[]> org)
        {
            /* List<LADDER[]> list = new List<LADDER[]>();
             foreach (LADDER[] list_array in org)
             {
                 LADDER[] array = new LADDER[list_array.Length];
                 for (int i = 0; i < list_array.Length; i++)
                 {
                     array[i] = new LADDER(1);
                     array[i] = list_array[i];
                 }
                 list.Add(array);
             }*/
            List<LADDER[]> list = new List<LADDER[]>();
            foreach (LADDER[] list_array in org)
            {
                LADDER[] array = new LADDER[list_array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = new LADDER(1);
                    array[i].ladderType = list_array[i].ladderType;
                    for (int k = 0; k <= list_array[i].元素數量; k++)
                    {
                        if (k <= list_array[i].元素數量) array[i].ladderParam[k] = list_array[i].ladderParam[k];
                    }
                    array[i].Vline_右上 = list_array[i].Vline_右上;
                    array[i].Vline_右下 = list_array[i].Vline_右下;
                    array[i].Vline_左上 = list_array[i].Vline_左上;
                    array[i].Vline_左下 = list_array[i].Vline_左下;

                    array[i].未編譯 = list_array[i].未編譯;
                }
                list.Add(array);
            }

            return list;
        }
        List<String[]> List_IL_All_Copy(List<String[]> org)
        {
            List<String[]> list = new List<String[]>();
            foreach (String[] list_array in org)
            {
                String[] array = new String[list_array.Length];
                for (int i = 0; i < list_array.Length; i++)
                {
                    array[i] = "";
                    array[i] = list_array[i];
                }
                list.Add(array);
            }
            return list;
        }
        void sub_鼠線窗選畫面寫入()
        {
            if (操作框窗選_位移量.X != 0 || 操作框窗選_位移量.Y != 0)
            {
                if (窗選模式 == Tx_窗選模式.鼠線模式)
                {
                    sub_記憶上一步();
                    int 起始_X = 0;
                    int 起始_Y = 0;
                    int 結束_X = 0;
                    int 結束_Y = 0;
                    int 要畫橫線列數 = 0;
                    int 要畫豎線行豎數 = 0;
                    #region 檢查起始與結束位
                    if (操作框窗選_位移量.Y > 0)//往下畫鼠線
                    {
                        起始_Y = 操作框窗選_初始位.Y;
                        結束_Y = 操作框窗選_初始位.Y + 操作框窗選_位移量.Y;
                        要畫橫線列數 = 結束_Y;

                        if (操作框窗選_位移量.X < 0)
                        {
                            起始_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            結束_X = 操作框窗選_初始位.X;
                            要畫豎線行豎數 = 結束_X;
                        }
                        if (操作框窗選_位移量.X >= 0)
                        {
                            起始_X = 操作框窗選_初始位.X;
                            結束_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            要畫豎線行豎數 = 起始_X;
                        }
                    }
                    if (操作框窗選_位移量.Y < 0)//往上畫鼠線
                    {

                        if (操作框窗選_位移量.X < 0)
                        {
                            起始_Y = 操作框窗選_初始位.Y + 操作框窗選_位移量.Y;
                            結束_Y = 操作框窗選_初始位.Y;
                            要畫橫線列數 = 結束_Y;

                            起始_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            結束_X = 操作框窗選_初始位.X;
                            要畫豎線行豎數 = 起始_X;
                        }
                        if (操作框窗選_位移量.X > 0)
                        {
                            起始_Y = 操作框窗選_初始位.Y + 操作框窗選_位移量.Y;
                            結束_Y = 操作框窗選_初始位.Y;
                            要畫橫線列數 = 結束_Y;

                            起始_X = 操作框窗選_初始位.X;
                            結束_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            要畫豎線行豎數 = 結束_X;
                        }
                    }
                    if (操作框窗選_位移量.Y == 0)//畫水平線
                    {
                        起始_Y = 操作框窗選_初始位.Y + 操作框窗選_位移量.Y;
                        結束_Y = 操作框窗選_初始位.Y;
                        要畫橫線列數 = 結束_Y;

                        if (操作框窗選_位移量.X < 0)
                        {
                            起始_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            結束_X = 操作框窗選_初始位.X;
                            要畫豎線行豎數 = 結束_X;
                        }
                        if (操作框窗選_位移量.X > 0)
                        {
                            起始_X = 操作框窗選_初始位.X;
                            結束_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            要畫豎線行豎數 = 結束_X;
                        }
                    }
                    if (操作框窗選_位移量.X == 0)//畫豎線
                    {

                        if (操作框窗選_位移量.Y < 0)
                        {
                            起始_Y = 操作框窗選_初始位.Y + 操作框窗選_位移量.Y;
                            結束_Y = 操作框窗選_初始位.Y;

                            起始_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            結束_X = 操作框窗選_初始位.X;
                            要畫豎線行豎數 = 結束_X;
                            要畫橫線列數 = 結束_Y;
                        }
                        if (操作框窗選_位移量.Y > 0)
                        {
                            起始_Y = 操作框窗選_初始位.Y;
                            結束_Y = 操作框窗選_初始位.Y + 操作框窗選_位移量.Y;

                            起始_X = 操作框窗選_初始位.X + 操作框窗選_位移量.X;
                            結束_X = 操作框窗選_初始位.X;
                            要畫豎線行豎數 = 起始_X;
                            要畫橫線列數 = 起始_Y;
                        }
                    }
                    #endregion
                    for (int Y = 起始_Y; Y <= 結束_Y; Y++)
                    {
                        for (int X = 起始_X; X <= 結束_X; X++)
                        {
                            #region 線判斷
                            LADDER ladder_temp = new LADDER(1);

                            bool Vline_左上 = false;
                            bool Vline_左下 = false;
                            Point 繪製位置 = new Point(X, Y);
                            if (操作框窗選_位移量.X == 0)//只有Y移動
                            {
                                Vline_左上 = true;
                                Vline_左下 = true;
                                if (操作框窗選_位移量.Y > 0)
                                {
                                    if (Y == 起始_Y)
                                    {
                                        Vline_左上 = false;
                                        Vline_左下 = true;
                                    }
                                    if (Y == 結束_Y)
                                    {
                                        Vline_左上 = true;
                                        Vline_左下 = false;
                                    }
                                }
                                if (操作框窗選_位移量.Y < 0)
                                {
                                    if (Y == 結束_Y)
                                    {
                                        Vline_左上 = true;
                                        Vline_左下 = false;
                                    }
                                    if (Y == 起始_Y)
                                    {
                                        Vline_左上 = false;
                                        Vline_左下 = true;
                                    }
                                }

                            }
                            else if (操作框窗選_位移量.Y == 0)//只有X移動
                            {
                                if (X != 結束_X) ladder_temp.ladderType = partTypeEnum.H_Line_Part;
                            }
                            else if (操作框窗選_位移量.X < 0 && 操作框窗選_位移量.Y > 0)//左下
                            {
                                if (X == 要畫豎線行豎數)
                                {
                                    Vline_左上 = true;
                                    Vline_左下 = true;
                                    if (Y == 起始_Y)
                                    {
                                        Vline_左上 = false;
                                        Vline_左下 = true;
                                    }
                                    if (Y == 結束_Y)
                                    {
                                        Vline_左上 = true;
                                        Vline_左下 = false;
                                    }
                                }
                                else if (Y == 要畫橫線列數)
                                {
                                    ladder_temp.ladderType = partTypeEnum.H_Line_Part;
                                }
                            }
                            else if (操作框窗選_位移量.X > 0 && 操作框窗選_位移量.Y > 0)//右下
                            {
                                if (X == 要畫豎線行豎數)
                                {
                                    Vline_左上 = true;
                                    Vline_左下 = true; ;
                                    if (Y == 起始_Y)
                                    {
                                        Vline_左上 = false;
                                        Vline_左下 = true;
                                    }
                                    if (Y == 結束_Y)
                                    {
                                        Vline_左上 = true;
                                        Vline_左下 = false;
                                    }
                                }
                                if (Y == 要畫橫線列數)
                                {
                                    ladder_temp.ladderType = partTypeEnum.H_Line_Part;
                                }
                            }
                            else if (操作框窗選_位移量.X < 0 && 操作框窗選_位移量.Y < 0)//左上
                            {

                                if (Y == 要畫橫線列數)
                                {
                                    if (X != 結束_X) ladder_temp.ladderType = partTypeEnum.H_Line_Part;
                                }
                                if (X == 要畫豎線行豎數)
                                {
                                    Vline_左上 = true;
                                    Vline_左下 = true;
                                    if (Y == 結束_Y)
                                    {
                                        Vline_左上 = true;
                                        Vline_左下 = false;
                                    }
                                    if (Y == 起始_Y)
                                    {
                                        Vline_左上 = false;
                                        Vline_左下 = true;
                                    }
                                }
                            }
                            else if (操作框窗選_位移量.X > 0 && 操作框窗選_位移量.Y < 0)//右上
                            {

                                if (Y == 要畫橫線列數)
                                {
                                    if (X != 結束_X) ladder_temp.ladderType = partTypeEnum.H_Line_Part;
                                }
                                if (X == 要畫豎線行豎數)
                                {
                                    Vline_左上 = true;
                                    Vline_左下 = true;
                                    if (Y == 起始_Y)
                                    {
                                        Vline_左上 = false;
                                        Vline_左下 = true;
                                    }
                                    if (Y == 結束_Y)
                                    {
                                        Vline_左上 = true;
                                        Vline_左下 = false;
                                    }
                                }
                            }
                            #endregion
                            if (繪製位置.X > 0)
                            {
                                if (繪製位置.Y != ladder_list.Count - 1)
                                {
                                    if (ladder_list[繪製位置.Y][繪製位置.X].ladderType == partTypeEnum.noPart)
                                    {
                                        if (ladder_temp.ladderType == partTypeEnum.H_Line_Part)
                                            ladder_list[繪製位置.Y][繪製位置.X].ladderType = partTypeEnum.H_Line_Part;
                                    }
                                }
                            }
                            if (Vline_左上 || Vline_左下)
                            {
                                if (繪製位置.Y != ladder_list.Count - 1)
                                {
                                    if (繪製位置.X - 1 > 0)
                                    {
                                        if (!ladder_list[繪製位置.Y][繪製位置.X - 1].Vline_右上) ladder_list[繪製位置.Y][繪製位置.X - 1].Vline_右上 = Vline_左上;
                                        if (!ladder_list[繪製位置.Y][繪製位置.X - 1].Vline_右下) ladder_list[繪製位置.Y][繪製位置.X - 1].Vline_右下 = Vline_左下;
                                    }
                                }
                            }


                            Point p0 = new Point();
                            Point p1 = new Point();
                            p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, Y), ladder_list);
                            p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, Y), ladder_list);
                            for (int Y0 = p0.Y; Y0 <= p1.Y; Y0++)
                            {
                                ladder_list[Y0][1].未編譯 = true;
                            }

                        }
                    }

                    操作框窗選_位移量 = new Point();
                }

            }

        }
        void sub_備份已編譯程式()
        {
            if (FLAG_有程式未編譯 != FLAG_有程式未編譯_buf)
            {
                if (!FLAG_有程式未編譯)
                {
                    ladder_list_備份 = List_LADDER_All_Copy(ladder_list);
                }
                FLAG_有程式未編譯_buf = FLAG_有程式未編譯;

            }
        }
        void sub_未編譯檢查()
        {
            if (T6_未編譯檢查)
            {
                bool FLAG_有程式未編譯_buf = false;
             
                    for (int Y = 0; Y < ladder_list.Count; Y++)
                    {
                        for (int X = 0; X < 一列格數; X++)
                        {
                            if (ladder_list[Y][X].未編譯)
                            {
                                Point p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, Y), ladder_list);
                                Point p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, Y), ladder_list);
                                for (int i = p0.Y; i <= p1.Y; i++)
                                {
                                    for (int k = 0; k < 一列格數; k++)
                                    {
                                        ladder_list[i][k].未編譯 = true;
                                        FLAG_有程式未編譯_buf = true;
                                        FLAG_有程式未編譯 = true;
                                    }
                                    if (i >= 未編譯範圍最大值) 未編譯範圍最大值 = i;
                                    if (i <= 未編譯範圍最小值) 未編譯範圍最小值 = i;
                                }
                                Y = p1.Y;
                                X = 一列格數;
                            }
                        }
                    }

                
                FLAG_有程式未編譯 = FLAG_有程式未編譯_buf;
                device_system.Set_Device("T6", 30);
                T6_未編譯檢查 = false;
            }
        }
        Point sub_搜尋指定位置階梯圖右上節點位置(Point p0, List<LADDER[]> list)
        {
            Point p0_temp = p0;
            while (true)
            {
                if (p0_temp.Y - 1 >= 0 && p0_temp.Y < list.Count)
                {
                    if (p0_temp.X > 一列格數 - 2) break;
                    if (list[p0_temp.Y][p0_temp.X].Vline_右上 && list[p0_temp.Y - 1][p0_temp.X].Vline_右下)
                    {
                        p0_temp.Y--;
                        p0_temp.X = p0.X;
                    }
                    else p0_temp.X++;
                }
                else break;
            }
            return p0_temp;
        }
        Point sub_搜尋指定位置階梯圖右下節點位置(Point p0, List<LADDER[]> list)
        {
            Point p0_temp = p0;
            while (true)
            {
                if (p0_temp.Y + 1 >= list.Count) break;
                if (p0_temp.X >= 一列格數 - 2) break;

                if (list[p0_temp.Y][p0_temp.X].Vline_右下 && list[p0_temp.Y + 1][p0_temp.X].Vline_右上)
                {
                    p0_temp.Y++;
                    p0_temp.X = p0.X;
                }
                else p0_temp.X++;

            }
            return p0_temp;
        }
        void sub_LADDER編譯區顯示大小檢查()
        {
            Point p0 = new Point();
            Size size = new System.Drawing.Size();
            p0.X = panel_程式分頁.Location.X + panel_程式分頁.Width;
            p0.Y = panel_工具箱.Location.Y + panel_工具箱.Size.Height;

            size.Width = panel_Datagrid_註解列表.Location.X - p0.X;
            size.Height = panel_Datagrid_註解列表.Size.Height - (p0.Y - panel_Datagrid_註解列表.Location.Y);
            if (size.Width > 0 && size.Height > 0 && p0.X >= 0 && p0.Y > 0)
            {
                if ((panel_LADDER.Location.X != p0.X) || (panel_LADDER.Location.Y != p0.Y) || (panel_LADDER.Size.Width != size.Width) || (panel_LADDER.Size.Height != size.Height))
                {
                    panel_LADDER.Location = p0;
                    panel_LADDER.Size = size;
                    graphics_init = false;
                    操作方框大小.Width = (int)(panel_LADDER.Width / (float)一列格數);
                    操作方框大小.Height = (int)(panel_LADDER.Height / (float)一個畫面列數);
                }
            }

        }
        void sub_顯示還原回預設值()
        {
            this.一列格數 = 12;
            this.接點偏移位置_Y = 40;
            this.註解一列半形字母數 = 8;
            this.註解列數 = 2;
            this.註解偏移位置_X = 5;
            this.註解偏移位置_Y = 10;
            this.接點寬度 = 15;
            this.接點高度 = 20;
            this.註解字體 = new Font("微軟正黑體", 14);
            this.註解文字顏色 = Color.Red;
            this.Online顏色 = Color.FromArgb(255, 0, 80, 255);
            this.視窗字體 = new Font("Courier New", 14);
            this.Online字體 = new Font("標楷體", 12);
            graphics_init = false;
            操作方框大小.Width = (int)(panel_LADDER.Width / (float)一列格數);
            操作方框大小.Height = (int)(panel_LADDER.Height / (float)一個畫面列數);
        }

        public static SystemProperties systemProperties = new SystemProperties();
        [Serializable]
        public class SystemProperties
        {
            public int 註解列數;
            public int 註解一列半形字母數;
            public Font 註解字體;
            public Color 註解文字顏色;
            public Color Online顏色;
            public Font Online字體;
            public Font 視窗字體;
         
        }
        private void SaveSystemProperties()
        {
            IFormatter binFmt = new BinaryFormatter();
            Stream stream = null;
            systemProperties.註解列數 = this.註解列數;
            systemProperties.註解一列半形字母數 = this.註解一列半形字母數;
            systemProperties.註解字體 = this.註解字體;
            systemProperties.註解文字顏色 = this.註解文字顏色;
            systemProperties.Online顏色 = this.Online顏色;
            systemProperties.Online字體 = this.Online字體;
            systemProperties.視窗字體 = this.視窗字體;
           
            try
            {
                stream = File.Open("properties.sys", FileMode.Create);
                binFmt.Serialize(stream, systemProperties);
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }
        private void LoadSystemProperties()
        {
            IFormatter binFmt = new BinaryFormatter();
            Stream stream = null;
            try
            {
                if (File.Exists(".\\properties.sys"))
                {
                    stream = File.Open(".\\properties.sys", FileMode.Open);
                    try { systemProperties = (SystemProperties)binFmt.Deserialize(stream); }
                    catch { }
                }
             
            }
            finally
            {
                if (stream != null) stream.Close();
            }
            int LoadError = 0;
            if (systemProperties == null) LoadError++;
            if (systemProperties.註解列數 == 0) LoadError++;
            if (systemProperties.註解一列半形字母數 == 0) LoadError++;
            if (systemProperties.註解字體 == null) LoadError++;
            if (systemProperties.註解文字顏色.Name == "0") LoadError++;
            if (systemProperties.Online顏色.Name == "0") LoadError++;
            if (systemProperties.Online字體 == null) LoadError++;
            if (systemProperties.視窗字體 == null) LoadError++;
            if (LoadError == 0)
            {
                this.註解列數 = systemProperties.註解列數;
                this.註解一列半形字母數 = systemProperties.註解一列半形字母數;
                this.註解字體 = systemProperties.註解字體;
                this.註解文字顏色 = systemProperties.註解文字顏色;
                this.Online顏色 = systemProperties.Online顏色;
                this.Online字體 = systemProperties.Online字體;
                this.視窗字體 = systemProperties.視窗字體;
            }
            else
            {
                sub_顯示還原回預設值();
            }
            toolStripTextBox_註解列數.Text = 註解列數.ToString();
            toolStripTextBox_註解字母數.Text = 註解一列半形字母數.ToString();
        }
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSystemProperties();
            TransferSetup.close();
        }
        #endregion
        #region 儲存檔案
        bool sub_儲存檔案(String str)
        {
            bool FLAG = false;
            P_基本參數.Program = List_IL_All_Copy(IL指令程式);
            P_基本參數.Comment = device.Get_Comment();
            IFormatter binFmt = new BinaryFormatter();
            try
            {
                Stream s = File.Open(str, FileMode.Create);
                binFmt.Serialize(s, P_基本參數);
                s.Close();
                FLAG = true;
            }
            catch
            {
                FLAG = false;
            }
            return FLAG;
        }
        #endregion
        #region 讀取檔案
        public static 基本參數 P_基本參數 = new 基本參數();
        [Serializable]
        public class 基本參數
        {
            public List<String[]> Program = new List<string[]>();
            public List<String[]> Comment = new List<string[]>();
        }
        public List<String[]> IL指令程式_讀取buf = new List<string[]>();
        byte cnt_讀取檔案 = 255;
        String str_讀取檔案彈出視窗文字 = "";
        void sub_讀取檔案()
        {
            if (cnt_讀取檔案 == 1) cnt_讀取檔案_00_開始讀取(ref cnt_讀取檔案);
            if (cnt_讀取檔案 == 2) cnt_讀取檔案 = 10;

            if (cnt_讀取檔案 == 10) cnt_讀取檔案_10_反編譯前初始化(ref cnt_讀取檔案);
            if (cnt_讀取檔案 == 11) cnt_讀取檔案_10_檢查反編譯_READY(ref cnt_讀取檔案);
            if (cnt_讀取檔案 == 12) cnt_讀取檔案_10_執行反編譯(ref cnt_讀取檔案);
            if (cnt_讀取檔案 == 13) cnt_讀取檔案_10_檢查反編譯結果(ref cnt_讀取檔案);
            if (cnt_讀取檔案 == 14) cnt_讀取檔案 = 20;

            if (cnt_讀取檔案 == 20) cnt_讀取檔案_20_編譯前初始化(ref cnt_讀取檔案);
            if (cnt_讀取檔案 == 21) cnt_讀取檔案_20_檢查編譯_READY(ref cnt_讀取檔案);
            if (cnt_讀取檔案 == 22) cnt_讀取檔案_20_檢查編譯_OVER(ref cnt_讀取檔案);
            if (cnt_讀取檔案 == 23) cnt_讀取檔案_20_檢查編譯結果(ref cnt_讀取檔案);
            if (cnt_讀取檔案 == 24) cnt_讀取檔案_20_記憶上一步(ref cnt_讀取檔案);
            if (cnt_讀取檔案 == 25) cnt_讀取檔案 = 150;

            if (cnt_讀取檔案 == 150) cnt_讀取檔案_150_讀取成功(ref cnt_讀取檔案);
            if (cnt_讀取檔案 == 151) cnt_讀取檔案 = 240;

            if (cnt_讀取檔案 == 200) cnt_讀取檔案_200_讀取失敗(ref cnt_讀取檔案);
            if (cnt_讀取檔案 == 201) cnt_讀取檔案 = 240;

            if (cnt_讀取檔案 == 240) cnt_讀取檔案_240_彈出視窗(ref cnt_讀取檔案);
            if (cnt_讀取檔案 == 241) cnt_讀取檔案 = 255;
        }
        void cnt_讀取檔案_00_開始讀取(ref byte cnt)
        {         
            if (openFileDialog_LOAD.ShowDialog(this) == DialogResult.OK)
            {
                bool 讀取成功 = false;
                Str_讀取位置 = openFileDialog_LOAD.FileName;
                讀取成功 = 讀取檔案(Str_讀取位置);
                if (讀取成功)
                {
                    // MessageBox.Show("讀取專案成功!", "檔案資訊", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cnt++;
                }
                else
                {
                    str_讀取檔案彈出視窗文字 = "檔案開啟失敗!";
                    cnt = 200;
                    //MessageBox.Show("讀取專案失敗!", "檔案資訊", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            else
            {
                cnt = 255;
            }
        
        }
        void cnt_讀取檔案_10_反編譯前初始化(ref byte cnt)
        {
            程式編譯_IL轉換為階梯圖_輸入參數.IL指令程式.Clear();
            foreach (string[] array_str in IL指令程式_讀取buf)
            {
                string[] str_temp = new string[array_str.Length];
                for (int i = 0; i < str_temp.Length; i++)
                {
                    str_temp[i] = array_str[i];
                }
                程式編譯_IL轉換為階梯圖_輸入參數.IL指令程式.Add(str_temp);
            }
            程式編譯_IL轉換為階梯圖_輸入參數.彈出視窗要顯示 = false;
            cnt++;
        }
        void cnt_讀取檔案_10_檢查反編譯_READY(ref byte cnt)
        {
            if (cnt_程式編譯_IL轉換為階梯圖 ==255)
            {
                cnt_程式編譯_IL轉換為階梯圖 = 1;
                cnt++;
            }
  
        }
        void cnt_讀取檔案_10_執行反編譯(ref byte cnt)
        {
            sub_程式編譯_IL轉換為階梯圖();
            if (cnt_程式編譯_IL轉換為階梯圖 == 255) cnt++;
       
        }
        void cnt_讀取檔案_10_檢查反編譯結果(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖_輸出參數.轉換NG)
            {
                str_讀取檔案彈出視窗文字 = "IL指令轉換異常!";
                cnt = 200;
            }
            else
            {
                ladder_list = List_LADDER_All_Copy(程式編譯_IL轉換為階梯圖_輸出參數.list);
                cnt++;
            }
        }
        void cnt_讀取檔案_20_編譯前初始化(ref byte cnt)
        {
            程式編譯_輸入參數.彈出視窗要顯示 = false;
            IL指令程式.Clear();
            cnt++;
        }
        void cnt_讀取檔案_20_檢查編譯_READY(ref byte cnt)
        {

            if (cnt_程式編譯 == 255)
            {
                cnt_程式編譯 = 1;
                cnt++;
            }
        }
        void cnt_讀取檔案_20_檢查編譯_OVER(ref byte cnt)
        {
            //sub_程式編譯();
            if (cnt_程式編譯 == 255)
            {
                cnt++;
            }
            
        }
        void cnt_讀取檔案_20_檢查編譯結果(ref byte cnt)
        {
              cnt++;
            return;
            bool 編譯與反編譯IL指令相符 = true;
            if (編譯與反編譯IL指令相符)
            {
                if (程式編譯_輸出參數.IL指令程式.Count != IL指令程式.Count) 編譯與反編譯IL指令相符 = false;
            }
     
            if (編譯與反編譯IL指令相符)
            {
                for(int i = 0; i <程式編譯_輸出參數.IL指令程式.Count;i++)
                {
                    string[] str_0 = 程式編譯_輸出參數.IL指令程式[i];
                    string[] str_1 = IL指令程式[i];
                    if (str_0.Length != str_1.Length)
                    {
                        編譯與反編譯IL指令相符 = false;
                        break;
                    }
                    for(int j = 0; j<str_0.Length ; j++)
                    {
                        if (str_0[j] != str_1[j])
                        {
                            編譯與反編譯IL指令相符 = false;
                            break;
                        }
                    }
                }
            }
            if (程式編譯_輸出參數.編譯失敗次數 == 0 && 編譯與反編譯IL指令相符)
            {
                cnt++;
            }
            else
            {
                if (!(程式編譯_輸出參數.編譯失敗次數 == 0)) str_讀取檔案彈出視窗文字 = "編譯失敗";
                if (!(編譯與反編譯IL指令相符)) str_讀取檔案彈出視窗文字 = "編譯與反編譯結果不相符!";
                cnt = 200;
            }
        }
        void cnt_讀取檔案_20_記憶上一步(ref byte cnt)
        {
            sub_記憶上一步();
            cnt++;
        }
        void cnt_讀取檔案_150_讀取成功(ref byte cnt)
        {
            cnt_DataTable_初始化 = 1;
            str_讀取檔案彈出視窗文字 = "";
            cnt++;
        }
        void cnt_讀取檔案_200_讀取失敗(ref byte cnt)
        {
            //ladder_list = List_LADDER_All_Copy(ladder_list_備份);
          //  ladder_list.Clear();
          //  FLAG_初始化 = false;
           // IL指令程式.Clear();
            cnt++;
        }
        void cnt_讀取檔案_240_彈出視窗(ref byte cnt)
        {
            操作方框索引.X = 0;
            操作方框索引.Y = 0;
            顯示畫面列數索引 = 0;
            if (str_讀取檔案彈出視窗文字!="")
            {
                MessageBox.Show(str_讀取檔案彈出視窗文字, " ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);         
            }
           cnt++;
        }
        bool 讀取檔案(String str)
        {
            bool FLAG = false;
            IL指令程式_讀取buf.Clear();
            IFormatter binFmt = new BinaryFormatter();
            try
            {
                if (File.Exists(str))
                {
                    Stream s = File.Open(str, FileMode.Open);
                    try { P_基本參數 = (基本參數)binFmt.Deserialize(s); }
                    finally { }
                    s.Close();
                }
                //ladder_list = List_LADDER_All_Copy(P_基本參數.ladder_list);
                IL指令程式_讀取buf = List_IL_All_Copy(P_基本參數.Program);
                device.Set_Comment(P_基本參數.Comment);
                if (device == null) device = new DEVICE(false);
                FLAG = true;
            }
            catch
            {
                FLAG = false;
            }
            return FLAG;
        }
        #endregion
        #region 程式編譯
        #region 程式編譯_階梯圖轉換為IL指令

        static private class 程式編譯_階梯圖轉換為IL指令_輸入參數
        {
            static public List<LADDER[]> list = new List<LADDER[]>();
            static public int X_現在值 = 0;
            static public int Y_現在值 = 0;
            static public int 插入位置上方IL指令行數 = 0;
        }
        static private class 程式編譯_階梯圖轉換為IL指令_輸出參數
        {
            static public List<String[]> IL指令程式 = new List<string[]>();
            static public bool 轉換NG = false;
            static public Point 最上方節點位置 = new Point();
            static public Point 最下方節點位置 = new Point();
        }
        private class _程式編譯_階梯圖轉換為IL指令
        {
            public _程式編譯_階梯圖轉換為IL指令(List<LADDER[]> _list, int _X_現在值, int _Y_現在值)
            {
                this.list = _list;
                this.X_現在值 = _X_現在值;
                this.Y_現在值 = _Y_現在值;
            }
            public List<LADDER[]> list = new List<LADDER[]>();
            public int X_現在值 = 0;
            public int Y_現在值 = 0;
            public LADDER 上方元件 = new LADDER(1);
            public LADDER 下方元件 = new LADDER(1);
            public LADDER 左方元件 = new LADDER(1);
            public LADDER 本身元件 = new LADDER(1);
            public Point 最上方節點位置 = new Point();
            public Point 最下方節點位置 = new Point();
            public List<IL節點> IL節點堆疊區 = new List<IL節點>();
            public List<String[]> IL指令程式 = new List<string[]>();
            public List<String> 解析指令 = new List<string>();
            public bool 轉換NG = false;
            public int 錯誤迴路計數設定值 = 3;
            public int 錯誤迴路計數現在值 = 0;
        }
        byte cnt_程式編譯_階梯圖轉換為IL指令 = 255;
        void sub_程式編譯_階梯圖轉換為IL指令()
        {
            bool FLAG_階梯圖轉換為IL指令_BUSY = false;
            if (cnt_程式編譯_階梯圖轉換為IL指令 == 1) FLAG_階梯圖轉換為IL指令_BUSY = true;
            while (FLAG_階梯圖轉換為IL指令_BUSY)
            {
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 255) FLAG_階梯圖轉換為IL指令_BUSY = false;
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 1) sub_程式編譯_階梯圖轉換為IL指令_00_初始化(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 2) sub_程式編譯_階梯圖轉換為IL指令_00_檢查節點位置(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 3) cnt_程式編譯_階梯圖轉換為IL指令 = 10;

                if (cnt_程式編譯_階梯圖轉換為IL指令 == 10) sub_程式編譯_階梯圖轉換為IL指令_10_檢查X及Y現在值(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 11) sub_程式編譯_階梯圖轉換為IL指令_10_參數設置(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 12) sub_程式編譯_階梯圖轉換為IL指令_10_檢查是否需要編譯(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 13) sub_程式編譯_階梯圖轉換為IL指令_10_解析指令(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 14) sub_程式編譯_階梯圖轉換為IL指令_10_將指令分析結果寫至節點堆疊區(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 15) cnt_程式編譯_階梯圖轉換為IL指令 = 20;

                if (cnt_程式編譯_階梯圖轉換為IL指令 == 20) sub_程式編譯_階梯圖轉換為IL指令_20_檢查有無向上連結(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 21) sub_程式編譯_階梯圖轉換為IL指令_20_檢查堆疊區上一位置條件是否可OR(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 22) cnt_程式編譯_階梯圖轉換為IL指令 = 30;

                if (cnt_程式編譯_階梯圖轉換為IL指令 == 30) sub_程式編譯_階梯圖轉換為IL指令_30_檢查有無右下連結(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 31) sub_程式編譯_階梯圖轉換為IL指令_30_檢查更變位置是否脫離BLOCK(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 32) sub_程式編譯_階梯圖轉換為IL指令_30_更變現在位置(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 33) cnt_程式編譯_階梯圖轉換為IL指令 = 10;

                if (cnt_程式編譯_階梯圖轉換為IL指令 == 40) sub_程式編譯_階梯圖轉換為IL指令_40_設定BLOCK結束(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 41) sub_程式編譯_階梯圖轉換為IL指令_40_檢查堆疊區上一位置條件是否可AND(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 42) sub_程式編譯_階梯圖轉換為IL指令_40_更換一行(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 43) cnt_程式編譯_階梯圖轉換為IL指令 = 10;

                if (cnt_程式編譯_階梯圖轉換為IL指令 == 200) sub_程式編譯_階梯圖轉換為IL指令_200_轉換失敗(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 201) cnt_程式編譯_階梯圖轉換為IL指令 = 150;


                if (cnt_程式編譯_階梯圖轉換為IL指令 == 150) sub_程式編譯_階梯圖轉換為IL指令_150_檢查是否還有剩餘未編譯區域(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 151) sub_程式編譯_階梯圖轉換為IL指令_150_更變輸出參數(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 152) sub_程式編譯_階梯圖轉換為IL指令_150_檢查Listbox顯示Ready(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 153) sub_程式編譯_階梯圖轉換為IL指令_150_檢查Listbox顯示結束(ref cnt_程式編譯_階梯圖轉換為IL指令);
                if (cnt_程式編譯_階梯圖轉換為IL指令 == 154) cnt_程式編譯_階梯圖轉換為IL指令 = 255;
            }
        }

        _程式編譯_階梯圖轉換為IL指令 程式編譯_階梯圖轉換為IL指令;
        void sub_程式編譯_階梯圖轉換為IL指令_00_初始化(ref byte cnt)
        {
            程式編譯_階梯圖轉換為IL指令 = new _程式編譯_階梯圖轉換為IL指令(程式編譯_階梯圖轉換為IL指令_輸入參數.list, 程式編譯_階梯圖轉換為IL指令_輸入參數.X_現在值, 程式編譯_階梯圖轉換為IL指令_輸入參數.Y_現在值);
            程式編譯_階梯圖轉換為IL指令_輸出參數.IL指令程式.Clear();
            程式編譯_階梯圖轉換為IL指令_輸出參數.最上方節點位置 = new Point();
            程式編譯_階梯圖轉換為IL指令_輸出參數.最下方節點位置 = new Point();
            程式編譯_階梯圖轉換為IL指令_輸出參數.轉換NG = false;
            cnt++;
        }
        void sub_程式編譯_階梯圖轉換為IL指令_00_檢查節點位置(ref byte cnt)
        {
            int X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
            int Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;
            程式編譯_階梯圖轉換為IL指令_輸出參數.最上方節點位置 = 程式編譯_階梯圖轉換為IL指令.最上方節點位置 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, Y_temp), 程式編譯_階梯圖轉換為IL指令.list);
            程式編譯_階梯圖轉換為IL指令_輸出參數.最下方節點位置 = 程式編譯_階梯圖轉換為IL指令.最下方節點位置 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, Y_temp), 程式編譯_階梯圖轉換為IL指令.list);

            int 總列數 = (程式編譯_階梯圖轉換為IL指令.最下方節點位置.Y - 程式編譯_階梯圖轉換為IL指令.最上方節點位置.Y + 1);
            int 階數值 = 1;
            for (int i = 1; i <= 總列數; i++)
            {
                階數值 *= i;
            }
            程式編譯_階梯圖轉換為IL指令.錯誤迴路計數設定值 = 總列數 * 100;

            cnt++;
        }
        void sub_程式編譯_階梯圖轉換為IL指令_10_檢查X及Y現在值(ref byte cnt)
        {
            //   程式編譯_階梯圖轉換為IL指令.錯誤迴路計數設定值 = (程式編譯_階梯圖轉換為IL指令.最下方節點位置.Y - 程式編譯_階梯圖轉換為IL指令.最上方節點位置.Y + 1) * (一列格數 - 2);
            程式編譯_階梯圖轉換為IL指令_輸出參數.轉換NG = 程式編譯_階梯圖轉換為IL指令.轉換NG;
            程式編譯_階梯圖轉換為IL指令.錯誤迴路計數現在值++;

            if (程式編譯_階梯圖轉換為IL指令.X_現在值 < 1) 程式編譯_階梯圖轉換為IL指令.X_現在值 = 1;
            if (程式編譯_階梯圖轉換為IL指令.錯誤迴路計數現在值 >= 程式編譯_階梯圖轉換為IL指令.錯誤迴路計數設定值)
            {
                程式編譯_階梯圖轉換為IL指令.轉換NG = true;
            }
            if ((程式編譯_階梯圖轉換為IL指令.Y_現在值 > 程式編譯_階梯圖轉換為IL指令.最下方節點位置.Y))
            {
                程式編譯_階梯圖轉換為IL指令.轉換NG = true;
            }


            if (程式編譯_階梯圖轉換為IL指令.轉換NG)
            {
                cnt = 200;
            }
            else
            {
                if (程式編譯_階梯圖轉換為IL指令.list[程式編譯_階梯圖轉換為IL指令.Y_現在值][10].ladderType == partTypeEnum.EndPart)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add("END");
                    程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());
                    for (int X = 0; X < 一列格數; X++)
                    {
                        程式編譯_階梯圖轉換為IL指令.list[程式編譯_階梯圖轉換為IL指令.Y_現在值][X].未編譯 = false;
                    }
                    cnt = 150;
                }
                else if ((程式編譯_階梯圖轉換為IL指令.X_現在值 >= 一列格數 - 1))
                {

                    cnt = 150;
                }
                else
                {
                    cnt++; ;
                }
            }
        }
        void sub_程式編譯_階梯圖轉換為IL指令_10_參數設置(ref byte cnt)
        {
            int X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
            int Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

            if (Y_temp - 1 >= 0) 程式編譯_階梯圖轉換為IL指令.上方元件 = 程式編譯_階梯圖轉換為IL指令.list[Y_temp - 1][X_temp];
            else 程式編譯_階梯圖轉換為IL指令.上方元件 = new LADDER(1);

            if (Y_temp + 1 < 程式編譯_階梯圖轉換為IL指令.list.Count)
            {
                程式編譯_階梯圖轉換為IL指令.下方元件 = 程式編譯_階梯圖轉換為IL指令.list[Y_temp + 1][X_temp];
            }
            else 程式編譯_階梯圖轉換為IL指令.下方元件 = new LADDER(1);
            程式編譯_階梯圖轉換為IL指令.左方元件 = 程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp - 1];
            程式編譯_階梯圖轉換為IL指令.本身元件 = 程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp];
            cnt++;
        }
        void sub_程式編譯_階梯圖轉換為IL指令_10_檢查是否需要編譯(ref byte cnt)
        {
            程式編譯_階梯圖轉換為IL指令.解析指令 = new List<string>();
            if (程式編譯_階梯圖轉換為IL指令.本身元件.未編譯 == true)
            {
                cnt++;
            }
            else
            {
                cnt = 20;
            }
        }
        void sub_程式編譯_階梯圖轉換為IL指令_10_解析指令(ref byte cnt)
        {
            int X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
            int Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;
            int 當行元件數量 = 0;
            bool 元件數量超過兩個 = false;
            for (int Y = 程式編譯_階梯圖轉換為IL指令.最上方節點位置.Y; Y <= 程式編譯_階梯圖轉換為IL指令.最下方節點位置.Y; Y++)
            {
                LADDER ladder_temp = 程式編譯_階梯圖轉換為IL指令.list[Y][程式編譯_階梯圖轉換為IL指令.X_現在值];
                if (ladder_temp.ladderType == partTypeEnum.A_Part || ladder_temp.ladderType == partTypeEnum.B_Part || ladder_temp.ladderType == partTypeEnum.LD_Equal_Part) 當行元件數量++;
            }
            if (當行元件數量 >= 2) 元件數量超過兩個 = true;
            bool LD = false;
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //A接點及B接點指令判斷
            if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.A_Part || 程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.B_Part || 程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.H_Line_Part)
            {
                if (程式編譯_階梯圖轉換為IL指令.左方元件.Vline_右上 || 程式編譯_階梯圖轉換為IL指令.左方元件.Vline_右下)
                {
                    if (程式編譯_階梯圖轉換為IL指令.左方元件.ladderType == partTypeEnum.noPart || 程式編譯_階梯圖轉換為IL指令.左方元件.ladderType == partTypeEnum.leftParenthesis)
                    {
                        LD = true;
                    }
                    if (程式編譯_階梯圖轉換為IL指令.左方元件.Vline_右上)
                    {
                        if (元件數量超過兩個 || 程式編譯_階梯圖轉換為IL指令.上方元件.ladderType == partTypeEnum.H_Line_Part)
                        {
                            LD = true;
                        }
                    }
                    if (程式編譯_階梯圖轉換為IL指令.左方元件.Vline_右下)
                    {
                        if (元件數量超過兩個 || 程式編譯_階梯圖轉換為IL指令.下方元件.ladderType == partTypeEnum.H_Line_Part)
                        {
                            LD = true;
                        }
                    }
                }
                else
                {
                    LD = false;
                }
                if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType != partTypeEnum.H_Line_Part)
                {
                    if (LD)
                    {
                        if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.A_Part) 程式編譯_階梯圖轉換為IL指令.解析指令.Add("LD");
                        if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.B_Part) 程式編譯_階梯圖轉換為IL指令.解析指令.Add("LDI");
                        程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[0]);
                    }
                    else
                    {
                        if (程式編譯_階梯圖轉換為IL指令.Y_現在值 != 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線列位置)
                        {
                            程式編譯_階梯圖轉換為IL指令.轉換NG = true;
                        }
                        if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.A_Part) 程式編譯_階梯圖轉換為IL指令.解析指令.Add("AND");
                        if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.B_Part) 程式編譯_階梯圖轉換為IL指令.解析指令.Add("ANI");
                        程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[0]);
                    }
                }
                else
                {
                    if (LD)
                    {
                        程式編譯_階梯圖轉換為IL指令.轉換NG = true;
                    }

                }
                cnt++;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //比較指令判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.LD_Equal_Part)
            {
                if (程式編譯_階梯圖轉換為IL指令.左方元件.Vline_右上 || 程式編譯_階梯圖轉換為IL指令.左方元件.Vline_右下)
                {
                    if (程式編譯_階梯圖轉換為IL指令.左方元件.ladderType == partTypeEnum.noPart || 程式編譯_階梯圖轉換為IL指令.左方元件.ladderType == partTypeEnum.leftParenthesis)
                    {
                        LD = true;
                    }
                    if (程式編譯_階梯圖轉換為IL指令.左方元件.Vline_右上)
                    {
                        if (元件數量超過兩個 || 程式編譯_階梯圖轉換為IL指令.上方元件.ladderType == partTypeEnum.H_Line_Part)
                        {
                            LD = true;
                        }
                    }
                    if (程式編譯_階梯圖轉換為IL指令.左方元件.Vline_右下)
                    {
                        if (元件數量超過兩個 || 程式編譯_階梯圖轉換為IL指令.下方元件.ladderType == partTypeEnum.H_Line_Part)
                        {
                            LD = true;
                        }
                    }
                }
                if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType != partTypeEnum.H_Line_Part || 程式編譯_階梯圖轉換為IL指令.左方元件.ladderType == partTypeEnum.Data_no_Part)
                {
                    if (LD)
                    {
                        for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                        {
                            String str_temp = "";
                            if (i == 0) str_temp = "LD";
                            str_temp += 程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i];
                            程式編譯_階梯圖轉換為IL指令.解析指令.Add(str_temp);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                        {
                            String str_temp = "";
                            if (i == 0) str_temp = "AND";
                            str_temp += 程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i];
                            程式編譯_階梯圖轉換為IL指令.解析指令.Add(str_temp);
                        }
                    }
                }
                else
                {
                    if (LD)
                    {
                        程式編譯_階梯圖轉換為IL指令.轉換NG = true;
                    }

                }
                cnt++;

            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //OUT輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.OUT_Part)
            {
                程式編譯_階梯圖轉換為IL指令.解析指令.Add("OUT");
                程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[0]);
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());
                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //MOV輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.MOV_Part)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 2].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //ADD輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.ADD_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 2].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 3].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //SUB輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.SUB_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 2].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 3].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //MUL輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.MUL_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 2].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 3].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //DIV輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.DIV_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 2].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 3].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //INC輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.INC_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //DRVA輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.DRVA_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 2].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 3].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //DRVI輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.DRVI_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 2].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 3].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //PLSV輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.PLSV_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 2].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //OUT_TIMER輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.OUT_TIMER_PART)
            {
                程式編譯_階梯圖轉換為IL指令.解析指令.Add("OUT");
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //SET輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.SET_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //RST輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.RST_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //ZRST輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.ZRST_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 2].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 3].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //BMOV輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.BMOV_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 2].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 3].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //WTB輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.WTB_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 2].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 3].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //BTW輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.BTW_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 2].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 3].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //TAB輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.TAB_PART)
            {
                程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[0]);
                程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[1]);
                程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[2]);
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());
                for (int i = 0; i < 一列格數; i ++ )
                {
                    程式編譯_階梯圖轉換為IL指令.list[Y_temp][i].未編譯 = false;
                }
              
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //JUMP輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.JUMP_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //REF輸出判斷
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.REF_PART)
            {
                for (int i = 0; i < 程式編譯_階梯圖轉換為IL指令.本身元件.元素數量; i++)
                {
                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(程式編譯_階梯圖轉換為IL指令.本身元件.ladderParam[i]);
                }
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());

                X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 1].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp + 2].未編譯 = false;
                程式編譯_階梯圖轉換為IL指令.X_現在值 = (一列格數 - 1);
                cnt = 10;
            }
            //----------------------------------------------------------------------------------------------------------------------------------------------
            else
            {
                cnt++;
            }

        }
        void sub_程式編譯_階梯圖轉換為IL指令_10_將指令分析結果寫至節點堆疊區(ref byte cnt)
        {
            IL節點 IL節點_temp = new IL節點();
            if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.A_Part || 程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.B_Part || 程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.LD_Equal_Part)
            {
                if (程式編譯_階梯圖轉換為IL指令.解析指令[0].IndexOf("LD") >= 0)
                {
                    IL節點_temp.母線首行位置 = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                    IL節點_temp.母線末行位置 = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                    IL節點_temp.母線列位置 = 程式編譯_階梯圖轉換為IL指令.Y_現在值;
                    程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Add(IL節點_temp);
                }
                if (程式編譯_階梯圖轉換為IL指令.解析指令[0].IndexOf("AN") >= 0)
                {
                    程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線末行位置 = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                    程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].BLOCK已完成 = false;
                }
            }
            else if( 程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.Data_no_Part)
            {
                if (程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線末行位置 < 程式編譯_階梯圖轉換為IL指令.X_現在值)
                {
                    程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線末行位置 = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                }
                else 程式編譯_階梯圖轉換為IL指令.轉換NG = true;
            }
            else if (程式編譯_階梯圖轉換為IL指令.本身元件.ladderType == partTypeEnum.H_Line_Part )
            {
                if (程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線末行位置 < 程式編譯_階梯圖轉換為IL指令.X_現在值)
                {
                    程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線末行位置 = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                }
                else 程式編譯_階梯圖轉換為IL指令.轉換NG = true;

            }

            cnt++;
        }
        void sub_程式編譯_階梯圖轉換為IL指令_20_檢查有無向上連結(ref byte cnt)
        {
            if (程式編譯_階梯圖轉換為IL指令.本身元件.Vline_右上)
            {
                if (程式編譯_階梯圖轉換為IL指令.上方元件.Vline_右下)
                {
                    cnt++;
                }
                else
                {
                    程式編譯_階梯圖轉換為IL指令.轉換NG = true;
                    cnt = 10;
                }
            }
            else
            {

                cnt = 30;
            }
            if (程式編譯_階梯圖轉換為IL指令.解析指令.Count > 0)
            {
                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());
            }
        }
        void sub_程式編譯_階梯圖轉換為IL指令_20_檢查堆疊區上一位置條件是否可OR(ref byte cnt)
        {
            if (程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 2 >= 0)
            {
                List<String> BLOCK_指令 = new List<string>();
                LADDER lader_temp = new LADDER(1);
                IL節點 IL節點_上一個位置 = 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 2];
                IL節點 IL節點_當前位置 = 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1];

                int 元件數量 = 0;
                int 橫線數量 = 0;
                int 元件總數量 = IL節點_當前位置.母線末行位置 - IL節點_當前位置.母線首行位置 + 1;
                for (int i = IL節點_當前位置.母線首行位置; i <= IL節點_當前位置.母線末行位置; i++)
                {
                    Point Po_temp = new Point(i, IL節點_當前位置.母線列位置);
                    if (程式編譯_階梯圖轉換為IL指令.list[Po_temp.Y][Po_temp.X].ladderType == partTypeEnum.A_Part || 程式編譯_階梯圖轉換為IL指令.list[Po_temp.Y][Po_temp.X].ladderType == partTypeEnum.B_Part || 程式編譯_階梯圖轉換為IL指令.list[Po_temp.Y][Po_temp.X].ladderType == partTypeEnum.LD_Equal_Part)
                    {
                        lader_temp = 程式編譯_階梯圖轉換為IL指令.list[Po_temp.Y][Po_temp.X];
                        元件數量++;
                    }
                    else if (程式編譯_階梯圖轉換為IL指令.list[Po_temp.Y][Po_temp.X].ladderType == partTypeEnum.H_Line_Part || 程式編譯_階梯圖轉換為IL指令.list[Po_temp.Y][Po_temp.X].ladderType == partTypeEnum.Data_no_Part)
                    {
                        橫線數量++;
                    }
                }
                if ((元件數量 + 橫線數量) == 元件總數量)
                {
                    if ((IL節點_當前位置.母線首行位置 == IL節點_上一個位置.母線首行位置) && (IL節點_當前位置.母線末行位置 == IL節點_上一個位置.母線末行位置))
                    {
                        if (IL節點_當前位置.BLOCK已完成 == false)
                        {

                            if (元件數量 == 1)
                            {
                                程式編譯_階梯圖轉換為IL指令.解析指令 = new List<string>();
                                if (lader_temp.ladderType == partTypeEnum.A_Part || lader_temp.ladderType == partTypeEnum.B_Part)
                                {

                                    if (lader_temp.ladderType == partTypeEnum.A_Part) 程式編譯_階梯圖轉換為IL指令.解析指令.Add("OR");
                                    if (lader_temp.ladderType == partTypeEnum.B_Part) 程式編譯_階梯圖轉換為IL指令.解析指令.Add("ORI");
                                    程式編譯_階梯圖轉換為IL指令.解析指令.Add(lader_temp.ladderParam[0]);

                                }
                                if (lader_temp.ladderType == partTypeEnum.LD_Equal_Part)
                                {
                                    for (int i = 0; i < lader_temp.元素數量; i++)
                                    {
                                        String str_temp = "";
                                        if (i == 0) str_temp = "OR";
                                        str_temp += lader_temp.ladderParam[i];
                                        程式編譯_階梯圖轉換為IL指令.解析指令.Add(str_temp);
                                    }
                                }
                                程式編譯_階梯圖轉換為IL指令.IL指令程式.RemoveAt(程式編譯_階梯圖轉換為IL指令.IL指令程式.Count - 1);
                                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(程式編譯_階梯圖轉換為IL指令.解析指令.ToArray());
                            }
                            else
                            {
                                BLOCK_指令.Add("ORB");
                                程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(BLOCK_指令.ToArray());
                            }
                            int X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                            int Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;

                            int 母線起始行 = IL節點_上一個位置.母線首行位置;
                            int 母線結束行 = IL節點_上一個位置.母線末行位置;
                            int 上母線列位置 = IL節點_上一個位置.母線列位置;
                            int 下母線列位置 = IL節點_當前位置.母線列位置;
                            for (int Y = 上母線列位置; Y <= 下母線列位置; Y++)
                            {
                                bool 檢查斷線_OK = true;
                                if (Y != 上母線列位置)
                                {
                                    if (!程式編譯_階梯圖轉換為IL指令.list[Y][母線起始行 - 1].Vline_右上) 檢查斷線_OK = false;
                                    if (!程式編譯_階梯圖轉換為IL指令.list[Y][母線結束行].Vline_右上) 檢查斷線_OK = false;
                                }
                                if (Y != 下母線列位置)
                                {
                                    if (!程式編譯_階梯圖轉換為IL指令.list[Y][母線起始行 - 1].Vline_右下) 檢查斷線_OK = false;
                                    if (!程式編譯_階梯圖轉換為IL指令.list[Y][母線結束行].Vline_右下) 檢查斷線_OK = false;
                                }
                                if (!檢查斷線_OK) 程式編譯_階梯圖轉換為IL指令.轉換NG = true;
                            }
                            程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.RemoveAt(程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1);
                            程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
                            程式編譯_階梯圖轉換為IL指令.X_現在值 = 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線末行位置;
                            程式編譯_階梯圖轉換為IL指令.Y_現在值 = 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線列位置;
                        }
                    }

                }
                else
                {
                    程式編譯_階梯圖轉換為IL指令.轉換NG = true;
                }
            }
            cnt++;

        }
        void sub_程式編譯_階梯圖轉換為IL指令_30_檢查有無右下連結(ref byte cnt)
        {
            int X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
            int Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;
            bool 不檢查 = false;
            if (X_temp < 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線末行位置)
            {
                if (X_temp >= 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線首行位置)
                {
                    if (!程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯)
                    {
                        不檢查 = true;
                    }
                }
            }
            if (!不檢查)
            {
                if (程式編譯_階梯圖轉換為IL指令.本身元件.Vline_右下)
                {
                    cnt++;
                }
                else
                {
                    cnt = 40;
                }
            }
            else
            {
                cnt = 40;
            }
        }
        void sub_程式編譯_階梯圖轉換為IL指令_30_檢查更變位置是否脫離BLOCK(ref byte cnt)
        {
            int X_temp = 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線首行位置;
            int Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值 + 1;
            if (Y_temp < 程式編譯_階梯圖轉換為IL指令.list.Count)
            {
                if (程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp - 1].Vline_右上)
                {
                    cnt++;
                }
                else
                {
                    程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].BLOCK已完成 = true;
                    cnt = 40;
                }
            }
            else cnt = 10;

        }
        void sub_程式編譯_階梯圖轉換為IL指令_30_更變現在位置(ref byte cnt)
        {
            int X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
            int Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;
            程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;
            程式編譯_階梯圖轉換為IL指令.X_現在值 = 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線首行位置;
            程式編譯_階梯圖轉換為IL指令.Y_現在值++;
            cnt++;
        }
        void sub_程式編譯_階梯圖轉換為IL指令_40_設定BLOCK結束(ref byte cnt)
        {
            int X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
            int Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;
            bool 要檢查 = false;
            if (X_temp == 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線末行位置)
            {
                要檢查 = true;
            }
            if (要檢查)
            {
                if (程式編譯_階梯圖轉換為IL指令.本身元件.Vline_右上)
                {
                    程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].BLOCK已完成 = true;
                }
            }

            cnt++;
        }
        void sub_程式編譯_階梯圖轉換為IL指令_40_檢查堆疊區上一位置條件是否可AND(ref byte cnt)
        {
            if (程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 2 >= 0)
            {
                List<String> BLOCK_指令 = new List<string>();
                IL節點 IL節點_上一個位置 = 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 2];
                IL節點 IL節點_當前位置 = 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1];
                if ((IL節點_上一個位置.母線末行位置 + 1) == IL節點_當前位置.母線首行位置)
                {
                    if (IL節點_上一個位置.母線列位置 == IL節點_當前位置.母線列位置)
                    {
                        if (IL節點_當前位置.BLOCK已完成 && IL節點_上一個位置.BLOCK已完成)
                        {
                            BLOCK_指令.Add("ANB");
                            IL節點 IL節點_temp = new IL節點();
                            程式編譯_階梯圖轉換為IL指令.IL指令程式.Add(BLOCK_指令.ToArray());
                            IL節點_temp.母線首行位置 = IL節點_上一個位置.母線首行位置;
                            IL節點_temp.母線列位置 = IL節點_上一個位置.母線列位置;
                            IL節點_temp.母線末行位置 = IL節點_當前位置.母線末行位置;
                            IL節點_temp.BLOCK已完成 = false;


                            程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.RemoveAt(程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1);
                            程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.RemoveAt(程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1);
                            程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Add(IL節點_temp);

                            int X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
                            int Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;
                            程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;

                            程式編譯_階梯圖轉換為IL指令.X_現在值 = 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線首行位置;
                            程式編譯_階梯圖轉換為IL指令.Y_現在值 = 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線列位置;
                        }
                    }
                    else
                    {
                        程式編譯_階梯圖轉換為IL指令.轉換NG = true;
                    }
                }
            }
            cnt++;
        }
        void sub_程式編譯_階梯圖轉換為IL指令_40_更換一行(ref byte cnt)
        {
            int X_temp = 程式編譯_階梯圖轉換為IL指令.X_現在值;
            int Y_temp = 程式編譯_階梯圖轉換為IL指令.Y_現在值;
            程式編譯_階梯圖轉換為IL指令.list[Y_temp][X_temp].未編譯 = false;

            if (程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].BLOCK已完成)
            {
                程式編譯_階梯圖轉換為IL指令.X_現在值 = 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線末行位置 + 1;
                程式編譯_階梯圖轉換為IL指令.Y_現在值 = 程式編譯_階梯圖轉換為IL指令.IL節點堆疊區[程式編譯_階梯圖轉換為IL指令.IL節點堆疊區.Count - 1].母線列位置;
            }
            else
            {
                程式編譯_階梯圖轉換為IL指令.X_現在值++;
            }
            cnt++;
        }
        void sub_程式編譯_階梯圖轉換為IL指令_150_檢查是否還有剩餘未編譯區域(ref byte cnt)
        {
            for (int Y = 程式編譯_階梯圖轉換為IL指令.最上方節點位置.Y; Y <= 程式編譯_階梯圖轉換為IL指令.最下方節點位置.Y; Y++)
            {
                for (int X = 1; X < 一列格數 - 1; X++)
                {
                    if (程式編譯_階梯圖轉換為IL指令.list[Y][X].ladderType != partTypeEnum.noPart)
                    {
                        if (程式編譯_階梯圖轉換為IL指令.list[Y][X].未編譯)
                        {
                            程式編譯_階梯圖轉換為IL指令.轉換NG = true;
                        }
                    }
                }
            }
            cnt++;
        }
        void sub_程式編譯_階梯圖轉換為IL指令_150_更變輸出參數(ref byte cnt)
        {
            程式編譯_階梯圖轉換為IL指令_輸出參數.IL指令程式 = 程式編譯_階梯圖轉換為IL指令.IL指令程式;
            程式編譯_階梯圖轉換為IL指令_輸出參數.轉換NG = 程式編譯_階梯圖轉換為IL指令.轉換NG;
            Point p0 = new Point();
            Point p1 = new Point();
            p0 = 程式編譯_階梯圖轉換為IL指令_輸出參數.最上方節點位置;
            p1 = 程式編譯_階梯圖轉換為IL指令_輸出參數.最下方節點位置;
            程式編譯_階梯圖轉換為IL指令_輸入參數.list[p0.Y][0].ladderParam[0] = 程式編譯_階梯圖轉換為IL指令_輸入參數.插入位置上方IL指令行數.ToString();
            if (p0.Y != p1.Y)
            {
                程式編譯_階梯圖轉換為IL指令_輸入參數.list[p1.Y][0].ladderParam[0] = (程式編譯_階梯圖轉換為IL指令_輸入參數.插入位置上方IL指令行數 + 程式編譯_階梯圖轉換為IL指令_輸出參數.IL指令程式.Count - 1).ToString();
            }
            else
            {
                程式編譯_階梯圖轉換為IL指令_輸入參數.list[p1.Y][0].ladderParam[1] = (程式編譯_階梯圖轉換為IL指令_輸入參數.插入位置上方IL指令行數 + 程式編譯_階梯圖轉換為IL指令_輸出參數.IL指令程式.Count - 1).ToString();
            }
            for (int i = p0.Y + 1; i < p1.Y; i++)
            {
                程式編譯_階梯圖轉換為IL指令_輸入參數.list[i][0].ladderParam[0] = "";
            }
            cnt++;
        }
        void sub_程式編譯_階梯圖轉換為IL指令_150_檢查Listbox顯示Ready(ref byte cnt)
        {

            if (cnt_ListBox_IL指令集更新 == 255 && false)
            {
                cnt_ListBox_IL指令集更新 = 1;
                cnt++;
            }
            cnt++;
        }
        void sub_程式編譯_階梯圖轉換為IL指令_150_檢查Listbox顯示結束(ref byte cnt)
        {
            if (cnt_ListBox_IL指令集更新 == 255 && false)
            {
                cnt++;
            }
            cnt++;
        }
        void sub_程式編譯_階梯圖轉換為IL指令_200_轉換失敗(ref byte cnt)
        {
            // MessageBox.Show("轉換失敗!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            cnt++;
        }
        void sub_程式編譯_階梯圖轉換為IL指令_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        static private class 程式編譯_輸入參數
        {
            static public int 插入位置上方IL指令行數;
            static public int 插入位置下方IL指令行數;
            static public int 起始編譯階梯列數;
            static public int 未編譯區塊最上方IL指令行數;
            static public int 未編譯區塊最下方IL指令行數;
            static public string 彈出視窗文字 = "";
            static public bool 彈出視窗要顯示 = true;
        }
        static private class 程式編譯_輸出參數
        {
            static public List<String[]> IL指令程式 = new List<string[]>();
            static public int 編譯完偏移行數;
            static public int 總編譯階梯列數;
            static public int 編譯失敗次數 = 0;
        }
        byte cnt_程式編譯 = 255;
        bool FLAG_程式編譯_成功 = false;
        void sub_程式編譯()
        {

            if (cnt_程式編譯 == 1) cnt_程式編譯_00_檢查可編譯(ref cnt_程式編譯);
            if (cnt_程式編譯 == 2) cnt_程式編譯_00_空行縮列檢查(ref cnt_程式編譯);
            if (cnt_程式編譯 == 3) cnt_程式編譯_00_未編譯前檢查(ref cnt_程式編譯);
            if (cnt_程式編譯 == 4) cnt_程式編譯_00_複製未編譯區塊(ref cnt_程式編譯);
            if (cnt_程式編譯 == 5) cnt_程式編譯 = 20;

            if (cnt_程式編譯 == 20) cnt_程式編譯_20_編譯前初始化(ref cnt_程式編譯);
            if (cnt_程式編譯 == 21) cnt_程式編譯_20_檢查是否有需要編譯區域(ref cnt_程式編譯);
            if (cnt_程式編譯 == 22) cnt_程式編譯_20_向上及向左縮排(ref cnt_程式編譯);
            if (cnt_程式編譯 == 23) cnt_程式編譯_20_計算編譯區包含的IL程式範圍(ref cnt_程式編譯);
            if (cnt_程式編譯 == 24) cnt_程式編譯_20_開始編成IL語法(ref cnt_程式編譯);
            if (cnt_程式編譯 == 25) cnt_程式編譯_20_檢查編譯結果(ref cnt_程式編譯);
            if (cnt_程式編譯 == 26) cnt_程式編譯 = 40;

            if (cnt_程式編譯 == 40) cnt_程式編譯_40_移除原本程式未編譯區塊(ref cnt_程式編譯);
            if (cnt_程式編譯 == 41) cnt_程式編譯_40_插入已編譯好區塊至原本程式(ref cnt_程式編譯);
            if (cnt_程式編譯 == 42) cnt_程式編譯_40_設定整段未編譯(ref cnt_程式編譯);
            if (cnt_程式編譯 == 43) cnt_程式編譯_40_未編譯前檢查(ref cnt_程式編譯);
            if (cnt_程式編譯 == 44) cnt_程式編譯_40_複製未編譯區塊(ref cnt_程式編譯);
            if (cnt_程式編譯 == 45) cnt_程式編譯_40_編譯前初始化(ref cnt_程式編譯);
            if (cnt_程式編譯 == 46) cnt_程式編譯_40_檢查是否有需要編譯區域(ref cnt_程式編譯);
            if (cnt_程式編譯 == 47) cnt_程式編譯_40_計算編譯區包含的IL程式範圍(ref cnt_程式編譯);
            if (cnt_程式編譯 == 48) cnt_程式編譯_40_開始編成IL語法(ref cnt_程式編譯);
            if (cnt_程式編譯 == 49) cnt_程式編譯_40_檢查編譯結果(ref cnt_程式編譯);
            if (cnt_程式編譯 == 50) cnt_程式編譯_40_清除階梯程式(ref cnt_程式編譯);
            if (cnt_程式編譯 == 51) cnt_程式編譯_40_插入已編譯好區塊至原本程式(ref cnt_程式編譯);
            if (cnt_程式編譯 == 52) cnt_程式編譯_40_清除原本程式IL指令(ref cnt_程式編譯);
            if (cnt_程式編譯 == 53) cnt_程式編譯_40_移入編譯好IL指令(ref cnt_程式編譯);
            if (cnt_程式編譯 == 54) cnt_程式編譯 = 150;

            if (cnt_程式編譯 == 150) cnt_程式編譯_150_編譯成功(ref cnt_程式編譯);
            if (cnt_程式編譯 == 151) cnt_程式編譯_150_檢查TAB目錄(ref cnt_程式編譯);          
            if (cnt_程式編譯 == 152) cnt_程式編譯 = 240;

            if (cnt_程式編譯 == 200) cnt_程式編譯_200_編譯失敗(ref cnt_程式編譯);
            if (cnt_程式編譯 == 201) cnt_程式編譯 = 240;

            if (cnt_程式編譯 == 240) cnt_程式編譯_240_等待顯示結果更新_READY(ref cnt_程式編譯);
            if (cnt_程式編譯 == 241) cnt_程式編譯_240_等待顯示結果更新_OVER(ref cnt_程式編譯);
            if (cnt_程式編譯 == 242) cnt_程式編譯_240_彈出視窗(ref cnt_程式編譯);
            if (cnt_程式編譯 == 243) cnt_程式編譯 = 255;
        }

        void cnt_程式編譯_00_未編譯前檢查(ref byte cnt)
        {
            程式編譯_輸入參數.彈出視窗文字 = "編譯失敗! ";
            未編譯範圍最大值 = 0;
            未編譯範圍最小值 = 99999999;
            device_system.Set_Device("T6",0);
            T6_未編譯檢查 = true;
            FLAG_程式編譯_成功 = false;
            sub_未編譯檢查();
            cnt++;
        }
        void cnt_程式編譯_00_檢查可編譯(ref byte cnt)
        {
            程式編譯_輸出參數.編譯失敗次數 = 0;
            if (程式編譯_檢查可編譯()) cnt++;
            else
            {
                程式編譯_輸出參數.編譯失敗次數++;
                程式編譯_輸入參數.彈出視窗文字 += "#001 ";
                cnt = 200;
            }
        }
        void cnt_程式編譯_00_空行縮列檢查(ref byte cnt)
        {
            while (true) if (程式編譯_空行縮列檢查()) break;
            cnt++;
        }
        void cnt_程式編譯_00_複製未編譯區塊(ref byte cnt)
        {
            程式編譯_複製未編譯區塊();
            cnt++;
        }

        void cnt_程式編譯_20_編譯前初始化(ref byte cnt)
        {
            程式編譯_輸出參數.IL指令程式.Clear();
            程式編譯_輸出參數.總編譯階梯列數 = 0;
            程式編譯_輸入參數.未編譯區塊最上方IL指令行數 = 999999;
            程式編譯_輸入參數.未編譯區塊最下方IL指令行數 = 0;
            程式編譯_輸出參數.編譯失敗次數 = 0;
            cnt++;
        }
        void cnt_程式編譯_20_檢查是否有需要編譯區域(ref byte cnt)
        {
            if (ladder_list_編譯區塊.Count > 0 )
            {
                cnt++;
            }
            else
            {
                cnt = 40;
            }
        }
        void cnt_程式編譯_20_向上及向左縮排(ref byte cnt)
        {
            for (int Y = 0; Y < ladder_list_編譯區塊.Count; Y++)
            {
                for (int X = 0; X < 一列格數; X++)
                {
                    Point p0 = new Point(1, Y);
                    while (true) if (程式編譯_向上縮排(ref p0)) break;
                    程式編譯_向左縮排(ref p0);
                    Y = p0.Y;
                }
            }
            cnt++;
        }
        void cnt_程式編譯_20_計算編譯區包含的IL程式範圍(ref byte cnt)
        {
          /*  for (int Y = 0; Y < ladder_list_編譯區塊.Count; Y++)
            {
                bool flag_Param_01 = false;
                bool flag_Param_02 = false;
                int Param_01 = 0;
                int Param_02 = 0;
                if (ladder_list_編譯區塊[Y][0].ladderParam[0] != null)
                {
                    if (ladder_list_編譯區塊[Y][0].ladderParam[0] != "")
                    {
                        Param_01 = Convert.ToInt32(ladder_list_編譯區塊[Y][0].ladderParam[0]);
                        flag_Param_01 = true;
                    }
                }
                if (ladder_list_編譯區塊[Y][0].ladderParam[0] != null && ladder_list_編譯區塊[Y][0].ladderParam[1] != null)
                {
                    if (ladder_list_編譯區塊[Y][0].ladderParam[0] != "" && ladder_list_編譯區塊[Y][0].ladderParam[1] != "")
                    {
                        Param_02 = Convert.ToInt32(ladder_list_編譯區塊[Y][0].ladderParam[1]);
                        flag_Param_02 = true;
                    }
                }
                if (flag_Param_01)
                {
                    if (Param_01 < 程式編譯_輸入參數.未編譯區塊最上方IL指令行數) 程式編譯_輸入參數.未編譯區塊最上方IL指令行數 = Param_01;

                    if (Param_01 > 程式編譯_輸入參數.未編譯區塊最下方IL指令行數) 程式編譯_輸入參數.未編譯區塊最下方IL指令行數 = Param_01;
                }
                if (flag_Param_02)
                {
                    if (Param_02 > 程式編譯_輸入參數.未編譯區塊最下方IL指令行數) 程式編譯_輸入參數.未編譯區塊最下方IL指令行數 = Param_02;
                }
            }*/
            cnt++;
        }
        void cnt_程式編譯_20_開始編成IL語法(ref byte cnt)
        {
            for (int Y = 0; Y < ladder_list_編譯區塊.Count; Y++)
            {
                Point p1 = new Point(1, Y);
                int cnt_step = 0;
                while (true)
                {
                    sub_程式編譯_階梯圖轉換為IL指令();
                    if (cnt_step == 0)
                    {

                        if (cnt_程式編譯_階梯圖轉換為IL指令 == 255)
                        {

                            程式編譯_階梯圖轉換為IL指令_輸入參數.list = ladder_list_編譯區塊;
                            程式編譯_階梯圖轉換為IL指令_輸入參數.X_現在值 = 1;
                            程式編譯_階梯圖轉換為IL指令_輸入參數.Y_現在值 = Y;
                            程式編譯_階梯圖轉換為IL指令_輸入參數.插入位置上方IL指令行數 = 程式編譯_輸出參數.IL指令程式.Count + 程式編譯_輸入參數.插入位置上方IL指令行數;
                            cnt_程式編譯_階梯圖轉換為IL指令 = 1;
                            cnt_step++;
                        }
                    }
                    if (cnt_step == 1)
                    {
                        if (cnt_程式編譯_階梯圖轉換為IL指令 == 255)
                        {
                            Y = 程式編譯_階梯圖轉換為IL指令_輸出參數.最下方節點位置.Y;
                            cnt_step++;
                        }
                    }
                    if (cnt_step == 2)
                    {
                        foreach (string[] str in 程式編譯_階梯圖轉換為IL指令_輸出參數.IL指令程式)
                        {
                            程式編譯_輸出參數.IL指令程式.Add(str);
                        }
                        cnt_step++;
                    }
                    if (cnt_step == 3)
                    {
                        if (程式編譯_階梯圖轉換為IL指令_輸出參數.轉換NG)
                        {
                            程式編譯_輸出參數.編譯失敗次數++;
                            程式編譯_輸入參數.彈出視窗文字 += "#002" + "{";
                            for (int i = 0; i < 程式編譯_輸出參數.IL指令程式[程式編譯_輸出參數.IL指令程式.Count - 1].Length; i++)
                            {
                                程式編譯_輸入參數.彈出視窗文字 += 程式編譯_輸出參數.IL指令程式[程式編譯_輸出參數.IL指令程式.Count - 1][i] + " "; 
                            }
                            程式編譯_輸入參數.彈出視窗文字 += "}";
                        }
                        程式編譯_輸出參數.總編譯階梯列數 = 程式編譯_階梯圖轉換為IL指令_輸出參數.最下方節點位置.Y;
                        break;
                    }
                    if (cnt_step == 4)
                    {

                    }
                    if (cnt_step == 5)
                    {

                    }
                }

            }
            cnt++;
        }
        void cnt_程式編譯_20_檢查編譯結果(ref byte cnt)
        {
            bool 編譯成功 = false;
            if (程式編譯_輸出參數.編譯失敗次數 == 0) 編譯成功 = true;
            if (編譯成功)
            {
                for (int Y = 0; Y < ladder_list_編譯區塊.Count; Y++)
                {
                    for (int X = 0; X < 一列格數; X++)
                    {
                        ladder_list_編譯區塊[Y][X].未編譯 = false;
                    }
                }
                cnt++;
            }
            else
            {
                cnt = 200;
            }
        }

        void cnt_程式編譯_40_移除原本程式未編譯區塊(ref byte cnt)
        {
            程式編譯_移除未編譯區塊();
            cnt++;
        }
        void cnt_程式編譯_40_插入已編譯好區塊至原本程式(ref byte cnt)
        {
            程式編譯_插入未編譯區塊();
            cnt++;
        }
        void cnt_程式編譯_40_設定整段未編譯(ref byte cnt)
        {
            foreach (LADDER[] ladder_temp in ladder_list)
            {
                for(int i =0 ; i<ladder_temp.Length ; i++)
                {
                    ladder_temp[i].未編譯 = true;
                }
            }
            cnt++;
        }
        void cnt_程式編譯_40_未編譯前檢查(ref byte cnt)
        {
            未編譯範圍最大值 = 0;
            未編譯範圍最小值 = 99999999;
            device_system.Set_Device("T6", 0);
            T6_未編譯檢查 = true;
            sub_未編譯檢查();
            cnt++;
        }
        void cnt_程式編譯_40_複製未編譯區塊(ref byte cnt)
        {
            程式編譯_複製未編譯區塊();
            cnt++;
        }
        void cnt_程式編譯_40_編譯前初始化(ref byte cnt)
        {
            程式編譯_輸出參數.IL指令程式.Clear();
            程式編譯_輸出參數.總編譯階梯列數 = 0;
            程式編譯_輸入參數.未編譯區塊最上方IL指令行數 = 999999;
            程式編譯_輸入參數.未編譯區塊最下方IL指令行數 = 0;
            程式編譯_輸出參數.編譯失敗次數 = 0;
            cnt++;
        }
        void cnt_程式編譯_40_檢查是否有需要編譯區域(ref byte cnt)
        {
            if (ladder_list_編譯區塊.Count > 0)
            {
                cnt++;
            }
            else
            {
                cnt = 150;
            }
        }
        void cnt_程式編譯_40_向上及向左縮排(ref byte cnt)
        {
            for (int Y = 0; Y < ladder_list_編譯區塊.Count; Y++)
            {
                for (int X = 0; X < 一列格數; X++)
                {
                    Point p0 = new Point(1, Y);
                    while (true) if (程式編譯_向上縮排(ref p0)) break;
                    程式編譯_向左縮排(ref p0);
                    Y = p0.Y;
                }
            }
            cnt++;
        }
        void cnt_程式編譯_40_計算編譯區包含的IL程式範圍(ref byte cnt)
        {
            for (int Y = 0; Y < ladder_list_編譯區塊.Count; Y++)
            {
                bool flag_Param_01 = false;
                bool flag_Param_02 = false;
                int Param_01 = 0;
                int Param_02 = 0;
                if (ladder_list_編譯區塊[Y][0].ladderParam[0] != null)
                {
                    if (ladder_list_編譯區塊[Y][0].ladderParam[0] != "")
                    {
                        Param_01 = Convert.ToInt32(ladder_list_編譯區塊[Y][0].ladderParam[0]);
                        flag_Param_01 = true;
                    }
                }
                if (ladder_list_編譯區塊[Y][0].ladderParam[0] != null && ladder_list_編譯區塊[Y][0].ladderParam[1] != null)
                {
                    if (ladder_list_編譯區塊[Y][0].ladderParam[0] != "" && ladder_list_編譯區塊[Y][0].ladderParam[1] != "")
                    {
                        Param_02 = Convert.ToInt32(ladder_list_編譯區塊[Y][0].ladderParam[1]);
                        flag_Param_02 = true;
                    }
                }
                if (flag_Param_01)
                {
                    if (Param_01 < 程式編譯_輸入參數.未編譯區塊最上方IL指令行數) 程式編譯_輸入參數.未編譯區塊最上方IL指令行數 = Param_01;

                    if (Param_01 > 程式編譯_輸入參數.未編譯區塊最下方IL指令行數) 程式編譯_輸入參數.未編譯區塊最下方IL指令行數 = Param_01;
                }
                if (flag_Param_02)
                {
                    if (Param_02 > 程式編譯_輸入參數.未編譯區塊最下方IL指令行數) 程式編譯_輸入參數.未編譯區塊最下方IL指令行數 = Param_02;
                }
            }
            cnt++;
        }
        void cnt_程式編譯_40_開始編成IL語法(ref byte cnt)
        {
            for (int Y = 0; Y < ladder_list_編譯區塊.Count; Y++)
            {
                Point p1 = new Point(1, Y);
                int cnt_step = 0;
                while (true)
                {
                    sub_程式編譯_階梯圖轉換為IL指令();
                    if (cnt_step == 0)
                    {

                        if (cnt_程式編譯_階梯圖轉換為IL指令 == 255)
                        {

                            程式編譯_階梯圖轉換為IL指令_輸入參數.list = ladder_list_編譯區塊;
                            程式編譯_階梯圖轉換為IL指令_輸入參數.X_現在值 = 1;
                            程式編譯_階梯圖轉換為IL指令_輸入參數.Y_現在值 = Y;
                            程式編譯_階梯圖轉換為IL指令_輸入參數.插入位置上方IL指令行數 = 程式編譯_輸出參數.IL指令程式.Count + 程式編譯_輸入參數.插入位置上方IL指令行數;
                            cnt_程式編譯_階梯圖轉換為IL指令 = 1;
                            cnt_step++;
                        }
                    }
                    if (cnt_step == 1)
                    {
                        if (cnt_程式編譯_階梯圖轉換為IL指令 == 255)
                        {
                            Y = 程式編譯_階梯圖轉換為IL指令_輸出參數.最下方節點位置.Y;
                            cnt_step++;
                        }
                    }
                    if (cnt_step == 2)
                    {
                        foreach (string[] str in 程式編譯_階梯圖轉換為IL指令_輸出參數.IL指令程式)
                        {
                            程式編譯_輸出參數.IL指令程式.Add(str);
                        }
                        cnt_step++;
                    }
                    if (cnt_step == 3)
                    {
                        if (程式編譯_階梯圖轉換為IL指令_輸出參數.轉換NG)
                        {
                            程式編譯_輸出參數.編譯失敗次數++;
                            程式編譯_輸入參數.彈出視窗文字 += "#003" + "{";
                            for (int i = 0; i < 程式編譯_輸出參數.IL指令程式[程式編譯_輸出參數.IL指令程式.Count - 1].Length; i++)
                            {
                                程式編譯_輸入參數.彈出視窗文字 += 程式編譯_輸出參數.IL指令程式[程式編譯_輸出參數.IL指令程式.Count - 1][i] + " ";
                            }
                            程式編譯_輸入參數.彈出視窗文字 += "}";
                        }
                        程式編譯_輸出參數.總編譯階梯列數 = 程式編譯_階梯圖轉換為IL指令_輸出參數.最下方節點位置.Y;
                        break;
                    }
                    if (cnt_step == 4)
                    {

                    }
                    if (cnt_step == 5)
                    {

                    }
                }

            }
            cnt++;
        }
        void cnt_程式編譯_40_檢查編譯結果(ref byte cnt)
        {
            bool 編譯成功 = false;
            if (程式編譯_輸出參數.編譯失敗次數 == 0) 編譯成功 = true;
            if (編譯成功)
            {
                for (int Y = 0; Y < ladder_list_編譯區塊.Count; Y++)
                {
                    for (int X = 0; X < 一列格數; X++)
                    {
                        ladder_list_編譯區塊[Y][X].未編譯 = false;
                    }
                }
                cnt++;
            }
            else
            {
                cnt = 200;
            }
        }
        void cnt_程式編譯_40_清除階梯程式(ref byte cnt)
        {
            ladder_list.Clear();
            cnt++;
        }
        void cnt_程式編譯_40_清除原本程式IL指令(ref byte cnt)
        {
            IL指令程式.Clear();
            cnt++;
        }
        void cnt_程式編譯_40_移入編譯好IL指令(ref byte cnt)
        {
            foreach (string[] str_array in 程式編譯_輸出參數.IL指令程式)
            {
                string[] str_temp = new string[str_array.Length];
                for (int i = 0; i < str_temp.Length; i++)
                {
                    str_temp[i] = str_array[i];
                }
                IL指令程式.Add(str_temp);
            }
            sub_記憶上一步();
            cnt++;
        }
  

        void cnt_程式編譯_40_插入區塊下方指令行數偏移(ref byte cnt)
        {
            程式編譯_指令行數偏移();
            cnt++;
        }
        void cnt_程式編譯_40_移除原本程式IL指令區塊(ref byte cnt)
        {
            程式編譯_移除IL指令區塊();
            cnt++;
        }
        void cnt_程式編譯_40_插入已編譯好IL指令至原本程式(ref byte cnt)
        {
            程式編譯_插入IL指令區塊();
            cnt++;
        }

        void cnt_程式編譯_150_編譯成功(ref byte cnt)
        {
            程式編譯_輸入參數.彈出視窗要顯示 = false;
            程式編譯_輸入參數.彈出視窗文字 = "編譯成功!";
            FLAG_程式編譯_成功 = true;
            FLAG_有程式未編譯 = false;
            Form_作用中表單.Text = Form_作用中表單_Text + " (" + IL指令程式.Count.ToString() + " Step)";
            cnt++;
        }
        void cnt_程式編譯_150_檢查TAB目錄(ref byte cnt)
        {
            TAB目錄.Clear();
            CallBackUI.treeview.清空節點(treeView_程式分頁);
            string[] TAB目錄_temp;
            int row = 0;
            foreach (LADDER[] ladder_temp in ladder_list)
            {
                if (ladder_temp[1].ladderType == partTypeEnum.TAB_PART) 
                {
                    if (ladder_temp[1].ladderParam[0] == "TAB")
                    {
                        TAB目錄_temp = new string[3];
                        TAB目錄_temp[0] = ladder_temp[1].ladderParam[1];
                        if (TAB目錄_temp[0].Length < 4) TAB目錄_temp[0] = "0" + TAB目錄_temp[0];
                        if (TAB目錄_temp[0].Length < 4) TAB目錄_temp[0] = "0" + TAB目錄_temp[0];
                        if (TAB目錄_temp[0].Length < 4) TAB目錄_temp[0] = "0" + TAB目錄_temp[0];
                        if (TAB目錄_temp[0].Length < 4) TAB目錄_temp[0] = "0" + TAB目錄_temp[0];
                        TAB目錄_temp[1] = row.ToString();
                        TAB目錄_temp[2] = ladder_temp[1].ladderParam[2];                     
                        TAB目錄.Add(TAB目錄_temp);
                    }
                }
                row++;
            }
            TAB目錄.Sort(new TAB目錄Comparer());
            treeView_程式分頁.BeginUpdate();
            string str_temp;
            foreach (string[] str_array in TAB目錄)
            {
                str_temp = "<" + str_array[0] + ">" + " " + str_array[2];
                CallBackUI.treeview.新增節點(str_temp, treeView_程式分頁);
            }
            treeView_程式分頁.EndUpdate();
            cnt++;
        }
        private class TAB目錄Comparer : IComparer<string[]>
        {
            // 遞增排序
            public int Compare(string[] x, string[] y)
            {
                return (x[0].CompareTo(y[0]));
            }
        }
        void cnt_程式編譯_200_編譯失敗(ref byte cnt)
        {
            //程式編譯_輸入參數.彈出視窗文字 = "編譯失敗!";
            FLAG_程式編譯_成功 = false;
            cnt++;
        }

        void cnt_程式編譯_240_等待顯示結果更新_READY(ref byte cnt)
        {
            if (cnt_ListBox_IL指令集更新 == 255)
            {
                cnt_ListBox_IL指令集更新 = 1;
                cnt++;
            }
        }
        void cnt_程式編譯_240_等待顯示結果更新_OVER(ref byte cnt)
        {
            if (cnt_ListBox_IL指令集更新 == 255)
            {
                cnt++;
            }     

            cnt++;
        }
        void cnt_程式編譯_240_彈出視窗(ref byte cnt)
        {
            if (程式編譯_輸入參數.彈出視窗要顯示)
            {
                MessageBox.Show(程式編譯_輸入參數.彈出視窗文字, " ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            cnt++;
        }
  


        bool 程式編譯_向上縮排(ref Point 檢查位置)
        {
            bool FEEDBAKE_不用向上縮排 = true;
            Point index = 檢查位置;
            Point p0 = new Point();
            Point p1 = new Point();
            p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, index.Y), ladder_list_編譯區塊);
            p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, index.Y), ladder_list_編譯區塊);
            for (int Y = p0.Y; Y <= p1.Y; Y++)
            {

                int X_temp = 1;
                int 找尋數量 = 0;
                int 非元件數量 = 0;
                if (Y != p0.Y)
                {
                    while (true)
                    {                  
                        if (X_temp <= 一列格數 - 2)
                        {
                            if (ladder_list_編譯區塊[Y - 1][X_temp].ladderType == partTypeEnum.noPart) 非元件數量++;
                            if (ladder_list_編譯區塊[Y][X_temp].ladderType != partTypeEnum.noPart) 找尋數量++;

                            if (ladder_list_編譯區塊[Y][X_temp].Vline_右上 || ladder_list_編譯區塊[Y][X_temp].Vline_右下)
                            {
        
                                if (非元件數量 == 找尋數量 && 找尋數量 > 0)
                                {
                                    for (int i = X_temp - 找尋數量 + 1; i <= X_temp; i++)
                                    {                                       
                                        ladder_list_編譯區塊[Y - 1][i].ladderType = ladder_list_編譯區塊[Y][i].ladderType;
                                        ladder_list_編譯區塊[Y - 1][i].ladderParam[0] = ladder_list_編譯區塊[Y][i].ladderParam[0];

                                        ladder_list_編譯區塊[Y][i].ladderType = partTypeEnum.noPart;
                                        ladder_list_編譯區塊[Y][i].ladderParam[0] = "";

                                        if (ladder_list_編譯區塊[Y][i].Vline_右上)
                                        {
                                            if (ladder_list_編譯區塊[Y][i + 1].ladderType == partTypeEnum.noPart)
                                            {
                                                ladder_list_編譯區塊[Y][i].Vline_右上 = false;
                                                ladder_list_編譯區塊[Y-1][i].Vline_右下 = false;
                                            }
                                        }
                                        if (ladder_list_編譯區塊[Y][i -1].Vline_右上)
                                        {
                                            if (ladder_list_編譯區塊[Y][i -1].ladderType == partTypeEnum.noPart)
                                            {
                                                ladder_list_編譯區塊[Y][i -1].Vline_右上 = false;
                                                ladder_list_編譯區塊[Y - 1][i -1].Vline_右下 = false;
                                            }
                                        }
                                        ladder_list_編譯區塊[Y][i].未編譯 = true;
                                    }
                                    FEEDBAKE_不用向上縮排 = false;
                                }
                                找尋數量 = 0;
                                非元件數量 = 0;
                         
                            }
                            X_temp++;
                  
                        }
                        else break;
                    }
                }

            }
            return FEEDBAKE_不用向上縮排;
        }
        void 程式編譯_向左縮排(ref Point 檢查位置)
        {
            while(true)
            {
                bool 縮排完成 = true;
                LADDER[] ladder_temp = new LADDER[一列格數];
                Point p0 = new Point();
                Point p1 = new Point();
                p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, 檢查位置.Y), ladder_list_編譯區塊);
                p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, 檢查位置.Y), ladder_list_編譯區塊);
                檢查位置 = new Point(0, p1.Y);
                for (int X = 1; X < 一列格數 - 2; X++)
                {
                    for (int Y = p0.Y; Y <= p1.Y; Y++)
                    {
                        #region 元件向左縮排
                        bool 上方有豎線 = false;
                        bool 下方有豎線 = false;

                        if (X + 1 < 一列格數 - 2)
                        {
                            if (ladder_list_編譯區塊[Y][X].ladderType == partTypeEnum.H_Line_Part)
                            {
                                if (ladder_list_編譯區塊[Y][X].Vline_右上)
                                {
                                    上方有豎線 = true;

                                }
                                if (ladder_list_編譯區塊[Y][X].Vline_右下)
                                {
                                    下方有豎線 = true;

                                }
                                if (!上方有豎線 && !下方有豎線)
                                {
                                    if (ladder_list_編譯區塊[Y][X + 1].ladderType == partTypeEnum.A_Part || ladder_list_編譯區塊[Y][X + 1].ladderType == partTypeEnum.B_Part )
                                    {
                                        int 搬移個數 = ladder_list_編譯區塊[Y][X + 1].元素數量;
                                        for (int i = 0; i < 搬移個數; i++)
                                        {
                                            ladder_list_編譯區塊[Y][X].ladderType = ladder_list_編譯區塊[Y][X + 1].ladderType;
                                            ladder_list_編譯區塊[Y][X].ladderParam[i] = ladder_list_編譯區塊[Y][X + 1].ladderParam[i];
                                            ladder_list_編譯區塊[Y][X + 1].ladderType = partTypeEnum.H_Line_Part;
                                        }                  
                                        縮排完成 = false;
                                    }
                                    else if(ladder_list_編譯區塊[Y][X + 1].ladderType == partTypeEnum.LD_Equal_Part)
                                    {
                                        int 搬移個數 = ladder_list_編譯區塊[Y][X + 1].元素數量;
                                        ladder_list_編譯區塊[Y][X].ladderType = ladder_list_編譯區塊[Y][X + 1].ladderType;
                                        ladder_list_編譯區塊[Y][X].元素數量 = ladder_list_編譯區塊[Y][X + 1].元素數量;
                                        for (int i = 0; i < 搬移個數; i++)
                                        {                                    
                                            ladder_list_編譯區塊[Y][X].ladderParam[i] = ladder_list_編譯區塊[Y][X + 1].ladderParam[i];                                        
                                        }
                           
                                        for (int i = 1; i < 搬移個數 - 1 ; i++)
                                        {
                                            LADDER ladder_temp0 = new LADDER(1);
                                            ladder_temp0.ladderType = partTypeEnum.Data_no_Part;
                                            ladder_list_編譯區塊[Y][X + i] = ladder_temp0;
                                            ladder_list_編譯區塊[Y][X + i].未編譯 = false;
                                        }
                                        ladder_list_編譯區塊[Y][X + 搬移個數].ladderType = partTypeEnum.H_Line_Part;
                                    }
                                }
                            }

                        }
                        #endregion
                    }
                }
                for (int X = 1; X < 一列格數 - 2; X++)
                {
                    for (int Y = p0.Y; Y <= p1.Y; Y++)
                    {
                        #region 豎線向左縮排
                        bool 上方有豎線 = false;
                        bool 下方有豎線 = false;
                        bool 豎線縮排異常 = false;
                        Point 上方豎線末點 = new Point();
                        Point 下方豎線末點 = new Point();
                        if (X + 1 <= 一列格數 - 2)
                        {
                            if (ladder_list_編譯區塊[Y][X].ladderType == partTypeEnum.H_Line_Part)
                            {
                                if (ladder_list_編譯區塊[Y][X].Vline_右上)
                                {
                                    上方有豎線 = true;

                                }
                                if (ladder_list_編譯區塊[Y][X].Vline_右下)
                                {
                                    下方有豎線 = true;

                                }
                                上方豎線末點 = new Point(X, Y);
                                下方豎線末點 = new Point(X, Y);
                            }
                            while (上方有豎線)
                            {
                                if (上方豎線末點.Y < 0)
                                {
                                    豎線縮排異常 = true;
                                    break;
                                }
                                if (!ladder_list_編譯區塊[上方豎線末點.Y][上方豎線末點.X].Vline_右上)
                                {
                                    break;
                                }
                                上方豎線末點.Y--;

                            }
                            while (下方有豎線)
                            {
                                if (下方豎線末點.Y >= ladder_list_編譯區塊.Count)
                                {
                                    豎線縮排異常 = true;
                                    break;
                                }
                                if (!ladder_list_編譯區塊[下方豎線末點.Y][下方豎線末點.X].Vline_右下)
                                {
                                    break;
                                }
                                下方豎線末點.Y++;
                            }
                            if (上方有豎線 || 下方有豎線)
                            {
                                if (!豎線縮排異常)
                                {
                                    int 橫線數量 = 0;
                                    int 空元件數量 = 0;
                                    int 一排元件數量 = 下方豎線末點.Y - 上方豎線末點.Y + 1;
                                    for (int i = 上方豎線末點.Y; i <= 下方豎線末點.Y; i++)
                                    {
                                        if (ladder_list_編譯區塊[i][X].ladderType == partTypeEnum.H_Line_Part) 橫線數量++;
                                        if (ladder_list_編譯區塊[i][X].ladderType == partTypeEnum.noPart) 空元件數量++;
                                    }
                                    if (一排元件數量 == 橫線數量 + 空元件數量)
                                    {
                                        for (int i = 上方豎線末點.Y; i <= 下方豎線末點.Y; i++)
                                        {
                                            if (X - 1 > 0)
                                            {
                                                ladder_list_編譯區塊[i][X - 1].Vline_右上 = ladder_list_編譯區塊[i][X].Vline_右上;
                                                ladder_list_編譯區塊[i][X - 1].Vline_右下 = ladder_list_編譯區塊[i][X].Vline_右下;
                                                ladder_list_編譯區塊[i][X].Vline_右上 = false;
                                                ladder_list_編譯區塊[i][X].Vline_右下 = false;
                                            }
                                            if (X + 1 <= 一列格數 - 2)
                                            {
                                                if (ladder_list_編譯區塊[i][X + 1].ladderType != partTypeEnum.noPart)
                                                {
                                                    ladder_list_編譯區塊[i][X].ladderType = partTypeEnum.H_Line_Part;
                                                }
                                                else
                                                {
                                                    ladder_list_編譯區塊[i][X].ladderType = partTypeEnum.noPart;
                                                }
                                            }

                                        }
                                        縮排完成 = false;
                                    }
                                }
                            }
                        }
                        #endregion

                    }
                }
                if (縮排完成) break;
            }
            
        }

        bool 程式編譯_移除未編譯區塊()
        {
            bool FLAG = false;
            if(FLAG_有程式未編譯)
            {
                int range = 未編譯範圍最大值 - 未編譯範圍最小值 + 1;
                ladder_list.RemoveRange(未編譯範圍最小值, range);
                FLAG = true;
            }
            return FLAG;
        }
        bool 程式編譯_複製未編譯區塊()
        {
            ladder_list_編譯區塊.Clear();
            int 複製起始點 = 未編譯範圍最小值;
            int 複製結束點 = 未編譯範圍最大值;
            int Y_現在值 = 0;
            foreach (LADDER[] list_array in ladder_list)
            {
                LADDER[] array = new LADDER[list_array.Length];
                for (int i = 0; i < list_array.Length; i++)
                {
                    array[i] = new LADDER(1);
                    array[i] = list_array[i];
                    array[i].未編譯 = true;
                }
                if (Y_現在值 > 複製結束點) break;
                if (Y_現在值 >= 複製起始點) ladder_list_編譯區塊.Add(array);
                Y_現在值++;
            }
            編譯區塊首列位置 = 複製起始點;

            if (複製起始點 != 99999999)
            {
                程式編譯_輸入參數.插入位置上方IL指令行數 = 0;
                程式編譯_輸入參數.起始編譯階梯列數 = 0;
                #region 搜尋上個IL行數及階梯列數
                int 搜尋起點 = 複製起始點 - 1;
                while (搜尋起點 >= 0)
                {
                    if (搜尋起點 < 複製起始點)
                    {
                        Point p0 = new Point();
                        Point p1 = new Point();
                        p0 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, 搜尋起點), ladder_list);
                        p1 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, 搜尋起點), ladder_list);
                        if (p0.Y == p1.Y)
                        {
                            if (ladder_list[p0.Y][0].ladderParam[1] != "")
                            {
                                程式編譯_輸入參數.起始編譯階梯列數 = p0.Y + 1;
                                程式編譯_輸入參數.插入位置上方IL指令行數 = Convert.ToInt32(ladder_list[p0.Y][0].ladderParam[1]) + 1;
                                break;
                            }
                        }
                        else
                        {
                            if (ladder_list[p0.Y][0].ladderParam[0] != "")
                            {
                                程式編譯_輸入參數.起始編譯階梯列數 = p0.Y + 1;
                                程式編譯_輸入參數.插入位置上方IL指令行數 = Convert.ToInt32(ladder_list[p0.Y][0].ladderParam[0]) + 1;
                                break;
                            }
                        }
                    }

                    搜尋起點--;
                }
                #endregion
            }
     
            return true;
        }
        void 程式編譯_插入未編譯區塊()
        {     
                for (int Y = ladder_list_編譯區塊.Count - 1; Y >= 0; --Y)
                {
                    ladder_list.Insert(編譯區塊首列位置, ladder_list_編譯區塊[Y]);
                }

                ladder_list_編譯區塊.Clear();         
        }
        void 程式編譯_指令行數偏移()
        {
            #region 插入區塊下方IL指令行數偏移

            程式編譯_輸出參數.編譯完偏移行數 = 0;
            int Y_temp = 0;
            foreach (LADDER[] list_array in ladder_list)
            {
                if (Y_temp == (程式編譯_輸入參數.起始編譯階梯列數 + 程式編譯_輸出參數.總編譯階梯列數 + 1))
                {
                    Point p0_上一列 = new Point();
                    Point p1_上一列 = new Point();
                    Point p0_當前列 = new Point();
                    int 上一列程式行數 = 0;
                    int 當前列程式行數 = 0;
                    p0_上一列 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, Y_temp - 1), ladder_list);
                    p1_上一列 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, Y_temp - 1), ladder_list);
                    if (p0_上一列.Y == p1_上一列.Y)
                    {
                        if (ladder_list[Y_temp - 1][0].ladderParam[1] != "") 上一列程式行數 = (Convert.ToInt32(ladder_list[Y_temp - 1][0].ladderParam[1]));
                    }
                    else
                    {
                        if (ladder_list[Y_temp - 1][0].ladderParam[0] != "") 上一列程式行數 = (Convert.ToInt32(ladder_list[Y_temp - 1][0].ladderParam[0]));
                    }
                    p0_當前列 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, Y_temp), ladder_list);
                    if (ladder_list[Y_temp][0].ladderParam[0] != "") 當前列程式行數 = (Convert.ToInt32(ladder_list[Y_temp][0].ladderParam[0]));
                    程式編譯_輸入參數.插入位置下方IL指令行數 = 當前列程式行數;
                    程式編譯_輸出參數.編譯完偏移行數 = 上一列程式行數 + 1 - 程式編譯_輸入參數.插入位置下方IL指令行數;
                }
                if (Y_temp > (程式編譯_輸入參數.起始編譯階梯列數 + 程式編譯_輸出參數.總編譯階梯列數))
                {
                    if (list_array[0].ladderParam[0] != "" && list_array[0].ladderParam[0] != null) list_array[0].ladderParam[0] = (Convert.ToInt32(list_array[0].ladderParam[0]) + 程式編譯_輸出參數.編譯完偏移行數).ToString();
                    if (list_array[0].ladderParam[1] != "" && list_array[0].ladderParam[1] != null) list_array[0].ladderParam[1] = (Convert.ToInt32(list_array[0].ladderParam[1]) + 程式編譯_輸出參數.編譯完偏移行數).ToString();
                }
                Y_temp++;
            }
            #endregion          
        }


        void 程式編譯_移除IL指令區塊()
        {
            int 移除起點 = 程式編譯_輸入參數.未編譯區塊最上方IL指令行數;
            int 移除個數 = 程式編譯_輸入參數.未編譯區塊最下方IL指令行數 - 程式編譯_輸入參數.未編譯區塊最上方IL指令行數 +1;
            if (程式編譯_輸入參數.未編譯區塊最上方IL指令行數 != 999999) IL指令程式.RemoveRange(移除起點, 移除個數);
        }
        void 程式編譯_插入IL指令區塊()
        {
            int Y = 0;
            foreach (string[] str_array in 程式編譯_輸出參數.IL指令程式)
            {
                string[] str_temp = new string[str_array.Length];
                for(int i = 0 ; i < str_temp.Length ; i++)
                {
                    str_temp[i] = str_array[i];
                }
                IL指令程式.Insert((程式編譯_輸入參數.插入位置上方IL指令行數) + Y, str_temp);
                Y++;
            }
            sub_記憶上一步();
        }

        bool 程式編譯_指定格向右搜索最末點(Point 檢查位置, int 搜索行數, ref  Point 線路節點, List<LADDER[]> list)
        {
            bool 搜尋成功 = true;
            Point p0 = new Point();
            p0 = 檢查位置;
            bool 有找到線段 = false;
            int 檢查次數 = 0;
            while (true)
            {
                if (p0.Y + 1 < list.Count)
                {
                    if (list[p0.Y][p0.X].Vline_右下 || list[p0.Y + 1][p0.X].Vline_右上)
                    {

                        // if (ladder_list[p0.Y + 1][p0.X].ladderType != partTypeEnum.noPart)

                        有找到線段 = true;
                    }
                }

                if (有找到線段 == true)
                {
                    線路節點 = p0;
                    搜尋成功 = true;
                    break;
                }
                else
                {
                    if (檢查次數 < 搜索行數)
                    {
                        p0.X++;
                        檢查次數++;
                    }
                    else
                    {
                        搜尋成功 = false;
                        break;
                    }
                }
            }
            return 搜尋成功;
        }
        bool 程式編譯_指定格向左搜索最末點(Point 檢查位置, int 搜索行數, ref  Point 線路節點, List<LADDER[]> list)
        {
            bool 搜尋成功 = true;
            Point p0 = new Point();
            p0 = 檢查位置;
            bool 有找到線段 = false;
            int 檢查次數 = 1;
            while (true)
            {
                if (檢查次數 > 搜索行數)
                {
                    搜尋成功 = false;
                    break;
                }
                if (p0.X - 檢查次數 >= 0)
                {
                    if (list[p0.Y][p0.X - 檢查次數].Vline_右上)
                    {
                        有找到線段 = true;
                    }
                }

                if (有找到線段 == true)
                {
                    線路節點 = new Point(p0.X - 檢查次數 + 1, p0.Y);
                    搜尋成功 = true;
                    break;
                }
                檢查次數++;
            }
            return 搜尋成功;
        }
        void 程式編譯_指定格向下連結性檢查(ref Point 檢查位置)
        {
            while (true)
            {
                if (ladder_list[檢查位置.Y][檢查位置.X].Vline_右下)
                {
                    if (檢查位置.Y < ladder_list.Count - 1) 檢查位置.Y++;
                    else break;
                }
                else
                {
                    break;
                }
            }
        }


        bool 程式編譯_檢查可編譯()
        {
            for (int Y = 0; Y < ladder_list.Count; Y++)
            {
                for (int X = 1; X < 一列格數; X++)
                {
                    if (ladder_list[Y][X].未編譯)
                    {
                        Point p0 = new Point(1, Y);
                        if (程式編譯_斷線檢查(ref p0))
                        {
                            return false;
                        }
                        Y = p0.Y;
                        break;
                    }
                }
            }
            return true;
        }
        bool 程式編譯_空行縮列檢查()
        {
            bool FEEDBACK_無空行要縮列 = true;
            for (int Y = 0; Y < ladder_list.Count; Y++)
            {
                bool 此列空列 = true;
                for (int X = 1; X < 一列格數 - 2; X++)
                {
                    if (ladder_list[Y][X].ladderType != partTypeEnum.noPart) 此列空列 = false;
                }
                if (此列空列)
                {
                    sub_刪除一列(Y, false);
                    FEEDBACK_無空行要縮列 = false;
                }
            }
            return FEEDBACK_無空行要縮列;
        }
        bool 程式編譯_斷線檢查(ref Point 檢查位置)
        {
            bool FLAG_有斷線 = false; ;
            Point index = 檢查位置;
            LADDER[] ladder_temp = new LADDER[一列格數];
            Point p0 = new Point();
            Point p1 = new Point();
            p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, index.Y), ladder_list);
            p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, index.Y), ladder_list);
            檢查位置.Y = p1.Y;
            for (int Y = p0.Y; Y <= p1.Y; Y++)
            {
                bool FLAG_有BLOCK區域 = false;
                int 空元件數量 = 0;

                for (int X = index.X; X < 一列格數 - 1; X++)
                {
                    if (ladder_list[Y][X].ladderType == partTypeEnum.noPart) 空元件數量++;
                    if (Y == p0.Y) //首行必須全部有元素
                    {
                        if (X >= 1)
                        {
                            if (ladder_list[Y][X].ladderType == partTypeEnum.noPart)
                            {
                                FLAG_有斷線 = true;
                            }
                        }
                    }

                    else
                    {
                        if (X >= 1)//遇到元素左邊必須有豎線,到沒元素時右邊必須有豎線
                        {
                            if (!FLAG_有BLOCK區域)
                            {
                                if (ladder_list[Y][X].ladderType != partTypeEnum.noPart)
                                {
                                    if (ladder_list[Y][X - 1].Vline_右上 && ladder_list[Y - 1][X - 1].Vline_右下)
                                    {
                                        FLAG_有BLOCK區域 = true;
                                    }
                                    else
                                    {
                                        FLAG_有斷線 = true;
                                    }
                                }
                            }
                            else if (FLAG_有BLOCK區域)
                            {
                                if (ladder_list[Y][X].ladderType == partTypeEnum.noPart)
                                {
                                    if (ladder_list[Y][X - 1].Vline_右上 && ladder_list[Y - 1][X - 1].Vline_右下)
                                    {
                                        FLAG_有BLOCK區域 = false;
                                    }
                                    else
                                    {
                                        FLAG_有斷線 = true;
                                    }
                                }
                            }

                            if (ladder_list[Y][X].Vline_右上)
                            {
                                if (!ladder_list[Y][X].Vline_右下)
                                {
                                    if (ladder_list[Y][X].ladderType == partTypeEnum.noPart)
                                    {
                                        if (ladder_list[Y][X + 1].ladderType == partTypeEnum.noPart) FLAG_有斷線 = true;
                                    }

                                }
                            }
                        }
                    }
                }
                if (空元件數量 == 10) FLAG_有斷線 = false;
            }
            return FLAG_有斷線;
        }

        bool sub_程式編譯_線路合法性檢查(ref Point 檢查位置)
        {
            bool feedback_線路合法 = true;
            Point index = 檢查位置;
            LADDER[] ladder_temp = new LADDER[一列格數];
            List<Point[]> List_迴路點 = new List<Point[]>();
            Point p0 = new Point();
            Point p1 = new Point();
            p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, index.Y), ladder_list_編譯區塊);
            p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, index.Y), ladder_list_編譯區塊);
            檢查位置.Y = p1.Y;

            /*for (int Y = p0.Y; Y <= p1.Y; Y++)
            {
                int 搜尋列數 = p1.Y - Y;
                if (sub_程式編譯_向右搜索封閉迴路(Y, 搜尋列數, ref List_迴路點)) feedback_線路合法 = false;       
            }
            if (!sub_程式編譯_檢查封閉迴路是否異常(List_迴路點)) feedback_線路合法 = false;*/
            return feedback_線路合法;
        }
        bool sub_程式編譯_向右搜索封閉迴路(int 指定列數, int 搜尋列數, ref List<Point[]> List_迴路點)
        {
            int 向右搜索行數;
            int 向左搜索行數;

            Point 右上 = new Point();
            Point 右下 = new Point();
            Point 左上 = new Point();
            Point 左下 = new Point();
            Point 驗算位置 = new Point();
            Point 檢查起始點 = new Point(1, 指定列數);
            int 向下搜尋現在列數 = 0;
            int 向上搜尋現在列數 = 0;
            bool 有錯誤線路 = false;
            while (true)
            {
                bool 右上_OK = false;
                bool 右下_OK = false;
                bool 左上_OK = false;
                bool 左下_OK = false;
                bool 驗算_OK = false;

                向右搜索行數 = (一列格數 - 2) - 檢查起始點.X;
                if (向右搜索行數 == 0) break;
                if (程式編譯_指定格向右搜索最末點(檢查起始點, 向右搜索行數, ref 右上, ladder_list_編譯區塊))
                {
                    if (ladder_list_編譯區塊[右上.Y][右上.X].ladderType != partTypeEnum.noPart) 右上_OK = true;
                }
                else
                {
                    //有錯誤線路 = true;
                    break;
                }
                檢查起始點 = 右上;
                if (!右上_OK) 檢查起始點.X++;

                if (右上_OK)
                {
                    向下搜尋現在列數 = 1;
                    while (true)
                    {
                        if (向下搜尋現在列數 > 搜尋列數) break;
                        if (ladder_list_編譯區塊[右上.Y + 向下搜尋現在列數][右上.X].Vline_右上)
                        {
                            if (ladder_list_編譯區塊[右上.Y + 向下搜尋現在列數][右上.X].ladderType != partTypeEnum.noPart)
                            {
                                右下 = 右上;
                                右下.Y += 向下搜尋現在列數;
                                右下_OK = true;
                                break;
                            }

                        }
                        向下搜尋現在列數++;
                    }

                }
                if (右下_OK)
                {
                    向左搜索行數 = 右下.X;
                    if (向左搜索行數 == 0) break;
                    if (程式編譯_指定格向左搜索最末點(右下, 向左搜索行數, ref 左下, ladder_list_編譯區塊))
                    {
                        左下_OK = true;
                    }
                    else
                    {
                        //  有錯誤線路 = true;
                        break;
                    }
                }
                if (左下_OK)
                {
                    向上搜尋現在列數 = 1;
                    int 向上搜尋最大列數 = 右下.Y - 右上.Y;
                    while (true)
                    {
                        if (向上搜尋現在列數 >= 向上搜尋最大列數)
                        {
                            if (ladder_list_編譯區塊[左下.Y - 向上搜尋現在列數][左下.X].ladderType != partTypeEnum.noPart)
                            {
                                左上 = 左下;
                                左上.Y -= 向上搜尋現在列數;
                                左上_OK = true;
                                break;
                            }
                        }

                        if (ladder_list_編譯區塊[左下.Y - 向上搜尋現在列數][左下.X - 1].Vline_右下)
                        {
                            向上搜尋現在列數++;
                        }
                        else break;
                    }
                }
                if (左上_OK)
                {
                    Point p0 = new Point();
                    p0.X = 右上.X;
                    if (左上.Y <= 右上.Y) p0.Y = 左上.Y;
                    else p0.Y = 右上.Y;

                    if (ladder_list_編譯區塊[p0.Y][p0.X].ladderType != partTypeEnum.noPart && ladder_list_編譯區塊[p0.Y][p0.X].Vline_右下)
                    {
                        右上 = p0;
                        驗算_OK = true;
                    }
                    else
                    {
                        有錯誤線路 = true;
                    }

                }
                if (右上_OK && 右下_OK && 左上_OK && 左下_OK && 驗算_OK)
                {
                    向右搜索行數 = (一列格數 - 2) - 左上.X;

                    Point[] p = new Point[4];
                    p[0] = 左上;
                    p[1] = 右上;
                    p[2] = 左下;
                    p[3] = 右下;
                    List_迴路點.Add(p);

                }
                if (驗算位置 != 右上)
                {
                    檢查起始點.X++;
                }
                else
                {
                    檢查起始點.X = 右上.X + 1;
                }

            }
            return 有錯誤線路;
        }
        bool sub_程式編譯_檢查封閉迴路是否異常(List<Point[]> List_迴路點)
        {
            bool 檢查_OK = true;
            foreach (Point[] po_array in List_迴路點)
            {
                int 起始點;
                int 結束點;
                int 檢查列;
                int H_Line_上限;
                int H_Line_現在值;
                起始點 = po_array[0].X;
                結束點 = po_array[1].X;
                檢查列 = po_array[0].Y;
                H_Line_上限 = Math.Abs(結束點 - 起始點);
                H_Line_現在值 = 0;
                for (int i = 起始點; i <= 結束點; i++)
                {
                    if (ladder_list_編譯區塊[檢查列][i].ladderType == partTypeEnum.H_Line_Part)
                    {
                        H_Line_現在值++;
                    }
                    if (ladder_list_編譯區塊[檢查列][i].ladderType == partTypeEnum.noPart)
                    {
                        檢查_OK = false;
                    }

                }
                if (H_Line_現在值 > H_Line_上限)
                {
                    檢查_OK = false;
                }

                起始點 = po_array[2].X;
                結束點 = po_array[3].X;
                檢查列 = po_array[2].Y;
                H_Line_現在值 = 0;
                for (int i = 起始點; i <= 結束點; i++)
                {
                    if (ladder_list_編譯區塊[檢查列][i].ladderType == partTypeEnum.H_Line_Part)
                    {
                        H_Line_現在值++;
                    }
                    if (ladder_list_編譯區塊[檢查列][i].ladderType == partTypeEnum.noPart)
                    {
                        檢查_OK = false;
                    }
                }
                if (H_Line_現在值 > H_Line_上限)
                {
                    檢查_OK = false;
                }
            }

            return 檢查_OK;
        }



        #endregion  
        #region 程式反編譯
        static private class 程式編譯_IL轉換為階梯圖_輸入參數
        {
            static public List<String[]> IL指令程式 = new List<string[]>();
            static public string 彈出視窗訊息 = "";
            static public bool 彈出視窗要顯示 = true;
       
        }
        static private class 程式編譯_IL轉換為階梯圖_輸出參數
        {
            static public List<LADDER[]> list = new List<LADDER[]>();
            static public bool 轉換NG = false;
            static public Point 最上方節點位置 = new Point();
            static public Point 最下方節點位置 = new Point();
        }
        private class _程式編譯_IL轉換為階梯圖
        {
            public _程式編譯_IL轉換為階梯圖(List<String[]> _IL指令程式)
            {
                this.IL指令程式 = _IL指令程式;     
            }
            public List<LADDER[]> list = new List<LADDER[]>();
            public LADDER 上方元件 = new LADDER(1);
            public LADDER 下方元件 = new LADDER(1);
            public LADDER 左方元件 = new LADDER(1);
            public LADDER 本身元件 = new LADDER(1);
            public Point 最上方節點位置 = new Point();
            public Point 最下方節點位置 = new Point();
            public List<IL節點> IL節點堆疊區 = new List<IL節點>();
            public List<String[]> IL指令程式 = new List<string[]>();
            public String[] 當前指令;
            public bool 轉換NG = false;
            public int 錯誤迴路計數設定值 = 3;
            public int 錯誤迴路計數現在值 = 0;
            public bool 下方有空列 = false;
            public int X_上一個區塊末端 = 0;
            public int Y_上一個區塊末端 = 0;
            public int X_現在值 = 0;
            public int Y_現在值 = 0;
            public int 空列數位置 = 0;
   
        }
        byte cnt_程式編譯_IL轉換為階梯圖 = 255;
        void sub_程式編譯_IL轉換為階梯圖()
        {
            while(true)
            {
                if (cnt_程式編譯_IL轉換為階梯圖 == 255) break;
                if (cnt_程式編譯_IL轉換為階梯圖 == 1) sub_程式編譯_IL轉換為階梯圖_00_初始化(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 2) cnt_程式編譯_IL轉換為階梯圖 = 10;

                if (cnt_程式編譯_IL轉換為階梯圖 == 10) sub_程式編譯_IL轉換為階梯圖_10_檢查有無指令需要編譯(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 11) sub_程式編譯_IL轉換為階梯圖_10_檢查空列數(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 12) sub_程式編譯_IL轉換為階梯圖_10_檢查指令種類(ref cnt_程式編譯_IL轉換為階梯圖);

                if (cnt_程式編譯_IL轉換為階梯圖 == 20) sub_程式編譯_IL轉換為階梯圖_20_LD_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 21) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 30) sub_程式編譯_IL轉換為階梯圖_30_LDI_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 31) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 40) sub_程式編譯_IL轉換為階梯圖_40_AND_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 41) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 50) sub_程式編譯_IL轉換為階梯圖_50_AN1_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 51) cnt_程式編譯_IL轉換為階梯圖 = 240;


                if (cnt_程式編譯_IL轉換為階梯圖 == 60) sub_程式編譯_IL轉換為階梯圖_20_LD_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 61) sub_程式編譯_IL轉換為階梯圖_60_OR_找尋上個區塊末端位置(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 62) sub_程式編譯_IL轉換為階梯圖_60_OR_繪製豎線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 63) sub_程式編譯_IL轉換為階梯圖_60_OR_繪製橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 64) sub_程式編譯_IL轉換為階梯圖_60_OR_找尋上個區塊末端位置(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 65) cnt_程式編譯_IL轉換為階梯圖 = 240;


                if (cnt_程式編譯_IL轉換為階梯圖 == 70) sub_程式編譯_IL轉換為階梯圖_30_LDI_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 71) sub_程式編譯_IL轉換為階梯圖_70_ORI_找尋上個區塊末端位置(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 72) sub_程式編譯_IL轉換為階梯圖_70_ORI_繪製豎線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 73) sub_程式編譯_IL轉換為階梯圖_70_ORI_繪製橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 74) sub_程式編譯_IL轉換為階梯圖_70_ORI_找尋上個區塊末端位置(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 75) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 80) sub_程式編譯_IL轉換為階梯圖_80_ORB_找尋上個區塊末端位置(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 81) sub_程式編譯_IL轉換為階梯圖_80_ORB_繪製橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 82) sub_程式編譯_IL轉換為階梯圖_80_ORB_繪製豎線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 83) sub_程式編譯_IL轉換為階梯圖_80_ORB_檢查是否需要折豎線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 84) sub_程式編譯_IL轉換為階梯圖_80_ORB_找尋上個區塊末端位置(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 85) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 90) sub_程式編譯_IL轉換為階梯圖_90_ANB_找尋上個區塊末端位置(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 91) sub_程式編譯_IL轉換為階梯圖_90_ANB_檢查區塊範圍且移動區塊(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 92) sub_程式編譯_IL轉換為階梯圖_90_ANB_更新現在位置(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 93) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 100) sub_程式編譯_IL轉換為階梯圖_100_OUT_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 101) sub_程式編譯_IL轉換為階梯圖_100_OUT_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 102) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 110) sub_程式編譯_IL轉換為階梯圖_110_END_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 111) sub_程式編譯_IL轉換為階梯圖_110_END_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 112) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 120) sub_程式編譯_IL轉換為階梯圖_120_LD_equal_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 121) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 125) sub_程式編譯_IL轉換為階梯圖_125_AND_equal_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 126) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 130) sub_程式編譯_IL轉換為階梯圖_130_OR_equal_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 131) sub_程式編譯_IL轉換為階梯圖_130_OR_equal_找尋上個區塊末端位置(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 132) sub_程式編譯_IL轉換為階梯圖_130_OR_equal_繪製橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 133) sub_程式編譯_IL轉換為階梯圖_130_OR_equal_繪製豎線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 134) sub_程式編譯_IL轉換為階梯圖_130_OR_equal_檢查是否需要折豎線(ref cnt_程式編譯_IL轉換為階梯圖);              
                if (cnt_程式編譯_IL轉換為階梯圖 == 135) sub_程式編譯_IL轉換為階梯圖_130_OR_equal_找尋上個區塊末端位置(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 136) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 140) sub_程式編譯_IL轉換為階梯圖_140_MOV_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 141) sub_程式編譯_IL轉換為階梯圖_140_MOV_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 142) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 145) sub_程式編譯_IL轉換為階梯圖_145_ADD_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 146) sub_程式編譯_IL轉換為階梯圖_145_ADD_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 147) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 150) sub_程式編譯_IL轉換為階梯圖_150_SUB_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 151) sub_程式編譯_IL轉換為階梯圖_150_SUB_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 152) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 155) sub_程式編譯_IL轉換為階梯圖_155_MUL_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 156) sub_程式編譯_IL轉換為階梯圖_155_MUL_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 157) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 160) sub_程式編譯_IL轉換為階梯圖_160_DIV_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 161) sub_程式編譯_IL轉換為階梯圖_160_DIV_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 162) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 165) sub_程式編譯_IL轉換為階梯圖_165_INC_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 166) sub_程式編譯_IL轉換為階梯圖_165_INC_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 167) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 170) sub_程式編譯_IL轉換為階梯圖_170_DRVA_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 171) sub_程式編譯_IL轉換為階梯圖_170_DRVA_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 172) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 180) sub_程式編譯_IL轉換為階梯圖_180_DRVI_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 181) sub_程式編譯_IL轉換為階梯圖_180_DRVI_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 182) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 185) sub_程式編譯_IL轉換為階梯圖_185_PLSV_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 186) sub_程式編譯_IL轉換為階梯圖_185_PLSV_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 187) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 190) sub_程式編譯_IL轉換為階梯圖_190_SET_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 191) sub_程式編譯_IL轉換為階梯圖_190_SET_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 192) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 195) sub_程式編譯_IL轉換為階梯圖_195_RST_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 196) sub_程式編譯_IL轉換為階梯圖_195_RST_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 197) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 200) sub_程式編譯_IL轉換為階梯圖_200_ZRST_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 201) sub_程式編譯_IL轉換為階梯圖_200_ZRST_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 202) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 205) sub_程式編譯_IL轉換為階梯圖_205_BMOV_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 206) sub_程式編譯_IL轉換為階梯圖_205_BMOV_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 207) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 210) sub_程式編譯_IL轉換為階梯圖_210_WTB_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 211) sub_程式編譯_IL轉換為階梯圖_210_WTB_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 212) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 215) sub_程式編譯_IL轉換為階梯圖_215_BTW_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 216) sub_程式編譯_IL轉換為階梯圖_215_BTW_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 217) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 220) sub_程式編譯_IL轉換為階梯圖_220_TAB_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 221) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 225) sub_程式編譯_IL轉換為階梯圖_225_JUMP_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 226) sub_程式編譯_IL轉換為階梯圖_226_JUMP_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 227) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 230) sub_程式編譯_IL轉換為階梯圖_230_REF_階梯圖寫入(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 231) sub_程式編譯_IL轉換為階梯圖_230_REF_當列補橫線(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 232) cnt_程式編譯_IL轉換為階梯圖 = 240;

                if (cnt_程式編譯_IL轉換為階梯圖 == 240) sub_程式編譯_IL轉換為階梯圖_240_IL指令程式移除一行(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 241) cnt_程式編譯_IL轉換為階梯圖 = 10;

                if (cnt_程式編譯_IL轉換為階梯圖 == 250) sub_程式編譯_IL轉換為階梯圖_250_反編譯彈出視窗(ref cnt_程式編譯_IL轉換為階梯圖);
                if (cnt_程式編譯_IL轉換為階梯圖 == 251) cnt_程式編譯_IL轉換為階梯圖 = 255;
            }
  
            
        }
        _程式編譯_IL轉換為階梯圖 程式編譯_IL轉換為階梯圖;
        void sub_程式編譯_IL轉換為階梯圖_00_初始化(ref byte cnt)
        {

            程式編譯_IL轉換為階梯圖 = new _程式編譯_IL轉換為階梯圖(程式編譯_IL轉換為階梯圖_輸入參數.IL指令程式);
          
            cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_10_檢查有無指令需要編譯(ref byte cnt)
        {
            程式編譯_IL轉換為階梯圖_輸出參數.轉換NG = 程式編譯_IL轉換為階梯圖.轉換NG;
            if (程式編譯_IL轉換為階梯圖.轉換NG)
            {
                程式編譯_IL轉換為階梯圖_輸入參數.彈出視窗訊息 = "反編譯失敗!";
                cnt = 250;
            }
            else
            {
                if (程式編譯_IL轉換為階梯圖.IL指令程式.Count == 0)
                {
                   
                    程式編譯_IL轉換為階梯圖_輸入參數.彈出視窗訊息 = "反編譯成功!";
                    cnt = 250;
                }
                else
                {
                    cnt++;
                }
            }  
        }
        void sub_程式編譯_IL轉換為階梯圖_10_檢查空列數(ref byte cnt)
        {
            int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
            if (程式編譯_IL轉換為階梯圖.list.Count == 0) 程式編譯_IL轉換為階梯圖.list.Add(程式編譯_IL轉換為階梯圖_創建空一列());
            while(true)
            {
                if (Y_temp >= 程式編譯_IL轉換為階梯圖.list.Count)
                {
                    程式編譯_IL轉換為階梯圖.轉換NG = true;
                    cnt = 10;
                    break;
                }

                bool 此列無元件 = true;
                for (int X = 1; X < 一列格數 - 1; X++)
                {
                    if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType != partTypeEnum.noPart) 此列無元件 = false;
                }

                if (此列無元件)
                {
                    程式編譯_IL轉換為階梯圖.空列數位置 = Y_temp;
                    break;
                }
                else
                {
                    if ((Y_temp + 1) >= 程式編譯_IL轉換為階梯圖.list.Count) 程式編譯_IL轉換為階梯圖.list.Add(程式編譯_IL轉換為階梯圖_創建空一列());
                }       
                Y_temp++;           
            }
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_10_檢查指令種類(ref byte cnt)
        {
            程式編譯_IL轉換為階梯圖.當前指令 = 程式編譯_IL轉換為階梯圖.IL指令程式[0];
            if (程式編譯_IL轉換為階梯圖.當前指令[0] == "LD") cnt = 20;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "LDI") cnt = 30;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "AND") cnt = 40;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ANI") cnt = 50;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "OR") cnt = 60;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ORI") cnt = 70;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ORB") cnt = 80;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ANB") cnt = 90;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "OUT") cnt = 100;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "END") cnt = 110;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "LD=") cnt = 120;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "LD>=") cnt = 120;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "LD<=") cnt = 120;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "LD<") cnt = 120;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "LD>")
            {
                cnt = 120;
            }
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "LD<>")
            {
                cnt = 120;
            }
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "LDD=") cnt = 120;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "LDD>=") cnt = 120;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "LDD<=") cnt = 120;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "LDD<") cnt = 120;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "LDD>") cnt = 120;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "LDD<>") cnt = 120;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "AND=") cnt = 125;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "AND>=") cnt = 125;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "AND<=") cnt = 125;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "AND<") cnt = 125;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "AND>") cnt = 125;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "AND<>") cnt = 125;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ANDD=") cnt = 125;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ANDD>=") cnt = 125;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ANDD<=") cnt = 125;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ANDD<") cnt = 125;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ANDD>") cnt = 125;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ANDD<>") cnt = 125;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "OR=") cnt = 130;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "OR>=") cnt = 130;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "OR<=") cnt = 130;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "OR<") cnt = 130;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "OR>") cnt = 130;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ORD=") cnt = 130;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ORD>=") cnt = 130;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ORD<=") cnt = 130;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ORD<") cnt = 130;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ORD>") cnt = 130;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "MOV") cnt = 140;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "MOVP") cnt = 140;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DMOV") cnt = 140;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DMOVP") cnt = 140;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ADD") cnt = 145;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ADDP") cnt = 145;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DADD") cnt = 145;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DADDP") cnt = 145;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "SUB") cnt = 150;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "SUBP") cnt = 150;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DSUB") cnt = 150;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DSUBP") cnt = 150;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "MUL") cnt = 155;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "MULP") cnt = 155;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DMUL") cnt = 155;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DMULP") cnt = 155;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DIV") cnt = 160;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DIVP") cnt = 160;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DDIV") cnt = 160;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DDIVP") cnt = 160;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "INC") cnt = 165;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "INCP") cnt = 165;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DINC") cnt = 165;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DINCP") cnt = 165;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DRVA") cnt = 170;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DDRVA") cnt = 170;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DRVI") cnt = 180;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DDRVI") cnt = 180;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "PLSV") cnt = 185;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DPLSV") cnt = 185;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "SET") cnt = 190;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "RST") cnt = 195;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ZRST") cnt = 200;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "ZRSTP") cnt = 200;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "BMOV") cnt = 205;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "BMOVP") cnt = 205;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "WTB") cnt = 210;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "WTBP") cnt = 210;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DWTB") cnt = 210;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DWTBP") cnt = 210;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "BTW") cnt = 215;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "BTWP") cnt = 215;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DBTW") cnt = 215;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "DBTWP") cnt = 215;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "TAB") cnt = 220;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "JUMP") cnt = 225;
            else if (程式編譯_IL轉換為階梯圖.當前指令[0] == "REF") cnt = 230;
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
        }
        
        void sub_程式編譯_IL轉換為階梯圖_20_LD_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length ==2)
            {
                int X_temp = 1;
                int Y_temp = 程式編譯_IL轉換為階梯圖.空列數位置;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderType = partTypeEnum.A_Part;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.X_現在值 = X_temp +1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = Y_temp;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;     
        }
        void sub_程式編譯_IL轉換為階梯圖_30_LDI_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 2)
            {
                int X_temp = 1;
                int Y_temp = 程式編譯_IL轉換為階梯圖.空列數位置;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderType = partTypeEnum.B_Part;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.X_現在值 = X_temp + 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = Y_temp;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_40_AND_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 2)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderType = partTypeEnum.A_Part;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.X_現在值 = X_temp +1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = Y_temp;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_50_AN1_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 2)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderType = partTypeEnum.B_Part;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.X_現在值 = X_temp + 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = Y_temp;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_60_OR_找尋上個區塊末端位置(ref byte cnt)
        {
            Point p = 程式編譯_IL轉換為階梯圖_檢查上一區塊末端位置(new Point(1, 程式編譯_IL轉換為階梯圖.Y_現在值));
            程式編譯_IL轉換為階梯圖.X_上一個區塊末端 = p.X;
            程式編譯_IL轉換為階梯圖.Y_上一個區塊末端 = p.Y;
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_60_OR_階梯圖寫入(ref byte cnt)
        {
 
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 2)
            {
                int X_temp = 1;
                int Y_temp = 程式編譯_IL轉換為階梯圖.空列數位置;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderType = partTypeEnum.A_Part;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[1]; 
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_60_OR_繪製豎線(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.IL指令程式.Count == 4)
            {

            }
            int X_temp = 程式編譯_IL轉換為階梯圖.X_上一個區塊末端 -1;
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            int 要繪製距離設定值 = 程式編譯_IL轉換為階梯圖.空列數位置 - 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            int 要繪製距離現在值 = 0;
            while (true)
            {
                if (要繪製距離現在值 > 要繪製距離設定值) break;
                if (要繪製距離現在值 == 0)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右下 = true;
                }
                else if(要繪製距離現在值 == 要繪製距離設定值)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右上 = true;
                }
                else
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右下 = true;
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右上 = true;
                }
                要繪製距離現在值++;
            }     
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_60_OR_繪製橫線(ref byte cnt)
        {
            int X_temp = 程式編譯_IL轉換為階梯圖.X_上一個區塊末端 -1;
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
            int 要繪製距離設定值 = X_temp - 1;
            int 要繪製距離現在值 = 0;
            while (true)
            {
                if (要繪製距離現在值 > 要繪製距離設定值) break;

                if (程式編譯_IL轉換為階梯圖.list[Y_temp][1 + 要繪製距離現在值].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][1 + 要繪製距離現在值].ladderType = partTypeEnum.H_Line_Part;
                }

                要繪製距離現在值++;
            }
            程式編譯_IL轉換為階梯圖.X_現在值 = 程式編譯_IL轉換為階梯圖.X_上一個區塊末端;
            程式編譯_IL轉換為階梯圖.Y_現在值 = 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_70_ORI_找尋上個區塊末端位置(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.IL指令程式.Count == 4)
            {

            }
            Point p = 程式編譯_IL轉換為階梯圖_檢查上一區塊末端位置(new Point(1, 程式編譯_IL轉換為階梯圖.Y_現在值));
            程式編譯_IL轉換為階梯圖.X_上一個區塊末端 = p.X;
            程式編譯_IL轉換為階梯圖.Y_上一個區塊末端 = p.Y;
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_70_ORI_階梯圖寫入(ref byte cnt)
        {

            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 2)
            {
                int X_temp = 1;
                int Y_temp = 程式編譯_IL轉換為階梯圖.空列數位置;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderType = partTypeEnum.B_Part;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[1];
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_70_ORI_繪製豎線(ref byte cnt)
        {
            int X_temp = 程式編譯_IL轉換為階梯圖.X_上一個區塊末端 - 1;
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            int 要繪製距離設定值 = 程式編譯_IL轉換為階梯圖.空列數位置 - 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            int 要繪製距離現在值 = 0;
            while (true)
            {
                if (要繪製距離現在值 > 要繪製距離設定值) break;
                if (要繪製距離現在值 == 0)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右下 = true;
                }
                else if (要繪製距離現在值 == 要繪製距離設定值)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右上 = true;
                }
                else
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右下 = true;
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右上 = true;
                }
                要繪製距離現在值++;
            }
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_70_ORI_繪製橫線(ref byte cnt)
        {
            int X_temp = 程式編譯_IL轉換為階梯圖.X_上一個區塊末端 - 1;
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
            int 要繪製距離設定值 = X_temp - 1;
            int 要繪製距離現在值 = 0;
            while (true)
            {
                if (要繪製距離現在值 > 要繪製距離設定值) break;

                if (程式編譯_IL轉換為階梯圖.list[Y_temp][1 + 要繪製距離現在值].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][1 + 要繪製距離現在值].ladderType = partTypeEnum.H_Line_Part;
                }

                要繪製距離現在值++;
            }
            程式編譯_IL轉換為階梯圖.X_現在值 = 程式編譯_IL轉換為階梯圖.X_上一個區塊末端;
            程式編譯_IL轉換為階梯圖.Y_現在值 = 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_80_ORB_找尋上個區塊末端位置(ref byte cnt)
        {
            Point p = 程式編譯_IL轉換為階梯圖_檢查上一區塊末端位置(new Point(1, 程式編譯_IL轉換為階梯圖.Y_現在值));
            程式編譯_IL轉換為階梯圖.X_上一個區塊末端 = p.X;
            程式編譯_IL轉換為階梯圖.Y_上一個區塊末端 = p.Y;
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_80_ORB_繪製橫線(ref byte cnt)
        {
            int X_start = 1;
            int X_End = 程式編譯_IL轉換為階梯圖.X_現在值 - 1;
            if (程式編譯_IL轉換為階梯圖.X_上一個區塊末端 - 1 > X_End) X_End = 程式編譯_IL轉換為階梯圖.X_上一個區塊末端 - 1;
            int X_temp = X_End;
            int 要繪製距離設定值 = X_temp - 1;
            int 要繪製距離現在值 = 0;
            while (true)
            {
                if (要繪製距離現在值 > 要繪製距離設定值) break;

                if (程式編譯_IL轉換為階梯圖.list[程式編譯_IL轉換為階梯圖.Y_現在值][1 + 要繪製距離現在值].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[程式編譯_IL轉換為階梯圖.Y_現在值][1 + 要繪製距離現在值].ladderType = partTypeEnum.H_Line_Part;
                }
                if (程式編譯_IL轉換為階梯圖.list[程式編譯_IL轉換為階梯圖.Y_上一個區塊末端][1 + 要繪製距離現在值].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[程式編譯_IL轉換為階梯圖.Y_上一個區塊末端][1 + 要繪製距離現在值].ladderType = partTypeEnum.H_Line_Part;
                }
                要繪製距離現在值++;
            }
            程式編譯_IL轉換為階梯圖.X_現在值 = X_End + 1;

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_80_ORB_繪製豎線(ref byte cnt)
        {
            int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值 - 1;
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            int 要繪製距離設定值 = 程式編譯_IL轉換為階梯圖.Y_現在值 - 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            int 要繪製距離現在值 = 0;
            while (true)
            {
                if (要繪製距離現在值 > 要繪製距離設定值) break;
                if (要繪製距離現在值 == 0)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右下 = true;
                }
                else if (要繪製距離現在值 == 要繪製距離設定值)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右上 = true;
                }
                else
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右下 = true;
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右上 = true;
                }
                要繪製距離現在值++;
            }
            程式編譯_IL轉換為階梯圖.Y_現在值 = 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_80_ORB_檢查是否需要折豎線(ref byte cnt)
        {
            int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值 - 2;
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            bool 要折豎線 = false;
            while (true)
            {
                if (X_temp == 0) break;
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].Vline_右下)
                {
                    要折豎線 = true;
                    break;
                }
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderType != partTypeEnum.H_Line_Part)
                {
                    要折豎線 = false;
                    break;
                }
          
                X_temp--;
            }
            if (要折豎線)
            {
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].Vline_右下 = false;
                程式編譯_IL轉換為階梯圖.list[Y_temp + 1][X_temp].Vline_右上 = false;
                while (true)
                {
                    if (X_temp == 程式編譯_IL轉換為階梯圖.X_現在值) break;
                    if (程式編譯_IL轉換為階梯圖.list[Y_temp + 1][X_temp].ladderType == partTypeEnum.noPart)
                    {
                        程式編譯_IL轉換為階梯圖.list[Y_temp + 1][X_temp].ladderType = partTypeEnum.H_Line_Part;
                    }
                    X_temp++;
                }
            }
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_90_ANB_找尋上個區塊末端位置(ref byte cnt)
        {
            Point p = 程式編譯_IL轉換為階梯圖_檢查上一區塊末端位置(new Point(1, 程式編譯_IL轉換為階梯圖.Y_現在值));
            程式編譯_IL轉換為階梯圖.X_上一個區塊末端 = p.X;
            程式編譯_IL轉換為階梯圖.Y_上一個區塊末端 = p.Y;
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_90_ANB_檢查區塊範圍且移動區塊(ref byte cnt)
        {
            int X範圍最大值 = 0;
            Point p0 = new Point();
            Point p1 = new Point();
            p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, 程式編譯_IL轉換為階梯圖.Y_現在值), 程式編譯_IL轉換為階梯圖.list);
            p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, 程式編譯_IL轉換為階梯圖.Y_現在值), 程式編譯_IL轉換為階梯圖.list);
            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[p0.Y][X].ladderType != partTypeEnum.noPart) p0.X = X;
                if (程式編譯_IL轉換為階梯圖.list[p1.Y][X].ladderType != partTypeEnum.noPart) p1.X = X;
            }


            if (p0.X > X範圍最大值) X範圍最大值 = p0.X;
            if (p1.X > X範圍最大值) X範圍最大值 = p1.X;
            Point 貼上位置 = new Point(程式編譯_IL轉換為階梯圖.X_上一個區塊末端, 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端);

            for (int Y = p0.Y; Y <= p1.Y; Y++)
            {
                貼上位置.X = 程式編譯_IL轉換為階梯圖.X_上一個區塊末端;
                for (int X = 1; X <= X範圍最大值; X++)
                {
                    bool 貼上位置右上原本有線 = 程式編譯_IL轉換為階梯圖.list[貼上位置.Y][貼上位置.X -1].Vline_右上;
                    bool 貼上位置右下原本有線 = 程式編譯_IL轉換為階梯圖.list[貼上位置.Y][貼上位置.X -1].Vline_右下;
                    程式編譯_IL轉換為階梯圖.list[貼上位置.Y][貼上位置.X] = 程式編譯_IL轉換為階梯圖.list[Y][X];
                    if (X == 1)
                    {
                        if (程式編譯_IL轉換為階梯圖.list[Y][X].ladderType != partTypeEnum.noPart)
                        {
                            if (Y == p0.Y) 程式編譯_IL轉換為階梯圖.list[貼上位置.Y][貼上位置.X - 1].Vline_右下 = true;
                            else if (Y == p1.Y) 程式編譯_IL轉換為階梯圖.list[貼上位置.Y][貼上位置.X - 1].Vline_右上 = true;
                            else
                            {
                                程式編譯_IL轉換為階梯圖.list[貼上位置.Y][貼上位置.X - 1].Vline_右上 = true;
                                if (程式編譯_IL轉換為階梯圖.list[Y+1][X].ladderType != partTypeEnum.noPart) 程式編譯_IL轉換為階梯圖.list[貼上位置.Y][貼上位置.X - 1].Vline_右下 = true;                                            
                            }
                        }
                        else
                        {
                            if (!貼上位置右上原本有線) 程式編譯_IL轉換為階梯圖.list[貼上位置.Y][貼上位置.X - 1].Vline_右上 = false;
                            if (!貼上位置右下原本有線) 程式編譯_IL轉換為階梯圖.list[貼上位置.Y][貼上位置.X  -1].Vline_右下 = false;
                        }
                    }
                    程式編譯_IL轉換為階梯圖.list[Y][X] = new LADDER(1);
                    程式編譯_IL轉換為階梯圖.list[Y][X].未編譯 = true;
                    貼上位置.X++;
                }
                貼上位置.Y++;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;  
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_90_ANB_更新現在位置(ref byte cnt)
        {
            Point p0 = new Point();
            p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端), 程式編譯_IL轉換為階梯圖.list);
            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[p0.Y][X].ladderType != partTypeEnum.noPart) p0.X = X;
            }
            程式編譯_IL轉換為階梯圖.X_現在值 = p0.X + 1;
            程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_100_OUT_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 2)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.OUT_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else if (程式編譯_IL轉換為階梯圖.當前指令.Length == 3)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.OUT_TIMER_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_100_OUT_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1;X++ )
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_110_END_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 1)
            {
                int X_temp = 一列格數 - 2;
                int Y_temp = 程式編譯_IL轉換為階梯圖.空列數位置;
                程式編譯_IL轉換為階梯圖.X_現在值 = X_temp;
                程式編譯_IL轉換為階梯圖.Y_現在值 = Y_temp;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderType = partTypeEnum.EndPart;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_110_END_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_120_LD_equal_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 3)
            {
                int X_temp = 1;
                int Y_temp = 程式編譯_IL轉換為階梯圖.空列數位置;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderType = partTypeEnum.LD_Equal_Part;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0].Replace("LD", "");
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp +1].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp +2].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.X_現在值 = X_temp + 3;
                程式編譯_IL轉換為階梯圖.Y_現在值 = Y_temp;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_125_AND_equal_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 3)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderType = partTypeEnum.LD_Equal_Part;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0].Replace("AND", "");
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp + 1].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp + 2].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.X_現在值 = X_temp + 3;
                程式編譯_IL轉換為階梯圖.Y_現在值 = Y_temp;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_130_OR_equal_找尋上個區塊末端位置(ref byte cnt)
        {
            Point p = 程式編譯_IL轉換為階梯圖_檢查上一區塊末端位置(new Point(1, 程式編譯_IL轉換為階梯圖.Y_現在值));
            程式編譯_IL轉換為階梯圖.X_上一個區塊末端 = p.X;
            程式編譯_IL轉換為階梯圖.Y_上一個區塊末端 = p.Y;
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_130_OR_equal_階梯圖寫入(ref byte cnt)
        {

            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 3)
            {
                int X_temp = 1;
                int Y_temp = 程式編譯_IL轉換為階梯圖.空列數位置;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderType = partTypeEnum.LD_Equal_Part;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0].Replace("OR", "");
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp + 1].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp + 2].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.X_現在值 = X_temp + 3;
                程式編譯_IL轉換為階梯圖.Y_現在值 = Y_temp;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_130_OR_equal_繪製橫線(ref byte cnt)
        {
            /*int X_temp = 程式編譯_IL轉換為階梯圖.X_上一個區塊末端 - 1;
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
            int 要繪製距離設定值 = X_temp - 1;
            int 要繪製距離現在值 = 0;
            while (true)
            {
                if (要繪製距離現在值 > 要繪製距離設定值) break;

                if (程式編譯_IL轉換為階梯圖.list[Y_temp][1 + 要繪製距離現在值].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][1 + 要繪製距離現在值].ladderType = partTypeEnum.H_Line_Part;
                }

                要繪製距離現在值++;
            }
            程式編譯_IL轉換為階梯圖.X_現在值 = 程式編譯_IL轉換為階梯圖.X_上一個區塊末端;
            程式編譯_IL轉換為階梯圖.Y_現在值 = 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;*/
            int X_start = 1;
            int X_End = 程式編譯_IL轉換為階梯圖.X_現在值 - 1;
            if (程式編譯_IL轉換為階梯圖.X_上一個區塊末端 - 1 > X_End) X_End = 程式編譯_IL轉換為階梯圖.X_上一個區塊末端 - 1;
            int X_temp = X_End;
            int 要繪製距離設定值 = X_temp - 1;
            int 要繪製距離現在值 = 0;
            while (true)
            {
                if (要繪製距離現在值 > 要繪製距離設定值) break;

                if (程式編譯_IL轉換為階梯圖.list[程式編譯_IL轉換為階梯圖.Y_現在值][1 + 要繪製距離現在值].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[程式編譯_IL轉換為階梯圖.Y_現在值][1 + 要繪製距離現在值].ladderType = partTypeEnum.H_Line_Part;
                }
                if (程式編譯_IL轉換為階梯圖.list[程式編譯_IL轉換為階梯圖.Y_上一個區塊末端][1 + 要繪製距離現在值].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[程式編譯_IL轉換為階梯圖.Y_上一個區塊末端][1 + 要繪製距離現在值].ladderType = partTypeEnum.H_Line_Part;
                }
                要繪製距離現在值++;
            }
            程式編譯_IL轉換為階梯圖.X_現在值 = X_End + 1;

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_130_OR_equal_繪製豎線(ref byte cnt)
        {
            /* int X_temp = 程式編譯_IL轉換為階梯圖.X_上一個區塊末端 - 1;
             int Y_temp = 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
             int 要繪製距離設定值 = 程式編譯_IL轉換為階梯圖.空列數位置 - 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
             int 要繪製距離現在值 = 0;
             while (true)
             {
                 if (要繪製距離現在值 > 要繪製距離設定值) break;
                 if (要繪製距離現在值 == 0)
                 {
                     程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右下 = true;
                 }
                 else if (要繪製距離現在值 == 要繪製距離設定值)
                 {
                     程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右上 = true;
                 }
                 else
                 {
                     程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右下 = true;
                     程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右上 = true;
                 }
                 要繪製距離現在值++;
             }
             if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
             else cnt++;*/
            int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值 - 1;
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            int 要繪製距離設定值 = 程式編譯_IL轉換為階梯圖.Y_現在值 - 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            int 要繪製距離現在值 = 0;
            while (true)
            {
                if (要繪製距離現在值 > 要繪製距離設定值) break;
                if (要繪製距離現在值 == 0)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右下 = true;
                }
                else if (要繪製距離現在值 == 要繪製距離設定值)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右上 = true;
                }
                else
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右下 = true;
                    程式編譯_IL轉換為階梯圖.list[Y_temp + 要繪製距離現在值][X_temp].Vline_右上 = true;
                }
                要繪製距離現在值++;
            }
            程式編譯_IL轉換為階梯圖.Y_現在值 = 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_130_OR_equal_檢查是否需要折豎線(ref byte cnt)
        {
            int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值 - 2;
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_上一個區塊末端;
            bool 要折豎線 = false;
            while (true)
            {
                if (X_temp == 1) break;
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].Vline_右下)
                {
                    要折豎線 = true;
                    break;
                }
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].ladderType != partTypeEnum.H_Line_Part)
                {
                    要折豎線 = false;
                    break;
                }

                X_temp--;
            }
            if (要折豎線)
            {
                程式編譯_IL轉換為階梯圖.list[Y_temp][X_temp].Vline_右下 = false;
                程式編譯_IL轉換為階梯圖.list[Y_temp + 1][X_temp].Vline_右上 = false;
                while (true)
                {
                    if (X_temp == 程式編譯_IL轉換為階梯圖.X_現在值) break;
                    if (程式編譯_IL轉換為階梯圖.list[Y_temp + 1][X_temp].ladderType == partTypeEnum.noPart)
                    {
                        程式編譯_IL轉換為階梯圖.list[Y_temp + 1][X_temp].ladderType = partTypeEnum.H_Line_Part;
                    }
                    X_temp++;
                }
            }
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_140_MOV_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 3)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderType = partTypeEnum.MOV_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_140_MOV_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_145_ADD_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 4)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderType = partTypeEnum.ADD_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[3] = 程式編譯_IL轉換為階梯圖.當前指令[3];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_145_ADD_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_150_SUB_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 4)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderType = partTypeEnum.SUB_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[3] = 程式編譯_IL轉換為階梯圖.當前指令[3];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_150_SUB_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_155_MUL_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 4)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderType = partTypeEnum.MUL_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[3] = 程式編譯_IL轉換為階梯圖.當前指令[3];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_155_MUL_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_160_DIV_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 4)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderType = partTypeEnum.DIV_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[3] = 程式編譯_IL轉換為階梯圖.當前指令[3];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_160_DIV_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_165_INC_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 2)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.INC_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_165_INC_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_170_DRVA_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 4)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderType = partTypeEnum.DRVA_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[3] = 程式編譯_IL轉換為階梯圖.當前指令[3];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_170_DRVA_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_180_DRVI_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 4)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderType = partTypeEnum.DRVI_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[3] = 程式編譯_IL轉換為階梯圖.當前指令[3];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_180_DRVI_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_185_PLSV_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 3)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderType = partTypeEnum.PLSV_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_185_PLSV_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_190_SET_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 2)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.SET_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_190_SET_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_195_RST_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 2)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.RST_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_195_RST_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_200_ZRST_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 3)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderType = partTypeEnum.ZRST_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_200_ZRST_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_205_BMOV_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 4)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderType = partTypeEnum.BMOV_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[3] = 程式編譯_IL轉換為階梯圖.當前指令[3];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_205_BMOV_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_210_WTB_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 4)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderType = partTypeEnum.WTB_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[3] = 程式編譯_IL轉換為階梯圖.當前指令[3];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_210_WTB_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_215_BTW_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 4)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderType = partTypeEnum.BTW_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 5].ladderParam[3] = 程式編譯_IL轉換為階梯圖.當前指令[3];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_215_BTW_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_220_TAB_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 3)
            {
                int X_temp = 1;
                int Y_temp = 程式編譯_IL轉換為階梯圖.空列數位置;
                for (int X = X_temp; X < 一列格數 - 1; X++)
                {
                    if (X == 1)
                    {
                        程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.TAB_PART;
                        程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                        程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                        程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                    }
                    else
                    {
                        程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.Data_no_Part;
                    }
                }
              
                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = Y_temp;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_225_JUMP_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 2)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.JUMP_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_226_JUMP_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_230_REF_階梯圖寫入(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖.當前指令.Length == 3)
            {
                int X_temp = 程式編譯_IL轉換為階梯圖.X_現在值;
                int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;
                Point p0 = new Point(X_temp, Y_temp);
                p0 = sub_搜尋指定位置階梯圖右上節點位置(p0, 程式編譯_IL轉換為階梯圖.list);
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderType = partTypeEnum.REF_PART;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderParam[0] = 程式編譯_IL轉換為階梯圖.當前指令[0];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderParam[1] = 程式編譯_IL轉換為階梯圖.當前指令[1];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 4].ladderParam[2] = 程式編譯_IL轉換為階梯圖.當前指令[2];
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 3].ladderType = partTypeEnum.Data_no_Part;
                程式編譯_IL轉換為階梯圖.list[p0.Y][一列格數 - 2].ladderType = partTypeEnum.Data_no_Part;

                程式編譯_IL轉換為階梯圖.X_現在值 = 一列格數 - 1;
                程式編譯_IL轉換為階梯圖.Y_現在值 = p0.Y;
            }
            else
            {
                程式編譯_IL轉換為階梯圖.轉換NG = true;
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_230_REF_當列補橫線(ref byte cnt)
        {
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值;

            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType == partTypeEnum.noPart)
                {
                    程式編譯_IL轉換為階梯圖.list[Y_temp][X].ladderType = partTypeEnum.H_Line_Part;
                }
            }

            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        void sub_程式編譯_IL轉換為階梯圖_240_IL指令程式移除一行(ref byte cnt)
        {
            程式編譯_IL轉換為階梯圖.IL指令程式.RemoveAt(0);
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_250_反編譯彈出視窗(ref byte cnt)
        {
            程式編譯_IL轉換為階梯圖_輸出參數.list.Clear();
            foreach (LADDER[] array_LADDER in 程式編譯_IL轉換為階梯圖.list)
            {
                LADDER[] ladder_temp = new LADDER[array_LADDER.Length];
                for (int i = 0; i < ladder_temp.Length;i++ )
                {
                    ladder_temp[i] = array_LADDER[i];
                }
                程式編譯_IL轉換為階梯圖_輸出參數.list.Add(ladder_temp);
            }
            if(程式編譯_IL轉換為階梯圖_輸入參數.彈出視窗要顯示)
            {
                MessageBox.Show(程式編譯_IL轉換為階梯圖_輸入參數.彈出視窗訊息, " ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);         
            }
            cnt++;
        }
        void sub_程式編譯_IL轉換為階梯圖_(ref byte cnt)
        {
 
            if (程式編譯_IL轉換為階梯圖.轉換NG) cnt = 10;
            else cnt++;
        }

        LADDER[] 程式編譯_IL轉換為階梯圖_創建空一列()
        {
            LADDER[] ladder_temp = new LADDER[一列格數];
            for (int i = 0; i < 一列格數; i++)
            {
                if (i == 0)
                {
                    ladder_temp[i].ladderType = partTypeEnum.leftParenthesis;
                }
                if (i == 一列格數 - 1)
                {
                    ladder_temp[i].ladderType = partTypeEnum.rightParenthesis;
                }
                ladder_temp[i].元素數量 = 1;
                ladder_temp[i].未編譯 = true;
            }
            return ladder_temp;
        }
        Point 程式編譯_IL轉換為階梯圖_檢查上一區塊末端位置(Point p)
        {
            Point p0 = new Point();
            Point p1 = new Point();
            p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, p.Y), 程式編譯_IL轉換為階梯圖.list);
            int Y_temp = 程式編譯_IL轉換為階梯圖.Y_現在值 - 1;
            while (true)
            {
                if ((Y_temp) < 0) break;
                p1 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, Y_temp), 程式編譯_IL轉換為階梯圖.list);
                if (p1.X != 0 && p1.Y != 0) break;
                Y_temp--;
            }
            for (int X = 1; X < 一列格數 - 1; X++)
            {
                if (程式編譯_IL轉換為階梯圖.list[p0.Y][X].ladderType != partTypeEnum.noPart) p0.X = X;
                if (程式編譯_IL轉換為階梯圖.list[p1.Y][X].ladderType != partTypeEnum.noPart) p1.X = X;
            }
            p.X = p1.X + 1;
            p.Y = p1.Y;
            return p;
        }
        #endregion
        #region ExButton程式
        byte cnt_ExButton程式_開新專案 = 255;
        byte cnt_ExButton程式_儲存檔案 = 255;
        byte cnt_ExButton程式_讀取檔案 = 255;
        byte cnt_ExButton程式_復原回未修正 = 255;
        byte cnt_ExButton程式_上一步 = 255;
        byte cnt_ExButton程式_剪下 = 255;
        byte cnt_ExButton程式_複製 = 255;
        byte cnt_ExButton程式_貼上 = 255;
        byte cnt_ExButton程式_畫橫線 = 255;
        byte cnt_ExButton程式_畫豎線 = 255;
        byte cnt_ExButton程式_刪除橫線 = 255;
        byte cnt_ExButton程式_刪除豎線 = 255;
        byte cnt_ExButton程式_寫入A接點 = 255;
        byte cnt_ExButton程式_寫入B接點 = 255;
     
        void sub_ExButton程式()
        {
            foreach(MyUI.ExButton ExButton_temp in List_ExButton)
            {
                ExButton_temp.Run();
            }
            exButton_Online.Set_LoadState(FLAG_Online);
            if (cnt_ExButton程式_開新專案 == 255) cnt_ExButton程式_開新專案 = 1;
            if (cnt_ExButton程式_開新專案 == 1) cnt_ExButton程式_開新專案_檢查按鈕放開(ref cnt_ExButton程式_開新專案);
            if (cnt_ExButton程式_開新專案 == 2) cnt_ExButton程式_開新專案_檢查按鈕按下(ref cnt_ExButton程式_開新專案);
            if (cnt_ExButton程式_開新專案 == 3) cnt_ExButton程式_開新專案_檢查是否編譯完成(ref cnt_ExButton程式_開新專案);
            if (cnt_ExButton程式_開新專案 == 4) cnt_ExButton程式_開新專案_檢查是否需要儲存專案(ref cnt_ExButton程式_開新專案);
            if (cnt_ExButton程式_開新專案 == 5) cnt_ExButton程式_開新專案_開始初始化新專案(ref cnt_ExButton程式_開新專案);
            if (cnt_ExButton程式_開新專案 == 6) cnt_ExButton程式_開新專案 = 255;
            if (cnt_ExButton程式_開新專案 == 10) cnt_ExButton程式_開新專案_10_等待儲存專案_READY(ref cnt_ExButton程式_開新專案);
            if (cnt_ExButton程式_開新專案 == 11) cnt_ExButton程式_開新專案_10_等待儲存專案_OVER(ref cnt_ExButton程式_開新專案);
            if (cnt_ExButton程式_開新專案 == 12) cnt_ExButton程式_開新專案 = 5;

            if (cnt_ExButton程式_儲存檔案 == 255) cnt_ExButton程式_儲存檔案 = 1;
            if (cnt_ExButton程式_儲存檔案 == 1) cnt_ExButton程式_儲存檔案_檢查按鈕放開(ref cnt_ExButton程式_儲存檔案);
            if (cnt_ExButton程式_儲存檔案 == 2) cnt_ExButton程式_儲存檔案_檢查按鈕按下(ref cnt_ExButton程式_儲存檔案);
            if (cnt_ExButton程式_儲存檔案 == 3) cnt_ExButton程式_儲存檔案_檢查是否編譯完成(ref cnt_ExButton程式_儲存檔案);           
            if (cnt_ExButton程式_儲存檔案 == 4) cnt_ExButton程式_儲存檔案_開始存檔(ref cnt_ExButton程式_儲存檔案);
            if (cnt_ExButton程式_儲存檔案 == 5) cnt_ExButton程式_儲存檔案 = 255;

            if (cnt_ExButton程式_讀取檔案 == 255) cnt_ExButton程式_讀取檔案 = 1;
            if (cnt_ExButton程式_讀取檔案 == 1) cnt_ExButton程式_讀取檔案_檢查按鈕按下(ref cnt_ExButton程式_讀取檔案);
            if (cnt_ExButton程式_讀取檔案 == 2) cnt_ExButton程式_讀取檔案_檢查是否需要儲存專案(ref cnt_ExButton程式_讀取檔案);          
            if (cnt_ExButton程式_讀取檔案 == 3) cnt_ExButton程式_讀取檔案_檢查讀取檔案_READY(ref cnt_ExButton程式_讀取檔案);
            if (cnt_ExButton程式_讀取檔案 == 4) cnt_ExButton程式_讀取檔案_檢查讀取檔案_OVER(ref cnt_ExButton程式_讀取檔案);
            if (cnt_ExButton程式_讀取檔案 == 5) cnt_ExButton程式_讀取檔案_檢查按鈕放開(ref cnt_ExButton程式_讀取檔案);
            if (cnt_ExButton程式_讀取檔案 == 6) cnt_ExButton程式_讀取檔案 = 255;
            if (cnt_ExButton程式_讀取檔案 == 10) cnt_ExButton程式_讀取檔案_10_等待儲存專案_READY(ref cnt_ExButton程式_讀取檔案);
            if (cnt_ExButton程式_讀取檔案 == 11) cnt_ExButton程式_讀取檔案_10_等待儲存專案_OVER(ref cnt_ExButton程式_讀取檔案);
            if (cnt_ExButton程式_讀取檔案 == 12) cnt_ExButton程式_讀取檔案 = 3;

            if (cnt_ExButton程式_復原回未修正 == 255) cnt_ExButton程式_復原回未修正 = 1;
            if (cnt_ExButton程式_復原回未修正 == 1) cnt_ExButton程式_復原回未修正_檢查按鈕放開(ref cnt_ExButton程式_復原回未修正);
            if (cnt_ExButton程式_復原回未修正 == 2) cnt_ExButton程式_復原回未修正_檢查按鈕按下(ref cnt_ExButton程式_復原回未修正);
            if (cnt_ExButton程式_復原回未修正 == 3) cnt_ExButton程式_復原回未修正_檢查是否編譯完成(ref cnt_ExButton程式_復原回未修正);
            if (cnt_ExButton程式_復原回未修正 == 4) cnt_ExButton程式_復原回未修正_回復回未編譯狀態(ref cnt_ExButton程式_復原回未修正);
            if (cnt_ExButton程式_復原回未修正 == 5) cnt_ExButton程式_復原回未修正 = 255;

            if (cnt_ExButton程式_上一步 == 255) cnt_ExButton程式_上一步 = 1;
            if (cnt_ExButton程式_上一步 == 1) cnt_ExButton程式_上一步_檢查按鈕放開(ref cnt_ExButton程式_上一步);
            if (cnt_ExButton程式_上一步 == 2) cnt_ExButton程式_上一步_檢查按鈕按下(ref cnt_ExButton程式_上一步);
            if (cnt_ExButton程式_上一步 == 3) cnt_ExButton程式_上一步_回復回上一步(ref cnt_ExButton程式_上一步);
            if (cnt_ExButton程式_上一步 == 4) cnt_ExButton程式_上一步 = 255;

            if (cnt_ExButton程式_剪下 == 255) cnt_ExButton程式_剪下 = 1;
            if (cnt_ExButton程式_剪下 == 1) cnt_ExButton程式_剪下_檢查按鈕放開(ref cnt_ExButton程式_剪下);
            if (cnt_ExButton程式_剪下 == 2) cnt_ExButton程式_剪下_檢查按鈕按下(ref cnt_ExButton程式_剪下);
            if (cnt_ExButton程式_剪下 == 3) cnt_ExButton程式_剪下_等待剪下_READY(ref cnt_ExButton程式_剪下);
            if (cnt_ExButton程式_剪下 == 4) cnt_ExButton程式_剪下_等待剪下_OVER(ref cnt_ExButton程式_剪下);
            if (cnt_ExButton程式_剪下 == 5) cnt_ExButton程式_剪下 = 255;

            if (cnt_ExButton程式_複製 == 255) cnt_ExButton程式_複製 = 1;
            if (cnt_ExButton程式_複製 == 1) cnt_ExButton程式_複製_檢查按鈕放開(ref cnt_ExButton程式_複製);
            if (cnt_ExButton程式_複製 == 2) cnt_ExButton程式_複製_檢查按鈕按下(ref cnt_ExButton程式_複製);
            if (cnt_ExButton程式_複製 == 3) cnt_ExButton程式_複製_等待複製_READY(ref cnt_ExButton程式_複製);
            if (cnt_ExButton程式_複製 == 4) cnt_ExButton程式_複製_等待複製_OVER(ref cnt_ExButton程式_複製);
            if (cnt_ExButton程式_複製 == 5) cnt_ExButton程式_複製 = 255;

            if (cnt_ExButton程式_貼上 == 255) cnt_ExButton程式_貼上 = 1;
            if (cnt_ExButton程式_貼上 == 1) cnt_ExButton程式_貼上_檢查按鈕放開(ref cnt_ExButton程式_貼上);
            if (cnt_ExButton程式_貼上 == 2) cnt_ExButton程式_貼上_檢查按鈕按下(ref cnt_ExButton程式_貼上);
            if (cnt_ExButton程式_貼上 == 3) cnt_ExButton程式_貼上_等待貼上_READY(ref cnt_ExButton程式_貼上);
            if (cnt_ExButton程式_貼上 == 4) cnt_ExButton程式_貼上_等待貼上_OVER(ref cnt_ExButton程式_貼上);
            if (cnt_ExButton程式_貼上 == 5) cnt_ExButton程式_貼上 = 255;

            if (cnt_ExButton程式_畫橫線 == 255) cnt_ExButton程式_畫橫線 = 1;
            if (cnt_ExButton程式_畫橫線 == 1) cnt_ExButton程式_畫橫線_檢查按鈕放開(ref cnt_ExButton程式_畫橫線);
            if (cnt_ExButton程式_畫橫線 == 2) cnt_ExButton程式_畫橫線_檢查按鈕按下(ref cnt_ExButton程式_畫橫線);
            if (cnt_ExButton程式_畫橫線 == 3) cnt_ExButton程式_畫橫線_等待畫橫線_READY(ref cnt_ExButton程式_畫橫線);
            if (cnt_ExButton程式_畫橫線 == 4) cnt_ExButton程式_畫橫線_等待畫橫線_OVER(ref cnt_ExButton程式_畫橫線);
            if (cnt_ExButton程式_畫橫線 == 5) cnt_ExButton程式_畫橫線 = 255;

            if (cnt_ExButton程式_畫豎線 == 255) cnt_ExButton程式_畫豎線 = 1;
            if (cnt_ExButton程式_畫豎線 == 1) cnt_ExButton程式_畫豎線_檢查按鈕放開(ref cnt_ExButton程式_畫豎線);
            if (cnt_ExButton程式_畫豎線 == 2) cnt_ExButton程式_畫豎線_檢查按鈕按下(ref cnt_ExButton程式_畫豎線);
            if (cnt_ExButton程式_畫豎線 == 3) cnt_ExButton程式_畫豎線_等待畫豎線_READY(ref cnt_ExButton程式_畫豎線);
            if (cnt_ExButton程式_畫豎線 == 4) cnt_ExButton程式_畫豎線_等待畫豎線_OVER(ref cnt_ExButton程式_畫豎線);
            if (cnt_ExButton程式_畫豎線 == 5) cnt_ExButton程式_畫豎線 = 255;

            if (cnt_ExButton程式_刪除橫線 == 255) cnt_ExButton程式_刪除橫線 = 1;
            if (cnt_ExButton程式_刪除橫線 == 1) cnt_ExButton程式_刪除橫線_檢查按鈕放開(ref cnt_ExButton程式_刪除橫線);
            if (cnt_ExButton程式_刪除橫線 == 2) cnt_ExButton程式_刪除橫線_檢查按鈕按下(ref cnt_ExButton程式_刪除橫線);
            if (cnt_ExButton程式_刪除橫線 == 3) cnt_ExButton程式_刪除橫線_等待刪除橫線_READY(ref cnt_ExButton程式_刪除橫線);
            if (cnt_ExButton程式_刪除橫線 == 4) cnt_ExButton程式_刪除橫線_等待刪除橫線_OVER(ref cnt_ExButton程式_刪除橫線);
            if (cnt_ExButton程式_刪除橫線 == 5) cnt_ExButton程式_刪除橫線 = 255;

            if (cnt_ExButton程式_刪除豎線 == 255) cnt_ExButton程式_刪除豎線 = 1;
            if (cnt_ExButton程式_刪除豎線 == 1) cnt_ExButton程式_刪除豎線_檢查按鈕放開(ref cnt_ExButton程式_刪除豎線);
            if (cnt_ExButton程式_刪除豎線 == 2) cnt_ExButton程式_刪除豎線_檢查按鈕按下(ref cnt_ExButton程式_刪除豎線);
            if (cnt_ExButton程式_刪除豎線 == 3) cnt_ExButton程式_刪除豎線_等待刪除豎線_READY(ref cnt_ExButton程式_刪除豎線);
            if (cnt_ExButton程式_刪除豎線 == 4) cnt_ExButton程式_刪除豎線_等待刪除豎線_OVER(ref cnt_ExButton程式_刪除豎線);
            if (cnt_ExButton程式_刪除豎線 == 5) cnt_ExButton程式_刪除豎線 = 255;

            if (cnt_ExButton程式_寫入A接點 == 255) cnt_ExButton程式_寫入A接點 = 1;
            if (cnt_ExButton程式_寫入A接點 == 1) cnt_ExButton程式_寫入A接點_檢查按鈕放開(ref cnt_ExButton程式_寫入A接點);
            if (cnt_ExButton程式_寫入A接點 == 2) cnt_ExButton程式_寫入A接點_檢查按鈕按下(ref cnt_ExButton程式_寫入A接點);
            if (cnt_ExButton程式_寫入A接點 == 3) cnt_ExButton程式_寫入A接點_等待寫入A接點_READY(ref cnt_ExButton程式_寫入A接點);
            if (cnt_ExButton程式_寫入A接點 == 4) cnt_ExButton程式_寫入A接點_等待寫入A接點_OVER(ref cnt_ExButton程式_寫入A接點);
            if (cnt_ExButton程式_寫入A接點 == 5) cnt_ExButton程式_寫入A接點 = 255;

            if (cnt_ExButton程式_寫入B接點 == 255) cnt_ExButton程式_寫入B接點 = 1;
            if (cnt_ExButton程式_寫入B接點 == 1) cnt_ExButton程式_寫入B接點_檢查按鈕放開(ref cnt_ExButton程式_寫入B接點);
            if (cnt_ExButton程式_寫入B接點 == 2) cnt_ExButton程式_寫入B接點_檢查按鈕按下(ref cnt_ExButton程式_寫入B接點);
            if (cnt_ExButton程式_寫入B接點 == 3) cnt_ExButton程式_寫入B接點_等待寫入B接點_READY(ref cnt_ExButton程式_寫入B接點);
            if (cnt_ExButton程式_寫入B接點 == 4) cnt_ExButton程式_寫入B接點_等待寫入B接點_OVER(ref cnt_ExButton程式_寫入B接點);
            if (cnt_ExButton程式_寫入B接點 == 5) cnt_ExButton程式_寫入B接點 = 255;
        }

        void cnt_ExButton程式_開新專案_檢查按鈕放開(ref byte cnt)
        {
            if (!exButton_開新專案.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_ExButton程式_開新專案_檢查按鈕按下(ref byte cnt)
        {
            if (exButton_開新專案.Load_WriteState())
            {
                exButton_開新專案.Set_LoadState(true);
                cnt++;
            }
        }
        void cnt_ExButton程式_開新專案_檢查是否編譯完成(ref byte cnt)
        {
            if (FLAG_有程式未編譯)
            {
                DialogResult Result = MessageBox.Show("確認要刪除未編譯區域?", "Warring", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (Result == DialogResult.Yes)
                {
                    ladder_list = List_LADDER_All_Copy(ladder_list_備份);
                    FLAG_有程式未編譯 = false;
                    sub_畫面更新();
                    cnt++;
                }
                else if (Result == DialogResult.No)
                {
                    cnt = 255;
                }
            }
            else
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_開新專案_檢查是否需要儲存專案(ref byte cnt)
        {
            if (!FLAG_專案已儲存)
            {
                DialogResult Result = MessageBox.Show("是否儲存專案?", "Warring", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (Result == DialogResult.Yes)
                {
                    cnt = 10;
                }
                else if (Result == DialogResult.No)
                {
                    cnt++;
                }
                else if (Result == DialogResult.Cancel)
                {
                    cnt = 255;
                }
            }
            else
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_開新專案_10_等待儲存專案_READY(ref byte cnt)
        {
            if (cnt_ExButton程式_儲存檔案 == 2)
            {
                cnt_ExButton程式_儲存檔案 = 3;
                cnt++;
            }
         
        }
        void cnt_ExButton程式_開新專案_10_等待儲存專案_OVER(ref byte cnt)
        {
            if (cnt_ExButton程式_儲存檔案 == 2)
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_開新專案_開始初始化新專案(ref byte cnt)
        {
            bool temp = false;
            sub_初始化(ref temp);
            FLAG_專案已儲存 = true;
            cnt++;
        }
        void cnt_ExButton程式_開新專案_(ref byte cnt)
        {
            cnt++;
        }

        void cnt_ExButton程式_儲存檔案_檢查按鈕放開(ref byte cnt)
        {
            if (!exButton_儲存檔案.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_ExButton程式_儲存檔案_檢查按鈕按下(ref byte cnt)
        {
            if (exButton_儲存檔案.Load_WriteState())
            {
                exButton_儲存檔案.Set_LoadState(true);
                cnt++;
            }
        }
        void cnt_ExButton程式_儲存檔案_檢查是否編譯完成(ref byte cnt)
        {
            if (FLAG_有程式未編譯)
            {
                DialogResult Result = MessageBox.Show("確認要刪除未編譯區域?", "Warring", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (Result == DialogResult.Yes)
                {
                    ladder_list = List_LADDER_All_Copy(ladder_list_備份);
                    FLAG_有程式未編譯 = false;
                    cnt++;
                }
                else if (Result == DialogResult.No)
                {
                    cnt = 255;
                }
            }
            else
            {
                cnt++;
            }
        
        }
        void cnt_ExButton程式_儲存檔案_開始存檔(ref byte cnt)
        {
            if (saveFileDialog_SAVE.ShowDialog(this) == DialogResult.OK)
            {
                bool 儲存成功 = false;
                Str_儲存位置 = saveFileDialog_SAVE.FileName;
                儲存成功 = sub_儲存檔案(Str_儲存位置);

                if (儲存成功)
                {
                    // MessageBox.Show("儲存專案成功!", "檔案資訊", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("儲存專案失敗!", "檔案資訊", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            CallBackUI.panel.取得焦點(panel_LADDER);
            FLAG_專案已儲存 = true;
            cnt++;
        }
        void cnt_ExButton程式_儲存檔案_(ref byte cnt)
        {
            cnt++;
        }

        void cnt_ExButton程式_讀取檔案_檢查按鈕放開(ref byte cnt)
        {
            if (!exButton_讀取檔案.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_ExButton程式_讀取檔案_檢查按鈕按下(ref byte cnt)
        {
            if (exButton_讀取檔案.Load_WriteState())
            {
                exButton_讀取檔案.Set_LoadState(true);
                cnt++;
            }
        }
        void cnt_ExButton程式_讀取檔案_檢查是否需要儲存專案(ref byte cnt)
        {
            if (!FLAG_專案已儲存)
            {
                DialogResult Result = MessageBox.Show("是否儲存專案?", "Warring", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (Result == DialogResult.Yes)
                {
                    cnt = 10;
                }
                else if (Result == DialogResult.No)
                {
                    cnt++;
                }
                else if (Result == DialogResult.Cancel)
                {
                    cnt = 255;
                }
            }
            else
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_讀取檔案_檢查讀取檔案_READY(ref byte cnt)
        {
            if (cnt_讀取檔案==255)
            {
                cnt_讀取檔案 = 1;
                cnt++;
            }
        }
        void cnt_ExButton程式_讀取檔案_檢查讀取檔案_OVER(ref byte cnt)
        {
            sub_讀取檔案();
            if (cnt_讀取檔案 == 255)
            {
                CallBackUI.panel.取得焦點(panel_LADDER);
                FLAG_專案已儲存 = true;
                cnt++;
            }
        }
        void cnt_ExButton程式_讀取檔案_10_等待儲存專案_READY(ref byte cnt)
        {
            if (cnt_ExButton程式_儲存檔案 == 2)
            {
                cnt_ExButton程式_儲存檔案 = 3;
                cnt++;
            }

        }
        void cnt_ExButton程式_讀取檔案_10_等待儲存專案_OVER(ref byte cnt)
        {
            if (cnt_ExButton程式_儲存檔案 == 2)
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_讀取檔案_(ref byte cnt)
        {
            cnt++;
        }

        void cnt_ExButton程式_復原回未修正_檢查按鈕放開(ref byte cnt)
        {
            if (!exButton_復原回未修正.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_ExButton程式_復原回未修正_檢查按鈕按下(ref byte cnt)
        {
            if (exButton_復原回未修正.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_復原回未修正_檢查是否編譯完成(ref byte cnt)
        {
            if (FLAG_有程式未編譯)
            {
                DialogResult Result = MessageBox.Show("確認要刪除未編譯區域?", "Warring", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (Result == DialogResult.Yes)
                {
                    cnt++;
                }
                else if (Result == DialogResult.No)
                {
                    cnt = 255;
                }
            }
            else
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_復原回未修正_回復回未編譯狀態(ref byte cnt)
        {
            ladder_list = List_LADDER_All_Copy(ladder_list_備份);
            FLAG_有程式未編譯 = false;
            sub_畫面更新();
            cnt++;
        }
        void cnt_ExButton程式_復原回未修正_(ref byte cnt)
        {
            cnt++;
        }

        void cnt_ExButton程式_上一步_檢查按鈕放開(ref byte cnt)
        {
            if (!exButton_上一步.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_ExButton程式_上一步_檢查按鈕按下(ref byte cnt)
        {
            if (exButton_上一步.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_上一步_回復回上一步(ref byte cnt)
        {
            sub_上一步();
            cnt++;
        }
        void cnt_ExButton程式_上一步_(ref byte cnt)
        {
            cnt++;
        }

        void cnt_ExButton程式_剪下_檢查按鈕放開(ref byte cnt)
        {
            if (!exButton_剪下.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_ExButton程式_剪下_檢查按鈕按下(ref byte cnt)
        {
            if (exButton_剪下.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_剪下_等待剪下_READY(ref byte cnt)
        {
            if (cnt_鍵盤按鈕檢查_Control_X ==1)
            {
                cnt_鍵盤按鈕檢查_Control_X = 2;
                cnt++;
            }
       
        }
        void cnt_ExButton程式_剪下_等待剪下_OVER(ref byte cnt)
        {
            FLAG_Key_ControlKey_致能 = true;
            if (cnt_鍵盤按鈕檢查_Control_X == 1)
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_剪下_(ref byte cnt)
        {
            cnt++;
        }

        void cnt_ExButton程式_複製_檢查按鈕放開(ref byte cnt)
        {
            if (!exButton_複製.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_ExButton程式_複製_檢查按鈕按下(ref byte cnt)
        {
            if (exButton_複製.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_複製_等待複製_READY(ref byte cnt)
        {
            if (cnt_鍵盤按鈕檢查_Control_C == 1)
            {
                cnt_鍵盤按鈕檢查_Control_C = 2;
                cnt++;
            }

        }
        void cnt_ExButton程式_複製_等待複製_OVER(ref byte cnt)
        {
            FLAG_Key_ControlKey_致能 = true;
            if (cnt_鍵盤按鈕檢查_Control_C == 1)
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_複製_(ref byte cnt)
        {
            cnt++;
        }

        void cnt_ExButton程式_貼上_檢查按鈕放開(ref byte cnt)
        {
            if (!exButton_貼上.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_ExButton程式_貼上_檢查按鈕按下(ref byte cnt)
        {
            if (exButton_貼上.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_貼上_等待貼上_READY(ref byte cnt)
        {
            if (cnt_鍵盤按鈕檢查_Control_V == 1)
            {
                cnt_鍵盤按鈕檢查_Control_V = 2;
                cnt++;
            }

        }
        void cnt_ExButton程式_貼上_等待貼上_OVER(ref byte cnt)
        {
            FLAG_Key_ControlKey_致能 = true;
            if (cnt_鍵盤按鈕檢查_Control_V == 1)
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_貼上_(ref byte cnt)
        {
            cnt++;
        }

        void cnt_ExButton程式_畫橫線_檢查按鈕放開(ref byte cnt)
        {
            if (!exButton_畫橫線.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_ExButton程式_畫橫線_檢查按鈕按下(ref byte cnt)
        {
            if (exButton_畫橫線.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_畫橫線_等待畫橫線_READY(ref byte cnt)
        {
            if (cnt_鍵盤按鈕檢查_F9 == 1)
            {
                cnt_鍵盤按鈕檢查_F9 = 2;
                cnt++;
            }

        }
        void cnt_ExButton程式_畫橫線_等待畫橫線_OVER(ref byte cnt)
        {
            if (cnt_鍵盤按鈕檢查_F9 == 1)
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_畫橫線_(ref byte cnt)
        {
            cnt++;
        }

        void cnt_ExButton程式_畫豎線_檢查按鈕放開(ref byte cnt)
        {
            if (!exButton_畫豎線.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_ExButton程式_畫豎線_檢查按鈕按下(ref byte cnt)
        {
            if (exButton_畫豎線.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_畫豎線_等待畫豎線_READY(ref byte cnt)
        {
            if (cnt_鍵盤按鈕檢查_Shift_F9 == 1)
            {
                cnt_鍵盤按鈕檢查_Shift_F9 = 2;
                cnt++;
            }

        }
        void cnt_ExButton程式_畫豎線_等待畫豎線_OVER(ref byte cnt)
        {
            FLAG_Key_ShiftKey_致能 = true;
            if (cnt_鍵盤按鈕檢查_Shift_F9 == 1)
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_畫豎線_(ref byte cnt)
        {
            cnt++;
        }

        void cnt_ExButton程式_刪除橫線_檢查按鈕放開(ref byte cnt)
        {
            if (!exButton_刪除橫線.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_ExButton程式_刪除橫線_檢查按鈕按下(ref byte cnt)
        {
            if (exButton_刪除橫線.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_刪除橫線_等待刪除橫線_READY(ref byte cnt)
        {
            if (cnt_鍵盤按鈕檢查_Control_F9 == 1)
            {
                cnt_鍵盤按鈕檢查_Control_F9 = 2;
                cnt++;
            }

        }
        void cnt_ExButton程式_刪除橫線_等待刪除橫線_OVER(ref byte cnt)
        {
            FLAG_Key_ControlKey_致能 = true;
            if (cnt_鍵盤按鈕檢查_Control_F9 == 1)
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_刪除橫線_(ref byte cnt)
        {
            cnt++;
        }

        void cnt_ExButton程式_刪除豎線_檢查按鈕放開(ref byte cnt)
        {
            if (!exButton_刪除豎線.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_ExButton程式_刪除豎線_檢查按鈕按下(ref byte cnt)
        {
            if (exButton_刪除豎線.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_刪除豎線_等待刪除豎線_READY(ref byte cnt)
        {
            if (cnt_鍵盤按鈕檢查_Control_F10 == 1)
            {
                cnt_鍵盤按鈕檢查_Control_F10 = 2;
                cnt++;
            }

        }
        void cnt_ExButton程式_刪除豎線_等待刪除豎線_OVER(ref byte cnt)
        {
            FLAG_Key_ControlKey_致能 = true;
            if (cnt_鍵盤按鈕檢查_Control_F10 == 1)
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_刪除豎線_(ref byte cnt)
        {
            cnt++;
        }

        void cnt_ExButton程式_寫入A接點_檢查按鈕放開(ref byte cnt)
        {
            if (!exButton_寫入A接點.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_ExButton程式_寫入A接點_檢查按鈕按下(ref byte cnt)
        {
            if (exButton_寫入A接點.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_寫入A接點_等待寫入A接點_READY(ref byte cnt)
        {
            if (cnt_鍵盤按鈕檢查_F5 == 1)
            {
                cnt_鍵盤按鈕檢查_F5 = 2;
                cnt++;
            }

        }
        void cnt_ExButton程式_寫入A接點_等待寫入A接點_OVER(ref byte cnt)
        {
            if (cnt_鍵盤按鈕檢查_F5 == 1)
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_寫入A接點_(ref byte cnt)
        {
            cnt++;
        }

        void cnt_ExButton程式_寫入B接點_檢查按鈕放開(ref byte cnt)
        {
            if (!exButton_寫入B接點.Load_WriteState())
            {
                cnt++;
            }

        }
        void cnt_ExButton程式_寫入B接點_檢查按鈕按下(ref byte cnt)
        {
            if (exButton_寫入B接點.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_寫入B接點_等待寫入B接點_READY(ref byte cnt)
        {
            if (cnt_鍵盤按鈕檢查_F6 == 1)
            {
                cnt_鍵盤按鈕檢查_F6 = 2;
                cnt++;
            }

        }
        void cnt_ExButton程式_寫入B接點_等待寫入B接點_OVER(ref byte cnt)
        {
            if (cnt_鍵盤按鈕檢查_F6 == 1)
            {
                cnt++;
            }
        }
        void cnt_ExButton程式_寫入B接點_(ref byte cnt)
        {
            cnt++;
        }
        #endregion       
        #region 鍵盤按鈕檢查
        bool FLAG_Key_ShiftKey_致能 = false;
        byte cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵 = 255;
        byte cnt_鍵盤按鈕檢查_Shift_ins = 255;
        byte cnt_鍵盤按鈕檢查_Shift_delete = 255;
        byte cnt_鍵盤按鈕檢查_Shift_F9 = 255;
        byte cnt_鍵盤按鈕檢查_Shift_Up= 255;
        byte cnt_鍵盤按鈕檢查_Shift_Down = 255;
        byte cnt_鍵盤按鈕檢查_Shift_Left = 255;
        byte cnt_鍵盤按鈕檢查_Shift_Right = 255;
        byte cnt_鍵盤按鈕檢查_Shift_R = 255;
        byte cnt_鍵盤按鈕檢查_Shift_Enter = 255;

        bool FLAG_Key_ControlKey_致能 = false;
        byte cnt_鍵盤按鈕檢查_Control_F9 = 255;
        byte cnt_鍵盤按鈕檢查_Control_F10 = 255;
        byte cnt_鍵盤按鈕檢查_Control_Z= 255;
        byte cnt_鍵盤按鈕檢查_Control_X = 255;
        byte cnt_鍵盤按鈕檢查_Control_C = 255;
        byte cnt_鍵盤按鈕檢查_Control_V = 255;
        byte cnt_鍵盤按鈕檢查_Control_F = 255;
        byte cnt_鍵盤按鈕檢查_Control_H = 255;
        byte cnt_鍵盤按鈕檢查_Control_ins = 255;
        byte cnt_鍵盤按鈕檢查_F4 = 255;
        byte cnt_鍵盤按鈕檢查_F5 = 255;
        byte cnt_鍵盤按鈕檢查_F6 = 255;
        byte cnt_鍵盤按鈕檢查_F9 = 255;
        byte cnt_鍵盤按鈕檢查_F10 = 255;
        byte cnt_鍵盤按鈕檢查_Delete = 255;
        byte cnt_鍵盤按鈕檢查_Esc = 255;
        byte cnt_鍵盤按鈕檢查_Back = 255;
       
        void sub_鍵盤按鈕檢查()
        {
            #region Shift_組合鍵
            if (cnt_鍵盤按鈕檢查_Shift_R == 255) cnt_鍵盤按鈕檢查_Shift_R = 1;
            if (cnt_鍵盤按鈕檢查_Shift_R == 1) cnt_鍵盤按鈕檢查_Shift_R_檢查按下(ref cnt_鍵盤按鈕檢查_Shift_R, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Shift_R == 2) cnt_鍵盤按鈕檢查_Shift_R_彈出視窗(ref cnt_鍵盤按鈕檢查_Shift_R, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Shift_R == 3) cnt_鍵盤按鈕檢查_Shift_R_等待程式編譯_READY(ref cnt_鍵盤按鈕檢查_Shift_R, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Shift_R == 4) cnt_鍵盤按鈕檢查_Shift_R_等待程式編譯_OVER(ref cnt_鍵盤按鈕檢查_Shift_R, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Shift_R == 5) cnt_鍵盤按鈕檢查_Shift_R_檢查編譯結果(ref cnt_鍵盤按鈕檢查_Shift_R, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Shift_R == 6) cnt_鍵盤按鈕檢查_Shift_R_檢查Upload視窗進入檢查_READY(ref cnt_鍵盤按鈕檢查_Shift_R, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Shift_R == 7) cnt_鍵盤按鈕檢查_Shift_R_檢查Upload視窗進入檢查_OVER(ref cnt_鍵盤按鈕檢查_Shift_R, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Shift_R == 8) cnt_鍵盤按鈕檢查_Shift_R_檢查放開(ref cnt_鍵盤按鈕檢查_Shift_R, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Shift_R == 9) cnt_鍵盤按鈕檢查_Shift_R = 255;
            if ((Keys.Key_ShiftKey || FLAG_Key_ShiftKey_致能) && !FLAG_有功能鍵按下)
            {
                if (cnt_鍵盤按鈕檢查_Shift_ins == 255) cnt_鍵盤按鈕檢查_Shift_ins = 1;
                if (cnt_鍵盤按鈕檢查_Shift_ins == 1) cnt_鍵盤按鈕檢查_Shift_ins_檢查按下(ref cnt_鍵盤按鈕檢查_Shift_ins, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_ins == 2) cnt_鍵盤按鈕檢查_Shift_ins_插入一列(ref cnt_鍵盤按鈕檢查_Shift_ins, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_ins == 3) cnt_鍵盤按鈕檢查_Shift_ins_檢查放開(ref cnt_鍵盤按鈕檢查_Shift_ins, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_ins == 4) cnt_鍵盤按鈕檢查_Shift_ins = 255;

                if (cnt_鍵盤按鈕檢查_Shift_delete == 255) cnt_鍵盤按鈕檢查_Shift_delete = 1;
                if (cnt_鍵盤按鈕檢查_Shift_delete == 1) cnt_鍵盤按鈕檢查_Shift_delete_檢查按下(ref cnt_鍵盤按鈕檢查_Shift_delete, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_delete == 2) cnt_鍵盤按鈕檢查_Shift_delete_刪除一列(ref cnt_鍵盤按鈕檢查_Shift_delete, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_delete == 3) cnt_鍵盤按鈕檢查_Shift_delete_檢查放開(ref cnt_鍵盤按鈕檢查_Shift_delete, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_delete == 4) cnt_鍵盤按鈕檢查_Shift_delete = 255;

                if (cnt_鍵盤按鈕檢查_Shift_F9 == 255) cnt_鍵盤按鈕檢查_Shift_F9 = 1;
                if (cnt_鍵盤按鈕檢查_Shift_F9 == 1) cnt_鍵盤按鈕檢查_Shift_F9_檢查按下(ref cnt_鍵盤按鈕檢查_Shift_F9, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_F9 == 2) cnt_鍵盤按鈕檢查_Shift_F9_寫入階梯圖(ref cnt_鍵盤按鈕檢查_Shift_F9, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_F9 == 3) cnt_鍵盤按鈕檢查_Shift_F9_檢查放開(ref cnt_鍵盤按鈕檢查_Shift_F9, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_F9 == 4) cnt_鍵盤按鈕檢查_Shift_F9 = 255;

                if (cnt_鍵盤按鈕檢查_Shift_Up == 255) cnt_鍵盤按鈕檢查_Shift_Up = 1;
                if (cnt_鍵盤按鈕檢查_Shift_Up == 1) cnt_鍵盤按鈕檢查_Shift_Up_檢查按下(ref cnt_鍵盤按鈕檢查_Shift_Up, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Up == 2) cnt_鍵盤按鈕檢查_Shift_Up_計算窗選位置(ref cnt_鍵盤按鈕檢查_Shift_Up, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Up == 3) cnt_鍵盤按鈕檢查_Shift_Up_檢查放開(ref cnt_鍵盤按鈕檢查_Shift_Up, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Up == 4) cnt_鍵盤按鈕檢查_Shift_Up = 255;

                if (cnt_鍵盤按鈕檢查_Shift_Down == 255) cnt_鍵盤按鈕檢查_Shift_Down = 1;
                if (cnt_鍵盤按鈕檢查_Shift_Down == 1) cnt_鍵盤按鈕檢查_Shift_Down_檢查按下(ref cnt_鍵盤按鈕檢查_Shift_Down, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Down == 2) cnt_鍵盤按鈕檢查_Shift_Down_計算窗選位置(ref cnt_鍵盤按鈕檢查_Shift_Down, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Down == 3) cnt_鍵盤按鈕檢查_Shift_Down_檢查放開(ref cnt_鍵盤按鈕檢查_Shift_Down, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Down == 4) cnt_鍵盤按鈕檢查_Shift_Down = 255;

                if (cnt_鍵盤按鈕檢查_Shift_Left == 255) cnt_鍵盤按鈕檢查_Shift_Left = 1;
                if (cnt_鍵盤按鈕檢查_Shift_Left == 1) cnt_鍵盤按鈕檢查_Shift_Left_檢查按下(ref cnt_鍵盤按鈕檢查_Shift_Left, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Left == 2) cnt_鍵盤按鈕檢查_Shift_Left_計算窗選位置(ref cnt_鍵盤按鈕檢查_Shift_Left, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Left == 3) cnt_鍵盤按鈕檢查_Shift_Left_檢查放開(ref cnt_鍵盤按鈕檢查_Shift_Left, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Left == 4) cnt_鍵盤按鈕檢查_Shift_Left = 255;

                if (cnt_鍵盤按鈕檢查_Shift_Right == 255) cnt_鍵盤按鈕檢查_Shift_Right = 1;
                if (cnt_鍵盤按鈕檢查_Shift_Right == 1) cnt_鍵盤按鈕檢查_Shift_Right_檢查按下(ref cnt_鍵盤按鈕檢查_Shift_Right, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Right == 2) cnt_鍵盤按鈕檢查_Shift_Right_計算窗選位置(ref cnt_鍵盤按鈕檢查_Shift_Right, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Right == 3) cnt_鍵盤按鈕檢查_Shift_Right_檢查放開(ref cnt_鍵盤按鈕檢查_Shift_Right, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Right == 4) cnt_鍵盤按鈕檢查_Shift_Right = 255;

                if (cnt_鍵盤按鈕檢查_Shift_Enter == 255) cnt_鍵盤按鈕檢查_Shift_Enter = 1;
                if (cnt_鍵盤按鈕檢查_Shift_Enter == 1) cnt_鍵盤按鈕檢查_Shift_Enter_檢查按下(ref cnt_鍵盤按鈕檢查_Shift_Enter, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Enter == 2) cnt_鍵盤按鈕檢查_Shift_Enter_檢查元件格式(ref cnt_鍵盤按鈕檢查_Shift_Enter, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Enter == 3) cnt_鍵盤按鈕檢查_Shift_Enter_取得元件狀態並ALT寫入(ref cnt_鍵盤按鈕檢查_Shift_Enter, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Enter == 4) cnt_鍵盤按鈕檢查_Shift_Enter_檢查放開(ref cnt_鍵盤按鈕檢查_Shift_Enter, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Shift_Enter == 5) cnt_鍵盤按鈕檢查_Shift_Enter = 255;

                FLAG_Key_ShiftKey_致能 = false;
            }
            else
            {
                cnt_鍵盤按鈕檢查_Shift_ins = 1;
                cnt_鍵盤按鈕檢查_Shift_delete = 1;
                cnt_鍵盤按鈕檢查_Shift_F9 = 1;
                cnt_鍵盤按鈕檢查_Shift_Up = 1;
                cnt_鍵盤按鈕檢查_Shift_Down = 1;
                cnt_鍵盤按鈕檢查_Shift_Left = 1;
                cnt_鍵盤按鈕檢查_Shift_Right = 1;
                cnt_鍵盤按鈕檢查_Shift_Enter = 1;
            }

 
            #endregion
            #region Control_組合鍵
            if ((Keys.Key_ControlKey||FLAG_Key_ControlKey_致能) && !FLAG_有功能鍵按下)
            {
                FLAG_有功能鍵按下 = true;
                if (cnt_鍵盤按鈕檢查_Control_Z == 255) cnt_鍵盤按鈕檢查_Control_Z = 1;
                if (cnt_鍵盤按鈕檢查_Control_Z == 1) cnt_鍵盤按鈕檢查_Control_Z_檢查按下(ref cnt_鍵盤按鈕檢查_Control_Z, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_Z == 2) cnt_鍵盤按鈕檢查_Control_Z_上一步(ref cnt_鍵盤按鈕檢查_Control_Z, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_Z == 3) cnt_鍵盤按鈕檢查_Control_Z_檢查放開(ref cnt_鍵盤按鈕檢查_Control_Z, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_Z == 4) cnt_鍵盤按鈕檢查_Control_Z = 255;

                if (cnt_鍵盤按鈕檢查_Control_X == 255) cnt_鍵盤按鈕檢查_Control_X = 1;
                if (cnt_鍵盤按鈕檢查_Control_X == 1) cnt_鍵盤按鈕檢查_Control_X_檢查按下(ref cnt_鍵盤按鈕檢查_Control_X, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_X == 2) cnt_鍵盤按鈕檢查_Control_X_複製元素(ref cnt_鍵盤按鈕檢查_Control_X, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_X == 3) cnt_鍵盤按鈕檢查_Control_X_刪除元素(ref cnt_鍵盤按鈕檢查_Control_X, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_X == 4) cnt_鍵盤按鈕檢查_Control_X_檢查放開(ref cnt_鍵盤按鈕檢查_Control_X, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_X == 5) cnt_鍵盤按鈕檢查_Control_X = 255;
                if (cnt_鍵盤按鈕檢查_Control_X == 200) cnt_鍵盤按鈕檢查_Control_X_剪下錯誤(ref cnt_鍵盤按鈕檢查_Control_X, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_X == 201) cnt_鍵盤按鈕檢查_Control_X = 255;

                if (cnt_鍵盤按鈕檢查_Control_C == 255) cnt_鍵盤按鈕檢查_Control_C = 1;
                if (cnt_鍵盤按鈕檢查_Control_C == 1) cnt_鍵盤按鈕檢查_Control_C_檢查按下(ref cnt_鍵盤按鈕檢查_Control_C, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_C == 2) cnt_鍵盤按鈕檢查_Control_C_複製元素(ref cnt_鍵盤按鈕檢查_Control_C, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_C == 3) cnt_鍵盤按鈕檢查_Control_C_檢查放開(ref cnt_鍵盤按鈕檢查_Control_C, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_C == 4) cnt_鍵盤按鈕檢查_Control_C = 255;
                if (cnt_鍵盤按鈕檢查_Control_C == 200) cnt_鍵盤按鈕檢查_Control_C_複製錯誤(ref cnt_鍵盤按鈕檢查_Control_C, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_C == 201) cnt_鍵盤按鈕檢查_Control_C = 255;

                if (cnt_鍵盤按鈕檢查_Control_V == 255) cnt_鍵盤按鈕檢查_Control_V = 1;
                if (cnt_鍵盤按鈕檢查_Control_V == 1) cnt_鍵盤按鈕檢查_Control_V_檢查按下(ref cnt_鍵盤按鈕檢查_Control_V, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_V == 2) cnt_鍵盤按鈕檢查_Control_V_貼上元素(ref cnt_鍵盤按鈕檢查_Control_V, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_V == 3) cnt_鍵盤按鈕檢查_Control_V_檢查放開(ref cnt_鍵盤按鈕檢查_Control_V, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_V == 4) cnt_鍵盤按鈕檢查_Control_V = 255;
                if (cnt_鍵盤按鈕檢查_Control_V == 200) cnt_鍵盤按鈕檢查_Control_V_貼上錯誤(ref cnt_鍵盤按鈕檢查_Control_V, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_V == 201) cnt_鍵盤按鈕檢查_Control_V = 255;

                if (cnt_鍵盤按鈕檢查_Control_F == 255) cnt_鍵盤按鈕檢查_Control_F = 1;
                if (cnt_鍵盤按鈕檢查_Control_F == 1) cnt_鍵盤按鈕檢查_Control_F_檢查按下(ref cnt_鍵盤按鈕檢查_Control_F, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_F == 2) cnt_鍵盤按鈕檢查_Control_F_開啟視窗(ref cnt_鍵盤按鈕檢查_Control_F, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_F == 3) cnt_鍵盤按鈕檢查_Control_F = 255;

                if (cnt_鍵盤按鈕檢查_Control_H == 255) cnt_鍵盤按鈕檢查_Control_H = 1;
                if (cnt_鍵盤按鈕檢查_Control_H == 1) cnt_鍵盤按鈕檢查_Control_H_檢查按下(ref cnt_鍵盤按鈕檢查_Control_H, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_H == 2) cnt_鍵盤按鈕檢查_Control_H_開啟視窗(ref cnt_鍵盤按鈕檢查_Control_H, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_H == 3) cnt_鍵盤按鈕檢查_Control_H = 255;

                if (cnt_鍵盤按鈕檢查_Control_ins == 255) cnt_鍵盤按鈕檢查_Control_ins = 1;
                if (cnt_鍵盤按鈕檢查_Control_ins == 1) cnt_鍵盤按鈕檢查_Control_ins_檢查按下(ref cnt_鍵盤按鈕檢查_Control_ins, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_ins == 2) cnt_鍵盤按鈕檢查_Control_ins_檢查放開(ref cnt_鍵盤按鈕檢查_Control_ins, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_ins == 3) cnt_鍵盤按鈕檢查_Control_ins = 255;

                if (cnt_鍵盤按鈕檢查_Control_F9 == 255) cnt_鍵盤按鈕檢查_Control_F9 = 1;
                if (cnt_鍵盤按鈕檢查_Control_F9 == 1) cnt_鍵盤按鈕檢查_Control_F9_檢查按下(ref cnt_鍵盤按鈕檢查_Control_F9, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_F9 == 2) cnt_鍵盤按鈕檢查_Control_F9_寫入階梯圖(ref cnt_鍵盤按鈕檢查_Control_F9, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_F9 == 3) cnt_鍵盤按鈕檢查_Control_F9_檢查放開(ref cnt_鍵盤按鈕檢查_Control_F9, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_F9 == 4) cnt_鍵盤按鈕檢查_Control_F9 = 255;

                if (cnt_鍵盤按鈕檢查_Control_F10 == 255) cnt_鍵盤按鈕檢查_Control_F10 = 1;
                if (cnt_鍵盤按鈕檢查_Control_F10 == 1) cnt_鍵盤按鈕檢查_Control_F10_檢查按下(ref cnt_鍵盤按鈕檢查_Control_F10, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_F10 == 2) cnt_鍵盤按鈕檢查_Control_F10_寫入階梯圖(ref cnt_鍵盤按鈕檢查_Control_F10, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_F10 == 3) cnt_鍵盤按鈕檢查_Control_F10_檢查放開(ref cnt_鍵盤按鈕檢查_Control_F10, ref FLAG_有功能鍵按下);
                if (cnt_鍵盤按鈕檢查_Control_F10 == 4) cnt_鍵盤按鈕檢查_Control_F10 = 255;
                FLAG_Key_ControlKey_致能 = false;
            }
            else
            {
                cnt_鍵盤按鈕檢查_Control_Z = 1;
                cnt_鍵盤按鈕檢查_Control_X = 1;
                cnt_鍵盤按鈕檢查_Control_C = 1;
                cnt_鍵盤按鈕檢查_Control_V = 1;
                cnt_鍵盤按鈕檢查_Control_ins = 1;
                cnt_鍵盤按鈕檢查_Control_F10 = 1;
            }
            #endregion
            #region 一般按鈕
            if (cnt_鍵盤按鈕檢查_F9 == 255) cnt_鍵盤按鈕檢查_F9 = 1;
            if (cnt_鍵盤按鈕檢查_F9 == 1) cnt_鍵盤按鈕檢查_F9_檢查按下(ref cnt_鍵盤按鈕檢查_F9, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F9 == 2) cnt_鍵盤按鈕檢查_F9_寫入階梯圖(ref cnt_鍵盤按鈕檢查_F9, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F9 == 3) cnt_鍵盤按鈕檢查_F9_移動操控框至下一格(ref cnt_鍵盤按鈕檢查_F9, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F9 == 4) cnt_鍵盤按鈕檢查_F9_檢查放開(ref cnt_鍵盤按鈕檢查_F9, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F9 == 5) cnt_鍵盤按鈕檢查_F9 = 255;

            if (cnt_鍵盤按鈕檢查_F10 == 255) cnt_鍵盤按鈕檢查_F10 = 1;
            if (cnt_鍵盤按鈕檢查_F10 == 1) cnt_鍵盤按鈕檢查_F10_檢查按下(ref cnt_鍵盤按鈕檢查_F10, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F10 == 2) cnt_鍵盤按鈕檢查_F10_檢查模式(ref cnt_鍵盤按鈕檢查_F10, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F10 == 3) cnt_鍵盤按鈕檢查_F10_檢查放開(ref cnt_鍵盤按鈕檢查_F10, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F10 == 4) cnt_鍵盤按鈕檢查_F10 = 255;

            if (cnt_鍵盤按鈕檢查_Delete == 255) cnt_鍵盤按鈕檢查_Delete = 1;
            if (cnt_鍵盤按鈕檢查_Delete == 1) cnt_鍵盤按鈕檢查_Delete_檢查按下(ref cnt_鍵盤按鈕檢查_Delete, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Delete == 2) cnt_鍵盤按鈕檢查_Delete_刪除一元件(ref cnt_鍵盤按鈕檢查_Delete, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Delete == 3) cnt_鍵盤按鈕檢查_Delete_檢查放開(ref cnt_鍵盤按鈕檢查_Delete, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Delete == 4) cnt_鍵盤按鈕檢查_Delete = 255;
            if (cnt_鍵盤按鈕檢查_Delete == 200) cnt_鍵盤按鈕檢查_Delete_刪除錯誤(ref cnt_鍵盤按鈕檢查_Delete, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Delete == 201) cnt_鍵盤按鈕檢查_Delete = 255;

            if (cnt_鍵盤按鈕檢查_F4 == 255) cnt_鍵盤按鈕檢查_F4 = 1;
            if (cnt_鍵盤按鈕檢查_F4 == 1) cnt_鍵盤按鈕檢查_F4_檢查按下(ref cnt_鍵盤按鈕檢查_F4, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F4 == 2) cnt_鍵盤按鈕檢查_F4_等待程式編譯_READY(ref cnt_鍵盤按鈕檢查_F4, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F4 == 3) cnt_鍵盤按鈕檢查_F4_等待程式編譯_OVER(ref cnt_鍵盤按鈕檢查_F4, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F4 == 4) cnt_鍵盤按鈕檢查_F4_檢查放開(ref cnt_鍵盤按鈕檢查_F4, ref FLAG_有功能鍵按下);           
            if (cnt_鍵盤按鈕檢查_F4 == 5) cnt_鍵盤按鈕檢查_F4 = 255;

            if (cnt_鍵盤按鈕檢查_F5 == 255) cnt_鍵盤按鈕檢查_F5 = 1;
            if (cnt_鍵盤按鈕檢查_F5 == 1) cnt_鍵盤按鈕檢查_F5_檢查按下(ref cnt_鍵盤按鈕檢查_F5, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F5 == 2) cnt_鍵盤按鈕檢查_F5_改變為A接點(ref cnt_鍵盤按鈕檢查_F5, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F5 == 3) cnt_鍵盤按鈕檢查_F5_檢查放開(ref cnt_鍵盤按鈕檢查_F5, ref FLAG_有功能鍵按下);          
            if (cnt_鍵盤按鈕檢查_F5 == 4) cnt_鍵盤按鈕檢查_F5 = 255;

            if (cnt_鍵盤按鈕檢查_F6 == 255) cnt_鍵盤按鈕檢查_F6 = 1;
            if (cnt_鍵盤按鈕檢查_F6 == 1) cnt_鍵盤按鈕檢查_F6_檢查按下(ref cnt_鍵盤按鈕檢查_F6, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F6 == 2) cnt_鍵盤按鈕檢查_F6_改變為B接點(ref cnt_鍵盤按鈕檢查_F6, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F6 == 3) cnt_鍵盤按鈕檢查_F6_檢查放開(ref cnt_鍵盤按鈕檢查_F6, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_F6 == 4) cnt_鍵盤按鈕檢查_F6 = 255;

            if (cnt_鍵盤按鈕檢查_Esc == 255) cnt_鍵盤按鈕檢查_Esc = 1;
            if (cnt_鍵盤按鈕檢查_Esc == 1) cnt_鍵盤按鈕檢查_Esc_檢查按下(ref cnt_鍵盤按鈕檢查_Esc, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Esc == 2) cnt_鍵盤按鈕檢查_Esc_檢查模式(ref cnt_鍵盤按鈕檢查_Esc, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Esc == 3) cnt_鍵盤按鈕檢查_Esc_檢查放開(ref cnt_鍵盤按鈕檢查_Esc, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Esc == 4) cnt_鍵盤按鈕檢查_Esc = 255;


            if (cnt_鍵盤按鈕檢查_Back == 255) cnt_鍵盤按鈕檢查_Back = 1;
            if (cnt_鍵盤按鈕檢查_Back == 1) cnt_鍵盤按鈕檢查_Back_檢查按下(ref cnt_鍵盤按鈕檢查_Back, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Back == 2) cnt_鍵盤按鈕檢查_Back_向左移動(ref cnt_鍵盤按鈕檢查_Back, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Back == 3) cnt_鍵盤按鈕檢查_Back_刪除元件(ref cnt_鍵盤按鈕檢查_Back, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Back == 4) cnt_鍵盤按鈕檢查_Back_檢查放開(ref cnt_鍵盤按鈕檢查_Back, ref FLAG_有功能鍵按下);
            if (cnt_鍵盤按鈕檢查_Back == 5) cnt_鍵盤按鈕檢查_Back = 255;
            #endregion
        }
        #region Shift_組合鍵
        void cnt_鍵盤按鈕檢查_Shift_ins_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_Insert)
            {              
                cnt++;
                isWork = true;
            }                        
        }
        void cnt_鍵盤按鈕檢查_Shift_ins_插入一列(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            sub_插入一列(操作方框索引.Y + 顯示畫面列數索引, true, true);
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Shift_ins_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Insert)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Shift_ins_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Shift_delete_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_Delete)
            {

                cnt++;
                isWork = true;
            }                      
        }
        void cnt_鍵盤按鈕檢查_Shift_delete_刪除一列(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            sub_刪除一列(操作方框索引.Y + 顯示畫面列數索引, true);
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Shift_delete_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Delete)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Shift_delete_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Shift_F9_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_F9)
            {
                cnt++;
                isWork = true;
            }   
        }
        void cnt_鍵盤按鈕檢查_Shift_F9_寫入階梯圖(ref byte cnt, ref bool isWork)
        {
            Point index = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            sub_畫豎線(index);
            操作方框移動_向下();
            isWork = true;
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Shift_F9_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_F9)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Shift_F9_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Shift_R_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_ShiftKey)
            {
                if (Keys.Key_R || Keys.Key_F4)
                {
                    cnt++;
                    isWork = true;
                }
         
            }
        }
        void cnt_鍵盤按鈕檢查_Shift_R_彈出視窗(ref byte cnt, ref bool isWork)
        {
            DialogResult Result = MessageBox.Show("確認寫入上位機?", "Warring", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (Result == DialogResult.Yes)
            {
                cnt++;
            }
            else if (Result == DialogResult.No)
            {
                cnt = 255;
            }
 
        }
        void cnt_鍵盤按鈕檢查_Shift_R_等待程式編譯_READY(ref byte cnt, ref bool isWork)
        {
            if (cnt_程式編譯 == 255)
            {
                程式編譯_輸入參數.彈出視窗要顯示 = true;
                cnt_程式編譯 = 1;
                cnt++;
            }
    
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Shift_R_等待程式編譯_OVER(ref byte cnt, ref bool isWork)
        {
            //sub_程式編譯();
            if (cnt_程式編譯 == 255)
            {       
                cnt++;
            }  
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Shift_R_檢查編譯結果(ref byte cnt, ref bool isWork)
        {
            if (FLAG_程式編譯_成功) cnt++;
            else cnt = 255;
        }
        void cnt_鍵盤按鈕檢查_Shift_R_檢查Upload視窗進入檢查_READY(ref byte cnt, ref bool isWork)
        {
            if (cnt_Upload視窗進入檢查 == 255)
            {
                FLAG_Upload視窗_快閃模式 = true;
                cnt_Upload視窗進入檢查 = 1;
                cnt++;
            }
        }
        void cnt_鍵盤按鈕檢查_Shift_R_檢查Upload視窗進入檢查_OVER(ref byte cnt, ref bool isWork)
        {
            if (cnt_Upload視窗進入檢查 == 255)
            {
                cnt++;
            }
        }
        void cnt_鍵盤按鈕檢查_Shift_R_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!(Keys.Key_R && Keys.Key_ShiftKey && Keys.Key_F4))
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Shift_R_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }


        bool FLAG_鍵盤按鈕檢查_Shift_Enter_Device_狀態 = false;
        String FLAG_鍵盤按鈕檢查_Shift_Enter_Device_名稱= "";
        void cnt_鍵盤按鈕檢查_Shift_Enter_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_Enter)
            {
                FLAG_鍵盤按鈕檢查_Shift_Enter_Device_名稱 = "";
                FLAG_鍵盤按鈕檢查_Shift_Enter_Device_狀態 = false;
                cnt++;
                isWork = true;
            }
        }
        void cnt_鍵盤按鈕檢查_Shift_Enter_檢查元件格式(ref byte cnt, ref bool isWork)
        {
            int temp_X=操作方框索引.X;
            int temp_Y = 操作方框索引.Y + 顯示畫面列數索引;
            if (ladder_list[temp_Y][temp_X].ladderType == partTypeEnum.A_Part || ladder_list[temp_Y][temp_X].ladderType == partTypeEnum.B_Part || ladder_list[temp_Y][temp_X].ladderType == partTypeEnum.OUT_Part)
            {
                FLAG_鍵盤按鈕檢查_Shift_Enter_Device_名稱 = ladder_list[temp_Y][temp_X].ladderParam[0];
                cnt++;
            }
            else
            {
                cnt = 255;
            }
            isWork = true;

        }
        void cnt_鍵盤按鈕檢查_Shift_Enter_取得元件狀態並ALT寫入(ref byte cnt, ref bool isWork)
        {
            int step = 1;
            while(true)
            {
                if (step == 1)
                {
                    int temp0 = TopMachine.DeviceRead(FLAG_鍵盤按鈕檢查_Shift_Enter_Device_名稱, ref FLAG_鍵盤按鈕檢查_Shift_Enter_Device_狀態);
                    if (temp0 == -1)
                    {
                        cnt = 255;
                        return;
                    }
                    else if (temp0 == 1)
                    {
      
                    }
                    else if (temp0 == 255)
                    {
                        step++;
                    }
                }
                if (step == 2)
                {
                    int temp1 = TopMachine.DeviceWrite(FLAG_鍵盤按鈕檢查_Shift_Enter_Device_名稱, !FLAG_鍵盤按鈕檢查_Shift_Enter_Device_狀態);
                    if (temp1 == -1)
                    {
                        cnt = 255;
                        return;
                    }
                    else if (temp1 == 1)
                    {
                        
                    }
                    else if (temp1 == 255)
                    {
                        step++;
                    }
                }
                if (step == 3)
                {
                    cnt++;
                    return;
                }
                System.Threading.Thread.Sleep(1);
            }
  
        }
        void cnt_鍵盤按鈕檢查_Shift_Enter_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Enter)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Shift_Enter_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }
        #endregion
        #region Control_組合鍵
        void cnt_鍵盤按鈕檢查_Control_ins_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_Insert)
            {
                sub_插入一元件();
                cnt++;
                isWork = true;
            }          
        }
        void cnt_鍵盤按鈕檢查_Control_ins_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Insert)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Control_ins_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Control_Z_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_Z)
            {
                cnt++;
                isWork = true;
            }           
        }
        void cnt_鍵盤按鈕檢查_Control_Z_上一步(ref byte cnt, ref bool isWork)
        {
            sub_上一步();
            isWork = true;
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Control_Z_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Z)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Control_Z_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Control_X_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_X)
            {
                cnt++;
                isWork = true;
            }
        }
        void cnt_鍵盤按鈕檢查_Control_X_複製元素(ref byte cnt, ref bool isWork)
        {
            if(sub_複製())
            {
                cnt++;
            }
            else
            {
                cnt = 200;
            }
            isWork = true;

        }
        void cnt_鍵盤按鈕檢查_Control_X_刪除元素(ref byte cnt, ref bool isWork)
        {
            if (sub_刪除())
            {
                cnt++;
            }
            else
            {
                cnt = 200;
            }
            isWork = true;

        }
        void cnt_鍵盤按鈕檢查_Control_X_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_X)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Control_X_剪下錯誤(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            MessageBox.Show("包含無法剪下的位置!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Control_X_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Control_C_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_C)
            {
                cnt++;
                isWork = true;
            }
        }
        void cnt_鍵盤按鈕檢查_Control_C_複製元素(ref byte cnt, ref bool isWork)
        {
            if (sub_複製())
            {
                cnt++;
            }
            else
            {
                cnt = 200;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Control_C_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_C)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Control_C_複製錯誤(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            MessageBox.Show("包含無法複製的位置!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Control_C_(ref byte cnt, ref bool isWork)
        {

        }

        void cnt_鍵盤按鈕檢查_Control_V_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_V)
            {
                cnt++;
                isWork = true;
            }
        }
        void cnt_鍵盤按鈕檢查_Control_V_貼上元素(ref byte cnt, ref bool isWork)
        {
            if (sub_貼上())
            {
                cnt++;
            }
            else cnt = 200;
            isWork = true;

        }
        void cnt_鍵盤按鈕檢查_Control_V_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_V)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Control_V_貼上錯誤(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            MessageBox.Show("無法貼上的位置!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Control_V_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Control_F_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_F)
            {
                cnt++;
                isWork = true;
            }
        }
        void cnt_鍵盤按鈕檢查_Control_F_開啟視窗(ref byte cnt, ref bool isWork)
        {
            if (cnt_FindDevice視窗進入檢查 == 255) cnt_FindDevice視窗進入檢查 = 1;
            cnt++;
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Control_F_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_F)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Control_F_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Control_H_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_H)
            {
                cnt++;
                isWork = true;
            }
        }
        void cnt_鍵盤按鈕檢查_Control_H_開啟視窗(ref byte cnt, ref bool isWork)
        {
            if (cnt_ReplaceDevice視窗進入檢查 == 255) cnt_ReplaceDevice視窗進入檢查 = 1;
            cnt++;
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Control_H_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_H)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Control_H_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Control_F9_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_F9)
            {
                cnt++;
                isWork = true;
            }
        }
        void cnt_鍵盤按鈕檢查_Control_F9_寫入階梯圖(ref byte cnt, ref bool isWork)
        {
            Point index = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            sub_刪除橫線(index);
            isWork = true;
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Control_F9_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_F9)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Control_F9_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Control_F10_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_F10)
            {
                cnt++;
                isWork = true;
            }          
        }
        void cnt_鍵盤按鈕檢查_Control_F10_寫入階梯圖(ref byte cnt, ref bool isWork)
        {
            Point index = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            sub_刪除豎線(index);
            isWork = true;
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Control_F10_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_F10)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Control_F10_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }
        #endregion
        #region 一般按鈕
        void cnt_鍵盤按鈕檢查_F9_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (!isWork)
            {
                if (Keys.Key_F9)
                {
                    cnt++;
                    isWork = true;
                }         
            }
        }
        void cnt_鍵盤按鈕檢查_F9_寫入階梯圖(ref byte cnt, ref bool isWork)
        {
            Point index = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            sub_畫橫線(index);
            isWork = true;
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_F9_移動操控框至下一格(ref byte cnt, ref bool isWork)
        {
            操作方框移動_向右();
            isWork = true;
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_F9_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_F9)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_F9_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_F10_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (!isWork)
            {
                if (Keys.Key_F10)
                {
                    cnt++;
                    isWork = true;
                }
            }
        }
        void cnt_鍵盤按鈕檢查_F10_檢查模式(ref byte cnt, ref bool isWork)
        {
            exButton_窗選模式切換.Push_Once();

            操作框窗選_初始位 = new Point();
            操作框窗選_位移量 = new Point();

            isWork = true;
            cnt++;
        }      
        void cnt_鍵盤按鈕檢查_F10_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_F10)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_F10_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Delete_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (!isWork)
            {
                if (Keys.Key_Delete)
                {
                    cnt++;
                    isWork = true;
                }
            }
        }
        void cnt_鍵盤按鈕檢查_Delete_刪除一元件(ref byte cnt, ref bool isWork)
        {         
            isWork = true;
            if (sub_刪除())
            {
                cnt++;
            }
            else
            {
                cnt = 200;
            }
        }
        void cnt_鍵盤按鈕檢查_Delete_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Delete)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Delete_刪除錯誤(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            MessageBox.Show("包含無法刪除的位置!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Delete_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_F4_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (!isWork)
            {
                if (Keys.Key_F4)
                {
                    cnt++;
                    isWork = true;
                }
            }
        }
        void cnt_鍵盤按鈕檢查_F4_等待程式編譯_READY(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            if (cnt_程式編譯 == 255)
            {
                程式編譯_輸入參數.彈出視窗要顯示 = true;
                cnt_程式編譯 = 1;
                cnt++;
            }      
        }
        void cnt_鍵盤按鈕檢查_F4_等待程式編譯_OVER(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            //sub_程式編譯();
            if (cnt_程式編譯 == 255)
            {
                cnt++;
            }    
        }
        void cnt_鍵盤按鈕檢查_F4_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_F4)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_F4_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_F5_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (!isWork)
            {
                if (Keys.Key_F5)
                {
                    cnt++;
                    isWork = true;
                }
            }
        }
        void cnt_鍵盤按鈕檢查_F5_改變為A接點(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            Point p = new Point();
            p.X = 操作方框索引.X;
            p.Y = 操作方框索引.Y + 顯示畫面列數索引 ;
            if (ladder_list[p.Y][p.X].ladderType == partTypeEnum.A_Part || ladder_list[p.Y][p.X].ladderType == partTypeEnum.B_Part)
            {
                string str_temp = ladder_list[p.Y][p.X].ladderParam[0];
                ladder_list[p.Y][p.X].ladderType = partTypeEnum.A_Part;
                ladder_list[p.Y][p.X].ladderParam[0] = str_temp;
                ladder_list[p.Y][p.X].未編譯 = true;
            }
            操作方框移動_向右();
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_F5_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_F5)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_F5_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_F6_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (!isWork)
            {
                if (Keys.Key_F6)
                {
                    cnt++;
                    isWork = true;
                }
            }
        }
        void cnt_鍵盤按鈕檢查_F6_改變為B接點(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            Point p = new Point();
            p.X = 操作方框索引.X;
            p.Y = 操作方框索引.Y + 顯示畫面列數索引;
            if (ladder_list[p.Y][p.X].ladderType == partTypeEnum.A_Part || ladder_list[p.Y][p.X].ladderType == partTypeEnum.B_Part)
            {
                string str_temp = ladder_list[p.Y][p.X].ladderParam[0];
                ladder_list[p.Y][p.X].ladderType = partTypeEnum.B_Part;
                ladder_list[p.Y][p.X].ladderParam[0] = str_temp;
                ladder_list[p.Y][p.X].未編譯 = true;
            }
            操作方框移動_向右();
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_F6_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_F6)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_F6_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Esc_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (!isWork)
            {
                if (Keys.Key_Esc && T10_編譯視窗ESC按鈕致能)
                {
                    cnt++;
                    isWork = true;
                }
            }
        }
        void cnt_鍵盤按鈕檢查_Esc_檢查模式(ref byte cnt, ref bool isWork)
        {
            exButton_窗選模式切換.Set_LoadState(true);
            exButton_程式_註解模式選擇.Set_LoadState(true);
            操作框窗選_位移量 = new Point();
            操作框窗選_初始位 = new Point();
            isWork = true;
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Esc_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Esc)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_ESC_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Back_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (!isWork)
            {
                if (Keys.Key_Back)
                {
                    cnt++;
                    isWork = true;
                }
            }
        }
        void cnt_鍵盤按鈕檢查_Back_向左移動(ref byte cnt, ref bool isWork)
        {
            操作方框移動_向左();
            isWork = true;
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Back_刪除元件(ref byte cnt, ref bool isWork)
        {
            sub_刪除一元件();
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Back_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Back)
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Back_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }
        #endregion
        #endregion
        #region 操作框相關
        private enum Tx_操作框走訪方向
        {
            左 = 0, 右, 上, 下, 無,
        }
        private Tx_窗選模式 窗選模式;
        public enum Tx_窗選模式
        {
            複製模式 = 0, 鼠線模式,
        }
        private Point 操作框窗選_初始位 = new Point();
        private Point 操作框窗選_位移量 = new Point();
        #region Shif_上下左右組合鍵
        void sub_Shif_上下左右組合鍵()
        {
            if (cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵 == 255) cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵 = 1;
            if (cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵 == 1) cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵_檢查按下(ref cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵);
            if (cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵 == 2) cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵_檢查放開(ref cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵);
            if (cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵 == 3) cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵_鼠線寫入(ref cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵);
            if (cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵 == 4) cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵 = 255;
        }
        void cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵_檢查按下(ref byte cnt)
        {
            if (Keys.Key_ShiftKey)
            {
                if (Keys.Key_Right)
                {
                    操作框窗選_初始位 = 操作方框索引;
                    操作框窗選_初始位.Y += 顯示畫面列數索引;
                    cnt++;
                }
                if (Keys.Key_Left)
                {
                    操作框窗選_初始位 = 操作方框索引;
                    操作框窗選_初始位.Y += 顯示畫面列數索引;
                    cnt++;
                }
                if (Keys.Key_Up)
                {
                    操作框窗選_初始位 = 操作方框索引;
                    操作框窗選_初始位.Y += 顯示畫面列數索引;
                    cnt++;
                }
                if (Keys.Key_Down)
                {
                    操作框窗選_初始位 = 操作方框索引;
                    操作框窗選_初始位.Y += 顯示畫面列數索引;
                    cnt++;
                }
            }

        }
        void cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵_檢查放開(ref byte cnt)
        {
            CallBackUI.panel.取得焦點(panel_LADDER);
            if (!Keys.Key_ShiftKey)
            {
                cnt++;
            }
        }
        void cnt_鍵盤按鈕檢查_Shift_上下左右組合鍵_鼠線寫入(ref byte cnt)
        {
            sub_鼠線窗選畫面寫入();
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Shift_Up_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_Up)
            {
                device_system.Set_Device("T1", 200);
                T1_操作方框移動_向上 = false;
                cnt++;
                isWork = true;
            }
        }
        void cnt_鍵盤按鈕檢查_Shift_Up_計算窗選位置(ref byte cnt, ref bool isWork)
        {
            Point 操作框原本位置 = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            操作方框移動_向上();
            Point 操作框移動後位置 = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);

            操作框窗選_位移量.X = 操作框移動後位置.X - 操作框窗選_初始位.X;
            操作框窗選_位移量.Y = 操作框移動後位置.Y - 操作框窗選_初始位.Y;
            sub_窗選滑鼠拖曳位移量檢查();
            isWork = true;
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Shift_Up_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Up)
            {
                cnt++;
            }
            else if (T1_操作方框移動_向上)
            {
                cnt--;
                device_system.Set_Device("T1", 50);
                T1_操作方框移動_向上 = false;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Shift_Up_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Shift_Down_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_Down)
            {
                device_system.Set_Device("T2", 200);
                T2_操作方框移動_向下 = false;
                cnt++;
                isWork = true;
            }
        }
        void cnt_鍵盤按鈕檢查_Shift_Down_計算窗選位置(ref byte cnt, ref bool isWork)
        {
            Point 操作框原本位置 = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            操作方框移動_向下();
            Point 操作框移動後位置 = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);

            操作框窗選_位移量.X = 操作框移動後位置.X - 操作框窗選_初始位.X;
            操作框窗選_位移量.Y = 操作框移動後位置.Y - 操作框窗選_初始位.Y;
            sub_窗選滑鼠拖曳位移量檢查();
            isWork = true;
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Shift_Down_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Down)
            {
                cnt++;
            }
            else if (T2_操作方框移動_向下)
            {
                cnt--;
                device_system.Set_Device("T2", 50);
                T2_操作方框移動_向下 = false;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Shift_Down_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Shift_Left_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_Left)
            {
                device_system.Set_Device("T3", 200);
                T3_操作方框移動_向左 = false;
                cnt++;
                isWork = true;
            }
        }
        void cnt_鍵盤按鈕檢查_Shift_Left_計算窗選位置(ref byte cnt, ref bool isWork)
        {
            Point 操作框原本位置 = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            操作方框移動_向左();
            Point 操作框移動後位置 = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            if (操作框原本位置.X == 0)
            {
                if (操作框移動後位置.X == 一列格數 - 2)
                {
                    操作框窗選_初始位 = 操作框原本位置;
                    操作框窗選_位移量.X = 0;
                    操作框窗選_位移量.Y = 0;
                }
            }
            操作框窗選_位移量.X = 操作框移動後位置.X - 操作框窗選_初始位.X;
            操作框窗選_位移量.Y = 操作框移動後位置.Y - 操作框窗選_初始位.Y;
            sub_窗選滑鼠拖曳位移量檢查();
            isWork = true;
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Shift_Left_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Left)
            {
                cnt++;
            }
            else if (T3_操作方框移動_向左)
            {
                cnt--;
                device_system.Set_Device("T3", 50);
                T3_操作方框移動_向左 = false;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Shift_Left_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }

        void cnt_鍵盤按鈕檢查_Shift_Right_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (Keys.Key_Right)
            {
                device_system.Set_Device("T4", 200);
                T4_操作方框移動_向右 = false;
                cnt++;
                isWork = true;
            }
        }
        void cnt_鍵盤按鈕檢查_Shift_Right_計算窗選位置(ref byte cnt, ref bool isWork)
        {
            Point 操作框原本位置 = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            操作方框移動_向右();
            Point 操作框移動後位置 = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            if (操作框原本位置.X == 一列格數 - 2)
            {
                if (操作框移動後位置.X == 0)
                {
                    操作框窗選_初始位 = 操作框原本位置;
                    操作框窗選_位移量.X = 0;
                    操作框窗選_位移量.Y = 0;
                }
            }
            操作框窗選_位移量.X = 操作框移動後位置.X - 操作框窗選_初始位.X;
            操作框窗選_位移量.Y = 操作框移動後位置.Y - 操作框窗選_初始位.Y;
            sub_窗選滑鼠拖曳位移量檢查();
            isWork = true;
            cnt++;
        }
        void cnt_鍵盤按鈕檢查_Shift_Right_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Right)
            {
                cnt++;
            }
            else if (T4_操作方框移動_向右)
            {
                cnt--;
                device_system.Set_Device("T4", 50);
                T4_操作方框移動_向右 = false;
            }
            isWork = true;
        }
        void cnt_鍵盤按鈕檢查_Shift_Right_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }
        #endregion
        #region 操作方框移動
        byte cnt_操作方框移動_向上 = 255;
        byte cnt_操作方框移動_向下 = 255;
        byte cnt_操作方框移動_向左 = 255;
        byte cnt_操作方框移動_向右 = 255;
        void sub_操作方框移動()
        {
            if (cnt_操作方框移動_向上 == 255) cnt_操作方框移動_向上 = 1;
            if (cnt_操作方框移動_向上 == 1) cnt_操作方框移動_向上_檢查按下(ref cnt_操作方框移動_向上, ref FLAG_有功能鍵按下);
            if (cnt_操作方框移動_向上 == 2) cnt_操作方框移動_向上_檢查放開(ref cnt_操作方框移動_向上, ref FLAG_有功能鍵按下);
            if (cnt_操作方框移動_向上 == 3) cnt_操作方框移動_向上 = 255;

            if (cnt_操作方框移動_向下 == 255) cnt_操作方框移動_向下 = 1;
            if (cnt_操作方框移動_向下 == 1) cnt_操作方框移動_向下_檢查按下(ref cnt_操作方框移動_向下, ref FLAG_有功能鍵按下);
            if (cnt_操作方框移動_向下 == 2) cnt_操作方框移動_向下_檢查放開(ref cnt_操作方框移動_向下, ref FLAG_有功能鍵按下);
            if (cnt_操作方框移動_向下 == 3) cnt_操作方框移動_向下 = 255;

            if (cnt_操作方框移動_向左 == 255) cnt_操作方框移動_向左 = 1;
            if (cnt_操作方框移動_向左 == 1) cnt_操作方框移動_向左_檢查按下(ref cnt_操作方框移動_向左, ref FLAG_有功能鍵按下);
            if (cnt_操作方框移動_向左 == 2) cnt_操作方框移動_向左_檢查放開(ref cnt_操作方框移動_向左, ref FLAG_有功能鍵按下);
            if (cnt_操作方框移動_向左 == 3) cnt_操作方框移動_向左 = 255;

            if (cnt_操作方框移動_向右 == 255) cnt_操作方框移動_向右 = 1;
            if (cnt_操作方框移動_向右 == 1) cnt_操作方框移動_向右_檢查按下(ref cnt_操作方框移動_向右, ref FLAG_有功能鍵按下);
            if (cnt_操作方框移動_向右 == 2) cnt_操作方框移動_向右_檢查放開(ref cnt_操作方框移動_向右, ref FLAG_有功能鍵按下);
            if (cnt_操作方框移動_向右 == 3) cnt_操作方框移動_向右 = 255;
        }
        void cnt_操作方框移動_向上_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (!isWork)
            {
                if (Keys.Key_Up)
                {
                    操作方框移動_向上();
                    操作框窗選_位移量 = new Point();
                    操作框窗選_初始位.X = 操作方框索引.X;
                    操作框窗選_初始位.Y = 操作方框索引.Y + 顯示畫面列數索引;
                    device_system.Set_Device("T1", 200);
                    T1_操作方框移動_向上 = false;
                    cnt++;
                    isWork = true;
                }
            }

        }
        void cnt_操作方框移動_向上_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Up)
            {
                cnt++;
            }
            if (T1_操作方框移動_向上)
            {
                操作方框移動_向上();
                device_system.Set_Device("T1", 50);
                T1_操作方框移動_向上 = false;
            }
            isWork = true;
        }

        void cnt_操作方框移動_向下_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (!isWork)
            {
                if (Keys.Key_Down)
                {

                    操作方框移動_向下();
                    操作框窗選_位移量 = new Point();
                    操作框窗選_初始位.X = 操作方框索引.X;
                    操作框窗選_初始位.Y = 操作方框索引.Y + 顯示畫面列數索引;
                    device_system.Set_Device("T2", 200);
                    T2_操作方框移動_向下 = false;
                    cnt++;
                    isWork = true;
                }

            }
        }
        void cnt_操作方框移動_向下_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Down)
            {
                cnt++;
            }
            if (T2_操作方框移動_向下)
            {
                操作方框移動_向下();
                device_system.Set_Device("T2", 50);
                T2_操作方框移動_向下 = false;
            }
            isWork = true;
        }

        void cnt_操作方框移動_向左_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (!isWork)
            {
                if (Keys.Key_Left)
                {

                    操作方框移動_向左();
                    操作框窗選_位移量 = new Point();
                    操作框窗選_初始位.X = 操作方框索引.X;
                    操作框窗選_初始位.Y = 操作方框索引.Y + 顯示畫面列數索引;
                    device_system.Set_Device("T3", 200);
                    T3_操作方框移動_向左 = false;
                    cnt++;
                    isWork = true;
                }

            }
        }
        void cnt_操作方框移動_向左_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Left)
            {
                cnt++;
            }
            if (T3_操作方框移動_向左)
            {
                操作方框移動_向左();
                device_system.Set_Device("T3", 50);
                T3_操作方框移動_向左 = false;
            }
            isWork = true;
        }

        void cnt_操作方框移動_向右_檢查按下(ref byte cnt, ref bool isWork)
        {
            if (!isWork)
            {
                if (Keys.Key_Right)
                {

                    操作方框移動_向右();
                    操作框窗選_位移量 = new Point();
                    操作框窗選_初始位.X = 操作方框索引.X;
                    操作框窗選_初始位.Y = 操作方框索引.Y + 顯示畫面列數索引;
                    device_system.Set_Device("T4", 200);
                    T4_操作方框移動_向右 = false;

                    cnt++;
                    isWork = true;
                }

            }
        }
        void cnt_操作方框移動_向右_檢查放開(ref byte cnt, ref bool isWork)
        {
            if (!Keys.Key_Right)
            {
                cnt++;
            }
            if (T4_操作方框移動_向右)
            {
                操作方框移動_向右();
                device_system.Set_Device("T4", 50);
                T4_操作方框移動_向右 = false;
            }
            isWork = true;
        }
        void 操作方框移動_向上()
        {
            操作方框索引.Y--;
            while (true)
            {
                if (操作方框索引.X > 0 && (操作方框索引.Y + 顯示畫面列數索引) >= 0)
                {
                    if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.Data_no_Part)
                    {
                        操作方框索引.X--;
                    }
                    else break;
                }
                else break;
            }
            sub_檢查操作框位置是否超出範圍();
        }
        void 操作方框移動_向下()
        {
            操作方框索引.Y++;
            while (true)
            {
                if (操作方框索引.X > 0 && (操作方框索引.Y + 顯示畫面列數索引) < ladder_list.Count)
                {
                    if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.Data_no_Part)
                    {
                        操作方框索引.X--;
                    }
                    else break;
                }
                else break;
            }
            sub_檢查操作框位置是否超出範圍();
        }
        void 操作方框移動_向左()
        {
            操作方框索引.X--;
            while (true)
            {
                if (操作方框索引.X > 0)
                {
                    if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.Data_no_Part)
                    {
                        操作方框索引.X--;
                    }
                    else break;
                }
                else break;
            }
            sub_檢查操作框位置是否超出範圍();
        }
        void 操作方框移動_向右()
        {
            操作方框索引.X++;
            while (true)
            {
      
                    if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.Data_no_Part)
                    {
                        操作方框索引.X++;
                    }
          
                else break;
            }
            sub_檢查操作框位置是否超出範圍();
        }
        #endregion
        void sub_檢查操作框位置是否超出範圍()
        {
            if (操作框窗選_初始位.X == 0 && (Math.Abs(操作框窗選_位移量.X) > 0 || Math.Abs(操作框窗選_位移量.Y) > 0) && 操作框窗選_位移量.X != 11) 操作框窗選_位移量.X = 11;
            int temp_X = 操作方框索引.X;
            int temp_Y = 操作方框索引.Y;
            if (temp_Y + 顯示畫面列數索引 >= ladder_list.Count) temp_Y--;
            else
            {
                if (temp_X < 0)
                {
                    temp_Y--;
                    temp_X = 一列格數 - 2;
                }
                if (temp_X > 一列格數 - 2)
                {
                    if (temp_Y + 顯示畫面列數索引 < ladder_list.Count - 1) temp_Y++;
                    temp_X = 0;
                }
                if (temp_Y > 一個畫面列數 - 1)
                {
                    if (一個畫面列數 + 顯示畫面列數索引 < ladder_list.Count)
                    {
                        temp_Y = 一個畫面列數 - 1;
                        顯示畫面列數索引++;
                    }
                    else
                    {
                        temp_Y--;
                    }
                }
                if (temp_Y < 0)
                {
                    temp_Y = 0;
                    if (顯示畫面列數索引 > 0) 顯示畫面列數索引--;
                }
            }
         
            while(true)
            {
                if (temp_X <= 0) break;
                if (temp_Y + 顯示畫面列數索引 < 0) break;
                if (temp_Y + 顯示畫面列數索引 >= ladder_list.Count) break;
                if (ladder_list[temp_Y + 顯示畫面列數索引][temp_X].ladderType == partTypeEnum.Data_no_Part)
                {
                    temp_X--;
                }
                else break;
            }

            操作方框索引.X = temp_X;
            操作方框索引.Y = temp_Y;
           
        }
        void sub_窗選滑鼠拖曳位移量檢查()
        {
            if(窗選模式 == Tx_窗選模式.複製模式)
            {
                if (操作框窗選_位移量.X >= 0 && 操作框窗選_位移量.Y >= 0)
                {
                    if (操作框窗選_初始位.X > 一列格數 - 2) 操作框窗選_初始位.X = 一列格數 - 2;
                    for (int Y = 0; Y <= 操作框窗選_位移量.Y; Y++)
                    {
                        int temp = 0;
                        for (int X = 0; X <= 操作框窗選_位移量.X; X++)
                        {
                            LADDER ladder_temp = ladder_list[操作框窗選_初始位.Y + Y][操作框窗選_初始位.X + X];
                            if (ladder_temp.ladderType != partTypeEnum.Data_no_Part) temp += ladder_temp.元素數量;
                        }
                        if (temp > 操作框窗選_位移量.X) 操作框窗選_位移量.X = temp - 1;
                    }
                }
                else if (操作框窗選_位移量.X < 0 && 操作框窗選_位移量.Y >= 0)
                {
                    int X_temp = Math.Abs(操作框窗選_位移量.X);
                    int temp0 = 0;
                  
                    for (int Y = 0; Y <= 操作框窗選_位移量.Y; Y++)
                    {
                        int temp = 0;
                        for (int X = 0; X <= X_temp; X++)
                        {
                            LADDER ladder_temp = ladder_list[操作框窗選_初始位.Y + Y][操作框窗選_初始位.X - X];
                            if (ladder_temp.ladderType != partTypeEnum.Data_no_Part) temp += ladder_temp.元素數量;
                        }
                        if (temp > temp0) temp0 = temp;
                    }
                    if (Math.Abs(temp0) > Math.Abs(操作框窗選_位移量.X))
                    {

                        操作框窗選_初始位.X =  操作框窗選_初始位.X + (temp0 - 1) + 操作框窗選_位移量.X;
                        操作框窗選_位移量.X = -(temp0 - 1);
                        if (操作框窗選_初始位.X > 一列格數 - 2) 操作框窗選_初始位.X = 一列格數 - 2;
                    }
                }
                else if (操作框窗選_位移量.X >= 0 && 操作框窗選_位移量.Y < 0)
                {
                    int X_temp = Math.Abs(操作框窗選_位移量.X);
                    int Y_temp = Math.Abs(操作框窗選_位移量.Y);
                    int temp0 = 0;
       
                    for (int Y = 0; Y <= Y_temp; Y++)
                    {
                        int temp = 0;
                        for (int X = 0; X <= X_temp; X++)
                        {
                            LADDER ladder_temp = ladder_list[操作框窗選_初始位.Y - Y][操作框窗選_初始位.X + X];
                            if (ladder_temp.ladderType != partTypeEnum.Data_no_Part) temp += ladder_temp.元素數量;
                        }
                        if (temp > temp0) temp0 = temp;
                    }
                    if (Math.Abs(temp0) > Math.Abs(操作框窗選_位移量.X))
                    {
                        操作框窗選_初始位.X = 操作框窗選_初始位.X + (temp0 - 1) + 操作框窗選_位移量.X;
                        操作框窗選_位移量.X = -(temp0 - 1);
                        if (操作框窗選_初始位.X > 一列格數 - 2) 操作框窗選_初始位.X = 一列格數 - 2;
                    }
                }
                else if (操作框窗選_位移量.X < 0 && 操作框窗選_位移量.Y < 0)
                {
                    int X_temp = Math.Abs(操作框窗選_位移量.X);
                    int Y_temp = Math.Abs(操作框窗選_位移量.Y);
                    int temp0 = 0;
               
                    for (int Y = 0; Y <= Y_temp; Y++)
                    {
                        int temp = 0;
                        for (int X = 0; X <= X_temp; X++)
                        {
                            LADDER ladder_temp = ladder_list[操作框窗選_初始位.Y - Y][操作框窗選_初始位.X - X];
                            if (ladder_temp.ladderType != partTypeEnum.Data_no_Part) temp += ladder_temp.元素數量;
                        }
                        if (temp > temp0) temp0 = temp;
                    }
                    if (Math.Abs(temp0) > Math.Abs(操作框窗選_位移量.X))
                    {
                        操作框窗選_初始位.X = 操作框窗選_初始位.X + (temp0 - 1) + 操作框窗選_位移量.X;                   
                        操作框窗選_位移量.X = -(temp0 - 1);
                        if (操作框窗選_初始位.X > 一列格數 - 2) 操作框窗選_初始位.X = 一列格數 - 2;
                    }
                }
            }
        }
        void sub_複製模式_檢查操作框走訪方向(Point 起始位, Point 位移量, ref Point 現在位置, ref Tx_操作框走訪方向 操作框走訪方向)
        {
            Point 最終位 = new Point();
            if (位移量.X != 0 || 位移量.Y != 0)
            {
                最終位.X = 起始位.X + 位移量.X;
                最終位.Y = 起始位.Y + 位移量.Y;
                if (現在位置.X == 最終位.X)
                {
                    if (現在位置.Y == 最終位.Y)
                    {
                        操作框走訪方向 = Tx_操作框走訪方向.無;
                    }
                    else if (現在位置.Y > 最終位.Y)
                    {
                        操作框走訪方向 = Tx_操作框走訪方向.上;
                    }
                    else if (現在位置.Y < 最終位.Y)
                    {
                        操作框走訪方向 = Tx_操作框走訪方向.下;
                    }
                }
                else if (現在位置.X > 最終位.X)
                {
                    操作框走訪方向 = Tx_操作框走訪方向.左;
                }
                else if (現在位置.X < 最終位.X)
                {
                    操作框走訪方向 = Tx_操作框走訪方向.右;
                }
            }
            else 操作框走訪方向 = Tx_操作框走訪方向.無;

        }
        void sub_複製模式_操作框走訪(Point 起始位, ref Point 現在位置, Tx_操作框走訪方向 操作框走訪方向)
        {
            if (操作框走訪方向 == Tx_操作框走訪方向.右) 現在位置.X++;
            else if (操作框走訪方向 == Tx_操作框走訪方向.左) 現在位置.X--;
            else if (操作框走訪方向 == Tx_操作框走訪方向.下)
            {
                現在位置.X = 起始位.X;
                現在位置.Y++;
            }
            else if (操作框走訪方向 == Tx_操作框走訪方向.上)
            {
                現在位置.X = 起始位.X;
                現在位置.Y--;
            }

        }
        bool sub_鼠線模式_檢查操作框走訪方向(Point 起始位, Point 位移量, ref Point 現在位置, ref Tx_操作框走訪方向 操作框走訪方向)
        {
            Point 最終位 = new Point();
            if (位移量.X != 0 || 位移量.Y != 0)
            {
                最終位.X = 起始位.X + 位移量.X;
                最終位.Y = 起始位.Y + 位移量.Y;

                if (位移量.Y > 0)
                {
                    if (現在位置.Y == 最終位.Y)
                    {
                        if (現在位置.X == 最終位.X)
                        {
                            操作框走訪方向 = Tx_操作框走訪方向.無;

                        }
                        else if (現在位置.X > 最終位.X)
                        {
                            操作框走訪方向 = Tx_操作框走訪方向.左;
                        }
                        else if (現在位置.X < 最終位.X)
                        {
                            操作框走訪方向 = Tx_操作框走訪方向.右;
                        }
                    }
                    else if (現在位置.Y > 最終位.Y)
                    {
                        操作框走訪方向 = Tx_操作框走訪方向.上;
                    }
                    else if (現在位置.Y < 最終位.Y)
                    {
                        操作框走訪方向 = Tx_操作框走訪方向.下;
                    }
                }
                else if (位移量.Y <= 0)
                {
                    if (現在位置.X == 最終位.X)
                    {
                        if (現在位置.Y == 最終位.Y)
                        {
                            操作框走訪方向 = Tx_操作框走訪方向.無;
                        }
                        else if (現在位置.Y > 最終位.Y)
                        {
                            操作框走訪方向 = Tx_操作框走訪方向.上;
                        }
                        else if (現在位置.Y < 最終位.Y)
                        {
                            操作框走訪方向 = Tx_操作框走訪方向.下;
                        }
                    }
                    else if (現在位置.X > 最終位.X)
                    {
                        操作框走訪方向 = Tx_操作框走訪方向.左;
                    }
                    else if (現在位置.X < 最終位.X)
                    {
                        操作框走訪方向 = Tx_操作框走訪方向.右;
                    }
                }

            }
            return false;
        }
        void sub_鼠線模式_操作框走訪(Point 起始位, ref Point 現在位置, Tx_操作框走訪方向 操作框走訪方向)
        {
            if (操作框走訪方向 == Tx_操作框走訪方向.右) 現在位置.X++;
            else if (操作框走訪方向 == Tx_操作框走訪方向.左) 現在位置.X--;
            else if (操作框走訪方向 == Tx_操作框走訪方向.下)
            {
                現在位置.Y++;
            }
            else if (操作框走訪方向 == Tx_操作框走訪方向.上)
            {
                現在位置.Y--;
            }

        }       
        #endregion
        #region 檢查輸入指令
        byte cnt_檢查輸入指令 = 255;
        void sub_檢查輸入指令()
        {
            if (cnt_檢查輸入指令 == 255) cnt_檢查輸入指令 = 1;
            if (cnt_檢查輸入指令 == 1) cnt_檢查輸入指令_檢查輸入指令完成(ref cnt_檢查輸入指令);
            if (cnt_檢查輸入指令 == 2) cnt_檢查輸入指令_檢查模式(ref cnt_檢查輸入指令);
            if (cnt_檢查輸入指令 == 3) cnt_檢查輸入指令_檢查位置可否輸入(ref cnt_檢查輸入指令);
            if (cnt_檢查輸入指令 == 4) cnt_檢查輸入指令_檢查指令種類並寫入(ref cnt_檢查輸入指令);
            if (cnt_檢查輸入指令 == 5) cnt_檢查輸入指令 = 255;

            if (cnt_檢查輸入指令 == 50) cnt_檢查輸入指令_寫入註解(ref cnt_檢查輸入指令);
            if (cnt_檢查輸入指令 == 51) cnt_檢查輸入指令 = 255;

            if (cnt_檢查輸入指令 == 200) cnt_檢查輸入指令_錯誤位置插入指令_MessegeBox(ref cnt_檢查輸入指令);
        }
        void cnt_檢查輸入指令_檢查輸入指令完成(ref byte cnt)
        {
            if (EnterSymbol.Command_OK)
            {
                EnterSymbol.Command_OK = false;
                cnt++;
            }
        }
        void cnt_檢查輸入指令_檢查模式(ref byte cnt)
        {
            if (!EnterSymbol.註解模式)
            {
                cnt++;
            }
            else
            {
                cnt = 50;
            }
        }
        void cnt_檢查輸入指令_檢查位置可否輸入(ref byte cnt)
        {
            Point index = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            if (index.Y != ladder_list.Count -1)
            {
                cnt++;
            }
            else
            {
                cnt = 200;
            }
        }
        void cnt_檢查輸入指令_檢查指令種類並寫入(ref byte cnt)
        {
            Point index = new Point( 操作方框索引.X,操作方框索引.Y + 顯示畫面列數索引);
            bool FLAG_指令輸入成功 = false;
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.LD)
            {
                ladder_list[index.Y][index.X].ladderType = partTypeEnum.A_Part;
                ladder_list[index.Y][index.X].ladderParam[0] = EnterSymbol.Str_指令內容[0] + EnterSymbol.cmommadval[0].ToString();
                操作方框移動_向右();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.LDI)
            {
                ladder_list[index.Y][index.X].ladderType = partTypeEnum.B_Part;
                ladder_list[index.Y][index.X].ladderParam[0] = EnterSymbol.Str_指令內容[0] + EnterSymbol.cmommadval[0].ToString();
                操作方框移動_向右();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.OUT)
            {

                for (int X = index.X; X < 一列格數 - 1; X++)
                {
                    if (X == 一列格數 - 2)
                    {
                        ladder_list[index.Y][X].ladderType = partTypeEnum.OUT_Part;
                        ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0] + EnterSymbol.cmommadval[0].ToString();                
                    }
                    else ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                    FLAG_指令輸入成功 = true;
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
            }
            //--------------------------------------------------------------------------------------------------------------
            if(EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.OR)
            {
                if (index.Y - 1 >= 0)
                {
                    ladder_list[index.Y][index.X].ladderType = partTypeEnum.A_Part;
                    ladder_list[index.Y][index.X].ladderParam[0] = EnterSymbol.Str_指令內容[0] + EnterSymbol.cmommadval[0].ToString();           
                    ladder_list[index.Y][index.X].Vline_右上 = true;               
                    ladder_list[index.Y - 1][index.X].Vline_右下 = true;
                    if (!ladder_list[index.Y][index.X - 1].Vline_右上) ladder_list[index.Y][index.X - 1].Vline_右上 = true;
                    if (!ladder_list[index.Y - 1][index.X - 1].Vline_右下) ladder_list[index.Y - 1][index.X - 1].Vline_右下 = true;
                    操作方框移動_向右();
                    FLAG_指令輸入成功 = true;
                }
                else
                {
                    cnt = 200;
                    return;
                }
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.ORI)
            {
                if (index.Y - 1 >= 0)
                {
                    ladder_list[index.Y][index.X].ladderType = partTypeEnum.B_Part;
                    ladder_list[index.Y][index.X].ladderParam[0] = EnterSymbol.Str_指令內容[0] + EnterSymbol.cmommadval[0].ToString();
                    ladder_list[index.Y][index.X].Vline_右上 = true;
                    ladder_list[index.Y - 1][index.X].Vline_右下 = true;

                    if (!ladder_list[index.Y][index.X - 1].Vline_右上) ladder_list[index.Y][index.X - 1].Vline_右上 = true;
                    if (!ladder_list[index.Y - 1][index.X - 1].Vline_右下) ladder_list[index.Y - 1][index.X - 1].Vline_右下 = true;
                    操作方框移動_向右();
                    FLAG_指令輸入成功 = true;          
                }
                else
                {
                    cnt = 200;
                    return;
                }
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.LD_Equal)
            {
                if(index.X + 2 < 一列格數 -1)
                {
                    ladder_list[index.Y][index.X].ladderType = partTypeEnum.LD_Equal_Part;
                    ladder_list[index.Y][index.X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                    ladder_list[index.Y][index.X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                    ladder_list[index.Y][index.X].ladderParam[2] = EnterSymbol.Str_指令內容[2] + EnterSymbol.cmommadval[2].ToString();
                    ladder_list[index.Y][index.X + 1].ladderType = partTypeEnum.Data_no_Part;
                    ladder_list[index.Y][index.X + 2].ladderType = partTypeEnum.Data_no_Part;
                }
                操作方框移動_向右();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.MOV)
            {
                if (index.X + 2 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 4)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.MOV_Part;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                            ladder_list[index.Y][X].ladderParam[2] = EnterSymbol.Str_指令內容[2] + EnterSymbol.cmommadval[2].ToString();
                            
                        }
                        else if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }        
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.ADD)
            {
                if (index.X + 3 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 5)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.ADD_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                            ladder_list[index.Y][X].ladderParam[2] = EnterSymbol.Str_指令內容[2] + EnterSymbol.cmommadval[2].ToString();
                            ladder_list[index.Y][X].ladderParam[3] = EnterSymbol.Str_指令內容[3] + EnterSymbol.cmommadval[3].ToString();
                        }
                        else if (X == 一列格數 - 4)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.SUB)
            {
                if (index.X + 3 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 5)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.SUB_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                            ladder_list[index.Y][X].ladderParam[2] = EnterSymbol.Str_指令內容[2] + EnterSymbol.cmommadval[2].ToString();
                            ladder_list[index.Y][X].ladderParam[3] = EnterSymbol.Str_指令內容[3] + EnterSymbol.cmommadval[3].ToString();
                        }
                        else if (X == 一列格數 - 4)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.MUL)
            {
                if (index.X + 3 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 5)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.MUL_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                            ladder_list[index.Y][X].ladderParam[2] = EnterSymbol.Str_指令內容[2] + EnterSymbol.cmommadval[2].ToString();
                            ladder_list[index.Y][X].ladderParam[3] = EnterSymbol.Str_指令內容[3] + EnterSymbol.cmommadval[3].ToString();
                        }
                        else if (X == 一列格數 - 4)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.DIV)
            {
                if (index.X + 3 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 5)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.DIV_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                            ladder_list[index.Y][X].ladderParam[2] = EnterSymbol.Str_指令內容[2] + EnterSymbol.cmommadval[2].ToString();
                            ladder_list[index.Y][X].ladderParam[3] = EnterSymbol.Str_指令內容[3] + EnterSymbol.cmommadval[3].ToString();
                        }
                        else if (X == 一列格數 - 4)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.INC)
            {
                if (index.X + 1 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.INC_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.DRVA)
            {
                if (index.X + 3 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 5)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.DRVA_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                            ladder_list[index.Y][X].ladderParam[2] = EnterSymbol.Str_指令內容[2] + EnterSymbol.cmommadval[2].ToString();
                            ladder_list[index.Y][X].ladderParam[3] = EnterSymbol.Str_指令內容[3] + EnterSymbol.cmommadval[3].ToString();
                        }
                        else if (X == 一列格數 - 4)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.DRVI)
            {
                if (index.X + 3 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 5)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.DRVI_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                            ladder_list[index.Y][X].ladderParam[2] = EnterSymbol.Str_指令內容[2] + EnterSymbol.cmommadval[2].ToString();
                            ladder_list[index.Y][X].ladderParam[3] = EnterSymbol.Str_指令內容[3] + EnterSymbol.cmommadval[3].ToString();
                        }
                        else if (X == 一列格數 - 4)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.PLSV)
            {
                if (index.X + 2 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 4)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.PLSV_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                            ladder_list[index.Y][X].ladderParam[2] = EnterSymbol.Str_指令內容[2] + EnterSymbol.cmommadval[2].ToString();
                        }
                        else if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.OUT_TIMER)
            {
                if (index.X + 1 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.OUT_TIMER_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0] + EnterSymbol.cmommadval[0].ToString();
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.SET)
            {
                if (index.X + 1 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.SET_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.RST)
            {
                if (index.X + 1 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.RST_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.ZRST)
            {
                if (index.X + 2 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 4)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.ZRST_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                            ladder_list[index.Y][X].ladderParam[2] = EnterSymbol.Str_指令內容[2] + EnterSymbol.cmommadval[2].ToString();

                        }
                        else if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.BMOV)
            {
                if (index.X + 3 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 5)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.BMOV_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                            ladder_list[index.Y][X].ladderParam[2] = EnterSymbol.Str_指令內容[2] + EnterSymbol.cmommadval[2].ToString();
                            ladder_list[index.Y][X].ladderParam[3] = EnterSymbol.Str_指令內容[3] + EnterSymbol.cmommadval[3].ToString();
                        }
                        else if (X == 一列格數 - 4)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.WTB)
            {
                if (index.X + 3 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 5)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.WTB_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                            ladder_list[index.Y][X].ladderParam[2] = EnterSymbol.Str_指令內容[2] + EnterSymbol.cmommadval[2].ToString();
                            ladder_list[index.Y][X].ladderParam[3] = EnterSymbol.Str_指令內容[3] + EnterSymbol.cmommadval[3].ToString();
                        }
                        else if (X == 一列格數 - 4)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.BTW)
            {
                if (index.X + 3 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 5)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.BTW_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                            ladder_list[index.Y][X].ladderParam[2] = EnterSymbol.Str_指令內容[2] + EnterSymbol.cmommadval[2].ToString();
                            ladder_list[index.Y][X].ladderParam[3] = EnterSymbol.Str_指令內容[3] + EnterSymbol.cmommadval[3].ToString();
                        }
                        else if (X == 一列格數 - 4)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.TAB)
            {
                index.X = 1;
                for (int X = index.X; X < 一列格數 - 1; X++)
                {
                    if (X == 1)
                    {
                        ladder_list[index.Y][X].ladderType = partTypeEnum.TAB_PART;
                        ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                        ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1];
                        ladder_list[index.Y][X].ladderParam[2] = EnterSymbol.Str_指令內容[2];
                    }
                    else
                    {
                        ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                    }
                }
                
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.JUMP)
            {
                if (index.X + 1 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.JUMP_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if (EnterSymbol.commandType == EnterSymbol.CommadTypeEnum.REF)
            {
                if (index.X + 2 < 一列格數 - 1)
                {
                    for (int X = index.X; X < 一列格數 - 1; X++)
                    {
                        if (X == 一列格數 - 4)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.PLSV_PART;
                            ladder_list[index.Y][X].ladderParam[0] = EnterSymbol.Str_指令內容[0];
                            ladder_list[index.Y][X].ladderParam[1] = EnterSymbol.Str_指令內容[1] + EnterSymbol.cmommadval[1].ToString();
                            ladder_list[index.Y][X].ladderParam[2] = EnterSymbol.Str_指令內容[2] + EnterSymbol.cmommadval[2].ToString();
                        }
                        else if (X == 一列格數 - 3)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else if (X == 一列格數 - 2)
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.Data_no_Part;
                        }
                        else
                        {
                            ladder_list[index.Y][X].ladderType = partTypeEnum.H_Line_Part;
                        }
                    }
                }
                操作方框索引.X = 1;
                操作方框移動_向下();
                FLAG_指令輸入成功 = true;
            }
            //--------------------------------------------------------------------------------------------------------------
            if(FLAG_指令輸入成功)
            {
           
                Point p0 = new Point();
                Point p1 = new Point();
                p0 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, index.Y), ladder_list);
                p1 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, index.Y), ladder_list);
                for (int Y = p0.Y; Y <= p1.Y; Y++)
                {
                    ladder_list[Y][1].未編譯 = true;
                }
            }


            cnt++;
        }

        void cnt_檢查輸入指令_寫入註解(ref byte cnt)
        {
            Point index = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            for (int i = 0; i < ladder_list[index.Y][index.X].元素數量; i++ )
            {
                device.Set_Device(ladder_list[index.Y][index.X].ladderParam[i], EnterSymbol.Str_指令內容[i]);
            }
              
            cnt++;
  
        }
        void cnt_檢查輸入指令_錯誤位置插入指令_MessegeBox(ref byte cnt)
        {
            MessageBox.Show("位置無法插入指令!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            cnt = 255;
        }
        void cnt_檢查輸入指令_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        #region EnterSymbol視窗進入檢查
        bool EnterSymbol進入視窗按鈕按下 = false;
        string EnterSymbol_起始視窗TextBox文字 = "";
        string EnterSymbol_彈出視窗文字 = "";
        byte cnt_EnterSymbol視窗進入檢查 = 255;
        int code = 0;
        void sub_EnterSymbol視窗進入檢查()
        {
            if (!FLAG_有功能鍵按下)
            {
                code = 0;
                if (Keys.Key_Q) { code++; EnterSymbol_起始視窗TextBox文字 = "Q"; }
                if (Keys.Key_W) { code++; EnterSymbol_起始視窗TextBox文字 = "W"; }
                if (Keys.Key_E) { code++; EnterSymbol_起始視窗TextBox文字 = "E"; }
                if (Keys.Key_R) { code++; EnterSymbol_起始視窗TextBox文字 = "R"; }
                if (Keys.Key_T) { code++; EnterSymbol_起始視窗TextBox文字 = "T"; }
                if (Keys.Key_Y) { code++; EnterSymbol_起始視窗TextBox文字 = "Y"; }
                if (Keys.Key_U) { code++; EnterSymbol_起始視窗TextBox文字 = "U"; }
                if (Keys.Key_I) { code++; EnterSymbol_起始視窗TextBox文字 = "I"; }
                if (Keys.Key_O) { code++; EnterSymbol_起始視窗TextBox文字 = "O"; }
                if (Keys.Key_P) { code++; EnterSymbol_起始視窗TextBox文字 = "P"; }
                if (Keys.Key_A) { code++; EnterSymbol_起始視窗TextBox文字 = "A"; }
                if (Keys.Key_S) { code++; EnterSymbol_起始視窗TextBox文字 = "S"; }
                if (Keys.Key_D) { code++; EnterSymbol_起始視窗TextBox文字 = "D"; }
                if (Keys.Key_F) { code++; EnterSymbol_起始視窗TextBox文字 = "F"; }
                if (Keys.Key_G) { code++; EnterSymbol_起始視窗TextBox文字 = "G"; }
                if (Keys.Key_H) { code++; EnterSymbol_起始視窗TextBox文字 = "H"; }
                if (Keys.Key_J) { code++; EnterSymbol_起始視窗TextBox文字 = "J"; }
                if (Keys.Key_K) { code++; EnterSymbol_起始視窗TextBox文字 = "K"; }
                if (Keys.Key_L) { code++; EnterSymbol_起始視窗TextBox文字 = "L"; }
                if (Keys.Key_Z) { code++; EnterSymbol_起始視窗TextBox文字 = "Z"; }
                if (Keys.Key_X) { code++; EnterSymbol_起始視窗TextBox文字 = "X"; }
                if (Keys.Key_C) { code++; EnterSymbol_起始視窗TextBox文字 = "C"; }
                if (Keys.Key_V) { code++; EnterSymbol_起始視窗TextBox文字 = "V"; }
                if (Keys.Key_B) { code++; EnterSymbol_起始視窗TextBox文字 = "B"; }
                if (Keys.Key_N) { code++; EnterSymbol_起始視窗TextBox文字 = "N"; }
                if (Keys.Key_M) { code++; EnterSymbol_起始視窗TextBox文字 = "M"; }
                if ((Keys.Key_Enter && !Keys.Key_ShiftKey) || FLAG_picture_LADDER_MouseDoubleclick)
                {
                    code = 99;
   
                }
                if (code > 0)
                {                    
                    EnterSymbol進入視窗按鈕按下 = true;
                }
                else EnterSymbol進入視窗按鈕按下 = false;


                if (cnt_EnterSymbol視窗進入檢查 == 255) cnt_EnterSymbol視窗進入檢查 = 1;
                if (cnt_EnterSymbol視窗進入檢查 == 1) cnt_EnterSymbol視窗進入檢查_檢查按鈕未按下(ref cnt_EnterSymbol視窗進入檢查, ref FLAG_有功能鍵按下);
                if (cnt_EnterSymbol視窗進入檢查 == 2) cnt_EnterSymbol視窗進入檢查_檢查按鈕已按下(ref cnt_EnterSymbol視窗進入檢查, ref FLAG_有功能鍵按下);
                if (cnt_EnterSymbol視窗進入檢查 == 3) cnt_EnterSymbol視窗進入檢查_檢查模式及可否顯示(ref cnt_EnterSymbol視窗進入檢查, ref FLAG_有功能鍵按下);

                if (cnt_EnterSymbol視窗進入檢查 == 10) cnt_EnterSymbol視窗進入檢查_10_檢查位置是否可寫註解(ref cnt_EnterSymbol視窗進入檢查, ref FLAG_有功能鍵按下);
                if (cnt_EnterSymbol視窗進入檢查 == 11) cnt_EnterSymbol視窗進入檢查 = 30;

                if (cnt_EnterSymbol視窗進入檢查 == 20) cnt_EnterSymbol視窗進入檢查_20_檢查位置是否可寫指令(ref cnt_EnterSymbol視窗進入檢查, ref FLAG_有功能鍵按下);
                if (cnt_EnterSymbol視窗進入檢查 == 21) cnt_EnterSymbol視窗進入檢查 = 30;

                if (cnt_EnterSymbol視窗進入檢查 == 30) cnt_EnterSymbol視窗進入檢查_30_開始輸入(ref cnt_EnterSymbol視窗進入檢查, ref FLAG_有功能鍵按下);
                if (cnt_EnterSymbol視窗進入檢查 == 31) cnt_EnterSymbol視窗進入檢查_30_檢查表單關閉(ref cnt_EnterSymbol視窗進入檢查, ref FLAG_有功能鍵按下);
                if (cnt_EnterSymbol視窗進入檢查 == 32) cnt_EnterSymbol視窗進入檢查 = 150;

                if (cnt_EnterSymbol視窗進入檢查 == 150) cnt_EnterSymbol視窗進入檢查_150_輸入完成(ref cnt_EnterSymbol視窗進入檢查, ref FLAG_有功能鍵按下);
                if (cnt_EnterSymbol視窗進入檢查 == 151) cnt_EnterSymbol視窗進入檢查 = 240;

                if (cnt_EnterSymbol視窗進入檢查 == 200) cnt_EnterSymbol視窗進入檢查_200_輸入失敗(ref cnt_EnterSymbol視窗進入檢查, ref FLAG_有功能鍵按下);
                if (cnt_EnterSymbol視窗進入檢查 == 201) cnt_EnterSymbol視窗進入檢查 = 240;

                if (cnt_EnterSymbol視窗進入檢查 == 240) cnt_EnterSymbol視窗進入檢查_240_彈出視窗(ref cnt_EnterSymbol視窗進入檢查, ref FLAG_有功能鍵按下);
                if (cnt_EnterSymbol視窗進入檢查 == 241) cnt_EnterSymbol視窗進入檢查 = 255;
            }
            else cnt_EnterSymbol視窗進入檢查 = 255;
        }
        void cnt_EnterSymbol視窗進入檢查_檢查按鈕未按下(ref byte cnt, ref bool isWork)
        {
            if (!EnterSymbol進入視窗按鈕按下 && !EnterSymbol.視窗已建立 ||true)
            {
                cnt++;
            }     
        }
        void cnt_EnterSymbol視窗進入檢查_檢查按鈕已按下(ref byte cnt, ref bool isWork)
        {
            if (EnterSymbol進入視窗按鈕按下 && !EnterSymbol.視窗已建立 && !isWork)
            {
                isWork = true;
                cnt++;
            }          
        }
        void cnt_EnterSymbol視窗進入檢查_檢查模式及可否顯示(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            if (FLAG_編譯視窗獲取焦點 && !EnterSymbol.視窗已建立)
            {
                EnterSymbol_彈出視窗文字 = "";
                if (exButton_程式_註解模式選擇.Load_WriteState())
                {
                    cnt = 10;
                }
                else
                {
                    cnt = 20;
                }
            }
            else
            {
                cnt = 150;
            }
            
        }
        void cnt_EnterSymbol視窗進入檢查_10_檢查位置是否可寫註解(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            EnterSymbol.註解模式 = true;

            object comment =new object();
            EnterSymbol.device = device;
            int 可輸入註解接點數量 = 0;
            for (int i = 0; i < ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].元素數量; i++)
            {
                if (DEVICE.TestDevice(ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderParam[i])) 可輸入註解接點數量++;
            }     
            if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.A_Part && 可輸入註解接點數量 > 0)
            {                           
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.B_Part && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.OUT_Part && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.LD_Equal_Part && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.MOV_Part && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.ADD_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.SUB_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.MUL_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.DIV_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.INC_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.DRVA_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.DRVI_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.PLSV_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.OUT_TIMER_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.SET_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.RST_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.ZRST_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.BMOV_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.BTW_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.WTB_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }
            else if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.REF_PART && 可輸入註解接點數量 > 0)
            {
                cnt++;
            }   
            else
            {
                EnterSymbol_彈出視窗文字 = "";
                cnt = 200;
            }
            if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType != partTypeEnum.leftParenthesis || ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType != partTypeEnum.rightParenthesis)
            {
                if (操作方框索引.Y + 顯示畫面列數索引 != ladder_list.Count - 1)
                if (code != 99) cnt = 20;
            }
        }
        void cnt_EnterSymbol視窗進入檢查_20_檢查位置是否可寫指令(ref byte cnt, ref bool isWork)
        {
            isWork = true;
  
            if ((操作方框索引.Y + 顯示畫面列數索引 <= ladder_list.Count - 2) && (操作方框索引.X != 0) && (操作方框索引.X != 一列格數 - 1))
            {
                if (Keys.Key_Enter || FLAG_picture_LADDER_MouseDoubleclick)
                {
                    string str_temp = "";
                    int X_temp = 操作方框索引.X;
                    int Y_temp = 操作方框索引.Y + 顯示畫面列數索引;
                    LADDER ladder_temp;
                    while(true)
                    {
                        if (X_temp <= 0)
                        {
                            ladder_temp = ladder_list[Y_temp][X_temp];
                            break;
                        }
                        if (ladder_list[Y_temp][X_temp].ladderType == partTypeEnum.Data_no_Part) X_temp--;
                        else
                        {
                            ladder_temp = ladder_list[Y_temp][X_temp];
                            break;
                        }
                    }
                    if (ladder_temp.ladderType == partTypeEnum.noPart)
                    {
                        str_temp = "";
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.A_Part)
                    {
                        str_temp += "LD ";
                        str_temp += ladder_temp.ladderParam[0];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.B_Part)
                    {
                        str_temp += "LDI ";
                        str_temp += ladder_temp.ladderParam[0];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.OUT_Part)
                    {
                        str_temp += "OUT ";
                        str_temp += ladder_temp.ladderParam[0];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.LD_Equal_Part)
                    {
                        str_temp += "LD";
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.MOV_Part)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.ADD_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[3];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.SUB_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[3];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.MUL_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[3];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.DIV_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[3];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.INC_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.DRVA_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[3];       
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.DRVI_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[3];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.PLSV_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.OUT_TIMER_PART)
                    {
                        str_temp += "OUT ";
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.SET_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];     
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.RST_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.ZRST_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];         
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.BMOV_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[3];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.WTB_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[3];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.BTW_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[3];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.TAB_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.JUMP_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                    }
                    else if (ladder_temp.ladderType == partTypeEnum.REF_PART)
                    {
                        str_temp += ladder_temp.ladderParam[0];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[1];
                        str_temp += " ";
                        str_temp += ladder_temp.ladderParam[2];
                    }
                    EnterSymbol_起始視窗TextBox文字 = str_temp;
                    FLAG_picture_LADDER_MouseDoubleclick = false;
                }

                cnt++;
            }
            else
            {
                EnterSymbol_彈出視窗文字 = "";
                cnt = 200;
            }
            EnterSymbol.起始視窗文字 = EnterSymbol_起始視窗TextBox文字;
            EnterSymbol.註解模式 = false;
        }
        void cnt_EnterSymbol視窗進入檢查_30_開始輸入(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            Point p0 = this.PointToScreen(new Point());
            Point p1 = new Point(操作方框索引.X * 操作方框大小.Width + this.Location.X, 操作方框索引.Y * 操作方框大小.Height + this.Location.Y + 操作方框大小.Height);     
            EnterSymbol.LADDER_buf = ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X];
            p1.X += p0.X;
            p1.Y += p0.Y;
            EnterSymbol form = EnterSymbol.GetForm(p1);
            form.TopLevel = true;//將表單顯示在最上層。
            form.Activate();//啟動表單並且給予焦點。
            cnt++;
            Keys.KeyReset();
            FLAG_picture_LADDER_MouseDoubleclick = false;
            form.ShowDialog();          
        }
        void cnt_EnterSymbol視窗進入檢查_30_檢查表單關閉(ref byte cnt, ref bool isWork)
        {
            if (!EnterSymbol.視窗已建立) cnt++;

        }
        void cnt_EnterSymbol視窗進入檢查_150_輸入完成(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }
        void cnt_EnterSymbol視窗進入檢查_200_輸入失敗(ref byte cnt, ref bool isWork)
        {     
            cnt++;
        }
        void cnt_EnterSymbol視窗進入檢查_240_彈出視窗(ref byte cnt, ref bool isWork)
        {
            if (EnterSymbol_彈出視窗文字 != "")
            {
                MessageBox.Show(EnterSymbol_彈出視窗文字, " ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            cnt++;
        }
        void cnt_EnterSymbol視窗進入檢查_(ref byte cnt, ref bool isWork)
        {
            cnt++;
        }
        #endregion
        #region FindDevice視窗進入檢查
        byte cnt_FindDevice視窗進入檢查 = 255;
        void sub_FindDevice視窗進入檢查()
        {
            if (cnt_FindDevice視窗進入檢查 == 1) cnt_FindDevice視窗進入檢查_檢查可開啟尋找元件視窗(ref cnt_FindDevice視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_FindDevice視窗進入檢查 == 2) cnt_FindDevice視窗進入檢查_尋找元件視窗初始化(ref cnt_FindDevice視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_FindDevice視窗進入檢查 == 3) cnt_FindDevice視窗進入檢查_開啟視窗(ref cnt_FindDevice視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_FindDevice視窗進入檢查 == 4) cnt_FindDevice視窗進入檢查 = 255;
        }
        void cnt_FindDevice視窗進入檢查_檢查可開啟尋找元件視窗(ref byte cnt, ref bool isWork)
        {
            if (FLAG_主視窗獲取焦點 && !FindDevice.視窗已建立)
            {
                cnt++;
            }
            else cnt = 255;
            isWork = true;
        }
        void cnt_FindDevice視窗進入檢查_尋找元件視窗初始化(ref byte cnt, ref bool isWork)
        {

            cnt++;
            isWork = true;
        }
        void cnt_FindDevice視窗進入檢查_開啟視窗(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            Point p0 = this.PointToScreen(new Point());
            Point p1 = new Point((操作方框索引.X) * 操作方框大小.Width + this.Location.X, 操作方框索引.Y * 操作方框大小.Height + this.Location.Y );
            p1.X += p0.X;
            p1.Y += p0.Y;
  
            FindDevice.LADDER_buf = ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X];
            FindDevice.ladder_list = ladder_list;
            FindDevice.一列格數 = 一列格數;
            FindDevice.一個畫面列數 = 一個畫面列數;
            FindDevice.操作方框大小 = 操作方框大小;
            FindDevice.視窗原點 = p0;
            FindDevice form = FindDevice.GetForm(p1);
            form.TopLevel = true;//將表單顯示在最上層。
            form.Activate();//啟動表單並且給予焦點。   
            cnt++;
            form.ShowDialog();
            isWork = true;
        }
        #endregion
        #region ReplaceDevice視窗進入檢查
        byte cnt_ReplaceDevice視窗進入檢查 = 255;
        void sub_ReplaceDevice視窗進入檢查()
        {
            if (cnt_ReplaceDevice視窗進入檢查 == 1) cnt_ReplaceDevice視窗進入檢查_檢查可開啟尋找元件視窗(ref cnt_ReplaceDevice視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_ReplaceDevice視窗進入檢查 == 2) cnt_ReplaceDevice視窗進入檢查_檢查未編譯(ref cnt_ReplaceDevice視窗進入檢查, ref FLAG_有功能鍵按下);           
            if (cnt_ReplaceDevice視窗進入檢查 == 3) cnt_ReplaceDevice視窗進入檢查_尋找元件視窗初始化(ref cnt_ReplaceDevice視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_ReplaceDevice視窗進入檢查 == 4) cnt_ReplaceDevice視窗進入檢查_開啟視窗(ref cnt_ReplaceDevice視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_ReplaceDevice視窗進入檢查 == 5) cnt_ReplaceDevice視窗進入檢查 = 255;
        }
        void cnt_ReplaceDevice視窗進入檢查_檢查可開啟尋找元件視窗(ref byte cnt, ref bool isWork)
        {
            if (FLAG_主視窗獲取焦點 && !ReplaceDevice.視窗已建立)
            {
                cnt++;
            }
            else cnt = 255;
            isWork = true;
        }
        void cnt_ReplaceDevice視窗進入檢查_檢查未編譯(ref byte cnt, ref bool isWork)
        {
            if (FLAG_有程式未編譯)
            {
                cnt = 255;
                MessageBox.Show("程式未編譯!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_ReplaceDevice視窗進入檢查_尋找元件視窗初始化(ref byte cnt, ref bool isWork)
        {

            cnt++;
            isWork = true;
        }
        void cnt_ReplaceDevice視窗進入檢查_開啟視窗(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            Point p0 = this.PointToScreen(new Point());
            Point p1 = new Point(pictureBox_LADDER.Width / 2 + this.Location.X , pictureBox_LADDER.Height / 2 + this.Location.Y);
            p1.X += p0.X;
            p1.Y += p0.Y;

            ReplaceDevice.LADDER_buf = new LADDER();
            ReplaceDevice.LADDER_buf = ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X];


            ReplaceDevice.ladder_list_buf = List_LADDER_All_Copy(ladder_list);

            ReplaceDevice.起始鼠標列數 = 操作方框索引.Y + 顯示畫面列數索引 ;
            ReplaceDevice.一列格數 = 一列格數;
            ReplaceDevice.一個畫面列數 = 一個畫面列數;
            ReplaceDevice.device = device;
            if (窗選模式 == Tx_窗選模式.複製模式)
            {
                int upper = 操作框窗選_初始位.Y;
                int lower = 操作框窗選_初始位.Y + 操作框窗選_位移量.Y;
                int buf = 0;
                if(lower > upper)
                {
                    buf = upper;
                    upper = lower;
                    lower = buf;
                }
                Point p10 = sub_搜尋指定位置階梯圖右上節點位置(new Point(1, lower), ladder_list);
                Point p11 = sub_搜尋指定位置階梯圖右下節點位置(new Point(1, upper), ladder_list);
                if (ladder_list[p10.Y][0].ladderParam[0] != "" && ladder_list[p10.Y][0].ladderParam[0] != null) Int32.TryParse(ladder_list[p10.Y][0].ladderParam[0], out ReplaceDevice.搜索範圍下限);

                if (p11.Y + 1 < ladder_list.Count)
                {
                    p11.Y++;
                    if (ladder_list[p11.Y][0].ladderParam[0] != "" && ladder_list[p11.Y][0].ladderParam[0] != null) Int32.TryParse(ladder_list[p11.Y][0].ladderParam[0], out ReplaceDevice.搜索範圍上限);
                    ReplaceDevice.搜索範圍上限--;
                }
                else
                {
                    if (ladder_list[p11.Y][0].ladderParam[0] != "" && ladder_list[p11.Y][0].ladderParam[0] != null) Int32.TryParse(ladder_list[p11.Y][0].ladderParam[0], out ReplaceDevice.搜索範圍上限);
                }
                  
            }

            ReplaceDevice form = ReplaceDevice.GetForm(p1);
            form.TopLevel = true;//將表單顯示在最上層。
            form.Activate();//啟動表單並且給予焦點。   
            cnt++;
            form.ShowDialog();
            isWork = true;
        }
        #endregion
        #region TransferSetup視窗進入檢查
        byte cnt_TransferSetup視窗進入檢查 = 255;
        void sub_TransferSetup視窗進入檢查()
        {
            if (cnt_TransferSetup視窗進入檢查 == 1) cnt_TransferSetup視窗進入檢查_檢查可開啟尋找元件視窗(ref cnt_TransferSetup視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_TransferSetup視窗進入檢查 == 2) cnt_TransferSetup視窗進入檢查_尋找元件視窗初始化(ref cnt_TransferSetup視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_TransferSetup視窗進入檢查 == 3) cnt_TransferSetup視窗進入檢查_開啟視窗(ref cnt_TransferSetup視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_TransferSetup視窗進入檢查 == 4) cnt_TransferSetup視窗進入檢查 = 255;
        }
        void cnt_TransferSetup視窗進入檢查_檢查可開啟尋找元件視窗(ref byte cnt, ref bool isWork)
        {
            if (FLAG_主視窗獲取焦點 && !TransferSetup.視窗已建立)
            {
                cnt++;
            }
            else cnt = 255;
            isWork = true;
        }
        void cnt_TransferSetup視窗進入檢查_尋找元件視窗初始化(ref byte cnt, ref bool isWork)
        {

            cnt++;
            isWork = true;
        }
        void cnt_TransferSetup視窗進入檢查_開啟視窗(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            Point p0 = this.PointToScreen(new Point());
            Point p1 = new Point(pictureBox_LADDER.Width / 2 + this.Location.X, pictureBox_LADDER.Height / 2 + this.Location.Y);
            p1.X += p0.X;
            p1.Y += p0.Y;


            TransferSetup form = TransferSetup.GetForm(p1);
            //form.TopLevel = true;//將表單顯示在最上層。
            form.Activate();//啟動表單並且給予焦點。   
            cnt++;
            form.ShowDialog();
            isWork = true;
        }
        #endregion
        #region Upload視窗進入檢查
        byte cnt_Upload視窗進入檢查 = 255;
        bool FLAG_Upload視窗_快閃模式 = false;
        void sub_Upload視窗進入檢查()
        {
            if (cnt_Upload視窗進入檢查 == 1) cnt_Upload視窗進入檢查_檢查可開啟尋找元件視窗(ref cnt_Upload視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_Upload視窗進入檢查 == 2) cnt_Upload視窗進入檢查_檢查未編譯(ref cnt_Upload視窗進入檢查, ref FLAG_有功能鍵按下);           
            if (cnt_Upload視窗進入檢查 == 3) cnt_Upload視窗進入檢查_尋找元件視窗初始化(ref cnt_Upload視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_Upload視窗進入檢查 == 4) cnt_Upload視窗進入檢查_開啟視窗(ref cnt_Upload視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_Upload視窗進入檢查 == 5) cnt_Upload視窗進入檢查 = 255;
            if (cnt_Upload視窗進入檢查 == 255) FLAG_Upload視窗_快閃模式 = false;
        }
        void cnt_Upload視窗進入檢查_檢查可開啟尋找元件視窗(ref byte cnt, ref bool isWork)
        {
            if (FLAG_主視窗獲取焦點 && !Upload.視窗已建立)
            {
                cnt++;
            }
            else cnt = 255;
            isWork = true;
        }
        void cnt_Upload視窗進入檢查_檢查未編譯(ref byte cnt, ref bool isWork)
        {
            if(FLAG_有程式未編譯)
            {
                cnt = 255;
                MessageBox.Show("程式未編譯!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_Upload視窗進入檢查_尋找元件視窗初始化(ref byte cnt, ref bool isWork)
        {

            cnt++;
            isWork = true;
        }
        void cnt_Upload視窗進入檢查_開啟視窗(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            Point p0 = this.PointToScreen(new Point());
            Point p1 = new Point(pictureBox_LADDER.Width / 2 + this.Location.X, pictureBox_LADDER.Height / 2 + this.Location.Y);
            p1.X += p0.X;
            p1.Y += p0.Y;
            Upload form = Upload.GetForm(p1);
            form.FLAG_快閃模式 = FLAG_Upload視窗_快閃模式;
            form.TopLevel = true;//將表單顯示在最上層。
            form.Activate();//啟動表單並且給予焦點。   
            cnt++;
            form.ShowDialog();
            isWork = true;
        }
        #endregion
        #region Download視窗進入檢查
        byte cnt_Download視窗進入檢查 = 255;
        void sub_Download視窗進入檢查()
        {
            if (cnt_Download視窗進入檢查 == 1) cnt_Download視窗進入檢查_檢查可開啟尋找元件視窗(ref cnt_Download視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_Download視窗進入檢查 == 2) cnt_Download視窗進入檢查_檢查未編譯(ref cnt_Download視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_Download視窗進入檢查 == 3) cnt_Download視窗進入檢查_尋找元件視窗初始化(ref cnt_Download視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_Download視窗進入檢查 == 4) cnt_Download視窗進入檢查_開啟視窗(ref cnt_Download視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_Download視窗進入檢查 == 5) cnt_Download視窗進入檢查 = 255;
        }
        void cnt_Download視窗進入檢查_檢查可開啟尋找元件視窗(ref byte cnt, ref bool isWork)
        {
            if (FLAG_主視窗獲取焦點 && !Download.視窗已建立)
            {
                cnt++;
            }
            else cnt = 255;
            isWork = true;
        }
        void cnt_Download視窗進入檢查_檢查未編譯(ref byte cnt, ref bool isWork)
        {
            if (FLAG_有程式未編譯)
            {
                cnt = 255;
                MessageBox.Show("程式未編譯!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_Download視窗進入檢查_尋找元件視窗初始化(ref byte cnt, ref bool isWork)
        {

            cnt++;
            isWork = true;
        }
        void cnt_Download視窗進入檢查_開啟視窗(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            Point p0 = this.PointToScreen(new Point());
            Point p1 = new Point(pictureBox_LADDER.Width / 2 + this.Location.X, pictureBox_LADDER.Height / 2 + this.Location.Y);
            p1.X += p0.X;
            p1.Y += p0.Y;
            Download form = Download.GetForm(p1);
            form.TopLevel = true;//將表單顯示在最上層。
            form.Activate();//啟動表單並且給予焦點。   
            cnt++;
            form.ShowDialog();
            isWork = true;
        }
        static public byte cnt_Download_程式反編譯及註解寫入 = 255;
        static public bool FLAG_Download_程式反編譯及註解寫入_註解要寫入 = false;
        String str_Download_程式反編譯及註解寫入彈出視窗文字 = "";
        void sub_Download_程式反編譯及註解寫入()
        {
            if (cnt_Download_程式反編譯及註解寫入 == 1) cnt_Download_程式反編譯及註解寫入 = 10;
            if (cnt_Download_程式反編譯及註解寫入 == 10) cnt_Download_程式反編譯及註解寫入_10_反編譯前初始化(ref cnt_Download_程式反編譯及註解寫入);
            if (cnt_Download_程式反編譯及註解寫入 == 11) cnt_Download_程式反編譯及註解寫入_10_檢查反編譯_READY(ref cnt_Download_程式反編譯及註解寫入);
            if (cnt_Download_程式反編譯及註解寫入 == 12) cnt_Download_程式反編譯及註解寫入_10_執行反編譯(ref cnt_Download_程式反編譯及註解寫入);
            if (cnt_Download_程式反編譯及註解寫入 == 13) cnt_Download_程式反編譯及註解寫入_10_檢查反編譯結果(ref cnt_Download_程式反編譯及註解寫入);
            if (cnt_Download_程式反編譯及註解寫入 == 14) cnt_Download_程式反編譯及註解寫入 = 20;

            if (cnt_Download_程式反編譯及註解寫入 == 20) cnt_Download_程式反編譯及註解寫入_20_編譯前初始化(ref cnt_Download_程式反編譯及註解寫入);
            if (cnt_Download_程式反編譯及註解寫入 == 21) cnt_Download_程式反編譯及註解寫入_20_檢查編譯_READY(ref cnt_Download_程式反編譯及註解寫入);
            if (cnt_Download_程式反編譯及註解寫入 == 22) cnt_Download_程式反編譯及註解寫入_20_檢查編譯_OVER(ref cnt_Download_程式反編譯及註解寫入);
            if (cnt_Download_程式反編譯及註解寫入 == 23) cnt_Download_程式反編譯及註解寫入_20_檢查編譯結果(ref cnt_Download_程式反編譯及註解寫入);
            if (cnt_Download_程式反編譯及註解寫入 == 24) cnt_Download_程式反編譯及註解寫入_20_記憶上一步(ref cnt_Download_程式反編譯及註解寫入);
            if (cnt_Download_程式反編譯及註解寫入 == 25) cnt_Download_程式反編譯及註解寫入_20_清除註解(ref cnt_Download_程式反編譯及註解寫入);
            if (cnt_Download_程式反編譯及註解寫入 == 26) cnt_Download_程式反編譯及註解寫入_20_寫入註解(ref cnt_Download_程式反編譯及註解寫入);
            if (cnt_Download_程式反編譯及註解寫入 == 27) cnt_Download_程式反編譯及註解寫入 = 150;

            if (cnt_Download_程式反編譯及註解寫入 == 150) cnt_Download_程式反編譯及註解寫入_150_讀取成功(ref cnt_Download_程式反編譯及註解寫入);
            if (cnt_Download_程式反編譯及註解寫入 == 151) cnt_Download_程式反編譯及註解寫入 = 240;

            if (cnt_Download_程式反編譯及註解寫入 == 200) cnt_Download_程式反編譯及註解寫入_200_讀取失敗(ref cnt_Download_程式反編譯及註解寫入);
            if (cnt_Download_程式反編譯及註解寫入 == 201) cnt_Download_程式反編譯及註解寫入 = 240;

            if (cnt_Download_程式反編譯及註解寫入 == 240) cnt_Download_程式反編譯及註解寫入_240_彈出視窗(ref cnt_Download_程式反編譯及註解寫入);
            if (cnt_Download_程式反編譯及註解寫入 == 241) cnt_Download_程式反編譯及註解寫入 = 255;
        }
        void cnt_Download_程式反編譯及註解寫入_10_反編譯前初始化(ref byte cnt)
        {
            程式編譯_IL轉換為階梯圖_輸入參數.IL指令程式.Clear();
            foreach (string[] array_str in TopMachine.properties.Program)
            {
                string[] str_temp = new string[array_str.Length];
                for (int i = 0; i < str_temp.Length; i++)
                {
                    str_temp[i] = array_str[i];
                }
                程式編譯_IL轉換為階梯圖_輸入參數.IL指令程式.Add(str_temp);
            }
            程式編譯_IL轉換為階梯圖_輸入參數.彈出視窗要顯示 = false;
            cnt++;
        }
        void cnt_Download_程式反編譯及註解寫入_10_檢查反編譯_READY(ref byte cnt)
        {
            if (cnt_程式編譯_IL轉換為階梯圖 == 255)
            {
                cnt_程式編譯_IL轉換為階梯圖 = 1;
                cnt++;
            }

        }
        void cnt_Download_程式反編譯及註解寫入_10_執行反編譯(ref byte cnt)
        {
            sub_程式編譯_IL轉換為階梯圖();
            if (cnt_程式編譯_IL轉換為階梯圖 == 255) cnt++;

        }
        void cnt_Download_程式反編譯及註解寫入_10_檢查反編譯結果(ref byte cnt)
        {
            if (程式編譯_IL轉換為階梯圖_輸出參數.轉換NG)
            {
                str_Download_程式反編譯及註解寫入彈出視窗文字 = "IL指令轉換異常!";
                cnt = 200;
            }
            else
            {
                ladder_list = List_LADDER_All_Copy(程式編譯_IL轉換為階梯圖_輸出參數.list);
                cnt++;
            }
        }
        void cnt_Download_程式反編譯及註解寫入_20_編譯前初始化(ref byte cnt)
        {
            程式編譯_輸入參數.彈出視窗要顯示 = false;
            IL指令程式.Clear();
            cnt++;
        }
        void cnt_Download_程式反編譯及註解寫入_20_檢查編譯_READY(ref byte cnt)
        {

            if (cnt_程式編譯 == 255)
            {
                cnt_程式編譯 = 1;
                cnt++;
            }
        }
        void cnt_Download_程式反編譯及註解寫入_20_檢查編譯_OVER(ref byte cnt)
        {
            //sub_程式編譯();
            if (cnt_程式編譯 == 255)
            {
                cnt++;
            }

        }
        bool 編譯與反編譯IL指令相符 = true;
        void cnt_Download_程式反編譯及註解寫入_20_檢查編譯結果(ref byte cnt)
        {
            編譯與反編譯IL指令相符 = true;
            if (編譯與反編譯IL指令相符)
            {
                //if (程式編譯_輸出參數.IL指令程式.Count != IL指令程式.Count) 編譯與反編譯IL指令相符 = false;
            }

            /*if (編譯與反編譯IL指令相符)
            {
                for (int i = 0; i < 程式編譯_輸出參數.IL指令程式.Count; i++)
                {
                    string[] str_0 = 程式編譯_輸出參數.IL指令程式[i];
                    string[] str_1 = IL指令程式[i];
                    if (str_0.Length != str_1.Length)
                    {
                        編譯與反編譯IL指令相符 = false;
                        break;
                    }
                    for (int j = 0; j < str_0.Length; j++)
                    {
                        if (str_0[j] != str_1[j])
                        {
                            編譯與反編譯IL指令相符 = false;
                            break;
                        }
                    }
                }
            }*/
            cnt++;
        }
        void cnt_Download_程式反編譯及註解寫入_20_記憶上一步(ref byte cnt)
        {
            sub_記憶上一步();
            cnt++;
        }
        void cnt_Download_程式反編譯及註解寫入_20_清除註解(ref byte cnt)
        {
            if (FLAG_Download_程式反編譯及註解寫入_註解要寫入) device.Clear_Comment();
            cnt++;
        }
        void cnt_Download_程式反編譯及註解寫入_20_寫入註解(ref byte cnt)
        {
            if(FLAG_Download_程式反編譯及註解寫入_註解要寫入)device.Set_Comment(TopMachine.properties.Comment);
            if (程式編譯_輸出參數.編譯失敗次數 == 0 && 編譯與反編譯IL指令相符)
            {
                cnt++;
            }
            else
            {
                if (!(程式編譯_輸出參數.編譯失敗次數 == 0)) str_Download_程式反編譯及註解寫入彈出視窗文字 = "編譯失敗!";
                if (!(編譯與反編譯IL指令相符)) str_Download_程式反編譯及註解寫入彈出視窗文字 = "編譯與反編譯結果不相符!";
                cnt = 200;
            }
        }

        void cnt_Download_程式反編譯及註解寫入_150_讀取成功(ref byte cnt)
        {
            cnt_DataTable_初始化 = 1;
            str_Download_程式反編譯及註解寫入彈出視窗文字 = "";
            cnt++;
        }
        void cnt_Download_程式反編譯及註解寫入_200_讀取失敗(ref byte cnt)
        {
            //ladder_list = List_LADDER_All_Copy(ladder_list_備份);
            //  ladder_list.Clear();
            //  FLAG_初始化 = false;
            IL指令程式.Clear();
            cnt++;
        }
        void cnt_Download_程式反編譯及註解寫入_240_彈出視窗(ref byte cnt)
        {
            操作方框索引.X = 0;
            操作方框索引.Y = 0;
            顯示畫面列數索引 = 0;
            if (str_Download_程式反編譯及註解寫入彈出視窗文字 != "")
            {
                MessageBox.Show(str_Download_程式反編譯及註解寫入彈出視窗文字, " ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            cnt++;
        }

        #endregion
        #region Verify視窗進入檢查
        byte cnt_Verify視窗進入檢查 = 255;
        void sub_Verify視窗進入檢查()
        {
            if (cnt_Verify視窗進入檢查 == 1) cnt_Verify視窗進入檢查_檢查可開啟尋找元件視窗(ref cnt_Verify視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_Verify視窗進入檢查 == 2) cnt_Verify視窗進入檢查_檢查未編譯(ref cnt_Verify視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_Verify視窗進入檢查 == 3) cnt_Verify視窗進入檢查_尋找元件視窗初始化(ref cnt_Verify視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_Verify視窗進入檢查 == 4) cnt_Verify視窗進入檢查_開啟視窗(ref cnt_Verify視窗進入檢查, ref FLAG_有功能鍵按下);
            if (cnt_Verify視窗進入檢查 == 5) cnt_Verify視窗進入檢查 = 255;
        }
        void cnt_Verify視窗進入檢查_檢查可開啟尋找元件視窗(ref byte cnt, ref bool isWork)
        {
            if (FLAG_主視窗獲取焦點 && !Verify.視窗已建立)
            {
                cnt++;
            }
            else cnt = 255;
            isWork = true;
        }
        void cnt_Verify視窗進入檢查_檢查未編譯(ref byte cnt, ref bool isWork)
        {
            if (FLAG_有程式未編譯)
            {
                cnt = 255;
                MessageBox.Show("程式未編譯!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                cnt++;
            }
            isWork = true;
        }
        void cnt_Verify視窗進入檢查_尋找元件視窗初始化(ref byte cnt, ref bool isWork)
        {

            cnt++;
            isWork = true;
        }
        void cnt_Verify視窗進入檢查_開啟視窗(ref byte cnt, ref bool isWork)
        {
            isWork = true;
            Point p0 = this.PointToScreen(new Point());
            Point p1 = new Point(pictureBox_LADDER.Width / 2 + this.Location.X, pictureBox_LADDER.Height / 2 + this.Location.Y);
            p1.X += p0.X;
            p1.Y += p0.Y;
            Verify form = Verify.GetForm(p1);
            form.Verify_Program = IL指令程式.DeepClone();
            form.TopLevel = true;//將表單顯示在最上層。
            form.Activate();//啟動表單並且給予焦點。   
            cnt++;
            form.ShowDialog();
            isWork = true;
        }
        #endregion
        #region 控件事件
        bool FLAG_pictureBox_LADDER_MouseDown = false;
        bool FLAG_picture_LADDER_MouseDoubleclick = false;
        private void pictureBox_LADDER_MouseDown(object sender, MouseEventArgs e)
        {
            vScrollBar_picture_滾動條.Focus();
            pictureBox_LADDER.Focus();
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (FLAG_編譯視窗獲取焦點)
                {
                    if(Keys.Key_ShiftKey)
                    {
                        Point p0 = new Point();
                        p0.X = e.X;
                        p0.Y = e.Y;
                        p0.X = (int)(p0.X / (float)操作方框大小.Width);
                        p0.Y = (int)(p0.Y / (float)操作方框大小.Height);
                        if (p0.X <= 一列格數 - 2) if (p0.Y < ladder_list.Count) 操作方框索引 = p0;
                        while (true)
                        {
                            if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.Data_no_Part)
                            {
                                操作方框索引.X--;
                            }
                            else break;
                        }
                        操作框窗選_位移量.X = -(操作框窗選_初始位.X - 操作方框索引.X);
                        操作框窗選_位移量.Y = -(操作框窗選_初始位.Y - (操作方框索引.Y + 顯示畫面列數索引));
                        sub_窗選滑鼠拖曳位移量檢查();
                    }
                    else
                    {
                        Point p0 = new Point();
                        p0.X = e.X;
                        p0.Y = e.Y;
                        p0.X = (int)(p0.X / (float)操作方框大小.Width);
                        p0.Y = (int)(p0.Y / (float)操作方框大小.Height);
                        if (p0.X <= 一列格數 - 2) if (p0.Y < ladder_list.Count) 操作方框索引 = p0;
                        while (true)
                        {
                            if (操作方框索引.Y + 顯示畫面列數索引 < ladder_list.Count)
                            {
                                if (ladder_list[操作方框索引.Y + 顯示畫面列數索引][操作方框索引.X].ladderType == partTypeEnum.Data_no_Part)
                                {
                                    操作方框索引.X--;
                                }
                                else break;
                            }
                            else break;
                        }
                        操作框窗選_初始位.X = 操作方框索引.X;
                        操作框窗選_初始位.Y = 操作方框索引.Y + 顯示畫面列數索引;
                        操作框窗選_位移量 = new Point();
                        FLAG_pictureBox_LADDER_MouseDown = true;
                    }
                }
            }
  
        }
        private void pictureBox_LADDER_MouseMove(object sender, MouseEventArgs e)
        {
            if(FLAG_pictureBox_LADDER_MouseDown)
            {
                Point 操作框移動後位置 = new Point();
                Point p0 = new Point();
                p0.X = e.X;
                p0.Y = e.Y;
                p0.X = (int)(p0.X / (float)操作方框大小.Width);
                p0.Y = (int)(p0.Y / (float)操作方框大小.Height);
                int temp_X = 操作方框索引.X;
                int temp_Y = 操作方框索引.Y;
                if (p0.X <= 一列格數 - 2)
                {
                    if (p0.Y < ladder_list.Count)
                    {

                     
                        操作框移動後位置.X = 操作方框索引.X - p0.X;
                        操作框移動後位置.Y = 操作方框索引.Y - p0.Y;
                       
                        if (操作框移動後位置.X == 0)
                        {
                    
                        }
                        if (操作框移動後位置.X > 0)
                        {
                           // 操作方框移動_向左();
                            if (p0.X > 0 )
                            {
                                if (ladder_list[p0.Y][p0.X].ladderType != partTypeEnum.Data_no_Part)
                                {
                                    操作方框索引 = p0;
                                }
                            }                   
                            sub_檢查操作框位置是否超出範圍();
                       
                        }
                        if (操作框移動後位置.X < 0)
                        {

                            if (ladder_list[p0.Y][p0.X].ladderType != partTypeEnum.Data_no_Part)
                            {
                                操作方框索引 = p0;
                            }
                            sub_檢查操作框位置是否超出範圍();
                        }
                        if (操作框移動後位置.Y == 0)
                        {

                        }
                        if (操作框移動後位置.Y > 0)
                        {
                            if (T5_拖曳操作框上下捲動間隔)
                            {
                                操作方框移動_向上();
                                device_system.Set_Device("T5", 10);
                                T5_拖曳操作框上下捲動間隔 = false;
                            }
                        }
                        if (操作框移動後位置.Y < 0)
                        {
                            if (T5_拖曳操作框上下捲動間隔)
                            {
                                操作方框移動_向下();
                                device_system.Set_Device("T5", 10);
                                T5_拖曳操作框上下捲動間隔 = false;
                            }
                        }                
                    }
                }
                操作框窗選_位移量.X = -(操作框窗選_初始位.X - (操作方框索引.X));
                if (操作框窗選_初始位.X == 0 && (Math.Abs(操作框窗選_位移量.X) > 0 || Math.Abs(操作框窗選_位移量.Y) > 0) && 操作框窗選_位移量.X != 11) 操作框窗選_位移量.X = 11;
                操作框窗選_位移量.Y = -(操作框窗選_初始位.Y - (操作方框索引.Y + 顯示畫面列數索引));
                sub_窗選滑鼠拖曳位移量檢查();
            }
        }
        private void pictureBox_LADDER_MouseUp(object sender, MouseEventArgs e)
        {
            sub_鼠線窗選畫面寫入();
            FLAG_pictureBox_LADDER_MouseDown = false;
        }
        private void pictureBox_LADDER_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int num = e.Delta / 120;
            if (num > 0)
            {
                if (顯示畫面列數索引 > 0) 顯示畫面列數索引--;
        
            }
            if (num < 0)
            {
                if((顯示畫面列數索引)<ladder_list.Count - 1) 顯示畫面列數索引++;
                if (操作方框索引.Y > 0) 操作方框索引.Y--;
            }
            操作框窗選_位移量 = new Point();
        }
        private void pictureBox_LADDER_MouseHover(object sender, EventArgs e)
        {
            pictureBox_LADDER.Focus();
            //vScrollBar_picture_滾動條.Focus();
        }
        private void pictureBox_LADDER_DoubleClick(object sender, EventArgs e)
        {
            FLAG_picture_LADDER_MouseDoubleclick = true;
        }
        private void panel_LADDER_Leave(object sender, EventArgs e)
        {
            if (Keys.Key_ShiftKey)
            {
                this.Focus();
            }
        }
        private void contextMenuStrip_右鍵選單_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            Point p = new Point(操作方框索引.X, 操作方框索引.Y + 顯示畫面列數索引);
            if (item.Name == "編譯")
            {
                if (cnt_鍵盤按鈕檢查_F4 == 1) cnt_鍵盤按鈕檢查_F4 = 2;
            }
            else if (item.Name == "復原回未編譯")
            {
                ladder_list = List_LADDER_All_Copy(ladder_list_備份);
            }
            else if (item.Name == "復原")
            {
                sub_上一步();
            }
            else if (item.Name == "剪下")
            {
                sub_複製();
                sub_刪除();
            }
            else if (item.Name == "複製")
            {
                sub_複製();
            }
            else if (item.Name == "貼上")
            {
                sub_貼上();
            }
            else if (item.Name == "插入一列")
            {
                sub_插入一列(操作方框索引.Y + 顯示畫面列數索引, true, true);
            }
            else if (item.Name == "刪除一列")
            {
                sub_刪除一列(操作方框索引.Y + 顯示畫面列數索引, true);
            }
            else if (item.Name == "插入一行")
            {
                sub_插入一元件();
            }
            else if (item.Name == "繪製橫線")
            {
                sub_畫橫線(p);
            }
            else if (item.Name == "刪除橫線")
            {
                sub_刪除橫線(p);
            }
            else if (item.Name == "繪製豎線")
            {
                sub_畫豎線(p);
            }
            else if (item.Name == "刪除豎線")
            {
                sub_刪除豎線(p);
            }
        }
        private void treeView_程式分頁_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            String NodeName = e.Node.Text;
            string str_temp;
            int int_temp;
            string[] str_array = myConvert.分解分隔號字串(NodeName, ' ');
            if (str_array.Length == 2)
            {
                str_temp = str_array[0].Replace("<", "");
                str_temp = str_temp.Replace(">", "");
                foreach (string[] str_temp_array in TAB目錄)
                {
                    if (str_temp_array[0] == str_temp && str_temp_array[2] == str_array[1])
                    {
                        str_temp = str_temp_array[1];
                        if (Int32.TryParse(str_temp, out int_temp))
                        {
                            操作方框索引.Y = 0;
                            顯示畫面列數索引 = int_temp;
                        }
                        return;
                    }
                }    
            }

        }

        bool FLAG_vScrollBar_picture_Scroll = false;
        private void vScrollBar_picture_滾動條_Scroll(object sender, ScrollEventArgs e)
        {
            FLAG_vScrollBar_picture_Scroll = true;
            顯示畫面列數索引 = vScrollBar_picture_滾動條.Value;
        }
        private void vScrollBar_picture_滾動條_MouseEnter(object sender, EventArgs e)
        {
            FLAG_vScrollBar_picture_Scroll = true;
        }
        private void vScrollBar_picture_滾動條_MouseLeave(object sender, EventArgs e)
        {
            FLAG_vScrollBar_picture_Scroll = false;
        }
        private void exButton_Online_btnClick(object sender, EventArgs e)
        {
            if (FLAG_Online) FLAG_Online = false;
            else FLAG_Online = true;
        }


        bool FLAG_treeView_程式分頁_MouseDown = false;
        int Width_treeView_程式分頁_MouseDown = 0;
        int X_treeView_程式分頁_MouseDown = 0;
        private void treeView_程式分頁_MouseMove(object sender, MouseEventArgs e)
        {
            if (FLAG_treeView_程式分頁_MouseDown)
            {
                this.treeView_程式分頁.Cursor = System.Windows.Forms.Cursors.SizeWE;
                if (FLAG_treeView_程式分頁_MouseDown)
                {
                    panel_程式分頁.Width = e.X - X_treeView_程式分頁_MouseDown + Width_treeView_程式分頁_MouseDown;
                }
            }
            else
            {
                this.treeView_程式分頁.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
        }
        private void treeView_程式分頁_MouseLeave(object sender, EventArgs e)
        {
            this.treeView_程式分頁.Cursor = System.Windows.Forms.Cursors.Arrow;
            FLAG_treeView_程式分頁_MouseDown = false;
            //if (FLAG_treeView_程式分頁_MouseDown)
            //{
            //    panel_程式分頁.Width = MousePosition.X + 10;
           // }
            // FLAG_treeView_程式分頁_MouseDown = false;
        }
        private void treeView_程式分頁_MouseDown(object sender, MouseEventArgs e)
        {
            this.treeView_程式分頁.Cursor = System.Windows.Forms.Cursors.Arrow;
            FLAG_treeView_程式分頁_MouseDown = false;
        }
        private void treeView_程式分頁_MouseUp(object sender, MouseEventArgs e)
        {

        }
        private void 調整大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.treeView_程式分頁.Cursor = System.Windows.Forms.Cursors.SizeWE;
            Width_treeView_程式分頁_MouseDown = panel_程式分頁.Width;
            X_treeView_程式分頁_MouseDown = MousePosition.X;
            FLAG_treeView_程式分頁_MouseDown = true;
        }

        private void 取消ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.treeView_程式分頁.Cursor = System.Windows.Forms.Cursors.Arrow;
            FLAG_treeView_程式分頁_MouseDown = false;
        }

        #region ToolStripMenuItem
        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(cnt_TransferSetup視窗進入檢查==255)
            {
                cnt_TransferSetup視窗進入檢查 = 1;
            }
        }
        private void 上傳ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cnt_Upload視窗進入檢查 == 255)
            {
                cnt_Upload視窗進入檢查 = 1;
            }
        }
        private void 下載ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(cnt_Download視窗進入檢查==255)
            {
                cnt_Download視窗進入檢查 = 1;
            }
        }
        private void 新專案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cnt_ExButton程式_開新專案 == 2) cnt_ExButton程式_開新專案 = 3;
        }
        private void 讀取ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cnt_ExButton程式_讀取檔案 == 1) cnt_ExButton程式_讀取檔案 = 2; 
        }
        private void 儲存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cnt_ExButton程式_儲存檔案 == 2) cnt_ExButton程式_儲存檔案 = 3; 
        }
        private void 關閉ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_作用中表單.Close();
        }
        private void commentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (註解顯示ToolStripMenuItem.Checked)
            {
                註解不顯示 = true;
                註解顯示ToolStripMenuItem.Checked = false;
            }
            else
            {
                註解不顯示 = false;
                註解顯示ToolStripMenuItem.Checked = true;
            }
            if (註解不顯示) 一個畫面列數 = 15;
            else 一個畫面列數 = 8;
            graphics_init = false;
            操作方框大小.Width = (int)(panel_LADDER.Width / (float)一列格數);
            操作方框大小.Height = (int)(panel_LADDER.Height / (float)一個畫面列數);
        }
        private void 註解顏色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog.Color = 註解文字顏色;
            if ( colorDialog.ShowDialog() != DialogResult.Cancel)
            {
                註解文字顏色 = colorDialog.Color;  // 回傳選擇顏色，並且設定 Textbox 的背景顏色
            }
           
        }
        private void 註解字體ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog.Font = 註解字體;
            if (fontDialog.ShowDialog() != DialogResult.Cancel)
            {
                註解字體 = fontDialog.Font;
            }
        }
        private void toolStripTextBox_註解字母數_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8) // 8 > BackSpace
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void toolStripMenuItem_註解字母數_確認_Click(object sender, EventArgs e)
        {
            int value = 0;
            if (Int32.TryParse(toolStripTextBox_註解字母數.Text, out value))
            {
                if (value < 6) value = 6;
                if (value > 20) value = 20;
                註解一列半形字母數 = value;
                toolStripTextBox_註解字母數.Text = value.ToString();
            }
        }
        private void toolStripTextBox_註解列數_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8) // 8 > BackSpace
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void toolStripMenuItem_註解列數_確認_Click(object sender, EventArgs e)
        {
            int value = 0;
            if (Int32.TryParse(toolStripTextBox_註解列數.Text, out value))
            {
                if (value < 2) value = 2;
                if (value > 6) value = 6;
                註解列數 = value;
                toolStripTextBox_註解列數.Text = value.ToString();
            }
        }
        private void oNLINE字體ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog.Font = Online字體;
            if (fontDialog.ShowDialog() != DialogResult.Cancel)
            {
                Online字體 = fontDialog.Font;
            }
        }
        private void toolStripMenuItem視窗字體_Click(object sender, EventArgs e)
        {
            fontDialog.Font = 視窗字體;
            if (fontDialog.ShowDialog() != DialogResult.Cancel)
            {
                視窗字體 = fontDialog.Font;
            }
        }
        private void 顯示_預設值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("還原回預設值?", "Asterisk", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            if (Result == DialogResult.Yes)
            {
                sub_顯示還原回預設值();
            }  
        }
        private void 程式比較ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cnt_Verify視窗進入檢查 == 255) cnt_Verify視窗進入檢查 = 1;
        }
        private void 顯示註解列表toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (顯示註解列表toolStripMenuItem.Checked)
            {
                panel_Datagrid_註解列表.Size = new Size(0, panel_Datagrid_註解列表.Size.Height);
                顯示註解列表toolStripMenuItem.Checked = false;
            }
            else
            {
                panel_Datagrid_註解列表.Size = new Size(400, panel_Datagrid_註解列表.Size.Height);
                顯示註解列表toolStripMenuItem.Checked = true;
            }
        }
        private void toolStripMenuItem顯示目錄_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItem顯示目錄.Checked)
            {
                panel_程式分頁.Size = new Size(0, panel_Datagrid_註解列表.Size.Height);
                toolStripMenuItem顯示目錄.Checked = false;
            }
            else
            {
                panel_程式分頁.Size = new Size(220, panel_Datagrid_註解列表.Size.Height);
                toolStripMenuItem顯示目錄.Checked = true;
            }
        }
        #endregion
        #endregion
        #endregion
        #region DataGridView_註解列表
        private DataTable dt;
        private DataTable dt_buf;
        private String str_首列_Device = "";
        private String[] Array_查詢Device = new string[1000];
        private int[] Array_接點數量 = new int[1000];
        private int[] Array_輸出數量 = new int[1000];
        private String[] Array_Comment = new string[1000];
        private int Device_範圍上限 = 0;
        private int Device_範圍下限 = 0;

        byte cnt_DataTable_初始化 = 1;
        byte cnt_DataGridView_註解列表 = 255;
        byte cnt_註解列表更新至Device= 255;
 

        void sub_DataGridView_註解列表()
        {
            if (cnt_DataTable_初始化 == 1) cnt_DataGridView_初始化_開始初始化(ref cnt_DataTable_初始化);
            if (cnt_DataTable_初始化 == 2) cnt_DataTable_初始化 = 255;

            if (cnt_DataTable_初始化 ==255)
            {
                if (cnt_註解列表更新至Device == 255)
                {
                    if (cnt_DataGridView_註解列表 == 1) cnt_DataGridView_註解列表_初始化(ref cnt_DataGridView_註解列表);
                    if (cnt_DataGridView_註解列表 == 2) cnt_DataGridView_註解列表_檢查輸入數值是否OK(ref cnt_DataGridView_註解列表);
                    if (cnt_DataGridView_註解列表 == 3) cnt_DataGridView_註解列表_取得首列Device參數(ref cnt_DataGridView_註解列表);
                    if (cnt_DataGridView_註解列表 == 4) cnt_DataGridView_註解列表_取得註解及搜索範圍(ref cnt_DataGridView_註解列表);
                    if (cnt_DataGridView_註解列表 == 5) cnt_DataGridView_註解列表_取得接點及輸出數量(ref cnt_DataGridView_註解列表);
                    if (cnt_DataGridView_註解列表 == 6) cnt_DataGridView_註解列表_更新列表(ref cnt_DataGridView_註解列表);
                    if (cnt_DataGridView_註解列表 == 7) cnt_DataGridView_註解列表_改變DataGridView外觀(ref cnt_DataGridView_註解列表);
                    if (cnt_DataGridView_註解列表 == 8) cnt_DataGridView_註解列表 = 255;
                    if (cnt_DataGridView_註解列表 == 200) cnt_DataGridView_註解列表_200_彈出錯誤視窗(ref cnt_DataGridView_註解列表);
                    if (cnt_DataGridView_註解列表 == 201) cnt_DataGridView_註解列表 = 255;
                    if (cnt_DataGridView_註解列表 == 255)
                    {
                        for (int Y = 0; Y < dataGridView_註解列表.Rows.Count; Y++)
                        {
                            for (int i = 0; i < dataGridView_註解列表.Rows[Y].Cells.Count; i++)
                            {
                                if (i != 4) dataGridView_註解列表.Rows[Y].Cells[i].Selected = false;
                            }
                        }
                    }
                }
                else cnt_DataGridView_註解列表 = 255;

        
            }         
        }
        #region cnt_DataTable_初始化
        void cnt_DataGridView_初始化_開始初始化(ref byte cnt)
        {
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("Device"));
            dt.Columns.Add(new DataColumn("-| |-"));
            dt.Columns.Add(new DataColumn("-( )-"));
            dt.Columns.Add(new DataColumn("Count"));
            dt.Columns.Add(new DataColumn("Comment"));
            for (int i = 0; i < 1000;i++ )
            {
                dt.Rows.Add();
            }
            CallBackUI.datagrid.填入DataTable(dt, dataGridView_註解列表);
            cnt_DataGridView_註解列表 = 1;
            cnt++;
        }
        #endregion
        #region cnt_DataGridView_註解列表
        void cnt_DataGridView_註解列表_初始化(ref byte cnt)
        {
    
            dt_buf = new DataTable();
            str_首列_Device = "";
            dt_buf.Columns.Add(new DataColumn("Device"));
            dt_buf.Columns.Add(new DataColumn("-| |-"));
            dt_buf.Columns.Add(new DataColumn("-( )-"));
            dt_buf.Columns.Add(new DataColumn("Count"));
            dt_buf.Columns.Add(new DataColumn("Comment"));

            Array_查詢Device = new string[1000];
            Array_接點數量 = new int[1000];
            Array_輸出數量 = new int[1000];
            Array_Comment = new string[1000];
            Device_範圍上限 = 0;
            Device_範圍下限 = 0;
            cnt++;
        }
        void cnt_DataGridView_註解列表_檢查輸入數值是否OK(ref byte cnt)
        {
            String str = "";
            object str_temp;
            CallBackUI.textbox.取得字串(ref str, textBox_註解查詢_Device_name);
            if (device.Get_Device(str, out str_temp))
            {
                cnt++;
            }
            else
            {
                cnt = 200;
            }
        }
        void cnt_DataGridView_註解列表_取得首列Device參數(ref byte cnt)
        {
            CallBackUI.textbox.取得字串(ref str_首列_Device, textBox_註解查詢_Device_name);
            str_首列_Device = str_首列_Device.ToUpper();
            cnt++;
        }
        void cnt_DataGridView_註解列表_取得註解及搜索範圍(ref byte cnt)
        {

            String str_Device_類型 = str_首列_Device.Substring(0, 1);
            String str_Device_編號 = str_首列_Device.Substring(1, str_首列_Device.Length - 1);
            int int_Device_編號 = 0;
            if (!Int32.TryParse(str_Device_編號, out int_Device_編號))
            {
                cnt = 200;
                return;
            }

            for (int i = 0; i < 1000; i++)
            {
                if (i == 0) Device_範圍下限 = int_Device_編號;
                if (i == 999) Device_範圍上限 = int_Device_編號 + i;
                String str_註解文字 = "";
                Array_查詢Device[i] = str_Device_類型 + (int_Device_編號 + i).ToString();
                object comment = new object();
                if (device.Get_Device(Array_查詢Device[i], 0, out comment))
                {
                    str_註解文字 = (String)comment;
                    Array_Comment[i] = str_註解文字;
                }
                else
                {
                    cnt = 200;
                    return;
                }

            }
            cnt++;
        }
        void cnt_DataGridView_註解列表_取得接點及輸出數量(ref byte cnt)
        {
            foreach (LADDER[] ladder_array in ladder_list)
            {
                foreach (LADDER ladder_temp in ladder_array)
                {
                    for (int i = 0; i < ladder_temp.元素數量; i++)
                    {
                        String Device = ladder_temp.ladderParam[i];
                        if (Device != "" && Device != null && Device.Length > 1)
                        {
                            String str_搜索Device類型 = str_首列_Device.Substring(0, 1);
                            String str_Device_類型 = Device.Substring(0, 1);
                            String str_Device_編號 = Device.Substring(1, Device.Length - 1);
                            if (str_Device_類型 == str_搜索Device類型)
                            {
                                if (str_Device_類型 == "X" || str_Device_類型 == "Y" || str_Device_類型 == "S" || str_Device_類型 == "M" || str_Device_類型 == "D" || str_Device_類型 == "F" || str_Device_類型 == "R" || str_搜索Device類型 == "T" || str_搜索Device類型 == "Z")
                                {
                                    int int_Device_編號 = 0;
                                    if (!Int32.TryParse(str_Device_編號, out int_Device_編號))
                                    {
                                        continue;
                                    }
                                    if (str_搜索Device類型 == "X" || str_搜索Device類型 == "Y" || str_搜索Device類型 == "T" || str_搜索Device類型 == "M" || str_搜索Device類型 == "S")
                                    {
                                        bool FLAG_要增加數量 = false;
                                        if (ladder_temp.ladderType == partTypeEnum.A_Part) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.B_Part) FLAG_要增加數量 = true;
                                        if (FLAG_要增加數量)
                                        {
                                            if (int_Device_編號 >= Device_範圍下限 && int_Device_編號 <= Device_範圍上限)
                                            {
                                                int index = int_Device_編號 - Device_範圍下限;
                                                Array_接點數量[index]++;

                                            }
                                        }
                                    }
                                    if (str_搜索Device類型 == "Y" || str_搜索Device類型 == "T" || str_搜索Device類型 == "M" || str_搜索Device類型 == "S")
                                    {
                                        bool FLAG_要增加數量 = false;
                                        if (ladder_temp.ladderType == partTypeEnum.OUT_Part) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.OUT_TIMER_PART) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.DRVA_PART) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.DRVI_PART) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.PLSV_PART) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.SET_PART) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.RST_PART) FLAG_要增加數量 = true;
                                        if (FLAG_要增加數量)
                                        {
                                            if (int_Device_編號 >= Device_範圍下限 && int_Device_編號 <= Device_範圍上限)
                                            {
                                                int index = int_Device_編號 - Device_範圍下限;
                                                Array_輸出數量[index]++;

                                            }
                                        }
                                    }
                                    if (str_搜索Device類型 == "D" || str_搜索Device類型 == "F" || str_搜索Device類型 == "R" || str_搜索Device類型 == "Z")
                                    {
                                        bool FLAG_要增加數量 = false;
                                        if (ladder_temp.ladderType == partTypeEnum.LD_Equal_Part) FLAG_要增加數量 = true;
                                        if (FLAG_要增加數量)
                                        {
                                            if (int_Device_編號 >= Device_範圍下限 && int_Device_編號 <= Device_範圍上限)
                                            {
                                                int index = int_Device_編號 - Device_範圍下限;
                                                Array_接點數量[index]++;
                                            }
                                        }
                                    }
                                    if (str_搜索Device類型 == "D" || str_搜索Device類型 == "F" || str_搜索Device類型 == "R" || str_搜索Device類型 == "Z")
                                    {
                                        bool FLAG_要增加數量 = false;
                                        if (ladder_temp.ladderType == partTypeEnum.MOV_Part) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.ADD_PART) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.SUB_PART) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.MUL_PART) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.DIV_PART) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.INC_PART) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.DRVA_PART) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.DRVI_PART) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.PLSV_PART) FLAG_要增加數量 = true;
                                        if (ladder_temp.ladderType == partTypeEnum.OUT_TIMER_PART) FLAG_要增加數量 = true;
                                        if (FLAG_要增加數量)
                                        {
                                            if (int_Device_編號 >= Device_範圍下限 && int_Device_編號 <= Device_範圍上限)
                                            {
                                                int index = int_Device_編號 - Device_範圍下限;
                                                Array_輸出數量[index]++;
                                            }
                                        }
                                    }
                                    if (ladder_temp.ladderType == partTypeEnum.WTB_PART)
                                    {

                                        if (str_搜索Device類型 == "D" || str_搜索Device類型 == "F" || str_搜索Device類型 == "R" || str_搜索Device類型 == "Z")
                                        {
                                            if (int_Device_編號 >= Device_範圍下限 && int_Device_編號 <= Device_範圍上限)
                                            {
                                                int index = int_Device_編號 - Device_範圍下限;
                                                Array_接點數量[index]++;
                                            }
                                        }
                                        if (str_搜索Device類型 == "Y" || str_搜索Device類型 == "T" || str_搜索Device類型 == "M" || str_搜索Device類型 == "S")
                                        {
                                            int num = Convert.ToInt32(ladder_temp.ladderParam[2].Substring(1));
                                            for(int k = 0 ; k < num ; k ++)
                                            {
                                                if (int_Device_編號 + k >= Device_範圍下限 && int_Device_編號 + k <= Device_範圍上限)
                                                {
                                                    int index = int_Device_編號 - Device_範圍下限 + k;
                                                    Array_輸出數量[index]++;
                                                }
                                            }

                                        }
                                    }
                                    if (ladder_temp.ladderType == partTypeEnum.BTW_PART)
                                    {

                                        if (str_搜索Device類型 == "D" || str_搜索Device類型 == "F" || str_搜索Device類型 == "R" || str_搜索Device類型 == "Z")
                                        {
                                            if (int_Device_編號 >= Device_範圍下限 && int_Device_編號 <= Device_範圍上限)
                                            {
                                                int index = int_Device_編號 - Device_範圍下限;
                                                Array_輸出數量[index]++;
                                            }
                                        }
                                        if (str_搜索Device類型 == "Y" || str_搜索Device類型 == "T" || str_搜索Device類型 == "M" || str_搜索Device類型 == "S")
                                        {
                                            int num = Convert.ToInt32(ladder_temp.ladderParam[2].Substring(1));
                                            for (int k = 0; k < num; k++)
                                            {
                                                if (int_Device_編號 + k >= Device_範圍下限 && int_Device_編號 + k <= Device_範圍上限)
                                                {
                                                    int index = int_Device_編號 - Device_範圍下限 + k;
                                                    Array_接點數量[index]++;
                                                }
                                            }

                                        }
                                    } 
                                    if (ladder_temp.ladderType == partTypeEnum.ZRST_PART)
                                    {
                                        /*if(i == 1)
                                        {
                                            int num0 = Convert.ToInt32(ladder_temp.ladderParam[1].Substring(1));
                                            int num1 = Convert.ToInt32(ladder_temp.ladderParam[2].Substring(1));
                                            for (int k = 0; k <= (num1 - num0); k++)
                                            {
                                                if (int_Device_編號 + k >= Device_範圍下限 && int_Device_編號 + k <= Device_範圍上限)
                                                {
                                                    int index = int_Device_編號 - Device_範圍下限 + k;
                                                    Array_接點數量[index]++;
                                                }
                                            } 
                                        }*/
                                                                     
                                    }
                                }
                            }
                        }
                    }
                }
            }
            cnt++;
        }
        void cnt_DataGridView_註解列表_更新列表(ref byte cnt)
        {
            for (int i = 0; i < 1000; i++)
            {
                String 接點符號 = "";
                String 輸出符號 = "";
                String 輸出數量 = Array_輸出數量[i].ToString();
                if (Array_輸出數量[i] == 0) 輸出數量 = "";
                if (Array_接點數量[i] > 0) 接點符號 = "*";
                if (Array_輸出數量[i] > 0) 輸出符號 = "*";
                dt_buf.Rows.Add(new object[] { Array_查詢Device[i], 接點符號, 輸出符號, 輸出數量, Array_Comment[i] });
            }
            for (int Y = 0; Y < dt_buf.Rows.Count; Y++)
            {
                for (int X = 0; X < dt_buf.Columns.Count; X++)
                {
                    dt.Rows[Y][X] = dt_buf.Rows[Y][X];
                }
            } 
            cnt++;
        }
        void cnt_DataGridView_註解列表_改變DataGridView外觀(ref byte cnt)
        {
            for (int i = 0; i < dataGridView_註解列表.Rows.Count; i++)
            {
                dataGridView_註解列表.Rows[i].Cells[0].ReadOnly = true;
                dataGridView_註解列表.Rows[i].Cells[1].ReadOnly = true;
                dataGridView_註解列表.Rows[i].Cells[2].ReadOnly = true;
                dataGridView_註解列表.Rows[i].Cells[3].ReadOnly = true;
                dataGridView_註解列表.Rows[i].Cells[4].ReadOnly = false;
                dataGridView_註解列表.Rows[i].Height = 20;
                if (dataGridView_註解列表.Rows[i].Cells[4].Value.ToString() == "#None") dataGridView_註解列表.Rows[i].Visible = false;
                else dataGridView_註解列表.Rows[i].Visible = true;
            }

            dataGridView_註解列表.Columns[0].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridView_註解列表.Columns[0].Width = 50;
            dataGridView_註解列表.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridView_註解列表.Columns[1].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridView_註解列表.Columns[1].Width = 30;
            dataGridView_註解列表.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView_註解列表.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView_註解列表.Columns[2].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridView_註解列表.Columns[2].Width = 30;
            dataGridView_註解列表.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView_註解列表.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView_註解列表.Columns[3].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridView_註解列表.Columns[3].Width = 50;

            dataGridView_註解列表.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView_註解列表.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView_註解列表.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView_註解列表.FirstDisplayedScrollingRowIndex = 0;
            cnt++;
        }
        void cnt_DataGridView_註解列表_200_彈出錯誤視窗(ref byte cnt)
        {
            MessageBox.Show("查詢失敗!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            cnt++;
        }
        void cnt_DataGridView_註解列表_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
        #region cnt_註解列表更新至Device
        void sub_註解列表更新至Device()
        {
            if (cnt_DataGridView_註解列表 == 255)
            {
                if (cnt_註解列表更新至Device == 255) cnt_註解列表更新至Device = 1;
                if (cnt_註解列表更新至Device == 1) cnt_註解列表更新至Device_初始化(ref cnt_註解列表更新至Device);
                if (cnt_註解列表更新至Device == 2) cnt_註解列表更新至Device_檢查可更新(ref cnt_註解列表更新至Device);
                if (cnt_註解列表更新至Device == 3) cnt_註解列表更新至Device_開始檢查(ref cnt_註解列表更新至Device);
                if (cnt_註解列表更新至Device == 4) cnt_註解列表更新至Device_開始更新(ref cnt_註解列表更新至Device);
                if (cnt_註解列表更新至Device == 5) cnt_註解列表更新至Device = 255;
            }
            else cnt_註解列表更新至Device = 255;
        }
        List<string[]> List_更新Device及內容 = new List<string[]>();
        void cnt_註解列表更新至Device_初始化(ref byte cnt)
        {
            List_更新Device及內容 = new List<string[]>();
            cnt++;
        }
        void cnt_註解列表更新至Device_檢查可更新(ref byte cnt)
        {
            if (dt.Rows.Count == dt_buf.Rows.Count) cnt++;
            else cnt = 255;
        }
        void cnt_註解列表更新至Device_開始檢查(ref byte cnt)
        {
            for (int Y = 0; Y < dt.Rows.Count; Y++)
            {
                if (dt.Rows[Y][4].ToString() != dt_buf.Rows[Y][4].ToString())
                {
                    string[] str_temp = new string[2];
                    str_temp[0] = dt.Rows[Y][0].ToString();
                    str_temp[1] = dt.Rows[Y][4].ToString();
                    List_更新Device及內容.Add(str_temp);
                    dt_buf.Rows[Y][4] = dt.Rows[Y][4];
                }              
            }
            cnt++;
        }
        void cnt_註解列表更新至Device_開始更新(ref byte cnt)
        {
            for(int i = 0 ; i < List_更新Device及內容.Count;i++)
            {
                string str_device = List_更新Device及內容[i][0];
                string str_cmomment = List_更新Device及內容[i][1];
                device.Set_Device(str_device, str_cmomment);
            }
            cnt++;
        }
        void cnt_註解列表更新至Device_(ref byte cnt)
        {

        }
        #endregion
        private void textBox_註解查詢_Device_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) if (cnt_DataGridView_註解列表 == 255) cnt_DataGridView_註解列表 = 1;
        }
        string str_dataGridView_註解列表_剪貼簿;
        void dataGridView_註解列表_複製()
        {
            DataObject dataobject = dataGridView_註解列表.GetClipboardContent();
            if (dataobject != null)
            {
                Clipboard.SetDataObject(dataobject);
            }
        }
        void dataGridView_註解列表_貼上()
        {
            str_dataGridView_註解列表_剪貼簿 = Clipboard.GetText();
            string temp = str_dataGridView_註解列表_剪貼簿;
            if (dt.Rows.Count > 0 && (temp != null && temp != ""))
            {
                int 選擇的列 = 0;
                int 選擇列數 = 0;
                for (int i = 0; i < dataGridView_註解列表.RowCount; i++)
                {
                    if (dataGridView_註解列表.Rows[i].Cells[4].Selected)
                    {
                        選擇的列 = i;
                        選擇列數++;
                        break;
                    }
                }
                if (選擇列數 > 0)
                {
                    List<string> List_貼上內容 = new List<string>();
                    string str_搜尋字串內容 = "\r\n";
                    int int_搜尋到字串起點 = 0;
                    int int_上次搜索到的位置 = 0;
                    temp = temp + str_搜尋字串內容;
                    while (true)
                    {
                        string str_temp = "";
                        if (int_搜尋到字串起點 > temp.Length) break;
                        int_搜尋到字串起點 = temp.IndexOf(str_搜尋字串內容, int_搜尋到字串起點);

                        if (int_搜尋到字串起點 == -1) break;

                        str_temp = temp.Substring(int_上次搜索到的位置, int_搜尋到字串起點 - int_上次搜索到的位置);
                        str_temp = str_temp.Replace(str_搜尋字串內容, "");
                        int_上次搜索到的位置 = int_搜尋到字串起點;
                        List_貼上內容.Add(str_temp);

                        int_搜尋到字串起點 += str_搜尋字串內容.Length;

                    }
                    string device_類型 = dt.Rows[選擇的列][0].ToString().Substring(0, 1);
                    string device_編號 = dt.Rows[選擇的列][0].ToString().Substring(1, dt.Rows[選擇的列][0].ToString().Length - 1);

                    int 貼上內容列數 = 0;
                    for (int i = 選擇的列; i < 1000; i++)
                    {
                        int 被貼上列數 = i;
                        bool 此列合法 = true;
                        if (device_類型 == "X" || device_類型 == "Y")
                        {
                            此列合法 = myConvert.檢查八進位合法(被貼上列數);
                        }

                        if (被貼上列數 < dt.Rows.Count && 貼上內容列數 < List_貼上內容.Count && 此列合法)
                        {
                            dt.Rows[被貼上列數][4] = List_貼上內容[貼上內容列數];
                            貼上內容列數++;
                        }

                    }
                }

            }
        }
        void dataGridView_註解列表_刪除()
        {
            for (int i = 0; i < dataGridView_註解列表.RowCount; i++)
            {
                if (dataGridView_註解列表.Rows[i].Cells[4].Selected) dataGridView_註解列表.Rows[i].Cells[4].Value = "";
            }
        }
        void dataGridView_註解列表_剪下()
        {
            dataGridView_註解列表_複製();
            dataGridView_註解列表_刪除();
        }
        #region 控件事件
        private void dataGridView_註解列表_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (Keys.Key_ControlKey && Keys.Key_C)
            {
                dataGridView_註解列表_複製();
            }
            if (Keys.Key_ControlKey && Keys.Key_V)
            {
                dataGridView_註解列表_貼上();
            }
            if (Keys.Key_ControlKey && Keys.Key_X)
            {
                dataGridView_註解列表_剪下();
            }
            if (Keys.Key_Delete)
            {
                dataGridView_註解列表_刪除();
            }
            if (Keys.Key_ControlKey && Keys.Key_A)
            {
                dataGridView_註解列表.SelectAll();
            }
        }
        private void dataGridView_註解列表_Enter(object sender, EventArgs e)
        {
            if (cnt_DataGridView_註解列表 == 255) cnt_DataGridView_註解列表 = 1;

        }
        private void exButton_註解查詢_btnClick(object sender, EventArgs e)
        {
            if (cnt_DataGridView_註解列表 == 255) cnt_DataGridView_註解列表 = 1;
        }
        private void panel_Datagrid_註解列表_Click(object sender, EventArgs e)
        {
            panel_Datagrid_註解列表.Focus();           
        }
        private void contextMenuStrip_註解列表_右鑑選單_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            if (item.Name == "D_剪下")
            {
                dataGridView_註解列表_剪下();
            }
            else if (item.Name == "D_複製")
            {
                dataGridView_註解列表_複製();
            }
            else if (item.Name == "D_貼上")
            {
                dataGridView_註解列表_貼上();
            }
            else if (item.Name == "D_刪除")
            {
                dataGridView_註解列表_刪除();
            }
            else if (item.Name == "D_選取全部")
            {
                dataGridView_註解列表.SelectAll();
            }
        }
        #endregion
        #endregion
        private void timer_主執行序監控_Tick(object sender, EventArgs e)
        {
            if (!backgroundWorker_LADDER_主程式.IsBusy) backgroundWorker_LADDER_主程式.RunWorkerAsync();
            if (!backgroundWorker_畫面更新.IsBusy) backgroundWorker_畫面更新.RunWorkerAsync();
        //    if (!backgroundWorker_計時器.IsBusy) backgroundWorker_計時器.RunWorkerAsync();
            if (!backgroundWorker_Online讀取.IsBusy) backgroundWorker_Online讀取.RunWorkerAsync();
            
            if (FLAG_有功能鍵按下 || !panel_LADDER.ContainsFocus)
            {
                device_system.Set_Device("T8", 150);
                T8_指令輸入視窗致能 = false;
            }

            if (!(!Form_作用中表單.ContainsFocus || !panel_LADDER.ContainsFocus))
            {
                device_system.Set_Device("T7", 50);
                T7_編譯視窗未獲取焦點 = false;
            }
            if (T7_編譯視窗未獲取焦點) FLAG_編譯視窗獲取焦點 = false;
            else FLAG_編譯視窗獲取焦點 = true;

            if (Form_作用中表單.ContainsFocus) FLAG_主視窗獲取焦點 = true;
            else FLAG_主視窗獲取焦點 = false;

            sub_FindDevice視窗進入檢查();
            sub_ReplaceDevice視窗進入檢查();
            sub_TransferSetup視窗進入檢查();
            sub_Upload視窗進入檢查();
            sub_Download視窗進入檢查();
            sub_Verify視窗進入檢查();
            sub_DataGridView_註解列表();
            sub_LADDER編譯區顯示大小檢查();
            if (cnt_檢查輸入指令 == 1 && T8_指令輸入視窗致能) sub_EnterSymbol視窗進入檢查();
            else cnt_EnterSymbol視窗進入檢查 = 1;


            if (FLAG_複製到剪貼簿)
            {
                Clipboard.SetData(DataFormats.Serializable, ladder_list_Copy);
                FLAG_複製到剪貼簿 = false;
            }
            if (FLAG_取得剪貼簿)
            {
                ladder_list_Copy = (List<LADDER[]>)Clipboard.GetData(DataFormats.Serializable);
                FLAG_取得剪貼簿 = false;
            }

        }

    }


}
