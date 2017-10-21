using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Containers;
using Wind.Types;

namespace Pollen_GH.Methods
{
    public class TreeToArray
    {

        public List<List<string>> FromString(GH_Structure<GH_String> Strings)
        {
            List<List<string>> arrStr = new List<List<string>>();

            for (int i = 0; i < Strings.PathCount; i++)
            {
                List<GH_String> list = Strings.Branches[i];
                List<string> lstStr = new List<string>();
                foreach (GH_String item in list)
                {
                    lstStr.Add(item.Value);
                }
                arrStr.Add(lstStr);
            }
            return arrStr;
        }

        public List<List<object>> FromObject(GH_Structure<IGH_Goo> Objects)
        {
            List<List<object>> arrObject = new List<List<object>>();
            int i = 0;

            for (i = 0; i < Objects.PathCount; i++)
            {
                List<IGH_Goo> list = Objects.Branches[i];
                List<object> lstObject = new List<object>();
                foreach (IGH_Goo item in list)
                {
                    object obj; 
                        item.CastTo(out obj);
                    lstObject.Add(obj);
                }
                arrObject.Add(lstObject);
            }
            return arrObject;
        }

        public List<List<wGraphic>> FromGraphics(GH_Structure<IGH_Goo> Graphics)
        {
            List<List<wGraphic>> arrGraphic = new List<List<wGraphic>>();
            int i = 0;

            for (i = 0; i < Graphics.PathCount; i++)
            {
                List<IGH_Goo> list = Graphics.Branches[i];
                List<wGraphic> lstGraphic = new List<wGraphic>();
                foreach (IGH_Goo item in list)
                {

                    wGraphic obj;
                    item.CastTo(out obj);
                    lstGraphic.Add(obj);
                }
                arrGraphic.Add(lstGraphic);
            }
            return arrGraphic;
        }

        public List<List<wFont>> FromFonts(GH_Structure<IGH_Goo> Fonts)
        {
            List<List<wFont>> arrFonts = new List<List<wFont>>();
            int i = 0;

            for (i = 0; i < Fonts.PathCount; i++)
            {
                List<IGH_Goo> list = Fonts.Branches[i];
                List<wFont> lstFonts = new List<wFont>();
                foreach (IGH_Goo item in list)
                {

                    wFont obj;
                    item.CastTo(out obj);
                    lstFonts.Add(obj);
                }
                arrFonts.Add(lstFonts);
            }
            return arrFonts;
        }
    }
}
