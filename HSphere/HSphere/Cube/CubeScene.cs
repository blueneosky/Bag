using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using HSphere.Engine;
using HSphere.Geometry;

namespace HSphere.Cube
{
    public class CubeScene : Scene
    {
        public CubeScene(Point3 cubeOrigin, double size)
        {
            MCube mCube = new MCube("myCube", cubeOrigin, size);

            BackgroundColor = Color.Black;
            SetMeshes(new List<IMesh> { mCube });
        }
    }
}