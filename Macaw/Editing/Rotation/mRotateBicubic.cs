using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Rotation
{
    public class mRotateBicubic : mFilter
    {

        RotateBicubic Effect = null;
        

        public mRotateBicubic(double Angle, bool Fit, System.Drawing.Color CornerColor)
        {

            BitmapType = 2;


            Effect = new RotateBicubic(Angle, Fit);
            Effect.FillColor = CornerColor;

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}