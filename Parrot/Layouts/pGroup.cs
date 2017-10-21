using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Parrot.Containers;
using Wind.Containers;
using System.Windows.Media;
using System.Windows;
using Parrot.Controls;

namespace Parrot.Layouts
{
    public class pGroup : pControl
    {
        public GroupBox Element;
        public string Type;

        public pGroup(string InstanceName)
        {
            Element = new GroupBox();
            Element.Name = InstanceName;
            Type = "GroupBox";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Element.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            Element.BorderThickness = new Thickness(1);
        }

        public void SetProperties(string Title, pElement ParrotElement)
        {
            ParrotElement.DetachParent();
            
            Element.Header = Title;
            Element.Content = ParrotElement.Container;

            Element.VerticalContentAlignment = VerticalAlignment.Stretch;
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
