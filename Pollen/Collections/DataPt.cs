using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Containers;
using Wind.Geometry.Vectors;
using Wind.Presets;
using Wind.Types;

namespace Pollen.Collections
{
    public class DataPt
    {
        public string Tag = "";
        public string Name = "";

        public int Type = 0;
        public string Format = "G";

        //Sets | Formatting Status
        public bool HasFont = false;
        public bool HasGraphics = false;
        public bool HasMarker = false;
        public bool HasTitle = false;
        public bool HasLabel = false;

        //Sets | Data
        public object Value = new object();
        public double Number = new double();
        public string Text = "";
        public int Integer = new int();
        public Tuple<double, double> Domain = new Tuple<double, double>(0.0,1.0);
        public wPoint Point = new wPoint();
        

        //Sets | Graphics, Fonts, Sizing
        public wGraphic Graphics = new wGraphic(new wColors().LightGray(),new wColors().Gray(), new wColors().DarkGray(), 1.0,1.0,1.0);
        public wFont Fonts = new wFont("Arial", 8, new wColors().Gray(),0,true,false,false,false);
        public wSize Sizes = new wSize(1.0,1.0);

        //Sets | Marker Properties
        public wMarker Marker = new wMarker();

        //Sets | Label Properties
        public wLabel Label = new wLabel();

        //Object | Status
        public int CustomMarkers = 0;
        public int CustomLabels = 0;


        public DataPt()
        {
        }
        
        public void SetMarker(wMarker.MarkerType MarkMode, int MarkSize, wColor MarkColor)
        {
            if (MarkMode == 0) { HasMarker = false; } else { HasMarker = true; }
            Marker.Mode = MarkMode;
            Marker.Radius = MarkSize;
            Marker.Graphics.Background = MarkColor;
        }

        public void SetLabel(wLabel NewLabel)
        {
            Label = NewLabel;
            CustomLabels += 1;
        }


        public void SetMarker(wMarker NewMarker)
        {
            Marker = NewMarker;
            CustomMarkers += 1;
        }

        public DataPt(object Data)
        {
            Value = Data;
            Type = 0;
        }
        
    }
}
