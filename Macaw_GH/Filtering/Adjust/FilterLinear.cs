using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Types;
using Wind.Containers;
using Macaw.Filtering.Adjustments.FilterColor;
using Macaw.Filtering;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;
using System.Windows.Forms;
using GH_IO.Serialization;

namespace Macaw_GH.Filtering.Adjust
{
    public class FilterLinear : GH_Component, IGH_VariableParameterComponent
    {
        private int ModeIndex = 0;
        private string[] modes = { "HSL", "YCbCr" };

        /// <summary>
        /// Initializes a new instance of the FilterLinearHSL class.
        /// </summary>
        public FilterLinear()
          : base("Filter Linear", "Linear", "...", "Aviary", "Bitmap Edit")
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

            pManager.AddIntervalParameter("Luminance", "LI", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            pManager[1].Optional = true;
            pManager.AddIntervalParameter("Luminance", "LO", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            pManager[2].Optional = true;
            pManager.AddIntervalParameter("Saturation In", "SI", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            pManager[3].Optional = true;
            pManager.AddIntervalParameter("Saturation Out", "SO", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            pManager[4].Optional = true;

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
            Interval LA = new Interval(0.0, 1.0);
            Interval LB = new Interval(0.0, 1.0);
            Interval SA = new Interval(0.0, 1.0);
            Interval SB = new Interval(0.0, 1.0);

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref LA)) return;
            if (!DA.GetData(2, ref LB)) return;
            if (!DA.GetData(3, ref SA)) return;
            if (!DA.GetData(4, ref SB)) return;

            Bitmap A = new Bitmap(10, 10);
            if (Z != null) { Z.CastTo(out A); }

            mFilter Filter = new mFilter();

            switch (ModeIndex)
            {
                default:
                    Filter = new mFilterHSLLinear(new wDomain(SA.T0, SA.T1), new wDomain(SB.T0, SB.T1), new wDomain(LA.T0, LA.T1), new wDomain(LB.T0, LB.T1));
                    break;
                case 1:
                    Interval TA = new Interval(0.0, 1.0);
                    Interval TB = new Interval(0.0, 1.0);
                    if (!DA.GetData(5, ref TA)) return;
                    if (!DA.GetData(6, ref TB)) return;

                    Filter = new mFilterYCbCrLinear(new wDomain(LA.T0, LA.T1), new wDomain(SA.T0, SA.T1), new wDomain(TA.T0, TA.T1), new wDomain(LB.T0, LB.T1), new wDomain(SB.T0, SB.T1), new wDomain(TB.T0, TB.T1));
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
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void ModeA(Object sender, EventArgs e)
        {
            ClearInputs(5);
            ModeIndex = 0;

            paramInterval(1, "Luminance", "LI", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            paramInterval(2, "Luminance", "LO", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            paramInterval(3, "Saturation In", "SI", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            paramInterval(4, "Saturation Out", "SO", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));

            Params.OnParametersChanged();

            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeB(Object sender, EventArgs e)
        {
            ModeIndex = 1;

            paramInterval(1, "Luminance", "LI", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            paramInterval(2, "Luminance", "LO", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            paramInterval(3, "Chrominance Blue In", "BI", "---", GH_ParamAccess.item, new Interval(-0.5, 0.5));
            paramInterval(4, "Chrominance Blue Out", "BO", "---", GH_ParamAccess.item, new Interval(-0.5, 0.5));
            paramInterval(5, "Chrominance Red In", "RI", "---", GH_ParamAccess.item, new Interval(-0.5, 0.5));
            paramInterval(6, "Chrominance Red Out", "RO", "---", GH_ParamAccess.item, new Interval(-0.5, 0.5));

            Params.OnParametersChanged();

            UpdateMessage();
            ExpireSolution(true);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void ClearInputs(int clearFrom = 1)
        {
            int j = Params.Input.Count - 1;

            for (int i = clearFrom-1; i < j; i++)
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

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Channels_HSL;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("0904a726-732c-44ca-84ae-a09d76c948a8"); }
        }
    }
}