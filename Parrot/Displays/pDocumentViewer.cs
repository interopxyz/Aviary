using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Wind.Containers;

namespace Parrot.Displays
{
    public class pDocumentViewer
    {
        public DocumentViewer Element;
        public string Type;

        public pDocumentViewer(string InstanceName)
        {
            Element = new DocumentViewer();
            Element.Name = InstanceName;
            Type = "DocumentViewer";
        }

        public void SetProperties(string Address)
        {
        }

        public void SetCorners(wGraphic Graphic)
        {
        }

        public void SetFont(wGraphic Graphic)
        {
        }
    }
}
