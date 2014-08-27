using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSphere.Geometry;

namespace HSphere.Engine
{
    public sealed class EVertex
    {
        public EVertex(Vertex vertex)
        {
            Vertex = vertex;
        }

        public Vertex Vertex { get; set; }

        /// <summary>
        /// Projection's result
        /// </summary>
        public Vertex ViewPort { get; set; }
    }
}