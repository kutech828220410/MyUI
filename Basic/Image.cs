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
        public static Image GetImage(this string fileName)
        {
            FileStream s = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader r = new BinaryReader(s);
            r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开
            byte[] bytes = r.ReadBytes((int)r.BaseStream.Length);
            r.Close();
            r.Dispose();
            s.Close();
            s.Dispose();
            MemoryStream memoryStream = new MemoryStream(bytes);
            Image img = Image.FromStream(memoryStream);
            memoryStream.Close();
            memoryStream.Dispose();
            return CopyImgByBytes((Bitmap)img);
        }
        public static Bitmap CopyImgByBytes(this Image image)
        {
            return CopyImgByBytes((Bitmap)image);
        }
        public static Bitmap CopyImgByBytes(this Bitmap bmp)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            Bitmap img = new Bitmap(bmp.Width, bmp.Height, bmp.PixelFormat);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            BitmapData imgData = img.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr optr = bmpData.Scan0;
            IntPtr nptr = imgData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] OriginalImgBytes = new byte[bytes];
            //把原始图像数据复制到byte[]数组中
            System.Runtime.InteropServices.Marshal.Copy(optr, OriginalImgBytes, 0, bytes);
            //把原始图像byte[]数据复制到新图像中
            System.Runtime.InteropServices.Marshal.Copy(OriginalImgBytes, 0, nptr, bytes);
            //解除锁定
            bmp.UnlockBits(bmpData);
            img.UnlockBits(imgData);
            return img;
        }

        // 将图像保存到指定路径，自动确定文件扩展名
        public static bool SaveImage(this Image image, string outputPath, string fileName)
        {
            try
            {
                string extension = GetImageExtension(image.RawFormat);
                if (extension != null)
                {
                    string filePath = Path.Combine(outputPath, fileName + extension);
                    image.Save(filePath);
                    Console.WriteLine("Image saved successfully at: " + filePath);
                    return true;
                }
                else
                {
                    Console.WriteLine("Unable to determine image format.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving image: " + ex.Message);
                return false;
            }
        }

        // 根据图像格式获取文件扩展名
        private static string GetImageExtension(ImageFormat format)
        {
            if (format.Equals(ImageFormat.Jpeg))
                return ".jpg";
            else if (format.Equals(ImageFormat.Png))
                return ".png";
            else if (format.Equals(ImageFormat.Bmp)|| format.Equals(ImageFormat.MemoryBmp))
                return ".bmp";
            else if (format.Equals(ImageFormat.Gif))
                return ".gif";
            else if (format.Equals(ImageFormat.Tiff))
                return ".tiff";
            else if (format.Equals(ImageFormat.Icon))
                return ".ico";
            else
                return null; // 未知格式

        }
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
                else if (format.Equals(ImageFormat.MemoryBmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
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

        public static string ImageToBase64(this Image image)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    byte[] imageBytes = image.ImageToBytes();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting image to base64: " + ex.Message);
                return null;
            }
        }
        public static Image Base64ToImage(this string base64String)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                Image image  = imageBytes.BytesToImage();
                return image;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting base64 to image: " + ex.Message);
                return null;
            }
        }
    }
}
