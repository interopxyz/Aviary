using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.IO;
using Flock.TJS.Build;
using Flock.TJS.Assemblies;

namespace Wind_GH.Files
{
    public class WriteThreeJS : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the WriteThreeJS class.
        /// </summary>
        public WriteThreeJS()
          : base("Write 3JS", "3JS Out", "---", "Aviary", "File")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("3JS Object", "3JS", "---", GH_ParamAccess.item);
            pManager.AddTextParameter("File Path", "P", "The target filepath for the image to be saved", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Save", "S", "If true the file will be saved", GH_ParamAccess.item);
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

            ThreeJS Y = new ThreeJS();

            // Access the input parameters 
            if (!DA.GetData(0, ref Y)) return;
            if (!DA.GetData(1, ref F)) return;
            if (!DA.GetData(2, ref S)) return;

            Compile3JS X = new Compile3JS(Y);

            string FilePath = "C:\\Users\\Public\\Documents\\untitled.svg";

            if (F == null)
            {
                if (this.OnPingDocument().FilePath != null)
                {
                    FilePath = Path.GetDirectoryName(this.OnPingDocument().FilePath) + "\\" + Path.GetFileNameWithoutExtension(this.OnPingDocument().FilePath) + ".svg";
                }
            }
            else
            {
                FilePath = F;
            }

            if (S)
            {
                X.Save(FilePath);
            }

            DA.SetData(0, FilePath);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("1c860d96-98ec-4466-8dcc-7c9d1a76f990"); }
        }
    }
}