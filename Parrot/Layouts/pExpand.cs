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

namespace Parrot.Layouts
{
    public class pExpand : pControl
    {
        public Expander Element;
        public string Type;

        public pExpand(string InstanceName)
        {
            Element = new Expander();
            Element.Name = InstanceName;
            Type = "Expander";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Element.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            Element.BorderThickness = new Thickness(1);
        }

        public void SetProperties(string Title, bool Expanded, pElement ParrotElement)
        {
            ParrotElement.DetachParent();

            Element.Header = Title;
            Element.IsExpanded = Expanded;
            Element.Content = ParrotElement.Container;

            Element.VerticalContentAlignment = VerticalAlignment.Stretch;
        }

        public override void SetFill()
        {
            Element.Background = Graphics.WpfFill;
        }

        public void SetCorners(wGraphic Graphic)
        {
        }

        public void SetFont(wGraphic Graphic)
        {
            Element.FontFamily = Graphic.FontObject.ToMediaFont().Family;
            Element.FontSize = Graphic.FontObject.Size;
            Element.FontStyle = Graphic.FontObject.ToMediaFont().Italic;
            Element.FontWeight = Graphic.FontObject.ToMediaFont().Bold;
        }

    }
}
