using Parrot.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wind.Containers;
using Wind.Geometry.Curves;
using Wind.Geometry.Curves.Primitives;
using Wind.Geometry.Curves.Splines;
using Wind.Geometry.Vectors;

namespace Parrot.Drawings
{
    public class pDrawing : pControl
    {
        public Canvas Element;
        public string Type;

        public wRectangle Boundary = new wRectangle();
        public wRectangle Extents = new wRectangle();
        public wRectangle Frame = new wRectangle();
        public double Scale = 1;
        public Point Center = new Point();
        public TransformGroup Xform = new TransformGroup();
        public wShapeCollection group = new wShapeCollection();
        
        public pDrawing(string InstanceName)
        {
            Type = "Drawing";
            Element = new Canvas();
            Element.Name = InstanceName;
        }

        public void SetProperties()
        {
        }

        public void ClearDrawing()
        {
            Element.Children.Clear();
            group.Clear();
        }

        public void SetCanvasSize(wRectangle FrameRectangle, wRectangle BoundaryRectangle)
        {
            Boundary = BoundaryRectangle;
            Frame = FrameRectangle;

            Center = new Point((int)(Frame.Width/2),(int)(Frame.Height/2));

            group.Boundary.Width = Frame.Width;
            group.Boundary.Height = Frame.Height;

            Element.Width = Frame.Width;
            Element.Height = Frame.Height;
        }

        public void SetScale()
        {
            double X = (Frame.Width / Boundary.Width);
            double Y= (Frame.Height / Boundary.Height);

            if (X < Y) { Scale = X; }else { Scale = Y; }

            double W = Boundary.Width * Scale;
            double H = Boundary.Height * Scale;
            
            Extents = new wRectangle(new wPlane(new wPoint(Boundary.Center.X*Scale,Boundary.Center.Y*Scale,0),Boundary.Plane.XAxis,Boundary.Plane.YAxis), Boundary.Width * Scale, Boundary.Height * Scale);

            wVector Shift = new wVector().SubtractVector(Frame.Center, Extents.Center);

            Xform.Children.Clear();
            Xform.Children.Add(new TranslateTransform(Shift.X, Shift.Y));
        }

        public void SetCanvas()
        {
            Element.RenderTransform = new ScaleTransform(1, -1, Center.X, Center.Y);
        }

        public void AddShape(wShapeCollection Shapes)
        {
            switch (Shapes.Type)
            {
                case "Arc":
                    Element.Children.Add(WpfArc(Shapes.Shapes[0].Curve, Shapes.Graphics,Shapes.Effects));
                    break;
                case "Circle":
                    Element.Children.Add(WpfCircle(Shapes.Shapes[0].Curve, Shapes.Graphics, Shapes.Effects));
                    break;
                case "Ellipse":
                    Element.Children.Add(WpfEllipse(Shapes.Shapes[0].Curve, Shapes.Graphics, Shapes.Effects));
                    break;
                case "Line":
                    Element.Children.Add(WpfLine(Shapes.Shapes[0].Curve, Shapes.Graphics, Shapes.Effects));
                    break;
                case "Polyline":
                    Element.Children.Add(WpfPolyline(Shapes.Shapes[0].Curve, Shapes.Graphics, Shapes.Effects));
                    break;
                case "BezierSpline":
                    Element.Children.Add(WpfSpline(Shapes.Shapes[0].Curve, Shapes.Graphics, Shapes.Effects));
                    break;
                default:

                    break;
            }

        }

        public Path WpfLine(wCurve Shape, wGraphic Graphics, wEffects ShapeEffects)
        {
            Path X = new Path();
            wLine C = (wLine)Shape;
            LineGeometry L = new LineGeometry();

            L.StartPoint = new System.Windows.Point(C.Start.X * Scale, C.Start.Y * Scale);
            L.EndPoint = new System.Windows.Point(C.End.X * Scale, C.End.Y * Scale);
            
            X.Data = L;

            X.RenderTransform = Xform;
            
            X = SetPathEffects(X, ShapeEffects);
            X = SetPathStroke(X, Graphics);
            
            group.Shapes.Add(new wShape(L,Graphics));
            return X;
        }

        public Path WpfCircle(wCurve Shape, wGraphic Graphics, wEffects ShapeEffects)
        {
            Path X = new Path();
            wCircle C = (wCircle)Shape;
            EllipseGeometry E = new EllipseGeometry();

            E.Center = new System.Windows.Point(C.Center.X * Scale, C.Center.Y * Scale);
            E.RadiusX = C.Radius* Scale;
            E.RadiusY = C.Radius * Scale;

            X.Data = E;

            X = SetPathFill(X, Graphics);
            X = SetPathStroke(X, Graphics);
            X = SetPathEffects(X, ShapeEffects);


            group.Shapes.Add(new wShape(E, Graphics));
            return X;
        }

        public Path WpfArc(wCurve Shape, wGraphic Graphics, wEffects ShapeEffects)
        {
            wArc C = (wArc)Shape;
            ArcSegment S = new ArcSegment();
                S.Point = new System.Windows.Point(C.EndPoint.X * Scale, C.EndPoint.Y * Scale);
                S.Size = new System.Windows.Size(C.Radius * Scale, C.Radius * Scale);

            if (C.Clockwise) { S.SweepDirection = SweepDirection.Clockwise; } else { S.SweepDirection = SweepDirection.Counterclockwise; }

            Path X = new Path();
            PathFigure Pf = new PathFigure();
            PathGeometry G = new PathGeometry();
            PathFigureCollection Fc = new PathFigureCollection();
            PathSegmentCollection Sc = new PathSegmentCollection();

            Pf.StartPoint = new System.Windows.Point(C.StartPoint.X * Scale, C.StartPoint.Y * Scale);

            Sc.Add(S);
            Pf.Segments = Sc;
            Fc.Add(Pf);
            G.Figures = Fc;
            X.Data = G;

            X.RenderTransform = Xform;
            
            X = SetPathFill(X, Graphics);
            X = SetPathStroke(X, Graphics);
            X = SetPathEffects(X, ShapeEffects);


            group.Shapes.Add(new wShape(G, Graphics));
            return X;
        }


        public Path WpfEllipse(wCurve Shape, wGraphic Graphics, wEffects ShapeEffects)
        {
            Path X = new Path();
            wEllipse C = (wEllipse)Shape;
            EllipseGeometry E = new EllipseGeometry();

            E.Center = new System.Windows.Point((C.Center.X) * Scale, (C.Center.Y) * Scale);
            E.RadiusX = C.RadiusX * Scale;
            E.RadiusY = C.RadiusY * Scale;

            E.Transform = new RotateTransform(C.Rotation, E.Center.X, E.Center.Y);

            X.Data = E;
            X.RenderTransform = Xform;

            X = SetPathFill(X, Graphics);
            X = SetPathStroke(X, Graphics);
            X = SetPathEffects(X, ShapeEffects);
            
            group.Shapes.Add(new wShape(E, Graphics));
            return X;
        }

        public Path WpfPolyline(wCurve Shape, wGraphic Graphics, wEffects ShapeEffects)
        {
            Path X = new Path();
            wPolyline C = (wPolyline)Shape;
            PathFigure Pf = new PathFigure();
            PolyLineSegment S = new PolyLineSegment();
            PathGeometry G = new PathGeometry();
            PathFigureCollection Fc = new PathFigureCollection();
            PathSegmentCollection Sc = new PathSegmentCollection();
            
            Pf.StartPoint = new System.Windows.Point(C.Points[0].X * Scale, C.Points[0].Y * Scale);

            for (int i = 1; i < C.Points.Count; i++)
            {
                wPoint P = C.Points[i];
                S.Points.Add(new System.Windows.Point(P.X * Scale, P.Y * Scale));
            }

            Sc.Add(S);
            Pf.Segments = Sc;
            Fc.Add(Pf);
            G.Figures = Fc;
            X.Data = G;

            X.StrokeMiterLimit = 1.0;

            X.RenderTransform = Xform;
            
            X = SetPathFill(X, Graphics);
            X = SetPathStroke(X, Graphics);
            X = SetPathEffects(X, ShapeEffects);


            group.Shapes.Add(new wShape(G, Graphics));
            return X;
        }

        public Path WpfSpline(wCurve Shape, wGraphic Graphics, wEffects ShapeEffects)
        {
            wBezierSpline C = (wBezierSpline)Shape;

            PathFigure Pf = new PathFigure();
            PolyBezierSegment S = new PolyBezierSegment();
            PathGeometry G = new PathGeometry();
            PathFigureCollection Fc = new PathFigureCollection();
            PathSegmentCollection Sc = new PathSegmentCollection();

            Path X = new Path();
            Pf.StartPoint = new System.Windows.Point(C.Points[0].X * Scale, C.Points[0].Y * Scale);

            for (int i = 1; i < C.Points.Count; i++)
            {
                wPoint P = C.Points[i];
                S.Points.Add(new System.Windows.Point(P.X * Scale, P.Y * Scale));
            }

            Sc.Add(S);
            Pf.Segments = Sc;
            Fc.Add(Pf);
            G.Figures = Fc;
            X.Data = G;
            
            X.StrokeMiterLimit = 1.0;

            X.RenderTransform = Xform;
            
            X = SetPathFill(X, Graphics);
            X = SetPathStroke(X, Graphics);
            X = SetPathEffects(X, ShapeEffects);


            group.Shapes.Add(new wShape(G, Graphics));
            return X;
        }

        public void AddPolyFigure(wShapeCollection Shapes)
        {
            Element.Children.Add(WpfCompoundPolyline(Shapes));
        }

        public Path WpfCompoundPolyline(wShapeCollection Shapes)
        {
            Path X = new Path();
            PathFigureCollection Fc = new PathFigureCollection();
            PathGeometry G = new PathGeometry();

            Fc.Clear();

            foreach (wShape Shape in Shapes.Shapes)
            {
                PathSegmentCollection Sc = new PathSegmentCollection();
                PathFigure Pf = new PathFigure();
                wCurve Crv = Shape.Curve;
                wPoint StartPoint = Crv.Points[0];

                Pf.StartPoint = new System.Windows.Point(StartPoint.X * Scale, StartPoint.Y * Scale);

                PathGeometry Geo = (PathGeometry)WpfPolyline(Crv, Shapes.Graphics, Shapes.Effects).Data;
                PathFigureCollection Fig = (PathFigureCollection)Geo.Figures;
                PathFigure PFig = (PathFigure)(Fig[0]);
                PathSegmentCollection Seg = (PathSegmentCollection)PFig.Segments;
                
                PolyLineSegment Pl = (PolyLineSegment)Seg[0];
                PolyLineSegment S = new PolyLineSegment(Pl.Points, true);

                Pf.IsClosed = true;

                Sc.Add(S);
                Pf.Segments = Sc;
                Fc.Add(Pf);
            }

            G.Figures = Fc;
            X.Data = G;

            X.RenderTransform = Xform;

            wGraphic Graphics = Shapes.Graphics;
            wEffects ShapeEffects = Shapes.Effects;
            
            X = SetPathFill(X, Graphics);
            X = SetPathStroke(X, Graphics);
            X = SetPathEffects(X, ShapeEffects);


            group.Shapes.Add(new wShape(G, Graphics));
            return X;
        }

        public void AddPolySpline(wShapeCollection Shapes)
        {
            Element.Children.Add(WpfCompoundSpline(Shapes));
        }

        public Path WpfCompoundSpline(wShapeCollection Shapes)
        {
            Path X = new Path();
            PathFigureCollection Fc = new PathFigureCollection();
            PathGeometry G = new PathGeometry();

            Fc.Clear();

            foreach (wShape Shape in Shapes.Shapes)
            {
                PathSegmentCollection Sc = new PathSegmentCollection();
                PathFigure Pf = new PathFigure();
                wBezierSpline C = (wBezierSpline)Shape.Curve;
                wPoint StartPoint = C.Points[0];
                
                PolyBezierSegment S = new PolyBezierSegment();

                Pf.StartPoint = new System.Windows.Point(C.Points[0].X * Scale, C.Points[0].Y * Scale);
                
                for(int i = 1; i <C.Points.Count;i++)
                {
                    wPoint P = C.Points[i];
                    S.Points.Add(new System.Windows.Point(P.X * Scale, P.Y * Scale));
                }
                
                Sc.Add(S);
                Pf.Segments = Sc;
                //Pf.IsClosed = true;
                Fc.Add(Pf);
            }

            G.Figures = Fc;
            X.Data = G;

            X.RenderTransform = Xform;

            wGraphic Graphics = Shapes.Graphics;
            wEffects ShapeEffects = Shapes.Effects;

            X = SetPathStroke(X, Graphics);
            X = SetPathFill(X, Graphics);
            X = SetPathEffects(X, ShapeEffects);


            group.Shapes.Add(new wShape(G, Graphics));
            return X;
        }

        public Bitmap GetBitmap()
        {
            Element.Measure(new System.Windows.Size(Frame.Width, Frame.Height));
            Element.Arrange(new System.Windows.Rect(new System.Windows.Size(Frame.Width, Frame.Height)));

            RenderTargetBitmap B = new RenderTargetBitmap((int)Frame.Width, (int)Frame.Height, 96, 96, PixelFormats.Pbgra32);
            B.Render(Element);

            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(B));
            encoder.Save(stream);

            Bitmap F = new Bitmap(stream);
            //F.MakeTransparent();

            return F;
        }

        //SHAPE GRAPHICS & EFFECTS

        public Path SetPathStroke(Path ShapePath, wGraphic ShapeGraphics)
        {
            System.Windows.Media.Brush brushP = new SolidColorBrush(ShapeGraphics.StrokeColor.ToMediaColor());

            ShapePath.Stroke = brushP;
            ShapePath.StrokeMiterLimit = 1.0;
            ShapePath.StrokeThickness = ShapeGraphics.StrokeWeight[0];

            return ShapePath;
        }

        public Path SetPathFill(Path ShapePath, wGraphic ShapeGraphics)
        {
            System.Windows.Media.Brush brushF = new SolidColorBrush(ShapeGraphics.Background.ToMediaColor());

            ShapePath.Fill = brushF;

            return ShapePath;
        }

        public Path SetPathEffects(Path ShapePath, wEffects ShapeEffects)
        {
            ShapePath.Effect = ShapeEffects.CurrentEffect;
            //ShapePath.OpacityMask = ShapePath.Fill;
            return ShapePath;
        }


        //CONTROL GRAPHICS & EFFECTS

        public override void SetSolidFill()
        {
            Element.Background = new SolidColorBrush(Graphics.Background.ToMediaColor());
        }

        public override void SetStroke()
        {
        }

        public override void SetSize()
        {
            if (Graphics.Width < 1) { Element.Width = double.NaN; } else { Element.Width = Graphics.Width; }
            if (Graphics.Height < 1) { Element.Height = double.NaN; } else { Element.Height = Graphics.Height; }
        }

        public override void SetMargin()
        {
        }

        public override void SetPadding()
        {
        }

        public override void SetFont()
        {
        }


    }
}
