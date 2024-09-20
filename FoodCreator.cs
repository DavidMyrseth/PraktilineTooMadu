using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraktilineTööMadu
{
    internal class FoodCreator
    {
        // Ширина и высота карты
        private int mapWidht;
        private int mapHeight;
        // Символ, представляющий еду
        private char sym;

        // Генератор случайных чисел
        Random random = new Random();

        // Конструктор класса FoodCreator
        public FoodCreator(int mapWidht, int mapHeight, char sym)
        {
            this.mapWidht = mapWidht; // Инициализация ширины карты
            this.mapHeight = mapHeight; // Инициализация высоты карты
            this.sym = sym; // Инициализация символа еды
        }

        // Метод для создания еды
        public Point CreateFood()
        {
            // Генерация случайных координат для еды
            int x = random.Next(2, mapWidht - 2); // Случайное значение x (не по краям)
            int y = random.Next(2, mapHeight - 2); // Случайное значение y (не по краям)
            // Возвращаем новую точку, представляющую еду
            return new Point(x, y, sym);
        }
    }
}
