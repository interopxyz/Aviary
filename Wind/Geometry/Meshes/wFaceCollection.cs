using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wind.Geometry.Meshes
{
    public class wFaceCollection
    {
        List<wFace> Vertices = new List<wFace>();

        public wFaceCollection()
        {

        }

        public wFaceCollection(List<wFace> InputVertices)
        {
            Vertices = InputVertices;
        }

    }
}
