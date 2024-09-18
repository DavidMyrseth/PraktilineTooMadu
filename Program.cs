using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace PraktilineTööMadu
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запрос имени пользователя
            Console.Write("Write your name: ");
            string playerName = Console.ReadLine();

            int foodCounter = 0;
            string gameOver = "GAME OVER";
            Console.SetWindowSize(80, 25);

            Walls walls = new Walls(80, 25);
            Console.ForegroundColor = ConsoleColor.Red;
            walls.Draw();

            Point p = new Point(4, 5, '@');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            Console.ForegroundColor = ConsoleColor.Green;
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(80, 25, '$');
            Point food = foodCreator.CreateFood();
            Console.ForegroundColor = ConsoleColor.Yellow;
            food.Draw(food.x, food.y, food.sym);

            bool gameOverFlag = false;

            // Создаем и запускаем таймер
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (!gameOverFlag)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    gameOverFlag = true;
                    Console.Clear();
                    int x = Console.WindowWidth;
                    int y = Console.WindowHeight;
                    Console.SetCursorPosition((x - gameOver.Length) / 2, y / 2);
                    Console.WriteLine(gameOver);
                }
                else
                {
                    if (snake.Eat(food))
                    {
                        // Очистка старой еды
                        Console.SetCursorPosition(food.x, food.y);
                        Console.Write(' ');

                        food = foodCreator.CreateFood();
                        foodCounter += 1;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        food.Draw(food.x, food.y, food.sym);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        snake.Move();
                    }
                }

                // Показываем прошедшее время
                TimeSpan elapsedTime = stopwatch.Elapsed;
                Console.SetCursorPosition(Console.WindowWidth - 15, 0);

                // Обновляем счетчик еды и таймер
                Console.SetCursorPosition(0, 0);
                Console.Write($"Food eaten: {foodCounter} Aeg: {elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}");

                Thread.Sleep(100);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }

            // Ожидаем нажатие клавиши перед завершением программы
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WriteLine("Väljumiseks vajutage suvalist klahvi...");
            Console.ReadKey();
        }
        static void DisplayScores()
        {
            string file = (@"..\..\..\nimekirja.txt");
            Console.WriteLine("\nMängijad ja nende punktid:");

            if (File.Exists(file))
            {
                string[] scores = File.ReadAllLines(file);
                foreach (string score in scores)
                {
                    Console.WriteLine(score);
                }
            }
            else
            {
                Console.WriteLine("Veel pole tulemusi.");
            }
        }
    }
}

