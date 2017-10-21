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
    public class wDirectionalLight : wLight
    {
        public wVector Direction = new wVector(-1, -1, -1);

        public wDirectionalLight()
        {

            SetWPFLight();
        }

        public wDirectionalLight(double Light_Intensity, wVector Light_Direction)
        {
            Direction = Light_Direction;

            Intensity = Light_Intensity;
            LightColor = new AdjustColor(LightColor).SetLuminance(Intensity / 100.00);

            SetWPFLight();
        }

        public wDirectionalLight(double Light_Intensity, wVector Light_Direction, wColor Light_Color)
        {
            Direction = Light_Direction;

            Intensity = Light_Intensity;
            LightColor = new AdjustColor(Light_Color).SetLuminance(Intensity / 100.00);

            SetWPFLight();
        }

        public wDirectionalLight( wVector Light_Direction)
        {
            Direction = Light_Direction;
            

            SetWPFLight();
        }

        public wDirectionalLight( wVector Light_Direction, wColor Light_Color)
        {
            Direction = Light_Direction;
            
            LightColor = Light_Color;

            SetWPFLight();
        }
        

        public void SetWPFLight()
        {
            System.Windows.Media.Color Clr = LightColor.ToMediaColor();
            System.Drawing.Color Xlr = System.Drawing.Color.Red;
            
            DirectionalLight LightObject = new DirectionalLight(LightColor.ToMediaColor(),Direction.ToVector3D());
            LightWPF = LightObject;
        }
    }
}
