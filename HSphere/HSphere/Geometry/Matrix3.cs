using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSphere.Geometry
{
    public class Matrix3 : Matrix
    {
        #region Members

        public static readonly Matrix3 Identity = new Matrix3(
            1, 0, 0,
            0, 1, 0,
            0, 0, 1);

        private Matrix3 _invert;

        #endregion Members

        #region ctor

        public Matrix3()
            : base(3, 3)
        {
        }

        public Matrix3(
            double a00, double a10, double a20,
            double a01, double a11, double a21,
            double a02, double a12, double a22)
            : base(new[] { a00, a10, a20, a01, a11, a21, a02, a12, a22 }, 3, 3)
        {
        }

        protected Matrix3(double[] components)
            : base(components, 3, 3)
        {
        }

        #endregion ctor

        #region Properties

        public Matrix3 Invert
        {
            get
            {
                if (_invert == null)
                {
                    _invert = new Matrix3();
                    MathArray.InvertMatrix3x3(_invert._components, _components);
                    _invert._invert = this; // optim
                }
                return _invert;
            }
        }

        #endregion Properties

        #region overrides

        public static Matrix3 operator -(Matrix3 m)
        {
            return (Matrix3)OpNegateMatrix(new Matrix3(), m);
        }

        public static Matrix3 operator -(Matrix3 m1, Matrix3 m2)
        {
            return (Matrix3)OpSubMatrices(new Matrix3(), m1, m2);
        }

        public static Vector3 operator *(Matrix3 m, Vector3 v)
        {
            return (Vector3)OpMultMatrixWithVector(new Vector3(), m, v);
        }

        public static Point3 operator *(Matrix3 m, Point3 p)
        {
            return (Point3)OpMultMatrixWithPoint(new Point3(), m, p);
        }

        public static Matrix3 operator *(Matrix3 m1, Matrix3 m2)
        {
            return (Matrix3)OpMultMatrices(new Matrix3(), m1, m2);
        }

        public static Matrix3 operator +(Matrix3 m1, Matrix3 m2)
        {
            return (Matrix3)OpAddMatrices(new Matrix3(), m1, m2);
        }

        #endregion overrides

        #region Functions

        public static Matrix3 RotateX(double angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            Matrix3 matrix = new Matrix3(
                1, 0, 0,
                0, cos, -sin,
                0, sin, cos);

            return matrix;
        }

        public static Matrix3 RotateY(double angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            Matrix3 matrix = new Matrix3(
                cos, 0, sin,
                0, 1, 0,
                -sin, 0, cos);

            return matrix;
        }

        public static Matrix3 RotateZ(double angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            Matrix3 matrix = new Matrix3(
                cos, -sin, 0,
                sin, cos, 0,
                0, 0, 1);

            return matrix;
        }

        #endregion Functions
    }
}