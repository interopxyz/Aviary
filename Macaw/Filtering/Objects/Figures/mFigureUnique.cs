using Accord.Imaging.Filters;
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

        bool Coupled = true;
        bool Blob = true;

        public mFigureUnique()
        {

            BitmapType = BitmapTypes.Rgb24bpp;

            Effect = new ConnectedComponentsLabeling();

            filter = Effect;
        }

        public mFigureUnique(wDomain WidthRange, wDomain HeightRange)
        {

            Width = WidthRange;
            Height = HeightRange;

            BitmapType = BitmapTypes.Rgb24bpp;

            Effect = new ConnectedComponentsLabeling();
            Effect.MinWidth = (int)Width.T0;
            Effect.MaxWidth = (int)Width.T1;
            Effect.MinHeight = (int)Height.T0;
            Effect.MaxHeight = (int)Height.T1;

            filter = Effect;
        }

        public mFigureUnique(wDomain WidthRange, wDomain HeightRange, bool coupled, bool blob)
        {

            Width = WidthRange;
            Height = HeightRange;
            Coupled = coupled;
            Blob = blob;

            BitmapType = BitmapTypes.Rgb24bpp;

            Effect = new ConnectedComponentsLabeling();
            Effect.MinWidth = (int)Width.T0;
            Effect.MaxWidth = (int)Width.T1;
            Effect.MinHeight = (int)Height.T0;
            Effect.MaxHeight = (int)Height.T1;

            Effect.CoupledSizeFiltering = Coupled;
            Effect.FilterBlobs = Blob;

            filter = Effect;
        }

    }
}