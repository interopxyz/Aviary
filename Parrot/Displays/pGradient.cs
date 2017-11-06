using Parrot.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

using Wind.Containers;
using Wind.Types;

namespace Parrot.Displays
{
    public class pGradient : pControl

    {
        public Grid Element = new Grid();

        Grid AnnotationBar = new Grid();
        Canvas canvas = new Canvas();
        Label TopLabel = new Label();
        List<Label> Labels = new List<Label>();
        List<double> Parameters = new List<double>();

        public bool IsHorizontal = false;
        public bool HasTick = true;
        public bool IsExtent = false;
        public bool IsSmooth = true;
        public int BarWidth = 20;

        public double TextRotation = 0;
        public Thickness TickWeight = new Thickness();

        SolidColorBrush FontColor = new SolidColorBrush();
        SolidColorBrush GradientColor = new SolidColorBrush();

        HorizontalAlignment GradientH = HorizontalAlignment.Right;
        HorizontalAlignment TextH = HorizontalAlignment.Left;

        VerticalAlignment GradientV = VerticalAlignment.Top;
        VerticalAlignment TextV = VerticalAlignment.Bottom;

        public List<string> Titles = new List<string>();
        GradientStopCollection Gradient = new GradientStopCollection();

        public pGradient(string InstanceName)
        {
            Element = new Grid();
            Element.Name = InstanceName;
            
            Type = "Gradient";
            

            Element.ColumnDefinitions.Add(new ColumnDefinition());
            Element.ColumnDefinitions.Add(new ColumnDefinition());

            Element.RowDefinitions.Add(new RowDefinition());
            Element.RowDefinitions.Add(new RowDefinition());
            

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

        }

        public void SetProperties(List<wColor> ColorSet, List<double> GradientParameters, List<string> LegendTitles, int GradientWidth, bool IsHorizontalOrientation, int Position, bool IsExtentLabeled, bool HasTickMarks, bool IsLight, bool IsFlipped)
        {
            canvas = new Canvas();
            AnnotationBar = new Grid();
            TopLabel = new Label();
            Labels = new List<Label>();
            
            Element.Children.Clear();
            AnnotationBar.Children.Clear();
            Labels.Clear();

            Parameters = GradientParameters;
            Titles = LegendTitles;

            BarWidth = GradientWidth;
            IsHorizontal = IsHorizontalOrientation;
            IsExtent = IsExtentLabeled;
            HasTick = HasTickMarks;

            Gradient = new wGradient(ColorSet, Parameters,!IsHorizontal).ToMediaGradient();

            SetFontColor(IsLight);
            SetTickOrientation(IsFlipped);

            if (IsFlipped) { TextRotation = 90.0; } else { TextRotation = 0.0; }

            if (IsHorizontal) { TopLabel.Content = Titles[0]; } else { TopLabel.Content = Titles[Titles.Count-1]; }

            if (IsHorizontal)
            {
                Element.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Auto);
                Element.ColumnDefinitions[1].Width = new GridLength(100, GridUnitType.Star);

                Element.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Auto);
                Element.RowDefinitions[1].Height = new GridLength(100, GridUnitType.Star);

                switch (Position)
                {
                    case 0:
                        SetDirections(true, false);
                        SetLabel(new Tuple<int, int>(0, 0));
                        SetLabels(new Tuple<int, int>(1, 0),false);
                        SetGradient(Gradient, new Tuple<int, int>(1, 1),true);
                        break;
                    case 1:
                        SetDirections(true, true);
                        SetLabel(new Tuple<int, int>(0, 1));
                        SetLabels(new Tuple<int, int>(1, 1), false);
                        SetGradient(Gradient, new Tuple<int, int>(1, 0), true);
                        break;
                    case 2:
                        SetLabel(new Tuple<int, int>(0, 0));
                        SetLabels(new Tuple<int, int>(1, 0),true);
                        break;
                }

            }
            else
            {
                Element.ColumnDefinitions[0].Width = new GridLength(100, GridUnitType.Star);
                Element.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Auto);

                Element.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Auto);
                Element.RowDefinitions[1].Height = new GridLength(100, GridUnitType.Star);

                switch (Position)
                {
                    case 0:
                        SetDirections(true, false);
                        SetLabel(new Tuple<int, int>(1, 0));
                        SetLabels(new Tuple<int, int>(1, 1), false);
                        SetGradient(Gradient, new Tuple<int, int>(0, 1), true);
                        break;
                    case 1:
                        SetDirections(false, false);
                        SetLabel(new Tuple<int, int>(0, 0));
                        SetLabels(new Tuple<int, int>(0, 1), false);
                        SetGradient(Gradient, new Tuple<int, int>(1, 1), true);
                        break;
                    case 2:
                        SetLabel(new Tuple<int, int>(1, 0));
                        SetLabels(new Tuple<int, int>(1, 1), true);
                        break;
                }
            }

            Labels.Insert(0, TopLabel);

            Element.Children.Add(canvas);
            Element.Children.Add(TopLabel);
            Element.Children.Add(AnnotationBar);
            
        }
        
        public void SetGradient(GradientStopCollection Gradient, Tuple<int, int> Location, bool FillBackground)
        {
            if (IsHorizontal)
            {
                canvas.HorizontalAlignment = HorizontalAlignment.Stretch;
                canvas.VerticalAlignment = GradientV;

                canvas.Width = double.NaN;
                canvas.MinWidth = 200;
                canvas.MinHeight = BarWidth;
            }
            else
            {
                canvas.HorizontalAlignment = GradientH;
                canvas.VerticalAlignment = VerticalAlignment.Stretch;

                canvas.MinWidth = BarWidth;
                canvas.MinHeight = 200;
                canvas.Height = double.NaN;
            }
            

            Grid.SetColumn(canvas, Location.Item1);
            Grid.SetRow(canvas, Location.Item2);

            if (FillBackground) { canvas.Background = SetFillGradient(); }
        }

        public void SetLabel( Tuple<int, int> Location)
        {
            TopLabel.BorderBrush = new SolidColorBrush(Colors.DarkGray);
            TopLabel.BorderThickness = TickWeight;
            TopLabel.FontWeight = FontWeights.SemiBold;

            if (IsHorizontal)
            {
                TopLabel.Padding = new Thickness(5, 0, 2, 3);
                TopLabel.Margin = new Thickness(0);
                
                TopLabel.HorizontalAlignment = HorizontalAlignment.Right;
                TopLabel.VerticalAlignment = TextV;
            }
            else
            {
                TopLabel.Padding = new Thickness(3, 0, 0, 0);
                TopLabel.Margin = new Thickness(0);
                
                TopLabel.HorizontalAlignment = TextH;
                TopLabel.VerticalAlignment = VerticalAlignment.Top;
            }
            TopLabel.LayoutTransform = new RotateTransform(TextRotation);

            Grid.SetColumn(TopLabel, Location.Item1);
            Grid.SetRow(TopLabel, Location.Item2);
        }

        public void SetLabels(Tuple<int,int> Location, bool FillBackground)
        {
            int k = Titles.Count - 1;
            if (IsExtent) { k = 1; }

            if (IsHorizontal)
            {
                BuildGrid(k, 1);

                SetTitles(Titles);
                AnnotationBar.HorizontalAlignment = HorizontalAlignment.Stretch;
                AnnotationBar.VerticalAlignment = TextV;

                if (FillBackground)
                {
                    AnnotationBar.Width = double.NaN;
                    AnnotationBar.MinWidth = 200;
                    AnnotationBar.MinHeight = BarWidth;
                }
            }
            else
            {
                BuildGrid(1, k);

                SetTitles(Titles);
                AnnotationBar.HorizontalAlignment = TextH;
                AnnotationBar.VerticalAlignment = VerticalAlignment.Stretch;

                if (FillBackground)
                {
                    AnnotationBar.MinWidth = BarWidth;
                    AnnotationBar.MinHeight = 200;
                    AnnotationBar.Height = double.NaN;
                }
            }
            Grid.SetColumn(AnnotationBar, Location.Item1);
            Grid.SetRow(AnnotationBar, Location.Item2);

            if (FillBackground) { AnnotationBar.Background = SetFillGradient(); }
        }
        
        public void SetTitles( List<string> Titles )
        {
            int k = 1;
            int S = 3;

            VerticalAlignment VAlign;
            HorizontalAlignment HAlign;
            int W = 0;
            int H = 0;

            if (IsHorizontal)
            {
                HAlign = HorizontalAlignment.Right;
                VAlign = TextV;
                H = S;
            }
            else
            {
                Titles.Reverse();
                Parameters.Reverse();

                HAlign = TextH;
                VAlign = VerticalAlignment.Bottom;
                W = S;
            }

            int j = k-1;
            if (IsExtent) {
                k = Titles.Count - 1;
                j = 0;
            }

            for (int i = k; i < Titles.Count; i++)
            {
                Label SingleLabel = new Label();
                SingleLabel.Padding = new Thickness(W, 0, 2, H);
                SingleLabel.Margin = new Thickness(0);
                SingleLabel.FontWeight = FontWeights.SemiBold;

                SingleLabel.BorderBrush = GradientColor;
                SingleLabel.BorderThickness = TickWeight;
                SingleLabel.LayoutTransform = new RotateTransform(TextRotation);

                SingleLabel.Content = Titles[i];
                SingleLabel.Foreground = FontColor;

                SingleLabel.HorizontalAlignment = HAlign;
                SingleLabel.HorizontalContentAlignment = HAlign;

                SingleLabel.VerticalAlignment = VAlign;
                SingleLabel.VerticalContentAlignment = VAlign;
                
                Labels.Add(SingleLabel);
                if (IsHorizontal)
                {
                    Grid.SetRow(SingleLabel, 0);
                    Grid.SetColumn(SingleLabel, j);

                    AnnotationBar.ColumnDefinitions[j].Width = new GridLength(Math.Abs(Parameters[i]- Parameters[i-1]) * 100, GridUnitType.Star);
                }
                else
                {
                    Grid.SetRow(SingleLabel,j);
                    Grid.SetColumn(SingleLabel, 0);

                    AnnotationBar.RowDefinitions[j].Height = new GridLength(Math.Abs(Parameters[i] - Parameters[i - 1]) * 100, GridUnitType.Star);
                }

                AnnotationBar.Children.Add(SingleLabel);

                j = i;
            }
            
        }

        public void SetTickOrientation( bool TickFlipped)
        {
            if (HasTick)
            {
                if (IsHorizontal)
                {
                    if (TickFlipped) { TickWeight = new Thickness(0, 1, 0, 0); } else { TickWeight = new Thickness(0, 0, 1, 0); }
                }
                else
                {
                    if (TickFlipped) { TickWeight = new Thickness(0, 0, 1, 0); } else { TickWeight = new Thickness(0, 0, 0, 1); }
                }
            }
            else
            {
                TickWeight = new Thickness(0, 0, 0, 0);
            }
        }

        public void BuildGrid(int ColumnCount, int RowCount)
        {

            for(int i = 0;i<ColumnCount;i++)
            {
                AnnotationBar.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < RowCount; i++)
            {
                AnnotationBar.RowDefinitions.Add(new RowDefinition());
            }

        }

        public void SetFontColor(bool IsLightColor)
        {
            if(IsLightColor)
            {
                FontColor = new SolidColorBrush(Color.FromArgb(255, 250, 250, 250));
                GradientColor = new SolidColorBrush(Colors.LightGray);
            }
            else
            {
                FontColor = new SolidColorBrush(Color.FromArgb(255, 50, 50, 50));
                GradientColor = new SolidColorBrush(Colors.DarkGray);
            }
        }

        public LinearGradientBrush SetFillGradient()
        {
            LinearGradientBrush gBrush = new LinearGradientBrush();
            if (IsHorizontal) { gBrush = new LinearGradientBrush(Gradient, 0.0); } else { gBrush = new LinearGradientBrush(Gradient, 90.0); }

            return gBrush;
        }

        public override void SetFill()
        {
            Element.Background = Graphics.WpfFill;
        }

        public void SetDirections(bool HorizontalFlip, bool VerticalFlip)
        {
            if(HorizontalFlip)
            { 
                GradientH = HorizontalAlignment.Right;
                TextH = HorizontalAlignment.Left;
            }
            else
            {
                GradientH = HorizontalAlignment.Left;
                TextH = HorizontalAlignment.Right;
            }

            if (VerticalFlip)
            {
                GradientV = VerticalAlignment.Bottom;
                TextV = VerticalAlignment.Top;
            }
            else
            {
                GradientV = VerticalAlignment.Top;
                TextV = VerticalAlignment.Bottom;
            }
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
            //MinLabel.Foreground = new SolidColorBrush(Graphics.FontObject.FontColor.ToMediaColor());
            //MinLabel.FontFamily = Graphics.FontObject.ToMediaFont().Family;
            //MinLabel.FontSize = Graphics.FontObject.Size;
            //MinLabel.FontStyle = Graphics.FontObject.ToMediaFont().Italic;
            //MinLabel.FontWeight = Graphics.FontObject.ToMediaFont().Bold;
            
        }

    }
}
