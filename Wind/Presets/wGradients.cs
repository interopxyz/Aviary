using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Wind.Presets
{
    public class wGradients
    {
        public List<double> tValues = new List<double>{ 0.0, 1.0 };
        public List<wColor> Colors = new List<wColor> { new wColors().LightGray(), new wColors().DarkGray() };

        private wGradient Gradient = new wGradient();

        public enum GradientTypes { Transparent, SolidColor, GreyScale,Metro,Ecotect, SolidLightGray, SolidDarkGray}
        public GradientTypes GradientType = GradientTypes.GreyScale;

        public wGradients()
        {

        }

        public wGradients(GradientTypes SetGradientType)
        {
            GradientType = SetGradientType;

            switch (SetGradientType)
            {
                default:
                    SetGrayscale();
                    break;
                case GradientTypes.Transparent:
                    SetTransparent();
                    break;
                case GradientTypes.SolidColor:
                    SetDefaultSolid();
                    break;
                case GradientTypes.SolidLightGray:
                    SetSolidLightGray();
                    break;
                case GradientTypes.SolidDarkGray:
                    SetSolidDarkGray();
                    break;
                case GradientTypes.Metro:
                    SetMetro();
                    break;
                case GradientTypes.Ecotect:
                    SetEcotect();
                    break;
            }
        }

        public wGradient GetGradient()
        {
            Gradient = new wGradient(Colors, tValues);

            return Gradient;
        }

        public void SetSolidColor(wColor SolidColor)
        {
            GradientType = GradientTypes.SolidColor;

            double[] T = { 0.0, 1.0 };
            tValues = T.ToList();

            Colors.Clear();
            Colors.Add(SolidColor);
            Colors.Add(SolidColor);

            Gradient = new wGradient(Colors, tValues);
        }

        //#########################################################################

        private void SetDefaultSolid()
        {
            double[] T = { 0.0, 1.0 };
            tValues = T.ToList();

            Colors.Clear();
            Colors.Add(new wColors().Gray());
            Colors.Add(new wColors().Gray());

            Gradient = new wGradient(Colors, tValues);
        }

        private void SetTransparent()
        {
            double[] T = { 0.0, 1.0 };
            tValues = T.ToList();

            Colors.Clear();
            Colors.Add(new wColors().Transparent());
            Colors.Add(new wColors().Transparent());

            Gradient = new wGradient(Colors, tValues);
        }

        private void SetSolidLightGray()
        {
            double[] T = { 0.0, 1.0 };
            tValues = T.ToList();

            Colors.Clear();
            Colors.Add(new wColors().VeryLightGray());
            Colors.Add(new wColors().VeryLightGray());

            Gradient = new wGradient(Colors, tValues);
        }

        private void SetSolidDarkGray()
        {
            double[] T = { 0.0, 1.0 };
            tValues = T.ToList();

            Colors.Clear();
            Colors.Add(new wColors().DarkGray());
            Colors.Add(new wColors().DarkGray());

            Gradient = new wGradient(Colors, tValues);
        }
        

        private void SetGrayscale()
        {
            double[] T = { 0.0, 1.0 };
            tValues = T.ToList();

            Colors.Clear();
            Colors.Add(new wColors().LightGray());
            Colors.Add(new wColors().DarkGray());

            Gradient = new wGradient(Colors, tValues);
        }

        private void SetMetro()
        {
            double[] T = { 0.0, 0.142857, 0.285714, 0.428571, 0.571429, 0.714286, 0.857143, 1.0 };
            tValues = T.ToList();

            Colors.Clear();
            Colors.Add(new wColor(255, 60, 162, 222));
            Colors.Add(new wColor(255, 52, 140, 191));
            Colors.Add(new wColor(255, 75,163,153));
            Colors.Add(new wColor(255, 84,196,163));
            Colors.Add(new wColor(255, 247,211,104));
            Colors.Add(new wColor(255, 250,160,95));
            Colors.Add(new wColor(255, 204,81,65));
            Colors.Add(new wColor(255, 235,93,80));

            Gradient = new wGradient(Colors, tValues);
        }

        private void SetEcotect()
        {
            double[] T = { 0.0, 0.5, 1.0 };
            tValues = T.ToList();

            Colors.Clear();
            Colors.Add(new wColors().Blue());
            Colors.Add(new wColors().Red());
            Colors.Add(new wColors().Yellow());

            Gradient = new wGradient(Colors, tValues);
        }

    }
}
