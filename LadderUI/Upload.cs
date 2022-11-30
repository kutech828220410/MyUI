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
    public partial class Upload : Form
    {
        private Keyboard Keys = new Keyboard();
        private MyConvert myConvert = new MyConvert();
        private static Upload upload;
        private static readonly object synRoot = new object();
        public static bool 視窗已建立;
        private bool FLAG_Messagebox已開啟 = false;
        public bool FLAG_快閃模式 = false;
        private DialogResult Result;
        private List<MyUI.ExButton> List_ExButton = new List<MyUI.ExButton>();

        public static Upload GetForm(Point P0)
        {
            lock (synRoot)
            {
                if (upload == null)
                {
                    upload = new Upload();
                }
                upload.StartPosition = FormStartPosition.Manual;
                P0.X -= upload.Size.Width / 2;
                P0.Y -= upload.Size.Height / 2;
                upload.Location = P0;
                視窗已建立 = true;
            }
            return upload;
        }
        public Upload()
        {
            InitializeComponent();
        }

        private void Upload_Load(object sender, EventArgs e)
        {
            up_Down_load_Panel.exButton_上傳_下載.ON_文字內容 = "上傳";
            up_Down_load_Panel.exButton_上傳_下載.OFF_文字內容 = "上傳";
            List_ExButton.Add(up_Down_load_Panel.exButton_上傳_下載);
            //if (FLAG_快閃模式) up_Down_load_Panel.checkBox_Comment.Checked = false;
            timer_程序執行.Enabled = true;
        }

        private void Upload_FormClosing(object sender, FormClosingEventArgs e)
        {
            TopMachine.ProgramStopWrite();
            timer_程序執行.Dispose();
            視窗已建立 = false;
            upload = null;
        }

        private void Upload_FormClosed(object sender, FormClosedEventArgs e)
        {
            視窗已建立 = false;
            upload = null;
        }
        byte cnt_快閃模式 = 1;
        private void timer_程序執行_Tick(object sender, EventArgs e)
        {
            foreach (MyUI.ExButton ExButton_temp in List_ExButton)
            {
                ExButton_temp.Run();
            }
            if(FLAG_快閃模式)
            {
                if(cnt_快閃模式 ==1)
                {
                    this.Enabled = false;
                    if (cnt_檢查上傳按鈕 == 255)
                    {
                        cnt_檢查上傳按鈕 = 2;
                        cnt_快閃模式++;
                    }
                }
                if(cnt_快閃模式 == 2)
                {
                    if (cnt_檢查上傳按鈕 == 255)
                    {
                        cnt_快閃模式++;
                    }
                }
                if (cnt_快閃模式 == 3)
                {
                    FLAG_快閃模式 = false;
                    this.Close();
                }

            }
            sub_檢查上傳按鈕();
            if(Keys.Key_Esc)
            {
                this.Close();
            }
        }
        #region 檢查上傳按鈕
        static string str_檢查上傳按鈕_視窗顯示文字 = "";
        byte cnt_檢查上傳按鈕 = 255;
        void sub_檢查上傳按鈕()
        {
            if (cnt_檢查上傳按鈕 == 255) cnt_檢查上傳按鈕 = 1;
            if (cnt_檢查上傳按鈕 == 1) cnt_檢查上傳按鈕_00_檢查按下(ref cnt_檢查上傳按鈕);
            if (cnt_檢查上傳按鈕 == 2) cnt_檢查上傳按鈕_00_彈出視窗(ref cnt_檢查上傳按鈕);            
            if (cnt_檢查上傳按鈕 == 3) cnt_檢查上傳按鈕_00_初始化(ref cnt_檢查上傳按鈕);
            if (cnt_檢查上傳按鈕 == 4) cnt_檢查上傳按鈕 = 10;

            if (cnt_檢查上傳按鈕 == 10) cnt_檢查上傳按鈕_10_開始寫入(ref cnt_檢查上傳按鈕);
            if (cnt_檢查上傳按鈕 == 11) cnt_檢查上傳按鈕_10_更新控件(ref cnt_檢查上傳按鈕);
            if (cnt_檢查上傳按鈕 == 12) cnt_檢查上傳按鈕 = 150;

            if (cnt_檢查上傳按鈕 == 150) cnt_檢查上傳按鈕_150_寫入成功(ref cnt_檢查上傳按鈕);
            if (cnt_檢查上傳按鈕 == 151) cnt_檢查上傳按鈕 = 250;

            if (cnt_檢查上傳按鈕 == 200) cnt_檢查上傳按鈕_200_寫入失敗(ref cnt_檢查上傳按鈕);
            if (cnt_檢查上傳按鈕 == 201) cnt_檢查上傳按鈕 = 250;

            if (cnt_檢查上傳按鈕 == 250) cnt_檢查上傳按鈕_250_檢查放開(ref cnt_檢查上傳按鈕);
            if (cnt_檢查上傳按鈕 == 251) cnt_檢查上傳按鈕_250_顯示彈出視窗(ref cnt_檢查上傳按鈕);
            if (cnt_檢查上傳按鈕 == 252) cnt_檢查上傳按鈕 = 255;
        }
        void cnt_檢查上傳按鈕_00_檢查按下(ref byte cnt)
        {
            if (up_Down_load_Panel.exButton_上傳_下載.Load_WriteState())
            {
                FLAG_Messagebox已開啟 = false;
                cnt++;
            }

        }
        void cnt_檢查上傳按鈕_00_彈出視窗(ref byte cnt)
        {
            if (!FLAG_快閃模式)
            {
                if (!FLAG_Messagebox已開啟)
                {
                    FLAG_Messagebox已開啟 = true;
                    Result = MessageBox.Show("確認【上傳】程式至上位機?", "Warring", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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
            else
            {
                cnt++;
            }
        }
        void cnt_檢查上傳按鈕_00_初始化(ref byte cnt)
        {
            up_Down_load_Panel.progressBar_處理進度條.Value = 0;
            TopMachine.properties.Program.Clear();
            for (int i = 0; i < LadderUI.LADDER_Panel.IL指令程式.Count; i++ )
            {
                string[] str = LadderUI.LADDER_Panel.IL指令程式[i];
                TopMachine.properties.Program.Add(str);
            }
            cnt++;
        }
   
        void cnt_檢查上傳按鈕_10_開始寫入(ref byte cnt)
        {
            int processvalue = 0;
            bool write_comment = up_Down_load_Panel.checkBox_Comment.Checked;
            int temp = TopMachine.ProgramWrite(ref processvalue, write_comment);
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

        void cnt_檢查上傳按鈕_10_更新控件(ref byte cnt)
        {
            cnt++;
        }
        void cnt_檢查上傳按鈕_150_寫入成功(ref byte cnt)
        {
            str_檢查上傳按鈕_視窗顯示文字 = "寫入成功!";
            cnt++;
        }
        void cnt_檢查上傳按鈕_200_寫入失敗(ref byte cnt)
        {
            str_檢查上傳按鈕_視窗顯示文字 = "寫入失敗!";
            cnt++;
        }

        void cnt_檢查上傳按鈕_250_檢查放開(ref byte cnt)
        {
            if (!up_Down_load_Panel.exButton_上傳_下載.Load_WriteState())
            {
                cnt++;
            }
        }
        void cnt_檢查上傳按鈕_250_顯示彈出視窗(ref byte cnt)
        {
            cnt++;
            if (!FLAG_快閃模式)
            {
                if (str_檢查上傳按鈕_視窗顯示文字 == "寫入成功!")
                {
                    MessageBox.Show(str_檢查上傳按鈕_視窗顯示文字, "Asterisk", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                }
            }
            if (str_檢查上傳按鈕_視窗顯示文字 == "寫入失敗!")
            {
                MessageBox.Show(str_檢查上傳按鈕_視窗顯示文字, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            
        }

        void cnt_檢查上傳按鈕_(ref byte cnt)
        {
            cnt++;
        }
        #endregion
    }
}
