using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Wind.Presets
{
    public class wFonts
    {
        public wFont Font = new wFont();
        public enum FontTypes { Regular, ChartPoint, ChartPointDark, ChartGauge, DataGrid, DataGridTitles, AxisLabel, Transparent }
        public FontTypes FontType = FontTypes.Regular;

        public wFonts()
        {

        }


        public wFonts(FontTypes SetFontType)
        {
            FontType = SetFontType;

            switch (SetFontType)
            {
                default:
                    SetRegular();
                    break;
                case FontTypes.ChartPoint:
                    SetChartPoint();
                    break;
                case FontTypes.Transparent:
                    SetTransparent();
                    break;
                case FontTypes.ChartPointDark:
                    SetChartPointDark();
                    break;
                case FontTypes.ChartGauge:
                    SetChartGauge();
                    break;
                case FontTypes.DataGrid:
                    SetDataGrid();
                    break;
                case FontTypes.DataGridTitles:
                    SetDataGridTitles();
                    break;
                case FontTypes.AxisLabel:
                    SetAxisLabels();
                    break;
            }
        }

        private void SetRegular()
        {
            Font = new wFont();
        }

        private void SetChartPoint()
        {
            Font = new wFont("Arial", 8, new wColors().White());
            Font.FontColor.A = 230;
            Font.IsBold = true;
        }

        private void SetTransparent()
        {
            Font = new wFont("Arial", 8, new wColors().Transparent());
        }

        private void SetChartPointDark()
        {
            Font = new wFont("Arial", 8, new wColors().Gray());
            Font.FontColor.A = 230;
        }

        private void SetChartGauge()
        {
            Font = new wFont("Calibri", 8, new wColors().Gray());
            Font.IsBold = true;
        }

        private void SetAxisLabels()
        {
            Font = new wFont("Calibri", 8, new wColors().WashedGray());
        }

        private void SetDataGrid()
        {
            Font = new wFont("Calibri", 10, new wColors().Gray());
        }

        private void SetDataGridTitles()
        {
            Font = new wFont("Arial", 10, new wColors().Gray());
            Font.Justify = wFontBase.Justification.BottomCenter;
            Font.IsBold = true;
        }

    }
}
