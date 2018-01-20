using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Presets;

namespace Wind.Containers
{
    public class wLabel
    {
        public string Content = "";
        public bool HasLeader = false;

        public enum LabelPosition { None, Bottom, BottomLeft, BottomRight, Center, Left, Right, Top, TopLeft, TopRight };
        public LabelPosition Position = LabelPosition.Center;

        public enum LabelAlignment { None, Center, Left, Right };
        public LabelAlignment Alignment = LabelAlignment.Center;

        public wGraphic Graphics = new wGraphic(new wColors().Transparent());

        public wLabel()
        {

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

        public wLabel(string LabelContent)
        {
            Content = LabelContent;
        }

    }
}
