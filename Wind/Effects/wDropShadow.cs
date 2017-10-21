using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using Wind.Types;

namespace Wind.Effects
{
    public class wDropShadow
    {

        public DropShadowEffect ShapeEffect = new DropShadowEffect();

        public double Radius = 1;
        public wColor Color = new wColor().Black();
        public double Direction = 315.0;
        public double Distance = 2.0;
        public double Opacity = 0.75;

        public wDropShadow()
        {
            SetEffect();
        }

        public wDropShadow(wColor ShadowColor, double Rotation, double Offset, double BlurRadius, double ShadowOpacity)
        {

            Color = ShadowColor;
            Direction = Rotation;
            Distance = Offset;
            Radius = BlurRadius;
            Opacity = ShadowOpacity;

            SetEffect();
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
