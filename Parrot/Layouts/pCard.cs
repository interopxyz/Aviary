using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

using Wind.Containers;

using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;
using System.Windows;

using Parrot.Containers;
using Parrot.Controls;

namespace Parrot.Layouts
{
    public class pCard : pControl
    {
        public Card Element;
        public StackPanel Layout;
        public Label Title;
        public StackPanel CardPanel;

        public string Type;

        public pCard(string InstanceName)
        {
            //Set Element info setup
            Element = new Card();
            Layout = new StackPanel();
            Title = new Label();
            CardPanel = new StackPanel();

            Layout.HorizontalAlignment = HorizontalAlignment.Stretch;
            Layout.VerticalAlignment = VerticalAlignment.Top;

            //Layout.Background = 

            ColorZone ClrA = new ColorZone();
            ClrA.Mode = ColorZoneMode.Standard;
            ClrA.Content = Title;
            
            Title.HorizontalAlignment = HorizontalAlignment.Stretch;
            Title.VerticalAlignment = VerticalAlignment.Top;
            Title.HorizontalContentAlignment = HorizontalAlignment.Left;
            Title.VerticalContentAlignment = VerticalAlignment.Center;
            
            Title.FontFamily = ClrA.FontFamily;
            Title.FontSize = ClrA.FontSize * 1.25;
                        
            Layout.Children.Add(ClrA);
            Layout.Children.Add(CardPanel);

            Element.Content = Layout;
            Element.Margin = new Thickness(2);

            Element.Name = InstanceName;
            Type = "Card";

            //Set "Clear" appearance to all elements
        }

        public void SetProperties(string Text)
        {
            CardPanel.Children.Clear();
            Title.Content = Text;
        }
        

        public void AddElement(pElement ParrotElement)
        {
            ParrotElement.DetachParent();
           CardPanel.Children.Add(ParrotElement.Container);
        }

        public void SetCorners(wGraphic Graphic)
        {
        }

        public void SetFont(wGraphic Graphic)
        {
            Element.Foreground = new SolidColorBrush(Graphic.FontObject.FontColor.ToMediaColor());
            Element.FontFamily = Graphic.FontObject.ToMediaFont().Family;
            Element.FontSize = Graphic.FontObject.Size;
            Element.FontStyle = Graphic.FontObject.ToMediaFont().Italic;
            Element.FontWeight = Graphic.FontObject.ToMediaFont().Bold;
        }

    }
}
