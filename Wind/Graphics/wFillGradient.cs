using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Wind.Types;

namespace Wind.Graphics
{
    public class wFillGradient : wFill
    {
        public enum GradientMode { Linear, Radial};

        public wFillGradient()
        {

        }

        public wFillGradient(wGradient WindGradient, int SetGradientMode)
        {
            GradientStopCollection gCollection = new GradientStopCollection();
            for(int i = 0; i<WindGradient.ColorSet.Count;i++)
            { 
             gCollection.Add(new GradientStop(WindGradient.ColorSet[i].ToMediaColor(), WindGradient.ParameterSet[i]));
            }
            
            double R = WindGradient.Angle / 180 * Math.PI;
            double XA = Math.Round(50 + Math.Sin(R) * 50) / 100.0;
            double YA = Math.Round(50 + Math.Cos(R) * 50) / 100.0;
            double XB = Math.Round(50 + Math.Sin(R + Math.PI) * 50) / 100.0;
            double YB = Math.Round(50 + Math.Cos(R + Math.PI) * 50) / 100.0;
            
            switch ((GradientMode)SetGradientMode)
            {
                case GradientMode.Radial:
                    RadialGradientBrush RBrush = new RadialGradientBrush(gCollection);
                    RBrush.MappingMode = BrushMappingMode.RelativeToBoundingBox;
                    RBrush.SpreadMethod = GradientSpreadMethod.Pad;

                    RBrush.GradientOrigin = new Point(1-WindGradient.Location.T0, 1-WindGradient.Location.T1);
                    RBrush.Center = new Point(1-WindGradient.Focus.T0, 1-WindGradient.Focus.T1);
                    RBrush.RadiusX = WindGradient.Radius;
                    RBrush.RadiusY = WindGradient.Radius;

                    GrdBrush = RBrush;
                    break;
                default:
                    LinearGradientBrush LBrush = new LinearGradientBrush(gCollection);
                    LBrush.MappingMode = BrushMappingMode.RelativeToBoundingBox;
                    LBrush.SpreadMethod = GradientSpreadMethod.Pad;

                    LBrush.StartPoint = new Point(XA, YA);
                    LBrush.EndPoint = new Point(XB, YB);

                    GrdBrush = LBrush;
                    break;
            }
            
        }


    }
}
