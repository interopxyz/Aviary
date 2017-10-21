using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Parrot.Utilities
{
    public class pPrint
    {
        public PrintDialog printDialog = new PrintDialog();
        public pPrint(Border Container)
        {
            printDialog.PrintVisual(Container, "Print");
        }
    }
}
