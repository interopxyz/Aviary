using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.Transform
{

    public class hTransform
    {

        public string Transformation = "transform=\" \"";

        public hTransform()
        {

        }

        public hTransform(List<string> Transformations)
        {
            Transformation = "transform=\"" + String.Join("", Transformations) + "\"";
        }

        public hTransform(string SingleTransformation)
        {
            Transformation = "transform=\"" + String.Join("", SingleTransformation) + "\"";
        }
    }
}
