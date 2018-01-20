using System;

using System.Windows;
using System.Windows.Controls;

using Parrot.Containers;

using MahApps.Metro;
using MaterialDesignThemes.MahApps;
using System.Windows.Media.Imaging;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Parrot.Controls;
using System.Windows.Media;
using Wind.Presets;
using System.Windows.Media.Effects;

namespace Parrot.Windows
{

    public class pWindow: pControl
    {
        public ParrotWindow Element;
        public StackPanel Container;
        public ScrollViewer ScrollFrame;

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
            Container.Background = Brushes.Transparent;

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

        public void SetTheme()
        {
            Palette palette = new PaletteHelper().QueryPalette();
            new PaletteHelper().SetLightDark(false);
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

        public void SetScroll(bool HasHorizontalScroll, bool HasVerticalScroll)
        {
            if (HasHorizontalScroll) { ScrollFrame.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto; } else { ScrollFrame.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled; }
            if (HasVerticalScroll) { ScrollFrame.VerticalScrollBarVisibility = ScrollBarVisibility.Auto; } else { ScrollFrame.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled; }
        }

        public void SetTitleBar(bool TitleBarState)
        {
            Element.ShowTitleBar = TitleBarState;
        }

        public void SetWindowControls(bool ControlState)
        {
            Element.ShowCloseButton= ControlState;
            Element.ShowMinButton = ControlState;
            Element.ShowMaxRestoreButton = ControlState;
        }

        public void SetTransparency(bool AllowTransparent)
        {
            Element.AllowsTransparency = AllowTransparent;
            if (AllowTransparent) { Element.Background = Brushes.Transparent; } else { Element.Background = new SolidColorBrush(new wColors().OffWhite().ToMediaColor()); }
        }

        public override void SetFill()
        {

                Element.Background = Graphics.GetBackgroundBrush();
        }

    }
}
