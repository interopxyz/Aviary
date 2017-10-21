using AForge.Imaging.Filters;


namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionHatBottom : mFilter
    {
        BottomHat Effect = new BottomHat();

        public mErosionHatBottom()
        {

            BitmapType = 0;

            Effect = new BottomHat();
            
            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}