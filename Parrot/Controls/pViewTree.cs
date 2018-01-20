using System;

using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xceed.Wpf.Toolkit;
using Wind.Containers;
using System.Linq;
using System.Windows.Input;

namespace Parrot.Controls
{
    public class pViewTree : pControl
    {
        public TreeView Element;
        

        public pViewTree(string InstanceName)
        {
            //Set Element info setup
            Element = new TreeView();
            Element.Name = InstanceName;
            Type = "TreeView";

            //Set "Clear" appearance to all elements
        }

        public void ListToLevels(List<string> Values, List<int> Levels)
        {
            Element.Items.Clear();

            for (int i = 0; i <Levels.Count; i++)
            {

                if (Levels[i] > 0)
                {

                    TreeViewItem ParentItem = (TreeViewItem)Element.Items[Element.Items.Count-1];
                    ObservableCollection<TreeViewItem> ParentItems = (ObservableCollection<TreeViewItem>)ParentItem.ItemsSource;
                    if (ParentItems == null) { ParentItems = new ObservableCollection<TreeViewItem>(); }

                    for (int j = 1; j < Levels[i]; j++)
                    {
                        ParentItem = (TreeViewItem)ParentItems[ParentItems.Count - 1];
                        ParentItems = (ObservableCollection<TreeViewItem>)ParentItem.ItemsSource;
                        if (ParentItems == null) { ParentItems = new ObservableCollection<TreeViewItem>(); }
                    }

                        TreeViewItem SingleItem = new TreeViewItem();
                        SingleItem.Header = Values[i];

                        ParentItems.Add(SingleItem);

                        ParentItem.ItemsSource = ParentItems;
                }
                else
                {
                    TreeViewItem SingleItem = new TreeViewItem();
                    SingleItem.Header = Values[i];
                    Element.Items.Add(SingleItem);
                }

            }


        }

        // DRAG & DROP #########################################################################################################################################



        // PROPERTIES #########################################################################################################################################

        public void SetProperties()
        {

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
