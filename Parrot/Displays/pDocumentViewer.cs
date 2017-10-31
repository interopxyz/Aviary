using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Wind.Containers;

using Parrot.Controls;

namespace Parrot.Displays
{
    public class pDocumentViewer : pControl
    {
        public DocumentViewer Element;

        public pDocumentViewer(string InstanceName)
        {
            Element = new DocumentViewer();
            Element.Name = InstanceName;
            Type = "DocumentViewer";
        }

        public void SetProperties(string Address)
        {
        }

        public override void SetFill()
        {
            Element.Background = Graphics.WpfFill;
        }

        public void SetCorners(wGraphic Graphic)
        {
        }

        public void SetFont(wGraphic Graphic)
        {
        }
    }
}
