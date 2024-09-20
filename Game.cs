using System;
using System.Diagnostics;
using System.Threading;

namespace PraktilineTööMadu
{
    internal class Game
    {
        private string playerName;
        private int foodCounter;
        private bool gameOver;
        private Walls walls;
        private Snake snake;
        private FoodCreator goodFoodCreator;
        private FoodCreator badFoodCreator;
        private Point goodFood;
        private Point badFood;
        private Stopwatch stopwatch;

        public Game(string playerName)
        {
            this.playerName = playerName;
            foodCounter = 0;
            gameOver = false;
            walls = new Walls(Console.WindowWidth, Console.WindowHeight);
            snake = InitializeSnake();
            goodFoodCreator = new FoodCreator(Console.WindowWidth, Console.WindowHeight, '$');
            badFoodCreator = new FoodCreator(Console.WindowWidth, Console.WindowHeight, 'X');
            goodFood = goodFoodCreator.CreateFood();
            badFood = badFoodCreator.CreateFood();
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void Start()
        {
            DrawInitialGameObjects();

            while (!gameOver)
            {
                gameOver = CheckGameOver();
                if (!gameOver)
                {
                    foodCounter = HandleFoodConsumption();
                    snake.Move();
                }
                DisplayGameStats();
                Thread.Sleep(75);
                HandleKeyPress();
            }
            EndGame();
        }

        private Snake InitializeSnake()
        {
            Point startPoint = new Point(4, 5, '@');
            Snake snake = new Snake(startPoint, 4, Direction.RIGHT);
            Console.ForegroundColor = ConsoleColor.Green;
            snake.Draw();
            return snake;
        }

        private void DrawInitialGameObjects()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            walls.Draw();
            Console.ForegroundColor = ConsoleColor.Yellow;
            goodFood.Draw(goodFood.x, goodFood.y, goodFood.sym);
            Console.ForegroundColor = ConsoleColor.Red;
            badFood.Draw(badFood.x, badFood.y, badFood.sym);
        }

        private bool CheckGameOver()
        {
            if (walls.IsHit(snake) || snake.IsHitTail())
            {
                Console.Clear();
                string gameOverMessage = "GAME OVER";
                Menu.DisplayCenteredMessage(gameOverMessage, 0);
                return true;
            }
            return false;
        }

        private int HandleFoodConsumption()
        {
            if (snake.Eat(goodFood) || snake.Eat(badFood))
            {
                ClearFoodPosition();
                goodFood = goodFoodCreator.CreateFood();
                badFood = badFoodCreator.CreateFood();

                if (snake.Eat(goodFood))
                {
                    foodCounter += 1;
                }
                else if (snake.Eat(badFood))
                {
                    foodCounter -= 1;
                }

                DrawFood();
            }
            return foodCounter;
        }

        private void ClearFoodPosition()
        {
            Console.SetCursorPosition(goodFood.x, goodFood.y);
            Console.Write(' ');
            Console.SetCursorPosition(badFood.x, badFood.y);
            Console.Write(' ');
        }

        private void DrawFood()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            goodFood.Draw(goodFood.x, goodFood.y, goodFood.sym);
            Console.ForegroundColor = ConsoleColor.Red;
            badFood.Draw(badFood.x, badFood.y, badFood.sym);
        }

        private void DisplayGameStats()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write($"Food eaten: {foodCounter} Time: {stopwatch.Elapsed.Minutes:D2}:{stopwatch.Elapsed.Seconds:D2}");
        }

        private void HandleKeyPress()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                snake.HandleKey(key);
            }
        }

        private void EndGame()
        {
            stopwatch.Stop();
            Leaderboard.SaveScore(playerName, foodCounter, stopwatch.Elapsed);
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

