using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wind.Types
{
    
    public class wDomain
    {
        public double T0 = 0;
        public double T1 = 0;

        public wDomain() { }

        public wDomain(double Min, double Max)
        {
            T0 = Min;
            T1 = Max;
        }
        
    }
}
