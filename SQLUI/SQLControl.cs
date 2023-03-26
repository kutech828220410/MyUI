using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using Basic;
namespace SQLUI
{
    public class SQLControl
    {
        public delegate void PrcessReprot(int value , int max);
        MySqlConnectionStringBuilder _MySqlConnectionStringBuilder = null;
        public string TableName = "";
        private bool IsConnect = false;
        private string Server = "";
        private string Database = "";
        private string UserID = "";
        private string Password = "";
        private uint Port = 1;
        private MySqlSslMode SSLMode;
        private string SeverString = "";
        public enum OrderType
        {
            UpToLow = 0,
            LowToUp = 1,
            None = 2
        }
        private enum SerchType
        {
            NONE = 0,
            DEFULT = 1,
            BETWEEN = 2,
            IN = 3,
            LIKE = 4,        
        }
        public SQLControl(string Server, string Database, string TableName ,string UserID, string Password, uint Port, MySqlSslMode SSLMode)
        {
            this.TableName = TableName;
            this.Server = Server;
            this.Database = Database;
            this.UserID = UserID;
            this.Password = Password;
            this.Port = Port;
            this.SSLMode = SSLMode;
            _MySqlConnectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = Server,
                Database = Database,
                UserID = UserID,
                Password = Password,
                Port = Port,
                SslMode = SSLMode
            };
            SeverString += string.Format("server='{0}';", Server);
            SeverString += string.Format("database='{0}';", Database);
            SeverString += string.Format("user id='{0}';", UserID);
            SeverString += string.Format("password='{0}';", Password);
            SeverString += string.Format("Port='{0}';", Port);
            SeverString += string.Format("SSLMode='{0}';", SSLMode.ToString());
            TestConnection();
        }
        public SQLControl(string Server, string Database, string UserID, string Password, uint Port, MySqlSslMode SSLMode)
        {
            this.Server = Server;
            this.Database = Database;
            this.UserID = UserID;
            this.Password = Password;
            this.Port = Port;
            this.SSLMode = SSLMode;
            _MySqlConnectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = Server,
                Database =  Database,
                UserID =  UserID ,
                Password =  Password,
                Port = Port,
                SslMode = SSLMode
            };
            SeverString += string.Format("server='{0}';", Server);
            SeverString += string.Format("database='{0}';", Database);
            SeverString += string.Format("user id='{0}';", UserID);
            SeverString += string.Format("password='{0}';", Password);
            SeverString += string.Format("Port='{0}';", Port);
            SeverString += string.Format("SSLMode='{0}';", SSLMode.ToString());
            //TestConnection();
        }
        public SQLControl(string Server, string Database, string UserID, string Password, MySqlSslMode SSLMode)
        {
            this.Server = Server;
            this.Database = Database;
            this.UserID = UserID;
            this.Password = Password;
            this.SSLMode = SSLMode;
            _MySqlConnectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = @Server,
                Database = @Database,
                UserID = @UserID,
                Password = @Password,
                SslMode = SSLMode
            };
            TestConnection();
        }
        public SQLControl(string Server, string Database, string UserID, string Password, uint Port)
        {
            this.Server = Server;
            this.Database = Database;
            this.UserID = UserID;
            this.Password = Password;
            this.Port = Port;
            _MySqlConnectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = @Server,
                Database = @Database,
                UserID = @UserID,
                Password = @Password,
                Port = Port,
                SslMode = MySqlSslMode.None
            };
            TestConnection();
        }
        public SQLControl(string Server, string Database, string UserID, string Password)
        {
            this.Server = Server;
            this.Database = Database;
            this.UserID = UserID;
            this.Password = Password;
            _MySqlConnectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = @Server,
                Database = @Database,
                UserID = @UserID,
                Password = @Password,
                SslMode = MySqlSslMode.None
            };
            TestConnection();
        }
        public SQLControl()
        {

        }

        public void Set_Database(string Database)
        {
            this.Database = Database;
            MySqlConnectionStringBuilder _MySqlConnectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = this.Server,
                Database = this.Database,
                UserID = this.UserID,
                Password = this.Password,
                Port = this.Port,
                SslMode = this.SSLMode
            };
            string Command = string.Format("USE  {0};", this.Database);
            WtrteCommand(Command);
        }
        public void Set_Connection(string Server, string UserID, string Password, uint Port, MySqlSslMode SSLMode)
        {
            this.Server = Server;
            this.UserID = UserID;
            this.Password = Password;
            this.Port = Port;
            this.SSLMode = SSLMode;
            _MySqlConnectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = this.Server,
                Database = this.Database,
                UserID = this.UserID,
                Password = this.Password,
                Port = this.Port,
                SslMode = this.SSLMode
            };
            SeverString += string.Format("server='{0}';", Server);
            SeverString += string.Format("database='{0}';", Database);
            SeverString += string.Format("user id='{0}';", UserID);
            SeverString += string.Format("password='{0}';", Password);
            SeverString += string.Format("Port='{0}';", Port);
            SeverString += string.Format("SSLMode='{0}';", SSLMode.ToString());
            TestConnection();
        }
        public bool TestConnection()
        {
            bool Isopen = false;
            using (MySqlConnection _MySqlConnection = new MySqlConnection(@_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true" + ";Connection Timeout=3"))
            {
                this.OpenConnection(_MySqlConnection);
                Isopen = (_MySqlConnection.State == System.Data.ConnectionState.Open ? true : false);
                this.CloseConection(_MySqlConnection);
            }
         
            return Isopen;
        }

        public bool GetConcetStatu()
        {
            return IsConnect;
        }
        public string GetServer()
        {
            if (IsConnect) return this.Server;
            else return null;
        }
        public string GetDatabase()
        {
            if (IsConnect) return this.Database;
            else return null;
        }
        public string GetUserID()
        {
            if (IsConnect) return this.UserID;
            else return null;
        }
        public string GetPassword()
        {
            if (IsConnect) return this.Password;
            else return null;
        }
        public string GetPort()
        {
            if (IsConnect) return this.Port.ToString();
            else return null;
        }
        public string GetSSLMode()
        {
            if (IsConnect) return this.SSLMode.ToString();
            else return null;
        }

        public List<string> Get_All_DataBase_Name()
        {
            List<string> string_list = new List<string>();
            string Command = "show databases;";

            using (MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;"))
            {
                this.OpenConnection(_MySqlConnection);
                MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();

                _MySqlCommand.CommandText = Command;
                var reader = _MySqlCommand.ExecuteReader();
                int FieldCount = reader.FieldCount;
                while (reader.Read())
                {
                    string_list.Add(reader["DataBase"].ToString());
                }
                this.CloseConection(_MySqlConnection);
            }
            return string_list;
        }
        public void Create_DataBase(string Name)
        {
            this.Create_DataBase(Name, "utf8");
        }
        public void Create_DataBase(string Name, string Endcoding)
        {
            string Command = string.Format("create database  {0} DEFAULT CHARACTER SET {1};", Name , Endcoding);
            WtrteCommand(Command);
        }
        public void Drop_DataBase(string Name)
        {
            string Command = string.Format("drop database  {0};", Name);
            WtrteCommand(Command);
        }
        public void Import_DataBase(string databaseName, string fullfilePath, string root_password, string mysqlDumpPath)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            String command = "";
            //mysql dump 存放位置
            String _mysqlDumpPath = mysqlDumpPath;
            command = "cd " + _mysqlDumpPath;
            //執行command
            p.StandardInput.WriteLine(command);
            //要進行backup 的db名稱
            String dbName = databaseName;
            String outputPath = @fullfilePath;
            //創建存放資料夾
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            //注意 -p後緊接你的MYSQL密碼
            command = "mysqldump -u root -p" + root_password + " " + dbName + " --default-character-set=utf8" + " < " + outputPath + "\\" + dbName + ".sql";
            //執行command
            p.StandardInput.WriteLine(command);
            p.StandardInput.WriteLine("exit");
            p.Close();
        }
        public void Import_Table(string databaseName, string TableName, string fullfilePath, string root_password, string mysqlDumpPath)
        {
            if (TableName == null) TableName = this.TableName;
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            String command = "";
            //mysql dump 存放位置
            String _mysqlDumpPath = mysqlDumpPath;
            command = "cd " + _mysqlDumpPath;
            //執行command
            p.StandardInput.WriteLine(command);
            //要進行backup 的db名稱
            String dbName = databaseName;
            String outputPath = @fullfilePath;
            //創建存放資料夾
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            //注意 -p後緊接你的MYSQL密碼
            command = "mysqldump -u root -p" + root_password + " " + dbName + " " + TableName + " --default-character-set=utf8" + " < " + outputPath + "\\" + TableName + ".sql";
            //執行command
            p.StandardInput.WriteLine(command);
            p.StandardInput.WriteLine("exit");
            p.Close();
        }

        public void Export_DataBase(string databaseName, string fullfilePath, string root_password, string mysqlDumpPath)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            String command = "";
            //mysql dump 存放位置
            String _mysqlDumpPath = mysqlDumpPath;
            command = "cd " + _mysqlDumpPath;
            //執行command
            p.StandardInput.WriteLine(command);
            //要進行backup 的db名稱
            String dbName = databaseName;
            String outputPath = @fullfilePath;
            //創建存放資料夾
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            //注意 -p後緊接你的MYSQL密碼
            command = "mysqldump -u root -p" + root_password + " " + dbName + " --default-character-set=utf8" + " > " + outputPath + "\\" + dbName + ".sql";
            //執行command
            p.StandardInput.WriteLine(command);
            p.StandardInput.WriteLine("exit");
            p.Close();
        }
        public void Export_Table(string databaseName, string TableName, string fullfilePath, string root_password, string mysqlDumpPath)
        {
            if (TableName == null) TableName = this.TableName;
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            String command = "";
            //mysql dump 存放位置
            String _mysqlDumpPath = mysqlDumpPath;
            command = "cd " + _mysqlDumpPath;
            //執行command
            p.StandardInput.WriteLine(command);
            //要進行backup 的db名稱
            String dbName = databaseName;
            String outputPath = @fullfilePath;
            //創建存放資料夾
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            //注意 -p後緊接你的MYSQL密碼
            command = "mysqldump -u root -p" + root_password + " " + dbName + " " + TableName +  " --default-character-set=utf8" + " > " + outputPath + "\\" + TableName + ".sql";
            //執行command
            p.StandardInput.WriteLine(command);
            p.StandardInput.WriteLine("exit");
            p.Close();
        }
        public void Set_Table_Name(string Old_Name, string New_Name)
        {
            string Command = string.Format("USE   {0};", this.Database);
            WtrteCommand(Command);
            Command = string.Format("RENAME TABLE {0} TO {1};", Old_Name, New_Name);
            WtrteCommand(Command);
        }
        public void Set_Columm_Name(string TableName, string Old_Name, string New_Name, string valueType, string TypeLen)
        {
            if (TableName == null) TableName = this.TableName;
            if (TypeLen == "longblob") TypeLen = "";
            if (TypeLen != "") TypeLen = "(" + TypeLen + ")";
            string Command = string.Format("ALTER TABLE {0} CHANGE COLUMN {1} {2} {3}{4}", TableName, Old_Name, New_Name, valueType, TypeLen);
            WtrteCommand(Command);
        }
      
        public int Add_Column(string TableName, string ColumnName, string ColumnType)
        {
            if (TableName == null) TableName = this.TableName;
            string[] ColumnNames = this.GetAllColumn_Name(TableName);
            foreach(string value in ColumnNames)
            {
                if (value == ColumnName) return -1;
            }
  
            return this.Add_Column(TableName , ColumnName, ColumnType, ColumnNames[ColumnNames.Length - 1]);
        }
        public int Add_Column(string TableName, string ColumnName, string ColumnType, Table.IndexType indexType, string AfterColumnName)
        {
            if (TableName == null) TableName = this.TableName;
            string[] ColumnNames = this.GetAllColumn_Name(TableName);
            foreach (string value in ColumnNames)
            {
                if (value == ColumnName) return -1;
            }
            int temp = this.Add_Column(TableName, ColumnName, ColumnType, AfterColumnName);
            if (temp == 1)
            {
                if (indexType != Table.IndexType.None)
                {
                    this.Add_Index(TableName, ColumnName, indexType);
                }
            }
            return temp;
        }
        public int Add_Column(string TableName, string ColumnName, string ColumnType, int index)
        {
            if (TableName == null) TableName = this.TableName;
            string[] ColumnNames = this.GetAllColumn_Name(TableName);
            foreach (string value in ColumnNames)
            {
                if (value == ColumnName) return -1;
            }
            return this.Add_Column(TableName, ColumnName, ColumnType, ColumnNames[index]);
        }
        public int Add_Column(string TableName, string ColumnName, string ColumnType, Table.IndexType indexType, int index)
        {
            if (TableName == null) TableName = this.TableName;
            string[] ColumnNames = this.GetAllColumn_Name(TableName);
            foreach (string value in ColumnNames)
            {
                if (value == ColumnName) return -1;
            }
            int temp = this.Add_Column(TableName, ColumnName, ColumnType, ColumnNames[index]);
            if (temp == 1)
            {
                if(indexType != Table.IndexType.None)
                {
                    this.Add_Index(TableName, ColumnName, indexType);
                }
            }
            return temp;
        }
        public int Add_Column(string TableName, string ColumnName, string ColumnType, string AfterColumnName)
        {
            if (TableName == null) TableName = this.TableName;
            string Command = $"ALTER TABLE {this.Database}.{TableName} ADD COLUMN {ColumnName} {ColumnType} NULL AFTER {AfterColumnName}";
            WtrteCommand(Command);
            return 1;
        }
        public void Drop_Column(string TableName, string ColumnName)
        {
            if (TableName == null) TableName = this.TableName;
            string Command = string.Format(" alter table {0} drop {1};", TableName, ColumnName);
            WtrteCommand(Command);
        }
        public void Modify_Afert_Column(string TableName, string Mod_ColumnName, string Mod_ColumnType, string Mod_DataLen, string Target_ColumnName)
        {
            if (TableName == null) TableName = this.TableName;
            if (Mod_ColumnType == "longblob") Mod_DataLen = "";
            if (Mod_DataLen != "") Mod_DataLen = "(" + Mod_DataLen + ")";
            string Command = string.Format("ALTER TABLE {0} MODIFY {1} {2}{3} AFTER {4};", TableName, Mod_ColumnName, Mod_ColumnType, Mod_DataLen, Target_ColumnName);
            WtrteCommand(Command);
        }
        public void Modify_First_Column(string TableName, string Mod_ColumnName, string Mod_ColumnType, string Mod_DataLen)
        {
            if (TableName == null) TableName = this.TableName;
            if (Mod_ColumnType == "longblob") Mod_DataLen = "";
            if (Mod_DataLen != "") Mod_DataLen = "(" + Mod_DataLen + ")";
            string Command = string.Format("ALTER TABLE {0} MODIFY {1} {2}{3} FIRST;", TableName, Mod_ColumnName, Mod_ColumnType, Mod_DataLen);
            WtrteCommand(Command);
        }
    

        public bool GetTableLock(string TableName)
        {
            List<string> list_database = new List<string>();
            List<string> list_table = new List<string>();
            string Command = $"show OPEN TABLES where (In_use > 0);";
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                list_database.Add(reader["Database"] as string);
                list_table.Add(reader["table"] as string);
            }
            this.CloseConection(_MySqlConnection);

           
            if (list_database.Count != list_table.Count) return false;
            for(int i = 0; i < list_database.Count; i++)
            {
                if(list_database[i].ToLower() == this.Database.ToLower())
                {
                    if (list_table[i].ToLower() == TableName.ToLower())
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public void LockTable(string TableName)
        {
            if (TableName == null) TableName = this.TableName;
            string Command = $"lock table {TableName} write;";
            WtrteCommand(Command);
        }
        public void UnLockTable()
        {
            string Command = $"unlock tables;";
            //string Command = $"flush tables with read lock;"; 
            WtrteCommand(Command);
            
        }
        public int DropTable(string TableName)
        {
            if (TableName == null) TableName = this.TableName;
            string Command;
            Command = @"DROP TABLE IF EXISTS ";
            Command += TableName;
            Command += @";";
            WtrteCommand(Command);
            return 1;
        }
        public int CreatTable(string TableName, string ColumnNames)
        {
            this.CreatTable(TableName, ColumnNames, "utf8");
            return 1;
        }
        public int CreatTable(string TableName , string ColumnNames ,string Endcoding)
        {
            if (TableName == null) TableName = this.TableName;
            string Command;
            this.DropTable(TableName);
            Command = @"CREATE TABLE ";
            Command += TableName;
            Command += @" (" + ColumnNames + ")ENGINE=InnoDB DEFAULT CHARSET=" + Endcoding + ";";
            WtrteCommand(Command);

            return 1;
        }
        public int CreatTable(Table _Table)
        {
            string Command;
            this.DropTable(_Table.GetTableName());
            Command = @"CREATE TABLE ";
            Command += @_Table.GetTableName();
            Command += @" (" + _Table.GetAllColumn_Name() + ");";
            WtrteCommand(Command);

            int i = 0;
            foreach (Table.ColumnElement temp in _Table.GetColumnList())
            {
                if (temp.TypeName == Table.GetTypeName(Table.StringType.TINYTEXT)) continue;
                if (temp.TypeName == Table.GetTypeName(Table.StringType.TEXT)) continue;
                if (temp.TypeName == Table.GetTypeName(Table.StringType.LONGTEXT)) continue;
                if (temp.TypeName == Table.GetTypeName(Table.StringType.LONGBLOB)) continue;
                if (temp.TypeName == Table.GetTypeName(Table.StringType.MEDIUMTEXT)) continue;
                if (temp.IndexType == Table.IndexType.INDEX)
                {              
                    this.Add_Index(_Table.GetTableName(), _Table.GetColumnName(i), false);
                }
                else if (temp.IndexType == Table.IndexType.UNIQUE)
                {
                    this.Add_Index(_Table.GetTableName(), _Table.GetColumnName(i), true);
                }
                else if (temp.IndexType == Table.IndexType.PRIMARY)
                {
                    this.Add_primary_key(_Table.GetTableName(), _Table.GetColumnName(i));
                }
                i++;
            }

            return 1;
        }
  
        public string[] GetTables()
        {
            List<string> string_list = new List<string>();
            string Command;
            Command = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='" + Database + "'";

            using (MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;"))
            {
                this.OpenConnection(_MySqlConnection);
                MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();

                _MySqlCommand.CommandText = Command;
                var reader = _MySqlCommand.ExecuteReader();
                int FieldCount = reader.FieldCount;
                while (reader.Read())
                {
                    string_list.Add(reader["TABLE_NAME"].ToString());
                }
                this.CloseConection(_MySqlConnection);
            }     
            return string_list.ToArray();
        }

        public bool IsTableCreat()
        {
            return this.IsTableCreat(null);
        }
        public bool IsTableCreat(string TableName)
        {
            if (TableName == null) TableName = this.TableName;
            string[] string_array = GetTables();
            foreach(string str_temp in string_array)
            {
                if (str_temp.ToLower() == TableName.ToLower()) return true;
            }
            return false;
        }

        public int AddRow(string TableName, object[] Value)
        {
            if (TableName == null) TableName = this.TableName;
            object[] obj_AllColumnName = GetAllColumn_Name(TableName);
            if (Value.Length > 0 && Value.Length <= obj_AllColumnName.Length)
            {
                string Command;
                List<string> Name = new List<string>();
                List<object[]> temp = new List<object[]>();
                Command = @"INSERT INTO ";
                Command += TableName;
                Command += " (";
                for (int i = 0; i < Value.Length; i++)
                {
                    Command += (string)obj_AllColumnName[i];
                    Command += ",";
                }
                Command = Command.Remove(Command.Length - 1);
                Command += " )";
                Command += " VALUES";
                Command += " (";
                for (int i = 0; i < Value.Length; i++)
                {
                    Name.Add("@" + (string)obj_AllColumnName[i] + "1");
                    Command += Name[i];
                    Command += ",";
                }
                Command = Command.Remove(Command.Length - 1);
                Command += ");";
                for (int i = 0; i < Value.Length; i++)
                {
                    temp.Add(new object[] { Name[i], Value[i] });
                }
                WtrteCommand(Command, temp.ToArray());
            }

            return 1;
        }
        //public int AddRows(string TableName, List<object[]> Value)
        //{
        //    return AddRows(TableName, Value.ToArray());
        //}
        public int AddRows(string TableName, List<object[]> Value)
        {
            if (TableName == null) TableName = this.TableName;
            object[] obj_AllColumnName = GetAllColumn_Name(TableName);
            if (Value.Count > 0)
            {
                if (Value[0].Length > 0 && Value[0].Length <= obj_AllColumnName.Length)
                {
                    List<string> list_command = new List<string>();
                    MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
                    this.OpenConnection(_MySqlConnection);
                    MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();

                    foreach (object[] value in Value)
                    {
                        _MySqlCommand.Parameters.Clear();
                        string Command = "";
                        string text = "";
                        Command = $@"INSERT INTO {TableName} VALUES (";
                        for (int k = 0; k < value.Length; k++)
                        {
                            text = $"@{obj_AllColumnName[k]}";
                            _MySqlCommand.Parameters.AddWithValue((string)obj_AllColumnName[k], value[k]);
                            //text = value[k].ObjectToString();
                            //text = text.Replace("'", @"\'");
                            Command += $"{text}";
                            if (k != value.Length - 1) Command += ",";
                        }
                        Command += ");";
                        _MySqlCommand.CommandText = Command;
                        _MySqlCommand.ExecuteNonQuery();
                    }

                    this.CloseConection(_MySqlConnection);
                    _MySqlConnection.Dispose();
                    _MySqlCommand.Dispose();
                    
                }
            }
           
            return 1;
        }
 
        public int GetColumnCount(string TableName)
        {
            if (TableName == null) TableName = this.TableName;
            List<object> obj_temp_array = new List<object>();
            string Command;
            Command = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME =" + "'";
            Command += TableName + "'";
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int Count = 0;
            while (reader.Read())
            {
                Count++;
            }
            this.CloseConection(_MySqlConnection);
            return Count;
        }
        public int GetRowsCount(string TableName)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetCOUNT(TableName, (string)AllColumName[0], false);
        }
        public object[] GetColumnValue(string TableName, uint Columnindex)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetColumnValue(TableName, (string)AllColumName[Columnindex]);
        }
        public object[] GetColumnValue(string TableName, string ColumnName)
        {
            if (TableName == null) TableName = this.TableName;
            List<object> obj_temp = new List<object>();

            string Command;
            Command = "SELECT ";
            Command += ColumnName;
            Command += " FROM ";
            Command += TableName;
            Command += ";";

            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                obj_temp.Add(reader[ColumnName]);
            }
            this.CloseConection(_MySqlConnection);
            
            return obj_temp.ToArray();
        }

        public void SetAllColumn_Endcoding(string TableName, string Endcoding)
        {
            if (TableName == null) TableName = this.TableName;
            string Command = string.Format("USE   {0};", this.Database);
            WtrteCommand(Command);
            Command = string.Format("ALTER TABLE {0} CONVERT TO CHARACTER SET {1};", TableName, Endcoding);
            WtrteCommand(Command);
        }

        public string GetColumnName(string TableName, uint Columnindex)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return (string)AllColumName[Columnindex];
        }
        public string[] GetAllColumn_Name(string TableName)
        {
            if (TableName == null) TableName = this.TableName;
            List<object> obj_temp_array = new List<object>();
            List<string> list_str = new List<string>();
            string Command;

            Command = string.Format("SELECT column_name FROM information_schema.columns WHERE table_schema = '{0}' AND table_name = '{1}'",this.Database, TableName);
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                obj_temp_array.Add(reader["COLUMN_NAME"]);
            }
            this.CloseConection(_MySqlConnection);

            foreach (object value in obj_temp_array)
            {
                list_str.Add((string)value);
            }
            return list_str.ToArray();
        }
        public string[] GetAllColumn_DataType(string TableName)
        {
            if (TableName == null) TableName = this.TableName;
            List<object> obj_temp_array = new List<object>();
            List<string> list_str = new List<string>();
            string Command;
            //Command = "SELECT COLUMN_KEY,COLUMN_NAME, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME =" + "'";
            //Command += TableName + "'";
            Command = string.Format("SELECT DATA_TYPE FROM information_schema.columns WHERE table_schema = '{0}' AND table_name = '{1}'", this.Database, TableName);
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                obj_temp_array.Add(reader["DATA_TYPE"]);
            }
            this.CloseConection(_MySqlConnection);

            foreach (object value in obj_temp_array)
            {
                list_str.Add((string)value);
            }
            return list_str.ToArray();
        }
        public string[] GetAllColumn_KEY(string TableName)
        {
            if (TableName == null) TableName = this.TableName;
            List<object> obj_temp_array = new List<object>();
            List<string> list_str = new List<string>();
            string Command;
            //Command = "SELECT COLUMN_KEY,COLUMN_NAME, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME =" + "'";
            //Command += TableName + "'";
            Command = string.Format("SELECT COLUMN_KEY FROM information_schema.columns WHERE table_schema = '{0}' AND table_name = '{1}'", this.Database, TableName);
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                obj_temp_array.Add(reader["COLUMN_KEY"]);
            }
            this.CloseConection(_MySqlConnection);

            foreach (object value in obj_temp_array)
            {
                list_str.Add((string)value);
            }
            return list_str.ToArray();
        }
        public string[] GetAllColumn_Max_Length(string TableName)
        {
            if (TableName == null) TableName = this.TableName;
            List<object> obj_temp_array = new List<object>();
            List<string> list_str = new List<string>();
            string Command;
            //Command = "SELECT COLUMN_KEY,COLUMN_NAME, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME =" + "'";
            //Command += TableName + "'";
            Command = string.Format("SELECT CHARACTER_MAXIMUM_LENGTH FROM information_schema.columns WHERE table_schema = '{0}' AND table_name = '{1}'", this.Database, TableName);
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                obj_temp_array.Add(reader["CHARACTER_MAXIMUM_LENGTH"]);
            }
            this.CloseConection(_MySqlConnection);

            foreach (object value in obj_temp_array)
            {
                list_str.Add(value.ToString());
            }
            return list_str.ToArray();
        }
        public string[] GetAllColumn_Endcoding(string TableName)
        {
            if (TableName == null) TableName = this.TableName;
            List<string> list_endcoding = new List<string>();
            string Command = string.Format("SELECT table_schema, table_name, character_set_name FROM information_schema.columns WHERE table_schema = '{0}' AND table_name = '{1}'; ", this.Database, TableName);
            using (MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;"))
            {
                this.OpenConnection(_MySqlConnection);
                MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();

                _MySqlCommand.CommandText = Command;
                var reader = _MySqlCommand.ExecuteReader();
                int FieldCount = reader.FieldCount;
                while (reader.Read())
                {
                    list_endcoding.Add(reader["character_set_name"].ToString());
                }
                this.CloseConection(_MySqlConnection);
            }

            return list_endcoding.ToArray();
        }
     
        #region Method_Index
        public void Modify_Index(string TableName, string ColumnName, Table.IndexType IndexType)
        {
            this.Modify_Index(TableName, new string[] { ColumnName }, IndexType);
        }
        public void Modify_Index(string TableName, string[] ColumnNames, Table.IndexType IndexType)
        {
            if (IndexType == Table.IndexType.PRIMARY)
            {
                if (ColumnNames.Length != 1) return;
                //if (this.CheckPrimaryKey(TableName))
                //{
                //    this.Drop_primary_key(TableName);
                //}
                this.Add_primary_key(TableName, ColumnNames[0]);
            }
            //if (this.CheckIndex(TableName, ColumnNames))
            //{
            //    this.Drop_Index(TableName, ColumnNames);
            //}

            if(IndexType == Table.IndexType.INDEX)
            {
                this.Add_Index(TableName, ColumnNames, false);
            }
            else if (IndexType == Table.IndexType.UNIQUE)
            {
                this.Add_Index(TableName, ColumnNames, true);
            }
      
        }
        public void Add_Index(string TableName, string ColumnName, Table.IndexType indexType)
        {
            if (indexType == Table.IndexType.INDEX)
            {
                this.Add_Index(TableName, ColumnName, false);
            }
            else if (indexType == Table.IndexType.UNIQUE)
            {
                this.Add_Index(TableName, ColumnName, true);
            }
            else if (indexType == Table.IndexType.PRIMARY)
            {
                this.Add_primary_key(TableName, ColumnName);
            }
        }
        public void Add_Index(string TableName, string[] ColumnNames, bool IsUnique)
        {
            string Command = string.Format("USE  {0};", this.Database);
            WtrteCommand(Command);

            string IndexName = this.GetIndexName(ColumnNames);
            this.Drop_Index(TableName, ColumnNames);
            Command = "CREATE ";
            if (IsUnique) Command += " UNIQUE ";
            Command += " INDEX ";
            Command += IndexName;
            Command += " ON ";
            Command += TableName;
            Command += " (";
            for (int i = 0; i < ColumnNames.Length; i++)
            {
                Command += ColumnNames[i] + ",";
            }
            Command = Command.Remove(Command.Length - 1);
            Command += ");";

            WtrteCommand(Command);
        }
        public void Add_Index(string TableName, string ColumnName, bool IsUnique)
        {
            this.Add_Index(TableName, new string[] { ColumnName }, IsUnique);
        }
        public void Drop_Index(string TableName, string[] ColumnNames)
        {
            string Command = string.Format("USE  {0};", this.Database);
            WtrteCommand(Command);
            string IndexName = this.GetIndexName(ColumnNames);
            if (this.CheckIndex(TableName, ColumnNames))
            {
                Command = "DROP ";
                Command += " INDEX ";
                Command += IndexName;
                Command += " ON ";
                Command += TableName;
                WtrteCommand(Command);
            }

        }
        public void Drop_Index(string TableName, string ColumnName)
        {
            this.Drop_Index(TableName, new string[] { ColumnName });
        }
        public void Add_primary_key(string TableName, string ColumnName)
        {
            string Command = string.Format("USE  {0};", this.Database);
            WtrteCommand(Command);

            this.Drop_primary_key(TableName);
            Command = @"ALTER TABLE ";
            Command += TableName;
            Command += " ADD primary key ";
            Command += " (";
            Command += ColumnName;
            Command += ");";
            WtrteCommand(Command);
        }
        public void Drop_primary_key(string TableName)
        {
            if (this.CheckPrimaryKey(TableName))
            {
                string Command = string.Format("USE  {0};", this.Database);
                WtrteCommand(Command);

                Command = @"ALTER TABLE ";
                Command += TableName;
                Command += " DROP primary key";
                WtrteCommand(Command);
            }
        }
        public string[] Get_Index_Key_Name(string TableName)
        {
            string Command = string.Format("USE  {0};", this.Database);
            WtrteCommand(Command);

            List<string> string_out_list = new List<string>();
            Command = "SHOW INDEX FROM " + TableName;
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                string_out_list.Add((string)reader["Key_name"]);
            }
            this.CloseConection(_MySqlConnection);
            return string_out_list.ToArray();
        }
        public string[] Get_Index_Column_Name(string TableName)
        {
            string Command = string.Format("USE  {0};", this.Database);
            WtrteCommand(Command);

            List<string> string_out_list = new List<string>();
            Command = "SHOW INDEX FROM " + TableName;
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                string_out_list.Add((string)reader["Column_name"]);
            }
            this.CloseConection(_MySqlConnection);
            return string_out_list.ToArray();
        }
        public string[] Get_Index_Non_Unique(string TableName)
        {
            string Command = string.Format("USE  {0};", this.Database);
            WtrteCommand(Command);

            List<string> string_out_list = new List<string>();
            Command = "SHOW INDEX FROM " + TableName;
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                string_out_list.Add(reader["Non_unique"].ToString());
            }
            this.CloseConection(_MySqlConnection);
            return string_out_list.ToArray();
        }
        private bool CheckPrimaryKey(string TableName)
        {
            string[] string_index_array;
            string_index_array = this.Get_Index_Key_Name(TableName);
            for (int i = 0; i < string_index_array.Length; i++)
            {
                if (string_index_array[i] == "PRIMARY") return true;
            }
            return false;
        }
        private bool CheckIndex(string TableName, string[] ColumnNames)
        {
            string[] string_index_array;
            string IndexName = GetIndexName(ColumnNames);
            string_index_array = this.Get_Index_Key_Name(TableName);
            for (int i = 0; i < string_index_array.Length; i++)
            {
                if (string_index_array[i] == IndexName) return true;
            }
            return false;
        }
        private string GetIndexName(string[] ColumnNames)
        {
            string string_out = "IDX_";
            for (int i = 0; i < ColumnNames.Length; i++)
            {
                string_out += (ColumnNames[i] + "_");
            }
            string_out = string_out.Remove(string_out.Length - 1);
            return string_out;
        }
        #endregion

        #region Method_GetColumnValues
        public List<object[]> GetColumnValues(string TableName, string ColumnName, bool Distinct)
        {
            List<object[]> obj_temp_array = new List<object[]>();
            List<object> obj_temp = new List<object>();
            string Command;
            Command = "SELECT ";
            if (Distinct) Command += " distinct ";
            Command += ColumnName;
            Command += " FROM ";
            Command += TableName;

            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                obj_temp.Clear();
                for (int i = 0; i < FieldCount; i++)
                {
                    obj_temp.Add(reader[ColumnName]);
                }
                obj_temp_array.Add(obj_temp.ToArray());
            }
            this.CloseConection(_MySqlConnection);

            return obj_temp_array;

        }
        #endregion
        #region Method_GetRows
        public List<object[]> GetAllRows(string TableName)
        {
            return GetRows(TableName, new string[] { }, new string[] { }, SerchType.NONE, "", 0, -1, "", OrderType.None);       
        }
        public List<object[]> GetAllRows(string TableName, int OrderColumnindex, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { }, new string[] { }, SerchType.NONE, "", 0, -1, (string)AllColumName[OrderColumnindex], _OrderType); 
        }
        public List<object[]> GetAllRows(string TableName, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            return GetRows(TableName, new string[] { }, new string[] { }, SerchType.NONE, "", 0, -1, OrderColumnName, _OrderType); 
        }

        public List<object[]> GetRowsOfRange(string TableName, uint StrintIndex, uint NumOfData)
        {
            return GetRows(TableName, new string[] { }, new string[] { }, SerchType.NONE, "", StrintIndex, (int)NumOfData, "", OrderType.None);
        }
        public List<object[]> GetRowsOfRange(string TableName, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { }, new string[] { }, SerchType.NONE, "", StrintIndex, (int)NumOfData, (string)AllColumName[OrderColumnindex], _OrderType); 
        }
        public List<object[]> GetRowsOfRange(string TableName, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            return GetRows(TableName, new string[] { }, new string[] { }, SerchType.NONE, "", StrintIndex, (int)NumOfData, OrderColumnName, _OrderType);   
        }

        public List<object[]> GetRowsByDefult(string TableName, int serchColumnindex, string serchValue)
        {
            if (TableName == null) TableName = this.TableName;
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { serchValue }, SerchType.DEFULT, "", 0, -1, "", OrderType.None); 
        }
        public List<object[]> GetRowsByDefult(string TableName, string serchColumnName, string serchValue)
        {
            if (TableName.StringIsEmpty()) TableName = this.TableName;
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { serchColumnName }, new string[] { serchValue }, SerchType.DEFULT, "", 0, -1, "", OrderType.None);
        }
        public List<object[]> GetRowsByDefult(string TableName, string serchColumnName, string serchValue, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            if (TableName == null) TableName = this.TableName;
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { serchColumnName }, new string[] { serchValue }, SerchType.DEFULT, "", 0, -1, OrderColumnName, _OrderType);
        }
        public List<object[]> GetRowsByDefult(string TableName, int serchColumnindex, string serchValue, uint StrintIndex, uint NumOfData)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { serchValue }, SerchType.DEFULT, "", StrintIndex, (int)NumOfData, "", OrderType.None);
        }
        public List<object[]> GetRowsByDefult(string TableName, int serchColumnindex, string serchValue, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { serchValue }, SerchType.DEFULT, "", StrintIndex, (int)NumOfData, (string)AllColumName[OrderColumnindex], _OrderType);
        }
        public List<object[]> GetRowsByDefult(string TableName, int serchColumnindex, string serchValue, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { serchValue }, SerchType.DEFULT, "", StrintIndex, (int)NumOfData, OrderColumnName, _OrderType);
        }
        public List<object[]> GetRowsByDefult(string TableName, string serchColumnName, string serchValue, uint StrintIndex, uint NumOfData)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { serchColumnName }, new string[] { serchValue }, SerchType.DEFULT, "", StrintIndex, (int)NumOfData, "", OrderType.None);
        }
        public List<object[]> GetRowsByDefult(string TableName, string serchColumnName, string serchValue, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { serchColumnName }, new string[] { serchValue }, SerchType.DEFULT, "", StrintIndex, (int)NumOfData, (string)AllColumName[OrderColumnindex], _OrderType);
        }
        public List<object[]> GetRowsByDefult(string TableName, string serchColumnName, string serchValue, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            return GetRows(TableName, new string[] { serchColumnName }, new string[] { serchValue }, SerchType.DEFULT, "", StrintIndex, (int)NumOfData, OrderColumnName, _OrderType);
        }
        public List<object[]> GetRowsByDefult(string TableName, string[] serchColumnName, string[] serchValue)
        {
            return GetRows(TableName, serchColumnName, serchValue, SerchType.DEFULT, "", 0, -1, "", OrderType.None);
        }
        public List<object[]> GetRowsByDefult(string TableName, string[] serchColumnName, string[] serchValue, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            return GetRows(TableName, serchColumnName, serchValue, SerchType.DEFULT, "", 0, -1, OrderColumnName, _OrderType);
        }
        public List<object[]> GetRowsByDefult(string TableName, string[] serchColumnName, string[] serchValue, uint StrintIndex, uint NumOfData)
        {
            return GetRows(TableName, serchColumnName, serchValue, SerchType.DEFULT, "", StrintIndex, (int)NumOfData, "", OrderType.None);
        }
        public List<object[]> GetRowsByDefult(string TableName, string[] serchColumnName, string[] serchValue, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, serchColumnName, serchValue, SerchType.DEFULT, "", StrintIndex, (int)NumOfData, (string)AllColumName[OrderColumnindex], _OrderType);
        }
        public List<object[]> GetRowsByDefult(string TableName, string[] serchColumnName, string[] serchValue, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            return GetRows(TableName, serchColumnName, serchValue, SerchType.DEFULT, "", StrintIndex, (int)NumOfData, OrderColumnName, _OrderType);
        }

        public List<object[]> GetRowsByLike(string TableName, int serchColumnindex, string LikeString)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { }, SerchType.LIKE, LikeString, 0, -1, "", OrderType.None);
        }
        public List<object[]> GetRowsByLike(string TableName, int serchColumnindex, string LikeString, uint StrintIndex, uint NumOfData)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { }, SerchType.LIKE, LikeString, StrintIndex, (int)NumOfData, "", OrderType.None);
        }
        public List<object[]> GetRowsByLike(string TableName, int serchColumnindex, string LikeString, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { }, SerchType.LIKE, LikeString, StrintIndex, (int)NumOfData, (string)AllColumName[OrderColumnindex], _OrderType);
        }
        public List<object[]> GetRowsByLike(string TableName, int serchColumnindex, string LikeString, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { }, SerchType.LIKE, LikeString, StrintIndex, (int)NumOfData, OrderColumnName, _OrderType);
        }
        public List<object[]> GetRowsByLike(string TableName, string serchColumnName, string LikeString)
        {
            return GetRows(TableName, new string[] { serchColumnName }, new string[] { }, SerchType.LIKE, LikeString, 0, -1, "", OrderType.None);
        }
        public List<object[]> GetRowsByLike(string TableName, string serchColumnName, string LikeString, uint StrintIndex, uint NumOfData)
        {
            return GetRows(TableName, new string[] { serchColumnName }, new string[] { }, SerchType.LIKE, LikeString, StrintIndex, (int)NumOfData, "", OrderType.None); 
        }
        public List<object[]> GetRowsByLike(string TableName, string serchColumnName, string LikeString, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { serchColumnName }, new string[] { }, SerchType.LIKE, LikeString, StrintIndex, (int)NumOfData, (string)AllColumName[OrderColumnindex], _OrderType);
        }
        public List<object[]> GetRowsByLike(string TableName, string serchColumnName, string LikeString, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            return GetRows(TableName, new string[] { serchColumnName }, new string[] { }, SerchType.LIKE, LikeString, StrintIndex, (int)NumOfData, OrderColumnName, _OrderType);
        }

        public List<object[]> GetRowsByBetween(string TableName, int serchColumnindex, string brtween_value1, string brtween_value2)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "", 0, -1, "", OrderType.None);
        }
        public List<object[]> GetRowsByBetween(string TableName, int serchColumnindex, string brtween_value1, string brtween_value2, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "", 0, -1, OrderColumnName, _OrderType);
        }
        public List<object[]> GetRowsByBetween(string TableName, int serchColumnindex, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "", StrintIndex, (int)NumOfData, "", OrderType.None);
        }
        public List<object[]> GetRowsByBetween(string TableName, int serchColumnindex, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "", StrintIndex, (int)NumOfData, (string)AllColumName[OrderColumnindex], _OrderType);
        }
        public List<object[]> GetRowsByBetween(string TableName, int serchColumnindex, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "", StrintIndex, (int)NumOfData, OrderColumnName, _OrderType);
        }
        public List<object[]> GetRowsByBetween(string TableName, string serchColumnName, string brtween_value1, string brtween_value2)
        {
            return GetRows(TableName, new string[] { serchColumnName }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "", 0, -1, "", OrderType.None);
        }
        public List<object[]> GetRowsByBetween(string TableName, string serchColumnName, string brtween_value1, string brtween_value2, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            return GetRows(TableName, new string[] { serchColumnName }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "", 0, -1, OrderColumnName, _OrderType);
        }
        public List<object[]> GetRowsByBetween(string TableName, string serchColumnName, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData)
        {
            return GetRows(TableName, new string[] { serchColumnName }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "", StrintIndex, (int)NumOfData, "", OrderType.None);
        }
        public List<object[]> GetRowsByBetween(string TableName, string serchColumnName, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { serchColumnName }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "", StrintIndex, (int)NumOfData, (string)AllColumName[OrderColumnindex], _OrderType);
        }
        public List<object[]> GetRowsByBetween(string TableName, string serchColumnName, string brtween_value1, string brtween_value2, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            return GetRows(TableName, new string[] { serchColumnName }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "", StrintIndex, (int)NumOfData, OrderColumnName, _OrderType);
        }

        public List<object[]> GetRowsByIn(string TableName, int serchColumnindex, string[] IN_value)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, IN_value, SerchType.IN, "", 0, -1, "", OrderType.None);
        }
        public List<object[]> GetRowsByIn(string TableName, int serchColumnindex, string[] IN_value, uint StrintIndex, uint NumOfData)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, IN_value, SerchType.IN, "", StrintIndex, (int)NumOfData, "", OrderType.None);
        }
        public List<object[]> GetRowsByIn(string TableName, int serchColumnindex, string[] IN_value, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, IN_value, SerchType.IN, "", StrintIndex, (int)NumOfData, (string)AllColumName[OrderColumnindex], _OrderType);
        }
        public List<object[]> GetRowsByIn(string TableName, int serchColumnindex, string[] IN_value, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { (string)AllColumName[serchColumnindex] }, IN_value, SerchType.IN, "", StrintIndex, (int)NumOfData, OrderColumnName, _OrderType);
        }
        public List<object[]> GetRowsByIn(string TableName, string serchColumnName, string[] IN_value)
        {
            return GetRows(TableName, new string[] { serchColumnName }, IN_value, SerchType.IN, "", 0, -1, "", OrderType.None);
        }
        public List<object[]> GetRowsByIn(string TableName, string serchColumnName, string[] IN_value, uint StrintIndex, uint NumOfData)
        {
            return GetRows(TableName, new string[] { serchColumnName }, IN_value, SerchType.IN, "", StrintIndex, (int)NumOfData, "", OrderType.None);
        }
        public List<object[]> GetRowsByIn(string TableName, string serchColumnName, string[] IN_value, uint StrintIndex, uint NumOfData, int OrderColumnindex, SQLControl.OrderType _OrderType)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return GetRows(TableName, new string[] { serchColumnName }, IN_value, SerchType.IN, "", StrintIndex, (int)NumOfData, (string)AllColumName[OrderColumnindex], _OrderType);
        }
        public List<object[]> GetRowsByIn(string TableName, string serchColumnName, string[] IN_value, uint StrintIndex, uint NumOfData, string OrderColumnName, SQLControl.OrderType _OrderType)
        {
            return GetRows(TableName, new string[] { serchColumnName }, IN_value, SerchType.IN, "", StrintIndex, (int)NumOfData, OrderColumnName, _OrderType);
        }

        private List<object[]> GetRows(string TableName, string[] serchColumnName, string[] serchValue, SerchType _SerchType, string LikeString, uint StrintIndex, int NumOfData, string ColumnName, SQLControl.OrderType _OrderType)
        {
            if (TableName == null) TableName = this.TableName;
            List<object[]> obj_temp_array = new List<object[]>();
            List<object> obj_temp = new List<object>();
            object[] AllColumnName = GetAllColumn_Name(TableName);
            string Command;
            Command = "SELECT ";
            Command += "*";
            Command += " FROM ";
            Command += TableName;

            if (_SerchType == SerchType.BETWEEN) Command += this.Get_WHERE_Command(serchColumnName, serchValue, _SerchType, LikeString);  
            else Command += this.Get_WHERE_Command(serchColumnName, _SerchType, LikeString);       

            if (_OrderType != OrderType.None && ColumnName != "")
            {
                Command += this.Order(ColumnName, _OrderType);
            }

            if (NumOfData > 0)
            {
                Command += " LIMIT " + StrintIndex.ToString() + "," + NumOfData.ToString() + " ";
            }


            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            _MySqlCommand.Parameters.Clear();

            if (_SerchType != SerchType.LIKE)
            {
                List<string> Name_temp = new List<string>();
                List<object[]> temp = new List<object[]>();
                for (int i = 0; i < serchColumnName.Length; i++)
                {
                    _MySqlCommand.Parameters.AddWithValue((string)(serchColumnName[i] + i.ToString()), serchValue[i]);
                }
            }
        

            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            object value;
            while (reader.Read())
            {
                obj_temp.Clear();
                for (int i = 0; i < FieldCount; i++)
                {
                    value = reader[(string)AllColumnName[i]];
                    //if (value is System.DBNull) value = "";
                    obj_temp.Add(value);
                }


                obj_temp_array.Add(obj_temp.ToArray());
            }
            this.CloseConection(_MySqlConnection);

            return obj_temp_array;
        }
        #endregion
        #region Method_Update
        public int UpdateByDefult(string TableName, int serchColumnindex, string serchValue, object[] Value)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return this.Update(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { serchValue }, SerchType.DEFULT, "", Value);
        }
        public int UpdateByDefult(string TableName, string serchColumnName, string serchValue, object[] Value)
        {
            return this.Update(TableName, new string[] { serchColumnName }, new string[] { serchValue }, SerchType.DEFULT, "", Value);
        }
        public int UpdateByDefult(string TableName, string[] serchColumnName, string[] serchValue, object[] Value)
        {
            return Update(TableName, serchColumnName, serchValue, SerchType.DEFULT, "", Value);
        }

        public int UpdateByLike(string TableName, int serchColumnindex, string LikeString, object[] Value)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return this.Update(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { }, SerchType.LIKE, LikeString, Value);
        }
        public int UpdateByLike(string TableName, string serchColumnName, string LikeString, object[] Value)
        {
            return this.Update(TableName, new string[] { serchColumnName }, new string[] { }, SerchType.LIKE, LikeString, Value);
        }

        public int UpdateByBetween(string TableName, int serchColumnindex, string brtween_value1, string brtween_value2, object[] Value)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return this.Update(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "", Value);
        }
        public int UpdateByBetween(string TableName, string serchColumnName, string brtween_value1, string brtween_value2, object[] Value)
        {
            return this.Update(TableName, new string[] { serchColumnName }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "", Value);
        }

        public int UpdateByIn(string TableName, int serchColumnindex, string IN_value, object[] Value)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return this.Update(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { IN_value }, SerchType.IN, "", Value);
        }
        public int UpdateByIn(string TableName, string serchColumnName, string IN_value, object[] Value)
        {
            return this.Update(TableName, new string[] { serchColumnName }, new string[] { IN_value }, SerchType.IN, "", Value);
        }
        public int UpdateByIn(string TableName, int serchColumnindex, string[] IN_value, object[] Value)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return this.Update(TableName, new string[] { (string)AllColumName[serchColumnindex] }, IN_value, SerchType.IN, "", Value);
        }
        public int UpdateByIn(string TableName, string[] serchColumnName, string[] IN_value, object[] Value)
        {
            return Update(TableName, serchColumnName, IN_value, SerchType.IN, "", Value);
        }

        private int Update(string TableName, string[] serchColumnName, string[] serchValue, SerchType _SerchType, string LikeString, object[] Value)
        {
            if (TableName == null) TableName = this.TableName;
            WtrteCommand("SET SQL_SAFE_UPDATES = 0;");
            List<string> Name_temp = new List<string>();
            List<object[]> temp = new List<object[]>();
            string Command = "";
            object[] AllColumName = GetAllColumn_Name(TableName);

            Command = "UPDATE ";
            Command += TableName + " ";
            Command += "SET ";

            for (int i = 0; i < AllColumName.GetLength(0); i++)
            {
                Command += " ";
                Name_temp.Add("@" + AllColumName[i]);
                Command += AllColumName[i] + "=" + Name_temp[i];
                Command += " ";
                Command += ",";
            }
            Command = Command.Remove(Command.Length - 1);

            Command += this.Get_WHERE_Command(serchColumnName, serchValue, _SerchType, LikeString); 

            for (int i = 0; i < Value.GetLength(0); i++)
            {
                temp.Add(new object[] { Name_temp[i], Value[i] });
            }

            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            _MySqlCommand.Parameters.Clear();
            foreach (object[] temp0 in temp)
            {
                _MySqlCommand.Parameters.AddWithValue((string)temp0[0], temp0[1]);
            }

            _MySqlCommand.ExecuteNonQuery();
            this.CloseConection(_MySqlConnection);
            _MySqlConnection.Dispose();
            _MySqlCommand.Dispose();
            WtrteCommand("SET SQL_SAFE_UPDATES = 1;");
            return 1;
        }

        public int UpdateByDefulteExtra(string TableName, List<object[]> Value)
        {
            string serchColumnName = "GUID";
            List<object> serchValue = new List<object>();
            for (int i = 0; i < Value.Count; i++)
            {
                serchValue.Add(Value[i][0]);
            }
            return UpdateByDefulteExtra(TableName, serchColumnName, serchValue, Value);
        }
        public int UpdateByDefulteExtra(string TableName, string serchColumnName, List<object> serchValue, List<object[]> Value)
        {
            List<object[]> _serchValue = new List<object[]>();
            for (int i = 0; i < serchValue.Count; i++)
            {
                _serchValue.Add(new object[] { serchValue[i] });
            }
            return UpdateByDefulteExtra(TableName, serchColumnName, _serchValue, Value);
        }
        public int UpdateByDefulteExtra(string TableName, string serchColumnName, List<object[]> serchValue, List<object[]> Value)
        {
            List<object[]> list_serchColumnName = new List<object[]>();
            for(int i = 0; i < serchValue.Count; i++)
            {
                list_serchColumnName.Add(new object[] { serchColumnName });
            }
            return UpdateByDefulteExtra(TableName, list_serchColumnName, serchValue, Value);
        }
        public int UpdateByDefulteExtra(string TableName, List<object[]> serchColumnName, List<object[]> serchValue, List<object[]> Value)
        {
            List<string[]> _serchColumnName = new List<string[]>();
            List<string[]> _serchValue = new List<string[]>();
            for (int i = 0; i < serchColumnName.Count; i++)
            {
                _serchColumnName.Add(serchColumnName[i].ObjectToString());
            }
            for (int i = 0; i < serchValue.Count; i++)
            {
                _serchValue.Add(serchValue[i].ObjectToString());
            }
            return UpdateByDefulteExtra(TableName, _serchColumnName, _serchValue, Value, null);
        }
        public int UpdateByDefulteExtra(string TableName, List<string[]> serchColumnName, List<string[]> serchValue, List<object[]> Value)
        {
            return UpdateByDefulteExtra(TableName, serchColumnName, serchValue, Value, null);
        }
        public int UpdateByDefulteExtra(string TableName, List<string[]> serchColumnName, List<string[]> serchValue, List<object[]> Value, PrcessReprot PrcessReprot)
        {
            List<SerchType> _SerchType = new List<SerchType>();
            List<string> LikeString = new List<string>();
            for (int i = 0; i < Value.Count; i++)
            {
                _SerchType.Add(SerchType.DEFULT);
                LikeString.Add("");
            }
            return UpdateExtra(TableName, serchColumnName, serchValue, _SerchType, LikeString, Value , PrcessReprot);
        }
        private int UpdateExtra(string TableName, List<string[]> serchColumnName, List<string[]> serchValue, List<SerchType> _SerchType, List<string> LikeString, List<object[]> Value, PrcessReprot PrcessReprot)
        {
            if (TableName == null) TableName = this.TableName;
            WtrteCommand("SET SQL_SAFE_UPDATES = 0;");
            List<string> Name_temp = new List<string>();
            List<object[]> temp = new List<object[]>();
     
            object[] AllColumName = GetAllColumn_Name(TableName);
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();

            //List<string> list_command = new List<string>();
            //List<object[]> list_value = new List<object[]>();
            //for (int i = 0; i < Value.Count; i++)
            //{
            //    list_value.Add(new object[] { Value[i], serchColumnName[i], serchValue[i], _SerchType[i], LikeString[i] });
            //}
            //Parallel.ForEach(list_value, value =>
            //{
            //    object[] _value = value[0] as object[];
            //    string Command = "";
            //    string text = "";
            //    string colname = "";
            //    Command += $"UPDATE {TableName} SET ";
            //    for (int k = 0; k < _value.Length; k++)
            //    {
            //        text = _value[k].ObjectToString();
            //        text = text.Replace("'", @"\'");
            //        colname = AllColumName[k].ObjectToString();
            //        colname = colname.Replace("'", @"\'");
            //        Command += $"{colname} = '{text}'";
            //        if (k != _value.Length - 1) Command += ",";
            //    }
            //    Command += this.Get_WHERE_Command((string[])value[1], (string[])value[2], (SerchType)value[3], (string)value[4]);
            //    Command += ";";
            //    list_command.Add(Command);
            //});


            //string result_Command = "";
            //for (int i = 0; i < list_command.Count; i++)
            //{
            //    result_Command += list_command[i];
            //}
            //_MySqlCommand.CommandText = result_Command;
            //if(result_Command.StringIsEmpty() == false)_MySqlCommand.ExecuteNonQuery();


            for (int i = 0; i < Value.Count; i++)
            {
                _MySqlCommand.Parameters.Clear();
                temp.Clear();
                Name_temp.Clear();
                string Command = "";
                Command = "UPDATE ";
                Command += TableName + " ";
                Command += "SET ";

                for (int m = 0; m < AllColumName.GetLength(0); m++)
                {
                    Command += $"{AllColumName[m]}=@{AllColumName[m]},";
                    _MySqlCommand.Parameters.AddWithValue((string)AllColumName[m], Value[i][m]);
                    //Name_temp.Add("@" + AllColumName[m]);
                    //Command += AllColumName[m] + "=" + Name_temp[m];
                    //Command += " ";
                    //Command += ",";
                }
                Command = Command.Remove(Command.Length - 1);
                Command += this.Get_WHERE_Command(serchColumnName[i], serchValue[i], _SerchType[i], LikeString[i]);

                //for (int k = 0; k < Value[i].GetLength(0); k++)
                //{
                //    temp.Add(new object[] { Name_temp[k], Value[i][k] });
                //}
                //foreach (object[] temp0 in temp)
                //{
                //    _MySqlCommand.Parameters.AddWithValue((string)temp0[0], temp0[1]);
                //}
                _MySqlCommand.CommandText = Command;
                _MySqlCommand.ExecuteNonQuery();
                if (PrcessReprot != null) PrcessReprot(i, Value.Count);
            }


            this.CloseConection(_MySqlConnection);
            _MySqlConnection.Dispose();
            _MySqlCommand.Dispose();
            WtrteCommand("SET SQL_SAFE_UPDATES = 1;");
            return 1;
        }
        #endregion
        #region Method_Delete
        public int DeleteByDefult(string TableName, int serchColumnindex, string serchValue)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return this.Delete(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { serchValue }, SerchType.DEFULT, "");
        }
        public int DeleteByDefult(string TableName, string serchColumnName, string serchValue)
        {
            return this.Delete(TableName, new string[] { serchColumnName }, new string[] { serchValue }, SerchType.DEFULT, "");
        }
        public int DeleteByDefult(string TableName, int serchColumnindex, string[] serchValue)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return this.Delete(TableName, new string[] { (string)AllColumName[serchColumnindex] }, serchValue, SerchType.DEFULT, "");
        }
        public int DeleteByDefult(string TableName, string[] serchColumnName, string[] serchValue)
        {
            return Delete(TableName, serchColumnName, serchValue, SerchType.DEFULT, "");
        }

        public int DeleteByLike(string TableName, int serchColumnindex, string LikeString)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return this.Delete(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { }, SerchType.LIKE, LikeString);
        }
        public int DeleteByLike(string TableName, string serchColumnName, string LikeString)
        {
            return this.Delete(TableName, new string[] { serchColumnName }, new string[] { }, SerchType.LIKE, LikeString);
        }

        public int DeleteByBetween(string TableName, int serchColumnindex, string brtween_value1, string brtween_value2)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return this.Delete(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "");
        }
        public int DeleteByBetween(string TableName, string serchColumnName, string brtween_value1, string brtween_value2)
        {
            return this.Delete(TableName, new string[] { serchColumnName }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "");
        }

        public int DeleteByIn(string TableName, int serchColumnindex, string IN_value)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return this.Delete(TableName, new string[] { (string)AllColumName[serchColumnindex] }, new string[] { IN_value }, SerchType.IN, "");
        }
        public int DeleteByIn(string TableName, string serchColumnName, string IN_value)
        {
            return this.Delete(TableName, new string[] { serchColumnName }, new string[] { IN_value }, SerchType.IN, "");
        }
        public int DeleteByIn(string TableName, int serchColumnindex, string[] IN_value)
        {
            object[] AllColumName = GetAllColumn_Name(TableName);
            return this.Delete(TableName, new string[] { (string)AllColumName[serchColumnindex] }, IN_value, SerchType.IN, "");
        }
        public int DeleteByIn(string TableName, string[] serchColumnName, string[] IN_value)
        {
            return Delete(TableName, serchColumnName, IN_value, SerchType.IN, "");
        }

        public void DeleteExtra(string TableName , object[] Value)
        {
            List<object[]> list_value = new List<object[]>();
            list_value.Add(Value);
            DeleteExtra(TableName, list_value);
        }
        public void DeleteExtra(string TableName, List<object[]> Value)
        {
            List<object> serchValue = new List<object>();
            for (int i = 0; i < Value.Count; i++)
            {
                serchValue.Add(Value[i][0]);
            }
            DeleteExtra(TableName, "GUID", serchValue);
        }
        public void DeleteExtra(string TableName, string serchColumnName, List<object> serchValue)
        {
            List<object[]> _serchValue = new List<object[]>();
            for (int i = 0; i < serchValue.Count; i++)
            {
                _serchValue.Add(new object[] { serchValue[i] });
            }
            DeleteExtra(TableName, new string[] { serchColumnName }, _serchValue);
        }
        public void DeleteExtra(string TableName, string[] serchColumnName, List<object[]> serchValue)
        {
            List<object[]> _serchColumnName = new List<object[]>();
            for (int i = 0; i < serchValue.Count; i++)
            {
                _serchColumnName.Add(serchColumnName);
            }
            DeleteExtra(TableName, _serchColumnName, serchValue);
        }
        public void DeleteExtra(string TableName, List<object[]> serchColumnName, List<object[]> serchValue)
        {
            List<string[]> _serchColumnName = new List<string[]>();
            List<string[]> _serchValue = new List<string[]>();
            for (int i = 0; i < serchColumnName.Count; i++)
            {
                _serchColumnName.Add(serchColumnName[i].ObjectToString());
            }
            for (int i = 0; i < serchValue.Count; i++)
            {
                _serchValue.Add(serchValue[i].ObjectToString());
            }
            DeleteExtra(TableName, _serchColumnName, _serchValue);
        }
        public void DeleteExtra(string TableName, List<string[]> serchColumnName, List<string[]> serchValue)
        {
            if (TableName == null) TableName = this.TableName;
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            string Command = "";
            Command += "SET SQL_SAFE_UPDATES = 0;";
            for (int i = 0; i < serchColumnName.Count; i++)
            {
                Command += "DELETE FROM ";
                Command += TableName + " ";
                Command += this.Get_WHERE_Command(serchColumnName[i], serchValue[i], SerchType.DEFULT, "");
                Command += ";";
            } 
            _MySqlCommand.CommandText = Command;
            _MySqlCommand.ExecuteNonQuery();

            _MySqlCommand.CommandText = "SET SQL_SAFE_UPDATES = 1;";
            _MySqlCommand.ExecuteNonQuery();
            this.CloseConection(_MySqlConnection);

        }

        private int Delete(string TableName, string[] serchColumnName, string[] serchValue, SerchType _SerchType, string LikeString)
        {
            if (TableName == null) TableName = this.TableName;
            WtrteCommand("SET SQL_SAFE_UPDATES = 0;");
            
            string Command = "";
            Command = "DELETE FROM ";
            Command += TableName + " ";        
            Command += this.Get_WHERE_Command(serchColumnName, serchValue, _SerchType, LikeString);
            WtrteCommand(Command);

            WtrteCommand("SET SQL_SAFE_UPDATES = 1;");
            return 1;
        }
        #endregion
        #region Method_AVG
        public double GetAVG(string TableName, string ColumnName)
        {
            return this.GetAVG(TableName, ColumnName, new string[] { }, new string[] { }, SerchType.NONE, "");
        }
        public double GetAVG_ByBetween(string TableName, string ColumnName, string serchColumnName, string brtween_value1, string brtween_value2)
        {
            return this.GetAVG(TableName, ColumnName, new string[] { serchColumnName }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "");
        }
        public double GetAVG_ByIN(string TableName, string ColumnName, string serchColumnName, string[] IN_value)
        {
            return this.GetAVG(TableName, ColumnName, new string[] { serchColumnName }, IN_value, SerchType.IN, "");
        }
        private double GetAVG(string TableName, string ColumnName, string[] serchColumnName, string[] serchValue, SerchType _SerchType, string LikeString)
        {
            if (TableName == null) TableName = this.TableName;
            double out_value = 0;
            string Command = "";
            object[] AllColumName = GetAllColumn_Name(TableName);
            Command = "SELECT AVG(";
            Command += ColumnName + ") FROM ";
            Command += TableName + " ";
            Command += this.Get_WHERE_Command(serchColumnName,serchValue, _SerchType, LikeString);

            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                out_value = reader.GetDouble(0);
            }
            this.CloseConection(_MySqlConnection);

            return out_value;
        }
        #endregion
        #region Method_COUNT
        public int GetCOUNT(string TableName, string ColumnName, bool GetNull)
        {
            return this.GetCOUNT(TableName, ColumnName, new string[] { }, new string[] { }, SerchType.NONE, "", GetNull);
        }
        public int GetCOUNT_ByBetween(string TableName, string ColumnName, string serchColumnName, string brtween_value1, string brtween_value2)
        {
            return this.GetCOUNT(TableName, ColumnName, new string[] { serchColumnName }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "", false);
        }
        public int GetCOUNT_ByIN(string TableName, string ColumnName, string serchColumnName, string[] IN_value)
        {
            return this.GetCOUNT(TableName, ColumnName, new string[] { serchColumnName }, IN_value, SerchType.IN, "", false);
        }
        private int GetCOUNT(string TableName, string ColumnName, string[] serchColumnName, string[] serchValue, SerchType _SerchType, string LikeString , bool GetNull)
        {
            if (TableName == null) TableName = this.TableName;
            int out_value = 0;
            string Command = "";
            object[] AllColumName = GetAllColumn_Name(TableName);
            Command = "SELECT COUNT(";
            Command += ColumnName + ") FROM ";
            Command += TableName + " ";
            Command += this.Get_WHERE_Command(serchColumnName, serchValue, _SerchType, LikeString);
            if (_SerchType == SerchType.NONE)
            {
                if (GetNull)
                {
                    Command += " IS NULL";
                }         
            }
       
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                out_value = reader.GetInt32(0);
            }
            this.CloseConection(_MySqlConnection);

            return out_value;
        }
        #endregion
        #region Method_MAX
        public double GetMAX(string TableName, string ColumnName)
        {
            return this.GetMAX(TableName, ColumnName, new string[] { }, new string[] { }, SerchType.NONE, "");
        }
        public double GetMAX_ByBetween(string TableName, string ColumnName, string serchColumnName, string brtween_value1, string brtween_value2)
        {
            return this.GetMAX(TableName, ColumnName, new string[] { serchColumnName }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "");
        }
        public double GetMAX_ByIN(string TableName, string ColumnName, string serchColumnName, string[] IN_value)
        {
            return this.GetMAX(TableName, ColumnName, new string[] { serchColumnName }, IN_value, SerchType.IN, "");
        }
        private double GetMAX(string TableName, string ColumnName, string[] serchColumnName, string[] serchValue, SerchType _SerchType, string LikeString)
        {
            if (TableName == null) TableName = this.TableName;
            double out_value = 0;
            string Command = "";
            object[] AllColumName = GetAllColumn_Name(TableName);
            Command = "SELECT MAX(";
            Command += ColumnName + ") FROM ";
            Command += TableName + " ";
            Command += this.Get_WHERE_Command(serchColumnName, serchValue, _SerchType, LikeString);

            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                out_value = reader.GetDouble(0);
            }
            this.CloseConection(_MySqlConnection);

            return out_value;
        }
        #endregion
        #region Method_MIN
        public double GetMIN(string TableName, string ColumnName)
        {
            return this.GetMIN(TableName, ColumnName, new string[] { }, new string[] { }, SerchType.NONE, "");
        }
        public double GetMIN_ByBetween(string TableName, string ColumnName, string serchColumnName, string brtween_value1, string brtween_value2)
        {
            return this.GetMIN(TableName, ColumnName, new string[] { serchColumnName }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "");
        }
        public double GetMIN_ByIN(string TableName, string ColumnName, string serchColumnName, string[] IN_value)
        {
            return this.GetMIN(TableName, ColumnName, new string[] { serchColumnName }, IN_value, SerchType.IN, "");
        }
        private double GetMIN(string TableName, string ColumnName, string[] serchColumnName, string[] serchValue, SerchType _SerchType, string LikeString)
        {
            if (TableName == null) TableName = this.TableName;
            double out_value = 0;
            string Command = "";
            object[] AllColumName = GetAllColumn_Name(TableName);
            Command = "SELECT MIN(";
            Command += ColumnName + ") FROM ";
            Command += TableName + " ";
            Command += this.Get_WHERE_Command(serchColumnName, serchValue, _SerchType, LikeString);

            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                out_value = reader.GetDouble(0);
            }
            this.CloseConection(_MySqlConnection);

            return out_value;
        }
        #endregion
        #region Method_SUM
        public double GetSUM(string TableName, string ColumnName)
        {
            return this.GetSUM(TableName, ColumnName, new string[] { }, new string[] { }, SerchType.NONE, "");
        }
        public double GetSUM_ByBetween(string TableName, string ColumnName, string serchColumnName, string brtween_value1, string brtween_value2)
        {
            return this.GetSUM(TableName, ColumnName, new string[] { serchColumnName }, new string[] { brtween_value1, brtween_value2 }, SerchType.BETWEEN, "");
        }
        public double GetSUM_ByIN(string TableName, string ColumnName, string serchColumnName, string[] IN_value)
        {
            return this.GetSUM(TableName, ColumnName, new string[] { serchColumnName }, IN_value, SerchType.IN, "");
        }
        private double GetSUM(string TableName, string ColumnName, string[] serchColumnName, string[] serchValue, SerchType _SerchType, string LikeString)
        {
            if (TableName == null) TableName = this.TableName;
            double out_value = 0;
            string Command = "";
            object[] AllColumName = GetAllColumn_Name(TableName);
            Command = "SELECT SUM(";
            Command += ColumnName + ") FROM ";
            Command += TableName + " ";
            Command += this.Get_WHERE_Command(serchColumnName, serchValue, _SerchType, LikeString);

            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                out_value = reader.GetDouble(0);
            }
            this.CloseConection(_MySqlConnection);

            return out_value;
        }
        #endregion

        private string Order(string ColumnName, SQLControl.OrderType _OrderType)
        {
            string Order_Command = " ORDER BY " + ColumnName+" ";
            if (_OrderType == OrderType.LowToUp)
            {
                Order_Command += "ASC";
            }
            else if (_OrderType == OrderType.UpToLow)
            {
                Order_Command += "DESC";
            }
            return Order_Command;
        }
        private int OpenConnection(MySqlConnection _MySqlConnection)
        {
            int code = -1;
            IsConnect = false;
            try
            {
                
                _MySqlConnection.Open();
                code = 0;
                IsConnect = true;
            }
            catch
            {
                code = -1;
            }
            return code;
        }
        private int CloseConection(MySqlConnection _MySqlConnection)
        {
            int code = -1;
            IsConnect = false;
            if (_MySqlConnection != null)
            {
                try
                {
                    _MySqlConnection.Close();
                    code = 0;
                }
                catch
                {
                    code = -1;
                }
            }
            return code;
        }
        private string Get_WHERE_Command(string[] serchColumnName, string[] serchValue, SerchType _SerchType, string LikeString)
        {
            string Command = "";
            if (_SerchType == SerchType.DEFULT)
            {
                if (serchColumnName.Length != 0)
                {
                    Command += " WHERE (";
                    for (int i = 0; i < serchColumnName.Length; i++)
                    {
                        Command += serchColumnName[i]+ "=" + "'" + serchValue[i] + "'" + " AND ";
                    }
                    Command = Command.Remove(Command.Length - 5);
                    Command += ")";
                }
            }
            else if (_SerchType == SerchType.BETWEEN)
            {
                string Name = "";
                if (serchColumnName.Length == 1)
                {
                    Command += " WHERE ";
                    Name = "(" + serchColumnName[0] + ")";          
                }
                Command += Name + " >= " + "'" + serchValue[0] + "'" + " AND " + Name  + "<="+ "'" + serchValue[1] + "'" ;
            }
            else if (_SerchType == SerchType.IN)
            {
                if (serchColumnName.Length != 0)
                {
                    Command += " WHERE (";
                    for (int i = 0; i < serchColumnName.Length; i++)
                    {
                        Command +=  serchColumnName[i] ;
                    }
                    Command += ")";
                }
                Command += " IN(";
                for (int i = 0; i < serchValue.Length; i++)
                {
                    Command += '+' + serchValue[i] + "'" + ",";
                }
                Command = Command.Remove(Command.Length - 1);
                Command += ")";
            }
            else if (_SerchType == SerchType.LIKE)
            {
                if (LikeString != "")
                {
                    if (serchColumnName.Length != 0)
                    {
                        Command += " WHERE (";
                        for (int i = 0; i < serchColumnName.Length; i++)
                        {
                            Command +=  serchColumnName[i] ;
                        }
                        Command += ")";
                    }
                    Command += " LIKE " + "'" + LikeString + "'";
                }
            }
            return Command;
        }
        private string Get_WHERE_Command(string[] serchColumnName, SerchType _SerchType, string LikeString)
        {
            string Command = "";
            if (_SerchType == SerchType.DEFULT)
            {
                if (serchColumnName.Length != 0)
                {
                    Command += " WHERE (";
                    for (int i = 0; i < serchColumnName.Length; i++)
                    {
                        Command += serchColumnName[i] + "=" + "@" + serchColumnName[i] + i.ToString() + " AND ";
                    }
                    Command = Command.Remove(Command.Length - 5);
                    Command += ")";
                }
            }
            else if (_SerchType == SerchType.BETWEEN)
            {
                if (serchColumnName.Length != 0)
                {
                    Command += " WHERE (";
                    for (int i = 0; i < serchColumnName.Length; i++)
                    {
                        Command += serchColumnName[i];
                    }
                    Command += ")";
                }
                Command += " BETWEEN " + "@" + serchColumnName[0] + 0.ToString() + " AND " + "@" + serchColumnName[1] + 1.ToString();
            }
            else if (_SerchType == SerchType.IN)
            {
                if (serchColumnName.Length != 0)
                {
                    Command += " WHERE (";
                    for (int i = 0; i < serchColumnName.Length; i++)
                    {
                        Command += serchColumnName[i];
                    }
                    Command += ")";
                }
                Command += " IN(";
                for (int i = 0; i < serchColumnName.Length; i++)
                {
                    Command += "+" + serchColumnName[i] + "'" + ",";
                }
                Command = Command.Remove(Command.Length - 1);
                Command += ")";
            }
            else if (_SerchType == SerchType.LIKE)
            {
                if (LikeString != "")
                {
                    if (serchColumnName.Length != 0)
                    {
                        Command += " WHERE (";
                        for (int i = 0; i < serchColumnName.Length; i++)
                        {
                            Command += serchColumnName[i];
                        }
                        Command += ")";
                    }
                    Command += " LIKE " + "'" + LikeString + "'";
                }
            }
            return Command;
        }
        private string Get_GROUP_BY_Command(string ColumnName)
        {
            string Command = "";
            if (ColumnName != "")
            {
                ColumnName = " GROUP BY " + ColumnName + " ";
            }
            return Command;
        }
        private void WtrteCommand(string CommandText)
        {
            if (CommandText.StringIsEmpty()) return;
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = CommandText;
            _MySqlCommand.ExecuteNonQuery();
            this.CloseConection(_MySqlConnection);
        }
        private void WtrteCommand(string CommandText, object[][] AddWithValue)
        {
            if (CommandText.StringIsEmpty()) return;
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = CommandText;
            _MySqlCommand.Parameters.Clear();
            foreach(object[] temp in AddWithValue)
            {
                _MySqlCommand.Parameters.AddWithValue((string)temp[0], temp[1]);
            }

            _MySqlCommand.ExecuteNonQuery();
            this.CloseConection(_MySqlConnection);
        }

        public static readonly string Column01ImageName = "ImageName";
        public static readonly string Column02ImageName = "ImageBlob";
        public static readonly string Column03ImageName = "Date";
        public void CreatImageTable(string TableName)
        {
            if (TableName == null) TableName = this.TableName;
            Table Image_Table = new Table(TableName);
            Image_Table.AddColumnList(Column01ImageName, Table.StringType.VARCHAR, Table.IndexType.PRIMARY);
            Image_Table.AddColumnList(Column02ImageName, Table.StringType.LONGBLOB, Table.IndexType.None);
            Image_Table.AddColumnList(Column03ImageName, Table.DateType.TIMESTAMP, Table.IndexType.INDEX);
            this.CreatTable(Image_Table);
            int i = 0;
            foreach (Table.ColumnElement temp in Image_Table.GetColumnList())
            {
                if (temp.IndexType == Table.IndexType.INDEX)
                {
                    this.Add_Index(Image_Table.GetTableName(), Image_Table.GetColumnName(i), false);
                }
                else if (temp.IndexType == Table.IndexType.UNIQUE)
                {
                    this.Add_Index(Image_Table.GetTableName(), Image_Table.GetColumnName(i), true);
                }
                else if (temp.IndexType == Table.IndexType.PRIMARY)
                {
                    this.Add_primary_key(Image_Table.GetTableName(), Image_Table.GetColumnName(i));
                }
                i++;
            }
        }
        public bool IsHaveImageName(string TableName ,string ImageName)
        {
            if (TableName == null) TableName = this.TableName;
            object[] object_array = this.GetColumnValue(TableName, Column01ImageName);
            foreach(object obj in object_array)
            {
                if ((string)obj == ImageName) return true;
            }
            return false;
        }
        public bool CheckServerImageNew(string TableName, string ImageName, string TIMESTAMP)
        {
            object[] ImageName_array = this.GetColumnValue(TableName, Column01ImageName);
            object[] TIMESTAMP_array = this.GetColumnValue(TableName, Column03ImageName);
            System.DateTime _DateTime;
            for (int i = 0; i < ImageName_array.Length; i++)
            {
                if ((string)ImageName_array[i] == ImageName)
                {
                    Int64 TimePC = 0;
                    Int64 TimePServer = 0;
                    if (Int64.TryParse(TIMESTAMP, out TimePC))
                    {
                        _DateTime = (DateTime)TIMESTAMP_array[i];
                        if (Int64.TryParse(Date_12H_To_24H(_DateTime.ToString()), out TimePServer))
                        {
                            if (TimePC < TimePServer) return true;
                            else return false;
                        }
                    }
                }
            }
            return false;
        }
        public object[] GetAllImageName(string TableName)
        {
           return this.GetColumnValue(TableName, Column01ImageName);
        }
        public object[] GetAllImageDate(string TableName)
        {
            object[] obj_array = this.GetColumnValue(TableName, Column03ImageName);
            List<string> syring_list = new List<string>();
            System.DateTime _DateTime;
            foreach(object obj in obj_array)
            {
                _DateTime = (System.DateTime)obj;
                syring_list.Add(Date_12H_To_24H(_DateTime.ToString()));
            }
            return syring_list.ToArray();
        }
        public string GetImageDate(string TableName, string ImageName)
        {
            object[] obj_array01 = this.GetAllImageDate(TableName);
            object[] obj_array02 = this.GetAllImageName(TableName);
            for(int i = 0 ; i < obj_array02.Length ; i++)
            {
                if ((string)obj_array02[i] == ImageName) return (string)obj_array01[i];
            }
            return null;
        }
        public string[] GetAllImageColumnsName(string TableName)
        {
            return new string[] { Column01ImageName, Column02ImageName, Column03ImageName };
        }
        public void DeleteImage(string TableName, string ImageName, string TIMESTAMP)
        {
            this.DeleteByDefult(TableName, new string[] { Column01ImageName, Column03ImageName }, new string[] { ImageName, TIMESTAMP });
        }
        public void DeleteImage(string TableName, string ImageName)
        {
            this.DeleteByDefult(TableName, new string[] { Column01ImageName }, new string[] { ImageName });
        }
        public void UploadImage(string TableName, string ImageName, Image Image)
        {
            UploadImage(TableName, ImageName, Image, System.Drawing.Imaging.ImageFormat.Bmp);
        }
        public void UploadImage(string TableName ,string ImageName , Image Image ,System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if (TableName == null) TableName = this.TableName;
            string DATETIME = this.GetTimeNow();
            object[] obj_AllColumnName = GetAllColumn_Name(TableName);
            byte[] buf = Basic.MyConvert.ImageToByte(Image, imageFormat);
            string Command;
            string val0 = "Value0";
            string val1 = "Value1";
            string val2 = "Value2";
            Command = @"INSERT INTO ";
            Command += TableName + " ";
            Command += "(" + (string)obj_AllColumnName[0] + "," + (string)obj_AllColumnName[1] + "," + (string)obj_AllColumnName[2] + ") ";
            Command += "VALUES (@" + val0 + ",@" + val1 + ",@" + val2 + ") ";

            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            _MySqlCommand.Parameters.Clear();
            _MySqlCommand.Parameters.AddWithValue(val0, ImageName);
            _MySqlCommand.Parameters.AddWithValue(val1, buf);
            _MySqlCommand.Parameters.AddWithValue(val2, DATETIME);
            _MySqlCommand.ExecuteNonQuery();
            this.CloseConection(_MySqlConnection);
        }
        public Bitmap DownloadImage(string TableName, string ImageName)
        {
            if (TableName == null) TableName = this.TableName;
            byte[] bmp_buf = null;
            object[] obj_AllColumnName = GetAllColumn_Name(TableName);
            string Command;
            Command = "SELECT ";
            Command += "*";
            Command += " FROM ";
            Command += TableName;
            Command += this.Get_WHERE_Command(new string[] { (string)obj_AllColumnName[0] }, new string[] {ImageName}, SerchType.DEFULT, "");

            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            if (reader.Read())
            {
                bmp_buf = (byte[])reader[(string)obj_AllColumnName[1]];
            }
            this.CloseConection(_MySqlConnection);
            return Basic.MyConvert.ByteToImage(bmp_buf);
        }
        public string GetTimeNow()
        {
            DateTime Date = DateTime.Now;
            string DATE = Date.Year.ToString("0000") + Date.Month.ToString("00") + Date.Day.ToString("00");
            string TIME = Date.Hour.ToString("00") + Date.Minute.ToString("00") + Date.Second.ToString("00");
            return DATE + TIME;
        }
        public DateTime GetSQLTimeNow()
        {
            DateTime date = DateTime.Now;
            string Command = string.Format("select now(6);");
            using (MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;"))
            {
                this.OpenConnection(_MySqlConnection);
                MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();

                _MySqlCommand.CommandText = Command;
                var reader = _MySqlCommand.ExecuteReader();
                int FieldCount = reader.FieldCount;
                while (reader.Read())
                {
                    date = (DateTime)reader[0];
                    Command = reader[0].ToString();
                }
                this.CloseConection(_MySqlConnection);
            }

            return date;
        }
        public string Date_12H_To_24H(string DateTime)
        {
            
            Basic.MyConvert _MyConvert = new Basic.MyConvert();
            string[] DateTime_array = _MyConvert.分解分隔號字串(DateTime, " ");
            if(DateTime_array.Length == 3)
            {
                int Hour = 0;
                string[] Date = _MyConvert.分解分隔號字串(DateTime_array[0], "/");
                string[] Time = _MyConvert.分解分隔號字串(DateTime_array[2], ":");
                if (Time.Length == 3)
                {
                    if(int.TryParse(Time[0] , out Hour))
                    {
                        if (DateTime_array[1] == "上午")
                        {
                            if (Hour == 12) Hour -= 12;
                        }
                        else if(DateTime_array[1] == "下午")
                        {
                            if (Hour != 12) Hour += 12;
                        }
                        else
                        {
                            return null;
                        }
                        if (Date[0].Length < 4) Date[0] = "0" + Date[0];
                        if (Date[0].Length < 4) Date[0] = "0" + Date[0];
                        if (Date[0].Length < 4) Date[0] = "0" + Date[0];
                        if (Date[0].Length < 4) Date[0] = "0" + Date[0];

                        if (Date[1].Length < 2) Date[1] = "0" + Date[1];
                        if (Date[1].Length < 2) Date[1] = "0" + Date[1];
                        if (Date[2].Length < 2) Date[2] = "0" + Date[2];
                        if (Date[2].Length < 2) Date[2] = "0" + Date[2];

                        if (Time[0].Length < 2) Time[0] = "0" + Time[0];
                        if (Time[0].Length < 2) Time[0] = "0" + Time[0];
                        if (Time[1].Length < 2) Time[1] = "0" + Time[1];
                        if (Time[1].Length < 2) Time[1] = "0" + Time[1];
                        if (Time[2].Length < 2) Time[2] = "0" + Time[2];
                        if (Time[2].Length < 2) Time[2] = "0" + Time[2];
                        return Date[0] + Date[1] + Date[2] + Hour.ToString("00") + Time[1] + Time[2];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
             
            }
            return null;
        }
        
    }
    public class Table
    {
        DateTime Date = DateTime.Now;
        private string TableName = "";
        public class ColumnElement
        {
           public string Name;
           public string TypeName;
           public IndexType IndexType = IndexType.None;
        }
        private List<ColumnElement> ColumnList = new List<ColumnElement>();

        public enum IndexType
        {
            None = 0,
            INDEX = 1,
            PRIMARY = 2,
            UNIQUE = 3,      
        }
        public enum OtherType
        {
            ENUM = 1,
            SET = 2,
            IMAGE = 3,
            None = 100,
        }
        public enum ValueType
        {
            TINYINT = 0,
            SMALLINT = 1,
            MEDIUMINT = 2,
            INTEGER = 3,
            BIGINT = 4,
            FLOAT = 5,
            DOUBLE = 6,
            REAL = 7,
            DECIMAL = 8,
            NUMERIC = 9,
            None = 100,
        }
        public enum StringType
        {
            CHAR = 0,
            VARCHAR = 1,
            TINYTEXT = 2,
            TEXT = 3,
            MEDIUMTEXT = 4,
            LONGTEXT = 5,
            LONGBLOB =6,
            TINYBLOB = 7,
            BLOB = 8,
            MEDIUMBLOB = 9,
            None = 100,
        }
        public enum DateType
        {
            DATE = 0,//'1000-01-01' to '9999-12-31'
            TIME = 1,//'-838:59:59' to '838:59:59'
            DATETIME = 2,//'1000-01-01 00:00:00' to '9999-12-31 23:59:59'
            TIMESTAMP = 3,//YYYYMMDDHHMMSS
            YEAR = 4,//YYYY
            None = 100,
        }
 
        public Table(string Name)
        {
            this.SetTableName(Name);
   
        }
        public void SetTableName(string Name)
        {
            this.TableName = Name;
        }
        public string GetTypeName(string ColumnName)
        {
            for(int i = 0; i < ColumnList.Count; i++)
            {
                if(ColumnList[i].Name == ColumnName)
                {
                    return ColumnList[i].TypeName;
                }
            }
            return null;
        }
        public IndexType GetIndexType(string ColumnName)
        {
            for (int i = 0; i < ColumnList.Count; i++)
            {
                if (ColumnList[i].Name == ColumnName)
                {
                    return ColumnList[i].IndexType;
                }
            }
            return IndexType.None;
        }

        public static string GetTypeName(OtherType _OtherType)
        {
            string str = null;
  
            return str;
        }
        public static string GetTypeName(OtherType _OtherType, string[] stringArray)
        {
            string str = null;
    
            if (_OtherType == OtherType.ENUM)
            {
                str = " ENUM(";
                for(int i =0 ; i < stringArray.Length ; i++)
                {
                    str += "'" + stringArray[i] + "',";
                }
                str = str.Remove(str.Length - 1);
                str += ")";
            }
            else if (_OtherType == OtherType.SET)
            {
                str = " SET(";
                for (int i = 0; i < stringArray.Length; i++)
                {
                    str += "'" + stringArray[i] + "',";
                }
                str = str.Remove(str.Length - 1);
                str += ")";
            }
            return str;
        }
        public static string GetTypeName(ValueType _ValueType)
        {
            string str = null;
        
            if(_ValueType == ValueType.TINYINT)
            {
                str = " TINYINT";
            }
            else if (_ValueType == ValueType.BIGINT)
            {
                str = " BIGINT";
            }
            else if (_ValueType == ValueType.SMALLINT)
            {
                str = " SMALLINT";
            }
            else if (_ValueType == ValueType.MEDIUMINT)
            {
                str = " MEDIUMINT";
            }
            else if (_ValueType == ValueType.INTEGER)
            {
                str = " INTEGER";
            }
            else if (_ValueType == ValueType.FLOAT)
            {
                str = " FLOAT";
            }
            else if (_ValueType == ValueType.DOUBLE)
            {
                str = " DOUBLE";
            }
            else if (_ValueType == ValueType.REAL)
            {
                str = " REAL";
            }
            else if (_ValueType == ValueType.DECIMAL)
            {
                str = " DECIMAL";
            }
            else if (_ValueType == ValueType.NUMERIC)
            {
                str = " NUMERIC";
            }
            return str;
        }
        public static string GetTypeName(ValueType _ValueType, uint M)
        {
            string str = null;
            string str_M = "(" + M.ToString() + ")";
            if (_ValueType == ValueType.TINYINT)
            {
                str = " TINYINT" + str_M;
            }
            else if (_ValueType == ValueType.BIGINT)
            {
                str = " BIGINT" + str_M;
            }
            else if (_ValueType == ValueType.SMALLINT)
            {
                str = " SMALLINT" + str_M;
            }
            else if (_ValueType == ValueType.MEDIUMINT)
            {
                str = " MEDIUMINT" + str_M;
            }
            else if (_ValueType == ValueType.INTEGER)
            {
                str = " INTEGER" + str_M;
            }
            else if (_ValueType == ValueType.FLOAT)
            {
                str_M = "(" + M.ToString() + "," + M.ToString() + ")";
                str = " FLOAT" + str_M;
            }
            else if (_ValueType == ValueType.DOUBLE)
            {
                str_M = "(" + M.ToString() + "," + M.ToString() + ")";
                str = " DOUBLE" + str_M;
            }
            else if (_ValueType == ValueType.REAL)
            {
                str_M = "(" + M.ToString() + "," + M.ToString() + ")";
                str = " REAL" + str_M;
            }
            else if (_ValueType == ValueType.DECIMAL)
            {
                str_M = "(" + M.ToString() + "," + M.ToString() + ")";
                str = " DECIMAL" + str_M;
            }
            else if (_ValueType == ValueType.NUMERIC)
            {
                str_M = "(" + M.ToString() + "," + M.ToString() + ")";
                str = " NUMERIC" + str_M;
            }
            return str;
        }
        public static string GetTypeName(ValueType _ValueType, uint M, uint D)
        {
            string str = null;
            string str_M = "(" + M.ToString() + ")";
            if (D > M) D = M;
            if (_ValueType == ValueType.TINYINT)
            {
                str = " TINYINT" + str_M;
            }
            else if (_ValueType == ValueType.SMALLINT)
            {
                str = " SMALLINT" + str_M;
            }
            else if (_ValueType == ValueType.MEDIUMINT)
            {
                str = " MEDIUMINT" + str_M;
            }
            else if (_ValueType == ValueType.INTEGER)
            {
                str = " INTEGER" + str_M;
            }
            else if (_ValueType == ValueType.FLOAT)
            {
                str_M = "(" + M.ToString() + "," + D.ToString() + ")";
                str = " FLOAT" + str_M;
            }
            else if (_ValueType == ValueType.DOUBLE)
            {
                str_M = "(" + M.ToString() + "," + D.ToString() + ")";
                str = " DOUBLE" + str_M;
            }
            else if (_ValueType == ValueType.REAL)
            {
                str_M = "(" + M.ToString() + "," + D.ToString() + ")";
                str = " REAL" + str_M;
            }
            else if (_ValueType == ValueType.DECIMAL)
            {
                str_M = "(" + M.ToString() + "," + D.ToString() + ")";
                str = " DECIMAL" + str_M;
            }
            else if (_ValueType == ValueType.NUMERIC)
            {
                str_M = "(" + M.ToString() + "," + D.ToString() + ")";
                str = " NUMERIC" + str_M;
            }
            return str;
        }
        public static string GetTypeName(StringType _StringType)
        {
            int M = 50;
            string str = null;
            string str_M = "(" + M.ToString() + ")";
            if (_StringType == StringType.CHAR)
            {
                str = " CHAR" + str_M;
            }
            else if (_StringType == StringType.VARCHAR)
            {
                str = " VARCHAR" + str_M;
            }
            else if (_StringType == StringType.TINYTEXT)
            {
                str = " TINYTEXT";
            }
            else if (_StringType == StringType.MEDIUMTEXT)
            {
                str = " MEDIUMTEXT";
            }
            else if (_StringType == StringType.TEXT)
            {
                str = " TEXT";
            }
            else if (_StringType == StringType.LONGTEXT)
            {
                str = " LONGTEXT";
            }    
            else if (_StringType == StringType.LONGBLOB)
            {
                str = " LONGBLOB";
            }
            else if (_StringType == StringType.TINYBLOB)
            {
                str = " TINYBLOB";
            }
            else if (_StringType == StringType.MEDIUMBLOB)
            {
                str = " MEDIUMBLOB";
            }
            else if (_StringType == StringType.BLOB)
            {
                str = " BLOB";
            }
            return str;
        }
        public static string GetTypeName(StringType _StringType, uint M)
        {
            string str = null;
            string str_M = "(" + M.ToString() + ")";
            if (_StringType == StringType.CHAR)
            {
                str = " CHAR" + str_M;
            }
            else if (_StringType == StringType.VARCHAR)
            {
                str = " VARCHAR" + str_M;
            }
            else if (_StringType == StringType.TINYTEXT)
            {
                str = " SMALLINT";
            }
            else if (_StringType == StringType.MEDIUMTEXT)
            {
                str = " MEDIUMTEXT";
            }
            else if (_StringType == StringType.TEXT)
            {
                str = " SMALLINT";
            }
            else if (_StringType == StringType.LONGTEXT)
            {
                str = " SMALLINT";
            }

            return str;
        }
        public static string GetTypeName(DateType _DateType)
        {
            string str = null;
            int M = 6;
            string str_M = "(" + M.ToString() + ")";
            if (_DateType == DateType.DATE)
            {
                str = " DATE";
            }
            else if (_DateType == DateType.TIME)
            {
                str = " TIME";
            }
            else if (_DateType == DateType.DATETIME)
            {
                str = " DATETIME" + str_M;
            }
            else if (_DateType == DateType.TIMESTAMP)
            {
                str = " TIMESTAMP" + str_M;
            }
            else if (_DateType == DateType.YEAR)
            {
                str = " YEAR";
            }
            return str;
        }
        public static string GetTypeName(DateType _DateType, uint M)
        {
            string str = null;
            if (M > 6) M = 6;
            string str_M = "(" + M.ToString() + ")";
            if (_DateType == DateType.DATE)
            {
                str = " DATE";
            }
            else if (_DateType == DateType.TIME)
            {
                str = " TIME";
            }
            else if (_DateType == DateType.DATETIME)
            {
                str = " DATETIME" + str_M;
            }
            else if (_DateType == DateType.TIMESTAMP)
            {
                str = " TIMESTAMP";
            }
            else if (_DateType == DateType.YEAR)
            {
                str = " YEAR";
            }
            return str;
        }
        public string GetTableName()
        {
            return this.TableName;
        }
        public void AddColumnList(string Name, OtherType _OtherType, IndexType _IndexType)
        {
            ColumnElement _ColumnElement = new ColumnElement();
            _ColumnElement.Name = Name;
            _ColumnElement.TypeName = GetTypeName(_OtherType);
            _ColumnElement.IndexType = _IndexType;
            ColumnList.Add(_ColumnElement);
        }
        public void AddColumnList(string Name, OtherType _OtherType, string[] stringArray, IndexType _IndexType)
        {
            ColumnElement _ColumnElement = new ColumnElement();
            _ColumnElement.Name = Name;
            _ColumnElement.TypeName = GetTypeName(_OtherType, stringArray);
            _ColumnElement.IndexType = _IndexType;
            ColumnList.Add(_ColumnElement);
        }
        public void AddColumnList(string Name, ValueType _ValueType, IndexType _IndexType)
        {
            ColumnElement _ColumnElement = new ColumnElement();
            _ColumnElement.Name = Name;
            _ColumnElement.TypeName = GetTypeName(_ValueType);
            _ColumnElement.IndexType = _IndexType;
            ColumnList.Add(_ColumnElement);
        }
        public void AddColumnList(string Name, ValueType _ValueType, uint M, IndexType _IndexType)
        {
            ColumnElement _ColumnElement = new ColumnElement();
            _ColumnElement.Name = Name;
            _ColumnElement.TypeName = GetTypeName(_ValueType, M);
            _ColumnElement.IndexType = _IndexType;
            ColumnList.Add(_ColumnElement);
        }
        public void AddColumnList(string Name, ValueType _ValueType, uint M, uint D, IndexType _IndexType)
        {
            ColumnElement _ColumnElement = new ColumnElement();
            _ColumnElement.Name = Name;
            _ColumnElement.TypeName = GetTypeName(_ValueType, M, D);
            _ColumnElement.IndexType = _IndexType;
            ColumnList.Add(_ColumnElement);
        }
        public void AddColumnList(string Name, StringType _StringType, IndexType _IndexType)
        {
            ColumnElement _ColumnElement = new ColumnElement();
            _ColumnElement.Name = Name;
            _ColumnElement.TypeName = GetTypeName(_StringType);
            _ColumnElement.IndexType = _IndexType;
            ColumnList.Add(_ColumnElement);
        }
        public void AddColumnList(string Name, StringType _StringType, uint M, IndexType _IndexType)
        {
            ColumnElement _ColumnElement = new ColumnElement();
            _ColumnElement.Name = Name;
            _ColumnElement.TypeName = GetTypeName(_StringType, M);
            _ColumnElement.IndexType = _IndexType;
            ColumnList.Add(_ColumnElement);
        }
        public void AddColumnList(string Name, DateType _DateType, IndexType _IndexType)
        {
            ColumnElement _ColumnElement = new ColumnElement();
            _ColumnElement.Name = Name;
            _ColumnElement.TypeName = GetTypeName(_DateType);
            _ColumnElement.IndexType = _IndexType;
            ColumnList.Add(_ColumnElement);
        }
        public void AddColumnList(string Name, DateType _DateType, uint M, IndexType _IndexType)
        {
            ColumnElement _ColumnElement = new ColumnElement();
            _ColumnElement.Name = Name;
            _ColumnElement.TypeName = GetTypeName(_DateType, M);
            _ColumnElement.IndexType = _IndexType;
            ColumnList.Add(_ColumnElement);
        }

        public void ClearColumn()
        {
            this.ColumnList.Clear();
        }
        public string GetAllColumn_Name()
        {
            string str = null;
            foreach (ColumnElement _ColumnElement in this.ColumnList)
            {
                str += _ColumnElement.Name;
                str += _ColumnElement.TypeName;
                str += ",";
            }
            str = str.Remove(str.Length - 1);
            return str;
        }
        public string GetColumnName(int index)
        {
            string str = null;
            if (index < 0) index = 0;
            if (index < ColumnList.Count())
            {
                str += ColumnList[index].Name;
            }
            return str;
        }
        public string GetColumnType(int index)
        {
            string str = null;
            if (index < 0) index = 0;
            if (index < ColumnList.Count())
            {
                str += ColumnList[index].TypeName;
            }
            return str;
        }
        public List<ColumnElement> GetColumnList()
        {
            return this.ColumnList;
        }
        public int GetNumOfColumn()
        {
            return this.ColumnList.Count;
        }

        public string ToDATE_String(string Year, string Month, string Day)
        {
            int int_Year = 0;
            int int_Month = 0;
            int int_Day = 0;
            if (!int.TryParse(Year, out int_Year)) return null;
            if (!int.TryParse(Month, out int_Month)) return null;
            if (!int.TryParse(Day, out int_Day)) return null;
            return this.ToDATE_String(int_Year, int_Month, int_Day);
        }
        public string ToDATE_String(int Year, int Month, int Day)
        {
            return Year.ToString("0000") + "/" + Month.ToString("00") + "/" + Day.ToString("00");
        }
        public string ToTIME_String(string Hour, string Min, string Sec)
        {
            int int_Hour = 0;
            int int_Min = 0;
            int int_Sec = 0;
            if (!int.TryParse(Hour, out int_Hour)) return null;
            if (!int.TryParse(Min, out int_Min)) return null;
            if (!int.TryParse(Sec, out int_Sec)) return null;
            return this.ToTIME_String(int_Hour, int_Min, int_Sec);
        }
        public string ToTIME_String(int Hour, int Min, int Sec)
        {
            return Hour.ToString("00") + ":" + Min.ToString("00") + ":" + Sec.ToString("00");
        }
        public string ToDATETIME_String(string Year, string Month, string Day, string Hour, string Min, string Sec)
        {
            string DATE = this.ToDATE_String(Year, Month, Day);
            string TIME = this.ToTIME_String(Hour, Min, Sec);
            return DATE + " " + TIME;
        }
        public string ToDATETIME_String(int Year, int Month, int Day, int Hour, int Min, int Sec)
        {
            string DATE = this.ToDATE_String(Year, Month, Day);
            string TIME = this.ToTIME_String(Hour, Min, Sec);
            return DATE + " " + TIME;
        }
        public string ToTIMESTAMP_String(string Year, string Month, string Day, string Hour, string Min, string Sec)
        {
            int int_Year = 0;
            int int_Month = 0;
            int int_Day = 0;
            if (!int.TryParse(Year, out int_Year)) return null;
            if (!int.TryParse(Month, out int_Month)) return null;
            if (!int.TryParse(Day, out int_Day)) return null;

            int int_Hour = 0;
            int int_Min = 0;
            int int_Sec = 0;
            if (!int.TryParse(Hour, out int_Hour)) return null;
            if (!int.TryParse(Min, out int_Min)) return null;
            if (!int.TryParse(Sec, out int_Sec)) return null;

            return ToTIMESTAMP_String(Year, Month, Day, Hour, Min, Sec);
        }
        public string ToTIMESTAMP_String(int Year, int Month, int Day, int Hour, int Min, int Sec)
        {
            string DATE = Year.ToString("0000") + Month.ToString("00") + Day.ToString("00");
            string TIME = Hour.ToString("00") + Min.ToString("00") + Sec.ToString("00");
            return DATE + TIME;
        }
        public string GetTimeNow(Table.DateType DateType)
        {
            Date = DateTime.Now;
            if (DateType == Table.DateType.DATE)
            {
                return this.ToDATE_String(Date.Year, Date.Month, Date.Day);
            }
            else if (DateType == Table.DateType.TIME)
            {
                return this.ToTIME_String(Date.Hour, Date.Minute, Date.Second);
            }
            else if (DateType == Table.DateType.DATETIME)
            {
                return this.ToDATETIME_String(Date.Year, Date.Month, Date.Day, Date.Hour, Date.Minute, Date.Second);
            }
            else if (DateType == Table.DateType.TIMESTAMP)
            {
                return this.ToDATETIME_String(Date.Year, Date.Month, Date.Day, Date.Hour, Date.Minute, Date.Second);
            }
            else
            {
                return null;
            }
        }
  
    }
}
