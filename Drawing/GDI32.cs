using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
namespace DrawingClass
{
    public class GDI32
    {
        [DllImport("gdi32.dll")]
        public static extern int SetDIBits(IntPtr hdc, IntPtr hBitmap, int nStartScan, int nNumScans, IntPtr lpBits, IntPtr lpBI, int wUsage);  
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("GDI32.dll")]
        public static extern bool DeleteObject(IntPtr objectHandle);
        [DllImport("gdi32.dll")]
        public static extern bool FillRgn(IntPtr hdc, IntPtr hrgn, IntPtr hbr);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(Int32 crColor);
        [DllImport("gdi32.dll")]
        public static extern int SetBkMode(IntPtr hdc, int iBkMode);
        public const int TRANSPARENT = 1;
        public const int OPAQUE = 2;
        [DllImport("gdi32.dll")]
        static extern uint SetBkColor(IntPtr hdc, int crColor);
        [DllImport("gdi32.dll")]
        static extern uint SetTextColor(IntPtr hdc, int crColor);
        [DllImport("gdi32", EntryPoint = "CreateFontW", CharSet = CharSet.Auto)]
        static extern IntPtr CreateFontW(
        [In] Int32 nHeight,
        [In] Int32 nWidth,
        [In] Int32 nEscapement,
        [In] Int32 nOrientation,
        [In] FontWeight fnWeight,
        [In] Boolean fdwItalic,
        [In] Boolean fdwUnderline,
        [In] Boolean fdwStrikeOut,
        [In] FontCharSet fdwCharSet,
        [In] FontPrecision fdwOutputPrecision,
        [In] FontClipPrecision fdwClipPrecision,
        [In] FontQuality fdwQuality,
        [In] FontPitchAndFamily fdwPitchAndFamily,
        [In] String lpszFace);
        [DllImport("gdi32.dll")]
        public static extern int GetTextFace(IntPtr hdc, int nCount,
        [Out] StringBuilder lpFaceName);
        public const Int32 LF_FACESIZE = 32;
        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern bool BitBlt(
        IntPtr hdcDest, // 目标设备的句柄
        int nXDest, // 目标对象的左上角的X坐标
        int nYDest, // 目标对象的左上角的Y坐标
        int nWidth, // 目标对象的矩形的宽度
        int nHeight, // 目标对象的矩形的长度
        IntPtr hdcSrc, // 源设备的句柄
        int nXSrc, // 源对象的左上角的X坐标
        int nYSrc, // 源对象的左上角的X坐标
        TernaryRasterOperations dwRop // 光栅的操作值
        );
        [DllImport("gdi32.dll", EntryPoint = "SetDIBitsToDevice", SetLastError = true)]
        public static extern int SetDIBitsToDevice([In] IntPtr hdc, int xDest, int yDest, uint w, uint h, int xSrc,int ySrc, uint startScan, uint cLines, [In] IntPtr lpvBits, [In] ref BITMAPINFO lpbmi, DIBColorTable colorUse);
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public BitmapCompressionMode biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
            public uint[] cols;
        }
        //=========================================================================================================================================

        //======================================================BitmapInfo結構=====================================================================
        //BitmapInfo   點陣圖資訊
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            public BITMAPINFOHEADER bmiHeader;
            public int bmiColors;
        }
        public enum BitmapCompressionMode : uint
        {
            BI_RGB = 0,
            BI_RLE8 = 1,
            BI_RLE4 = 2,
            BI_BITFIELDS = 3,
            BI_JPEG = 4,
            BI_PNG = 5
        }

        public enum DIBColorTable
        {
            DIB_RGB_COLORS = 0,    /* color table in RGBs */
            DIB_PAL_COLORS
        };    /* color table in palette indices */
        [DllImport("gdi32.dll")]
        public static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest,
        int nWidthDest, int nHeightDest,
        IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,TernaryRasterOperations dwRop);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetTextExtentPoint(IntPtr hdc, string lpString,int cbString, ref Size lpSize);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetTextMetrics(IntPtr hdc, out TEXTMETRIC lptm);
        [DllImport("gdi32.dll")]
        public static extern bool GetCharABCWidthsFloatW(IntPtr hdc, uint iFirstChar, uint iLastChar, [Out] ABCFLOAT[] lpABCF);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern bool TextOutW(IntPtr hdc, int nXStart, int nYStart,string lpString, int cbString);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCharWidth32(IntPtr hdc, uint iFirstChar, uint iLastChar,[Out] int[] lpBuffer);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int DrawText(IntPtr hdc, string lpStr, int nCount, ref Rect lpRect, dwDTFormat wFormat);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern bool DeleteDC(IntPtr hdc);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct ENUMLOGFONTEX
        {

            public LOGFONT elfLogFont;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]

            public string elfFullName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]

            public string elfStyle;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]

            public string elfScript;

        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

        public class LOGFONT
        {

            public int lfHeight;

            public int lfWidth;

            public int lfEscapement;

            public int lfOrientation;

            public FontWeight lfWeight;

            [MarshalAs(UnmanagedType.U1)]

            public bool lfItalic;

            [MarshalAs(UnmanagedType.U1)]

            public bool lfUnderline;

            [MarshalAs(UnmanagedType.U1)]

            public bool lfStrikeOut;

            public FontCharSet lfCharSet;

            public FontPrecision lfOutPrecision;

            public FontClipPrecision lfClipPrecision;

            public FontQuality lfQuality;

            public FontPitchAndFamily lfPitchAndFamily;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]

            public string lfFaceName;

            public override string ToString()
            {

                StringBuilder sb = new StringBuilder();

                sb.Append("LOGFONT\n");

                sb.AppendFormat(" lfHeight: {0}\n", lfHeight);

                sb.AppendFormat(" lfWidth: {0}\n", lfWidth);

                sb.AppendFormat(" lfEscapement: {0}\n", lfEscapement);

                sb.AppendFormat(" lfOrientation: {0}\n", lfOrientation);

                sb.AppendFormat(" lfWeight: {0}\n", lfWeight);

                sb.AppendFormat(" lfItalic: {0}\n", lfItalic);

                sb.AppendFormat(" lfUnderline: {0}\n", lfUnderline);

                sb.AppendFormat(" lfStrikeOut: {0}\n", lfStrikeOut);

                sb.AppendFormat(" lfCharSet: {0}\n", lfCharSet);

                sb.AppendFormat(" lfOutPrecision: {0}\n", lfOutPrecision);

                sb.AppendFormat(" lfClipPrecision: {0}\n", lfClipPrecision);

                sb.AppendFormat(" lfQuality: {0}\n", lfQuality);

                sb.AppendFormat(" lfPitchAndFamily: {0}\n", lfPitchAndFamily);

                sb.AppendFormat(" lfFaceName: {0}\n", lfFaceName);

                return sb.ToString();

            }

        }

        public enum FontWeight : int
        {

            FW_DONTCARE = 0,

            FW_THIN = 100,

            FW_EXTRALIGHT = 200,

            FW_LIGHT = 300,

            FW_NORMAL = 400,

            FW_MEDIUM = 500,

            FW_SEMIBOLD = 600,

            FW_BOLD = 700,

            FW_EXTRABOLD = 800,

            FW_HEAVY = 900,

        }

        public enum FontCharSet : byte
        {

            ANSI_CHARSET = 0,

            DEFAULT_CHARSET = 1,

            SYMBOL_CHARSET = 2,

            SHIFTJIS_CHARSET = 128,

            HANGEUL_CHARSET = 129,

            HANGUL_CHARSET = 129,

            GB2312_CHARSET = 134,

            CHINESEBIG5_CHARSET = 136,

            OEM_CHARSET = 255,

            JOHAB_CHARSET = 130,

            HEBREW_CHARSET = 177,

            ARABIC_CHARSET = 178,

            GREEK_CHARSET = 161,

            TURKISH_CHARSET = 162,

            VIETNAMESE_CHARSET = 163,

            THAI_CHARSET = 222,

            EASTEUROPE_CHARSET = 238,

            RUSSIAN_CHARSET = 204,

            MAC_CHARSET = 77,

            BALTIC_CHARSET = 186,

        }

        public enum FontPrecision : byte
        {

            OUT_DEFAULT_PRECIS = 0,

            OUT_STRING_PRECIS = 1,

            OUT_CHARACTER_PRECIS = 2,

            OUT_STROKE_PRECIS = 3,

            OUT_TT_PRECIS = 4,

            OUT_DEVICE_PRECIS = 5,

            OUT_RASTER_PRECIS = 6,

            OUT_TT_ONLY_PRECIS = 7,

            OUT_OUTLINE_PRECIS = 8,

            OUT_SCREEN_OUTLINE_PRECIS = 9,

            OUT_PS_ONLY_PRECIS = 10,

        }

        public enum FontClipPrecision : byte
        {

            CLIP_DEFAULT_PRECIS = 0,

            CLIP_CHARACTER_PRECIS = 1,

            CLIP_STROKE_PRECIS = 2,

            CLIP_MASK = 0xf,

            CLIP_LH_ANGLES = (1 << 4),

            CLIP_TT_ALWAYS = (2 << 4),

            CLIP_DFA_DISABLE = (4 << 4),

            CLIP_EMBEDDED = (8 << 4),

        }

        public enum FontQuality : byte
        {

            DEFAULT_QUALITY = 0,

            DRAFT_QUALITY = 1,

            PROOF_QUALITY = 2,

            NONANTIALIASED_QUALITY = 3,

            ANTIALIASED_QUALITY = 4,

            CLEARTYPE_QUALITY = 5,

            CLEARTYPE_NATURAL_QUALITY = 6,

        }

        [Flags]

        public enum FontPitchAndFamily : byte
        {

            DEFAULT_PITCH = 0,

            FIXED_PITCH = 1,

            VARIABLE_PITCH = 2,

            FF_DONTCARE = (0 << 4),

            FF_ROMAN = (1 << 4),

            FF_SWISS = (2 << 4),

            FF_MODERN = (3 << 4),

            FF_SCRIPT = (4 << 4),

            FF_DECORATIVE = (5 << 4),

        }

        /// <summary> 

        /// Enumeration for the raster operations used in BitBlt. 

        /// In C++ these are actually #define. But to use these 

        /// constants with C#, a new enumeration type is defined. 

        /// </summary> 

        public enum TernaryRasterOperations
        {

            SRCCOPY = 0x00CC0020, /* dest = source */

            SRCPAINT = 0x00EE0086, /* dest = source OR dest */

            SRCAND = 0x008800C6, /* dest = source AND dest */

            SRCINVERT = 0x00660046, /* dest = source XOR dest */

            SRCERASE = 0x00440328, /* dest = source AND (NOT dest ) */

            NOTSRCCOPY = 0x00330008, /* dest = (NOT source) */

            NOTSRCERASE = 0x001100A6, /* dest = (NOT src) AND (NOT dest) */

            MERGECOPY = 0x00C000CA, /* dest = (source AND pattern) */

            MERGEPAINT = 0x00BB0226, /* dest = (NOT source) OR dest */

            PATCOPY = 0x00F00021, /* dest = pattern */

            PATPAINT = 0x00FB0A09, /* dest = DPSnoo */

            PATINVERT = 0x005A0049, /* dest = pattern XOR dest */

            DSTINVERT = 0x00550009, /* dest = (NOT dest) */

            BLACKNESS = 0x00000042, /* dest = BLACK */

            WHITENESS = 0x00FF0062, /* dest = WHITE */

        };

        [Flags]

        public enum dwDTFormat : int
        {

            DT_TOP = 0, DT_LEFT = 0x00000000, DT_CENTER = 0x00000001, DT_RIGHT = 0x00000002,

            DT_VCENTER = 0x00000004, DT_BOTTOM = 0x00000008, DT_WORDBREAK = 0x00000010, DT_SINGLELINE = 0x00000020,

            DT_EXPANDTABS = 0x00000040, DT_TABSTOP = 0x00000080, DT_NOCLIP = 0x00000100, DT_EXTERNALLEADING = 0x00000200,

            DT_CALCRECT = 0x00000400, DT_NOPREFIX = 0x00000800, DT_INTERNAL = 0x00001000

        };

        public struct Rect
        {

            public int Left, Top, Right, Bottom;

            public Rect(Rectangle r)
            {

                this.Left = r.Left;

                this.Top = r.Top;

                this.Bottom = r.Bottom;

                this.Right = r.Right;

            }

        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

        public struct TEXTMETRIC
        {

            public Int32 tmHeight;

            public Int32 tmAscent;

            public Int32 tmDescent;

            public Int32 tmInternalLeading;

            public Int32 tmExternalLeading;

            public Int32 tmAveCharWidth;

            public Int32 tmMaxCharWidth;

            public Int32 tmWeight;

            public Int32 tmOverhang;

            public Int32 tmDigitizedAspectX;

            public Int32 tmDigitizedAspectY;

            public char tmFirstChar;

            public char tmLastChar;

            public char tmDefaultChar;

            public char tmBreakChar;

            public byte tmItalic;

            public byte tmUnderlined;

            public byte tmStruckOut;

            public byte tmPitchAndFamily;

            public byte tmCharSet;

        }
        [StructLayout(LayoutKind.Sequential)]
        public struct ABCFLOAT
        {
            public float abcfA;
            public float abcfB;
            public float abcfC;
        }

        public static void DrawPic(PictureBox _PictureBox, Bitmap drawBmp)
        {
            Graphics g = _PictureBox.CreateGraphics();
            IntPtr pTarget = g.GetHdc();

            IntPtr pSource = CreateCompatibleDC(pTarget);

            IntPtr pOrig = SelectObject(pSource, drawBmp.GetHbitmap());

            StretchBlt(pTarget, 0, 0, _PictureBox.ClientSize.Width, _PictureBox.ClientSize.Height, pSource, 0, 0, drawBmp.Width, drawBmp.Height, TernaryRasterOperations.SRCCOPY);

            IntPtr pNew = SelectObject(pSource, pOrig);

            DeleteObject(pNew);

            DeleteDC(pSource);

            g.ReleaseHdc(pTarget); 
        }
        public static void DrawImage(Graphics grDest, Bitmap grSrcBitmap)
        {                   
            IntPtr hdcDest = grDest.GetHdc();
            Graphics grSrc = Graphics.FromImage(grSrcBitmap);
            IntPtr hdcSrc = grSrc.GetHdc();
            IntPtr hBitmap = grSrcBitmap.GetHbitmap();
            IntPtr hOldObject = SelectObject(hdcSrc, hBitmap);
            BitBlt(hdcDest, 0, 0, grSrcBitmap.Width, grSrcBitmap.Height, hdcSrc, 0, 0, TernaryRasterOperations.SRCCOPY);
            if (hOldObject != IntPtr.Zero) SelectObject(hdcSrc, hOldObject);
            if (hBitmap != IntPtr.Zero) DeleteObject(hBitmap);
            if (hdcDest != IntPtr.Zero) grDest.ReleaseHdc(hdcDest);
            if (hdcSrc != IntPtr.Zero) grSrc.ReleaseHdc(hdcSrc);

        }
        public static int DrawToBitmap(Bitmap bmp, IntPtr DataSrcPtr, int ColorDepthRGB)
        {
            int code;
            Graphics Bitmap_Graphics = Graphics.FromImage(bmp);
            code = DrawToBitmap(Bitmap_Graphics.GetHdc(), bmp.Width, bmp.Height, DataSrcPtr, ColorDepthRGB);
            Bitmap_Graphics.ReleaseHdc();
            return code;
        }
        public static int DrawToBitmap(IntPtr BitmapHDC, int bmpWidth, int bmpHeight, IntPtr DataSrcPtr, int ColorDepthRGB)
        {
            DrawingClass.GDI32.BITMAPINFO info = new DrawingClass.GDI32.BITMAPINFO();
            info.bmiHeader = new DrawingClass.GDI32.BITMAPINFOHEADER();
            info.bmiHeader.biSize = Marshal.SizeOf(info.bmiHeader);
            info.bmiHeader.biWidth = bmpWidth;
            info.bmiHeader.biHeight = -bmpHeight;
            info.bmiHeader.biPlanes = 1;
           // info.bmiHeader.biBitCount = (short)(ColorDepthRGB * 8);
           // info.bmiHeader.biSizeImage = (int)(bmpWidth * bmpHeight * ColorDepthRGB);
            info.bmiHeader.biBitCount = (short)(8 * ColorDepthRGB);
            info.bmiHeader.biSizeImage = (int)(((bmpWidth + 7) & 0xFFFFFFF8) * bmpHeight / 8);
            info.bmiHeader.biCompression = DrawingClass.GDI32.BitmapCompressionMode.BI_RGB;
            return DrawingClass.GDI32.SetDIBitsToDevice(BitmapHDC, 0, 0, (uint)bmpWidth, (uint)bmpHeight, 0, 0, 0, (uint)bmpHeight, DataSrcPtr, ref info, GDI32.DIBColorTable.DIB_RGB_COLORS);
        }
        static uint MAKERGB(int r, int g, int b)
        {
            return ((uint)(b & 255)) | ((uint)((r & 255) << 8)) | ((uint)((g & 255) << 16));
        }
        public static ColorPalette GetColorPalette(uint nColors)
        {
            // Assume monochrome image.
            PixelFormat bitscolordepth = PixelFormat.Format1bppIndexed;
            ColorPalette palette;       // The Palette we are stealing
            Bitmap bitmap;              // The source of the stolen palette
            // Determine number of colors.
            if (nColors > 2)
                bitscolordepth = PixelFormat.Format4bppIndexed;
            if (nColors > 16)
                bitscolordepth = PixelFormat.Format8bppIndexed;
            // Make a new Bitmap object to get its Palette.
            bitmap = new Bitmap(1, 1, bitscolordepth);
            palette = bitmap.Palette;   // Grab the palette
            bitmap.Dispose();           // cleanup the source Bitmap
            return palette;             // Send the palette back
        }
        public static ColorPalette ColorPaletteGRAY = GetGRAYColorPalette();
        public static ColorPalette ColorPaletteARGB = GetARGBColorPalette();
        private static ColorPalette GetGRAYColorPalette()
        {
            List<Color> cl;
            ColorPalette pal = GetColorPalette(256);
            cl = new List<Color>();
            for (int i = 0; i < 256; i++)
            {
                cl.Add(Color.FromArgb(255, i, i, i));
            }

            for (int i = 0; i < cl.Count; i++)
            {
                pal.Entries[i] = cl[i];
            }
            return pal;
        }
        private static ColorPalette GetARGBColorPalette()
        {
            ColorPalette pal = GetColorPalette(0);
            return pal;
        }
    }
}
