using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Scene;
using Wind.Geometry.Vectors;

namespace Wind_GH.Scene
{
    public class Camera : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SetCamera class.
        /// </summary>
        public Camera()
          : base("Set Camera", "Camera", "---", "Aviary", "3D Scene")
        {

        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Center", "C", "---", GH_ParamAccess.item, new Point3d(0, 0, 0));
            pManager[0].Optional = true;
            pManager.AddNumberParameter("Pivot", "P", "---", GH_ParamAccess.item,0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Tilt", "T", "---", GH_ParamAccess.item,0);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Distance", "D", "---", GH_ParamAccess.item,1);
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Lens Length", "L", "---", GH_ParamAccess.item,0);
            pManager[4].Optional = true;
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

            Point3d C = new Point3d(0,0,0);
            double P = 0;
            double T = 0;
            double D = 0;
            double L = 0;

            if (!DA.GetData(0, ref C)) return;
            if (!DA.GetData(1, ref P)) return;
            if (!DA.GetData(2, ref T)) return;
            if (!DA.GetData(3, ref D)) return;
            if (!DA.GetData(4, ref L)) return;
            
            wCamera Camera = new wCamera(new wPoint(C.X,C.Y,C.Z),P,T,D,L);
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
            get { return new Guid("{ee59a9b9-fe8a-4beb-beb7-00239905861e}"); }
        }
    }
}