using System;

using System.Windows;
using System.Windows.Media;

using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xceed.Wpf.Toolkit;

using Wind.Types;
using Wind.Containers;

using Parrot.Collections;

namespace Parrot.Controls
{
    public class pColorPicker : pControl
    {
        public ColorPicker Element;
        public string Type;

        public pColorPicker(string InstanceName)
        {
            //Set Element info setup
            Element = new ColorPicker();
            Element.Name = InstanceName;
            Type = "ColorPicker";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Element.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Element.BorderThickness = new Thickness(0);

            Element.VerticalAlignment = VerticalAlignment.Center;
        }
        
        public void SetProperties(System.Drawing.Color Default, List<System.Drawing.Color> CustomColors, List<System.Drawing.Color> ColorSet, int Mode)
        {
            Element.SelectedColor = new wColor(Default).ToNullableMediaColor().Value;
            Element.ShowDropDownButton = false;
            Element.ShowAdvancedButton = false;

            pColorSets ColorSets = new pColorSets();
            switch (Mode)
            {
                case 0:
                    Element.AvailableColors = ColorSets.CustomSet(ColorSet);
                    break;
                case 1:
                    Element.AvailableColors = ColorSets.RGBrange;
                    break;
                default:
                    Element.AvailableColors = ColorSets.RGBrange;
                    break;
            }
            Element.StandardColors = ListToCollection(CustomColors);
        }

        private ObservableCollection<ColorItem> ListToCollection(List<System.Drawing.Color> Colors)
        {
            ObservableCollection<ColorItem> ColorSet = new ObservableCollection<ColorItem>();

            int i = 0;
            for (i = 0; i < Colors.Count; i++)
            {
                ColorSet.Add(new ColorItem(Color.FromArgb(Colors[i].A, Colors[i].R, Colors[i].G, Colors[i].B), Colors[i].ToString()));
            }

            return ColorSet;
        }

        public override void SetSolidFill()
        {
            Element.Background = new SolidColorBrush(Graphics.Background.ToMediaColor());
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
