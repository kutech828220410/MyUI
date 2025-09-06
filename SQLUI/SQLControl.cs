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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;
using System.Threading;

namespace SQLUI
{
    public class SQLControl
    {
        public delegate void PrcessReprot(int value , int max);
        MySqlConnectionStringBuilder _MySqlConnectionStringBuilder = null;
        public string TableName = "";
        private bool IsConnect = false;

        public string Server = "";
        public string Database = "";
        public string UserID = "";
        public string Password = "";
        public uint Port = 1;
        public MySqlSslMode SSLMode;
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
        /// <summary>
        /// 新增單筆資料列至指定資料表。
        /// </summary>
        /// <param name="tableName">資料表名稱，若為 null 或空字串則使用預設 TableName。</param>
        /// <param name="value">單筆資料列的欄位值陣列。</param>
        /// <param name="ct">取消作業的 CancellationToken。</param>
        /// <returns>受影響的資料筆數。</returns>
        public async Task<int> AddRowAsync(string tableName, object[] value, CancellationToken ct = default)
        {
            List<object[]> values = new List<object[]>();
            values.Add(value);
            return await AddRowsAsync(tableName, values, ct).ConfigureAwait(false);
        }
        /// <summary>
        /// 新增多筆資料列至指定資料表。
        /// </summary>
        /// <param name="tableName">資料表名稱，若為 null 或空字串則使用預設 TableName。</param>
        /// <param name="values">多筆資料列的欄位值集合。</param>
        /// <param name="ct">取消作業的 CancellationToken。</param>
        /// <returns>受影響的資料筆數。</returns>
        public async Task<int> AddRowsAsync(string tableName, List<object[]> values, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(tableName)) tableName = this.TableName;
            if (values == null || values.Count == 0) return 0;

            object[] allColumnNames = GetAllColumn_Name(tableName);
            if (values[0].Length == 0 || values[0].Length > allColumnNames.Length) return 0;

            var connStr = _MySqlConnectionStringBuilder.ConnectionString + ";Pooling=true;";
            int affected = 0;

            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync(ct).ConfigureAwait(false);

                using (var tx = await conn.BeginTransactionAsync(ct).ConfigureAwait(false))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tx;

                    // 建立 INSERT 模板
                    var sb = new StringBuilder();
                    sb.Append($"INSERT INTO {tableName} VALUES (");

                    for (int k = 0; k < values[0].Length; k++)
                    {
                        var colName = (string)allColumnNames[k];
                        if (k > 0) sb.Append(",");
                        sb.Append($"@{colName}");

                        // 預先建立參數
                        var p = cmd.CreateParameter();
                        p.ParameterName = colName; // 注意這裡不用 @
                        p.Value = DBNull.Value;
                        cmd.Parameters.Add(p);
                    }
                    sb.Append(");");
                    cmd.CommandText = sb.ToString();

                    // 逐筆更新參數並執行
                    foreach (var row in values)
                    {
                        for (int k = 0; k < row.Length; k++)
                        {
                            var colName = (string)allColumnNames[k];
                            cmd.Parameters[colName].Value = row[k] ?? DBNull.Value;
                        }

                        affected += await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
                    }

                    // Commit transaction
                    tx.Commit();
                }
            }

            return affected;
        }
        /// <summary>
        /// 更新單筆資料列（依 GUID 比對）
        /// </summary>
        /// <param name="tableName">資料表名稱，若為 null 或空字串則使用預設 TableName。</param>
        /// <param name="value">單筆完整資料列（含 GUID 與所有欄位值）。</param>
        /// <param name="ct">取消作業的 CancellationToken。</param>
        /// <returns>受影響的資料筆數。</returns>
        public async Task<int> UpdateRowAsync(string tableName, object[] value, CancellationToken ct = default)
        {
            List<object[]> values = new List<object[]>();
            values.Add(value);
            return await UpdateRowsAsync(tableName, values, ct).ConfigureAwait(false);
        }
        /// <summary>
        /// 更新多筆資料列（依 GUID 比對）
        /// </summary>
        /// <param name="tableName">資料表名稱，若為 null 或空字串則使用預設 TableName。</param>
        /// <param name="values">多筆完整資料列（含 GUID 與所有欄位值）。</param>
        /// <param name="ct">取消作業的 CancellationToken。</param>
        /// <returns>受影響的資料筆數。</returns>
        public async Task<int> UpdateRowsAsync(string tableName, List<object[]> values, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(tableName)) tableName = this.TableName;
            if (values == null || values.Count == 0) return 0;

            object[] allColumnNames = GetAllColumn_Name(tableName);
            int colCount = allColumnNames.Length;
            if (values[0].Length != colCount) return 0; // 你已保證一致，這裡再防呆一次

            var connStr = _MySqlConnectionStringBuilder.ConnectionString + ";Pooling=true;";
            int affected = 0;

            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync(ct).ConfigureAwait(false);

                using (var tx = conn.BeginTransaction())       // MySql.Data：同步 Commit()
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tx;

                    // UPDATE {table} SET col2=@col2, col3=@col3, ... WHERE GUID=@GUID;
                    var sb = new StringBuilder();
                    sb.Append($"UPDATE {tableName} SET ");
                    for (int i = 1; i < colCount; i++)
                    {
                        if (i > 1) sb.Append(", ");
                        sb.Append($"{allColumnNames[i]}=@{allColumnNames[i]}");
                    }
                    sb.Append(" WHERE GUID=@GUID;");
                    cmd.CommandText = sb.ToString();

                    // 預先建立所有參數（GUID + 其餘欄位）
                    for (int i = 0; i < colCount; i++)
                    {
                        var p = cmd.CreateParameter();
                        p.ParameterName = (string)allColumnNames[i];  // 不含 @
                        p.Value = DBNull.Value;
                        cmd.Parameters.Add(p);
                    }

                    // 逐筆指定值並執行
                    foreach (var row in values)
                    {
                        for (int i = 0; i < colCount; i++)
                        {
                            cmd.Parameters[(string)allColumnNames[i]].Value = row[i] ?? DBNull.Value;
                        }
                        affected += await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
                    }

                    tx.Commit(); // 若改用 MySqlConnector → await tx.CommitAsync(ct);
                }
            }

            return affected;
        }
        /// <summary>
        /// 刪除單筆資料列（依 GUID 比對）。
        /// </summary>
        /// <param name="tableName">資料表名稱，若為 null 或空字串則使用預設 TableName。</param>
        /// <param name="value">單筆資料列（GUID 須存在於索引 0）。</param>
        /// <param name="ct">取消作業的 CancellationToken。</param>
        /// <returns>受影響的資料筆數。</returns>
        public async Task<int> DeleteRowAsync(string tableName, object[] value, CancellationToken ct = default)
        {
            List<object[]> values = new List<object[]>();
            values.Add(value);
            return await DeleteRowsAsync(tableName, values, ct).ConfigureAwait(false);
        }
        /// <summary>
        /// 刪除多筆資料列（依 GUID 比對）。
        /// </summary>
        /// <param name="tableName">資料表名稱，若為 null 或空字串則使用預設 TableName。</param>
        /// <param name="rows">多筆資料列（每筆的 GUID 須存在於索引 0）。</param>
        /// <param name="ct">取消作業的 CancellationToken。</param>
        /// <returns>受影響的資料筆數。</returns>
        public async Task<int> DeleteRowsAsync(string tableName, List<object[]> rows, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(tableName)) tableName = this.TableName;
            if (rows == null || rows.Count == 0) return 0;

            var connStr = _MySqlConnectionStringBuilder.ConnectionString + ";Pooling=true;";
            int affected = 0;

            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync(ct).ConfigureAwait(false);

                using (var tx = conn.BeginTransaction())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tx;
                    cmd.CommandText = $"DELETE FROM {tableName} WHERE GUID=@GUID;";

                    var p = cmd.CreateParameter();
                    p.ParameterName = "GUID";
                    p.Value = DBNull.Value;
                    cmd.Parameters.Add(p);

                    foreach (var row in rows)
                    {
                        object guid = (row != null && row.Length > 0) ? row[0] : null;
                        cmd.Parameters["GUID"].Value = guid ?? DBNull.Value;
                        affected += await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
                    }

                    tx.Commit();
                }
            }

            return affected;
        }
        /// <summary>
        /// 執行 SQL 指令並回傳查詢結果。
        /// </summary>
        /// <param name="commandText">欲執行的 SQL 查詢字串。</param>
        /// <param name="ct">取消作業的 CancellationToken。</param>
        /// <returns>查詢結果集合，每筆為 object[]。</returns>
        /// <remarks>
        /// 此方法使用 DataReader 逐筆讀取資料，並將每列存入 object[]。
        /// </remarks>
        public async Task<List<object[]>> WriteCommandAsync(string commandText, CancellationToken ct = default)
        {
            List<object[]> results = new List<object[]>();
            if (commandText.StringIsEmpty()) return results;

            var connStr = _MySqlConnectionStringBuilder.ConnectionString + ";Pooling=true;";

            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync(ct).ConfigureAwait(false);

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = commandText;

                    using (var reader = await cmd.ExecuteReaderAsync(ct).ConfigureAwait(false))
                    {
                        int fieldCount = reader.FieldCount;

                        while (await reader.ReadAsync(ct).ConfigureAwait(false))
                        {
                            var values = new object[fieldCount];
                            reader.GetValues(values);
                            results.Add(values);
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 初始化 SQLControl 類別的新實例，使用指定的伺服器、資料庫、表格名稱、使用者ID、密碼、連接埠和 SSL 模式。
        /// </summary>
        /// <param name="Server">伺服器名稱。</param>
        /// <param name="Database">資料庫名稱。</param>
        /// <param name="TableName">表格名稱。</param>
        /// <param name="UserID">使用者ID。</param>
        /// <param name="Password">密碼。</param>
        /// <param name="Port">連接埠號。</param>
        /// <param name="SSLMode">SSL 模式。</param>
        public SQLControl(string Server, string Database, string TableName, string UserID, string Password, uint Port, MySqlSslMode SSLMode)
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
            //TestConnection();
        }

        /// <summary>
        /// 初始化 SQLControl 類別的新實例，使用指定的伺服器、資料庫、使用者ID、密碼、連接埠和 SSL 模式。
        /// </summary>
        /// <param name="Server">伺服器名稱。</param>
        /// <param name="Database">資料庫名稱。</param>
        /// <param name="UserID">使用者ID。</param>
        /// <param name="Password">密碼。</param>
        /// <param name="Port">連接埠號。</param>
        /// <param name="SSLMode">SSL 模式。</param>
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
            //TestConnection();
        }

        /// <summary>
        /// 初始化 SQLControl 類別的新實例，使用指定的伺服器、資料庫、使用者ID、密碼和 SSL 模式。
        /// </summary>
        /// <param name="Server">伺服器名稱。</param>
        /// <param name="Database">資料庫名稱。</param>
        /// <param name="UserID">使用者ID。</param>
        /// <param name="Password">密碼。</param>
        /// <param name="SSLMode">SSL 模式。</param>
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

        /// <summary>
        /// 初始化 SQLControl 類別的新實例，使用指定的伺服器、資料庫、使用者ID、密碼和連接埠。
        /// </summary>
        /// <param name="Server">伺服器名稱。</param>
        /// <param name="Database">資料庫名稱。</param>
        /// <param name="UserID">使用者ID。</param>
        /// <param name="Password">密碼。</param>
        /// <param name="Port">連接埠號。</param>
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

        /// <summary>
        /// 初始化 SQLControl 類別的新實例，使用指定的 Table 物件。
        /// </summary>
        /// <param name="table">Table 物件，包含伺服器、資料庫、使用者ID和密碼。</param>
        public SQLControl(Table table)
        {
            this.TableName = table.TableName;
            this.Server = table.Server;
            this.Database = table.DBName;
            this.UserID = table.Username;
            this.Password = table.Password;
            this.Port = (uint)table.Port.StringToInt32();
            this.SSLMode = MySqlSslMode.None;
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

        /// <summary>
        /// 初始化 SQLControl 類別的新實例，使用指定的伺服器、資料庫、使用者ID和密碼。
        /// </summary>
        /// <param name="Server">伺服器名稱。</param>
        /// <param name="Database">資料庫名稱。</param>
        /// <param name="UserID">使用者ID。</param>
        /// <param name="Password">密碼。</param>
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

        /// <summary>
        /// 初始化 SQLControl 類別的新實例。
        /// </summary>
        public SQLControl()
        {

        }


        public void Init(Table table)
        {
            this.TableName = table.TableName;
            this.Server = table.Server;
            this.Database = table.DBName;
            this.UserID = table.Username;
            this.Password = table.Password;
            this.Port = (uint)table.Port.StringToInt32();
            this.SSLMode = MySqlSslMode.None;
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
        /// <summary>
        /// 設定伺服器和資料庫。
        /// </summary>
        /// <param name="server">伺服器名稱。</param>
        /// <param name="dbName">資料庫名稱。</param>
        public void Set_Config(string server, string dbName)
        {
            this.Server = server;
            this.Database = dbName;
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
        }

        /// <summary>
        /// 設定資料庫。
        /// </summary>
        /// <param name="Database">資料庫名稱。</param>
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

        /// <summary>
        /// 設定連接資訊。
        /// </summary>
        /// <param name="Server">伺服器名稱。</param>
        /// <param name="UserID">使用者ID。</param>
        /// <param name="Password">密碼。</param>
        /// <param name="Port">連接埠號。</param>
        /// <param name="SSLMode">SSL 模式。</param>
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

        /// <summary>
        /// 測試資料庫連接。
        /// </summary>
        /// <returns>如果連接成功，則返回 true；否則返回 false。</returns>
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

        /// <summary>
        /// 獲取連接狀態。
        /// </summary>
        /// <returns>如果連接成功，則返回 true；否則返回 false。</returns>
        public bool GetConcetStatu()
        {
            return IsConnect;
        }

        /// <summary>
        /// 獲取伺服器名稱。
        /// </summary>
        /// <returns>如果連接成功，則返回伺服器名稱；否則返回 null。</returns>
        public string GetServer()
        {
            if (IsConnect) return this.Server;
            else return null;
        }

        /// <summary>
        /// 獲取資料庫名稱。
        /// </summary>
        /// <returns>如果連接成功，則返回資料庫名稱；否則返回 null。</returns>
        public string GetDatabase()
        {
            if (IsConnect) return this.Database;
            else return null;
        }

        /// <summary>
        /// 獲取使用者ID。
        /// </summary>
        /// <returns>如果連接成功，則返回使用者ID；否則返回 null。</returns>
        public string GetUserID()
        {
            if (IsConnect) return this.UserID;
            else return null;
        }

        /// <summary>
        /// 獲取密碼。
        /// </summary>
        /// <returns>如果連接成功，則返回密碼；否則返回 null。</returns>
        public string GetPassword()
        {
            if (IsConnect) return this.Password;
            else return null;
        }

        /// <summary>
        /// 獲取連接埠號。
        /// </summary>
        /// <returns>如果連接成功，則返回連接埠號；否則返回 null。</returns>
        public string GetPort()
        {
            if (IsConnect) return this.Port.ToString();
            else return null;
        }

        /// <summary>
        /// 獲取 SSL 模式。
        /// </summary>
        /// <returns>如果連接成功，則返回 SSL 模式；否則返回 null。</returns>
        public string GetSSLMode()
        {
            if (IsConnect) return this.SSLMode.ToString();
            else return null;
        }


        /// <summary>
        /// 獲取所有資料庫的名稱。
        /// </summary>
        /// <returns>包含所有資料庫名稱的列表。</returns>
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

        /// <summary>
        /// 創建資料庫。
        /// </summary>
        /// <param name="Name">資料庫名稱。</param>
        public void Create_DataBase(string Name)
        {
            this.Create_DataBase(Name, "utf8");
        }

        /// <summary>
        /// 創建資料庫，使用指定的編碼。
        /// </summary>
        /// <param name="Name">資料庫名稱。</param>
        /// <param name="Encoding">資料庫編碼。</param>
        public void Create_DataBase(string Name, string Encoding)
        {
            string Command = string.Format("create database {0} DEFAULT CHARACTER SET {1};", Name, Encoding);
            WtrteCommand(Command);
        }

        /// <summary>
        /// 刪除資料庫。
        /// </summary>
        /// <param name="Name">資料庫名稱。</param>
        public void Drop_DataBase(string Name)
        {
            string Command = string.Format("drop database {0};", Name);
            WtrteCommand(Command);
        }

        /// <summary>
        /// 匯入資料庫。
        /// </summary>
        /// <param name="databaseName">資料庫名稱。</param>
        /// <param name="fullfilePath">資料庫檔案完整路徑。</param>
        /// <param name="root_password">根使用者密碼。</param>
        /// <param name="mysqlDumpPath">MySQL Dump 工具路徑。</param>
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
            String _mysqlDumpPath = mysqlDumpPath;
            command = "cd " + _mysqlDumpPath;
            p.StandardInput.WriteLine(command);
            String dbName = databaseName;
            String outputPath = @fullfilePath;
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            command = "mysqldump -u root -p" + root_password + " " + dbName + " --default-character-set=utf8" + " < " + outputPath + "\\" + dbName + ".sql";
            p.StandardInput.WriteLine(command);
            p.StandardInput.WriteLine("exit");
            p.Close();
        }

        /// <summary>
        /// 匯入資料表。
        /// </summary>
        /// <param name="databaseName">資料庫名稱。</param>
        /// <param name="TableName">資料表名稱。</param>
        /// <param name="fullfilePath">資料表檔案完整路徑。</param>
        /// <param name="root_password">根使用者密碼。</param>
        /// <param name="mysqlDumpPath">MySQL Dump 工具路徑。</param>
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
            String _mysqlDumpPath = mysqlDumpPath;
            command = "cd " + _mysqlDumpPath;
            p.StandardInput.WriteLine(command);
            String dbName = databaseName;
            String outputPath = @fullfilePath;
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            command = "mysqldump -u root -p" + root_password + " " + dbName + " " + TableName + " --default-character-set=utf8" + " < " + outputPath + "\\" + TableName + ".sql";
            p.StandardInput.WriteLine(command);
            p.StandardInput.WriteLine("exit");
            p.Close();
        }

        /// <summary>
        /// 匯出資料庫。
        /// </summary>
        /// <param name="databaseName">資料庫名稱。</param>
        /// <param name="fullfilePath">匯出檔案的完整路徑。</param>
        /// <param name="root_password">根使用者密碼。</param>
        /// <param name="mysqlDumpPath">MySQL Dump 工具路徑。</param>
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
            String _mysqlDumpPath = mysqlDumpPath;
            command = "cd " + _mysqlDumpPath;
            p.StandardInput.WriteLine(command);
            String dbName = databaseName;
            String outputPath = @fullfilePath;
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            command = "mysqldump -u root -p" + root_password + " " + dbName + " --default-character-set=utf8" + " > " + outputPath + "\\" + dbName + ".sql";
            p.StandardInput.WriteLine(command);
            p.StandardInput.WriteLine("exit");
            p.Close();
        }

        /// <summary>
        /// 匯出資料表。
        /// </summary>
        /// <param name="databaseName">資料庫名稱。</param>
        /// <param name="TableName">資料表名稱。</param>
        /// <param name="fullfilePath">匯出檔案的完整路徑。</param>
        /// <param name="root_password">根使用者密碼。</param>
        /// <param name="mysqlDumpPath">MySQL Dump 工具路徑。</param>
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
            String _mysqlDumpPath = mysqlDumpPath;
            command = "cd " + _mysqlDumpPath;
            p.StandardInput.WriteLine(command);
            String dbName = databaseName;
            String outputPath = @fullfilePath;
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            command = "mysqldump -u root -p" + root_password + " " + dbName + " " + TableName + " --default-character-set=utf8" + " > " + outputPath + "\\" + TableName + ".sql";
            p.StandardInput.WriteLine(command);
            p.StandardInput.WriteLine("exit");
            p.Close();
        }

        /// <summary>
        /// 設置資料表名稱。
        /// </summary>
        /// <param name="Old_Name">舊的資料表名稱。</param>
        /// <param name="New_Name">新的資料表名稱。</param>
        public void Set_Table_Name(string Old_Name, string New_Name)
        {
            string Command = string.Format("USE {0};", this.Database);
            WtrteCommand(Command);
            Command = string.Format("RENAME TABLE {0} TO {1};", Old_Name, New_Name);
            WtrteCommand(Command);
        }

        /// <summary>
        /// 設置欄位名稱。
        /// </summary>
        /// <param name="TableName">資料表名稱。</param>
        /// <param name="Old_Name">舊的欄位名稱。</param>
        /// <param name="New_Name">新的欄位名稱。</param>
        /// <param name="valueType">欄位類型。</param>
        /// <param name="TypeLen">欄位長度。</param>
        public void Set_Columm_Name(string TableName, string Old_Name, string New_Name, string valueType, string TypeLen)
        {
            if (TableName == null) TableName = this.TableName;
            if (TypeLen == "longblob") TypeLen = "";
            if (TypeLen != "") TypeLen = "(" + TypeLen + ")";
            string Command = string.Format("ALTER TABLE {0} CHANGE COLUMN {1} {2} {3}{4}", TableName, Old_Name, New_Name, valueType, TypeLen);
            WtrteCommand(Command);
        }

        /// <summary>
        /// 添加欄位。
        /// </summary>
        /// <param name="TableName">資料表名稱。</param>
        /// <param name="ColumnName">欄位名稱。</param>
        /// <param name="ColumnType">欄位類型。</param>
        /// <returns>如果欄位已存在，返回 -1；否則返回 1。</returns>
        public int Add_Column(string TableName, string ColumnName, string ColumnType)
        {
            if (TableName == null) TableName = this.TableName;
            string[] ColumnNames = this.GetAllColumn_Name(TableName);
            foreach (string value in ColumnNames)
            {
                if (value == ColumnName) return -1;
            }

            return this.Add_Column(TableName, ColumnName, ColumnType, ColumnNames[ColumnNames.Length - 1]);
        }

        /// <summary>
        /// 添加欄位，並設置索引類型。
        /// </summary>
        /// <param name="TableName">資料表名稱。</param>
        /// <param name="ColumnName">欄位名稱。</param>
        /// <param name="ColumnType">欄位類型。</param>
        /// <param name="indexType">索引類型。</param>
        /// <param name="AfterColumnName">添加到指定欄位之後。</param>
        /// <returns>如果欄位已存在，返回 -1；否則返回 1。</returns>
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

        /// <summary>
        /// 添加欄位，並指定欄位索引位置。
        /// </summary>
        /// <param name="TableName">資料表名稱。</param>
        /// <param name="ColumnName">欄位名稱。</param>
        /// <param name="ColumnType">欄位類型。</param>
        /// <param name="index">索引位置。</param>
        /// <returns>如果欄位已存在，返回 -1；否則返回 1。</returns>
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

        /// <summary>
        /// 添加欄位，設置索引類型並指定欄位索引位置。
        /// </summary>
        /// <param name="TableName">資料表名稱。</param>
        /// <param name="ColumnName">欄位名稱。</param>
        /// <param name="ColumnType">欄位類型。</param>
        /// <param name="indexType">索引類型。</param>
        /// <param name="index">索引位置。</param>
        /// <returns>如果欄位已存在，返回 -1；否則返回 1。</returns>
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
                if (indexType != Table.IndexType.None)
                {
                    this.Add_Index(TableName, ColumnName, indexType);
                }
            }
            return temp;
        }

        /// <summary>
        /// 添加欄位，並指定欄位之後的位置。
        /// </summary>
        /// <param name="TableName">資料表名稱。</param>
        /// <param name="ColumnName">欄位名稱。</param>
        /// <param name="ColumnType">欄位類型。</param>
        /// <param name="AfterColumnName">添加到指定欄位之後。</param>
        /// <returns>返回 1 表示成功。</returns>
        public int Add_Column(string TableName, string ColumnName, string ColumnType, string AfterColumnName)
        {
            if (TableName == null) TableName = this.TableName;
            string Command = $"ALTER TABLE {this.Database}.{TableName} ADD COLUMN {ColumnName} {ColumnType} NULL AFTER {AfterColumnName}";
            WtrteCommand(Command);
            return 1;
        }

        /// <summary>
        /// 刪除欄位。
        /// </summary>
        /// <param name="TableName">資料表名稱。</param>
        /// <param name="ColumnName">欄位名稱。</param>
        public void Drop_Column(string TableName, string ColumnName)
        {
            if (TableName == null) TableName = this.TableName;
            string Command = string.Format(" alter table {0} drop {1};", TableName, ColumnName);
            WtrteCommand(Command);
        }

        /// <summary>
        /// 修改欄位位置，將欄位移動到指定欄位之後。
        /// </summary>
        /// <param name="TableName">資料表名稱。</param>
        /// <param name="Mod_ColumnName">要修改的欄位名稱。</param>
        /// <param name="Mod_ColumnType">要修改的欄位類型。</param>
        /// <param name="Mod_DataLen">要修改的欄位長度。</param>
        /// <param name="Target_ColumnName">目標欄位名稱。</param>
        public void Modify_After_Column(string TableName, string Mod_ColumnName, string Mod_ColumnType, string Mod_DataLen, string Target_ColumnName)
        {
            if (TableName == null) TableName = this.TableName;
            if (Mod_ColumnType == "longblob") Mod_DataLen = "";
            if (Mod_DataLen != "") Mod_DataLen = "(" + Mod_DataLen + ")";
            string Command = string.Format("ALTER TABLE {0} MODIFY {1} {2}{3} AFTER {4};", TableName, Mod_ColumnName, Mod_ColumnType, Mod_DataLen, Target_ColumnName);
            WtrteCommand(Command);
        }

        /// <summary>
        /// 修改欄位位置，將欄位移動到表的第一欄。
        /// </summary>
        /// <param name="TableName">資料表名稱。</param>
        /// <param name="Mod_ColumnName">要修改的欄位名稱。</param>
        /// <param name="Mod_ColumnType">要修改的欄位類型。</param>
        /// <param name="Mod_DataLen">要修改的欄位長度。</param>
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
                //if (temp.TypeName == Table.GetTypeName(Table.StringType.TEXT)) continue;
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
        public bool CheckAllColumnName(Table table ,bool autoAdd)
        {
            string TableName = table.GetTableName();
            object[] obj_colname = this.GetAllColumn_Name(TableName);
            List<string> list_error_msg = new List<string>();
            string error_msg = "";
            for (int k = 0; k < table.GetColumnList().Count; k++)
            {
                bool flag_OK = false;
                for (int i = 0; i < obj_colname.Length; i++)
                {
                    string str = obj_colname[i].ObjectToString();
                    if (table.GetColumnList()[k].Name == str)
                    {
                        if (i != k)
                        {
                            list_error_msg.Add($"排序不一致>>>ColumnName : [{str}] ,Database排序 : {i} , 程式排序 : {k}");
                        }
                        flag_OK = true;
                    }
                }
                if (!flag_OK)
                {
                    if ((k - 1) >= 0)
                    {
                        if (autoAdd) Add_Column(TableName, table.GetColumnList()[k].Name, table.GetTypeName(table.GetColumnList()[k].Name), table.GetIndexType(table.GetColumnList()[k].Name), table.GetColumnList()[k - 1].Name);
                    }

                    list_error_msg.Add($"DataBase找無欄位>> 請新增   [排序:{k}]{table.GetColumnList()[k].ToString()}");
                }
            }

            for (int i = 0; i < list_error_msg.Count; i++)
            {
                error_msg += $"({(i + 1).ToString("00")}).{list_error_msg[i]}\n";
            }
            if (error_msg.Length != 0)
            {
                error_msg = $"TableName : {table.GetTableName()}\n{error_msg}";
                Console.WriteLine($"{error_msg}");
                //MyMessageBox.ShowDialog(error_msg);
            }
            return (error_msg.Length == 0);
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
        public async Task<string[]> GetAllColumn_NameAsync(string TableName, CancellationToken ct = default)
        {
            if (TableName == null) TableName = this.TableName;
            List<object> obj_temp_array = new List<object>();
            List<string> list_str = new List<string>();
            string Command;


            Command = string.Format("SELECT column_name FROM information_schema.columns WHERE table_schema = '{0}' AND table_name = '{1}'", this.Database, TableName);
            var connStr = _MySqlConnectionStringBuilder.ConnectionString + ";Pooling=true;";
            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync(ct).ConfigureAwait(false);

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = Command;

                    using (var reader = await cmd.ExecuteReaderAsync(ct).ConfigureAwait(false))
                    {
                        int fieldCount = reader.FieldCount;

                        while (await reader.ReadAsync(ct).ConfigureAwait(false))
                        {
                            obj_temp_array.Add(reader["COLUMN_NAME"]);

                        }
                    }
                }
                foreach (object value in obj_temp_array)
                {
                    list_str.Add((string)value);
                }
                return list_str.ToArray();
            }
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

        public bool ColumnName_Enum_IsEqual(object _enum)
        {
            string[] _enum_ary = _enum.GetEnumNames();
            string[] colName_ary = GetAllColumn_Name(null);
            if (_enum_ary.Length != colName_ary.Length) return false;
            for (int i = 0; i < _enum_ary.Length; i++)
            {
                if (_enum_ary[i] != colName_ary[i]) return false;
            }
            return true;
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
            if (TableName == null) TableName = this.TableName;
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
        public List<object[]> GetColumnValues(string TableName, string[] ColumnNames, bool Distinct)
        {
            if (TableName == null) TableName = this.TableName;

            string[] all_colNames = GetAllColumn_Name(TableName);
            List<int> list_col_index = new List<int>();
            for (int i = 0; i < ColumnNames.Length; i++)
            {
                int temp = -1;
                for (int k = 0; k < all_colNames.Length; k++)
                {
                    if(ColumnNames[i] == all_colNames[k])
                    {
                        temp = k;
                    }
                  
                }
                list_col_index.Add(temp);
            }


            List<object[]> obj_temp_array = new List<object[]>();
            List<object> obj_temp = new List<object>();
            string Command;
            Command = "SELECT ";
            if (Distinct) Command += " distinct ";
            for(int i = 0; i < ColumnNames.Length; i++)
            {
                Command += ColumnNames[i];
                if (i != ColumnNames.Length - 1) Command += ",";
            }
         
            Command += " FROM ";
            Command += TableName;
            System.Data.DataTable dataTable = new System.Data.DataTable();
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = Command;
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;
            while (reader.Read())
            {
                object[] value = new object[all_colNames.Length];
                for (int i = 0; i < ColumnNames.Length; i++)
                {
                    value[list_col_index[i]] = reader[ColumnNames[i]];
                }
                obj_temp_array.Add(value);
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
        public async Task<List<object[]>> GetAllRowsAsync(string TableName)
        {
            if (TableName.StringIsEmpty()) TableName = this.TableName;
            string db = this.Database;
            string command = $"SELECT * FROM {db}.{TableName};";
            return await WriteCommandAsync(command);
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
        public async Task<List<object[]>> GetRowsByDefultAsync(string TableName, string serchColumnName, string serchValue)
        {
            if (TableName.StringIsEmpty()) TableName = this.TableName;
            string db = this.Database;
            string command = $"SELECT * FROM {db}.{TableName} WHERE {serchColumnName} = '{serchValue}';";
            return await WriteCommandAsync(command);
        }
        public async Task<List<object[]>> GetRowsByDefultAsync(string TableName, int serchColumnindex, string serchValue)
        {
            if (TableName.StringIsEmpty()) TableName = this.TableName;
            string db = this.Database;
            object[] AllColumName = await GetAllColumn_NameAsync(TableName);
            string serchColumnName = (string)AllColumName[serchColumnindex];
            string command = $"SELECT * FROM {db}.{TableName} WHERE {serchColumnName} = '{serchValue}';";
            return await WriteCommandAsync(command);
        }
        public async Task<List<object[]>> GetRowsByDefultAsync(string TableName, int serchColumnindex, string[] serchValue)
        {
            if (TableName.StringIsEmpty()) TableName = this.TableName;
            string db = this.Database;
            object[] AllColumName = await GetAllColumn_NameAsync(TableName);
            string serchColumnName = (string)AllColumName[serchColumnindex];
            string serchValue_str = string.Join(",", serchValue.Select(x => $"'{x}'"));
            string command = $"SELECT * FROM {db}.{TableName} WHERE {serchColumnName} IN ({serchValue_str});";
            return await WriteCommandAsync(command);
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
            Command += $"{Database}.{TableName}";

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

            //Console.WriteLine($"[GetRows]Command:{Command}");
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

        public int UpdateByDefulteExtra(string TableName, object[] value)
        {
            List<object[]> Value = new List<object[]>();
            Value.Add(value);
            return UpdateByDefulteExtra(TableName, Value);
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
                    if (serchValue[0].Check_Date_String() || serchValue[1].Check_Date_String())
                    {
                        Name = "(" + serchColumnName[0] + ")";
                        //Console.WriteLine($"get rows date beteeen {serchValue[0]} : {serchValue[1]}");
                    }
                    else Name = "(" + serchColumnName[0] + ")";          
                }
                Command += Name + " >= " + "'" + serchValue[0] + "'" + " AND " + Name + "<="+ "'" + serchValue[1] + "'" ;
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
        public void WtrteCommandSafeUpdate(bool flag)
        {
            WtrteCommand($"SET SQL_SAFE_UPDATES = {(flag ? "1" : "0")};");
        }
        public void WtrteCommand(string CommandText)
        {
            if (CommandText.StringIsEmpty()) return;
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = CommandText;
            _MySqlCommand.ExecuteNonQuery();
            this.CloseConection(_MySqlConnection);
        }
        public DataTable WtrteCommandAndExecuteReader(string CommandText)
        {
            if (CommandText.StringIsEmpty()) return null;
            MySqlConnection _MySqlConnection = new MySqlConnection(_MySqlConnectionStringBuilder.ConnectionString + ";pooling=true;");
            this.OpenConnection(_MySqlConnection);
            MySqlCommand _MySqlCommand = _MySqlConnection.CreateCommand();
            _MySqlCommand.CommandText = CommandText;
      
            var reader = _MySqlCommand.ExecuteReader();
            int FieldCount = reader.FieldCount;

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            this.CloseConection(_MySqlConnection);

            return dataTable;
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
        static public string GetTimeNow_6()
        {
            return DateTime.Now.TimeOfDay.ToString().Replace(":", "").Replace("/", "").Replace(".", "");
        }
    }
    public class Table
    {

        public ColumnElement this[string colName]
        {
            get
            {
                for (int i = 0; i < columnList.Count; i++)
                {
                    if (columnList[i].Name == colName) return columnList[i];
                }
                return null;
            }
        }
        private DateTime Date = DateTime.Now;
        private string tableName = "";
        private List<ColumnElement> columnList = new List<ColumnElement>();
        [JsonPropertyName("tableName")]
        public string TableName { get => tableName; set => tableName = value; }
        [JsonPropertyName("server")]
        public string Server { get; set; }
        [JsonPropertyName("dBName")]
        public string DBName { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("port")]
        public string Port { get; set; }
        [JsonPropertyName("columnList")]
        public List<ColumnElement> ColumnList { get => columnList; set => columnList = value; }
        public class ColumnElement
        {
            public override string ToString()
            {
                return $"名稱 : {Name} ,類別 : {TypeName} , 索引 : {IndexType.ToString()}";
            }

            private string name;
            private string typeName;
            private IndexType indexType = IndexType.None;
            private int num = 50;

            [JsonPropertyName("name")]
            public string Name { get => name; set => name = value; }
            [JsonPropertyName("typeName")]
            public string TypeName { get => typeName; set => typeName = value; }
            [JsonPropertyName("indexType")]
            public IndexType IndexType { get => indexType; set => indexType = value; }
            [JsonIgnore]
            public Table.OtherType OtherType
            {
                get
                {
                    string temp = TypeName;
                    int index_of = temp.IndexOf('(');

                    if (index_of != -1)
                    {
                        temp = temp.Remove(index_of, temp.Length - index_of);
                    }
                    temp = temp.Trim();

                    string[] ary = new Table.OtherType().GetEnumNames();
                    for (int i = 0; i < ary.Length; i++)
                    {
                        if (temp == ary[i])
                        {
                            return (OtherType)i;
                        }
                    }
                    return OtherType.None;
                }
            }
            [JsonIgnore]
            public Table.ValueType ValueType
            {
                get
                {
                    string temp = TypeName;
                    int index_of = temp.IndexOf('(');

                    int num = 50;
                    if (index_of != -1)
                    {
                        string str_num = temp.Substring(index_of, temp.Length - index_of);
                        str_num = str_num.Replace("(", "");
                        str_num = str_num.Replace(")", "");
                        num = str_num.StringToInt32();
                        this.Num = num;
                    }
                    temp = temp.Trim();
                    string[] ary = new Table.ValueType().GetEnumNames();
                    for (int i = 0; i < ary.Length; i++)
                    {
                        if (temp == ary[i])
                        {
                            return (ValueType)i;
                        }
                    }
                    return ValueType.None;
                }
            }
            [JsonIgnore]
            public Table.StringType StringType
            {
                get
                {
                    string temp = TypeName;
                    int index_of = temp.IndexOf('(');
                    if (index_of != -1)
                    {
                        temp = temp.Remove(index_of, temp.Length - index_of);
                    }
                    temp = temp.Trim();
                    string[] ary = new Table.StringType().GetEnumNames();
                    for (int i = 0; i < ary.Length; i++)
                    {
                        if (temp == ary[i])
                        {
                            return (StringType)i;
                        }
                    }
                    return StringType.None;
                }
            }
            [JsonIgnore]
            public Table.DateType DateType
            {
                get
                {
                    string temp = TypeName;
                    int index_of = temp.IndexOf('(');
                    if (index_of != -1)
                    {
                        temp = temp.Remove(index_of, temp.Length - index_of);
                    }
                    temp = temp.Trim();
                    string[] ary = new Table.DateType().GetEnumNames();
                    for (int i = 0; i < ary.Length; i++)
                    {
                        if (temp == ary[i])
                        {
                            return (DateType)i;
                        }
                    }
                    return DateType.None;
                }
            }
            [JsonIgnore]
            public int Num { get => num; set => num = value; }
            [JsonIgnore]
            public List<string> EnumAry
            {
                get
                {
                    string temp = TypeName;
                    return ExtractEnumValues(temp);
                }
            }

            static List<string> ExtractEnumValues(string enumString)
            {
                List<string> enumValues = new List<string>();
                string pattern = @"'([^']*)'";

                MatchCollection matches = Regex.Matches(enumString, pattern);

                foreach (Match match in matches)
                {
                    enumValues.Add(match.Groups[1].Value);
                }

                return enumValues;
            }
        }
        [JsonConstructor]
        public Table(string tableName, List<ColumnElement> columnList)
        {
            TableName = tableName;
            ColumnList = columnList;
        }
        public Table(Enum enum_type)
        {

            TableName = enum_type.GetEnumDescription();
            AddColumnList(enum_type);
        }
        public void SetTableConfig(SQLControl sQLControl)
        {
            this.TableName = sQLControl.TableName;
            this.Server = sQLControl.Server;
            this.DBName = sQLControl.Database;
            this.Username = sQLControl.UserID;
            this.Password = sQLControl.Password;
            this.Port = sQLControl.Port.ToString();

        }





        public enum IndexType
        {
            None = 0,
            INDEX = 1,
            PRIMARY = 2,
            UNIQUE = 3,
        }
        public enum OtherType
        {
            ENUM = 0,
            SET = 1,
            IMAGE = 2,
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
            LONGBLOB = 6,
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
            for (int i = 0; i < ColumnList.Count; i++)
            {
                if (ColumnList[i].Name == ColumnName)
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
                for (int i = 0; i < stringArray.Length; i++)
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

            if (_ValueType == ValueType.TINYINT)
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
                str = " TEXT";
            }
            else if (_StringType == StringType.LONGTEXT)
            {
                str = " LONGTEXT";
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
        public void AddColumnList(object Enum)
        {
            ValueType valueType = ValueType.None;
            StringType stringType = StringType.None;
            DateType dateType = DateType.None;
            OtherType otherType = OtherType.None;
            IndexType indexType = IndexType.None;
            string[] descriptions = Enum.GetEnumDescriptions();
            for (int i = 0; i < descriptions.Length; i++)
            {
                valueType = ValueType.None;
                stringType = StringType.None;
                dateType = DateType.None;
                otherType = OtherType.None;
                indexType = IndexType.None;
                string[] temp_ary = descriptions[i].Split(',');
                string[] type_names;
                if (temp_ary.Length == 4)
                {
                    string name = temp_ary[0];
                    string valtype = temp_ary[1];
                    int len = temp_ary[2].StringToInt32();
                    string indextype = temp_ary[3];
                    if (name.StringIsEmpty()) continue;
                    if (valtype.StringIsEmpty()) continue;
                    if (indextype.StringIsEmpty()) continue;
                    if (len <= 0) continue;
                    type_names = new ValueType().GetEnumNames();
                    for (int k = 0; k < type_names.Length; k++)
                    {
                        if (type_names[k] == valtype)
                        {
                            valueType = (ValueType)k;
                        }
                    }
                    type_names = new StringType().GetEnumNames();
                    for (int k = 0; k < type_names.Length; k++)
                    {
                        if (type_names[k] == valtype)
                        {
                            stringType = (StringType)k;
                        }
                    }
                    type_names = new DateType().GetEnumNames();
                    for (int k = 0; k < type_names.Length; k++)
                    {
                        if (type_names[k] == valtype)
                        {
                            dateType = (DateType)k;
                        }
                    }
                    type_names = new OtherType().GetEnumNames();
                    for (int k = 0; k < type_names.Length; k++)
                    {
                        if (type_names[k] == valtype)
                        {
                            otherType = (OtherType)k;
                        }
                    }
                    type_names = new IndexType().GetEnumNames();
                    for (int k = 0; k < type_names.Length; k++)
                    {
                        if (type_names[k] == indextype)
                        {
                            indexType = (IndexType)k;
                        }
                    }
                    bool flag_continue = false;

                    if (valueType != ValueType.None)
                    {
                        AddColumnList(name, valueType, (uint)len, indexType);
                    }
                    if (stringType != StringType.None)
                    {
                        AddColumnList(name, stringType, (uint)len, indexType);
                    }
                    if (dateType != DateType.None)
                    {
                        AddColumnList(name, dateType, (uint)len, indexType);
                    }
                    if (otherType != OtherType.None)
                    {
                        AddColumnList(name, otherType, indexType);
                    }
                    if (flag_continue)
                    {
                        this.ColumnList = null;
                        return;
                        continue;
                    }


                }
            }

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
    public static class TableMethod
    {
        public static Table GetTable(this List<Table> tables , Enum _enum)
        {
            string name = _enum.GetEnumDescription();
            for(int i = 0; i < tables.Count; i++)
            {
                if (tables[i].TableName == name) return tables[i];
            }
            return null;
        }
    }

}
