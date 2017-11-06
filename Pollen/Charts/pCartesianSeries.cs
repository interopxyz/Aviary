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

    public class pCartesianSeries : pChart
    {
        public ScatterSeries ChartSeries;
        public RowSeries GanttSeries;
        public DataPtSet DataList;
        
        public bool Status;
        public int Mode;

        public pCartesianSeries(string InstanceName)
        {
            ChartSeries = new ScatterSeries();
            GanttSeries = new RowSeries();
            //Set Element info setup
            Type = "ScatterSeries";
        }

        public void SetProperties(DataPtSet PollenDataSet, int ChartMode)
        {
            Mode = ChartMode;
            DataList = PollenDataSet;

            switch (Mode)
            {
                case 0:
                    ChartSeries.PointGeometry = DefaultGeometries.Circle;
                    ChartSeries.Values = new ChartValues<ScatterPoint>();
                    break;
                case 1:
                    GanttSeries.Values = new ChartValues<GanttPoint>();
                    GanttSeries.DataLabels = true;
                    break;
            }
            //Set unique properties of the control
        }

        public DataPoint SetMarkerType(DataPoint Pt, int Mode)
        {

            return Pt;
        }

        public void SetPointChartType(int Mode, int StackMode)
        {
        }

        public void SetChartLabels(int Mode, bool HasLeader)
        {
        }

        public void SetScatterData(int DataMode)
        {
            List< ScatterPoint> DataCollection = new List<ScatterPoint>();
            for (int i = 0; i < DataList.Count; i++)
            {
                DataPt D = DataList.Points[i];
                DataCollection.Add( new ScatterPoint(D.Point.X, D.Point.Y, D.Point.Z));
            }

            ChartSeries.Values.Clear();
            ChartSeries.Values.AddRange(DataCollection);
        }

        public void SetGanttData(int DataMode)
        {
            List<GanttPoint> DataCollection = new List<GanttPoint>();
            for (int i = 0; i < DataList.Count; i++)
            {
                DataPt D = DataList.Points[i];
                DataCollection.Add(new GanttPoint(D.Domain.Item1,D.Domain.Item2));
            }

            GanttSeries.Values.Clear();
            GanttSeries.Values.AddRange(DataCollection);
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
