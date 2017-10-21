using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Geometry.Vectors;

namespace Wind.Geometry.Meshes
{
    public class wNormalCollection
    {
        public List<wVector> Normals = new List<wVector>();

        public wNormalCollection()
        {

        }

        public wNormalCollection(List<wVector> NormalList)
        {
            Normals = NormalList;
        }

    }
}
