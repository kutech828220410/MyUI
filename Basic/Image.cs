using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
namespace Basic
{
    static public class MyImage
    {
        public static void SaveJpeg(this System.Drawing.Image img, string filePath, long quality)
        {
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;//給Compression無效

            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(myEncoder, quality);
            img.Save(filePath, jpgEncoder, myEncoderParameters);
        }
        public static void SaveJpeg(this System.Drawing.Image img,System.IO.Stream stream, long quality)
        {
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            img.Save(stream, GetEncoder(ImageFormat.Jpeg), encoderParameters);
        }
        public static byte[] ImageToJpegBytes(this Image image)
        {
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 90);

                image.Save(ms, ImageFormat.Jpeg);
                bytes = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(bytes, 0, (int)ms.Length);
                return bytes;
            }
        }
        public static byte[] ImageToBytes(this Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()會改變MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }
        public static Image BytesToImage(this byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }
        static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo codec = ImageCodecInfo.GetImageDecoders().Where(m => m.FormatID == format.Guid).FirstOrDefault();
            if (codec == null)
            {
                return null;
            }
            return codec;
        }

        public static Bitmap BytesToBitmap(this byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                Bitmap bitmap = new Bitmap(ms);
                return bitmap;
            }
        }

        public static byte[] BitmapToBytes(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // 你可以根據需要選擇其他的 ImageFormat
                return ms.ToArray();
            }
        }
        public static Bitmap ScaleImage(this Bitmap SrcBitmap, int dstWidth, int dstHeight)
        {
            Graphics g = null;
            try
            {
                Bitmap DstBitmap = new Bitmap(dstWidth, dstHeight);
                //按比例缩放           
                int width = (int)(SrcBitmap.Width * ((float)dstWidth / (float)SrcBitmap.Width));
                int height = (int)(SrcBitmap.Height * ((float)dstHeight / (float)SrcBitmap.Height));


                g = Graphics.FromImage(DstBitmap);
                g.Clear(Color.Transparent);

                //设置画布的描绘质量         
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(SrcBitmap, new Rectangle((width - width) / 2, (height - height) / 2, width, height), 0, 0, SrcBitmap.Width, SrcBitmap.Height, GraphicsUnit.Pixel);


                return DstBitmap;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (g != null)
                {
                    g.Dispose();
                }

            }
            return null;
        }
    }
}
