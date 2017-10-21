using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using System.Windows.Controls;
using Wind.Containers;
using Parrot.Collections;

using MaterialDesignThemes.Wpf;
using System.Windows;

namespace Parrot.Controls
{
    public class pClock : pControl
    {
        public Clock Element;
        public string Type;
        public pModifiers Modify = new pModifiers();

        public pClock(string InstanceName)
        {
            //Set Element info setup
            Element = new Clock();
            Element.Name = InstanceName;
            Type = "Clock";

            //Set "Clear" appearance to all elements

        }

        public void SetProperties(DateTime SelectedDate, bool SelectionMode)
        {
            Element.Is24Hours = SelectionMode;
            Element.Time = SelectedDate;
           
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