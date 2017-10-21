using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Wind.Containers;
using Wind.Geometry.Curves;

namespace Wind.Geometry.Shapes
{
    public class wCompoundCurve
    {
        public string Type = "CompoundCurve";
        public Rect Boundary = new Rect();

        public List<wObject> Segments = new List<wObject>();

        public wCompoundCurve()
        {

        }

        public wCompoundCurve(wObject Segment)
        {
            Segments.Add(Segment);
        }

        public wCompoundCurve(List<wObject> SegmentList)
        {
            Segments.AddRange(SegmentList);
        }

        public void AddSegment(wObject Segment)
        {
            Segments.Add(Segment);
        }

        public void AddSegments(List<wObject> SegmentList)
        {
            Segments.AddRange(SegmentList);
        }

    }
}
