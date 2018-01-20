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

        public double BarScale = 0.75;
        public double PieScale = 1.0;
        public double PointScale = 1.0;
        
        /// <summary>
        /// Compiles a Data Set Object from a 1D array
        /// </summary>
        public DataPtSet()
        {

        }

        public DataPtSet(List<DataPt> DataSet)
        {
            Points = DataSet;
            Count = DataSet.Count;
            CalculateDataExtents();
        }

        public DataPtSet(string CollectionTitle, List<DataPt> DataSet)
        {
            Title = CollectionTitle;
            Points = DataSet;
            Count = DataSet.Count;
            CalculateDataExtents();
        }

        public DataPtSet(string CollectionTitle, List<object> DataSet)
        {
            Title = CollectionTitle;
            Points = ObjectListToDataPointList(DataSet);
            Count = DataSet.Count;
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


        public int CalculateCustomStrokes()
        {
            int TotalCustom = 0;

            for (int i = 0; i < Points.Count; i++)
            {
                TotalCustom += Points[i].Graphics.CustomStrokes;
            }
            return TotalCustom;
        }

        public int CalculateCustomLabels()
        {
            int TotalCustom = 0;

            for (int i = 0; i < Points.Count; i++)
            {
                TotalCustom += Points[i].CustomLabels;
            }
            return TotalCustom;
        }

        public int CalculateCustomMarkers()
        {
            int TotalCustom = 0;

            for (int i = 0; i < Points.Count; i++)
            {
                TotalCustom += Points[i].CustomMarkers;
            }
            return TotalCustom;
        }

        public int CalculateCustomFills()
        {
            int TotalCustom = 0;

            for (int i = 0; i < Points.Count; i++)
            {
                TotalCustom += Points[i].Graphics.CustomFills;
            }
            return TotalCustom;
        }

        public int CalculateCustomFonts()
        {
            int TotalCustom = 0;

            for (int i = 0; i < Points.Count; i++)
            {
                TotalCustom += Points[i].Graphics.CustomFonts;
            }
            return TotalCustom;
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

        public void SetScales()
        {
            BarScale = Graphics.Scale;
            PieScale = Graphics.Scale;
            PointScale = Graphics.Scale;
        }


    }
}