using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Utilities;
using Wind.Containers;

using Parrot.Containers;
using Parrot.Controls;

namespace Parrot_GH.Controls
{
  public class TextBox : GH_Component
  {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the TextBox class.
        /// </summary>
        public TextBox()
      : base("TextBox", "TextBox", "Parrot Control Element. Field for input or editing of unformatted text.", "Aviary", "Control")
    {
    }

    /// <summary>
    /// Registers all the input parameters for this component.
    /// </summary>
    protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
    {
            pManager.AddTextParameter("Text", "T", "Initial text", GH_ParamAccess.item, "");
            pManager[0].Optional = true;
            pManager.AddBooleanParameter("Wrap", "W", "Toggle which if true will wrap the text to the next line when it hits with width size limit.", GH_ParamAccess.item, true);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Width", "S", "Optional limit on the width of the text box.", GH_ParamAccess.item, 300);
            pManager[2].Optional = true;
        }

    /// <summary>
    /// Registers all the output parameters for this component.
    /// </summary>
    protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
    {
            pManager.AddGenericParameter("Elements", "E", "Parrot WPF Control Element", GH_ParamAccess.item);
    }

    /// <summary>
    /// This is the method that actually does the work.
    /// </summary>
    /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
    protected override void SolveInstance(IGH_DataAccess DA)
        {
            string ID = this.Attributes.InstanceGuid.ToString();
            string name = new GUIDtoAlpha(Convert.ToString(ID + Convert.ToString(this.RunCount)),true).Text;
            int C = this.RunCount;

            wObject WindObject = new wObject();
            pElement Element = new pElement();
            bool Active = Elements.ContainsKey(C);

            var pCtrl = new pTextBox(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if(Active){
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pTextBox)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties
            string Text = "";
            bool HasText = false;
            bool Wraps = false;
            double Width = 0;

            if (!DA.GetData(0, ref Text)) return;
            if (!DA.GetData(1, ref Wraps)) return;
            if (!DA.GetData(2, ref Width)) return;

            if (Text != "") { HasText = true; }

            pCtrl.SetProperties(Text, HasText,Wraps,Width);

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
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
    {
      get
      {
        return Properties.Resources.Parrot_TextBox;
      }
    }

    /// <summary>
    /// Gets the unique ID for this component. Do not change this ID after release.
    /// </summary>
    public override Guid ComponentGuid
    {
      get { return new Guid("{42192f42-0b95-46b1-adc3-9746295f90c2}"); }
    }
  }
}