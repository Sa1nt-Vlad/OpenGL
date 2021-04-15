using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGL
{
    public class ShapeExamples
    {
        public static void Cube()
        {
            GL.Begin(PrimitiveType.Quads);
            Painter.PaintCube(Vector3.Zero, 1);
            GL.End();
        }

        public static void Square()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Aqua);
            Painter.PaintSquare(
                new Vector3(0, 0, 0), 
                new Vector3(0, 0.5f, 0),
                new Vector3(0.5f, 0.5f, 0),
                new Vector3(0.5f, 0, 0));
            GL.End();
        }

        public static void Triangle()
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Coral);
            Painter.PaintTriangle(
                new Vector3(-0.5f, -0.5f, 0), 
                new Vector3(0, 0.8f, 0), 
                new Vector3(0.9f, 0, 0));
            GL.End();
        }

        public static void Polygon()
        {
            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(Color.Gold);
            Painter.PaintPolygon(new Vector3(-0.5f, -0.5f, 0), 
                new Vector3(0, 0.8f, 0), 
                new Vector3(0.9f, 0, 0), 
                new Vector3(0.7f, -0.1f, 0),
                new Vector3(0, -0.8f, 0));
            GL.End();
        }

        public static void Circle()
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.GreenYellow);
            Painter.PaintCircle();
            GL.End();
        }

        public static void Pyramid()
        {
            Painter.PaintRegularPyramid(Vector3.Zero, 1, 8);
        }

        public static void Cone()
        {
            Painter.PaintCone(Vector3.Zero, 1);
        }

        public static void Sphere()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.End();
        }

        public static void Spiral()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.End();
        }

        public static void Torus()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.End();
        }
    }
}