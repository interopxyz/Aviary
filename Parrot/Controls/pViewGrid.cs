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

namespace Parrot.Controls
{
    public class pViewGrid : pControl
    {
        public DataGrid Element;
        public string Type;

        public pViewGrid(string InstanceName)
        {
            //Set Element info setup
            Element = new DataGrid();
            Element.Name = InstanceName;
            Type = "GridView";

            //Set "Clear" appearance to all elements
        }
        

        public void SetProperties(DataSetCollection WindDataCollection, int GridType, bool ResizeRows, bool ResizeCols, bool Sortable, bool Alternate, bool AddRows)
        {
            DataTable Table = new DataTable();
            System.Data.DataSet DS = new System.Data.DataSet();

            DS.Tables.Add(Table);
            for (int i = 0; i < WindDataCollection.Sets.Count; i++)
           {
                if (WindDataCollection.Sets[i].Title == "") { WindDataCollection.Sets[i].Title = ("Title " + i.ToString()); }
                DataColumn col = new DataColumn(WindDataCollection.Sets[i].Title.ToString(), typeof(string));
                Table.Columns.Add(col);
            }

            for (int i = 0; i < WindDataCollection.Sets[0].Points.Count; i++)
            {
                System.Data.DataRow row = Table.NewRow();
                for (int j = 0; j < WindDataCollection.Count; j++)
                {
                    row[WindDataCollection.Sets[j].Title] = WindDataCollection.Sets[j].Points[i].Text;
                }
                Table.Rows.Add(row);
            }

            Element.CanUserResizeColumns = ResizeCols;
            Element.CanUserResizeRows = ResizeRows;

            Element.CanUserSortColumns = Sortable;

            Element.VerticalGridLinesBrush = Element.HorizontalGridLinesBrush;

            Element.CanUserAddRows = AddRows;

            switch (GridType)
            {
                case (1):
                    Element.GridLinesVisibility = DataGridGridLinesVisibility.Vertical;
                    break;
                case (2):
                    Element.GridLinesVisibility = DataGridGridLinesVisibility.Horizontal;
                    break;
                case (3):
                    Element.GridLinesVisibility = DataGridGridLinesVisibility.All;
                    break;
                default:
                    Element.GridLinesVisibility = DataGridGridLinesVisibility.None;
                    break;

            }

            if (Alternate) {
                Element.AlternationCount = 2;
                Element.AlternatingRowBackground = new SolidColorBrush(Colors.LightGray);
            }
            else
            {
                Element.AlternationCount = 0;
                Element.AlternatingRowBackground = Element.Background;
            }

            Element.ItemsSource = Table.DefaultView;
            Element.AutoGenerateColumns = true;
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
