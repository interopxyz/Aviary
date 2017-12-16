using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Utilities;

namespace Wind_GH.Files
{
    public class ReadBitmap : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ReadBitmap class.
        /// </summary>
        public ReadBitmap()
          : base("Bitmap Read", "Bmp In", "---", "Aviary", "File")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("FilePath", "F", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Width", "W", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Height", "H", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            string P = null;

            // Access the input parameters 
            if (!DA.GetData(0, ref P)) return;

            GetBitmap bmpObj = new GetBitmap(P);
            

            DA.SetData(0, bmpObj.BitmapObject);
            DA.SetData(1, bmpObj.Width);
            DA.SetData(2, bmpObj.Height);
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.septenary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Wind_Bitmap_Open;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{592d30f9-10c4-4724-ae22-95cfc625b125}"); }
        }
    }
}