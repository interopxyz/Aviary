using Flock.TJS.Assemblies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flock.TJS.Build
{
    public class Compile3JS
    {
        public ThreeJS ThreeJSAssembly = new ThreeJS();

        public Compile3JS()
        {

        }

        public Compile3JS(ThreeJS ThreeJSassembly)
        {
            ThreeJSAssembly = ThreeJSassembly;
        }

        public void BuildFile()
        {

        }

        public void Save(string FilePath)
        {
            StreamWriter Writer = new StreamWriter(FilePath);
            Writer.Write(ThreeJSAssembly.Document.ToString());
            Writer.Close();
        }

    }
}
