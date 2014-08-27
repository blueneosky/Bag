using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSphere.Geometry
{
    public class Vertex : Vector
    {
        #region ctor

        public Vertex(double x, double y, double z)
            : base(new[] { x, y, z, 1 })
        {
        }

        public Vertex(double x, double y, double z, double w)
            : base(new[] { x, y, z, w })
        {
        }

        public Vertex()
            : base(4)
        {
        }

        /// <summary>
        /// Note : no copy
        /// </summary>
        /// <param name="components"></param>
        private Vertex(double[] components)
            : base(components)
        {
        }

        #endregion ctor

        #region Properties

        public double W
        {
            get { return _components[3]; }
        }

        public double X
        {
            get { return _components[0]; }
        }

        public double Y
        {
            get { return _components[1]; }
        }

        public double Z
        {
            get { return _components[2]; }
        }

        #endregion Properties

        #region override

        public static explicit operator Point3(Vertex v)
        {
            Point3 result = new Point3();
            Array.Copy(v._components, result.Components, 3);

            return result;
        }

        public static explicit operator Vertex(Point3 p)
        {
            Vertex result = new Vertex();
            Array.Copy(p.Components, result._components, 3);
            result.Components[3] = 1;

            return result;
        }

        public static explicit operator Vertex(Vector3 v)
        {
            Vertex result = new Vertex();
            Array.Copy(v.Components, result._components, 3);
            result.Components[3] = 1;

            return result;
        }

        public static explicit operator Vector3(Vertex v)
        {
            Vector3 result = new Vector3();
            Array.Copy(v._components, result.Components, 3);

            return result;
        }

        #endregion override
    }
}