using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Containers;
using Wind.Geometry.Curves.Primitives;
using Wind.Types;

namespace Wind.Geometry.Curves
{
    public class wShapeCollection
    {
        public List<wShape> Shapes = new List<wShape>();
        public wGraphic Graphics = new wGraphic();
        public wEffects Effects = new wEffects();
        public wFont Fonts = new wFont();

        public double X = 0;
        public double Y = 0;

        public double Width = 0;
        public double Height = 0;

        public string Type;

        public wRectangle Boundary = new wRectangle();

        public wShapeCollection()
        {

        }

        public wShapeCollection(wShape WindShape)
        {
            Shapes = new List<wShape>() { WindShape };
        }

        public wShapeCollection(List<wShape> WindShapes)
        {
            Shapes = WindShapes;
        }
        public void Clear()
        {
            Shapes.Clear();

            Graphics = new wGraphic();
            Effects = new wEffects();
            Fonts = new wFont();
            
            Boundary = new wRectangle();
    }

    }
}
