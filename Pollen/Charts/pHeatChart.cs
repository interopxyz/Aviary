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

using Wind.Containers;

using Pollen.Collections;
using System.Windows.Media;
using LiveCharts.Wpf;

namespace Pollen.Charts
{
    public class pHeatChart : pChart
    {
        public CartesianChart Element;
        public List<pPointSeries> ChartSeriesSet;
        public bool Status;

        public DataSetCollection DataGrid;
        public HeatSeries ChartSeries;
        public SeriesCollection SeriesCollect;

        public pHeatChart(string InstanceName)
        {
            //Set Element info setup
            Element = new CartesianChart();
            Element.DisableAnimations = true;

            DataGrid = new DataSetCollection();
            ChartSeries = new HeatSeries();
            SeriesCollect = new SeriesCollection();

            ChartSeries.Values = new ChartValues<HeatPoint>();
            ChartSeries.DataLabels = true;
            
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

            Element.MinWidth = 300;
            Element.MinHeight = 300;

            Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            Element.Background = Brushes.Transparent;
        }

        public void SetHeatData()
        {
            List<HeatPoint> DataCollection = new List<HeatPoint>();

            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    DataPt D = DataGrid.Sets[i].Points[j];
                    DataCollection.Add(new HeatPoint(i, j, D.Number));
                }
            }
            ChartSeries.Values.Clear();
            ChartSeries.Values.AddRange(DataCollection);

            SeriesCollect.Clear();
            SeriesCollect.Add(ChartSeries);
            Element.Series = SeriesCollect;

        }

        public void SetFormatting()
        {
            DataPt Pt = DataGrid.Sets[0].Points[0];
            wGraphic G = Pt.Graphics;

            ChartSeries.DataLabels = true;
            ChartSeries.LabelPoint = point => string.Format("{0:" + Pt.Format + "}", point.Weight);

            ChartSeries.Foreground = G.GetFontBrush();
            ChartSeries.FontFamily = G.FontObject.ToMediaFont().Family;
            ChartSeries.FontSize = G.FontObject.Size;
            ChartSeries.FontStyle = G.FontObject.ToMediaFont().Italic;
            ChartSeries.FontWeight = G.FontObject.ToMediaFont().Bold;

            ChartSeries.StrokeThickness = G.StrokeWeight[0];
            ChartSeries.Stroke = G.GetStrokeBrush();
        }
        
        
        public override void SetSize()
        {
            double W = double.NaN;
            double H = double.NaN;
            if (Graphics.Width > 0) { W = Graphics.Width; }
            if (Graphics.Height > 0) { H = Graphics.Height; }

            Element.Width = W;
            Element.Height = H;
        }

        public override void SetSolidFill()
        {
            Element.Background = DataSet.Graphics.GetBackgroundBrush();
        }

        public void SetCorners(wGraphic Graphic)
        {
        }

        public void SetFont(wGraphic Graphic)
        {
        }
    }
}
