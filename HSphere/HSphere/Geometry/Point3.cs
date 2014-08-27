using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSphere.Geometry
{
    public class Point3 : Point
    {
        #region Members

        public static readonly Point3 Origin = new Point3(0, 0, 0);

        #endregion Members

        #region ctor

        public Point3(double x, double y, double z)

            : base(new[] { x, y, z })
        {
        }

        public Point3()
            : base(3)
        {
        }

        /// <summary>
        /// Note : no copy
        /// </summary>
        /// <param name="components"></param>
        private Point3(double[] components)

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

        public static explicit operator Point3(Vector3 v)
        {
            return new Point3(v.Components);
        }

        public static Point3 operator -(Point3 p)
        {
            return (Point3)OpNegatePoints(new Point3(), p);
        }

        public static Vector3 operator -(Point3 pDest, Point3 pOrigin)
        {
            return (Vector3)OpSubPoints(new Vector3(), pDest, pOrigin);
        }

        public static Point3 operator -(Point3 p, Vector3 v)
        {
            return (Point3)OpSubVectorFromPoint(new Point3(), p, v);
        }

        public static Point3 operator -(Vector3 v, Point3 p)
        {
            return (Point3)OpSubPointFromVector(new Point3(), v, p);
        }

        public static Point3 operator *(double scalar, Point3 p)
        {
            return (Point3)OpMultPointAndScalar(new Point3(), p, scalar);
        }

        public static Point3 operator *(Point3 p, double scalar)
        {
            return (Point3)OpMultPointAndScalar(new Point3(), p, scalar);
        }

        public static Point3 operator +(Point3 p1, Point3 p2)
        {
            return (Point3)OpAddPoints(new Point3(), p1, p2);
        }

        public static Point3 operator +(Point3 p, Vector3 v)
        {
            return (Point3)OpAddPointAndVector(new Point3(), v, p);
        }

        public static Point3 operator +(Vector3 v, Point3 p)
        {
            return (Point3)OpAddPointAndVector(new Point3(), v, p);
        }

        #endregion override
    }
}