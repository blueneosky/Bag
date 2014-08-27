using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSphere.Geometry;

namespace HSphere.Engine
{
    public class Camera
    {
        public static readonly double ConstHumanEyeZoom = 1.4;

        public Camera()
        {
            Location = Point3.Origin;
            Orientation = Matrix3.Identity;
        }

        public Point3 Location { get; set; }

        /// <summary>
        /// Scene is seen through (x, y) plane, negative z is before (and positive behind)
        /// </summary>
        public Matrix3 Orientation { get; set; }
    }
}