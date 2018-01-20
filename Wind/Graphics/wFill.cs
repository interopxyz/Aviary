using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Wind.Graphics
{
    abstract public class wFill
    {
        public Brush FillBrush = new SolidColorBrush(Colors.White);
        public DrawingBrush DwgBrush = new DrawingBrush();
        public DrawingGroup DwgGroup = new DrawingGroup();
        public GradientBrush GrdBrush = new LinearGradientBrush(Colors.LightGray,Colors.SlateGray,0.0);

        public wFill()
        {
        }
        
    }
}
