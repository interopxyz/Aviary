
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Containers;
using Wind.Geometry.Curves;
using Wind.Geometry.Curves.Primitives;
using Wind.Geometry.Curves.Splines;
using Wind.Geometry.Vectors;

using Hoopoe.Assembly;
using Hoopoe.Geometry;
using Hoopoe.Geometry.Curves;
using Hoopoe.Geometry.Primitives;
using Hoopoe.Graphics.Stroke;
using Hoopoe.Graphics.Fill;
using Hoopoe.Transform;

namespace Hoopoe.Drawing
{
    public class CompiledSVG
    {

        public hDocument Doc = new hDocument();
        public StringBuilder Content = new StringBuilder();

        public CompiledSVG()
        {
        }
        
        public void SetSize(int Width, int Height)
        {
            Doc.SetSize(Width, Height);
        }

        public void Build()
        {
            Doc.Build(Content.ToString());
        }

        public void Save(string FilePath)
        {
            StreamWriter Writer = new StreamWriter(FilePath);
            Writer.Write(Doc.svgText.ToString());
            Writer.Close();
        }

        public void AddShape(wShapeCollection Shapes)
        {
            hCurve crv = null;
            hShape shape = new hShape();
            hPath path = new hPath();

            switch (Shapes.Type)
            {
                case "Arc":
                    AddArc((wArc)Shapes.Shapes[0].Curve);
                    break;
                case "Circle":
                    shape = new hShape(AddCircle((wCircle)Shapes.Shapes[0].Curve));
                    break;
                case "Ellipse":
                    wEllipse tempEllipse = (wEllipse)Shapes.Shapes[0].Curve;
                    shape = new hShape(AddEllipse(tempEllipse));
                    shape.AddAttribute(new hTransform(new hRotate(tempEllipse.Center,tempEllipse.Rotation).Transformation).Transformation);
                    break;
                case "Line":
                    shape = new hShape(AddLine((wLine)Shapes.Shapes[0].Curve));
                    break;
                case "Polyline":
                    shape = new hShape(AddPolyline((wPolyline)Shapes.Shapes[0].Curve));
                    break;
                case "BezierSpline":
                    shape = new hShape(AddSpline((wBezierSpline)Shapes.Shapes[0].Curve));
                    break;
                default:
                    break;
            }


            SetGraphics(shape, Shapes.Graphics);

            path = new hPath(shape);

            Content.Append(path.svgPath);

        }

        public void AddArc(wArc InputCurve)
        {


        }

        public hCircle AddCircle(wCircle InputCurve)
        {
            hCircle crv = new hCircle(InputCurve);
            crv.BuildCurve();
            return crv;
        }

        public hEllipse AddEllipse(wEllipse InputCurve)
        {
            hEllipse crv = new hEllipse(InputCurve);
            crv.BuildCurve();
            return crv;
        }

        public hLine AddLine(wLine InputCurve)
        {
            hLine crv = new hLine(InputCurve);
            crv.BuildCurve();
            return crv;
        }

        public hPolyline AddPolyline(wPolyline InputCurve)
        {
            hPolyline crv = new hPolyline(InputCurve);
            crv.BuildCurve();
            return crv;
        }

        public hCubicBezierSpline AddSpline(wBezierSpline InputCurve)
        {
            hCubicBezierSpline crv = new hCubicBezierSpline(InputCurve);
            crv.BuildCurve();
            return crv;
        }
        
        public hShape SetGraphics(hShape HoopoeShape, wGraphic Graphics)
        {
            HoopoeShape.AddAttribute(new hStrokeColor(Graphics.StrokeColor).Value);
            HoopoeShape.AddAttribute(new hStrokeWeight(Graphics.StrokeWeight[0]).Value);
            HoopoeShape.AddAttribute(new hStrokeOpacity(Graphics.StrokeColor.A).Value);

            HoopoeShape.AddAttribute(new hFillColor(Graphics.Background).Value);
            HoopoeShape.AddAttribute(new hFillOpacity(Graphics.Background.A).Value);

            return HoopoeShape;
        }

    }
}
