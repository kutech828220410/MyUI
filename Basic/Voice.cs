﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using SpeechLib;
using System.Net;
using System.IO;
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
        static public string GoogleSpeakerBase64(string text, string language = "zh-tw")
        {
            // 確保輸入的文字是URL安全的
            string encodedText = Uri.EscapeDataString(text);
            string url = string.Format("http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen={0}&client=tw-ob&q={1}&tl={2}",
                                        text.Length, encodedText, language);
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    byte[] audioData = webClient.DownloadData(url);

                    // 將音訊資料轉換為 Base64 字串
                    string base64Audio = Convert.ToBase64String(audioData);

                    return base64Audio;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return null;
                }
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
