using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wind.Types
{
    public class wImage
    {
        public Bitmap BitmapImage = null;

        public enum FillSpace { Local, Global }
        public FillSpace FillMode = FillSpace.Local;

        public enum AlignMode { TopLeft, TopMiddle, TopRight, CenterLeft, CenterMiddle, CenterRight, BottomLeft, BottomMiddle, BottomRight}
        public AlignMode Alignment = AlignMode.CenterMiddle;

        public enum FitMode { Fit, Fill, Stretch }
        public FitMode Fitting = FitMode.Fill;

        public byte[] ImageByteArray = null;

        public double Angle = 0;

        public bool IsEmbedded = false;

        public wImage()
        {

        }

        public wImage(Bitmap InitialBitmap)
        {
            BitmapImage = InitialBitmap;

            ToByteArray();
        }

        public wImage(Bitmap InitialBitmap, FillSpace Extents)
        {
            BitmapImage = InitialBitmap;
            FillMode = Extents;

            ToByteArray();
        }

        public wImage(Bitmap InitialBitmap, FillSpace Extents, bool Embed)
        {
            BitmapImage = InitialBitmap;
            FillMode = Extents;
            IsEmbedded = Embed;

            ToByteArray();
        }

        public wImage(Bitmap InitialBitmap, FillSpace Extents, bool Embed, AlignMode ImageAlignment, FitMode ImageFitting)
        {
            BitmapImage = InitialBitmap;
            FillMode = Extents;
            IsEmbedded = Embed;

            Alignment = ImageAlignment;
            Fitting = ImageFitting;

            ToByteArray();
        }

        public wImage(Bitmap InitialBitmap, FillSpace Extents, bool Embed, AlignMode ImageAlignment, FitMode ImageFitting, double RotationAngle)
        {
            BitmapImage = InitialBitmap;
            FillMode = Extents;
            IsEmbedded = Embed;

            Alignment = ImageAlignment;
            Fitting = ImageFitting;

            Angle = RotationAngle;

            ToByteArray();
        }

        public void ToByteArray()
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            BitmapImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            ImageByteArray = stream.ToArray();
            stream.Close();
        }
    }
}
