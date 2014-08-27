using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HSphere.Cube;
using HSphere.Engine;
using HSphere.Geometry;
using HSphere.Sphere;

namespace HSphere
{
    public partial class Form1 : Form
    {
        #region Members

        private const double ConstAngularFactor = Math.PI / 200f;

        private System.Drawing.Point? _lastMousePosition;
        private AScene _scene;
        private double _tetaX = 0;
        private double _tetaY = 0;

        #endregion Members

        #region ctor

        public Form1(AScene scene)
        {
            InitializeComponent();

            Scene = scene;

            _panelEngine.MouseMove += new MouseEventHandler(_panelEngine_MouseMove);
            _panelEngine.MouseDown += new MouseEventHandler(_panelEngine_MouseDown);
            _panelEngine.MouseUp += new MouseEventHandler(_panelEngine_MouseUp);
            _panelEngine.MouseWheel += new MouseEventHandler(_panelEngine_MouseWheel);
        }

        private Form1()
        {
            InitializeComponent();
        }

        #endregion ctor

        #region Properties

        public AScene Scene
        {
            get { return _scene; }
            set
            {
                _scene = value;
#warning TODO ZETA point - uncomment
                //_panelEngine.ViewPort = value != null ? value.ViewPort.Device : null;
            }
        }

        #endregion Properties

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Scene == null)
                return;

            UpdateCamera();
            Scene.Render();
        }

        #endregion Overrides

        #region User inputs

        private void _isDpiScalingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCamera();
            Scene.Render();
        }

        private void _panelEngine_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                _lastMousePosition = e.Location;
        }

        private void _panelEngine_MouseMove(object sender, MouseEventArgs e)
        {
            if (_lastMousePosition.HasValue)
            {
                double deltaX = e.X - _lastMousePosition.Value.X;
                double deltaY = e.Y - _lastMousePosition.Value.Y;

                // deltaTeta# is angular arroud axis #
                double deltaTetaY = -deltaX * ConstAngularFactor;
                double deltaTetaX = -deltaY * ConstAngularFactor;

                _tetaX += deltaTetaX;
                _tetaY += deltaTetaY;
                UpdateCamera();
                Scene.Render();

                _lastMousePosition = e.Location;
            }
        }

        private void _panelEngine_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                _lastMousePosition = null;
        }

        private void _panelEngine_MouseWheel(object sender, MouseEventArgs e)
        {
            int zoom = _viewDistanceTrackBar.Value + -2 * e.Delta / 120;// each ticks mult by 120 => µcro$soft
            zoom = Math.Max(_viewDistanceTrackBar.Minimum, zoom);
            zoom = Math.Min(_viewDistanceTrackBar.Maximum, zoom);
            _viewDistanceTrackBar.Value = zoom;
        }

        private void _perspectiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCamera();
            Scene.Render();
        }

        private void _viewDistanceTrackBar_ValueChanged(object sender, EventArgs e)
        {
            UpdateCamera();
            Scene.Render();
        }

        private void UpdateCamera()
        {
            AScene scene = Scene;
            if (scene == null)
                return;

            bool isDpiScaling = _isDpiScalingCheckBox.Checked;
            bool perspective = _perspectiveCheckBox.Checked;
            double viewDistance = _viewDistanceTrackBar.Value / 10.0;
            _viewDistanceLabel.Text = "" + viewDistance;

            ViewPortCamera camera = scene.ViewPort;
            camera.Orientation = Matrix3.RotateY(_tetaY) * Matrix3.RotateX(_tetaX);
            camera.Location = Point3.Origin + (camera.Orientation * Vector3.ZAxis * viewDistance);
#warning TODO ZETA point - uncomment
            //camera.Perspective = perspective;
            camera.Device.IsDpiScaling = isDpiScaling;
        }

        #endregion User inputs

        #region Scene creation

        private void _cubeBbutton_Click(object sender, EventArgs e)
        {
            CubeScene scene = new CubeScene(Point3.Origin, 0.20);

            Scene = scene;
            UpdateCamera();

            scene.Render();
        }

        private void _sphereButton_Click(object sender, EventArgs e)
        {
            SphereScene scene = new SphereScene(1.0);   // sphere of 1 meter diameter
            scene.BackgroundColor = Color.Black;

            Scene = scene;
            UpdateCamera();
            scene.Build();
        }

        #endregion Scene creation
    }
}