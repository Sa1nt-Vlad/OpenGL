using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace OpenGL
{
    class TankGameWindow : GameWindow
    {
        private double turretRotationAngle;
        private double perspective = 1.0;
        private float angleX = 0;
        private float angleY = 0;
        private float lastMouseX = 0;
        private float lastMouseY = 0;
        
        public static int[] TextureIds;
        
        public TankGameWindow(int width, int height, GraphicsMode mode, string title) : base(width, height, mode, title) { }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.15f, 0.2f, 0.25f, 1f);
            GL.DepthMask(true);

            // enable z-buffer
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Enable(EnableCap.Lighting);
            GL.Light(LightName.Light0, LightParameter.Ambient, new[] { 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Diffuse, new[] { 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Position, new[] { 1.0f, 0.0f, 0.0f });
            GL.Enable(EnableCap.Light0);
            GL.Light(LightName.Light1, LightParameter.Ambient, new[] { 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light1, LightParameter.Diffuse, new[] { 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light1, LightParameter.Position, new[] { 0.0f, 0.0f, 1.0f });
            GL.Enable(EnableCap.Light1);
            GL.Light(LightName.Light2, LightParameter.Ambient, new[] { 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light2, LightParameter.Diffuse, new[] { 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light2, LightParameter.Position, new[] { 0.0f, 1.0f, 0.0f });
            GL.Enable(EnableCap.Light2);
            
            
            GL.Enable(EnableCap.Texture2D);
            TextureIds = new int[9];

            GL.Enable(EnableCap.AutoNormal);
            
            GL.GenTextures(TextureIds.Length, TextureIds);
            GenerateNewTexture(0, @"..\..\Textures\body_1.bmp");
            GenerateNewTexture(1, @"..\..\Textures\wheel_1.bmp");
            GenerateNewTexture(2, @"..\..\Textures\track_2.bmp");
            GenerateNewTexture(3, @"..\..\Textures\body_1.bmp");
            GenerateNewTexture(4, @"..\..\Textures\body_1.bmp");
            GenerateNewTexture(5, @"..\..\Textures\metal_3.bmp");
            GenerateNewTexture(6, @"..\..\Textures\metal.bmp");
            GenerateNewTexture(7, @"..\..\Textures\camo_1.bmp");
            
            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Scale(perspective, perspective, perspective);

            TankPainter.DrawTank(2d);
            
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
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Rotate(angleX, 1, 0, 0);
            GL.Rotate(angleY, 0, 1, 0);
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
                if (Math.Abs(state.X - lastMouseX) >= 20)
                    lastMouseX = state.X;
                if (Math.Abs(state.Y - lastMouseY) >= 20)
                    lastMouseY = state.Y;
                
                angleX += (lastMouseY - state.Y) / 2;
                angleY += (lastMouseX - state.X) / 2;
                
                lastMouseX = state.X;
                lastMouseY = state.Y;

                RotateView();
            }
        }
        
        private static void GenerateNewTexture(int id, string filePath)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureIds[id]);
            var textureData = LoadImage(filePath);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb,
                textureData.Width, textureData.Height, 0,
                PixelFormat.Bgr, PixelType.UnsignedByte,
                textureData.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
        
        private static BitmapData LoadImage(string filePath)
        {
            var bmp = new Bitmap(filePath);
            var rectangle = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var bmpData = bmp.LockBits(rectangle,ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            bmp.UnlockBits(bmpData);
            return bmpData;
        }
    }
}