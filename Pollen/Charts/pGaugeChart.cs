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
using LiveCharts.WinForms;

using Wind.Containers;

using Pollen.Collections;

namespace Pollen.Charts
{
    public class pGaugeChart : pChart
    {
        public System.Windows.Controls.Panel Element;
        public WindowsFormsHost ChartHost;
        public SolidGauge ChartObject;
        public List<pPointSeries> ChartSeriesSet;
        public bool Status;
        public DataSetCollection DataGrid = new DataSetCollection();

        public pGaugeChart(string InstanceName)
        {
            //Set Element info setup
            Element = new WrapPanel();
            ChartHost = new WindowsFormsHost();
            ChartObject = new SolidGauge();
            ChartObject.DisableAnimations = false;
            ChartHost.Child = ChartObject;

            Element.Children.Add(ChartHost);
            Element.Name = InstanceName;
            Type = "GaugeChart";

        }

        public void SetProperties(DataSetCollection PollenDataGrid, int ControlSize)
        {
            //Set unique properties of the control
            DataGrid = PollenDataGrid;

            ChartHost.Width = ControlSize;
            ChartHost.Height = ControlSize;
            
            ChartObject.DisableAnimations = true;
        }
        
        public void SetGaugeType(int Mode)
        {
            switch (Mode)
            {
                case 0:
                    ChartObject.Uses360Mode = true;
                    break;
                case 1:
                    ChartObject.Uses360Mode = false;
                    break;
                case 2:
                    ChartObject.Uses360Mode = true;
                    ChartObject.InnerRadius = 1;
                    ChartObject.HighFontSize = 32;
                    break;
                case 3:
                    ChartObject.Uses360Mode = false;
                    ChartObject.InnerRadius = 1;
                    ChartObject.HighFontSize = 32;
                    break;
            }
        }

        public void SetGaugeValues(DataPt PollenDataPoint, int Mode, double Min, double Max, double Sum)
        {

            switch (Mode)
            {
                case 0:
                ChartObject.From = 0;
                ChartObject.To = 100;
                ChartObject.Value = SetSigDigits((PollenDataPoint.Number/Sum*100),3);
                break;
                case 1:
                    ChartObject.From = 0;
                    ChartObject.To = SetSigDigits(Sum,3);
                    ChartObject.Value = SetSigDigits(PollenDataPoint.Number,3);
                    break;
                case 2:
                    ChartObject.From = SetSigDigits(Min,3);
                    ChartObject.To = SetSigDigits(Max,3);
                    ChartObject.Value = SetSigDigits((PollenDataPoint.Number),3);
                    break;
                case 3:
                    ChartObject.From = 0;
                    ChartObject.To = 100;
                    ChartObject.Value = SetSigDigits((PollenDataPoint.Number-Min)/(Max-Min)*100,3);
                    break;
            }
        }

        public double SetSigDigits(double Number, int Digits)
        {
            int N;

            N = (int)Math.Pow(10, (double)Digits);
            return Math.Truncate(Number * N) / N;
        }

        public void SetAxisAppearance()
        {
        }

        public void SetAxisScale()
        {

        }

        public void SetCorners(wGraphic Graphic)
        {
            //Element.CornerRadius = new CornerRadius(Graphic.Radius[0], Graphic.Radius[1], Graphic.Radius[2], Graphic.Radius[3]);
        }

        public void SetFont(wGraphic Graphic)
        {
        }
    }
}