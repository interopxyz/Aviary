
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

using Hoopoe.SVG.Assembly;
using Hoopoe.SVG.Geometry;
using Hoopoe.SVG.Geometry.Curves;
using Hoopoe.SVG.Geometry.Primitives;
using Hoopoe.SVG.Graphics.Stroke;
using Hoopoe.SVG.Graphics.Fill;
using Hoopoe.SVG.Transform;
using Hoopoe.SVG.Geometry.Compound;
using Hoopoe.SVG.Modify;
using Hoopoe.SVG.Graphics.Effects;
using Hoopoe.SVG.Geometry.Text;

namespace Hoopoe.SVG.Drawing
{
    public class CompileSVG
    {

        public hDocument Doc = new hDocument();
        public Dictionary<string,StringBuilder> PathSet = new Dictionary<string, StringBuilder>();
        public hAnimation Frames = new hAnimation();
        public int FrameCount = 0;
        public StringBuilder Content = new StringBuilder();
        public StringBuilder Styles = new StringBuilder();

        private int Index = 0;
        private wShapeCollection Shapes = new wShapeCollection();

        public CompileSVG()
        {
        }
        
        public void LoadPaths(Dictionary<string, StringBuilder> PresetPathSet)
        {
            PathSet = PresetPathSet;
        }

        public void SetSize(int Width, int Height, wRectangle CropBoundary, double Scale)
        {
            Doc.SetSize(Width, Height, CropBoundary, Scale);
        }

        public void SetQuality(int QualityMode)
        {
            Doc.SetQuality((hDocument.RenderingMode)QualityMode);
        }

        public void Build()
        {
            Doc.Build(Styles.ToString(), Content.ToString());
        }

        public void Save(string FilePath)
        {
            StreamWriter Writer = new StreamWriter(FilePath);
            Writer.Write(Doc.svgText.ToString());
            Writer.Close();
        }

        public void SetShapeType(wShapeCollection HoopoeShapes, int ShapeIndex)
        {
            Index = ShapeIndex;
            Shapes = HoopoeShapes;

            switch (Shapes.Type)
            {
                case "PolyCurveGroup":
                    CompoundCurve();
                    break;
                case "PolylineGroup":
                    CompoundPolyline();
                    break;
                case "Text":
                    AddText();
                    break;
                default:
                    AddShape();
                    break;
            }
        }

        private void AddText()
        {
            hCompoundCurve crv = new hCompoundCurve();
            hShape shape = new hShape("p" + Index);

            GroupCheck(Shapes);

            hText txt = new hText((wTextObject)Shapes.Shapes[0].Curve, Shapes.Fonts);
            txt.BuildSVGCurve();

            PathSet[Shapes.Group].Append(txt.Curve);
        }

        public void CompoundPolyline()
        {
            hCompoundCurve crv = new hCompoundCurve();
            hShape shape = new hShape("p" + Index);
            hPath path = new hPath();

            GroupCheck(Shapes);

            foreach (wShape Shape in Shapes.Shapes)
            {
                hPolyline pline = AddPolyline((wPolyline)Shape.Curve);

                crv.AddCurve(pline);
            }

            shape.SetShape(crv);

            shape.AddAttribute(new hBoolean(hBoolean.FillRule.evenodd).Value);

            shape = SetGraphics(shape, Shapes.Graphics);

            if (Shapes.Effects.HasEffect)
            {
                hFilter filter = SetEffects(Shapes.Effects, Index);
                PathSet[Shapes.Group].Append(filter.Value);
                shape.AddAttribute(filter.ApplyFilter());
            }

            path = new hPath(shape);

            PathSet[Shapes.Group].Append(path.svgPath);
        }

        public void CompoundCurve()
        {
            hCompoundCurve crv = new hCompoundCurve();
            hShape shape = new hShape("p" + Index);
            hPath path = new hPath();

            GroupCheck(Shapes);

            foreach (wShape Shape in Shapes.Shapes)
            {
                hCubicBezierSpline spline = AddSpline((wBezierSpline)Shape.Curve);

                crv.AddCurve(spline);
            }

            shape.SetShape(crv);

            shape.AddAttribute(new hBoolean(hBoolean.FillRule.evenodd).Value);

            shape = SetGraphics(shape, Shapes.Graphics);

            if (Shapes.Effects.HasEffect)
            {
                hFilter filter = SetEffects(Shapes.Effects, Index);
                PathSet[Shapes.Group].Append(filter.Value);
                shape.AddAttribute(filter.ApplyFilter());
            }

            path = new hPath(shape);

            PathSet[Shapes.Group].Append(path.svgPath);
        }



        public void AddShape()
        {
            hShape shape = new hShape("p" + Index);
            hPath path = new hPath();

            GroupCheck(Shapes);

            switch (Shapes.Type)
            {
                case "Arc":
                    AddArc((wArc)Shapes.Shapes[0].Curve);
                    break;
                case "Circle":
                    shape.SetShape(AddCircle((wCircle)Shapes.Shapes[0].Curve));
                    break;
                case "Ellipse":
                    wEllipse tempEllipse = (wEllipse)Shapes.Shapes[0].Curve;
                    shape.SetShape(AddEllipse(tempEllipse));
                    shape.AddAttribute(new hTransform(new hRotate(tempEllipse.Center,tempEllipse.Rotation).Transformation).Transformation);
                    break;
                case "Line":
                    shape.SetShape(AddLine((wLine)Shapes.Shapes[0].Curve));
                    break;
                case "Polyline":
                    shape.SetShape(AddPolyline((wPolyline)Shapes.Shapes[0].Curve));
                    break;
                case "BezierSpline":
                    shape.SetShape(AddSpline((wBezierSpline)Shapes.Shapes[0].Curve));
                    break;
                default:
                    break;
            }

            shape = SetGraphics(shape, Shapes.Graphics);

            if (Shapes.Effects.HasEffect)
            {
                hFilter filter = SetEffects(Shapes.Effects, Index);
                PathSet[Shapes.Group].Append( filter.Value);
                shape.AddAttribute(filter.ApplyFilter());
            }

            path = new hPath(shape);

            PathSet[Shapes.Group].Append(path.svgPath);

        }

        private void AddArc(wArc InputCurve)
        {


        }

        private hCircle AddCircle(wCircle InputCurve)
        {
            hCircle crv = new hCircle(InputCurve);
            crv.BuildSVGCurve();
            return crv;
        }

        private hEllipse AddEllipse(wEllipse InputCurve)
        {
            hEllipse crv = new hEllipse(InputCurve);
            crv.BuildSVGCurve();
            return crv;
        }

        private hLine AddLine(wLine InputCurve)
        {
            hLine crv = new hLine(InputCurve);
            crv.BuildSVGCurve();
            return crv;
        }

        private hPolyline AddPolyline(wPolyline InputCurve)
        {
            hPolyline crv = new hPolyline(InputCurve);
            crv.BuildSVGCurve();
            return crv;
        }

        private hCubicBezierSpline AddSpline(wBezierSpline InputCurve)
        {
            hCubicBezierSpline crv = new hCubicBezierSpline(InputCurve);
            crv.BuildSVGCurve();
            return crv;
        }

        private void GroupCheck(wShapeCollection shapes)
        {
            string Name = shapes.Group;
            if (!PathSet.ContainsKey(Name))
            {
                PathSet.Add(Name, new StringBuilder());
                Frames.AddFrame(shapes.Frame);
                FrameCount += Convert.ToInt32(shapes.Frame.Active);
            }
        }

        private hShape SetGraphics(hShape HoopoeShape, wGraphic Graphics)
        {
            HoopoeShape.AddAttribute(new hStrokeColor(Graphics.StrokeColor).Value);
            HoopoeShape.AddAttribute(new hStrokeWeight(Graphics.StrokeWeight[0]).Value);
            HoopoeShape.AddAttribute(new hStrokeOpacity(Graphics.StrokeColor.A).Value);
            HoopoeShape.AddAttribute(new hStrokePattern(Graphics.StrokePattern).Value);
            HoopoeShape.AddAttribute(new hStrokeCap((hStrokeCap.StrokeCap)Graphics.StrokeCap).Value);
            HoopoeShape.AddAttribute(new hStrokeCorner((hStrokeCorner.StrokeCorner)Graphics.StrokeCorner).Value);
            HoopoeShape.AddAttribute(new hStrokeMitre(89).Value);

            switch(Graphics.FillType)
            {
                case wGraphic.FillTypes.Solid:
                    HoopoeShape.AddAttribute(new hFillColor(Graphics.Background).Value);
                    HoopoeShape.AddAttribute(new hFillOpacity(Graphics.Background.A).Value);
                    break;
                case wGraphic.FillTypes.Pattern:
                    HoopoeShape.AddAttribute(new hFillColor(Graphics.Background).Value);
                    HoopoeShape.AddAttribute(new hFillOpacity(Graphics.Background.A).Value);
                    break;
                case wGraphic.FillTypes.LinearGradient:
                    hFillGradientLinear Gradient = new hFillGradientLinear(Index, Graphics.Gradient);
                    Styles.Append(Gradient.Style);
                    HoopoeShape.AddAttribute(Gradient.Value);
                    break;
                case wGraphic.FillTypes.RadialGradient:
                    hFillGradientRadial RGradient = new hFillGradientRadial(Index, Graphics.Gradient);
                    Styles.Append(RGradient.Style);
                    HoopoeShape.AddAttribute(RGradient.Value);
                    break;
                case wGraphic.FillTypes.Bitmap:
                    hFillBitmap hBitmapFill = new hFillBitmap(Index, Graphics.FillBitmap,  Shapes.Boundary.Width, Shapes.Boundary.Height);
                    Styles.Append(hBitmapFill.Style);
                    HoopoeShape.AddAttribute(hBitmapFill.Value);
                    break;
            }

            return HoopoeShape;
        }

        private hFilter SetEffects(wEffects Effects, int Index)
        {
            string InID = "SourceGraphic";
            string OutID = "Result";

            hFilter Filter = new hFilter(Index.ToString());
            if (Effects.Blur.Active)
            {
                Filter.AddEffect(new hBlur(Effects.Blur,InID, OutID).Value);
                InID = OutID;
                OutID = "ResultBlur";
            }

            if (Effects.DropShadow.Active)
            {
                Filter.AddEffect(new hDropShadow(Effects.DropShadow, InID, OutID).Value);
                InID = OutID;
                OutID = "ResultShadow";
            }

            Filter.Build();
            return Filter;
        }

        public void SetFrames()
        {
            Frames.CloseSet(true);
                
            for(int i = 0; i<Frames.Indices.Count;i++)
            {
                hFrame Frame = new hFrame(i, PathSet[PathSet.Keys.ElementAt(Frames.Sequence[i])],Frames);
                Content.Append(Frame.svgGroup.ToString());
            }
        }

        public void SetGroups()
        {
            foreach(string GroupID in PathSet.Keys)
            {
                hGroup Group = new hGroup(GroupID,PathSet[GroupID]);
                Content.Append(Group.svgGroup.ToString());
            }
        }

    }
}
