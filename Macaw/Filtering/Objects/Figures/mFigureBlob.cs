using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Figures
{
    public class mFigureBlob : mFilters
    {
        ExtractBiggestBlob Effect = new ExtractBiggestBlob();
        
        public mFigureBlob()
        {

            BitmapType = 0;

            Effect = new ExtractBiggestBlob();
            
            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}