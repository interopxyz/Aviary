using System.Collections.Generic;
using System.Drawing;

namespace Macaw.Utilities.Channels
{
    public class mGetAlpha: mGetChannel
    {

        public mGetAlpha(Bitmap BaseBitmap)
        {
            Bitmap bmp = new Bitmap(BaseBitmap);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Values.Add(bmp.GetPixel(i, j).A);
                }
            }
        }

        public mGetAlpha(Bitmap BaseBitmap, List<double> X, List<double> Y, bool isUnitized)
        {
            IsUnitized = isUnitized;
            double divisor = 1;
            if (IsUnitized) { divisor = 255; }

            Bitmap bmp = new Bitmap(BaseBitmap);
            for (int i = 0; i < X.Count; i++)
            {
                Values.Add(bmp.GetPixel((int)((bmp.Width - 1) * X[i]), (int)((bmp.Height - 1) * Y[i])).A/divisor);
            }
        }


    }
}