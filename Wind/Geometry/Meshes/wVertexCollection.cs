using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wind.Geometry.Meshes
{

    public class wVertexCollection
    {
        List<wVertex> Vertices = new List<wVertex>();

        public wVertexCollection()
        {

        }

        public wVertexCollection(List<wVertex> InputVertices)
        {
            Vertices = InputVertices;
        }
    }
}
