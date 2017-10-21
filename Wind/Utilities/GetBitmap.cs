using System.Drawing;

namespace Wind.Utilities
{
    public class GetBitmap
    {

        public string Tag, Name;
        public int Width, Height;
        public Bitmap BitmapObject;

        public GetBitmap(string FilePath)
        {
            BitmapObject = (Bitmap)Bitmap.FromFile(FilePath);

            Width = BitmapObject.Width;
            Height = BitmapObject.Height;
        }
    }
}