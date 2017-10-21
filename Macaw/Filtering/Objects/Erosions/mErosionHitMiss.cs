using AForge.Imaging.Filters;


namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionHitMiss : mFilter
    {
        HitAndMiss Effect = null;

        short[,] PatternHM = new short[,] { { -1, -1, -1 }, { 1, 1, 0 }, { -1, -1, -1 } };

        public mErosionHitMiss(short[,] Pattern, int Mode)
        {

            BitmapType = 0;

            PatternHM = Pattern;

            Effect = new HitAndMiss(PatternHM, (HitAndMiss.Modes)Mode);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}