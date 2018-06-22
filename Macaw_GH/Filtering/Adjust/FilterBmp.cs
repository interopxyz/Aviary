using System;

using Grasshopper.Kernel;
using Wind.Types;
using Wind.Containers;
using Macaw.Filtering.Adjustments.FilterColor;
using Macaw.Filtering;
using System.Drawing;
using Macaw.Build;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;
using GH_IO.Serialization;
using System.Windows.Forms;
using Rhino.Geometry;

namespace Macaw_GH.Filtering.Adjust
{
    public class FilterBmp : GH_Component
    {
        private int ModeIndex = 0;
        private string[] modes = { "Channel", "Color", "HSL", "YCbCr" };
        private bool ModeInOut = false;

        /// <summary>
        /// Initializes a new instance of the Filter class.
        /// </summary>
        public FilterBmp()
          : base("Filter", "Filter", "---", "Aviary", "Bitmap Edit")
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

            pManager.AddColourParameter("Color", "C", "Replacement Color", GH_ParamAccess.item, Color.Black);
            pManager[1].Optional = true;
            pManager.AddIntervalParameter("Red", "R", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[2].Optional = true;
            pManager.AddIntervalParameter("Green", "G", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[3].Optional = true;
            pManager.AddIntervalParameter("Blue", "B", "---", GH_ParamAccess.item, new Interval(0, 255));
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
            Color C = Color.Black;
            Interval Rc = new Interval(0, 255);
            Interval Gc = new Interval(100, 255);
            Interval Bc = new Interval(100, 255);

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref C)) return;
            if (!DA.GetData(2, ref Rc)) return;
            if (!DA.GetData(3, ref Gc)) return;
            if (!DA.GetData(4, ref Bc)) return;

            Bitmap A = new Bitmap(10, 10);
            if (Z != null) { Z.CastTo(out A); }
            mFilter Filter = new mFilter();

            switch (ModeIndex)
            {
                case 0:
                        Filter = new mFilterARGBChannel(new wDomain(Rc.T0, Rc.T1), new wDomain(Gc.T0, Gc.T1), new wDomain(Bc.T0, Bc.T1),ModeInOut);
                    break;
                case 1:
                    Filter = new mFilterARGBColor(new wDomain(Rc.T0, Rc.T1), new wDomain(Gc.T0, Gc.T1), new wDomain(Bc.T0, Bc.T1),ModeInOut,C);
            break;
                case 2:
                    Filter = new mFilterHSL(new wDomain(Rc.T0, Rc.T1), new wDomain(Gc.T0, Gc.T1), new wDomain(Bc.T0, Bc.T1),ModeInOut,C);
            break;
                case 3:
                        Filter = new mFilterYCbCrColor(new wDomain(Rc.T0, Rc.T1), new wDomain(Gc.T0, Gc.T1), new wDomain(Bc.T0, Bc.T1),ModeInOut,C);
                    
            break;
        }

        Bitmap B = new mApply(A, Filter).ModifiedBitmap;
        wObject W = new wObject(Filter, "Macaw", Filter.Type);


        DA.SetData(0, B);
            DA.SetData(1, W);
        }

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    private void UpdateMessage()
    {
        string iMode = "Out";
        if (ModeInOut) { iMode = "In"; }

        Message = modes[ModeIndex] + " " + iMode;
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
            Menu_AppendItem(menu, modes[3], ModeD, true, ModeIndex == 3);

            Menu_AppendSeparator(menu);
        Menu_AppendItem(menu, "In", InOutMode, true, ModeInOut);
        Menu_AppendItem(menu, "Out", InOutMode, true, !ModeInOut);

    }

    private void InOutMode(Object sender, EventArgs e)//Switch between In Out
    {
        ModeInOut = !ModeInOut;

        UpdateMessage();
        ExpireSolution(true);
    }

    private void ModeA(Object sender, EventArgs e)//RGB
    {
        ModeIndex = 0;

        paramDomain(2, "Red", "R", "Domain from [0,255]", new Interval(0, 255));
        paramDomain(3, "Green", "G", "Domain from [0,255]", new Interval(100, 255));
        paramDomain(4, "Blue", "B", "Domain from [0,255]", new Interval(100, 255));

        UpdateMessage();
        ExpireSolution(true);
        }

        private void ModeB(Object sender, EventArgs e)//RGB
        {
            ModeIndex = 1;

            paramDomain(2, "Red", "R", "Domain from [0,255]", new Interval(0, 255));
            paramDomain(3, "Green", "G", "Domain from [0,255]", new Interval(100, 255));
            paramDomain(4, "Blue", "B", "Domain from [0,255]", new Interval(100, 255));

            UpdateMessage();
            ExpireSolution(true);
        }

        private void ModeC(Object sender, EventArgs e)//HSL
    {
        ModeIndex = 2;

        paramDomain(2, "Hue", "H", "Domain from [0,360]", new Interval(0, 360));
        paramDomain(3, "Saturation", "S", "Domain from [0,1.0]", new Interval(0, 1.0));
        paramDomain(4, "Luminance", "L", "Domain from [0,1.0]", new Interval(0, 1.0));

        UpdateMessage();
        ExpireSolution(true);
    }

    private void ModeD(Object sender, EventArgs e)//YCbCr
    {
        ModeIndex = 3;

        paramDomain(2, "Top", "Y", "Domain from [0,1.0]", new Interval(0.0, 1.0));
        paramDomain(3, "Bottom", "Cb", "Domain from [-0.5,0.5]", new Interval(-0.5, 0.5));
        paramDomain(4, "Left", "Cr", "Domain from [-0.5,0.5]", new Interval(-0.5, 0.5));

        UpdateMessage();
        ExpireSolution(true);
    }

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    private Param_Interval paramDomain(int index, string Name, string NickName, string Description, Interval Value)
    {

        Param_Interval param = (Param_Interval)Params.Input[index];
        param.Name = Name;
        param.NickName = NickName;
        param.Description = Description;
        param.PersistentData.Clear();
        param.SetPersistentData(Value);

        return param;
    }

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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
            return Properties.Resources.Macaw_Channels_ARGB;
        }
    }

    /// <summary>
    /// Gets the unique ID for this component. Do not change this ID after release.
    /// </summary>
    public override Guid ComponentGuid
    {
        get { return new Guid("5f953b09-2cff-4899-b827-6b136adcbfea"); }
    }
}
}