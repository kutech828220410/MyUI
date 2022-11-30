using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
namespace MyUI
{
    public partial class DateTimeComList : UserControl
    {
        private int start_Year = 2022;
        [Category("RJ Code Advance")]
        public int Start_Year
        {
            get
            {
                return start_Year;
            }
            set
            {
                if (start_Year > end_Year) start_Year = end_Year;
                start_Year = value;
                this.Invalidate();
            }
        }
        private int end_Year = 2030;
        [Category("RJ Code Advance")]
        public int End_Year
        {
            get
            {
                return end_Year;
            }
            set
            {
                if (end_Year < start_Year) end_Year = start_Year;
                end_Year = value;
                this.Invalidate();
            }
        }
        private Font font = new Font("標楷體", 18);
        [Category("RJ Code Advance")]
        public Font mFont
        {
            get => font;
            set
            {
                this.rJ_ComboBox_Year.Font = value;
                this.rJ_ComboBox_Month.Font = value;
                this.rJ_ComboBox_Day.Font = value;
                this.label1.Font = value;
                this.label2.Font = value;

                Size size_Year = TextRenderer.MeasureText("YYYY", value);
                Size size_Month = TextRenderer.MeasureText("MM", value);
                Size size_Day = TextRenderer.MeasureText("DD", value);
                Size size_sq = TextRenderer.MeasureText("//", value);
                size_Year.Width += 40;
                size_Month.Width += 40;
                size_Day.Width += 40;

                rJ_ComboBox_Year.Size = size_Year;
                rJ_ComboBox_Month.Size = size_Month;
                rJ_ComboBox_Day.Size = size_Day;
                this.label1.Width = size_sq.Width;
                this.label2.Width = size_sq.Width;
                size_Year.Height = size_Month.Height = size_Day.Height = this.label1.Height = this.label1.Height + 20;

                if (DesignMode)
                    this.Size = new Size(rJ_ComboBox_Year.Width + rJ_ComboBox_Month.Width + rJ_ComboBox_Day.Width + this.label1.Width * 2, this.label1.Height);

                font = value;
            }
        }
        public int Year
        {
            get
            {
                return this.rJ_ComboBox_Year.Texts.StringToInt32();
            }
            set
            {
                this.rJ_ComboBox_Year.Texts = value.ToString();
            }
        }
        public int Month
        {
            get
            {
                return this.rJ_ComboBox_Month.Texts.StringToInt32();
            }
            set
            {
                this.rJ_ComboBox_Month.Texts = value.ToString();
            }
        }
        public int Day
        {
            get
            {
                return this.rJ_ComboBox_Day.Texts.StringToInt32();
            }
            set
            {
                this.rJ_ComboBox_Day.Texts = value.ToString();
            }
        }
        private DateTime value = DateTime.Now;
        [Category("RJ Code Advance")]
        public DateTime Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
                if (Year != value.Year) Year = value.Year;
                if (Month != value.Month) Month = value.Month;
                if (Day != value.Day) Day = value.Day;
            }
        }

        public DateTimeComList()
        {
            InitializeComponent();    
        }
        private void DateTimeComList_Load(object sender, EventArgs e)
        {
            DateTime value_buf = value;
            this.mFont = font;
            this.label1.ForeColor = Color.Black;
            this.label2.ForeColor = Color.Black;
            if (start_Year > end_Year) start_Year = end_Year;
            for (int i = start_Year; i <= end_Year; i++)
            {
                this.rJ_ComboBox_Year.Items.Add(i.ToString());
            }
            for (int i = 1; i <= 12; i++)
            {
                this.rJ_ComboBox_Month.Items.Add(i.ToString());
            }
            this.rJ_ComboBox_Year.SelectedIndex = 0;
            this.rJ_ComboBox_Month.SelectedIndex = 0;
            rJ_ComboBox_Month_OnSelectedIndexChanged(null, null);
            this.Value = value_buf;
        }

        private void rJ_ComboBox_Year_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Value = new DateTime(Year, Value.Month, Value.Day);
        }
        private void rJ_ComboBox_Month_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int old_selected = this.rJ_ComboBox_Day.SelectedIndex;
            int MontOfDays = Basic.TypeConvert.GetMonthOfDays(Month);
            if (Value.Day > MontOfDays) Value = new DateTime(Value.Year, Month, Value.Day);
            else Value = new DateTime(Value.Year, Month, Value.Day);

            List<string> list_value = new List<string>();
            for (int i = 1; i <= MontOfDays; i++)
            {
                list_value.Add(i.ToString());
            }
            this.rJ_ComboBox_Day.DataSource = list_value;
            if (this.rJ_ComboBox_Day.Items.Count == 0) return;
            if (old_selected == -1) this.rJ_ComboBox_Day.SelectedIndex = 0;
            else if (old_selected < this.rJ_ComboBox_Day.Items.Count) this.rJ_ComboBox_Day.SelectedIndex = old_selected;

        }
        private void rJ_ComboBox_Day_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Value = new DateTime(Value.Year, Value.Month, Day);
        }


    }
}
