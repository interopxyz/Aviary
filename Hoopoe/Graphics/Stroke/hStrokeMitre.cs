using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.Graphics.Stroke
{
    public class hStrokeMitre : hGraphic
    {
        public double Mitre = 4;

        public hStrokeMitre()
        {
        }

        public hStrokeMitre(double CornerMitre)
        {
            Mitre = CornerMitre;
            Value = "stroke-miterlimit=\"" + Mitre + "\"";
        }

    }
}
