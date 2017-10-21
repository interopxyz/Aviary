using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Resizing
{
    public class mResizeBilinear : mFilter
    {

        ResizeBilinear Effect = null;
        

        int Width = 800;
        int Height = 600;

        public mResizeBilinear(int ImageWidth, int ImageHeight)
        {

            BitmapType = 1;

            Width = ImageWidth;
            Height = ImageHeight;

            Effect = new ResizeBilinear(Width, Height);
            

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}