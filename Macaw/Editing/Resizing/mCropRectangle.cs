using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Resizing
{
    public class mCropRectangle : mFilters
    {

        Crop Effect = null;

        public mCropRectangle(int X, int Y, int W, int H, System.Drawing.Color FillColor)
        {
            Sequence.Clear();

            BitmapType = mFilter.BitmapTypes.Rgb24bpp;

            Effect = new Crop(new System.Drawing.Rectangle(X, Y, W, H));

            Sequence.Clear();
            Sequence.Add(Effect);

        }

    }
}