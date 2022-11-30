using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace HsBasic
{
    public class Memory
    {
        public static ulong StackMemory = 500;
        public static ulong StackNum = 1;
        static IntPtr[] StartMemoryPtr;
        public static void MemoryInit()
        {
            StartMemoryPtr = new IntPtr[StackNum];
            for (int i = 0; i < StartMemoryPtr.Length; i++)
            {
                StartMemoryPtr[i] = MemoryInit((int)StackMemory);
            }
            MaxLenth = (ulong)(StackMemory * 1024 * 1024 * (ulong)StartMemoryPtr.Length);
        }
      

        static ulong MaxLenth;
        static ulong InUseLenth = 0;
        static int InUseStack = 0;
        private class MemoryStackClass
        {
            public IntPtr Ptr;
            public int Lenth;
            public int StackNum;
            public MemoryStackClass(IntPtr Ptr, int StackNum, int Lenth)
            {
                this.Ptr = Ptr;
                this.Lenth = Lenth;
                this.StackNum = StackNum;
            }
            public static ulong GetStackNumLenth(List<MemoryStackClass> MemoryStackList,int StackNum)
            {
                ulong SumOfLenth = 0;
                foreach(MemoryStackClass MemoryStackClass in MemoryStackList)
                {
                    if (MemoryStackClass.StackNum == StackNum) SumOfLenth += (ulong)MemoryStackClass.Lenth;
                }
                return SumOfLenth;
            }
        }
        private static List<MemoryStackClass> MemoryStack = new List<MemoryStackClass>();

        private static IntPtr MemoryInit(int MegaByte)
        {
            IntPtr StartMemoryPtr = Marshal.AllocHGlobal(MegaByte * 1024 * 1024);
         
            return StartMemoryPtr;
        }
        public static IntPtr GetMemoryFromStack(int len)
        {
            IntPtr MemoryPtr = IntPtr.Zero;

            ulong StackUsedLenth = MemoryStackClass.GetStackNumLenth(MemoryStack, InUseStack);
            if (StackUsedLenth + (ulong)len >= StackMemory * 1024 * 1024)
            {
                InUseStack++;
                StackUsedLenth = 0;
            }
            MemoryPtr = StartMemoryPtr[InUseStack] + (int)StackUsedLenth ;
            MemoryStack.Add(new MemoryStackClass(MemoryPtr, InUseStack, len));

            InUseLenth += (ulong)len;
           
            return MemoryPtr;
        }
        /// <summary>
        /// 申请内存
        /// </summary>
        /// <param name="len">内存长度(单位:字节)</param>
        /// <returns>内存首地址</returns>
        public static IntPtr Malloc(int len)
        {
            IntPtr MemoryPtr = Marshal.AllocHGlobal(len);
            return MemoryPtr;
        }

        /// <summary>
        /// 释放ptr托管的内存
        /// </summary>
        /// <param name="ptr">托管指针</param>
        public static void Free(IntPtr ptr)
        {
            Marshal.FreeHGlobal(ptr);
        }

        /// <summary>
        /// 将字节数组的内容拷贝到托管内存中
        /// </summary>
        /// <param name="source">元数据</param>
        /// <param name="startIndex">元数据拷贝起始位置</param>
        /// <param name="destination">托管内存</param>
        /// <param name="length">拷贝长度</param>
        public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// 将托管内存的内容拷贝到字节数组中
        /// </summary>
        /// <param name="source">托管内存</param>
        /// <param name="destination">目标字节数组</param>
        /// <param name="startIndex">拷贝起始位置</param>
        /// <param name="length">拷贝长度</param>
        public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);

        }

        /// <summary>
        /// 将ptr托管的内存转化为结构体对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="ptr">托管指针</param>
        /// <returns>转化后的对象</returns>
        public static T PtrToStructure<T>(IntPtr ptr)
        {
            return Marshal.PtrToStructure<T>(ptr);
        }

        /// <summary>
        /// 将结构体对象复制到ptr托管的内存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="ptr"></param>
        public static void StructureToPtr<T>(T t, IntPtr ptr)
        {
            Marshal.StructureToPtr(t, ptr, true);
        }

        /// <summary>
        /// 获取类型的大小
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>类型的大小</returns>
        public static int SizeOf<T>()
        {
            return Marshal.SizeOf<T>();
        }
        unsafe public static void CopyPtr(IntPtr SrcPtr,IntPtr DstPtr,int len)
        {
            Buffer.MemoryCopy((byte*)SrcPtr, (byte*)DstPtr, len, len);
        }
    }
}
