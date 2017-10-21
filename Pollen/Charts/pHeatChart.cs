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
using System.Windows.Media;
using LiveCharts.Wpf;

namespace Pollen.Charts
{
    public class pHeatChart : pChart
    {
        public Panel Element;
        public WindowsFormsHost ChartHost;
        public LiveCharts.WinForms.CartesianChart ChartObject;
        public List<pPointSeries> ChartSeriesSet;
        public string Type;
        public bool Status;

        public DataSetCollection DataGrid;
        public HeatSeries ChartSeries;
        public SeriesCollection SeriesCollect;

        public pHeatChart(string InstanceName)
        {
            //Set Element info setup
            Element = new StackPanel();
            ChartHost = new WindowsFormsHost();
            ChartObject = new LiveCharts.WinForms.CartesianChart();
            ChartObject.DisableAnimations = true;
            ChartHost.Child = ChartObject;

            DataGrid = new DataSetCollection();
            ChartSeries = new HeatSeries();
            SeriesCollect = new SeriesCollection();

            ChartSeries.Values = new ChartValues<HeatPoint>();
            ChartSeries.DataLabels = true;

            Element.Children.Add(ChartHost);
            Element.Name = InstanceName;
            Type = "HeatChart";
            
        }

        public void SetGradient(wGraphic Graphics)
        {
            ChartSeries.GradientStopCollection = Graphics.Gradient.ToMediaGradient();
        }

        public void SetProperties(DataSetCollection PollenDataGrid)
        {
            //Set unique properties of the control
            DataGrid = PollenDataGrid;

            ChartObject.Width = (int)DataGrid.Graphics.Width;
            ChartObject.Height = (int)DataGrid.Graphics.Height;

            ChartHost.Width = (int)DataGrid.Graphics.Width;
            ChartHost.Height = (int)DataGrid.Graphics.Height;
            //SetAxisScale();

            Element.Width = (int)DataGrid.Graphics.Width;
            Element.Height = (int)DataGrid.Graphics.Height;
        }

        public void SetAxisAppearance()
        {
        }

        public void SetHeatData()
        {
            List<HeatPoint> DataCollection = new List<HeatPoint>();

            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {

                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    DataPt D = DataGrid.Sets[i].Points[j];
                    DataCollection.Add(new HeatPoint(i, j, SetSigDigits(D.Number,3)));
                }
            }
            ChartSeries.Values.Clear();
            ChartSeries.Values.AddRange(DataCollection);

            SeriesCollect.Clear();
            SeriesCollect.Add(ChartSeries);
            ChartObject.Series = SeriesCollect;

        }

        public double SetSigDigits(double Number, int Digits)
        {
            int N;

            N = (int)Math.Pow(10, (double)Digits);
            return Math.Truncate(Number * N) / N;
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
