using AForge.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mDitherJarvisJudiceNinke : mFilter
    {
        JarvisJudiceNinkeDithering Effect = new JarvisJudiceNinkeDithering();
        
        public mDitherJarvisJudiceNinke()
        {

            BitmapType = 0;

            Effect = new JarvisJudiceNinkeDithering();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}

