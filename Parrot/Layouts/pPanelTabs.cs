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
using Wind.Types;
using Wind.Presets;

namespace Parrot.Layouts
{
    public class pPanelTabs : pControl
    {
        public TabControl Element;

        public List<StackPanel> Panels = new List<StackPanel>();
        public List<TabItem> Tabs = new List<TabItem>();

        public pPanelTabs(string InstanceName)
        {
            Element = new TabControl();
            Panels = new List<StackPanel>();
            Tabs = new List<TabItem>();

        Element.Name = InstanceName;
            Type = "TabsPanel";
        }

        public void SetProperties()
        {
            Element.Items.Clear();
            Panels = new List<StackPanel>();
            Tabs = new List<TabItem>();
        }

        public void AddTab(string Name)
        {
            TabItem tab = new TabItem();
            tab.Header = Name;
            StackPanel pan = new StackPanel();
            pan.VerticalAlignment = VerticalAlignment.Stretch;
            pan.HorizontalAlignment = HorizontalAlignment.Stretch;
            
            tab.BorderBrush = new SolidColorBrush(wColors.DarkGray.ToMediaColor());
            tab.BorderThickness = new Thickness(0, 0, 0, 2);
            tab.Margin = new Thickness(0, 0, 2, 0);

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
