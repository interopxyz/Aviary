using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;

using Wind.Containers;
using Wind.Utilities;

using Parrot.Containers;
using Parrot.Layouts;
using Grasshopper.Kernel.Types;

namespace Parrot_GH.Layouts
{
    public class PanelWrap : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the WrapPanel class.
        /// </summary>
        public PanelWrap()
          : base("WrapPanel", "Wrap", "---", "Aviary", "Dashboard Layout")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "E", "Elements", GH_ParamAccess.list);
            pManager.AddBooleanParameter("Horizontal", "H", "---", GH_ParamAccess.item, true);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Align", "A", "Align", GH_ParamAccess.item, 1);
            pManager[2].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[2];
            param.AddNamedValue("Left / Top", 1);
            param.AddNamedValue("Middle / Center", 2);
            param.AddNamedValue("Right / Bottom", 3);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "E", "---", GH_ParamAccess.item);
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

            var pCtrl = new pPanelWrap(name);

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pPanelWrap)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            List<IGH_Goo> X = new List<IGH_Goo>();
            int A = 0;
            bool H = true;

            // Access the input parameters 
            if (!DA.GetDataList(0, X)) return;
            if (!DA.GetData(1, ref H)) return;
            if (!DA.GetData(2, ref A)) return;

            pCtrl.SetProperties(H,A);

            wObject W;
            pElement E;

            foreach (IGH_Goo Y in X)
            {
                Y.CastTo(out W);
                E = (pElement)W.Element;
                pCtrl.AddElement(E);
            }


            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pCtrl.Element, pCtrl, pCtrl.Type); }
            WindObject = new wObject(Element, "Parrot", Element.Type);
            WindObject.GUID = this.InstanceGuid;
            WindObject.Instance = C;

            Elements[this.RunCount] = WindObject;

            DA.SetData(0, WindObject);
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
                return Properties.Resources.Parrot_Wrap_W;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{4b9a4c90-7cad-463d-bb80-2e125f5711b0}"); }
        }
    }
}