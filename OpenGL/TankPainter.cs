using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGL
{
    public class TankPainter
    {
        public static void DrawTank(double turretAngle)
        {
            DrawCorpus();
        }

        private static void DrawTower()
        {
            
        }

        private static void DrawGun()
        {
            
        }

        private static void DrawTrack()
        {
            
        }

        private static void DrawRollers()
        {
            
        }

        private static void DrawCorpus()
        {
            GL.Color3(Color.Gray);

            var points = new[]
            {
                /* 0 */ Vector3.Zero,
                /* 1 */ new Vector3(0.2f, 0, -1), 
                /* 2 */ new Vector3(0.2f, 0.1f, -0.8f), 
                /* 3 */ new Vector3(-0.2f, 0.1f, -0.8f),
                /* 4 */ new Vector3(-0.2f, 0, -1),
                /* 5 */ new Vector3(0.3f, 0.1f, -0.8f),
                /* 6 */ new Vector3(0.3f, 0.15f, -0.7f),
                /* 7 */ new Vector3(-0.3f, 0.15f, -0.7f),
                /* 8 */ new Vector3(-0.3f, 0.1f, -0.8f),
                /* 9 */ new Vector3(-0.3f, 0.2f, -0.65f),
                /* 10 */ new Vector3(0.3f, 0.2f, -0.65f),
            };
            
            
            // DrawSquare(
            //     new Vector3(0.2f, 0, -1), 
            //     new Vector3(-0.2f, 0, -1), 
            //     new Vector3(-0.2f, -0.2f, -0.7f), 
            //     new Vector3(0.2f, -0.2f, -0.7f));

            GL.Begin(PrimitiveType.Quads);
            MakeSquare(points[1], points[2], points[3], points[4]);
            MakeSquare(points[5], points[6], points[7], points[8]);
            MakeSquare(points[6], points[10], points[9], points[7]);
            GL.End();
        }

        private static void DrawSquare(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4)
        {
            GL.Begin(PrimitiveType.Quads);
            Painter.PaintSquare(point1, point2, point3, point4);
            GL.End();
        }
        
        private static void MakeSquare(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4)
        {
            Painter.PaintSquare(point1, point2, point3, point4);
        }
    }
}