using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Figures
{
    public class mFigureFilter : mFilter
    {

        BlobsFiltering Effect = new BlobsFiltering();

        wDomain Width = new wDomain(50, 1000);
        wDomain Height = new wDomain(50, 1000);

        public mFigureFilter()
        {

            BitmapType = 0;

            Effect = new BlobsFiltering();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

        public mFigureFilter(wDomain WidthRange, wDomain HeightRange)
        {

            Width = WidthRange;
            Height = HeightRange;

            BitmapType = 0;

            Effect = new BlobsFiltering((int)Width.T0, (int)Height.T0, (int)Width.T1,(int)Height.T1);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}