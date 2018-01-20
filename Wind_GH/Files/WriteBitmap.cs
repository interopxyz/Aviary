using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.IO;
using System.Drawing;

namespace Wind_GH.Files
{
    public class WriteBitmap : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the WriteBitmap class.
        /// </summary>
        public WriteBitmap()
          : base("Bitmap Write", "Bmp Out", "---", "Aviary", "File")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap Object", "B", "The bitmap object to be saved", GH_ParamAccess.item);
            pManager.AddTextParameter("File Path", "P", "The target filepath for the image to be saved", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Save", "S", "If true the bitmap will be saved to a bitmap", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("File Path", "R", "The resulting filepath of the saved image", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            string F = null;
            bool S = false;

            Bitmap B = null;

            // Access the input parameters 
            if (!DA.GetData(0, ref F)) return;
            if (!DA.GetData(1, ref S)) return;
            if (!DA.GetData(2, ref B)) return;

            string FilePath = "C:\\Users\\Public\\Documents\\untitled.jpg";

            if (F == null)
            {
                if (this.OnPingDocument().FilePath != null)
                {
                    FilePath = Path.GetDirectoryName(this.OnPingDocument().FilePath) + "\\" + Path.GetFileNameWithoutExtension(this.OnPingDocument().FilePath) + ".jpg";
                }
            }
            else
            {
                FilePath = F;
            }

            if (S)
            {
                B.Save(FilePath);
            }

            DA.SetData(0, FilePath);
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
                return Properties.Resources.Wind_Bitmap_Save;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{c2420713-c692-4112-afff-78226a429b71}"); }
        }
    }
}