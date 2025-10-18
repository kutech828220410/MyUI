﻿
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using HsWin;

namespace HsWin
{
    public static class GraphicsPathHelper
    {

        public static GraphicsPath CreatePath(
            Rectangle rect, int radius, RoundStyle style, bool correction)
        {
            GraphicsPath path = new GraphicsPath();
            int radiusCorrection = correction ? 1 : 0;
            switch (style)
            {
                case RoundStyle.None:
                    path.AddRectangle(rect);
                    break;
                case RoundStyle.All:
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Y,
                        radius,
                        radius,
                        270,
                        90);
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Bottom - radius - radiusCorrection,
                        radius,
                        radius, 0, 90);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - radius - radiusCorrection,
                        radius,
                        radius,
                        90,
                        90);
                    break;
                case RoundStyle.Left:
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddLine(
                        rect.Right - radiusCorrection, rect.Y,
                        rect.Right - radiusCorrection, rect.Bottom - radiusCorrection);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - radius - radiusCorrection,
                        radius,
                        radius,
                        90,
                        90);
                    break;
                case RoundStyle.Right:
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Y,
                        radius,
                        radius,
                        270,
                        90);
                    path.AddArc(
                       rect.Right - radius - radiusCorrection,
                       rect.Bottom - radius - radiusCorrection,
                       radius,
                       radius,
                       0,
                       90);
                    path.AddLine(rect.X, rect.Bottom - radiusCorrection, rect.X, rect.Y);
                    break;
                case RoundStyle.Top:
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Y,
                        radius,
                        radius,
                        270,
                        90);
                    path.AddLine(
                        rect.Right - radiusCorrection, rect.Bottom - radiusCorrection,
                        rect.X, rect.Bottom - radiusCorrection);
                    break;
                case RoundStyle.Bottom:
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Bottom - radius - radiusCorrection,
                        radius,
                        radius,
                        0,
                        90);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - radius - radiusCorrection,
                        radius,
                        radius,
                        90,
                        90);
                    path.AddLine(rect.X, rect.Y, rect.Right - radiusCorrection, rect.Y);
                    break;
                case RoundStyle.BottomLeft:
                    path.AddArc(
                        rect.X,
                        rect.Bottom - radius - radiusCorrection,
                        radius,
                        radius,
                        90,
                        90);
                    path.AddLine(rect.X, rect.Y, rect.Right - radiusCorrection, rect.Y);
                    path.AddLine(
                        rect.Right - radiusCorrection,
                        rect.Y,
                        rect.Right - radiusCorrection,
                        rect.Bottom - radiusCorrection);
                    break;
                case RoundStyle.BottomRight:
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Bottom - radius - radiusCorrection,
                        radius,
                        radius,
                        0,
                        90);
                    path.AddLine(rect.X, rect.Bottom - radiusCorrection, rect.X, rect.Y);
                    path.AddLine(rect.X, rect.Y, rect.Right - radiusCorrection, rect.Y);
                    break;
            }
            path.CloseFigure();
            return path;
        }

        public static GraphicsPath CreateTrackBarThumbPath(
           Rectangle rect, ThumbArrowDirection arrowDirection)
        {
            GraphicsPath path = new GraphicsPath();
            PointF centerPoint = new PointF(
                rect.X + rect.Width / 2f, rect.Y + rect.Height / 2f);
            float offset = 0;

            switch (arrowDirection)
            {
                case ThumbArrowDirection.Left:
                case ThumbArrowDirection.Right:
                    offset = rect.Width / 2f - 4;
                    break;
                case ThumbArrowDirection.Up:
                case ThumbArrowDirection.Down:
                    offset = rect.Height / 2f - 4;
                    break;
            }

            switch (arrowDirection)
            {
                case ThumbArrowDirection.Left:
                    path.AddLine(
                        rect.X, centerPoint.Y, rect.X + offset, rect.Y);
                    path.AddLine(
                        rect.Right, rect.Y, rect.Right, rect.Bottom);
                    path.AddLine(
                        rect.X + offset, rect.Bottom, rect.X, centerPoint.Y);
                    break;
                case ThumbArrowDirection.Right:
                    path.AddLine(
                        rect.Right, centerPoint.Y, rect.Right - offset, rect.Bottom);
                    path.AddLine(
                        rect.X, rect.Bottom, rect.X, rect.Y);
                    path.AddLine(
                        rect.Right - offset, rect.Y, rect.Right, centerPoint.Y);
                    break;
                case ThumbArrowDirection.Up:
                    path.AddLine(
                        centerPoint.X, rect.Y, rect.X, rect.Y + offset);
                    path.AddLine(
                        rect.X, rect.Bottom, rect.Right, rect.Bottom);
                    path.AddLine(
                        rect.Right, rect.Y + offset, centerPoint.X, rect.Y);
                    break;
                case ThumbArrowDirection.Down:
                    path.AddLine(
                         centerPoint.X, rect.Bottom, rect.X, rect.Bottom - offset);
                    path.AddLine(
                        rect.X, rect.Y, rect.Right, rect.Y);
                    path.AddLine(
                        rect.Right, rect.Bottom - offset, centerPoint.X, rect.Bottom);
                    break;
                case ThumbArrowDirection.LeftRight:
                    break;
                case ThumbArrowDirection.UpDown:
                    break;
                case ThumbArrowDirection.None:
                    path.AddRectangle(rect);
                    break;
            }

            path.CloseFigure();
            return path;
        }
    }
    public enum ThumbArrowDirection
    {
        None = 0,
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4,
        LeftRight = 5,
        UpDown = 6
    }
}