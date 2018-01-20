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

namespace Pollen.Charts
{
    public class pRadialChart : pChart
    {
        public PieChart Element = new PieChart();
        public bool Status = false;
        public DataSetCollection DataGrid = new DataSetCollection();

        public pRadialChart(string InstanceName)
        {
            //Set Element info setup
            Element = new PieChart();
            Element.DisableAnimations = true;

            Element.Name = InstanceName;
            Type = "RadialChart";

            DataGrid = new DataSetCollection();

            //Set "Clear" appearance to all elements

        }

        public void SetProperties(DataSetCollection PollenDataGrid, double Radius)
        {
            //Set unique properties of the control
            DataGrid = PollenDataGrid;

            Element.MinWidth = 300;
            Element.MinHeight = 300;

            Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            Element.Background = Brushes.Transparent;
            Element.InnerRadius = Radius;

        }

        public void SetSeries(List<pRadialSeries> DataSeries)
        {
            SeriesCollection SeriesCollect = new SeriesCollection();

            for (int i = 0; i < DataSeries[0].DataList.Count; i++)
            {
                List<double> ValueSet = new List<double>();
                for (int j = 0; j < DataSeries.Count; j++)
                {

                    ValueSet.Add(DataSeries[j].ChartSeries[i]);
                }

                PieSeries pSeries = new PieSeries();
                SetSequenceProperties(DataSeries[0].DataList.Points[i], pSeries);
                pSeries.Values = new ChartValues<double>(ValueSet.ToArray());

                SeriesCollect.Add(pSeries);

            }
            Element.Series = SeriesCollect;
        }

        public Series SetSequenceProperties(DataPt Pt, LiveCharts.Wpf.Series Sequence)
        {
            wGraphic G = Pt.Graphics;

            Sequence.PointGeometry = GetGeometry((int)Pt.Marker.Mode);

            Sequence.DataLabels = true;
            Sequence.LabelPoint = point => Pt.Label.Content;

            Sequence.Fill = G.GetBackgroundBrush();
            Sequence.Foreground = G.GetFontBrush();

            Sequence.StrokeThickness = G.StrokeWeight[0];
            Sequence.Stroke = G.GetStrokeBrush();

            return Sequence;
        }

        private Geometry GetGeometry(int MarkerMode)
        {
            Geometry Geo = DefaultGeometries.None;

            switch (MarkerMode)
            {
                case 1:
                    Geo = DefaultGeometries.Circle;
                    break;
                case 2:
                    Geo = DefaultGeometries.Square;
                    break;
                case 3:
                    Geo = DefaultGeometries.Diamond;
                    break;
                case 4:
                    Geo = DefaultGeometries.Triangle;
                    break;
                case 5:
                    Geo = DefaultGeometries.Cross;
                    break;
            }

            return Geo;
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

        public override void SetStroke()
        {
            base.SetStroke();
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
