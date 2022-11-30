using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace Basic
{
    static public class Reflection
    {
        [DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SETREDRAW = 0xB;

        public static void SuspendDrawing(this Control target)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 0, 0);
        }

        public static void ResumeDrawing(this Control target) { ResumeDrawing(target, true); }
        public static void ResumeDrawing(this Control target, bool redraw)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 1, 0);

            if (redraw)
            {
                target.Refresh();
            }
        }

        public static void MakeDoubleBuffered(Control control, bool setting)
        {
            Type controlType = control.GetType();
            PropertyInfo pi = controlType.GetProperty("DoubleBuffered",
            BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(control, setting, null);
        }
        /// <summary>
        /// 取得類別內變數值
        /// </summary>
        /// <param name="Class">引入:類別</param>
        /// <param name="Name">引入:變數名稱</param>
        /// <returns>變數值</returns>
        public static object GetClassField(object Class, string Name)
        {
            FieldInfo[] _FieldInfo = null;
            _FieldInfo = Class.GetType().GetFields();
            foreach(FieldInfo fieldInfo in _FieldInfo)
            {
               if(fieldInfo.Name.ToString() == Name)
               {
                   return Class.GetType().GetField(Name, BindingFlags.Public | BindingFlags.Instance).GetValue(Class);
               }
            }             
            return null;
        }
        /// <summary>
        /// 設定類別內變數值
        /// </summary>
        /// <param name="Class">引入:類別</param>
        /// <param name="Name">引入:變數名稱</param>
        /// <param name="value">引入:變數內容</param>
        /// <returns>更改後類別</returns>
        public static object SetClassField(object Class ,string Name ,object value)
        {
            FieldInfo[] _FieldInfo = null;
            _FieldInfo = Class.GetType().GetFields();
            foreach (FieldInfo fieldInfo in _FieldInfo)
            {
                if (fieldInfo.Name.ToString() == Name)
                {
                    fieldInfo.SetValue(Class, value);
                }
            }
            return Class;
        }
        /// <summary>
        /// 取得類別內所有變數值
        /// </summary>
        /// <param name="Class">引入:類別</param>
        /// <returns>變數值陣列</returns>
        public static object[] GetClassFields(object Class)
        {
            FieldInfo[] _FieldInfo = null;
            List<object> objectList = new List<object>();
            _FieldInfo = Class.GetType().GetFields();
            foreach (FieldInfo fieldInfo in _FieldInfo)
            {
                objectList.Add(Class.GetType().GetField(fieldInfo.Name.ToString(), BindingFlags.Public | BindingFlags.Instance).GetValue(Class));              
            }
            return objectList.ToArray();
        }
        /// <summary>
        /// 取得類別內所有變數名稱
        /// </summary>
        /// <param name="Class">引入:類別</param>
        /// <returns>變數名稱陣列</returns>
        public static string[] GetClassFieldsName(object Class)
        {
            FieldInfo[] _FieldInfo = null;
            List<string> stringList = new List<string>();
            _FieldInfo = Class.GetType().GetFields();
            foreach (FieldInfo fieldInfo in _FieldInfo)
            {

                stringList.Add(fieldInfo.Name.ToString());

            }
            return stringList.ToArray();
        }


        public static string GetMethodName()
        {
            MethodBase m = MethodBase.GetCurrentMethod();
            return m.DeclaringType.Name;
        }

    }
}
