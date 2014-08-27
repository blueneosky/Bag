using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HSphere.Engine
{
    public partial class PanelEngine : UserControl
    {
        private ViewPortCamera _viewPort;

        public PanelEngine()
        {
            InitializeComponent();
        }

        public Point Center
        {
            get { return new Point(Width / 2, Height / 2); }
        }

        public ViewPortCamera ViewPort
        {
            get { return _viewPort; }
            set
            {
                if (_viewPort != null)
                {
                    _viewPort.Device.Rendered -= _device_Rendered;
                }
                _viewPort = value;
                if (_viewPort != null)
                {
                    _viewPort.Device.Rendered += _device_Rendered;
                    Point center = this.Center;
                    using (Graphics g = CreateGraphics())
                    {
#warning TODO ZETA point - uncomment
                        //_viewPort.DpiX = g.DpiX;
                    }
#warning TODO ZETA point - uncomment
                    //_viewPort.SetViewPort(Width, Height, center.X, center.Y);
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ViewPortCamera viewPort = this.ViewPort;
            if (viewPort == null)
                return;

            Point center = this.Center;
#warning TODO ZETA point - uncomment
            //viewPort.SetViewPort(Width, Height, center.X, center.Y);

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ViewPortCamera viewPort = this.ViewPort;
            if (viewPort == null)
                return;

            if (!this.DisplayRectangle.IntersectsWith(e.ClipRectangle))
                return;

            try
            {
#warning TODO ZETA point - uncomment
                //viewPort.Present(e.Graphics);
            }
            catch (Exception)
            {
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ViewPortCamera viewPort = this.ViewPort;
            if (viewPort == null)
                return;

            Point center = this.Center;
#warning TODO ZETA point - uncomment
            //viewPort.SetViewPort(Width, Height, center.X, center.Y);
            
        }

        private void _device_Rendered(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}