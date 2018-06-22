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

using Wind.Containers;

using Pollen.Collections;

namespace Pollen.Charts
{
    public class pGaugeChartSeries : pChart
    {
        public WrapPanel Element;
        public DataSetCollection DataSet = new DataSetCollection();
        public string Name = "";

        public pGaugeChartSeries(string InstanceName)
        {
            Name = InstanceName;

            Element = new WrapPanel();
            Element.Name = InstanceName;
            Type = "Chart";

            //Set "Clear" appearance to all elements
            Element.Background = Brushes.Transparent;
        }

        public void SetProperties(DataSetCollection ChartDataSet, bool Horizontal, int Align)
        {
            DataSet = ChartDataSet;
            Element.Children.Clear();

            Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            if (Horizontal) { Element.Orientation = Orientation.Horizontal; } else { Element.Orientation = Orientation.Vertical; }
        }


        public void SetCharts(int Radius, int ChartMode, int Status)
        {
            for (int i = 0; i < DataSet.Sets.Count; i++)
            {
                for (int j = 0; j < DataSet.Sets[i].Points.Count; j++)
                {
                    pGaugeChart ChartObj = new pGaugeChart(Name + "_" + i.ToString() + "_" + j.ToString());

                    ChartObj.SetProperties(DataSet, Radius);
                    ChartObj.SetGaugeType(ChartMode);
                    ChartObj.SetGaugeValues(DataSet.Sets[i].Points[j], Status, DataSet.Sets[i].NumericBounds.T0, DataSet.Sets[i].NumericBounds.T1, DataSet.Sets[i].NumericSum);

                    Element.Children.Add(ChartObj.Element);
                }
            }
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
        }

        public override void SetFont()
        {

        }

        public override void SetMargin()
        {
            Element.Margin = Graphics.GetMargin();
        }
        

    }
}
