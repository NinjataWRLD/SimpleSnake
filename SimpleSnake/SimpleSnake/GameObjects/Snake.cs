using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public class Snake
    {
        private const char SnakeSymbol = '\u25CF';
        private const char EmptySpace = ' ';

        private readonly Queue<Point> snake;
        private readonly Food[] food;
        private readonly Wall wall;
        
        private int foodIndex;
        private int nextLeftX;
        private int nextTopY;

        public Snake(Wall wall)
        {
            this.wall = wall;
            this.snake = new Queue<Point>();
            this.food = new Food[3];
            this.foodIndex = this.RandomFoodNumber;
            this.GetFoods();
            this.CreateSneak();
            this.food[this.foodIndex].SetRandomPosition(this.snake);
        }

        public int RandomFoodNumber => new Random().Next(0, this.food.Length);

        private void CreateSneak()
        {
            for (int topY = 1; topY <= 6; topY++)
            {
                this.snake.Enqueue(new Point(2, topY));
            }
        }

        private void GetFoods()
        {
            this.food[0] = new FoodAsterisk(this.wall);
            this.food[1] = new FoodDollar(this.wall);
            this.food[2] = new FoodHash(this.wall);
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;
        }

        public bool IsMoving(Point direction)
        {
            Point snakeHead = this.snake.Last();

            this.GetNextPoint(direction, snakeHead);

            bool snakeBitHerself = this.snake
                .Any(point => point.LeftX == this.nextLeftX && point.TopY == nextTopY);

            if (snakeBitHerself)
            {
                return false;
            }

            Point newHead = new Point(this.nextLeftX, this.nextTopY);
            bool snakeHitWall = this.wall.IsPointOfWall(newHead);

            if (snakeHitWall)
            {
                return false;
            }

            this.snake.Enqueue(newHead);
            newHead.Draw(SnakeSymbol);

            if (food[foodIndex].IsFoodPoint(newHead))
            {
                this.Eat(direction, newHead);
            }

            Point snakeTail = this.snake.Dequeue();
            snakeTail.Draw(EmptySpace);

            return true;
        }

        private void Eat(Point direction, Point snakeHead)
        {
            int length = this.food[this.foodIndex].FoodPoints;

            for (int i = 0; i < length; i++)
            {
                this.snake.Enqueue(new Point(this.nextLeftX, nextTopY));
                this.GetNextPoint(direction, snakeHead);
            }

            this.foodIndex = this.RandomFoodNumber;
            this.food[this.foodIndex].SetRandomPosition(this.snake);
        }
    }
}
