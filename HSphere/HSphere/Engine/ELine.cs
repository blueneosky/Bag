using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using HSphere.Geometry;

namespace HSphere.Engine
{
    public class ELine
    {
        public readonly EVertex[] Vertices;

        public ELine(EVertex[] points, Color? color)
        {
            Vertices = points;
            Color = color;
        }

        public ELine(EVertex p1, EVertex p2, Color? color)
        {
            Vertices = new[] { p1, p2 };
            Color = color;
        }

        public Vertex V1 { get { return Vertices[0].ViewPort; } }

        public Vertex V2 { get { return Vertices[1].ViewPort; } }

        public Color? Color { get; set; }
    }
}