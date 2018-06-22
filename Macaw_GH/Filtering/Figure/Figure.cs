using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Wind.Containers;
using Macaw.Filtering;
using Macaw.Filtering.Objects.Erosions;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;
using Macaw.Filtering.Stylized;
using Macaw.Filtering.Objects.Figures;
using Rhino.Geometry;
using Wind.Types;
using System.Windows.Forms;
using GH_IO.Serialization;

namespace Macaw_GH.Filtering.Figure
{
    public class Figure : GH_Component, IGH_VariableParameterComponent
    {
        private int ModeIndex = 0;
        private string[] modes = { "Simple", "Dilation", "Opening", "Closing", "Hat Top", "Hat Bottom", "Skeleton", "Streak Horizontal", "Streak Vertical", "Fill", "Filter", "Unique" };

        /// <summary>
        /// Initializes a new instance of the Erosion class.
        /// </summary>
        public Figure()
          : base("Figure", "Figure", "...", "Aviary", "Bitmap Edit")
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
            int I = 0;
            int J = 0;
            bool C = true;
            bool D = true;
            Interval X = new Interval(0, 20);
            Interval Y = new Interval(0, 20);


            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;

            Bitmap A = new Bitmap(10, 10);
            if (Z != null) { Z.CastTo(out A); }

            mFilter Filter = new mFilter();

            switch (ModeIndex)
            {
                case 0:
                    Filter = new mErosionSimple();
                    break;
                case 1:
                    Filter = new mErosionDilation();
                    break;
                case 2:
                    Filter = new mErosionOpening();
                    break;
                case 3:
                    Filter = new mErosionClosing();
                    break;
                case 4:
                    Filter = new mErosionHatTop();
                    break;
                case 5:
                    Filter = new mErosionHatBottom();
                    break;
                case 6:
                    if (!DA.GetData(1, ref I)) return;
                    if (!DA.GetData(2, ref J)) return;
                    Filter = new mErosionSkeleton(I,J);
                    break;
                case 7:
                    if (!DA.GetData(1, ref I)) return;
                    if (!DA.GetData(2, ref C)) return;
                    Filter = new mStreakHorizontal(I, C);
                    break;
                case 8:
                    if (!DA.GetData(1, ref I)) return;
                    if (!DA.GetData(2, ref C)) return;
                    Filter = new mStreakVertical(I, C);
                    break;
                case 9:
                    if (!DA.GetData(1, ref I)) return;
                    if (!DA.GetData(2, ref J)) return;
                    if (!DA.GetData(3, ref C)) return;
                    Filter = new mErosionFill(I, J, C);
                    break;
                case 10:
                    if (!DA.GetData(1, ref X)) return;
                    if (!DA.GetData(2, ref Y)) return;
                    if (!DA.GetData(3, ref C)) return;
                    Filter = new mFigureFilter(new wDomain(X.T0, X.T1), new wDomain(Y.T0, Y.T1), C);
                    break;
                case 11:
                    if (!DA.GetData(1, ref X)) return;
                    if (!DA.GetData(2, ref Y)) return;
                    if (!DA.GetData(3, ref C)) return;
                    if (!DA.GetData(3, ref D)) return;
                    Filter = new mFigureUnique(new wDomain(X.T0, X.T1), new wDomain(Y.T0, Y.T1), C, D);
                    break;
            }

            Bitmap B = new mApply(A, Filter).ModifiedBitmap;
            wObject W = new wObject(Filter, "Macaw", Filter.Type);


            DA.SetData(0, B);
            DA.SetData(1, W);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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
            Menu_AppendItem(menu, modes[11], ModeL, true, ModeIndex == 11);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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
            ClearInputs();

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeC(Object sender, EventArgs e)
        {
            ModeIndex = 2;
            ClearInputs();

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeD(Object sender, EventArgs e)
        {
            ModeIndex = 3;
            ClearInputs();

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeE(Object sender, EventArgs e)
        {
            ModeIndex = 4;
            ClearInputs();

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeF(Object sender, EventArgs e)
        {
            ModeIndex = 5;
            ClearInputs();

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeG(Object sender, EventArgs e)
        {
            ModeIndex = 6;

            ClearInputs();
            paramInteger(1, "Background", "B", "---", GH_ParamAccess.item, 0);
            paramInteger(2, "Foreground", "F", "---", GH_ParamAccess.item, 255);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeH(Object sender, EventArgs e)
        {
            ModeIndex = 7;

            ClearInputs(3);
            paramInteger(1, "Gap Size", "G", "---", GH_ParamAccess.item, 10);
            paramBoolean(2, "Borders", "B", "---", GH_ParamAccess.item, false);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeI(Object sender, EventArgs e)
        {
            ModeIndex = 8;

            ClearInputs(3);
            paramInteger(1, "Gap Size", "G", "---", GH_ParamAccess.item, 10);
            paramBoolean(2, "Borders", "B", "---", GH_ParamAccess.item, false);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeJ(Object sender, EventArgs e)
        {
            ModeIndex = 9;

            ClearInputs(4);
            paramInteger(1, "Width", "W", "---", GH_ParamAccess.item, 10);
            paramInteger(2, "Height", "H", "---", GH_ParamAccess.item, 10);
            paramBoolean(3, "Coupled", "C", "---", GH_ParamAccess.item, false);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeK(Object sender, EventArgs e)
        {
            ModeIndex = 10;

            ClearInputs(4);
            paramInterval(1, "Width", "W", "---", GH_ParamAccess.item, new Interval(0,20));
            paramInterval(2, "Height", "H", "---", GH_ParamAccess.item, new Interval(0, 20));
            paramBoolean(3, "Coupled", "C", "---", GH_ParamAccess.item, false);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeL(Object sender, EventArgs e)
        {
            ModeIndex = 11;

            ClearInputs(5);
            paramInterval(1, "Width", "W", "---", GH_ParamAccess.item, new Interval(0, 20));
            paramInterval(2, "Height", "H", "---", GH_ParamAccess.item, new Interval(0, 20));
            paramBoolean(3, "Coupled", "C", "---", GH_ParamAccess.item, false);
            paramBoolean(3, "Blobs", "B", "---", GH_ParamAccess.item, false);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
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

        private void paramBoolean(int index, string name, string nickName, string description, GH_ParamAccess access, bool Value)
        {
            if ((Params.Input.Count - 1) < index)
            {
                Params.RegisterInputParam(new Param_Boolean(), index);
                Params.OnParametersChanged();
            }
            else
            {
                if (Params.Input[index].GetType() != new Param_Boolean().GetType())
                {
                    Params.Input[index].RemoveAllSources();
                    Params.Input[index] = new Param_Boolean();
                    Params.OnParametersChanged();
                }
            }

            Params.Input[index].ClearData();

            Param_Boolean param = (Param_Boolean)Params.Input[index];
            param.PersistentData.ClearData();
            param.PersistentData.Clear();
            param.SetPersistentData(Value);
            SetParamProperties(index, name, nickName, description, access);

        }

        private void paramInterval(int index, string name, string nickName, string description, GH_ParamAccess access, Interval Value)
        {
            if ((Params.Input.Count - 1) < index)
            {
                Params.RegisterInputParam(new Param_Interval(), index);
                Params.OnParametersChanged();
            }
            else
            {
                if (Params.Input[index].GetType() != new Param_Interval().GetType())
                {
                    Params.Input[index].RemoveAllSources();
                    Params.Input[index] = new Param_Interval();
                    Params.OnParametersChanged();
                }
            }

            Params.Input[index].ClearData();

            Param_Interval param = (Param_Interval)Params.Input[index];
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

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.quarternary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Filter_Extract;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("7b0080e9-9aaa-4e81-afe5-894ac119f142"); }
        }
    }
}