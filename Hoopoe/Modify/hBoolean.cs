using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.Modify
{
    public class hBoolean : hModify
    {
        public enum FillRule { nonzero, evenodd, inherit}

        FillRule PathFillRule = FillRule.nonzero;
        
        public hBoolean()
        {
            Value = "fill-rule=\"evenodd\" ";
        }

        public hBoolean(FillRule PathBooleanRule)
        {
            PathFillRule = PathBooleanRule;
            Value = "fill-rule=\"" + PathFillRule.ToString() + "\" ";
        }

    }
}
