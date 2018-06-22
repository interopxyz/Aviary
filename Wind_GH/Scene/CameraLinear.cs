using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Scene;
using Wind.Geometry.Vectors;
using Wind.Scene.Cameras;

namespace Wind_GH.Scene
{
    public class CameraLinear : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CameraLinear class.
        /// </summary>
        public CameraLinear()
          : base("3pt Camera", "Cam", "---", "Aviary", "3D Format")
        {

        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Position", "P", "---", GH_ParamAccess.item, new Point3d(100,100,100));
            pManager[0].Optional = true;
            pManager.AddPointParameter("Target", "T", "---", GH_ParamAccess.item, new Point3d(0, 0, 0));
            pManager[1].Optional = true;
            pManager.AddVectorParameter("Up Vector", "U", "---", GH_ParamAccess.item, new Vector3d(0, 0, 1));
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Lens Length", "L", "---", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;
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

            Point3d P = new Point3d(100, 100, 100);
            Point3d T = new Point3d(0, 0, 0);
            Vector3d Z = new Vector3d(0, 0, 1);
            double L = 0;

            if (!DA.GetData(0, ref P)) return;
            if (!DA.GetData(1, ref T)) return;
            if (!DA.GetData(2, ref Z)) return;
            if (!DA.GetData(3, ref L)) return;

            wCamera Cam = new wCameraStandard(new wPoint(P.X, P.Y, P.Z), new wPoint(T.X,T.Y,T.Z),new wVector(Z.X,Z.Y,Z.Z),L);

            DA.SetData(0, Cam);

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