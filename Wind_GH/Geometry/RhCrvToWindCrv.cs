using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Geometry.Vectors;
using Wind.Geometry.Curves;
using Wind.Geometry.Curves.Primitives;
using Wind.Geometry.Curves.Splines;

namespace Wind_GH.Geometry
{
    public class RhCrvToWindCrv
    {
        public wCurve WindCurve = null;

        public RhCrvToWindCrv()
        {
        }

        public RhCrvToWindCrv(Curve RhinoCurve)
        {

            Circle R = new Circle();
            Arc A = new Arc();
            Ellipse S = new Ellipse();
            Polyline P = new Polyline();
            
            
            
            if (RhinoCurve.TryGetCircle(out R))
            {
                WindCurve = new wEllipse(new wPoint(R.Plane.Origin.X, R.Plane.Origin.Y), R.Radius, R.Radius);
            }
            else if (RhinoCurve.TryGetArc(out A))
            {
                WindCurve = new wArc(
                    new wPoint(A.Center.X, A.Center.Y, A.Center.Z), A.Radius,
                    Vector3d.VectorAngle(Vector3d.XAxis, new Vector3d(A.StartPoint - A.Center), Plane.WorldXY) / Math.PI * 180.0,
                    Vector3d.VectorAngle(Vector3d.XAxis, new Vector3d(A.EndPoint - A.Center), Plane.WorldXY) / Math.PI * 180.0);
            }
            else if (RhinoCurve.TryGetEllipse(out S))
            {
                Box bBox = new Box();
                RhinoCurve.GetBoundingBox(S.Plane,out bBox);

                double RadiusX = (bBox.X.T1 - bBox.X.T0) / 2;
                double RadiusY = (bBox.Y.T1 - bBox.Y.T0) / 2;
                double Rotation = Vector3d.VectorAngle(Vector3d.YAxis, S.Plane.YAxis, Plane.WorldXY) / Math.PI * 180.0;

                WindCurve = new wEllipse(new wPoint(S.Plane.Origin.X, S.Plane.Origin.Y), RadiusX, RadiusY, Rotation);
            }
            else if (RhinoCurve.IsLinear())
            {
                Point3d PtA = RhinoCurve.PointAtStart;
                Point3d PtB = RhinoCurve.PointAtEnd;
                WindCurve = new wLine(new wPoint(PtA.X, PtA.Y, PtA.Z), new wPoint(PtB.X, PtB.Y, PtB.Z));
            }
            else if (RhinoCurve.TryGetPolyline(out P))
            {
                WindCurve = new wPolyline(RhPlineToWindPoints(P), P.IsClosed);
            }
            else
            {
                WindCurve = ToPiecewiseBezier(RhinoCurve);
            }
        }

        public List<wPoint> RhPlineToWindPoints(Polyline Pline)
        {
            List<wPoint> wPointSet = new List<wPoint>();

            for(int i = 0; i<Pline.Count;i++)
            {
                wPointSet.Add(new wPoint(Pline[0].X, Pline[0].Y, Pline[0].Z));
            }

            return wPointSet;
        }

        public wCurve ToPiecewiseBezier(Curve RhinoCurve)
        {
            NurbsCurve N = RhinoCurve.ToNurbsCurve();
            N.MakePiecewiseBezier(true);
            BezierCurve[] B = BezierCurve.CreateCubicBeziers(N, 0, 0);

            Point3d PtA = B[0].GetControlVertex3d(0);
            Point3d PtB = B[0].GetControlVertex3d(1);
            Point3d PtC = B[0].GetControlVertex3d(2);
            Point3d PtD = B[0].GetControlVertex3d(3);

            wBezierSpline pCurve = new wBezierSpline(new wPoint(PtA.X, PtA.Y, PtA.Z), new wPoint(PtB.X, PtB.Y, PtB.Z), new wPoint(PtC.X, PtC.Y, PtC.Z), new wPoint(PtD.X, PtD.Y, PtD.Z));
            for (int i = 1; i < B.Count(); i++)
            {
                PtA = B[i].GetControlVertex3d(0);
                PtB = B[i].GetControlVertex3d(1);
                PtC = B[i].GetControlVertex3d(2);
                PtD = B[i].GetControlVertex3d(3);
                pCurve.AddSpan(new wPoint(PtA.X, PtA.Y, PtA.Z), new wPoint(PtB.X, PtB.Y, PtB.Z), new wPoint(PtC.X, PtC.Y, PtC.Z), new wPoint(PtD.X, PtD.Y, PtD.Z));
            }
            WindCurve = pCurve;

            return WindCurve;
        }

    }
}
