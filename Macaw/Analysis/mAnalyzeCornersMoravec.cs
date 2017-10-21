using AForge;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;
using AForge.Imaging;
using Wind.Geometry.Vectors;

namespace Macaw.Analysis
{
    public class mAnalyzeCornersMoravec : mFilter
    {

        CornersMarker Effect = null;
        public List<wPoint> Points = new List<wPoint>();

        public mAnalyzeCornersMoravec(Bitmap InitialBitmap, Color CornerColor, int DiffThreshold)
        {
            

            BitmapType = 0;

            MoravecCornersDetector cMethod = new MoravecCornersDetector();

            cMethod.Threshold = DiffThreshold;

            Effect = new CornersMarker(cMethod, CornerColor);

            Effect.Detector = cMethod;


            List<IntPoint> corners = cMethod.ProcessImage(InitialBitmap);

            foreach (IntPoint corner in corners)
            {
                Points.Add(new wPoint(corner.X, corner.Y));
            }

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}