using Parrot.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Wind.Containers;

namespace Parrot.Displays
{
    public class pWebBrowser : pControl
    {
        public Grid Element;
        public WebBrowser Web;
        public string Type;

        public pWebBrowser(string InstanceName)
        {
            Element = new Grid();
            Web = new WebBrowser();

            Element.Name = InstanceName;
            Type = "WebBrowser";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        public void SetProperties(string Address)
        {
            Element.VerticalAlignment = VerticalAlignment.Stretch;
            Element.HorizontalAlignment = HorizontalAlignment.Stretch;
            Element.Children.Add(Web);

            Web.VerticalAlignment = VerticalAlignment.Stretch;
            Web.HorizontalAlignment = HorizontalAlignment.Stretch;
            Web.Navigate(Address);
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
