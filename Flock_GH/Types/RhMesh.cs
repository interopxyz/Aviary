using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Geometry.Meshes;
using Rhino.Geometry;

namespace Flock_GH.Types
{
    public class RhMesh
    {
        public wMesh WindMesh = new wMesh();
        public Mesh RhinoMesh = new Mesh();

        public RhMesh()
        {

        }

    }
}
