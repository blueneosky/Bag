using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HSphere.Geometry
{
    public static class MathArray
    {
        public static void AddItems(double[] result, double[] a1, double[] a2)
        {
            int size = result.Length;
            Debug.Assert((a1.Length == size) && (a2.Length == size));

            for (int i = 0; i < size; i++)
            {
                result[i] = a1[i] + a2[i];
            }
        }

        public static bool EqualsItems(double[] a1, double[] a2)
        {
            int size = a1.Length;
            if (a2.Length != size)
                return false;

            for (int i = 0; i < size; i++)
            {
                if (a1[i] != a2[i])
                    return false;
            }

            return true;
        }

        public static void IverseItems(double[] result, double[] a1)
        {
            int size = result.Length;
            Debug.Assert(a1.Length == size);

            for (int i = 0; i < size; i++)
            {
                result[i] = 1.0f / a1[i];
            }
        }

        public static void NegateItems(double[] result, double[] a1)
        {
            int size = result.Length;
            Debug.Assert(a1.Length == size);

            for (int i = 0; i < size; i++)
            {
                result[i] = -a1[i];
            }
        }

        public static void SubtractItems(double[] result, double[] a1, double[] a2)
        {
            int size = result.Length;
            Debug.Assert((a1.Length == size) && (a2.Length == size));

            for (int i = 0; i < size; i++)
            {
                result[i] = a1[i] - a2[i];
            }
        }

        public static void MultMatrices(double[] result, int nbLine, int nbCol, double[] m1, double[] m2, int nbSum)
        {
            Debug.Assert((m1.Length == nbSum * nbLine) && (m2.Length == nbSum * nbCol));

            int offsetCol = 0;                  // offset for result
            int offsetColM1 = 0;
            for (int i = 0; i < nbLine; i++)    // line for result
            {
                int offset = offsetCol;
                for (int j = 0; j < nbCol; j++) // col for result
                {
                    double value = 0;
                    int offsetM1 = offsetColM1;
                    int offsetM2 = j;
                    for (int k = 0; k < nbSum; k++)
                    {
                        value += m1[offsetM1] * m2[offsetM2];
                        offsetM1++;
                        offsetM2 += nbCol;
                    }
                    result[offset] = value;
                    offset++;
                }
                offsetCol += nbCol;
                offsetColM1 += nbSum;
            }
        }

        public static void InvertMatrix3x3(double[] result, double[] m)
        {
            double a = m[0];
            double b = m[1];
            double c = m[2];
            double d = m[3];
            double e = m[4];
            double f = m[5];
            double g = m[6];
            double h = m[7];
            double i = m[8];

            // determinant
            double ei_fh = e * i - f * h;
            double fg_di = f * g - d * i;
            double dh_eg = d * h - e * g;
            double det = (a * ei_fh) + (b * fg_di) + (c * dh_eg);
            Debug.Assert(det != 0);

            //invert
            result[0] = (ei_fh) / det;
            result[1] = (c * h - b * i) / det;
            result[2] = (b * f - c * e) / det;
            result[3] = (fg_di) / det;
            result[4] = (a * i - c * g) / det;
            result[5] = (c * d - a * f) / det;
            result[6] = (dh_eg) / det;
            result[7] = (b * g - a * h) / det;
            result[8] = (a * e - b * d) / det;
        }

        public static void MultItems(double[] result, double[] a, double scalar)
        {
            int size = result.Length;
            Debug.Assert(a.Length == size);

            for (int i = 0; i < size; i++)
            {
                result[i] = a[i] * scalar;
            }
        }

        public static void CrossProuct(double[] result, double[] u, double[] v)
        {
            double u1 = u[0];
            double u2 = u[1];
            double u3 = u[2];
            double v1 = v[0];
            double v2 = v[1];
            double v3 = v[2];

            result[0] = u2 * v3 - u3 * v2;
            result[1] = u3 * v1 - u1 * v3;
            result[2] = u1 * v2 - u2 * v1;
        }

        public static double ComputeLength(double[] u)
        {
            double sum = ComputeSquareLength(u);
            double result = Math.Sqrt(sum);

            return result;
        }

        public static double ComputeSquareLength(double[] u)
        {
            double result = 0.0;

            int size = u.Length;
            for (int i = 0; i < size; i++)
            {
                double c = u[i];
                result += c * c;
            }

            return result;
        }

        internal static double ScalarProduct(double[] u, double[] v)
        {
            double result = 0.0;

            int size = u.Length;
            for (int i = 0; i < size; i++)
            {
                result += u[i] * v[i];
            }

            return result;
        }
    }
}