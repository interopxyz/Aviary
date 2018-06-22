using Flock.TJS.Build;
using Flock.TJS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flock.TJS.Assemblies
{
    public class ThreeJS
    {

        public Dictionary<string, List<StringBuilder>> Assembly = new Dictionary<string, List<StringBuilder>>();
        public StringBuilder Document = new StringBuilder();


        public ThreeJS()
        {
            ResetAssembly();
        }

        public void ResetAssembly()
        {
            Document.Clear();
            Assembly.Clear();

            Assembly.Add("Geometry", new List<StringBuilder>());
            Assembly.Add("Materials", new List<StringBuilder>());

            Assembly.Add("Scene", new List<StringBuilder>());
            Assembly.Add("Cameras", new List<StringBuilder>());
            Assembly.Add("Lights", new List<StringBuilder>());

            Assembly.Add("Effects", new List<StringBuilder>());

            Assembly.Add("Header", new List<StringBuilder>());
            Assembly.Add("Footer", new List<StringBuilder>());
        }

        public void Assemble()
        {
            CompileCategory("Header");
            CompileCategory("Camera");
            CompileCategory("Lighting");
            CompileCategory("Scene");
            CompileCategory("Geometry");
            CompileCategory("Footer");
        }

        public void SetHeader(Header TjsObject)
        {
            Assembly["Header"].Clear();
            Assembly["Header"].Add(TjsObject.Assembly);
        }

        public void SetFooter(Footer TjsObject)
        {
            Assembly["Footer"].Clear();
            Assembly["Footer"].Add(TjsObject.Assembly);
        }

        public void AddGeometry(fGeometry TjsObject)
        {
            Assembly["Geometry"].Add(TjsObject.Assembly);
        }

        private void CompileCategory(string Key)
        {
            foreach (StringBuilder Element in Assembly[Key])
            {
                Append(Element);
            }
        }

        private void Append(StringBuilder Element)
        {
            Document.Append(Element);
            Document.Append(Environment.NewLine);
        }
    }
}
