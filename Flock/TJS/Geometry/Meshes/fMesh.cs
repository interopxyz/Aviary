using Flock.TJS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Meshes;
using Wind.Types;

namespace Flock.TJS.Geometry.Meshes
{
    public class fMesh : fGeometry
    {

        public wMesh Mesh = new wMesh();

        public fMesh()
        {
        }

        public fMesh(wMesh InputMesh)
        {
            Mesh = InputMesh;
        }

        public override void BuildThreeGeometry(int Index)
        {
            string MeshName = "msh" + Index;
            string GeoName = "geo" + Index;
            Assembly.Clear();
            Assembly.Append("var " + MeshName + " = new THREE.Geometry();" + Environment.NewLine);

            foreach (wVertex V in Mesh.Vertices) { Assembly.Append(MeshName + ".vertices.push( new THREE.Vector3(" + V.X + ", " + V.Y + ", " + V.Z + "));" + Environment.NewLine); }
            //foreach (wNormal N in Mesh.VertexNormals) { ThreeGeometry.Append("geom.vertices.push( new THREE.Vector3(" + V.X + ", " + V.Y + ", " + V.Z + "));" + Environment.NewLine);}
            foreach (wFace F in Mesh.Faces) { Assembly.Append(MeshName + ".faces.push( new THREE.Face3(" + F.A + ", " + F.B + ", " + F.C + "));" + Environment.NewLine); }
            //foreach (wColor F in Mesh.Colors) { ThreeGeometry.Append("geom.faces.push( new THREE.Face3(" + F.A + ", " + F.B + ", " + F.C + "));" + Environment.NewLine); }

            Assembly.Append(MeshName + ".computeFaceNormals();" + Environment.NewLine);
            Assembly.Append("var " + GeoName + " = new THREE.Mesh( " + MeshName + ", new THREE.MeshNormalMaterial() );" + Environment.NewLine);

            Assembly.Append("scene.add(" + GeoName + ");" + Environment.NewLine);
        }

    }
}
