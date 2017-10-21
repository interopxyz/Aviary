using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments
{
    public class mNormalizeHistogram : mFilter
    {
        HistogramEqualization Effect = new HistogramEqualization();
        
        public mNormalizeHistogram()
        {
            

            BitmapType = 1;

            Effect = new HistogramEqualization();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}