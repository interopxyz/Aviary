using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Rotation
{
    public class mMirror : mFilter
    {

        Mirror Effect = null;


        public mMirror( bool Horizontal, bool Vertical)
        {

            BitmapType = mFilter.BitmapTypes.Rgb24bpp;
            
            Effect = new Mirror(Horizontal,Vertical);

            filter = Effect;
        }

    }
}
