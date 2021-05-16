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
            GL.TexCoord2(0, 0);
            GL.Vertex3(point1);
            
            GL.TexCoord2(1, 0);
            GL.Vertex3(point2);
            
            GL.TexCoord2(1, 1);
            GL.Vertex3(point3);
            
            GL.TexCoord2(0, 1);
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
        
        public static void PaintCircle(Vector3 center, float radius)
        {
            var angle = 1;
            
            var x = center.X - radius;
            var y = center.Y;
            var newX = x;
            var newY = y;
            
            GL.Begin(PrimitiveType.Triangles);
            for (int i = 0; i <= 360; i += angle)
            {
                GL.Vertex3(center);
                GL.Vertex2(newX, newY);
                
                var radians = i * (Math.PI / 180);
                newX = (float) ((x - center.X) * Math.Cos(radians) - (y - center.Y) * Math.Sin(radians) + center.X);
                newY = (float) ((x - center.X) * Math.Sin(radians) + (y - center.Y) * Math.Cos(radians) + center.Y);
                
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

        #region Cylinder

        public static void PaintCylinder(Axis directAxis, Vector3 center, float radius, float height,
            Color facesColor, Color sideColor)
        {
            PaintCylinder(directAxis, center, radius, radius, height, facesColor, sideColor);
        }

        public static void PaintCylinder(Axis directAxis, Vector3 center, float radius1, float radius2, float height,
            Color facesColor, Color sideColor)
        {
            switch (directAxis)
            {
                case Axis.X:
                    PaintCylinderYZ(center, radius1, radius2, height, facesColor, sideColor);
                    break;
                case Axis.Y:
                    PaintCylinderXZ(center, radius1, radius2, height, facesColor, sideColor);
                    break;
                case Axis.Z:
                    PaintCylinderXY(center, radius1, radius2, height, facesColor, sideColor);
                    break;
            }
        }
        
        private static void PaintCylinderXY(Vector3 center, float radius1, float radius2, float height,
            Color facesColor, Color sideColor)
        {
            var circleCenter1 = new Vector3(center.X, center.Y, center.Z - height / 2);
            var circleCenter2 = new Vector3(center.X, center.Y, center.Z + height / 2);

            var x1 = circleCenter1.X - radius1;
            var y1 = circleCenter1.Y;
            var newX1 = x1;
            var newY1 = y1;
            
            var x2 = circleCenter2.X - radius2;
            var y2 = circleCenter2.Y;
            var newX2 = x2;
            var newY2 = y2;

            var vertices1 = new Vector3[360];
            var vertices2 = new Vector3[360];
            
            for (int i = 0; i < 360; i++)
            {
                vertices1[i] = new Vector3(newX1, newY1, circleCenter1.Z);
                vertices2[i] = new Vector3(newX2, newY2, circleCenter2.Z);
                
                var radians = i * (Math.PI / 180);
                
                newX1 = (float) ((x1 - circleCenter1.X) * Math.Cos(radians) - (y1 - circleCenter1.Y) * Math.Sin(radians) + circleCenter1.X);
                newY1 = (float) ((x1 - circleCenter1.X) * Math.Sin(radians) + (y1 - circleCenter1.Y) * Math.Cos(radians) + circleCenter1.Y);
                
                newX2 = (float) ((x2 - circleCenter2.X) * Math.Cos(radians) - (y2 - circleCenter2.Y) * Math.Sin(radians) + circleCenter2.X);
                newY2 = (float) ((x2 - circleCenter2.X) * Math.Sin(radians) + (y2 - circleCenter2.Y) * Math.Cos(radians) + circleCenter2.Y);
            }

            
            for (int i = 1; i < vertices1.Length; i++)
            {
                DrawCylinderPart(
                    vertices1[i], vertices1[i - 1], circleCenter1, vertices2[i], vertices2[i - 1], circleCenter2,
                    facesColor, sideColor);

                if (i == vertices1.Length - 1)
                    DrawCylinderPart(
                        vertices1[i], vertices1[0], circleCenter1, vertices2[i], vertices2[0], circleCenter2,
                        facesColor, sideColor);
            }
        }
        
        private static void PaintCylinderXZ(Vector3 center, float radius1, float radius2, float height,
            Color facesColor, Color sideColor)
        {
            var circleCenter1 = new Vector3(center.X, center.Y - height / 2, center.Z);
            var circleCenter2 = new Vector3(center.X, center.Y + height / 2, center.Z);

            var x1 = circleCenter1.X - radius1;
            var z1 = circleCenter1.Z;
            var newX1 = x1;
            var newZ1 = z1;
            
            var x2 = circleCenter2.X - radius2;
            var z2 = circleCenter2.Z;
            var newX2 = x2;
            var newZ2 = z2;

            var vertices1 = new Vector3[360];
            var vertices2 = new Vector3[360];
            
            for (int i = 0; i < 360; i++)
            {
                vertices1[i] = new Vector3(newX1, circleCenter1.Y, newZ1);
                vertices2[i] = new Vector3(newX2, circleCenter2.Y, newZ2);
                
                var radians = i * (Math.PI / 180);
                
                newX1 = (float) ((x1 - circleCenter1.X) * Math.Cos(radians) - (z1 - circleCenter1.Z) * Math.Sin(radians) + circleCenter1.X);
                newZ1 = (float) ((x1 - circleCenter1.X) * Math.Sin(radians) + (z1 - circleCenter1.Z) * Math.Cos(radians) + circleCenter1.Z);
                
                newX2 = (float) ((x2 - circleCenter2.X) * Math.Cos(radians) - (z2 - circleCenter2.Z) * Math.Sin(radians) + circleCenter2.X);
                newZ2 = (float) ((x2 - circleCenter2.X) * Math.Sin(radians) + (z2 - circleCenter2.Z) * Math.Cos(radians) + circleCenter2.Z);
            }

            
            for (int i = 1; i < vertices1.Length; i++)
            {
                DrawCylinderPart(
                    vertices1[i], vertices1[i - 1], circleCenter1, vertices2[i], vertices2[i - 1], circleCenter2,
                    facesColor, sideColor);

                if (i == vertices1.Length - 1)
                    DrawCylinderPart(
                        vertices1[i], vertices1[0], circleCenter1, vertices2[i], vertices2[0], circleCenter2,
                        facesColor, sideColor);
            }
        }

        private static void PaintCylinderYZ(Vector3 center, float radius1, float radius2, float height,
            Color facesColor, Color sideColor)
        {
            var circleCenter1 = new Vector3(center.X - height / 2, center.Y, center.Z);
            var circleCenter2 = new Vector3(center.X  + height / 2, center.Y, center.Z);

            var z1 = circleCenter1.Z - radius1;
            var y1 = circleCenter1.Y;
            var newZ1 = z1;
            var newY1 = y1;
            
            var z2 = circleCenter2.Z - radius2;
            var y2 = circleCenter2.Y;
            var newZ2 = z2;
            var newY2 = y2;

            var vertices1 = new Vector3[360];
            var vertices2 = new Vector3[360];
            
            for (int i = 0; i < 360; i++)
            {
                vertices1[i] = new Vector3(circleCenter1.X, newY1, newZ1);
                vertices2[i] = new Vector3(circleCenter2.X, newY2, newZ2);
                
                var radians = i * (Math.PI / 180);
                
                newZ1 = (float) ((z1 - circleCenter1.Z) * Math.Cos(radians) - (y1 - circleCenter1.Y) * Math.Sin(radians) + circleCenter1.Z);
                newY1 = (float) ((z1 - circleCenter1.Z) * Math.Sin(radians) + (y1 - circleCenter1.Y) * Math.Cos(radians) + circleCenter1.Y);
                
                newZ2 = (float) ((z2 - circleCenter2.Z) * Math.Cos(radians) - (y2 - circleCenter2.Y) * Math.Sin(radians) + circleCenter2.Z);
                newY2 = (float) ((z2 - circleCenter2.Z) * Math.Sin(radians) + (y2 - circleCenter2.Y) * Math.Cos(radians) + circleCenter2.Y);
            }

            
            for (int i = 1; i < vertices1.Length; i++)
            {
                DrawCylinderPart(
                    vertices1[i], vertices1[i - 1], circleCenter1, vertices2[i], vertices2[i - 1], circleCenter2,
                    facesColor, sideColor);

                if (i == vertices1.Length - 1)
                    DrawCylinderPart(
                        vertices1[i], vertices1[0], circleCenter1, vertices2[i], vertices2[0], circleCenter2,
                        facesColor, sideColor);
            }
        }

        private static void DrawCylinderPart(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Vector3 v5, Vector3 v6,
            Color color1, Color color2)
        {
            GL.Color3(color1);
            GL.Begin(PrimitiveType.Triangles);
            GL.TexCoord2(v1.Y * 4 - 0.2, v1.Z * 4);
            GL.Vertex3(v1);
            GL.TexCoord2(v2.Y * 4 - 0.2, v2.Z * 4);
            GL.Vertex3(v2);
            GL.TexCoord2(v3.Y * 4 - 0.2, v3.Z * 4);
            GL.Vertex3(v3);
                
            GL.TexCoord2(v5.Y, v5.Z);
            GL.Vertex3(v5);
            GL.TexCoord2(v4.Y, v4.Z);
            GL.Vertex3(v4);
            GL.TexCoord2(v6.Y, v6.Z);
            GL.Vertex3(v6);
            GL.End();
                
            GL.Color3(color2);
            GL.Begin(PrimitiveType.Quads);
            
            GL.TexCoord2(v1.Y, v1.Z);
            GL.Vertex3(v1);
            GL.TexCoord2(v2.Y, v2.Z);
            GL.Vertex3(v2);
            GL.TexCoord2(v5.Y, v5.Z);
            GL.Vertex3(v5);
            GL.TexCoord2(v4.Y, v4.Z);
            GL.Vertex3(v4);

            GL.End();
        }

        #endregion

        #region ClampedCylinder
        
        public static void PaintClampedCylinder(Axis directAxis, Vector3 center, float outRadius, float inRadius, float height,
            Color facesColor, Color sideColor)
        {
            switch (directAxis)
            {
                case Axis.X:
                    PaintClampedCylinderYZ(center, outRadius, inRadius, height, facesColor, sideColor);
                    break;
                case Axis.Y:
                    PaintClampedCylinderXZ(center, outRadius, inRadius, height, facesColor, sideColor);
                    break;
                case Axis.Z:
                    PaintClampedCylinderXY(center, outRadius, inRadius, height, facesColor, sideColor);
                    break;
            }
        }
        
         private static void PaintClampedCylinderXY(Vector3 center, float radius1, float radius2, float height,
            Color facesColor, Color sideColor)
        {
            var circleCenter1 = new Vector3(center.X, center.Y, center.Z - height / 2);
            var circleCenter2 = new Vector3(center.X, center.Y, center.Z + height / 2);

            var x1 = circleCenter1.X - radius1;
            var y1 = circleCenter1.Y;
            var newX1 = x1;
            var newY1 = y1;
            var x11 = circleCenter1.X - radius2;
            var y11 = circleCenter1.Y;
            var newX11 = x11;
            var newY11 = y11;
            
            var x2 = circleCenter2.X - radius1;
            var y2 = circleCenter2.Y;
            var newX2 = x2;
            var newY2 = y2;
            var x22 = circleCenter2.X - radius2;
            var y22 = circleCenter2.Y;
            var newX22 = x22;
            var newY22 = y22;
            
            var vertices1 = new Vector3[360];
            var vertices11 = new Vector3[360];
            var vertices2 = new Vector3[360];
            var vertices22 = new Vector3[360];
            
            for (int i = 0; i < 360; i++)
            {
                vertices1[i] = new Vector3(newX1, newY1, circleCenter1.Z);
                vertices2[i] = new Vector3(newX2, newY2, circleCenter2.Z);
                vertices11[i] = new Vector3(newX11, newY11, circleCenter1.Z);
                vertices22[i] = new Vector3(newX22, newY22, circleCenter2.Z);
                
                var radians = i * (Math.PI / 180);
                
                newX1 = (float) ((x1 - circleCenter1.X) * Math.Cos(radians) - (y1 - circleCenter1.Y) * Math.Sin(radians) + circleCenter1.X);
                newY1 = (float) ((x1 - circleCenter1.X) * Math.Sin(radians) + (y1 - circleCenter1.Y) * Math.Cos(radians) + circleCenter1.Y);
                newX11 = (float) ((x11 - circleCenter1.X) * Math.Cos(radians) - (y11 - circleCenter1.Y) * Math.Sin(radians) + circleCenter1.X);
                newY11 = (float) ((x11 - circleCenter1.X) * Math.Sin(radians) + (y11 - circleCenter1.Y) * Math.Cos(radians) + circleCenter1.Y);
                
                newX2 = (float) ((x2 - circleCenter2.X) * Math.Cos(radians) - (y2 - circleCenter2.Y) * Math.Sin(radians) + circleCenter2.X);
                newY2 = (float) ((x2 - circleCenter2.X) * Math.Sin(radians) + (y2 - circleCenter2.Y) * Math.Cos(radians) + circleCenter2.Y);
                newX22 = (float) ((x22 - circleCenter2.X) * Math.Cos(radians) - (y22 - circleCenter2.Y) * Math.Sin(radians) + circleCenter2.X);
                newY22 = (float) ((x22 - circleCenter2.X) * Math.Sin(radians) + (y22 - circleCenter2.Y) * Math.Cos(radians) + circleCenter2.Y);
            }

            
            for (int i = 1; i < vertices1.Length; i++)
            {
                DrawClampedCylinderPart(vertices11[i], vertices11[i - 1], vertices1[i - 1], vertices1[i], 
                    vertices2[i], vertices2[i - 1], vertices22[i - 1], vertices22[i], 
                    facesColor, sideColor);

                if (i == vertices1.Length - 1)
                    DrawClampedCylinderPart(vertices11[i], vertices11[0], vertices1[0], vertices1[i], 
                        vertices2[i], vertices2[0], vertices22[0], vertices22[i], 
                        facesColor, sideColor);
            }
        }

         private static void PaintClampedCylinderXZ(Vector3 center, float radius1, float radius2, float height,
            Color facesColor, Color sideColor)
        {
            var circleCenter1 = new Vector3(center.X, center.Y - height / 2, center.Z);
            var circleCenter2 = new Vector3(center.X, center.Y + height / 2, center.Z);

            var x1 = circleCenter1.X - radius1;
            var y1 = circleCenter1.Z;
            var newX1 = x1;
            var newY1 = y1;
            var x11 = circleCenter1.X - radius2;
            var y11 = circleCenter1.Z;
            var newX11 = x11;
            var newY11 = y11;
            
            var x2 = circleCenter2.X - radius1;
            var y2 = circleCenter2.Z;
            var newX2 = x2;
            var newY2 = y2;
            var x22 = circleCenter2.X - radius2;
            var y22 = circleCenter2.Z;
            var newX22 = x22;
            var newY22 = y22;
            
            var vertices1 = new Vector3[360];
            var vertices11 = new Vector3[360];
            var vertices2 = new Vector3[360];
            var vertices22 = new Vector3[360];
            
            for (int i = 0; i < 360; i++)
            {
                vertices1[i] = new Vector3(newX1, circleCenter1.Y, newY1);
                vertices2[i] = new Vector3(newX2, circleCenter2.Y, newY2);
                vertices11[i] = new Vector3(newX11, circleCenter1.Y, newY11);
                vertices22[i] = new Vector3(newX22, circleCenter2.Y, newY22);
                
                var radians = i * (Math.PI / 180);
                
                newX1 = (float) ((x1 - circleCenter1.X) * Math.Cos(radians) - (y1 - circleCenter1.Z) * Math.Sin(radians) + circleCenter1.X);
                newY1 = (float) ((x1 - circleCenter1.X) * Math.Sin(radians) + (y1 - circleCenter1.Z) * Math.Cos(radians) + circleCenter1.Z);
                newX11 = (float) ((x11 - circleCenter1.X) * Math.Cos(radians) - (y11 - circleCenter1.Z) * Math.Sin(radians) + circleCenter1.X);
                newY11 = (float) ((x11 - circleCenter1.X) * Math.Sin(radians) + (y11 - circleCenter1.Z) * Math.Cos(radians) + circleCenter1.Z);
                
                newX2 = (float) ((x2 - circleCenter2.X) * Math.Cos(radians) - (y2 - circleCenter2.Z) * Math.Sin(radians) + circleCenter2.X);
                newY2 = (float) ((x2 - circleCenter2.X) * Math.Sin(radians) + (y2 - circleCenter2.Z) * Math.Cos(radians) + circleCenter2.Z);
                newX22 = (float) ((x22 - circleCenter2.X) * Math.Cos(radians) - (y22 - circleCenter2.Z) * Math.Sin(radians) + circleCenter2.X);
                newY22 = (float) ((x22 - circleCenter2.X) * Math.Sin(radians) + (y22 - circleCenter2.Z) * Math.Cos(radians) + circleCenter2.Z);
            }

            
            for (int i = 1; i < vertices1.Length; i++)
            {
                DrawClampedCylinderPart(vertices11[i], vertices11[i - 1], vertices1[i - 1], vertices1[i], 
                    vertices2[i], vertices2[i - 1], vertices22[i - 1], vertices22[i], 
                    facesColor, sideColor);

                if (i == vertices1.Length - 1)
                    DrawClampedCylinderPart(vertices11[i], vertices11[0], vertices1[0], vertices1[i], 
                        vertices2[i], vertices2[0], vertices22[0], vertices22[i], 
                        facesColor, sideColor);
            }
        }

        private static void PaintClampedCylinderYZ(Vector3 center, float radius1, float radius2, float height,
            Color facesColor, Color sideColor)
        {
            var circleCenter1 = new Vector3(center.X - height / 2, center.Y, center.Z);
            var circleCenter2 = new Vector3(center.X + height / 2, center.Y, center.Z);

            var x1 = circleCenter1.Y - radius1;
            var y1 = circleCenter1.Z;
            var newX1 = x1;
            var newY1 = y1;
            var x11 = circleCenter1.Y - radius2;
            var y11 = circleCenter1.Z;
            var newX11 = x11;
            var newY11 = y11;
            
            var x2 = circleCenter2.Y - radius1;
            var y2 = circleCenter2.Z;
            var newX2 = x2;
            var newY2 = y2;
            var x22 = circleCenter2.Y - radius2;
            var y22 = circleCenter2.Z;
            var newX22 = x22;
            var newY22 = y22;
            
            var vertices1 = new Vector3[360];
            var vertices11 = new Vector3[360];
            var vertices2 = new Vector3[360];
            var vertices22 = new Vector3[360];
            
            for (int i = 0; i < 360; i++)
            {
                vertices1[i] = new Vector3(circleCenter1.X, newX1, newY1);
                vertices2[i] = new Vector3(circleCenter2.X, newX2, newY2);
                vertices11[i] = new Vector3(circleCenter1.X, newX11, newY11);
                vertices22[i] = new Vector3(circleCenter2.X, newX22, newY22);
                
                var radians = i * (Math.PI / 180);
                
                newX1 = (float) ((x1 - circleCenter1.Y) * Math.Cos(radians) - (y1 - circleCenter1.Z) * Math.Sin(radians) + circleCenter1.Y);
                newY1 = (float) ((x1 - circleCenter1.Y) * Math.Sin(radians) + (y1 - circleCenter1.Z) * Math.Cos(radians) + circleCenter1.Z);
                newX11 = (float) ((x11 - circleCenter1.Y) * Math.Cos(radians) - (y11 - circleCenter1.Z) * Math.Sin(radians) + circleCenter1.Y);
                newY11 = (float) ((x11 - circleCenter1.Y) * Math.Sin(radians) + (y11 - circleCenter1.Z) * Math.Cos(radians) + circleCenter1.Z);
                
                newX2 = (float) ((x2 - circleCenter2.Y) * Math.Cos(radians) - (y2 - circleCenter2.Z) * Math.Sin(radians) + circleCenter2.Y);
                newY2 = (float) ((x2 - circleCenter2.Y) * Math.Sin(radians) + (y2 - circleCenter2.Z) * Math.Cos(radians) + circleCenter2.Z);
                newX22 = (float) ((x22 - circleCenter2.Y) * Math.Cos(radians) - (y22 - circleCenter2.Z) * Math.Sin(radians) + circleCenter2.Y);
                newY22 = (float) ((x22 - circleCenter2.Y) * Math.Sin(radians) + (y22 - circleCenter2.Z) * Math.Cos(radians) + circleCenter2.Z);
            }

            
            for (int i = 1; i < vertices1.Length; i++)
            {
                DrawClampedCylinderPart(vertices11[i], vertices11[i - 1], vertices1[i - 1], vertices1[i], 
                    vertices2[i], vertices2[i - 1], vertices22[i - 1], vertices22[i], 
                    facesColor, sideColor);

                if (i == vertices1.Length - 1)
                    DrawClampedCylinderPart(vertices11[i], vertices11[0], vertices1[0], vertices1[i], 
                        vertices2[i], vertices2[0], vertices22[0], vertices22[i], 
                        facesColor, sideColor);
            }
        }
        
        private static void DrawClampedCylinderPart(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Vector3 v5, 
            Vector3 v6, Vector3 v7, Vector3 v8, Color facesColor, Color sideColor)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(facesColor);
            GL.Vertex3(v1);
            GL.Vertex3(v2);
            GL.Vertex3(v3);
            GL.Vertex3(v4);
                
            GL.Vertex3(v5);
            GL.Vertex3(v6);
            GL.Vertex3(v7);
            GL.Vertex3(v8);
             
            GL.Color3(sideColor);

            GL.Vertex3(v3);
            GL.Vertex3(v4);
            GL.Vertex3(v5);
            GL.Vertex3(v6);

            GL.Vertex3(v1);
            GL.Vertex3(v2);
            GL.Vertex3(v7);
            GL.Vertex3(v8);
             
            GL.End();
        }
        
        #endregion
    }
}