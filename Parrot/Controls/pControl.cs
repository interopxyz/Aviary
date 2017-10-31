using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Containers;
using Wind.Types;

namespace Parrot.Controls
{
    public class pControl
    {
        public wGraphic Graphics = new wGraphic();
        public wEffects Effects = new wEffects();
        public wFont Font = new wFont();

        public string Type = "Control";

        public virtual void SetFont()
        {

        }

        public virtual void SetFill()
        {

        }

        public virtual void SetSolidFill()
        {

        }

        public virtual void SetPatternFill()
        {

        }

        public virtual void SetGradientFill()
        {

        }

        public virtual void SetStroke()
        {

        }

        public virtual void SetCorners()
        {

        }
        
        public virtual void SetSize()
        {

        }

        public virtual void SetMargin()
        {

        }

        public virtual void SetPadding()
        {

        }
        
    }
}
