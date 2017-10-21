using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

using SharpGL;
using SharpGL.WPF;
using SharpGL.SceneGraph.Shaders;
using SharpGL.SceneGraph.Primitives;
using Wind.Geometry.Meshes;
using Wind.Types;

namespace Parrot.Drawings
{
    public class xViewMeshGL
    {
        public Canvas Element = new Canvas();
        public string Type;

        public OpenGLControl ControlGL = new OpenGLControl();
        public OpenGL ObjectGL = new OpenGL();

        public List<wMesh> Meshes = new List<wMesh>();

        ShaderProgram program = new ShaderProgram();

        public xViewMeshGL()
        {
            Type = "OpenGL";
            Element = new Canvas();

        }

        public xViewMeshGL(string InstanceName)
        {
            Type = "OpenGL";
            Element = new Canvas();
            Element.Name = InstanceName;

        }


        public void SetProperties()
        {

            ControlGL.Width = 600;
            ControlGL.Height = 600;

            Element.Width = 600;
            Element.Height = 600;

            Element.Children.Clear();
            Element.Children.Add(ControlGL);

        }

        public void RunSample()
        {
            ControlGL.OpenGLDraw -= (o, e) => { OpenGLControl_OpenGLDraw(); };
            ControlGL.OpenGLDraw += (o, e) => { OpenGLControl_OpenGLDraw(); };
        }

        private void OpenGLControl_OpenGLDraw()
        {
            //  Get the OpenGL instance that's been passed to us.
            ObjectGL = ControlGL.OpenGL;

            //  Clear the color and depth buffers.
            ObjectGL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            ObjectGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            ObjectGL.Enable(OpenGL.GL_DEPTH_TEST);


            //  Reset the modelview matrix.
            ObjectGL.LoadIdentity();

            //  Move the geometry into a fairly central position.
            ObjectGL.Translate(0.0f, 0.0f, -3.0f);

            //  Start drawing triangles.
            wMesh Mesh = Meshes[0];

            SetMesh(Mesh);

            //  Flush OpenGL.
            ObjectGL.Flush();

        }

        public void AddMeshes(List<wMesh> InputMeshes)
        {
            Meshes.Clear();
            Meshes.AddRange(InputMeshes);
        }

        public void SetMesh(wMesh Mesh)
        {
            ObjectGL.Begin(OpenGL.GL_TRIANGLES);
            foreach (wFace F in Mesh.Faces)
            {

                wVertex V = Mesh.Vertices[F.A];
                wColor C = Mesh.Colors[F.A];
                ObjectGL.Color((float)((double)C.R / 255.0), (float)((double)C.G / 255.0), (float)((double)C.B / 255.0));
                ObjectGL.Vertex((float)V.X, (float)V.Y, (float)V.Z);

                V = Mesh.Vertices[F.B];
                C = Mesh.Colors[F.B];
                ObjectGL.Color((float)((double)C.R / 255.0), (float)((double)C.G / 255.0), (float)((double)C.B / 255.0));
                ObjectGL.Vertex((float)V.X, (float)V.Y, (float)V.Z);

                V = Mesh.Vertices[F.C];
                C = Mesh.Colors[F.C];
                ObjectGL.Color((float)((double)C.R / 255.0), (float)((double)C.G / 255.0), (float)((double)C.B / 255.0));
                ObjectGL.Vertex((float)V.X, (float)V.Y, (float)V.Z);

            }
            ObjectGL.End();

            ObjectGL.Disable(OpenGL.GL_CULL_FACE);
        }

    }
}
