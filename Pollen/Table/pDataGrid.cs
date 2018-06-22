using System;

using System.Windows;
using System.Windows.Media;
using System.Data;
using System.Collections.Generic;

using System.Windows.Controls;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.DataGrid;

using Wind.Containers;
using Wind.Collections;
using Pollen.Collections;
using Pollen.Charts;
using Wind.Types;

namespace Parrot.Controls
{
    public class pDataGrid : pChart
    {
        public Grid Element = new Grid();
        private DataSetCollection Wc = new DataSetCollection();

        public pDataGrid(string InstanceName)
        {
            //Set Element info setup
            Element = new Grid();
            Element.Name = InstanceName;
            Type = "Table";

            //Set "Clear" appearance to all elements
        }
        

        public void SetProperties(DataSetCollection WindDataCollection, bool HasTitles)
        {
            Element.Children.Clear();
            Element.RowDefinitions.Clear();
            Element.ColumnDefinitions.Clear();

            Wc = WindDataCollection;

            for (int i = 0; i < Wc.Sets.Count; i++)
            {
                if (Wc.Sets[i].Title == "") { Wc.Sets[i].Title = ("Title " + i.ToString()); }

                ColumnDefinition col = new ColumnDefinition();
                Element.ColumnDefinitions.Add(col);
            }

            int k = 0;
            if (HasTitles)
            {
                k = 1;

                RowDefinition TitleRow = new RowDefinition();
                Element.RowDefinitions.Add(TitleRow);

                for (int  i = 0; i < Wc.Sets.Count; i++)
                {
                    wGraphic G = Wc.Graphics;

                    Label ttxt = new Label();
                    ttxt.Content = Wc.Sets[i].Title;
                    ttxt.Background = G.GetBackgroundBrush();
                    ttxt.Foreground = G.FontObject.GetFontBrush();
                    ttxt.BorderBrush = G.GetStrokeBrush();
                    ttxt.BorderThickness = G.GetStroke();

                    ttxt.Padding = G.GetPadding();
                    ttxt.Margin = G.GetMargin();

                    ttxt.FontFamily = G.FontObject.ToMediaFont().Family;
                    ttxt.FontSize = G.FontObject.Size;
                    ttxt.FontStyle = G.FontObject.ToMediaFont().Italic;
                    ttxt.FontWeight = G.FontObject.ToMediaFont().Bold;
                    ttxt.HorizontalContentAlignment = G.FontObject.ToMediaFont().HAlign;
                    ttxt.VerticalContentAlignment = G.FontObject.ToMediaFont().VAlign;

                    Grid.SetColumn(ttxt, i);
                    Grid.SetRow(ttxt, 0);
                    Element.Children.Add(ttxt);
                }
            }

            for (int i = 0; i < Wc.Sets[0].Points.Count; i++)
            {
                RowDefinition row = new RowDefinition();
                Element.RowDefinitions.Add(row);
                for (int j = 0; j < (Wc.Count ); j++)
                {
                    DataPt Pt = Wc.Sets[j].Points[i];
                    wGraphic G = Pt.Graphics;
                    wFont F = G.FontObject;

                    Border brd = new Border();
                    Label txt = new Label();
                    txt.Content = string.Format("{0:"+Pt.Label.Format+"}", Pt.Number);

                    txt.Background = Brushes.Transparent;
                    txt.Foreground = G.GetFontBrush();
                    txt.BorderBrush = Brushes.Transparent;
                    txt.BorderThickness = new Thickness(0);

                    txt.Padding = G.GetPadding();
                    txt.Margin = G.GetMargin();

                    txt.FontFamily = F.ToMediaFont().Family;
                    txt.FontSize = F.Size;
                    txt.FontStyle = G.FontObject.ToMediaFont().Italic;
                    txt.FontWeight = G.FontObject.ToMediaFont().Bold;
                    txt.HorizontalContentAlignment = F.ToMediaFont().HAlign;
                    txt.VerticalContentAlignment = F.ToMediaFont().VAlign;

                    if (G.Width > 1) { txt.Width = G.Width; } else { txt.Width = double.NaN; }
                    if (G.Height > 1) { txt.Height = G.Height; } else { txt.Height = double.NaN; }


                    brd.CornerRadius = G.GetCorner();
                    brd.Background = G.GetBackgroundBrush();
                    brd.BorderBrush = G.GetStrokeBrush();
                    brd.BorderThickness = G.GetStroke();

                    brd.Child = txt;

                    Grid.SetColumn(brd, j);
                    Grid.SetRow(brd, i+k);
                    Element.Children.Add(brd);
                }
            }


        }

        
        public override void SetSize()
        {
            double W = double.NaN;
            double H = double.NaN;
            if (Graphics.Width > 0) { W = Graphics.Width; }
            if (Graphics.Height > 0) { H = Graphics.Height; }

            Element.Width = W;
            Element.Height = H;

        }

        public override void SetSolidFill()
        {
            Element.Background = Graphics.GetBackgroundBrush();
        }

        public override void SetGradientFill()
        {
            Element.Background = Graphics.WpfFill;
        }

        public override void SetPatternFill()
        {
            Element.Background = Graphics.WpfFill;
        }

        public override void SetStroke()
        {
        }

        public override void SetFont()
        {
            
        }

        public override void SetMargin()
        {
            Element.Margin = Graphics.GetMargin();
        }

        public override void SetPadding()
        {

        }

    }
}
