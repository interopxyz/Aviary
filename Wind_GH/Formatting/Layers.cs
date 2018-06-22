using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Parrot.Containers;
using Parrot.Controls;
using Pollen.Collections;
using Wind.Geometry.Curves;
using Grasshopper.Kernel.Parameters;
using Parrot.Displays;
using Wind.Utilities;

namespace Wind_GH.Formatting
{
    public class Layers : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Layers class.
        /// </summary>
        public Layers()
          : base("Layer", "Layer", "---", "Aviary", "2D Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Wind Objects", GH_ParamAccess.item);
            pManager.AddTextParameter("Layer Name", "N", "---", GH_ParamAccess.item, "Layer 01");
            pManager[1].Optional = true;
            
            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.PersistentData.Append(new GH_ObjectWrapper(new pSpacer(new GUIDtoAlpha(Convert.ToString(this.Attributes.InstanceGuid.ToString() + Convert.ToString(this.RunCount)), false).Text)));
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
            string LayerName = "Layer 01";

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref LayerName)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            G.Layer = LayerName;

            W.Graphics = G;

            switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;
                    pControl C = (pControl)E.ParrotControl;
                    C.Graphics = G;

                    break;
                case "Pollen":
                    switch (W.SubType)
                    {

                    }
                    break;
                case "Hoopoe":
                    wShapeCollection Shapes = (wShapeCollection)W.Element;

                    Shapes.Group = LayerName;
                    Shapes.Graphics.Layer = LayerName;

                    W.Element = Shapes;
                    break;
            }

            DA.SetData(0, W);
            DA.SetData(1, G);
        }

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
                return Properties.Resources.Wind_Layers;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("36062f11-ec5a-4521-98e0-833b2b064ebd"); }
        }
    }
}