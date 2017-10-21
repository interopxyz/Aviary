using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Geometry.Shapes;
using Wind.Geometry.Vectors;

namespace Wind.Geometry.Curves
{
    public class wTextObject : wCurve
    {
        public override string GetCurveType { get { return "Text"; } }

        public wText Text = new wText("");
        public wPlane Plane = new wPlane().XYPlane();

        public wTextObject()
        {

        }

        public wTextObject(wText WindText)
        {
            Text = WindText;
        }

        public wTextObject(wText WindText, wPlane PlaneObject)
        {
            Text = WindText;
            Plane = PlaneObject;
        }

    }
}
