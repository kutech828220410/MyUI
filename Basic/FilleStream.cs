﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
namespace Basic
{
    public class FileInfoModel
    {
        public string FileName { get; set; } // 檔案名稱
        public string FullPath { get; set; } // 完整路徑
        public DateTime LastModified { get; set; } // 修改日期
        public long FileSize { get; set; } // 檔案大小（以位元組為單位）


    }
    static public class FileIO
    {
        /// <summary>
        /// 從提供的檔案清單中篩選檔名包含特定文字的檔案
        /// </summary>
        /// <param name="fileList">檔案清單</param>
        /// <param name="keyword">檔名中包含的文字</param>
        /// <returns>符合條件的檔案清單</returns>
        public static List<FileInfoModel> FilterByFileName(this List<FileInfoModel> fileList, string keyword)
        {
            if (fileList == null || fileList.Count == 0 || string.IsNullOrWhiteSpace(keyword))
            {
                return new List<FileInfoModel>();
            }

            return fileList.Where(file =>
                file.FileName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
            ).ToList();
        }
        /// <summary>
        /// 從提供的檔案清單中篩選指定日期範圍內的檔案
        /// </summary>
        /// <param name="fileList">檔案資訊列表</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <returns>符合日期範圍的檔案資訊列表</returns>
        public static List<FileInfoModel> FilterByDateRange(this List<FileInfoModel> fileList, DateTime startDate, DateTime endDate)
        {
            if (fileList == null || fileList.Count == 0)
            {
                return new List<FileInfoModel>(); // 如果輸入列表為空，直接返回空列表
            }

            // 篩選符合日期範圍的檔案
            return fileList.Where(file => file.LastModified >= startDate && file.LastModified <= endDate).ToList();
        }
        public static List<FileInfoModel> GetFilesInfo(string folderPath)
        {
            var fileList = new List<FileInfoModel>();

            try
            {
                // 檢查資料夾是否存在
                if (!Directory.Exists(folderPath))
                {
                    throw new DirectoryNotFoundException($"資料夾不存在: {folderPath}");
                }

                // 取得資料夾內所有檔案資訊
                string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);

                Parallel.ForEach(Directory.EnumerateFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly), file =>
                {
                    var fileInfo = new FileInfo(file);
                    lock (fileList) // 確保多線程安全
                    {
                        fileList.Add(new FileInfoModel
                        {
                            FileName = fileInfo.Name,
                            FullPath = fileInfo.FullName,
                            LastModified = fileInfo.LastWriteTime,
                            FileSize = fileInfo.Length
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"發生錯誤: {ex.Message}");
            }

            return fileList;
        }
        /// <summary>
        /// 獲取指定資料夾中包含特定文字的檔案資訊
        /// </summary>
        /// <param name="folderPath">資料夾路徑</param>
        /// <param name="filterKeyword">過濾檔名的文字，忽略大小寫</param>
        /// <returns>符合條件的檔案資訊列表</returns>
        public static List<FileInfoModel> GetFilesInfo(string folderPath, string filterKeyword)
        {
            var fileList = new List<FileInfoModel>();

            try
            {
                // 檢查資料夾是否存在
                if (!Directory.Exists(folderPath))
                {
                    throw new DirectoryNotFoundException($"資料夾不存在: {folderPath}");
                }

                // 使用 EnumerateFiles 獲取檔案
                var files = Directory.EnumerateFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);

                // 過濾包含指定文字的檔案（忽略大小寫）
                var filteredFiles = files.Where(file =>
                    Path.GetFileName(file).IndexOf(filterKeyword, StringComparison.OrdinalIgnoreCase) >= 0);

                // 逐步處理檔案資訊
                Parallel.ForEach(filteredFiles, file =>
                {
                    var fileInfo = new FileInfo(file);
                    lock (fileList) // 確保多線程安全
                    {
                        fileList.Add(new FileInfoModel
                        {
                            FileName = fileInfo.Name,
                            FullPath = fileInfo.FullName,
                            LastModified = fileInfo.LastWriteTime,
                            FileSize = fileInfo.Length
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"發生錯誤: {ex.Message}");
            }

            return fileList;
        }
        static public byte[] LoadFileStream(string filename)
        {
            List<byte> bytes = new List<byte>();
            IFormatter binFmt = new BinaryFormatter();
            Stream stream = null;
            MemoryStream memoryStream = null;
            try
            {
                if (File.Exists(@filename))
                {
                    stream = File.Open(@filename, FileMode.Open);
                    for (int i = 0; i < stream.Length; i++)
                    {
                        bytes.Add((byte)stream.ReadByte());
                    }
                }

            }
            finally
            {
                if (stream != null) stream.Close();
                if (memoryStream != null) memoryStream.Close();
            }
            return bytes.ToArray();
        }
        static public bool SaveFileStream(this byte[] bytes ,string filename)
        {
            try
            {
                IFormatter binFmt = new BinaryFormatter();
                Stream stream = null;


                try
                {
                    stream = File.Open(@filename, FileMode.Create);
                    for (int i = 0; i < bytes.Length; i++) stream.WriteByte(bytes[i]);
                }
                finally
                {
                    if (stream != null) stream.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }


        static public  byte[] Serialize(object data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream rems = new MemoryStream();
            formatter.Serialize(rems, data);
            return rems.GetBuffer();
        }
        static public object Deserialize(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream rems = new MemoryStream(data);
            data = null;
            return formatter.Deserialize(rems);
        }
        static public void SaveProperties(Object savePropertyFile, String StreamName)
        {
            IFormatter binFmt = new BinaryFormatter();
            Stream stream = null;
            try
            {
                FileInfo fi = new FileInfo(StreamName);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }

                stream = File.Open(StreamName, FileMode.Create);
                binFmt.Serialize(stream, savePropertyFile);
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }
        static public bool LoadProperties(ref Object PropertyFile, String StreamName)
        {
            IFormatter binFmt = new BinaryFormatter();
            Stream stream = null;
            try
            {
                if (File.Exists(".\\" + StreamName))
                {
                    stream = File.Open(".\\" + StreamName, FileMode.Open);
                    try { PropertyFile = (Object)binFmt.Deserialize(stream); }
                    catch { return false; }

                }
                else if (File.Exists(StreamName))
                {
                    stream = File.Open(StreamName, FileMode.Open);
                    try { PropertyFile = (Object)binFmt.Deserialize(stream); }
                    catch { return false; }
                }

            }
            catch
            {

            }
            finally
            {
                if (stream != null) stream.Close();
            }
            return true;
        }
        static public bool CopyFile(string sourceFile, string destFile)
        {
            try
            {
                System.IO.File.Copy(@sourceFile, @destFile, true);
            }
            catch
            {
                return false;
            }
            return true;

        }
        static public bool Connect(string remoteHost, string userName, string passWord)
        {

            bool Flag = true;
            Process proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;

            try
            {

                proc.Start();
                string command = @"net  use  \\" + remoteHost + "  " + passWord + "  " + "  /user:" + userName + ">NUL";
                proc.StandardInput.WriteLine(command);
                command = "exit";
                proc.StandardInput.WriteLine(command);

                while (proc.HasExited == false)
                {
                    proc.WaitForExit(1000);
                }

                string errormsg = proc.StandardError.ReadToEnd();
                if (errormsg != "")
                    Flag = false;

                proc.StandardError.Close();
            }
            catch (Exception ex)
            {
                Flag = false;

            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }
        static public bool DisConnect(string remoteHost)
        {

            bool Flag = true;
            Process proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;

            try
            {

                proc.Start();
                string command = @"net  use  \\" + remoteHost + " /delete";
                proc.StandardInput.WriteLine(command);
                command = "exit";
                proc.StandardInput.WriteLine(command);

                while (proc.HasExited == false)
                {
                    proc.WaitForExit(1000);
                }

                string errormsg = proc.StandardError.ReadToEnd();
                if (errormsg != "")
                    Flag = false;

                proc.StandardError.Close();
            }
            catch (Exception ex)
            {
                Flag = false;

            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }
        static public bool CopyToServer(string serverfilepath, string serverfilename, string desfilepath, string desfilename, string username, string password)
        {
            try
            {
                bool bln = false;

                bln = Connect(serverfilepath, username, password);

                File.Copy(desfilepath + "\\" + desfilename, "\\\\" + serverfilepath + "\\" + serverfilename, true);

                bln = DisConnect(serverfilepath);
                return bln;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        static public bool ServerFileCopy(string filepath, string filename, string desfilepath, string desfilename, string username, string password)
        {
            try
            {
                bool bln = false;

                bln = Connect(filepath, username, password);

                if (filename.Length > 0)
                {
                    File.Copy("\\\\" + filepath + "\\" + filename, desfilepath + "\\" + desfilename, true);
                }
                else
                {                    
                    DirectoryInfo source = new DirectoryInfo(filepath);
                    FileInfo[] finfo = source.GetFiles();

                    foreach (FileInfo f in finfo)
                    {
                        File.Copy(f.FullName, desfilepath + "\\" + desfilename, true);
                    }
                }

                bln = DisConnect(filepath);
                return bln;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        static public DateTime GetLastWriteTime(string filepath, string filename, string username, string password)
        {
            try
            {
                DateTime dateTime;
                bool bln = false;
                bln = Connect(filepath, username, password);
                dateTime =  File.GetLastWriteTime($"{filepath}\\{filename}");
                bln = DisConnect(filepath);
                return dateTime;

            }
            catch
            {
                return DateTime.MinValue;
            }
          
        }
        static public DateTime GetCreationTime(string filepath, string filename, string username, string password)
        {
            try
            {
                DateTime dateTime;
                bool bln = false;
                bln = Connect(filepath, username, password);
                dateTime = File.GetCreationTime($"{filepath}\\{filename}");
                bln = DisConnect(filepath);
                return dateTime;

            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        static public string GetFileName(this string str)
        {
            return System.IO.Path.GetFileName(str);
        }
        static public string GetFilePath(this string str)
        {
            return $@"{System.IO.Path.GetDirectoryName(str)}\";
        }


        public static byte[] ReadFileFromNetworkDrive(string fullFilePath, string username, string password)
        {
            try
            {
                // 提取網路目錄部分
                string networkPath = Path.GetDirectoryName(fullFilePath);

                // 連接到網路磁碟機
                ConnectToNetworkDrive(networkPath, username, password);



                // 讀取檔案內容
                using (FileStream fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] fileBytes = new byte[fs.Length];
                    fs.Read(fileBytes, 0, (int)fs.Length);
                    return fileBytes;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"讀取檔案失敗: {ex.Message}");
                throw;
            }
            finally
            {
                // 斷開網路磁碟機
                string networkPath = Path.GetDirectoryName(fullFilePath);
                DisconnectFromNetworkDrive(networkPath);
            }
        }

        private static void ConnectToNetworkDrive(string networkPath, string username, string password)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("net", $"use {networkPath} /user:{username} {password}")
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        string errorMessage = process.StandardError.ReadToEnd();
                        throw new Exception($"連接網路磁碟失敗: {errorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"連接網路磁碟時發生錯誤: {ex.Message}");
            }
        }

        private static void DisconnectFromNetworkDrive(string networkPath)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("net", $"use {networkPath} /delete")
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        string errorMessage = process.StandardError.ReadToEnd();
                        Console.WriteLine($"斷開網路磁碟失敗: {errorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"斷開網路磁碟時發生錯誤: {ex.Message}");
            }
        }
    }
    static public class MyFileStream
    {
        static public  bool IsBig5Encoding(byte[] bytes)
        {
            Encoding big5 = Encoding.GetEncoding(950);
            return (bytes.Length == big5.GetByteCount(big5.GetString(bytes)));
        }
        static public  bool IsBig5Encoding(string file)
        {
            if (!File.Exists(file)) return false;
            return IsBig5Encoding(File.ReadAllBytes(file));
        }
        static public bool 刪除上限檔案(String FilePlace, String FileName, String 副檔名, int MaxNumFile)
        {
            bool FLAG = false;
            while (true)
            {
                DirectoryInfo di = new DirectoryInfo(@FilePlace);
                FileInfo[] info = di.GetFiles("*" + FileName + "*." + 副檔名);
                int NumOfFile = info.Length;
                if (NumOfFile >= MaxNumFile * 2)
                {
                    try
                    {
                        info[0].Delete();
                        info[1].Delete();
                    }
                    catch
                    {
                        break;
                    }

                }
                else { break; }
            }


            return FLAG;
        }
        static public void 檔案更名(String FilePlace, string fileName0, string fileName1)
        {
            File.Delete(FilePlace + fileName1);
            try
            {
                File.Copy(FilePlace + fileName0, FilePlace + fileName1);
            }
            catch
            {
                return;
            }
            File.Delete(FilePlace + fileName0);
        }
        static public string 儲存檔案名稱檢查(String FilePlace, String 名稱, String 副檔名)
        {
            Directory.CreateDirectory(FilePlace);
            DirectoryInfo di = new DirectoryInfo(@FilePlace);
            FileInfo[] info = di.GetFiles("*" + 名稱 + "." + 副檔名);
            int NumOfFile = info.Length;
            string 儲存位置 = FilePlace + @"\" + 名稱 + (info.Length + 1).ToString() + "." + 副檔名;
            return 儲存位置;
        }
        static public bool IsFilePathExists(string FilePath)
        {
            return Directory.Exists(FilePath);
        }
        static public bool IsFileNameExists(string FullFileName)
        {
            return File.Exists(FullFileName);
        }
        static public void CreatFilePath(string FilePath)
        {
            Directory.CreateDirectory(FilePath);
        }
        static public void SetFileAttributes(string FullFileName, FileAttributes FileAttributes)
        {
            System.IO.FileInfo batchItemAttribute = new FileInfo(FullFileName);
            batchItemAttribute.Attributes = FileAttributes;
        }
        static public T DeepClone<T>(this T item)
        {
            if (item != null)
            {
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, item);
                    stream.Seek(0, SeekOrigin.Begin);
                    var result = (T)formatter.Deserialize(stream);
                    return result;
                }
            }

            return default(T);
        }
        static public List<string> LoadFile(string filename)
        {
            List<string> list_read = new List<string>();
            if (!IsFileNameExists(filename)) return null;
            string Endcoding = "Unicode";
            if (IsBig5Encoding(filename)) Endcoding = "BIG5";
            list_read = LoadFile(filename, Endcoding);
            return list_read;
        }
        static public List<string> LoadFile(string filename, string endcoding)
        {
            List<string> list_read = new List<string>();
            if (!IsFileNameExists(filename)) return null;
            try
            {
                StreamReader reader = new StreamReader(filename, System.Text.Encoding.GetEncoding(endcoding), false);
                while (reader.Peek() > 0)
                {

                    list_read.Add(reader.ReadLine());
                }
                reader.Close();
                reader.Dispose();
            }
            catch
            {
                return null;
            }
          
            return list_read;
        }
        public static string ToSafeFileName(this string s)
        {
            return s
                .Replace("\\", "")
                .Replace("/", "")
                .Replace("\"", "")
                .Replace("*", "")
                .Replace(":", "")
                .Replace("?", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "");
        }
        static public string LoadFileAllText(string filename)
        {
            if (!IsFileNameExists(filename)) return "";
            string Endcoding = "Unicode";
            if (IsBig5Encoding(filename)) Endcoding = "BIG5";
            return LoadFileAllText(filename, Endcoding);
        }
        static public string LoadFileAllText(string filename, string endcoding)
        {
            string text = "";
            if (!IsFileNameExists(filename)) return "";
            try
            {
                StreamReader reader = new StreamReader(filename, System.Text.Encoding.GetEncoding(endcoding), false);
                while (reader.Peek() > 0)
                {
                    text = reader.ReadToEnd();
                }
                reader.Close();
                reader.Dispose();
            }
            catch
            {
                return "";
            }

            return text;

        }
        static public bool SaveFile(string filename, string text)
        {
            List<string> list_value = new List<string>();
            list_value.Add(text);
            return SaveFile(filename, list_value);
        }
        static public bool SaveFile(string filename, List<string> list_value , string endcoding)
        {
            try
            {
                FileStream fs = new FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding(endcoding));
                foreach (string value in list_value)
                {
                    sw.WriteLine(value);
                }
                sw.Close();
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
        static public bool SaveFile(string filename, List<string> list_value)
        {           
            try
            {
                FileStream fs = new FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("BIG5"));
                foreach (string value in list_value)
                {
                    sw.WriteLine(value);
                }
                sw.Close();
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
        static public DataTable ReorderTable(this DataTable dt, object Enum)
        {
            string[] colnames = Enum.GetEnumNames();
            return ReorderTable(dt, colnames);
        }
        static public DataTable ReorderTable(this DataTable dt, params string[] colnames)
        {
         
            bool flag_OK = false;
            string error_msg = "";
            for (int k = 0; k < colnames.Length; k++)
            {
                flag_OK = false;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (colnames[k] == dt.Columns[i].ToString())
                    {
                        flag_OK = true;
                    }
                }
                if (!flag_OK)
                {
                    error_msg += string.Format("未找到 {0} 欄位!\r\n", colnames[k]);
                }

            }
            //if (error_msg != "")
            //{
            //    Console.WriteLine(error_msg);
            //    return null;
            //}
            DataTable dataTable = new DataTable();
            for (int k = 0; k < colnames.Length; k++)
            {
                dataTable.Columns.Add(colnames[k]);
            }
            List<object> list_obj = new List<object>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list_obj.Clear();
                for (int k = 0; k < colnames.Length; k++)
                {
                    if (dt.Columns.Contains(colnames[k]))
                    {
                        list_obj.Add(dt.Rows[i][colnames[k]]);
                    }
                    else
                    {
                        list_obj.Add("");
                    }
                }
                dataTable.Rows.Add(list_obj.ToArray());
            }
            return dataTable;
        }
        static public List<object[]> DataTableToRowList(this DataTable dt)
        {
            List<object[]> list_value = new List<object[]>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list_value.Add(dt.Rows[i].ItemArray);
            }
            return list_value;
        }
        static public DataTable ToDataTable(this List<object[]> list_value, Enum Enum)
        {
            string[] colnames = Enum.GetEnumNames();

            DataTable dataTable = new DataTable();
            for (int k = 0; k < colnames.Length; k++)
            {
                dataTable.Columns.Add(colnames[k]);
            }
            for (int i = 0; i < list_value.Count; i++)
            {
                dataTable.Rows.Add(list_value[i].ToArray());
            }
            return dataTable;
        }
        static public DataTable ToDataTable(this List<object[]> list_value, string[] colnames)
        {
            DataTable dataTable = new DataTable();
            for (int k = 0; k < colnames.Length; k++)
            {
                dataTable.Columns.Add(colnames[k]);
            }
            for (int i = 0; i < list_value.Count; i++)
            {
                dataTable.Rows.Add(list_value[i].ToArray());
            }
            return dataTable;
        }
        static public DataTable ColumnRename(this DataTable dt, string CurrentColumnName ,string NewColumnName)
        {
            DataColumn column = dt.Columns[CurrentColumnName];
            // 更新欄位名稱
            if (column != null)
            {
                column.ColumnName = NewColumnName;
            }
            return dt;
        }
        static public DataTable RemoveColumn(this DataTable dt, Enum EnumColumnName)
        {
            dt = RemoveColumn(dt, EnumColumnName.GetEnumName());
            return dt;
        }
        static public DataTable RemoveColumn(this DataTable dt, string ColumnName)
        {
            dt.Columns.Remove(ColumnName);
            return dt;
        }
        static public int GetColumnIndex(this DataTable dt, string col_Name)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if(dt.Columns[i].ColumnName == col_Name)
                {
                    return i;
                }
            }
            return -1;
        }


        internal enum MoveFileFlags
        {
            MOVEFILE_REPLACE_EXISTING = 1,
            MOVEFILE_COPY_ALLOWED = 2,
            MOVEFILE_DELAY_UNTIL_REBOOT = 4,
            MOVEFILE_WRITE_THROUGH = 8
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);
        static public void RunFile(string sourceFolderPath, string updateFolderPath, string tempFolderPath, string StartFileName, SearchOption searchOption = SearchOption.TopDirectoryOnly, params string[] exclude_filename)
        {
            try
            {
                CopyFolder(sourceFolderPath, updateFolderPath, tempFolderPath, searchOption, exclude_filename);


                //清除垃圾舊檔
                Process P_sources = new Process();
                //設定一秒延遲,讓程式順利關閉才能刪除 
                P_sources.StartInfo = new ProcessStartInfo("cmd.exe", $"/C choice /C Y /N /D Y /T 3 & rmdir /S /Q \"{tempFolderPath}\"");
                P_sources.StartInfo.CreateNoWindow = true;
                P_sources.StartInfo.UseShellExecute = false;
                P_sources.Start();

                Process P_new = new Process();
                //設定一秒延遲,讓程式順利關閉才能刪除 
                P_new.StartInfo = new ProcessStartInfo("cmd.exe", "/C choice /C Y /N /D Y /T 3 & " + "\"" + $"{sourceFolderPath}\\{StartFileName}" + "\"");
                P_new.StartInfo.CreateNoWindow = true;
                P_new.StartInfo.UseShellExecute = false;
                P_new.Start();

                Process.GetCurrentProcess().Close();
                Process.GetCurrentProcess().CloseMainWindow();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred during program upgrade: " + ex.Message);
            }
        }
        static public void CopyFolder(string sourceFolderPath, string updateFolderPath, string tempFolderPath, SearchOption searchOption = SearchOption.TopDirectoryOnly, params string[] exclude_filename)
        {
            // 创建目标文件夹
            Directory.CreateDirectory(sourceFolderPath);
            Directory.CreateDirectory(updateFolderPath);
            Directory.CreateDirectory(tempFolderPath);

            // 获取源文件夹中的所有文件和子文件夹
            string[] source_files = Directory.GetFiles(sourceFolderPath, "*", SearchOption.TopDirectoryOnly);
            // 移動每个文件到目标文件夹
            foreach (string file in source_files)
            {
                string filename = Path.GetFileName(file);
                bool flag_move = true;
                for (int i = 0; i < exclude_filename.Length; i++)
                {
                    if (exclude_filename[i] == filename)
                    {
                        flag_move = false;
                        break;
                    }
                }
                string targetFilePath = $"{tempFolderPath}\\{filename}";
                if (flag_move) MoveFileEx(file, targetFilePath, MoveFileFlags.MOVEFILE_REPLACE_EXISTING);
            }
            string[] update_files = Directory.GetFiles(updateFolderPath, "*", SearchOption.TopDirectoryOnly);
            // 复制每个文件到目标文件夹
            foreach (string file in update_files)
            {
                string filename = Path.GetFileName(file);
                string targetFilePath = $"{sourceFolderPath}\\{filename}";
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(targetFilePath));
                System.IO.File.Copy(file, targetFilePath, true);
            }
        }

    }
    static public class CSVHelper
    {

        static public  bool IsBig5Encoding(byte[] bytes)
        {
            Encoding big5 = Encoding.GetEncoding(950);
            return (bytes.Length == big5.GetByteCount(big5.GetString(bytes)));
        }
        static public  bool IsBig5Encoding(string file)
        {
            if (!File.Exists(file)) return false;
            return IsBig5Encoding(File.ReadAllBytes(file));
        }

        static public DataTable LoadFile(string FullFilePath, char SplitChar, int TitleNum, DataTable dt)
        {
            string Endcoding = "Unicode";
            if (IsBig5Encoding(FullFilePath)) Endcoding = "BIG5";
            return LoadFile(FullFilePath, SplitChar, Endcoding, 0, dt);
        }
        static public DataTable LoadFile(string FullFilePath, string Encoding, int TitleNum, DataTable dt)
        {
            return LoadFile(FullFilePath, '\t', Encoding, 0, dt);
        }
        static public DataTable LoadFile(string FullFilePath, int TitleNum, DataTable dt)
        {
            string Endcoding = "Unicode";
            if (IsBig5Encoding(FullFilePath)) Endcoding = "BIG5";
            return LoadFile(FullFilePath, '\t', Endcoding, TitleNum, dt);
        }
        static public DataTable LoadFile(string FullFilePath, char SplitChar, string Encoding, int TitleNum, DataTable dt) //這個dt 是個空白的沒有任何行列的DataTable
        {
            try
            {
                int i = 0, m = 0;
                StreamReader reader = new StreamReader(FullFilePath, System.Text.Encoding.GetEncoding(Encoding), false);
                if (TitleNum == -1)
                {
                    reader.Peek();
                    bool Init = false;
                    while (reader.Peek() > 0)
                    {
                        string str = reader.ReadLine();
                        str = str.Replace("\"", "");
                        string[] array = str.Split(SplitChar);
                        for (int k = 0; k < array.Length; k++)
                        {
                            array[k] = array[k].Trim();
                            if (!Init)
                            {
                                dt.Columns.Add(k.ToString());
                            }
                        }
                        Init = true;

                        i = 0;
                        System.Data.DataRow dr = dt.NewRow();
                        foreach (string mc in array)
                        {
                            if (i < dr.ItemArray.Length) dr[i] = @mc;
                            i++;
                        }
                        dt.Rows.Add(dr);  //DataTable 增加一行 
                    }
                }
                else
                {
                    reader.Peek();
                    while (reader.Peek() > 0)
                    {
                        m++;
                        string str = reader.ReadLine();
                        str = str.Replace("\"", "");
                        if (m >= TitleNum + 1)
                        {
                            if (m == TitleNum + 1) //如果是欄位行，則自動加入欄位。
                            {
                                string[] array = str.Split(SplitChar);
                                foreach (string mc in array)
                                {
                                    dt.Columns.Add(mc); //增加列標題
                                }

                            }
                            else
                            {
                                string[] array = str.Split(SplitChar);
                                for (int k = 0; k < array.Length; k++)
                                {
                                    array[k] = array[k].Trim();
                                }
                                i = 0;
                                System.Data.DataRow dr = dt.NewRow();
                                foreach (string mc in array)
                                {
                                    if (i < dr.ItemArray.Length) dr[i] = mc;
                                    i++;
                                }
                                dt.Rows.Add(dr);  //DataTable 增加一行     
                            }

                        }
                    }
                }

                reader.Dispose();
                return dt;
            }
            catch
            {
                return null;
            }
        }


        static public bool SaveFile(DataTable dt, string FullFilePath, char SplitChar)
        {
            return SaveFile(dt, FullFilePath, SplitChar, "UTF-8");
        }
        static public bool SaveFile(DataTable dt, string FullFilePath, string Encoding)
        {
            return SaveFile(dt, FullFilePath, '\t', Encoding);
        }
        static public bool SaveFile(DataTable dt, string FullFilePath)
        {
            return SaveFile(dt, FullFilePath, '\t', "BIG5");
        }
        static public bool SaveFile(DataTable dt, string FullFilePath, char SplitChar, string Encoding)
        {
            try
            {
                FileInfo fi = new FileInfo(FullFilePath);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                FileStream fs = new FileStream(FullFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding(Encoding));
                string data = "";
                //寫出列名稱
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    data += "" + dt.Columns[i].ColumnName.ToString() + "";
                    if (i < dt.Columns.Count - 1)
                    {
                        data += SplitChar;
                    }
                }
                sw.WriteLine(data);
                //寫出各行數據
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string str = dt.Rows[i][j].ToString();
                        str = string.Format("{0}", str);
                        data += str;
                        if (j < dt.Columns.Count - 1)
                        {
                            data += SplitChar;
                        }
                    }
                    sw.WriteLine(@data);
                }
                sw.Close();
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
