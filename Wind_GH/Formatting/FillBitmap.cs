using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Windows.Forms;
using GH_IO.Serialization;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Wind.Containers;
using Parrot.Containers;
using Parrot.Controls;
using Wind.Geometry.Curves;
using Wind.Types;
using Grasshopper.Kernel.Parameters;
using Wind.Graphics;
using Pollen.Collections;

namespace Wind_GH.Formatting
{
    public class FillBitmap : GH_Component
    {
        NumericUpDown DS = new NumericUpDown();
        public double ScaleFactor = 1.0;
        public int FillSpace = 0;
        public bool IsEmbedded = false;

        /// <summary>
        /// Initializes a new instance of the FillBitmap class.
        /// </summary>
        public FillBitmap()
          : base("ImageFill", "ImageFill", "---", "Aviary", "Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Wind Objects", GH_ParamAccess.item);
            pManager.AddGenericParameter("Bitmap", "B", "Bitmap", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Alignment", "A", "---", GH_ParamAccess.item,4);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Scaling", "S", "---", GH_ParamAccess.item, 1);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("Tiling", "T", "---", GH_ParamAccess.item, 4);
            pManager[4].Optional = true;
            pManager.AddNumberParameter("Rotation", "R", "---", GH_ParamAccess.item, 0);
            pManager[5].Optional = true;

            Param_Integer ParamA = (Param_Integer)pManager[2];
            ParamA.AddNamedValue("Top Left", 0);
            ParamA.AddNamedValue("Top Middle", 1);
            ParamA.AddNamedValue("Top Right", 2);
            ParamA.AddNamedValue("Center Left", 3);
            ParamA.AddNamedValue("Center Middle", 4);
            ParamA.AddNamedValue("Center Right", 5);
            ParamA.AddNamedValue("Bottom Right", 6);
            ParamA.AddNamedValue("Bottom Left", 7);
            ParamA.AddNamedValue("Bottom Right", 8);

            Param_Integer ParamB = (Param_Integer)pManager[3];
            ParamB.AddNamedValue("Fit", 0);
            ParamB.AddNamedValue("Fill", 1);
            ParamB.AddNamedValue("Stretch", 2);

            Param_Integer ParamC = (Param_Integer)pManager[4];
            ParamC.AddNamedValue("None", 0);
            ParamC.AddNamedValue("FlipX", 1);
            ParamC.AddNamedValue("FlipY", 2);
            ParamC.AddNamedValue("FlipXY", 3);
            ParamC.AddNamedValue("Tile", 4);
            

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
            // Declare variables
            IGH_Goo Element = null;
            IGH_Goo Z = null;
            int A = 6;
            int T = 4;
            int F = 1;
            double R = 0;

            // Access the input parameters 
            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref Z)) return;
            if (!DA.GetData(2, ref A)) return;
            if (!DA.GetData(3, ref F)) return;
            if (!DA.GetData(4, ref T)) return;
            if (!DA.GetData(5, ref R)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            Bitmap B = null;
            if (Z != null) { Z.CastTo(out B); }

            G.FillType = wGraphic.FillTypes.Bitmap;
            G.FillBitmap = new wImage(B, (wImage.FillSpace)FillSpace, IsEmbedded,(wImage.AlignMode)A,(wImage.FitMode)F,R);

            G.WpfFill = new wFillBitmap(B,A,F,T,R, ScaleFactor).FillBrush;
            G.CustomFills += 1;

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
                        }
                        break;
                    case "Hoopoe":
                        wShapeCollection Shapes = (wShapeCollection)W.Element;
                        Shapes.Graphics.FillType = wGraphic.FillTypes.Bitmap;
                        Shapes.Graphics.WpfFill = G.WpfFill;

                        Shapes.Graphics.FillBitmap = new wImage(B, (wImage.FillSpace)FillSpace, IsEmbedded, (wImage.AlignMode)A, (wImage.FitMode)F,R);

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

            DS.Minimum = 0.00M;
            DS.Maximum = 1.00M;
            DS.Value = (Decimal)ScaleFactor;
            DS.DecimalPlaces = 2;
            DS.Increment = 0.01M;

            DS.UpDownAlign = LeftRightAlignment.Left;

            Menu_AppendCustomItem(menu, DS);

            DS.ValueChanged -= (o, e) => { UpdateScaleValue(); };
            DS.ValueChanged += (o, e) => { UpdateScaleValue(); };

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Embedded", EmbedMode, true, IsEmbedded);

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Global", LocalSpace, true, (FillSpace == 0));
            Menu_AppendItem(menu, "Local", GlobalSpace, true, (FillSpace == 1));
            
        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("Space", FillSpace);
            writer.SetBoolean("Embed", IsEmbedded);
            writer.SetDouble("Scale", ScaleFactor);

            this.UpdateMessage();
            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            FillSpace = reader.GetInt32("Space");
            IsEmbedded = reader.GetBoolean("Embed");
            ScaleFactor = reader.GetDouble("Scale");

            this.UpdateMessage();
            this.ExpireSolution(true);
            return base.Read(reader);
        }

        private void UpdateScaleValue()
        {
            ScaleFactor = (double)DS.Value;
            
            this.ExpireSolution(true);
        }

        private void LocalSpace(Object sender, EventArgs e)
        {
            FillSpace = 0;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void GlobalSpace(Object sender, EventArgs e)
        {
            FillSpace = 1;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void EmbedMode(Object sender, EventArgs e)
        {
            IsEmbedded = !IsEmbedded;

            this.ExpireSolution(true);
        }
        

        private void UpdateMessage()
        {
            string[] Messages = { "Global", "Local" };
            Message = Messages[FillSpace];
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
                return Properties.Resources.Wind_Fill_Image;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f2907ab4-ad55-453c-9461-a705c78e69ec"); }
        }
    }
}