using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;

using Wind.Containers;
using Wind.Types;

using Parrot.Containers;
using Parrot.Controls;
using Pollen.Collections;
using Grasshopper.Kernel.Parameters;
using Parrot.Displays;
using Wind.Utilities;

namespace Wind_GH.Formatting
{
    public class Corners : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Corners class.
        /// </summary>
        public Corners()
          : base("Corners", "Corners", "---", "Aviary", "2D Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Wind Objects", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Pad Interior", "P", "---", GH_ParamAccess.item, false);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Radius", "R", "---", GH_ParamAccess.item, 5);
            pManager[2].Optional = true;

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
            bool P = false;
            double R0 = 5;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref P)) return;
            if (!DA.GetData(2, ref R0)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            G.PadRadius = P;
            G.Radius[0] = R0;
            G.Radius[1] = R0;
            G.Radius[2] = R0;
            G.Radius[3] = R0;

            W.Graphics = G;

            if (P) { W.Graphics.SetPaddingFromCorners(); } else { W.Graphics.SetUniformPadding(0); }

            switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;
                    pControl C = (pControl)E.ParrotControl;
                    C.Graphics = G;

                    C.SetCorners();

                    break;
                case "Pollen":
                    switch (W.SubType)
                    {
                        case "DataPoint":
                            DataPt tDataPt = (DataPt)W.Element;

                            tDataPt.Graphics.Radius = G.Radius;
                            W.Element = tDataPt;
                            break;
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;

                            tDataSet.Graphics.Radius = G.Radius;
                            W.Element = tDataSet;
                            break;
                    }
                    break;
                case "Hoopoe":
                    break;
            }

            DA.SetData(0, W);
            DA.SetData(1, G);
        }


        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.quarternary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Wind_Corners;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{3c9e6a2d-ab16-441f-a508-d720a07bed45}"); }
        }
    }
}