using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.SVG.Geometry
{
    public abstract class hCurve
    {
        public StringBuilder Curve = new StringBuilder();

        public hCurve()
        {

        }

        public virtual void BuildSVGCurve()
        {

        }

    }
}
