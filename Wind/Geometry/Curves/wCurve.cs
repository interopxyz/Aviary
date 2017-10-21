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

    }

}
