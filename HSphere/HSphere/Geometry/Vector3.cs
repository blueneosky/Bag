using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSphere.Geometry
{
    public class Vector3 : Vector
    {
        #region Members

        public static readonly Vector3 XAxis = new Vector3(1, 0, 0);
        public static readonly Vector3 YAxis = new Vector3(0, 1, 0);
        public static readonly Vector3 ZAxis = new Vector3(0, 0, 1);

        #endregion Members

        #region ctor

        public Vector3(double x, double y, double z)
            : base(new[] { x, y, z })
        {
        }

        public Vector3()
            : base(3)
        {
        }

        /// <summary>
        /// Note : no copy
        /// </summary>
        /// <param name="components"></param>
        private Vector3(double[] components)
            : base(components)
        {
        }

        #endregion ctor

        #region Properties

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

        public static explicit operator Vector3(Point3 p)
        {
            return new Vector3(p.Components);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return (Vector3)OpSubVectors(new Vector3(), v1, v2);
        }

        public static Vector3 operator -(Vector3 v)
        {
            return (Vector3)OpNegateVector(new Vector3(), v);
        }

        public static Vector3 operator *(double scalar, Vector3 v)
        {
            return (Vector3)OpMultVectorAndScalar(new Vector3(), v, scalar);
        }

        public static Vector3 operator *(Vector3 v, double scalar)
        {
            return (Vector3)OpMultVectorAndScalar(new Vector3(), v, scalar);
        }

        public static double operator *(Vector3 v1, Vector3 v2)
        {
            return ScalarProduct(v1, v2);
        }

        public static Vector3 operator ^(Vector3 v1, Vector3 v2)
        {
            return CrossProduct(new Vector3(), v1, v2);
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return (Vector3)OpAddVectors(new Vector3(), v1, v2);
        }

        #endregion override

        #region Protected functions

        protected static Vector3 CrossProduct(Vector3 result, Vector3 v1, Vector3 v2)
        {
            MathArray.CrossProuct(result._components, v1._components, v2._components);

            return result;
        }

        #endregion Protected functions
    }
}