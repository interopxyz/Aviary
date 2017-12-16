using Hoopoe.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.Assembly
{
    public class hPath
    {

        public StringBuilder svgPath = new StringBuilder();

        public hPath()
        {

        }

        public hPath(hShape Shape)
        {
            svgPath.Append("<path id=\"" + Shape.ID + "\" d= " + Environment.NewLine);
            svgPath.Append(Shape.svgShape.ToString() + Environment.NewLine);
            svgPath.Append(" />" + Environment.NewLine);
        }

    }
}
