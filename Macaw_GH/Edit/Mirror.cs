using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;
using Wind.Containers;
using Macaw.Filtering;
using Macaw.Editing.Rotation;
using Grasshopper.Kernel.Parameters;

namespace Macaw_GH.Edit
{
    public class Mirror : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Mirror class.
        /// </summary>
        public Mirror()
          : base("Mirror", "Mirror", "---", "Aviary", "Bitmap Edit")
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

            pManager.AddBooleanParameter("Horizontal", "H", "---", GH_ParamAccess.item, false);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Vertical", "V", "---", GH_ParamAccess.item, false);
            pManager[2].Optional = true;
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
            bool H = false;
            bool V = false;

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref H)) return;
            if (!DA.GetData(2, ref V)) return;

            Bitmap A = null;
            if (X != null) { X.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();
            
            Filter = new mMirror(H,V);
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
            get { return GH_Exposure.quinary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Edit_Mirror;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e8973dae-6b45-4b1b-91d7-a921fe5debf9"); }
        }
    }
}