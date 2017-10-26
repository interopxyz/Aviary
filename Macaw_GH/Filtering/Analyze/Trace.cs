using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Wind.Geometry.Vectors;
using Macaw.Filtering;
using Macaw.Analysis;
using Macaw.Build;
using Wind.Containers;
using Grasshopper;
using Grasshopper.Kernel.Data;
using GH_IO.Serialization;
using System.Windows.Forms;

namespace Macaw_GH.Filtering.Analyze
{

    public class Trace : GH_Component
    {

        public bool OptimizeCurve = false;

        public int FilterMode = 0;

        /// <summary>
        /// Initializes a new instance of the Trace class.
        /// </summary>
        public Trace()
          : base("Trace Bitmap", "Trace", "---", "Aviary", "Image Build")
        {
            this.UpdateMessage();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager[0].Optional = false;
            /*
            pManager[0].Optional = true;
            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.PersistentData.Append(new GH_ObjectWrapper(new Bitmap(100, 100)));
            */

            pManager.AddNumberParameter("Threshold", "T", "Threshold (0.0-1.0)", GH_ParamAccess.item, 0.90);
            pManager[1].Optional = true;

            pManager.AddNumberParameter("Alpha", "A", "Threshold for detection of corners (0.0-1.3334)", GH_ParamAccess.item, 1.00);
            pManager[2].Optional = true;
            
            pManager.AddIntegerParameter("Smooth", "S", "Pixel Smoothing Distance", GH_ParamAccess.item, 2);
            pManager[3].Optional = true;


        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddCurveParameter("Curves", "C", "---", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo V = null;
            double T = 0.90;
            double X = 1.00;
            int S = 2;

            // Access the input parameters 
            if (!DA.GetData(0, ref V)) return;
            if (!DA.GetData(1, ref T)) return;
            if (!DA.GetData(2, ref X)) return;
            if (!DA.GetData(3, ref S)) return;

            Bitmap A = null;
            if (V != null) { V.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            
            List<List<wPoint[]>> PointArr = new List<List<wPoint[]>>();
            
            PointArr.Clear();

            mAnalyzePotrace Scorner = new mAnalyzePotrace(A, T, X, 0.2, S, OptimizeCurve, FilterMode);
            PointArr = Scorner.VectorizedPointArray;
            
            List<Polyline> CL = new List<Polyline>();

            foreach (var ptArrList in PointArr)
            {

                List<Point3d> Points = new List<Point3d>();
                PointCloud PtCld = new PointCloud();
                
                foreach (var ptArr in ptArrList)
                {

                    Points.Add(new Point3d(ptArr[0].X, ptArr[0].Y, 0));

                }
                Points.Add(new Point3d(ptArrList[ptArrList.Count-1][1].X, ptArrList[ptArrList.Count - 1][1].Y, 0));

                Polyline Pline = new Polyline(Points.ToArray());
                if (!Pline.IsClosed) { Pline.Add(new Point3d(ptArrList[0][0].X, ptArrList[0][0].Y, 0)); }
                CL.Add(Pline);

            }
            

            DA.SetData(0, B);
            DA.SetDataList(1, CL);
        }


        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Smallest", ModeSmallest, true, (FilterMode == 0));
            Menu_AppendItem(menu, "Largest", ModeLargest, true, (FilterMode == 1));
            Menu_AppendItem(menu, "Direction", ModeDirection, true, (FilterMode == 2));
            Menu_AppendItem(menu, "Dark", ModeDark, true, (FilterMode == 3));
            Menu_AppendItem(menu, "Light", ModeLight, true, (FilterMode == 4));

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Optimize", SetOptimization, true, OptimizeCurve);




        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("FilterMode", FilterMode);
            writer.SetBoolean("Optimize", OptimizeCurve);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            FilterMode = reader.GetInt32("FilterMode");
            OptimizeCurve = reader.GetBoolean("Optimize");

            return base.Read(reader);
        }


        private void ModeSmallest(Object sender, EventArgs e)
        {
            FilterMode = 0;
            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void ModeLargest(Object sender, EventArgs e)
        {
            FilterMode = 1;
            this.UpdateMessage();
            this.ExpireSolution(true);
        }


        private void ModeDirection(Object sender, EventArgs e)
        {
            FilterMode = 2;
            this.UpdateMessage();
            this.ExpireSolution(true);
        }
        private void ModeDark(Object sender, EventArgs e)
        {
            FilterMode = 3;
            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void ModeLight(Object sender, EventArgs e)
        {
            FilterMode = 4;
            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void SetOptimization(Object sender, EventArgs e)
        {
            OptimizeCurve = !OptimizeCurve;
            this.UpdateMessage();
            this.ExpireSolution(true);
        }


        private void UpdateMessage()
        {
            string[] arrMessage = { "Smallest", "Largest", "Direction", "Dark", "Light" };
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
                return Properties.Resources.Macaw_Object_Trace;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("a52f640e-9ee9-4f74-8465-dc52d87032a1"); }
        }
    }
}