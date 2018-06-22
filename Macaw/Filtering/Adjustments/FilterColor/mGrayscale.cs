using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Accord.Imaging;
using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.FilterColor
{
    public class mGrayscale : mFilter
    {
        Grayscale Effect = null;

        public double RedCoef = 0.2125;
        public double GreenCoef = 0.7154;
        public double BlueCoef = 0.0721;

        public enum GrayscaleModes { Custom, BT709, RMY, Y };
        public GrayscaleModes Mode = GrayscaleModes.BT709;


        public mGrayscale(double redCoef, double greenCoef, double blueCoef, GrayscaleModes mode)
        {

            RedCoef = redCoef;
            GreenCoef = greenCoef;
            BlueCoef = blueCoef;
            Mode = mode;

            BitmapType = mFilter.BitmapTypes.None;

            switch (Mode)
            {
                default:
                    Effect = new Grayscale(RedCoef,GreenCoef,BlueCoef);
                    break;
                case GrayscaleModes.BT709:
                    Effect = Grayscale.CommonAlgorithms.BT709;
                    break;
                case GrayscaleModes.RMY:
                    Effect = Grayscale.CommonAlgorithms.RMY;
                    break;
                case GrayscaleModes.Y:
                    Effect = Grayscale.CommonAlgorithms.Y;
                    break;
            }
            
            filter = Effect;
        }
    }
}
