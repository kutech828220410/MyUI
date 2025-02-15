﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Xml;
using System.Net.WebSockets;
//using Newtonsoft.Json;
namespace Basic
{
    static public class Net
    {
        public static bool DebugLog = false;
        public class SocketClient
        {
            static public bool ConsoleWrite = false;
            private ClientWebSocket clientWebSocket;
            private Uri serverUri
            {
                get
                {
                    return new Uri(webSocketUrl);
                }
            }
            private string _url = "";
            private string webSocketUrl
            {
                get
                {
                    return $@"ws://{_url}";
                }
            }
            public SocketClient(string url)
            {
                _url = url;           
                clientWebSocket = new ClientWebSocket();
                clientWebSocket.Options.Proxy = null;
                Open();
            }
            public bool Open()
            {
                try
                {
                    Task.Run(async () =>
                    {
                        await OpenAsync();
                        return;
                    }).Wait();
                    return true;
                }
                catch
                {
                    return false;
                }
              

            }
            public async Task OpenAsync()
            {
                try
                {
                    if (clientWebSocket == null) return;
                    await clientWebSocket.ConnectAsync(serverUri, System.Threading.CancellationToken.None);
                }
                catch(Exception e)
                {
                    if (ConsoleWrite) Console.WriteLine($"open clientWebSocket error! {e.Message}");
                }
                finally
                {
                    if (ConsoleWrite) Console.WriteLine($"open clientWebSocket finally! state : {clientWebSocket.State} [{_url}]");
                }
             
            }
            public bool Close()
            {
                try
                {
                    Task.Run(async () =>
                    {
                        await CloseAsync();
                        return;
                    }).Wait();
                    return true;
                }
                catch
                {
                    return false;
                }


            }
            public async Task CloseAsync()
            {
                try
                {
                    if (clientWebSocket == null) return;
                    await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing connection", System.Threading.CancellationToken.None);
                    clientWebSocket.Dispose();
                    clientWebSocket = null;
                }
                catch (Exception e)
                {
                    if (ConsoleWrite) Console.WriteLine($"close clientWebSocket error! {e.Message}");
                }
                finally
                {
                    if (ConsoleWrite) Console.WriteLine($"close clientWebSocket finally! state : {clientWebSocket.State} [{_url}]");
                }
            }

            public string PostJson(string jsonString)
            {
                string result = Task.Run(async () =>
                {
                    string responseBody = await PostJsonAsync(jsonString);
                    return responseBody;
                }).Result;
                return result;
            }
            public async Task<string> PostJsonAsync(string jsonString)
            {
                string str = "";
                if (clientWebSocket.State != WebSocketState.Open)
                {
                    await clientWebSocket.ConnectAsync(serverUri, System.Threading.CancellationToken.None);
                }
                if (clientWebSocket.State == WebSocketState.Open)
                {
                    ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(jsonString));
                    var result = new byte[4096];
                    try
                    {
                        await clientWebSocket.SendAsync(bytesToSend, WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);
                        if (ConsoleWrite) Console.WriteLine($"send clientWebSocket sucess! message : {jsonString} [{_url}]");
                    }
                    catch(Exception e)
                    {
                        if (ConsoleWrite) Console.WriteLine($"send clientWebSocket error! {e.Message}");
                        return "";
                    }
                    try
                    {
                        await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(result), new System.Threading.CancellationToken());//接受数据
                        var lastbyte = ByteCut(result, 0x00);
                        str = Encoding.UTF8.GetString(lastbyte, 0, lastbyte.Length);
                        if (ConsoleWrite) Console.WriteLine($"receive clientWebSocket sucess! message : {str} [{_url}]");
                    }
                    catch (Exception e)
                    {
                        if (ConsoleWrite) Console.WriteLine($"receive clientWebSocket error! {e.Message}");
                        return "";
                    }
             
                }
                return str;
            }
            public byte[] ByteCut(byte[] b, byte cut)
            {
                var list = new List<byte>();
                list.AddRange(b);
                for (var i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i] == cut)
                        list.RemoveAt(i);
                }
                var lastbyte = new byte[list.Count];
                for (var i = 0; i < list.Count; i++)
                {
                    lastbyte[i] = list[i];
                }
                return lastbyte;
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

        public static string JsonSerializeDataTable(this List<System.Data.DataTable> tableList)
        {
            List<string> xmlList = new List<string>();

            foreach (var table in tableList)
            {
                using (StringWriter sw = new StringWriter())
                {
                    table.WriteXml(sw, System.Data.XmlWriteMode.WriteSchema);
                    xmlList.Add(sw.ToString());
                }
            }

            return xmlList.JsonSerializationt();
        }

        // 反序列化 JSON 回到 List<DataTable>
        public static List<System.Data.DataTable> JsonDeserializeToDataTables(this string json)
        {
            List<string> xmlList = json.JsonDeserializet<List<string>>();
            List<System.Data.DataTable> tableList = new List<System.Data.DataTable>();

            foreach (var xmlString in xmlList)
            {
                System.Data.DataTable table = new System.Data.DataTable();
                using (StringReader sr = new StringReader(xmlString))
                {
                    table.ReadXml(sr);
                }
                tableList.Add(table);
            }

            return tableList;
        }
        public static string JsonSerializeDataTable(this System.Data.DataTable table)
        {
            // 使用 WriteXml 將 DataTable 轉換成 XML 字串
            using (StringWriter sw = new StringWriter())
            {
                table.WriteXml(sw, System.Data.XmlWriteMode.WriteSchema);
                return sw.JsonSerializationt();
            }
        }

        // 反序列化 JSON 回到 DataTable
        public static System.Data.DataTable JsonDeserializeToDataTable(this string json)
        {
            string xmlString = json.JsonDeserializet<string>();
            System.Data.DataTable table = new System.Data.DataTable();

            using (StringReader sr = new StringReader(xmlString))
            {
                table.ReadXml(sr);
            }

            return table;
        }

        public static bool is_Chrome()
        {
            try
            {
                string app = "chrome.exe";
                Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey regSubKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\" + app, false);
                string strKey = string.Empty;
                object objResult = regSubKey.GetValue(strKey);
                Microsoft.Win32.RegistryValueKind regValueKind = regSubKey.GetValueKind(strKey);
                return true;

            }
            catch
            {
                MessageBox.Show("請檢查本機是否安裝谷歌Chrome瀏覽器！", "提示");
                return false;
            }
        }

        public static bool DowloadToPictureBox(string url, PictureBox pictureBox)
        {
            Uri urlCheck = new Uri(@url);
            WebRequest webreq = WebRequest.Create(@url);
            HttpWebRequest request = (HttpWebRequest)webreq;

            request.Timeout = 1000;
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                WebResponse webres = webreq.GetResponse();
                Stream stream = webres.GetResponseStream();
                Image image;
                image = Image.FromStream(stream);

                pictureBox.Image = image;

                stream.Close();
                stream.Dispose();
                webres.Close();
                webres.Dispose();
            }
            catch (Exception)
            {

                return false; //could not connect to the internet (maybe) 
            }


            return true;
        }
        /// <summary>
        /// 下載指定網址的圖片並返回Base64字串。
        /// </summary>
        /// <param name="url">圖片的網址。</param>
        /// <returns>圖片的Base64字串。</returns>
        public static string DownloadImageAsBase64(string url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    // 從指定URL下載圖片數據
                    byte[] imageBytes = client.DownloadData(url);

                    // 將圖片數據轉換為Base64字串
                    string base64String = Convert.ToBase64String(imageBytes);

                    return base64String;
                }
            }
            catch (Exception ex)
            {
                // 處理下載或轉換過程中的錯誤
                Console.WriteLine($"Error downloading image: {ex.Message}");
                return null;
            }
        }
        public static bool Ping(string IP, int retrynum, int timeout)
        {
            try
            {
                if (IP.StringIsEmpty()) return false;
                int retry = 0;
                while (true)
                {
                    if (retry >= retrynum)
                    {
                        return false;
                    }
                    System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
                    System.Net.NetworkInformation.PingReply reply = pingSender.Send(IP, timeout);//第一個引數為ip地址,第二個引數為ping的時間
                    if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    {
                        return true;
                        //ping的通

                    }
                    else
                    {
                        retry++;
                        //ping不通

                    }
                }
            }
            catch
            {
                Console.Write($"{IP} ping failed!\n");
                return false;
            }

        }

        public static string JsonSerializationt<T>(this T value)
        {
            return JsonSerializationt(value, false);
        }
        public static string JsonSerializationt<T>(this T value, bool WriteIndented)
        {
            //string jsonString = JsonConvert.SerializeObject(value);
            //return jsonString;
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = WriteIndented,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true,
                PropertyNameCaseInsensitive = true,
                //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            };
            string jsonString = JsonSerializer.Serialize<object>(value, options);
            return jsonString;
        }
        public static Task<string> JsonSerializationtAsync<T>(this T value)
        {
            return Task.FromResult(JsonSerializationt(value));
        }
        public static Task<string> JsonSerializationtAsync<T>(this T value, bool WriteIndented)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = WriteIndented,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true,
                PropertyNameCaseInsensitive = true,
            };
            string jsonString = JsonSerializer.Serialize(value, options);
            return Task.FromResult(jsonString);
        }

        public static JsonSerializerOptions DeserializeOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            IgnoreNullValues = true,
            IgnoreReadOnlyProperties = true,
            PropertyNameCaseInsensitive = false,
        };
        public static T JsonDeserializet<T>(this string jsonString)
        {
          
            try
            {
                return JsonSerializer.Deserialize<T>(jsonString, DeserializeOptions);
            }
            catch(Exception e)
            {
                Console.WriteLine($"[JsonDeserializet]{e.Message}");
                return default(T);
            }

        }
        public static Task<T> JsonDeserializetAsync<T>(this string jsonString)
        {
            return Task.FromResult(JsonDeserializet<T>(jsonString));
        }


        public static async Task<List<T>> DeserializeJsonListAsync<T>(List<string> jsonStrings)
        {
            var tasks = jsonStrings.Select(json => Task.Run(() => JsonSerializer.Deserialize<T>(json)));
            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }
        public static T NewtonJsonDeserializet<T>(this string jsonString)
        {
 
            try
            {
                Newtonsoft.Json.JsonSerializerSettings options = new Newtonsoft.Json.JsonSerializerSettings
                {

                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize
                };

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString, options);
            }
            catch
            {
                return default(T);
            }

        }
        public static string NewtonJsonSerializationt<T>(this T value)
        {

            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            return jsonString;
        }

        public static byte[] JsonUtf8BytesSerializationt<T>(this T value)
        {
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(value);
            return jsonUtf8Bytes;
        }
        public static T JsonUtf8BytesDeserializet<T>(this byte[] jsonUtf8Bytes)
        {
            var readOnlySpan = new ReadOnlySpan<byte>(jsonUtf8Bytes);
            return JsonSerializer.Deserialize<T>(readOnlySpan);
        }

        public static XmlDocument Xml_GetDocument(this string str)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(str);
            return doc;
        }
        public static XmlElement Xml_GetElement(this string str, string[] nodeNames)
        {
            XmlDocument doc = Xml_GetDocument(str);
            if (doc == null) return null;
            XmlElement xmlElement = doc.DocumentElement;
            return Xml_GetElement(xmlElement, nodeNames);
        }
        public static XmlElement Xml_GetElement(this XmlElement xmlElement, string[] nodeNames)
        {
            for (int i = 0; i < nodeNames.Length; i++)
            {
                if (xmlElement == null) return null;
                xmlElement = xmlElement[nodeNames[i]];
            }
            return xmlElement;
        }
        public static string Xml_GetInnerXml(this string str, string[] nodeNames)
        {
            XmlDocument doc = Xml_GetDocument(str);
            if (doc == null) return string.Empty;
            XmlElement xmlElement = doc.DocumentElement;
            return xmlElement.Xml_GetInnerXml(nodeNames);
        }
        public static string Xml_GetInnerXml(this XmlElement xmlElement, string[] nodeNames)
        {
            for (int i = 0; i < nodeNames.Length; i++)
            {
                if (xmlElement == null) return string.Empty;
                xmlElement = xmlElement[nodeNames[i]];
            }
            return xmlElement.InnerXml;
        }
        public static string Xml_GetInnerXml(this XmlElement xmlElement, string Names)
        {
            XmlElement xmlElement1 = xmlElement[Names];
            if (xmlElement1 == null) return "";
            return xmlElement1.InnerXml;
        }

        public class HttpContentType
        {
            public const string TEXT_PLAIN = "text/plain";
            public const string APPLICATION_JSON = "application/json";
            public const string APPLICATION_OCTET_STREAM = "application/octet-stream";
            public const string WWW_FORM_URLENCODED = "application/x-www-form-urlencoded";
            public const string WWW_FORM_URLENCODED_GB2312 = "application/x-www-form-urlencoded;charset=gb2312";
            public const string WWW_FORM_URLENCODED_UTF8 = "application/x-www-form-urlencoded;charset=utf-8";
            public const string MULTIPART_FORM_DATA = "multipart/form-data";
        }
        public class API_Key
        {
            public API_Key(string name , string value)
            {
                this.name = name;
                this.value = value;
            }
            public string name
            {
                get;set;
            }
            public string value
            {
                get;set;
            }
        }
        public static async Task<string> WEBApiGetAsync(string url)
        {
            string responseBody = "";
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch
            {
                return responseBody;
            }
        }
        public static string WEBApiGet(string url)
        {
            string result = Task.Run(async () =>
            {
                string responseBody = await WEBApiGetAsync(url);
                return responseBody;
            }).Result;
            return result;
        }
        public static async Task<string> WEBApiGetAsync(string url, API_Key aPI_Key)
        {
            string responseBody = "";
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpClient client = new HttpClient();
                if (aPI_Key != null) client.DefaultRequestHeaders.Add(aPI_Key.name, aPI_Key.value);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch
            {
                return responseBody;
            }
        }
        public static string WEBApiGet(string url, API_Key aPI_Key)
        {
            string result = Task.Run(async () =>
            {
                string responseBody = await WEBApiGetAsync(url, aPI_Key);
                return responseBody;
            }).Result;
            return result;
        }
        public static string WEBApiPost(string url, List<string> names, List<string> values)
        {
            string result = Task.Run(async () =>
            {
                string responseBody = await WEBApiPostAsync(url, names, values);
                return responseBody;
            }).Result;
            return result;
        }
        public static async Task<string> WEBApiPostAsync(string url, List<string> names, List<string> values)
        {
            string responseBody = "";
            if (url.StringIsEmpty())
            {
                Console.WriteLine($"{Basic.Reflection.GetMethodName()} : 網址不得為空!");
                return null;
            }
            if (names.Count != values.Count)
            {
                Console.WriteLine($"{Basic.Reflection.GetMethodName()} : 參數長度不對稱!");
                return null;
            }
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Post;

                MultipartFormDataContent content = new MultipartFormDataContent();
                for (int i = 0; i < names.Count; i++)
                {
                    content.Add(new StringContent(values[i]), names[i]);
                }

                request.Content = content;

                var response = await client.SendAsync(request);
                responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                return responseBody;
            }
            catch
            {
                return responseBody;
            }
        }

        public static string WEBApiPostJson(string url, string value)
        {
            return WEBApiPostJson(url, value, false);
        }
        public static string WEBApiPostJson(string url, string value, API_Key aPI_Key)
        {
            return WEBApiPostJson(url, value, aPI_Key, false);
        }
        public static string WEBApiPostJson(string url, string value, bool debug)
        {
            return WEBApiPostJson(url, value, null, debug);
        }
        public static string WEBApiPostJson(string url, string value, API_Key aPI_Key, bool debug)
        {
            string result = Task.Run(async () =>
            {
                string responseBody = await WEBApiPostJsonAsync(url, value, aPI_Key, debug);
                return responseBody;
            }).Result;
            return result;
        }
        public static async Task<string> WEBApiPostJsonAsync(string url, string value, bool debug)
        {
            string responseBody = "";
            if (url.StringIsEmpty())
            {
                Console.WriteLine($"{Basic.Reflection.GetMethodName()} : 網址不得為空!");
                return null;
            }

            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                myTimerBasic.StartTickTime(50000);

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Post;



                request.Content = new StringContent(value, Encoding.UTF8, HttpContentType.APPLICATION_JSON);
                var response = await client.SendAsync(request);
                responseBody = await response.Content.ReadAsStringAsync();
                if (debug || DebugLog) Console.WriteLine($"[{DateTime.Now.ToDateTimeString()}] Sucess, url:{url}  <{myTimerBasic.ToString()}>");
                return responseBody;
            }
            catch
            {
                return responseBody;
            }
        }
        public static async Task<string> WEBApiPostJsonAsync(string url, string value, API_Key aPI_Key, bool debug)
        {
            string responseBody = "";
            if (url.StringIsEmpty())
            {
                Console.WriteLine($"{Basic.Reflection.GetMethodName()} : 網址不得為空!");
                return null;
            }

            try
            {
                MyTimerBasic myTimerBasic = new MyTimerBasic();
                myTimerBasic.StartTickTime(50000);

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpClient client = new HttpClient();
                if (aPI_Key != null) client.DefaultRequestHeaders.Add(aPI_Key.name, aPI_Key.value);
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Post;

               

                request.Content = new StringContent(value, Encoding.UTF8, HttpContentType.APPLICATION_JSON);
                var response = await client.SendAsync(request);
                responseBody = await response.Content.ReadAsStringAsync();
                if (debug|| DebugLog) Console.WriteLine($"[{DateTime.Now.ToDateTimeString()}] Sucess, url:{url}  <{myTimerBasic.ToString()}>");
                return responseBody;
            }
            catch
            {
                return responseBody;
            }
        }
        public static byte[] WEBApiPostJsonBytes(string url, string value)
        {
            byte[] result = Task.Run(async () =>
            {
                byte[] responseBody = await WEBApiPostJsonAsyncBytes(url, value);
                return responseBody;
            }).Result;
            return result;
        }
        public static async Task<byte[]> WEBApiPostJsonAsyncBytes(string url, string value)
        {
            byte[] responseBody = new byte[0];
            if (url.StringIsEmpty())
            {
                Console.WriteLine($"{Basic.Reflection.GetMethodName()} : 網址不得為空!");
                return null;
            }

            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Post;


                request.Content = new StringContent(value, Encoding.UTF8, HttpContentType.APPLICATION_JSON);
                var response = await client.SendAsync(request);
                responseBody = await response.Content.ReadAsByteArrayAsync();
                return responseBody;
            }
            catch
            {
                return responseBody;
            }
        }
        public static string WebServicePost(string uri, StringBuilder _str)
        {
            return WebServicePost(uri, _str.ToString());
        }
        public static string WebServicePost(string uri, string _str)
        {
            string _returnstr = "";
            Uri _uri = new Uri(@uri);
            //发起请求
            WebRequest webRequest = WebRequest.Create(_uri);
            webRequest.ContentType = "text/xml; charset=utf-8";
            webRequest.Method = "POST";
            using (Stream requestStream = webRequest.GetRequestStream())
            {
                byte[] paramBytes = Encoding.UTF8.GetBytes(_str);
                requestStream.Write(paramBytes, 0, paramBytes.Length);
            }
            //响应
            try
            {
                WebResponse webResponse = webRequest.GetResponse();
                using (StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    _returnstr = myStreamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                _returnstr = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }
            return _returnstr;
        }

        public static bool DownloadFile(string url, string saveFilePath)
        {
            return DownloadFile(url, saveFilePath, null);
        }
        public static bool DownloadFile(string url, string saveFilePath, DownloadProgressChangedEventHandler Client_DownloadProgressChanged)
        {
            try
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    if (Client_DownloadProgressChanged != null) client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    client.DownloadFile($"{url}", saveFilePath);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static string UploadFileToApi(string filePath, string apiUrl)
        {
            return UploadFileToApi(filePath, apiUrl, "");
        }
        public static string UploadFileToApi(string filePath, string apiUrl, string jsonParams)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new MultipartFormDataContent();

                    // Adding the file content
                    byte[] fileBytes = LoadFileBytes(filePath);
                    var fileContent = new ByteArrayContent(fileBytes);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    content.Add(fileContent, "file", Path.GetFileName(filePath)); // 替换为实际的文件名

                    // Adding JSON parameters as StringContent
                    var jsonContent = new StringContent(jsonParams);
                    jsonContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    content.Add(jsonContent, "jsonParams");

                    HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        throw new Exception("File upload failed. Status code: " + response.StatusCode);
                    }
                }
            }
            catch
            {

            }
            return "";
        }
        public static byte[] LoadFileBytes(string filePath)
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static string WEBApiPost(string apiUrl, string fileName, byte[] file_bytes, List<string> names, List<string> values)
        {
            string result = Task.Run(async () =>
            {
                string responseBody = await WEBApiPostAsync(apiUrl, fileName, file_bytes, names, values);
                return responseBody;
            }).Result;
            return result;
        }

        public static async Task<string> WEBApiPostAsync(string apiUrl, string fileName, byte[] file_bytes, List<string> names, List<string> values)
        {
            var client = new HttpClient();
            string responseBody = "";
            // 建立Form資料
            MultipartFormDataContent form = new MultipartFormDataContent();
            if (file_bytes != null || file_bytes.Length != 0)
            {
                // 加入檔案
                var fileContent = new ByteArrayContent(file_bytes);
                form.Add(fileContent, "file", fileName); // 替換為你的檔名
            }

            for (int i = 0; i < names.Count; i++)
            {
                // 加入其他表單參數
                form.Add(new StringContent(values[i]), names[i]);

            }


            // 呼叫API
            HttpResponseMessage response = await client.PostAsync(apiUrl, form);

            if (response.IsSuccessStatusCode)
            {
                // 取得API回應
                responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API 回應: " + responseBody);
                return responseBody;

            }
            else
            {
                Console.WriteLine("API 呼叫失敗，狀態碼: " + response.StatusCode);
                return responseBody;

            }
        }

        public static byte[] WEBApiPostDownloaFile(string url, string jsonPayload)
        {
            byte[] result = Task.Run(async () =>
            {
                byte[] responseBody = await WEBApiPostDownloaFileAsync(url, jsonPayload);
                return responseBody;
            }).Result;
            return result;
        }
        static async Task<byte[]> WEBApiPostDownloaFileAsync(string url, string jsonPayload)
        {
            using (HttpClient client = new HttpClient())
            {
                // 設置Content-Type為application/json
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // 發送POST請求
                HttpResponseMessage response = await client.PostAsync(url, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

                // 確認回應狀態碼是否成功
                response.EnsureSuccessStatusCode();

                // 讀取回應內容
                byte[] xlsFile = await response.Content.ReadAsByteArrayAsync();

                return xlsFile;
            }
        }
    }
}
