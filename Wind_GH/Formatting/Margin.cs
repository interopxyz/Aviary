﻿using System;
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
using Pollen.Charts;

namespace Wind_GH.Formatting
{
    public class Margin : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Padding class.
        /// </summary>
        public Margin()
          : base("Margin", "Margin", "---", "Aviary", "2D Format")
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
            double T0 = 1;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref T0)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            G.Margin[0] = T0;
            G.Margin[1] = T0;
            G.Margin[2] = T0;
            G.Margin[3] = T0;
            
            W.Graphics = G;

            switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;
                    pControl C = (pControl)E.ParrotControl;
                    C.Graphics = G;

                    C.SetMargin();

                    break;
                case "Pollen":
                    switch (W.SubType)
                    {
                        case "DataPoint":
                            DataPt tDataPt = (DataPt)W.Element;
                            tDataPt.Graphics = G;

                            W.Element = tDataPt;
                            break;
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;
                            tDataSet.Graphics = G;

                            W.Element = tDataSet;
                            break;
                        case "Chart":
                        case "Table":

                            pElement pE = (pElement)W.Element;
                            pChart pC = pE.PollenControl;
                            pC.Graphics = G;

                            pC.SetMargin();

                            pE.PollenControl = pC;
                            W.Element = pE;
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
                return Properties.Resources.Parrot_Margin;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{c2419454-cee4-40dc-bf18-cf142816b090}"); }
        }
    }
}