using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Wind.Types;

namespace Wind.Graphics
{
    public class wFillSolid: wFill
    {

        public wFillSolid()
        {
        }

        public wFillSolid(wColor FillColor)
        {
            FillBrush = new SolidColorBrush(FillColor.ToMediaColor());
        }

    }
}
