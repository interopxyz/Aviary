using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Containers;

using Pollen.Collections;
using Pollen.Utilities;

namespace Pollen.Charts
{
    public class pChart
    {
        public DataSetCollection DataSet = new DataSetCollection();
        public wGraphic Graphics = new wGraphic();
        public string Type = "Chart";
        public p3D View = new p3D();

        public enum ChartTypes { Point, Bar, Column, Line, StepLine, Spline, Area, SplineArea, Pie, Doughnut, Polar, Radar, Funnel, Pyramid,Range,RangeColumn,RangeBar,RangeBubble};

        
        public virtual void SetThreeDView()
        {

        }

        public virtual void SetToolTip()
        {

        }

        public virtual void SetAxisAppearance()
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

        public virtual void SetCorners()
        {

        }


        public virtual void SetSize()
        {

        }

        public virtual void SetMargin()
        {

        }

        public virtual void SetPadding()
        {

        }

    }
}
