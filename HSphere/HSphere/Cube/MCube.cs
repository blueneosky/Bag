using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using HSphere.Engine;
using HSphere.Geometry;

namespace HSphere.Cube
{
    public class MCube : Mesh
    {
        private readonly Point3 _cubeOrigin;
        private readonly double _size;

        public MCube(string name, Point3 cubeOrigin, double size)
            : base(name)
        {
            _cubeOrigin = cubeOrigin;
            _size = size;

            // repere
            List<EVertex> vertices = new List<EVertex>
            {
                new EVertex((Vertex)Point3.Origin),
                new EVertex((Vertex)Vector3.XAxis),
                new EVertex((Vertex)Vector3.YAxis),
                new EVertex((Vertex)Vector3.ZAxis),
            };
            List<ELine> lines = new List<ELine>
            {
                new ELine(vertices[0], vertices[1], Color.Red),
                new ELine(vertices[0], vertices[2], Color.Green),
                new ELine(vertices[0], vertices[3], Color.Blue),
            };
            List<ETriangle> triangles = new List<ETriangle> { };

            // cube points
            EVertex[] cubeVertices = new EVertex[8];
            int n;
            for (n = 0; n < 8; n++)
            {
                int i = (n & 1) == 0 ? -1 : 1;
                int j = (n & 2) == 0 ? -1 : 1;
                int k = (n & 4) == 0 ? -1 : 1;
                Point3 p = new Point3(i, j, k) * (size / 2.0);
                p += cubeOrigin;
                cubeVertices[n] = new EVertex((Vertex)p);
            }
            // cube lines
            ELine[] cubeLines = new ELine[12];
            n = 0;
            for (int i = 0; i < cubeVertices.Length - 1; i++)
            {
                EVertex point1 = cubeVertices[i];
                for (int j = i + 1; j < cubeVertices.Length; j++)
                {
                    int deltaMark = i ^ j;
                    if (deltaMark == 1 || deltaMark == 2 || deltaMark == 4)
                    {
                        EVertex point2 = cubeVertices[j];
                        cubeLines[n++] = new ELine(point1, point2, null/*Pens.White*/);
                    }
                }
            }
            //cube faces
            // u . v = ||u||.||v||.cos(u,v)
            // (u,v) is (v0,normal) and must be on same direction
            // so (v0,normal) must be greater than 0 (>cos(Pi/4))
            // => u . v / ( ||u||.||v|| ) > 0
            // => u . v > 0
            List<ETriangle> cubeTriangles = new List<ETriangle> { };
            int[] v0356 = new[] { 0, 3, 5, 6 }; // (0,0,0); (1,1,0); (1,0,1); (0,1,1)
            Color[] colors = new Color[64];
            colors[1] = Color.Red;           // 00 00 01
            colors[2] = Color.DarkRed;       // 00 00 10
            colors[4] = Color.Green;         // 00 01 00
            colors[8] = Color.DarkGreen;     // 00 10 00
            colors[16] = Color.Blue;         // 01 00 00
            colors[32] = Color.DarkBlue;     // 10 00 00
            for (n = 0; n < v0356.Length; n++)
            {
                int i0 = v0356[n];
                int i1 = i0 ^ 1 << 0;
                int i2 = i0 ^ 1 << 1;
                int i3 = i0 ^ 1 << 2;

                EVertex p0 = cubeVertices[i0];
                EVertex p1 = cubeVertices[i1];
                EVertex p2 = cubeVertices[i2];
                EVertex p3 = cubeVertices[i3];

                Vector3 v0 = (Point3)p0.Vertex - cubeOrigin;

                for (int i = 0; i < 3; i++)
                {
                    ETriangle triangle = new ETriangle(p1, p0, p2, null, null);
                    Vector3 normal = triangle.Normal;
                    double scalarProduct = v0 * normal;
                    if (scalarProduct < 0)
                        triangle = new ETriangle(p2, p0, p1, null, null);
                    normal = triangle.Normal;
                    Debug.Assert(v0 * normal > 0);
                    normal *= (1.0 / normal.Length);

                    int a = (int)normal.X;
                    int b = (int)normal.Y;
                    int c = (int)normal.Z;
                    a = (a == -1) ? 1 : (a == 1) ? 2 : 0;
                    b = (b == -1) ? 1 : (b == 1) ? 2 : 0;
                    c = (c == -1) ? 1 : (c == 1) ? 2 : 0;
                    int abc = a + (b << 2) + (c << 4);
                    Color color = colors[abc];
                    triangle.FillColor = color;

                    cubeTriangles.Add(triangle);

                    // permutation
                    EVertex pTemp = p1;
                    p1 = p2;
                    p2 = p3;
                    p3 = pTemp;
                }
            }

            vertices.AddRange(cubeVertices);
            lines.AddRange(cubeLines);
            triangles.AddRange(cubeTriangles);

            Vertices = vertices;
            Lines = lines;
            Triangles = triangles;
        }
    }
}