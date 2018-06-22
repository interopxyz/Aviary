using Pollen.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Wind.Containers;
using Wind.Presets;
using Wind.Types;

namespace Pollen.Collections
{
    public class DataSetCollection
    {
        public string Tag = "";
        public string Name = "";
        public string Title = "";
        public int Count = 0;

        public wGraphic Graphics = new wGraphic(wColors.Transparent, wColors.Transparent, wColors.LightGray, 1.0, 300, 300);
        public wFont Fonts = new wFont("Arial", 8, wColors.LightGray);


        public bool EnableThreeD = false;
        public p3D View = new p3D();

        public int TotalCustomFill = 0;
        public int TotalCustomStroke = 0;
        public int TotalCustomFont = 0;
        public int TotalCustomMarker = 0;
        public int TotalCustomLabel = 0;
        public int TotalCustomTitles = 0;

        public wAxes Axes = new wAxes(false,false);
        public wLabel Label = new wLabel();
        public wLabel ToolTip = new wLabel();

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
                DataPtSet TempSet = new DataPtSet(Title[i], DataSet[i]);

                TempSet = CalculateDefaults(TempSet);

                Sets.Add(TempSet);
            }
        }

        public DataSetCollection(string Title, List<DataPt> DataSet)
        {
            Count = DataSet.Count;
            DataPtSet TempSet = new DataPtSet(Title, DataSet);

            TempSet = CalculateDefaults(TempSet);

            Sets.Add(TempSet);
        }

        public DataSetCollection(List<string> Title, List<List<object>> DataSet)
        {
            Count = DataSet.Count;

            for (int i = (Title.Count - 1); i < Count; i++)
            {
                Title.Add(Title[Title.Count - 1]);
            }

            for (int i = 0; i < Count; i++)
            {
                DataPtSet TempSet = new DataPtSet(Title[i], ObjectListToDataPointList(DataSet[i]));

                TempSet = CalculateDefaults(TempSet);

                Sets.Add(TempSet);
            }
        }

        public DataSetCollection(string Title, List<object> DataSet)
        {
            Count = DataSet.Count;
            DataPtSet TempSet = new DataPtSet(Title, ObjectListToDataPointList(DataSet));

            TempSet = CalculateDefaults(TempSet);

            Sets.Add(TempSet);
        }
        

        public void SetUniformLabel(wLabel NewLabel, double Angle)
        {
            for (int i = 0; i < Sets.Count; i++)
            {
                for (int j = 0; j < Sets[i].Points.Count; j++)
                {
                    Sets[i].Points[j].Graphics.FontObject.Angle = Angle;

                    Sets[i].Points[j].Label.HasLeader = NewLabel.HasLeader;
                    Sets[i].Points[j].Label.Graphics = NewLabel.Graphics;
                    Sets[i].Points[j].Label.Position = NewLabel.Position;
                    Sets[i].Points[j].Label.Alignment = NewLabel.Alignment;

                    Sets[i].Points[j].CustomLabels += 1;
                }
            }
        }

        public void SetThreeDView(bool EnabledTD, p3D OverrideView)
        {
            EnableThreeD = EnabledTD;
            View = OverrideView;

        }

        public void SetSeriesScales()
        {
            for (int i = 0; i < Sets.Count; i++)
            {
                Sets[i].Graphics.Scale = this.Graphics.Scale;
            }
        }

        private List<DataPt> ObjectListToDataPointList(List<object> objects)
        {

            List<DataPt> DataPoints = new List<DataPt>();

            for (int i = 0; i < objects.Count; i++)
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

        //  ASSIGN DEFAULT GRAPHICS  ##########################################################################################

        public void SetDefaultStrokes(wStrokes.StrokeTypes StrokeType)
        {
            for (int i = 0; i < Sets.Count; i++)
            {
                for (int j = 0; j < Sets[i].Points.Count; j++)
                {
                    Sets[i].Points[j].Graphics = new wStrokes(Sets[i].Points[j].Graphics, StrokeType).GetGraphic();
                }
            }
        }

        public void SetDefaultStrokes(wStrokes.StrokeTypes StrokeType, wGradient Pallet, bool IsRandom, bool BySeries)
        {

            int Total = Pallet.ColorSet.Count;

            if (IsRandom)
            {
                for (int i = 0; i < Sets.Count; i++)
                {
                    Random rnd = new Random();
                    for (int j = 0; j < Sets[i].Points.Count; j++)
                    {
                        int Index = rnd.Next(0, Total);
                        Sets[i].Points[j].Graphics = new wStrokes(Sets[i].Points[j].Graphics, StrokeType).GetGraphic();
                        Sets[i].Points[j].Graphics.StrokeColor = Pallet.ColorSet[Index];
                    }
                }
            }
            else
            {

                int im = 0;
                int jm = 0;
                if (BySeries) { im = 1; } else { jm = 1; }

                for (int i = 0; i < Sets.Count; i++)
                {
                    for (int j = 0; j < Sets[i].Points.Count; j++)
                    {
                        Sets[i].Points[j].Graphics = new wStrokes(Sets[i].Points[j].Graphics, StrokeType).GetGraphic();
                        Sets[i].Points[j].Graphics.StrokeColor = Pallet.ColorSet[((i * im) + (j * jm)) % Total];
                    }
                }
            }

        }

        public void SetDefaultFonts(wFont CustomFont)
        {
            for (int i = 0; i < Sets.Count; i++)
            {
                for (int j = 0; j < Sets[i].Points.Count; j++)
                {
                    Sets[i].Points[j].Graphics.FontObject = CustomFont;
                }
            }
        }

        public void SetDefaultLabels(wLabel CustomLabel)
        {
            for (int i = 0; i < Sets.Count; i++)
            {
                for (int j = 0; j < Sets[i].Points.Count; j++)
                {
                    Sets[i].Points[j].SetLabel(CustomLabel);
                }
            }
        }

        public void SetDefaultPallet(wGradient Pallet, bool IsRandom, bool BySeries)
        {

            int Total = Pallet.ColorSet.Count;

            if (IsRandom)
            {
                for (int i = 0; i < Sets.Count; i++)
                {
                    Random rnd = new Random();
                    for (int j = 0; j < Sets[i].Points.Count; j++)
                    {
                        int Index = rnd.Next(0, Total);
                        Sets[i].Points[j].Graphics.Background = Pallet.ColorSet[Index];
                    }
                }
            }
            else
            {

                int im = 0;
                int jm = 0;
                if (BySeries) { im = 1; } else { jm = 1; }

                for (int i = 0; i < Sets.Count; i++)
                {
                    for (int j = 0; j < Sets[i].Points.Count; j++)
                    {
                        Sets[i].Points[j].Graphics.Background = Pallet.ColorSet[((i * im) + (j * jm)) % Total];
                    }
                }
            }

        }

        public void SetDefaultMarkers(wGradient Pallet,wMarker.MarkerType MarkerType, bool IsRandom, bool BySeries)
        {

            int Total = Pallet.ColorSet.Count;

            if (IsRandom)
            {
                for (int i = 0; i < Sets.Count; i++)
                {
                    Random rnd = new Random();
                    for (int j = 0; j < Sets[i].Points.Count; j++)
                    {
                        int Index = rnd.Next(0, Total);
                        Sets[i].Points[j].Marker.Graphics.Background = Pallet.ColorSet[Index];
                        Sets[i].Points[j].Marker.Mode = MarkerType;
                    }
                }
            }
            else
            {

                int im = 0;
                int jm = 0;
                if (BySeries) { im = 1; } else { jm = 1; }

                for (int i = 0; i < Sets.Count; i++)
                {
                    for (int j = 0; j < Sets[i].Points.Count; j++)
                    {
                        Sets[i].Points[j].Marker.Graphics.Background = Pallet.ColorSet[((i * im) + (j * jm)) % Total];
                        Sets[i].Points[j].Marker.Mode = MarkerType;
                    }
                }
            }

        }

        public void SetScales()
        {
            for (int i = 0; i < Sets.Count; i++)
            {
                Sets[i].Graphics.Scale = this.Graphics.Scale;
                Sets[i].SetScales();
            }
        }


        public void SetUniformMarkers(wMarker UniformMarker)
        {
            for (int i = 0; i < Sets.Count; i++)
            {
                for (int j = 0; j < Sets[i].Points.Count; j++)
                {
                    Sets[i].Points[j].Marker = UniformMarker;
                    Sets[i].Points[j].CustomMarkers += 1;
                    TotalCustomMarker += 1;
                }
            }
        }

        public void SetUniformTooltips(wLabel newToolTip)
        {
            for (int i = 0; i < Sets.Count; i++)
            {
                for (int j = 0; j < Sets[i].Points.Count; j++)
                {
                    Sets[i].Points[j].ToolTip.Enabled = true;
                    Sets[i].Points[j].ToolTip.Graphics = newToolTip.Graphics;
                    Sets[i].Points[j].ToolTip.Font = newToolTip.Font;
                }
            }
        }

        private DataPtSet CalculateDefaults(DataPtSet TempSet)
        {
            TotalCustomFill += TempSet.CalculateCustomFills();
            TotalCustomFont += TempSet.CalculateCustomFonts();
            TotalCustomMarker += TempSet.CalculateCustomMarkers();
            TotalCustomTitles += TempSet.Graphics.CustomFonts;
            TotalCustomStroke += TempSet.CalculateCustomStrokes();
            TotalCustomLabel += TempSet.CalculateCustomLabels();

            return TempSet;
        }

    }
}