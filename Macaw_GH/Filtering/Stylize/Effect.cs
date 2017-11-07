using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Wind.Containers;
using Wind.Types;
using Macaw.Filtering;
using Macaw.Filtering.Stylized;
using Macaw.Compiling;
using Macaw.Compiling.Modifiers;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;

namespace Macaw_GH.Filtering.Stylize
{
    public class Effect : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Effect class.
        /// </summary>
        public Effect()
          : base("Effect", "Effect", "---", "Aviary", "Bitmap Edit")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager[0].Optional = true;
            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.PersistentData.Append(new GH_ObjectWrapper(new Bitmap(100, 100)));

            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Width", "W", "---", GH_ParamAccess.item, 5);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Height", "H", "---", GH_ParamAccess.item, 5);
            pManager[3].Optional = true;
            pManager.AddIntervalParameter("Amplitude", "A", "---", GH_ParamAccess.item, new Interval(5,10));
            pManager[4].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Jitter", 0);
            param.AddNamedValue("Daube", 1);
            param.AddNamedValue("Pixelate", 2);
            param.AddNamedValue("Posterize", 3);
            param.AddNamedValue("Ripple", 4);
            param.AddNamedValue("*Emboss", 5);
            param.AddNamedValue("*Solarize", 6);
            param.AddNamedValue("*Feather", 7);
            param.AddNamedValue("*Vignette", 8);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Filter", "F", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Modifier", "M", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo V = null;
            int M = 0;
            double X = 5;
            double Y = 5;
            Interval Z = new Interval(5, 10);

            // Access the input parameters 
            if (!DA.GetData(0, ref V)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref X)) return;
            if (!DA.GetData(3, ref Y)) return;
            if (!DA.GetData(4, ref Z)) return;

            Bitmap A = null;
            if (V != null) { V.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            wDomain D = new wDomain(Z.T0, Z.T1);

            mFilter Filter = new mFilter();
            mModifiers Modifier = new mModifiers();

            switch (M)
            {
                case 0:
                    Filter = new mEffectJitter((int)X);
                    B = new mApply(A, Filter).ModifiedBitmap;
                    break;
                case 1:
                    Filter = new mEffectDaube((int)X);
                    B = new mApply(A, Filter).ModifiedBitmap;
                    break;
                case 2:
                    Filter = new mEffectPixelate((int)X,(int)Y);
                    B = new mApply(A, Filter).ModifiedBitmap;
                    break;
                case 3:
                    Filter = new mEffectPosterization((byte)X);
                    B = new mApply(A, Filter).ModifiedBitmap;
                    break;
                case 4:
                    Filter = new mEffectRipple(D,(int)X,(int)Y);
                    B = new mApply(A, Filter).ModifiedBitmap;
                    break;
                case 5:
                    Modifier = new mModifyEmboss((float)X);
                    B = new Bitmap(new mQuickComposite(A, Modifier).ModifiedBitmap);
                    break;
                case 6:
                    Modifier = new mModifySolarize();
                    B = new Bitmap(new mQuickComposite(A, Modifier).ModifiedBitmap);
                    break;
                case 7:
                    Modifier = new mModifyFeather((int)X);
                    B = new Bitmap(new mQuickComposite(A, Modifier).ModifiedBitmap);
                    break;
                case 8:
                    Modifier = new mModifyVignette();
                    B = new Bitmap(new mQuickComposite(A, Modifier).ModifiedBitmap);
                    break;
            }
            
            wObject W = new wObject(Filter, "Macaw", Filter.Type);
            wObject U = new wObject(Modifier, "Macaw", Modifier.Type);


            DA.SetData(0, B);
            DA.SetData(1, U);
            DA.SetData(2, W);
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
                return Properties.Resources.Macaw_Effect_Effect;
            }
        }


        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("01fa1cc3-016e-4199-8662-d4accf9b2507"); }
        }
    }
}