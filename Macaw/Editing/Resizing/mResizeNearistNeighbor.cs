using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Resizing
{
    public class mResizeNearistNeighbor : mFilters
    {

        ResizeNearestNeighbor Effect = null;

        int Width = 800;
        int Height = 600;

        public mResizeNearistNeighbor(int ImageWidth, int ImageHeight)
        {

            BitmapType = mFilter.BitmapTypes.None;

            Width = ImageWidth;
            Height = ImageHeight;

            Effect = new ResizeNearestNeighbor(Width, Height);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}