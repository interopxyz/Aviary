using AForge.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mDitherThresholdCarry : mFilter
    {
        ThresholdWithCarry Effect = new ThresholdWithCarry();

        byte Parameter = 50;

        public mDitherThresholdCarry(byte ParameterValue)
        {

            BitmapType = 0;

            Parameter = ParameterValue;

            Effect = new ThresholdWithCarry(Parameter);
            
            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}

