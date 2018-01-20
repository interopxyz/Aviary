using System;


using System.Windows.Forms.DataVisualization.Charting;

using Wind.Containers;


using Pollen.Collections;

namespace Pollen.Charts
{
    public class pPointSeries : pChart
    {
        public Series ChartSeries;
        public DataPtSet DataList;

        public ChartTypes ChartType = ChartTypes.Point;

        public bool Status;
        private bool StrokeMode = false;

        public pPointSeries(string InstanceName)
        {
            ChartSeries = new Series();
            
            ChartSeries.AxisLabel = InstanceName;
            ChartSeries.XAxisType = AxisType.Primary;
            ChartSeries.LegendText = InstanceName;
            
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
            Mode = Mode % 2;
            ChartType = (ChartTypes)(10+Mode);

            int[] Indices = { 26, 25 };
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
            ChartType = (ChartTypes)(12 + Mode);

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

        public void SetNumberChartType(int Mode, int StackMode, bool IsLineBased)
        {
            Mode = Mode % 9;
            ChartType = (ChartTypes)Mode;

            StrokeMode = IsLineBased; 

            switch (StackMode)
            {
                case 0:
                    int[] IndicesA = { 0, 7, 10, 3, 5, 4, 13, 14, 0 };
                    ChartSeries.ChartType = ((SeriesChartType)IndicesA[Mode]);
                    break;
                case 1:
                    int[] IndicesB = { 0, 8, 11, 3, 5, 4, 15, 14, 0 };
                    ChartSeries.ChartType = ((SeriesChartType)IndicesB[Mode]);
                    break;
                case 2:
                    int[] IndicesC = { 0, 9, 12, 3, 5, 4, 16, 14, 0 };
                    ChartSeries.ChartType = ((SeriesChartType)IndicesC[Mode]);
                    break;
            }

        }

        public void SetRangeChartType(int Mode)
        {
            Mode = Mode % 5;
            ChartType = (ChartTypes)(Mode+14);
            int[] Indices = { 21, 22, 2, 24, 23 };
            ChartSeries.ChartType = ((SeriesChartType)Indices[Mode]);
        }

        public void SetChartLabels( wLabel NewLabel)
        {
            int Mode = (int)NewLabel.Position;
            ChartSeries.SmartLabelStyle.Enabled = NewLabel.HasLeader;
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
            ChartSeries.SmartLabelStyle.Enabled = DataList.Points[0].Label.HasLeader;
            ChartSeries.SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes;
            //ChartSeries.SmartLabelStyle.MovingDirection = (LabelAlignmentStyles)DataList.Points[0].Label.GetPositionIndex();
            ChartSeries.SmartLabelStyle.IsMarkerOverlappingAllowed = false;
            ChartSeries.SmartLabelStyle.MaxMovingDistance = 600;
            ChartSeries.SmartLabelStyle.CalloutStyle = LabelCalloutStyle.Box;
            ChartSeries.SmartLabelStyle.CalloutLineAnchorCapStyle = LineAnchorCapStyle.None;
            

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
            
            //Assign formatting to each point
            for (int i = 0; i < DataList.Count; i++)
            {
                DataPt D = DataList.Points[i];
                wGraphic G = D.Graphics;

                DataPoint Pt = new DataPoint();
                
                //Set Point [Fill / Line] Color
                if (StrokeMode) { Pt.Color = D.Graphics.StrokeColor.ToDrawingColor(); } else { Pt.Color = D.Graphics.Background.ToDrawingColor(); }

                //Set Line & Border Properties
                Pt.BorderColor = G.StrokeColor.ToDrawingColor();
                Pt.BorderWidth = (int)G.StrokeWeight[0];

                //Set Label Properties
                Pt.Label = D.Label.Content;
                Pt.Font = G.FontObject.ToDrawingFont().FontObject;
                
                Pt.LabelAngle = (int)G.FontObject.Angle;
                Pt.LabelForeColor = G.FontObject.FontColor.ToDrawingColor();

                //  Label Frame
                Pt["LabelStyle"] = D.Label.GetLabelAlignment();
                Pt["BarLabelStyle"] = D.Label.GetBarAlignment();
                Pt.LabelBackColor = D.Label.Graphics.Background.ToDrawingColor();
                Pt.LabelBorderColor = D.Label.Graphics.StrokeColor.ToDrawingColor();
                Pt.LabelBorderWidth = (int)D.Label.Graphics.StrokeWeight[0];
                
                //Set Marker Properties
                Pt = SetMarkerType(Pt, (int)D.Marker.Mode);
                Pt.MarkerSize = D.Marker.Radius;
                Pt.MarkerColor = D.Marker.Graphics.Background.ToDrawingColor();
                Pt.MarkerBorderWidth = (int)D.Marker.Graphics.StrokeWeight[0];
                Pt.MarkerBorderColor = D.Marker.Graphics.StrokeColor.ToDrawingColor();

                ChartSeries.Points[i] = Pt;
            }
            
            ChartSeries["PointWidth"] = Convert.ToString(DataList.BarScale % 1.00001);

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
                        ChartSeries.Points[i].XValue = i;
                    }
                    ChartSeries["PointWidth"] = Convert.ToString(DataList.Points[0].Graphics.Width);
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
                case 4:
                    for (int i = 0; i < DataList.Count; i++)
                    {
                        DataPt D = DataList.Points[i];
                        double[] X = { D.Number };
                        ChartSeries.Points[i].YValues = X;
                        ChartSeries.Points[i].XValue = i;
                    }
                    break;
            }
            

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
