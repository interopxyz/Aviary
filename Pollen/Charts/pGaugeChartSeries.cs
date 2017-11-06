using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xaml;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms.Integration;

using System.Windows.Media;

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
using LiveCharts.WinForms;

using Wind.Containers;

using Pollen.Collections;

namespace Pollen.Charts
{
    public class pGaugeChartSeries : pChart
    {
        public WrapPanel Element;

        public pGaugeChartSeries(string InstanceName)
        {
            Element = new WrapPanel();
            Element.Name = InstanceName;
            Type = "GaugeChartSeries";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        public void SetProperties(bool Horizontal, int Align)
        {
            Element.Children.Clear();

            Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            if (Horizontal) { Element.Orientation = Orientation.Horizontal; } else { Element.Orientation = Orientation.Vertical; }
        }

        public void AddChart(pGaugeChart GaugeChart)
        {
            GaugeChart.Element.Margin = new System.Windows.Thickness(10);
            Element.Children.Add(GaugeChart.Element);
        }

    }
}
