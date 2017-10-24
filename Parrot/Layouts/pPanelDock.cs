using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Parrot.Containers;
using System.Windows.Media;
using Parrot.Controls;

namespace Parrot.Layouts
{
    public class pPanelDock : pControl
    {
        public DockPanel Element;
        public string Type;

        public pPanelDock(string InstanceName)
        {
            Element = new DockPanel();
            Element.Name = InstanceName;
            Type = "DockPanel";

            //Set "Clear" appearance to all elements
            Element.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        public void SetProperties()
        {
            Element.Children.Clear();
        }

        public void AddElements(pElement ParrotElement, int Direction)
        {
            ParrotElement.DetachParent();
            DockPanel.SetDock(ParrotElement.Container, DockDirection(Direction));
            Element.Children.Add(ParrotElement.Container);
        }

        public void LastElement(pElement ParrotElement)
        {
            ParrotElement.DetachParent();
            Element.Children.Add(ParrotElement.Container);
        }

        public Dock DockDirection(int I)
        {
            switch (I)
            {
                case 1:
                    return Dock.Bottom;
                case 2:
                    return Dock.Left;
                case 3:
                    return Dock.Right;
                default:
                    return Dock.Top;
            }

        }

        public override void SetFill()
        {
            Element.Background = Graphics.WpfFill;
        }

    }
}
