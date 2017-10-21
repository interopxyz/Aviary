using AForge.Imaging.Filters;


namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionHatTop : mFilter
    {
        TopHat Effect = new TopHat();

        public mErosionHatTop()
        {

            BitmapType = 0;

            Effect = new TopHat();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}