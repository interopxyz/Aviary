using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Grasshopper.Kernel.Parameters;
using System.Drawing;

using Xceed.Wpf.Toolkit;

using Wind.Containers;
using Wind.Utilities;

using Parrot.Collections;
using Parrot.Containers;
using Parrot.Controls;

namespace Parrot_GH.Controls
{
    public class ColorPicker : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the ColorPicker class.
        /// </summary>
        public ColorPicker()
          : base("ColorPicker", "Picker", "---", "Aviary", "Dashboard Control")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pColorSets ClrSets = new pColorSets();
            pManager.AddColourParameter("Color", "C", "Color", GH_ParamAccess.item, Color.Transparent);
            pManager[0].Optional = true;
            pManager.AddIntegerParameter("Color Modes", "M", "---", GH_ParamAccess.item, 1);
            pManager[1].Optional = true;
            pManager.AddColourParameter("Custom Palette", "P", "Custom Palette", GH_ParamAccess.list, ClrSets.Standard);
            pManager[2].Optional = true;
            pManager.AddColourParameter("Standard Colors", "S", "Standard Colors", GH_ParamAccess.list, ClrSets.Standard);
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Custom", 0);
            param.AddNamedValue("RGB Range", 1);

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

            var pCtrl = new pColorPicker(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pColorPicker)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            Color D = Color.Transparent;
            List<Color> S = new List<Color>();
            List<Color> K = new List<Color>();
            int M = 1;

            if (!DA.GetData(0, ref D)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetDataList(2, K)) return;
            if (!DA.GetDataList(3, S)) return;

            pColorSets ClrSets = new pColorSets();
            
            if (S.Count < 1) { S = ClrSets.Standard; }
            if (K.Count < 1) { K = ClrSets.Standard; }

            pCtrl.SetProperties(D, S, K, M);

            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pCtrl.Element, pCtrl, pCtrl.Type, 1); }
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
            get { return GH_Exposure.secondary; }
        }


        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Parrot_ColorPicker02;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{5497d4b8-1a3c-4e56-9494-83769a5dd42b}"); }
        }
    }
}