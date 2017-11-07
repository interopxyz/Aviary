using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Pollen.Collections;
using Wind.Containers;
using Grasshopper.Kernel.Types;

namespace Pollen_GH.Format
{
    public class DataLabel : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Label class.
        /// </summary>
        public DataLabel()
          : base("Label", "Label", "---", "Aviary", "Charting & Data")
        {

        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("DataSet", "D", "---", GH_ParamAccess.item);
            
            pManager.AddIntegerParameter("Placement", "P", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Leader", "L", "---", GH_ParamAccess.item, false);
            pManager[2].Optional = true;


            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Bottom", 0);
            param.AddNamedValue("BottomLeft", 1);
            param.AddNamedValue("BottomRight", 2);
            param.AddNamedValue("Center", 3);
            param.AddNamedValue("Left", 4);
            param.AddNamedValue("Right", 5);
            param.AddNamedValue("Top", 6);
            param.AddNamedValue("TopLeft", 7);
            param.AddNamedValue("TopRight", 8);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IGH_Goo Element = null;
            int P = 0;
            bool L = false;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref P)) return;
            if (!DA.GetData(2, ref L)) return;

            wObject W;
            Element.CastTo(out W);


            switch (W.Type)
            {
                case "Pollen":

                    switch (W.SubType)
                    {
                        case "DataPoint":
                            DataPt tDataPt = (DataPt)W.Element;
                            W.Element = tDataPt;
                            break;
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;
                            tDataSet.SetLabel(P, L);
                            W.Element = tDataSet;
                            break;
                    }
                    break;
            }

            DA.SetData(0, W);
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
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
                return Properties.Resources.Pollen_Labels;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{89cd9015-82db-4851-a682-69c8b373866f}"); }
        }
    }
}