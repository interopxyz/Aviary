using System;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;

using Wind.Containers;
using Wind.Types;

using Pollen.Collections;

namespace Pollen_GH.Format
{
    public class Marker : GH_Component
  {
    /// <summary>
    /// Initializes a new instance of the Marker class.
    /// </summary>
    public Marker()
      : base("Marker", "Mark", "---", "Aviary", "Charting & Data")
    {
    }

    /// <summary>
    /// Registers all the input parameters for this component.
    /// </summary>
    protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
    {
            pManager.AddGenericParameter("DataSet", "D", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 1);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Radius", "R", "---", GH_ParamAccess.item, 10);
            pManager[2].Optional = true;
            pManager.AddColourParameter("Fill Color", "F", "---", GH_ParamAccess.item, System.Drawing.Color.DarkGray);
            pManager[3].Optional = true;
            pManager.AddColourParameter("Stroke Color", "S", "---", GH_ParamAccess.item, System.Drawing.Color.Transparent);
            pManager[4].Optional = true;
            pManager.AddNumberParameter("Weight", "W", "---", GH_ParamAccess.item, 0);
            pManager[5].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("None", 0);
            param.AddNamedValue("Circle", 1);
            param.AddNamedValue("Square", 2);
            param.AddNamedValue("Diamond", 3);
            param.AddNamedValue("Triangle", 4);
            param.AddNamedValue("Cross", 5);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
    {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            pManager.AddGenericParameter("Marker", "M", "Marker Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IGH_Goo Element = null;
            int M = 0;
            int R = 10;
            System.Drawing.Color F = System.Drawing.Color.DarkGray;
            System.Drawing.Color S = System.Drawing.Color.Transparent;
            double T = 0;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref R)) return;
            if (!DA.GetData(3, ref F)) return;
            if (!DA.GetData(4, ref S)) return;
            if (!DA.GetData(5, ref T)) return;

            wObject W;
            Element.CastTo(out W);

            wGraphic G = new wGraphic();
            G.Background = new wColor(F);
            G.StrokeColor = new wColor(S);
            G.SetUniformStrokeWeight(T);

            wMarker CustomMarker = new wMarker((wMarker.MarkerType)M, (int)R, G);

            switch (W.Type)
            {
                case "Pollen":

                    switch (W.SubType)
                    {
                        case "DataPoint":
                            DataPt Pt = (DataPt)W.Element;
                            Pt.SetMarker(CustomMarker);

                            W.Element = Pt;
                            break;
                        case "DataSet":
                            DataSetCollection St = (DataSetCollection)W.Element;
                            St.SetUniformMarkers(CustomMarker);
                            
                            W.Element = St;
                            break;
                    }
                    break;
            }

            DA.SetData(0, W);
            DA.SetData(1, CustomMarker);
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
                return Properties.Resources.Pollen_Marker;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
    {
      get { return new Guid("{c89bda1b-87b9-434f-aea1-b3e1f25325dd}"); }
    }
  }
}