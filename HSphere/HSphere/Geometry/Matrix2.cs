using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSphere.Geometry
{
    public class Matrix2 : Matrix
    {
        #region ctor

        public Matrix2()
            : base(2, 2)
        {
        }

        public Matrix2(double a, double b, double c, double d)
            : base(new[] { a, b, c, d }, 2, 2)
        {
        }

        protected Matrix2(double[] components)
            : base(components, 2, 2)
        {
        }

        #endregion ctor

        #region overrides

        public static Matrix2 operator -(Matrix2 m)
        {
            return (Matrix2)OpNegateMatrix(new Matrix2(), m);
        }

        public static Matrix2 operator -(Matrix2 m1, Matrix2 m2)
        {
            return (Matrix2)OpSubMatrices(new Matrix2(), m1, m2);
        }

        public static Vector2 operator *(Matrix2 m, Vector2 v)
        {
            return (Vector2)OpMultMatrixWithVector(new Vector2(), m, v);
        }

        public static Point2 operator *(Matrix2 m, Point2 p)
        {
            return (Point2)OpMultMatrixWithPoint(new Point2(), m, p);
        }

        public static Matrix2 operator *(Matrix2 m1, Matrix2 m2)
        {
            return (Matrix2)OpMultMatrices(new Matrix2(), m1, m2);
        }

        public static Matrix2 operator +(Matrix2 m1, Matrix2 m2)
        {
            return (Matrix2)OpAddMatrices(new Matrix2(), m1, m2);
        }

        #endregion overrides

        #region Functions

        public static Matrix2 Rotate(double angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            Matrix2 matrix = new Matrix2(cos, -sin, sin, cos);

            return matrix;
        }

        #endregion
    }
}