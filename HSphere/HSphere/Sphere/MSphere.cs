using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using HSphere.Engine;
using HSphere.Geometry;

namespace HSphere.Sphere
{
    public class MSphere : Mesh
    {
        private readonly double _radius;
        private readonly List<ETriangle> _triangles = new List<ETriangle>();
        private List<ESLine> _lines = new List<ESLine>();
        private List<EVertex> _vertices = new List<EVertex>();

        public MSphere(string name, double radius)
            : base(name)
        {
            _radius = radius;
        }

        public event EventHandler Changed;

        public void Build()
        {
            BuildCore();
        }

        protected virtual void OnChanged(object sender, EventArgs e)
        {
            EventHandler changed = Changed;
            if (changed == null)
                return;
            changed(sender, e);
        }

        private void AddInitPoint(List<EVertex> ePoints, List<ESLine> lines, Vector3 vector, Color? color)
        {
            Point3 point;
            if (ePoints.Count > 1)
            {
                Point3 firstPoint = (Point3)ePoints[0].Vertex;
                double length = firstPoint.DistanceFromOrigin();
                point = (Point3)(vector * Math.Sqrt(3) * length);
            }
            else
            {
                point = (Point3)vector;
            }
            EVertex ePoint = new EVertex((Vertex)point);
            Point3 sum = point;

            for (int i = 0; i < ePoints.Count; i++)
            {
                EVertex iEPoint = ePoints[i];
                ESLine line = new ESLine(iEPoint, ePoint, color);
                lines.Add(line);
                _lines.Add(line);
                sum += (Point3)iEPoint.Vertex;
            }

            ePoints.Add(ePoint);
            _vertices.Add(ePoint);

            if (ePoints.Count == 1)
                return;
            Vector3 centroyd = (Vector3)(sum * (-1.0 / ePoints.Count));
            for (int i = 0; i < ePoints.Count; i++)
            {
                EVertex iEPoint = ePoints[i];
                Point3 p = (Point3)iEPoint.Vertex + centroyd;
                double length = p.DistanceFromOrigin();
                iEPoint.Vertex = (Vertex)(p * (_radius / length));
            }
        }

        private void BuildCore()
        {
            Color? color = Color.White;

            _vertices = new List<EVertex>
            {
                new EVertex((Vertex)Point3.Origin),   // make a copy
                new EVertex((Vertex)Vector3.XAxis),
                new EVertex((Vertex)Vector3.YAxis),
                new EVertex((Vertex)Vector3.ZAxis),
            };
            _lines = new List<ESLine>
            {
                new ESLine(_vertices[0], _vertices[1], Color.Red),
                new ESLine(_vertices[0], _vertices[2], Color.Green),
                new ESLine(_vertices[0], _vertices[3], Color.Blue),
            };

            //int offsetPoints = _points.Count;
            int offsetLines = _lines.Count;
            List<EVertex> ePoints = new List<EVertex>();
            List<ESLine> lines = new List<ESLine>();

            BuildCoreInit(color, ePoints, lines);

#warning TODO ALPHA point
        }

        private void BuildCoreInit(Color? color, List<EVertex> ePoints, List<ESLine> lines)
        {
            int pause = 500;
            AddInitPoint(ePoints, lines, (Vector3)Point3.Origin, color);
            OnChanged(this, EventArgs.Empty);
            Thread.Sleep(pause);

            AddInitPoint(ePoints, lines, Vector3.XAxis, color);
            OnChanged(this, EventArgs.Empty);
            Thread.Sleep(pause);

            AddInitPoint(ePoints, lines, Vector3.YAxis, color);
            OnChanged(this, EventArgs.Empty);
            Thread.Sleep(pause);

            AddInitPoint(ePoints, lines, Vector3.ZAxis, color);
            OnChanged(this, EventArgs.Empty);
            Thread.Sleep(pause);
        }
    }
}