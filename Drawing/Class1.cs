using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace DrawingClass
{
    static public class Draw
    {
        static public void 文字左上繪製(String _str, PointF 文字位置, Font 文字字體, Color 文字顏色, Color 背景顏色, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            SizeF 文字面積 = AxCanvasHDC.MeasureString(_str, 文字字體);
            文字位置 = new PointF((float)文字位置.X * Zoom_X, (float)文字位置.Y * Zoom_Y);
            AxCanvasHDC.FillRectangle(new SolidBrush(背景顏色), new RectangleF(文字位置, 文字面積));
            AxCanvasHDC.DrawString(_str, 文字字體, new SolidBrush(文字顏色), 文字位置);
        }
        static public void 文字中心繪製(String _str, PointF 文字位置, Font 文字字體, Color 文字顏色, Color 背景顏色, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            SizeF 文字面積 = AxCanvasHDC.MeasureString(_str, 文字字體);
            文字位置 = new PointF((float)文字位置.X * Zoom_X, (float)文字位置.Y * Zoom_Y);
            文字位置.X = 文字位置.X - 文字面積.Width / 2;
            文字位置.Y = 文字位置.Y - 文字面積.Height / 2;
            AxCanvasHDC.FillRectangle(new SolidBrush(背景顏色), new RectangleF(文字位置, 文字面積));
            AxCanvasHDC.DrawString(_str, 文字字體, new SolidBrush(文字顏色), 文字位置);
        }
        static public void 文字右中繪製(String _str, PointF 文字位置, Font 文字字體, Color 文字顏色, Color 背景顏色, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            SizeF 文字面積 = AxCanvasHDC.MeasureString(_str, 文字字體);
            文字位置 = new PointF((float)文字位置.X * Zoom_X, (float)文字位置.Y * Zoom_Y);
            文字位置.X = 文字位置.X - 文字面積.Width;
            文字位置.Y = 文字位置.Y - 文字面積.Height / 2;
            AxCanvasHDC.FillRectangle(new SolidBrush(背景顏色), new RectangleF(文字位置, 文字面積));
            AxCanvasHDC.DrawString(_str, 文字字體, new SolidBrush(文字顏色), 文字位置);
        }
        static public void 文字左中繪製(String _str, PointF 文字位置, Font 文字字體, Color 文字顏色, Color 背景顏色, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            SizeF 文字面積 = AxCanvasHDC.MeasureString(_str, 文字字體);
            文字位置 = new PointF((float)文字位置.X * Zoom_X, (float)文字位置.Y * Zoom_Y);
            文字位置.X = (int)文字位置.X;
            文字位置.Y = (int)(文字位置.Y - 文字面積.Height / 2F);
            AxCanvasHDC.FillRectangle(new SolidBrush(背景顏色), new RectangleF(文字位置, 文字面積));
            AxCanvasHDC.DrawString(_str, 文字字體, new SolidBrush(文字顏色), 文字位置);
        }
        static public void 十字中心(PointF 十字位置, float 十字大小, Color 十字顏色, int 十字粗細, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            Pen _pen = new Pen(十字顏色, 十字粗細);
            PointF p1 = new PointF((十字位置.X - 十字大小 / 2) * Zoom_X, (十字位置.Y) * Zoom_Y);
            PointF p2 = new PointF((十字位置.X + 十字大小 / 2) * Zoom_X, (十字位置.Y) * Zoom_Y);
            PointF p3 = new PointF((十字位置.X) * Zoom_X, (十字位置.Y - 十字大小 / 2) * Zoom_Y);
            PointF p4 = new PointF((十字位置.X) * Zoom_X, (十字位置.Y + 十字大小 / 2) * Zoom_Y);
            AxCanvasHDC.DrawLine(_pen, p1, p2);
            AxCanvasHDC.DrawLine(_pen, p3, p4);
        }
        static public void 線段繪製(PointF P1, PointF P2, Color 線段顏色, float 線段粗細, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            Pen _pen = new Pen(線段顏色, 線段粗細);
            PointF p1 = new PointF((P1.X) * Zoom_X, (P1.Y) * Zoom_Y);
            PointF p2 = new PointF((P2.X) * Zoom_X, (P2.Y) * Zoom_Y);
            AxCanvasHDC.DrawLine(_pen, p1, p2);
        }
        static public void 方框繪製(PointF P1, Size 方框大小, Color 顏色, float 粗細, bool 填滿, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            Rectangle rect = new Rectangle((int)P1.X, (int)P1.Y, 方框大小.Width, 方框大小.Height);
            Pen _pen = new Pen(顏色, 粗細);
            PointF p1 = new PointF((P1.X) * Zoom_X, (P1.Y) * Zoom_Y);
            if (!填滿) AxCanvasHDC.DrawRectangle(_pen, rect);
            if (填滿) AxCanvasHDC.FillRectangle(new SolidBrush(顏色), new RectangleF(P1, 方框大小));
        }
        static public void 左括號繪製(PointF 連結點, Size size, Color 顏色, float 粗細, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            Rectangle rect = new Rectangle((int)連結點.X, (int)(連結點.Y - (size.Height / 2)), size.Width, size.Height);
            Pen _pen = new Pen(顏色, 粗細);
            AxCanvasHDC.DrawArc(_pen, rect, 90, 180);
        }
        static public void 左括號填滿(PointF 連結點, Size size, Color 顏色, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            Rectangle rect = new Rectangle((int)連結點.X, (int)(連結點.Y - (size.Height / 2)), size.Width, size.Height);
            AxCanvasHDC.FillEllipse(new SolidBrush(顏色), rect);
        }
        static public void 右括號繪製(PointF 連結點, Size size, Color 顏色, float 粗細, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            Rectangle rect = new Rectangle((int)(連結點.X-(size.Width)), (int)(連結點.Y - (size.Height / 2)), size.Width, size.Height);
            Pen _pen = new Pen(顏色, 粗細);
       
            AxCanvasHDC.DrawArc(_pen, rect, 270, 180);
        }
        static public void 右括號填滿(PointF 連結點, Size size, Color 顏色, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            Rectangle rect = new Rectangle((int)(連結點.X - (size.Width)), (int)(連結點.Y - (size.Height / 2)), size.Width, size.Height);
            AxCanvasHDC.FillEllipse(new SolidBrush(顏色), rect);
        }
        static public void 水平箭頭繪製(PointF P1, PointF P2, Color 線段顏色, float 線段粗細, float 箭頭大小, float 線段偏移, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            double 箭頭角度 = 30;
            Pen _pen = new Pen(線段顏色, 線段粗細);
            PointF P1_top = new PointF();
            PointF P1_but = new PointF();
            PointF P2_top = new PointF();
            PointF P2_but = new PointF();
            float X_dis = (float)(箭頭大小 * Math.Cos(箭頭角度 / 180D * Math.PI));
            float Y_dis = (float)(箭頭大小 * Math.Sin(箭頭角度 / 180D * Math.PI));
            P1_top.X = P1.X + X_dis;
            P1_top.Y = P1.Y - Y_dis;
            P1_but.X = P1.X + X_dis;
            P1_but.Y = P1.Y + Y_dis;

            P2_top.X = P2.X - X_dis;
            P2_top.Y = P2.Y - Y_dis;
            P2_but.X = P2.X - X_dis;
            P2_but.Y = P2.Y + Y_dis;
            PointF p1_basic = new PointF((P1.X) * Zoom_X, (P1.Y) * Zoom_Y);
            PointF p2_basic = new PointF((P2.X) * Zoom_X, (P2.Y) * Zoom_Y);
            PointF p1 = new PointF((P1.X) * Zoom_X, (P1.Y + 線段偏移) * Zoom_Y);
            PointF p2 = new PointF((P2.X) * Zoom_X, (P2.Y + 線段偏移) * Zoom_Y);
            PointF p1_top = new PointF((P1_top.X) * Zoom_X, (P1_top.Y + 線段偏移) * Zoom_Y);
            PointF p1_but = new PointF((P1_but.X) * Zoom_X, (P1_but.Y + 線段偏移) * Zoom_Y);
            PointF p2_top = new PointF((P2_top.X) * Zoom_X, (P2_top.Y + 線段偏移) * Zoom_Y);
            PointF p2_but = new PointF((P2_but.X) * Zoom_X, (P2_but.Y + 線段偏移) * Zoom_Y);
            AxCanvasHDC.DrawLine(_pen, p1, p1_basic);
            AxCanvasHDC.DrawLine(_pen, p2_basic, p2);
            AxCanvasHDC.DrawLine(_pen, p1, p2);
            AxCanvasHDC.DrawLine(_pen, p1, p1_top);
            AxCanvasHDC.DrawLine(_pen, p1, p1_but);
            AxCanvasHDC.DrawLine(_pen, p2, p2_top);
            AxCanvasHDC.DrawLine(_pen, p2, p2_but);
        }
        static public void 垂直箭頭繪製(PointF P1, PointF P2, Color 線段顏色, float 線段粗細, float 箭頭大小, float 線段偏移, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            double 箭頭角度 = 30;
            Pen _pen = new Pen(線段顏色, 線段粗細);
            PointF P1_left = new PointF();
            PointF P1_right = new PointF();
            PointF P2_left = new PointF();
            PointF P2_right = new PointF();
            float X_dis = (float)(箭頭大小 * Math.Sin(箭頭角度 / 180D * Math.PI));
            float Y_dis = (float)(箭頭大小 * Math.Cos(箭頭角度 / 180D * Math.PI));
            P1_left.X = P1.X - X_dis;
            P1_left.Y = P1.Y + Y_dis;
            P1_right.X = P1.X + X_dis;
            P1_right.Y = P1.Y + Y_dis;

            P2_left.X = P2.X - X_dis;
            P2_left.Y = P2.Y - Y_dis;
            P2_right.X = P2.X + X_dis;
            P2_right.Y = P2.Y - Y_dis;
            PointF p1_basic = new PointF((P1.X) * Zoom_X, (P1.Y) * Zoom_Y);
            PointF p2_basic = new PointF((P2.X) * Zoom_X, (P2.Y) * Zoom_Y);
            PointF p1 = new PointF((P1.X + 線段偏移) * Zoom_X, (P1.Y) * Zoom_Y);
            PointF p2 = new PointF((P2.X + 線段偏移) * Zoom_X, (P2.Y) * Zoom_Y);
            PointF p1_left = new PointF((P1_left.X + 線段偏移) * Zoom_X, (P1_left.Y) * Zoom_Y);
            PointF p1_right = new PointF((P1_right.X + 線段偏移) * Zoom_X, (P1_right.Y) * Zoom_Y);
            PointF p2_left = new PointF((P2_left.X + 線段偏移) * Zoom_X, (P2_left.Y) * Zoom_Y);
            PointF p2_right = new PointF((P2_right.X + 線段偏移) * Zoom_X, (P2_right.Y) * Zoom_Y);
            AxCanvasHDC.DrawLine(_pen, p1, p1_basic);
            AxCanvasHDC.DrawLine(_pen, p2_basic, p2);
            AxCanvasHDC.DrawLine(_pen, p1, p2);
            AxCanvasHDC.DrawLine(_pen, p1, p1_left);
            AxCanvasHDC.DrawLine(_pen, p1, p1_right);
            AxCanvasHDC.DrawLine(_pen, p2, p2_left);
            AxCanvasHDC.DrawLine(_pen, p2, p2_right);
        }
        static public void 左方形括號繪製(PointF 連結點, Size size, Color 顏色, float 粗細, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            Pen _pen = new Pen(顏色, 粗細);
            //  p1  _  p2
            //     | 
            // org |
            //     |_ 
            //  p3     p4
            PointF org = 連結點;
            PointF p1 = new PointF();
            PointF p2 = new PointF();
            PointF p3 = new PointF();
            PointF p4 = new PointF();
            p1.X = org.X;
            p1.Y = org.Y - size.Height;
            p2.X = org.X + size.Width;
            p2.Y = p1.Y;
            p3.X = org.X;
            p3.Y = org.Y + size.Height;
            p4.X = org.X + size.Width;
            p4.Y = p3.Y;
            AxCanvasHDC.DrawLine(_pen, p1, p2);
            AxCanvasHDC.DrawLine(_pen, p1, p3);
            AxCanvasHDC.DrawLine(_pen, p3, p4);

        }
        static public void 左方形括號填滿(PointF 連結點, Size size, Color 顏色, float 粗細, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            AxCanvasHDC.FillRectangle(new SolidBrush(顏色), new RectangleF(new PointF(連結點.X, 連結點.Y - (size.Height / 2)), size));
        }
        static public void 右方形括號繪製(PointF 連結點, Size size, Color 顏色, float 粗細, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            Pen _pen = new Pen(顏色, 粗細);
            //  p1 _  p2
            //      | 
            // org  |
            //     _| 
            //  p3    p4
            PointF org = 連結點;
            PointF p1 = new PointF();
            PointF p2 = new PointF();
            PointF p3 = new PointF();
            PointF p4 = new PointF();
            p1.X = org.X;
            p1.Y = org.Y - size.Height;
            p2.X = org.X - size.Width;
            p2.Y = p1.Y;
            p3.X = org.X;
            p3.Y = org.Y + size.Height;
            p4.X = org.X - size.Width;
            p4.Y = p3.Y;
            AxCanvasHDC.DrawLine(_pen, p1, p2);
            AxCanvasHDC.DrawLine(_pen, p1, p3);
            AxCanvasHDC.DrawLine(_pen, p3, p4);
        }
        static public void 右方形括號填滿(PointF 連結點, Size size, Color 顏色, float 粗細, Graphics AxCanvasHDC, float Zoom_X, float Zoom_Y)
        {
            AxCanvasHDC.FillRectangle(new SolidBrush(顏色), new RectangleF(new PointF(連結點.X - size.Width, 連結點.Y - (size.Height / 2)), size));
        }
        static public void 清除畫布(Graphics AxCanvasHDC)
        {
            AxCanvasHDC.Clear(Color.White);
        }

    }
      
}
