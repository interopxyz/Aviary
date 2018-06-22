using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Hoopoe.SVG.Graphics.Stroke
{
    public class hStrokeColor : hGraphic
    {

        public hStrokeColor()
        {
            Value = "stroke=\"none\"";
        }

        public void Empty()
        {
            Value = " ";
        }

        public void None()
        {
            Value = "stroke=\"none\"";
        }

        public hStrokeColor(wColor WindColor)
        {
            if(WindColor.A == 0)
            {
                Value = "stroke=\"none\"";
            }
            else
            {
                Value = "stroke=\"rgb(" + WindColor.R + "," + WindColor.G + "," + WindColor.B + ")\"";
            }
        }
    }
}
