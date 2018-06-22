using Accord;
using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;
using Accord.Imaging;
using Wind.Geometry.Vectors;

namespace Macaw.Analysis
{
    public class mAnalyzeCornersMoravec : mFilter
    {

        CornersMarker Effect = null;
        public List<wPoint> Points = new List<wPoint>();

        public mAnalyzeCornersMoravec(Bitmap InitialBitmap, Color CornerColor, int DiffThreshold)
        {
            

            BitmapType = mFilter.BitmapTypes.None;

            MoravecCornersDetector cMethod = new MoravecCornersDetector();

            cMethod.Threshold = DiffThreshold;

            Effect = new CornersMarker(cMethod, CornerColor);

            Effect.Detector = cMethod;


            List<IntPoint> corners = cMethod.ProcessImage(InitialBitmap);

            foreach (IntPoint corner in corners)
            {
                Points.Add(new wPoint(corner.X, InitialBitmap.Height - corner.Y));
            }

            filter = Effect;
        }

    }
}