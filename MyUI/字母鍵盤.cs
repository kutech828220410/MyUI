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
    public partial class 字母鍵盤 : Form
    {
        private bool Shift = true;
        private bool Beep_Voice = false;
        private Basic.Keyboard keys = new Basic.Keyboard();
        private List<ExButton> List_ExButton = new List<ExButton>();
        private static 字母鍵盤 _字母鍵盤;
        private static readonly object synRoot = new object();
        public static bool 視窗已建立 = false;
        public static bool 音效 = false;
        public static String value = "";
        public static int 字節數 = 0;
        public static bool 密碼字元 = false;
        public static string InitText = "";
        private char[] Word_L = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private char[] Word_S = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static 字母鍵盤 GetForm()
        {
            lock (synRoot)
            {
                if (_字母鍵盤 == null)
                {
                    _字母鍵盤 = new 字母鍵盤();
                }
                視窗已建立 = true;
            }
            return _字母鍵盤;
        }
        public 字母鍵盤()
        {
            InitializeComponent();
            exButton_1.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_2.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_3.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_4.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_5.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_6.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_7.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_8.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_9.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_0.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_A.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_B.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_C.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_D.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_E.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_F.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_G.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_H.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_I.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_J.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_K.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_L.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_M.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_N.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_O.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_P.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_Q.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_R.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_S.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_T.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_U.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_V.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_W.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_X.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_Y.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_Z.btnClick += new System.EventHandler(this.exButton_btnClick);

            exButton1.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton2.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton3.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton4.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton5.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton6.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton7.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton8.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton9.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton10.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton11.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton12.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton13.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton14.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton15.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton16.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton17.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton18.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton19.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton20.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton21.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton22.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton23.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton24.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton25.btnClick += new System.EventHandler(this.exButton_btnClick);


            exButton_Shift.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_Enter.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_Backspace.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_Clear.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_ESC.btnClick += new System.EventHandler(this.exButton_btnClick);
            exButton_Space.btnClick += new System.EventHandler(this.exButton_btnClick);
        }
        bool 有按下按鈕過 = false;
        int cnt_push = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            cnt_push = 0;
            foreach (MyUI.ExButton exButton in List_ExButton)
            {
                exButton.Run();
                if (exButton.Load_WriteState())
                {
                    cnt_push++;
                    if (!有按下按鈕過)
                    {
                        if (exButton == exButton_Shift)
                        {
                            Shift = !Shift;
                            ChangeTextShift(Shift);
                        }
                        else if (exButton == exButton_Backspace)
                        {
                            if (textBox_Value.Text.Length > 0)
                            {
                                textBox_Value.Text = textBox_Value.Text.Remove(textBox_Value.Text.Length-1);
                            }
                        }
                        else if (exButton == exButton_Clear)
                        {
                            textBox_Value.Text = "";
                        }     
                        else if (exButton == exButton_Enter)
                        {
                            InitText = textBox_Value.Text;
                            this.Close();
                        }
                        else if (exButton == exButton_ESC)
                        {
                            this.Close();
                        }
                        else if (exButton.OFF_文字內容.Length == 1)
                        {
                            if (textBox_Value.Text.IndexOf((char)32) == 0)
                            {
                                textBox_Value.Text= textBox_Value.Text.Substring(1);
                            }
                            if (textBox_Value.Text.Length < 字節數) textBox_Value.Text += exButton.OFF_文字內容;
  
                        }
                    }
                    有按下按鈕過 = true;
                    Beep_Voice = true;
                }
            }
            if (keys.Key_Enter)
            {
                InitText = textBox_Value.Text;
                this.Close();
            }
            if (keys.Key_Esc)
            {
                this.Close();
            }

            if (cnt_push == 0) 有按下按鈕過 = false;
            if (Beep_Voice && 音效)
            {
                System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
                sp.Stream = Resource1.BEEP;
                sp.Play();
                Beep_Voice = false;
            }
        }

        private void 字母鍵盤_Load(object sender, EventArgs e)
        {
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
            List_ExButton.Add(exButton_A);
            List_ExButton.Add(exButton_B);
            List_ExButton.Add(exButton_C);
            List_ExButton.Add(exButton_D);
            List_ExButton.Add(exButton_E);
            List_ExButton.Add(exButton_F);
            List_ExButton.Add(exButton_G);
            List_ExButton.Add(exButton_H);
            List_ExButton.Add(exButton_I);
            List_ExButton.Add(exButton_J);
            List_ExButton.Add(exButton_K);
            List_ExButton.Add(exButton_L);
            List_ExButton.Add(exButton_M);
            List_ExButton.Add(exButton_N);
            List_ExButton.Add(exButton_O);
            List_ExButton.Add(exButton_P);
            List_ExButton.Add(exButton_Q);
            List_ExButton.Add(exButton_R);
            List_ExButton.Add(exButton_S);
            List_ExButton.Add(exButton_T);
            List_ExButton.Add(exButton_U);
            List_ExButton.Add(exButton_V);
            List_ExButton.Add(exButton_W);
            List_ExButton.Add(exButton_X);
            List_ExButton.Add(exButton_Y);
            List_ExButton.Add(exButton_Z);
            List_ExButton.Add(exButton_Space);
            List_ExButton.Add(exButton_Backspace);
            List_ExButton.Add(exButton_Enter);
            List_ExButton.Add(exButton_Shift);
            List_ExButton.Add(exButton_Clear);
            List_ExButton.Add(exButton_ESC);
            List_ExButton.Add(exButton1);
            List_ExButton.Add(exButton2);
            List_ExButton.Add(exButton3);
            List_ExButton.Add(exButton4);
            List_ExButton.Add(exButton5);
            List_ExButton.Add(exButton6);
            List_ExButton.Add(exButton7);
            List_ExButton.Add(exButton8);
            List_ExButton.Add(exButton9);
            List_ExButton.Add(exButton10);
            List_ExButton.Add(exButton11);
            List_ExButton.Add(exButton12);
            List_ExButton.Add(exButton13);
            List_ExButton.Add(exButton14);
            List_ExButton.Add(exButton15);
            List_ExButton.Add(exButton16);
            List_ExButton.Add(exButton17);
            List_ExButton.Add(exButton18);
            List_ExButton.Add(exButton19);
            List_ExButton.Add(exButton20);
            List_ExButton.Add(exButton21);
            List_ExButton.Add(exButton22);
            List_ExButton.Add(exButton23);
            List_ExButton.Add(exButton24);
            List_ExButton.Add(exButton25);

            ChangeTextShift(Shift);
            textBox_Value.UseSystemPasswordChar = 密碼字元;
            textBox_Value.Text = InitText;
            timer.Enabled = true;
        }
        void ChangeTextShift(bool shift)
        {
            int start_index = 10;
            string temp = "";
            for (int i = 0; i < 26; i++)
            {
                if (shift) temp = Word_L[i].ToString();
                else temp = Word_S[i].ToString();
                List_ExButton[i + start_index].ON_文字內容 = temp;
                List_ExButton[i + start_index].OFF_文字內容 = temp;
                List_ExButton[i + start_index].文字鎖住 = true;
            }
        }

        private void exButton_btnClick(object sender, EventArgs e)
        {
         
         
        }

        private void 字母鍵盤_FormClosing(object sender, FormClosingEventArgs e)
        {
            視窗已建立 = false;
            timer.Dispose();
            _字母鍵盤 = null;
        }
    }
}
