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
    public class pCartesianChart : pChart
    {
        public System.Windows.Controls.Panel Element;
        public WindowsFormsHost ChartHost;
        public CartesianChart ChartObject;
        public List<pPointSeries> ChartSeriesSet;
        public string Type;
        public bool Status;
        public DataSetCollection DataGrid = new DataSetCollection();

        public pCartesianChart(string InstanceName)
        {
            //Set Element info setup
            Element = new StackPanel();
            ChartHost = new WindowsFormsHost();
            ChartObject = new CartesianChart();
            ChartObject.DisableAnimations = true;
            ChartHost.Child = ChartObject;
            
            Element.Children.Add(ChartHost);
            Element.Name = InstanceName;
            Type = "CartesianChart";

            //Set "Clear" appearance to all elements

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
        
        public void SetGanttSeries(List<pCartesianSeries> DataSeries)
        {
            int cnt = DataSeries.Count;
            SeriesCollection SeriesCollect = new SeriesCollection();

            for (int i = 0; i < cnt; i++)
            {
                SeriesCollect.Add(DataSeries[i].GanttSeries);
            }

            ChartObject.Series = SeriesCollect;
        }

        public void SetScatterSeries(List<pCartesianSeries> DataSeries)
        {
            int cnt = DataSeries.Count;
            SeriesCollection SeriesCollect = new SeriesCollection ();

            for (int i = 0; i < cnt; i++)
            {
                SeriesCollect.Add(DataSeries[i].ChartSeries);
            }

            ChartObject.Series = SeriesCollect;
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
