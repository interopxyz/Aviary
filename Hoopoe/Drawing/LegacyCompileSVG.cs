using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Svg;
using Svg.Transforms;
using Svg.FilterEffects;
using Svg.Pathing;

using Wind.Geometry.Curves;
using Wind.Geometry.Curves.Primitives;
using System.Drawing;
using Wind.Geometry.Curves.Splines;
using Wind.Geometry.Vectors;
using Wind.Containers;

namespace Hoopoe.Drawing
{
    public class LegacyCompileSVG
    {
        public string testing = null;
        SvgDocument Doc = new SvgDocument();

        List<SvgPath> Paths = new List<SvgPath>();

        public LegacyCompileSVG()
        {
            Doc = new SvgDocument();

            SvgViewBox Box = new SvgViewBox(0, 0, 800, 600);
            Doc.ViewBox = Box;
        }

        public void Save(string FilePath)
        {
            Doc.Write(FilePath);
        }

        public void AddShape(wShapeCollection Shapes)
        {
            switch (Shapes.Type)
            {
                case "Arc":
                    AddArc((wArc)Shapes.Shapes[0].Curve);
                    break;
                case "Circle":
                    AddCircle((wCircle)Shapes.Shapes[0].Curve);
                    break;
                case "Ellipse":
                    AddEllipse((wEllipse)Shapes.Shapes[0].Curve);
                    break;
                case "Line":
                    AddLine((wLine)Shapes.Shapes[0].Curve);
                    break;
                case "Polyline":
                    AddPolyline((wPolyline)Shapes.Shapes[0].Curve);
                    break;
                case "BezierSpline":
                    AddSpline((wBezierSpline)Shapes.Shapes[0].Curve);
                    break;
                default:
                    break;
            }

            SetGraphics(Shapes.Graphics);

        }
        
        public void AddArc(wArc InputCurve)
        {
            SvgArcSegment seg = new SvgArcSegment(new PointF((float)InputCurve.StartPoint.X, (float)InputCurve.StartPoint.Y), (float)InputCurve.Radius, (float)InputCurve.Radius,(float)InputCurve.Angle,SvgArcSize.Small,SvgArcSweep.Positive, new PointF((float)InputCurve.EndPoint.X, (float)InputCurve.EndPoint.Y));
            SvgPath path = new SvgPath();

            //Doc.Children.Add(path);
        }

        public void AddCircle(wCircle InputCurve)
        {
            SvgCircle path = new SvgCircle();
            path.CenterX = new SvgUnit( (float)InputCurve.Center.X);
            path.CenterY = new SvgUnit((float)InputCurve.Center.Y);
            path.Radius = new SvgUnit((float)InputCurve.Radius);

            SvgGaussianBlur blur = new SvgGaussianBlur(10, BlurType.Both);
            SvgFilter filter = new SvgFilter();

            filter.AddStyle("Blur", "0", 0);
   
            Doc.Children.Add(path);
            
        }

        public void AddEllipse(wEllipse InputCurve)
        {
            SvgTransformCollection xForm = new SvgTransformCollection();
            SvgEllipse path = new SvgEllipse();

            xForm.Add(new SvgRotate((float)InputCurve.Rotation, (float)InputCurve.Center.X, (float)InputCurve.Center.Y));

            path.CenterX = new SvgUnit((float)InputCurve.Center.X);
            path.CenterY = new SvgUnit((float)InputCurve.Center.Y);
            path.RadiusX = new SvgUnit((float)InputCurve.RadiusX);
            path.RadiusY = new SvgUnit((float)InputCurve.RadiusY);

            path.Transforms = xForm;

            Doc.Children.Add(path);
            
        }

        public void AddLine(wLine InputCurve)
        {
            SvgLine path = new SvgLine();
            path.StartX = (float)InputCurve.Start.X;
            path.StartY = (float)InputCurve.Start.Y;

            path.EndX = (float)InputCurve.End.X;
            path.EndY = (float)InputCurve.End.Y;
            
            Doc.Children.Add(path);
            
        }

        public void AddPolyline(wPolyline InputCurve)
        {
            SvgPolyline path = new SvgPolyline();

            List<wPoint> points = InputCurve.Points;
            SvgPointCollection pointset = new SvgPointCollection();

            for(int i = 0;i< points.Count();i++)
            {

                pointset.Add(new SvgUnit((float)points[i].X));
                pointset.Add(new SvgUnit((float)points[i].Y));

            }

            path.Points = pointset;

            Doc.Children.Add(path);
            
        }

        public void AddSpline(wBezierSpline InputCurve)
        {
            SvgPath path = new SvgPath();
            SvgPathSegmentList segments = new SvgPathSegmentList();
            SvgPathBuilder build = new SvgPathBuilder();

            for(int i = 0;i<(InputCurve.Points.Count()-3);i+=3)
            {

                //wCubicBezier wSeg = InputCurve.Segments[i];
                //SvgPathSegment segment = new SvgCubicCurveSegment(wSeg.StartPoint.ToPointF(), wSeg.StartControlPoint.ToPointF(), wSeg.EndControlPoint.ToPointF(), wSeg.EndPoint.ToPointF());

                SvgCubicCurveSegment segment = new SvgCubicCurveSegment(InputCurve.Points[i].ToPointF(), InputCurve.Points[i+1].ToPointF(), InputCurve.Points[i+2].ToPointF(), InputCurve.Points[i+3].ToPointF());
                
                segments.Add(segment);

            }
            

            path.PathData = segments;

            testing = segments.ToString();

            Doc.Children.Add(path);

        }

        public void SetGraphics(wGraphic pathGraphic)
        {
            int count = Doc.Children.Count - 1;
            var shp = Doc.Children[count];

            SvgPaintServer fill = new SvgColourServer(pathGraphic.Background.ToDrawingColor());

            shp.Fill = fill;
            shp.FillOpacity = (float)(pathGraphic.Background.A / 255.0);
            

            SvgPaintServer stroke = new SvgColourServer(pathGraphic.StrokeColor.ToDrawingColor());

            shp.Stroke = stroke;
            shp.StrokeOpacity = (float)(pathGraphic.StrokeColor.A / 255.0);
            shp.StrokeWidth = new SvgUnit(SvgUnitType.Pixel,(float)pathGraphic.StrokeWeight[0]);

            shp.StrokeLineCap = StrokeCapToSVG((int)pathGraphic.StrokeCap);
            shp.StrokeLineJoin = StrokeCornerToSVG((int)pathGraphic.StrokeCorner);
            shp.StrokeMiterLimit = 89.0f;


            SvgUnitCollection pattern = new SvgUnitCollection();
            
            List<SvgUnit> unitViaFloat = pathGraphic.StrokePattern.ToList().ConvertAll(x => (SvgUnit)(float)x);

            pattern.AddRange(unitViaFloat);

            shp.StrokeDashArray =  pattern;

            Doc.Children[count] = shp;

        }

        public SvgStrokeLineCap StrokeCapToSVG(int WindCapType)
        {
            SvgStrokeLineCap cap = SvgStrokeLineCap.Butt;

            switch(WindCapType)
            {
                case 1:
                    cap = SvgStrokeLineCap.Square;
                    break;
                case 2:
                    cap = SvgStrokeLineCap.Round;
                    break;
            }

            return cap;
        }

        public SvgStrokeLineJoin StrokeCornerToSVG(int WindCornerType)
        {
            SvgStrokeLineJoin cap = SvgStrokeLineJoin.Bevel;

            switch (WindCornerType)
            {
                case 1:
                    cap = SvgStrokeLineJoin.Miter;
                    break;
                case 2:
                    cap = SvgStrokeLineJoin.Round;
                    break;
            }

            return cap;
        }

    }


}
