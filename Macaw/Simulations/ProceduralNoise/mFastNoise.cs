using Macaw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Wind.Types;


//Uses https://github.com/Auburns/FastNoise/wiki
namespace Macaw.ProceduralNoise
{
    public class mFastNoise
    {
        public enum NoiseModes {Value, Perlin, Simplex, WhiteNoise, Cubic };
        NoiseModes NoiseMode = NoiseModes.Value;

        public enum InterpolationModes {Linear, Hermite, Quintic, None};
        InterpolationModes InterpolationMode = InterpolationModes.None;

        public enum FractalModes {FBM, Billow, Rigid, None};
        FractalModes FractalMode = FractalModes.None;

        public int Width = 100;
        public int Height = 100;
        public int Depth = 0;

        public double Frequency = 0.02;

        public int Octaves = 5;
        public double Lacunarity = 2.0;
        public double Gain = 0.5;

        public double PerturbanceAmplitude = 30;
        public double PerturbanceFrequency = 0.01;

        public int Seed = 1;
        public List<double> Values = new List<double>();


        public FastNoise Noise = new FastNoise(1);

        public Bitmap OutputBitmap = null;

        public mFastNoise()
        {
            
        }
        
        public mFastNoise(int seed)
        {
            Seed = seed;

            Noise = new FastNoise(Seed);
            Noise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
        }

        public mFastNoise(int width, int height, int depth,int seed)
        {
            Width = width;
            Height = height;
            Depth = depth;

            Seed = seed;
            
            Noise = new FastNoise(Seed);
            Noise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
            BuildBitmap();
        }

        public void BuildBitmap()
        {
            Values.Clear();
            Bitmap bmp = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            bmp = new mConvert(new mConvert(bmp).BitmapToSource()).SourceToBitmap();
            
            if(FractalMode == FractalModes.None)
            { 
            switch (NoiseMode)
            {
                case NoiseModes.Cubic:
                    Noise.SetNoiseType(FastNoise.NoiseType.Cubic);
                    OutputBitmap = GetCubic(bmp);
                    break;
                case NoiseModes.Perlin:
                    Noise.SetNoiseType(FastNoise.NoiseType.Perlin);
                    OutputBitmap = GetPerlin(bmp);
                    break;
                case NoiseModes.Simplex:
                    Noise.SetNoiseType(FastNoise.NoiseType.Simplex);
                    OutputBitmap = GetSimplex(bmp);
                    break;
                case NoiseModes.Value:

                    Noise.SetNoiseType(FastNoise.NoiseType.Value);
                    OutputBitmap = GetValue(bmp);
                    break;
                case NoiseModes.WhiteNoise:

                    Noise.SetNoiseType(FastNoise.NoiseType.WhiteNoise);
                    OutputBitmap = GetWhiteNoise(bmp);
                    break;
            }
            }
            else
            {
                switch (NoiseMode)
                {
                    case NoiseModes.Cubic:

                        Noise.SetNoiseType(FastNoise.NoiseType.CubicFractal);
                        OutputBitmap = GetCubicFractal(bmp);
                        break;
                    case NoiseModes.Perlin:

                        Noise.SetNoiseType(FastNoise.NoiseType.PerlinFractal);
                        OutputBitmap = GetPerlinFractal(bmp);
                        break;
                    case NoiseModes.Simplex:

                        Noise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
                        OutputBitmap = GetSimplexFractal(bmp);
                        break;
                    case NoiseModes.Value:

                        Noise.SetNoiseType(FastNoise.NoiseType.ValueFractal);
                        OutputBitmap = GetValueFractal(bmp);
                        break;
                    case NoiseModes.WhiteNoise:

                        Noise.SetNoiseType(FastNoise.NoiseType.WhiteNoise);
                        OutputBitmap = GetWhiteNoise(bmp);
                        break;
                }
            }
        }

        private Bitmap GetCubic(Bitmap bmp)
        {
            int k = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = Noise.GetCubic(j, i, Depth);
                    Values.Add(Value);
                    int IntValue = (int)(255.0 * (1.0 + Value) / 2);

                    bmp.SetPixel(j, i, Color.FromArgb(IntValue, IntValue, IntValue));
                    k += 1;
                }
            }

            return bmp;
        }

        private Bitmap GetPerlin(Bitmap bmp)
        {
            int k = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = Noise.GetPerlin(j, i, Depth);
                    Values.Add(Value);
                    int IntValue = (int)(255.0 * (1.0 + Value) / 2);

                    bmp.SetPixel(j, i, Color.FromArgb(IntValue, IntValue, IntValue));
                    k += 1;
                }
            }

            return bmp;
        }

        private Bitmap GetSimplex(Bitmap bmp)
        {
            int k = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = Noise.GetSimplex(j, i, Depth);
                    Values.Add(Value);
                    int IntValue = (int)(255.0 * (1.0 + Value) / 2);

                    bmp.SetPixel(j, i, Color.FromArgb(IntValue, IntValue, IntValue));
                    k += 1;
                }
            }

            return bmp;
        }

        private Bitmap GetWhiteNoise(Bitmap bmp)
        {
            int k = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = Noise.GetWhiteNoise(j, i, Depth,Frequency);
                    Values.Add(Value);
                    int IntValue = (int)(255.0 * (1.0 + Value) / 2);

                    bmp.SetPixel(j, i, Color.FromArgb(IntValue, IntValue, IntValue));
                    k += 1;
                }
            }

            return bmp;
        }

        private Bitmap GetValue(Bitmap bmp)
        {
            int k = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Noise.SetNoiseType(FastNoise.NoiseType.Value);
                    double Value = Noise.GetNoise(j, i, Depth);
                    Values.Add(Value);
                    int IntValue = (int)(255.0 * (1.0 + Value) / 2);

                    bmp.SetPixel(j, i, Color.FromArgb(IntValue, IntValue, IntValue));
                    k += 1;
                }
            }

            return bmp;
        }


        private Bitmap GetCubicFractal(Bitmap bmp)
        {
            int k = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = Noise.GetCubicFractal(j, i, Depth);
                    Values.Add(Value);
                    int IntValue = (int)(255.0 * (1.0 + Value) / 2);

                    bmp.SetPixel(j, i, Color.FromArgb(IntValue, IntValue, IntValue));
                    k += 1;
                }
            }

            return bmp;
        }

        private Bitmap GetPerlinFractal(Bitmap bmp)
        {
            int k = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = Noise.GetPerlinFractal(j, i, Depth);
                    Values.Add(Value);
                    int IntValue = (int)(255.0 * (1.0 + Value) / 2);

                    bmp.SetPixel(j, i, Color.FromArgb(IntValue, IntValue, IntValue));
                    k += 1;
                }
            }

            return bmp;
        }

        private Bitmap GetSimplexFractal(Bitmap bmp)
        {
            int k = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    double Value = Noise.GetSimplexFractal(j, i, Depth);
                    Values.Add(Value);
                    int IntValue = (int)(255.0 * (1.0 + Value) / 2);

                    bmp.SetPixel(j, i, Color.FromArgb(IntValue, IntValue, IntValue));
                    k += 1;
                }
            }

            return bmp;
        }

        private Bitmap GetValueFractal(Bitmap bmp)
        {
            int k = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Noise.SetNoiseType(FastNoise.NoiseType.Value);
                    double Value = Noise.GetNoise(j, i, Depth);
                    Values.Add(Value);
                    int IntValue = (int)(255.0 * (1.0 + Value) / 2);

                    bmp.SetPixel(j, i, Color.FromArgb(IntValue, IntValue, IntValue));
                    k += 1;
                }
            }

            return bmp;
        }


        public void SetSize(int width, int height, int depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
        }

        public void SetNoiseParameters(NoiseModes mode, double frequency, InterpolationModes interpolation)
        {
            NoiseMode = mode;
            Frequency = frequency;
            InterpolationMode = interpolation;

            Noise.SetFrequency(Frequency);
            Noise.SetInterp((FastNoise.Interp)(int)InterpolationMode);

        }

        public void SetFractal(FractalModes mode, int octaves, double lacunarity, double gain)
        {
            FractalMode = mode;

            Octaves = octaves;
            Lacunarity = lacunarity;
            Gain = gain;

            Noise.SetFractalType((FastNoise.FractalType)(int)FractalMode);

            Noise.SetFractalOctaves(Octaves);
            Noise.SetFractalLacunarity(Lacunarity);
            Noise.SetFractalGain(Gain);
        }

        public void SetPerturbance(double amplitude, double frequency)
        {
            PerturbanceAmplitude = amplitude;
            PerturbanceFrequency = frequency;

            Noise.GradientPerturb(ref frequency, ref frequency, ref amplitude);
            Noise.SetGradientPerturbAmp(PerturbanceAmplitude);
        }

        public void SetFractalPerturbance(double amplitude, double frequency)
        {
            PerturbanceAmplitude = amplitude;
            PerturbanceFrequency = frequency;

            //Noise.SetGradientPerturbAmp(PerturbanceFrequency);
        }

    }
}
