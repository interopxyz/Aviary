using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Rotation
{
    public class mRotateBilinear : mFilter
    {

        RotateBilinear Effect = null;

        public mRotateBilinear(double Angle, bool Fit, System.Drawing.Color CornerColor)
        {

            BitmapType = 2;


            Effect = new RotateBilinear(Angle, Fit);
            Effect.FillColor = CornerColor;

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}