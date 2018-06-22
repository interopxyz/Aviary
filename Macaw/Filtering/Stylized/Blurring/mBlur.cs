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
    public class mBlur : mFilter
    {
        
        public mBlur()
        {

            BitmapType = mFilter.BitmapTypes.None;
            filter = new Blur();
        }

    }
}
