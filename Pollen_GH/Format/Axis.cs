using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Pollen.Collections;
using System.Windows.Forms;
using Wind.Types;
using Rhino.Geometry;
using Parrot.Containers;
using Pollen.Charts;

namespace Pollen_GH.Format
{
    public class Axis : GH_Component
    {
        int modeStatus = 2;

        /// <summary>
        /// Initializes a new instance of the Axis class.
        /// </summary>
        public Axis()
          : base("Format Axis", "Axis","---","Aviary", "Charting & Data")
        {
            this.UpdateMessage();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("DataSet", "D", "---", GH_ParamAccess.item);

            pManager.AddIntegerParameter("Grid Line Spacing", "S", "---", GH_ParamAccess.item, 1);
            pManager[1].Optional = true;

            pManager.AddBooleanParameter("Label", "L", "---", GH_ParamAccess.item, false);
            pManager[2].Optional = true;

            pManager.AddNumberParameter("Angle", "A", "---", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;

            pManager.AddIntervalParameter("Bounds", "B", "---", GH_ParamAccess.item, new Interval(0,0));
            pManager[4].Optional = true;

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
            int S = 1;
            bool L = true;
            int A = 0;
            Interval B = new Interval(0, 0);

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref S)) return;
            if (!DA.GetData(2, ref L)) return;
            if (!DA.GetData(3, ref A)) return;
            if (!DA.GetData(4, ref B)) return;

            wObject W;
            Element.CastTo(out W);

            switch (W.Type)
            {
                case "Pollen":

                    switch (W.SubType)
                    {
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;

                            switch(modeStatus)
                            {
                                default:
                                    tDataSet.Axes.SetXYAxes(S, new wDomain(B.T0, B.T1), L, A);
                                    break;
                                case 1:
                                    tDataSet.Axes.SetXAxis(S, new wDomain(B.T0, B.T1), L, A);
                                    break;
                                case 2:
                                    tDataSet.Axes.SetYAxis(S, new wDomain(B.T0, B.T1), L, A);
                                    break;
                            }

                            W.Element = tDataSet;
                            break;
                    }
                    break;
            }
            

            DA.SetData(0, W);
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "X&Y Axis", XYAxisMode, true, (modeStatus == 0));
            Menu_AppendItem(menu, "X Axis", XAxisMode, true, (modeStatus==1));
            Menu_AppendItem(menu, "Y Axis", YAxisMode, true, (modeStatus == 2));
        }

        private void XYAxisMode(Object sender, EventArgs e)
        {
            modeStatus = 0;
            this.ExpireSolution(true);
        }

        private void XAxisMode(Object sender, EventArgs e)
        {
            modeStatus = 1;
            this.ExpireSolution(true);
        }

        private void YAxisMode(Object sender, EventArgs e)
        {
            modeStatus = 2;
            this.ExpireSolution(true);
        }

        private void UpdateMessage()
        {
            string[] Messages = { "XY", "X", "Y" };
            Message = Messages[modeStatus];
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
                return Properties.Resources.Pollen_Axis;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{3b7d675d-874b-4df3-8a47-a818e7610063}"); }
        }
    }
}