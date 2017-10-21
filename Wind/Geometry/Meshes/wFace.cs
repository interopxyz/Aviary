using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wind.Geometry.Meshes
{
    public class wFace
    {
        public int A;
        public int B;
        public int C;
        public int D;

        public int[] Indices;

        public bool IsTriangle;
        public bool IsQuad;
        public bool IsNgon;

        public bool IsValid;

        public wFace()
        {

        }

        public wFace(int FaceIndexA, int FaceIndexB, int FaceIndexC)
        {
            A = FaceIndexA;
            B = FaceIndexB;
            C = FaceIndexC;
            D = -1;

            Indices = new int[] { A, B, C };

            IsTriangle = true;
        IsQuad = false;
        IsNgon = false;

            IsValid = true;
        }

        public wFace(int FaceIndexA, int FaceIndexB, int FaceIndexC, int FaceIndexD)
        {
            A = FaceIndexA;
            B = FaceIndexB;
            C = FaceIndexC;
            D = FaceIndexD;

            Indices = new int[] { A,B,C,D};

            IsTriangle = false;
            IsQuad = true;
            IsNgon = false;

            IsValid = true;
        }

        public wFace(int[] FaceIndices)
        {
            if (FaceIndices.Count()<3)
            {
                A = -1;
                B = -1;
                C = -1;
                D = -1;

                Indices = null;

                IsTriangle = false;
                IsQuad = false;
                IsNgon = false;

                IsValid = false;
            }
            else
            {
                switch (FaceIndices.Count())
                {
                    case 3:
                        A = FaceIndices[0];
                        B = FaceIndices[1];
                        C = FaceIndices[2];
                        D = -1;

                        Indices = FaceIndices;

                        IsTriangle = true;
                        IsQuad = false;
                        IsNgon = true;

                        IsValid = true;
                        break;
                    case 4:
                        A = FaceIndices[0];
                        B = FaceIndices[1];
                        C = FaceIndices[2];
                        D = FaceIndices[3];

                        Indices = FaceIndices;

                        IsTriangle = false;
                        IsQuad = true;
                        IsNgon = true;

                        IsValid = true;
                        break;
                    default:

                        break;
                }
            }
        }
    }
}
