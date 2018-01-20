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
            Type = "GridView";

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
                    Label ttxt = new Label();
                    ttxt.Content = Wc.Sets[i].Title;
                    ttxt.Background = Wc.Graphics.GetBackgroundBrush();
                    ttxt.Foreground = Wc.Graphics.FontObject.GetFontBrush();
                    ttxt.BorderBrush = Wc.Graphics.GetStrokeBrush();
                    ttxt.BorderThickness = Wc.Graphics.GetStroke();

                    ttxt.Padding = Wc.Graphics.GetPadding();
                    ttxt.Margin = Wc.Graphics.GetMargin();

                    ttxt.FontFamily = Wc.Graphics.FontObject.ToMediaFont().Family;
                    ttxt.FontSize = Wc.Graphics.FontObject.Size;
                    ttxt.HorizontalContentAlignment = Wc.Graphics.FontObject.ToMediaFont().HAlign;
                    ttxt.VerticalContentAlignment = Wc.Graphics.FontObject.ToMediaFont().VAlign;

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

                    Label txt = new Label();
                    txt.Content = string.Format("{0:"+Pt.Format+"}", Pt.Number);

                    txt.Background = G.GetBackgroundBrush();
                    txt.Foreground = G.GetFontBrush();
                    txt.BorderBrush = G.GetStrokeBrush();
                    txt.BorderThickness = G.GetStroke();

                    txt.Padding = G.GetPadding();
                    txt.Margin = G.GetMargin();

                    txt.FontFamily = F.ToMediaFont().Family;
                    txt.FontSize = F.Size;
                    txt.HorizontalContentAlignment = F.ToMediaFont().HAlign;
                    txt.VerticalContentAlignment = F.ToMediaFont().VAlign;

                    if (G.Width > 1) { txt.Width = G.Width; } else { txt.Width = double.NaN; }
                    if (G.Height > 1) { txt.Height = G.Height; } else { txt.Height = double.NaN; }
                    

                    Grid.SetColumn(txt, j);
                    Grid.SetRow(txt, i+k);
                    Element.Children.Add(txt);
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

    }
}
