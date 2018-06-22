using System;

using System.Windows;
using System.Windows.Media;
using System.Data;
using System.Collections.Generic;
using System.Windows.Controls;

using Wind.Containers;
using Wind.Collections;
using Pollen.Collections;
using Pollen.Charts;

namespace Parrot.Controls
{
    public class pDataTableA : pChart
    {

        public DataGrid Element = new DataGrid();

        DataTable Table = new DataTable();
        DataSet DS = new DataSet();

        DataSetCollection DataCollection = new DataSetCollection();

        public pDataTableA(string InstanceName)
        {
            //Set Element info setup
            Element = new DataGrid();
            Element.Name = InstanceName;
            Type = "Table";

            //Set "Clear" appearance to all elements
        }


        public void SetProperties(DataSetCollection WindDataCollection)
        {
            Element.SelectionMode = DataGridSelectionMode.Extended;
            Element.SelectionUnit = DataGridSelectionUnit.Cell;

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

            
            Element.DataContext = DS.Tables[0].DefaultView;
            Element.ItemsSource = DS.Tables[0].DefaultView;
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
