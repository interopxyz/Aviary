using Hoopoe.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.Assembly
{
    public class hShape
    {
        
        public StringBuilder svgShape = new StringBuilder();
        public hCurve Curve = null;

        public hShape()
        {

        }

        private void SetFrame()
        {
            svgShape.Append(" \"");
            svgShape.Append(Curve.Curve.ToString());
            svgShape.Append(" \"" + Environment.NewLine);
        }

        public hShape(hCurve HoopoeCurve)
        {
            Curve = HoopoeCurve;
            SetFrame();
        }

        public void ClearAttributes()
        {
            svgShape.Clear();
            SetFrame();
        }

        public void AddAttribute(string Attribute)
        {
            svgShape.Append(Attribute + " " + Environment.NewLine);
        }

    }
}
