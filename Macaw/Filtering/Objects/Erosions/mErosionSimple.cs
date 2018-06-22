using Accord.Imaging.Filters;

namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionSimple : mFilter
    {
        Erosion Effect = new Erosion();

        public mErosionSimple()
        {

            BitmapType = mFilter.BitmapTypes.GrayscaleBT709;

            Effect = new Erosion();

            filter = Effect;
        }

    }
}