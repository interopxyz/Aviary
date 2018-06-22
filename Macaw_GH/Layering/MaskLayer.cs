using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Macaw.Compiling;
using System.Drawing;
using System.Windows.Forms;
using GH_IO.Serialization;
using Grasshopper.Kernel.Parameters;
using Wind.Types;

namespace Macaw_GH.Compose
{
    public class Mask : GH_Component, IGH_VariableParameterComponent
    {

        private int ModeIndex = 0;
        private string[] modes = { "Image", "Color" };

        /// <summary>
        /// Initializes a new instance of the Mask class.
        /// </summary>
        public Mask()
          : base("Mask Layer", "Mask", "---", "Aviary", "Bitmap Build")
        {
            UpdateMessage();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Image", "B", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo X = null;

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;

            wObject Z = new wObject();
            if (X != null) { X.CastTo(out Z); }
            mLayer L = new mLayer((mLayer)Z.Element);

            switch (ModeIndex)
            {
                case 0:
                    IGH_Goo Y = null;
                    Bitmap A = new Bitmap(10,10);

                    if (!DA.GetData(1, ref Y)) return;
                    if (Y != null) { Y.CastTo(out A); }
                    Bitmap B = (Bitmap)A.Clone();

                    L.SetImageMask(B);

                    break;
                case 1:
                    Color C = Color.Black;
                    double V = 1.0;
                    if (!DA.GetData(1, ref C)) return;
                    if (!DA.GetData(2, ref V)) return;

                    L.SetColorMask(new wColor(C.A,C.R,C.G,C.B), V);

                    break;
            }


            wObject W = new wObject(L, "Macaw", L.Type);


            DA.SetData(0, W);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, modes[0], ModeA, true, ModeIndex == 0);
            Menu_AppendItem(menu, modes[1], ModeB, true, ModeIndex == 1);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void ModeA(Object sender, EventArgs e)
        {
            ClearInput();
            ClearInput();
            ModeIndex = 0;

            Param_GenericObject param = new Param_GenericObject();
            param.Name = "Bitmap";
            param.NickName = "B";
            param.Description = "---";
            param.Access = GH_ParamAccess.item;
            param.SetPersistentData(new Bitmap(10,10));
            param.Optional = true;

            Params.RegisterInputParam(param, 1);
            Params.OnParametersChanged();

            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeB(Object sender, EventArgs e)
        {
            ClearInput();
            ModeIndex = 1;

            Param_Colour param = new Param_Colour();
            param.Name = "Color";
            param.NickName = "C";
            param.Description = "---";
            param.Access = GH_ParamAccess.item;
            param.SetPersistentData(Color.Black);
            param.Optional = true;

            Params.RegisterInputParam(param, 1);

            Param_Number paramA = new Param_Number();
            paramA.Name = "Value";
            paramA.NickName = "T";
            paramA.Description = "---";
            paramA.Access = GH_ParamAccess.item;
            paramA.SetPersistentData(1);
            paramA.Optional = true;

            Params.RegisterInputParam(paramA, 2);

            Params.OnParametersChanged();

            UpdateMessage();
            ExpireSolution(true);
        }

        private void ClearInput()
        {
            Params.Input[1].RemoveAllSources();
            Params.Input[1].ClearData();
            Params.UnregisterInputParameter(Params.Input[1]);
            Params.OnParametersChanged();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void UpdateMessage()
        {
            Message = modes[ModeIndex];
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Set Exposure level for the component.
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
                return Properties.Resources.Macaw_Build_Mask;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("5b2e88a6-394d-4cf8-bebe-6caaea2cbdc1"); }
        }
    }
}