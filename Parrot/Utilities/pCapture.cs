using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Wind;
using Parrot.Containers;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;

namespace Parrot.Utilities
{
    public class pCapture
    {
        public Bitmap BitmapObject;

        public pCapture(Border Container, int DPI)
        {
            BitmapObject = GetImage(Container, DPI);
        }

        public static System.Drawing.Bitmap GetImage(Border view, int DPI)
        {
            double W = view.ActualWidth;
            double H = view.ActualHeight;
            if (W == 0) { W = 800; }
            if (H == 0) { H = 600; }

            System.Windows.Size size = new System.Windows.Size(W, H);

            RenderTargetBitmap result = new RenderTargetBitmap((int)(size.Width / 96 * DPI), (int)(size.Height / 96 * DPI), DPI, DPI, PixelFormats.Pbgra32);

            DrawingVisual drawingvisual = new DrawingVisual();
            using (DrawingContext context = drawingvisual.RenderOpen())
            {
                context.DrawRectangle(new VisualBrush(view), null, new Rect(new System.Windows.Point(), size));
                context.Close();
            }

            result.Render(drawingvisual);

            MemoryStream stream = new MemoryStream();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(result));
            encoder.Save(stream);

            Bitmap bitmap = new Bitmap(stream);

            return bitmap;
        }

    }
}
