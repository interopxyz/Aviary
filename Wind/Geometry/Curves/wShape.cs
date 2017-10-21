using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Wind.Containers;
using Wind.Geometry.Curves.Primitives;

namespace Wind.Geometry.Curves
{
    public class wShape
    {
        public wCurve Curve = null;
        public wGraphic Graphic = new wGraphic();
        public GeometryGroup GeometrySet = new GeometryGroup();

        public wShape()
        {

        }

        public wShape(wCurve WindCurve)
        {
            Curve = WindCurve;
        }

        public wShape(wCurve WindCurve, wGraphic WindGraphic)
        {
            Curve = WindCurve;
            Graphic = WindGraphic;
        }

        public wShape(System.Windows.Media.Geometry GeoObject, wGraphic WindGraphic)
        {
            GeometrySet.Children.Clear();
            GeometrySet.Children.Add(GeoObject);
            Graphic = WindGraphic;
        }

        public wShape(GeometryGroup GeoGroup, wGraphic WindGraphic)
        {
            GeometrySet = GeoGroup;
            Graphic = WindGraphic;
        }

    }
}
