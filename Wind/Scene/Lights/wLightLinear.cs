using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Types;
using Wind.Geometry.Vectors;
using Wind.Geometry.Curves.Primitives;
using System.Windows.Media.Media3D;
using Wind.Utilities;

namespace Wind.Scene
{
    public class wLightLinear : wLight
    {

        public wLine Line = new wLine(new wPoint(0,0,0),new wPoint(0,0,1));

        public wLightLinear()
        {
        }

        public wLightLinear(double Light_Intensity, wLine Light_Line)
        {
            Line = Light_Line;

            Intensity = Light_Intensity;
            LightColor = new AdjustColor(LightColor).SetLuminance(Intensity / 100.00);
        }

        public wLightLinear(double Light_Intensity, wLine Light_Line, wColor Light_Color)
        {
            Line = Light_Line;

            Intensity = Light_Intensity;
            LightColor = new AdjustColor(Light_Color).SetLuminance(Intensity / 100.00);
        }

        public wLightLinear(wLine Light_Line, wColor Light_Color)
        {
            Line = Light_Line;
            
            LightColor = Light_Color;
        }

        public wLightLinear( wLine Light_Line)
        {
            Line = Light_Line;

        }

        public void SetWPFLight()
        {
            DirectionalLight LightObject = new DirectionalLight(LightColor.ToMediaColor(), new wVector(Line.Start,Line.End).ToVector3D());

            LightWPF = LightObject;
        }

    }
}
