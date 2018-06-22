using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Presets;
using Wind.Types;

namespace Wind.Containers
{
    public class wLabel
    {
        public bool Enabled = false;
        public string Content = "";
        public bool HasLeader = false;

        public string Format = "G";

        public enum LabelPosition { None, Bottom, BottomLeft, BottomRight, Center, Left, Right, Top, TopLeft, TopRight };
        public LabelPosition Position = LabelPosition.Center;

        public enum LabelAlignment { None, Center, Left, Right, Perp };
        public LabelAlignment Alignment = LabelAlignment.None;

        public wGraphic Graphics = new wGraphic(wColors.Transparent);
        public wFont Font = new wFont();

        public wLabel()
        {

        }

        public wLabel(LabelPosition NewPosition, LabelAlignment NewAlignment, wGraphic NewGraphic)
        {
            Enabled = true;
            Position = NewPosition;
            Alignment = NewAlignment;
            Graphics = NewGraphic;

        }
        public int GetPositionIndex()
        {
            int[] ConversionValue = { 256, 2, 64, 128, 256, 8, 4, 1, 16, 32 };
            return ConversionValue[(int)Position];
        }

        public string GetBarAlignment()
        {
            string[] ConversionValue = { "Outside", "Center", "Left", "Right" };
            return ConversionValue[(int)Alignment];
        }

        public string GetLabelAlignment()
        {
            string[] ConversionValue = { "Auto", "Center", "Left", "Right" };
            return ConversionValue[(int)Alignment];
        }

        public wLabel(string LabelContent,string LabelFormat)
        {
            Content = LabelContent;
            Format = LabelFormat;
        }

    }
}
