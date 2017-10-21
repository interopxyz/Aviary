using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Resizing
{
    public class mResizeNearistNeighbor : mFilter
    {

        ResizeNearestNeighbor Effect = null;

        int Width = 800;
        int Height = 600;

        public mResizeNearistNeighbor(int ImageWidth, int ImageHeight)
        {

            BitmapType = 1;

            Width = ImageWidth;
            Height = ImageHeight;

            Effect = new ResizeNearestNeighbor(Width, Height);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}