using System;

using System.Windows;
using System.Windows.Controls;

using Parrot.Containers;

using MahApps.Metro;
using MaterialDesignThemes.MahApps;
using System.Windows.Media.Imaging;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace Parrot.Windows
{

    public class pWindow
    {
        public ParrotWindow Element;
        public StackPanel Container;
        public ScrollViewer ScrollFrame;
        public string Type;

        public pWindow()
        {
            Element = new ParrotWindow();
            Container = new StackPanel();
            ScrollFrame = new ScrollViewer();

            Container.Orientation = Orientation.Vertical;
            Container.Margin = new Thickness(5);

            Element.Closing -= (o, e) => { Container.Children.Clear(); };
            Element.Closing += (o, e) => { Container.Children.Clear(); };

            Element.SizeToContent = SizeToContent.WidthAndHeight;
            ScrollFrame.Content = Container;
            Element.Content = ScrollFrame;

            Type = Element.GetType().Name.ToString();
        }

        public pWindow(string InstanceName)
        {
            Element = new ParrotWindow();
            Container = new StackPanel();
            ScrollFrame = new ScrollViewer();

            Container.Orientation = Orientation.Vertical;
            Container.Margin = new Thickness(5);

            Element.Name = InstanceName;

            Element.Closing -= (o, e) => { Container.Children.Clear(); };
            Element.Closing += (o, e) => { Container.Children.Clear(); };

            Element.SizeToContent = SizeToContent.WidthAndHeight;
            ScrollFrame.Content = Container;
            Element.Content = ScrollFrame;
        }

        public void SetProperties(string TitleName)
        {
            Element.Title = TitleName;
            
        }

        public void SetScroll(bool HasScroll)
        {
            if(HasScroll)
            {
                ScrollFrame.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                ScrollFrame.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
            else
            {
                ScrollFrame.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                ScrollFrame.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            }

        }

        public void SetTheme()
        {
            Palette palette = new PaletteHelper().QueryPalette();
            new PaletteHelper().SetLightDark(true);
        }

        public void AddElement(pElement ParrotElement)
        {
            ParrotElement.DetachParent();
            Container.Children.Add(ParrotElement.Container);
        }

        public void Open()
        {
            Element.OpenWindow();
        }

        public void Close()
        {
            Element.Close();
        }



    }
}
