using Parrot.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

using Wind.Containers;

namespace Parrot.Displays
{
    public class pGradient : pControl

    {
        public Grid Element;
        public Label MinLabel;
        public Label MaxLabel;
        public string Type;

        public pGradient(string InstanceName)
        {
            Element = new Grid();
            Element.Name = InstanceName;
            
            Type = "Gradient";

            MinLabel = new Label();
            MaxLabel = new Label();

            Element.ColumnDefinitions.Add(new ColumnDefinition());
            Element.ColumnDefinitions.Add(new ColumnDefinition());

            Element.RowDefinitions.Add(new RowDefinition());
            Element.RowDefinitions.Add(new RowDefinition());

            Element.Children.Add(MaxLabel);
            Element.Children.Add(MinLabel);

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

        }

        public void SetProperties(string Min, string Max, wGraphic Graphic, bool Horizontal)
        {
            Element.HorizontalAlignment = HorizontalAlignment.Stretch;
            Element.VerticalAlignment = VerticalAlignment.Stretch;

            Element.Height = double.NaN;
            Element.Width = double.NaN;

            MinLabel.Content = Min;
            MaxLabel.Content = Max;

            if (Horizontal)
            {
                Element.Background = new LinearGradientBrush(Graphic.Gradient.ToMediaGradient(), 0.0);

                Element.ColumnDefinitions[0].Width = new GridLength(50, GridUnitType.Star);
                Element.ColumnDefinitions[1].Width = new GridLength(50, GridUnitType.Star);

                Element.RowDefinitions[0].Height = new GridLength(100, GridUnitType.Star);
                Element.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);

                Grid.SetColumn(MinLabel, 0);
                Grid.SetColumn(MaxLabel, 1);

                Grid.SetRow(MinLabel, 0);
                Grid.SetRow(MaxLabel, 0);

                MinLabel.HorizontalAlignment = HorizontalAlignment.Left;
                MaxLabel.HorizontalAlignment = HorizontalAlignment.Right;

                MinLabel.VerticalAlignment = VerticalAlignment.Stretch;
                MaxLabel.VerticalAlignment = VerticalAlignment.Stretch;
            }
            else
            {
                Brush BackgroundBrush = new LinearGradientBrush(Graphic.Gradient.ToMediaGradient(), 90.0);
                Element.Background = BackgroundBrush;

                Element.ColumnDefinitions[0].Width = new GridLength(100, GridUnitType.Star);
                Element.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);

                Element.RowDefinitions[0].Height = new GridLength(50, GridUnitType.Star);
                Element.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Star);

                Grid.SetColumn(MinLabel, 0);
                Grid.SetColumn(MaxLabel, 0);

                Grid.SetRow(MinLabel, 1);
                Grid.SetRow(MaxLabel, 0);

                MinLabel.HorizontalAlignment = HorizontalAlignment.Stretch;
                MaxLabel.HorizontalAlignment = HorizontalAlignment.Stretch;

                MinLabel.VerticalAlignment = VerticalAlignment.Bottom;
                MaxLabel.VerticalAlignment = VerticalAlignment.Top;

            }

        }
        
        public override void SetSolidFill()
        {
            Element.Background = new SolidColorBrush(Graphics.Background.ToMediaColor());
        }

        public override void SetStroke()
        {
            //Element.BorderThickness = new Thickness(Graphics.StrokeWeight[0], Graphics.StrokeWeight[1], Graphics.StrokeWeight[2], Graphics.StrokeWeight[3]);
            //Element.BorderBrush = new SolidColorBrush(Graphics.StrokeColor.ToMediaColor());
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
            MinLabel.Foreground = new SolidColorBrush(Graphics.FontObject.FontColor.ToMediaColor());
            MinLabel.FontFamily = Graphics.FontObject.ToMediaFont().Family;
            MinLabel.FontSize = Graphics.FontObject.Size;
            MinLabel.FontStyle = Graphics.FontObject.ToMediaFont().Italic;
            MinLabel.FontWeight = Graphics.FontObject.ToMediaFont().Bold;

            MaxLabel.Foreground = new SolidColorBrush(Graphics.FontObject.FontColor.ToMediaColor());
            MaxLabel.FontFamily = Graphics.FontObject.ToMediaFont().Family;
            MaxLabel.FontSize = Graphics.FontObject.Size;
            MaxLabel.FontStyle = Graphics.FontObject.ToMediaFont().Italic;
            MaxLabel.FontWeight = Graphics.FontObject.ToMediaFont().Bold;
        }

    }
}
