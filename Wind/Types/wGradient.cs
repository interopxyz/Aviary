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

            for (int i = 0; i < ColorSet.Count; i++)
            {
                MediaGradient.Add(new GradientStop(ColorSet[i].ToMediaColor(), ParameterSet[i]));
            }
            
            return MediaGradient;
        }

}
}
