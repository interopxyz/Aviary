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

using Pollen.Collections;

namespace Pollen.Charts
{
    public class pPointChart : pChart
    {
        public System.Windows.Controls.Panel Element;
        public WindowsFormsHost ChartHost;
        public Chart ChartObject;
        public List<pPointSeries> ChartSeriesSet;
        public ChartArea ChartAreas;
        public string Type;
        public bool Status;
        public DataSetCollection DataGrid = new DataSetCollection();

        public pPointChart(string InstanceName)
        {
            //Set Element info setup
            Element = new StackPanel();
            ChartHost = new WindowsFormsHost();
            ChartObject = new Chart();
            ChartAreas = new ChartArea();

            ChartObject.AntiAliasing = AntiAliasingStyles.All;
            ChartAreas.Name = InstanceName;
            
            ChartObject.ChartAreas.Add(ChartAreas);

            ChartHost.Child = ChartObject;

            Element.Children.Add(ChartHost);
            Element.Name = InstanceName;
            Type = "PointChart";

            //Set "Clear" appearance to all elements

        }

        public void SetProperties(DataSetCollection PollenDataGrid)
        {
            //Set unique properties of the control
            DataGrid = PollenDataGrid;

            ChartObject.Width = (int)DataGrid.Graphics.Width;
            ChartObject.Height = (int)DataGrid.Graphics.Height;
            
            SetAxisScale();

            Element.Width = (int)DataGrid.Graphics.Width;
            Element.Height = (int)DataGrid.Graphics.Height;
        }
        
        public void SetSeries(List<pPointSeries> DataSeries)
        {
            int cnt = (DataSeries.Count - ChartObject.Series.Count);

            //Add more data points if the total number of values is higher than the current number of data points in the chart
            if (DataSeries.Count > ChartObject.Series.Count)
            {
                for (int i = 0; i < cnt; i++)
                {
                    ChartObject.Series.Add(new Series());
                }
            }

            cnt = (ChartObject.Series.Count - DataSeries.Count);

            //Remove data points if the total number of values is less than the current number of data points in the chart
            if (ChartObject.Series.Count > DataSeries.Count)
            {
                for (int i = 0; i < cnt; i++)
                {
                    ChartObject.Series.RemoveAt(ChartObject.Series.Count - 1);
                }
            }

            cnt = DataSeries.Count;
            for (int i = 0; i < cnt; i++)
            {
                DataSeries[i].ChartSeries.ChartArea = ChartAreas.Name;
                ChartObject.Series[i] = DataSeries[i].ChartSeries;
            }
        }

        public void SetAxisAppearance()
        {
            wColor HalfTone = new wColor(DataGrid.Graphics.StrokeColor);
            HalfTone.Lighten(0.5);

            // ==================== X Axis Formatting ==================== 
            //if (DataGrid.HasXAxis){ChartAreas.AxisX.Enabled = AxisEnabled.True; }
            ChartAreas.AxisX.LineColor = DataGrid.Graphics.StrokeColor.ToDrawingColor();
            ChartAreas.AxisX.MajorTickMark.Enabled = false;
            ChartAreas.AxisX.MinorTickMark.Enabled = false;

            //X Label Formatting
            ChartAreas.AxisX.LabelStyle.Enabled = DataGrid.HasXLabel;
            ChartAreas.AxisX.LabelStyle.Font = DataGrid.Fonts.ToDrawingFont().FontObject;
            ChartAreas.AxisX.LabelStyle.ForeColor = DataGrid.Fonts.FontColor.ToDrawingColor();

            //X Major Grid Formatting
            ChartAreas.AxisX.MajorGrid.Enabled = DataGrid.HasXGrid;
            ChartAreas.AxisX.MajorGrid.LineWidth = (int)DataGrid.Graphics.StrokeWeight[0];
            ChartAreas.AxisX.MajorGrid.LineColor = DataGrid.Graphics.StrokeColor.ToDrawingColor();

            //X Minor Grid Formatting
            ChartAreas.AxisX.MinorGrid.Enabled = (DataGrid.XGridSpacing!=0);
            ChartAreas.AxisX.MinorGrid.LineWidth = (int)DataGrid.Graphics.StrokeWeight[0];
            ChartAreas.AxisX.MinorGrid.LineColor = HalfTone.ToDrawingColor();
            ChartAreas.AxisX.MinorGrid.Interval = ChartAreas.AxisX.MajorGrid.Interval / DataGrid.XGridSpacing;

            // ==================== Y Axis Formatting ==================== 
            //if (DataGrid.HasYAxis) { ChartAreas.AxisY.Enabled = AxisEnabled.True; }
            ChartAreas.AxisY.LineColor = DataGrid.Graphics.StrokeColor.ToDrawingColor();
            ChartAreas.AxisY.MajorTickMark.Enabled = false;
            ChartAreas.AxisY.MinorTickMark.Enabled = false;

            //Y Label Formatting
            ChartAreas.AxisY.LabelStyle.Enabled = DataGrid.HasYLabel;
            ChartAreas.AxisY.LabelStyle.Font = DataGrid.Fonts.ToDrawingFont().FontObject;
            ChartAreas.AxisY.LabelStyle.ForeColor = DataGrid.Fonts.FontColor.ToDrawingColor();

            //Y Major Grid Formatting
            ChartAreas.AxisY.MajorGrid.Enabled = DataGrid.HasYGrid;
            ChartAreas.AxisY.MajorGrid.LineWidth = (int)DataGrid.Graphics.StrokeWeight[0];
            ChartAreas.AxisY.MajorGrid.LineColor = DataGrid.Graphics.StrokeColor.ToDrawingColor();

            //Y Minor Grid Formatting
            ChartAreas.AxisY.MinorGrid.Enabled = (DataGrid.YGridSpacing != 0);
            ChartAreas.AxisY.MinorGrid.LineWidth = (int)DataGrid.Graphics.StrokeWeight[0];
            ChartAreas.AxisY.MinorGrid.LineColor = HalfTone.ToDrawingColor();
            ChartAreas.AxisY.MinorGrid.Interval = ChartAreas.AxisY.MajorGrid.Interval / DataGrid.YGridSpacing;

        }

        public void SetAxisScale()
        {

                    if ((DataGrid.YAxisMin == 0) & (DataGrid.YAxisMax == 0))
                    {
                        ChartAreas.AxisY.Maximum = Double.NaN;
                    }
                    else
                    {
                        ChartAreas.AxisY.Minimum = DataGrid.YAxisMin;
                        ChartAreas.AxisY.Maximum = DataGrid.YAxisMax;
                    }
           ChartAreas.RecalculateAxesScale();

        }

        public void SetThreeDView()
        {
            ChartAreas.Area3DStyle.Enable3D = DataGrid.EnableThreeD;

            ChartAreas.Area3DStyle.Rotation = DataGrid.RotateX%180;
            ChartAreas.Area3DStyle.Inclination = DataGrid.RotateY%90;
            ChartAreas.Area3DStyle.Perspective = DataGrid.Perspective%100;

            ChartAreas.Area3DStyle.WallWidth = 0;
            ChartAreas.BackColor = new wColor().Transparent().ToDrawingColor();

            DataGrid.LightingStyle = DataGrid.LightingStyle % 3;
            int[] Indices = { 0, 1, 2 };
            ChartAreas.Area3DStyle.LightStyle = ((LightStyle)Indices[DataGrid.LightingStyle]);

        }

        public void SetPyramidThreeDView(int Mode)
        {
            ChartAreas.Area3DStyle.Enable3D = DataGrid.EnableThreeD;

            switch (Mode)
            {
                case 0:
            ChartObject.Series[0]["Funnel3DRotationAngle"] = Convert.ToString((int)DataGrid.RotateY % 10);
                    break;
                case 1:
                    ChartObject.Series[0]["Pyramid3DRotationAngle"] = Convert.ToString((int)DataGrid.RotateY%10 );
                    break;
            }

            ChartAreas.Area3DStyle.WallWidth = 0;
            ChartAreas.BackColor = new wColor().Transparent().ToDrawingColor();

            DataGrid.LightingStyle = DataGrid.LightingStyle % 3;
            int[] Indices = { 0, 1, 2 };
            ChartAreas.Area3DStyle.LightStyle = ((LightStyle)Indices[DataGrid.LightingStyle]);

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
