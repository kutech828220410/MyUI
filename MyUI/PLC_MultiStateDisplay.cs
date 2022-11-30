using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LadderConnection;
using System.Media;
using System.Text.RegularExpressions;
namespace MyUI
{
    [System.Drawing.ToolboxBitmap(typeof(PLC_MultiStateDisplay), "PLC_MultiStateDisplay.bmp")]
    [Serializable]
    public partial class PLC_MultiStateDisplay : UserControl
    {
        private MyTimer myTimer_RefreshTime = new MyTimer();
        private int borderRadius = 0;
        public LowerMachine PLC;
        private Point BasicPoint;
        private Size BasicSize;
        private Basic.MyConvert MyConvert = new Basic.MyConvert();
        private string string_state = "";
        #region 隱藏屬性
        [Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        [Browsable(false)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }
        [Browsable(false)]
        public override ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
                base.BackgroundImageLayout = value;
            }
        }
        [Browsable(false)]
        public override Cursor Cursor
        {
            get
            {
                return base.Cursor;
            }
            set
            {
                base.Cursor = value;
            }
        }
        [Browsable(false)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }
        [Browsable(false)]
        public override System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }
        [Browsable(false)]
        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                base.RightToLeft = value;
            }
        }

        #endregion
        #region 自訂屬性
        [Category("RJ Code Advance")]
        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }
            set
            {
                if (value <= this.Height)
                {
                    this.borderRadius = value;
                }
                else
                {
                    this.borderRadius = this.Height;
                }
                this.Invalidate();
            }
        }
        private ContentAlignment _文字對齊位置 = ContentAlignment.MiddleCenter;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public ContentAlignment 文字對齊位置
        {
            get
            {
                return _文字對齊位置;
            }
            set
            {
                _文字對齊位置 = value;
            }
        }
        public class TextValue
        {
            private string _Name = "S4500";
            public string Name
            {
                get
                {
                    return _Name;
                }
                set
                {
                    value = value.ToUpper();
                    bool divice_OK = false;
                    if (LadderProperty.DEVICE.TestDevice(value))
                    {
                        string temp = value.Remove(1);
                        if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                    }

                    if (divice_OK) _Name = value;
                    else _Name = "";
                }
            }
            private string _Text = "顯示內容";
            public string Text
            {
                get
                {
                    return _Text;
                }
                set
                {
                    _Text = value;
                }
            }
            private Font _字體 = new Font("新細明體", 12);
            public Font 字體
            {
                get
                {
                    return _字體;
                }
                set
                {
                    _字體 = value;
                }
            }
            private Color _文字顏色 = Color.FromArgb(255, 255, 255);
            public Color 文字顏色
            {
                get
                {
                    return _文字顏色;
                }
                set
                {
                    _文字顏色 = value;
                }
            }
            public enum Alignment
            {
                Left, Center, Right
            }
            private Alignment _文字對齊方式 = Alignment.Left;
            public Alignment 文字對齊方式
            {
                get
                {
                    return _文字對齊方式;
                }
                set
                {
                    _文字對齊方式 = value;
                }
            }
            private bool _自定義參數 = false;
            public bool 自定義參數
            {
                get
                {
                    return _自定義參數;
                }
                set
                {
                    _自定義參數 = value;
                }
            }
        }
        private List<TextValue> _TextValue = new List<TextValue>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [ReadOnly(false), Browsable(true), Category("自訂集合"), Description("自訂集合"), DefaultValue("")]
        public List<TextValue> 狀態內容
        {
            get
            {
                return _TextValue;
            }
            set
            {

                _TextValue = value;

            }
        }

        Font _顯示字體 = new Font("新細明體", 12);
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Font 顯示字體
        {
            get
            {
                return _顯示字體;
            }
            set
            {
                pictureBox.Font = value;
                _顯示字體 = value;
            }
        }
        Color _背景顏色 = Color.Black;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Color 背景顏色
        {
            get
            {
                this.BackColor = _背景顏色;
                pictureBox.BackColor = _背景顏色;
                return _背景顏色;
            }
            set
            {
                this.BackColor = value;
                pictureBox.BackColor = value;
                _背景顏色 = value;
            }
        }
        Color _字體顏色 = Color.White;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Color 字體顏色
        {
            get
            {
                pictureBox.ForeColor = _字體顏色;
                return _字體顏色;
            }
            set
            {
                pictureBox.ForeColor = value;
                _字體顏色 = value;
            }
        }

        private int refreshTime = 50;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int RefreshTime
        {
            get
            {
                return refreshTime;
            }
            set
            {
                refreshTime = value;
            }
        }

        #endregion
        public PLC_MultiStateDisplay()
        {
            InitializeComponent();


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
        public virtual void Run(LowerMachine pLC)
        {
            if (PLC == null)
            {
                if (Set_PLC(pLC))
                {
                    this.Invoke(new Action(delegate
                    {
                        pictureBox.Text = string_state;
                        BasicSize = pictureBox.Size;
                        BasicPoint = new Point(pictureBox.Padding.Left, pictureBox.Padding.Top);
                    }));
                    foreach (TextValue _TextValue in this.狀態內容)
                    {
                        PLC.properties.Device.Set_Device(_TextValue.Name, "*狀態顯示-" + _TextValue.Text);
                    }
                    PLC.Add_UI_Method(Run);
                }
            }
        }
        object value = new object();
        public void SetTextValue(string Adress, string Value)
        {
            foreach (TextValue _TextValue in this.狀態內容)
            {
                if (_TextValue.Name == Adress)
                {
                    _TextValue.Text = Value;
                }
            }
        }
        string[] string_state_Array;
        string[] string_currentline_Array;
       
        static readonly string 字體字串 = "<Font>";
        static readonly string 文字顏色字串 = "<Color>";
        static readonly string 文字對齊方式字串 = "<Alignment>";
        static readonly string 指令開頭字串 = "</t>";

        public virtual void Run()
        {
            string_state = "";
            foreach (TextValue _TextValue in this.狀態內容)
            {

                if (PLC != null && _TextValue.Name != null && _TextValue.Name != "")
                {
                    if (LadderProperty.DEVICE.TestDevice(_TextValue.Name))
                    {
                        PLC.properties.Device.Get_Device(_TextValue.Name, out value);
                    }
                    if (value is bool)
                    {
                        if ((bool)value)
                        {
                            if (!_TextValue.自定義參數)
                            {
                                string_state += 指令開頭字串;
                                string_state += string.Format(文字對齊方式字串 + "{0}" + 文字對齊方式字串, _TextValue.文字對齊方式.ToString());
                                string_state += string.Format(字體字串 + "{0},{1}" + 字體字串, _TextValue.字體.Name, _TextValue.字體.Size.ToString());
                                string_state += string.Format(文字顏色字串 + "{0},{1},{2}" + 文字顏色字串, _TextValue.文字顏色.R.ToString(), _TextValue.文字顏色.G.ToString(), _TextValue.文字顏色.B.ToString());
                            }
                            string_state += (_TextValue.Text + "\r\n");
                        }
                    }
                }
            }
            if (!myTimer_RefreshTime.IsTimeOut())
            {
                myTimer_RefreshTime.StartTickTime(refreshTime);
            }
            else
            {
                BasicPoint = new Point();
                BasicPoint.Y += borderRadius / 2;
                string_state_Array = MyConvert.分解分隔號字串(string_state, "\r\n");
                if(this.CanSelect)
                {
                    Bitmap bitmap = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
                    using (Graphics Graphics_Draw_Bitmap = Graphics.FromImage(bitmap))
                    {
                        Graphics_Draw_Bitmap.FillRectangle(new SolidBrush(背景顏色), new RectangleF(new PointF(0, 0), BasicSize));
                        for (int i = 0; i < string_state_Array.Length; i++)
                        {
                            List_RowStringValues.Clear();
                            SizeF 文字面積 = new SizeF();
                            分析文字對齊方式(ref string_state_Array[i], ref RowStringValues.Alignment);
                            string_currentline_Array = Regex.Split(string_state_Array[i], 指令開頭字串, RegexOptions.IgnoreCase);
                            for (int k = 0; k < string_currentline_Array.Length; k++)
                            {
                                if (string_currentline_Array[k] != "")
                                {
                                    分析文字字體(ref string_currentline_Array[k], ref _顯示字體);
                                    分析文字顏色(ref string_currentline_Array[k], ref _字體顏色);
                                    RowStringValues rowStringValues = new RowStringValues();
                                    rowStringValues.font = _顯示字體;
                                    rowStringValues.color = _字體顏色;
                                    rowStringValues.str = string_currentline_Array[k];
                                    List_RowStringValues.Add(rowStringValues);
                                }
                            }

                            SizeF AllstrSize = new SizeF();
                            int MaxHeight = 0;
                            for (int k = 0; k < List_RowStringValues.Count; k++)
                            {
                                文字面積 = TextRenderer.MeasureText(List_RowStringValues[k].str, List_RowStringValues[k].font);
                                AllstrSize.Width += 文字面積.Width;
                                List_RowStringValues[k].PaintSize = 文字面積;
                                if (文字面積.Height > MaxHeight) MaxHeight = (int)文字面積.Height;
                            }
                            if (RowStringValues.Alignment == TextValue.Alignment.Left)
                            {
                                BasicPoint.X = 0;
                            }
                            else if (RowStringValues.Alignment == TextValue.Alignment.Center)
                            {
                                BasicPoint.X = (int)(BasicSize.Width / 2 - AllstrSize.Width / 2);
                            }
                            else if (RowStringValues.Alignment == TextValue.Alignment.Right)
                            {
                                BasicPoint.X = (int)(BasicSize.Width - AllstrSize.Width);
                            }
                            for (int k = 0; k < List_RowStringValues.Count; k++)
                            {
                                Point po = new Point((int)(BasicPoint.X), (int)BasicPoint.Y);
                                Graphics_Draw_Bitmap.DrawString(List_RowStringValues[k].str, List_RowStringValues[k].font, new SolidBrush(List_RowStringValues[k].color), po);
                                BasicPoint.X += (int)List_RowStringValues[k].PaintSize.Width;
                            }
                            BasicPoint.Y += MaxHeight;
                        }
                        if (pictureBox.Image != null) pictureBox.Image.Dispose();
                        pictureBox.Image = bitmap;
                    }
             
                }


            }
           
          
         
        }
        private List<RowStringValues> List_RowStringValues = new List<RowStringValues>();
        private class RowStringValues
        {
            public static TextValue.Alignment Alignment = TextValue.Alignment.Left;
            public String str = "";
            public Font font = new Font("新細名體", 14);
            public Color color = Color.Black;
            public SizeF PaintSize = new SizeF();
        }
        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            return path;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);

            if (this.borderRadius > 2)
            {
                using (GraphicsPath pathSurface = this.GetFigurePath(rectSurface, this.borderRadius))
                using (GraphicsPath pathBorder = this.GetFigurePath(rectBorder, this.borderRadius - 1F))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                {
                    this.Region = new Region(pathSurface);
                    pevent.Graphics.DrawPath(penSurface, pathSurface);


                }
            }
            else
            {
                this.Region = new Region(rectSurface);
                if (this.borderRadius >= 1)
                {

                }
            }
        }
        void 分析文字字體(ref string str, ref Font font)
        {
            int temp0 = 0;
            int temp1 = 0;
            temp0 = str.IndexOf(字體字串, 0);
            if (temp0 != -1)
            {
                temp1 = str.IndexOf(字體字串, temp0 + 字體字串.Length);
                if (temp1 != -1)
                {
                    string str_Font_buf = str.Substring(temp0 + 字體字串.Length, temp1 - temp0 - 字體字串.Length);
                    string[] str_Font_Array = MyConvert.分解分隔號字串(str_Font_buf, ",");
                    if (str_Font_Array.Length == 2)
                    {
                        float FontSize = 0;
                        if (float.TryParse(str_Font_Array[1], out FontSize))
                        {
                            font = new Font(str_Font_Array[0], FontSize);
                            str = str.Remove(temp0, str_Font_buf.Length + 字體字串.Length * 2);
                        }
                    }
                }
            }
        }
        void 分析文字顏色(ref string str, ref Color color)
        {
            int temp0 = 0;
            int temp1 = 0;
            temp0 = str.IndexOf(文字顏色字串, 0);
            if (temp0 != -1)
            {
                temp1 = str.IndexOf(文字顏色字串, temp0 + 文字顏色字串.Length);
                if (temp1 != -1)
                {
                    string str_buf = str.Substring(temp0 + 文字顏色字串.Length, temp1 - temp0 - 文字顏色字串.Length);
                    string[] str_Array = MyConvert.分解分隔號字串(str_buf, ",");
                    if (str_Array.Length == 3)
                    {
                        byte R = 0;
                        byte G = 0;
                        byte B = 0;
                        if (byte.TryParse(str_Array[0], out R))
                        {
                            if (byte.TryParse(str_Array[1], out G))
                            {
                                if (byte.TryParse(str_Array[2], out B))
                                {
                                    color = Color.FromArgb(255, R, G, B);
                                    str = str.Remove(temp0, str_buf.Length + 文字顏色字串.Length * 2);
                                }
                            }

                        }
                    }
                }
            }
        }
        void 分析文字對齊方式(ref string str, ref TextValue.Alignment Alignment)
        {
            int temp0 = 0;
            int temp1 = 0;
            SizeF 文字面積;
            temp0 = str.IndexOf(文字對齊方式字串, 0);
            if (temp0 != -1)
            {
                temp1 = str.IndexOf(文字對齊方式字串, temp0 + 文字對齊方式字串.Length);
                if (temp1 != -1)
                {
                    string str_buf = str.Substring(temp0 + 文字對齊方式字串.Length, temp1 - temp0 - 文字對齊方式字串.Length);
                    if (str_buf == TextValue.Alignment.Left.ToString())
                    {
                        Alignment = TextValue.Alignment.Left;
                        str = str.Remove(temp0, str_buf.Length + 文字對齊方式字串.Length * 2);
                    }
                    else if (str_buf == TextValue.Alignment.Center.ToString())
                    {
                        Alignment = TextValue.Alignment.Center;
                        str = str.Remove(temp0, str_buf.Length + 文字對齊方式字串.Length * 2);
                    }
                    else if (str_buf == TextValue.Alignment.Right.ToString())
                    {
                        Alignment = TextValue.Alignment.Right;
                        str = str.Remove(temp0, str_buf.Length + 文字對齊方式字串.Length * 2);
                    }
                }
            }
        }

        public string GetFontString(Font font, bool CommandSapce)
        {
            if (CommandSapce)
            {
                return 指令開頭字串 + string.Format(字體字串 + "{0},{1}" + 字體字串, font.Name, font.Size.ToString());
            }
            else
            {
                return string.Format(字體字串 + "{0},{1}" + 字體字串, font.Name, font.Size.ToString());
            }
        }
        public string GetFontColorString(Color color, bool CommandSapce)
        {
            if (CommandSapce)
            {
                return 指令開頭字串 + string.Format(文字顏色字串 + "{0},{1},{2}" + 文字顏色字串, color.R.ToString(), color.G.ToString(), color.B.ToString());
            }
            else
            {
                return string.Format(文字顏色字串 + "{0},{1},{2}" + 文字顏色字串, color.R.ToString(), color.G.ToString(), color.B.ToString());
            }
        }
        public string GetAlignmentString(TextValue.Alignment Alignment)
        {
            return string.Format(文字對齊方式字串 + "{0}" + 文字對齊方式字串, Alignment.ToString());
        }

    }
}
