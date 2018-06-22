using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;

using Wind.Containers;
using Wind.Types;

using Parrot.Containers;
using Pollen.Collections;
using Parrot.Controls;
using Pollen.Charts;
using Grasshopper.Kernel.Parameters;
using Parrot.Displays;
using Wind.Utilities;

namespace Wind_GH.Formatting
{
    public class Size : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Size class.
        /// </summary>
        public Size()
          : base("Size", "Size", "---", "Aviary", "2D Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            pManager.AddNumberParameter("Width", "W", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Height", "H", "---", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Scale", "S", "---", GH_ParamAccess.item, 1.0);
            pManager[3].Optional = true;

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
            double width = 0;
            double height = 0;
            double scale = 1.0;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref width)) return;
            if (!DA.GetData(2, ref height)) return;
            if (!DA.GetData(3, ref scale)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            G.Width = width;
            G.Height = height;
            G.Scale = scale;
            
            W.Graphics = G;

            switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;
                    pControl C = (pControl)E.ParrotControl;

                    C.Graphics = G;
                    C.SetSize();
                    break;
                case "Pollen":
                    switch (W.SubType)
                    {
                        default:
                            pElement El = (pElement)W.Element;
                            pChart P = (pChart)El.PollenControl;
                            P.Graphics = G;

                            P.SetSize();
                            break;
                        case "DataPoint":
                            DataPt tDataPt = (DataPt)W.Element;
                            tDataPt.Graphics = G;
                            
                            W.Element = tDataPt;
                            break;
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;
                            tDataSet.Graphics = G;

                            tDataSet.SetSeriesScales();

                            tDataSet.SetScales();
                            W.Element = tDataSet;
                            break;
                        case "Chart":
                        case "Table":

                            pElement pE = (pElement)W.Element;
                            pChart pC = pE.PollenControl;
                            pC.Graphics = G;

                            pC.SetSize();
                            pE.PollenControl = pC;
                            W.Element = pE;
                            break;
                    }
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
                return Properties.Resources.Wind_Sizing;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{81c07c87-da27-4a88-a70d-4a8c4185a214}"); }
        }
    }
}