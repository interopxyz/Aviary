using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Rotation
{
    public class mRotateNearistNeighbor : mFilter
    {

        RotateNearestNeighbor Effect = null;

        public mRotateNearistNeighbor(double Angle, bool Fit,System.Drawing.Color CornerColor)
        {

            BitmapType = 2;
            

            Effect = new RotateNearestNeighbor(Angle,Fit);
            Effect.FillColor = CornerColor;

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}