using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wind.Containers
{
    public class wFrames
    {
        public int Index = 0;
        public double Duration = 0.333;
        public bool Persists = false;
        public bool Active = false;

        public wFrames()
        {

        }

        public wFrames(bool IsActive)
        {
            Active = IsActive;
        }

        public wFrames(int FrameIndex)
        {
            Index = FrameIndex;
            Active = true;
        }

        public wFrames(int FrameIndex, double FrameDuration)
        {
            Index = FrameIndex;
            Duration = FrameDuration;
            Active = true;
        }

        public wFrames(int FrameIndex, double FrameDuration, bool FramePersists)
        {
            Index = FrameIndex;
            Duration = FrameDuration;
            Persists = FramePersists;
            Active = true;
        }
    }
}
