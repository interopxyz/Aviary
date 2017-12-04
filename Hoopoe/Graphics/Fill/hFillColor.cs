using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Hoopoe.Graphics.Fill
{
    class hFillColor : hGraphic
    {

        public hFillColor()
        {
            Value = "fill=\"none\"";
        }

        public void Empty()
        {
            Value = " ";
        }

        public void None()
        {
            Value = "fill=\"none\"";
        }

        public hFillColor(wColor WindColor)
        {
            if (WindColor.A == 0)
            {
                Value = "fill=\"none\"";
            }
            else
            {
                Value = "fill=\"rgb(" + WindColor.R + "," + WindColor.G + "," + WindColor.B + ")\"";
            }
        }
    }
}