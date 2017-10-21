using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;

using Wind.Types;
using Wind.Containers;

using Parrot.Containers;
using Wind.Geometry.Curves;
using Parrot.Controls;

namespace Wind_GH.Formatting
{
    public class FillGradient : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the FillGradient class.
        /// </summary>
        public FillGradient()
          : base("Gradient", "Gradient", "---", "Aviary", "Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Wind Objects", GH_ParamAccess.item);
            pManager[0].Optional = true;
            pManager.AddColourParameter("Colors", "C", "---", GH_ParamAccess.list, new List<System.Drawing.Color>());
            pManager.AddNumberParameter("Parameters", "P", "---", GH_ParamAccess.list, new List<double>());
            pManager[2].Optional = false;
            pManager.AddIntegerParameter("Types", "T", "---", GH_ParamAccess.item,0);
            pManager[3].Optional = false;
            
            Param_Integer param = (Param_Integer)Params.Input[3];
            param.AddNamedValue("Linear", 0);
            param.AddNamedValue("Radial", 1);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            pManager.AddGenericParameter("Graphics", "G", "Graphics Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IGH_Goo Element = null;

            List<System.Drawing.Color> Colors = new List<System.Drawing.Color>(); ;
            List<double> Parameters = new List<double>();
            int GradientType = 0;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetDataList(1, Colors)) return;
            if (!DA.GetDataList(2, Parameters)) return;
            if (!DA.GetData(3, ref GradientType)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            if (Parameters.Count < 1)
            { 
            G.Gradient = new wGradient(Colors);
            }
            else
            {
                G.Gradient = new wGradient(Colors,Parameters);
            }
            
            W.Graphics = G;

            if (Element != null)
            {
                switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;
                        pControl C = (pControl)E.ParrotControl;

                        C.Graphics = G;
                        break;
                case "Pollen":
                    switch (W.SubType)
                    {
                        case "DataPoint":
                            break;
                        case "DataSet":
                            break;
                    }
                    break;
                    case "Hoopoe":
                        wShapeCollection Shapes = (wShapeCollection)W.Element;

                        if (Parameters.Count < 1)
                        {
                            Shapes.Graphics.Gradient = new wGradient(Colors);
                        }
                        else
                        {
                            Shapes.Graphics.Gradient = new wGradient(Colors, Parameters);
                        }

                        W.Element = Shapes;
                        break;
                }
            }

            DA.SetData(0, W);
            DA.SetData(1, G);

        }


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
                return Properties.Resources.Parrot_Gradient;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{666b9109-9195-4a65-8fa3-b590e59d78fd}"); }
        }
    }
}