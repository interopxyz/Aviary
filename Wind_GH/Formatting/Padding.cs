using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Parrot.Containers;
using Parrot.Controls;

namespace Wind_GH.Formatting
{
    public class Padding : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Padding class.
        /// </summary>
        public Padding()
          : base("Padding", "Padding", "---", "Aviary", "Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            pManager.AddNumberParameter("Weight", "W", "---", GH_ParamAccess.item, 1);
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            pManager.AddGenericParameter("Graphics", "G", "Graphics Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            IGH_Goo Element = null;
            double T0 = 1;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref T0)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            G.Padding[0] = T0;
            G.Padding[1] = T0;
            G.Padding[2] = T0;
            G.Padding[3] = T0;
            
            W.Graphics = G;

            if (W.Type == "Parrot")
            {
                pElement E = (pElement)W.Element;
                pControl C = (pControl)E.ParrotControl;

                C.Graphics = G;
                C.SetPadding();
            }

            DA.SetData(0, W);

        }

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
                return Properties.Resources.Parrot_Padding;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("6d1c729a-c745-49e1-b60a-442c503407cb"); }
        }
    }
}