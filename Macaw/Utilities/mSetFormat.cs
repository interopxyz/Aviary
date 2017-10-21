using AForge.Imaging.Filters;
using System.Drawing;

namespace Macaw.Utilities
{
    public class mSetFormat
    {
        public Bitmap ModifiedBitmap = null;

        public mSetFormat(Bitmap SourceBitmap, int Mode)
        {
            ModifiedBitmap = new Bitmap(SourceBitmap);

            switch (Mode)
            {
                case 0:
                    ModifiedBitmap = Grayscale.CommonAlgorithms.BT709.Apply(ModifiedBitmap);
                    break;
                case 2:
                    ModifiedBitmap = AForge.Imaging.Image.Clone(ModifiedBitmap, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    break;
                case 3:
                    ModifiedBitmap = AForge.Imaging.Image.Clone(ModifiedBitmap, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);
                    break;
            }
        }


    }
}
