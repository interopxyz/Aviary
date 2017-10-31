using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Parrot.Containers;
using Parrot.Collections;

using System.Windows.Media;
using Parrot.Controls;
using System.Windows;

namespace Parrot.Layouts
{
    public class pPanelWrap : pControl
    {
        public WrapPanel Element;
        public pModifiers Modify = new pModifiers();

        public pPanelWrap(string InstanceName)
        {
            Element = new WrapPanel();
            Element.Name = InstanceName;
            Type = "WrapPanel";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        public void SetProperties(bool Horizontal, int Align)
        {
            Element.Children.Clear();
            if (Horizontal)
            {
                SetDirection(Orientation.Horizontal, Align, 1);
            }
            else
            {
                SetDirection(Orientation.Vertical, 1, Align);
            }
        }

        public void AddElement(pElement ParrotElement)
        {
            ParrotElement.DetachParent();
            Element.Children.Add(ParrotElement.Container);
        }

        public void SetDirection(Orientation Orient, int hAlign, int vAlign)
        {

            Element.Orientation = Orient;

            Element.HorizontalAlignment = Modify.Halign(hAlign);
            Element.VerticalAlignment = Modify.Valign(vAlign);
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
