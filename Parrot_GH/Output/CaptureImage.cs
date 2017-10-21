using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Wind.Containers;
using Grasshopper.Kernel.Types;
using Parrot.Containers;
using System.IO;
using System.Drawing;

namespace Parrot_GH.Output
{
    public class CaptureImage : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CaptureImage class.
        /// </summary>
        public CaptureImage()
          : base("CaptureImage", "Capture", "---", "Aviary", "Utility")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "Element", GH_ParamAccess.item);
            pManager.AddIntegerParameter("DPI", "D", "DPI", GH_ParamAccess.item, 96);
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "Bitmap", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            //Set Unique Control Properties
            IGH_Goo X = null;
            int D = 96;
            
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref D)) return;

            wObject W = new wObject();
            X.CastTo(out W);
            
            pElement E = (pElement)W.Element;

            int XD = 800;
            int YD = 600;

            if (E.Layout.ActualWidth > 0) { XD = (int)E.Layout.ActualWidth; }
            if (E.Layout.ActualHeight > 0) { YD = (int)E.Layout.ActualHeight; }

            RenderTargetBitmap B = new RenderTargetBitmap((int)(XD*(D/96.0)), (int)(YD* (D / 96.0)), D, D, PixelFormats.Pbgra32);
            B.Render(E.Layout);
            
            MemoryStream stream = new MemoryStream();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(B));
            encoder.Save(stream);

            Bitmap F = new Bitmap(stream);
            F.MakeTransparent();

            DA.SetData(0, F);
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.senary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Parrot_Snapshot;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{41c6b2a0-ded7-46d4-95fc-1dd955ed7e95}"); }
        }
    }
}