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
namespace MyUI
{
    public partial class 數字鍵盤 : MyDialog
    {
        private bool Beep_Voice = false;
        private int cnt_Beep_Voice = 0;
        private Basic.Keyboard keys = new Basic.Keyboard();
        private List<ExButton> List_ExButton = new List<ExButton>();
        private static 數字鍵盤 _數字鍵盤;
        private static readonly object synRoot = new object();
        public static bool _視窗已建立 = false;
        public static bool 視窗已建立
        {
            get
            {
                return _視窗已建立;
            }
            set
            {
                _視窗已建立 = value;
            }
        }
        public static bool 音效 = false;
        public static String value = "";
        public static int 小數點位置 = 0;
        public static bool Enter = false;
        public static 數字鍵盤 GetForm()
        {
            lock (synRoot)
            {
                if (_數字鍵盤 == null)
                {
                    _數字鍵盤 = new 數字鍵盤();
                }
                Enter = false;
                
            }
            return _數字鍵盤;
        }

        public void SetPosition(Point P0)
        {
            _數字鍵盤.StartPosition = FormStartPosition.Manual;
            _數字鍵盤.Location = P0;
        }
        public string _Init_Text = "";
        public bool flag_Init_Text = false;
        public string Init_Text
        {
            get
            {
                return _Init_Text;
            }
            set
            {
                _Init_Text = value;
                flag_Init_Text = true;
            }
        }
        public 數字鍵盤()
        {
            InitializeComponent();
        }


        private void 數字鍵盤_Load(object sender, EventArgs e)
        {
            _視窗已建立 = true;
            List_ExButton.Add(exButton_0);
            List_ExButton.Add(exButton_1);
            List_ExButton.Add(exButton_2);
            List_ExButton.Add(exButton_3);
            List_ExButton.Add(exButton_4);
            List_ExButton.Add(exButton_5);
            List_ExButton.Add(exButton_6);
            List_ExButton.Add(exButton_7);
            List_ExButton.Add(exButton_8);
            List_ExButton.Add(exButton_9);
            List_ExButton.Add(exButton_Backspace);
            List_ExButton.Add(exButton_Enter);
            List_ExButton.Add(exButton_CE);
            List_ExButton.Add(exButton_Dot);
            List_ExButton.Add(exButton_正負);
            if (小數點位置 < 0) 小數點位置 = 0;
            textBox_Value.Text = _Init_Text;
        }
        string but_CE()
        {
            string temp = "0";
            for (int i = 0; i < 小數點位置; i++)
            {
                if (i == 0) temp = temp + ".0";
                else temp = temp + "0";
            }
            return temp;
        }
        string but_0(string str)
        {
            bool 有負號 = false;
            if (str.IndexOf("-") == 0)
            {
                str = str.Substring(1);
                有負號 = true;
            }
            int find_dot = str.IndexOf(".");
            if (str == but_CE()) str = "";
            bool flag = false;
            if (find_dot > 0)
            {
                if (str.Length - find_dot > 小數點位置) flag = true;
            }
            if (!flag) str = str + "0";
            if (有負號) str = "-" + str;
            return str;
        }
        string but_1(string str)
        {
            bool 有負號 = false;
            if (str.IndexOf("-") == 0)
            {
                str = str.Substring(1);
                有負號 = true;
            }
            int find_dot = str.IndexOf(".");
            if (str == but_CE()) str = "";
            bool flag = false;
            if (find_dot > 0)
            {
                if (str.Length - find_dot > 小數點位置) flag = true;
            }
            if (!flag) str = str + "1";
            if (有負號) str = "-" + str;
            return str;
        }
        string but_2(string str)
        {
            bool 有負號 = false;
            if (str.IndexOf("-") == 0)
            {
                str = str.Substring(1);
                有負號 = true;
            }
            int find_dot = str.IndexOf(".");
            if (str == but_CE()) str = "";
            bool flag = false;
            if (find_dot > 0)
            {
                if (str.Length - find_dot > 小數點位置) flag = true;
            }
            if (!flag) str = str + "2";
            if (有負號) str = "-" + str;
            return str;
        }
        string but_3(string str)
        {
            bool 有負號 = false;
            if (str.IndexOf("-") == 0)
            {
                str = str.Substring(1);
                有負號 = true;
            }
            int find_dot = str.IndexOf(".");
            if (str == but_CE()) str = "";
            bool flag = false;
            if (find_dot > 0)
            {
                if (str.Length - find_dot > 小數點位置) flag = true;
            }
            if (!flag) str = str + "3";
            if (有負號) str = "-" + str;
            return str;
        }
        string but_4(string str)
        {
            bool 有負號 = false;
            if (str.IndexOf("-") == 0)
            {
                str = str.Substring(1);
                有負號 = true;
            }
            int find_dot = str.IndexOf(".");
            if (str == but_CE()) str = "";
            bool flag = false;
            if (find_dot > 0)
            {
                if (str.Length - find_dot > 小數點位置) flag = true;
            }
            if (!flag) str = str + "4";
            if (有負號) str = "-" + str;
            return str;
        }
        string but_5(string str)
        {
            bool 有負號 = false;
            if (str.IndexOf("-") == 0)
            {
                str = str.Substring(1);
                有負號 = true;
            }
            int find_dot = str.IndexOf(".");
            if (str == but_CE()) str = "";
            bool flag = false;
            if (find_dot > 0)
            {
                if (str.Length - find_dot > 小數點位置) flag = true;
            }
            if (!flag) str = str + "5";
            if (有負號) str = "-" + str;
            return str;
        }
        string but_6(string str)
        {
            bool 有負號 = false;
            if (str.IndexOf("-") == 0)
            {
                str = str.Substring(1);
                有負號 = true;
            }
            int find_dot = str.IndexOf(".");
            if (str == but_CE()) str = "";
            bool flag = false;
            if (find_dot > 0)
            {
                if (str.Length - find_dot > 小數點位置) flag = true;
            }
            if (!flag) str = str + "6";
            if (有負號) str = "-" + str;
            return str;
        }
        string but_7(string str)
        {
            bool 有負號 = false;
            if (str.IndexOf("-") == 0)
            {
                str = str.Substring(1);
                有負號 = true;
            }
            int find_dot = str.IndexOf(".");
            if (str == but_CE()) str = "";
            bool flag = false;
            if (find_dot > 0)
            {
                if (str.Length - find_dot > 小數點位置) flag = true;
            }
            if (!flag) str = str + "7";
            if (有負號) str = "-" + str;
            return str;
        }
        string but_8(string str)
        {
            bool 有負號 = false;
            if (str.IndexOf("-") == 0)
            {
                str = str.Substring(1);
                有負號 = true;
            }
            int find_dot = str.IndexOf(".");
            if (str == but_CE()) str = "";
            bool flag = false;
            if (find_dot > 0)
            {
                if (str.Length - find_dot > 小數點位置) flag = true;
            }
            if (!flag) str = str + "8";
            if (有負號) str = "-" + str;
            return str;
        }
        string but_9(string str)
        {
            bool 有負號 = false;
            if (str.IndexOf("-") == 0)
            {
                str = str.Substring(1);
                有負號 = true;
            }
            int find_dot = str.IndexOf(".");
            if (str == but_CE()) str = "";
            bool flag = false;
            if (find_dot > 0)
            {
                if (str.Length - find_dot > 小數點位置) flag = true;
            }
            if (!flag) str = str + "9";
            if (有負號) str = "-" + str;
            return str;
        }
        string but_Dot(string str)
        {
            bool 有負號 = false;
            if (str.IndexOf("-") == 0)
            {
                str = str.Substring(1);
                有負號 = true;
            }
            if (小數點位置 > 0 && str.Length > 0)
            {
                int find_dot = str.IndexOf(".");
                if (find_dot > 0)
                {
                    if (find_dot != str.Length) str = str.Substring(0, find_dot + 1);
                }
                else str = str + ".";
            }
            if (有負號) str = "-" + str;
            return str;
        }
        string but_Backspace(string str)
        {
            if (str.Length > 0) str = str.Remove(str.Length - 1);
            return str;
        }
        string but_正負(string str)
        {
            if(str.Length > 0)
            {
                string first_str;
                if (str.Length > 1) first_str = str.Remove(1);
                else first_str = str;
                if (first_str == "+") str = "-" + str.Substring(1);
                else if (first_str == "-") str = "+" + str.Substring(1);
                else str = "-" + str;
            }    
            return str;
        }
        string but_Enter(string str)
        {
            int find_dot = str.IndexOf(".");
            if (str.Length == 0) str = "0";
            if (小數點位置 > 0)
            {
                if (find_dot < 0)
                {
                    str = str + ".";
                    find_dot = str.Length - 1;
                }
                int 補零數量 = 小數點位置 - (str.Length  - find_dot);
                for(int i = 0 ; i <= 補零數量 ; i++)
                {
                    str = str + "0";
                }
            }
            Enter = true;
            return str;
        }
        bool 有按下按鈕過 = false;
        int cnt_push = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!textBox_Value.Focused) textBox_Value.Focus();
            if (Beep_Voice && 音效)
            {
                System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
                sp.Stream = Resource1.BEEP;
                sp.Play();        
                Beep_Voice = false;
                cnt_Beep_Voice = 0;
            }
            cnt_Beep_Voice++;


            cnt_push = 0;
            foreach (MyUI.ExButton exButton_temp in List_ExButton)
            {
                exButton_temp.Run();
                if (exButton_temp.Load_WriteState())
                {
                    cnt_push++;
                    if (!有按下按鈕過)
                    {
                        string temp = textBox_Value.Text;
                        if (exButton_temp == exButton_0)
                        {
                            if (flag_Init_Text) temp = but_CE();
                            temp = but_0(temp);
                            if (cnt_Beep_Voice >= 1) Beep_Voice = true;
                        }
                        else if (exButton_temp == exButton_1)
                        {
                            if (flag_Init_Text) temp = but_CE();
                            temp = but_1(temp);
                            if (cnt_Beep_Voice >= 1) Beep_Voice = true;
                        }
                        else if (exButton_temp == exButton_2)
                        {
                            if (flag_Init_Text) temp = but_CE();
                            temp = but_2(temp);
                            if (cnt_Beep_Voice >= 1) Beep_Voice = true;
                        }
                        else if (exButton_temp == exButton_3)
                        {
                            if (flag_Init_Text) temp = but_CE();
                            temp = but_3(temp);
                            if (cnt_Beep_Voice >= 1) Beep_Voice = true;
                        }
                        else if (exButton_temp == exButton_4)
                        {
                            if (flag_Init_Text) temp = but_CE();
                            temp = but_4(temp);
                            if (cnt_Beep_Voice >= 1) Beep_Voice = true;
                        }
                        else if (exButton_temp == exButton_5)
                        {
                            if (flag_Init_Text) temp = but_CE();
                            temp = but_5(temp);
                            if (cnt_Beep_Voice >= 1) Beep_Voice = true;
                        }
                        else if (exButton_temp == exButton_6)
                        {
                            if (flag_Init_Text) temp = but_CE();
                            temp = but_6(temp);
                            if (cnt_Beep_Voice >= 1) Beep_Voice = true;
                        }
                        else if (exButton_temp == exButton_7)
                        {
                            if (flag_Init_Text) temp = but_CE();
                            temp = but_7(temp);
                            if (cnt_Beep_Voice >= 1) Beep_Voice = true;
                        }
                        else if (exButton_temp == exButton_8)
                        {
                            if (flag_Init_Text) temp = but_CE();
                            temp = but_8(temp);
                            if (cnt_Beep_Voice >= 1) Beep_Voice = true;
                        }
                        else if (exButton_temp == exButton_9)
                        {
                            if (flag_Init_Text) temp = but_CE();
                            temp = but_9(temp);
                            if (cnt_Beep_Voice >= 1) Beep_Voice = true;
                        }
                        else if (exButton_temp == exButton_Dot)
                        {
                            if (flag_Init_Text) temp = but_CE();
                            temp = but_Dot(temp);
                            if (cnt_Beep_Voice >= 1) Beep_Voice = true;
                        }
                        else if (exButton_temp == exButton_Backspace)
                        {
                            if (flag_Init_Text) temp = but_CE();
                            temp = but_Backspace(temp);
                            if (cnt_Beep_Voice >= 1) Beep_Voice = true;
                        }
                        else if (exButton_temp == exButton_CE)
                        {
                            temp = but_CE();
                            if (cnt_Beep_Voice >= 1) Beep_Voice = true;
                        }
                        else if (exButton_temp == exButton_正負)
                        {
                            if (flag_Init_Text) temp = but_CE();
                            temp = but_正負(temp);
                            if (cnt_Beep_Voice >= 1) Beep_Voice = true;
                        }                 
                        if(temp.Length > 1)
                        {
                            if (temp.Remove(1) == "0" && temp.IndexOf(".") == -1)
                            {
                                temp = temp.Substring(1);
                            }
                        }     
                        if (exButton_temp == exButton_Enter)
                        {
                            value = but_Enter(temp);
                            this.Close();
                        }
                        textBox_Value.Text = temp;
                    }
                    有按下按鈕過 = true;
                    flag_Init_Text = false;
                }
            }
            if (cnt_push == 0) 有按下按鈕過 = false;
            if(keys.Key_Enter)
            {
                value = but_Enter(textBox_Value.Text);
                Enter = true;
                this.Close();
            }
            if(keys.Key_Esc)
            {
                value = "";
                this.Close();
            }
        }

        private void 數字鍵盤_FormClosing(object sender, FormClosingEventArgs e)
        {
            視窗已建立 = false;
            timer.Dispose();
            _數字鍵盤 = null;
        }
        private void 數字鍵盤_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void 數字鍵盤_KeyDown(object sender, KeyEventArgs e)
        {
        
        }
        private void 數字鍵盤_KeyUp(object sender, KeyEventArgs e)
        {
       
        }
        private void textBox_Value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.D0)
            {
                if (flag_Init_Text) textBox_Value.Text = but_CE();
                textBox_Value.Text = but_0(textBox_Value.Text);
                if (cnt_Beep_Voice >= 1) Beep_Voice = true;
            }
            else if (e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.D1)
            {
                if (flag_Init_Text) textBox_Value.Text = but_CE();
                textBox_Value.Text = but_1(textBox_Value.Text);
                if (cnt_Beep_Voice >= 1) Beep_Voice = true;
            }
            else if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2)
            {
                if (flag_Init_Text) textBox_Value.Text = but_CE();
                textBox_Value.Text = but_2(textBox_Value.Text);
                if (cnt_Beep_Voice >= 1) Beep_Voice = true;
            }
            else if (e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.D3)
            {
                if (flag_Init_Text) textBox_Value.Text = but_CE();
                textBox_Value.Text = but_3(textBox_Value.Text);
                if (cnt_Beep_Voice >= 1) Beep_Voice = true;
            }
            else if (e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.D4)
            {
                if (flag_Init_Text) textBox_Value.Text = but_CE();
                textBox_Value.Text = but_4(textBox_Value.Text);
                if (cnt_Beep_Voice >= 1) Beep_Voice = true;
            }
            else if (e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.D5)
            {
                if (flag_Init_Text) textBox_Value.Text = but_CE();
                textBox_Value.Text = but_5(textBox_Value.Text);
                if (cnt_Beep_Voice >= 1) Beep_Voice = true;
            }
            else if (e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.D6)
            {
                if (flag_Init_Text) textBox_Value.Text = but_CE();
                textBox_Value.Text = but_6(textBox_Value.Text);
                if (cnt_Beep_Voice >= 1) Beep_Voice = true;
            }
            else if (e.KeyCode == Keys.NumPad7 || e.KeyCode == Keys.D7)
            {
                if (flag_Init_Text) textBox_Value.Text = but_CE();
                textBox_Value.Text = but_7(textBox_Value.Text);
                if (cnt_Beep_Voice >= 1) Beep_Voice = true;
            }
            else if (e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.D8)
            {
                if (flag_Init_Text) textBox_Value.Text = but_CE();
                textBox_Value.Text = but_8(textBox_Value.Text);
                if (cnt_Beep_Voice >= 1) Beep_Voice = true;
            }
            else if (e.KeyCode == Keys.NumPad9 || e.KeyCode == Keys.D9)
            {
                if (flag_Init_Text) textBox_Value.Text = but_CE();
                textBox_Value.Text = but_9(textBox_Value.Text);
                if (cnt_Beep_Voice >= 1) Beep_Voice = true;
            }
            else if (e.KeyCode == Keys.OemPeriod || e.KeyCode == Keys.Decimal)
            {
                if (flag_Init_Text) textBox_Value.Text = but_CE();
                textBox_Value.Text = but_Dot(textBox_Value.Text);
                if (cnt_Beep_Voice >= 1) Beep_Voice = true;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (flag_Init_Text) textBox_Value.Text = but_CE();
                textBox_Value.Text = but_Backspace(textBox_Value.Text);
                if (cnt_Beep_Voice >= 1) Beep_Voice = true;
            }
            else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Subtract)
            {
                if (flag_Init_Text) textBox_Value.Text = but_CE();
                textBox_Value.Text = but_正負(textBox_Value.Text);
                if (cnt_Beep_Voice >= 1) Beep_Voice = true;
            }
            else if (e.KeyCode == Keys.Escape )
            {
                value = "";
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                value = but_Enter(textBox_Value.Text);
                Enter = true;
                this.Close();
            }
            flag_Init_Text = false;
        }
        private void textBox_Value_KeyUp(object sender, KeyEventArgs e)
        {
        
        }
    }
}
