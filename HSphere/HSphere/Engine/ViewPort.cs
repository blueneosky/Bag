using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using HSphere.Geometry;
using System.Diagnostics;

namespace HSphere.Engine
{
    public unsafe class ViewPort : IDisposable
    {
        #region Members

        private const PixelFormat ConstViewPortPixelFormat = PixelFormat.Format32bppPArgb;
        private static readonly int ConstViewPortBitsPerPixel = Image.GetPixelFormatSize(ConstViewPortPixelFormat);
        private static readonly int ConstViewPortBytesPerPixel = ConstViewPortBitsPerPixel / 8;
        private static readonly int ConstZBufferBytesPerPixel = sizeof(double);

        private Bitmap _bitmap;
        private object _bitmapLock = new object();

        private IntPtr _scanIntPtr;  // byte[]
        private byte* _scan0;
        private int _height;
        private int _width;
        private int _stride;
        private int _size;

        private IntPtr _zBufferIntPtr; // double[]
        private double* _zBuffer0;
        private int _zBufferSize;

        private int _xOrigin;
        private int _yOrigin;

        #endregion Members

        #region ctor

        public ViewPort()
        {
            _bitmap = null;
            _scanIntPtr = IntPtr.Zero;
            _zBufferIntPtr = IntPtr.Zero;

            SetViewPort(16, 16, 8, 8);

            SetDefaultRenderFactor();
        }

        #endregion ctor

        #region Event

        public event EventHandler Rendered;

        public event EventHandler ViewPortChanged;

        protected virtual void OnRendered(object sender, EventArgs e)
        {
            EventHandler rendered = Rendered;
            if (rendered == null)
                return;
            rendered(sender, e);
        }

        protected virtual void OnViewPortChanged(object sender, EventArgs e)
        {
            EventHandler viewPortChanged = ViewPortChanged;
            if (viewPortChanged == null)
                return;
            viewPortChanged(sender, e);
        }

        #endregion Event

        #region Properties

        public double DpiX { get; set; }

        public int Height { get { return _height; } }

        public bool IsDpiScaling { get; set; }

        public double RenderFactor { get; set; }

        public int Width { get { return _width; } }

        #endregion Properties

        #region Function

        public void Clear(Color color)
        {
            int argb = color.ToArgb();
            byte* scanLine = _scan0;
            double* zBuffer = _zBuffer0;
            for (int y = 0; y < _height; y++)
            {
                int* scan = (int*)scanLine;
                for (int x = 0; x < _width; x++)
                {
                    *scan = argb;
                    *zBuffer = 0.0;
                    scan++;
                    zBuffer++;
                }
                scanLine += _stride;
            }
        }

        public void DrawLine(ELine line)
        {
            Color? color = line.Color;
            if (color.HasValue)
                DrawLine(line.V1, line.V2, color.Value);
        }

        public void DrawLine(Vertex v1, Vertex v2, Color c)
        {

#warning TODO
        }

        public void DrawPoint(Vertex v, Color c)
        {

#warning TODO
        }

        public void DrawPoint(EVertex v, Color c)
        {
            DrawPoint(v.ViewPort, c);
        }

        public void DrawTriangle(ETriangle triangle)
        {
            Color? fillColor = triangle.FillColor;
            if (fillColor.HasValue)
                FillTriangle(triangle.V1, triangle.V2, triangle.V3, fillColor.Value);
            Color? lineColor = triangle.LineColor;
            if (lineColor.HasValue)
                DrawTriangle(triangle.V1, triangle.V2, triangle.V3, lineColor.Value);
        }

        public void DrawTriangle(Vertex v1, Vertex v2, Vertex v3, Color c)
        {

#warning TODO
        }

        public void FillTriangle(Vertex v1, Vertex v2, Vertex v3, Color c)
        {

#warning TODO
        }

        public void Present()
        {
            // write scan (back buffer) into bitmap
            BitmapData bitmapData = new BitmapData()
            {
                Scan0 = _scanIntPtr,
                Stride = _stride,
                Height = _height,
                Width = _width,
                PixelFormat = ConstViewPortPixelFormat,
            };
            BitmapData bitmapDataResult = _bitmap.LockBits(
                new Rectangle(0, 0, _width, _height)
                , ImageLockMode.UserInputBuffer | ImageLockMode.WriteOnly
                , ConstViewPortPixelFormat
                , bitmapData);
            Debug.Assert(Object.ReferenceEquals(bitmapData, bitmapDataResult));
            _bitmap.UnlockBits(bitmapDataResult);
        }

        public void Draw(Graphics g)
        {
            // show bitmap
            g.DrawImageUnscaled(_bitmap, 0, 0, _width, _height);
        }

        /// <summary>
        /// Rendering is relative to screen witdh.
        /// An object with a witdh of 1 unit, distant of 1 unit from the camera, are seieng as a (Camera.Zoom * Screen.Width) width on screen
        /// </summary>
        public void SetDefaultRenderFactor()
        {
#warning TODO
            RenderFactor = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
        }

        public void SetViewPort(int width, int height, int xOrigin, int yOrigin)
        {
            if ((_bitmap == null) || (_bitmap.Width < width) || (_bitmap.Height < height))
            {
                // new bigger bitmap
                _bitmap.Dispose();
                _bitmap = new Bitmap(width, height, ConstViewPortPixelFormat);

                BitmapData bitmapData = _bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, _bitmap.PixelFormat);
                _stride = bitmapData.Stride;
                _bitmap.UnlockBits(bitmapData);

                int size = _stride * height * ConstViewPortBytesPerPixel;
                if (_scanIntPtr == IntPtr.Zero)
                {
                    _scanIntPtr = Marshal.AllocHGlobal(size);
                    _size = size;
                }
                else if (size > _size)
                {
                    _scanIntPtr = Marshal.ReAllocHGlobal(_scanIntPtr, (IntPtr)size);
                    _size = size;
                }
            }

            _width = width;
            _height = height;

            int zBufferSize = width * height * ConstZBufferBytesPerPixel;
            if (_zBufferIntPtr == IntPtr.Zero)
            {
                _zBufferIntPtr = Marshal.AllocHGlobal(zBufferSize);
                _zBufferSize = zBufferSize;
            }
            if (zBufferSize > _zBufferSize)
            {
                // new bigger zBuffer
                _zBufferIntPtr = Marshal.ReAllocHGlobal(_zBufferIntPtr, (IntPtr)zBufferSize);
                _zBufferSize = zBufferSize;
            }

            _xOrigin = xOrigin;
            _yOrigin = yOrigin;

            _scan0 = (byte*)_scanIntPtr.ToPointer();
            _zBuffer0 = (double*)_zBufferIntPtr.ToPointer();

            OnViewPortChanged(this, EventArgs.Empty);
        }

        #endregion Function

        #region Private

        #region Line clipping

        [Flags]
        enum EnumClippingCode : int
        {
            Inside = 0,
            Left = 1,
            Right = 2,
            Top = 4,
            Bottom = 8,

            TopLeft = Top | Left,
            TopRight = Top | Right,
            BottomLeft = Bottom | Left,
            BottomRight = Bottom | Right,
        }

        private static bool GetLineClippingBox(ref double x0, ref double y0, ref double z0, ref  double x1, ref double y1, ref double z1, out bool p0Clipped, out bool p1Clipped)
        {
            p0Clipped = false;
            p1Clipped = false;

            EnumClippingCode p0Code = EnumClippingCode.Inside;          // initialised as being inside of clip window
            if (x0 < -1.0)           // to the left of clip window
                p0Code |= EnumClippingCode.Left;
            else if (x0 > 1.0)      // to the right of clip window
                p0Code |= EnumClippingCode.Right;
            if (y0 < -1.0)           // below the clip window
                p0Code |= EnumClippingCode.Bottom;
            else if (y0 > 1.0)      // above the clip window
                p0Code |= EnumClippingCode.Top;

            EnumClippingCode p1Code = EnumClippingCode.Inside;          // initialised as being inside of clip window
            if (x1 < -1.0)           // to the left of clip window
                p1Code |= EnumClippingCode.Left;
            else if (x1 > 1.0)      // to the right of clip window
                p1Code |= EnumClippingCode.Right;
            if (y1 < -1.0)           // below the clip window
                p1Code |= EnumClippingCode.Bottom;
            else if (y1 > 1.0)      // above the clip window
                p1Code |= EnumClippingCode.Top;


            if ((p0Code | p1Code) == EnumClippingCode.Inside)
                return true;   // trivial - inside

            if ((p0Code & p1Code) != EnumClippingCode.Inside)
                return false;   // trivial - outside

#warning Activate this code if a large amount of line is outside of the cube
            //if ((y0 > x0 + 2) && (y1 > x1 + 2))
            //    return false;   // outside
            //if ((y0 < x0 - 2) && (y1 < x1 - 2))
            //    return false;   // outside
            //if ((y0 > -x0 + 2) && (y1 > -x1 + 2))
            //    return false;   // outside
            //if ((y0 < -x0 - 2) && (y1 < -(x1 + 2)))
            //    return false;   // outside

            double x;
            double y;

            // Point (x0, y0)
            switch (p0Code)
            {
                case EnumClippingCode.Inside:
                    // good -> nothing
                    break;

                case EnumClippingCode.TopLeft:
                    y = (1.0 + x0) * (y0 - y1) / (x1 - x0) + y0;
                    if (y < -1.0)
                        return false;   // outside
                    if (y > 1.0)
                    {
                        x0 = (1.0 - y0) * (x1 - x0) / (y1 - y0) + x0;
                        if (x0 > 1.0)
                            return false;   // outside
                        z0 = (1.0 - y0) * (z1 - z0) / (y1 - y0) + z0;
                        y0 = 1.0;
                    }
                    else
                    {
                        z0 = (1.0 + x0) * (z0 - z1) / (x1 - x0) + z0;
                        x0 = -1.0;
                        y0 = y;
                    }
                    p0Clipped = true;
                    break;
                case EnumClippingCode.TopRight:
                    y = (1.0 - x0) * (y1 - y0) / (x1 - x0) + y0;
                    if (y < -1.0)
                        return false;   // outside
                    if (y > 1.0)
                    {
                        x0 = (1.0 - y0) * (x1 - x0) / (y1 - y0) + x0;
                        if (x0 < -1.0)
                            return false;   // outside
                        z0 = (1.0 - y0) * (z1 - z0) / (y1 - y0) + z0;
                        y0 = 1.0;
                    }
                    else
                    {
                        z0 = (1.0 - x0) * (z1 - z0) / (x1 - x0) + z0;
                        x0 = 1.0;
                        y0 = y;
                    }
                    p0Clipped = true;
                    break;
                case EnumClippingCode.BottomLeft:
                    y = (1.0 + x0) * (y0 - y1) / (x1 - x0) + y0;
                    if (y > 1.0)
                        return false;   // outside
                    if (y < -1.0)
                    {
                        x = (1.0 + y0) * (x0 - x1) / (y1 - y0) + x0;
                        if (x > 1.0)
                            return false;   // outside
                        x0 = x; y0 = -1.0;
                    }
                    else
                    {
                        x0 = -1.0; y0 = y;
                    }
                    p0Clipped = true;
                    break;
                case EnumClippingCode.BottomRight:
                    y = (1.0 - x0) * (y1 - y0) / (x1 - x0) + y0;
                    if (y > 1.0)
                        return false;   // outside
                    if (y < -1.0)
                    {
                        x = (1.0 + y0) * (x0 - x1) / (y1 - y0) + x0;
                        if (x < -1.0)
                            return false;   // outside
                        x0 = x; y0 = -1.0;
                    }
                    else
                    {
                        x0 = 1.0; y0 = y;
                    }
                    p0Clipped = true;
                    break;

                case EnumClippingCode.Left:
                    y0 = (1.0 + x0) * (y0 - y1) / (x1 - x0) + y0;
                    if ((y0 > 1.0) || (y0 < -1.0))
                        return false;                       // outside
                    x0 = -1.0;
                    p0Clipped = true;
                    break;
                case EnumClippingCode.Right:
                    y0 = (1.0 - x0) * (y1 - y0) / (x1 - x0) + y0;
                    if ((y0 > 1.0) || (y0 < -1.0))
                        return false;                       // outside
                    x0 = 1.0;
                    p0Clipped = true;
                    break;
                case EnumClippingCode.Top:
                    x0 = (1.0 - y0) * (x1 - x0) / (y1 - y0) + x0;
                    if ((x0 < -1.0) || (x0 > 1.0))
                        return false;                       // outside
                    y0 = 1.0;
                    p0Clipped = true;
                    break;
                case EnumClippingCode.Bottom:
                    x0 = (1.0 + y0) * (x0 - x1) / (y1 - y0) + x0;
                    if ((x0 < -1.0) || (x0 > 1.0))
                        return false;                       // outside
                    y0 = -1.0;
                    p0Clipped = true;
                    break;
                default:
                    Debug.Fail("Not expected");
                    break;
            }

            // Point (x1, y1)
            switch (p1Code)
            {
                case EnumClippingCode.Inside:
                    // good -> nothing
                    break;

                case EnumClippingCode.TopLeft:
                    y = (1.0 + x1) * (y1 - y0) / (x0 - x1) + y1;
                    if (y < -1.0)
                        return false;   // outside
                    if (y > 1.0)
                    {
                        x = (1.0 - y1) * (x0 - x1) / (y0 - y1) + x1;
                        if (x > 1.0)
                            return false;   // outside
                        x1 = x; y1 = 1.0;
                    }
                    else
                    {
                        x1 = -1.0; y1 = y;
                    }
                    p1Clipped = true;
                    break;
                case EnumClippingCode.TopRight:
                    y = (1.0 - x1) * (y0 - y1) / (x0 - x1) + y1;
                    if (y < -1.0)
                        return false;   // outside
                    if (y > 1.0)
                    {
                        x = (1.0 - y1) * (x0 - x1) / (y0 - y1) + x1;
                        if (x < -1.0)
                            return false;   // outside
                        x1 = x; y1 = 1.0;
                    }
                    else
                    {
                        x1 = 1.0; y1 = y;
                    }
                    p1Clipped = true;
                    break;
                case EnumClippingCode.BottomLeft:
                    y = (1.0 + x1) * (y1 - y0) / (x0 - x1) + y1;
                    if (y > 1.0)
                        return false;   // outside
                    if (y < -1.0)
                    {
                        x = (1.0 + y1) * (x1 - x0) / (y0 - y1) + x1;
                        if (x > 1.0)
                            return false;   // outside
                        x1 = x; y1 = -1.0;
                    }
                    else
                    {
                        x1 = -1.0; y1 = y;
                    }
                    p1Clipped = true;
                    break;
                case EnumClippingCode.BottomRight:
                    y = (1.0 - x1) * (y0 - y1) / (x0 - x1) + y1;
                    if (y > 1.0)
                        return false;   // outside
                    if (y < -1.0)
                    {
                        x = (1.0 + y1) * (x1 - x0) / (y0 - y1) + x1;
                        if (x < -1.0)
                            return false;   // outside
                        x1 = x; y1 = -1.0;
                    }
                    else
                    {
                        x1 = 1.0; y1 = y;
                    }
                    p1Clipped = true;
                    break;

                case EnumClippingCode.Left:
                    y1 = (1.0 + x1) * (y1 - y0) / (x0 - x1) + y1;
                    if ((y1 > 1.0) || (y0 < -1.0))
                        return false;                       // outside
                    x1 = -1.0;
                    p1Clipped = true;
                    break;
                case EnumClippingCode.Right:
                    y1 = (1.0 - x1) * (y0 - y1) / (x0 - x1) + y1;
                    if ((y1 > 1.0) || (y0 < -1.0))
                        return false;                       // outside
                    x1 = 1.0;
                    p1Clipped = true;
                    break;
                case EnumClippingCode.Top:
                    x1 = (1.0 - y1) * (x0 - x1) / (y0 - y1) + x1;
                    if ((x1 < -1.0) || (x1 > 1.0))
                        return false;                       // outside
                    y1 = 1.0;
                    p1Clipped = true;
                    break;
                case EnumClippingCode.Bottom:
                    x1 = (1.0 + y1) * (x1 - x0) / (y0 - y1) + x1;
                    if ((x1 < -1.0) || (x1 > 1.0))
                        return false;                       // outside
                    y1 = -1.0;
                    p1Clipped = true;
                    break;
                default:
                    Debug.Fail("Not expected");
                    break;
            }

            return true;
        }

        #region Test

        public static bool TestGetLineClipping(ref double x0, ref double y0, ref double z0, ref  double x1, ref double y1, ref double z1, out bool p0Clipped, out bool p1Clipped)
        {
            //return GetLineClipping(ref  x0, ref  y0, ref   x1, ref  y1, -1.0, 1.0, -1.0, 1.0, out  p0Clipped, out  p1Clipped);
            return GetLineClippingBox(ref  x0, ref  y0, ref z0, ref   x1, ref  y1, ref z1, out  p0Clipped, out  p1Clipped);
        }

        public static void TestGetLineClippingSwapped(double x0, double y0, double z0, double x1, double y1, double z1, bool expectedResult, double expectedX0, double expectedY0, double expectedZ0, double expectedX1, double expectedY1, double expectedZ1)
        {
            TestGetLineClipping(x0, y0, z0, x1, y1, z1, expectedResult, expectedX0, expectedY0, expectedZ0, expectedX1, expectedY1, expectedZ1);
            TestGetLineClipping(x1, y1, z1, x0, y0, z0, expectedResult, expectedX1, expectedY1, expectedZ1, expectedX0, expectedY0, expectedZ0);
        }

        private static bool _showTestGetLineClippingResult = true;

        public static void TestGetLineClipping(double x0, double y0, double z0, double x1, double y1, double z1, bool expectedResult, double expectedX0, double expectedY0, double expectedZ0, double expectedX1, double expectedY1, double expectedZ1)
        {
            bool p0Clipped; bool p1Clipped;
            bool result = TestGetLineClipping(ref x0, ref y0, ref z0, ref x1, ref y1, ref z1, out p0Clipped, out p1Clipped);

            if (!_showTestGetLineClippingResult)
                return;

            Console.Write("({0}, {1}) - ({2}, {3}) : ", x0, y0, x1, y1);

            bool success = (expectedResult == result)
                && (!result ||
                    (x0 == expectedX0)
                    && (y0 == expectedY0)
                    && (z0 == expectedZ0)
                    && (x1 == expectedX1)
                    && (y1 == expectedY1)
                    && (z1 == expectedZ1)
                );
            if (success)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Failed");

                if (result)
                {
                    if (!(p0Clipped || p1Clipped))
                    {
                        Console.WriteLine("=> Inside");
                    }
                    else
                    {
                        string left = "()";
                        string right = left;
                        if (p0Clipped)
                        {
                            left = String.Format("({0}, {1})", x0, y0);
                        }
                        if (p1Clipped)
                        {
                            right = String.Format("({0}, {1})", x1, y1);
                        }
                        Console.WriteLine("=> " + left + " - " + right);
                    }
                    Debug.Assert(-1 <= x0 && x0 <= 1);
                    Debug.Assert(-1 <= y0 && y0 <= 1);
                    Debug.Assert(-1 <= x1 && x1 <= 1);
                    Debug.Assert(-1 <= y1 && y1 <= 1);
                }
                else
                {
                    Console.WriteLine("=> Outside");
                }
            }

        }

        public static void TestGetLineClipping(bool showResult)
        {
            _showTestGetLineClippingResult = showResult;

            // (15*4 + 1 )*2 = 122 Tests

            //goto Skip11;

            TestGetLineClippingSwapped(0, 0, 0, 0, 0, 0, true, 0, 0, 0, 0, 0, 0);

        Skip0:
            TestGetLineClippingSwapped(0, 0, 0, +1, 0, 0, true, 0, 0, 0, +1, 0, 0);
            TestGetLineClippingSwapped(0, 0, 0, -1, 0, 0, true, 0, 0, 0, -1, 0, 0);
            TestGetLineClippingSwapped(0, 0, 0, 0, +1, 0, true, 0, 0, 0, 0, +1, 0);
            TestGetLineClippingSwapped(0, 0, 0, 0, -1, 0, true, 0, 0, 0, 0, -1, 0);

        Skip1:
            TestGetLineClippingSwapped(0, 0, 0, +1, +1, 0, true, 0, 0, 0, +1, +1, 0);
            TestGetLineClippingSwapped(0, 0, 0, -1, -1, 0, true, 0, 0, 0, -1, -1, 0);
            TestGetLineClippingSwapped(0, 0, 0, -1, +1, 0, true, 0, 0, 0, -1, +1, 0);
            TestGetLineClippingSwapped(0, 0, 0, +1, -1, 0, true, 0, 0, 0, +1, -1, 0);

        Skip2:
            TestGetLineClippingSwapped(0, 0, 0, +2, 0, 0, true, 0, 0, 0, +1, 0, 0);
            TestGetLineClippingSwapped(0, 0, 0, -2, 0, 0, true, 0, 0, 0, -1, 0, 0);
            TestGetLineClippingSwapped(0, 0, 0, 0, +2, 0, true, 0, 0, 0, 0, +1, 0);
            TestGetLineClippingSwapped(0, 0, 0, 0, -2, 0, true, 0, 0, 0, 0, -1, 0);

        Skip3:
            TestGetLineClippingSwapped(0, 0, 0, +2, +2, 0, true, 0, 0, 0, +1, +1, 0);
            TestGetLineClippingSwapped(0, 0, 0, -2, -2, 0, true, 0, 0, 0, -1, -1, 0);
            TestGetLineClippingSwapped(0, 0, 0, -2, +2, 0, true, 0, 0, 0, -1, +1, 0);
            TestGetLineClippingSwapped(0, 0, 0, +2, -2, 0, true, 0, 0, 0, +1, -1, 0);

        Skip4:
            TestGetLineClippingSwapped(0, 0, 0, +1, +2, 0, true, 0, 0, 0, +0.5, +1.0, 0);
            TestGetLineClippingSwapped(0, 0, 0, +1, -2, 0, true, 0, 0, 0, +0.5, -1.0, 0);
            TestGetLineClippingSwapped(0, 0, 0, -1, +2, 0, true, 0, 0, 0, -0.5, +1.0, 0);
            TestGetLineClippingSwapped(0, 0, 0, -1, -2, 0, true, 0, 0, 0, -0.5, -1.0, 0);
            TestGetLineClippingSwapped(0, 0, 0, +2, +1, 0, true, 0, 0, 0, +1.0, +0.5, 0);
            TestGetLineClippingSwapped(0, 0, 0, +2, -1, 0, true, 0, 0, 0, +1.0, -0.5, 0);
            TestGetLineClippingSwapped(0, 0, 0, -2, +1, 0, true, 0, 0, 0, -1.0, +0.5, 0);
            TestGetLineClippingSwapped(0, 0, 0, -2, -1, 0, true, 0, 0, 0, -1.0, -0.5, 0);

        Skip5:
            TestGetLineClippingSwapped(0, 0, 0, +2, +4, 0, true, 0, 0, 0, +0.5, +1.0, 0);
            TestGetLineClippingSwapped(0, 0, 0, +2, -4, 0, true, 0, 0, 0, +0.5, -1.0, 0);
            TestGetLineClippingSwapped(0, 0, 0, -2, +4, 0, true, 0, 0, 0, -0.5, +1.0, 0);
            TestGetLineClippingSwapped(0, 0, 0, -2, -4, 0, true, 0, 0, 0, -0.5, -1.0, 0);

            TestGetLineClippingSwapped(0, 0, 0, +4, +2, 0, true, 0, 0, 0, +1.0, +0.5, 0);
            TestGetLineClippingSwapped(0, 0, 0, +4, -2, 0, true, 0, 0, 0, +1.0, -0.5, 0);
            TestGetLineClippingSwapped(0, 0, 0, -4, +2, 0, true, 0, 0, 0, -1.0, +0.5, 0);
            TestGetLineClippingSwapped(0, 0, 0, -4, -2, 0, true, 0, 0, 0, -1.0, -0.5, 0);

        Skip6:
            TestGetLineClippingSwapped(-1, +1, 0, +1, +1, 0, true, -1, +1, 0, +1, +1, 0);
            TestGetLineClippingSwapped(-1, -1, 0, +1, -1, 0, true, -1, -1, 0, +1, -1, 0);
            TestGetLineClippingSwapped(+1, -1, 0, +1, +1, 0, true, +1, -1, 0, +1, +1, 0);
            TestGetLineClippingSwapped(-1, -1, 0, -1, +1, 0, true, -1, -1, 0, -1, +1, 0);

        Skip7:
            TestGetLineClippingSwapped(-2, +1, 0, +2, +1, 0, true, -1, +1, 0, +1, +1, 0);
            TestGetLineClippingSwapped(-2, -1, 0, +2, -1, 0, true, -1, -1, 0, +1, -1, 0);
            TestGetLineClippingSwapped(+1, -2, 0, +1, +2, 0, true, +1, -1, 0, +1, +1, 0);
            TestGetLineClippingSwapped(-1, -2, 0, -1, +2, 0, true, -1, -1, 0, -1, +1, 0);

        Skip8:
            TestGetLineClippingSwapped(-2, +2, 0, +2, +2, 0, false, 0, 0, 0, 0, 0, 0);
            TestGetLineClippingSwapped(-2, -2, 0, +2, -2, 0, false, 0, 0, 0, 0, 0, 0);
            TestGetLineClippingSwapped(+2, -2, 0, +2, +2, 0, false, 0, 0, 0, 0, 0, 0);
            TestGetLineClippingSwapped(-2, -2, 0, -2, +2, 0, false, 0, 0, 0, 0, 0, 0);

        Skip9:
            TestGetLineClippingSwapped(+2, +1, 0, -1, -2, 0, true, +1, +0, 0, +0, -1, 0);
            TestGetLineClippingSwapped(+1, +2, 0, -2, -1, 0, true, +0, +1, 0, -1, -0, 0);
            TestGetLineClippingSwapped(+2, -1, 0, -1, +2, 0, true, +1, -0, 0, +0, +1, 0);
            TestGetLineClippingSwapped(+1, -2, 0, -2, +1, 0, true, +0, -1, 0, -1, +0, 0);

        Skip10:
            TestGetLineClippingSwapped(+4, +0, 0, +0, +4, 0, false, 0, 0, 0, 0, 0, 0);
            TestGetLineClippingSwapped(+4, +0, 0, +0, -4, 0, false, 0, 0, 0, 0, 0, 0);
            TestGetLineClippingSwapped(-4, +0, 0, +0, +4, 0, false, 0, 0, 0, 0, 0, 0);
            TestGetLineClippingSwapped(-4, +0, 0, +0, -4, 0, false, 0, 0, 0, 0, 0, 0);

        Skip11:
            TestGetLineClippingSwapped(+4, +4, 0, +0, -4, 0, false, 0, 0, 0, 0, 0, 0);
            TestGetLineClippingSwapped(-4, -4, 0, -0, +4, 0, false, 0, 0, 0, 0, 0, 0);
            TestGetLineClippingSwapped(-4, +4, 0, +4, -0, 0, false, 0, 0, 0, 0, 0, 0);
            TestGetLineClippingSwapped(+4, -4, 0, -4, +0, 0, false, 0, 0, 0, 0, 0, 0);

            TestGetLineClippingSwapped(+4, +4, 0, -4, +0, 0, false, 0, 0, 0, 0, 0, 0);
            TestGetLineClippingSwapped(-4, -4, 0, +4, -0, 0, false, 0, 0, 0, 0, 0, 0);
            TestGetLineClippingSwapped(-4, +4, 0, -0, -4, 0, false, 0, 0, 0, 0, 0, 0);
            TestGetLineClippingSwapped(+4, -4, 0, +0, +4, 0, false, 0, 0, 0, 0, 0, 0);

        }

        #endregion
        #endregion

        #endregion Private
        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~ViewPort()
        {
            Dispose(false);
        }
        private bool _disposed;
        protected virtual void Dispose(bool isDispose)
        {
            if (_disposed)
                return;
            _disposed = true;

            if (isDispose)
            {
                // managed resources
                if (_bitmap != null)
                    _bitmap.Dispose();
                _bitmap = null;
            }

            // unmanaged resources
            if (_scanIntPtr != IntPtr.Zero)
                Marshal.FreeHGlobal(_scanIntPtr);
            _scanIntPtr = IntPtr.Zero;
            if (_zBufferIntPtr != IntPtr.Zero)
                Marshal.FreeHGlobal(_zBufferIntPtr);
            _zBufferIntPtr = IntPtr.Zero;

            _scan0 = (byte*)_scanIntPtr.ToPointer();
            _zBuffer0 = (double*)_zBufferIntPtr.ToPointer();
        }
        #endregion IDisposable
    }
}

/*
        private static bool GetLineClipping(ref double x0, ref double y0, ref  double x1, ref double y1, double left, double right, double bottom, double top, out bool p0Clipped, out bool p1Clipped)
        {
            p0Clipped = false;
            p1Clipped = false;

            EnumClippingCode p0Code = GetClippingCode(x0, y0, left, right, bottom, top);
            EnumClippingCode p1Code = GetClippingCode(x1, y1, left, right, bottom, top);
            if ((p0Code | p1Code) == EnumClippingCode.Inside)
                return true;   // trivial - inside

            if ((p0Code & p1Code) != EnumClippingCode.Inside)
                return false;   // trivial - outside

            bool result;

#warning TODO - optim : sacrifier précision en dégageant ces 2 variables
            double x = x0;
            double y = y0;
            result = GetPointClipping(ref x0, ref y0, out p0Clipped, p0Code, left, right, bottom, top, x1, y1);
            if (false == result)
                return false;   // outside

            result = GetPointClipping(ref x1, ref y1, out p1Clipped, p1Code, left, right, bottom, top, x, y);
            if (false == result)
                return false;   // outside

            return true;
        }

        private static bool GetPointClipping(ref double x0, ref double y0, out bool p0Clipped, EnumClippingCode p0Code, double left, double right, double bottom, double top, double x1, double y1)
        {
            p0Clipped = false;

            double x;
            double y;
            double ySlope;
            switch (p0Code)
            {
                case EnumClippingCode.Inside:
                    // good -> nothing
                    break;

                case EnumClippingCode.TopLeft:
                    x = left;
                    y = (x - x0) * (y1 - y0) / (x1 - x0) + y0;
                    if (y < bottom)
                        return false;   // outside
                    if (y > top)
                    {
                        y = top;
                        x = (y - y0) * (x1 - x0) / (y1 - y0) + x0;
                        if (x > right)
                            return false;   // outside
                    }
                    x0 = x; y0 = y; p0Clipped = true;
                    break;
                case EnumClippingCode.TopRight:
                    x = right;
                    y = (x - x0) * (y1 - y0) / (x1 - x0) + y0;
                    if (y < bottom)
                        return false;   // outside
                    if (y > top)
                    {
                        y = top;
                        x = (y - y0) * (x1 - x0) / (y1 - y0) + x0;
                        if (x < left)
                            return false;   // outside
                    }
                    x0 = x; y0 = y; p0Clipped = true;
                    break;
                case EnumClippingCode.BottomLeft:
                    x = left;
                    y = (x - x0) * (y1 - y0) / (x1 - x0) + y0;
                    if (y > top)
                        return false;   // outside
                    if (y < bottom)
                    {
                        y = bottom;
                        x = (y - y0) * (x1 - x0) / (y1 - y0) + x0;
                        if (x > right)
                            return false;   // outside
                    }
                    x0 = x; y0 = y; p0Clipped = true;
                    break;
                case EnumClippingCode.BottomRight:
                    x = right;
                    y = (x - x0) * (y1 - y0) / (x1 - x0) + y0;
                    if (y > top)
                        return false;   // outside
                    if (y < bottom)
                    {
                        y = bottom;
                        x = (y - y0) * (x1 - x0) / (y1 - y0) + x0;
                        if (x < left)
                            return false;   // outside
                    }
                    x0 = x; y0 = y; p0Clipped = true;
                    break;

                case EnumClippingCode.Left:
                    x = left;
                    y = (x - x0) * (y1 - y0) / (x1 - x0) + y0;
                    if ((y > top) || (y < bottom))
                        return false;                       // outside
                    x0 = x; y0 = y; p0Clipped = true;
                    break;
                case EnumClippingCode.Right:
                    x = right;
                    y = (x - x0) * (y1 - y0) / (x1 - x0) + y0;
                    if ((y > top) || (y < bottom))
                        return false;                       // outside
                    x0 = x; y0 = y; p0Clipped = true;
                    break;
                case EnumClippingCode.Top:
                    y = top;
                    x = (y - y0) * (x1 - x0) / (y1 - y0) + x0;
                    if ((x < left) || (x > right))
                        return false;                       // outside
                    x0 = x; y0 = y; p0Clipped = true;
                    break;
                case EnumClippingCode.Bottom:
                    y = bottom;
                    x = (y - y0) * (x1 - x0) / (y1 - y0) + x0;
                    if ((x < left) || (x > right))
                        return false;                       // outside
                    x0 = x; y0 = y; p0Clipped = true;
                    break;
                default:
                    Debug.Fail("Not expected");
                    break;
            }
            return true;
        }

*/