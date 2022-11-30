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
    public partial class Verify : Form
    {
        private Keyboard Keys = new Keyboard();
        private MyConvert myConvert = new MyConvert();
        private static Verify verify;
        private static readonly object synRoot = new object();
        public static bool 視窗已建立;
        private bool FLAG_Messagebox已開啟 = false;
        private List<string> Verify_Result = new List<string>();
        public List<string[]> Verify_Program = new List<string[]>();
        private List<MyUI.ExButton> List_ExButton = new List<MyUI.ExButton>();
        public static Verify GetForm(Point P0)
        {
            lock (synRoot)
            {
                if (verify == null)
                {
                    verify = new Verify();
                }
                verify.StartPosition = FormStartPosition.Manual;
                P0.X -= verify.Size.Width / 2;
                P0.Y -= verify.Size.Height / 2;
                verify.Location = P0;
                視窗已建立 = true;
            }
            return verify;
        }
        public Verify()
        {
            InitializeComponent();
        }

        private void Verify_Load(object sender, EventArgs e)
        {
            up_Down_load_Panel.exButton_上傳_下載.ON_文字內容 = "比較";
            up_Down_load_Panel.exButton_上傳_下載.OFF_文字內容 = "比較";
            up_Down_load_Panel.checkBox_Comment.Visible = false;

            List_ExButton.Add(up_Down_load_Panel.exButton_上傳_下載);
            timer_程序執行.Enabled = true;
        }
        private void Verify_FormClosing(object sender, FormClosingEventArgs e)
        {
            TopMachine.ProgramStopVerify();
            timer_程序執行.Dispose();
            視窗已建立 = false;
            verify = null;
        }
        private void Verify_FormClosed(object sender, FormClosedEventArgs e)
        {
            視窗已建立 = false;
            verify = null;
        }

        private void timer_程序執行_Tick(object sender, EventArgs e)
        {
            foreach (MyUI.ExButton ExButton_temp in List_ExButton)
            {
                ExButton_temp.Run();
            }
            sub_檢查比較按鈕();
            if (Keys.Key_Esc)
            {
                this.Close();
            }
        }
        #region 檢查比較按鈕
        static string str_檢查比較按鈕_視窗顯示文字 = "";
        byte cnt_檢查比較按鈕 = 255;
        void sub_檢查比較按鈕()
        {
            if (cnt_檢查比較按鈕 == 255) cnt_檢查比較按鈕 = 1;
            if (cnt_檢查比較按鈕 == 1) cnt_檢查比較按鈕_00_檢查按下(ref cnt_檢查比較按鈕);
            if (cnt_檢查比較按鈕 == 2) cnt_檢查比較按鈕_00_初始化(ref cnt_檢查比較按鈕);
            if (cnt_檢查比較按鈕 == 3) cnt_檢查比較按鈕 = 10;

            if (cnt_檢查比較按鈕 == 10) cnt_檢查比較按鈕_10_開始比較(ref cnt_檢查比較按鈕);
            if (cnt_檢查比較按鈕 == 11) cnt_檢查比較按鈕_10_更新控件(ref cnt_檢查比較按鈕);
            if (cnt_檢查比較按鈕 == 12) cnt_檢查比較按鈕_10_等待編譯與註解寫入_READY(ref cnt_檢查比較按鈕);
            if (cnt_檢查比較按鈕 == 13) cnt_檢查比較按鈕_10_等待編譯與註解寫入_OVER(ref cnt_檢查比較按鈕);
            if (cnt_檢查比較按鈕 == 14) cnt_檢查比較按鈕 = 150;

            if (cnt_檢查比較按鈕 == 150) cnt_檢查比較按鈕_150_比較成功(ref cnt_檢查比較按鈕);
            if (cnt_檢查比較按鈕 == 151) cnt_檢查比較按鈕 = 250;

            if (cnt_檢查比較按鈕 == 200) cnt_檢查比較按鈕_200_比較失敗(ref cnt_檢查比較按鈕);
            if (cnt_檢查比較按鈕 == 201) cnt_檢查比較按鈕 = 250;

            if (cnt_檢查比較按鈕 == 250) cnt_檢查比較按鈕_250_檢查放開(ref cnt_檢查比較按鈕);
            if (cnt_檢查比較按鈕 == 251) cnt_檢查比較按鈕_250_顯示彈出視窗(ref cnt_檢查比較按鈕);
            if (cnt_檢查比較按鈕 == 252) cnt_檢查比較按鈕 = 255;
        }
        void cnt_檢查比較按鈕_00_檢查按下(ref byte cnt)
        {
            if (up_Down_load_Panel.exButton_上傳_下載.Load_WriteState())
            {
                FLAG_Messagebox已開啟 = false;
                cnt++;
            }

        }
        void cnt_檢查比較按鈕_00_初始化(ref byte cnt)
        {
            up_Down_load_Panel.progressBar_處理進度條.Value = 0;
            TopMachine.properties.Program.Clear();
            Verify_Result.Clear();
            for (int i = 0; i < LadderUI.LADDER_Panel.IL指令程式.Count; i++)
            {
                string[] str = LadderUI.LADDER_Panel.IL指令程式[i];
                TopMachine.properties.Program.Add(str);
            }
            cnt++;
        }

        void cnt_檢查比較按鈕_10_開始比較(ref byte cnt)
        {
            int processvalue = 0;
            bool read_comment = up_Down_load_Panel.checkBox_Comment.Checked;
            int temp = TopMachine.ProgramVerify(ref processvalue, ref Verify_Result, Verify_Program);
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
        void cnt_檢查比較按鈕_10_更新控件(ref byte cnt)
        {
              
            cnt++;
        }
        void cnt_檢查比較按鈕_10_等待編譯與註解寫入_READY(ref byte cnt)
        {
            cnt++;
        }
        void cnt_檢查比較按鈕_10_等待編譯與註解寫入_OVER(ref byte cnt)
        {
            cnt++;
        }

        void cnt_檢查比較按鈕_150_比較成功(ref byte cnt)
        {
            str_檢查比較按鈕_視窗顯示文字 = "比較成功!";
            cnt++;
        }

        void cnt_檢查比較按鈕_200_比較失敗(ref byte cnt)
        {
            str_檢查比較按鈕_視窗顯示文字 = "比較失敗!";
            cnt++;
        }

        void cnt_檢查比較按鈕_250_檢查放開(ref byte cnt)
        {
            if (!up_Down_load_Panel.exButton_上傳_下載.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_檢查比較按鈕_250_顯示彈出視窗(ref byte cnt)
        {
            for (int i = 0; i < Verify_Result.Count; i++)
            {
                CallBackUI.listbox.新增項目(Verify_Result[i], listBox_比較結果);
            }     
            cnt++;
            if (str_檢查比較按鈕_視窗顯示文字 == "比較成功!")
            {
                MessageBox.Show(str_檢查比較按鈕_視窗顯示文字, "Asterisk", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            if (str_檢查比較按鈕_視窗顯示文字 == "比較失敗!")
            {
                MessageBox.Show(str_檢查比較按鈕_視窗顯示文字, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        void cnt_檢查比較按鈕_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
    }
}
