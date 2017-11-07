using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Containers;
using Wind.Utilities;

using Parrot.Containers;
using Parrot.Controls;
using System.Windows.Forms;
using GH_IO.Serialization;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;

namespace Parrot_GH.Controls
{
    public class ViewGrid : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        public int GridType = 0;
        public bool ResizeHorizontal = false;

        public bool AddlRows = false;
        public bool AlternateGraphics = false;
        public bool Sortable = true;

        /// <summary>
        /// Initializes a new instance of the ViewGrid class.
        /// </summary>
        public ViewGrid()
          : base("View Grid", "Grid", "---", "Aviary", "Dashboard Control")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Values", "V", "Values", GH_ParamAccess.tree);
            pManager.AddTextParameter("Titles", "T", "Column Titles", GH_ParamAccess.list,"Title");
            pManager[1].Optional = true;
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

            var pCtrl = new pViewGrid(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pViewGrid)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            GH_Structure<GH_String> D = new GH_Structure<GH_String>();
            List<string> T = new List<string>();

            if (!DA.GetDataTree(0, out D)) return;
            if (!DA.GetDataList(1, T)) return;

            int k = T.Count;
            for (int i = k;i<D.PathCount;i++)
            {
                T.Add(T[k - 1] + i);
            }
           
            pCtrl.SetProperties(GridType, false, ResizeHorizontal, Sortable, AlternateGraphics, AddlRows);
            pCtrl.SetTitles(T);

            List<List<string>> Rows = new List<List<string>>();

            k = 0;
            for (int i = 0; i < D.PathCount; i++)
            {
                if (D.Branches[i].Count > k) { k = D.Branches[i].Count; }

                List<string> RowValues = new List<string>();
                for (int j = 0; j < D.Branches[i].Count; j++)
                {
                    RowValues.Add(D.Branches[i][j].ToString());
                }
                Rows.Add(RowValues);
            }

            pCtrl.SetRows(k);

            for (int i = 0;i<D.PathCount;i++)
            {
                pCtrl.AddRow(T[i],Rows[i]);
            }

            pCtrl.BuildTable();

            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pCtrl.Element, pCtrl, pCtrl.Type); }
            WindObject = new wObject(Element, "Parrot", Element.Type);
            WindObject.GUID = this.InstanceGuid;
            WindObject.Instance = C;

            Elements[this.RunCount] = WindObject;

            DA.SetData(0, WindObject);
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "None", GridNone, true, (GridType == 0));
            Menu_AppendItem(menu, "Columns", GridColumn, true, (GridType == 1));
            Menu_AppendItem(menu, "Rows", GridRow, true, (GridType == 2));
            Menu_AppendItem(menu, "Both", GridAll, true, (GridType == 3));

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Resize", ResizeRow, true, ResizeHorizontal);
            Menu_AppendItem(menu, "Sortable", SortCols, true, Sortable);

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Alternate Rows", AltGraphic, true, AlternateGraphics);
            Menu_AppendItem(menu, "Additional Rows", AdditionalRows, true, AddlRows);
        }

        public override bool Write(GH_IWriter writer)
        {

            writer.SetBoolean("alternateGraphics", AlternateGraphics);
            writer.SetBoolean("Sortable", Sortable);
            writer.SetBoolean("ResizeHorizontal", ResizeHorizontal);
            writer.SetBoolean("AddlRows", AddlRows);
            writer.SetInt32("GridType", GridType);
            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {

            AlternateGraphics = reader.GetBoolean("alternateGraphics");
            Sortable = reader.GetBoolean("Sortable");
            ResizeHorizontal = reader.GetBoolean("ResizeHorizontal");
            AddlRows = reader.GetBoolean("AddlRows");
            GridType = reader.GetInt32("GridType");

            return base.Read(reader);
        }
        
        private void AdditionalRows(Object sender, EventArgs e)
        {
            AddlRows = !AddlRows;
            this.ExpireSolution(true);
        }

        private void AltGraphic(Object sender, EventArgs e)
        {
            AlternateGraphics = !AlternateGraphics;
            this.ExpireSolution(true);
        }

        private void SortCols(Object sender, EventArgs e)
        {
            Sortable = !Sortable;
            this.ExpireSolution(true);
        }
        
        private void ResizeRow(Object sender, EventArgs e)
        {
            ResizeHorizontal = !ResizeHorizontal;
            this.ExpireSolution(true);
        }

        private void GridNone(Object sender, EventArgs e)
        {
            GridType = 0;
            this.ExpireSolution(true);
        }

        private void GridColumn(Object sender, EventArgs e)
        {
            GridType = 1;
            this.ExpireSolution(true);
        }

        private void GridRow(Object sender, EventArgs e)
        {
            GridType = 2;
            this.ExpireSolution(true);
        }

        private void GridAll(Object sender, EventArgs e)
        {
            GridType = 3;
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
                return Properties.Resources.Parrot_ViewGrid;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("4840ec9d-4a01-4ecf-86e4-2a0da8cdfb7b"); }
        }
    }
}