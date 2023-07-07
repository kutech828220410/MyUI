using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Configuration;
using Basic;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
namespace SQLUI
{
    [DefaultEvent("MouseDown")]
    [System.Drawing.ToolboxBitmap(typeof(DataSet))]
    public partial class SQL_DataGridView : UserControl
    {
        private bool flag_Init = false;
        private bool flag_Refresh = false;
        private CheckBox checkBoxHeader = new CheckBox();
        private bool flag_unCheckedAll = false;

        private int NumOfPageRows
        {
            get
            {
                float colheight = this.columnHeadersHeight;
                float rowsheight = this.RowsHeight;
                float value = this.Height - colheight;
                value = (float)Math.Round(value / rowsheight);
                return (int)value;
            }
        }
        private int SelectRowindex_Buf = -1;
        #region Event
        public delegate void DataGridRowsChangeEventHandler(List<object[]> RowsList);
        public event DataGridRowsChangeEventHandler DataGridRowsChangeEvent;
        public delegate void DataGridRowsChangeRefEventHandler(ref List<object[]> RowsList);
        public event DataGridRowsChangeRefEventHandler DataGridRowsChangeRefEvent;
        public delegate void DataGridRefreshEventHandler();
        public event DataGridRefreshEventHandler DataGridRefreshEvent;
        public delegate void DataGridColumnHeaderMouseClickEventHandler(int ColumnIndex);
        public event DataGridColumnHeaderMouseClickEventHandler DataGridColumnHeaderMouseClickEvent;
        public delegate void RowDoubleClickEventHandler(object[] RowValue);
        public event RowDoubleClickEventHandler RowDoubleClickEvent;
        public delegate void RowEnterEventHandler(object[] RowValue);
        public event RowEnterEventHandler RowEnterEvent;
        public delegate void RowEndEditEventHandler(object[] RowValue, int rowIndex, int colIndex, string value);
        public event RowEndEditEventHandler RowEndEditEvent;
        public delegate void CellValidatingEventHandler(object[] RowValue, int rowIndex, int colIndex, string value, DataGridViewCellValidatingEventArgs e);
        public event CellValidatingEventHandler CellValidatingEvent;
        public delegate void CheckedChangedEventHandler(List<object[]> RowsList, int index);
        public event CheckedChangedEventHandler CheckedChangedEvent;


        private delegate void ModuleChangeEventHandler(List<object[]> RowsList);
        private event ModuleChangeEventHandler ModuleChangeEvent;
        private void RowsChange(List<object[]> RowsList)
        {
            this.RowsChange(RowsList, this.AutoSelectToDeep);
        }
        public void RowsChange(List<object[]> RowsList, bool SelectToDeep)
        {
            this.flag_Refresh = true;
            dataGridView.CellValueChanged -= DataGridView_CellValueChanged;
            if (this.DataGridRowsChangeEvent != null)
            {
                this.DataGridRowsChangeEvent(RowsList);
            }
            if (this.DataGridRowsChangeRefEvent != null)
            {
                this.DataGridRowsChangeRefEvent(ref RowsList);
            }
            List<int> List_SelectRowindex = this.GetSelectRowsIndex();
            int ScrollingRowIndex = dataGridView.FirstDisplayedScrollingRowIndex;
            SaveDataVal.RowsList = null;
            SaveDataVal.RowsList = RowsList;          

            List<object[]> RowsList_buf = new List<object[]>();
            DateTime Datebuf;
            int Index = 0;
            object[] obj_buf;
            foreach (object[] object_array in RowsList)
            {
                obj_buf = object_array;
                for (int i = 0; i < object_array.Length; i++)
                {
                    if (object_array[i] is DateTime)
                    {
                        Datebuf = (DateTime)obj_buf[i];
                        if (Columns[i].DateType == Table.DateType.YEAR)
                        {
                            obj_buf[i] = Datebuf.Year.ToString("0000");
                        }
                        else if (Columns[i].DateType == Table.DateType.DATE)
                        {
                            obj_buf[i] = Datebuf.Year.ToString("0000") + "-" + Datebuf.Month.ToString("00") + "-" + Datebuf.Day.ToString("00");
                        }
                        else if (Columns[i].DateType == Table.DateType.TIME)
                        {
                            obj_buf[i] = Datebuf.Hour.ToString("00") + ":" + Datebuf.Minute.ToString(":") + "-" + Datebuf.Second.ToString(":");
                        }
                        else if (Columns[i].DateType == Table.DateType.DATETIME)
                        {
                            obj_buf[i] = Datebuf.Year.ToString("0000") + "-" + Datebuf.Month.ToString("00") + "-" + Datebuf.Day.ToString("00");
                            obj_buf[i] += " ";
                            obj_buf[i] += Datebuf.Hour.ToString("00") + ":" + Datebuf.Minute.ToString("00") + ":" + Datebuf.Second.ToString("00");
                        }
                        else if (Columns[i].DateType == Table.DateType.TIMESTAMP)
                        {
                            obj_buf[i] = Datebuf.Year.ToString("0000") + Datebuf.Month.ToString("00") + Datebuf.Day.ToString("00");
                            obj_buf[i] += Datebuf.Hour.ToString("00") + Datebuf.Minute.ToString("00") + Datebuf.Second.ToString("00");
                        }

                    }

                }
                RowsList_buf.Add(obj_buf);
                Index++;
            }
           
            RowsList = RowsList_buf.DeepClone();
            RowsList_buf = null;

            if (dataTable_buffer != null) dataTable_buffer.Dispose();
            dataTable_buffer = new DataTable();
            foreach (ColumnElement columns in Columns)
            {
                if (dataTable_buffer.Columns[columns.Text] == null)
                {
                    if (this.IsColumnsText(columns.Text)) dataTable_buffer.Columns.Add(new DataColumn(columns.Text, typeof(string)));
                    else dataTable_buffer.Columns.Add(new DataColumn(columns.Text, typeof(Image)));

                }
            }
            foreach (object[] object_templass in RowsList)
            {
                dataTable_buffer.Rows.Add(object_templass);
            }
             
            dataGridView.Invoke(new Action(delegate
            {
                this.SuspendDrawing();
                dataGridView.SuspendDrawing();
                //if (dataGridView.DataSource != null) dataGridView.DataSource = null;
                dataGridView.DataSource = dataTable_buffer;
               
                bool IsSetEnable = (dataTable.Rows.Count != dataTable_buffer.Rows.Count);
                if (IsSetEnable) dataGridView.Enabled = false;
                //dataTable.BeginInit();             
                //dataTable.Rows.Clear();
                //for (int Y = 0; Y < dataTable_buffer.Rows.Count; Y++)
                //{
                //    dataTable.Rows.Add(dataTable_buffer.Rows[Y].ItemArray);
                //}
                //dataTable.EndInit();
               

                //dataGridView.Enabled = false;
                //for (int Y = 0; Y < dataTable_buffer.Rows.Count; Y++)
                //{
                //    for (int X = 0; X < dataTable_buffer.Columns.Count; X++)
                //    {
                //        if (Y >= dataTable.Rows.Count) dataTable.Rows.Add(dataTable_buffer.Rows[Y][X]);
                //        else dataTable.Rows[Y][X] = dataTable_buffer.Rows[Y][X];
                //    }
                //}
                //dataGridView.Enabled = true;

                if (DataGridRefreshEvent != null) this.DataGridRefreshEvent();
                if(this.顯示首列)
                {
                    dataGridView.RowHeadersDefaultCellStyle.Padding = new Padding(0, 0, 0, 0);
                    dataGridView.RowHeadersDefaultCellStyle.Font = this.cellStyleFont;
                    dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                }
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    dataGridView.Rows[i].Selected = false;
                    if (this.顯示首列)
                    {
                        dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }
                }
                Size size = System.Windows.Forms.TextRenderer.MeasureText((dataGridView.Rows.Count + 1).ToString(), dataGridView.RowHeadersDefaultCellStyle.Font);
                dataGridView.RowHeadersWidth = size.Width + 22;
               

                for (int i = 0; i < List_SelectRowindex.Count; i++)
                {
                    if (List_SelectRowindex[i] < dataGridView.RowCount)
                    {
                        if (List_SelectRowindex[i] != -1)
                        {
                            dataGridView.Rows[List_SelectRowindex[i]].Selected = true;
                        }
                    }
                }
                foreach (ColumnElement columns in Columns)
                {
                    if(columns.CanEdit)
                    {
                        dataGridView.Columns[columns.Text].DefaultCellStyle.SelectionBackColor = Color.Yellow;
                        dataGridView.Columns[columns.Text].DefaultCellStyle.SelectionForeColor = Color.DimGray;
                        dataGridView.Columns[columns.Text].DefaultCellStyle.ForeColor = Color.DimGray;
                    }
                   
                }


                if (IsSetEnable) dataGridView.Enabled = true;
                if (dataGridView.RowCount - 1 > 0)
                {
                    if (SelectToDeep)
                    {
                        dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.RowCount - 1;
                    }
                    else
                    {
                        if (ScrollingRowIndex != -1)
                        {
                            if (ScrollingRowIndex < dataGridView.RowCount - 1) dataGridView.FirstDisplayedScrollingRowIndex = ScrollingRowIndex;
                        }
                    }
                }
                this.ResumeDrawing();
                dataGridView.ResumeDrawing();
               
            }));
            this.flag_Refresh = false;
            dataGridView.CellValueChanged += DataGridView_CellValueChanged;
        }
        private void DataGridView_MouseWheel(object sender, MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            bool bToDown = e.Delta > 0 ? false : true;
            int index = dataGridView.FirstDisplayedScrollingRowIndex;
            if (bToDown) index++;
            else index--;
            if (index < 0) return;
            if (index < dataGridView.RowCount - 1)
            {
                dataGridView.FirstDisplayedScrollingRowIndex = index;
            }
        }
        #endregion
        private SaveDataValClass SaveDataVal = new SaveDataValClass();
        [Serializable]
        private class SaveDataValClass
        {
            public List<object[]> RowsList = new List<object[]>();
            public List<object[]> OtherSaveList = new List<object[]>();
        }

        private const string Extension = @".data";
        private bool IsStart = false;
        private SQLControl _SQLControl;
        private DataTable dataTable = new DataTable();
        private DataTable dataTable_buffer = new DataTable();
        private Basic.MyConvert _MyConvert = new MyConvert();
        private Table SQL_Table = new Table("");
        public override System.Windows.Forms.Layout.LayoutEngine LayoutEngine
        {
            get
            {

                return base.LayoutEngine;
            }
        }
        public int DataLenth
        {
            get
            {
                return this.Columns.Count;
            }
        }
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
        #region SQL-屬性
        public enum OnlineEnum : int
        {
            Online,
            Offline
        }
        OnlineEnum _OnlineEnum = new OnlineEnum();
        [ReadOnly(false), Browsable(true), Category("SQL-屬性"), Description(""), DefaultValue("")]
        public OnlineEnum OnlineState
        {
            get
            {
                return _OnlineEnum;
            }
            set
            {
                _OnlineEnum = value;
            }
        }
        string _Server = "127.0.0.0";
        [ReadOnly(false), Browsable(true), Category("SQL-屬性"), Description(""), DefaultValue("")]
        public string Server
        {
            get
            {
                return _Server;
            }
            set
            {
                _Server = value;
            }
        }
        string _DataBaseName = "";
        [ReadOnly(false), Browsable(true), Category("SQL-屬性"), Description(""), DefaultValue("")]
        public string DataBaseName
        {
            get
            {
                return _DataBaseName;
            }
            set
            {
                _DataBaseName = value;
            }
        }
        string _UserName = "root";
        [ReadOnly(false), Browsable(true), Category("SQL-屬性"), Description(""), DefaultValue("")]
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }
        string _Password = "user82822040";
        [ReadOnly(false), Browsable(true), Category("SQL-屬性"), Description(""), DefaultValue("")]
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }
        uint _Port = 3306;
        [ReadOnly(false), Browsable(true), Category("SQL-屬性"), Description(""), DefaultValue("")]
        public uint Port
        {
            get
            {
                return _Port;
            }
            set
            {
                _Port = value;
            }
        }
        MySqlSslMode _SSLMode = MySqlSslMode.None;
        [ReadOnly(false), Browsable(true), Category("SQL-屬性"), Description(""), DefaultValue("")]
        public MySqlSslMode SSLMode
        {
            get
            {
                return _SSLMode;
            }
            set
            {
                _SSLMode = value;
            }
        }

        #endregion
        #region 自訂屬性
        bool _自動換行 = true;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 自動換行
        {
            get
            {
                return _自動換行;
            }
            set
            {
                _自動換行 = value;
            }
        }

        Color _RowsColor = Control.DefaultBackColor;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Color RowsColor
        {
            get
            {
                return _RowsColor;
            }
            set
            {
                _RowsColor = value;
            }
        }
        int _RowsHeight = 10;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int RowsHeight
        {
            get
            {
                return _RowsHeight;
            }
            set
            {
                _RowsHeight = value;
            }
        }
        bool _AutoSelectToDeep = true;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool AutoSelectToDeep
        {
            get
            {
                return _AutoSelectToDeep;
            }
            set
            {
                _AutoSelectToDeep = value;
            }
        }
        string _SaveFileName = "SQL_DataGridView";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string SaveFileName
        {
            get
            {
                return _SaveFileName;
            }
            set
            {
                _SaveFileName = value;
            }
        }
        string _TableName = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
            }
        }
        Font _表單字體 = new Font("新細明體", 9);
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Font 表單字體
        {
            get
            {
                return _表單字體;
            }
            set
            {
                this.Font = value;
                _表單字體 = value;
            }
        }
        bool _顯示首行 = true;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 顯示首行
        {
            get
            {
                return _顯示首行;
            }
            set
            {
                dataGridView.ColumnHeadersVisible = value;
                _顯示首行 = value;
            }
        }
        bool _可選擇多列 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 可選擇多列
        {
            get
            {
                return _可選擇多列;
            }
            set
            {
                _可選擇多列 = value;
            }
        }
        bool _可拖曳欄位寬度 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 可拖曳欄位寬度
        {
            get
            {
                return _可拖曳欄位寬度;
            }
            set
            {
                _可拖曳欄位寬度 = value;
            }
        }
        DataGridViewHeaderBorderStyle _首行樣式 = DataGridViewHeaderBorderStyle.Raised;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public DataGridViewHeaderBorderStyle 首行樣式
        {
            get
            {

                return _首行樣式;
            }
            set
            {
                _首行樣式 = value;
                if (_首行樣式 == DataGridViewHeaderBorderStyle.Custom) _首行樣式 = DataGridViewHeaderBorderStyle.Single;
                dataGridView.ColumnHeadersBorderStyle = _首行樣式;
          
            }
        }
        DataGridViewHeaderBorderStyle _首列樣式 = DataGridViewHeaderBorderStyle.Raised;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public DataGridViewHeaderBorderStyle 首列樣式
        {
            get
            {
                return _首列樣式;
            }
            set
            {
                _首列樣式 = value;
                if (_首列樣式 == DataGridViewHeaderBorderStyle.Custom) _首列樣式 = DataGridViewHeaderBorderStyle.Single;
                dataGridView.RowHeadersBorderStyle = _首列樣式;
          
            }
        }
        System.Windows.Forms.BorderStyle _邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public System.Windows.Forms.BorderStyle 邊框樣式
        {
            get
            {
                return _邊框樣式;
            }
            set
            {
                _邊框樣式 = value;
                dataGridView.BorderStyle = _邊框樣式;
        
            }
        }
        DataGridViewCellBorderStyle _單格樣式 = DataGridViewCellBorderStyle.Raised;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public DataGridViewCellBorderStyle 單格樣式
        {
            get
            {
                return _單格樣式;
            }
            set
            {
                _單格樣式 = value;
                if (_單格樣式 == DataGridViewCellBorderStyle.Custom) _單格樣式 = DataGridViewCellBorderStyle.Single;
                dataGridView.CellBorderStyle = _單格樣式;
            
            }
        }
        bool _顯示首列 = true;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 顯示首列
        {
            get
            {
                return _顯示首列;
            }
            set
            {
                _顯示首列 = value;
                dataGridView.RowHeadersVisible = value;
        
            }
        }

        bool _顯示CheckBox = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 顯示CheckBox
        {
            get
            {
                return _顯示CheckBox;
            }
            set
            {
                _顯示CheckBox = value;

            }
        }

        private bool _ImageBox = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool ImageBox
        {
            get { return _ImageBox; }
            set
            {
                if (value)
                {
                    Columns.Clear();
                    ColumnElement _ColumnElement01 = new ColumnElement();
                    _ColumnElement01.Name = SQLControl.Column01ImageName;
                    _ColumnElement01.Width = 200;
                    _ColumnElement01.ValueType = Table.ValueType.None;
                    _ColumnElement01.StringType = Table.StringType.VARCHAR;
                    _ColumnElement01.DateType = Table.DateType.None;
                    _ColumnElement01.IndexType = Table.IndexType.PRIMARY;

                    ColumnElement _ColumnElement02 = new ColumnElement();
                    _ColumnElement02.Name = SQLControl.Column02ImageName;
                    _ColumnElement02.Width = 200;
                    _ColumnElement02.ValueType = Table.ValueType.None;
                    _ColumnElement02.StringType = Table.StringType.LONGBLOB;
                    _ColumnElement02.DateType = Table.DateType.None;
                    _ColumnElement02.IndexType = Table.IndexType.None;
                    _ColumnElement02.Visable = false;

                    ColumnElement _ColumnElement03 = new ColumnElement();
                    _ColumnElement03.Name = SQLControl.Column03ImageName;
                    _ColumnElement03.Width = 200;
                    _ColumnElement03.ValueType = Table.ValueType.None;
                    _ColumnElement03.StringType = Table.StringType.None;
                    _ColumnElement03.DateType = Table.DateType.TIMESTAMP;
                    _ColumnElement03.IndexType = Table.IndexType.INDEX;

                    Columns.Add(_ColumnElement01);
                    Columns.Add(_ColumnElement02);
                    Columns.Add(_ColumnElement03);
                }
                _ImageBox = value;
            }
        }
        [Serializable]
        public class ColumnElement
        {
            private string _Name = "ColumnName";
            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }
            private int _Width = 100;
            public int Width
            {
                get { return _Width; }
                set { _Width = value; }
            }
            private bool _Visable = true;
            public bool Visable
            {
                get { return _Visable; }
                set { _Visable = value; }
            }
            private uint datalen = 50;
            public uint Datalen
            {
                get { return datalen; }
                set { datalen = value; }
            }
            private string text = "";
            public string Text
            {
                get
                {
                    if (text.StringIsEmpty()) return this._Name;
                    return text;
                }
                set
                {
                    this.text = value;
                }
            }
            private bool canEdit = true;
            public bool CanEdit
            {
                get { return canEdit; }
                set { canEdit = value; }
            }


            private Color _BackgroundColor = Color.White;
            public Color BackgroundColor
            {
                get { return _BackgroundColor; }
                set { _BackgroundColor = value; }
            }
            private DataGridViewColumnSortMode _SortMode = DataGridViewColumnSortMode.NotSortable;
            public DataGridViewColumnSortMode SortMode
            {
                get { return _SortMode; }
                set { _SortMode = value; }
            }
            private DataGridViewContentAlignment _Alignment = DataGridViewContentAlignment.MiddleCenter;
            public DataGridViewContentAlignment Alignment
            {
                get { return _Alignment; }
                set { _Alignment = value; }
            }

            private Table.IndexType _IndexType = Table.IndexType.None;
            public Table.IndexType IndexType
            {
                get { return _IndexType; }
                set
                {
                    _IndexType = value;
                }
            }
            private Table.ValueType _ValueType = Table.ValueType.None;
            public Table.ValueType ValueType
            {
                get { return _ValueType; }
                set
                {
                    _ValueType = value;
                }
            }
            private Table.DateType _DateType = Table.DateType.None;
            public Table.DateType DateType
            {
                get { return _DateType; }
                set
                {
                    _DateType = value;
                }
            }
            private Table.StringType _StringType = Table.StringType.VARCHAR;
            public Table.StringType StringType
            {
                get { return _StringType; }
                set
                {
                    _StringType = value;
                }
            }
            private Table.OtherType _OtherType = Table.OtherType.None;
            public Table.OtherType OtherType
            {
                get { return _OtherType; }
                set
                {
                    _OtherType = value;
                }
            }

            public override string ToString()
            {
                string str = $"[Name : {Name}]";

                if(_ValueType != Table.ValueType.None)
                {
                    str += $"[Type : {_ValueType.GetEnumName()}]";
                }
                else if (_DateType != Table.DateType.None)
                {
                    str += $"[Type : {_DateType.GetEnumName()}]";
                }
                else if (_StringType != Table.StringType.None)
                {
                    str += $"[Type : {_StringType.GetEnumName()}]";
                }
                else if (_OtherType != Table.OtherType.None)
                {
                    str += $"[Type : {_OtherType.GetEnumName()}]";
                }
                str += $"[Datalen : {Datalen}]";
                str += $"[Index : {_IndexType.GetEnumName()}]";
                return str;
            }
        }
        private List<ColumnElement> _Columns = new List<ColumnElement>();       
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [ReadOnly(false), Browsable(true), Category("自訂集合"), Description("自訂集合"), DefaultValue("")]
        public List<ColumnElement> Columns
        {
            get
            {
                return _Columns;
            }
            set
            {

                _Columns = value;
                if (this.DesignMode)
                {
                    DataGrid_Init(false);
                }
            }
        }
        #endregion

        [Category("RJ Code - Appearence")]
        public Font columnHeaderFont
        {
            get
            {
                return dataGridView.ColumnHeadersDefaultCellStyle.Font;
            }
            set
            {
                dataGridView.ColumnHeadersDefaultCellStyle.Font = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public Color columnHeaderBackColor
        {
            get
            {
                return dataGridView.ColumnHeadersDefaultCellStyle.BackColor;
            }
            set
            {
                dataGridView.ColumnHeadersDefaultCellStyle.BackColor = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public DataGridViewHeaderBorderStyle columnHeadersBorderStyle
        {
            get
            {
                return dataGridView.ColumnHeadersBorderStyle;
            }
            set
            {
                dataGridView.ColumnHeadersBorderStyle = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public DataGridViewColumnHeadersHeightSizeMode columnHeadersHeightSizeMode
        {
            get
            {
                return dataGridView.ColumnHeadersHeightSizeMode;
            }
            set
            {
                dataGridView.ColumnHeadersHeightSizeMode = value;
                dataGridView.Invalidate();
            }
        }
        [Category("RJ Code - Appearence")]
        public int columnHeadersHeight
        {
            get
            {
                return dataGridView.ColumnHeadersHeight;
            }
            set
            {
                dataGridView.ColumnHeadersHeight = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public Color rowHeaderBackColor
        {
            get
            {
                return dataGridView.RowHeadersDefaultCellStyle.BackColor;
            }
            set
            {
                dataGridView.RowHeadersDefaultCellStyle.BackColor = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public DataGridViewHeaderBorderStyle rowHeadersBorderStyle
        {
            get
            {
                return dataGridView.RowHeadersBorderStyle;
            }
            set
            {
                dataGridView.RowHeadersBorderStyle = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public Font cellStyleFont
        {
            get
            {
                return dataGridView.DefaultCellStyle.Font;
            }
            set
            {
                dataGridView.DefaultCellStyle.Font = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public Color cellStylForeColor
        {
            get
            {
                return dataGridView.DefaultCellStyle.ForeColor;
            }
            set
            {
                dataGridView.DefaultCellStyle.ForeColor = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public Color cellStylBackColor
        {
            get
            {
                return dataGridView.DefaultCellStyle.BackColor;
            }
            set
            {
                dataGridView.DefaultCellStyle.BackColor = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public DataGridViewCellBorderStyle cellBorderStyle
        {
            get
            {
                return dataGridView.CellBorderStyle;
            }
            set
            {
                dataGridView.CellBorderStyle = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public int BorderSize
        {
            get
            {
                return dataGridView.BorderSize;
            }
            set
            {
                dataGridView.BorderSize = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public int BorderRadius
        {
            get
            {
                return dataGridView.BorderRadius;
            }
            set
            {
                dataGridView.BorderRadius = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public Color BorderColor
        {
            get
            {
                return dataGridView.BorderColor;
            }
            set
            {
                dataGridView.BorderColor = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public Color backColor
        {
            get
            {
                return dataGridView.BackgroundColor;
            }
            set
            {
                dataGridView.BackgroundColor = value;
            }
        }

        public class ConnentionClass
        {
            private string dataBaseName = "";
            private string iP = "";
            private string userName = "";
            private string password = "";
            private uint port = 0;
            private string tableName = "";
            private MySql.Data.MySqlClient.MySqlSslMode mySqlSslMode = MySql.Data.MySqlClient.MySqlSslMode.None;

            public string DataBaseName { get => dataBaseName; set => dataBaseName = value; }
            public string IP { get => iP; set => iP = value; }
            public string UserName { get => userName; set => userName = value; }
            public string Password { get => password; set => password = value; }
            public uint Port { get => port; set => port = value; }
            public MySqlSslMode MySqlSslMode { get => mySqlSslMode; set => mySqlSslMode = value; }
            public string TableName { get => tableName; set => tableName = value; }
        }

        public static void SQL_UI_Init(Form ActiveForm)
        {
            List<Control> list_ctl = MyUI.PLC_UI_Init.Find_Control(ActiveForm, new SQLUI.SQL_DataGridView().GetType());

            foreach (Control ctl in list_ctl)
            {
                if (ctl is SQLUI.SQL_DataGridView)
                {
                    ((SQLUI.SQL_DataGridView)ctl).Init();
                }
            }
        }
        public static void SQL_Set_Properties(string UserName, string Password, string Server, uint Port, Form ActiveForm)
        {
            List<Control> list_ctl = MyUI.PLC_UI_Init.Find_Control(ActiveForm, new SQLUI.SQL_DataGridView().GetType());

            foreach (Control ctl in list_ctl)
            {
                if (ctl is SQLUI.SQL_DataGridView)
                {
                    ((SQLUI.SQL_DataGridView)ctl).UserName = UserName;
                    ((SQLUI.SQL_DataGridView)ctl).Password = Password;
                    ((SQLUI.SQL_DataGridView)ctl).Server = Server;
                    ((SQLUI.SQL_DataGridView)ctl).Port = Port;
                }
            }
        }
        public static void SQL_Set_Properties(ConnentionClass connentionClass, Form ActiveForm)
        {
            SQL_Set_Properties(connentionClass.DataBaseName, connentionClass.UserName, connentionClass.Password, connentionClass.IP, connentionClass.Port, connentionClass.MySqlSslMode, ActiveForm);
        }
        public static void SQL_Set_Properties(string DataBaseName, string UserName, string Password, string Server, uint Port, MySqlSslMode SSLMode, Form ActiveForm)
        {
            List<Control> list_ctl = MyUI.PLC_UI_Init.Find_Control(ActiveForm, new SQLUI.SQL_DataGridView().GetType());

            foreach (Control ctl in list_ctl)
            {
                if (ctl is SQLUI.SQL_DataGridView)
                {
                    ((SQLUI.SQL_DataGridView)ctl).DataBaseName = DataBaseName;
                    ((SQLUI.SQL_DataGridView)ctl).UserName = UserName;
                    ((SQLUI.SQL_DataGridView)ctl).Password = Password;
                    ((SQLUI.SQL_DataGridView)ctl).Server = Server;
                    ((SQLUI.SQL_DataGridView)ctl).Port = Port;
                    ((SQLUI.SQL_DataGridView)ctl).SSLMode = SSLMode;
                }
            }
        }
        public static void SQL_Set_Properties(SQL_DataGridView sQL_DataGridView ,ConnentionClass connentionClass)
        {
            SQL_Set_Properties(sQL_DataGridView, connentionClass.TableName, connentionClass.DataBaseName, connentionClass.UserName, connentionClass.Password, connentionClass.IP, connentionClass.Port, connentionClass.MySqlSslMode);
        }
        public static void SQL_Set_Properties(SQL_DataGridView sQL_DataGridView, string DataBaseName, string UserName, string Password, string Server, uint Port, MySqlSslMode SSLMode)
        {
            SQL_Set_Properties(sQL_DataGridView, null, DataBaseName, UserName, Password, Server, Port, SSLMode);
        }
        public static void SQL_Set_Properties(SQL_DataGridView sQL_DataGridView ,string TableName, string DataBaseName, string UserName, string Password, string Server, uint Port, MySqlSslMode SSLMode)
        {
            if(!TableName.StringIsEmpty())
            {
                sQL_DataGridView.TableName = TableName;
            }
            sQL_DataGridView.DataBaseName = DataBaseName;
            sQL_DataGridView.UserName = UserName;
            sQL_DataGridView.Password = Password;
            sQL_DataGridView.Server = Server;
            sQL_DataGridView.Port = Port;
            sQL_DataGridView.SSLMode = SSLMode;
        }

        public SQL_DataGridView()
        {
            InitializeComponent();
            this.IsStart = true;
            this.Resize += SQL_DataGridView_Resize;
        }

        private void SQL_DataGridView_Resize(object sender, EventArgs e)
        {
            DataGrid_Init(!this.DesignMode);
        }
        private void SQL_DataGridView_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            Basic.Reflection.MakeDoubleBuffered(this.dataGridView, true);

            DataGridViewRow row = this.dataGridView.RowTemplate;
            row.DefaultCellStyle.BackColor = this.RowsColor;
            row.Height = this.RowsHeight;
            row.MinimumHeight = 20;

        }
        private void SQL_DataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }
        public void Init(SQL_DataGridView sQL_DataGridView)
        {
            this.Columns = sQL_DataGridView.Columns.DeepClone();
            this.Server = sQL_DataGridView.Server;
            this.DataBaseName = sQL_DataGridView.DataBaseName;
            this.UserName = sQL_DataGridView.UserName;
            this.Password = sQL_DataGridView.Password;
            this.Port = sQL_DataGridView.Port;
            this.SSLMode = sQL_DataGridView.SSLMode;
            this.TableName = sQL_DataGridView.TableName;
            this.Init();
        }
        public void Init(Table table)
        {
            this.TableName = table.TableName;
            for(int i = 0; i < table.ColumnList.Count; i++)
            {
                ColumnElement columnElement = new ColumnElement();
                columnElement.Name = table.ColumnList[i].Name;
                columnElement.Text = table.ColumnList[i].Name;
                columnElement.IndexType = table.ColumnList[i].IndexType;
                columnElement.StringType = table.ColumnList[i].StringType;
                columnElement.ValueType = table.ColumnList[i].ValueType;
                columnElement.DateType = table.ColumnList[i].DateType;
                columnElement.OtherType = table.ColumnList[i].OtherType;
                columnElement.Datalen = (uint)table.ColumnList[i].Num;
                this.Columns.Add(columnElement);
            }
            Init();
        }
        public void Init(List<ColumnElement> Columns)
        {
            this.Columns = Columns.DeepClone();
            this.Init();
        }
        public void Init()
        {
            if (this.flag_Init == true) return;
            if (OnlineState == OnlineEnum.Online)
            {
                this.SQL_Reset();
                this.SQL_TableInit();
                this.DataGrid_Init(IsStart);       
                this.ModuleChangeEvent += new ModuleChangeEventHandler(RowsChange);
                dataGridView.MouseDown += SQL_DataGridView_MouseDown;
                dataGridView.CellClick += DataGridView_CellClick;
                dataGridView.CellEnter += DataGridView_CellEnter;
                dataGridView.DoubleClick += DataGridView_DoubleClick;
                dataGridView.CellPainting += DataGridView_CellPainting;
                dataGridView.CellEndEdit += DataGridView_CellEndEdit;
            
                dataGridView.CellValidating += DataGridView_CellValidating;
                dataGridView.CellValidated += DataGridView_CellValidated;
            }
            else if (OnlineState == OnlineEnum.Offline)
            {
                this.SQL_TableInit();
                this.DataGrid_Init(IsStart);
                this.ModuleChangeEvent += new ModuleChangeEventHandler(RowsChange);
                dataGridView.MouseDown += SQL_DataGridView_MouseDown;
                dataGridView.CellClick += DataGridView_CellClick;
                dataGridView.CellEnter += DataGridView_CellEnter;
                dataGridView.DoubleClick += DataGridView_DoubleClick;
                dataGridView.CellPainting += DataGridView_CellPainting;
                dataGridView.CellEndEdit += DataGridView_CellEndEdit;
               
                dataGridView.CellValidating += DataGridView_CellValidating;
                dataGridView.CellValidated += DataGridView_CellValidated;
            }
            dataGridView.Paint += DataGridView_Paint;
            dataGridView.MouseWheel += DataGridView_MouseWheel;
            if(checkBoxHeader != null)
            {
                checkBoxHeader.CheckedChanged += CheckBoxHeader_CheckedChanged;
            }
            this.flag_Init = true;
        }



        public DataTable GetDataTable()
        {
            return this.GetDataTable(true);
        }
        public DataTable GetDataTable(bool AllColunms)
        {
            DataTable dt = (DataTable)this.dataGridView.DataSource;
            while(!AllColunms)
            {
                bool flag_break = true;
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    if (!this.Columns[i].Visable)
                    {
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            if (dt.Columns[k].ColumnName == this.Columns[i].Name)
                            {
                                dt.Columns.Remove(this.Columns[i].Name);
                                flag_break = false;
                            }
                        }
                    }

                }
                if (flag_break) break;
            }
            
            return dt;
        }
        public DataTable GetSelectRowsDataTable()
        {
            return this.GetSelectRowsDataTable(true);
        }
        public DataTable GetSelectRowsDataTable(bool AllColunms)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < this.Columns.Count; i++)
            {
                dt.Columns.Add(this.Columns[i].Name);
            }
            List<object[]> list_value = this.Get_All_Select_RowsValues();
            for (int i = 0; i < list_value.Count; i++)
            {
                dt.Rows.Add(list_value[i]);
            }
            while (!AllColunms)
            {
                bool flag_break = true;
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    if (!this.Columns[i].Visable)
                    {
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            if (dt.Columns[k].ColumnName == this.Columns[i].Name)
                            {
                                dt.Columns.Remove(this.Columns[i].Name);
                                flag_break = false;
                            }
                        }
                    }

                }
                if (flag_break) break;
            }
            return dt;
        }

        public SQLControl GetSQLControl()
        {
            _SQLControl.TableName = this.TableName;
            return _SQLControl;
        }

        public bool SQL_GetTableLock()
        {
            return _SQLControl.GetTableLock(SQL_Table.GetTableName());
        }
        public void SQL_LockTable()
        {
            _SQLControl.LockTable(SQL_Table.GetTableName());
        }
        public void SQL_UnLockTable()
        {
            _SQLControl.UnLockTable();
        }
        public void SQL_Reset()
        {
            _SQLControl = new SQLControl(this.Server, this.DataBaseName, this.UserName, this.Password, this.Port, this.SSLMode);
        }
        public bool SQL_IsConnect(bool IsShowMessageBox)
        {
            bool IsOpen = _SQLControl.TestConnection();
            if (IsShowMessageBox) MessageBox.Show(IsOpen ? "Sucess" : "Connect Fail");
            return IsOpen;
        }
        public void SQL_TableInit()
        {
            SQL_Table.ClearColumn();
            SQL_Table.SetTableName(this.TableName);
            foreach (ColumnElement columns in Columns)
            {
                if (columns.ValueType != Table.ValueType.None)
                {
                    SQL_Table.AddColumnList(columns.Name, columns.ValueType, columns.IndexType);
                }
                else if (columns.StringType != Table.StringType.None)
                {
                    if (columns.StringType == Table.StringType.VARCHAR)
                    {
                        uint datalen = columns.Datalen;
                        if (datalen <= 0) datalen = 50;
                        SQL_Table.AddColumnList(columns.Name, columns.StringType , datalen, columns.IndexType);
                        continue;
                    }
                    if (columns.StringType == Table.StringType.CHAR)
                    {
                        uint datalen = columns.Datalen;
                        if (datalen <= 0) datalen = 50;
                        SQL_Table.AddColumnList(columns.Name, columns.StringType, datalen, columns.IndexType);
                        continue;
                    }
                    SQL_Table.AddColumnList(columns.Name, columns.StringType, columns.IndexType);
                }
                else if (columns.DateType != Table.DateType.None)
                {
                    SQL_Table.AddColumnList(columns.Name, columns.DateType, columns.IndexType);
                }
                else if (columns.OtherType != Table.OtherType.None)
                {
                    SQL_Table.AddColumnList(columns.Name, columns.OtherType, columns.IndexType);
                }
            }
        }
        public void SQL_CreateTable()
        {
            _SQLControl.CreatTable(SQL_Table);

        }
        public void SQL_DropTable()
        {
            _SQLControl.DropTable(SQL_Table.GetTableName());

        }
        public bool SQL_IsTableCreat()
        {
            return _SQLControl.IsTableCreat(SQL_Table.GetTableName());
        }
        public void SQL_Add_Column(string ColumnName, string AfterColumnName)
        {
            _SQLControl.Add_Column(this.TableName, ColumnName, SQL_Table.GetTypeName(ColumnName), SQL_Table.GetIndexType(ColumnName), AfterColumnName);
        }

        public bool SQL_CheckAllColumnName()
        {
            return this.SQL_CheckAllColumnName(false);
        }
        public bool SQL_CheckAllColumnName(bool autoAdd)
        {
            return _SQLControl.CheckAllColumnName(SQL_Table, autoAdd);
        
        }
        public object[] SQL_GetAllColumn_Name()
        {
            return _SQLControl.GetAllColumn_Name(SQL_Table.GetTableName());
        }
        public string SQL_GetColumnName(int Index)
        {
            object[] AllColumName = SQL_GetAllColumn_Name();
            if (Index < AllColumName.Length) return (string)AllColumName[Index];
            return null;
        }
        public int SQL_GetColumnIndex(string ColumnName)
        {
            object[] AllColumName = SQL_GetAllColumn_Name();
            int index = 0;
            foreach (object temp in AllColumName)
            {
                if ((string)temp == ColumnName) return index;
                index++;
            }
            return -1;
        }
        public List<object> SQL_GetColumnValues(string ColumnName, bool distinct)
        {
            List<object[]> temp = _SQLControl.GetColumnValues(SQL_Table.GetTableName(), ColumnName, distinct);
            List<object> temp_obj = new List<object>();
            for (int i = 0; i < temp.Count; i++)
            {
                temp_obj.Add(temp[i][0]);
            }
            return temp_obj;
        }
        public List<object> SQL_GetColumnValues(string ColumnName, bool distinct, ComboBox comboBox)
        {
            List<object> temp = this.SQL_GetColumnValues(ColumnName, distinct);
            this.Invoke(new Action(delegate
            {
                comboBox.Items.Clear();
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i].ObjectToString() != "")
                    {
                        comboBox.Items.Add(temp[i].ObjectToString());
                    }

                }
                if (comboBox.Text == "" && comboBox.Items.Count > 0) comboBox.SelectedIndex = 0;
            }));
            return temp;
        }

        public List<object[]> SQL_GetAllRows(bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetAllRows(SQL_Table.GetTableName());
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetAllRows(int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetAllRows(SQL_Table.GetTableName(), OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetAllRows(string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetAllRows(SQL_Table.GetTableName(), OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }

        public object[] SQL_GetRows(object[] value)
        {
            List<object[]> list_value = this.SQL_GetRows(0, value[0].ObjectToString(), false);
            if (list_value.Count == 0) return null;
            return list_value[0];
        }
        public List<object[]> SQL_GetRows(int serchColumnindex, string serchValue, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnindex, serchValue);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRows(string serchColumnName, string serchValue, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRows(string serchColumnName, string serchValue, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRows(int serchColumnindex, string serchValue, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnindex, serchValue, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRows(int serchColumnindex, string serchValue, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnindex, serchValue, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRows(int serchColumnindex, string serchValue, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnindex, serchValue, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRows(string serchColumnName, string serchValue, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRows(string serchColumnName, string serchValue, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRows(string serchColumnName, string serchValue, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRows(string[] serchColumnName, string[] serchValue, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRows(string[] serchColumnName, string[] serchValue, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRows(string[] serchColumnName, string[] serchValue, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRows(string[] serchColumnName, string[] serchValue, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRows(string[] serchColumnName, string[] serchValue, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsOfRange(uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsOfRange(SQL_Table.GetTableName(), StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsOfRange(uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsOfRange(SQL_Table.GetTableName(), StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsOfRange(uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsOfRange(SQL_Table.GetTableName(), StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(int serchColumnindex, string LikeString, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnindex, LikeString);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(int serchColumnindex, string LikeString, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnindex, LikeString, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(int serchColumnindex, string LikeString, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnindex, LikeString, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(int serchColumnindex, string LikeString, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnindex, LikeString, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(string serchColumnName, string LikeString, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnName, LikeString);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(string serchColumnName, string LikeString, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnName, LikeString, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(string serchColumnName, string LikeString, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnName, LikeString, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(string serchColumnName, string LikeString, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnName, LikeString, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }

        public List<object[]> SQL_GetRowsByBetween(int serchColumnindex, DateTime datetime, bool IsRefreshGrid)
        {
            DateTime datetime_start = new DateTime(datetime.Year, datetime.Month, datetime.Day, 0, 0, 0);
            DateTime dateTime_end = new DateTime(datetime.Year, datetime.Month, datetime.Day, 23, 59, 59);
            return this.SQL_GetRowsByBetween(serchColumnindex, datetime_start, dateTime_end, IsRefreshGrid);
        }
        public List<object[]> SQL_GetRowsByBetween(int serchColumnindex, DateTime datetime_start, DateTime datetime_end, bool IsRefreshGrid)
        {
            return this.SQL_GetRowsByBetween(serchColumnindex, datetime_start.ToDateTimeString_6(), datetime_end.ToDateTimeString_6(), IsRefreshGrid);
        }
        public List<object[]> SQL_GetRowsByBetween(int serchColumnindex, System.Windows.Forms.DateTimePicker datetime_start, System.Windows.Forms.DateTimePicker datetime_end, bool IsRefreshGrid)
        {
            DateTime dateTime_start = datetime_start.Value;
            DateTime dateTime_end = datetime_end.Value;

            dateTime_start = new DateTime(dateTime_start.Year, dateTime_start.Month, dateTime_start.Day, 0, 0, 0);
            dateTime_end = new DateTime(dateTime_end.Year, dateTime_end.Month, dateTime_end.Day, 23, 59, 59);

            return this.SQL_GetRowsByBetween(serchColumnindex, dateTime_start.ToDateTimeString_6(), dateTime_end.ToDateTimeString_6(), IsRefreshGrid);
        }
        public List<object[]> SQL_GetRowsByBetween(int serchColumnindex, string brtween_value1, string brtween_value2, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnindex, brtween_value1, brtween_value2);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(int serchColumnindex, string brtween_value1, string brtween_value2, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnindex, brtween_value1, brtween_value2, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(int serchColumnindex, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnindex, brtween_value1, brtween_value2, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(int serchColumnindex, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnindex, brtween_value1, brtween_value2, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(int serchColumnindex, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnindex, brtween_value1, brtween_value2, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }

        public List<object[]> SQL_GetRowsByBetween(string serchColumnName, DateTime datetime_start, DateTime datetime_end, bool IsRefreshGrid)
        {
            return this.SQL_GetRowsByBetween(serchColumnName, datetime_start.ToDateTimeString_6(), datetime_end.ToDateTimeString_6(), IsRefreshGrid);
        }
        public List<object[]> SQL_GetRowsByBetween(string serchColumnName, System.Windows.Forms.DateTimePicker datetime_start, System.Windows.Forms.DateTimePicker datetime_end, bool IsRefreshGrid)
        {
            DateTime dateTime_start = datetime_start.Value;
            DateTime dateTime_end = datetime_end.Value;

            dateTime_start = new DateTime(dateTime_start.Year, dateTime_start.Month, dateTime_start.Day, 0, 0, 0);
            dateTime_end = new DateTime(dateTime_end.Year, dateTime_end.Month, dateTime_end.Day, 23, 59, 59);

            return this.SQL_GetRowsByBetween(serchColumnName, dateTime_start.ToDateTimeString_6(), dateTime_end.ToDateTimeString_6(), IsRefreshGrid);
        }
        public List<object[]> SQL_GetRowsByBetween(string serchColumnName, string brtween_value1, string brtween_value2, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnName, brtween_value1, brtween_value2);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(string serchColumnName, string brtween_value1, string brtween_value2, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnName, brtween_value1, brtween_value2, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(string serchColumnName, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnName, brtween_value1, brtween_value2, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(string serchColumnName, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnName, brtween_value1, brtween_value2, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(string serchColumnName, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnName, brtween_value1, brtween_value2, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(int serchColumnindex, string[] IN_value, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnindex, IN_value);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(int serchColumnindex, string[] IN_value, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnindex, IN_value, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(int serchColumnindex, string[] IN_value, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnindex, IN_value, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(int serchColumnindex, string[] IN_value, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnindex, IN_value, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(string serchColumnName, string[] IN_value, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnName, IN_value);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(string serchColumnName, string[] IN_value, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnName, IN_value, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(string serchColumnName, string[] IN_value, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnName, IN_value, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(string serchColumnName, string[] IN_value, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnName, IN_value, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp);
            return temp;
        }

        public void SQL_AddRow(object[] Value, bool IsRefreshGrid)
        {
            _SQLControl.AddRow(SQL_Table.GetTableName(), Value);
            if (IsRefreshGrid)
            {
                List<object[]> temp = _SQLControl.GetAllRows(SQL_Table.GetTableName());
                if (IsRefreshGrid) this.RowsChange(temp, this.AutoSelectToDeep);
            }
        }
        public void SQL_AddRows(List<object[]> Values, bool IsRefreshGrid)
        {
            _SQLControl.AddRows(SQL_Table.GetTableName(), Values);
            if (IsRefreshGrid)
            {
                List<object[]> temp = _SQLControl.GetAllRows(SQL_Table.GetTableName());
                if (IsRefreshGrid) this.RowsChange(temp, this.AutoSelectToDeep);
            }
        }

        public void SQL_Replace(object[] Value, bool IsRefreshGrid)
        {
            _SQLControl.UpdateByDefult(SQL_Table.GetTableName(), "GUID", Value[0].ObjectToString(), Value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_Replace(string[] serchColumnName, string[] serchValue, object[] Value, bool IsRefreshGrid)
        {
            _SQLControl.UpdateByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, Value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_Replace(string serchColumnName, string serchValue, object[] Value, bool IsRefreshGrid)
        {
            _SQLControl.UpdateByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, Value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_Replace(int serchColumnindex, string serchValue, object[] Value, bool IsRefreshGrid)
        {
            _SQLControl.UpdateByDefult(SQL_Table.GetTableName(), serchColumnindex, serchValue, Value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_ReplaceByLike(int serchColumnindex, string LikeString, object[] Value, bool IsRefreshGrid)
        {
            _SQLControl.UpdateByLike(SQL_Table.GetTableName(), serchColumnindex, LikeString, Value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_ReplaceByLike(string serchColumnName, string LikeString, object[] Value, bool IsRefreshGrid)
        {
            _SQLControl.UpdateByLike(SQL_Table.GetTableName(), serchColumnName, LikeString, Value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_ReplaceByBetween(int serchColumnindex, string brtween_value1, string brtween_value2, object[] Value, bool IsRefreshGrid)
        {
            _SQLControl.UpdateByBetween(SQL_Table.GetTableName(), serchColumnindex, brtween_value1, brtween_value2, Value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_ReplaceByBetween(string serchColumnName, string brtween_value1, string brtween_value2, object[] Value, bool IsRefreshGrid)
        {
            _SQLControl.UpdateByBetween(SQL_Table.GetTableName(), serchColumnName, brtween_value1, brtween_value2, Value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_ReplaceByIn(int serchColumnindex, string IN_value, object[] Value, bool IsRefreshGrid)
        {
            _SQLControl.UpdateByIn(SQL_Table.GetTableName(), serchColumnindex, IN_value, Value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_ReplaceByIn(string serchColumnName, string IN_value, object[] Value, bool IsRefreshGrid)
        {
            _SQLControl.UpdateByIn(SQL_Table.GetTableName(), serchColumnName, IN_value, Value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_ReplaceByIn(int serchColumnindex, string[] IN_value, object[] Value, bool IsRefreshGrid)
        {
            _SQLControl.UpdateByIn(SQL_Table.GetTableName(), serchColumnindex, IN_value, Value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_ReplaceByIn(string[] serchColumnName, string[] IN_value, object[] Value, bool IsRefreshGrid)
        {
            _SQLControl.UpdateByIn(SQL_Table.GetTableName(), serchColumnName, IN_value, Value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }

        public void SQL_ReplaceExtra(object[] Value, bool IsRefreshGrid)
        {
            List<object[]> list_value = new List<object[]>();
            list_value.Add(Value);
            SQL_ReplaceExtra(list_value, IsRefreshGrid);
        }
        public void SQL_ReplaceExtra(List<object[]> Value, bool IsRefreshGrid)
        {
            List<string> serchValue = new List<string>();
            for (int i = 0; i < Value.Count; i++)
            {
                serchValue.Add(Value[i][0].ObjectToString());
            }
            SQL_ReplaceExtra("GUID", serchValue, Value, IsRefreshGrid);
        }
        public void SQL_ReplaceExtra(int GUID_Index, List<object[]> Value, bool IsRefreshGrid)
        {
            List<string> serchValue = new List<string>();
            for(int i = 0; i < Value.Count; i++)
            {
                serchValue.Add(Value[i][GUID_Index].ObjectToString());
            }
            SQL_ReplaceExtra("GUID", serchValue, Value, IsRefreshGrid);
        }
        public void SQL_ReplaceExtra(string[] serchColumnName, List<string[]> serchValue, List<object[]> Value, SQLControl.PrcessReprot prcessReprot, bool IsRefreshGrid)
        {
            List<string[]> _serchColumnName = new List<string[]>();
            for (int i = 0; i < Value.Count; i++)
            {
                _serchColumnName.Add(serchColumnName);
            }
            _SQLControl.UpdateByDefulteExtra(SQL_Table.GetTableName(), _serchColumnName, serchValue, Value, prcessReprot);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_ReplaceExtra(string serchColumnName, List<object> serchValue, List<object[]> Value, bool IsRefreshGrid)
        {
            List<string[]> _serchValue = new List<string[]>();
            for (int i = 0; i < serchValue.Count; i++)
            {
                _serchValue.Add(new string[] { serchValue[i].ObjectToString() });
            }
            this.SQL_ReplaceExtra(new string[] { serchColumnName }, _serchValue, Value, IsRefreshGrid);
        }
        public void SQL_ReplaceExtra(string serchColumnName, List<string> serchValue, List<object[]> Value, bool IsRefreshGrid)
        {
            List<string[]> _serchValue = new List<string[]>();
            for (int i = 0; i < serchValue.Count; i++)
            {
                _serchValue.Add(new string[] { serchValue[i] });
            }
            this.SQL_ReplaceExtra(new string[] { serchColumnName }, _serchValue, Value, IsRefreshGrid);
        }

        public void SQL_ReplaceExtra(string serchColumnName, List<string[]> serchValue, List<object[]> Value, bool IsRefreshGrid)
        {

            this.SQL_ReplaceExtra( new string[] { serchColumnName }, serchValue, Value, IsRefreshGrid);
        }
        public void SQL_ReplaceExtra(string[] serchColumnName, List<string[]> serchValue, List<object[]> Value, bool IsRefreshGrid)
        {
            List<string[]> _serchColumnName = new List<string[]>();
            for (int i = 0; i < Value.Count; i++)
            {
                _serchColumnName.Add(serchColumnName);
            }
            this.SQL_ReplaceExtra( _serchColumnName, serchValue, Value, IsRefreshGrid);
        }
        public void SQL_ReplaceExtra(List<string[]> serchColumnName, List<string[]> serchValue, List<object[]> Value, bool IsRefreshGrid)
        {
            _SQLControl.UpdateByDefulteExtra(SQL_Table.GetTableName(), serchColumnName, serchValue, Value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }


        public void SQL_Delete(string serchValue, bool IsRefreshGrid)
        {
            _SQLControl.DeleteByDefult(SQL_Table.GetTableName(), "GUID", serchValue);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_Delete(object[] serchColumnName, object[] serchValue, bool IsRefreshGrid)
        {
            _SQLControl.DeleteByDefult(SQL_Table.GetTableName(), ToStrArray(serchColumnName), ToStrArray(serchValue));
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_Delete(string serchColumnName, string serchValue, bool IsRefreshGrid)
        {
            _SQLControl.DeleteByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_Delete(int serchColumnindex, object[] serchValue, bool IsRefreshGrid)
        {
            _SQLControl.DeleteByDefult(SQL_Table.GetTableName(), serchColumnindex, ToStrArray(serchValue));
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_Delete(int serchColumnindex, string serchValue, bool IsRefreshGrid)
        {
            _SQLControl.DeleteByDefult(SQL_Table.GetTableName(), serchColumnindex, serchValue);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_DeleteByLike(int serchColumnindex, string LikeString, bool IsRefreshGrid)
        {
            _SQLControl.DeleteByDefult(SQL_Table.GetTableName(), serchColumnindex, LikeString);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_DeleteByLike(string serchColumnName, string LikeString, bool IsRefreshGrid)
        {
            _SQLControl.DeleteByDefult(SQL_Table.GetTableName(), serchColumnName, LikeString);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_DeleteByBetween(int serchColumnindex, string brtween_value1, string brtween_value2, bool IsRefreshGrid)
        {
            _SQLControl.DeleteByBetween(SQL_Table.GetTableName(), serchColumnindex, brtween_value1, brtween_value2);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_DeleteByBetween(string serchColumnName, string brtween_value1, string brtween_value2, bool IsRefreshGrid)
        {
            _SQLControl.DeleteByBetween(SQL_Table.GetTableName(), serchColumnName, brtween_value1, brtween_value2);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_DeleteByIn(int serchColumnindex, string IN_value, bool IsRefreshGrid)
        {
            _SQLControl.DeleteByIn(SQL_Table.GetTableName(), serchColumnindex, IN_value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_DeleteByIn(string serchColumnName, string IN_value, bool IsRefreshGrid)
        {
            _SQLControl.DeleteByIn(SQL_Table.GetTableName(), serchColumnName, IN_value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_DeleteByIn(int serchColumnindex, string[] IN_value, bool IsRefreshGrid)
        {
            _SQLControl.DeleteByIn(SQL_Table.GetTableName(), serchColumnindex, IN_value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_DeleteByIn(string[] serchColumnName, string[] IN_value, bool IsRefreshGrid)
        {
            _SQLControl.DeleteByIn(SQL_Table.GetTableName(), serchColumnName, IN_value);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }

        public void SQL_DeleteExtra(List<object[]> Value, bool IsRefreshGrid)
        {
            List<object> serchValue = new List<object>();
            for (int i = 0; i < Value.Count; i++)
            {
                serchValue.Add(Value[i][0]);
            }
            SQL_DeleteExtra(serchValue, IsRefreshGrid);
        }
        public void SQL_DeleteExtra(List<string> serchValue, bool IsRefreshGrid)
        {
            List<object> _serchValue = new List<object>();
            for(int i = 0; i < serchValue.Count; i++)
            {
                _serchValue.Add(serchValue[i]);
            }
            SQL_DeleteExtra("GUID", _serchValue, IsRefreshGrid);
        }
        public void SQL_DeleteExtra(List<object> serchValue, bool IsRefreshGrid)
        {
            SQL_DeleteExtra("GUID", serchValue, IsRefreshGrid);
        }
        public void SQL_DeleteExtra(string serchColumnName, List<object> serchValue, bool IsRefreshGrid)
        {
            _SQLControl.DeleteExtra(SQL_Table.GetTableName(), serchColumnName, serchValue);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_DeleteExtra(string[] serchColumnName, List<object[]> serchValue, bool IsRefreshGrid)
        {
            _SQLControl.DeleteExtra(SQL_Table.GetTableName(), serchColumnName, serchValue);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_DeleteExtra( List<object[]> serchColumnName, List<object[]> serchValue, bool IsRefreshGrid)
        {
            _SQLControl.DeleteExtra(SQL_Table.GetTableName(), serchColumnName, serchValue);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }
        public void SQL_DeleteExtra(List<string[]> serchColumnName, List<string[]> serchValue, bool IsRefreshGrid)
        {
            _SQLControl.DeleteExtra(SQL_Table.GetTableName(), serchColumnName, serchValue);
            if (IsRefreshGrid) SQL_GetAllRows(true);
        }

        public double SQL_GetAVG(string ColumnName)
        {
            return _SQLControl.GetAVG(SQL_Table.GetTableName(), ColumnName); ;
        }
        public double SQL_GetAVG_ByBetween(string ColumnName, string serchColumnName, string brtween_value1, string brtween_value2)
        {
            return _SQLControl.GetAVG_ByBetween(SQL_Table.GetTableName(), ColumnName, serchColumnName, brtween_value1, brtween_value2);
        }
        public double SQL_GetAVG_ByIN(string ColumnName, string serchColumnName, string[] IN_value)
        {
            return _SQLControl.GetAVG_ByIN(SQL_Table.GetTableName(), ColumnName, serchColumnName, IN_value);
        }

        public double SQL_GetCOUNT(string ColumnName, bool GetNullbool)
        {
            return _SQLControl.GetCOUNT(SQL_Table.GetTableName(), ColumnName, GetNullbool); ;
        }
        public double SQL_GetCOUNT_ByBetween(string ColumnName, string serchColumnName, string brtween_value1, string brtween_value2)
        {
            return _SQLControl.GetCOUNT_ByBetween(SQL_Table.GetTableName(), ColumnName, serchColumnName, brtween_value1, brtween_value2);
        }
        public double SQL_GetCOUNT_ByIN(string ColumnName, string serchColumnName, string[] IN_value)
        {
            return _SQLControl.GetCOUNT_ByIN(SQL_Table.GetTableName(), ColumnName, serchColumnName, IN_value);
        }

        public double SQL_GetMAX(string ColumnName)
        {
            return _SQLControl.GetMAX(SQL_Table.GetTableName(), ColumnName); ;
        }
        public double SQL_GetMAX_ByBetween(string ColumnName, string serchColumnName, string brtween_value1, string brtween_value2)
        {
            return _SQLControl.GetMAX_ByBetween(SQL_Table.GetTableName(), ColumnName, serchColumnName, brtween_value1, brtween_value2);
        }
        public double SQL_GetMAX_ByIN(string ColumnName, string serchColumnName, string[] IN_value)
        {
            return _SQLControl.GetMAX_ByIN(SQL_Table.GetTableName(), ColumnName, serchColumnName, IN_value);
        }

        public double SQL_GetMIN(string ColumnName)
        {
            return _SQLControl.GetMIN(SQL_Table.GetTableName(), ColumnName); ;
        }
        public double SQL_GetMIN_ByBetween(string ColumnName, string serchColumnName, string brtween_value1, string brtween_value2)
        {
            return _SQLControl.GetMIN_ByBetween(SQL_Table.GetTableName(), ColumnName, serchColumnName, brtween_value1, brtween_value2);
        }
        public double SQL_GetMIN_ByIN(string ColumnName, string serchColumnName, string[] IN_value)
        {
            return _SQLControl.GetMIN_ByIN(SQL_Table.GetTableName(), ColumnName, serchColumnName, IN_value);
        }

        public double SQL_GetSUM(string ColumnName)
        {
            return _SQLControl.GetSUM(SQL_Table.GetTableName(), ColumnName); ;
        }
        public double SQL_GetSUM_ByBetween(string ColumnName, string serchColumnName, string brtween_value1, string brtween_value2)
        {
            return _SQLControl.GetSUM_ByBetween(SQL_Table.GetTableName(), ColumnName, serchColumnName, brtween_value1, brtween_value2);
        }
        public double SQL_GetSUM_ByIN(string ColumnName, string serchColumnName, string[] IN_value)
        {
            return _SQLControl.GetSUM_ByIN(SQL_Table.GetTableName(), ColumnName, serchColumnName, IN_value);
        }

        public bool SQL_IsHaveMember(string serchColumnName, string serchValue)
        {
            return (this.SQL_GetRows(serchColumnName, serchValue, false).Count >= 1);
        }
        public bool SQL_IsHaveMember(int serchColumnindex, string serchValue)
        {
            return (this.SQL_GetRows(serchColumnindex, serchValue, false).Count >= 1);
        }

        public List<object[]> SQL_GetAllImageRows(bool IsRefreshGrid)
        {
            List<object[]> obj_list = new List<object[]>();
            object[] Name = this.SQL_GetAllImageName();
            object[] Date = this.SQL_GetAllImageDate();
            if (Name.Length == Date.Length)
            {
                for (int i = 0; i < Name.Length; i++)
                {
                    obj_list.Add(new object[] { Name[i], "", Date[i] });
                }
            }
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(obj_list);
            return obj_list;
        }
        public List<object[]> SQL_GetAllImageRows(bool IsRefreshGrid, bool IsGetImage)
        {
            List<object[]> obj_list = new List<object[]>();
            if (!IsGetImage) obj_list = this.SQL_GetAllImageRows(false);
            else
            {
                List<object[]> obj_list_buf = new List<object[]>();
                object[] obj_array_buf;
                obj_list = this.SQL_GetAllRows(false);
                foreach (object[] obj_array in obj_list)
                {
                    obj_array_buf = new object[obj_array.Length];
                    obj_array_buf[0] = obj_array[0];
                    obj_array_buf[1] = obj_array[1];
                    obj_array_buf[2] = obj_array[2];
                    obj_list_buf.Add(obj_array_buf);
                }
                obj_list = obj_list_buf.DeepClone();
            }
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(obj_list);
            return obj_list;
        }
        public bool SQL_CheckServerImageNew(string ImageName, string TIMESTAMP)
        {
            return _SQLControl.CheckServerImageNew(SQL_Table.GetTableName(), ImageName, TIMESTAMP);
        }
        public string SQL_GetImageDate(string ImageName)
        {
            return _SQLControl.GetImageDate(SQL_Table.GetTableName(), ImageName);
        }
        public object[] SQL_GetAllImageName()
        {
            return _SQLControl.GetAllImageName(SQL_Table.GetTableName());
        }
        public object[] SQL_GetAllImageDate()
        {
            return _SQLControl.GetAllImageDate(SQL_Table.GetTableName());
        }
        public bool SQL_IsHaveImageName(string ImageName)
        {
            return _SQLControl.IsHaveImageName(SQL_Table.GetTableName(), ImageName);
        }
        public void SQL_DeleteImage(string ImageName)
        {
            _SQLControl.DeleteImage(SQL_Table.GetTableName(), ImageName);
        }
        public void SQL_DeleteImage(string ImageName, string TIMESTAMP)
        {
            _SQLControl.DeleteImage(SQL_Table.GetTableName(), ImageName, TIMESTAMP);
        }
        public void SQL_UploadImage(string ImageName, Image Image)
        {
            _SQLControl.UploadImage(SQL_Table.GetTableName(), ImageName, Image);
        }
        public Bitmap SQL_DownloadImage(string ImageName)
        {
            return _SQLControl.DownloadImage(SQL_Table.GetTableName(), ImageName);
        }
        public Bitmap GetImage(string ImageName)
        {
            object[] object_buf;
            for (int i = 0; i < SaveDataVal.RowsList.Count; i++)
            {
                object_buf = SaveDataVal.RowsList[i].ToArray();
                if ((string)object_buf[0] == ImageName)
                {
                    return (Bitmap)object_buf[1];
                }
            }
            return null;
        }

        public void DataGrid_Init(bool IsInvoke)
        {
            if (IsInvoke)
            {
                dataGridView.Invoke(new Action(delegate
                {
                    DataGrid_Init();
                }));
            }
            else
            {
                DataGrid_Init();
            }
        }
        private bool IsColumnsText(string colName)
        {
            foreach (ColumnElement columns in Columns)
            {
                if(columns.Text == colName)
                {
                    return (columns.OtherType != Table.OtherType.IMAGE);
                }
            }
            return true;
        }
        private void DataGrid_Init()
        {
            dataGridView.Columns.Clear();
            dataTable = new DataTable();
            foreach (ColumnElement columns in Columns)
            {
                if(columns.OtherType == Table.OtherType.IMAGE)
                {
                    dataTable.Columns.Add(new DataColumn(columns.Text, typeof(Image)));
                    continue;
                }
                dataTable.Columns.Add(new DataColumn(columns.Text , typeof(string)));
            }
            dataGridView.DataSource = dataTable;
            dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView.ReadOnly = false;
            foreach (ColumnElement columns in Columns)
            {
                //DataGridViewColumn dataGridViewColumn = new DataGridViewColumn();
                dataGridView.Columns[$"{columns.Text}"].DefaultCellStyle.BackColor = columns.BackgroundColor;
                dataGridView.Columns[$"{columns.Text}"].Width = columns.Width;
                dataGridView.Columns[$"{columns.Text}"].SortMode = columns.SortMode;
                dataGridView.Columns[$"{columns.Text}"].DefaultCellStyle.Alignment = columns.Alignment;
                dataGridView.Columns[$"{columns.Text}"].Visible = columns.Visable;
                dataGridView.Columns[$"{columns.Text}"].ReadOnly = !columns.CanEdit;
          
                if (columns.OtherType == Table.OtherType.IMAGE)
                {
                    ((DataGridViewImageColumn)dataGridView.Columns[$"{columns.Text}"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                }
            }

            if(_顯示CheckBox)
            {
                DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
                dataGridViewCheckBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dataGridViewCheckBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCheckBoxColumn.FlatStyle = FlatStyle.Standard;
                dataGridView.Columns.Insert(0, dataGridViewCheckBoxColumn);
                dataGridViewCheckBoxColumn.ReadOnly = false;

                dataGridView.Controls.Clear();
                if (checkBoxHeader == null) checkBoxHeader = new CheckBox();
                checkBoxHeader.BackColor = this.columnHeaderBackColor;
                checkBoxHeader.Name = "checkBoxHeader";
                checkBoxHeader.Size = new Size(18, 18);
                checkBoxHeader.Visible = false;
                dataGridView.Controls.Add(checkBoxHeader);
            }

            if (this.DesignMode)
            {
                object[] value = new object[dataGridView.Columns.Count];
                if (_顯示CheckBox) value = new object[dataGridView.Columns.Count - 1];
              
                for (int k = 0; k < Columns.Count; k++)
                {
                    if (Columns[k].OtherType == Table.OtherType.IMAGE) value[k] = null;
                    else value[k] = "#######";

                }
                dataTable.Rows.Add(value);
                dataTable.Rows.Add(value);
            }
            dataGridView.ColumnHeadersBorderStyle = _首行樣式;
            dataGridView.RowHeadersBorderStyle = _首列樣式;
            dataGridView.BorderStyle = _邊框樣式;
            dataGridView.CellBorderStyle = _單格樣式;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView.RowTemplate.Height = this.RowsHeight;
            if(自動換行)
            {
                dataGridView.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            else
            {
                dataGridView.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }
            dataGridView.RowHeadersVisible = _顯示首列;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = this.可選擇多列;
            dataGridView.AllowUserToResizeColumns = this.可拖曳欄位寬度;
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView.ClearSelection();
            //dataGridView.AdvancedRowHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.InsetDouble;
            dataGridView.Refresh();
        }
        public int GetSelectRow()
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView.Rows[i].Selected)
                {
                    return i;
                }
            }
            return -1;
        }
        public List<int> GetSelectRowsIndex()
        {
            List<int> list_value = new List<int>();
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView.Rows[i].Selected)
                {
                    list_value.Add(i);
                }
            }
            return list_value;
        }
        public void SetSelectRow(string[] ColumnName, string[] value)
        {
            if (ColumnName.Length != value.Length) return;
            List<int> list_col_index = new List<int>();
            for (int i = 0; i < ColumnName.Length; i++)
            {
                int temp = this.GetColumnIndex(ColumnName[i]);
                if (temp == -1) return;
                list_col_index.Add(temp);
            }
            bool flag_ok = false;
            for (int i = 0; i < SaveDataVal.RowsList.Count; i++)
            {
                flag_ok = true;
                for (int k = 0; k < list_col_index.Count; k++)
                {
                    int col_index = list_col_index[k];
                    if (SaveDataVal.RowsList[i][col_index].ObjectToString() != value[k])
                    {
                        flag_ok = false;                      
                    }
                }
                if(flag_ok == true) SetSelectRow(i);
            }
        }
        public void SetSelectRow(string ColumnName , string value)
        {
            int col_index = this.GetColumnIndex(ColumnName);
            if (col_index == -1) return;
            for (int i = 0; i < SaveDataVal.RowsList.Count; i++)
            {
                if (SaveDataVal.RowsList[i][col_index].ObjectToString() == value)
                {
                    SetSelectRow(i);
                    return;
                }
            }    
        }
        public void SetSelectRow(int index)
        {
            this.Invoke(new Action(delegate
            {
                if (index < dataGridView.Rows.Count)
                {
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        dataGridView.Rows[i].Selected = false;
                    }
                    dataGridView.Rows[index].Selected = true;
                }
            }));
          
            if (RowEnterEvent != null) RowEnterEvent(this.GetRowValues(index));
        }

        public void ClearSelection()
        {
            this.Invoke(new Action(delegate { dataGridView.ClearSelection(); }));

        }
        public object[] GetRowValues()
        {
            return GetRowValues(GetSelectRow());
        }
        public object[] GetRowValues(int Index)
        {
            if (Index < SaveDataVal.RowsList.Count && Index >= 0)
            {
                return SaveDataVal.RowsList[Index].ToArray();
            }
            return null;
        }
   
        public List<object> GetColumnValues(string ColumnName, List<object[]> List_value)
        {
            List<object> list_value = new List<object>();
            int index = this.GetColumnIndex(ColumnName);
            string str_temp = "";
            if (index != -1)
            {
                bool flag_eqal = false;
                foreach (object[] obj_temp in List_value)
                {
                    if (obj_temp[index] is string)
                    {
                        str_temp = obj_temp[index].ObjectToString();
                    }
                    else if (obj_temp[index] is DateTime)
                    {
                        str_temp = obj_temp[index].ToDateTimeString();
                    }
                    flag_eqal = false;
                    foreach (object value in list_value)
                    {
                        if (obj_temp[index] is string)
                        {
                            if (value.ObjectToString() == str_temp)
                            {
                                flag_eqal = true;
                                break;
                            }
                        }
                        else if (obj_temp[index] is DateTime)
                        {
                            if (value.ToDateTimeString() == str_temp)
                            {
                                flag_eqal = true;
                                break;
                            }
                        }

                    }
                    if (!flag_eqal)
                    {
                        list_value.Add(str_temp);
                    }

                }
            }

            return list_value;
        }
        public List<object> GetColumnValues(string ColumnName)
        {
            return this.GetColumnValues(ColumnName, this.SaveDataVal.RowsList);
        }

        public List<object[]> Get_All_Checked_RowsValues()
        {
            List<object[]> list_value = new List<object[]>();
            if (this._顯示CheckBox == false) return list_value;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView[0, i].Value != null)
                {
                    if ((bool)dataGridView[0, i].Value == true) list_value.Add(this.GetRowValues(i));
                }
            }
            return list_value;
        }
        public List<object[]> Get_All_Select_RowsValues()
        {
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView.Rows[i].Selected)
                {
                    list_value.Add(this.GetRowValues(i));
                }
            }
            return list_value;
        }

        public void AddRow(object[] Value, bool IsRefreshGrid)
        {
            List<object[]> Values = new List<object[]>();
            Values.Add(Value);
            this.AddRows(Values, IsRefreshGrid);
        }
        public void AddRows(List<object[]> Values, bool IsRefreshGrid)
        {
            foreach (object[] obj_temp in Values)
            {
                SaveDataVal.RowsList.Add(obj_temp);
            }
            if (IsRefreshGrid) this.RowsChange(SaveDataVal.RowsList, this.AutoSelectToDeep);
        }

        public string[] GetAllColumn_Name()
        {
            return this.GetAllColumn_Name(false);
        }
        public string[] GetAllColumn_Name(bool remove_unVisable)
        {
            List<string> string_list = new List<string>();

            for (int i = 0; i < this.dataGridView.ColumnCount; i++)
            {
                if(_顯示CheckBox)
                {
                    if (i == 0) continue;
                }
                if (remove_unVisable)
                {
                    if (this.dataGridView.Columns[i].Visible)
                    {
                        string_list.Add(this.dataGridView.Columns[i].Name);
                    }
                }
                else
                {
                    string_list.Add(this.dataGridView.Columns[i].Name);
                }
            }
            return string_list.ToArray();
        }
        public string GetColumnName(int Index)
        {
            return SQL_Table.GetColumnName(Index);
        }
        public int GetColumnIndex(string ColumnName)
        {
            object[] AllColumName = GetAllColumn_Name();
            int index = 0;
            foreach (object temp in AllColumName)
            {
                if ((string)temp == ColumnName) return index;
                index++;
            }
            return -1;
        }

        public List<object[]> GetRows(int serchColumnindex, object serchValue, bool IsRefreshGrid)
        {
            return this.GetRows(new string[] { this.GetColumnName(serchColumnindex) }, new object[] { serchValue }, IsRefreshGrid);
        }
        public List<object[]> GetRows(string serchColumnName, object serchValue, bool IsRefreshGrid)
        {
            return this.GetRows(new string[] { serchColumnName }, new object[] { serchValue }, IsRefreshGrid);
        }
        public List<object[]> GetRows(string[] serchColumnName, object[] serchValue, bool IsRefreshGrid)
        {
            List<int> index_list = new List<int>();
            List<object[]> RowsList_buf = new List<object[]>();
            bool flag_equal = false;
            for (int i = 0; i < serchColumnName.Length; i++)
            {
                index_list.Add(this.GetColumnIndex(serchColumnName[i]));
            }
            int list_index = 0;
            foreach (object[] obj_array in SaveDataVal.RowsList)
            {
                flag_equal = true;
                for (int i = 0; i < index_list.Count; i++)
                {
                    if (index_list[i] < obj_array.Length)
                    {
                        if ((obj_array[index_list[i]] is System.DBNull)) obj_array[index_list[i]] = "";

                        if ((string)obj_array[index_list[i]] != (string)serchValue[i])
                        {
                            flag_equal = false;
                        }


                    }
                }
                if (flag_equal)
                {
                    RowsList_buf.Add(obj_array);
                }
                list_index++;
            }
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(RowsList_buf);
            return RowsList_buf;
        }
        public List<object[]> GetAllRows()
        {
            return this.SaveDataVal.RowsList;
        }

        public void CheckedAll()
        {
            for(int i = 0; i < dataGridView.Rows.Count; i++)
            {
                dataGridView[0, i].Value = true;
            }
        }
        public void UnCheckedAll()
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                dataGridView[0, i].Value = false;
            }
        }
        public void Set_Checked(int index, bool value)
        {
            if (index >= dataGridView.Rows.Count) return;
            dataGridView[0, index].Value = value;
        }

        public bool IsHaveMember(int serchColumnindex, string serchValue)
        {
            return (this.GetRows(serchColumnindex, serchValue, false).Count > 0);
        }
        public bool IsHaveMember(string[] serchColumnName, string[] serchValue)
        {
            return (this.GetRows(serchColumnName, serchValue, false).Count > 0);
        }
        public bool IsHaveMember(string serchColumnName, string serchValue)
        {
            return (this.GetRows(serchColumnName, serchValue, false).Count > 0);
        }

        public void Replace(object[] Value, bool IsRefreshGrid)
        {
            this.Replace(this.GetSelectRow(), Value, IsRefreshGrid);
        }
        public void Replace(int SelectIndex, object[] Value, bool IsRefreshGrid)
        {
            if (SelectIndex != -1)
            {
                this.SaveDataVal.RowsList[SelectIndex] = Value;
                if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList);
            }

        }
        public void ReplaceExtra(object[] Value, bool IsRefreshGrid)
        {
            List<object[]> list_value = new List<object[]>();
            list_value.Add(Value);
            this.ReplaceExtra(list_value, IsRefreshGrid);
        }
        public void ReplaceExtra( List<object[]> list_value, bool IsRefreshGrid)
        {
            List<object[]> serchValue = new List<object[]>();
            for (int i = 0; i < list_value.Count; i++)
            {
                serchValue.Add(new object[] { list_value[i][0].ObjectToString() });
            }
            this.ReplaceExtra(new string[] { "GUID" }, serchValue, list_value, IsRefreshGrid);
        }
        public void ReplaceExtra(string[] ColumnName, List<string[]> serchValue, List<object[]> list_value, bool IsRefreshGrid)
        {
            List<object[]> _serchValue = new List<object[]>();
            for (int i = 0; i < serchValue.Count; i++)
            {
                object[] value = new object[serchValue[i].Length];
                for (int k = 0; k < serchValue[i].Length; k++)
                {
                    value[k] = serchValue[i][k];
                }
                _serchValue.Add(value);
            }
            this.ReplaceExtra(ColumnName, _serchValue, list_value, IsRefreshGrid);
        }
        public void ReplaceExtra(string[] ColumnName , List<object[]> serchValue , List<object[]> list_value, bool IsRefreshGrid)
        {
            List<int> index_list = new List<int>();
            List<object[]> RowsList_buf = SaveDataVal.RowsList.DeepClone();
            List<object[]> list_value_buf = new List<object[]>();
            for (int i = 0; i < ColumnName.Length; i++)
            {
                index_list.Add(this.GetColumnIndex(ColumnName[i]));
            }
            int list_index = 0;

            for (int i = 0; i < serchValue.Count; i++)
            {
                list_value_buf = RowsList_buf;
                for (int k = 0; k < serchValue[i].Length; k++)
                {
                    list_index = index_list[k];
                    list_value_buf = (from value in list_value_buf
                                      where value[list_index].ObjectToString() == serchValue[i][k].ObjectToString()
                                      select value).ToList();
                }
                if (list_value_buf.Count > 0)
                {
                    list_value[i].CopyRowTo(list_value_buf[0]);
                }
            }
            
            SaveDataVal.RowsList = RowsList_buf.DeepClone();
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList);
        }
        public void Replace(int ColumnIndex, object serchValue, object[] Value, bool IsRefreshGrid)
        {
            this.Replace(new string[] { this.GetColumnName(ColumnIndex) }, new object[] { serchValue }, Value, IsRefreshGrid);
        }
        public void Replace(string ColumnName, object serchValue, object[] Value, bool IsRefreshGrid)
        {
            this.Replace(new string[] { ColumnName }, new object[] { serchValue }, Value, IsRefreshGrid);
        }
        public void Replace(string[] ColumnName, object[] serchValue, object[] Value, bool IsRefreshGrid)
        {
            List<int> index_list = new List<int>();
            List<object[]> RowsList_buf = SaveDataVal.RowsList.DeepClone();
            bool flag_equal = false;
            for (int i = 0; i < ColumnName.Length; i++)
            {
                index_list.Add(this.GetColumnIndex(ColumnName[i]));
            }
            int list_index = 0;
            string Sourcedata = "";
            string Targetdata = "";
            foreach (object[] obj_array in SaveDataVal.RowsList)
            {
                flag_equal = true;
                for (int i = 0; i < index_list.Count; i++)
                {
                    if (index_list[i] < obj_array.Length)
                    {
                        if (obj_array[index_list[i]] is string) Sourcedata = obj_array[index_list[i]].ObjectToString();
                        else if (obj_array[index_list[i]] is DateTime) Sourcedata = obj_array[index_list[i]].ToDateString();

                        if (serchValue[i] is string) Targetdata = serchValue[i].ObjectToString();
                        else if (serchValue[i] is DateTime) Targetdata = serchValue[i].ToDateString();

                        if (Sourcedata != Targetdata)
                        {
                            flag_equal = false;
                        }
                    }
                }
                if (flag_equal)
                {
                    RowsList_buf[list_index] = Value;
                }
                else
                {

                }
                list_index++;
            }
            SaveDataVal.RowsList = RowsList_buf.DeepClone();
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList);
        }


        public void DeleteExtra(object[] Value, bool IsRefreshGrid)
        {
            List<object[]> list_value = new List<object[]>();
            list_value.Add(Value);
            this.DeleteExtra(list_value, IsRefreshGrid);
        }
        public void DeleteExtra(List<object[]> list_value, bool IsRefreshGrid)
        {
            List<object[]> serchValue = new List<object[]>();
            for (int i = 0; i < list_value.Count; i++)
            {
                serchValue.Add(new object[] { list_value[i][0].ObjectToString() });
            }
            this.DeleteExtra(new string[] { "GUID" }, serchValue, IsRefreshGrid);
        }
        public void DeleteExtra(string[] ColumnName, List<object[]> serchValue, bool IsRefreshGrid)
        {
            List<int> index_list = new List<int>();
            List<object[]> RowsList_buf = SaveDataVal.RowsList.DeepClone();
            List<object[]> list_value_buf = new List<object[]>();
            for (int i = 0; i < ColumnName.Length; i++)
            {
                index_list.Add(this.GetColumnIndex(ColumnName[i]));
            }
            int list_index = 0;

            for (int i = 0; i < serchValue.Count; i++)
            {
                for (int k = 0; k < serchValue[i].Length; k++)
                {
                    list_index = index_list[k];
                    list_value_buf = (from value in RowsList_buf
                                      where value[list_index].ObjectToString() == serchValue[i][k].ObjectToString()
                                      select value).ToList();
                }
                if (list_value_buf.Count > 0)
                {
                    RowsList_buf.Remove(list_value_buf[0]);
                }
            }

            SaveDataVal.RowsList = RowsList_buf.DeepClone();
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList);
        }

        public void Delete(bool IsRefreshGrid)
        {
            List<object[]> list_remove_buf = new List<object[]>();
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView.Rows[i].Selected)
                {
                    list_remove_buf.Add(this.SaveDataVal.RowsList[i]);
                }
            }
            foreach (object[] value in list_remove_buf)
            {
                this.SaveDataVal.RowsList.Remove(value);
            }
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList);
        }
        public void Delete(int SelectIndex, bool IsRefreshGrid)
        {
            this.SaveDataVal.RowsList.RemoveAt(SelectIndex);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList);
        }
        public void Delete(int ColumnIndex, object serchValue, bool IsRefreshGrid)
        {
            this.Delete(new string[] { this.GetColumnName(ColumnIndex) }, new object[] { serchValue }, IsRefreshGrid);
        }
        public void Delete(string ColumnName, object serchValue, bool IsRefreshGrid)
        {
            this.Delete(new string[] { ColumnName }, new object[] { serchValue }, IsRefreshGrid);
        }
        public void Delete(string[] ColumnName, object[] serchValue, bool IsRefreshGrid)
        {
            List<int> index_list = new List<int>();
            List<object[]> RowsList_buf = new List<object[]>();
            bool flag_equal = false;
            for (int i = 0; i < ColumnName.Length; i++)
            {
                index_list.Add(this.GetColumnIndex(ColumnName[i]));
            }
            int list_index = 0;
            string Sourcedata = "";
            string Targetdata = "";
            foreach (object[] obj_array in SaveDataVal.RowsList)
            {
                flag_equal = true;
                for (int i = 0; i < index_list.Count; i++)
                {
                    if (index_list[i] < obj_array.Length)
                    {
                        if (obj_array[index_list[i]] is string) Sourcedata = obj_array[index_list[i]].ObjectToString();
                        else if (obj_array[index_list[i]] is DateTime) Sourcedata = obj_array[index_list[i]].ToDateString();

                        if (serchValue[i] is string) Targetdata = serchValue[i].ObjectToString();
                        else if (serchValue[i] is DateTime) Targetdata = serchValue[i].ToDateString();

                        if (Sourcedata != Targetdata)
                        {
                            flag_equal = false;
                        }
                    }
                }
                if (!flag_equal)
                {
                    RowsList_buf.Add(obj_array);
                }
                else
                {

                }
                list_index++;
            }
            SaveDataVal.RowsList = RowsList_buf.DeepClone();
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList);
        }

        public void Set_ColumnVisible(string ColumnName, bool Visible)
        {
            DataGridViewColumnCollection columns = this.dataGridView.Columns;
            this.Invoke(new Action(delegate {
                for (int i = 0; i < columns.Count; i++)
                {
                    if (columns[i].Name == ColumnName)
                    {
                        columns[i].Visible = Visible;
                    }
                }
            }));

        }
        public void Set_ColumnVisible(bool visable, params object[] Enum)
        {
            string[] array = Enum.GetEnumNames();
            Set_ColumnVisible(visable, array);
        }
        public void Set_ColumnVisible(bool visable, params string[] name)
        {
            for (int i = 0; i < name.Length; i++)
            {
                for (int k = 0; k < this.Columns.Count; k++)
                {
                    if(this.Columns[k].Name == name[i])
                    {
                        Columns[k].Visable = visable;
                    }
                }             
            }
            foreach (ColumnElement columns in Columns)
            {
                dataGridView.Columns[$"{columns.Text}"].DefaultCellStyle.BackColor = columns.BackgroundColor;
                dataGridView.Columns[$"{columns.Text}"].Width = columns.Width;
                dataGridView.Columns[$"{columns.Text}"].SortMode = columns.SortMode;
                dataGridView.Columns[$"{columns.Text}"].DefaultCellStyle.Alignment = columns.Alignment;
                dataGridView.Columns[$"{columns.Text}"].Visible = columns.Visable;
                dataGridView.Columns[$"{columns.Text}"].ReadOnly = !columns.CanEdit;
            }
        }
        public void Set_ColumnWidth(int width, object Enum)
        {
            this.Set_ColumnWidth(width, Enum.GetEnumName());
        }
        public void Set_ColumnWidth(int width, string name)
        {
            for (int i = 0; i < name.Length; i++)
            {
                for (int k = 0; k < this.Columns.Count; k++)
                {
                    if (this.Columns[k].Name == name)
                    {
                        Columns[k].Width = width;
                        Columns[k].Visable = true;
                    }
                }
            }
            foreach (ColumnElement columns in Columns)
            {
                dataGridView.Columns[$"{columns.Text}"].DefaultCellStyle.BackColor = columns.BackgroundColor;
                dataGridView.Columns[$"{columns.Text}"].Width = columns.Width;
                dataGridView.Columns[$"{columns.Text}"].SortMode = columns.SortMode;
                dataGridView.Columns[$"{columns.Text}"].DefaultCellStyle.Alignment = columns.Alignment;
                dataGridView.Columns[$"{columns.Text}"].Visible = columns.Visable;
                dataGridView.Columns[$"{columns.Text}"].ReadOnly = !columns.CanEdit;
            }
        }
        public void Set_ColumnWidth(int width, DataGridViewContentAlignment alignment, object Enum)
        {
            this.Set_ColumnWidth(width, alignment, Enum.GetEnumName());
        }
        public void Set_ColumnWidth(int width, DataGridViewContentAlignment alignment, string name)
        {
            for (int i = 0; i < name.Length; i++)
            {
                for (int k = 0; k < this.Columns.Count; k++)
                {
                    if (this.Columns[k].Name == name)
                    {
                        Columns[k].Width = width;
                        Columns[k].Visable = true;
                        Columns[k].Alignment = alignment;
                        Columns[k].CanEdit = false;
                    }
                }
            }
            foreach (ColumnElement columns in Columns)
            {
                dataGridView.Columns[$"{columns.Text}"].DefaultCellStyle.BackColor = columns.BackgroundColor;
                dataGridView.Columns[$"{columns.Text}"].Width = columns.Width;
                dataGridView.Columns[$"{columns.Text}"].SortMode = columns.SortMode;
                dataGridView.Columns[$"{columns.Text}"].DefaultCellStyle.Alignment = columns.Alignment;
                dataGridView.Columns[$"{columns.Text}"].Visible = columns.Visable;
                dataGridView.Columns[$"{columns.Text}"].ReadOnly = !columns.CanEdit;
            }
        }


        public void Set_ColumnHeaderHeight(int height)
        {
            this.columnHeadersHeight = height;
        }
       

        public void ClearGrid()
        {
            List<object[]> RowsList = new List<object[]>();
            SaveDataVal.RowsList = RowsList;
            if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList);
        }
        public void RefreshGrid(List<object[]> RowsList)
        {
            SaveDataVal.RowsList = RowsList.DeepClone();
            if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList);
        }
        public void Set_Columns_NoSort()
        {
            foreach (DataGridViewColumn col in this.dataGridView.Columns)
            {
                if (col.SortMode == DataGridViewColumnSortMode.Automatic)
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }
        public void RefreshGrid()
        {
            this.RefreshGrid(this.SaveDataVal.RowsList);
        }
        public List<object[]> GetRowsList()
        {
            return this.SaveDataVal.RowsList;
        }
        public void SetRowsList(List<object[]> RowsList)
        {
            List<object[]> RowsList_buf = new List<object[]>();
            foreach (object[] obj in RowsList)
            {
                RowsList_buf.Add(obj);
            }
            SaveDataVal.RowsList = RowsList_buf;
        }
        public List<object[]> RowsChangeFunction(List<object[]> listValue)
        {
            if (this.DataGridRowsChangeEvent != null)
            {
                this.DataGridRowsChangeEvent(listValue);
            }
            if (this.DataGridRowsChangeRefEvent != null)
            {
                this.DataGridRowsChangeRefEvent(ref listValue);
            }
            return listValue;
        }


        public List<object[]> GetOtherSaveList()
        {
            return this.SaveDataVal.OtherSaveList;
        }
        public void SetOtherSaveList(List<object[]> OtherSaveList)
        {
            this.SaveDataVal.OtherSaveList = OtherSaveList.DeepClone();
        }

        public void UploadToSQL()
        {
            this.SQL_CreateTable();
            this.SQL_AddRows(SaveDataVal.RowsList, true);
        }
        public void SaveData()
        {
            this.SaveData(this._SaveFileName);
        }
        public void SaveData(string FileName)
        {
            FileIO.SaveProperties((object)SaveDataVal, FileName + Extension);
        }
        public void LoadData()
        {
            this.LoadData(this._SaveFileName);
        }
        public void LoadData(string FileName)
        {
            SaveDataValClass _SaveDataValClass;
            object FileSaveClass_buf = new object();
            FileIO.LoadProperties(ref FileSaveClass_buf, FileName + Extension);
            try
            {
                _SaveDataValClass = (SaveDataValClass)FileSaveClass_buf;
                if (_SaveDataValClass.RowsList != null) SaveDataVal.RowsList = _SaveDataValClass.RowsList.DeepClone();
                else SaveDataVal.RowsList = new List<object[]>();
                if (_SaveDataValClass.OtherSaveList != null) SaveDataVal.OtherSaveList = _SaveDataValClass.OtherSaveList.DeepClone();
                else SaveDataVal.OtherSaveList = new List<object[]>();
            }
            catch
            {
                SaveDataVal.RowsList = new List<object[]>();
                SaveDataVal.OtherSaveList = new List<object[]>();
            }
            if (ModuleChangeEvent != null) ModuleChangeEvent(SaveDataVal.RowsList);
        }

        public string ToDATE_String(string Year, string Month, string Day)
        {
            return SQL_Table.ToDATE_String(Year, Month, Day);
        }
        public string ToDATE_String(int Year, int Month, int Day)
        {
            return SQL_Table.ToDATE_String(Year, Month, Day);
        }
        public string ToTIME_String(string Hour, string Min, string Sec)
        {
            return SQL_Table.ToTIME_String(Hour, Min, Sec);
        }
        public string ToTIME_String(int Hour, int Min, int Sec)
        {
            return SQL_Table.ToTIME_String(Hour, Min, Sec);
        }
        public string ToDATETIME_String(string Year, string Month, string Day, string Hour, string Min, string Sec)
        {
            return SQL_Table.ToDATETIME_String(Hour, Min, Sec, Hour, Min, Sec);
        }
        public string ToDATETIME_String(int Year, int Month, int Day, int Hour, int Min, int Sec)
        {
            return SQL_Table.ToDATETIME_String(Year, Month, Day, Hour, Min, Sec);
        }
        public string ToTIMESTAMP_String(string Year, string Month, string Day, string Hour, string Min, string Sec)
        {
            return SQL_Table.ToDATETIME_String(Hour, Min, Sec, Hour, Min, Sec);
        }
        public string ToTIMESTAMP_String(int Year, int Month, int Day, int Hour, int Min, int Sec)
        {
            return SQL_Table.ToDATETIME_String(Hour, Min, Sec, Hour, Min, Sec);
        }
        public string GetTimeNow(Table.DateType DateType)
        {
            return this.SQL_Table.GetTimeNow(DateType);
        }
        public string GetTimeNow()
        {
            return this.SQL_Table.GetTimeNow(Table.DateType.TIMESTAMP);
        }
        public string SQL_GetDateTimeNow_6()
        {
            return this._SQLControl.GetSQLTimeNow().Year.ToString("00") + this._SQLControl.GetSQLTimeNow().Month.ToString("00") + this._SQLControl.GetSQLTimeNow().Day.ToString("00") + this._SQLControl.GetSQLTimeNow().TimeOfDay.ToString().Replace(":", "").Replace("/", "").Replace(".", "");
        }
        public string SQL_GetTimeNow_6()
        {
            return this._SQLControl.GetSQLTimeNow().TimeOfDay.ToString().Replace(":", "").Replace("/", "").Replace(".", "");
        }

        private string[] ToStrArray(object[] objarray)
        {
            List<string> string_list = new List<string>();
            int i = 0;
            foreach (object obj in objarray)
            {
                if (obj is string) string_list.Add((string)obj);
                else if (obj is DateTime)
                {
                    object obj_buf = new object();

                    DateTime Datebuf = (DateTime)obj;
                    if (Columns[i].DateType == Table.DateType.YEAR)
                    {
                        obj_buf = Datebuf.Year.ToString("0000");
                    }
                    else if (Columns[i].DateType == Table.DateType.DATE)
                    {
                        obj_buf = Datebuf.Year.ToString("0000") + "-" + Datebuf.Month.ToString("00") + "-" + Datebuf.Day.ToString("00");
                    }
                    else if (Columns[i].DateType == Table.DateType.TIME)
                    {
                        obj_buf = Datebuf.Hour.ToString("00") + ":" + Datebuf.Minute.ToString(":") + "-" + Datebuf.Second.ToString(":");
                    }
                    else if (Columns[i].DateType == Table.DateType.DATETIME)
                    {
                        obj_buf = Datebuf.Year.ToString("0000") + "-" + Datebuf.Month.ToString("00") + "-" + Datebuf.Day.ToString("00");
                        obj_buf += " ";
                        obj_buf += Datebuf.Hour.ToString("00") + ":" + Datebuf.Minute.ToString("00") + ":" + Datebuf.Second.ToString("00");
                    }
                    else if (Columns[i].DateType == Table.DateType.TIMESTAMP)
                    {
                        obj_buf = Datebuf.Year.ToString("0000") + Datebuf.Month.ToString("00") + Datebuf.Day.ToString("00");
                        obj_buf += Datebuf.Hour.ToString("00") + Datebuf.Minute.ToString("00") + Datebuf.Second.ToString("00");
                    }



                    string_list.Add(obj_buf.ToString());
                }
                i++;
            }

            return string_list.ToArray();
        }

        private void DataGridView_Paint(object sender, PaintEventArgs e)
        {
            if(dataGridView.CanSelect && dataGridView.Rows.Count > 0)
            {
                if (_顯示CheckBox)
                {
                    checkBoxHeader.Visible = true;
                    int headerWidth = dataGridView.RowHeadersWidth;
                    int headerHeight = dataGridView.ColumnHeadersHeight;
                    checkBoxHeader.Location = new Point(headerWidth + (dataGridView.Columns[0].Width - checkBoxHeader.Width) /2, (headerHeight - checkBoxHeader.Height) / 2);
                }
            }      
        }
        private void DataGridView_DoubleClick(object sender, EventArgs e)
        {
            object[] value = this.GetRowValues();
            if (value != null)
            {
                if (this.RowDoubleClickEvent != null) this.RowDoubleClickEvent(value);
            }
        }
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int SelectRowindex = GetSelectRow();
            this.On_RowEnter(SelectRowindex);
            if (_顯示CheckBox)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 0)
                {
                    if (dataGridView[0, e.RowIndex].Value == null)
                    {
                        dataGridView[0, e.RowIndex].Value = false;
                    }
                    if ((bool)dataGridView[0, e.RowIndex].Value == true)
                    {
                        dataGridView[0, e.RowIndex].Value = !(bool)dataGridView[0, e.RowIndex].Value;
                        foreach (DataGridViewRow r in dataGridView.Rows)
                        {
                            if (r.Cells[0].Value == null) r.Cells[0].Value = false;
                            if ((bool)r.Cells[0].Value == true)
                            {
                                this.flag_unCheckedAll = false;
                                break;
                            }
                        }
                        checkBoxHeader.Checked = false;

                    }
                    else
                    {
                        bool check = true;
                        dataGridView[0, e.RowIndex].Value = !(bool)dataGridView[0, e.RowIndex].Value;
                        foreach (DataGridViewRow r in dataGridView.Rows)
                        {
                            if (r.Cells[0].Value == null) r.Cells[0].Value = false;
                            if ((bool)r.Cells[0].Value == false)
                            {
                                check = false;
                                break;
                            }
                        }
                        if(check)
                        {
                            checkBoxHeader.Checked = true;
                        }
                    }
                    this.On_CheckedChanged(SelectRowindex);
                }
          
            }
       
        }
        private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (_顯示CheckBox)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 0)
                {
                    e.PaintBackground(e.CellBounds, true);

                    ControlPaint.DrawCheckBox(e.Graphics, e.CellBounds.X + 1, e.CellBounds.Y + 1,
                        e.CellBounds.Width - 2, e.CellBounds.Height - 2,
                        (bool)e.FormattedValue ? ButtonState.Checked : ButtonState.Normal);
                    e.Handled = true;
                }
            }
          
        }
        private void DataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int SelectRowindex = GetSelectRow();
            this.On_RowEnter(SelectRowindex);
        }
        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            


        }
        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            object[] value = this.GetRowValues();
            if (value == null) return;
            string colname = dataGridView.Columns[e.ColumnIndex].Name;
            int col_index = -1;
            for (int i = 0; i < Columns.Count; i++)
            {
                if (Columns[i].Name == colname)
                {
                    col_index = i;
                    break;
                }
            }
            if (col_index == -1) return;
            value[col_index] = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
            
            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = new Font(cellStyleFont.FontFamily, cellStyleFont.Size, FontStyle.Bold);
            if (value != null)
            {
                if (this.RowEndEditEvent != null) this.RowEndEditEvent(value , e.RowIndex,e.ColumnIndex, value[col_index].ToString());
            }
            dataGridView.Rows[0].Cells[e.ColumnIndex].Selected = true;
            //dataGridView.CurrentCell = dataGridView.Rows[0].Cells[e.ColumnIndex];
        }
        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.flag_Refresh) return;
            object[] value = this.GetRowValues();
            if (value == null) return;
            string colname = dataGridView.Columns[e.ColumnIndex].Name;
            int col_index = -1;
            for (int i = 0; i < Columns.Count; i++)
            {
                if (Columns[i].Name == colname)
                {
                    col_index = i;
                    break;
                }
            }
            if (col_index == -1) return;
            if (this.CellValidatingEvent != null) CellValidatingEvent(value, e.RowIndex, e.ColumnIndex, e.FormattedValue.ToString(), e);
        }
        private void DataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void CheckBoxHeader_CheckedChanged(object sender, EventArgs e)
        {
            if (!_顯示CheckBox) return;
            this.SuspendDrawing();
            dataGridView.SuspendDrawing();
            if (checkBoxHeader.Checked)
            {
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    dataGridView[0, i].Value = true;
                }
            }
            else
            {
                if (flag_unCheckedAll)
                {
                    for (int i = 0; i < dataGridView.RowCount; i++)
                    {
                        dataGridView[0, i].Value = false;
                    }
                }
            }
            flag_unCheckedAll = true;
            this.On_CheckedChanged(-1);
            this.ResumeDrawing();
            dataGridView.ResumeDrawing();
        }
        public void On_RowDoubleClick()
        {
            object[] value = this.GetRowValues();
            if (value != null)
            {
                if (this.RowDoubleClickEvent != null) this.RowDoubleClickEvent(value);
            }
        }
        public void On_RowEnter()
        {
            SelectRowindex_Buf = -1;
            int SelectRowindex = GetSelectRow();
            this.On_RowEnter(SelectRowindex);
        }
        private void On_RowEnter(int SelectRowindex)
        {
            if(SelectRowindex_Buf != SelectRowindex)
            {
                SelectRowindex_Buf = SelectRowindex;
                object[] value = this.GetRowValues(SelectRowindex);
                if (value != null)
                {
                    if (this.RowEnterEvent != null) this.RowEnterEvent(value);
                }
            }
    

        }
        private void On_CheckedChanged(int index)
        {
            if (!_顯示CheckBox) return;

            List<object[]> list_all_value = this.GetAllRows();
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                if (!(dataGridView[0, i].Value is bool)) continue;
                if ((bool)dataGridView[0, i].Value == true)
                {
                    if (i >= list_all_value.Count) continue;
                    list_value.Add(list_all_value[i]);
                }
            }
            if (this.CheckedChangedEvent != null) this.CheckedChangedEvent(list_value, index);
        }
        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.DataGridColumnHeaderMouseClickEvent != null) this.DataGridColumnHeaderMouseClickEvent(e.ColumnIndex);
            this.Refresh_Rows_From_Grid();
        }
        private void Refresh_Rows_From_Grid()
        {
            List<object[]> RowsList_buf = new List<object[]>();
            List<object> RowValue = new List<object>();
            for (int i = 0; i < this.dataGridView.RowCount; i++)
            {
                RowValue.Clear();
                for (int k = 0; k < this.dataGridView.Rows[i].Cells.Count; k++)
                {
                    if (this.dataGridView.Rows[i].Cells[k].Value == null) continue;
                    RowValue.Add(this.dataGridView.Rows[i].Cells[k].Value.ToString());
                }
                RowsList_buf.Add(RowValue.ToArray());
            }
            this.SaveDataVal.RowsList = RowsList_buf.DeepClone();
        }

        private void dataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    dataGridView.Rows[i].Selected = false;
                }
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
         
            }
        }


   
      

    }
}
