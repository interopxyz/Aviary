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
            using (var fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Open))
            {
                BitmapObject = new Bitmap(fs);
                Width = BitmapObject.Width;
                Height = BitmapObject.Height;
            }
        }
    }
}