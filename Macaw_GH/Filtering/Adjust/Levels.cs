using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Containers;
using Wind.Types;
using Macaw.Filtering;
using Macaw.Filtering.Adjustments.AdjustColor;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;
using Macaw.Filtering.Adjustments;
using System.Windows.Forms;
using GH_IO.Serialization;

namespace Macaw_GH.Filtering.Adjust
{
    public class Levels : GH_Component, IGH_VariableParameterComponent
    {
        public int ModeIndex = 0;
        private string[] modes = { "Uniform","Gray", "RGB", "Full" };

        public int TypeIndex = 0;
        private string[] types = { "Basic", "16bpp" };

        /// <summary>
        /// Initializes a new instance of the Levels class.
        /// </summary>
        public Levels()
          : base("Levels", "Levels", "...", "Aviary", "Bitmap Edit")
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

            pManager.AddIntervalParameter("Gray In", "G0", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[1].Optional = true;
            pManager.AddIntervalParameter("Gray Out", "G1", "---", GH_ParamAccess.item, new Interval(0, 255));
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

            Interval Ra = new Interval(0, 255);
            Interval Ga = new Interval(0, 255);
            Interval Ba = new Interval(0, 255);
            Interval Xa = new Interval(0, 255);

            Interval Rb = new Interval(0, 255);
            Interval Gb = new Interval(0, 255);
            Interval Bb = new Interval(0, 255);
            Interval Xb = new Interval(0, 255);

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref Ra)) return;
            if (!DA.GetData(2, ref Rb)) return;

            Bitmap A = new Bitmap(10,10);
            if (Z != null) { Z.CastTo(out A); }

            mFilter Filter = new mFilter();


            switch (ModeIndex)
            {
                case 0:
                    if (TypeIndex == 0)
                    {
                        Filter = new mAdjustLevels(new wDomain(Ra.T0, Ra.T1), new wDomain(Rb.T0, Rb.T1), new wDomain(Ra.T0, Ra.T1), new wDomain(Rb.T0, Rb.T1), new wDomain(Ra.T0, Ra.T1), new wDomain(Rb.T0, Rb.T1));
                    }
                    else
                    {
                        Filter = new mAdjustLevels16bpp(new wDomain(Ra.T0, Ra.T1), new wDomain(Rb.T0, Rb.T1), new wDomain(Ra.T0, Ra.T1), new wDomain(Rb.T0, Rb.T1), new wDomain(Ra.T0, Ra.T1), new wDomain(Rb.T0, Rb.T1));
                    }
                    break;
                case 1:
                    if (TypeIndex==0)
                    {
                        Filter = new mAdjustLevels(new wDomain(Ra.T0, Ra.T1), new wDomain(Rb.T0, Rb.T1));
                    }
                    else
                    {
                        Filter = new mAdjustLevels16bpp(new wDomain(Ra.T0, Ra.T1), new wDomain(Rb.T0, Rb.T1));
                    }
                    break;
                case 2:
                    if (!DA.GetData(3, ref Ga)) return;
                    if (!DA.GetData(4, ref Gb)) return;
                    if (!DA.GetData(5, ref Ba)) return;
                    if (!DA.GetData(6, ref Bb)) return;

                    if (TypeIndex == 0)
                    {
                        Filter = new mAdjustLevels(new wDomain(Ra.T0, Ra.T1), new wDomain(Rb.T0, Rb.T1), new wDomain(Ga.T0, Ga.T1), new wDomain(Gb.T0, Gb.T1), new wDomain(Ba.T0, Ba.T1), new wDomain(Bb.T0, Bb.T1));
                    }
                    else
                    {
                        Filter = new mAdjustLevels16bpp(new wDomain(Ra.T0, Ra.T1), new wDomain(Rb.T0, Rb.T1), new wDomain(Ga.T0, Ga.T1), new wDomain(Gb.T0, Gb.T1), new wDomain(Ba.T0, Ba.T1), new wDomain(Bb.T0, Bb.T1));
                    }
                    break;
                case 3:
                    if (!DA.GetData(3, ref Ga)) return;
                    if (!DA.GetData(4, ref Gb)) return;
                    if (!DA.GetData(5, ref Ba)) return;
                    if (!DA.GetData(6, ref Bb)) return;
                    if (!DA.GetData(7, ref Xa)) return;
                    if (!DA.GetData(8, ref Xb)) return;

                    if (TypeIndex == 0)
                    {
                        Filter = new mAdjustLevels(new wDomain(Ra.T0, Ra.T1), new wDomain(Rb.T0, Rb.T1), new wDomain(Ga.T0, Ga.T1), new wDomain(Gb.T0, Gb.T1), new wDomain(Ba.T0, Ba.T1), new wDomain(Bb.T0, Bb.T1), new wDomain(Xa.T0, Xa.T1), new wDomain(Xb.T0, Xb.T1));
                    }
                    else
                    {
                        Filter = new mAdjustLevels16bpp(new wDomain(Ra.T0, Ra.T1), new wDomain(Rb.T0, Rb.T1), new wDomain(Ga.T0, Ga.T1), new wDomain(Gb.T0, Gb.T1), new wDomain(Ba.T0, Ba.T1), new wDomain(Bb.T0, Bb.T1), new wDomain(Xa.T0, Xa.T1), new wDomain(Xb.T0, Xb.T1));
                    }
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

        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void ModeA(Object sender, EventArgs e)
        {
            ModeIndex = 0;

            ClearInputs(3);
            paramInterval(1,"Value In", "V0", "---", GH_ParamAccess.item, new Interval(0,255));
            paramInterval(2,"Value Out", "V1", "---", GH_ParamAccess.item, new Interval(0, 255));

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeB(Object sender, EventArgs e)
        {
            ModeIndex = 1;

            ClearInputs(3);
            paramInterval(1, "Gray In", "G0", "---", GH_ParamAccess.item, new Interval(0, 255));
            paramInterval(2, "Gray Out", "G1", "---", GH_ParamAccess.item, new Interval(0, 255));

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeC(Object sender, EventArgs e)
        {
            ModeIndex = 2;

            ClearInputs(7);
            paramInterval(1, "Red In", "R0", "---", GH_ParamAccess.item, new Interval(0, 255));
            paramInterval(2, "Red Out", "R1", "---", GH_ParamAccess.item, new Interval(0, 255));
            paramInterval(3, "Green In", "G0", "---", GH_ParamAccess.item, new Interval(0, 255));
            paramInterval(4, "Green Out", "G1", "---", GH_ParamAccess.item, new Interval(0, 255));
            paramInterval(5, "Blue In", "B0", "---", GH_ParamAccess.item, new Interval(0, 255));
            paramInterval(6, "Blue Out", "B1", "---", GH_ParamAccess.item, new Interval(0, 255));

            Params.OnParametersChanged();
            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeD(Object sender, EventArgs e)
        {
            ModeIndex = 3;

            ClearInputs(9);
            paramInterval(1, "Red In", "R0", "---", GH_ParamAccess.item, new Interval(0, 255));
            paramInterval(2, "Red Out", "R1", "---", GH_ParamAccess.item, new Interval(0, 255));
            paramInterval(3, "Green In", "G0", "---", GH_ParamAccess.item, new Interval(0, 255));
            paramInterval(4, "Green Out", "G1", "---", GH_ParamAccess.item, new Interval(0, 255));
            paramInterval(5, "Blue In", "B0", "---", GH_ParamAccess.item, new Interval(0, 255));
            paramInterval(6, "Blue Out", "B1", "---", GH_ParamAccess.item, new Interval(0, 255));
            paramInterval(7, "Grey In", "X0", "---", GH_ParamAccess.item, new Interval(0, 255));
            paramInterval(8, "Grey Out", "X1", "---", GH_ParamAccess.item, new Interval(0, 255));
            Params.OnParametersChanged();

            UpdateMessage();
            ExpireSolution(true);
        }

        private void TypeA(Object sender, EventArgs e)
        {
            TypeIndex = 0;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void TypeB(Object sender, EventArgs e)
        {
            TypeIndex = 1;

            UpdateMessage();
            ExpireSolution(true);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void UpdateMessage()
        {
            Message = types[TypeIndex] + " " + modes[ModeIndex];
        }
        
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Filter_Levels;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("aca04898-d1c1-4e32-82e2-afff8c7cdaa3"); }
        }
    }
}