using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Geometry.Vectors;

namespace Wind.Containers
{
    public class wSize
    {
        public double Width = 1;
        public double Height = 1;

        public double Left = 0;
        public double Right = 1;
        public double Top = 1;
        public double Bottom = 0;

        public wPoint BottomLeft = new wPoint(0,0,0);
        public wPoint BottomRight = new wPoint(0, 1, 0);
        public wPoint TopLeft = new wPoint(1, 1, 0);
        public wPoint TopRight = new wPoint(1, 0, 0);
        public wPoint Center = new wPoint(0.5, 0.5, 0);

        public wSize()
        {
        }

        public wSize(double WidthSize, double HeightSize)
        {
            Width = HeightSize;
            Height = HeightSize;
        }

    }
}
