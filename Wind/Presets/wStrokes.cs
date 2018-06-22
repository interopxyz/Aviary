using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Containers;
using Wind.Types;

namespace Wind.Presets
{
    public class wStrokes
    {
        public wGraphic Graphic = new wGraphic();

        public enum StrokeTypes { Default, Transparent, OffWhiteSolid, VeryLightGraySolid, LineChart }

        public StrokeTypes StrokeType = StrokeTypes.Default;

        public wStrokes()
        {

        }

        public wStrokes(wGraphic SetGraphic, StrokeTypes SetStrokeType)
        {
            Graphic = SetGraphic;
            StrokeType = SetStrokeType;

            switch (SetStrokeType)
            {
                default:
                    SetDefault();
                    break;
                case StrokeTypes.Transparent:
                    SetTransparent();
                    break;
                case StrokeTypes.OffWhiteSolid:
                    SetOffWhite();
                    break;
                case StrokeTypes.VeryLightGraySolid:
                    SetVeryLightGray();
                    break;
                case StrokeTypes.LineChart:
                    SetLineChart();
                    break;
            }
        }

        public wGraphic GetGraphic()
        {
            return Graphic;
        }


        //#########################################################################


        private void SetDefault()
        {
            Graphic.StrokeColor = wColors.Black;
            Graphic.SetUniformStrokeWeight(1);
        }

        private void SetTransparent()
        {
            Graphic.StrokeColor = wColors.Transparent;
            Graphic.SetUniformStrokeWeight(0);
        }
        
        private void SetLineChart()
        {
            Graphic.StrokeColor = wColors.OffWhite;
            Graphic.SetUniformStrokeWeight(2);
        }

        private void SetOffWhite()
        {
            Graphic.StrokeColor = wColors.OffWhite;
            Graphic.SetUniformStrokeWeight(1);
        }

        private void SetVeryLightGray()
        {
            Graphic.StrokeColor = wColors.VeryLightGray;
            Graphic.SetUniformStrokeWeight(1);
        }



    }
}