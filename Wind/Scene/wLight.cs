using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Wind.Types;
using Wind.Utilities;

namespace Wind.Scene
{

    public class wLight
    {
        public string Type = "Ambient";
        public wColor LightColor = new wColor();
        public double Intensity = 100.0;
        public Light LightWPF = new AmbientLight();

        public wLight()
        {

        }

        public wLight(string Light_Type)
        {
            Type = Light_Type;
        }

        public void SetBrightness(double Light_Intensity)
        {
            Intensity = Light_Intensity;
            LightColor = new AdjustColor(LightColor).SetLuminance(Intensity / 100.00);
        }

        public Light GetWPFLight()
        {
            LightWPF.Color = LightColor.ToMediaColor();
            return LightWPF;
        }
        
    }
}
