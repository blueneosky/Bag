using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSphere.Geometry
{
    public class Matrix
    {
        #region Members

        protected readonly double[] _components;
        protected readonly int _nbCol;
        protected readonly int _nbLine;

        #endregion Members

        #region ctor

        public Matrix(int nbLine, int nbCol)
        {
            _components = new double[nbLine * nbCol];
            _nbLine = nbLine;
            _nbCol = nbCol;
        }

        /// <summary>
        /// Note : no copy
        /// </summary>
        /// <param name="components"></param>
        /// <param name="nbLine"></param>
        /// <param name="nbCol"></param>
        protected Matrix(double[] components, int nbLine, int nbCol)
        {
            _components = components;
            _nbLine = nbLine;
            _nbCol = nbCol;
        }

        #endregion ctor

        #region Properties

        public double[] Components
        {
            get { return _components; }
        }

        public int NbCol
        {
            get { return _nbCol; }
        }

        public int NbLine
        {
            get { return _nbLine; }
        }

        public double this[int i, int j]
        {
            get { return _components[i * _nbCol + j]; }
        }

        #endregion Properties

        #region overrides

        public static bool Equals(Matrix m1, Matrix m2)
        {
            if (Object.ReferenceEquals(m1, m2))
                return true;

            if ((m1 == null) || (m2 == null))
                return false;

            return MathArray.EqualsItems(m1._components, m2._components);
        }

        public static Matrix operator -(Matrix m)
        {
            return OpNegateMatrix(new Matrix(m._nbLine, m._nbCol), m);
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            return OpSubMatrices(new Matrix(m1._nbLine, m1._nbCol), m1, m2);
        }

        public static Vector operator *(Matrix m, Vector v)
        {
            return OpMultMatrixWithVector(new Vector(m._nbLine), m, v);
        }

        public static Point operator *(Matrix m, Point p)
        {
            return OpMultMatrixWithPoint(new Point(m._nbLine), m, p);
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            return OpMultMatrices(new Matrix(m1._nbLine, m2._nbCol), m1, m2);
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            return OpAddMatrices(new Matrix(m1._nbLine, m1._nbCol), m1, m2);
        }

        public override bool Equals(object obj)
        {
            Matrix matrix = obj as Matrix;
            if (matrix == null)
                return false;

            return Equals(this, matrix);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string numbers = String.Empty;
            if (_components.Length > 0)
                numbers = _components
                    .Select((c, i) => Tuple.Create(c.ToString(), (int)(i / _nbCol)))
                    .GroupBy(t => t.Item2, t => t.Item1)
                    .Select(grp => "[ " + grp.Aggregate((acc, c) => acc + "; " + c) + " ]")
                    .Aggregate((acc, g) => acc + g);
            return "[" + numbers + "]";
        }

        #endregion overrides

        #region Protected functions

        protected static Matrix OpAddMatrices(Matrix result, Matrix m1, Matrix m2)
        {
            MathArray.AddItems(result._components, m1._components, m2._components);

            return result;
        }

        protected static Matrix OpMultMatrices(Matrix result, Matrix m1, Matrix m2)
        {
            MathArray.MultMatrices(result._components, m1._nbLine, m2._nbCol, m1._components, m2._components, m1._nbCol);

            return result;
        }

        protected static Vector OpMultMatrixWithVector(Vector result, Matrix m, Vector v)
        {
            MathArray.MultMatrices(result.Components, m._nbLine, 1, m._components, v.Components, m._nbCol);

            return result;
        }

        protected static Point OpMultMatrixWithPoint(Point result, Matrix m, Point p)
        {
            MathArray.MultMatrices(result.Components, m._nbLine, 1, m._components, p.Components, m._nbCol);

            return result;
        }

        protected static Matrix OpNegateMatrix(Matrix result, Matrix m)
        {
            MathArray.NegateItems(result._components, m._components);

            return result;
        }

        protected static Matrix OpSubMatrices(Matrix result, Matrix m1, Matrix m2)
        {
            MathArray.SubtractItems(result._components, m1._components, m2._components);

            return result;
        }

        #endregion Protected functions
    }
}