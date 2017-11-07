using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Pollen.Collections;
using Grasshopper.Kernel.Parameters;
using Wind.Scene;

namespace Pollen_GH.Format
{
    public class Rotate3D : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Rotate3D class.
        /// </summary>
        public Rotate3D()
          : base("Rotate3D", "R3D", "---", "Aviary", "Charting & Data")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("DataSet", "D", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Camera", "C", "---", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Lighting Style", "L", "---", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[2];
            param.AddNamedValue("None", 0);
            param.AddNamedValue("Simple", 1);
            param.AddNamedValue("Realistic", 2);

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
            IGH_Goo U = null;
            int L = 0;
            bool D = false;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref U)) return;
            if (!DA.GetData(2, ref L)) return;

            wObject W;
            Element.CastTo(out W);

            wCamera C = new wCamera();
            U.CastTo(out C);

            int X = (int)C.Pivot;
            int Y = (int)C.Tilt;
            int P = (int)C.Length;
            
            D = !((X == 0) & (Y == 0) & (P == 0));

            switch (W.Type)
            {
                case "Pollen":

                    switch (W.SubType)
                    {
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;
                            tDataSet.SetThreeDView(D, X, Y, P, L);
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