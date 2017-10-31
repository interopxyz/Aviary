using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Parrot.Containers;
using System.Windows.Media;
using Parrot.Controls;
using System.Windows;

namespace Parrot.Layouts
{
    public class pPanelStack : pControl
    {
        public StackPanel Element;

        public pPanelStack(string InstanceName)
        {
            Element = new StackPanel();
            Element.Name = InstanceName;
            Type = "StackPanel";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        public void SetProperties(bool Horizontal)
        {
            Element.Children.Clear();
            if (Horizontal)
            {
                Element.Orientation = Orientation.Horizontal;
            }
            else
            {
                Element.Orientation = Orientation.Vertical;
            }
        }

        public void AddElement(pElement ParrotElement)
        {
            ParrotElement.DetachParent();
            Element.Children.Add(ParrotElement.Container);
        }
        
        public override void SetFill()
        {
            Element.Background = Graphics.WpfFill;
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
        

    }
}
