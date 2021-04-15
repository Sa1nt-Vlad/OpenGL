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
            Painter.PaintTriangle(
                new Vector3(-0.5f, -0.5f, 0), 
                new Vector3(0, 0.8f, 0), 
                new Vector3(0.9f, 0, 0));
            GL.End();
        }

        public static void Polygon()
        {
            GL.Begin(PrimitiveType.Polygon);
            Painter.PaintPolygon(new Vector3(-0.5f, -0.5f, 0), 
                new Vector3(0, 0.8f, 0), 
                new Vector3(0.9f, 0, 0), 
                new Vector3(0.7f, -0.1f, 0),
                new Vector3(0, -0.8f, 0));
            GL.End();
        }

        public static void Circle()
        {
            GL.Begin(PrimitiveType.Polygon);
            Painter.PaintCircle();
            GL.End();
        }

        public static void Pyramid()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.End();
        }

        public static void TruncatedPyramid()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.End();
        }

        public static void Cone()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.End();
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