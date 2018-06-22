using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Wind.Containers
{
    public class wAxes
    {
        public bool Enabled = false;
        public wAxis AxisX = new wAxis();
        public wAxis AxisY = new wAxis();

        public wAxes()
        {
        }

        public wAxes(bool HasAxisX, bool HasAxisY)
        {
            AxisX.Enabled = HasAxisX;
            AxisY.Enabled = HasAxisY;
        }
        
        
    }
}
