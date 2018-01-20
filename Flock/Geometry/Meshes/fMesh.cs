using Hoopoe.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Meshes;
using Wind.Types;

namespace Flock.Geometry.Meshes
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

        public override void BuildThreeGeometry()
        {
            ThreeGeometry.Clear();
            ThreeGeometry.Append("var tempGeometry = new THREE.Geometry();" + Environment.NewLine);

            foreach(wVertex V in Mesh.Vertices){ThreeGeometry.Append("tempGeometry.vertices.push( new THREE.Vector3(" + V.X+", " +V.Y+", " +V.Z+"));" + Environment.NewLine);}
            //foreach (wNormal N in Mesh.VertexNormals) { ThreeGeometry.Append("geom.vertices.push( new THREE.Vector3(" + V.X + ", " + V.Y + ", " + V.Z + "));" + Environment.NewLine);}
            foreach (wFace F in Mesh.Faces) { ThreeGeometry.Append("tempGeometry.faces.push( new THREE.Face3(" + F.A + ", " + F.B + ", " + F.C + "));" + Environment.NewLine); }
            //foreach (wColor F in Mesh.Colors) { ThreeGeometry.Append("geom.faces.push( new THREE.Face3(" + F.A + ", " + F.B + ", " + F.C + "));" + Environment.NewLine); }

            ThreeGeometry.Append("tempGeometry.computeFaceNormals();" + Environment.NewLine);
            ThreeGeometry.Append("var testObject = new THREE.Mesh( tempGeometry, new THREE.MeshNormalMaterial() );" + Environment.NewLine);

            ThreeGeometry.Append("scene.add(testObject);" + Environment.NewLine);
        }

    }
}
