using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Windows;

namespace Wind.Types
{
    public class wFontDrawing
    {
        public string Name = "Arial";
        public double Size = 8;
        public int Justify = 0;

        public bool IsBold = false;
        public bool IsItalic = false;
        public bool IsUnderlined = false;
        public bool IsStrikethrough = false;

        public wColor FontColor = new wColor().Black();

        public Font FontObject = new System.Drawing.Font("Arial", 8);

        public System.Windows.Forms.HorizontalAlignment HAlign;
        public int VAlign;

        public void UpdateFont()
        {
            FontObject = new Font(new FontFamily(Name), (float)Size, drawingBold(IsBold) | drawingItalic(IsItalic) | drawingUnderline(IsUnderlined) | drawingStrikeout(IsStrikethrough));
        }

        public wFontDrawing()
        {
            UpdateFont();
        }

        public wFontDrawing(string FontName, double FontSize)
        {
            Name = FontName;
            Size = FontSize;

            UpdateFont();
        }

        public wFontDrawing(string FontName, double FontSize, wColor color)
        {
            Name = FontName;
            Size = FontSize;

            FontColor = color;

            UpdateFont();
        }

        public wFontDrawing(string FontName, double FontSize, wColor color, int Justification)
        {
            Name = FontName;
            Size = FontSize;

            FontColor = color;
            Justify = Justification;

            HAlign = textHjust(Justify);
            VAlign = textVjust(Justify);

            UpdateFont();
        }


        public wFontDrawing(string FontName, double FontSize, wColor color, int Justification, bool isBold, bool isItalic, bool isUnderlined, bool isStrikethrough)
        {
            Name = FontName;
            Size = FontSize;

            FontColor = color;
            Justify = Justification;

            IsBold = isBold;
            IsItalic = isItalic;
            IsUnderlined = isUnderlined;
            IsStrikethrough = isStrikethrough;

            HAlign = textHjust(Justify);
            VAlign = textVjust(Justify);

            UpdateFont();
        }

        private System.Windows.Forms.HorizontalAlignment textHjust(int jst)
        {

            switch (jst % 3)
            {
                case 1:
                case 4:
                case 7:
                    return System.Windows.Forms.HorizontalAlignment.Center;
                case 2:
                case 5:
                case 8:
                    return System.Windows.Forms.HorizontalAlignment.Right;
                default:
                    return System.Windows.Forms.HorizontalAlignment.Left;
            }
        }

        private int textVjust(int jst)
        {

            if (jst < 3)
            {
                return 0;
            }
            else if (jst > 5)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }

        // Toggle Bold font style
        private System.Drawing.FontStyle drawingBold(bool bln)
        {
            if (bln) { return System.Drawing.FontStyle.Bold; } else { return System.Drawing.FontStyle.Regular; }
        }
        // Toggle Italic font style
        private System.Drawing.FontStyle drawingItalic(bool bln)
        {
            if (bln) { return System.Drawing.FontStyle.Italic; } else { return System.Drawing.FontStyle.Regular; }
        }
        // Toggle Underline font style
        private System.Drawing.FontStyle drawingUnderline(bool bln)
        {
            if (bln) { return System.Drawing.FontStyle.Underline; } else { return System.Drawing.FontStyle.Regular; }
        }
        // Toggle Strikeout font style
        private System.Drawing.FontStyle drawingStrikeout(bool bln)
        {
            if (bln) { return System.Drawing.FontStyle.Strikeout; } else { return System.Drawing.FontStyle.Regular; }
        }

    }
}
