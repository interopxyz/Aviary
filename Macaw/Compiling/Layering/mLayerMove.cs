using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Compiling.Layering
{
    public class mLayerMove: mLayer
    {
        public mLayerMove(int X, int Y)
        {
            CompositionLayer.X += X;
            CompositionLayer.Y += Y;
        }
    }
}
