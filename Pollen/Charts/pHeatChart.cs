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

using Wind.Containers;

using Pollen.Collections;
using System.Windows.Media;
using LiveCharts.Wpf;
using Wind.Types;

namespace Pollen.Charts
{
    public class pHeatChart : pChart
    {
        public CartesianChart Element;
        public List<pPointSeries> ChartSeriesSet;
        public bool Status;

        public DataSetCollection DataGrid;
        public HeatSeries ChartSeries;
        public SeriesCollection SeriesCollect;

        public pHeatChart(string InstanceName)
        {
            //Set Element info setup
            Element = new CartesianChart();
            Element.DisableAnimations = true;

            DataGrid = new DataSetCollection();
            ChartSeries = new HeatSeries();
            SeriesCollect = new SeriesCollection();

            ChartSeries.Values = new ChartValues<HeatPoint>();
            ChartSeries.DataLabels = true;

            Element.Series.Add(ChartSeries);

            Element.Name = InstanceName;

        }

        public void SetGradient(wGraphic Graphics)
        {
            ChartSeries.GradientStopCollection = Graphics.Gradient.ToMediaGradient();
        }

        public void SetProperties(DataSetCollection PollenDataGrid)
        {
            //Set unique properties of the control
            DataGrid = PollenDataGrid;

            Element.MinWidth = 300;
            Element.MinHeight = 300;

            Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            Element.Background = Brushes.Transparent;
            
        }

        public void SetPollenSeries()
        {
            List<double> NumberSeries = new List<double>();
            List<int> U = new List<int>();
            List<int> V = new List<int>();

            //Populate Data Set
            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    NumberSeries.Add(DataGrid.Sets[i].Points[j].Number);
                    U.Add(i);
                    V.Add(j);
                }
            }

            int C = Element.Series[0].Values.Count;
            int S = NumberSeries.Count;

            //SERIES########################################
            // Remove all extra series

            for (int i = C; i < S; i++)
            {
                Element.Series[0].Values.Add(new HeatPoint(U[i],V[i],NumberSeries[i]));
            }

            C = Element.Series[0].Values.Count;

            if (C > S)
            {
                for (int i = S; i < C; i++)
                {
                    Element.Series[0].Values.RemoveAt(Element.Series.Count - 1);
                }
            }

            for (int i = 0; i < S; i++)
            {
                Element.Series[0].Values[i] = new HeatPoint(U[i], V[i], NumberSeries[i]);
            }

        }

        public void ForceRefresh()
        {
            Element.Update(false, true);
        }

        public void SetHeatData()
        {
            List<HeatPoint> DataCollection = new List<HeatPoint>();



            for (int i = 0; i < DataGrid.Sets.Count; i++)
            {
                for (int j = 0; j < DataGrid.Sets[i].Points.Count; j++)
                {
                    DataPt D = DataGrid.Sets[i].Points[j];
                    DataCollection.Add(new HeatPoint(i, j, D.Number));
                }
            }
            ChartSeries.Values.Clear();
            ChartSeries.Values.AddRange(DataCollection);

            SeriesCollect.Clear();
            SeriesCollect.Add(ChartSeries);
            Element.Series = SeriesCollect;

        }

        public void SetFormatting()
        {
            DataPt Pt = DataGrid.Sets[0].Points[0];
            wGraphic G = Pt.Graphics;

            ChartSeries.DataLabels = Pt.Label.Enabled;
            ChartSeries.LabelPoint = point => string.Format("{0:" + Pt.Label.Format + "}", point.Weight);

            ChartSeries.Foreground = G.GetFontBrush();
            ChartSeries.FontFamily = G.FontObject.ToMediaFont().Family;
            ChartSeries.FontSize = G.FontObject.Size;
            ChartSeries.FontStyle = G.FontObject.ToMediaFont().Italic;
            ChartSeries.FontWeight = G.FontObject.ToMediaFont().Bold;

            ChartSeries.StrokeThickness = G.StrokeWeight[0];
            ChartSeries.Stroke = G.GetStrokeBrush();
            ChartSeries.StrokeDashArray = new DoubleCollection(G.StrokePattern);

            ChartSeries.PointGeometry = GetGeometry((int)Pt.Marker.Mode);
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


        // ################################# OVERIDE GRAPHIC PROPERTIES #################################
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

        public void SetCorners(wGraphic Graphic)
        {
        }

        public override void SetAxisAppearance()
        {

            if (DataGrid.Axes.AxisX.Enabled)
            {

                Axis AxsX = new Axis();

                AxsX.Separator.OverridesDefaultStyle = true;

                if ((DataGrid.Axes.AxisX.Domain.T0 == 0) && (DataGrid.Axes.AxisX.Domain.T0 == 1))
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

                if ((DataGrid.Axes.AxisY.Domain.T0 == 0) && (DataGrid.Axes.AxisY.Domain.T0 == 1))
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

    }
}
