using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Wind.Types
{
    public class wFontMedia
    {
        public string Name = "Arial";
        public double Size = 8;
        public int Justify = 0;

        public bool IsBold = false;
        public bool IsItalic = false;
        public bool IsUnderlined = false;
        public bool IsStrikethrough = false;

        public wColor FontColor = new wColor().Black();

        public FontFamily Family = new FontFamily("Arial");
        public TextDecorationCollection Style = new TextDecorationCollection();
        public FontWeight Bold = FontWeights.Normal;
        public FontStyle Italic = FontStyles.Normal;

        public HorizontalAlignment HAlign = HorizontalAlignment.Left;
        public VerticalAlignment VAlign = VerticalAlignment.Center;
        public TextAlignment TextAlign = TextAlignment.Left;

        public wFontMedia()
        {
            updateStyle();
        }

        public wFontMedia(string FontName)
        {
            Name = FontName;
            Family = new FontFamily(FontName);
            updateStyle();
        }

        public wFontMedia(string FontName, double FontSize)
        {
            Name = FontName;
            Family = new FontFamily(FontName);
            Size = FontSize;
            updateStyle();
        }

        public wFontMedia(string FontName, double FontSize, wColor color)
        {
            Name = FontName;
            Family = new FontFamily(FontName);
            Size = FontSize;

            FontColor = color;
            updateStyle();
        }

        public wFontMedia(string FontName, double FontSize, wColor color, int Justification)
        {
            Name = FontName;
            Family = new FontFamily(FontName);
            Size = FontSize;

            FontColor = color;
            Justify = Justification;

            HAlign = MediaHjust(Justification);
            VAlign = MediaVJust(Justification);
            updateStyle();
        }

        public wFontMedia(string FontName, double FontSize, wColor color, int Justification, bool isBold, bool isItalic, bool isUnderlined, bool isStrikethrough)
        {
            Name = FontName;
            Family = new FontFamily(FontName);
            Size = FontSize;

            FontColor = color;
            Justify = Justification;

            IsBold = isBold;
            IsItalic = isItalic;
            IsUnderlined = isUnderlined;
            IsStrikethrough = isStrikethrough;

            HAlign = MediaHjust(Justification);
            VAlign = MediaVJust(Justification);
            updateStyle();
        }

        public void updateStyle()
        {
            if (IsBold) { Bold = FontWeights.Bold; }
            if (IsItalic) { Italic = FontStyles.Italic; }
            if (IsUnderlined) { Style.Add(TextDecorations.Underline); }
            if (IsStrikethrough) { Style.Add(TextDecorations.Strikethrough); }
        }

        private System.Windows.HorizontalAlignment MediaHjust(int jst)
        {

            switch (jst % 3)
            {
                case 1:
                case 4:
                case 7:
                    return System.Windows.HorizontalAlignment.Center;
                case 2:
                case 5:
                case 8:
                    return System.Windows.HorizontalAlignment.Right;
                default:
                    return System.Windows.HorizontalAlignment.Left;
            }
        }

        private System.Windows.VerticalAlignment MediaVJust(int jst)
        {

            if (jst < 3)
            {
                return System.Windows.VerticalAlignment.Top;
            }
            else if (jst > 5)
            {
                return System.Windows.VerticalAlignment.Bottom;
            }
            else
            {
                return System.Windows.VerticalAlignment.Center;
            }
        }

    }
}
