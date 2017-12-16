using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Parrot.Containers;
using Wind.Geometry.Curves;
using Wind.Types;
using Wind.Effects;

namespace Wind_GH.Effects
{
    public class DropShadow : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the DropShadow class.
        /// </summary>
        public DropShadow()
          : base("Drop Shadow", "Shadow", "---", "Aviary", "Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Wind Objects", GH_ParamAccess.item);
            pManager.AddNumberParameter("Distance", "D", "---", GH_ParamAccess.item, 5.0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Angle", "A", "---", GH_ParamAccess.item, 315.0);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Radius", "R", "---", GH_ParamAccess.item, 2.0);
            pManager[3].Optional = true;
            pManager.AddColourParameter("Color", "C", "---", GH_ParamAccess.item, System.Drawing.Color.Black);
            pManager[4].Optional = true;

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
            double D = 5.0;
            double A = 315.0;
            double R = 2.0;
            System.Drawing.Color X = new System.Drawing.Color();

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref D)) return;
            if (!DA.GetData(2, ref A)) return;
            if (!DA.GetData(3, ref R)) return;
            if (!DA.GetData(4, ref X)) return;

            wColor C = new wColor(X);

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }

            switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;

                    break;
                case "Pollen":
                    switch (W.SubType)
                    {
                        
                    }
                    break;
                case "Hoopoe":
                    wShapeCollection Shapes = (wShapeCollection)W.Element;

                    Shapes.Effects.DropShadow = new wDropShadow(C, A, D, R, (255.0 / C.A));
                    Shapes.Effects.SetEffect(Shapes.Effects.DropShadow.ShapeEffect);

                    Shapes.Effects.HasEffect = true;

                    W.Element = Shapes;
                    break;
            }

            DA.SetData(0, W);
        }

        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.senary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Wind_Effects_DropShadow;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f4e2c669-c62c-4e1b-b81c-a74f3e0f4081"); }
        }
    }
}