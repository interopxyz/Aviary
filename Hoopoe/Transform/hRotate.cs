using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Geometry.Vectors;

namespace Hoopoe.Transform
{
    public class hRotate
    {
        public string Transformation = "rotate(0 0 0)";

        public hRotate()
        {

        }

        public hRotate(double Angle)
        {
            Transformation = "rotate(" + Angle + " 0 0)";
        }

        public hRotate(wPoint Center, double Angle)
        {
            Transformation = "rotate(" + Angle + " " + Center.X + " " + Center.Y + ") ";
        }
    }

}
