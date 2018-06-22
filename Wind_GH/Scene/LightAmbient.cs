using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Scene;
using Wind.Types;

namespace Wind_GH.Scene
{
    public class LightAmbient : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the LightAmbient class.
        /// </summary>
        public LightAmbient()
          : base("Ambient Light", "Ambient", "---", "Aviary", "3D Format")
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


            if (!DA.GetData(0, ref I)) return;
            if (!DA.GetData(1, ref C)) return;

            wLight Light = new wAmbientLight( new wColor(C));

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
                return Properties.Resources.Wind_Lights_Ambient;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{9cc1b9d7-ef8a-4e6e-98d2-87a57644f098}"); }
        }
    }
}