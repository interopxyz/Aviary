using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Accord.Imaging.Filters;

namespace Macaw.Filtering
{
    public class mFilters
    {

        public string Type = "Filters";

        public mFilter.BitmapTypes BitmapType = mFilter.BitmapTypes.None;

        public FiltersSequence Sequence = new FiltersSequence();

        public mFilters()
        {

        }

    }
}
