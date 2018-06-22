
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Containers;

namespace Hoopoe.SVG.Assembly
{


    public class hAnimation
    {
        public List<int> Indices = new List<int>();
        public List<int> Sequence = new List<int>();
        public List<double> TimeSet = new List<double>();
        public List<string> EnterState = new List<string>();
        public List<string> ExitState = new List<string>();
        public List<string> SetState = new List<string>();
        public string Intervals = "0";
        public double RunTime = 0;
        private string[] Status = { "none", "inline" };
        public bool IsPersistant = false;

        public hAnimation()
        {

        }

        public void Clear()
        {
            RunTime = 0;
            Sequence.Clear();
            Indices.Clear();
            TimeSet.Clear();
            EnterState.Clear();
            ExitState.Clear();
        }

        public void AddFrame(wFrames Frame)
        {
            RunTime += Frame.Duration;
            Sequence.Add(Indices.Count);
            Indices.Add(Frame.Index);
            TimeSet.Add(RunTime);

            if (Frame.Persists)
            {
                IsPersistant = true;
                SetState.Add("inline;");
                EnterState.Add("inline;");
                ExitState.Add("none;");
            }
            else
            {
                SetState.Add("inline;");
                EnterState.Add("none;");
                ExitState.Add("none;");
            }
            
        }

        public void CloseSet(bool Sort)
        {
            if(Sort)
            {
                int[] ArrIndex = Indices.ToArray();
                int[] ArrSequence = Sequence.ToArray();

                Array.Sort(ArrIndex, ArrSequence);

                Indices = ArrIndex.ToList();
                Sequence = ArrSequence.ToList();
            }
            
            EnterState.Add("none");
            ExitState.Add("none");

            StringBuilder TimeStamps = new StringBuilder("0");

            foreach(double Increment in TimeSet)
            {
                TimeStamps.Append(";" + Increment / RunTime);
            }

            Intervals = TimeStamps.ToString();
        }

    }
}
