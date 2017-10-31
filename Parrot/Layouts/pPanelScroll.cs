using Parrot.Containers;
using Parrot.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Parrot.Layouts
{
    public class pPanelScroll : pControl
    {
        public ScrollViewer Element;

        public pPanelScroll(string InstanceName)
        {
            Element = new ScrollViewer();
            Element.Name = InstanceName;
            Type = "PlacePanel";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        public void SetProperties( bool HorizontalScroll, bool VerticalScroll, bool Auto)
        {

            Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            
            if (HorizontalScroll){
                if (Auto) { Element.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto; } else { Element.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible; }
            }
            else
            {
                Element.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }

            if (VerticalScroll)
            {
                if (Auto) { Element.VerticalScrollBarVisibility = ScrollBarVisibility.Auto; } else { Element.VerticalScrollBarVisibility = ScrollBarVisibility.Visible; }
            }
            else
            {
                Element.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
            
        }

        public void SetElement(pElement ParrotElement)
        {
            Element.Content = null;

            ParrotElement.DetachParent();
            Element.Content = ParrotElement.Container;

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
