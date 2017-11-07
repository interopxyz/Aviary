using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Scene;
using Wind.Geometry.Vectors;
using Wind.Types;

namespace Wind_GH.Scene
{
    public class LightPoint : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the LightPoint class.
        /// </summary>
        public LightPoint()
          : base("Point Light", "Point", "---", "Aviary", "3D Scene")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Intensity", "I", "---", GH_ParamAccess.item, -1);
            pManager[0].Optional = true;
            pManager.AddColourParameter("Color", "C", "---", GH_ParamAccess.item, System.Drawing.Color.White);
            pManager[1].Optional = true;
            pManager.AddPointParameter("Direction", "L", "---", GH_ParamAccess.item, new Point3d(0,0,1));
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Light", "L", "Light Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double I = -1;
            System.Drawing.Color C = System.Drawing.Color.White;
            Point3d P = new Point3d(0,0,1);

            if (!DA.GetData(0, ref I)) return;
            if (!DA.GetData(1, ref C)) return;
            if (!DA.GetData(2, ref P)) return;

            wLight Light = new wPointLight(new wPoint(P.X, P.Y, P.Z), new wColor(C));

            if (I >= 0) { Light.SetBrightness(I); }

            DA.SetData(0, Light);
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Wind_Lights_Point;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{9a32f78b-56dd-4a84-8781-63662691ce5b}"); }
        }
    }
}