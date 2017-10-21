using System;

using System.Windows;
using System.Windows.Media;

using System.Windows.Controls;
using MahApps.Metro.Controls;

using Wind.Containers;

namespace Parrot.Controls
{ 
    public class pRangeSlider : pControl
    {
        public Grid Element;
        public string Type;

        public RangeSlider Slide;
        public TextBox BlockA;
        public TextBox BlockB;

        //public MahApps.Metro.

        public bool IsHorizontal = true;
        public bool IsLabeled = true;
        public int TickDirection = 0;

        public double ValueLow = 0.25;
        public double ValueHigh = 0.75;
        public double Min = 0;
        public double Max = 1;

        public pRangeSlider(string InstanceName)
        {
            //Set Element info setup
            Element = new Grid();
            Element.Name = InstanceName;
            
            Slide = new RangeSlider();
            BlockA = new TextBox();
            BlockB = new TextBox();

            Type = "RangeSlider";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Slide.ExtendedMode = true;

            BlockA.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            BlockA.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            BlockA.BorderThickness = new Thickness(0);

            BlockB.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            BlockB.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            BlockB.BorderThickness = new Thickness(0);
        }

        public void SetProperties()
        {

            BlockA.TextAlignment = TextAlignment.Center;
            BlockA.VerticalContentAlignment = VerticalAlignment.Center;

            BlockB.TextAlignment = TextAlignment.Center;
            BlockB.VerticalContentAlignment = VerticalAlignment.Center;

            Element.Margin = new Thickness(5);

            Element.ColumnDefinitions.Add(new ColumnDefinition());
            Element.RowDefinitions.Add(new RowDefinition());

            Element.ColumnDefinitions.Add(new ColumnDefinition());
            Element.RowDefinitions.Add(new RowDefinition());

            Element.ColumnDefinitions.Add(new ColumnDefinition());
            Element.RowDefinitions.Add(new RowDefinition());

            Grid.SetColumn(Slide, 0);
            Grid.SetRow(Slide, 0);

            Grid.SetColumn(BlockA, 1);
            Grid.SetRow(BlockA, 0);

            Grid.SetColumn(BlockB, 2);
            Grid.SetRow(BlockB, 0);

            Element.Children.Add(Slide);
            Element.Children.Add(BlockA);
            Element.Children.Add(BlockB);

            SetHorizontal();

        }

        public void SetValue(double RangeStart, double RangeEnd, double SelectLow, double SelectHigh, double Increment, bool HorizontalDirection, bool HasLabel, bool HasTick)
        {
            Min = RangeStart;
            Max = RangeEnd;

            ValueLow = SelectLow;
            ValueHigh = SelectHigh;

            Slide.MinRangeWidth = 5;
            Slide.Minimum = Min;
            Slide.Maximum = Max;

            Slide.LowerValue = ValueLow;
            Slide.UpperValue = ValueHigh;

            //if (Increment > 0) { Slide.Interval = Increment; }
            
            BlockA.Text = Convert.ToString(Math.Truncate(Slide.LowerValue * 1000) / 1000);
            BlockB.Text = Convert.ToString(Math.Truncate(Slide.UpperValue * 1000) / 1000);

            BlockA.TextAlignment = TextAlignment.Center;
            BlockA.VerticalContentAlignment = VerticalAlignment.Center;

            BlockB.TextAlignment = TextAlignment.Center;
            BlockB.VerticalContentAlignment = VerticalAlignment.Center;
            
            Slide.LowerValueChanged -= (o, e) => { BlockA.Text = Convert.ToString(Math.Truncate(Slide.LowerValue * 1000) / 1000); };
            Slide.LowerValueChanged += (o, e) => { BlockA.Text = Convert.ToString(Math.Truncate(Slide.LowerValue * 1000) / 1000); };

            BlockA.TextChanged -= (o, e) => { if (Double.TryParse(BlockA.Text, out ValueLow)) { Slide.LowerValue = CapValue(Convert.ToDouble(BlockA.Text), Min, Slide.UpperValue); } else { BlockA.Text = Convert.ToString(Math.Truncate(Slide.LowerValue * 1000) / 1000); } };
            BlockA.TextChanged += (o, e) => { if (Double.TryParse(BlockA.Text, out ValueLow)) { Slide.LowerValue = CapValue(Convert.ToDouble(BlockA.Text), Min, Slide.UpperValue); } else { BlockA.Text = Convert.ToString(Math.Truncate(Slide.LowerValue * 1000) / 1000); } };

            BlockB.TextChanged -= (o, e) => { if (Double.TryParse(BlockB.Text, out ValueHigh)) { Slide.UpperValue = CapValue(Convert.ToDouble(BlockB.Text), Slide.LowerValue, Max); } else { BlockB.Text = Convert.ToString(Math.Truncate(Slide.UpperValue * 1000) / 1000); } };
            BlockB.TextChanged += (o, e) => { if (Double.TryParse(BlockB.Text, out ValueHigh)) { Slide.UpperValue = CapValue(Convert.ToDouble(BlockB.Text), Slide.LowerValue, Max); } else { BlockB.Text = Convert.ToString(Math.Truncate(Slide.UpperValue * 1000) / 1000); } };

            Slide.UpperValueChanged -= (o, e) => { BlockB.Text = Convert.ToString(Math.Truncate(Slide.UpperValue * 1000) / 1000); };
            Slide.UpperValueChanged += (o, e) => { BlockB.Text = Convert.ToString(Math.Truncate(Slide.UpperValue * 1000) / 1000); };

            SetDirection(HorizontalDirection, HasLabel);
            if (HasTick) { SetTickmark(2); } else { SetTickmark(0); }
        }

        public void RemoveLabel()
        {
            if (Element.Children.Count > 1)
            {
                Element.Children.Remove(BlockA);
                Element.Children.Remove(BlockB);
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

        public void SetVertical()
        {
            RemoveLabel();

            Slide.Orientation = Orientation.Vertical;
            Slide.HorizontalAlignment = HorizontalAlignment.Center;
            Slide.VerticalAlignment = VerticalAlignment.Stretch;

            BlockA.Margin = new Thickness(0, 5, 0, 0);
            BlockA.BorderThickness = new Thickness(0, 1, 0, 0);

            BlockB.Margin = new Thickness(0, 5, 0, 0);
            BlockB.BorderThickness = new Thickness(0, 1, 0, 0);

            Element.Width = 25;
            Element.Height = 200;

            Element.ColumnDefinitions[0].Width = new GridLength(100, GridUnitType.Star);
            Element.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);
            Element.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Star);

            Element.RowDefinitions[0].Height = new GridLength(100, GridUnitType.Star);
            Element.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            Element.RowDefinitions[2].Height = new GridLength(0, GridUnitType.Star);

            if (IsLabeled)
            {
                Element.RowDefinitions[0].Height = new GridLength(80, GridUnitType.Star);
                Element.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
                Element.RowDefinitions[2].Height = new GridLength(10, GridUnitType.Star);

                Grid.SetColumn(BlockA, 0);
                Grid.SetRow(BlockA, 1);
                Element.Children.Add(BlockA);

                Grid.SetColumn(BlockB, 0);
                Grid.SetRow(BlockB, 2);
                Element.Children.Add(BlockB);
            }
        }

        public void SetHorizontal()
        {
            RemoveLabel();

            Slide.Orientation = Orientation.Horizontal;
            Slide.HorizontalAlignment = HorizontalAlignment.Stretch;
            Slide.VerticalAlignment = VerticalAlignment.Center;


            BlockA.Margin = new Thickness(0, 5, 0, 0);
            BlockA.BorderThickness = new Thickness(1, 0, 0, 0);

            BlockB.Margin = new Thickness(0, 5, 0, 0);
            BlockB.BorderThickness = new Thickness(1, 0, 0, 0);

            Element.Width = 200;
            Element.Height = 30;

            Element.ColumnDefinitions[0].Width = new GridLength(100, GridUnitType.Star);
            Element.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);
            Element.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Star);

            Element.RowDefinitions[0].Height = new GridLength(100, GridUnitType.Star);
            Element.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            Element.RowDefinitions[2].Height = new GridLength(0, GridUnitType.Star);

            if (IsLabeled)
            {
                Element.ColumnDefinitions[0].Width = new GridLength(60, GridUnitType.Star);
                Element.ColumnDefinitions[1].Width = new GridLength(20, GridUnitType.Star);
                Element.ColumnDefinitions[2].Width = new GridLength(20, GridUnitType.Star);

                Grid.SetColumn(BlockA, 1);
                Grid.SetRow(BlockA, 0);
                Element.Children.Add(BlockA);

                Grid.SetColumn(BlockB, 2);
                Grid.SetRow(BlockB, 0);
                Element.Children.Add(BlockB);
            }
        }

        public void SetTickmark(int TickType)
        {
            TickDirection = TickType;
            switch (TickType)
            {
                case 1:
                    Slide.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.TopLeft;
                    break;
                case 2:
                    Slide.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight;
                    break;
                case 3:
                    Slide.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.Both;
                    break;
                default:
                    Slide.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.None;
                    break;
            }
        }

        private double CapValue(double val, double min, double max)
        {
            if (val < min) { val = min; }
            if (val > max) { val = max; }
            return val;
        }

        public override void SetSolidFill()
        {
            Element.Background = new SolidColorBrush(Graphics.Background.ToMediaColor());
            Slide.Foreground = new SolidColorBrush(Graphics.Foreground.ToMediaColor());
        }

        public override void SetStroke()
        {
            BlockA.BorderThickness = new Thickness(Graphics.StrokeWeight[0], Graphics.StrokeWeight[1], Graphics.StrokeWeight[2], Graphics.StrokeWeight[3]);
            BlockA.BorderBrush = new SolidColorBrush(Graphics.StrokeColor.ToMediaColor());
            BlockB.BorderThickness = new Thickness(Graphics.StrokeWeight[0], Graphics.StrokeWeight[1], Graphics.StrokeWeight[2], Graphics.StrokeWeight[3]);
            BlockB.BorderBrush = new SolidColorBrush(Graphics.StrokeColor.ToMediaColor());
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
            BlockA.Foreground = new SolidColorBrush(Graphics.FontObject.FontColor.ToMediaColor());
            BlockA.FontFamily = Graphics.FontObject.ToMediaFont().Family;
            BlockA.FontSize = Graphics.FontObject.Size;
            BlockA.FontStyle = Graphics.FontObject.ToMediaFont().Italic;
            BlockA.FontWeight = Graphics.FontObject.ToMediaFont().Bold;

            BlockB.Foreground = new SolidColorBrush(Graphics.FontObject.FontColor.ToMediaColor());
            BlockB.FontFamily = Graphics.FontObject.ToMediaFont().Family;
            BlockB.FontSize = Graphics.FontObject.Size;
            BlockB.FontStyle = Graphics.FontObject.ToMediaFont().Italic;
            BlockB.FontWeight = Graphics.FontObject.ToMediaFont().Bold;
        }

    }
}
