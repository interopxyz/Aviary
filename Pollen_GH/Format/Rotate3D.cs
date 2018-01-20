using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Pollen.Collections;
using Grasshopper.Kernel.Parameters;
using Wind.Scene;
using System.Windows.Forms;
using GH_IO.Serialization;
using Pollen.Charts;
using Pollen.Utilities;
using Parrot.Containers;

namespace Pollen_GH.Format
{
    public class Rotate3D : GH_Component
    {
        public int ModeLighting = 0;

        /// <summary>
        /// Initializes a new instance of the Rotate3D class.
        /// </summary>
        public Rotate3D()
          : base("Rotate3D", "R3D", "---", "Aviary", "Charting & Data")
        {
            this.UpdateMessage();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("DataSet", "D", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Pivot", "P", "---", GH_ParamAccess.item,0);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Tilt", "T", "---", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Distance", "Z", "---", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;
            
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
            int X = 0;
            int Y = 0;
            int Z = 0;
            bool D = false;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref X)) return;
            if (!DA.GetData(2, ref Y)) return;
            if (!DA.GetData(3, ref Z)) return;

            wObject W;
            Element.CastTo(out W);
            
            
            D = !((X == 0) & (Y == 0) & (Z == 0));

            p3D V = new p3D(X, Y, Z, D, (p3D.LightingMode)ModeLighting);

            switch (W.Type)
            {
                case "Pollen":

                    switch (W.SubType)
                    {
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;

                            tDataSet.SetThreeDView(D, V);

                            W.Element = tDataSet;
                            break;
                        case "PointChart":
                            pElement E = (pElement)W.Element;
                            pChart C = (pChart)E.PollenControl;

                            C.View = V;
                            C.SetThreeDView();

                            E.PollenControl = C;
                            W.Element = E;
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

            Menu_AppendItem(menu, "None", ModeNone, true, (ModeLighting == 0));
            Menu_AppendItem(menu, "Simple", ModeSimple, true, (ModeLighting == 1));
            Menu_AppendItem(menu, "Realistic", ModeReal, true, (ModeLighting == 2));

        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("Mode", ModeLighting);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            ModeLighting = reader.GetInt32("Mode");

            return base.Read(reader);
        }

        private void ModeNone(Object sender, EventArgs e)
        {
            ModeLighting = 0;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void ModeSimple(Object sender, EventArgs e)
        {
            ModeLighting = 1;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void ModeReal(Object sender, EventArgs e)
        {
            ModeLighting = 2;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }
        
        private void UpdateMessage()
        {
            string[] msg = { "None", "Simple", "Realistic" };
            Message = msg[ModeLighting];
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.quinary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Pollen_3D;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{64587746-3310-42a8-9571-84b9d7d8b79a}"); }
        }
    }
}