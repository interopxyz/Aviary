using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

using System.Windows;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing.Imaging;

using Wind.Containers;
using Parrot.Controls;

namespace Parrot.Displays
{
    public class pFrame : pControl
    {
        public System.Windows.Controls.Image Element;
        public string Type;

        public pFrame(string InstanceName)
        {
            Element = new System.Windows.Controls.Image();
            Element.Name = InstanceName;
            Type = "PictureFrame";

        }

        public void SetProperties(Bitmap image, int Sizing)
        {
            ImageSource BmpImage = BitmapToImageSource(image);

            Element.Source = BmpImage;
            SetSizing(Sizing);

            Element.HorizontalAlignment = HorizontalAlignment.Center;
            Element.VerticalAlignment = VerticalAlignment.Center;
        }

        public void SetSizing(int Sizing)
        {
            switch (Sizing)
            {
                case (1):
                    Element.Stretch = Stretch.UniformToFill;
                    break;
                case (2):
                    Element.Stretch = Stretch.Fill;
                    break;
                case (3):
                    Element.Stretch = Stretch.None;
                    break;
                case (4):
                    Element.Stretch = Stretch.None;
                    Element.HorizontalAlignment = HorizontalAlignment.Left;
                    Element.VerticalAlignment = VerticalAlignment.Top;
                    break;
                default:
                    Element.Stretch = Stretch.Uniform;
                    break;
            }
        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        public override void SetSolidFill()
        {
            //Element.Background = new SolidColorBrush(Graphics.Background.ToMediaColor());
        }

        public override void SetStroke()
        {
        }

        public override void SetSize()
        {
            if (Graphics.Width < 1) { Element.Width = double.NaN; } else { Element.Width = Graphics.Width; }
            if (Graphics.Height < 1) { Element.Height = double.NaN; } else { Element.Height = Graphics.Height; }
        }

        public override void SetMargin()
        {
            Element.Margin = new Thickness(Graphics.Margin[0], Graphics.Margin[1], Graphics.Margin[2], Graphics.Margin[3]);
        }

        public override void SetPadding()
        {
        }

        public override void SetFont()
        {
        }
    }
}
