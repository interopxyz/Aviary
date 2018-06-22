using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Wind.Geometry.Vectors;

namespace Wind.Geometry.Curves
{
    abstract public class wCurve
    {
        public abstract string GetCurveType { get; }

        public List<wPoint> Points = new List<wPoint>();
        public List<int> Indices = new List<int>();

        public Rect Boundary = new Rect();

        public bool IsClosed = false;
        public bool IsSingle = true;

        public wCurve()
        {
        }

        public virtual void SetFont()
        {

        }

        public virtual void SetSolidFill()
        {

        }

        public virtual void SetPatternFill()
        {

        }

        public virtual void SetGradientFill()
        {

        }

        public virtual void SetStroke()
        {

        }

        public virtual void SetBlur()
        {

        }

        public bool IsConvex()
        {
            int count = Points.Count;

            bool isNegative = false;
            bool isPositive = false;
            bool result = false;
            int i, j, k;
            wVector Va, Vb, Vc;

            for (i = 0; i < count; i++)
            {
                j = (i + 1) % count;
                k = (j + 1) % count;

                Va = new wVector(Points[j], Points[i]);

                Vb = new wVector(Points[j], Points[k]);

                Vc = Va.GetCrossProduct(Vb);

                if (Vc.Z < 0)
                {
                    isNegative = true;
                }
                else if (Vc.Z > 0)
                {
                    isPositive = true;
                }
                result = (isNegative && isPositive);
                if (result) { break; }
            }

            return result;
        }

        public bool IsClockwise()
        {
            double V = 0;
            for (int i = 0; i < Points.Count - 1; i++)
            {
                V += ((Points[i + 1].X - Points[i].X) * (Points[i + 1].Y + Points[i].Y));
            }
            V += ((Points[0].X - Points[Points.Count - 1].X) * (Points[0].Y + Points[Points.Count - 1].Y));


            return (V > 0);

        }


        public bool IsPointInside(wPoint TestPoint, double Tolerance = 0.000001)
        {
            int count = Points.Count - 1;
            double sumAngle = new wVector(Points[count].X - TestPoint.X, Points[count].Y - TestPoint.Y, 0).GetAngle(new wVector(Points[0].X - TestPoint.X, Points[0].Y - TestPoint.Y, 0));

            for (int i = 0; i < count; i++)
            {
                sumAngle += new wVector(Points[i].X - TestPoint.X, Points[i].Y - TestPoint.Y, 0).GetAngle(new wVector(Points[i + 1].X - TestPoint.X, Points[i + 1].Y - TestPoint.Y, 0));
            }

            return (Math.Abs(sumAngle) > (0.000001));
        }

        
        public double GetArea()
        {

            double area = 0;
            for (int i = 0; i < Points.Count - 1; i++)
            {
                area += (Points[i + 1].X - Points[i].X) * (Points[i + 1].Y + Points[i].Y) / 2;
            }

            return Math.Abs(area);
        }

        public wPoint Get2dCentroid()
        {
            double Xs = 0;
            double Ys = 0;

            wVector Center = new wVector();

            for (int i = 0; i < Points.Count - 1; i++)
            {
                double f = Points[i].X * Points[i + 1].Y - Points[i + 1].X * Points[i].Y;
                Xs += (Points[i].X + Points[i + 1].X) * f;
                Ys += (Points[i].Y + Points[i + 1].Y) * f;
            }

            double area = GetArea();
            Xs /= (6 * area);
            Ys /= (6 * area);
            
                Xs = -Xs;
                Ys = -Ys;

            return new wPoint(Xs, Ys, 0);
        }

    }

}
