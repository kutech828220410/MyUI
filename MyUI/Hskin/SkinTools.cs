﻿
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using HsWin;

namespace HsWin
{
    public class SkinTools
    {
        /// <summary>
        /// 将图像转换成灰色介
        /// </summary>
        /// <returns>灰色图像</returns>
        public static Bitmap GaryImg(Bitmap b)
        {
            Bitmap bmp = b.Clone(new Rectangle(0, 0, b.Width, b.Height), PixelFormat.Format24bppRgb);
            b.Dispose();
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            byte[] byColorInfo = new byte[bmp.Height * bmpData.Stride];
            Marshal.Copy(bmpData.Scan0, byColorInfo, 0, byColorInfo.Length);
            for (int x = 0, xLen = bmp.Width; x < xLen; x++)
            {
                for (int y = 0, yLen = bmp.Height; y < yLen; y++)
                {
                    byColorInfo[y * bmpData.Stride + x * 3] =
                        byColorInfo[y * bmpData.Stride + x * 3 + 1] =
                        byColorInfo[y * bmpData.Stride + x * 3 + 2] =
                        GetAvg(
                        byColorInfo[y * bmpData.Stride + x * 3],
                        byColorInfo[y * bmpData.Stride + x * 3 + 1],
                        byColorInfo[y * bmpData.Stride + x * 3 + 2]);
                }
            }
            Marshal.Copy(byColorInfo, 0, bmpData.Scan0, byColorInfo.Length);
            bmp.UnlockBits(bmpData);
            return bmp;
        }
        private static byte GetAvg(byte b, byte g, byte r)
        {
            return (byte)((r + g + b) / 3);
        }

        /// <summary>
        /// 获取图片主色调
        /// </summary>
        /// <param name="back">图片</param>
        public static Color GetImageAverageColor(Bitmap back)
        {
            return BitmapHelper.GetImageAverageColor(back);
        }

        /// <summary>
        /// 判断颜色偏向于暗色或亮色(true为偏向于暗色，false位偏向于亮色。)
        /// </summary>
        /// <param name="c">要判断的颜色</param>
        /// <returns>true为偏向于暗色，false位偏向于亮色。</returns>
        public static bool ColorSlantsDarkOrBright(Color c)
        {
            HSL hsl = ColorConverterEx.ColorToHSL(c);
            if (hsl.Luminance < 0.15d)
            {
                return true;
            }
            else if (hsl.Luminance < 0.35d)
            {
                return true;
            }
            else if (hsl.Luminance < 0.85d)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 绘制组件圆角
        /// </summary>
        /// <param name="frm">要绘制的组件</param>
        /// <param name="RgnRadius">圆角大小</param>
        public static void CreateRegion(Control ctrl, int RgnRadius)
        {
            int Rgn = NativeMethods.CreateRoundRectRgn(0, 0, ctrl.ClientRectangle.Width + 1, ctrl.ClientRectangle.Height + 1, RgnRadius, RgnRadius);
            NativeMethods.SetWindowRgn(ctrl.Handle, Rgn, true);
        }

        /// <summary>
        /// 样式绘制圆角
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="bounds">范围</param>
        /// <param name="radius">圆角大小</param>
        /// <param name="roundStyle">圆角样式</param>
        public static void CreateRegion(
             Control control,
             Rectangle bounds,
             int radius,
             RoundStyle roundStyle)
        {
            using (GraphicsPath path =
                GraphicsPathHelper.CreatePath(
                bounds, radius, roundStyle, true))
            {
                Region region = new Region(path);
                path.Widen(Pens.White);
                region.Union(path);
                control.Region = region;
            }
        }

        /// <summary>
        /// 绘制四个角弧度为8
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="bounds">范围</param>
        public static void CreateRegion(
            Control control,
            Rectangle bounds)
        {
            CreateRegion(control, bounds, 8, RoundStyle.All);
        }

        /// <summary>
        /// 样式绘制圆角
        /// </summary>
        /// <param name="hWnd">控件句柄</param>
        /// <param name="radius">圆角</param>
        /// <param name="roundStyle">圆角样式</param>
        /// <param name="redraw">是否重画</param>
        public static void CreateRegion(
           IntPtr hWnd,
           int radius,
           RoundStyle roundStyle,
           bool redraw)
        {
            RECT bounds = new RECT();
            NativeMethods.GetWindowRect(hWnd, ref bounds);

            Rectangle rect = new Rectangle(
                Point.Empty, bounds.Size);

            if (roundStyle != RoundStyle.None)
            {
                using (GraphicsPath path =
                    GraphicsPathHelper.CreatePath(
                    rect, radius, roundStyle, true))
                {
                    using (Region region = new Region(path))
                    {
                        path.Widen(Pens.White);
                        region.Union(path);
                        IntPtr hDc = NativeMethods.GetWindowDC(hWnd);
                        try
                        {
                            using (Graphics g = Graphics.FromHdc(hDc))
                            {
                                NativeMethods.SetWindowRgn(hWnd, region.GetHrgn(g), redraw);
                            }
                        }
                        finally
                        {
                            NativeMethods.ReleaseDC(hWnd, hDc);
                        }
                    }
                }
            }
            else
            {
                IntPtr hRgn = NativeMethods.CreateRectRgn(0, 0, rect.Width, rect.Height);
                NativeMethods.SetWindowRgn(hWnd, hRgn, redraw);
            }
        }

        /// <summary>
        /// 设置图形边缘半透明
        /// </summary>
        /// <param name="p_Bitmap">图形</param>
        /// <param name="p_CentralTransparent">true中心透明 false边缘透明</param>
        /// <param name="p_Crossdirection">true横 false纵</param>
        /// <returns></returns>
        public static Bitmap BothAlpha(Bitmap p_Bitmap, bool p_CentralTransparent, bool p_Crossdirection)
        {
            Bitmap _SetBitmap = new Bitmap(p_Bitmap.Width, p_Bitmap.Height);
            Graphics _GraphisSetBitmap = Graphics.FromImage(_SetBitmap);
            _GraphisSetBitmap.DrawImage(p_Bitmap, new Rectangle(0, 0, p_Bitmap.Width, p_Bitmap.Height));
            _GraphisSetBitmap.Dispose();

            Bitmap _Bitmap = new Bitmap(_SetBitmap.Width, _SetBitmap.Height);
            Graphics _Graphcis = Graphics.FromImage(_Bitmap);

            Point _Left1 = new Point(0, 0);
            Point _Left2 = new Point(_Bitmap.Width, 0);
            Point _Left3 = new Point(_Bitmap.Width, _Bitmap.Height / 2);
            Point _Left4 = new Point(0, _Bitmap.Height / 2);

            if (p_Crossdirection)
            {
                _Left1 = new Point(0, 0);
                _Left2 = new Point(_Bitmap.Width / 2, 0);
                _Left3 = new Point(_Bitmap.Width / 2, _Bitmap.Height);
                _Left4 = new Point(0, _Bitmap.Height);
            }

            Point[] _Point = new Point[] { _Left1, _Left2, _Left3, _Left4 };
            PathGradientBrush _SetBruhs = new PathGradientBrush(_Point, WrapMode.TileFlipY);

            _SetBruhs.CenterPoint = new PointF(0, 0);
            _SetBruhs.FocusScales = new PointF(_Bitmap.Width / 2, 0);
            _SetBruhs.CenterColor = Color.FromArgb(0, 255, 255, 255);
            _SetBruhs.SurroundColors = new Color[] { Color.FromArgb(255, 255, 255, 255) };
            if (p_Crossdirection)
            {
                _SetBruhs.FocusScales = new PointF(0, _Bitmap.Height);
                _SetBruhs.WrapMode = WrapMode.TileFlipX;
            }

            if (p_CentralTransparent)
            {
                _SetBruhs.CenterColor = Color.FromArgb(255, 255, 255, 255);
                _SetBruhs.SurroundColors = new Color[] { Color.FromArgb(0, 255, 255, 255) };
            }

            _Graphcis.FillRectangle(_SetBruhs, new Rectangle(0, 0, _Bitmap.Width, _Bitmap.Height));
            _Graphcis.Dispose();

            BitmapData _NewData = _Bitmap.LockBits(new Rectangle(0, 0, _Bitmap.Width, _Bitmap.Height), ImageLockMode.ReadOnly, _Bitmap.PixelFormat);
            byte[] _NewBytes = new byte[_NewData.Stride * _NewData.Height];
            Marshal.Copy(_NewData.Scan0, _NewBytes, 0, _NewBytes.Length);
            _Bitmap.UnlockBits(_NewData);

            BitmapData _SetData = _SetBitmap.LockBits(new Rectangle(0, 0, _SetBitmap.Width, _SetBitmap.Height), ImageLockMode.ReadWrite, _SetBitmap.PixelFormat);
            byte[] _SetBytes = new byte[_SetData.Stride * _SetData.Height];
            Marshal.Copy(_SetData.Scan0, _SetBytes, 0, _SetBytes.Length);

            int _WriteIndex = 0;

            for (int i = 0; i != _SetData.Height; i++)
            {
                _WriteIndex = i * _SetData.Stride + 3;
                for (int z = 0; z != _SetData.Width; z++)
                {
                    _SetBytes[_WriteIndex] = _NewBytes[_WriteIndex];
                    _WriteIndex += 4;
                }
            }
            Marshal.Copy(_SetBytes, 0, _SetData.Scan0, _SetBytes.Length);
            _SetBitmap.UnlockBits(_SetData);
            return _SetBitmap;
        }

        /// <summary>
        /// 绘制发光字体
        /// </summary>
        /// <param name="Str">字体</param>
        /// <param name="F">字体样式</param>
        /// <param name="ColorFore">字体颜色</param>
        /// <param name="ColorBack">光圈颜色</param>
        /// <param name="BlurConsideration">光圈大小</param>
        /// <returns>Image格式图</returns>
        public static Image ImageLightEffect(string Str, Font F, Color ColorFore, Color ColorBack, int BlurConsideration)
        {
            Bitmap Var_Bitmap = null;//实例化Bitmap类
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))//实例化Graphics类
            {
                SizeF Var_Size = g.MeasureString(Str, F);//对字符串进行测量
                using (Bitmap Var_bmp = new Bitmap((int)Var_Size.Width, (int)Var_Size.Height))//通过文字的大小实例化Bitmap类
                using (Graphics Var_G_Bmp = Graphics.FromImage(Var_bmp))//实例化Bitmap类
                using (SolidBrush Var_BrushBack = new SolidBrush(Color.FromArgb(16, ColorBack.R, ColorBack.G, ColorBack.B)))//根据RGB的值定义画刷
                using (SolidBrush Var_BrushFore = new SolidBrush(ColorFore))//定义画刷
                {
                    Var_G_Bmp.SmoothingMode = SmoothingMode.HighQuality;//设置为高质量
                    Var_G_Bmp.InterpolationMode = InterpolationMode.HighQualityBilinear;//设置为高质量的收缩
                    Var_G_Bmp.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;//消除锯齿
                    Var_G_Bmp.DrawString(Str, F, Var_BrushBack, 0, 0);//给制文字
                    Var_Bitmap = new Bitmap(Var_bmp.Width + BlurConsideration, Var_bmp.Height + BlurConsideration);//根据辉光文字的大小实例化Bitmap类
                    using (Graphics Var_G_Bitmap = Graphics.FromImage(Var_Bitmap))//实例化Graphics类
                    {
                        Var_G_Bitmap.SmoothingMode = SmoothingMode.HighQuality;//设置为高质量
                        Var_G_Bitmap.InterpolationMode = InterpolationMode.HighQualityBilinear;//设置为高质量的收缩
                        Var_G_Bitmap.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;//消除锯齿
                        //遍历辉光文字的各象素点
                        for (int x = 0; x <= BlurConsideration; x++)
                        {
                            for (int y = 0; y <= BlurConsideration; y++)
                            {
                                Var_G_Bitmap.DrawImageUnscaled(Var_bmp, x, y);//绘制辉光文字的点
                            }
                        }
                        Var_G_Bitmap.DrawString(Str, F, Var_BrushFore, BlurConsideration / 2, BlurConsideration / 2);//绘制文字
                    }
                }
            }
            return Var_Bitmap;
        }

        /// <summary>
        /// 范围绘制发光字体
        /// </summary>
        /// <param name="Str">字体</param>
        /// <param name="F">字体样式</param>
        /// <param name="ColorFore">字体颜色</param>
        /// <param name="ColorBack">光圈颜色</param>
        /// <param name="BlurConsideration">光圈大小</param>
        /// <param name="rc">文字范围</param>
        /// <param name="auto">是否启用范围绘制发光字体</param>
        /// <returns>Image格式图</returns>
        public static Image ImageLightEffect(string Str, Font F, Color ColorFore, Color ColorBack, int BlurConsideration, Rectangle rc, bool auto)
        {
            Bitmap Var_Bitmap = null;//实例化Bitmap类
            StringFormat sf = new StringFormat(StringFormatFlags.NoWrap);
            sf.Trimming = auto ? StringTrimming.EllipsisWord : StringTrimming.None;
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))//实例化Graphics类
            {
                SizeF Var_Size = g.MeasureString(Str, F);//对字符串进行测量
                using (Bitmap Var_bmp = new Bitmap((int)Var_Size.Width, (int)Var_Size.Height))//通过文字的大小实例化Bitmap类
                using (Graphics Var_G_Bmp = Graphics.FromImage(Var_bmp))//实例化Bitmap类
                using (SolidBrush Var_BrushBack = new SolidBrush(Color.FromArgb(16, ColorBack.R, ColorBack.G, ColorBack.B)))//根据RGB的值定义画刷
                using (SolidBrush Var_BrushFore = new SolidBrush(ColorFore))//定义画刷
                {
                    Var_G_Bmp.SmoothingMode = SmoothingMode.HighQuality;//设置为高质量
                    Var_G_Bmp.InterpolationMode = InterpolationMode.HighQualityBilinear;//设置为高质量的收缩
                    Var_G_Bmp.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;//消除锯齿
                    Var_G_Bmp.DrawString(Str, F, Var_BrushBack, rc, sf);//给制文字
                    Var_Bitmap = new Bitmap(Var_bmp.Width + BlurConsideration, Var_bmp.Height + BlurConsideration);//根据辉光文字的大小实例化Bitmap类
                    using (Graphics Var_G_Bitmap = Graphics.FromImage(Var_Bitmap))//实例化Graphics类
                    {
                        if (ColorBack != Color.Transparent)
                        {
                            Var_G_Bitmap.SmoothingMode = SmoothingMode.HighQuality;//设置为高质量
                            Var_G_Bitmap.InterpolationMode = InterpolationMode.HighQualityBilinear;//设置为高质量的收缩
                            Var_G_Bitmap.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;//消除锯齿
                            //遍历辉光文字的各象素点
                            for (int x = 0; x <= BlurConsideration; x++)
                            {
                                for (int y = 0; y <= BlurConsideration; y++)
                                {
                                    Var_G_Bitmap.DrawImageUnscaled(Var_bmp, x, y);//绘制辉光文字的点
                                }
                            }
                        }
                        Var_G_Bitmap.DrawString(Str, F, Var_BrushFore, new Rectangle(new Point(Convert.ToInt32(BlurConsideration / 2), Convert.ToInt32(BlurConsideration / 2)), rc.Size), sf);//绘制文字
                    }
                }
            }
            return Var_Bitmap;
        }

        /// <summary>
        /// 执行一次鼠标点击
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public static void CursorClick(int x, int y)
        {
            int MOUSEEVENTF_LEFTDOWN = 0x2;
            int MOUSEEVENTF_LEFTUP = 0x4;
            //恢复一次鼠标点击
            NativeMethods.mouse_event(MOUSEEVENTF_LEFTDOWN, x * 65536 / 1024, y * 65536 / 768, 0, 0);
            NativeMethods.mouse_event(MOUSEEVENTF_LEFTUP, x * 65536 / 1024, y * 65536 / 768, 0, 0);
        }

        /// <summary>
        /// 图片缩放
        /// </summary>
        /// <param name="b">源图片Bitmap</param>
        /// <param name="dstWidth">目标宽度</param>
        /// <param name="dstHeight">目标高度</param>
        /// <returns>处理完成的图片 Bitmap</returns>
        public static Bitmap ResizeBitmap(Bitmap b, int dstWidth, int dstHeight)
        {
            Bitmap dstImage = new Bitmap(dstWidth, dstHeight);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dstImage);
            //   设置插值模式 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            //   设置平滑模式 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //用Graphic的DrawImage方法通过设置大小绘制新的图片实现缩放
            g.DrawImage(b, new Rectangle(0, 0, dstImage.Width, dstImage.Height), new Rectangle(0, 0, b.Width, b.Height), GraphicsUnit.Pixel);
            g.Save();
            g.Dispose();
            return dstImage;
        }

        #region 获取窗体不透明区域
        /// <summary> 
        /// 创建支持位图区域的控件（目前有button和form）
        /// </summary> 
        /// <param name="control">控件</param> 
        /// <param name="bitmap">位图</param>
        /// <param name="Alpha">小于此透明值的去除</param> 
        public static void CreateControlRegion(Control control, Bitmap bitmap, int Alpha)
        {
            //判断是否存在控件和位图
            if (control == null || bitmap == null)
                return;

            control.Width = bitmap.Width;
            control.Height = bitmap.Height;
            //当控件是form时
            if (control is System.Windows.Forms.Form)
            {
                //强制转换为FORM
                Form form = (Form)control;
                //当FORM的边界FormBorderStyle不为NONE时，应将FORM的大小设置成比位图大小稍大一点
                form.Width = control.Width;
                form.Height = control.Height;
                //没有边界
                form.FormBorderStyle = FormBorderStyle.None;
                //将位图设置成窗体背景图片
                form.BackgroundImage = bitmap;
                //计算位图中不透明部分的边界
                GraphicsPath graphicsPath = CalculateControlGraphicsPath(bitmap, Alpha);
                //应用新的区域
                form.Region = new Region(graphicsPath);
            }
            //当控件是button时
            else if (control is SkinButton)
            {
                //强制转换为 button
                SkinButton button = (SkinButton)control;
                //计算位图中不透明部分的边界
                GraphicsPath graphicsPath = CalculateControlGraphicsPath(bitmap, Alpha);
                //应用新的区域
                button.Region = new Region(graphicsPath);
            }
            else if (control is SkinProgressBar)
            {
                //强制转换为 SkinProgressBar
                SkinProgressBar Progressbar = (SkinProgressBar)control;
                //计算位图中不透明部分的边界
                GraphicsPath graphicsPath = CalculateControlGraphicsPath(bitmap, Alpha);
                //应用新的区域
                Progressbar.Region = new Region(graphicsPath);
            }
        }

        //计算位图中不透明部分的边界
        public static GraphicsPath CalculateControlGraphicsPath(Bitmap bitmap, int Alpha)
        {
            //创建 GraphicsPath
            GraphicsPath graphicsPath = new GraphicsPath();
            //第一个找到点的X
            int colOpaquePixel = 0;
            // 偏历所有行（Y方向）
            for (int row = 0; row < bitmap.Height; row++)
            {
                //重设
                colOpaquePixel = 0;
                //偏历所有列（X方向）
                for (int col = 0; col < bitmap.Width; col++)
                {
                    //如果是不需要透明处理的点则标记，然后继续偏历
                    if (bitmap.GetPixel(col, row).A >= Alpha)
                    {
                        //记录当前
                        colOpaquePixel = col;
                        //建立新变量来记录当前点
                        int colNext = col;
                        ///从找到的不透明点开始，继续寻找不透明点,一直到找到或则达到图片宽度 
                        for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
                            if (bitmap.GetPixel(colNext, row).A < Alpha)
                                break;
                        //将不透明点加到graphics path
                        graphicsPath.AddRectangle(new Rectangle(colOpaquePixel, row, colNext - colOpaquePixel, 1));
                        col = colNext;
                    }
                }
            }
            return graphicsPath;
        }
        #endregion
    }
    public class HSL
    {
        private int _hue;
        private double _saturation;
        private double _luminance;

        public int Hue
        {
            get { return _hue; }
            set
            {
                if (value < 0)
                {
                    _hue = 0;
                }
                else if (value <= 360)
                {
                    _hue = value;
                }
                else
                {
                    _hue = value % 360;
                }
            }
        }

        public double Saturation
        {
            get { return _saturation; }
            set
            {
                if (value < 0)
                {
                    _saturation = 0;
                }
                else
                {
                    _saturation = Math.Min(value, 1D);
                }
            }
        }

        public double Luminance
        {
            get { return _luminance; }
            set
            {
                if (value < 0)
                {
                    _luminance = 0;
                }
                else
                {
                    _luminance = Math.Min(value, 1D);
                }
            }
        }

        public HSL() { }

        public HSL(int hue, double saturation, double luminance)
        {
            Hue = hue;
            Saturation = saturation;
            Luminance = luminance;
        }

        public override string ToString()
        {
            return string.Format("HSL [H={0}, S={1}, L={2}]", _hue, _saturation, _luminance);
        }

    }
    public sealed class ColorConverterEx
    {
        private static readonly int[] BT907 =
            new int[] { 2125, 7154, 721, 10000 };
        private static readonly int[] RMY =
            new int[] { 500, 419, 81, 1000 };
        private static readonly int[] Y =
            new int[] { 299, 587, 114, 1000 };

        private ColorConverterEx() { }

        public static HSL RgbToHsl(RGB rgb)
        {
            HSL hsl = new HSL();
            RgbToHsl(rgb, hsl);
            return hsl;
        }

        public static void RgbToHsl(RGB rgb, HSL hsl)
        {
            double r = (rgb.R / 255.0);
            double g = (rgb.G / 255.0);
            double b = (rgb.G / 255.0);

            double min = Math.Min(Math.Min(r, g), b);
            double max = Math.Max(Math.Max(r, g), b);
            double delta = max - min;

            hsl.Luminance = (max + min) / 2;

            if (delta == 0)
            {
                hsl.Hue = 0;
                hsl.Saturation = 0.0;
            }
            else
            {
                hsl.Saturation = (hsl.Luminance < 0.5) ?
                    (delta / (max + min)) : (delta / (2 - max - min));

                double del_r = (((max - r) / 6) + (delta / 2)) / delta;
                double del_g = (((max - g) / 6) + (delta / 2)) / delta;
                double del_b = (((max - b) / 6) + (delta / 2)) / delta;
                double hue;

                if (r == max)
                {
                    hue = del_b - del_g;
                }
                else if (g == max)
                {
                    hue = (1.0 / 3) + del_r - del_b;
                }
                else
                {
                    hue = (2.0 / 3) + del_g - del_r;
                }

                if (hue < 0)
                {
                    hue += 1;
                }
                if (hue > 1)
                {
                    hue -= 1;
                }

                hsl.Hue = (int)(hue * 360);
            }
        }

        public static HSL ColorToHSL(Color color)
        {
            int hue;
            double luminance;
            double saturation;

            double r = (color.R / 255.0d);
            double g = (color.G / 255.0d);
            double b = (color.B / 255.0d);

            double min = Math.Min(Math.Min(r, g), b);
            double max = Math.Max(Math.Max(r, g), b);
            double delta = max - min;

            // get luminance value
            luminance = (max + min) / 2;

            if (delta == 0)
            {
                // gray color
                hue = 0;
                saturation = 0.0;
            }
            else
            {
                // get saturation value
                saturation = (luminance < 0.5) ?
                    (delta / (max + min)) : (delta / (2 - max - min));

                // get hue value
                double del_r = (((max - r) / 6) + (delta / 2)) / delta;
                double del_g = (((max - g) / 6) + (delta / 2)) / delta;
                double del_b = (((max - b) / 6) + (delta / 2)) / delta;
                double dHue;

                if (r == max)
                {
                    dHue = del_b - del_g;
                }
                else if (g == max)
                {
                    dHue = (1.0 / 3) + del_r - del_b;
                }
                else
                {
                    dHue = (2.0 / 3) + del_g - del_r;
                }

                // correct hue if needed
                if (dHue < 0)
                {
                    dHue += 1;
                }

                if (dHue > 1)
                {
                    dHue -= 1;
                }

                hue = (int)(dHue * 360);
            }

            return new HSL(hue, saturation, luminance);
        }

        public static Color HSLToColor(HSL hsl)
        {
            byte r;
            byte g;
            byte b;

            if (hsl.Saturation == 0)
            {
                // gray values
                r = g = b = (byte)(hsl.Luminance * 255);
            }
            else
            {
                double v1, v2;
                double hue = (double)hsl.Hue / 360;

                v2 = (hsl.Luminance < 0.5) ?
                    (hsl.Luminance * (1 + hsl.Saturation)) :
                    ((hsl.Luminance + hsl.Saturation) - (hsl.Luminance * hsl.Saturation));
                v1 = 2 * hsl.Luminance - v2;

                r = (byte)(255 * HueToRGB(v1, v2, hue + (1.0d / 3)));
                g = (byte)(255 * HueToRGB(v1, v2, hue));
                b = (byte)(255 * HueToRGB(v1, v2, hue - (1.0d / 3)));
            }

            return Color.FromArgb(r, g, b);
        }

        public static RGB HslToRgb(HSL hsl)
        {
            RGB rgb = new RGB();
            HslToRgb(hsl, rgb);
            return rgb;
        }

        public static void HslToRgb(HSL hsl, RGB rgb)
        {
            if (hsl.Saturation == 0)
            {
                rgb.R = rgb.G = rgb.B = (byte)(hsl.Luminance * 255);
            }
            else
            {
                double v1, v2;
                double hue = (double)hsl.Hue / 360;

                v2 = (hsl.Luminance < 0.5) ?
                    (hsl.Luminance * (1 + hsl.Saturation)) :
                    ((hsl.Luminance + hsl.Saturation) - (hsl.Luminance * hsl.Saturation));
                v1 = 2 * hsl.Luminance - v2;

                rgb.R = (byte)(255 * HueToRGB(v1, v2, hue + (1.0 / 3)));
                rgb.G = (byte)(255 * HueToRGB(v1, v2, hue));
                rgb.B = (byte)(255 * HueToRGB(v1, v2, hue - (1.0 / 3)));
            }
        }

        public static RGB RgbToGray(RGB source)
        {
            RGB dest = new RGB();
            RgbToGray(source, dest);
            return dest;
        }

        public static RGB RgbToGray(RGB source, GrayscaleStyle style)
        {
            RGB dest = new RGB();
            RgbToGray(source, dest, style);
            return dest;
        }

        public static void RgbToGray(RGB source, RGB dest)
        {
            RgbToGray(source, dest, GrayscaleStyle.BT907);
        }

        public static void RgbToGray(
            RGB source, RGB dest, GrayscaleStyle style)
        {
            byte gray = 127;
            switch (style)
            {
                case GrayscaleStyle.BT907:
                    gray = GetGray(source, BT907);
                    break;
                case GrayscaleStyle.RMY:
                    gray = GetGray(source, RMY);
                    break;
                case GrayscaleStyle.Y:
                    gray = GetGray(source, Y);
                    break;
            }

            dest.R = dest.G = dest.B = gray;
        }

        #region Private Methods

        private static double HueToRGB(double v1, double v2, double vH)
        {
            if (vH < 0)
            {
                vH += 1;
            }

            if (vH > 1)
            {
                vH -= 1;
            }

            if ((6 * vH) < 1)
            {
                return (v1 + (v2 - v1) * 6 * vH);
            }

            if ((2 * vH) < 1)
            {
                return v2;
            }

            if ((3 * vH) < 2)
            {
                return (v1 + (v2 - v1) * ((2.0 / 3) - vH) * 6);
            }

            return v1;
        }

        private static byte GetGray(RGB rgb, int[] coefficient)
        {
            return (byte)((rgb.R * coefficient[0] +
                rgb.G * coefficient[1] +
                rgb.B * coefficient[2]) / coefficient[3]);
        }

        #endregion
    }
    public class RGB
    {
        private byte _r;
        private byte _g;
        private byte _b;

        public const short RIndex = 2;
        public const short GIndex = 1;
        public const short BIndex = 0;

        public byte R
        {
            get { return _r; }
            set { _r = value; }
        }

        public byte G
        {
            get { return _g; }
            set { _g = value; }
        }

        public byte B
        {
            get { return _b; }
            set { _b = value; }
        }

        public Color Color
        {
            get { return Color.FromArgb(_r, _g, _b); }
            set
            {
                _r = value.R;
                _g = value.G;
                _b = value.B;
            }
        }

        public RGB() { }

        public RGB(byte r, byte g, byte b)
        {
            _r = r;
            _g = g;
            _b = b;
        }

        public RGB(Color color)
        {
            _r = color.R;
            _g = color.G;
            _b = color.B;
        }

        public override string ToString()
        {
            return string.Format("RGB [R={0}, G={1}, B={2}]", _r, _g, _b);
        }
    }
    public enum GrayscaleStyle
    {
        BT907 = 0,
        RMY = 1,
        Y = 2,
    }
}
