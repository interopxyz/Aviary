using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wind.Containers;

using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows;

namespace Parrot.Controls
{
    public class pToggle : pControl
    {
        public ToggleButton Element;
        public string Type;


        public pToggle(string InstanceName)
        {
            //Set Element info setup
            Element = new ToggleButton();
            Element.Name = InstanceName;
            Type = "Toggle";

            //Set "Clear" appearance to all elements
            Element.Margin = new System.Windows.Thickness(4);
        }

        public void SetProperties(bool State, string Name)
        {
            Element.IsChecked = State;
            Element.Content = Name;
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
