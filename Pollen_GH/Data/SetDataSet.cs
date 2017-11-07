using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using GH_IO.Serialization;
using System.Windows.Forms;
using Grasshopper.Kernel.Data;

using Wind.Containers;

using Pollen.Collections;
using Pollen_GH.Methods;

namespace Pollen_GH.Data
{
    public class SetDataSet : GH_Component
    {
        bool modeStatus = false;

        public SetDataSet()
          : base("Compile Data", "Data", "---", "Aviary", "Charting & Data")
        {
            this.UpdateMessage();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Data Set", "D", "A list or datatree of Pollen DataPoints", GH_ParamAccess.list);
            pManager.AddTextParameter("Set Title", "T", "The title of the data series", GH_ParamAccess.item, "");
            pManager[1].Optional = true;
            
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Data Collection", "Dc", "A compiled data and formatting collection", GH_ParamAccess.item);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            DataSetCollection DataTrees = new DataSetCollection();
            if (modeStatus)
            {
                TreeToArray TtA = new TreeToArray();
                List<string> Tb = new List<string>();
                GH_Structure<IGH_Goo> Db;

                // Access the input parameters 
                if (!DA.GetDataTree(0, out Db)) return;
                if (!DA.GetDataList(1, Tb)) return;

                //Output the formatting Object
                DataTrees = new DataSetCollection(Tb, TtA.FromObject(Db));

            }
            else
            {
                ListToArray TlA = new ListToArray();
                string Ta = null;
                List<IGH_Goo> Da = new List<IGH_Goo>();

                // Access the input parameters 
                if (!DA.GetDataList(0, Da)) return;
                if (!DA.GetData(1, ref Ta)) return;

                //Output the formatting Object
                DataTrees = new DataSetCollection(Ta, TlA.FromObject(Da));

            }

            // Assign the class to the output parameter.
            wObject WindObject = new wObject(DataTrees, "Pollen", "DataSet");

            DA.SetData(0, WindObject);
        }

        /// <summary>
        /// The Exposure property control
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
                // You can add image files to your project resources and access them like this:
                return Properties.Resources.Parrot_Data_Gray;
            }
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "List", List_Mode, true, !modeStatus);
            Menu_AppendItem(menu, "Tree", Tree_Mode, true, modeStatus);
        }

        private void Set_Mode(bool mode)
        {
            if (mode)
            {
                modeStatus = true;
                Params.Input[0].Access = GH_ParamAccess.tree;
                Params.Input[1].Access = GH_ParamAccess.list;
            }
            else
            {
                modeStatus = false;
                Params.Input[0].Access = GH_ParamAccess.list;
                Params.Input[1].Access = GH_ParamAccess.item;
            }

            this.UpdateMessage();
            this.ExpireSolution(true);

        }

        private void Tree_Mode(Object sender, EventArgs e)
        {
            Set_Mode(true);
        }

        private void List_Mode(Object sender, EventArgs e)
        {
            Set_Mode(false);
        }

        /// <summary>
        /// Adds to the default serialization method to save the current child status so it persists on copy/paste and save/reopen.
        /// </summary>

        public override bool Write(GH_IWriter writer)
        {
            writer.SetBoolean("ModeStatus", modeStatus);
            return base.Write(writer);
        }

        /// <summary>
        /// Adds to the default deserialization method to retrieve the saved child status so it persists on copy/paste and save/reopen.
        /// </summary>
        public override bool Read(GH_IReader reader)
        {
            bool readVal = false;
            reader.TryGetBoolean("ModeStatus", ref readVal);
            modeStatus = readVal;
            Set_Mode(modeStatus);

            return base.Read(reader);
        }

        private void UpdateMessage()
        {
            if (modeStatus) { Message = "Tree"; } else { Message = "List"; }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
    {
      get { return new Guid("{cb0b1c64-ece4-4bf7-8932-754d431ac763}"); }
    }
  }
}