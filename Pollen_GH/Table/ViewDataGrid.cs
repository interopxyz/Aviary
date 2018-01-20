using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Containers;
using Wind.Utilities;

using Parrot.Containers;
using Parrot.Controls;
using Grasshopper.Kernel.Types;
using Wind.Collections;
using Pollen.Collections;
using System.Windows.Forms;
using GH_IO.Serialization;
using Wind.Presets;

namespace Pollen_GH.Table
{
    public class ViewDataGrid : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        public int GridType = 0;
        public bool HasTitle = true;

        /// <summary>
        /// Initializes a new instance of the ViewGrid class.
        /// </summary>
        public ViewDataGrid()
          : base("Quick Grid", "Grid", "---", "Aviary", "Charting & Data")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("DataSets", "D", "Items", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "WPF Control Element", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string ID = this.Attributes.InstanceGuid.ToString();
            string name = new GUIDtoAlpha(Convert.ToString(ID + Convert.ToString(this.RunCount)), false).Text;
            int C = this.RunCount;

            wObject WindObject = new wObject();
            pElement Element = new pElement();
            bool Active = Elements.ContainsKey(C);

            var pCtrl = new pDataGrid(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                if (Elements[C] != null)
                {
                    WindObject = Elements[C];
                    Element = (pElement)WindObject.Element;
                    pCtrl = (pDataGrid)Element.PollenControl;
                }
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            IGH_Goo D = null;

            if (!DA.GetData(0, ref D)) return;

            wObject W = new wObject();
            D.CastTo(out W);
            DataSetCollection DC = (DataSetCollection)W.Element;

            if (DC.TotalCustomFill == 0) { DC.SetDefaultPallet(wGradients.GradientTypes.SolidLightGray, false, DC.Sets.Count > 1); }
            if (DC.TotalCustomFont == 0) { DC.SetDefaultFonts(new wFonts(wFonts.FontTypes.DataGrid).Font); }
            if (DC.TotalCustomStroke == 0) { DC.SetDefaultStrokes(wStrokes.StrokeTypes.OffWhiteSolid); }

            if (DC.TotalCustomTitles == 0) { DC.Graphics.FontObject = new wFonts(wFonts.FontTypes.DataGridTitles).Font; }

            pCtrl.SetProperties(DC, HasTitle);

            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pCtrl.Element, pCtrl, pCtrl.Type); }
            WindObject = new wObject(Element, "Pollen", Element.Type);
            WindObject.GUID = this.InstanceGuid;
            WindObject.Instance = C;

            Elements[this.RunCount] = WindObject;

            DA.SetData(0, WindObject);
            
        }
        
        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Titles", TitleMode, true, HasTitle);
            
        }

        public override bool Write(GH_IWriter writer)
        {

            writer.SetBoolean("Title", HasTitle);
            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {

            HasTitle = reader.GetBoolean("Title");

            return base.Read(reader);
        }


        private void TitleMode(Object sender, EventArgs e)
        {
            HasTitle = !HasTitle;
            this.ExpireSolution(true);
        }


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
                return Properties.Resources.Pollen_GridView;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{04b68edc-45d7-4e7d-87b2-a95360d3cdc7}"); }
        }
    }
}