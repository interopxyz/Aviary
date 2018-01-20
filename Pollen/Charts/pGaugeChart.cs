using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xaml;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms.Integration;

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;

using Wind.Containers;

using Pollen.Collections;
using System.Windows.Media;
using Wind.Types;
using Wind.Presets;

namespace Pollen.Charts
{
    public class pGaugeChart : pChart
    {
        public Gauge Element;
        public List<pPointSeries> ChartSeriesSet;
        public bool Status;
        public DataSetCollection DataGrid = new DataSetCollection();

        public pGaugeChart(string InstanceName)
        {
            //Set Element info setup
            Element = new Gauge();
            Element.DisableAnimations = false;
            Element.Name = InstanceName;
            Type = "GaugeChart";

        }

        public void SetProperties(DataSetCollection PollenDataGrid, int ControlSize)
        {
            //Set unique properties of the control
            DataGrid = PollenDataGrid;

            Element.Width = ControlSize;
            Element.Height = ControlSize;

            Element.Margin = new System.Windows.Thickness(10);

            Element.DisableAnimations = true;
        }
        
        public void SetGaugeType(int Mode)
        {
            switch (Mode)
            {
                case 0:
                    Element.Uses360Mode = true;
                    break;
                case 1:
                    Element.Uses360Mode = false;
                    break;
                case 2:
                    Element.Uses360Mode = true;
                    Element.InnerRadius = 1;
                    Element.HighFontSize = 32;
                    break;
                case 3:
                    Element.Uses360Mode = false;
                    Element.InnerRadius = 1;
                    Element.HighFontSize = 32;
                    break;
            }
        }

        public void SetGaugeValues(DataPt PollenDataPoint, int Mode, double Min, double Max, double Sum)
        {
            wGraphic G = PollenDataPoint.Graphics;
            Element.GaugeBackground = new SolidColorBrush(Color.FromArgb(100,245,245,245));
            Element.GaugeActiveFill = PollenDataPoint.Graphics.GetBackgroundBrush();
            
            Element.Stroke = G.GetStrokeBrush();
            Element.StrokeThickness = G.StrokeWeight[0];
            
            Element.Foreground = G.FontObject.GetFontBrush();
            

            switch (Mode)
            {
                default:
                    Element.From = 0;
                    Element.To = 100;
                    Element.Value = SetSigDigits((PollenDataPoint.Number/Sum*100),3);
                break;
                case 1:
                    Element.From = 0;
                    Element.To = SetSigDigits(Sum,3);
                    Element.Value = SetSigDigits(PollenDataPoint.Number,3);
                    break;
                case 2:
                    Element.From = SetSigDigits(Min,3);
                    Element.To = SetSigDigits(Max,3);
                    Element.Value = SetSigDigits((PollenDataPoint.Number),3);
                    break;
                case 3:
                    Element.From = 0;
                    Element.To = 100;
                    Element.Value = SetSigDigits((PollenDataPoint.Number-Min)/(Max-Min)*100,3);
                    break;
            }
        }

        public double SetSigDigits(double Number, int Digits)
        {
            int N;

            N = (int)Math.Pow(10, (double)Digits);
            return Math.Truncate(Number * N) / N;
        }
        
        public override void SetSolidFill()
        {
            Element.Background = DataSet.Graphics.GetBackgroundBrush();
        }

        public void SetAxisAppearance()
        {
        }

        public void SetAxisScale()
        {

        }

        public void SetCorners(wGraphic Graphic)
        {
        }

        public void SetFont(wGraphic Graphic)
        {
            wGraphic G = DataGrid.Sets[0].Points[0].Graphics;
            Element.Foreground = G.GetFontBrush();
            Element.FontFamily = G.FontObject.ToMediaFont().Family;
            Element.FontSize = G.FontObject.Size;
            Element.HighFontSize = G.FontObject.Size;
            Element.FontStyle = G.FontObject.ToMediaFont().Italic;
            Element.FontWeight = G.FontObject.ToMediaFont().Bold;

        }
    }
}