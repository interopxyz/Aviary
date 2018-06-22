using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flock.TJS.Build
{
    public class Header
    {

        public StringBuilder Assembly = new StringBuilder();

        public Header()
        {
            SetDefault();
        }

        public void SetDefault()
        {
            Assembly.Clear();

            Assembly.Append("<html>" + Environment.NewLine);
            Assembly.Append("<head>" + Environment.NewLine);
            Assembly.Append("<title> My first three.js app</title>" + Environment.NewLine);
            Assembly.Append("<style>" + Environment.NewLine);
            Assembly.Append("body { margin: 0; }" + Environment.NewLine);
            Assembly.Append("canvas { width: 100%; height: 100% }" + Environment.NewLine);
            Assembly.Append("</style>" + Environment.NewLine);
            Assembly.Append("</head>" + Environment.NewLine);
            Assembly.Append("<body>" + Environment.NewLine);
            Assembly.Append("<script src=\"three.js\"></script>" + Environment.NewLine);
            Assembly.Append("<script>" + Environment.NewLine);

            Assembly.Append("var renderer = new THREE.WebGLRenderer({ antialias: true });" + Environment.NewLine);
            Assembly.Append("renderer.setSize(window.innerWidth, window.innerHeight);" + Environment.NewLine);
            Assembly.Append("document.body.appendChild(renderer.domElement);" + Environment.NewLine);

            Assembly.Append("var camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);" + Environment.NewLine);

            Assembly.Append("// White directional light at half intensity shining from the top." + Environment.NewLine);
            Assembly.Append("var ambientLight = new THREE.AmbientLight(0xffffff, 10.0);" + Environment.NewLine);

            Assembly.Append("var material = new THREE.MeshBasicMaterial({color: 0x00bfff});" + Environment.NewLine);

        }

}
}
