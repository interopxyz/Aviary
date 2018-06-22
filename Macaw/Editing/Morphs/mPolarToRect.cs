using Accord.Imaging.Filters;
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
        int Width = 0;
        int Height = 0;

        public mPolarToRect()
        {
        }

        public mPolarToRect(double offset, double depth, int width, int height)
        {

            BitmapType = mFilter.BitmapTypes.None;

            Offset = offset;
            Depth = depth;
            Width = width;
            Height = height;

            Effect = new TransformFromPolar();

            Effect.OffsetAngle = Offset;
            Effect.CirlceDepth = Depth;
            if ((Width < 1) & (Height < 1))
            {
                Effect.UseOriginalImageSize = true;
            }
            else
            {
                Effect.UseOriginalImageSize = true;
                Effect.NewSize = new Size(Width, Height);
            }

            filter = Effect;
        }

    }
}
