using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Loop.Common.Extension
{
    public static class ExtensionGraphics
    {
        public static void FillCenteredRectangle(this Graphics g, Brush brush, Point point, Size size)
        {
            FillCenteredRectangle(g, brush, point.X, point.Y, size.Width, size.Height);
        }

        public static void FillCenteredRectangle(this Graphics g, Brush brush, int x, int y, int width, int height)
        {
            g.FillRectangle(brush, x - width / 2, y - height / 2, width, height);
        }

        public static void DrawCenteredString(this Graphics g, string s, Font font, Brush brush, int x, int y, int width, int height, StringFormat format)
        {
            Rectangle bounds = new Rectangle(x - width / 2, y - height / 2, width, height);
            g.DrawString(s, font, brush, bounds, format);
        }
    }
}