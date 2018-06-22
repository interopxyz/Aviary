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
    public class mSmoothConservative : mFilter
    {

        public mSmoothConservative()
        {

            BitmapType = BitmapTypes.None;
            
            filter = new ConservativeSmoothing();
            
        }

    }
}
