using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;

using Wind.Types;
using Wind.Containers;

using Parrot.Containers;
using Wind.Geometry.Curves;
using Parrot.Controls;
using System.Windows.Media;
using Wind.Graphics;
using GH_IO.Serialization;
using System.Windows.Forms;

namespace Wind_GH.Formatting
{
    public class FillGradientRadial : GH_Component
    {
        public int GradientType = 0;
        public int GradientSpace = 0;

        /// <summary>
        /// Initializes a new instance of the FillGradientRadial class.
        /// </summary>
        public FillGradientRadial()
          : base("Radial Gradient", "Radial", "---", "Aviary", "Format")
        {
            this.UpdateMessage();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Wind Objects", GH_ParamAccess.item);
            pManager[0].Optional = true;
            pManager.AddColourParameter("Colors", "C", "---", GH_ParamAccess.list);
            pManager.AddNumberParameter("Parameters", "P", "---", GH_ParamAccess.list);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Radius", "R", "---", GH_ParamAccess.item, 1.0);
            pManager[3].Optional = true;
            pManager.AddIntervalParameter("Location", "L", "---", GH_ParamAccess.item, new Interval(0.5,0.5));
            pManager[4].Optional = true;
            pManager.AddIntervalParameter("Focus", "F", "---", GH_ParamAccess.item, new Interval(0.5, 0.5));
            pManager[5].Optional = true;
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

            List<System.Drawing.Color> Colors = new List<System.Drawing.Color>();
            List<double> Parameters = new List<double>();
            double Radius = 1.0;
            Interval Location = new Interval(0.5, 0.5);
            Interval Focus = new Interval(0.5, 0.5);

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetDataList(1, Colors)) return;
            if (!DA.GetDataList(2, Parameters)) return;
            if (!DA.GetData(3, ref Radius)) return;
            if (!DA.GetData(4, ref Location)) return;
            if (!DA.GetData(5, ref Focus)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            G.FillType = wGraphic.FillTypes.RadialGradient;

            if (Parameters.Count < 1)
            {
                G.Gradient = new wGradient(Colors);
            }
            else
            {
                if (Parameters.Count < Colors.Count)
                {
                    for (int i = Parameters.Count; i < Colors.Count; i++)
                    {
                        Parameters.Add(Parameters[Parameters.Count - 1]);
                    }
                }

                G.Gradient = new wGradient(Colors, Parameters, new wDomain(Location.Min,Location.Max), new wDomain(Focus.Min,Focus.Max), Radius, (wGradient.GradientSpace)GradientSpace);
            }

            G.WpfFill = new wFillGradient(G.Gradient, GradientType).GrdBrush;

            W.Graphics = G;

            if (Element != null)
            {
                switch (W.Type)
                {
                    case "Parrot":
                        pElement E = (pElement)W.Element;
                        pControl C = (pControl)E.ParrotControl;
                        C.SetFill();

                        C.Graphics = G;
                        break;
                    case "Pollen":
                        switch (W.SubType)
                        {
                            case "DataPoint":
                                break;
                            case "DataSet":
                                break;
                        }
                        break;
                    case "Hoopoe":
                        wShapeCollection Shapes = (wShapeCollection)W.Element;
                        Shapes.Graphics.FillType = wGraphic.FillTypes.RadialGradient;

                        if (Parameters.Count < 1)
                        {
                            Shapes.Graphics.Gradient = new wGradient(Colors, new wDomain(Location.Min, Location.Max), new wDomain(Focus.Min, Focus.Max), Radius, (wGradient.GradientSpace)GradientSpace);
                        }
                        else
                        {
                            Shapes.Graphics.Gradient = new wGradient(Colors, Parameters, new wDomain(Location.Min, Location.Max), new wDomain(Focus.Min, Focus.Max), Radius, (wGradient.GradientSpace)GradientSpace);
                        }

                        W.Element = Shapes;
                        break;
                }
            }

            DA.SetData(0, W);
            DA.SetData(1, G);

        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Global", LocalSpace, true, (GradientSpace == 0));
            Menu_AppendItem(menu, "Local", GlobalSpace, true, (GradientSpace == 1));

            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Custom", ModeCustom, true, (GradientType == 0));
        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("Pattern", GradientType);
            writer.SetInt32("Space", GradientSpace);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            GradientType = reader.GetInt32("Pattern");
            GradientSpace = reader.GetInt32("Space");

            this.UpdateMessage();
            this.ExpireSolution(true);
            return base.Read(reader);
        }

        private void LocalSpace(Object sender, EventArgs e)
        {
            GradientSpace = 0;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void GlobalSpace(Object sender, EventArgs e)
        {
            GradientSpace = 1;

            this.ExpireSolution(true);
        }

        private void ModeCustom(Object sender, EventArgs e)
        {
            GradientType = 0;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void UpdateMessage()
        {
            string[] Messages = { "Global", "Local" };
            Message = Messages[GradientSpace];
        }

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
                return Properties.Resources.Wind_Gradient_Radial;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("15aa8427-501b-425b-bec5-5efeeb3da0fb"); }
        }
    }
}