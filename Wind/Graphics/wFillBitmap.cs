using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wind.Graphics
{
    public class wFillBitmap : wFill
    {
        public Bitmap BaseBitmap = new Bitmap(100, 100);
        public ImageBrush ImgBrsh = new ImageBrush();

        public int Alignment = 0;
        public int FillMethod = 2;
        public double Angle = 0;
        public double Scale = 1;

        public AlignmentX hAlign = AlignmentX.Center;
        public AlignmentY vAlign = AlignmentY.Center;
        public TileMode Tiling = TileMode.Tile;

        public wFillBitmap()
        {
            BuildBrush();
        }

        public wFillBitmap(Bitmap FillBitmapObject)
        {
            BaseBitmap = FillBitmapObject;
            BuildBrush();
        }

        public wFillBitmap(Bitmap FillBitmapObject,int BitmapAlignment, int BitmapFillMethod)
        {
            Alignment = BitmapAlignment;
            FillMethod = BitmapFillMethod;

            BaseBitmap = FillBitmapObject;
            BuildBrush();
        }

        public wFillBitmap(Bitmap FillBitmapObject, int BitmapAlignment, int BitmapFillMethod, int TilingMode)
        {
            Alignment = BitmapAlignment;
            FillMethod = BitmapFillMethod;
            Tiling = (TileMode)TilingMode;

            BaseBitmap = FillBitmapObject;
            BuildBrush();
        }

        public wFillBitmap(Bitmap FillBitmapObject, int BitmapAlignment, int BitmapFillMethod, int TilingMode, double RotationAngle)
        {
            Alignment = BitmapAlignment;
            FillMethod = BitmapFillMethod;
            Tiling = (TileMode)TilingMode;
            Angle = RotationAngle;

            BaseBitmap = FillBitmapObject;
            BuildBrush();
        }

        public wFillBitmap(Bitmap FillBitmapObject, int BitmapAlignment, int BitmapFillMethod, int TilingMode, double RotationAngle, double ScaleFactor)
        {
            Alignment = BitmapAlignment;
            FillMethod = BitmapFillMethod;
            Tiling = (TileMode)TilingMode;
            Angle = RotationAngle;
            Scale = ScaleFactor;

            BaseBitmap = FillBitmapObject;
            BuildBrush();
        }
        
        public void BuildBrush()
        {
            SetJustification();

            ImgBrsh.ImageSource = ImageSourceForBitmap(BaseBitmap);
            ImgBrsh.Stretch = StretchMode(FillMethod);
            ImgBrsh.AlignmentX = hAlign;
            ImgBrsh.AlignmentY = vAlign;
            ImgBrsh.TileMode = Tiling;

            if ((FillMethod > 0)&((int)Tiling > 0))
            {
                ImgBrsh.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
                ImgBrsh.Viewport = new Rect(0, 0, 1, 1);
            }
            else
            {
                ImgBrsh.ViewportUnits = BrushMappingMode.Absolute;
                ImgBrsh.Viewport = new Rect(0, 0, BaseBitmap.Width * Scale, BaseBitmap.Height * Scale);
            }

            ImgBrsh.ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;
            ImgBrsh.Viewbox = new Rect(0, 0, 1, 1);
            ImgBrsh.Transform = new RotateTransform(Angle);
            FillBrush = ImgBrsh;
        }

        public Stretch StretchMode(int Mode)
        {
            Stretch ModeStretch = Stretch.Fill;

            switch(Mode)
            {
                case 0:
                    ModeStretch = Stretch.Uniform;
                    break;
                case 1:
                    ModeStretch = Stretch.UniformToFill;
                    break;
                default:
                    ModeStretch = Stretch.Fill;
                    break;
            }

            return ModeStretch;

        }

        public void SetJustification()
        {
            hAlign = (AlignmentX)(int)((Alignment + 3) % 3);
            vAlign = (AlignmentY)(Math.Ceiling(((Alignment + 1) / 3.0)) - 1);
        }

        public ImageSource ImageSourceForBitmap(Bitmap bmp)
        {
                return Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }


    }
}
