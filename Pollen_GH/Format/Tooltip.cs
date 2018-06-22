using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Wind.Containers;
using Grasshopper.Kernel.Types;
using Wind.Types;
using Pollen.Collections;
using Wind.Presets;

namespace Pollen_GH.Format
{
    public class Tooltip : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Tooltip class.
        /// </summary>
        public Tooltip()
          : base("Tooltip", "Tooltip","---", "Aviary", "Charting & Data")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("DataSet", "D", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Graphic", "G", "---", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddGenericParameter("Font", "F", "---", GH_ParamAccess.item);
            pManager[2].Optional = true;


            Param_GenericObject paramGen1 = (Param_GenericObject)Params.Input[0];
            paramGen1.PersistentData.Append(new GH_ObjectWrapper(null));


            Param_GenericObject paramGen2 = (Param_GenericObject)Params.Input[1];
            paramGen2.PersistentData.Append(new GH_ObjectWrapper(new wGraphic()));


            Param_GenericObject paramGen3 = (Param_GenericObject)Params.Input[2];
            paramGen3.PersistentData.Append(new GH_ObjectWrapper(wFonts.ChartPointDark));
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            pManager.AddGenericParameter("Tooltip", "T", "Tooltip", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IGH_Goo Element = null;
            IGH_Goo Gx = null;
            IGH_Goo Fx = null;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref Gx)) return;
            if (!DA.GetData(2, ref Fx)) return;

            wObject W;
            Element.CastTo(out W);

            wLabel CustomToolTip = new wLabel();

            wGraphic G = CustomToolTip.Graphics;
            wFont F = CustomToolTip.Font;

            Gx.CastTo(out G);
            Fx.CastTo(out F);

            CustomToolTip.Graphics = G;
            CustomToolTip.Font = F;

            switch (W.Type)
            {
                case "Pollen":

                    switch (W.SubType)
                    {
                        case "DataPoint":
                            DataPt Pt = (DataPt)W.Element;

                            Pt.ToolTip.Enabled = true;
                            Pt.ToolTip.Graphics = G;
                            Pt.ToolTip.Font = F;
                            Pt.ToolTip.Graphics.FontObject = F;

                            W.Element = Pt;
                            break;
                        case "DataSet":
                            DataSetCollection St = (DataSetCollection)W.Element;
                            St.SetUniformTooltips(CustomToolTip);

                            St.ToolTip.Enabled = true;
                            St.ToolTip.Graphics = G;
                            St.ToolTip.Font = F;
                            St.ToolTip.Graphics.FontObject = F;

                            W.Element = St;
                            break;
                    }
                    break;
            }

            DA.SetData(0, W);
            DA.SetData(1, CustomToolTip);
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
                return Properties.Resources.Pollen_ToolTip;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("91c58635-346a-4f07-bad5-bb4d0e25c3b3"); }
        }
    }
}