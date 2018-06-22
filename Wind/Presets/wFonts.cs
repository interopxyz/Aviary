using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Wind.Presets
{
    public static class wFonts
    {
        //Standard Fonts
        public static wFont Regular = new wFont("Regular");
        public static wFont Transparent = new wFont("Transparent","Arial", 8, wColors.Transparent);

        //Chart Point Fonts
        public static wFont ChartPoint = new wFont("ChartPoint","Arial",8,new wColor(wColors.White, 230),true);
        public static wFont ChartPointDark = new wFont("ChartPointDark","Arial", 8, new wColor(wColors.Gray, 230));
        public static wFont ChartGauge = new wFont("ChartGauge","Calibri", 8, wColors.Gray, true);

        //Data Grid Fonts
        public static wFont AxisLabel = new wFont("AxisLabels","Calibri", 8, wColors.WashedGray);
        public static wFont DataGrid = new wFont("DataGrid","Calibri", 10, wColors.Gray);
        public static wFont DataGridTitle = new wFont("DataGridTitle","Arial", 10, wColors.Gray, wFontBase.Justification.BottomCenter, true, false, false, false);
        
        //Labels
        public static wFont Bold = new wFont("Bold","Arial", 24, wColors.Gray, true);
        public static wFont Title = new wFont("Title","Arial", 24, wColors.DarkGray);
        public static wFont SubTitle = new wFont("SubTitle","Arial", 14, wColors.Charcoal);
        public static wFont Text = new wFont("Text","Calibri", 12, wColors.VeryDarkGray);
        public static wFont Subtext = new wFont("Subtext","Calibri Light", 10.5, wColors.VeryDarkGray);
    }

}
