using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoundInTheory.DynamicImage;
using System.Drawing;
using SoundInTheory.DynamicImage.Filters;
using SoundInTheory.DynamicImage.Sources;
using SoundInTheory.DynamicImage.Layers;
using Macaw.Compiling.Modifiers;
using Macaw.Utilities;
using Wind.Types;
using Wind.Presets;

namespace Macaw.Compiling
{
    public class mLayer
    {

        public string Type = "Layer";
        public ImageLayer CompositionLayer = new ImageLayer();

        public BlendMode Blend = BlendMode.Normal;

        public Bitmap LayerImage = new Bitmap(10, 10);

        public Bitmap MaskImage = new Bitmap(10, 10);
        public wColor MaskColor = wColors.Transparent;
        public double Opacity = 100.00;
        public double MaskSample = 1;

        public bool[] StandardModifiers = { false, false, false, false, false,false };
        public int[] Xform = { 0, 0, 0,0,0,0,0 };
        public List<mModifier> Modifiers = new List<mModifier>();

        public mLayer()
        {

        }

        public mLayer(mLayer tempLayer)
        {
            LayerImage = tempLayer.LayerImage;
            Blend = tempLayer.Blend;

            StandardModifiers = tempLayer.StandardModifiers;
            Modifiers = tempLayer.Modifiers;
            
            MaskImage  = (Bitmap)tempLayer.MaskImage.Clone();
            MaskColor  = tempLayer.MaskColor;
            Opacity    = tempLayer.Opacity;
            MaskSample = tempLayer.MaskSample;

            Xform = tempLayer.Xform;
        }


        public virtual void ApplyBitmap()
        {
            Bitmap tempBitmap = (Bitmap)LayerImage.Clone();
            ImageImageSource imgSource = new ImageImageSource();
            imgSource.Image = new mConvert(tempBitmap).BitmapToWritableBitmap();

            CompositionLayer.Source = imgSource;
        }

        public virtual void ClearModifiers()
        {
            Modifiers.Clear();
        }

        public virtual void AddModifiers(List<mModifier> Modifiers)
        {
            foreach (mModifier Modifier in Modifiers)
            {
                Modifiers.Add(Modifier);
            }
        }

        public virtual void ApplyFilters()
        {
            for (int i = 0; i < Modifiers.Count; i++)
            {
                CompositionLayer.Filters.Add(Modifiers[i].filter);
            }
        }

        public virtual void ApplyStandardFilters()
        {
            CompositionLayer.BlendMode = Blend;

            if (StandardModifiers[0])
            {
                ColorKeyFilter ColorMask = new ColorKeyFilter();
                ColorMask.Color = new mImageColor(MaskColor).ToDynamicColor();
                ColorMask.ColorTolerance = (byte)MaskSample;

                CompositionLayer.Filters.Add(ColorMask);
            }

            if (StandardModifiers[1])
            {
                ClippingMaskFilter ImageMask = new ClippingMaskFilter();
                ImageImageSource MaskLayerImage = new ImageImageSource();

                MaskLayerImage.Image = new mConvert((Bitmap)MaskImage.Clone()).BitmapToWritableBitmap();
                ImageMask.MaskImage = MaskLayerImage;
                
                CompositionLayer.Filters.Add(ImageMask);
            }

            if (StandardModifiers[5])
            {
                CompositionLayer.Filters.Add(new mModifyResize((mModifyResize.ResizingMode)Xform[5], (mModifyResize.ScalingMode)Xform[6],Xform[3],Xform[4]).filter);
            }

            if (StandardModifiers[2])
            {
                OpacityAdjustmentFilter OpacityMask = new OpacityAdjustmentFilter();
                OpacityMask.Opacity = (byte)Opacity;

                CompositionLayer.Filters.Add(OpacityMask);
            }

            if (StandardModifiers[3])
            {
                RotationFilter Rotation = new RotationFilter();
                Rotation.Angle = Xform[2];

                CompositionLayer.Filters.Add(Rotation);
            }

            if (StandardModifiers[4])
            {
                CompositionLayer.X = Xform[0];
                CompositionLayer.Y = Xform[1];
            }
        }

        public virtual void SetSizing(int Mode, int Type, int W, int H)
        {
            Xform[3] = W;
            Xform[4] = H;
            Xform[5] = Mode;
            Xform[6] = Type;
            StandardModifiers[5] = true;
        }

        public virtual void SetPosition(int X, int Y)
        {
            Xform[0] = X;
            Xform[1] = Y;
            StandardModifiers[4] = true;
        }

        public virtual void SetRotation(int Angle)
        {
            Xform[2] = Angle;
            StandardModifiers[3] = true;
        }

        public virtual void SetOpacity(double LayerOpacity)
        {
            Opacity = LayerOpacity;
            StandardModifiers[2] = true;
        }

        public virtual void SetImageMask(Bitmap LayerMaskBitmap)
        {
            MaskImage = (Bitmap)LayerMaskBitmap.Clone();
            StandardModifiers[1] = true;
        }

        public virtual void SetColorMask(wColor LayerMaskColor, double colorTolerance)
        {
            MaskColor = LayerMaskColor;
            MaskSample = colorTolerance;
            StandardModifiers[0] = true;
        }

    }
}
