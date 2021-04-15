using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGL
{
    public class Painter
    {
        public static void PaintCube(Vector3 center, float sideSize)
        {
            var step = sideSize / 2;
            
            var leftFBCorner = new Vector3(center.X - step, center.Y - step, center.Z - step);
            var leftFTCorner = new Vector3(center.X - step, center.Y + step, center.Z - step);
            var rightFTCorner = new Vector3(center.X + step, center.Y + step, center.Z - step);
            var rightFBCorner = new Vector3(center.X + step, center.Y - step, center.Z - step); 
            
            var leftBBCorner = new Vector3(center.X - step, center.Y - step, center.Z + step);
            var leftBTCorner = new Vector3(center.X - step, center.Y + step, center.Z + step);
            var rightBTCorner = new Vector3(center.X + step, center.Y + step, center.Z + step);
            var rightBBCorner = new Vector3(center.X + step, center.Y - step, center.Z + step); 

            // front side
            GL.Color3(Color.IndianRed);
            PaintSquare(leftFBCorner, leftFTCorner, rightFTCorner, rightFBCorner);
            
            // back side
            GL.Color3(Color.Coral);
            PaintSquare(leftBTCorner, rightBTCorner, rightBBCorner, leftBBCorner);
            
            // bottom side
            GL.Color3(Color.LimeGreen);
            PaintSquare(leftFBCorner, leftBBCorner, rightBBCorner, rightFBCorner);
            
            // top side
            GL.Color3(Color.Aquamarine);
            PaintSquare(leftFTCorner, rightFTCorner, rightBTCorner, leftBTCorner);
            
            // right side
            GL.Color3(Color.DodgerBlue);
            PaintSquare(rightFBCorner, rightFTCorner, rightBTCorner, rightBBCorner);
            
            // left side
            GL.Color3(Color.YellowGreen);
            PaintSquare(leftFBCorner, leftFTCorner, leftBTCorner, leftBBCorner);
        }

        public static void PaintSquare(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4)
        {
            GL.Vertex3(point1);
            GL.Vertex3(point2);
            GL.Vertex3(point3);
            GL.Vertex3(point4);
        }
        
        public static void PaintTriangle(Vector3 point1, Vector3 point2, Vector3 point3)
        {
            GL.Vertex3(point1);
            GL.Vertex3(point2);
            GL.Vertex3(point3);
        }

        public static void PaintPolygon(params Vector3[] points)
        {
            foreach (var point in points) 
                GL.Vertex3(point);
        }
        
        public static void PaintCircle()
        {
            PaintRegularFigure(360);
        }

        public static void PaintRegularFigure(int pointsCount)
        {
            var angle = 360 / pointsCount;

            var x = 0f;
            var y = 1f;
            
            for (int i = 0; i < 360; i += angle)
            {
                x = (float) (x * Math.Cos(i) - y * Math.Sin(i));
                y = (float) (x * Math.Sin(i) + y * Math.Cos(i));
                GL.Vertex2(x, y);    
            }
        }
        
        public static void PaintPyramid()
        {

        }
        
        public static void PaintTruncatedPyramid()
        {

        }
        
        public static void PaintCone()
        {

        }
        
        public static void PaintSphere()
        {

        }
        
        public static void PaintSpiral()
        {

        }
        
        public static void PaintTorus()
        {

        }
    }
}