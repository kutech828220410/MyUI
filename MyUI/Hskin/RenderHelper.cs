
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing.Imaging;
using HsWin;

namespace HsWin
{
    internal class RenderHelper
    {
        internal static void RenderBackgroundInternal(
            Graphics g,
            Rectangle rect,
            Color baseColor,
            Color borderColor,
            Color innerBorderColor,
            RoundStyle style,
            bool drawBorder,
            bool drawGlass,
            LinearGradientMode mode)
        {
            RenderBackgroundInternal(
                g,
                rect,
                baseColor,
                borderColor,
                innerBorderColor,
                style,
                8,
                drawBorder,
                drawGlass,
                mode);
        }

        internal static void RenderBackgroundInternal(
           Graphics g,
           Rectangle rect,
           Color baseColor,
           Color borderColor,
           Color innerBorderColor,
           RoundStyle style,
           int roundWidth,
           bool drawBorder,
           bool drawGlass,
           LinearGradientMode mode)
        {
            RenderBackgroundInternal(
                 g,
                 rect,
                 baseColor,
                 borderColor,
                 innerBorderColor,
                 style,
                 8,
                 0.45f,
                 drawBorder,
                 drawGlass,
                 mode);
        }

        internal static void RenderBackgroundInternal(
           Graphics g,
           Rectangle rect,
           Color baseColor,
           Color borderColor,
           Color innerBorderColor,
           RoundStyle style,
           int roundWidth,
           float basePosition,
           bool drawBorder,
           bool drawGlass,
           LinearGradientMode mode)
        {
            if (drawBorder)
            {
                rect.Width--;
                rect.Height--;
            }

            if (rect.Width == 0 || rect.Height == 0)
            {
                return;
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect, Color.Transparent, Color.Transparent, mode))
            {
                Color[] colors = new Color[4];
                colors[0] = GetColor(baseColor, 0, 35, 24, 9);
                colors[1] = GetColor(baseColor, 0, 13, 8, 3);
                colors[2] = baseColor;
                colors[3] = GetColor(baseColor, 0, 35, 24, 9);

                ColorBlend blend = new ColorBlend();
                blend.Positions = new float[] { 0.0f, basePosition, basePosition + 0.05f, 1.0f };
                blend.Colors = colors;
                brush.InterpolationColors = blend;
                if (style != RoundStyle.None)
                {
                    using (GraphicsPath path =
                        GraphicsPathHelper.CreatePath(rect, roundWidth, style, false))
                    {
                        g.FillPath(brush, path);
                    }

                    if (drawGlass)
                    {
                        if (baseColor.A > 80)
                        {
                            Rectangle rectTop = rect;

                            if (mode == LinearGradientMode.Vertical)
                            {
                                rectTop.Height = (int)(rectTop.Height * basePosition);
                            }
                            else
                            {
                                rectTop.Width = (int)(rect.Width * basePosition);
                            }
                            using (GraphicsPath pathTop = GraphicsPathHelper.CreatePath(
                                rectTop, roundWidth, RoundStyle.Top, false))
                            {
                                using (SolidBrush brushAlpha =
                                    new SolidBrush(Color.FromArgb(128, 255, 255, 255)))
                                {
                                    g.FillPath(brushAlpha, pathTop);
                                }
                            }
                        }
                        RectangleF glassRect = rect;
                        if (mode == LinearGradientMode.Vertical)
                        {
                            glassRect.Y = rect.Y + rect.Height * basePosition;
                            glassRect.Height = (rect.Height - rect.Height * basePosition) * 2;
                        }
                        else
                        {
                            glassRect.X = rect.X + rect.Width * basePosition;
                            glassRect.Width = (rect.Width - rect.Width * basePosition) * 2;
                        }
                        ControlPaintEx.DrawGlass(g, glassRect, 170, 0);
                    }

                    if (drawBorder)
                    {
                        using (GraphicsPath path =
                            GraphicsPathHelper.CreatePath(rect, roundWidth, style, false))
                        {
                            using (Pen pen = new Pen(borderColor))
                            {
                                g.DrawPath(pen, path);
                            }
                        }

                        rect.Inflate(-1, -1);
                        using (GraphicsPath path =
                            GraphicsPathHelper.CreatePath(rect, roundWidth, style, false))
                        {
                            using (Pen pen = new Pen(innerBorderColor))
                            {
                                g.DrawPath(pen, path);
                            }
                        }
                    }
                }
                else
                {
                    g.FillRectangle(brush, rect);

                    if (drawGlass)
                    {
                        if (baseColor.A > 80)
                        {
                            Rectangle rectTop = rect;
                            if (mode == LinearGradientMode.Vertical)
                            {
                                rectTop.Height = (int)(rectTop.Height * basePosition);
                            }
                            else
                            {
                                rectTop.Width = (int)(rect.Width * basePosition);
                            }
                            using (SolidBrush brushAlpha =
                                new SolidBrush(Color.FromArgb(128, 255, 255, 255)))
                            {
                                g.FillRectangle(brushAlpha, rectTop);
                            }
                        }
                        RectangleF glassRect = rect;
                        if (mode == LinearGradientMode.Vertical)
                        {
                            glassRect.Y = rect.Y + rect.Height * basePosition;
                            glassRect.Height = (rect.Height - rect.Height * basePosition) * 2;
                        }
                        else
                        {
                            glassRect.X = rect.X + rect.Width * basePosition;
                            glassRect.Width = (rect.Width - rect.Width * basePosition) * 2;
                        }
                        ControlPaintEx.DrawGlass(g, glassRect, 200, 0);
                    }

                    if (drawBorder)
                    {
                        using (Pen pen = new Pen(borderColor))
                        {
                            g.DrawRectangle(pen, rect);
                        }

                        rect.Inflate(-1, -1);
                        using (Pen pen = new Pen(innerBorderColor))
                        {
                            g.DrawRectangle(pen, rect);
                        }
                    }
                }
            }
        }

        internal static void RenderArrowInternal(
            Graphics g,
            Rectangle dropDownRect,
            ArrowDirection direction,
            Brush brush)
        {
            Point point = new Point(
                dropDownRect.Left + (dropDownRect.Width / 2),
                dropDownRect.Top + (dropDownRect.Height / 2));
            Point[] points = null;
            switch (direction)
            {
                case ArrowDirection.Left:
                    points = new Point[] {
                        new Point(point.X + 1, point.Y - 4),
                        new Point(point.X + 1, point.Y + 4),
                        new Point(point.X - 2, point.Y) };
                    break;

                case ArrowDirection.Up:
                    points = new Point[] {
                        new Point(point.X - 4, point.Y + 1),
                        new Point(point.X + 4, point.Y + 1),
                        new Point(point.X, point.Y - 2) };
                    break;

                case ArrowDirection.Right:
                    points = new Point[] {
                        new Point(point.X - 2, point.Y - 4),
                        new Point(point.X - 2, point.Y + 4),
                        new Point(point.X + 1, point.Y) };
                    break;

                default:
                    points = new Point[] {
                        new Point(point.X - 4, point.Y - 1),
                        new Point(point.X + 4, point.Y - 1),
                        new Point(point.X, point.Y + 2) };
                    break;
            }
            g.FillPolygon(brush, points);
        }

        internal static void RenderAlphaImage(
            Graphics g,
            Image image,
            Rectangle imageRect,
            float alpha)
        {
            using (ImageAttributes imageAttributes = new ImageAttributes())
            {
                ColorMap colorMap = new ColorMap();

                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

                ColorMap[] remapTable = { colorMap };

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                float[][] colorMatrixElements = {
                    new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                    new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                    new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                    new float[] {0.0f,  0.0f,  0.0f,  alpha, 0.0f},
                    new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}};
                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(
                    wmColorMatrix,
                    ColorMatrixFlag.Default,
                    ColorAdjustType.Bitmap);

                g.DrawImage(
                    image,
                    imageRect,
                    0,
                    0,
                    image.Width,
                    image.Height,
                    GraphicsUnit.Pixel,
                    imageAttributes);
            }
        }

        internal static Color GetColor(
            Color colorBase, int a, int r, int g, int b)
        {
            int a0 = colorBase.A;
            int r0 = colorBase.R;
            int g0 = colorBase.G;
            int b0 = colorBase.B;

            if (a + a0 > 255) { a = 255; } else { a = Math.Max(0, a + a0); }
            if (r + r0 > 255) { r = 255; } else { r = Math.Max(0, r + r0); }
            if (g + g0 > 255) { g = 255; } else { g = Math.Max(0, g + g0); }
            if (b + b0 > 255) { b = 255; } else { b = Math.Max(0, b + b0); }

            return Color.FromArgb(a, r, g, b);
        }
    }
    public sealed class ControlPaintEx
    {
        private ControlPaintEx() { }

        public static void DrawCheckedFlag(
            Graphics g, Rectangle rect, Color color)
        {
            PointF[] points = new PointF[3];
            points[0] = new PointF(
                rect.X + rect.Width / 4.5f,
                rect.Y + rect.Height / 2.5f);
            points[1] = new PointF(
                rect.X + rect.Width / 2.5f,
                rect.Bottom - rect.Height / 3f);
            points[2] = new PointF(
                rect.Right - rect.Width / 4.0f,
                rect.Y + rect.Height / 4.5f);
            using (Pen pen = new Pen(color, 2F))
            {
                g.DrawLines(pen, points);
            }
        }

        public static void DrawGlass(
            Graphics g, RectangleF glassRect,
            int alphaCenter, int alphaSurround)
        {
            DrawGlass(g, glassRect, Color.White, alphaCenter, alphaSurround);
        }

        public static void DrawGlass(
           Graphics g,
            RectangleF glassRect,
            Color glassColor,
            int alphaCenter,
            int alphaSurround)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(glassRect);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = Color.FromArgb(alphaCenter, glassColor);
                    brush.SurroundColors = new Color[] {
                        Color.FromArgb(alphaSurround, glassColor) };
                    brush.CenterPoint = new PointF(
                        glassRect.X + glassRect.Width / 2,
                        glassRect.Y + glassRect.Height / 2);
                    g.FillPath(brush, path);
                }
            }
        }

        public static void DrawBackgroundImage(
            Graphics g,
            Image backgroundImage,
            Color backColor,
            ImageLayout backgroundImageLayout,
            Rectangle bounds,
            Rectangle clipRect)
        {
            DrawBackgroundImage(
                g,
                backgroundImage,
                backColor,
                backgroundImageLayout,
                bounds,
                clipRect,
                Point.Empty,
                RightToLeft.No);
        }

        public static void DrawBackgroundImage(
            Graphics g,
            Image backgroundImage,
            Color backColor,
            ImageLayout backgroundImageLayout,
            Rectangle bounds,
            Rectangle clipRect,
            Point scrollOffset)
        {
            DrawBackgroundImage(
                g,
                backgroundImage,
                backColor,
                backgroundImageLayout,
                bounds,
                clipRect,
                scrollOffset,
                RightToLeft.No);
        }

        public static void DrawBackgroundImage(
            Graphics g,
            Image backgroundImage,
            Color backColor,
            ImageLayout backgroundImageLayout,
            Rectangle bounds,
            Rectangle clipRect,
            Point scrollOffset,
            RightToLeft rightToLeft)
        {
            if (g == null)
            {
                throw new ArgumentNullException("g");
            }
            if (backgroundImageLayout == ImageLayout.Tile)
            {
                using (TextureBrush brush = new TextureBrush(backgroundImage, WrapMode.Tile))
                {
                    if (scrollOffset != Point.Empty)
                    {
                        Matrix transform = brush.Transform;
                        transform.Translate((float)scrollOffset.X, (float)scrollOffset.Y);
                        brush.Transform = transform;
                    }
                    g.FillRectangle(brush, clipRect);
                    return;
                }
            }
            Rectangle rect = CalculateBackgroundImageRectangle(
                bounds,
                backgroundImage,
                backgroundImageLayout);
            if ((rightToLeft == RightToLeft.Yes) &&
                (backgroundImageLayout == ImageLayout.None))
            {
                rect.X += clipRect.Width - rect.Width;
            }
            using (SolidBrush brush2 = new SolidBrush(backColor))
            {
                g.FillRectangle(brush2, clipRect);
            }
            if (!clipRect.Contains(rect))
            {
                if ((backgroundImageLayout == ImageLayout.Stretch) ||
                    (backgroundImageLayout == ImageLayout.Zoom))
                {
                    rect.Intersect(clipRect);
                    g.DrawImage(backgroundImage, rect);
                }
                else if (backgroundImageLayout == ImageLayout.None)
                {
                    rect.Offset(clipRect.Location);
                    Rectangle destRect = rect;
                    destRect.Intersect(clipRect);
                    Rectangle rectangle3 = new Rectangle(Point.Empty, destRect.Size);
                    g.DrawImage(
                        backgroundImage,
                        destRect,
                        rectangle3.X,
                        rectangle3.Y,
                        rectangle3.Width,
                        rectangle3.Height,
                        GraphicsUnit.Pixel);
                }
                else
                {
                    Rectangle rectangle4 = rect;
                    rectangle4.Intersect(clipRect);
                    Rectangle rectangle5 = new Rectangle(
                        new Point(rectangle4.X - rect.X, rectangle4.Y - rect.Y),
                        rectangle4.Size);
                    g.DrawImage(
                        backgroundImage,
                        rectangle4,
                        rectangle5.X,
                        rectangle5.Y,
                        rectangle5.Width,
                        rectangle5.Height,
                        GraphicsUnit.Pixel);
                }
            }
            else
            {
                ImageAttributes imageAttr = new ImageAttributes();
                imageAttr.SetWrapMode(WrapMode.TileFlipXY);
                g.DrawImage(
                    backgroundImage,
                    rect,
                    0,
                    0,
                    backgroundImage.Width,
                    backgroundImage.Height,
                    GraphicsUnit.Pixel,
                    imageAttr);
                imageAttr.Dispose();
            }
        }

        public static void DrawScrollBarTrack(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Orientation orientation)
        {
            bool bHorizontal = orientation == Orientation.Horizontal;
            LinearGradientMode mode = bHorizontal ?
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            Blend blend = new Blend();
            blend.Factors = new float[] { 1f, 0.5f, 0f };
            blend.Positions = new float[] { 0f, 0.5f, 1f };

            DrawGradientRect(
                g,
                rect,
                begin,
                end,
                begin,
                begin,
                blend,
                mode,
                true,
                false);
        }

        public static void DrawScrollBarThumb(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Color border,
            Color innerBorder,
            Orientation orientation,
            bool changeColor)
        {
            if (changeColor)
            {
                Color tmp = begin;
                begin = end;
                end = tmp;
            }

            bool bHorizontal = orientation == Orientation.Horizontal;
            LinearGradientMode mode = bHorizontal ?
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            Blend blend = new Blend();
            blend.Factors = new float[] { 1f, 0.5f, 0f };
            blend.Positions = new float[] { 0f, 0.5f, 1f };

            if (bHorizontal)
            {
                rect.Inflate(0, -1);
            }
            else
            {
                rect.Inflate(-1, 0);
            }

            DrawGradientRoundRect(
                g,
                rect,
                begin,
                end,
                border,
                innerBorder,
                blend,
                mode,
                4,
                RoundStyle.All,
                true,
                true);
        }

        public static void DrawScrollBarArraw(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Color border,
            Color innerBorder,
            Color fore,
            Orientation orientation,
            ArrowDirection arrowDirection,
            bool changeColor)
        {
            if (changeColor)
            {
                Color tmp = begin;
                begin = end;
                end = tmp;
            }

            bool bHorizontal = orientation == Orientation.Horizontal;
            LinearGradientMode mode = bHorizontal ?
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            rect.Inflate(-1, -1);

            Blend blend = new Blend();
            blend.Factors = new float[] { 1f, 0.5f, 0f };
            blend.Positions = new float[] { 0f, 0.5f, 1f };

            DrawGradientRoundRect(
                g,
                rect,
                begin,
                end,
                border,
                innerBorder,
                blend,
                mode,
                4,
                RoundStyle.All,
                true,
                true);

            using (SolidBrush brush = new SolidBrush(fore))
            {
                RenderHelper.RenderArrowInternal(
                    g,
                    rect,
                    arrowDirection,
                    brush);
            }
        }

        public static void DrawScrollBarSizer(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end)
        {
            Blend blend = new Blend();
            blend.Factors = new float[] { 1f, 0.5f, 0f };
            blend.Positions = new float[] { 0f, 0.5f, 1f };

            DrawGradientRect(
                 g,
                 rect,
                 begin,
                 end,
                 begin,
                 begin,
                 blend,
                 LinearGradientMode.Horizontal,
                 true,
                 false);
        }

        internal static void DrawGradientRect(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Color border,
            Color innerBorder,
            Blend blend,
            LinearGradientMode mode,
            bool drawBorder,
            bool drawInnerBorder)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect, begin, end, mode))
            {
                brush.Blend = blend;
                g.FillRectangle(brush, rect);
            }

            if (drawBorder)
            {
                ControlPaint.DrawBorder(
                    g, rect, border, ButtonBorderStyle.Solid);
            }

            if (drawInnerBorder)
            {
                rect.Inflate(-1, -1);
                ControlPaint.DrawBorder(
                    g, rect, border, ButtonBorderStyle.Solid);
            }
        }

        internal static void DrawGradientRoundRect(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Color border,
            Color innerBorder,
            Blend blend,
            LinearGradientMode mode,
            int radios,
            RoundStyle roundStyle,
            bool drawBorder,
            bool drawInnderBorder)
        {
            using (GraphicsPath path = GraphicsPathHelper.CreatePath(
                rect, radios, roundStyle, true))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                      rect, begin, end, mode))
                {
                    brush.Blend = blend;
                    g.FillPath(brush, path);
                }

                if (drawBorder)
                {
                    using (Pen pen = new Pen(border))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }

            if (drawInnderBorder)
            {
                rect.Inflate(-1, -1);
                using (GraphicsPath path = GraphicsPathHelper.CreatePath(
                    rect, radios, roundStyle, true))
                {
                    using (Pen pen = new Pen(innerBorder))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }

        internal static Rectangle CalculateBackgroundImageRectangle(
            Rectangle bounds,
            Image backgroundImage,
            ImageLayout imageLayout)
        {
            Rectangle rectangle = bounds;
            if (backgroundImage != null)
            {
                switch (imageLayout)
                {
                    case ImageLayout.None:
                        rectangle.Size = backgroundImage.Size;
                        return rectangle;

                    case ImageLayout.Tile:
                        return rectangle;

                    case ImageLayout.Center:
                        {
                            rectangle.Size = backgroundImage.Size;
                            Size size = bounds.Size;
                            if (size.Width > rectangle.Width)
                            {
                                rectangle.X = (size.Width - rectangle.Width) / 2;
                            }
                            if (size.Height > rectangle.Height)
                            {
                                rectangle.Y = (size.Height - rectangle.Height) / 2;
                            }
                            return rectangle;
                        }
                    case ImageLayout.Stretch:
                        rectangle.Size = bounds.Size;
                        return rectangle;

                    case ImageLayout.Zoom:
                        {
                            Size size2 = backgroundImage.Size;
                            float num = ((float)bounds.Width) / ((float)size2.Width);
                            float num2 = ((float)bounds.Height) / ((float)size2.Height);
                            if (num >= num2)
                            {
                                rectangle.Height = bounds.Height;
                                rectangle.Width = (int)((size2.Width * num2) + 0.5);
                                if (bounds.X >= 0)
                                {
                                    rectangle.X = (bounds.Width - rectangle.Width) / 2;
                                }
                                return rectangle;
                            }
                            rectangle.Width = bounds.Width;
                            rectangle.Height = (int)((size2.Height * num) + 0.5);
                            if (bounds.Y >= 0)
                            {
                                rectangle.Y = (bounds.Height - rectangle.Height) / 2;
                            }
                            return rectangle;
                        }
                }
            }
            return rectangle;
        }
    }
}
