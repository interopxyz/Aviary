using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wind.Types
{
    
    public class wDomain3D
    {
        public double U0 = 0;
        public double U1 = 1;
        public double V0 = 0;
        public double V1 = 1;
        public double W0 = 0;
        public double W1 = 1;

        public wDomain3D() { }

        public wDomain3D(double UMin, double UMax, double VMin, double VMax, double WMin, double WMax)
        {
            U0 = UMin;
            U1 = UMax;
            V0 = VMin;
            V1 = VMax;
            W0 = WMin;
            W1 = WMax;
        }

        public wDomain GetDomainU()
        {
            return new wDomain(U0, U1);
        }

        public wDomain GetDomainV()
        {
            return new wDomain(V0, V1);
        }

        public wDomain GetDomainW()
        {
            return new wDomain(W0, W1);
        }

        public wDomain2D GetUVDomain()
        {
            return new wDomain2D(U0, U1, V0, V1);
        }

        public wDomain2D GetUWDomain()
        {
            return new wDomain2D(U0, U1, W0, W1);
        }

        public wDomain2D GetVWDomain()
        {
            return new wDomain2D(V0, V1, W0, W1);
        }

        public double Volume()
        {
            return ((U1 - U0) * (V1 - V0) * (W1 - W0));
        }

    }
}
