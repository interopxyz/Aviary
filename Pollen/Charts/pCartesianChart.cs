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
using System.Collections.Specialized;

namespace Pollen.Charts
{
    public class pCartesianChart : pChart
    {
        public CartesianChart Element = new CartesianChart();
        public bool Status = false;
        public DataSetCollection DataGrid = new DataSetCollection();

        public enum SeriesChartType
        {
            Point, Line, StepLine, Spline,
            Area, AreaStack, AreaStack100, SplineArea, SplineAreaStack, SplineAreaStack100,
            ColumnAdjacent, ColumnStack, ColumnStack100, BarAdjacent, BarStack, BarStack100,
            Gantt, Bubble, Scatter, None
        }

        public SeriesChartType ChartType = SeriesChartType.None;

        public pCartesianChart(string InstanceName)
        {
            //Set Element info setup
            Element = new CartesianChart();
            Element.DisableAnimations = true;

            Element.Name = InstanceName;

            DataGrid = new DataSetCollection();

            Element.Background = Brushes.Transparent;

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
            
            SetToolTip();
        }

        //AREA CHART ######################################################################################################################
        public void SetAreaChart(int Smoothness)
        {
            if (ChartType != SeriesChartType.Area) { Element.Series.Clear(); }
            ChartType = SeriesChartType.Area;


            List<List<double>> DV = new List<List<double>>();

            //Populate Data Set
            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                List<double> tDV = new List<double>();

                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    tDV.Add(DataGrid.Sets[i].Points[j].Number);
                }
                DV.Add(tDV);
            }

            int C = Element.Series.Count;
            int S = DV.Count;

            for (int i = C; i < S; i++)
            {
                LineSeries CS = new LineSeries();
                CS.LineSmoothness = Smoothness;

                Element.Series.Add(CS);
                Element.Series[i].Values = new ChartValues<double>(DV[i].ToArray());
            }

            C = Element.Series.Count;

            if (C > S)
            {
                for (int i = S; i < C; i++)
                {
                    Element.Series.RemoveAt(Element.Series.Count - 1);
                }
            }

            C = Element.Series.Count;

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = DV[i].Count;

                for (int j = T; j < V; j++)
                {
                    Element.Series[i].Values.Add(1.0);
                }
            }

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = DV[i].Count;

                if (T > V)
                {
                    for (int j = V; j < T; j++)
                    {
                        Element.Series[i].Values.RemoveAt(Element.Series[i].Values.Count - 1);
                    }
                }
            }

            for (int i = 0; i < S; i++)
            {
                int V = DV[i].Count;

                for (int j = 0; j < V; j++)
                {
                    Element.Series[i].Values[j] = DV[i][j];
                    SetAreaProperties((LineSeries)Element.Series[i], DataGrid.Sets[i].Points[j]);
                    SetSequenceProperties(i, j, (LineSeries)Element.Series[i]);
                }
            }
        }
        public void SetAreaProperties(LineSeries Ser, DataPt Pt)
        {
        }

        //AREA STACK CHART #########################################################################################################################
        public void SetStackAreaChart(bool IsStretched, int Smoothness)
        {
            if (IsStretched)
            {
                if (ChartType != SeriesChartType.AreaStack100) { Element.Series.Clear(); }
                ChartType = SeriesChartType.AreaStack100;
            }
            else
            {
                if (ChartType != SeriesChartType.AreaStack) { Element.Series.Clear(); }
                ChartType = SeriesChartType.AreaStack;
            }

            List<List<double>> DV = new List<List<double>>();

            //Populate Data Set
            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                List<double> tDV = new List<double>();

                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    tDV.Add(DataGrid.Sets[i].Points[j].Number);
                }
                DV.Add(tDV);
            }

            int C = Element.Series.Count;
            int S = DV.Count;

            for (int i = C; i < S; i++)
            {
                StackedAreaSeries CS = new StackedAreaSeries();
                CS.LineSmoothness = Smoothness;
                if (IsStretched) { CS.StackMode = StackMode.Percentage; } else { CS.StackMode = StackMode.Values; }

                Element.Series.Add(CS);
                Element.Series[i].Values = new ChartValues<double>(DV[i].ToArray());
            }

            C = Element.Series.Count;

            if (C > S)
            {
                for (int i = S; i < C; i++)
                {
                    Element.Series.RemoveAt(Element.Series.Count - 1);
                }
            }

            C = Element.Series.Count;

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = DV[i].Count;

                for (int j = T; j < V; j++)
                {
                    Element.Series[i].Values.Add(1.0);
                }
            }

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = DV[i].Count;

                if (T > V)
                {
                    for (int j = V; j < T; j++)
                    {
                        Element.Series[i].Values.RemoveAt(Element.Series[i].Values.Count - 1);
                    }
                }
            }

            for (int i = 0; i < S; i++)
            {
                int V = DV[i].Count;

                for (int j = 0; j < V; j++)
                {
                    Element.Series[i].Values[j] = DV[i][j];
                    SetStackAreaProperties((StackedAreaSeries)Element.Series[i], DataGrid.Sets[i].Points[j]);
                    SetSequenceProperties(i, j, (StackedAreaSeries)Element.Series[i]);
                }
            }
        }
        public void SetStackAreaProperties(StackedAreaSeries Ser, DataPt Pt)
        {
            Ser.PointForeground = Pt.Marker.Graphics.GetBackgroundBrush();
            Ser.PointGeometrySize = Pt.Marker.Radius;
        }

        //POINT CHART ######################################################################################################################
        public void SetPointChart()
        {
            if (ChartType != SeriesChartType.Point) { Element.Series.Clear(); }
            ChartType = SeriesChartType.Point;


            List<List<double>> DV = new List<List<double>>();

            //Populate Data Set
            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                List<double> tDV = new List<double>();

                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    tDV.Add(DataGrid.Sets[i].Points[j].Number);
                }
                DV.Add(tDV);
            }

            int C = Element.Series.Count;
            int S = DV.Count;

            for (int i = C; i < S; i++)
            {
                LineSeries CS = new LineSeries();

                Element.Series.Add(CS);
                Element.Series[i].Values = new ChartValues<double>(DV[i].ToArray());
            }

            C = Element.Series.Count;

            if (C > S)
            {
                for (int i = S; i < C; i++)
                {
                    Element.Series.RemoveAt(Element.Series.Count - 1);
                }
            }

            C = Element.Series.Count;

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = DV[i].Count;

                for (int j = T; j < V; j++)
                {
                    Element.Series[i].Values.Add(1.0);
                }
            }

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = DV[i].Count;

                if (T > V)
                {
                    for (int j = V; j < T; j++)
                    {
                        Element.Series[i].Values.RemoveAt(Element.Series[i].Values.Count - 1);
                    }
                }
            }

            for (int i = 0; i < S; i++)
            {
                int V = DV[i].Count;

                for (int j = 0; j < V; j++)
                {
                    Element.Series[i].Values[j] = DV[i][j];
                    SetPointProperties((LineSeries)Element.Series[i], DataGrid.Sets[i].Points[j]);
                    SetSequenceProperties(i, j, (LineSeries)Element.Series[i]);
                }
            }
        }
        public void SetPointProperties(LineSeries Ser, DataPt Pt)
        {
            Ser.Fill = Brushes.Transparent;

            Ser.PointGeometry = GetGeometry(1);
            Ser.PointForeground = Pt.Marker.Graphics.GetBackgroundBrush();
        }

        //LINE / SPLINE CHART ######################################################################################################################
        public void SetLineChart(int Smoothness)
        {
            if (Smoothness == 0)
            {
                if (ChartType != SeriesChartType.Line) { Element.Series.Clear(); }
                ChartType = SeriesChartType.Line;
            }
            else
            {
                if (ChartType != SeriesChartType.Spline) { Element.Series.Clear(); }
                ChartType = SeriesChartType.Spline;
            }

            List<List<double>> DV = new List<List<double>>();

            //Populate Data Set
            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                List<double> tDV = new List<double>();

                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    tDV.Add(DataGrid.Sets[i].Points[j].Number);
                }
                DV.Add(tDV);
            }

            int C = Element.Series.Count;
            int S = DV.Count;

            for (int i = C; i < S; i++)
            {
                LineSeries CS = new LineSeries();
                CS.LineSmoothness = Smoothness;

                Element.Series.Add(CS);
                Element.Series[i].Values = new ChartValues<double>(DV[i].ToArray());
            }

            C = Element.Series.Count;

            if (C > S)
            {
                for (int i = S; i < C; i++)
                {
                    Element.Series.RemoveAt(Element.Series.Count - 1);
                }
            }

            C = Element.Series.Count;

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = DV[i].Count;

                for (int j = T; j < V; j++)
                {
                    Element.Series[i].Values.Add(1.0);
                }
            }

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = DV[i].Count;

                if (T > V)
                {
                    for (int j = V; j < T; j++)
                    {
                        Element.Series[i].Values.RemoveAt(Element.Series[i].Values.Count - 1);
                    }
                }
            }

            for (int i = 0; i < S; i++)
            {
                int V = DV[i].Count;

                for (int j = 0; j < V; j++)
                {
                    Element.Series[i].Values[j] = DV[i][j];
                    SetLineProperties((LineSeries)Element.Series[i], DataGrid.Sets[i].Points[j]);
                    SetSequenceProperties(i, j, (LineSeries)Element.Series[i]);
                }
            }
        }
        private void SetLineProperties(LineSeries Ser, DataPt Pt)
        {
            Ser.Fill = Brushes.Transparent;

            Ser.PointForeground = Pt.Marker.Graphics.GetBackgroundBrush();
            Ser.PointGeometrySize = Pt.Marker.Radius;
        }

        //STEP LINE CHART ######################################################################################################################
        public void SetStepLineChart()
        {
            if (ChartType != SeriesChartType.StepLine) { Element.Series.Clear(); }
            ChartType = SeriesChartType.StepLine;


            List<List<double>> DV = new List<List<double>>();

            //Populate Data Set
            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                List<double> tDV = new List<double>();

                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    tDV.Add(DataGrid.Sets[i].Points[j].Number);
                }
                DV.Add(tDV);
            }

            int C = Element.Series.Count;
            int S = DV.Count;

            for (int i = C; i < S; i++)
            {
                Element.Series.Add(new StepLineSeries());
                Element.Series[i].Values = new ChartValues<double>(DV[i].ToArray());
            }

            C = Element.Series.Count;

            if (C > S)
            {
                for (int i = S; i < C; i++)
                {
                    Element.Series.RemoveAt(Element.Series.Count - 1);
                }
            }

            C = Element.Series.Count;

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = DV[i].Count;

                for (int j = T; j < V; j++)
                {
                    Element.Series[i].Values.Add(1.0);
                }
            }

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = DV[i].Count;

                if (T > V)
                {
                    for (int j = V; j < T; j++)
                    {
                        Element.Series[i].Values.RemoveAt(Element.Series[i].Values.Count - 1);
                    }
                }
            }

            for (int i = 0; i < S; i++)
            {
                int V = DV[i].Count;

                for (int j = 0; j < V; j++)
                {
                    Element.Series[i].Values[j] = DV[i][j];
                    SetStepLineProperties((StepLineSeries)Element.Series[i], DataGrid.Sets[i].Points[j]);
                    SetSequenceProperties(i, j, (StepLineSeries)Element.Series[i]);
                }
            }
        }
        private void SetStepLineProperties(StepLineSeries Ser, DataPt Pt)
        {
            Ser.Fill = Brushes.Transparent;
            Ser.PointForeground = Pt.Marker.Graphics.GetBackgroundBrush();
            Ser.PointGeometrySize = Pt.Marker.Radius;
            Ser.AlternativeStroke = Pt.Graphics.GetStrokeBrush();
        }

        //COLUMN ADJACENT CHART ######################################################################################################################
        public void SetAdjacentColumnChart()
        {
            if (ChartType != SeriesChartType.ColumnAdjacent) { Element.Series.Clear(); }
            ChartType = SeriesChartType.ColumnAdjacent;

            List<double> PT = new List<double>();
            List<int> IV = new List<int>();
            List<int> JV = new List<int>();

            //Populate Data Set
            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    PT.Add(DataGrid.Sets[i].Points[j].Number);
                    IV.Add(i);
                    JV.Add(j);
                }
            }

            int C = Element.Series.Count;
            int S = PT.Count;

            for (int i = C; i < S; i++)
            {
                ColumnSeries CS = new ColumnSeries();
                CS.LabelsPosition = BarLabelPosition.Parallel;

                Element.Series.Add(CS);
                Element.Series[i].Values = new ChartValues<double>();
                Element.Series[i].Values.Add(0.0);
            }

            C = Element.Series.Count;

            if (C > S)
            {
                for (int i = S; i < C; i++)
                {
                    Element.Series.RemoveAt(Element.Series.Count - 1);
                }
            }

            C = Element.Series.Count;

            for (int i = 0; i < S; i++)
            {
                Element.Series[i].Values[0] = PT[i];
                SetAdjacentColumnProperties((ColumnSeries)Element.Series[i], (int)DataGrid.Sets[IV[i]].Points[JV[i]].Label.Alignment);
                SetSequenceProperties(IV[i], JV[i], (ColumnSeries)Element.Series[i]);
            }
        }
        private void SetAdjacentColumnProperties(ColumnSeries Ser, int Position)
        {
            Ser.LabelsPosition = GetPosition(Position);
        }

        //COLUMN STACK CHART #########################################################################################################################
        public void SetStackColumnChart(bool IsStretched)
        {
            if (IsStretched)
            {
                if (ChartType != SeriesChartType.ColumnStack100) { Element.Series.Clear(); }
                ChartType = SeriesChartType.ColumnStack100;
            }
            else
            {
                if (ChartType != SeriesChartType.ColumnStack) { Element.Series.Clear(); }
                ChartType = SeriesChartType.ColumnStack;
            }

            List<List<double>> DV = new List<List<double>>();

            //Populate Data Set
            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                List<double> tDV = new List<double>();

                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    tDV.Add(DataGrid.Sets[i].Points[j].Number);
                }
                DV.Add(tDV);
            }

            int C = Element.Series.Count;
            int S = DV.Count;

            for (int i = C; i < S; i++)
            {
                StackedColumnSeries CS = new StackedColumnSeries();
                if (IsStretched) { CS.StackMode = StackMode.Percentage; } else { CS.StackMode = StackMode.Values; }

                Element.Series.Add(CS);
                Element.Series[i].Values = new ChartValues<double>(DV[i].ToArray());
            }

            C = Element.Series.Count;

            if (C > S)
            {
                for (int i = S; i < C; i++)
                {
                    Element.Series.RemoveAt(Element.Series.Count - 1);
                }
            }

            C = Element.Series.Count;

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = DV[i].Count;

                for (int j = T; j < V; j++)
                {
                    Element.Series[i].Values.Add(1.0);
                }
            }

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = DV[i].Count;

                if (T > V)
                {
                    for (int j = V; j < T; j++)
                    {
                        Element.Series[i].Values.RemoveAt(Element.Series[i].Values.Count - 1);
                    }
                }
            }

            for (int i = 0; i < S; i++)
            {
                int V = DV[i].Count;

                for (int j = 0; j < V; j++)
                {
                    Element.Series[i].Values[j] = DV[i][j];
                    SetStackColumnProperties((StackedColumnSeries)Element.Series[i], (int)DataGrid.Sets[i].Points[j].Label.Alignment);
                    SetSequenceProperties(i, j, (StackedColumnSeries)Element.Series[i]);
                }
            }
        }
        private void SetStackColumnProperties(StackedColumnSeries Ser, int Position)
        {
            Ser.LabelsPosition = GetPosition(Position);
        }

        //BAR ADJACENT CHART #######################################################################################################################
        public void SetAdjacentBarChart()
        {
            if (ChartType != SeriesChartType.BarAdjacent) { Element.Series.Clear(); }
            ChartType = SeriesChartType.BarAdjacent;

            List<double> PT = new List<double>();
            List<int> IV = new List<int>();
            List<int> JV = new List<int>();

            //Populate Data Set
            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    PT.Add(DataGrid.Sets[i].Points[j].Number);
                    IV.Add(i);
                    JV.Add(j);
                }
            }

            int C = Element.Series.Count;
            int S = PT.Count;

            for (int i = C; i < S; i++)
            {
                RowSeries CS = new RowSeries();
                CS.LabelsPosition = BarLabelPosition.Parallel;

                Element.Series.Add(CS);
                Element.Series[i].Values = new ChartValues<double>();
                Element.Series[i].Values.Add(0.0);
            }

            C = Element.Series.Count;

            if (C > S)
            {
                for (int i = S; i < C; i++)
                {
                    Element.Series.RemoveAt(Element.Series.Count - 1);
                }
            }

            C = Element.Series.Count;

            for (int i = 0; i < S; i++)
            {
                Element.Series[i].Values[0] = PT[i];
                SetAdjacentBarProperties((RowSeries)Element.Series[i], (int)DataGrid.Sets[IV[i]].Points[JV[i]].Label.Alignment);
                SetSequenceProperties(IV[i], JV[i], (RowSeries)Element.Series[i]);
            }
        }
        private void SetAdjacentBarProperties(RowSeries Ser, int Position)
        {
            Ser.LabelsPosition = GetPosition(Position);
        }

        //BAR STACK CHART #########################################################################################################################
        public void SetStackBarChart(bool IsStretched)
        {
            if (IsStretched)
            {
                if (ChartType != SeriesChartType.BarStack100) { Element.Series.Clear(); }
                ChartType = SeriesChartType.BarStack100;
            }
            else
            {
                if (ChartType != SeriesChartType.BarStack) { Element.Series.Clear(); }
                ChartType = SeriesChartType.BarStack;
            }

            List<List<double>> DV = new List<List<double>>();

            //Populate Data Set
            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                List<double> tDV = new List<double>();

                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    tDV.Add(DataGrid.Sets[i].Points[j].Number);
                }
                DV.Add(tDV);
            }
            int C = Element.Series.Count;
            int S = DV.Count;
            

            for (int i = C; i < S; i++)
            {
                StackedRowSeries CS = new StackedRowSeries();
                if (IsStretched) { CS.StackMode = StackMode.Percentage; } else { CS.StackMode = StackMode.Values; }

                Element.Series.Add(CS);
                Element.Series[i].Values = new ChartValues<double>(DV[i].ToArray());
            }

            C = Element.Series.Count;

            if (C > S)
            {
                for (int i = S; i < C; i++)
                {
                        Element.Series.RemoveAt(Element.Series.Count - 1);
                    
                }
            }

            C = Element.Series.Count;

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = DV[i].Count;

                if (T > V)
                {
                    for (int j = V; j < T; j++)
                    {
                        Element.Series[i].Values.RemoveAt(Element.Series[i].Values.Count - 1);
                    }
                }
            }

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = DV[i].Count;

                for (int j = T; j < V; j++)
                {
                    Element.Series[i].Values.Add(1.0);
                }
            }
            
            for (int i = 0; i < S; i++)
            {
                int V = DV[i].Count;

                for (int j = 0; j < V; j++)
                {
                    Element.Series[i].Values[j] = DV[i][j];
                    SetStackBarProperties((StackedRowSeries)Element.Series[i], (int)DataGrid.Sets[i].Points[j].Label.Alignment);
                    SetSequenceProperties(i, j, (StackedRowSeries)Element.Series[i]);
                }
            }

        }
        private void SetStackBarProperties(StackedRowSeries Ser, int Position)
        {
            Ser.LabelsPosition = GetPosition(Position);
        }

        //BUBBLE CHART #######################################################################################################################
        public void SetBubbleChart()
        {
            if (ChartType != SeriesChartType.Bubble) { Element.Series.Clear(); }
            ChartType = SeriesChartType.Bubble;

            List<ScatterPoint> PT = new List<ScatterPoint>();
            List<int> IV = new List<int>();
            List<int> JV = new List<int>();

            //Populate Data Set
            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    PT.Add(new ScatterPoint(DataGrid.Sets[i].Points[j].Point.X, DataGrid.Sets[i].Points[j].Point.Y, DataGrid.Sets[i].Points[j].Point.Z));
                    IV.Add(i);
                    JV.Add(j);
                }
            }

            int C = Element.Series.Count;
            int S = PT.Count;

            for (int i = C; i < S; i++)
            {
                Element.Series.Add(new ScatterSeries());
                Element.Series[i].Values = new ChartValues<ScatterPoint>();
                Element.Series[i].Values.Add(new ScatterPoint());
            }

            C = Element.Series.Count;

            if (C > S)
            {
                for (int i = S; i < C; i++)
                {
                    Element.Series.RemoveAt(Element.Series.Count - 1);
                }
            }

            C = Element.Series.Count;

            for (int i = 0; i < S; i++)
            {
                Element.Series[i].Values[0] = PT[i];
                SetBubbleProperties((ScatterSeries)Element.Series[i], PT[i].Weight);
                SetSequenceProperties(IV[i], JV[i], (ScatterSeries)Element.Series[i]);
            }
        }
        private void SetBubbleProperties(ScatterSeries Ser, double Radius)
        {
            Ser.MinPointShapeDiameter = Math.Abs(Radius);
            Ser.MaxPointShapeDiameter = Math.Abs(Radius);
        }

        //SCATTER CHART #######################################################################################################################
        public void SetScatterChart()
        {
            if (ChartType != SeriesChartType.Scatter) { Element.Series.Clear(); }
            ChartType = SeriesChartType.Scatter;

            List<ObservablePoint> PT = new List<ObservablePoint>();
            List<int> IV = new List<int>();
            List<int> JV = new List<int>();

            //Populate Data Set
            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    PT.Add(new ObservablePoint(DataGrid.Sets[i].Points[j].Point.X, DataGrid.Sets[i].Points[j].Point.Y));
                    IV.Add(i);
                    JV.Add(j);
                }
            }

            int C = Element.Series.Count;
            int S = PT.Count;

            for (int i = C; i < S; i++)
            {
                Element.Series.Add(new ScatterSeries());
                Element.Series[i].Values = new ChartValues<ObservablePoint>();
                Element.Series[i].Values.Add(new ObservablePoint());
            }

            C = Element.Series.Count;

            if (C > S)
            {
                for (int i = S; i < C; i++)
                {
                    Element.Series.RemoveAt(Element.Series.Count - 1);
                }
            }

            C = Element.Series.Count;

            for (int i = 0; i < S; i++)
            {
                Element.Series[i].Values[0] = PT[i];
                SetScatterProperties((ScatterSeries)Element.Series[i], IV[i]);
                SetSequenceProperties(IV[i], JV[i], (ScatterSeries)Element.Series[i]);
            }
        }
        private void SetScatterProperties(ScatterSeries Ser, int i)
        {
            Ser.MinPointShapeDiameter = Math.Abs(DataGrid.Sets[i].Points[0].Marker.Radius);
            Ser.MaxPointShapeDiameter = Math.Abs(DataGrid.Sets[i].Points[0].Marker.Radius);
            Ser.PointGeometry = GetGeometry((int)DataGrid.Sets[i].Points[0].Marker.Mode);
        }

        //GANTT CHART #########################################################################################################################
        public void SetGanttChart()
        {
            if (ChartType != SeriesChartType.Gantt) { Element.Series.Clear(); }
            ChartType = SeriesChartType.Gantt;

            List<List<GanttPoint>> GP = new List<List<GanttPoint>>();

            //Populate Data Set
            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                List<GanttPoint> tGP = new List<GanttPoint>();

                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    wDomain TD = SortDomain(DataGrid.Sets[i].Points[j].Domain.Item1, DataGrid.Sets[i].Points[j].Domain.Item2);
                    tGP.Add(new GanttPoint(TD.T0, TD.T1));
                }
                GP.Add(tGP);
            }

            int C = Element.Series.Count;
            int S = GP.Count;

            for (int i = C; i < S; i++)
            {
                Element.Series.Add(new RowSeries());
                Element.Series[i].Values = new ChartValues<GanttPoint>(GP[i].ToArray());
            }

            C = Element.Series.Count;

            if (C > S)
            {
                for (int i = S; i < C; i++)
                {
                    Element.Series.RemoveAt(Element.Series.Count - 1);
                }
            }

            C = Element.Series.Count;

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = GP[i].Count;

                for (int j = T; j < V; j++)
                {
                    Element.Series[i].Values.Add(new GanttPoint());
                }
            }

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = GP[i].Count;

                if (T > V)
                {
                    for (int j = V; j < T; j++)
                    {
                        Element.Series[i].Values.RemoveAt(Element.Series[i].Values.Count - 1);
                    }
                }
            }

            for (int i = 0; i < S; i++)
            {
                int V = GP[i].Count;

                for (int j = 0; j < V; j++)
                {
                    Element.Series[i].Values[j] = GP[i][j];
                    SetSequenceProperties(i, j, (RowSeries)Element.Series[i]);
                }
            }
        }

        public void ForceRefresh()
        {
            Element.Update(false, true);
        }

        //LEGACY #########################################################################################################################
        public void SetSeries(List<pCartesianSeries> DataSeries)
        {
            SeriesCollection SeriesCollect = new SeriesCollection();

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

        public override void SetToolTip()
        {
            wLabel L = DataGrid.Sets[0].Points[0].ToolTip;
            wGraphic G = L.Graphics;
            wFont F = G.FontObject;

            DefaultTooltip Tip = (DefaultTooltip)Element.DataTooltip;

            Tip.SelectionMode = TooltipSelectionMode.OnlySender;

            if (L.Enabled)
            {
                Tip.BulletSize = 0;

                Tip.ShowSeries = false;
                Tip.Background = G.GetBackgroundBrush();
                Tip.Foreground = F.GetFontBrush();

                Tip.BorderBrush = G.GetStrokeBrush();
                Tip.BorderThickness = G.GetStroke();

                Tip.FontFamily = F.ToMediaFont().Family;
                Tip.FontSize = F.Size;
                Tip.FontStyle = F.ToMediaFont().Italic;
                Tip.FontWeight = F.ToMediaFont().Bold;
            }

            Element.DataTooltip = Tip;

        }

        private wDomain SortDomain(double T0, double T1)
        {
            wDomain OutputDomain = new wDomain(T0, T1);

            if (T0 > T1) { OutputDomain = new wDomain(T1, T0); }

            return OutputDomain;
        }

        // ################################# OVERRIDE CHART PROPERTIES #################################

        public void SetSequenceProperties(int I, int J, Series Sequence)
        {
            DataPt Pt = DataGrid.Sets[I].Points[J];

            wGraphic G = Pt.Graphics;
            wFont F = G.FontObject;

            Sequence.DataLabels = Pt.Label.Enabled;
            switch (ChartType)
            {
                default:
                    Sequence.LabelPoint = point => string.Format("{0:" + Pt.Label.Format + "}", point.Y);
                    break;
                case SeriesChartType.BarAdjacent:
                case SeriesChartType.BarStack:
                case SeriesChartType.BarStack100:
                    Sequence.LabelPoint = point => string.Format("{0:" + Pt.Label.Format + "}", point.X);
                    break;
                case SeriesChartType.Gantt:
                    Sequence.LabelPoint = point => (string.Format("{0:" + Pt.Label.Format + "}", point.XStart) + " & " + string.Format("{0:" + Pt.Label.Format + "}", point.X));
                    break;
                case SeriesChartType.Scatter:
                    Sequence.LabelPoint = point => (string.Format("{0:" + Pt.Label.Format + "}", point.X) + " & " + string.Format("{0:" + Pt.Label.Format + "}", point.Y));
                    break;
                case SeriesChartType.Bubble:
                    Sequence.LabelPoint = point => string.Format("{0:" + Pt.Label.Format + "}", point.Weight);
                    break;

            }

            Sequence.PointGeometry = GetGeometry((int)Pt.Marker.Mode);

            Sequence.Foreground = G.GetFontBrush();
            Sequence.FontFamily = F.ToMediaFont().Family;
            Sequence.FontSize = F.Size;
            Sequence.FontStyle = F.ToMediaFont().Italic;
            Sequence.FontWeight = F.ToMediaFont().Bold;

            Sequence.Fill = G.GetBackgroundBrush();

            Sequence.StrokeThickness = G.StrokeWeight[0];
            Sequence.Stroke = G.GetStrokeBrush();
            Sequence.StrokeDashArray = new DoubleCollection(G.StrokePattern);

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

        private BarLabelPosition GetPosition(int PositionMode)
        {
            BarLabelPosition Pos = BarLabelPosition.Parallel;

            switch (PositionMode)
            {
                case 0:
                    Pos = BarLabelPosition.Top;
                    break;
                case 4:
                    Pos = BarLabelPosition.Perpendicular;
                    break;
            }

            return Pos;

        }

        // ################################# OVERRIDE GRAPHIC PROPERTIES #################################
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
            Element.Background = Graphics.GetBackgroundBrush();
        }

        public override void SetGradientFill()
        {
            Element.Background = Graphics.WpfFill;
        }

        public override void SetPatternFill()
        {
            Element.Background = Graphics.WpfPattern;
        }

        public override void SetStroke()
        {
            Element.BorderBrush = Graphics.GetStrokeBrush();
            Element.BorderThickness = Graphics.GetStroke();
        }

        public override void SetFont()
        {
            Element.FontFamily = Graphics.FontObject.ToMediaFont().Family;
            Element.FontSize = Graphics.FontObject.ToMediaFont().Size;
            Element.FontStyle = Graphics.FontObject.ToMediaFont().Italic;
            Element.FontWeight = Graphics.FontObject.ToMediaFont().Bold;
        }

        public override void SetMargin()
        {
            Element.Margin = Graphics.GetMargin();
        }

        public override void SetPadding()
        {
            Element.Padding = Graphics.GetPadding();
        }

        public override void SetAxisAppearance()
        {
            if (DataGrid.Axes.AxisX.Enabled)
            {
                Axis AxsX = new Axis();
                AxsX.Separator.OverridesDefaultStyle = true;

                if ((DataGrid.Axes.AxisX.Domain.T0 == 0) && (DataGrid.Axes.AxisX.Domain.T1 == 0))
                {
                    AxsX.MinValue = double.NaN;
                    AxsX.MaxValue = double.NaN;
                }
                else
                {
                    AxsX.MinValue = DataGrid.Axes.AxisX.Domain.T0;
                    AxsX.MaxValue = DataGrid.Axes.AxisX.Domain.T1;
                }

                if (DataGrid.Axes.AxisX.MinorSpacing > 0) { AxsX.Separator.Step = DataGrid.Axes.AxisX.MinorSpacing; } else { AxsX.Separator.Step = 1; }

                AxsX.Separator.Stroke = DataGrid.Graphics.GetStrokeBrush();
                AxsX.Separator.StrokeThickness = DataGrid.Graphics.StrokeWeight[0];

                AxsX.ShowLabels = DataGrid.Axes.AxisX.HasLabel;
                AxsX.LabelsRotation = DataGrid.Axes.AxisX.Angle;

                Element.AxisX.Clear();
                Element.AxisX.Add(AxsX);

            }

            if (DataGrid.Axes.AxisY.Enabled)
            {
                Axis AxsY = new Axis();
                AxsY.Separator.OverridesDefaultStyle = true;

                if ((DataGrid.Axes.AxisY.Domain.T0 == 0) && (DataGrid.Axes.AxisY.Domain.T1 == 0))
                {
                    AxsY.MinValue = double.NaN;
                    AxsY.MaxValue = double.NaN;
                }
                else
                {
                    AxsY.MinValue = DataGrid.Axes.AxisY.Domain.T0;
                    AxsY.MaxValue = DataGrid.Axes.AxisY.Domain.T1;
                }

                if (DataGrid.Axes.AxisY.MinorSpacing > 0) { AxsY.Separator.Step = DataGrid.Axes.AxisY.MinorSpacing; } else { AxsY.Separator.Step = 1; }

                AxsY.Separator.Stroke = DataGrid.Graphics.GetStrokeBrush();
                AxsY.Separator.StrokeThickness = DataGrid.Graphics.StrokeWeight[0];

                AxsY.ShowLabels = DataGrid.Axes.AxisY.HasLabel;
                AxsY.LabelsRotation = DataGrid.Axes.AxisY.Angle;



                Element.AxisY.Clear();
                Element.AxisY.Add(AxsY);

            }
        }

        public void SetAxisScale()
        {

        }


    }
}
