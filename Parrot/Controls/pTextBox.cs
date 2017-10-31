using System;

using System.Windows;
using System.Windows.Media;

using MahApps.Metro.Controls;
using Wind.Containers;
using System.Windows.Controls;

namespace Parrot.Controls
{
    public class pTextBox : pControl
    {

        public TextBoxHelper tbox;
        public TextBox Element;
        
        public pTextBox(string InstanceName)
        {
            //Set Element info setup
            Element = new TextBox();
            Element.Name = InstanceName;
            Type = "TextBox";

            //Set "Clear" appearance to all elements
        }

        public void SetProperties(string Text, bool HasText, bool Wraps, double Width)
        {
            //
            Element.AcceptsReturn = true;
            Element.Text = Text;

            if (Width > 0) { Element.Width = Width; } else { Element.Width = double.NaN; }
            if (Wraps) { Element.TextWrapping = TextWrapping.Wrap; } else { Element.TextWrapping = TextWrapping.NoWrap; }

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
            Element.TextDecorations = Graphics.FontObject.ToMediaFont().Style;
        }

    }
}
