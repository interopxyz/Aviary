using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wind.Types
{
    
    public class wDomain2D
    {
        public double U0 = 0;
        public double U1 = 1;
        public double V0 = 0;
        public double V1 = 1;

        public wDomain2D() { }

        public wDomain2D(double UMin, double UMax, double VMin, double VMax)
        {
            U0 = UMin;
            U1 = UMax;
            V0 = VMin;
            V1 = VMax;
        }

        public wDomain GetDomainU()
        {
            return new wDomain(U0, U1);
        }

        public wDomain GetDomainV()
        {
            return new wDomain(V0, V1);
        }

        public double Area()
        {
            return ((U1 - U0) * (V1 - V0));
        }

    }
}
