using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using SpeechLib;
using System.Net;
using System.IO;
using System.Threading;
namespace Basic
{
    public class Voice
    {
        static public void MediaPlay(string fileName)
        {
            try
            {
                using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer($@"{fileName}"))
                {
                    sp.Stop();
                    sp.Play();
                    sp.PlaySync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now.ToDateTimeString()} : [{System.Reflection.MethodBase.GetCurrentMethod().Name}] Exception:{e.Message}");
            }

        }
        static public void MediaPlayAsync(string fileName)
        {
            Task.Run(new Action(delegate 
            {
                try
                {
                    using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer($@"{fileName}"))
                    {
                        sp.Stop();
                        sp.Play();
                        sp.PlaySync();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{DateTime.Now.ToDateTimeString()} : [{System.Reflection.MethodBase.GetCurrentMethod().Name}] Exception:{e.Message}");
                }
            }));
       

        }
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string lpszCommand, StringBuilder returnString, int bufferSize, IntPtr hwndCallback);
        [DllImport("winmm.dll")]
        static extern bool mciGetErrorString(Int32 errorCode, StringBuilder errorText, Int32 errorTextSize);
        [DllImport("winmm.dll")]
        public static extern bool PlaySound(string pszSound, int hmod, int fdwSound);
        static public void GoogleSpeaker(string text , string fileName)
        {
            CloseMP3(fileName);
            string str = string.Format("http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q={0}&tl=zh-tw", text);
            WebClient mywebClient = new WebClient();
            mywebClient.DownloadFile(str, @fileName);
            if (File.Exists(@fileName))
            {
                PlayMP3(@fileName);
     
            }
          
        }
        static public void PlayGoogleTTS(string text, string language = "zh-tw")
        {
            string encodedText = Uri.EscapeDataString(text);
            string url = string.Format(
                "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen={0}&client=tw-ob&q={1}&tl={2}",
                text.Length, encodedText, language);

            Console.WriteLine($"PlayGoogleTTS : {url}");

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    Console.WriteLine("開始下載語音資料...");
                    byte[] audioData = webClient.DownloadData(url);
                    Console.WriteLine($"下載完成，資料大小: {audioData.Length} bytes");

                    // 建立臨時檔案
                    string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".mp3");
                    File.WriteAllBytes(tempFile, audioData);
                    Console.WriteLine($"臨時檔案建立於: {tempFile}");

                    // 撥放
                    Console.WriteLine("準備播放音檔...");
                    mciSendString("close Mp3File", null, 0, IntPtr.Zero);
                    mciSendString($"open \"{tempFile}\" type mpegvideo alias Mp3File", null, 0, IntPtr.Zero);
                    mciSendString("play Mp3File", null, 0, IntPtr.Zero);
                    Console.WriteLine("播放中...");

                    // 等待播放完成
                    bool isPlaying = true;
                    while (isPlaying)
                    {
                        StringBuilder sb = new StringBuilder(128);
                        mciSendString("status Mp3File mode", sb, sb.Capacity, IntPtr.Zero);
                        string status = sb.ToString().Trim();
                        Console.WriteLine($"播放狀態: {status}");
                        if (status != "playing")
                        {
                            isPlaying = false;
                        }
                        System.Threading.Thread.Sleep(100);
                    }

                    // 關閉並刪除檔案
                    Console.WriteLine("播放結束，關閉並刪除臨時檔案。");
                    mciSendString("close Mp3File", null, 0, IntPtr.Zero);
                    File.Delete(tempFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("播放失敗: " + ex.Message);
                }
            }

        }

        static public string GoogleSpeakerBase64(string text, string language = "zh-tw")
        {
            string encodedText = Uri.EscapeDataString(text);
            string url = string.Format(
                "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen={0}&client=tw-ob&q={1}&tl={2}",
                text.Length, encodedText, language);
            Console.WriteLine($"Google TTS URL: {url}");

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    byte[] audioData = webClient.DownloadData(url);
                    Console.WriteLine($"下載完成，音檔大小：{audioData.Length} bytes");
                    return Convert.ToBase64String(audioData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("下載失敗: " + ex.Message);
                    return null;
                }
            }
        }
        static public void PlayVoiceFromBase64(string base64Audio)
        {
            try
            {
                Console.WriteLine("開始解碼 Base64 音檔...");
                byte[] audioData = Convert.FromBase64String(base64Audio);

                string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".mp3");
                File.WriteAllBytes(tempFile, audioData);
                Console.WriteLine($"臨時音檔已儲存: {tempFile}");

                Console.WriteLine("開始播放...");
                mciSendString("close Mp3File", null, 0, IntPtr.Zero);
                mciSendString($"open \"{tempFile}\" type mpegvideo alias Mp3File", null, 0, IntPtr.Zero);
                mciSendString("play Mp3File", null, 0, IntPtr.Zero);

                bool isPlaying = true;
                while (isPlaying)
                {
                    StringBuilder sb = new StringBuilder(128);
                    mciSendString("status Mp3File mode", sb, sb.Capacity, IntPtr.Zero);
                    string status = sb.ToString().Trim();
                    Console.WriteLine($"播放狀態: {status}");
                    if (status != "playing")
                    {
                        isPlaying = false;
                    }
                    Thread.Sleep(100);
                }

                Console.WriteLine("播放結束，關閉檔案並刪除臨時檔案");
                mciSendString("close Mp3File", null, 0, IntPtr.Zero);
                File.Delete(tempFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("播放失敗: " + ex.Message);
            }
        }
        static private void CloseMP3(string fileName)
        {
            string CommandString = "";
            CommandString = "close  Mp3File";
            mciSendString(CommandString, null, 0, IntPtr.Zero);
    

        }
        static public void PlayMP3(string fileName)
        {
            string CommandString = "";
            CommandString = "close  Mp3File";
            mciSendString(CommandString, null, 0, IntPtr.Zero);
            CommandString = "open " + "\"" + @fileName + "\"" + " type MPEGVideo alias Mp3File";//注意，alias 后面的Mp3File是你取的别名，当然废话，英文意思就是别名。所以可以随便取，但一定要与后面的命令中的名字相同。注意“\”“，你可以不要，但最好还是保留，否则不小心忘了在"type"前留空格，整个命令就没有分隔符了，其实整个命令也可以不用“ type MPEGVideo”，但别忘了在alias前加入空格（如果有\"也可以不用）。
            mciSendString(CommandString, null, 0, IntPtr.Zero);
            CommandString = "set Mp3File time format ms";
            mciSendString(CommandString, null, 0, IntPtr.Zero);
            CommandString = "seek Mp3File to 0";//0 即音频开始，当然第一次本来就在开始，但如果你重复动作，不将播放位置放在0位，那么第一次播完就无法再播出声音了，因为系统已经播放文件到最后了。
            mciSendString(CommandString, null, 0, IntPtr.Zero);
            CommandString = "play Mp3File";
            mciSendString(CommandString, null, 0, IntPtr.Zero);
            //CommandString = "close  Mp3File";
            //mciSendString(CommandString, null, 0, IntPtr.Zero);

        }
        public void Speak(string str)
        {
            this.Speak(str, SpeechVoiceSpeakFlags.SVSFDefault);
        }
        public void Speak(string str, SpeechVoiceSpeakFlags speechVoiceSpeakFlags)
        {
            SpVoiceClass voice = new SpVoiceClass();
            voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);//Item(0)中文女聲
            voice.Speak(str, speechVoiceSpeakFlags);
               
        }
        public void SpeakOnTask(string str)
        {
            Task task = new Task(() =>
            {
                this.Speak(str);
            });
            task.Start();
        }
    }
}
