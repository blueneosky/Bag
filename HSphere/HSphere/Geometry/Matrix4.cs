using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSphere.Geometry
{
    public class Matrix4 : Matrix
    {
        #region Members

        public static readonly Matrix4 Identity = new Matrix4(new double[]{
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0 ,0, 1});

        private Matrix4 _invert;

        #endregion Members

        #region ctor

        public Matrix4()
            : base(4, 4)
        {
        }

        public Matrix4(
            double a00, double a10, double a20, double a30,
            double a01, double a11, double a21, double a31,
            double a02, double a12, double a22, double a32,
            double a03, double a13, double a23, double a33)
            : base(new[] { 
            a00, a10, a20, a30,
            a01, a11, a21, a31,
            a02, a12, a22, a32,
            a03, a13, a23, a33 }, 4, 4)
        {

        }

        protected Matrix4(double[] components)
            : base(components, 4, 4)
        {
        }

        #endregion ctor

        #region overrides

        public static explicit operator Matrix4(Matrix3 m)
        {
            Matrix4 result = new Matrix4();
            double[] c = m.Components;
            double[] cResult = result._components;
            for (int i = 0; i < 3; i++)
            {
                Array.Copy(c, i * 3, cResult, i * 4, 3);
            }

            return result;
        }

        public static Matrix4 operator -(Matrix4 m)
        {
            return (Matrix4)OpNegateMatrix(new Matrix4(), m);
        }

        public static Matrix4 operator -(Matrix4 m1, Matrix4 m2)
        {
            return (Matrix4)OpSubMatrices(new Matrix4(), m1, m2);
        }

        public static Vector3 operator *(Matrix4 m, Vector3 v)
        {
            return (Vector3)OpMultMatrixWithVector(new Vector3(), m, v);
        }

        public static Point3 operator *(Matrix4 m, Point3 p)
        {
            return (Point3)OpMultMatrixWithPoint(new Point3(), m, p);
        }

        public static Matrix4 operator *(Matrix4 m1, Matrix4 m2)
        {
            return (Matrix4)OpMultMatrices(new Matrix4(), m1, m2);
        }

        public static Vertex operator *(Matrix4 m, Vertex v)
        {
            return (Vertex)OpMultMatrixWithVector(new Vertex(), m, v);
        }

        public static Matrix4 operator +(Matrix4 m1, Matrix4 m2)
        {
            return (Matrix4)OpAddMatrices(new Matrix4(), m1, m2);
        }

        #endregion overrides

        #region Functions

        public static Matrix4 RotateX(double angle)
        {
            double s = Math.Sin(angle);
            double c = Math.Cos(angle);
            Matrix4 matrix = new Matrix4(new[]{
                1, 0,  0, 0,
                0, c, -s, 0,
                0, s,  c, 0,
                0, 0,  0, 1});

            return matrix;
        }

        public static Matrix4 RotateY(double angle)
        {
            double s = Math.Sin(angle);
            double c = Math.Cos(angle);
            Matrix4 matrix = new Matrix4(new[]{
                 c, 0, s, 0,
                 0, 1, 0, 0,
                -s, 0, c, 0,
                 0, 0, 0, 1});

            return matrix;
        }

        public static Matrix4 RotateZ(double angle)
        {
            double s = Math.Sin(angle);
            double c = Math.Cos(angle);
            Matrix4 matrix = new Matrix4(new[]{
                c, -s, 0, 0,
                s,  c, 0, 0,
                0,  0, 1, 0,
                0,  0, 0, 1});

            return matrix;
        }

        public static Matrix4 Translate(Vector3 v)
        {
            Matrix4 matrix = new Matrix4(new[]{
                1, 0, 0, v[0],
                0, 1, 0, v[1],
                0, 0, 1, v[2],
                0, 0, 0, 1    });

            return matrix;
        }

        #endregion Functions
    }
}