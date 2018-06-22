using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;
using Wind.Geometry.Meshes;
using Wind.Geometry.Curves;
using Wind.Geometry.Vectors;

namespace Wind_GH.Utilities
{
    public class wMeshToRhMesh:Mesh
    {

     public wMeshToRhMesh(wMesh inputMesh)
        {

            foreach(wVertex v in inputMesh.Vertices)
            {
                this.Vertices.Add(new Point3d(v.X, v.Y, v.Z));
            }

            foreach(wFace f in inputMesh.Faces)
            {
                this.Faces.AddFace(new MeshFace(f.A, f.B, f.C));
            }

            this.FaceNormals.ComputeFaceNormals();
            this.Normals.ComputeNormals();
            this.Normals.UnitizeNormals();

        }   

    }
}
