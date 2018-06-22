using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using System.Drawing;
using System.Windows.Forms;
using Wind.Containers;
using Grasshopper.Kernel.Types;
using Macaw.Filtering.Stylized;
using Macaw.Filtering;
using GH_IO.Serialization;
using Macaw.Build;

namespace Macaw_GH.Filtering.Stylize
{
    public class Effect : GH_Component, IGH_VariableParameterComponent
    {
        private int ModeIndex = 0;
        private string[] modes = { "Blur", "Jitter", "Daube", "Kuwahara","Additive","Salt&Pepper", "Box Blur", "Gaussian Blur", "Pixelate", "Posterize", "Ripple" }; 

        /// <summary>
        /// Initializes a new instance of the Effect class.
        /// </summary>
        public Effect()
          : base("Effect", "Effect", "---", "Aviary", "Bitmap Edit")
        {
            UpdateMessage();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager[0].Optional = true;

            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.SetPersistentData(new Bitmap(10, 10));
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Filter", "F", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo Z = null;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;

            Bitmap A = new Bitmap(10, 10);
            if (Z != null) { Z.CastTo(out A); }

            mFilter Filter = new mFilter();

            switch (ModeIndex)
            {
                case 0://Blur
                    Filter = new mBlur();
                    break;
                case 1://Jitter
                    int R3 = 10;
                    if (!DA.GetData(1, ref R3)) return;
                    Filter = new mEffectJitter(R3);
                    break;
                case 2://Daube
                    int R4 = 10;
                    if (!DA.GetData(1, ref R4)) return;
                    Filter = new mEffectDaube(R4);
                    break;
                case 3://Kuwahara
                    int R7 = 5;
                    if (!DA.GetData(1, ref R7)) return;
                    Filter = new mEffectKuwahara(R7);
                    break;
                case 4://Additive
                    int R5 = 5;
                    if (!DA.GetData(1, ref R5)) return;
                    Filter = new mNoiseAdditive(new Wind.Types.wDomain(0,R5));
                    break;
                case 5://Salt&Pepper
                    int R6 = 5;
                    if (!DA.GetData(1, ref R6)) return;
                    Filter = new mNoiseSandP(R6);
                    break;
                case 6://Box Blur
                    int H1 = 3;
                    int V1 = 3;
                    if (!DA.GetData(1, ref H1)) return;
                    if (!DA.GetData(2, ref V1)) return;
                    Filter = new mBlurBox((byte)H1, (byte)V1);
                    break;
                case 7://Gaussian Blur
                    double S2 = 4.0;
                    int K2 = 11;
                    if (!DA.GetData(1, ref S2)) return;
                    if (!DA.GetData(2, ref K2)) return;
                    Filter = new mBlurGaussian(S2, K2);
                    break;
                case 8://Pixelate
                    int H8 = 3;
                    int W8 = 3;
                    if (!DA.GetData(1, ref H8)) return;
                    if (!DA.GetData(2, ref W8)) return;
                    Filter = new mEffectPixelate(H8, W8);
                    break;
                case 9://Posterization
                    int D9 = 5;
                    int M9 = 0;
                    if (!DA.GetData(1, ref D9)) return;
                    if (!DA.GetData(2, ref M9)) return;
                    Filter = new mEffectPosterization((byte)D9,M9);
                    break;
                case 10://Ripple
                    int X10 = 10;
                    int Y10 = 5;
                    int H10 = 5;
                    int W10 = 5;
                    if (!DA.GetData(1, ref X10)) return;
                    if (!DA.GetData(2, ref Y10)) return;
                    if (!DA.GetData(3, ref H10)) return;
                    if (!DA.GetData(4, ref W10)) return;
                    Filter = new mEffectRipple(X10,Y10, H10, W10);
                    break;
            }

            Bitmap B = new mApply(A, Filter).ModifiedBitmap;
            wObject W = new wObject(Filter, "Macaw", Filter.Type);

            DA.SetData(0, B);
            DA.SetData(1, W);
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, modes[0], ModeA, true, ModeIndex == 0);
            Menu_AppendItem(menu, modes[1], ModeB, true, ModeIndex == 1);
            Menu_AppendItem(menu, modes[2], ModeC, true, ModeIndex == 2);
            Menu_AppendItem(menu, modes[3], ModeD, true, ModeIndex == 3);

            Menu_AppendItem(menu, modes[4], ModeE, true, ModeIndex == 4);
            Menu_AppendItem(menu, modes[5], ModeF, true, ModeIndex == 5);
            Menu_AppendItem(menu, modes[6], ModeG, true, ModeIndex == 6);
            Menu_AppendItem(menu, modes[7], ModeH, true, ModeIndex == 7);

            Menu_AppendItem(menu, modes[8], ModeI, true, ModeIndex == 8);
            Menu_AppendItem(menu, modes[9], ModeJ, true, ModeIndex == 9);
            Menu_AppendItem(menu, modes[10], ModeK, true, ModeIndex == 10);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void ModeA(Object sender, EventArgs e)
        {
            ModeIndex = 0;
            ClearInputs();

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeB(Object sender, EventArgs e)
        {
            ModeIndex = 1;

            ClearInputs(1);
            paramInteger(1,"Size", "S", "---", GH_ParamAccess.item,10);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeC(Object sender, EventArgs e)
        {
            ModeIndex = 2;

            ClearInputs(1);
            paramInteger(1, "Size", "S", "---", GH_ParamAccess.item, 5);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeD(Object sender, EventArgs e)
        {
            ModeIndex = 3;

            ClearInputs(1);
            paramInteger(1, "Size", "S", "---", GH_ParamAccess.item, 5);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeE(Object sender, EventArgs e)//Additive
        {
            ModeIndex = 4;

            ClearInputs(1);
            paramInteger(1, "Size", "S", "---", GH_ParamAccess.item, 5);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeF(Object sender, EventArgs e)//Salt&Pepper
        {
            ModeIndex = 5;

            ClearInputs(1);
            paramInteger(1, "Size", "S", "---", GH_ParamAccess.item, 5);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeG(Object sender, EventArgs e)
        {
            ModeIndex = 6;

            ClearInputs(2);
            paramInteger(1, "Horizontal Size", "H", "---", GH_ParamAccess.item, 5);
            paramInteger(2, "Vertical Size", "V", "---", GH_ParamAccess.item, 5);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeH(Object sender, EventArgs e)
        {
            ModeIndex = 7;

            ClearInputs(2);
            paramNumber(1, "Sigma", "X", "---", GH_ParamAccess.item, 1.4);
            paramInteger(2, "Kernal Size", "S", "---", GH_ParamAccess.item, 10);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeI(Object sender, EventArgs e)
        {
            ModeIndex = 8;

            ClearInputs(2);
            paramInteger(1, "Horizontal Size", "H", "---", GH_ParamAccess.item, 3);
            paramInteger(2, "Vertical Size", "V", "---", GH_ParamAccess.item, 3);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeJ(Object sender, EventArgs e)
        {
            ModeIndex = 9;

            ClearInputs(2);
            paramInteger(1, "Interval", "I", "---", GH_ParamAccess.item, 5);
            paramInteger(2, "Mode", "M", "---", GH_ParamAccess.item, 0);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeK(Object sender, EventArgs e)
        {
            ModeIndex = 10;

            ClearInputs(4);
            paramInteger(1, "Horizontal Amplitude", "X", "---", GH_ParamAccess.item, 3);
            paramInteger(2, "Vertical Amplitude", "Y", "---", GH_ParamAccess.item, 3);
            paramInteger(3, "Horizontal Count", "H", "---", GH_ParamAccess.item, 3);
            paramInteger(4, "Vertical Count", "V", "---", GH_ParamAccess.item, 3);
            
            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void ClearInputs(int clearFrom = 0)
        {
            int j = Params.Input.Count;

            for (int i = clearFrom; i < j; i++)
            {
                Params.Input[Params.Input.Count - 1].RemoveAllSources();
                Params.Input[Params.Input.Count - 1].ClearData();
                Params.UnregisterInputParameter(Params.Input[Params.Input.Count - 1]);
            }

            Params.OnParametersChanged();

        }

        private void SetParamProperties(int index, string name, string nickName, string description, GH_ParamAccess access)
        {
            Params.Input[index].Name = name;
            Params.Input[index].NickName = nickName;
            Params.Input[index].Description = description;
            Params.Input[index].Access = access;
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        
        private void paramNumber(int index, string name, string nickName, string description, GH_ParamAccess access, double Value)
        {
            if ((Params.Input.Count - 1) < index)
            {
                Params.RegisterInputParam(new Param_Number(), index);
                Params.OnParametersChanged();
            }
            else
            {
                if (Params.Input[index].GetType() != new Param_Number().GetType())
                {
                    Params.Input[index].RemoveAllSources();
                    Params.Input[index] = new Param_Number();
                    Params.OnParametersChanged();
                }
            }

            Params.Input[index].ClearData();

            Param_Number param = (Param_Number)Params.Input[index];
            param.PersistentData.ClearData();
            param.PersistentData.Clear();
            param.SetPersistentData(Value);
            SetParamProperties(index, name, nickName, description, access);

        }

        private void paramInteger(int index, string name, string nickName, string description, GH_ParamAccess access, int Value)
        {
            if ((Params.Input.Count - 1) < index)
            {
                Params.RegisterInputParam(new Param_Integer(), index);
                Params.OnParametersChanged();
            }
            else
            {
                if (Params.Input[index].GetType() != new Param_Integer().GetType())
                {
                    Params.Input[index].RemoveAllSources();
                    Params.Input[index] = new Param_Integer();
                    Params.OnParametersChanged();
                }
            }

            Params.Input[index].ClearData();

            Param_Integer param = (Param_Integer)Params.Input[index];
            param.PersistentData.ClearData();
            param.PersistentData.Clear();
            param.SetPersistentData(Value);
            SetParamProperties(index, name, nickName, description, access);

        }
        
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void UpdateMessage()
        {
            Message = modes[ModeIndex];
        }

        /// <summary>
        /// Adds to the default serialization method to save the current child status so it persists on copy/paste and save/reopen.
        /// </summary>
        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("FilterMode", ModeIndex);

            return base.Write(writer);
        }

        /// <summary>
        /// Adds to the default deserialization method to retrieve the saved child status so it persists on copy/paste and save/reopen.
        /// </summary>
        public override bool Read(GH_IReader reader)
        {
            ModeIndex = reader.GetInt32("FilterMode");

            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.SetPersistentData(new Bitmap(10, 10));

            UpdateMessage();
            return base.Read(reader);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        bool IGH_VariableParameterComponent.CanInsertParameter(GH_ParameterSide side, int index)
        {
            return false;
        }

        bool IGH_VariableParameterComponent.CanRemoveParameter(GH_ParameterSide side, int index)
        {
            return false;
        }

        IGH_Param IGH_VariableParameterComponent.CreateParameter(GH_ParameterSide side, int index)
        {
            return new Param_GenericObject(); ;
        }

        bool IGH_VariableParameterComponent.DestroyParameter(GH_ParameterSide side, int index)
        {
            //Nothing to do here by the moment
            return true;
        }

        void IGH_VariableParameterComponent.VariableParameterMaintenance()
        {
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        
        /// <summary>
        /// The Exposure property control
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
                // You can add image files to your project resources and access them like this:
                return Properties.Resources.Macaw_Filter_Sharpen;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("fee59e18-3aaa-4ca5-a6b3-d760a95abfb4"); }
        }
    }
}