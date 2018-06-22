using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Editing.Resizing
{
    public class mCropCanvas : mFilters
    {
        
        
        public mCropCanvas(int ModeX,int ModeY, int CropWidth, int CropHeight, int ImageWidth, int ImageHeight,System.Drawing.Color FillColor)
        {
            Sequence.Clear();

            BitmapType = mFilter.BitmapTypes.Rgb24bpp;

            int IW = ImageWidth;
            int IH = ImageHeight;
            int CW = CropWidth;
            int CH = CropHeight;

            int FX = 0;
            int FY = 0;
            int FW = CW;
            int FH = CH;

            if (IW>CW)
            {
                switch (ModeX)
                {
                    case 0:
                        Sequence.Add(new Crop(new System.Drawing.Rectangle(0,0,CW,IH)));
                        break;
                    case 1:
                        Sequence.Add(new Crop(new System.Drawing.Rectangle((IW - CW) / 2, 0, CW, IH)));
                        break;
                    case 2:
                        Sequence.Add(new Crop(new System.Drawing.Rectangle((IW - CW), 0, CW, IH)));
                        break;
                }
            }
            else
            {
                switch (ModeX)
                {
                    case 0:
                        Sequence.Add(new Crop(new System.Drawing.Rectangle(0, 0, CW, IH)));
                        FW = CW - (CW - IW);
                        break;
                    case 1:
                        Sequence.Add(new Crop(new System.Drawing.Rectangle(-(CW - IW) / 2, 0, CW, IH)));
                        FX = (CW - IW) / 2;
                        FW = CW-(CW - IW);
                        break;
                    case 2:
                        Sequence.Add(new Crop(new System.Drawing.Rectangle(-(CW - IW), 0, CW, IH)));
                        FX = (CW - IW);
                        break;
                }
            }

            if (IH > CH)
            {
                switch (ModeY)
                {
                    case 0:
                        Sequence.Add(new Crop(new System.Drawing.Rectangle(0, 0, CW, CH)));
                        break;
                    case 1:
                        Sequence.Add(new Crop(new System.Drawing.Rectangle(0,(IH - CH) / 2, CW, CH)));
                        break;
                    case 2:
                        Sequence.Add(new Crop(new System.Drawing.Rectangle(0, (IH - CH), CW, CH)));
                        break;
                }
            }
            else
            {
                switch (ModeY)
                {
                    case 0:
                        Sequence.Add(new Crop(new System.Drawing.Rectangle(0, 0, CW, CH)));
                        FH = CH - (CH - IH);
                        break;
                    case 1:
                        Sequence.Add(new Crop(new System.Drawing.Rectangle(0, -(CH - IH) / 2, CW, CH)));
                        FY = (CH - IH) / 2;
                        FH = CH - (CH - IH);
                        break;
                    case 2:
                        Sequence.Add(new Crop(new System.Drawing.Rectangle(0, -(CH - IH), CW, CH)));
                        FY = (CH - IH);
                        break;
                }
            }
            
            Sequence.Add(new CanvasCrop(new System.Drawing.Rectangle(FX, FY, FW, FH), FillColor));

        }

    }
}