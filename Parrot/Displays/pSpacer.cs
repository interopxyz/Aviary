using Parrot.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;
using Wind.Containers;

namespace Parrot.Displays
{
    public class pSpacer : pControl
    {
        public UniformGrid Element;

        public bool IsHoriztonal = false;
        public bool IsFill = true;

        public pSpacer(string InstanceName)
        {
            //Set Element info setup
            Element = new UniformGrid();
            Element.Name = InstanceName;
            Type = "Spacer";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        public void SetProperties(System.Windows.Media.Color FillColor, double Width, bool IsHorizontalAlignment, bool IsFilled)
        {

            Element.Children.Clear();
            

            IsHoriztonal = IsHorizontalAlignment;
            IsFill = IsFilled;

            if (IsHoriztonal)
            {
                Element.Width = double.NaN;
                Element.HorizontalAlignment = HorizontalAlignment.Stretch;
                if (Width == 0)
                {
                    Element.Height = double.NaN;
                    Element.VerticalAlignment= VerticalAlignment.Stretch;
                } else {
                    Element.Height = Width;
                }
            }
            else
            {
                Element.Height = double.NaN;
                Element.VerticalAlignment = VerticalAlignment.Stretch;
                if (Width == 0)
                {
                    Element.Width = double.NaN;
                    Element.HorizontalAlignment = HorizontalAlignment.Stretch;
                }
                else
                {
                    Element.Width = Width;
                }
            }

            if (IsFilled)
            {
                Element.Background = new SolidColorBrush(FillColor);
            }
            else
            {
                Element.Background = new SolidColorBrush(Colors.Transparent);

                Label LabelA = new Label();
                LabelA.BorderBrush = new SolidColorBrush(FillColor);

                Element.Children.Add(LabelA);

                if (IsHoriztonal)
                {
                    Element.Columns = 1;
                    Element.Rows = 2;

                    Grid.SetColumn(LabelA, 0);
                    Grid.SetRow(LabelA, 0);

                    LabelA.BorderThickness = new Thickness(0, 0, 0, 1);
                }
                else
                {
                    Element.Columns = 2;
                    Element.Rows = 1;

                    Grid.SetColumn(LabelA, 0);
                    Grid.SetRow(LabelA, 0);

                    LabelA.BorderThickness = new Thickness(0, 0,1, 0);
                }

            }

        }

        public override void SetFill()
        {
            Element.Background = Graphics.WpfFill;
        }

        public override void SetStroke()
        {
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
        }

        public override void SetFont()
        {
        }
    }
}
