using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Geometry.Meshes;
using Wind.Geometry.Curves;
using Wind.Geometry.Vectors;
using Wind_GH.Utilities;

namespace Wind_GH.Collections
{
    public class PolylineToMesh : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the PolylineToMesh class.
        /// </summary>
        public PolylineToMesh()
          : base("PlinetoMesh", "PL2MSH", "---", "Aviary", "Presets")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "P", "---", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Point3d> points = new List<Point3d>();
            if (!DA.GetDataList(0, points)) return;

            List<wPoint> x = new List<wPoint>();

            foreach(Point3d pt in points)
            {
                x.Add(new wPoint(pt.X, pt.Y, pt.Z));
            }

            wMesh msh = new Wind.Geometry.Utilities.PolylineToMesh(new wPolyline(x));

            Mesh m = new Mesh();
            
            foreach (wVertex v in msh.Vertices)
            {
                m.Vertices.Add(new Point3d(v.X, v.Y, v.Z));
            }

            foreach (wFace f in msh.Faces)
            {
                m.Faces.AddFace(new MeshFace(f.A, f.B, f.C));
            }

            m.FaceNormals.ComputeFaceNormals();
            m.Normals.ComputeNormals();
            m.Normals.UnitizeNormals();

            DA.SetData(0, m);
        }

        private void wMeshToRhMesh(wMesh msh)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e4432ce1-816c-44a7-9da3-d0292eea472a"); }
        }
    }
}