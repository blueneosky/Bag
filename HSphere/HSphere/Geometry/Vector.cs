using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

namespace HSphere.Geometry
{
    public class Vector
    {
        #region Members

        protected readonly double[] _components;

        #endregion Members

        #region ctor

        public Vector(int size)
        {
            _components = new double[size];
        }

        public Vector(int size, double value)
        {
            _components = new double[size];
            for (int i = 0; i < size; i++)
            {
                _components[i] = value;
            }
        }

        public Vector(IEnumerable<double> values)
        {
            _components = values.ToArray();
        }

        /// <summary>
        /// Note : no copy
        /// </summary>
        /// <param name="components"></param>
        protected Vector(double[] components)
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

        public static bool Equals(Vector v1, Vector v2)
        {
            if (Object.ReferenceEquals(v1, v2))
                return true;

            if ((v1 == null) || (v2 == null))
                return false;

            return MathArray.EqualsItems(v1._components, v2._components);
        }

        public static explicit operator Vector(Point p)
        {
            return new Vector(p.Components);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return OpSubVectors(new Vector(v1.Size), v1, v2);
        }

        public static Vector operator -(Vector v)
        {
            return OpNegateVector(new Vector(v.Size), v);
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !Equals(v1, v2);
        }

        public static double operator *(Vector v1, Vector v2)
        {
            return ScalarProduct(v1, v2);
        }

        public static Vector operator *(double scalar, Vector v)
        {
            return OpMultVectorAndScalar(new Vector(v.Size), v, scalar);
        }

        public static Vector operator *(Vector v, double scalar)
        {
            return OpMultVectorAndScalar(new Vector(v.Size), v, scalar);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return OpAddVectors(new Vector(v1.Size), v1, v2);
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            return Equals(v1, v2);
        }

        public override bool Equals(object obj)
        {
            Vector vector = obj as Vector;
            if (vector == null)
                return false;

            return Equals(this, vector);
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
            return "{ " + numbers + " }";
        }

        #endregion override

        #region Protected functions

        protected static Vector OpAddVectors(Vector result, Vector v1, Vector v2)
        {
            MathArray.AddItems(result._components, v1._components, v2._components);

            return result;
        }

        protected static Vector OpMultVectorAndScalar(Vector result, Vector v, double scalar)
        {
            MathArray.MultItems(result._components, v._components, scalar);

            return result;
        }

        protected static Vector OpNegateVector(Vector result, Vector v)
        {
            MathArray.NegateItems(result._components, v._components);

            return result;
        }

        protected static Vector OpSubVectors(Vector result, Vector v1, Vector v2)
        {
            MathArray.SubtractItems(result._components, v1._components, v2._components);

            return result;
        }

        protected static double ScalarProduct(Vector v1, Vector v2)
        {
            return MathArray.ScalarProduct(v1._components, v2._components);
        }

        #endregion Protected functions
    }
}