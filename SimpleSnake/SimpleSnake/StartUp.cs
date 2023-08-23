using SimpleSnake.Core;
using SimpleSnake.GameObjects;
using SimpleSnake.Utilities;

namespace SimpleSnake
{
    public class StartUp
    {
        public static void Main()
        {
            ConsoleWindow.CustomizeConsole();

            Wall wall = new(60, 20);
            Snake snake = new(wall);
            
            Engine engine = new(wall, snake);
            engine.Run();
        }
    }
}
