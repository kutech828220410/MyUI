using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic
{
    static public class LINQ
    {
        /// <summary>
        /// 以執行緒安全的方式將元素添加到清單的末尾。
        /// </summary>
        /// <typeparam name="T">清單中元素的類型。</typeparam>
        /// <param name="list_value">要添加元素的清單。</param>
        /// <param name="value">要添加到清單中的元素。</param>
        static public void LockAdd<T>(this List<T> list_value, T value)
        {
            lock (list_value)
            {
                list_value.Add(value);
            }
        }

        /// <summary>
        /// 以執行緒安全的方式將多個元素添加到清單的末尾。
        /// </summary>
        /// <typeparam name="T">清單中元素的類型。</typeparam>
        /// <param name="list_value">要添加元素的清單。</param>
        /// <param name="values">要添加到清單中的元素列表。</param>
        static public void LockAdd<T>(this List<T> list_value, List<T> values)
        {
            lock (list_value)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    list_value.Add(values[i]);
                }
            }
        }

        /// <summary>
        /// 以執行緒安全的方式將多個字串元素添加到清單的末尾，可選擇是否去重。
        /// </summary>
        /// <param name="list_value">要添加元素的清單。</param>
        /// <param name="values">要添加到清單中的字串列表。</param>
        /// <param name="Distinct">是否去重。</param>
        static public void LockAdd(this List<string> list_value, List<string> values, bool Distinct)
        {
            lock (list_value)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    list_value.Add(values[i]);
                }
            }
            if (Distinct)
            {
                list_value = list_value.Distinct().ToList();
            }
        }

        /// <summary>
        /// 獲取清單中指定列索引的所有值。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="colindex">要獲取值的列索引。</param>
        /// <returns>指定列的所有值的列表。</returns>
        static public List<string> GetSelectColumnValue(this List<object[]> list_value, int colindex)
        {
            List<string> list_value_out = new List<string>();
            for (int i = 0; i < list_value.Count; i++)
            {
                string value = list_value[i][colindex].ObjectToString();
                list_value_out.Add(value);
            }
            return list_value_out;
        }

        /// <summary>
        /// 根據指定的列索引和搜索值，篩選出符合條件的行，並返回這些行的列表。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="colindex">用於篩選的列索引。</param>
        /// <param name="serchvalue">要匹配的搜索值。</param>
        /// <returns>篩選後符合條件的行的列表。</returns>
        static public List<object[]> GetRows(this List<object[]> list_value, int colindex, string serchvalue)
        {
            list_value = (from value in list_value
                          where value[colindex].ObjectToString() == serchvalue
                          select value).ToList();
            return list_value;
        }

        /// <summary>
        /// 根據指定的列索引和搜索值，以大小寫不敏感的方式篩選出包含指定值的行，並返回這些行的列表。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="colindex">用於篩選的列索引。</param>
        /// <param name="serchvalue">要匹配的搜索值。</param>
        /// <returns>篩選後符合條件的行的列表。</returns>
        static public List<object[]> GetRowsByLike(this List<object[]> list_value, int colindex, string serchvalue)
        {
            return GetRowsByLike(list_value, colindex, serchvalue, true);
        }

        /// <summary>
        /// 根據指定的列索引和搜索值，以指定的大小寫敏感方式篩選出包含指定值的行，並返回這些行的列表。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="colindex">用於篩選的列索引。</param>
        /// <param name="serchvalue">要匹配的搜索值。</param>
        /// <param name="upper">是否進行大小寫不敏感的比較。</param>
        /// <returns>篩選後符合條件的行的列表。</returns>
        static public List<object[]> GetRowsByLike(this List<object[]> list_value, int colindex, string serchvalue, bool upper)
        {
            if (upper)
            {
                list_value = (from value in list_value
                              where value[colindex].ObjectToString().ToUpper().Contains(serchvalue.ToUpper())
                              select value).ToList();
            }
            else
            {
                list_value = (from value in list_value
                              where value[colindex].ObjectToString().Contains(serchvalue)
                              select value).ToList();
            }

            return list_value;
        }

        /// <summary>
        /// 根據指定的列索引和搜索值，以大小寫不敏感的方式篩選出以指定值開頭的行，並返回這些行的列表。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="colindex">用於篩選的列索引。</param>
        /// <param name="serchvalue">要匹配的搜索值。</param>
        /// <returns>篩選後符合條件的行的列表。</returns>
        static public List<object[]> GetRowsStartWithByLike(this List<object[]> list_value, int colindex, string serchvalue)
        {
            list_value = (from value in list_value
                          where value[colindex].ObjectToString().Length >= serchvalue.Length
                          select value).ToList();

            list_value = (from value in list_value
                          where value[colindex].ObjectToString().Substring(0, serchvalue.Length).ToUpper() == serchvalue.ToUpper()
                          select value).ToList();
            return list_value;
        }


        /// <summary>
        /// 根據指定的日期篩選出符合條件的行，並返回這些行的列表。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="colindex">用於篩選的列索引。</param>
        /// <param name="datetime">要篩選的日期。</param>
        /// <returns>篩選後符合條件的行的列表。</returns>
        static public List<object[]> GetRowsInDate(this List<object[]> list_value, int colindex, DateTime datetime)
        {
            DateTime datetime_start = new DateTime(datetime.Year, datetime.Month, datetime.Day, 0, 0, 0);
            DateTime datetime_end = new DateTime(datetime.Year, datetime.Month, datetime.Day, 23, 59, 59);
            return GetRowsInDate(list_value, colindex, datetime_start, datetime_end);
        }

        /// <summary>
        /// 根據指定的年份和月份篩選出符合條件的行，並返回這些行的列表。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="colindex">用於篩選的列索引。</param>
        /// <param name="Year">要篩選的年份。</param>
        /// <param name="Month">要篩選的月份。</param>
        /// <returns>篩選後符合條件的行的列表。</returns>
        static public List<object[]> GetRowsInMonth(this List<object[]> list_value, int colindex, int Year, int Month)
        {
            if (Month < 1 || Month > 12)
            {
                return new List<object[]>();
            }
            DateTime datetime_start = new DateTime(Year, Month, 1, 0, 0, 0);
            DateTime datetime_end = datetime_start.AddMonths(1).AddDays(-1);
            datetime_end = new DateTime(datetime_end.Year, datetime_end.Month, datetime_end.Day, 23, 59, 59);
            return GetRowsInDate(list_value, colindex, datetime_start, datetime_end);
        }

        /// <summary>
        /// 根據指定的日期範圍篩選出符合條件的行，並返回這些行的列表。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="colindex">用於篩選的列索引。</param>
        /// <param name="datetime_start">要篩選的起始日期。</param>
        /// <param name="datetime_end">要篩選的結束日期。</param>
        /// <returns>篩選後符合條件的行的列表。</returns>
        static public List<object[]> GetRowsInDate(this List<object[]> list_value, int colindex, DateTime datetime_start, DateTime datetime_end)
        {
            list_value = (from value in list_value
                          where TypeConvert.IsInDate(value[colindex].StringToDateTime(), datetime_start, datetime_end)
                          select value).ToList();
            return list_value;
        }

        /// <summary>
        /// 根據指定的日期範圍篩選出符合條件的行，並返回這些行的列表（使用DateTimePicker控制項）。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="colindex">用於篩選的列索引。</param>
        /// <param name="datetime_start">要篩選的起始日期控制項。</param>
        /// <param name="datetime_end">要篩選的結束日期控制項。</param>
        /// <returns>篩選後符合條件的行的列表。</returns>
        static public List<object[]> GetRowsInDate(this List<object[]> list_value, int colindex, System.Windows.Forms.DateTimePicker datetime_start, System.Windows.Forms.DateTimePicker datetime_end)
        {
            DateTime dateTime_start = datetime_start.Value;
            DateTime dateTime_end = datetime_end.Value;

            dateTime_start = new DateTime(dateTime_start.Year, dateTime_start.Month, dateTime_start.Day, 0, 0, 0);
            dateTime_end = new DateTime(dateTime_end.Year, dateTime_end.Month, dateTime_end.Day, 23, 59, 59);
            return GetRowsInDate(list_value, colindex, dateTime_start, dateTime_end);
        }

        /// <summary>
        /// 根據指定的日期範圍篩選出符合條件的行，並返回這些行的列表（帶擴展功能）。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="colindex">用於篩選的列索引。</param>
        /// <param name="datetime_start">要篩選的起始日期。</param>
        /// <param name="datetime_end">要篩選的結束日期。</param>
        /// <returns>篩選後符合條件的行的列表。</returns>
        static public List<object[]> GetRowsInDateEx(this List<object[]> list_value, int colindex, DateTime datetime_start, DateTime datetime_end)
        {
            list_value = (from value in list_value
                          where TypeConvert.IsInDate(value[colindex].StringToDateTime(), datetime_start, datetime_end)
                          select value).ToList();
            return list_value;
        }

        /// <summary>
        /// 根據指定的搜索值從字典中獲取對應的行，並返回這些行的列表。
        /// </summary>
        /// <param name="dictionary">包含多行的字典，每個鍵對應一個物件數組列表。</param>
        /// <param name="serchvalue">要匹配的搜索值。</param>
        /// <returns>篩選後符合條件的行的列表。</returns>
        static public List<object[]> GetRows(this Dictionary<object, List<object[]>> dictionary, object serchvalue)
        {
            if (dictionary.TryGetValue(serchvalue, out List<object[]> result))
            {
                return result;
            }
            else
            {
                return new List<object[]>();
            }
        }

        /// <summary>
        /// 將列表轉換為字典，鍵為指定列索引的值，值為對應的行。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="idColumnIndex">用作鍵的列索引。</param>
        /// <returns>轉換後的字典。</returns>
        static public Dictionary<object, object[]> ConvertToDictionaryPRI(this List<object[]> list_value, int idColumnIndex)
        {
            Dictionary<object, object[]> dictionary = list_value.ToDictionary(item => item[idColumnIndex]);

            return dictionary;
        }

        /// <summary>
        /// 將列表轉換為字典，鍵為指定列索引的值，值為對應的行列表。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="idColumnIndex">用作鍵的列索引。</param>
        /// <returns>轉換後的字典。</returns>
        static public Dictionary<object, List<object[]>> ConvertToDictionary(this List<object[]> list_value, int idColumnIndex)
        {
            Dictionary<object, List<object[]>> dictionary = new Dictionary<object, List<object[]>>();

            foreach (var item in list_value)
            {
                object key = item[idColumnIndex];

                // 如果字典中已經存在該索引鍵，則將值添加到對應的列表中
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key].Add(item);
                }
                // 否則創建一個新的列表並添加值
                else
                {
                    List<object[]> values = new List<object[]> { item };
                    dictionary[key] = values;
                }
            }

            return dictionary;
        }

        /// <summary>
        /// 比較兩個物件數組是否相等，排除指定的列。
        /// </summary>
        /// <param name="srcvalue">第一個物件數組。</param>
        /// <param name="dstvalue">第二個物件數組。</param>
        /// <param name="exclude">要排除比較的列索引。</param>
        /// <returns>如果相等，返回true；否則返回false。</returns>
        static public bool IsEqual(this object[] srcvalue, object[] dstvalue, params int[] exclude)
        {
            bool flag_continue = false;
            if (srcvalue.Length != dstvalue.Length) return false;
            for (int i = 0; i < srcvalue.Length; i++)
            {
                flag_continue = false;
                for (int k = 0; k < exclude.Length; k++)
                {
                    if (i == exclude[k])
                    {
                        flag_continue = true;
                        break;
                    }
                }
                if (flag_continue) continue;
                if (srcvalue[i] == null || dstvalue[i] == null)
                {
                    if (srcvalue[i].ObjectToString() != dstvalue[i].ObjectToString()) return false;
                    else continue;
                }
                if (srcvalue[i].GetType().ToString() != dstvalue[i].GetType().ToString()) return false;

                if (srcvalue[i] is DateTime)
                {
                    if (srcvalue[i].ToDateTimeString_6() != dstvalue[i].ToDateTimeString_6()) return false;
                }
                else
                {
                    if (srcvalue[i].ObjectToString() != dstvalue[i].ObjectToString()) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 將源物件數組的值複製到目標物件數組中。
        /// </summary>
        /// <param name="src_value">源物件數組。</param>
        /// <param name="value">目標物件數組。</param>
        static public void CopyRowTo(this object[] src_value, object[] value)
        {
            if (src_value.Length == value.Length)
            {
                for (int i = 0; i < src_value.Length; i++)
                {
                    value[i] = src_value[i];
                }
            }
        }


        /// <summary>
        /// 將源物件數組列表的值複製到目標物件數組列表中。
        /// </summary>
        /// <param name="src_values">源物件數組列表。</param>
        /// <param name="enum_src">源列舉類型。</param>
        /// <param name="enum_dst">目標列舉類型。</param>
        /// <param name="src_exclude">要排除的列索引。</param>
        /// <returns>目標物件數組列表。</returns>
        static public List<object[]> CopyRows(this List<object[]> src_values, object enum_src, object enum_dst, params int[] src_exclude)
        {
            List<object[]> dst_values = new List<object[]>();

            for (int i = 0; i < src_values.Count; i++)
            {
                object[] value = src_values[i].CopyRow(enum_src, enum_dst, src_exclude);
                dst_values.Add(value);
            }

            return dst_values;
        }

        /// <summary>
        /// 將源物件數組的值複製到新的目標物件數組中。
        /// </summary>
        /// <param name="src_value">源物件數組。</param>
        /// <param name="enum_src">源列舉類型。</param>
        /// <param name="enum_dst">目標列舉類型。</param>
        /// <param name="src_exclude">要排除的列索引。</param>
        /// <returns>新的目標物件數組。</returns>
        static public object[] CopyRow(this object[] src_value, object enum_src, object enum_dst, params int[] src_exclude)
        {
            string[] dst_EnumNames = enum_dst.GetEnumNames();
            object[] dst_value = new object[dst_EnumNames.Length];
            return src_value.CopyRow(ref dst_value, enum_src, enum_dst, src_exclude);
        }

        /// <summary>
        /// 將源物件數組的值複製到新的目標物件數組中。
        /// </summary>
        /// <param name="src_value">源物件數組。</param>
        /// <param name="src_EnumNames">源列舉名稱。</param>
        /// <param name="dst_EnumNames">目標列舉名稱。</param>
        /// <param name="src_exclude">要排除的列索引。</param>
        /// <returns>新的目標物件數組。</returns>
        static public object[] CopyRow(this object[] src_value, string[] src_EnumNames, string[] dst_EnumNames, params int[] src_exclude)
        {
            object[] dst_value = new object[dst_EnumNames.Length];
            return src_value.CopyRow(ref dst_value, src_EnumNames, dst_EnumNames, src_exclude);
        }

        /// <summary>
        /// 將源物件數組的值複製到指定的目標物件數組中。
        /// </summary>
        /// <param name="src_value">源物件數組。</param>
        /// <param name="dst_value">目標物件數組。</param>
        /// <param name="enum_src">源列舉類型。</param>
        /// <param name="enum_dst">目標列舉類型。</param>
        /// <param name="src_exclude">要排除的列索引。</param>
        /// <returns>目標物件數組。</returns>
        static public object[] CopyRow(this object[] src_value, ref object[] dst_value, object enum_src, object enum_dst, params int[] src_exclude)
        {
            string[] src_EnumNames = enum_src.GetEnumNames();
            string[] dst_EnumNames = enum_dst.GetEnumNames();

            return src_value.CopyRow(ref dst_value, src_EnumNames, dst_EnumNames, src_exclude);
        }

        /// <summary>
        /// 將源物件數組的值複製到指定的目標物件數組中。
        /// </summary>
        /// <param name="src_value">源物件數組。</param>
        /// <param name="dst_value">目標物件數組。</param>
        /// <param name="src_EnumNames">源列舉名稱。</param>
        /// <param name="dst_EnumNames">目標列舉名稱。</param>
        /// <param name="src_exclude">要排除的列索引。</param>
        /// <returns>目標物件數組。</returns>
        static public object[] CopyRow(this object[] src_value, ref object[] dst_value, string[] src_EnumNames, string[] dst_EnumNames, params int[] src_exclude)
        {
            bool flag_continue = false;
            for (int i = 0; i < dst_EnumNames.Length; i++)
            {
                for (int k = 0; k < src_EnumNames.Length; k++)
                {
                    flag_continue = false;
                    for (int m = 0; m < src_exclude.Length; m++)
                    {
                        if (k == src_exclude[m])
                        {
                            flag_continue = true;
                            break;
                        }
                    }
                    if (flag_continue) continue;
                    if (dst_EnumNames[i] == src_EnumNames[k])
                    {
                        dst_value[i] = src_value[k];
                        break;
                    }
                }
            }
            for (int i = 0; i < dst_value.Length; i++)
            {
                if (dst_value[i] == null) dst_value[i] = "";
            }
            return dst_value;
        }

        /// <summary>
        /// 根據指定的GUID從列表中移除行。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="serchvalue">要匹配的物件數組。</param>
        /// <returns>移除的行數。</returns>
        static public int RemoveByGUID(this List<object[]> list_value, object[] serchvalue)
        {
            List<object[]> list_serchvalue = new List<object[]>();
            list_serchvalue.Add(serchvalue);
            return RemoveByGUID(list_value, list_serchvalue);
        }

        /// <summary>
        /// 根據指定的GUID列表從列表中移除行。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="list_serchvalue">要匹配的物件數組列表。</param>
        /// <returns>移除的行數。</returns>
        static public int RemoveByGUID(this List<object[]> list_value, List<object[]> list_serchvalue)
        {
            string[] serchvalue = new string[list_serchvalue.Count];
            for (int i = 0; i < serchvalue.Length; i++)
            {
                serchvalue[i] = list_serchvalue[i][0].ObjectToString();
            }
            return RemoveByGUID(list_value, serchvalue);
        }

        /// <summary>
        /// 根據指定的GUID從列表中移除行。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="serchvalue">要匹配的GUID。</param>
        /// <returns>移除的行數。</returns>
        static public int RemoveByGUID(this List<object[]> list_value, string serchvalue)
        {
            return RemoveByGUID(list_value, new string[] { serchvalue });
        }

        /// <summary>
        /// 根據指定的GUID列表從列表中移除行。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="serchvalue">要匹配的GUID列表。</param>
        /// <returns>移除的行數。</returns>
        static public int RemoveByGUID(this List<object[]> list_value, string[] serchvalue)
        {
            return RemoveRow(list_value, 0, serchvalue);
        }

        /// <summary>
        /// 根據指定的列索引和搜索值從列表中移除行。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="colindex">用於篩選的列索引。</param>
        /// <param name="serchvalue">要匹配的搜索值。</param>
        /// <returns>移除的行數。</returns>
        static public int RemoveRow(this List<object[]> list_value, int colindex, string serchvalue)
        {
            return RemoveRow(list_value, colindex, new string[] { serchvalue });
        }

        /// <summary>
        /// 根據指定的列索引和搜索值列表從列表中移除行。
        /// </summary>
        /// <param name="list_value">包含多行的列表，每行是一個物件數組。</param>
        /// <param name="colindex">用於篩選的列索引。</param>
        /// <param name="serchvalue">要匹配的搜索值列表。</param>
        /// <returns>移除的行數。</returns>
        static public int RemoveRow(this List<object[]> list_value, int colindex, string[] serchvalue)
        {
            int num = 0;
            for (int i = 0; i < serchvalue.Length; i++)
            {
                num += list_value.RemoveAll(value => value[colindex].ObjectToString() == serchvalue[i]);
            }
            return num;
        }

    }
}
