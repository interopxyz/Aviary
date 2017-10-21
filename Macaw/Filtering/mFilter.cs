using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using AForge.Imaging.Filters;

namespace Macaw.Filtering
{
    public class mFilter
    {

        public string Type = "Filter";

        public int BitmapType = 0;

        public FiltersSequence Sequence = new FiltersSequence();

        public mFilter()
        {

        }

    }
}
