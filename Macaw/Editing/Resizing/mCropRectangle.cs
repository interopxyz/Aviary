using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Resizing
{
    class mCropRectangle : mFilter
    {

        Crop Effect = null;

        public mCropRectangle(int X, int Y, int W, int H, System.Drawing.Color FillColor)
        {
            Sequence.Clear();

            BitmapType = 2;

            Effect = new Crop(new System.Drawing.Rectangle(X, Y, W, H));

            Sequence.Clear();
            Sequence.Add(Effect);

        }

    }
}