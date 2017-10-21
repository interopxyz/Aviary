using AForge.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Macaw.Filtering.Objects.Figures
{
    public class mFigureUnique : mFilter
    {
        ConnectedComponentsLabeling Effect = new ConnectedComponentsLabeling();

        wDomain Width = new wDomain(50, 1000);
        wDomain Height = new wDomain(50, 1000);

        public mFigureUnique()
        {

            BitmapType = 0;

            Effect = new ConnectedComponentsLabeling();


            Sequence.Clear();
            Sequence.Add(Effect);
        }

        public mFigureUnique(wDomain WidthRange, wDomain HeightRange)
        {

            Width = WidthRange;
            Height = HeightRange;

            BitmapType = 0;

            Effect = new ConnectedComponentsLabeling();
            Effect.MinWidth = (int)Width.T0;
            Effect.MaxWidth = (int)Height.T0;
            Effect.MinHeight = (int)Width.T1;
            Effect.MaxHeight = (int)Height.T1;
            
            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}