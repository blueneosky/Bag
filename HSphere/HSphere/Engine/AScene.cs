using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HSphere.Engine
{
    public abstract class AScene
    {
        #region Members

        private static readonly int ConstBaseViewPortStride = 16;

        private bool _hasChanged;
        private int _renderingCount;
        private ViewPortCamera _viewPort;

        #endregion Members

        #region ctor

        public AScene()
        {
            ViewPort = new ViewPortCamera();
        }

        #endregion ctor

        #region Properties

        public Color BackgroundColor { get; set; }

        public abstract IEnumerable<IMesh> Meshes { get; }

        public ViewPortCamera ViewPort
        {
            get { return _viewPort; }
            set
            {
                if (_viewPort != null)
                {
                    _viewPort.DeviceSizeChanged -= _viewPort_DeviceSizeChanged;
                }
                _viewPort = value;
                if (_viewPort != null)
                {
                    _viewPort.DeviceSizeChanged += _viewPort_DeviceSizeChanged;
                }
            }
        }

        #endregion Properties

        #region Functions

        public void Render()
        {
            ViewPort.Render(this.Meshes, BackgroundColor);
        }

        private void _viewPort_DeviceSizeChanged(object sender, EventArgs e)
        {
            Render();
        }

        #endregion Functions
    }
}