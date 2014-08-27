using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSphere.Engine;
using System.Drawing;
using HSphere.Geometry;

namespace HSphere.Sphere
{
    public class ESLine : ELine
    {
        private EVertex _center;

        public ESLine(EVertex[] vertices, Color? color)
            : base(vertices, color)
        {

        }

        public ESLine(EVertex v1, EVertex v2, Color? color)
            : base(v1, v2, color)
        {

        }

        public EVertex Center
        {
            get
            {
                if (_center == null)
                {
                    Point3 p1 = (Point3)this.Vertices[0].Vertex;
                    Point3 p2 = (Point3)this.Vertices[1].Vertex;
                    Point3 center = (p1 + p2) * 0.5;

                    _center = new EVertex((Vertex)center);
                }
                return _center;
            }
        }

    }
}
