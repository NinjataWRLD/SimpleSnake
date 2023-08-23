using SimpleSnake.Enums;
using SimpleSnake.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSnake.Core
{
    public class Engine
    {
        private Point[] pointsOfDirection;
        private Direction direction;
        private Wall wall;
        private Snake snake;
        private float sleepTime;

        public Engine(Wall wall, Snake snake)
        {
            this.pointsOfDirection = new Point[4];
            this.wall = wall;
            this.snake = snake;
            this.sleepTime = 100;
        }

        public void Run()
        {
            this.CreateDirections();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    this.GetNextDirection();
                }

                bool isMoving = snake.IsMoving(this.pointsOfDirection[(int)direction]);

                if (!isMoving)
                {
                    AskUserForRestart();
                }

                this.sleepTime -= 0.01f;

                Thread.Sleep((int)this.sleepTime);
            }
        }

        private void CreateDirections()
        {
            this.pointsOfDirection[0] = new Point(1, 0);
            this.pointsOfDirection[1] = new Point(-1, 0);
            this.pointsOfDirection[2] = new Point(0, 1);
            this.pointsOfDirection[3] = new Point(0, -1);
        }

        private void GetNextDirection()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.LeftArrow:
                    if (direction != Direction.Right) direction = Direction.Left;
                    break;

                case ConsoleKey.RightArrow:
                    if (direction != Direction.Left) direction = Direction.Right;
                    break;

                case ConsoleKey.UpArrow:
                    if (direction != Direction.Down) direction = Direction.Up;
                    break;

                case ConsoleKey.DownArrow:
                    if (direction != Direction.Up) direction = Direction.Down;
                    break;
            }

            Console.CursorVisible = false;
        }

            private void AskUserForRestart()
            {
                int leftX = this.wall.LeftX + 1;
                int topY = 3;

                Console.SetCursorPosition(leftX, topY);
                Console.Write("Would you like to continue? y/n");


                switch (Console.ReadLine())
                {
                    case "y": Console.Clear(); StartUp.Main(); break;
                    case "n": StopGame(); break;
                }
            }

        private void StopGame()
        {
            Console.SetCursorPosition(20, 10);
            Console.Write("Game over!");
            Environment.Exit(0);
        }
    }
}
