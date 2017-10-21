using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Filtering;
using Wind.Containers;
using Macaw.Compiling.Modifiers;
using Macaw.Build;
using Macaw.Filtering.Stylized;
using Macaw.Compiling;
using Macaw.Analysis;
using Wind.Geometry.Vectors;

namespace Macaw_GH.Filtering.Analyze
{
    public class Corner : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Corner class.
        /// </summary>
        public Corner()
          : base("Figure Corner", "Corner", "---", "Aviary", "Image Build")
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

            pManager.AddIntegerParameter("Difference", "D", "---", GH_ParamAccess.item, 25);
            pManager[2].Optional = true;

            pManager.AddIntegerParameter("Geometry", "G", "---", GH_ParamAccess.item, 18);
            pManager[3].Optional = true;

            pManager.AddColourParameter("Highlight Color", "H", "---", GH_ParamAccess.item, Color.Red);
            pManager[4].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Susan", 0);
            param.AddNamedValue("Moravec", 1);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Filter", "F", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Points", "P", "---", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo V = null;
            int M = 0;
            int D = 25;
            int G = 18;
            Color Y = Color.Red;

            // Access the input parameters 
            if (!DA.GetData(0, ref V)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref D)) return;
            if (!DA.GetData(3, ref G)) return;
            if (!DA.GetData(4, ref Y)) return;

            Bitmap A = null;
            if (V != null) { V.CastTo(out A); }
            Bitmap B = new Bitmap(A);


        List<wPoint> Points = new List<wPoint>();

            mFilter Filter = new mFilter();

            switch (M)
            {
                case 0:
                    mAnalyzeCornersSusan Scorner = new mAnalyzeCornersSusan(A, Y, D, G);
                    Points = Scorner.Points;
                    Filter = Scorner;
                    break;
                case 1:
                    mAnalyzeCornersMoravec Mcorner = new mAnalyzeCornersMoravec(A, Y, D);
                    Points = Mcorner.Points;
                    Filter = Mcorner;
                    break;
            }
            
            

            B = new mApply(A, Filter).ModifiedBitmap;

            wObject W = new wObject(Filter, "Macaw", Filter.Type);

            List<Point3d> P = new List<Point3d>();

            foreach(wPoint X in Points)
            {
                P.Add(new Point3d(X.X, X.Y, X.Z));
            }

            DA.SetData(0, B);
            DA.SetData(1, W);
            DA.SetDataList(2, P);
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
            get { return new Guid("b6c45097-d010-4ca0-8205-85c44143384d"); }
        }
    }
}