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
        private float angle_x = 0;
        private float angle_y = 0;
        private float last_mouse_x = 0;
        private float last_mouse_y = 0;
        
        public TankGameWindow(int width, int height, GraphicsMode mode, string title) : base(width, height, mode, title) { }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1f);
            GL.DepthMask(true);

            // enable z-buffer
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            // GL.MatrixMode(MatrixMode.Modelview);
            // GL.Rotate(-45, 1, 1, 0);

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Scale(perspective, perspective, perspective);

            TankPainter.DrawTank(2d);

            // Painter.PaintCylinder(Axis.X, new Vector3(0, 0, 0), 0.3f, 0.3f, 0.8f, Color.Aqua, Color.Azure);
            
            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Focused) return;
            ProcessKeyboardState();

            base.OnUpdateFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            ProcessMouseState();
            base.OnMouseMove(e);
        }

        private void RotateView()
        {
            // if (angle_x < 0) angle_x = Math.Abs(angle_x);
            // if (angle_y < 0) angle_y = Math.Abs(angle_y);
            //
            // if (angle_x > 360) angle_x %= 360;
            // if (angle_y > 360) angle_y %= 360;

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Rotate(angle_x, 1, 0, 0);
            GL.Rotate(angle_y, 0, 1, 0);
        }

        private void ProcessKeyboardState()
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Key.Escape)) Exit();

            if (keyboardState.IsKeyDown(Key.D))
            {
                if (Math.Abs(turretRotationAngle - 360.0) < 0.001)
                    turretRotationAngle = 0.0;
                else
                    turretRotationAngle += 5.0;
            }

            if (keyboardState.IsKeyDown(Key.A))
            {
                if (Math.Abs(turretRotationAngle - (-360.0)) < 0.001)
                    turretRotationAngle = 0.0;
                else
                    turretRotationAngle -= 5.0;
            }

            if (keyboardState.IsKeyDown(Key.W)) perspective += 0.01;
            if (keyboardState.IsKeyDown(Key.S)) perspective -= 0.01;
        }

        private void ProcessMouseState()
        {
            var state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed)
            {
                if (Math.Abs(state.X - last_mouse_x) >= 20)
                    last_mouse_x = state.X;
                if (Math.Abs(state.Y - last_mouse_y) >= 20)
                    last_mouse_y = state.Y;
                
                angle_x += (last_mouse_y - state.Y) / 2;
                angle_y += (last_mouse_x - state.X) / 2;
                
                last_mouse_x = state.X;
                last_mouse_y = state.Y;

                RotateView();
            }
        }
    }
}