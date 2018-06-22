using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Compiling;
using Wind.Containers;
using Grasshopper.Kernel.Parameters;
using System.Collections.Generic;
using Macaw.Compiling.Modifiers;

namespace Macaw_GH.Compose
{
    public class Layer : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Layer class.
        /// </summary>
        public Layer()
          : base("Assign Layer", "Layer", "---", "Aviary", "Bitmap Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Image", "B", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("BlendMode", "M", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Opacity", "O", "---", GH_ParamAccess.item, 100);
            pManager[2].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("00 Normal", 0);
            param.AddNamedValue("23 Color", 23);
            param.AddNamedValue("08 ColorBurn", 8);
            param.AddNamedValue("07 ColorDodge", 7);
            param.AddNamedValue("05 Darken", 5);
            param.AddNamedValue("12 DarkerColor", 12);
            param.AddNamedValue("19 Difference", 19);
            param.AddNamedValue("01 Dissolve", 1);
            param.AddNamedValue("20 Exclusion", 20);
            param.AddNamedValue("13 HardLight", 13);
            param.AddNamedValue("06 Lighten", 6);
            param.AddNamedValue("11 LighterColor", 11);
            param.AddNamedValue("10 LinearBurn", 10);
            param.AddNamedValue("09 LinearDodge", 9);
            param.AddNamedValue("16 LinearLight", 16);
            param.AddNamedValue("24 Luminosity", 24);
            param.AddNamedValue("02 Multiply", 2);
            param.AddNamedValue("04 Overlay", 4);
            param.AddNamedValue("17 PinLight", 17);
            param.AddNamedValue("03 Screen", 3);
            param.AddNamedValue("14 SoftLight", 14);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo X = null;
            int M = 0;
            int T = 100;
            List<IGH_Goo> F = new List<IGH_Goo>();

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref T)) return;

            Bitmap A = new Bitmap(10, 10);
            if (X != null) { X.CastTo(out A); }
            Bitmap B = (Bitmap)A.Clone();

            mLayer LayerObject = new mLayerImage(B, M, (byte)T);
            
            wObject W = new wObject(LayerObject, "Macaw", LayerObject.Type);


            DA.SetData(0, W);
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Build_Layer;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("fafdfbbf-ab83-4b65-93bf-e5a2f25b0d96"); }
        }
    }
}