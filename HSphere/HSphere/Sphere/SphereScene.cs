using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using HSphere.Engine;
using HSphere.Geometry;
using System.Threading;

namespace HSphere.Sphere
{
    public class SphereScene : Scene
    {
        MSphere _mSphere;
        public SphereScene(double radius)
        {
            _mSphere = new MSphere("mySphere", radius);
            _mSphere.Changed += new EventHandler(_mSphere_Changed);
            SetMeshes(new List<IMesh> { _mSphere });
        }

        void _mSphere_Changed(object sender, EventArgs e)
        {
            this.Render();
        }

        public void Build()
        {
            ThreadStart ts = _mSphere.Build;
            new Thread(ts).Start();
        }


    }
}