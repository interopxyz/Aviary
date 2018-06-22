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

namespace Pollen.Charts
{
    public class pRadialChart : pChart
    {
        public PieChart Element = new PieChart();
        public bool Status = false;
        public DataSetCollection DataGrid = new DataSetCollection();
        public List<pRadialSeries> PollenSeries = new List<pRadialSeries>();

        public pRadialChart(string InstanceName)
        {
            //Set Element info setup
            Element = new PieChart();
            Element.DisableAnimations = true;

            Element.Name = InstanceName;
            Element.Series.Clear();

            DataGrid = new DataSetCollection();

            //Set "Clear" appearance to all elements

        }

        public void SetProperties(DataSetCollection PollenDataGrid, double Radius)
        {
            //Set unique properties of the control
            DataGrid = PollenDataGrid;

            Element.MinWidth = 300;
            Element.MinHeight = 300;

            Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            Element.Background = Brushes.Transparent;
            Element.InnerRadius = Radius;

            //SetToolTip();
        }

        public void SetPollenSeries(string SeriesName)
        {
            List<List<double>> NumberSeries = new List<List<double>>();

            //Populate Data Set
            for (int i = 0; i < DataGrid.Sets[0].Points.Count; i++)
            {
                List<double> TempNumbers = new List<double>();

                for (int j = 0; j < DataGrid.Sets.Count; j++)
                {
                    TempNumbers.Add(DataGrid.Sets[j].Points[i].Number);
                }
                NumberSeries.Add(TempNumbers);
            }

            int C = Element.Series.Count;
            int S = NumberSeries.Count;

            //SERIES########################################
            // Remove all extra series

            for (int i = C; i < S; i++)
            {
                Element.Series.Add(new PieSeries());
                Element.Series[i].Values = new ChartValues<double>(NumberSeries[i].ToArray());
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

            //VALUES########################################
            // Remove all extra series

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = NumberSeries[i].Count;

                for (int j = T; j < V; j++)
                {
                    Element.Series[i].Values.Add(1.0);
                }
            }

            for (int i = 0; i < S; i++)
            {
                int T = Element.Series[i].Values.Count;
                int V = NumberSeries[i].Count;

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
                int V = NumberSeries[i].Count;
                for (int j = 0; j < V; j++)
                {
                    Element.Series[i].Values[j] = NumberSeries[i][j];
                    SetSequenceProperties(DataGrid.Sets[j].Points[i], (PieSeries)Element.Series[i]);
                }
            }

            SetToolTip();
        }

        public PieSeries SetSequenceProperties(DataPt Pt, PieSeries Sequence)
        {
            wGraphic G = Pt.Graphics;
            wFont F = G.FontObject;

            Sequence.PointGeometry = GetGeometry((int)Pt.Marker.Mode);

            Sequence.DataLabels = true;
            Sequence.LabelPoint = point => string.Format("{0:" + Pt.Label.Format + "}", point.Y);

            Sequence.Fill = G.GetBackgroundBrush();

            Sequence.Foreground = F.GetFontBrush();
            Sequence.FontSize = F.Size;
            Sequence.FontStyle = F.ToMediaFont().Italic;
            Sequence.FontWeight = F.ToMediaFont().Bold;

            Sequence.StrokeThickness = G.StrokeWeight[0];
            Sequence.Stroke = G.GetStrokeBrush();
            Sequence.StrokeDashArray = new DoubleCollection(G.StrokePattern);

            Sequence.PointGeometry = GetGeometry((int)Pt.Marker.Mode);
            if (Pt.Label.Alignment == 0) { Sequence.LabelPosition = PieLabelPosition.OutsideSlice; } else { Sequence.LabelPosition = PieLabelPosition.InsideSlice; }

            return Sequence;
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

        public void ForceRefresh()
        {
            Element.Update(false, true);
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
