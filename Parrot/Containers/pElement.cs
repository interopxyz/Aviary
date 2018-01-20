using System;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Xceed.Wpf.Toolkit;
using MahApps.Metro.Controls;

using Wind.Containers;

using Parrot.Controls;
using Parrot.Layouts;
using Parrot.Displays;

using Pollen.Charts;

using MaterialDesignThemes.Wpf;

namespace Parrot.Containers
{
    public class pElement
    {
        public pControl ParrotControl = null;
        public pChart PollenControl = new pChart();
        public ColorZone Container = new ColorZone();
        public Control Element = new Control();
        public Panel Layout = null;
        public Image ImageObject = new Image();
        public ColorZone Wrapper = new ColorZone();

        public pControl Control = new pControl();

        public string Type = "";
        public string Category = "";
        public string Parent = "";

        //Default
        public pElement()
        {
        }

        //Control
        public pElement(Control WPFControl, pControl ParrotControlObject, string ElementType, bool IsNoGood)
        {
            ParrotControl = ParrotControlObject;
            Element = WPFControl;

            SetGraphics("B" + WPFControl.Name);

            Type = ElementType;
            Category = "Control";
        }

        //Control
        public pElement(Control WPFControl, pControl ParrotControlObject, string ElementType)
        {
            ParrotControl = ParrotControlObject;
            Element = WPFControl;

            SetGraphics("B" + WPFControl.Name);

            Container.Content = WPFControl;
            Container.Mode = ColorZoneMode.Standard;
            Type = ElementType;
            Category = "Control";
        }

        //Control
        public pElement(Control WPFControl, pControl ParrotControlObject, string ElementType, double Margin)
        {
            ParrotControl = ParrotControlObject;
            Element = WPFControl;

            SetGraphics("B" + WPFControl.Name);

            Container.Content = WPFControl;
            Container.Margin = new Thickness(Margin);

            Type = ElementType;
            Category = "Control";
        }

        //Control
        public pElement(ColorZone WPFBorder, pControl ParrotControlObject, string ElementType, double Margin)
        {
            ParrotControl = ParrotControlObject;
            Wrapper = WPFBorder;

            SetGraphics("B" + WPFBorder.Name);

            Container.Content = WPFBorder;
            Container.Margin = new Thickness(Margin);

            Type = ElementType;
            Category = "Control";
        }

        //Panel
        public pElement(Panel GenericPanel, pControl ParrotControlObject, string ElementType)
        {
            ParrotControl = ParrotControlObject;
            Layout = GenericPanel;

            SetGraphics("B" + GenericPanel.Name);

            Container.Content = GenericPanel;

            Type = ElementType;
            Category = "Panel";
        }

        //Image
        public pElement(Image GenericImage, pControl ParrotControlObject, string ElementType)
        {
            ParrotControl = ParrotControlObject;
            ImageObject = GenericImage;

            SetGraphics("B" + GenericImage.Name);

            Container.Content = ImageObject;

            Type = ElementType;
            Category = "Image";
        }

        //Control
        public pElement(Panel GenericPanel, pChart PollenControlObject, string ElementType)
        {
            PollenControl = PollenControlObject;
            Layout = GenericPanel;

            SetGraphics("B" + Layout.Name);

            Container.Content = Layout;
            Container.Mode = ColorZoneMode.Standard;
            Type = ElementType;
            Category = "Chart";
        }


        //Control
        public pElement(Control WPFControl, pChart PollenControlObject, string ElementType)
        {
            PollenControl = PollenControlObject;
            Element = WPFControl;

            SetGraphics("B" + WPFControl.Name);

            Container.Content = WPFControl;
            Container.Mode = ColorZoneMode.Standard;
            Type = ElementType;
            Category = "Chart";
        }

        //Remove from parent
        public void DetachParent()
        {
            if (Container.Parent != null)
            {
                Panel ParentLayout = Container.Parent as Panel;
                ParentLayout.Children.Remove(Container);
            }
        }
        
        public void SetGraphics(string ElementName)
        {
            Container.Name = ElementName;
            Container.Background = Brushes.Transparent;
        }
        
    }
}
