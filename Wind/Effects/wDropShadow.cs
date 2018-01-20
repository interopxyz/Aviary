using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using Wind.Presets;
using Wind.Types;

namespace Wind.Effects
{
    public class wDropShadow : wEffect
    {

        public DropShadowEffect ShapeEffect = new DropShadowEffect();

        public double Radius = 1;
        public wColor Color = new wColors().Black();
        public double Direction = 315.0;
        public double Distance = 2.0;
        public double Opacity = 0.75;

        public double OffsetX = 0;
        public double OffsetY = 0;

        public wDropShadow()
        {
            CalculateOffset();

            SetEffect();
        }
        

        public wDropShadow(wColor ShadowColor, double Rotation, double Offset, double BlurRadius, double ShadowOpacity)
        {
            Active = true;
            Color = ShadowColor;
            Direction = Rotation;
            Distance = Offset;
            Radius = BlurRadius;
            Opacity = ShadowOpacity;

            CalculateOffset();

            SetEffect();
        }

        public void CalculateOffset()
        {
            OffsetX = Distance * Math.Sin(Direction / 180 * Math.PI);
            OffsetY = Distance * Math.Cos(Direction / 180 * Math.PI);
        }

        public void SetEffect()
        {
            ShapeEffect.Color = Color.ToMediaColor();
            ShapeEffect.Direction = Direction;
            ShapeEffect.ShadowDepth = Distance;
            ShapeEffect.BlurRadius = Radius;
            ShapeEffect.Opacity = Opacity;
        }

    }
}
