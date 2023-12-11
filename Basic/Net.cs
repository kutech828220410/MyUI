using System;
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
//using Newtonsoft.Json;
namespace Basic
{
    static public class Net
    {
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
        public static string JsonSerializationt<T>(this T value ,bool WriteIndented)
        {
            //string jsonString = JsonConvert.SerializeObject(value);
            //return jsonString;
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = WriteIndented,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true,
                //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            };
            string jsonString = JsonSerializer.Serialize<object>(value, options);
            return jsonString;
        }
        public static T JsonDeserializet<T>(this string jsonString)
        {
            //return JsonConvert.DeserializeObject<T>(jsonString);
            try
            {
                return JsonSerializer.Deserialize<T>(jsonString);
            }
            catch
            {
                return default(T);
            }
           
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
            string result = Task.Run(async () =>
            {
                string responseBody = await WEBApiPostJsonAsync(url, value);
                return responseBody;
            }).Result;
            return result;
        }
        public static async Task<string> WEBApiPostJsonAsync(string url, string value)
        {
            string responseBody = "";
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
                responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
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
        public static bool DownloadFile(string url, string saveFilePath , DownloadProgressChangedEventHandler Client_DownloadProgressChanged)
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

        public static async Task<string> WEBApiPostAsync(string apiUrl,string fileName, byte[] file_bytes, List<string> names, List<string> values)
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

    }
}
