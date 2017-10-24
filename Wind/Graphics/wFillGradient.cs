using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            GrdBrush.MappingMode = BrushMappingMode.RelativeToBoundingBox;
            GrdBrush.SpreadMethod = GradientSpreadMethod.Reflect;

            switch((GradientMode)SetGradientMode)
            {
                case GradientMode.Linear:
                    GrdBrush = new LinearGradientBrush(gCollection);
                break;
                case GradientMode.Radial:
                    GrdBrush = new RadialGradientBrush(gCollection);
                    break;
                default:
                    GrdBrush = new LinearGradientBrush(gCollection);
                    break;

            }


        }


    }
}
