using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Resizing
{
    public class mResizeBilinear : mFilters
    {

        ResizeBilinear Effect = null;
        

        int Width = 800;
        int Height = 600;

        public mResizeBilinear(int ImageWidth, int ImageHeight)
        {

            BitmapType = mFilter.BitmapTypes.None;

            Width = ImageWidth;
            Height = ImageHeight;

            Effect = new ResizeBilinear(Width, Height);
            

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}