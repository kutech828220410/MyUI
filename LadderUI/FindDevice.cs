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
    public partial class FindDevice : Form
    {
        private Keyboard Keys = new Keyboard();
        private MyConvert myConvert = new MyConvert();
        private static FindDevice findDevice;
        private static readonly object synRoot = new object();
        public static bool 視窗已建立;
        public static string 起始視窗文字 = "";
       
        private Point 起始位置 = new Point();
        private Point 現在位置 = new Point();
        private int 起始位置畫面索引 = 0;
        public static LADDER_Panel.LADDER LADDER_buf = new LADDER_Panel.LADDER();
        public static List<LADDER_Panel.LADDER[]> ladder_list = new List<LADDER_Panel.LADDER[]>();
        public static int 一列格數;
        public static int 一個畫面列數;
        public static Size 操作方框大小;
        public static Point 視窗原點;
        private List<MyUI.ExButton> List_ExButton = new List<MyUI.ExButton>();

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            /* switch (keyData)
             {
                
             }*/
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public static FindDevice GetForm(Point P0)
        {
            lock (synRoot)
            {
                if (findDevice == null)
                {
                    findDevice = new FindDevice();          
                }
                findDevice.StartPosition = FormStartPosition.Manual;
                findDevice.Location = P0;
                視窗已建立 = true;
            }           
            return findDevice;
        }
        private void FindDevice_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < LADDER_buf.元素數量; i++ )
            {
                if(DEVICE.TestDevice(LADDER_buf.ladderParam[i]))
                {
                    起始視窗文字 = LADDER_buf.ladderParam[i];
                    break;
                }
            }
            起始位置 = LADDER_Panel.操作方框索引;
            起始位置畫面索引 = LADDER_Panel.顯示畫面列數索引;
            findDevice.textBox_DeviceName.Text = 起始視窗文字;
            現在位置 = new Point();
            timer_程序執行.Enabled = true;
            List_ExButton.Add(exButton_Close);
            List_ExButton.Add(exButton_FindNext);
        }
        private void FindDevice_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer_程序執行.Dispose();
            視窗已建立 = false;
            findDevice = null;
        }
        private void FindDevice_FormClosed(object sender, FormClosedEventArgs e)
        {
            視窗已建立 = false;
            findDevice = null;
        }
        public FindDevice()
        {
            InitializeComponent();
        }
        private void timer_程序執行_Tick(object sender, EventArgs e)
        {
            if (findDevice != null)
            {
                foreach (MyUI.ExButton ExButton_temp in List_ExButton)
                {
                    ExButton_temp.Run();
                }

                if (findDevice.ContainsFocus) 視窗已建立 = true;
                else 視窗已建立 = false;

                if (!findDevice.ContainsFocus)
                {
                    SystemSounds.Beep.Play();
                    findDevice.Focus();
                }
                sub_檢查ENTER按下();
                sub_搜尋指定Device();
                if (Keys.Key_Esc)
                {
                    if(findDevice!=null)findDevice.Close();
                }      
            }                    
        }
        String str_devicename = "";
        String str_彈出視窗文字 = "";
        byte cnt_搜尋指定Device = 255;
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
            if(Keys.Key_Enter)
            {
                if (cnt_搜尋指定Device == 255) cnt_搜尋指定Device = 1;
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
            str_devicename = textBox_DeviceName.Text;
            str_devicename = str_devicename.ToUpper();
            if (str_devicename == "")
            {
                str_彈出視窗文字 = "非法元件,查詢失敗!";
                cnt = 200;
            }
            else cnt++;

        }
        void cnt_搜尋指定Device_檢查字串可搜尋(ref byte cnt)
        {
            if (DEVICE.TestDevice(str_devicename)) cnt++;
            else
            {
                str_彈出視窗文字 = "非法元件,查詢失敗!";
                cnt = 200;
            }

        }
        void cnt_搜尋指定Device_開始搜尋(ref byte cnt)
        {
            for (int Y = 現在位置.Y; Y < ladder_list.Count;Y++ )
            {
                for (int X = 0; X < 一列格數; X++)
                {
                    if (X >= 現在位置.X)
                    {
                        for (int i = 0; i < ladder_list[Y][X].元素數量; i++)
                        {
                            if (ladder_list[Y][X].ladderParam[i]!=null)
                            {
                                if (ladder_list[Y][X].ladderParam[i] == str_devicename)
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
                現在位置.X = 0;
            }
            cnt = 200;
            str_彈出視窗文字 = "已搜尋到終點!";
        }
        void cnt_搜尋指定Device_更新索引及現在位置(ref byte cnt)
        {
            int temp_X = 現在位置.X;
            int temp_Y = 現在位置.Y;
            int temp_顯示畫面列數索引 = 0;
            while(true)
            {
                if (temp_Y > 一個畫面列數 - 1)
                {
                    temp_Y --;
                    temp_顯示畫面列數索引++;
                }
                else break;
            }

            LADDER_Panel.操作方框索引.X = temp_X;
            LADDER_Panel.操作方框索引.Y = temp_Y;
            LADDER_Panel.顯示畫面列數索引 = temp_顯示畫面列數索引;

             Point p0 = 視窗原點;
             Point p1 = new Point((temp_X -1)* 操作方框大小.Width + 視窗原點.X, (temp_Y) * 操作方框大小.Height + 視窗原點.Y - (操作方框大小.Height / 2));
             p1.X += p0.X;
             p1.Y += p0.Y;
             findDevice.Location = p1;


            
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
        private void exButton_Close_btnClick(object sender, EventArgs e)
        {
            findDevice.Close();
        }

        private void exButton_FindNext_btnClick(object sender, EventArgs e)
        {
            if (cnt_搜尋指定Device == 255) cnt_搜尋指定Device = 1;
        }

        private void textBox_DeviceName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.Enter || e.KeyData == System.Windows.Forms.Keys.Escape)
            {
                e.Handled = true;
            }
        }

        private void textBox_DeviceName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13) || (e.KeyChar == System.Convert.ToChar(86)))
            {
                e.Handled = true;
            }
        }

        private void textBox_DeviceName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.Enter || e.KeyData == System.Windows.Forms.Keys.Escape)
            {
                e.Handled = true;
            }
        }

    }
}
