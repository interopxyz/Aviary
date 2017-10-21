
namespace Wind.Types
{
    public class wFont
    {
        public string Name = "Arial";
        public double Size = 8;
        public int Justify = 0;
        public bool IsBold = false;
        public bool IsItalic = false;
        public bool IsUnderlined = false;
        public bool IsStrikethrough = false;
        public wColor FontColor = new wColor().Black();

        public wFont()
        {
        }

        public wFont(string FontName, double FontSize)
        {
            Name = FontName;
            Size = FontSize;
        }

        public wFont(string FontName, double FontSize, wColor color)
        {
            Name = FontName;
            Size = FontSize;

            FontColor = color;
        }

        public wFont(string FontName, double FontSize, wColor color, int Justification)
        {
            Name = FontName;
            Size = FontSize;

            FontColor = color;
            Justify = Justification;
        }


        public wFont(string FontName, double FontSize, wColor color, int Justification, bool isBold, bool isItalic, bool isUnderlined, bool isStrikethrough)
        {
            Name = FontName;
            Size = FontSize;

            FontColor = color;
            Justify = Justification;

            IsBold = isBold;
            IsItalic = isItalic;
            IsUnderlined = isUnderlined;
            IsStrikethrough = isStrikethrough;
        }

        public wFont(wFontMedia MediaFont)
        {
            Name = MediaFont.Name;
            Size = MediaFont.Size;

            Justify = MediaFont.Justify;
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

            Justify = DrawingFont.Justify;
            FontColor = DrawingFont.FontColor;

            IsBold = DrawingFont.IsBold;
            IsItalic = DrawingFont.IsItalic;
            IsUnderlined = DrawingFont.IsUnderlined;
            IsStrikethrough = DrawingFont.IsStrikethrough;
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
