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
using System.Windows.Media;
using Wind.Types;

namespace Pollen.Charts
{

    public class pCartesianSeries : pChart
    {
        public List<LiveCharts.Wpf.Series> ChartSeries = new List<LiveCharts.Wpf.Series>();
        public LiveCharts.Wpf.Series ChartSequence = new LineSeries();
        public DataPtSet DataList = new DataPtSet();

        public enum SeriesChartType
        {
            Point, Line, StepLine, Spline,
            Area, AreaStack, AreaStack100, SplineArea, SplineAreaStack, SplineAreaStack100,
            ColumnAdjacent, ColumnStack, ColumnStack100, BarAdjacent, BarStack, BarStack100,
            Gantt, Bubble, Scatter
        }

        public SeriesChartType ChartType = SeriesChartType.Line;

        public pCartesianSeries(string InstanceName)
        {
            ChartSeries = new List<LiveCharts.Wpf.Series>();
            ChartSequence = new RowSeries();
            DataList = new DataPtSet();

            //Set Element info setup
            Type = "ScatterSequence";
        }

        public void SetGantSeries(DataPtSet PollenDataSet)
        {
            DataList = PollenDataSet;
            ChartType = SeriesChartType.Gantt;

            List<GanttPoint> DmList = new List<GanttPoint>();
            ChartSequence = new RowSeries();

            foreach (DataPt D in DataList.Points)
            {
                wDomain SD = SortDomain(D.Domain.Item1, D.Domain.Item2);
                DmList.Add(new GanttPoint(SD.T0, SD.T1));
                RowSeries TempSeries = new RowSeries { Values = new ChartValues<GanttPoint> { new GanttPoint(SD.T0, SD.T1) } };
                TempSeries = SetProperties(TempSeries, D);

                ChartSeries.Add(TempSeries);
            }

            ChartSequence.Values = new ChartValues<GanttPoint>(DmList.ToArray());
        }

        public void SetScatterSeries(DataPtSet PollenDataSet, SeriesChartType ChartMode)
        {
            DataList = PollenDataSet;
            ChartType = ChartMode;

            switch (ChartType)
            {
                default:
                    List<ScatterPoint> PtList = new List<ScatterPoint>();
                    ScatterSeries TempSequence = new ScatterSeries
                    {
                        MinPointShapeDiameter = Math.Abs(DataList.Points[0].Point.Z),
                        MaxPointShapeDiameter = Math.Abs(DataList.Points[0].Point.Z)
                    };

                    TempSequence = SetProperties(TempSequence, DataList.Points[0]);

                    ChartSequence = TempSequence;
                    foreach (DataPt D in DataList.Points)
                    {
                        PtList.Add(new ScatterPoint(D.Point.X, D.Point.Y, D.Point.Z));
                        double W = Math.Abs(D.Point.Z);

                        ScatterSeries TempSeries = new ScatterSeries
                        {
                            Values = new ChartValues<ScatterPoint> { new ScatterPoint(D.Point.X, D.Point.Y, D.Point.Z) },
                            MinPointShapeDiameter = W,
                            MaxPointShapeDiameter = W,
                        };

                        TempSeries = SetProperties(TempSeries, D);

                        ChartSeries.Add(TempSeries);
                    }

                    ChartSequence.Values = new ChartValues<ScatterPoint>(PtList.ToArray());

                    break;
                case SeriesChartType.Scatter:
                    List<ObservablePoint> MkList = new List<ObservablePoint>();
                    ScatterSeries TmpSqnc = new ScatterSeries
                    {
                        MinPointShapeDiameter = Math.Abs(DataList.Points[0].Marker.Radius),
                        MaxPointShapeDiameter = Math.Abs(DataList.Points[0].Marker.Radius),
                        PointGeometry = GetGeometry((int)DataList.Points[0].Marker.Mode)
                    };

                    TmpSqnc = SetProperties(TmpSqnc, DataList.Points[0]);

                    ChartSequence = TmpSqnc;
                    foreach (DataPt D in DataList.Points)
                    {
                        MkList.Add(new ObservablePoint(D.Point.X, D.Point.Y));
                        double W = Math.Abs(D.Marker.Radius);
                        ScatterSeries TempSeries = new ScatterSeries
                        {
                            Values = new ChartValues<ObservablePoint> { new ObservablePoint(D.Point.X, D.Point.Y) },
                            MinPointShapeDiameter = W,
                            MaxPointShapeDiameter = W,
                            PointGeometry = GetGeometry((int)D.Marker.Mode)
                        };

                        TempSeries = SetProperties(TempSeries, D);

                        ChartSeries.Add(TempSeries);
                    }

                    ChartSequence.Values = new ChartValues<ObservablePoint>(MkList.ToArray());
                    break;
            }
        }

        public void SetBarSeries(DataPtSet PollenDataSet, SeriesChartType ChartMode)
        {
            DataList = PollenDataSet;
            ChartType = ChartMode;

            List<double> DbList = new List<double>();

            RowSeries AdjacentRow = new RowSeries();
            StackedRowSeries StackRow = new StackedRowSeries();
            ColumnSeries AdjacentCol = new ColumnSeries();
            StackedColumnSeries StackCol = new StackedColumnSeries();

            StackRow.LabelsPosition = BarLabelPosition.Parallel;
            StackCol.LabelsPosition = BarLabelPosition.Parallel;
            AdjacentCol.LabelsPosition = BarLabelPosition.Parallel;
            StackCol.LabelsPosition = BarLabelPosition.Parallel;

            AdjacentRow = (SetProperties(AdjacentRow, DataList.Points[0]));
            StackRow = (SetProperties(StackRow, DataList.Points[0]));
            AdjacentCol = (SetProperties(AdjacentCol, DataList.Points[0]));
            StackCol = (SetProperties(StackCol, DataList.Points[0]));


            switch (ChartType)
            {
                case SeriesChartType.ColumnAdjacent:
                    ChartSequence = AdjacentCol;
                    foreach (DataPt D in DataList.Points)
                    {
                        ColumnSeries NewSeries = new ColumnSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries.LabelsPosition = BarLabelPosition.Parallel;
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;

                case SeriesChartType.ColumnStack:
                    StackCol.StackMode = StackMode.Values;
                    ChartSequence = StackCol;
                    foreach (DataPt D in DataList.Points)
                    {
                        StackedColumnSeries NewSeries = new StackedColumnSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;

                case SeriesChartType.ColumnStack100:
                    StackCol.StackMode = StackMode.Percentage;
                    ChartSequence = StackCol;
                    foreach (DataPt D in DataList.Points)
                    {
                        StackedColumnSeries NewSeries = new StackedColumnSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;

                case SeriesChartType.BarAdjacent:
                    ChartSequence = AdjacentRow;
                    foreach (DataPt D in DataList.Points)
                    {
                        RowSeries NewSeries = new RowSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries.LabelsPosition = BarLabelPosition.Parallel;
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;

                case SeriesChartType.BarStack:
                    StackRow.StackMode = StackMode.Values;
                    ChartSequence = StackRow;
                    foreach (DataPt D in DataList.Points)
                    {
                        StackedRowSeries NewSeries = new StackedRowSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;

                case SeriesChartType.BarStack100:
                    StackRow.StackMode = StackMode.Percentage;
                    ChartSequence = StackRow;
                    foreach (DataPt D in DataList.Points)
                    {
                        StackedRowSeries NewSeries = new StackedRowSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;

            }
            ChartSequence.Values = new ChartValues<double>(DbList.ToArray());
            //Set unique properties of the control

        }

        public void SetLineSeries(DataPtSet PollenDataSet, SeriesChartType ChartMode)
        {
            DataList = PollenDataSet;
            ChartType = ChartMode;

            List<double> DbList = new List<double>();
            LineSeries tLineSeries = new LineSeries();
            StackedAreaSeries tAreaSeries = new StackedAreaSeries();
            tLineSeries.PointGeometry = DefaultGeometries.None;
            tLineSeries.Fill = null;
            tAreaSeries.PointGeometry = DefaultGeometries.None;
            tAreaSeries.Fill = null;

            tLineSeries = (SetProperties(tLineSeries, DataList.Points[0]));
            tAreaSeries = (SetProperties(tAreaSeries, DataList.Points[0]));

            switch (ChartType)
            {
                case SeriesChartType.Point:

                    ChartSequence = (SetProperties(tLineSeries, DataList.Points[0]));
                    foreach (DataPt D in DataList.Points)
                    {
                        LineSeries NewSeries = new LineSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;
                    
                case SeriesChartType.Line:

                    tLineSeries.Fill = Brushes.Transparent;
                    tLineSeries.LineSmoothness = 0;
                    ChartSequence = tLineSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        LineSeries NewSeries = new LineSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries = SetProperties(NewSeries, D);

                        ChartSeries.Add(NewSeries);
                    }
                    break;

                case SeriesChartType.Spline:
                    tLineSeries.Fill = Brushes.Transparent;
                    ChartSequence = tLineSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        LineSeries NewSeries = new LineSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;

                case SeriesChartType.StepLine:
                    StepLineSeries StepSeries = new StepLineSeries();
                    StepSeries.AlternativeStroke = DataList.Points[0].Graphics.GetStrokeBrush();
                    StepSeries = SetProperties(StepSeries, DataList.Points[0]);

                    ChartSequence = StepSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        StepLineSeries NewSeries = new StepLineSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;

                case SeriesChartType.Area:
                    ChartSequence = tLineSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        LineSeries NewSeries = new LineSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;

                case SeriesChartType.AreaStack:
                    tAreaSeries.LineSmoothness = 0;
                    ChartSequence = tAreaSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        StackedAreaSeries NewSeries = new StackedAreaSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;

                case SeriesChartType.AreaStack100:
                    tAreaSeries.LineSmoothness = 0;
                    tAreaSeries.StackMode = StackMode.Percentage;
                    ChartSequence = tAreaSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        StackedAreaSeries NewSeries = new StackedAreaSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;

                case SeriesChartType.SplineArea:
                    ChartSequence = tLineSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        LineSeries NewSeries = new LineSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;

                case SeriesChartType.SplineAreaStack:
                    ChartSequence = tAreaSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        StackedAreaSeries NewSeries = new StackedAreaSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;

                case SeriesChartType.SplineAreaStack100:
                    tAreaSeries.StackMode = StackMode.Percentage;
                    ChartSequence = tAreaSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        StackedAreaSeries NewSeries = new StackedAreaSeries { Values = new ChartValues<double> { D.Number } };
                        NewSeries = SetProperties(NewSeries, D);

                        DbList.Add(D.Number);
                        ChartSeries.Add(NewSeries);
                    }
                    break;

            }
            ChartSequence.Values = new ChartValues<double>(DbList.ToArray());
            //Set unique properties of the control

        }


        // ################################## MARKER SERIES ##################################
        // Bar Series
        public StackedRowSeries SetProperties(StackedRowSeries NewSeries, DataPt Pt)
        {
            NewSeries.LabelsPosition = GetPosition((int)Pt.Label.Alignment);

            return NewSeries;
        }
        public RowSeries SetProperties(RowSeries NewSeries, DataPt Pt)
        {
            NewSeries.LabelsPosition = GetPosition((int)Pt.Label.Alignment);

            return NewSeries;
        }
        public ColumnSeries SetProperties(ColumnSeries NewSeries, DataPt Pt)
        {
            NewSeries.LabelsPosition = GetPosition((int)Pt.Label.Alignment);

            return NewSeries;
        }
        public StackedColumnSeries SetProperties(StackedColumnSeries NewSeries, DataPt Pt)
        {
            NewSeries.LabelsPosition = GetPosition((int)Pt.Label.Alignment);

            return NewSeries;
        }

        // Line Series
        public StepLineSeries SetProperties(StepLineSeries NewSeries, DataPt Pt)
        {
            NewSeries.PointForeground = Pt.Marker.Graphics.GetBackgroundBrush();
            NewSeries.PointGeometrySize = Pt.Marker.Radius;

            return NewSeries;
        }
        public LineSeries SetProperties(LineSeries NewSeries, DataPt Pt)
        {
            NewSeries.PointForeground = Pt.Marker.Graphics.GetBackgroundBrush();
            NewSeries.PointGeometrySize = Pt.Marker.Radius;

            return NewSeries;
        }
        public StackedAreaSeries SetProperties(StackedAreaSeries NewSeries, DataPt Pt)
        {
            NewSeries.PointForeground = Pt.Marker.Graphics.GetBackgroundBrush();
            NewSeries.PointGeometrySize = Pt.Marker.Radius;

            return NewSeries;
        }

        //Scatter Series
        public ScatterSeries SetProperties(ScatterSeries NewSeries, DataPt Pt)
        {
            return NewSeries;
        }

        // ############################ ASSIGN SERIES PROPERTIES #############################
        public void SetSeriesProperties()
        {
            ChartSequence = SetSequenceProperties(DataList.Points[0], ChartSequence);

            for (int i = 0; i < DataList.Count; i++)
            {
                ChartSeries[i] = SetSequenceProperties(DataList.Points[i], ChartSeries[i]);
            }
        }

        public LiveCharts.Wpf.Series SetSequenceProperties(DataPt Pt, LiveCharts.Wpf.Series Sequence)
        {
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

            return Sequence;
        }

        private wDomain SortDomain(double T0, double T1)
        {
            wDomain OutputDomain = new wDomain(T0, T1);

            if (T0 > T1) { OutputDomain = new wDomain(T1, T0); }

            return OutputDomain;
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

        public void SetMarkerType()
        {

        }

        public void SetChartLabels(int Mode, bool HasLeader)
        {

        }

        public void SetCorners(wGraphic Graphic)
        {
            //Element.CornerRadius = new CornerRadius(Graphic.Radius[0], Graphic.Radius[1], Graphic.Radius[2], Graphic.Radius[3]);
        }

    }
}
