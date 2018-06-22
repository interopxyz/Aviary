using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Vectors;
using Wind.Scene.Cameras;

namespace Wind.Presets
{
    public static class wCameras
    {
        public static wCamera Top = new wCamera("Top", new wPoint(), Math.PI / 2, 0, 100, 0);
        public static wCamera Bottom = new wCamera("Bottom", new wPoint(), Math.PI / 2, Math.PI, 100, 0);
        public static wCamera Front = new wCamera("Front", new wPoint(), Math.PI / 2, -Math.PI / 2, 100, 0);
        public static wCamera Back = new wCamera("Back", new wPoint(), -Math.PI / 2, -Math.PI / 2, 100, 0);
        public static wCamera Left = new wCamera("Left", new wPoint(), 0, -Math.PI / 2, 100, 0);
        public static wCamera Right = new wCamera("Right", new wPoint(), -Math.PI, -Math.PI / 2, 100, 0);

        public static wCamera IsometricSE = new wCamera("IsometricSE", new wPoint(), Math.PI * 0.75, -Math.PI * 0.30409, 1000, 0);
        public static wCamera IsometricSW = new wCamera("IsometricSW", new wPoint(), Math.PI * 0.25, -Math.PI * 0.30409, 100, 0);
        public static wCamera IsometricNE = new wCamera("IsometricNE", new wPoint(), Math.PI * 1.306, -Math.PI * 0.30409, 100, 0);
        public static wCamera IsometricNW = new wCamera("IsometricNW", new wPoint(), Math.PI * 1.75, -Math.PI * 0.30409, 100, 0);
        
    }
}
