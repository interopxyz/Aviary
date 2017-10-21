using Macaw.Build;
using Macaw.Filtering;
using System.Drawing;

namespace Macaw.Utilities
{
    public class mMatchBitmaps
    {

        public Bitmap BottomImage = null;
        public Bitmap TopImage = null;

         Bitmap BitmapA = null;
         Bitmap BitmapB = null;

        public mMatchBitmaps(Bitmap ImageA, Bitmap ImageB, int MatchMode, int ScaleMode)
        {
            if (MatchMode == 0)
            {
                BitmapA = new Bitmap(ImageA);
                BitmapB = new Bitmap(ImageB);
            }
            else
            {
                BitmapA = new Bitmap(ImageB);
                BitmapB = new Bitmap(ImageA);
            }
            

            mFilter Filter = new mFilter();
            switch (ScaleMode)
            {
                case 0: //Crop
                    Filter = new mFitCrop(BitmapA, BitmapB);
                    break;
                case 1: //Fit
                    Filter = new mFitScale(BitmapA, BitmapB);
                    break;
                case 2: //Stretch
                    Filter = new mFitStretch(BitmapA, BitmapB);
                    break;
            }



            if (MatchMode == 0)
            {
                BottomImage = new Bitmap(new mApply(BitmapA, Filter).ModifiedBitmap);
                TopImage = new Bitmap(BitmapB);
            }
            else
            {
                BottomImage = new Bitmap(BitmapB);
                TopImage = new Bitmap(new mApply(BitmapA, Filter).ModifiedBitmap);
            }


        }

    }
}
