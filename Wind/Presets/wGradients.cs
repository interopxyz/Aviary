using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Wind.Presets
{
    public static class wGradients
    {

        //Solid Colors
        public static wGradient SolidGray = new wGradient(new List<wColor> { wColors.Gray, wColors.Gray }, new List<double> { 0.0, 1.0 });
        public static wGradient SolidTransparent = new wGradient(new List<wColor> { wColors.Transparent, wColors.Transparent }, new List<double> { 0.0, 1.0 });
        public static wGradient SolidLightGray = new wGradient(new List<wColor> { wColors.VeryLightGray, wColors.VeryLightGray }, new List<double> { 0.0, 1.0 });
        public static wGradient SolidDarkGray = new wGradient(new List<wColor> { wColors.DarkGray, wColors.DarkGray }, new List<double> { 0.0, 1.0 });

        //Basic Gradients
        public static wGradient GrayScale = new wGradient(new List<wColor> { wColors.LightGray, wColors.DarkGray }, new List<double> { 0.0, 1.0 });

        //Colorset Gradients
        public static wGradient Metro = new wGradient
        {
            Name = "Metro",
            ColorSet = new List<wColor>
            {
                new wColor(60, 162, 222),
                new wColor(52, 140, 191),
                new wColor(75, 163, 153),
                new wColor(84, 196, 163),
                new wColor(247, 211, 104),
                new wColor(250, 160, 95),
                new wColor(204, 81, 65),
                new wColor(235, 93, 80)
            },

            ParameterSet = new List<double> { 0.0, 0.142857, 0.285714, 0.428571, 0.571429, 0.714286, 0.857143, 1.0 }
        };

        public static wGradient BlueRedYellow = new wGradient
        {
            Name = "BlueRedYellow",
            ColorSet = new List<wColor>
            {
                wColors.Blue,
                wColors.Red,
                wColors.Yellow
            },
            ParameterSet = new List<double> { 0.0, 0.5, 1.0 }
        };

        public static wGradient Vidris = new wGradient
        {
            Name = "Vidris",
            ColorSet = new List<wColor>
            {
                new wColor(68,1,84),
                new wColor(72,36,117),
                new wColor(65,68,135),
                new wColor(53,95,141),
                new wColor(42,120,142),
                new wColor(33,145,140),
                new wColor(34,168,132),
                new wColor(68,191,112),
                new wColor(122,209,81),
                new wColor(189,223,38),
                new wColor(253,231,37)
            },

            ParameterSet = new List<double> { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0 }
        };

        public static wGradient Inferno = new wGradient
        {
            Name = "Inferno",
            ColorSet = new List<wColor>
            {
                new wColor(0,0,4),
                new wColor(22,11,57),
                new wColor(66,10,104),
                new wColor(106,23,110),
                new wColor(147,38,103),
                new wColor(188,55,84),
                new wColor(221,81,58),
                new wColor(243,120,25),
                new wColor(252,165,10),
                new wColor(246,215,70),
                new wColor(252,255,164)
            },

            ParameterSet = new List<double> { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0 }
        };

        public static wGradient Magma = new wGradient
        {
            Name = "Magma",
            ColorSet = new List<wColor>
            {
                new wColor(0,0,4),
                new wColor(20,14,54),
                new wColor(59,15,112),
                new wColor(100,26,128),
                new wColor(140,41,129),
                new wColor(183,55,121),
                new wColor(222,73,104),
                new wColor(247,112,92),
                new wColor(254,159,109),
                new wColor(254,207,146),
                new wColor(252,253,191)
            },

            ParameterSet = new List<double> { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0 }
        };

        public static wGradient Plasma = new wGradient
        {
            Name = "Plasma",
            ColorSet = new List<wColor>
            {
                new wColor(13,8,135),
                new wColor(65,4,157),
                new wColor(106,0,168),
                new wColor(143,13,164),
                new wColor(177,42,144),
                new wColor(204,71,120),
                new wColor(225,100,98),
                new wColor(242,132,75),
                new wColor(252,166,54),
                new wColor(252,206,37),
                new wColor(240,249,33)
            },

            ParameterSet = new List<double> { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0 }
        };

        public static wGradient Cyclical = new wGradient
        {
            Name = "Cyclical",
            ColorSet = new List<wColor>
            {
                new wColor(110,64,170),
                new wColor(210,62,167),
                new wColor(255,94,99),
                new wColor(239,167,47),
                new wColor(175,240,91),
                new wColor(64,243,115),
                new wColor(26,200,193),
                new wColor(65,126,224),
                new wColor(110,64,171)
            },

            ParameterSet = new List<double> { 0.0, 0.125, 0.25, 0.375, 0.5, 0.625, 0.75, 0.875, 1.0 }
        };

        public static wGradient Spectral = new wGradient
        {
            Name = "Spectral",
            ColorSet = new List<wColor>
            {
                new wColor(158,1,66),
                new wColor(209,60,75),
                new wColor(240,112,74),
                new wColor(252,172,99),
                new wColor(254,221,141),
                new wColor(251,248,176),
                new wColor(224,243,161),
                new wColor(169,221,162),
                new wColor(105,189,169),
                new wColor(66,136,181),
                new wColor(94,79,162)
            },

            ParameterSet = new List<double> { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0 }
        };

        public static wGradient Cool = new wGradient
        {
            Name = "Cool",
            ColorSet = new List<wColor>
            {
                new wColor(110,64,170),
                new wColor(92,90,206),
                new wColor(65,125,224),
                new wColor(39,163,220),
                new wColor(26,199,194),
                new wColor(32,227,155),
                new wColor(63,243,116),
                new wColor(114,246,90),
                new wColor(175,240,90)
            },

            ParameterSet = new List<double> { 0.0, 0.125, 0.25, 0.375, 0.5, 0.625, 0.75, 0.875, 1.0 }
        };

        public static wGradient Warm = new wGradient
        {
            Name = "Warm",
            ColorSet = new List<wColor>
            {
                new wColor(110,64,170),
                new wColor(160,61,179),
                new wColor(210,62,167),
                new wColor(249,72,138),
                new wColor(255,94,99),
                new wColor(255,127,65),
                new wColor(239,166,47),
                new wColor(205,207,55),
                new wColor(175,240,90)
            },

            ParameterSet = new List<double> { 0.0, 0.125, 0.25, 0.375, 0.5, 0.625, 0.75, 0.875, 1.0 }
        };

        public static wGradient Jet = new wGradient
        {
            Name = "Jet",
            ColorSet = new List<wColor>
            {
                new wColor(37,16,158),
                new wColor(57,51,254),
                new wColor(0,199,255),
                new wColor(98,252,174),
                new wColor(250,255,18),
                new wColor(255,134,0),
                new wColor(218,44,0),
                new wColor(146,29,0)
            },

            ParameterSet = new List<double> { 0.0, 0.142857, 0.285714, 0.428571, 0.571429, 0.714286, 0.857143, 1.0 }
        };

        public static wGradient PurpleOrange = new wGradient
        {
            Name = "PurpleOrange",
            ColorSet = new List<wColor>
            {
                new wColor(45,0,75),
                new wColor(85,45,132),
                new wColor(129,112,172),
                new wColor(176,170,208),
                new wColor(215,215,233),
                new wColor(243,238,234),
                new wColor(253,221,179),
                new wColor(248,182,100),
                new wColor(221,132,31),
                new wColor(178,90,9),
                new wColor(127,59,8)
            },

            ParameterSet = new List<double> { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0 }
        };

        public static wGradient RedYellowGreen = new wGradient
        {
            Name = "RedYellowGreen",
            ColorSet = new List<wColor>
            {
                new wColor(165,0,38),
                new wColor(212,50,44),
                new wColor(241,110,67),
                new wColor(252,172,99),
                new wColor(254,221,141),
                new wColor(249,247,174),
                new wColor(215,238,142),
                new wColor(164,216,110),
                new wColor(100,188,97),
                new wColor(34,150,79),
                new wColor(0,104,55)
            },

            ParameterSet = new List<double> { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0 }
        };

        public static wGradient RedYellowBlue = new wGradient
        {
            Name = "RedYellowBlue",
            ColorSet = new List<wColor>
            {
                new wColor(165,0,38),
                new wColor(212,50,44),
                new wColor(241,110,67),
                new wColor(252,172,100),
                new wColor(254,221,144),
                new wColor(250,248,193),
                new wColor(220,241,236),
                new wColor(171,214,232),
                new wColor(117,171,208),
                new wColor(74,116,180),
                new wColor(49,54,149)
            },

            ParameterSet = new List<double> { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0 }
        };

        public static wGradient PinkYellowGreen = new wGradient
        {
            Name = "PinkYellowGreen",
            ColorSet = new List<wColor>
            {
                new wColor(142,1,82),
                new wColor(192,38,126),
                new wColor(221,114,173),
                new wColor(240,179,214),
                new wColor(250,221,237),
                new wColor(245,243,239),
                new wColor(225,242,202),
                new wColor(182,222,135),
                new wColor(128,187,71),
                new wColor(79,145,37),
                new wColor(39,100,25)
            },

            ParameterSet = new List<double> { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0 }
        };

    }
}