using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
//using Newtonsoft.Json;
namespace Basic
{
    static public class Net
    {
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
                IgnoreReadOnlyProperties = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
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
    }
}
