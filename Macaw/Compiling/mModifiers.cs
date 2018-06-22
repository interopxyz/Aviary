using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoundInTheory.DynamicImage;
using SoundInTheory.DynamicImage.Fluent;
using SoundInTheory.DynamicImage.Filters;
using Macaw.Compiling.Modifiers;

namespace Macaw.Compiling
{
    public class mModifiers
    {
        public string Type = "LayerFilter";
        public List<mModifier> Modifiers = new List<mModifier>();

        public mModifiers()
        {

        }

        public void AddFilter(mModifier Modifier)
        {
            Modifiers.Add(Modifier);
        }



        }
}
