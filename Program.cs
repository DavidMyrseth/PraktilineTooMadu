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
            // Настраиваем размеры окна консоли
            SetupWindow(80, 30);

            // Отображаем стартовое сообщение
            ShowStartMessage();

            // Ожидание нажатия клавиши пробела для продолжения
            WaitForKeyPress(ConsoleKey.Spacebar);

            // Получение имени игрока
            string playerName = GetPlayerName();

            // Отображаем правила игры
            ShowGameRules();

            // Ожидание нажатия пробела для старта игры
            WaitForKeyPress(ConsoleKey.Spacebar);

            // Запуск игры с полученным именем игрока
            StartGame(playerName);
        }

        static void SetupWindow(int width, int height)
        {
            // Устанавливаем размеры окна консоли
            Console.SetWindowSize(width, height);
            // Очищаем экран
            Console.Clear();
        }

        static void ShowStartMessage()
        {
            // Стартовое сообщение
            string message = "You play as Hungry At sign -> @";
            // Сообщение для продолжения
            string prompt = "Press SPACE to continue...";
            // Отображаем сообщение в центре экрана
            DisplayCenteredMessage(message, -2);
            // Отображаем инструкцию о продолжении игры
            DisplayCenteredMessage(prompt, 2);
        }

        static void DisplayCenteredMessage(string message, int yOffset)
        {
            // Вычисляем координаты для центрирования сообщения
            int x = Console.WindowWidth / 2 - message.Length / 2;
            int y = Console.WindowHeight / 2 + yOffset;
            // Устанавливаем позицию курсора
            Console.SetCursorPosition(x, y);
            // Печатаем сообщение
            Console.WriteLine(message);
        }

        static void WaitForKeyPress(ConsoleKey key)
        {
            // Ожидаем, пока пользователь не нажмет нужную клавишу
            while (Console.ReadKey(true).Key != key) { }
        }

        static string GetPlayerName()
        {
            // Очищаем экран
            Console.Clear();
            // Сообщение о вводе имени
            string prompt = "Write your name: ";
            // Отображаем сообщение по центру
            int x = Console.WindowWidth / 2 - prompt.Length / 2;
            int y = Console.WindowHeight / 2 - 2;
            Console.SetCursorPosition(x, y);
            // Печатаем сообщение "Write your name:" и оставляем курсор для ввода рядом
            Console.Write(prompt);

            // Ввод имени игрока на той же строке
            string playerName = Console.ReadLine();
            return playerName;
        }


        static void ShowGameRules()
        {
            // Очищаем экран перед показом правил
            Console.Clear();
            // Правила игры
            string rules = "You play as @. Eat $ to gain 1 point. Eat X to lose 1 point.";
            // Сообщение о начале игры
            string prompt = "Press SPACE to start the game...";
            // Отображаем правила в центре экрана
            DisplayCenteredMessage(rules, 0);
            // Отображаем инструкцию о продолжении
            DisplayCenteredMessage(prompt, 2);
        }

        static void StartGame(string playerName)
        {
            // Счетчик съеденной еды
            int foodCounter = 0;
            // Флаг для проверки состояния игры (закончена или нет)
            bool gameOver = false;

            // Создание объекта стен для игры
            Walls walls = new Walls(Console.WindowWidth, Console.WindowHeight);
            // Инициализация змейки
            Snake snake = InitializeSnake();

            // Создание объектов для хорошей и плохой еды
            FoodCreator goodFoodCreator = new FoodCreator(Console.WindowWidth, Console.WindowHeight, '$');
            FoodCreator badFoodCreator = new FoodCreator(Console.WindowWidth, Console.WindowHeight, 'X');

            // Создание первой позиции хорошей и плохой еды
            Point goodFood = goodFoodCreator.CreateFood();
            Point badFood = badFoodCreator.CreateFood();

            // Отрисовка стен, змейки и еды на экране
            DrawInitialGameObjects(walls, snake, goodFood, badFood);

            // Инициализация таймера для отслеживания времени игры
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Основной игровой цикл
            while (!gameOver)
            {
                // Проверяем, не завершилась ли игра (столкновение со стеной или хвостом змеи)
                gameOver = CheckGameOver(snake, walls);

                if (!gameOver)
                {
                    // Обрабатываем поедание еды змейкой
                    foodCounter = HandleFoodConsumption(snake, goodFoodCreator, badFoodCreator, ref goodFood, ref badFood, foodCounter);
                    // Движение змейки
                    snake.Move();
                }

                // Отображаем текущие результаты игры (счет и время)
                DisplayGameStats(foodCounter, stopwatch.Elapsed);

                // Приостанавливаем выполнение на 75 миллисекунд для анимации змейки
                Thread.Sleep(75);

                // Обрабатываем нажатие клавиш для управления змейкой
                HandleKeyPress(snake);
            }

            // Завершаем игру: сохраняем результат, отображаем сообщение
            EndGame(playerName, foodCounter, stopwatch);
        }

        static Snake InitializeSnake()
        {
            // Начальная точка для змейки
            Point startPoint = new Point(4, 5, '@');
            // Инициализация змейки с указанной длиной и направлением
            Snake snake = new Snake(startPoint, 4, Direction.RIGHT);
            // Задаем цвет змейки
            Console.ForegroundColor = ConsoleColor.Green;
            // Отрисовываем змейку на экране
            snake.Draw();
            return snake; // Возвращаем объект змейки
        }

        static void DrawInitialGameObjects(Walls walls, Snake snake, Point goodFood, Point badFood)
        {
            // Рисуем стены
            Console.ForegroundColor = ConsoleColor.Red;
            walls.Draw();

            // Рисуем хорошую еду
            Console.ForegroundColor = ConsoleColor.Yellow;
            goodFood.Draw(goodFood.x, goodFood.y, goodFood.sym);

            // Рисуем плохую еду
            Console.ForegroundColor = ConsoleColor.Red;
            badFood.Draw(badFood.x, badFood.y, badFood.sym);
        }

        static bool CheckGameOver(Snake snake, Walls walls)
        {
            // Проверяем столкновение змейки со стеной или с собственным хвостом
            if (walls.IsHit(snake) || snake.IsHitTail())
            {
                // Очищаем экран и выводим сообщение о конце игры
                Console.Clear();
                string gameOverMessage = "GAME OVER";
                DisplayCenteredMessage(gameOverMessage, 0);
                return true; // Игра окончена
            }
            return false; // Игра продолжается
        }

        static int HandleFoodConsumption(Snake snake, FoodCreator goodFoodCreator, FoodCreator badFoodCreator, ref Point goodFood, ref Point badFood, int foodCounter)
        {
            // Проверяем, съела ли змея хорошую еду
            if (snake.Eat(goodFood))
            {
                foodCounter += 1; // Увеличиваем счет за хорошую еду
                ClearFoodPosition(goodFood);
                goodFood = goodFoodCreator.CreateFood(); // Создаем новую позицию для хорошей еды
                DrawFood(goodFood, badFood); // Отрисовываем новую хорошую еду
            }
            // Проверяем, съела ли змея плохую еду
            else if (snake.Eat(badFood))
            {
                foodCounter -= 1; // Уменьшаем счет за плохую еду
                ClearFoodPosition(badFood);
                badFood = badFoodCreator.CreateFood(); // Создаем новую позицию для плохой еды
                DrawFood(goodFood, badFood); // Отрисовываем новую плохую еду
            }

            return foodCounter; // Возвращаем обновленный счетчик еды
        }

        static void ClearFoodPosition(Point food)
        {
            // Очищаем позицию еды, заменяя символ на пробел
            Console.SetCursorPosition(food.x, food.y);
            Console.Write(' ');
        }

        static void DrawFood(Point goodFood, Point badFood)
        {
            // Рисуем хорошую еду
            Console.ForegroundColor = ConsoleColor.Yellow;
            goodFood.Draw(goodFood.x, goodFood.y, goodFood.sym);

            // Рисуем плохую еду
            Console.ForegroundColor = ConsoleColor.Red;
            badFood.Draw(badFood.x, badFood.y, badFood.sym);
        }

        static void DisplayGameStats(int foodCounter, TimeSpan elapsedTime)
        {
            // Отображаем счетчик еды и прошедшее время в левом верхнем углу экрана
            Console.SetCursorPosition(0, 0);
            Console.Write($"Food eaten: {foodCounter} Time: {elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}");
        }

        static void HandleKeyPress(Snake snake)
        {
            // Проверяем, была ли нажата клавиша
            if (Console.KeyAvailable)
            {
                // Считываем нажатую клавишу и передаем ее змейке для обработки
                ConsoleKey key = Console.ReadKey(true).Key;
                snake.HandleKey(key);
            }
        }

        static void EndGame(string playerName, int foodCounter, Stopwatch stopwatch)
        {
            // Останавливаем таймер
            stopwatch.Stop();
            // Сохраняем результат игрока в таблицу лидеров
            Leaderboard.SaveScore(playerName, foodCounter, stopwatch.Elapsed);
            // Сообщение об окончании игры и ожидание нажатия клавиши для выхода
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}