using Parrot.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Wind.Containers;

namespace Parrot.Displays
{
    public class pProgress : pControl
    {
        public ProgressBar Element;

        public pProgress(string InstanceName)
        {
            Element = new ProgressBar();
            Element.Name = InstanceName;
            Type = "ProgressBar";
        }
        
        public void SetProperties(double value, bool IsHorizontal)
        {
            Element.Value = value * 100;
            if (IsHorizontal)
            {
                Element.Orientation = Orientation.Horizontal;
                Element.Width = double.NaN;
                Element.Height = 20;
            }
            else
            {
                Element.Orientation = Orientation.Vertical;
                Element.Width = 200;
                Element.Height = 20;
            }
        }


        public override void SetFill()
        {
            Element.Background = new SolidColorBrush(Graphics.Background.ToMediaColor());
            Element.Foreground = new SolidColorBrush(Graphics.Foreground.ToMediaColor());
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
            Element.FontFamily = Graphics.FontObject.ToMediaFont().Family;
            Element.FontSize = Graphics.FontObject.Size;
            Element.FontStyle = Graphics.FontObject.ToMediaFont().Italic;
            Element.FontWeight = Graphics.FontObject.ToMediaFont().Bold;
        }
    }
}
