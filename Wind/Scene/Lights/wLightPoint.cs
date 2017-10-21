using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Types;
using Wind.Geometry.Vectors;
using System.Windows.Media.Media3D;
using Wind.Utilities;

namespace Wind.Scene
{
    public class wPointLight : wLight
    {
        public wPoint Origin = new wPoint(0, 0, 0);

        public wPointLight()
        {

            SetWPFLight();
        }

        public wPointLight(double Light_Intensity, wPoint Light_Origin)
        {
            Origin = Light_Origin;

            Intensity = Light_Intensity;
            LightColor = new AdjustColor(LightColor).SetLuminance(Intensity / 100.00);

            SetWPFLight();
        }

        public wPointLight(double Light_Intensity, wPoint Light_Origin, wColor Light_Color)
        {
            Origin = Light_Origin;

            Intensity = Light_Intensity;
            LightColor = new AdjustColor(Light_Color).SetLuminance(Intensity / 100.00);

            SetWPFLight();
        }

        public wPointLight( wPoint Light_Origin)
        {
            Origin = Light_Origin;

            SetWPFLight();
        }

        public wPointLight( wPoint Light_Origin, wColor Light_Color)
        {
            Origin = Light_Origin;
            
            LightColor = Light_Color;

            SetWPFLight();
        }
        

        public void SetWPFLight()
        {
            PointLight LightObject = new PointLight(LightColor.ToMediaColor(), Origin.ToPoint3D());

            LightWPF = LightObject;
        }
    }
}
