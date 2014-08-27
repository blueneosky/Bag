using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSphere.Engine
{
    public class Mesh : IMesh
    {
        public Mesh(string name)
        {
            Name = name;
            Vertices = new EVertex[0];
            Lines = new ELine[0];
            Triangles = new ETriangle[0];
        }

        public IEnumerable<ELine> Lines { get; set; }

        public string Name { get; protected set; }

        public IEnumerable<ETriangle> Triangles { get; set; }

        public IEnumerable<EVertex> Vertices { get; set; }

        public virtual void PrepareRender(Camera camera)
        {
        }
    }
}