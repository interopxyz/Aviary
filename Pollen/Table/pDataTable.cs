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

namespace Parrot.Controls
{
    public class pDataTable : pChart
    {
        public TableView dView = new TableView();
        public string Type;

        public DataGridControl Element = new DataGridControl();

        DataTable Table = new DataTable();
        DataSet DS = new DataSet();

        public pDataTable(string InstanceName)
        {
            //Set Element info setup
            Element = new DataGridControl();
            Element.Name = InstanceName;
            Type = "GridView";

            //Set "Clear" appearance to all elements
        }


        public void SetProperties(DataSetCollection WindDataCollection)
        {
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

            //DataGridCollectionViewSource VS = Table.DefaultView;

            //Element.ColumnStretchMode = ResizeCols;
            //Element.CanUserResizeRows = ResizeRows;

            //Element.CanUserSortColumns = Sortable;

            //Element.VerticalGridLinesBrush = Element.HorizontalGridLinesBrush;

            //Element.CanUserAddRows = AddRows;

            //Element.point

            //dView.DataContext = able.DefaultView;

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
        

    }
}
