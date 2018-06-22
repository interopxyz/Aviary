using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media.Media3D;

using Wind.Geometry.Vectors;
using Wind.Presets;
using Wind.Scene;
using Wind.Types;

namespace Wind.Geometry.Meshes
{
    public class wMesh
    {
        public List<wVertex> Vertices = new List<wVertex>();
        public List<wFace> Faces = new List<wFace>();
        public List<wNormal> VertexNormals = new List<wNormal>();
        public List<wNormal> FaceNormals = new List<wNormal>();
        public List<wDomain> Edges = new List<wDomain>();
        public List<wColor> Colors = new List<wColor>();
        public wShader Material = new wShader();

        public MeshGeometry3D WpfMesh = new MeshGeometry3D();
        
        public wMesh()
        {

        }

        public void AddVertex(double X, double Y, double Z, int Index)
        {
            Vertices.Add(new wVertex(X, Y, Z, Index));
            WpfMesh.Positions.Add(new Point3D(X, Y, Z));
            Colors.Add(wColors.LightGray);
        }

        public void AddVertex(double X, double Y, double Z, int Index, wColor VertexColor)
        {
            Vertices.Add(new wVertex(X, Y, Z, Index));
            WpfMesh.Positions.Add(new Point3D(X, Y, Z));
            Colors.Add(VertexColor);
        }

        public void AddNormal(double X, double Y, double Z, int Index)
        {
            VertexNormals.Add(new wNormal(X, Y, Z,Index));
            WpfMesh.Normals.Add(new Vector3D(X, Y, Z));
        }

        public void AddTriangularFace(int A, int B, int C, int Index)
        {
            Faces.Add(new wFace(A, B, C, Index));
            WpfMesh.TriangleIndices.Add(A);
            WpfMesh.TriangleIndices.Add(B);
            WpfMesh.TriangleIndices.Add(C);
        }

        public void AddEdges(int T0, int T1)
        {
            Edges.Add(new wDomain(T0,T1));
        }

        public void AddFaceNormal(double X, double Y, double Z, int Index)
        {
            FaceNormals.Add(new wNormal(X, Y, Z, Index));
        }

        public void SetVertices(List<wPoint> points)
        {
            foreach(wPoint pt in points)
            {
                Vertices.Add(new wVertex(pt.X, pt.Y, pt.Z));
            }
        }

    }
}
