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
using Pollen.Utilities;
using System.Reflection;
using Wind.Presets;

namespace Pollen.Charts
{
    public class pPointChart : pChart
    {
        public System.Windows.Controls.Panel Element;
        public WindowsFormsHost ChartHost = new WindowsFormsHost();
        public Chart ChartObject = new Chart();
        public List<pPointSeries> ChartSeriesSet = new List<pPointSeries>();
        public ChartArea ChartsArea = new ChartArea();
        public bool Status = false;
        public bool IsSingle = true;

        public string Name = "";

        public pPointChart(string InstanceName)
        {
            Name = InstanceName;

            Element = new DockPanel();
            ChartHost = new WindowsFormsHost();
            ChartObject = new Chart();


            //Set Generic Chart Object & Properties
            ChartObject.AntiAliasing = AntiAliasingStyles.All;
            ChartObject.Dock = DockStyle.Fill;

            ChartObject.BackColor = System.Drawing.Color.Transparent;

            //Set WPF Winform Chart 
            ChartHost.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            ChartHost.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            //Set Element Properties;
            Element.MinWidth = 300;
            Element.MinHeight = 300;

            Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            Element.Name = Name;

            ChartHost.Child = ChartObject;
            Element.Children.Add(ChartHost);

            Type = "PointChart";

        }

        public void SetProperties(DataSetCollection PollenDataGrid)
        {
            //Set unique properties of the control
            DataSet = PollenDataGrid;

            ChartsArea = new ChartArea();
            ChartsArea.Name = Name;


            //Set Chart Area
            ChartsArea.InnerPlotPosition = new ElementPosition(0, 0, 100, 100);
            ChartsArea.Position = new ElementPosition(0, 0, 100, 100);

            ChartObject.ChartAreas.Clear();
            ChartObject.ChartAreas.Add(ChartsArea);
            
            ChartsArea.BackColor = System.Drawing.Color.Transparent;
        }
        
        public void SetXaxis(wDomain Bounds)
        {
            ChartsArea.AxisX.Minimum = Bounds.T0;
            ChartsArea.AxisX.Maximum = Bounds.T1;
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
                DataSeries[i].ChartSeries.ChartArea = ChartsArea.Name;
                ChartObject.Series[i] = DataSeries[i].ChartSeries;
                ChartObject.Series[i]["DrawSideBySide"] = "Auto";
            }
        }

        public override void SetAxisAppearance()
        {
            
            SetAxisScale();

            wColor HalfTone = new wColor(DataSet.Graphics.StrokeColor);
            HalfTone.Lighten(0.5);

            ChartArea A = ChartObject.ChartAreas[0];
            wAxis X = DataSet.Axes;
            wGraphic G = DataSet.Graphics;
            wFont F = G.FontObject;

            A.AxisX.Interval = 1;
            A.AxisY.Interval = 0;

            // ==================== X Axis Formatting ==================== 
            if (X.HasXAxis){A.AxisX.Enabled = AxisEnabled.True; } else { A.AxisX.Enabled = AxisEnabled.False; }
            A.AxisX.LineColor =G.StrokeColor.ToDrawingColor();
            A.AxisX.MajorTickMark.Enabled = true;
            A.AxisX.MajorTickMark.LineWidth = (int)G.StrokeWeight[0];
            A.AxisX.MajorTickMark.LineColor = G.StrokeColor.ToDrawingColor();
            A.AxisX.MinorTickMark.Enabled = false;

            //X Major Grid Formatting
            A.AxisX.MajorGrid.Enabled = X.HasXGrid;
            A.AxisX.MajorGrid.Interval = 1;
            A.AxisX.MajorGrid.LineWidth = (int)G.StrokeWeight[0];
            A.AxisX.MajorGrid.LineColor = G.StrokeColor.ToDrawingColor();

            //X Label Formatting
            A.AxisX.LabelStyle.Enabled = X.HasXLabel;
            A.AxisX.LabelStyle.Interval = 1;
            A.AxisX.LabelStyle.Font = F.ToDrawingFont().FontObject;
            A.AxisX.LabelStyle.ForeColor = F.FontColor.ToDrawingColor();
            A.AxisX.LabelStyle.Angle = (int)X.XAngle;

            //X Minor Grid Formatting
            A.AxisX.MinorGrid.Enabled = (X.XGridSpacing!=0);
            A.AxisX.MinorGrid.LineWidth = (int)G.StrokeWeight[0];
            A.AxisX.MinorGrid.LineColor = HalfTone.ToDrawingColor();
            A.AxisX.MinorGrid.Interval = A.AxisX.MajorGrid.Interval / X.XGridSpacing;

            // ==================== Y Axis Formatting ==================== 
            if (X.HasYAxis) { A.AxisY.Enabled = AxisEnabled.True; } else { A.AxisY.Enabled = AxisEnabled.False; }
            A.AxisY.LineColor = G.StrokeColor.ToDrawingColor();
            A.AxisY.MajorTickMark.Enabled = true;
            A.AxisY.MajorTickMark.LineWidth = (int)G.StrokeWeight[0];
            A.AxisY.MajorTickMark.LineColor = G.StrokeColor.ToDrawingColor();
            A.AxisY.MinorTickMark.Enabled = false;

            //Y Major Grid Formatting
            A.AxisY.MajorGrid.Enabled = X.HasYGrid;
            A.AxisY.MajorGrid.Interval = 0;
            A.AxisY.MajorGrid.LineWidth = (int)G.StrokeWeight[0];
            A.AxisY.MajorGrid.LineColor = G.StrokeColor.ToDrawingColor();

            //Y Label Formatting
            A.AxisY.LabelStyle.Enabled = X.HasYLabel;
            A.AxisY.LabelStyle.Interval = 0;
            A.AxisY.LabelStyle.Font = F.ToDrawingFont().FontObject;
            A.AxisY.LabelStyle.ForeColor = F.FontColor.ToDrawingColor();
            A.AxisY.LabelStyle.IntervalType = DateTimeIntervalType.Auto;
            A.AxisY.LabelStyle.Angle = (int)X.YAngle;

            //Y Minor Grid Formatting
            A.AxisY.MinorGrid.Enabled = (X.YGridSpacing != 0);
            A.AxisY.MinorGrid.LineWidth = (int)G.StrokeWeight[0];
            A.AxisY.MinorGrid.LineColor = HalfTone.ToDrawingColor();
            A.AxisY.MinorGrid.Interval = A.AxisY.MajorGrid.Interval / X.YGridSpacing;

            ChartObject.ChartAreas[0] = A;
        }

        public void SetAxisScale()
        {
                    if ((DataSet.Axes.DomainX.T0 == 0) & (DataSet.Axes.DomainY.T1 == 0))
                    {
                        ChartsArea.AxisY.Maximum = Double.NaN;
                    }
                    else
                    {
                        ChartsArea.AxisY.Minimum = DataSet.Axes.DomainY.T0;
                        ChartsArea.AxisY.Maximum = DataSet.Axes.DomainY.T1;
                    }

           ChartsArea.RecalculateAxesScale();
        }

        public override void SetThreeDView()
        {
            ChartsArea.Area3DStyle.Enable3D = View.Is3D;

            ChartsArea.Area3DStyle.Rotation = View.Pivot % 180;
            ChartsArea.Area3DStyle.Inclination = View.Tilt % 90;
            ChartsArea.Area3DStyle.Perspective = View.Distance%100;

            ChartsArea.Area3DStyle.WallWidth = 0;

            ChartsArea.Area3DStyle.LightStyle = ((LightStyle)View.Light);

        }

        public void SetPyramidThreeDView(int Mode)
        {
            ChartsArea.Area3DStyle.Enable3D = DataSet.View.Is3D;

            switch (Mode)
            {
                case 0:
            ChartObject.Series[0]["Funnel3DRotationAngle"] = Convert.ToString(DataSet.View.Tilt % 10);
                    break;
                case 1:
                    ChartObject.Series[0]["Pyramid3DRotationAngle"] = Convert.ToString(DataSet.View.Tilt % 10 );
                    break;
            }

            ChartsArea.Area3DStyle.WallWidth = 0;
            ChartsArea.BackColor = new wColors().Transparent().ToDrawingColor();

            ChartsArea.Area3DStyle.LightStyle = ((LightStyle)DataSet.View.Light);

        }

        public static void SetDoubleBuffered(System.Windows.Forms.Control control)
        {
            // set instance non-public property with name "DoubleBuffered" to true
            typeof(System.Windows.Forms.Control).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, control, new object[] { true });

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
            ChartObject.BackColor = DataSet.Graphics.Background.ToDrawingColor();
        }

        public void SetCorners(wGraphic Graphic)
        {
        }

        public void SetFont(wGraphic Graphic)
        {
        }
    }
}
