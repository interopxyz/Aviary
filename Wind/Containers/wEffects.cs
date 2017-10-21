using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using Wind.Effects;

namespace Wind.Containers
{
    public class wEffects
    {
        public bool HasEffect = false;

        public wBlur Blur = new wBlur();
        public wDropShadow DropShadow = new wDropShadow();

        public Effect CurrentEffect = null;


        public wEffects()
        {
        }

        public void SetEffect(Effect SelectedEffect)
        {
            CurrentEffect = SelectedEffect;
        }

    }
}
