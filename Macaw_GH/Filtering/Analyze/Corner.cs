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
using System.Windows.Forms;
using GH_IO.Serialization;

namespace Macaw_GH.Filtering.Analyze
{
    public class Corner : GH_Component
    {

        public int FilterMode = 0;

        /// <summary>
        /// Initializes a new instance of the Corner class.
        /// </summary>
        public Corner()
          : base("Figure Corner", "Corner", "---", "Aviary", "Bitmap Build")
        {
            this.UpdateMessage();
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
            
            pManager.AddIntegerParameter("Difference", "D", "---", GH_ParamAccess.item, 25);
            pManager[1].Optional = true;

            pManager.AddIntegerParameter("Geometry", "G", "---", GH_ParamAccess.item, 18);
            pManager[2].Optional = true;

            pManager.AddColourParameter("Highlight Color", "H", "---", GH_ParamAccess.item, Color.Red);
            pManager[3].Optional = true;
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
            int D = 25;
            int G = 18;
            Color Y = Color.Red;

            // Access the input parameters 
            if (!DA.GetData(0, ref V)) return;
            if (!DA.GetData(1, ref D)) return;
            if (!DA.GetData(2, ref G)) return;
            if (!DA.GetData(3, ref Y)) return;

            Bitmap A = null;
            if (V != null) { V.CastTo(out A); }
            Bitmap B = new Bitmap(A);


        List<wPoint> Points = new List<wPoint>();

            mFilter Filter = new mFilter();

            switch (FilterMode)
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


        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Susan", ModeSusan, true, (FilterMode == 0));
            Menu_AppendItem(menu, "Moravec", ModeMoravec, true, (FilterMode == 1));
            
        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("FilterMode", FilterMode);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            FilterMode = reader.GetInt32("FilterMode");

            return base.Read(reader);
        }
        
        private void ModeSusan(Object sender, EventArgs e)
        {
            FilterMode = 0;
            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void ModeMoravec(Object sender, EventArgs e)
        {
            FilterMode = 1;
            this.UpdateMessage();
            this.ExpireSolution(true);
        }
        
        private void UpdateMessage()
        {
            string[] arrMessage = { "Susan", "Moravec" };
            Message = arrMessage[FilterMode];
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
                return Properties.Resources.Macaw_Object_Corners;
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