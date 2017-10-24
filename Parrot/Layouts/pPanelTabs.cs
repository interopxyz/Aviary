using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using System.Windows;
using System.Windows.Media;

using Wind.Containers;

using Parrot.Containers;
using Parrot.Controls;

namespace Parrot.Layouts
{
    public class pPanelTabs : pControl
    {
        public TabControl Element;
        public string Type;

        public List<StackPanel> Panels = new List<StackPanel>();
        public List<TabItem> Tabs = new List<TabItem>();

        public pPanelTabs(string InstanceName)
        {
            Element = new TabControl();
            Element.Name = InstanceName;
            Type = "TabsPanel";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Element.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Element.BorderThickness = new Thickness(0);
        }

        public void SetProperties()
        {
            Element.Items.Clear();
        }

        public void AddTab(string Name)
        {
            TabItem tab = new TabItem();
            tab.Header = Name;
            StackPanel pan = new StackPanel();
            pan.VerticalAlignment = VerticalAlignment.Stretch;
            pan.HorizontalAlignment = HorizontalAlignment.Stretch;

            tab.Content = pan;
            Element.Items.Add(tab);

            Tabs.Add(tab);
            Panels.Add(pan);
        }

        public void AddElement(int PanelIndex, pElement ParrotElement)
        {
            ParrotElement.DetachParent();
            Panels[PanelIndex].Children.Add(ParrotElement.Container);
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
