using System.Collections.Generic;
using System.Drawing;

using Accord;
using Accord.Imaging.Filters;
using Accord.Imaging;

using Pot = CsPotrace;

using Macaw.Filtering;

using Wind.Geometry.Vectors;

namespace Macaw.Analysis
{
    public class mAnalyzePotrace : mFilters
    {
        
        public List<List<wPoint[]>> VectorizedPointArray = new List<List<wPoint[]>>();

        public mAnalyzePotrace(Bitmap InitialBitmap, double Threshold, double Alpha, double OptimizationTolerance, int ObjectSize, bool Optimize, int Mode)
        {
            // Potrace Vectorizatiton
            var crvs = new List<List<Pot.Curve>>();
            var output = new List<List<wPoint[]>>();

            int H = InitialBitmap.Height;

            VectorizedPointArray.Clear();
            crvs.Clear();
            output.Clear();

            Pot.Potrace.Clear();

            Pot.Potrace.turnpolicy = (Pot.TurnPolicy)Mode;

            Pot.Potrace.curveoptimizing = Optimize;

            Pot.Potrace.opttolerance = OptimizationTolerance;
            Pot.Potrace.Treshold = Threshold;
            Pot.Potrace.alphamax = Alpha;

            Pot.Potrace.turdsize = ObjectSize;

            Pot.Potrace.Potrace_Trace(InitialBitmap, crvs);

            foreach (var crvList in crvs)
            {
                var list = new List<wPoint[]>();
                foreach(var crv in crvList)
                {
                    var ptArr = new wPoint[] { new wPoint(crv.A.x, H-crv.A.y), new wPoint(crv.B.x, H-crv.B.y) };
                    list.Add(ptArr);
                }
                VectorizedPointArray.Add(list);
            }


        }

    }
}