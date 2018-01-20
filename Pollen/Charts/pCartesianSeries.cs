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
        public LiveCharts.Wpf.Series ChartSequence = new RowSeries();
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
                wDomain SD = SortDomain(D.Domain.Item1,D.Domain.Item2);
                DmList.Add(new GanttPoint(SD.T0, SD.T1));
                RowSeries TempSeries = new RowSeries { Values = new ChartValues<GanttPoint> { new GanttPoint(SD.T0, SD.T1) } };
                ChartSeries.Add(TempSeries);
            }

            ChartSequence.Values = new ChartValues<GanttPoint>(DmList.ToArray());
        }

        public void SetScatterSeries(DataPtSet PollenDataSet, SeriesChartType ChartMode, int PointMarker)
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
                        PointGeometry = GetGeometry(PointMarker)
                    };

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
                            PointGeometry = GetGeometry(PointMarker)
                        };
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

            switch (ChartType)
            {
                case SeriesChartType.ColumnAdjacent:
                    ChartSequence = AdjacentCol;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        ColumnSeries NewColSeries = new ColumnSeries { Values = new ChartValues<double> { D.Number } };
                        NewColSeries.LabelsPosition = BarLabelPosition.Parallel;
                        ChartSeries.Add(NewColSeries);
                    }
                    break;

                case SeriesChartType.ColumnStack:
                    StackCol.StackMode = StackMode.Values;
                    ChartSequence = StackCol;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        ChartSeries.Add(new StackedColumnSeries { Values = new ChartValues<double> { D.Number } });
                    }
                    break;

                case SeriesChartType.ColumnStack100:
                    StackCol.StackMode = StackMode.Percentage;
                    ChartSequence = StackCol;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        ChartSeries.Add(new StackedColumnSeries { Values = new ChartValues<double> { D.Number } });
                    }
                    break;

                case SeriesChartType.BarAdjacent:
                    ChartSequence = AdjacentRow;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        RowSeries NewRowSeries = new RowSeries { Values = new ChartValues<double> { D.Number } };
                        NewRowSeries.LabelsPosition = BarLabelPosition.Parallel;
                        ChartSeries.Add(NewRowSeries);
                    }
                    break;

                case SeriesChartType.BarStack:
                    StackRow.StackMode = StackMode.Values;
                    ChartSequence = StackRow;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        ChartSeries.Add(new StackedRowSeries { Values = new ChartValues<double> { D.Number } });
                    }
                    break;

                case SeriesChartType.BarStack100:
                    StackRow.StackMode = StackMode.Percentage;
                    ChartSequence = StackRow;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        ChartSeries.Add(new StackedRowSeries { Values = new ChartValues<double> { D.Number } });
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

            switch (ChartType)
            {
                case SeriesChartType.Point:

                    ChartSequence = (SetPointFormat(tLineSeries, DataList.Points[0].Marker));
                    foreach (DataPt D in DataList.Points)
                    {
                        LineSeries NewLineSeries = new LineSeries { Values = new ChartValues<double> { D.Number } };
                        
                        DbList.Add(D.Number);
                        ChartSeries.Add(SetPointFormat(NewLineSeries, D.Marker));
                    }
                    break;

                case SeriesChartType.Line:
                    tLineSeries.Fill = Brushes.Transparent;
                    tLineSeries.LineSmoothness = 0;
                    ChartSequence = tLineSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        ChartSeries.Add(new LineSeries { Values = new ChartValues<double> { D.Number } });
                    }
                    break;

                case SeriesChartType.Spline:
                    tLineSeries.Fill = Brushes.Transparent;
                    ChartSequence = tLineSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        ChartSeries.Add(new LineSeries { Values = new ChartValues<double> { D.Number } });
                    }
                    break;

                case SeriesChartType.StepLine:
                    StepLineSeries StepSeries = new StepLineSeries();
                    StepSeries.AlternativeStroke = DataList.Points[0].Graphics.GetStrokeBrush();
                    ChartSequence = StepSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        ChartSeries.Add(new StepLineSeries { Values = new ChartValues<double> { D.Number } });
                    }
                    break;

                case SeriesChartType.Area:
                    tLineSeries.StrokeThickness = 0;
                    tLineSeries.LineSmoothness = 0;
                    ChartSequence = tLineSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        ChartSeries.Add(new StepLineSeries { Values = new ChartValues<double> { D.Number } });
                    }
                    break;

                case SeriesChartType.AreaStack:
                    tAreaSeries.LineSmoothness = 0;
                    ChartSequence = tAreaSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        ChartSeries.Add(new StackedAreaSeries { Values = new ChartValues<double> { D.Number } });
                    }
                    break;

                case SeriesChartType.AreaStack100:
                    tAreaSeries.LineSmoothness = 0;
                    tAreaSeries.StackMode = StackMode.Percentage;
                    ChartSequence = tAreaSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        ChartSeries.Add(new StackedAreaSeries { Values = new ChartValues<double> { D.Number } });
                    }
                    break;

                case SeriesChartType.SplineArea:
                    tLineSeries.StrokeThickness = 0;
                    ChartSequence = tLineSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        ChartSeries.Add(new StepLineSeries { Values = new ChartValues<double> { D.Number } });
                    }
                    break;

                case SeriesChartType.SplineAreaStack:
                    ChartSequence = tAreaSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        ChartSeries.Add(new StackedAreaSeries { Values = new ChartValues<double> { D.Number } });
                    }
                    break;

                case SeriesChartType.SplineAreaStack100:
                    tAreaSeries.StackMode = StackMode.Percentage;
                    ChartSequence = tAreaSeries;
                    foreach (DataPt D in DataList.Points)
                    {
                        DbList.Add(D.Number);
                        ChartSeries.Add(new StackedAreaSeries { Values = new ChartValues<double> { D.Number } });
                    }
                    break;

            }
            ChartSequence.Values = new ChartValues<double>(DbList.ToArray());
            //Set unique properties of the control

        }

        public LineSeries SetPointFormat(LineSeries NewLineSeries, wMarker Marker)
        {
            NewLineSeries.Fill = Brushes.Transparent;
            NewLineSeries.PointGeometry = GetGeometry((int)Marker.Mode);
            NewLineSeries.PointGeometrySize = Marker.Radius;
            NewLineSeries.PointForeground = Marker.Graphics.GetBackgroundBrush();

            return NewLineSeries;
        }

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
            
            Sequence.DataLabels = true;
            Sequence.LabelPoint = point => Pt.Label.Content;
            
            Sequence.Foreground = G.GetFontBrush();
            Sequence.FontFamily = G.FontObject.ToMediaFont().Family;
            Sequence.FontSize = G.FontObject.Size;
            Sequence.FontStyle = G.FontObject.ToMediaFont().Italic;
            Sequence.FontWeight = G.FontObject.ToMediaFont().Bold;

            Sequence.Fill = G.GetBackgroundBrush();

            Sequence.StrokeThickness = G.StrokeWeight[0];
            Sequence.Stroke = G.GetStrokeBrush();

            return Sequence;
        }

        private wDomain SortDomain(double T0, double T1)
        {
            wDomain OutputDomain = new wDomain(T0,T1);

            if (T0 > T1) { OutputDomain = new wDomain(T1, T0); }

            return OutputDomain;
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
