using Accord;
using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments
{
    public class mFillColor : mFilter
    {

        PointedColorFloodFill Effect = new PointedColorFloodFill();

        Color Tolerance = Color.Red;
        Color Fill = Color.Black;

        IntPoint CoordPoint = new IntPoint(1, 1);

        public mFillColor(Color ToleranceColor, Color FillColor, int X, int Y)
        {

            Tolerance = ToleranceColor;
            Fill = FillColor;
            CoordPoint = new IntPoint(X, Y);

            BitmapType = mFilter.BitmapTypes.Rgb24bpp;

            Effect = new PointedColorFloodFill();

            Effect.Tolerance = Tolerance;
            Effect.FillColor = Fill;
            Effect.StartingPoint = CoordPoint;

            filter = Effect;
        }

    }
}
