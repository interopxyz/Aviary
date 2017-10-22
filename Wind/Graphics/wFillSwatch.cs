using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Wind.Geometry.Curves;

namespace Wind.Graphics
{
    public class wFillSwatch : wFill
    {

        public wFillSwatch()
        {
        }

        public wFillSwatch(wShapeCollection ShapeSet, double Scale, int TilingMode,int PanelWidth, int PanelHeight)
        {

            foreach (wShape Shp in ShapeSet.Shapes)
            {
                GeometryDrawing dwgG = new GeometryDrawing(new SolidColorBrush(Shp.Graphic.Foreground.ToMediaColor()), new Pen(new SolidColorBrush(Shp.Graphic.StrokeColor.ToMediaColor()), Shp.Graphic.StrokeWeight[0]), Shp.GeometrySet);
                dwgG.Pen.StartLineCap = (PenLineCap)Shp.Graphic.StrokeCap;
                dwgG.Pen.EndLineCap = (PenLineCap)Shp.Graphic.StrokeCap;
                DwgGroup.Children.Add(dwgG);
            }

            DwgGroup.ClipGeometry = new RectangleGeometry(new System.Windows.Rect(0, 0, PanelWidth, PanelHeight));

            DwgBrush.Drawing = DwgGroup;
            DwgBrush.Viewbox = new System.Windows.Rect(0, 0, 1, 1);
            DwgBrush.Viewport = new System.Windows.Rect(0, 0, Scale, Scale);
            DwgBrush.TileMode = (TileMode)TilingMode;

            FillBrush = DwgBrush;
        }

    }
}
