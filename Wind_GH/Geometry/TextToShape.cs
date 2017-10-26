using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Geometry.Curves;
using Wind.Geometry.Shapes;
using Wind.Geometry.Vectors;
using Wind.Containers;

namespace Wind_GH.Geometry
{
    public class TextToShape : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the TextToShape class.
        /// </summary>
        public TextToShape()
          : base("TextToShape", "Tshp", "---", "Aviary", "Shape")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Text", "T", "---", GH_ParamAccess.item);
            pManager.AddPlaneParameter("Plane", "P", "---", GH_ParamAccess.item);
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Shape", "SH", "Shape", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string T = "";
            Plane P = Plane.WorldXY;
            if (!DA.GetData(0, ref T)) return;
            if (!DA.GetData(1, ref P)) return;

            wPlane Pln = new wPlane(new wPoint(P.Origin.X,P.Origin.Y,P.Origin.Z),new wVector(P.XAxis.X, P.XAxis.Y, P.XAxis.Z),new wVector(P.YAxis.X, P.YAxis.Y, P.YAxis.Z));
                
            wText Txt = new wText(T);
            wTextObject TxtObj = new wTextObject(Txt, Pln);

            wShape Shape = new wShape(TxtObj);
            wShapeCollection Shapes = new wShapeCollection(Shape);

            Shapes.Type = "Text";

            Shapes.Graphics = new wGraphic().BlackFill();
            Shapes.Effects = new wEffects();

            wObject WindObject = new wObject(Shapes, "Hoopoe", Shapes.Type);

            DA.SetData(0, WindObject);

        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Wind_Shape_Text;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{067a2e58-512c-42c6-9758-587a80a7975d}"); }
        }
    }
}