using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Resizing
{
    public class mShrinkToColor : mFilter
    {
        Shrink Effect = new Shrink();

        System.Drawing.Color FilterColor = System.Drawing.Color.Black;
        

        public mShrinkToColor(System.Drawing.Color SelectedColor)
        {

            BitmapType = 2;

            FilterColor = SelectedColor;

            Effect = new Shrink(FilterColor);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}