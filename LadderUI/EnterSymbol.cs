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
    public partial class EnterSymbol : Form
    {
    
        private byte cnt_檢查輸入指令 = 255;
        private string str_輸入指令內容 = "";
        private int 註解輸入次數 = 0;
        private bool 按下輸入 = false;
        private List<String> str_temp_array = new List<String>();
        private static Keyboard Keys = new Keyboard();
        private MyConvert myConvert = new MyConvert();
        private static EnterSymbol enterSymbol;
        private static readonly object synRoot = new object();
        public static bool 視窗已建立;
        public static LADDER_Panel.LADDER LADDER_buf = new LADDER_Panel.LADDER();
        public static DEVICE device;
        public static string 起始視窗文字 = "";
        public static bool 註解模式 = false;
        public static bool Command_OK = false;
        public static CommadTypeEnum commandType = new CommadTypeEnum();
        public static String[] Str_指令內容;
        public static String[] cmommadval = new String[10];
        private int 輸入指令元素數量 = 0;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
           /* switch (keyData)
            {
                
            }*/
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public enum CommadTypeEnum
        {
            LD = 0, LDI, OR, ORI, OUT, LD_Equal, MOV, ADD, SUB, MUL, DIV, INC, DRVA, DRVI, PLSV, OUT_TIMER, SET, RST, ZRST, BMOV, WTB, BTW 
                ,TAB ,JUMP,REF

        }
        public EnterSymbol()
        {
            InitializeComponent();

        }
        public static EnterSymbol GetForm(Point P0)
        {

            lock (synRoot)
            {
                if (enterSymbol == null)
                {
                    enterSymbol = new EnterSymbol();
                }
                enterSymbol.StartPosition = FormStartPosition.Manual;
                enterSymbol.Location = P0;
                視窗已建立 = true;
                Command_OK = false;
            }

            return enterSymbol;
        }
        private void EnterSymbol_Load(object sender, EventArgs e)
        {
            if (!註解模式)
            {
                this.ImeMode = System.Windows.Forms.ImeMode.Off;
                textBox_指令輸入.Location = new Point(8, 11);
                textBox_指令輸入.Size = new System.Drawing.Size(291, 22);
                panel_devicename.Visible = false;
                enterSymbol.Text = "指令輸入";
                enterSymbol.textBox_指令輸入.Text = 起始視窗文字 + enterSymbol.textBox_指令輸入.Text;
                enterSymbol.textBox_指令輸入.Select(enterSymbol.textBox_指令輸入.Text.Length, 0);
            }
            else
            {
                this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
                textBox_指令輸入.Location = new Point(49, 11);
                textBox_指令輸入.Size = new System.Drawing.Size(250, 22);
                panel_devicename.Visible = true;
                enterSymbol.Text = "註解輸入";
            }
            Str_指令內容 = new string[LADDER_buf.元素數量];
            timer_程序執行.Enabled = true;
        }
        private void EnterSymbol_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer_程序執行.Dispose();
            視窗已建立 = false;
            enterSymbol = null;
        }
        private void EnterSymbol_FormClosed(object sender, FormClosedEventArgs e)
        {
            視窗已建立 = false;
            enterSymbol = null;
        }
        private void textBox_指令輸入_TextChanged(object sender, EventArgs e)
        {
            str_輸入指令內容 = textBox_指令輸入.Text;
        }
        private void button_指令輸入完成_Click(object sender, EventArgs e)
        {
            按下輸入 = true;
        }
        private void button_離開指令輸入_Click(object sender, EventArgs e)
        {
            視窗已建立 = false;
            enterSymbol.Close();
        }
        private int tmr_TextBox_可輸入 = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (enterSymbol != null)
            {
                if (enterSymbol.CanFocus) 視窗已建立 = true;
                else 視窗已建立 = false;
                if (Keys.Key_Esc)
                {
                    enterSymbol.Close();
                }
                sub_cnt_檢查輸入指令();
        
                tmr_TextBox_可輸入++;

              
            }
   
        }
        #region 檢查輸入指令
        byte cnt_初始化 = 1;
        void sub_cnt_檢查輸入指令()
        {
            if (cnt_初始化 == 1) cnt_初始化_00_檢查模式(ref cnt_初始化);
            if (cnt_初始化 == 10) cnt_初始化_10_指令寫入TEXTBOX(ref cnt_初始化);
            if (cnt_初始化 == 11) cnt_初始化 = 255;
            if (cnt_初始化 == 20) cnt_初始化_20_註解寫入TEXTBOX(ref cnt_初始化);
            if (cnt_初始化 == 21) cnt_初始化 = 255;
            if (cnt_初始化 == 150) { cnt_檢查輸入指令 = 150; cnt_初始化 = 255; }
            if (按下輸入)
            {
                cnt_檢查輸入指令 = 1;
                按下輸入 = false;
            }
            if (cnt_初始化 == 255)
            {
                if (cnt_檢查輸入指令 == 1) cnt_檢查輸入指令_00_檢查字串大小(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 2) cnt_檢查輸入指令_00_檢查模式(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 3) cnt_檢查輸入指令_00_字串拆分(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 4) cnt_檢查輸入指令_00_檢查指令類別(ref cnt_檢查輸入指令);


                if (cnt_檢查輸入指令 == 10) cnt_檢查輸入指令_10_檢查一般指令內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 11) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 20) cnt_檢查輸入指令_20_檢查特殊指令_LD_Equal_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 21) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 25) cnt_檢查輸入指令_25_檢查特殊指令_MOV_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 26) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 30) cnt_檢查輸入指令_30_檢查特殊指令_ADD_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 31) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 35) cnt_檢查輸入指令_35_檢查特殊指令_SUB_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 36) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 40) cnt_檢查輸入指令_40_檢查特殊指令_MUL_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 41) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 45) cnt_檢查輸入指令_45_檢查特殊指令_DIV_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 46) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 50) cnt_檢查輸入指令_50_檢查特殊指令_INC_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 51) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 55) cnt_檢查輸入指令_55_檢查特殊指令_DRVA_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 56) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 60) cnt_檢查輸入指令_60_檢查特殊指令_DRVI_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 61) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 65) cnt_檢查輸入指令_65_檢查特殊指令_PLSV_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 66) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 70) cnt_檢查輸入指令_70_檢查特殊指令_SET_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 71) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 75) cnt_檢查輸入指令_75_檢查特殊指令_RST_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 76) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 80) cnt_檢查輸入指令_80_檢查特殊指令_ZRST_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 81) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 85) cnt_檢查輸入指令_85_檢查特殊指令_BMOV_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 86) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 90) cnt_檢查輸入指令_90_檢查特殊指令_WTB_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 91) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 95) cnt_檢查輸入指令_95_檢查特殊指令_BTW_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 96) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 100) cnt_檢查輸入指令_100_檢查特殊指令_TAB_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 101) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 105) cnt_檢查輸入指令_105_檢查特殊指令_JUMP_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 106) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 110) cnt_檢查輸入指令_110_檢查特殊指令_REF_內容(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 111) cnt_檢查輸入指令 = 150;

                if (cnt_檢查輸入指令 == 140) cnt_檢查輸入指令_140_寫入註解(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 141) { cnt_檢查輸入指令 = 255; cnt_初始化 = 1; }

                if (cnt_檢查輸入指令 == 150) cnt_檢查輸入指令_150_指令輸入完成(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 151) cnt_檢查輸入指令 = 255;

                if (cnt_檢查輸入指令 == 200) cnt_檢查輸入指令_200_輸入失敗(ref cnt_檢查輸入指令);
                if (cnt_檢查輸入指令 == 201) cnt_檢查輸入指令 = 255;
            }

     
        }
        void cnt_初始化_00_檢查模式(ref byte cnt)
        {
            if (!註解模式) cnt = 10;
            else cnt = 20;
          
        }
        void cnt_初始化_10_指令寫入TEXTBOX(ref byte cnt)
        {
            cnt++;
        }
        void cnt_初始化_20_註解寫入TEXTBOX(ref byte cnt)
        {
            object comment = new object();
            if (註解輸入次數 < LADDER_buf.元素數量)
            {
                if (!device.Get_Device(LADDER_buf.ladderParam[註解輸入次數], 0, out comment))
                {
                    Str_指令內容[註解輸入次數] = "";
                    註解輸入次數++;
                    return;
                }
                else
                {
                    label_Devicename.Text = LADDER_buf.ladderParam[註解輸入次數];
                    textBox_指令輸入.Text = (String)comment;
                    textBox_指令輸入.Select(0, enterSymbol.textBox_指令輸入.Text.Length);
                    cnt++;
                }
              
            }
            else
            {
                cnt = 150;
            }
          
        }
        void cnt_檢查輸入指令_00_檢查字串大小(ref byte cnt)
        {
            if (LADDER_buf.ladderType == LADDER_Panel.partTypeEnum.leftParenthesis)
            {
                cnt = 200;
            }
            else if (LADDER_buf.ladderType == LADDER_Panel.partTypeEnum.rightParenthesis)
            {
                cnt = 200;
            }    
            else
            {
                str_temp_array.Clear();
                cnt++; 
            }
        }
        void cnt_檢查輸入指令_00_檢查模式(ref byte cnt)
        {
            if (!註解模式)
            {
                if (str_輸入指令內容.Length <= 0)
                {
                    cnt = 200;
                }             
                else cnt++;
            }
            if (註解模式) cnt = 140;
        }
        void cnt_檢查輸入指令_00_字串拆分(ref byte cnt)
        {
            string str_temp = str_輸入指令內容.Trim();
            str_temp = str_temp.ToUpper();
            bool 有空白區域 = true;
            while (有空白區域)
            {
                int i = 0 ;
                i = str_temp.IndexOf(" ");
                if (i == -1)
                {
                    if (str_temp.Length > 0)
                    {
                        str_temp_array.Add(str_temp);
                    }
                    有空白區域 = false;
                }
                else
                {
                    string str = str_temp.Substring(0, i);
                    str_temp_array.Add(str);
                    str_temp = str_temp.Remove(0, i);
                    str_temp = str_temp.Trim();
                }
            }
            cnt++;
        }
        void cnt_檢查輸入指令_00_檢查指令類別(ref byte cnt)
        {
        
            if (str_temp_array[0] == "LD") { commandType = CommadTypeEnum.LD; cnt = 10; 輸入指令元素數量 = 1; }
            else if (str_temp_array[0] == "LDI") { commandType = CommadTypeEnum.LDI; cnt = 10; 輸入指令元素數量 = 1; }
            else if (str_temp_array[0] == "OR") { commandType = CommadTypeEnum.OR; cnt = 10; 輸入指令元素數量 = 1; }
            else if (str_temp_array[0] == "ORI") { commandType = CommadTypeEnum.ORI; cnt = 10; 輸入指令元素數量 = 1; }
            else if (str_temp_array[0] == "OUT") { commandType = CommadTypeEnum.OUT; cnt = 10; 輸入指令元素數量 = 1; }
            else if (str_temp_array[0] == "LD=") { commandType = CommadTypeEnum.LD_Equal; cnt = 20; 輸入指令元素數量 = 3;}
            else if (str_temp_array[0] == "LD>=") { commandType = CommadTypeEnum.LD_Equal; cnt = 20; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "LD<=") { commandType = CommadTypeEnum.LD_Equal; cnt = 20; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "LD>") { commandType = CommadTypeEnum.LD_Equal; cnt = 20; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "LD<") { commandType = CommadTypeEnum.LD_Equal; cnt = 20; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "LD<>") { commandType = CommadTypeEnum.LD_Equal; cnt = 20; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "LDD=") { commandType = CommadTypeEnum.LD_Equal; cnt = 20; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "LDD>=") { commandType = CommadTypeEnum.LD_Equal; cnt = 20; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "LDD<=") { commandType = CommadTypeEnum.LD_Equal; cnt = 20; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "LDD>") { commandType = CommadTypeEnum.LD_Equal; cnt = 20; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "LDD<") { commandType = CommadTypeEnum.LD_Equal; cnt = 20; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "LDD<>") { commandType = CommadTypeEnum.LD_Equal; cnt = 20; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "MOV") { commandType = CommadTypeEnum.MOV; cnt = 25; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "MOVP") { commandType = CommadTypeEnum.MOV; cnt = 25; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "DMOV") { commandType = CommadTypeEnum.MOV; cnt = 25; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "DMOVP") { commandType = CommadTypeEnum.MOV; cnt = 25; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "ADD") { commandType = CommadTypeEnum.ADD; cnt = 30; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "ADDP") { commandType = CommadTypeEnum.ADD; cnt = 30; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "DADD") { commandType = CommadTypeEnum.ADD; cnt = 30; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "DADDP") { commandType = CommadTypeEnum.ADD; cnt = 30; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "SUB") { commandType = CommadTypeEnum.SUB; cnt = 35; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "SUBP") { commandType = CommadTypeEnum.SUB; cnt = 35; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "DSUB") { commandType = CommadTypeEnum.SUB; cnt = 35; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "DSUBP") { commandType = CommadTypeEnum.SUB; cnt = 35; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "MUL") { commandType = CommadTypeEnum.MUL; cnt = 40; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "MULP") { commandType = CommadTypeEnum.MUL; cnt = 40; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "DMUL") { commandType = CommadTypeEnum.MUL; cnt = 40; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "DMULP") { commandType = CommadTypeEnum.MUL; cnt = 40; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "DIV") { commandType = CommadTypeEnum.DIV; cnt = 45; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "DIVP") { commandType = CommadTypeEnum.DIV; cnt = 45; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "DDIV") { commandType = CommadTypeEnum.DIV; cnt = 45; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "DDIVP") { commandType = CommadTypeEnum.DIV; cnt = 45; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "INC") { commandType = CommadTypeEnum.INC; cnt = 50; 輸入指令元素數量 = 2; }
            else if (str_temp_array[0] == "INCP") { commandType = CommadTypeEnum.INC; cnt = 50; 輸入指令元素數量 = 2; }
            else if (str_temp_array[0] == "DINC") { commandType = CommadTypeEnum.INC; cnt = 50; 輸入指令元素數量 = 2; }
            else if (str_temp_array[0] == "DINCP") { commandType = CommadTypeEnum.INC; cnt = 50; 輸入指令元素數量 = 2; }
            else if (str_temp_array[0] == "DRVA") { commandType = CommadTypeEnum.DRVA; cnt = 55; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "DDRVA") { commandType = CommadTypeEnum.DRVA; cnt = 55; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "DRVI") { commandType = CommadTypeEnum.DRVI; cnt = 60; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "DDRVI") { commandType = CommadTypeEnum.DRVI; cnt = 60; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "PLSV") { commandType = CommadTypeEnum.PLSV; cnt = 65; 輸入指令元素數量 = 2; }
            else if (str_temp_array[0] == "DPLSV") { commandType = CommadTypeEnum.PLSV; cnt = 65; 輸入指令元素數量 = 2; }
            else if (str_temp_array[0] == "SET") { commandType = CommadTypeEnum.SET; cnt = 70; 輸入指令元素數量 = 2; }
            else if (str_temp_array[0] == "RST") { commandType = CommadTypeEnum.RST; cnt = 75; 輸入指令元素數量 = 2; }
            else if (str_temp_array[0] == "ZRST") { commandType = CommadTypeEnum.ZRST; cnt = 80; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "ZRSTP") { commandType = CommadTypeEnum.ZRST; cnt = 80; 輸入指令元素數量 = 3; }
            else if (str_temp_array[0] == "BMOV") { commandType = CommadTypeEnum.BMOV; cnt = 85; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "BMOVP") { commandType = CommadTypeEnum.BMOV; cnt = 85; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "WTB") { commandType = CommadTypeEnum.WTB; cnt = 90; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "WTBP") { commandType = CommadTypeEnum.WTB; cnt = 90; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "DWTB") { commandType = CommadTypeEnum.WTB; cnt = 90; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "DWTBP") { commandType = CommadTypeEnum.WTB; cnt = 90; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "BTW") { commandType = CommadTypeEnum.BTW; cnt = 95; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "BTWP") { commandType = CommadTypeEnum.BTW; cnt = 95; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "DBTW") { commandType = CommadTypeEnum.BTW; cnt = 95; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "DBTWP") { commandType = CommadTypeEnum.BTW; cnt = 95; 輸入指令元素數量 = 4; }
            else if (str_temp_array[0] == "TAB") { commandType = CommadTypeEnum.TAB; cnt = 100; 輸入指令元素數量 = 10; }
            else if (str_temp_array[0] == "JUMP") { commandType = CommadTypeEnum.JUMP; cnt = 105; 輸入指令元素數量 = 2; }
            else if (str_temp_array[0] == "REF") { commandType = CommadTypeEnum.REF; cnt = 110; 輸入指令元素數量 = 3; }    

            else
            {
                cnt = 200;
                return;
            }
            bool 要判斷指令長度 = false;
            if (commandType == CommadTypeEnum.LD) 要判斷指令長度 = true;
            if (commandType == CommadTypeEnum.LDI) 要判斷指令長度 = true;
            if (commandType == CommadTypeEnum.OR) 要判斷指令長度 = true;
            if (commandType == CommadTypeEnum.ORI) 要判斷指令長度 = true;
            if (要判斷指令長度)
            {
                if (LADDER_buf.元素數量 != 輸入指令元素數量) cnt = 200;
            }      
        }
        void cnt_檢查輸入指令_10_檢查一般指令內容(ref byte cnt)
        {
            bool FLAG = false;
            string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            int Cmommadval = 0;
            if (str_temp_array.Count == 2)
            {
                Str_指令內容 = new string[1];
                string[] str_array;
                if (DEVICE.TestDevice(str_temp_array[1], out str_array))
                {
                    str_接點類型 = str_array[0].Remove(1);
                    str_接點編號 = str_array[0].Substring(1);
                    Cmommadval = Convert.ToInt32(str_接點編號);
                    if (str_接點類型 == "X")
                    {
                        if (commandType != CommadTypeEnum.OUT) FLAG = myConvert.檢查八進位合法(Cmommadval);
                    }
                    else if (str_接點類型 == "Y") FLAG = myConvert.檢查八進位合法(Cmommadval);
                    else if (str_接點類型 == "M") FLAG = true;
                    else if (str_接點類型 == "S") FLAG = true;
                    else if (str_接點類型 == "T")
                    {
                        if (commandType != CommadTypeEnum.OUT) FLAG = true;
                    }
                    if(str_array.Length == 2)
                    {
                        str_接點編號 += str_array[1];
                    }
                }
                else
                {
                    FLAG = false;
                }
                if (FLAG)
                {
                    Str_指令內容[0] = str_接點類型;
                    cmommadval[0] = str_接點編號;
                    cnt++;
                    return;
                }
                else
                {
                    cnt = 200;
                    return;
                }
            }
            else if (str_temp_array.Count == 3)
            {
                if (commandType == CommadTypeEnum.OUT)
                {
                    Str_指令內容 = new string[2];

                    int 指令有錯誤 = 0;
                    if (str_temp_array[1].Length > 1) str_接點類型 = str_temp_array[1].Remove(1);
                    else 指令有錯誤++;
                    str_接點編號 = str_temp_array[1].Substring(1);
                    if (!int.TryParse(str_接點編號, out int_接點編號)) 指令有錯誤++;

                    if (str_接點類型 == "T") ;
                    else 指令有錯誤++;

                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[0] = str_接點類型;
                        cmommadval[0] = str_接點編號;
                    }

                    if (str_temp_array[2].Length > 1) str_接點類型 = str_temp_array[2].Remove(1);
                    else 指令有錯誤++;
                    str_接點編號 = str_temp_array[2].Substring(1);
                    if (!int.TryParse(str_接點編號, out int_接點編號)) 指令有錯誤++;

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "K") ;
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }


                    if (指令有錯誤 == 0)
                    {
                        commandType = CommadTypeEnum.OUT_TIMER;
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
                    return;
                }

            }
        }

        void cnt_檢查輸入指令_20_檢查特殊指令_LD_Equal_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            if (commandType == CommadTypeEnum.LD_Equal)
            {
                Str_指令內容 = new string[3];

                if (str_temp_array.Count == 3)
                {
                    int 指令有錯誤 = 0;

                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[0] = str_temp_array[0].Replace("LD", "");
                    }
                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "F") ;     
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[2], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[2].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[2] = str_接點類型;
                        cmommadval[2] = str_接點編號;
                    }
               

                    if(指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_25_檢查特殊指令_MOV_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            if (commandType == CommadTypeEnum.MOV)
            {
                Str_指令內容 = new string[3];

                if (str_temp_array.Count == 3)
                {
                    int 指令有錯誤 = 0;

                   
                    Str_指令內容[0] = str_temp_array[0];
                    bool DoubleWord = (Str_指令內容[0].Substring(0, 1) == "D");


                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "F") ;   
                    else if (str_接點類型 == "T" && !DoubleWord) ;
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }


                    if (!DEVICE.TestDevice(str_temp_array[2], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[2].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[2] = str_接點類型;
                        cmommadval[2] = str_接點編號;
                    }


                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_30_檢查特殊指令_ADD_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            if (commandType == CommadTypeEnum.ADD)
            {
                Str_指令內容 = new string[4];

                if (str_temp_array.Count == 4)
                {
                    int 指令有錯誤 = 0;


                    Str_指令內容[0] = str_temp_array[0];

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[2], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[2].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[2] = str_接點類型;
                        cmommadval[2] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[3], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[3].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[3] = str_接點類型;
                        cmommadval[3] = str_接點編號;
                    }

                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_35_檢查特殊指令_SUB_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            if (commandType == CommadTypeEnum.SUB)
            {
                Str_指令內容 = new string[4];

                if (str_temp_array.Count == 4)
                {
                    int 指令有錯誤 = 0;


                    Str_指令內容[0] = str_temp_array[0];

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }


                    if (!DEVICE.TestDevice(str_temp_array[2], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[2].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[2] = str_接點類型;
                        cmommadval[2] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[3], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[3].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[3] = str_接點類型;
                        cmommadval[3] = str_接點編號;
                    }

                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_40_檢查特殊指令_MUL_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            if (commandType == CommadTypeEnum.MUL)
            {
                Str_指令內容 = new string[4];

                if (str_temp_array.Count == 4)
                {
                    int 指令有錯誤 = 0;


                    Str_指令內容[0] = str_temp_array[0];

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }


                    if (!DEVICE.TestDevice(str_temp_array[2], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[2].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[2] = str_接點類型;
                        cmommadval[2] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[3], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[3].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[3] = str_接點類型;
                        cmommadval[3] = str_接點編號;
                    }

                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_45_檢查特殊指令_DIV_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            if (commandType == CommadTypeEnum.DIV)
            {
                Str_指令內容 = new string[4];

                if (str_temp_array.Count == 4)
                {
                    int 指令有錯誤 = 0;


                    Str_指令內容[0] = str_temp_array[0];

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[2], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[2].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[2] = str_接點類型;
                        cmommadval[2] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[3], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[3].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[3] = str_接點類型;
                        cmommadval[3] = str_接點編號;
                    }

                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_50_檢查特殊指令_INC_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            if (commandType == CommadTypeEnum.INC)
            {
                Str_指令內容 = new string[4];

                if (str_temp_array.Count == 2)
                {
                    int 指令有錯誤 = 0;
                    Str_指令內容[0] = str_temp_array[0];

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;  
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }

                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_55_檢查特殊指令_DRVA_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            if (commandType == CommadTypeEnum.DRVA)
            {
                Str_指令內容 = new string[4];

                if (str_temp_array.Count == 4)
                {
                    int 指令有錯誤 = 0;


                    Str_指令內容[0] = str_temp_array[0];

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[2], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[2].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[2] = str_接點類型;
                        cmommadval[2] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[3], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[3].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[3] = str_接點類型;
                        cmommadval[3] = str_接點編號;
                    }
           
                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_60_檢查特殊指令_DRVI_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            if (commandType == CommadTypeEnum.DRVI)
            {
                Str_指令內容 = new string[4];

                if (str_temp_array.Count == 4)
                {
                    int 指令有錯誤 = 0;


                    Str_指令內容[0] = str_temp_array[0];

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }


                    if (!DEVICE.TestDevice(str_temp_array[2], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[2].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[2] = str_接點類型;
                        cmommadval[2] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[3], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[3].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[3] = str_接點類型;
                        cmommadval[3] = str_接點編號;
                    }

                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_65_檢查特殊指令_PLSV_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            if (commandType == CommadTypeEnum.PLSV)
            {
                Str_指令內容 = new string[3];

                if (str_temp_array.Count == 3)
                {
                    int 指令有錯誤 = 0;


                    Str_指令內容[0] = str_temp_array[0];

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[2], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[2].Remove(1);

             
                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[2] = str_接點類型;
                        cmommadval[2] = str_接點編號;
                    }



                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_70_檢查特殊指令_SET_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            if (commandType == CommadTypeEnum.SET)
            {
                Str_指令內容 = new string[4];

                if (str_temp_array.Count == 2)
                {
                    int 指令有錯誤 = 0;


                    Str_指令內容[0] = str_temp_array[0];

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "X") ;
                    else if (str_接點類型 == "Y") ;
                    else if (str_接點類型 == "M") ;
                    else if (str_接點類型 == "S") ;
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }

                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_75_檢查特殊指令_RST_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            if (commandType == CommadTypeEnum.RST)
            {
                Str_指令內容 = new string[4];

                if (str_temp_array.Count == 2)
                {
                    int 指令有錯誤 = 0;


                    Str_指令內容[0] = str_temp_array[0];

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "X") ;
                    else if (str_接點類型 == "Y") ;
                    else if (str_接點類型 == "M") ;
                    else if (str_接點類型 == "S") ;
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }

                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_80_檢查特殊指令_ZRST_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            if (commandType == CommadTypeEnum.ZRST)
            {
                Str_指令內容 = new string[3];

                if (str_temp_array.Count == 3)
                {
                    int 指令有錯誤 = 0;


                    Str_指令內容[0] = str_temp_array[0];
                    bool DoubleWord = (Str_指令內容[0].Substring(0, 1) == "D");

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "X") ;
                    else if (str_接點類型 == "Y") ;
                    else if (str_接點類型 == "M") ;
                    else if (str_接點類型 == "S") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[2], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[2].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "X") ;
                    else if (str_接點類型 == "Y") ;
                    else if (str_接點類型 == "M") ;
                    else if (str_接點類型 == "S") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[2] = str_接點類型;
                        cmommadval[2] = str_接點編號;
                    }
                    if (指令有錯誤 == 0)
                    {
                        if (Str_指令內容[1] != Str_指令內容[2]) 指令有錯誤++;
                        if (Convert.ToInt32(cmommadval[1]) >= Convert.ToInt32(cmommadval[2])) 指令有錯誤++;
                    }
                   
                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_85_檢查特殊指令_BMOV_內容(ref byte cnt)
        {
          string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            if (commandType == CommadTypeEnum.BMOV)
            {
                Str_指令內容 = new string[4];

                if (str_temp_array.Count == 4)
                {
                    int 指令有錯誤 = 0;
                    Str_指令內容[0] = str_temp_array[0];

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;      
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[2], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[2].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[2] = str_接點類型;
                        cmommadval[2] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[3], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[3].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "Z") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[3] = str_接點類型;
                        cmommadval[3] = str_接點編號;
                    }
                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_90_檢查特殊指令_WTB_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            if (commandType == CommadTypeEnum.WTB)
            {
                Str_指令內容 = new string[4];

                if (str_temp_array.Count == 4)
                {
                    int 指令有錯誤 = 0;
                    Str_指令內容[0] = str_temp_array[0];

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[2], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[2].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "F") ;   
                    else if (str_接點類型 == "K") ;
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[2] = str_接點類型;
                        cmommadval[2] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[3], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[3].Remove(1);

                    if (str_接點類型 == "S") ;
                    else if (str_接點類型 == "Y") ;
                    else if (str_接點類型 == "M") ;
                    else 指令有錯誤++;

                    if (str_接點類型.IndexOf("Z") != -1) 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[3] = str_接點類型;
                        cmommadval[3] = str_接點編號;
                    }
                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_95_檢查特殊指令_BTW_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            int int_接點編號 = 0;
            if (commandType == CommadTypeEnum.BTW)
            {
                Str_指令內容 = new string[4];

                if (str_temp_array.Count == 4)
                {
                    int 指令有錯誤 = 0;
                    Str_指令內容[0] = str_temp_array[0];

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "X") ;
                    else if (str_接點類型 == "Y") ;
                    else if (str_接點類型 == "M") ;
                    else if (str_接點類型 == "S") ;
                    else 指令有錯誤++;
                    if (str_接點類型.IndexOf("Z") != -1) 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[2], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[2].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[2] = str_接點類型;
                        cmommadval[2] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[3], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[3].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[3] = str_接點類型;
                        cmommadval[3] = str_接點編號;
                    }
                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_100_檢查特殊指令_TAB_內容(ref byte cnt)
        {
            if (commandType == CommadTypeEnum.TAB)
            {
                Str_指令內容 = new string[3];
                int temp = 0;
                if (str_temp_array.Count == 3)
                {
                    int 指令有錯誤 = 0;
                    Str_指令內容[0] = str_temp_array[0];
                    if(!Int32.TryParse(str_temp_array[1],out temp))
                    {
                        指令有錯誤++;
                    }
                    if (temp < 0) 指令有錯誤++;
                    Str_指令內容[1] = str_temp_array[1];
                    Str_指令內容[2] = str_temp_array[2];
                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_105_檢查特殊指令_JUMP_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            if (commandType == CommadTypeEnum.JUMP)
            {
                Str_指令內容 = new string[2];
                int temp = 0;
                if (str_temp_array.Count == 2)
                {
                    int 指令有錯誤 = 0;
                    Str_指令內容[0] = str_temp_array[0];
                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "F") ;   
                    else if (str_接點類型 == "K") ;
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }
                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }
        void cnt_檢查輸入指令_110_檢查特殊指令_REF_內容(ref byte cnt)
        {
            string str_接點類型 = "";
            string str_接點編號 = "";
            if (commandType == CommadTypeEnum.REF)
            {
                Str_指令內容 = new string[3];

                if (str_temp_array.Count == 3)
                {
                    int 指令有錯誤 = 0;
                    Str_指令內容[0] = str_temp_array[0];

                    if (!DEVICE.TestDevice(str_temp_array[1], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[1].Remove(1);

                    if (str_接點類型 == "X") ;
                    else if (str_接點類型 == "Y") ;
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[1] = str_接點類型;
                        cmommadval[1] = str_接點編號;
                    }

                    if (!DEVICE.TestDevice(str_temp_array[2], ref str_接點編號)) 指令有錯誤++;
                    str_接點類型 = str_temp_array[2].Remove(1);

                    if (str_接點類型 == "K") ;
                    else if (str_接點類型 == "D") ;
                    else if (str_接點類型 == "R") ;
                    else if (str_接點類型 == "F") ;   
                    else 指令有錯誤++;
                    if (指令有錯誤 == 0)
                    {
                        Str_指令內容[2] = str_接點類型;
                        cmommadval[2] = str_接點編號;
                    }

            
                    if (指令有錯誤 == 0)
                    {
                        cnt++;
                    }
                    else
                    {
                        cnt = 200;
                    }
                }
            }
        }

        void cnt_檢查輸入指令_140_寫入註解(ref byte cnt)
        {                     
            Str_指令內容[註解輸入次數] = textBox_指令輸入.Text;
            註解輸入次數++;
            cnt++;
        }
        void cnt_檢查輸入指令_150_指令輸入完成(ref byte cnt)
        {
            Command_OK = true;
            button_指令輸入完成.Focus();   
            this.Close();
            cnt++;
        }
        void cnt_檢查輸入指令_200_輸入失敗(ref byte cnt)
        {
            SystemSounds.Beep.Play();
            cnt++;
        }

        void cnt_檢查輸入指令_(ref byte cnt)
        {

            cnt++;
        }
        void sub_cnt_檢查輸入指令_檢查是否為8進位(ref bool FLAG,int val)
        {
            FLAG = false;
            int temp0 = val;
            int 位數 = 0;
            if (val >= 0)         
            {
                while (true)
                {
                    if ((int)(val / (int)Math.Pow(10, 位數)) > 0)
                    {
                        int i = val / (int)Math.Pow(10, 位數);
                        位數++;
                    }
                    else break;
                }
                while(true)
                {
                    int temp1 = temp0 / (int)Math.Pow(10, 位數 );
                    temp0 = temp0 - temp1 * (int)Math.Pow(10, 位數 );
                   
                    if (temp1 > 7) break;
                    if (位數 == 0)
                    {
                        FLAG = true;
                        break;
                    }
                    位數--;
                }
            }
        }

        #endregion
        private void textBox_指令輸入_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.Enter)
            {
                e.Handled = true;
            }
        }
        private void textBox_指令輸入_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.Enter)
            {
                e.Handled = true;          
            }
        }
        private void textBox_指令輸入_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                按下輸入 = true;
                e.Handled = true;
            }
   
        }



    }
}
