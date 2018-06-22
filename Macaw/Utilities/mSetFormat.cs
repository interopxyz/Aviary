using Accord.Imaging.Filters;
using Macaw.Filtering;
using System.Drawing;

namespace Macaw.Utilities
{
    public class mSetFormat
    {
        public Bitmap ModifiedBitmap = null;

        public mSetFormat(Bitmap SourceBitmap, mFilter.BitmapTypes Mode)
        {
            ModifiedBitmap = (Bitmap)SourceBitmap.Clone();

            switch (Mode)
            {
                case mFilter.BitmapTypes.GrayscaleBT709:
                    ModifiedBitmap = Grayscale.CommonAlgorithms.BT709.Apply(ModifiedBitmap);
                    break;
                case mFilter.BitmapTypes.GrayscaleY:
                    ModifiedBitmap = Grayscale.CommonAlgorithms.Y.Apply(ModifiedBitmap);
                    break;
                case mFilter.BitmapTypes.GrayscaleRMY:
                    ModifiedBitmap = Grayscale.CommonAlgorithms.RMY.Apply(ModifiedBitmap);
                    break;
                case mFilter.BitmapTypes.GrayScale16bpp:
                    ModifiedBitmap = Accord.Imaging.Image.Clone(ModifiedBitmap, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);
                    break;
                case mFilter.BitmapTypes.Rgb16bpp:
                    ModifiedBitmap = Accord.Imaging.Image.Clone(ModifiedBitmap, System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
                    break;
                case mFilter.BitmapTypes.Rgb24bpp:
                    ModifiedBitmap = Accord.Imaging.Image.Clone(ModifiedBitmap, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    //ModifiedBitmap.PixelFormat = System.Drawing.Imaging.PixelFormat.
                    break;
            }
        }


    }
}
