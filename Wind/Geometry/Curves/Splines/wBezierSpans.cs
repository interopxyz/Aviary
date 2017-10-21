using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wind.Geometry.Curves.Splines
{
    public class wBezierSpans
    {
        public int A = 0;
        public int B = 1;
        public int C = 2;
        public int D = 3;

        public wBezierSpans()
        {
        }

        public wBezierSpans(int IndexA, int IndexB, int IndexC, int IndexD)
        {
            A = IndexA;
            B = IndexB;
            C = IndexC;
            D = IndexD;
        }
    }
}
