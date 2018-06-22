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

        public bool Enabled = false;
        public bool HasGrid = false;
        public bool HasLabel = true;

        public int MajorSpacing = 0;
        public int MinorSpacing = 0;
        
        public wDomain Domain = new wDomain(0, 0);
        
        public double Angle = 0;
        

        public wAxis()
        {

        }

        public wAxis(bool IsEnabled)
        {
            Enabled = IsEnabled;
        }

        public wAxis(int Spacing, wDomain Bounds, bool HasAxisLabel, double TextAngle)
        {
            Enabled = Spacing > 0;
            HasGrid = Spacing > 1;
            if (Enabled) { MajorSpacing = 1; } else { MajorSpacing = 0; }
            if (HasGrid) { MinorSpacing = Spacing; } else { MinorSpacing = 0; }

            Domain = Bounds;
            
            HasLabel = HasAxisLabel;
            Angle = TextAngle;
        }

        public void SetAxisProperties(int Spacing, wDomain Bounds, bool HasAxisLabel, double TextAngle)
        {
            Enabled = Spacing > 0;
            HasGrid = Spacing > 1;
            if (Enabled) { MajorSpacing = 1; } else { MajorSpacing = 0; }
            if (HasGrid) { MinorSpacing = Spacing; } else { MinorSpacing = 0; }

            Domain = Bounds;

            HasLabel = HasAxisLabel;
            Angle = TextAngle;
        }

    }
}
