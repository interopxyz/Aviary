using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Resizing
{
    public class mResizeBicubic : mFilters
    {

        ResizeBicubic Effect = null;
        
        int Width = 800;
        int Height = 600;

        public mResizeBicubic(int ImageWidth, int ImageHeight)
        {

            BitmapType = mFilter.BitmapTypes.Rgb24bpp;

            Width = ImageWidth;
            Height = ImageHeight;
            
            Effect = new ResizeBicubic(Width, Height);

            Sequence.Clear();
            Sequence.Add(Effect);
        }


    }
}