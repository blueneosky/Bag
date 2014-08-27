using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSphere.Geometry
{
    public class Point
    {
        #region Members

        protected readonly double[] _components;

        #endregion Members

        #region ctor

        public Point(int size)
        {
            _components = new double[size];
        }

        public Point(int size, double value)
        {
            _components = new double[size];
            for (int i = 0; i < size; i++)
            {
                _components[i] = value;
            }
        }

        public Point(IEnumerable<double> values)
        {
            _components = values.ToArray();
        }

        /// <summary>
        /// Note : no copy !
        /// </summary>
        /// <param name="components"></param>
        protected Point(double[] components)
        {
            _components = components;
        }

        #endregion ctor

        #region Property

        public double[] Components
        {
            get { return _components; }
        }

        public double Length
        {
            get { return MathArray.ComputeLength(_components); }
        }

        public int Size
        {
            get { return _components.Length; }
        }

        public double SquareLength
        {
            get { return MathArray.ComputeSquareLength(_components); }
        }

        public double this[int i]
        {
            get { return _components[i]; }
        }

        #endregion Property

        #region override

        public static bool Equals(Point p1, Point p2)
        {
            if (Object.ReferenceEquals(p1, p2))
                return true;

            if ((p1 == null) || (p2 == null))
                return false;

            return MathArray.EqualsItems(p1._components, p2._components);
        }

        public static explicit operator Point(Vector v)
        {
            return new Point(v.Components);
        }

        public static Vector operator -(Point pDest, Point pOrigin)
        {
            return OpSubPoints(new Vector(pDest.Size), pDest, pOrigin);
        }

        public static Point operator -(Point p)
        {
            return OpNegatePoints(new Point(p.Size), p);
        }

        public static Point operator -(Point p, Vector v)
        {
            return OpSubVectorFromPoint(new Point(p.Size), p, v);
        }

        public static Point operator -(Vector v, Point p)
        {
            return OpSubPointFromVector(new Point(v.Size), v, p);
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !Equals(p1, p2);
        }

        public static Point operator *(double scalar, Point p)
        {
            return OpMultPointAndScalar(new Point(p.Size), p, scalar);
        }

        public static Point operator *(Point p, double scalar)
        {
            return OpMultPointAndScalar(new Point(p.Size), p, scalar);
        }

        public static Point operator +(Point p1, Point p2)
        {
            return OpAddPoints(new Point(p1.Size), p1, p2);
        }

        public static Point operator +(Point p, Vector v)
        {
            return OpAddPointAndVector(new Point(p.Size), v, p);
        }

        public static Point operator +(Vector v, Point p)
        {
            return OpAddPointAndVector(new Point(v.Size), v, p);
        }

        public static bool operator ==(Point p1, Point p2)
        {
            return Equals(p1, p2);
        }

        public override bool Equals(object obj)
        {
            Point point = obj as Point;
            if (point == null)
                return false;

            return Equals(this, point);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string numbers = String.Empty;
            if (_components.Length > 0)
                numbers = _components.Select(c => c.ToString()).Aggregate((acc, c) => acc + "; " + c);
            return "( " + numbers + " )";
        }

        #endregion override

        #region Protected functions

        protected static Point OpAddPointAndVector(Point result, Vector v, Point p)
        {
            MathArray.AddItems(result._components, v.Components, p._components);

            return result;
        }

        protected static Point OpAddPoints(Point result, Point p1, Point p2)
        {
            MathArray.AddItems(result._components, p1._components, p2._components);

            return result;
        }

        protected static Point OpMultPointAndScalar(Point result, Point p, double scalar)
        {
            MathArray.MultItems(result._components, p._components, scalar);

            return result;
        }

        protected static Point OpNegatePoints(Point result, Point p)
        {
            MathArray.NegateItems(result._components, p._components);

            return result;
        }

        protected static Point OpSubPointFromVector(Point result, Vector v, Point p)
        {
            MathArray.SubtractItems(result._components, v.Components, p._components);

            return result;
        }

        protected static Vector OpSubPoints(Vector result, Point pDest, Point pOrigin)
        {
            MathArray.SubtractItems(result.Components, pDest._components, pOrigin._components);

            return result;
        }

        protected static Point OpSubVectorFromPoint(Point result, Point p, Vector v)
        {
            MathArray.SubtractItems(result._components, p._components, v.Components);

            return result;
        }

        #endregion Protected functions

        #region Functions

        public double DistanceFromOrigin()
        {
            double sum = 0;
            int size = _components.Length;
            for (int i = 0; i < size; i++)
            {
                double component = _components[i];
                sum += (component * component);
            }
            double result = Math.Sqrt(sum);

            return result;
        }

        #endregion Functions
    }
}