using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public abstract class Food : Point
    {
        private readonly Wall wall;
        private readonly Random random;
        private readonly char foodSymbol;
        protected Food(Wall wall, char foodSymbol, int points)
            : base(wall.LeftX, wall.TopY)
        {
            this.wall = wall;
            this.FoodPoints = points;
            this.foodSymbol = foodSymbol;
            random = new Random();
        }

        public int FoodPoints { get; private set; }

        public void SetRandomPosition(Queue<Point> snakePoints)
        {
            do
            {
                this.LeftX = random.Next(2, wall.LeftX - 2);
                this.TopY = random.Next(2, wall.TopY - 2);
            }
            while (snakePoints.Any(point => this.IsFoodPoint(point)));

            Console.ForegroundColor = ConsoleColor.DarkRed;
            this.Draw(foodSymbol);
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public bool IsFoodPoint(Point snake)
            => snake.TopY == this.TopY && snake.LeftX == this.LeftX;
    }
}
