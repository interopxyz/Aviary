using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing.Imaging;

using Xceed.Wpf.Toolkit;

using Wind.Containers;
using Parrot.Controls;

namespace Parrot.Displays
{
    public class pScrollView : pControl
    {
        public ScrollViewer Element;
        public System.Windows.Controls.Image ImageObject = new System.Windows.Controls.Image();
        public string Type;

        public pScrollView(string InstanceName)
        {
            Element = new ScrollViewer();
            Element.Name = InstanceName;
            Type = "ScrollViewer";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0));
            Element.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0));
            Element.BorderThickness = new Thickness(0);
        }

        public void SetProperties(Bitmap image)
        {
            ImageSource BmpImage = BitmapToImageSource(image);

            ImageObject.Source = BmpImage;

            Element.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            Element.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            ImageObject.Stretch = Stretch.None;

            Element.Content = ImageObject;
        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        public void SetCorners(wGraphic Graphic)
        {
        }

        public void SetFont(wGraphic Graphic)
        {
        }
    }
}
