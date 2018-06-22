using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Scene.Cameras;
using Wind.Presets;
using Grasshopper.Kernel.Parameters;

namespace Wind_GH.Collections
{
    public class CameraSets : GH_Component
    {

        wCamera SelectedCamera = new wCamera();
        List<wCamera> CameraSet = new List<wCamera>
            {
            wCameras.Top,
            wCameras.Bottom,
            wCameras.Left,
            wCameras.Right,
            wCameras.Front,
            wCameras.Back,
            wCameras.IsometricNE,
            wCameras.IsometricNW,
            wCameras.IsometricSE,
            wCameras.IsometricSW
        };

        /// <summary>
        /// Initializes a new instance of the CameraSets class.
        /// </summary>
        public CameraSets()
          : base("Camera Presets", "Cameras", "---", "Aviary", "Presets")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Camera", "C", "---", GH_ParamAccess.item, 0);

            pManager.AddNumberParameter("Distance", "D", "---", GH_ParamAccess.item,100);
            pManager.AddNumberParameter("Lense Length", "L", "---", GH_ParamAccess.item, 0);

            Param_Integer param = (Param_Integer)Params.Input[0];
            param.AddNamedValue(CameraSet[0].Name, 0);
            param.AddNamedValue(CameraSet[1].Name, 1);
            param.AddNamedValue(CameraSet[2].Name, 2);
            param.AddNamedValue(CameraSet[3].Name, 3);
            param.AddNamedValue(CameraSet[4].Name, 4);
            param.AddNamedValue(CameraSet[5].Name, 5);

            param.AddNamedValue(CameraSet[6].Name, 6);
            param.AddNamedValue(CameraSet[7].Name, 7);
            param.AddNamedValue(CameraSet[8].Name, 8);
            param.AddNamedValue(CameraSet[9].Name, 9);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Camera", "C", "Camera Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int Index = 0;
            double D = 0;
            double L = 0;
            if (!DA.GetData(0, ref Index)) return;
            if (!DA.GetData(1, ref D)) return;
            if (!DA.GetData(2, ref L)) return;

            SelectedCamera = CameraSet[Index];

            SelectedCamera.SetLength(D);
            SelectedCamera.LensLength = L;

            DA.SetData(0, SelectedCamera);

            UpdateMessage();
        }

        private void UpdateMessage()
        {
            Message = SelectedCamera.Name;
        }


        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Wind_Cameras;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("d1525a23-9162-46a1-9a64-c56c2958a871"); }
        }
    }
}