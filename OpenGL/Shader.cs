using System;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace OpenGL
{
    public class Shader
    {
        private int handle;

        public Shader(string vertexPath, string fragmentPath)
        {
            //LoadShaders(vertexPath, fragmentPath);
            
            handle = GL.CreateProgram();

            var vertexShader = LoadShader(vertexPath, ShaderType.VertexShader);
            var fragmentShader = LoadShader(fragmentPath, ShaderType.FragmentShader);
            
            GL.AttachShader(handle, vertexShader);
            GL.AttachShader(handle, fragmentShader);

            GL.LinkProgram(handle);
            
            GL.DetachShader(handle, vertexShader);
            GL.DetachShader(handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }

        private void LoadShaders(string vertexPath, string fragmentPath)
        {
            using (var reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                var vertexShaderSource = reader.ReadToEnd();
                var vertexShader = GL.CreateShader(ShaderType.VertexShader);
                GL.ShaderSource(vertexShader, vertexShaderSource);
                
                GL.CompileShader(vertexShader);

                string infoLogVert = GL.GetShaderInfoLog(vertexShader);
                if (infoLogVert != System.String.Empty)
                    System.Console.WriteLine(infoLogVert);
            }
            
            using (var reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                var fragmentShaderSource = reader.ReadToEnd();
                var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
                GL.ShaderSource(fragmentShader, fragmentShaderSource);
                
                GL.CompileShader(fragmentShader);

                var infoLogFrag = GL.GetShaderInfoLog(fragmentShader);

                if (infoLogFrag != string.Empty)
                    System.Console.WriteLine(infoLogFrag);
            }
        }

        private static int LoadShader(string shaderPath, ShaderType shaderType)
        {
            var shaderId = 0;
            
            using (var reader = new StreamReader(shaderPath, Encoding.UTF8))
            {
                var shaderSource = reader.ReadToEnd();
                shaderId = GL.CreateShader(shaderType);
                GL.ShaderSource(shaderId, shaderSource);
                GL.CompileShader(shaderId);

                var infoLogVert = GL.GetShaderInfoLog(shaderId);
                if (infoLogVert != string.Empty)
                    System.Console.WriteLine(infoLogVert);
            }

            return shaderId;
        }
        
        public void Use()
        {
            GL.UseProgram(handle);
        }
        
        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(handle, attribName);
        }
        
        #region Dispose

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(handle);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}