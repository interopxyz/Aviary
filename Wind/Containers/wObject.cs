using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wind.Containers
{
    public class wObject
    {
        public object Element = null;
        public wGraphic Graphics = new wGraphic();

        public string Type = "";
        public string SubType = "";
        public string Category = "";

        public Guid GUID = new Guid();
        public int Instance = 0;
        
        public wObject()
        {
        }

        public wObject(object ElementObject, string Main_Type, string Sub_Type)
        {
            Element = ElementObject;

            Type = Main_Type;
            SubType = Sub_Type;
        }

        public wObject(object ElementObject, string Main_Type, string Sub_Type, string Category_Type)
        {
            Element = ElementObject;

            Type = Main_Type;
            SubType = Sub_Type;
            Category = Category_Type;
        }



    }
}
