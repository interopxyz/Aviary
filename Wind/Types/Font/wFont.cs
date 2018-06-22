
using System;
using System.Windows.Media;

namespace Wind.Types
{

    public class wFont: wFontBase
    {
        public enum hALign {Left, Center, Right };
        public enum vALign { Top, Middle, Bottom};

        public hALign HorizontalAlignment = hALign.Left;
        public vALign VerticalAlignment = vALign.Top;

        public wFont()
        {
        }
        public wFont(string FontTitle)
        {
            Title = FontTitle;
        }

        public wFont(string FontName, double FontSize)
        {
            Name = FontName;
            Size = FontSize;
        }
        public wFont(string FontTitle, string FontName, double FontSize)
        {
            Title = FontTitle;
            Name = FontName;
            Size = FontSize;
        }

        public wFont(string FontName, double FontSize, wColor color)
        {
            Name = FontName;
            Size = FontSize;

            FontColor = color;
        }

        public wFont(string FontTitle, string FontName, double FontSize, wColor color)
        {
            Title = FontTitle;
            Name = FontName;
            Size = FontSize;

            FontColor = color;
        }

        public wFont(string FontName, double FontSize, wColor color, Justification FontJustification)
        {
            Name = FontName;
            Size = FontSize;

            FontColor = color;

            Justify = FontJustification;
            SetJustification();
        }

        public wFont(string FontName, double FontSize, wColor color, bool isBold)
        {
            Name = FontName;
            Size = FontSize;

            FontColor = color;

            IsBold = isBold;
            SetJustification();
        }

        public wFont(string FontTitle, string FontName, double FontSize, wColor color, bool isBold)
        {
            Title = FontTitle;
            Name = FontName;
            Size = FontSize;

            FontColor = color;

            IsBold = isBold;
            SetJustification();
        }


        public wFont(string FontName, double FontSize, wColor color, Justification FontJustification, bool isBold, bool isItalic, bool isUnderlined, bool isStrikethrough)
        {
            Name = FontName;
            Size = FontSize;

            FontColor = color;

            Justify = FontJustification;
            SetJustification();

            IsBold = isBold;
            IsItalic = isItalic;
            IsUnderlined = isUnderlined;
            IsStrikethrough = isStrikethrough;
        }

        public wFont(string FontTitle, string FontName, double FontSize, wColor color, Justification FontJustification, bool isBold, bool isItalic, bool isUnderlined, bool isStrikethrough)
        {
            Title = FontTitle;
            Name = FontName;
            Size = FontSize;

            FontColor = color;

            Justify = FontJustification;
            SetJustification();

            IsBold = isBold;
            IsItalic = isItalic;
            IsUnderlined = isUnderlined;
            IsStrikethrough = isStrikethrough;
        }

        public wFont(wFontMedia MediaFont)
        {
            Name = MediaFont.Name;
            Size = MediaFont.Size;

            Justify = (Justification)MediaFont.Justify;
            SetJustification();

            FontColor = MediaFont.FontColor;

            IsBold = MediaFont.IsBold;
            IsItalic = MediaFont.IsItalic;
            IsUnderlined = MediaFont.IsUnderlined;
            IsStrikethrough = MediaFont.IsStrikethrough;
        }

        public wFont(wFontDrawing DrawingFont)
        {
            Name = DrawingFont.Name;
            Size = DrawingFont.Size;

            Justify = (Justification)DrawingFont.Justify;
            SetJustification();

            FontColor = DrawingFont.FontColor;

            IsBold = DrawingFont.IsBold;
            IsItalic = DrawingFont.IsItalic;
            IsUnderlined = DrawingFont.IsUnderlined;
            IsStrikethrough = DrawingFont.IsStrikethrough;
        }

        public void SetJustification()
        {
            HorizontalAlignment = (hALign)(int)(((int)Justify + 3) % 3);
            VerticalAlignment = (vALign)(Math.Ceiling((((int)Justify + 1) / 3.0)) - 1);
        }

        public SolidColorBrush GetFontBrush()
        {
            return new SolidColorBrush(FontColor.ToMediaColor());
        }

        public wFontMedia ToMediaFont()
        {
            return new wFontMedia(Name, Size, FontColor, Justify, IsBold, IsItalic, IsUnderlined, IsStrikethrough);
        }

        public wFontDrawing ToDrawingFont()
        {
            return new wFontDrawing(Name, Size, FontColor, Justify, IsBold, IsItalic, IsUnderlined, IsStrikethrough);
        }

    }

}
