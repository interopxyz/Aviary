using AForge;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments
{
    public class mFillMean : mFilter
    {

        PointedMeanFloodFill Effect = new PointedMeanFloodFill();

        Color Tolerance = Color.Red;

        IntPoint CoordPoint = new IntPoint(1, 1);

        public mFillMean(Color ToleranceColor, int X, int Y)
        {

            Tolerance = ToleranceColor;
            CoordPoint = new IntPoint(X, Y);

            BitmapType = 2;

            Effect = new PointedMeanFloodFill();

            Effect.Tolerance = Tolerance;
            Effect.StartingPoint = CoordPoint;

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}