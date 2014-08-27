using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSphere.Geometry
{
    public class Vector2 : Vector
    {
        #region Members

        public static readonly Vector2 XAxis = new Vector2(1, 0);
        public static readonly Vector2 YAxis = new Vector2(0, 1);

        #endregion Members

        #region ctor

        public Vector2(double x, double y)
            : base(new[] { x, y })
        {
        }

        public Vector2()
            : base(2)
        {
        }

        /// <summary>
        /// Note : no copy
        /// </summary>
        /// <param name="components"></param>
        private Vector2(double[] components)
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

        #endregion Properties

        #region override

        public static explicit operator Vector2(Point2 p)
        {
            return new Vector2(p.Components);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return (Vector2)OpSubVectors(new Vector2(), v1, v2);
        }

        public static Vector2 operator -(Vector2 v)
        {
            return (Vector2)OpNegateVector(new Vector2(), v);
        }

        public static Vector2 operator *(double scalar, Vector2 v)
        {
            return (Vector2)OpMultVectorAndScalar(new Vector2(), v, scalar);
        }

        public static Vector2 operator *(Vector2 v, double scalar)
        {
            return (Vector2)OpMultVectorAndScalar(new Vector2(), v, scalar);
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return (Vector2)OpAddVectors(new Vector2(), v1, v2);
        }

        #endregion override
    }
}