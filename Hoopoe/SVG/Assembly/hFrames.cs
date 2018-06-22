using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.SVG.Assembly
{
    public class hFrame
    {
        public int Index = 0;
        public StringBuilder svgGroup = new StringBuilder();

        public hFrame()
        {

        }

        public hFrame(int FrameIndex)
        {
            Index = FrameIndex;
        }

        public hFrame(int FrameIndex, StringBuilder PathSet, hAnimation Animation)
        {
            Animation.EnterState[FrameIndex] = Animation.SetState[FrameIndex];

            Index = FrameIndex;
            svgGroup.Append("<g class=\"Frames" + Animation.Indices[Index] + "\" id=\"Frames" + Animation.Indices[Index] + "\">" + Environment.NewLine);
            svgGroup.Append(PathSet.ToString() + Environment.NewLine);

            svgGroup.Append("<animate " + Environment.NewLine);
            svgGroup.Append("id=\"Frame" + Index + "\" " + Environment.NewLine);
            svgGroup.Append("attributeName=\"display\" " + Environment.NewLine);
            svgGroup.Append("values=\"" + string.Concat(Animation.EnterState.ToArray()) + "\" " + Environment.NewLine);
            svgGroup.Append("keyTimes=\"" + Animation.Intervals + "\" " + Environment.NewLine);
            svgGroup.Append("dur =\"" + Animation.RunTime + "s\" " + Environment.NewLine);
            svgGroup.Append("begin=\"0s\" " + Environment.NewLine);
            svgGroup.Append("repeatCount=\"indefinite\" /> " + Environment.NewLine);

            svgGroup.Append(" </g>" + Environment.NewLine);
            
            Animation.EnterState[FrameIndex] = Animation.ExitState[FrameIndex];
        }

    }
}