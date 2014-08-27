using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSphere.Engine
{
    public interface IMesh
    {
        IEnumerable<ELine> Lines { get; }

        string Name { get; }

        IEnumerable<ETriangle> Triangles { get; }

        IEnumerable<EVertex> Vertices { get; }

        void PrepareRender(Camera camera);
    }
}