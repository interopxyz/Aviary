using AForge.Imaging.Filters;

namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionSimple : mFilter
    {
        Erosion Effect = new Erosion();

        public mErosionSimple()
        {

            BitmapType = 0;

            Effect = new Erosion();
            
            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}