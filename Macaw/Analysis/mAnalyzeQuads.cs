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
    public class mAnalyzeQuads: mFilter
    {

        CornersMarker Effect = null;
        public List<wPoint> Points = new List<wPoint>();

        public mAnalyzeQuads(Bitmap InitialBitmap)
        {
            

            BitmapType = 0;

            QuadrilateralFinder cMethod = new QuadrilateralFinder();

            //Effect = new CornersMarker(cMethod, CornerColor);

            //Effect.Detector = cMethod;


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