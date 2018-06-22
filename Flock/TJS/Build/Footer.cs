using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flock.TJS.Build
{
    public class Footer
    {
        public StringBuilder Assembly = new StringBuilder();

        public Footer()
        {
            SetDefault();
        }

        public void SetDefault()
        {
            Assembly.Clear();

            Assembly.Append("camera.position.z = 5;"+Environment.NewLine);
            Assembly.Append("renderer.render(scene, camera);" + Environment.NewLine);
            Assembly.Append("</script>" + Environment.NewLine);
            Assembly.Append("</body>" + Environment.NewLine);
            Assembly.Append("</html>" + Environment.NewLine);

        }

    }
}
