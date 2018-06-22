using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.SVG.Assembly
{
    class hGroup
    {
        public string Name = " ";
        public StringBuilder svgGroup = new StringBuilder();

        public hGroup()
        {

        }

        public hGroup(string GroupName)
        {
            Name = GroupName;
        }

        public hGroup(string GroupName, StringBuilder PathSet)
        {
            Name = GroupName;
            svgGroup.Append("<g class=\"Frames" + Name + "\" id=\"Frames" + Name + "\">" + Environment.NewLine);
            svgGroup.Append(PathSet.ToString() + Environment.NewLine);
            svgGroup.Append(" </g>" + Environment.NewLine);
        }

    }
}