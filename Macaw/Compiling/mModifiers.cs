using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoundInTheory.DynamicImage;
using SoundInTheory.DynamicImage.Fluent;
using SoundInTheory.DynamicImage.Filters;

namespace Macaw.Compiling
{
    public class mModifiers
    {
        public string Type = "LayerFilter";
        public List<Filter> Modifiers = new List<Filter>();

        public mModifiers()
        {

        }

        public void AddFilter(Filter Filter)
        {
            Modifiers.Add(Filter);
        }



        }
}
