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
using HsBasic;
using Basic;
using System.ComponentModel;
namespace HsBase
{
    public partial class Canvas : UserControl
    {
        private bool HDC_IS_USE = false;
        public ImageClass ImageClass;
        private ImageClass.ImagePixelFormat ImagePixelFormat = ImageClass.ImagePixelFormat.RGB24;
        private int _CanvasWidth = 300;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int CanvasWidth
        {
            get { return _CanvasWidth; }
            set
            {
                pictureBox_basic.Width = value;
                _CanvasWidth = value;
            }
        }
        private int _CanvasHeight = 300;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int CanvasHeight
        {
            get { return _CanvasHeight; }
            set
            {
                pictureBox_basic.Height = value;
                _CanvasHeight = value;
            }
        }
        private Bitmap CanvasBitmap;
        private Graphics CanvasBitmap_Graphics;
        public IntPtr CanvasHDC
        {
            get
            {
                return GetBimapHDC();
            }
            private set
            {

            }
        }
        private double _ZoomX = 0;
        public double ZoomX
        {
            get { return _ZoomX; }
            private set
            {
                _ZoomX = value;
            }
        }
        private double _ZoomY = 0;
        public double ZoomY
        {
            get { return _ZoomY; }
            private set
            {
                _ZoomY = value;
            }
        }

        public void SetScale(int Width ,int Height)
        {
            this.ZoomX = ((double)CanvasWidth / (double)Width);
            this.ZoomY = ((double)CanvasHeight / (double)Height);
        }
        public IntPtr GetBimapHDC()
        {
            return GetBimapHDC(ImageClass.ImagePixelFormat.RGB24);
        }
        private object lock_obj =new object();
        public IntPtr GetBimapHDC(ImageClass.ImagePixelFormat ImagePixelFormat)
        {
            bool flag = false;
            IntPtr HDC = IntPtr.Zero;

            if(ImageClass == null)ImageClass = new ImageClass(ImageClass.ImagePixelFormat.RGB24);

            if (_CanvasWidth != 0 && _CanvasHeight != 0)
            {
                if (_CanvasWidth != CanvasBitmap.Width) flag = true;
                if (_CanvasHeight != CanvasBitmap.Height) flag = true;

                if (flag == true)
                {
                    if (ImagePixelFormat == HsBasic.ImageClass.ImagePixelFormat.GRAY)
                    {
                        CanvasBitmap = new Bitmap(_CanvasWidth, _CanvasHeight, PixelFormat.Format24bppRgb);
                    }
                    else
                    {
                        CanvasBitmap = new Bitmap(_CanvasWidth, _CanvasHeight, PixelFormat.Format24bppRgb);
                    }
                    CanvasBitmap_Graphics = Graphics.FromImage(CanvasBitmap);
                }
                if (ImagePixelFormat != this.ImagePixelFormat)
                {
                    if (ImagePixelFormat == HsBasic.ImageClass.ImagePixelFormat.GRAY)
                    {
                        // CanvasBitmap.Palette = GDI32.ColorPaletteGRAY;
                        //ImageClass = new ImageClass(ImageClass.ImagePixelFormat.GRAY);
                    }
                    else
                    {

                        ImageClass = new ImageClass(ImageClass.ImagePixelFormat.RGB24);
                    }
                }
            }
            HDC = CanvasBitmap_Graphics.GetHdc();       
            return HDC;
        }
        public void ReleaseHDC()
        {            
            CanvasBitmap_Graphics.ReleaseHdc();
        }
        public Canvas()
        {
            InitializeComponent();
            Basic.Reflection.MakeDoubleBuffered(this, true);
            CanvasBitmap = new Bitmap(pictureBox_basic.ClientSize.Width, pictureBox_basic.ClientSize.Height,PixelFormat.Format24bppRgb);
            CanvasBitmap_Graphics = Graphics.FromImage(CanvasBitmap);
     
          //  CanvasHDC = GetBimapHDC();
        }
        public Graphics GetPicGraphics()
        {
            return this.pictureBox_basic.CreateGraphics();
        }
        public void DrawLine(PointF P1, PointF P2, Color 線段顏色, float 線段粗細, double ZoomX, double ZoomY)
        {
            Graphics g = Graphics.FromHdc(CanvasHDC);    
            Pen _pen = new Pen(線段顏色, 線段粗細);
            PointF p1 = new PointF((P1.X) * (float)ZoomX, (P1.Y) * (float)ZoomY);
            PointF p2 = new PointF((P2.X) * (float)ZoomX, (P2.Y) * (float)ZoomY);
            g.DrawLine(_pen, p1, p2);
            ReleaseHDC();
        }
        public void DrawRect(PointF P1, Size 方框大小, Color 顏色, float 粗細, bool 填滿, double ZoomX, double ZoomY)
        {
            Graphics g = Graphics.FromHdc(CanvasHDC);
            Draw.方框繪製(P1, 方框大小, 顏色, 粗細, 填滿, g, (float)ZoomX, (float)ZoomY);
            ReleaseHDC();
        }
        public void DrawString(String _str, PointF 文字位置, Font 文字字體, Color 文字顏色, Color 背景顏色, double ZoomX, double ZoomY)
        {
            Graphics g = Graphics.FromHdc(CanvasHDC);
            文字字體 = new Font(文字字體.Name, (int)(文字字體.Size * ZoomY));
            Draw.文字左上繪製(_str, 文字位置, 文字字體, 文字顏色, 背景顏色, g, (float)ZoomX, (float)ZoomY);
            ReleaseHDC();
        }
        public void CanvasRefresh(Bitmap bmp)
        {
            this.Invoke(new Action(delegate { pictureBox_basic.Image = bmp; }));        
        }
        public void CavasClear()
        {
            Graphics g = Graphics.FromHdc(this.CanvasHDC);
            DrawingClass.Draw.方框繪製(new PointF(0, 0), new Size(CanvasBitmap.Width, CanvasBitmap.Height), Color.Black, 1, true, g, 1, 1);
            this.ReleaseHDC();
            g.Dispose();
            CanvasRefresh();
        }
        public void CanvasRefresh()
        {
            if (ImageClass != null)
            {
                BitmapData bmData = CanvasBitmap.LockBits(new Rectangle(0, 0, CanvasBitmap.Width, CanvasBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                ImageClass.SetSurfacePtr(bmData.Scan0, bmData.Width, bmData.Height, ImagePixelFormat);
                CanvasBitmap.UnlockBits(bmData);
                
                try
                {
                    int code = ImageClass.DrawImage(pictureBox_basic.CreateGraphics().GetHdc(), 1, 1);
                }
                catch
                {

                }

            }
           
        }
        public event MouseEventHandler CanvasMouseDown;
        protected void OnCanvasMouseDown(object sender, MouseEventArgs e)
        {
            if (CanvasMouseDown != null) CanvasMouseDown(this, e);            
        }
        public event MouseEventHandler CanvasMouseMove;
        protected void OnCanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (CanvasMouseMove != null) CanvasMouseMove(this, e);
        }
        public event MouseEventHandler CanvasMouseUp;
        protected void OnCanvasMouseUp(object sender, MouseEventArgs e)
        {
            if (CanvasMouseUp != null) CanvasMouseUp(this, e);
        }

        private void pictureBox_basic_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnCanvasMouseDown(sender, e);
        }
        private void pictureBox_basic_MouseMove(object sender, MouseEventArgs e)
        {
            this.OnCanvasMouseMove(sender, e);
        }
        private void pictureBox_basic_MouseUp(object sender, MouseEventArgs e)
        {
            this.OnCanvasMouseUp(sender, e);
        }
    }
}
