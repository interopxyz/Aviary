using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.Graphics.Fill
{
    public class hFillOpacity : hGraphic
    {

        public hFillOpacity()
        {
            Value = "fill-opacity = \"0.4\"";
        }

        public hFillOpacity(int Alpha)
        {
            Value = "fill-opacity = \"" + Alpha / 255.0 + "255\"";
        }
    }
}
