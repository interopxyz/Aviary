using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Wind.Geometry.Vectors;

namespace Wind.Geometry.Meshes
{
    public class wVertex
    {
        public double X = 0.0;
        public double Y = 0.0;
        public double Z = 0.0;
        public int Index;
        public int TopologyIndex;

        public wVertex()
        {

        }

        public wVertex(double X_Position, double Y_Position, double Z_Position)
        {
            X = X_Position;
            Y = Y_Position;
            Z = Z_Position;
        }

        public wVertex(wPoint VertexPoint, int VertexIndex)
        {
            X = VertexPoint.X;
            Y = VertexPoint.Y;
            Z = VertexPoint.Z;

            Index = VertexIndex;
        }

        public wVertex(wPoint VertexPoint, int VertexIndex, int VertexTopologyIndex)
        {
            X = VertexPoint.X;
            Y = VertexPoint.Y;
            Z = VertexPoint.Z;

            Index = VertexIndex;
            TopologyIndex = VertexTopologyIndex;
        }

        public wVertex(double X_Position, double Y_Position, double Z_Position, int VertexIndex)
        {
            X = X_Position;
            Y = Y_Position;
            Z = Z_Position;

            Index = VertexIndex;
        }

        public wVertex(double X_Position, double Y_Position, double Z_Position, int VertexIndex, int VertexTopologyIndex)
        {
            X = X_Position;
            Y = Y_Position;
            Z = Z_Position;

            Index = VertexIndex;
            TopologyIndex = VertexTopologyIndex;
        }

        public Point3D ToPoint3D()
        {
            return new Point3D(X, Y, Z);
        }
    }
}
