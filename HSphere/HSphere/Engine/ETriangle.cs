using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using HSphere.Geometry;

namespace HSphere.Engine
{
    public sealed class ETriangle
    {
        public readonly EVertex[] Vertices;

        public ETriangle(EVertex[] vertices, Color? lineColor, Color? fillColor)
        {
            Vertices = vertices;
            LineColor = lineColor;
            FillColor = fillColor;
        }

        public ETriangle(EVertex v1, EVertex v2, EVertex v3, Color? lineColor, Color? fillColor)
        {
            Vertices = new[] { v1, v2, v3 };
            LineColor = lineColor;
            FillColor = fillColor;
        }

        public Color? FillColor { get; set; }

        public HSphere.Geometry.Vector3 Normal
        {
            get
            {
#warning TODO - optimize by Push/Pop matrix vertice
                HSphere.Geometry.Point3 a = (HSphere.Geometry.Point3)Vertices[0].Vertex;
                HSphere.Geometry.Point3 o = (HSphere.Geometry.Point3)Vertices[1].Vertex;
                HSphere.Geometry.Point3 b = (HSphere.Geometry.Point3)Vertices[2].Vertex;
                HSphere.Geometry.Vector3 normal = (a - o) ^ (b - o);

                return normal;
            }
        }

        public Vertex V1 { get { return Vertices[0].ViewPort; } }

        public Vertex V2 { get { return Vertices[1].ViewPort; } }

        public Vertex V3 { get { return Vertices[2].ViewPort; } }

        public Color? LineColor { get; set; }

        public HSphere.Geometry.Vector3 RenderedNormal
        {
            get
            {
#warning TODO - optimize by Push/Pop matrix vertice
                HSphere.Geometry.Point3 a = (HSphere.Geometry.Point3)Vertices[0].ViewPort;
                HSphere.Geometry.Point3 o = (HSphere.Geometry.Point3)Vertices[1].ViewPort;
                HSphere.Geometry.Point3 b = (HSphere.Geometry.Point3)Vertices[2].ViewPort;
                HSphere.Geometry.Vector3 normal = (a - o) ^ (b - o);

                return normal;
            }
        }
    }
}