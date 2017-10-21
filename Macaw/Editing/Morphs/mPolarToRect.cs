using AForge.Imaging.Filters;
using Macaw.Filtering;
using System.Drawing;
using Wind.Types;

namespace Macaw.Editing.Morphs
{
    public class mPolarToRect : mFilter
    {

        TransformFromPolar Effect = new TransformFromPolar();
        

        double Offset = 0;
        double Depth = 1;
        wDomain ImageSize = new wDomain(0,0);

        public mPolarToRect()
        {
        }

        public mPolarToRect(double OffsetValue, double DepthValue, wDomain ImgSize)
        {

            BitmapType = 1;

            Offset = OffsetValue;
            Depth = DepthValue;
            ImageSize = ImgSize;

            Effect = new TransformFromPolar();

            Effect.OffsetAngle = Offset;
            Effect.CirlceDepth = Depth;
            if((ImageSize.T0==0)& (ImageSize.T1 == 0))
            {
                Effect.UseOriginalImageSize = true;
            }
            else
            {
                Effect.UseOriginalImageSize = true;
                Effect.NewSize = new Size((int)ImageSize.T0, (int)ImageSize.T1);
            }

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
