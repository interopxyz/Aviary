using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Vectors;

namespace Wind.Geometry.Meshes
{

    public class wNormal
    {
        public double X = 0;
        public double Y = 0;
        public double Z = 0;
        public int Index;
        public int TopologyIndex;

        public wNormal()
        {

        }

        public wNormal(double X_Position, double Y_Position, double Z_Position)
        {
            X = X_Position;
            Y = Y_Position;
            Z = Z_Position;
        }

        public wNormal(wVector NormalVector, int VertexIndex)
        {
            X = NormalVector.X;
            Y = NormalVector.Y;
            Z = NormalVector.Z;

            Index = VertexIndex;
        }

        public wNormal(wVector NormalVector, int VertexIndex, int VertexTopologyIndex)
        {
            X = NormalVector.X;
            Y = NormalVector.Y;
            Z = NormalVector.Z;

            Index = VertexIndex;
            TopologyIndex = VertexTopologyIndex;
        }

        public wNormal(double X_Position, double Y_Position, double Z_Position, int VertexIndex)
        {
            X = X_Position;
            Y = Y_Position;
            Z = Z_Position;

            Index = VertexIndex;
        }

        public wNormal(double X_Position, double Y_Position, double Z_Position, int VertexIndex, int VertexTopologyIndex)
        {
            X = X_Position;
            Y = Y_Position;
            Z = Z_Position;

            Index = VertexIndex;
            TopologyIndex = VertexTopologyIndex;
        }

    }
}
