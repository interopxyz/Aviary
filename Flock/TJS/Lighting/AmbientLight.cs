using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Flock.TJS.Lighting
{
    public class AmbientLight : Light
    {

        public AmbientLight(string LightName)
        {
            Name = LightName;
        }

        public AmbientLight(string LightName, wColor LightColor, double Intensity)
        {
            Name = LightName;
        }



    }
}
