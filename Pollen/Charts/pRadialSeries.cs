using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Wind.Containers;
using Pollen.Collections;
using System.Windows.Forms.DataVisualization.Charting;

namespace Pollen.Charts
{

    public class pRadialSeries : pChart
    {
        public List<double> DataSeries = new List<double>();
        public DataPtSet DataList = new DataPtSet();


        public enum SeriesChartType
        {
            Pie, Doughnut
        }

        public SeriesChartType ChartType = SeriesChartType.Pie;

        public pRadialSeries(string InstanceName)
        {
            DataSeries = new List<double>();
            DataList = new DataPtSet();

            //Set Element info setup
            Type = "RadialSequence";
        }


        public DataPoint SetMarkerType(DataPoint Pt, int Mode)
        {
            return Pt;
        }

        public void SetChartLabels(int Mode, bool HasLeader)
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
