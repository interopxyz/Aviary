using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Pollen.Collections;
using Wind.Containers;
using Grasshopper.Kernel.Types;
using Wind.Types;

namespace Pollen_GH.Format
{
    public class DataLabel : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Label class.
        /// </summary>
        public DataLabel()
          : base("Label", "Label", "---", "Aviary", "Charting & Data")
        {

        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("DataSet", "D", "---", GH_ParamAccess.item);

            pManager.AddIntegerParameter("Label Alignment", "A", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Leader Alignment", "L", "---", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Angle", "R", "---", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;
            pManager.AddColourParameter("Color", "C", "---", GH_ParamAccess.item, System.Drawing.Color.Transparent);
            pManager[4].Optional = true;
            pManager.AddColourParameter("Stroke Color", "S", "---", GH_ParamAccess.item, System.Drawing.Color.Transparent);
            pManager[5].Optional = true;
            pManager.AddNumberParameter("Weight", "W", "---", GH_ParamAccess.item, 0);
            pManager[6].Optional = true;

            Param_Integer param1 = (Param_Integer)Params.Input[1];
            param1.AddNamedValue("Outside", 0);
            param1.AddNamedValue("Center", 1);
            param1.AddNamedValue("Left", 2);
            param1.AddNamedValue("Right", 3);

            Param_Integer param2 = (Param_Integer)Params.Input[2];
            param2.AddNamedValue("None", 0);
            param2.AddNamedValue("Bottom", 1);
            param2.AddNamedValue("BottomLeft", 2);
            param2.AddNamedValue("BottomRight", 3);
            param2.AddNamedValue("Center", 4);
            param2.AddNamedValue("Left", 5);
            param2.AddNamedValue("Right", 6);
            param2.AddNamedValue("Top", 7);
            param2.AddNamedValue("TopLeft", 8);
            param2.AddNamedValue("TopRight", 9);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            pManager.AddGenericParameter("Label", "L", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IGH_Goo Element = null;
            int X = 0;
            int P = 0;
            double A = 0;
            bool L = true;
            System.Drawing.Color C = System.Drawing.Color.Transparent;
            System.Drawing.Color F = System.Drawing.Color.Transparent;
            double T = 0;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref X)) return;
            if (!DA.GetData(2, ref P)) return;
            if (!DA.GetData(3, ref A)) return;
            if (!DA.GetData(4, ref C)) return;
            if (!DA.GetData(5, ref F)) return;
            if (!DA.GetData(6, ref T)) return;

            wObject W;
            Element.CastTo(out W);

            wLabel CustomLabel = new wLabel();

            wGraphic G = CustomLabel.Graphics;
            
            if (P == 0) { L = false; }

            CustomLabel.HasLeader = L;
            CustomLabel.Graphics = new wGraphic(new wColor(C),G.Foreground,new wColor(F),T);
            CustomLabel.Position = (wLabel.LabelPosition)P;
            CustomLabel.Alignment = (wLabel.LabelAlignment)X;

            CustomLabel.Graphics = G;

            switch (W.Type)
            {
                case "Pollen":

                    switch (W.SubType)
                    {
                        case "DataPoint":
                            DataPt Pt = (DataPt)W.Element;
                            
                            Pt.Graphics.FontObject.Angle = A;
                            
                            Pt.Label.HasLeader = L;
                            Pt.Label.Graphics = new wGraphic(new wColor(C),G.Foreground,new wColor(F),T);
                            Pt.Label.Position = (wLabel.LabelPosition)P;
                            Pt.Label.Alignment = (wLabel.LabelAlignment)X;
                            Pt.CustomLabels += 1;

                            W.Element = Pt;
                            break;
                        case "DataSet":
                            DataSetCollection St = (DataSetCollection)W.Element;
                            St.SetUniformLabel(CustomLabel,A);

                            St.Graphics.FontObject.Angle = A;

                            St.Label.HasLeader = L;
                            St.Label.Graphics = new wGraphic(new wColor(C), G.Foreground, new wColor(F), T);
                            St.Label.Position = (wLabel.LabelPosition)P;
                            St.Label.Alignment = (wLabel.LabelAlignment)X;

                            W.Element = St;
                            break;
                    }
                    break;
            }

            DA.SetData(0, W);
            DA.SetData(1, CustomLabel);
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Pollen_Labels;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{89cd9015-82db-4851-a682-69c8b373866f}"); }
        }
    }
}