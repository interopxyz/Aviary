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

namespace Hoopoe.Drawing
{
    public class CompileSVG
    {

        SvgDocument Doc = new SvgDocument();

        List<SvgPath> Paths = new List<SvgPath>();

        public CompileSVG()
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

        }
        
        public void AddArc(wArc InputCurve)
        {
            SvgArcSegment seg = new SvgArcSegment(new PointF((float)InputCurve.StartPoint.X, (float)InputCurve.StartPoint.Y), (float)InputCurve.Radius, (float)InputCurve.Radius,(float)InputCurve.Angle,SvgArcSize.Small,SvgArcSweep.Positive, new PointF((float)InputCurve.EndPoint.X, (float)InputCurve.EndPoint.Y));
            SvgPath path = new SvgPath();
            //seg.
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

            SvgPointCollection points = new SvgPointCollection();
            for(int i = 0;i<InputCurve.Points.Count();i++)
            {

            }


        }

        public void AddSpline(wBezierSpline InputCurve)
        {


        }

    }


}
