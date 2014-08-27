using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSphere.Geometry
{
    public class Point2 : Point
    {
        #region Members

        public static readonly Point2 Origin = new Point2(0, 0);

        #endregion Members

        #region ctor

        public Point2(double x, double y)
            : base(new[] { x, y })
        {
        }

        public Point2()
            : base(2)
        {
        }

        /// <summary>
        /// Note : no copy
        /// </summary>
        /// <param name="components"></param>
        private Point2(double[] components)
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

        public static explicit operator Point2(Vector2 v)
        {
            return new Point2(v.Components);
        }

        public static Point2 operator -(Point2 p)
        {
            return (Point2)OpNegatePoints(new Point2(), p);
        }

        public static Vector2 operator -(Point2 pDest, Point2 pOrigin)
        {
            return (Vector2)OpSubPoints(new Vector2(), pDest, pOrigin);
        }

        public static Point2 operator -(Point2 p, Vector2 v)
        {
            return (Point2)OpSubVectorFromPoint(new Point2(), p, v);
        }

        public static Point2 operator -(Vector2 v, Point2 p)
        {
            return (Point2)OpSubPointFromVector(new Point2(), v, p);
        }

        public static Point2 operator *(double scalar, Point2 p)
        {
            return (Point2)OpMultPointAndScalar(new Point2(), p, scalar);
        }

        public static Point2 operator *(Point2 p, double scalar)
        {
            return (Point2)OpMultPointAndScalar(new Point2(), p, scalar);
        }

        public static Point2 operator +(Point2 p1, Point2 p2)
        {
            return (Point2)OpAddPoints(new Point2(), p1, p2);
        }

        public static Point2 operator +(Point2 p, Vector2 v)
        {
            return (Point2)OpAddPointAndVector(new Point2(), v, p);
        }

        public static Point2 operator +(Vector2 v, Point2 p)
        {
            return (Point2)OpAddPointAndVector(new Point2(), v, p);
        }

        #endregion override
    }
}