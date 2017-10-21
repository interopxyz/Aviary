using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Wind.Geometry.Vectors;

namespace Wind.Geometry.Curves
{
    public class wPolycurve : wCurve
    {
        public override string GetCurveType { get { return "Polycurve"; } }

        public List<wCurve> Segments = new List<wCurve>();

        public wPolycurve()
        {
        }

        public wPolycurve(List<wCurve> Curves)
        {

            IsClosed = Segments.Count != 1;
        }

        public void AddCurve(wCurve Curve)
        {

        }

    }
}
