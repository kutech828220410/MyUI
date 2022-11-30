using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using DrawingClass;
using Basic;

namespace HsBasic
{
    unsafe public struct StructVegaHandle
    {

        public IntPtr ParentHandleDataPtr;
        public IntPtr ImageDataPtr;
        public bool MaskImage;
        public double ZoomX;
        public double ZoomY;
        private int _LenthOfPtr;
        public int LenthOfPtr
        {
            get
            {
                return this._LenthOfPtr;
            }
            set
            {
                ImageDataLenth = value;
                //if (MaskImage) ImageMaskDataLenth = value;
                this._LenthOfPtr = value;
            }
        }
        private int _ImageDataLenth;
        public int ImageDataLenth
        {
            get
            {
                return this._ImageDataLenth;
            }
            set
            {
                if (this._ImageDataLenth < value)
                {
                    this._ImageDataLenth = value;
                    if (ImageDataPtr != IntPtr.Zero)
                    {
                        //Memory.Free(this.ImageDataPtr);
                        ImageDataPtr = IntPtr.Zero;
                    }
                    this._ImageDataLenth = value;
                    this.ImageDataPtr = Memory.GetMemoryFromStack(value);                    
                }
            }
        }
        public int Width;
        public int Height;
        public int ByteOfSkip;
        public int ColorDepth;
        public int ByteOfWidth;
        public uint IntegraValue;
        public ulong IntegraPowValue;
        public uint ValidPixcelsNum;

        public ImageClass.ImagePixelFormat ImagePixelFormat;
          
        public void CopyStruct(StructVegaHandle StructVegaHandle)
        {
            this.Width = StructVegaHandle.Width;
            this.Height = StructVegaHandle.Height;
            this.ByteOfSkip = StructVegaHandle.ByteOfSkip;
            this.ColorDepth = StructVegaHandle.ColorDepth;
            this.ImagePixelFormat = StructVegaHandle.ImagePixelFormat;
            this.ByteOfWidth = (this.Width + this.ByteOfSkip) * this.ColorDepth;
            this.LenthOfPtr = ((this.Width + this.ByteOfSkip) * this.ColorDepth * this.Height);
        }
        public int GetBitmapSkip()
        {
            int ByteOfSkip = Width * ColorDepth % 4;
            if (ByteOfSkip > 0) ByteOfSkip = 4 - ByteOfSkip;
            return ByteOfSkip;
        }
        #region Event
        public delegate void StructRefreshEventHandler();
        public event StructRefreshEventHandler StructRefreshEvent;
        public void StructRefresh()
        {
            if (StructRefreshEvent != null) this.StructRefreshEvent();
        }

        public delegate void ClearCanvasEventHandler();
        public event ClearCanvasEventHandler ClearCanvasEvent;
        public void ClearCanvas()
        {
            if (ClearCanvasEvent != null) this.ClearCanvasEvent();
        }

        #endregion
    }

    public class ImageClass
    {
        static public byte[] ImageDataArray;
        public void RefreshVegaHandle()
        {
            if (_VegaHandle != IntPtr.Zero) _StructVegaHandle = Memory.PtrToStructure<StructVegaHandle>(_VegaHandle);
        }
        public void ClearCanvasHandle()
        {
            this.SetSurface(this.Width, this.Height, this.imagePixelFormat);
        }
        unsafe public IntPtr ResetImageDataArray(int Lenth)
        {
            ImageDataArray = new byte[Lenth];
            fixed(byte* Ptr = ImageDataArray)
            {
                return (IntPtr) Ptr;
            }
        }
        private StructVegaHandle _StructVegaHandle;
        private IntPtr _VegaHandle;
        public IntPtr VegaHandle
        {
            get
            {
                _StructVegaHandle = Memory.PtrToStructure<StructVegaHandle>(_VegaHandle);
                return _VegaHandle;
            }
            private set
            {

            }
        }

        private IntPtr SurfacePtr;     
        private Bitmap _Bitmap;
        private PixelFormat _PixelFormat;
        public IntPtr ImageDataPtr
        {
            get
            {
                return this._StructVegaHandle.ImageDataPtr;
            }
            private set
            {
                this._StructVegaHandle.ImageDataPtr = value;
            }
        }

        public int LenthOfPtr
        {
            get
            {
                return _StructVegaHandle.LenthOfPtr;
            }
            private set
            {
                _StructVegaHandle.LenthOfPtr = value;       
            }
        }
        public int Width
        {
            get
            {
                return _StructVegaHandle.Width;
            }
            private set
            {
                _StructVegaHandle.Width = value;
            }
        }
        public int Height
        {
            get
            {
                return _StructVegaHandle.Height;
            }
            private set
            {
                _StructVegaHandle.Height = value;
            }
        }
        public int ColorDepth
        {
            get
            {
                return _StructVegaHandle.ColorDepth;
            }
            private set
            {
                _StructVegaHandle.ColorDepth = value;
            }
        }
        public int ByteOfSkip
        {
            get
            {
                return _StructVegaHandle.ByteOfSkip;
            }
            private set
            {
                _StructVegaHandle.ByteOfSkip = value;
            }
        }
        public int ByteOfWidth
        {
            get
            {
                return _StructVegaHandle.ByteOfWidth;
            }
            private set
            {
                _StructVegaHandle.ByteOfWidth = value;
            }
        }
        public double ZoomX
        {
            get
            {
                return this._StructVegaHandle.ZoomX;
            }
        }
        public double ZoomY
        {
            get
            {
                return this._StructVegaHandle.ZoomY;
            }
        }
        public uint IntegraValue
        {
            get
            {
                return _StructVegaHandle.IntegraValue;
            }
            private set
            {
                _StructVegaHandle.IntegraValue = value;
            }
        }
        public ulong IntegraPowValue
        {
            get
            {
                return _StructVegaHandle.IntegraPowValue;
            }
            private set
            {
                _StructVegaHandle.IntegraPowValue = value;
            }
        }
        public uint ValidPixcelsNum
        {
            get
            {
                return _StructVegaHandle.ValidPixcelsNum;
            }
            private set
            {
                _StructVegaHandle.ValidPixcelsNum = value;
            }
        }
        public bool MaskImage
        {
            get
            {
                return _StructVegaHandle.MaskImage;
            }
            private set
            {
       
            }
        }
        public enum ImagePixelFormat
        {
            ARGB32 = 0,
            RGB24 = 1,
            GRAY = 2,
        }
        public ImagePixelFormat imagePixelFormat
        {
            get
            {
                return _StructVegaHandle.ImagePixelFormat;
            }
            private set
            {
                _StructVegaHandle.ImagePixelFormat = value;
                if (value == ImagePixelFormat.GRAY)
                {
                    this._PixelFormat = PixelFormat.Format8bppIndexed;
                    this.ColorDepth = 1;
                }
                else if (value == ImagePixelFormat.RGB24)
                {
                    this._PixelFormat = PixelFormat.Format24bppRgb;
                    this.ColorDepth = 3;
                }
                else if (value == ImagePixelFormat.ARGB32)
                {
                    this._PixelFormat = PixelFormat.Format32bppArgb;
                    this.ColorDepth = 4;
                }
            }

        }
        BitmapData bmData;

        public void InitImage(int Width, int Height,double ZoomX,double ZoomY, ImagePixelFormat ImagePixelFormat, bool MaskImage)
        {
            this.imagePixelFormat = ImagePixelFormat;
            this._VegaHandle = Memory.Malloc(Memory.SizeOf<StructVegaHandle>());
            this._StructVegaHandle.MaskImage = MaskImage;
            this._StructVegaHandle.ImagePixelFormat = ImagePixelFormat;
            this._StructVegaHandle.Width = (int)Math.Round(Width * ZoomX);
            this._StructVegaHandle.Height = (int)Math.Round(Height * ZoomY);
            this._StructVegaHandle.ZoomX = ZoomX;
            this._StructVegaHandle.ZoomY = ZoomY;
            this._StructVegaHandle.ByteOfSkip = this._StructVegaHandle.GetBitmapSkip();
            this._StructVegaHandle.ByteOfWidth = this._StructVegaHandle.Width + ByteOfSkip;
            this._StructVegaHandle.ImageDataLenth = this._StructVegaHandle.ByteOfWidth * this._StructVegaHandle.ColorDepth * this._StructVegaHandle.Height;

            Memory.StructureToPtr<StructVegaHandle>(_StructVegaHandle, _VegaHandle);

            this.CheckImageDataLen(this.ByteOfWidth * this.Height);
        }
        public ImageClass()
        {
            this._StructVegaHandle.StructRefreshEvent += this.RefreshVegaHandle;
            this._StructVegaHandle.ClearCanvasEvent += this.ClearCanvasHandle;
            this._VegaHandle = Memory.Malloc(Memory.SizeOf<StructVegaHandle>());
            Memory.StructureToPtr<StructVegaHandle>(_StructVegaHandle, _VegaHandle);
        }
        public ImageClass(int Width , int Height ,ImagePixelFormat ImagePixelFormat)
        {
            this._StructVegaHandle.StructRefreshEvent += this.RefreshVegaHandle;
            this._StructVegaHandle.ClearCanvasEvent += this.ClearCanvasHandle;
            this.InitImage(Width, Height,1,1, ImagePixelFormat ,false);
        }
        public ImageClass(int Width, int Height, double ZoomX, double ZoomY, ImagePixelFormat ImagePixelFormat)
        {
            this._StructVegaHandle.StructRefreshEvent += this.RefreshVegaHandle;
            this._StructVegaHandle.ClearCanvasEvent += this.ClearCanvasHandle;
            this.InitImage(Width, Height, ZoomX, ZoomY, ImagePixelFormat, false);
        }
        public ImageClass(int Width, int Height, ImagePixelFormat ImagePixelFormat, bool MaskImage)
        {
            this._StructVegaHandle.StructRefreshEvent += this.RefreshVegaHandle;
            this._StructVegaHandle.ClearCanvasEvent += this.ClearCanvasHandle;
            this.InitImage(Width, Height, 1, 1, ImagePixelFormat, MaskImage);
        }
        public ImageClass(int Width, int Height, double ZoomX, double ZoomY, ImagePixelFormat ImagePixelFormat, bool MaskImage)
        {
            this._StructVegaHandle.StructRefreshEvent += this.RefreshVegaHandle;
            this._StructVegaHandle.ClearCanvasEvent += this.ClearCanvasHandle;
            this.InitImage(Width, Height, ZoomX, ZoomY, ImagePixelFormat, MaskImage);
        }
        public ImageClass(ImagePixelFormat ImagePixelFormat)
        {
            this._StructVegaHandle.StructRefreshEvent += this.RefreshVegaHandle;
            this._StructVegaHandle.ClearCanvasEvent += this.ClearCanvasHandle;
            this.InitImage(2592, 1944, 1, 1, ImagePixelFormat, false);
        }
        public ImageClass(ImagePixelFormat ImagePixelFormat, bool MaskImage)
        {
            this._StructVegaHandle.StructRefreshEvent += this.RefreshVegaHandle;
            this._StructVegaHandle.ClearCanvasEvent += this.ClearCanvasHandle;
            this.InitImage(2592, 1944, 1, 1, ImagePixelFormat, MaskImage);
        }

        unsafe public void GetInegralValue()
        {
            byte* SurfacePtr = (byte*)_StructVegaHandle.ImageDataPtr;
            int WidthOfByte = _StructVegaHandle.ByteOfWidth;
            uint Value = 0;
            int SurfaceIndex;
            int SurfaceWidthxY;
            uint IntegraValue = 0;
            ulong IntegraPowValue = 0;
            uint ValidPixcelsNum = 0;

            for (int y = 0; y < _StructVegaHandle.Height; y++)
            {
                SurfaceWidthxY = y * WidthOfByte;
                for (int x = 0; x < _StructVegaHandle.Width; x++)
                {
                    SurfaceIndex = SurfaceWidthxY + x;
                    Value = SurfacePtr[SurfaceIndex];
                    IntegraValue += (uint)Value;
                    IntegraPowValue += (ulong)(Value * Value);
                    if (Value != 0) ValidPixcelsNum++;
                }
            }
            _StructVegaHandle.IntegraValue = IntegraValue;
            _StructVegaHandle.IntegraPowValue = IntegraPowValue;
            _StructVegaHandle.ValidPixcelsNum = ValidPixcelsNum;
            Memory.StructureToPtr<StructVegaHandle>(_StructVegaHandle, _VegaHandle);
        }
        public void CheckImageDataLen(int Len)
        {
            if(this.LenthOfPtr != Len)
            {
                this.LenthOfPtr = Len;
            }
        }
        public void ReadBMP(ref Bitmap bmp)
        {
            if (IsNeedCreatNew(ref bmp))
            {
                _Bitmap = new Bitmap(bmp);
               
            }
            SetDataArray(ref bmp);
        }
        private bool IsNeedCreatNew(ref Bitmap bmp)
        {
            if (this._Bitmap == null) return true;
            if (this._Bitmap.Width != bmp.Width) return true;
            if (this._Bitmap.Height != bmp.Height) return true;
           // if (this._Bitmap.PixelFormat != bmp.PixelFormat) return true;
            return false;
        }
        unsafe private void SetDataArray(ref Bitmap bimage) 
        {
            
            BitmapData bmData = bimage.LockBits(new Rectangle(0, 0, bimage.Width, bimage.Height), ImageLockMode.ReadWrite, _PixelFormat);
            int stride = bmData.Stride;
            this.Width = bimage.Width;
            this.Height = bimage.Height;
            this.ByteOfSkip = GetBitmapSkip(this.Width, this.ColorDepth);
            this.SurfacePtr = bmData.Scan0;
            this.ByteOfWidth = this.Width * this.ColorDepth + this.ByteOfSkip;
            this.CheckImageDataLen(this.ByteOfWidth * this.Height);
            Memory.CopyPtr(SurfacePtr, _StructVegaHandle.ImageDataPtr, _StructVegaHandle.LenthOfPtr);
            bimage.UnlockBits(bmData);
            Memory.StructureToPtr<StructVegaHandle>(_StructVegaHandle, _VegaHandle);
        }
        private void CheckImageDataLen()
        {
        

        }
        private void FreeImgaeData()
        {
            if (ImageDataPtr != IntPtr.Zero)
            {
                Memory.Free(ImageDataPtr);
                ImageDataPtr = IntPtr.Zero;
            }

        }
        unsafe public void GetBitmap(IntPtr SurfaceHandle, ref Bitmap bmp)
        {
     
            _StructVegaHandle = Memory.PtrToStructure<StructVegaHandle>(SurfaceHandle);
            if (bmp == null)
            {
                if (_StructVegaHandle.ImagePixelFormat == ImagePixelFormat.RGB24)
                {
                    bmp = new Bitmap(_StructVegaHandle.Width, _StructVegaHandle.Height,PixelFormat.Format24bppRgb);
                }
                else
                {
                    bmp = new Bitmap(_StructVegaHandle.Width, _StructVegaHandle.Height, PixelFormat.Format8bppIndexed);
                }
            }
            if (bmp.Width != _StructVegaHandle.Width || bmp.Height != _StructVegaHandle.Height)
            {
                if (_StructVegaHandle.ImagePixelFormat == ImagePixelFormat.RGB24)
                {
                    bmp = new Bitmap(_StructVegaHandle.Width, _StructVegaHandle.Height, PixelFormat.Format24bppRgb);
                }
                else
                {
                    bmp = new Bitmap(_StructVegaHandle.Width, _StructVegaHandle.Height, PixelFormat.Format8bppIndexed);
                }
            }
            if (bmp != null)
            {
                if (_StructVegaHandle.ImagePixelFormat == ImagePixelFormat.GRAY)
                {
                    bmp.Palette = GDI32.ColorPaletteGRAY;
                    bmData = bmp.LockBits(new Rectangle(0, 0, _StructVegaHandle.Width, _StructVegaHandle.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
                    Memory.CopyPtr(ImageDataPtr, bmData.Scan0, _StructVegaHandle.LenthOfPtr);
                    bmp.UnlockBits(bmData);
                }
                else DrawingClass.GDI32.DrawToBitmap(bmp, ImageDataPtr, ColorDepth);
            }
        }
        unsafe public void SetSurfacePtr(IntPtr SurfacePtr, int Width, int Height, ImagePixelFormat ImagePixelFormat)
        {        
            this.SurfacePtr = SurfacePtr;
            this.Width = Width;
            this.Height = Height;
            this.imagePixelFormat = ImagePixelFormat;
            this.ByteOfSkip = GetBitmapSkip(this.Width, this.ColorDepth);
            this.ByteOfWidth = this.Width * this.ColorDepth + this.ByteOfSkip;
            this.CheckImageDataLen(this.ByteOfWidth * this.Height);           
            Memory.CopyPtr(SurfacePtr, _StructVegaHandle.ImageDataPtr, _StructVegaHandle.LenthOfPtr);
            Memory.StructureToPtr<StructVegaHandle>(_StructVegaHandle, _VegaHandle);
        }
        unsafe public void SetSurface(int Width, int Height, ImagePixelFormat ImagePixelFormat)
        {
            this.Width = Width;
            this.Height = Height;
            this.imagePixelFormat = ImagePixelFormat;
            this.ByteOfSkip = GetBitmapSkip(this.Width, this.ColorDepth);
            this.ByteOfWidth = this.Width * this.ColorDepth + this.ByteOfSkip;
            this.CheckImageDataLen(this.ByteOfWidth * this.Height);
            byte[] byte_Blink = new byte[ByteOfWidth];
            IntPtr DstPtr = _StructVegaHandle.ImageDataPtr;
            IntPtr SrcPtr;
            fixed (byte* SrcIntPtr = byte_Blink)
            {
                SrcPtr = (IntPtr)SrcIntPtr;
                for (int i = 0; i < this.Height; i++)
                {
                    Memory.CopyPtr(SrcPtr, DstPtr, this.ByteOfWidth);
                    DstPtr += _StructVegaHandle.ByteOfWidth;
                } 
            }
      

            Memory.StructureToPtr<StructVegaHandle>(_StructVegaHandle, _VegaHandle);
        }
        unsafe public void SetSurfacePtr(IntPtr SurfacePtr, int Width, int Height, double ZoomX ,double ZoomY, ImagePixelFormat ImagePixelFormat)
        {
            _StructVegaHandle.ZoomX = ZoomX;
            _StructVegaHandle.ZoomY = ZoomY;
            if (ZoomX == 1 && ZoomY == 1)
            {
                this.SetSurfacePtr(SurfacePtr, Width, Height, ImagePixelFormat);
                return;
            }
            this.SurfacePtr = SurfacePtr;
            this.Width = (int)Math.Round(Width * ZoomX);
            this.Height = (int)Math.Round(Height * ZoomY);
            this.imagePixelFormat = ImagePixelFormat;
            this.ByteOfSkip = GetBitmapSkip(this.Width, this.ColorDepth);
            this.ByteOfWidth = this.Width * this.ColorDepth + this.ByteOfSkip;
            this.CheckImageDataLen(this.ByteOfWidth * this.Height);

            byte* SrcPtr = (byte*)SurfacePtr;
            byte* DstPtr = (byte*)_StructVegaHandle.ImageDataPtr;
            int SrcWidth = GetBitmapSkip(Width, this.ColorDepth) + Width * this.ColorDepth;
            int DstWidth = this.ByteOfWidth;
            int SrcIndex = 0;
            int DstIndex = 0;
            int SrcWidthxY;
            int DstWidthxY;
            int m, n;
            double p;
            double q;
            double Dst_Y;
            double Dst_X;
            double value = 0;

            if (this.ColorDepth == 1)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    DstWidthxY = y * DstWidth;
                    for (int x = 0; x < this.Width; x++)
                    {
                        DstIndex = DstWidthxY + x;
                        Dst_Y = y / ZoomY;
                        Dst_X = x / ZoomX;

                        m = (int)Dst_Y;
                        n = (int)Dst_X;
                        q = Dst_Y - m;
                        p = Dst_X - n;
                        if ((m >= 0) && (m < Height) && (n >= 0) && (n < Width))
                        {
                            SrcIndex = m * SrcWidth + n;
                            value = (1.0 - q) * ((1.0 - p) * SrcPtr[SrcIndex] + p * SrcPtr[SrcIndex + 1]) + q * ((1.0 - p) * SrcPtr[SrcIndex + SrcWidth] + p * SrcPtr[SrcIndex + SrcWidth + 1]);
                        }
                        else
                        {
                            value = 0;
                        }
                        DstPtr[DstIndex] = (byte)value;
                    }
                }
            }
            else if (this.ColorDepth == 3)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    DstWidthxY = y * DstWidth;
                    if (y > 0) m = (int)(y / ZoomY + 0.5);
                    else m = (int)(y / ZoomY - 0.5);
                    SrcWidthxY = m * SrcWidth;
                    for (int x = 0; x < this.Width; x++)
                    {
                        DstIndex = DstWidthxY + x * this.ColorDepth;
                        if (x > 0) n = (int)(x / ZoomX + 0.5);
                        else n = (int)(x / ZoomX - 0.5);
                        if ((m >= 0) && (m < Height) && (n >= 0) && (n < Width))
                        {
                            SrcIndex = SrcWidthxY + n * this.ColorDepth;
                            DstPtr[DstIndex] = SrcPtr[SrcIndex];
                            DstPtr[DstIndex + 1] = SrcPtr[SrcIndex + 1];
                            DstPtr[DstIndex + 2] = SrcPtr[SrcIndex + 2];
                        }
                        else
                        {
                            DstPtr[DstIndex] = 255;
                            DstPtr[DstIndex + 1] = 255;
                            DstPtr[DstIndex + 2] = 255;
                        }
                    }
                }
            }
       
            Memory.StructureToPtr<StructVegaHandle>(_StructVegaHandle, _VegaHandle);
        }
        unsafe public void SetSurfaceObj(IntPtr SurfaceHandle)
        {
            _StructVegaHandle = Memory.PtrToStructure<StructVegaHandle>(SurfaceHandle);
            this.imagePixelFormat = _StructVegaHandle.ImagePixelFormat;
            this.Width = _StructVegaHandle.Width;
            this.Height = _StructVegaHandle.Height;
            this.ByteOfSkip = GetBitmapSkip(this.Width, this.ColorDepth);
            this.ByteOfWidth = this.Width * this.ColorDepth + this.ByteOfSkip;
            this.CheckImageDataLen(this.ByteOfWidth * this.Height);
            Memory.StructureToPtr<StructVegaHandle>(_StructVegaHandle, _VegaHandle);
        }
        unsafe public int DrawImage(IntPtr hdcDest)
        {
            return DrawImage(hdcDest, 1, 1);
        }
        unsafe public int DrawImage(HsBase.Canvas Canvas, double ZoomX, double ZoomY)
        {
            int code = 0;

            IntPtr hdcDest = Canvas.CanvasHDC;
            code = DrawImage(hdcDest, ZoomX, ZoomY);
            Canvas.ReleaseHDC();

            return code;
        }
        unsafe public int DrawImage(IntPtr hdcDest, double ZoomX, double ZoomY)
        {
            return DrawImage(hdcDest, ImageDataPtr, ZoomX, ZoomY);
        }
        private int ColorDepthRGB = 3;
        unsafe public int DrawImage(IntPtr hdcDest,IntPtr srcPtr, double ZoomX, double ZoomY)
        {          
            int X_SIZE_out = (int)(this.Width * ZoomX);
            int Y_SIZE_out = (int)(this.Height * ZoomY);
            int ByteOfSkip = X_SIZE_out * ColorDepthRGB % 4;
            if (ByteOfSkip > 0) ByteOfSkip = 4 - ByteOfSkip;
            byte[] ImageData_Out = new byte[(int)(X_SIZE_out * ColorDepthRGB + ByteOfSkip) * Y_SIZE_out];
            int SrcIndex = 0;
            int DstIndex = 0;
            int SrcWidth = this.ByteOfWidth;
            int DstWidth = X_SIZE_out * ColorDepthRGB + ByteOfSkip;
            int SrcWidthxY;
            int DstWidthxY;
            int m, n;         
            byte* SrcPtr = (byte*)srcPtr;
            bool MaskBlink = false;
            fixed (byte* Out_Ptr = ImageData_Out)
            {
                IntPtr DstPtr = (IntPtr)Out_Ptr;
                if (ColorDepth == 1)
                {
                    for (int y = 0; y < Y_SIZE_out; y++)
                    {
                        DstWidthxY = y * DstWidth;
                        if (y > 0) m = (int)(y / ZoomY + 0.5);
                        else m = (int)(y / ZoomY - 0.5);
                        SrcWidthxY = m * SrcWidth;
                        for (int x = 0; x < X_SIZE_out; x++)
                        {
                            DstIndex = DstWidthxY + x * ColorDepthRGB;
                            if (x > 0) n = (int)(x / ZoomX + 0.5);
                            else n = (int)(x / ZoomX - 0.5);
                            if ((m >= 0) && (m < this.Height) && (n >= 0) && (n < this.Width))
                            {
                                SrcIndex = SrcWidthxY + n * this.ColorDepth;
                                if (this.MaskImage)
                                {
                                    if (SrcPtr[SrcIndex] != 0)
                                    {
                                        Out_Ptr[DstIndex] = SrcPtr[SrcIndex];
                                        Out_Ptr[DstIndex + 1] = SrcPtr[SrcIndex];
                                        Out_Ptr[DstIndex + 2] = SrcPtr[SrcIndex];
                                    }
                                    else
                                    {
                                        if (!MaskBlink || true)
                                        {
                                            Out_Ptr[DstIndex] = 100;
                                            Out_Ptr[DstIndex + 1] = 100;
                                            Out_Ptr[DstIndex + 2] = 0;
                                            MaskBlink = true;
                                        }
                                        else if (MaskBlink) MaskBlink = false;
                               
                                    }
                                }
                                else
                                {
                                    Out_Ptr[DstIndex] = SrcPtr[SrcIndex];
                                    Out_Ptr[DstIndex + 1] = SrcPtr[SrcIndex];
                                    Out_Ptr[DstIndex + 2] = SrcPtr[SrcIndex];
                                }
                               
                            }
                            else
                            {
                                Out_Ptr[DstIndex] = 255;
                                Out_Ptr[DstIndex + 1] = 255;
                                Out_Ptr[DstIndex + 2] = 255;
                            }
                        }
                    }
                }
                else if (ColorDepth == 3)
                {
                    for (int y = 0; y < Y_SIZE_out; y++)
                    {
                        DstWidthxY = y * DstWidth;
                        if (y > 0) m = (int)(y / ZoomY + 0.5);
                        else m = (int)(y / ZoomY - 0.5);
                        SrcWidthxY = m * SrcWidth;
                        for (int x = 0; x < X_SIZE_out; x++)
                        {
                            DstIndex = DstWidthxY + x * ColorDepthRGB;
                            if (x > 0) n = (int)(x / ZoomX + 0.5);
                            else n = (int)(x / ZoomX - 0.5);
                            if ((m >= 0) && (m < this.Height) && (n >= 0) && (n < this.Width))
                            {
                                SrcIndex = SrcWidthxY + n * this.ColorDepth;
                                Out_Ptr[DstIndex] = SrcPtr[SrcIndex];
                                Out_Ptr[DstIndex + 1] = SrcPtr[SrcIndex + 1];
                                Out_Ptr[DstIndex + 2] = SrcPtr[SrcIndex + 2];           
                            }
                            else
                            {
                                Out_Ptr[DstIndex] = 255;
                                Out_Ptr[DstIndex + 1] = 255;
                                Out_Ptr[DstIndex + 2] = 255;
                            }
                        }
                    }
                }
                int code = DrawingClass.GDI32.DrawToBitmap(hdcDest, X_SIZE_out, Y_SIZE_out, DstPtr, 3);
                return code;
            }   
        }
        public int GetBitmapSkip(int ByteOfNumWidth, int ColorDepth)
        {
            int ByteOfSkip = ByteOfNumWidth * ColorDepth % 4;
            if (ByteOfSkip > 0) ByteOfSkip = 4 - ByteOfSkip;
            return ByteOfSkip;
        }
        public bool LoadFile(string FileName)
        {
            try
            {
                using (FileStream fs = File.OpenRead(@FileName))
                {
                    using (Bitmap bmp = (Bitmap)Image.FromStream(fs))
                    {
                        BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                        if (bmp.PixelFormat == PixelFormat.Format24bppRgb) this.imagePixelFormat = ImagePixelFormat.RGB24;
                        else if (bmp.PixelFormat == PixelFormat.Format8bppIndexed) this.imagePixelFormat = ImagePixelFormat.GRAY;
                        this.SetSurfacePtr(bmData.Scan0, bmData.Width, bmData.Height, this.imagePixelFormat);   
                    }
                    fs.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }       
        }
    }
}
