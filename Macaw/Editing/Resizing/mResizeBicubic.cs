using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Resizing
{
    public class mResizeBicubic : mFilter
    {

        ResizeBicubic Effect = null;
        
        int Width = 800;
        int Height = 600;

        public mResizeBicubic(int ImageWidth, int ImageHeight)
        {

            BitmapType = 2;

            Width = ImageWidth;
            Height = ImageHeight;
            
            Effect = new ResizeBicubic(Width, Height);

            Sequence.Clear();
            Sequence.Add(Effect);
        }


    }
}