using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Containers;
using Wind.Utilities;
using Wind.Types;
using Wind.Scene;

using Parrot.Containers;
using Parrot.Displays;
using Parrot.Drawings;
using Rhino.DocObjects;
using Grasshopper.Kernel.Types;
using Wind.Geometry.Meshes;

namespace Parrot_GH.Drawings
{
    public class ViewOpenGLMesh : GH_Component
    {
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the ViewOpenGLMesh class.
        /// </summary>
        public ViewOpenGLMesh()
          : base("View OpenGL Mesh", "OpenGL Mesh", "---", "Aviary", "3D Scene")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {

            pManager.AddGenericParameter("Mesh", "M", "---", GH_ParamAccess.list);
            pManager.AddGenericParameter("Lights", "L", "---", GH_ParamAccess.list);
            pManager[1].Optional = true;
            pManager.AddGenericParameter("Camera", "C", "---", GH_ParamAccess.item);
            pManager[2].Optional = true;

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

            var pCtrl = new pViewMeshGL(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                if (Elements[C] != null)
                {
                    WindObject = Elements[C];
                    Element = (pElement)WindObject.Element;
                    pCtrl = (pViewMeshGL)Element.ParrotControl;
                }
            }
            else
            {
                pCtrl.SetProperties();
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            List<IGH_Goo> X = new List<IGH_Goo>();
            List<IGH_Goo> Y = new List<IGH_Goo>();
            IGH_Goo U = null;

            if (!DA.GetDataList(0, X)) return;
            if (!DA.GetDataList(1, Y)) return;
            if (!DA.GetData(2, ref U)) return;

            wCamera V = new wCamera();

            U.CastTo(out V);
            

            List<wMesh> Meshes = new List<wMesh>();
            foreach (IGH_Goo Obj in X)
            {
                wMesh M = new wMesh();
                Obj.CastTo(out M);
                Meshes.Add(M);
            }
            

            List<wLight> Lights = new List<wLight>();
            foreach (IGH_Goo Obj in Y)
            {
                wLight L = new wLight();
                Obj.CastTo(out L);
                Lights.Add(L);
            }

            pCtrl.AddMeshes(Meshes);
            pCtrl.RunSample();

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
            get { return GH_Exposure.quarternary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Parrot_ViewMeshGL;
            }
        }
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{1910a720-6078-4f2e-8772-c7ebfe6dde18}"); }
        }
    }
}