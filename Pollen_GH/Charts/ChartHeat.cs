﻿using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Wind.Containers;
using Wind.Utilities;
using Parrot.Containers;
using Pollen.Charts;
using Grasshopper.Kernel.Types;
using Pollen.Collections;
using System.Drawing;

namespace Pollen_GH.Charts
{
    public class ChartHeat : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();
        
        /// <summary>
        /// Initializes a new instance of the ChartHeat class.
        /// </summary>
        public ChartHeat()
          : base("Heat Chart", "Heat Chart", "---", "Aviary", "Chart")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "D", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Graphic", "G", "---", GH_ParamAccess.item);
            pManager[1].Optional = true;
            
            List<Color> TempColor = new List<Color>();
            TempColor.Add(Color.LightBlue);
            TempColor.Add(Color.CornflowerBlue);

            wGraphic TempGraphic = new wGraphic();
            TempGraphic.Gradient = new Wind.Types.wGradient(TempColor);
            pManager[1].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0),0, TempGraphic);

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

            var pControl = new pHeatChart(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pControl = (pHeatChart)Element.PollenControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            IGH_Goo D = null;
            IGH_Goo G = null;

            if (!DA.GetData(0, ref D)) return;
            if (!DA.GetData(1, ref G)) return;

            wObject W = new wObject();
            wGraphic GR = new wGraphic();
            D.CastTo(out W);
            G.CastTo(out GR);


            DataSetCollection DC = (DataSetCollection)W.Element;

            pControl.SetProperties(DC);
            pControl.SetHeatData();

            if (G != null)
            {
                pControl.SetGradient(GR);
            }
            
            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pControl.Element, pControl, pControl.Type); }
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
                return Properties.Resources.Pollen_Heat;
            }
        }
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{124a3285-4145-4991-935d-76a0498de328}"); }
        }
    }
}