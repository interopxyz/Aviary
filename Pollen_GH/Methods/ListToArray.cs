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
    public class ListToArray
    {

        public List<string> FromString(List<GH_String> Strings)
        {

            List<string> arrStr = new List<string>();
                foreach (GH_String item in Strings)
                {
                arrStr.Add(item.Value);
                }

            return arrStr;
        }

        public List<object> FromObject(List<IGH_Goo> Objects)
        {

            List<object> arrObject = new List<object>();
                foreach (IGH_Goo item in Objects)
                {
                object obj;
                item.CastTo(out obj);
                arrObject.Add(obj);
                }

            return arrObject;
        }


    }
}
