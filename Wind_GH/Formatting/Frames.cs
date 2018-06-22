using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Parrot.Controls;
using Parrot.Containers;
using Wind.Geometry.Curves;
using System.Windows.Forms;
using GH_IO.Serialization;
using Grasshopper.Kernel.Parameters;
using Wind.Utilities;
using Parrot.Displays;

namespace Wind_GH.Formatting
{
    public class Frames : GH_Component
    {
        bool CompositeMode = false;

        /// <summary>
        /// Initializes a new instance of the AnimationFrame class.
        /// </summary>
        public Frames()
          : base("Animation", "Animation", "---", "Aviary", "2D Format")
        {
            this.UpdateMessage();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Wind Objects", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Index", "I", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Span", "S", "---", GH_ParamAccess.item, 0.333);
            pManager[2].Optional = true;
            
            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.PersistentData.Append(new GH_ObjectWrapper(new pSpacer(new GUIDtoAlpha(Convert.ToString(this.Attributes.InstanceGuid.ToString() + Convert.ToString(this.RunCount)), false).Text)));
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            pManager.AddGenericParameter("Graphics", "G", "Graphics Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IGH_Goo Element = null;
            int FrameIndex = 0;
            double FrameDuration = 0.333;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref FrameIndex)) return;
            if (!DA.GetData(2, ref FrameDuration)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            string LayerName = FrameIndex.ToString();

            G.Layer = LayerName;

            W.Graphics = G;

            switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;
                    pControl C = (pControl)E.ParrotControl;
                    C.Graphics = G;

                    break;
                case "Pollen":
                    switch (W.SubType)
                    {

                    }
                    break;
                case "Hoopoe":
                    wShapeCollection Shapes = (wShapeCollection)W.Element;

                    Shapes.Group = LayerName;
                    Shapes.Graphics.Layer = LayerName;

                    Shapes.Frame = new wFrames(FrameIndex, FrameDuration, CompositeMode);

                    W.Element = Shapes;
                    break;
            }

            DA.SetData(0, W);
            DA.SetData(1, G);
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);
            
            Menu_AppendItem(menu, "Stack", SetCompositeMode, true, CompositeMode);
        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetBoolean("Composite", CompositeMode);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            CompositeMode = reader.GetBoolean("Composite");

            this.UpdateMessage();
            this.ExpireSolution(true);
            return base.Read(reader);
        }

        private void SetCompositeMode(Object sender, EventArgs e)
        {
            CompositeMode = !CompositeMode;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }


        private void UpdateMessage()
        {
            if (CompositeMode) { Message = "Stack"; } else { Message = "Step"; }
        }

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
                return Properties.Resources.Wind_Animation;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f8c67260-8e7c-40d9-9f9a-9479189c8caa"); }
        }
    }
}