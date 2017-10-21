using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Wind.Containers;
using Wind.Types;

namespace Pollen.Collections
{
    public class DataPtSet
    {
        public string Tag = "";
        public string Name = "";
        public string Title = "";
        public int Count = 0;
        public double Total = 0;
        public double NumericSum = 0;
        public wDomain NumericBounds = new wDomain();

        public wGraphic Graphics = new wGraphic();
        public wFont Fonts = new wFont();
        public wSize Sizes = new wSize();

        //Sets | Data
        public List<DataPt> Points = new List<DataPt>();

        //Sets | Font Properties
        public bool HasFonts = false;
        public bool HasGraphics = false;
        public bool HasTitle = false;

        /// <summary>
        /// Compiles a Data Set Object from a 1D array
        /// </summary>
        public DataPtSet()
        {

        }

        public DataPtSet(List<DataPt> DataPointSet)
        {
            Points = DataPointSet;
            Count = DataPointSet.Count;
            CalculateDataExtents();
        }

        public DataPtSet(string CollectionTitle, List<DataPt> DataPointSet)
        {
            Title = CollectionTitle;
            Points = DataPointSet;
            Count = DataPointSet.Count;
            CalculateDataExtents();
        }

        public DataPtSet(string CollectionTitle, List<object> DataPointSet)
        {
            Title = CollectionTitle;
            Points = ObjectListToDataPointList(DataPointSet);
            Count = DataPointSet.Count;
        }

        private List<DataPt> ObjectListToDataPointList(List<object> objects)
        {


            List<DataPt> DataPoints = new List<DataPt>();

            double[] TempNumberSet = new double[objects.Count];
            NumericSum = 0;

            for (int i = 0; i < objects.Count; i++)
            {
                wObject W = (wObject)objects[i];
                DataPt Pt = (DataPt)W.Element;
                DataPoints.Add((DataPt)W.Element);

                TempNumberSet[i] = Pt.Number;
                NumericSum += Pt.Number;
            }

            Array.Sort(TempNumberSet);
            NumericBounds = new wDomain(TempNumberSet[0], TempNumberSet[TempNumberSet.Count() - 1]);

            return DataPoints;
        }

        private void CalculateDataExtents()
        {

            double[] TempNumberSet = new double[Points.Count];
            NumericSum = 0;

            for (int i = 0; i < Points.Count; i++)
            {
                DataPt Pt = Points[i];

                TempNumberSet[i] = Pt.Number;
                NumericSum += Pt.Number;
            }

            Array.Sort(TempNumberSet);

            NumericBounds = new wDomain(TempNumberSet[0], TempNumberSet[TempNumberSet.Count() - 1]);

        }


    }
}