using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Effects;
using Wind.Types;

namespace Hoopoe.SVG.Graphics.Effects
{
    public class hDropShadow : hEffect
    {

        public double Radius = 1;
        public double OffsetX = 1;
        public double OffsetY = 1;
        public wColor FillColor = new wColor();

        public hDropShadow()
        {

        }

        public hDropShadow(wDropShadow Effect)
        {
            FillColor = Effect.Color;

            Radius = Effect.Radius;

            OffsetX = Effect.OffsetX;
            OffsetY = Effect.OffsetY;

            SetValue();
        }

        public hDropShadow(wDropShadow Effect, string SourceID, string ResultID)
        {
            FillColor = Effect.Color;

            Radius = Effect.Radius;

            OffsetX = Effect.OffsetX;
            OffsetY = Effect.OffsetY;

            InputID = SourceID;
            OutputID = ResultID;

            SetValue();
        }

        public void SetValue()
        {
            StringBuilder filter = new StringBuilder();

            filter.Append("<feOffset in=\"" + InputID + "\" result=\"offOut\" dx=\"" + OffsetX + "\" dy=\"" + OffsetY + "\" /> " + Environment.NewLine);
            //filter.Append("<feFlood int=\"offOut\" flood-color=\"rgb(" + FillColor.R + "," + FillColor.G + "," + FillColor.B + ")\" flood-opacity=\"" + FillColor.A / 255.0 + "\" result=\"colorOut\" />");
            filter.Append("<feGaussianBlur in=\"colorOut\" result=\"blurOut\" stdDeviation=\"" + Radius + "\" /> " + Environment.NewLine);
            filter.Append("<feBlend in=\"" + InputID + "\" in2=\"blurOut\" result=\"" + OutputID + "\" mode=\"normal\" /> ");

            Value = filter.ToString();
        }

    }
}
