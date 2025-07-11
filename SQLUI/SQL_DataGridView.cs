﻿using System;
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
using System.Text.Json.Serialization;
using MySqlX.XDevAPI.Relational;

namespace SQLUI
{
    [DefaultEvent("MouseDown")]
    [System.Drawing.ToolboxBitmap(typeof(DataSet))]
    public partial class SQL_DataGridView : UserControl
    {
        public List<DataKeysClass> DataKeysClasses = new List<DataKeysClass>();
        private List<ComboBox> comboBoxes = new List<ComboBox>();
        public bool flag_Init = false;
        public bool CustomEnable = false;
        private bool flag_Refresh = false;
        private CheckBox checkBoxHeader = new CheckBox();
        private bool flag_unCheckedAll = false;
        public bool[] Checked = new bool[] { };
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
        public delegate void DataGridClearGridEventHandler();
        public event DataGridClearGridEventHandler DataGridClearGridEvent;
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
        public delegate void RowClickEventHandler(object[] RowValue);
        public event RowClickEventHandler RowClickEvent;
        public delegate bool RowEndEditEventHandler(object[] RowValue, int rowIndex, int colIndex, string value);
        public event RowEndEditEventHandler RowEndEditEvent;
        public delegate void CellValidatingEventHandler(object[] RowValue, int rowIndex, int colIndex, string value, DataGridViewCellValidatingEventArgs e);
        public event CellValidatingEventHandler CellValidatingEvent;
        public delegate void CheckedChangedEventHandler(List<object[]> RowsList, int index);
        public event CheckedChangedEventHandler CheckedChangedEvent;
        public delegate void CellPaintingImageEventHandler(DataGridViewCellPaintingEventArgs e);
        public event CellPaintingImageEventHandler CellPaintingImageEvent;
        public delegate void RowPostPaintingEventHandler(DataGridViewRowPostPaintEventArgs e);
        public event RowPostPaintingEventHandler RowPostPaintingEvent;
        public delegate void RowPostPaintingEventExHandler(SQL_DataGridView sQL_DataGridView, DataGridViewRowPostPaintEventArgs e);
        public event RowPostPaintingEventExHandler RowPostPaintingEventEx;

        public delegate void RowPostPaintingFinishedEventHandler(DataGridViewRowPostPaintEventArgs e);
        public event RowPostPaintingFinishedEventHandler RowPostPaintingFinishedEvent;
        public delegate void RowHeaderPostPaintingEventHandler(object sender , Graphics g, Rectangle rect_hedder, Brush brush_background, Pen pen_border);
        public event RowHeaderPostPaintingEventHandler RowHeaderPostPaintingEvent;
        public delegate void ComboBoxSelectedIndexChangedEventHandler(object sender, string colName, object[] RowValue);
        public event ComboBoxSelectedIndexChangedEventHandler ComboBoxSelectedIndexChangedEvent;

        private delegate void ModuleChangeEventHandler(List<object[]> RowsList, bool DoEvent);
        private event ModuleChangeEventHandler ModuleChangeEvent;
        private void RowsChange(List<object[]> RowsList)
        {
            this.RowsChange(RowsList, this.AutoSelectToDeep, true);
        }
        private void RowsChange(List<object[]> RowsList, bool DoEvent)
        {
            this.RowsChange(RowsList, this.AutoSelectToDeep , DoEvent);
        }
        public void RowsChange(List<object[]> RowsList, bool SelectToDeep, bool DoEvent)
        {
            try
            {
                this.flag_Refresh = true;
                dataGridView.CellValueChanged -= DataGridView_CellValueChanged;
                if (DoEvent)
                {
                    if (this.DataGridRowsChangeEvent != null)
                    {
                        this.DataGridRowsChangeEvent(RowsList);
                    }
                    if (this.DataGridRowsChangeRefEvent != null)
                    {
                        this.DataGridRowsChangeRefEvent(ref RowsList);
                    }
                }
                List<int> List_SelectRowindex = this.GetSelectRowsIndex();
                int ScrollingRowIndex = dataGridView.FirstDisplayedScrollingRowIndex;
                SaveDataVal.RowsList = null;
                SaveDataVal.RowsList = RowsList;
                this.Checked = new bool[RowsList.Count];
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
                                obj_buf[i] = Datebuf.ToDateTimeString_6();
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
                        if (this.IsColumnsText(columns.Text))
                        {
                            dataTable_buffer.Columns.Add(new DataColumn(columns.Text, typeof(string)));
                        }
                        else
                        {
                            dataTable_buffer.Columns.Add(new DataColumn(columns.Text, typeof(Image)));
                        }

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

                    if (DataGridRefreshEvent != null) this.DataGridRefreshEvent();
                    if (this.顯示首列)
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
                        if (columns.CanEdit)
                        {
                            dataGridView.Columns[columns.Text].DefaultCellStyle.SelectionBackColor = Color.Yellow;
                            dataGridView.Columns[columns.Text].DefaultCellStyle.SelectionForeColor = Color.DimGray;
                            dataGridView.Columns[columns.Text].DefaultCellStyle.ForeColor = Color.DimGray;
                        }
                        if (columns.TextFont != null) dataGridView.Columns[columns.Text].DefaultCellStyle.Font = columns.TextFont;
                        dataGridView.Columns[columns.Text].Width = columns.Width;
                        dataGridView.Columns[$"{columns.Text}"].ReadOnly = !columns.CanEdit;
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
                    int temp = -1;
                    for (int i = 0; i < dataGridView.ColumnCount; i++)
                    {
                        if (dataGridView.Columns[i].Visible == true) temp = i;
                    }
                    if (temp != -1) dataGridView.Columns[temp].AutoSizeMode = DataGridViewAutoSizeColumnMode;

                    this.ResumeDrawing();
                    dataGridView.ResumeDrawing();

                }));
                this.flag_Refresh = false;
                dataGridView.CellValueChanged += DataGridView_CellValueChanged;
                if (_顯示CheckBox)
                {
                    if (dataGridView.Rows.Count == 0) return;
                    //dataGridView.ClearSelection();
                    //this.SetSelectRow(0);
           
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToDateTimeString()} - Exception : {ex.Message}");
            }
            finally
            {

            }
           
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

        public DataGridViewRowCollection Rows
        {
            get
            {
                return dataGridView.Rows;
            }
        }

        private const string Extension = @".data";
        private bool IsStart = false;
        private SQLControl _SQLControl;
        private DataTable dataTable = new DataTable();
        private DataTable dataTable_buffer;
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
        private bool _自動換行 = true;
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

        private DataGridViewAutoSizeColumnMode dataGridViewAutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public DataGridViewAutoSizeColumnMode DataGridViewAutoSizeColumnMode
        {
            get
            {
                return dataGridViewAutoSizeColumnMode;
            }
            set
            {
                dataGridViewAutoSizeColumnMode = value;
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

        private bool _顯示CheckBox = false;
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
  

            public Type type = typeof(string);
            public string[] EnumAry = new string[] { };

            [JsonIgnore]
            public Font TextFont = null;
            [Browsable(false)]
            public string TextFont_Serialize
            {
                get { return FontSerializationHelper.ToString(TextFont); }
                set { TextFont = FontSerializationHelper.FromString(value); }
            }
            [JsonIgnore]
            public Color BackgroundColor = Color.White;
            [Browsable(false)]
            public string BackgroundColor_Serialize
            {
                get { return ColorSerializationHelper.ToString(BackgroundColor); }
                set { BackgroundColor = ColorSerializationHelper.FromString(value); }
            }
            [JsonIgnore]
            public Color ForeColor = Color.White;
            [Browsable(false)]
            public string ForeColor_Serialize
            {
                get { return ColorSerializationHelper.ToString(ForeColor); }
                set { ForeColor = ColorSerializationHelper.FromString(value); }
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

        private bool dataKeyEnable = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool DataKeyEnable
        {
            get
            {
                return dataKeyEnable;
            }
            set
            {
                dataKeyEnable = value;

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

        public enum RowBorderStyleOption
        {
            All,
            BottomLine
        }

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
        private Color _columnHeaderBorderColor = Color.DimGray;
        [Category("RJ Code - Appearence")]
        public Color columnHeaderBorderColor
        {
            get
            {
                return _columnHeaderBorderColor;
            }
            set
            {
                _columnHeaderBorderColor = value;
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
       
        private RowBorderStyleOption _rowHeaderBorderStyleOption = RowBorderStyleOption.All;
        [Category("RJ Code - Appearence")]
        public RowBorderStyleOption  rowHeaderBorderStyleOption
        {
            get
            {
                return _rowHeaderBorderStyleOption;
            }
            set
            {
                _rowHeaderBorderStyleOption = value;
                dataGridView.Invalidate();
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
        public RowBorderStyleOption _rowBorderStyleOption = RowBorderStyleOption.All;
        [Category("RJ Code - Appearence")]
        public RowBorderStyleOption rowBorderStyleOption
        {
            get
            {
                return _rowBorderStyleOption;
            }
            set
            {
                _rowBorderStyleOption = value;
                dataGridView.Invalidate();
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
        private Color _RowsColor = Control.DefaultBackColor;
        [Category("RJ Code - Appearence")]
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
        private Color cellBorderColor = Color.White;
        [Category("RJ Code - Appearence")]
        public Color CellBorderColor
        {
            get
            {
                return cellBorderColor;
            }
            set
            {
                cellBorderColor = value;
            }
        }

        private Color _selectedRowBackColor = Color.Blue;
        [Category("RJ Code - Appearence")]
        public Color selectedRowBackColor
        {
            get
            {
                return _selectedRowBackColor;
            }
            set
            {
                _selectedRowBackColor = value;
            }
        }
        private Color _selectedRowBorderColor = Color.Blue;
        [Category("RJ Code - Appearence")]
        public Color selectedRowBorderColor
        {
            get
            {
                return _selectedRowBorderColor;
            }
            set
            {
                _selectedRowBorderColor = value;
            }
        }
        private Color _selectedRowForeColor = Color.White;
        [Category("RJ Code - Appearence")]
        public Color selectedRowForeColor
        {
            get
            {
                return _selectedRowForeColor;
            }
            set
            {
                _selectedRowForeColor = value;
            }
        }
        private int _selectedBorderSize = 0;
        [Category("RJ Code - Appearence")]
        public int selectedBorderSize
        {
            get
            {
                return _selectedBorderSize;
            }
            set
            {
                _selectedBorderSize = value;
            }
        }

        private Color _checkedRowBackColor = Color.YellowGreen;
        [Category("RJ Code - Appearence")]
        public Color checkedRowBackColor
        {
            get
            {
                return _checkedRowBackColor;
            }
            set
            {
                _checkedRowBackColor = value;
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
            this.dataGridView.RowPostPaint += DataGridView_RowPostPaint;
            this.dataGridView.CellPainting += DataGridView_CellPainting;
            this.dataGridView.Paint += DataGridView_Paint;
            this.dataGridView.EditingControlShowing += DataGridView_EditingControlShowing;
            this.dataGridView.Scroll += DataGridView_Scroll;
            this.dataGridView.SelectionChanged += DataGridView_SelectionChanged;
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
            string TableName = table.TableName;
            Init(table, TableName);
        }
        public void InitEx(Table table)
        {
            string TableName = table.TableName;
            this.Server = table.Server;
            this.DataBaseName = table.DBName;
            this.UserName = table.Username;
            this.Password = table.Password;
            this.Port = table.Port.StringToUInt32();
            Init(table, TableName);
        }
        public void Init(Table table , string TableName)
        {
            this.Columns.Clear();
            this.TableName = TableName;
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
                columnElement.EnumAry = table.ColumnList[i].EnumAry.ToArray();
                columnElement.Datalen = (uint)table.ColumnList[i].Num;
                columnElement.CanEdit = false;
                //if (columnElement.OtherType == Table.OtherType.ENUM) columnElement.CanEdit = true;
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
            if (OnlineState == OnlineEnum.Online) this.SQL_Reset();
            this.SQL_TableInit();
            this.DataGrid_Init(IsStart);
            if (this.flag_Init == true)
            {
                return;
            }
            this.flag_Init = true;
            if (OnlineState == OnlineEnum.Online)
            {           
     
                this.ModuleChangeEvent += new ModuleChangeEventHandler(RowsChange);
                dataGridView.MouseDown += SQL_DataGridView_MouseDown;
                dataGridView.CellClick += DataGridView_CellClick;
                dataGridView.CellEnter += DataGridView_CellEnter;
                dataGridView.DoubleClick += DataGridView_DoubleClick;
                dataGridView.CellEndEdit += DataGridView_CellEndEdit;
            
                dataGridView.CellValidating += DataGridView_CellValidating;
                dataGridView.CellValidated += DataGridView_CellValidated;
            }
            else if (OnlineState == OnlineEnum.Offline)
            {
                this.ModuleChangeEvent += new ModuleChangeEventHandler(RowsChange);
                dataGridView.MouseDown += SQL_DataGridView_MouseDown;
                dataGridView.CellClick += DataGridView_CellClick;
                dataGridView.CellEnter += DataGridView_CellEnter;
                dataGridView.DoubleClick += DataGridView_DoubleClick;
                dataGridView.CellEndEdit += DataGridView_CellEndEdit;
                dataGridView.Click += DataGridView_Click;
                dataGridView.CellValidating += DataGridView_CellValidating;
                dataGridView.CellValidated += DataGridView_CellValidated;
            }
            dataGridView.MouseWheel += DataGridView_MouseWheel;
            if(checkBoxHeader != null)
            {
                checkBoxHeader.CheckedChanged += CheckBoxHeader_CheckedChanged;
            }
  
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
                    if(columns.OtherType == Table.OtherType.ENUM)
                    {
                        SQL_Table.AddColumnList(columns.Name, columns.OtherType, columns.EnumAry, columns.IndexType);
                    }
          
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
            List<object[]> temp = new List<object[]>();
            try
            {
                temp = _SQLControl.GetAllRows(SQL_Table.GetTableName());
                if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp, true);
            }
            catch
            {

            }
   
            return temp;
        }
        public List<object[]> SQL_GetAllRows(int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetAllRows(SQL_Table.GetTableName(), OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetAllRows(string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetAllRows(SQL_Table.GetTableName(), OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
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
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRows(string serchColumnName, string serchValue, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRows(string serchColumnName, string serchValue, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRows(int serchColumnindex, string serchValue, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnindex, serchValue, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRows(int serchColumnindex, string serchValue, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnindex, serchValue, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRows(int serchColumnindex, string serchValue, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnindex, serchValue, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRows(string serchColumnName, string serchValue, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRows(string serchColumnName, string serchValue, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRows(string serchColumnName, string serchValue, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRows(string[] serchColumnName, string[] serchValue, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRows(string[] serchColumnName, string[] serchValue, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRows(string[] serchColumnName, string[] serchValue, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRows(string[] serchColumnName, string[] serchValue, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRows(string[] serchColumnName, string[] serchValue, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByDefult(SQL_Table.GetTableName(), serchColumnName, serchValue, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsOfRange(uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsOfRange(SQL_Table.GetTableName(), StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsOfRange(uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsOfRange(SQL_Table.GetTableName(), StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsOfRange(uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsOfRange(SQL_Table.GetTableName(), StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(int serchColumnindex, string LikeString, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnindex, LikeString);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(int serchColumnindex, string LikeString, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnindex, LikeString, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(int serchColumnindex, string LikeString, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnindex, LikeString, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(int serchColumnindex, string LikeString, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnindex, LikeString, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(string serchColumnName, string LikeString, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnName, LikeString);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(string serchColumnName, string LikeString, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnName, LikeString, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(string serchColumnName, string LikeString, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnName, LikeString, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByLike(string serchColumnName, string LikeString, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByLike(SQL_Table.GetTableName(), serchColumnName, LikeString, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
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
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(int serchColumnindex, string brtween_value1, string brtween_value2, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnindex, brtween_value1, brtween_value2, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(int serchColumnindex, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnindex, brtween_value1, brtween_value2, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(int serchColumnindex, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnindex, brtween_value1, brtween_value2, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(int serchColumnindex, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnindex, brtween_value1, brtween_value2, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
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
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(string serchColumnName, string brtween_value1, string brtween_value2, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnName, brtween_value1, brtween_value2, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(string serchColumnName, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnName, brtween_value1, brtween_value2, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(string serchColumnName, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnName, brtween_value1, brtween_value2, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByBetween(string serchColumnName, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByBetween(SQL_Table.GetTableName(), serchColumnName, brtween_value1, brtween_value2, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(int serchColumnindex, string[] IN_value, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnindex, IN_value);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(int serchColumnindex, string[] IN_value, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnindex, IN_value, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(int serchColumnindex, string[] IN_value, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnindex, IN_value, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(int serchColumnindex, string[] IN_value, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnindex, IN_value, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(string serchColumnName, string[] IN_value, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnName, IN_value);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(string serchColumnName, string[] IN_value, uint StrintIndex, uint NumOfData, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnName, IN_value, StrintIndex, NumOfData);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(string serchColumnName, string[] IN_value, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnName, IN_value, StrintIndex, NumOfData, OrderColumnindex, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }
        public List<object[]> SQL_GetRowsByIn(string serchColumnName, string[] IN_value, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType, bool IsRefreshGrid)
        {
            List<object[]> temp = _SQLControl.GetRowsByIn(SQL_Table.GetTableName(), serchColumnName, IN_value, StrintIndex, NumOfData, OrderColumnName, _OrderType);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(temp , true);
            return temp;
        }

        public void SQL_AddRow(object[] Value, bool IsRefreshGrid)
        {
            _SQLControl.AddRow(SQL_Table.GetTableName(), Value);
            if (IsRefreshGrid)
            {
                List<object[]> temp = _SQLControl.GetAllRows(SQL_Table.GetTableName());
                if (IsRefreshGrid) this.RowsChange(temp, this.AutoSelectToDeep, true);
            }
        }
        public void SQL_AddRows(List<object[]> Values, bool IsRefreshGrid)
        {
            _SQLControl.AddRows(SQL_Table.GetTableName(), Values);
            if (IsRefreshGrid)
            {
                List<object[]> temp = _SQLControl.GetAllRows(SQL_Table.GetTableName());
                if (IsRefreshGrid) this.RowsChange(temp, this.AutoSelectToDeep, true);
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
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(obj_list, true);
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
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(obj_list , true);
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
        bool flag_init = false;
       

        private void DataGrid_Init()
        {

            comboBoxes.Clear();
            if(DesignMode == true)
            {
                dataGridView.Columns.Clear();
            }
            else
            {
                if (dataGridView.Columns.Count > 0)
                {
                    dataGridView.Invoke(new Action(delegate
                    {
                        dataGridView.SuspendLayout();
                        dataGridView.DataSource = null;  // 解除绑定
                        dataGridView.Columns.Clear();
                        dataGridView.ResumeLayout();
                    }));
                }
            }
           
            dataTable = new DataTable();
            foreach (ColumnElement columns in Columns)
            {
                if(columns.OtherType == Table.OtherType.IMAGE)
                {
                    dataTable.Columns.Add(new DataColumn(columns.Text, typeof(string)));
                    continue;
                }
                if (columns.OtherType == Table.OtherType.ENUM)
                {
                    dataTable.Columns.Add(new DataColumn(columns.Text, typeof(ComboBox)));
              
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
                if (columns.TextFont != null) dataGridView.Columns[$"{columns.Text}"].DefaultCellStyle.Font = columns.TextFont;
                if (columns.OtherType == Table.OtherType.IMAGE)
                {
                    ((DataGridViewImageColumn)dataGridView.Columns[$"{columns.Text}"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                }
                if (columns.OtherType == Table.OtherType.ENUM)
                {
                    ComboBox comboBox = new ComboBox();  
                    comboBox.Name = columns.Text;
                    comboBox.Visible = false;
                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    comboBox.Font = this.cellStyleFont;
                    comboBox.Items.AddRange(columns.EnumAry);
                    comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                    comboBoxes.Add(comboBox);
                    this.Controls.Add(comboBox);
                }


            }

            if (_顯示CheckBox)
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
        public void SetSelectRow(object[] value)
        {
            this.SetSelectRow("GUID", value[0].ObjectToString());
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
                this.SuspendDrawing();
                dataGridView.SuspendDrawing();
                if (index < dataGridView.Rows.Count)
                {
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        dataGridView.Rows[i].Selected = false;
                    }
                    dataGridView.Rows[index].Selected = true;
                    dataGridView.FirstDisplayedScrollingRowIndex = index;
                }
                this.ResumeDrawing();
                dataGridView.ResumeDrawing();
            }));
            this.SelectRowindex_Buf = -1;
            if (RowEnterEvent != null) RowEnterEvent(this.GetRowValues(index));
            if (RowClickEvent != null) RowClickEvent(this.GetRowValues(index));
        }

        public Rectangle GetColumnBounds(string name)
        {
            return GetColumnBounds(name, name);
        }
        public Rectangle GetColumnBounds(string start_col_name, string end_col_name)
        {
            int start_index = dataGridView.Columns[start_col_name].Index;
            int end_index = dataGridView.Columns[end_col_name].Index;
            Rectangle rectangle = new Rectangle();
            rectangle.X = this.dataGridView.RowHeadersWidth;
            if (顯示首列 == false) rectangle.X = 0;
            rectangle.Height = this.columnHeadersHeight;
            rectangle.Width = 0;
            for (int i = start_index; i <= end_index; i++)
            {
                rectangle.Width += dataGridView.Columns[i].Width;
            }
            for (int i = start_index - 1; i > 0; i--)
            {
                rectangle.X += dataGridView.Columns[i].Width;
            }
            return rectangle;
        }

        public void ClearSelection()
        {
            SelectRowindex_Buf = -1;
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
                    if ((bool)dataGridView[0, i].Value == true ) list_value.Add(this.GetRowValues(i));
                }
            }
            return list_value;
        }
        public List<object[]> Get_All_Checked_RowsValuesEx(List<object[]> list_rowValue)
        {
            List<object[]> list_value = new List<object[]>();
            if (dataKeyEnable)
            {
                System.Collections.Generic.Dictionary<string, DataKeysClass> keyValuePairs = DataKeysClasses.CoverToDictionaryByGUID();
                for (int i = 0; i < list_rowValue.Count; i++)
                {
                    string GUID = list_rowValue[i][0].ObjectToString();
                    DataKeysClass dataKeysClass = keyValuePairs.SortDictionaryByDataKeysClass(GUID);
                    if (dataKeysClass != null)
                    {
                        if (dataKeysClass.check) list_value.Add(list_rowValue[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    if (Checked[i]) list_value.Add(this.GetRowValues(i));
                }
            }

            return list_value;
        }
        public List<object[]> Get_All_Checked_RowsValuesEx()
        {
            List<object[]> list_value = new List<object[]>();
            if(dataKeyEnable)
            {
                System.Collections.Generic.Dictionary<string, DataKeysClass> keyValuePairs =  DataKeysClasses.CoverToDictionaryByGUID();
                List<object[]> list_rowValue = this.GetAllRows();
                for (int i = 0; i < list_rowValue.Count; i++)
                {
                    string GUID = list_rowValue[i][0].ObjectToString();
                    DataKeysClass dataKeysClass = keyValuePairs.SortDictionaryByDataKeysClass(GUID);
                    if (dataKeysClass != null)
                    {
                        if(dataKeysClass.check)list_value.Add(list_rowValue[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    if (Checked[i]) list_value.Add(this.GetRowValues(i));
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
            if (IsRefreshGrid) this.RowsChange(SaveDataVal.RowsList, this.AutoSelectToDeep, true);
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
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(RowsList_buf, true);
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
                if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList , true);
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
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList , true);
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
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList , true);
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
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList , true);
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
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList , true);
        }
        public void Delete(int SelectIndex, bool IsRefreshGrid)
        {
            this.SaveDataVal.RowsList.RemoveAt(SelectIndex);
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList , true);
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
            if (IsRefreshGrid) if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList , true);
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
                    if(this.Columns[k].Text == name[i] || this.Columns[k].Name == name[i])
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
                    if (this.Columns[k].Text == name || this.Columns[k].Name == name)
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
            for (int k = 0; k < this.Columns.Count; k++)
            {
                if (this.Columns[k].Text == name || this.Columns[k].Name == name)
                {
                    Columns[k].Width = width;
                    Columns[k].Visable = true;
                    Columns[k].Alignment = alignment;
                    Columns[k].CanEdit = false;
                }
            }
            foreach (ColumnElement columns in Columns)
            {
                DataGridViewColumn dataGridViewColumn = dataGridView.Columns[$"{columns.Text}"];
                if (dataGridViewColumn == null) dataGridViewColumn = dataGridView.Columns[$"{columns.Name}"];
                if (dataGridViewColumn == null) continue;

                dataGridViewColumn.DefaultCellStyle.BackColor = columns.BackgroundColor;
                dataGridViewColumn.Width = columns.Width;
                dataGridViewColumn.SortMode = columns.SortMode;
                dataGridViewColumn.DefaultCellStyle.Alignment = columns.Alignment;
                dataGridViewColumn.Visible = columns.Visable;
                dataGridViewColumn.ReadOnly = !columns.CanEdit;
            }
        }

        public void Set_CanEdit(bool can_edit, object Enum)
        {
            this.Set_CanEdit(can_edit, Enum.GetEnumName());
        }
        public void Set_CanEdit( bool can_edit, string name)
        {
            for (int k = 0; k < this.Columns.Count; k++)
            {
                if (this.Columns[k].Text == name || this.Columns[k].Name == name)
                {
                    Columns[k].Visable = true;
                    Columns[k].CanEdit = can_edit;
                }
            }
            foreach (ColumnElement columns in Columns)
            {
                DataGridViewColumn dataGridViewColumn = dataGridView.Columns[$"{columns.Text}"];
                if (dataGridViewColumn == null) dataGridViewColumn = dataGridView.Columns[$"{columns.Name}"];
                if (dataGridViewColumn == null) continue;

                dataGridViewColumn.DefaultCellStyle.BackColor = columns.BackgroundColor;
                dataGridViewColumn.Width = columns.Width;
                dataGridViewColumn.SortMode = columns.SortMode;
                dataGridViewColumn.DefaultCellStyle.Alignment = columns.Alignment;
                dataGridViewColumn.Visible = columns.Visable;
                dataGridViewColumn.ReadOnly = !columns.CanEdit;
            }
        }

        public void Set_ColumnText(string text, object Enum)
        {
            this.Set_ColumnText(text, Enum.GetEnumName());
        }
        public void Set_ColumnText(string text, string ColumnName)
        {
            if (text.StringIsEmpty()) return;
            for (int k = 0; k < this.Columns.Count; k++)
            {
                if (this.Columns[k].Name == ColumnName)
                {
                    dataGridView.Columns[$"{ Columns[k].Name}"].HeaderText = text;
                    Columns[k].Text = text;
                    Columns[k].Visable = true;
                }
            }

        }
        public void Set_ColumnSortMode(DataGridViewColumnSortMode dataGridViewColumnSortMode, object Enum)
        {
            this.Set_ColumnSortMode(dataGridViewColumnSortMode, Enum.GetEnumName());
        }
        public void Set_ColumnSortMode(DataGridViewColumnSortMode dataGridViewColumnSortMode, string name)
        {
            for (int i = 0; i < name.Length; i++)
            {
                for (int k = 0; k < this.Columns.Count; k++)
                {
                    if (this.Columns[k].Text == name || this.Columns[k].Name == name)
                    {
                        Columns[k].SortMode = dataGridViewColumnSortMode;
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
        public void Set_ColumnType(string name, Type type)
        {
            for (int k = 0; k < this.Columns.Count; k++)
            {
                if (this.Columns[k].Text == name || this.Columns[k].Name == name)
                {
                    Columns[k].type = type;
                }
            }
        }
        public void Set_ColumnFont(Font font, object Enum)
        {
            this.Set_ColumnFont(font, Enum.GetEnumName());
        }
        public void Set_ColumnFont(Font font, string name)
        {
            for (int k = 0; k < this.Columns.Count; k++)
            {
                if (this.Columns[k].Text == name || this.Columns[k].Name == name)
                {
                    Columns[k].TextFont = font;
                }
            }

        }



        public void Set_ColumnHeaderHeight(int height)
        {
            this.columnHeadersHeight = height;
        }

        public int Get_ColumnWidth(string name)
        {
            int width = -1;
            foreach (ColumnElement columns in Columns)
            {
                if (columns.Name == name || columns.Text == name) return columns.Width;
            }
            return width;
        }
        public bool Get_ColumnVisible(string name)
        {
            int width = -1;
            foreach (ColumnElement columns in Columns)
            {
                if (columns.Name == name || columns.Text == name) return columns.Visable;
            }
            return false;
        }
        public Font Get_ColumnFont(string name)
        {
            Font font = null;
            try
            {
                foreach (ColumnElement columns in Columns)
                {
                    if (columns.Name == name || columns.Text == name)
                    {
                        font = columns.TextFont;
                        break;
                    }
                }
                if (font == null) font = this.cellStyleFont;
                return font;
            }
            catch
            {
                return font;
            }
            finally
            {


            }

        }
        public DataGridViewContentAlignment Get_ColumnAlignment(string name)
        {
            DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleCenter;
            try
            {
                foreach (ColumnElement columns in Columns)
                {
                    if (columns.Name == name || columns.Text == name)
                    {
                        alignment = columns.Alignment;
                        break;
                    }
                }
                return alignment;
            }
            catch
            {
                return alignment;
            }
            finally
            {


            }

        }
        public void ScrollToIndex(int index)
        {
            this.Invoke(new Action(delegate
            {
                if (index < 0) index = 0;
                if (index > this.dataGridView.RowCount - 1) index = this.dataGridView.RowCount - 1;
                this.dataGridView.FirstDisplayedScrollingRowIndex = index;
            }));

        }
        // 滾動到上一行
        public void ScrollUp()
        {
            this.Invoke(new Action(delegate
            {
                if (this.dataGridView.FirstDisplayedScrollingRowIndex > 0)
                {
                    this.dataGridView.FirstDisplayedScrollingRowIndex--;
                }
            }));
      
        }

        // 滾動到下一行
        public void ScrollDown()
        {
            this.Invoke(new Action(delegate
            {
                if (this.dataGridView.FirstDisplayedScrollingRowIndex < this.dataGridView.RowCount - 1)
                {
                    this.dataGridView.FirstDisplayedScrollingRowIndex++;
                }
            }));
         
        }
        public void ClearGrid()
        {
            List<object[]> RowsList = new List<object[]>();
            SaveDataVal.RowsList = RowsList;
            if (DataGridClearGridEvent != null) DataGridClearGridEvent();
            if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList , true);
        }
        public void RefreshGridNoEvent(List<object[]> RowsList)
        {
            SaveDataVal.RowsList = RowsList.DeepClone();
            if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList, false);
        }
        public void RefreshGrid(List<object[]> RowsList)
        {
            SaveDataVal.RowsList = RowsList.DeepClone();
            if (ModuleChangeEvent != null) this.ModuleChangeEvent(SaveDataVal.RowsList , true);
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
        public void RefreshGridByUpdate(List<object[]> RowsList)
        {
            List<object[]> list_add = new List<object[]>();
            List<object[]> list_replace = new List<object[]>();
            List<object[]> list_delete = new List<object[]>();

            // Create dictionaries for quick lookup
            Dictionary<string, object[]> currentDataDict = this.SaveDataVal.RowsList.ToDictionary(row => row[0].ToString());
            Dictionary<string, object[]> newDataDict = RowsList.ToDictionary(row => row[0].ToString());

            // Find rows to add and replace
            foreach (var newRow in RowsList)
            {
                string key = newRow[0].ToString();
                if (currentDataDict.ContainsKey(key))
                {
                    list_replace.Add(newRow);
                }
                else
                {
                    list_add.Add(newRow);
                }
            }

            // Find rows to delete
            foreach (var currentRow in this.SaveDataVal.RowsList)
            {
                string key = currentRow[0].ToString();
                if (!newDataDict.ContainsKey(key))
                {
                    list_delete.Add(currentRow);
                }
            }

            // Perform the updates
            foreach (var row in list_add)
            {
                this.SaveDataVal.RowsList.Add(row);
            }

            foreach (var row in list_replace)
            {
                int index = this.SaveDataVal.RowsList.FindIndex(r => r[0].ToString() == row[0].ToString());
                if (index != -1)
                {
                    this.SaveDataVal.RowsList[index] = row;
                }
            }

            foreach (var row in list_delete)
            {
                this.SaveDataVal.RowsList.Remove(row);
            }

            // Refresh the grid
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

        public void SetDataKeys(List<object[]> list_value)
        {
            this.SetDataKeys(list_value, true);
        }
        public void SetDataKeys(List<object[]> list_value , bool flag_clear)
        {
            if (flag_clear) ClearDataKeys();
            for (int i = 0; i < list_value.Count; i++)
            {
                DataKeysClasses.SetDataKey(list_value[i][0].ObjectToString());
            }
        }
        public void ClearDataKeys()
        {
            DataKeysClasses.ClearDataKey();

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
            if (ModuleChangeEvent != null) ModuleChangeEvent(SaveDataVal.RowsList, true);
        }

        public string GetColumnsJsonStr()
        {
            return this.Columns.JsonSerializationt();
        }
        public void SetColumnsJsonStr(string json)
        {
            List<ColumnElement> columnElements = json.JsonDeserializet<List<ColumnElement>>();
            if (columnElements != null)
            {
                for (int i = 0; i < columnElements.Count; i++)
                {
                    foreach(ColumnElement columnElement in Columns)
                    {
                        if (columnElement.Name == columnElements[i].Name)
                        {
                            //columnElement.Text = columnElements[i].Text;
                            columnElement.TextFont = columnElements[i].TextFont;
                            columnElement.ForeColor = columnElements[i].ForeColor;
                            columnElement.Width = columnElements[i].Width;
                            columnElement.Alignment = columnElements[i].Alignment;
                            columnElement.SortMode = columnElements[i].SortMode;
                            columnElement.Visable = columnElements[i].Visable;
                        }
                    }
                }
            }
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
            object[] value = this.GetRowValues();
            if (value != null)
            {
                if (dataKeyEnable)
                {
                    DataKeysClasses.UpdateDataKey(value[0].ObjectToString());
                    var dataGridView = sender as DataGridView;
                    if (dataGridView != null)
                    {
                        using (Graphics graphics = dataGridView.CreateGraphics())
                        {
                            CustomRowPostPaint(graphics, dataGridView, SelectRowindex);
                        }
                    }
                }
                if (this.RowClickEvent != null) this.RowClickEvent(value);
            }
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
         
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = e.ColumnIndex >= 0 ? this.dataGridView.Columns[e.ColumnIndex].Name : string.Empty;
                if (columnName.StringIsEmpty()) return;
                ColumnElement columnElement = Columns.GetColumn(columnName);
                if (columnElement.OtherType == Table.OtherType.ENUM)
                {
                    Rectangle rect = dataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    for (int i = 0; i < comboBoxes.Count; i++)
                    {
                        if(comboBoxes[i].Name == columnElement.Text || comboBoxes[i].Name == columnElement.Name)
                        {
                            comboBoxes[i].Bounds = rect;
                            comboBoxes[i].Visible = true;
                            comboBoxes[i].DroppedDown = true;
                        }
                    }
                 
                }
            }


        }
        private void DataGridView_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            if (dataTable_buffer != null) this.dataGridView.DataSource = dataTable_buffer;
            if (dataGridView.CanSelect && dataGridView.Rows.Count > 0)
            {
                if (_顯示CheckBox)
                {
                    checkBoxHeader.Visible = true;
                    int headerposX = 0;
                    int headerWidth = dataGridView.RowHeadersWidth;
                    int headerHeight = dataGridView.ColumnHeadersHeight;
                   if (this.顯示首列) headerposX = headerWidth + (dataGridView.Columns[0].Width - checkBoxHeader.Width) / 2; 
                   else headerposX = (dataGridView.Columns[0].Width - checkBoxHeader.Width) / 2;
                    checkBoxHeader.Location = new Point(headerposX, (headerHeight - checkBoxHeader.Height) / 2);
                }
            }
            if (RowHeaderPostPaintingEvent != null)
            {
                using (Brush brush_background = new SolidBrush(columnHeaderBackColor))
                using (Pen pen_border = new Pen(columnHeaderBorderColor))
                {

                    int col_width = this.dataGridView.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                    int x = 0;
                    int y = 0;
                    int width = col_width;
                    int height = this.columnHeadersHeight;
                    //e.Graphics.FillRectangle(brush_background, new RectangleF(x, y, width, height));
                    //e.Graphics.DrawRectangle(pen_border, new Rectangle(x, y, width, height));

                    RowHeaderPostPaintingEvent(this.dataGridView, e.Graphics, new Rectangle(x, y, width, height), brush_background, pen_border);
                }
            }
            else
            {
                if (_rowHeaderBorderStyleOption == RowBorderStyleOption.BottomLine)
                {
                    using (Brush brush_background = new SolidBrush(columnHeaderBackColor))
                    using (Pen pen_border = new Pen(columnHeaderBorderColor))
                    {
                        // 取得整個標題列的矩形範圍
                        Rectangle headerRect = new Rectangle(0, 0, this.dataGridView.Width, columnHeadersHeight);

                        // 填充背景色
                        e.Graphics.FillRectangle(brush_background, headerRect);

                        // 畫底部邊界線
                        e.Graphics.DrawLine(pen_border, headerRect.Left, headerRect.Bottom - 1, headerRect.Right, headerRect.Bottom - 1);

                        foreach (DataGridViewColumn column in this.dataGridView.Columns)
                        {
                            if (column.Visible)
                            {
                                Rectangle rect = this.dataGridView.GetColumnDisplayRectangle(column.Index, false);
                                rect = new Rectangle(rect.X, rect.Y, rect.Width - 1, columnHeadersHeight);  
                                DrawString(e.Graphics, column.HeaderText, columnHeaderFont, rect, cellStylForeColor, Get_ColumnAlignment(column.HeaderText));
                            }
                        }
                    }
                }
               
            }
        }
        private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            string columnName = e.ColumnIndex >= 0 ? this.dataGridView.Columns[e.ColumnIndex].Name : string.Empty;


            //序號欄
            if (e.RowIndex > -1 && e.ColumnIndex == -1)
            {
         
                using (Brush brush_background = new SolidBrush(this.rowHeaderBackColor))
                using (Pen pen_border = new Pen(cellBorderColor))
                {
                    e.Graphics.FillRectangle(brush_background, e.CellBounds);
                    e.Graphics.DrawRectangle(pen_border, e.CellBounds);
              
                    if (e.Value != null) DrawString(e.Graphics, e.Value.ToString(), e.CellStyle.Font, e.CellBounds, e.CellStyle.ForeColor, e.CellStyle.Alignment);
                    e.Handled = true;
                }
            }
            if (RowHeaderPostPaintingEvent == null)
            {
                if (e.RowIndex == -1 && e.ColumnIndex >= -1)
                {
                    if (_rowHeaderBorderStyleOption == RowBorderStyleOption.All)
                    {
                        using (Brush brush_background = new SolidBrush(this.columnHeaderBackColor))
                        using (Pen pen_border = new Pen(columnHeaderBorderColor))
                        {
                            e.Graphics.FillRectangle(brush_background, e.CellBounds);
                            DrawBottomLine(e.Graphics, e.CellBounds, columnHeaderBorderColor, 1);
                            DrawLeftLine(e.Graphics, e.CellBounds, columnHeaderBackColor, 1);
                            DrawRightLine(e.Graphics, e.CellBounds, columnHeaderBackColor, 1);
                            if (e.Value != null)
                            {
                                DrawString(e.Graphics, e.Value.ToString(), e.CellStyle.Font, e.CellBounds, e.CellStyle.ForeColor, e.CellStyle.Alignment);
                            }
                            e.Handled = true;
                        }
                    }
                    
                 
                }
            }
            else
            {
 
                if (e.RowIndex == -1 && e.ColumnIndex == -1)
                {                                   
                    e.Handled = true;
                }
            }
          
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (RowPostPaintingEvent != null || RowPostPaintingEventEx != null)
                {
                    e.Handled = true;
                    return;
                }
  
                if (_顯示CheckBox)
                {
                    if (e.RowIndex >= 0 && e.ColumnIndex == 0)
                    {
                        e.PaintBackground(e.CellBounds, true);

                        ControlPaint.DrawCheckBox(e.Graphics, e.CellBounds.X + 1, e.CellBounds.Y + 1,
                            e.CellBounds.Width - 2, e.CellBounds.Height - 2,
                            (bool)e.FormattedValue ? ButtonState.Checked : ButtonState.Normal);
                        e.Handled = true;
                        return;
                    }
                }
                using (Brush brush_background = new SolidBrush(e.CellStyle.BackColor))
                using (Pen pen_cell_border = new Pen(cellBorderColor))
                using (Brush brush_check_background = new SolidBrush(checkedRowBackColor))
                using (Pen pen_check_border = new Pen(cellBorderColor))
                {
                    if (dataKeyEnable == false)
                    {
                        e.Graphics.FillRectangle(brush_background, e.CellBounds);
                        e.Graphics.DrawRectangle(pen_cell_border, e.CellBounds);
                    }
                    else
                    {
                        object[] value = GetRowValues(e.RowIndex);
                        if (DataKeysClasses.GetDataKeysCheck(value[0].ObjectToString()))
                        {
                            e.Graphics.FillRectangle(brush_check_background, e.CellBounds);
                            e.Graphics.DrawRectangle(pen_cell_border, e.CellBounds);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(brush_background, e.CellBounds);
                            e.Graphics.DrawRectangle(pen_cell_border, e.CellBounds);
                        }
                    }

                    if (e.Value == null) return;
                    Type type = e.Value.GetType();
                    if (type == typeof(Image) || type == typeof(Bitmap))
                    {
                        double scale = 0.0F;
                        Image image = (Image)e.Value;

                        if (e.CellBounds.Width < e.CellBounds.Height)
                        {
                            scale = (double)e.CellBounds.Width / (double)image.Width;
                        }
                        else
                        {
                            scale = (double)e.CellBounds.Height / (double)image.Height;
                        }
                    
                        float width = (float)(image.Width * scale);
                        float height = (float)(image.Height * scale);

                        float pX = (float)(e.CellBounds.X + (e.CellBounds.Width - width) / 2);
                        float pY = (float)(e.CellBounds.Y + (e.CellBounds.Height - height) / 2);

                        if (CellPaintingImageEvent != null) CellPaintingImageEvent(e);
                        e.Graphics.DrawImage(image, pX, pY, width, height);
                    }
                    else
                    {

                        if (e.Value != null)
                        {
                            int index = e.ColumnIndex;
                            if (this.顯示CheckBox) index = index - 1;
                            if (index < 0) return;
                            ColumnElement columnElement = Columns.GetColumn(columnName);
                            if (columnElement.OtherType == Table.OtherType.ENUM)
                            {
                                Rectangle display_rect = dataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                                e.Graphics.FillRectangle(brush_background, display_rect);
                          
                                Rectangle rect = new Rectangle(e.CellBounds.X + (e.CellBounds.Width - 20), e.CellBounds.Y + 2, 20, e.CellBounds.Height - 4);
                                ComboBoxRenderer.DrawDropDownButton(e.Graphics, rect, System.Windows.Forms.VisualStyles.ComboBoxState.Disabled);
                                display_rect.Width = display_rect.Width - 20;
                                DrawString(e.Graphics, e.Value.ToString(), e.CellStyle.Font, display_rect, e.CellStyle.ForeColor, columnElement.Alignment);

                                e.Handled = true;
                            }
                            else
                            {
                                DrawString(e.Graphics, e.Value.ToString(), e.CellStyle.Font, e.CellBounds, e.CellStyle.ForeColor, columnElement.Alignment);
                            }
            
                            e.Handled = true;
                        }
                    }
                    e.Handled = true;

                }
            }
        }
        private void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            CustomRowPostPaint(e.Graphics, (DataGridView)sender, e.RowIndex);
            if (RowPostPaintingEvent != null) RowPostPaintingEvent(e);
            if (RowPostPaintingEventEx != null) RowPostPaintingEventEx(this,e);
            
        }
        private void CustomRowPostPaint(Graphics graphics, DataGridView dataGridView, int rowIndex)
        {
            var rowBounds = dataGridView.GetRowDisplayRectangle(rowIndex, true);

            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            if (this.dataGridView.Rows[rowIndex].Selected || (_rowBorderStyleOption == RowBorderStyleOption.BottomLine))
            {
                using (Brush brush_background = new SolidBrush(this.RowsColor))
                using (Brush brush_check_background = new SolidBrush(checkedRowBackColor))
                using (Pen pen_check_border = new Pen(cellBorderColor))
                using (Brush brush_back = new SolidBrush(((this.dataGridView.Rows[rowIndex].Selected) ? selectedRowBackColor : RowsColor)))
                using (Pen pen_Border = new Pen(selectedRowBorderColor))
                {
                    Color _selectedRowForeColor = ((this.dataGridView.Rows[rowIndex].Selected) ? selectedRowForeColor : cellStylForeColor);
                    int penWidth = (int)pen_Border.Width;
                    int col_width = this.dataGridView.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                    int RowHeadersWidth = this.dataGridView.RowHeadersWidth;
                    if (!顯示首列) RowHeadersWidth = 0;

                    int x = rowBounds.Left + (penWidth / 2) + 1 + RowHeadersWidth;
                    int y = rowBounds.Top + (penWidth / 2);
                    int width = col_width - penWidth - 1;
                    int height = rowBounds.Height - penWidth;

                    if (selectedRowBackColor != Color.Transparent && dataKeyEnable == false)
                    {
                        graphics.FillRectangle(brush_back, x, y, width, height);
    
                    }
                    if (dataKeyEnable)
                    {
                        object[] value = GetRowValues(rowIndex);
                        if (value != null)
                        {
                            if (DataKeysClasses.GetDataKeysCheck(value[0].ObjectToString()))
                            {
                                graphics.FillRectangle(brush_check_background, x, y, width, height);
                            }
                            else
                            {
                                graphics.FillRectangle(brush_background, x, y, width, height);
                            }
                        }
                    }
                    if (_rowBorderStyleOption == RowBorderStyleOption.BottomLine)
                    {
                        graphics.DrawRectangle(new Pen(RowsColor), x, y, width, height);
                        DrawTopLine(graphics, rowBounds, cellBorderColor, 1);
                        DrawBottomLine(graphics, rowBounds, cellBorderColor, 1);
                    }
                    if (this.selectedBorderSize > 0 && this.dataGridView.Rows[rowIndex].Selected) graphics.DrawRectangle(pen_Border, x, y, width, height);

                    DataGridViewCellCollection cells = this.dataGridView.Rows[rowIndex].Cells;
                    for (int i = 0; i < cells.Count; i++)
                    {
                        if (_顯示CheckBox)
                        {
                            if (rowIndex >= 0 && i == 0)
                            {
                                Rectangle rectangle = dataGridView.GetCellDisplayRectangle(i, rowIndex, false);
                                ControlPaint.DrawCheckBox(graphics, rectangle.X + 1, rectangle.Y + 1,
                                    rectangle.Width - 2, rectangle.Height - 2,
                                    (bool)cells[i].FormattedValue ? ButtonState.Checked : ButtonState.Normal);
                                continue;
                            }
                        }

                        if (selectedRowBackColor != Color.Transparent)
                        {
                            if (cells[i].Value != null && cells[i].Visible)
                            {
                                string columnName = i >= 0 ? this.dataGridView.Columns[i].Name : string.Empty;
                                ColumnElement columnElement = Columns.GetColumn(columnName);
                                Font font = columnElement.TextFont ?? cellStyleFont;

                                if (columnElement.OtherType == Table.OtherType.ENUM)
                                {
                                    Rectangle displayRect = dataGridView.GetCellDisplayRectangle(i, rowIndex, true);
                                    graphics.FillRectangle(brush_back, displayRect);

                                    Rectangle rect = new Rectangle(displayRect.X + (displayRect.Width - 20), displayRect.Y + 2, 20, displayRect.Height - 4);
                                    ComboBoxRenderer.DrawDropDownButton(graphics, rect, System.Windows.Forms.VisualStyles.ComboBoxState.Disabled);
                                    displayRect.Width -= 20;
                                    DrawString(graphics, cells[i].Value.ToString(), font, displayRect, _selectedRowForeColor, columnElement.Alignment);
                                }
                                else
                                {
                                    Rectangle rectangle = dataGridView.GetCellDisplayRectangle(i, rowIndex, false);
                                    if (dataKeyEnable == false)
                                    {
                                        DrawString(graphics, cells[i].Value.ToString(), font, rectangle, _selectedRowForeColor, columnElement.Alignment);
                                    }
                                    else
                                    {

                                        DrawString(graphics, cells[i].Value.ToString(), font, rectangle, _selectedRowForeColor, columnElement.Alignment);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                
            }
        }
        private void DataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
  
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
                bool flag_replace =  false;
                if (this.RowEndEditEvent != null) flag_replace = this.RowEndEditEvent(value , e.RowIndex,e.ColumnIndex, value[col_index].ToString());

                if(flag_replace)
                {
                    this.ReplaceExtra(value, true);
                }
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
            if (Columns[col_index].CanEdit == false) return;

            if (this.CellValidatingEvent != null) CellValidatingEvent(value, e.RowIndex, e.ColumnIndex, e.FormattedValue.ToString(), e);
        }
        private void DataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void DataGridView_Click(object sender, EventArgs e)
        {
          
        }
        private void DataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            for (int i = 0; i < comboBoxes.Count; i++)
            {
                comboBoxes[i].Visible = false;
                comboBoxes[i].DroppedDown = false;

            }
        }
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            List<object[]> list_value = this.Get_All_Select_RowsValues();
        }


        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView.CurrentCell != null)
            {
                ComboBox comboBox = (ComboBox)sender;
                List<object[]> list_rowValues = this.Get_All_Select_RowsValues();
                if (list_rowValues.Count == 0) return;
                object[] rowValue = this.Get_All_Select_RowsValues()[0];

                string colName = comboBox.Name;
                int col_index = Columns.GetColumnIndex(colName);

                if (col_index == -1) return;

                rowValue[col_index] = comboBox.SelectedItem.ToString();
                //this.ReplaceExtra(rowValue, true);
                comboBox.Visible = false;
                comboBox.DroppedDown = false;
                if (ComboBoxSelectedIndexChangedEvent != null) ComboBoxSelectedIndexChangedEvent(sender, comboBox.Name, rowValue);
            }
        }
        private void CheckBoxHeader_CheckedChanged(object sender, EventArgs e)
        {
            if (!_顯示CheckBox) return;
            if (dataGridView.Rows.Count == 0) return;
            dataGridView.ClearSelection();
            this.SetSelectRow(0);
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

        public void On_RowDoubleClick(int SelectRowindex)
        {
            if (SelectRowindex_Buf != SelectRowindex)
            {
                SelectRowindex_Buf = SelectRowindex;
                object[] value = this.GetRowValues(SelectRowindex);
                if (value != null)
                {
                    if (this.RowEnterEvent != null) this.RowDoubleClickEvent(value);
                }
            }
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
         
            if (SelectRowindex_Buf != SelectRowindex)
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
        private void DrawString(Graphics e, string text, Font font, Rectangle rectangle, Color forecolor, DataGridViewContentAlignment dataGridViewContentAlignment)
        {
            if (text == null) return;
            if (text.Check_Date_String())
            {
                text = text.StringToDateTime().ToDateTimeString();
            }
            Rectangle rectangle_text = new Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);

            SizeF size_Text_temp = e.MeasureString(text, font, new SizeF(rectangle_text.Width, rectangle_text.Height), StringFormat.GenericDefault);
            Size size_Text = new Size((int)size_Text_temp.Width, (int)size_Text_temp.Height);
            Point point = new Point(0, 0);
            if (dataGridViewContentAlignment == DataGridViewContentAlignment.TopLeft)
            {
                point = new Point(0, 0);
            }
            else if (dataGridViewContentAlignment == DataGridViewContentAlignment.TopCenter)
            {
                point = new Point((rectangle_text.Width - size_Text.Width) / 2, 0);
            }
            else if (dataGridViewContentAlignment == DataGridViewContentAlignment.TopRight)
            {
                point = new Point((rectangle_text.Width - size_Text.Width), 0);
            }
            else if (dataGridViewContentAlignment == DataGridViewContentAlignment.MiddleLeft)
            {
                point = new Point(0, (rectangle_text.Height - size_Text.Height) / 2);
            }
            else if (dataGridViewContentAlignment == DataGridViewContentAlignment.MiddleCenter)
            {
                point = new Point((rectangle_text.Width - size_Text.Width) / 2, (rectangle_text.Height - size_Text.Height) / 2);
            }
            else if (dataGridViewContentAlignment == DataGridViewContentAlignment.MiddleRight)
            {
                point = new Point((rectangle_text.Width - size_Text.Width), (rectangle_text.Height - size_Text.Height) / 2);
            }
            else if (dataGridViewContentAlignment == DataGridViewContentAlignment.BottomLeft)
            {
                point = new Point(0, (rectangle_text.Height - size_Text.Height));
            }
            else if (dataGridViewContentAlignment == DataGridViewContentAlignment.BottomCenter)
            {
                point = new Point((rectangle_text.Width - size_Text.Width) / 2, (rectangle_text.Height - size_Text.Height));
            }
            else if (dataGridViewContentAlignment == DataGridViewContentAlignment.BottomRight)
            {
                point = new Point((rectangle_text.Width - size_Text.Width), (rectangle_text.Height - size_Text.Height));
            }

            rectangle.X += point.X;
            rectangle.Y += point.Y;

            e.DrawString($"{text}", font, new SolidBrush(forecolor), rectangle, StringFormat.GenericDefault);
        }
        private Rectangle GetFirstColumnDimensions(DataGridView dataGridView)
        {
            if (dataGridView.Columns.Count == 0 )
            {
                MessageBox.Show("DataGridView中沒有行。");
                return new Rectangle(0,0,0,0);
            }

            // 獲取首列的列索引
            int firstColumnIndex = 0;

            Rectangle headerRectangle = dataGridView.GetCellDisplayRectangle(firstColumnIndex, -1, true);
            int col_width = this.dataGridView.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            int RowHeadersWidth = this.dataGridView.RowHeadersWidth;
            // 取得 X, Y, Width 和 Height
            int x = headerRectangle.X;
            int y = headerRectangle.Y;
            int width = col_width;
            int height = headerRectangle.Height;

            Rectangle rectangle = new Rectangle(x, y, width, height);
            return rectangle;
        }
        private void DrawBottomLine(Graphics e, Rectangle rectangle, Color lineColor, int lineWidth)
        {
            using (Pen pen = new Pen(lineColor, lineWidth))
            {
                e.DrawLine(pen, rectangle.Left, rectangle.Bottom - lineWidth / 2, rectangle.Right, rectangle.Bottom - lineWidth / 2);
            }
        }
        private void DrawTopLine(Graphics e, Rectangle rectangle, Color lineColor, int lineWidth)
        {
            using (Pen pen = new Pen(lineColor, lineWidth))
            {
                e.DrawLine(pen, rectangle.Left, rectangle.Top + lineWidth / 2, rectangle.Right, rectangle.Top + lineWidth / 2);
            }
        }

        private void DrawLeftLine(Graphics e, Rectangle rectangle, Color lineColor, int lineWidth)
        {
            using (Pen pen = new Pen(lineColor, lineWidth))
            {
                e.DrawLine(pen, rectangle.Left + lineWidth / 2, rectangle.Top, rectangle.Left + lineWidth / 2, rectangle.Bottom);
            }
        }

        private void DrawRightLine(Graphics e, Rectangle rectangle, Color lineColor, int lineWidth)
        {
            using (Pen pen = new Pen(lineColor, lineWidth))
            {
                e.DrawLine(pen, rectangle.Right - lineWidth / 2, rectangle.Top, rectangle.Right - lineWidth / 2, rectangle.Bottom);
            }
        }

        static public class ColorSerializationHelper
        {
            static public Color FromString(string value)
            {
                var parts = value.Split(':');

                int A = 0;
                int R = 0;
                int G = 0;
                int B = 0;
                int.TryParse(parts[0], out A);
                int.TryParse(parts[1], out R);
                int.TryParse(parts[2], out G);
                int.TryParse(parts[3], out B);
                return Color.FromArgb(A, R, G, B);
            }
            static public string ToString(Color color)
            {
                return color.A + ":" + color.R + ":" + color.G + ":" + color.B;

            }
        }
        [TypeConverter(typeof(FontConverter))]
        static public class FontSerializationHelper
        {
            static public Font FromString(string value)
            {
                if (value.StringIsEmpty()) return null;
                var parts = value.Split(':');
                return new Font(
                    parts[0],                                                   // FontFamily.Name
                    float.Parse(parts[1]),                                      // Size
                    EnumSerializationHelper.FromString<FontStyle>(parts[2]),    // Style
                    EnumSerializationHelper.FromString<GraphicsUnit>(parts[3]), // Unit
                    byte.Parse(parts[4]),                                       // GdiCharSet
                    bool.Parse(parts[5])                                        // GdiVerticalFont
                );
            }
            static public string ToString(Font font)
            {
                if (font == null) return "";
                return font.FontFamily.Name
                        + ":" + font.Size
                        + ":" + font.Style
                        + ":" + font.Unit
                        + ":" + font.GdiCharSet
                        + ":" + font.GdiVerticalFont
                        ;
            }
        }
        [TypeConverter(typeof(EnumConverter))]
        static public class EnumSerializationHelper
        {
            static public T FromString<T>(string value)
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
        }
    }
    static public class ColumnElementMethod
    {
        static public SQL_DataGridView.ColumnElement GetColumn(this List<SQL_DataGridView.ColumnElement> columns, string columnName)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                if (columns[i].Text == columnName || columns[i].Name == columnName) return columns[i];
            }
            return null;
        }
        static public int GetColumnIndex(this List<SQL_DataGridView.ColumnElement> columns, string columnName)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                if (columns[i].Text == columnName || columns[i].Name == columnName) return i;
            }
            return -1;
        }
    }

    static public class DataKeysClassMethod
    {
        static public DataKeysClass SortDictionaryByDataKeysClass(this System.Collections.Generic.Dictionary<string, DataKeysClass> dictionary, string guid)
        {
            if (dictionary.ContainsKey(guid))
            {
                return dictionary[guid];
            }
            return null;
        }
        static public System.Collections.Generic.Dictionary<string, DataKeysClass> CoverToDictionaryByGUID(this List<DataKeysClass> dataKeysClasses)
        {
            Dictionary<string, DataKeysClass> dictionary = new Dictionary<string, DataKeysClass>();

            foreach (var item in dataKeysClasses)
            {
                string key = item.GUID;
                dictionary[key] = item;
            }

            return dictionary;
        }

        static public void SetDataKey(this List<DataKeysClass> dataKeysClasses, string GUID)
        {
            System.Collections.Generic.Dictionary<string, DataKeysClass> keyValuePairs = dataKeysClasses.CoverToDictionaryByGUID();
            DataKeysClass dataKeysClass_temp = keyValuePairs.SortDictionaryByDataKeysClass(GUID);
            if (dataKeysClass_temp == null)
            {
                DataKeysClass dataKeysClass = new DataKeysClass();
                dataKeysClass.GUID = GUID;
                dataKeysClass.check = true;
                dataKeysClasses.Add(dataKeysClass);
            }

        }
        static public void UpdateDataKey(this List<DataKeysClass> dataKeysClasses, string GUID)
        {
            System.Collections.Generic.Dictionary<string, DataKeysClass> keyValuePairs = dataKeysClasses.CoverToDictionaryByGUID();
            DataKeysClass dataKeysClass_temp = keyValuePairs.SortDictionaryByDataKeysClass(GUID);
            if (dataKeysClass_temp == null)
            {
                DataKeysClass dataKeysClass = new DataKeysClass();
                dataKeysClass.GUID = GUID;
                dataKeysClass.check = true;
                dataKeysClasses.Add(dataKeysClass);
            }
            else
            {
                dataKeysClass_temp.check = !dataKeysClass_temp.check;
            }
        }
        static public void AddDataKey(this List<DataKeysClass> dataKeysClasses, DataKeysClass dataKeysClass)
        {
            AddDataKey(dataKeysClasses, dataKeysClass.GUID, dataKeysClass.check);
        }
        static public void AddDataKey(this List<DataKeysClass> dataKeysClasses, string GUID, bool flag_checked)
        {
            System.Collections.Generic.Dictionary<string, DataKeysClass> keyValuePairs = dataKeysClasses.CoverToDictionaryByGUID();
            DataKeysClass dataKeysClass_temp = keyValuePairs.SortDictionaryByDataKeysClass(GUID);
            if (dataKeysClass_temp == null)
            {
                DataKeysClass dataKeysClass = new DataKeysClass();
                dataKeysClass.GUID = GUID;
                dataKeysClass.check = flag_checked;
                dataKeysClasses.Add(dataKeysClass);
            }
            else
            {
                dataKeysClass_temp.check = flag_checked;
            }
        }
        static public void ClearDataKey(this List<DataKeysClass> dataKeysClasses)
        {
            dataKeysClasses.Clear();
        }
      
        static public void DeleteDataKey(this List<DataKeysClass> dataKeysClasses, DataKeysClass dataKeysClass)
        {
            System.Collections.Generic.Dictionary<string, DataKeysClass> keyValuePairs = dataKeysClasses.CoverToDictionaryByGUID();
            DataKeysClass dataKeysClass_temp = keyValuePairs.SortDictionaryByDataKeysClass(dataKeysClass.GUID);
            if (dataKeysClass_temp != null)
            {
                dataKeysClasses.Remove(dataKeysClass);
            }
        }
        static public DataKeysClass GetDataKeysClass(this List<DataKeysClass> dataKeysClasses, string GUID)
        {
            System.Collections.Generic.Dictionary<string, DataKeysClass> keyValuePairs = dataKeysClasses.CoverToDictionaryByGUID();
            DataKeysClass dataKeysClass_temp = keyValuePairs.SortDictionaryByDataKeysClass(GUID);
            return dataKeysClass_temp;
        }
        static public bool GetDataKeysCheck(this List<DataKeysClass> dataKeysClasses, string GUID)
        {
            System.Collections.Generic.Dictionary<string, DataKeysClass> keyValuePairs = dataKeysClasses.CoverToDictionaryByGUID();
            DataKeysClass dataKeysClass_temp = keyValuePairs.SortDictionaryByDataKeysClass(GUID);
            if (dataKeysClass_temp == null) return false;
            return dataKeysClass_temp.check;
        }

    }
    public class DataKeysClass
    {
        public string GUID = "";
        public bool check = false;

      
    }
}
