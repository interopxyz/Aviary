using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Curves;
using Wind.Geometry.Meshes;
using Wind.Geometry.Vectors;

namespace Wind.Geometry.Utilities
{
    public class PolylineToMesh : wMesh
    {
        public wPolyline pgon = new wPolyline();
        public List<wPolyline> Triangles = new List<wPolyline>();

        public PolylineToMesh(wPolyline polyline)
        {
            if (polyline.IsClockwise()) { polyline.Flip(); }
            polyline.OpenPolyline();

            this.SetVertices(polyline.Points);

            pgon = polyline;

            TriangulatePolyline();
        }


        public void TriangulatePolyline()
        {
            while (pgon.Points.Count > 3)
            {
                RemoveEar();
            }

            Triangles.Add(new wPolyline(new wPoint[] { pgon.Points[0], pgon.Points[1], pgon.Points[2] }));
            this.Faces.Add(new wFace(pgon.Indices[0], pgon.Indices[1], pgon.Indices[2]));

        }

        private void RemoveEar()
        {
            int A = 0, B = 0, C = 0;
            FindEar(ref A, ref B, ref C);

            Triangles.Add(new wPolyline(new wPoint[] { pgon.Points[A], pgon.Points[B], pgon.Points[C] }));
            this.Faces.Add(new wFace(pgon.Indices[A], pgon.Indices[B], pgon.Indices[C]));

            RemoveVertex(B);
        }

        private void RemoveVertex(int t)
        {
            wPoint[] p = new wPoint[pgon.Points.Count - 1];
            int[] i = new int[pgon.Indices.Count - 1];

            Array.Copy(pgon.Points.ToArray(), 0, p, 0, t);
            Array.Copy(pgon.Indices.ToArray(), 0, i, 0, t);

            Array.Copy(pgon.Points.ToArray(), t + 1, p, t, pgon.Points.Count - t - 1);
            Array.Copy(pgon.Indices.ToArray(), t + 1, i, t, pgon.Indices.Count - t - 1);

            pgon.Points = p.ToList();
            pgon.Indices = i.ToList();
        }

        private void FindEar(ref int A, ref int B, ref int C)
        {
            int x = pgon.Points.Count;

            for (A = 0; A < x-1; A++)
            {
                B = (A + 1) % x;
                C = (B + 1) % x;

                if (FormsEar(A, B, C)) { return; }
            }

        }

        private bool FormsEar(int A, int B, int C)
        {
            wVector V0 = new wVector(pgon.Points[A], pgon.Points[B]);
            wVector V1 = new wVector(pgon.Points[C], pgon.Points[B]);

            if (V0.GetAngle(V1) > 0) { return false; }

            wPolyline triangle = new wPolyline(new wPoint[] { pgon.Points[A], pgon.Points[B], pgon.Points[C] });

            for (int i = 0; i < pgon.Points.Count; i++)
            {
                if ((i != A) && (i != B) && (i != C))
                {
                    if (triangle.IsPointInside(pgon.Points[i])) { return false; }
                }
            }

            return true;
        }

    }
}
