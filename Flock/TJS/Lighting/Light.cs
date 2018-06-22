using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Presets;
using Wind.Types;

namespace Flock.TJS.Lighting
{
    public abstract class Light
    {
        public string Name = "Light";

        public double Intensity = 1.0;
        public wColor Color = wColors.White;

        public Light()
        {
            
        }

    }
}
