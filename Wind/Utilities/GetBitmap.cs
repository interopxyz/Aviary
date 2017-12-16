using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

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

            PropertyItem[] attribute = BitmapObject.PropertyItems;

            attribute[0].Id = 0;
            attribute[0].Value = Encoding.ASCII.GetBytes(FilePath);

            BitmapObject.SetPropertyItem(attribute[0]);

            Width = BitmapObject.Width;
            Height = BitmapObject.Height;
        }
    }
}