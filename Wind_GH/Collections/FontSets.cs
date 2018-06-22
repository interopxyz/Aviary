using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Types;
using Wind.Containers;
using Wind.Presets;
using Grasshopper.Kernel.Parameters;

namespace Wind_GH.Collections
{
    public class FontSets : GH_Component
    {

        wFont SelectedFont = new wFont();
        List<wFont> FontSet = new List<wFont>
            {
            wFonts.Bold,
            wFonts.Title,
            wFonts.SubTitle,
            wFonts.Text,
            wFonts.Subtext
        };

        /// <summary>
        /// Initializes a new instance of the FontSets class.
        /// </summary>
        public FontSets()
          : base("Font Presets", "Fonts", "---", "Aviary", "Presets")
        {
        }


        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Gradient", "G", "---", GH_ParamAccess.item, 0);

            Param_Integer param = (Param_Integer)Params.Input[0];
            param.AddNamedValue(FontSet[0].Title, 0);
            param.AddNamedValue(FontSet[1].Title, 1);
            param.AddNamedValue(FontSet[2].Title, 2);
            param.AddNamedValue(FontSet[3].Title, 3);
            param.AddNamedValue(FontSet[4].Title, 4);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "N", "---", GH_ParamAccess.item);
            pManager.AddNumberParameter("Size", "S", "---", GH_ParamAccess.item);
            pManager.AddColourParameter("Color", "C", "---", GH_ParamAccess.item);
            pManager.AddNumberParameter("Styling", "X", "---", GH_ParamAccess.list);
            pManager.AddGenericParameter("Font", "F", "Font Object", GH_ParamAccess.item);
            pManager.AddGenericParameter("Graphics", "G", "Graphics Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int Index = 0;
            if (!DA.GetData(0, ref Index)) return;

            SelectedFont = FontSet[Index];

            wGraphic G = new wGraphic();
            G.FontObject = SelectedFont;

            DA.SetData(0, SelectedFont.Name);
            DA.SetData(1, SelectedFont.Size);
            DA.SetData(2, SelectedFont.FontColor.ToDrawingColor());
            DA.SetDataList(3, new List<bool>{ SelectedFont.IsBold, SelectedFont.IsItalic , SelectedFont.IsUnderlined });

            DA.SetData(4, SelectedFont);
            DA.SetData(5, G);

            UpdateMessage();
        }

        private void UpdateMessage()
        {
            Message = SelectedFont.Title;
        }


        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Wind_Presets_Gradients;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("37c8e93f-8d19-4732-b2d2-b681497a4d29"); }
        }
    }
}