using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using System.Windows.Media;
using System.Windows;

using Wind.Containers;

using Parrot.Containers;
using Parrot.Controls;
using Wind.Types;
using Wind.Presets;

namespace Parrot.Layouts
{
    public class pExpand : pControl
    {
        public Expander Element = new Expander();
        TextBlock tBlock = new TextBlock();

        public pExpand(string InstanceName)
        {
            Element = new Expander();
            Element.Name = InstanceName;
            Type = "Expander";
            
        }

        public void SetProperties(string Title, bool Expanded, int ExpanderDirection)
        {
            tBlock = new TextBlock();
            tBlock.Text = Title;
            if (ExpanderDirection > 1) { tBlock.LayoutTransform = new RotateTransform(-90); }else { tBlock.LayoutTransform = new RotateTransform(0); }


            Element.BorderBrush = new SolidColorBrush(new wColors().DarkGray().ToMediaColor());
            switch (ExpanderDirection)
            {
                case 1:
                    Element.BorderThickness = new Thickness(0, 1, 0, 0);
                    break;
                case 2:
                    Element.BorderThickness = new Thickness(1, 0, 0, 0);
                    break;
                case 3:
                    Element.BorderThickness = new Thickness(0, 0, 1, 0);
                    break;
                default:
                    Element.BorderThickness = new Thickness(0, 0, 0, 1);
                    break;
            }


            Element.ExpandDirection = (ExpandDirection)ExpanderDirection;

            Element.Header = tBlock;
            Element.IsExpanded = !Expanded;
            
            Element.VerticalContentAlignment = VerticalAlignment.Stretch;
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
            if (Graphics.Width < 1) 
{ Element.Width = double.NaN; } else { Element.Width = Graphics.Width; }
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
