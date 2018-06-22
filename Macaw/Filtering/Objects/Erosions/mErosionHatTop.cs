using Accord.Imaging.Filters;


namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionHatTop : mFilter
    {
        TopHat Effect = new TopHat();

        public mErosionHatTop()
        {

            BitmapType = BitmapTypes.Rgb24bpp;

            Effect = new TopHat();
            
            filter = Effect;
        }

    }
}