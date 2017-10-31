using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Parrot.Containers;
using System.Windows.Media;
using Parrot.Controls;

namespace Parrot.Layouts
{
    public class pPanelPlace : pControl
    {
        public Canvas Element;

        public pPanelPlace(string InstanceName)
        {
            Element = new Canvas();
            Element.Name = InstanceName;
            Type = "PlacePanel";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        public void ClearChildren()
        {
            Element.Children.Clear();
        }

        public void SetProperties(int W, int H)
        {
            Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            Element.Width = W;
            Element.Height = H;
            
        }

        public void AddElement(pElement ParrotElement, int X, int Y)
        {
            ParrotElement.DetachParent();
            
            Element.Children.Add(ParrotElement.Container);

            Canvas.SetLeft(Element.Children[Element.Children.Count - 1], X);
            Canvas.SetTop(Element.Children[Element.Children.Count - 1], Y);
        }

        public override void SetFill()
        {
            Element.Background = Graphics.WpfFill;
        }

    }
}
