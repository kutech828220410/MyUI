using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HsBasic;
using Basic;
using System.Diagnostics;
using System.Drawing;
namespace HsImage
{
    public class ImageRotator
    {
        public ImageRotator()
        {

        }
        private StructVegaHandle StructSrcImageHandle;
        private StructVegaHandle StructDstImageHandle;
        public IntPtr VegaHandle;
        private IntPtr _SrcImageHandle;
        public IntPtr SrcImageHandle
        {
            get
            {
                return _SrcImageHandle;
            }
            set
            {
 
                StructSrcImageHandle = Memory.PtrToStructure<StructVegaHandle>(value);
                _SrcImageHandle = value;
            }
        }
        private IntPtr _DstImageHandle;
        public IntPtr DstImageHandle
        {
             get
            {
                return _DstImageHandle;
            }
            set
            {
                _DstImageHandle = value;
            }
        }
        public uint BlankColorBW8 = 0;
        public double RotateCenterX = 0;
        public double RotateCenterY = 0;
        public double RotateDegree = 0;
        public int Width
        {
            get
            {
                return StructDstImageHandle.Width;
            }
            private set
            {
            }
        }
        public int Height
        {
            get
            {
                return StructDstImageHandle.Height;
            }
            private set
            {
            }
        }
        unsafe public void Rotate()
        {
            double Radiu = RotateDegree * Math.PI / 180;
            double SinValue = Math.Sin(Radiu);
            double CosValue = Math.Cos(Radiu);
            int Src_X_SIZE = StructSrcImageHandle.Width;
            int Src_Y_SIZE = StructSrcImageHandle.Height;
            Size Dst_Size = this.CalculateNewSize(Src_X_SIZE, Src_Y_SIZE, RotateDegree);
            int Dst_X_SIZE = Dst_Size.Width;
            int Dst_Y_SIZE = Dst_Size.Height;

            StructDstImageHandle = Memory.PtrToStructure<StructVegaHandle>(DstImageHandle);
            StructDstImageHandle.Width = Dst_X_SIZE;
            StructDstImageHandle.Height = Dst_Y_SIZE;
            StructDstImageHandle.ColorDepth = StructSrcImageHandle.ColorDepth;
            StructDstImageHandle.ByteOfSkip = StructDstImageHandle.GetBitmapSkip();
            StructDstImageHandle.ImagePixelFormat = StructSrcImageHandle.ImagePixelFormat;
            StructDstImageHandle.ByteOfWidth = (StructDstImageHandle.Width * StructDstImageHandle.ColorDepth) + StructDstImageHandle.ByteOfSkip;
            StructDstImageHandle.MaskImage = true;
            StructDstImageHandle.LenthOfPtr = (StructDstImageHandle.ByteOfWidth * StructDstImageHandle.Height);
            Memory.StructureToPtr<StructVegaHandle>(StructDstImageHandle, DstImageHandle);
            StructDstImageHandle.StructRefresh();
            StructDstImageHandle.ClearCanvas();

           // this.ShiftToCenter(Src_X_SIZE, Src_Y_SIZE, Dst_X_SIZE, Dst_Y_SIZE);
            Task_Rotate(6, Dst_Y_SIZE);

        }
        private void Task_Rotate(int NumOfTask , int Y_Size)
        {
            int[] StartY = new int[NumOfTask];
            int[] EndY = new int[NumOfTask];
            int thread_Y_size = Y_Size / NumOfTask;
            Action[] Act = new Action[NumOfTask];
            for(int i = 0 ; i < NumOfTask ; i ++)
            {
                StartY[i] = i * thread_Y_size;
                if (i == NumOfTask - 1) EndY[i] = Y_Size;
                else EndY[i] = (i + 1) * thread_Y_size;
                if (i == 0)
                {
                    Act[0] = new Action(delegate
                    {

                        sub_Rotate(StartY[0], EndY[0]);
                    });
                }
                else if (i == 1)
                {
                    Act[1] = new Action(delegate
                    {
                        sub_Rotate(StartY[1], EndY[1]);
                    });
                }
                else if (i == 2)
                {
                    Act[2] = new Action(delegate
                    {
                        sub_Rotate(StartY[2], EndY[2]);
                    });
                }
                else if (i == 3)
                {
                    Act[3] = new Action(delegate
                    {
                        sub_Rotate(StartY[3], EndY[3]);
                    });
                }
                else if (i == 4)
                {
                    Act[4] = new Action(delegate
                    {
                        sub_Rotate(StartY[4], EndY[4]);
                    });
                }
                else if (i == 5)
                {
                    Act[5] = new Action(delegate
                    {
                        sub_Rotate(StartY[5], EndY[5]);
                    });
                } 
            }
            Parallel.Invoke(Act);
        }
        unsafe private void sub_Rotate(int start_Y, int end_Y)
        {
            int Src_X_SIZE = StructSrcImageHandle.Width;
            int Src_Y_SIZE = StructSrcImageHandle.Height;
            int Dst_X_SIZE = StructDstImageHandle.Width;
            int Dst_Y_SIZE = StructDstImageHandle.Height;
            int Distance_Src_Dst_X = Dst_X_SIZE - Src_X_SIZE;
            int Distance_Src_Dst_Y = Dst_Y_SIZE - Src_Y_SIZE;

           
            Distance_Src_Dst_X /= 2;
            Distance_Src_Dst_Y /= 2;
            start_Y -= Distance_Src_Dst_Y;
            end_Y -= Distance_Src_Dst_Y;
            double Radiu = RotateDegree * Math.PI / 180;
            double SinValue = Math.Sin(Radiu);
            double CosValue = Math.Cos(Radiu);
            int Src_WidthByte = StructSrcImageHandle.ByteOfWidth;
            int Dst_WidthByte = StructDstImageHandle.ByteOfWidth;
            byte* SrcPtr = (byte*)StructSrcImageHandle.ImageDataPtr;
            byte* DstPtr = (byte*)StructDstImageHandle.ImageDataPtr;
            RotateCenterX = Src_X_SIZE / 2.0;
            RotateCenterY = Src_Y_SIZE / 2.0;
            Src_X_SIZE = StructSrcImageHandle.Width;
            Src_Y_SIZE = StructSrcImageHandle.Height;
            int DstWidthxY;
            int DstIndex;
            int SrcIndex;
            int m;
            int n;
            double p;
            double q;
            double Dst_Y;
            double Dst_X;
            double value = 0;
    
            for (int y = start_Y; y < end_Y; y++)
            {
                DstWidthxY = y * Dst_WidthByte;
                for (int x = -Distance_Src_Dst_X; x < Dst_X_SIZE - Distance_Src_Dst_X; x++)
                {
                    DstIndex = DstWidthxY + x;
                    Dst_Y = ((x - RotateCenterX) * SinValue + (y - RotateCenterY) * CosValue) + RotateCenterY;
                    Dst_X = ((x - RotateCenterX) * CosValue - (y - RotateCenterY) * SinValue) + RotateCenterX ;
                    if (Dst_Y > 0) m = (int)Dst_Y;
                    else m = (int)(Dst_Y - 1);
                    if (Dst_X > 0) n = (int)Dst_X;
                    else n = (int)(Dst_X - 1);
                    m = (int)Dst_Y ;
                    n = (int)Dst_X;
                    q = Dst_Y - m;
                    p = Dst_X - n;
                    if ((m > 0) && (m < Src_Y_SIZE - 1) && (n > 0) && (n < Src_X_SIZE - 1))
                    {
                        SrcIndex = m * Src_WidthByte + n;
                        value = (1.0 - q) * ((1.0 - p) * SrcPtr[SrcIndex] + p * SrcPtr[SrcIndex + 1]) + q * ((1.0 - p) * SrcPtr[SrcIndex + Src_WidthByte] + p * SrcPtr[SrcIndex + Src_WidthByte + 1]);
                    }
                    else
                    {
                        value = 0;
                    }
                    DstPtr[DstIndex + Distance_Src_Dst_X + Distance_Src_Dst_Y * Dst_WidthByte] = (byte)value;
                }
            }
        }

        public Size CalculateNewSize(int width, int height, double RotateAngle)
        {
            double r = Math.Sqrt(Math.Pow((double)width / 2d, 2d) + Math.Pow((double)height / 2d, 2d)); //半徑L
            double OriginalAngle = Math.Acos((width / 2d) / r) / Math.PI * 180d;  //對角線和X軸的角度θ
            double minW = 0d, maxW = 0d, minH = 0d, maxH = 0d; //最大和最小的 X、Y座標
            double[] drawPoint = new double[4];

            drawPoint[0] = (-OriginalAngle + RotateAngle) * Math.PI / 180d;
            drawPoint[1] = (OriginalAngle + RotateAngle) * Math.PI / 180d;
            drawPoint[2] = (180f - OriginalAngle + RotateAngle) * Math.PI / 180d;
            drawPoint[3] = (180f + OriginalAngle + RotateAngle) * Math.PI / 180d;

            foreach (double point in drawPoint) //由四個角的點算出X、Y的最大值及最小值
            {
                double x = r * Math.Cos(point);
                double y = r * Math.Sin(point);

                if (x < minW)
                    minW = x;
                if (x > maxW)
                    maxW = x;
                if (y < minH)
                    minH = y;
                if (y > maxH)
                    maxH = y;
            }
            int SizeX = (int)Math.Round((maxW - minW));
            int SizeY = (int)Math.Round((maxH - minH));
            return new Size(SizeX, SizeY);
        }
    }
}
