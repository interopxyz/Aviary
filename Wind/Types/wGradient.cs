using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Wind.Types
{
    public class wGradient
    {
        public List<wColor> ColorSet = new List<wColor>();
        public List<double> ParameterSet = new List<double>();
        public int Type = 0;
        public double Angle = 0;
        public bool IsInverted = false;
        public enum GradientSpace { Local, Global }
        public GradientSpace FillMode = GradientSpace.Local;

        public enum GradientMode { Linear, Radial}
        public GradientMode Mode = GradientMode.Linear;

        public double Radius = 1.0;
        public wDomain Location = new wDomain(0.5, 0.5);
        public wDomain Focus = new wDomain(0.5, 0.5);

        GradientStopCollection MediaGradient = new GradientStopCollection();

        public wGradient()
        {
            ColorSet.Add(new wColor().Black());
            ColorSet.Add(new wColor().White());
            ParameterSet.Add(0);
            ParameterSet.Add(1);
        }

        public wGradient(List<wColor> GradientColors)
        {
            ColorSet = GradientColors;

            ParameterSet.Clear();
            for (int i = 0; i < GradientColors.Count; i++)
            {
                ParameterSet.Add((1.0 / GradientColors.Count) * (double)i);
            }
        }

        public wGradient(List<System.Drawing.Color> GradientColors)
        {
            ColorSet.Clear();

            ParameterSet.Clear();
            for (int i = 0; i< GradientColors.Count; i++)
            {
                ColorSet.Add(new wColor(GradientColors[i]));
                ParameterSet.Add((1.0 / GradientColors.Count) * (double)i);
            }

        }

        public wGradient(List<System.Drawing.Color> GradientColors, double RotationAngle)
        {
            ColorSet.Clear();

            ParameterSet.Clear();
            for (int i = 0; i < GradientColors.Count; i++)
            {
                ColorSet.Add(new wColor(GradientColors[i]));
                ParameterSet.Add((1.0 / GradientColors.Count) * (double)i);
            }

            Angle = RotationAngle;

        }

        public wGradient(List<System.Drawing.Color> GradientColors, wDomain GradientLocation, wDomain GradientFocus,  double GradientRadius)
        {
            ColorSet.Clear();

            ParameterSet.Clear();
            for (int i = 0; i < GradientColors.Count; i++)
            {
                ColorSet.Add(new wColor(GradientColors[i]));
                ParameterSet.Add((1.0 / GradientColors.Count) * (double)i);
            }

            Radius = GradientRadius;
            Location = GradientLocation;
            Focus = GradientFocus;
            Mode = GradientMode.Radial;
        }

        public wGradient(List<System.Drawing.Color> GradientColors, wDomain GradientLocation, wDomain GradientFocus, double GradientRadius, GradientSpace Extents)
        {
            ColorSet.Clear();

            ParameterSet.Clear();
            for (int i = 0; i < GradientColors.Count; i++)
            {
                ColorSet.Add(new wColor(GradientColors[i]));
                ParameterSet.Add((1.0 / GradientColors.Count) * (double)i);
            }

            Radius = GradientRadius;
            Location = GradientLocation;
            Focus = GradientFocus;
            FillMode = Extents;

            Mode = GradientMode.Radial;
        }

        public wGradient(List<System.Drawing.Color> GradientColors, double RotationAngle, GradientSpace Extents)
        {
            ColorSet.Clear();

            ParameterSet.Clear();
            for (int i = 0; i < GradientColors.Count; i++)
            {
                ColorSet.Add(new wColor(GradientColors[i]));
                ParameterSet.Add((1.0 / GradientColors.Count) * (double)i);
            }

            Angle = RotationAngle;
            FillMode = Extents;

        }

        public wGradient(List<System.Windows.Media.Color> GradientColors)
        {
            ColorSet.Clear();

            ParameterSet.Clear();
            for (int i = 0; i < GradientColors.Count; i++)
            {
                ColorSet.Add(new wColor(GradientColors[i]));
                ParameterSet.Add((1.0 / GradientColors.Count) * (double)i);
            }
        }

        public wGradient(List<wColor> GradientColors,List<double> GradientParameters)
        {
            ParameterSet = GradientParameters;
            ColorSet = GradientColors;
        }

        public wGradient(List<wColor> GradientColors, List<double> GradientParameters, bool Invert)
        {
            IsInverted = Invert;
            ParameterSet = GradientParameters;
            ColorSet = GradientColors;
        }

        public wGradient(List<System.Drawing.Color> GradientColors, List<double> GradientParameters)
        {
            ColorSet.Clear();
            ParameterSet = GradientParameters;

            for (int i = 0; i < GradientColors.Count; i++)
            {
                ColorSet.Add(new wColor(GradientColors[i]));
            }

        }

        public wGradient(List<System.Windows.Media.Color> GradientColors, List<double> GradientParameters)
        {
            ColorSet.Clear();
            ParameterSet = GradientParameters;

            for (int i = 0; i < GradientColors.Count; i++)
            {
                ColorSet.Add(new wColor(GradientColors[i]));
            }
        }

        public wGradient(List<System.Drawing.Color> GradientColors, List<double> GradientParameters, double RotationAngle)
        {
            ColorSet.Clear();
            ParameterSet = GradientParameters;

            for (int i = 0; i < GradientColors.Count; i++)
            {
                ColorSet.Add(new wColor(GradientColors[i]));
            }

            Angle = RotationAngle;

        }

        public wGradient(List<System.Drawing.Color> GradientColors, List<double> GradientParameters, wDomain GradientLocation, wDomain GradientFocus, double GradientRadius)
        {
            ColorSet.Clear();
            ParameterSet = GradientParameters;

            for (int i = 0; i < GradientColors.Count; i++)
            {
                ColorSet.Add(new wColor(GradientColors[i]));
            }

            Radius = GradientRadius;
            Location = GradientLocation;
            Focus = GradientFocus;
            Mode = GradientMode.Radial;

        }

        public wGradient(List<System.Drawing.Color> GradientColors, List<double> GradientParameters, wDomain GradientLocation, wDomain GradientFocus, double GradientRadius, GradientSpace Extents)
        {
            ColorSet.Clear();
            ParameterSet = GradientParameters;

            for (int i = 0; i < GradientColors.Count; i++)
            {
                ColorSet.Add(new wColor(GradientColors[i]));
            }

            Radius = GradientRadius;
            Location = GradientLocation;
            Focus = GradientFocus;
            FillMode = Extents;

            Mode = GradientMode.Radial;

        }

        public wGradient(List<System.Drawing.Color> GradientColors, List<double> GradientParameters, double RotationAngle, GradientSpace Extents)
        {
            ColorSet.Clear();
            ParameterSet = GradientParameters;

            for (int i = 0; i < GradientColors.Count; i++)
            {
                ColorSet.Add(new wColor(GradientColors[i]));
            }

            Angle = RotationAngle;
            FillMode = Extents;

        }

        public void SetParameters(List<double> GradientParameters)
        {
            ParameterSet = GradientParameters;
        }

        public void SetGradientType(int GradientType)
        {
            Type = GradientType;
        }

        public GradientStopCollection ToMediaGradient()
        {
            MediaGradient.Clear();
            int k = 0;
            if (IsInverted) { k = 1; }
            for (int i = 0; i < ColorSet.Count; i++)
            {
                MediaGradient.Add(new GradientStop(ColorSet[i].ToMediaColor(), Math.Abs(k- ParameterSet[i])));
            }
            
            return MediaGradient;
        }

}
}
