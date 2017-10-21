using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Types;
using Wind.Geometry.Vectors;
using Wind.Utilities;
using System.Windows.Media.Media3D;

namespace Wind.Scene
{
    public class wLightSpot : wLight
    {
        
        public wPoint Location = new wPoint(100, 100, 100);
        public wPoint Target = new wPoint(0, 0, 0);
        public double ConeAngleIn = 30;
        public double ConeAngleOut = 45;
        public double LightIntensity = 100;

        public wLightSpot()
        {
            SetWPFLight();
        }

        public wLightSpot(double Light_Intensity, wPoint Light_Location, wPoint Light_Target)
        {
            Location = Light_Location;
            Target = Light_Target;

            Intensity = Light_Intensity;
            LightColor = new AdjustColor(LightColor).SetLuminance(Intensity / 100.00);

            SetWPFLight();
        }

        public wLightSpot(double Light_Intensity, wPoint Light_Location, wPoint Light_Target, wColor Light_Color)
        {
            Location = Light_Location;
            Target = Light_Target;

            Intensity = Light_Intensity;
            LightColor = new AdjustColor(Light_Color).SetLuminance(Intensity / 100.00);

            SetWPFLight();
        }

        public wLightSpot(double Light_Intensity, wPoint Light_Location, wPoint Light_Target, double Light_InnerAngle, double Light_OuterAngle)
        {
            Location = Light_Location;
            Target = Light_Target;

            Intensity = Light_Intensity;
            LightColor = new AdjustColor(LightColor).SetLuminance(Intensity / 100.00);

            ConeAngleIn = Light_InnerAngle;
            ConeAngleOut = Light_OuterAngle;

            SetWPFLight();
        }

        public wLightSpot(double Light_Intensity, wPoint Light_Location, wPoint Light_Target, double Light_InnerAngle, double Light_OuterAngle, wColor Light_Color)
        {
            Location = Light_Location;
            Target = Light_Target;

            Intensity = Light_Intensity;
            LightColor = new AdjustColor(Light_Color).SetLuminance(Intensity / 100.00);

            SetWPFLight();
        }

        //
        public wLightSpot( wPoint Light_Location, wPoint Light_Target)
        {
            Location = Light_Location;
            Target = Light_Target;
            

            SetWPFLight();
        }

        public wLightSpot( wPoint Light_Location, wPoint Light_Target, wColor Light_Color)
        {
            Location = Light_Location;
            Target = Light_Target;
            
            LightColor = Light_Color;

            SetWPFLight();
        }

        public wLightSpot( wPoint Light_Location, wPoint Light_Target, double Light_InnerAngle, double Light_OuterAngle)
        {
            Location = Light_Location;
            Target = Light_Target;

            ConeAngleIn = Light_InnerAngle;
            ConeAngleOut = Light_OuterAngle;

            SetWPFLight();
        }

        public wLightSpot(wPoint Light_Location, wPoint Light_Target, double Light_InnerAngle, double Light_OuterAngle, wColor Light_Color)
        {
            Location = Light_Location;
            Target = Light_Target;
            
            LightColor = Light_Color;

            SetWPFLight();
        }

        public void SetWPFLight()
        {
            SpotLight LightObject = new SpotLight(LightColor.ToMediaColor(),Location.ToPoint3D(),new wVector(Location,Target).ToVector3D(),ConeAngleOut,ConeAngleIn);

            LightWPF = LightObject;
        }
    }
}
