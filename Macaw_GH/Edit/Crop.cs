using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Macaw.Build;
using Wind.Containers;
using System.Drawing;
using Grasshopper.Kernel.Types;
using Macaw.Editing.Resizing;
using Macaw.Filtering;
using System.Windows.Forms;
using Rhino.Geometry;
using GH_IO.Serialization;

namespace Macaw_GH.Edit
{
    public class Crop : GH_Component, IGH_VariableParameterComponent
    {
        private int ModeIndex = 0;
        private string[] modes = { "Rectangle", "Region", "Shrink", "Resize" };

        /// <summary>
        /// Initializes a new instance of the Crop class.
        /// </summary>
        public Crop()
          : base("Crop & Resize", "Resize", "---", "Aviary", "Bitmap Edit")
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

            pManager.AddColourParameter("Background", "C", "---", GH_ParamAccess.item, Color.Transparent);
            pManager[1].Optional = true;

            pManager.AddRectangleParameter("Rectangle", "R", "---", GH_ParamAccess.item, new Rectangle3d(Plane.WorldXY, 800, 600));
            pManager[2].Optional = true;
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
            int M = 0;
            int AX = 1;
            int AY = 1;
            int CW = 800;
            int CH = 600;
            Rectangle3d Rc = new Rectangle3d(Plane.WorldXY, 800, 600);
            Color C = Color.Transparent;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            
            Bitmap A = new Bitmap(10, 10);
            if (Z != null) { Z.CastTo(out A); }

            mFilters Filter = new mFilters();

            switch (ModeIndex)
            {
                case 0://Rectangle
                    if (!DA.GetData(1, ref C)) return;
                    if (!DA.GetData(2, ref Rc)) return;

                    Filter = new mCropRectangle((int)Rc.X.T0, (int)Rc.Y.T0, (int)Rc.Width, (int)Rc.Height, C);
                    break;
                case 1://Region
                    if (!DA.GetData(1, ref C)) return;
                    if (!DA.GetData(2, ref AX)) return;
                    if (!DA.GetData(3, ref AY)) return;
                    if (!DA.GetData(4, ref CW)) return;
                    if (!DA.GetData(5, ref CH)) return;

                    Filter = new mCropCanvas(AX, AY, CW, CH, A.Width, A.Height, C);
                    break;
                case 2://Shrink
                    if (!DA.GetData(1, ref C)) return;

                    Filter = new mShrinkToColor(C);
                    break;
                case 3://Resize
                    if (!DA.GetData(1, ref M)) return;
                    if (!DA.GetData(2, ref CW)) return;
                    if (!DA.GetData(3, ref CH)) return;

                    switch (M)
                    {
                        case 0:
                            Filter = new mResizeBicubic(CW, CH);
                            break;
                        case 1:
                            Filter = new mResizeBilinear(CW, CH);
                            break;
                        case 2:
                            Filter = new mResizeNearistNeighbor(CW, CH);
                            break;
                    }
                    break;
            }
            
            Bitmap B = new mApplySequence(A, Filter).ModifiedBitmap;
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
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void ModeA(Object sender, EventArgs e)
        {
            ModeIndex = 0;

            ClearInputs(2);
            paramColor(1, "Color", "C", "---", GH_ParamAccess.item, Color.Black);
            paramRectangle(2, "Rectangle", "R", "---", GH_ParamAccess.item, new Rectangle3d(Plane.WorldXY, 800, 600));


            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeB(Object sender, EventArgs e)
        {
            ModeIndex = 1;

            ClearInputs(5);
            paramColor(1, "Color", "C", "---", GH_ParamAccess.item, Color.Black);
            paramInteger(2, "X Justification", "X", "---", GH_ParamAccess.item, 1);
            paramInteger(3, "Y Justification", "Y", "---", GH_ParamAccess.item, 1);
            paramInteger(4, "Width", "W", "---", GH_ParamAccess.item, 800);
            paramInteger(5, "Height", "H", "---", GH_ParamAccess.item, 600);

            Params.OnParametersChanged();

            Param_Integer paramX = (Param_Integer)Params.Input[2];
            paramX.AddNamedValue("Left", 0);
            paramX.AddNamedValue("Middle", 1);
            paramX.AddNamedValue("Right", 2);

            Param_Integer paramY = (Param_Integer)Params.Input[3];
            paramY.AddNamedValue("Top", 0);
            paramY.AddNamedValue("Center", 1);
            paramY.AddNamedValue("Bottom", 2);
            
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeC(Object sender, EventArgs e)
        {
            ModeIndex = 2;

            ClearInputs(1);
            paramColor(1, "Color", "C", "---", GH_ParamAccess.item, Color.Black);


            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeD(Object sender, EventArgs e)
        {
            ModeIndex = 3;

            ClearInputs(3);
            paramInteger(1, "Mode", "M", "---", GH_ParamAccess.item, 0);
            paramInteger(2, "Width", "W", "---", GH_ParamAccess.item, 800);
            paramInteger(3, "Height", "H", "---", GH_ParamAccess.item, 600);

            Params.OnParametersChanged();
            Param_Integer paramX = (Param_Integer)Params.Input[1];
            paramX.AddNamedValue("Bicubic", 0);
            paramX.AddNamedValue("Bilinear", 1);
            paramX.AddNamedValue("Neighbor", 2);


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

        private void paramRectangle(int index, string name, string nickName, string description, GH_ParamAccess access, Rectangle3d Value)
        {
            if ((Params.Input.Count - 1) < index)
            {
                Params.RegisterInputParam(new Param_Rectangle(), index);
                Params.OnParametersChanged();
            }
            else
            {
                if (Params.Input[index].GetType() != new Param_Rectangle().GetType())
                {
                    Params.Input[index].RemoveAllSources();
                    Params.Input[index] = new Param_Rectangle();
                    Params.OnParametersChanged();
                }
            }

            Params.Input[index].ClearData();

            Param_Rectangle param = (Param_Rectangle)Params.Input[index];
            param.PersistentData.ClearData();
            param.PersistentData.Clear();
            param.SetPersistentData(Value);
            SetParamProperties(index, name, nickName, description, access);

        }

        private void paramColor(int index, string name, string nickName, string description, GH_ParamAccess access, System.Drawing.Color Value)
        {
            if ((Params.Input.Count - 1) < index)
            {
                Params.RegisterInputParam(new Param_Colour(), index);
                Params.OnParametersChanged();
            }
            else
            {
                if (Params.Input[index].GetType() != new Param_Colour().GetType())
                {
                    Params.Input[index].RemoveAllSources();
                    Params.Input[index] = new Param_Colour();
                    Params.OnParametersChanged();
                }
            }

            Params.Input[index].ClearData();

            Param_Colour param = (Param_Colour)Params.Input[index];
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
                return Properties.Resources.Macaw_Edit_Crop;
            }
        }


        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("8ae47e45-d17f-4c31-8078-e4c1d32f8dda"); }
        }
    }
}