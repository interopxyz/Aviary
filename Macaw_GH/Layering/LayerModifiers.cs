using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System.Collections.Generic;
using Wind.Containers;
using Macaw.Compiling;
using System.Drawing;
using Grasshopper.Kernel.Parameters;
using Macaw.Compiling.Modifiers;

namespace Macaw_GH.Compose
{
    public class Modifiers : GH_Component
    {
        public int[] ModeIndex = { 0 };
        string ID = "";
        private string[] modes = {"Invert", "Solarize", "Grayscale","Vignette", "Emboss", "Brightness", "Contrast", "Feather", "Gaussian","Border", "ColorTint"};
        /// <summary>
        /// Initializes a new instance of the Filters class.
        /// </summary>
        public Modifiers()
          : base("Modify Layer", "Modify", "---", "Aviary", "Bitmap Build")
        {
            UpdateMessage();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.list, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Value", "T", "---", GH_ParamAccess.list, 0.0);
            pManager[2].Optional = true;
            pManager.AddColourParameter("Color", "C", "---", GH_ParamAccess.list, Color.Black);
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue(modes[0], 0);
            param.AddNamedValue(modes[1], 1);
            param.AddNamedValue(modes[2], 2);
            param.AddNamedValue(modes[3], 3);
            param.AddNamedValue(modes[4], 4);
            param.AddNamedValue(modes[5], 5);
            param.AddNamedValue(modes[6], 6);
            param.AddNamedValue(modes[7], 7);
            param.AddNamedValue(modes[8], 8);
            param.AddNamedValue(modes[9], 9);
            param.AddNamedValue(modes[10], 10);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("T", "T", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo X = null;
            List<int> M = new List<int>();
            List<double> V = new List<double>();
            List<Color> C = new List<Color>();

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetDataList(1, M)) return;
            if (!DA.GetDataList(2, V)) return;
            if (!DA.GetDataList(3, C)) return;

            int total = M.Count;
            if (V.Count > total) { total = V.Count; }
            if (C.Count > total) { total = C.Count; }

            int i = 0;
            int j = M.Count;

            for (i=j;i<total;i++)
            {
                M.Add(M[j - 1]);
            }

            j = V.Count;
            for (i = j; i < total; i++)
            {
                V.Add(V[j - 1]);
            }

            j = C.Count;
            for (i = j; i < total; i++)
            {
                C.Add(C[j - 1]);
            }

            wObject Z = new wObject();
            if (X != null) { X.CastTo(out Z); }
            mLayer L = new mLayer((mLayer)Z.Element);

            List<mModifier> modifiers = new List<mModifier>();
            foreach (mModifier mm in L.Modifiers) modifiers.Add(mm);

            int[] Ma = M.ToArray();

            string tID = String.Join("~", Ma);

            if (tID != ID)
            {
                Array.Sort(Ma);
                Array.Reverse(Ma);
                int Mi = Ma[0];

                ModeIndex = Ma;
                UpdateMessage();

                if (Mi < 4) { Params.Input[2].NickName = "-"; } else { Params.Input[2].NickName = "V"; }
                if (Mi < 9) { Params.Input[3].NickName = "-"; } else { Params.Input[3].NickName = "C"; }
                if (Mi < 9) { Params.Input[3].Description = "Not used by this filter"; } else { Params.Input[3].Description = "Color"; }

            }

            //L.Modifiers.Clear();

            for (i = 0; i < M.Count; i++)
            {
                mModifier modifier = null;
                switch (M[i])
                {
                    case 0://Invert
                        modifier = new mModifyInvert();
                        break;
                    case 1://Solarize
                        modifier = new mModifySolarize();
                        break;
                    case 2://Grayscale
                        modifier = new mModifyGrayscale();
                        break;
                    case 3://Vignette
                        modifier = new mModifyVignette();
                        break;
                    case 4://Emboss
                        modifier = new mModifyEmboss((float)V[i]);
                        break;
                    case 5://Brightness
                        modifier = new mModifyBrightness((int)V[i]);
                        break;
                    case 6://Contrast
                        modifier = new mModifyContrast((int)V[i]);
                        break;
                    case 7://Feather
                        modifier = new mModifyFeather((int)V[i]);
                        break;
                    case 8://Gaussian
                        modifier = new mModifyGaussian(1+(int)V[i]);
                        break;
                    case 9://Border
                        modifier = new mModifyGaussian((int)V[i]);
                        break;
                    case 10://Tint
                        modifier = new mModifyColorTint(new Wind.Types.wColor(C[i].A, C[i].R, C[i].G, C[i].B), (int)V[i]);
                        break;
                }
                modifiers.Add(modifier);
            }

            L.Modifiers = modifiers;

            wObject W = new wObject(L, "Macaw", L.Type);


            DA.SetData(0, W);
            DA.SetData(1, L.Modifiers.Count);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void UpdateMessage()
        {
            int i =0;
            string title = "";
            foreach(int mi in ModeIndex)
            {
                title+=i+": "+modes[mi] + Environment.NewLine;
                i ++;
            }

            Message = title;
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Build_Modifiers;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("063c542c-cfa2-466f-b108-1bda03b17cd4"); }
        }
    }
}