using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Resizing
{
    public class mShrinkToColor : mFilters
    {
        Shrink Effect = new Shrink();

        System.Drawing.Color FilterColor = System.Drawing.Color.Black;
        

        public mShrinkToColor(System.Drawing.Color SelectedColor)
        {

            BitmapType = mFilter.BitmapTypes.Rgb24bpp;

            FilterColor = SelectedColor;

            Effect = new Shrink(FilterColor);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}