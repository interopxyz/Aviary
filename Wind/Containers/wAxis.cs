using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Wind.Containers
{
    public class wAxis
    {

        public bool HasXAxis = false;
        public bool HasYAxis = true;
        
        public bool HasXGrid = false;
        public bool HasYGrid = false;

        public int XGridSpacing = 1;
        public int YGridSpacing = 1;

        public wDomain DomainX = new wDomain(0, 0);
        public wDomain DomainY = new wDomain(0, 0);

        public bool HasXLabel = true;
        public bool HasYLabel = true;

        public double XAngle = 0;
        public double YAngle = 0;
        

        public wAxis()
        {

        }

        public wAxis(bool AxisX, bool AxisY)
        {
            HasXAxis = AxisX;
            HasYAxis = AxisY;
        }

        public void SetXAxis(int Spacing, wDomain Bounds, bool HasLabel, double Angle)
        {
            HasXAxis = Spacing > 0;
            HasXGrid = Spacing > 1;

            DomainX = Bounds;

            XGridSpacing = Spacing;

            HasXLabel = HasLabel;
            XAngle = Angle;
        }

        public void SetYAxis(int Spacing, wDomain Bounds, bool HasLabel, double Angle)
        {
            HasYAxis = Spacing > 0;
            HasYGrid = Spacing > 1;

            DomainY = Bounds;

            YGridSpacing = Spacing;

            HasYLabel = HasLabel;
            YAngle = Angle;
        }

        public void SetXYAxes(int Spacing, wDomain Bounds, bool HasLabel, double Angle)
        {
            HasXAxis = Spacing > 0;
            HasYAxis = Spacing > 0;
            HasXGrid = Spacing > 1;
            HasYGrid = Spacing > 1;

            DomainX = Bounds;
            DomainY = Bounds;

            XGridSpacing = Spacing;
            YGridSpacing = Spacing;

            HasXLabel = HasLabel;
            HasYLabel = HasLabel;
            XAngle = Angle;
            YAngle = Angle;
        }
    }
}
