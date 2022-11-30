using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using DrawingClass;
using Basic;
using HsBasic;

namespace HsBase
{
    public class ROIBW8
    {
        private class CusorClass
        {
            public static IntPtr OnDragVegaHandle;
            public bool IsMouseDown = false;
            public TxHitHandle HitHandle = TxHitHandle.NONE;
            public Point OldPosition = new Point(0, 0);
            public bool CanSetNewPosition(TxHitHandle HitHandle ,Point Position)
            {
                if (this.HitHandle != HitHandle)
                {
                    this.SetCusor(HitHandle, Position);
                    return false;
                }
                if (this.OldPosition != Position) return true;
                return false;
            }
            public void SetCusor(TxHitHandle HitHandle, Point Position)
            {
                this.HitHandle = HitHandle;
                this.OldPosition = Position;
            }
        }
        private CusorClass cusorClass = new CusorClass();
        private bool CanDrag = false;
        public TxHitHandle HitHandle
        {
            get
            {
                return this.cusorClass.HitHandle;
            }
            private set
            {
                this.cusorClass.HitHandle = value;
            }
        }
        public bool IsMouseDown
        {
            get
            {
                return this.cusorClass.IsMouseDown;
            }
            set
            {
                if (value == false)
                {
                    this.cusorClass.HitHandle = TxHitHandle.NONE;
                }
                this.cusorClass.IsMouseDown = value;
            }
        }

        public enum TxHitHandle
        {
            NONE,
            INSIDE,
            NORTH,
            EAST,
            SOUTH,
            WEST,
            NORTH_WEST,
            SOUTH_WEST,
            NORTH_EAST,
            SOUTH_EAST
        }

        private ImageClass ImageClass = new ImageClass(ImageClass.ImagePixelFormat.GRAY);
        private StructVegaHandle StructParentHandle;
        private StructVegaHandle StructDstHandle;

        public IntPtr VegaHandle
        {
            get
            {
                return ImageClass.VegaHandle;
            }
            private set
            {

            }
        }

        private IntPtr _ParentHandle;
        public IntPtr ParentHandle
        {
            get
            {
                return _ParentHandle;
            }
            set
            {
                this._ParentHandle = value;
                SetImage(value);
            }
        }

        private int _OrgX = 300;
        public int OrgX
        {
            get
            {
                return _OrgX;
            }
            set
            {
                this._OrgX = value;
            }
        }

        private int _OrgY = 300;
        public int OrgY
        {
            get
            {
                return _OrgY;
            }
            set
            {
                this._OrgY = value;
            }
        }

        private int _ROIWidth = 100;
        public int ROIWidth
        {
            get
            {
                return _ROIWidth;
            }
            set
            {
                this._ROIWidth = value;
            }
        }

        private int _ROIHeight = 100;
        public int ROIHeight
        {
            get
            {
                return _ROIHeight;
            }
            set
            {
                this._ROIHeight = value;
            }
        }

        private int _RowPitch;
        public int RowPitch
        {
            get
            {
                return StructDstHandle.ByteOfWidth;
            }
            private set
            {
     
            }
        }

        public int ColorDepth
        {
            get
            {
                return StructParentHandle.ColorDepth;
            }
            private set
            {
                this.StructParentHandle.ColorDepth = value;
            }
        }

        private int _ColPitch;
        public int ColPitch
        {
            get
            {
                return _ColPitch;
            }
            private set
            {
                this._ColPitch = value;
            }
        }

        public int TotalWidth 
        {
            get
            {
                return StructParentHandle.Width;
            }
            private set
            {
                this.StructParentHandle.Width= value;
            }
        }
        public int TotalHeight
        {
            get
            {
                return StructParentHandle.Height;
            }
            private set
            {
                this.StructParentHandle.Height = value;
            }
        }



        public int LenthOfPtr
        {
            get
            {
                return ImageClass.LenthOfPtr;
            }
            private set
            {
            }
        }
        public int Width
        {
            get
            {
                this.ImageClass.RefreshVegaHandle();
                return ImageClass.Width;
            }
            private set
            {
            }
        }
        public int Height
        {
            get
            {
                this.ImageClass.RefreshVegaHandle();
                return ImageClass.Height;
            }
            private set
            {
            }
        }
        public int ByteOfSkip
        {
            get
            {
                return ImageClass.ByteOfSkip;
            }
            private set
            {
            }
        }
        public int ByteOfWidth
        {
            get
            {
                return ImageClass.ByteOfWidth;
            }
            private set
            {
            }
        }

        private SizeF _Scale = new SizeF(1, 1);
        public SizeF Scale
        {
            get
            {
                return this._Scale;
            }
            private set
            {
                this._Scale = value;
            }
        }

        private Point NORTH = new Point();
        private Point EAST = new Point();
        private Point SOUTH = new Point();
        private Point WEST = new Point();
        private Point NORTH_WEST = new Point();
        private Point SOUTH_WEST = new Point();
        private Point NORTH_EAST = new Point();
        private Point SOUTH_EAST = new Point();
        private Size CusorPointSize = new Size(10, 10);
        private Color CusorPointColor = Color.Gray;

        private void SetCusorPoint()
        {
            int Half_Width = _ROIWidth / 2;
            int Half_Height = _ROIHeight / 2;
            this.NORTH_WEST.X = _OrgX;
            this.NORTH_WEST.Y = _OrgY;
            this.NORTH.X = _OrgX + Half_Width;
            this.NORTH.Y = _OrgY;
            this.NORTH_EAST.X = _OrgX + _ROIWidth;
            this.NORTH_EAST.Y = _OrgY;
            this.WEST.X = _OrgX;
            this.WEST.Y = _OrgY + Half_Height;
            this.EAST.X = _OrgX + _ROIWidth;
            this.EAST.Y = _OrgY + Half_Height;
            this.SOUTH_WEST.X = _OrgX;
            this.SOUTH_WEST.Y = _OrgY + _ROIHeight;
            this.SOUTH.X = _OrgX + Half_Width;
            this.SOUTH.Y = _OrgY + _ROIHeight;
            this.SOUTH_EAST.X = _OrgX + _ROIWidth;
            this.SOUTH_EAST.Y = _OrgY + _ROIHeight;
        }
        private bool IsContain(Point CurrentCusorPoint, Point CusorPoint, Size CusorSize)
        {
            //NORTH_WEST       NORTH_EAST 
            //        ------------
            //        |          |
            //        |          |
            //        |          |
            //        |          |
            //        ------------
            //SOUTH_WEST       SOUTH_EAST
            bool flag_IsContain = true;
            int Half_CusorSizeWidth = CusorSize.Width / 2;
            int Half_CusorSizeHeight = CusorSize.Height / 2;
            Point NORTH_WEST = new Point(CusorPoint.X - Half_CusorSizeWidth, CusorPoint.Y - Half_CusorSizeHeight);
            Point NORTH_EAST = new Point(CusorPoint.X + Half_CusorSizeWidth, CusorPoint.Y - Half_CusorSizeHeight);
            Point SOUTH_WEST = new Point(CusorPoint.X - Half_CusorSizeWidth, CusorPoint.Y + Half_CusorSizeHeight);
            Point SOUTH_EAST = new Point(CusorPoint.X + Half_CusorSizeWidth, CusorPoint.Y + Half_CusorSizeHeight);

            if (CurrentCusorPoint.X < NORTH_WEST.X) flag_IsContain = false;
            if (CurrentCusorPoint.Y < NORTH_WEST.Y) flag_IsContain = false;
            if (CurrentCusorPoint.X > NORTH_EAST.X) flag_IsContain = false;
            if (CurrentCusorPoint.Y < NORTH_EAST.Y) flag_IsContain = false;

            if (CurrentCusorPoint.X < SOUTH_WEST.X) flag_IsContain = false;
            if (CurrentCusorPoint.Y > SOUTH_WEST.Y) flag_IsContain = false;
            if (CurrentCusorPoint.X > SOUTH_EAST.X) flag_IsContain = false;
            if (CurrentCusorPoint.Y > SOUTH_EAST.Y) flag_IsContain = false;
            return flag_IsContain;
        }

        public TxHitHandle HitTest(int X, int Y, double ZoomX, double ZoomY)
        {
            TxHitHandle HitHandle;
            Point CurrentPoint = new Point((int)(X / ZoomX), (int)(Y / ZoomY));
            Size CusorPointSizeBuf = new Size((int)(this.CusorPointSize.Width / ZoomX), (int)(this.CusorPointSize.Height / ZoomY));

            if (IsContain(CurrentPoint, this.NORTH_WEST, CusorPointSizeBuf))
            {
                HitHandle = TxHitHandle.NORTH_WEST;
            }
            else if (IsContain(CurrentPoint, this.NORTH, CusorPointSizeBuf))
            {
                HitHandle = TxHitHandle.NORTH;
            }
            else if (IsContain(CurrentPoint, this.NORTH_EAST, CusorPointSizeBuf))
            {
                HitHandle = TxHitHandle.NORTH_EAST;
            }
            else if (IsContain(CurrentPoint, this.EAST, CusorPointSizeBuf))
            {
                HitHandle = TxHitHandle.EAST;
            }
            else if (IsContain(CurrentPoint, this.WEST, CusorPointSizeBuf))
            {
                HitHandle = TxHitHandle.WEST;
            }
            else if (IsContain(CurrentPoint, this.SOUTH_WEST, CusorPointSizeBuf))
            {
                HitHandle = TxHitHandle.SOUTH_WEST;
            }
            else if (IsContain(CurrentPoint, this.SOUTH, CusorPointSizeBuf))
            {
                HitHandle = TxHitHandle.SOUTH;
            }
            else if (IsContain(CurrentPoint, this.SOUTH_EAST, CusorPointSizeBuf))
            {
                HitHandle = TxHitHandle.SOUTH_EAST;
            }
            else
            {
                HitHandle = TxHitHandle.NONE;
            }

            Size INSIDE_SIZE = new Size(ROIWidth, ROIHeight);
            Point INSIDE_CENTER_Po = new Point(SOUTH.X, WEST.Y);
            if (IsContain(CurrentPoint, INSIDE_CENTER_Po, INSIDE_SIZE))
            {
                HitHandle = TxHitHandle.INSIDE;
            }

            this.HitHandle = HitHandle;
            cusorClass.SetCusor(HitHandle, CurrentPoint);
            return HitHandle;
        }
        public void DragROI(TxHitHandle HitHandle, int X, int Y, double ZoomX, double ZoomY)
        {
            Point CurrentPoint = new Point((int)(X / ZoomX), (int)(Y / ZoomY));
            if (cusorClass.CanSetNewPosition(HitHandle, CurrentPoint) && this.CanDrag)
            {
                int OffsetX = cusorClass.OldPosition.X - CurrentPoint.X;
                int OffsetY = cusorClass.OldPosition.Y - CurrentPoint.Y;
                if (HitHandle == TxHitHandle.NORTH_WEST)
                {
                    this.OrgX -= OffsetX;
                    this.OrgY -= OffsetY;
                    this.ROIWidth += OffsetX;
                    this.ROIHeight+= OffsetY;
                }
                else if (HitHandle == TxHitHandle.NORTH)
                {
                    this.OrgY -= OffsetY;
                    this.ROIHeight += OffsetY;
                }
                else if (HitHandle == TxHitHandle.NORTH_EAST)
                {
                    this.ROIWidth -= OffsetX;
                    this.OrgY -= OffsetY;
                    this.ROIHeight += OffsetY;
                }
                else if (HitHandle == TxHitHandle.WEST)
                {
                    this.OrgX -= OffsetX;
                    this.ROIWidth += OffsetX;
                }
                else if (HitHandle == TxHitHandle.EAST)
                {
                    this.ROIWidth -= OffsetX;
                }
                else if (HitHandle == TxHitHandle.SOUTH_WEST)
                {
                    this.OrgX -= OffsetX;
                    this.ROIWidth += OffsetX;
                    this.ROIHeight -= OffsetY;
                }
                else if (HitHandle == TxHitHandle.SOUTH)
                {
                    this.ROIHeight -= OffsetY;
                }
                else if (HitHandle == TxHitHandle.SOUTH_EAST)
                {
                    this.ROIWidth -= OffsetX;
                    this.ROIHeight -= OffsetY;
                }
                else if (HitHandle == TxHitHandle.INSIDE)
                {
                    this.OrgX -= OffsetX;
                    this.OrgY -= OffsetY;
                }
            }
            cusorClass.SetCusor(HitHandle, CurrentPoint);
        }
        public void DrawRect(Canvas Canvas, double ZoomX, double ZoomY, Color FrameColor)
        {

            Graphics g = Graphics.FromHdc(Canvas.CanvasHDC);
            Draw.方框繪製(new PointF(OrgX, OrgY), new Size(ROIWidth, ROIHeight), FrameColor, 1, false, g, (float)ZoomX, (float)ZoomY);
            Canvas.ReleaseHDC();
            this.CanDrag = false;
        }
        public void DrawFrame(Canvas Canvas, double ZoomX, double ZoomY, Color FrameColor)
        {

            Graphics g = Graphics.FromHdc(Canvas.CanvasHDC);

            Draw.方框繪製(new PointF(OrgX, OrgY), new Size(ROIWidth, ROIHeight), FrameColor, 1, false, g, (float)ZoomX, (float)ZoomY);

            this.SetCusorPoint();

            Size CusorPointSize_Buf = new Size((int)(CusorPointSize.Width / ZoomX), (int)(CusorPointSize.Height / ZoomY));

            Draw.方框中心繪製(NORTH_WEST, CusorPointSize_Buf, CusorPointColor, 1, true, g, (float)ZoomX, (float)ZoomY);
            Draw.方框中心繪製(NORTH, CusorPointSize_Buf, CusorPointColor, 1, true, g, (float)ZoomX, (float)ZoomY);
            Draw.方框中心繪製(NORTH_EAST, CusorPointSize_Buf, CusorPointColor, 1, true, g, (float)ZoomX, (float)ZoomY);
            Draw.方框中心繪製(EAST, CusorPointSize_Buf, CusorPointColor, 1, true, g, (float)ZoomX, (float)ZoomY);
            Draw.方框中心繪製(WEST, CusorPointSize_Buf, CusorPointColor, 1, true, g, (float)ZoomX, (float)ZoomY);
            Draw.方框中心繪製(SOUTH_WEST, CusorPointSize_Buf, CusorPointColor, 1, true, g, (float)ZoomX, (float)ZoomY);
            Draw.方框中心繪製(SOUTH, CusorPointSize_Buf, CusorPointColor, 1, true, g, (float)ZoomX, (float)ZoomY);
            Draw.方框中心繪製(SOUTH_EAST, CusorPointSize_Buf, CusorPointColor, 1, true, g, (float)ZoomX, (float)ZoomY);


            Draw.方框中心繪製(NORTH_WEST, CusorPointSize_Buf, FrameColor, 1, false, g, (float)ZoomX, (float)ZoomY);
            Draw.方框中心繪製(NORTH, CusorPointSize_Buf, FrameColor, 1, false, g, (float)ZoomX, (float)ZoomY);
            Draw.方框中心繪製(NORTH_EAST, CusorPointSize_Buf, FrameColor, 1, false, g, (float)ZoomX, (float)ZoomY);
            Draw.方框中心繪製(EAST, CusorPointSize_Buf, FrameColor, 1, false, g, (float)ZoomX, (float)ZoomY);
            Draw.方框中心繪製(WEST, CusorPointSize_Buf, FrameColor, 1, false, g, (float)ZoomX, (float)ZoomY);
            Draw.方框中心繪製(SOUTH_WEST, CusorPointSize_Buf, FrameColor, 1, false, g, (float)ZoomX, (float)ZoomY);
            Draw.方框中心繪製(SOUTH, CusorPointSize_Buf, FrameColor, 1, false, g, (float)ZoomX, (float)ZoomY);
            Draw.方框中心繪製(SOUTH_EAST, CusorPointSize_Buf, FrameColor, 1, false, g, (float)ZoomX, (float)ZoomY);

            Canvas.ReleaseHDC();
            this.CanDrag = true;

           
        }

        private double defult_ZoomX = 0;
        private double defult_ZoomY = 0;
        private bool IsSetDefultEvent = false;
        private void canvas_CanvasMouseDown(object sender, MouseEventArgs e)
        {
            this.HitTest(e.X, e.Y, defult_ZoomX, defult_ZoomY);
            if (this.HitHandle != TxHitHandle.NONE)
            {
                CusorClass.OnDragVegaHandle = this.VegaHandle;
                this.IsMouseDown = true;
            }
        }
        private void canvas_CanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (CusorClass.OnDragVegaHandle == this.VegaHandle)
            {
                if (this.HitHandle != TxHitHandle.NONE)
                {
                    this.DragROI(this.HitHandle, e.X, e.Y, defult_ZoomX, defult_ZoomY);
                } 
            }                   
        }
        private void canvas_CanvasMouseUp(object sender, MouseEventArgs e)
        {
            this.IsMouseDown = false;
            CusorClass.OnDragVegaHandle = IntPtr.Zero;
        }
        public void SetDefultEvent(Canvas Canvas, double ZoomX, double ZoomY)
        {
            bool flag = false;
            if (this.defult_ZoomX != ZoomX) flag = true;
            if (this.defult_ZoomY != ZoomY) flag = true;
            this.defult_ZoomX = ZoomX;
            this.defult_ZoomY = ZoomY;
            if (IsSetDefultEvent && flag)
            {
                Canvas.CanvasMouseDown -= this.canvas_CanvasMouseDown;
                Canvas.CanvasMouseMove -= this.canvas_CanvasMouseMove;
                Canvas.CanvasMouseUp -= this.canvas_CanvasMouseUp;
                flag = true;
            }
            if (flag || !IsSetDefultEvent)
            {
                Canvas.CanvasMouseDown += this.canvas_CanvasMouseDown;
                Canvas.CanvasMouseMove += this.canvas_CanvasMouseMove;
                Canvas.CanvasMouseUp += this.canvas_CanvasMouseUp;
                IsSetDefultEvent = true;
            }
        }

        public void SetImage(IntPtr SurfaceHandle)
        {
            this.StructParentHandle = Memory.PtrToStructure<StructVegaHandle>(SurfaceHandle);
            int SrcOrgX = OrgX;
            int SrcOrgY = OrgY;
            this.StructDstHandle = Memory.PtrToStructure<StructVegaHandle>(VegaHandle);
            this.StructDstHandle.ByteOfSkip = this.ImageClass.GetBitmapSkip(ROIWidth, ColorDepth);
            this.StructDstHandle.Width = ROIWidth;
            this.StructDstHandle.Height = ROIHeight;
            this.StructDstHandle.ByteOfWidth = (ROIWidth * ColorDepth) + this.StructDstHandle.ByteOfSkip;
            this.StructDstHandle.ImagePixelFormat = HsBasic.ImageClass.ImagePixelFormat.GRAY;
            this.StructDstHandle.ColorDepth = 1;
            this.StructDstHandle.LenthOfPtr = (StructDstHandle.ByteOfWidth * StructDstHandle.Height);
            this.StructDstHandle.ParentHandleDataPtr = this._ParentHandle;
            Memory.StructureToPtr<StructVegaHandle>(StructDstHandle, VegaHandle);

            IntPtr SrcPtr = StructParentHandle.ImageDataPtr;
            IntPtr DstPtr = StructDstHandle.ImageDataPtr;
            SrcPtr += (SrcOrgX * ColorDepth);
            for (int i = 0; i < SrcOrgY; i++) SrcPtr += StructParentHandle.ByteOfWidth;

            for (int i = 0; i < this.StructDstHandle.Height; i++)
            {
                Memory.CopyPtr(SrcPtr, DstPtr, this.RowPitch);
                DstPtr += StructDstHandle.ByteOfWidth;
                SrcPtr += StructParentHandle.ByteOfWidth;
            }      
        }
        public void GetBitmap(ref Bitmap bmp)
        {
            ImageClass.GetBitmap(this.VegaHandle, ref bmp);
        }
    }
}
