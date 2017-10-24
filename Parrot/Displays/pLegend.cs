using Parrot.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Wind.Containers;

namespace Parrot.Displays
{
    public class pLegend : pControl
    {
        public WrapPanel Element;
        public string Type;

        public pLegend(string InstanceName)
        {
            Element = new WrapPanel();
            Element.Name = InstanceName;
            Type = "Legend";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Element.Margin = new System.Windows.Thickness(5);
        }

        public void SetDirection(int Distance, bool Horizontal)
        {
            if (Horizontal)
            {
                Element.Orientation = Orientation.Horizontal;
                if (Distance > 0) { Element.Width = Distance; }
            }
            else
            {
                Element.Orientation = Orientation.Vertical;
                if (Distance > 0) { Element.Height = Distance; }
            }

        }

        public void SetItems(List<String> items, List<System.Drawing.Color> colors)
        {
            Element.Children.Clear();
            for (int i = 0; i < items.Count; i++)
            {
                Element.Children.Add(LegendItem(items[i], colors[i]));
            }
        }
        
        public Panel LegendItem(string I, System.Drawing.Color C)
        {
            Label canvas = new Label();
            canvas.Background = new SolidColorBrush(Color.FromArgb(C.A, C.R, C.G, C.B));
            canvas.Width = 10;
            canvas.Height = 10;

            Label text = new Label();
            text.Content = I;

            Canvas spacer = new Canvas();
            spacer.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            spacer.Width = 20;
            spacer.Height = 10;

            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;
            panel.Children.Add(canvas);
            panel.Children.Add(text);
            panel.Children.Add(spacer);

            return panel;
        }

        public override void SetFill()
        {
            Element.Background = Graphics.WpfFill;
        }

        public override void SetStroke()
        {
            foreach(StackPanel P in Element.Children)
                {
                Label C = (Label)P.Children[0];
            C.BorderThickness = new Thickness(Graphics.StrokeWeight[0], Graphics.StrokeWeight[1], Graphics.StrokeWeight[2], Graphics.StrokeWeight[3]);
            C.BorderBrush = new SolidColorBrush(Graphics.StrokeColor.ToMediaColor());
            }
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
            //Element.Padding = new Thickness(Graphics.Padding[0], Graphics.Padding[1], Graphics.Padding[2], Graphics.Padding[3]);
        }

        public override void SetFont()
        {
            //Element.Foreground = new SolidColorBrush(Graphics.FontObject.FontColor.ToMediaColor());
            //Element.FontFamily = Graphics.FontObject.ToMediaFont().Family;
            //Element.FontSize = Graphics.FontObject.Size;
            //Element.FontStyle = Graphics.FontObject.ToMediaFont().Italic;
            //Element.FontWeight = Graphics.FontObject.ToMediaFont().Bold;
            //Element.TextDecorations = Graphics.FontObject.ToMediaFont().Style;
        }
    }
}
