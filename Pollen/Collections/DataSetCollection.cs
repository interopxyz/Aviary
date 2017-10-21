using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Wind.Containers;
using Wind.Types;

namespace Pollen.Collections
{
    public class DataSetCollection
    {
        public string Tag = "";
        public string Name = "";
        public string Title = "";
        public int Count = 0;

        public wGraphic Graphics = new wGraphic(new wColor().Transparent(), new wColor().Transparent(), new wColor().LightGray(), 1.0, 300, 300);
        public wFont Fonts = new wFont("Arial", 8, new wColor().LightGray());

        public bool HasXAxis = false;
        public bool HasYAxis = true;

        public bool HasXLabel = false;
        public bool HasYLabel = true;

        public bool HasLeader = false;
        public int LeaderPostion = 0;

        public bool HasXGrid = false;
        public bool HasYGrid = true;

        public int XGridSpacing = 0;
        public int YGridSpacing = 0;

        public bool EnableThreeD = false;
        public int RotateX = 0;
        public int RotateY = 0; 
        public int Perspective = 0;
        public int LightingStyle = 0;

        public double YAxisMin = 0;
        public double YAxisMax = 0;

        public List<DataPtSet> Sets = new List<DataPtSet>();
        /// <summary>
        /// Compiles a Data Set Object Collection from a 2D array
        /// </summary>
        public DataSetCollection()
        {
        }

        public DataSetCollection(List<string> Title, List<List<DataPt>> DataSet)
        {
            Count = DataSet.Count;
            for (int i = 0; i < Count; i++)
            {
                Sets.Add(new DataPtSet(Title[i], DataSet[i]));
            }
        }
        
        public DataSetCollection(string Title, List<DataPt> DataSet)
        {
            Count = DataSet.Count;
            Sets.Add(new DataPtSet(Title, DataSet));
        }

        public DataSetCollection(List<string> Title, List<List<object>> DataSet)
        {
            Count = DataSet.Count;

            for (int i = (Title.Count-1); i < Count; i++)
            {
                Title.Add(Title[Title.Count - 1]);
            }

                for (int i = 0; i < Count; i++)
            {
                Sets.Add(new DataPtSet(Title[i], ObjectListToDataPointList(DataSet[i])));
            }
        }

        public DataSetCollection(string Title, List<object> DataSet)
        {
            Count = DataSet.Count;
            Sets.Add(new DataPtSet(Title, ObjectListToDataPointList(DataSet)));
        }

        public void SetAxis(int Mode, bool Grid, bool Label, int Spacing, wDomain YDomain)
        {
            if (Mode != 1)
            {
                HasXLabel = Label;
                HasXGrid = Grid;
                XGridSpacing = Spacing;
            }

            if (Mode != 2)
            {
                HasYLabel = Label;
                HasYGrid = Grid;
                YGridSpacing = Spacing;
            }

            YAxisMin = YDomain.T0;
            YAxisMax = YDomain.T1;
        }

        public void SetLabel(int Position, bool Leader)
        {
            HasLeader = Leader;
            LeaderPostion = Position;
        }

        public void SetThreeDView(bool EnabledTD, int XRotation, int YRotation, int PercentPerspective, int LightStyle)
        {
        EnableThreeD = EnabledTD;
        RotateX = XRotation;
        RotateY = YRotation;
        Perspective = PercentPerspective;

            LightingStyle = LightStyle;
        }

    private List<DataPt> ObjectListToDataPointList(List<object> objects)
        {

            List<DataPt> DataPoints = new List<DataPt>();

            for(int i = 0; i < objects.Count;i++)
            {
                wObject W = (wObject)objects[i];
                DataPoints.Add((DataPt)W.Element);
            }

            return DataPoints;
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