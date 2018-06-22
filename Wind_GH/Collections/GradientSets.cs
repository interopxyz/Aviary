using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Types;
using Wind.Containers;
using Wind.Presets;
using Grasshopper.Kernel.Parameters;

namespace Wind_GH.Collections
{
    public class GradientSets : GH_Component
    {

        wGradient SelectedGradient = new wGradient();
        List<wGradient> GradientSet = new List<wGradient>
            {
            wGradients.Metro,
            wGradients.Vidris,
            wGradients.Inferno,
            wGradients.Magma,
            wGradients.Plasma,
            wGradients.Cyclical,
            wGradients.Spectral,
            wGradients.Cool,
            wGradients.Warm,
            wGradients.Jet,
            wGradients.PinkYellowGreen
        };

        /// <summary>
        /// Initializes a new instance of the Gradients class.
        /// </summary>
        public GradientSets()
          : base("Gradient Presets", "Gradients", "---", "Aviary", "Presets")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Gradient", "G", "---", GH_ParamAccess.item, 0);

            Param_Integer param = (Param_Integer)Params.Input[0];
            param.AddNamedValue(GradientSet[0].Name, 0);
            param.AddNamedValue(GradientSet[1].Name, 1);
            param.AddNamedValue(GradientSet[2].Name, 2);
            param.AddNamedValue(GradientSet[3].Name, 3);
            param.AddNamedValue(GradientSet[4].Name, 4);
            param.AddNamedValue(GradientSet[5].Name, 5);
            param.AddNamedValue(GradientSet[6].Name, 6);
            param.AddNamedValue(GradientSet[7].Name, 7);
            param.AddNamedValue(GradientSet[8].Name, 8);
            param.AddNamedValue(GradientSet[9].Name, 9);
            param.AddNamedValue(GradientSet[10].Name, 10);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddColourParameter("Colors", "C", "---", GH_ParamAccess.list);
            pManager.AddNumberParameter("Parameters", "T", "---", GH_ParamAccess.list);
            pManager.AddGenericParameter("Gradient", "GD", "Gradient Object", GH_ParamAccess.item);
            pManager.AddGenericParameter("Graphics", "GR", "Graphics Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int Index = 0;
            if (!DA.GetData(0, ref Index)) return;
            
            SelectedGradient = GradientSet[Index];

            wGraphic G = new wGraphic();
            G.Gradient = SelectedGradient;

            List<System.Drawing.Color> clrs = new List<System.Drawing.Color>();
            foreach (wColor C in SelectedGradient.ColorSet)
            {
                clrs.Add(C.ToDrawingColor());
            }

            DA.SetDataList(0, clrs);
            DA.SetDataList(1, SelectedGradient.ParameterSet);
            DA.SetData(2, SelectedGradient);
            DA.SetData(3, G);

            UpdateMessage();
        }

        private void UpdateMessage()
        {
            Message = SelectedGradient.Name;
        }


        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Wind_Presets_Fonts;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("44f9295b-402a-44eb-9d0b-96fa52bc1fe2"); }
        }
    }
}