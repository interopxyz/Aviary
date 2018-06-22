using System;

using System.Windows;
using System.Windows.Media;
using System.Data;
using System.Collections.Generic;
using System.Windows.Controls;

using Xceed.Wpf.Toolkit;
using Xceed.Wpf.DataGrid;
using Xceed.Wpf.DataGrid.Views;

using Wind.Containers;
using Wind.Collections;
using Pollen.Collections;
using Pollen.Charts;

using MaterialDesignThemes.Wpf;

namespace Parrot.Controls
{
    public class pDataTable : pChart
    {
        public TableView dView = new TableView();

        public DataGridControl Element = new DataGridControl();

        DataTable Table = new DataTable();
        DataSet DS = new DataSet();

        DataSetCollection DataCollection = new DataSetCollection();

        public pDataTable(string InstanceName)
        {
            //Set Element info setup
            Element = new DataGridControl();
            Element.Name = InstanceName;
            Type = "Table";

            //Set "Clear" appearance to all elements
        }


        public void SetProperties(DataSetCollection WindDataCollection)
        {
            Element.SelectionMode = SelectionMode.Extended;
            Element.SelectionUnit = SelectionUnit.Cell;
            Element.NavigationBehavior = NavigationBehavior.CellOnly;
            Element.Foreground = Brushes.Aqua;
            DataCollection = WindDataCollection;

            Table = new DataTable();
            DS = new DataSet();

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

            
            Element.View = dView;
            Element.DataContext = DS.Tables[0].DefaultView;
            Element.ItemsSource = DS.Tables[0].DefaultView;
            Element.AutoCreateColumns = true;
        }

        void SetOverides(bool Alternate)
        {
            
            if (Alternate)
            {
                //Element.AlternationCount = 2;
                //Element.AlternatingRowBackground = new SolidColorBrush(Colors.LightGray);
            }
            else
            {
                //Element.AlternationCount = 0;
                //Element.AlternatingRowBackground = Element.Background;
            }
        }

        void SetGrid(int GridType)
        {
            switch (GridType)
            {
                case (1):
                    //Element.GridLinesVisibility = DataGridGridLinesVisibility.Vertical;
                    break;
                case (2):
                    //Element.GridLinesVisibility = DataGridGridLinesVisibility.Horizontal;
                    break;
                case (3):
                    //Element.GridLinesVisibility = DataGridGridLinesVisibility.All;
                    break;
                default:
                    //Element.GridLinesVisibility = DataGridGridLinesVisibility.None;
                    break;

            }
        }

        // ################################# OVERIDE GRAPHIC PROPERTIES #################################
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
            Element.Background = Graphics.WpfPattern;
        }

        public override void SetStroke()
        {
            Element.BorderBrush = Graphics.GetStrokeBrush();
            Element.BorderThickness = Graphics.GetStroke();
        }

        public override void SetFont()
        {
            Element.FontFamily = Graphics.FontObject.ToMediaFont().Family;
            Element.FontSize = Graphics.FontObject.Size;
            Element.FontStyle = Graphics.FontObject.ToMediaFont().Italic;
            Element.FontWeight = Graphics.FontObject.ToMediaFont().Bold;
        }
    }
}
