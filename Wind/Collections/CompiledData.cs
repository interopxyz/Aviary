using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Wind.Containers;
using Wind.Types;

namespace Wind.Collections
{
    public class CompiledData
    {
        public string Tag = "";
        public string Name = "";
        public string Title = "";
        public int Total = 0;

        //Sets | Data
        public List<object> Values = new List<object>();
        public List<string> Formats = new List<string>();

        //Sets | Graphics, Fonts, Sizing
        public List<wGraphic> Graphics = new List<wGraphic>();
        public List<wFont> Fonts = new List<wFont>();

        //Sets | Font Properties
        public bool HasFont = false;
        public bool HasGraphics = false;
        public bool HasTitle = false;

        /// <summary>
        /// Compiles a Data Set Object from a 1D array
        /// </summary>
        public CompiledData(string Title, List<object> DataSet)
        {
            Values = DataSet;
            Formats.Add("String");
            Graphics.Add(new wGraphic());
            Fonts.Add(new wFont());
            MatchData();
        }

        public CompiledData(string Title, List<object> DataSet, List<string> FormatSet)
        {
            Values = DataSet;
            Formats = FormatSet;
            Graphics.Add(new wGraphic());
            Fonts.Add(new wFont());
            MatchData();
        }

        public CompiledData(string Title, List<object> DataSet, List<string> FormatSet, List<wGraphic> GraphicSet, List<wFont> FontSet)
        {
            Values = DataSet;
            Formats = FormatSet;
            Graphics = GraphicSet;
            Fonts = FontSet;
            MatchData();
        }

        public void MatchData()
        {
            Total = Values.Count();

            int C;

            C = Formats.Count;
            for (int i = Total - 1; i < C; i++)
            {
                Formats.Add(Formats[C - 1]); //Match Data Type
            }

            C = Graphics.Count;
            for (int i = Total - 1; i < C; i++)
            {
                Graphics.Add(Graphics[C - 1]); //Direct | Data Type
            }

            C = Fonts.Count;
            for (int i = Total - 1; i < C; i++)
            {
                Fonts.Add(Fonts[C - 1]); //Direct | Data Type
            }
        }

    }
}

