using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic
{
    static public class LINQ
    {
        static public void LockAdd<T>(this List<T> list_value, T value)
        {
            lock (list_value)
            {
                list_value.Add(value);
            }
        }
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

        static public List<object[]> GetRows(this List<object[]> list_value, int colindex, string serchvalue)
        {
            list_value = (from value in list_value
                          where value[colindex].ObjectToString() == serchvalue
                          select value).ToList();
            return list_value;
        }
        static public List<object[]> GetRowsByLike(this List<object[]> list_value, int colindex, string serchvalue)
        {
            return GetRowsByLike(list_value, colindex, serchvalue , false);
        }
        static public List<object[]> GetRowsByLike(this List<object[]> list_value, int colindex, string serchvalue , bool upper)
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
        static public List<object[]> GetRowsInDate(this List<object[]> list_value, int colindex, DateTime datetime)
        {
            DateTime datetime_start = new DateTime(datetime.Year, datetime.Month, datetime.Day, 0, 0, 0);
            DateTime datetime_end =  new DateTime(datetime.Year, datetime.Month, datetime.Day, 23, 59, 59);
            return GetRowsInDate(list_value, colindex, datetime_start, datetime_end);
        }

        static public List<object[]> GetRowsInDate(this List<object[]> list_value, int colindex, System.Windows.Forms.DateTimePicker datetime_start, System.Windows.Forms.DateTimePicker datetime_end)
        {
            DateTime dateTime_start = datetime_start.Value;
            DateTime dateTime_end = datetime_end.Value;

            dateTime_start = new DateTime(dateTime_start.Year, dateTime_start.Month, dateTime_start.Day, 0, 0, 0);
            dateTime_end = new DateTime(dateTime_end.Year, dateTime_end.Month, dateTime_end.Day, 23, 59, 59);
            return GetRowsInDate(list_value, colindex, dateTime_start, dateTime_end);
        }
        static public List<object[]> GetRowsInDate(this List<object[]> list_value, int colindex, DateTime datetime_start, DateTime datetime_end)
        {
            list_value = (from value in list_value
                          where TypeConvert.IsInDate(value[colindex].StringToDateTime(), datetime_start, datetime_end)
                          select value).ToList();
            return list_value;
        }
        static public bool IsEqual(this object[] srcvalue, object[] dstvalue ,params int[] exclude)
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

                if(srcvalue[i] is DateTime)
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

        static public object[] CopyRow(this object[] src_value ,object enum_src, object enum_dst, params int[] src_exclude)
        {
            string[] dst_EnumNames = enum_dst.GetEnumNames();
            object[] dst_value = new object[dst_EnumNames.Length];
            return src_value.CopyRow(ref dst_value, enum_src, enum_dst, src_exclude);
        }
        static public object[] CopyRow(this object[] src_value, ref object[] dst_value, object enum_src, object enum_dst, params int[] src_exclude)
        {
            string[] src_EnumNames = enum_src.GetEnumNames();
            string[] dst_EnumNames = enum_dst.GetEnumNames();
          
            bool flag_continue = false;
            for (int i = 0; i < dst_EnumNames.Length; i++)
            {
                for (int k = 0; k < src_EnumNames.Length; k++)
                {
                    flag_continue = false;
                    for (int m = 0; m < src_exclude.Length; m++)
                    {
                        if(k == src_exclude[m])
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

        static public int RemoveByGUID(this List<object[]> list_value, object[] serchvalue)
        {
            List<object[]> list_serchvalue = new List<object[]>();
            list_serchvalue.Add(serchvalue);
            return RemoveByGUID(list_value, list_serchvalue);
        }
        static public int RemoveByGUID(this List<object[]> list_value, List<object[]> list_serchvalue)
        {
            string[] serchvalue = new string[list_serchvalue.Count];
            for (int i = 0; i < serchvalue.Length; i++)
            {
                serchvalue[i] = list_serchvalue[i][0].ObjectToString();
            }
            return RemoveByGUID(list_value,serchvalue);
        }
        static public int RemoveByGUID(this List<object[]> list_value, string serchvalue)
        {
            return RemoveByGUID(list_value, new string[] { serchvalue });
        }
        static public int RemoveByGUID(this List<object[]> list_value, string[] serchvalue)
        {
            return RemoveRow(list_value, 0, serchvalue);
        }
        static public int RemoveRow(this List<object[]> list_value, int colindex, string serchvalue)
        {
            return RemoveRow(list_value, colindex, new string[]{ serchvalue });
        }
        static public int RemoveRow(this List<object[]> list_value, int colindex, string[] serchvalue)
        {
            int num = 0;
            for(int i = 0; i < serchvalue.Length; i++)
            {
                num += list_value.RemoveAll(value => value[colindex].ObjectToString() == serchvalue[i]);
            }
            return num;
        }
    }
}
