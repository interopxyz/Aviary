using System;
using System.Windows;
using System.Windows.Media;

using Wind.Containers;

using MahApps.Metro.Controls;

namespace Parrot.Controls
{
    public class pScrollNumber : pControl
    {
        public NumericUpDown Element;
        public string Type;

        public string Value;
        public bool IsCapped = true;

        public pScrollNumber(string InstanceName)
        {
            //Set Element info setup
            Element = new NumericUpDown();
            Element.Name = InstanceName;
            Type = "ScrollNumber";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Element.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Element.BorderThickness = new Thickness(0);
        }

        public void SetProperties(double Value, double Min, double Max, double Increment)
        {
            Element.HorizontalContentAlignment = HorizontalAlignment.Center;
            Element.MinWidth = 100;
            Element.Value = Min;
            Element.Minimum = Min;
            Element.Maximum = Max;
            Element.Interval = Increment;
            Element.HasDecimals = true;
            Element.Value = Value;

        }

        private double CapValue(double val, double step, double min, double max, bool dir, bool wrap, bool cap)
        {
            if (dir) { val += step; } else { val -= step; }

            if (cap)
            {
                if (val < min) { if (wrap) { val = max; } else { val = min; } }
                if (val > max) { if (wrap) { val = min; } else { val = max; } }
            }

            return val;
        }

        public override void SetSolidFill()
        {
            Element.Background = new SolidColorBrush(Graphics.Background.ToMediaColor());
        }

        public override void SetStroke()
        {
            Element.BorderThickness = new Thickness(Graphics.StrokeWeight[0], Graphics.StrokeWeight[1], Graphics.StrokeWeight[2], Graphics.StrokeWeight[3]);
            Element.BorderBrush = new SolidColorBrush(Graphics.StrokeColor.ToMediaColor());
        }

        public override void SetSize()
        {
            if (Graphics.Width < 1) { Element.Width = double.NaN; } else { Element.Width = Graphics.Width; }
            if (Graphics.Height < 1) { Element.Height = double.NaN; } else { Element.Height = Graphics.Height; }
        }

        public override void SetMargin()
        {
            Element.Margin = new Thickness(Graphics.Margin[0], Graphics.Margin[1], Graphics.Margin[2], Graphics.Margin[3]);
        }

        public override void SetPadding()
        {
            Element.Padding = new Thickness(Graphics.Padding[0], Graphics.Padding[1], Graphics.Padding[2], Graphics.Padding[3]);
        }

        public override void SetFont()
        {
            Element.Foreground = new SolidColorBrush(Graphics.FontObject.FontColor.ToMediaColor());
            Element.FontFamily = Graphics.FontObject.ToMediaFont().Family;
            Element.FontSize = Graphics.FontObject.Size;
            Element.FontStyle = Graphics.FontObject.ToMediaFont().Italic;
            Element.FontWeight = Graphics.FontObject.ToMediaFont().Bold;
        }

    }
}
