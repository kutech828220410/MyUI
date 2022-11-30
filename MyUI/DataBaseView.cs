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

namespace MyUI
{
    public partial class DataBaseView : UserControl
    {
        public override System.Windows.Forms.Layout.LayoutEngine LayoutEngine
        {
            get
            {
                return base.LayoutEngine;
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
        #region 自訂屬性

        bool _顯示首行 = true;
        [ReadOnly(false), Browsable(true), Category("自訂屬性-MessageBox"), Description(""), DefaultValue("")]
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
        DataGridViewHeaderBorderStyle _首行樣式 = DataGridViewHeaderBorderStyle.Raised;
        [ReadOnly(false), Browsable(true), Category("自訂屬性-MessageBox"), Description(""), DefaultValue("")]
        public DataGridViewHeaderBorderStyle 首行樣式
        {
            get
            {
                
                return _首行樣式;
            }
            set
            {
                dataGridView.ColumnHeadersBorderStyle = _首行樣式;
                _首行樣式 = value;
            }
        }
        DataGridViewHeaderBorderStyle _首列樣式 = DataGridViewHeaderBorderStyle.Raised;
        [ReadOnly(false), Browsable(true), Category("自訂屬性-MessageBox"), Description(""), DefaultValue("")]
        public DataGridViewHeaderBorderStyle 首列樣式
        {
            get
            {
                return _首列樣式;
            }
            set
            {
                dataGridView.RowHeadersBorderStyle = _首列樣式;
                _首列樣式 = value;
            }
        }
        System.Windows.Forms.BorderStyle _邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
        [ReadOnly(false), Browsable(true), Category("自訂屬性-MessageBox"), Description(""), DefaultValue("")]
        public System.Windows.Forms.BorderStyle 邊框樣式
        {
            get
            {
                return _邊框樣式;
            }
            set
            {
                dataGridView.BorderStyle = _邊框樣式;
                _邊框樣式 = value;
            }
        }
        DataGridViewCellBorderStyle _單格樣式 = DataGridViewCellBorderStyle.Raised;
        [ReadOnly(false), Browsable(true), Category("自訂屬性-MessageBox"), Description(""), DefaultValue("")]
        public DataGridViewCellBorderStyle 單格樣式
        {
            get
            {
                return _單格樣式;
            }
            set
            {
                dataGridView.CellBorderStyle = _單格樣式;
                _單格樣式 = value;
            }
        }
        bool _顯示首列 = true;
        [ReadOnly(false), Browsable(true), Category("自訂屬性-MessageBox"), Description(""), DefaultValue("")]
        public bool 顯示首列
        {
            get
            {
                return _顯示首列;
            }
            set
            {
                dataGridView.RowHeadersVisible = value;
                _顯示首列 = value;
            }
        }
        #endregion
        #region AddColumns
        public class Columns
        {
            public string Name;
            public int Width;
            public DataGridViewColumnSortMode SortMode;
            public Color BackGroundColor;
            public DataGridViewContentAlignment Alignment;
        }
        public List<Columns> ColumnsList = new List<Columns>();
        public void AddColumns(string Name, int Width, DataGridViewColumnSortMode SortMode, Color BackGroundColor, DataGridViewContentAlignment Alignment)
        {
            Columns temp = new Columns();
            temp.Name = Name;
            temp.Width = Width;
            temp.SortMode = SortMode;
            temp.BackGroundColor = BackGroundColor;
            temp.Alignment = Alignment;
            ColumnsList.Add(temp);
            ModuleClass.AddDataGridValue(Name, "");
        }
        public void AddColumns(string Name, int Width, DataGridViewColumnSortMode SortMode, DataGridViewContentAlignment Alignment)
        {
            Columns temp = new Columns();
            temp.Name = Name;
            temp.Width = Width;
            temp.SortMode = SortMode;
            temp.BackGroundColor = Color.LightGray;
            temp.Alignment = Alignment;
            ColumnsList.Add(temp);
            ModuleClass.AddDataGridValue(Name, "");
        }
        public void AddColumns(string Name, int Width, DataGridViewColumnSortMode SortMode)
        {
            Columns temp = new Columns();
            temp.Name = Name;
            temp.Width = Width;
            temp.SortMode = SortMode;
            temp.BackGroundColor = Color.LightGray;
            temp.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColumnsList.Add(temp);
            ModuleClass.AddDataGridValue(Name, "");
        }
        public void AddColumns(string Name, int Width, DataGridViewContentAlignment Alignment)
        {
            Columns temp = new Columns();
            temp.Name = Name;
            temp.Width = Width;
            temp.SortMode = DataGridViewColumnSortMode.NotSortable;
            temp.BackGroundColor = Color.LightGray;
            temp.Alignment = Alignment;
            ColumnsList.Add(temp);
            ModuleClass.AddDataGridValue(Name, "");
        }
        public void AddColumns(string Name, int Width)
        {
            Columns temp = new Columns();
            temp.Name = Name;
            temp.Width = Width;
            temp.SortMode = DataGridViewColumnSortMode.NotSortable;
            temp.BackGroundColor = Color.LightGray;
            temp.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColumnsList.Add(temp);
            ModuleClass.AddDataGridValue(Name, "");
        }
        public void AddColumns(string Name, int Width, Color BackGroundColor, DataGridViewContentAlignment Alignment)
        {
            Columns temp = new Columns();
            temp.Name = Name;
            temp.Width = Width;
            temp.SortMode = DataGridViewColumnSortMode.NotSortable;
            temp.BackGroundColor = BackGroundColor;
            temp.Alignment = Alignment;
            ColumnsList.Add(temp);
            ModuleClass.AddDataGridValue(Name, "");
        }
        public void AddColumns(string Name, int Width, Color BackGroundColor)
        {
            Columns temp = new Columns();
            temp.Name = Name;
            temp.Width = Width;
            temp.SortMode = DataGridViewColumnSortMode.NotSortable;
            temp.BackGroundColor = BackGroundColor;
            temp.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColumnsList.Add(temp);
            ModuleClass.AddDataGridValue(Name, "");
        }
        #endregion

        public ModuleDataGridViewClass ModuleDataGridView;
        public ModuleListCtlClass ModuleListCtl = new ModuleListCtlClass();

        public DataBaseView()
        {               
            InitializeComponent();
        }

        private void DataBaseView_Load(object sender, EventArgs e)
        {
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.ReadOnly = true;
            
        }
        public void Init(string FileName)
        {
            ModuleDataGridView = new ModuleDataGridViewClass(ModuleListCtl, this.dataGridView, ColumnsList);
            ModuleListCtl.SetFileName(FileName);
            ModuleDataGridView.DataTableInit();
           
        }
        #region Module
        [Serializable]
        public class ModuleClass
        {
            public DataGridValueClass DataGridValue = new DataGridValueClass();   
            [Serializable]
            public class DataGridValueClass
            {
                public List<string> Name = new List<string>();
                public List<object> val = new List<object>();
            }
 
            static DataGridValueClass Base_DataGridValue = new DataGridValueClass();
            /// <summary>
            /// 增加Modeule內類別Data
            /// </summary>
            /// <param name="Name">引入:變數名稱</param>
            /// <param name="val">引入:變數內容</param>
            /// <returns>無</returns>
            static public void AddDataGridValue(string Name, object val)
            {
                Base_DataGridValue.val.Add(val);
            }
            /// <summary>
            /// 創建Moudule類別
            /// </summary>
            /// <param name="Class">引入:類別</param>
            /// <returns>Moudule類別</returns>
            static public ModuleClass CreatClass(object Class)
            {
                ModuleClass _ModuleClass = new ModuleClass();
                object[] objArray = Reflection.GetClassFields(Class);
                string[] stringName = Reflection.GetClassFieldsName(Class);
                foreach (string str in stringName)
                {
                    _ModuleClass.DataGridValue.Name.Add(str);
                }
                foreach (object obj in objArray)
                {
                    _ModuleClass.DataGridValue.val.Add(obj);
                }
                return _ModuleClass;
            }
            static public ModuleClass CreatClass(string[] ColName,object[] objValue)
            {
                ModuleClass _ModuleClass = new ModuleClass();
                foreach (string str in ColName)
                {
                    _ModuleClass.DataGridValue.Name.Add(str);
                }
                foreach (object obj in objValue)
                {
                    _ModuleClass.DataGridValue.val.Add(obj);
                }
                return _ModuleClass;
            }
            public int GetNameIndex(string Name)
            {
                for (int i = 0; i < this.DataGridValue.Name.Count; i++)
                {
                    if (Name == DataGridValue.Name[i]) return i;
                }
                return -1;
            }
            public object GetNameValue(string Name)
            {
                for (int i = 0; i < this.DataGridValue.Name.Count; i++)
                {
                    if (Name == DataGridValue.Name[i]) return (string)DataGridValue.val[i];
                }
                return "-1";
            }
            public void SetNameValue(string Name , string val)
            {
                for (int i = 0; i < this.DataGridValue.Name.Count; i++)
                {
                    if (Name == DataGridValue.Name[i]) DataGridValue.val[i] = val;
                }
            }
            public string[] GetAllName()
            {
                return this.DataGridValue.Name.ToArray();
            }
            public object[] GetAllValue()
            {
                return this.DataGridValue.val.ToArray();
            }
        }
        [Serializable]
        public class ModuleListCtlClass
        {
            #region Event
            public delegate void ModuleChangeEventHandler(List<ModuleClass> ModuleList);
            public event ModuleChangeEventHandler ModuleChangeEvent;
            public delegate void LoadModuleEventHandler();
            public event LoadModuleEventHandler LoadModuleEvent;
            public delegate void SaveModuleEventHandler();
            public event SaveModuleEventHandler SaveModuleEvent;
            #endregion
            [Serializable]
            private class FileSaveClass
            {
                public List<ModuleClass> ModuleList = new List<ModuleClass>();
            }
            public List<ModuleClass> ModuleList = new List<ModuleClass>();
            private ModuleClass CurrentModule;
            private string FileName = ".\\" + "" + ".val";
            /// <summary>
            /// 設置存檔檔名
            /// </summary>
            /// <param name="Name">引入:檔名</param>
            /// <returns>無</returns>
            public void SetFileName(string Name)
            {
                FileName = ".\\" + Name + ".val";
            }
            #region SetCurrentModule
            /// <summary>
            /// 設定當前作用Module
            /// </summary>
            /// <param name="Module">引入:ModuleClass</param>
            /// <returns>無</returns>
            public void SetCurrentModule(ModuleClass Module)
            {
                CurrentModule = Module.DeepClone();
            }
            /// <summary>
            /// 設定當前作用Module
            /// </summary>
            /// <param name="Module">引入:(object)ModuleClass</param>
            /// <returns>無</returns>
            public void SetCurrentModule(object Module)
            {
                CurrentModule = DataBaseView.ModuleClass.CreatClass(Module).DeepClone();
            }
            #endregion
            #region GetCurrentModule
            /// <summary>
            /// 取得當前作用Module
            /// </summary>
            /// <returns>ModuleClass</returns>
            public ModuleClass GetCurrentModule()
            {
                return CurrentModule;
            }
            /// <summary>
            /// 取得當前作用Module
            /// </summary>
            ///  <param name="Class">引入:(object)ModuleClass</param>
            /// <returns>無</returns>
            public void GetCurrentModule(ref object Class)
            {
                ModuleClass ModuleClass = GetCurrentModule();
                if (ModuleClass != null)
                {
                    string[] strThisModule = ModuleClass.DataGridValue.Name.ToArray();
                    string[] strClass = Basic.Reflection.GetClassFieldsName(Class);
                    object[] obj = ModuleClass.DataGridValue.val.ToArray();
                    for (int i = 0; i < strClass.Length; i++)
                    {
                        if (strClass[i] == strThisModule[i])
                        {
                            Reflection.SetClassField(Class, strClass[i], obj[i]);
                        }
                    }
                }
            }
            #endregion
            public bool IsModuleNull()
            {
                return (CurrentModule == null);
            }
            #region AddModule
            /// <summary>
            /// 增加Module
            /// </summary>
            ///  <param name="ModuleClass">引入:ModuleClass</param>
            /// <returns>無</returns>
            public void AddModule(ModuleClass Module)
            {
                SetCurrentModule(Module);
                AddModule();
            }
            /// <summary>
            /// 增加Module
            /// </summary>
            ///  <param name="ModuleClass">引入:(object)ModuleClass</param>
            /// <returns>無</returns>
            public void AddModule(object Module)
            {
                SetCurrentModule(DataBaseView.ModuleClass.CreatClass(Module));
                AddModule();
            }
            /// <summary>
            /// 增加Module,來源於CurrentModule
            /// </summary>
            /// <returns>無</returns>
            public void AddModule()
            {
                if (!IsModuleNull())
                {
                    ModuleList.Add(CurrentModule);
                    if (ModuleChangeEvent != null) ModuleChangeEvent(this.ModuleList);
                }
            }
            #endregion
            #region GetModule
            /// <summary>
            /// 取得指定搜尋內容Module
            /// </summary>
            ///  <param name="ValName">引入:變數內容</param>
            ///  <param name="TypeName">引入:變數名稱</param>
            /// <returns>ModuleClass</returns>
            public ModuleClass GetModule(string ValName, string TypeName)
            {
                ModuleClass ModuleClass = null;
                for (int i = 0; i < ModuleList.Count; i++)
                {
                    for (int j = 0; j < ModuleList[i].DataGridValue.Name.Count; j++)
                    {
                        if (ModuleList[i].DataGridValue.Name[j] == TypeName)
                        {
                            if ((string)ModuleList[i].DataGridValue.val[j] == ValName) ModuleClass = ModuleList[i];      
                        }
                    }
                }
                return ModuleClass;
            }
            /// <summary>
            /// 取得指定搜尋內容Module
            /// </summary>
            ///  <param name="ValName">引入:變數內容</param>
            ///  <param name="TypeName">引入:變數名稱</param>
            ///  <param name="Class">引入:(object)ModuleClass</param>
            /// <returns>ModuleClass</returns>
            public ModuleClass GetModule(string ValName, string TypeName, ref object Class)
            {
                ModuleClass ModuleClass = null;
                for (int i = 0; i < ModuleList.Count; i++)
                {
                    for (int j = 0; j < ModuleList[i].DataGridValue.Name.Count; j++)
                    {
                        if (ModuleList[i].DataGridValue.Name[j] == TypeName)
                        {
                            if ((string)ModuleList[i].DataGridValue.val[j] == ValName) ModuleClass = ModuleList[i];     
                        }
                    }
                }

                if (ModuleClass != null)
                {
                    string[] strThisModule = ModuleClass.DataGridValue.Name.ToArray();
                    string[] strClass = Basic.Reflection.GetClassFieldsName(Class);
                    object[] obj = ModuleClass.DataGridValue.val.ToArray();
                    for (int i = 0; i < strClass.Length; i++)
                    {
                        if (strClass[i] == strThisModule[i])
                        {
                            Reflection.SetClassField(Class, strClass[i], obj[i]);
                        }
                    }
                }
                

                return ModuleClass;
            }
            #endregion
            /// <summary>
            /// 取得ModuleList內編號
            /// </summary>
            ///  <param name="ValName">引入:變數內容</param>
            ///  <param name="TypeName">引入:變數名稱</param>
            /// <returns>索引編號</returns>
            public int GetModuleIndex(string ValName, string TypeName)
            {
                for (int i = 0; i < ModuleList.Count; i++)
                {
                    for (int j = 0; j < ModuleList[i].DataGridValue.Name.Count; j++)
                    {
                        if (ModuleList[i].DataGridValue.Name[j] == TypeName)
                        {
                            if ((string)ModuleList[i].DataGridValue.val[j] == ValName) return i;
                        }
                    }
                }
                return -1;
            }
            /// <summary>
            /// 取代指定ModuleList內指定編號內容
            /// </summary>
            ///  <param name="ValName">引入:變數內容</param>
            ///  <param name="TypeName">引入:變數名稱</param>
            ///  <param name="Class">引入:(object)ModuleClass</param>
            /// <returns>索引編號</returns>
            public void RelpaceModudle(string ValName, string TypeName, object Class)
            {
                int index = GetModuleIndex(ValName, TypeName);
                if (index != -1)
                {
                    if (index < ModuleList.Count && index != -1)
                    {
                        ModuleClass ModuleClass = DataBaseView.ModuleClass.CreatClass(Class);
                        ModuleList[index] = ModuleClass.DeepClone();
                        if (ModuleChangeEvent != null) this.ModuleChangeEvent(this.ModuleList);
                    }
                }         
            }
            public void RelpaceModudle(string ValName, string TypeName, string[] ColName, object[] objValue)
            {
                int index = GetModuleIndex(ValName, TypeName);
                if (index != -1)
                {
                    if (index < ModuleList.Count && index != -1)
                    {
                        ModuleClass ModuleClass = DataBaseView.ModuleClass.CreatClass(ColName, objValue);
                        ModuleList[index] = ModuleClass.DeepClone();
                        if (ModuleChangeEvent != null) this.ModuleChangeEvent(this.ModuleList);
                    }
                }
            }
            /// <summary>
            /// 取代指定ModuleList內指定編號內容
            /// </summary>
            ///  <param name="index">引入:編號</param>
            ///  <param name="Class">引入:(object)ModuleClass</param>
            /// <returns>索引編號</returns>
            public void RelpaceModudle(int index ,object Class)
            {
                if (index < ModuleList.Count && index != -1)
                {
                    ModuleClass ModuleClass = DataBaseView.ModuleClass.CreatClass(Class);
                    ModuleList[index] = ModuleClass.DeepClone();
                    if (ModuleChangeEvent != null) this.ModuleChangeEvent(this.ModuleList);
                }
            }
            #region DeleteModule
            /// <summary>
            /// 刪除指定搜索內容Module
            /// </summary>
            ///  <param name="ValName">引入:變數內容</param>
            ///  <param name="TypeName">引入:變數名稱</param>
            /// <returns>無</returns>
            public void DeleteModule(string ValName, string TypeName)
            {
                for (int i = 0; i < ModuleList.Count; i++)
                {
                    if (ModuleList.Count <= 0) return;
                    for (int j = 0; j < ModuleList[i].DataGridValue.Name.Count; j++)
                    {
                        if (ModuleList[i].DataGridValue.Name[j] == TypeName)
                        {
                            if ((string)ModuleList[i].DataGridValue.val[j] == ValName)
                            {
                                ModuleList.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    if (i >= 0) i--;
                }
                this.ModuleChangeEvent(this.ModuleList);
            }
            /// <summary>
            /// 刪除指定編號Module
            /// </summary>
            ///  <param name="index">引入:編號</param>
            /// <returns>無</returns>
            public void DeleteModule(int index)
            {
                this.ModuleList.RemoveAt(index);
                ModuleChangeEvent(this.ModuleList);
            }
            #endregion

            public void ClearModuleList()
            {
                ModuleList.Clear();
                this.ModuleChangeEvent(this.ModuleList);
            }
            public void RefreshModule()
            {
                if (ModuleChangeEvent != null) this.ModuleChangeEvent(this.ModuleList);
            }
            /// <summary>
            /// 儲存Module內容
            /// </summary>
            /// <returns>無</returns>
            public void SaveModules()
            {
                FileSaveClass FileSaveClass = new ModuleListCtlClass.FileSaveClass();
                FileSaveClass.ModuleList = this.ModuleList;
                FileIO.SaveProperties((object)FileSaveClass, FileName);
                if (SaveModuleEvent != null) SaveModuleEvent();
            }
            /// <summary>
            /// 讀取Module內容
            /// </summary>
            /// <returns>無</returns>
            public void LoadModules()
            {
                FileSaveClass FileSaveClass;
                object FileSaveClass_buf = new object();
                FileIO.LoadProperties(ref FileSaveClass_buf, FileName);
                try
                {
                    FileSaveClass = (FileSaveClass)FileSaveClass_buf;
                    this.ModuleList = FileSaveClass.ModuleList.DeepClone();
                }
                catch
                {
                    this.ModuleList = new List<ModuleClass>();
                }
                if (LoadModuleEvent != null) LoadModuleEvent();
                if (ModuleChangeEvent != null) ModuleChangeEvent(this.ModuleList);
            }
        }
        [Serializable]
        public class ModuleDataGridViewClass
        {

            #region Event
            public delegate object[] GetModuleValEventHandler(ModuleClass ModuleClass, object[] objArray);
            public event GetModuleValEventHandler GetModuleValEvent;
            public delegate void GridViewRefrshHandler();
            public event GridViewRefrshHandler GridViewRefrshEvent;
            #endregion

            private delegate void MainThreadUI();
            private ModuleListCtlClass ModuleListCtl;
            private DataGridView dataGridView;
            private DataTable dataTable = new DataTable();
            private DataTable dataTable_Buffer = new DataTable();
            private List<ModuleClass> _ModuleList;
            private List<Columns> ColumnsList;

            public ModuleDataGridViewClass(ModuleListCtlClass ModuleListCtl, DataGridView dataGridView, List<Columns> ColumnsList)
            {
                this.ModuleListCtl = ModuleListCtl;
                this.dataGridView = dataGridView;
                this.ColumnsList = ColumnsList;
                ModuleListCtl.ModuleChangeEvent += new ModuleListCtlClass.ModuleChangeEventHandler(ModuleChange);
                //DataTableInit();
            }
            private void ModuleChange(List<ModuleClass> ModuleList)
            {
                RefreshDataTable(ModuleList);
            }
            private void RefreshDataTable(List<ModuleClass> ModuleList)
            {
                _ModuleList = ModuleList;
                dataGridView.Invoke(new Action(delegate
                {
                    dataTable_Buffer = new DataTable();
                    foreach (Columns columns in ColumnsList)
                    {
                        dataTable_Buffer.Columns.Add(new DataColumn(columns.Name));
                    }
      
                    foreach (ModuleClass ModuleClass in _ModuleList)
                    {

                        object[] obj_array = GetModuleVal(ModuleClass);
                        dataTable_Buffer.Rows.Add(obj_array);
                    }
                    dataTable.Clear();
                    for (int Y = 0; Y < dataTable_Buffer.Rows.Count; Y++)
                    {
                        for (int X = 0; X < dataTable_Buffer.Columns.Count; X++)
                        {
                            if (Y >= dataTable.Rows.Count) dataTable.Rows.Add(dataTable_Buffer.Rows[Y][X]);
                            else dataTable.Rows[Y][X] = dataTable_Buffer.Rows[Y][X].DeepClone();
                        }
                    }
                    if (dataGridView.RowCount - 1 > 0)
                    {
                        dataGridView.Rows[dataGridView.RowCount - 1].Selected = true;
                        dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.RowCount - 1;
                    }
                    
                    dataGridView.Refresh();
                    if (GridViewRefrshEvent != null) GridViewRefrshEvent();
                }));
            }
            public virtual object[] GetModuleVal(ModuleClass ModuleClass)
            {
                object[] objArray = new object[dataGridView.ColumnCount];
                int index = -1;
                int val_index = 0;
                foreach (string str in ModuleClass.DataGridValue.Name)
                {
                    index = this.FindColumnNum(str);
                    if (index < objArray.Length && index != -1) objArray[index] = ModuleClass.DataGridValue.val[val_index];
                    val_index++;
                }
                if (GetModuleValEvent != null) GetModuleValEvent(ModuleClass, objArray);
                return objArray;
            }
            /// <summary>
            /// 初始化
            /// </summary>
            /// <returns>無</returns>
            public void DataTableInit()
            {
                dataGridView.Invoke(new Action(delegate
                {
                    dataTable = new DataTable();
                    foreach (Columns columns in ColumnsList)
                    {
                        dataTable.Columns.Add(new DataColumn(columns.Name));
                    }
                    dataGridView.DataSource = dataTable;
                    int i = 0;
                    foreach (Columns columns in ColumnsList)
                    {
                        dataGridView.Columns[i].DefaultCellStyle.BackColor = columns.BackGroundColor;
                        dataGridView.Columns[i].Width = columns.Width;
                        dataGridView.Columns[i].SortMode = columns.SortMode;
                        dataGridView.Columns[i].DefaultCellStyle.Alignment = columns.Alignment;
                        i++;
                    }

                 
                    dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView.MultiSelect = false;
                    dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                    dataGridView.AdvancedRowHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.InsetDouble;
                    dataGridView.Refresh();
                    // dataGridView.FirstDisplayedScrollingRowIndex = 0;
                }));
            }
            /// <summary>
            /// 刪除選擇的Module
            /// </summary>          
            /// <returns>無</returns>
            public void DeleteModule()
            {
                int i = this.GetSelectRow();
                if (i != -1)
                {
                    ModuleListCtl.DeleteModule(i);
                }                         
            }
            /// <summary>
            /// 取得選擇的Module
            /// </summary>
            /// <param name="Module">引入:(object)ModuleClass</param>
            /// <returns>ModuleClass</returns>
            public ModuleClass GetModule(ref object Class)
            {
                ModuleClass ModuleClass = null;
                int index = this.GetSelectRow();
                ModuleClass = ModuleListCtl.ModuleList[index];        
                if (ModuleClass != null)
                {
                    string[] strThisModule = ModuleClass.DataGridValue.Name.ToArray();
                    string[] strClass = Basic.Reflection.GetClassFieldsName(Class);
                    object[] obj = ModuleClass.DataGridValue.val.ToArray();
                    for (int i = 0; i < strClass.Length; i++)
                    {
                        if (strClass[i] == strThisModule[i])
                        {
                            Reflection.SetClassField(Class, strClass[i], obj[i]);
                        }
                    }
                }
                return ModuleClass;
            }
            /// <summary>
            /// 取得指定編號行數名稱
            /// </summary>
            /// <param name="Num">引入:編號</param>
            /// <returns>行名稱</returns>
            public string GetColumName(int Num)
            {
                if (Num < dataGridView.Columns.Count) return dataGridView.Columns[Num].HeaderText;
                else return "-1";
            }
            /// <summary>
            /// 取得指定行數名稱編號
            /// </summary>
            /// <param name="Num">引入:名稱</param>
            /// <returns>行編號</returns>
            public int FindColumnNum(string ColumnName)
            {
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    if (dataGridView.Columns[i].HeaderText == ColumnName) return i;
                }
                return -1;
            }
            /// <summary>
            /// 取得指定格內容
            /// </summary>
            /// <param name="ColumName">引入:行名稱</param>
            /// <returns>格內容</returns>
            public string GetSelectedCell(string ColumnName)
            {
                int index = this.FindColumnNum(ColumnName);
                for (int i = 0; i < this.dataGridView.Rows.Count; i++)
                {
                    if (this.dataGridView.Rows[i].Selected)
                    {
                        return (string)this.dataGridView[index, i].Value;                                                                          
                    }
                }
                return "-1";
            }
            /// <summary>
            /// 設定選擇列數
            /// </summary>
            /// <param name="index">引入:指定列編號</param>
            /// <returns>無</returns>
            public void SetSelectRow(int index)
            {
                dataGridView.Invoke(new Action(delegate
                {
                    dataGridView.Rows[index].Selected = true;
                }));
      
            }
            /// <summary>
            /// 取得選擇列數
            /// </summary>
            /// <returns>選擇列編號</returns>
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
            public object[] GetSelectRowValue()
            {
                int index = this.GetSelectRow();

                if (index != -1)
                {
                    return ModuleListCtl.ModuleList[index].GetAllValue();
                }
                return null;
            }
            /// <summary>
            /// 取得指定行所有內容
            /// </summary>
            /// <param name="ColumName">引入:行名稱</param>
            /// <returns>行所有內容</returns>
            public string[] GetAllColumnValue(string ColumnName)
            {
                List<string> str_array = new List<string>();
                int index = this.FindColumnNum(ColumnName);
                for (int i = 0; i < this.dataGridView.Rows.Count; i++)
                {
                    str_array.Add((string)this.dataGridView[index, i].Value);
                }
                return str_array.ToArray();
            }
            public void RefreshTable()
            {
                ModuleListCtl.RefreshModule();
            }
        }
        #endregion
    }
}
