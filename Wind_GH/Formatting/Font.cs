using System;

using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;

using Wind.Types;
using Wind.Containers;

using Parrot.Containers;

using Pollen.Collections;
using Wind.Geometry.Curves;
using Parrot.Controls;
using Wind.Utilities;
using Parrot.Displays;
using Pollen.Charts;

namespace Wind_GH.Formatting
{
    public class Font : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Font class.
        /// </summary>
        public Font()
          : base("Font Advanced", "Font", "---", "Aviary", "2D Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            pManager.AddTextParameter("Name", "N", "---", GH_ParamAccess.item, "Arial");
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Size", "S", "---", GH_ParamAccess.item, 8);
            pManager[2].Optional = true;
            pManager.AddColourParameter("Color", "C", "---", GH_ParamAccess.item, System.Drawing.Color.Black);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("Justification", "J", "The justificaiton of the text" + System.Environment.NewLine +
      "  0 = Top Left" + System.Environment.NewLine +
      "  1 = Top Center" + System.Environment.NewLine +
      "  2 = Top Right" + System.Environment.NewLine +
      "  3 = Middle Left" + System.Environment.NewLine +
      "  4 = Middle Center" + System.Environment.NewLine +
      "  5 = Middle Right" + System.Environment.NewLine +
      "  6 = Bottom Left" + System.Environment.NewLine +
      "  7 = Bottom Center" + System.Environment.NewLine +
      "  8 = Bottom Right"
      , GH_ParamAccess.item, 0);
            pManager[4].Optional = true;
            pManager.AddBooleanParameter("Bold", "B", "---", GH_ParamAccess.item, false);
            pManager[5].Optional = true;
            pManager.AddBooleanParameter("Italic", "I", "---", GH_ParamAccess.item, false);
            pManager[6].Optional = true;
            pManager.AddBooleanParameter("Underline", "U", "---", GH_ParamAccess.item, false);
            pManager[7].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[4];
            param.AddNamedValue("Top Left", 0);
            param.AddNamedValue("Top Center", 1);
            param.AddNamedValue("Top Right", 2);
            param.AddNamedValue("Middle Left", 3);
            param.AddNamedValue("Middle Center", 4);
            param.AddNamedValue("Middle Right", 5);
            param.AddNamedValue("Bottom Left", 6);
            param.AddNamedValue("Bottom Center", 7);
            param.AddNamedValue("Bottom Right", 8);

            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.PersistentData.Append(new GH_ObjectWrapper(new pSpacer(new GUIDtoAlpha(Convert.ToString(this.Attributes.InstanceGuid.ToString() + Convert.ToString(this.RunCount)), false).Text)));
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            pManager.AddGenericParameter("Font", "F", "Font Object", GH_ParamAccess.item);
            pManager.AddGenericParameter("Graphics", "G", "Graphics Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IGH_Goo Element = null;
            string N = "Arial";
            double S = 8;
            System.Drawing.Color X = System.Drawing.Color.Black;
            int J = 0;
            bool B = false;
            bool I = false;
            bool U = false;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref N)) return;
            if (!DA.GetData(2, ref S)) return;
            if (!DA.GetData(3, ref X)) return;
            if (!DA.GetData(4, ref J)) return;
            if (!DA.GetData(5, ref B)) return;
            if (!DA.GetData(6, ref I)) return;
            if (!DA.GetData(7, ref U)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            wFont F = new wFont(N, S, new wColor(X), (wFontBase.Justification)J, B, I, U, false);
            G.FontObject = F;

            W.Graphics = G;

            switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;
                    pControl C = (pControl)E.ParrotControl;

                    C.Graphics = G;
                    C.SetFont();
                    break;
                case "Pollen":
                    switch (W.SubType)
                    {
                        case "DataPoint":
                            DataPt tDataPt = (DataPt)W.Element;
                            tDataPt.Graphics = G;

                            W.Element = tDataPt;
                            break;
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;
                            tDataSet.Graphics = G;

                            tDataSet.TotalCustomTitles += 1;

                            W.Element = tDataSet;
                            break;
                        case "Chart":
                        case "Table":
                            pElement pE = (pElement)W.Element;
                            pChart pC = pE.PollenControl;
                            pC.Graphics = G;

                            pC.SetFont();

                            pE.PollenControl = pC;
                            W.Element = pE;
                            break;
                    }
                    break;
                case "Hoopoe":
                    wShapeCollection Shapes = (wShapeCollection)W.Element;

                    Shapes.Fonts = F;

                    W.Element = Shapes;
                    break;
            }

            DA.SetData(0, W);
            DA.SetData(1, G.FontObject);
            DA.SetData(2, G);
        }


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
                return Properties.Resources.Wind_Font_Adv;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{efe779ad-f333-4205-b0cc-cd502c482bc3}"); }
        }
    }
}