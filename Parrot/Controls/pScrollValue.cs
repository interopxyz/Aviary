using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Xceed.Wpf.Toolkit;

using Wind.Containers;

namespace Parrot.Controls
{
    public class pScrollValue : pControl
    {
        public ButtonSpinner Element;
        public string Type;

        public string Value;
        public List<string> ValueSet = new List<string>();
        public int Index;
        public int MinVal;
        public int MaxVal;
        public bool IsWrapped = false;

        public pScrollValue(string InstanceName)
        {
            //Set Element info setup
            Element = new ButtonSpinner();
            Element.Name = InstanceName;
            Type = "ScrollValue";
            

            Element.Spin -= (o, e) => { Element.Content = ValueSet[CapValue(e.Direction == SpinDirection.Increase)]; };
            Element.Spin += (o, e) => { Element.Content = ValueSet[CapValue(e.Direction == SpinDirection.Increase)]; };

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Element.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Element.BorderThickness = new Thickness(0);
        }

        public void SetProperties(List<string> values, int index, bool Cycle)
        {
            ValueSet = values;
            MaxVal = ValueSet.Count-1;
            MinVal = 0;
            Index = index;

            IsWrapped = Cycle;

            Element.MinWidth = 100;
            Element.Content = values[Index];
            
        }

        private int CapValue(bool dir)
        {
            if (dir) { Index += 1; } else { Index -= 1; }

            if (Index < MinVal) { if (IsWrapped) { Index = MaxVal; } else { Index = MinVal; } }
            if (Index > MaxVal) { if (IsWrapped) { Index = MinVal; } else { Index = MaxVal; } }
            
            return Index;
        }

        public override void SetFill()
        {
            Element.Background = Graphics.WpfFill;
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
