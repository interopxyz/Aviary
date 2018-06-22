using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Windows.Forms;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel.Parameters;
using Grasshopper.GUI;
using Macaw.Filtering.Stylized;
using Macaw.Filtering;
using System.Drawing;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using GH_IO.Serialization;
using Macaw.Build;

namespace Macaw_GH.Filtering.Stylize
{
    public class Sharpen : GH_Component, IGH_VariableParameterComponent
    {
        private int ModeIndex = 0;
        private string[] modes = { "+ Simple", "+ Gaussian", "+ High Boost", "- Adaptive", "- Bilateral", "- Conservative", "- Mean", "- Median" };

        /// <summary>
        /// Initializes a new instance of the Temp class.
        /// </summary>
        public Sharpen()
          : base("(Un)Sharpen", "(Un)Sharpen", "---", "Aviary", "Bitmap Edit")
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
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
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
                case 0:
                    Filter = new mSharpenSimple();
                    break;
                case 1:
                    int K1 = 11;
                    double S1 = 4;
                    if (!DA.GetData(1, ref K1)) return;
                    if (!DA.GetData(2, ref S1)) return;
                    Filter = new mSharpenGaussian(K1, S1);
                    break;
                case 2:
                    int B2 = 10;
                    if (!DA.GetData(1, ref B2)) return;
                    Filter = new mSharpenHighBoost(B2);
                    break;
                case 3:
                    int F3 = 1;
                    if (!DA.GetData(1, ref F3)) return;
                    Filter = new mSmoothAdaptive(F3);
                    break;
                case 4:
                    int K4 = 7;
                    double S4 = 10;
                    double F4 = 60;
                    double P4 = 0.5;
                    if (!DA.GetData(1, ref K4)) return;
                    if (!DA.GetData(2, ref S4)) return;
                    if (!DA.GetData(3, ref F4)) return;
                    if (!DA.GetData(4, ref P4)) return;
                    Filter = new mSmoothBilateral(K4, S4, F4, P4);
                    break;
                case 5:
                    Filter = new mSmoothConservative();
                    break;
                case 6:
                    int D6 = 9;
                    if (!DA.GetData(1, ref D6)) return;
                    Filter = new mSmoothMean(D6);
                    break;
                case 7:
                    int K7 = 3;
                    if (!DA.GetData(1, ref K7)) return;
                    Filter = new mSmoothMedian(K7);
                    break;
            }

            Bitmap B = new mApply(A, Filter).ModifiedBitmap;
            wObject W = new wObject(Filter, "Macaw", Filter.Type);

            DA.SetData(0, B);
            DA.SetData(1, W);
        }
        
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, modes[0], ModeA, true, ModeIndex == 0);
            Menu_AppendItem(menu, modes[1], ModeB, true, ModeIndex == 1);
            Menu_AppendItem(menu, modes[2], ModeD, true, ModeIndex == 2);

            Menu_AppendItem(menu, modes[3], ModeE, true, ModeIndex == 3);
            Menu_AppendItem(menu, modes[4], ModeF, true, ModeIndex == 4);
            Menu_AppendItem(menu, modes[5], ModeG, true, ModeIndex == 5);
            Menu_AppendItem(menu, modes[6], ModeH, true, ModeIndex == 6);
            Menu_AppendItem(menu, modes[7], ModeI, true, ModeIndex == 7);
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void ModeA(Object sender, EventArgs e)//Simple
        {
            ModeIndex = 0;

            ClearInputs();

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeG(Object sender, EventArgs e)//Conservative
        {
            ModeIndex = 5;

            ClearInputs();

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeD(Object sender, EventArgs e)//High Boost
        {
            ModeIndex = 2;

            ClearInputs(2);

            paramInteger(1, "Boost", "B", "---", GH_ParamAccess.item, 10);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeE(Object sender, EventArgs e)//Adaptive
        {
            ModeIndex = 3;

            ClearInputs(2);

            paramInteger(1, "Factor", "F", "---", GH_ParamAccess.item, 1);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeH(Object sender, EventArgs e)//Mean
        {
            ModeIndex = 6;

            ClearInputs(2);

            paramInteger(1, "Divisor", "D", "---", GH_ParamAccess.item, 9);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeI(Object sender, EventArgs e)//Median
        {
            ModeIndex = 7;

            ClearInputs(2);

            paramInteger(1, "Kernal Size", "K", "---", GH_ParamAccess.item, 3);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeB(Object sender, EventArgs e)//Gaussian
        {
            ModeIndex = 1;

            ClearInputs(3);

            paramInteger(1, "Kernal Size", "K", "---", GH_ParamAccess.item, 11);
            paramNumber(2, "Sigma", "X", "---", GH_ParamAccess.item, 4);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeF(Object sender, EventArgs e)//Bilateral
        {
            ModeIndex = 4;

            ClearInputs(5);

            paramInteger(1, "Kernal Size", "K", "---",GH_ParamAccess.item, 5);
            paramNumber(2, "Spatial Factor", "S", "---", GH_ParamAccess.item, 10.0);
            paramNumber(3, "Color Factor", "F", "---", GH_ParamAccess.item, 60.0);
            paramNumber(4, "Color Power", "P", "---", GH_ParamAccess.item, 0.5);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void ClearInputs(int clearFrom = 1)
        {
            int j = Params.Input.Count - 1;

            for (int i = clearFrom - 1; i < j; i++)
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


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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
            get { return new Guid("9d6c550d-934e-439d-adc0-1a1be97d6a60"); }
        }

    }


}