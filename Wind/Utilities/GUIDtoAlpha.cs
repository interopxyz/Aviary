using System;
using System.Linq;

namespace Wind.Utilities
{
    public class GUIDtoAlpha
    {
        public String Text;
        public GUIDtoAlpha(string T, bool TimeStamp)
        {

            if (TimeStamp) { T += new NOW().Number; }

            T = T.Replace("-", "");
            Char[] A = T.ToCharArray();

            int i = 0;

            for (i = 0; i < A.Count(); i++)
            {
                int n;
                if (int.TryParse(Convert.ToString(A[i]), out n))
                {
                    A[i] = (Char)(65 + (int)A[i]);
                }
            }

            Text = new string(A);
        }
    }
}