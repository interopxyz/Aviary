using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Wind.Geometry.Vectors;
using Wind.Types;
using Wind.Utilities;

namespace Wind.Scene
{
    public class wAmbientLight : wLight
    {

        public wAmbientLight()
        {

            SetWPFLight();
        }

        public wAmbientLight(double Light_Intensity)
        {
            Intensity = Light_Intensity;

            SetWPFLight();
        }

        public wAmbientLight(double Light_Intensity, wColor Light_Color)
        {
            Intensity = Light_Intensity;
            LightColor = new AdjustColor(Light_Color).SetLuminance(Intensity / 100.00);

            SetWPFLight();
        }

        public wAmbientLight(wColor Light_Color)
        {
            LightColor = Light_Color;

            SetWPFLight();
        }
        

        public void SetWPFLight()
        {
            AmbientLight LightObject = new AmbientLight(LightColor.ToMediaColor());

            LightWPF = LightObject;
        }

    }


}
