using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Presets;
using Wind.Types;

namespace Wind.Containers
{
    public class wMarker
    {
        public wGraphic Graphics = new wGraphic();
        public int Radius = 10;

        public enum MarkerType { None, Circle, Square, Diamond, Triangle, Cross }
        public MarkerType Mode = MarkerType.Circle;

        public wMarker()
        {

        }

        public wMarker(MarkerType SetMarkerType)
        {
            Mode = SetMarkerType;
        }

        public wMarker(MarkerType SetMarkerType, int MarkerRadius)
        {
            Mode = SetMarkerType;
            Radius = MarkerRadius;
        }

        public wMarker(MarkerType SetMarkerType, int MarkerRadius, wColor MarkerColor)
        {
            Mode = SetMarkerType;
            Radius = MarkerRadius;
            Graphics.Background = MarkerColor;
        }

        public wMarker(MarkerType SetMarkerType, int MarkerRadius, wGraphic MarkerGraphics)
        {
            Mode = SetMarkerType;
            Radius = MarkerRadius;
            Graphics = MarkerGraphics;
        }

    }
}
