using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.Assembly
{
    public class hDocument
    {

        public StringBuilder svgText = new StringBuilder();

        public int Width = 800;
        public int Height = 600;

        public hDocument()
        {

        }

        public void SetSize(int PanelWidth, int PanelHeight)
        {
            Width = PanelWidth;
            Height = PanelHeight;
        }
        

        public void Build(string svgAssembly)
        {
            svgText.Append("<svg ");
            svgText.Append("width =\"" + Width + "\" ");
            svgText.Append("height =\"" + Height + "\" ");
            svgText.Append("xmlns = \"http://www.w3.org/2000/svg\" > ");
            svgText.Append(svgAssembly);
            svgText.Append(" </svg>" + Environment.NewLine);

        }
    }
}
