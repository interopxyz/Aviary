using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Wind.Containers;
using Wind.Types;

namespace Wind.Collections
{
    public class CompiledDataSet
    {
        public List<CompiledData> Sets = new List<CompiledData>();
        public int Total = 0;
        /// <summary>
        /// Compiles a Data Set Object Collection from a 2D array
        /// </summary>
        public CompiledDataSet(List<string> Title, List<List<object>> DataSet, List<List<string>> FormatSet, List<List<wGraphic>> GraphicSet, List<List<wFont>> FontSet)
        {

            Total = DataSet.Count;

            int[] T = Lacing(FormatSet.Count, Total, 0);
            int[] G = Lacing(GraphicSet.Count, Total, 0);
            int[] F = Lacing(FontSet.Count, Total, 0);

            for (int i = 0; i < Total; i++)
            {
                Sets.Add(new CompiledData(Title[i], DataSet[i], FormatSet[T[i]], GraphicSet[G[i]], FontSet[F[i]]));
            }

        }

        public int[] Lacing(int Start, int Target, int Mode)
        {
            //  | 0 = Loop  | 1 = Start   | 2 = End

            Start -= 1;

            IEnumerable<int> T = new List<int>();
            T = Enumerable.Range(0, Start);
            List<int> arrVal = new List<int>();

            arrVal = T.ToList();

            for (int j = Start; j < Target; j++)
            {
                arrVal.Add(Start);
            }

            return arrVal.ToArray();
        }
    }
}

