using Parrot.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Wind.Containers;
using Wind.Types;

namespace Parrot.Displays
{
    public class pLegend : pControl
    {
        public WrapPanel Element;
        public enum IconMode { Box, Dot, Bar, Fill, Underline}

        public IconMode IconType = IconMode.Box;

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
                Element.Height = double.NaN;
                Element.VerticalAlignment = VerticalAlignment.Stretch;
                if (Distance > 0){Element.Width = Distance; } else { Element.Width = double.NaN; }
            }
            else
            {
                Element.Orientation = Orientation.Vertical;
                Element.Width = double.NaN;
                Element.HorizontalAlignment = HorizontalAlignment.Stretch;
                if (Distance > 0) { Element.Height = Distance; } else { Element.Height = double.NaN; }
            }

        }

        public void SetItems(List<String> items, List<System.Drawing.Color> colors, IconMode iconMode, bool IsLight)
        {
            Element.Children.Clear();
            for (int i = 0; i < items.Count; i++)
            {
                Element.Children.Add(LegendItem(items[i], colors[i], (IconMode)iconMode, IsLight));
            }
        }
        
        public Panel LegendItem(string I, System.Drawing.Color C, IconMode iconMode, bool IsLight)
        {
            StackPanel panel = new StackPanel();
            Canvas canvas = new Canvas();
            Label text = new Label();
            Canvas spacer = new Canvas();

            spacer.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            spacer.Width = 20;
            spacer.Height = 10;

            canvas.Width = 12;
            canvas.Height = 12;
            Path X = new Path();

            switch (iconMode)
            {
                case IconMode.Box:
                    X.Data = new RectangleGeometry(new Rect(1, 1, 10, 10));
                break;
                case IconMode.Dot:
                    X.Data = new EllipseGeometry(new Rect(1, 1, 10, 10));
                    break;
                case IconMode.Bar:
                    X.Data = new RectangleGeometry(new Rect(0, 0, 4, 16));
                    canvas.Width = 4;
                    canvas.Height = 16;
                    spacer.Width = 4;
                    break;
                case IconMode.Underline:
                    canvas.Width = 0;
                    spacer.Width = 4;
                    text.BorderBrush = new SolidColorBrush(new wColor(C).ToMediaColor());
                    text.BorderThickness = new Thickness(0, 0, 0, 4);
                    break;
                case IconMode.Fill:
                    canvas.Width = 0;
                    spacer.Width = 0;
                    text.Background = new SolidColorBrush(new wColor(C).ToMediaColor());
                    text.Margin = new Thickness(1);
                    text.FontWeight = FontWeights.SemiBold;
                    break;
            }

            if (IsLight) { text.Foreground = new SolidColorBrush(Color.FromArgb(255,250,250,250)); }

            X.Fill = new SolidColorBrush(new wColor(C).ToMediaColor());
            canvas.Children.Add(X);
            
            text.Content = I;
            
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
