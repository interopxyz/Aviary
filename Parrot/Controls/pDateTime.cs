using System;

using System.Windows;
using System.Windows.Media;

//using MahApps.Metro.Controls;
using Xceed.Wpf.Toolkit;

using Wind.Containers;

namespace Parrot.Controls
{
    public class pDateTime : pControl
    {
        public DateTimePicker Element;
        public string Type;

        public pDateTime(string InstanceName)
        {
            //Set Element info setup
            Element = new DateTimePicker();
            Element.Name = InstanceName;
            Type = "PickDateTime";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Element.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            Element.BorderThickness = new Thickness(0);
        }

        public void SetDate(DateTime date, int mode, string format)
        {
            Element.ShowButtonSpinner = false;
            Element.AllowSpin = true;
            Element.AllowTextInput = true;
            Element.ButtonSpinnerLocation = Location.Left;

            Element.Value = date;
            Element.Format = DateTimeFormat.Custom;

            if (mode > 0)
            {
                //Element.FormatString = DateStructures(mode);
            }
            else
            {
                //Element.FormatString = format;
            }

        }

        private string DateStructures(int type)
        {
            switch (type)
            {
                case 1:
                    return "dddd, MMMM dd, yyyy ( hh:mm:ss tt )";
                case 2:
                    return "mmmm/ dd/ yyyy, hh:mm:ss tt";
                case 3:
                    return "yyyy/ mmmm/ yyyy/ dd, HH:mm:ss";
                case 4:
                    return "D";
                case 5:
                    return "d";
                case 6:
                    return "yyyy-MM-dd";
                case 7:
                    return "hh:mm tt";
                case 8:
                    return "hh: mm: s tt";
                case 9:
                    return "HH:mm:ss";
                default:
                    return "dddd, MMMM dd, yyyy ( hh:mm:ss tt )";
            }
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
