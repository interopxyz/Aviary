using System;

using System.Windows;
using System.Windows.Media;

using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;
using System.Windows.Forms;

using System.Windows.Controls;
using System.Collections.Generic;

using Wind.Containers;
using Wind.Types;

using Pollen;

using LiveCharts.WinForms;

using Pollen.Collections;

namespace Pollen.Charts
{
    public class pPointSeries : pChart
    {
        public Series ChartSeries;
        public DataPtSet DataList;

        public string Type;
        public bool Status;

        public pPointSeries(string InstanceName)
        {
            ChartSeries = new Series();

            //Set Element info setup
            Type = "PointSeries";
        }

        public void SetProperties(DataPtSet PollenDataSet)
        {
            DataList = PollenDataSet;
        
            //Set unique properties of the control
        }

        public DataPoint SetMarkerType(DataPoint Pt, int Mode)
        {
            Mode = Mode % 6;
            int[] Indices = { 0,2, 1, 3, 4, 5 };
            Pt.MarkerStyle = ((MarkerStyle)Indices[Mode]);
            
            return Pt;
        }

        public void SetRadialChartType(int Mode)
        {
            Mode = Mode % 4;
            int[] Indices = { 17, 18, 26, 25 };
            ChartSeries.ChartType = ((SeriesChartType)Indices[Mode]);

        }

        public void SetPointChartType(int Mode, int StackMode)
        {
            Mode = Mode % 1;
            int[] Indices = { 27 };
            ChartSeries.ChartType = ((SeriesChartType)Indices[Mode]);
        }

        public void SetStackChartType(int Mode, int JustificationMode)
        {
            Mode = Mode % 2;
            int[] Indices = { 33, 34 };
            ChartSeries.ChartType = ((SeriesChartType)Indices[Mode]);

            JustificationMode = JustificationMode % 5;
            string[] JustIndex = { "OutsideInColumn", "Outside", "Inside", "Outside", "OutsideInColumn" };
            string[] PosIndex = { "Left", "Left", "Right", "Right", "Right" };

            switch (Mode)
            {
                case 0:
                    ChartSeries["FunnelLabelStyle"] = JustIndex[JustificationMode];
                    ChartSeries["FunnelOutsideLabelPlacement"] = PosIndex[JustificationMode];
                    ChartSeries["FunnelInsideLabelPlacement"] = "Center";
                    break;
                case 1:
                    ChartSeries["PyramidLabelStyle"] = JustIndex[JustificationMode];
                    ChartSeries["PyramidOutsideLabelPlacement"] = PosIndex[JustificationMode];
                    ChartSeries["PyramidInsideLabelPlacement"] = "Center";
                    break;
            }

        }

        public void SetNumberChartType(int Mode, int StackMode)
        {
            switch (StackMode)
            {
                case 0:
                    Mode = Mode % 9;
                    int[] IndicesA = { 0, 7, 10, 3, 5, 4, 13, 14, 0 };
                    ChartSeries.ChartType = ((SeriesChartType)IndicesA[Mode]);
                    break;
                case 1:
                    Mode = Mode % 9;
                    int[] IndicesB = { 0, 8, 11, 3, 5, 4, 15, 14, 0 };
                    ChartSeries.ChartType = ((SeriesChartType)IndicesB[Mode]);
                    break;
                case 2:
                    Mode = Mode % 9;
                    int[] IndicesC = { 0, 9, 12, 3, 5, 4, 16, 14, 0 };
                    ChartSeries.ChartType = ((SeriesChartType)IndicesC[Mode]);
                    break;
            }

        }

        public void SetRangeChartType(int Mode)
        {
            Mode = Mode % 5;
            int[] Indices = { 21, 24, 23, 2 };
            ChartSeries.ChartType = ((SeriesChartType)Indices[Mode]);
        }

        public void SetChartLabels( int Mode, bool HasLeader)
        {
            ChartSeries.SmartLabelStyle.Enabled = HasLeader;
            ChartSeries.SmartLabelStyle.CalloutStyle = LabelCalloutStyle.None;

            Mode = Mode % 9;
            int[] Indices = { 2, 64, 128, 256, 8, 4, 1, 16, 32 };
            ChartSeries.SmartLabelStyle.MovingDirection = ((LabelAlignmentStyles)Indices[Mode]);

            ChartSeries.SmartLabelStyle.IsOverlappedHidden = false;
            ChartSeries.SmartLabelStyle.CalloutLineAnchorCapStyle = LineAnchorCapStyle.Round;
            ChartSeries.SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes;
        }
        
        public void SetNumericData(int Mode)
        {
            int cnt = (DataList.Count - ChartSeries.Points.Count);

            //Add more data points if the total number of values is higher than the current number of data points in the chart
            if (DataList.Count > ChartSeries.Points.Count)
            {
                for (int i = 0; i < cnt; i++)
                {
                    ChartSeries.Points.Add(new DataPoint());
                }
            }

            cnt = (ChartSeries.Points.Count - DataList.Count);

            //Remove data points if the total number of values is less than the current number of data points in the chart
            if (ChartSeries.Points.Count > DataList.Count)
            {
                for (int i = 0; i < cnt; i++)
                {
                    ChartSeries.Points.RemoveAt(ChartSeries.Points.Count - 1);
                }
            }
            
                for (int i = 0; i < DataList.Count; i++)
            {
                DataPt D = DataList.Points[i];
                DataPoint Pt = new DataPoint();

                Pt.Color = D.Graphics.Background.ToDrawingColor();

                Pt.Label = D.Label;
                Pt.LabelForeColor = D.Fonts.FontColor.ToDrawingColor();

                Pt.Font = D.Fonts.ToDrawingFont().FontObject;

                Pt.BorderColor = D.Graphics.StrokeColor.ToDrawingColor();
                Pt.BorderWidth = (int)D.Graphics.StrokeWeight[0];

                Pt = SetMarkerType(Pt, D.MarkerMode);
                Pt.MarkerBorderWidth = 0;
                Pt.MarkerColor = D.MarkerColor.ToDrawingColor();
                Pt.MarkerSize = D.MarkerSize;
                ChartSeries.Points[i] = Pt;
            }

            switch (Mode)
            {
                case 0:
                    for (int i = 0; i < DataList.Count; i++)
                    {
                        DataPt D = DataList.Points[i];
                        double[] X = { D.Number };
                        ChartSeries.Points[i].YValues = X;
                    }
                    break;
                case 1:
                    for (int i = 0; i < DataList.Count; i++)
                    {
                        DataPt D = DataList.Points[i];
                        double[] X = { D.Domain.Item1, D.Domain.Item2 };
                        ChartSeries.Points[i].YValues = X;
                    }
                    break;
                case 2:
                    double T = 0;
                    for (int i = 0; i < DataList.Count; i++)
                    {
                        T += DataList.Points[i].Number;
                    }
                    for (int i = 0; i < DataList.Count; i++)
                    {
                        DataPt D = DataList.Points[i];
                        double[] X = { (D.Number/ T)*100 };
                        ChartSeries.Points[i].YValues = X;
                    }
                    break;
                case 3:
                    for (int i = 0; i < DataList.Count; i++)
                    {
                        DataPt D = DataList.Points[i];
                        double[] X = { D.Point.X, D.Point.Y,D.Point.Z };
                        ChartSeries.Points[i].YValues = X;
                    }
                    break;
            }

            ChartSeries["PointWidth"] = Convert.ToString(DataList.Points[0].Graphics.Width % 1.00001);

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
