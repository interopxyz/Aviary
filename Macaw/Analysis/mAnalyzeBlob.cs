using AForge.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Macaw.Analysis
{
    public class mAnalyzeBlobs : mFilter
    {
        ConnectedComponentsLabeling Effect = new ConnectedComponentsLabeling();

        wDomain Width = new wDomain(50, 1000);
        wDomain Height = new wDomain(50, 1000);

        public mAnalyzeBlobs()
        {

            BitmapType = 0;

            Effect = new ConnectedComponentsLabeling();


            Sequence.Clear();
            Sequence.Add(Effect);
        }

        public mAnalyzeBlobs(Bitmap BaseBitmap,wDomain WidthRange, wDomain HeightRange)
        {

            Width = WidthRange;
            Height = HeightRange;

            BitmapType = 0;

            Effect = new ConnectedComponentsLabeling();
            Effect.MinWidth = (int)Width.T0;
            Effect.MaxWidth = (int)Width.T1;
            Effect.MinHeight = (int)Height.T0;
            Effect.MaxHeight = (int)Height.T1;
            
            Effect.Apply(BaseBitmap);
            Effect.BlobCounter.ProcessImage(BaseBitmap);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

        public Rectangle[] ExtractBoundaries()
        {
            Rectangle[] Boundaries = Effect.BlobCounter.GetObjectsRectangles();

            return Boundaries;
        }

    }
}