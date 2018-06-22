using Accord.Imaging;
using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Figures
{
    public class mFigureCorners : mFilters
    {
        CornersMarker Effect = null;

        System.Drawing.Color MarkerColor = System.Drawing.Color.Red;

        public mFigureCorners(System.Drawing.Color MarkColor)
        {
            MarkerColor = MarkColor;

            BitmapType = 0;

            SusanCornersDetector Detector = new SusanCornersDetector();
            Effect = new CornersMarker(Detector,MarkerColor);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}