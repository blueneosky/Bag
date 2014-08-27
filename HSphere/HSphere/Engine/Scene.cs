using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSphere.Engine
{
    public class Scene : AScene
    {
        IEnumerable<IMesh> _meshes;
        public Scene()
        {
            _meshes = new List<IMesh> { };
        }

        public override IEnumerable<IMesh> Meshes
        {
            get { return _meshes; }
        }

        public void SetMeshes(IEnumerable<IMesh> meshes)
        {
            _meshes = meshes;
        }
    }
}