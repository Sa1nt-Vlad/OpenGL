using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGL
{
    public class TankPainter
    {
        private static readonly Vector3[] corpusPoints = {
            /* 0 */ Vector3.Zero,
            /* 1 */ new Vector3(0.2f, 0, -0.94f), 
            /* 2 */ new Vector3(0.2f, 0.1f, -0.78f), 
            /* 3 */ new Vector3(-0.2f, 0.1f, -0.78f),
            /* 4 */ new Vector3(-0.2f, 0, -0.94f),
            /* 5 */ new Vector3(0.3f, 0.1f, -0.78f),
            /* 6 */ new Vector3(0.3f, 0.17f, -0.66f),
            /* 7 */ new Vector3(-0.3f, 0.17f, -0.66f),
            /* 8 */ new Vector3(-0.3f, 0.1f, -0.78f),
            /* 9 */ new Vector3(-0.3f, 0.2f, -0.55f),
            /* 10 */ new Vector3(0.3f, 0.2f, -0.55f),
                
            /* 11 */ new Vector3(0.3f, 0.1f, -0.52f),
            /* 12 */ new Vector3(0.3f, 0.12f, 0.21f),
            /* 13 */ new Vector3(0.3f, 0.22f, 0.2f), 
            /* 14 */ new Vector3(-0.3f, 0.1f, -0.52f),
            /* 15 */ new Vector3(-0.3f, 0.12f, 0.21f),
            /* 16 */ new Vector3(-0.3f, 0.22f, 0.2f),
            /* 17 */ new Vector3(0.3f, 0.1f, 0.22f),
            /* 18 */ new Vector3(0.3f, 0.1f, 0.97f),
            /* 19 */ new Vector3(0.3f, 0.12f, 0.97f),
            /* 20 */ new Vector3(-0.3f, 0.1f, 0.22f),
            /* 21 */ new Vector3(-0.3f, 0.1f, 0.97f),
            /* 22 */ new Vector3(-0.3f, 0.12f, 0.97f),
                
            /* 23 */ new Vector3(0.2f, 0.1f, -0.52f),
            /* 24 */ new Vector3(0.2f, -0.08f, -0.74f),
            /* 25 */ new Vector3(0.2f, -0.08f, 0.89f),
            /* 26 */ new Vector3(0.2f, 0.1f, 0.97f),
            /* 27 */ new Vector3(-0.2f, -0.08f, -0.74f),
            /* 28 */ new Vector3(-0.2f, 0.1f, -0.52f),
            /* 29 */ new Vector3(-0.2f, 0.1f, 0.97f),
            /* 30 */ new Vector3(-0.2f, -0.08f, 0.89f),
        };
        private static readonly Vector3[] towerPoints = {
            /* 0 */ Vector3.Zero,
            /* 1 */ new Vector3(0.07f, 0.12f, 0.24f), 
            /* 2 */ new Vector3(0.15f, 0.12f, 0.29f), 
            /* 3 */ new Vector3(0.07f, 0.29f, 0.26f),
            /* 4 */ new Vector3(0.15f, 0.31f, 0.31f),
            /* 5 */ new Vector3(0.182f, 0.37f, 0.4f),
            /* 6 */ new Vector3(0.07f, 0.37f, 0.4f),
            /* 7 */ new Vector3(0.07f, 0.45f, 0.54f),
            /* 8 */ new Vector3(-0.3f, 0.1f, -0.78f),
            /* 9 */ new Vector3(0.24f, 0.45f, 0.55f),
            /* 10 */ new Vector3(0.3f, 0.2f, -0.55f),
            /* 11 */ new Vector3(0.25f, 0.12f, 0.55f),
            /* 12 */ new Vector3(0.31f, 0.24f, 0.73f),
            /* 13 */ new Vector3(0.26f, 0.48f, 0.61f), 
            /* 14 */ new Vector3(0.34f, 0.43f, 0.83f),
            /* 15 */ new Vector3(0.34f, 0.24f, 0.83f),
            /* 16 */ new Vector3(0.34f, 0.24f, 1.12f),
            /* 17 */ new Vector3(0.34f, 0.43f, 1.12f),
            /* 18 */ new Vector3(0.28f, 0.47f, 1.08f),
            /* 19 */ new Vector3(0.3f, 0.12f, 0.97f),
            /* 20 */ new Vector3(-0.3f, 0.1f, 0.22f),
            /* 21 */ new Vector3(0, 0.56f, 0.73f),
            /* 22 */ new Vector3(0, 0.56f, 0.99f), 
            
            /* 23 */ new Vector3(-0.07f, 0.12f, 0.24f), 
            /* 24 */ new Vector3(-0.15f, 0.12f, 0.29f), 
            /* 25 */ new Vector3(-0.07f, 0.29f, 0.26f),
            /* 26 */ new Vector3(-0.15f, 0.31f, 0.31f),
            /* 27 */ new Vector3(-0.182f, 0.37f, 0.4f),
            /* 28 */ new Vector3(-0.07f, 0.37f, 0.4f),
            /* 29 */ new Vector3(-0.07f, 0.45f, 0.54f),
            /* 30 */ new Vector3(-0.3f, 0.1f, -0.78f),
            /* 31 */ new Vector3(-0.24f, 0.45f, 0.55f),
            /* 32 */ new Vector3(-0.3f, 0.2f, -0.55f),
            /* 33 */ new Vector3(-0.25f, 0.12f, 0.55f),
            /* 34 */ new Vector3(-0.31f, 0.24f, 0.73f),
            /* 35 */ new Vector3(-0.26f, 0.48f, 0.61f), 
            /* 36 */ new Vector3(-0.34f, 0.43f, 0.83f),
            /* 37 */ new Vector3(-0.34f, 0.24f, 0.83f),
            /* 38 */ new Vector3(-0.34f, 0.24f, 1.12f),
            /* 39 */ new Vector3(-0.34f, 0.43f, 1.12f),
            /* 40 */ new Vector3(-0.28f, 0.47f, 1.08f),
            /* 41 */ new Vector3(-0.3f, 0.12f, 0.97f),
            /* 42 */ new Vector3(-0.3f, 0.1f, 0.22f),
            /* 43 */ new Vector3(-0, 0.56f, 0.73f),
            /* 44 */ new Vector3(-0, 0.56f, 0.99f), 
        };
        
        public static void DrawTank(double turretAngle)
        {
            DrawCorpus();
            DrawTower();
        }

        private static void DrawTower()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.DimGray);
            MakeSquare(towerPoints[1], towerPoints[2], towerPoints[4], towerPoints[3]);
            MakeSquare(towerPoints[14], towerPoints[15], towerPoints[16], towerPoints[17]);
            MakeSquare(towerPoints[23], towerPoints[24], towerPoints[26], towerPoints[25]);
            MakeSquare(towerPoints[36], towerPoints[37], towerPoints[38], towerPoints[39]);
            
            GL.Color3(Color.SlateGray);
            MakeSquare(towerPoints[2], towerPoints[4], towerPoints[5], towerPoints[11]);
            MakeSquare(towerPoints[5], towerPoints[11], towerPoints[12], towerPoints[13]);
            MakeSquare(towerPoints[12], towerPoints[13], towerPoints[14], towerPoints[15]);
            MakeSquare(towerPoints[24], towerPoints[26], towerPoints[27], towerPoints[33]);
            MakeSquare(towerPoints[27], towerPoints[33], towerPoints[34], towerPoints[35]);
            MakeSquare(towerPoints[34], towerPoints[35], towerPoints[36], towerPoints[37]);
            
            GL.Color3(Color.IndianRed);
            MakeSquare(towerPoints[3], towerPoints[4], towerPoints[5], towerPoints[6]);
            MakeSquare(towerPoints[13], towerPoints[14], towerPoints[17], towerPoints[18]);
            MakeSquare(towerPoints[25], towerPoints[26], towerPoints[27], towerPoints[28]);
            MakeSquare(towerPoints[35], towerPoints[36], towerPoints[39], towerPoints[40]);

            GL.Color3(Color.CornflowerBlue);
            MakeSquare(towerPoints[28], towerPoints[27], towerPoints[31], towerPoints[29]);
            GL.Color3(Color.CornflowerBlue);
            MakeSquare(towerPoints[6], towerPoints[5], towerPoints[9], towerPoints[7]);
            
            GL.Color3(Color.Gray);
            MakeSquare(towerPoints[21], towerPoints[22], towerPoints[18], towerPoints[13]);
            MakeSquare(towerPoints[43], towerPoints[44], towerPoints[40], towerPoints[35]);
            
            GL.End();
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
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.DimGray);
            MakeSquare(corpusPoints[1], corpusPoints[2], corpusPoints[3], corpusPoints[4]);
            MakeSquare(corpusPoints[5], corpusPoints[6], corpusPoints[7], corpusPoints[8]);
            GL.Color3(Color.Gray);
            MakeSquare(corpusPoints[6], corpusPoints[10], corpusPoints[9], corpusPoints[7]);
            GL.Color3(Color.DimGray);
            MakeSquare(corpusPoints[9], corpusPoints[10], corpusPoints[13], corpusPoints[16]);
            GL.Color3(Color.SlateGray);
            MakeSquare(corpusPoints[5], corpusPoints[6], corpusPoints[10], corpusPoints[11]);
            MakeSquare(corpusPoints[8], corpusPoints[7], corpusPoints[9], corpusPoints[14]);
            GL.Color3(Color.Gray);
            MakeSquare(corpusPoints[10], corpusPoints[13], corpusPoints[17], corpusPoints[11]);
            MakeSquare(corpusPoints[9], corpusPoints[14], corpusPoints[20], corpusPoints[16]);
            GL.Color3(Color.SlateGray);
            MakeSquare(corpusPoints[20], corpusPoints[16], corpusPoints[13], corpusPoints[17]);
            GL.Color3(Color.DimGray);
            MakeSquare(corpusPoints[12], corpusPoints[17], corpusPoints[18], corpusPoints[19]);
            MakeSquare(corpusPoints[15], corpusPoints[20], corpusPoints[21], corpusPoints[22]);
            MakeSquare(corpusPoints[21], corpusPoints[22], corpusPoints[19], corpusPoints[18]);
            GL.Color3(Color.Gray);
            MakeSquare(corpusPoints[15], corpusPoints[22], corpusPoints[19], corpusPoints[12]);

            GL.Color3(Color.LightSlateGray);
            MakeSquare(corpusPoints[1], corpusPoints[2], corpusPoints[23], corpusPoints[24]);
            MakeSquare(corpusPoints[3], corpusPoints[4], corpusPoints[27], corpusPoints[28]);
            
            GL.Color3(Color.SlateGray);
            MakeSquare(corpusPoints[24], corpusPoints[23], corpusPoints[26], corpusPoints[25]);
            MakeSquare(corpusPoints[27], corpusPoints[28], corpusPoints[29], corpusPoints[30]);
            GL.Color3(Color.Gray);
            MakeSquare(corpusPoints[4], corpusPoints[1], corpusPoints[24], corpusPoints[27]);
            MakeSquare(corpusPoints[17], corpusPoints[18], corpusPoints[21], corpusPoints[20]);
            MakeSquare(corpusPoints[2], corpusPoints[5], corpusPoints[11], corpusPoints[23]);
            MakeSquare(corpusPoints[3], corpusPoints[8], corpusPoints[14], corpusPoints[28]);
            GL.Color3(Color.DimGray);
            MakeSquare(corpusPoints[24], corpusPoints[27], corpusPoints[30], corpusPoints[25]);
            MakeSquare(corpusPoints[11], corpusPoints[14], corpusPoints[20], corpusPoints[17]);
            GL.Color3(Color.LightSlateGray);
            MakeSquare(corpusPoints[25], corpusPoints[30], corpusPoints[29], corpusPoints[26]);

            GL.End();
        }

        private static void MakeSquare(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4)
        {
            Painter.PaintSquare(point1, point2, point3, point4);
        }
        
        private static void MakeTriangle(Vector3 point1, Vector3 point2, Vector3 point3)
        {
            Painter.PaintTriangle(point1, point2, point3);
        }
    }
}