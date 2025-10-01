using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
        /// 根據指定的鍵值從字典中獲取對應的列表，如果鍵值不存在則返回空列表。
        /// </summary>
        /// <param name="dictionary">要查詢的字典，其中鍵是對象，值是對象數組的列表。</param>
        /// <param name="value">要查詢的鍵值。</param>
        /// <returns>返回對應於指定鍵值的對象數組列表。如果鍵值不存在，則返回空列表。</returns>
        static public List<object[]> SortDictionary(this Dictionary<object, List<object[]>> dictionary, object value)
        {
            if (dictionary.ContainsKey(value))
            {
                return dictionary[value];
            }
            return new List<object[]>();
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

    public static class BasicClassMethod
    {
        static public object[] ClassToSQL<T, E>(this T _class) where E : Enum
        {
            object[] value = new object[Enum.GetNames(typeof(E)).Length];
            Type enumType = typeof(E);
            E _enum = Activator.CreateInstance<E>();
            foreach (var field in enumType.GetFields())
            {
                if (field.FieldType.IsEnum)
                {
                    string enumName = field.Name;
                    int enumIndex = (int)field.GetValue(_enum);

                    // 使用 enumIndex 來填入對應的屬性值
                    value[enumIndex] = typeof(T).GetProperty(enumName)?.GetValue(_class);
                }
            }

            return value;
        }
        /// <summary>
        /// 將 Class 轉換為 SQL 用 object[]
        /// (只取有 Description 的屬性，依照宣告順序回傳值)
        /// </summary>
        public static object[] ClassToSQL<T>(this T obj)
        {
            if (obj == null) return Array.Empty<object>();

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // 過濾出有 Description 的屬性
            var filtered = new List<object>();
            foreach (var prop in properties)
            {
                var descAttr = prop.GetCustomAttribute<DescriptionAttribute>();
                if (descAttr == null) continue;

                filtered.Add(prop.GetValue(obj));
            }

            return filtered.ToArray();
        }

        static public T SQLToClass<T, E>(this object[] values)
        {
            T obj = Activator.CreateInstance<T>();
            E _enum = Activator.CreateInstance<E>();
            Type enumType = typeof(E);

            foreach (var field in enumType.GetFields())
            {
                string enumName = field.Name;
                if (field.FieldType.IsEnum)
                {

                    int enumIndex = (int)field.GetValue(_enum);

                    // 使用 enumIndex 來取得對應的屬性值
                    object value = values[enumIndex];

                    // 使用 enumName 來取得對應的屬性
                    var property = typeof(T).GetProperty(enumName);
                    if (value is DateTime)
                    {
                        if (property == null)
                        {
                            property?.SetValue(obj, value.ObjectToString());
                        }
                        else if (property.PropertyType.Name == "DateTime")
                        {
                            property?.SetValue(obj, value);
                        }
                        else
                        {
                            property?.SetValue(obj, value.ToDateTimeString());
                        }

                    }
                    else
                    {
                        if (property == null)
                        {
                            property?.SetValue(obj, value.ObjectToString());
                        }
                        else if (property.PropertyType.Name == "DateTime")
                        {
                            property?.SetValue(obj, value.StringToDateTime());
                        }
                        else
                        {
                            // 將值填入對應的屬性
                            property?.SetValue(obj, value.ObjectToString());
                        }

                    }

                }
            }

            return obj;
        }
        /// <summary>
        /// 將 SQL 取得的 object[] 轉換為 Class
        /// (只填入有 Description 的屬性，依宣告順序對應 values)
        /// </summary>
        public static T SQLToClass<T>(this object[] values) where T : new()
        {
            if (values == null) return default;

            T obj = new T();

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            int index = 0;
            foreach (var prop in properties)
            {
                // 只處理有 [Description] 的屬性
                var descAttr = prop.GetCustomAttribute<DescriptionAttribute>();
                if (descAttr == null) continue;

                if (index >= values.Length) break;

                object value = values[index];

                if (value == null)
                {
                    prop.SetValue(obj, null);
                }
                else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                {
                    // 如果屬性是 DateTime
                    if (value is DateTime dt)
                    {
                        prop.SetValue(obj, dt);
                    }
                    else
                    {
                        prop.SetValue(obj, value.ToString().StringToDateTime());
                    }
                }
                else
                {
                    string temp = value.ToString();
                    if (temp.Check_Date_String() == true)
                    {
                        prop.SetValue(obj, temp.StringToDateTime().ToDateTimeString_6('-'));
                    }
                    else
                    {
                        prop.SetValue(obj, temp);
                    }
                }

                index++;
            }

            return obj;
        }

        static public List<object[]> ClassToSQL<T, E>(this List<T> _classes) where E : Enum, new()
        {
            List<object[]> list_value = new List<object[]>();
            E _enum = Activator.CreateInstance<E>();
            for (int i = 0; i < _classes.Count; i++)
            {
                object[] value = _classes[i].ClassToSQL<T, E>();
                list_value.Add(value);
            }
            return list_value;
        }
        /// <summary>
        /// 將 List<Class> 轉換為 List<object[]>
        /// (只取有 Description 的屬性，依宣告順序回傳)
        /// </summary>
        public static List<object[]> ClassToSQL<T>(this List<T> classes)
        {
            List<object[]> list_value = new List<object[]>();
            if (classes == null || classes.Count == 0) return list_value;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var obj in classes)
            {
                var row = new List<object>();
                foreach (var prop in properties)
                {
                    var descAttr = prop.GetCustomAttribute<DescriptionAttribute>();
                    if (descAttr == null) continue;

                    row.Add(prop.GetValue(obj));
                }
                list_value.Add(row.ToArray());
            }

            return list_value;
        }

        static public List<T> SQLToClass<T, E>(this List<object[]> values) where E : Enum, new()
        {
            List<T> list_value = new List<T>();
            E _enum = Activator.CreateInstance<E>();

            for (int i = 0; i < values.Count; i++)
            {
                object[] value = values[i];
                T _class = values[i].SQLToClass<T, E>();
                list_value.Add(_class);
            }
            return list_value;
        }
        /// <summary>
        /// 將 List<object[]> 轉換為 List<Class>
        /// (只填入有 Description 的屬性，依宣告順序對應 values)
        /// </summary>
        public static List<T> SQLToClass<T>(this List<object[]> values) where T : new()
        {
            var list_value = new List<T>();
            if (values == null || values.Count == 0) return list_value;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                      .Where(p => p.GetCustomAttribute<DescriptionAttribute>() != null)
                                      .ToArray();

            foreach (var row in values)
            {
                T obj = new T();
                int index = 0;

                foreach (var prop in properties)
                {
                    if (index >= row.Length) break;

                    object value = row[index];

                    if (value == null)
                    {
                        prop.SetValue(obj, null);
                    }
                    else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                    {
                        if (value is DateTime dt)
                        {
                            prop.SetValue(obj, dt);
                        }
                        else
                        {
                            prop.SetValue(obj, value.ToString().StringToDateTime());
                        }
                    }
                    else
                    {
                        string temp = value.ToString();
                        if (temp.Check_Date_String() == true)
                        {
                            prop.SetValue(obj, temp.StringToDateTime().ToDateTimeString_6('-'));                        
                        }
                        else
                        {
                            prop.SetValue(obj, temp);
                        }
                    }

                    index++;
                }

                list_value.Add(obj);
            }

            return list_value;
        }


        static public T ObjToClass<T>(this object data)
        {
            string jsondata = data.JsonSerializationt();
            return jsondata.JsonDeserializet<T>();
        }
        static public List<T> ObjToListClass<T>(this object data)
        {
            string jsondata = data.JsonSerializationt();
            return jsondata.JsonDeserializet<List<T>>();
        }

        static public List<object[]> ObjToListSQL<T, E>(this object data) where E : Enum, new()
        {
            List<T> list_T = data.ObjToListClass<T>();

            return list_T.ClassToSQL<T, E>();
        }

        public static T ShallowCopy<T>(this T source) where T : class, new()
        {
            if (source == null) return null;
            T copy = new T();
            foreach (var prop in typeof(T).GetProperties().Where(p => p.CanWrite))
            {
                var value = prop.GetValue(source);
                prop.SetValue(copy, value);
            }
            return copy;
        }
    }
}
