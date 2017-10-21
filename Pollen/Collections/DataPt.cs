using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Containers;
using Wind.Geometry.Vectors;
using Wind.Types;

namespace Pollen.Collections
{
    public class DataPt
    {
        public string Tag = "";
        public string Name = "";

        public int Type = 0;
        public int Format = 0;

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
        public wGraphic Graphics = new wGraphic(new wColor().LightGray(),new wColor().Gray(), new wColor().DarkGray(), 1.0,1.0,1.0);
        public wFont Fonts = new wFont("Arial", 8, new wColor().Gray(),0,true,false,false,false);
        public wSize Sizes = new wSize(1.0,1.0);

        //Sets | Marker Properties
        public wColor MarkerColor = new wColor().Black();
        public int MarkerSize = 10;
        public int MarkerMode = 0;

        //Sets | Label Properties
        public string Label = "";
        public int LabelMode;
        
        public DataPt()
        {
        }

        public void SetMarker(int MarkMode, int MarkSize, wColor MarkColor)
        {
            if (MarkMode == 0) { HasMarker = false; } else { HasMarker = true; }
            MarkerMode = MarkMode;
            MarkerSize = MarkSize;
            MarkerColor = MarkColor;
        }
        

        public DataPt(object Data)
        {
            Value = Data;
            Type = 0;
        }
        
    }
}
