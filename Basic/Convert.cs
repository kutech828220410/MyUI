using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Drawing;
namespace Basic
{
    public class MyConvert
    {
        public bool 檢查八進位合法(int val)
        {
            string str = val.ToString();
            if (str == "")
                return false;
            const string PATTERN = @"[0-7]+$";
            bool sign = false;
            for (int i = 0; i < str.Length; i++)
            {
                sign = System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), PATTERN);
                if (!sign)
                {
                    return sign;
                }
            }
            return sign;
        }
        public int 十進位轉八進位(int val)
        {
            string str_bin = Convert.ToString(val, 2);
            string str_out = "";
            int 位數 = str_bin.Length / 3;
            int 位數_temp = str_bin.Length % 3;
            位數_temp = 3 - 位數_temp;
            if (位數_temp > 0)
            {
                for (int i = 0; i < 位數_temp; i++)
                {
                    str_bin = "0" + str_bin;
                }
                位數++;
            }
            for (int i = 0; i < 位數; i++)
            {
                int bin提取首位 = i * 3;
                string str_temp0 = str_bin.Substring(bin提取首位, 3);
                str_out = str_out + Convert.ToInt32(str_temp0, 2).ToString();
            }
            val = Convert.ToInt32(str_out);
            return val;
        }
        public int 八進位轉十進位(int val)
        {
            string str_temp = val.ToString();
            string str_out = "";
            char[] char_array = str_temp.ToCharArray();
            for (int i = 0; i < char_array.Length; i++)
            {
                int temp = char_array[i] - 48;
                string str_temp0 = Convert.ToString(temp, 2);
                if (str_temp0.Length < 3) str_temp0 = "0" + str_temp0;
                if (str_temp0.Length < 3) str_temp0 = "0" + str_temp0;
                if (str_temp0.Length < 3) str_temp0 = "0" + str_temp0;
                if (str_temp0.Length < 3) str_temp0 = "0" + str_temp0;
                str_out = str_out + str_temp0;
            }
            val = Convert.ToInt32(str_out, 2);
            return val;
        }
        public int 檢查八進位進位(int val)
        {
            string str_temp = val.ToString();
            string str_out = "";
            str_temp = "0" + str_temp;
            char[] char_array = str_temp.ToCharArray();
            bool 要進位 = false;
            for (int i = char_array.Length - 1; i >= 0; i--)
            {
                int temp = char_array[i] - 48;
                if (要進位)
                {
                    temp++;
                    要進位 = false;
                }
                if (temp > 7)
                {
                    temp -= 8;
                    要進位 = true;
                }
                str_out = temp.ToString() + str_out;
            }
            Int32.TryParse(str_out, out val);
            return val;
        }
        public void Int64轉Int32(Int64 int_64, ref int int_32_high, ref int int_32_low)
        {
            int _32位元滿位元 = Convert.ToInt32("11111111111111111111111111111111", 2);
            int_32_high = (int)(int_64 >> 32);
            int_32_low = (int)(int_64 & _32位元滿位元);
        }
        public void Int32轉Int64(ref Int64 int_64, int int_32_high, int int_32_low)
        {
            string str_int_64 = Convert.ToString(int_64, 2);
            string str_int_32_high = Convert.ToString(int_32_high, 2);
            string str_int_32_low = Convert.ToString(int_32_low, 2);
            while (true)
            {
                if (str_int_64.Length >= 64) break;
                str_int_64 = "0" + str_int_64;
            }
            while (true)
            {
                if (str_int_32_high.Length >= 32) break;
                str_int_32_high = "0" + str_int_32_high;
            }
            while (true)
            {
                if (str_int_32_low.Length >= 32) break;
                str_int_32_low = "0" + str_int_32_low;
            }
            char[] char_int_64 = str_int_64.ToCharArray();
            char[] char_int_32_high = str_int_32_high.ToCharArray();
            char[] char_int_32_low = str_int_32_low.ToCharArray();

            for (int i = 0; i < 64; i++)
            {
                if (i >= 0 && i < 32)
                {
                    char_int_64[i] = char_int_32_low[31 - i];
                }
                else if (i >= 32 && i < 64)
                {
                    char_int_64[i] = str_int_32_high[31 - (i - 32)];
                }
            }

            string str_temp = "";
            for (int i = 0; i < char_int_64.Length; i++)
            {
                str_temp = char_int_64[i] + str_temp;
            }
            int_64 = Convert.ToInt64(str_temp, 2);

        }

        public bool ByteGetBit(Byte val, int num)
        {
            bool FLAG = false;
            num = 7 - num;
            string str_temp0 = Convert.ToString(val, 2);
            while (true)
            {
                if (str_temp0.Length >= 8) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (char_buf[num] == '1') FLAG = true;
            else FLAG = false;

            return FLAG;
        }
        public Byte ByteSetBit(bool FLAG, Byte val, int num)
        {
            num = 7 - num;
            string str_temp0 = Convert.ToString(val, 2);
            while (true)
            {
                if (str_temp0.Length >= 8) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (FLAG) char_buf[num] = '1';
            else char_buf[num] = '0';
            str_temp0 = new string(char_buf);
            val = Convert.ToByte(str_temp0, 2);
            return val;
        }
        public string ByteGetBitString(Byte val)
        {
            string str_temp0 = Convert.ToString(val, 2);
            if (str_temp0.Length < 8) str_temp0 = "0" + str_temp0;
            if (str_temp0.Length < 8) str_temp0 = "0" + str_temp0;
            if (str_temp0.Length < 8) str_temp0 = "0" + str_temp0;
            if (str_temp0.Length < 8) str_temp0 = "0" + str_temp0;
            if (str_temp0.Length < 8) str_temp0 = "0" + str_temp0;
            if (str_temp0.Length < 8) str_temp0 = "0" + str_temp0;
            if (str_temp0.Length < 8) str_temp0 = "0" + str_temp0;
            if (str_temp0.Length < 8) str_temp0 = "0" + str_temp0;
            return str_temp0;
        }

        public bool UInt16GetBit(UInt16 val, int num)
        {
            /* BitConverter.GetBytes(val);
             byte[] list = BitConverter.GetBytes(val);
             System.Collections.BitArray arr = new System.Collections.BitArray(list);
             return arr[num];*/

            bool FLAG = false;
            num = 15 - num;
            string str_temp0 = Convert.ToString(val, 2);
            while (true)
            {
                if (str_temp0.Length >= 16) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (char_buf[num] == '1') FLAG = true;
            else FLAG = false;

            return FLAG;
        }
        public UInt16 UInt16SetBit(bool FLAG, UInt16 val, int num)
        {
            num = 15 - num;
            string str_temp0 = Convert.ToString(val, 2);
            while (true)
            {
                if (str_temp0.Length >= 16) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (FLAG) char_buf[num] = '1';
            else char_buf[num] = '0';
            str_temp0 = new string(char_buf);
            val = Convert.ToUInt16(str_temp0, 2);
            return val;
        }

        public bool UInt32GetBit(UInt32 val, int num)
        {
            /* BitConverter.GetBytes(val);
             byte[] list = BitConverter.GetBytes(val);
             System.Collections.BitArray arr = new System.Collections.BitArray(list);
             return arr[num];*/

            bool FLAG = false;
            num = 31 - num;
            string str_temp0 = Convert.ToString(val, 2);
            while (true)
            {
                if (str_temp0.Length >= 32) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (char_buf[num] == '1') FLAG = true;
            else FLAG = false;

            return FLAG;
        }
        public UInt32 UInt32SetBit(bool FLAG, UInt32 val, int num)
        {
            num = 31 - num;
            string str_temp0 = Convert.ToString(val, 2);
            while (true)
            {
                if (str_temp0.Length >= 32) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (FLAG) char_buf[num] = '1';
            else char_buf[num] = '0';
            str_temp0 = new string(char_buf);
            val = Convert.ToUInt32(str_temp0, 2);
            return val;
        }

        public bool Int32GetBit(Int32 val, int num)
        {
            bool FLAG = false;
            num = 31 - num;
            string str_temp0 = Convert.ToString(val, 2);
            while (true)
            {
                if (str_temp0.Length >= 32) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (char_buf[num] == '1') FLAG = true;
            else FLAG = false;

            return FLAG;
        }
        public Int32 Int32SetBit(bool FLAG, Int32 val, int num)
        {
            num = 31 - num;
            string str_temp0 = Convert.ToString(val, 2);
            while (true)
            {
                if (str_temp0.Length >= 32) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (FLAG) char_buf[num] = '1';
            else char_buf[num] = '0';
            str_temp0 = new string(char_buf);
            val = Convert.ToInt32(str_temp0, 2);
            return val;
        }

        public bool Int64GetBit(Int64 val, int num)
        {
            bool FLAG = false;
            num = 63 - num;
            string str_temp0 = Convert.ToString(val, 2);
            while (true)
            {
                if (str_temp0.Length >= 64) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (char_buf[num] == '1') FLAG = true;
            else FLAG = false;

            return FLAG;
        }
        public Int64 Int64SetBit(bool FLAG, Int64 val, int num)
        {
            num = 63 - num;
            string str_temp0 = Convert.ToString(val, 2);
            while (true)
            {
                if (str_temp0.Length >= 64) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (FLAG) char_buf[num] = '1';
            else char_buf[num] = '0';
            str_temp0 = new string(char_buf);
            val = Convert.ToInt64(str_temp0, 2);
            return val;
        }

        public int StringHexToint(string strHex)
        {
            int num = -1;
            try
            {
                num = Int32.Parse(strHex, System.Globalization.NumberStyles.HexNumber);
            }
            catch
            {
                num = -1;
            }
            return num;
        }
        public string ByteToStringHex(byte[] value)
        {
            return BitConverter.ToString(value);
        }

        public string[] 分解分隔號字串(string str, char 分隔號)
        {
            String[] Str_array;
            if (str != null)
            {
                Str_array = str.Split(new char[1] { 分隔號 }, StringSplitOptions.RemoveEmptyEntries);

            }
            else
            {
                Str_array = new string[0];
            }
            return Str_array;
        }
        public string[] 分解分隔號字串(string str, char 分隔號, StringSplitOptions StringSplitOptions)
        {
            String[] Str_array;
            if (str != null)
            {
                Str_array = str.Split(new char[1] { 分隔號 }, StringSplitOptions);

            }
            else
            {
                Str_array = new string[0];
            }
            return Str_array;
        }
        public string[] 分解分隔號字串(string str, string 分隔號, StringSplitOptions StringSplitOptions)
        {
            String[] Str_array = str.Split(分隔號.ToCharArray(), StringSplitOptions);
            return Str_array;
        }
        public string[] 分解分隔號字串(string str, string 分隔號)
        {
            String[] Str_array = str.Split(分隔號.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return Str_array;
        }

        public char Int32ToASCII(int val)
        {
            bool Error = false;
            if (val <= 32)
            {
                Error = false;
            }
            if (val >= 126)
            {
                Error = false;
            }
            if (Error)
            {
                val = 63;
            }
            return Convert.ToChar(val);
        }
        public int ASCIIToInt32(char val)
        {
            int temp = 63;
            int.TryParse(val.ToString(), out temp);
            return temp;
        }
        public int ASCIIToInt32(string val)
        {
            int temp = 63;
            int.TryParse(val, out temp);
            return temp;
        }

        private int char數字轉int(char val)
        {
            return val - 48;
        }

        public static byte[] ImageToByte(Image Image, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if (Image == null) { return null; }
            byte[] data = null;
            using (MemoryStream oMemoryStream = new MemoryStream())
            {
                //建立副本
                using (Bitmap oBitmap = new Bitmap(Image))
                {
                    //儲存圖片到 MemoryStream 物件，並且指定儲存影像之格式
                    oBitmap.Save(oMemoryStream, imageFormat);
                    //設定資料流位置
                    oMemoryStream.Position = 0;
                    //設定 buffer 長度
                    data = new byte[oMemoryStream.Length];
                    //將資料寫入 buffer
                    oMemoryStream.Read(data, 0, Convert.ToInt32(oMemoryStream.Length));
                    //將所有緩衝區的資料寫入資料流
                    oMemoryStream.Flush();
                }
            }
            return data;
        }
        public static Bitmap ByteToImage(byte[] Buffer)
        {
            if (Buffer == null || Buffer.Length == 0) { return null; }
            byte[] data = null;
            Image oImage = null;
            Bitmap oBitmap = null;
            //建立副本
            data = (byte[])Buffer.Clone();
            try
            {
                MemoryStream oMemoryStream = new MemoryStream(Buffer);
                //設定資料流位置
                oMemoryStream.Position = 0;
                oImage = System.Drawing.Image.FromStream(oMemoryStream);
                //建立副本
                oBitmap = new Bitmap(oImage);
            }
            catch
            {
                throw;
            }
            //return oImage;
            return oBitmap;
        }


        public static ushort Get_CRC16(byte[] pDataBytes)
        {
            ushort crc = 0xffff;
            ushort polynom = 0xA001;

            for (int i = 0; i < pDataBytes.Length; i++)
            {
                crc ^= pDataBytes[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x01) == 0x01)
                    {
                        crc >>= 1;
                        crc ^= polynom;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }

            return crc;
        }
        public static void Get_CRC16(byte[] pDataBytes, ref byte L_byte, ref byte H_byte)
        {
            ushort value = Get_CRC16(pDataBytes);
            L_byte = (byte)(value);
            H_byte = (byte)(value >> 8);
        }
    }

    public static class TypeConvert
    {
        static public UInt32 SetBit(this ref UInt32 value, int index, bool state)
        {
            index = 31 - index;
            string str_temp0 = Convert.ToString(value, 2);
            while (true)
            {
                if (str_temp0.Length >= 32) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (state) char_buf[index] = '1';
            else char_buf[index] = '0';
            str_temp0 = new string(char_buf);
            value = Convert.ToUInt32(str_temp0, 2);
            return value;
        }
        static public bool GetBit(this UInt32 value, int index)
        {
            bool FLAG = false;
            index = 31 - index;
            string str_temp0 = Convert.ToString(value, 2);
            while (true)
            {
                if (str_temp0.Length >= 32) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (char_buf[index] == '1') FLAG = true;
            else FLAG = false;

            return FLAG;
        }
        static public int SetBit(this ref int value, int index, bool state)
        {
            if (state) // 设置位
            {
                value |= (1 << index); // 将指定位设置为 1
            }
            else // 重置位
            {
                value &= ~(1 << index); // 将指定位设置为 0
            }
            return value;
        }
        static public bool GetBit(this int value, int index)
        {
            bool FLAG = false;
            index = 31 - index;
            string str_temp0 = Convert.ToString(value, 2);
            while (true)
            {
                if (str_temp0.Length >= 32) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (char_buf[index] == '1') FLAG = true;
            else FLAG = false;

            return FLAG;
        }

        static public string SetTextValue(string[] columns, object[] value)
        {
            if (columns.Length != value.Length) return null;
            List<string> titles = new List<string>();
            List<string> values = new List<string>();
            for(int i = 0; i < columns.Length; i++)
            {
                titles.Add(columns[i]);
                if(value[i] is DateTime)
                {
                    values.Add(((DateTime)value[i]).ToDateTimeString_6());
                }
                else
                {
                    values.Add(value[i].ToString());
                }
            }
            return SetTextValue(titles, values);

        }
        static public string SetTextValue(string title, string value)
        {
            List<string> titles = new List<string>();
            List<string> values = new List<string>();
            titles.Add(title);
            values.Add(value);
            return SetTextValue(titles, values);
        }
        static public string SetTextValue(List<string> titles , List<string> values)
        {
            if (titles.Count != values.Count) return null;
            string str = "";
            for(int i = 0; i < titles.Count; i++)
            {
                str += $"[{titles[i]}]:{values[i]}";
                if (i != titles.Count - 1) str += ",";
            }
            return str;
        }

        static public object[] GetTextValue(this string str , string[] columns)
        {
            string Mbracket = "";
            string Value = "";
            List<string> titles = new List<string>();
            List<string> values = new List<string>();
            List<string> values_buf = new List<string>();
            string[] subText = str.Split(',');
            for(int i = 0; i < subText.Length; i++)
            {
                subText[i].GetTextValue(":", out Mbracket, out Value);
                titles.Add(Mbracket);
                values.Add(Value);
            }
            object[] obj_out = new object[columns.Length];
            for (int i = 0; i < columns.Length; i++)
            {
                string colname = columns[i];

                values_buf = (from value in titles
                              where value == colname
                              select value).ToList();
                if(values_buf.Count > 0)
                {
                    obj_out[i] = values_buf[0];
                }
                else
                {
                    obj_out[i] = "";
                }
            }
            return obj_out;
        }
        static public void GetTextValue(this string str, out string Title, out string Value)
        {
            str.GetTextValue(":", out Title, out Value);
        }
        static public void GetTextValue(this string str, string markOfValue, out string Title, out string Value)
        {
            Title = "";
            Value = "";
            string[] Mbrackets = str.GetMbrackets();
            string[] subText = str.Split(markOfValue.ToCharArray());
            if (Mbrackets.Length == 0) return;
            if (subText.Length != 2) return;
            Title = Mbrackets[0];
            Title = Title.Substring(1, Title.Length - 2);
            Value = subText[1];
        }

        static public string GetTextValue(this string str, string Title)
        {
            return str.GetTextValue(Title, ",", ":");
        }
        static public string GetTextValue(this string str, string Title, string martkOfSplit, string markOfValue)
        {
            string Mbracket = "";
            string Value = "";
            string[] subText = str.Split(martkOfSplit.ToCharArray());
            for (int i = 0; i < subText.Length; i++)
            {
                subText[i].GetTextValue(markOfValue, out Mbracket, out Value);
                if (Mbracket == Title)
                {
                    return Value;
                }
            }

            return "";
        }

        static public List<string> GetTextValues(this string str, string Title)
        {
            return str.GetTextValues(Title, ",", ":");
        }
        static public List<string> GetTextValues(this string str, string Title, string martkOfSplit, string markOfValue)
        {
            string Mbracket = "";
            List<string> list_value = new List<string>();
            string Value = "";
            string[] subText = str.Split(martkOfSplit.ToCharArray());
            for (int i = 0; i < subText.Length; i++)
            {
                subText[i].GetTextValue(markOfValue, out Mbracket, out Value);
                if(Mbracket == Title)
                {
                    list_value.Add(Value);
                }
            }

            return list_value;
        }

        static public string[] GetMbrackets(this string str)
        {
            Regex regex = new Regex(@"\[[\w ]+\]");
            string[] inBrackets = regex.Matches(str)
                                       .Cast<Match>()
                                       .Select(m => m.Value)
                                       .ToArray();
            return inBrackets;
        }

        static public string ObjectToString(this object item)
        {
            if (item == null) return "";
            if (item is int) return item.ToString();
            if (item is double) return item.ToString();
            if (item is float) return item.ToString();
            if (!(item is string)) return "";
            return ObjectToString((string)item);
        }
        static public string ObjectToString(this string item)
        {
            return item;
        }
        static public string[] ObjectToString(this object[] item)
        {
            List<string> list_str = new List<string>();

            foreach (object obj in item)
            {
                if (obj is string)
                {
                    list_str.Add(obj.ObjectToString());
                }
                else if (obj is DateTime)
                {
                    list_str.Add(obj.ToDateString());
                }
            }

            return list_str.ToArray();
        }
        static public int StringToInt32(this object item)
        {
            if (item == null) return -1;
            if (item is Int32) return (Int32)item;
            if (!(item is string)) return -1;
            return StringToInt32((string)item);
        }
        static public int StringToInt32(this string item)
        {
            int value_temp = -1;
            if (int.TryParse(item, out value_temp))
            {
                return value_temp;
            }
            else
            {
                return -1;
            }
            return value_temp;

        }
        static public UInt32 StringToUInt32(this object item)
        {
            if (item == null) return 0;
            if (item is Int32) return (UInt32)item;
            if (!(item is string)) return 0;
            return StringToUInt32((string)item);
        }
        static public UInt32 StringToUInt32(this string item)
        {
            UInt32 value_temp = 0;
            if (UInt32.TryParse(item, out value_temp))
            {
                return value_temp;
            }
            else
            {
                return 0;
            }
            return value_temp;

        }
        static public Int64 StringToInt64(this string item)
        {
            Int64 value_temp = -1;
            if (Int64.TryParse(item, out value_temp))
            {
                return value_temp;
            }
            else
            {
                return -1;
            }
            return value_temp;

        }
        static public double StringToDouble(this object item)
        {
            if (item == null) return -1;
            if (item is double) return (double)item;
            if (!(item is string)) return -1;
            return StringToDouble((string)item);
        }
        static public double StringToDouble(this string item)
        {
            double value_temp = -1;
            if (double.TryParse(item, out value_temp))
            {
                return value_temp;
            }
            return -1;

        }

        static public bool StringToBool(this object item)
        {
            if (item == null) return false;
            if (!(item is string)) return false;
            return StringToBool((string)item);
        }
        static public bool StringToBool(this string item)
        {
            if (item == true.ToString()) return true;
            else return false;
        }

        static public string StringLength(this string source, int len)
        {
            int len_str;
            while (true)
            {
                len_str = Encoding.Default.GetByteCount(source);
                if (len_str < len) source = (string)source + " ";
                else break;
            }
            return source.ToString();
        }
        static public bool StringIsInt32(this string source)
        {
            int value = 0;
            return int.TryParse(source, out value);
        }


        static public int StringHexToint(this string strHex)
        {
            int num = -1;
            try
            {
                num = Int32.Parse(strHex, System.Globalization.NumberStyles.HexNumber);
            }
            catch
            {
                num = -1;
            }
            return num;
        }
        static public byte[] StringHexTobytes(this string hexValues)
        {
            List<byte> list_byte = new List<byte>();
            string[] hexValuesSplit = hexValues.Split('-');
            try
            {
                for (int i = 0; i < hexValuesSplit.Length; i++)
                {
                    list_byte.Add(Convert.ToByte(hexValuesSplit[i], 16));
                }
            }
            catch
            {
                list_byte.Clear();
                return list_byte.ToArray();
            }
            return list_byte.ToArray();
        }
        static public string ByteToStringHex(this byte[] value)
        {
            return BitConverter.ToString(value);
        }

        static public bool ByteGetBit(this byte val, int num)
        {
            bool FLAG = false;
            num = 7 - num;
            string str_temp0 = Convert.ToString(val, 2);
            while (true)
            {
                if (str_temp0.Length >= 8) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (char_buf[num] == '1') FLAG = true;
            else FLAG = false;

            return FLAG;
        }
        static public Byte ByteSetBit(this byte val, bool FLAG, int num)
        {
            num = 7 - num;
            string str_temp0 = Convert.ToString(val, 2);
            while (true)
            {
                if (str_temp0.Length >= 8) break;
                str_temp0 = "0" + str_temp0;
            }
            char[] char_buf = str_temp0.ToCharArray();
            if (FLAG) char_buf[num] = '1';
            else char_buf[num] = '0';
            str_temp0 = new string(char_buf);
            val = Convert.ToByte(str_temp0, 2);
            return val;
        }
        #region Enum_Convert
        // 取得 Enum 列舉 Attribute Description 設定值
   
        public static string GetDescriptionText<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
        public static string[] GetEnumDescriptions<T>(this T source)
        {
            string[] texts = new string[source.GetLength()];
            Type t = source.GetType();
            Array Array_Enum = System.Enum.GetValues(t);
            for (int i = 0; i < Array_Enum.Length; i++)
            {
              
                if (Array_Enum.GetValue(i).GetDescriptionText().StringIsEmpty())
                {
                    texts[i] = Array_Enum.GetValue(i).ToString();
                }
                else
                {
                    texts[i] = Array_Enum.GetValue(i).GetDescriptionText();
                }
          
            }
            return texts;
        }
        public static int GetLength<T>(this T source)
        {
            return source.GetEnumNames().Length;
        }
        public static string GetEnumName<T>(this T source)
        {
            return source.ToString();
        }
        public static string[] GetEnumNames<T>(this T source)
        {
            if (source == null) return new string[0];
            Type t = source.GetType();
            List<string> List_str = new List<string>();
            Array Array_Enum = System.Enum.GetValues(t);
            for (int i = 0; i < Array_Enum.Length; i++)
            {
                List_str.Add(Array_Enum.GetValue(i).ToString());
            }
            return List_str.ToArray();
        }
        public static string[] GetEnumNames(this object[] source)
        {
            string[] array = new string[source.Length];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = source[i].GetEnumName();
            }

            return array;
        }

        public static int GetEnumValue<T>(this T source, int index)
        {
            return (int)System.Enum.GetValues(source.GetType()).GetValue(index);
        }
        public static int GetEnumValue<T>(this T source)
        {
            Type t = source.GetType();
            Array Array_Enum = System.Enum.GetValues(t);
            for (int i = 0; i < Array_Enum.Length; i++)
            {
                if (source.ToString() == Array_Enum.GetValue(i).ToString())
                {
                    return (int)Array_Enum.GetValue(i);
                }
            }
            return -1;
        }
        public static int[] GetEnumValues<T>(this T source)
        {
            List<int> List_int = new List<int>();
            foreach (int i in System.Enum.GetValues(source.GetType()))
            {
                List_int.Add(i);
            }
            return List_int.ToArray();
        }
        #endregion
        #region DateTime_Convert
        public enum Enum_Year_Type
        {
            Anno_Domini, Republic_of_China
        }
        static public DateTime StringToAnnoDomini(this string x)
        {
            DateTime dt = DateTime.ParseExact(x, "yyyMMdd", System.Globalization.CultureInfo.InvariantCulture).AddYears(1911);
            return dt;
        }

        static public string ToDateString(this object item)
        {
            return ToDateString(item, Enum_Year_Type.Anno_Domini, "/");

        }
        static public string ToDateString(this DateTime item)
        {
            return ToDateString(item, Enum_Year_Type.Anno_Domini, "/");
        }
        static public string ToDateString(this object item, Enum_Year_Type Enum_Year_Type)
        {
            if (item is DateTime)
            {
                return ToDateString((DateTime)item, Enum_Year_Type, "/");
            }
            else if (item is string)
            {
                int Year = 0;
                int Month = 0;
                int Day = 0;
                if (Get_Date((string)item, ref Year, ref Month, ref Day, Enum_Year_Type))
                {
                    return ToDATE_String(Year, Month, Day, "/");
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

        }
        static public string ToDateString(this DateTime item, Enum_Year_Type Enum_Year_Type)
        {
            return ToDateString(item, Enum_Year_Type, "/");
        }
        static public string ToDateString(this object item, string split_char)
        {
            return ToDateString(item, Enum_Year_Type.Anno_Domini, split_char);

        }
        static public string ToDateString(this object item, Enum_Year_Type Enum_Year_Type, string split_char)
        {
            if (item is DateTime)
            {
                return ToDateString((DateTime)item, Enum_Year_Type, split_char);
            }
            else if (item is string)
            {
                int Year = 0;
                int Month = 0;
                int Day = 0;
                if (Get_Date((string)item, ref Year, ref Month, ref Day, Enum_Year_Type))
                {
                    return ToDATE_String(Year, Month, Day, split_char);
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }


        }
        static public string ToDateString(this DateTime item, Enum_Year_Type Enum_Year_Type, string split_char)
        {
            DateTime datetime = item;
            if (Enum_Year_Type == TypeConvert.Enum_Year_Type.Republic_of_China)
            {
                System.Globalization.TaiwanCalendar TaiwanCalendar = new System.Globalization.TaiwanCalendar();

                return ToDATE_String(TaiwanCalendar.GetYear(datetime), datetime.Month, datetime.Day, split_char);
            }
            return ToDATE_String(datetime.Year, datetime.Month, datetime.Day, split_char);

        }
        static public string ToDateTinyString(this DateTime item)
        {
            return $"{item.Year.ToString("0000")}{item.Month.ToString("00")}{item.Day.ToString("00")}";
        }

        static public string ToTimeString(this object item)
        {
            if (!(item is DateTime)) return "";
            return ToTimeString((DateTime)item, ":");

        }
        static public string ToTimeString(this DateTime item)
        {
            return ToTimeString(item, ":");
        }
        static public string ToTimeString(this DateTime item, string split_char)
        {
            DateTime datetime = item;
            return item.Hour.ToString("00") + split_char + item.Minute.ToString("00") + split_char + item.Second.ToString("00");

        }

        static public string ToDateTimeString(this object item)
        {
            return ToDateTimeString(item, Enum_Year_Type.Anno_Domini);

        }
        static public string ToDateTimeString(this DateTime item)
        {
            return ToDateTimeString(item, Enum_Year_Type.Anno_Domini);
        }
        static public string ToDateTimeString(this object item, Enum_Year_Type Enum_Year_Type)
        {
            if (!(item is DateTime)) return "";
            return ToDateTimeString((DateTime)item, Enum_Year_Type);

        }
        static public string ToDateTimeString(this DateTime item, Enum_Year_Type Enum_Year_Type)
        {
            DateTime datetime = item;
            if (Enum_Year_Type == TypeConvert.Enum_Year_Type.Republic_of_China)
            {
                System.Globalization.TaiwanCalendar TaiwanCalendar = new System.Globalization.TaiwanCalendar();

                return ToDATETIME_String(TaiwanCalendar.GetYear(datetime), datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second);
            }
            return ToDATETIME_String(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second);

        }
        static public string ToDateTimeString_6(this object item)
        {
            if (!(item is DateTime)) return "";
            DateTime datetime = (DateTime)item;
            string Year = datetime.Year.ToString("0000");
            string Month = datetime.Month.ToString("00");
            string Day = datetime.Day.ToString("00");
            string Hour = datetime.Hour.ToString("00");
            string Minute = datetime.Minute.ToString("00");
            string Second = datetime.Second.ToString("00");
            string TimeOfDay = datetime.TimeOfDay.ToString();
            string str_dateTime = string.Format("{0}/{1}/{2} {3}", Year, Month, Day, TimeOfDay);
            return str_dateTime;

        }

        static public DateTime StringToDateTime(this object item)
        {
            if (item is DateTime) return (DateTime)item;
            DateTime dateTime = new DateTime();
            if (item is string)
            {

                if (DateTime.TryParse((string)item, out dateTime))
                {
                    return dateTime;
                }
                else { return dateTime; }
            }
            return dateTime;
        }

        public static bool Check_Date_String(this string source)
        {
            return source.Check_Date_String(Enum_Year_Type.Anno_Domini);
        }
        public static bool Check_Date_String(this string source, Enum_Year_Type Enum_Year_Type)
        {

            DateTime dateTime = new DateTime();
            return DateTime.TryParse(source, out dateTime);
            int Year = 0;
            int Month = 0;
            int Day = 0;
            return source.Get_Date(ref Year, ref Month, ref Day, Enum_Year_Type);
        }
        public static int Get_DateTINY(this string source)
        {
            int Year = 0;
            int Month = 0;
            int Day = 0;
            int out_value = -1;
            if (source.Get_Date(ref Year, ref Month, ref Day, Enum_Year_Type.Anno_Domini))
            {
                string str_TINY = Year.ToString("0000") + Month.ToString("00") + Day.ToString("00");
                int.TryParse(str_TINY, out out_value);
            }
            return out_value;
        }
        public static Int64 Get_DateTimeTINY(this DateTime datetime)
        {
            return $"{datetime.Year.ToString("0000")}{datetime.Month.ToString("00")}{datetime.Day.ToString("00")}{datetime.Hour.ToString("00")}{datetime.Minute.ToString("00")}{datetime.Second.ToString("00")}".StringToInt64();
        }
     
        public static bool Get_Date(this string source, ref int Year, ref int Month, ref int Day, Enum_Year_Type Enum_Year_Type)
        {
            if (source is string)
            {
                object temp = (object)source;
                string str_Date = (string)temp;
                String[] str_Date_array;
                if (str_Date != "" || str_Date != null)
                {
                    str_Date_array = str_Date.Split(new char[1] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (str_Date_array.Length != 3) str_Date_array = str_Date.Split(new char[1] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    if (str_Date_array.Length == 3)
                    {
                        if (!int.TryParse(str_Date_array[0], out Year)) return false;
                        if (!int.TryParse(str_Date_array[1], out Month)) return false;
                        if (!int.TryParse(str_Date_array[2], out Day)) return false;

                        if (Year < 0) return false;
                        if (Enum_Year_Type == TypeConvert.Enum_Year_Type.Republic_of_China)
                        {
                            if (Year > 200) return false;
                        }
                        else if (Enum_Year_Type == TypeConvert.Enum_Year_Type.Anno_Domini)
                        {
                            if (Year < 1900) return false;
                        }

                        if (Month < 1 || Month > 12) return false;
                        if (Month == 1)
                        {
                            if (Day < 1 || Day > 31) return false;
                        }
                        else if (Month == 2)
                        {
                            if ((Year % 4 == 0 && Year % 100 != 0) || Year % 400 == 0)
                            {
                                if (Day < 1 || Day > 29) return false;
                            }
                            else
                            {
                                if (Day < 1 || Day > 28) return false;
                            }



                        }
                        else if (Month == 3)
                        {
                            if (Day < 1 || Day > 31) return false;
                        }
                        else if (Month == 4)
                        {
                            if (Day < 1 || Day > 30) return false;
                        }
                        else if (Month == 5)
                        {
                            if (Day < 1 || Day > 31) return false;
                        }
                        else if (Month == 6)
                        {
                            if (Day < 1 || Day > 30) return false;
                        }
                        else if (Month == 7)
                        {
                            if (Day < 1 || Day > 31) return false;
                        }
                        else if (Month == 8)
                        {
                            if (Day < 1 || Day > 31) return false;
                        }
                        else if (Month == 9)
                        {
                            if (Day < 1 || Day > 30) return false;
                        }
                        else if (Month == 10)
                        {
                            if (Day < 1 || Day > 31) return false;
                        }
                        else if (Month == 11)
                        {
                            if (Day < 1 || Day > 30) return false;
                        }
                        else if (Month == 12)
                        {
                            if (Day < 1 || Day > 31) return false;
                        }
                        return true;
                    }

                }
            }
            return false;
        }
        public static bool IsHolidays(DateTime date)
        {
            // 週休二日
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
            // 國定假日(國曆)
            if (date.ToString("MM/dd").Equals("01/01"))
            {
                return true;
            }
            if (date.ToString("MM/dd").Equals("02/28"))
            {
                return true;
            }
            if (date.ToString("MM/dd").Equals("04/04"))
            {
                return true;
            }
            if (date.ToString("MM/dd").Equals("04/05"))
            {
                return true;
            }
            if (date.ToString("MM/dd").Equals("05/01"))
            {
                return true;
            }
            if (date.ToString("MM/dd").Equals("10/10"))
            {
                return true;
            }

            // 國定假日(農曆)
            System.Globalization.TaiwanLunisolarCalendar TaiwanLunisolarCalendar = new System.Globalization.TaiwanLunisolarCalendar();
            string LeapDate = string.Format("{0}/{1}", TaiwanLunisolarCalendar.GetMonth(date), TaiwanLunisolarCalendar.GetDayOfMonth(date));
            if (LeapDate == "12/30")
            {
                return true;
            }
            if (LeapDate == ("1/1"))
            {
                return true;
            }
            if (LeapDate == ("1/2"))
            {
                return true;
            }
            if (LeapDate == ("1/3"))
            {
                return true;
            }
            if (LeapDate == ("1/4"))
            {
                return true;
            }
            if (LeapDate == ("1/5"))
            {
                return true;
            }
            if (LeapDate == ("5/5"))
            {
                return true;
            }
            if (LeapDate == ("8/15"))
            {
                return true;
            }

            return false;
        }
        public static bool IsHspitalHolidays(DateTime date)
        {
            if (date.ToString("MM/dd").Equals("09/23"))
            {
                return false;
            }
            // 週休二日
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
            // 國定假日(國曆)
            if (date.ToString("MM/dd").Equals("01/01"))
            {
                return true;
            }
            if (date.ToString("MM/dd").Equals("02/28"))
            {
                return true;
            }
            if (date.ToString("MM/dd").Equals("04/05"))
            {
                return true;
            }
     
            if (date.ToString("MM/dd").Equals("10/10"))
            {
                return true;
            }

            // 國定假日(農曆)
            System.Globalization.TaiwanLunisolarCalendar TaiwanLunisolarCalendar = new System.Globalization.TaiwanLunisolarCalendar();
            string LeapDate = string.Format("{0}/{1}", TaiwanLunisolarCalendar.GetMonth(date), TaiwanLunisolarCalendar.GetDayOfMonth(date));
            if (LeapDate == "12/30")
            {
                return true;
            }
            if (LeapDate == ("1/1"))
            {
                return true;
            }
            if (LeapDate == ("1/2"))
            {
                return true;
            }
            if (LeapDate == ("1/3"))
            {
                return true;
            }
            if (LeapDate == ("1/4"))
            {
                return true;
            }
            if (LeapDate == ("1/5"))
            {
                return true;
            }
            if (LeapDate == ("5/5"))
            {
                return true;
            }
            if (LeapDate == ("8/15"))
            {
                return true;
            }

            return false;
        }
        public static bool IsLeap_Year(int input_years)
        {
            bool isleap_year = input_years % 400 == 0 || (input_years % 4 == 0 && input_years % 100 != 0);//將閏年條件賦值給bool
            return isleap_year;
        }
        public static int GetMonthOfDays(int Month)
        {
            switch(Month)
            {
                case 1:
                    return 31;
                case 2:
                    if (IsLeap_Year(DateTime.Now.Year))
                    {
                        return 29;
                    }
                    else
                    {
                        return 28;
                    }
                  
                case 3:
                    return 31;
                case 4:
                    return 30;
                case 5:
                    return 31;
                case 6:
                    return 30;
                case 7:
                    return 31;
                case 8:
                    return 31;
                case 9:
                    return 30;
                case 10:
                    return 30;
                case 11:
                    return 30;
                case 12:
                    return 31;
                default:
                    return -1;
                    
            }
                
        }

        public static bool IsInDate(this DateTime dt_keyin, DateTime dt_start, DateTime dt_end)
        {
            return dt_keyin.CompareTo(dt_start) >= 0 && dt_keyin.CompareTo(dt_end) <= 0;
        }

     
        public static bool IsNewDay(int hour , int min)
        {
            return IsNewDay(DateTime.Now, hour, min);
        }
        public static bool IsNewDay(this DateTime dt_keyin, int hour, int min)
        {
            int _hour = dt_keyin.Hour;
            int _min = dt_keyin.Minute;
            int _temp = _hour * 1000 + _min;
            int temp = hour * 1000 + min;
            return (temp - _temp <= 0);
        }
        #region Function
        static private string ToDATE_String(string Year, string Month, string Day)
        {
            int int_Year = 0;
            int int_Month = 0;
            int int_Day = 0;
            if (!int.TryParse(Year, out int_Year)) return null;
            if (!int.TryParse(Month, out int_Month)) return null;
            if (!int.TryParse(Day, out int_Day)) return null;
            return ToDATE_String(int_Year, int_Month, int_Day);
        }
        static private string ToDATE_String(int Year, int Month, int Day)
        {
            return ToDATE_String(Year, Month, Day, "/");
        }
        static private string ToDATE_String(int Year, int Month, int Day, string split_char)
        {
            return Year.ToString() + split_char + Month.ToString("00") + split_char + Day.ToString("00");
        }
        static private string ToTIME_String(string Hour, string Min, string Sec)
        {
            int int_Hour = 0;
            int int_Min = 0;
            int int_Sec = 0;
            if (!int.TryParse(Hour, out int_Hour)) return null;
            if (!int.TryParse(Min, out int_Min)) return null;
            if (!int.TryParse(Sec, out int_Sec)) return null;
            return ToTIME_String(int_Hour, int_Min, int_Sec);
        }
        static private string ToTIME_String(int Hour, int Min, int Sec)
        {
            return ToTIME_String(Hour, Min, Sec, ":");
        }
        static private string ToTIME_String(int Hour, int Min, int Sec, string split_char)
        {
            return Hour.ToString("00") + split_char + Min.ToString("00") + split_char + Sec.ToString("00");
        }
        static private string ToDATETIME_String(string Year, string Month, string Day, string Hour, string Min, string Sec)
        {
            string DATE = ToDATE_String(Year, Month, Day);
            string TIME = ToTIME_String(Hour, Min, Sec);
            return DATE + " " + TIME;
        }
        static private string ToDATETIME_String(int Year, int Month, int Day, int Hour, int Min, int Sec)
        {
            string DATE = ToDATE_String(Year, Month, Day);
            string TIME = ToTIME_String(Hour, Min, Sec);
            return DATE + " " + TIME;
        }
        #endregion
        #endregion

        static public string EncodingTo(this string text, string src_encoding_name, string dst_encoding_name)
        {
            Encoding src_encoding = Encoding.GetEncoding(src_encoding_name);
            Encoding dst_encoding = Encoding.GetEncoding(dst_encoding_name);
            return EncodingTo(text, src_encoding, dst_encoding);

        }
        static public string EncodingTo(this string text , Encoding src_encoding , Encoding dst_encoding)
        {
            byte[] bytes = src_encoding.GetBytes(text);
            string utf8ReturnString = dst_encoding.GetString(bytes);
            return text;
        }
        public static string UnescapeUnicode(this string str)  // 将unicode转义序列(\uxxxx)解码为字符串
        {
            return (System.Text.RegularExpressions.Regex.Unescape(str));
        }

        static public bool StringIsEmpty(this string item)
        {

            if (item == null) return true;
            if (item.Length == 0) return true;
            item = item.Replace(" ", "");
            return false;
        }
        static public bool Check_IP_Adress(this string item)
        {
            string[] ips = item.Split('.');
            if (ips.Length != 4) return false;
            System.Net.IPAddress iPAddress = new System.Net.IPAddress(new byte[4] { 255, 255, 255, 255 });
            return System.Net.IPAddress.TryParse(item, out iPAddress);
        }
        static public System.Net.IPAddress StrinToIPAddress(this string item)
        {
            
            System.Net.IPAddress iPAddress = new System.Net.IPAddress(new byte[4] { 255, 255, 255, 255 });
            if(System.Net.IPAddress.TryParse(item, out iPAddress))
            {
                return iPAddress;
            }
            return null;
        }
        static public string ToColorString(this Color item)
        {
            return string.Format("{2},{1},{0}", item.R.ToString(), item.G.ToString(), item.B.ToString());
        }
        static public Color ToColor(this string item)
        {
            string[] array_color = item.Split(',');
            if (array_color.Length != 3) return Color.Black;
            int R, G, B;
            B = array_color[0].StringToInt32();
            G = array_color[1].StringToInt32();
            R = array_color[2].StringToInt32();
            if (R == -1) return Color.Black;
            if (G == -1) return Color.Black;
            if (B == -1) return Color.Black;
            return Color.FromArgb(255, R, G, B);
        }
        static public Color SetAlpha(this Color color , byte alpha)
        {
            byte R = color.R;
            byte G = color.G;
            byte B = color.B;
            return Color.FromArgb(alpha, R, G, B);
        }

        static public Font ToFont(this string value)
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
        static public string ToFontString(this Font font)
        {
            return font.FontFamily.Name
                    + ":" + font.Size
                    + ":" + font.Style
                    + ":" + font.Unit
                    + ":" + font.GdiCharSet
                    + ":" + font.GdiVerticalFont
                    ;
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

}
