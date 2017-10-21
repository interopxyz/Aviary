using System;

using Grasshopper.Kernel;
using Wind.Containers;
using Grasshopper.Kernel.Parameters;
using Macaw.Filtering;
using Macaw.Filtering.Objects.Edging;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;

namespace Macaw_GH.Filtering.Object
{
    public class Edge : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Edge class.
        /// </summary>
        public Edge()
          : base("Edge", "Edge", "...", "Aviary", "Image Edit")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager[0].Optional = true;
            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.PersistentData.Append(new GH_ObjectWrapper(new Bitmap(100, 100)));

            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Size", "S", "---", GH_ParamAccess.item, 1);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Low", "L", "---", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("High", "H", "---", GH_ParamAccess.item, 0);
            pManager[4].Optional = true;
            

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Simple", 0);
            param.AddNamedValue("Difference", 1);
            param.AddNamedValue("Canny", 2);
            param.AddNamedValue("Homogenity", 3);
            param.AddNamedValue("Sobel", 4);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Filter", "F", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo X = null;
            int M = 0;
            int L = 0;
            int H = 0;
            int S = 1;

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref S)) return;
            if (!DA.GetData(3, ref L)) return;
            if (!DA.GetData(4, ref H)) return;
            
            Bitmap A = null;
            if (X != null) { X.CastTo(out A); }
            Bitmap B = new Bitmap(A);
            
            mFilter Filter = new mFilter();

            switch (M)
            {
                case 0:
                    Filter = new mEdgeSimple(L,S);
                    break;
                case 1:
                    Filter = new mEdgeDifference();
                    break;
                case 2:
                    Filter = new mEdgeCanny(L, H, S);
                    break;
                case 3:
                    Filter = new mEdgeHomogenity();
                    break;
                case 4:
                    Filter = new mEdgeSobel();
                    break;
            }

            B = new mApply(A, Filter).ModifiedBitmap;
            
            wObject W = new wObject(Filter, "Macaw", Filter.Type);


            DA.SetData(0, B);
            DA.SetData(1, W);
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
                return Properties.Resources.Macaw_Filter_Edges;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("2296c265-1159-4275-b96c-8db295b815e3"); }
        }
    }
}