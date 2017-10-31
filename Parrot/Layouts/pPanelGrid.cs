using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Parrot.Containers;
using System.Windows;
using System.Windows.Media;
using Parrot.Controls;

namespace Parrot.Layouts
{
    public class pPanelGrid : pControl
    {
        public Grid Element;
        public int R = 0, C = 0;

        public pPanelGrid(string InstanceName)
        {
            Element = new Grid();
            Element.Name = InstanceName;
            Type = "Grid";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        public void SetProperties()
        {
            Element.ColumnDefinitions.Clear();
            Element.RowDefinitions.Clear();
            Element.Children.Clear();
        }

        public void SetColumns(int Columns)
        {

            C = Columns;

            for (int i = 0; i < C; i++)
            {
                ColumnDefinition Col = new ColumnDefinition();
                Col.Width = new GridLength(1, GridUnitType.Star);
                Element.ColumnDefinitions.Add(Col);
            }

        }

        public void SetRows(int Rows)
        {
            R = Rows;

            for (int i = 0; i < R; i++)
            {
                RowDefinition Row = new RowDefinition();
                Row.Height = new GridLength(1, GridUnitType.Star);
                Element.RowDefinitions.Add(Row);
            }
        }

        public void AddElement(pElement ParrotElement, int Ci, int Ri)
        {
            ParrotElement.DetachParent();

            Grid.SetColumn(ParrotElement.Container, Ci);
            Grid.SetRow(ParrotElement.Container, Ri);
            Element.Children.Add(ParrotElement.Container);
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
