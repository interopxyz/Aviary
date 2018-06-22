using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Accord.Imaging;
using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Filtering.Stylized
{
    public class mBlurBox : mFilter
    {

        byte HorizontalKernal = 3;
        byte VerticalKernal = 3;

        public mBlurBox(byte horizontalKernal, byte verticalKernal)
        {

            BitmapType = mFilter.BitmapTypes.None;

            HorizontalKernal = horizontalKernal;
            VerticalKernal = verticalKernal;

            filter = new FastBoxBlur(HorizontalKernal, VerticalKernal);
        }

    }
}
