using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
using LadderConnection;
namespace LadderForm
{
    public partial class Download : Form
    {
        private Keyboard Keys = new Keyboard();
        private MyConvert myConvert = new MyConvert();
        private static Download download;
        private static readonly object synRoot = new object();
        public static bool 視窗已建立;
        private bool FLAG_Messagebox已開啟 = false;
        private DialogResult Result;
        private List<MyUI.ExButton> List_ExButton = new List<MyUI.ExButton>();
        public static Download GetForm(Point P0)
        {
            lock (synRoot)
            {
                if (download == null)
                {
                    download = new Download();
                }
                download.StartPosition = FormStartPosition.Manual;
                P0.X -= download.Size.Width / 2;
                P0.Y -= download.Size.Height / 2;
                download.Location = P0;
                視窗已建立 = true;
            }
            return download;
        }
        public Download()
        {
            InitializeComponent();
        }

        private void Download_Load(object sender, EventArgs e)
        {
            up_Down_load_Panel.exButton_上傳_下載.ON_文字內容 = "下載";
            up_Down_load_Panel.exButton_上傳_下載.OFF_文字內容 = "下載";
            List_ExButton.Add(up_Down_load_Panel.exButton_上傳_下載);
            timer_程序執行.Enabled = true;
        }
        private void Download_FormClosing(object sender, FormClosingEventArgs e)
        {
            TopMachine.ProgramStopRead();
            timer_程序執行.Dispose();
            視窗已建立 = false;
            download = null;
        }
        private void Download_FormClosed(object sender, FormClosedEventArgs e)
        {
            視窗已建立 = false;
            download = null;
        }

        private void timer_程序執行_Tick(object sender, EventArgs e)
        {
            foreach (MyUI.ExButton ExButton_temp in List_ExButton)
            {
                ExButton_temp.Run();
            }
            sub_檢查下載按鈕();
            if (Keys.Key_Esc)
            {
                this.Close();
            }
        }
        #region 檢查下載按鈕
        static string str_檢查下載按鈕_視窗顯示文字 = "";
        byte cnt_檢查下載按鈕 = 255;
        void sub_檢查下載按鈕()
        {
            if (cnt_檢查下載按鈕 == 255) cnt_檢查下載按鈕 = 1;
            if (cnt_檢查下載按鈕 == 1) cnt_檢查下載按鈕_00_檢查按下(ref cnt_檢查下載按鈕);
            if (cnt_檢查下載按鈕 == 2) cnt_檢查下傳按鈕_00_彈出視窗(ref cnt_檢查下載按鈕);
            if (cnt_檢查下載按鈕 == 3) cnt_檢查下載按鈕_00_初始化(ref cnt_檢查下載按鈕);
            if (cnt_檢查下載按鈕 == 4) cnt_檢查下載按鈕 = 10;

            if (cnt_檢查下載按鈕 == 10) cnt_檢查下載按鈕_10_開始讀取(ref cnt_檢查下載按鈕);
            if (cnt_檢查下載按鈕 == 11) cnt_檢查下載按鈕_10_更新控件(ref cnt_檢查下載按鈕);
            if (cnt_檢查下載按鈕 == 12) cnt_檢查下載按鈕_10_等待編譯與註解寫入_READY(ref cnt_檢查下載按鈕);
            if (cnt_檢查下載按鈕 == 13) cnt_檢查下載按鈕_10_等待編譯與註解寫入_OVER(ref cnt_檢查下載按鈕);
            if (cnt_檢查下載按鈕 == 14) cnt_檢查下載按鈕 = 150;

            if (cnt_檢查下載按鈕 == 150) cnt_檢查下載按鈕_150_讀取成功(ref cnt_檢查下載按鈕);
            if (cnt_檢查下載按鈕 == 151) cnt_檢查下載按鈕 = 250;

            if (cnt_檢查下載按鈕 == 200) cnt_檢查下載按鈕_200_讀取失敗(ref cnt_檢查下載按鈕);
            if (cnt_檢查下載按鈕 == 201) cnt_檢查下載按鈕 = 250;

            if (cnt_檢查下載按鈕 == 250) cnt_檢查下載按鈕_250_檢查放開(ref cnt_檢查下載按鈕);
            if (cnt_檢查下載按鈕 == 251) cnt_檢查下載按鈕_250_顯示彈出視窗(ref cnt_檢查下載按鈕);
            if (cnt_檢查下載按鈕 == 252) cnt_檢查下載按鈕 = 255;
        }
        void cnt_檢查下載按鈕_00_檢查按下(ref byte cnt)
        {
            if (up_Down_load_Panel.exButton_上傳_下載.Load_WriteState())
            {
                FLAG_Messagebox已開啟 = false;
                cnt++;
            }

        }
        void cnt_檢查下傳按鈕_00_彈出視窗(ref byte cnt)
        {
            if (!FLAG_Messagebox已開啟)
            {
                FLAG_Messagebox已開啟 = true;
                Result = MessageBox.Show("確認【下載】程式至本機?", "Warring", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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
        void cnt_檢查下載按鈕_00_初始化(ref byte cnt)
        {
            up_Down_load_Panel.progressBar_處理進度條.Value = 0;
            TopMachine.properties.Program.Clear();
            for (int i = 0; i < LadderUI.LADDER_Panel.IL指令程式.Count; i++)
            {
                string[] str = LadderUI.LADDER_Panel.IL指令程式[i];
                TopMachine.properties.Program.Add(str);
            }
            cnt++;
        }

        void cnt_檢查下載按鈕_10_開始讀取(ref byte cnt)
        {
            int processvalue = 0;
            bool read_comment = up_Down_load_Panel.checkBox_Comment.Checked;
            int temp = TopMachine.ProgramRead(ref processvalue, read_comment);           
            up_Down_load_Panel.progressBar_處理進度條.Value = processvalue;
            CallBackUI.label.字串更換(processvalue.ToString(), up_Down_load_Panel.label_進度);
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
        void cnt_檢查下載按鈕_10_更新控件(ref byte cnt)
        {
            cnt++;
        }
        void cnt_檢查下載按鈕_10_等待編譯與註解寫入_READY(ref byte cnt)
        {
           if( LadderUI.LADDER_Panel.cnt_Download_程式反編譯及註解寫入 ==255)
           {
               bool read_comment = up_Down_load_Panel.checkBox_Comment.Checked;
               if (read_comment) LadderUI.LADDER_Panel.FLAG_Download_程式反編譯及註解寫入_註解要寫入 = true;
               else LadderUI.LADDER_Panel.FLAG_Download_程式反編譯及註解寫入_註解要寫入 = false;

               LadderUI.LADDER_Panel.cnt_Download_程式反編譯及註解寫入 = 1;
               cnt++;
           }           
        }
        void cnt_檢查下載按鈕_10_等待編譯與註解寫入_OVER(ref byte cnt)
        {
            if (LadderUI.LADDER_Panel.cnt_Download_程式反編譯及註解寫入 == 255)
            {
                cnt++;
            }  
        }

        void cnt_檢查下載按鈕_150_讀取成功(ref byte cnt)
        {
            str_檢查下載按鈕_視窗顯示文字 = "讀取成功!";
            cnt++;
        }

        void cnt_檢查下載按鈕_200_讀取失敗(ref byte cnt)
        {
            str_檢查下載按鈕_視窗顯示文字 = "讀取失敗!";
            cnt++;
        }

        void cnt_檢查下載按鈕_250_檢查放開(ref byte cnt)
        {
            if (!up_Down_load_Panel.exButton_上傳_下載.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_檢查下載按鈕_250_顯示彈出視窗(ref byte cnt)
        {
            cnt++;
            if (str_檢查下載按鈕_視窗顯示文字 == "讀取成功!")
            {
                MessageBox.Show(str_檢查下載按鈕_視窗顯示文字, "Asterisk", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            if (str_檢查下載按鈕_視窗顯示文字 == "讀取失敗!")
            {
                MessageBox.Show(str_檢查下載按鈕_視窗顯示文字, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        void cnt_檢查下載按鈕_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
    }
}
