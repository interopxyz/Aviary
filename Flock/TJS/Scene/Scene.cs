using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flock.TJS.Scene
{
    public class Scene
    {
        public StringBuilder Assembly = new StringBuilder();

        public Scene()
        {
            SetDefault();
        }

        public void SetDefault()
        {
            Assembly.Clear();

            Assembly.Append("var scene = new THREE.Scene();" + Environment.NewLine);

        }

        public void AddLight()
        {
            Assembly.Append("scene.add(ambientLight);" + Environment.NewLine);
        }
    }
}
