using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pollen.Utilities
{
    public class p3D
    {

        public int Pivot = 0;
        public int Tilt = 0;
        public int Distance = 0;
        public bool Is3D = false;

        public enum LightingMode { None, Simple, Realistic};
        public LightingMode Light = LightingMode.None;

        public p3D()
        {

        }

        public p3D(int PivotChart, int TiltChart)
        {
            Pivot = PivotChart;
            Tilt = TiltChart;
        }

        public p3D(int PivotChart, int TiltChart, int ViewDistance)
        {
            Pivot = PivotChart;
            Tilt = TiltChart;
            Distance = ViewDistance;

            if ((Pivot == 0) && (Tilt == 0) && (Distance == 0)) { Is3D = false; } else { Is3D = true; }

        }

        public p3D(int PivotChart, int TiltChart, int ViewDistance,bool IsChart3D, LightingMode ChartLightingMode)
        {
            Pivot = PivotChart;
            Tilt = TiltChart;
            Distance = ViewDistance;

            Is3D = IsChart3D;

            Light = ChartLightingMode;
        }

        public p3D(int PivotChart, int TiltChart, int ViewDistance, LightingMode ChartLightingMode)
        {
            Pivot = PivotChart;
            Tilt = TiltChart;
            Distance = ViewDistance;

            if ((Pivot ==0) && (Tilt == 0)&& (Distance== 0)) { Is3D = false; } else { Is3D = true; }
            
            Light = ChartLightingMode;
        }

    }
}
