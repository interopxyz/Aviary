using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Rhino.Geometry;
using Rhino.Display;
using Rhino.DocObjects;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;

using Wind.Geometry.Meshes;
using Wind.Scene;
using Wind.Geometry.Vectors;
using Wind.Types;
using Wind_GH.Types;
using Grasshopper.Kernel.Parameters;

namespace Wind_GH.Geometry
{
    public class SetMesh : GH_Component
    {
        DisplayMaterial Shader = new DisplayMaterial();
        int ShaderMode = 0;

        /// <summary>
        /// Initializes a new instance of the BuildMesh class.
        /// </summary>
        public SetMesh()
          : base("Set Mesh", "Set Mesh", "---", "Aviary", "3D Scene")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            Shader = MaterialWhite();

            pManager.AddMeshParameter("Mesh", "M", "---", GH_ParamAccess.item);
            pManager.AddPlaneParameter("Plane", "P", "---", GH_ParamAccess.item,Plane.WorldXY);
            pManager[1].Optional = true;

            pManager.AddGenericParameter("Shader", "S", "---", GH_ParamAccess.item);
            pManager[2].Optional = true;
            UpdateMaterial(2);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("WindMesh", "M", "A Wind Mesh Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            Mesh M = new Mesh();
            Plane P = Plane.WorldXY;
            IGH_Goo X = null;
            
            if (Params.Input[2].VolatileDataCount < 1) { UpdateMaterial(2); }

            if (!DA.GetData(0, ref M)) return;
            if (!DA.GetData(1, ref P)) return;
            if (!DA.GetData(2, ref X)) return;
            
            X.CastTo(out Shader);
            wShader Sh = new wShader();

            Sh.DiffuseColor = new wColor(Shader.Diffuse);
            Sh.SpecularColor = new wColor(Shader.Specular);
            Sh.SpecularValue = Shader.Shine;
            Sh.EmissiveColor = new wColor(Shader.Emission);
            Sh.Transparency = (1.0 - Shader.Transparency);
            Sh.SetDiffuseTransparency();

            M.Faces.ConvertQuadsToTriangles();
            M.Normals.ComputeNormals();

            if (M.VertexColors.Count != M.Vertices.Count) { M.VertexColors.CreateMonotoneMesh(System.Drawing.Color.LightGray); }
            
            wMesh MeshObject = new wMesh();

            MeshObject.Material = Sh;

            for (int i = 0; i < M.Vertices.Count; i++)
            {
                MeshObject.AddVertex(M.Vertices[i].X, M.Vertices[i].Y, M.Vertices[i].Z,i,new wColor(M.VertexColors[i]));
                MeshObject.AddNormal(M.Normals[i].X, M.Normals[i].Y, M.Normals[i].Z,i);
            }
            
            for (int i = 0; i < M.Faces.Count; i++)
            {
                MeshObject.AddTriangularFace(M.Faces[i].A, M.Faces[i].B, M.Faces[i].C,i);
                MeshObject.AddFaceNormal(M.FaceNormals[i].X, M.FaceNormals[i].Y, M.FaceNormals[i].Z,i);
            }

            for (int i = 0; i < M.TopologyEdges.Count; i++)
            {
                MeshObject.AddEdges(M.TopologyVertices.MeshVertexIndices(M.TopologyEdges.GetTopologyVertices(i).I)[0], M.TopologyVertices.MeshVertexIndices(M.TopologyEdges.GetTopologyVertices(i).J)[0]);
            }

            DA.SetData(0, MeshObject);

        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "White", ModeMaterialWhite, true, (ShaderMode == 0));
            Menu_AppendItem(menu, "Light Gray", ModeMaterialLightGray, true, (ShaderMode == 1));
            Menu_AppendItem(menu, "Ghosted", ModeMaterialGhosted, true, (ShaderMode == 2));

        }

        //SET MATERIALS
        public void UpdateMaterial(int index)
        {

            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[index];
            paramGen.PersistentData.ClearData();
            paramGen.PersistentData.Append(new GH_ObjectWrapper(Shader));

            Params.Input[index].ClearData();
            Params.Input[index].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, Shader);

        }

        private DisplayMaterial MaterialWhite()
        {
            return new DisplayMaterial(System.Drawing.Color.White);
        }

        private DisplayMaterial MaterialLightGray()
        {
            return new DisplayMaterial(System.Drawing.Color.LightGray);
        }

        private DisplayMaterial MaterialGhosted()
        {
            return new DisplayMaterial(System.Drawing.Color.White, 0.5);
        }

        //MATERIAL PRESETS
        private void ModeMaterialWhite(Object sender, EventArgs e)
        {
            Shader = MaterialWhite();
            UpdateMaterial(2);

            ShaderMode = 0;
            this.ExpireSolution(true);
        }

        private void ModeMaterialLightGray(Object sender, EventArgs e)
        {
            Shader =  MaterialLightGray();
            UpdateMaterial(2);

            ShaderMode = 1;
            this.ExpireSolution(true);
        }

        private void ModeMaterialGhosted(Object sender, EventArgs e)
        {
            Shader = MaterialGhosted();
            UpdateMaterial(2);

            ShaderMode = 2;
            this.ExpireSolution(true);
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
                return Properties.Resources.Wind_Mesh3D;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{1934b196-dee1-4794-ba86-06212153c351}"); }
        }
    }
}