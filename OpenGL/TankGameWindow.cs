using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenGL
{
    class TankGameWindow : GameWindow
    {
        private double turretRotationAngle;
        private double perspective = 1.0;
        public TankGameWindow(int width, int height, GraphicsMode mode, string title) : base(width, height, mode, title){}

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1f);
            GL.DepthMask(true);
            
            // включение z-buffer
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            
            GL.MatrixMode(MatrixMode.Modelview);
            GL.Rotate(-45, 1, 1, 0);
            
            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Scale(perspective, perspective, perspective);

            // фигуры
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.IndianRed);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0.5, 0);
            GL.Vertex3(0.5, 0.5, 0);
            GL.Vertex3(0.5, 0, 0);
            
            GL.Color3(Color.ForestGreen);
            GL.Vertex3(0.5, 0, 0);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 0.5);
            GL.Vertex3(0.5, 0, 0.5);
            
            GL.Color3(Color.DodgerBlue);
            GL.Vertex3(0.5, 0, 0.5);
            GL.Vertex3(0.5, 0, 0);
            GL.Vertex3(0.5, 0.5, 0);
            GL.Vertex3(0.5, 0.5, 0.5);

            
            GL.End();
            // Tank.DrawTank(_turretRotationAngle);                

            // поворот фигуры
            
            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var input = Keyboard.GetState();
            
            if (input.IsKeyDown(Key.Escape)) Exit();

            if (input.IsKeyDown(Key.D))
            {
                if (Math.Abs(turretRotationAngle - 360.0) < 0.001)
                    turretRotationAngle = 0.0;
                else
                    turretRotationAngle += 5.0;
            }

            if (input.IsKeyDown(Key.A))
            {
                if (Math.Abs(turretRotationAngle - (-360.0)) < 0.001)
                    turretRotationAngle = 0.0;
                else
                    turretRotationAngle -= 5.0;
            }

            if (input.IsKeyDown(Key.W)) perspective += 0.1;
            if (input.IsKeyDown(Key.S)) perspective -= 0.1;
            
            base.OnUpdateFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }
    }
}