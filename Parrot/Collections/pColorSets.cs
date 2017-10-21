using System;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xceed.Wpf.Toolkit;

namespace Parrot.Collections
{

    public class pColorSets
    {
        public ObservableCollection<ColorItem> RGBrange = new ObservableCollection<ColorItem>();
        public List<System.Drawing.Color> Standard = new List<System.Drawing.Color>();

        public pColorSets()
        {
            RGBrange = RGBSet();
            Standard = BasicSet();
        }

        private List<System.Drawing.Color> BasicSet()
        {
            List<System.Drawing.Color> ColorSet = new List<System.Drawing.Color>();
            ColorSet.Add(System.Drawing.Color.Transparent);
            ColorSet.Add(System.Drawing.Color.White);
            ColorSet.Add(System.Drawing.Color.Black);
            ColorSet.Add(System.Drawing.Color.Red);
            ColorSet.Add(System.Drawing.Color.Orange);
            ColorSet.Add(System.Drawing.Color.Yellow);
            ColorSet.Add(System.Drawing.Color.Lime);
            ColorSet.Add(System.Drawing.Color.Cyan);
            ColorSet.Add(System.Drawing.Color.Blue);
            ColorSet.Add(System.Drawing.Color.Magenta);

            return ColorSet;
        }

        public ObservableCollection<ColorItem> CustomSet(List<System.Drawing.Color> Colors)
        {
            ObservableCollection<ColorItem> ColorSet = new ObservableCollection<ColorItem>();

            for (int i = 0; i < Colors.Count; i++)
            {
                ColorSet.Add(new ColorItem(Color.FromArgb(Colors[i].A, Colors[i].R, Colors[i].G, Colors[i].B), Colors[i].ToString()));
            }

            return ColorSet;
        }

        private ObservableCollection<ColorItem> RGBSet()
        {
            ObservableCollection<ColorItem> ColorSet = new ObservableCollection<ColorItem>();

            ColorSet.Add(new ColorItem(Color.FromArgb(255, 85, 0, 0), "255,85,0,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 127, 0, 0), "255,127,0,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 169, 0, 0), "255,169,0,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 211, 0, 0), "255,211,0,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 253, 0, 0), "255,253,0,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 42, 42), "255,255,42,42"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 84, 84), "255,255,84,84"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 126, 126), "255,255,126,126"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 170, 170), "255,255,170,170"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 212, 212), "255,255,212,212"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 85, 34, 0), "255,85,34,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 127, 51, 0), "255,127,51,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 169, 67, 0), "255,169,67,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 211, 84, 0), "255,211,84,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 253, 101, 0), "255,253,101,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 127, 42), "255,255,127,42"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 152, 84), "255,255,152,84"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 178, 126), "255,255,178,126"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 204, 170), "255,255,204,170"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 229, 212), "255,255,229,212"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 85, 68, 0), "255,85,68,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 127, 102, 0), "255,127,102,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 169, 135, 0), "255,169,135,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 211, 169, 0), "255,211,169,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 253, 203, 0), "255,253,203,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 212, 42), "255,255,212,42"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 220, 84), "255,255,220,84"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 229, 126), "255,255,229,126"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 238, 170), "255,255,238,170"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 246, 212), "255,255,246,212"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 68, 85, 0), "255,68,85,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 102, 127, 0), "255,102,127,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 136, 169, 0), "255,136,169,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 169, 211, 0), "255,169,211,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 203, 253, 0), "255,203,253,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 212, 255, 42), "255,212,255,42"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 221, 255, 84), "255,221,255,84"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 229, 255, 126), "255,229,255,126"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 238, 255, 170), "255,238,255,170"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 246, 255, 212), "255,246,255,212"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 34, 85, 0), "255,34,85,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 51, 127, 0), "255,51,127,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 68, 169, 0), "255,68,169,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 84, 211, 0), "255,84,211,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 101, 253, 0), "255,101,253,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 127, 255, 42), "255,127,255,42"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 153, 255, 84), "255,153,255,84"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 178, 255, 126), "255,178,255,126"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 204, 255, 170), "255,204,255,170"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 229, 255, 212), "255,229,255,212"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 85, 0), "255,0,85,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 127, 0), "255,0,127,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 169, 0), "255,0,169,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 211, 0), "255,0,211,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 253, 0), "255,0,253,0"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 43, 255, 42), "255,43,255,42"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 85, 255, 84), "255,85,255,84"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 127, 255, 126), "255,127,255,126"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 171, 255, 170), "255,171,255,170"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 212, 255, 212), "255,212,255,212"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 85, 34), "255,0,85,34"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 127, 51), "255,0,127,51"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 169, 67), "255,0,169,67"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 211, 84), "255,0,211,84"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 253, 101), "255,0,253,101"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 42, 255, 127), "255,42,255,127"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 84, 255, 152), "255,84,255,152"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 126, 255, 177), "255,126,255,177"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 170, 255, 204), "255,170,255,204"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 212, 255, 229), "255,212,255,229"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 85, 68), "255,0,85,68"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 127, 102), "255,0,127,102"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 169, 135), "255,0,169,135"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 211, 169), "255,0,211,169"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 253, 202), "255,0,253,202"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 42, 255, 212), "255,42,255,212"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 84, 255, 220), "255,84,255,220"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 126, 255, 229), "255,126,255,229"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 170, 255, 238), "255,170,255,238"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 212, 255, 246), "255,212,255,246"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 68, 85), "255,0,68,85"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 102, 127), "255,0,102,127"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 136, 169), "255,0,136,169"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 169, 211), "255,0,169,211"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 203, 253), "255,0,203,253"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 42, 212, 255), "255,42,212,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 84, 221, 255), "255,84,221,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 126, 229, 255), "255,126,229,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 170, 238, 255), "255,170,238,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 212, 246, 255), "255,212,246,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 34, 85), "255,0,34,85"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 51, 127), "255,0,51,127"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 68, 169), "255,0,68,169"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 84, 211), "255,0,84,211"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 101, 253), "255,0,101,253"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 42, 127, 255), "255,42,127,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 84, 153, 255), "255,84,153,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 126, 178, 255), "255,126,178,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 170, 204, 255), "255,170,204,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 212, 229, 255), "255,212,229,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 0, 85), "255,0,0,85"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 0, 127), "255,0,0,127"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 0, 169), "255,0,0,169"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 0, 211), "255,0,0,211"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 0, 0, 253), "255,0,0,253"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 42, 43, 255), "255,42,43,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 84, 85, 255), "255,84,85,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 126, 127, 255), "255,126,127,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 170, 171, 255), "255,170,171,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 212, 212, 255), "255,212,212,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 34, 0, 85), "255,34,0,85"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 51, 0, 127), "255,51,0,127"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 67, 0, 169), "255,67,0,169"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 84, 0, 211), "255,84,0,211"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 101, 0, 253), "255,101,0,253"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 127, 42, 255), "255,127,42,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 152, 84, 255), "255,152,84,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 177, 126, 255), "255,177,126,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 204, 170, 255), "255,204,170,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 229, 212, 255), "255,229,212,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 68, 0, 85), "255,68,0,85"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 102, 0, 127), "255,102,0,127"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 135, 0, 169), "255,135,0,169"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 169, 0, 211), "255,169,0,211"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 202, 0, 253), "255,202,0,253"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 212, 42, 255), "255,212,42,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 220, 84, 255), "255,220,84,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 229, 126, 255), "255,229,126,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 238, 170, 255), "255,238,170,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 246, 212, 255), "255,246,212,255"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 85, 0, 68), "255,85,0,68"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 127, 0, 102), "255,127,0,102"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 169, 0, 136), "255,169,0,136"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 211, 0, 169), "255,211,0,169"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 253, 0, 203), "255,253,0,203"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 42, 212), "255,255,42,212"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 84, 221), "255,255,84,221"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 126, 229), "255,255,126,229"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 170, 238), "255,255,170,238"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 212, 246), "255,255,212,246"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 85, 0, 34), "255,85,0,34"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 127, 0, 51), "255,127,0,51"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 169, 0, 68), "255,169,0,68"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 211, 0, 85), "255,211,0,85"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 253, 0, 101), "255,253,0,101"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 42, 128), "255,255,42,128"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 84, 153), "255,255,84,153"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 126, 178), "255,255,126,178"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 170, 204), "255,255,170,204"));
            ColorSet.Add(new ColorItem(Color.FromArgb(255, 255, 212, 229), "255,255,212,229"));

            return ColorSet;
        }


    }

}
