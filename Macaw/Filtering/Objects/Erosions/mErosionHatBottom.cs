using Accord.Imaging.Filters;


namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionHatBottom : mFilter
    {
        BottomHat Effect = new BottomHat();

        public mErosionHatBottom()
        {

            BitmapType = BitmapTypes.Rgb24bpp;

            Effect = new BottomHat();

            filter = Effect;
        }

    }
}