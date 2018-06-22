using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Macaw.Utilities
{
    public class mConvert
    {
        Bitmap ImageBitmap = null;
        BitmapSource ImageBitmapSource = null;
        BitmapImage ImageBitmapImage = null;

        public mConvert(Bitmap BitmapImg)
        {
            ImageBitmap = (Bitmap)BitmapImg.Clone();
        }

        public mConvert(BitmapSource BitmapSourceImg)
        {
            ImageBitmapSource = BitmapSourceImg;
        }

        public mConvert(BitmapImage BitmapImageImg)
        {
            ImageBitmapImage = BitmapImageImg;
        }

        public Bitmap SourceToBitmap()
        {
            Bitmap bitmap= new Bitmap(10,10);
            using (MemoryStream outStream = new MemoryStream())
            {
                PngBitmapEncoder enc = new PngBitmapEncoder();

                enc.Frames.Add(BitmapFrame.Create(ImageBitmapSource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);

            } 
            return bitmap;
        }

        public BitmapSource BitmapToSource()
        {
            var bitmapData = ImageBitmap.LockBits(
                new Rectangle(0, 0, ImageBitmap.Width, ImageBitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, ImageBitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height, 96, 96, PixelFormats.Bgr24, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            ImageBitmap.UnlockBits(bitmapData);
            return bitmapSource;
        }

        public Bitmap ImageToBitmap()
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                PngBitmapEncoder enc = new PngBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(ImageBitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public BitmapImage BitmapToImage()
        {
            IntPtr hBitmap = ImageBitmap.GetHbitmap();
            BitmapImage retval;

            try
            {
                retval = (BitmapImage)Imaging.CreateBitmapSourceFromHBitmap(
                             hBitmap,
                             IntPtr.Zero,
                             Int32Rect.Empty,
                             BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(hBitmap);
            }

            return retval;
        }

        public WriteableBitmap BitmapToWritableBitmap()
        {
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(ImageBitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            return new WriteableBitmap(bitmapSource);
        }

    }
}
