using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Второй урок на этом уроке мы сделали небольшой кусок кода с процедурным подходом.
// Принц повторяющийся куски кода и переиспользованное выноситься в отдельную функцию то есть в нашем случае в Draw.

// Третий урок мы научились создавать классы с точкой которые состоят из данных x, y и sym.

// Четвертый урок мы научились созадавать конструкторы, было написано 2 конструктора.
// Функция которых вызываеться при создании обьекта класса точки.
// И разобрали выжный принцеп инкомпсуляция. Инкомпсуляция это свойство классов скрывать детали своей реализации.

// Пятый урок "Комплектующие", "Стэк и Куча". Если p1 и p2 равны то они будут указывать на одну и тоже точку.

// Шестой урок работа с Инкомпсуляцией в данном случае в классе List (F12). 


// Седьмой урок работа с классами 
// Класс состоит из трех составляющих данные, КОНСТРУКТОР (метод) и Обьекты.
namespace PraktilineTööMadu
{
    class Program
    {
        static void Main(string[] args)
        {
            Point p1 = new Point(1, 3, '*'); // Класс с точкой // Конструктор который принимает 3 переменных и имеет метод вывода на экран.
                                             // (Инкомпцуляция -> свойства системы обьединить данныe с методами в классе).
            p1.Draw();

            Point p2 = new Point(4, 5, '#');
            p2.Draw();

            List<int> numlist = new List<int>();
            numlist.Add(0);
            numlist.Add(1);
            numlist.Add(2);

            int x = numlist[ 0 ];
            int y = numlist[ 1 ];
            int z = numlist[ 2 ];

            foreach (int i in numlist)
            {
                Console.WriteLine( i );
            }

            numlist.Remove( 0 );

            List<Point> plist = new List<Point>();
            plist.Add(p1);
            plist.Add(p2);

            Console.ReadLine();
        }
    }
}

// Пятый урок "Комплектующие", "Стэк и Куча".
// int x = 1;
// int y = 3;
// Point p1 = new Point();
// p1.x = 1;
// p1.y = 3;
// p1.sym ='*';
// Point 2 p2 = new Point(4, 5, '#');
// p1 = p2
// p2.x = 8;
// p2.x = 8;

// public satic void Main()
// {
//     Point p1= new Point(1, 3, '*');
//     Move(p1, 10,10);
// }

// public static void Move(Point p, int dx, int dy)
// {
//     p.x= p.x + dx;
//     p.y= p.y + dy;
// }

//Четвертый урок
//p1.x = 1; 
//p1.y = 3; Закоментировано так код сделали компактней
//p1.sym = '*';

//p2.x = 4;
//p2.y = 5; Аналогично первому
//p2.sym = '#';

//Draw( p1.x, p1.y, p1.sym ); Данный кусок кода нам не нуэен так это записано в Point 

//Draw(p2.x, p2.y, p2.sym); Данный кусок кода нам не нуэен так это записано в Point

//int x1 = 1; // переменная 1
//int y1 = 3; // переменная 2                             // все три перемнные логически связаны
//char sym1 = '*'; // сумма двух переменных равна '*'

//Console.SetCursorPosition(x1, y1);
//Console.Write( sym1 );

//Console.ReadLine();

//Draw(x1, y1, sym1);

//int x2 = 4;// переменная 1
//int y2 = 5;// переменная 2                             // все три перемнные логически связаны
//char sym2 = '#'; // сумма двух переменных равна '#'

//Draw(x2, y2, sym2);

//Console.WriteLine();

//static void Draw(int x, int y, char sym) // Функция Draw принимает для входных параметров координаты и символы
//{
//    Console.SetCursorPosition(x, y);
//    Console.Write( sym );

//    Console.ReadLine();
//}