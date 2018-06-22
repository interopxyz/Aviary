using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Filtering;
using Macaw.Compiling;
using Wind.Containers;
using Macaw.Editing.Resizing;
using Macaw.Filtering.Stylized;
using Macaw.Compiling.Modifiers;
using Wind.Types;
using System.Windows.Forms;
using GH_IO.Serialization;
using Macaw.Build;

namespace Macaw_GH.Edit
{
    public class FlattenBorder : GH_Component, IGH_VariableParameterComponent
    {
        private int ModeIndex = 0;
        private string[] modes = { "Flatten", "Border", "Borders" };

        /// <summary>
        /// Initializes a new instance of the FlattenBorder class.
        /// </summary>
        public FlattenBorder()
          : base("Flatten", "Flatten", "---", "Aviary", "Bitmap Edit")
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

            pManager.AddColourParameter("Color", "C", "---", GH_ParamAccess.item, Color.White);
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo Z = null;
            int T = 10;
            int B = 10;
            int L = 10;
            int R = 10;
            Color C = Color.White;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref C)) return;

            Bitmap A = new Bitmap(10, 10);
            if (Z != null) { Z.CastTo(out A); }
            Bitmap X = new Bitmap(10, 10);

            switch (ModeIndex)
            {
                default:
                    X = new mModifyFlatten(A, C).ModifiedBitmap;
                    break;
                case 1:
                    if (!DA.GetData(2, ref T)) return;
                    X = new mApply(A, new mPadding(T, T, T, T, A.Width, A.Height, C)).ModifiedBitmap;
                    break;
                case 2:
                    if (!DA.GetData(2, ref T)) return;
                    if (!DA.GetData(3, ref B)) return;
                    if (!DA.GetData(4, ref L)) return;
                    if (!DA.GetData(5, ref R)) return;
                    X = new mApply(A, new mPadding(T, B, L, R, A.Width, A.Height, C)).ModifiedBitmap;
                    break;
            }

            DA.SetData(0, X);
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void UpdateMessage()
        {
            Message = modes[ModeIndex];
        }

        /// <summary>
        /// Adds to the default serialization method to save the current child status so it persists on copy/paste and save/reopen.
        /// </summary>
        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("ModeIndex", ModeIndex);

            return base.Write(writer);
        }

        /// <summary>
        /// Adds to the default deserialization method to retrieve the saved child status so it persists on copy/paste and save/reopen.
        /// </summary>
        public override bool Read(GH_IReader reader)
        {
            ModeIndex = reader.GetInt32("ModeIndex");

            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.SetPersistentData(new Bitmap(10, 10));

            UpdateMessage();
            return base.Read(reader);
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, modes[0], ModeA, true, ModeIndex == 0);
            Menu_AppendItem(menu, modes[1], ModeB, true, ModeIndex == 1);
            Menu_AppendItem(menu, modes[2], ModeC, true, ModeIndex == 2);
        }

        private void ModeA(Object sender, EventArgs e)//Flatten
        {
            ClearInputs(1);
            ModeIndex = 0;

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeB(Object sender, EventArgs e)//UniformBorder
        {
            if (Params.Input.Count > 2) { ClearInputs(1); }
            ModeIndex = 1;

            paramInteger(2, "Distance", "D", "---", 10);

            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeC(Object sender, EventArgs e)//Non Uniform Border
        {
            if (Params.Input.Count > 2) { ClearInputs(1); }
            ModeIndex = 2;

            paramInteger(2, "Top", "T", "---", 10);
            paramInteger(3, "Bottom", "B", "---", 10);
            paramInteger(4, "Left", "L", "---", 10);
            paramInteger(5, "Right", "R", "---", 10);

            UpdateMessage();
            ExpireSolution(true);
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        // Utility Methods
        private void ClearInputs(int offset = 0)
        {
            int j = Params.Input.Count - 1;
            for (int i = offset; i < j; i++)
            {
                Params.Input[Params.Input.Count - 1].RemoveAllSources();
                Params.Input[Params.Input.Count - 1].ClearData();
                Params.UnregisterInputParameter(Params.Input[Params.Input.Count - 1]);
            }
            Params.OnParametersChanged();
        }


        private Param_Number paramNumber(int index, string Name, string NickName, string Description, double Value)
        {
            if ((Params.Input.Count - 1) < index)
            {
                Params.RegisterInputParam(new Param_Number(), index);
                Params.OnParametersChanged();
            }

            Param_Number param = (Param_Number)Params.Input[index];
            param.Name = Name;
            param.NickName = NickName;
            param.Description = Description;
            param.Access = GH_ParamAccess.item;
            param.PersistentData.Clear();
            param.SetPersistentData(Value);

            return param;
        }

        private Param_Integer paramInteger(int index, string Name, string NickName, string Description, int Value)
        {
            if ((Params.Input.Count - 1) < index)
            {
                Params.RegisterInputParam(new Param_Integer(), index);
                Params.OnParametersChanged();
            }

            Param_Integer param = (Param_Integer)Params.Input[index];
            param.Name = Name;
            param.NickName = NickName;
            param.Description = Description;
            param.Access = GH_ParamAccess.item;
            param.PersistentData.Clear();
            param.SetPersistentData(Value);

            return param;
        }



        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.quinary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Effect_Border;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("275e9824-405f-4725-bc29-82ecb0a752cc"); }
        }
    }
}