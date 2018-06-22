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
using Pollen.Collections;
using Parrot.Displays;
using Wind.Utilities;
using Pollen.Charts;

namespace Wind_GH.Formatting
{
    public class FillGradientLinear : GH_Component
    {
        public int GradientType = 0;
        public int GradientSpace = 0;

        /// <summary>
        /// Initializes a new instance of the FillGradient class.
        /// </summary>
        public FillGradientLinear()
          : base("Linear Gradient", "Linear", "---", "Aviary", "2D Format")
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
            pManager.AddNumberParameter("Angle", "A", "---", GH_ParamAccess.item,0);
            pManager[3].Optional = true;

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

            List<System.Drawing.Color> Colors = new List<System.Drawing.Color>();
            List<double> Parameters = new List<double>();
            double GradientAngle = 0;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetDataList(1, Colors)) return;
            if (!DA.GetDataList(2, Parameters)) return;
            if (!DA.GetData(3, ref GradientAngle)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            G.FillType = wGraphic.FillTypes.LinearGradient;
            G.CustomFills += 1;

            if (Parameters.Count < 1)
            { 
            G.Gradient = new wGradient(Colors);
            }
            else
            {
                if (Parameters.Count<Colors.Count)
                {
                    for(int i = Parameters.Count;i<Colors.Count;i++)
                    {
                        Parameters.Add(Parameters[Parameters.Count - 1]);
                    }
                }

                    G.Gradient = new wGradient(Colors,Parameters,GradientAngle, (wGradient.GradientSpace)GradientSpace);
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
                                DataPt tDataPt = (DataPt)W.Element;
                                tDataPt.Graphics = G;

                                tDataPt.Graphics.WpfFill = G.WpfFill;
                                tDataPt.Graphics.WpfPattern = G.WpfPattern;

                                W.Element = tDataPt;
                                break;
                            case "DataSet":
                                DataSetCollection tDataSet = (DataSetCollection)W.Element;
                                tDataSet.Graphics = G;

                                tDataSet.Graphics.WpfFill = G.WpfFill;
                                tDataSet.Graphics.WpfPattern = G.WpfPattern;

                                W.Element = tDataSet;
                                break;
                            case "Chart":
                            case "Table":

                                pElement pE = (pElement)W.Element;
                                pChart pC = pE.PollenControl;
                                pC.Graphics = G;

                                pC.Graphics.WpfFill = G.WpfFill;
                                pC.Graphics.WpfPattern = G.WpfPattern;

                                pC.SetGradientFill();

                                pE.PollenControl = pC;
                                W.Element = pE;
                                break;
                        }
                        break;
                    case "Hoopoe":
                        wShapeCollection Shapes = (wShapeCollection)W.Element;
                        Shapes.Graphics.FillType = wGraphic.FillTypes.LinearGradient;

                        wGradient GRD = new wGradient();

                        if (Parameters.Count < 1)
                        {
                            GRD = new wGradient(Colors, GradientAngle, (wGradient.GradientSpace)GradientSpace);
                        }
                        else
                        {
                            GRD = new wGradient(Colors, Parameters, GradientAngle, (wGradient.GradientSpace)GradientSpace);
                        }

                        Shapes.Graphics.Gradient = GRD;
                        Shapes.Graphics.WpfFill = new wFillGradient(G.Gradient, GradientType).GrdBrush;



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
                return Properties.Resources.Parrot_Gradient;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{666b9109-9195-4a65-8fa3-b590e59d78fd}"); }
        }
    }
}