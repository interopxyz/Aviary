using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Scene;
using Wind.Geometry.Vectors;

namespace Wind_GH.Scene
{
    public class CameraLinear : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CameraLinear class.
        /// </summary>
        public CameraLinear()
          : base("Linear Camera", "Cam", "---", "Aviary", "3D Scene")
        {

        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddLineParameter("Line", "C", "---", GH_ParamAccess.item, new Line(0,0,0,100,100,100));
            pManager[0].Optional = true;
            pManager.AddNumberParameter("Lens Length", "L", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Camera", "C", "Wind Camera Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            Point3d C = new Point3d(0, 0, 0);
            Line X = new Line(0, 0, 0, 100, 100, 100);
            double P = 0;
            double T = 0;
            double D = 0;
            double L = 0;

            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref L)) return;

            Plane XY = Plane.WorldXY;

            Vector3d VY = new Vector3d(X.From.X-X.To.X,X.From.Y-X.To.Y,0);
            Vector3d VZ = new Vector3d(X.From.X - X.To.X, X.From.Y - X.To.Y, X.From.Z - X.To.Z);

            P = Vector3d.VectorAngle(XY.XAxis, VY, XY) / Math.PI * 180;
            T = Vector3d.VectorAngle(XY.ZAxis, VZ) / Math.PI * 180;
            
            wCamera Camera = new wCamera(new wPoint(X.To.X, X.To.Y, X.To.Z), P, T, X.Length, L);
            Camera.IsPreset = false;

            DA.SetData(0, Camera);

        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Wind_Camera;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("82e0cf16-3c52-422e-b429-6dec5a873fc7"); }
        }
    }
}