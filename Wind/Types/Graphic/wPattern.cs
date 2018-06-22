using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Wind.Containers;
using Wind.Geometry.Curves;
using Wind.Geometry.Curves.Primitives;

namespace Wind.Types
{
    public class wPattern
    {

        wShapeCollection Shp = new wShapeCollection();

        public double X = 0;
        public double Y = 0;

        public double W = 100;
        public double H = 100;

        public int SpacingMode = 1;
        public int StrokeMode = 0;
        public int PatternMode = 0;
        public int TypeMode = 0;

        public wPattern()
        {
        }

        public wPattern(double OriginX, double OriginY, double CropWidth, double CropHeight)
        {
            X = OriginX;
            Y = OriginY;

            W = CropWidth;
            H = CropHeight;

            Shp.X = X;
            Shp.Y = Y;

            Shp.Width = W;
            Shp.Height = H;
        }

        public wPattern(double OriginX, double OriginY, double CropWidth, double CropHeight, wShapeCollection ShapeGraphic)
        {
            X = OriginX;
            Y = OriginY;

            W = CropWidth;
            H = CropHeight;

            Shp = ShapeGraphic;

            Shp.X = X;
            Shp.Y = Y;

            Shp.Width = W;
            Shp.Height = H;
        }

        public wShapeCollection SetPattern(int FillPatternMode, int PenStrokeMode, int SpaceMode)
        {
            PatternMode = FillPatternMode;
            SpacingMode = SpaceMode;
            TypeMode = PenStrokeMode;
            
            switch (PatternMode)
            {

                default: // Grid
                    switch (TypeMode)
                    {
                        default: // SquareGrid
                            HorizontalGrid();
                            break;
                        case 1: // Diamond
                            DiagonalGrid();
                            break;
                        case 2: // Triangular
                            HorizontalGrid();
                            break;
                        case 3: // Hexagonal
                            HorizontalGrid();
                            break;
                        case 4: // Grids | Stagger
                            HorizontalGrid();
                            break;
                        case 5: // Grids | Checker
                            HorizontalGrid();
                            break;
                        case 6: // Grids | Solid Diamond
                            HorizontalGrid();
                            break;
                        case 7: // Grids | Trellis
                            HorizontalGrid();
                            break;
                        case 8: // Grids | Dots
                            HorizontalGrid();
                            break;
                    }
                    break;

                case 1: // Horizontal
                    HorizontalStripes();
                    break;

                case 2: // Vertical 
                    VerticalStripes();
                    break;

                case 3: // Diagonal Left
                    DiagonalLeftStripes();
                    break;

                case 4: // Diagonal Right
                    DiagonalRightStripes();
                    break;

                case 5: // Percent
                    switch (PatternMode)
                    {
                        default: // 10%
                            HorizontalGrid();
                            break;
                        case 1: // 20%
                            HorizontalGrid();
                            break;
                        case 2: // 30%
                            HorizontalGrid();
                            break;
                        case 3: // 40%
                            HorizontalGrid();
                            break;
                        case 4: // 50%
                            HorizontalGrid();
                            break;
                        case 5: // 60%
                            HorizontalGrid();
                            break;
                        case 6: // 70%
                            HorizontalGrid();
                            break;
                        case 7: // 80%
                            HorizontalGrid();
                            break;
                        case 8: // 90%
                            HorizontalGrid();
                            break;
                    }
                    break;

                case 6: // Pattern
                    switch (PatternMode)
                    {
                        default: // Zig Zag
                            HorizontalGrid();
                            break;
                        case 1: // Confetti
                            HorizontalGrid();
                            break;
                        case 2: // Tile
                            HorizontalGrid();
                            break;
                        case 3: // Bamboo
                            HorizontalGrid();
                            break;
                        case 4: // Cross
                            HorizontalGrid();
                            break;
                        case 5: // Scatter
                            HorizontalGrid();
                            break;
                        case 6: // Star
                            HorizontalGrid();
                            break;
                        case 7: // Pinwheel
                            HorizontalGrid();
                            break;
                        case 8: // Rings
                            HorizontalGrid();
                            break;
                        case 9: // Weave
                            HorizontalGrid();
                            break;
                    }

                    break;

                case 7: // Architectural
                    switch (PatternMode)
                    {
                        default: // Steel
                            HorizontalGrid();
                            break;
                        case 1: // Aluminum
                            HorizontalGrid();
                            break;
                        case 2: // Glass
                            HorizontalGrid();
                            break;
                        case 3: // Concrete
                            HorizontalGrid();
                            break;
                        case 4: // Stone
                            HorizontalGrid();
                            break;
                        case 5: // Tile
                            HorizontalGrid();
                            break;
                        case 6: // Wood
                            HorizontalGrid();
                            break;
                        case 7: // Parquet
                            HorizontalGrid();
                            break;
                        case 8: // Earth
                            HorizontalGrid();
                            break;
                        case 9: // Grass
                            HorizontalGrid();
                            break;
                    }
                    break;
            }

            return Shp;
        }

        public void SetStroke(int PenStrokeMode)
        {
            StrokeMode = PenStrokeMode;
            SetStrokeMode();
        }

        private void SetStrokeMode()
        {
            switch (StrokeMode)
            {
                default:
                    Shp.Graphics.StrokePattern = new double[] { 1.0, 0.0 };
                    break;
                case 1:
                    Shp.Graphics.StrokePattern = new double[] { 1 };
                    break;
                case 2:
                    Shp.Graphics.StrokePattern = new double[] { 3.0 };
                    break;
            }
        }

        // Pattern Definitions Below ++++++++++++++++++++++++++++++++

        public void HorizontalStripes()
        {
            int C = 5;
            int S = 2;

            switch (SpacingMode)
            {
                case 1: //Narrow
                    C = 9;
                    S = 1;
                    break;
                case 3: //Wide
                    C = 3;
                    S = 4;
                    break;
            }

            for (int i = 0; i < C; i++)
            {
                Shp.Shapes.Add(new wShape(QuickLine(X, Y + i * S, X + W, Y + i * S), Shp.Graphics));
            }
        }

        public void VerticalStripes()
        {
            switch (SpacingMode)
            {
                default: //Regular
                    for (int i = 0; i < 5; i++)
                    {
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 2, Y, X + i * 2, Y + H), Shp.Graphics));
                    }
                    break;
                case 1: //Narrow
                    for (int i = 0; i < 9; i++)
                    {
                        Shp.Shapes.Add(new wShape(QuickLine(X+i, Y, X + i, Y + H), Shp.Graphics));
                    }
                    break;
                case 3: //Wide
                    for (int i = 0; i < 3; i++)
                    {
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 4, Y, X + i * 4, Y + H), Shp.Graphics));
                    }
                    break;
            }
        }

        public void DiagonalLeftStripes()
        {
            switch (SpacingMode)
            {
                default: //Regular
                    for (int i = 0; i < 5; i++)
                    {
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 2, Y + 0, X + 0, Y + i * 2), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 2, Y + H, X + W, Y + i * 2), Shp.Graphics));
                    }
                    break;
                case 1: //Narrow
                    for (int i = 0; i < 9; i++)
                    {
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 1, Y + 0, X + 0, Y + i * 1), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 1, Y + H, X + W, Y + i * 1), Shp.Graphics));
                    }
                    break;
                case 3: //Wide
                    for (int i = 0; i < 3; i++)
                    {
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 4, Y + 0, X + 0, Y + i * 4), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 4, Y + H, X + W, Y + i * 4), Shp.Graphics));
                    }
                    break;
            }
        }

        public void DiagonalRightStripes()
        {
            switch (SpacingMode)
            {
                default: //Regular
                    for (int i = 0; i < 5; i++)
                    {
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 2, Y + H, X + 0, H - (Y + i * 2)), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 2, Y + 0, X + W, H - (Y + i * 2)), Shp.Graphics));
                    }
                    break;
                case 1: //Narrow
                    for (int i = 0; i < 9; i++)
                    {
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 1, Y + H, X + 0, H - (Y + i * 1)), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 1, Y + 0, X + W, H - (Y + i * 1)), Shp.Graphics));
                    }
                    break;
                case 3: //Wide
                    for (int i = 0; i < 3; i++)
                    {
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 4, Y + H, X + 0, H - (Y + i * 4)), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 4, Y + 0, X + W, H - (Y + i * 4)), Shp.Graphics));
                    }
                    break;
            }
        }

        public void HorizontalGrid()
        {
            switch (SpacingMode)
            {
                default: //Regular
                    for (int i = 0; i < 5; i++)
                    {
                        Shp.Shapes.Add(new wShape(QuickLine(X, Y + i * 2, X + W, Y + i * 2), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 2, Y , X + i * 2, Y + H), Shp.Graphics));
                    }
                    break;
                case 1: //Narrow
                    for (int i = 0; i < 9; i++)
                    { 
                        Shp.Shapes.Add(new wShape(QuickLine(X, Y + i, X + W, Y + i), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i, Y, X + i, Y + H), Shp.Graphics));
                    }
                    break;
                case 3: //Wide
                    for (int i = 0; i < 3; i++)
                    {
                        Shp.Shapes.Add(new wShape(QuickLine(X, Y + i*4, X + W, Y + i*4), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 4, Y, X + i * 4, Y + H), Shp.Graphics));
                    }
                    break;
            }
        }

        public void SquareCheckerGrid()
        {
            switch (SpacingMode)
            {
                default: //Regular
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            Shp.Shapes.Add(new wShape(QuickRect(X + i * 2, Y + j * 2, 2, 2), Shp.Graphics));
                        }
                    }
                    break;
                case 1: //Narrow
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            Shp.Shapes.Add(new wShape(QuickRect(X + i, Y + j, 1, 1), Shp.Graphics));
                        }
                    }
                    break;
                case 3: //Wide
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            Shp.Shapes.Add(new wShape(QuickRect(X + i * 4, Y + j * 4, 4, 4), Shp.Graphics));
                        }
                    }
                    break;
            }
        }

        public void DiagonalGrid()
        {
            switch (SpacingMode)
            {
                default: //Regular
                    for (int i = 0; i < 5; i++)
                    {
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 2, Y + H, X + 0, H - (Y + i * 2)), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 2, Y + 0, X + W, H - (Y + i * 2)), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 2, Y + 0, X + 0, Y + i * 2), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 2, Y + H, X + W, Y + i * 2), Shp.Graphics));
                    }
                    break;
                case 1: //Narrow
                    for (int i = 0; i < 9; i++)
                    {
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 1, Y + H, X + 0, H - (Y + i * 1)), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 1, Y + 0, X + W, H - (Y + i * 1)), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 1, Y + 0, X + 0, Y + i * 1), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 1, Y + H, X + W, Y + i * 1), Shp.Graphics));
                    }
                    break;
                case 3: //Wide
                    for (int i = 0; i < 3; i++)
                    {
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 4, Y + H, X + 0, H - (Y + i * 4)), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 4, Y + 0, X + W, H - (Y + i * 4)), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 4, Y + 0, X + 0, Y + i * 4), Shp.Graphics));
                        Shp.Shapes.Add(new wShape(QuickLine(X + i * 4, Y + H, X + W, Y + i * 4), Shp.Graphics));
                    }
                    break;
            }
        }

        public RectangleGeometry QuickRect(double XCorner, double YCorner, double RectWidth, double RectHeight)
        {
            return new RectangleGeometry(new Rect(XCorner,YCorner,RectWidth,RectHeight));
        }

        public LineGeometry QuickLine(double StartX, double StartY, double EndX, double EndY)
        {
            return new LineGeometry(new Point(StartX, StartY), new Point(EndX, EndY));
        }

    }
}
