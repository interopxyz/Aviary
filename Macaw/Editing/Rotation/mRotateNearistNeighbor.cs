using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Rotation
{
    public class mRotateNearistNeighbor : mFilter
    {

        RotateNearestNeighbor Effect = null;

        public mRotateNearistNeighbor(double Angle, bool Fit,System.Drawing.Color CornerColor)
        {

            BitmapType = mFilter.BitmapTypes.Rgb24bpp;
            
            Effect = new RotateNearestNeighbor(Angle,Fit);
            Effect.FillColor = CornerColor;

            filter = Effect;
        }

    }
}