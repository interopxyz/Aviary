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
using Pot = CsPotrace;

namespace Macaw.Analysis
{
    public class mAnalyzeCornersSusan : mFilter
    {

        CornersMarker Effect = null;
        public List<wPoint> Points = new List<wPoint>();
        public List<List<wPoint[]>> VectorizedPointArray = new List<List<wPoint[]>>();

        public mAnalyzeCornersSusan(Bitmap InitialBitmap, Color CornerColor, int DiffThreshold, int GeoThreshold)
        {
            // Potrace Vectorizatiton
            var crvs = new List<List<Pot.Curve>>();
            var output = new List<List<wPoint[]>>();

            VectorizedPointArray.Clear();
            crvs.Clear();
            output.Clear();

            Pot.Potrace.Clear();
            Pot.Potrace.Potrace_Trace(InitialBitmap, crvs);

            foreach (var crvList in crvs)
            {
                var list = new List<wPoint[]>();
                foreach(var crv in crvList)
                {
                    var ptArr = new wPoint[] { new wPoint(crv.A.x, crv.A.y), new wPoint(crv.B.x, crv.B.y) };
                    list.Add(ptArr);
                }
                VectorizedPointArray.Add(list);
            }

            BitmapType = 0;

            SusanCornersDetector cMethod = new SusanCornersDetector();

            cMethod.DifferenceThreshold = DiffThreshold;
            cMethod.GeometricalThreshold = GeoThreshold;

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