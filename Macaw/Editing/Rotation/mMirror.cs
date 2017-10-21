using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Rotation
{
    public class mMirror : mFilter
    {

        Mirror Effect = null;


        public mMirror( bool Horizontal, bool Vertical)
        {

            BitmapType = 2;
            
            Effect = new Mirror(Horizontal,Vertical);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
