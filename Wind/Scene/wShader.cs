using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Wind.Scene
{
    public class wShader
    {
        public wColor DiffuseColor = new wColor().White();
        public wColor SpecularColor = new wColor().Black();
        public wColor EmissiveColor = new wColor().Black();

        public double SpecularValue = 0.0;
        public double EmissiveValue = 0.0;

        public double Transparency = 1.0;


        public wShader()
        {

        }

        public wShader(wColor Diffuse)
        {
            DiffuseColor = Diffuse;
        }
        
        public void SetDiffuseTransparency()
        {
            DiffuseColor.A = (int)(Transparency * 255.0);
        }

        public void SetSpecularTransparency()
        {
            SpecularColor.A = (int)(Transparency * 255.0);
        }

        public void SetEmissiveTransparency()
        {
            EmissiveColor.A = (int)(Transparency * 255.0);
        }
    }
}
