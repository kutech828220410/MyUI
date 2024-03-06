using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Reflection;
using LadderConnection;
using Basic;
using System.Runtime.InteropServices;
namespace MyUI
{
    [System.Drawing.ToolboxBitmap(typeof(PLC_ScreenPage), "PLC_ScreenPage.bmp")]
    public partial class PLC_ScreenPage : TabControl
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }
        public delegate void TabChangeEventHandler(string PageText);
        public event TabChangeEventHandler TabChangeEvent;

        private LowerMachine PLC;
        Form form;
        public string PageText = "";
        #region 自訂屬性

        private string _控制位址 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 控制位址
        {
            get { return _控制位址; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "D" || temp == "R") divice_OK = true;
                }

                if (divice_OK) _控制位址 = value;
                else _控制位址 = "";
            }
        }
        private string _狀態位址 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 狀態位址
        {
            get { return _狀態位址; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "D" || temp == "R" ) divice_OK = true;
                }

                if (divice_OK) _狀態位址 = value;
                else _狀態位址 = "";
            }
        }
        public enum TabVisibleEnum : int
        {
            顯示 =0,隱藏
        }
        TabVisibleEnum _顯示標籤列 = new TabVisibleEnum();
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public TabVisibleEnum 顯示標籤列
        {
            get
            {
                return _顯示標籤列;
            }
            set
            {
                if (value == TabVisibleEnum.隱藏)
                {
                    this.SizeMode = TabSizeMode.Fixed;
                    this.ItemSize = new Size(0, 1);
                }
                else if (value == TabVisibleEnum.顯示)
                {
                    this.SizeMode = TabSizeMode.Normal;
                    this.ItemSize = new Size(54, 21);
                }
                _顯示標籤列 = value;
            }
        }
 

        private int _顯示頁面 = 0;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        [TypeConverter(typeof(NameConverter))]
        public int 顯示頁面
        {
            get 
            {
                return _顯示頁面; 
            }
            set
            {
                int NumOfPages = this.TabPages.Count;
              
                if (value >= NumOfPages) value = NumOfPages - 1;
                if (value < 0) value = 0;

                if(this.InvokeRequired)
                {
                    SelecteTabDelegate Delegate = new SelecteTabDelegate(SelecteTab);
                    BeginInvoke(Delegate, value);
                }
                else
                {
                    SelecteTab(value);
                }
              
                _顯示頁面 = value;
            }
        }
        static private List<int> List頁面 = new List<int>();
        public class NameConverter : Int32Converter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(List頁面);
            }
            //(選擇性) 讓使用者能輸入下拉式清單以外的值
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return false;
            }
        }



        public enum WordLengthEnum : int
        {
            單字元, 雙字元
        }
        [ReadOnly(true), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public WordLengthEnum 字元長度 { get; set; }

        private Color backColor = Color.White;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Color BackColor
        {
            get
            {
                return backColor;
            }
            set
            {
                backColor = value;
            }
        }
        private Color foreColor = Color.Black;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Color ForekColor
        {
            get
            {
                return foreColor;
            }
            set
            {
                foreColor = value;
            }
        }
        private Color tabBackColor = Color.White;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Color TabBackColor
        {
            get
            {
                return tabBackColor;
            }
            set
            {
                tabBackColor = value;
            }
        }
        #endregion

        private int page_index = 0;
        private int page_index_buf = 0;

        public PLC_ScreenPage()
        {
            InitializeComponent();
            this.DrawItem += PLC_ScreenPage_DrawItem;
            //this.DrawMode = TabDrawMode.OwnerDrawFixed;
            //this.SizeMode = TabSizeMode.Fixed;
            // this.Layout += PLC_ScreenPage_Layout;
            this.SelectedIndexChanged += PLC_ScreenPage_SelectedIndexChanged;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

      

        delegate void SelecteTabDelegate(int page);
        void SelecteTab(int page)
        {
            this.SelectedIndex = page;
        }
        public void SelecteTabText(String str)
        {
            this.Invoke(new Action(delegate 
            {
                int page = 0;
                foreach (TabPage tabPage in this.TabPages)
                {
                    if (tabPage.Text == str)
                    {
                        this.SelectedIndex = page;
                        if (_控制位址 != "" && _控制位址 != null && PLC != null) PLC.properties.Device.Set_Device(_控制位址, page);
                        break;
                    }
                    page++;
                }
            }));
         
        }
        public bool Set_PLC(LowerMachine pLC)
        {
            if (pLC != null)
            {
                this.PLC = pLC;
                return true;
            }
            return false;
        }
        public void Run(LowerMachine pLC , Form form)
        { 
            if (PLC == null)
            {
                this.form = form;
               
                if (Set_PLC(pLC))
                {
                    PLC_ScreenPage_Layout(this, new LayoutEventArgs(this, ""));
                    PLC.Add_UI_Method(Run);
         
                }
            }
        }
        public void Run()
        {
            if (_控制位址.StringIsEmpty() || _狀態位址.StringIsEmpty())
            {
           
            }
            else
            {
                if (_控制位址 != "" && _控制位址 != null && PLC != null)
                {
                    object value = 0;
                    if (字元長度 == WordLengthEnum.單字元)
                    {
                        PLC.properties.Device.Get_Device(_控制位址, out value);
                    }
                    else if (字元長度 == WordLengthEnum.雙字元)
                    {
                        value = PLC.properties.Device.Get_DoubleWord(_控制位址);
                    }
                    if (value is int) 顯示頁面 = (int)value;
                }
                if (_狀態位址 != "" && _狀態位址 != null && PLC != null)
                {
                    int value = 顯示頁面;
                    if (字元長度 == WordLengthEnum.單字元)
                    {
                        PLC.properties.Device.Set_Device(_狀態位址, value);
                    }
                    else if (字元長度 == WordLengthEnum.雙字元)
                    {
                        PLC.properties.Device.Set_DoubleWord(_狀態位址, Convert.ToInt64(value));
                    }
                }
            }
            
            this.Invoke(new Action(delegate { this.PageText = this.SelectedTab.Text; }));
        }

        private void PLC_ScreenPage_ControlAdded(object sender, ControlEventArgs e)
        {
            List頁面.Clear();
            for(int i = 0 ; i < this.TabPages.Count ; i ++)
            {
                List頁面.Add(i); 
            }
        }
        private void PLC_ScreenPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            PLC_ScreenPage_Layout(this, new LayoutEventArgs(this, ""));
            if (TabChangeEvent != null)
            {
                this.Invoke(new Action(delegate
                {
                    this.PageText = this.SelectedTab.Text;
                    TabChangeEvent(this.PageText);
                }));
            }

        }
        private void PLC_ScreenPage_Layout(object sender, LayoutEventArgs e)
        {

            using (Graphics g = this.CreateGraphics())
            {
                StringFormat StrFormat = new StringFormat();
                //設定文字樣式
                StrFormat.LineAlignment = StringAlignment.Center;//垂直置中
                StrFormat.Alignment = StringAlignment.Center;//水平置中   
                Font New_Font = this.Font;
                SolidBrush Brush_Font = new SolidBrush(foreColor);//標籤字體顏色
                SolidBrush Brush_Tab = new SolidBrush(tabBackColor);//標籤預設顏色
                SolidBrush Brush_Back = new SolidBrush(backColor);//標籤預設顏色

                g.FillRectangle(Brush_Back, this.Parent.ClientRectangle);

                //繪製標籤背景
                for (int i = 0; i < this.TabPages.Count; i++)
                {
                    //獲取標籤區域
                    Rectangle recChild = this.GetTabRect(i);
                    //設定標籤顏色要實現的區域
                    g.FillRectangle(Brush_Tab, recChild);
                    //設定標籤文字&顏色
                    g.DrawString(this.TabPages[i].Text, New_Font, Brush_Font, recChild, StrFormat);
                }
            }

        }
        private void PLC_ScreenPage_DrawItem(object sender, DrawItemEventArgs e)
        {
            StringFormat StrFormat = new StringFormat();
            //設定文字樣式
            StrFormat.LineAlignment = StringAlignment.Center;//垂直置中
            StrFormat.Alignment = StringAlignment.Center;//水平置中   
            Font New_Font = this.Font;
            SolidBrush Brush_Font = new SolidBrush(foreColor);//標籤字體顏色
            SolidBrush Brush_Tab = new SolidBrush(tabBackColor);//標籤預設顏色
            SolidBrush Brush_Back = new SolidBrush(backColor);//標籤預設顏色

            //繪置元件背景
            e.Graphics.FillRectangle(Brush_Back, this.ClientRectangle);

            //繪製標籤背景
            for (int i = 0; i < this.TabPages.Count; i++)
            {
                //獲取標籤區域
                Rectangle recChild = this.GetTabRect(i);
                //設定標籤顏色要實現的區域
                e.Graphics.FillRectangle(Brush_Tab, recChild);
                //設定標籤文字&顏色
                e.Graphics.DrawString(this.TabPages[i].Text, New_Font, Brush_Font, recChild, StrFormat);
            }
            //繪製被選取的標籤背景
            if (e.Index == this.SelectedIndex)
            {
                Rectangle recChild = this.GetTabRect(this.SelectedIndex);
                e.Graphics.FillRectangle(Brush_Tab, recChild);
                e.Graphics.DrawString(this.TabPages[this.SelectedIndex].Text, New_Font, Brush_Font, recChild, StrFormat);
            }

        }

    }
}
