using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Rotation
{
    public class mRotateBicubic : mFilter
    {

        RotateBicubic Effect = null;
        

        public mRotateBicubic(double Angle, bool Fit, System.Drawing.Color CornerColor)
        {
            BitmapType = mFilter.BitmapTypes.Rgb24bpp;
            
            Effect = new RotateBicubic(Angle, Fit);
            Effect.FillColor = CornerColor;

            filter = Effect;
        }

    }
}