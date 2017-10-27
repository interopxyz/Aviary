using System;

using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

using Xceed.Wpf.Toolkit;

using Wind.Containers;
using System.Windows.Controls.Primitives;
using MaterialDesignThemes.Wpf;

namespace Parrot.Controls
{
    public class pSlider : pControl
    {
        public Grid Element;
        public string Type;

        public bool IsHorizontal = true;
        public bool IsLabeled = true;
        public int TickDirection = 0;
        public double Value = 0.5;
        public double Min = 0.0;
        public double Max = 1.0;
        public Slider Slide;
        public TextBox Block;

        public pSlider(string InstanceName)
        {
            //Set Element info setup
            Element = new Grid();
            Element.Name = InstanceName;

            Slide = new Slider();
            Block = new TextBox();

            Type = "Slider";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

            Slide.AutoToolTipPlacement = System.Windows.Controls.Primitives.AutoToolTipPlacement.TopLeft;
            Slide.AutoToolTipPrecision = 3;

            Block.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Block.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            Block.BorderThickness = new Thickness(1, 0, 0, 0);
        }

        public void SetProperties()
        {
            Block.TextAlignment = TextAlignment.Center;
            Block.VerticalContentAlignment = VerticalAlignment.Center;

            Element.Margin = new Thickness(5);

            Element.ColumnDefinitions.Add(new ColumnDefinition());
            Element.RowDefinitions.Add(new RowDefinition());

            Element.ColumnDefinitions.Add(new ColumnDefinition());
            Element.RowDefinitions.Add(new RowDefinition());
            

            Grid.SetColumn(Slide, 0);
            Grid.SetRow(Slide, 0);

            Grid.SetColumn(Block, 1);
            Grid.SetRow(Block, 0);

            Element.Children.Add(Slide);
            Element.Children.Add(Block);

            SetHorizontal();
            SetTickmark(TickDirection);

        }

        public void SetValues(double MinValue, double MaxValue, double Increment, double InitialValue, bool HorizontalDirection, bool HasLabel, bool HasTick)
        {
            Min = MinValue;
            Max = MaxValue;
            Value = InitialValue;

            Slide.Minimum = Min;
            Slide.Maximum = Max;
            Slide.Value = Value;

            Slide.TickFrequency = Increment;

            if (Increment == 0) { Slide.IsSnapToTickEnabled = false; } else { Slide.IsSnapToTickEnabled = true; }

            Block.Text = Convert.ToString(Math.Truncate(Slide.Value * 1000) / 1000);

            Block.TextChanged -= (o, e) => { if (Double.TryParse(Block.Text, out Value)) { Slide.Value = CapValue(Convert.ToDouble(Block.Text), Min, Max); } else { Block.Text = Convert.ToString(Min); } };
            Block.TextChanged += (o, e) => { if (Double.TryParse(Block.Text, out Value)) { Slide.Value = CapValue(Convert.ToDouble(Block.Text), Min, Max); } else { Block.Text = Convert.ToString(Min); } };

            Slide.ValueChanged -= (o, e) => { Block.Text = Convert.ToString(Math.Truncate(Slide.Value * 1000) / 1000); };
            Slide.ValueChanged += (o, e) => { Block.Text = Convert.ToString(Math.Truncate(Slide.Value * 1000) / 1000); };

            SetDirection(HorizontalDirection, HasLabel);
            if (HasTick) { SetTickmark(2); } else { SetTickmark(0); }
        }

        public void SetValues(double MinValue, double MaxValue, double Increment, double InitialValue, bool HorizontalDirection, bool HasLabel, int TickType)
        {
            Min = MinValue;
            Max = MaxValue;
            Value = InitialValue;

            Slide.Minimum = Min;
            Slide.Maximum = Max;
            Slide.Value = Value;

            Slide.TickFrequency = Increment;

            if (Increment == 0) { Slide.IsSnapToTickEnabled = false; } else { Slide.IsSnapToTickEnabled = true; }

            Block.Text = Convert.ToString(Math.Truncate(Slide.Value * 1000) / 1000);

            Block.TextChanged -= (o, e) => { if (Double.TryParse(Block.Text, out Value)) { Slide.Value = CapValue(Convert.ToDouble(Block.Text), Min, Max); } else { Block.Text = Convert.ToString(Min); } };
            Block.TextChanged += (o, e) => { if (Double.TryParse(Block.Text, out Value)) { Slide.Value = CapValue(Convert.ToDouble(Block.Text), Min, Max); } else { Block.Text = Convert.ToString(Min); } };

            Slide.ValueChanged -= (o, e) => { Block.Text = Convert.ToString(Math.Truncate(Slide.Value * 1000) / 1000); };
            Slide.ValueChanged += (o, e) => { Block.Text = Convert.ToString(Math.Truncate(Slide.Value * 1000) / 1000); };

            SetDirection(HorizontalDirection, HasLabel);
            SetTickmark(TickType);
        }

        public void SetTickmark(int TickType)
        {
            TickDirection = TickType;
            switch (TickType)
            {
                case 1:
                    Slide.TickPlacement = TickPlacement.TopLeft;
                    break;
                case 2:
                    Slide.TickPlacement = TickPlacement.BottomRight;
                    break;
                case 3:
                    Slide.TickPlacement = TickPlacement.Both;
                    break;
                default:
                    Slide.TickPlacement = TickPlacement.None;
                    break;
            }
        }

        public void SetDirection(bool HorizontalDirection, bool HasLabel)
        {
            if ((IsHorizontal != HorizontalDirection) || (IsLabeled != HasLabel))
            {
                IsLabeled = HasLabel;
                IsHorizontal = HorizontalDirection;

                if (IsHorizontal) { SetHorizontal(); } else { SetVertical(); }
            }
        }

        public void RemoveLabel()
        {
            if (Element.Children.Count > 1) { Element.Children.Remove(Block); }
        }

        public void SetVertical()
        {
            RemoveLabel();

            Slide.Orientation = Orientation.Vertical;
            Slide.HorizontalAlignment = HorizontalAlignment.Center;
            Slide.VerticalAlignment = VerticalAlignment.Stretch;

            Block.Margin = new Thickness(0, 5, 0, 0);
            Block.BorderThickness = new Thickness(0, 1, 0, 0);

            Element.MinHeight = 200;
            Element.Width = 30;
            Element.VerticalAlignment = VerticalAlignment.Stretch;
            Element.HorizontalAlignment = HorizontalAlignment.Left;

            Element.ColumnDefinitions[0].Width = new GridLength(30, GridUnitType.Star);
            Element.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);

            Element.RowDefinitions[0].Height = new GridLength(100, GridUnitType.Star);
            Element.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);

            if (IsLabeled)
            {
                Element.RowDefinitions[0].Height = new GridLength(75, GridUnitType.Star);
                Element.RowDefinitions[1].Height = new GridLength(25, GridUnitType.Star);

                Grid.SetColumn(Block, 0);
                Grid.SetRow(Block, 1);
                Element.Children.Add(Block);
            }
        }

        public void SetHorizontal()
        {
            RemoveLabel();

            Slide.Orientation = Orientation.Horizontal;
            Slide.HorizontalAlignment = HorizontalAlignment.Stretch;
            Slide.VerticalAlignment = VerticalAlignment.Center;

            Block.Margin = new Thickness(5, 0, 0, 0);
            Block.BorderThickness = new Thickness(1, 0, 0, 0);

            Element.Height = 25;
            Element.MinWidth = 200;
            Element.HorizontalAlignment = HorizontalAlignment.Stretch;
            Element.VerticalAlignment = VerticalAlignment.Top;

            Element.ColumnDefinitions[0].Width = new GridLength(100, GridUnitType.Star);
            Element.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);

            if (IsLabeled)
            {
                Element.ColumnDefinitions[0].Width = new GridLength(75, GridUnitType.Star);
                Element.ColumnDefinitions[1].Width = new GridLength(25, GridUnitType.Star);

                Grid.SetColumn(Block, 1);
                Grid.SetRow(Block, 0);
                Element.Children.Add(Block);
            }

            Element.RowDefinitions[0].Height = new GridLength(100, GridUnitType.Star);
            Element.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);

        }

        private double CapValue(double val, double min, double max)
        {
            if (val < min) { val = min; }
            if (val > max) { val = max; }
            return val;
        }
                public override void SetFill()
        {
            Element.Background = Graphics.WpfFill;
        }

        public override void SetStroke()
        {
            Block.BorderThickness = new Thickness(Graphics.StrokeWeight[0], Graphics.StrokeWeight[1], Graphics.StrokeWeight[2], Graphics.StrokeWeight[3]);
            Block.BorderBrush = new SolidColorBrush(Graphics.StrokeColor.ToMediaColor());
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
        }

        public override void SetFont()
        {
            Block.Foreground = new SolidColorBrush(Graphics.FontObject.FontColor.ToMediaColor());
            Block.FontFamily = Graphics.FontObject.ToMediaFont().Family;
            Block.FontSize = Graphics.FontObject.Size;
            Block.FontStyle = Graphics.FontObject.ToMediaFont().Italic;
            Block.FontWeight = Graphics.FontObject.ToMediaFont().Bold;
        }
    }
}
