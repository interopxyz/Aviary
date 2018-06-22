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
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace Parrot.Controls
{
    public class pDataTableX : pChart
    {
        public DataGridView TableView = new DataGridView();
        public WindowsFormsHost Host = new WindowsFormsHost();
        public DockPanel Element = new DockPanel();

        public DataSetCollection Data = new DataSetCollection();

        public string Name = "";

        public pDataTableX(string InstanceName)
        {
            Name = InstanceName;

            Element = new DockPanel();
            Host = new WindowsFormsHost();
            TableView = new DataGridView();


            //Set Generic Chart Object & Properties
            TableView.Dock = DockStyle.Fill;

            //Set WPF Winform Chart 
            Host.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Host.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            //Set Element Properties;
            Element.MinWidth = 300;
            Host.MinWidth = 300;

            Element.Width = 600;
            Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            Element.Name = Name;

            Host.Child = TableView;
            Element.Children.Add(Host);
        }


        public void SetProperties(DataSetCollection WindDataCollection)
        {
            Data = WindDataCollection;

            TableView.ColumnCount = Data.Count;
            TableView.RowCount = Data.Sets[0].Points.Count;


            for (int i = 0; i < Data.Sets.Count; i++)
            {
                if (WindDataCollection.Sets[i].Title == "") { WindDataCollection.Sets[i].Title = ("Title " + i.ToString()); }
                TableView.Columns[i].HeaderText = Data.Sets[i].Title;
                for (int j = 0; j < Data.Sets[i].Points.Count; j++)
                {
                    TableView.Rows[i].Cells[j].Value = Data.Sets[i].Points[j].Text;
                }
            }

            TableView.AutoSize = false;
            TableView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Host.Background = Brushes.IndianRed;
            TableView.BackgroundColor = System.Drawing.Color.DarkCyan;

        }
        

        //################################################################################################

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
