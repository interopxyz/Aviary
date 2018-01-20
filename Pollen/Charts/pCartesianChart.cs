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
    public class pCartesianChart : pChart
    {
        public CartesianChart Element = new CartesianChart();
        public bool Status = false;
        public DataSetCollection DataGrid = new DataSetCollection();

        public pCartesianChart(string InstanceName)
        {
            //Set Element info setup
            Element = new CartesianChart();
            Element.DisableAnimations = true;
            
            Element.Name = InstanceName;
            Type = "CartesianChart";
            
            DataGrid = new DataSetCollection();

            //Set "Clear" appearance to all elements

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

        public void SetSeries(List<pCartesianSeries> DataSeries)
        {
            SeriesCollection SeriesCollect = new SeriesCollection ();

            for (int i = 0; i < DataSeries.Count; i++)
            {
                for (int j = 0; j < DataSeries[i].DataList.Count; j++)
                {
                    SeriesCollect.Add(DataSeries[i].ChartSeries[j]);
                }
            }
            Element.Series = SeriesCollect;
        }

        public void SetSequence(List<pCartesianSeries> DataSeries)
        {
            SeriesCollection SeriesCollect = new SeriesCollection();

            for (int i = 0; i < DataSeries.Count; i++)
            {
                    SeriesCollect.Add(DataSeries[i].ChartSequence);
            }
            Element.Series = SeriesCollect;
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
