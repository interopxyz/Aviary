using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Rotation
{
    public class mRotateBilinear : mFilter
    {

        RotateBilinear Effect = null;

        public mRotateBilinear(double Angle, bool Fit, System.Drawing.Color CornerColor)
        {
            BitmapType = mFilter.BitmapTypes.Rgb24bpp;
            
            Effect = new RotateBilinear(Angle, Fit);
            Effect.FillColor = CornerColor;

            filter = Effect;
        }

    }
}