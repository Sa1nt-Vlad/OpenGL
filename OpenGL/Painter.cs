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
            
            GL.Begin(PrimitiveType.Quads);
            
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
            
            GL.End();
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
            var center = Vector2.Zero;
            
            var x = 0f;
            var y = 1f;
            var newX = x;
            var newY = y;
            
            GL.Begin(PrimitiveType.Triangles);
            for (int i = 0; i <= 360; i += angle)
            {
                GL.Vertex2(center);
                GL.Vertex2(newX, newY);
                
                var radians = i * (Math.PI / 180);
                newX = (float) (x * Math.Cos(radians) - y * Math.Sin(radians));
                newY = (float) (x * Math.Sin(radians) + y * Math.Cos(radians));
                
                GL.Vertex2(newX, newY);
            }
            GL.End();
        }

        public static void PaintRegularPyramid(Vector3 center, float sideSize, int sideCount)
        {
            var angle = 360 / sideCount;
            var step = sideSize / 2;
            var bottomCenter = new Vector3(center.X, center.Y - step, center.Z);
            
            var x = center.X - step;
            var z = center.Z - step;
            var newX = x;
            var newZ = z;
            
            var bottomPoints = new Vector3[sideCount];
            
            
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Coral);
            
            // bottom drawing
            for (int i = 0; i <= 360; i += angle)
            {
                GL.Vertex3(bottomCenter);
                GL.Vertex3(newX, bottomCenter.Y, newZ);

                var radians = i * (Math.PI / 180);
                newX = (float) (x * Math.Cos(radians) - z * Math.Sin(radians));
                newZ = (float) (x * Math.Sin(radians) + z * Math.Cos(radians));
                
                if (i != 360) bottomPoints[i / angle] = new Vector3(newX, bottomCenter.Y, newZ);
                
                GL.Vertex3(newX, bottomCenter.Y, newZ);
            }

            // sides drawing
            var top = new Vector3(center.X, center.Y + step, center.Z);
            var colors = new[] {Color.YellowGreen, Color.Gold, Color.DodgerBlue, Color.IndianRed};
            for (int i = 0; i < bottomPoints.Length; i++)
            {
                GL.Color3(colors[i % colors.Length]);
                PaintTriangle(top, bottomPoints[i],
                    i + 1 == bottomPoints.Length ? bottomPoints[0] : bottomPoints[i + 1]);
            }
            
            GL.End();
        }

        public static void PaintCone(Vector3 center, float sideSize)
        {
            const int sideCount = 360;
            const int angle = 1;
            var step = sideSize / 2;
            var bottomCenter = new Vector3(center.X, center.Y - step, center.Z);
            
            var x = center.X - step;
            var z = center.Z - step;
            var newX = x;
            var newZ = z;
            
            var bottomPoints = new Vector3[sideCount];
            
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Coral);
            
            // bottom side drawing
            for (int i = 0; i <= 360; i += angle)
            {
                GL.Vertex3(bottomCenter);
                GL.Vertex3(newX, bottomCenter.Y, newZ);

                var radians = i * (Math.PI / 180);
                newX = (float) (x * Math.Cos(radians) - z * Math.Sin(radians));
                newZ = (float) (x * Math.Sin(radians) + z * Math.Cos(radians));
                
                if (i != 360) bottomPoints[i / angle] = new Vector3(newX, bottomCenter.Y, newZ);
                
                GL.Vertex3(newX, bottomCenter.Y, newZ);
            }

            var top = new Vector3(center.X, center.Y + step, center.Z);
            GL.Color3(Color.DodgerBlue);
            for (int i = 0; i < bottomPoints.Length; i++)
            {
                PaintTriangle(top, bottomPoints[i],
                    i + 1 == bottomPoints.Length ? bottomPoints[0] : bottomPoints[i + 1]);
            }
            
            GL.End();
        }
        
        public static void PaintSphere()
        {
            var x = 0f;
            var y = 0f;
            var z = 1f;
            
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.IndianRed);

            var step = 5;
            
            /*for (int u = -180; u <= 180; u += step)
            {
                for (int v = -90; v <= 90; v += step)
                {
                    GL.Vertex3(x, y, z);
                    var radU = u * (Math.PI / 180);
                    var radV = v * (Math.PI / 180);
                    x = (float) (Math.Cos(radU) * Math.Cos(radV));
                    y = (float) (Math.Sin(radU) * Math.Cos(radV));
                    z = (float) Math.Sin(radV);
                    
                    GL.Vertex3(x, y, z);
                }
            }*/

            for (int v = -90; v <= 90; v += step)
            {
                for (int u = -180; u <= 180; u += step)
                {
                    GL.Vertex3(x, y, z);
                    var radU = u * (Math.PI / 180);
                    var radV = v * (Math.PI / 180);
                    x = (float) (Math.Cos(radU) * Math.Cos(radV));
                    y = (float) (Math.Sin(radU) * Math.Cos(radV));
                    z = (float) Math.Sin(radV);
                    
                    GL.Vertex3(x, y, z);
                }
            }

            GL.End();
        }
        
        public static void PaintSpiral()
        {
            var x = 1f;
            var y = 0f;
            var z = 0f;
            
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.IndianRed);
            var isFirstPoint = true;
            
            for (int u = -360; u <= 360; u += 1)
            {
                for (int v = -180; v <= 180; v += 1)
                {
                    if (!isFirstPoint)
                        GL.Vertex3(x, y, z);
                    var radU = u * (Math.PI / 180);
                    var radV = v * (Math.PI / 180);
                    x = (float) (Math.Cos(radU) * (Math.Cos(radV) + 3));
                    y = (float) (Math.Sin(radU) * (Math.Cos(radV) + 3));
                    z = (float) (Math.Sin(radV) + radU);
                    
                    if (isFirstPoint)
                        GL.Vertex3(x, y, z);
                    GL.Vertex3(x, y, z);
                    isFirstPoint = false;
                }
            }
            
            GL.End();
        }
        
        public static void PaintLogarithmicSpiral()
        {
            var x = 1f;
            var y = 0f;
            var z = 0f;
            
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.IndianRed);
            var isFirstPoint = true;
            
            for (int u = 0; u <= 540; u += 3)
            {
                for (int v = -180; v <= 180; v += 3)
                {
                    if (!isFirstPoint)
                        GL.Vertex3(x, y, z);
                    var radU = u * (Math.PI / 180);
                    var radV = v * (Math.PI / 180);
                    x = (float) (radU * Math.Cos(radU) * (Math.Cos(radV) + 1));
                    y = (float) (radU * Math.Sin(radU) * (Math.Cos(radV) + 1));
                    z = (float) (radU * Math.Sin(radV));
                    
                    if (isFirstPoint)
                        GL.Vertex3(x, y, z);
                    GL.Vertex3(x, y, z);
                    isFirstPoint = false;
                }
            }
            
            GL.End();
        }

        public static void PaintTorus()
        {
            var x = 1f;
            var y = 0f;
            var z = 0f;
            
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.IndianRed);
            var isFirstPoint = true;
            
            for (int u = -180; u <= 180; u += 1)
            {
                for (int v = -180; v <= 180; v += 1)
                {
                    if (!isFirstPoint)
                        GL.Vertex3(x, y, z);
                    var radU = u * (Math.PI / 180);
                    var radV = v * (Math.PI / 180);
                    x = (float) (Math.Cos(radU) * (Math.Cos(radV) + 3));
                    y = (float) (Math.Sin(radU) * (Math.Cos(radV) + 3));
                    z = (float) Math.Sin(radV);
                    
                    if (isFirstPoint)
                        GL.Vertex3(x, y, z);
                    GL.Vertex3(x, y, z);
                    isFirstPoint = false;
                }
            }
            
            GL.End();
        }
    }
}