using System;

using Grasshopper.Kernel;
using Wind.Containers;
using Grasshopper.Kernel.Parameters;
using Macaw.Filtering;
using Macaw.Filtering.Objects.Edging;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;
using System.Windows.Forms;
using GH_IO.Serialization;

namespace Macaw_GH.Filtering.Figure
{
    public class Edge : GH_Component, IGH_VariableParameterComponent
    {
        private int ModeIndex = 0;
        private string[] modes = { "Difference", "Homogenity", "Sobel", "Simple", "Canny" };

        /// <summary>
        /// Initializes a new instance of the Edge class.
        /// </summary>
        public Edge()
          : base("Edge", "Edge", "...", "Aviary", "Bitmap Edit")
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
            IGH_Goo X = null;
            int L = 0;
            int H = 0;
            int S = 1;
            bool Z = false;

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            
            Bitmap A = new Bitmap(10, 10);
            if (X != null) { X.CastTo(out A); }

            mFilter Filter = new mFilter();
            
            switch (ModeIndex)
            {
                case 0:
                    Filter = new mEdgeDifference();
                    break;
                case 1:
                    Filter = new mEdgeHomogenity();
                    break;
                case 2:
                    if (!DA.GetData(1, ref Z)) return;
                    Filter = new mEdgeSobel(Z);
                    break;
                case 3:
                    if (!DA.GetData(1, ref S)) return;
                    if (!DA.GetData(2, ref L)) return;
                    if (!DA.GetData(3, ref Z)) return;
                    Filter = new mEdgeSimple(L, S, Z);
                    break;
                case 4:
                    if (!DA.GetData(1, ref S)) return;
                    if (!DA.GetData(2, ref L)) return;
                    if (!DA.GetData(3, ref H)) return;
                    Filter = new mEdgeCanny(L, H, S);
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

            ClearInputs(2);
            paramBoolean(1, "Scale", "S", "---", GH_ParamAccess.item, true);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeD(Object sender, EventArgs e)
        {
            ModeIndex = 3;

            ClearInputs(4);
            paramInteger(1, "Threshold", "T", "---", GH_ParamAccess.item, 1);
            paramInteger(2, "Divisor", "D", "---", GH_ParamAccess.item, 255);
            paramBoolean(3, "Dynamic", "B", "---", GH_ParamAccess.item, true);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeE(Object sender, EventArgs e)//Additive
        {
            ModeIndex = 4;

            ClearInputs(4);
            paramInteger(1, "Low", "L", "---", GH_ParamAccess.item, 0);
            paramInteger(2, "High", "H", "---", GH_ParamAccess.item, 0);
            paramInteger(3, "Size", "S", "---", GH_ParamAccess.item, 1);

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void ClearInputs(int clearFrom = 1)
        {
            int j = Params.Input.Count - 1;

            if (Params.Input.Count > (clearFrom = 1))
            {
                for (int i = clearFrom - 1; i < j; i++)
                {
                    Params.Input[Params.Input.Count - 1].RemoveAllSources();
                    Params.Input[Params.Input.Count - 1].ClearData();
                    Params.UnregisterInputParameter(Params.Input[Params.Input.Count - 1]);
                }
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
                return Properties.Resources.Macaw_Filter_Edges;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("2296c265-1159-4275-b96c-8db295b815e3"); }
        }
    }
}