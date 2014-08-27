using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using HSphere.Geometry;

namespace HSphere.Engine
{
    public sealed class ViewPortCamera : Camera
    {
        #region Members

        private double _bottom;
        private double _far;
        private double _left;
        private double _near;
        private double _right;
        private double _top;

        #endregion Members

        #region ctor

        public ViewPortCamera()
            : base()
        {
            Device = new ViewPort();
        }

        #endregion ctor

        #region Properties

        private ViewPort _device;

        public double Bottom
        {
            get { return _bottom; }
        }

        public ViewPort Device
        {
            get { return _device; }
            set
            {
                if (_device != null)
                {
                    _device.ViewPortChanged -= OnDeviceSizeChanged;
                    _device.Dispose();
                }
                _device = value;
                if (_device != null)
                {
                    _device.ViewPortChanged += OnDeviceSizeChanged;
                }
            }
        }

        public double Far
        {
            get { return _far; }
        }

        public double Left
        {
            get { return _left; }
        }

        public double Near
        {
            get { return _near; }
        }

        public double Right
        {
            get { return _right; }
        }

        public double Top
        {
            get { return _top; }
        }

        #endregion Properties

        #region Events

        public event EventHandler DeviceSizeChanged;

        protected void OnDeviceSizeChanged(object sender, EventArgs e)
        {
            EventHandler deviceSizeChanged = DeviceSizeChanged;
            if (deviceSizeChanged == null)
                return;
            deviceSizeChanged(this, e);
        }

        #endregion Events

        #region Functions

        public void Render(IEnumerable<IMesh> meshes, Color backColor)
        {
            Matrix4 vertexToDevice = GetVertexToDeviceMatrix();

#warning TODO ZETA point - uncomment
            //Device.Lock();

            Device.Clear(backColor);
            foreach (IMesh mesh in meshes)
            {
                Render(mesh, vertexToDevice);
            }

#warning TODO ZETA point - uncomment
            //Device.Unlock();
        }

        public void SetViewPort(double left, double right, double top, double bottom, double near, double far)
        {

        }

        #endregion Functions

        #region Private

        private void Draw(IMesh mesh)
        {
            ViewPort device = Device;
            IEnumerable<ELine> meshLines = mesh.Lines;
            IEnumerable<ETriangle> meshTriangles = mesh.Triangles;

            IList<ELine> lines = meshLines as IList<ELine>;
            if (lines == null)
                lines = meshLines.ToList();

            IList<ETriangle> triangles = meshTriangles as IList<ETriangle>;
            if (triangles == null)
                triangles = meshTriangles.ToList();

            int trianglesLength = triangles.Count;
            for (int i = 0; i < trianglesLength; i++)
            {
                ETriangle triangle = triangles[i];

                Vector3 normal = triangle.RenderedNormal;
                if (normal.Z <= 0)
                    continue;   // not visible

                device.DrawTriangle(triangle);
            }

            int linesLength = lines.Count;
            for (int i = 0; i < linesLength; i++)
            {
                ELine line = lines[i];
                device.DrawLine(line);
            }
        }

        private Matrix4 GetVertexToDeviceMatrix()
        {
            // Note :
            // P@Rcam = Trans' + Rot . P@Rscene
            // with :
            //  Trans' = Vect(0cam, 0scene)@Rcam
            //  Rot = Rot(Rscene -> Rcam)
            //      = Rot(Rcam -> Rscene)^-1
            //      = [ Vect(x)@Rscene , Vect(y)@Rscene , Vect(z)@Rscene ]^-1
            //      = Camera.Orientation^-1
            // but Trans' is in Rcam, do some tricky math
            // P@Rcam = Rot . Rot^-1 . Trans' + Rot . P@Rscene
            //        = Rot . ( Rot^-1 . Trans' + P@Rscene )
            //        = Rot . ( Trans + P@Rscene )
            // and
            // Trans = Rot^-1 . Trans'
            //       = Rot(Rcam -> Rscene) . Vect(0cam, 0scene)@Rcam
            //       = Vect(0cam, 0scene)@Rscene
            //       = -Camera.Location
            //
            // here we are !
            // ********************************************************
            // P@Rcam = Rot . ( Trans + P@Rscene )
            // Rot = Camera.Orientation^-1
            // Trans = -Camera.Location
            // ********************************************************
            // Trans + P@Rscene = MTrans . P@Rscene
            // => P@Rcam = Rot . MTrans . P@Rscene
            //    P@Rcam = MViewPort + P@Rscene
            // MViewPort = Rot . MTrans
            // *******************************************************

            Vector3 trans = (Vector3)(-Location);
            Matrix4 mTrans = Matrix4.Translate(trans);
            Matrix4 rot = (Matrix4)Orientation.Invert;
            Matrix4 mViewPort = rot * mTrans;
#warning TODO ZETA point - uncomment
            //bool perspective = Perspective;
            bool isDpiScaling = Device.IsDpiScaling;

            double f = 0.0;     // globalViewFactor
            double d = 0.0;     // distanceFromCenterScene
            double z = 0.0;     // zFactor
            if (isDpiScaling)
            {
#warning TODO ZETA point - uncomment
                //f = Zoom
                //    * Device.DpiX // pixel by inch
                //    * 39.37; // inch by meter
            }
            else
            {
#warning TODO ZETA point - uncomment
                //f = Zoom * Device.RenderFactor;
            }
#warning TODO ZETA point - uncomment
            //if (perspective)
            //{
            //    z = 1.0;
            //}
            //else
            {
                // compute the center screen point
                Vertex center = (Vertex)Point3.Origin;
                center = mViewPort * center;
                d = -center.Z;    // negate => the Z vector go to the viewer (not the scene)
            }
            Matrix4 mProjection = new Matrix4(
                f, 0, 0, 0,
                0, f, 0, 0,
                0, 0, f, 0,
                0, 0, z, d);

            Matrix4 mDevice = mProjection * mViewPort;

            return mDevice;
        }

        private void Project(IMesh mesh, Matrix4 vertexToDevice)
        {
            IEnumerable<EVertex> meshVertices = mesh.Vertices;
            IList<EVertex> vertices = meshVertices as IList<EVertex>;
            if (vertices == null)
                vertices = meshVertices.ToList();

            int pointsLength = vertices.Count;
            for (int i = 0; i < vertices.Count; i++)
            {
                EVertex ePoint = vertices[i];
                ePoint.ViewPort = vertexToDevice * ePoint.Vertex;
            }
        }

        private void Render(IMesh mesh, Matrix4 vertexToDevice)
        {
            mesh.PrepareRender(this);
            Project(mesh, vertexToDevice);
            Draw(mesh);
        }

        #endregion Private
    }
}