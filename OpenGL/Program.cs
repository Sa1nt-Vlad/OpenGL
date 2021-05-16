using OpenTK.Graphics;

namespace OpenGL
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (var game = new TankGameWindow(700, 700, new GraphicsMode(32, 24, 8, 1), "WT auf E100"))
            {
                game.Run(60.0);
            }
        }
    }
}