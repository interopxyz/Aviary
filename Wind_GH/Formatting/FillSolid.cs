using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Types;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Parrot.Containers;
using Parrot.Controls;
using Pollen.Collections;
using Wind.Geometry.Curves;
using Wind.Graphics;
using Pollen.Charts;
using Wind.Presets;
using Grasshopper.Kernel.Parameters;
using Wind.Utilities;
using Parrot.Displays;

namespace Wind_GH.Formatting
{
    public class FillSolid : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the FillSolid class.
        /// </summary>
        public FillSolid()
          : base("Fill", "Fill", "---", "Aviary", "2D Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Wind Objects", GH_ParamAccess.item);
            pManager.AddColourParameter("Color", "C", "---", GH_ParamAccess.item, wColors.VeryLightGray.ToDrawingColor());
            pManager[1].Optional = true;
            
            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.PersistentData.Append(new GH_ObjectWrapper(new pSpacer(new GUIDtoAlpha(Convert.ToString(this.Attributes.InstanceGuid.ToString() + Convert.ToString(this.RunCount)), false).Text)));
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
            System.Drawing.Color Background = wColors.VeryLightGray.ToDrawingColor();

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref Background)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            G.FillType = wGraphic.FillTypes.Solid;

            G.Background = new wColor(Background);
            G.Foreground = new wColor(Background);

            G.WpfFill = new wFillSolid(G.Background).FillBrush;
            G.CustomFills += 1;

            W.Graphics = G;

            switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;
                    pControl C = (pControl)E.ParrotControl;
                    C.Graphics = G;

                    C.SetFill();

                    break;
                case "Pollen":
                    switch (W.SubType)
                    {
                        case "DataPoint":
                            DataPt tDataPt = (DataPt)W.Element;
                            tDataPt.Graphics = G;

                            tDataPt.Graphics.WpfFill = G.WpfFill;
                            tDataPt.Graphics.WpfPattern = G.WpfPattern;

                            W.Element = tDataPt;
                            break;
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;
                            tDataSet.Graphics = G;

                            tDataSet.Graphics.WpfFill = G.WpfFill;
                            tDataSet.Graphics.WpfPattern = G.WpfPattern;

                            W.Element = tDataSet;
                            break;
                        case "Chart":
                        case "Table":

                            pElement pE = (pElement)W.Element;
                            pChart pC = pE.PollenControl;
                            pC.Graphics = G;

                            pC.SetSolidFill();

                            pE.PollenControl = pC;
                            W.Element = pE;
                            break;
                    }
                    break;
                case "Hoopoe":
                    wShapeCollection Shapes = (wShapeCollection)W.Element;
                    Shapes.Graphics.FillType = wGraphic.FillTypes.Solid;
                    Shapes.Graphics.WpfFill = G.WpfFill;

                    Shapes.Graphics.Background = new wColor(Background);
                    Shapes.Graphics.Foreground = new wColor(Background);
                    
                    W.Element = Shapes;
                    break;
            }

            DA.SetData(0, W);
            DA.SetData(1, G);
        }

        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Wind_Fill_Solid;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("a1e55fc7-69e8-476c-ad25-31cfd39861cc"); }
        }

        public object PatternWeight { get; private set; }
    }
}