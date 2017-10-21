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

namespace Wind_GH.Formatting
{
    public class FillSolid : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the FillSolid class.
        /// </summary>
        public FillSolid()
          : base("Fill", "Fill", "---", "Aviary", "Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Wind Objects", GH_ParamAccess.item);
            pManager.AddColourParameter("Color", "B", "---", GH_ParamAccess.item, new wColor().VeryLightGray().ToDrawingColor());

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
            System.Drawing.Color Background = new wColor().VeryLightGray().ToDrawingColor();

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref Background)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            G.Background = new wColor(Background);
            G.Foreground = new wColor(Background);
            
            W.Graphics = G;

            switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;
                    pControl C = (pControl)E.ParrotControl;
                    C.Graphics = G;
                    
                            C.SetSolidFill();
                    break;
                case "Pollen":
                    switch (W.SubType)
                    {
                        case "DataPoint":
                            DataPt tDataPt = (DataPt)W.Element;
                            tDataPt.Graphics.Background = new wColor(Background);
                            tDataPt.Graphics.Foreground = new wColor(Background);
                            W.Element = tDataPt;
                            break;
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;
                            tDataSet.Graphics.Background = new wColor(Background);
                            tDataSet.Graphics.Foreground = new wColor(Background);
                            W.Element = tDataSet;
                            break;
                    }
                    break;
                case "Hoopoe":
                    wShapeCollection Shapes = (wShapeCollection)W.Element;
                    Shapes.Graphics.Background = new wColor(Background);
                    Shapes.Graphics.Foreground = new wColor(Background);

                    W.Element = Shapes;
                    break;
            }

            DA.SetData(0, W);
            DA.SetData(1, PatternWeight);
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