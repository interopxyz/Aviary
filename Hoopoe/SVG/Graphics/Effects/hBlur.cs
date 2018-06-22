using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Effects;

namespace Hoopoe.SVG.Graphics.Effects
{
    public class hBlur : hEffect
    {

        public double Radius = 1;

        public hBlur()
        {

        }

        public hBlur(wBlur Effect)
        {
            Radius = Effect.Radius;
            SetValue();
        }

        public hBlur(wBlur Effect, string SourceID, string ResultID)
        {
            Radius = Effect.Radius;

            InputID = SourceID;
            OutputID = ResultID;

            SetValue();
        }

        public void SetValue()
        {
            Value = "<feGaussianBlur in=\"" + InputID + "\" result=\"" + OutputID + "\" stdDeviation=\"" + Radius + "\"/> ";
        }

    }
}
