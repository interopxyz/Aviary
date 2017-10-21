using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Parrot.Windows;

using Parrot_GH.Controls;
using Parrot_GH.Displays;
using Parrot_GH.Layouts;
using Grasshopper.Kernel;

using Wind.Containers;

namespace Parrot_GH.Utilities
{
    public class SetComponentWindow
    {
        public SetComponentWindow(IGH_Component C, wObject W, string T, int R)
        {
            switch (T)
            {
                case "TextBox":
                    TextBox A15 = (TextBox)C;
                    A15.Elements[R] = W;
                    break;
            }

        }
    }
}
