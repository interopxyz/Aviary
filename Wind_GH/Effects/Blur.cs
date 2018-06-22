using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Wind.Types;
using Wind.Containers;
using Parrot.Containers;
using Parrot.Controls;
using Wind.Geometry.Curves;
using Wind.Effects;

namespace Wind_GH.Effects
{
    public class Blur : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Blur class.
        /// </summary>
        public Blur()
          : base("Blur", "Blur", "---", "Aviary", "2D Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Wind Objects", GH_ParamAccess.item);
            pManager.AddNumberParameter("Blur", "R", "---", GH_ParamAccess.item, 10.0);
            pManager[1].Optional = true;

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
            double R = 10.0;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref R)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }

            switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;

                    break;
                case "Pollen":
                    break;
                case "Hoopoe":
                    wShapeCollection Shapes = (wShapeCollection)W.Element;

                    Shapes.Effects.Blur = new wBlur(R);
                    Shapes.Effects.SetEffect(Shapes.Effects.Blur.ShapeEffect);

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
                return Properties.Resources.Wind_Effects_BlurA; 
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("74ead455-b3b7-4d78-8da5-b283266396c0"); }
        }
    }
}