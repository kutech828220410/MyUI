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
    public class ImageBW8
    {
        private ImageClass ImageClass = new ImageClass(ImageClass.ImagePixelFormat.GRAY);

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
        public int ColorDepth
        {
            get
            {
                return ImageClass.ColorDepth;
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

        public ImageBW8()
        {
        }
        public void SetSurfaceObj(IntPtr SurfaceHandle)
        {
            ImageClass.SetSurfaceObj(SurfaceHandle);
       
        }
        public void ReadBMP(ref Bitmap bmp)
        {
            ImageClass.ReadBMP(ref bmp);
        }
        public void ReadBMP(Bitmap bmp)
        {
            ImageClass.ReadBMP(ref bmp);
        }
        public void GetBitmap(ref Bitmap bmp)
        {
            ImageClass.GetBitmap(this.VegaHandle ,ref bmp);
        }
        public void SetSurfacePtr(IntPtr SurfacePtr, int Width, int Height)
        {
            ImageClass.SetSurfacePtr(SurfacePtr, Width, Height, HsBasic.ImageClass.ImagePixelFormat.GRAY);
        }
        public void DrawImage(PictureBox picturebox)
        {
            ImageClass.DrawImage(picturebox.CreateGraphics().GetHdc());
        }
        public void DrawImage(HsBase.Canvas Canvas, double ZoomX, double ZoomY)
        {
            ImageClass.DrawImage(Canvas, ZoomX, ZoomY);
        }
        public void DrawImage(Graphics g, double ZoomX, double ZoomY)
        {
            ImageClass.DrawImage(g.GetHdc(), ZoomX, ZoomY);
        }
        public void DrawImage(IntPtr hdc, double ZoomX, double ZoomY)
        {
            ImageClass.DrawImage(hdc, ZoomX, ZoomY);
        }
        public bool LoadFile(string FileName)
        {
            return ImageClass.LoadFile(FileName);
        }
    }
}
